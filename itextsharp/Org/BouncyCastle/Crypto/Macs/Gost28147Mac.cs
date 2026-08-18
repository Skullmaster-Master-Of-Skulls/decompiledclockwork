using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000089 RID: 137
	public class Gost28147Mac : IMac
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x00016550 File Offset: 0x00015550
		public Gost28147Mac()
		{
			this.mac = new byte[8];
			this.buf = new byte[8];
			this.bufOff = 0;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000165A4 File Offset: 0x000155A4
		private static int[] generateWorkingKey(byte[] userKey)
		{
			if (userKey.Length != 32)
			{
				throw new ArgumentException("Key length invalid. Key needs to be 32 byte - 256 bit!!!");
			}
			int[] array = new int[8];
			for (int num = 0; num != 8; num++)
			{
				array[num] = Gost28147Mac.bytesToint(userKey, num * 4);
			}
			return array;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000165E4 File Offset: 0x000155E4
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.buf = new byte[8];
			if (parameters is ParametersWithSBox)
			{
				ParametersWithSBox parametersWithSBox = (ParametersWithSBox)parameters;
				parametersWithSBox.GetSBox().CopyTo(this.S, 0);
				if (parametersWithSBox.Parameters != null)
				{
					this.workingKey = Gost28147Mac.generateWorkingKey(((KeyParameter)parametersWithSBox.Parameters).GetKey());
					return;
				}
				return;
			}
			else
			{
				if (parameters is KeyParameter)
				{
					this.workingKey = Gost28147Mac.generateWorkingKey(((KeyParameter)parameters).GetKey());
					return;
				}
				throw new ArgumentException("invalid parameter passed to Gost28147 init - " + parameters.GetType().Name);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00016682 File Offset: 0x00015682
		public string AlgorithmName
		{
			get
			{
				return "Gost28147Mac";
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00016689 File Offset: 0x00015689
		public int GetMacSize()
		{
			return 4;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001668C File Offset: 0x0001568C
		private int gost28147_mainStep(int n1, int key)
		{
			int num = key + n1;
			int num2 = (int)this.S[num & 15];
			num2 += (int)this.S[16 + (num >> 4 & 15)] << 4;
			num2 += (int)this.S[32 + (num >> 8 & 15)] << 8;
			num2 += (int)this.S[48 + (num >> 12 & 15)] << 12;
			num2 += (int)this.S[64 + (num >> 16 & 15)] << 16;
			num2 += (int)this.S[80 + (num >> 20 & 15)] << 20;
			num2 += (int)this.S[96 + (num >> 24 & 15)] << 24;
			num2 += (int)this.S[112 + (num >> 28 & 15)] << 28;
			int num3 = num2 << 11;
			int num4 = (int)((uint)num2 >> 21);
			return num3 | num4;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00016754 File Offset: 0x00015754
		private void gost28147MacFunc(int[] workingKey, byte[] input, int inOff, byte[] output, int outOff)
		{
			int num = Gost28147Mac.bytesToint(input, inOff);
			int num2 = Gost28147Mac.bytesToint(input, inOff + 4);
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					int num3 = num;
					num = (num2 ^ this.gost28147_mainStep(num, workingKey[j]));
					num2 = num3;
				}
			}
			Gost28147Mac.intTobytes(num, output, outOff);
			Gost28147Mac.intTobytes(num2, output, outOff + 4);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000167B7 File Offset: 0x000157B7
		private static int bytesToint(byte[] input, int inOff)
		{
			return (int)((long)((long)input[inOff + 3] << 24) & (long)((ulong)-16777216)) + ((int)input[inOff + 2] << 16 & 16711680) + ((int)input[inOff + 1] << 8 & 65280) + (int)(input[inOff] & byte.MaxValue);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000167F1 File Offset: 0x000157F1
		private static void intTobytes(int num, byte[] output, int outOff)
		{
			output[outOff + 3] = (byte)(num >> 24);
			output[outOff + 2] = (byte)(num >> 16);
			output[outOff + 1] = (byte)(num >> 8);
			output[outOff] = (byte)num;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00016818 File Offset: 0x00015818
		private static byte[] CM5func(byte[] buf, int bufOff, byte[] mac)
		{
			byte[] array = new byte[buf.Length - bufOff];
			Array.Copy(buf, bufOff, array, 0, mac.Length);
			for (int num = 0; num != mac.Length; num++)
			{
				array[num] ^= mac[num];
			}
			return array;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00016858 File Offset: 0x00015858
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				byte[] array = new byte[this.buf.Length];
				Array.Copy(this.buf, 0, array, 0, this.mac.Length);
				if (this.firstStep)
				{
					this.firstStep = false;
				}
				else
				{
					array = Gost28147Mac.CM5func(this.buf, 0, this.mac);
				}
				this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000168F4 File Offset: 0x000158F4
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = 8 - this.bufOff;
			if (len > num)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num);
				byte[] array = new byte[this.buf.Length];
				Array.Copy(this.buf, 0, array, 0, this.mac.Length);
				if (this.firstStep)
				{
					this.firstStep = false;
				}
				else
				{
					array = Gost28147Mac.CM5func(this.buf, 0, this.mac);
				}
				this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > 8)
				{
					array = Gost28147Mac.CM5func(input, inOff, this.mac);
					this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
					len -= 8;
					inOff += 8;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000169F8 File Offset: 0x000159F8
		public int DoFinal(byte[] output, int outOff)
		{
			while (this.bufOff < 8)
			{
				this.buf[this.bufOff++] = 0;
			}
			byte[] array = new byte[this.buf.Length];
			Array.Copy(this.buf, 0, array, 0, this.mac.Length);
			if (this.firstStep)
			{
				this.firstStep = false;
			}
			else
			{
				array = Gost28147Mac.CM5func(this.buf, 0, this.mac);
			}
			this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
			Array.Copy(this.mac, this.mac.Length / 2 - 4, output, outOff, 4);
			this.Reset();
			return 4;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00016AA7 File Offset: 0x00015AA7
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.firstStep = true;
		}

		// Token: 0x04000221 RID: 545
		private const int blockSize = 8;

		// Token: 0x04000222 RID: 546
		private const int macSize = 4;

		// Token: 0x04000223 RID: 547
		private int bufOff;

		// Token: 0x04000224 RID: 548
		private byte[] buf;

		// Token: 0x04000225 RID: 549
		private byte[] mac;

		// Token: 0x04000226 RID: 550
		private bool firstStep = true;

		// Token: 0x04000227 RID: 551
		private int[] workingKey;

		// Token: 0x04000228 RID: 552
		private byte[] S = new byte[]
		{
			9,
			6,
			3,
			2,
			8,
			11,
			1,
			7,
			10,
			4,
			14,
			15,
			12,
			0,
			13,
			5,
			3,
			7,
			14,
			9,
			8,
			10,
			15,
			0,
			5,
			2,
			6,
			12,
			11,
			4,
			13,
			1,
			14,
			4,
			6,
			2,
			11,
			3,
			13,
			8,
			12,
			15,
			5,
			10,
			0,
			7,
			1,
			9,
			14,
			7,
			10,
			12,
			13,
			1,
			3,
			9,
			0,
			2,
			11,
			4,
			15,
			8,
			5,
			6,
			11,
			5,
			1,
			9,
			8,
			13,
			15,
			0,
			14,
			4,
			2,
			3,
			12,
			7,
			10,
			6,
			3,
			10,
			13,
			12,
			1,
			2,
			0,
			11,
			7,
			5,
			9,
			4,
			8,
			15,
			14,
			6,
			1,
			13,
			2,
			9,
			7,
			10,
			6,
			0,
			8,
			12,
			4,
			5,
			15,
			3,
			11,
			14,
			11,
			10,
			15,
			5,
			0,
			12,
			14,
			8,
			6,
			2,
			3,
			9,
			1,
			7,
			13,
			4
		};
	}
}
