using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000094 RID: 148
	public class BufferedBlockCipher : BufferedCipherBase
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x0001A244 File Offset: 0x00019244
		protected BufferedBlockCipher()
		{
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001A24C File Offset: 0x0001924C
		public BufferedBlockCipher(IBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001A281 File Offset: 0x00019281
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001A28E File Offset: 0x0001928E
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.Reset();
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001A2BF File Offset: 0x000192BF
		public override int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001A2CC File Offset: 0x000192CC
		public override int GetUpdateOutputSize(int length)
		{
			int num = length + this.bufOff;
			int num2 = num % this.buf.Length;
			return num - num2;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001A2F0 File Offset: 0x000192F0
		public override int GetOutputSize(int length)
		{
			return length + this.bufOff;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001A2FC File Offset: 0x000192FC
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.buf[this.bufOff++] = input;
			if (this.bufOff != this.buf.Length)
			{
				return 0;
			}
			if (outOff + this.buf.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.bufOff = 0;
			return this.cipher.ProcessBlock(this.buf, 0, output, outOff);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001A36C File Offset: 0x0001936C
		public override byte[] ProcessByte(byte input)
		{
			int updateOutputSize = this.GetUpdateOutputSize(1);
			byte[] array = (updateOutputSize > 0) ? new byte[updateOutputSize] : null;
			int num = this.ProcessByte(input, array, 0);
			if (updateOutputSize > 0 && num < updateOutputSize)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001A3B8 File Offset: 0x000193B8
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (length < 1)
			{
				return null;
			}
			int updateOutputSize = this.GetUpdateOutputSize(length);
			byte[] array = (updateOutputSize > 0) ? new byte[updateOutputSize] : null;
			int num = this.ProcessBytes(input, inOff, length, array, 0);
			if (updateOutputSize > 0 && num < updateOutputSize)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001A418 File Offset: 0x00019418
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 1)
			{
				if (length < 0)
				{
					throw new ArgumentException("Can't have a negative input length!");
				}
				return 0;
			}
			else
			{
				int blockSize = this.GetBlockSize();
				int updateOutputSize = this.GetUpdateOutputSize(length);
				if (updateOutputSize > 0 && outOff + updateOutputSize > output.Length)
				{
					throw new DataLengthException("output buffer too short");
				}
				int num = 0;
				int num2 = this.buf.Length - this.bufOff;
				if (length > num2)
				{
					Array.Copy(input, inOff, this.buf, this.bufOff, num2);
					num += this.cipher.ProcessBlock(this.buf, 0, output, outOff);
					this.bufOff = 0;
					length -= num2;
					inOff += num2;
					while (length > this.buf.Length)
					{
						num += this.cipher.ProcessBlock(input, inOff, output, outOff + num);
						length -= blockSize;
						inOff += blockSize;
					}
				}
				Array.Copy(input, inOff, this.buf, this.bufOff, length);
				this.bufOff += length;
				if (this.bufOff == this.buf.Length)
				{
					num += this.cipher.ProcessBlock(this.buf, 0, output, outOff + num);
					this.bufOff = 0;
				}
				return num;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001A538 File Offset: 0x00019538
		public override byte[] DoFinal()
		{
			byte[] array = BufferedCipherBase.EmptyBuffer;
			int outputSize = this.GetOutputSize(0);
			if (outputSize > 0)
			{
				array = new byte[outputSize];
				int num = this.DoFinal(array, 0);
				if (num < array.Length)
				{
					byte[] array2 = new byte[num];
					Array.Copy(array, 0, array2, 0, num);
					array = array2;
				}
			}
			else
			{
				this.Reset();
			}
			return array;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001A58C File Offset: 0x0001958C
		public override byte[] DoFinal(byte[] input, int inOff, int inLen)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			int outputSize = this.GetOutputSize(inLen);
			byte[] array = BufferedCipherBase.EmptyBuffer;
			if (outputSize > 0)
			{
				array = new byte[outputSize];
				int num = (inLen > 0) ? this.ProcessBytes(input, inOff, inLen, array, 0) : 0;
				num += this.DoFinal(array, num);
				if (num < array.Length)
				{
					byte[] array2 = new byte[num];
					Array.Copy(array, 0, array2, 0, num);
					array = array2;
				}
			}
			else
			{
				this.Reset();
			}
			return array;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001A600 File Offset: 0x00019600
		public override int DoFinal(byte[] output, int outOff)
		{
			if (this.bufOff != 0)
			{
				if (!this.cipher.IsPartialBlockOkay)
				{
					throw new DataLengthException("data not block size aligned");
				}
				if (outOff + this.bufOff > output.Length)
				{
					throw new DataLengthException("output buffer too short for DoFinal()");
				}
				this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				Array.Copy(this.buf, 0, output, outOff, this.bufOff);
			}
			int result = this.bufOff;
			this.Reset();
			return result;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001A681 File Offset: 0x00019681
		public override void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04000267 RID: 615
		internal byte[] buf;

		// Token: 0x04000268 RID: 616
		internal int bufOff;

		// Token: 0x04000269 RID: 617
		internal bool forEncryption;

		// Token: 0x0400026A RID: 618
		internal IBlockCipher cipher;
	}
}
