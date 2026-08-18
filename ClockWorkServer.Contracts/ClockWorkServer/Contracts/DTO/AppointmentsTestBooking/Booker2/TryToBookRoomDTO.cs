using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NewBooker.Entities.AutoTestBooking.Booker2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2
{
	// Token: 0x02000A91 RID: 2705
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookRoomDTO
	{
		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x0001B9A7 File Offset: 0x00019BA7
		// (set) Token: 0x060038DD RID: 14557 RVA: 0x0001B9AF File Offset: 0x00019BAF
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		// (set) Token: 0x060038DF RID: 14559 RVA: 0x0001B9C0 File Offset: 0x00019BC0
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x0001B9C9 File Offset: 0x00019BC9
		// (set) Token: 0x060038E1 RID: 14561 RVA: 0x0001B9D1 File Offset: 0x00019BD1
		[DataMember]
		public eRoomType RoomType { get; set; }

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x0001B9DA File Offset: 0x00019BDA
		// (set) Token: 0x060038E3 RID: 14563 RVA: 0x0001B9E2 File Offset: 0x00019BE2
		[DataMember]
		public string[] Campuses { get; set; }

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x0001B9EB File Offset: 0x00019BEB
		// (set) Token: 0x060038E5 RID: 14565 RVA: 0x0001B9F3 File Offset: 0x00019BF3
		[DataMember]
		public IList<string> AssetsSupported { get; set; }

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x0001B9FC File Offset: 0x00019BFC
		// (set) Token: 0x060038E7 RID: 14567 RVA: 0x0001BA04 File Offset: 0x00019C04
		[DataMember]
		public int OrderNum { get; set; }
	}
}
