using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C3 RID: 1219
	[Serializable]
	public class AppointmentIcon : BusinessBase<int>
	{
		// Token: 0x060024E1 RID: 9441 RVA: 0x00027D53 File Offset: 0x00025F53
		public AppointmentIcon()
		{
			this.Icon = new IconInfo();
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x00027D6C File Offset: 0x00025F6C
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentIconId
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

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x00027D84 File Offset: 0x00025F84
		// (set) Token: 0x060024E5 RID: 9445 RVA: 0x00027D8C File Offset: 0x00025F8C
		public IconInfo Icon { get; set; }

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x00027D95 File Offset: 0x00025F95
		// (set) Token: 0x060024E7 RID: 9447 RVA: 0x00027D9D File Offset: 0x00025F9D
		public DynamicFormBase Screen { get; set; }

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00027DA8 File Offset: 0x00025FA8
		public int IconNum
		{
			get
			{
				return (this.Icon == null) ? 0 : this.Icon.IconNum;
			}
		}
	}
}
