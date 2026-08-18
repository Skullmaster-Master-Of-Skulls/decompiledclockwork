using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020002A8 RID: 680
	public class AesEngine : IBlockCipher
	{
		// Token: 0x06001999 RID: 6553 RVA: 0x00094D29 File Offset: 0x00093D29
		private uint Shift(uint r, int shift)
		{
			return r >> shift | r << 32 - shift;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00094D3B File Offset: 0x00093D3B
		private uint FFmulX(uint x)
		{
			return (x & 2139062143U) << 1 ^ ((x & 2155905152U) >> 7) * 27U;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00094D54 File Offset: 0x00093D54
		private uint Inv_Mcol(uint x)
		{
			uint num = this.FFmulX(x);
			uint num2 = this.FFmulX(num);
			uint num3 = this.FFmulX(num2);
			uint num4 = x ^ num3;
			return num ^ num2 ^ num3 ^ this.Shift(num ^ num4, 8) ^ this.Shift(num2 ^ num4, 16) ^ this.Shift(num4, 24);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00094DA4 File Offset: 0x00093DA4
		private uint SubWord(uint x)
		{
			return (uint)((int)AesEngine.S[(int)((UIntPtr)(x & 255U))] | (int)AesEngine.S[(int)((UIntPtr)(x >> 8 & 255U))] << 8 | (int)AesEngine.S[(int)((UIntPtr)(x >> 16 & 255U))] << 16 | (int)AesEngine.S[(int)((UIntPtr)(x >> 24 & 255U))] << 24);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00094DFC File Offset: 0x00093DFC
		private uint[,] GenerateWorkingKey(byte[] key, bool forEncryption)
		{
			int num = key.Length / 4;
			if (num != 4 && num != 6 && num != 8)
			{
				throw new ArgumentException("Key length not 128/192/256 bits.");
			}
			this.ROUNDS = num + 6;
			uint[,] array = new uint[this.ROUNDS + 1, 4];
			int num2 = 0;
			int i = 0;
			while (i < key.Length)
			{
				array[num2 >> 2, num2 & 3] = Pack.LE_To_UInt32(key, i);
				i += 4;
				num2++;
			}
			int num3 = this.ROUNDS + 1 << 2;
			for (int j = num; j < num3; j++)
			{
				uint num4 = array[j - 1 >> 2, j - 1 & 3];
				if (j % num == 0)
				{
					num4 = (this.SubWord(this.Shift(num4, 8)) ^ (uint)AesEngine.rcon[j / num - 1]);
				}
				else if (num > 6 && j % num == 4)
				{
					num4 = this.SubWord(num4);
				}
				array[j >> 2, j & 3] = (array[j - num >> 2, j - num & 3] ^ num4);
			}
			if (!forEncryption)
			{
				for (int k = 1; k < this.ROUNDS; k++)
				{
					for (int l = 0; l < 4; l++)
					{
						array[k, l] = this.Inv_Mcol(array[k, l]);
					}
				}
			}
			return array;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00094F40 File Offset: 0x00093F40
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			KeyParameter keyParameter = parameters as KeyParameter;
			if (keyParameter == null)
			{
				throw new ArgumentException("invalid parameter passed to AES init - " + parameters.GetType().Name);
			}
			this.WorkingKey = this.GenerateWorkingKey(keyParameter.GetKey(), forEncryption);
			this.forEncryption = forEncryption;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00094F8C File Offset: 0x00093F8C
		public string AlgorithmName
		{
			get
			{
				return "AES";
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00094F93 File Offset: 0x00093F93
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00094F96 File Offset: 0x00093F96
		public int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00094F9C File Offset: 0x00093F9C
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.WorkingKey == null)
			{
				throw new InvalidOperationException("AES engine not initialised");
			}
			if (inOff + 16 > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 16 > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.UnPackBlock(input, inOff);
			if (this.forEncryption)
			{
				this.EncryptBlock(this.WorkingKey);
			}
			else
			{
				this.DecryptBlock(this.WorkingKey);
			}
			this.PackBlock(output, outOff);
			return 16;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0009501A File Offset: 0x0009401A
		public void Reset()
		{
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0009501C File Offset: 0x0009401C
		private void UnPackBlock(byte[] bytes, int off)
		{
			this.C0 = Pack.LE_To_UInt32(bytes, off);
			this.C1 = Pack.LE_To_UInt32(bytes, off + 4);
			this.C2 = Pack.LE_To_UInt32(bytes, off + 8);
			this.C3 = Pack.LE_To_UInt32(bytes, off + 12);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00095059 File Offset: 0x00094059
		private void PackBlock(byte[] bytes, int off)
		{
			Pack.UInt32_To_LE(this.C0, bytes, off);
			Pack.UInt32_To_LE(this.C1, bytes, off + 4);
			Pack.UInt32_To_LE(this.C2, bytes, off + 8);
			Pack.UInt32_To_LE(this.C3, bytes, off + 12);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00095098 File Offset: 0x00094098
		private void EncryptBlock(uint[,] KW)
		{
			this.C0 ^= KW[0, 0];
			this.C1 ^= KW[0, 1];
			this.C2 ^= KW[0, 2];
			this.C3 ^= KW[0, 3];
			uint num = 1U;
			uint num2;
			uint num3;
			uint num4;
			uint num5;
			while ((ulong)num < (ulong)((long)(this.ROUNDS - 1)))
			{
				num2 = (AesEngine.T0[(int)((UIntPtr)(this.C0 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C1 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C2 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C3 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)0)]);
				num3 = (AesEngine.T0[(int)((UIntPtr)(this.C1 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C2 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C3 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C0 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)1)]);
				num4 = (AesEngine.T0[(int)((UIntPtr)(this.C2 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C3 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C0 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C1 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)2)]);
				num5 = (AesEngine.T0[(int)((UIntPtr)(this.C3 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C0 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C1 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C2 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)(num++)), (int)((UIntPtr)3)]);
				this.C0 = (AesEngine.T0[(int)((UIntPtr)(num2 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num3 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num4 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num5 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)0)]);
				this.C1 = (AesEngine.T0[(int)((UIntPtr)(num3 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num4 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num5 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num2 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)1)]);
				this.C2 = (AesEngine.T0[(int)((UIntPtr)(num4 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num5 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num2 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num3 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)2)]);
				this.C3 = (AesEngine.T0[(int)((UIntPtr)(num5 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num2 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num3 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(num4 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)(num++)), (int)((UIntPtr)3)]);
			}
			num2 = (AesEngine.T0[(int)((UIntPtr)(this.C0 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C1 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C2 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C3 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)0)]);
			num3 = (AesEngine.T0[(int)((UIntPtr)(this.C1 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C2 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C3 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C0 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)1)]);
			num4 = (AesEngine.T0[(int)((UIntPtr)(this.C2 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C3 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C0 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C1 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)num), (int)((UIntPtr)2)]);
			num5 = (AesEngine.T0[(int)((UIntPtr)(this.C3 & 255U))] ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C0 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C1 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.T0[(int)((UIntPtr)(this.C2 >> 24 & 255U))], 8) ^ KW[(int)((UIntPtr)(num++)), (int)((UIntPtr)3)]);
			this.C0 = (uint)((int)AesEngine.S[(int)((UIntPtr)(num2 & 255U))] ^ (int)AesEngine.S[(int)((UIntPtr)(num3 >> 8 & 255U))] << 8 ^ (int)AesEngine.S[(int)((UIntPtr)(num4 >> 16 & 255U))] << 16 ^ (int)AesEngine.S[(int)((UIntPtr)(num5 >> 24 & 255U))] << 24 ^ (int)KW[(int)((UIntPtr)num), (int)((UIntPtr)0)]);
			this.C1 = (uint)((int)AesEngine.S[(int)((UIntPtr)(num3 & 255U))] ^ (int)AesEngine.S[(int)((UIntPtr)(num4 >> 8 & 255U))] << 8 ^ (int)AesEngine.S[(int)((UIntPtr)(num5 >> 16 & 255U))] << 16 ^ (int)AesEngine.S[(int)((UIntPtr)(num2 >> 24 & 255U))] << 24 ^ (int)KW[(int)((UIntPtr)num), (int)((UIntPtr)1)]);
			this.C2 = (uint)((int)AesEngine.S[(int)((UIntPtr)(num4 & 255U))] ^ (int)AesEngine.S[(int)((UIntPtr)(num5 >> 8 & 255U))] << 8 ^ (int)AesEngine.S[(int)((UIntPtr)(num2 >> 16 & 255U))] << 16 ^ (int)AesEngine.S[(int)((UIntPtr)(num3 >> 24 & 255U))] << 24 ^ (int)KW[(int)((UIntPtr)num), (int)((UIntPtr)2)]);
			this.C3 = (uint)((int)AesEngine.S[(int)((UIntPtr)(num5 & 255U))] ^ (int)AesEngine.S[(int)((UIntPtr)(num2 >> 8 & 255U))] << 8 ^ (int)AesEngine.S[(int)((UIntPtr)(num3 >> 16 & 255U))] << 16 ^ (int)AesEngine.S[(int)((UIntPtr)(num4 >> 24 & 255U))] << 24 ^ (int)KW[(int)((UIntPtr)num), (int)((UIntPtr)3)]);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00095814 File Offset: 0x00094814
		private void DecryptBlock(uint[,] KW)
		{
			this.C0 ^= KW[this.ROUNDS, 0];
			this.C1 ^= KW[this.ROUNDS, 1];
			this.C2 ^= KW[this.ROUNDS, 2];
			this.C3 ^= KW[this.ROUNDS, 3];
			int i = this.ROUNDS - 1;
			uint num;
			uint num2;
			uint num3;
			uint num4;
			while (i > 1)
			{
				num = (AesEngine.Tinv0[(int)((UIntPtr)(this.C0 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C3 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C2 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C1 >> 24 & 255U))], 8) ^ KW[i, 0]);
				num2 = (AesEngine.Tinv0[(int)((UIntPtr)(this.C1 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C0 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C3 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C2 >> 24 & 255U))], 8) ^ KW[i, 1]);
				num3 = (AesEngine.Tinv0[(int)((UIntPtr)(this.C2 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C1 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C0 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C3 >> 24 & 255U))], 8) ^ KW[i, 2]);
				num4 = (AesEngine.Tinv0[(int)((UIntPtr)(this.C3 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C2 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C1 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C0 >> 24 & 255U))], 8) ^ KW[i--, 3]);
				this.C0 = (AesEngine.Tinv0[(int)((UIntPtr)(num & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num4 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num3 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num2 >> 24 & 255U))], 8) ^ KW[i, 0]);
				this.C1 = (AesEngine.Tinv0[(int)((UIntPtr)(num2 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num4 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num3 >> 24 & 255U))], 8) ^ KW[i, 1]);
				this.C2 = (AesEngine.Tinv0[(int)((UIntPtr)(num3 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num2 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num4 >> 24 & 255U))], 8) ^ KW[i, 2]);
				this.C3 = (AesEngine.Tinv0[(int)((UIntPtr)(num4 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num3 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num2 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(num >> 24 & 255U))], 8) ^ KW[i--, 3]);
			}
			num = (AesEngine.Tinv0[(int)((UIntPtr)(this.C0 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C3 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C2 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C1 >> 24 & 255U))], 8) ^ KW[i, 0]);
			num2 = (AesEngine.Tinv0[(int)((UIntPtr)(this.C1 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C0 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C3 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C2 >> 24 & 255U))], 8) ^ KW[i, 1]);
			num3 = (AesEngine.Tinv0[(int)((UIntPtr)(this.C2 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C1 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C0 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C3 >> 24 & 255U))], 8) ^ KW[i, 2]);
			num4 = (AesEngine.Tinv0[(int)((UIntPtr)(this.C3 & 255U))] ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C2 >> 8 & 255U))], 24) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C1 >> 16 & 255U))], 16) ^ this.Shift(AesEngine.Tinv0[(int)((UIntPtr)(this.C0 >> 24 & 255U))], 8) ^ KW[i, 3]);
			this.C0 = (uint)((int)AesEngine.Si[(int)((UIntPtr)(num & 255U))] ^ (int)AesEngine.Si[(int)((UIntPtr)(num4 >> 8 & 255U))] << 8 ^ (int)AesEngine.Si[(int)((UIntPtr)(num3 >> 16 & 255U))] << 16 ^ (int)AesEngine.Si[(int)((UIntPtr)(num2 >> 24 & 255U))] << 24 ^ (int)KW[0, 0]);
			this.C1 = (uint)((int)AesEngine.Si[(int)((UIntPtr)(num2 & 255U))] ^ (int)AesEngine.Si[(int)((UIntPtr)(num >> 8 & 255U))] << 8 ^ (int)AesEngine.Si[(int)((UIntPtr)(num4 >> 16 & 255U))] << 16 ^ (int)AesEngine.Si[(int)((UIntPtr)(num3 >> 24 & 255U))] << 24 ^ (int)KW[0, 1]);
			this.C2 = (uint)((int)AesEngine.Si[(int)((UIntPtr)(num3 & 255U))] ^ (int)AesEngine.Si[(int)((UIntPtr)(num2 >> 8 & 255U))] << 8 ^ (int)AesEngine.Si[(int)((UIntPtr)(num >> 16 & 255U))] << 16 ^ (int)AesEngine.Si[(int)((UIntPtr)(num4 >> 24 & 255U))] << 24 ^ (int)KW[0, 2]);
			this.C3 = (uint)((int)AesEngine.Si[(int)((UIntPtr)(num4 & 255U))] ^ (int)AesEngine.Si[(int)((UIntPtr)(num3 >> 8 & 255U))] << 8 ^ (int)AesEngine.Si[(int)((UIntPtr)(num2 >> 16 & 255U))] << 16 ^ (int)AesEngine.Si[(int)((UIntPtr)(num >> 24 & 255U))] << 24 ^ (int)KW[0, 3]);
		}

		// Token: 0x04001110 RID: 4368
		private const uint m1 = 2155905152U;

		// Token: 0x04001111 RID: 4369
		private const uint m2 = 2139062143U;

		// Token: 0x04001112 RID: 4370
		private const uint m3 = 27U;

		// Token: 0x04001113 RID: 4371
		private const int BLOCK_SIZE = 16;

		// Token: 0x04001114 RID: 4372
		private static readonly byte[] S = new byte[]
		{
			99,
			124,
			119,
			123,
			242,
			107,
			111,
			197,
			48,
			1,
			103,
			43,
			254,
			215,
			171,
			118,
			202,
			130,
			201,
			125,
			250,
			89,
			71,
			240,
			173,
			212,
			162,
			175,
			156,
			164,
			114,
			192,
			183,
			253,
			147,
			38,
			54,
			63,
			247,
			204,
			52,
			165,
			229,
			241,
			113,
			216,
			49,
			21,
			4,
			199,
			35,
			195,
			24,
			150,
			5,
			154,
			7,
			18,
			128,
			226,
			235,
			39,
			178,
			117,
			9,
			131,
			44,
			26,
			27,
			110,
			90,
			160,
			82,
			59,
			214,
			179,
			41,
			227,
			47,
			132,
			83,
			209,
			0,
			237,
			32,
			252,
			177,
			91,
			106,
			203,
			190,
			57,
			74,
			76,
			88,
			207,
			208,
			239,
			170,
			251,
			67,
			77,
			51,
			133,
			69,
			249,
			2,
			127,
			80,
			60,
			159,
			168,
			81,
			163,
			64,
			143,
			146,
			157,
			56,
			245,
			188,
			182,
			218,
			33,
			16,
			byte.MaxValue,
			243,
			210,
			205,
			12,
			19,
			236,
			95,
			151,
			68,
			23,
			196,
			167,
			126,
			61,
			100,
			93,
			25,
			115,
			96,
			129,
			79,
			220,
			34,
			42,
			144,
			136,
			70,
			238,
			184,
			20,
			222,
			94,
			11,
			219,
			224,
			50,
			58,
			10,
			73,
			6,
			36,
			92,
			194,
			211,
			172,
			98,
			145,
			149,
			228,
			121,
			231,
			200,
			55,
			109,
			141,
			213,
			78,
			169,
			108,
			86,
			244,
			234,
			101,
			122,
			174,
			8,
			186,
			120,
			37,
			46,
			28,
			166,
			180,
			198,
			232,
			221,
			116,
			31,
			75,
			189,
			139,
			138,
			112,
			62,
			181,
			102,
			72,
			3,
			246,
			14,
			97,
			53,
			87,
			185,
			134,
			193,
			29,
			158,
			225,
			248,
			152,
			17,
			105,
			217,
			142,
			148,
			155,
			30,
			135,
			233,
			206,
			85,
			40,
			223,
			140,
			161,
			137,
			13,
			191,
			230,
			66,
			104,
			65,
			153,
			45,
			15,
			176,
			84,
			187,
			22
		};

		// Token: 0x04001115 RID: 4373
		private static readonly byte[] Si = new byte[]
		{
			82,
			9,
			106,
			213,
			48,
			54,
			165,
			56,
			191,
			64,
			163,
			158,
			129,
			243,
			215,
			251,
			124,
			227,
			57,
			130,
			155,
			47,
			byte.MaxValue,
			135,
			52,
			142,
			67,
			68,
			196,
			222,
			233,
			203,
			84,
			123,
			148,
			50,
			166,
			194,
			35,
			61,
			238,
			76,
			149,
			11,
			66,
			250,
			195,
			78,
			8,
			46,
			161,
			102,
			40,
			217,
			36,
			178,
			118,
			91,
			162,
			73,
			109,
			139,
			209,
			37,
			114,
			248,
			246,
			100,
			134,
			104,
			152,
			22,
			212,
			164,
			92,
			204,
			93,
			101,
			182,
			146,
			108,
			112,
			72,
			80,
			253,
			237,
			185,
			218,
			94,
			21,
			70,
			87,
			167,
			141,
			157,
			132,
			144,
			216,
			171,
			0,
			140,
			188,
			211,
			10,
			247,
			228,
			88,
			5,
			184,
			179,
			69,
			6,
			208,
			44,
			30,
			143,
			202,
			63,
			15,
			2,
			193,
			175,
			189,
			3,
			1,
			19,
			138,
			107,
			58,
			145,
			17,
			65,
			79,
			103,
			220,
			234,
			151,
			242,
			207,
			206,
			240,
			180,
			230,
			115,
			150,
			172,
			116,
			34,
			231,
			173,
			53,
			133,
			226,
			249,
			55,
			232,
			28,
			117,
			223,
			110,
			71,
			241,
			26,
			113,
			29,
			41,
			197,
			137,
			111,
			183,
			98,
			14,
			170,
			24,
			190,
			27,
			252,
			86,
			62,
			75,
			198,
			210,
			121,
			32,
			154,
			219,
			192,
			254,
			120,
			205,
			90,
			244,
			31,
			221,
			168,
			51,
			136,
			7,
			199,
			49,
			177,
			18,
			16,
			89,
			39,
			128,
			236,
			95,
			96,
			81,
			127,
			169,
			25,
			181,
			74,
			13,
			45,
			229,
			122,
			159,
			147,
			201,
			156,
			239,
			160,
			224,
			59,
			77,
			174,
			42,
			245,
			176,
			200,
			235,
			187,
			60,
			131,
			83,
			153,
			97,
			23,
			43,
			4,
			126,
			186,
			119,
			214,
			38,
			225,
			105,
			20,
			99,
			85,
			33,
			12,
			125
		};

		// Token: 0x04001116 RID: 4374
		private static readonly byte[] rcon = new byte[]
		{
			1,
			2,
			4,
			8,
			16,
			32,
			64,
			128,
			27,
			54,
			108,
			216,
			171,
			77,
			154,
			47,
			94,
			188,
			99,
			198,
			151,
			53,
			106,
			212,
			179,
			125,
			250,
			239,
			197,
			145
		};

		// Token: 0x04001117 RID: 4375
		private static readonly uint[] T0 = new uint[]
		{
			2774754246U,
			2222750968U,
			2574743534U,
			2373680118U,
			234025727U,
			3177933782U,
			2976870366U,
			1422247313U,
			1345335392U,
			50397442U,
			2842126286U,
			2099981142U,
			436141799U,
			1658312629U,
			3870010189U,
			2591454956U,
			1170918031U,
			2642575903U,
			1086966153U,
			2273148410U,
			368769775U,
			3948501426U,
			3376891790U,
			200339707U,
			3970805057U,
			1742001331U,
			4255294047U,
			3937382213U,
			3214711843U,
			4154762323U,
			2524082916U,
			1539358875U,
			3266819957U,
			486407649U,
			2928907069U,
			1780885068U,
			1513502316U,
			1094664062U,
			49805301U,
			1338821763U,
			1546925160U,
			4104496465U,
			887481809U,
			150073849U,
			2473685474U,
			1943591083U,
			1395732834U,
			1058346282U,
			201589768U,
			1388824469U,
			1696801606U,
			1589887901U,
			672667696U,
			2711000631U,
			251987210U,
			3046808111U,
			151455502U,
			907153956U,
			2608889883U,
			1038279391U,
			652995533U,
			1764173646U,
			3451040383U,
			2675275242U,
			453576978U,
			2659418909U,
			1949051992U,
			773462580U,
			756751158U,
			2993581788U,
			3998898868U,
			4221608027U,
			4132590244U,
			1295727478U,
			1641469623U,
			3467883389U,
			2066295122U,
			1055122397U,
			1898917726U,
			2542044179U,
			4115878822U,
			1758581177U,
			0U,
			753790401U,
			1612718144U,
			536673507U,
			3367088505U,
			3982187446U,
			3194645204U,
			1187761037U,
			3653156455U,
			1262041458U,
			3729410708U,
			3561770136U,
			3898103984U,
			1255133061U,
			1808847035U,
			720367557U,
			3853167183U,
			385612781U,
			3309519750U,
			3612167578U,
			1429418854U,
			2491778321U,
			3477423498U,
			284817897U,
			100794884U,
			2172616702U,
			4031795360U,
			1144798328U,
			3131023141U,
			3819481163U,
			4082192802U,
			4272137053U,
			3225436288U,
			2324664069U,
			2912064063U,
			3164445985U,
			1211644016U,
			83228145U,
			3753688163U,
			3249976951U,
			1977277103U,
			1663115586U,
			806359072U,
			452984805U,
			250868733U,
			1842533055U,
			1288555905U,
			336333848U,
			890442534U,
			804056259U,
			3781124030U,
			2727843637U,
			3427026056U,
			957814574U,
			1472513171U,
			4071073621U,
			2189328124U,
			1195195770U,
			2892260552U,
			3881655738U,
			723065138U,
			2507371494U,
			2690670784U,
			2558624025U,
			3511635870U,
			2145180835U,
			1713513028U,
			2116692564U,
			2878378043U,
			2206763019U,
			3393603212U,
			703524551U,
			3552098411U,
			1007948840U,
			2044649127U,
			3797835452U,
			487262998U,
			1994120109U,
			1004593371U,
			1446130276U,
			1312438900U,
			503974420U,
			3679013266U,
			168166924U,
			1814307912U,
			3831258296U,
			1573044895U,
			1859376061U,
			4021070915U,
			2791465668U,
			2828112185U,
			2761266481U,
			937747667U,
			2339994098U,
			854058965U,
			1137232011U,
			1496790894U,
			3077402074U,
			2358086913U,
			1691735473U,
			3528347292U,
			3769215305U,
			3027004632U,
			4199962284U,
			133494003U,
			636152527U,
			2942657994U,
			2390391540U,
			3920539207U,
			403179536U,
			3585784431U,
			2289596656U,
			1864705354U,
			1915629148U,
			605822008U,
			4054230615U,
			3350508659U,
			1371981463U,
			602466507U,
			2094914977U,
			2624877800U,
			555687742U,
			3712699286U,
			3703422305U,
			2257292045U,
			2240449039U,
			2423288032U,
			1111375484U,
			3300242801U,
			2858837708U,
			3628615824U,
			84083462U,
			32962295U,
			302911004U,
			2741068226U,
			1597322602U,
			4183250862U,
			3501832553U,
			2441512471U,
			1489093017U,
			656219450U,
			3114180135U,
			954327513U,
			335083755U,
			3013122091U,
			856756514U,
			3144247762U,
			1893325225U,
			2307821063U,
			2811532339U,
			3063651117U,
			572399164U,
			2458355477U,
			552200649U,
			1238290055U,
			4283782570U,
			2015897680U,
			2061492133U,
			2408352771U,
			4171342169U,
			2156497161U,
			386731290U,
			3669999461U,
			837215959U,
			3326231172U,
			3093850320U,
			3275833730U,
			2962856233U,
			1999449434U,
			286199582U,
			3417354363U,
			4233385128U,
			3602627437U,
			974525996U
		};

		// Token: 0x04001118 RID: 4376
		private static readonly uint[] Tinv0 = new uint[]
		{
			1353184337U,
			1399144830U,
			3282310938U,
			2522752826U,
			3412831035U,
			4047871263U,
			2874735276U,
			2466505547U,
			1442459680U,
			4134368941U,
			2440481928U,
			625738485U,
			4242007375U,
			3620416197U,
			2151953702U,
			2409849525U,
			1230680542U,
			1729870373U,
			2551114309U,
			3787521629U,
			41234371U,
			317738113U,
			2744600205U,
			3338261355U,
			3881799427U,
			2510066197U,
			3950669247U,
			3663286933U,
			763608788U,
			3542185048U,
			694804553U,
			1154009486U,
			1787413109U,
			2021232372U,
			1799248025U,
			3715217703U,
			3058688446U,
			397248752U,
			1722556617U,
			3023752829U,
			407560035U,
			2184256229U,
			1613975959U,
			1165972322U,
			3765920945U,
			2226023355U,
			480281086U,
			2485848313U,
			1483229296U,
			436028815U,
			2272059028U,
			3086515026U,
			601060267U,
			3791801202U,
			1468997603U,
			715871590U,
			120122290U,
			63092015U,
			2591802758U,
			2768779219U,
			4068943920U,
			2997206819U,
			3127509762U,
			1552029421U,
			723308426U,
			2461301159U,
			4042393587U,
			2715969870U,
			3455375973U,
			3586000134U,
			526529745U,
			2331944644U,
			2639474228U,
			2689987490U,
			853641733U,
			1978398372U,
			971801355U,
			2867814464U,
			111112542U,
			1360031421U,
			4186579262U,
			1023860118U,
			2919579357U,
			1186850381U,
			3045938321U,
			90031217U,
			1876166148U,
			4279586912U,
			620468249U,
			2548678102U,
			3426959497U,
			2006899047U,
			3175278768U,
			2290845959U,
			945494503U,
			3689859193U,
			1191869601U,
			3910091388U,
			3374220536U,
			0U,
			2206629897U,
			1223502642U,
			2893025566U,
			1316117100U,
			4227796733U,
			1446544655U,
			517320253U,
			658058550U,
			1691946762U,
			564550760U,
			3511966619U,
			976107044U,
			2976320012U,
			266819475U,
			3533106868U,
			2660342555U,
			1338359936U,
			2720062561U,
			1766553434U,
			370807324U,
			179999714U,
			3844776128U,
			1138762300U,
			488053522U,
			185403662U,
			2915535858U,
			3114841645U,
			3366526484U,
			2233069911U,
			1275557295U,
			3151862254U,
			4250959779U,
			2670068215U,
			3170202204U,
			3309004356U,
			880737115U,
			1982415755U,
			3703972811U,
			1761406390U,
			1676797112U,
			3403428311U,
			277177154U,
			1076008723U,
			538035844U,
			2099530373U,
			4164795346U,
			288553390U,
			1839278535U,
			1261411869U,
			4080055004U,
			3964831245U,
			3504587127U,
			1813426987U,
			2579067049U,
			4199060497U,
			577038663U,
			3297574056U,
			440397984U,
			3626794326U,
			4019204898U,
			3343796615U,
			3251714265U,
			4272081548U,
			906744984U,
			3481400742U,
			685669029U,
			646887386U,
			2764025151U,
			3835509292U,
			227702864U,
			2613862250U,
			1648787028U,
			3256061430U,
			3904428176U,
			1593260334U,
			4121936770U,
			3196083615U,
			2090061929U,
			2838353263U,
			3004310991U,
			999926984U,
			2809993232U,
			1852021992U,
			2075868123U,
			158869197U,
			4095236462U,
			28809964U,
			2828685187U,
			1701746150U,
			2129067946U,
			147831841U,
			3873969647U,
			3650873274U,
			3459673930U,
			3557400554U,
			3598495785U,
			2947720241U,
			824393514U,
			815048134U,
			3227951669U,
			935087732U,
			2798289660U,
			2966458592U,
			366520115U,
			1251476721U,
			4158319681U,
			240176511U,
			804688151U,
			2379631990U,
			1303441219U,
			1414376140U,
			3741619940U,
			3820343710U,
			461924940U,
			3089050817U,
			2136040774U,
			82468509U,
			1563790337U,
			1937016826U,
			776014843U,
			1511876531U,
			1389550482U,
			861278441U,
			323475053U,
			2355222426U,
			2047648055U,
			2383738969U,
			2302415851U,
			3995576782U,
			902390199U,
			3991215329U,
			1018251130U,
			1507840668U,
			1064563285U,
			2043548696U,
			3208103795U,
			3939366739U,
			1537932639U,
			342834655U,
			2262516856U,
			2180231114U,
			1053059257U,
			741614648U,
			1598071746U,
			1925389590U,
			203809468U,
			2336832552U,
			1100287487U,
			1895934009U,
			3736275976U,
			2632234200U,
			2428589668U,
			1636092795U,
			1890988757U,
			1952214088U,
			1113045200U
		};

		// Token: 0x04001119 RID: 4377
		private int ROUNDS;

		// Token: 0x0400111A RID: 4378
		private uint[,] WorkingKey;

		// Token: 0x0400111B RID: 4379
		private uint C0;

		// Token: 0x0400111C RID: 4380
		private uint C1;

		// Token: 0x0400111D RID: 4381
		private uint C2;

		// Token: 0x0400111E RID: 4382
		private uint C3;

		// Token: 0x0400111F RID: 4383
		private bool forEncryption;
	}
}
