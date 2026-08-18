using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200009C RID: 156
	internal static class XmlUtil
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000FD94 File Offset: 0x0000DF94
		public static string GetXmlLangAttribute(XmlReader reader)
		{
			string text = null;
			if (reader.MoveToAttribute("lang", "http://www.w3.org/XML/1998/namespace"))
			{
				text = reader.Value;
				reader.MoveToElement();
			}
			if (text == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("XmlLangAttributeMissing")));
			}
			return text;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000FDE1 File Offset: 0x0000DFE1
		public static bool IsTrue(string booleanValue)
		{
			return !string.IsNullOrEmpty(booleanValue) && XmlConvert.ToBoolean(booleanValue);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000FDF3 File Offset: 0x0000DFF3
		public static void ReadContentAsQName(XmlReader reader, out string localName, out string ns)
		{
			XmlUtil.ParseQName(reader, reader.ReadContentAsString(), out localName, out ns);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000FE03 File Offset: 0x0000E003
		public static bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n';
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000FE1C File Offset: 0x0000E01C
		public static string TrimEnd(string s)
		{
			int num = s.Length;
			while (num > 0 && XmlUtil.IsWhitespace(s[num - 1]))
			{
				num--;
			}
			if (num != s.Length)
			{
				return s.Substring(0, num);
			}
			return s;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000FE60 File Offset: 0x0000E060
		public static string TrimStart(string s)
		{
			int num = 0;
			while (num < s.Length && XmlUtil.IsWhitespace(s[num]))
			{
				num++;
			}
			if (num != 0)
			{
				return s.Substring(num);
			}
			return s;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000FE98 File Offset: 0x0000E098
		public static string Trim(string s)
		{
			int num = 0;
			while (num < s.Length && XmlUtil.IsWhitespace(s[num]))
			{
				num++;
			}
			if (num >= s.Length)
			{
				return string.Empty;
			}
			int num2 = s.Length;
			while (num2 > 0 && XmlUtil.IsWhitespace(s[num2 - 1]))
			{
				num2--;
			}
			if (num != 0 || num2 != s.Length)
			{
				return s.Substring(num, num2 - num);
			}
			return s;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public static void ParseQName(XmlReader reader, string qname, out string localName, out string ns)
		{
			int num = qname.IndexOf(':');
			string prefix;
			if (num < 0)
			{
				prefix = "";
				localName = XmlUtil.TrimStart(XmlUtil.TrimEnd(qname));
			}
			else
			{
				if (num == qname.Length - 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidXmlQualifiedName", new object[]
					{
						qname
					})));
				}
				prefix = XmlUtil.TrimStart(qname.Substring(0, num));
				localName = XmlUtil.TrimEnd(qname.Substring(num + 1));
			}
			ns = reader.LookupNamespace(prefix);
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnboundPrefixInQName", new object[]
				{
					qname
				})));
			}
		}

		// Token: 0x04000911 RID: 2321
		public const string XmlNs = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000912 RID: 2322
		public const string XmlNsNs = "http://www.w3.org/2000/xmlns/";
	}
}
