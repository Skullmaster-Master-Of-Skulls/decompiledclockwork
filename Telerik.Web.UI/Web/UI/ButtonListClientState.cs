using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020000AC RID: 172
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class ButtonListClientState
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001AC66 File Offset: 0x00018E66
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001AC6E File Offset: 0x00018E6E
		public bool Visible { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0001AC77 File Offset: 0x00018E77
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0001AC7F File Offset: 0x00018E7F
		public bool Enabled { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001AC88 File Offset: 0x00018E88
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001AC90 File Offset: 0x00018E90
		public int SelectedIndex { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001AC99 File Offset: 0x00018E99
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001ACA1 File Offset: 0x00018EA1
		public string ToolTip { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001ACAA File Offset: 0x00018EAA
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001ACB2 File Offset: 0x00018EB2
		public Unit Height { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001ACBB File Offset: 0x00018EBB
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0001ACC3 File Offset: 0x00018EC3
		public Unit Width { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001ACCC File Offset: 0x00018ECC
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		public string ValidationGroup { get; set; }
	}
}
