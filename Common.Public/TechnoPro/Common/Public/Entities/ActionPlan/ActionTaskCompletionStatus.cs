using System;

namespace TechnoPro.Common.Public.Entities.ActionPlan
{
	// Token: 0x02000197 RID: 407
	public class ActionTaskCompletionStatus : BusinessBase<int>
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x0001376B File Offset: 0x0001196B
		public ActionTaskCompletionStatus()
		{
			this.Title = "";
			this.Description = "";
			this.IsActive = true;
			this.IsDefault = false;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x000137A0 File Offset: 0x000119A0
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int CompletedId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x000137B8 File Offset: 0x000119B8
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x000137C0 File Offset: 0x000119C0
		public string Title { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x000137C9 File Offset: 0x000119C9
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x000137D1 File Offset: 0x000119D1
		public string Description { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x000137DA File Offset: 0x000119DA
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x000137E2 File Offset: 0x000119E2
		public bool MeansComplete { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000137EB File Offset: 0x000119EB
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x000137F3 File Offset: 0x000119F3
		public int? ColourArgB { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000137FC File Offset: 0x000119FC
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00013804 File Offset: 0x00011A04
		public int ImageIndex { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0001380D File Offset: 0x00011A0D
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00013815 File Offset: 0x00011A15
		public bool IsActive { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001381E File Offset: 0x00011A1E
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x00013826 File Offset: 0x00011A26
		public bool IsDefault { get; set; }
	}
}
