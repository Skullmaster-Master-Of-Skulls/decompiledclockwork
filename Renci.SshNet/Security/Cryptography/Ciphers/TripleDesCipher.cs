using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers
{
	// Token: 0x0200008D RID: 141
	public sealed class TripleDesCipher : DesCipher
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0001BC63 File Offset: 0x00019E63
		public TripleDesCipher(byte[] key, CipherMode mode, CipherPadding padding) : base(key, mode, padding)
		{
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001BC70 File Offset: 0x00019E70
		public override int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputOffset + (int)base.BlockSize > inputBuffer.Length)
			{
				throw new IndexOutOfRangeException("input buffer too short");
			}
			if (outputOffset + (int)base.BlockSize > outputBuffer.Length)
			{
				throw new IndexOutOfRangeException("output buffer too short");
			}
			if (this._encryptionKey1 == null || this._encryptionKey2 == null || this._encryptionKey3 == null)
			{
				byte[] array = new byte[8];
				byte[] array2 = new byte[8];
				Buffer.BlockCopy(base.Key, 0, array, 0, 8);
				Buffer.BlockCopy(base.Key, 8, array2, 0, 8);
				this._encryptionKey1 = base.GenerateWorkingKey(true, array);
				this._encryptionKey2 = base.GenerateWorkingKey(false, array2);
				if (base.Key.Length == 24)
				{
					byte[] array3 = new byte[8];
					Buffer.BlockCopy(base.Key, 16, array3, 0, 8);
					this._encryptionKey3 = base.GenerateWorkingKey(true, array3);
				}
				else
				{
					this._encryptionKey3 = this._encryptionKey1;
				}
			}
			byte[] array4 = new byte[(int)base.BlockSize];
			DesCipher.DesFunc(this._encryptionKey1, inputBuffer, inputOffset, array4, 0);
			DesCipher.DesFunc(this._encryptionKey2, array4, 0, array4, 0);
			DesCipher.DesFunc(this._encryptionKey3, array4, 0, outputBuffer, outputOffset);
			return (int)base.BlockSize;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001BD94 File Offset: 0x00019F94
		public override int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputOffset + (int)base.BlockSize > inputBuffer.Length)
			{
				throw new IndexOutOfRangeException("input buffer too short");
			}
			if (outputOffset + (int)base.BlockSize > outputBuffer.Length)
			{
				throw new IndexOutOfRangeException("output buffer too short");
			}
			if (this._decryptionKey1 == null || this._decryptionKey2 == null || this._decryptionKey3 == null)
			{
				byte[] array = new byte[8];
				byte[] array2 = new byte[8];
				Buffer.BlockCopy(base.Key, 0, array, 0, 8);
				Buffer.BlockCopy(base.Key, 8, array2, 0, 8);
				this._decryptionKey1 = base.GenerateWorkingKey(false, array);
				this._decryptionKey2 = base.GenerateWorkingKey(true, array2);
				if (base.Key.Length == 24)
				{
					byte[] array3 = new byte[8];
					Buffer.BlockCopy(base.Key, 16, array3, 0, 8);
					this._decryptionKey3 = base.GenerateWorkingKey(false, array3);
				}
				else
				{
					this._decryptionKey3 = this._decryptionKey1;
				}
			}
			byte[] array4 = new byte[(int)base.BlockSize];
			DesCipher.DesFunc(this._decryptionKey3, inputBuffer, inputOffset, array4, 0);
			DesCipher.DesFunc(this._decryptionKey2, array4, 0, array4, 0);
			DesCipher.DesFunc(this._decryptionKey1, array4, 0, outputBuffer, outputOffset);
			return (int)base.BlockSize;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		protected override void ValidateKey()
		{
			int num = base.Key.Length * 8;
			if (num != 128 && num != 192)
			{
				throw new ArgumentException(string.Format("KeySize '{0}' is not valid for this algorithm.", num));
			}
		}

		// Token: 0x040002BC RID: 700
		private int[] _encryptionKey1;

		// Token: 0x040002BD RID: 701
		private int[] _encryptionKey2;

		// Token: 0x040002BE RID: 702
		private int[] _encryptionKey3;

		// Token: 0x040002BF RID: 703
		private int[] _decryptionKey1;

		// Token: 0x040002C0 RID: 704
		private int[] _decryptionKey2;

		// Token: 0x040002C1 RID: 705
		private int[] _decryptionKey3;
	}
}
