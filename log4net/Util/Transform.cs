using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace log4net.Util
{
	// Token: 0x0200011D RID: 285
	public sealed class Transform
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x00019C75 File Offset: 0x00017E75
		private Transform()
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00019C80 File Offset: 0x00017E80
		public static void WriteEscapedXmlString(XmlWriter writer, string textData, string invalidCharReplacement)
		{
			string text = Transform.MaskXmlInvalidCharacters(textData, invalidCharReplacement);
			int num = 12 * (1 + Transform.CountSubstrings(text, "]]>"));
			int num2 = 3 * (Transform.CountSubstrings(text, "<") + Transform.CountSubstrings(text, ">")) + 4 * Transform.CountSubstrings(text, "&");
			if (num2 <= num)
			{
				writer.WriteString(text);
				return;
			}
			int i = text.IndexOf("]]>");
			if (i < 0)
			{
				writer.WriteCData(text);
				return;
			}
			int num3 = 0;
			while (i > -1)
			{
				writer.WriteCData(text.Substring(num3, i - num3));
				if (i == text.Length - 3)
				{
					num3 = text.Length;
					writer.WriteString("]]>");
					break;
				}
				writer.WriteString("]]");
				num3 = i + 2;
				i = text.IndexOf("]]>", num3);
			}
			if (num3 < text.Length)
			{
				writer.WriteCData(text.Substring(num3));
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00019D63 File Offset: 0x00017F63
		public static string MaskXmlInvalidCharacters(string textData, string mask)
		{
			return Transform.INVALIDCHARS.Replace(textData, mask);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00019D74 File Offset: 0x00017F74
		private static int CountSubstrings(string text, string substring)
		{
			int num = 0;
			int i = 0;
			int length = text.Length;
			int length2 = substring.Length;
			if (length == 0)
			{
				return 0;
			}
			if (length2 == 0)
			{
				return 0;
			}
			while (i < length)
			{
				int num2 = text.IndexOf(substring, i);
				if (num2 == -1)
				{
					break;
				}
				num++;
				i = num2 + length2;
			}
			return num;
		}

		// Token: 0x04000307 RID: 775
		private const string CDATA_END = "]]>";

		// Token: 0x04000308 RID: 776
		private const string CDATA_UNESCAPABLE_TOKEN = "]]";

		// Token: 0x04000309 RID: 777
		private static Regex INVALIDCHARS = new Regex("[^\\x09\\x0A\\x0D\\x20-\\uD7FF\\uE000-\\uFFFD]", RegexOptions.Compiled);
	}
}
