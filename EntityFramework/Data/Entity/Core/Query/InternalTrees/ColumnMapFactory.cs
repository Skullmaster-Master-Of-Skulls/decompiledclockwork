using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000633 RID: 1587
	internal class ColumnMapFactory
	{
		// Token: 0x06003DBA RID: 15802 RVA: 0x0011BAAC File Offset: 0x00119CAC
		internal virtual CollectionColumnMap CreateFunctionImportStructuralTypeColumnMap(DbDataReader storeDataReader, FunctionImportMappingNonComposable mapping, int resultSetIndex, EntitySet entitySet, StructuralType baseStructuralType)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = mapping.GetResultMapping(resultSetIndex);
			if (resultMapping.NormalizedEntityTypeMappings.Count == 0)
			{
				return this.CreateColumnMapFromReaderAndType(storeDataReader, baseStructuralType, entitySet, resultMapping.ReturnTypeColumnsRenameMapping);
			}
			EntityType item = baseStructuralType as EntityType;
			ScalarColumnMap[] typeDiscriminators = ColumnMapFactory.CreateDiscriminatorColumnMaps(storeDataReader, mapping, resultSetIndex);
			HashSet<EntityType> hashSet = new HashSet<EntityType>(resultMapping.MappedEntityTypes);
			hashSet.Add(item);
			Dictionary<EntityType, TypedColumnMap> dictionary = new Dictionary<EntityType, TypedColumnMap>(hashSet.Count);
			ColumnMap[] baseTypeColumns = null;
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
					baseTypeColumns = columnMapsForType;
				}
			}
			MultipleDiscriminatorPolymorphicColumnMap elementMap = new MultipleDiscriminatorPolymorphicColumnMap(TypeUsage.Create(baseStructuralType), baseStructuralType.Name, baseTypeColumns, typeDiscriminators, dictionary, (object[] discriminatorValues) => mapping.Discriminate(discriminatorValues, resultSetIndex));
			return new SimpleCollectionColumnMap(baseStructuralType.GetCollectionType().TypeUsage, baseStructuralType.Name, elementMap, null, null);
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x0011BC0C File Offset: 0x00119E0C
		internal virtual CollectionColumnMap CreateColumnMapFromReaderAndType(DbDataReader storeDataReader, EdmType edmType, EntitySet entitySet, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
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
					throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderFieldCountForScalarType);
				}
				elementMap = new ScalarColumnMap(TypeUsage.Create(edmType), edmType.Name, 0, 0);
			}
			else if (Helper.IsEntityType(edmType))
			{
				elementMap = ColumnMapFactory.CreateEntityTypeElementColumnMap(storeDataReader, edmType, entitySet, columnMapsForType, null);
			}
			return new SimpleCollectionColumnMap(edmType.GetCollectionType().TypeUsage, edmType.Name, elementMap, null, null);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x0011BCE8 File Offset: 0x00119EE8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal virtual CollectionColumnMap CreateColumnMapFromReaderAndClrType(DbDataReader reader, Type type, MetadataWorkspace workspace)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(new Type[0]);
			if (type.IsAbstract() || (null == declaredConstructor && !type.IsValueType()))
			{
				throw new InvalidOperationException(Strings.ObjectContext_InvalidTypeForStoreQuery(type));
			}
			List<Tuple<MemberAssignment, int, EdmProperty>> list = new List<Tuple<MemberAssignment, int, EdmProperty>>();
			foreach (PropertyInfo propertyInfo in from p in type.GetInstanceProperties()
			select p.GetPropertyInfoForSet())
			{
				Type type2 = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
				Type type3 = type2.IsEnum() ? type2.GetEnumUnderlyingType() : propertyInfo.PropertyType;
				int item;
				EdmType edmType;
				if (ColumnMapFactory.TryGetColumnOrdinalFromReader(reader, propertyInfo.Name, out item) && workspace.TryDetermineCSpaceModelType(type3, out edmType) && Helper.IsScalarType(edmType) && propertyInfo.CanWriteExtended() && propertyInfo.GetIndexParameters().Length == 0 && null != propertyInfo.Setter())
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
					throw new InvalidOperationException(Strings.ObjectContext_TwoPropertiesMappedToSameColumn(reader.GetName(grouping.Key), string.Join(", ", grouping.Select((Tuple<MemberAssignment, int, EdmProperty> tuple) => tuple.Item3.Name).ToArray<string>())));
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
			NewExpression newExpression = (null == declaredConstructor) ? Expression.New(type) : Expression.New(declaredConstructor);
			MemberInitExpression initExpression = Expression.MemberInit(newExpression, array2);
			InitializerMetadata initializerMetadata = InitializerMetadata.CreateProjectionInitializer((EdmItemCollection)workspace.GetItemCollection(DataSpace.CSpace), initExpression);
			RowType rowType = new RowType(array4, initializerMetadata);
			RecordColumnMap elementMap = new RecordColumnMap(TypeUsage.Create(rowType), "DefaultTypeProjection", array3, null);
			return new SimpleCollectionColumnMap(rowType.GetCollectionType().TypeUsage, rowType.Name, elementMap, null, null);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x0011C020 File Offset: 0x0011A220
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

		// Token: 0x06003DBE RID: 15806 RVA: 0x0011C108 File Offset: 0x0011A308
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
					throw new InvalidOperationException(Strings.ADP_InvalidDataReaderUnableToMaterializeNonScalarType(edmMember.Name, edmMember.TypeUsage.EdmType.FullName));
				}
				int memberOrdinalFromReader = ColumnMapFactory.GetMemberOrdinalFromReader(storeDataReader, edmMember, edmType, renameList);
				array[num] = new ScalarColumnMap(edmMember.TypeUsage, edmMember.Name, 0, memberOrdinalFromReader);
				num++;
			}
			return array;
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x0011C1CC File Offset: 0x0011A3CC
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

		// Token: 0x06003DC0 RID: 15808 RVA: 0x0011C240 File Offset: 0x0011A440
		private static int GetMemberOrdinalFromReader(DbDataReader storeDataReader, EdmMember member, EdmType currentType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			string renameForMember = ColumnMapFactory.GetRenameForMember(member, currentType, renameList);
			int result;
			if (!ColumnMapFactory.TryGetColumnOrdinalFromReader(storeDataReader, renameForMember, out result))
			{
				throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderMissingColumnForType(currentType.FullName, member.Name));
			}
			return result;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x0011C29C File Offset: 0x0011A49C
		private static string GetRenameForMember(EdmMember member, EdmType currentType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			if (renameList != null && renameList.Count != 0 && renameList.Any((KeyValuePair<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> m) => m.Key == member.Name))
			{
				return renameList[member.Name].GetRename(currentType);
			}
			return member.Name;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x0011C2F8 File Offset: 0x0011A4F8
		private static int GetDiscriminatorOrdinalFromReader(DbDataReader storeDataReader, string columnName, EdmFunction functionImport)
		{
			int result;
			if (!ColumnMapFactory.TryGetColumnOrdinalFromReader(storeDataReader, columnName, out result))
			{
				throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderMissingDiscriminatorColumn(columnName, functionImport.FullName));
			}
			return result;
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x0011C324 File Offset: 0x0011A524
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
