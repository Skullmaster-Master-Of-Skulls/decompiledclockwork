using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000026 RID: 38
	public class CountingStream : Stream
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00004890 File Offset: 0x00002A90
		public CountingStream(Stream stream)
		{
			this._s = stream;
			try
			{
				this._initialOffset = this._s.Position;
			}
			catch
			{
				this._initialOffset = 0L;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000048D8 File Offset: 0x00002AD8
		public Stream WrappedStream
		{
			get
			{
				return this._s;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000048E0 File Offset: 0x00002AE0
		public long BytesWritten
		{
			get
			{
				return this._bytesWritten;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000048E8 File Offset: 0x00002AE8
		public long BytesRead
		{
			get
			{
				return this._bytesRead;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000048F0 File Offset: 0x00002AF0
		public void Adjust(long delta)
		{
			this._bytesWritten -= delta;
			if (this._bytesWritten < 0L)
			{
				throw new InvalidOperationException();
			}
			if (this._s is CountingStream)
			{
				((CountingStream)this._s).Adjust(delta);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004930 File Offset: 0x00002B30
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this._s.Read(buffer, offset, count);
			this._bytesRead += (long)num;
			return num;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000495C File Offset: 0x00002B5C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			this._s.Write(buffer, offset, count);
			this._bytesWritten += (long)count;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000497F File Offset: 0x00002B7F
		public override bool CanRead
		{
			get
			{
				return this._s.CanRead;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000498C File Offset: 0x00002B8C
		public override bool CanSeek
		{
			get
			{
				return this._s.CanSeek;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004999 File Offset: 0x00002B99
		public override bool CanWrite
		{
			get
			{
				return this._s.CanWrite;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000049A6 File Offset: 0x00002BA6
		public override void Flush()
		{
			this._s.Flush();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000049B3 File Offset: 0x00002BB3
		public override long Length
		{
			get
			{
				return this._s.Length;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000049C0 File Offset: 0x00002BC0
		public long ComputedPosition
		{
			get
			{
				return this._initialOffset + this._bytesWritten;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000049CF File Offset: 0x00002BCF
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000049DC File Offset: 0x00002BDC
		public override long Position
		{
			get
			{
				return this._s.Position;
			}
			set
			{
				this._s.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000049EC File Offset: 0x00002BEC
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._s.Seek(offset, origin);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000049FB File Offset: 0x00002BFB
		public override void SetLength(long value)
		{
			this._s.SetLength(value);
		}

		// Token: 0x04000062 RID: 98
		private Stream _s;

		// Token: 0x04000063 RID: 99
		private long _bytesWritten;

		// Token: 0x04000064 RID: 100
		private long _bytesRead;

		// Token: 0x04000065 RID: 101
		private long _initialOffset;
	}
}
