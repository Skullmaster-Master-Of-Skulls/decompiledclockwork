using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x0200092C RID: 2348
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentRoomDTO : ICloneable<AppointmentRoomDTO>, ICloneable
	{
		// Token: 0x06002FB7 RID: 12215 RVA: 0x000036BD File Offset: 0x000018BD
		public AppointmentRoomDTO()
		{
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00016D2C File Offset: 0x00014F2C
		public AppointmentRoomDTO(AppointmentRoomDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.RoomId = item.RoomId;
				this.RoomUniqueId = item.RoomUniqueId;
				this.RoomTitle = item.RoomTitle;
				this.RoomDescription = item.RoomDescription;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x00016D7F File Offset: 0x00014F7F
		// (set) Token: 0x06002FBA RID: 12218 RVA: 0x00016D87 File Offset: 0x00014F87
		[DataMember]
		public int RoomId { get; set; }

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x00016D90 File Offset: 0x00014F90
		// (set) Token: 0x06002FBC RID: 12220 RVA: 0x00016D98 File Offset: 0x00014F98
		[DataMember]
		public string RoomUniqueId { get; set; }

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x00016DA1 File Offset: 0x00014FA1
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x00016DA9 File Offset: 0x00014FA9
		[DataMember]
		public string RoomTitle { get; set; }

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x00016DB2 File Offset: 0x00014FB2
		// (set) Token: 0x06002FC0 RID: 12224 RVA: 0x00016DBA File Offset: 0x00014FBA
		[DataMember]
		public string RoomDescription { get; set; }

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x00016DC3 File Offset: 0x00014FC3
		// (set) Token: 0x06002FC2 RID: 12226 RVA: 0x00016DCB File Offset: 0x00014FCB
		[DataMember]
		public string RoomInfo { get; set; }

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00016DD4 File Offset: 0x00014FD4
		public AppointmentRoomDTO Clone()
		{
			return new AppointmentRoomDTO(this);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00016DEC File Offset: 0x00014FEC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
