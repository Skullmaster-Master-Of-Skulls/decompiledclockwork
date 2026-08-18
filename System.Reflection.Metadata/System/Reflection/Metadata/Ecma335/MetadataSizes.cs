using System;
using System.Collections.Immutable;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000B8 RID: 184
	internal sealed class MetadataSizes
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00014C2F File Offset: 0x00012E2F
		public bool IsMetadataTableStreamCompressed
		{
			get
			{
				return !this.IsMinimalDelta;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00014C3C File Offset: 0x00012E3C
		public MetadataSizes(ImmutableArray<int> rowCounts, ImmutableArray<int> externalRowCounts, ImmutableArray<int> heapSizes, bool isMinimalDelta, bool isStandaloneDebugMetadata)
		{
			this.RowCounts = rowCounts;
			this.ExternalRowCounts = externalRowCounts;
			this.HeapSizes = heapSizes;
			this.IsMinimalDelta = isMinimalDelta;
			this.BlobIndexSize = ((isMinimalDelta || heapSizes[2] > 65535) ? 4 : 2);
			this.StringIndexSize = ((isMinimalDelta || heapSizes[1] > 65535) ? 4 : 2);
			this.GuidIndexSize = ((isMinimalDelta || heapSizes[3] > 65535) ? 4 : 2);
			this.PresentTablesMask = MetadataSizes.ComputeNonEmptyTableMask(rowCounts);
			this.ExternalTablesMask = MetadataSizes.ComputeNonEmptyTableMask(externalRowCounts);
			this.CustomAttributeTypeCodedIndexSize = this.GetReferenceByteSize(3, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.MemberRef
			});
			this.DeclSecurityCodedIndexSize = this.GetReferenceByteSize(2, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.TypeDef
			});
			this.EventDefIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.Event
			});
			this.FieldDefIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.Field
			});
			this.GenericParamIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.GenericParam
			});
			this.HasConstantCodedIndexSize = this.GetReferenceByteSize(2, new TableIndex[]
			{
				TableIndex.Field,
				TableIndex.Param,
				TableIndex.Property
			});
			this.HasCustomAttributeCodedIndexSize = this.GetReferenceByteSize(5, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.Field,
				TableIndex.TypeRef,
				TableIndex.TypeDef,
				TableIndex.Param,
				TableIndex.InterfaceImpl,
				TableIndex.MemberRef,
				TableIndex.Module,
				TableIndex.DeclSecurity,
				TableIndex.Property,
				TableIndex.Event,
				TableIndex.StandAloneSig,
				TableIndex.ModuleRef,
				TableIndex.TypeSpec,
				TableIndex.Assembly,
				TableIndex.AssemblyRef,
				TableIndex.File,
				TableIndex.ExportedType,
				TableIndex.ManifestResource,
				TableIndex.GenericParam,
				TableIndex.GenericParamConstraint,
				TableIndex.MethodSpec
			});
			this.HasFieldMarshalCodedIndexSize = this.GetReferenceByteSize(1, new TableIndex[]
			{
				TableIndex.Field,
				TableIndex.Param
			});
			this.HasSemanticsCodedIndexSize = this.GetReferenceByteSize(1, new TableIndex[]
			{
				TableIndex.Event,
				TableIndex.Property
			});
			this.ImplementationCodedIndexSize = this.GetReferenceByteSize(2, new TableIndex[]
			{
				TableIndex.File,
				TableIndex.AssemblyRef,
				TableIndex.ExportedType
			});
			this.MemberForwardedCodedIndexSize = this.GetReferenceByteSize(1, new TableIndex[]
			{
				TableIndex.Field,
				TableIndex.MethodDef
			});
			this.MemberRefParentCodedIndexSize = this.GetReferenceByteSize(3, new TableIndex[]
			{
				TableIndex.TypeDef,
				TableIndex.TypeRef,
				TableIndex.ModuleRef,
				TableIndex.MethodDef,
				TableIndex.TypeSpec
			});
			this.MethodDefIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.MethodDef
			});
			this.MethodDefOrRefCodedIndexSize = this.GetReferenceByteSize(1, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.MemberRef
			});
			this.ModuleRefIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.ModuleRef
			});
			this.ParameterIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.Param
			});
			this.PropertyDefIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.Property
			});
			this.ResolutionScopeCodedIndexSize = this.GetReferenceByteSize(2, new TableIndex[]
			{
				TableIndex.Module,
				TableIndex.ModuleRef,
				TableIndex.AssemblyRef,
				TableIndex.TypeRef
			});
			this.TypeDefIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.TypeDef
			});
			this.TypeDefOrRefCodedIndexSize = this.GetReferenceByteSize(2, new TableIndex[]
			{
				TableIndex.TypeDef,
				TableIndex.TypeRef,
				TableIndex.TypeSpec
			});
			this.TypeOrMethodDefCodedIndexSize = this.GetReferenceByteSize(1, new TableIndex[]
			{
				TableIndex.TypeDef,
				TableIndex.MethodDef
			});
			this.DocumentIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.Document
			});
			this.LocalVariableIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.LocalVariable
			});
			this.LocalConstantIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.LocalConstant
			});
			this.ImportScopeIndexSize = this.GetReferenceByteSize(0, new TableIndex[]
			{
				TableIndex.ImportScope
			});
			this.HasCustomDebugInformationSize = this.GetReferenceByteSize(5, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.Field,
				TableIndex.TypeRef,
				TableIndex.TypeDef,
				TableIndex.Param,
				TableIndex.InterfaceImpl,
				TableIndex.MemberRef,
				TableIndex.Module,
				TableIndex.DeclSecurity,
				TableIndex.Property,
				TableIndex.Event,
				TableIndex.StandAloneSig,
				TableIndex.ModuleRef,
				TableIndex.TypeSpec,
				TableIndex.Assembly,
				TableIndex.AssemblyRef,
				TableIndex.File,
				TableIndex.ExportedType,
				TableIndex.ManifestResource,
				TableIndex.GenericParam,
				TableIndex.GenericParamConstraint,
				TableIndex.MethodSpec,
				TableIndex.Document,
				TableIndex.LocalScope,
				TableIndex.LocalVariable,
				TableIndex.LocalConstant,
				TableIndex.ImportScope
			});
			int num = this.CalculateTableStreamHeaderSize();
			num += this.GetTableSize(TableIndex.Module, (int)(2 + 3 * this.GuidIndexSize + this.StringIndexSize));
			num += this.GetTableSize(TableIndex.TypeRef, (int)(this.ResolutionScopeCodedIndexSize + this.StringIndexSize + this.StringIndexSize));
			num += this.GetTableSize(TableIndex.TypeDef, (int)(4 + this.StringIndexSize + this.StringIndexSize + this.TypeDefOrRefCodedIndexSize + this.FieldDefIndexSize + this.MethodDefIndexSize));
			num += this.GetTableSize(TableIndex.Field, (int)(2 + this.StringIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.MethodDef, (int)(8 + this.StringIndexSize + this.BlobIndexSize + this.ParameterIndexSize));
			num += this.GetTableSize(TableIndex.Param, (int)(4 + this.StringIndexSize));
			num += this.GetTableSize(TableIndex.InterfaceImpl, (int)(this.TypeDefIndexSize + this.TypeDefOrRefCodedIndexSize));
			num += this.GetTableSize(TableIndex.MemberRef, (int)(this.MemberRefParentCodedIndexSize + this.StringIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.Constant, (int)(2 + this.HasConstantCodedIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.CustomAttribute, (int)(this.HasCustomAttributeCodedIndexSize + this.CustomAttributeTypeCodedIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.FieldMarshal, (int)(this.HasFieldMarshalCodedIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.DeclSecurity, (int)(2 + this.DeclSecurityCodedIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.ClassLayout, (int)(6 + this.TypeDefIndexSize));
			num += this.GetTableSize(TableIndex.FieldLayout, (int)(4 + this.FieldDefIndexSize));
			num += this.GetTableSize(TableIndex.StandAloneSig, (int)this.BlobIndexSize);
			num += this.GetTableSize(TableIndex.EventMap, (int)(this.TypeDefIndexSize + this.EventDefIndexSize));
			num += this.GetTableSize(TableIndex.Event, (int)(2 + this.StringIndexSize + this.TypeDefOrRefCodedIndexSize));
			num += this.GetTableSize(TableIndex.PropertyMap, (int)(this.TypeDefIndexSize + this.PropertyDefIndexSize));
			num += this.GetTableSize(TableIndex.Property, (int)(2 + this.StringIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.MethodSemantics, (int)(2 + this.MethodDefIndexSize + this.HasSemanticsCodedIndexSize));
			num += this.GetTableSize(TableIndex.MethodImpl, (int)(0 + this.TypeDefIndexSize + this.MethodDefOrRefCodedIndexSize + this.MethodDefOrRefCodedIndexSize));
			num += this.GetTableSize(TableIndex.ModuleRef, (int)(0 + this.StringIndexSize));
			num += this.GetTableSize(TableIndex.TypeSpec, (int)(0 + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.ImplMap, (int)(2 + this.MemberForwardedCodedIndexSize + this.StringIndexSize + this.ModuleRefIndexSize));
			num += this.GetTableSize(TableIndex.FieldRva, (int)(4 + this.FieldDefIndexSize));
			num += this.GetTableSize(TableIndex.EncLog, 8);
			num += this.GetTableSize(TableIndex.EncMap, 4);
			num += this.GetTableSize(TableIndex.Assembly, (int)(16 + this.BlobIndexSize + this.StringIndexSize + this.StringIndexSize));
			num += this.GetTableSize(TableIndex.AssemblyRef, (int)(12 + this.BlobIndexSize + this.StringIndexSize + this.StringIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.File, (int)(4 + this.StringIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.ExportedType, (int)(8 + this.StringIndexSize + this.StringIndexSize + this.ImplementationCodedIndexSize));
			num += this.GetTableSize(TableIndex.ManifestResource, (int)(8 + this.StringIndexSize + this.ImplementationCodedIndexSize));
			num += this.GetTableSize(TableIndex.NestedClass, (int)(this.TypeDefIndexSize + this.TypeDefIndexSize));
			num += this.GetTableSize(TableIndex.GenericParam, (int)(4 + this.TypeOrMethodDefCodedIndexSize + this.StringIndexSize));
			num += this.GetTableSize(TableIndex.MethodSpec, (int)(this.MethodDefOrRefCodedIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.GenericParamConstraint, (int)(this.GenericParamIndexSize + this.TypeDefOrRefCodedIndexSize));
			num += this.GetTableSize(TableIndex.Document, (int)(this.BlobIndexSize + this.GuidIndexSize + this.BlobIndexSize + this.GuidIndexSize));
			num += this.GetTableSize(TableIndex.MethodDebugInformation, (int)(this.DocumentIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.LocalScope, (int)(this.MethodDefIndexSize + this.ImportScopeIndexSize + this.LocalVariableIndexSize + this.LocalConstantIndexSize + 4 + 4));
			num += this.GetTableSize(TableIndex.LocalVariable, (int)(4 + this.StringIndexSize));
			num += this.GetTableSize(TableIndex.LocalConstant, (int)(this.StringIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.ImportScope, (int)(this.ImportScopeIndexSize + this.BlobIndexSize));
			num += this.GetTableSize(TableIndex.StateMachineMethod, (int)(this.MethodDefIndexSize + this.MethodDefIndexSize));
			num += this.GetTableSize(TableIndex.CustomDebugInformation, (int)(this.HasCustomDebugInformationSize + this.GuidIndexSize + this.BlobIndexSize));
			num = BitArithmetic.Align(num + 1, 4);
			this.MetadataTableStreamSize = num;
			num += this.GetAlignedHeapSize(HeapIndex.String);
			num += this.GetAlignedHeapSize(HeapIndex.UserString);
			num += this.GetAlignedHeapSize(HeapIndex.Guid);
			num += this.GetAlignedHeapSize(HeapIndex.Blob);
			this.StandalonePdbStreamSize = (isStandaloneDebugMetadata ? this.CalculateStandalonePdbStreamSize() : 0);
			num += this.StandalonePdbStreamSize;
			this.MetadataStreamStorageSize = num;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001546F File Offset: 0x0001366F
		public bool IsStandaloneDebugMetadata
		{
			get
			{
				return this.StandalonePdbStreamSize > 0;
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001547A File Offset: 0x0001367A
		public bool IsPresent(TableIndex table)
		{
			return (this.PresentTablesMask & 1UL << (int)table) > 0UL;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001548E File Offset: 0x0001368E
		public int MetadataHeaderSize
		{
			get
			{
				return 32 + (this.IsStandaloneDebugMetadata ? 16 : 0) + 76 + (this.IsMinimalDelta ? 16 : 0);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000154B1 File Offset: 0x000136B1
		public static int GetMetadataStreamHeaderSize(string streamName)
		{
			return 8 + BitArithmetic.Align(streamName.Length + 1, 4);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x000154C3 File Offset: 0x000136C3
		public int MetadataSize
		{
			get
			{
				return this.MetadataHeaderSize + this.MetadataStreamStorageSize;
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000154D4 File Offset: 0x000136D4
		public int GetAlignedHeapSize(HeapIndex index)
		{
			return BitArithmetic.Align(this.HeapSizes[(int)index], 4);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000154F8 File Offset: 0x000136F8
		internal int CalculateTableStreamHeaderSize()
		{
			int num = 24;
			for (int i = 0; i < this.RowCounts.Length; i++)
			{
				if ((1UL << i & this.PresentTablesMask) != 0UL)
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00015536 File Offset: 0x00013736
		internal int CalculateStandalonePdbStreamSize()
		{
			return 32 + BitArithmetic.CountBits(this.ExternalTablesMask) * 4;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00015548 File Offset: 0x00013748
		private static ulong ComputeNonEmptyTableMask(ImmutableArray<int> rowCounts)
		{
			ulong num = 0UL;
			for (int i = 0; i < rowCounts.Length; i++)
			{
				if (rowCounts[i] > 0)
				{
					num |= 1UL << i;
				}
			}
			return num;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00015580 File Offset: 0x00013780
		private int GetTableSize(TableIndex index, int rowSize)
		{
			return this.RowCounts[(int)index] * rowSize;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001559E File Offset: 0x0001379E
		private byte GetReferenceByteSize(int tagBitSize, params TableIndex[] tables)
		{
			if (this.IsMetadataTableStreamCompressed && this.ReferenceFits(16 - tagBitSize, tables))
			{
				return 2;
			}
			return 4;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000155B8 File Offset: 0x000137B8
		private bool ReferenceFits(int bitCount, TableIndex[] tables)
		{
			int num = (1 << bitCount) - 1;
			foreach (TableIndex index in tables)
			{
				if (this.RowCounts[(int)index] + this.ExternalRowCounts[(int)index] > num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040004C2 RID: 1218
		private const int StreamAlignment = 4;

		// Token: 0x040004C3 RID: 1219
		public const ulong DebugMetadataTablesMask = 71776119061217280UL;

		// Token: 0x040004C4 RID: 1220
		public const ulong SortedDebugTables = 55169095435288576UL;

		// Token: 0x040004C5 RID: 1221
		public readonly bool IsMinimalDelta;

		// Token: 0x040004C6 RID: 1222
		public readonly byte BlobIndexSize;

		// Token: 0x040004C7 RID: 1223
		public readonly byte StringIndexSize;

		// Token: 0x040004C8 RID: 1224
		public readonly byte GuidIndexSize;

		// Token: 0x040004C9 RID: 1225
		public readonly byte CustomAttributeTypeCodedIndexSize;

		// Token: 0x040004CA RID: 1226
		public readonly byte DeclSecurityCodedIndexSize;

		// Token: 0x040004CB RID: 1227
		public readonly byte EventDefIndexSize;

		// Token: 0x040004CC RID: 1228
		public readonly byte FieldDefIndexSize;

		// Token: 0x040004CD RID: 1229
		public readonly byte GenericParamIndexSize;

		// Token: 0x040004CE RID: 1230
		public readonly byte HasConstantCodedIndexSize;

		// Token: 0x040004CF RID: 1231
		public readonly byte HasCustomAttributeCodedIndexSize;

		// Token: 0x040004D0 RID: 1232
		public readonly byte HasFieldMarshalCodedIndexSize;

		// Token: 0x040004D1 RID: 1233
		public readonly byte HasSemanticsCodedIndexSize;

		// Token: 0x040004D2 RID: 1234
		public readonly byte ImplementationCodedIndexSize;

		// Token: 0x040004D3 RID: 1235
		public readonly byte MemberForwardedCodedIndexSize;

		// Token: 0x040004D4 RID: 1236
		public readonly byte MemberRefParentCodedIndexSize;

		// Token: 0x040004D5 RID: 1237
		public readonly byte MethodDefIndexSize;

		// Token: 0x040004D6 RID: 1238
		public readonly byte MethodDefOrRefCodedIndexSize;

		// Token: 0x040004D7 RID: 1239
		public readonly byte ModuleRefIndexSize;

		// Token: 0x040004D8 RID: 1240
		public readonly byte ParameterIndexSize;

		// Token: 0x040004D9 RID: 1241
		public readonly byte PropertyDefIndexSize;

		// Token: 0x040004DA RID: 1242
		public readonly byte ResolutionScopeCodedIndexSize;

		// Token: 0x040004DB RID: 1243
		public readonly byte TypeDefIndexSize;

		// Token: 0x040004DC RID: 1244
		public readonly byte TypeDefOrRefCodedIndexSize;

		// Token: 0x040004DD RID: 1245
		public readonly byte TypeOrMethodDefCodedIndexSize;

		// Token: 0x040004DE RID: 1246
		public readonly byte DocumentIndexSize;

		// Token: 0x040004DF RID: 1247
		public readonly byte LocalVariableIndexSize;

		// Token: 0x040004E0 RID: 1248
		public readonly byte LocalConstantIndexSize;

		// Token: 0x040004E1 RID: 1249
		public readonly byte ImportScopeIndexSize;

		// Token: 0x040004E2 RID: 1250
		public readonly byte HasCustomDebugInformationSize;

		// Token: 0x040004E3 RID: 1251
		public readonly ImmutableArray<int> RowCounts;

		// Token: 0x040004E4 RID: 1252
		public readonly ImmutableArray<int> ExternalRowCounts;

		// Token: 0x040004E5 RID: 1253
		public readonly ulong PresentTablesMask;

		// Token: 0x040004E6 RID: 1254
		public readonly ulong ExternalTablesMask;

		// Token: 0x040004E7 RID: 1255
		public readonly ImmutableArray<int> HeapSizes;

		// Token: 0x040004E8 RID: 1256
		public readonly int MetadataStreamStorageSize;

		// Token: 0x040004E9 RID: 1257
		public readonly int MetadataTableStreamSize;

		// Token: 0x040004EA RID: 1258
		public readonly int StandalonePdbStreamSize;

		// Token: 0x040004EB RID: 1259
		public const int MetadataVersionPaddedLength = 12;

		// Token: 0x040004EC RID: 1260
		internal const int PdbIdSize = 20;
	}
}
