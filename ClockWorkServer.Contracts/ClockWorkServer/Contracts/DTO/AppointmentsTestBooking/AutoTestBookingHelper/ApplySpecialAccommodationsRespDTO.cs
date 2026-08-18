using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A96 RID: 2710
	[DataContract(Namespace = "http://tpro.ca")]
	public class ApplySpecialAccommodationsRespDTO
	{
		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x0001BAA6 File Offset: 0x00019CA6
		// (set) Token: 0x06003900 RID: 14592 RVA: 0x0001BAAE File Offset: 0x00019CAE
		[DataMember]
		public IList<PrivateNoteDTO> PrivateNotes { get; set; }

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x0001BAB7 File Offset: 0x00019CB7
		// (set) Token: 0x06003902 RID: 14594 RVA: 0x0001BABF File Offset: 0x00019CBF
		[DataMember]
		public string EmailBodySb { get; set; }

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		// (set) Token: 0x06003904 RID: 14596 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		[DataMember]
		public IList<int> IconsToBookWith { get; set; }

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x0001BAD9 File Offset: 0x00019CD9
		// (set) Token: 0x06003906 RID: 14598 RVA: 0x0001BAE1 File Offset: 0x00019CE1
		[DataMember]
		public TestDTO NewTestScheduledTimeAndRoom { get; set; }
	}
}
