using System;
using System.Globalization;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020004F1 RID: 1265
	internal class NegotiateClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x0600279D RID: 10141 RVA: 0x000A2EFC File Offset: 0x000A1EFC
		public NegotiateClient()
		{
			if (!ComNetOS.IsWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000A2F1B File Offset: 0x000A1F1B
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000A2F28 File Offset: 0x000A1F28
		private Authorization DoAuthenticate(string challenge, WebRequest webRequest, ICredentials credentials, bool preAuthenticate)
		{
			if (credentials == null)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication ntauthentication = null;
			string text = null;
			if (!preAuthenticate)
			{
				int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, NegotiateClient.Signature);
				if (num < 0)
				{
					return null;
				}
				int num2 = num + NegotiateClient.SignatureSize;
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
						text = challenge.Substring(num2, num - num2);
					}
					else
					{
						text = challenge.Substring(num2);
					}
				}
				ntauthentication = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			}
			if (ntauthentication == null)
			{
				NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, NegotiateClient.Signature);
				string text2 = string.Empty;
				if (credential == null || (!(credential is SystemNetworkCredential) && (text2 = credential.InternalGetUserName()).Length == 0))
				{
					return null;
				}
				if (text2.Length + credential.InternalGetPassword().Length + credential.InternalGetDomain().Length > 527)
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
				ntauthentication = new NTAuthentication("Negotiate", credential, computeSpn, httpWebRequest, channelBinding);
				httpWebRequest.CurrentAuthenticationState.SetSecurityContext(ntauthentication, this);
			}
			string outgoingBlob = ntauthentication.GetOutgoingBlob(text);
			if (outgoingBlob == null)
			{
				return null;
			}
			bool unsafeOrProxyAuthenticatedConnectionSharing = httpWebRequest.UnsafeOrProxyAuthenticatedConnectionSharing;
			if (unsafeOrProxyAuthenticatedConnectionSharing)
			{
				httpWebRequest.LockConnection = true;
			}
			httpWebRequest.NtlmKeepAlive = (text == null && ntauthentication.IsValidContext && !ntauthentication.IsKerberos);
			return AuthenticationManager.GetGroupAuthorization(this, "Negotiate " + outgoingBlob, ntauthentication.IsCompleted, ntauthentication, unsafeOrProxyAuthenticatedConnectionSharing, ntauthentication.IsKerberos);
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000A3101 File Offset: 0x000A2101
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000A3104 File Offset: 0x000A2104
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000A3110 File Offset: 0x000A2110
		public string AuthenticationType
		{
			get
			{
				return "Negotiate";
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000A3118 File Offset: 0x000A2118
		public bool Update(string challenge, WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication securityContext = httpWebRequest.CurrentAuthenticationState.GetSecurityContext(this);
			if (securityContext == null)
			{
				return true;
			}
			if (!securityContext.IsCompleted && httpWebRequest.CurrentAuthenticationState.StatusCodeMatch == httpWebRequest.ResponseStatusCode)
			{
				return false;
			}
			if (!httpWebRequest.UnsafeOrProxyAuthenticatedConnectionSharing)
			{
				httpWebRequest.ServicePoint.ReleaseConnectionGroup(httpWebRequest.GetConnectionGroupLine());
			}
			int num = (challenge == null) ? -1 : AuthenticationManager.FindSubstringNotInQuotes(challenge, NegotiateClient.Signature);
			if (num >= 0)
			{
				int num2 = num + NegotiateClient.SignatureSize;
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
				securityContext.GetOutgoingBlob(incomingBlob);
				httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated = securityContext.IsMutualAuthFlag;
			}
			httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, securityContext.ChannelBinding);
			this.ClearSession(httpWebRequest);
			return true;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000A3208 File Offset: 0x000A2208
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000A3227 File Offset: 0x000A2227
		public bool CanUseDefaultCredentials
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040026C7 RID: 9927
		internal const string AuthType = "Negotiate";

		// Token: 0x040026C8 RID: 9928
		internal static string Signature = "Negotiate".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x040026C9 RID: 9929
		internal static int SignatureSize = NegotiateClient.Signature.Length;
	}
}
