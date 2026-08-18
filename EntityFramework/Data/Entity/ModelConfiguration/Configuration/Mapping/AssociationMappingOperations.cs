using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007B5 RID: 1973
	internal static class AssociationMappingOperations
	{
		// Token: 0x06005953 RID: 22867 RVA: 0x00180BD8 File Offset: 0x0017EDD8
		private static void MoveAssociationSetMappingDependents(AssociationSetMapping associationSetMapping, EndPropertyMapping dependentMapping, EntitySet toSet, bool useExistingColumns)
		{
			EntityType toTable = toSet.ElementType;
			dependentMapping.PropertyMappings.Each(delegate(ScalarPropertyMapping pm)
			{
				EdmProperty oldColumn = pm.Column;
				pm.Column = TableOperations.MoveColumnAndAnyConstraints(associationSetMapping.Table, toTable, oldColumn, useExistingColumns);
				(from cc in associationSetMapping.Conditions
				where cc.Column == oldColumn
				select cc).Each((ConditionPropertyMapping cc) => cc.Column = pm.Column);
			});
			associationSetMapping.StoreEntitySet = toSet;
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x00180D20 File Offset: 0x0017EF20
		public static void MoveAllDeclaredAssociationSetMappings(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, bool useExistingColumns)
		{
			foreach (AssociationSetMapping associationSetMapping in (from a in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping asm) => asm.AssociationSetMappings)
			where a.Table == fromTable && (a.AssociationSet.ElementType.SourceEnd.GetEntityType() == entityType || a.AssociationSet.ElementType.TargetEnd.GetEntityType() == entityType)
			select a).ToArray<AssociationSetMapping>())
			{
				AssociationEndMember associationEndMember;
				AssociationEndMember targetEnd;
				if (!associationSetMapping.AssociationSet.ElementType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out targetEnd))
				{
					targetEnd = associationSetMapping.AssociationSet.ElementType.TargetEnd;
				}
				if (targetEnd.GetEntityType() == entityType)
				{
					EndPropertyMapping endPropertyMapping = (targetEnd == associationSetMapping.TargetEndMapping.AssociationEnd) ? associationSetMapping.SourceEndMapping : associationSetMapping.TargetEndMapping;
					AssociationMappingOperations.MoveAssociationSetMappingDependents(associationSetMapping, endPropertyMapping, databaseMapping.Database.GetEntitySet(toTable), useExistingColumns);
					EndPropertyMapping endPropertyMapping2 = (endPropertyMapping == associationSetMapping.TargetEndMapping) ? associationSetMapping.SourceEndMapping : associationSetMapping.TargetEndMapping;
					endPropertyMapping2.PropertyMappings.Each(delegate(ScalarPropertyMapping pm)
					{
						if (pm.Column.DeclaringType != toTable)
						{
							pm.Column = toTable.Properties.Single((EdmProperty p) => string.Equals(p.GetPreferredName(), pm.Column.GetPreferredName(), StringComparison.Ordinal));
						}
					});
				}
			}
		}
	}
}
