using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A98 RID: 2712
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoBookTestExamPreviewResultDTO
	{
		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x0001BB3F File Offset: 0x00019D3F
		// (set) Token: 0x06003914 RID: 14612 RVA: 0x0001BB47 File Offset: 0x00019D47
		[DataMember]
		public bool Succeeded { get; set; }

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x0001BB50 File Offset: 0x00019D50
		// (set) Token: 0x06003916 RID: 14614 RVA: 0x0001BB58 File Offset: 0x00019D58
		[DataMember]
		public IList<TryToBookFailureDTO> Failures { get; set; }

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x0001BB61 File Offset: 0x00019D61
		// (set) Token: 0x06003918 RID: 14616 RVA: 0x0001BB69 File Offset: 0x00019D69
		[DataMember]
		public DateTime? PotentialStartDateTime { get; set; }

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x0001BB72 File Offset: 0x00019D72
		// (set) Token: 0x0600391A RID: 14618 RVA: 0x0001BB7A File Offset: 0x00019D7A
		[DataMember]
		public DateTime? PotentialEndDateTime { get; set; }

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x0001BB83 File Offset: 0x00019D83
		// (set) Token: 0x0600391C RID: 14620 RVA: 0x0001BB8B File Offset: 0x00019D8B
		[DataMember]
		public BasicPersonDTO Student { get; set; }

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x0001BB94 File Offset: 0x00019D94
		// (set) Token: 0x0600391E RID: 14622 RVA: 0x0001BB9C File Offset: 0x00019D9C
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x0001BBA5 File Offset: 0x00019DA5
		// (set) Token: 0x06003920 RID: 14624 RVA: 0x0001BBAD File Offset: 0x00019DAD
		[DataMember]
		public AppointmentRoomDTO PotentialRoom { get; set; }

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x0001BBB6 File Offset: 0x00019DB6
		// (set) Token: 0x06003922 RID: 14626 RVA: 0x0001BBBE File Offset: 0x00019DBE
		[DataMember]
		public IList<int> AccommodationCids { get; set; }
	}
}
