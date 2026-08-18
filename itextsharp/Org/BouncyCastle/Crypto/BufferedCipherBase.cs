using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000093 RID: 147
	public abstract class BufferedCipherBase : IBufferedCipher
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004AA RID: 1194
		public abstract string AlgorithmName { get; }

		// Token: 0x060004AB RID: 1195
		public abstract void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060004AC RID: 1196
		public abstract int GetBlockSize();

		// Token: 0x060004AD RID: 1197
		public abstract int GetOutputSize(int inputLen);

		// Token: 0x060004AE RID: 1198
		public abstract int GetUpdateOutputSize(int inputLen);

		// Token: 0x060004AF RID: 1199
		public abstract byte[] ProcessByte(byte input);

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001A118 File Offset: 0x00019118
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			byte[] array = this.ProcessByte(input);
			if (array == null)
			{
				return 0;
			}
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001A152 File Offset: 0x00019152
		public virtual byte[] ProcessBytes(byte[] input)
		{
			return this.ProcessBytes(input, 0, input.Length);
		}

		// Token: 0x060004B2 RID: 1202
		public abstract byte[] ProcessBytes(byte[] input, int inOff, int length);

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001A15F File Offset: 0x0001915F
		public virtual int ProcessBytes(byte[] input, byte[] output, int outOff)
		{
			return this.ProcessBytes(input, 0, input.Length, output, outOff);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001A170 File Offset: 0x00019170
		public virtual int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			byte[] array = this.ProcessBytes(input, inOff, length);
			if (array == null)
			{
				return 0;
			}
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x060004B5 RID: 1205
		public abstract byte[] DoFinal();

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001A1B0 File Offset: 0x000191B0
		public virtual byte[] DoFinal(byte[] input)
		{
			return this.DoFinal(input, 0, input.Length);
		}

		// Token: 0x060004B7 RID: 1207
		public abstract byte[] DoFinal(byte[] input, int inOff, int length);

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001A1C0 File Offset: 0x000191C0
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] array = this.DoFinal();
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001A1F4 File Offset: 0x000191F4
		public virtual int DoFinal(byte[] input, byte[] output, int outOff)
		{
			return this.DoFinal(input, 0, input.Length, output, outOff);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001A204 File Offset: 0x00019204
		public virtual int DoFinal(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			int num = this.ProcessBytes(input, inOff, length, output, outOff);
			return num + this.DoFinal(output, outOff + num);
		}

		// Token: 0x060004BB RID: 1211
		public abstract void Reset();

		// Token: 0x04000266 RID: 614
		protected static readonly byte[] EmptyBuffer = new byte[0];
	}
}
