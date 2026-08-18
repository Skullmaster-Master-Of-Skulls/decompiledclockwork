using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200012A RID: 298
	public class HC256Engine : IStreamCipher
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00038FA0 File Offset: 0x00037FA0
		private uint Step()
		{
			uint num = this.cnt & 1023U;
			uint result;
			if (this.cnt < 1024U)
			{
				uint num2 = this.p[(int)((UIntPtr)(num - 3U & 1023U))];
				uint num3 = this.p[(int)((UIntPtr)(num - 1023U & 1023U))];
				this.p[(int)((UIntPtr)num)] += this.p[(int)((UIntPtr)(num - 10U & 1023U))] + (HC256Engine.RotateRight(num2, 10) ^ HC256Engine.RotateRight(num3, 23)) + this.q[(int)((UIntPtr)((num2 ^ num3) & 1023U))];
				num2 = this.p[(int)((UIntPtr)(num - 12U & 1023U))];
				result = (this.q[(int)((UIntPtr)(num2 & 255U))] + this.q[(int)((UIntPtr)((num2 >> 8 & 255U) + 256U))] + this.q[(int)((UIntPtr)((num2 >> 16 & 255U) + 512U))] + this.q[(int)((UIntPtr)((num2 >> 24 & 255U) + 768U))] ^ this.p[(int)((UIntPtr)num)]);
			}
			else
			{
				uint num4 = this.q[(int)((UIntPtr)(num - 3U & 1023U))];
				uint num5 = this.q[(int)((UIntPtr)(num - 1023U & 1023U))];
				this.q[(int)((UIntPtr)num)] += this.q[(int)((UIntPtr)(num - 10U & 1023U))] + (HC256Engine.RotateRight(num4, 10) ^ HC256Engine.RotateRight(num5, 23)) + this.p[(int)((UIntPtr)((num4 ^ num5) & 1023U))];
				num4 = this.q[(int)((UIntPtr)(num - 12U & 1023U))];
				result = (this.p[(int)((UIntPtr)(num4 & 255U))] + this.p[(int)((UIntPtr)((num4 >> 8 & 255U) + 256U))] + this.p[(int)((UIntPtr)((num4 >> 16 & 255U) + 512U))] + this.p[(int)((UIntPtr)((num4 >> 24 & 255U) + 768U))] ^ this.q[(int)((UIntPtr)num)]);
			}
			this.cnt = (this.cnt + 1U & 2047U);
			return result;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000391CC File Offset: 0x000381CC
		private void Init()
		{
			if (this.key.Length != 32 && this.key.Length != 16)
			{
				throw new ArgumentException("The key must be 128/256 bits long");
			}
			if (this.iv.Length < 16)
			{
				throw new ArgumentException("The IV must be at least 128 bits long");
			}
			if (this.key.Length != 32)
			{
				byte[] destinationArray = new byte[32];
				Array.Copy(this.key, 0, destinationArray, 0, this.key.Length);
				Array.Copy(this.key, 0, destinationArray, 16, this.key.Length);
				this.key = destinationArray;
			}
			if (this.iv.Length < 32)
			{
				byte[] array = new byte[32];
				Array.Copy(this.iv, 0, array, 0, this.iv.Length);
				Array.Copy(this.iv, 0, array, this.iv.Length, array.Length - this.iv.Length);
				this.iv = array;
			}
			this.cnt = 0U;
			uint[] array2 = new uint[2560];
			for (int i = 0; i < 32; i++)
			{
				array2[i >> 2] |= (uint)((uint)this.key[i] << 8 * (i & 3));
			}
			for (int j = 0; j < 32; j++)
			{
				array2[(j >> 2) + 8] |= (uint)((uint)this.iv[j] << 8 * (j & 3));
			}
			for (uint num = 16U; num < 2560U; num += 1U)
			{
				uint num2 = array2[(int)((UIntPtr)(num - 2U))];
				uint num3 = array2[(int)((UIntPtr)(num - 15U))];
				array2[(int)((UIntPtr)num)] = (HC256Engine.RotateRight(num2, 17) ^ HC256Engine.RotateRight(num2, 19) ^ num2 >> 10) + array2[(int)((UIntPtr)(num - 7U))] + (HC256Engine.RotateRight(num3, 7) ^ HC256Engine.RotateRight(num3, 18) ^ num3 >> 3) + array2[(int)((UIntPtr)(num - 16U))] + num;
			}
			Array.Copy(array2, 512, this.p, 0, 1024);
			Array.Copy(array2, 1536, this.q, 0, 1024);
			for (int k = 0; k < 4096; k++)
			{
				this.Step();
			}
			this.cnt = 0U;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x000393F0 File Offset: 0x000383F0
		public string AlgorithmName
		{
			get
			{
				return "HC-256";
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000393F8 File Offset: 0x000383F8
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
			throw new ArgumentException("Invalid parameter passed to HC256 init - " + parameters.GetType().Name, "parameters");
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00039484 File Offset: 0x00038484
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

		// Token: 0x06000ADE RID: 2782 RVA: 0x000394CC File Offset: 0x000384CC
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

		// Token: 0x06000ADF RID: 2783 RVA: 0x0003953F File Offset: 0x0003853F
		public void Reset()
		{
			this.idx = 0;
			this.Init();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003954E File Offset: 0x0003854E
		public byte ReturnByte(byte input)
		{
			return input ^ this.GetByte();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00039559 File Offset: 0x00038559
		private static uint RotateRight(uint x, int bits)
		{
			return x >> bits | x << -bits;
		}

		// Token: 0x04000896 RID: 2198
		private uint[] p = new uint[1024];

		// Token: 0x04000897 RID: 2199
		private uint[] q = new uint[1024];

		// Token: 0x04000898 RID: 2200
		private uint cnt;

		// Token: 0x04000899 RID: 2201
		private byte[] key;

		// Token: 0x0400089A RID: 2202
		private byte[] iv;

		// Token: 0x0400089B RID: 2203
		private bool initialised;

		// Token: 0x0400089C RID: 2204
		private byte[] buf = new byte[4];

		// Token: 0x0400089D RID: 2205
		private int idx;
	}
}
