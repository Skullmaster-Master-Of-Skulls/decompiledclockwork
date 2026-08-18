using System;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000114 RID: 276
	public abstract class OcspRespStatus
	{
		// Token: 0x04000868 RID: 2152
		public const int Successful = 0;

		// Token: 0x04000869 RID: 2153
		public const int MalformedRequest = 1;

		// Token: 0x0400086A RID: 2154
		public const int InternalError = 2;

		// Token: 0x0400086B RID: 2155
		public const int TryLater = 3;

		// Token: 0x0400086C RID: 2156
		public const int SigRequired = 5;

		// Token: 0x0400086D RID: 2157
		public const int Unauthorized = 6;
	}
}
