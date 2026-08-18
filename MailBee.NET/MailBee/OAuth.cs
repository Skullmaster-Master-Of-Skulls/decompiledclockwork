using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using a;

namespace MailBee
{
	// Token: 0x02000019 RID: 25
	public class OAuth
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00006860 File Offset: 0x00005860
		private OAuth(string A_0, string A_1, OAuth.a A_2)
		{
			if (A_0 == null || A_0 == string.Empty || A_1 == null || A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.p = A_0;
			this.q = A_1;
			this.w = A_2;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000068C6 File Offset: 0x000058C6
		public OAuth(string consumerKey, string consumerSecret) : this(consumerKey, consumerSecret, OAuth.a.a)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000068D1 File Offset: 0x000058D1
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000068D9 File Offset: 0x000058D9
		public bool EnableOpenIDHybrid
		{
			get
			{
				return this.z;
			}
			set
			{
				this.z = value;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000068E2 File Offset: 0x000058E2
		public void RequestToken(string uri)
		{
			this.RequestToken(uri, null);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000068EC File Offset: 0x000058EC
		public void RequestToken(string uri, StringDictionary parameters)
		{
			if (uri == null || uri == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (parameters == null)
			{
				parameters = new StringDictionary();
			}
			this.u = this.a();
			this.t = this.b();
			string arg;
			string text;
			string a_ = this.b(this.a(uri, parameters, out arg, out text));
			text += string.Format("&{0}={1}", "oauth_signature", OAuth.a(a_));
			StringDictionary stringDictionary = this.c(string.Format("{0}?{1}", arg, text));
			this.r = stringDictionary["oauth_token"];
			this.s = stringDictionary["oauth_token_secret"];
			this.x = true;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000699E File Offset: 0x0000599E
		public string AuthorizeToken(string uri)
		{
			return this.AuthorizeToken(uri, null);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000069A8 File Offset: 0x000059A8
		public string AuthorizeToken(string uri, StringDictionary parameters)
		{
			if (uri == null || uri == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (!this.x)
			{
				throw new MailBeeInvalidStateException(11);
			}
			if (parameters == null)
			{
				parameters = new StringDictionary();
			}
			string arg;
			string text;
			string a_ = this.b(this.a(uri, parameters, out arg, out text));
			text += string.Format("&{0}={1}", "oauth_signature", OAuth.a(a_));
			this.x = false;
			this.y = true;
			return string.Format("{0}?{1}", arg, text);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006A30 File Offset: 0x00005A30
		public void AccessToken(string uri, string key)
		{
			this.AccessToken(uri, key, null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006A3C File Offset: 0x00005A3C
		public void AccessToken(string uri, string key, StringDictionary parameters)
		{
			if (uri == null || key == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (uri == string.Empty || key == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (!this.z && !this.y)
			{
				throw new MailBeeInvalidStateException(11);
			}
			if (parameters == null)
			{
				parameters = new StringDictionary();
			}
			this.u = this.a();
			this.t = this.b();
			if (!this.z)
			{
				parameters.Add("oauth_verifier", key);
			}
			else
			{
				parameters.Add("oauth_token", key);
			}
			string arg;
			string text;
			string a_ = this.b(this.a(uri, parameters, out arg, out text));
			text += string.Format("&{0}={1}", "oauth_signature", OAuth.a(a_));
			StringDictionary stringDictionary = this.c(string.Format("{0}?{1}", arg, text));
			this.r = stringDictionary["oauth_token"];
			this.s = stringDictionary["oauth_token_secret"];
			this.y = false;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006B40 File Offset: 0x00005B40
		public string Token
		{
			get
			{
				return this.r;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00006B48 File Offset: 0x00005B48
		public string TokenSecret
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006B50 File Offset: 0x00005B50
		public string GetXOAuthKey(string uri)
		{
			return this.GetXOAuthKey(uri, new StringDictionary());
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006B60 File Offset: 0x00005B60
		public string GetXOAuthKey(string uri, StringDictionary parameters)
		{
			if (uri == null || uri == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			string arg;
			string text;
			string a_ = this.b(this.a(uri, parameters, out arg, out text));
			text += string.Format("&{0}={1}", "oauth_signature", OAuth.a(a_));
			string[] array = text.Split(new char[]
			{
				'&'
			});
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					'='
				}, 2);
				stringBuilder.Append((stringBuilder.ToString() == string.Empty) ? string.Format("{0}=\"{1}\"", array3[0], array3[1]) : string.Format(",{0}=\"{1}\"", array3[0], array3[1]));
			}
			return string.Format("GET {0} {1}", arg, stringBuilder.ToString());
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006C48 File Offset: 0x00005C48
		private StringDictionary c(string A_0)
		{
			WebRequest webRequest = null;
			HttpWebResponse httpWebResponse = null;
			Stream stream = null;
			StreamReader streamReader = null;
			string text = string.Empty;
			try
			{
				webRequest = WebRequest.Create(A_0);
			}
			catch (UriFormatException a_)
			{
				throw new MailBeeDataParsingException(44, a_);
			}
			try
			{
				httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
				stream = httpWebResponse.GetResponseStream();
				streamReader = new StreamReader(stream);
				text = streamReader.ReadToEnd();
			}
			catch (IOException a_2)
			{
				throw new MailBeeStreamException(30, a_2);
			}
			catch (WebException a_3)
			{
				throw new MailBeeWebException(34, a_3);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
				if (httpWebResponse != null)
				{
					httpWebResponse.Close();
				}
			}
			StringDictionary stringDictionary = new StringDictionary();
			string[] array = text.Split(new char[]
			{
				'&'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				}, 2);
				if (array2.Length == 2)
				{
					stringDictionary.Add(array2[0], array2[1]);
				}
			}
			return stringDictionary;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006D64 File Offset: 0x00005D64
		private string a(string A_0, StringDictionary A_1, out string A_2, out string A_3)
		{
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			StringDictionary stringDictionary = new StringDictionary();
			foreach (object obj in A_1.Keys)
			{
				string key = (string)obj;
				stringDictionary[key] = OAuth.a(A_1[key]);
			}
			A_1 = stringDictionary;
			Uri uri = new Uri(A_0);
			this.a(A_1, uri.Query);
			A_1.Add("oauth_version", "1.0");
			A_1.Add("oauth_nonce", this.u);
			A_1.Add("oauth_timestamp", this.t);
			switch (this.w)
			{
			case OAuth.a.a:
				A_1.Add("oauth_signature_method", "HMAC-SHA1");
				break;
			case OAuth.a.b:
				A_1.Add("oauth_signature_method", "PLAINTEXT");
				break;
			case OAuth.a.c:
				A_1.Add("oauth_signature_method", "RSA-SHA1");
				break;
			}
			A_1.Add("oauth_consumer_key", OAuth.a(this.p));
			if (this.r != null)
			{
				A_1.Add("oauth_token", this.r);
			}
			A_3 = OAuth.a(A_1);
			A_2 = string.Format("{0}://{1}", uri.Scheme, uri.Host);
			if ((!(uri.Scheme == "http") || uri.Port != 80) && (!(uri.Scheme == "https") || uri.Port != 443))
			{
				A_2 = A_2 + ":" + uri.Port;
			}
			A_2 += uri.AbsolutePath;
			return string.Format("GET&{0}&{1}", OAuth.a(A_2), OAuth.a(A_3));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006F50 File Offset: 0x00005F50
		private string b(string A_0)
		{
			switch (this.w)
			{
			case OAuth.a.a:
			{
				HMACSHA1 hmacsha = new HMACSHA1();
				hmacsha.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", OAuth.a(this.q), (this.s == string.Empty) ? string.Empty : this.s));
				byte[] bytes = Encoding.ASCII.GetBytes(A_0);
				return Convert.ToBase64String(hmacsha.ComputeHash(bytes));
			}
			case OAuth.a.b:
				return au.g(string.Format("{0}&{1}", this.q, this.s));
			case OAuth.a.c:
				throw new NotImplementedException();
			default:
				throw new ArgumentException("Unknown signature type", "signatureType");
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007010 File Offset: 0x00006010
		private void a(StringDictionary A_0, string A_1)
		{
			if (A_1.StartsWith("?"))
			{
				A_1 = A_1.Remove(0, 1);
			}
			if (A_1 != null && A_1 != string.Empty)
			{
				foreach (string text in A_1.Split(new char[]
				{
					'&'
				}))
				{
					if (text != null && text != string.Empty && !text.StartsWith("oauth_"))
					{
						string[] array2 = text.Split(new char[]
						{
							'='
						}, 2);
						if (array2.Length == 2)
						{
							A_0.Add(array2[0], array2[1]);
						}
						else
						{
							A_0.Add(text, string.Empty);
						}
					}
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000070BC File Offset: 0x000060BC
		internal static string a(StringDictionary A_0)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in A_0.Keys)
			{
				string text = (string)obj;
				arrayList.Add(string.Format("{0}={1}", text, A_0[text]));
			}
			StringBuilder stringBuilder = new StringBuilder();
			arrayList.Sort();
			for (int i = 0; i < arrayList.Count; i++)
			{
				stringBuilder.Append(arrayList[i]);
				if (i < arrayList.Count - 1)
				{
					stringBuilder.Append("&");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007180 File Offset: 0x00006180
		internal static string a(string A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in A_0)
			{
				if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".IndexOf(c) != -1)
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append("%" + string.Format("{0:X2}", (int)c));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000071F0 File Offset: 0x000061F0
		private string b()
		{
			return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000722C File Offset: 0x0000622C
		private string a()
		{
			return this.v.Next(123400, 9999999).ToString();
		}

		// Token: 0x0400007D RID: 125
		private const string a = "HMAC-SHA1";

		// Token: 0x0400007E RID: 126
		private const string b = "PLAINTEXT";

		// Token: 0x0400007F RID: 127
		private const string c = "RSA-SHA1";

		// Token: 0x04000080 RID: 128
		private const string d = "1.0";

		// Token: 0x04000081 RID: 129
		private const string e = "oauth_";

		// Token: 0x04000082 RID: 130
		private const string f = "oauth_consumer_key";

		// Token: 0x04000083 RID: 131
		public const string OAuthCallbackKey = "oauth_callback";

		// Token: 0x04000084 RID: 132
		private const string g = "oauth_version";

		// Token: 0x04000085 RID: 133
		private const string h = "oauth_signature_method";

		// Token: 0x04000086 RID: 134
		private const string i = "oauth_signature";

		// Token: 0x04000087 RID: 135
		private const string j = "oauth_timestamp";

		// Token: 0x04000088 RID: 136
		private const string k = "oauth_nonce";

		// Token: 0x04000089 RID: 137
		private const string l = "oauth_token";

		// Token: 0x0400008A RID: 138
		private const string m = "oauth_token_secret";

		// Token: 0x0400008B RID: 139
		private const string n = "oauth_verifier";

		// Token: 0x0400008C RID: 140
		private const string o = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

		// Token: 0x0400008D RID: 141
		private string p;

		// Token: 0x0400008E RID: 142
		private string q;

		// Token: 0x0400008F RID: 143
		private string r;

		// Token: 0x04000090 RID: 144
		private string s = string.Empty;

		// Token: 0x04000091 RID: 145
		private string t;

		// Token: 0x04000092 RID: 146
		private string u;

		// Token: 0x04000093 RID: 147
		private Random v = new Random();

		// Token: 0x04000094 RID: 148
		private OAuth.a w;

		// Token: 0x04000095 RID: 149
		private bool x;

		// Token: 0x04000096 RID: 150
		private bool y;

		// Token: 0x04000097 RID: 151
		private bool z;

		// Token: 0x0200001A RID: 26
		private enum a
		{
			// Token: 0x04000099 RID: 153
			a,
			// Token: 0x0400009A RID: 154
			b,
			// Token: 0x0400009B RID: 155
			c
		}
	}
}
