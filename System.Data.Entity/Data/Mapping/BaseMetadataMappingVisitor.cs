using System;
using System.Data.Common;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000253 RID: 595
	internal abstract class BaseMetadataMappingVisitor
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x0008A2C4 File Offset: 0x000884C4
		protected virtual void Visit(StorageEntityContainerMapping storageEntityContainerMapping)
		{
			this.Visit(storageEntityContainerMapping.EdmEntityContainer);
			this.Visit(storageEntityContainerMapping.StorageEntityContainer);
			foreach (StorageSetMapping storageSetMapping in storageEntityContainerMapping.EntitySetMaps)
			{
				this.Visit(storageSetMapping);
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0008A32C File Offset: 0x0008852C
		protected virtual void Visit(EntitySetBase entitySetBase)
		{
			BuiltInTypeKind builtInTypeKind = entitySetBase.BuiltInTypeKind;
			if (builtInTypeKind != BuiltInTypeKind.AssociationSet)
			{
				if (builtInTypeKind == BuiltInTypeKind.EntitySet)
				{
					this.Visit((EntitySet)entitySetBase);
					return;
				}
			}
			else
			{
				this.Visit((AssociationSet)entitySetBase);
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0008A364 File Offset: 0x00088564
		protected virtual void Visit(StorageSetMapping storageSetMapping)
		{
			foreach (StorageTypeMapping storageTypeMapping in storageSetMapping.TypeMappings)
			{
				this.Visit(storageTypeMapping);
			}
			this.Visit(storageSetMapping.EntityContainerMapping);
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x0008A3C0 File Offset: 0x000885C0
		protected virtual void Visit(EntityContainer entityContainer)
		{
			foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
			{
				this.Visit(entitySetBase);
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0008A414 File Offset: 0x00088614
		protected virtual void Visit(EntitySet entitySet)
		{
			this.Visit(entitySet.ElementType);
			this.Visit(entitySet.EntityContainer);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0008A430 File Offset: 0x00088630
		protected virtual void Visit(AssociationSet associationSet)
		{
			this.Visit(associationSet.ElementType);
			this.Visit(associationSet.EntityContainer);
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				this.Visit(associationSetEnd);
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x0008A49C File Offset: 0x0008869C
		protected virtual void Visit(EntityType entityType)
		{
			foreach (EdmMember edmMember in entityType.KeyMembers)
			{
				this.Visit(edmMember);
			}
			foreach (EdmMember edmMember2 in entityType.GetDeclaredOnlyMembers<EdmMember>())
			{
				this.Visit(edmMember2);
			}
			foreach (NavigationProperty navigationProperty in entityType.NavigationProperties)
			{
				this.Visit(navigationProperty);
			}
			foreach (EdmProperty edmProperty in entityType.Properties)
			{
				this.Visit(edmProperty);
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0008A5BC File Offset: 0x000887BC
		protected virtual void Visit(AssociationType associationType)
		{
			foreach (AssociationEndMember associationEndMember in associationType.AssociationEndMembers)
			{
				this.Visit(associationEndMember);
			}
			this.Visit(associationType.BaseType);
			foreach (EdmMember edmMember in associationType.KeyMembers)
			{
				this.Visit(edmMember);
			}
			foreach (EdmMember edmMember2 in associationType.GetDeclaredOnlyMembers<EdmMember>())
			{
				this.Visit(edmMember2);
			}
			foreach (ReferentialConstraint referentialConstraint in associationType.ReferentialConstraints)
			{
				this.Visit(referentialConstraint);
			}
			foreach (RelationshipEndMember relationshipEndMember in associationType.RelationshipEndMembers)
			{
				this.Visit(relationshipEndMember);
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0008A72C File Offset: 0x0008892C
		protected virtual void Visit(AssociationSetEnd associationSetEnd)
		{
			this.Visit(associationSetEnd.CorrespondingAssociationEndMember);
			this.Visit(associationSetEnd.EntitySet);
			this.Visit(associationSetEnd.ParentAssociationSet);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x0008A752 File Offset: 0x00088952
		protected virtual void Visit(EdmProperty edmProperty)
		{
			this.Visit(edmProperty.TypeUsage);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x0008A760 File Offset: 0x00088960
		protected virtual void Visit(NavigationProperty navigationProperty)
		{
			this.Visit(navigationProperty.FromEndMember);
			this.Visit(navigationProperty.RelationshipType);
			this.Visit(navigationProperty.ToEndMember);
			this.Visit(navigationProperty.TypeUsage);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x0008A752 File Offset: 0x00088952
		protected virtual void Visit(EdmMember edmMember)
		{
			this.Visit(edmMember.TypeUsage);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0008A752 File Offset: 0x00088952
		protected virtual void Visit(AssociationEndMember associationEndMember)
		{
			this.Visit(associationEndMember.TypeUsage);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0008A794 File Offset: 0x00088994
		protected virtual void Visit(ReferentialConstraint referentialConstraint)
		{
			foreach (EdmProperty edmProperty in referentialConstraint.FromProperties)
			{
				this.Visit(edmProperty);
			}
			this.Visit(referentialConstraint.FromRole);
			foreach (EdmProperty edmProperty2 in referentialConstraint.ToProperties)
			{
				this.Visit(edmProperty2);
			}
			this.Visit(referentialConstraint.ToRole);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x0008A752 File Offset: 0x00088952
		protected virtual void Visit(RelationshipEndMember relationshipEndMember)
		{
			this.Visit(relationshipEndMember.TypeUsage);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x0008A844 File Offset: 0x00088A44
		protected virtual void Visit(TypeUsage typeUsage)
		{
			this.Visit(typeUsage.EdmType);
			foreach (Facet facet in typeUsage.Facets)
			{
				this.Visit(facet);
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x0008A8A4 File Offset: 0x00088AA4
		protected virtual void Visit(RelationshipType relationshipType)
		{
			if (relationshipType == null)
			{
				return;
			}
			BuiltInTypeKind builtInTypeKind = relationshipType.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.AssociationType)
			{
				this.Visit((AssociationType)relationshipType);
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0008A8CC File Offset: 0x00088ACC
		protected virtual void Visit(EdmType edmType)
		{
			if (edmType == null)
			{
				return;
			}
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind > BuiltInTypeKind.ComplexType)
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
				return;
			}
			if (builtInTypeKind == BuiltInTypeKind.AssociationType)
			{
				this.Visit((AssociationType)edmType);
				return;
			}
			if (builtInTypeKind == BuiltInTypeKind.CollectionType)
			{
				this.Visit((CollectionType)edmType);
				return;
			}
			if (builtInTypeKind != BuiltInTypeKind.ComplexType)
			{
				return;
			}
			this.Visit((ComplexType)edmType);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x0008A984 File Offset: 0x00088B84
		protected virtual void Visit(Facet facet)
		{
			this.Visit(facet.FacetType);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x0008A994 File Offset: 0x00088B94
		protected virtual void Visit(EdmFunction edmFunction)
		{
			this.Visit(edmFunction.BaseType);
			foreach (EntitySet entitySet in edmFunction.EntitySets)
			{
				if (entitySet != null)
				{
					this.Visit(entitySet);
				}
			}
			foreach (FunctionParameter functionParameter in edmFunction.Parameters)
			{
				this.Visit(functionParameter);
			}
			foreach (FunctionParameter functionParameter2 in edmFunction.ReturnParameters)
			{
				this.Visit(functionParameter2);
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void Visit(PrimitiveType primitiveType)
		{
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0008AA80 File Offset: 0x00088C80
		protected virtual void Visit(ComplexType complexType)
		{
			this.Visit(complexType.BaseType);
			foreach (EdmMember edmMember in complexType.Members)
			{
				this.Visit(edmMember);
			}
			foreach (EdmProperty edmProperty in complexType.Properties)
			{
				this.Visit(edmProperty);
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0008AB24 File Offset: 0x00088D24
		protected virtual void Visit(RefType refType)
		{
			this.Visit(refType.BaseType);
			this.Visit(refType.ElementType);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0008AB40 File Offset: 0x00088D40
		protected virtual void Visit(EnumType enumType)
		{
			foreach (EnumMember enumMember in enumType.Members)
			{
				this.Visit(enumMember);
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void Visit(EnumMember enumMember)
		{
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0008AB94 File Offset: 0x00088D94
		protected virtual void Visit(CollectionType collectionType)
		{
			this.Visit(collectionType.BaseType);
			this.Visit(collectionType.TypeUsage);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0008ABB0 File Offset: 0x00088DB0
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

		// Token: 0x06002536 RID: 9526 RVA: 0x0008ABEB File Offset: 0x00088DEB
		protected virtual void Visit(FunctionParameter functionParameter)
		{
			this.Visit(functionParameter.DeclaringFunction);
			this.Visit(functionParameter.TypeUsage);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void Visit(DbProviderManifest providerManifest)
		{
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0008AC08 File Offset: 0x00088E08
		protected virtual void Visit(StorageTypeMapping storageTypeMapping)
		{
			foreach (EdmType edmType in storageTypeMapping.IsOfTypes)
			{
				this.Visit(edmType);
			}
			foreach (StorageMappingFragment storageMappingFragment in storageTypeMapping.MappingFragments)
			{
				this.Visit(storageMappingFragment);
			}
			this.Visit(storageTypeMapping.SetMapping);
			foreach (EdmType edmType2 in storageTypeMapping.Types)
			{
				this.Visit(edmType2);
			}
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0008ACE0 File Offset: 0x00088EE0
		protected virtual void Visit(StorageMappingFragment storageMappingFragment)
		{
			foreach (StoragePropertyMapping storagePropertyMapping in storageMappingFragment.AllProperties)
			{
				this.Visit(storagePropertyMapping);
			}
			this.Visit(storageMappingFragment.TableSet);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0008AD3C File Offset: 0x00088F3C
		protected virtual void Visit(StoragePropertyMapping storagePropertyMapping)
		{
			if (storagePropertyMapping.GetType() == typeof(StorageComplexPropertyMapping))
			{
				this.Visit((StorageComplexPropertyMapping)storagePropertyMapping);
				return;
			}
			if (storagePropertyMapping.GetType() == typeof(StorageConditionPropertyMapping))
			{
				this.Visit((StorageConditionPropertyMapping)storagePropertyMapping);
				return;
			}
			if (storagePropertyMapping.GetType() == typeof(StorageScalarPropertyMapping))
			{
				this.Visit((StorageScalarPropertyMapping)storagePropertyMapping);
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0008ADB4 File Offset: 0x00088FB4
		protected virtual void Visit(StorageComplexPropertyMapping storageComplexPropertyMapping)
		{
			this.Visit(storageComplexPropertyMapping.EdmProperty);
			foreach (StorageComplexTypeMapping storageComplexTypeMapping in storageComplexPropertyMapping.TypeMappings)
			{
				this.Visit(storageComplexTypeMapping);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x0008AE10 File Offset: 0x00089010
		protected virtual void Visit(StorageConditionPropertyMapping storageConditionPropertyMapping)
		{
			this.Visit(storageConditionPropertyMapping.ColumnProperty);
			this.Visit(storageConditionPropertyMapping.EdmProperty);
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x0008AE2A File Offset: 0x0008902A
		protected virtual void Visit(StorageScalarPropertyMapping storageScalarPropertyMapping)
		{
			this.Visit(storageScalarPropertyMapping.ColumnProperty);
			this.Visit(storageScalarPropertyMapping.EdmProperty);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0008AE44 File Offset: 0x00089044
		protected virtual void Visit(StorageComplexTypeMapping storageComplexTypeMapping)
		{
			foreach (StoragePropertyMapping storagePropertyMapping in storageComplexTypeMapping.AllProperties)
			{
				this.Visit(storagePropertyMapping);
			}
			foreach (ComplexType complexType in storageComplexTypeMapping.IsOfTypes)
			{
				this.Visit(complexType);
			}
			foreach (ComplexType complexType2 in storageComplexTypeMapping.Types)
			{
				this.Visit(complexType2);
			}
		}
	}
}
