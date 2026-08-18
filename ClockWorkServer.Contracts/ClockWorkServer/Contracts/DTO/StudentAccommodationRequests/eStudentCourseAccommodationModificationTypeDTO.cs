using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200023F RID: 575
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eStudentCourseAccommodationModificationTypeDTO
	{
		// Token: 0x04000397 RID: 919
		[EnumMember]
		none,
		// Token: 0x04000398 RID: 920
		[EnumMember]
		Remove,
		// Token: 0x04000399 RID: 921
		[EnumMember]
		Change
	}
}
