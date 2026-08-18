using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x02000020 RID: 32
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000B2CA File Offset: 0x000094CA
		public CrcCalculatorStream(Stream stream) : this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000B2DA File Offset: 0x000094DA
		public CrcCalculatorStream(Stream stream, bool leaveOpen) : this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000B2EA File Offset: 0x000094EA
		public CrcCalculatorStream(Stream stream, long length) : this(true, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000B306 File Offset: 0x00009506
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen) : this(leaveOpen, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000B322 File Offset: 0x00009522
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32) : this(leaveOpen, length, stream, crc32)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000B33F File Offset: 0x0000953F
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = (crc32 ?? new CRC32());
			this._lengthLimit = length;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000B376 File Offset: 0x00009576
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000B383 File Offset: 0x00009583
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000B390 File Offset: 0x00009590
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000B398 File Offset: 0x00009598
		public bool LeaveOpen
		{
			get
			{
				return this._leaveOpen;
			}
			set
			{
				this._leaveOpen = value;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = count;
			if (this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit)
			{
				if (this._Crc32.TotalBytesRead >= this._lengthLimit)
				{
					return 0;
				}
				long num2 = this._lengthLimit - this._Crc32.TotalBytesRead;
				if (num2 < (long)count)
				{
					num = (int)num2;
				}
			}
			int num3 = this._innerStream.Read(buffer, offset, num);
			if (num3 > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, num3);
			}
			return num3;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000B412 File Offset: 0x00009612
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000B434 File Offset: 0x00009634
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004975 File Offset: 0x00002B75
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000B444 File Offset: 0x00009644
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000B451 File Offset: 0x00009651
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000B45E File Offset: 0x0000965E
		public override long Length
		{
			get
			{
				if (this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit)
				{
					return this._innerStream.Length;
				}
				return this._lengthLimit;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000B376 File Offset: 0x00009576
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000090DB File Offset: 0x000072DB
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000B4A1 File Offset: 0x000096A1
		void IDisposable.Dispose()
		{
			base.Dispose();
			if (!this._leaveOpen)
			{
				this._innerStream.Dispose();
			}
		}

		// Token: 0x0400015E RID: 350
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x0400015F RID: 351
		internal Stream _innerStream;

		// Token: 0x04000160 RID: 352
		private CRC32 _Crc32;

		// Token: 0x04000161 RID: 353
		private long _lengthLimit = -99L;

		// Token: 0x04000162 RID: 354
		private bool _leaveOpen;
	}
}
