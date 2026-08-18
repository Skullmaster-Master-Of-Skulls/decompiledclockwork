using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001357 RID: 4951
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ResultType", Namespace = "urn:hisoftware:compliancesheriff:data")]
	public enum ResultType
	{
		// Token: 0x04003764 RID: 14180
		[EnumMember]
		Fail,
		// Token: 0x04003765 RID: 14181
		[EnumMember]
		Warning,
		// Token: 0x04003766 RID: 14182
		[EnumMember]
		Visual,
		// Token: 0x04003767 RID: 14183
		[EnumMember]
		Pass,
		// Token: 0x04003768 RID: 14184
		[EnumMember]
		NA
	}
}
