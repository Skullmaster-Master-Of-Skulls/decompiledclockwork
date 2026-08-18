using System;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200001F RID: 31
	public class Salsa20Engine : IStreamCipher
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000610C File Offset: 0x0000510C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			ParametersWithIV parametersWithIV = parameters as ParametersWithIV;
			if (parametersWithIV == null)
			{
				throw new ArgumentException("Salsa20 Init requires an IV", "parameters");
			}
			byte[] iv = parametersWithIV.GetIV();
			if (iv == null || iv.Length != 8)
			{
				throw new ArgumentException("Salsa20 requires exactly 8 bytes of IV");
			}
			KeyParameter keyParameter = parametersWithIV.Parameters as KeyParameter;
			if (keyParameter == null)
			{
				throw new ArgumentException("Salsa20 Init requires a key", "parameters");
			}
			this.workingKey = keyParameter.GetKey();
			this.workingIV = iv;
			this.setKey(this.workingKey, this.workingIV);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00006192 File Offset: 0x00005192
		public string AlgorithmName
		{
			get
			{
				return "Salsa20";
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000619C File Offset: 0x0000519C
		public byte ReturnByte(byte input)
		{
			if (this.limitExceeded())
			{
				throw new MaxBytesExceededException("2^70 byte limit per IV; Change IV");
			}
			if (this.index == 0)
			{
				this.salsa20WordToByte(this.engineState, this.keyStream);
				this.engineState[8]++;
				if (this.engineState[8] == 0)
				{
					this.engineState[9]++;
				}
			}
			byte result = this.keyStream[this.index] ^ input;
			this.index = (this.index + 1 & 63);
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006238 File Offset: 0x00005238
		public void ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			if (inOff + len > inBytes.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + len > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (this.limitExceeded(len))
			{
				throw new MaxBytesExceededException("2^70 byte limit per IV would be exceeded; Change IV");
			}
			for (int i = 0; i < len; i++)
			{
				if (this.index == 0)
				{
					this.salsa20WordToByte(this.engineState, this.keyStream);
					this.engineState[8]++;
					if (this.engineState[8] == 0)
					{
						this.engineState[9]++;
					}
				}
				outBytes[i + outOff] = (this.keyStream[this.index] ^ inBytes[i + inOff]);
				this.index = (this.index + 1 & 63);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006334 File Offset: 0x00005334
		public void Reset()
		{
			this.setKey(this.workingKey, this.workingIV);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006348 File Offset: 0x00005348
		private void setKey(byte[] keyBytes, byte[] ivBytes)
		{
			this.workingKey = keyBytes;
			this.workingIV = ivBytes;
			this.index = 0;
			this.resetCounter();
			int num = 0;
			this.engineState[1] = this.byteToIntLittle(this.workingKey, 0);
			this.engineState[2] = this.byteToIntLittle(this.workingKey, 4);
			this.engineState[3] = this.byteToIntLittle(this.workingKey, 8);
			this.engineState[4] = this.byteToIntLittle(this.workingKey, 12);
			byte[] array;
			if (this.workingKey.Length == 32)
			{
				array = Salsa20Engine.sigma;
				num = 16;
			}
			else
			{
				array = Salsa20Engine.tau;
			}
			this.engineState[11] = this.byteToIntLittle(this.workingKey, num);
			this.engineState[12] = this.byteToIntLittle(this.workingKey, num + 4);
			this.engineState[13] = this.byteToIntLittle(this.workingKey, num + 8);
			this.engineState[14] = this.byteToIntLittle(this.workingKey, num + 12);
			this.engineState[0] = this.byteToIntLittle(array, 0);
			this.engineState[5] = this.byteToIntLittle(array, 4);
			this.engineState[10] = this.byteToIntLittle(array, 8);
			this.engineState[15] = this.byteToIntLittle(array, 12);
			this.engineState[6] = this.byteToIntLittle(this.workingIV, 0);
			this.engineState[7] = this.byteToIntLittle(this.workingIV, 4);
			this.engineState[8] = (this.engineState[9] = 0);
			this.initialised = true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000064CC File Offset: 0x000054CC
		private void salsa20WordToByte(int[] input, byte[] output)
		{
			Array.Copy(input, 0, this.x, 0, input.Length);
			for (int i = 0; i < 10; i++)
			{
				this.x[4] ^= this.rotl(this.x[0] + this.x[12], 7);
				this.x[8] ^= this.rotl(this.x[4] + this.x[0], 9);
				this.x[12] ^= this.rotl(this.x[8] + this.x[4], 13);
				this.x[0] ^= this.rotl(this.x[12] + this.x[8], 18);
				this.x[9] ^= this.rotl(this.x[5] + this.x[1], 7);
				this.x[13] ^= this.rotl(this.x[9] + this.x[5], 9);
				this.x[1] ^= this.rotl(this.x[13] + this.x[9], 13);
				this.x[5] ^= this.rotl(this.x[1] + this.x[13], 18);
				this.x[14] ^= this.rotl(this.x[10] + this.x[6], 7);
				this.x[2] ^= this.rotl(this.x[14] + this.x[10], 9);
				this.x[6] ^= this.rotl(this.x[2] + this.x[14], 13);
				this.x[10] ^= this.rotl(this.x[6] + this.x[2], 18);
				this.x[3] ^= this.rotl(this.x[15] + this.x[11], 7);
				this.x[7] ^= this.rotl(this.x[3] + this.x[15], 9);
				this.x[11] ^= this.rotl(this.x[7] + this.x[3], 13);
				this.x[15] ^= this.rotl(this.x[11] + this.x[7], 18);
				this.x[1] ^= this.rotl(this.x[0] + this.x[3], 7);
				this.x[2] ^= this.rotl(this.x[1] + this.x[0], 9);
				this.x[3] ^= this.rotl(this.x[2] + this.x[1], 13);
				this.x[0] ^= this.rotl(this.x[3] + this.x[2], 18);
				this.x[6] ^= this.rotl(this.x[5] + this.x[4], 7);
				this.x[7] ^= this.rotl(this.x[6] + this.x[5], 9);
				this.x[4] ^= this.rotl(this.x[7] + this.x[6], 13);
				this.x[5] ^= this.rotl(this.x[4] + this.x[7], 18);
				this.x[11] ^= this.rotl(this.x[10] + this.x[9], 7);
				this.x[8] ^= this.rotl(this.x[11] + this.x[10], 9);
				this.x[9] ^= this.rotl(this.x[8] + this.x[11], 13);
				this.x[10] ^= this.rotl(this.x[9] + this.x[8], 18);
				this.x[12] ^= this.rotl(this.x[15] + this.x[14], 7);
				this.x[13] ^= this.rotl(this.x[12] + this.x[15], 9);
				this.x[14] ^= this.rotl(this.x[13] + this.x[12], 13);
				this.x[15] ^= this.rotl(this.x[14] + this.x[13], 18);
			}
			int num = 0;
			for (int j = 0; j < 16; j++)
			{
				this.intToByteLittle(this.x[j] + input[j], output, num);
				num += 4;
			}
			for (int k = 16; k < this.x.Length; k++)
			{
				this.intToByteLittle(this.x[k], output, num);
				num += 4;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006B90 File Offset: 0x00005B90
		private byte[] intToByteLittle(int x, byte[] bs, int off)
		{
			bs[off] = (byte)x;
			bs[off + 1] = (byte)(x >> 8);
			bs[off + 2] = (byte)(x >> 16);
			bs[off + 3] = (byte)(x >> 24);
			return bs;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006BB5 File Offset: 0x00005BB5
		private int rotl(int x, int y)
		{
			return x << y | (int)((uint)x >> -y);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006BC5 File Offset: 0x00005BC5
		private int byteToIntLittle(byte[] x, int offset)
		{
			return (int)(x[offset] & byte.MaxValue) | (int)(x[offset + 1] & byte.MaxValue) << 8 | (int)(x[offset + 2] & byte.MaxValue) << 16 | (int)x[offset + 3] << 24;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006BF6 File Offset: 0x00005BF6
		private void resetCounter()
		{
			this.cW0 = 0;
			this.cW1 = 0;
			this.cW2 = 0;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006C10 File Offset: 0x00005C10
		private bool limitExceeded()
		{
			this.cW0++;
			if (this.cW0 == 0)
			{
				this.cW1++;
				if (this.cW1 == 0)
				{
					this.cW2++;
					return (this.cW2 & 32) != 0;
				}
			}
			return false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006C68 File Offset: 0x00005C68
		private bool limitExceeded(int len)
		{
			if (this.cW0 >= 0)
			{
				this.cW0 += len;
			}
			else
			{
				this.cW0 += len;
				if (this.cW0 >= 0)
				{
					this.cW1++;
					if (this.cW1 == 0)
					{
						this.cW2++;
						return (this.cW2 & 32) != 0;
					}
				}
			}
			return false;
		}

		// Token: 0x0400005E RID: 94
		private const int stateSize = 16;

		// Token: 0x0400005F RID: 95
		private static readonly byte[] sigma = Encoding.ASCII.GetBytes("expand 32-byte k");

		// Token: 0x04000060 RID: 96
		private static readonly byte[] tau = Encoding.ASCII.GetBytes("expand 16-byte k");

		// Token: 0x04000061 RID: 97
		private int index;

		// Token: 0x04000062 RID: 98
		private int[] engineState = new int[16];

		// Token: 0x04000063 RID: 99
		private int[] x = new int[16];

		// Token: 0x04000064 RID: 100
		private byte[] keyStream = new byte[64];

		// Token: 0x04000065 RID: 101
		private byte[] workingKey;

		// Token: 0x04000066 RID: 102
		private byte[] workingIV;

		// Token: 0x04000067 RID: 103
		private bool initialised;

		// Token: 0x04000068 RID: 104
		private int cW0;

		// Token: 0x04000069 RID: 105
		private int cW1;

		// Token: 0x0400006A RID: 106
		private int cW2;
	}
}
