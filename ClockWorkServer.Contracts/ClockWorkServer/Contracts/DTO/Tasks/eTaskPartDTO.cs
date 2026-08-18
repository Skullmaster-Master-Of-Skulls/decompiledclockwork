using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E5 RID: 485
	[Flags]
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eTaskPartDTO
	{
		// Token: 0x040002C3 RID: 707
		[EnumMember]
		None = 0,
		// Token: 0x040002C4 RID: 708
		[EnumMember]
		Clients = 1,
		// Token: 0x040002C5 RID: 709
		[EnumMember]
		Notes = 2,
		// Token: 0x040002C6 RID: 710
		[EnumMember]
		All = 3
	}
}
