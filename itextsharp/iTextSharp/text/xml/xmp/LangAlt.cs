using System;
using System.Text;
using System.util;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x020002C6 RID: 710
	public class LangAlt : Properties
	{
		// Token: 0x06001A92 RID: 6802 RVA: 0x0009C69D File Offset: 0x0009B69D
		public LangAlt(string defaultValue)
		{
			this.AddLanguage("x-default", defaultValue);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0009C6B1 File Offset: 0x0009B6B1
		public LangAlt()
		{
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0009C6B9 File Offset: 0x0009B6B9
		public void AddLanguage(string language, string value)
		{
			this[language] = XmpSchema.Escape(value);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0009C6C8 File Offset: 0x0009B6C8
		protected internal void Process(StringBuilder buf, string lang)
		{
			buf.Append("<rdf:li xml:lang=\"");
			buf.Append(lang);
			buf.Append("\" >");
			buf.Append(this[lang]);
			buf.Append("</rdf:li>");
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0009C704 File Offset: 0x0009B704
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<rdf:Alt>");
			foreach (string lang in base.Keys)
			{
				this.Process(stringBuilder, lang);
			}
			stringBuilder.Append("</rdf:Alt>");
			return stringBuilder.ToString();
		}

		// Token: 0x040011BF RID: 4543
		public const string DEFAULT = "x-default";
	}
}
