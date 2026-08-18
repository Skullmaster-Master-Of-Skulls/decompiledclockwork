using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Files
{
	// Token: 0x020005F1 RID: 1521
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eFileFormatDTO
	{
		// Token: 0x04000AF0 RID: 2800
		[EnumMember]
		Unknown,
		// Token: 0x04000AF1 RID: 2801
		[EnumMember]
		Word,
		// Token: 0x04000AF2 RID: 2802
		[EnumMember]
		WordX,
		// Token: 0x04000AF3 RID: 2803
		[EnumMember]
		PDF = 4
	}
}
