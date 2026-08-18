using System;
using System.IO;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020001FA RID: 506
	public class BufferedIesCipher : BufferedCipherBase
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x00071AF4 File Offset: 0x00070AF4
		public BufferedIesCipher(IesEngine engine)
		{
			if (engine == null)
			{
				throw new ArgumentNullException("engine");
			}
			this.engine = engine;
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00071B1C File Offset: 0x00070B1C
		public override string AlgorithmName
		{
			get
			{
				return "IES";
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00071B23 File Offset: 0x00070B23
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			throw Platform.CreateNotImplementedException("IES");
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00071B36 File Offset: 0x00070B36
		public override int GetBlockSize()
		{
			return 0;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00071B3C File Offset: 0x00070B3C
		public override int GetOutputSize(int inputLen)
		{
			if (this.engine == null)
			{
				throw new InvalidOperationException("cipher not initialised");
			}
			int num = inputLen + (int)this.buffer.Length;
			if (!this.forEncryption)
			{
				return num - 20;
			}
			return num + 20;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00071B7C File Offset: 0x00070B7C
		public override int GetUpdateOutputSize(int inputLen)
		{
			return 0;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00071B7F File Offset: 0x00070B7F
		public override byte[] ProcessByte(byte input)
		{
			this.buffer.WriteByte(input);
			return null;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00071B90 File Offset: 0x00070B90
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inOff < 0)
			{
				throw new ArgumentException("inOff");
			}
			if (length < 0)
			{
				throw new ArgumentException("length");
			}
			if (inOff + length > input.Length)
			{
				throw new ArgumentException("invalid offset/length specified for input array");
			}
			this.buffer.Write(input, inOff, length);
			return null;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00071BEC File Offset: 0x00070BEC
		public override byte[] DoFinal()
		{
			byte[] array = this.buffer.ToArray();
			this.Reset();
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00071C1B File Offset: 0x00070C1B
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			this.ProcessBytes(input, inOff, length);
			return this.DoFinal();
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00071C2D File Offset: 0x00070C2D
		public override void Reset()
		{
			this.buffer.SetLength(0L);
		}

		// Token: 0x04000DAB RID: 3499
		private readonly IesEngine engine;

		// Token: 0x04000DAC RID: 3500
		private bool forEncryption;

		// Token: 0x04000DAD RID: 3501
		private MemoryStream buffer = new MemoryStream();
	}
}
