using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E0 RID: 224
	[DataContract(Namespace = "http://tpro.ca")]
	public enum LicenseType
	{
		// Token: 0x0400008C RID: 140
		[EnumMember]
		Demo,
		// Token: 0x0400008D RID: 141
		[EnumMember]
		Production,
		// Token: 0x0400008E RID: 142
		[EnumMember]
		Development,
		// Token: 0x0400008F RID: 143
		[EnumMember]
		Trial,
		// Token: 0x04000090 RID: 144
		[EnumMember]
		Beta,
		// Token: 0x04000091 RID: 145
		[EnumMember]
		SupportPlan
	}
}
