using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x0200081D RID: 2077
	internal class AssociationTypeMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x06005D5D RID: 23901 RVA: 0x00193031 File Offset: 0x00191231
		public AssociationTypeMappingGenerator(DbProviderManifest providerManifest) : base(providerManifest)
		{
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x0019303A File Offset: 0x0019123A
		public void Generate(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			if (associationType.Constraint != null)
			{
				AssociationTypeMappingGenerator.GenerateForeignKeyAssociationType(associationType, databaseMapping);
				return;
			}
			if (associationType.IsManyToMany())
			{
				this.GenerateManyToManyAssociation(associationType, databaseMapping);
				return;
			}
			this.GenerateIndependentAssociationType(associationType, databaseMapping);
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0019309C File Offset: 0x0019129C
		private static void GenerateForeignKeyAssociationType(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			AssociationEndMember dependentEnd = associationType.Constraint.DependentEnd;
			AssociationEndMember otherEnd = associationType.GetOtherEnd(dependentEnd);
			EntityTypeMapping entityTypeMappingInHierarchy = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, otherEnd.GetEntityType());
			EntityTypeMapping dependentEntityTypeMapping = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, dependentEnd.GetEntityType());
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(databaseMapping.Database, associationType.Name)
			{
				PrincipalTable = entityTypeMappingInHierarchy.MappingFragments.Single<MappingFragment>().Table,
				DeleteAction = ((otherEnd.DeleteBehavior != OperationAction.None) ? otherEnd.DeleteBehavior : OperationAction.None)
			};
			dependentEntityTypeMapping.MappingFragments.Single<MappingFragment>().Table.AddForeignKey(foreignKeyBuilder);
			foreignKeyBuilder.DependentColumns = from dependentProperty in associationType.Constraint.ToProperties
			select dependentEntityTypeMapping.GetPropertyMapping(new EdmProperty[]
			{
				dependentProperty
			}).ColumnProperty;
			foreignKeyBuilder.SetAssociationType(associationType);
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x00193170 File Offset: 0x00191370
		private void GenerateManyToManyAssociation(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			EntityType entityType = associationType.SourceEnd.GetEntityType();
			EntityType entityType2 = associationType.TargetEnd.GetEntityType();
			EntityType dependentTable = databaseMapping.Database.AddTable(entityType.Name + entityType2.Name);
			AssociationSetMapping associationSetMapping = AssociationTypeMappingGenerator.GenerateAssociationSetMapping(associationType, databaseMapping, associationType.SourceEnd, associationType.TargetEnd, dependentTable);
			this.GenerateIndependentForeignKeyConstraint(databaseMapping, entityType, entityType2, dependentTable, associationSetMapping, associationSetMapping.SourceEndMapping, associationType.SourceEnd.Name, null, true);
			this.GenerateIndependentForeignKeyConstraint(databaseMapping, entityType2, entityType, dependentTable, associationSetMapping, associationSetMapping.TargetEndMapping, associationType.TargetEnd.Name, null, true);
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x00193204 File Offset: 0x00191404
		private void GenerateIndependentAssociationType(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			AssociationEndMember sourceEnd;
			AssociationEndMember targetEnd;
			if (!associationType.TryGuessPrincipalAndDependentEnds(out sourceEnd, out targetEnd))
			{
				if (!associationType.IsPrincipalConfigured())
				{
					throw Error.UnableToDeterminePrincipal(associationType.SourceEnd.GetEntityType().GetClrType(), associationType.TargetEnd.GetEntityType().GetClrType());
				}
				sourceEnd = associationType.SourceEnd;
				targetEnd = associationType.TargetEnd;
			}
			EntityTypeMapping entityTypeMappingInHierarchy = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, targetEnd.GetEntityType());
			EntityType table = entityTypeMappingInHierarchy.MappingFragments.First<MappingFragment>().Table;
			AssociationSetMapping associationSetMapping = AssociationTypeMappingGenerator.GenerateAssociationSetMapping(associationType, databaseMapping, sourceEnd, targetEnd, table);
			this.GenerateIndependentForeignKeyConstraint(databaseMapping, sourceEnd.GetEntityType(), targetEnd.GetEntityType(), table, associationSetMapping, associationSetMapping.SourceEndMapping, associationType.Name, sourceEnd, false);
			foreach (EdmProperty edmProperty in targetEnd.GetEntityType().KeyProperties())
			{
				associationSetMapping.TargetEndMapping.AddPropertyMapping(new ScalarPropertyMapping(edmProperty, entityTypeMappingInHierarchy.GetPropertyMapping(new EdmProperty[]
				{
					edmProperty
				}).ColumnProperty));
			}
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x0019331C File Offset: 0x0019151C
		private static AssociationSetMapping GenerateAssociationSetMapping(AssociationType associationType, DbDatabaseMapping databaseMapping, AssociationEndMember principalEnd, AssociationEndMember dependentEnd, EntityType dependentTable)
		{
			AssociationSetMapping associationSetMapping = databaseMapping.AddAssociationSetMapping(databaseMapping.Model.GetAssociationSet(associationType), databaseMapping.Database.GetEntitySet(dependentTable));
			associationSetMapping.StoreEntitySet = databaseMapping.Database.GetEntitySet(dependentTable);
			associationSetMapping.SourceEndMapping.AssociationEnd = principalEnd;
			associationSetMapping.TargetEndMapping.AssociationEnd = dependentEnd;
			return associationSetMapping;
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00193398 File Offset: 0x00191598
		private void GenerateIndependentForeignKeyConstraint(DbDatabaseMapping databaseMapping, EntityType principalEntityType, EntityType dependentEntityType, EntityType dependentTable, AssociationSetMapping associationSetMapping, EndPropertyMapping associationEndMapping, string name, AssociationEndMember principalEnd, bool isPrimaryKeyColumn = false)
		{
			EntityType table = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, principalEntityType).MappingFragments.Single<MappingFragment>().Table;
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(databaseMapping.Database, name)
			{
				PrincipalTable = table,
				DeleteAction = ((associationEndMapping.AssociationEnd.DeleteBehavior != OperationAction.None) ? associationEndMapping.AssociationEnd.DeleteBehavior : OperationAction.None)
			};
			NavigationProperty principalNavigationProperty = databaseMapping.Model.EntityTypes.SelectMany((EntityType e) => e.DeclaredNavigationProperties).SingleOrDefault((NavigationProperty n) => n.ResultEnd == principalEnd);
			dependentTable.AddForeignKey(foreignKeyBuilder);
			foreignKeyBuilder.DependentColumns = this.GenerateIndependentForeignKeyColumns(principalEntityType, dependentEntityType, associationSetMapping, associationEndMapping, dependentTable, isPrimaryKeyColumn, principalNavigationProperty);
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x00193770 File Offset: 0x00191970
		private IEnumerable<EdmProperty> GenerateIndependentForeignKeyColumns(EntityType principalEntityType, EntityType dependentEntityType, AssociationSetMapping associationSetMapping, EndPropertyMapping associationEndMapping, EntityType dependentTable, bool isPrimaryKeyColumn, NavigationProperty principalNavigationProperty)
		{
			foreach (EdmProperty property in principalEntityType.KeyProperties())
			{
				string columnName = ((principalNavigationProperty != null) ? principalNavigationProperty.Name : principalEntityType.Name) + "_" + property.Name;
				EdmProperty foreignKeyColumn = base.MapTableColumn(property, columnName, false);
				dependentTable.AddColumn(foreignKeyColumn);
				if (isPrimaryKeyColumn)
				{
					dependentTable.AddKeyMember(foreignKeyColumn);
				}
				foreignKeyColumn.Nullable = (associationEndMapping.AssociationEnd.IsOptional() || (associationEndMapping.AssociationEnd.IsRequired() && dependentEntityType.BaseType != null));
				foreignKeyColumn.StoreGeneratedPattern = StoreGeneratedPattern.None;
				yield return foreignKeyColumn;
				associationEndMapping.AddPropertyMapping(new ScalarPropertyMapping(property, foreignKeyColumn));
				if (foreignKeyColumn.Nullable)
				{
					associationSetMapping.AddCondition(new IsNullConditionMapping(foreignKeyColumn, false));
				}
			}
			yield break;
		}
	}
}
