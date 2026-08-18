using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations
{
	// Token: 0x02000C97 RID: 3223
	[Flags]
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eAccommodationGroupDTO
	{
		// Token: 0x04001981 RID: 6529
		[EnumMember]
		None = 0,
		// Token: 0x04001982 RID: 6530
		[EnumMember]
		Classroom = 1,
		// Token: 0x04001983 RID: 6531
		[EnumMember]
		TestExam = 2,
		// Token: 0x04001984 RID: 6532
		[EnumMember]
		Other = 4,
		// Token: 0x04001985 RID: 6533
		[EnumMember]
		Report = 8
	}
}
