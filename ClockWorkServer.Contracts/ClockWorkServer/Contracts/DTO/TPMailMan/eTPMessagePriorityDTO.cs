using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B2 RID: 434
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public enum eTPMessagePriorityDTO
	{
		// Token: 0x04000235 RID: 565
		[EnumMember]
		Unknown,
		// Token: 0x04000236 RID: 566
		[EnumMember]
		Lowest = 5,
		// Token: 0x04000237 RID: 567
		[EnumMember]
		Low = 4,
		// Token: 0x04000238 RID: 568
		[EnumMember]
		Normal = 3,
		// Token: 0x04000239 RID: 569
		[EnumMember]
		High = 2,
		// Token: 0x0400023A RID: 570
		[EnumMember]
		Highest = 1
	}
}
