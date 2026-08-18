using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000553 RID: 1363
	public class Sha384Digest : LongDigest
	{
		// Token: 0x06002EEF RID: 12015 RVA: 0x00123551 File Offset: 0x00122551
		public Sha384Digest()
		{
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x00123559 File Offset: 0x00122559
		public Sha384Digest(Sha384Digest t) : base(t)
		{
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x00123562 File Offset: 0x00122562
		public override string AlgorithmName
		{
			get
			{
				return "SHA-384";
			}
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x00123569 File Offset: 0x00122569
		public override int GetDigestSize()
		{
			return 48;
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x00123570 File Offset: 0x00122570
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt64_To_BE(this.H1, output, outOff);
			Pack.UInt64_To_BE(this.H2, output, outOff + 8);
			Pack.UInt64_To_BE(this.H3, output, outOff + 16);
			Pack.UInt64_To_BE(this.H4, output, outOff + 24);
			Pack.UInt64_To_BE(this.H5, output, outOff + 32);
			Pack.UInt64_To_BE(this.H6, output, outOff + 40);
			this.Reset();
			return 48;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x001235E8 File Offset: 0x001225E8
		public override void Reset()
		{
			base.Reset();
			this.H1 = 14680500436340154072UL;
			this.H2 = 7105036623409894663UL;
			this.H3 = 10473403895298186519UL;
			this.H4 = 1526699215303891257UL;
			this.H5 = 7436329637833083697UL;
			this.H6 = 10282925794625328401UL;
			this.H7 = 15784041429090275239UL;
			this.H8 = 5167115440072839076UL;
		}

		// Token: 0x04002050 RID: 8272
		private const int DigestLength = 48;
	}
}
