using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000696 RID: 1686
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eEnforceTypeDTO
	{
		// Token: 0x04000C3E RID: 3134
		[EnumMember]
		Optional,
		// Token: 0x04000C3F RID: 3135
		[EnumMember]
		Warning,
		// Token: 0x04000C40 RID: 3136
		[EnumMember]
		Error
	}
}
