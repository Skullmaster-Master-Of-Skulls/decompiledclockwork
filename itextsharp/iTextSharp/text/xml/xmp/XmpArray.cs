using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x02000632 RID: 1586
	public class XmpArray : List<string>
	{
		// Token: 0x060035A8 RID: 13736 RVA: 0x0014C42C File Offset: 0x0014B42C
		public XmpArray(string type)
		{
			this.type = type;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0014C43C File Offset: 0x0014B43C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("<");
			stringBuilder.Append(this.type);
			stringBuilder.Append('>');
			foreach (string content in this)
			{
				stringBuilder.Append("<rdf:li>");
				stringBuilder.Append(XmpSchema.Escape(content));
				stringBuilder.Append("</rdf:li>");
			}
			stringBuilder.Append("</");
			stringBuilder.Append(this.type);
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x040023F9 RID: 9209
		public const string UNORDERED = "rdf:Bag";

		// Token: 0x040023FA RID: 9210
		public const string ORDERED = "rdf:Seq";

		// Token: 0x040023FB RID: 9211
		public const string ALTERNATIVE = "rdf:Alt";

		// Token: 0x040023FC RID: 9212
		protected string type;
	}
}
