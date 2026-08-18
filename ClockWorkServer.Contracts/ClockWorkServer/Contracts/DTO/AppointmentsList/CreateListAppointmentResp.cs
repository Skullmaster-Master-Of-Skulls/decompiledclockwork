using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AC4 RID: 2756
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateListAppointmentResp
	{
		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x0001C8BF File Offset: 0x0001AABF
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x0001C8C7 File Offset: 0x0001AAC7
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
