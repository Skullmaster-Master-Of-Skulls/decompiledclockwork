using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x0200003C RID: 60
	public abstract class BaseInputStream : Stream
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000094B4 File Offset: 0x000084B4
		public sealed override bool CanRead
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000094BF File Offset: 0x000084BF
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000094C2 File Offset: 0x000084C2
		public sealed override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000094C5 File Offset: 0x000084C5
		public override void Close()
		{
			this.closed = true;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000094CE File Offset: 0x000084CE
		public sealed override void Flush()
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000094D0 File Offset: 0x000084D0
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000094D7 File Offset: 0x000084D7
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000094DE File Offset: 0x000084DE
		public sealed override long Position
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

		// Token: 0x06000192 RID: 402 RVA: 0x000094E8 File Offset: 0x000084E8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = offset;
			try
			{
				int num = offset + count;
				while (i < num)
				{
					int num2 = this.ReadByte();
					if (num2 == -1)
					{
						break;
					}
					buffer[i++] = (byte)num2;
				}
			}
			catch (IOException)
			{
				if (i == offset)
				{
					throw;
				}
			}
			return i - offset;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009534 File Offset: 0x00008534
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000953B File Offset: 0x0000853B
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009542 File Offset: 0x00008542
		public sealed override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040000B9 RID: 185
		private bool closed;
	}
}
