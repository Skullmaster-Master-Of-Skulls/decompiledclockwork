using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020001C8 RID: 456
	internal class NegotiateClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x00060B90 File Offset: 0x0005ED90
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00060B9C File Offset: 0x0005ED9C
		private Authorization DoAuthenticate(string challenge, WebRequest webRequest, ICredentials credentials, bool preAuthenticate)
		{
			if (credentials == null)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			NTAuthentication ntauthentication = null;
			string text = null;
			bool flag = false;
			if (!preAuthenticate)
			{
				int num = NegotiateClient.GetSignatureIndex(challenge, out flag);
				if (num < 0)
				{
					return null;
				}
				int num2 = num + (flag ? "nego2".Length : "negotiate".Length);
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
				NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, "negotiate");
				string empty = string.Empty;
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
			return AuthenticationManager.GetGroupAuthorization(this, (flag ? "Nego2" : "Negotiate") + " " + outgoingBlob, ntauthentication.IsCompleted, ntauthentication, unsafeOrProxyAuthenticatedConnectionSharing, ntauthentication.IsKerberos);
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00060D78 File Offset: 0x0005EF78
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00060D7B File Offset: 0x0005EF7B
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00060D87 File Offset: 0x0005EF87
		public string AuthenticationType
		{
			get
			{
				return "Negotiate";
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00060D90 File Offset: 0x0005EF90
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
			bool flag = true;
			int num = (challenge == null) ? -1 : NegotiateClient.GetSignatureIndex(challenge, out flag);
			if (num >= 0)
			{
				int num2 = num + (flag ? "nego2".Length : "negotiate".Length);
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

		// Token: 0x0600122B RID: 4651 RVA: 0x00060E9C File Offset: 0x0005F09C
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00060EBB File Offset: 0x0005F0BB
		public bool CanUseDefaultCredentials
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00060EC0 File Offset: 0x0005F0C0
		private static int GetSignatureIndex(string challenge, out bool useNego2)
		{
			useNego2 = true;
			int num = -1;
			if (ComNetOS.IsWin7orLater)
			{
				num = AuthenticationManager.FindSubstringNotInQuotes(challenge, "nego2");
			}
			if (num < 0)
			{
				useNego2 = false;
				num = AuthenticationManager.FindSubstringNotInQuotes(challenge, "negotiate");
			}
			return num;
		}

		// Token: 0x04001484 RID: 5252
		internal const string AuthType = "Negotiate";

		// Token: 0x04001485 RID: 5253
		private const string negotiateHeader = "Negotiate";

		// Token: 0x04001486 RID: 5254
		private const string negotiateSignature = "negotiate";

		// Token: 0x04001487 RID: 5255
		private const string nego2Header = "Nego2";

		// Token: 0x04001488 RID: 5256
		private const string nego2Signature = "nego2";
	}
}
