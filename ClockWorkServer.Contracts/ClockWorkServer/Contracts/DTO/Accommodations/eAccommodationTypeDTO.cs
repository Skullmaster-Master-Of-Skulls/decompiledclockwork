using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations
{
	// Token: 0x02000C98 RID: 3224
	[Flags]
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eAccommodationTypeDTO
	{
		// Token: 0x04001987 RID: 6535
		[EnumMember]
		Unknown = 0,
		// Token: 0x04001988 RID: 6536
		[EnumMember]
		ExtraTime = 1,
		// Token: 0x04001989 RID: 6537
		[EnumMember]
		AloneRoom = 2,
		// Token: 0x0400198A RID: 6538
		[EnumMember]
		NeedsComputer = 4,
		// Token: 0x0400198B RID: 6539
		[EnumMember]
		NeedsReaderScribe = 8,
		// Token: 0x0400198C RID: 6540
		[EnumMember]
		AvailableInAllRooms = 16,
		// Token: 0x0400198D RID: 6541
		[EnumMember]
		GroupRoom = 32,
		// Token: 0x0400198E RID: 6542
		[EnumMember]
		TapedExams = 64,
		// Token: 0x0400198F RID: 6543
		[EnumMember]
		Other = 128,
		// Token: 0x04001990 RID: 6544
		[EnumMember]
		EnlargedText = 256
	}
}
