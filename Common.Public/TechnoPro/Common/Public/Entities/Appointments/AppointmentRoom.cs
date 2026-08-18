using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004AE RID: 1198
	public class AppointmentRoom : BusinessBase<int>, ICloneable
	{
		// Token: 0x0600242B RID: 9259 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public AppointmentRoom()
		{
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000275C0 File Offset: 0x000257C0
		public AppointmentRoom(AppointmentRoom room)
		{
			this.RoomId = room.RoomId;
			this.RoomUniqueId = room.RoomUniqueId;
			this.RoomTitle = room.RoomTitle;
			this.RoomDescription = room.RoomDescription;
			this.RoomInfo = room.RoomInfo;
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x00027618 File Offset: 0x00025818
		// (set) Token: 0x0600242E RID: 9262 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int RoomId
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

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x00027630 File Offset: 0x00025830
		// (set) Token: 0x06002430 RID: 9264 RVA: 0x00027638 File Offset: 0x00025838
		public string RoomUniqueId { get; set; }

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x00027641 File Offset: 0x00025841
		// (set) Token: 0x06002432 RID: 9266 RVA: 0x00027649 File Offset: 0x00025849
		public string RoomTitle { get; set; }

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x00027652 File Offset: 0x00025852
		// (set) Token: 0x06002434 RID: 9268 RVA: 0x0002765A File Offset: 0x0002585A
		public string RoomDescription { get; set; }

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x00027663 File Offset: 0x00025863
		// (set) Token: 0x06002436 RID: 9270 RVA: 0x0002766B File Offset: 0x0002586B
		public string RoomInfo { get; set; }

		// Token: 0x06002437 RID: 9271 RVA: 0x00027674 File Offset: 0x00025874
		public AppointmentRoom Clone()
		{
			return new AppointmentRoom(this);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0002768C File Offset: 0x0002588C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
