using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Common
{
	// Token: 0x02000840 RID: 2112
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eConfirmDenyStatusDTO
	{
		// Token: 0x04000FF8 RID: 4088
		[EnumMember]
		Unknown,
		// Token: 0x04000FF9 RID: 4089
		[EnumMember]
		Confirmed,
		// Token: 0x04000FFA RID: 4090
		[EnumMember]
		Denied
	}
}
