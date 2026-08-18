using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004CC RID: 1228
	public class ClockWorkSyncAppType : BusinessBase<int>
	{
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00027FEC File Offset: 0x000261EC
		// (set) Token: 0x0600251D RID: 9501 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppTypeId
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

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00028004 File Offset: 0x00026204
		// (set) Token: 0x0600251F RID: 9503 RVA: 0x0002800C File Offset: 0x0002620C
		public string Description { get; set; }
	}
}
