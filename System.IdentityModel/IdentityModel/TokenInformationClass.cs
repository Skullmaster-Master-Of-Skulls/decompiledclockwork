using System;

namespace System.IdentityModel
{
	// Token: 0x02000050 RID: 80
	internal enum TokenInformationClass : uint
	{
		// Token: 0x040002BE RID: 702
		TokenUser = 1U,
		// Token: 0x040002BF RID: 703
		TokenGroups,
		// Token: 0x040002C0 RID: 704
		TokenPrivileges,
		// Token: 0x040002C1 RID: 705
		TokenOwner,
		// Token: 0x040002C2 RID: 706
		TokenPrimaryGroup,
		// Token: 0x040002C3 RID: 707
		TokenDefaultDacl,
		// Token: 0x040002C4 RID: 708
		TokenSource,
		// Token: 0x040002C5 RID: 709
		TokenType,
		// Token: 0x040002C6 RID: 710
		TokenImpersonationLevel,
		// Token: 0x040002C7 RID: 711
		TokenStatistics,
		// Token: 0x040002C8 RID: 712
		TokenRestrictedSids,
		// Token: 0x040002C9 RID: 713
		TokenSessionId,
		// Token: 0x040002CA RID: 714
		TokenGroupsAndPrivileges,
		// Token: 0x040002CB RID: 715
		TokenSessionReference,
		// Token: 0x040002CC RID: 716
		TokenSandBoxInert
	}
}
