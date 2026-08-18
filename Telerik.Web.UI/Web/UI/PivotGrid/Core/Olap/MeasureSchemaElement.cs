using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D29 RID: 3369
	internal class MeasureSchemaElement : UniqueSchemaElement
	{
		// Token: 0x170027F8 RID: 10232
		// (get) Token: 0x06007D66 RID: 32102 RVA: 0x001CB9E5 File Offset: 0x001C9BE5
		// (set) Token: 0x06007D67 RID: 32103 RVA: 0x001CB9ED File Offset: 0x001C9BED
		public string GroupCaption { get; set; }

		// Token: 0x170027F9 RID: 10233
		// (get) Token: 0x06007D68 RID: 32104 RVA: 0x001CB9F6 File Offset: 0x001C9BF6
		// (set) Token: 0x06007D69 RID: 32105 RVA: 0x001CB9FE File Offset: 0x001C9BFE
		public string GroupName { get; set; }

		// Token: 0x170027FA RID: 10234
		// (get) Token: 0x06007D6A RID: 32106 RVA: 0x001CBA07 File Offset: 0x001C9C07
		// (set) Token: 0x06007D6B RID: 32107 RVA: 0x001CBA0F File Offset: 0x001C9C0F
		public string DisplayFolder { get; set; }

		// Token: 0x170027FB RID: 10235
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x001CBA18 File Offset: 0x001C9C18
		// (set) Token: 0x06007D6D RID: 32109 RVA: 0x001CBA20 File Offset: 0x001C9C20
		public int DataTypeNumber { get; set; }
	}
}
