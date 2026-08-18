using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003A6 RID: 934
	internal abstract class BaseMetadataMappingVisitor
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x0009DE53 File Offset: 0x0009C053
		protected BaseMetadataMappingVisitor(bool sortSequence)
		{
			this._sortSequence = sortSequence;
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0009DE6C File Offset: 0x0009C06C
		protected virtual void Visit(EntityContainerMapping entityContainerMapping)
		{
			this.Visit(entityContainerMapping.EdmEntityContainer);
			this.Visit(entityContainerMapping.StorageEntityContainer);
			foreach (EntitySetBaseMapping setMapping in this.GetSequence<EntitySetBaseMapping>(entityContainerMapping.EntitySetMaps, (EntitySetBaseMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(setMapping);
			}
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0009DEF4 File Offset: 0x0009C0F4
		protected virtual void Visit(EntitySetBase entitySetBase)
		{
			BuiltInTypeKind builtInTypeKind = entitySetBase.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.AssociationSet)
			{
				this.Visit((AssociationSet)entitySetBase);
				return;
			}
			if (builtInTypeKind != BuiltInTypeKind.EntitySet)
			{
				return;
			}
			this.Visit((EntitySet)entitySetBase);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0009DF34 File Offset: 0x0009C134
		protected virtual void Visit(EntitySetBaseMapping setMapping)
		{
			foreach (TypeMapping typeMapping in this.GetSequence<TypeMapping>(setMapping.TypeMappings, (TypeMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(typeMapping);
			}
			this.Visit(setMapping.EntityContainerMapping);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0009DFB8 File Offset: 0x0009C1B8
		protected virtual void Visit(EntityContainer entityContainer)
		{
			foreach (EntitySetBase entitySetBase in this.GetSequence<EntitySetBase>(entityContainer.BaseEntitySets, (EntitySetBase it) => it.Identity))
			{
				this.Visit(entitySetBase);
			}
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0009E028 File Offset: 0x0009C228
		protected virtual void Visit(EntitySet entitySet)
		{
			this.Visit(entitySet.ElementType);
			this.Visit(entitySet.EntityContainer);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0009E04C File Offset: 0x0009C24C
		protected virtual void Visit(AssociationSet associationSet)
		{
			this.Visit(associationSet.ElementType);
			this.Visit(associationSet.EntityContainer);
			foreach (AssociationSetEnd associationSetEnd in this.GetSequence<AssociationSetEnd>(associationSet.AssociationSetEnds, (AssociationSetEnd it) => it.Identity))
			{
				this.Visit(associationSetEnd);
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0009E0F4 File Offset: 0x0009C2F4
		protected virtual void Visit(EntityType entityType)
		{
			foreach (EdmMember edmMember in this.GetSequence<EdmMember>(entityType.KeyMembers, (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember);
			}
			foreach (EdmMember edmMember2 in this.GetSequence<EdmMember>(entityType.GetDeclaredOnlyMembers<EdmMember>(), (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember2);
			}
			foreach (NavigationProperty navigationProperty in this.GetSequence<NavigationProperty>(entityType.NavigationProperties, (NavigationProperty it) => it.Identity))
			{
				this.Visit(navigationProperty);
			}
			foreach (EdmProperty edmProperty in this.GetSequence<EdmProperty>(entityType.Properties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty);
			}
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0009E2C0 File Offset: 0x0009C4C0
		protected virtual void Visit(AssociationType associationType)
		{
			foreach (AssociationEndMember associationEndMember in this.GetSequence<AssociationEndMember>(associationType.AssociationEndMembers, (AssociationEndMember it) => it.Identity))
			{
				this.Visit(associationEndMember);
			}
			this.Visit(associationType.BaseType);
			foreach (EdmMember edmMember in this.GetSequence<EdmMember>(associationType.KeyMembers, (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember);
			}
			foreach (EdmMember edmMember2 in this.GetSequence<EdmMember>(associationType.GetDeclaredOnlyMembers<EdmMember>(), (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember2);
			}
			foreach (ReferentialConstraint referentialConstraint in this.GetSequence<ReferentialConstraint>(associationType.ReferentialConstraints, (ReferentialConstraint it) => it.Identity))
			{
				this.Visit(referentialConstraint);
			}
			foreach (RelationshipEndMember relationshipEndMember in this.GetSequence<RelationshipEndMember>(associationType.RelationshipEndMembers, (RelationshipEndMember it) => it.Identity))
			{
				this.Visit(relationshipEndMember);
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0009E4D4 File Offset: 0x0009C6D4
		protected virtual void Visit(AssociationSetEnd associationSetEnd)
		{
			this.Visit(associationSetEnd.CorrespondingAssociationEndMember);
			this.Visit(associationSetEnd.EntitySet);
			this.Visit(associationSetEnd.ParentAssociationSet);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0009E4FA File Offset: 0x0009C6FA
		protected virtual void Visit(EdmProperty edmProperty)
		{
			this.Visit(edmProperty.TypeUsage);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0009E508 File Offset: 0x0009C708
		protected virtual void Visit(NavigationProperty navigationProperty)
		{
			this.Visit(navigationProperty.FromEndMember);
			this.Visit(navigationProperty.RelationshipType);
			this.Visit(navigationProperty.ToEndMember);
			this.Visit(navigationProperty.TypeUsage);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0009E53A File Offset: 0x0009C73A
		protected virtual void Visit(EdmMember edmMember)
		{
			this.Visit(edmMember.TypeUsage);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0009E548 File Offset: 0x0009C748
		protected virtual void Visit(AssociationEndMember associationEndMember)
		{
			this.Visit(associationEndMember.TypeUsage);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0009E568 File Offset: 0x0009C768
		protected virtual void Visit(ReferentialConstraint referentialConstraint)
		{
			foreach (EdmProperty edmProperty in this.GetSequence<EdmProperty>(referentialConstraint.FromProperties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty);
			}
			this.Visit(referentialConstraint.FromRole);
			foreach (EdmProperty edmProperty2 in this.GetSequence<EdmProperty>(referentialConstraint.ToProperties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty2);
			}
			this.Visit(referentialConstraint.ToRole);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0009E650 File Offset: 0x0009C850
		protected virtual void Visit(RelationshipEndMember relationshipEndMember)
		{
			this.Visit(relationshipEndMember.TypeUsage);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0009E668 File Offset: 0x0009C868
		protected virtual void Visit(TypeUsage typeUsage)
		{
			this.Visit(typeUsage.EdmType);
			foreach (Facet facet in this.GetSequence<Facet>(typeUsage.Facets, (Facet it) => it.Identity))
			{
				this.Visit(facet);
			}
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0009E6E4 File Offset: 0x0009C8E4
		protected virtual void Visit(RelationshipType relationshipType)
		{
			if (relationshipType == null)
			{
				return;
			}
			BuiltInTypeKind builtInTypeKind = relationshipType.BuiltInTypeKind;
			if (builtInTypeKind != BuiltInTypeKind.AssociationType)
			{
				return;
			}
			this.Visit((AssociationType)relationshipType);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0009E710 File Offset: 0x0009C910
		protected virtual void Visit(EdmType edmType)
		{
			if (edmType == null)
			{
				return;
			}
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind == BuiltInTypeKind.AssociationType)
				{
					this.Visit((AssociationType)edmType);
					return;
				}
				switch (builtInTypeKind)
				{
				case BuiltInTypeKind.CollectionType:
					this.Visit((CollectionType)edmType);
					return;
				case BuiltInTypeKind.CollectionKind:
					break;
				case BuiltInTypeKind.ComplexType:
					this.Visit((ComplexType)edmType);
					return;
				default:
					return;
				}
			}
			else
			{
				switch (builtInTypeKind)
				{
				case BuiltInTypeKind.EntityType:
					this.Visit((EntityType)edmType);
					return;
				case BuiltInTypeKind.EnumType:
					this.Visit((EnumType)edmType);
					break;
				case BuiltInTypeKind.EnumMember:
				case BuiltInTypeKind.Facet:
					break;
				case BuiltInTypeKind.EdmFunction:
					this.Visit((EdmFunction)edmType);
					return;
				default:
					if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
					{
						this.Visit((PrimitiveType)edmType);
						return;
					}
					if (builtInTypeKind != BuiltInTypeKind.RefType)
					{
						return;
					}
					this.Visit((RefType)edmType);
					return;
				}
			}
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0009E7D4 File Offset: 0x0009C9D4
		protected virtual void Visit(Facet facet)
		{
			this.Visit(facet.FacetType);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0009E7FC File Offset: 0x0009C9FC
		protected virtual void Visit(EdmFunction edmFunction)
		{
			this.Visit(edmFunction.BaseType);
			foreach (EntitySet entitySet in this.GetSequence<EntitySet>(edmFunction.EntitySets, (EntitySet it) => it.Identity))
			{
				if (entitySet != null)
				{
					this.Visit(entitySet);
				}
			}
			foreach (FunctionParameter functionParameter in this.GetSequence<FunctionParameter>(edmFunction.Parameters, (FunctionParameter it) => it.Identity))
			{
				this.Visit(functionParameter);
			}
			foreach (FunctionParameter functionParameter2 in this.GetSequence<FunctionParameter>(edmFunction.ReturnParameters, (FunctionParameter it) => it.Identity))
			{
				this.Visit(functionParameter2);
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0009E944 File Offset: 0x0009CB44
		protected virtual void Visit(PrimitiveType primitiveType)
		{
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0009E958 File Offset: 0x0009CB58
		protected virtual void Visit(ComplexType complexType)
		{
			this.Visit(complexType.BaseType);
			foreach (EdmMember edmMember in this.GetSequence<EdmMember>(complexType.Members, (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember);
			}
			foreach (EdmProperty edmProperty in this.GetSequence<EdmProperty>(complexType.Properties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty);
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0009EA34 File Offset: 0x0009CC34
		protected virtual void Visit(RefType refType)
		{
			this.Visit(refType.BaseType);
			this.Visit(refType.ElementType);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0009EA58 File Offset: 0x0009CC58
		protected virtual void Visit(EnumType enumType)
		{
			foreach (EnumMember enumMember in this.GetSequence<EnumMember>(enumType.Members, (EnumMember it) => it.Identity))
			{
				this.Visit(enumMember);
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0009EAC8 File Offset: 0x0009CCC8
		protected virtual void Visit(EnumMember enumMember)
		{
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0009EACA File Offset: 0x0009CCCA
		protected virtual void Visit(CollectionType collectionType)
		{
			this.Visit(collectionType.BaseType);
			this.Visit(collectionType.TypeUsage);
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0009EAE4 File Offset: 0x0009CCE4
		protected virtual void Visit(EntityTypeBase entityTypeBase)
		{
			if (entityTypeBase == null)
			{
				return;
			}
			BuiltInTypeKind builtInTypeKind = entityTypeBase.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.AssociationType)
			{
				this.Visit((AssociationType)entityTypeBase);
				return;
			}
			if (builtInTypeKind != BuiltInTypeKind.EntityType)
			{
				return;
			}
			this.Visit((EntityType)entityTypeBase);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0009EB1F File Offset: 0x0009CD1F
		protected virtual void Visit(FunctionParameter functionParameter)
		{
			this.Visit(functionParameter.DeclaringFunction);
			this.Visit(functionParameter.TypeUsage);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0009EB39 File Offset: 0x0009CD39
		protected virtual void Visit(DbProviderManifest providerManifest)
		{
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0009EB54 File Offset: 0x0009CD54
		protected virtual void Visit(TypeMapping typeMapping)
		{
			foreach (EntityTypeBase entityTypeBase in this.GetSequence<EntityTypeBase>(typeMapping.IsOfTypes, (EntityTypeBase it) => it.Identity))
			{
				this.Visit(entityTypeBase);
			}
			foreach (MappingFragment mappingFragment in this.GetSequence<MappingFragment>(typeMapping.MappingFragments, (MappingFragment it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(mappingFragment);
			}
			this.Visit(typeMapping.SetMapping);
			foreach (EntityTypeBase entityTypeBase2 in this.GetSequence<EntityTypeBase>(typeMapping.Types, (EntityTypeBase it) => it.Identity))
			{
				this.Visit(entityTypeBase2);
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0009ECA0 File Offset: 0x0009CEA0
		protected virtual void Visit(MappingFragment mappingFragment)
		{
			foreach (PropertyMapping propertyMapping in this.GetSequence<PropertyMapping>(mappingFragment.AllProperties, (PropertyMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(propertyMapping);
			}
			this.Visit(mappingFragment.TableSet);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0009ED1C File Offset: 0x0009CF1C
		protected virtual void Visit(PropertyMapping propertyMapping)
		{
			if (propertyMapping.GetType() == typeof(ComplexPropertyMapping))
			{
				this.Visit((ComplexPropertyMapping)propertyMapping);
				return;
			}
			if (propertyMapping.GetType() == typeof(ConditionPropertyMapping))
			{
				this.Visit((ConditionPropertyMapping)propertyMapping);
				return;
			}
			if (propertyMapping.GetType() == typeof(ScalarPropertyMapping))
			{
				this.Visit((ScalarPropertyMapping)propertyMapping);
			}
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0009ED9C File Offset: 0x0009CF9C
		protected virtual void Visit(ComplexPropertyMapping complexPropertyMapping)
		{
			this.Visit(complexPropertyMapping.Property);
			foreach (ComplexTypeMapping complexTypeMapping in this.GetSequence<ComplexTypeMapping>(complexPropertyMapping.TypeMappings, (ComplexTypeMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(complexTypeMapping);
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0009EE18 File Offset: 0x0009D018
		protected virtual void Visit(ConditionPropertyMapping conditionPropertyMapping)
		{
			this.Visit(conditionPropertyMapping.Column);
			this.Visit(conditionPropertyMapping.Property);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0009EE32 File Offset: 0x0009D032
		protected virtual void Visit(ScalarPropertyMapping scalarPropertyMapping)
		{
			this.Visit(scalarPropertyMapping.Column);
			this.Visit(scalarPropertyMapping.Property);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0009EE64 File Offset: 0x0009D064
		protected virtual void Visit(ComplexTypeMapping complexTypeMapping)
		{
			foreach (PropertyMapping propertyMapping in this.GetSequence<PropertyMapping>(complexTypeMapping.AllProperties, (PropertyMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(propertyMapping);
			}
			foreach (ComplexType complexType in this.GetSequence<ComplexType>(complexTypeMapping.IsOfTypes, (ComplexType it) => it.Identity))
			{
				this.Visit(complexType);
			}
			foreach (ComplexType complexType2 in this.GetSequence<ComplexType>(complexTypeMapping.Types, (ComplexType it) => it.Identity))
			{
				this.Visit(complexType2);
			}
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0009EF9C File Offset: 0x0009D19C
		protected IEnumerable<T> GetSequence<T>(IEnumerable<T> sequence, Func<T, string> keySelector)
		{
			if (!this._sortSequence)
			{
				return sequence;
			}
			return sequence.OrderBy(keySelector, StringComparer.Ordinal);
		}

		// Token: 0x04000BDB RID: 3035
		private readonly bool _sortSequence;

		// Token: 0x020003A7 RID: 935
		internal static class IdentityHelper
		{
			// Token: 0x060021FC RID: 8700 RVA: 0x0009EFB4 File Offset: 0x0009D1B4
			public static string GetIdentity(EntitySetBaseMapping mapping)
			{
				return mapping.Set.Identity;
			}

			// Token: 0x060021FD RID: 8701 RVA: 0x0009EFC4 File Offset: 0x0009D1C4
			public static string GetIdentity(TypeMapping mapping)
			{
				EntityTypeMapping entityTypeMapping = mapping as EntityTypeMapping;
				if (entityTypeMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(entityTypeMapping);
				}
				AssociationTypeMapping mapping2 = (AssociationTypeMapping)mapping;
				return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(mapping2);
			}

			// Token: 0x060021FE RID: 8702 RVA: 0x0009F008 File Offset: 0x0009D208
			public static string GetIdentity(EntityTypeMapping mapping)
			{
				IOrderedEnumerable<string> first = (from it in mapping.Types
				select it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				IOrderedEnumerable<string> second = (from it in mapping.IsOfTypes
				select it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				return string.Join(",", first.Concat(second));
			}

			// Token: 0x060021FF RID: 8703 RVA: 0x0009F0C6 File Offset: 0x0009D2C6
			public static string GetIdentity(AssociationTypeMapping mapping)
			{
				return mapping.AssociationType.Identity;
			}

			// Token: 0x06002200 RID: 8704 RVA: 0x0009F0F4 File Offset: 0x0009D2F4
			public static string GetIdentity(ComplexTypeMapping mapping)
			{
				IOrderedEnumerable<string> first = (from it in mapping.AllProperties
				select BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)).OrderBy((string it) => it, StringComparer.Ordinal);
				IOrderedEnumerable<string> second = (from it in mapping.Types
				select it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				IOrderedEnumerable<string> second2 = (from it in mapping.IsOfTypes
				select it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				return string.Join(",", first.Concat(second).Concat(second2));
			}

			// Token: 0x06002201 RID: 8705 RVA: 0x0009F208 File Offset: 0x0009D408
			public static string GetIdentity(MappingFragment mapping)
			{
				return mapping.TableSet.Identity;
			}

			// Token: 0x06002202 RID: 8706 RVA: 0x0009F218 File Offset: 0x0009D418
			public static string GetIdentity(PropertyMapping mapping)
			{
				ScalarPropertyMapping scalarPropertyMapping = mapping as ScalarPropertyMapping;
				if (scalarPropertyMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(scalarPropertyMapping);
				}
				ComplexPropertyMapping complexPropertyMapping = mapping as ComplexPropertyMapping;
				if (complexPropertyMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(complexPropertyMapping);
				}
				EndPropertyMapping endPropertyMapping = mapping as EndPropertyMapping;
				if (endPropertyMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(endPropertyMapping);
				}
				ConditionPropertyMapping mapping2 = (ConditionPropertyMapping)mapping;
				return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(mapping2);
			}

			// Token: 0x06002203 RID: 8707 RVA: 0x0009F268 File Offset: 0x0009D468
			public static string GetIdentity(ScalarPropertyMapping mapping)
			{
				return string.Concat(new string[]
				{
					"ScalarProperty(Identity=",
					mapping.Property.Identity,
					",ColumnIdentity=",
					mapping.Column.Identity,
					")"
				});
			}

			// Token: 0x06002204 RID: 8708 RVA: 0x0009F2B6 File Offset: 0x0009D4B6
			public static string GetIdentity(ComplexPropertyMapping mapping)
			{
				return "ComplexProperty(Identity=" + mapping.Property.Identity + ")";
			}

			// Token: 0x06002205 RID: 8709 RVA: 0x0009F2D2 File Offset: 0x0009D4D2
			public static string GetIdentity(ConditionPropertyMapping mapping)
			{
				if (mapping.Property == null)
				{
					return "ConditionProperty(ColumnIdentity=" + mapping.Column.Identity + ")";
				}
				return "ConditionProperty(Identity=" + mapping.Property.Identity + ")";
			}

			// Token: 0x06002206 RID: 8710 RVA: 0x0009F311 File Offset: 0x0009D511
			public static string GetIdentity(EndPropertyMapping mapping)
			{
				return "EndProperty(Identity=" + mapping.AssociationEnd.Identity + ")";
			}
		}
	}
}
