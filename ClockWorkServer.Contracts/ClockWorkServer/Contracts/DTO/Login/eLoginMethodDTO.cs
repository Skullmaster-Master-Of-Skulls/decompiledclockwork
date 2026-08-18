using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Login
{
	// Token: 0x020004BD RID: 1213
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eLoginMethodDTO
	{
		// Token: 0x040008D9 RID: 2265
		[EnumMember]
		ClockWorkLogin,
		// Token: 0x040008DA RID: 2266
		[EnumMember]
		WindowsLogin,
		// Token: 0x040008DB RID: 2267
		[EnumMember]
		Ldap,
		// Token: 0x040008DC RID: 2268
		[EnumMember]
		ActiveDirectory = 4,
		// Token: 0x040008DD RID: 2269
		[EnumMember]
		Shiboleth = 8
	}
}
