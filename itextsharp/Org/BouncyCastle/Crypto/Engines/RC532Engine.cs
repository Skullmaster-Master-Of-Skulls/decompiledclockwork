using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020005AC RID: 1452
	public class RC532Engine : IBlockCipher
	{
		// Token: 0x0600321B RID: 12827 RVA: 0x00137FB3 File Offset: 0x00136FB3
		public RC532Engine()
		{
			this._noRounds = 12;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x00137FC3 File Offset: 0x00136FC3
		public string AlgorithmName
		{
			get
			{
				return "RC5-32";
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x00137FCA File Offset: 0x00136FCA
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x00137FCD File Offset: 0x00136FCD
		public int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x00137FD0 File Offset: 0x00136FD0
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (typeof(RC5Parameters).IsInstanceOfType(parameters))
			{
				RC5Parameters rc5Parameters = (RC5Parameters)parameters;
				this._noRounds = rc5Parameters.Rounds;
				this.SetKey(rc5Parameters.GetKey());
			}
			else
			{
				if (!typeof(KeyParameter).IsInstanceOfType(parameters))
				{
					throw new ArgumentException("invalid parameter passed to RC532 init - " + parameters.GetType().ToString());
				}
				KeyParameter keyParameter = (KeyParameter)parameters;
				this.SetKey(keyParameter.GetKey());
			}
			this.forEncryption = forEncryption;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x00138059 File Offset: 0x00137059
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x0013807A File Offset: 0x0013707A
		public void Reset()
		{
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x0013807C File Offset: 0x0013707C
		private void SetKey(byte[] key)
		{
			int[] array = new int[(key.Length + 3) / 4];
			for (int num = 0; num != key.Length; num++)
			{
				array[num / 4] += (int)(key[num] & byte.MaxValue) << 8 * (num % 4);
			}
			this._S = new int[2 * (this._noRounds + 1)];
			this._S[0] = RC532Engine.P32;
			for (int i = 1; i < this._S.Length; i++)
			{
				this._S[i] = this._S[i - 1] + RC532Engine.Q32;
			}
			int num2;
			if (array.Length > this._S.Length)
			{
				num2 = 3 * array.Length;
			}
			else
			{
				num2 = 3 * this._S.Length;
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int j = 0; j < num2; j++)
			{
				num3 = (this._S[num5] = this.RotateLeft(this._S[num5] + num3 + num4, 3));
				num4 = (array[num6] = this.RotateLeft(array[num6] + num3 + num4, num3 + num4));
				num5 = (num5 + 1) % this._S.Length;
				num6 = (num6 + 1) % array.Length;
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x001381B4 File Offset: 0x001371B4
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff) + this._S[0];
			int num2 = this.BytesToWord(input, inOff + 4) + this._S[1];
			for (int i = 1; i <= this._noRounds; i++)
			{
				num = this.RotateLeft(num ^ num2, num2) + this._S[2 * i];
				num2 = this.RotateLeft(num2 ^ num, num) + this._S[2 * i + 1];
			}
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x00138240 File Offset: 0x00137240
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + 4);
			for (int i = this._noRounds; i >= 1; i--)
			{
				num2 = (this.RotateRight(num2 - this._S[2 * i + 1], num) ^ num);
				num = (this.RotateRight(num - this._S[2 * i], num2) ^ num2);
			}
			this.WordToBytes(num - this._S[0], outBytes, outOff);
			this.WordToBytes(num2 - this._S[1], outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x001382C9 File Offset: 0x001372C9
		private int RotateLeft(int x, int y)
		{
			return x << y | (int)((uint)x >> 32 - (y & 31));
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x001382E1 File Offset: 0x001372E1
		private int RotateRight(int x, int y)
		{
			return (int)((uint)x >> y | (uint)((uint)x << 32 - (y & 31)));
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x001382F9 File Offset: 0x001372F9
		private int BytesToWord(byte[] src, int srcOff)
		{
			return (int)(src[srcOff] & byte.MaxValue) | (int)(src[srcOff + 1] & byte.MaxValue) << 8 | (int)(src[srcOff + 2] & byte.MaxValue) << 16 | (int)(src[srcOff + 3] & byte.MaxValue) << 24;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x00138330 File Offset: 0x00137330
		private void WordToBytes(int word, byte[] dst, int dstOff)
		{
			dst[dstOff] = (byte)word;
			dst[dstOff + 1] = (byte)(word >> 8);
			dst[dstOff + 2] = (byte)(word >> 16);
			dst[dstOff + 3] = (byte)(word >> 24);
		}

		// Token: 0x04002263 RID: 8803
		private int _noRounds;

		// Token: 0x04002264 RID: 8804
		private int[] _S;

		// Token: 0x04002265 RID: 8805
		private static readonly int P32 = -1209970333;

		// Token: 0x04002266 RID: 8806
		private static readonly int Q32 = -1640531527;

		// Token: 0x04002267 RID: 8807
		private bool forEncryption;
	}
}
