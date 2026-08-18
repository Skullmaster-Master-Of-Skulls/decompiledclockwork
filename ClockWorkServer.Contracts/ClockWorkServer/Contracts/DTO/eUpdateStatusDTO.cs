using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000FB RID: 251
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eUpdateStatusDTO
	{
		// Token: 0x040000CB RID: 203
		[EnumMember]
		Pending,
		// Token: 0x040000CC RID: 204
		[EnumMember]
		Done,
		// Token: 0x040000CD RID: 205
		[EnumMember]
		OnSchedule,
		// Token: 0x040000CE RID: 206
		[EnumMember]
		Dismiss
	}
}
