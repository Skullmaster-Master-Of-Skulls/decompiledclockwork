using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A94 RID: 2708
	[DataContract(Namespace = "http://tpro.ca")]
	public class AccommodationDTO
	{
		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x060038F1 RID: 14577 RVA: 0x0001BA40 File Offset: 0x00019C40
		// (set) Token: 0x060038F2 RID: 14578 RVA: 0x0001BA48 File Offset: 0x00019C48
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x0001BA51 File Offset: 0x00019C51
		// (set) Token: 0x060038F4 RID: 14580 RVA: 0x0001BA59 File Offset: 0x00019C59
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x0001BA62 File Offset: 0x00019C62
		// (set) Token: 0x060038F6 RID: 14582 RVA: 0x0001BA6A File Offset: 0x00019C6A
		[DataMember]
		public string LookupText { get; set; }

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x0001BA73 File Offset: 0x00019C73
		// (set) Token: 0x060038F8 RID: 14584 RVA: 0x0001BA7B File Offset: 0x00019C7B
		[DataMember]
		public int Level { get; set; }

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x060038F9 RID: 14585 RVA: 0x0001BA84 File Offset: 0x00019C84
		// (set) Token: 0x060038FA RID: 14586 RVA: 0x0001BA8C File Offset: 0x00019C8C
		[DataMember]
		public string SubText { get; set; }
	}
}
