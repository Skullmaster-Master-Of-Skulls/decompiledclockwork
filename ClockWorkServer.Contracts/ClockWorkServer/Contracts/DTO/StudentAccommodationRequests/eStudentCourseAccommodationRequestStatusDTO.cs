using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000240 RID: 576
	[Flags]
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eStudentCourseAccommodationRequestStatusDTO
	{
		// Token: 0x0400039B RID: 923
		[EnumMember]
		Unknown = 0,
		// Token: 0x0400039C RID: 924
		[EnumMember]
		PendingWaitingForStaff = 1,
		// Token: 0x0400039D RID: 925
		[EnumMember]
		PendingWaitingForStudent = 2,
		// Token: 0x0400039E RID: 926
		[EnumMember]
		Denied = 4,
		// Token: 0x0400039F RID: 927
		[EnumMember]
		Approved = 8,
		// Token: 0x040003A0 RID: 928
		[EnumMember]
		InstructorInfoMissing = 16
	}
}
