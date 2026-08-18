using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002F5 RID: 757
	[DataContract(Namespace = "http://tpro.ca")]
	public class SeatDTO : AppointmentRoomDTO
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0000823E File Offset: 0x0000643E
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00008246 File Offset: 0x00006446
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0000824F File Offset: 0x0000644F
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00008257 File Offset: 0x00006457
		[DataMember]
		public int ParentSeatGroupId { get; set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00008260 File Offset: 0x00006460
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x00008268 File Offset: 0x00006468
		[DataMember]
		public IList<string> AssetIds { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00008271 File Offset: 0x00006471
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00008279 File Offset: 0x00006479
		[DataMember]
		public eTestExamSeatType SeatType { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00008282 File Offset: 0x00006482
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x0000828A File Offset: 0x0000648A
		[DataMember]
		public int OrderNum { get; set; }
	}
}
