using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2
{
	// Token: 0x02000A8F RID: 2703
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookPotentialBookingDTO
	{
		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x0001B853 File Offset: 0x00019A53
		// (set) Token: 0x060038B7 RID: 14519 RVA: 0x0001B85B File Offset: 0x00019A5B
		[DataMember]
		public TryToBookRoomDTO Room { get; set; }

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060038B8 RID: 14520 RVA: 0x0001B864 File Offset: 0x00019A64
		// (set) Token: 0x060038B9 RID: 14521 RVA: 0x0001B86C File Offset: 0x00019A6C
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x0001B875 File Offset: 0x00019A75
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x0001B87D File Offset: 0x00019A7D
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x0001B886 File Offset: 0x00019A86
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x0001B88E File Offset: 0x00019A8E
		[DataMember]
		public IList<string> Notices { get; set; }
	}
}
