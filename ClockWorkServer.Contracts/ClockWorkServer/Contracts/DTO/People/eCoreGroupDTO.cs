using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000371 RID: 881
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eCoreGroupDTO
	{
		// Token: 0x0400069F RID: 1695
		[EnumMember]
		[CoreGroupDTO("unknown")]
		Unknown,
		// Token: 0x040006A0 RID: 1696
		[EnumMember]
		[CoreGroupDTO("student")]
		Students,
		// Token: 0x040006A1 RID: 1697
		[EnumMember]
		[CoreGroupDTO("staff")]
		Staff,
		// Token: 0x040006A2 RID: 1698
		[EnumMember]
		[CoreGroupDTO("room")]
		Rooms,
		// Token: 0x040006A3 RID: 1699
		[EnumMember]
		[CoreGroupDTO("resource")]
		Resources,
		// Token: 0x040006A4 RID: 1700
		[EnumMember]
		[CoreGroupDTO("tutor")]
		Tutors,
		// Token: 0x040006A5 RID: 1701
		[EnumMember]
		[CoreGroupDTO("admin")]
		Admin = 10
	}
}
