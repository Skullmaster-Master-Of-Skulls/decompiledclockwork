using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ActionPlan
{
	// Token: 0x02000196 RID: 406
	public class ActionTask : BusinessBase<int>
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x0001367F File Offset: 0x0001187F
		public ActionTask()
		{
			this.Description = "";
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00013698 File Offset: 0x00011898
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000136B0 File Offset: 0x000118B0
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x000136B8 File Offset: 0x000118B8
		public eWhoResponsible WhoResponsible { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x000136C1 File Offset: 0x000118C1
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x000136C9 File Offset: 0x000118C9
		public PersonBase Student { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000136D2 File Offset: 0x000118D2
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x000136DA File Offset: 0x000118DA
		public DateTime DateAdded { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000136E3 File Offset: 0x000118E3
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000136EB File Offset: 0x000118EB
		public DateTime LastDateModified { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000136F4 File Offset: 0x000118F4
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x000136FC File Offset: 0x000118FC
		public PersonBase WhoAdded { get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00013705 File Offset: 0x00011905
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0001370D File Offset: 0x0001190D
		public PersonBase WhoLastModified { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00013716 File Offset: 0x00011916
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0001371E File Offset: 0x0001191E
		public string Description { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00013727 File Offset: 0x00011927
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0001372F File Offset: 0x0001192F
		public string StaffNotes { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00013738 File Offset: 0x00011938
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00013740 File Offset: 0x00011940
		public string StudentNotes { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00013749 File Offset: 0x00011949
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00013751 File Offset: 0x00011951
		public int OrderNum { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001375A File Offset: 0x0001195A
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x00013762 File Offset: 0x00011962
		public ActionTaskCompletionStatus CompletionStatus { get; set; }
	}
}
