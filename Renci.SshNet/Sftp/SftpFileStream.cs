using System;
using System.IO;
using System.Threading;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000036 RID: 54
	public class SftpFileStream : Stream
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		public override bool CanSeek
		{
			get
			{
				return this._canSeek;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000F7B8 File Offset: 0x0000D9B8
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000CACF File Offset: 0x0000ACCF
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		public override long Length
		{
			get
			{
				object @lock = this._lock;
				long size;
				lock (@lock)
				{
					this.CheckSessionIsOpen();
					if (!this.CanSeek)
					{
						throw new NotSupportedException("Seek operation is not supported.");
					}
					if (this._bufferOwnedByWrite)
					{
						this.FlushWriteBuffer();
					}
					this._attributes = this._session.RequestFStat(this._handle);
					if (this._attributes == null || this._attributes.Size <= -1L)
					{
						throw new IOException("Seek operation failed.");
					}
					size = this._attributes.Size;
				}
				return size;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000F868 File Offset: 0x0000DA68
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000F889 File Offset: 0x0000DA89
		public override long Position
		{
			get
			{
				this.CheckSessionIsOpen();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seek operation not supported.");
				}
				return this._position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000F894 File Offset: 0x0000DA94
		public virtual bool IsAsync
		{
			get
			{
				return this._isAsync;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000F89C File Offset: 0x0000DA9C
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public string Name { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000F8AD File Offset: 0x0000DAAD
		public virtual byte[] Handle
		{
			get
			{
				this.Flush();
				return this._handle;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000F8BB File Offset: 0x0000DABB
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000F8C3 File Offset: 0x0000DAC3
		public TimeSpan Timeout { get; set; }

		// Token: 0x06000462 RID: 1122 RVA: 0x0000F8CC File Offset: 0x0000DACC
		internal SftpFileStream(ISftpSession session, string path, FileMode mode, FileAccess access, int bufferSize) : this(session, path, mode, access, bufferSize, false)
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000F8DC File Offset: 0x0000DADC
		internal SftpFileStream(ISftpSession session, string path, FileMode mode, FileAccess access, int bufferSize, bool useAsync)
		{
			if (session == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (mode < FileMode.CreateNew || mode > FileMode.Append)
			{
				throw new ArgumentOutOfRangeException("mode");
			}
			this.Timeout = TimeSpan.FromSeconds(30.0);
			this.Name = path;
			this._session = session;
			this._ownsHandle = true;
			this._isAsync = useAsync;
			this._bufferPosition = 0;
			this._bufferLen = 0;
			this._bufferOwnedByWrite = false;
			this._canRead = ((access & FileAccess.Read) > (FileAccess)0);
			this._canSeek = true;
			this._canWrite = ((access & FileAccess.Write) > (FileAccess)0);
			this._position = 0L;
			this._serverFilePosition = 0UL;
			Flags flags = Flags.None;
			switch (access)
			{
			case FileAccess.Read:
				flags |= Flags.Read;
				break;
			case FileAccess.Write:
				flags |= Flags.Write;
				break;
			case FileAccess.ReadWrite:
				flags |= Flags.Read;
				flags |= Flags.Write;
				break;
			}
			switch (mode)
			{
			case FileMode.CreateNew:
				flags |= Flags.CreateNew;
				break;
			case FileMode.Create:
				this._handle = this._session.RequestOpen(path, flags | Flags.Truncate, true);
				if (this._handle == null)
				{
					flags |= Flags.CreateNew;
				}
				else
				{
					flags |= Flags.Truncate;
				}
				break;
			case FileMode.OpenOrCreate:
				flags |= Flags.CreateNewOrOpen;
				break;
			case FileMode.Truncate:
				flags |= Flags.Truncate;
				break;
			case FileMode.Append:
				flags |= Flags.Append;
				break;
			}
			if (this._handle == null)
			{
				this._handle = this._session.RequestOpen(path, flags, false);
			}
			this._attributes = this._session.RequestFStat(this._handle);
			this._readBufferSize = (int)session.CalculateOptimalReadLength((uint)bufferSize);
			this._readBuffer = new byte[this._readBufferSize];
			this._writeBufferSize = (int)session.CalculateOptimalWriteLength((uint)bufferSize, this._handle);
			this._writeBuffer = new byte[this._writeBufferSize];
			if (mode == FileMode.Append)
			{
				this._position = this._attributes.Size;
				this._serverFilePosition = (ulong)this._attributes.Size;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		~SftpFileStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000FB28 File Offset: 0x0000DD28
		public override void Flush()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.CheckSessionIsOpen();
				if (this._bufferOwnedByWrite)
				{
					this.FlushWriteBuffer();
				}
				else
				{
					this.FlushReadBuffer();
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000FB80 File Offset: 0x0000DD80
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Invalid array range.");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.CheckSessionIsOpen();
				this.SetupRead();
				while (count > 0)
				{
					int num2 = this._bufferLen - this._bufferPosition;
					if (num2 <= 0)
					{
						this._bufferPosition = 0;
						byte[] array = this._session.RequestRead(this._handle, (ulong)this._position, (uint)this._readBufferSize);
						this._bufferLen = array.Length;
						Buffer.BlockCopy(array, 0, this._readBuffer, 0, this._bufferLen);
						this._serverFilePosition = (ulong)this._position;
						if (this._bufferLen < 0)
						{
							this._bufferLen = 0;
							throw new IOException("Read operation failed.");
						}
						if (this._bufferLen == 0)
						{
							break;
						}
						num2 = this._bufferLen;
					}
					if (num2 > count)
					{
						num2 = count;
					}
					Buffer.BlockCopy(this._readBuffer, this._bufferPosition, buffer, offset, num2);
					num += num2;
					offset += num2;
					count -= num2;
					this._bufferPosition += num2;
					this._position += (long)num2;
				}
			}
			return num;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public override int ReadByte()
		{
			object @lock = this._lock;
			int result;
			lock (@lock)
			{
				this.CheckSessionIsOpen();
				this.SetupRead();
				if (this._bufferPosition >= this._bufferLen)
				{
					this._bufferPosition = 0;
					byte[] array = this._session.RequestRead(this._handle, (ulong)this._position, (uint)this._readBufferSize);
					this._bufferLen = array.Length;
					Buffer.BlockCopy(array, 0, this._readBuffer, 0, this._readBufferSize);
					this._serverFilePosition = (ulong)this._position;
					if (this._bufferLen < 0)
					{
						this._bufferLen = 0;
						throw new IOException("Read operation failed.");
					}
					if (this._bufferLen == 0)
					{
						return -1;
					}
				}
				this._position += 1L;
				byte[] readBuffer = this._readBuffer;
				int bufferPosition = this._bufferPosition;
				this._bufferPosition = bufferPosition + 1;
				result = readBuffer[bufferPosition];
			}
			return result;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = -1L;
			object @lock = this._lock;
			long result;
			lock (@lock)
			{
				this.CheckSessionIsOpen();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seek is not supported.");
				}
				if (origin == SeekOrigin.Begin && offset == this._position)
				{
					result = offset;
				}
				else if (origin == SeekOrigin.Current && offset == 0L)
				{
					result = this._position;
				}
				else
				{
					this._attributes = this._session.RequestFStat(this._handle);
					if (this._bufferOwnedByWrite)
					{
						this.FlushWriteBuffer();
						switch (origin)
						{
						case SeekOrigin.Begin:
							num = offset;
							break;
						case SeekOrigin.Current:
							num = this._position + offset;
							break;
						case SeekOrigin.End:
							num = this._attributes.Size - offset;
							break;
						}
						if (num == -1L)
						{
							throw new EndOfStreamException("End of stream.");
						}
						this._position = num;
						this._serverFilePosition = (ulong)num;
					}
					else
					{
						if (origin == SeekOrigin.Begin)
						{
							num = this._position - (long)this._bufferPosition;
							if (offset >= num && offset < num + (long)this._bufferLen)
							{
								this._bufferPosition = (int)(offset - num);
								this._position = offset;
								return this._position;
							}
						}
						else if (origin == SeekOrigin.Current)
						{
							num = this._position + offset;
							if (num >= this._position - (long)this._bufferPosition && num < this._position - (long)this._bufferPosition + (long)this._bufferLen)
							{
								this._bufferPosition = (int)(num - (this._position - (long)this._bufferPosition));
								this._position = num;
								return this._position;
							}
						}
						this._bufferPosition = 0;
						this._bufferLen = 0;
						switch (origin)
						{
						case SeekOrigin.Begin:
							num = offset;
							break;
						case SeekOrigin.Current:
							num = this._position + offset;
							break;
						case SeekOrigin.End:
							num = this._attributes.Size - offset;
							break;
						}
						if (num < 0L)
						{
							throw new EndOfStreamException();
						}
						this._position = num;
					}
					result = this._position;
				}
			}
			return result;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.CheckSessionIsOpen();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seek is not supported.");
				}
				this.SetupWrite();
				this._attributes.Size = value;
				this._session.RequestFSetStat(this._handle, this._attributes);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010068 File Offset: 0x0000E268
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Invalid array range.");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.CheckSessionIsOpen();
				this.SetupWrite();
				while (count > 0)
				{
					int num = this._writeBufferSize - this._bufferPosition;
					if (num <= 0)
					{
						using (AutoResetEvent autoResetEvent = new AutoResetEvent(false))
						{
							this._session.RequestWrite(this._handle, this._serverFilePosition, this._writeBuffer, this._bufferPosition, autoResetEvent, null);
							this._serverFilePosition += (ulong)((long)this._bufferPosition);
						}
						this._bufferPosition = 0;
						num = this._writeBufferSize;
					}
					if (num > count)
					{
						num = count;
					}
					if (this._bufferPosition == 0 && num == this._writeBufferSize)
					{
						using (AutoResetEvent autoResetEvent2 = new AutoResetEvent(false))
						{
							this._session.RequestWrite(this._handle, this._serverFilePosition, buffer, num, autoResetEvent2, null);
							this._serverFilePosition += (ulong)((long)num);
							goto IL_147;
						}
						goto IL_125;
					}
					goto IL_125;
					IL_147:
					this._position += (long)num;
					offset += num;
					count -= num;
					continue;
					IL_125:
					Buffer.BlockCopy(buffer, offset, this._writeBuffer, this._bufferPosition, num);
					this._bufferPosition += num;
					goto IL_147;
				}
				if (this._bufferPosition >= this._writeBufferSize)
				{
					using (AutoResetEvent autoResetEvent3 = new AutoResetEvent(false))
					{
						this._session.RequestWrite(this._handle, this._serverFilePosition, this._writeBuffer, this._bufferPosition, autoResetEvent3, null);
						this._serverFilePosition += (ulong)((long)this._bufferPosition);
					}
					this._bufferPosition = 0;
				}
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public override void WriteByte(byte value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.CheckSessionIsOpen();
				this.SetupWrite();
				if (this._bufferPosition >= this._writeBufferSize)
				{
					using (AutoResetEvent autoResetEvent = new AutoResetEvent(false))
					{
						this._session.RequestWrite(this._handle, this._serverFilePosition, this._writeBuffer, this._bufferPosition, autoResetEvent, null);
						this._serverFilePosition += (ulong)((long)this._bufferPosition);
					}
					this._bufferPosition = 0;
				}
				byte[] writeBuffer = this._writeBuffer;
				int bufferPosition = this._bufferPosition;
				this._bufferPosition = bufferPosition + 1;
				writeBuffer[bufferPosition] = value;
				this._position += 1L;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00010390 File Offset: 0x0000E590
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._session != null && disposing)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._session != null)
					{
						this._canRead = false;
						this._canSeek = false;
						this._canWrite = false;
						if (this._handle != null)
						{
							if (this._session.IsOpen)
							{
								if (this._bufferOwnedByWrite)
								{
									this.FlushWriteBuffer();
								}
								if (this._ownsHandle)
								{
									this._session.RequestClose(this._handle);
								}
							}
							this._handle = null;
						}
						this._session = null;
					}
				}
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001044C File Offset: 0x0000E64C
		private void FlushReadBuffer()
		{
			if (this._canSeek)
			{
				if (this._bufferPosition < this._bufferLen)
				{
					this._position -= (long)this._bufferPosition;
				}
				this._bufferPosition = 0;
				this._bufferLen = 0;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00010488 File Offset: 0x0000E688
		private void FlushWriteBuffer()
		{
			if (this._bufferPosition > 0)
			{
				using (AutoResetEvent autoResetEvent = new AutoResetEvent(false))
				{
					this._session.RequestWrite(this._handle, this._serverFilePosition, this._writeBuffer, this._bufferPosition, autoResetEvent, null);
					this._serverFilePosition += (ulong)((long)this._bufferPosition);
				}
				this._bufferPosition = 0;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00010504 File Offset: 0x0000E704
		private void SetupRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Read not supported.");
			}
			if (this._bufferOwnedByWrite)
			{
				this.FlushWriteBuffer();
				this._bufferOwnedByWrite = false;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001052E File Offset: 0x0000E72E
		private void SetupWrite()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Write not supported.");
			}
			if (!this._bufferOwnedByWrite)
			{
				this.FlushReadBuffer();
				this._bufferOwnedByWrite = true;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00010558 File Offset: 0x0000E758
		private void CheckSessionIsOpen()
		{
			if (this._session == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this._session.IsOpen)
			{
				throw new ObjectDisposedException(base.GetType().FullName, "Cannot access a closed SFTP session.");
			}
		}

		// Token: 0x0400016A RID: 362
		private byte[] _handle;

		// Token: 0x0400016B RID: 363
		private readonly bool _ownsHandle;

		// Token: 0x0400016C RID: 364
		private readonly bool _isAsync;

		// Token: 0x0400016D RID: 365
		private ISftpSession _session;

		// Token: 0x0400016E RID: 366
		private readonly int _readBufferSize;

		// Token: 0x0400016F RID: 367
		private readonly byte[] _readBuffer;

		// Token: 0x04000170 RID: 368
		private readonly int _writeBufferSize;

		// Token: 0x04000171 RID: 369
		private readonly byte[] _writeBuffer;

		// Token: 0x04000172 RID: 370
		private int _bufferPosition;

		// Token: 0x04000173 RID: 371
		private int _bufferLen;

		// Token: 0x04000174 RID: 372
		private long _position;

		// Token: 0x04000175 RID: 373
		private bool _bufferOwnedByWrite;

		// Token: 0x04000176 RID: 374
		private bool _canRead;

		// Token: 0x04000177 RID: 375
		private bool _canSeek;

		// Token: 0x04000178 RID: 376
		private bool _canWrite;

		// Token: 0x04000179 RID: 377
		private ulong _serverFilePosition;

		// Token: 0x0400017A RID: 378
		private SftpFileAttributes _attributes;

		// Token: 0x0400017B RID: 379
		private readonly object _lock = new object();
	}
}
