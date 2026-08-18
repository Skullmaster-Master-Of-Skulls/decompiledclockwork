using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000C1 RID: 193
	public static class MetadataTokens
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0001609F File Offset: 0x0001429F
		public static int GetRowNumber(this MetadataReader reader, EntityHandle handle)
		{
			if (handle.IsVirtual)
			{
				return MetadataTokens.MapVirtualHandleRowId(reader, handle);
			}
			return handle.RowId;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000160BE File Offset: 0x000142BE
		public static int GetHeapOffset(this MetadataReader reader, Handle handle)
		{
			if (!handle.IsHeapHandle)
			{
				Throw.HeapHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return MetadataTokens.MapVirtualHandleRowId(reader, handle);
			}
			return handle.Offset;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000160E6 File Offset: 0x000142E6
		public static int GetToken(this MetadataReader reader, EntityHandle handle)
		{
			if (handle.IsVirtual)
			{
				return (int)(handle.Type | (uint)MetadataTokens.MapVirtualHandleRowId(reader, handle));
			}
			return handle.Token;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001610D File Offset: 0x0001430D
		public static int GetToken(this MetadataReader reader, Handle handle)
		{
			if (!handle.IsEntityOrUserStringHandle)
			{
				Throw.EntityOrUserStringHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return (int)(handle.EntityHandleType | (uint)MetadataTokens.MapVirtualHandleRowId(reader, handle));
			}
			return handle.Token;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00016140 File Offset: 0x00014340
		private static int MapVirtualHandleRowId(MetadataReader reader, Handle handle)
		{
			HandleKind kind = handle.Kind;
			if (kind == HandleKind.AssemblyReference)
			{
				return reader.AssemblyRefTable.NumberOfNonVirtualRows + 1 + handle.RowId;
			}
			if (kind != HandleKind.Blob && kind != HandleKind.String)
			{
				throw new ArgumentException(SR.InvalidHandle, "handle");
			}
			throw new NotSupportedException(SR.CantGetOffsetForVirtualHeapHandle);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00016196 File Offset: 0x00014396
		public static int GetRowNumber(EntityHandle handle)
		{
			if (!handle.IsVirtual)
			{
				return handle.RowId;
			}
			return -1;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000161AA File Offset: 0x000143AA
		public static int GetHeapOffset(Handle handle)
		{
			if (!handle.IsHeapHandle)
			{
				Throw.HeapHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return -1;
			}
			return handle.Offset;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000161CC File Offset: 0x000143CC
		public static int GetToken(Handle handle)
		{
			if (!handle.IsEntityOrUserStringHandle)
			{
				Throw.EntityOrUserStringHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return 0;
			}
			return handle.Token;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000161EE File Offset: 0x000143EE
		public static int GetToken(EntityHandle handle)
		{
			if (!handle.IsVirtual)
			{
				return handle.Token;
			}
			return 0;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00016202 File Offset: 0x00014402
		public static bool TryGetTableIndex(HandleKind type, out TableIndex index)
		{
			if (type < (HandleKind)56)
			{
				index = (TableIndex)type;
				return true;
			}
			index = TableIndex.Module;
			return false;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00016212 File Offset: 0x00014412
		public static bool TryGetHeapIndex(HandleKind type, out HeapIndex index)
		{
			switch (type)
			{
			case HandleKind.UserString:
				index = HeapIndex.UserString;
				return true;
			case HandleKind.Blob:
				index = HeapIndex.Blob;
				return true;
			case HandleKind.Guid:
				index = HeapIndex.Guid;
				return true;
			default:
				if (type != HandleKind.String && type != HandleKind.NamespaceDefinition)
				{
					index = HeapIndex.UserString;
					return false;
				}
				index = HeapIndex.String;
				return true;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001624D File Offset: 0x0001444D
		public static Handle Handle(int token)
		{
			if (!TokenTypeIds.IsEntityOrUserStringToken((uint)token))
			{
				Throw.InvalidToken();
			}
			return System.Reflection.Metadata.Handle.FromVToken((uint)token);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00016262 File Offset: 0x00014462
		public static EntityHandle EntityHandle(int token)
		{
			if (!TokenTypeIds.IsEntityToken((uint)token))
			{
				Throw.InvalidToken();
			}
			return new EntityHandle((uint)token);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00016277 File Offset: 0x00014477
		public static EntityHandle EntityHandle(TableIndex tableIndex, int rowNumber)
		{
			return MetadataTokens.Handle(tableIndex, rowNumber);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00016280 File Offset: 0x00014480
		public static EntityHandle Handle(TableIndex tableIndex, int rowNumber)
		{
			int vToken = (int)((int)tableIndex << 24) | rowNumber;
			if (!TokenTypeIds.IsEntityOrUserStringToken((uint)vToken))
			{
				Throw.TableIndexOutOfRange();
			}
			return new EntityHandle((uint)vToken);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001629A File Offset: 0x0001449A
		private static int ToRowId(int rowNumber)
		{
			return rowNumber & 16777215;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000162A3 File Offset: 0x000144A3
		public static MethodDefinitionHandle MethodDefinitionHandle(int rowNumber)
		{
			return System.Reflection.Metadata.MethodDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000162B0 File Offset: 0x000144B0
		public static MethodImplementationHandle MethodImplementationHandle(int rowNumber)
		{
			return System.Reflection.Metadata.MethodImplementationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000162BD File Offset: 0x000144BD
		public static MethodSpecificationHandle MethodSpecificationHandle(int rowNumber)
		{
			return System.Reflection.Metadata.MethodSpecificationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000162CA File Offset: 0x000144CA
		public static TypeDefinitionHandle TypeDefinitionHandle(int rowNumber)
		{
			return System.Reflection.Metadata.TypeDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000162D7 File Offset: 0x000144D7
		public static ExportedTypeHandle ExportedTypeHandle(int rowNumber)
		{
			return System.Reflection.Metadata.ExportedTypeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000162E4 File Offset: 0x000144E4
		public static TypeReferenceHandle TypeReferenceHandle(int rowNumber)
		{
			return System.Reflection.Metadata.TypeReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000162F1 File Offset: 0x000144F1
		public static TypeSpecificationHandle TypeSpecificationHandle(int rowNumber)
		{
			return System.Reflection.Metadata.TypeSpecificationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000162FE File Offset: 0x000144FE
		public static InterfaceImplementationHandle InterfaceImplementationHandle(int rowNumber)
		{
			return System.Reflection.Metadata.InterfaceImplementationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001630B File Offset: 0x0001450B
		public static MemberReferenceHandle MemberReferenceHandle(int rowNumber)
		{
			return System.Reflection.Metadata.MemberReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00016318 File Offset: 0x00014518
		public static FieldDefinitionHandle FieldDefinitionHandle(int rowNumber)
		{
			return System.Reflection.Metadata.FieldDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00016325 File Offset: 0x00014525
		public static EventDefinitionHandle EventDefinitionHandle(int rowNumber)
		{
			return System.Reflection.Metadata.EventDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00016332 File Offset: 0x00014532
		public static PropertyDefinitionHandle PropertyDefinitionHandle(int rowNumber)
		{
			return System.Reflection.Metadata.PropertyDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001633F File Offset: 0x0001453F
		public static StandaloneSignatureHandle StandaloneSignatureHandle(int rowNumber)
		{
			return System.Reflection.Metadata.StandaloneSignatureHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001634C File Offset: 0x0001454C
		public static ParameterHandle ParameterHandle(int rowNumber)
		{
			return System.Reflection.Metadata.ParameterHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00016359 File Offset: 0x00014559
		public static GenericParameterHandle GenericParameterHandle(int rowNumber)
		{
			return System.Reflection.Metadata.GenericParameterHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00016366 File Offset: 0x00014566
		public static GenericParameterConstraintHandle GenericParameterConstraintHandle(int rowNumber)
		{
			return System.Reflection.Metadata.GenericParameterConstraintHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00016373 File Offset: 0x00014573
		public static ModuleReferenceHandle ModuleReferenceHandle(int rowNumber)
		{
			return System.Reflection.Metadata.ModuleReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00016380 File Offset: 0x00014580
		public static AssemblyReferenceHandle AssemblyReferenceHandle(int rowNumber)
		{
			return System.Reflection.Metadata.AssemblyReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001638D File Offset: 0x0001458D
		public static CustomAttributeHandle CustomAttributeHandle(int rowNumber)
		{
			return System.Reflection.Metadata.CustomAttributeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001639A File Offset: 0x0001459A
		public static DeclarativeSecurityAttributeHandle DeclarativeSecurityAttributeHandle(int rowNumber)
		{
			return System.Reflection.Metadata.DeclarativeSecurityAttributeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000163A7 File Offset: 0x000145A7
		public static ConstantHandle ConstantHandle(int rowNumber)
		{
			return System.Reflection.Metadata.ConstantHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000163B4 File Offset: 0x000145B4
		public static ManifestResourceHandle ManifestResourceHandle(int rowNumber)
		{
			return System.Reflection.Metadata.ManifestResourceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000163C1 File Offset: 0x000145C1
		public static AssemblyFileHandle AssemblyFileHandle(int rowNumber)
		{
			return System.Reflection.Metadata.AssemblyFileHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000163CE File Offset: 0x000145CE
		public static DocumentHandle DocumentHandle(int rowNumber)
		{
			return System.Reflection.Metadata.DocumentHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000163DB File Offset: 0x000145DB
		public static MethodDebugInformationHandle MethodDebugInformationHandle(int rowNumber)
		{
			return System.Reflection.Metadata.MethodDebugInformationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000163E8 File Offset: 0x000145E8
		public static LocalScopeHandle LocalScopeHandle(int rowNumber)
		{
			return System.Reflection.Metadata.LocalScopeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000163F5 File Offset: 0x000145F5
		public static LocalVariableHandle LocalVariableHandle(int rowNumber)
		{
			return System.Reflection.Metadata.LocalVariableHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00016402 File Offset: 0x00014602
		public static LocalConstantHandle LocalConstantHandle(int rowNumber)
		{
			return System.Reflection.Metadata.LocalConstantHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001640F File Offset: 0x0001460F
		public static ImportScopeHandle ImportScopeHandle(int rowNumber)
		{
			return System.Reflection.Metadata.ImportScopeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001641C File Offset: 0x0001461C
		public static CustomDebugInformationHandle CustomDebugInformationHandle(int rowNumber)
		{
			return System.Reflection.Metadata.CustomDebugInformationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00016429 File Offset: 0x00014629
		public static UserStringHandle UserStringHandle(int offset)
		{
			return System.Reflection.Metadata.UserStringHandle.FromOffset(offset & 16777215);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00016437 File Offset: 0x00014637
		public static StringHandle StringHandle(int offset)
		{
			return System.Reflection.Metadata.StringHandle.FromOffset(offset);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001643F File Offset: 0x0001463F
		public static BlobHandle BlobHandle(int offset)
		{
			return System.Reflection.Metadata.BlobHandle.FromOffset(offset);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00016447 File Offset: 0x00014647
		public static GuidHandle GuidHandle(int offset)
		{
			return System.Reflection.Metadata.GuidHandle.FromIndex(offset);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001644F File Offset: 0x0001464F
		public static DocumentNameBlobHandle DocumentNameBlobHandle(int offset)
		{
			return System.Reflection.Metadata.DocumentNameBlobHandle.FromOffset(offset);
		}

		// Token: 0x04000502 RID: 1282
		public static readonly int TableCount = 56;

		// Token: 0x04000503 RID: 1283
		public static readonly int HeapCount = 4;
	}
}
