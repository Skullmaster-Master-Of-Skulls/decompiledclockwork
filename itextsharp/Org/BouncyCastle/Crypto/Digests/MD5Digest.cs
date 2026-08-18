using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020002AA RID: 682
	public class MD5Digest : GeneralDigest
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x00098E55 File Offset: 0x00097E55
		public MD5Digest()
		{
			this.Reset();
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00098E70 File Offset: 0x00097E70
		public MD5Digest(MD5Digest t) : base(t)
		{
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00098EE8 File Offset: 0x00097EE8
		public override string AlgorithmName
		{
			get
			{
				return "MD5";
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00098EEF File Offset: 0x00097EEF
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00098EF4 File Offset: 0x00097EF4
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff++] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00098F5E File Offset: 0x00097F5E
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00098F8C File Offset: 0x00097F8C
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00098FB0 File Offset: 0x00097FB0
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H1, output, outOff);
			this.UnpackWord(this.H2, output, outOff + 4);
			this.UnpackWord(this.H3, output, outOff + 8);
			this.UnpackWord(this.H4, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0009900C File Offset: 0x0009800C
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193;
			this.H2 = -271733879;
			this.H3 = -1732584194;
			this.H4 = 271733878;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0009906E File Offset: 0x0009806E
		private int RotateLeft(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00099080 File Offset: 0x00098080
		private int F(int u, int v, int w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0009908A File Offset: 0x0009808A
		private int G(int u, int v, int w)
		{
			return (u & w) | (v & ~w);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00099094 File Offset: 0x00098094
		private int H(int u, int v, int w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0009909B File Offset: 0x0009809B
		private int K(int u, int v, int w)
		{
			return v ^ (u | ~w);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000990A4 File Offset: 0x000980A4
		internal override void ProcessBlock()
		{
			int num = this.H1;
			int num2 = this.H2;
			int num3 = this.H3;
			int num4 = this.H4;
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[0] + -680876936, MD5Digest.S11) + num2;
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[1] + -389564586, MD5Digest.S12) + num;
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[2] + 606105819, MD5Digest.S13) + num4;
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[3] + -1044525330, MD5Digest.S14) + num3;
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[4] + -176418897, MD5Digest.S11) + num2;
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[5] + 1200080426, MD5Digest.S12) + num;
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[6] + -1473231341, MD5Digest.S13) + num4;
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[7] + -45705983, MD5Digest.S14) + num3;
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[8] + 1770035416, MD5Digest.S11) + num2;
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[9] + -1958414417, MD5Digest.S12) + num;
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[10] + -42063, MD5Digest.S13) + num4;
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[11] + -1990404162, MD5Digest.S14) + num3;
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[12] + 1804603682, MD5Digest.S11) + num2;
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[13] + -40341101, MD5Digest.S12) + num;
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[14] + -1502002290, MD5Digest.S13) + num4;
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[15] + 1236535329, MD5Digest.S14) + num3;
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[1] + -165796510, MD5Digest.S21) + num2;
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[6] + -1069501632, MD5Digest.S22) + num;
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[11] + 643717713, MD5Digest.S23) + num4;
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[0] + -373897302, MD5Digest.S24) + num3;
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[5] + -701558691, MD5Digest.S21) + num2;
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[10] + 38016083, MD5Digest.S22) + num;
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[15] + -660478335, MD5Digest.S23) + num4;
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[4] + -405537848, MD5Digest.S24) + num3;
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[9] + 568446438, MD5Digest.S21) + num2;
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[14] + -1019803690, MD5Digest.S22) + num;
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[3] + -187363961, MD5Digest.S23) + num4;
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[8] + 1163531501, MD5Digest.S24) + num3;
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[13] + -1444681467, MD5Digest.S21) + num2;
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[2] + -51403784, MD5Digest.S22) + num;
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[7] + 1735328473, MD5Digest.S23) + num4;
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[12] + -1926607734, MD5Digest.S24) + num3;
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[5] + -378558, MD5Digest.S31) + num2;
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[8] + -2022574463, MD5Digest.S32) + num;
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[11] + 1839030562, MD5Digest.S33) + num4;
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[14] + -35309556, MD5Digest.S34) + num3;
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[1] + -1530992060, MD5Digest.S31) + num2;
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[4] + 1272893353, MD5Digest.S32) + num;
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[7] + -155497632, MD5Digest.S33) + num4;
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[10] + -1094730640, MD5Digest.S34) + num3;
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[13] + 681279174, MD5Digest.S31) + num2;
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[0] + -358537222, MD5Digest.S32) + num;
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[3] + -722521979, MD5Digest.S33) + num4;
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[6] + 76029189, MD5Digest.S34) + num3;
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[9] + -640364487, MD5Digest.S31) + num2;
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[12] + -421815835, MD5Digest.S32) + num;
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[15] + 530742520, MD5Digest.S33) + num4;
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[2] + -995338651, MD5Digest.S34) + num3;
			num = this.RotateLeft(num + this.K(num2, num3, num4) + this.X[0] + -198630844, MD5Digest.S41) + num2;
			num4 = this.RotateLeft(num4 + this.K(num, num2, num3) + this.X[7] + 1126891415, MD5Digest.S42) + num;
			num3 = this.RotateLeft(num3 + this.K(num4, num, num2) + this.X[14] + -1416354905, MD5Digest.S43) + num4;
			num2 = this.RotateLeft(num2 + this.K(num3, num4, num) + this.X[5] + -57434055, MD5Digest.S44) + num3;
			num = this.RotateLeft(num + this.K(num2, num3, num4) + this.X[12] + 1700485571, MD5Digest.S41) + num2;
			num4 = this.RotateLeft(num4 + this.K(num, num2, num3) + this.X[3] + -1894986606, MD5Digest.S42) + num;
			num3 = this.RotateLeft(num3 + this.K(num4, num, num2) + this.X[10] + -1051523, MD5Digest.S43) + num4;
			num2 = this.RotateLeft(num2 + this.K(num3, num4, num) + this.X[1] + -2054922799, MD5Digest.S44) + num3;
			num = this.RotateLeft(num + this.K(num2, num3, num4) + this.X[8] + 1873313359, MD5Digest.S41) + num2;
			num4 = this.RotateLeft(num4 + this.K(num, num2, num3) + this.X[15] + -30611744, MD5Digest.S42) + num;
			num3 = this.RotateLeft(num3 + this.K(num4, num, num2) + this.X[6] + -1560198380, MD5Digest.S43) + num4;
			num2 = this.RotateLeft(num2 + this.K(num3, num4, num) + this.X[13] + 1309151649, MD5Digest.S44) + num3;
			num = this.RotateLeft(num + this.K(num2, num3, num4) + this.X[4] + -145523070, MD5Digest.S41) + num2;
			num4 = this.RotateLeft(num4 + this.K(num, num2, num3) + this.X[11] + -1120210379, MD5Digest.S42) + num;
			num3 = this.RotateLeft(num3 + this.K(num4, num, num2) + this.X[2] + 718787259, MD5Digest.S43) + num4;
			num2 = this.RotateLeft(num2 + this.K(num3, num4, num) + this.X[9] + -343485551, MD5Digest.S44) + num3;
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.xOff = 0;
			for (int num5 = 0; num5 != this.X.Length; num5++)
			{
				this.X[num5] = 0;
			}
		}

		// Token: 0x0400112D RID: 4397
		private const int DigestLength = 16;

		// Token: 0x0400112E RID: 4398
		private int H1;

		// Token: 0x0400112F RID: 4399
		private int H2;

		// Token: 0x04001130 RID: 4400
		private int H3;

		// Token: 0x04001131 RID: 4401
		private int H4;

		// Token: 0x04001132 RID: 4402
		private int[] X = new int[16];

		// Token: 0x04001133 RID: 4403
		private int xOff;

		// Token: 0x04001134 RID: 4404
		private static readonly int S11 = 7;

		// Token: 0x04001135 RID: 4405
		private static readonly int S12 = 12;

		// Token: 0x04001136 RID: 4406
		private static readonly int S13 = 17;

		// Token: 0x04001137 RID: 4407
		private static readonly int S14 = 22;

		// Token: 0x04001138 RID: 4408
		private static readonly int S21 = 5;

		// Token: 0x04001139 RID: 4409
		private static readonly int S22 = 9;

		// Token: 0x0400113A RID: 4410
		private static readonly int S23 = 14;

		// Token: 0x0400113B RID: 4411
		private static readonly int S24 = 20;

		// Token: 0x0400113C RID: 4412
		private static readonly int S31 = 4;

		// Token: 0x0400113D RID: 4413
		private static readonly int S32 = 11;

		// Token: 0x0400113E RID: 4414
		private static readonly int S33 = 16;

		// Token: 0x0400113F RID: 4415
		private static readonly int S34 = 23;

		// Token: 0x04001140 RID: 4416
		private static readonly int S41 = 6;

		// Token: 0x04001141 RID: 4417
		private static readonly int S42 = 10;

		// Token: 0x04001142 RID: 4418
		private static readonly int S43 = 15;

		// Token: 0x04001143 RID: 4419
		private static readonly int S44 = 21;
	}
}
