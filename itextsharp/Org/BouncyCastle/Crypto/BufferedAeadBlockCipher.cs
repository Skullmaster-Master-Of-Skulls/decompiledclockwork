using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020002AC RID: 684
	public class BufferedAeadBlockCipher : BufferedCipherBase
	{
		// Token: 0x060019D7 RID: 6615 RVA: 0x00099CF1 File Offset: 0x00098CF1
		public BufferedAeadBlockCipher(IAeadBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00099D0E File Offset: 0x00098D0E
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00099D1B File Offset: 0x00098D1B
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00099D3F File Offset: 0x00098D3F
		public override int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00099D4C File Offset: 0x00098D4C
		public override int GetUpdateOutputSize(int length)
		{
			return this.cipher.GetUpdateOutputSize(length);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00099D5A File Offset: 0x00098D5A
		public override int GetOutputSize(int length)
		{
			return this.cipher.GetOutputSize(length);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00099D68 File Offset: 0x00098D68
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			return this.cipher.ProcessByte(input, output, outOff);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00099D78 File Offset: 0x00098D78
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

		// Token: 0x060019DF RID: 6623 RVA: 0x00099DC4 File Offset: 0x00098DC4
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

		// Token: 0x060019E0 RID: 6624 RVA: 0x00099E23 File Offset: 0x00098E23
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			return this.cipher.ProcessBytes(input, inOff, length, output, outOff);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00099E38 File Offset: 0x00098E38
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
			return array;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00099E84 File Offset: 0x00098E84
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
			return array;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00099EF0 File Offset: 0x00098EF0
		public override int DoFinal(byte[] output, int outOff)
		{
			return this.cipher.DoFinal(output, outOff);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00099EFF File Offset: 0x00098EFF
		public override void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04001145 RID: 4421
		private readonly IAeadBlockCipher cipher;
	}
}
