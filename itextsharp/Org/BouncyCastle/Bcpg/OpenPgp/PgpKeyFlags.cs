using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200060A RID: 1546
	public abstract class PgpKeyFlags
	{
		// Token: 0x04002360 RID: 9056
		public const int CanCertify = 1;

		// Token: 0x04002361 RID: 9057
		public const int CanSign = 2;

		// Token: 0x04002362 RID: 9058
		public const int CanEncryptCommunications = 4;

		// Token: 0x04002363 RID: 9059
		public const int CanEncryptStorage = 8;

		// Token: 0x04002364 RID: 9060
		public const int MaybeSplit = 16;

		// Token: 0x04002365 RID: 9061
		public const int MaybeShared = 128;
	}
}
