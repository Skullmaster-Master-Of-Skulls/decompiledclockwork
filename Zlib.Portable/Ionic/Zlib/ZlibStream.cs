using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x0200001E RID: 30
	public class ZlibStream : Stream
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000AA82 File Offset: 0x00008C82
		public ZlibStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000AA8E File Offset: 0x00008C8E
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000AA9A File Offset: 0x00008C9A
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000AAC3 File Offset: 0x00008CC3
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000AAF1 File Offset: 0x00008CF1
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000AB00 File Offset: 0x00008D00
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", new object[]
					{
						value,
						1024
					}));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000AB78 File Offset: 0x00008D78
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000AB8A File Offset: 0x00008D8A
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000AB9C File Offset: 0x00008D9C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004975 File Offset: 0x00002B75
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000AC10 File Offset: 0x00008E10
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000AC35 File Offset: 0x00008E35
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000AC5C File Offset: 0x00008E5C
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000ACAF File Offset: 0x00008EAF
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000090DB File Offset: 0x000072DB
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000AD04 File Offset: 0x00008F04
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000AD94 File Offset: 0x00008F94
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new ZlibStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new ZlibStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x04000156 RID: 342
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000157 RID: 343
		private bool _disposed;
	}
}
