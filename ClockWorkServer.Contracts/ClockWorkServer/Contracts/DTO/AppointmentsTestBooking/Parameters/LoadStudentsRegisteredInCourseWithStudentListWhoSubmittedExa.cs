using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A57 RID: 2647
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq : BaseMessageReq
	{
		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x0001B084 File Offset: 0x00019284
		// (set) Token: 0x06003799 RID: 14233 RVA: 0x0001B08C File Offset: 0x0001928C
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
