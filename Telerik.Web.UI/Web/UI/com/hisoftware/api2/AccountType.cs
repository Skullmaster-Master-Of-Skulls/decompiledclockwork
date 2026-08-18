using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001360 RID: 4960
	[DataContract(Name = "AccountType", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum AccountType
	{
		// Token: 0x04003781 RID: 14209
		[EnumMember]
		Trial,
		// Token: 0x04003782 RID: 14210
		[EnumMember]
		Enterprise,
		// Token: 0x04003783 RID: 14211
		[EnumMember]
		Partner,
		// Token: 0x04003784 RID: 14212
		[EnumMember]
		Internal
	}
}
