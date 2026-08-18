using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x02000353 RID: 851
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eRunStatusStepDTO
	{
		// Token: 0x04000646 RID: 1606
		[EnumMember]
		Pending,
		// Token: 0x04000647 RID: 1607
		[EnumMember]
		Started,
		// Token: 0x04000648 RID: 1608
		[EnumMember]
		CompletedSuccessfully,
		// Token: 0x04000649 RID: 1609
		[EnumMember]
		Failed,
		// Token: 0x0400064A RID: 1610
		[EnumMember]
		FailedUnableToStart
	}
}
