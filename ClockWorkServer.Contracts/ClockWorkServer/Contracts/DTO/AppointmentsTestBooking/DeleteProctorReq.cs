using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A02 RID: 2562
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProctorReq : BaseMessageReq
	{
		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x00019DBF File Offset: 0x00017FBF
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x00019DC7 File Offset: 0x00017FC7
		[DataMember]
		public int PersonId { get; set; }
	}
}
