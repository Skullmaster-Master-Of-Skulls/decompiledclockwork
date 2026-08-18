using System;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zip
{
	// Token: 0x0200003D RID: 61
	public class ZipInputStream : Stream
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00010716 File Offset: 0x0000E916
		public ZipInputStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00010720 File Offset: 0x0000E920
		public ZipInputStream(string fileName)
		{
			Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this._Init(stream, false, fileName);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00010746 File Offset: 0x0000E946
		public ZipInputStream(Stream stream, bool leaveOpen)
		{
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00010758 File Offset: 0x0000E958
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._inputStream = stream;
			if (!this._inputStream.CanRead)
			{
				throw new ZipException("The stream must be readable.");
			}
			this._container = new ZipContainer(this);
			this._provisionalAlternateEncoding = Encoding.GetEncoding("IBM437");
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this._findRequired = true;
			this._name = (name ?? "(stream)");
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000107BE File Offset: 0x0000E9BE
		public override string ToString()
		{
			return string.Format("ZipInputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000107DB File Offset: 0x0000E9DB
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000107E3 File Offset: 0x0000E9E3
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				return this._provisionalAlternateEncoding;
			}
			set
			{
				this._provisionalAlternateEncoding = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000107EC File Offset: 0x0000E9EC
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public int CodecBufferSize { get; set; }

		// Token: 0x170000A6 RID: 166
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x000107FD File Offset: 0x0000E9FD
		public string Password
		{
			set
			{
				if (this._closed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._Password = value;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010820 File Offset: 0x0000EA20
		private void SetupStream()
		{
			this._crcStream = this._currentEntry.InternalOpenReader(this._Password);
			this._LeftToRead = this._crcStream.Length;
			this._needSetup = false;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00010851 File Offset: 0x0000EA51
		internal Stream ReadStream
		{
			get
			{
				return this._inputStream;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001085C File Offset: 0x0000EA5C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._closed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (this._needSetup)
			{
				this.SetupStream();
			}
			if (this._LeftToRead == 0L)
			{
				return 0;
			}
			int count2 = (this._LeftToRead > (long)count) ? count : ((int)this._LeftToRead);
			int num = this._crcStream.Read(buffer, offset, count2);
			this._LeftToRead -= (long)num;
			if (this._LeftToRead == 0L)
			{
				int crc = this._crcStream.Crc;
				this._currentEntry.VerifyCrcAfterExtract(crc);
				this._inputStream.Seek(this._endOfEntry, SeekOrigin.Begin);
			}
			return num;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010908 File Offset: 0x0000EB08
		public ZipEntry GetNextEntry()
		{
			if (this._findRequired)
			{
				long num = SharedUtilities.FindSignature(this._inputStream, 67324752);
				if (num == -1L)
				{
					return null;
				}
				this._inputStream.Seek(-4L, SeekOrigin.Current);
			}
			else if (this._firstEntry)
			{
				this._inputStream.Seek(this._endOfEntry, SeekOrigin.Begin);
			}
			this._currentEntry = ZipEntry.ReadEntry(this._container, !this._firstEntry);
			this._endOfEntry = this._inputStream.Position;
			this._firstEntry = true;
			this._needSetup = true;
			this._findRequired = false;
			return this._currentEntry;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000109A8 File Offset: 0x0000EBA8
		protected override void Dispose(bool disposing)
		{
			if (this._closed)
			{
				return;
			}
			if (disposing)
			{
				if (this._exceptionPending)
				{
					return;
				}
				if (!this._leaveUnderlyingStreamOpen)
				{
					this._inputStream.Dispose();
				}
			}
			this._closed = true;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000109D9 File Offset: 0x0000EBD9
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000109DC File Offset: 0x0000EBDC
		public override bool CanSeek
		{
			get
			{
				return this._inputStream.CanSeek;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000109E9 File Offset: 0x0000EBE9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000109EC File Offset: 0x0000EBEC
		public override long Length
		{
			get
			{
				return this._inputStream.Length;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000109F9 File Offset: 0x0000EBF9
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00010A06 File Offset: 0x0000EC06
		public override long Position
		{
			get
			{
				return this._inputStream.Position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00010A11 File Offset: 0x0000EC11
		public override void Flush()
		{
			throw new NotSupportedException("Flush");
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010A1D File Offset: 0x0000EC1D
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Write");
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010A2C File Offset: 0x0000EC2C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this._findRequired = true;
			return this._inputStream.Seek(offset, origin);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00010A4F File Offset: 0x0000EC4F
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000169 RID: 361
		private Stream _inputStream;

		// Token: 0x0400016A RID: 362
		private Encoding _provisionalAlternateEncoding;

		// Token: 0x0400016B RID: 363
		private ZipEntry _currentEntry;

		// Token: 0x0400016C RID: 364
		private bool _firstEntry;

		// Token: 0x0400016D RID: 365
		private bool _needSetup;

		// Token: 0x0400016E RID: 366
		private ZipContainer _container;

		// Token: 0x0400016F RID: 367
		private CrcCalculatorStream _crcStream;

		// Token: 0x04000170 RID: 368
		private long _LeftToRead;

		// Token: 0x04000171 RID: 369
		internal string _Password;

		// Token: 0x04000172 RID: 370
		private long _endOfEntry;

		// Token: 0x04000173 RID: 371
		private string _name;

		// Token: 0x04000174 RID: 372
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x04000175 RID: 373
		private bool _closed;

		// Token: 0x04000176 RID: 374
		private bool _findRequired;

		// Token: 0x04000177 RID: 375
		private bool _exceptionPending;
	}
}
