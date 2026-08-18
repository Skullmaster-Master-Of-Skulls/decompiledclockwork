using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x02000007 RID: 7
	public class DeflateStream : Stream
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000047B5 File Offset: 0x000029B5
		public DeflateStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000047C1 File Offset: 0x000029C1
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000047CD File Offset: 0x000029CD
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000047D9 File Offset: 0x000029D9
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000047FD File Offset: 0x000029FD
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000480A File Offset: 0x00002A0A
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
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000482B File Offset: 0x00002A2B
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00004838 File Offset: 0x00002A38
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
					throw new ObjectDisposedException("DeflateStream");
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000048B0 File Offset: 0x00002AB0
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000048BD File Offset: 0x00002ABD
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000048DE File Offset: 0x00002ADE
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000048F0 File Offset: 0x00002AF0
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004904 File Offset: 0x00002B04
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004950 File Offset: 0x00002B50
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00004975 File Offset: 0x00002B75
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00004978 File Offset: 0x00002B78
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000499D File Offset: 0x00002B9D
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000049BD File Offset: 0x00002BBD
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000049C4 File Offset: 0x00002BC4
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000049BD File Offset: 0x00002BBD
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004A17 File Offset: 0x00002C17
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000049BD File Offset: 0x00002BBD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000049BD File Offset: 0x00002BBD
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004A48 File Offset: 0x00002C48
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004A6C File Offset: 0x00002C6C
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004AFC File Offset: 0x00002CFC
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new DeflateStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004B40 File Offset: 0x00002D40
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new DeflateStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x04000063 RID: 99
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000064 RID: 100
		internal Stream _innerStream;

		// Token: 0x04000065 RID: 101
		private bool _disposed;
	}
}
