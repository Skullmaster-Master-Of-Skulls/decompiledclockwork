using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;

namespace iTextSharp.text.error_messages
{
	// Token: 0x020001D2 RID: 466
	public class MessageLocalization
	{
		// Token: 0x0600121E RID: 4638 RVA: 0x00068231 File Offset: 0x00067231
		private MessageLocalization()
		{
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0006823C File Offset: 0x0006723C
		static MessageLocalization()
		{
			try
			{
				MessageLocalization.defaultLanguage = MessageLocalization.GetLanguageMessages("en", null);
			}
			catch
			{
			}
			if (MessageLocalization.defaultLanguage == null)
			{
				MessageLocalization.defaultLanguage = new Dictionary<string, string>();
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0006828C File Offset: 0x0006728C
		public static string GetMessage(string key)
		{
			Dictionary<string, string> dictionary = MessageLocalization.currentLanguage;
			string text;
			if (dictionary != null)
			{
				dictionary.TryGetValue(key, out text);
				if (text != null)
				{
					return text;
				}
			}
			dictionary = MessageLocalization.defaultLanguage;
			dictionary.TryGetValue(key, out text);
			if (text != null)
			{
				return text;
			}
			return "No message found for " + key;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000682D1 File Offset: 0x000672D1
		public static string GetComposedMessage(string key)
		{
			return MessageLocalization.GetComposedMessage(key, null, null);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000682DB File Offset: 0x000672DB
		public static string GetComposedMessage(string key, object p1)
		{
			return MessageLocalization.GetComposedMessage(key, p1, null);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000682E5 File Offset: 0x000672E5
		public static string GetComposedMessage(string key, object p1, object p2)
		{
			return MessageLocalization.GetComposedMessage(key, p1, p2, null, null);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000682F1 File Offset: 0x000672F1
		public static string GetComposedMessage(string key, object p1, object p2, object p3)
		{
			return MessageLocalization.GetComposedMessage(key, p1, p2, p3, null);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00068300 File Offset: 0x00067300
		public static string GetComposedMessage(string key, object p1, object p2, object p3, object p4)
		{
			string text = MessageLocalization.GetMessage(key);
			if (p1 != null)
			{
				text = text.Replace("{1}", p1.ToString());
			}
			if (p2 != null)
			{
				text = text.Replace("{2}", p2.ToString());
			}
			if (p3 != null)
			{
				text = text.Replace("{3}", p3.ToString());
			}
			if (p4 != null)
			{
				text = text.Replace("{4}", p4.ToString());
			}
			return text;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0006836C File Offset: 0x0006736C
		public static bool SetLanguage(string language, string country)
		{
			Dictionary<string, string> languageMessages = MessageLocalization.GetLanguageMessages(language, country);
			if (languageMessages == null)
			{
				return false;
			}
			MessageLocalization.currentLanguage = languageMessages;
			return true;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0006838D File Offset: 0x0006738D
		public static void SetMessages(TextReader r)
		{
			MessageLocalization.currentLanguage = MessageLocalization.ReadLanguageStream(r);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0006839C File Offset: 0x0006739C
		private static Dictionary<string, string> GetLanguageMessages(string language, string country)
		{
			if (language == null)
			{
				throw new ArgumentException("The language cannot be null.");
			}
			Stream stream = null;
			Dictionary<string, string> result;
			try
			{
				string str;
				if (country != null)
				{
					str = language + "_" + country + ".lng";
				}
				else
				{
					str = language + ".lng";
				}
				stream = BaseFont.GetResourceStream("iTextSharp.text.error_messages." + str);
				if (stream != null)
				{
					result = MessageLocalization.ReadLanguageStream(stream);
				}
				else if (country == null)
				{
					result = null;
				}
				else
				{
					str = language + ".lng";
					stream = BaseFont.GetResourceStream("iTextSharp.text.error_messages." + str);
					if (stream != null)
					{
						result = MessageLocalization.ReadLanguageStream(stream);
					}
					else
					{
						result = null;
					}
				}
			}
			finally
			{
				try
				{
					stream.Close();
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00068458 File Offset: 0x00067458
		private static Dictionary<string, string> ReadLanguageStream(Stream isp)
		{
			return MessageLocalization.ReadLanguageStream(new StreamReader(isp, Encoding.UTF8));
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0006846C File Offset: 0x0006746C
		private static Dictionary<string, string> ReadLanguageStream(TextReader br)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string text;
			while ((text = br.ReadLine()) != null)
			{
				int num = text.IndexOf('=');
				if (num >= 0)
				{
					string text2 = text.Substring(0, num).Trim();
					if (!text2.StartsWith("#"))
					{
						dictionary[text2] = text.Substring(num + 1);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04000CC3 RID: 3267
		private const string BASE_PATH = "iTextSharp.text.error_messages.";

		// Token: 0x04000CC4 RID: 3268
		private static Dictionary<string, string> defaultLanguage = new Dictionary<string, string>();

		// Token: 0x04000CC5 RID: 3269
		private static Dictionary<string, string> currentLanguage;
	}
}
