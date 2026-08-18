using System;

namespace System.Xml
{
	// Token: 0x020000A7 RID: 167
	internal interface IDtdAttributeInfo
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005CE RID: 1486
		string Prefix { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005CF RID: 1487
		string LocalName { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005D0 RID: 1488
		int LineNumber { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005D1 RID: 1489
		int LinePosition { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005D2 RID: 1490
		bool IsNonCDataType { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005D3 RID: 1491
		bool IsDeclaredInExternal { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005D4 RID: 1492
		bool IsXmlAttribute { get; }
	}
}
