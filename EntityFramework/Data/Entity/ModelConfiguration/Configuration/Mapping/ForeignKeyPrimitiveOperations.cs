using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007B2 RID: 1970
	internal static class ForeignKeyPrimitiveOperations
	{
		// Token: 0x06005935 RID: 22837 RVA: 0x0017F90E File Offset: 0x0017DB0E
		public static void UpdatePrincipalTables(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, bool isMappingAnyInheritedProperty)
		{
			if (fromTable != toTable)
			{
				ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, toTable, entityType, false);
				if (isMappingAnyInheritedProperty)
				{
					ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, toTable, (EntityType)entityType.BaseType, true);
				}
			}
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0017F970 File Offset: 0x0017DB70
		private static void UpdatePrincipalTables(DbDatabaseMapping databaseMapping, EntityType toTable, EntityType entityType, bool removeFks)
		{
			foreach (AssociationType associationType in from at in databaseMapping.Model.AssociationTypes
			where at.SourceEnd.GetEntityType().Equals(entityType) || at.TargetEnd.GetEntityType().Equals(entityType)
			select at)
			{
				ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, toTable, removeFks, associationType, entityType);
			}
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x0017FBB0 File Offset: 0x0017DDB0
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static void UpdatePrincipalTables(DbDatabaseMapping databaseMapping, EntityType toTable, bool removeFks, AssociationType associationType, EntityType et)
		{
			List<AssociationEndMember> list = new List<AssociationEndMember>();
			AssociationEndMember item;
			AssociationEndMember associationEndMember;
			if (associationType.TryGuessPrincipalAndDependentEnds(out item, out associationEndMember))
			{
				list.Add(item);
			}
			else if (associationType.SourceEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && associationType.TargetEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				list.Add(associationType.SourceEnd);
				list.Add(associationType.TargetEnd);
			}
			else
			{
				list.Add(associationType.SourceEnd);
			}
			foreach (AssociationEndMember associationEndMember2 in list)
			{
				if (associationEndMember2.GetEntityType() == et)
				{
					IEnumerable<KeyValuePair<EntityType, IEnumerable<EdmProperty>>> enumerable;
					if (associationType.Constraint != null)
					{
						EntityType entityType = associationType.GetOtherEnd(associationEndMember2).GetEntityType();
						IEnumerable<EntityType> selfAndAllDerivedTypes = databaseMapping.Model.GetSelfAndAllDerivedTypes(entityType);
						enumerable = from df in (from t in selfAndAllDerivedTypes
						select databaseMapping.GetEntityTypeMapping(t) into dm
						where dm != null
						select dm).SelectMany((EntityTypeMapping dm) => from tmf in dm.MappingFragments
						where associationType.Constraint.ToProperties.All((EdmProperty p) => tmf.ColumnMappings.Any((ColumnMappingBuilder pm) => pm.PropertyPath.First<EdmProperty>() == p))
						select tmf).Distinct((MappingFragment f1, MappingFragment f2) => f1.Table == f2.Table)
						select new KeyValuePair<EntityType, IEnumerable<EdmProperty>>(df.Table, from pm in df.ColumnMappings
						where associationType.Constraint.ToProperties.Contains(pm.PropertyPath.First<EdmProperty>())
						select pm.ColumnProperty);
					}
					else
					{
						AssociationSetMapping associationSetMapping = databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().AssociationSetMappings.Single((AssociationSetMapping asm) => asm.AssociationSet.ElementType == associationType);
						EntityType table = associationSetMapping.Table;
						ReadOnlyCollection<ScalarPropertyMapping> source = (associationSetMapping.SourceEndMapping.AssociationEnd == associationEndMember2) ? associationSetMapping.SourceEndMapping.PropertyMappings : associationSetMapping.TargetEndMapping.PropertyMappings;
						IEnumerable<EdmProperty> value = from pm in source
						select pm.Column;
						enumerable = new KeyValuePair<EntityType, IEnumerable<EdmProperty>>[]
						{
							new KeyValuePair<EntityType, IEnumerable<EdmProperty>>(table, value)
						};
					}
					using (IEnumerator<KeyValuePair<EntityType, IEnumerable<EdmProperty>>> enumerator2 = enumerable.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<EntityType, IEnumerable<EdmProperty>> tableInfo = enumerator2.Current;
							KeyValuePair<EntityType, IEnumerable<EdmProperty>> tableInfo4 = tableInfo;
							foreach (ForeignKeyBuilder foreignKeyBuilder in tableInfo4.Key.ForeignKeyBuilders.Where(delegate(ForeignKeyBuilder fk)
							{
								IEnumerable<EdmProperty> dependentColumns = fk.DependentColumns;
								KeyValuePair<EntityType, IEnumerable<EdmProperty>> tableInfo3 = tableInfo;
								return dependentColumns.SequenceEqual(tableInfo3.Value);
							}).ToArray<ForeignKeyBuilder>())
							{
								if (removeFks)
								{
									KeyValuePair<EntityType, IEnumerable<EdmProperty>> tableInfo2 = tableInfo;
									tableInfo2.Key.RemoveForeignKey(foreignKeyBuilder);
								}
								else if (foreignKeyBuilder.GetAssociationType() == null || foreignKeyBuilder.GetAssociationType() == associationType)
								{
									foreignKeyBuilder.PrincipalTable = toTable;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x0017FF34 File Offset: 0x0017E134
		private static void MoveForeignKeyConstraint(EntityType fromTable, EntityType toTable, ForeignKeyBuilder fk)
		{
			fromTable.RemoveForeignKey(fk);
			if (fk.PrincipalTable == toTable)
			{
				if (fk.DependentColumns.All((EdmProperty c) => c.IsPrimaryKeyColumn))
				{
					return;
				}
			}
			EdmProperty[] sourceColumns = fk.DependentColumns.ToArray<EdmProperty>();
			IList<EdmProperty> dependentColumns = ForeignKeyPrimitiveOperations.GetDependentColumns(sourceColumns, toTable.Properties);
			if (!ForeignKeyPrimitiveOperations.ContainsEquivalentForeignKey(toTable, fk.PrincipalTable, dependentColumns))
			{
				toTable.AddForeignKey(fk);
				fk.DependentColumns = dependentColumns;
			}
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x0017FFBC File Offset: 0x0017E1BC
		private static void CopyForeignKeyConstraint(EdmModel database, EntityType toTable, ForeignKeyBuilder fk, Func<EdmProperty, EdmProperty> selector = null)
		{
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(database, database.EntityTypes.SelectMany((EntityType t) => t.ForeignKeyBuilders).UniquifyName(fk.Name))
			{
				PrincipalTable = fk.PrincipalTable,
				DeleteAction = fk.DeleteAction
			};
			foreignKeyBuilder.SetPreferredName(fk.Name);
			IList<EdmProperty> dependentColumns = ForeignKeyPrimitiveOperations.GetDependentColumns((selector != null) ? fk.DependentColumns.Select(selector) : fk.DependentColumns, toTable.Properties);
			if (!ForeignKeyPrimitiveOperations.ContainsEquivalentForeignKey(toTable, foreignKeyBuilder.PrincipalTable, dependentColumns))
			{
				toTable.AddForeignKey(foreignKeyBuilder);
				foreignKeyBuilder.DependentColumns = dependentColumns;
			}
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x00180094 File Offset: 0x0017E294
		private static bool ContainsEquivalentForeignKey(EntityType dependentTable, EntityType principalTable, IEnumerable<EdmProperty> columns)
		{
			return dependentTable.ForeignKeyBuilders.Any((ForeignKeyBuilder fk) => fk.PrincipalTable == principalTable && fk.DependentColumns.SequenceEqual(columns));
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x00180164 File Offset: 0x0017E364
		private static IList<EdmProperty> GetDependentColumns(IEnumerable<EdmProperty> sourceColumns, IEnumerable<EdmProperty> destinationColumns)
		{
			return (from sc in sourceColumns
			select destinationColumns.SingleOrDefault((EdmProperty dc) => string.Equals(dc.Name, sc.Name, StringComparison.Ordinal)) ?? destinationColumns.Single((EdmProperty dc) => string.Equals(dc.GetUnpreferredUniqueName(), sc.Name, StringComparison.Ordinal))).ToList<EdmProperty>();
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00180248 File Offset: 0x0017E448
		private static IEnumerable<ForeignKeyBuilder> FindAllForeignKeyConstraintsForColumn(EntityType fromTable, EntityType toTable, EdmProperty column)
		{
			return from fk in fromTable.ForeignKeyBuilders
			where fk.DependentColumns.Contains(column) && fk.DependentColumns.All((EdmProperty c) => toTable.Properties.Any((EdmProperty nc) => string.Equals(nc.Name, c.Name, StringComparison.Ordinal) || string.Equals(nc.GetUnpreferredUniqueName(), c.Name, StringComparison.Ordinal)))
			select fk;
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x001802BC File Offset: 0x0017E4BC
		public static void CopyAllForeignKeyConstraintsForColumn(EdmModel database, EntityType fromTable, EntityType toTable, EdmProperty column, EdmProperty movedColumn)
		{
			ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, column).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
			{
				ForeignKeyPrimitiveOperations.CopyForeignKeyConstraint(database, toTable, fk, delegate(EdmProperty c)
				{
					if (c != column)
					{
						return c;
					}
					return movedColumn;
				});
			});
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x00180368 File Offset: 0x0017E568
		public static void MoveAllDeclaredForeignKeyConstraintsForPrimaryKeyColumns(EntityType entityType, EntityType fromTable, EntityType toTable)
		{
			foreach (EdmProperty column in fromTable.KeyProperties)
			{
				ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, column).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
				{
					AssociationType associationType = fk.GetAssociationType();
					if (associationType != null && associationType.Constraint.ToRole.GetEntityType() == entityType && !fk.GetIsTypeConstraint())
					{
						ForeignKeyPrimitiveOperations.MoveForeignKeyConstraint(fromTable, toTable, fk);
					}
				});
			}
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0018042C File Offset: 0x0017E62C
		public static void CopyAllForeignKeyConstraintsForPrimaryKeyColumns(EdmModel database, EntityType fromTable, EntityType toTable)
		{
			foreach (EdmProperty column in fromTable.KeyProperties)
			{
				ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, column).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
				{
					if (!fk.GetIsTypeConstraint())
					{
						ForeignKeyPrimitiveOperations.CopyForeignKeyConstraint(database, toTable, fk, null);
					}
				});
			}
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x001804D4 File Offset: 0x0017E6D4
		public static void MoveAllForeignKeyConstraintsForColumn(EntityType fromTable, EntityType toTable, EdmProperty column)
		{
			ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, column).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
			{
				ForeignKeyPrimitiveOperations.MoveForeignKeyConstraint(fromTable, toTable, fk);
			});
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x001805E8 File Offset: 0x0017E7E8
		public static void RemoveAllForeignKeyConstraintsForColumn(EntityType table, EdmProperty column, DbDatabaseMapping databaseMapping)
		{
			(from fk in table.ForeignKeyBuilders
			where fk.DependentColumns.Contains(column)
			select fk).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
			{
				table.RemoveForeignKey(fk);
				ForeignKeyBuilder foreignKeyBuilder = databaseMapping.Database.EntityTypes.SelectMany((EntityType t) => t.ForeignKeyBuilders).SingleOrDefault((ForeignKeyBuilder fk2) => object.Equals(fk2.GetPreferredName(), fk.Name));
				if (foreignKeyBuilder != null)
				{
					foreignKeyBuilder.Name = foreignKeyBuilder.GetPreferredName();
				}
			});
		}
	}
}
