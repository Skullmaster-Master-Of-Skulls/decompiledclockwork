using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000040 RID: 64
	internal class ZipSegmentedStream : Stream
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00011507 File Offset: 0x0000F707
		private ZipSegmentedStream()
		{
			this._exceptionPending = false;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011518 File Offset: 0x0000F718
		public static ZipSegmentedStream ForReading(string name, uint initialDiskNumber, uint maxDiskNumber)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.ReadOnly,
				CurrentSegment = initialDiskNumber,
				_maxDiskNumber = maxDiskNumber,
				_baseName = name
			};
			zipSegmentedStream._SetReadStream();
			return zipSegmentedStream;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00011550 File Offset: 0x0000F750
		public static ZipSegmentedStream ForWriting(string name, int maxSegmentSize)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.Write,
				CurrentSegment = 0U,
				_baseName = name,
				_maxSegmentSize = maxSegmentSize,
				_baseDir = Path.GetDirectoryName(name)
			};
			if (zipSegmentedStream._baseDir == "")
			{
				zipSegmentedStream._baseDir = ".";
			}
			zipSegmentedStream._SetWriteStream(0U);
			return zipSegmentedStream;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public static Stream ForUpdate(string name, uint diskNumber)
		{
			if (diskNumber >= 99U)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			string path = string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name)), diskNumber + 1U);
			return File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000115FE File Offset: 0x0000F7FE
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00011606 File Offset: 0x0000F806
		public bool ContiguousWrite { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001160F File Offset: 0x0000F80F
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00011617 File Offset: 0x0000F817
		public uint CurrentSegment
		{
			get
			{
				return this._currentDiskNumber;
			}
			private set
			{
				this._currentDiskNumber = value;
				this._currentName = null;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00011627 File Offset: 0x0000F827
		public string CurrentName
		{
			get
			{
				if (this._currentName == null)
				{
					this._currentName = this._NameForSegment(this.CurrentSegment);
				}
				return this._currentName;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00011649 File Offset: 0x0000F849
		public string CurrentTempName
		{
			get
			{
				return this._currentTempName;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00011654 File Offset: 0x0000F854
		private string _NameForSegment(uint diskNumber)
		{
			if (diskNumber >= 99U)
			{
				this._exceptionPending = true;
				throw new OverflowException("The number of zip segments would exceed 99.");
			}
			return string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(this._baseName), Path.GetFileNameWithoutExtension(this._baseName)), diskNumber + 1U);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000116A5 File Offset: 0x0000F8A5
		public uint ComputeSegment(int length)
		{
			if (this._innerStream.Position + (long)length > (long)this._maxSegmentSize)
			{
				return this.CurrentSegment + 1U;
			}
			return this.CurrentSegment;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000116D0 File Offset: 0x0000F8D0
		public override string ToString()
		{
			return string.Format("{0}[{1}][{2}], pos=0x{3:X})", new object[]
			{
				"ZipSegmentedStream",
				this.CurrentName,
				this.rwMode.ToString(),
				this.Position
			});
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011724 File Offset: 0x0000F924
		private void _SetReadStream()
		{
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
			}
			if (this.CurrentSegment + 1U == this._maxDiskNumber)
			{
				this._currentName = this._baseName;
			}
			this._innerStream = File.OpenRead(this.CurrentName);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00011774 File Offset: 0x0000F974
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.ReadOnly)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Read.");
			}
			int num = this._innerStream.Read(buffer, offset, count);
			int num2 = num;
			while (num2 != count)
			{
				if (this._innerStream.Position != this._innerStream.Length)
				{
					this._exceptionPending = true;
					throw new ZipException(string.Format("Read error in file {0}", this.CurrentName));
				}
				if (this.CurrentSegment + 1U == this._maxDiskNumber)
				{
					return num;
				}
				this.CurrentSegment += 1U;
				this._SetReadStream();
				offset += num2;
				count -= num2;
				num2 = this._innerStream.Read(buffer, offset, count);
				num += num2;
			}
			return num;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001182C File Offset: 0x0000FA2C
		private void _SetWriteStream(uint increment)
		{
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
				if (File.Exists(this.CurrentName))
				{
					File.Delete(this.CurrentName);
				}
				File.Move(this._currentTempName, this.CurrentName);
			}
			if (increment > 0U)
			{
				this.CurrentSegment += increment;
			}
			SharedUtilities.CreateAndOpenUniqueTempFile(this._baseDir, out this._innerStream, out this._currentTempName);
			if (this.CurrentSegment == 0U)
			{
				this._innerStream.Write(BitConverter.GetBytes(134695760), 0, 4);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000118C0 File Offset: 0x0000FAC0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Write.");
			}
			if (this.ContiguousWrite)
			{
				if (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					this._SetWriteStream(1U);
				}
			}
			else
			{
				while (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					int num = this._maxSegmentSize - (int)this._innerStream.Position;
					this._innerStream.Write(buffer, offset, num);
					this._SetWriteStream(1U);
					count -= num;
					offset += num;
				}
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00011968 File Offset: 0x0000FB68
		public long TruncateBackward(uint diskNumber, long offset)
		{
			if (diskNumber >= 99U)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new ZipException("bad state.");
			}
			if (diskNumber == this.CurrentSegment)
			{
				return this._innerStream.Seek(offset, SeekOrigin.Begin);
			}
			if (this._innerStream != null)
			{
				this._innerStream.Dispose();
				if (File.Exists(this._currentTempName))
				{
					File.Delete(this._currentTempName);
				}
			}
			for (uint num = this.CurrentSegment - 1U; num > diskNumber; num -= 1U)
			{
				string path = this._NameForSegment(num);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			this.CurrentSegment = diskNumber;
			for (int i = 0; i < 3; i++)
			{
				try
				{
					this._currentTempName = SharedUtilities.InternalGetTempFileName();
					File.Move(this.CurrentName, this._currentTempName);
					break;
				}
				catch (IOException)
				{
					if (i == 2)
					{
						throw;
					}
				}
			}
			this._innerStream = new FileStream(this._currentTempName, FileMode.Open);
			return this._innerStream.Seek(offset, SeekOrigin.Begin);
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public override bool CanRead
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.ReadOnly && this._innerStream != null && this._innerStream.CanRead;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00011A9C File Offset: 0x0000FC9C
		public override bool CanSeek
		{
			get
			{
				return this._innerStream != null && this._innerStream.CanSeek;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		public override bool CanWrite
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.Write && this._innerStream != null && this._innerStream.CanWrite;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00011AD3 File Offset: 0x0000FCD3
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00011AED File Offset: 0x0000FCED
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00011AFA File Offset: 0x0000FCFA
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00011B08 File Offset: 0x0000FD08
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00011B24 File Offset: 0x0000FD24
		public override void SetLength(long value)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException();
			}
			this._innerStream.SetLength(value);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00011B48 File Offset: 0x0000FD48
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._innerStream != null)
				{
					this._innerStream.Dispose();
					if (this.rwMode == ZipSegmentedStream.RwMode.Write)
					{
						bool exceptionPending = this._exceptionPending;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0400019A RID: 410
		private ZipSegmentedStream.RwMode rwMode;

		// Token: 0x0400019B RID: 411
		private bool _exceptionPending;

		// Token: 0x0400019C RID: 412
		private string _baseName;

		// Token: 0x0400019D RID: 413
		private string _baseDir;

		// Token: 0x0400019E RID: 414
		private string _currentName;

		// Token: 0x0400019F RID: 415
		private string _currentTempName;

		// Token: 0x040001A0 RID: 416
		private uint _currentDiskNumber;

		// Token: 0x040001A1 RID: 417
		private uint _maxDiskNumber;

		// Token: 0x040001A2 RID: 418
		private int _maxSegmentSize;

		// Token: 0x040001A3 RID: 419
		private Stream _innerStream;

		// Token: 0x02000041 RID: 65
		private enum RwMode
		{
			// Token: 0x040001A6 RID: 422
			None,
			// Token: 0x040001A7 RID: 423
			ReadOnly,
			// Token: 0x040001A8 RID: 424
			Write
		}
	}
}
