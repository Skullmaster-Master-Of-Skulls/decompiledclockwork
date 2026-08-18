using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A16 RID: 2582
	public class RemoveInstructorHasSubmittedInformationAboutThisTestMarkerReq : BaseMessageReq
	{
		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x00019F9B File Offset: 0x0001819B
		// (set) Token: 0x0600357E RID: 13694 RVA: 0x00019FA3 File Offset: 0x000181A3
		[DataMember]
		public int ExamId { get; set; }
	}
}
