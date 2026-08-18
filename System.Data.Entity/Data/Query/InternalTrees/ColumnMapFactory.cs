using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000093 RID: 147
	internal static class ColumnMapFactory
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x000351D4 File Offset: 0x000333D4
		internal static CollectionColumnMap CreateFunctionImportStructuralTypeColumnMap(DbDataReader storeDataReader, FunctionImportMappingNonComposable mapping, int resultSetIndex, EntitySet entitySet, StructuralType baseStructuralType)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = mapping.GetResultMapping(resultSetIndex);
			if (resultMapping.NormalizedEntityTypeMappings.Count == 0)
			{
				return ColumnMapFactory.CreateColumnMapFromReaderAndType(storeDataReader, baseStructuralType, entitySet, resultMapping.ReturnTypeColumnsRenameMapping);
			}
			EntityType item = baseStructuralType as EntityType;
			ScalarColumnMap[] array = ColumnMapFactory.CreateDiscriminatorColumnMaps(storeDataReader, mapping, resultSetIndex);
			HashSet<EntityType> hashSet = new HashSet<EntityType>(resultMapping.MappedEntityTypes);
			hashSet.Add(item);
			Dictionary<EntityType, TypedColumnMap> dictionary = new Dictionary<EntityType, TypedColumnMap>(hashSet.Count);
			ColumnMap[] array2 = null;
			foreach (EntityType entityType in hashSet)
			{
				ColumnMap[] columnMapsForType = ColumnMapFactory.GetColumnMapsForType(storeDataReader, entityType, resultMapping.ReturnTypeColumnsRenameMapping);
				EntityColumnMap value = ColumnMapFactory.CreateEntityTypeElementColumnMap(storeDataReader, entityType, entitySet, columnMapsForType, resultMapping.ReturnTypeColumnsRenameMapping);
				if (!entityType.Abstract)
				{
					dictionary.Add(entityType, value);
				}
				if (entityType == baseStructuralType)
				{
					array2 = columnMapsForType;
				}
			}
			TypeUsage type = TypeUsage.Create(baseStructuralType);
			string name = baseStructuralType.Name;
			ColumnMap[] baseTypeColumns = array2;
			SimpleColumnMap[] typeDiscriminators = array;
			MultipleDiscriminatorPolymorphicColumnMap elementMap = new MultipleDiscriminatorPolymorphicColumnMap(type, name, baseTypeColumns, typeDiscriminators, dictionary, (object[] discriminatorValues) => mapping.Discriminate(discriminatorValues, resultSetIndex));
			return new SimpleCollectionColumnMap(baseStructuralType.GetCollectionType().TypeUsage, baseStructuralType.Name, elementMap, null, null);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00035330 File Offset: 0x00033530
		internal static CollectionColumnMap CreateColumnMapFromReaderAndType(DbDataReader storeDataReader, EdmType edmType, EntitySet entitySet, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			ColumnMap[] columnMapsForType = ColumnMapFactory.GetColumnMapsForType(storeDataReader, edmType, renameList);
			ColumnMap elementMap = null;
			if (Helper.IsRowType(edmType))
			{
				elementMap = new RecordColumnMap(TypeUsage.Create(edmType), edmType.Name, columnMapsForType, null);
			}
			else if (Helper.IsComplexType(edmType))
			{
				elementMap = new ComplexTypeColumnMap(TypeUsage.Create(edmType), edmType.Name, columnMapsForType, null);
			}
			else if (Helper.IsScalarType(edmType))
			{
				if (storeDataReader.FieldCount != 1)
				{
					throw EntityUtil.CommandExecutionDataReaderFieldCountForScalarType();
				}
				elementMap = new ScalarColumnMap(TypeUsage.Create(edmType), edmType.Name, 0, 0);
			}
			else if (Helper.IsEntityType(edmType))
			{
				elementMap = ColumnMapFactory.CreateEntityTypeElementColumnMap(storeDataReader, edmType, entitySet, columnMapsForType, null);
			}
			return new SimpleCollectionColumnMap(edmType.GetCollectionType().TypeUsage, edmType.Name, elementMap, null, null);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000353E0 File Offset: 0x000335E0
		internal static CollectionColumnMap CreateColumnMapFromReaderAndClrType(DbDataReader reader, Type type, MetadataWorkspace workspace)
		{
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (type.IsAbstract || (null == constructor && !type.IsValueType))
			{
				throw EntityUtil.InvalidOperation(Strings.ObjectContext_InvalidTypeForStoreQuery(type));
			}
			List<Tuple<MemberAssignment, int, EdmProperty>> list = new List<Tuple<MemberAssignment, int, EdmProperty>>();
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Type type2 = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
				Type type3 = type2.IsEnum ? type2.GetEnumUnderlyingType() : propertyInfo.PropertyType;
				int item;
				EdmType edmType;
				if (ColumnMapFactory.TryGetColumnOrdinalFromReader(reader, propertyInfo.Name, out item) && MetadataHelper.TryDetermineCSpaceModelType(type3, workspace, out edmType) && Helper.IsScalarType(edmType) && propertyInfo.CanWrite && propertyInfo.GetIndexParameters().Length == 0 && null != propertyInfo.GetSetMethod(true))
				{
					list.Add(Tuple.Create<MemberAssignment, int, EdmProperty>(Expression.Bind(propertyInfo, Expression.Parameter(propertyInfo.PropertyType, "placeholder")), item, new EdmProperty(propertyInfo.Name, TypeUsage.Create(edmType))));
				}
			}
			MemberInfo[] array = new MemberInfo[list.Count];
			MemberBinding[] array2 = new MemberBinding[list.Count];
			ColumnMap[] array3 = new ColumnMap[list.Count];
			EdmProperty[] array4 = new EdmProperty[list.Count];
			int num = 0;
			foreach (IGrouping<int, Tuple<MemberAssignment, int, EdmProperty>> grouping in from tuple in list
			group tuple by tuple.Item2 into tuple
			orderby tuple.Key
			select tuple)
			{
				if (grouping.Count<Tuple<MemberAssignment, int, EdmProperty>>() != 1)
				{
					throw EntityUtil.InvalidOperation(Strings.ObjectContext_TwoPropertiesMappedToSameColumn(reader.GetName(grouping.Key), string.Join(", ", grouping.Select((Tuple<MemberAssignment, int, EdmProperty> tuple) => tuple.Item3.Name).ToArray<string>())));
				}
				Tuple<MemberAssignment, int, EdmProperty> tuple2 = grouping.Single<Tuple<MemberAssignment, int, EdmProperty>>();
				MemberAssignment item2 = tuple2.Item1;
				int item3 = tuple2.Item2;
				EdmProperty item4 = tuple2.Item3;
				array[num] = item2.Member;
				array2[num] = item2;
				array3[num] = new ScalarColumnMap(item4.TypeUsage, item4.Name, 0, item3);
				array4[num] = item4;
				num++;
			}
			NewExpression newExpression = (null == constructor) ? Expression.New(type) : Expression.New(constructor);
			MemberInitExpression initExpression = Expression.MemberInit(newExpression, array2);
			InitializerMetadata initializerMetadata = InitializerMetadata.CreateProjectionInitializer((EdmItemCollection)workspace.GetItemCollection(DataSpace.CSpace), initExpression, array);
			RowType rowType = new RowType(array4, initializerMetadata);
			RecordColumnMap elementMap = new RecordColumnMap(TypeUsage.Create(rowType), "DefaultTypeProjection", array3, null);
			return new SimpleCollectionColumnMap(rowType.GetCollectionType().TypeUsage, rowType.Name, elementMap, null, null);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000356F4 File Offset: 0x000338F4
		private static EntityColumnMap CreateEntityTypeElementColumnMap(DbDataReader storeDataReader, EdmType edmType, EntitySet entitySet, ColumnMap[] propertyColumnMaps, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			EntityType entityType = (EntityType)edmType;
			ColumnMap[] array = new ColumnMap[storeDataReader.FieldCount];
			foreach (ColumnMap columnMap in propertyColumnMaps)
			{
				int columnPos = ((ScalarColumnMap)columnMap).ColumnPos;
				array[columnPos] = columnMap;
			}
			IList<EdmMember> keyMembers = entityType.KeyMembers;
			SimpleColumnMap[] array2 = new SimpleColumnMap[keyMembers.Count];
			int num = 0;
			foreach (EdmMember member in keyMembers)
			{
				int memberOrdinalFromReader = ColumnMapFactory.GetMemberOrdinalFromReader(storeDataReader, member, edmType, renameList);
				ColumnMap columnMap2 = array[memberOrdinalFromReader];
				array2[num] = (SimpleColumnMap)columnMap2;
				num++;
			}
			SimpleEntityIdentity entityIdentity = new SimpleEntityIdentity(entitySet, array2);
			return new EntityColumnMap(TypeUsage.Create(edmType), edmType.Name, propertyColumnMaps, entityIdentity);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000357DC File Offset: 0x000339DC
		private static ColumnMap[] GetColumnMapsForType(DbDataReader storeDataReader, EdmType edmType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(edmType);
			ColumnMap[] array = new ColumnMap[allStructuralMembers.Count];
			int num = 0;
			foreach (object obj in allStructuralMembers)
			{
				EdmMember edmMember = (EdmMember)obj;
				if (!Helper.IsScalarType(edmMember.TypeUsage.EdmType))
				{
					throw EntityUtil.InvalidOperation(Strings.ADP_InvalidDataReaderUnableToMaterializeNonScalarType(edmMember.Name, edmMember.TypeUsage.EdmType.FullName));
				}
				int memberOrdinalFromReader = ColumnMapFactory.GetMemberOrdinalFromReader(storeDataReader, edmMember, edmType, renameList);
				array[num] = new ScalarColumnMap(edmMember.TypeUsage, edmMember.Name, 0, memberOrdinalFromReader);
				num++;
			}
			return array;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000358A4 File Offset: 0x00033AA4
		private static ScalarColumnMap[] CreateDiscriminatorColumnMaps(DbDataReader storeDataReader, FunctionImportMappingNonComposable mapping, int resultIndex)
		{
			EdmType primitiveType = MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.String);
			TypeUsage type = TypeUsage.Create(primitiveType);
			IList<string> discriminatorColumns = mapping.GetDiscriminatorColumns(resultIndex);
			ScalarColumnMap[] array = new ScalarColumnMap[discriminatorColumns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				string text = discriminatorColumns[i];
				ScalarColumnMap scalarColumnMap = new ScalarColumnMap(type, text, 0, ColumnMapFactory.GetDiscriminatorOrdinalFromReader(storeDataReader, text, mapping.FunctionImport));
				array[i] = scalarColumnMap;
			}
			return array;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00035918 File Offset: 0x00033B18
		private static int GetMemberOrdinalFromReader(DbDataReader storeDataReader, EdmMember member, EdmType currentType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			string renameForMember = ColumnMapFactory.GetRenameForMember(member, currentType, renameList);
			int result;
			if (!ColumnMapFactory.TryGetColumnOrdinalFromReader(storeDataReader, renameForMember, out result))
			{
				throw EntityUtil.CommandExecutionDataReaderMissingColumnForType(member, currentType);
			}
			return result;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00035944 File Offset: 0x00033B44
		private static string GetRenameForMember(EdmMember member, EdmType currentType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			if (renameList != null && renameList.Count != 0 && renameList.Any((KeyValuePair<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> m) => m.Key == member.Name))
			{
				return renameList[member.Name].GetRename(currentType);
			}
			return member.Name;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000359A0 File Offset: 0x00033BA0
		private static int GetDiscriminatorOrdinalFromReader(DbDataReader storeDataReader, string columnName, EdmFunction functionImport)
		{
			int result;
			if (!ColumnMapFactory.TryGetColumnOrdinalFromReader(storeDataReader, columnName, out result))
			{
				throw EntityUtil.CommandExecutionDataReaderMissinDiscriminatorColumn(columnName, functionImport);
			}
			return result;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000359C4 File Offset: 0x00033BC4
		private static bool TryGetColumnOrdinalFromReader(DbDataReader storeDataReader, string columnName, out int ordinal)
		{
			if (storeDataReader.FieldCount == 0)
			{
				ordinal = 0;
				return false;
			}
			bool result;
			try
			{
				ordinal = storeDataReader.GetOrdinal(columnName);
				result = true;
			}
			catch (IndexOutOfRangeException)
			{
				ordinal = 0;
				result = false;
			}
			return result;
		}
	}
}
