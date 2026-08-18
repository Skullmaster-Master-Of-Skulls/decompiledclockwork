using System;

namespace System.Web.Mvc
{
	// Token: 0x020001B4 RID: 436
	public class SelectListItem
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00020A18 File Offset: 0x0001EC18
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x00020A20 File Offset: 0x0001EC20
		public bool Disabled { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00020A29 File Offset: 0x0001EC29
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x00020A31 File Offset: 0x0001EC31
		public SelectListGroup Group { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00020A3A File Offset: 0x0001EC3A
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x00020A42 File Offset: 0x0001EC42
		public bool Selected { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00020A4B File Offset: 0x0001EC4B
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00020A53 File Offset: 0x0001EC53
		public string Text { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00020A5C File Offset: 0x0001EC5C
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00020A64 File Offset: 0x0001EC64
		public string Value { get; set; }
	}
}
