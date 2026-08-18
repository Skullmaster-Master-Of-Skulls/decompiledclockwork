using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Internal;
using System.Reflection.Metadata;
using System.Threading;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000029 RID: 41
	public sealed class PEReader : IDisposable
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00006A9F File Offset: 0x00004C9F
		public unsafe PEReader(byte* peImage, int size)
		{
			if (peImage == null)
			{
				throw new ArgumentNullException("peImage");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this._peImage = new ExternalMemoryBlockProvider(peImage, size);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006AD3 File Offset: 0x00004CD3
		public PEReader(Stream peStream) : this(peStream, PEStreamOptions.Default)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public PEReader(Stream peStream, PEStreamOptions options) : this(peStream, options, null)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006AFE File Offset: 0x00004CFE
		public PEReader(Stream peStream, PEStreamOptions options, int size) : this(peStream, options, new int?(size))
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006B10 File Offset: 0x00004D10
		private PEReader(Stream peStream, PEStreamOptions options, int? sizeOpt)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanRead || !peStream.CanSeek)
			{
				throw new ArgumentException(SR.StreamMustSupportReadAndSeek, "peStream");
			}
			if (!options.IsValid())
			{
				throw new ArgumentOutOfRangeException("options");
			}
			long position = peStream.Position;
			int andValidateSize = PEBinaryReader.GetAndValidateSize(peStream, sizeOpt);
			bool flag = true;
			try
			{
				bool isFileStream = FileStreamReadLightUp.IsFileStream(peStream);
				if ((options & (PEStreamOptions.PrefetchMetadata | PEStreamOptions.PrefetchEntireImage)) == PEStreamOptions.Default)
				{
					this._peImage = new StreamMemoryBlockProvider(peStream, position, andValidateSize, isFileStream, (options & PEStreamOptions.LeaveOpen) > PEStreamOptions.Default);
					flag = false;
				}
				else if ((options & PEStreamOptions.PrefetchEntireImage) != PEStreamOptions.Default)
				{
					NativeHeapMemoryBlock nativeHeapMemoryBlock = StreamMemoryBlockProvider.ReadMemoryBlockNoLock(peStream, isFileStream, 0L, (int)Math.Min(peStream.Length, 2147483647L));
					this._lazyImageBlock = nativeHeapMemoryBlock;
					this._peImage = new ExternalMemoryBlockProvider(nativeHeapMemoryBlock.Pointer, nativeHeapMemoryBlock.Size);
					if ((options & PEStreamOptions.PrefetchMetadata) != PEStreamOptions.Default)
					{
						this.InitializePEHeaders();
					}
				}
				else
				{
					this._lazyPEHeaders = new PEHeaders(peStream);
					this._lazyMetadataBlock = StreamMemoryBlockProvider.ReadMemoryBlockNoLock(peStream, isFileStream, (long)this._lazyPEHeaders.MetadataStartOffset, this._lazyPEHeaders.MetadataSize);
				}
			}
			finally
			{
				if (flag && (options & PEStreamOptions.LeaveOpen) == PEStreamOptions.Default)
				{
					peStream.Dispose();
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006C40 File Offset: 0x00004E40
		public PEReader(ImmutableArray<byte> peImage)
		{
			if (peImage.IsDefault)
			{
				throw new ArgumentNullException("peImage");
			}
			this._peImage = new ByteArrayMemoryProvider(peImage);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006C68 File Offset: 0x00004E68
		public void Dispose()
		{
			MemoryBlockProvider peImage = this._peImage;
			if (peImage != null)
			{
				peImage.Dispose();
				this._peImage = null;
			}
			AbstractMemoryBlock lazyImageBlock = this._lazyImageBlock;
			if (lazyImageBlock != null)
			{
				lazyImageBlock.Dispose();
				this._lazyImageBlock = null;
			}
			AbstractMemoryBlock lazyMetadataBlock = this._lazyMetadataBlock;
			if (lazyMetadataBlock != null)
			{
				lazyMetadataBlock.Dispose();
				this._lazyMetadataBlock = null;
			}
			AbstractMemoryBlock[] lazyPESectionBlocks = this._lazyPESectionBlocks;
			if (lazyPESectionBlocks != null)
			{
				foreach (AbstractMemoryBlock abstractMemoryBlock in lazyPESectionBlocks)
				{
					if (abstractMemoryBlock != null)
					{
						abstractMemoryBlock.Dispose();
					}
				}
				this._lazyPESectionBlocks = null;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00006CF3 File Offset: 0x00004EF3
		public PEHeaders PEHeaders
		{
			get
			{
				if (this._lazyPEHeaders == null)
				{
					this.InitializePEHeaders();
				}
				return this._lazyPEHeaders;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006D0C File Offset: 0x00004F0C
		private void InitializePEHeaders()
		{
			StreamConstraints streamConstraints;
			Stream stream = this._peImage.GetStream(out streamConstraints);
			PEHeaders value;
			if (streamConstraints.GuardOpt != null)
			{
				object guardOpt = streamConstraints.GuardOpt;
				lock (guardOpt)
				{
					value = PEReader.ReadPEHeadersNoLock(stream, streamConstraints.ImageStart, streamConstraints.ImageSize);
					goto IL_5B;
				}
			}
			value = PEReader.ReadPEHeadersNoLock(stream, streamConstraints.ImageStart, streamConstraints.ImageSize);
			IL_5B:
			Interlocked.CompareExchange<PEHeaders>(ref this._lazyPEHeaders, value, null);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006D94 File Offset: 0x00004F94
		private static PEHeaders ReadPEHeadersNoLock(Stream stream, long imageStartPosition, int imageSize)
		{
			stream.Seek(imageStartPosition, SeekOrigin.Begin);
			return new PEHeaders(stream, imageSize);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006DA8 File Offset: 0x00004FA8
		private AbstractMemoryBlock GetEntireImageBlock()
		{
			if (this._lazyImageBlock == null)
			{
				if (this._peImage == null)
				{
					throw new InvalidOperationException(SR.PEImageNotAvailable);
				}
				AbstractMemoryBlock memoryBlock = this._peImage.GetMemoryBlock();
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyImageBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyImageBlock;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006DF8 File Offset: 0x00004FF8
		private AbstractMemoryBlock GetMetadataBlock()
		{
			if (!this.HasMetadata)
			{
				throw new InvalidOperationException(SR.PEImageDoesNotHaveMetadata);
			}
			if (this._lazyMetadataBlock == null)
			{
				AbstractMemoryBlock memoryBlock = this._peImage.GetMemoryBlock(this.PEHeaders.MetadataStartOffset, this.PEHeaders.MetadataSize);
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyMetadataBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyMetadataBlock;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00006E60 File Offset: 0x00005060
		private AbstractMemoryBlock GetPESectionBlock(int index)
		{
			if (this._lazyPESectionBlocks == null)
			{
				Interlocked.CompareExchange<AbstractMemoryBlock[]>(ref this._lazyPESectionBlocks, new AbstractMemoryBlock[this.PEHeaders.SectionHeaders.Length], null);
			}
			AbstractMemoryBlock memoryBlock = this._peImage.GetMemoryBlock(this.PEHeaders.SectionHeaders[index].PointerToRawData, this.PEHeaders.SectionHeaders[index].SizeOfRawData);
			if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyPESectionBlocks[index], memoryBlock, null) != null)
			{
				memoryBlock.Dispose();
			}
			return this._lazyPESectionBlocks[index];
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00006F01 File Offset: 0x00005101
		public bool IsEntireImageAvailable
		{
			get
			{
				return this._lazyImageBlock != null || this._peImage != null;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00006F16 File Offset: 0x00005116
		public PEMemoryBlock GetEntireImage()
		{
			return new PEMemoryBlock(this.GetEntireImageBlock(), 0);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00006F24 File Offset: 0x00005124
		public bool HasMetadata
		{
			get
			{
				return this.PEHeaders.MetadataSize > 0;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00006F34 File Offset: 0x00005134
		public PEMemoryBlock GetMetadata()
		{
			return new PEMemoryBlock(this.GetMetadataBlock(), 0);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00006F44 File Offset: 0x00005144
		public PEMemoryBlock GetSectionData(int relativeVirtualAddress)
		{
			int containingSectionIndex = this.PEHeaders.GetContainingSectionIndex(relativeVirtualAddress);
			if (containingSectionIndex < 0)
			{
				return default(PEMemoryBlock);
			}
			int num = relativeVirtualAddress - this.PEHeaders.SectionHeaders[containingSectionIndex].VirtualAddress;
			int virtualSize = this.PEHeaders.SectionHeaders[containingSectionIndex].VirtualSize;
			AbstractMemoryBlock block;
			if (this._peImage != null)
			{
				block = this.GetPESectionBlock(containingSectionIndex);
			}
			else
			{
				block = this.GetEntireImageBlock();
				num += this.PEHeaders.SectionHeaders[containingSectionIndex].PointerToRawData;
			}
			return new PEMemoryBlock(block, num);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00006FF0 File Offset: 0x000051F0
		public ImmutableArray<DebugDirectoryEntry> ReadDebugDirectory()
		{
			DirectoryEntry debugTableDirectory = this.PEHeaders.PEHeader.DebugTableDirectory;
			if (debugTableDirectory.Size == 0)
			{
				return ImmutableArray<DebugDirectoryEntry>.Empty;
			}
			int start;
			if (!this.PEHeaders.TryGetDirectoryOffset(debugTableDirectory, out start))
			{
				throw new BadImageFormatException(SR.InvalidDirectoryRVA);
			}
			if (debugTableDirectory.Size % 28 != 0)
			{
				throw new BadImageFormatException(SR.InvalidDirectorySize);
			}
			ImmutableArray<DebugDirectoryEntry> result;
			using (AbstractMemoryBlock memoryBlock = this._peImage.GetMemoryBlock(start, debugTableDirectory.Size))
			{
				BlobReader blobReader = new BlobReader(memoryBlock.Pointer, memoryBlock.Size);
				int num = debugTableDirectory.Size / 28;
				ImmutableArray<DebugDirectoryEntry>.Builder builder = ImmutableArray.CreateBuilder<DebugDirectoryEntry>(num);
				for (int i = 0; i < num; i++)
				{
					if (blobReader.ReadInt32() != 0)
					{
						throw new BadImageFormatException(SR.InvalidDebugDirectoryEntryCharacteristics);
					}
					uint stamp = blobReader.ReadUInt32();
					ushort majorVersion = blobReader.ReadUInt16();
					ushort minorVersion = blobReader.ReadUInt16();
					DebugDirectoryEntryType type = (DebugDirectoryEntryType)blobReader.ReadInt32();
					int dataSize = blobReader.ReadInt32();
					int dataRelativeVirtualAddress = blobReader.ReadInt32();
					int dataPointer = blobReader.ReadInt32();
					builder.Add(new DebugDirectoryEntry(stamp, majorVersion, minorVersion, type, dataSize, dataRelativeVirtualAddress, dataPointer));
				}
				result = builder.MoveToImmutable();
			}
			return result;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000712C File Offset: 0x0000532C
		public CodeViewDebugDirectoryData ReadCodeViewDebugDirectoryData(DebugDirectoryEntry entry)
		{
			if (entry.Type != DebugDirectoryEntryType.CodeView)
			{
				throw new ArgumentException(SR.NotCodeViewEntry, "entry");
			}
			CodeViewDebugDirectoryData result;
			using (AbstractMemoryBlock memoryBlock = this._peImage.GetMemoryBlock(entry.DataPointer, entry.DataSize))
			{
				BlobReader blobReader = new BlobReader(memoryBlock.Pointer, memoryBlock.Size);
				if (blobReader.ReadByte() != 82 || blobReader.ReadByte() != 83 || blobReader.ReadByte() != 68 || blobReader.ReadByte() != 83)
				{
					throw new BadImageFormatException(SR.UnexpectedCodeViewDataSignature);
				}
				Guid guid = blobReader.ReadGuid();
				int age = blobReader.ReadInt32();
				string path = blobReader.ReadUtf8NullTerminated();
				while (blobReader.RemainingBytes > 0)
				{
					if (blobReader.ReadByte() != 0)
					{
						throw new BadImageFormatException(SR.InvalidPathPadding);
					}
				}
				result = new CodeViewDebugDirectoryData(guid, age, path);
			}
			return result;
		}

		// Token: 0x04000174 RID: 372
		private MemoryBlockProvider _peImage;

		// Token: 0x04000175 RID: 373
		private PEHeaders _lazyPEHeaders;

		// Token: 0x04000176 RID: 374
		private AbstractMemoryBlock _lazyMetadataBlock;

		// Token: 0x04000177 RID: 375
		private AbstractMemoryBlock _lazyImageBlock;

		// Token: 0x04000178 RID: 376
		private AbstractMemoryBlock[] _lazyPESectionBlocks;
	}
}
