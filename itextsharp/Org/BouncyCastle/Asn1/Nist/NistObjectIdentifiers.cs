using System;

namespace Org.BouncyCastle.Asn1.Nist
{
	// Token: 0x02000145 RID: 325
	public sealed class NistObjectIdentifiers
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x00041588 File Offset: 0x00040588
		private NistObjectIdentifiers()
		{
		}

		// Token: 0x0400092B RID: 2347
		public static readonly DerObjectIdentifier NistAlgorithm = new DerObjectIdentifier("2.16.840.1.101.3.4");

		// Token: 0x0400092C RID: 2348
		public static readonly DerObjectIdentifier IdSha256 = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".2.1");

		// Token: 0x0400092D RID: 2349
		public static readonly DerObjectIdentifier IdSha384 = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".2.2");

		// Token: 0x0400092E RID: 2350
		public static readonly DerObjectIdentifier IdSha512 = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".2.3");

		// Token: 0x0400092F RID: 2351
		public static readonly DerObjectIdentifier IdSha224 = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".2.4");

		// Token: 0x04000930 RID: 2352
		public static readonly DerObjectIdentifier Aes = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".1");

		// Token: 0x04000931 RID: 2353
		public static readonly DerObjectIdentifier IdAes128Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".1");

		// Token: 0x04000932 RID: 2354
		public static readonly DerObjectIdentifier IdAes128Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".2");

		// Token: 0x04000933 RID: 2355
		public static readonly DerObjectIdentifier IdAes128Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".3");

		// Token: 0x04000934 RID: 2356
		public static readonly DerObjectIdentifier IdAes128Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".4");

		// Token: 0x04000935 RID: 2357
		public static readonly DerObjectIdentifier IdAes128Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".5");

		// Token: 0x04000936 RID: 2358
		public static readonly DerObjectIdentifier IdAes128Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".6");

		// Token: 0x04000937 RID: 2359
		public static readonly DerObjectIdentifier IdAes128Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".7");

		// Token: 0x04000938 RID: 2360
		public static readonly DerObjectIdentifier IdAes192Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".21");

		// Token: 0x04000939 RID: 2361
		public static readonly DerObjectIdentifier IdAes192Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".22");

		// Token: 0x0400093A RID: 2362
		public static readonly DerObjectIdentifier IdAes192Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".23");

		// Token: 0x0400093B RID: 2363
		public static readonly DerObjectIdentifier IdAes192Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".24");

		// Token: 0x0400093C RID: 2364
		public static readonly DerObjectIdentifier IdAes192Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".25");

		// Token: 0x0400093D RID: 2365
		public static readonly DerObjectIdentifier IdAes192Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".26");

		// Token: 0x0400093E RID: 2366
		public static readonly DerObjectIdentifier IdAes192Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".27");

		// Token: 0x0400093F RID: 2367
		public static readonly DerObjectIdentifier IdAes256Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".41");

		// Token: 0x04000940 RID: 2368
		public static readonly DerObjectIdentifier IdAes256Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".42");

		// Token: 0x04000941 RID: 2369
		public static readonly DerObjectIdentifier IdAes256Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".43");

		// Token: 0x04000942 RID: 2370
		public static readonly DerObjectIdentifier IdAes256Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".44");

		// Token: 0x04000943 RID: 2371
		public static readonly DerObjectIdentifier IdAes256Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".45");

		// Token: 0x04000944 RID: 2372
		public static readonly DerObjectIdentifier IdAes256Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".46");

		// Token: 0x04000945 RID: 2373
		public static readonly DerObjectIdentifier IdAes256Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".47");

		// Token: 0x04000946 RID: 2374
		public static readonly DerObjectIdentifier IdDsaWithSha2 = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".3");

		// Token: 0x04000947 RID: 2375
		public static readonly DerObjectIdentifier DsaWithSha224 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".1");

		// Token: 0x04000948 RID: 2376
		public static readonly DerObjectIdentifier DsaWithSha256 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".2");

		// Token: 0x04000949 RID: 2377
		public static readonly DerObjectIdentifier DsaWithSha384 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".3");

		// Token: 0x0400094A RID: 2378
		public static readonly DerObjectIdentifier DsaWithSha512 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".4");
	}
}
