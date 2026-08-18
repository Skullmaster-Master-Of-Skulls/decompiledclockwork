using System;
using System.Globalization;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020001BB RID: 443
	internal class KerberosClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x0005E429 File Offset: 0x0005C629
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0005E438 File Offset: 0x0005C638
		private Authorization DoAuthenticate(string challenge, WebRequest webRequest, ICredentials credentials, bool preAuthenticate)
		{
			if (credentials == null)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication ntauthentication = null;
			string incomingBlob = null;
			if (!preAuthenticate)
			{
				int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, KerberosClient.Signature);
				if (num < 0)
				{
					return null;
				}
				int num2 = num + KerberosClient.SignatureSize;
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
					num = challenge.IndexOf(',', num2);
					if (num != -1)
					{
						incomingBlob = challenge.Substring(num2, num - num2);
					}
					else
					{
						incomingBlob = challenge.Substring(num2);
					}
				}
				ntauthentication = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			}
			if (ntauthentication == null)
			{
				NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, KerberosClient.Signature);
				if (credential == null || (!(credential is SystemNetworkCredential) && credential.InternalGetUserName().Length == 0))
				{
					return null;
				}
				ICredentialPolicy credentialPolicy = AuthenticationManager.CredentialPolicy;
				if (credentialPolicy != null && !credentialPolicy.ShouldSendCredential(httpWebRequest.ChallengedUri, httpWebRequest, credential, this))
				{
					return null;
				}
				SpnToken computeSpn = httpWebRequest.CurrentAuthenticationState.GetComputeSpn(httpWebRequest);
				ChannelBinding channelBinding = null;
				if (httpWebRequest.CurrentAuthenticationState.TransportContext != null)
				{
					channelBinding = httpWebRequest.CurrentAuthenticationState.TransportContext.GetChannelBinding(ChannelBindingKind.Endpoint);
				}
				ntauthentication = new NTAuthentication("Kerberos", credential, computeSpn, httpWebRequest, channelBinding);
				httpWebRequest.CurrentAuthenticationState.SetSecurityContext(ntauthentication, this);
			}
			string outgoingBlob = ntauthentication.GetOutgoingBlob(incomingBlob);
			if (outgoingBlob == null)
			{
				return null;
			}
			return new Authorization("Kerberos " + outgoingBlob, ntauthentication.IsCompleted, string.Empty, ntauthentication.IsMutualAuthFlag);
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0005E5B6 File Offset: 0x0005C7B6
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0005E5B9 File Offset: 0x0005C7B9
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0005E5C5 File Offset: 0x0005C7C5
		public string AuthenticationType
		{
			get
			{
				return "Kerberos";
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0005E5CC File Offset: 0x0005C7CC
		public bool Update(string challenge, WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication securityContext = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			if (securityContext == null)
			{
				return true;
			}
			if (httpWebRequest.CurrentAuthenticationState.StatusCodeMatch == httpWebRequest.ResponseStatusCode)
			{
				return false;
			}
			int num = (challenge == null) ? -1 : AuthenticationManager.FindSubstringNotInQuotes(challenge, KerberosClient.Signature);
			if (num >= 0)
			{
				int num2 = num + KerberosClient.SignatureSize;
				string incomingBlob = null;
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
					incomingBlob = challenge.Substring(num2);
				}
				string outgoingBlob = securityContext.GetOutgoingBlob(incomingBlob);
				httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated = securityContext.IsMutualAuthFlag;
			}
			httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, securityContext.ChannelBinding);
			this.ClearSession(httpWebRequest);
			return true;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0005E69C File Offset: 0x0005C89C
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0005E6BB File Offset: 0x0005C8BB
		public bool CanUseDefaultCredentials
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400144D RID: 5197
		internal const string AuthType = "Kerberos";

		// Token: 0x0400144E RID: 5198
		internal static string Signature = "Kerberos".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x0400144F RID: 5199
		internal static int SignatureSize = KerberosClient.Signature.Length;
	}
}
