using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020002AB RID: 683
	public class BufferedStreamCipher : BufferedCipherBase
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x00099BC5 File Offset: 0x00098BC5
		public BufferedStreamCipher(IStreamCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00099BE2 File Offset: 0x00098BE2
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00099BEF File Offset: 0x00098BEF
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00099C13 File Offset: 0x00098C13
		public override int GetBlockSize()
		{
			return 0;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00099C16 File Offset: 0x00098C16
		public override int GetOutputSize(int inputLen)
		{
			return inputLen;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00099C19 File Offset: 0x00098C19
		public override int GetUpdateOutputSize(int inputLen)
		{
			return inputLen;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00099C1C File Offset: 0x00098C1C
		public override byte[] ProcessByte(byte input)
		{
			return new byte[]
			{
				this.cipher.ReturnByte(input)
			};
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00099C40 File Offset: 0x00098C40
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			if (outOff >= output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			output[outOff] = this.cipher.ReturnByte(input);
			return 1;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00099C64 File Offset: 0x00098C64
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return null;
			}
			byte[] array = new byte[length];
			this.cipher.ProcessBytes(input, inOff, length, array, 0);
			return array;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00099C8F File Offset: 0x00098C8F
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 1)
			{
				return 0;
			}
			if (length > 0)
			{
				this.cipher.ProcessBytes(input, inOff, length, output, outOff);
			}
			return length;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00099CAE File Offset: 0x00098CAE
		public override byte[] DoFinal()
		{
			this.Reset();
			return BufferedCipherBase.EmptyBuffer;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00099CBC File Offset: 0x00098CBC
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return BufferedCipherBase.EmptyBuffer;
			}
			byte[] result = this.ProcessBytes(input, inOff, length);
			this.Reset();
			return result;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00099CE4 File Offset: 0x00098CE4
		public override void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04001144 RID: 4420
		private readonly IStreamCipher cipher;
	}
}
