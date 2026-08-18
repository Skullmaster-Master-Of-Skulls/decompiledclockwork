using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006F9 RID: 1785
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eDataSyncCourseMiscActionDTO
	{
		// Token: 0x04000D45 RID: 3397
		[EnumMember]
		eNoAction,
		// Token: 0x04000D46 RID: 3398
		[EnumMember]
		eCreatedSubject
	}
}
