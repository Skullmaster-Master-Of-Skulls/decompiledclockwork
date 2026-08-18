using System;

namespace TechnoPro.Common.Public.Entities.Surveys
{
	// Token: 0x0200017D RID: 381
	public class SurveyStatusTypeAttribute : Attribute
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public SurveyStatusTypeAttribute()
		{
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00012B1F File Offset: 0x00010D1F
		public SurveyStatusTypeAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00012B31 File Offset: 0x00010D31
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00012B39 File Offset: 0x00010D39
		public string Title { get; set; }
	}
}
