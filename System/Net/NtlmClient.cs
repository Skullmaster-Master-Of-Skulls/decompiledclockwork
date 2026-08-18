using System;
using System.Globalization;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020004FA RID: 1274
	internal class NtlmClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x060027E5 RID: 10213 RVA: 0x000A4A77 File Offset: 0x000A3A77
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000A4A84 File Offset: 0x000A3A84
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
				int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, NtlmClient.Signature);
				if (num < 0)
				{
					return null;
				}
				int num2 = num + NtlmClient.SignatureSize;
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
				NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, NtlmClient.Signature);
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
				ntauthentication = new NTAuthentication("NTLM", credential, computeSpn, httpWebRequest, channelBinding);
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
			httpWebRequest.NtlmKeepAlive = (text == null);
			return AuthenticationManager.GetGroupAuthorization(this, "NTLM " + outgoingBlob, ntauthentication.IsCompleted, ntauthentication, unsafeOrProxyAuthenticatedConnectionSharing, false);
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x000A4C45 File Offset: 0x000A3C45
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000A4C48 File Offset: 0x000A3C48
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000A4C54 File Offset: 0x000A3C54
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000A4C5C File Offset: 0x000A3C5C
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
			this.ClearSession(httpWebRequest);
			if (!httpWebRequest.UnsafeOrProxyAuthenticatedConnectionSharing)
			{
				httpWebRequest.ServicePoint.ReleaseConnectionGroup(httpWebRequest.GetConnectionGroupLine());
			}
			httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, securityContext.ChannelBinding);
			return true;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000A4CD8 File Offset: 0x000A3CD8
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000A4CF7 File Offset: 0x000A3CF7
		public bool CanUseDefaultCredentials
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002717 RID: 10007
		internal const string AuthType = "NTLM";

		// Token: 0x04002718 RID: 10008
		internal const int MaxNtlmCredentialSize = 527;

		// Token: 0x04002719 RID: 10009
		internal static string Signature = "NTLM".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x0400271A RID: 10010
		internal static int SignatureSize = NtlmClient.Signature.Length;
	}
}
