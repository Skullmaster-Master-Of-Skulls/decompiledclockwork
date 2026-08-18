using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E2 RID: 226
	[DataContract(Namespace = "http://tpro.ca")]
	public enum ProductLicenseStatus
	{
		// Token: 0x04000097 RID: 151
		[EnumMember]
		NoneLicense,
		// Token: 0x04000098 RID: 152
		[EnumMember]
		OutdatedLicense,
		// Token: 0x04000099 RID: 153
		[EnumMember]
		NotValidLicense,
		// Token: 0x0400009A RID: 154
		[EnumMember]
		Licensed
	}
}
