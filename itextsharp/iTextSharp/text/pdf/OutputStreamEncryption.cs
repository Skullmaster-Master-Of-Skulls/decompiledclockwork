using System;
using System.IO;
using iTextSharp.text.pdf.crypto;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003A0 RID: 928
	public class OutputStreamEncryption : Stream
	{
		// Token: 0x06002014 RID: 8212 RVA: 0x000BF6BC File Offset: 0x000BE6BC
		public OutputStreamEncryption(Stream outc, byte[] key, int off, int len, int revision)
		{
			this.outc = outc;
			this.aes = (revision == 4);
			if (this.aes)
			{
				byte[] iv = IVGenerator.GetIV();
				byte[] array = new byte[len];
				Array.Copy(key, off, array, 0, len);
				this.cipher = new AESCipher(true, array, iv);
				this.Write(iv, 0, iv.Length);
				return;
			}
			this.arcfour = new ARCFOUREncryption();
			this.arcfour.PrepareARCFOURKey(key, off, len);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000BF742 File Offset: 0x000BE742
		public OutputStreamEncryption(Stream outc, byte[] key, int revision) : this(outc, key, 0, key.Length, revision)
		{
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x000BF751 File Offset: 0x000BE751
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x000BF754 File Offset: 0x000BE754
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x000BF757 File Offset: 0x000BE757
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x000BF75A File Offset: 0x000BE75A
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x000BF761 File Offset: 0x000BE761
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x000BF768 File Offset: 0x000BE768
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000BF76F File Offset: 0x000BE76F
		public override void Flush()
		{
			this.outc.Flush();
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000BF77C File Offset: 0x000BE77C
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000BF783 File Offset: 0x000BE783
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000BF78A File Offset: 0x000BE78A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000BF794 File Offset: 0x000BE794
		public override void Write(byte[] b, int off, int len)
		{
			if (!this.aes)
			{
				byte[] array = new byte[Math.Min(len, 4192)];
				while (len > 0)
				{
					int num = Math.Min(len, array.Length);
					this.arcfour.EncryptARCFOUR(b, off, num, array, 0);
					this.outc.Write(array, 0, num);
					len -= num;
					off += num;
				}
				return;
			}
			byte[] array2 = this.cipher.Update(b, off, len);
			if (array2 == null || array2.Length == 0)
			{
				return;
			}
			this.outc.Write(array2, 0, array2.Length);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000BF81B File Offset: 0x000BE81B
		public override void Close()
		{
			this.Finish();
			this.outc.Close();
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000BF82E File Offset: 0x000BE82E
		public override void WriteByte(byte value)
		{
			this.buf[0] = value;
			this.Write(this.buf, 0, 1);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000BF848 File Offset: 0x000BE848
		public void Finish()
		{
			if (!this.finished)
			{
				this.finished = true;
				if (this.aes)
				{
					byte[] array = this.cipher.DoFinal();
					this.outc.Write(array, 0, array.Length);
				}
			}
		}

		// Token: 0x0400161C RID: 5660
		private const int AES_128 = 4;

		// Token: 0x0400161D RID: 5661
		protected Stream outc;

		// Token: 0x0400161E RID: 5662
		protected ARCFOUREncryption arcfour;

		// Token: 0x0400161F RID: 5663
		protected AESCipher cipher;

		// Token: 0x04001620 RID: 5664
		private byte[] buf = new byte[1];

		// Token: 0x04001621 RID: 5665
		private bool aes;

		// Token: 0x04001622 RID: 5666
		private bool finished;
	}
}
