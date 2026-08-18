using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020004C8 RID: 1224
	public class IsaacEngine : IStreamCipher
	{
		// Token: 0x060029B8 RID: 10680 RVA: 0x000FDEF4 File Offset: 0x000FCEF4
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to ISAAC Init - " + parameters.GetType().Name, "parameters");
			}
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000FDF3C File Offset: 0x000FCF3C
		public byte ReturnByte(byte input)
		{
			if (this.index == 0)
			{
				this.isaac();
				this.keyStream = this.intToByteLittle(this.results);
			}
			byte result = this.keyStream[this.index] ^ input;
			this.index = (this.index + 1 & 1023);
			return result;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000FDF90 File Offset: 0x000FCF90
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
				if (this.index == 0)
				{
					this.isaac();
					this.keyStream = this.intToByteLittle(this.results);
				}
				output[i + outOff] = (this.keyStream[this.index] ^ input[i + inOff]);
				this.index = (this.index + 1 & 1023);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000FE03E File Offset: 0x000FD03E
		public string AlgorithmName
		{
			get
			{
				return "ISAAC";
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000FE045 File Offset: 0x000FD045
		public void Reset()
		{
			this.setKey(this.workingKey);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000FE054 File Offset: 0x000FD054
		private void setKey(byte[] keyBytes)
		{
			this.workingKey = keyBytes;
			if (this.engineState == null)
			{
				this.engineState = new uint[IsaacEngine.stateArraySize];
			}
			if (this.results == null)
			{
				this.results = new uint[IsaacEngine.stateArraySize];
			}
			for (int i = 0; i < IsaacEngine.stateArraySize; i++)
			{
				this.engineState[i] = (this.results[i] = 0U);
			}
			this.a = (this.b = (this.c = 0U));
			this.index = 0;
			byte[] array = new byte[keyBytes.Length + (keyBytes.Length & 3)];
			Array.Copy(keyBytes, 0, array, 0, keyBytes.Length);
			for (int i = 0; i < array.Length; i += 4)
			{
				this.results[i >> 2] = this.byteToIntLittle(array, i);
			}
			uint[] array2 = new uint[IsaacEngine.sizeL];
			for (int i = 0; i < IsaacEngine.sizeL; i++)
			{
				array2[i] = 2654435769U;
			}
			for (int i = 0; i < 4; i++)
			{
				this.mix(array2);
			}
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < IsaacEngine.stateArraySize; j += IsaacEngine.sizeL)
				{
					for (int k = 0; k < IsaacEngine.sizeL; k++)
					{
						array2[k] += ((i < 1) ? this.results[j + k] : this.engineState[j + k]);
					}
					this.mix(array2);
					for (int k = 0; k < IsaacEngine.sizeL; k++)
					{
						this.engineState[j + k] = array2[k];
					}
				}
			}
			this.isaac();
			this.initialised = true;
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000FE1E8 File Offset: 0x000FD1E8
		private void isaac()
		{
			this.b += (this.c += 1U);
			for (int i = 0; i < IsaacEngine.stateArraySize; i++)
			{
				uint num = this.engineState[i];
				switch (i & 3)
				{
				case 0:
					this.a ^= this.a << 13;
					break;
				case 1:
					this.a ^= this.a >> 6;
					break;
				case 2:
					this.a ^= this.a << 2;
					break;
				case 3:
					this.a ^= this.a >> 16;
					break;
				}
				this.a += this.engineState[i + 128 & 255];
				uint num2 = this.engineState[i] = this.engineState[(int)(num >> 2 & 255U)] + this.a + this.b;
				this.results[i] = (this.b = this.engineState[(int)(num2 >> 10 & 255U)] + num);
			}
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000FE31C File Offset: 0x000FD31C
		private void mix(uint[] x)
		{
			x[0] ^= x[1] << 11;
			x[3] += x[0];
			x[1] += x[2];
			x[1] ^= x[2] >> 2;
			x[4] += x[1];
			x[2] += x[3];
			x[2] ^= x[3] << 8;
			x[5] += x[2];
			x[3] += x[4];
			x[3] ^= x[4] >> 16;
			x[6] += x[3];
			x[4] += x[5];
			x[4] ^= x[5] << 10;
			x[7] += x[4];
			x[5] += x[6];
			x[5] ^= x[6] >> 4;
			x[0] += x[5];
			x[6] += x[7];
			x[6] ^= x[7] << 8;
			x[1] += x[6];
			x[7] += x[0];
			x[7] ^= x[0] >> 9;
			x[2] += x[7];
			x[0] += x[1];
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000FE550 File Offset: 0x000FD550
		private uint byteToIntLittle(byte[] x, int offset)
		{
			uint num = (uint)x[offset + 3];
			num = (num << 8 | (uint)x[offset + 2]);
			num = (num << 8 | (uint)x[offset + 1]);
			return num << 8 | (uint)x[offset];
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000FE580 File Offset: 0x000FD580
		private byte[] intToByteLittle(uint x)
		{
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				(byte)x
			};
			array[2] = (byte)(x >> 8);
			array[1] = (byte)(x >> 16);
			array[0] = (byte)(x >> 24);
			return array;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000FE5B4 File Offset: 0x000FD5B4
		private byte[] intToByteLittle(uint[] x)
		{
			byte[] array = new byte[4 * x.Length];
			int i = 0;
			int num = 0;
			while (i < x.Length)
			{
				Array.Copy(this.intToByteLittle(x[i]), 0, array, num, 4);
				i++;
				num += 4;
			}
			return array;
		}

		// Token: 0x04001D14 RID: 7444
		private static readonly int sizeL = 8;

		// Token: 0x04001D15 RID: 7445
		private static readonly int stateArraySize = IsaacEngine.sizeL << 5;

		// Token: 0x04001D16 RID: 7446
		private uint[] engineState;

		// Token: 0x04001D17 RID: 7447
		private uint[] results;

		// Token: 0x04001D18 RID: 7448
		private uint a;

		// Token: 0x04001D19 RID: 7449
		private uint b;

		// Token: 0x04001D1A RID: 7450
		private uint c;

		// Token: 0x04001D1B RID: 7451
		private int index;

		// Token: 0x04001D1C RID: 7452
		private byte[] keyStream = new byte[IsaacEngine.stateArraySize << 2];

		// Token: 0x04001D1D RID: 7453
		private byte[] workingKey;

		// Token: 0x04001D1E RID: 7454
		private bool initialised;
	}
}
