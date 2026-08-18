using System;

namespace Org.BouncyCastle.Asn1.TeleTrust
{
	// Token: 0x020002B7 RID: 695
	public sealed class TeleTrusTObjectIdentifiers
	{
		// Token: 0x06001A42 RID: 6722 RVA: 0x0009B639 File Offset: 0x0009A639
		private TeleTrusTObjectIdentifiers()
		{
		}

		// Token: 0x0400117D RID: 4477
		public static readonly DerObjectIdentifier TeleTrusTAlgorithm = new DerObjectIdentifier("1.3.36.3");

		// Token: 0x0400117E RID: 4478
		public static readonly DerObjectIdentifier RipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.1");

		// Token: 0x0400117F RID: 4479
		public static readonly DerObjectIdentifier RipeMD128 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.2");

		// Token: 0x04001180 RID: 4480
		public static readonly DerObjectIdentifier RipeMD256 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.3");

		// Token: 0x04001181 RID: 4481
		public static readonly DerObjectIdentifier TeleTrusTRsaSignatureAlgorithm = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.1");

		// Token: 0x04001182 RID: 4482
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".2");

		// Token: 0x04001183 RID: 4483
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD128 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".3");

		// Token: 0x04001184 RID: 4484
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD256 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".4");

		// Token: 0x04001185 RID: 4485
		public static readonly DerObjectIdentifier ECSign = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.2");

		// Token: 0x04001186 RID: 4486
		public static readonly DerObjectIdentifier ECSignWithSha1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.ECSign + ".1");

		// Token: 0x04001187 RID: 4487
		public static readonly DerObjectIdentifier ECSignWithRipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.ECSign + ".2");

		// Token: 0x04001188 RID: 4488
		public static readonly DerObjectIdentifier EccBrainpool = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.2.8");

		// Token: 0x04001189 RID: 4489
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.EccBrainpool + ".1");

		// Token: 0x0400118A RID: 4490
		public static readonly DerObjectIdentifier VersionOne = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.EllipticCurve + ".1");

		// Token: 0x0400118B RID: 4491
		public static readonly DerObjectIdentifier BrainpoolP160R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".1");

		// Token: 0x0400118C RID: 4492
		public static readonly DerObjectIdentifier BrainpoolP160T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".2");

		// Token: 0x0400118D RID: 4493
		public static readonly DerObjectIdentifier BrainpoolP192R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".3");

		// Token: 0x0400118E RID: 4494
		public static readonly DerObjectIdentifier BrainpoolP192T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".4");

		// Token: 0x0400118F RID: 4495
		public static readonly DerObjectIdentifier BrainpoolP224R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".5");

		// Token: 0x04001190 RID: 4496
		public static readonly DerObjectIdentifier BrainpoolP224T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".6");

		// Token: 0x04001191 RID: 4497
		public static readonly DerObjectIdentifier BrainpoolP256R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".7");

		// Token: 0x04001192 RID: 4498
		public static readonly DerObjectIdentifier BrainpoolP256T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".8");

		// Token: 0x04001193 RID: 4499
		public static readonly DerObjectIdentifier BrainpoolP320R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".9");

		// Token: 0x04001194 RID: 4500
		public static readonly DerObjectIdentifier BrainpoolP320T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".10");

		// Token: 0x04001195 RID: 4501
		public static readonly DerObjectIdentifier BrainpoolP384R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".11");

		// Token: 0x04001196 RID: 4502
		public static readonly DerObjectIdentifier BrainpoolP384T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".12");

		// Token: 0x04001197 RID: 4503
		public static readonly DerObjectIdentifier BrainpoolP512R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".13");

		// Token: 0x04001198 RID: 4504
		public static readonly DerObjectIdentifier BrainpoolP512T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".14");
	}
}
