using System;

namespace System.Security.Cryptography
{
	// Token: 0x020000E0 RID: 224
	internal struct BCRYPT_DSA_KEY_BLOB_V2
	{
		// Token: 0x040005DD RID: 1501
		public BCryptNative.KeyBlobMagicNumber dwMagic;

		// Token: 0x040005DE RID: 1502
		public int cbKey;

		// Token: 0x040005DF RID: 1503
		public HASHALGORITHM_ENUM hashAlgorithm;

		// Token: 0x040005E0 RID: 1504
		public DSAFIPSVERSION_ENUM standardVersion;

		// Token: 0x040005E1 RID: 1505
		public int cbSeedLength;

		// Token: 0x040005E2 RID: 1506
		public int cbGroupSize;

		// Token: 0x040005E3 RID: 1507
		public byte Count3;

		// Token: 0x040005E4 RID: 1508
		public byte Count2;

		// Token: 0x040005E5 RID: 1509
		public byte Count1;

		// Token: 0x040005E6 RID: 1510
		public byte Count0;
	}
}
