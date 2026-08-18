using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004BB RID: 1211
	public class Holiday : BusinessBase<int>
	{
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x00027AEC File Offset: 0x00025CEC
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int HolidayId
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

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x00027B04 File Offset: 0x00025D04
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x00027B0C File Offset: 0x00025D0C
		public DateTime Date { get; set; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x00027B15 File Offset: 0x00025D15
		// (set) Token: 0x0600249D RID: 9373 RVA: 0x00027B1D File Offset: 0x00025D1D
		public string Title { get; set; }

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x00027B26 File Offset: 0x00025D26
		// (set) Token: 0x0600249F RID: 9375 RVA: 0x00027B2E File Offset: 0x00025D2E
		public string Description { get; set; }
	}
}
