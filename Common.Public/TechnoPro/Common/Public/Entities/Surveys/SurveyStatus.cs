using System;

namespace TechnoPro.Common.Public.Entities.Surveys
{
	// Token: 0x02000181 RID: 385
	public class SurveyStatus : BusinessBase<int>
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00012E04 File Offset: 0x00011004
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PeopleSurveyStatusId
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

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00012E1C File Offset: 0x0001101C
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00012E24 File Offset: 0x00011024
		public string Title { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00012E2D File Offset: 0x0001102D
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x00012E35 File Offset: 0x00011035
		public eSurveyStatusType StatusType { get; set; }
	}
}
