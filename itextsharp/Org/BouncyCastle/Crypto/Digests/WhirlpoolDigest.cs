using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000090 RID: 144
	public sealed class WhirlpoolDigest : IDigest
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x00018B98 File Offset: 0x00017B98
		static WhirlpoolDigest()
		{
			WhirlpoolDigest.EIGHT[31] = 8;
			for (int i = 0; i < 256; i++)
			{
				int num = WhirlpoolDigest.SBOX[i];
				int num2 = WhirlpoolDigest.maskWithReductionPolynomial(num << 1);
				int num3 = WhirlpoolDigest.maskWithReductionPolynomial(num2 << 1);
				int num4 = num3 ^ num;
				int num5 = WhirlpoolDigest.maskWithReductionPolynomial(num3 << 1);
				int num6 = num5 ^ num;
				WhirlpoolDigest.C0[i] = WhirlpoolDigest.packIntoLong(num, num, num3, num, num5, num4, num2, num6);
				WhirlpoolDigest.C1[i] = WhirlpoolDigest.packIntoLong(num6, num, num, num3, num, num5, num4, num2);
				WhirlpoolDigest.C2[i] = WhirlpoolDigest.packIntoLong(num2, num6, num, num, num3, num, num5, num4);
				WhirlpoolDigest.C3[i] = WhirlpoolDigest.packIntoLong(num4, num2, num6, num, num, num3, num, num5);
				WhirlpoolDigest.C4[i] = WhirlpoolDigest.packIntoLong(num5, num4, num2, num6, num, num, num3, num);
				WhirlpoolDigest.C5[i] = WhirlpoolDigest.packIntoLong(num, num5, num4, num2, num6, num, num, num3);
				WhirlpoolDigest.C6[i] = WhirlpoolDigest.packIntoLong(num3, num, num5, num4, num2, num6, num, num);
				WhirlpoolDigest.C7[i] = WhirlpoolDigest.packIntoLong(num, num3, num, num5, num4, num2, num6, num);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00018D4C File Offset: 0x00017D4C
		public WhirlpoolDigest()
		{
			this._rc[0] = 0L;
			for (int i = 1; i <= 10; i++)
			{
				int num = 8 * (i - 1);
				this._rc[i] = ((WhirlpoolDigest.C0[num] & -72057594037927936L) ^ (WhirlpoolDigest.C1[num + 1] & 71776119061217280L) ^ (WhirlpoolDigest.C2[num + 2] & 280375465082880L) ^ (WhirlpoolDigest.C3[num + 3] & 1095216660480L) ^ (WhirlpoolDigest.C4[num + 4] & (long)((ulong)-16777216)) ^ (WhirlpoolDigest.C5[num + 5] & 16711680L) ^ (WhirlpoolDigest.C6[num + 6] & 65280L) ^ (WhirlpoolDigest.C7[num + 7] & 255L));
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00018E7E File Offset: 0x00017E7E
		private static long packIntoLong(int b7, int b6, int b5, int b4, int b3, int b2, int b1, int b0)
		{
			return (long)b7 << 56 ^ (long)b6 << 48 ^ (long)b5 << 40 ^ (long)b4 << 32 ^ (long)b3 << 24 ^ (long)b2 << 16 ^ (long)b1 << 8 ^ (long)b0;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00018EB0 File Offset: 0x00017EB0
		private static int maskWithReductionPolynomial(int input)
		{
			int num = input;
			if ((long)num >= 256L)
			{
				num ^= 285;
			}
			return num;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00018ED4 File Offset: 0x00017ED4
		public WhirlpoolDigest(WhirlpoolDigest originalDigest)
		{
			Array.Copy(originalDigest._rc, 0, this._rc, 0, this._rc.Length);
			Array.Copy(originalDigest._buffer, 0, this._buffer, 0, this._buffer.Length);
			this._bufferPos = originalDigest._bufferPos;
			Array.Copy(originalDigest._bitCount, 0, this._bitCount, 0, this._bitCount.Length);
			Array.Copy(originalDigest._hash, 0, this._hash, 0, this._hash.Length);
			Array.Copy(originalDigest._K, 0, this._K, 0, this._K.Length);
			Array.Copy(originalDigest._L, 0, this._L, 0, this._L.Length);
			Array.Copy(originalDigest._block, 0, this._block, 0, this._block.Length);
			Array.Copy(originalDigest._state, 0, this._state, 0, this._state.Length);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001902E File Offset: 0x0001802E
		public string AlgorithmName
		{
			get
			{
				return "Whirlpool";
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00019035 File Offset: 0x00018035
		public int GetDigestSize()
		{
			return 64;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001903C File Offset: 0x0001803C
		public int DoFinal(byte[] output, int outOff)
		{
			this.finish();
			for (int i = 0; i < 8; i++)
			{
				WhirlpoolDigest.convertLongToByteArray(this._hash[i], output, outOff + i * 8);
			}
			this.Reset();
			return this.GetDigestSize();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001907C File Offset: 0x0001807C
		public void Reset()
		{
			this._bufferPos = 0;
			Array.Clear(this._bitCount, 0, this._bitCount.Length);
			Array.Clear(this._buffer, 0, this._buffer.Length);
			Array.Clear(this._hash, 0, this._hash.Length);
			Array.Clear(this._K, 0, this._K.Length);
			Array.Clear(this._L, 0, this._L.Length);
			Array.Clear(this._block, 0, this._block.Length);
			Array.Clear(this._state, 0, this._state.Length);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001911C File Offset: 0x0001811C
		private void processFilledBuffer()
		{
			for (int i = 0; i < this._state.Length; i++)
			{
				this._block[i] = WhirlpoolDigest.bytesToLongFromBuffer(this._buffer, i * 8);
			}
			this.processBlock();
			this._bufferPos = 0;
			Array.Clear(this._buffer, 0, this._buffer.Length);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00019174 File Offset: 0x00018174
		private static long bytesToLongFromBuffer(byte[] buffer, int startPos)
		{
			return (long)(((ulong)buffer[startPos] & 255UL) << 56 | ((ulong)buffer[startPos + 1] & 255UL) << 48 | ((ulong)buffer[startPos + 2] & 255UL) << 40 | ((ulong)buffer[startPos + 3] & 255UL) << 32 | ((ulong)buffer[startPos + 4] & 255UL) << 24 | ((ulong)buffer[startPos + 5] & 255UL) << 16 | ((ulong)buffer[startPos + 6] & 255UL) << 8 | ((ulong)buffer[startPos + 7] & 255UL));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00019204 File Offset: 0x00018204
		private static void convertLongToByteArray(long inputLong, byte[] outputArray, int offSet)
		{
			for (int i = 0; i < 8; i++)
			{
				outputArray[offSet + i] = (byte)(inputLong >> 56 - i * 8 & 255L);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00019238 File Offset: 0x00018238
		private void processBlock()
		{
			for (int i = 0; i < 8; i++)
			{
				this._state[i] = (this._block[i] ^ (this._K[i] = this._hash[i]));
			}
			for (int j = 1; j <= 10; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					this._L[k] = 0L;
					this._L[k] ^= WhirlpoolDigest.C0[(int)(this._K[k & 7] >> 56) & 255];
					this._L[k] ^= WhirlpoolDigest.C1[(int)(this._K[k - 1 & 7] >> 48) & 255];
					this._L[k] ^= WhirlpoolDigest.C2[(int)(this._K[k - 2 & 7] >> 40) & 255];
					this._L[k] ^= WhirlpoolDigest.C3[(int)(this._K[k - 3 & 7] >> 32) & 255];
					this._L[k] ^= WhirlpoolDigest.C4[(int)(this._K[k - 4 & 7] >> 24) & 255];
					this._L[k] ^= WhirlpoolDigest.C5[(int)(this._K[k - 5 & 7] >> 16) & 255];
					this._L[k] ^= WhirlpoolDigest.C6[(int)(this._K[k - 6 & 7] >> 8) & 255];
					this._L[k] ^= WhirlpoolDigest.C7[(int)this._K[k - 7 & 7] & 255];
				}
				Array.Copy(this._L, 0, this._K, 0, this._K.Length);
				this._K[0] ^= this._rc[j];
				for (int l = 0; l < 8; l++)
				{
					this._L[l] = this._K[l];
					this._L[l] ^= WhirlpoolDigest.C0[(int)(this._state[l & 7] >> 56) & 255];
					this._L[l] ^= WhirlpoolDigest.C1[(int)(this._state[l - 1 & 7] >> 48) & 255];
					this._L[l] ^= WhirlpoolDigest.C2[(int)(this._state[l - 2 & 7] >> 40) & 255];
					this._L[l] ^= WhirlpoolDigest.C3[(int)(this._state[l - 3 & 7] >> 32) & 255];
					this._L[l] ^= WhirlpoolDigest.C4[(int)(this._state[l - 4 & 7] >> 24) & 255];
					this._L[l] ^= WhirlpoolDigest.C5[(int)(this._state[l - 5 & 7] >> 16) & 255];
					this._L[l] ^= WhirlpoolDigest.C6[(int)(this._state[l - 6 & 7] >> 8) & 255];
					this._L[l] ^= WhirlpoolDigest.C7[(int)this._state[l - 7 & 7] & 255];
				}
				Array.Copy(this._L, 0, this._state, 0, this._state.Length);
			}
			for (int m = 0; m < 8; m++)
			{
				this._hash[m] ^= (this._state[m] ^ this._block[m]);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001968E File Offset: 0x0001868E
		public void Update(byte input)
		{
			this._buffer[this._bufferPos] = input;
			this._bufferPos++;
			if (this._bufferPos == this._buffer.Length)
			{
				this.processFilledBuffer();
			}
			this.increment();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000196C8 File Offset: 0x000186C8
		private void increment()
		{
			int num = 0;
			for (int i = this._bitCount.Length - 1; i >= 0; i--)
			{
				int num2 = (int)((this._bitCount[i] & 255) + WhirlpoolDigest.EIGHT[i]) + num;
				num = num2 >> 8;
				this._bitCount[i] = (short)(num2 & 255);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00019719 File Offset: 0x00018719
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00019734 File Offset: 0x00018734
		private void finish()
		{
			byte[] array = this.copyBitLength();
			byte[] buffer = this._buffer;
			int num = this._bufferPos++;
			buffer[num] |= 128;
			if (this._bufferPos == this._buffer.Length)
			{
				this.processFilledBuffer();
			}
			if (this._bufferPos > 32)
			{
				while (this._bufferPos != 0)
				{
					this.Update(0);
				}
			}
			while (this._bufferPos <= 32)
			{
				this.Update(0);
			}
			Array.Copy(array, 0, this._buffer, 32, array.Length);
			this.processFilledBuffer();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000197D4 File Offset: 0x000187D4
		private byte[] copyBitLength()
		{
			byte[] array = new byte[32];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)(this._bitCount[i] & 255);
			}
			return array;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001980A File Offset: 0x0001880A
		public int GetByteLength()
		{
			return 64;
		}

		// Token: 0x0400023B RID: 571
		private const int BYTE_LENGTH = 64;

		// Token: 0x0400023C RID: 572
		private const int DIGEST_LENGTH_BYTES = 64;

		// Token: 0x0400023D RID: 573
		private const int ROUNDS = 10;

		// Token: 0x0400023E RID: 574
		private const int REDUCTION_POLYNOMIAL = 285;

		// Token: 0x0400023F RID: 575
		private const int BITCOUNT_ARRAY_SIZE = 32;

		// Token: 0x04000240 RID: 576
		private static readonly int[] SBOX = new int[]
		{
			24,
			35,
			198,
			232,
			135,
			184,
			1,
			79,
			54,
			166,
			210,
			245,
			121,
			111,
			145,
			82,
			96,
			188,
			155,
			142,
			163,
			12,
			123,
			53,
			29,
			224,
			215,
			194,
			46,
			75,
			254,
			87,
			21,
			119,
			55,
			229,
			159,
			240,
			74,
			218,
			88,
			201,
			41,
			10,
			177,
			160,
			107,
			133,
			189,
			93,
			16,
			244,
			203,
			62,
			5,
			103,
			228,
			39,
			65,
			139,
			167,
			125,
			149,
			216,
			251,
			238,
			124,
			102,
			221,
			23,
			71,
			158,
			202,
			45,
			191,
			7,
			173,
			90,
			131,
			51,
			99,
			2,
			170,
			113,
			200,
			25,
			73,
			217,
			242,
			227,
			91,
			136,
			154,
			38,
			50,
			176,
			233,
			15,
			213,
			128,
			190,
			205,
			52,
			72,
			255,
			122,
			144,
			95,
			32,
			104,
			26,
			174,
			180,
			84,
			147,
			34,
			100,
			241,
			115,
			18,
			64,
			8,
			195,
			236,
			219,
			161,
			141,
			61,
			151,
			0,
			207,
			43,
			118,
			130,
			214,
			27,
			181,
			175,
			106,
			80,
			69,
			243,
			48,
			239,
			63,
			85,
			162,
			234,
			101,
			186,
			47,
			192,
			222,
			28,
			253,
			77,
			146,
			117,
			6,
			138,
			178,
			230,
			14,
			31,
			98,
			212,
			168,
			150,
			249,
			197,
			37,
			89,
			132,
			114,
			57,
			76,
			94,
			120,
			56,
			140,
			209,
			165,
			226,
			97,
			179,
			33,
			156,
			30,
			67,
			199,
			252,
			4,
			81,
			153,
			109,
			13,
			250,
			223,
			126,
			36,
			59,
			171,
			206,
			17,
			143,
			78,
			183,
			235,
			60,
			129,
			148,
			247,
			185,
			19,
			44,
			211,
			231,
			110,
			196,
			3,
			86,
			68,
			127,
			169,
			42,
			187,
			193,
			83,
			220,
			11,
			157,
			108,
			49,
			116,
			246,
			70,
			172,
			137,
			20,
			225,
			22,
			58,
			105,
			9,
			112,
			182,
			208,
			237,
			204,
			66,
			152,
			164,
			40,
			92,
			248,
			134
		};

		// Token: 0x04000241 RID: 577
		private static readonly long[] C0 = new long[256];

		// Token: 0x04000242 RID: 578
		private static readonly long[] C1 = new long[256];

		// Token: 0x04000243 RID: 579
		private static readonly long[] C2 = new long[256];

		// Token: 0x04000244 RID: 580
		private static readonly long[] C3 = new long[256];

		// Token: 0x04000245 RID: 581
		private static readonly long[] C4 = new long[256];

		// Token: 0x04000246 RID: 582
		private static readonly long[] C5 = new long[256];

		// Token: 0x04000247 RID: 583
		private static readonly long[] C6 = new long[256];

		// Token: 0x04000248 RID: 584
		private static readonly long[] C7 = new long[256];

		// Token: 0x04000249 RID: 585
		private readonly long[] _rc = new long[11];

		// Token: 0x0400024A RID: 586
		private static readonly short[] EIGHT = new short[32];

		// Token: 0x0400024B RID: 587
		private byte[] _buffer = new byte[64];

		// Token: 0x0400024C RID: 588
		private int _bufferPos;

		// Token: 0x0400024D RID: 589
		private short[] _bitCount = new short[32];

		// Token: 0x0400024E RID: 590
		private long[] _hash = new long[8];

		// Token: 0x0400024F RID: 591
		private long[] _K = new long[8];

		// Token: 0x04000250 RID: 592
		private long[] _L = new long[8];

		// Token: 0x04000251 RID: 593
		private long[] _block = new long[8];

		// Token: 0x04000252 RID: 594
		private long[] _state = new long[8];
	}
}
