using System;

namespace TechnoPro.Common.Public.Entities.Room
{
	// Token: 0x0200020E RID: 526
	public class SeatGroup : BusinessBase<int>
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000173AC File Offset: 0x000155AC
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SeatGroupId
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

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000173C4 File Offset: 0x000155C4
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x000173CC File Offset: 0x000155CC
		public string Title { get; set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x000173D5 File Offset: 0x000155D5
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x000173DD File Offset: 0x000155DD
		public int PrimaryRoomPersonId { get; set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000173E6 File Offset: 0x000155E6
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000173EE File Offset: 0x000155EE
		public int ParentSeatGroupId { get; set; }
	}
}
