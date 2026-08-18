using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A40 RID: 2624
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorAcknowledgedStudentResp
	{
		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x0001A3A8 File Offset: 0x000185A8
		// (set) Token: 0x06003622 RID: 13858 RVA: 0x0001A3B0 File Offset: 0x000185B0
		[DataMember]
		public InstructorAcknowledgedStudentDTO AcknowledgedInfo { get; set; }
	}
}
