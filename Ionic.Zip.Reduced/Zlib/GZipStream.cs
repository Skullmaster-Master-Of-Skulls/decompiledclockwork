using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000055 RID: 85
	public class GZipStream : Stream
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00019424 File Offset: 0x00017624
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0001942C File Offset: 0x0001762C
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00019448 File Offset: 0x00017648
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00019450 File Offset: 0x00017650
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				if (this._FileName == null)
				{
					return;
				}
				if (this._FileName.IndexOf("/") != -1)
				{
					this._FileName = this._FileName.Replace("/", "\\");
				}
				if (this._FileName.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (this._FileName.IndexOf("\\") != -1)
				{
					this._FileName = Path.GetFileName(this._FileName);
				}
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000194EF File Offset: 0x000176EF
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000194F7 File Offset: 0x000176F7
		public GZipStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00019503 File Offset: 0x00017703
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001950F File Offset: 0x0001770F
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001951B File Offset: 0x0001771B
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00019538 File Offset: 0x00017738
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00019545 File Offset: 0x00017745
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00019566 File Offset: 0x00017766
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00019574 File Offset: 0x00017774
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000195E0 File Offset: 0x000177E0
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000195F2 File Offset: 0x000177F2
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00019604 File Offset: 0x00017804
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00019664 File Offset: 0x00017864
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00019689 File Offset: 0x00017889
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001968C File Offset: 0x0001788C
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000196B1 File Offset: 0x000178B1
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000196D1 File Offset: 0x000178D1
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000196D8 File Offset: 0x000178D8
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00019739 File Offset: 0x00017939
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00019740 File Offset: 0x00017940
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int result = this._baseStream.Read(buffer, offset, count);
			if (!this._firstReadDone)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return result;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000197A1 File Offset: 0x000179A1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000197A8 File Offset: 0x000179A8
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000197B0 File Offset: 0x000179B0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._baseStream._wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00019810 File Offset: 0x00017A10
		private int EmitHeader()
		{
			byte[] array = (this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment);
			byte[] array2 = (this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName);
			int num = (this.Comment == null) ? 0 : (array.Length + 1);
			int num2 = (this.FileName == null) ? 0 : (array2.Length + 1);
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			array3[num4++] = 31;
			array3[num4++] = 139;
			array3[num4++] = 8;
			byte b = 0;
			if (this.Comment != null)
			{
				b ^= 16;
			}
			if (this.FileName != null)
			{
				b ^= 8;
			}
			array3[num4++] = b;
			if (this.LastModified == null)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			int value = (int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(value), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000199B4 File Offset: 0x00017BB4
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000199FC File Offset: 0x00017BFC
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00019A44 File Offset: 0x00017C44
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new GZipStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00019A88 File Offset: 0x00017C88
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new GZipStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x040002B0 RID: 688
		public DateTime? LastModified;

		// Token: 0x040002B1 RID: 689
		private int _headerByteCount;

		// Token: 0x040002B2 RID: 690
		internal ZlibBaseStream _baseStream;

		// Token: 0x040002B3 RID: 691
		private bool _disposed;

		// Token: 0x040002B4 RID: 692
		private bool _firstReadDone;

		// Token: 0x040002B5 RID: 693
		private string _FileName;

		// Token: 0x040002B6 RID: 694
		private string _Comment;

		// Token: 0x040002B7 RID: 695
		private int _Crc32;

		// Token: 0x040002B8 RID: 696
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x040002B9 RID: 697
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
	}
}
