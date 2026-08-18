using System;

namespace TechnoPro.Common.UI.Web.Entity.Instructor.FinalExamRequest
{
	// Token: 0x02000038 RID: 56
	public class FinalExamDay
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000033CE File Offset: 0x000015CE
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000033D6 File Offset: 0x000015D6
		public int Level { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000033DF File Offset: 0x000015DF
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000033E7 File Offset: 0x000015E7
		public DateTime Date { get; set; }

		// Token: 0x06000152 RID: 338 RVA: 0x00002221 File Offset: 0x00000421
		public FinalExamDay()
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000033F0 File Offset: 0x000015F0
		public FinalExamDay(int level, DateTime date)
		{
			this.Level = level;
			this.Date = date;
		}
	}
}
