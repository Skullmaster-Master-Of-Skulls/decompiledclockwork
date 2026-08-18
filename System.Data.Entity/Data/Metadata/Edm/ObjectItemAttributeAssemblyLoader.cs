using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200021C RID: 540
	internal sealed class ObjectItemAttributeAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x0007D0DC File Offset: 0x0007B2DC
		private new MutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (MutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0007D0E9 File Offset: 0x0007B2E9
		internal ObjectItemAttributeAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData) : base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0007D110 File Offset: 0x0007B310
		internal override void OnLevel1SessionProcessing()
		{
			foreach (Action action in this._referenceResolutions)
			{
				action();
			}
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0007D164 File Offset: 0x0007B364
		internal override void OnLevel2SessionProcessing()
		{
			foreach (Action action in this._unresolvedNavigationProperties)
			{
				action();
			}
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0007D1B8 File Offset: 0x0007B3B8
		internal override void Load()
		{
			base.Load();
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0007D1C0 File Offset: 0x0007B3C0
		protected override void AddToAssembliesLoaded()
		{
			base.SessionData.AssembliesLoaded.Add(base.SourceAssembly, this.CacheEntry);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0007D1E0 File Offset: 0x0007B3E0
		private bool TryGetLoadedType(Type clrType, out EdmType edmType)
		{
			if (base.SessionData.TypesInLoading.TryGetValue(clrType.FullName, out edmType) || this.TryGetCachedEdmType(clrType, out edmType))
			{
				if (edmType.ClrType != clrType)
				{
					base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NewTypeConflictsWithExistingType(clrType.AssemblyQualifiedName, edmType.ClrType.AssemblyQualifiedName), edmType));
					edmType = null;
					return false;
				}
				return true;
			}
			else
			{
				if (!clrType.IsGenericType)
				{
					edmType = null;
					return false;
				}
				Type genericTypeDefinition = clrType.GetGenericTypeDefinition();
				EdmType edmType2;
				if (!this.TryGetLoadedType(clrType.GetGenericArguments()[0], out edmType2))
				{
					return false;
				}
				if (typeof(IEnumerable).IsAssignableFrom(clrType))
				{
					EntityType entityType = edmType2 as EntityType;
					if (entityType == null)
					{
						return false;
					}
					edmType = entityType.GetCollectionType();
				}
				else
				{
					edmType = edmType2;
				}
				return true;
			}
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0007D2A8 File Offset: 0x0007B4A8
		private bool TryGetCachedEdmType(Type clrType, out EdmType edmType)
		{
			ImmutableAssemblyCacheEntry immutableAssemblyCacheEntry;
			if (base.SessionData.LockedAssemblyCache.TryGetValue(clrType.Assembly, out immutableAssemblyCacheEntry))
			{
				return immutableAssemblyCacheEntry.TryGetEdmType(clrType.FullName, out edmType);
			}
			edmType = null;
			return false;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0007D2E4 File Offset: 0x0007B4E4
		protected override void LoadTypesFromAssembly()
		{
			this.LoadRelationshipTypes();
			foreach (Type type in EntityUtil.GetTypesSpecial(base.SourceAssembly))
			{
				if (type.IsDefined(typeof(EdmTypeAttribute), false))
				{
					if (type.IsGenericType)
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.GenericTypeNotSupported(type.FullName), null));
					}
					else
					{
						this.LoadType(type);
					}
				}
			}
			if (this._referenceResolutions.Count != 0)
			{
				base.SessionData.RegisterForLevel1PostSessionProcessing(this);
			}
			if (this._unresolvedNavigationProperties.Count != 0)
			{
				base.SessionData.RegisterForLevel2PostSessionProcessing(this);
			}
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0007D38C File Offset: 0x0007B58C
		private void LoadRelationshipTypes()
		{
			foreach (EdmRelationshipAttribute edmRelationshipAttribute in base.SourceAssembly.GetCustomAttributes(typeof(EdmRelationshipAttribute), false))
			{
				if (!this.TryFindNullParametersInRelationshipAttribute(edmRelationshipAttribute))
				{
					bool flag = false;
					if (edmRelationshipAttribute.Role1Name == edmRelationshipAttribute.Role2Name)
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.SameRoleNameOnRelationshipAttribute(edmRelationshipAttribute.RelationshipName, edmRelationshipAttribute.Role2Name), null));
						flag = true;
					}
					if (!flag)
					{
						AssociationType associationType = new AssociationType(edmRelationshipAttribute.RelationshipName, edmRelationshipAttribute.RelationshipNamespaceName, edmRelationshipAttribute.IsForeignKey, DataSpace.OSpace);
						base.SessionData.TypesInLoading.Add(associationType.FullName, associationType);
						this.TrackClosure(edmRelationshipAttribute.Role1Type);
						this.TrackClosure(edmRelationshipAttribute.Role2Type);
						string r1Name = edmRelationshipAttribute.Role1Name;
						Type r1Type = edmRelationshipAttribute.Role1Type;
						RelationshipMultiplicity r1Multiplicity = edmRelationshipAttribute.Role1Multiplicity;
						this.AddTypeResolver(delegate
						{
							this.ResolveAssociationEnd(associationType, r1Name, r1Type, r1Multiplicity);
						});
						string r2Name = edmRelationshipAttribute.Role2Name;
						Type r2Type = edmRelationshipAttribute.Role2Type;
						RelationshipMultiplicity r2Multiplicity = edmRelationshipAttribute.Role2Multiplicity;
						this.AddTypeResolver(delegate
						{
							this.ResolveAssociationEnd(associationType, r2Name, r2Type, r2Multiplicity);
						});
						this.CacheEntry.TypesInAssembly.Add(associationType);
					}
				}
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0007D514 File Offset: 0x0007B714
		private void ResolveAssociationEnd(AssociationType associationType, string roleName, Type clrType, RelationshipMultiplicity multiplicity)
		{
			EntityType entityType;
			if (!this.TryGetRelationshipEndEntityType(clrType, out entityType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.RoleTypeInEdmRelationshipAttributeIsInvalidType(associationType.Name, roleName, clrType), null));
				return;
			}
			associationType.AddKeyMember(new AssociationEndMember(roleName, entityType.GetReferenceType(), multiplicity));
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0007D564 File Offset: 0x0007B764
		private void LoadType(Type clrType)
		{
			EdmType edmType = null;
			EdmTypeAttribute[] array = (EdmTypeAttribute[])clrType.GetCustomAttributes(typeof(EdmTypeAttribute), false);
			if (array.Length == 0)
			{
				return;
			}
			if (clrType.IsNested)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NestedClassNotSupported(clrType.FullName, clrType.Assembly.FullName), null));
				return;
			}
			EdmTypeAttribute edmTypeAttribute = array[0];
			string cspaceTypeName = string.IsNullOrEmpty(edmTypeAttribute.Name) ? clrType.Name : edmTypeAttribute.Name;
			if (string.IsNullOrEmpty(edmTypeAttribute.NamespaceName) && clrType.Namespace == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_TypeHasNoNamespace, edmType));
				return;
			}
			string cspaceNamespaceName = string.IsNullOrEmpty(edmTypeAttribute.NamespaceName) ? clrType.Namespace : edmTypeAttribute.NamespaceName;
			if (edmTypeAttribute.GetType() == typeof(EdmEntityTypeAttribute))
			{
				edmType = new ClrEntityType(clrType, cspaceNamespaceName, cspaceTypeName);
			}
			else if (edmTypeAttribute.GetType() == typeof(EdmComplexTypeAttribute))
			{
				edmType = new ClrComplexType(clrType, cspaceNamespaceName, cspaceTypeName);
			}
			else
			{
				PrimitiveType primitiveType;
				if (!ClrProviderManifest.Instance.TryGetPrimitiveType(clrType.GetEnumUnderlyingType(), out primitiveType))
				{
					base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_UnsupportedEnumUnderlyingType(clrType.GetEnumUnderlyingType().FullName), edmType));
					return;
				}
				edmType = new ClrEnumType(clrType, cspaceNamespaceName, cspaceTypeName);
			}
			this.CacheEntry.TypesInAssembly.Add(edmType);
			base.SessionData.TypesInLoading.Add(clrType.FullName, edmType);
			if (Helper.IsStructuralType(edmType))
			{
				if (Helper.IsEntityType(edmType))
				{
					this.TrackClosure(clrType.BaseType);
					this.AddTypeResolver(delegate
					{
						edmType.BaseType = this.ResolveBaseType(clrType.BaseType);
					});
				}
				this.LoadPropertiesFromType((StructuralType)edmType);
			}
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0007D7B8 File Offset: 0x0007B9B8
		private void AddTypeResolver(Action resolver)
		{
			this._referenceResolutions.Add(resolver);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0007D7C8 File Offset: 0x0007B9C8
		private EdmType ResolveBaseType(Type type)
		{
			EdmType result;
			if (type.GetCustomAttributes(typeof(EdmEntityTypeAttribute), false).Length != 0 && this.TryGetLoadedType(type, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0007D7F8 File Offset: 0x0007B9F8
		private bool TryFindNullParametersInRelationshipAttribute(EdmRelationshipAttribute roleAttribute)
		{
			if (roleAttribute.RelationshipName == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullRelationshipNameforEdmRelationshipAttribute(base.SourceAssembly.FullName), null));
				return true;
			}
			bool result = false;
			if (roleAttribute.RelationshipNamespaceName == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("RelationshipNamespaceName", roleAttribute.RelationshipName), null));
				result = true;
			}
			if (roleAttribute.Role1Name == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role1Name", roleAttribute.RelationshipName), null));
				result = true;
			}
			if (roleAttribute.Role1Type == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role1Type", roleAttribute.RelationshipName), null));
				result = true;
			}
			if (roleAttribute.Role2Name == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role2Name", roleAttribute.RelationshipName), null));
				result = true;
			}
			if (roleAttribute.Role2Type == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role2Type", roleAttribute.RelationshipName), null));
				result = true;
			}
			return result;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0007D934 File Offset: 0x0007BB34
		private bool TryGetRelationshipEndEntityType(Type type, out EntityType entityType)
		{
			if (type == null)
			{
				entityType = null;
				return false;
			}
			EdmType edmType;
			if (!this.TryGetLoadedType(type, out edmType) || !Helper.IsEntityType(edmType))
			{
				entityType = null;
				return false;
			}
			entityType = (EntityType)edmType;
			return true;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x0007D970 File Offset: 0x0007BB70
		private void LoadPropertiesFromType(StructuralType structuralType)
		{
			PropertyInfo[] properties = structuralType.ClrType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (PropertyInfo propertyInfo in properties)
			{
				EdmMember edmMember = null;
				bool flag = false;
				if (propertyInfo.IsDefined(typeof(EdmRelationshipNavigationPropertyAttribute), false))
				{
					PropertyInfo pi = propertyInfo;
					this._unresolvedNavigationProperties.Add(delegate
					{
						this.ResolveNavigationProperty(structuralType, pi);
					});
				}
				else if (propertyInfo.IsDefined(typeof(EdmScalarPropertyAttribute), false))
				{
					if ((Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType).IsEnum)
					{
						this.TrackClosure(propertyInfo.PropertyType);
						PropertyInfo local = propertyInfo;
						this.AddTypeResolver(delegate
						{
							this.ResolveEnumTypeProperty(structuralType, local);
						});
					}
					else
					{
						edmMember = this.LoadScalarProperty(structuralType.ClrType, propertyInfo, out flag);
					}
				}
				else if (propertyInfo.IsDefined(typeof(EdmComplexPropertyAttribute), false))
				{
					this.TrackClosure(propertyInfo.PropertyType);
					PropertyInfo local = propertyInfo;
					this.AddTypeResolver(delegate
					{
						this.ResolveComplexTypeProperty(structuralType, local);
					});
				}
				if (edmMember != null)
				{
					structuralType.AddMember(edmMember);
					if (Helper.IsEntityType(structuralType) && flag)
					{
						((EntityType)structuralType).AddKeyMember(edmMember);
					}
				}
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0007DB14 File Offset: 0x0007BD14
		internal void ResolveNavigationProperty(StructuralType declaringType, PropertyInfo propertyInfo)
		{
			object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(EdmRelationshipNavigationPropertyAttribute), false);
			EdmType edmType;
			if (!this.TryGetLoadedType(propertyInfo.PropertyType, out edmType) || (edmType.BuiltInTypeKind != BuiltInTypeKind.EntityType && edmType.BuiltInTypeKind != BuiltInTypeKind.CollectionType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_InvalidNavPropReturnType(propertyInfo.Name, propertyInfo.DeclaringType.FullName, propertyInfo.PropertyType.FullName), null));
				return;
			}
			EdmRelationshipNavigationPropertyAttribute edmRelationshipNavigationPropertyAttribute = (EdmRelationshipNavigationPropertyAttribute)customAttributes[0];
			EdmMember edmMember = null;
			EdmType edmType2;
			if (base.SessionData.TypesInLoading.TryGetValue(edmRelationshipNavigationPropertyAttribute.RelationshipNamespaceName + "." + edmRelationshipNavigationPropertyAttribute.RelationshipName, out edmType2) && Helper.IsAssociationType(edmType2))
			{
				AssociationType associationType = (AssociationType)edmType2;
				if (associationType != null)
				{
					NavigationProperty navigationProperty = new NavigationProperty(propertyInfo.Name, TypeUsage.Create(edmType), propertyInfo);
					navigationProperty.RelationshipType = associationType;
					edmMember = navigationProperty;
					if (associationType.Members[0].Name == edmRelationshipNavigationPropertyAttribute.TargetRoleName)
					{
						navigationProperty.ToEndMember = (RelationshipEndMember)associationType.Members[0];
						navigationProperty.FromEndMember = (RelationshipEndMember)associationType.Members[1];
					}
					else if (associationType.Members[1].Name == edmRelationshipNavigationPropertyAttribute.TargetRoleName)
					{
						navigationProperty.ToEndMember = (RelationshipEndMember)associationType.Members[1];
						navigationProperty.FromEndMember = (RelationshipEndMember)associationType.Members[0];
					}
					else
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.TargetRoleNameInNavigationPropertyNotValid(propertyInfo.Name, propertyInfo.DeclaringType.FullName, edmRelationshipNavigationPropertyAttribute.TargetRoleName, edmRelationshipNavigationPropertyAttribute.RelationshipName), navigationProperty));
						edmMember = null;
					}
					if (edmMember != null && ((RefType)navigationProperty.FromEndMember.TypeUsage.EdmType).ElementType.ClrType != declaringType.ClrType)
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NavigationPropertyRelationshipEndTypeMismatch(declaringType.FullName, navigationProperty.Name, associationType.FullName, navigationProperty.FromEndMember.Name, ((RefType)navigationProperty.FromEndMember.TypeUsage.EdmType).ElementType.ClrType), navigationProperty));
						edmMember = null;
					}
				}
			}
			else
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.RelationshipNameInNavigationPropertyNotValid(propertyInfo.Name, propertyInfo.DeclaringType.FullName, edmRelationshipNavigationPropertyAttribute.RelationshipName), declaringType));
			}
			if (edmMember != null)
			{
				declaringType.AddMember(edmMember);
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0007DDB8 File Offset: 0x0007BFB8
		private EdmMember LoadScalarProperty(Type clrType, PropertyInfo property, out bool isEntityKeyProperty)
		{
			EdmMember result = null;
			isEntityKeyProperty = false;
			PrimitiveType edmType;
			if (!base.TryGetPrimitiveType(property.PropertyType, out edmType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ScalarPropertyNotPrimitive(property.Name, property.DeclaringType.FullName, property.PropertyType.FullName), null));
			}
			else
			{
				object[] customAttributes = property.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false);
				isEntityKeyProperty = ((EdmScalarPropertyAttribute)customAttributes[0]).EntityKeyProperty;
				bool isNullable = ((EdmScalarPropertyAttribute)customAttributes[0]).IsNullable;
				result = new EdmProperty(property.Name, TypeUsage.Create(edmType, new FacetValues
				{
					Nullable = new bool?(isNullable)
				}), property, clrType.TypeHandle);
			}
			return result;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0007DE74 File Offset: 0x0007C074
		private void ResolveEnumTypeProperty(StructuralType declaringType, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (!this.TryGetLoadedType(clrProperty.PropertyType, out edmType) || !Helper.IsEnumType(edmType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ScalarPropertyNotPrimitive(clrProperty.Name, clrProperty.DeclaringType.FullName, clrProperty.PropertyType.FullName), null));
				return;
			}
			EdmScalarPropertyAttribute edmScalarPropertyAttribute = (EdmScalarPropertyAttribute)clrProperty.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false).Single<object>();
			EdmProperty member = new EdmProperty(clrProperty.Name, TypeUsage.Create(edmType, new FacetValues
			{
				Nullable = new bool?(edmScalarPropertyAttribute.IsNullable)
			}), clrProperty, declaringType.ClrType.TypeHandle);
			declaringType.AddMember(member);
			if (declaringType.BuiltInTypeKind == BuiltInTypeKind.EntityType && edmScalarPropertyAttribute.EntityKeyProperty)
			{
				((EntityType)declaringType).AddKeyMember(member);
			}
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0007DF4C File Offset: 0x0007C14C
		private void ResolveComplexTypeProperty(StructuralType type, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (!this.TryGetLoadedType(clrProperty.PropertyType, out edmType) || edmType.BuiltInTypeKind != BuiltInTypeKind.ComplexType)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ComplexPropertyNotComplex(clrProperty.Name, clrProperty.DeclaringType.FullName, clrProperty.PropertyType.FullName), null));
				return;
			}
			EdmProperty member = new EdmProperty(clrProperty.Name, TypeUsage.Create(edmType, new FacetValues
			{
				Nullable = new bool?(false)
			}), clrProperty, type.ClrType.TypeHandle);
			type.AddMember(member);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0007DFE8 File Offset: 0x0007C1E8
		private void TrackClosure(Type type)
		{
			if (base.SourceAssembly != type.Assembly && !this.CacheEntry.ClosureAssemblies.Contains(type.Assembly) && ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(type.Assembly) && (!type.IsGenericType || (!EntityUtil.IsAnICollection(type) && !(type.GetGenericTypeDefinition() == typeof(EntityReference<>)) && !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))))
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

		// Token: 0x06002359 RID: 9049 RVA: 0x0007E0A9 File Offset: 0x0007C2A9
		internal static bool IsSchemaAttributePresent(Assembly assembly)
		{
			return assembly.IsDefined(typeof(EdmSchemaAttribute), false);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0007E0BC File Offset: 0x0007C2BC
		internal static ObjectItemAssemblyLoader Create(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
			{
				return new ObjectItemAttributeAssemblyLoader(assembly, sessionData);
			}
			return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x04000FAB RID: 4011
		private readonly List<Action> _unresolvedNavigationProperties = new List<Action>();

		// Token: 0x04000FAC RID: 4012
		private List<Action> _referenceResolutions = new List<Action>();
	}
}
