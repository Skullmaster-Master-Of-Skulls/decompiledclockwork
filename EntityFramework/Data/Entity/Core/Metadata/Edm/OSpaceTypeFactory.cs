using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001FA RID: 506
	internal abstract class OSpaceTypeFactory
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060011AB RID: 4523
		public abstract List<Action> ReferenceResolutions { get; }

		// Token: 0x060011AC RID: 4524
		public abstract void LogLoadMessage(string message, EdmType relatedType);

		// Token: 0x060011AD RID: 4525
		public abstract void LogError(string errorMessage, EdmType relatedType);

		// Token: 0x060011AE RID: 4526
		public abstract void TrackClosure(Type type);

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060011AF RID: 4527
		public abstract Dictionary<EdmType, EdmType> CspaceToOspace { get; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060011B0 RID: 4528
		public abstract Dictionary<string, EdmType> LoadedTypes { get; }

		// Token: 0x060011B1 RID: 4529
		public abstract void AddToTypesInAssembly(EdmType type);

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004B61C File Offset: 0x0004981C
		public virtual EdmType TryCreateType(Type type, EdmType cspaceType)
		{
			if (Helper.IsEnumType(cspaceType) ^ type.IsEnum())
			{
				this.LogLoadMessage(Strings.Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch(cspaceType.FullName, cspaceType.FullName), cspaceType);
				return null;
			}
			EdmType result;
			if (Helper.IsEnumType(cspaceType))
			{
				this.TryCreateEnumType(type, (EnumType)cspaceType, out result);
				return result;
			}
			this.TryCreateStructuralType(type, (StructuralType)cspaceType, out result);
			return result;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0004B680 File Offset: 0x00049880
		private bool TryCreateEnumType(Type enumType, EnumType cspaceEnumType, out EdmType newOSpaceType)
		{
			newOSpaceType = null;
			if (!this.UnderlyingEnumTypesMatch(enumType, cspaceEnumType) || !this.EnumMembersMatch(enumType, cspaceEnumType))
			{
				return false;
			}
			newOSpaceType = new ClrEnumType(enumType, cspaceEnumType.NamespaceName, cspaceEnumType.Name);
			this.LoadedTypes.Add(enumType.FullName, newOSpaceType);
			return true;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0004B704 File Offset: 0x00049904
		private bool TryCreateStructuralType(Type type, StructuralType cspaceType, out EdmType newOSpaceType)
		{
			List<Action> list = new List<Action>();
			newOSpaceType = null;
			StructuralType ospaceType;
			if (Helper.IsEntityType(cspaceType))
			{
				ospaceType = new ClrEntityType(type, cspaceType.NamespaceName, cspaceType.Name);
			}
			else
			{
				ospaceType = new ClrComplexType(type, cspaceType.NamespaceName, cspaceType.Name);
			}
			if (cspaceType.BaseType != null)
			{
				if (!OSpaceTypeFactory.TypesMatchByConvention(type.BaseType(), cspaceType.BaseType))
				{
					string message = Strings.Validator_OSpace_Convention_BaseTypeIncompatible(type.BaseType().FullName, type.FullName, cspaceType.BaseType.FullName);
					this.LogLoadMessage(message, cspaceType);
					return false;
				}
				this.TrackClosure(type.BaseType());
				list.Add(delegate
				{
					ospaceType.BaseType = this.ResolveBaseType((StructuralType)cspaceType.BaseType, type);
				});
			}
			if (!this.TryCreateMembers(type, cspaceType, ospaceType, list))
			{
				return false;
			}
			this.LoadedTypes.Add(type.FullName, ospaceType);
			foreach (Action item in list)
			{
				this.ReferenceResolutions.Add(item);
			}
			newOSpaceType = ospaceType;
			return true;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004B8D0 File Offset: 0x00049AD0
		internal static bool TypesMatchByConvention(Type type, EdmType cspaceType)
		{
			return type.Name == cspaceType.Name;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004B8E4 File Offset: 0x00049AE4
		private bool UnderlyingEnumTypesMatch(Type enumType, EnumType cspaceEnumType)
		{
			PrimitiveType primitiveType;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(enumType.GetEnumUnderlyingType(), out primitiveType))
			{
				this.LogLoadMessage(Strings.Validator_UnsupportedEnumUnderlyingType(enumType.GetEnumUnderlyingType().FullName), cspaceEnumType);
				return false;
			}
			if (primitiveType.PrimitiveTypeKind != cspaceEnumType.UnderlyingType.PrimitiveTypeKind)
			{
				this.LogLoadMessage(Strings.Validator_OSpace_Convention_NonMatchingUnderlyingTypes, cspaceEnumType);
				return false;
			}
			return true;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0004B94C File Offset: 0x00049B4C
		private bool EnumMembersMatch(Type enumType, EnumType cspaceEnumType)
		{
			Type enumUnderlyingType = enumType.GetEnumUnderlyingType();
			IEnumerator<EnumMember> enumerator = (from m in cspaceEnumType.Members
			orderby m.Name
			select m).GetEnumerator();
			IEnumerator<string> enumerator2 = (from n in enumType.GetEnumNames()
			orderby n
			select n).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return true;
			}
			while (enumerator2.MoveNext())
			{
				if (enumerator.Current.Name == enumerator2.Current && enumerator.Current.Value.Equals(Convert.ChangeType(Enum.Parse(enumType, enumerator2.Current), enumUnderlyingType, CultureInfo.InvariantCulture)) && !enumerator.MoveNext())
				{
					return true;
				}
			}
			this.LogLoadMessage(Strings.Mapping_Enum_OCMapping_MemberMismatch(enumType.FullName, enumerator.Current.Name, enumerator.Current.Value, cspaceEnumType.FullName), cspaceEnumType);
			return false;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0004BA54 File Offset: 0x00049C54
		private bool TryCreateMembers(Type type, StructuralType cspaceType, StructuralType ospaceType, List<Action> referenceResolutionListForCurrentType)
		{
			IEnumerable<PropertyInfo> clrProperties = from p in (cspaceType.BaseType == null) ? type.GetRuntimeProperties() : type.GetDeclaredProperties()
			where !p.IsStatic()
			select p;
			return this.TryFindAndCreatePrimitiveProperties(type, cspaceType, ospaceType, clrProperties) && this.TryFindAndCreateEnumProperties(type, cspaceType, ospaceType, clrProperties, referenceResolutionListForCurrentType) && this.TryFindComplexProperties(type, cspaceType, ospaceType, clrProperties, referenceResolutionListForCurrentType) && this.TryFindNavigationProperties(type, cspaceType, ospaceType, clrProperties, referenceResolutionListForCurrentType);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004BB40 File Offset: 0x00049D40
		private bool TryFindComplexProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<EdmProperty, PropertyInfo>> list = new List<KeyValuePair<EdmProperty, PropertyInfo>>();
			using (IEnumerator<EdmProperty> enumerator = (from m in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
			where Helper.IsComplexType(m.TypeUsage.EdmType)
			select m).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(message, cspaceType);
						return false;
					}
					list.Add(new KeyValuePair<EdmProperty, PropertyInfo>(cspaceProperty, propertyInfo));
				}
			}
			foreach (KeyValuePair<EdmProperty, PropertyInfo> keyValuePair in list)
			{
				this.TrackClosure(keyValuePair.Value.PropertyType);
				StructuralType ot = ospaceType;
				EdmProperty cp = keyValuePair.Key;
				PropertyInfo clrp = keyValuePair.Value;
				referenceResolutionListForCurrentType.Add(delegate
				{
					this.CreateAndAddComplexType(type, ot, cp, clrp);
				});
			}
			return true;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004BD24 File Offset: 0x00049F24
		private bool TryFindNavigationProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<NavigationProperty, PropertyInfo>> list = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			using (ReadOnlyMetadataCollection<NavigationProperty>.Enumerator enumerator = cspaceType.GetDeclaredOnlyMembers<NavigationProperty>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NavigationProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.NonPrimitiveMemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(message, cspaceType);
						return false;
					}
					bool flag = cspaceProperty.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many;
					if (propertyInfo.CanRead && (!flag || propertyInfo.CanWriteExtended()))
					{
						list.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(cspaceProperty, propertyInfo));
					}
				}
			}
			foreach (KeyValuePair<NavigationProperty, PropertyInfo> keyValuePair in list)
			{
				this.TrackClosure(keyValuePair.Value.PropertyType);
				StructuralType ct = cspaceType;
				StructuralType ot = ospaceType;
				NavigationProperty cp = keyValuePair.Key;
				referenceResolutionListForCurrentType.Add(delegate
				{
					this.CreateAndAddNavigationProperty(ct, ot, cp);
				});
			}
			return true;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004BEAC File Offset: 0x0004A0AC
		private EdmType ResolveBaseType(StructuralType baseCSpaceType, Type type)
		{
			EdmType result;
			if (!this.CspaceToOspace.TryGetValue(baseCSpaceType, out result))
			{
				this.LogError(Strings.Validator_OSpace_Convention_BaseTypeNotLoaded(type, baseCSpaceType), baseCSpaceType);
			}
			return result;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0004BF04 File Offset: 0x0004A104
		private bool TryFindAndCreatePrimitiveProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties)
		{
			using (IEnumerator<EdmProperty> enumerator = (from p in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
			where Helper.IsPrimitiveType(p.TypeUsage.EdmType)
			select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(message, cspaceType);
						return false;
					}
					PrimitiveType propertyType;
					if (!OSpaceTypeFactory.TryGetPrimitiveType(propertyInfo.PropertyType, out propertyType))
					{
						string message2 = Strings.Validator_OSpace_Convention_NonPrimitiveTypeProperty(propertyInfo.Name, type.FullName, propertyInfo.PropertyType.FullName);
						this.LogLoadMessage(message2, cspaceType);
						return false;
					}
					if (!propertyInfo.CanRead || !propertyInfo.CanWriteExtended())
					{
						string message3 = Strings.Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(propertyInfo.Name, type.FullName, type.Assembly().FullName);
						this.LogLoadMessage(message3, cspaceType);
						return false;
					}
					OSpaceTypeFactory.AddScalarMember(type, propertyInfo, ospaceType, cspaceProperty, propertyType);
				}
			}
			return true;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004C06C File Offset: 0x0004A26C
		protected static bool TryGetPrimitiveType(Type type, out PrimitiveType primitiveType)
		{
			return ClrProviderManifest.Instance.TryGetPrimitiveType(Nullable.GetUnderlyingType(type) ?? type, out primitiveType);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004C0EC File Offset: 0x0004A2EC
		private bool TryFindAndCreateEnumProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<EdmProperty, PropertyInfo>> list = new List<KeyValuePair<EdmProperty, PropertyInfo>>();
			using (IEnumerator<EdmProperty> enumerator = (from p in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
			where Helper.IsEnumType(p.TypeUsage.EdmType)
			select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(message, cspaceType);
						return false;
					}
					list.Add(new KeyValuePair<EdmProperty, PropertyInfo>(cspaceProperty, propertyInfo));
				}
			}
			foreach (KeyValuePair<EdmProperty, PropertyInfo> keyValuePair in list)
			{
				this.TrackClosure(keyValuePair.Value.PropertyType);
				StructuralType ot = ospaceType;
				EdmProperty cp = keyValuePair.Key;
				PropertyInfo clrp = keyValuePair.Value;
				referenceResolutionListForCurrentType.Add(delegate
				{
					this.CreateAndAddEnumProperty(type, ot, cp, clrp);
				});
			}
			return true;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004C290 File Offset: 0x0004A490
		private static bool MemberMatchesByConvention(PropertyInfo clrProperty, EdmMember cspaceMember)
		{
			return clrProperty.Name == cspaceMember.Name;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004C2A4 File Offset: 0x0004A4A4
		private void CreateAndAddComplexType(Type type, StructuralType ospaceType, EdmProperty cspaceProperty, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (this.CspaceToOspace.TryGetValue(cspaceProperty.TypeUsage.EdmType, out edmType))
			{
				EdmProperty member = new EdmProperty(cspaceProperty.Name, TypeUsage.Create(edmType, new FacetValues
				{
					Nullable = new bool?(false)
				}), clrProperty, type);
				ospaceType.AddMember(member);
				return;
			}
			this.LogError(Strings.Validator_OSpace_Convention_MissingOSpaceType(cspaceProperty.TypeUsage.EdmType.FullName), cspaceProperty.TypeUsage.EdmType);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004C326 File Offset: 0x0004A526
		private static bool NonPrimitiveMemberMatchesByConvention(PropertyInfo clrProperty, EdmMember cspaceMember)
		{
			return !clrProperty.PropertyType.IsValueType() && !clrProperty.PropertyType.IsAssignableFrom(typeof(string)) && clrProperty.Name == cspaceMember.Name;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004C3C8 File Offset: 0x0004A5C8
		private void CreateAndAddNavigationProperty(StructuralType cspaceType, StructuralType ospaceType, NavigationProperty cspaceProperty)
		{
			EdmType edmType;
			if (this.CspaceToOspace.TryGetValue(cspaceProperty.RelationshipType, out edmType))
			{
				EdmType edmType2 = null;
				if (Helper.IsCollectionType(cspaceProperty.TypeUsage.EdmType))
				{
					EdmType edmType3;
					bool flag = this.CspaceToOspace.TryGetValue(((CollectionType)cspaceProperty.TypeUsage.EdmType).TypeUsage.EdmType, out edmType3);
					if (flag)
					{
						edmType2 = edmType3.GetCollectionType();
					}
				}
				else
				{
					EdmType edmType4;
					bool flag = this.CspaceToOspace.TryGetValue(cspaceProperty.TypeUsage.EdmType, out edmType4);
					if (flag)
					{
						edmType2 = edmType4;
					}
				}
				NavigationProperty navigationProperty = new NavigationProperty(cspaceProperty.Name, TypeUsage.Create(edmType2));
				RelationshipType relationshipType = (RelationshipType)edmType;
				navigationProperty.RelationshipType = relationshipType;
				navigationProperty.ToEndMember = (RelationshipEndMember)relationshipType.Members.First((EdmMember e) => e.Name == cspaceProperty.ToEndMember.Name);
				navigationProperty.FromEndMember = (RelationshipEndMember)relationshipType.Members.First((EdmMember e) => e.Name == cspaceProperty.FromEndMember.Name);
				ospaceType.AddMember(navigationProperty);
				return;
			}
			EntityTypeBase entityTypeBase = (from e in cspaceProperty.RelationshipType.RelationshipEndMembers
			select ((RefType)e.TypeUsage.EdmType).ElementType).First((EntityTypeBase e) => e != cspaceType);
			this.LogError(Strings.Validator_OSpace_Convention_RelationshipNotLoaded(cspaceProperty.RelationshipType.FullName, entityTypeBase.FullName), entityTypeBase);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0004C590 File Offset: 0x0004A790
		private void CreateAndAddEnumProperty(Type type, StructuralType ospaceType, EdmProperty cspaceProperty, PropertyInfo clrProperty)
		{
			EdmType propertyType;
			if (!this.CspaceToOspace.TryGetValue(cspaceProperty.TypeUsage.EdmType, out propertyType))
			{
				this.LogError(Strings.Validator_OSpace_Convention_MissingOSpaceType(cspaceProperty.TypeUsage.EdmType.FullName), cspaceProperty.TypeUsage.EdmType);
				return;
			}
			if (clrProperty.CanRead && clrProperty.CanWriteExtended())
			{
				OSpaceTypeFactory.AddScalarMember(type, clrProperty, ospaceType, cspaceProperty, propertyType);
				return;
			}
			this.LogError(Strings.Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(clrProperty.Name, type.FullName, type.Assembly().FullName), cspaceProperty.TypeUsage.EdmType);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0004C62C File Offset: 0x0004A82C
		private static void AddScalarMember(Type type, PropertyInfo clrProperty, StructuralType ospaceType, EdmProperty cspaceProperty, EdmType propertyType)
		{
			StructuralType declaringType = cspaceProperty.DeclaringType;
			bool flag = Helper.IsEntityType(declaringType) && ((EntityType)declaringType).KeyMemberNames.Contains(clrProperty.Name);
			bool value = !flag && (!clrProperty.PropertyType.IsValueType() || Nullable.GetUnderlyingType(clrProperty.PropertyType) != null);
			EdmProperty member = new EdmProperty(cspaceProperty.Name, TypeUsage.Create(propertyType, new FacetValues
			{
				Nullable = new bool?(value)
			}), clrProperty, type);
			if (flag)
			{
				((EntityType)ospaceType).AddKeyMember(member);
				return;
			}
			ospaceType.AddMember(member);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004C6D4 File Offset: 0x0004A8D4
		public virtual void CreateRelationships(EdmItemCollection edmItemCollection)
		{
			foreach (AssociationType associationType in edmItemCollection.GetItems<AssociationType>())
			{
				if (!this.CspaceToOspace.ContainsKey(associationType))
				{
					EdmType[] array = new EdmType[2];
					if (this.CspaceToOspace.TryGetValue(OSpaceTypeFactory.GetRelationshipEndType(associationType.RelationshipEndMembers[0]), out array[0]) && this.CspaceToOspace.TryGetValue(OSpaceTypeFactory.GetRelationshipEndType(associationType.RelationshipEndMembers[1]), out array[1]))
					{
						AssociationType associationType2 = new AssociationType(associationType.Name, associationType.NamespaceName, associationType.IsForeignKey, DataSpace.OSpace);
						for (int i = 0; i < associationType.RelationshipEndMembers.Count; i++)
						{
							EntityType entityType = (EntityType)array[i];
							RelationshipEndMember relationshipEndMember = associationType.RelationshipEndMembers[i];
							associationType2.AddKeyMember(new AssociationEndMember(relationshipEndMember.Name, entityType.GetReferenceType(), relationshipEndMember.RelationshipMultiplicity));
						}
						this.AddToTypesInAssembly(associationType2);
						this.LoadedTypes.Add(associationType2.FullName, associationType2);
						this.CspaceToOspace.Add(associationType, associationType2);
					}
				}
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004C828 File Offset: 0x0004AA28
		private static StructuralType GetRelationshipEndType(RelationshipEndMember relationshipEndMember)
		{
			return ((RefType)relationshipEndMember.TypeUsage.EdmType).ElementType;
		}
	}
}
