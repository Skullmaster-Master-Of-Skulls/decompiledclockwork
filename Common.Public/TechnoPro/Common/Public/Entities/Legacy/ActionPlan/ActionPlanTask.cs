using System;

namespace TechnoPro.Common.Public.Entities.Legacy.ActionPlan
{
	// Token: 0x020002FF RID: 767
	public class ActionPlanTask : BusinessBase<int>
	{
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0001C614 File Offset: 0x0001A814
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TaskId
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

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0001C62C File Offset: 0x0001A82C
		// (set) Token: 0x06001775 RID: 6005 RVA: 0x0001C634 File Offset: 0x0001A834
		public int PersonId { get; set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0001C63D File Offset: 0x0001A83D
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x0001C645 File Offset: 0x0001A845
		public int WhoResponsibleCode { get; set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0001C64E File Offset: 0x0001A84E
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x0001C656 File Offset: 0x0001A856
		public DateTime? DateLastModified { get; set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0001C65F File Offset: 0x0001A85F
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x0001C667 File Offset: 0x0001A867
		public int WhoAdded { get; set; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0001C670 File Offset: 0x0001A870
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x0001C678 File Offset: 0x0001A878
		public int WhoLastModified { get; set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0001C681 File Offset: 0x0001A881
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x0001C689 File Offset: 0x0001A889
		public string Group { get; set; }

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0001C692 File Offset: 0x0001A892
		// (set) Token: 0x06001781 RID: 6017 RVA: 0x0001C69A File Offset: 0x0001A89A
		public string Description { get; set; }

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0001C6A3 File Offset: 0x0001A8A3
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x0001C6AB File Offset: 0x0001A8AB
		public int? CompletedId { get; set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		public string Completed { get; set; }

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0001C6C5 File Offset: 0x0001A8C5
		// (set) Token: 0x06001787 RID: 6023 RVA: 0x0001C6CD File Offset: 0x0001A8CD
		public bool MeansComplete { get; set; }

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0001C6D6 File Offset: 0x0001A8D6
		// (set) Token: 0x06001789 RID: 6025 RVA: 0x0001C6DE File Offset: 0x0001A8DE
		public string StaffNotes { get; set; }

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0001C6E7 File Offset: 0x0001A8E7
		// (set) Token: 0x0600178B RID: 6027 RVA: 0x0001C6EF File Offset: 0x0001A8EF
		public string StudentNotes { get; set; }

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		// (set) Token: 0x0600178D RID: 6029 RVA: 0x0001C700 File Offset: 0x0001A900
		public int OrderNum { get; set; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0001C709 File Offset: 0x0001A909
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x0001C711 File Offset: 0x0001A911
		public string FirstName { get; set; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0001C71A File Offset: 0x0001A91A
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0001C722 File Offset: 0x0001A922
		public string LastName { get; set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0001C72B File Offset: 0x0001A92B
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0001C733 File Offset: 0x0001A933
		public DateTime DateAdded { get; set; }
	}
}
