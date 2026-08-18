using System;
using System.Collections;
using System.IO;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000520 RID: 1312
	public class XmlBuilder : ControlBuilder
	{
		// Token: 0x0600425A RID: 16986 RVA: 0x00006164 File Offset: 0x00004364
		public override void AppendLiteralString(string s)
		{
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x0000298D File Offset: 0x00000B8D
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			return null;
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool NeedsTagInnerText()
		{
			return true;
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000D8A3C File Offset: 0x000D6C3C
		public override void SetTagInnerText(string text)
		{
			if (!Util.IsWhiteSpaceString(text))
			{
				int num = Util.FirstNonWhiteSpaceIndex(text);
				string s = text.Substring(num);
				base.Line += Util.LineCount(text, 0, num);
				XmlDocument xmlDocument = new XmlDocument();
				XmlReaderSettings xmlReaderSettings = XmlUtils.CreateXmlReaderSettings();
				xmlReaderSettings.LineNumberOffset = base.Line - 1;
				xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
				xmlReaderSettings.CheckCharacters = false;
				XmlReader reader = XmlUtils.CreateXmlReader(new StringReader(s), string.Empty, xmlReaderSettings);
				try
				{
					xmlDocument.Load(reader);
				}
				catch (XmlException ex)
				{
					if (ex.LineNumber >= 0)
					{
						base.Line = ex.LineNumber;
					}
					throw;
				}
				base.AppendLiteralString(text);
			}
		}
	}
}
