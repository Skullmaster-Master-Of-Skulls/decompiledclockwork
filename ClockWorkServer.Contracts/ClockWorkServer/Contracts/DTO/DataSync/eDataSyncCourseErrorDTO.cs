using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006FA RID: 1786
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eDataSyncCourseErrorDTO
	{
		// Token: 0x04000D48 RID: 3400
		[EnumMember]
		eNoError,
		// Token: 0x04000D49 RID: 3401
		[EnumMember]
		eFailedToCreateCourse,
		// Token: 0x04000D4A RID: 3402
		[EnumMember]
		eUnknownError
	}
}
