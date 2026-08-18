using System;
using System.Collections.Generic;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000C0 RID: 192
	public static class MetadataReaderExtensions
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x00015808 File Offset: 0x00013A08
		public static int GetTableRowCount(this MetadataReader reader, TableIndex tableIndex)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (tableIndex >= (TableIndex)56)
			{
				Throw.TableIndexOutOfRange();
			}
			return reader.TableRowCounts[(int)tableIndex];
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001582C File Offset: 0x00013A2C
		public static int GetTableRowSize(this MetadataReader reader, TableIndex tableIndex)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			switch (tableIndex)
			{
			case TableIndex.Module:
				return reader.ModuleTable.RowSize;
			case TableIndex.TypeRef:
				return reader.TypeRefTable.RowSize;
			case TableIndex.TypeDef:
				return reader.TypeDefTable.RowSize;
			case TableIndex.FieldPtr:
				return reader.FieldPtrTable.RowSize;
			case TableIndex.Field:
				return reader.FieldTable.RowSize;
			case TableIndex.MethodPtr:
				return reader.MethodPtrTable.RowSize;
			case TableIndex.MethodDef:
				return reader.MethodDefTable.RowSize;
			case TableIndex.ParamPtr:
				return reader.ParamPtrTable.RowSize;
			case TableIndex.Param:
				return reader.ParamTable.RowSize;
			case TableIndex.InterfaceImpl:
				return reader.InterfaceImplTable.RowSize;
			case TableIndex.MemberRef:
				return reader.MemberRefTable.RowSize;
			case TableIndex.Constant:
				return reader.ConstantTable.RowSize;
			case TableIndex.CustomAttribute:
				return reader.CustomAttributeTable.RowSize;
			case TableIndex.FieldMarshal:
				return reader.FieldMarshalTable.RowSize;
			case TableIndex.DeclSecurity:
				return reader.DeclSecurityTable.RowSize;
			case TableIndex.ClassLayout:
				return reader.ClassLayoutTable.RowSize;
			case TableIndex.FieldLayout:
				return reader.FieldLayoutTable.RowSize;
			case TableIndex.StandAloneSig:
				return reader.StandAloneSigTable.RowSize;
			case TableIndex.EventMap:
				return reader.EventMapTable.RowSize;
			case TableIndex.EventPtr:
				return reader.EventPtrTable.RowSize;
			case TableIndex.Event:
				return reader.EventTable.RowSize;
			case TableIndex.PropertyMap:
				return reader.PropertyMapTable.RowSize;
			case TableIndex.PropertyPtr:
				return reader.PropertyPtrTable.RowSize;
			case TableIndex.Property:
				return reader.PropertyTable.RowSize;
			case TableIndex.MethodSemantics:
				return reader.MethodSemanticsTable.RowSize;
			case TableIndex.MethodImpl:
				return reader.MethodImplTable.RowSize;
			case TableIndex.ModuleRef:
				return reader.ModuleRefTable.RowSize;
			case TableIndex.TypeSpec:
				return reader.TypeSpecTable.RowSize;
			case TableIndex.ImplMap:
				return reader.ImplMapTable.RowSize;
			case TableIndex.FieldRva:
				return reader.FieldRvaTable.RowSize;
			case TableIndex.EncLog:
				return reader.EncLogTable.RowSize;
			case TableIndex.EncMap:
				return reader.EncMapTable.RowSize;
			case TableIndex.Assembly:
				return reader.AssemblyTable.RowSize;
			case TableIndex.AssemblyProcessor:
				return reader.AssemblyProcessorTable.RowSize;
			case TableIndex.AssemblyOS:
				return reader.AssemblyOSTable.RowSize;
			case TableIndex.AssemblyRef:
				return reader.AssemblyRefTable.RowSize;
			case TableIndex.AssemblyRefProcessor:
				return reader.AssemblyRefProcessorTable.RowSize;
			case TableIndex.AssemblyRefOS:
				return reader.AssemblyRefOSTable.RowSize;
			case TableIndex.File:
				return reader.FileTable.RowSize;
			case TableIndex.ExportedType:
				return reader.ExportedTypeTable.RowSize;
			case TableIndex.ManifestResource:
				return reader.ManifestResourceTable.RowSize;
			case TableIndex.NestedClass:
				return reader.NestedClassTable.RowSize;
			case TableIndex.GenericParam:
				return reader.GenericParamTable.RowSize;
			case TableIndex.MethodSpec:
				return reader.MethodSpecTable.RowSize;
			case TableIndex.GenericParamConstraint:
				return reader.GenericParamConstraintTable.RowSize;
			case TableIndex.Document:
				return reader.DocumentTable.RowSize;
			case TableIndex.MethodDebugInformation:
				return reader.MethodDebugInformationTable.RowSize;
			case TableIndex.LocalScope:
				return reader.LocalScopeTable.RowSize;
			case TableIndex.LocalVariable:
				return reader.LocalVariableTable.RowSize;
			case TableIndex.LocalConstant:
				return reader.LocalConstantTable.RowSize;
			case TableIndex.ImportScope:
				return reader.ImportScopeTable.RowSize;
			case TableIndex.StateMachineMethod:
				return reader.StateMachineMethodTable.RowSize;
			case TableIndex.CustomDebugInformation:
				return reader.CustomDebugInformationTable.RowSize;
			}
			throw new ArgumentOutOfRangeException("tableIndex");
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00015BB8 File Offset: 0x00013DB8
		public static int GetTableMetadataOffset(this MetadataReader reader, TableIndex tableIndex)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return (int)((long)(reader.GetTableMetadataBlock(tableIndex).Pointer - reader.Block.Pointer));
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00015BE4 File Offset: 0x00013DE4
		private static MemoryBlock GetTableMetadataBlock(this MetadataReader reader, TableIndex tableIndex)
		{
			switch (tableIndex)
			{
			case TableIndex.Module:
				return reader.ModuleTable.Block;
			case TableIndex.TypeRef:
				return reader.TypeRefTable.Block;
			case TableIndex.TypeDef:
				return reader.TypeDefTable.Block;
			case TableIndex.FieldPtr:
				return reader.FieldPtrTable.Block;
			case TableIndex.Field:
				return reader.FieldTable.Block;
			case TableIndex.MethodPtr:
				return reader.MethodPtrTable.Block;
			case TableIndex.MethodDef:
				return reader.MethodDefTable.Block;
			case TableIndex.ParamPtr:
				return reader.ParamPtrTable.Block;
			case TableIndex.Param:
				return reader.ParamTable.Block;
			case TableIndex.InterfaceImpl:
				return reader.InterfaceImplTable.Block;
			case TableIndex.MemberRef:
				return reader.MemberRefTable.Block;
			case TableIndex.Constant:
				return reader.ConstantTable.Block;
			case TableIndex.CustomAttribute:
				return reader.CustomAttributeTable.Block;
			case TableIndex.FieldMarshal:
				return reader.FieldMarshalTable.Block;
			case TableIndex.DeclSecurity:
				return reader.DeclSecurityTable.Block;
			case TableIndex.ClassLayout:
				return reader.ClassLayoutTable.Block;
			case TableIndex.FieldLayout:
				return reader.FieldLayoutTable.Block;
			case TableIndex.StandAloneSig:
				return reader.StandAloneSigTable.Block;
			case TableIndex.EventMap:
				return reader.EventMapTable.Block;
			case TableIndex.EventPtr:
				return reader.EventPtrTable.Block;
			case TableIndex.Event:
				return reader.EventTable.Block;
			case TableIndex.PropertyMap:
				return reader.PropertyMapTable.Block;
			case TableIndex.PropertyPtr:
				return reader.PropertyPtrTable.Block;
			case TableIndex.Property:
				return reader.PropertyTable.Block;
			case TableIndex.MethodSemantics:
				return reader.MethodSemanticsTable.Block;
			case TableIndex.MethodImpl:
				return reader.MethodImplTable.Block;
			case TableIndex.ModuleRef:
				return reader.ModuleRefTable.Block;
			case TableIndex.TypeSpec:
				return reader.TypeSpecTable.Block;
			case TableIndex.ImplMap:
				return reader.ImplMapTable.Block;
			case TableIndex.FieldRva:
				return reader.FieldRvaTable.Block;
			case TableIndex.EncLog:
				return reader.EncLogTable.Block;
			case TableIndex.EncMap:
				return reader.EncMapTable.Block;
			case TableIndex.Assembly:
				return reader.AssemblyTable.Block;
			case TableIndex.AssemblyProcessor:
				return reader.AssemblyProcessorTable.Block;
			case TableIndex.AssemblyOS:
				return reader.AssemblyOSTable.Block;
			case TableIndex.AssemblyRef:
				return reader.AssemblyRefTable.Block;
			case TableIndex.AssemblyRefProcessor:
				return reader.AssemblyRefProcessorTable.Block;
			case TableIndex.AssemblyRefOS:
				return reader.AssemblyRefOSTable.Block;
			case TableIndex.File:
				return reader.FileTable.Block;
			case TableIndex.ExportedType:
				return reader.ExportedTypeTable.Block;
			case TableIndex.ManifestResource:
				return reader.ManifestResourceTable.Block;
			case TableIndex.NestedClass:
				return reader.NestedClassTable.Block;
			case TableIndex.GenericParam:
				return reader.GenericParamTable.Block;
			case TableIndex.MethodSpec:
				return reader.MethodSpecTable.Block;
			case TableIndex.GenericParamConstraint:
				return reader.GenericParamConstraintTable.Block;
			case TableIndex.Document:
				return reader.DocumentTable.Block;
			case TableIndex.MethodDebugInformation:
				return reader.MethodDebugInformationTable.Block;
			case TableIndex.LocalScope:
				return reader.LocalScopeTable.Block;
			case TableIndex.LocalVariable:
				return reader.LocalVariableTable.Block;
			case TableIndex.LocalConstant:
				return reader.LocalConstantTable.Block;
			case TableIndex.ImportScope:
				return reader.ImportScopeTable.Block;
			case TableIndex.StateMachineMethod:
				return reader.StateMachineMethodTable.Block;
			case TableIndex.CustomDebugInformation:
				return reader.CustomDebugInformationTable.Block;
			}
			throw new ArgumentOutOfRangeException("tableIndex");
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00015F62 File Offset: 0x00014162
		public static int GetHeapSize(this MetadataReader reader, HeapIndex heapIndex)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return reader.GetMetadataBlock(heapIndex).Length;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00015F7E File Offset: 0x0001417E
		public static int GetHeapMetadataOffset(this MetadataReader reader, HeapIndex heapIndex)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return (int)((long)(reader.GetMetadataBlock(heapIndex).Pointer - reader.Block.Pointer));
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00015FAC File Offset: 0x000141AC
		private static MemoryBlock GetMetadataBlock(this MetadataReader reader, HeapIndex heapIndex)
		{
			switch (heapIndex)
			{
			case HeapIndex.UserString:
				return reader.UserStringStream.Block;
			case HeapIndex.String:
				return reader.StringStream.Block;
			case HeapIndex.Blob:
				return reader.BlobStream.Block;
			case HeapIndex.Guid:
				return reader.GuidStream.Block;
			default:
				throw new ArgumentOutOfRangeException("heapIndex");
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001600B File Offset: 0x0001420B
		public static UserStringHandle GetNextHandle(this MetadataReader reader, UserStringHandle handle)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return reader.UserStringStream.GetNextHandle(handle);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00016027 File Offset: 0x00014227
		public static BlobHandle GetNextHandle(this MetadataReader reader, BlobHandle handle)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return reader.BlobStream.GetNextHandle(handle);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00016043 File Offset: 0x00014243
		public static StringHandle GetNextHandle(this MetadataReader reader, StringHandle handle)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return reader.StringStream.GetNextHandle(handle);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001605F File Offset: 0x0001425F
		public static IEnumerable<EditAndContinueLogEntry> GetEditAndContinueLogEntries(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.EncLogTable.NumberOfRows; rid = num + 1)
			{
				yield return new EditAndContinueLogEntry(new EntityHandle(reader.EncLogTable.GetToken(rid)), reader.EncLogTable.GetFuncCode(rid));
				num = rid;
			}
			yield break;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001606F File Offset: 0x0001426F
		public static IEnumerable<EntityHandle> GetEditAndContinueMapEntries(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.EncMapTable.NumberOfRows; rid = num + 1)
			{
				yield return new EntityHandle(reader.EncMapTable.GetToken(rid));
				num = rid;
			}
			yield break;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001607F File Offset: 0x0001427F
		public static IEnumerable<TypeDefinitionHandle> GetTypesWithProperties(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.PropertyMapTable.NumberOfRows; rid = num + 1)
			{
				yield return reader.PropertyMapTable.GetParentType(rid);
				num = rid;
			}
			yield break;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001608F File Offset: 0x0001428F
		public static IEnumerable<TypeDefinitionHandle> GetTypesWithEvents(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.EventMapTable.NumberOfRows; rid = num + 1)
			{
				yield return reader.EventMapTable.GetParentType(rid);
				num = rid;
			}
			yield break;
		}
	}
}
