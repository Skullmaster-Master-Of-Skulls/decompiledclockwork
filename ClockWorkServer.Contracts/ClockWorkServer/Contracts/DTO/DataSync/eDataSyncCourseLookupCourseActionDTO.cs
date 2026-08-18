using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006F7 RID: 1783
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eDataSyncCourseLookupCourseActionDTO
	{
		// Token: 0x04000D34 RID: 3380
		[EnumMember]
		eNoChange,
		// Token: 0x04000D35 RID: 3381
		[EnumMember]
		eUpdatedPrimaryInstructor,
		// Token: 0x04000D36 RID: 3382
		[EnumMember]
		eUpdatedInstructorList,
		// Token: 0x04000D37 RID: 3383
		[EnumMember]
		eUpdatedAltContactList,
		// Token: 0x04000D38 RID: 3384
		[EnumMember]
		eChangedLocation,
		// Token: 0x04000D39 RID: 3385
		[EnumMember]
		eSetLocation,
		// Token: 0x04000D3A RID: 3386
		[EnumMember]
		eChangedCampus,
		// Token: 0x04000D3B RID: 3387
		[EnumMember]
		eSetCampus,
		// Token: 0x04000D3C RID: 3388
		[EnumMember]
		eUpdatedTimetable,
		// Token: 0x04000D3D RID: 3389
		[EnumMember]
		eCreatedCourse,
		// Token: 0x04000D3E RID: 3390
		[EnumMember]
		eRemovedSecondaryInstructor,
		// Token: 0x04000D3F RID: 3391
		[EnumMember]
		eAddedSecondaryInstructor
	}
}
