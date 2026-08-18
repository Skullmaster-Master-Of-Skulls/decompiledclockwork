using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A00 RID: 2560
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProctorByIdReq : BaseMessageReq
	{
		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x00019D9D File Offset: 0x00017F9D
		// (set) Token: 0x0600352C RID: 13612 RVA: 0x00019DA5 File Offset: 0x00017FA5
		[DataMember]
		public int PersonId { get; set; }
	}
}
