using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.IO.Compression
{
	// Token: 0x02000006 RID: 6
	[__DynamicallyInvokable]
	public class ZipArchive : IDisposable
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000026A7 File Offset: 0x000008A7
		[__DynamicallyInvokable]
		public ZipArchive(Stream stream) : this(stream, ZipArchiveMode.Read, false, null)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026B3 File Offset: 0x000008B3
		[__DynamicallyInvokable]
		public ZipArchive(Stream stream, ZipArchiveMode mode) : this(stream, mode, false, null)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026BF File Offset: 0x000008BF
		[__DynamicallyInvokable]
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen) : this(stream, mode, leaveOpen, null)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026CB File Offset: 0x000008CB
		[__DynamicallyInvokable]
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.EntryNameEncoding = entryNameEncoding;
			this.Init(stream, mode, leaveOpen);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026F2 File Offset: 0x000008F2
		[__DynamicallyInvokable]
		public ReadOnlyCollection<ZipArchiveEntry> Entries
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._mode == ZipArchiveMode.Create)
				{
					throw new NotSupportedException(Messages.EntriesInCreateMode);
				}
				this.ThrowIfDisposed();
				this.EnsureCentralDirectoryRead();
				return this._entriesCollection;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000271A File Offset: 0x0000091A
		[__DynamicallyInvokable]
		public ZipArchiveMode Mode
		{
			[__DynamicallyInvokable]
			get
			{
				return this._mode;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002724 File Offset: 0x00000924
		[__DynamicallyInvokable]
		public ZipArchiveEntry CreateEntry(string entryName)
		{
			return this.DoCreateEntry(entryName, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002741 File Offset: 0x00000941
		[__DynamicallyInvokable]
		public ZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
		{
			return this.DoCreateEntry(entryName, new CompressionLevel?(compressionLevel));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002750 File Offset: 0x00000950
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				ZipArchiveMode mode = this._mode;
				if (mode != ZipArchiveMode.Read)
				{
					int num = mode - ZipArchiveMode.Create;
					try
					{
						this.WriteFile();
					}
					catch (InvalidDataException)
					{
						this.CloseStreams();
						this._isDisposed = true;
						throw;
					}
				}
				this.CloseStreams();
				this._isDisposed = true;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027B0 File Offset: 0x000009B0
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027C0 File Offset: 0x000009C0
		[__DynamicallyInvokable]
		public ZipArchiveEntry GetEntry(string entryName)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (this._mode == ZipArchiveMode.Create)
			{
				throw new NotSupportedException(Messages.EntriesInCreateMode);
			}
			this.EnsureCentralDirectoryRead();
			ZipArchiveEntry result;
			this._entriesDictionary.TryGetValue(entryName, out result);
			return result;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002805 File Offset: 0x00000A05
		internal BinaryReader ArchiveReader
		{
			get
			{
				return this._archiveReader;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000280D File Offset: 0x00000A0D
		internal Stream ArchiveStream
		{
			get
			{
				return this._archiveStream;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002815 File Offset: 0x00000A15
		internal uint NumberOfThisDisk
		{
			get
			{
				return this._numberOfThisDisk;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000281D File Offset: 0x00000A1D
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002828 File Offset: 0x00000A28
		internal Encoding EntryNameEncoding
		{
			get
			{
				return this._entryNameEncoding;
			}
			private set
			{
				if (value != null && (value.Equals(Encoding.BigEndianUnicode) || value.Equals(Encoding.Unicode) || value.Equals(Encoding.UTF32) || value.Equals(Encoding.UTF7)))
				{
					throw new ArgumentException(Messages.EntryNameEncodingNotSupported, "entryNameEncoding");
				}
				this._entryNameEncoding = value;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002884 File Offset: 0x00000A84
		private ZipArchiveEntry DoCreateEntry(string entryName, CompressionLevel? compressionLevel)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentException(Messages.CannotBeEmpty, "entryName");
			}
			if (this._mode == ZipArchiveMode.Read)
			{
				throw new NotSupportedException(Messages.CreateInReadMode);
			}
			this.ThrowIfDisposed();
			ZipArchiveEntry zipArchiveEntry = (compressionLevel != null) ? new ZipArchiveEntry(this, entryName, compressionLevel.Value) : new ZipArchiveEntry(this, entryName);
			this.AddEntry(zipArchiveEntry);
			return zipArchiveEntry;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028F9 File Offset: 0x00000AF9
		internal void AcquireArchiveStream(ZipArchiveEntry entry)
		{
			if (this._archiveStreamOwner != null)
			{
				if (this._archiveStreamOwner.EverOpenedForWrite)
				{
					throw new IOException(Messages.CreateModeCreateEntryWhileOpen);
				}
				this._archiveStreamOwner.WriteAndFinishLocalEntry();
			}
			this._archiveStreamOwner = entry;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002930 File Offset: 0x00000B30
		private void AddEntry(ZipArchiveEntry entry)
		{
			this._entries.Add(entry);
			string fullName = entry.FullName;
			if (!this._entriesDictionary.ContainsKey(fullName))
			{
				this._entriesDictionary.Add(fullName, entry);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000296B File Offset: 0x00000B6B
		internal bool IsStillArchiveStreamOwner(ZipArchiveEntry entry)
		{
			return this._archiveStreamOwner == entry;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002976 File Offset: 0x00000B76
		internal void ReleaseArchiveStream(ZipArchiveEntry entry)
		{
			this._archiveStreamOwner = null;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000297F File Offset: 0x00000B7F
		internal void RemoveEntry(ZipArchiveEntry entry)
		{
			this._entries.Remove(entry);
			this._entriesDictionary.Remove(entry.FullName);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029A0 File Offset: 0x00000BA0
		internal void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029BC File Offset: 0x00000BBC
		private void CloseStreams()
		{
			if (!this._leaveOpen)
			{
				this._archiveStream.Close();
				if (this._backingStream != null)
				{
					this._backingStream.Close();
				}
				if (this._archiveReader != null)
				{
					this._archiveReader.Close();
					return;
				}
			}
			else if (this._backingStream != null)
			{
				this._archiveStream.Close();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A16 File Offset: 0x00000C16
		private void EnsureCentralDirectoryRead()
		{
			if (!this._readEntries)
			{
				this.ReadCentralDirectory();
				this._readEntries = true;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A30 File Offset: 0x00000C30
		private void Init(Stream stream, ZipArchiveMode mode, bool leaveOpen)
		{
			Stream stream2 = null;
			try
			{
				this._backingStream = null;
				switch (mode)
				{
				case ZipArchiveMode.Read:
					if (!stream.CanRead)
					{
						throw new ArgumentException(Messages.ReadModeCapabilities);
					}
					if (!stream.CanSeek)
					{
						this._backingStream = stream;
						stream = (stream2 = new MemoryStream());
						this._backingStream.CopyTo(stream);
						stream.Seek(0L, SeekOrigin.Begin);
					}
					break;
				case ZipArchiveMode.Create:
					if (!stream.CanWrite)
					{
						throw new ArgumentException(Messages.CreateModeCapabilities);
					}
					break;
				case ZipArchiveMode.Update:
					if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
					{
						throw new ArgumentException(Messages.UpdateModeCapabilities);
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("mode");
				}
				this._mode = mode;
				this._archiveStream = stream;
				this._archiveStreamOwner = null;
				if (mode == ZipArchiveMode.Create)
				{
					this._archiveReader = null;
				}
				else
				{
					this._archiveReader = new BinaryReader(stream);
				}
				this._entries = new List<ZipArchiveEntry>();
				this._entriesCollection = new ReadOnlyCollection<ZipArchiveEntry>(this._entries);
				this._entriesDictionary = new Dictionary<string, ZipArchiveEntry>();
				this._readEntries = false;
				this._leaveOpen = leaveOpen;
				this._centralDirectoryStart = 0L;
				this._isDisposed = false;
				this._numberOfThisDisk = 0U;
				this._archiveComment = null;
				switch (mode)
				{
				case ZipArchiveMode.Read:
					this.ReadEndOfCentralDirectory();
					goto IL_19F;
				case ZipArchiveMode.Create:
					this._readEntries = true;
					goto IL_19F;
				}
				if (this._archiveStream.Length == 0L)
				{
					this._readEntries = true;
				}
				else
				{
					this.ReadEndOfCentralDirectory();
					this.EnsureCentralDirectoryRead();
					foreach (ZipArchiveEntry zipArchiveEntry in this._entries)
					{
						zipArchiveEntry.ThrowIfNotOpenable(false, true);
					}
				}
				IL_19F:;
			}
			catch
			{
				if (stream2 != null)
				{
					stream2.Close();
				}
				throw;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C20 File Offset: 0x00000E20
		private void ReadCentralDirectory()
		{
			try
			{
				this._archiveStream.Seek(this._centralDirectoryStart, SeekOrigin.Begin);
				long num = 0L;
				bool saveExtraFieldsAndComments = this.Mode == ZipArchiveMode.Update;
				ZipCentralDirectoryFileHeader cd;
				while (ZipCentralDirectoryFileHeader.TryReadBlock(this._archiveReader, saveExtraFieldsAndComments, out cd))
				{
					this.AddEntry(new ZipArchiveEntry(this, cd));
					num += 1L;
				}
				if (num != this._expectedNumberOfEntries)
				{
					throw new InvalidDataException(Messages.NumEntriesWrong);
				}
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidDataException(Messages.CentralDirectoryInvalid, innerException);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CA4 File Offset: 0x00000EA4
		private void ReadEndOfCentralDirectory()
		{
			try
			{
				this._archiveStream.Seek(-18L, SeekOrigin.End);
				if (!ZipHelper.SeekBackwardsToSignature(this._archiveStream, 101010256U))
				{
					throw new InvalidDataException(Messages.EOCDNotFound);
				}
				long position = this._archiveStream.Position;
				ZipEndOfCentralDirectoryBlock zipEndOfCentralDirectoryBlock;
				bool flag = ZipEndOfCentralDirectoryBlock.TryReadBlock(this._archiveReader, out zipEndOfCentralDirectoryBlock);
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk != zipEndOfCentralDirectoryBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
				{
					throw new InvalidDataException(Messages.SplitSpanned);
				}
				this._numberOfThisDisk = (uint)zipEndOfCentralDirectoryBlock.NumberOfThisDisk;
				this._centralDirectoryStart = (long)((ulong)zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber);
				if (zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory != zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
				{
					throw new InvalidDataException(Messages.SplitSpanned);
				}
				this._expectedNumberOfEntries = (long)((ulong)zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory);
				if (this._mode == ZipArchiveMode.Update)
				{
					this._archiveComment = zipEndOfCentralDirectoryBlock.ArchiveComment;
				}
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk == 65535 || zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == 4294967295U || zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory == 65535)
				{
					this._archiveStream.Seek(position - 16L, SeekOrigin.Begin);
					if (ZipHelper.SeekBackwardsToSignature(this._archiveStream, 117853008U))
					{
						Zip64EndOfCentralDirectoryLocator zip64EndOfCentralDirectoryLocator;
						bool flag2 = Zip64EndOfCentralDirectoryLocator.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryLocator);
						if (zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD > 9223372036854775807UL)
						{
							throw new InvalidDataException(Messages.FieldTooBigOffsetToZip64EOCD);
						}
						long offsetOfZip64EOCD = (long)zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD;
						this._archiveStream.Seek(offsetOfZip64EOCD, SeekOrigin.Begin);
						Zip64EndOfCentralDirectoryRecord zip64EndOfCentralDirectoryRecord;
						if (!Zip64EndOfCentralDirectoryRecord.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryRecord))
						{
							throw new InvalidDataException(Messages.Zip64EOCDNotWhereExpected);
						}
						this._numberOfThisDisk = zip64EndOfCentralDirectoryRecord.NumberOfThisDisk;
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal > 9223372036854775807UL)
						{
							throw new InvalidDataException(Messages.FieldTooBigNumEntries);
						}
						if (zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory > 9223372036854775807UL)
						{
							throw new InvalidDataException(Messages.FieldTooBigOffsetToCD);
						}
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal != zip64EndOfCentralDirectoryRecord.NumberOfEntriesOnThisDisk)
						{
							throw new InvalidDataException(Messages.SplitSpanned);
						}
						this._expectedNumberOfEntries = (long)zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal;
						this._centralDirectoryStart = (long)zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory;
					}
				}
				if (this._centralDirectoryStart > this._archiveStream.Length)
				{
					throw new InvalidDataException(Messages.FieldTooBigOffsetToCD);
				}
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidDataException(Messages.CDCorrupt, innerException);
			}
			catch (IOException innerException2)
			{
				throw new InvalidDataException(Messages.CDCorrupt, innerException2);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002EFC File Offset: 0x000010FC
		private void WriteFile()
		{
			if (this._mode == ZipArchiveMode.Update)
			{
				List<ZipArchiveEntry> list = new List<ZipArchiveEntry>();
				foreach (ZipArchiveEntry zipArchiveEntry in this._entries)
				{
					if (!zipArchiveEntry.LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded())
					{
						list.Add(zipArchiveEntry);
					}
				}
				foreach (ZipArchiveEntry zipArchiveEntry2 in list)
				{
					zipArchiveEntry2.Delete();
				}
				this._archiveStream.Seek(0L, SeekOrigin.Begin);
				this._archiveStream.SetLength(0L);
			}
			foreach (ZipArchiveEntry zipArchiveEntry3 in this._entries)
			{
				zipArchiveEntry3.WriteAndFinishLocalEntry();
			}
			long position = this._archiveStream.Position;
			foreach (ZipArchiveEntry zipArchiveEntry4 in this._entries)
			{
				zipArchiveEntry4.WriteCentralDirectoryFileHeader();
			}
			long sizeOfCentralDirectory = this._archiveStream.Position - position;
			this.WriteArchiveEpilogue(position, sizeOfCentralDirectory);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003074 File Offset: 0x00001274
		private void WriteArchiveEpilogue(long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			bool flag = false;
			if (startOfCentralDirectory >= (long)((ulong)-1) || sizeOfCentralDirectory >= (long)((ulong)-1) || this._entries.Count >= 65535)
			{
				flag = true;
			}
			if (flag)
			{
				long position = this._archiveStream.Position;
				Zip64EndOfCentralDirectoryRecord.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory);
				Zip64EndOfCentralDirectoryLocator.WriteBlock(this._archiveStream, position);
			}
			ZipEndOfCentralDirectoryBlock.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory, this._archiveComment);
		}

		// Token: 0x04000019 RID: 25
		private Stream _archiveStream;

		// Token: 0x0400001A RID: 26
		private ZipArchiveEntry _archiveStreamOwner;

		// Token: 0x0400001B RID: 27
		private BinaryReader _archiveReader;

		// Token: 0x0400001C RID: 28
		private ZipArchiveMode _mode;

		// Token: 0x0400001D RID: 29
		private List<ZipArchiveEntry> _entries;

		// Token: 0x0400001E RID: 30
		private ReadOnlyCollection<ZipArchiveEntry> _entriesCollection;

		// Token: 0x0400001F RID: 31
		private Dictionary<string, ZipArchiveEntry> _entriesDictionary;

		// Token: 0x04000020 RID: 32
		private bool _readEntries;

		// Token: 0x04000021 RID: 33
		private bool _leaveOpen;

		// Token: 0x04000022 RID: 34
		private long _centralDirectoryStart;

		// Token: 0x04000023 RID: 35
		private bool _isDisposed;

		// Token: 0x04000024 RID: 36
		private uint _numberOfThisDisk;

		// Token: 0x04000025 RID: 37
		private long _expectedNumberOfEntries;

		// Token: 0x04000026 RID: 38
		private Stream _backingStream;

		// Token: 0x04000027 RID: 39
		private byte[] _archiveComment;

		// Token: 0x04000028 RID: 40
		private Encoding _entryNameEncoding;
	}
}
