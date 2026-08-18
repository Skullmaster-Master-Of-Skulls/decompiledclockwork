using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000131 RID: 305
	public class RipeMD128Digest : GeneralDigest
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x0003E841 File Offset: 0x0003D841
		public RipeMD128Digest()
		{
			this.Reset();
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0003E85C File Offset: 0x0003D85C
		public RipeMD128Digest(RipeMD128Digest t) : base(t)
		{
			this.H0 = t.H0;
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0003E8D4 File Offset: 0x0003D8D4
		public override string AlgorithmName
		{
			get
			{
				return "RIPEMD128";
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0003E8DB File Offset: 0x0003D8DB
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003E8E0 File Offset: 0x0003D8E0
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff++] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0003E94A File Offset: 0x0003D94A
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003E978 File Offset: 0x0003D978
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003E99C File Offset: 0x0003D99C
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H0, output, outOff);
			this.UnpackWord(this.H1, output, outOff + 4);
			this.UnpackWord(this.H2, output, outOff + 8);
			this.UnpackWord(this.H3, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0003E9F8 File Offset: 0x0003D9F8
		public override void Reset()
		{
			base.Reset();
			this.H0 = 1732584193;
			this.H1 = -271733879;
			this.H2 = -1732584194;
			this.H3 = 271733878;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0003EA5A File Offset: 0x0003DA5A
		private int RL(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0003EA6C File Offset: 0x0003DA6C
		private int F1(int x, int y, int z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0003EA73 File Offset: 0x0003DA73
		private int F2(int x, int y, int z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0003EA7D File Offset: 0x0003DA7D
		private int F3(int x, int y, int z)
		{
			return (x | ~y) ^ z;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0003EA85 File Offset: 0x0003DA85
		private int F4(int x, int y, int z)
		{
			return (x & z) | (y & ~z);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0003EA8F File Offset: 0x0003DA8F
		private int F1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0003EAA8 File Offset: 0x0003DAA8
		private int F2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1518500249, s);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0003EAC7 File Offset: 0x0003DAC7
		private int F3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1859775393, s);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0003EAE6 File Offset: 0x0003DAE6
		private int F4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + -1894007588, s);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0003EB05 File Offset: 0x0003DB05
		private int FF1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0003EB1E File Offset: 0x0003DB1E
		private int FF2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1836072691, s);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0003EB3D File Offset: 0x0003DB3D
		private int FF3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1548603684, s);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0003EB5C File Offset: 0x0003DB5C
		private int FF4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + 1352829926, s);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0003EB7C File Offset: 0x0003DB7C
		internal override void ProcessBlock()
		{
			int num2;
			int num = num2 = this.H0;
			int num4;
			int num3 = num4 = this.H1;
			int num6;
			int num5 = num6 = this.H2;
			int num8;
			int num7 = num8 = this.H3;
			num2 = this.F1(num2, num4, num6, num8, this.X[0], 11);
			num8 = this.F1(num8, num2, num4, num6, this.X[1], 14);
			num6 = this.F1(num6, num8, num2, num4, this.X[2], 15);
			num4 = this.F1(num4, num6, num8, num2, this.X[3], 12);
			num2 = this.F1(num2, num4, num6, num8, this.X[4], 5);
			num8 = this.F1(num8, num2, num4, num6, this.X[5], 8);
			num6 = this.F1(num6, num8, num2, num4, this.X[6], 7);
			num4 = this.F1(num4, num6, num8, num2, this.X[7], 9);
			num2 = this.F1(num2, num4, num6, num8, this.X[8], 11);
			num8 = this.F1(num8, num2, num4, num6, this.X[9], 13);
			num6 = this.F1(num6, num8, num2, num4, this.X[10], 14);
			num4 = this.F1(num4, num6, num8, num2, this.X[11], 15);
			num2 = this.F1(num2, num4, num6, num8, this.X[12], 6);
			num8 = this.F1(num8, num2, num4, num6, this.X[13], 7);
			num6 = this.F1(num6, num8, num2, num4, this.X[14], 9);
			num4 = this.F1(num4, num6, num8, num2, this.X[15], 8);
			num2 = this.F2(num2, num4, num6, num8, this.X[7], 7);
			num8 = this.F2(num8, num2, num4, num6, this.X[4], 6);
			num6 = this.F2(num6, num8, num2, num4, this.X[13], 8);
			num4 = this.F2(num4, num6, num8, num2, this.X[1], 13);
			num2 = this.F2(num2, num4, num6, num8, this.X[10], 11);
			num8 = this.F2(num8, num2, num4, num6, this.X[6], 9);
			num6 = this.F2(num6, num8, num2, num4, this.X[15], 7);
			num4 = this.F2(num4, num6, num8, num2, this.X[3], 15);
			num2 = this.F2(num2, num4, num6, num8, this.X[12], 7);
			num8 = this.F2(num8, num2, num4, num6, this.X[0], 12);
			num6 = this.F2(num6, num8, num2, num4, this.X[9], 15);
			num4 = this.F2(num4, num6, num8, num2, this.X[5], 9);
			num2 = this.F2(num2, num4, num6, num8, this.X[2], 11);
			num8 = this.F2(num8, num2, num4, num6, this.X[14], 7);
			num6 = this.F2(num6, num8, num2, num4, this.X[11], 13);
			num4 = this.F2(num4, num6, num8, num2, this.X[8], 12);
			num2 = this.F3(num2, num4, num6, num8, this.X[3], 11);
			num8 = this.F3(num8, num2, num4, num6, this.X[10], 13);
			num6 = this.F3(num6, num8, num2, num4, this.X[14], 6);
			num4 = this.F3(num4, num6, num8, num2, this.X[4], 7);
			num2 = this.F3(num2, num4, num6, num8, this.X[9], 14);
			num8 = this.F3(num8, num2, num4, num6, this.X[15], 9);
			num6 = this.F3(num6, num8, num2, num4, this.X[8], 13);
			num4 = this.F3(num4, num6, num8, num2, this.X[1], 15);
			num2 = this.F3(num2, num4, num6, num8, this.X[2], 14);
			num8 = this.F3(num8, num2, num4, num6, this.X[7], 8);
			num6 = this.F3(num6, num8, num2, num4, this.X[0], 13);
			num4 = this.F3(num4, num6, num8, num2, this.X[6], 6);
			num2 = this.F3(num2, num4, num6, num8, this.X[13], 5);
			num8 = this.F3(num8, num2, num4, num6, this.X[11], 12);
			num6 = this.F3(num6, num8, num2, num4, this.X[5], 7);
			num4 = this.F3(num4, num6, num8, num2, this.X[12], 5);
			num2 = this.F4(num2, num4, num6, num8, this.X[1], 11);
			num8 = this.F4(num8, num2, num4, num6, this.X[9], 12);
			num6 = this.F4(num6, num8, num2, num4, this.X[11], 14);
			num4 = this.F4(num4, num6, num8, num2, this.X[10], 15);
			num2 = this.F4(num2, num4, num6, num8, this.X[0], 14);
			num8 = this.F4(num8, num2, num4, num6, this.X[8], 15);
			num6 = this.F4(num6, num8, num2, num4, this.X[12], 9);
			num4 = this.F4(num4, num6, num8, num2, this.X[4], 8);
			num2 = this.F4(num2, num4, num6, num8, this.X[13], 9);
			num8 = this.F4(num8, num2, num4, num6, this.X[3], 14);
			num6 = this.F4(num6, num8, num2, num4, this.X[7], 5);
			num4 = this.F4(num4, num6, num8, num2, this.X[15], 6);
			num2 = this.F4(num2, num4, num6, num8, this.X[14], 8);
			num8 = this.F4(num8, num2, num4, num6, this.X[5], 6);
			num6 = this.F4(num6, num8, num2, num4, this.X[6], 5);
			num4 = this.F4(num4, num6, num8, num2, this.X[2], 12);
			num = this.FF4(num, num3, num5, num7, this.X[5], 8);
			num7 = this.FF4(num7, num, num3, num5, this.X[14], 9);
			num5 = this.FF4(num5, num7, num, num3, this.X[7], 9);
			num3 = this.FF4(num3, num5, num7, num, this.X[0], 11);
			num = this.FF4(num, num3, num5, num7, this.X[9], 13);
			num7 = this.FF4(num7, num, num3, num5, this.X[2], 15);
			num5 = this.FF4(num5, num7, num, num3, this.X[11], 15);
			num3 = this.FF4(num3, num5, num7, num, this.X[4], 5);
			num = this.FF4(num, num3, num5, num7, this.X[13], 7);
			num7 = this.FF4(num7, num, num3, num5, this.X[6], 7);
			num5 = this.FF4(num5, num7, num, num3, this.X[15], 8);
			num3 = this.FF4(num3, num5, num7, num, this.X[8], 11);
			num = this.FF4(num, num3, num5, num7, this.X[1], 14);
			num7 = this.FF4(num7, num, num3, num5, this.X[10], 14);
			num5 = this.FF4(num5, num7, num, num3, this.X[3], 12);
			num3 = this.FF4(num3, num5, num7, num, this.X[12], 6);
			num = this.FF3(num, num3, num5, num7, this.X[6], 9);
			num7 = this.FF3(num7, num, num3, num5, this.X[11], 13);
			num5 = this.FF3(num5, num7, num, num3, this.X[3], 15);
			num3 = this.FF3(num3, num5, num7, num, this.X[7], 7);
			num = this.FF3(num, num3, num5, num7, this.X[0], 12);
			num7 = this.FF3(num7, num, num3, num5, this.X[13], 8);
			num5 = this.FF3(num5, num7, num, num3, this.X[5], 9);
			num3 = this.FF3(num3, num5, num7, num, this.X[10], 11);
			num = this.FF3(num, num3, num5, num7, this.X[14], 7);
			num7 = this.FF3(num7, num, num3, num5, this.X[15], 7);
			num5 = this.FF3(num5, num7, num, num3, this.X[8], 12);
			num3 = this.FF3(num3, num5, num7, num, this.X[12], 7);
			num = this.FF3(num, num3, num5, num7, this.X[4], 6);
			num7 = this.FF3(num7, num, num3, num5, this.X[9], 15);
			num5 = this.FF3(num5, num7, num, num3, this.X[1], 13);
			num3 = this.FF3(num3, num5, num7, num, this.X[2], 11);
			num = this.FF2(num, num3, num5, num7, this.X[15], 9);
			num7 = this.FF2(num7, num, num3, num5, this.X[5], 7);
			num5 = this.FF2(num5, num7, num, num3, this.X[1], 15);
			num3 = this.FF2(num3, num5, num7, num, this.X[3], 11);
			num = this.FF2(num, num3, num5, num7, this.X[7], 8);
			num7 = this.FF2(num7, num, num3, num5, this.X[14], 6);
			num5 = this.FF2(num5, num7, num, num3, this.X[6], 6);
			num3 = this.FF2(num3, num5, num7, num, this.X[9], 14);
			num = this.FF2(num, num3, num5, num7, this.X[11], 12);
			num7 = this.FF2(num7, num, num3, num5, this.X[8], 13);
			num5 = this.FF2(num5, num7, num, num3, this.X[12], 5);
			num3 = this.FF2(num3, num5, num7, num, this.X[2], 14);
			num = this.FF2(num, num3, num5, num7, this.X[10], 13);
			num7 = this.FF2(num7, num, num3, num5, this.X[0], 13);
			num5 = this.FF2(num5, num7, num, num3, this.X[4], 7);
			num3 = this.FF2(num3, num5, num7, num, this.X[13], 5);
			num = this.FF1(num, num3, num5, num7, this.X[8], 15);
			num7 = this.FF1(num7, num, num3, num5, this.X[6], 5);
			num5 = this.FF1(num5, num7, num, num3, this.X[4], 8);
			num3 = this.FF1(num3, num5, num7, num, this.X[1], 11);
			num = this.FF1(num, num3, num5, num7, this.X[3], 14);
			num7 = this.FF1(num7, num, num3, num5, this.X[11], 14);
			num5 = this.FF1(num5, num7, num, num3, this.X[15], 6);
			num3 = this.FF1(num3, num5, num7, num, this.X[0], 14);
			num = this.FF1(num, num3, num5, num7, this.X[5], 6);
			num7 = this.FF1(num7, num, num3, num5, this.X[12], 9);
			num5 = this.FF1(num5, num7, num, num3, this.X[2], 12);
			num3 = this.FF1(num3, num5, num7, num, this.X[13], 9);
			num = this.FF1(num, num3, num5, num7, this.X[9], 12);
			num7 = this.FF1(num7, num, num3, num5, this.X[7], 5);
			num5 = this.FF1(num5, num7, num, num3, this.X[10], 15);
			num3 = this.FF1(num3, num5, num7, num, this.X[14], 8);
			num7 += num6 + this.H1;
			this.H1 = this.H2 + num8 + num;
			this.H2 = this.H3 + num2 + num3;
			this.H3 = this.H0 + num4 + num5;
			this.H0 = num7;
			this.xOff = 0;
			for (int num9 = 0; num9 != this.X.Length; num9++)
			{
				this.X[num9] = 0;
			}
		}

		// Token: 0x040008D2 RID: 2258
		private const int DigestLength = 16;

		// Token: 0x040008D3 RID: 2259
		private int H0;

		// Token: 0x040008D4 RID: 2260
		private int H1;

		// Token: 0x040008D5 RID: 2261
		private int H2;

		// Token: 0x040008D6 RID: 2262
		private int H3;

		// Token: 0x040008D7 RID: 2263
		private int[] X = new int[16];

		// Token: 0x040008D8 RID: 2264
		private int xOff;
	}
}
