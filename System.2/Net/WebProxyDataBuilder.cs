using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x020001E3 RID: 483
	internal abstract class WebProxyDataBuilder
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x0006391C File Offset: 0x00061B1C
		public WebProxyData Build()
		{
			this.m_Result = new WebProxyData();
			this.BuildInternal();
			return this.m_Result;
		}

		// Token: 0x060012D4 RID: 4820
		protected abstract void BuildInternal();

		// Token: 0x060012D5 RID: 4821 RVA: 0x00063938 File Offset: 0x00061B38
		protected void SetProxyAndBypassList(string addressString, string bypassListString)
		{
			if (addressString != null)
			{
				addressString = addressString.Trim();
				if (addressString != string.Empty)
				{
					if (addressString.IndexOf('=') == -1)
					{
						this.m_Result.proxyAddress = WebProxyDataBuilder.ParseProxyUri(addressString);
					}
					else
					{
						this.m_Result.proxyHostAddresses = WebProxyDataBuilder.ParseProtocolProxies(addressString);
					}
					if (bypassListString != null)
					{
						bypassListString = bypassListString.Trim();
						if (bypassListString != string.Empty)
						{
							bool bypassOnLocal = false;
							this.m_Result.bypassList = WebProxyDataBuilder.ParseBypassList(bypassListString, out bypassOnLocal);
							this.m_Result.bypassOnLocal = bypassOnLocal;
						}
					}
				}
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000639C8 File Offset: 0x00061BC8
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

		// Token: 0x060012D7 RID: 4823 RVA: 0x000639F6 File Offset: 0x00061BF6
		protected void SetAutoDetectSettings(bool value)
		{
			this.m_Result.automaticallyDetectSettings = value;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00063A04 File Offset: 0x00061C04
		private static Uri ParseProxyUri(string proxyString)
		{
			if (proxyString.IndexOf("://") == -1)
			{
				proxyString = "http://" + proxyString;
			}
			Uri result;
			try
			{
				result = new Uri(proxyString);
			}
			catch (UriFormatException ex)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, ex.Message);
				}
				throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyString);
			}
			return result;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00063A68 File Offset: 0x00061C68
		private static Hashtable ParseProtocolProxies(string proxyListString)
		{
			string[] array = proxyListString.Split(new char[]
			{
				';'
			});
			Hashtable hashtable = new Hashtable(CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (!(text == string.Empty))
				{
					string[] array2 = text.Split(new char[]
					{
						'='
					});
					if (array2.Length != 2)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					array2[0] = array2[0].Trim();
					array2[1] = array2[1].Trim();
					if (array2[0] == string.Empty || array2[1] == string.Empty)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					hashtable[array2[0]] = WebProxyDataBuilder.ParseProxyUri(array2[1]);
				}
			}
			return hashtable;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00063B34 File Offset: 0x00061D34
		private static FormatException CreateInvalidProxyStringException(string originalProxyString)
		{
			string @string = SR.GetString("net_proxy_invalid_url_format", new object[]
			{
				originalProxyString
			});
			if (Logging.On)
			{
				Logging.PrintError(Logging.Web, @string);
			}
			return new FormatException(@string);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00063B70 File Offset: 0x00061D70
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

		// Token: 0x060012DC RID: 4828 RVA: 0x00063C58 File Offset: 0x00061E58
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

		// Token: 0x060012DD RID: 4829 RVA: 0x00063CC8 File Offset: 0x00061EC8
		private static ArrayList ParseBypassList(string bypassListString, out bool bypassOnLocal)
		{
			string[] array = bypassListString.Split(new char[]
			{
				';'
			});
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

		// Token: 0x04001526 RID: 5414
		private const char addressListDelimiter = ';';

		// Token: 0x04001527 RID: 5415
		private const char addressListSchemeValueDelimiter = '=';

		// Token: 0x04001528 RID: 5416
		private const char bypassListDelimiter = ';';

		// Token: 0x04001529 RID: 5417
		private WebProxyData m_Result;

		// Token: 0x0400152A RID: 5418
		private const string regexReserved = "#$()+.?[\\^{|";
	}
}
