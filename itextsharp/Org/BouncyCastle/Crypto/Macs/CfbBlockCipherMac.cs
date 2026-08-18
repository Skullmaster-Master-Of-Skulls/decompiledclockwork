using System;
using Org.BouncyCastle.Crypto.Paddings;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200034D RID: 845
	public class CfbBlockCipherMac : IMac
	{
		// Token: 0x06001E74 RID: 7796 RVA: 0x000B62F8 File Offset: 0x000B52F8
		public CfbBlockCipherMac(IBlockCipher cipher) : this(cipher, 8, cipher.GetBlockSize() * 8 / 2, null)
		{
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000B630D File Offset: 0x000B530D
		public CfbBlockCipherMac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, 8, cipher.GetBlockSize() * 8 / 2, padding)
		{
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000B6322 File Offset: 0x000B5322
		public CfbBlockCipherMac(IBlockCipher cipher, int cfbBitSize, int macSizeInBits) : this(cipher, cfbBitSize, macSizeInBits, null)
		{
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000B6330 File Offset: 0x000B5330
		public CfbBlockCipherMac(IBlockCipher cipher, int cfbBitSize, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			this.mac = new byte[cipher.GetBlockSize()];
			this.cipher = new MacCFBBlockCipher(cipher, cfbBitSize);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.Buffer = new byte[this.cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000B639F File Offset: 0x000B539F
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000B63AC File Offset: 0x000B53AC
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000B63C1 File Offset: 0x000B53C1
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000B63CC File Offset: 0x000B53CC
		public void Update(byte input)
		{
			if (this.bufOff == this.Buffer.Length)
			{
				this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.Buffer[this.bufOff++] = input;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000B6424 File Offset: 0x000B5424
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			int num2 = blockSize - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > blockSize)
				{
					num += this.cipher.ProcessBlock(input, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.Buffer, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000B64E8 File Offset: 0x000B54E8
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					this.Buffer[this.bufOff++] = 0;
				}
			}
			else
			{
				this.padding.AddPadding(this.Buffer, this.bufOff);
			}
			this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
			this.cipher.GetMacBlock(this.mac);
			Array.Copy(this.mac, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000B6592 File Offset: 0x000B5592
		public void Reset()
		{
			Array.Clear(this.Buffer, 0, this.Buffer.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04001518 RID: 5400
		private byte[] mac;

		// Token: 0x04001519 RID: 5401
		private byte[] Buffer;

		// Token: 0x0400151A RID: 5402
		private int bufOff;

		// Token: 0x0400151B RID: 5403
		private MacCFBBlockCipher cipher;

		// Token: 0x0400151C RID: 5404
		private IBlockCipherPadding padding;

		// Token: 0x0400151D RID: 5405
		private int macSize;
	}
}
