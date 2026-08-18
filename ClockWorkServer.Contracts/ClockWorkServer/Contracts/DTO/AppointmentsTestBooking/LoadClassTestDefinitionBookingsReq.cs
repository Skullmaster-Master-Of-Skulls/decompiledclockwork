using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A23 RID: 2595
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionBookingsReq : BaseMessageReq
	{
		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x0001A078 File Offset: 0x00018278
		// (set) Token: 0x060035A5 RID: 13733 RVA: 0x0001A080 File Offset: 0x00018280
		[DataMember]
		public int ExamId { get; set; }
	}
}
