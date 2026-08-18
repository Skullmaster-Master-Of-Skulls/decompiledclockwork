using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006F8 RID: 1784
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eDataSyncCourseInstructorActionDTO
	{
		// Token: 0x04000D41 RID: 3393
		[EnumMember]
		eNoChange,
		// Token: 0x04000D42 RID: 3394
		[EnumMember]
		eCreatedInstructor,
		// Token: 0x04000D43 RID: 3395
		[EnumMember]
		eUpdatedInstructorNameEmailPhone
	}
}
