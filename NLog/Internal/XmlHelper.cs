using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace NLog.Internal
{
	// Token: 0x020000BE RID: 190
	public static class XmlHelper
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		private static string RemoveInvalidXmlChars(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				return XmlHelper.InvalidXmlChars.Replace(text, "");
			}
			return "";
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000C810 File Offset: 0x0000AA10
		public static void WriteAttributeSafeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
		{
			writer.WriteAttributeString(XmlHelper.RemoveInvalidXmlChars(prefix), XmlHelper.RemoveInvalidXmlChars(localName), XmlHelper.RemoveInvalidXmlChars(ns), XmlHelper.RemoveInvalidXmlChars(value));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000C831 File Offset: 0x0000AA31
		public static void WriteAttributeSafeString(this XmlWriter writer, string thread, string localName)
		{
			writer.WriteAttributeString(XmlHelper.RemoveInvalidXmlChars(thread), XmlHelper.RemoveInvalidXmlChars(localName));
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000C845 File Offset: 0x0000AA45
		public static void WriteElementSafeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
		{
			writer.WriteElementString(XmlHelper.RemoveInvalidXmlChars(prefix), XmlHelper.RemoveInvalidXmlChars(localName), XmlHelper.RemoveInvalidXmlChars(ns), XmlHelper.RemoveInvalidXmlChars(value));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000C866 File Offset: 0x0000AA66
		public static void WriteSafeCData(this XmlWriter writer, string text)
		{
			writer.WriteCData(XmlHelper.RemoveInvalidXmlChars(text));
		}

		// Token: 0x04000145 RID: 325
		private static readonly Regex InvalidXmlChars = new Regex("(?<![\\uD800-\\uDBFF])[\\uDC00-\\uDFFF]|[\\uD800-\\uDBFF](?![\\uDC00-\\uDFFF])|[\\x00-\\x08\\x0B\\x0C\\x0E-\\x1F\\x7F-\\x9F\\uFEFF\\uFFFE\\uFFFF]", RegexOptions.Compiled);
	}
}
