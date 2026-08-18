using System;
using Renci.SshNet.Security.Cryptography.Ciphers;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x0200007C RID: 124
	public abstract class BlockCipher : SymmetricCipher
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001500A File Offset: 0x0001320A
		public override byte MinimumSize
		{
			get
			{
				return this.BlockSize;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00015012 File Offset: 0x00013212
		public byte BlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001501A File Offset: 0x0001321A
		protected BlockCipher(byte[] key, byte blockSize, CipherMode mode, CipherPadding padding) : base(key)
		{
			this._blockSize = blockSize;
			this._mode = mode;
			this._padding = padding;
			if (this._mode != null)
			{
				this._mode.Init(this);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00015050 File Offset: 0x00013250
		public override byte[] Encrypt(byte[] data, int offset, int length)
		{
			if (length % (int)this._blockSize > 0)
			{
				if (this._padding == null)
				{
					throw new ArgumentException("data");
				}
				int num = (int)this._blockSize - length % (int)this._blockSize;
				data = this._padding.Pad(data, num);
				length += num;
			}
			byte[] array = new byte[length];
			int num2 = 0;
			for (int i = 0; i < length / (int)this._blockSize; i++)
			{
				if (this._mode == null)
				{
					num2 += this.EncryptBlock(data, offset + i * (int)this._blockSize, (int)this._blockSize, array, i * (int)this._blockSize);
				}
				else
				{
					num2 += this._mode.EncryptBlock(data, offset + i * (int)this._blockSize, (int)this._blockSize, array, i * (int)this._blockSize);
				}
			}
			if (num2 < length)
			{
				throw new InvalidOperationException("Encryption error.");
			}
			return array;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00015120 File Offset: 0x00013320
		public override byte[] Decrypt(byte[] data)
		{
			if (data.Length % (int)this._blockSize > 0)
			{
				if (this._padding == null)
				{
					throw new ArgumentException("data");
				}
				data = this._padding.Pad((int)this._blockSize, data);
			}
			byte[] array = new byte[data.Length];
			int num = 0;
			for (int i = 0; i < data.Length / (int)this._blockSize; i++)
			{
				if (this._mode == null)
				{
					num += this.DecryptBlock(data, i * (int)this._blockSize, (int)this._blockSize, array, i * (int)this._blockSize);
				}
				else
				{
					num += this._mode.DecryptBlock(data, i * (int)this._blockSize, (int)this._blockSize, array, i * (int)this._blockSize);
				}
			}
			if (num < data.Length)
			{
				throw new InvalidOperationException("Encryption error.");
			}
			return array;
		}

		// Token: 0x04000262 RID: 610
		private readonly CipherMode _mode;

		// Token: 0x04000263 RID: 611
		private readonly CipherPadding _padding;

		// Token: 0x04000264 RID: 612
		private readonly byte _blockSize;
	}
}
