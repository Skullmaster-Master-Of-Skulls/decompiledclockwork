using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E06 RID: 3590
	[Serializable]
	internal class PivotGridModelCell : PivotGridModelCellBase
	{
		// Token: 0x17002A17 RID: 10775
		// (get) Token: 0x06008519 RID: 34073 RVA: 0x001E63B2 File Offset: 0x001E45B2
		// (set) Token: 0x0600851A RID: 34074 RVA: 0x001E63BA File Offset: 0x001E45BA
		public int GroupLevel { get; set; }

		// Token: 0x17002A18 RID: 10776
		// (get) Token: 0x0600851B RID: 34075 RVA: 0x001E63C3 File Offset: 0x001E45C3
		// (set) Token: 0x0600851C RID: 34076 RVA: 0x001E63CB File Offset: 0x001E45CB
		public int RowSpan { get; set; }

		// Token: 0x17002A19 RID: 10777
		// (get) Token: 0x0600851D RID: 34077 RVA: 0x001E63D4 File Offset: 0x001E45D4
		// (set) Token: 0x0600851E RID: 34078 RVA: 0x001E63DC File Offset: 0x001E45DC
		public int ColSpan { get; set; }

		// Token: 0x17002A1A RID: 10778
		// (get) Token: 0x0600851F RID: 34079 RVA: 0x001E63E5 File Offset: 0x001E45E5
		// (set) Token: 0x06008520 RID: 34080 RVA: 0x001E63ED File Offset: 0x001E45ED
		public int Slot { get; set; }

		// Token: 0x17002A1B RID: 10779
		// (get) Token: 0x06008521 RID: 34081 RVA: 0x001E63F6 File Offset: 0x001E45F6
		// (set) Token: 0x06008522 RID: 34082 RVA: 0x001E63FE File Offset: 0x001E45FE
		public bool ShouldCreateExpandCollapseButton { get; set; }

		// Token: 0x17002A1C RID: 10780
		// (get) Token: 0x06008523 RID: 34083 RVA: 0x001E6407 File Offset: 0x001E4607
		// (set) Token: 0x06008524 RID: 34084 RVA: 0x001E640F File Offset: 0x001E460F
		public bool IsCollapsed { get; set; }

		// Token: 0x17002A1D RID: 10781
		// (get) Token: 0x06008525 RID: 34085 RVA: 0x001E6418 File Offset: 0x001E4618
		// (set) Token: 0x06008526 RID: 34086 RVA: 0x001E6420 File Offset: 0x001E4620
		public bool HasChildren { get; set; }

		// Token: 0x17002A1E RID: 10782
		// (get) Token: 0x06008527 RID: 34087 RVA: 0x001E6429 File Offset: 0x001E4629
		// (set) Token: 0x06008528 RID: 34088 RVA: 0x001E6431 File Offset: 0x001E4631
		public bool IsTotalCell { get; set; }

		// Token: 0x17002A1F RID: 10783
		// (get) Token: 0x06008529 RID: 34089 RVA: 0x001E643A File Offset: 0x001E463A
		// (set) Token: 0x0600852A RID: 34090 RVA: 0x001E6442 File Offset: 0x001E4642
		public bool IsGrandTotalCell { get; set; }
	}
}
