using System;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using System.Reflection.Internal;
using System.Reflection.Metadata;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200004D RID: 77
	internal sealed class PEReader : IDisposable
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00004F07 File Offset: 0x00003107
		public bool IsLoadedImage { get; }

		// Token: 0x06000201 RID: 513 RVA: 0x00004F0F File Offset: 0x0000310F
		[SecurityCritical]
		public unsafe PEReader(byte* peImage, int size) : this(peImage, size, false)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00004F1A File Offset: 0x0000311A
		[SecurityCritical]
		public unsafe PEReader(byte* peImage, int size, bool isLoadedImage)
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
			this.IsLoadedImage = isLoadedImage;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00004F55 File Offset: 0x00003155
		public PEReader(Stream peStream) : this(peStream, PEStreamOptions.Default)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00004F5F File Offset: 0x0000315F
		public PEReader(Stream peStream, PEStreamOptions options) : this(peStream, options, 0)
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00004F6C File Offset: 0x0000316C
		[SecuritySafeCritical]
		public PEReader(Stream peStream, PEStreamOptions options, int size)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanRead || !peStream.CanSeek)
			{
				throw new ArgumentException("StreamMustSupportReadAndSeek", "peStream");
			}
			if (!options.IsValid())
			{
				throw new ArgumentOutOfRangeException("options");
			}
			this.IsLoadedImage = ((options & PEStreamOptions.IsLoadedImage) > PEStreamOptions.Default);
			long position = peStream.Position;
			int andValidateSize = StreamExtensions.GetAndValidateSize(peStream, size, "peStream");
			bool flag = true;
			try
			{
				bool isFileStream = peStream is FileStream;
				if ((options & (PEStreamOptions.PrefetchMetadata | PEStreamOptions.PrefetchEntireImage)) == PEStreamOptions.Default)
				{
					this._peImage = new StreamMemoryBlockProvider(peStream, position, andValidateSize, isFileStream, (options & PEStreamOptions.LeaveOpen) > PEStreamOptions.Default);
					flag = false;
				}
				else if ((options & PEStreamOptions.PrefetchEntireImage) != PEStreamOptions.Default)
				{
					NativeHeapMemoryBlock nativeHeapMemoryBlock = StreamMemoryBlockProvider.ReadMemoryBlockNoLock(peStream, isFileStream, position, andValidateSize);
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

		// Token: 0x06000206 RID: 518 RVA: 0x0000509C File Offset: 0x0000329C
		public PEReader(ImmutableArray<byte> peImage)
		{
			if (peImage.IsDefault)
			{
				throw new ArgumentNullException("peImage");
			}
			this._peImage = new ByteArrayMemoryProvider(peImage);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000050C4 File Offset: 0x000032C4
		public void Dispose()
		{
			this._lazyPEHeaders = null;
			MemoryBlockProvider peImage = this._peImage;
			if (peImage != null)
			{
				peImage.Dispose();
			}
			this._peImage = null;
			AbstractMemoryBlock lazyImageBlock = this._lazyImageBlock;
			if (lazyImageBlock != null)
			{
				lazyImageBlock.Dispose();
			}
			this._lazyImageBlock = null;
			AbstractMemoryBlock lazyMetadataBlock = this._lazyMetadataBlock;
			if (lazyMetadataBlock != null)
			{
				lazyMetadataBlock.Dispose();
			}
			this._lazyMetadataBlock = null;
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

		// Token: 0x06000208 RID: 520 RVA: 0x00005150 File Offset: 0x00003350
		private MemoryBlockProvider GetPEImage()
		{
			MemoryBlockProvider peImage = this._peImage;
			if (peImage == null)
			{
				if (this._lazyPEHeaders == null)
				{
					Throw.PEReaderDisposed();
				}
				Throw.InvalidOperation_PEImageNotAvailable();
			}
			return peImage;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000517A File Offset: 0x0000337A
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

		// Token: 0x0600020A RID: 522 RVA: 0x00005190 File Offset: 0x00003390
		private void InitializePEHeaders()
		{
			StreamConstraints streamConstraints;
			Stream stream = this.GetPEImage().GetStream(out streamConstraints);
			PEHeaders value;
			if (streamConstraints.GuardOpt != null)
			{
				object guardOpt = streamConstraints.GuardOpt;
				lock (guardOpt)
				{
					value = PEReader.ReadPEHeadersNoLock(stream, streamConstraints.ImageStart, streamConstraints.ImageSize, this.IsLoadedImage);
					goto IL_67;
				}
			}
			value = PEReader.ReadPEHeadersNoLock(stream, streamConstraints.ImageStart, streamConstraints.ImageSize, this.IsLoadedImage);
			IL_67:
			Interlocked.CompareExchange<PEHeaders>(ref this._lazyPEHeaders, value, null);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005224 File Offset: 0x00003424
		private static PEHeaders ReadPEHeadersNoLock(Stream stream, long imageStartPosition, int imageSize, bool isLoadedImage)
		{
			stream.Seek(imageStartPosition, SeekOrigin.Begin);
			return new PEHeaders(stream, imageSize, isLoadedImage);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005238 File Offset: 0x00003438
		private AbstractMemoryBlock GetEntireImageBlock()
		{
			if (this._lazyImageBlock == null)
			{
				AbstractMemoryBlock memoryBlock = this.GetPEImage().GetMemoryBlock();
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyImageBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyImageBlock;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005274 File Offset: 0x00003474
		private AbstractMemoryBlock GetMetadataBlock()
		{
			if (!this.HasMetadata)
			{
				throw new InvalidOperationException("PEImageDoesNotHaveMetadata");
			}
			if (this._lazyMetadataBlock == null)
			{
				AbstractMemoryBlock memoryBlock = this.GetPEImage().GetMemoryBlock(this.PEHeaders.MetadataStartOffset, this.PEHeaders.MetadataSize);
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyMetadataBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyMetadataBlock;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000052DC File Offset: 0x000034DC
		private AbstractMemoryBlock GetPESectionBlock(int index)
		{
			MemoryBlockProvider peimage = this.GetPEImage();
			if (this._lazyPESectionBlocks == null)
			{
				Interlocked.CompareExchange<AbstractMemoryBlock[]>(ref this._lazyPESectionBlocks, new AbstractMemoryBlock[this.PEHeaders.SectionHeaders.Length], null);
			}
			AbstractMemoryBlock memoryBlock;
			if (this.IsLoadedImage)
			{
				memoryBlock = peimage.GetMemoryBlock(this.PEHeaders.SectionHeaders[index].VirtualAddress, this.PEHeaders.SectionHeaders[index].VirtualSize);
			}
			else
			{
				int size = Math.Min(this.PEHeaders.SectionHeaders[index].VirtualSize, this.PEHeaders.SectionHeaders[index].SizeOfRawData);
				memoryBlock = peimage.GetMemoryBlock(this.PEHeaders.SectionHeaders[index].PointerToRawData, size);
			}
			if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyPESectionBlocks[index], memoryBlock, null) != null)
			{
				memoryBlock.Dispose();
			}
			return this._lazyPESectionBlocks[index];
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000053ED File Offset: 0x000035ED
		public bool IsEntireImageAvailable
		{
			get
			{
				return this._lazyImageBlock != null || this._peImage != null;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00005402 File Offset: 0x00003602
		public PEMemoryBlock GetEntireImage()
		{
			return new PEMemoryBlock(this.GetEntireImageBlock(), 0);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005410 File Offset: 0x00003610
		public bool HasMetadata
		{
			get
			{
				return this.PEHeaders.MetadataSize > 0;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005420 File Offset: 0x00003620
		public PEMemoryBlock GetMetadata()
		{
			return new PEMemoryBlock(this.GetMetadataBlock(), 0);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00005430 File Offset: 0x00003630
		public PEMemoryBlock GetSectionData(int relativeVirtualAddress)
		{
			if (relativeVirtualAddress < 0)
			{
				Throw.ArgumentOutOfRange("relativeVirtualAddress");
			}
			int containingSectionIndex = this.PEHeaders.GetContainingSectionIndex(relativeVirtualAddress);
			if (containingSectionIndex < 0)
			{
				return default(PEMemoryBlock);
			}
			AbstractMemoryBlock pesectionBlock = this.GetPESectionBlock(containingSectionIndex);
			int num = relativeVirtualAddress - this.PEHeaders.SectionHeaders[containingSectionIndex].VirtualAddress;
			if (num > pesectionBlock.Size)
			{
				return default(PEMemoryBlock);
			}
			return new PEMemoryBlock(pesectionBlock, num);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000054AC File Offset: 0x000036AC
		public PEMemoryBlock GetSectionData(string sectionName)
		{
			if (sectionName == null)
			{
				Throw.ArgumentNull("sectionName");
			}
			int num = this.PEHeaders.IndexOfSection(sectionName);
			if (num < 0)
			{
				return default(PEMemoryBlock);
			}
			return new PEMemoryBlock(this.GetPESectionBlock(num), 0);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000054F0 File Offset: 0x000036F0
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
				throw new BadImageFormatException("InvalidDirectoryRVA");
			}
			if (debugTableDirectory.Size % 28 != 0)
			{
				throw new BadImageFormatException("InvalidDirectorySize");
			}
			ImmutableArray<DebugDirectoryEntry> result;
			using (AbstractMemoryBlock memoryBlock = this.GetPEImage().GetMemoryBlock(start, debugTableDirectory.Size))
			{
				result = PEReader.ReadDebugDirectoryEntries(memoryBlock.GetReader());
			}
			return result;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000558C File Offset: 0x0000378C
		internal static ImmutableArray<DebugDirectoryEntry> ReadDebugDirectoryEntries(BlobReader reader)
		{
			int num = reader.Length / 28;
			ImmutableArray<DebugDirectoryEntry>.Builder builder = ImmutableArray.CreateBuilder<DebugDirectoryEntry>(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				if (num2 != 0)
				{
					throw new BadImageFormatException("InvalidDebugDirectoryEntryCharacteristics");
				}
				uint stamp = reader.ReadUInt32();
				ushort majorVersion = reader.ReadUInt16();
				ushort minorVersion = reader.ReadUInt16();
				DebugDirectoryEntryType type = (DebugDirectoryEntryType)reader.ReadInt32();
				int dataSize = reader.ReadInt32();
				int dataRelativeVirtualAddress = reader.ReadInt32();
				int dataPointer = reader.ReadInt32();
				builder.Add(new DebugDirectoryEntry(stamp, majorVersion, minorVersion, type, dataSize, dataRelativeVirtualAddress, dataPointer));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000562C File Offset: 0x0000382C
		private AbstractMemoryBlock GetDebugDirectoryEntryDataBlock(DebugDirectoryEntry entry)
		{
			int start = this.IsLoadedImage ? entry.DataRelativeVirtualAddress : entry.DataPointer;
			return this.GetPEImage().GetMemoryBlock(start, entry.DataSize);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005668 File Offset: 0x00003868
		public CodeViewDebugDirectoryData ReadCodeViewDebugDirectoryData(DebugDirectoryEntry entry)
		{
			if (entry.Type != DebugDirectoryEntryType.CodeView)
			{
				Throw.InvalidArgument("UnexpectedDebugDirectoryType", "entry");
			}
			CodeViewDebugDirectoryData result;
			using (AbstractMemoryBlock debugDirectoryEntryDataBlock = this.GetDebugDirectoryEntryDataBlock(entry))
			{
				result = PEReader.DecodeCodeViewDebugDirectoryData(debugDirectoryEntryDataBlock);
			}
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000056BC File Offset: 0x000038BC
		internal static CodeViewDebugDirectoryData DecodeCodeViewDebugDirectoryData(AbstractMemoryBlock block)
		{
			BlobReader reader = block.GetReader();
			if (reader.ReadByte() != 82 || reader.ReadByte() != 83 || reader.ReadByte() != 68 || reader.ReadByte() != 83)
			{
				throw new BadImageFormatException("UnexpectedCodeViewDataSignature");
			}
			Guid guid = reader.ReadGuid();
			int age = reader.ReadInt32();
			string path = reader.ReadUtf8NullTerminated();
			return new CodeViewDebugDirectoryData(guid, age, path);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00005728 File Offset: 0x00003928
		public bool TryOpenAssociatedPortablePdb(string peImagePath, Func<string, Stream> pdbFileStreamProvider, out MetadataReaderProvider pdbReaderProvider, out string pdbPath)
		{
			if (peImagePath == null)
			{
				Throw.ArgumentNull("peImagePath");
			}
			if (pdbFileStreamProvider == null)
			{
				Throw.ArgumentNull("pdbFileStreamProvider");
			}
			pdbReaderProvider = null;
			pdbPath = null;
			string directoryName;
			try
			{
				directoryName = Path.GetDirectoryName(peImagePath);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "peImagePath");
			}
			Exception ex2 = null;
			ImmutableArray<DebugDirectoryEntry> immutableArray = this.ReadDebugDirectory();
			DebugDirectoryEntry codeViewEntry = immutableArray.FirstOrDefault((DebugDirectoryEntry e) => e.IsPortableCodeView);
			if (codeViewEntry.DataSize != 0 && this.TryOpenCodeViewPortablePdb(codeViewEntry, directoryName, pdbFileStreamProvider, out pdbReaderProvider, out pdbPath, ref ex2))
			{
				return true;
			}
			DebugDirectoryEntry embeddedPdbEntry = immutableArray.FirstOrDefault((DebugDirectoryEntry e) => e.Type == DebugDirectoryEntryType.EmbeddedPortablePdb);
			if (embeddedPdbEntry.DataSize != 0)
			{
				bool flag = false;
				pdbReaderProvider = null;
				this.TryOpenEmbeddedPortablePdb(embeddedPdbEntry, ref flag, ref pdbReaderProvider, ref ex2);
				if (flag)
				{
					return true;
				}
			}
			if (ex2 != null)
			{
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			return false;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005828 File Offset: 0x00003A28
		private bool TryOpenCodeViewPortablePdb(DebugDirectoryEntry codeViewEntry, string peImageDirectory, Func<string, Stream> pdbFileStreamProvider, out MetadataReaderProvider provider, out string pdbPath, ref Exception errorToReport)
		{
			pdbPath = null;
			provider = null;
			CodeViewDebugDirectoryData codeViewDebugDirectoryData;
			try
			{
				codeViewDebugDirectoryData = this.ReadCodeViewDebugDirectoryData(codeViewEntry);
			}
			catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
			{
				errorToReport = (errorToReport ?? ex);
				return false;
			}
			BlobContentId id = new BlobContentId(codeViewDebugDirectoryData.Guid, codeViewEntry.Stamp);
			string text = PathUtilities.CombinePathWithRelativePath(peImageDirectory, PathUtilities.GetFileName(codeViewDebugDirectoryData.Path, true));
			if (this.TryOpenPortablePdbFile(text, id, pdbFileStreamProvider, out provider, ref errorToReport))
			{
				pdbPath = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000058D0 File Offset: 0x00003AD0
		private bool TryOpenPortablePdbFile(string path, BlobContentId id, Func<string, Stream> pdbFileStreamProvider, out MetadataReaderProvider provider, ref Exception errorToReport)
		{
			provider = null;
			MetadataReaderProvider metadataReaderProvider = null;
			bool result;
			try
			{
				Stream stream;
				try
				{
					stream = pdbFileStreamProvider(path);
				}
				catch (FileNotFoundException)
				{
					stream = null;
				}
				if (stream == null)
				{
					result = false;
				}
				else
				{
					if (!stream.CanRead || !stream.CanSeek)
					{
						throw new InvalidOperationException("StreamMustSupportReadAndSeek");
					}
					metadataReaderProvider = MetadataReaderProvider.FromPortablePdbStream(stream, MetadataStreamOptions.Default, 0);
					if (new BlobContentId(metadataReaderProvider.GetMetadataReader(MetadataReaderOptions.Default).DebugMetadataHeader.Id) != id)
					{
						result = false;
					}
					else
					{
						provider = metadataReaderProvider;
						result = true;
					}
				}
			}
			catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
			{
				errorToReport = (errorToReport ?? ex);
				result = false;
			}
			finally
			{
				if (provider == null && metadataReaderProvider != null)
				{
					metadataReaderProvider.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000059B8 File Offset: 0x00003BB8
		private void TryOpenEmbeddedPortablePdb(DebugDirectoryEntry embeddedPdbEntry, ref bool openedEmbeddedPdb, ref MetadataReaderProvider provider, ref Exception errorToReport)
		{
			provider = null;
			MetadataReaderProvider metadataReaderProvider = null;
			try
			{
				metadataReaderProvider = this.ReadEmbeddedPortablePdbDebugDirectoryData(embeddedPdbEntry);
				metadataReaderProvider.GetMetadataReader(MetadataReaderOptions.Default);
				provider = metadataReaderProvider;
				openedEmbeddedPdb = true;
			}
			catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
			{
				errorToReport = (errorToReport ?? ex);
				openedEmbeddedPdb = false;
			}
			finally
			{
				if (metadataReaderProvider == null && metadataReaderProvider != null)
				{
					metadataReaderProvider.Dispose();
				}
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005A44 File Offset: 0x00003C44
		public MetadataReaderProvider ReadEmbeddedPortablePdbDebugDirectoryData(DebugDirectoryEntry entry)
		{
			if (entry.Type != DebugDirectoryEntryType.EmbeddedPortablePdb)
			{
				Throw.InvalidArgument("UnexpectedDebugDirectoryType", "entry");
			}
			PEReader.ValidateEmbeddedPortablePdbVersion(entry);
			MetadataReaderProvider result;
			using (AbstractMemoryBlock debugDirectoryEntryDataBlock = this.GetDebugDirectoryEntryDataBlock(entry))
			{
				ImmutableArray<byte> image = PEReader.DecodeEmbeddedPortablePdbDebugDirectoryData(debugDirectoryEntryDataBlock);
				result = MetadataReaderProvider.FromPortablePdbImage(image);
			}
			return result;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00005AA4 File Offset: 0x00003CA4
		internal static void ValidateEmbeddedPortablePdbVersion(DebugDirectoryEntry entry)
		{
			ushort majorVersion = entry.MajorVersion;
			if (majorVersion < 256)
			{
				throw new BadImageFormatException("UnsupportedFormatVersion");
			}
			ushort minorVersion = entry.MinorVersion;
			if (minorVersion != 256)
			{
				throw new BadImageFormatException("UnsupportedFormatVersion");
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00005AE8 File Offset: 0x00003CE8
		[SecuritySafeCritical]
		internal static ImmutableArray<byte> DecodeEmbeddedPortablePdbDebugDirectoryData(AbstractMemoryBlock block)
		{
			BlobReader reader = block.GetReader();
			if (reader.ReadUInt32() != 1111773261U)
			{
				throw new BadImageFormatException("UnexpectedEmbeddedPortablePdbDataSignature");
			}
			int num = reader.ReadInt32();
			byte[] array;
			try
			{
				array = new byte[num];
			}
			catch
			{
				throw new BadImageFormatException("DataTooBig");
			}
			ReadOnlyUnmanagedMemoryStream stream = new ReadOnlyUnmanagedMemoryStream(reader.CurrentPointer, reader.RemainingBytes);
			DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true);
			if (num > 0)
			{
				int num2;
				try
				{
					num2 = deflateStream.TryReadAll(array, 0, array.Length);
				}
				catch (InvalidDataException ex)
				{
					throw new BadImageFormatException(ex.Message, ex.InnerException);
				}
				if (num2 != array.Length)
				{
					throw new BadImageFormatException("SizeMismatch");
				}
			}
			if (deflateStream.ReadByte() != -1)
			{
				throw new BadImageFormatException("SizeMismatch");
			}
			return new ImmutableArray<byte>(array);
		}

		// Token: 0x040002DE RID: 734
		private MemoryBlockProvider _peImage;

		// Token: 0x040002DF RID: 735
		private PEHeaders _lazyPEHeaders;

		// Token: 0x040002E0 RID: 736
		private AbstractMemoryBlock _lazyMetadataBlock;

		// Token: 0x040002E1 RID: 737
		private AbstractMemoryBlock _lazyImageBlock;

		// Token: 0x040002E2 RID: 738
		private AbstractMemoryBlock[] _lazyPESectionBlocks;
	}
}
