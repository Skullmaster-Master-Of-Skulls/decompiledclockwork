using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x0200050A RID: 1290
	internal abstract class WebProxyDataBuilder
	{
		// Token: 0x06002809 RID: 10249 RVA: 0x000A507C File Offset: 0x000A407C
		public WebProxyData Build()
		{
			this.m_Result = new WebProxyData();
			this.BuildInternal();
			return this.m_Result;
		}

		// Token: 0x0600280A RID: 10250
		protected abstract void BuildInternal();

		// Token: 0x0600280B RID: 10251 RVA: 0x000A5098 File Offset: 0x000A4098
		protected void SetProxyAndBypassList(string addressString, string bypassListString)
		{
			Uri uri = null;
			Hashtable hashtable = null;
			if (addressString != null)
			{
				uri = WebProxyDataBuilder.ParseProxyUri(addressString, true);
				if (uri == null)
				{
					hashtable = WebProxyDataBuilder.ParseProtocolProxies(addressString);
				}
				if ((uri != null || hashtable != null) && bypassListString != null)
				{
					bool bypassOnLocal = false;
					this.m_Result.bypassList = WebProxyDataBuilder.ParseBypassList(bypassListString, out bypassOnLocal);
					this.m_Result.bypassOnLocal = bypassOnLocal;
				}
			}
			if (hashtable != null)
			{
				uri = (hashtable["http"] as Uri);
			}
			this.m_Result.proxyAddress = uri;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000A5114 File Offset: 0x000A4114
		protected void SetAutoProxyUrl(string autoConfigUrl)
		{
			if (!string.IsNullOrEmpty(autoConfigUrl))
			{
				Uri scriptLocation = null;
				if (Uri.TryCreate(autoConfigUrl, UriKind.Absolute, out scriptLocation))
				{
					this.m_Result.scriptLocation = scriptLocation;
				}
			}
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000A5142 File Offset: 0x000A4142
		protected void SetAutoDetectSettings(bool value)
		{
			this.m_Result.automaticallyDetectSettings = value;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000A5150 File Offset: 0x000A4150
		private static Uri ParseProxyUri(string proxyString, bool validate)
		{
			if (validate)
			{
				if (proxyString.Length == 0)
				{
					return null;
				}
				if (proxyString.IndexOf('=') != -1)
				{
					return null;
				}
			}
			if (proxyString.IndexOf("://") == -1)
			{
				proxyString = "http://" + proxyString;
			}
			try
			{
				return new Uri(proxyString);
			}
			catch (UriFormatException ex)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, ex.Message);
				}
			}
			return null;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000A51C8 File Offset: 0x000A41C8
		private static Hashtable ParseProtocolProxies(string proxyListString)
		{
			if (proxyListString.Length == 0)
			{
				return null;
			}
			string[] array = proxyListString.Split(WebProxyDataBuilder.s_AddressListSplitChars);
			bool flag = true;
			string key = null;
			Hashtable hashtable = new Hashtable(CaseInsensitiveAscii.StaticInstance);
			foreach (string text in array)
			{
				string text2 = text.Trim().ToLower(CultureInfo.InvariantCulture);
				if (flag)
				{
					key = text2;
				}
				else
				{
					hashtable[key] = WebProxyDataBuilder.ParseProxyUri(text2, false);
				}
				flag = !flag;
			}
			if (hashtable.Count == 0)
			{
				return null;
			}
			return hashtable;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000A5254 File Offset: 0x000A4254
		private static string BypassStringEscape(string rawString)
		{
			Regex regex = new Regex("^(?<scheme>.*://)?(?<host>[^:]*)(?<port>:[0-9]{1,5})?$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			Match match = regex.Match(rawString);
			string text;
			string text2;
			string text3;
			if (match.Success)
			{
				text = match.Groups["scheme"].Value;
				text2 = match.Groups["host"].Value;
				text3 = match.Groups["port"].Value;
			}
			else
			{
				text = string.Empty;
				text2 = rawString;
				text3 = string.Empty;
			}
			text = WebProxyDataBuilder.ConvertRegexReservedChars(text);
			text2 = WebProxyDataBuilder.ConvertRegexReservedChars(text2);
			text3 = WebProxyDataBuilder.ConvertRegexReservedChars(text3);
			if (text == string.Empty)
			{
				text = "(?:.*://)?";
			}
			if (text3 == string.Empty)
			{
				text3 = "(?::[0-9]{1,5})?";
			}
			return string.Concat(new string[]
			{
				"^",
				text,
				text2,
				text3,
				"$"
			});
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000A5348 File Offset: 0x000A4348
		private static string ConvertRegexReservedChars(string rawString)
		{
			if (rawString.Length == 0)
			{
				return rawString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in rawString)
			{
				if ("#$()+.?[\\^{|".IndexOf(c) != -1)
				{
					stringBuilder.Append('\\');
				}
				else if (c == '*')
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000A53B8 File Offset: 0x000A43B8
		private static ArrayList ParseBypassList(string bypassListString, out bool bypassOnLocal)
		{
			string[] array = bypassListString.Split(WebProxyDataBuilder.s_BypassListDelimiter);
			bypassOnLocal = false;
			if (array.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = null;
			foreach (string text in array)
			{
				if (text != null)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						if (string.Compare(text2, "<local>", StringComparison.OrdinalIgnoreCase) == 0)
						{
							bypassOnLocal = true;
						}
						else
						{
							text2 = WebProxyDataBuilder.BypassStringEscape(text2);
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							if (!arrayList.Contains(text2))
							{
								arrayList.Add(text2);
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04002757 RID: 10071
		private const string regexReserved = "#$()+.?[\\^{|";

		// Token: 0x04002758 RID: 10072
		private static readonly char[] s_AddressListSplitChars = new char[]
		{
			';',
			'='
		};

		// Token: 0x04002759 RID: 10073
		private static readonly char[] s_BypassListDelimiter = new char[]
		{
			';'
		};

		// Token: 0x0400275A RID: 10074
		private WebProxyData m_Result;
	}
}
