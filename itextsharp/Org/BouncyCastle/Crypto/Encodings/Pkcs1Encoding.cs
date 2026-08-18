using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x02000619 RID: 1561
	public class Pkcs1Encoding : IAsymmetricBlockCipher
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x00149FC4 File Offset: 0x00148FC4
		// (set) Token: 0x06003519 RID: 13593 RVA: 0x00149FCD File Offset: 0x00148FCD
		public static bool StrictLengthEnabled
		{
			get
			{
				return Pkcs1Encoding.strictLengthEnabled[0];
			}
			set
			{
				Pkcs1Encoding.strictLengthEnabled[0] = value;
			}
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x00149FD8 File Offset: 0x00148FD8
		static Pkcs1Encoding()
		{
			string environmentVariable = Platform.GetEnvironmentVariable("Org.BouncyCastle.Pkcs1.Strict");
			Pkcs1Encoding.strictLengthEnabled = new bool[]
			{
				environmentVariable == null || environmentVariable.Equals("true")
			};
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x0014A011 File Offset: 0x00149011
		public Pkcs1Encoding(IAsymmetricBlockCipher cipher)
		{
			this.engine = cipher;
			this.useStrictLength = Pkcs1Encoding.StrictLengthEnabled;
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x0014A02B File Offset: 0x0014902B
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x0014A033 File Offset: 0x00149033
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/PKCS1Padding";
			}
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x0014A04C File Offset: 0x0014904C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.random = parametersWithRandom.Random;
				asymmetricKeyParameter = (AsymmetricKeyParameter)parametersWithRandom.Parameters;
			}
			else
			{
				this.random = new SecureRandom();
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			this.engine.Init(forEncryption, parameters);
			this.forPrivateKey = asymmetricKeyParameter.IsPrivate;
			this.forEncryption = forEncryption;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x0014A0B4 File Offset: 0x001490B4
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (!this.forEncryption)
			{
				return inputBlockSize;
			}
			return inputBlockSize - 10;
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x0014A0DC File Offset: 0x001490DC
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (!this.forEncryption)
			{
				return outputBlockSize - 10;
			}
			return outputBlockSize;
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x0014A103 File Offset: 0x00149103
		public byte[] ProcessBlock(byte[] input, int inOff, int length)
		{
			if (!this.forEncryption)
			{
				return this.DecodeBlock(input, inOff, length);
			}
			return this.EncodeBlock(input, inOff, length);
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x0014A120 File Offset: 0x00149120
		private byte[] EncodeBlock(byte[] input, int inOff, int inLen)
		{
			if (inLen > this.GetInputBlockSize())
			{
				throw new ArgumentException("input data too large", "inLen");
			}
			byte[] array = new byte[this.engine.GetInputBlockSize()];
			if (this.forPrivateKey)
			{
				array[0] = 1;
				for (int num = 1; num != array.Length - inLen - 1; num++)
				{
					array[num] = byte.MaxValue;
				}
			}
			else
			{
				this.random.NextBytes(array);
				array[0] = 2;
				for (int num2 = 1; num2 != array.Length - inLen - 1; num2++)
				{
					while (array[num2] == 0)
					{
						array[num2] = (byte)this.random.NextInt();
					}
				}
			}
			array[array.Length - inLen - 1] = 0;
			Array.Copy(input, inOff, array, array.Length - inLen, inLen);
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x0014A1E0 File Offset: 0x001491E0
		private byte[] DecodeBlock(byte[] input, int inOff, int inLen)
		{
			byte[] array = this.engine.ProcessBlock(input, inOff, inLen);
			if (array.Length < this.GetOutputBlockSize())
			{
				throw new InvalidCipherTextException("block truncated");
			}
			byte b = array[0];
			if (b != 1 && b != 2)
			{
				throw new InvalidCipherTextException("unknown block type");
			}
			if (this.useStrictLength && array.Length != this.engine.GetOutputBlockSize())
			{
				throw new InvalidCipherTextException("block incorrect size");
			}
			int num;
			for (num = 1; num != array.Length; num++)
			{
				byte b2 = array[num];
				if (b2 == 0)
				{
					break;
				}
				if (b == 1 && b2 != 255)
				{
					throw new InvalidCipherTextException("block padding incorrect");
				}
			}
			num++;
			if (num > array.Length || num < 10)
			{
				throw new InvalidCipherTextException("no data in block");
			}
			byte[] array2 = new byte[array.Length - num];
			Array.Copy(array, num, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x04002383 RID: 9091
		public const string StrictLengthEnabledProperty = "Org.BouncyCastle.Pkcs1.Strict";

		// Token: 0x04002384 RID: 9092
		private const int HeaderLength = 10;

		// Token: 0x04002385 RID: 9093
		private static readonly bool[] strictLengthEnabled;

		// Token: 0x04002386 RID: 9094
		private SecureRandom random;

		// Token: 0x04002387 RID: 9095
		private IAsymmetricBlockCipher engine;

		// Token: 0x04002388 RID: 9096
		private bool forEncryption;

		// Token: 0x04002389 RID: 9097
		private bool forPrivateKey;

		// Token: 0x0400238A RID: 9098
		private bool useStrictLength;
	}
}
