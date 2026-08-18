using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x0200002C RID: 44
	internal class ZipCipherStream : Stream
	{
		// Token: 0x060000FE RID: 254 RVA: 0x000056FE File Offset: 0x000038FE
		public ZipCipherStream(Stream s, ZipCrypto cipher, CryptoMode mode)
		{
			this._cipher = cipher;
			this._s = s;
			this._mode = mode;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000571C File Offset: 0x0000391C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException("This stream does not encrypt via Read()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte[] array = new byte[count];
			int num = this._s.Read(array, 0, count);
			byte[] array2 = this._cipher.DecryptMessage(array, num);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array2[i];
			}
			return num;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005784 File Offset: 0x00003984
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException("This stream does not Decrypt via Write()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count == 0)
			{
				return;
			}
			byte[] array;
			if (offset != 0)
			{
				array = new byte[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = buffer[offset + i];
				}
			}
			else
			{
				array = buffer;
			}
			byte[] array2 = this._cipher.EncryptMessage(array, count);
			this._s.Write(array2, 0, array2.Length);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000057F9 File Offset: 0x000039F9
		public override bool CanRead
		{
			get
			{
				return this._mode == CryptoMode.Decrypt;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005804 File Offset: 0x00003A04
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005807 File Offset: 0x00003A07
		public override bool CanWrite
		{
			get
			{
				return this._mode == CryptoMode.Encrypt;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005812 File Offset: 0x00003A12
		public override void Flush()
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005814 File Offset: 0x00003A14
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000581B File Offset: 0x00003A1B
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005822 File Offset: 0x00003A22
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

		// Token: 0x06000108 RID: 264 RVA: 0x00005829 File Offset: 0x00003A29
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005830 File Offset: 0x00003A30
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000096 RID: 150
		private ZipCrypto _cipher;

		// Token: 0x04000097 RID: 151
		private Stream _s;

		// Token: 0x04000098 RID: 152
		private CryptoMode _mode;
	}
}
