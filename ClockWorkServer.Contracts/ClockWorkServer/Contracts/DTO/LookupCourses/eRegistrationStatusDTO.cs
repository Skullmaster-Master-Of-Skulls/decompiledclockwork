using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200078A RID: 1930
	public enum eRegistrationStatusDTO
	{
		// Token: 0x04000EA5 RID: 3749
		[EnumMember]
		Normal,
		// Token: 0x04000EA6 RID: 3750
		[EnumMember]
		Dropped = 2,
		// Token: 0x04000EA7 RID: 3751
		[EnumMember]
		NormalAndExemptFromDataSync = 8
	}
}
