using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO.Compression
{
	// Token: 0x02000007 RID: 7
	[__DynamicallyInvokable]
	public class ZipArchiveEntry
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000030F4 File Offset: 0x000012F4
		internal ZipArchiveEntry(ZipArchive archive, ZipCentralDirectoryFileHeader cd, CompressionLevel compressionLevel) : this(archive, cd)
		{
			this._compressionLevel = new CompressionLevel?(compressionLevel);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000310C File Offset: 0x0000130C
		internal ZipArchiveEntry(ZipArchive archive, ZipCentralDirectoryFileHeader cd)
		{
			this._archive = archive;
			this._originallyInArchive = true;
			this._diskNumberStart = cd.DiskNumberStart;
			this._versionToExtract = (ZipVersionNeededValues)cd.VersionNeededToExtract;
			this._generalPurposeBitFlag = (ZipArchiveEntry.BitFlagValues)cd.GeneralPurposeBitFlag;
			this.CompressionMethod = (ZipArchiveEntry.CompressionMethodValues)cd.CompressionMethod;
			this._lastModified = new DateTimeOffset(ZipHelper.DosTimeToDateTime(cd.LastModified));
			this._compressedSize = cd.CompressedSize;
			this._uncompressedSize = cd.UncompressedSize;
			this._externalFileAttr = cd.ExternalFileAttributes;
			this._offsetOfLocalHeader = cd.RelativeOffsetOfLocalHeader;
			this._storedOffsetOfCompressedData = null;
			this._crc32 = cd.Crc32;
			this._compressedBytes = null;
			this._storedUncompressedData = null;
			this._currentlyOpenForWrite = false;
			this._everOpenedForWrite = false;
			this._outstandingWriteStream = null;
			this.FullName = this.DecodeEntryName(cd.Filename);
			this._lhUnknownExtraFields = null;
			this._cdUnknownExtraFields = cd.ExtraFields;
			this._fileComment = cd.FileComment;
			this._compressionLevel = null;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000321B File Offset: 0x0000141B
		internal ZipArchiveEntry(ZipArchive archive, string entryName, CompressionLevel compressionLevel) : this(archive, entryName)
		{
			this._compressionLevel = new CompressionLevel?(compressionLevel);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003234 File Offset: 0x00001434
		internal ZipArchiveEntry(ZipArchive archive, string entryName)
		{
			this._archive = archive;
			this._originallyInArchive = false;
			this._diskNumberStart = 0;
			this._versionToExtract = ZipVersionNeededValues.Default;
			this._generalPurposeBitFlag = (ZipArchiveEntry.BitFlagValues)0;
			this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Deflate;
			this._lastModified = DateTimeOffset.Now;
			this._compressedSize = 0L;
			this._uncompressedSize = 0L;
			this._externalFileAttr = 0U;
			this._offsetOfLocalHeader = 0L;
			this._storedOffsetOfCompressedData = null;
			this._crc32 = 0U;
			this._compressedBytes = null;
			this._storedUncompressedData = null;
			this._currentlyOpenForWrite = false;
			this._everOpenedForWrite = false;
			this._outstandingWriteStream = null;
			this.FullName = entryName;
			this._cdUnknownExtraFields = null;
			this._lhUnknownExtraFields = null;
			this._fileComment = null;
			this._compressionLevel = null;
			if (this._storedEntryNameBytes.Length > 65535)
			{
				throw new ArgumentException(Messages.EntryNamesTooLong);
			}
			if (this._archive.Mode == ZipArchiveMode.Create)
			{
				this._archive.AcquireArchiveStream(this);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000332E File Offset: 0x0000152E
		[__DynamicallyInvokable]
		public ZipArchive Archive
		{
			[__DynamicallyInvokable]
			get
			{
				return this._archive;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003336 File Offset: 0x00001536
		[__DynamicallyInvokable]
		public long CompressedLength
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._everOpenedForWrite)
				{
					throw new InvalidOperationException(Messages.LengthAfterWrite);
				}
				return this._compressedSize;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003351 File Offset: 0x00001551
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003359 File Offset: 0x00001559
		public int ExternalAttributes
		{
			get
			{
				return (int)this._externalFileAttr;
			}
			set
			{
				this.ThrowIfInvalidArchive();
				this._externalFileAttr = (uint)value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003368 File Offset: 0x00001568
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003370 File Offset: 0x00001570
		[__DynamicallyInvokable]
		public string FullName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._storedEntryName;
			}
			private set
			{
				if (value == null)
				{
					throw new ArgumentNullException("FullName");
				}
				bool flag;
				this._storedEntryNameBytes = this.EncodeEntryName(value, out flag);
				this._storedEntryName = value;
				if (flag)
				{
					this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.UnicodeFileName;
				}
				else
				{
					this._generalPurposeBitFlag &= ~ZipArchiveEntry.BitFlagValues.UnicodeFileName;
				}
				if (ZipHelper.EndsWithDirChar(value))
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000033DA File Offset: 0x000015DA
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000033E4 File Offset: 0x000015E4
		[__DynamicallyInvokable]
		public DateTimeOffset LastWriteTime
		{
			[__DynamicallyInvokable]
			get
			{
				return this._lastModified;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ThrowIfInvalidArchive();
				if (this._archive.Mode == ZipArchiveMode.Read)
				{
					throw new NotSupportedException(Messages.ReadOnlyArchive);
				}
				if (this._archive.Mode == ZipArchiveMode.Create && this._everOpenedForWrite)
				{
					throw new IOException(Messages.FrozenAfterWrite);
				}
				if (value.DateTime.Year < 1980 || value.DateTime.Year > 2107)
				{
					throw new ArgumentOutOfRangeException("value", Messages.DateTimeOutOfRange);
				}
				this._lastModified = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003473 File Offset: 0x00001673
		[__DynamicallyInvokable]
		public long Length
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._everOpenedForWrite)
				{
					throw new InvalidOperationException(Messages.LengthAfterWrite);
				}
				return this._uncompressedSize;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000348E File Offset: 0x0000168E
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return Path.GetFileName(this.FullName);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000349C File Offset: 0x0000169C
		[__DynamicallyInvokable]
		public void Delete()
		{
			if (this._archive == null)
			{
				return;
			}
			if (this._currentlyOpenForWrite)
			{
				throw new IOException(Messages.DeleteOpenEntry);
			}
			if (this._archive.Mode != ZipArchiveMode.Update)
			{
				throw new NotSupportedException(Messages.DeleteOnlyInUpdate);
			}
			this._archive.ThrowIfDisposed();
			this._archive.RemoveEntry(this);
			this._archive = null;
			this.UnloadStreams();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003504 File Offset: 0x00001704
		[__DynamicallyInvokable]
		public Stream Open()
		{
			this.ThrowIfInvalidArchive();
			switch (this._archive.Mode)
			{
			case ZipArchiveMode.Read:
				return this.OpenInReadMode(true);
			case ZipArchiveMode.Create:
				return this.OpenInWriteMode();
			}
			return this.OpenInUpdateMode();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000354C File Offset: 0x0000174C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003554 File Offset: 0x00001754
		internal bool EverOpenedForWrite
		{
			get
			{
				return this._everOpenedForWrite;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000355C File Offset: 0x0000175C
		private long OffsetOfCompressedData
		{
			get
			{
				if (this._storedOffsetOfCompressedData == null)
				{
					this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
					if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
					{
						throw new InvalidDataException(Messages.LocalFileHeaderCorrupt);
					}
					this._storedOffsetOfCompressedData = new long?(this._archive.ArchiveStream.Position);
				}
				return this._storedOffsetOfCompressedData.Value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000035D4 File Offset: 0x000017D4
		private MemoryStream UncompressedData
		{
			get
			{
				if (this._storedUncompressedData == null)
				{
					this._storedUncompressedData = new MemoryStream((int)this._uncompressedSize);
					if (this._originallyInArchive)
					{
						using (Stream stream = this.OpenInReadMode(false))
						{
							try
							{
								stream.CopyTo(this._storedUncompressedData);
							}
							catch (InvalidDataException)
							{
								this._storedUncompressedData.Dispose();
								this._storedUncompressedData = null;
								this._currentlyOpenForWrite = false;
								this._everOpenedForWrite = false;
								throw;
							}
						}
					}
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Deflate;
				}
				return this._storedUncompressedData;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003670 File Offset: 0x00001870
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003678 File Offset: 0x00001878
		private ZipArchiveEntry.CompressionMethodValues CompressionMethod
		{
			get
			{
				return this._storedCompressionMethod;
			}
			set
			{
				if (value == ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
				this._storedCompressionMethod = value;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003690 File Offset: 0x00001890
		private string DecodeEntryName(byte[] entryNameBytes)
		{
			Encoding encoding;
			if ((this._generalPurposeBitFlag & ZipArchiveEntry.BitFlagValues.UnicodeFileName) == (ZipArchiveEntry.BitFlagValues)0)
			{
				encoding = ((this._archive == null) ? Encoding.GetEncoding(0) : (this._archive.EntryNameEncoding ?? Encoding.GetEncoding(0)));
			}
			else
			{
				encoding = Encoding.UTF8;
			}
			return new string(encoding.GetChars(entryNameBytes));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000036E8 File Offset: 0x000018E8
		private byte[] EncodeEntryName(string entryName, out bool isUTF8)
		{
			Encoding encoding;
			if (this._archive != null && this._archive.EntryNameEncoding != null)
			{
				encoding = this._archive.EntryNameEncoding;
			}
			else
			{
				encoding = (ZipHelper.RequiresUnicode(entryName) ? Encoding.UTF8 : Encoding.GetEncoding(0));
			}
			isUTF8 = (encoding is UTF8Encoding && encoding.Equals(Encoding.UTF8));
			return encoding.GetBytes(entryName);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000374D File Offset: 0x0000194D
		internal void WriteAndFinishLocalEntry()
		{
			this.CloseStreams();
			this.WriteLocalFileHeaderAndDataIfNeeded();
			this.UnloadStreams();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003764 File Offset: 0x00001964
		internal void WriteCentralDirectoryFileHeader()
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint value;
			uint value2;
			if (this.SizesTooLarge())
			{
				flag = true;
				value = uint.MaxValue;
				value2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = new long?(this._compressedSize);
				zip64ExtraField.UncompressedSize = new long?(this._uncompressedSize);
			}
			else
			{
				value = (uint)this._compressedSize;
				value2 = (uint)this._uncompressedSize;
			}
			uint value3;
			if (this._offsetOfLocalHeader > (long)((ulong)-1))
			{
				flag = true;
				value3 = uint.MaxValue;
				zip64ExtraField.LocalHeaderOffset = new long?(this._offsetOfLocalHeader);
			}
			else
			{
				value3 = (uint)this._offsetOfLocalHeader;
			}
			if (flag)
			{
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			int num = (int)(flag ? zip64ExtraField.TotalSize : 0) + ((this._cdUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(this._cdUnknownExtraFields) : 0);
			ushort value4;
			if (num > 65535)
			{
				value4 = (flag ? zip64ExtraField.TotalSize : 0);
				this._cdUnknownExtraFields = null;
			}
			else
			{
				value4 = (ushort)num;
			}
			binaryWriter.Write(33639248U);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write((ushort)this._storedEntryNameBytes.Length);
			binaryWriter.Write(value4);
			binaryWriter.Write((this._fileComment != null) ? ((ushort)this._fileComment.Length) : 0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(this._externalFileAttr);
			binaryWriter.Write(value3);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._cdUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._cdUnknownExtraFields, this._archive.ArchiveStream);
			}
			if (this._fileComment != null)
			{
				binaryWriter.Write(this._fileComment);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000396C File Offset: 0x00001B6C
		internal bool LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded()
		{
			if (this._originallyInArchive)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				this._lhUnknownExtraFields = ZipLocalFileHeader.GetExtraFields(this._archive.ArchiveReader);
			}
			if (!this._everOpenedForWrite && this._originallyInArchive)
			{
				this._compressedBytes = new byte[this._compressedSize];
				this._archive.ArchiveStream.Seek(this.OffsetOfCompressedData, SeekOrigin.Begin);
				ZipHelper.ReadBytes(this._archive.ArchiveStream, this._compressedBytes, (int)this._compressedSize);
			}
			return true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A08 File Offset: 0x00001C08
		internal void ThrowIfNotOpenable(bool needToUncompress, bool needToLoadIntoMemory)
		{
			string message;
			if (!this.IsOpenable(needToUncompress, needToLoadIntoMemory, out message))
			{
				throw new InvalidDataException(message);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A28 File Offset: 0x00001C28
		private CheckSumAndSizeWriteStream GetDataCompressor(Stream backingStream, bool leaveBackingStreamOpen, EventHandler onClose)
		{
			Stream baseStream = (this._compressionLevel != null) ? new DeflateStream(backingStream, this._compressionLevel.Value, leaveBackingStreamOpen) : new DeflateStream(backingStream, CompressionMode.Compress, leaveBackingStreamOpen);
			bool flag = true;
			bool leaveOpenOnClose = leaveBackingStreamOpen && !flag;
			return new CheckSumAndSizeWriteStream(baseStream, backingStream, leaveOpenOnClose, delegate(long initialPosition, long currentPosition, uint checkSum)
			{
				this._crc32 = checkSum;
				this._uncompressedSize = currentPosition;
				this._compressedSize = backingStream.Position - initialPosition;
				if (onClose != null)
				{
					onClose(this, EventArgs.Empty);
				}
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003AB0 File Offset: 0x00001CB0
		private Stream GetDataDecompressor(Stream compressedStreamToRead)
		{
			ZipArchiveEntry.CompressionMethodValues compressionMethod = this.CompressionMethod;
			Stream result;
			if (compressionMethod != ZipArchiveEntry.CompressionMethodValues.Stored && compressionMethod == ZipArchiveEntry.CompressionMethodValues.Deflate)
			{
				result = new DeflateStream(compressedStreamToRead, CompressionMode.Decompress);
			}
			else
			{
				result = compressedStreamToRead;
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003ADC File Offset: 0x00001CDC
		private Stream OpenInReadMode(bool checkOpenable)
		{
			if (checkOpenable)
			{
				this.ThrowIfNotOpenable(true, false);
			}
			Stream compressedStreamToRead = new SubReadStream(this._archive.ArchiveStream, this.OffsetOfCompressedData, this._compressedSize);
			return this.GetDataDecompressor(compressedStreamToRead);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B18 File Offset: 0x00001D18
		private Stream OpenInWriteMode()
		{
			if (this._everOpenedForWrite)
			{
				throw new IOException(Messages.CreateModeWriteOnceAndOneEntryAtATime);
			}
			this._everOpenedForWrite = true;
			CheckSumAndSizeWriteStream dataCompressor = this.GetDataCompressor(this._archive.ArchiveStream, true, delegate(object o, EventArgs e)
			{
				this._archive.ReleaseArchiveStream(this);
				this._outstandingWriteStream = null;
			});
			this._outstandingWriteStream = new ZipArchiveEntry.DirectToArchiveWriterStream(dataCompressor, this);
			return new WrappedStream(this._outstandingWriteStream, delegate(object o, EventArgs e)
			{
				this._outstandingWriteStream.Close();
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B84 File Offset: 0x00001D84
		private Stream OpenInUpdateMode()
		{
			if (this._currentlyOpenForWrite)
			{
				throw new IOException(Messages.UpdateModeOneStream);
			}
			this.ThrowIfNotOpenable(true, true);
			this._everOpenedForWrite = true;
			this._currentlyOpenForWrite = true;
			this.UncompressedData.Seek(0L, SeekOrigin.Begin);
			return new WrappedStream(this.UncompressedData, delegate(object o, EventArgs e)
			{
				this._currentlyOpenForWrite = false;
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private bool IsOpenable(bool needToUncompress, bool needToLoadIntoMemory, out string message)
		{
			message = null;
			if (this._originallyInArchive)
			{
				if (needToUncompress && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Stored && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					message = Messages.UnsupportedCompression;
					return false;
				}
				if ((long)this._diskNumberStart != (long)((ulong)this._archive.NumberOfThisDisk))
				{
					message = Messages.SplitSpanned;
					return false;
				}
				if (this._offsetOfLocalHeader > this._archive.ArchiveStream.Length)
				{
					message = Messages.LocalFileHeaderCorrupt;
					return false;
				}
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
				{
					message = Messages.LocalFileHeaderCorrupt;
					return false;
				}
				if (this.OffsetOfCompressedData + this._compressedSize > this._archive.ArchiveStream.Length)
				{
					message = Messages.LocalFileHeaderCorrupt;
					return false;
				}
				if (needToLoadIntoMemory && this._compressedSize > 2147483647L)
				{
					message = Messages.EntryTooLarge;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003CCD File Offset: 0x00001ECD
		private bool SizesTooLarge()
		{
			return this._compressedSize > (long)((ulong)-1) || this._uncompressedSize > (long)((ulong)-1);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003CE8 File Offset: 0x00001EE8
		private bool WriteLocalFileHeader(bool isEmptyFile)
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint value;
			uint value2;
			if (isEmptyFile)
			{
				this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				value = 0U;
				value2 = 0U;
			}
			else if (this._archive.Mode == ZipArchiveMode.Create && !this._archive.ArchiveStream.CanSeek && !isEmptyFile)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				flag = false;
				value = 0U;
				value2 = 0U;
			}
			else if (this.SizesTooLarge())
			{
				flag = true;
				value = uint.MaxValue;
				value2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = new long?(this._compressedSize);
				zip64ExtraField.UncompressedSize = new long?(this._uncompressedSize);
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			else
			{
				flag = false;
				value = (uint)this._compressedSize;
				value2 = (uint)this._uncompressedSize;
			}
			this._offsetOfLocalHeader = binaryWriter.BaseStream.Position;
			int num = (int)(flag ? zip64ExtraField.TotalSize : 0) + ((this._lhUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(this._lhUnknownExtraFields) : 0);
			ushort value3;
			if (num > 65535)
			{
				value3 = (flag ? zip64ExtraField.TotalSize : 0);
				this._lhUnknownExtraFields = null;
			}
			else
			{
				value3 = (ushort)num;
			}
			binaryWriter.Write(67324752U);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write((ushort)this._storedEntryNameBytes.Length);
			binaryWriter.Write(value3);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._lhUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._lhUnknownExtraFields, this._archive.ArchiveStream);
			}
			return flag;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003EC4 File Offset: 0x000020C4
		private void WriteLocalFileHeaderAndDataIfNeeded()
		{
			if (this._storedUncompressedData != null || this._compressedBytes != null)
			{
				if (this._storedUncompressedData != null)
				{
					this._uncompressedSize = this._storedUncompressedData.Length;
					using (Stream stream = new ZipArchiveEntry.DirectToArchiveWriterStream(this.GetDataCompressor(this._archive.ArchiveStream, true, null), this))
					{
						this._storedUncompressedData.Seek(0L, SeekOrigin.Begin);
						this._storedUncompressedData.CopyTo(stream);
						this._storedUncompressedData.Close();
						this._storedUncompressedData = null;
						return;
					}
				}
				if (this._uncompressedSize == 0L)
				{
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				}
				this.WriteLocalFileHeader(false);
				using (MemoryStream memoryStream = new MemoryStream(this._compressedBytes))
				{
					memoryStream.CopyTo(this._archive.ArchiveStream);
					return;
				}
			}
			if (this._archive.Mode == ZipArchiveMode.Update || !this._everOpenedForWrite)
			{
				this._everOpenedForWrite = true;
				this.WriteLocalFileHeader(true);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FD4 File Offset: 0x000021D4
		private void WriteCrcAndSizesInLocalHeader(bool zip64HeaderUsed)
		{
			long position = this._archive.ArchiveStream.Position;
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			bool flag = this.SizesTooLarge();
			bool flag2 = flag && !zip64HeaderUsed;
			uint value = flag ? uint.MaxValue : ((uint)this._compressedSize);
			uint value2 = flag ? uint.MaxValue : ((uint)this._uncompressedSize);
			if (flag2)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 6L, SeekOrigin.Begin);
				binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			}
			this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 14L, SeekOrigin.Begin);
			if (!flag2)
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(value);
				binaryWriter.Write(value2);
			}
			else
			{
				binaryWriter.Write(0U);
				binaryWriter.Write(0U);
				binaryWriter.Write(0U);
			}
			if (zip64HeaderUsed)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 30L + (long)this._storedEntryNameBytes.Length + 4L, SeekOrigin.Begin);
				binaryWriter.Write(this._uncompressedSize);
				binaryWriter.Write(this._compressedSize);
				this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			}
			this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			if (flag2)
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004150 File Offset: 0x00002350
		private void WriteDataDescriptor()
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			binaryWriter.Write(134695760U);
			binaryWriter.Write(this._crc32);
			if (this.SizesTooLarge())
			{
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
				return;
			}
			binaryWriter.Write((uint)this._compressedSize);
			binaryWriter.Write((uint)this._uncompressedSize);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000041C0 File Offset: 0x000023C0
		private void UnloadStreams()
		{
			if (this._storedUncompressedData != null)
			{
				this._storedUncompressedData.Close();
			}
			this._compressedBytes = null;
			this._outstandingWriteStream = null;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000041E3 File Offset: 0x000023E3
		private void CloseStreams()
		{
			if (this._outstandingWriteStream != null)
			{
				this._outstandingWriteStream.Close();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000041F8 File Offset: 0x000023F8
		private void VersionToExtractAtLeast(ZipVersionNeededValues value)
		{
			if (this._versionToExtract < value)
			{
				this._versionToExtract = value;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000420A File Offset: 0x0000240A
		private void ThrowIfInvalidArchive()
		{
			if (this._archive == null)
			{
				throw new InvalidOperationException(Messages.DeletedEntry);
			}
			this._archive.ThrowIfDisposed();
		}

		// Token: 0x04000029 RID: 41
		private const ushort DefaultVersionToExtract = 10;

		// Token: 0x0400002A RID: 42
		private ZipArchive _archive;

		// Token: 0x0400002B RID: 43
		private readonly bool _originallyInArchive;

		// Token: 0x0400002C RID: 44
		private readonly int _diskNumberStart;

		// Token: 0x0400002D RID: 45
		private ZipVersionNeededValues _versionToExtract;

		// Token: 0x0400002E RID: 46
		private ZipArchiveEntry.BitFlagValues _generalPurposeBitFlag;

		// Token: 0x0400002F RID: 47
		private ZipArchiveEntry.CompressionMethodValues _storedCompressionMethod;

		// Token: 0x04000030 RID: 48
		private DateTimeOffset _lastModified;

		// Token: 0x04000031 RID: 49
		private long _compressedSize;

		// Token: 0x04000032 RID: 50
		private long _uncompressedSize;

		// Token: 0x04000033 RID: 51
		private long _offsetOfLocalHeader;

		// Token: 0x04000034 RID: 52
		private long? _storedOffsetOfCompressedData;

		// Token: 0x04000035 RID: 53
		private uint _crc32;

		// Token: 0x04000036 RID: 54
		private byte[] _compressedBytes;

		// Token: 0x04000037 RID: 55
		private MemoryStream _storedUncompressedData;

		// Token: 0x04000038 RID: 56
		private bool _currentlyOpenForWrite;

		// Token: 0x04000039 RID: 57
		private bool _everOpenedForWrite;

		// Token: 0x0400003A RID: 58
		private Stream _outstandingWriteStream;

		// Token: 0x0400003B RID: 59
		private uint _externalFileAttr;

		// Token: 0x0400003C RID: 60
		private string _storedEntryName;

		// Token: 0x0400003D RID: 61
		private byte[] _storedEntryNameBytes;

		// Token: 0x0400003E RID: 62
		private List<ZipGenericExtraField> _cdUnknownExtraFields;

		// Token: 0x0400003F RID: 63
		private List<ZipGenericExtraField> _lhUnknownExtraFields;

		// Token: 0x04000040 RID: 64
		private byte[] _fileComment;

		// Token: 0x04000041 RID: 65
		private CompressionLevel? _compressionLevel;

		// Token: 0x02000014 RID: 20
		private class DirectToArchiveWriterStream : Stream
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x00005586 File Offset: 0x00003786
			public DirectToArchiveWriterStream(CheckSumAndSizeWriteStream crcSizeStream, ZipArchiveEntry entry)
			{
				this._position = 0L;
				this._crcSizeStream = crcSizeStream;
				this._everWritten = false;
				this._isDisposed = false;
				this._entry = entry;
				this._usedZip64inLH = false;
				this._canWrite = true;
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x000055C0 File Offset: 0x000037C0
			public override long Length
			{
				get
				{
					this.ThrowIfDisposed();
					throw new NotSupportedException(Messages.SeekingNotSupported);
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x000055D2 File Offset: 0x000037D2
			// (set) Token: 0x060000D9 RID: 217 RVA: 0x000055E0 File Offset: 0x000037E0
			public override long Position
			{
				get
				{
					this.ThrowIfDisposed();
					return this._position;
				}
				set
				{
					this.ThrowIfDisposed();
					throw new NotSupportedException(Messages.SeekingNotSupported);
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060000DA RID: 218 RVA: 0x000055F2 File Offset: 0x000037F2
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060000DB RID: 219 RVA: 0x000055F5 File Offset: 0x000037F5
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060000DC RID: 220 RVA: 0x000055F8 File Offset: 0x000037F8
			public override bool CanWrite
			{
				get
				{
					return this._canWrite;
				}
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00005600 File Offset: 0x00003800
			private void ThrowIfDisposed()
			{
				if (this._isDisposed)
				{
					throw new ObjectDisposedException(base.GetType().Name, Messages.HiddenStreamName);
				}
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00005620 File Offset: 0x00003820
			public override int Read(byte[] buffer, int offset, int count)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.ReadingNotSupported);
			}

			// Token: 0x060000DF RID: 223 RVA: 0x00005632 File Offset: 0x00003832
			public override long Seek(long offset, SeekOrigin origin)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00005644 File Offset: 0x00003844
			public override void SetLength(long value)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.SetLengthRequiresSeekingAndWriting);
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x00005658 File Offset: 0x00003858
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0)
				{
					throw new ArgumentOutOfRangeException("offset", Messages.ArgumentNeedNonNegative);
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", Messages.ArgumentNeedNonNegative);
				}
				if (buffer.Length - offset < count)
				{
					throw new ArgumentException(Messages.OffsetLengthInvalid);
				}
				this.ThrowIfDisposed();
				if (count == 0)
				{
					return;
				}
				if (!this._everWritten)
				{
					this._everWritten = true;
					this._usedZip64inLH = this._entry.WriteLocalFileHeader(false);
				}
				this._crcSizeStream.Write(buffer, offset, count);
				this._position += (long)count;
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x000056F6 File Offset: 0x000038F6
			public override void Flush()
			{
				this.ThrowIfDisposed();
				this._crcSizeStream.Flush();
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x0000570C File Offset: 0x0000390C
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this._isDisposed)
				{
					this._crcSizeStream.Close();
					if (!this._everWritten)
					{
						this._entry.WriteLocalFileHeader(true);
					}
					else if (this._entry._archive.ArchiveStream.CanSeek)
					{
						this._entry.WriteCrcAndSizesInLocalHeader(this._usedZip64inLH);
					}
					else
					{
						this._entry.WriteDataDescriptor();
					}
					this._canWrite = false;
					this._isDisposed = true;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000091 RID: 145
			private long _position;

			// Token: 0x04000092 RID: 146
			private CheckSumAndSizeWriteStream _crcSizeStream;

			// Token: 0x04000093 RID: 147
			private bool _everWritten;

			// Token: 0x04000094 RID: 148
			private bool _isDisposed;

			// Token: 0x04000095 RID: 149
			private ZipArchiveEntry _entry;

			// Token: 0x04000096 RID: 150
			private bool _usedZip64inLH;

			// Token: 0x04000097 RID: 151
			private bool _canWrite;
		}

		// Token: 0x02000015 RID: 21
		[Flags]
		private enum BitFlagValues : ushort
		{
			// Token: 0x04000099 RID: 153
			DataDescriptor = 8,
			// Token: 0x0400009A RID: 154
			UnicodeFileName = 2048
		}

		// Token: 0x02000016 RID: 22
		private enum CompressionMethodValues : ushort
		{
			// Token: 0x0400009C RID: 156
			Stored,
			// Token: 0x0400009D RID: 157
			Deflate = 8
		}

		// Token: 0x02000017 RID: 23
		private enum OpenableValues
		{
			// Token: 0x0400009F RID: 159
			Openable,
			// Token: 0x040000A0 RID: 160
			FileNonExistent,
			// Token: 0x040000A1 RID: 161
			FileTooLarge
		}
	}
}
