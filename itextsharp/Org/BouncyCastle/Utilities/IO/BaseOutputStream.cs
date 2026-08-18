using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x0200002A RID: 42
	public abstract class BaseOutputStream : Stream
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000886C File Offset: 0x0000786C
		public sealed override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000886F File Offset: 0x0000786F
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00008872 File Offset: 0x00007872
		public sealed override bool CanWrite
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000887D File Offset: 0x0000787D
		public override void Close()
		{
			this.closed = true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008886 File Offset: 0x00007886
		public override void Flush()
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00008888 File Offset: 0x00007888
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000888F File Offset: 0x0000788F
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00008896 File Offset: 0x00007896
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

		// Token: 0x06000132 RID: 306 RVA: 0x0000889D File Offset: 0x0000789D
		public sealed override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000088A4 File Offset: 0x000078A4
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000088AB File Offset: 0x000078AB
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000088B4 File Offset: 0x000078B4
		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = offset + count;
			for (int i = offset; i < num; i++)
			{
				this.WriteByte(buffer[i]);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000088DA File Offset: 0x000078DA
		public virtual void Write(params byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x0400009A RID: 154
		private bool closed;
	}
}
