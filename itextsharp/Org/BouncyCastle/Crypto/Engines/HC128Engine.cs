using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020002A7 RID: 679
	public class HC128Engine : IStreamCipher
	{
		// Token: 0x06001985 RID: 6533 RVA: 0x000947FE File Offset: 0x000937FE
		private static uint F1(uint x)
		{
			return HC128Engine.RotateRight(x, 7) ^ HC128Engine.RotateRight(x, 18) ^ x >> 3;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00094814 File Offset: 0x00093814
		private static uint F2(uint x)
		{
			return HC128Engine.RotateRight(x, 17) ^ HC128Engine.RotateRight(x, 19) ^ x >> 10;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0009482C File Offset: 0x0009382C
		private uint G1(uint x, uint y, uint z)
		{
			return (HC128Engine.RotateRight(x, 10) ^ HC128Engine.RotateRight(z, 23)) + HC128Engine.RotateRight(y, 8);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00094847 File Offset: 0x00093847
		private uint G2(uint x, uint y, uint z)
		{
			return (HC128Engine.RotateLeft(x, 10) ^ HC128Engine.RotateLeft(z, 23)) + HC128Engine.RotateLeft(y, 8);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00094862 File Offset: 0x00093862
		private static uint RotateLeft(uint x, int bits)
		{
			return x << bits | x >> -bits;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00094872 File Offset: 0x00093872
		private static uint RotateRight(uint x, int bits)
		{
			return x >> bits | x << -bits;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00094882 File Offset: 0x00093882
		private uint H1(uint x)
		{
			return this.q[(int)((UIntPtr)(x & 255U))] + this.q[(int)((UIntPtr)((x >> 16 & 255U) + 256U))];
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000948AC File Offset: 0x000938AC
		private uint H2(uint x)
		{
			return this.p[(int)((UIntPtr)(x & 255U))] + this.p[(int)((UIntPtr)((x >> 16 & 255U) + 256U))];
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000948D6 File Offset: 0x000938D6
		private static uint Mod1024(uint x)
		{
			return x & 1023U;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000948DF File Offset: 0x000938DF
		private static uint Mod512(uint x)
		{
			return x & 511U;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000948E8 File Offset: 0x000938E8
		private static uint Dim(uint x, uint y)
		{
			return HC128Engine.Mod512(x - y);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000948F4 File Offset: 0x000938F4
		private uint Step()
		{
			uint num = HC128Engine.Mod512(this.cnt);
			uint result;
			if (this.cnt < 512U)
			{
				this.p[(int)((UIntPtr)num)] += this.G1(this.p[(int)((UIntPtr)HC128Engine.Dim(num, 3U))], this.p[(int)((UIntPtr)HC128Engine.Dim(num, 10U))], this.p[(int)((UIntPtr)HC128Engine.Dim(num, 511U))]);
				result = (this.H1(this.p[(int)((UIntPtr)HC128Engine.Dim(num, 12U))]) ^ this.p[(int)((UIntPtr)num)]);
			}
			else
			{
				this.q[(int)((UIntPtr)num)] += this.G2(this.q[(int)((UIntPtr)HC128Engine.Dim(num, 3U))], this.q[(int)((UIntPtr)HC128Engine.Dim(num, 10U))], this.q[(int)((UIntPtr)HC128Engine.Dim(num, 511U))]);
				result = (this.H2(this.q[(int)((UIntPtr)HC128Engine.Dim(num, 12U))]) ^ this.q[(int)((UIntPtr)num)]);
			}
			this.cnt = HC128Engine.Mod1024(this.cnt + 1U);
			return result;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00094A14 File Offset: 0x00093A14
		private void Init()
		{
			if (this.key.Length != 16)
			{
				throw new ArgumentException("The key must be 128 bits long");
			}
			this.cnt = 0U;
			uint[] array = new uint[1280];
			for (int i = 0; i < 16; i++)
			{
				array[i >> 2] |= (uint)((uint)this.key[i] << 8 * (i & 3));
			}
			Array.Copy(array, 0, array, 4, 4);
			int num = 0;
			while (num < this.iv.Length && num < 16)
			{
				array[(num >> 2) + 8] |= (uint)((uint)this.iv[num] << 8 * (num & 3));
				num++;
			}
			Array.Copy(array, 8, array, 12, 4);
			for (uint num2 = 16U; num2 < 1280U; num2 += 1U)
			{
				array[(int)((UIntPtr)num2)] = HC128Engine.F2(array[(int)((UIntPtr)(num2 - 2U))]) + array[(int)((UIntPtr)(num2 - 7U))] + HC128Engine.F1(array[(int)((UIntPtr)(num2 - 15U))]) + array[(int)((UIntPtr)(num2 - 16U))] + num2;
			}
			Array.Copy(array, 256, this.p, 0, 512);
			Array.Copy(array, 768, this.q, 0, 512);
			for (int j = 0; j < 512; j++)
			{
				this.p[j] = this.Step();
			}
			for (int k = 0; k < 512; k++)
			{
				this.q[k] = this.Step();
			}
			this.cnt = 0U;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00094B8B File Offset: 0x00093B8B
		public string AlgorithmName
		{
			get
			{
				return "HC-128";
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00094B94 File Offset: 0x00093B94
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			ICipherParameters cipherParameters = parameters;
			if (parameters is ParametersWithIV)
			{
				this.iv = ((ParametersWithIV)parameters).GetIV();
				cipherParameters = ((ParametersWithIV)parameters).Parameters;
			}
			else
			{
				this.iv = new byte[0];
			}
			if (cipherParameters is KeyParameter)
			{
				this.key = ((KeyParameter)cipherParameters).GetKey();
				this.Init();
				this.initialised = true;
				return;
			}
			throw new ArgumentException("Invalid parameter passed to HC128 init - " + parameters.GetType().Name, "parameters");
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00094C20 File Offset: 0x00093C20
		private byte GetByte()
		{
			if (this.idx == 0)
			{
				Pack.UInt32_To_LE(this.Step(), this.buf);
			}
			byte result = this.buf[this.idx];
			this.idx = (this.idx + 1 & 3);
			return result;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00094C68 File Offset: 0x00093C68
		public void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			if (inOff + len > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + len > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			for (int i = 0; i < len; i++)
			{
				output[outOff + i] = (input[inOff + i] ^ this.GetByte());
			}
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00094CDB File Offset: 0x00093CDB
		public void Reset()
		{
			this.idx = 0;
			this.Init();
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00094CEA File Offset: 0x00093CEA
		public byte ReturnByte(byte input)
		{
			return input ^ this.GetByte();
		}

		// Token: 0x04001108 RID: 4360
		private uint[] p = new uint[512];

		// Token: 0x04001109 RID: 4361
		private uint[] q = new uint[512];

		// Token: 0x0400110A RID: 4362
		private uint cnt;

		// Token: 0x0400110B RID: 4363
		private byte[] key;

		// Token: 0x0400110C RID: 4364
		private byte[] iv;

		// Token: 0x0400110D RID: 4365
		private bool initialised;

		// Token: 0x0400110E RID: 4366
		private byte[] buf = new byte[4];

		// Token: 0x0400110F RID: 4367
		private int idx;
	}
}
