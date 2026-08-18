using System;

namespace TechnoPro.Common.Public.Entities.Surveys
{
	// Token: 0x0200017F RID: 383
	public class SurveyForDisplay : BusinessBase<int>
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00012D18 File Offset: 0x00010F18
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SurveyId
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

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00012D30 File Offset: 0x00010F30
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00012D38 File Offset: 0x00010F38
		public string Title { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00012D41 File Offset: 0x00010F41
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00012D49 File Offset: 0x00010F49
		public string Description { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00012D52 File Offset: 0x00010F52
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x00012D5A File Offset: 0x00010F5A
		public string ShortCode { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00012D63 File Offset: 0x00010F63
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00012D6B File Offset: 0x00010F6B
		public int ScreenNum { get; set; }
	}
}
