using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001BF RID: 447
	internal class EdmValidator
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0006D8DC File Offset: 0x0006BADC
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x0006D8E4 File Offset: 0x0006BAE4
		internal bool SkipReadOnlyItems
		{
			get
			{
				return this._skipReadOnlyItems;
			}
			set
			{
				this._skipReadOnlyItems = value;
			}
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0006D8F0 File Offset: 0x0006BAF0
		public void Validate<T>(IEnumerable<T> items, List<EdmItemError> ospaceErrors) where T : EdmType
		{
			EntityUtil.CheckArgumentNull<IEnumerable<T>>(items, "items");
			EntityUtil.CheckArgumentNull<IEnumerable<T>>(items, "ospaceErrors");
			HashSet<MetadataItem> validatedItems = new HashSet<MetadataItem>();
			foreach (T t in items)
			{
				MetadataItem item = t;
				this.InternalValidate(item, ospaceErrors, validatedItems);
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void OnValidationError(ValidationErrorEventArgs e)
		{
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0006D960 File Offset: 0x0006BB60
		private void AddError(List<EdmItemError> errors, EdmItemError newError)
		{
			ValidationErrorEventArgs validationErrorEventArgs = new ValidationErrorEventArgs(newError);
			this.OnValidationError(validationErrorEventArgs);
			errors.Add(validationErrorEventArgs.ValidationError);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00006174 File Offset: 0x00004374
		protected virtual IEnumerable<EdmItemError> CustomValidate(MetadataItem item)
		{
			return null;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0006D988 File Offset: 0x0006BB88
		private void InternalValidate(MetadataItem item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if ((item.IsReadOnly && this.SkipReadOnlyItems) || validatedItems.Contains(item))
			{
				return;
			}
			validatedItems.Add(item);
			if (string.IsNullOrEmpty(item.Identity))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_EmptyIdentity, item));
			}
			switch (item.BuiltInTypeKind)
			{
			case BuiltInTypeKind.CollectionType:
				this.ValidateCollectionType((CollectionType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.ComplexType:
				this.ValidateComplexType((ComplexType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.EntityType:
				this.ValidateEntityType((EntityType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.Facet:
				this.ValidateFacet((Facet)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.MetadataProperty:
				this.ValidateMetadataProperty((MetadataProperty)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.NavigationProperty:
				this.ValidateNavigationProperty((NavigationProperty)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.PrimitiveType:
				this.ValidatePrimitiveType((PrimitiveType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.EdmProperty:
				this.ValidateEdmProperty((EdmProperty)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.RefType:
				this.ValidateRefType((RefType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.TypeUsage:
				this.ValidateTypeUsage((TypeUsage)item, errors, validatedItems);
				break;
			}
			IEnumerable<EdmItemError> enumerable = this.CustomValidate(item);
			if (enumerable != null)
			{
				errors.AddRange(enumerable);
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0006DB2C File Offset: 0x0006BD2C
		private void ValidateCollectionType(CollectionType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			if (item.BaseType != null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_CollectionTypesCannotHaveBaseType, item));
			}
			if (item.TypeUsage == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_CollectionHasNoTypeUsage, item));
				return;
			}
			this.InternalValidate(item.TypeUsage, errors, validatedItems);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0006DB85 File Offset: 0x0006BD85
		private void ValidateComplexType(ComplexType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateStructuralType(item, errors, validatedItems);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0006DB90 File Offset: 0x0006BD90
		private void ValidateEdmType(EdmType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeHasNoName, item));
			}
			if (item.NamespaceName == null || (item.DataSpace != DataSpace.OSpace && string.Empty == item.NamespaceName))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeHasNoNamespace, item));
			}
			if (item.BaseType != null)
			{
				this.InternalValidate(item.BaseType, errors, validatedItems);
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0006DC10 File Offset: 0x0006BE10
		private void ValidateEntityType(EntityType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.BaseType == null)
			{
				if (item.KeyMembers.Count < 1)
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_NoKeyMembers(item.FullName), item));
				}
				else
				{
					foreach (EdmMember edmMember in item.KeyMembers)
					{
						EdmProperty edmProperty = (EdmProperty)edmMember;
						if (edmProperty.Nullable)
						{
							this.AddError(errors, new EdmItemError(Strings.Validator_NullableEntityKeyProperty(edmProperty.Name, item.FullName), edmProperty));
						}
					}
				}
			}
			this.ValidateStructuralType(item, errors, validatedItems);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0006DCC4 File Offset: 0x0006BEC4
		private void ValidateFacet(Facet item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_FacetHasNoName, item));
			}
			if (item.FacetType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_FacetTypeIsNull, item));
				return;
			}
			this.InternalValidate(item.FacetType, errors, validatedItems);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0006DD24 File Offset: 0x0006BF24
		private void ValidateItem(MetadataItem item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.RawMetadataProperties != null)
			{
				foreach (MetadataProperty item2 in item.MetadataProperties)
				{
					this.InternalValidate(item2, errors, validatedItems);
				}
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0006DD84 File Offset: 0x0006BF84
		private void ValidateEdmMember(EdmMember item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNoName, item));
			}
			if (item.DeclaringType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNullDeclaringType, item));
			}
			else
			{
				this.InternalValidate(item.DeclaringType, errors, validatedItems);
			}
			if (item.TypeUsage == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNullTypeUsage, item));
				return;
			}
			this.InternalValidate(item.TypeUsage, errors, validatedItems);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0006DE0C File Offset: 0x0006C00C
		private void ValidateMetadataProperty(MetadataProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.PropertyKind == PropertyKind.Extended)
			{
				this.ValidateItem(item, errors, validatedItems);
				if (string.IsNullOrEmpty(item.Name))
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_MetadataPropertyHasNoName, item));
				}
				if (item.TypeUsage == null)
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_ItemAttributeHasNullTypeUsage, item));
					return;
				}
				this.InternalValidate(item.TypeUsage, errors, validatedItems);
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0006DE73 File Offset: 0x0006C073
		private void ValidateNavigationProperty(NavigationProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmMember(item, errors, validatedItems);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0006DE7E File Offset: 0x0006C07E
		private void ValidatePrimitiveType(PrimitiveType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateSimpleType(item, errors, validatedItems);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0006DE73 File Offset: 0x0006C073
		private void ValidateEdmProperty(EdmProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmMember(item, errors, validatedItems);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0006DE8C File Offset: 0x0006C08C
		private void ValidateRefType(RefType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			if (item.BaseType != null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_RefTypesCannotHaveBaseType, item));
			}
			if (item.ElementType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_RefTypeHasNullEntityType, null));
				return;
			}
			this.InternalValidate(item.ElementType, errors, validatedItems);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0006DEE5 File Offset: 0x0006C0E5
		private void ValidateSimpleType(SimpleType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0006DEF0 File Offset: 0x0006C0F0
		private void ValidateStructuralType(StructuralType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			Dictionary<string, EdmMember> dictionary = new Dictionary<string, EdmMember>();
			foreach (EdmMember edmMember in item.Members)
			{
				EdmMember edmMember2 = null;
				if (dictionary.TryGetValue(edmMember.Name, out edmMember2))
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_BaseTypeHasMemberOfSameName, item));
				}
				else
				{
					dictionary.Add(edmMember.Name, edmMember);
				}
				this.InternalValidate(edmMember, errors, validatedItems);
			}
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006DF88 File Offset: 0x0006C188
		private void ValidateTypeUsage(TypeUsage item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (item.EdmType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeUsageHasNullEdmType, item));
			}
			else
			{
				this.InternalValidate(item.EdmType, errors, validatedItems);
			}
			foreach (Facet item2 in item.Facets)
			{
				this.InternalValidate(item2, errors, validatedItems);
			}
		}

		// Token: 0x04000D13 RID: 3347
		private bool _skipReadOnlyItems;
	}
}
