using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C4 RID: 1220
	[Serializable]
	public class AppTypeBase : BusinessBase<int>
	{
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x00027DD0 File Offset: 0x00025FD0
		// (set) Token: 0x060024EA RID: 9450 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00027DE8 File Offset: 0x00025FE8
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x00027DF0 File Offset: 0x00025FF0
		public string Description { get; set; }
	}
}
