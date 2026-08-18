using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020005AB RID: 1451
	public class RC6Engine : IBlockCipher
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x001379F4 File Offset: 0x001369F4
		public string AlgorithmName
		{
			get
			{
				return "RC6";
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x001379FB File Offset: 0x001369FB
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x001379FE File Offset: 0x001369FE
		public int GetBlockSize()
		{
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x00137A08 File Offset: 0x00136A08
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to RC6 init - " + parameters.GetType().ToString());
			}
			this.forEncryption = forEncryption;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.SetKey(keyParameter.GetKey());
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x00137A54 File Offset: 0x00136A54
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			int blockSize = this.GetBlockSize();
			if (this._S == null)
			{
				throw new InvalidOperationException("RC6 engine not initialised");
			}
			if (inOff + blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + blockSize > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x00137AC1 File Offset: 0x00136AC1
		public void Reset()
		{
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x00137AC4 File Offset: 0x00136AC4
		private void SetKey(byte[] key)
		{
			if ((key.Length + (RC6Engine.bytesPerWord - 1)) / RC6Engine.bytesPerWord == 0)
			{
			}
			int[] array = new int[(key.Length + RC6Engine.bytesPerWord - 1) / RC6Engine.bytesPerWord];
			for (int i = key.Length - 1; i >= 0; i--)
			{
				array[i / RC6Engine.bytesPerWord] = (array[i / RC6Engine.bytesPerWord] << 8) + (int)(key[i] & byte.MaxValue);
			}
			this._S = new int[2 + 2 * RC6Engine._noRounds + 2];
			this._S[0] = RC6Engine.P32;
			for (int j = 1; j < this._S.Length; j++)
			{
				this._S[j] = this._S[j - 1] + RC6Engine.Q32;
			}
			int num;
			if (array.Length > this._S.Length)
			{
				num = 3 * array.Length;
			}
			else
			{
				num = 3 * this._S.Length;
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			for (int k = 0; k < num; k++)
			{
				num2 = (this._S[num4] = this.RotateLeft(this._S[num4] + num2 + num3, 3));
				num3 = (array[num5] = this.RotateLeft(array[num5] + num2 + num3, num2 + num3));
				num4 = (num4 + 1) % this._S.Length;
				num5 = (num5 + 1) % array.Length;
			}
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00137C1C File Offset: 0x00136C1C
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord);
			int num3 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 2);
			int num4 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 3);
			num2 += this._S[0];
			num4 += this._S[1];
			for (int i = 1; i <= RC6Engine._noRounds; i++)
			{
				int num5 = num2 * (2 * num2 + 1);
				num5 = this.RotateLeft(num5, 5);
				int num6 = num4 * (2 * num4 + 1);
				num6 = this.RotateLeft(num6, 5);
				num ^= num5;
				num = this.RotateLeft(num, num6);
				num += this._S[2 * i];
				num3 ^= num6;
				num3 = this.RotateLeft(num3, num5);
				num3 += this._S[2 * i + 1];
				int num7 = num;
				num = num2;
				num2 = num3;
				num3 = num4;
				num4 = num7;
			}
			num += this._S[2 * RC6Engine._noRounds + 2];
			num3 += this._S[2 * RC6Engine._noRounds + 3];
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC6Engine.bytesPerWord);
			this.WordToBytes(num3, outBytes, outOff + RC6Engine.bytesPerWord * 2);
			this.WordToBytes(num4, outBytes, outOff + RC6Engine.bytesPerWord * 3);
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00137D74 File Offset: 0x00136D74
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord);
			int num3 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 2);
			int num4 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 3);
			num3 -= this._S[2 * RC6Engine._noRounds + 3];
			num -= this._S[2 * RC6Engine._noRounds + 2];
			for (int i = RC6Engine._noRounds; i >= 1; i--)
			{
				int num5 = num4;
				num4 = num3;
				num3 = num2;
				num2 = num;
				num = num5;
				int num6 = num2 * (2 * num2 + 1);
				num6 = this.RotateLeft(num6, RC6Engine.LGW);
				int num7 = num4 * (2 * num4 + 1);
				num7 = this.RotateLeft(num7, RC6Engine.LGW);
				num3 -= this._S[2 * i + 1];
				num3 = this.RotateRight(num3, num6);
				num3 ^= num7;
				num -= this._S[2 * i];
				num = this.RotateRight(num, num7);
				num ^= num6;
			}
			num4 -= this._S[1];
			num2 -= this._S[0];
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC6Engine.bytesPerWord);
			this.WordToBytes(num3, outBytes, outOff + RC6Engine.bytesPerWord * 2);
			this.WordToBytes(num4, outBytes, outOff + RC6Engine.bytesPerWord * 3);
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x00137ED4 File Offset: 0x00136ED4
		private int RotateLeft(int x, int y)
		{
			return x << (y & RC6Engine.wordSize - 1) | (int)((uint)x >> RC6Engine.wordSize - (y & RC6Engine.wordSize - 1));
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x00137EF9 File Offset: 0x00136EF9
		private int RotateRight(int x, int y)
		{
			return (int)((uint)x >> (y & RC6Engine.wordSize - 1) | (uint)((uint)x << RC6Engine.wordSize - (y & RC6Engine.wordSize - 1)));
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x00137F20 File Offset: 0x00136F20
		private int BytesToWord(byte[] src, int srcOff)
		{
			int num = 0;
			for (int i = RC6Engine.bytesPerWord - 1; i >= 0; i--)
			{
				num = (num << 8) + (int)(src[i + srcOff] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x00137F54 File Offset: 0x00136F54
		private void WordToBytes(int word, byte[] dst, int dstOff)
		{
			for (int i = 0; i < RC6Engine.bytesPerWord; i++)
			{
				dst[i + dstOff] = (byte)word;
				word = (int)((uint)word >> 8);
			}
		}

		// Token: 0x0400225B RID: 8795
		private static readonly int wordSize = 32;

		// Token: 0x0400225C RID: 8796
		private static readonly int bytesPerWord = RC6Engine.wordSize / 8;

		// Token: 0x0400225D RID: 8797
		private static readonly int _noRounds = 20;

		// Token: 0x0400225E RID: 8798
		private int[] _S;

		// Token: 0x0400225F RID: 8799
		private static readonly int P32 = -1209970333;

		// Token: 0x04002260 RID: 8800
		private static readonly int Q32 = -1640531527;

		// Token: 0x04002261 RID: 8801
		private static readonly int LGW = 5;

		// Token: 0x04002262 RID: 8802
		private bool forEncryption;
	}
}
