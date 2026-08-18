using System;
using System.Globalization;
using System.Text;

namespace System.Net
{
	// Token: 0x02000195 RID: 405
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x000521E8 File Offset: 0x000503E8
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

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00052236 File Offset: 0x00050436
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0005223C File Offset: 0x0005043C
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

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0005226A File Offset: 0x0005046A
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00052274 File Offset: 0x00050474
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

		// Token: 0x06000FBC RID: 4028 RVA: 0x00052320 File Offset: 0x00050520
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

		// Token: 0x040012E3 RID: 4835
		internal const string AuthType = "Basic";

		// Token: 0x040012E4 RID: 4836
		internal static string Signature = "Basic".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x040012E5 RID: 4837
		internal static int SignatureSize = BasicClient.Signature.Length;
	}
}
