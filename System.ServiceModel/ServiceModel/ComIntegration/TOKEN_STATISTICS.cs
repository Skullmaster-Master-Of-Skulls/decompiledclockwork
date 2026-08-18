using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000250 RID: 592
	internal struct TOKEN_STATISTICS
	{
		// Token: 0x04001923 RID: 6435
		internal LUID TokenId;

		// Token: 0x04001924 RID: 6436
		internal LUID AuthenticationId;

		// Token: 0x04001925 RID: 6437
		internal long ExpirationTime;

		// Token: 0x04001926 RID: 6438
		internal uint TokenType;

		// Token: 0x04001927 RID: 6439
		internal SecurityImpersonationLevel ImpersonationLevel;

		// Token: 0x04001928 RID: 6440
		internal uint DynamicCharged;

		// Token: 0x04001929 RID: 6441
		internal uint DynamicAvailable;

		// Token: 0x0400192A RID: 6442
		internal uint GroupCount;

		// Token: 0x0400192B RID: 6443
		internal uint PrivilegeCount;

		// Token: 0x0400192C RID: 6444
		internal LUID ModifiedId;
	}
}
