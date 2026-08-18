using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B3 RID: 179
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eSessionTokenStatusDTO
	{
		// Token: 0x04000043 RID: 67
		[EnumMember]
		BelowConcurrentUserLimit,
		// Token: 0x04000044 RID: 68
		[EnumMember]
		AboveConcurrentUserLimit
	}
}
