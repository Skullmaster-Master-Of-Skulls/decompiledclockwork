using System;
using System.Globalization;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020004D5 RID: 1237
	internal class DigestClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x06002678 RID: 9848 RVA: 0x0009C606 File Offset: 0x0009B606
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0009C614 File Offset: 0x0009B614
		private Authorization DoAuthenticate(string challenge, WebRequest webRequest, ICredentials credentials, bool preAuthenticate)
		{
			if (credentials == null)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, DigestClient.Signature);
			if (credential is SystemNetworkCredential)
			{
				if (DigestClient.WDigestAvailable)
				{
					return this.XPDoAuthenticate(challenge, httpWebRequest, credentials, preAuthenticate);
				}
				return null;
			}
			else
			{
				HttpDigestChallenge httpDigestChallenge;
				if (!preAuthenticate)
				{
					int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, DigestClient.Signature);
					if (num < 0)
					{
						return null;
					}
					httpDigestChallenge = HttpDigest.Interpret(challenge, num, httpWebRequest);
				}
				else
				{
					httpDigestChallenge = (DigestClient.challengeCache.Lookup(httpWebRequest.ChallengedUri.AbsoluteUri) as HttpDigestChallenge);
				}
				if (httpDigestChallenge == null)
				{
					return null;
				}
				if (!DigestClient.CheckQOP(httpDigestChallenge))
				{
					throw new NotSupportedException(SR.GetString("net_QOPNotSupportedException", new object[]
					{
						httpDigestChallenge.QualityOfProtection
					}));
				}
				if (preAuthenticate)
				{
					httpDigestChallenge = httpDigestChallenge.CopyAndIncrementNonce();
					httpDigestChallenge.SetFromRequest(httpWebRequest);
				}
				if (credential == null)
				{
					return null;
				}
				ICredentialPolicy credentialPolicy = AuthenticationManager.CredentialPolicy;
				if (credentialPolicy != null && !credentialPolicy.ShouldSendCredential(httpWebRequest.ChallengedUri, httpWebRequest, credential, this))
				{
					return null;
				}
				string computeSpn = httpWebRequest.CurrentAuthenticationState.GetComputeSpn(httpWebRequest);
				ChannelBinding binding = null;
				if (httpWebRequest.CurrentAuthenticationState.TransportContext != null)
				{
					binding = httpWebRequest.CurrentAuthenticationState.TransportContext.GetChannelBinding(ChannelBindingKind.Endpoint);
				}
				Authorization authorization = HttpDigest.Authenticate(httpDigestChallenge, credential, computeSpn, binding);
				if (!preAuthenticate && authorization != null)
				{
					string[] array = (httpDigestChallenge.Domain == null) ? new string[]
					{
						httpWebRequest.ChallengedUri.GetParts(UriComponents.SchemeAndServer, UriFormat.UriEscaped)
					} : httpDigestChallenge.Domain.Split(DigestClient.singleSpaceArray);
					authorization.ProtectionRealm = ((httpDigestChallenge.Domain == null) ? null : array);
					for (int i = 0; i < array.Length; i++)
					{
						DigestClient.challengeCache.Add(array[i], httpDigestChallenge);
					}
				}
				return authorization;
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0009C7BB File Offset: 0x0009B7BB
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x0009C7C7 File Offset: 0x0009B7C7
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x0009C7CA File Offset: 0x0009B7CA
		public string AuthenticationType
		{
			get
			{
				return "Digest";
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0009C7D4 File Offset: 0x0009B7D4
		internal static bool CheckQOP(HttpDigestChallenge challenge)
		{
			if (challenge.QopPresent)
			{
				for (int i = 0; i >= 0; i += "auth".Length)
				{
					i = challenge.QualityOfProtection.IndexOf("auth", i);
					if (i < 0)
					{
						return false;
					}
					if ((i == 0 || ", \"'\t\r\n".IndexOf(challenge.QualityOfProtection[i - 1]) >= 0) && (i + "auth".Length == challenge.QualityOfProtection.Length || ", \"'\t\r\n".IndexOf(challenge.QualityOfProtection[i + "auth".Length]) >= 0))
					{
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x0009C87C File Offset: 0x0009B87C
		public bool Update(string challenge, WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this) != null)
			{
				return this.XPUpdate(challenge, httpWebRequest);
			}
			if (httpWebRequest.ResponseStatusCode != httpWebRequest.CurrentAuthenticationState.StatusCodeMatch)
			{
				ChannelBinding binding = null;
				if (httpWebRequest.CurrentAuthenticationState.TransportContext != null)
				{
					binding = httpWebRequest.CurrentAuthenticationState.TransportContext.GetChannelBinding(ChannelBindingKind.Endpoint);
				}
				httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, binding);
				return true;
			}
			int num = (challenge == null) ? -1 : AuthenticationManager.FindSubstringNotInQuotes(challenge, DigestClient.Signature);
			if (num < 0)
			{
				return true;
			}
			int num2 = num + DigestClient.SignatureSize;
			if (challenge.Length > num2 && challenge[num2] != ',')
			{
				num2++;
			}
			else
			{
				num = -1;
			}
			if (num >= 0 && challenge.Length > num2)
			{
				challenge.Substring(num2);
			}
			HttpDigestChallenge httpDigestChallenge = HttpDigest.Interpret(challenge, num, httpWebRequest);
			return httpDigestChallenge == null || !httpDigestChallenge.Stale;
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x0009C95B File Offset: 0x0009B95B
		public bool CanUseDefaultCredentials
		{
			get
			{
				return DigestClient.WDigestAvailable;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x0009C962 File Offset: 0x0009B962
		internal static bool WDigestAvailable
		{
			get
			{
				return DigestClient._WDigestAvailable;
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0009C96C File Offset: 0x0009B96C
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0009C98C File Offset: 0x0009B98C
		private Authorization XPDoAuthenticate(string challenge, HttpWebRequest httpWebRequest, ICredentials credentials, bool preAuthenticate)
		{
			NTAuthentication ntauthentication = null;
			string text;
			if (!preAuthenticate)
			{
				int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, DigestClient.Signature);
				if (num < 0)
				{
					return null;
				}
				ntauthentication = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
				text = DigestClient.RefineDigestChallenge(challenge, num);
			}
			else
			{
				HttpDigestChallenge httpDigestChallenge = DigestClient.challengeCache.Lookup(httpWebRequest.ChallengedUri.AbsoluteUri) as HttpDigestChallenge;
				if (httpDigestChallenge == null)
				{
					return null;
				}
				httpDigestChallenge = httpDigestChallenge.CopyAndIncrementNonce();
				httpDigestChallenge.SetFromRequest(httpWebRequest);
				text = httpDigestChallenge.ToBlob();
			}
			UriComponents uriParts;
			if (httpWebRequest.CurrentMethod.ConnectRequest)
			{
				uriParts = UriComponents.HostAndPort;
			}
			else if (httpWebRequest.UsesProxySemantics)
			{
				uriParts = UriComponents.HttpRequestUrl;
			}
			else
			{
				uriParts = UriComponents.PathAndQuery;
			}
			string parts = httpWebRequest.Address.GetParts(uriParts, UriFormat.UriEscaped);
			if (ntauthentication == null)
			{
				NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, DigestClient.Signature);
				if (credential == null || (!(credential is SystemNetworkCredential) && credential.InternalGetUserName().Length == 0))
				{
					return null;
				}
				ICredentialPolicy credentialPolicy = AuthenticationManager.CredentialPolicy;
				if (credentialPolicy != null && !credentialPolicy.ShouldSendCredential(httpWebRequest.ChallengedUri, httpWebRequest, credential, this))
				{
					return null;
				}
				string computeSpn = httpWebRequest.CurrentAuthenticationState.GetComputeSpn(httpWebRequest);
				ChannelBinding channelBinding = null;
				if (httpWebRequest.CurrentAuthenticationState.TransportContext != null)
				{
					channelBinding = httpWebRequest.CurrentAuthenticationState.TransportContext.GetChannelBinding(ChannelBindingKind.Endpoint);
				}
				ntauthentication = new NTAuthentication("WDigest", credential, computeSpn, httpWebRequest, channelBinding);
				httpWebRequest.CurrentAuthenticationState.SetSecurityContext(ntauthentication, this);
			}
			SecurityStatus securityStatus;
			string outgoingDigestBlob = ntauthentication.GetOutgoingDigestBlob(text, httpWebRequest.CurrentMethod.Name, parts, null, false, true, out securityStatus);
			Authorization authorization = new Authorization("Digest " + outgoingDigestBlob, ntauthentication.IsCompleted, string.Empty, ntauthentication.IsMutualAuthFlag);
			if (!preAuthenticate)
			{
				HttpDigestChallenge httpDigestChallenge2 = HttpDigest.Interpret(text, -1, httpWebRequest);
				string[] array = (httpDigestChallenge2.Domain == null) ? new string[]
				{
					httpWebRequest.ChallengedUri.GetParts(UriComponents.SchemeAndServer, UriFormat.UriEscaped)
				} : httpDigestChallenge2.Domain.Split(DigestClient.singleSpaceArray);
				authorization.ProtectionRealm = ((httpDigestChallenge2.Domain == null) ? null : array);
				for (int i = 0; i < array.Length; i++)
				{
					DigestClient.challengeCache.Add(array[i], httpDigestChallenge2);
				}
			}
			return authorization;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0009CBA4 File Offset: 0x0009BBA4
		private bool XPUpdate(string challenge, HttpWebRequest httpWebRequest)
		{
			NTAuthentication securityContext = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			if (securityContext == null)
			{
				return false;
			}
			int num = (challenge == null) ? -1 : AuthenticationManager.FindSubstringNotInQuotes(challenge, DigestClient.Signature);
			if (num < 0)
			{
				httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, securityContext.ChannelBinding);
				this.ClearSession(httpWebRequest);
				return true;
			}
			if (httpWebRequest.ResponseStatusCode != httpWebRequest.CurrentAuthenticationState.StatusCodeMatch)
			{
				httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, securityContext.ChannelBinding);
				this.ClearSession(httpWebRequest);
				return true;
			}
			string incomingBlob = DigestClient.RefineDigestChallenge(challenge, num);
			SecurityStatus securityStatus;
			securityContext.GetOutgoingDigestBlob(incomingBlob, httpWebRequest.CurrentMethod.Name, null, null, false, true, out securityStatus);
			httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated = securityContext.IsMutualAuthFlag;
			return securityContext.IsCompleted;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0009CC6C File Offset: 0x0009BC6C
		private static string RefineDigestChallenge(string challenge, int index)
		{
			if (challenge == null || index >= challenge.Length)
			{
				throw new ArgumentOutOfRangeException("challenge", challenge);
			}
			int num = index + DigestClient.SignatureSize;
			if (challenge.Length > num && challenge[num] != ',')
			{
				num++;
			}
			else
			{
				index = -1;
			}
			if (index >= 0 && challenge.Length > num)
			{
				string text = challenge.Substring(num);
				int num2 = 0;
				int num3 = num2;
				bool flag = true;
				HttpDigestChallenge httpDigestChallenge = new HttpDigestChallenge();
				int num4;
				for (;;)
				{
					num4 = num3;
					index = AuthenticationManager.SplitNoQuotes(text, ref num4);
					if (num4 < 0)
					{
						break;
					}
					string name = text.Substring(num3, num4 - num3);
					string value;
					if (index < 0)
					{
						value = HttpDigest.unquote(text.Substring(num4 + 1));
					}
					else
					{
						value = HttpDigest.unquote(text.Substring(num4 + 1, index - num4 - 1));
					}
					flag = httpDigestChallenge.defineAttribute(name, value);
					if (index < 0 || !flag)
					{
						break;
					}
					index = (num3 = index + 1);
				}
				if ((!flag || num4 < 0) && num3 < text.Length)
				{
					text = text.Substring(0, num3 - 1);
				}
				return text;
			}
			throw new ArgumentOutOfRangeException("challenge", challenge);
		}

		// Token: 0x040025F7 RID: 9719
		internal const string AuthType = "Digest";

		// Token: 0x040025F8 RID: 9720
		internal static string Signature = "Digest".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x040025F9 RID: 9721
		internal static int SignatureSize = DigestClient.Signature.Length;

		// Token: 0x040025FA RID: 9722
		private static PrefixLookup challengeCache = new PrefixLookup();

		// Token: 0x040025FB RID: 9723
		private static readonly char[] singleSpaceArray = new char[]
		{
			' '
		};

		// Token: 0x040025FC RID: 9724
		private static bool _WDigestAvailable = SSPIWrapper.GetVerifyPackageInfo(GlobalSSPI.SSPIAuth, "WDigest") != null;
	}
}
