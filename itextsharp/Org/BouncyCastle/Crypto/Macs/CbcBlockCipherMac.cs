using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200054E RID: 1358
	public class CbcBlockCipherMac : IMac
	{
		// Token: 0x06002EAD RID: 11949 RVA: 0x0011FF6A File Offset: 0x0011EF6A
		public CbcBlockCipherMac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8 / 2, null)
		{
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x0011FF7E File Offset: 0x0011EF7E
		public CbcBlockCipherMac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, cipher.GetBlockSize() * 8 / 2, padding)
		{
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x0011FF92 File Offset: 0x0011EF92
		public CbcBlockCipherMac(IBlockCipher cipher, int macSizeInBits) : this(cipher, macSizeInBits, null)
		{
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x0011FFA0 File Offset: 0x0011EFA0
		public CbcBlockCipherMac(IBlockCipher cipher, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x0011FFF7 File Offset: 0x0011EFF7
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x00120004 File Offset: 0x0011F004
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x00120019 File Offset: 0x0011F019
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00120024 File Offset: 0x0011F024
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x0012007C File Offset: 0x0011F07C
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = blockSize - this.bufOff;
			if (len > num)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num);
				this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > blockSize)
				{
					this.cipher.ProcessBlock(input, inOff, this.buf, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00120138 File Offset: 0x0011F138
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					this.buf[this.bufOff++] = 0;
				}
			}
			else
			{
				if (this.bufOff == blockSize)
				{
					this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
			}
			this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
			Array.Copy(this.buf, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x001201FB File Offset: 0x0011F1FB
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x0400201A RID: 8218
		private byte[] buf;

		// Token: 0x0400201B RID: 8219
		private int bufOff;

		// Token: 0x0400201C RID: 8220
		private IBlockCipher cipher;

		// Token: 0x0400201D RID: 8221
		private IBlockCipherPadding padding;

		// Token: 0x0400201E RID: 8222
		private int macSize;
	}
}
