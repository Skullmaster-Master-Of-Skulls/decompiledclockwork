using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C6 RID: 1222
	[Serializable]
	public class AppTypeGroup : BusinessBase<int>
	{
		// Token: 0x060024F9 RID: 9465 RVA: 0x00027E78 File Offset: 0x00026078
		public AppTypeGroup()
		{
			this.AppointmentTypeGroupId = 0;
			this.Description = "";
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x00027E98 File Offset: 0x00026098
		// (set) Token: 0x060024FB RID: 9467 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentTypeGroupId
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

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00027EB0 File Offset: 0x000260B0
		// (set) Token: 0x060024FD RID: 9469 RVA: 0x00027EB8 File Offset: 0x000260B8
		public string Description { get; set; }

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x00027EC1 File Offset: 0x000260C1
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x00027EC9 File Offset: 0x000260C9
		public int ClientGroupId { get; set; }
	}
}
