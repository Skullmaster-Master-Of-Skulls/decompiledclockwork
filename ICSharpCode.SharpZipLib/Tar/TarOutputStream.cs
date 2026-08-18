using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200003E RID: 62
	public class TarOutputStream : Stream
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000FA00 File Offset: 0x0000EA00
		public TarOutputStream(Stream outputStream) : this(outputStream, 20)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000FA0C File Offset: 0x0000EA0C
		public TarOutputStream(Stream outputStream, int blockFactor)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			this.outputStream = outputStream;
			this.buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
			this.assemblyBuffer = new byte[512];
			this.blockBuffer = new byte[512];
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000FA61 File Offset: 0x0000EA61
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000FA6E File Offset: 0x0000EA6E
		public bool IsStreamOwner
		{
			get
			{
				return this.buffer.IsStreamOwner;
			}
			set
			{
				this.buffer.IsStreamOwner = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000FA7C File Offset: 0x0000EA7C
		public override bool CanRead
		{
			get
			{
				return this.outputStream.CanRead;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000FA89 File Offset: 0x0000EA89
		public override bool CanSeek
		{
			get
			{
				return this.outputStream.CanSeek;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000FA96 File Offset: 0x0000EA96
		public override bool CanWrite
		{
			get
			{
				return this.outputStream.CanWrite;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000FAA3 File Offset: 0x0000EAA3
		public override long Length
		{
			get
			{
				return this.outputStream.Length;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000FAB0 File Offset: 0x0000EAB0
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000FABD File Offset: 0x0000EABD
		public override long Position
		{
			get
			{
				return this.outputStream.Position;
			}
			set
			{
				this.outputStream.Position = value;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000FACB File Offset: 0x0000EACB
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.outputStream.Seek(offset, origin);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000FADA File Offset: 0x0000EADA
		public override void SetLength(long value)
		{
			this.outputStream.SetLength(value);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000FAE8 File Offset: 0x0000EAE8
		public override int ReadByte()
		{
			return this.outputStream.ReadByte();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000FAF5 File Offset: 0x0000EAF5
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.outputStream.Read(buffer, offset, count);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000FB05 File Offset: 0x0000EB05
		public override void Flush()
		{
			this.outputStream.Flush();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000FB12 File Offset: 0x0000EB12
		public void Finish()
		{
			if (this.IsEntryOpen)
			{
				this.CloseEntry();
			}
			this.WriteEofBlock();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000FB28 File Offset: 0x0000EB28
		public override void Close()
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				this.Finish();
				this.buffer.Close();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000FB4A File Offset: 0x0000EB4A
		public int RecordSize
		{
			get
			{
				return this.buffer.RecordSize;
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000FB57 File Offset: 0x0000EB57
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.buffer.RecordSize;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000FB64 File Offset: 0x0000EB64
		private bool IsEntryOpen
		{
			get
			{
				return this.currBytes < this.currSize;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000FB74 File Offset: 0x0000EB74
		public void PutNextEntry(TarEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.TarHeader.Name.Length > 100)
			{
				TarHeader tarHeader = new TarHeader();
				tarHeader.TypeFlag = 76;
				tarHeader.Name += "././@LongLink";
				tarHeader.Mode = 420;
				tarHeader.UserId = entry.UserId;
				tarHeader.GroupId = entry.GroupId;
				tarHeader.GroupName = entry.GroupName;
				tarHeader.UserName = entry.UserName;
				tarHeader.LinkName = "";
				tarHeader.Size = (long)(entry.TarHeader.Name.Length + 1);
				tarHeader.WriteHeader(this.blockBuffer);
				this.buffer.WriteBlock(this.blockBuffer);
				int i = 0;
				while (i < entry.TarHeader.Name.Length)
				{
					Array.Clear(this.blockBuffer, 0, this.blockBuffer.Length);
					TarHeader.GetAsciiBytes(entry.TarHeader.Name, i, this.blockBuffer, 0, 512);
					i += 512;
					this.buffer.WriteBlock(this.blockBuffer);
				}
			}
			entry.WriteEntryHeader(this.blockBuffer);
			this.buffer.WriteBlock(this.blockBuffer);
			this.currBytes = 0L;
			this.currSize = (entry.IsDirectory ? 0L : entry.Size);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000FCE8 File Offset: 0x0000ECE8
		public void CloseEntry()
		{
			if (this.assemblyBufferLength > 0)
			{
				Array.Clear(this.assemblyBuffer, this.assemblyBufferLength, this.assemblyBuffer.Length - this.assemblyBufferLength);
				this.buffer.WriteBlock(this.assemblyBuffer);
				this.currBytes += (long)this.assemblyBufferLength;
				this.assemblyBufferLength = 0;
			}
			if (this.currBytes < this.currSize)
			{
				string message = string.Format("Entry closed at '{0}' before the '{1}' bytes specified in the header were written", this.currBytes, this.currSize);
				throw new TarException(message);
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000FD80 File Offset: 0x0000ED80
		public override void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000FDA4 File Offset: 0x0000EDA4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset and count combination is invalid");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			if (this.currBytes + (long)count > this.currSize)
			{
				string message = string.Format("request to write '{0}' bytes exceeds size in header of '{1}' bytes", count, this.currSize);
				throw new ArgumentOutOfRangeException("count", message);
			}
			if (this.assemblyBufferLength > 0)
			{
				if (this.assemblyBufferLength + count >= this.blockBuffer.Length)
				{
					int num = this.blockBuffer.Length - this.assemblyBufferLength;
					Array.Copy(this.assemblyBuffer, 0, this.blockBuffer, 0, this.assemblyBufferLength);
					Array.Copy(buffer, offset, this.blockBuffer, this.assemblyBufferLength, num);
					this.buffer.WriteBlock(this.blockBuffer);
					this.currBytes += (long)this.blockBuffer.Length;
					offset += num;
					count -= num;
					this.assemblyBufferLength = 0;
				}
				else
				{
					Array.Copy(buffer, offset, this.assemblyBuffer, this.assemblyBufferLength, count);
					offset += count;
					this.assemblyBufferLength += count;
					count -= count;
				}
			}
			while (count > 0)
			{
				if (count < this.blockBuffer.Length)
				{
					Array.Copy(buffer, offset, this.assemblyBuffer, this.assemblyBufferLength, count);
					this.assemblyBufferLength += count;
					return;
				}
				this.buffer.WriteBlock(buffer, offset);
				int num2 = this.blockBuffer.Length;
				this.currBytes += (long)num2;
				count -= num2;
				offset += num2;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000FF5A File Offset: 0x0000EF5A
		private void WriteEofBlock()
		{
			Array.Clear(this.blockBuffer, 0, this.blockBuffer.Length);
			this.buffer.WriteBlock(this.blockBuffer);
		}

		// Token: 0x040001D0 RID: 464
		private long currBytes;

		// Token: 0x040001D1 RID: 465
		private int assemblyBufferLength;

		// Token: 0x040001D2 RID: 466
		private bool isClosed;

		// Token: 0x040001D3 RID: 467
		protected long currSize;

		// Token: 0x040001D4 RID: 468
		protected byte[] blockBuffer;

		// Token: 0x040001D5 RID: 469
		protected byte[] assemblyBuffer;

		// Token: 0x040001D6 RID: 470
		protected TarBuffer buffer;

		// Token: 0x040001D7 RID: 471
		protected Stream outputStream;
	}
}
