using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x0200001B RID: 27
	internal class ZlibBaseStream : Stream
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00009A6E File Offset: 0x00007C6E
		internal int Crc32
		{
			get
			{
				if (this.crc == null)
				{
					return 0;
				}
				return this.crc.Crc32Result;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009A88 File Offset: 0x00007C88
		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen)
		{
			this._flushMode = FlushType.None;
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._compressionMode = compressionMode;
			this._flavor = flavor;
			this._level = level;
			if (flavor == ZlibStreamFlavor.GZIP)
			{
				this.crc = new CRC32();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00009AF9 File Offset: 0x00007CF9
		protected internal bool _wantCompress
		{
			get
			{
				return this._compressionMode == CompressionMode.Compress;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00009B04 File Offset: 0x00007D04
		private ZlibCodec z
		{
			get
			{
				if (this._z == null)
				{
					bool flag = this._flavor == ZlibStreamFlavor.ZLIB;
					this._z = new ZlibCodec();
					if (this._compressionMode == CompressionMode.Decompress)
					{
						this._z.InitializeInflate(flag);
					}
					else
					{
						this._z.Strategy = this.Strategy;
						this._z.InitializeDeflate(this._level, flag);
					}
				}
				return this._z;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00009B74 File Offset: 0x00007D74
		private byte[] workingBuffer
		{
			get
			{
				if (this._workingBuffer == null)
				{
					this._workingBuffer = new byte[this._bufferSize];
				}
				return this._workingBuffer;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00009B98 File Offset: 0x00007D98
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, count);
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				this._streamMode = ZlibBaseStream.StreamMode.Writer;
			}
			else if (this._streamMode != ZlibBaseStream.StreamMode.Writer)
			{
				throw new ZlibException("Cannot Write after Reading.");
			}
			if (count == 0)
			{
				return;
			}
			this.z.InputBuffer = buffer;
			this._z.NextIn = offset;
			this._z.AvailableBytesIn = count;
			for (;;)
			{
				this._z.OutputBuffer = this.workingBuffer;
				this._z.NextOut = 0;
				this._z.AvailableBytesOut = this._workingBuffer.Length;
				int num = this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode);
				if (num != 0 && num != 1)
				{
					break;
				}
				this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
				bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
				if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
				{
					flag = (this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0);
				}
				if (flag)
				{
					return;
				}
			}
			throw new ZlibException((this._wantCompress ? "de" : "in") + "flating: " + this._z.Message);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00009D20 File Offset: 0x00007F20
		private void finish()
		{
			if (this._z == null)
			{
				return;
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Writer)
			{
				int num;
				for (;;)
				{
					this._z.OutputBuffer = this.workingBuffer;
					this._z.NextOut = 0;
					this._z.AvailableBytesOut = this._workingBuffer.Length;
					num = (this._wantCompress ? this._z.Deflate(FlushType.Finish) : this._z.Inflate(FlushType.Finish));
					if (num != 1 && num != 0)
					{
						break;
					}
					if (this._workingBuffer.Length - this._z.AvailableBytesOut > 0)
					{
						this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
					}
					bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
					if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
					{
						flag = (this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0);
					}
					if (flag)
					{
						goto Block_12;
					}
				}
				string text = (this._wantCompress ? "de" : "in") + "flating";
				if (this._z.Message == null)
				{
					throw new ZlibException(string.Format("{0}: (rc = {1})", new object[]
					{
						text,
						num
					}));
				}
				throw new ZlibException(text + ": " + this._z.Message);
				Block_12:
				this.Flush();
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					if (this._wantCompress)
					{
						int crc32Result = this.crc.Crc32Result;
						this._stream.Write(BitConverter.GetBytes(crc32Result), 0, 4);
						int num2 = (int)(this.crc.TotalBytesRead & (long)((ulong)-1));
						this._stream.Write(BitConverter.GetBytes(num2), 0, 4);
						return;
					}
					throw new ZlibException("Writing with decompression is not supported.");
				}
			}
			else if (this._streamMode == ZlibBaseStream.StreamMode.Reader && this._flavor == ZlibStreamFlavor.GZIP)
			{
				if (!this._wantCompress)
				{
					long totalBytesOut = this._z.TotalBytesOut;
					return;
				}
				throw new ZlibException("Reading with compression is not supported.");
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00009F43 File Offset: 0x00008143
		private void end()
		{
			if (this.z == null)
			{
				return;
			}
			if (this._wantCompress)
			{
				this._z.EndDeflate();
			}
			else
			{
				this._z.EndInflate();
			}
			this._z = null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009F78 File Offset: 0x00008178
		public void Close()
		{
			if (this._stream == null)
			{
				return;
			}
			try
			{
				this.finish();
			}
			finally
			{
				this.end();
				if (!this._leaveOpen)
				{
					this._stream.Dispose();
				}
				this._stream = null;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00009FC8 File Offset: 0x000081C8
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000049BD File Offset: 0x00002BBD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009FDC File Offset: 0x000081DC
		public override void SetLength(long value)
		{
			this._stream.SetLength(value);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009FEC File Offset: 0x000081EC
		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			while (this._stream.Read(this._buf1, 0, 1) == 1)
			{
				if (this._buf1[0] == 0)
				{
					flag = true;
				}
				else
				{
					list.Add(this._buf1[0]);
				}
				if (flag)
				{
					byte[] array = list.ToArray();
					return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
				}
			}
			throw new ZlibException("Unexpected EOF reading GZIP header.");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A058 File Offset: 0x00008258
		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = new byte[10];
			int num2 = this._stream.Read(array, 0, array.Length);
			if (num2 == 0)
			{
				return 0;
			}
			if (num2 != 10)
			{
				throw new ZlibException("Not a valid GZIP stream.");
			}
			if (array[0] != 31 || array[1] != 139 || array[2] != 8)
			{
				throw new ZlibException("Bad GZIP header.");
			}
			int num3 = BitConverter.ToInt32(array, 4);
			this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double)num3);
			num += num2;
			if ((array[3] & 4) == 4)
			{
				num2 = this._stream.Read(array, 0, 2);
				num += num2;
				short num4 = (short)((int)array[0] + (int)array[1] * 256);
				byte[] array2 = new byte[(int)num4];
				num2 = this._stream.Read(array2, 0, array2.Length);
				if (num2 != (int)num4)
				{
					throw new ZlibException("Unexpected end-of-file reading GZIP header.");
				}
				num += num2;
			}
			if ((array[3] & 8) == 8)
			{
				this._GzipFileName = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 16) == 16)
			{
				this._GzipComment = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 2) == 2)
			{
				this.Read(this._buf1, 0, 1);
			}
			return num;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A178 File Offset: 0x00008378
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._stream.CanRead)
				{
					throw new ZlibException("The stream is not readable.");
				}
				this._streamMode = ZlibBaseStream.StreamMode.Reader;
				this.z.AvailableBytesIn = 0;
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
					if (this._gzipHeaderByteCount == 0)
					{
						return 0;
					}
				}
			}
			if (this._streamMode != ZlibBaseStream.StreamMode.Reader)
			{
				throw new ZlibException("Cannot Read after Writing.");
			}
			if (count == 0)
			{
				return 0;
			}
			if (this._nomoreinput && this._wantCompress)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < buffer.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.GetLength(0))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._z.OutputBuffer = buffer;
			this._z.NextOut = offset;
			this._z.AvailableBytesOut = count;
			this._z.InputBuffer = this.workingBuffer;
			int num;
			for (;;)
			{
				if (this._z.AvailableBytesIn == 0 && !this._nomoreinput)
				{
					this._z.NextIn = 0;
					this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
					if (this._z.AvailableBytesIn == 0)
					{
						this._nomoreinput = true;
					}
				}
				num = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
				if (this._nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_20;
				}
				if (((this._nomoreinput || num == 1) && this._z.AvailableBytesOut == count) || this._z.AvailableBytesOut <= 0 || this._nomoreinput || num != 0)
				{
					goto IL_219;
				}
			}
			return 0;
			Block_20:
			throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", new object[]
			{
				this._wantCompress ? "de" : "in",
				num,
				this._z.Message
			}));
			IL_219:
			if (this._z.AvailableBytesOut > 0)
			{
				if (num == 0)
				{
					int availableBytesIn = this._z.AvailableBytesIn;
				}
				if (this._nomoreinput && this._wantCompress)
				{
					num = this._z.Deflate(FlushType.Finish);
					if (num != 0 && num != 1)
					{
						throw new ZlibException(string.Format("Deflating:  rc={0}  msg={1}", new object[]
						{
							num,
							this._z.Message
						}));
					}
				}
			}
			num = count - this._z.AvailableBytesOut;
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000A431 File Offset: 0x00008631
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000A43E File Offset: 0x0000863E
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000A44B File Offset: 0x0000864B
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000A458 File Offset: 0x00008658
		public override long Length
		{
			get
			{
				return this._stream.Length;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000049BD File Offset: 0x00002BBD
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000049BD File Offset: 0x00002BBD
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000A474 File Offset: 0x00008674
		public static void CompressString(string s, Stream compressor)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			try
			{
				compressor.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				if (compressor != null)
				{
					compressor.Dispose();
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public static void CompressBuffer(byte[] b, Stream compressor)
		{
			try
			{
				compressor.Write(b, 0, b.Length);
			}
			finally
			{
				if (compressor != null)
				{
					compressor.Dispose();
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public static string UncompressString(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			Encoding utf = Encoding.UTF8;
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						decompressor.Dispose();
					}
				}
				memoryStream.Seek(0L, 0);
				result = new StreamReader(memoryStream, utf).ReadToEnd();
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000A580 File Offset: 0x00008780
		public static byte[] UncompressBuffer(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						decompressor.Dispose();
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0400012B RID: 299
		protected internal ZlibCodec _z;

		// Token: 0x0400012C RID: 300
		protected internal ZlibBaseStream.StreamMode _streamMode = ZlibBaseStream.StreamMode.Undefined;

		// Token: 0x0400012D RID: 301
		protected internal FlushType _flushMode;

		// Token: 0x0400012E RID: 302
		protected internal ZlibStreamFlavor _flavor;

		// Token: 0x0400012F RID: 303
		protected internal CompressionMode _compressionMode;

		// Token: 0x04000130 RID: 304
		protected internal CompressionLevel _level;

		// Token: 0x04000131 RID: 305
		protected internal bool _leaveOpen;

		// Token: 0x04000132 RID: 306
		protected internal byte[] _workingBuffer;

		// Token: 0x04000133 RID: 307
		protected internal int _bufferSize = 16384;

		// Token: 0x04000134 RID: 308
		protected internal byte[] _buf1 = new byte[1];

		// Token: 0x04000135 RID: 309
		protected internal Stream _stream;

		// Token: 0x04000136 RID: 310
		protected internal CompressionStrategy Strategy;

		// Token: 0x04000137 RID: 311
		private CRC32 crc;

		// Token: 0x04000138 RID: 312
		private bool _nomoreinput;

		// Token: 0x04000139 RID: 313
		protected internal string _GzipFileName;

		// Token: 0x0400013A RID: 314
		protected internal string _GzipComment;

		// Token: 0x0400013B RID: 315
		protected internal DateTime _GzipMtime;

		// Token: 0x0400013C RID: 316
		protected internal int _gzipHeaderByteCount;

		// Token: 0x02000027 RID: 39
		internal enum StreamMode
		{
			// Token: 0x040001AC RID: 428
			Writer,
			// Token: 0x040001AD RID: 429
			Reader,
			// Token: 0x040001AE RID: 430
			Undefined
		}
	}
}
