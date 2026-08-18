using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D25 RID: 3365
	internal class KpiSchemaElement : SchemaElement
	{
		// Token: 0x170027EF RID: 10223
		// (get) Token: 0x06007D47 RID: 32071 RVA: 0x001CB7B9 File Offset: 0x001C99B9
		// (set) Token: 0x06007D48 RID: 32072 RVA: 0x001CB7C1 File Offset: 0x001C99C1
		public string DisplayFolder { get; set; }

		// Token: 0x170027F0 RID: 10224
		// (get) Token: 0x06007D49 RID: 32073 RVA: 0x001CB7CA File Offset: 0x001C99CA
		// (set) Token: 0x06007D4A RID: 32074 RVA: 0x001CB7D2 File Offset: 0x001C99D2
		public string ValueMemberUniqueName { get; set; }

		// Token: 0x170027F1 RID: 10225
		// (get) Token: 0x06007D4B RID: 32075 RVA: 0x001CB7DB File Offset: 0x001C99DB
		// (set) Token: 0x06007D4C RID: 32076 RVA: 0x001CB7E3 File Offset: 0x001C99E3
		public string GoalMemberUniqueName { get; set; }

		// Token: 0x170027F2 RID: 10226
		// (get) Token: 0x06007D4D RID: 32077 RVA: 0x001CB7EC File Offset: 0x001C99EC
		// (set) Token: 0x06007D4E RID: 32078 RVA: 0x001CB7F4 File Offset: 0x001C99F4
		public string StatusMemberUniqueName { get; set; }

		// Token: 0x170027F3 RID: 10227
		// (get) Token: 0x06007D4F RID: 32079 RVA: 0x001CB7FD File Offset: 0x001C99FD
		// (set) Token: 0x06007D50 RID: 32080 RVA: 0x001CB805 File Offset: 0x001C9A05
		public string TrendMemberUniqueName { get; set; }

		// Token: 0x170027F4 RID: 10228
		// (get) Token: 0x06007D51 RID: 32081 RVA: 0x001CB80E File Offset: 0x001C9A0E
		// (set) Token: 0x06007D52 RID: 32082 RVA: 0x001CB816 File Offset: 0x001C9A16
		public string StatusGraphic { get; set; }

		// Token: 0x170027F5 RID: 10229
		// (get) Token: 0x06007D53 RID: 32083 RVA: 0x001CB81F File Offset: 0x001C9A1F
		// (set) Token: 0x06007D54 RID: 32084 RVA: 0x001CB827 File Offset: 0x001C9A27
		public string TrendGraphic { get; set; }
	}
}
