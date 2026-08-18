using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BF RID: 1215
	internal class EdmValidator
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x000D9D51 File Offset: 0x000D7F51
		// (set) Token: 0x06002CB2 RID: 11442 RVA: 0x000D9D59 File Offset: 0x000D7F59
		internal bool SkipReadOnlyItems { get; set; }

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000D9D64 File Offset: 0x000D7F64
		public void Validate<T>(IEnumerable<T> items, List<EdmItemError> ospaceErrors) where T : EdmType
		{
			Check.NotNull<IEnumerable<T>>(items, "items");
			Check.NotNull<IEnumerable<T>>(items, "items");
			HashSet<MetadataItem> validatedItems = new HashSet<MetadataItem>();
			foreach (T t in items)
			{
				MetadataItem item = t;
				this.InternalValidate(item, ospaceErrors, validatedItems);
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000D9DD4 File Offset: 0x000D7FD4
		protected virtual void OnValidationError(ValidationErrorEventArgs e)
		{
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000D9DD8 File Offset: 0x000D7FD8
		private void AddError(List<EdmItemError> errors, EdmItemError newError)
		{
			ValidationErrorEventArgs validationErrorEventArgs = new ValidationErrorEventArgs(newError);
			this.OnValidationError(validationErrorEventArgs);
			errors.Add(validationErrorEventArgs.ValidationError);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000D9DFF File Offset: 0x000D7FFF
		protected virtual IEnumerable<EdmItemError> CustomValidate(MetadataItem item)
		{
			return null;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000D9E04 File Offset: 0x000D8004
		private void InternalValidate(MetadataItem item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if ((item.IsReadOnly && this.SkipReadOnlyItems) || validatedItems.Contains(item))
			{
				return;
			}
			validatedItems.Add(item);
			if (string.IsNullOrEmpty(item.Identity))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_EmptyIdentity));
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

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000D9FA4 File Offset: 0x000D81A4
		private void ValidateCollectionType(CollectionType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			if (item.BaseType != null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_CollectionTypesCannotHaveBaseType));
			}
			if (item.TypeUsage == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_CollectionHasNoTypeUsage));
				return;
			}
			this.InternalValidate(item.TypeUsage, errors, validatedItems);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000D9FFB File Offset: 0x000D81FB
		private void ValidateComplexType(ComplexType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateStructuralType(item, errors, validatedItems);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000DA008 File Offset: 0x000D8208
		[SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength")]
		private void ValidateEdmType(EdmType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeHasNoName));
			}
			if (item.NamespaceName == null || (item.DataSpace != DataSpace.OSpace && string.Empty == item.NamespaceName))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeHasNoNamespace));
			}
			if (item.BaseType != null)
			{
				this.InternalValidate(item.BaseType, errors, validatedItems);
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000DA088 File Offset: 0x000D8288
		private void ValidateEntityType(EntityType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.BaseType == null)
			{
				if (item.KeyMembers.Count < 1)
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_NoKeyMembers(item.FullName)));
				}
				else
				{
					foreach (EdmMember edmMember in item.KeyMembers)
					{
						EdmProperty edmProperty = (EdmProperty)edmMember;
						if (edmProperty.Nullable)
						{
							this.AddError(errors, new EdmItemError(Strings.Validator_NullableEntityKeyProperty(edmProperty.Name, item.FullName)));
						}
					}
				}
			}
			this.ValidateStructuralType(item, errors, validatedItems);
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000DA13C File Offset: 0x000D833C
		private void ValidateFacet(Facet item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_FacetHasNoName));
			}
			if (item.FacetType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_FacetTypeIsNull));
				return;
			}
			this.InternalValidate(item.FacetType, errors, validatedItems);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000DA198 File Offset: 0x000D8398
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

		// Token: 0x06002CBE RID: 11454 RVA: 0x000DA1F8 File Offset: 0x000D83F8
		private void ValidateEdmMember(EdmMember item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNoName));
			}
			if (item.DeclaringType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNullDeclaringType));
			}
			else
			{
				this.InternalValidate(item.DeclaringType, errors, validatedItems);
			}
			if (item.TypeUsage == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNullTypeUsage));
				return;
			}
			this.InternalValidate(item.TypeUsage, errors, validatedItems);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000DA280 File Offset: 0x000D8480
		private void ValidateMetadataProperty(MetadataProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.PropertyKind == PropertyKind.Extended)
			{
				this.ValidateItem(item, errors, validatedItems);
				if (string.IsNullOrEmpty(item.Name))
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_MetadataPropertyHasNoName));
				}
				if (item.TypeUsage == null)
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_ItemAttributeHasNullTypeUsage));
					return;
				}
				this.InternalValidate(item.TypeUsage, errors, validatedItems);
			}
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000DA2E5 File Offset: 0x000D84E5
		private void ValidateNavigationProperty(NavigationProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmMember(item, errors, validatedItems);
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000DA2F0 File Offset: 0x000D84F0
		private void ValidatePrimitiveType(PrimitiveType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateSimpleType(item, errors, validatedItems);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000DA2FB File Offset: 0x000D84FB
		private void ValidateEdmProperty(EdmProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmMember(item, errors, validatedItems);
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000DA308 File Offset: 0x000D8508
		private void ValidateRefType(RefType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			if (item.BaseType != null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_RefTypesCannotHaveBaseType));
			}
			if (item.ElementType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_RefTypeHasNullEntityType));
				return;
			}
			this.InternalValidate(item.ElementType, errors, validatedItems);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000DA35F File Offset: 0x000D855F
		private void ValidateSimpleType(SimpleType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000DA36C File Offset: 0x000D856C
		private void ValidateStructuralType(StructuralType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			Dictionary<string, EdmMember> dictionary = new Dictionary<string, EdmMember>();
			foreach (EdmMember edmMember in item.Members)
			{
				EdmMember edmMember2 = null;
				if (dictionary.TryGetValue(edmMember.Name, out edmMember2))
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_BaseTypeHasMemberOfSameName));
				}
				else
				{
					dictionary.Add(edmMember.Name, edmMember);
				}
				this.InternalValidate(edmMember, errors, validatedItems);
			}
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000DA404 File Offset: 0x000D8604
		private void ValidateTypeUsage(TypeUsage item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (item.EdmType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeUsageHasNullEdmType));
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
	}
}
