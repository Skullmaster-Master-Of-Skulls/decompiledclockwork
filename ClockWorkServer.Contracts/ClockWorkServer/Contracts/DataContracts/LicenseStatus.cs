using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E1 RID: 225
	[DataContract(Namespace = "http://tpro.ca")]
	public enum LicenseStatus
	{
		// Token: 0x04000093 RID: 147
		[EnumMember]
		Outdated,
		// Token: 0x04000094 RID: 148
		[EnumMember]
		Updated,
		// Token: 0x04000095 RID: 149
		[EnumMember]
		Invalid
	}
}
