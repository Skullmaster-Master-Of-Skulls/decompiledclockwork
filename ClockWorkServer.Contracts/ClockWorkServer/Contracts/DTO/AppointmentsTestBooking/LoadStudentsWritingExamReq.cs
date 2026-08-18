using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A3D RID: 2621
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsWritingExamReq : BaseMessageReq
	{
		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x0001A375 File Offset: 0x00018575
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x0001A37D File Offset: 0x0001857D
		[DataMember]
		public int ExamId { get; set; }
	}
}
