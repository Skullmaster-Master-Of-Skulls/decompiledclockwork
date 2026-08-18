using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Metadata
{
	// Token: 0x0200007C RID: 124
	public sealed class MetadataReader
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0000AEB9 File Offset: 0x000090B9
		public unsafe MetadataReader(byte* metadata, int length) : this(metadata, length, MetadataReaderOptions.Default, null)
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000AEC5 File Offset: 0x000090C5
		public unsafe MetadataReader(byte* metadata, int length, MetadataReaderOptions options) : this(metadata, length, options, null)
		{
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000AED4 File Offset: 0x000090D4
		public unsafe MetadataReader(byte* metadata, int length, MetadataReaderOptions options, MetadataStringDecoder utf8Decoder)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (utf8Decoder == null)
			{
				utf8Decoder = MetadataStringDecoder.DefaultUTF8;
			}
			if (!(utf8Decoder.Encoding is UTF8Encoding))
			{
				throw new ArgumentException(SR.MetadataStringDecoderEncodingMustBeUtf8, "utf8Decoder");
			}
			if (!BitConverter.IsLittleEndian)
			{
				throw new PlatformNotSupportedException(SR.LitteEndianArchitectureRequired);
			}
			this.Block = new MemoryBlock(metadata, length);
			this._options = options;
			this.utf8Decoder = utf8Decoder;
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
			if (memoryBlock.Length == 0 && this.ModuleTable.NumberOfRows < 1)
			{
				throw new BadImageFormatException(SR.Format(SR.ModuleTableInvalidNumberOfRows, this.ModuleTable.NumberOfRows));
			}
			this.namespaceCache = new NamespaceCache(this);
			if (this._metadataKind != MetadataKind.Ecma335)
			{
				this.WinMDMscorlibRef = this.FindMscorlibAssemblyRefNoProjection();
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000B054 File Offset: 0x00009254
		private void ReadMetadataHeader(ref BlobReader memReader, out string versionString)
		{
			if (memReader.RemainingBytes < 16)
			{
				throw new BadImageFormatException(SR.MetadataHeaderTooSmall);
			}
			if (memReader.ReadUInt32() != 1112167234U)
			{
				throw new BadImageFormatException(SR.MetadataSignature);
			}
			memReader.ReadUInt16();
			memReader.ReadUInt16();
			memReader.ReadUInt32();
			int num = memReader.ReadInt32();
			if (memReader.RemainingBytes < num)
			{
				throw new BadImageFormatException(SR.NotEnoughSpaceForVersionString);
			}
			int num2;
			versionString = memReader.GetMemoryBlockAt(0, num).PeekUtf8NullTerminated(0, null, this.utf8Decoder, out num2, '\0');
			memReader.SkipBytes(num);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000B0E2 File Offset: 0x000092E2
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

		// Token: 0x0600054B RID: 1355 RVA: 0x0000B110 File Offset: 0x00009310
		private StreamHeader[] ReadStreamHeaders(ref BlobReader memReader)
		{
			memReader.ReadUInt16();
			StreamHeader[] array = new StreamHeader[(int)memReader.ReadInt16()];
			for (int i = 0; i < array.Length; i++)
			{
				if (memReader.RemainingBytes < 8)
				{
					throw new BadImageFormatException(SR.StreamHeaderTooSmall);
				}
				array[i].Offset = memReader.ReadUInt32();
				array[i].Size = memReader.ReadInt32();
				array[i].Name = memReader.ReadUtf8NullTerminated();
				if (!memReader.TryAlign(4) || memReader.RemainingBytes == 0)
				{
					throw new BadImageFormatException(SR.NotEnoughSpaceForStreamHeaderName);
				}
			}
			return array;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000B1A8 File Offset: 0x000093A8
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
										throw new BadImageFormatException(SR.NotEnoughSpaceForBlobStream);
									}
									this.UserStringStream = new UserStringStreamReader(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size));
								}
							}
						}
						else if (name == "#JTD")
						{
							if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
							{
								throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
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
									throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
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
							throw new BadImageFormatException(SR.NotEnoughSpaceForStringStream);
						}
						this.StringStream = new StringStreamReader(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size), this._metadataKind);
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
									throw new BadImageFormatException(SR.NotEnoughSpaceForGUIDStream);
								}
								this.GuidStream = new GuidStreamReader(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size));
							}
						}
					}
					else if (name == "#~")
					{
						if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
						{
							throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
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
								throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
							}
							standalonePdbStream = metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size);
						}
					}
				}
				else if (name == "#Blob")
				{
					if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
					{
						throw new BadImageFormatException(SR.NotEnoughSpaceForBlobStream);
					}
					this.BlobStream = new BlobStreamReader(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size), this._metadataKind);
				}
			}
			if (this.IsMinimalDelta && metadataStreamKind != MetadataStreamKind.Uncompressed)
			{
				throw new BadImageFormatException(SR.InvalidMetadataStreamFormat);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000B548 File Offset: 0x00009748
		private void ReadMetadataTableHeader(ref BlobReader reader, out HeapSizes heapSizes, out int[] metadataTableRowCounts, out TableMask sortedTables)
		{
			if (reader.RemainingBytes < 24)
			{
				throw new BadImageFormatException(SR.MetadataTableHeaderTooSmall);
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
				throw new BadImageFormatException(SR.Format(SR.UnknownTables, num));
			}
			if (this._metadataStreamKind == MetadataStreamKind.Compressed && (num & (ulong)-2142764888) != 0UL)
			{
				throw new BadImageFormatException(SR.IllegalTablesInCompressedMetadataStream);
			}
			metadataTableRowCounts = MetadataReader.ReadMetadataTableRowCounts(ref reader, num);
			if ((heapSizes & HeapSizes.ExtraData) == HeapSizes.ExtraData)
			{
				reader.ReadUInt32();
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000B5FC File Offset: 0x000097FC
		private static int[] ReadMetadataTableRowCounts(ref BlobReader memReader, ulong presentTableMask)
		{
			ulong num = 1UL;
			int[] array = new int[56];
			for (int i = 0; i < array.Length; i++)
			{
				if ((presentTableMask & num) != 0UL)
				{
					if (memReader.RemainingBytes < 4)
					{
						throw new BadImageFormatException(SR.TableRowCountSpaceTooSmall);
					}
					uint num2 = memReader.ReadUInt32();
					if (num2 > 16777215U)
					{
						throw new BadImageFormatException(SR.Format(SR.InvalidRowCount, num2));
					}
					array[i] = (int)num2;
				}
				num <<= 1;
			}
			return array;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000B66C File Offset: 0x0000986C
		internal static void ReadStandalonePortablePdbStream(MemoryBlock block, out DebugMetadataHeader debugMetadataHeader, out int[] externalTableRowCounts)
		{
			BlobReader blobReader = new BlobReader(block);
			byte[] array = blobReader.ReadBytes(20);
			uint num = blobReader.ReadUInt32();
			int num2 = (int)(num & 16777215U);
			if (num != 0U && ((num & 2130706432U) != 100663296U || num2 == 0))
			{
				throw new BadImageFormatException(string.Format(SR.InvalidEntryPointToken, new object[]
				{
					num
				}));
			}
			ulong num3 = blobReader.ReadUInt64();
			if ((num3 & 18446709124491641000UL) != 0UL)
			{
				throw new BadImageFormatException(string.Format(SR.UnknownTables, new object[]
				{
					(TableMask)num3
				}));
			}
			externalTableRowCounts = MetadataReader.ReadMetadataTableRowCounts(ref blobReader, num3);
			debugMetadataHeader = new DebugMetadataHeader(ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array), MethodDefinitionHandle.FromRowId(num2));
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000B724 File Offset: 0x00009924
		private int GetReferenceSize(int[] rowCounts, TableIndex index)
		{
			if ((long)rowCounts[(int)index] >= 65536L || this.IsMinimalDelta)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000B740 File Offset: 0x00009940
		private void InitializeTableReaders(MemoryBlock metadataTablesMemoryBlock, HeapSizes heapSizes, int[] rowCounts, int[] externalRowCountsOpt)
		{
			this.TableRowCounts = rowCounts;
			int fieldRefSize = (this.GetReferenceSize(rowCounts, TableIndex.FieldPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Field);
			int methodRefSize = (this.GetReferenceSize(rowCounts, TableIndex.MethodPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.MethodDef);
			int paramRefSize = (this.GetReferenceSize(rowCounts, TableIndex.ParamPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Param);
			int eventRefSize = (this.GetReferenceSize(rowCounts, TableIndex.EventPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Event);
			int propertyRefSize = (this.GetReferenceSize(rowCounts, TableIndex.PropertyPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Property);
			int typeDefOrRefRefSize = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.TypeRef | TableMask.TypeDef | TableMask.TypeSpec);
			int hasConstantRefSize = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.Field | TableMask.Param | TableMask.Property);
			int hasCustomAttributeRefSize = this.ComputeCodedTokenSize(2048, rowCounts, TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint);
			int hasFieldMarshalRefSize = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.Field | TableMask.Param);
			int hasDeclSecurityRefSize = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.TypeDef | TableMask.MethodDef | TableMask.Assembly);
			int memberRefParentRefSize = this.ComputeCodedTokenSize(8192, rowCounts, TableMask.TypeRef | TableMask.TypeDef | TableMask.MethodDef | TableMask.ModuleRef | TableMask.TypeSpec);
			int hasSemanticRefSize = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.Event | TableMask.Property);
			int methodDefOrRefRefSize = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.MethodDef | TableMask.MemberRef);
			int memberForwardedRefSize = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.Field | TableMask.MethodDef);
			int implementationRefSize = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType);
			int customAttributeTypeRefSize = this.ComputeCodedTokenSize(8192, rowCounts, TableMask.MethodDef | TableMask.MemberRef);
			int resolutionScopeRefSize = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.Module | TableMask.TypeRef | TableMask.ModuleRef | TableMask.AssemblyRef);
			int typeOrMethodDefRefSize = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.TypeDef | TableMask.MethodDef);
			int stringHeapRefSize = ((heapSizes & HeapSizes.StringHeapLarge) == HeapSizes.StringHeapLarge) ? 4 : 2;
			int guidHeapRefSize = ((heapSizes & HeapSizes.GuidHeapLarge) == HeapSizes.GuidHeapLarge) ? 4 : 2;
			int blobHeapRefSize = ((heapSizes & HeapSizes.BlobHeapLarge) == HeapSizes.BlobHeapLarge) ? 4 : 2;
			int num = 0;
			this.ModuleTable = new ModuleTableReader(rowCounts[0], stringHeapRefSize, guidHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ModuleTable.Block.Length;
			this.TypeRefTable = new TypeRefTableReader(rowCounts[1], resolutionScopeRefSize, stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.TypeRefTable.Block.Length;
			this.TypeDefTable = new TypeDefTableReader(rowCounts[2], fieldRefSize, methodRefSize, typeDefOrRefRefSize, stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.TypeDefTable.Block.Length;
			this.FieldPtrTable = new FieldPtrTableReader(rowCounts[3], this.GetReferenceSize(rowCounts, TableIndex.Field), metadataTablesMemoryBlock, num);
			num += this.FieldPtrTable.Block.Length;
			this.FieldTable = new FieldTableReader(rowCounts[4], stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.FieldTable.Block.Length;
			this.MethodPtrTable = new MethodPtrTableReader(rowCounts[5], this.GetReferenceSize(rowCounts, TableIndex.MethodDef), metadataTablesMemoryBlock, num);
			num += this.MethodPtrTable.Block.Length;
			this.MethodDefTable = new MethodTableReader(rowCounts[6], paramRefSize, stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.MethodDefTable.Block.Length;
			this.ParamPtrTable = new ParamPtrTableReader(rowCounts[7], this.GetReferenceSize(rowCounts, TableIndex.Param), metadataTablesMemoryBlock, num);
			num += this.ParamPtrTable.Block.Length;
			this.ParamTable = new ParamTableReader(rowCounts[8], stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ParamTable.Block.Length;
			this.InterfaceImplTable = new InterfaceImplTableReader(rowCounts[9], this.IsDeclaredSorted(TableMask.InterfaceImpl), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), typeDefOrRefRefSize, metadataTablesMemoryBlock, num);
			num += this.InterfaceImplTable.Block.Length;
			this.MemberRefTable = new MemberRefTableReader(rowCounts[10], memberRefParentRefSize, stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.MemberRefTable.Block.Length;
			this.ConstantTable = new ConstantTableReader(rowCounts[11], this.IsDeclaredSorted(TableMask.Constant), hasConstantRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ConstantTable.Block.Length;
			this.CustomAttributeTable = new CustomAttributeTableReader(rowCounts[12], this.IsDeclaredSorted(TableMask.CustomAttribute), hasCustomAttributeRefSize, customAttributeTypeRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.CustomAttributeTable.Block.Length;
			this.FieldMarshalTable = new FieldMarshalTableReader(rowCounts[13], this.IsDeclaredSorted(TableMask.FieldMarshal), hasFieldMarshalRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.FieldMarshalTable.Block.Length;
			this.DeclSecurityTable = new DeclSecurityTableReader(rowCounts[14], this.IsDeclaredSorted(TableMask.DeclSecurity), hasDeclSecurityRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.DeclSecurityTable.Block.Length;
			this.ClassLayoutTable = new ClassLayoutTableReader(rowCounts[15], this.IsDeclaredSorted(TableMask.ClassLayout), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), metadataTablesMemoryBlock, num);
			num += this.ClassLayoutTable.Block.Length;
			this.FieldLayoutTable = new FieldLayoutTableReader(rowCounts[16], this.IsDeclaredSorted(TableMask.FieldLayout), this.GetReferenceSize(rowCounts, TableIndex.Field), metadataTablesMemoryBlock, num);
			num += this.FieldLayoutTable.Block.Length;
			this.StandAloneSigTable = new StandAloneSigTableReader(rowCounts[17], blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.StandAloneSigTable.Block.Length;
			this.EventMapTable = new EventMapTableReader(rowCounts[18], this.GetReferenceSize(rowCounts, TableIndex.TypeDef), eventRefSize, metadataTablesMemoryBlock, num);
			num += this.EventMapTable.Block.Length;
			this.EventPtrTable = new EventPtrTableReader(rowCounts[19], this.GetReferenceSize(rowCounts, TableIndex.Event), metadataTablesMemoryBlock, num);
			num += this.EventPtrTable.Block.Length;
			this.EventTable = new EventTableReader(rowCounts[20], typeDefOrRefRefSize, stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.EventTable.Block.Length;
			this.PropertyMapTable = new PropertyMapTableReader(rowCounts[21], this.GetReferenceSize(rowCounts, TableIndex.TypeDef), propertyRefSize, metadataTablesMemoryBlock, num);
			num += this.PropertyMapTable.Block.Length;
			this.PropertyPtrTable = new PropertyPtrTableReader(rowCounts[22], this.GetReferenceSize(rowCounts, TableIndex.Property), metadataTablesMemoryBlock, num);
			num += this.PropertyPtrTable.Block.Length;
			this.PropertyTable = new PropertyTableReader(rowCounts[23], stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.PropertyTable.Block.Length;
			this.MethodSemanticsTable = new MethodSemanticsTableReader(rowCounts[24], this.IsDeclaredSorted(TableMask.MethodSemantics), this.GetReferenceSize(rowCounts, TableIndex.MethodDef), hasSemanticRefSize, metadataTablesMemoryBlock, num);
			num += this.MethodSemanticsTable.Block.Length;
			this.MethodImplTable = new MethodImplTableReader(rowCounts[25], this.IsDeclaredSorted(TableMask.MethodImpl), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), methodDefOrRefRefSize, metadataTablesMemoryBlock, num);
			num += this.MethodImplTable.Block.Length;
			this.ModuleRefTable = new ModuleRefTableReader(rowCounts[26], stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ModuleRefTable.Block.Length;
			this.TypeSpecTable = new TypeSpecTableReader(rowCounts[27], blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.TypeSpecTable.Block.Length;
			this.ImplMapTable = new ImplMapTableReader(rowCounts[28], this.IsDeclaredSorted(TableMask.ImplMap), this.GetReferenceSize(rowCounts, TableIndex.ModuleRef), memberForwardedRefSize, stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ImplMapTable.Block.Length;
			this.FieldRvaTable = new FieldRVATableReader(rowCounts[29], this.IsDeclaredSorted(TableMask.FieldRva), this.GetReferenceSize(rowCounts, TableIndex.Field), metadataTablesMemoryBlock, num);
			num += this.FieldRvaTable.Block.Length;
			this.EncLogTable = new EnCLogTableReader(rowCounts[30], metadataTablesMemoryBlock, num, this._metadataStreamKind);
			num += this.EncLogTable.Block.Length;
			this.EncMapTable = new EnCMapTableReader(rowCounts[31], metadataTablesMemoryBlock, num);
			num += this.EncMapTable.Block.Length;
			this.AssemblyTable = new AssemblyTableReader(rowCounts[32], stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.AssemblyTable.Block.Length;
			this.AssemblyProcessorTable = new AssemblyProcessorTableReader(rowCounts[33], metadataTablesMemoryBlock, num);
			num += this.AssemblyProcessorTable.Block.Length;
			this.AssemblyOSTable = new AssemblyOSTableReader(rowCounts[34], metadataTablesMemoryBlock, num);
			num += this.AssemblyOSTable.Block.Length;
			this.AssemblyRefTable = new AssemblyRefTableReader(rowCounts[35], stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num, this._metadataKind);
			num += this.AssemblyRefTable.Block.Length;
			this.AssemblyRefProcessorTable = new AssemblyRefProcessorTableReader(rowCounts[36], this.GetReferenceSize(rowCounts, TableIndex.AssemblyRef), metadataTablesMemoryBlock, num);
			num += this.AssemblyRefProcessorTable.Block.Length;
			this.AssemblyRefOSTable = new AssemblyRefOSTableReader(rowCounts[37], this.GetReferenceSize(rowCounts, TableIndex.AssemblyRef), metadataTablesMemoryBlock, num);
			num += this.AssemblyRefOSTable.Block.Length;
			this.FileTable = new FileTableReader(rowCounts[38], stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.FileTable.Block.Length;
			this.ExportedTypeTable = new ExportedTypeTableReader(rowCounts[39], implementationRefSize, stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ExportedTypeTable.Block.Length;
			this.ManifestResourceTable = new ManifestResourceTableReader(rowCounts[40], implementationRefSize, stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ManifestResourceTable.Block.Length;
			this.NestedClassTable = new NestedClassTableReader(rowCounts[41], this.IsDeclaredSorted(TableMask.NestedClass), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), metadataTablesMemoryBlock, num);
			num += this.NestedClassTable.Block.Length;
			this.GenericParamTable = new GenericParamTableReader(rowCounts[42], this.IsDeclaredSorted(TableMask.GenericParam), typeOrMethodDefRefSize, stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.GenericParamTable.Block.Length;
			this.MethodSpecTable = new MethodSpecTableReader(rowCounts[43], methodDefOrRefRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.MethodSpecTable.Block.Length;
			this.GenericParamConstraintTable = new GenericParamConstraintTableReader(rowCounts[44], this.IsDeclaredSorted(TableMask.GenericParamConstraint), this.GetReferenceSize(rowCounts, TableIndex.GenericParam), typeDefOrRefRefSize, metadataTablesMemoryBlock, num);
			num += this.GenericParamConstraintTable.Block.Length;
			int[] rowCounts2 = (externalRowCountsOpt != null) ? MetadataReader.CombineRowCounts(rowCounts, externalRowCountsOpt, TableIndex.Document) : rowCounts;
			int referenceSize = this.GetReferenceSize(rowCounts2, TableIndex.MethodDef);
			int hasCustomDebugInformationRefSize = this.ComputeCodedTokenSize(2048, rowCounts2, TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint | TableMask.Document | TableMask.LocalScope | TableMask.LocalVariable | TableMask.LocalConstant | TableMask.ImportScope);
			this.DocumentTable = new DocumentTableReader(rowCounts[48], guidHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.DocumentTable.Block.Length;
			this.MethodDebugInformationTable = new MethodDebugInformationTableReader(rowCounts[49], this.GetReferenceSize(rowCounts, TableIndex.Document), blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.MethodDebugInformationTable.Block.Length;
			this.LocalScopeTable = new LocalScopeTableReader(rowCounts[50], this.IsDeclaredSorted(TableMask.LocalScope), referenceSize, this.GetReferenceSize(rowCounts, TableIndex.ImportScope), this.GetReferenceSize(rowCounts, TableIndex.LocalVariable), this.GetReferenceSize(rowCounts, TableIndex.LocalConstant), metadataTablesMemoryBlock, num);
			num += this.LocalScopeTable.Block.Length;
			this.LocalVariableTable = new LocalVariableTableReader(rowCounts[51], stringHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.LocalVariableTable.Block.Length;
			this.LocalConstantTable = new LocalConstantTableReader(rowCounts[52], stringHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.LocalConstantTable.Block.Length;
			this.ImportScopeTable = new ImportScopeTableReader(rowCounts[53], this.GetReferenceSize(rowCounts, TableIndex.ImportScope), blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.ImportScopeTable.Block.Length;
			this.StateMachineMethodTable = new StateMachineMethodTableReader(rowCounts[54], this.IsDeclaredSorted(TableMask.StateMachineMethod), referenceSize, metadataTablesMemoryBlock, num);
			num += this.StateMachineMethodTable.Block.Length;
			this.CustomDebugInformationTable = new CustomDebugInformationTableReader(rowCounts[55], this.IsDeclaredSorted(TableMask.CustomDebugInformation), hasCustomDebugInformationRefSize, guidHeapRefSize, blobHeapRefSize, metadataTablesMemoryBlock, num);
			num += this.CustomDebugInformationTable.Block.Length;
			if (num > metadataTablesMemoryBlock.Length)
			{
				throw new BadImageFormatException(SR.MetadataTablesTooSmall);
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000C384 File Offset: 0x0000A584
		private static int[] CombineRowCounts(int[] local, int[] external, TableIndex firstExternalTableIndex)
		{
			int[] array = new int[local.Length];
			for (int i = 0; i < (int)firstExternalTableIndex; i++)
			{
				array[i] = local[i];
			}
			for (int j = (int)firstExternalTableIndex; j < array.Length; j++)
			{
				array[j] = external[j];
			}
			return array;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		private int ComputeCodedTokenSize(int largeRowSize, int[] rowCounts, TableMask tablesReferenced)
		{
			if (this.IsMinimalDelta)
			{
				return 4;
			}
			bool flag = true;
			ulong num = (ulong)tablesReferenced;
			for (int i = 0; i < 56; i++)
			{
				if ((num & 1UL) != 0UL)
				{
					flag = (flag && rowCounts[i] < largeRowSize);
				}
				num >>= 1;
			}
			if (!flag)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000C409 File Offset: 0x0000A609
		private bool IsDeclaredSorted(TableMask index)
		{
			return (this._sortedTables & index) > (TableMask)0UL;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000C417 File Offset: 0x0000A617
		internal NamespaceCache NamespaceCache
		{
			get
			{
				return this.namespaceCache;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000C41F File Offset: 0x0000A61F
		internal bool UseFieldPtrTable
		{
			get
			{
				return this.FieldPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000C42F File Offset: 0x0000A62F
		internal bool UseMethodPtrTable
		{
			get
			{
				return this.MethodPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000C43F File Offset: 0x0000A63F
		internal bool UseParamPtrTable
		{
			get
			{
				return this.ParamPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000C44F File Offset: 0x0000A64F
		internal bool UseEventPtrTable
		{
			get
			{
				return this.EventPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000C45F File Offset: 0x0000A65F
		internal bool UsePropertyPtrTable
		{
			get
			{
				return this.PropertyPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000C470 File Offset: 0x0000A670
		internal void GetFieldRange(TypeDefinitionHandle typeDef, out int firstFieldRowId, out int lastFieldRowId)
		{
			int rowId = typeDef.RowId;
			firstFieldRowId = this.TypeDefTable.GetFieldStart(rowId);
			if (firstFieldRowId == 0)
			{
				firstFieldRowId = 1;
				lastFieldRowId = 0;
				return;
			}
			if (rowId == this.TypeDefTable.NumberOfRows)
			{
				lastFieldRowId = (this.UseFieldPtrTable ? this.FieldPtrTable.NumberOfRows : this.FieldTable.NumberOfRows);
				return;
			}
			lastFieldRowId = this.TypeDefTable.GetFieldStart(rowId + 1) - 1;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		internal void GetMethodRange(TypeDefinitionHandle typeDef, out int firstMethodRowId, out int lastMethodRowId)
		{
			int rowId = typeDef.RowId;
			firstMethodRowId = this.TypeDefTable.GetMethodStart(rowId);
			if (firstMethodRowId == 0)
			{
				firstMethodRowId = 1;
				lastMethodRowId = 0;
				return;
			}
			if (rowId == this.TypeDefTable.NumberOfRows)
			{
				lastMethodRowId = (this.UseMethodPtrTable ? this.MethodPtrTable.NumberOfRows : this.MethodDefTable.NumberOfRows);
				return;
			}
			lastMethodRowId = this.TypeDefTable.GetMethodStart(rowId + 1) - 1;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000C558 File Offset: 0x0000A758
		internal void GetEventRange(TypeDefinitionHandle typeDef, out int firstEventRowId, out int lastEventRowId)
		{
			int num = this.EventMapTable.FindEventMapRowIdFor(typeDef);
			if (num == 0)
			{
				firstEventRowId = 1;
				lastEventRowId = 0;
				return;
			}
			firstEventRowId = this.EventMapTable.GetEventListStartFor(num);
			if (num == this.EventMapTable.NumberOfRows)
			{
				lastEventRowId = (this.UseEventPtrTable ? this.EventPtrTable.NumberOfRows : this.EventTable.NumberOfRows);
				return;
			}
			lastEventRowId = this.EventMapTable.GetEventListStartFor(num + 1) - 1;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		internal void GetPropertyRange(TypeDefinitionHandle typeDef, out int firstPropertyRowId, out int lastPropertyRowId)
		{
			int num = this.PropertyMapTable.FindPropertyMapRowIdFor(typeDef);
			if (num == 0)
			{
				firstPropertyRowId = 1;
				lastPropertyRowId = 0;
				return;
			}
			firstPropertyRowId = this.PropertyMapTable.GetPropertyListStartFor(num);
			if (num == this.PropertyMapTable.NumberOfRows)
			{
				lastPropertyRowId = (this.UsePropertyPtrTable ? this.PropertyPtrTable.NumberOfRows : this.PropertyTable.NumberOfRows);
				return;
			}
			lastPropertyRowId = this.PropertyMapTable.GetPropertyListStartFor(num + 1) - 1;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000C648 File Offset: 0x0000A848
		internal void GetParameterRange(MethodDefinitionHandle methodDef, out int firstParamRowId, out int lastParamRowId)
		{
			int rowId = methodDef.RowId;
			firstParamRowId = this.MethodDefTable.GetParamStart(rowId);
			if (firstParamRowId == 0)
			{
				firstParamRowId = 1;
				lastParamRowId = 0;
				return;
			}
			if (rowId == this.MethodDefTable.NumberOfRows)
			{
				lastParamRowId = (this.UseParamPtrTable ? this.ParamPtrTable.NumberOfRows : this.ParamTable.NumberOfRows);
				return;
			}
			lastParamRowId = this.MethodDefTable.GetParamStart(rowId + 1) - 1;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		internal void GetLocalVariableRange(LocalScopeHandle scope, out int firstVariableRowId, out int lastVariableRowId)
		{
			int rowId = scope.RowId;
			firstVariableRowId = this.LocalScopeTable.GetVariableStart(rowId);
			if (firstVariableRowId == 0)
			{
				firstVariableRowId = 1;
				lastVariableRowId = 0;
				return;
			}
			if (rowId == this.LocalScopeTable.NumberOfRows)
			{
				lastVariableRowId = this.LocalVariableTable.NumberOfRows;
				return;
			}
			lastVariableRowId = this.LocalScopeTable.GetVariableStart(rowId + 1) - 1;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000C718 File Offset: 0x0000A918
		internal void GetLocalConstantRange(LocalScopeHandle scope, out int firstConstantRowId, out int lastConstantRowId)
		{
			int rowId = scope.RowId;
			firstConstantRowId = this.LocalScopeTable.GetConstantStart(rowId);
			if (firstConstantRowId == 0)
			{
				firstConstantRowId = 1;
				lastConstantRowId = 0;
				return;
			}
			if (rowId == this.LocalScopeTable.NumberOfRows)
			{
				lastConstantRowId = this.LocalConstantTable.NumberOfRows;
				return;
			}
			lastConstantRowId = this.LocalScopeTable.GetConstantStart(rowId + 1) - 1;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000C774 File Offset: 0x0000A974
		public MetadataReaderOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000C77C File Offset: 0x0000A97C
		public string MetadataVersion
		{
			get
			{
				return this._versionString;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000C784 File Offset: 0x0000A984
		public DebugMetadataHeader DebugMetadataHeader
		{
			get
			{
				return this._debugMetadataHeader;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000C78C File Offset: 0x0000A98C
		public MetadataKind MetadataKind
		{
			get
			{
				return this._metadataKind;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000C794 File Offset: 0x0000A994
		public MetadataStringComparer StringComparer
		{
			get
			{
				return new MetadataStringComparer(this);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000C79C File Offset: 0x0000A99C
		public bool IsAssembly
		{
			get
			{
				return this.AssemblyTable.NumberOfRows == 1;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public AssemblyReferenceHandleCollection AssemblyReferences
		{
			get
			{
				return new AssemblyReferenceHandleCollection(this);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public TypeDefinitionHandleCollection TypeDefinitions
		{
			get
			{
				return new TypeDefinitionHandleCollection(this.TypeDefTable.NumberOfRows);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		public TypeReferenceHandleCollection TypeReferences
		{
			get
			{
				return new TypeReferenceHandleCollection(this.TypeRefTable.NumberOfRows);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public CustomAttributeHandleCollection CustomAttributes
		{
			get
			{
				return new CustomAttributeHandleCollection(this);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public DeclarativeSecurityAttributeHandleCollection DeclarativeSecurityAttributes
		{
			get
			{
				return new DeclarativeSecurityAttributeHandleCollection(this);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public MemberReferenceHandleCollection MemberReferences
		{
			get
			{
				return new MemberReferenceHandleCollection(this.MemberRefTable.NumberOfRows);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000C7FA File Offset: 0x0000A9FA
		public ManifestResourceHandleCollection ManifestResources
		{
			get
			{
				return new ManifestResourceHandleCollection(this.ManifestResourceTable.NumberOfRows);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000C80C File Offset: 0x0000AA0C
		public AssemblyFileHandleCollection AssemblyFiles
		{
			get
			{
				return new AssemblyFileHandleCollection(this.FileTable.NumberOfRows);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000C81E File Offset: 0x0000AA1E
		public ExportedTypeHandleCollection ExportedTypes
		{
			get
			{
				return new ExportedTypeHandleCollection(this.ExportedTypeTable.NumberOfRows);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000C830 File Offset: 0x0000AA30
		public MethodDefinitionHandleCollection MethodDefinitions
		{
			get
			{
				return new MethodDefinitionHandleCollection(this);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000C838 File Offset: 0x0000AA38
		public FieldDefinitionHandleCollection FieldDefinitions
		{
			get
			{
				return new FieldDefinitionHandleCollection(this);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000C840 File Offset: 0x0000AA40
		public EventDefinitionHandleCollection EventDefinitions
		{
			get
			{
				return new EventDefinitionHandleCollection(this);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000C848 File Offset: 0x0000AA48
		public PropertyDefinitionHandleCollection PropertyDefinitions
		{
			get
			{
				return new PropertyDefinitionHandleCollection(this);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000C850 File Offset: 0x0000AA50
		public DocumentHandleCollection Documents
		{
			get
			{
				return new DocumentHandleCollection(this);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000C858 File Offset: 0x0000AA58
		public MethodDebugInformationHandleCollection MethodDebugInformation
		{
			get
			{
				return new MethodDebugInformationHandleCollection(this);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000C860 File Offset: 0x0000AA60
		public LocalScopeHandleCollection LocalScopes
		{
			get
			{
				return new LocalScopeHandleCollection(this, 0);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000C86C File Offset: 0x0000AA6C
		public LocalVariableHandleCollection LocalVariables
		{
			get
			{
				return new LocalVariableHandleCollection(this, default(LocalScopeHandle));
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000C888 File Offset: 0x0000AA88
		public LocalConstantHandleCollection LocalConstants
		{
			get
			{
				return new LocalConstantHandleCollection(this, default(LocalScopeHandle));
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public ImportScopeCollection ImportScopes
		{
			get
			{
				return new ImportScopeCollection(this);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		public CustomDebugInformationHandleCollection CustomDebugInformation
		{
			get
			{
				return new CustomDebugInformationHandleCollection(this);
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public AssemblyDefinition GetAssemblyDefinition()
		{
			if (!this.IsAssembly)
			{
				throw new InvalidOperationException(SR.MetadataImageDoesNotRepresentAnAssembly);
			}
			return new AssemblyDefinition(this);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000C8CF File Offset: 0x0000AACF
		public string GetString(StringHandle handle)
		{
			return this.StringStream.GetString(handle, this.utf8Decoder);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000C8E3 File Offset: 0x0000AAE3
		public string GetString(NamespaceDefinitionHandle handle)
		{
			if (handle.HasFullName)
			{
				return this.StringStream.GetString(handle.GetFullName(), this.utf8Decoder);
			}
			return this.namespaceCache.GetFullName(handle);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000C913 File Offset: 0x0000AB13
		public byte[] GetBlobBytes(BlobHandle handle)
		{
			return this.BlobStream.GetBytes(handle);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000C924 File Offset: 0x0000AB24
		public ImmutableArray<byte> GetBlobContent(BlobHandle handle)
		{
			byte[] blobBytes = this.GetBlobBytes(handle);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref blobBytes);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000C940 File Offset: 0x0000AB40
		public BlobReader GetBlobReader(BlobHandle handle)
		{
			return this.BlobStream.GetBlobReader(handle);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000C94E File Offset: 0x0000AB4E
		public string GetUserString(UserStringHandle handle)
		{
			return this.UserStringStream.GetString(handle);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public Guid GetGuid(GuidHandle handle)
		{
			return this.GuidStream.GetGuid(handle);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000C96A File Offset: 0x0000AB6A
		public ModuleDefinition GetModuleDefinition()
		{
			if (this._debugMetadataHeader != null)
			{
				throw new InvalidOperationException(SR.StandaloneDebugMetadataImageDoesNotContainModuleTable);
			}
			return new ModuleDefinition(this);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000C985 File Offset: 0x0000AB85
		public AssemblyReference GetAssemblyReference(AssemblyReferenceHandle handle)
		{
			return new AssemblyReference(this, handle.Value);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000C994 File Offset: 0x0000AB94
		public TypeDefinition GetTypeDefinition(TypeDefinitionHandle handle)
		{
			return new TypeDefinition(this, this.GetTypeDefTreatmentAndRowId(handle));
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000C9A3 File Offset: 0x0000ABA3
		public NamespaceDefinition GetNamespaceDefinitionRoot()
		{
			return new NamespaceDefinition(this.namespaceCache.GetRootNamespace());
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000C9B5 File Offset: 0x0000ABB5
		public NamespaceDefinition GetNamespaceDefinition(NamespaceDefinitionHandle handle)
		{
			return new NamespaceDefinition(this.namespaceCache.GetNamespaceData(handle));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		private uint GetTypeDefTreatmentAndRowId(TypeDefinitionHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateTypeDefTreatmentAndRowId(handle);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000C9E1 File Offset: 0x0000ABE1
		public TypeReference GetTypeReference(TypeReferenceHandle handle)
		{
			return new TypeReference(this, this.GetTypeRefTreatmentAndRowId(handle));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		private uint GetTypeRefTreatmentAndRowId(TypeReferenceHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateTypeRefTreatmentAndRowId(handle);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000CA09 File Offset: 0x0000AC09
		public ExportedType GetExportedType(ExportedTypeHandle handle)
		{
			return new ExportedType(this, handle.RowId);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000CA18 File Offset: 0x0000AC18
		public CustomAttributeHandleCollection GetCustomAttributes(EntityHandle handle)
		{
			return new CustomAttributeHandleCollection(this, handle);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000CA21 File Offset: 0x0000AC21
		public CustomAttribute GetCustomAttribute(CustomAttributeHandle handle)
		{
			return new CustomAttribute(this, this.GetCustomAttributeTreatmentAndRowId(handle));
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000CA30 File Offset: 0x0000AC30
		private uint GetCustomAttributeTreatmentAndRowId(CustomAttributeHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return MetadataReader.TreatmentAndRowId(1, handle.RowId);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000CA4F File Offset: 0x0000AC4F
		public DeclarativeSecurityAttribute GetDeclarativeSecurityAttribute(DeclarativeSecurityAttributeHandle handle)
		{
			return new DeclarativeSecurityAttribute(this, handle.RowId);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000CA5E File Offset: 0x0000AC5E
		public Constant GetConstant(ConstantHandle handle)
		{
			return new Constant(this, handle.RowId);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000CA6D File Offset: 0x0000AC6D
		public MethodDefinition GetMethodDefinition(MethodDefinitionHandle handle)
		{
			return new MethodDefinition(this, this.GetMethodDefTreatmentAndRowId(handle));
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		private uint GetMethodDefTreatmentAndRowId(MethodDefinitionHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateMethodDefTreatmentAndRowId(handle);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000CA95 File Offset: 0x0000AC95
		public FieldDefinition GetFieldDefinition(FieldDefinitionHandle handle)
		{
			return new FieldDefinition(this, this.GetFieldDefTreatmentAndRowId(handle));
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		private uint GetFieldDefTreatmentAndRowId(FieldDefinitionHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateFieldDefTreatmentAndRowId(handle);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000CABD File Offset: 0x0000ACBD
		public PropertyDefinition GetPropertyDefinition(PropertyDefinitionHandle handle)
		{
			return new PropertyDefinition(this, handle);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000CAC6 File Offset: 0x0000ACC6
		public EventDefinition GetEventDefinition(EventDefinitionHandle handle)
		{
			return new EventDefinition(this, handle);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000CACF File Offset: 0x0000ACCF
		public MethodImplementation GetMethodImplementation(MethodImplementationHandle handle)
		{
			return new MethodImplementation(this, handle);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public MemberReference GetMemberReference(MemberReferenceHandle handle)
		{
			return new MemberReference(this, this.GetMemberRefTreatmentAndRowId(handle));
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000CAE7 File Offset: 0x0000ACE7
		private uint GetMemberRefTreatmentAndRowId(MemberReferenceHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateMemberRefTreatmentAndRowId(handle);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public MethodSpecification GetMethodSpecification(MethodSpecificationHandle handle)
		{
			return new MethodSpecification(this, handle);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000CB09 File Offset: 0x0000AD09
		public Parameter GetParameter(ParameterHandle handle)
		{
			return new Parameter(this, handle);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000CB12 File Offset: 0x0000AD12
		public GenericParameter GetGenericParameter(GenericParameterHandle handle)
		{
			return new GenericParameter(this, handle);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000CB1B File Offset: 0x0000AD1B
		public GenericParameterConstraint GetGenericParameterConstraint(GenericParameterConstraintHandle handle)
		{
			return new GenericParameterConstraint(this, handle);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public ManifestResource GetManifestResource(ManifestResourceHandle handle)
		{
			return new ManifestResource(this, handle);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000CB2D File Offset: 0x0000AD2D
		public AssemblyFile GetAssemblyFile(AssemblyFileHandle handle)
		{
			return new AssemblyFile(this, handle);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000CB36 File Offset: 0x0000AD36
		public StandaloneSignature GetStandaloneSignature(StandaloneSignatureHandle handle)
		{
			return new StandaloneSignature(this, handle);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000CB3F File Offset: 0x0000AD3F
		public TypeSpecification GetTypeSpecification(TypeSpecificationHandle handle)
		{
			return new TypeSpecification(this, handle);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public ModuleReference GetModuleReference(ModuleReferenceHandle handle)
		{
			return new ModuleReference(this, handle);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000CB51 File Offset: 0x0000AD51
		public InterfaceImplementation GetInterfaceImplementation(InterfaceImplementationHandle handle)
		{
			return new InterfaceImplementation(this, handle);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		internal TypeDefinitionHandle GetDeclaringType(MethodDefinitionHandle methodDef)
		{
			int methodDefOrPtrRowId;
			if (this.UseMethodPtrTable)
			{
				methodDefOrPtrRowId = this.MethodPtrTable.GetRowIdForMethodDefRow(methodDef.RowId);
			}
			else
			{
				methodDefOrPtrRowId = methodDef.RowId;
			}
			return this.TypeDefTable.FindTypeContainingMethod(methodDefOrPtrRowId, this.MethodDefTable.NumberOfRows);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		internal TypeDefinitionHandle GetDeclaringType(FieldDefinitionHandle fieldDef)
		{
			int fieldDefOrPtrRowId;
			if (this.UseFieldPtrTable)
			{
				fieldDefOrPtrRowId = this.FieldPtrTable.GetRowIdForFieldDefRow(fieldDef.RowId);
			}
			else
			{
				fieldDefOrPtrRowId = fieldDef.RowId;
			}
			return this.TypeDefTable.FindTypeContainingField(fieldDefOrPtrRowId, this.FieldTable.NumberOfRows);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		public string GetString(DocumentNameBlobHandle handle)
		{
			return this.BlobStream.GetDocumentName(handle);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000CBFF File Offset: 0x0000ADFF
		public Document GetDocument(DocumentHandle handle)
		{
			return new Document(this, handle);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000CC08 File Offset: 0x0000AE08
		public MethodDebugInformation GetMethodDebugInformation(MethodDebugInformationHandle handle)
		{
			return new MethodDebugInformation(this, handle);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000CC11 File Offset: 0x0000AE11
		public MethodDebugInformation GetMethodDebugInformation(MethodDefinitionHandle handle)
		{
			return new MethodDebugInformation(this, MethodDebugInformationHandle.FromRowId(handle.RowId));
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000CC25 File Offset: 0x0000AE25
		public LocalScope GetLocalScope(LocalScopeHandle handle)
		{
			return new LocalScope(this, handle);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000CC2E File Offset: 0x0000AE2E
		public LocalVariable GetLocalVariable(LocalVariableHandle handle)
		{
			return new LocalVariable(this, handle);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public LocalConstant GetLocalConstant(LocalConstantHandle handle)
		{
			return new LocalConstant(this, handle);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000CC40 File Offset: 0x0000AE40
		public ImportScope GetImportScope(ImportScopeHandle handle)
		{
			return new ImportScope(this, handle);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000CC49 File Offset: 0x0000AE49
		public CustomDebugInformation GetCustomDebugInformation(CustomDebugInformationHandle handle)
		{
			return new CustomDebugInformation(this, handle);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000CC52 File Offset: 0x0000AE52
		public CustomDebugInformationHandleCollection GetCustomDebugInformation(EntityHandle handle)
		{
			return new CustomDebugInformationHandleCollection(this, handle);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000CC5B File Offset: 0x0000AE5B
		public LocalScopeHandleCollection GetLocalScopes(MethodDefinitionHandle handle)
		{
			return new LocalScopeHandleCollection(this, handle.RowId);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000CC6A File Offset: 0x0000AE6A
		public LocalScopeHandleCollection GetLocalScopes(MethodDebugInformationHandle handle)
		{
			return new LocalScopeHandleCollection(this, handle.RowId);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		private void InitializeNestedTypesMap()
		{
			Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>.Builder> dictionary = new Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>.Builder>();
			int numberOfRows = this.NestedClassTable.NumberOfRows;
			ImmutableArray<TypeDefinitionHandle>.Builder builder = null;
			TypeDefinitionHandle right = default(TypeDefinitionHandle);
			for (int i = 1; i <= numberOfRows; i++)
			{
				TypeDefinitionHandle enclosingClass = this.NestedClassTable.GetEnclosingClass(i);
				if (enclosingClass != right)
				{
					if (!dictionary.TryGetValue(enclosingClass, out builder))
					{
						builder = ImmutableArray.CreateBuilder<TypeDefinitionHandle>();
						dictionary.Add(enclosingClass, builder);
					}
					right = enclosingClass;
				}
				builder.Add(this.NestedClassTable.GetNestedClass(i));
			}
			Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>> dictionary2 = new Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>>();
			foreach (KeyValuePair<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>.Builder> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key, keyValuePair.Value.ToImmutable());
			}
			this._lazyNestedTypesMap = dictionary2;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000CD64 File Offset: 0x0000AF64
		internal ImmutableArray<TypeDefinitionHandle> GetNestedTypes(TypeDefinitionHandle typeDef)
		{
			if (this._lazyNestedTypesMap == null)
			{
				this.InitializeNestedTypesMap();
			}
			ImmutableArray<TypeDefinitionHandle> result;
			if (this._lazyNestedTypesMap.TryGetValue(typeDef, out result))
			{
				return result;
			}
			return ImmutableArray<TypeDefinitionHandle>.Empty;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000CD98 File Offset: 0x0000AF98
		private TypeDefTreatment GetWellKnownTypeDefinitionTreatment(TypeDefinitionHandle typeDef)
		{
			MetadataReader.InitializeProjectedTypes();
			StringHandle name = this.TypeDefTable.GetName(typeDef);
			int num = this.StringStream.BinarySearchRaw(MetadataReader.s_projectedTypeNames, name);
			if (num < 0)
			{
				return TypeDefTreatment.None;
			}
			StringHandle @namespace = this.TypeDefTable.GetNamespace(typeDef);
			if (this.StringStream.EqualsRaw(@namespace, this.StringStream.GetVirtualValue(MetadataReader.s_projectionInfos[num].ClrNamespace)))
			{
				return MetadataReader.s_projectionInfos[num].Treatment;
			}
			if (this.StringStream.EqualsRaw(@namespace, MetadataReader.s_projectionInfos[num].WinRTNamespace))
			{
				return MetadataReader.s_projectionInfos[num].Treatment | TypeDefTreatment.MarkInternalFlag;
			}
			return TypeDefTreatment.None;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000CE4C File Offset: 0x0000B04C
		private int GetProjectionIndexForTypeReference(TypeReferenceHandle typeRef, out bool isIDisposable)
		{
			MetadataReader.InitializeProjectedTypes();
			int num = this.StringStream.BinarySearchRaw(MetadataReader.s_projectedTypeNames, this.TypeRefTable.GetName(typeRef));
			if (num >= 0 && this.StringStream.EqualsRaw(this.TypeRefTable.GetNamespace(typeRef), MetadataReader.s_projectionInfos[num].WinRTNamespace))
			{
				isIDisposable = MetadataReader.s_projectionInfos[num].IsIDisposable;
				return num;
			}
			isIDisposable = false;
			return -1;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		internal static AssemblyReferenceHandle GetProjectedAssemblyRef(int projectionIndex)
		{
			return AssemblyReferenceHandle.FromVirtualIndex(MetadataReader.s_projectionInfos[projectionIndex].AssemblyRef);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000CED7 File Offset: 0x0000B0D7
		internal static StringHandle GetProjectedName(int projectionIndex)
		{
			return StringHandle.FromVirtualIndex(MetadataReader.s_projectionInfos[projectionIndex].ClrName);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000CEEE File Offset: 0x0000B0EE
		internal static StringHandle GetProjectedNamespace(int projectionIndex)
		{
			return StringHandle.FromVirtualIndex(MetadataReader.s_projectionInfos[projectionIndex].ClrNamespace);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000CF05 File Offset: 0x0000B105
		internal static TypeRefSignatureTreatment GetProjectedSignatureTreatment(int projectionIndex)
		{
			return MetadataReader.s_projectionInfos[projectionIndex].SignatureTreatment;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000CF18 File Offset: 0x0000B118
		private static void InitializeProjectedTypes()
		{
			if (MetadataReader.s_projectedTypeNames == null || MetadataReader.s_projectionInfos == null)
			{
				AssemblyReferenceHandle.VirtualIndex clrAssembly = AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime;
				AssemblyReferenceHandle.VirtualIndex clrAssembly2 = AssemblyReferenceHandle.VirtualIndex.System_Runtime;
				AssemblyReferenceHandle.VirtualIndex clrAssembly3 = AssemblyReferenceHandle.VirtualIndex.System_ObjectModel;
				AssemblyReferenceHandle.VirtualIndex clrAssembly4 = AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime_UI_Xaml;
				AssemblyReferenceHandle.VirtualIndex clrAssembly5 = AssemblyReferenceHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime;
				AssemblyReferenceHandle.VirtualIndex clrAssembly6 = AssemblyReferenceHandle.VirtualIndex.System_Numerics_Vectors;
				string[] array = new string[50];
				MetadataReader.ProjectionInfo[] array2 = new MetadataReader.ProjectionInfo[50];
				int num = 0;
				int num2 = 0;
				array[num++] = "AttributeTargets";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Metadata", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.AttributeTargets, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "AttributeUsageAttribute";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Metadata", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.AttributeUsageAttribute, clrAssembly2, TypeDefTreatment.RedirectedToClrAttribute, TypeRefSignatureTreatment.None, false);
				array[num++] = "Color";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI", StringHandle.VirtualIndex.Windows_UI, StringHandle.VirtualIndex.Color, clrAssembly, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "CornerRadius";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.CornerRadius, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "DateTime";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.DateTimeOffset, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Duration";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.Duration, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "DurationType";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.DurationType, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "EventHandler`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.EventHandler1, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "EventRegistrationToken";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime, StringHandle.VirtualIndex.EventRegistrationToken, clrAssembly5, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "GeneratorPosition";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Controls.Primitives", StringHandle.VirtualIndex.Windows_UI_Xaml_Controls_Primitives, StringHandle.VirtualIndex.GeneratorPosition, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "GridLength";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.GridLength, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "GridUnitType";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.GridUnitType, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "HResult";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Exception, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToClass, false);
				array[num++] = "IBindableIterable";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections, StringHandle.VirtualIndex.IEnumerable, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IBindableVector";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections, StringHandle.VirtualIndex.IList, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IClosable";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.IDisposable, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, true);
				array[num++] = "ICommand";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Input", StringHandle.VirtualIndex.System_Windows_Input, StringHandle.VirtualIndex.ICommand, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IIterable`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IEnumerable1, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IKeyValuePair`2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.KeyValuePair2, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToValueType, false);
				array[num++] = "IMapView`2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IReadOnlyDictionary2, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IMap`2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IDictionary2, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "INotifyCollectionChanged";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.INotifyCollectionChanged, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "INotifyPropertyChanged";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Data", StringHandle.VirtualIndex.System_ComponentModel, StringHandle.VirtualIndex.INotifyPropertyChanged, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IReference`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Nullable1, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToValueType, false);
				array[num++] = "IVectorView`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IReadOnlyList1, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IVector`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IList1, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "KeyTime";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Animation", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Animation, StringHandle.VirtualIndex.KeyTime, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media", StringHandle.VirtualIndex.Windows_UI_Xaml_Media, StringHandle.VirtualIndex.Matrix, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix3D";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Media3D", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Media3D, StringHandle.VirtualIndex.Matrix3D, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix3x2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Matrix3x2, clrAssembly6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix4x4";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Matrix4x4, clrAssembly6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "NotifyCollectionChangedAction";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.NotifyCollectionChangedAction, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "NotifyCollectionChangedEventArgs";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.NotifyCollectionChangedEventArgs, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "NotifyCollectionChangedEventHandler";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.NotifyCollectionChangedEventHandler, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Plane";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Plane, clrAssembly6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Point";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.Windows_Foundation, StringHandle.VirtualIndex.Point, clrAssembly, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "PropertyChangedEventArgs";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Data", StringHandle.VirtualIndex.System_ComponentModel, StringHandle.VirtualIndex.PropertyChangedEventArgs, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "PropertyChangedEventHandler";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Data", StringHandle.VirtualIndex.System_ComponentModel, StringHandle.VirtualIndex.PropertyChangedEventHandler, clrAssembly3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Quaternion";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Quaternion, clrAssembly6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Rect";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.Windows_Foundation, StringHandle.VirtualIndex.Rect, clrAssembly, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "RepeatBehavior";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Animation", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Animation, StringHandle.VirtualIndex.RepeatBehavior, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "RepeatBehaviorType";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Animation", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Animation, StringHandle.VirtualIndex.RepeatBehaviorType, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Size";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.Windows_Foundation, StringHandle.VirtualIndex.Size, clrAssembly, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Thickness";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.Thickness, clrAssembly4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "TimeSpan";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.TimeSpan, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "TypeName";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Type, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToClass, false);
				array[num++] = "Uri";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Uri, clrAssembly2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Vector2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Vector2, clrAssembly6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Vector3";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Vector3, clrAssembly6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Vector4";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Vector4, clrAssembly6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				MetadataReader.s_projectedTypeNames = array;
				MetadataReader.s_projectionInfos = array2;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000D868 File Offset: 0x0000BA68
		[Conditional("DEBUG")]
		private static void AssertSorted(string[] keys)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000D885 File Offset: 0x0000BA85
		internal static string[] GetProjectedTypeNames()
		{
			MetadataReader.InitializeProjectedTypes();
			return MetadataReader.s_projectedTypeNames;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000D891 File Offset: 0x0000BA91
		private static uint TreatmentAndRowId(byte treatment, int rowId)
		{
			return (uint)((int)treatment << 24 | rowId);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000D89C File Offset: 0x0000BA9C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal uint CalculateTypeDefTreatmentAndRowId(TypeDefinitionHandle handle)
		{
			TypeAttributes flags = this.TypeDefTable.GetFlags(handle);
			EntityHandle extends = this.TypeDefTable.GetExtends(handle);
			TypeDefTreatment typeDefTreatment;
			if ((flags & TypeAttributes.WindowsRuntime) != TypeAttributes.NotPublic)
			{
				if (this._metadataKind == MetadataKind.WindowsMetadata)
				{
					typeDefTreatment = this.GetWellKnownTypeDefinitionTreatment(handle);
					if (typeDefTreatment != TypeDefTreatment.None)
					{
						return MetadataReader.TreatmentAndRowId((byte)typeDefTreatment, handle.RowId);
					}
					if (extends.Kind == HandleKind.TypeReference && this.IsSystemAttribute((TypeReferenceHandle)extends))
					{
						typeDefTreatment = TypeDefTreatment.NormalAttribute;
					}
					else
					{
						typeDefTreatment = TypeDefTreatment.NormalNonAttribute;
					}
				}
				else if (this._metadataKind == MetadataKind.ManagedWindowsMetadata && this.NeedsWinRTPrefix(flags, extends))
				{
					typeDefTreatment = TypeDefTreatment.PrefixWinRTName;
				}
				else
				{
					typeDefTreatment = TypeDefTreatment.None;
				}
				if ((typeDefTreatment == TypeDefTreatment.PrefixWinRTName || typeDefTreatment == TypeDefTreatment.NormalNonAttribute) && (flags & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && this.HasAttribute(handle, "Windows.UI.Xaml", "TreatAsAbstractComposableClassAttribute"))
				{
					typeDefTreatment |= TypeDefTreatment.MarkAbstractFlag;
				}
			}
			else if (this._metadataKind == MetadataKind.ManagedWindowsMetadata && this.IsClrImplementationType(handle))
			{
				typeDefTreatment = TypeDefTreatment.UnmangleWinRTName;
			}
			else
			{
				typeDefTreatment = TypeDefTreatment.None;
			}
			return MetadataReader.TreatmentAndRowId((byte)typeDefTreatment, handle.RowId);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000D97C File Offset: 0x0000BB7C
		private bool IsClrImplementationType(TypeDefinitionHandle typeDef)
		{
			return (this.TypeDefTable.GetFlags(typeDef) & (TypeAttributes.VisibilityMask | TypeAttributes.SpecialName)) == TypeAttributes.SpecialName && this.StringStream.StartsWithRaw(this.TypeDefTable.GetName(typeDef), "<CLR>");
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		internal uint CalculateTypeRefTreatmentAndRowId(TypeReferenceHandle handle)
		{
			bool flag;
			int projectionIndexForTypeReference = this.GetProjectionIndexForTypeReference(handle, out flag);
			if (projectionIndexForTypeReference >= 0)
			{
				return MetadataReader.TreatmentAndRowId(3, projectionIndexForTypeReference);
			}
			return MetadataReader.TreatmentAndRowId((byte)this.GetSpecialTypeRefTreatment(handle), handle.RowId);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		private TypeRefTreatment GetSpecialTypeRefTreatment(TypeReferenceHandle handle)
		{
			if (this.StringStream.EqualsRaw(this.TypeRefTable.GetNamespace(handle), "System"))
			{
				StringHandle name = this.TypeRefTable.GetName(handle);
				if (this.StringStream.EqualsRaw(name, "MulticastDelegate"))
				{
					return TypeRefTreatment.SystemDelegate;
				}
				if (this.StringStream.EqualsRaw(name, "Attribute"))
				{
					return TypeRefTreatment.SystemAttribute;
				}
			}
			return TypeRefTreatment.None;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000DA53 File Offset: 0x0000BC53
		private bool IsSystemAttribute(TypeReferenceHandle handle)
		{
			return this.StringStream.EqualsRaw(this.TypeRefTable.GetNamespace(handle), "System") && this.StringStream.EqualsRaw(this.TypeRefTable.GetName(handle), "Attribute");
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000DA91 File Offset: 0x0000BC91
		private bool IsSystemEnum(TypeReferenceHandle handle)
		{
			return this.StringStream.EqualsRaw(this.TypeRefTable.GetNamespace(handle), "System") && this.StringStream.EqualsRaw(this.TypeRefTable.GetName(handle), "Enum");
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		private bool NeedsWinRTPrefix(TypeAttributes flags, EntityHandle extends)
		{
			if ((flags & (TypeAttributes.VisibilityMask | TypeAttributes.ClassSemanticsMask)) != TypeAttributes.Public)
			{
				return false;
			}
			if (extends.Kind != HandleKind.TypeReference)
			{
				return false;
			}
			TypeReferenceHandle handle = (TypeReferenceHandle)extends;
			if (this.StringStream.EqualsRaw(this.TypeRefTable.GetNamespace(handle), "System"))
			{
				StringHandle name = this.TypeRefTable.GetName(handle);
				if (this.StringStream.EqualsRaw(name, "MulticastDelegate") || this.StringStream.EqualsRaw(name, "ValueType") || this.StringStream.EqualsRaw(name, "Attribute"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000DB60 File Offset: 0x0000BD60
		private uint CalculateMethodDefTreatmentAndRowId(MethodDefinitionHandle methodDef)
		{
			MethodDefTreatment methodDefTreatment = MethodDefTreatment.Implementation;
			TypeDefinitionHandle declaringType = this.GetDeclaringType(methodDef);
			TypeAttributes flags = this.TypeDefTable.GetFlags(declaringType);
			if ((flags & TypeAttributes.WindowsRuntime) != TypeAttributes.NotPublic)
			{
				if (this.IsClrImplementationType(declaringType))
				{
					methodDefTreatment = MethodDefTreatment.Implementation;
				}
				else if (flags.IsNested())
				{
					methodDefTreatment = MethodDefTreatment.Implementation;
				}
				else if ((flags & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					methodDefTreatment = MethodDefTreatment.InterfaceMethod;
				}
				else if (this._metadataKind == MetadataKind.ManagedWindowsMetadata && (flags & TypeAttributes.Public) == TypeAttributes.NotPublic)
				{
					methodDefTreatment = MethodDefTreatment.Implementation;
				}
				else
				{
					methodDefTreatment = MethodDefTreatment.Other;
					EntityHandle extends = this.TypeDefTable.GetExtends(declaringType);
					if (extends.Kind == HandleKind.TypeReference)
					{
						TypeRefTreatment specialTypeRefTreatment = this.GetSpecialTypeRefTreatment((TypeReferenceHandle)extends);
						if (specialTypeRefTreatment != TypeRefTreatment.SystemDelegate)
						{
							if (specialTypeRefTreatment == TypeRefTreatment.SystemAttribute)
							{
								methodDefTreatment = MethodDefTreatment.AttributeMethod;
							}
						}
						else
						{
							methodDefTreatment = (MethodDefTreatment.DelegateMethod | MethodDefTreatment.MarkPublicFlag);
						}
					}
				}
			}
			if (methodDefTreatment == MethodDefTreatment.Other)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				foreach (MethodImplementationHandle handle in new MethodImplementationHandleCollection(this, declaringType))
				{
					MethodImplementation methodImplementation = this.GetMethodImplementation(handle);
					if (methodImplementation.MethodBody == methodDef)
					{
						EntityHandle methodDeclaration = methodImplementation.MethodDeclaration;
						if (methodDeclaration.Kind == HandleKind.MemberReference && this.ImplementsRedirectedInterface((MemberReferenceHandle)methodDeclaration, out flag3))
						{
							flag = true;
							if (flag3)
							{
								break;
							}
						}
						else
						{
							flag2 = true;
						}
					}
				}
				if (flag3)
				{
					methodDefTreatment = MethodDefTreatment.DisposeMethod;
				}
				else if (flag && !flag2)
				{
					methodDefTreatment = MethodDefTreatment.HiddenInterfaceImplementation;
				}
			}
			if (methodDefTreatment == MethodDefTreatment.Other)
			{
				methodDefTreatment |= this.GetMethodTreatmentFromCustomAttributes(methodDef);
			}
			return MetadataReader.TreatmentAndRowId((byte)methodDefTreatment, methodDef.RowId);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000DCCC File Offset: 0x0000BECC
		private MethodDefTreatment GetMethodTreatmentFromCustomAttributes(MethodDefinitionHandle methodDef)
		{
			MethodDefTreatment methodDefTreatment = MethodDefTreatment.None;
			foreach (CustomAttributeHandle caHandle in this.GetCustomAttributes(methodDef))
			{
				StringHandle rawHandle;
				StringHandle rawHandle2;
				if (this.GetAttributeTypeNameRaw(caHandle, out rawHandle, out rawHandle2) && this.StringStream.EqualsRaw(rawHandle, "Windows.UI.Xaml"))
				{
					if (this.StringStream.EqualsRaw(rawHandle2, "TreatAsPublicMethodAttribute"))
					{
						methodDefTreatment |= MethodDefTreatment.MarkPublicFlag;
					}
					if (this.StringStream.EqualsRaw(rawHandle2, "TreatAsAbstractMethodAttribute"))
					{
						methodDefTreatment |= MethodDefTreatment.MarkAbstractFlag;
					}
				}
			}
			return methodDefTreatment;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000DD78 File Offset: 0x0000BF78
		private uint CalculateFieldDefTreatmentAndRowId(FieldDefinitionHandle handle)
		{
			bool flags = this.FieldTable.GetFlags(handle) != FieldAttributes.PrivateScope;
			FieldDefTreatment treatment = FieldDefTreatment.None;
			if (((flags ? 1 : 0) & 1024) != 0 && this.StringStream.EqualsRaw(this.FieldTable.GetName(handle), "value__"))
			{
				TypeDefinitionHandle declaringType = this.GetDeclaringType(handle);
				EntityHandle extends = this.TypeDefTable.GetExtends(declaringType);
				if (extends.Kind == HandleKind.TypeReference)
				{
					TypeReferenceHandle handle2 = (TypeReferenceHandle)extends;
					if (this.StringStream.EqualsRaw(this.TypeRefTable.GetName(handle2), "Enum") && this.StringStream.EqualsRaw(this.TypeRefTable.GetNamespace(handle2), "System"))
					{
						treatment = FieldDefTreatment.EnumValue;
					}
				}
			}
			return MetadataReader.TreatmentAndRowId((byte)treatment, handle.RowId);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000DE30 File Offset: 0x0000C030
		private uint CalculateMemberRefTreatmentAndRowId(MemberReferenceHandle handle)
		{
			bool flag;
			MemberRefTreatment treatment;
			if (this.ImplementsRedirectedInterface(handle, out flag) && flag)
			{
				treatment = MemberRefTreatment.Dispose;
			}
			else
			{
				treatment = MemberRefTreatment.None;
			}
			return MetadataReader.TreatmentAndRowId((byte)treatment, handle.RowId);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000DE60 File Offset: 0x0000C060
		private bool ImplementsRedirectedInterface(MemberReferenceHandle memberRef, out bool isIDisposable)
		{
			isIDisposable = false;
			EntityHandle @class = this.MemberRefTable.GetClass(memberRef);
			TypeReferenceHandle typeRef;
			if (@class.Kind == HandleKind.TypeReference)
			{
				typeRef = (TypeReferenceHandle)@class;
			}
			else
			{
				if (@class.Kind != HandleKind.TypeSpecification)
				{
					return false;
				}
				BlobHandle signature = this.TypeSpecTable.GetSignature((TypeSpecificationHandle)@class);
				BlobReader blobReader = new BlobReader(this.BlobStream.GetMemoryBlock(signature));
				if (blobReader.Length < 2 || blobReader.ReadByte() != 21 || blobReader.ReadByte() != 18)
				{
					return false;
				}
				EntityHandle handle = blobReader.ReadTypeHandle();
				if (handle.Kind != HandleKind.TypeReference)
				{
					return false;
				}
				typeRef = (TypeReferenceHandle)handle;
			}
			return this.GetProjectionIndexForTypeReference(typeRef, out isIDisposable) >= 0;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000DF14 File Offset: 0x0000C114
		private int FindMscorlibAssemblyRefNoProjection()
		{
			for (int i = 1; i <= this.AssemblyRefTable.NumberOfNonVirtualRows; i++)
			{
				if (this.StringStream.EqualsRaw(this.AssemblyRefTable.GetName(i), "mscorlib"))
				{
					return i;
				}
			}
			throw new BadImageFormatException(SR.WinMDMissingMscorlibRef);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000DF64 File Offset: 0x0000C164
		internal CustomAttributeValueTreatment CalculateCustomAttributeValueTreatment(CustomAttributeHandle handle)
		{
			EntityHandle parent = this.CustomAttributeTable.GetParent(handle);
			if (!this.IsWindowsAttributeUsageAttribute(parent, handle))
			{
				return CustomAttributeValueTreatment.None;
			}
			TypeDefinitionHandle handle2 = (TypeDefinitionHandle)parent;
			if (this.StringStream.EqualsRaw(this.TypeDefTable.GetNamespace(handle2), "Windows.Foundation.Metadata"))
			{
				if (this.StringStream.EqualsRaw(this.TypeDefTable.GetName(handle2), "VersionAttribute"))
				{
					return CustomAttributeValueTreatment.AttributeUsageVersionAttribute;
				}
				if (this.StringStream.EqualsRaw(this.TypeDefTable.GetName(handle2), "DeprecatedAttribute"))
				{
					return CustomAttributeValueTreatment.AttributeUsageDeprecatedAttribute;
				}
			}
			if (!this.HasAttribute(handle2, "Windows.Foundation.Metadata", "AllowMultipleAttribute"))
			{
				return CustomAttributeValueTreatment.AttributeUsageAllowSingle;
			}
			return CustomAttributeValueTreatment.AttributeUsageAllowMultiple;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000E00C File Offset: 0x0000C20C
		private bool IsWindowsAttributeUsageAttribute(EntityHandle targetType, CustomAttributeHandle attributeHandle)
		{
			if (targetType.Kind != HandleKind.TypeDefinition)
			{
				return false;
			}
			EntityHandle constructor = this.CustomAttributeTable.GetConstructor(attributeHandle);
			if (constructor.Kind != HandleKind.MemberReference)
			{
				return false;
			}
			EntityHandle @class = this.MemberRefTable.GetClass((MemberReferenceHandle)constructor);
			if (@class.Kind != HandleKind.TypeReference)
			{
				return false;
			}
			TypeReferenceHandle handle = (TypeReferenceHandle)@class;
			return this.StringStream.EqualsRaw(this.TypeRefTable.GetName(handle), "AttributeUsageAttribute") && this.StringStream.EqualsRaw(this.TypeRefTable.GetNamespace(handle), "Windows.Foundation.Metadata");
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		private bool HasAttribute(EntityHandle token, string asciiNamespaceName, string asciiTypeName)
		{
			foreach (CustomAttributeHandle caHandle in this.GetCustomAttributes(token))
			{
				StringHandle rawHandle;
				StringHandle rawHandle2;
				if (this.GetAttributeTypeNameRaw(caHandle, out rawHandle, out rawHandle2) && this.StringStream.EqualsRaw(rawHandle2, asciiTypeName) && this.StringStream.EqualsRaw(rawHandle, asciiNamespaceName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000E128 File Offset: 0x0000C328
		private bool GetAttributeTypeNameRaw(CustomAttributeHandle caHandle, out StringHandle namespaceName, out StringHandle typeName)
		{
			namespaceName = (typeName = default(StringHandle));
			EntityHandle attributeTypeRaw = this.GetAttributeTypeRaw(caHandle);
			if (attributeTypeRaw.IsNil)
			{
				return false;
			}
			if (attributeTypeRaw.Kind == HandleKind.TypeReference)
			{
				TypeReferenceHandle handle = (TypeReferenceHandle)attributeTypeRaw;
				EntityHandle resolutionScope = this.TypeRefTable.GetResolutionScope(handle);
				if (!resolutionScope.IsNil && resolutionScope.Kind == HandleKind.TypeReference)
				{
					return false;
				}
				typeName = this.TypeRefTable.GetName(handle);
				namespaceName = this.TypeRefTable.GetNamespace(handle);
			}
			else
			{
				if (attributeTypeRaw.Kind != HandleKind.TypeDefinition)
				{
					return false;
				}
				TypeDefinitionHandle handle2 = (TypeDefinitionHandle)attributeTypeRaw;
				if (this.TypeDefTable.GetFlags(handle2).IsNested())
				{
					return false;
				}
				typeName = this.TypeDefTable.GetName(handle2);
				namespaceName = this.TypeDefTable.GetNamespace(handle2);
			}
			return true;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000E20C File Offset: 0x0000C40C
		private EntityHandle GetAttributeTypeRaw(CustomAttributeHandle handle)
		{
			EntityHandle constructor = this.CustomAttributeTable.GetConstructor(handle);
			if (constructor.Kind == HandleKind.MethodDefinition)
			{
				return this.GetDeclaringType((MethodDefinitionHandle)constructor);
			}
			if (constructor.Kind == HandleKind.MemberReference)
			{
				EntityHandle @class = this.MemberRefTable.GetClass((MemberReferenceHandle)constructor);
				HandleKind kind = @class.Kind;
				if (kind == HandleKind.TypeReference || kind == HandleKind.TypeDefinition)
				{
					return @class;
				}
			}
			return default(EntityHandle);
		}

		// Token: 0x04000356 RID: 854
		private readonly MetadataReaderOptions _options;

		// Token: 0x04000357 RID: 855
		internal readonly MetadataStringDecoder utf8Decoder;

		// Token: 0x04000358 RID: 856
		internal readonly NamespaceCache namespaceCache;

		// Token: 0x04000359 RID: 857
		private Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>> _lazyNestedTypesMap;

		// Token: 0x0400035A RID: 858
		internal readonly MemoryBlock Block;

		// Token: 0x0400035B RID: 859
		internal readonly int WinMDMscorlibRef;

		// Token: 0x0400035C RID: 860
		private readonly string _versionString;

		// Token: 0x0400035D RID: 861
		private readonly MetadataKind _metadataKind;

		// Token: 0x0400035E RID: 862
		private readonly MetadataStreamKind _metadataStreamKind;

		// Token: 0x0400035F RID: 863
		private readonly DebugMetadataHeader _debugMetadataHeader;

		// Token: 0x04000360 RID: 864
		internal StringStreamReader StringStream;

		// Token: 0x04000361 RID: 865
		internal BlobStreamReader BlobStream;

		// Token: 0x04000362 RID: 866
		internal GuidStreamReader GuidStream;

		// Token: 0x04000363 RID: 867
		internal UserStringStreamReader UserStringStream;

		// Token: 0x04000364 RID: 868
		internal bool IsMinimalDelta;

		// Token: 0x04000365 RID: 869
		private readonly TableMask _sortedTables;

		// Token: 0x04000366 RID: 870
		internal int[] TableRowCounts;

		// Token: 0x04000367 RID: 871
		internal ModuleTableReader ModuleTable;

		// Token: 0x04000368 RID: 872
		internal TypeRefTableReader TypeRefTable;

		// Token: 0x04000369 RID: 873
		internal TypeDefTableReader TypeDefTable;

		// Token: 0x0400036A RID: 874
		internal FieldPtrTableReader FieldPtrTable;

		// Token: 0x0400036B RID: 875
		internal FieldTableReader FieldTable;

		// Token: 0x0400036C RID: 876
		internal MethodPtrTableReader MethodPtrTable;

		// Token: 0x0400036D RID: 877
		internal MethodTableReader MethodDefTable;

		// Token: 0x0400036E RID: 878
		internal ParamPtrTableReader ParamPtrTable;

		// Token: 0x0400036F RID: 879
		internal ParamTableReader ParamTable;

		// Token: 0x04000370 RID: 880
		internal InterfaceImplTableReader InterfaceImplTable;

		// Token: 0x04000371 RID: 881
		internal MemberRefTableReader MemberRefTable;

		// Token: 0x04000372 RID: 882
		internal ConstantTableReader ConstantTable;

		// Token: 0x04000373 RID: 883
		internal CustomAttributeTableReader CustomAttributeTable;

		// Token: 0x04000374 RID: 884
		internal FieldMarshalTableReader FieldMarshalTable;

		// Token: 0x04000375 RID: 885
		internal DeclSecurityTableReader DeclSecurityTable;

		// Token: 0x04000376 RID: 886
		internal ClassLayoutTableReader ClassLayoutTable;

		// Token: 0x04000377 RID: 887
		internal FieldLayoutTableReader FieldLayoutTable;

		// Token: 0x04000378 RID: 888
		internal StandAloneSigTableReader StandAloneSigTable;

		// Token: 0x04000379 RID: 889
		internal EventMapTableReader EventMapTable;

		// Token: 0x0400037A RID: 890
		internal EventPtrTableReader EventPtrTable;

		// Token: 0x0400037B RID: 891
		internal EventTableReader EventTable;

		// Token: 0x0400037C RID: 892
		internal PropertyMapTableReader PropertyMapTable;

		// Token: 0x0400037D RID: 893
		internal PropertyPtrTableReader PropertyPtrTable;

		// Token: 0x0400037E RID: 894
		internal PropertyTableReader PropertyTable;

		// Token: 0x0400037F RID: 895
		internal MethodSemanticsTableReader MethodSemanticsTable;

		// Token: 0x04000380 RID: 896
		internal MethodImplTableReader MethodImplTable;

		// Token: 0x04000381 RID: 897
		internal ModuleRefTableReader ModuleRefTable;

		// Token: 0x04000382 RID: 898
		internal TypeSpecTableReader TypeSpecTable;

		// Token: 0x04000383 RID: 899
		internal ImplMapTableReader ImplMapTable;

		// Token: 0x04000384 RID: 900
		internal FieldRVATableReader FieldRvaTable;

		// Token: 0x04000385 RID: 901
		internal EnCLogTableReader EncLogTable;

		// Token: 0x04000386 RID: 902
		internal EnCMapTableReader EncMapTable;

		// Token: 0x04000387 RID: 903
		internal AssemblyTableReader AssemblyTable;

		// Token: 0x04000388 RID: 904
		internal AssemblyProcessorTableReader AssemblyProcessorTable;

		// Token: 0x04000389 RID: 905
		internal AssemblyOSTableReader AssemblyOSTable;

		// Token: 0x0400038A RID: 906
		internal AssemblyRefTableReader AssemblyRefTable;

		// Token: 0x0400038B RID: 907
		internal AssemblyRefProcessorTableReader AssemblyRefProcessorTable;

		// Token: 0x0400038C RID: 908
		internal AssemblyRefOSTableReader AssemblyRefOSTable;

		// Token: 0x0400038D RID: 909
		internal FileTableReader FileTable;

		// Token: 0x0400038E RID: 910
		internal ExportedTypeTableReader ExportedTypeTable;

		// Token: 0x0400038F RID: 911
		internal ManifestResourceTableReader ManifestResourceTable;

		// Token: 0x04000390 RID: 912
		internal NestedClassTableReader NestedClassTable;

		// Token: 0x04000391 RID: 913
		internal GenericParamTableReader GenericParamTable;

		// Token: 0x04000392 RID: 914
		internal MethodSpecTableReader MethodSpecTable;

		// Token: 0x04000393 RID: 915
		internal GenericParamConstraintTableReader GenericParamConstraintTable;

		// Token: 0x04000394 RID: 916
		internal DocumentTableReader DocumentTable;

		// Token: 0x04000395 RID: 917
		internal MethodDebugInformationTableReader MethodDebugInformationTable;

		// Token: 0x04000396 RID: 918
		internal LocalScopeTableReader LocalScopeTable;

		// Token: 0x04000397 RID: 919
		internal LocalVariableTableReader LocalVariableTable;

		// Token: 0x04000398 RID: 920
		internal LocalConstantTableReader LocalConstantTable;

		// Token: 0x04000399 RID: 921
		internal ImportScopeTableReader ImportScopeTable;

		// Token: 0x0400039A RID: 922
		internal StateMachineMethodTableReader StateMachineMethodTable;

		// Token: 0x0400039B RID: 923
		internal CustomDebugInformationTableReader CustomDebugInformationTable;

		// Token: 0x0400039C RID: 924
		private const int SmallIndexSize = 2;

		// Token: 0x0400039D RID: 925
		private const int LargeIndexSize = 4;

		// Token: 0x0400039E RID: 926
		private static readonly ObjectPool<StringBuilder> s_stringBuilderPool = new ObjectPool<StringBuilder>(() => new StringBuilder());

		// Token: 0x0400039F RID: 927
		internal const string ClrPrefix = "<CLR>";

		// Token: 0x040003A0 RID: 928
		internal static readonly byte[] WinRTPrefix = new byte[]
		{
			60,
			87,
			105,
			110,
			82,
			84,
			62
		};

		// Token: 0x040003A1 RID: 929
		private static string[] s_projectedTypeNames;

		// Token: 0x040003A2 RID: 930
		private static MetadataReader.ProjectionInfo[] s_projectionInfos;

		// Token: 0x02000187 RID: 391
		private struct ProjectionInfo
		{
			// Token: 0x06000BE0 RID: 3040 RVA: 0x00021655 File Offset: 0x0001F855
			public ProjectionInfo(string winRtNamespace, StringHandle.VirtualIndex clrNamespace, StringHandle.VirtualIndex clrName, AssemblyReferenceHandle.VirtualIndex clrAssembly, TypeDefTreatment treatment = TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment signatureTreatment = TypeRefSignatureTreatment.None, bool isIDisposable = false)
			{
				this.WinRTNamespace = winRtNamespace;
				this.ClrNamespace = clrNamespace;
				this.ClrName = clrName;
				this.AssemblyRef = clrAssembly;
				this.Treatment = treatment;
				this.SignatureTreatment = signatureTreatment;
				this.IsIDisposable = isIDisposable;
			}

			// Token: 0x040009F7 RID: 2551
			public readonly string WinRTNamespace;

			// Token: 0x040009F8 RID: 2552
			public readonly StringHandle.VirtualIndex ClrNamespace;

			// Token: 0x040009F9 RID: 2553
			public readonly StringHandle.VirtualIndex ClrName;

			// Token: 0x040009FA RID: 2554
			public readonly AssemblyReferenceHandle.VirtualIndex AssemblyRef;

			// Token: 0x040009FB RID: 2555
			public readonly TypeDefTreatment Treatment;

			// Token: 0x040009FC RID: 2556
			public readonly TypeRefSignatureTreatment SignatureTreatment;

			// Token: 0x040009FD RID: 2557
			public readonly bool IsIDisposable;
		}
	}
}
