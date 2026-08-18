using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200051B RID: 1307
	internal sealed class ObjectItemAttributeAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x000EB194 File Offset: 0x000E9394
		private new MutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (MutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000EB1A1 File Offset: 0x000E93A1
		internal ObjectItemAttributeAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData) : base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000EB1C8 File Offset: 0x000E93C8
		internal override void OnLevel1SessionProcessing()
		{
			foreach (Action action in this._referenceResolutions)
			{
				action();
			}
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000EB21C File Offset: 0x000E941C
		internal override void OnLevel2SessionProcessing()
		{
			foreach (Action action in this._unresolvedNavigationProperties)
			{
				action();
			}
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000EB270 File Offset: 0x000E9470
		internal override void Load()
		{
			base.Load();
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000EB278 File Offset: 0x000E9478
		protected override void AddToAssembliesLoaded()
		{
			base.SessionData.AssembliesLoaded.Add(base.SourceAssembly, this.CacheEntry);
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000EB298 File Offset: 0x000E9498
		private bool TryGetLoadedType(Type clrType, out EdmType edmType)
		{
			if (base.SessionData.TypesInLoading.TryGetValue(clrType.FullName, out edmType) || this.TryGetCachedEdmType(clrType, out edmType))
			{
				if (edmType.ClrType != clrType)
				{
					base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NewTypeConflictsWithExistingType(clrType.AssemblyQualifiedName, edmType.ClrType.AssemblyQualifiedName)));
					edmType = null;
					return false;
				}
				return true;
			}
			else
			{
				if (!clrType.IsGenericType())
				{
					edmType = null;
					return false;
				}
				clrType.GetGenericTypeDefinition();
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

		// Token: 0x06003138 RID: 12600 RVA: 0x000EB360 File Offset: 0x000E9560
		private bool TryGetCachedEdmType(Type clrType, out EdmType edmType)
		{
			ImmutableAssemblyCacheEntry immutableAssemblyCacheEntry;
			if (base.SessionData.LockedAssemblyCache.TryGetValue(clrType.Assembly(), out immutableAssemblyCacheEntry))
			{
				return immutableAssemblyCacheEntry.TryGetEdmType(clrType.FullName, out edmType);
			}
			edmType = null;
			return false;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000EB39C File Offset: 0x000E959C
		protected override void LoadTypesFromAssembly()
		{
			this.LoadRelationshipTypes();
			foreach (Type type in base.SourceAssembly.GetAccessibleTypes())
			{
				if (type.GetCustomAttributes(false).Any<EdmTypeAttribute>())
				{
					if (type.IsGenericType())
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.GenericTypeNotSupported(type.FullName)));
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

		// Token: 0x0600313A RID: 12602 RVA: 0x000EB4B4 File Offset: 0x000E96B4
		private void LoadRelationshipTypes()
		{
			foreach (EdmRelationshipAttribute edmRelationshipAttribute in base.SourceAssembly.GetCustomAttributes<EdmRelationshipAttribute>())
			{
				if (!this.TryFindNullParametersInRelationshipAttribute(edmRelationshipAttribute))
				{
					bool flag = false;
					if (edmRelationshipAttribute.Role1Name == edmRelationshipAttribute.Role2Name)
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.SameRoleNameOnRelationshipAttribute(edmRelationshipAttribute.RelationshipName, edmRelationshipAttribute.Role2Name)));
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

		// Token: 0x0600313B RID: 12603 RVA: 0x000EB648 File Offset: 0x000E9848
		private void ResolveAssociationEnd(AssociationType associationType, string roleName, Type clrType, RelationshipMultiplicity multiplicity)
		{
			EntityType entityType;
			if (!this.TryGetRelationshipEndEntityType(clrType, out entityType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.RoleTypeInEdmRelationshipAttributeIsInvalidType(associationType.Name, roleName, clrType)));
				return;
			}
			associationType.AddKeyMember(new AssociationEndMember(roleName, entityType.GetReferenceType(), multiplicity));
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000EB6C4 File Offset: 0x000E98C4
		private void LoadType(Type clrType)
		{
			EdmType edmType = null;
			IEnumerable<EdmTypeAttribute> customAttributes = clrType.GetCustomAttributes(false);
			if (!customAttributes.Any<EdmTypeAttribute>())
			{
				return;
			}
			if (clrType.IsNested)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NestedClassNotSupported(clrType.FullName, clrType.Assembly().FullName)));
				return;
			}
			EdmTypeAttribute edmTypeAttribute = customAttributes.First<EdmTypeAttribute>();
			string cspaceTypeName = string.IsNullOrEmpty(edmTypeAttribute.Name) ? clrType.Name : edmTypeAttribute.Name;
			if (string.IsNullOrEmpty(edmTypeAttribute.NamespaceName) && clrType.Namespace == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_TypeHasNoNamespace));
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
					base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_UnsupportedEnumUnderlyingType(clrType.GetEnumUnderlyingType().FullName)));
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
					this.TrackClosure(clrType.BaseType());
					this.AddTypeResolver(delegate
					{
						edmType.BaseType = this.ResolveBaseType(clrType.BaseType());
					});
				}
				this.LoadPropertiesFromType((StructuralType)edmType);
			}
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000EB925 File Offset: 0x000E9B25
		private void AddTypeResolver(Action resolver)
		{
			this._referenceResolutions.Add(resolver);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000EB934 File Offset: 0x000E9B34
		private EdmType ResolveBaseType(Type type)
		{
			EdmType result;
			if (type.GetCustomAttributes(false).Any<EdmEntityTypeAttribute>() && this.TryGetLoadedType(type, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000EB960 File Offset: 0x000E9B60
		private bool TryFindNullParametersInRelationshipAttribute(EdmRelationshipAttribute roleAttribute)
		{
			if (roleAttribute.RelationshipName == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullRelationshipNameforEdmRelationshipAttribute(base.SourceAssembly.FullName)));
				return true;
			}
			bool result = false;
			if (roleAttribute.RelationshipNamespaceName == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("RelationshipNamespaceName", roleAttribute.RelationshipName)));
				result = true;
			}
			if (roleAttribute.Role1Name == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role1Name", roleAttribute.RelationshipName)));
				result = true;
			}
			if (roleAttribute.Role1Type == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role1Type", roleAttribute.RelationshipName)));
				result = true;
			}
			if (roleAttribute.Role2Name == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role2Name", roleAttribute.RelationshipName)));
				result = true;
			}
			if (roleAttribute.Role2Type == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role2Type", roleAttribute.RelationshipName)));
				result = true;
			}
			return result;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000EBA98 File Offset: 0x000E9C98
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

		// Token: 0x06003141 RID: 12609 RVA: 0x000EBB68 File Offset: 0x000E9D68
		private void LoadPropertiesFromType(StructuralType structuralType)
		{
			IEnumerable<PropertyInfo> enumerable = from p in structuralType.ClrType.GetDeclaredProperties()
			where !p.IsStatic()
			select p;
			foreach (PropertyInfo propertyInfo in enumerable)
			{
				EdmMember edmMember = null;
				bool flag = false;
				if (propertyInfo.GetCustomAttributes(false).Any<EdmRelationshipNavigationPropertyAttribute>())
				{
					PropertyInfo pi = propertyInfo;
					this._unresolvedNavigationProperties.Add(delegate
					{
						this.ResolveNavigationProperty(structuralType, pi);
					});
				}
				else if (propertyInfo.GetCustomAttributes(false).Any<EdmScalarPropertyAttribute>())
				{
					if ((Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType).IsEnum())
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
				else if (propertyInfo.GetCustomAttributes(false).Any<EdmComplexPropertyAttribute>())
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

		// Token: 0x06003142 RID: 12610 RVA: 0x000EBD48 File Offset: 0x000E9F48
		internal void ResolveNavigationProperty(StructuralType declaringType, PropertyInfo propertyInfo)
		{
			IEnumerable<EdmRelationshipNavigationPropertyAttribute> customAttributes = propertyInfo.GetCustomAttributes(false);
			EdmType edmType;
			if (!this.TryGetLoadedType(propertyInfo.PropertyType, out edmType) || (edmType.BuiltInTypeKind != BuiltInTypeKind.EntityType && edmType.BuiltInTypeKind != BuiltInTypeKind.CollectionType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_InvalidNavPropReturnType(propertyInfo.Name, propertyInfo.DeclaringType.FullName, propertyInfo.PropertyType.FullName)));
				return;
			}
			EdmRelationshipNavigationPropertyAttribute edmRelationshipNavigationPropertyAttribute = customAttributes.First<EdmRelationshipNavigationPropertyAttribute>();
			EdmMember edmMember = null;
			EdmType edmType2;
			if (base.SessionData.TypesInLoading.TryGetValue(edmRelationshipNavigationPropertyAttribute.RelationshipNamespaceName + "." + edmRelationshipNavigationPropertyAttribute.RelationshipName, out edmType2) && Helper.IsAssociationType(edmType2))
			{
				AssociationType associationType = (AssociationType)edmType2;
				if (associationType != null)
				{
					NavigationProperty navigationProperty = new NavigationProperty(propertyInfo.Name, TypeUsage.Create(edmType));
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
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.TargetRoleNameInNavigationPropertyNotValid(propertyInfo.Name, propertyInfo.DeclaringType.FullName, edmRelationshipNavigationPropertyAttribute.TargetRoleName, edmRelationshipNavigationPropertyAttribute.RelationshipName)));
						edmMember = null;
					}
					if (edmMember != null && ((RefType)navigationProperty.FromEndMember.TypeUsage.EdmType).ElementType.ClrType != declaringType.ClrType)
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NavigationPropertyRelationshipEndTypeMismatch(declaringType.FullName, navigationProperty.Name, associationType.FullName, navigationProperty.FromEndMember.Name, ((RefType)navigationProperty.FromEndMember.TypeUsage.EdmType).ElementType.ClrType)));
						edmMember = null;
					}
				}
			}
			else
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.RelationshipNameInNavigationPropertyNotValid(propertyInfo.Name, propertyInfo.DeclaringType.FullName, edmRelationshipNavigationPropertyAttribute.RelationshipName)));
			}
			if (edmMember != null)
			{
				declaringType.AddMember(edmMember);
			}
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000EBFDC File Offset: 0x000EA1DC
		private EdmMember LoadScalarProperty(Type clrType, PropertyInfo property, out bool isEntityKeyProperty)
		{
			EdmMember result = null;
			isEntityKeyProperty = false;
			PrimitiveType edmType;
			if (!ObjectItemAssemblyLoader.TryGetPrimitiveType(property.PropertyType, out edmType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ScalarPropertyNotPrimitive(property.Name, property.DeclaringType.FullName, property.PropertyType.FullName)));
			}
			else
			{
				IEnumerable<EdmScalarPropertyAttribute> customAttributes = property.GetCustomAttributes(false);
				isEntityKeyProperty = customAttributes.First<EdmScalarPropertyAttribute>().EntityKeyProperty;
				bool isNullable = customAttributes.First<EdmScalarPropertyAttribute>().IsNullable;
				result = new EdmProperty(property.Name, TypeUsage.Create(edmType, new FacetValues
				{
					Nullable = new bool?(isNullable)
				}), property, clrType);
			}
			return result;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000EC088 File Offset: 0x000EA288
		private void ResolveEnumTypeProperty(StructuralType declaringType, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (!this.TryGetLoadedType(clrProperty.PropertyType, out edmType) || !Helper.IsEnumType(edmType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ScalarPropertyNotPrimitive(clrProperty.Name, clrProperty.DeclaringType.FullName, clrProperty.PropertyType.FullName)));
				return;
			}
			EdmScalarPropertyAttribute edmScalarPropertyAttribute = clrProperty.GetCustomAttributes(false).Single<EdmScalarPropertyAttribute>();
			EdmProperty member = new EdmProperty(clrProperty.Name, TypeUsage.Create(edmType, new FacetValues
			{
				Nullable = new bool?(edmScalarPropertyAttribute.IsNullable)
			}), clrProperty, declaringType.ClrType);
			declaringType.AddMember(member);
			if (declaringType.BuiltInTypeKind == BuiltInTypeKind.EntityType && edmScalarPropertyAttribute.EntityKeyProperty)
			{
				((EntityType)declaringType).AddKeyMember(member);
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000EC14C File Offset: 0x000EA34C
		private void ResolveComplexTypeProperty(StructuralType type, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (!this.TryGetLoadedType(clrProperty.PropertyType, out edmType) || edmType.BuiltInTypeKind != BuiltInTypeKind.ComplexType)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ComplexPropertyNotComplex(clrProperty.Name, clrProperty.DeclaringType.FullName, clrProperty.PropertyType.FullName)));
				return;
			}
			EdmProperty member = new EdmProperty(clrProperty.Name, TypeUsage.Create(edmType, new FacetValues
			{
				Nullable = new bool?(false)
			}), clrProperty, type.ClrType);
			type.AddMember(member);
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000EC1E4 File Offset: 0x000EA3E4
		private void TrackClosure(Type type)
		{
			if (base.SourceAssembly != type.Assembly() && !this.CacheEntry.ClosureAssemblies.Contains(type.Assembly()) && ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(type.Assembly()) && (!type.IsGenericType() || (!EntityUtil.IsAnICollection(type) && !(type.GetGenericTypeDefinition() == typeof(EntityReference<>)) && !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))))
			{
				this.CacheEntry.ClosureAssemblies.Add(type.Assembly());
			}
			if (type.IsGenericType())
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					this.TrackClosure(type2);
				}
			}
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000EC2A5 File Offset: 0x000EA4A5
		internal static bool IsSchemaAttributePresent(Assembly assembly)
		{
			return assembly.GetCustomAttributes<EdmSchemaAttribute>().Any<EdmSchemaAttribute>();
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000EC2B2 File Offset: 0x000EA4B2
		internal static ObjectItemAssemblyLoader Create(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (!ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
			{
				return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
			}
			return new ObjectItemAttributeAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x04001298 RID: 4760
		private readonly List<Action> _unresolvedNavigationProperties = new List<Action>();

		// Token: 0x04001299 RID: 4761
		private readonly List<Action> _referenceResolutions = new List<Action>();
	}
}
