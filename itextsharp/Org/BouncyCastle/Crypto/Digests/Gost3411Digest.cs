using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000026 RID: 38
	public class Gost3411Digest : IDigest
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00007A8C File Offset: 0x00006A8C
		public Gost3411Digest()
		{
			for (int i = 0; i < 4; i++)
			{
				this.C[i] = new byte[32];
			}
			this.cipher.Init(true, new ParametersWithSBox(null, Gost28147Engine.GetSBox("D-A")));
			this.Reset();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007B9C File Offset: 0x00006B9C
		public Gost3411Digest(Gost3411Digest t) : this()
		{
			Array.Copy(t.H, 0, this.H, 0, t.H.Length);
			Array.Copy(t.L, 0, this.L, 0, t.L.Length);
			Array.Copy(t.M, 0, this.M, 0, t.M.Length);
			Array.Copy(t.Sum, 0, this.Sum, 0, t.Sum.Length);
			Array.Copy(t.C[1], 0, this.C[1], 0, t.C[1].Length);
			Array.Copy(t.C[2], 0, this.C[2], 0, t.C[2].Length);
			Array.Copy(t.C[3], 0, this.C[3], 0, t.C[3].Length);
			Array.Copy(t.xBuf, 0, this.xBuf, 0, t.xBuf.Length);
			this.xBufOff = t.xBufOff;
			this.byteCount = t.byteCount;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00007CB1 File Offset: 0x00006CB1
		public string AlgorithmName
		{
			get
			{
				return "Gost3411";
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007CB8 File Offset: 0x00006CB8
		public int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007CBC File Offset: 0x00006CBC
		public void Update(byte input)
		{
			this.xBuf[this.xBufOff++] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.sumByteArray(this.xBuf);
				this.processBlock(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount += 1L;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007D24 File Offset: 0x00006D24
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (this.xBufOff != 0)
			{
				if (length <= 0)
				{
					break;
				}
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			while (length > this.xBuf.Length)
			{
				Array.Copy(input, inOff, this.xBuf, 0, this.xBuf.Length);
				this.sumByteArray(this.xBuf);
				this.processBlock(this.xBuf, 0);
				inOff += this.xBuf.Length;
				length -= this.xBuf.Length;
				this.byteCount += (long)this.xBuf.Length;
			}
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007DD8 File Offset: 0x00006DD8
		private byte[] P(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				this.K[num++] = input[i];
				this.K[num++] = input[8 + i];
				this.K[num++] = input[16 + i];
				this.K[num++] = input[24 + i];
			}
			return this.K;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007E40 File Offset: 0x00006E40
		private byte[] A(byte[] input)
		{
			for (int i = 0; i < 8; i++)
			{
				this.a[i] = (input[i] ^ input[i + 8]);
			}
			Array.Copy(input, 8, input, 0, 24);
			Array.Copy(this.a, 0, input, 24, 8);
			return input;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007E87 File Offset: 0x00006E87
		private void E(byte[] key, byte[] s, int sOff, byte[] input, int inOff)
		{
			this.cipher.Init(true, new KeyParameter(key));
			this.cipher.ProcessBlock(input, inOff, s, sOff);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007EB0 File Offset: 0x00006EB0
		private void fw(byte[] input)
		{
			Gost3411Digest.cpyBytesToShort(input, this.wS);
			this.w_S[15] = (this.wS[0] ^ this.wS[1] ^ this.wS[2] ^ this.wS[3] ^ this.wS[12] ^ this.wS[15]);
			Array.Copy(this.wS, 1, this.w_S, 0, 15);
			Gost3411Digest.cpyShortToBytes(this.w_S, input);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007F2C File Offset: 0x00006F2C
		private void processBlock(byte[] input, int inOff)
		{
			Array.Copy(input, inOff, this.M, 0, 32);
			this.H.CopyTo(this.U, 0);
			this.M.CopyTo(this.V, 0);
			for (int i = 0; i < 32; i++)
			{
				this.W[i] = (this.U[i] ^ this.V[i]);
			}
			this.E(this.P(this.W), this.S, 0, this.H, 0);
			for (int j = 1; j < 4; j++)
			{
				byte[] array = this.A(this.U);
				for (int k = 0; k < 32; k++)
				{
					this.U[k] = (array[k] ^ this.C[j][k]);
				}
				this.V = this.A(this.A(this.V));
				for (int l = 0; l < 32; l++)
				{
					this.W[l] = (this.U[l] ^ this.V[l]);
				}
				this.E(this.P(this.W), this.S, j * 8, this.H, j * 8);
			}
			for (int m = 0; m < 12; m++)
			{
				this.fw(this.S);
			}
			for (int n = 0; n < 32; n++)
			{
				this.S[n] = (this.S[n] ^ this.M[n]);
			}
			this.fw(this.S);
			for (int num = 0; num < 32; num++)
			{
				this.S[num] = (this.H[num] ^ this.S[num]);
			}
			for (int num2 = 0; num2 < 61; num2++)
			{
				this.fw(this.S);
			}
			Array.Copy(this.S, 0, this.H, 0, this.H.Length);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008120 File Offset: 0x00007120
		private void finish()
		{
			Gost3411Digest.LongToBytes(this.byteCount * 8L, this.L, 0);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.processBlock(this.L, 0);
			this.processBlock(this.Sum, 0);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000816D File Offset: 0x0000716D
		public int DoFinal(byte[] output, int outOff)
		{
			this.finish();
			this.H.CopyTo(output, outOff);
			this.Reset();
			return 32;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000818C File Offset: 0x0000718C
		public void Reset()
		{
			this.byteCount = 0L;
			this.xBufOff = 0;
			Array.Clear(this.H, 0, this.H.Length);
			Array.Clear(this.L, 0, this.L.Length);
			Array.Clear(this.M, 0, this.M.Length);
			Array.Clear(this.C[1], 0, this.C[1].Length);
			Array.Clear(this.C[3], 0, this.C[3].Length);
			Array.Clear(this.Sum, 0, this.Sum.Length);
			Array.Clear(this.xBuf, 0, this.xBuf.Length);
			Gost3411Digest.C2.CopyTo(this.C[2], 0);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008250 File Offset: 0x00007250
		private void sumByteArray(byte[] input)
		{
			int num = 0;
			for (int num2 = 0; num2 != this.Sum.Length; num2++)
			{
				int num3 = (int)((this.Sum[num2] & byte.MaxValue) + (input[num2] & byte.MaxValue)) + num;
				this.Sum[num2] = (byte)num3;
				num = num3 >> 8;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000829C File Offset: 0x0000729C
		private static void LongToBytes(long r, byte[] output, int outOff)
		{
			output[outOff + 7] = (byte)(r >> 56);
			output[outOff + 6] = (byte)(r >> 48);
			output[outOff + 5] = (byte)(r >> 40);
			output[outOff + 4] = (byte)(r >> 32);
			output[outOff + 3] = (byte)(r >> 24);
			output[outOff + 2] = (byte)(r >> 16);
			output[outOff + 1] = (byte)(r >> 8);
			output[outOff] = (byte)r;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000082F4 File Offset: 0x000072F4
		private static void cpyBytesToShort(byte[] S, short[] wS)
		{
			for (int i = 0; i < S.Length / 2; i++)
			{
				wS[i] = (short)(((int)S[i * 2 + 1] << 8 & 65280) | (int)(S[i * 2] & byte.MaxValue));
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008330 File Offset: 0x00007330
		private static void cpyShortToBytes(short[] wS, byte[] S)
		{
			for (int i = 0; i < S.Length / 2; i++)
			{
				S[i * 2 + 1] = (byte)(wS[i] >> 8);
				S[i * 2] = (byte)wS[i];
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008363 File Offset: 0x00007363
		public int GetByteLength()
		{
			return 32;
		}

		// Token: 0x04000081 RID: 129
		private const int DIGEST_LENGTH = 32;

		// Token: 0x04000082 RID: 130
		private byte[] H = new byte[32];

		// Token: 0x04000083 RID: 131
		private byte[] L = new byte[32];

		// Token: 0x04000084 RID: 132
		private byte[] M = new byte[32];

		// Token: 0x04000085 RID: 133
		private byte[] Sum = new byte[32];

		// Token: 0x04000086 RID: 134
		private byte[][] C = new byte[4][];

		// Token: 0x04000087 RID: 135
		private byte[] xBuf = new byte[32];

		// Token: 0x04000088 RID: 136
		private int xBufOff;

		// Token: 0x04000089 RID: 137
		private long byteCount;

		// Token: 0x0400008A RID: 138
		private readonly IBlockCipher cipher = new Gost28147Engine();

		// Token: 0x0400008B RID: 139
		private byte[] K = new byte[32];

		// Token: 0x0400008C RID: 140
		private byte[] a = new byte[8];

		// Token: 0x0400008D RID: 141
		internal short[] wS = new short[16];

		// Token: 0x0400008E RID: 142
		internal short[] w_S = new short[16];

		// Token: 0x0400008F RID: 143
		internal byte[] S = new byte[32];

		// Token: 0x04000090 RID: 144
		internal byte[] U = new byte[32];

		// Token: 0x04000091 RID: 145
		internal byte[] V = new byte[32];

		// Token: 0x04000092 RID: 146
		internal byte[] W = new byte[32];

		// Token: 0x04000093 RID: 147
		private static readonly byte[] C2 = new byte[]
		{
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue
		};
	}
}
