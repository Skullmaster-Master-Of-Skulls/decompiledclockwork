using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000091 RID: 145
	public class MD4Digest : GeneralDigest
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0001980E File Offset: 0x0001880E
		public MD4Digest()
		{
			this.Reset();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001982C File Offset: 0x0001882C
		public MD4Digest(MD4Digest t) : base(t)
		{
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000198A4 File Offset: 0x000188A4
		public override string AlgorithmName
		{
			get
			{
				return "MD4";
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000198AB File Offset: 0x000188AB
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000198B0 File Offset: 0x000188B0
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff++] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001991A File Offset: 0x0001891A
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00019948 File Offset: 0x00018948
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001996C File Offset: 0x0001896C
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

		// Token: 0x06000492 RID: 1170 RVA: 0x000199C8 File Offset: 0x000189C8
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

		// Token: 0x06000493 RID: 1171 RVA: 0x00019A2A File Offset: 0x00018A2A
		private int RotateLeft(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00019A3C File Offset: 0x00018A3C
		private int F(int u, int v, int w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00019A46 File Offset: 0x00018A46
		private int G(int u, int v, int w)
		{
			return (u & v) | (u & w) | (v & w);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00019A53 File Offset: 0x00018A53
		private int H(int u, int v, int w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00019A5C File Offset: 0x00018A5C
		internal override void ProcessBlock()
		{
			int num = this.H1;
			int num2 = this.H2;
			int num3 = this.H3;
			int num4 = this.H4;
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[0], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[1], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[2], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[3], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[4], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[5], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[6], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[7], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[8], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[9], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[10], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[11], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[12], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[13], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[14], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[15], 19);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[0] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[4] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[8] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[12] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[1] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[5] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[9] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[13] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[2] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[6] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[10] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[14] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[3] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[7] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[11] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[15] + 1518500249, 13);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[0] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[8] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[4] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[12] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[2] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[10] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[6] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[14] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[1] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[9] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[5] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[13] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[3] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[11] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[7] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[15] + 1859775393, 15);
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

		// Token: 0x04000253 RID: 595
		private const int DigestLength = 16;

		// Token: 0x04000254 RID: 596
		private const int S11 = 3;

		// Token: 0x04000255 RID: 597
		private const int S12 = 7;

		// Token: 0x04000256 RID: 598
		private const int S13 = 11;

		// Token: 0x04000257 RID: 599
		private const int S14 = 19;

		// Token: 0x04000258 RID: 600
		private const int S21 = 3;

		// Token: 0x04000259 RID: 601
		private const int S22 = 5;

		// Token: 0x0400025A RID: 602
		private const int S23 = 9;

		// Token: 0x0400025B RID: 603
		private const int S24 = 13;

		// Token: 0x0400025C RID: 604
		private const int S31 = 3;

		// Token: 0x0400025D RID: 605
		private const int S32 = 9;

		// Token: 0x0400025E RID: 606
		private const int S33 = 11;

		// Token: 0x0400025F RID: 607
		private const int S34 = 15;

		// Token: 0x04000260 RID: 608
		private int H1;

		// Token: 0x04000261 RID: 609
		private int H2;

		// Token: 0x04000262 RID: 610
		private int H3;

		// Token: 0x04000263 RID: 611
		private int H4;

		// Token: 0x04000264 RID: 612
		private int[] X = new int[16];

		// Token: 0x04000265 RID: 613
		private int xOff;
	}
}
