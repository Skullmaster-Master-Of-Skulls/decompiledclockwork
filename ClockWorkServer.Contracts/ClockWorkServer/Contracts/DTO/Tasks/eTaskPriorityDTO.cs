using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E4 RID: 484
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eTaskPriorityDTO
	{
		// Token: 0x040002BD RID: 701
		[EnumMember]
		NotSpecified,
		// Token: 0x040002BE RID: 702
		[EnumMember]
		Low,
		// Token: 0x040002BF RID: 703
		[EnumMember]
		Medium,
		// Token: 0x040002C0 RID: 704
		[EnumMember]
		High,
		// Token: 0x040002C1 RID: 705
		[EnumMember]
		ReallyHigh
	}
}
