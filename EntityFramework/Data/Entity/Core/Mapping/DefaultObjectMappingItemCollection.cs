using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003AB RID: 939
	internal class DefaultObjectMappingItemCollection : MappingItemCollection
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x0009F8D8 File Offset: 0x0009DAD8
		public DefaultObjectMappingItemCollection(EdmItemCollection edmCollection, ObjectItemCollection objectCollection) : base(DataSpace.OCSpace)
		{
			this._edmCollection = edmCollection;
			this._objectCollection = objectCollection;
			ReadOnlyCollection<PrimitiveType> primitiveTypes = this._edmCollection.GetPrimitiveTypes();
			foreach (PrimitiveType primitiveType in primitiveTypes)
			{
				PrimitiveType mappedPrimitiveType = this._objectCollection.GetMappedPrimitiveType(primitiveType.PrimitiveTypeKind);
				this.AddInternalMapping(new ObjectTypeMapping(mappedPrimitiveType, primitiveType), this._clrTypeIndexes, this._edmTypeIndexes);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0009F994 File Offset: 0x0009DB94
		public ObjectItemCollection ObjectItemCollection
		{
			get
			{
				return this._objectCollection;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x0009F99C File Offset: 0x0009DB9C
		public EdmItemCollection EdmItemCollection
		{
			get
			{
				return this._edmCollection;
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0009F9A4 File Offset: 0x0009DBA4
		internal override MappingBase GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			MappingBase result;
			if (!this.TryGetMap(identity, typeSpace, ignoreCase, out result))
			{
				throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(identity));
			}
			return result;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0009F9CC File Offset: 0x0009DBCC
		internal override bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out MappingBase map)
		{
			EdmType edmType = null;
			EdmType edmType2 = null;
			if (typeSpace == DataSpace.CSpace)
			{
				if (ignoreCase)
				{
					if (!this._edmCollection.TryGetItem<EdmType>(identity, true, out edmType))
					{
						map = null;
						return false;
					}
					identity = edmType.Identity;
				}
				int index;
				if (this._edmTypeIndexes.TryGetValue(identity, out index))
				{
					map = (MappingBase)base[index];
					return true;
				}
				if (edmType != null || this._edmCollection.TryGetItem<EdmType>(identity, ignoreCase, out edmType))
				{
					this._objectCollection.TryGetOSpaceType(edmType, out edmType2);
				}
			}
			else if (typeSpace == DataSpace.OSpace)
			{
				if (ignoreCase)
				{
					if (!this._objectCollection.TryGetItem<EdmType>(identity, true, out edmType2))
					{
						map = null;
						return false;
					}
					identity = edmType2.Identity;
				}
				int index2;
				if (this._clrTypeIndexes.TryGetValue(identity, out index2))
				{
					map = (MappingBase)base[index2];
					return true;
				}
				if (edmType2 != null || this._objectCollection.TryGetItem<EdmType>(identity, ignoreCase, out edmType2))
				{
					string identity2 = ObjectItemCollection.TryGetMappingCSpaceTypeIdentity(edmType2);
					this._edmCollection.TryGetItem<EdmType>(identity2, out edmType);
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

		// Token: 0x06002231 RID: 8753 RVA: 0x0009FAD6 File Offset: 0x0009DCD6
		internal override MappingBase GetMap(string identity, DataSpace typeSpace)
		{
			return this.GetMap(identity, typeSpace, false);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0009FAE1 File Offset: 0x0009DCE1
		internal override bool TryGetMap(string identity, DataSpace typeSpace, out MappingBase map)
		{
			return this.TryGetMap(identity, typeSpace, false, out map);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0009FAF0 File Offset: 0x0009DCF0
		internal override MappingBase GetMap(GlobalItem item)
		{
			MappingBase result;
			if (!this.TryGetMap(item, out result))
			{
				throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(item.Identity));
			}
			return result;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0009FB1C File Offset: 0x0009DD1C
		internal override bool TryGetMap(GlobalItem item, out MappingBase map)
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

		// Token: 0x06002235 RID: 8757 RVA: 0x0009FB6A File Offset: 0x0009DD6A
		private MappingBase GetDefaultMapping(EdmType cdmType, EdmType clrType)
		{
			return DefaultObjectMappingItemCollection.LoadObjectMapping(cdmType, clrType, this);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0009FB74 File Offset: 0x0009DD74
		private MappingBase GetOCMapForTransientType(EdmType edmType, DataSpace typeSpace)
		{
			EdmType edmType2 = null;
			EdmType edmType3 = null;
			int index = -1;
			if (typeSpace != DataSpace.OSpace)
			{
				if (this._edmTypeIndexes.TryGetValue(edmType.Identity, out index))
				{
					return (MappingBase)base[index];
				}
				edmType3 = edmType;
				edmType2 = this.ConvertCSpaceToOSpaceType(edmType);
			}
			else if (typeSpace == DataSpace.OSpace)
			{
				if (this._clrTypeIndexes.TryGetValue(edmType.Identity, out index))
				{
					return (MappingBase)base[index];
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
			if (!this._edmTypeIndexes.ContainsKey(edmType3.Identity) && !this._clrTypeIndexes.ContainsKey(edmType2.Identity))
			{
				lock (this._lock)
				{
					Dictionary<string, int> clrTypeIndexes = new Dictionary<string, int>(this._clrTypeIndexes);
					Dictionary<string, int> edmTypeIndexes = new Dictionary<string, int>(this._edmTypeIndexes);
					objectTypeMapping = this.AddInternalMapping(objectTypeMapping, clrTypeIndexes, edmTypeIndexes);
					this._clrTypeIndexes = clrTypeIndexes;
					this._edmTypeIndexes = edmTypeIndexes;
				}
			}
			return objectTypeMapping;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0009FCDC File Offset: 0x0009DEDC
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
				RowType rowType = (RowType)cdmType;
				foreach (EdmProperty edmProperty in rowType.Properties)
				{
					EdmType edmType = this.ConvertCSpaceToOSpaceType(edmProperty.TypeUsage.EdmType);
					EdmProperty item = new EdmProperty(edmProperty.Name, TypeUsage.Create(edmType));
					list.Add(item);
				}
				result = new RowType(list, rowType.InitializerMetadata);
			}
			else if (Helper.IsRefType(cdmType))
			{
				result = new RefType((EntityType)this.ConvertCSpaceToOSpaceType(((RefType)cdmType).ElementType));
			}
			else if (Helper.IsPrimitiveType(cdmType))
			{
				result = this._objectCollection.GetMappedPrimitiveType(((PrimitiveType)cdmType).PrimitiveTypeKind);
			}
			else
			{
				result = ((ObjectTypeMapping)this.GetMap(cdmType)).ClrType;
			}
			return result;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0009FE08 File Offset: 0x0009E008
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
				RowType rowType = (RowType)clrType;
				foreach (EdmProperty edmProperty in rowType.Properties)
				{
					EdmType edmType = this.ConvertOSpaceToCSpaceType(edmProperty.TypeUsage.EdmType);
					EdmProperty item = new EdmProperty(edmProperty.Name, TypeUsage.Create(edmType));
					list.Add(item);
				}
				result = new RowType(list, rowType.InitializerMetadata);
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

		// Token: 0x06002239 RID: 8761 RVA: 0x0009FF14 File Offset: 0x0009E114
		private void AddInternalMappings(IEnumerable<ObjectTypeMapping> typeMappings)
		{
			lock (this._lock)
			{
				Dictionary<string, int> clrTypeIndexes = new Dictionary<string, int>(this._clrTypeIndexes);
				Dictionary<string, int> edmTypeIndexes = new Dictionary<string, int>(this._edmTypeIndexes);
				foreach (ObjectTypeMapping objectMap in typeMappings)
				{
					this.AddInternalMapping(objectMap, clrTypeIndexes, edmTypeIndexes);
				}
				this._clrTypeIndexes = clrTypeIndexes;
				this._edmTypeIndexes = edmTypeIndexes;
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0009FFB8 File Offset: 0x0009E1B8
		private ObjectTypeMapping AddInternalMapping(ObjectTypeMapping objectMap, Dictionary<string, int> clrTypeIndexes, Dictionary<string, int> edmTypeIndexes)
		{
			if (base.Source.ContainsIdentity(objectMap.Identity))
			{
				return (ObjectTypeMapping)base.Source[objectMap.Identity];
			}
			objectMap.DataSpace = DataSpace.OCSpace;
			int count = base.Count;
			base.AddInternal(objectMap);
			string identity = objectMap.ClrType.Identity;
			if (!clrTypeIndexes.ContainsKey(identity))
			{
				clrTypeIndexes.Add(identity, count);
			}
			string identity2 = objectMap.EdmType.Identity;
			if (!edmTypeIndexes.ContainsKey(identity2))
			{
				edmTypeIndexes.Add(identity2, count);
			}
			return objectMap;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000A0040 File Offset: 0x0009E240
		internal static ObjectTypeMapping LoadObjectMapping(EdmType cdmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection)
		{
			Dictionary<string, ObjectTypeMapping> dictionary = new Dictionary<string, ObjectTypeMapping>(StringComparer.Ordinal);
			ObjectTypeMapping result = DefaultObjectMappingItemCollection.LoadObjectMapping(cdmType, objectType, ocItemCollection, dictionary);
			if (ocItemCollection != null)
			{
				ocItemCollection.AddInternalMappings(dictionary.Values);
			}
			return result;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000A0074 File Offset: 0x0009E274
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

		// Token: 0x0600223D RID: 8765 RVA: 0x000A0124 File Offset: 0x0009E324
		private static EdmMember GetObjectMember(EdmMember edmMember, StructuralType objectType)
		{
			EdmMember result;
			if (!objectType.Members.TryGetValue(edmMember.Name, false, out result))
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_Clr_Member(edmMember.Name, edmMember.DeclaringType.FullName, objectType.FullName));
			}
			return result;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000A016C File Offset: 0x0009E36C
		private static void ValidateMembersMatch(EdmMember edmMember, EdmMember objectMember)
		{
			if (edmMember.BuiltInTypeKind != objectMember.BuiltInTypeKind)
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_MemberKind_Mismatch(edmMember.Name, edmMember.DeclaringType.FullName, edmMember.BuiltInTypeKind, objectMember.Name, objectMember.DeclaringType.FullName, objectMember.BuiltInTypeKind));
			}
			if (edmMember.TypeUsage.EdmType.BuiltInTypeKind != objectMember.TypeUsage.EdmType.BuiltInTypeKind)
			{
				throw Error.Mapping_Default_OCMapping_Member_Type_Mismatch(edmMember.TypeUsage.EdmType.Name, edmMember.TypeUsage.EdmType.BuiltInTypeKind, edmMember.Name, edmMember.DeclaringType.FullName, objectMember.TypeUsage.EdmType.Name, objectMember.TypeUsage.EdmType.BuiltInTypeKind, objectMember.Name, objectMember.DeclaringType.FullName);
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

		// Token: 0x0600223F RID: 8767 RVA: 0x000A0428 File Offset: 0x0009E628
		private static ObjectPropertyMapping LoadScalarPropertyMapping(EdmProperty edmProperty, EdmProperty objectProperty)
		{
			return new ObjectPropertyMapping(edmProperty, objectProperty);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000A0434 File Offset: 0x0009E634
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

		// Token: 0x06002241 RID: 8769 RVA: 0x000A0530 File Offset: 0x0009E730
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

		// Token: 0x06002242 RID: 8770 RVA: 0x000A0610 File Offset: 0x0009E810
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

		// Token: 0x06002243 RID: 8771 RVA: 0x000A0784 File Offset: 0x0009E984
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

		// Token: 0x06002244 RID: 8772 RVA: 0x000A0880 File Offset: 0x0009EA80
		private static ObjectComplexPropertyMapping LoadComplexMemberMapping(EdmProperty containingEdmMember, EdmProperty containingClrMember, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			ComplexType edmType = (ComplexType)containingEdmMember.TypeUsage.EdmType;
			ComplexType objectType = (ComplexType)containingClrMember.TypeUsage.EdmType;
			DefaultObjectMappingItemCollection.LoadTypeMapping(edmType, objectType, ocItemCollection, typeMappings);
			return new ObjectComplexPropertyMapping(containingEdmMember, containingClrMember);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000A08C0 File Offset: 0x0009EAC0
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

		// Token: 0x06002246 RID: 8774 RVA: 0x000A08F8 File Offset: 0x0009EAF8
		private bool ContainsMap(GlobalItem cspaceItem, out ObjectTypeMapping map)
		{
			int index;
			if (this._edmTypeIndexes.TryGetValue(cspaceItem.Identity, out index))
			{
				map = (ObjectTypeMapping)base[index];
				return true;
			}
			map = null;
			return false;
		}

		// Token: 0x04000C0D RID: 3085
		private readonly ObjectItemCollection _objectCollection;

		// Token: 0x04000C0E RID: 3086
		private readonly EdmItemCollection _edmCollection;

		// Token: 0x04000C0F RID: 3087
		private Dictionary<string, int> _clrTypeIndexes = new Dictionary<string, int>(StringComparer.Ordinal);

		// Token: 0x04000C10 RID: 3088
		private Dictionary<string, int> _edmTypeIndexes = new Dictionary<string, int>(StringComparer.Ordinal);

		// Token: 0x04000C11 RID: 3089
		private readonly object _lock = new object();
	}
}
