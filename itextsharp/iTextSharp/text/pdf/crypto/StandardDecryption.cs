using System;

namespace iTextSharp.text.pdf.crypto
{
	// Token: 0x020001C3 RID: 451
	public class StandardDecryption
	{
		// Token: 0x060010F9 RID: 4345 RVA: 0x0005FF88 File Offset: 0x0005EF88
		public StandardDecryption(byte[] key, int off, int len, int revision)
		{
			this.aes = (revision == 4);
			if (this.aes)
			{
				this.key = new byte[len];
				Array.Copy(key, off, this.key, 0, len);
				return;
			}
			this.arcfour = new ARCFOUREncryption();
			this.arcfour.PrepareARCFOURKey(key, off, len);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0005FFF0 File Offset: 0x0005EFF0
		public byte[] Update(byte[] b, int off, int len)
		{
			if (!this.aes)
			{
				byte[] array = new byte[len];
				this.arcfour.EncryptARCFOUR(b, off, len, array, 0);
				return array;
			}
			if (this.initiated)
			{
				return this.cipher.Update(b, off, len);
			}
			int num = Math.Min(this.iv.Length - this.ivptr, len);
			Array.Copy(b, off, this.iv, this.ivptr, num);
			off += num;
			len -= num;
			this.ivptr += num;
			if (this.ivptr == this.iv.Length)
			{
				this.cipher = new AESCipher(false, this.key, this.iv);
				this.initiated = true;
				if (len > 0)
				{
					return this.cipher.Update(b, off, len);
				}
			}
			return null;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000600BD File Offset: 0x0005F0BD
		public byte[] Finish()
		{
			if (this.aes)
			{
				return this.cipher.DoFinal();
			}
			return null;
		}

		// Token: 0x04000C4A RID: 3146
		private const int AES_128 = 4;

		// Token: 0x04000C4B RID: 3147
		protected ARCFOUREncryption arcfour;

		// Token: 0x04000C4C RID: 3148
		protected AESCipher cipher;

		// Token: 0x04000C4D RID: 3149
		private byte[] key;

		// Token: 0x04000C4E RID: 3150
		private bool aes;

		// Token: 0x04000C4F RID: 3151
		private bool initiated;

		// Token: 0x04000C50 RID: 3152
		private byte[] iv = new byte[16];

		// Token: 0x04000C51 RID: 3153
		private int ivptr;
	}
}
