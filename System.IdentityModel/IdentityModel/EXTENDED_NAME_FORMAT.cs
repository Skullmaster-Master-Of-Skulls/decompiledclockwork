using System;

namespace System.IdentityModel
{
	// Token: 0x0200004F RID: 79
	internal enum EXTENDED_NAME_FORMAT
	{
		// Token: 0x040002B3 RID: 691
		NameUnknown,
		// Token: 0x040002B4 RID: 692
		NameFullyQualifiedDN,
		// Token: 0x040002B5 RID: 693
		NameSamCompatible,
		// Token: 0x040002B6 RID: 694
		NameDisplay,
		// Token: 0x040002B7 RID: 695
		NameUniqueId = 6,
		// Token: 0x040002B8 RID: 696
		NameCanonical,
		// Token: 0x040002B9 RID: 697
		NameUserPrincipalName,
		// Token: 0x040002BA RID: 698
		NameCanonicalEx,
		// Token: 0x040002BB RID: 699
		NameServicePrincipalName,
		// Token: 0x040002BC RID: 700
		NameDnsDomainName = 12
	}
}
