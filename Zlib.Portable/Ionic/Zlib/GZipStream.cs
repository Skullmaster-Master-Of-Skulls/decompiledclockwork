using System;
using System.IO;
using System.Text;
using Ionic.Encoding;

namespace Ionic.Zlib
{
	// Token: 0x02000008 RID: 8
	public class GZipStream : Stream
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004B84 File Offset: 0x00002D84
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00004B8C File Offset: 0x00002D8C
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00004BA8 File Offset: 0x00002DA8
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00004BB0 File Offset: 0x00002DB0
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
					this._FileName = Path2.GetFileName(this._FileName);
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004C4F File Offset: 0x00002E4F
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004C57 File Offset: 0x00002E57
		public GZipStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004C63 File Offset: 0x00002E63
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004C6F File Offset: 0x00002E6F
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004C7B File Offset: 0x00002E7B
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004C98 File Offset: 0x00002E98
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00004CA5 File Offset: 0x00002EA5
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004CC6 File Offset: 0x00002EC6
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00004CD4 File Offset: 0x00002ED4
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
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", new object[]
					{
						value,
						1024
					}));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004D4C File Offset: 0x00002F4C
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004D5E File Offset: 0x00002F5E
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004D70 File Offset: 0x00002F70
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004DD0 File Offset: 0x00002FD0
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004975 File Offset: 0x00002B75
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004DF8 File Offset: 0x00002FF8
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

		// Token: 0x06000066 RID: 102 RVA: 0x00004E1D File Offset: 0x0000301D
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000049BD File Offset: 0x00002BBD
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004E44 File Offset: 0x00003044
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000049BD File Offset: 0x00002BBD
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

		// Token: 0x0600006A RID: 106 RVA: 0x00004EAC File Offset: 0x000030AC
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

		// Token: 0x0600006B RID: 107 RVA: 0x000049BD File Offset: 0x00002BBD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000049BD File Offset: 0x00002BBD
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004F1C File Offset: 0x0000311C
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

		// Token: 0x0600006E RID: 110 RVA: 0x00004F7C File Offset: 0x0000317C
		private int EmitHeader()
		{
			byte[] array = (this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment);
			byte[] array2 = (this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName);
			int num = (this.Comment == null) ? 0 : (array.Length + 1);
			int num2 = (this.FileName == null) ? 0 : (array2.Length + 1);
			byte[] array3 = new byte[10 + num + num2];
			int num3 = 0;
			array3[num3++] = 31;
			array3[num3++] = 139;
			array3[num3++] = 8;
			byte b = 0;
			if (this.Comment != null)
			{
				b ^= 16;
			}
			if (this.FileName != null)
			{
				b ^= 8;
			}
			array3[num3++] = b;
			if (this.LastModified == null)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			Array.Copy(BitConverter.GetBytes((int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds), 0, array3, num3, 4);
			num3 += 4;
			array3[num3++] = 0;
			array3[num3++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num3, num2 - 1);
				num3 += num2 - 1;
				array3[num3++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num3, num - 1);
				num3 += num - 1;
				array3[num3++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005118 File Offset: 0x00003318
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

		// Token: 0x06000070 RID: 112 RVA: 0x00005160 File Offset: 0x00003360
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

		// Token: 0x06000071 RID: 113 RVA: 0x000051A8 File Offset: 0x000033A8
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

		// Token: 0x06000072 RID: 114 RVA: 0x000051EC File Offset: 0x000033EC
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

		// Token: 0x04000066 RID: 102
		public DateTime? LastModified;

		// Token: 0x04000067 RID: 103
		private int _headerByteCount;

		// Token: 0x04000068 RID: 104
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000069 RID: 105
		private bool _disposed;

		// Token: 0x0400006A RID: 106
		private bool _firstReadDone;

		// Token: 0x0400006B RID: 107
		private string _FileName;

		// Token: 0x0400006C RID: 108
		private string _Comment;

		// Token: 0x0400006D RID: 109
		private int _Crc32;

		// Token: 0x0400006E RID: 110
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);

		// Token: 0x0400006F RID: 111
		internal static readonly Encoding iso8859dash1 = new Iso8859Dash1Encoding();
	}
}
