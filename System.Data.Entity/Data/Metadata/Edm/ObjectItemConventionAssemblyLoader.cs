using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200021E RID: 542
	internal sealed class ObjectItemConventionAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0007D0DC File Offset: 0x0007B2DC
		private new MutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (MutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0007E16C File Offset: 0x0007C36C
		internal ObjectItemConventionAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData) : base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
			base.SessionData.RegisterForLevel1PostSessionProcessing(this);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0007E194 File Offset: 0x0007C394
		protected override void LoadTypesFromAssembly()
		{
			foreach (Type type in EntityUtil.GetTypesSpecial(base.SourceAssembly))
			{
				EdmType edmType;
				if (this.TryGetCSpaceTypeMatch(type, out edmType))
				{
					EdmType edmType2;
					if (type.IsValueType && !type.IsEnum)
					{
						base.SessionData.LoadMessageLogger.LogLoadMessage(Strings.Validator_OSpace_Convention_Struct(edmType.FullName, type.FullName), edmType);
					}
					else if (this.TryCreateType(type, edmType, out edmType2))
					{
						this.CacheEntry.TypesInAssembly.Add(edmType2);
						if (!base.SessionData.CspaceToOspace.ContainsKey(edmType))
						{
							base.SessionData.CspaceToOspace.Add(edmType, edmType2);
						}
						else
						{
							EdmType edmType3 = base.SessionData.CspaceToOspace[edmType];
							base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AmbiguousClrType(edmType.Name, edmType3.ClrType.FullName, type.FullName), edmType3));
						}
					}
				}
			}
			if (base.SessionData.TypesInLoading.Count == 0)
			{
				base.SessionData.ObjectItemAssemblyLoaderFactory = null;
			}
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0007E2B8 File Offset: 0x0007C4B8
		protected override void AddToAssembliesLoaded()
		{
			base.SessionData.AssembliesLoaded.Add(base.SourceAssembly, this.CacheEntry);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0007E2D8 File Offset: 0x0007C4D8
		private bool TryGetCSpaceTypeMatch(Type type, out EdmType cspaceType)
		{
			KeyValuePair<EdmType, int> keyValuePair;
			if (base.SessionData.ConventionCSpaceTypeNames.TryGetValue(type.Name, out keyValuePair))
			{
				if (keyValuePair.Value == 1)
				{
					cspaceType = keyValuePair.Key;
					return true;
				}
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_MultipleTypesWithSameName(type.Name), keyValuePair.Key));
			}
			cspaceType = null;
			return false;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0007E340 File Offset: 0x0007C540
		private bool TryCreateType(Type type, EdmType cspaceType, out EdmType newOSpaceType)
		{
			newOSpaceType = null;
			if (Helper.IsEnumType(cspaceType) ^ type.IsEnum)
			{
				base.SessionData.LoadMessageLogger.LogLoadMessage(Strings.Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch(cspaceType.FullName, cspaceType.FullName), cspaceType);
				return false;
			}
			if (Helper.IsEnumType(cspaceType))
			{
				return this.TryCreateEnumType(type, (EnumType)cspaceType, out newOSpaceType);
			}
			return this.TryCreateStructuralType(type, (StructuralType)cspaceType, out newOSpaceType);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0007E3A8 File Offset: 0x0007C5A8
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
				if (!ObjectItemConventionAssemblyLoader.TypesMatchByConvention(type.BaseType, cspaceType.BaseType))
				{
					string message = Strings.Validator_OSpace_Convention_BaseTypeIncompatible(type.BaseType.FullName, type.FullName, cspaceType.BaseType.FullName);
					base.SessionData.LoadMessageLogger.LogLoadMessage(message, cspaceType);
					return false;
				}
				this.TrackClosure(type.BaseType);
				list.Add(delegate
				{
					ospaceType.BaseType = this.ResolveBaseType((StructuralType)cspaceType.BaseType, type);
				});
			}
			if (!this.TryCreateMembers(type, cspaceType, ospaceType, list))
			{
				return false;
			}
			base.SessionData.TypesInLoading.Add(type.FullName, ospaceType);
			foreach (Action item in list)
			{
				this._referenceResolutions.Add(item);
			}
			newOSpaceType = ospaceType;
			return true;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0007E564 File Offset: 0x0007C764
		private bool TryCreateEnumType(Type enumType, EnumType cspaceEnumType, out EdmType newOSpaceType)
		{
			newOSpaceType = null;
			if (!this.UnderlyingEnumTypesMatch(enumType, cspaceEnumType) || !this.EnumMembersMatch(enumType, cspaceEnumType))
			{
				return false;
			}
			newOSpaceType = new ClrEnumType(enumType, cspaceEnumType.NamespaceName, cspaceEnumType.Name);
			base.SessionData.TypesInLoading.Add(enumType.FullName, newOSpaceType);
			return true;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0007E5B8 File Offset: 0x0007C7B8
		private bool UnderlyingEnumTypesMatch(Type enumType, EnumType cspaceEnumType)
		{
			PrimitiveType primitiveType;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(enumType.GetEnumUnderlyingType(), out primitiveType))
			{
				base.SessionData.LoadMessageLogger.LogLoadMessage(Strings.Validator_UnsupportedEnumUnderlyingType(enumType.GetEnumUnderlyingType().FullName), cspaceEnumType);
				return false;
			}
			if (primitiveType.PrimitiveTypeKind != cspaceEnumType.UnderlyingType.PrimitiveTypeKind)
			{
				base.SessionData.LoadMessageLogger.LogLoadMessage(Strings.Validator_OSpace_Convention_NonMatchingUnderlyingTypes, cspaceEnumType);
				return false;
			}
			return true;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0007E628 File Offset: 0x0007C828
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
			base.SessionData.LoadMessageLogger.LogLoadMessage(Strings.Mapping_Enum_OCMapping_MemberMismatch(enumType.FullName, enumerator.Current.Name, enumerator.Current.Value, cspaceEnumType.FullName), cspaceEnumType);
			return false;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0007E734 File Offset: 0x0007C934
		internal override void OnLevel1SessionProcessing()
		{
			this.CreateRelationships();
			foreach (Action action in this._referenceResolutions)
			{
				action();
			}
			base.OnLevel1SessionProcessing();
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0007E794 File Offset: 0x0007C994
		private EdmType ResolveBaseType(StructuralType baseCSpaceType, Type type)
		{
			EdmType edmType;
			if (!base.SessionData.CspaceToOspace.TryGetValue(baseCSpaceType, out edmType))
			{
				string message = base.SessionData.LoadMessageLogger.CreateErrorMessageWithTypeSpecificLoadLogs(Strings.Validator_OSpace_Convention_BaseTypeNotLoaded(type, baseCSpaceType), baseCSpaceType);
				base.SessionData.EdmItemErrors.Add(new EdmItemError(message, edmType));
			}
			return edmType;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0007E7EC File Offset: 0x0007C9EC
		private bool TryCreateMembers(Type type, StructuralType cspaceType, StructuralType ospaceType, List<Action> referenceResolutionListForCurrentType)
		{
			BindingFlags bindingAttr = (cspaceType.BaseType == null) ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) : (BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = type.GetProperties(bindingAttr);
			return this.TryFindAndCreatePrimitiveProperties(type, cspaceType, ospaceType, properties) && this.TryFindAndCreateEnumProperties(type, cspaceType, ospaceType, properties, referenceResolutionListForCurrentType) && this.TryFindComplexProperties(type, cspaceType, ospaceType, properties, referenceResolutionListForCurrentType) && this.TryFindNavigationProperties(type, cspaceType, ospaceType, properties, referenceResolutionListForCurrentType);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x0007E850 File Offset: 0x0007CA50
		private bool TryFindComplexProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, PropertyInfo[] clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<EdmProperty, PropertyInfo>> list = new List<KeyValuePair<EdmProperty, PropertyInfo>>();
			using (IEnumerator<EdmProperty> enumerator = (from m in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
			where Helper.IsComplexType(m.TypeUsage.EdmType)
			select m).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => ObjectItemConventionAssemblyLoader.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						base.SessionData.LoadMessageLogger.LogLoadMessage(message, cspaceType);
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

		// Token: 0x0600236D RID: 9069 RVA: 0x0007E9E4 File Offset: 0x0007CBE4
		private bool TryFindNavigationProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, PropertyInfo[] clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<NavigationProperty, PropertyInfo>> list = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			using (ReadOnlyMetadataCollection<NavigationProperty>.Enumerator enumerator = cspaceType.GetDeclaredOnlyMembers<NavigationProperty>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NavigationProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => ObjectItemConventionAssemblyLoader.NonPrimitiveMemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						base.SessionData.LoadMessageLogger.LogLoadMessage(message, cspaceType);
						return false;
					}
					bool flag = cspaceProperty.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many;
					if (propertyInfo.CanRead && (!flag || propertyInfo.CanWrite))
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
				PropertyInfo clrp = keyValuePair.Value;
				referenceResolutionListForCurrentType.Add(delegate
				{
					this.CreateAndAddNavigationProperty(ct, ot, cp, clrp);
				});
			}
			return true;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0007EB74 File Offset: 0x0007CD74
		private void TrackClosure(Type type)
		{
			if (base.SourceAssembly != type.Assembly && !this.CacheEntry.ClosureAssemblies.Contains(type.Assembly) && (!type.IsGenericType || (!EntityUtil.IsAnICollection(type) && !(type.GetGenericTypeDefinition() == typeof(EntityReference<>)) && !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))))
			{
				this.CacheEntry.ClosureAssemblies.Add(type.Assembly);
			}
			if (type.IsGenericType)
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					this.TrackClosure(type2);
				}
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0007EC28 File Offset: 0x0007CE28
		private void CreateAndAddComplexType(Type type, StructuralType ospaceType, EdmProperty cspaceProperty, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (base.SessionData.CspaceToOspace.TryGetValue((StructuralType)cspaceProperty.TypeUsage.EdmType, out edmType))
			{
				EdmProperty member = new EdmProperty(cspaceProperty.Name, TypeUsage.Create(edmType, new FacetValues
				{
					Nullable = new bool?(false)
				}), clrProperty, type.TypeHandle);
				ospaceType.AddMember(member);
				return;
			}
			string message = base.SessionData.LoadMessageLogger.CreateErrorMessageWithTypeSpecificLoadLogs(Strings.Validator_OSpace_Convention_MissingOSpaceType(cspaceProperty.TypeUsage.EdmType.FullName), cspaceProperty.TypeUsage.EdmType);
			base.SessionData.EdmItemErrors.Add(new EdmItemError(message, ospaceType));
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0007ECDC File Offset: 0x0007CEDC
		private void CreateAndAddNavigationProperty(StructuralType cspaceType, StructuralType ospaceType, NavigationProperty cspaceProperty, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (base.SessionData.CspaceToOspace.TryGetValue(cspaceProperty.RelationshipType, out edmType))
			{
				EdmType edmType2 = null;
				if (Helper.IsCollectionType(cspaceProperty.TypeUsage.EdmType))
				{
					EdmType edmType3;
					bool flag = base.SessionData.CspaceToOspace.TryGetValue((StructuralType)((CollectionType)cspaceProperty.TypeUsage.EdmType).TypeUsage.EdmType, out edmType3);
					if (flag)
					{
						edmType2 = edmType3.GetCollectionType();
					}
				}
				else
				{
					EdmType edmType4;
					bool flag = base.SessionData.CspaceToOspace.TryGetValue((StructuralType)cspaceProperty.TypeUsage.EdmType, out edmType4);
					if (flag)
					{
						edmType2 = edmType4;
					}
				}
				ospaceType.AddMember(new NavigationProperty(cspaceProperty.Name, TypeUsage.Create(edmType2), clrProperty)
				{
					RelationshipType = (RelationshipType)edmType,
					ToEndMember = (RelationshipEndMember)((RelationshipType)edmType).Members.First((EdmMember e) => e.Name == cspaceProperty.ToEndMember.Name),
					FromEndMember = (RelationshipEndMember)((RelationshipType)edmType).Members.First((EdmMember e) => e.Name == cspaceProperty.FromEndMember.Name)
				});
				return;
			}
			EntityTypeBase entityTypeBase = (from e in cspaceProperty.RelationshipType.RelationshipEndMembers
			select ((RefType)e.TypeUsage.EdmType).ElementType).First((EntityTypeBase e) => e != cspaceType);
			string message = base.SessionData.LoadMessageLogger.CreateErrorMessageWithTypeSpecificLoadLogs(Strings.Validator_OSpace_Convention_RelationshipNotLoaded(cspaceProperty.RelationshipType.FullName, entityTypeBase.FullName), entityTypeBase);
			base.SessionData.EdmItemErrors.Add(new EdmItemError(message, ospaceType));
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0007EEBC File Offset: 0x0007D0BC
		private bool TryFindAndCreatePrimitiveProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, PropertyInfo[] clrProperties)
		{
			using (IEnumerator<EdmProperty> enumerator = (from p in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
			where Helper.IsPrimitiveType(p.TypeUsage.EdmType)
			select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => ObjectItemConventionAssemblyLoader.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						base.SessionData.LoadMessageLogger.LogLoadMessage(message, cspaceType);
						return false;
					}
					PrimitiveType propertyType;
					if (!base.TryGetPrimitiveType(propertyInfo.PropertyType, out propertyType))
					{
						string message2 = Strings.Validator_OSpace_Convention_NonPrimitiveTypeProperty(propertyInfo.Name, type.FullName, propertyInfo.PropertyType.FullName);
						base.SessionData.LoadMessageLogger.LogLoadMessage(message2, cspaceType);
						return false;
					}
					if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
					{
						string message3 = Strings.Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(propertyInfo.Name, type.FullName, type.Assembly.FullName);
						base.SessionData.LoadMessageLogger.LogLoadMessage(message3, cspaceType);
						return false;
					}
					this.AddScalarMember(type, propertyInfo, ospaceType, cspaceProperty, propertyType);
				}
			}
			return true;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0007F038 File Offset: 0x0007D238
		private bool TryFindAndCreateEnumProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, PropertyInfo[] clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<EdmProperty, PropertyInfo>> list = new List<KeyValuePair<EdmProperty, PropertyInfo>>();
			using (IEnumerator<EdmProperty> enumerator = (from p in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
			where Helper.IsEnumType(p.TypeUsage.EdmType)
			select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => ObjectItemConventionAssemblyLoader.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string message = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						base.SessionData.LoadMessageLogger.LogLoadMessage(message, cspaceType);
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

		// Token: 0x06002373 RID: 9075 RVA: 0x0007F1CC File Offset: 0x0007D3CC
		private void CreateAndAddEnumProperty(Type type, StructuralType ospaceType, EdmProperty cspaceProperty, PropertyInfo clrProperty)
		{
			EdmType propertyType;
			if (!base.SessionData.CspaceToOspace.TryGetValue(cspaceProperty.TypeUsage.EdmType, out propertyType))
			{
				string message = base.SessionData.LoadMessageLogger.CreateErrorMessageWithTypeSpecificLoadLogs(Strings.Validator_OSpace_Convention_MissingOSpaceType(cspaceProperty.TypeUsage.EdmType.FullName), cspaceProperty.TypeUsage.EdmType);
				base.SessionData.EdmItemErrors.Add(new EdmItemError(message, ospaceType));
				return;
			}
			if (clrProperty.CanRead && clrProperty.CanWrite)
			{
				this.AddScalarMember(type, clrProperty, ospaceType, cspaceProperty, propertyType);
				return;
			}
			string message2 = base.SessionData.LoadMessageLogger.CreateErrorMessageWithTypeSpecificLoadLogs(Strings.Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(clrProperty.Name, type.FullName, type.Assembly.FullName), cspaceProperty.TypeUsage.EdmType);
			base.SessionData.EdmItemErrors.Add(new EdmItemError(message2, ospaceType));
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0007F2B0 File Offset: 0x0007D4B0
		private void CreateRelationships()
		{
			if (base.SessionData.ConventionBasedRelationshipsAreLoaded)
			{
				return;
			}
			base.SessionData.ConventionBasedRelationshipsAreLoaded = true;
			foreach (AssociationType associationType in base.SessionData.EdmItemCollection.GetItems<AssociationType>())
			{
				if (!base.SessionData.CspaceToOspace.ContainsKey(associationType))
				{
					EdmType[] array = new EdmType[2];
					if (base.SessionData.CspaceToOspace.TryGetValue(ObjectItemConventionAssemblyLoader.GetRelationshipEndType(associationType.RelationshipEndMembers[0]), out array[0]) && base.SessionData.CspaceToOspace.TryGetValue(ObjectItemConventionAssemblyLoader.GetRelationshipEndType(associationType.RelationshipEndMembers[1]), out array[1]))
					{
						AssociationType associationType2 = new AssociationType(associationType.Name, associationType.NamespaceName, associationType.IsForeignKey, DataSpace.OSpace);
						for (int i = 0; i < associationType.RelationshipEndMembers.Count; i++)
						{
							EntityType entityType = (EntityType)array[i];
							RelationshipEndMember relationshipEndMember = associationType.RelationshipEndMembers[i];
							associationType2.AddKeyMember(new AssociationEndMember(relationshipEndMember.Name, entityType.GetReferenceType(), relationshipEndMember.RelationshipMultiplicity));
						}
						this.CacheEntry.TypesInAssembly.Add(associationType2);
						base.SessionData.TypesInLoading.Add(associationType2.FullName, associationType2);
						base.SessionData.CspaceToOspace.Add(associationType, associationType2);
					}
				}
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0007F44C File Offset: 0x0007D64C
		private static StructuralType GetRelationshipEndType(RelationshipEndMember relationshipEndMember)
		{
			return ((RefType)relationshipEndMember.TypeUsage.EdmType).ElementType;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0007F463 File Offset: 0x0007D663
		private static bool MemberMatchesByConvention(PropertyInfo clrProperty, EdmMember cspaceMember)
		{
			return clrProperty.Name == cspaceMember.Name;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0007F476 File Offset: 0x0007D676
		private static bool NonPrimitiveMemberMatchesByConvention(PropertyInfo clrProperty, EdmMember cspaceMember)
		{
			return !clrProperty.PropertyType.IsValueType && !clrProperty.PropertyType.IsAssignableFrom(typeof(string)) && clrProperty.Name == cspaceMember.Name;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0007F4AF File Offset: 0x0007D6AF
		internal static bool SessionContainsConventionParameters(ObjectItemLoadingSessionData sessionData)
		{
			return sessionData.EdmItemCollection != null;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0007F4BA File Offset: 0x0007D6BA
		internal static bool TypesMatchByConvention(Type type, EdmType cspaceType)
		{
			return type.Name == cspaceType.Name;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0007F4D0 File Offset: 0x0007D6D0
		private void AddScalarMember(Type type, PropertyInfo clrProperty, StructuralType ospaceType, EdmProperty cspaceProperty, EdmType propertyType)
		{
			StructuralType declaringType = cspaceProperty.DeclaringType;
			bool flag = Helper.IsEntityType(declaringType) && ((EntityType)declaringType).KeyMemberNames.Contains(clrProperty.Name);
			bool value = !flag && (!clrProperty.PropertyType.IsValueType || Nullable.GetUnderlyingType(clrProperty.PropertyType) != null);
			EdmProperty member = new EdmProperty(cspaceProperty.Name, TypeUsage.Create(propertyType, new FacetValues
			{
				Nullable = new bool?(value)
			}), clrProperty, type.TypeHandle);
			if (flag)
			{
				((EntityType)ospaceType).AddKeyMember(member);
				return;
			}
			ospaceType.AddMember(member);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0007F577 File Offset: 0x0007D777
		internal static ObjectItemAssemblyLoader Create(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (!ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
			{
				return new ObjectItemConventionAssemblyLoader(assembly, sessionData);
			}
			sessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AttributeAssemblyReferenced(assembly.FullName), null));
			return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x04000FAD RID: 4013
		private const BindingFlags RootEntityPropertyReflectionBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x04000FAE RID: 4014
		private List<Action> _referenceResolutions = new List<Action>();
	}
}
