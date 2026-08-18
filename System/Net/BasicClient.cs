using System;
using System.Globalization;
using System.Text;

namespace System.Net
{
	// Token: 0x020004B6 RID: 1206
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x06002556 RID: 9558 RVA: 0x00094E88 File Offset: 0x00093E88
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || credentials is SystemNetworkCredential)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || httpWebRequest.ChallengedUri == null)
			{
				return null;
			}
			int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, BasicClient.Signature);
			if (num < 0)
			{
				return null;
			}
			return this.Lookup(httpWebRequest, credentials);
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x00094ED6 File Offset: 0x00093ED6
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00094EDC File Offset: 0x00093EDC
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || credentials is SystemNetworkCredential)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			return this.Lookup(httpWebRequest, credentials);
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x00094F0A File Offset: 0x00093F0A
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00094F14 File Offset: 0x00093F14
		private Authorization Lookup(HttpWebRequest httpWebRequest, ICredentials credentials)
		{
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, BasicClient.Signature);
			if (credential == null)
			{
				return null;
			}
			ICredentialPolicy credentialPolicy = AuthenticationManager.CredentialPolicy;
			if (credentialPolicy != null && !credentialPolicy.ShouldSendCredential(httpWebRequest.ChallengedUri, httpWebRequest, credential, this))
			{
				return null;
			}
			string text = credential.InternalGetUserName();
			string text2 = credential.InternalGetDomain();
			if (ValidationHelper.IsBlankString(text))
			{
				return null;
			}
			string rawString = ((!ValidationHelper.IsBlankString(text2)) ? (text2 + "\\") : "") + text + ":" + credential.InternalGetPassword();
			byte[] inArray = BasicClient.EncodingRightGetBytes(rawString);
			string token = "Basic " + Convert.ToBase64String(inArray);
			return new Authorization(token, true);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00094FC0 File Offset: 0x00093FC0
		internal static byte[] EncodingRightGetBytes(string rawString)
		{
			byte[] bytes = Encoding.Default.GetBytes(rawString);
			string @string = Encoding.Default.GetString(bytes);
			if (string.Compare(rawString, @string, StringComparison.Ordinal) != 0)
			{
				throw ExceptionHelper.MethodNotSupportedException;
			}
			return bytes;
		}

		// Token: 0x04002521 RID: 9505
		internal const string AuthType = "Basic";

		// Token: 0x04002522 RID: 9506
		internal static string Signature = "Basic".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x04002523 RID: 9507
		internal static int SignatureSize = BasicClient.Signature.Length;
	}
}
