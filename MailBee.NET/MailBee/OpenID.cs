using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using a;

namespace MailBee
{
	// Token: 0x0200001C RID: 28
	[Obsolete("This class is now obsolete as OpenID technology itself.")]
	public class OpenID
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00007385 File Offset: 0x00006385
		private OpenID()
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007390 File Offset: 0x00006390
		public static string GetOpenIDServer(string openIdKeyUri)
		{
			if (openIdKeyUri == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			WebRequest webRequest = null;
			HttpWebResponse httpWebResponse = null;
			Stream stream = null;
			StreamReader streamReader = null;
			string a_ = string.Empty;
			try
			{
				webRequest = WebRequest.Create(openIdKeyUri);
			}
			catch (UriFormatException)
			{
				throw new MailBeeInvalidArgumentException(44);
			}
			try
			{
				httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
				stream = httpWebResponse.GetResponseStream();
				streamReader = new StreamReader(stream);
				a_ = streamReader.ReadToEnd();
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
			return OpenID.a(a_);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007460 File Offset: 0x00006460
		private static string a(string A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_0.IndexOf("<xrds:") != -1)
			{
				Match match = new Regex("<URI>([^>]*)</URI>", RegexOptions.IgnoreCase).Match(A_0);
				if (match.Success)
				{
					return match.Groups[1].Value;
				}
			}
			else
			{
				Match match2 = new Regex("<link[^>]*rel=['\"]openid.server['\"][^>]*href=['\"]([^'\"]+)['\"][^>]*/?>", RegexOptions.IgnoreCase).Match(A_0);
				if (match2.Success)
				{
					return match2.Groups[1].Value;
				}
				match2 = new Regex("<link[^>]*href='\"([^'\"]+)['\"][^>]*rel=['\"]openid.server['\"][^>]*/?>").Match(A_0);
				if (match2.Success)
				{
					return match2.Groups[1].Value;
				}
			}
			return null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007504 File Offset: 0x00006504
		public static StringDictionary GetParamsForYahooOpenIdWithOAuth()
		{
			StringDictionary stringDictionary = new StringDictionary();
			stringDictionary["openid.ns"] = "http://specs.openid.net/auth/2.0";
			stringDictionary["openid.claimed_id"] = "http://specs.openid.net/auth/2.0/identifier_select";
			stringDictionary["openid.identity"] = "http://specs.openid.net/auth/2.0/identifier_select";
			stringDictionary["openid.mode"] = "checkid_setup";
			stringDictionary["openid.ns.oauth"] = "http://specs.openid.net/extensions/oauth/1.0";
			return stringDictionary;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007568 File Offset: 0x00006568
		public static StringDictionary GetParamsForGoogleOpenIdWithOAuth()
		{
			StringDictionary stringDictionary = new StringDictionary();
			stringDictionary["openid.ns"] = "http://specs.openid.net/auth/2.0";
			stringDictionary["openid.claimed_id"] = "http://specs.openid.net/auth/2.0/identifier_select";
			stringDictionary["openid.identity"] = "http://specs.openid.net/auth/2.0/identifier_select";
			stringDictionary["openid.mode"] = "checkid_setup";
			stringDictionary["openid.ns.ui"] = "http://specs.openid.net/extensions/ui/1.0";
			stringDictionary["openid.ns.ext1"] = "http://openid.net/srv/ax/1.0";
			stringDictionary["openid.ext1.mode"] = "fetch_request";
			stringDictionary["openid.ext1.type.email"] = "http://schema.openid.net/contact/email";
			stringDictionary["openid.ext1.type.first"] = "http://schema.openid.net/namePerson/first";
			stringDictionary["openid.ext1.type.last"] = "http://schema.openid.net/namePerson/last";
			stringDictionary["openid.ext1.type.country"] = "http://schema.openid.net/contact/country/home";
			stringDictionary["openid.ext1.type.lang"] = "http://axschema.org/pref/language";
			stringDictionary["openid.ext1.required"] = "email,first,last";
			stringDictionary["openid.ns.oauth"] = "http://specs.openid.net/extensions/oauth/1.0";
			stringDictionary["openid.oauth.scope"] = "https://mail.google.com/";
			return stringDictionary;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000766C File Offset: 0x0000666C
		public static string GetRedirectURL(string openIdServerUrl, StringDictionary parameters)
		{
			if (openIdServerUrl == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in parameters.Keys)
			{
				string text = (string)obj;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("&");
				}
				stringBuilder.Append(text + "=" + au.g(parameters[text]));
			}
			return openIdServerUrl + "?" + stringBuilder.ToString();
		}
	}
}
