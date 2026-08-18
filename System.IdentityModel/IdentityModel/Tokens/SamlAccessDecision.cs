using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000149 RID: 329
	[DataContract]
	public enum SamlAccessDecision
	{
		// Token: 0x04000B7B RID: 2939
		[EnumMember]
		Permit,
		// Token: 0x04000B7C RID: 2940
		[EnumMember]
		Deny,
		// Token: 0x04000B7D RID: 2941
		[EnumMember]
		Indeterminate
	}
}
