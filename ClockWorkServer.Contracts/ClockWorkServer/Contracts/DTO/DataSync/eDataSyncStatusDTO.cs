using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000729 RID: 1833
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eDataSyncStatusDTO
	{
		// Token: 0x04000DDA RID: 3546
		[EnumMember]
		Unknown,
		// Token: 0x04000DDB RID: 3547
		[EnumMember]
		CompletedSuccessfully,
		// Token: 0x04000DDC RID: 3548
		[EnumMember]
		Failed,
		// Token: 0x04000DDD RID: 3549
		[EnumMember]
		FailedCantFindStudentNumber
	}
}
