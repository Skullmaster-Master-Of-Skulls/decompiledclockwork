using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200012F RID: 303
	public class Sha512Digest : LongDigest
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0003DFE9 File Offset: 0x0003CFE9
		public Sha512Digest()
		{
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0003DFF1 File Offset: 0x0003CFF1
		public Sha512Digest(Sha512Digest t) : base(t)
		{
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0003DFFA File Offset: 0x0003CFFA
		public override string AlgorithmName
		{
			get
			{
				return "SHA-512";
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0003E001 File Offset: 0x0003D001
		public override int GetDigestSize()
		{
			return 64;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0003E008 File Offset: 0x0003D008
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt64_To_BE(this.H1, output, outOff);
			Pack.UInt64_To_BE(this.H2, output, outOff + 8);
			Pack.UInt64_To_BE(this.H3, output, outOff + 16);
			Pack.UInt64_To_BE(this.H4, output, outOff + 24);
			Pack.UInt64_To_BE(this.H5, output, outOff + 32);
			Pack.UInt64_To_BE(this.H6, output, outOff + 40);
			Pack.UInt64_To_BE(this.H7, output, outOff + 48);
			Pack.UInt64_To_BE(this.H8, output, outOff + 56);
			this.Reset();
			return 64;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0003E0A0 File Offset: 0x0003D0A0
		public override void Reset()
		{
			base.Reset();
			this.H1 = 7640891576956012808UL;
			this.H2 = 13503953896175478587UL;
			this.H3 = 4354685564936845355UL;
			this.H4 = 11912009170470909681UL;
			this.H5 = 5840696475078001361UL;
			this.H6 = 11170449401992604703UL;
			this.H7 = 2270897969802886507UL;
			this.H8 = 6620516959819538809UL;
		}

		// Token: 0x040008C5 RID: 2245
		private const int DigestLength = 64;
	}
}
