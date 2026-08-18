using System;
using System.Xml;
using iTextSharp.text;

namespace a.c
{
	// Token: 0x02000239 RID: 569
	internal class m : j, w
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x000553A1 File Offset: 0x000543A1
		public m(s A_0) : base(A_0)
		{
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000553AA File Offset: 0x000543AA
		IElement w.b()
		{
			return null;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000553AD File Offset: 0x000543AD
		public XmlNode an(XmlNode A_0, u A_1)
		{
			base.a().b("Header");
			return A_0.NextSibling;
		}
	}
}
