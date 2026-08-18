using System;
using System.Xml.Serialization;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D82 RID: 3458
	internal class GenericXmlaProperty : IXmlaMethodProperty
	{
		// Token: 0x170028E7 RID: 10471
		// (get) Token: 0x060080ED RID: 33005 RVA: 0x001D797C File Offset: 0x001D5B7C
		// (set) Token: 0x060080EE RID: 33006 RVA: 0x001D7984 File Offset: 0x001D5B84
		public string Name { get; set; }

		// Token: 0x170028E8 RID: 10472
		// (get) Token: 0x060080EF RID: 33007 RVA: 0x001D798D File Offset: 0x001D5B8D
		// (set) Token: 0x060080F0 RID: 33008 RVA: 0x001D7995 File Offset: 0x001D5B95
		[XmlText]
		public object Value { get; set; }
	}
}
