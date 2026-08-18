using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AC9 RID: 2761
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelListAppointmentReq : BaseMessageReq
	{
		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x0001C8F2 File Offset: 0x0001AAF2
		// (set) Token: 0x06003A92 RID: 14994 RVA: 0x0001C8FA File Offset: 0x0001AAFA
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
