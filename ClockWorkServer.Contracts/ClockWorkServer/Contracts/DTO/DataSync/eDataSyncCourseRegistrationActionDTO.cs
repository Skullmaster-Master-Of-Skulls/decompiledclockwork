using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006F6 RID: 1782
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eDataSyncCourseRegistrationActionDTO
	{
		// Token: 0x04000D2E RID: 3374
		[EnumMember]
		eNoChange,
		// Token: 0x04000D2F RID: 3375
		[EnumMember]
		eAdded,
		// Token: 0x04000D30 RID: 3376
		[EnumMember]
		eDropped,
		// Token: 0x04000D31 RID: 3377
		[EnumMember]
		eIgnoredDueToCourseDataSyncExemption,
		// Token: 0x04000D32 RID: 3378
		[EnumMember]
		eUnDropped
	}
}
