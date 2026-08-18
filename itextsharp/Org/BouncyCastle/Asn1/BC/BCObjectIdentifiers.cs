using System;

namespace Org.BouncyCastle.Asn1.BC
{
	// Token: 0x020002C3 RID: 707
	public abstract class BCObjectIdentifiers
	{
		// Token: 0x040011AD RID: 4525
		public static readonly DerObjectIdentifier bc = new DerObjectIdentifier("1.3.6.1.4.1.22554");

		// Token: 0x040011AE RID: 4526
		public static readonly DerObjectIdentifier bc_pbe = new DerObjectIdentifier(BCObjectIdentifiers.bc + ".1");

		// Token: 0x040011AF RID: 4527
		public static readonly DerObjectIdentifier bc_pbe_sha1 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe + ".1");

		// Token: 0x040011B0 RID: 4528
		public static readonly DerObjectIdentifier bc_pbe_sha256 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe + ".2.1");

		// Token: 0x040011B1 RID: 4529
		public static readonly DerObjectIdentifier bc_pbe_sha384 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe + ".2.2");

		// Token: 0x040011B2 RID: 4530
		public static readonly DerObjectIdentifier bc_pbe_sha512 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe + ".2.3");

		// Token: 0x040011B3 RID: 4531
		public static readonly DerObjectIdentifier bc_pbe_sha224 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe + ".2.4");

		// Token: 0x040011B4 RID: 4532
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs5 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha1 + ".1");

		// Token: 0x040011B5 RID: 4533
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha1 + ".2");

		// Token: 0x040011B6 RID: 4534
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs5 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha256 + ".1");

		// Token: 0x040011B7 RID: 4535
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12 = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha256 + ".2");

		// Token: 0x040011B8 RID: 4536
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes128_cbc = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha1_pkcs12 + ".1.2");

		// Token: 0x040011B9 RID: 4537
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes192_cbc = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha1_pkcs12 + ".1.22");

		// Token: 0x040011BA RID: 4538
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes256_cbc = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha1_pkcs12 + ".1.42");

		// Token: 0x040011BB RID: 4539
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes128_cbc = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha256_pkcs12 + ".1.2");

		// Token: 0x040011BC RID: 4540
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes192_cbc = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha256_pkcs12 + ".1.22");

		// Token: 0x040011BD RID: 4541
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes256_cbc = new DerObjectIdentifier(BCObjectIdentifiers.bc_pbe_sha256_pkcs12 + ".1.42");
	}
}
