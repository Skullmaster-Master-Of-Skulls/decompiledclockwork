using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000551 RID: 1361
	public class Gost28147Engine : IBlockCipher
	{
		// Token: 0x06002ED4 RID: 11988 RVA: 0x00121A08 File Offset: 0x00120A08
		static Gost28147Engine()
		{
			Gost28147Engine.sBoxes.Add("E-TEST", Gost28147Engine.ESbox_Test);
			Gost28147Engine.sBoxes.Add("E-A", Gost28147Engine.ESbox_A);
			Gost28147Engine.sBoxes.Add("E-B", Gost28147Engine.ESbox_B);
			Gost28147Engine.sBoxes.Add("E-C", Gost28147Engine.ESbox_C);
			Gost28147Engine.sBoxes.Add("E-D", Gost28147Engine.ESbox_D);
			Gost28147Engine.sBoxes.Add("D-TEST", Gost28147Engine.DSbox_Test);
			Gost28147Engine.sBoxes.Add("D-A", Gost28147Engine.DSbox_A);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x00121C0C File Offset: 0x00120C0C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithSBox)
			{
				ParametersWithSBox parametersWithSBox = (ParametersWithSBox)parameters;
				Array.Copy(parametersWithSBox.GetSBox(), 0, this.S, 0, parametersWithSBox.GetSBox().Length);
				if (parametersWithSBox.Parameters != null)
				{
					this.workingKey = this.generateWorkingKey(forEncryption, ((KeyParameter)parametersWithSBox.Parameters).GetKey());
					return;
				}
				return;
			}
			else
			{
				if (parameters is KeyParameter)
				{
					this.workingKey = this.generateWorkingKey(forEncryption, ((KeyParameter)parameters).GetKey());
					return;
				}
				throw new ArgumentException("invalid parameter passed to Gost28147 init - " + parameters.GetType().Name);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x00121CA5 File Offset: 0x00120CA5
		public string AlgorithmName
		{
			get
			{
				return "Gost28147";
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x00121CAC File Offset: 0x00120CAC
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x00121CAF File Offset: 0x00120CAF
		public int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x00121CB4 File Offset: 0x00120CB4
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey == null)
			{
				throw new InvalidOperationException("Gost28147 engine not initialised");
			}
			if (inOff + 8 > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 8 > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.Gost28147Func(this.workingKey, input, inOff, output, outOff);
			return 8;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x00121D0D File Offset: 0x00120D0D
		public void Reset()
		{
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x00121D10 File Offset: 0x00120D10
		private int[] generateWorkingKey(bool forEncryption, byte[] userKey)
		{
			this.forEncryption = forEncryption;
			if (userKey.Length != 32)
			{
				throw new ArgumentException("Key length invalid. Key needs to be 32 byte - 256 bit!!!");
			}
			int[] array = new int[8];
			for (int num = 0; num != 8; num++)
			{
				array[num] = Gost28147Engine.bytesToint(userKey, num * 4);
			}
			return array;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x00121D58 File Offset: 0x00120D58
		private int Gost28147_mainStep(int n1, int key)
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

		// Token: 0x06002EDE RID: 11998 RVA: 0x00121E20 File Offset: 0x00120E20
		private void Gost28147Func(int[] workingKey, byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			int num = Gost28147Engine.bytesToint(inBytes, inOff);
			int num2 = Gost28147Engine.bytesToint(inBytes, inOff + 4);
			if (this.forEncryption)
			{
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						int num3 = num;
						int num4 = this.Gost28147_mainStep(num, workingKey[j]);
						num = (num2 ^ num4);
						num2 = num3;
					}
				}
				for (int k = 7; k > 0; k--)
				{
					int num3 = num;
					num = (num2 ^ this.Gost28147_mainStep(num, workingKey[k]));
					num2 = num3;
				}
			}
			else
			{
				for (int l = 0; l < 8; l++)
				{
					int num3 = num;
					num = (num2 ^ this.Gost28147_mainStep(num, workingKey[l]));
					num2 = num3;
				}
				for (int m = 0; m < 3; m++)
				{
					int num5 = 7;
					while (num5 >= 0 && (m != 2 || num5 != 0))
					{
						int num3 = num;
						num = (num2 ^ this.Gost28147_mainStep(num, workingKey[num5]));
						num2 = num3;
						num5--;
					}
				}
			}
			num2 ^= this.Gost28147_mainStep(num, workingKey[0]);
			Gost28147Engine.intTobytes(num, outBytes, outOff);
			Gost28147Engine.intTobytes(num2, outBytes, outOff + 4);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00121F1D File Offset: 0x00120F1D
		private static int bytesToint(byte[] inBytes, int inOff)
		{
			return (int)((long)((long)inBytes[inOff + 3] << 24) & (long)((ulong)-16777216)) + ((int)inBytes[inOff + 2] << 16 & 16711680) + ((int)inBytes[inOff + 1] << 8 & 65280) + (int)(inBytes[inOff] & byte.MaxValue);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x00121F57 File Offset: 0x00120F57
		private static void intTobytes(int num, byte[] outBytes, int outOff)
		{
			outBytes[outOff + 3] = (byte)(num >> 24);
			outBytes[outOff + 2] = (byte)(num >> 16);
			outBytes[outOff + 1] = (byte)(num >> 8);
			outBytes[outOff] = (byte)num;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x00121F7C File Offset: 0x00120F7C
		public static byte[] GetSBox(string sBoxName)
		{
			byte[] array = (byte[])Gost28147Engine.sBoxes[sBoxName.ToUpper(CultureInfo.InvariantCulture)];
			if (array == null)
			{
				throw new ArgumentException("Unknown S-Box - possible types: \"E-Test\", \"E-A\", \"E-B\", \"E-C\", \"E-D\", \"D-Test\", \"D-A\".");
			}
			return (byte[])array.Clone();
		}

		// Token: 0x04002034 RID: 8244
		private const int BlockSize = 8;

		// Token: 0x04002035 RID: 8245
		private int[] workingKey;

		// Token: 0x04002036 RID: 8246
		private bool forEncryption;

		// Token: 0x04002037 RID: 8247
		private readonly byte[] S = new byte[]
		{
			4,
			10,
			9,
			2,
			13,
			8,
			0,
			14,
			6,
			11,
			1,
			12,
			7,
			15,
			5,
			3,
			14,
			11,
			4,
			12,
			6,
			13,
			15,
			10,
			2,
			3,
			8,
			1,
			0,
			7,
			5,
			9,
			5,
			8,
			1,
			13,
			10,
			3,
			4,
			2,
			14,
			15,
			12,
			7,
			6,
			0,
			9,
			11,
			7,
			13,
			10,
			1,
			0,
			8,
			9,
			15,
			14,
			4,
			6,
			12,
			11,
			2,
			5,
			3,
			6,
			12,
			7,
			1,
			5,
			15,
			13,
			8,
			4,
			10,
			9,
			14,
			0,
			3,
			11,
			2,
			4,
			11,
			10,
			0,
			7,
			2,
			1,
			13,
			3,
			6,
			8,
			5,
			9,
			12,
			15,
			14,
			13,
			11,
			4,
			1,
			3,
			15,
			5,
			9,
			0,
			10,
			14,
			7,
			6,
			8,
			2,
			12,
			1,
			15,
			13,
			0,
			5,
			7,
			10,
			4,
			9,
			2,
			3,
			14,
			6,
			11,
			8,
			12
		};

		// Token: 0x04002038 RID: 8248
		private static readonly byte[] ESbox_Test = new byte[]
		{
			4,
			2,
			15,
			5,
			9,
			1,
			0,
			8,
			14,
			3,
			11,
			12,
			13,
			7,
			10,
			6,
			12,
			9,
			15,
			14,
			8,
			1,
			3,
			10,
			2,
			7,
			4,
			13,
			6,
			0,
			11,
			5,
			13,
			8,
			14,
			12,
			7,
			3,
			9,
			10,
			1,
			5,
			2,
			4,
			6,
			15,
			0,
			11,
			14,
			9,
			11,
			2,
			5,
			15,
			7,
			1,
			0,
			13,
			12,
			6,
			10,
			4,
			3,
			8,
			3,
			14,
			5,
			9,
			6,
			8,
			0,
			13,
			10,
			11,
			7,
			12,
			2,
			1,
			15,
			4,
			8,
			15,
			6,
			11,
			1,
			9,
			12,
			5,
			13,
			3,
			7,
			10,
			0,
			14,
			2,
			4,
			9,
			11,
			12,
			0,
			3,
			6,
			7,
			5,
			4,
			8,
			14,
			15,
			1,
			10,
			2,
			13,
			12,
			6,
			5,
			2,
			11,
			0,
			9,
			13,
			3,
			14,
			7,
			10,
			15,
			4,
			1,
			8
		};

		// Token: 0x04002039 RID: 8249
		private static readonly byte[] ESbox_A = new byte[]
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

		// Token: 0x0400203A RID: 8250
		private static readonly byte[] ESbox_B = new byte[]
		{
			8,
			4,
			11,
			1,
			3,
			5,
			0,
			9,
			2,
			14,
			10,
			12,
			13,
			6,
			7,
			15,
			0,
			1,
			2,
			10,
			4,
			13,
			5,
			12,
			9,
			7,
			3,
			15,
			11,
			8,
			6,
			14,
			14,
			12,
			0,
			10,
			9,
			2,
			13,
			11,
			7,
			5,
			8,
			15,
			3,
			6,
			1,
			4,
			7,
			5,
			0,
			13,
			11,
			6,
			1,
			2,
			3,
			10,
			12,
			15,
			4,
			14,
			9,
			8,
			2,
			7,
			12,
			15,
			9,
			5,
			10,
			11,
			1,
			4,
			0,
			13,
			6,
			8,
			14,
			3,
			8,
			3,
			2,
			6,
			4,
			13,
			14,
			11,
			12,
			1,
			7,
			15,
			10,
			0,
			9,
			5,
			5,
			2,
			10,
			11,
			9,
			1,
			12,
			3,
			7,
			4,
			13,
			0,
			6,
			15,
			8,
			14,
			0,
			4,
			11,
			14,
			8,
			3,
			7,
			1,
			10,
			2,
			9,
			6,
			15,
			13,
			5,
			12
		};

		// Token: 0x0400203B RID: 8251
		private static readonly byte[] ESbox_C = new byte[]
		{
			1,
			11,
			12,
			2,
			9,
			13,
			0,
			15,
			4,
			5,
			8,
			14,
			10,
			7,
			6,
			3,
			0,
			1,
			7,
			13,
			11,
			4,
			5,
			2,
			8,
			14,
			15,
			12,
			9,
			10,
			6,
			3,
			8,
			2,
			5,
			0,
			4,
			9,
			15,
			10,
			3,
			7,
			12,
			13,
			6,
			14,
			1,
			11,
			3,
			6,
			0,
			1,
			5,
			13,
			10,
			8,
			11,
			2,
			9,
			7,
			14,
			15,
			12,
			4,
			8,
			13,
			11,
			0,
			4,
			5,
			1,
			2,
			9,
			3,
			12,
			14,
			6,
			15,
			10,
			7,
			12,
			9,
			11,
			1,
			8,
			14,
			2,
			4,
			7,
			3,
			6,
			5,
			10,
			0,
			15,
			13,
			10,
			9,
			6,
			8,
			13,
			14,
			2,
			0,
			15,
			3,
			5,
			11,
			4,
			1,
			12,
			7,
			7,
			4,
			0,
			5,
			10,
			2,
			15,
			14,
			12,
			6,
			1,
			11,
			13,
			9,
			3,
			8
		};

		// Token: 0x0400203C RID: 8252
		private static readonly byte[] ESbox_D = new byte[]
		{
			15,
			12,
			2,
			10,
			6,
			4,
			5,
			0,
			7,
			9,
			14,
			13,
			1,
			11,
			8,
			3,
			11,
			6,
			3,
			4,
			12,
			15,
			14,
			2,
			7,
			13,
			8,
			0,
			5,
			10,
			9,
			1,
			1,
			12,
			11,
			0,
			15,
			14,
			6,
			5,
			10,
			13,
			4,
			8,
			9,
			3,
			7,
			2,
			1,
			5,
			14,
			12,
			10,
			7,
			0,
			13,
			6,
			2,
			11,
			4,
			9,
			3,
			15,
			8,
			0,
			12,
			8,
			9,
			13,
			2,
			10,
			11,
			7,
			3,
			6,
			5,
			4,
			14,
			15,
			1,
			8,
			0,
			15,
			3,
			2,
			5,
			14,
			11,
			1,
			10,
			4,
			7,
			12,
			9,
			13,
			6,
			3,
			0,
			6,
			15,
			1,
			14,
			9,
			2,
			13,
			8,
			12,
			4,
			11,
			10,
			5,
			7,
			1,
			10,
			6,
			8,
			15,
			11,
			0,
			4,
			12,
			3,
			5,
			9,
			7,
			13,
			2,
			14
		};

		// Token: 0x0400203D RID: 8253
		private static readonly byte[] DSbox_Test = new byte[]
		{
			4,
			10,
			9,
			2,
			13,
			8,
			0,
			14,
			6,
			11,
			1,
			12,
			7,
			15,
			5,
			3,
			14,
			11,
			4,
			12,
			6,
			13,
			15,
			10,
			2,
			3,
			8,
			1,
			0,
			7,
			5,
			9,
			5,
			8,
			1,
			13,
			10,
			3,
			4,
			2,
			14,
			15,
			12,
			7,
			6,
			0,
			9,
			11,
			7,
			13,
			10,
			1,
			0,
			8,
			9,
			15,
			14,
			4,
			6,
			12,
			11,
			2,
			5,
			3,
			6,
			12,
			7,
			1,
			5,
			15,
			13,
			8,
			4,
			10,
			9,
			14,
			0,
			3,
			11,
			2,
			4,
			11,
			10,
			0,
			7,
			2,
			1,
			13,
			3,
			6,
			8,
			5,
			9,
			12,
			15,
			14,
			13,
			11,
			4,
			1,
			3,
			15,
			5,
			9,
			0,
			10,
			14,
			7,
			6,
			8,
			2,
			12,
			1,
			15,
			13,
			0,
			5,
			7,
			10,
			4,
			9,
			2,
			3,
			14,
			6,
			11,
			8,
			12
		};

		// Token: 0x0400203E RID: 8254
		private static readonly byte[] DSbox_A = new byte[]
		{
			10,
			4,
			5,
			6,
			8,
			1,
			3,
			7,
			13,
			12,
			14,
			0,
			9,
			2,
			11,
			15,
			5,
			15,
			4,
			0,
			2,
			13,
			11,
			9,
			1,
			7,
			6,
			3,
			12,
			14,
			10,
			8,
			7,
			15,
			12,
			14,
			9,
			4,
			1,
			0,
			3,
			11,
			5,
			2,
			6,
			10,
			8,
			13,
			4,
			10,
			7,
			12,
			0,
			15,
			2,
			8,
			14,
			1,
			6,
			5,
			13,
			11,
			9,
			3,
			7,
			6,
			4,
			11,
			9,
			12,
			2,
			10,
			1,
			8,
			0,
			14,
			15,
			13,
			3,
			5,
			7,
			6,
			2,
			4,
			13,
			9,
			15,
			0,
			10,
			1,
			5,
			11,
			8,
			14,
			12,
			3,
			13,
			14,
			4,
			1,
			7,
			0,
			5,
			10,
			3,
			12,
			8,
			15,
			6,
			2,
			9,
			11,
			1,
			3,
			10,
			9,
			5,
			11,
			4,
			15,
			8,
			6,
			7,
			14,
			13,
			0,
			2,
			12
		};

		// Token: 0x0400203F RID: 8255
		private static readonly Hashtable sBoxes = new Hashtable();
	}
}
