using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003D4 RID: 980
	public class AesLightEngine : IBlockCipher
	{
		// Token: 0x06002215 RID: 8725 RVA: 0x000CE246 File Offset: 0x000CD246
		private uint Shift(uint r, int shift)
		{
			return r >> shift | r << 32 - shift;
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000CE258 File Offset: 0x000CD258
		private uint FFmulX(uint x)
		{
			return (x & 2139062143U) << 1 ^ ((x & 2155905152U) >> 7) * 27U;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000CE270 File Offset: 0x000CD270
		private uint Mcol(uint x)
		{
			uint num = this.FFmulX(x);
			return num ^ this.Shift(x ^ num, 8) ^ this.Shift(x, 16) ^ this.Shift(x, 24);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000CE2A8 File Offset: 0x000CD2A8
		private uint Inv_Mcol(uint x)
		{
			uint num = this.FFmulX(x);
			uint num2 = this.FFmulX(num);
			uint num3 = this.FFmulX(num2);
			uint num4 = x ^ num3;
			return num ^ num2 ^ num3 ^ this.Shift(num ^ num4, 8) ^ this.Shift(num2 ^ num4, 16) ^ this.Shift(num4, 24);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000CE2F8 File Offset: 0x000CD2F8
		private uint SubWord(uint x)
		{
			return (uint)((int)AesLightEngine.S[(int)((UIntPtr)(x & 255U))] | (int)AesLightEngine.S[(int)((UIntPtr)(x >> 8 & 255U))] << 8 | (int)AesLightEngine.S[(int)((UIntPtr)(x >> 16 & 255U))] << 16 | (int)AesLightEngine.S[(int)((UIntPtr)(x >> 24 & 255U))] << 24);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000CE350 File Offset: 0x000CD350
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
					num4 = (this.SubWord(this.Shift(num4, 8)) ^ (uint)AesLightEngine.rcon[j / num - 1]);
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

		// Token: 0x0600221C RID: 8732 RVA: 0x000CE494 File Offset: 0x000CD494
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to AES init - " + parameters.GetType().ToString());
			}
			this.WorkingKey = this.GenerateWorkingKey(((KeyParameter)parameters).GetKey(), forEncryption);
			this.forEncryption = forEncryption;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000CE4E3 File Offset: 0x000CD4E3
		public string AlgorithmName
		{
			get
			{
				return "AES";
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000CE4EA File Offset: 0x000CD4EA
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000CE4ED File Offset: 0x000CD4ED
		public int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000CE4F4 File Offset: 0x000CD4F4
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
			if (this.forEncryption)
			{
				this.UnPackBlock(input, inOff);
				this.EncryptBlock(this.WorkingKey);
				this.PackBlock(output, outOff);
			}
			else
			{
				this.UnPackBlock(input, inOff);
				this.DecryptBlock(this.WorkingKey);
				this.PackBlock(output, outOff);
			}
			return 16;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000CE583 File Offset: 0x000CD583
		public void Reset()
		{
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000CE585 File Offset: 0x000CD585
		private void UnPackBlock(byte[] bytes, int off)
		{
			this.C0 = Pack.LE_To_UInt32(bytes, off);
			this.C1 = Pack.LE_To_UInt32(bytes, off + 4);
			this.C2 = Pack.LE_To_UInt32(bytes, off + 8);
			this.C3 = Pack.LE_To_UInt32(bytes, off + 12);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000CE5C2 File Offset: 0x000CD5C2
		private void PackBlock(byte[] bytes, int off)
		{
			Pack.UInt32_To_LE(this.C0, bytes, off);
			Pack.UInt32_To_LE(this.C1, bytes, off + 4);
			Pack.UInt32_To_LE(this.C2, bytes, off + 8);
			Pack.UInt32_To_LE(this.C3, bytes, off + 12);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000CE600 File Offset: 0x000CD600
		private void EncryptBlock(uint[,] KW)
		{
			this.C0 ^= KW[0, 0];
			this.C1 ^= KW[0, 1];
			this.C2 ^= KW[0, 2];
			this.C3 ^= KW[0, 3];
			int i = 1;
			uint num;
			uint num2;
			uint num3;
			uint num4;
			while (i < this.ROUNDS - 1)
			{
				num = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C0 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C1 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C3 >> 24 & 255U))] << 24)) ^ KW[i, 0]);
				num2 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C1 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C0 >> 24 & 255U))] << 24)) ^ KW[i, 1]);
				num3 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C2 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C0 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C1 >> 24 & 255U))] << 24)) ^ KW[i, 2]);
				num4 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C3 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C0 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C1 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C2 >> 24 & 255U))] << 24)) ^ KW[i++, 3]);
				this.C0 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(num & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num4 >> 24 & 255U))] << 24)) ^ KW[i, 0]);
				this.C1 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(num2 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num4 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num >> 24 & 255U))] << 24)) ^ KW[i, 1]);
				this.C2 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(num3 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num4 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num2 >> 24 & 255U))] << 24)) ^ KW[i, 2]);
				this.C3 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(num4 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num3 >> 24 & 255U))] << 24)) ^ KW[i++, 3]);
			}
			num = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C0 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C1 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C3 >> 24 & 255U))] << 24)) ^ KW[i, 0]);
			num2 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C1 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C0 >> 24 & 255U))] << 24)) ^ KW[i, 1]);
			num3 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C2 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C0 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C1 >> 24 & 255U))] << 24)) ^ KW[i, 2]);
			num4 = (this.Mcol((uint)((int)AesLightEngine.S[(int)((UIntPtr)(this.C3 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C0 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C1 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(this.C2 >> 24 & 255U))] << 24)) ^ KW[i++, 3]);
			this.C0 = (uint)((int)AesLightEngine.S[(int)((UIntPtr)(num & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num4 >> 24 & 255U))] << 24 ^ (int)KW[i, 0]);
			this.C1 = (uint)((int)AesLightEngine.S[(int)((UIntPtr)(num2 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num4 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num >> 24 & 255U))] << 24 ^ (int)KW[i, 1]);
			this.C2 = (uint)((int)AesLightEngine.S[(int)((UIntPtr)(num3 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num4 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num2 >> 24 & 255U))] << 24 ^ (int)KW[i, 2]);
			this.C3 = (uint)((int)AesLightEngine.S[(int)((UIntPtr)(num4 & 255U))] ^ (int)AesLightEngine.S[(int)((UIntPtr)(num >> 8 & 255U))] << 8 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.S[(int)((UIntPtr)(num3 >> 24 & 255U))] << 24 ^ (int)KW[i, 3]);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000CECEC File Offset: 0x000CDCEC
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
				num = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 >> 24 & 255U))] << 24)) ^ KW[i, 0]);
				num2 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 >> 24 & 255U))] << 24)) ^ KW[i, 1]);
				num3 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 >> 24 & 255U))] << 24)) ^ KW[i, 2]);
				num4 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 >> 24 & 255U))] << 24)) ^ KW[i--, 3]);
				this.C0 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num4 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num2 >> 24 & 255U))] << 24)) ^ KW[i, 0]);
				this.C1 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num2 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num4 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num3 >> 24 & 255U))] << 24)) ^ KW[i, 1]);
				this.C2 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num3 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num4 >> 24 & 255U))] << 24)) ^ KW[i, 2]);
				this.C3 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num4 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num >> 24 & 255U))] << 24)) ^ KW[i--, 3]);
			}
			num = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 >> 24 & 255U))] << 24)) ^ KW[i, 0]);
			num2 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 >> 24 & 255U))] << 24)) ^ KW[i, 1]);
			num3 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 >> 24 & 255U))] << 24)) ^ KW[i, 2]);
			num4 = (this.Inv_Mcol((uint)((int)AesLightEngine.Si[(int)((UIntPtr)(this.C3 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C1 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(this.C0 >> 24 & 255U))] << 24)) ^ KW[i, 3]);
			this.C0 = (uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num4 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num3 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num2 >> 24 & 255U))] << 24 ^ (int)KW[0, 0]);
			this.C1 = (uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num2 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num4 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num3 >> 24 & 255U))] << 24 ^ (int)KW[0, 1]);
			this.C2 = (uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num3 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num2 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num4 >> 24 & 255U))] << 24 ^ (int)KW[0, 2]);
			this.C3 = (uint)((int)AesLightEngine.Si[(int)((UIntPtr)(num4 & 255U))] ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num3 >> 8 & 255U))] << 8 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num2 >> 16 & 255U))] << 16 ^ (int)AesLightEngine.Si[(int)((UIntPtr)(num >> 24 & 255U))] << 24 ^ (int)KW[0, 3]);
		}

		// Token: 0x04001758 RID: 5976
		private const uint m1 = 2155905152U;

		// Token: 0x04001759 RID: 5977
		private const uint m2 = 2139062143U;

		// Token: 0x0400175A RID: 5978
		private const uint m3 = 27U;

		// Token: 0x0400175B RID: 5979
		private const int BLOCK_SIZE = 16;

		// Token: 0x0400175C RID: 5980
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

		// Token: 0x0400175D RID: 5981
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

		// Token: 0x0400175E RID: 5982
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

		// Token: 0x0400175F RID: 5983
		private int ROUNDS;

		// Token: 0x04001760 RID: 5984
		private uint[,] WorkingKey;

		// Token: 0x04001761 RID: 5985
		private uint C0;

		// Token: 0x04001762 RID: 5986
		private uint C1;

		// Token: 0x04001763 RID: 5987
		private uint C2;

		// Token: 0x04001764 RID: 5988
		private uint C3;

		// Token: 0x04001765 RID: 5989
		private bool forEncryption;
	}
}
