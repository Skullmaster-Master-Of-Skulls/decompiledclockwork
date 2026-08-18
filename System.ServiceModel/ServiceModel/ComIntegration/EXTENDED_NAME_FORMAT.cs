using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200024A RID: 586
	internal enum EXTENDED_NAME_FORMAT
	{
		// Token: 0x040018DC RID: 6364
		NameUnknown,
		// Token: 0x040018DD RID: 6365
		NameFullyQualifiedDN,
		// Token: 0x040018DE RID: 6366
		NameSamCompatible,
		// Token: 0x040018DF RID: 6367
		NameDisplay,
		// Token: 0x040018E0 RID: 6368
		NameUniqueId = 6,
		// Token: 0x040018E1 RID: 6369
		NameCanonical,
		// Token: 0x040018E2 RID: 6370
		NameUserPrincipalName,
		// Token: 0x040018E3 RID: 6371
		NameCanonicalEx,
		// Token: 0x040018E4 RID: 6372
		NameServicePrincipalName,
		// Token: 0x040018E5 RID: 6373
		NameDnsDomainName = 12
	}
}
