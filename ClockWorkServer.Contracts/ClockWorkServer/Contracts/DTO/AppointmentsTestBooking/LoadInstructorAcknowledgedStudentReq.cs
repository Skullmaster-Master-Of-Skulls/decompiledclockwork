using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A3F RID: 2623
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorAcknowledgedStudentReq : BaseMessageReq
	{
		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x0001A397 File Offset: 0x00018597
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x0001A39F File Offset: 0x0001859F
		[DataMember]
		public int AppId { get; set; }
	}
}
