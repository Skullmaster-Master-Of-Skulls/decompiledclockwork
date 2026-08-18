using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x02000054 RID: 84
	public class DeflateStream : Stream
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0001905E File Offset: 0x0001725E
		public DeflateStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001906A File Offset: 0x0001726A
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00019076 File Offset: 0x00017276
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00019082 File Offset: 0x00017282
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000190A6 File Offset: 0x000172A6
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x000190B3 File Offset: 0x000172B3
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000190D4 File Offset: 0x000172D4
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x000190E4 File Offset: 0x000172E4
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
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00019150 File Offset: 0x00017350
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0001915D File Offset: 0x0001735D
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001917E File Offset: 0x0001737E
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00019190 File Offset: 0x00017390
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000191A4 File Offset: 0x000173A4
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000191F0 File Offset: 0x000173F0
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00019215 File Offset: 0x00017415
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00019218 File Offset: 0x00017418
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

		// Token: 0x06000405 RID: 1029 RVA: 0x0001923D File Offset: 0x0001743D
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001925D File Offset: 0x0001745D
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00019264 File Offset: 0x00017464
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000192B0 File Offset: 0x000174B0
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

		// Token: 0x06000409 RID: 1033 RVA: 0x000192B7 File Offset: 0x000174B7
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000192DA File Offset: 0x000174DA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000192E1 File Offset: 0x000174E1
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000192E8 File Offset: 0x000174E8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001930C File Offset: 0x0001750C
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

		// Token: 0x0600040E RID: 1038 RVA: 0x00019354 File Offset: 0x00017554
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

		// Token: 0x0600040F RID: 1039 RVA: 0x0001939C File Offset: 0x0001759C
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

		// Token: 0x06000410 RID: 1040 RVA: 0x000193E0 File Offset: 0x000175E0
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

		// Token: 0x040002AD RID: 685
		internal ZlibBaseStream _baseStream;

		// Token: 0x040002AE RID: 686
		internal Stream _innerStream;

		// Token: 0x040002AF RID: 687
		private bool _disposed;
	}
}
