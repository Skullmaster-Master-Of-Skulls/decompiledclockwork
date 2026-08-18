using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers
{
	// Token: 0x0200008E RID: 142
	public sealed class TwofishCipher : BlockCipher
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		public TwofishCipher(byte[] key, CipherMode mode, CipherPadding padding) : base(key, 16, mode, padding)
		{
			int num = key.Length * 8;
			if (num != 128 && num != 192 && num != 256)
			{
				throw new ArgumentException(string.Format("KeySize '{0}' is not valid for this algorithm.", num));
			}
			int[] array = new int[2];
			int[] array2 = new int[2];
			int[] array3 = new int[2];
			for (int i = 0; i < 256; i++)
			{
				int num2 = (int)(TwofishCipher.P[i] & byte.MaxValue);
				array[0] = num2;
				array2[0] = (TwofishCipher.Mx_X(num2) & 255);
				array3[0] = (TwofishCipher.Mx_Y(num2) & 255);
				num2 = (int)(TwofishCipher.P[256 + i] & byte.MaxValue);
				array[1] = num2;
				array2[1] = (TwofishCipher.Mx_X(num2) & 255);
				array3[1] = (TwofishCipher.Mx_Y(num2) & 255);
				this.gMDS0[i] = (array[1] | array2[1] << 8 | array3[1] << 16 | array3[1] << 24);
				this.gMDS1[i] = (array3[0] | array3[0] << 8 | array2[0] << 16 | array[0] << 24);
				this.gMDS2[i] = (array2[1] | array3[1] << 8 | array[1] << 16 | array3[1] << 24);
				this.gMDS3[i] = (array2[0] | array[0] << 8 | array3[0] << 16 | array2[0] << 24);
			}
			this._k64Cnt = key.Length / 8;
			this.SetKey(key);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001C0B8 File Offset: 0x0001A2B8
		public override int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset) ^ this.gSubKeys[0];
			int num2 = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset + 4) ^ this.gSubKeys[1];
			int num3 = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset + 8) ^ this.gSubKeys[2];
			int num4 = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset + 12) ^ this.gSubKeys[3];
			int num5 = 8;
			for (int i = 0; i < 16; i += 2)
			{
				int num6 = TwofishCipher.Fe32_0(this.gSBox, num);
				int num7 = TwofishCipher.Fe32_3(this.gSBox, num2);
				num3 ^= num6 + num7 + this.gSubKeys[num5++];
				num3 = (int)((uint)num3 >> 1 | (uint)((uint)num3 << 31));
				num4 = ((num4 << 1 | (int)((uint)num4 >> 31)) ^ num6 + 2 * num7 + this.gSubKeys[num5++]);
				num6 = TwofishCipher.Fe32_0(this.gSBox, num3);
				num7 = TwofishCipher.Fe32_3(this.gSBox, num4);
				num ^= num6 + num7 + this.gSubKeys[num5++];
				num = (int)((uint)num >> 1 | (uint)((uint)num << 31));
				num2 = ((num2 << 1 | (int)((uint)num2 >> 31)) ^ num6 + 2 * num7 + this.gSubKeys[num5++]);
			}
			TwofishCipher.Bits32ToBytes(num3 ^ this.gSubKeys[4], outputBuffer, outputOffset);
			TwofishCipher.Bits32ToBytes(num4 ^ this.gSubKeys[5], outputBuffer, outputOffset + 4);
			TwofishCipher.Bits32ToBytes(num ^ this.gSubKeys[6], outputBuffer, outputOffset + 8);
			TwofishCipher.Bits32ToBytes(num2 ^ this.gSubKeys[7], outputBuffer, outputOffset + 12);
			return (int)base.BlockSize;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001C23C File Offset: 0x0001A43C
		public override int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset) ^ this.gSubKeys[4];
			int num2 = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset + 4) ^ this.gSubKeys[5];
			int num3 = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset + 8) ^ this.gSubKeys[6];
			int num4 = TwofishCipher.BytesTo32Bits(inputBuffer, inputOffset + 12) ^ this.gSubKeys[7];
			int num5 = 39;
			for (int i = 0; i < 16; i += 2)
			{
				int num6 = TwofishCipher.Fe32_0(this.gSBox, num);
				int num7 = TwofishCipher.Fe32_3(this.gSBox, num2);
				num4 ^= num6 + 2 * num7 + this.gSubKeys[num5--];
				num3 = ((num3 << 1 | (int)((uint)num3 >> 31)) ^ num6 + num7 + this.gSubKeys[num5--]);
				num4 = (int)((uint)num4 >> 1 | (uint)((uint)num4 << 31));
				num6 = TwofishCipher.Fe32_0(this.gSBox, num3);
				num7 = TwofishCipher.Fe32_3(this.gSBox, num4);
				num2 ^= num6 + 2 * num7 + this.gSubKeys[num5--];
				num = ((num << 1 | (int)((uint)num >> 31)) ^ num6 + num7 + this.gSubKeys[num5--]);
				num2 = (int)((uint)num2 >> 1 | (uint)((uint)num2 << 31));
			}
			TwofishCipher.Bits32ToBytes(num3 ^ this.gSubKeys[0], outputBuffer, outputOffset);
			TwofishCipher.Bits32ToBytes(num4 ^ this.gSubKeys[1], outputBuffer, outputOffset + 4);
			TwofishCipher.Bits32ToBytes(num ^ this.gSubKeys[2], outputBuffer, outputOffset + 8);
			TwofishCipher.Bits32ToBytes(num2 ^ this.gSubKeys[3], outputBuffer, outputOffset + 12);
			return (int)base.BlockSize;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		private void SetKey(byte[] key)
		{
			int[] array = new int[4];
			int[] array2 = new int[4];
			int[] array3 = new int[4];
			this.gSubKeys = new int[40];
			if (this._k64Cnt < 1)
			{
				throw new ArgumentException("Key size less than 64 bits");
			}
			if (this._k64Cnt > 4)
			{
				throw new ArgumentException("Key size larger than 256 bits");
			}
			for (int i = 0; i < this._k64Cnt; i++)
			{
				int num = i * 8;
				array[i] = TwofishCipher.BytesTo32Bits(key, num);
				array2[i] = TwofishCipher.BytesTo32Bits(key, num + 4);
				array3[this._k64Cnt - 1 - i] = TwofishCipher.RS_MDS_Encode(array[i], array2[i]);
			}
			for (int j = 0; j < 20; j++)
			{
				int num2 = j * 33686018;
				int num3 = this.F32(num2, array);
				int num4 = this.F32(num2 + 16843009, array2);
				num4 = (num4 << 8 | (int)((uint)num4 >> 24));
				num3 += num4;
				this.gSubKeys[j * 2] = num3;
				num3 += num4;
				this.gSubKeys[j * 2 + 1] = (num3 << 9 | (int)((uint)num3 >> 23));
			}
			int x = array3[0];
			int x2 = array3[1];
			int x3 = array3[2];
			int x4 = array3[3];
			this.gSBox = new int[1024];
			int k = 0;
			while (k < 256)
			{
				int num8;
				int num7;
				int num6;
				int num5 = num6 = (num7 = (num8 = k));
				switch (this._k64Cnt & 3)
				{
				case 0:
					num6 = ((int)(TwofishCipher.P[256 + num6] & byte.MaxValue) ^ TwofishCipher.M_b0(x4));
					num5 = ((int)(TwofishCipher.P[num5] & byte.MaxValue) ^ TwofishCipher.M_b1(x4));
					num7 = ((int)(TwofishCipher.P[num7] & byte.MaxValue) ^ TwofishCipher.M_b2(x4));
					num8 = ((int)(TwofishCipher.P[256 + num8] & byte.MaxValue) ^ TwofishCipher.M_b3(x4));
					goto IL_294;
				case 1:
					this.gSBox[k * 2] = this.gMDS0[(int)(TwofishCipher.P[num6] & byte.MaxValue) ^ TwofishCipher.M_b0(x)];
					this.gSBox[k * 2 + 1] = this.gMDS1[(int)(TwofishCipher.P[num5] & byte.MaxValue) ^ TwofishCipher.M_b1(x)];
					this.gSBox[k * 2 + 512] = this.gMDS2[(int)(TwofishCipher.P[256 + num7] & byte.MaxValue) ^ TwofishCipher.M_b2(x)];
					this.gSBox[k * 2 + 513] = this.gMDS3[(int)(TwofishCipher.P[256 + num8] & byte.MaxValue) ^ TwofishCipher.M_b3(x)];
					break;
				case 2:
					goto IL_300;
				case 3:
					goto IL_294;
				}
				IL_412:
				k++;
				continue;
				IL_300:
				this.gSBox[k * 2] = this.gMDS0[(int)(TwofishCipher.P[(int)(TwofishCipher.P[num6] & byte.MaxValue) ^ TwofishCipher.M_b0(x2)] & byte.MaxValue) ^ TwofishCipher.M_b0(x)];
				this.gSBox[k * 2 + 1] = this.gMDS1[(int)(TwofishCipher.P[(int)(TwofishCipher.P[256 + num5] & byte.MaxValue) ^ TwofishCipher.M_b1(x2)] & byte.MaxValue) ^ TwofishCipher.M_b1(x)];
				this.gSBox[k * 2 + 512] = this.gMDS2[(int)(TwofishCipher.P[256 + (int)(TwofishCipher.P[num7] & byte.MaxValue) ^ TwofishCipher.M_b2(x2)] & byte.MaxValue) ^ TwofishCipher.M_b2(x)];
				this.gSBox[k * 2 + 513] = this.gMDS3[(int)(TwofishCipher.P[256 + (int)(TwofishCipher.P[256 + num8] & byte.MaxValue) ^ TwofishCipher.M_b3(x2)] & byte.MaxValue) ^ TwofishCipher.M_b3(x)];
				goto IL_412;
				IL_294:
				num6 = ((int)(TwofishCipher.P[256 + num6] & byte.MaxValue) ^ TwofishCipher.M_b0(x3));
				num5 = ((int)(TwofishCipher.P[256 + num5] & byte.MaxValue) ^ TwofishCipher.M_b1(x3));
				num7 = ((int)(TwofishCipher.P[num7] & byte.MaxValue) ^ TwofishCipher.M_b2(x3));
				num8 = ((int)(TwofishCipher.P[num8] & byte.MaxValue) ^ TwofishCipher.M_b3(x3));
				goto IL_300;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		private int F32(int x, int[] k32)
		{
			int num = TwofishCipher.M_b0(x);
			int num2 = TwofishCipher.M_b1(x);
			int num3 = TwofishCipher.M_b2(x);
			int num4 = TwofishCipher.M_b3(x);
			int x2 = k32[0];
			int x3 = k32[1];
			int x4 = k32[2];
			int x5 = k32[3];
			int result = 0;
			switch (this._k64Cnt & 3)
			{
			case 0:
				num = ((int)(TwofishCipher.P[256 + num] & byte.MaxValue) ^ TwofishCipher.M_b0(x5));
				num2 = ((int)(TwofishCipher.P[num2] & byte.MaxValue) ^ TwofishCipher.M_b1(x5));
				num3 = ((int)(TwofishCipher.P[num3] & byte.MaxValue) ^ TwofishCipher.M_b2(x5));
				num4 = ((int)(TwofishCipher.P[256 + num4] & byte.MaxValue) ^ TwofishCipher.M_b3(x5));
				break;
			case 1:
				return this.gMDS0[(int)(TwofishCipher.P[num] & byte.MaxValue) ^ TwofishCipher.M_b0(x2)] ^ this.gMDS1[(int)(TwofishCipher.P[num2] & byte.MaxValue) ^ TwofishCipher.M_b1(x2)] ^ this.gMDS2[(int)(TwofishCipher.P[256 + num3] & byte.MaxValue) ^ TwofishCipher.M_b2(x2)] ^ this.gMDS3[(int)(TwofishCipher.P[256 + num4] & byte.MaxValue) ^ TwofishCipher.M_b3(x2)];
			case 2:
				goto IL_1A7;
			case 3:
				break;
			default:
				return result;
			}
			num = ((int)(TwofishCipher.P[256 + num] & byte.MaxValue) ^ TwofishCipher.M_b0(x4));
			num2 = ((int)(TwofishCipher.P[256 + num2] & byte.MaxValue) ^ TwofishCipher.M_b1(x4));
			num3 = ((int)(TwofishCipher.P[num3] & byte.MaxValue) ^ TwofishCipher.M_b2(x4));
			num4 = ((int)(TwofishCipher.P[num4] & byte.MaxValue) ^ TwofishCipher.M_b3(x4));
			IL_1A7:
			result = (this.gMDS0[(int)(TwofishCipher.P[(int)(TwofishCipher.P[num] & byte.MaxValue) ^ TwofishCipher.M_b0(x3)] & byte.MaxValue) ^ TwofishCipher.M_b0(x2)] ^ this.gMDS1[(int)(TwofishCipher.P[(int)(TwofishCipher.P[256 + num2] & byte.MaxValue) ^ TwofishCipher.M_b1(x3)] & byte.MaxValue) ^ TwofishCipher.M_b1(x2)] ^ this.gMDS2[(int)(TwofishCipher.P[256 + (int)(TwofishCipher.P[num3] & byte.MaxValue) ^ TwofishCipher.M_b2(x3)] & byte.MaxValue) ^ TwofishCipher.M_b2(x2)] ^ this.gMDS3[(int)(TwofishCipher.P[256 + (int)(TwofishCipher.P[256 + num4] & byte.MaxValue) ^ TwofishCipher.M_b3(x3)] & byte.MaxValue) ^ TwofishCipher.M_b3(x2)]);
			return result;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001CA87 File Offset: 0x0001AC87
		private static int RS_MDS_Encode(int k0, int k1)
		{
			return TwofishCipher.RS_rem(TwofishCipher.RS_rem(TwofishCipher.RS_rem(TwofishCipher.RS_rem(TwofishCipher.RS_rem(TwofishCipher.RS_rem(TwofishCipher.RS_rem(TwofishCipher.RS_rem(k1)))) ^ k0))));
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001CAB4 File Offset: 0x0001ACB4
		private static int RS_rem(int x)
		{
			int num = (int)((uint)x >> 24 & 255U);
			int num2 = (num << 1 ^ (((num & 128) != 0) ? 333 : 0)) & 255;
			int num3 = (int)((uint)num >> 1 ^ (((num & 1) != 0) ? 166U : 0U) ^ (uint)num2);
			return x << 8 ^ num3 << 24 ^ num2 << 16 ^ num3 << 8 ^ num;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001CB0F File Offset: 0x0001AD0F
		private static int LFSR1(int x)
		{
			return x >> 1 ^ (((x & 1) != 0) ? 180 : 0);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001CB22 File Offset: 0x0001AD22
		private static int LFSR2(int x)
		{
			return x >> 2 ^ (((x & 2) != 0) ? 180 : 0) ^ (((x & 1) != 0) ? 90 : 0);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001CB40 File Offset: 0x0001AD40
		private static int Mx_X(int x)
		{
			return x ^ TwofishCipher.LFSR2(x);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001CB4A File Offset: 0x0001AD4A
		private static int Mx_Y(int x)
		{
			return x ^ TwofishCipher.LFSR1(x) ^ TwofishCipher.LFSR2(x);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001CB5B File Offset: 0x0001AD5B
		private static int M_b0(int x)
		{
			return x & 255;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001CB64 File Offset: 0x0001AD64
		private static int M_b1(int x)
		{
			return (int)((uint)x >> 8 & 255U);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001CB6F File Offset: 0x0001AD6F
		private static int M_b2(int x)
		{
			return (int)((uint)x >> 16 & 255U);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001CB7B File Offset: 0x0001AD7B
		private static int M_b3(int x)
		{
			return (int)((uint)x >> 24 & 255U);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001CB88 File Offset: 0x0001AD88
		private static int Fe32_0(int[] gSBox1, int x)
		{
			return gSBox1[2 * (x & 255)] ^ gSBox1[(int)(1U + 2U * ((uint)x >> 8 & 255U))] ^ gSBox1[(int)(512U + 2U * ((uint)x >> 16 & 255U))] ^ gSBox1[(int)(513U + 2U * ((uint)x >> 24 & 255U))];
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001CBDC File Offset: 0x0001ADDC
		private static int Fe32_3(int[] gSBox1, int x)
		{
			return gSBox1[(int)(2U * ((uint)x >> 24 & 255U))] ^ gSBox1[1 + 2 * (x & 255)] ^ gSBox1[(int)(512U + 2U * ((uint)x >> 8 & 255U))] ^ gSBox1[(int)(513U + 2U * ((uint)x >> 16 & 255U))];
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001CC2E File Offset: 0x0001AE2E
		private static int BytesTo32Bits(byte[] b, int p)
		{
			return (int)(b[p] & byte.MaxValue) | (int)(b[p + 1] & byte.MaxValue) << 8 | (int)(b[p + 2] & byte.MaxValue) << 16 | (int)(b[p + 3] & byte.MaxValue) << 24;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001CC65 File Offset: 0x0001AE65
		private static void Bits32ToBytes(int inData, byte[] b, int offset)
		{
			b[offset] = (byte)inData;
			b[offset + 1] = (byte)(inData >> 8);
			b[offset + 2] = (byte)(inData >> 16);
			b[offset + 3] = (byte)(inData >> 24);
		}

		// Token: 0x040002C2 RID: 706
		private static readonly byte[] P = new byte[]
		{
			169,
			103,
			179,
			232,
			4,
			253,
			163,
			118,
			154,
			146,
			128,
			120,
			228,
			221,
			209,
			56,
			13,
			198,
			53,
			152,
			24,
			247,
			236,
			108,
			67,
			117,
			55,
			38,
			250,
			19,
			148,
			72,
			242,
			208,
			139,
			48,
			132,
			84,
			223,
			35,
			25,
			91,
			61,
			89,
			243,
			174,
			162,
			130,
			99,
			1,
			131,
			46,
			217,
			81,
			155,
			124,
			166,
			235,
			165,
			190,
			22,
			12,
			227,
			97,
			192,
			140,
			58,
			245,
			115,
			44,
			37,
			11,
			187,
			78,
			137,
			107,
			83,
			106,
			180,
			241,
			225,
			230,
			189,
			69,
			226,
			244,
			182,
			102,
			204,
			149,
			3,
			86,
			212,
			28,
			30,
			215,
			251,
			195,
			142,
			181,
			233,
			207,
			191,
			186,
			234,
			119,
			57,
			175,
			51,
			201,
			98,
			113,
			129,
			121,
			9,
			173,
			36,
			205,
			249,
			216,
			229,
			197,
			185,
			77,
			68,
			8,
			134,
			231,
			161,
			29,
			170,
			237,
			6,
			112,
			178,
			210,
			65,
			123,
			160,
			17,
			49,
			194,
			39,
			144,
			32,
			246,
			96,
			byte.MaxValue,
			150,
			92,
			177,
			171,
			158,
			156,
			82,
			27,
			95,
			147,
			10,
			239,
			145,
			133,
			73,
			238,
			45,
			79,
			143,
			59,
			71,
			135,
			109,
			70,
			214,
			62,
			105,
			100,
			42,
			206,
			203,
			47,
			252,
			151,
			5,
			122,
			172,
			127,
			213,
			26,
			75,
			14,
			167,
			90,
			40,
			20,
			63,
			41,
			136,
			60,
			76,
			2,
			184,
			218,
			176,
			23,
			85,
			31,
			138,
			125,
			87,
			199,
			141,
			116,
			183,
			196,
			159,
			114,
			126,
			21,
			34,
			18,
			88,
			7,
			153,
			52,
			110,
			80,
			222,
			104,
			101,
			188,
			219,
			248,
			200,
			168,
			43,
			64,
			220,
			254,
			50,
			164,
			202,
			16,
			33,
			240,
			211,
			93,
			15,
			0,
			111,
			157,
			54,
			66,
			74,
			94,
			193,
			224,
			117,
			243,
			198,
			244,
			219,
			123,
			251,
			200,
			74,
			211,
			230,
			107,
			69,
			125,
			232,
			75,
			214,
			50,
			216,
			253,
			55,
			113,
			241,
			225,
			48,
			15,
			248,
			27,
			135,
			250,
			6,
			63,
			94,
			186,
			174,
			91,
			138,
			0,
			188,
			157,
			109,
			193,
			177,
			14,
			128,
			93,
			210,
			213,
			160,
			132,
			7,
			20,
			181,
			144,
			44,
			163,
			178,
			115,
			76,
			84,
			146,
			116,
			54,
			81,
			56,
			176,
			189,
			90,
			252,
			96,
			98,
			150,
			108,
			66,
			247,
			16,
			124,
			40,
			39,
			140,
			19,
			149,
			156,
			199,
			36,
			70,
			59,
			112,
			202,
			227,
			133,
			203,
			17,
			208,
			147,
			184,
			166,
			131,
			32,
			byte.MaxValue,
			159,
			119,
			195,
			204,
			3,
			111,
			8,
			191,
			64,
			231,
			43,
			226,
			121,
			12,
			170,
			130,
			65,
			58,
			234,
			185,
			228,
			154,
			164,
			151,
			126,
			218,
			122,
			23,
			102,
			148,
			161,
			29,
			61,
			240,
			222,
			179,
			11,
			114,
			167,
			28,
			239,
			209,
			83,
			62,
			143,
			51,
			38,
			95,
			236,
			118,
			42,
			73,
			129,
			136,
			238,
			33,
			196,
			26,
			235,
			217,
			197,
			57,
			153,
			205,
			173,
			49,
			139,
			1,
			24,
			35,
			221,
			31,
			78,
			45,
			249,
			72,
			79,
			242,
			101,
			142,
			120,
			92,
			88,
			25,
			141,
			229,
			152,
			87,
			103,
			127,
			5,
			100,
			175,
			99,
			182,
			254,
			245,
			183,
			60,
			165,
			206,
			233,
			104,
			68,
			224,
			77,
			67,
			105,
			41,
			46,
			172,
			21,
			89,
			168,
			10,
			158,
			110,
			71,
			223,
			52,
			53,
			106,
			207,
			220,
			34,
			201,
			192,
			155,
			137,
			212,
			237,
			171,
			18,
			162,
			13,
			82,
			187,
			2,
			47,
			169,
			215,
			97,
			30,
			180,
			80,
			4,
			246,
			194,
			22,
			37,
			134,
			86,
			85,
			9,
			190,
			145
		};

		// Token: 0x040002C3 RID: 707
		private const int P_00 = 1;

		// Token: 0x040002C4 RID: 708
		private const int P_01 = 0;

		// Token: 0x040002C5 RID: 709
		private const int P_02 = 0;

		// Token: 0x040002C6 RID: 710
		private const int P_03 = 1;

		// Token: 0x040002C7 RID: 711
		private const int P_04 = 1;

		// Token: 0x040002C8 RID: 712
		private const int P_10 = 0;

		// Token: 0x040002C9 RID: 713
		private const int P_11 = 0;

		// Token: 0x040002CA RID: 714
		private const int P_12 = 1;

		// Token: 0x040002CB RID: 715
		private const int P_13 = 1;

		// Token: 0x040002CC RID: 716
		private const int P_14 = 0;

		// Token: 0x040002CD RID: 717
		private const int P_20 = 1;

		// Token: 0x040002CE RID: 718
		private const int P_21 = 1;

		// Token: 0x040002CF RID: 719
		private const int P_22 = 0;

		// Token: 0x040002D0 RID: 720
		private const int P_23 = 0;

		// Token: 0x040002D1 RID: 721
		private const int P_24 = 0;

		// Token: 0x040002D2 RID: 722
		private const int P_30 = 0;

		// Token: 0x040002D3 RID: 723
		private const int P_31 = 1;

		// Token: 0x040002D4 RID: 724
		private const int P_32 = 1;

		// Token: 0x040002D5 RID: 725
		private const int P_33 = 0;

		// Token: 0x040002D6 RID: 726
		private const int P_34 = 1;

		// Token: 0x040002D7 RID: 727
		private const int GF256_FDBK = 361;

		// Token: 0x040002D8 RID: 728
		private const int GF256_FDBK_2 = 180;

		// Token: 0x040002D9 RID: 729
		private const int GF256_FDBK_4 = 90;

		// Token: 0x040002DA RID: 730
		private const int RS_GF_FDBK = 333;

		// Token: 0x040002DB RID: 731
		private const int ROUNDS = 16;

		// Token: 0x040002DC RID: 732
		private const int MAX_ROUNDS = 16;

		// Token: 0x040002DD RID: 733
		private const int MAX_KEY_BITS = 256;

		// Token: 0x040002DE RID: 734
		private const int INPUT_WHITEN = 0;

		// Token: 0x040002DF RID: 735
		private const int OUTPUT_WHITEN = 4;

		// Token: 0x040002E0 RID: 736
		private const int ROUND_SUBKEYS = 8;

		// Token: 0x040002E1 RID: 737
		private const int TOTAL_SUBKEYS = 40;

		// Token: 0x040002E2 RID: 738
		private const int SK_STEP = 33686018;

		// Token: 0x040002E3 RID: 739
		private const int SK_BUMP = 16843009;

		// Token: 0x040002E4 RID: 740
		private const int SK_ROTL = 9;

		// Token: 0x040002E5 RID: 741
		private readonly int[] gMDS0 = new int[256];

		// Token: 0x040002E6 RID: 742
		private readonly int[] gMDS1 = new int[256];

		// Token: 0x040002E7 RID: 743
		private readonly int[] gMDS2 = new int[256];

		// Token: 0x040002E8 RID: 744
		private readonly int[] gMDS3 = new int[256];

		// Token: 0x040002E9 RID: 745
		private int[] gSubKeys;

		// Token: 0x040002EA RID: 746
		private int[] gSBox;

		// Token: 0x040002EB RID: 747
		private readonly int _k64Cnt;
	}
}
