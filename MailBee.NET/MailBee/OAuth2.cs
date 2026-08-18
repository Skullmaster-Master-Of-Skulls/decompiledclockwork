using System;
using System.Collections.Specialized;

namespace MailBee
{
	// Token: 0x0200001B RID: 27
	public class OAuth2
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00007256 File Offset: 0x00006256
		public OAuth2(string clientId, string clientSecret)
		{
			if (clientId == null || clientId == string.Empty || clientSecret == null || clientSecret == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.a = clientId;
			this.b = clientSecret;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007294 File Offset: 0x00006294
		public string AuthorizeToken(string uri, StringDictionary parameters)
		{
			if (uri == null || uri == string.Empty || parameters == null)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			StringDictionary stringDictionary = new StringDictionary();
			foreach (object obj in parameters.Keys)
			{
				string key = (string)obj;
				stringDictionary[key] = OAuth.a(parameters[key]);
			}
			parameters = stringDictionary;
			parameters.Add("client_id", this.a);
			return string.Format("{0}?{1}", uri, OAuth.a(parameters));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007340 File Offset: 0x00006340
		public string GetXOAuthKey(string email, string accessToken)
		{
			return OAuth2.GetXOAuthKeyStatic(email, accessToken);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007349 File Offset: 0x00006349
		public static string GetXOAuthKeyStatic(string email, string accessToken)
		{
			if (email == null || email == string.Empty || accessToken == null || accessToken == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			return string.Format("user={0}{2}auth=Bearer {1}{2}{2}", email, accessToken, '\u0001');
		}

		// Token: 0x0400009C RID: 156
		public const string Scope = "scope";

		// Token: 0x0400009D RID: 157
		public const string ClientIdKey = "client_id";

		// Token: 0x0400009E RID: 158
		public const string RedirectUriKey = "redirect_uri";

		// Token: 0x0400009F RID: 159
		public const string ResponseTypeKey = "response_type";

		// Token: 0x040000A0 RID: 160
		private string a;

		// Token: 0x040000A1 RID: 161
		private string b;
	}
}
