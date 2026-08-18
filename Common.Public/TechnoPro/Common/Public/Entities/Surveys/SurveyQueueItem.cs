using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Surveys
{
	// Token: 0x02000180 RID: 384
	public class SurveyQueueItem : BusinessBase<int>
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00012D74 File Offset: 0x00010F74
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PeopleSurveyId
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00012D8C File Offset: 0x00010F8C
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x00012D94 File Offset: 0x00010F94
		public BasicPerson Student { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00012D9D File Offset: 0x00010F9D
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x00012DA5 File Offset: 0x00010FA5
		public BasicPerson AssignedCounsellor { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00012DAE File Offset: 0x00010FAE
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00012DB6 File Offset: 0x00010FB6
		public SurveyForDisplay Survey { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00012DBF File Offset: 0x00010FBF
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x00012DC7 File Offset: 0x00010FC7
		public DateTime DateEntered { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00012DD0 File Offset: 0x00010FD0
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00012DD8 File Offset: 0x00010FD8
		public SurveyStatus Status { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00012DE1 File Offset: 0x00010FE1
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00012DE9 File Offset: 0x00010FE9
		public string StudentEmail { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00012DF2 File Offset: 0x00010FF2
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00012DFA File Offset: 0x00010FFA
		public string StaffNote { get; set; }
	}
}
