using System;
using System.Globalization;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020004E4 RID: 1252
	internal class KerberosClient : ISessionAuthenticationModule, IAuthenticationModule
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x000A0B3E File Offset: 0x0009FB3E
		internal KerberosClient()
		{
			if (!ComNetOS.IsWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000A0B5D File Offset: 0x0009FB5D
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(challenge, webRequest, credentials, false);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000A0B6C File Offset: 0x0009FB6C
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
				string computeSpn = httpWebRequest.CurrentAuthenticationState.GetComputeSpn(httpWebRequest);
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

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000A0CE2 File Offset: 0x0009FCE2
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000A0CE5 File Offset: 0x0009FCE5
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return this.DoAuthenticate(null, webRequest, credentials, true);
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x000A0CF1 File Offset: 0x0009FCF1
		public string AuthenticationType
		{
			get
			{
				return "Kerberos";
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000A0CF8 File Offset: 0x0009FCF8
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
				securityContext.GetOutgoingBlob(incomingBlob);
				httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated = securityContext.IsMutualAuthFlag;
			}
			httpWebRequest.ServicePoint.SetCachedChannelBinding(httpWebRequest.ChallengedUri, securityContext.ChannelBinding);
			this.ClearSession(httpWebRequest);
			return true;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000A0DC4 File Offset: 0x0009FDC4
		public void ClearSession(WebRequest webRequest)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			httpWebRequest.CurrentAuthenticationState.ClearSession();
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x000A0DE3 File Offset: 0x0009FDE3
		public bool CanUseDefaultCredentials
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400269B RID: 9883
		internal const string AuthType = "Kerberos";

		// Token: 0x0400269C RID: 9884
		internal static string Signature = "Kerberos".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x0400269D RID: 9885
		internal static int SignatureSize = KerberosClient.Signature.Length;
	}
}
