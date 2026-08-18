using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.OnlineForms
{
	// Token: 0x02000278 RID: 632
	public class OnlineFormQueueItem : BusinessBase<int>
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00019364 File Offset: 0x00017564
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PeopleOnlineFormId
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

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0001937C File Offset: 0x0001757C
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x00019384 File Offset: 0x00017584
		public BasicPerson Student { get; set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0001938D File Offset: 0x0001758D
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x00019395 File Offset: 0x00017595
		public BasicPerson AssignedCounsellor { get; set; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0001939E File Offset: 0x0001759E
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x000193A6 File Offset: 0x000175A6
		public OnlineFormForDisplay OnlineForm { get; set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x000193AF File Offset: 0x000175AF
		// (set) Token: 0x06001316 RID: 4886 RVA: 0x000193B7 File Offset: 0x000175B7
		public DateTime DateEntered { get; set; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x000193C0 File Offset: 0x000175C0
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x000193C8 File Offset: 0x000175C8
		public OnlineFormStatus Status { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x000193D1 File Offset: 0x000175D1
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x000193D9 File Offset: 0x000175D9
		public string StudentEmail { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x000193E2 File Offset: 0x000175E2
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x000193EA File Offset: 0x000175EA
		public string StaffNote { get; set; }
	}
}
