using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A59 RID: 2649
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsByCourseReq : BaseMessageReq
	{
		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x0001B0B7 File Offset: 0x000192B7
		// (set) Token: 0x060037A1 RID: 14241 RVA: 0x0001B0BF File Offset: 0x000192BF
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
