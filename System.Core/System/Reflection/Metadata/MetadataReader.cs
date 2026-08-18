using System;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Security;

namespace System.Reflection.Metadata
{
	// Token: 0x02000058 RID: 88
	internal sealed class MetadataReader
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000674C File Offset: 0x0000494C
		[SecurityCritical]
		public unsafe MetadataReader(byte* metadata, int length, MetadataReaderOptions options)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			this.Block = new MemoryBlock(metadata, length);
			this._options = options;
			BlobReader blobReader = new BlobReader(this.Block);
			this.ReadMetadataHeader(ref blobReader, out this._versionString);
			this._metadataKind = this.GetMetadataKind(this._versionString);
			StreamHeader[] streamHeaders = this.ReadStreamHeaders(ref blobReader);
			MemoryBlock block;
			MemoryBlock memoryBlock;
			this.InitializeStreamReaders(ref this.Block, streamHeaders, out this._metadataStreamKind, out block, out memoryBlock);
			int[] externalRowCountsOpt;
			if (memoryBlock.Length > 0)
			{
				MetadataReader.ReadStandalonePortablePdbStream(memoryBlock, out this._debugMetadataHeader, out externalRowCountsOpt);
			}
			else
			{
				externalRowCountsOpt = null;
			}
			BlobReader blobReader2 = new BlobReader(block);
			HeapSizes heapSizes;
			int[] rowCounts;
			this.ReadMetadataTableHeader(ref blobReader2, out heapSizes, out rowCounts, out this._sortedTables);
			this.InitializeTableReaders(blobReader2.GetMemoryBlockAt(0, blobReader2.RemainingBytes), heapSizes, rowCounts, externalRowCountsOpt);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006830 File Offset: 0x00004A30
		private void ReadMetadataHeader(ref BlobReader memReader, out string versionString)
		{
			if (memReader.RemainingBytes < 16)
			{
				throw new BadImageFormatException("MetadataHeaderTooSmall");
			}
			uint num = memReader.ReadUInt32();
			if (num != 1112167234U)
			{
				throw new BadImageFormatException("MetadataSignature");
			}
			memReader.ReadUInt16();
			memReader.ReadUInt16();
			memReader.ReadUInt32();
			int num2 = memReader.ReadInt32();
			if (memReader.RemainingBytes < num2)
			{
				throw new BadImageFormatException("NotEnoughSpaceForVersionString");
			}
			int num3;
			versionString = memReader.GetMemoryBlockAt(0, num2).PeekUtf8NullTerminated(0, out num3, '\0');
			memReader.Offset += num2;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000068C0 File Offset: 0x00004AC0
		private MetadataKind GetMetadataKind(string versionString)
		{
			if ((this._options & MetadataReaderOptions.Default) == MetadataReaderOptions.None)
			{
				return MetadataKind.Ecma335;
			}
			if (!versionString.Contains("WindowsRuntime"))
			{
				return MetadataKind.Ecma335;
			}
			if (versionString.Contains("CLR"))
			{
				return MetadataKind.ManagedWindowsMetadata;
			}
			return MetadataKind.WindowsMetadata;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000068F0 File Offset: 0x00004AF0
		private StreamHeader[] ReadStreamHeaders(ref BlobReader memReader)
		{
			memReader.ReadUInt16();
			int num = (int)memReader.ReadInt16();
			StreamHeader[] array = new StreamHeader[num];
			for (int i = 0; i < array.Length; i++)
			{
				if (memReader.RemainingBytes < 8)
				{
					throw new BadImageFormatException("StreamHeaderTooSmall");
				}
				array[i].Offset = memReader.ReadUInt32();
				array[i].Size = memReader.ReadInt32();
				array[i].Name = memReader.ReadUtf8NullTerminated();
				bool flag = memReader.TryAlign(4);
				if (!flag || memReader.RemainingBytes == 0)
				{
					throw new BadImageFormatException("NotEnoughSpaceForStreamHeaderName");
				}
			}
			return array;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000698C File Offset: 0x00004B8C
		private void InitializeStreamReaders(ref MemoryBlock metadataRoot, StreamHeader[] streamHeaders, out MetadataStreamKind metadataStreamKind, out MemoryBlock metadataTableStream, out MemoryBlock standalonePdbStream)
		{
			metadataTableStream = default(MemoryBlock);
			standalonePdbStream = default(MemoryBlock);
			metadataStreamKind = MetadataStreamKind.Illegal;
			foreach (StreamHeader streamHeader in streamHeaders)
			{
				string name = streamHeader.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 617129517U)
				{
					if (num <= 368124450U)
					{
						if (num != 154065968U)
						{
							if (num == 368124450U)
							{
								if (name == "#US")
								{
									if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
									{
										throw new BadImageFormatException("NotEnoughSpaceForBlobStream");
									}
								}
							}
						}
						else if (name == "#JTD")
						{
							if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
							{
								throw new BadImageFormatException("NotEnoughSpaceForMetadataStream");
							}
							this.IsMinimalDelta = true;
						}
					}
					else if (num != 491825896U)
					{
						if (num == 617129517U)
						{
							if (name == "#-")
							{
								if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
								{
									throw new BadImageFormatException("NotEnoughSpaceForMetadataStream");
								}
								metadataStreamKind = MetadataStreamKind.Uncompressed;
								metadataTableStream = metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size);
							}
						}
					}
					else if (name == "#Strings")
					{
						if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
						{
							throw new BadImageFormatException("NotEnoughSpaceForStringStream");
						}
					}
				}
				else if (num <= 1422005491U)
				{
					if (num != 1372122372U)
					{
						if (num == 1422005491U)
						{
							if (name == "#GUID")
							{
								if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
								{
									throw new BadImageFormatException("NotEnoughSpaceForGUIDStream");
								}
							}
						}
					}
					else if (name == "#~")
					{
						if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
						{
							throw new BadImageFormatException("NotEnoughSpaceForMetadataStream");
						}
						metadataStreamKind = MetadataStreamKind.Compressed;
						metadataTableStream = metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size);
					}
				}
				else if (num != 1638201209U)
				{
					if (num == 2979271308U)
					{
						if (name == "#Pdb")
						{
							if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
							{
								throw new BadImageFormatException("NotEnoughSpaceForMetadataStream");
							}
							standalonePdbStream = metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size);
						}
					}
				}
				else if (name == "#Blob")
				{
					if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
					{
						throw new BadImageFormatException("NotEnoughSpaceForBlobStream");
					}
					this.BlobHeap = new BlobHeap(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size), this._metadataKind);
				}
			}
			if (this.IsMinimalDelta && metadataStreamKind != MetadataStreamKind.Uncompressed)
			{
				throw new BadImageFormatException("InvalidMetadataStreamFormat");
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00006CC8 File Offset: 0x00004EC8
		private void ReadMetadataTableHeader(ref BlobReader reader, out HeapSizes heapSizes, out int[] metadataTableRowCounts, out TableMask sortedTables)
		{
			if (reader.RemainingBytes < 24)
			{
				throw new BadImageFormatException("MetadataTableHeaderTooSmall");
			}
			reader.ReadUInt32();
			reader.ReadByte();
			reader.ReadByte();
			heapSizes = (HeapSizes)reader.ReadByte();
			reader.ReadByte();
			ulong num = reader.ReadUInt64();
			sortedTables = (TableMask)reader.ReadUInt64();
			ulong num2 = 71811071505072127UL;
			if ((num & ~(num2 != 0UL)) != 0UL)
			{
				throw new BadImageFormatException("UnknownTables");
			}
			if (this._metadataStreamKind == MetadataStreamKind.Compressed && (num & (ulong)-2142764888) != 0UL)
			{
				throw new BadImageFormatException("IllegalTablesInCompressedMetadataStream");
			}
			metadataTableRowCounts = MetadataReader.ReadMetadataTableRowCounts(ref reader, num);
			if ((heapSizes & HeapSizes.ExtraData) == HeapSizes.ExtraData)
			{
				reader.ReadUInt32();
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00006D70 File Offset: 0x00004F70
		private static int[] ReadMetadataTableRowCounts(ref BlobReader memReader, ulong presentTableMask)
		{
			ulong num = 1UL;
			int[] array = new int[MetadataTokens.TableCount];
			for (int i = 0; i < array.Length; i++)
			{
				if ((presentTableMask & num) != 0UL)
				{
					if (memReader.RemainingBytes < 4)
					{
						throw new BadImageFormatException("TableRowCountSpaceTooSmall");
					}
					uint num2 = memReader.ReadUInt32();
					if (num2 > 16777215U)
					{
						throw new BadImageFormatException("InvalidRowCount");
					}
					array[i] = (int)num2;
				}
				num <<= 1;
			}
			return array;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00006DD8 File Offset: 0x00004FD8
		internal static void ReadStandalonePortablePdbStream(MemoryBlock block, out DebugMetadataHeader debugMetadataHeader, out int[] externalTableRowCounts)
		{
			BlobReader blobReader = new BlobReader(block);
			byte[] array = blobReader.ReadBytes(20);
			uint num = blobReader.ReadUInt32();
			int num2 = (int)(num & 16777215U);
			if (num != 0U && ((num & 2130706432U) != 100663296U || num2 == 0))
			{
				throw new BadImageFormatException("InvalidEntryPointToken");
			}
			ulong num3 = blobReader.ReadUInt64();
			if ((num3 & 18446709124491641000UL) != 0UL)
			{
				throw new BadImageFormatException("UnknownTables");
			}
			externalTableRowCounts = MetadataReader.ReadMetadataTableRowCounts(ref blobReader, num3);
			debugMetadataHeader = new DebugMetadataHeader(new ImmutableArray<byte>(array), MethodDefinitionHandle.FromRowId(num2));
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00006E66 File Offset: 0x00005066
		private int GetReferenceSize(int[] rowCounts, TableIndex index)
		{
			if ((long)rowCounts[(int)index] >= 65536L || this.IsMinimalDelta)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00006E80 File Offset: 0x00005080
		private void InitializeTableReaders(MemoryBlock metadataTablesMemoryBlock, HeapSizes heapSizes, int[] rowCounts, int[] externalRowCountsOpt)
		{
			this.TableRowCounts = rowCounts;
			int guidHeapRefSize = ((heapSizes & HeapSizes.GuidHeapLarge) == HeapSizes.GuidHeapLarge) ? 4 : 2;
			int blobHeapRefSize = ((heapSizes & HeapSizes.BlobHeapLarge) == HeapSizes.BlobHeapLarge) ? 4 : 2;
			int num = 0;
			this.DocumentTable = new DocumentTableReader(rowCounts[48], guidHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.DocumentTable.Block.Length;
			this.MethodDebugInformationTable = new MethodDebugInformationTableReader(rowCounts[49], this.GetReferenceSize(rowCounts, TableIndex.Document), blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.MethodDebugInformationTable.Block.Length;
			if (num > metadataTablesMemoryBlock.Length)
			{
				throw new BadImageFormatException("MetadataTablesTooSmall");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00006F14 File Offset: 0x00005114
		public MetadataReaderOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00006F1C File Offset: 0x0000511C
		public string MetadataVersion
		{
			get
			{
				return this._versionString;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00006F24 File Offset: 0x00005124
		public DebugMetadataHeader DebugMetadataHeader
		{
			get
			{
				return this._debugMetadataHeader;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00006F2C File Offset: 0x0000512C
		public string GetString(DocumentNameBlobHandle handle)
		{
			return this.BlobHeap.GetDocumentName(handle);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00006F3A File Offset: 0x0000513A
		public Document GetDocument(DocumentHandle handle)
		{
			return new Document(this, handle);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00006F43 File Offset: 0x00005143
		public MethodDebugInformation GetMethodDebugInformation(MethodDebugInformationHandle handle)
		{
			return new MethodDebugInformation(this, handle);
		}

		// Token: 0x0400032D RID: 813
		internal readonly MemoryBlock Block;

		// Token: 0x0400032E RID: 814
		private readonly MetadataReaderOptions _options;

		// Token: 0x0400032F RID: 815
		private readonly string _versionString;

		// Token: 0x04000330 RID: 816
		private readonly MetadataKind _metadataKind;

		// Token: 0x04000331 RID: 817
		private readonly MetadataStreamKind _metadataStreamKind;

		// Token: 0x04000332 RID: 818
		private readonly DebugMetadataHeader _debugMetadataHeader;

		// Token: 0x04000333 RID: 819
		internal BlobHeap BlobHeap;

		// Token: 0x04000334 RID: 820
		internal bool IsMinimalDelta;

		// Token: 0x04000335 RID: 821
		private readonly TableMask _sortedTables;

		// Token: 0x04000336 RID: 822
		internal int[] TableRowCounts;

		// Token: 0x04000337 RID: 823
		internal DocumentTableReader DocumentTable;

		// Token: 0x04000338 RID: 824
		internal MethodDebugInformationTableReader MethodDebugInformationTable;

		// Token: 0x04000339 RID: 825
		private const int SmallIndexSize = 2;

		// Token: 0x0400033A RID: 826
		private const int LargeIndexSize = 4;
	}
}
