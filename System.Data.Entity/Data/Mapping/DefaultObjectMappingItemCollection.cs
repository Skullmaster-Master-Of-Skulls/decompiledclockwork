using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000230 RID: 560
	internal class DefaultObjectMappingItemCollection : MappingItemCollection
	{
		// Token: 0x060023D4 RID: 9172 RVA: 0x00081688 File Offset: 0x0007F888
		public DefaultObjectMappingItemCollection(EdmItemCollection edmCollection, ObjectItemCollection objectCollection) : base(DataSpace.OCSpace)
		{
			EntityUtil.CheckArgumentNull<EdmItemCollection>(edmCollection, "edmCollection");
			EntityUtil.CheckArgumentNull<ObjectItemCollection>(objectCollection, "objectCollection");
			this.m_edmCollection = edmCollection;
			this.m_objectCollection = objectCollection;
			this.LoadPrimitiveMaps();
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000816E8 File Offset: 0x0007F8E8
		internal override Map GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			Map result;
			if (!this.TryGetMap(identity, typeSpace, ignoreCase, out result))
			{
				throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(identity));
			}
			return result;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00081710 File Offset: 0x0007F910
		internal override bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out Map map)
		{
			EdmType edmType = null;
			EdmType edmType2 = null;
			if (typeSpace == DataSpace.CSpace)
			{
				if (ignoreCase)
				{
					if (!this.m_edmCollection.TryGetItem<EdmType>(identity, true, out edmType))
					{
						map = null;
						return false;
					}
					identity = edmType.Identity;
				}
				int index;
				if (this.cdmTypeIndexes.TryGetValue(identity, out index))
				{
					map = (Map)base[index];
					return true;
				}
				if (edmType != null || this.m_edmCollection.TryGetItem<EdmType>(identity, ignoreCase, out edmType))
				{
					this.m_objectCollection.TryGetOSpaceType(edmType, out edmType2);
				}
			}
			else if (typeSpace == DataSpace.OSpace)
			{
				if (ignoreCase)
				{
					if (!this.m_objectCollection.TryGetItem<EdmType>(identity, true, out edmType2))
					{
						map = null;
						return false;
					}
					identity = edmType2.Identity;
				}
				int index2;
				if (this.clrTypeIndexes.TryGetValue(identity, out index2))
				{
					map = (Map)base[index2];
					return true;
				}
				if (edmType2 != null || this.m_objectCollection.TryGetItem<EdmType>(identity, ignoreCase, out edmType2))
				{
					string identity2 = ObjectItemCollection.TryGetMappingCSpaceTypeIdentity(edmType2);
					this.m_edmCollection.TryGetItem<EdmType>(identity2, out edmType);
				}
			}
			if (edmType2 == null || edmType == null)
			{
				map = null;
				return false;
			}
			map = this.GetDefaultMapping(edmType, edmType2);
			return true;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0008181A File Offset: 0x0007FA1A
		internal override Map GetMap(string identity, DataSpace typeSpace)
		{
			return this.GetMap(identity, typeSpace, false);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00081825 File Offset: 0x0007FA25
		internal override bool TryGetMap(string identity, DataSpace typeSpace, out Map map)
		{
			return this.TryGetMap(identity, typeSpace, false, out map);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00081834 File Offset: 0x0007FA34
		internal override Map GetMap(GlobalItem item)
		{
			EntityUtil.CheckArgumentNull<GlobalItem>(item, "item");
			Map result;
			if (!this.TryGetMap(item, out result))
			{
				throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(item.Identity));
			}
			return result;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x0008186C File Offset: 0x0007FA6C
		internal override bool TryGetMap(GlobalItem item, out Map map)
		{
			if (item == null)
			{
				map = null;
				return false;
			}
			DataSpace dataSpace = item.DataSpace;
			EdmType edmType = item as EdmType;
			if (edmType != null && Helper.IsTransientType(edmType))
			{
				map = this.GetOCMapForTransientType(edmType, dataSpace);
				return map != null;
			}
			return this.TryGetMap(item.Identity, dataSpace, out map);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000818BA File Offset: 0x0007FABA
		private Map GetDefaultMapping(EdmType cdmType, EdmType clrType)
		{
			return DefaultObjectMappingItemCollection.LoadObjectMapping(cdmType, clrType, this);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000818C4 File Offset: 0x0007FAC4
		private Map GetOCMapForTransientType(EdmType edmType, DataSpace typeSpace)
		{
			EdmType edmType2 = null;
			EdmType edmType3 = null;
			int index = -1;
			if (typeSpace != DataSpace.OSpace)
			{
				if (this.cdmTypeIndexes.TryGetValue(edmType.Identity, out index))
				{
					return (Map)base[index];
				}
				edmType3 = edmType;
				edmType2 = this.ConvertCSpaceToOSpaceType(edmType);
			}
			else if (typeSpace == DataSpace.OSpace)
			{
				if (this.clrTypeIndexes.TryGetValue(edmType.Identity, out index))
				{
					return (Map)base[index];
				}
				edmType2 = edmType;
				edmType3 = this.ConvertOSpaceToCSpaceType(edmType2);
			}
			ObjectTypeMapping objectTypeMapping = new ObjectTypeMapping(edmType2, edmType3);
			if (BuiltInTypeKind.RowType == edmType.BuiltInTypeKind)
			{
				RowType rowType = (RowType)edmType2;
				RowType rowType2 = (RowType)edmType3;
				for (int i = 0; i < rowType.Properties.Count; i++)
				{
					objectTypeMapping.AddMemberMap(new ObjectPropertyMapping(rowType2.Properties[i], rowType.Properties[i]));
				}
			}
			if (!this.cdmTypeIndexes.ContainsKey(edmType3.Identity) && !this.clrTypeIndexes.ContainsKey(edmType2.Identity))
			{
				this.AddInternalMapping(objectTypeMapping);
			}
			return objectTypeMapping;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000819CC File Offset: 0x0007FBCC
		private EdmType ConvertCSpaceToOSpaceType(EdmType cdmType)
		{
			EdmType result;
			if (Helper.IsCollectionType(cdmType))
			{
				EdmType elementType = this.ConvertCSpaceToOSpaceType(((CollectionType)cdmType).TypeUsage.EdmType);
				result = new CollectionType(elementType);
			}
			else if (Helper.IsRowType(cdmType))
			{
				List<EdmProperty> list = new List<EdmProperty>();
				foreach (EdmProperty edmProperty in ((RowType)cdmType).Properties)
				{
					EdmType edmType = this.ConvertCSpaceToOSpaceType(edmProperty.TypeUsage.EdmType);
					EdmProperty item = new EdmProperty(edmProperty.Name, TypeUsage.Create(edmType));
					list.Add(item);
				}
				result = new RowType(list, ((RowType)cdmType).InitializerMetadata);
			}
			else if (Helper.IsRefType(cdmType))
			{
				result = new RefType((EntityType)this.ConvertCSpaceToOSpaceType(((RefType)cdmType).ElementType));
			}
			else if (Helper.IsPrimitiveType(cdmType))
			{
				result = this.m_objectCollection.GetMappedPrimitiveType(((PrimitiveType)cdmType).PrimitiveTypeKind);
			}
			else
			{
				result = ((ObjectTypeMapping)this.GetMap(cdmType)).ClrType;
			}
			return result;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00081AFC File Offset: 0x0007FCFC
		private EdmType ConvertOSpaceToCSpaceType(EdmType clrType)
		{
			EdmType result;
			if (Helper.IsCollectionType(clrType))
			{
				EdmType elementType = this.ConvertOSpaceToCSpaceType(((CollectionType)clrType).TypeUsage.EdmType);
				result = new CollectionType(elementType);
			}
			else if (Helper.IsRowType(clrType))
			{
				List<EdmProperty> list = new List<EdmProperty>();
				foreach (EdmProperty edmProperty in ((RowType)clrType).Properties)
				{
					EdmType edmType = this.ConvertOSpaceToCSpaceType(edmProperty.TypeUsage.EdmType);
					EdmProperty item = new EdmProperty(edmProperty.Name, TypeUsage.Create(edmType));
					list.Add(item);
				}
				result = new RowType(list, ((RowType)clrType).InitializerMetadata);
			}
			else if (Helper.IsRefType(clrType))
			{
				result = new RefType((EntityType)this.ConvertOSpaceToCSpaceType(((RefType)clrType).ElementType));
			}
			else
			{
				result = ((ObjectTypeMapping)this.GetMap(clrType)).EdmType;
			}
			return result;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00081C0C File Offset: 0x0007FE0C
		private void LoadPrimitiveMaps()
		{
			IEnumerable<PrimitiveType> primitiveTypes = this.m_edmCollection.GetPrimitiveTypes();
			foreach (PrimitiveType primitiveType in primitiveTypes)
			{
				PrimitiveType mappedPrimitiveType = this.m_objectCollection.GetMappedPrimitiveType(primitiveType.PrimitiveTypeKind);
				this.AddInternalMapping(new ObjectTypeMapping(mappedPrimitiveType, primitiveType));
			}
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00081C78 File Offset: 0x0007FE78
		private void AddInternalMapping(ObjectTypeMapping objectMap)
		{
			string identity = objectMap.ClrType.Identity;
			string identity2 = objectMap.EdmType.Identity;
			int count = base.Count;
			if (this.clrTypeIndexes.ContainsKey(identity))
			{
				if (BuiltInTypeKind.PrimitiveType != objectMap.ClrType.BuiltInTypeKind && BuiltInTypeKind.RowType != objectMap.ClrType.BuiltInTypeKind && BuiltInTypeKind.CollectionType != objectMap.ClrType.BuiltInTypeKind)
				{
					throw new MappingException(Strings.Mapping_Duplicate_Type(identity));
				}
			}
			else
			{
				this.clrTypeIndexes.Add(identity, count);
			}
			if (this.cdmTypeIndexes.ContainsKey(identity2))
			{
				if (BuiltInTypeKind.PrimitiveType != objectMap.EdmType.BuiltInTypeKind && BuiltInTypeKind.RowType != objectMap.EdmType.BuiltInTypeKind && BuiltInTypeKind.CollectionType != objectMap.EdmType.BuiltInTypeKind)
				{
					throw new MappingException(Strings.Mapping_Duplicate_Type(identity));
				}
			}
			else
			{
				this.cdmTypeIndexes.Add(identity2, count);
			}
			objectMap.DataSpace = DataSpace.OCSpace;
			base.AddInternal(objectMap);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00081D58 File Offset: 0x0007FF58
		internal static ObjectTypeMapping LoadObjectMapping(EdmType cdmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection)
		{
			Dictionary<string, ObjectTypeMapping> dictionary = new Dictionary<string, ObjectTypeMapping>(StringComparer.Ordinal);
			ObjectTypeMapping result = DefaultObjectMappingItemCollection.LoadObjectMapping(cdmType, objectType, ocItemCollection, dictionary);
			if (ocItemCollection != null)
			{
				foreach (ObjectTypeMapping objectMap in dictionary.Values)
				{
					ocItemCollection.AddInternalMapping(objectMap);
				}
			}
			return result;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00081DC4 File Offset: 0x0007FFC4
		private static ObjectTypeMapping LoadObjectMapping(EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			if (Helper.IsEnumType(edmType) ^ Helper.IsEnumType(objectType))
			{
				throw new MappingException(Strings.Mapping_EnumTypeMappingToNonEnumType(edmType.FullName, objectType.FullName));
			}
			if (edmType.Abstract != objectType.Abstract)
			{
				throw new MappingException(Strings.Mapping_AbstractTypeMappingToNonAbstractType(edmType.FullName, objectType.FullName));
			}
			ObjectTypeMapping objectTypeMapping = new ObjectTypeMapping(objectType, edmType);
			typeMappings.Add(edmType.FullName, objectTypeMapping);
			if (Helper.IsEntityType(edmType) || Helper.IsComplexType(edmType))
			{
				DefaultObjectMappingItemCollection.LoadEntityTypeOrComplexTypeMapping(objectTypeMapping, edmType, objectType, ocItemCollection, typeMappings);
			}
			else if (Helper.IsEnumType(edmType))
			{
				DefaultObjectMappingItemCollection.ValidateEnumTypeMapping((EnumType)edmType, (EnumType)objectType);
			}
			else
			{
				DefaultObjectMappingItemCollection.LoadAssociationTypeMapping(objectTypeMapping, edmType, objectType, ocItemCollection, typeMappings);
			}
			return objectTypeMapping;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00081E74 File Offset: 0x00080074
		private static EdmMember GetObjectMember(EdmMember edmMember, StructuralType objectType)
		{
			EdmMember result;
			if (!objectType.Members.TryGetValue(edmMember.Name, false, out result))
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_Clr_Member(edmMember.Name, edmMember.DeclaringType.FullName, objectType.FullName));
			}
			return result;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x00081EBC File Offset: 0x000800BC
		private static void ValidateMembersMatch(EdmMember edmMember, EdmMember objectMember)
		{
			if (edmMember.BuiltInTypeKind != objectMember.BuiltInTypeKind)
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_MemberKind_Mismatch(edmMember.Name, edmMember.DeclaringType.FullName, edmMember.BuiltInTypeKind, objectMember.Name, objectMember.DeclaringType.FullName, objectMember.BuiltInTypeKind));
			}
			if (edmMember.TypeUsage.EdmType.BuiltInTypeKind != objectMember.TypeUsage.EdmType.BuiltInTypeKind)
			{
				throw new MappingException(EntityRes.GetString("Mapping_Default_OCMapping_Member_Type_Mismatch", new object[]
				{
					edmMember.TypeUsage.EdmType.Name,
					edmMember.TypeUsage.EdmType.BuiltInTypeKind,
					edmMember.Name,
					edmMember.DeclaringType.FullName,
					objectMember.TypeUsage.EdmType.Name,
					objectMember.TypeUsage.EdmType.BuiltInTypeKind,
					objectMember.Name,
					objectMember.DeclaringType.FullName
				}));
			}
			if (Helper.IsPrimitiveType(edmMember.TypeUsage.EdmType))
			{
				PrimitiveType spatialNormalizedPrimitiveType = Helper.GetSpatialNormalizedPrimitiveType(edmMember.TypeUsage.EdmType);
				if (spatialNormalizedPrimitiveType.PrimitiveTypeKind != ((PrimitiveType)objectMember.TypeUsage.EdmType).PrimitiveTypeKind)
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_Invalid_MemberType(edmMember.TypeUsage.EdmType.FullName, edmMember.Name, edmMember.DeclaringType.FullName, objectMember.TypeUsage.EdmType.FullName, objectMember.Name, objectMember.DeclaringType.FullName));
				}
			}
			else
			{
				if (Helper.IsEnumType(edmMember.TypeUsage.EdmType))
				{
					DefaultObjectMappingItemCollection.ValidateEnumTypeMapping((EnumType)edmMember.TypeUsage.EdmType, (EnumType)objectMember.TypeUsage.EdmType);
					return;
				}
				EdmType edmType;
				EdmType edmType2;
				if (edmMember.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember)
				{
					edmType = ((RefType)edmMember.TypeUsage.EdmType).ElementType;
					edmType2 = ((RefType)objectMember.TypeUsage.EdmType).ElementType;
				}
				else if (BuiltInTypeKind.NavigationProperty == edmMember.BuiltInTypeKind && Helper.IsCollectionType(edmMember.TypeUsage.EdmType))
				{
					edmType = ((CollectionType)edmMember.TypeUsage.EdmType).TypeUsage.EdmType;
					edmType2 = ((CollectionType)objectMember.TypeUsage.EdmType).TypeUsage.EdmType;
				}
				else
				{
					edmType = edmMember.TypeUsage.EdmType;
					edmType2 = objectMember.TypeUsage.EdmType;
				}
				if (edmType.Identity != ObjectItemCollection.TryGetMappingCSpaceTypeIdentity(edmType2))
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_Invalid_MemberType(edmMember.TypeUsage.EdmType.FullName, edmMember.Name, edmMember.DeclaringType.FullName, objectMember.TypeUsage.EdmType.FullName, objectMember.Name, objectMember.DeclaringType.FullName));
				}
			}
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000821A3 File Offset: 0x000803A3
		private static ObjectPropertyMapping LoadScalarPropertyMapping(EdmProperty edmProperty, EdmProperty objectProperty)
		{
			return new ObjectPropertyMapping(edmProperty, objectProperty);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000821AC File Offset: 0x000803AC
		private static void LoadEntityTypeOrComplexTypeMapping(ObjectTypeMapping objectMapping, EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			StructuralType structuralType = (StructuralType)edmType;
			StructuralType structuralType2 = (StructuralType)objectType;
			DefaultObjectMappingItemCollection.ValidateAllMembersAreMapped(structuralType, structuralType2);
			foreach (EdmMember edmMember in structuralType.Members)
			{
				EdmMember objectMember = DefaultObjectMappingItemCollection.GetObjectMember(edmMember, structuralType2);
				DefaultObjectMappingItemCollection.ValidateMembersMatch(edmMember, objectMember);
				if (Helper.IsEdmProperty(edmMember))
				{
					EdmProperty edmProperty = (EdmProperty)edmMember;
					EdmProperty edmProperty2 = (EdmProperty)objectMember;
					if (Helper.IsComplexType(edmMember.TypeUsage.EdmType))
					{
						objectMapping.AddMemberMap(DefaultObjectMappingItemCollection.LoadComplexMemberMapping(edmProperty, edmProperty2, ocItemCollection, typeMappings));
					}
					else
					{
						objectMapping.AddMemberMap(DefaultObjectMappingItemCollection.LoadScalarPropertyMapping(edmProperty, edmProperty2));
					}
				}
				else
				{
					NavigationProperty navigationProperty = (NavigationProperty)edmMember;
					NavigationProperty navigationProperty2 = (NavigationProperty)objectMember;
					DefaultObjectMappingItemCollection.LoadTypeMapping(navigationProperty.RelationshipType, navigationProperty2.RelationshipType, ocItemCollection, typeMappings);
					objectMapping.AddMemberMap(new ObjectNavigationPropertyMapping(navigationProperty, navigationProperty2));
				}
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000822AC File Offset: 0x000804AC
		private static void ValidateAllMembersAreMapped(StructuralType cdmStructuralType, StructuralType objectStructuralType)
		{
			if (cdmStructuralType.Members.Count != objectStructuralType.Members.Count)
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_Member_Count_Mismatch(cdmStructuralType.FullName, objectStructuralType.FullName));
			}
			foreach (EdmMember edmMember in objectStructuralType.Members)
			{
				if (!cdmStructuralType.Members.Contains(edmMember.Identity))
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_Clr_Member2(edmMember.Name, objectStructuralType.FullName, cdmStructuralType.FullName));
				}
			}
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00082358 File Offset: 0x00080558
		private static void ValidateEnumTypeMapping(EnumType edmEnumType, EnumType objectEnumType)
		{
			if (edmEnumType.UnderlyingType.PrimitiveTypeKind != objectEnumType.UnderlyingType.PrimitiveTypeKind)
			{
				throw new MappingException(Strings.Mapping_Enum_OCMapping_UnderlyingTypesMismatch(edmEnumType.UnderlyingType.Name, edmEnumType.FullName, objectEnumType.UnderlyingType.Name, objectEnumType.FullName));
			}
			IEnumerator<EnumMember> enumerator = (from m in edmEnumType.Members
			orderby Convert.ToInt64(m.Value, CultureInfo.InvariantCulture), m.Name
			select m).GetEnumerator();
			IEnumerator<EnumMember> enumerator2 = (from m in objectEnumType.Members
			orderby Convert.ToInt64(m.Value, CultureInfo.InvariantCulture), m.Name
			select m).GetEnumerator();
			if (enumerator.MoveNext())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator.Current.Name == enumerator2.Current.Name && enumerator.Current.Value.Equals(enumerator2.Current.Value) && !enumerator.MoveNext())
					{
						return;
					}
				}
				throw new MappingException(Strings.Mapping_Enum_OCMapping_MemberMismatch(objectEnumType.FullName, enumerator.Current.Name, enumerator.Current.Value, edmEnumType.FullName));
			}
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000824D4 File Offset: 0x000806D4
		private static void LoadAssociationTypeMapping(ObjectTypeMapping objectMapping, EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			AssociationType associationType = (AssociationType)edmType;
			AssociationType associationType2 = (AssociationType)objectType;
			foreach (AssociationEndMember associationEndMember in associationType.AssociationEndMembers)
			{
				AssociationEndMember associationEndMember2 = (AssociationEndMember)DefaultObjectMappingItemCollection.GetObjectMember(associationEndMember, associationType2);
				DefaultObjectMappingItemCollection.ValidateMembersMatch(associationEndMember, associationEndMember2);
				if (associationEndMember.RelationshipMultiplicity != associationEndMember2.RelationshipMultiplicity)
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_MultiplicityMismatch(associationEndMember.RelationshipMultiplicity, associationEndMember.Name, associationType.FullName, associationEndMember2.RelationshipMultiplicity, associationEndMember2.Name, associationType2.FullName));
				}
				DefaultObjectMappingItemCollection.LoadTypeMapping(((RefType)associationEndMember.TypeUsage.EdmType).ElementType, ((RefType)associationEndMember2.TypeUsage.EdmType).ElementType, ocItemCollection, typeMappings);
				objectMapping.AddMemberMap(new ObjectAssociationEndMapping(associationEndMember, associationEndMember2));
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000825D8 File Offset: 0x000807D8
		private static ObjectComplexPropertyMapping LoadComplexMemberMapping(EdmProperty containingEdmMember, EdmProperty containingClrMember, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			ComplexType edmType = (ComplexType)containingEdmMember.TypeUsage.EdmType;
			ComplexType objectType = (ComplexType)containingClrMember.TypeUsage.EdmType;
			ObjectTypeMapping complexTypeMapping = DefaultObjectMappingItemCollection.LoadTypeMapping(edmType, objectType, ocItemCollection, typeMappings);
			return new ObjectComplexPropertyMapping(containingEdmMember, containingClrMember, complexTypeMapping);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x0008261C File Offset: 0x0008081C
		private static ObjectTypeMapping LoadTypeMapping(EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			ObjectTypeMapping result;
			if (typeMappings.TryGetValue(edmType.FullName, out result))
			{
				return result;
			}
			ObjectTypeMapping result2;
			if (ocItemCollection != null && ocItemCollection.ContainsMap(edmType, out result2))
			{
				return result2;
			}
			return DefaultObjectMappingItemCollection.LoadObjectMapping(edmType, objectType, ocItemCollection, typeMappings);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x00082654 File Offset: 0x00080854
		private bool ContainsMap(GlobalItem cspaceItem, out ObjectTypeMapping map)
		{
			int index;
			if (this.cdmTypeIndexes.TryGetValue(cspaceItem.Identity, out index))
			{
				map = (ObjectTypeMapping)base[index];
				return true;
			}
			map = null;
			return false;
		}

		// Token: 0x04000FEA RID: 4074
		private ObjectItemCollection m_objectCollection;

		// Token: 0x04000FEB RID: 4075
		private EdmItemCollection m_edmCollection;

		// Token: 0x04000FEC RID: 4076
		private Dictionary<string, int> clrTypeIndexes = new Dictionary<string, int>(StringComparer.Ordinal);

		// Token: 0x04000FED RID: 4077
		private Dictionary<string, int> cdmTypeIndexes = new Dictionary<string, int>(StringComparer.Ordinal);
	}
}
