using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A5B RID: 2651
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateExamRequestReq : BaseMessageReq
	{
		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x0001B0D9 File Offset: 0x000192D9
		// (set) Token: 0x060037A7 RID: 14247 RVA: 0x0001B0E1 File Offset: 0x000192E1
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x0001B0EA File Offset: 0x000192EA
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x0001B0F2 File Offset: 0x000192F2
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
