using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F4 RID: 2036
	public class MappingInheritedPropertiesSupportConvention : IDbMappingConvention, IConvention
	{
		// Token: 0x06005C42 RID: 23618 RVA: 0x0018DE04 File Offset: 0x0018C004
		void IDbMappingConvention.Apply(DbDatabaseMapping databaseMapping)
		{
			Check.NotNull<DbDatabaseMapping>(databaseMapping, "databaseMapping");
			databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings).Each(delegate(EntitySetMapping esm)
			{
				foreach (EntityTypeMapping entityTypeMapping in esm.EntityTypeMappings)
				{
					if (MappingInheritedPropertiesSupportConvention.RemapsInheritedProperties(databaseMapping, entityTypeMapping) && MappingInheritedPropertiesSupportConvention.HasBaseWithIsTypeOf(esm, entityTypeMapping.EntityType))
					{
						throw Error.UnsupportedHybridInheritanceMapping(entityTypeMapping.EntityType.Name);
					}
				}
			});
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x0018DEA8 File Offset: 0x0018C0A8
		private static bool RemapsInheritedProperties(DbDatabaseMapping databaseMapping, EntityTypeMapping entityTypeMapping)
		{
			IEnumerable<EdmProperty> enumerable = entityTypeMapping.EntityType.Properties.Except(entityTypeMapping.EntityType.DeclaredProperties).Except(entityTypeMapping.EntityType.GetKeyProperties());
			using (IEnumerator<EdmProperty> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MappingInheritedPropertiesSupportConvention.<>c__DisplayClass8 CS$<>8__locals1 = new MappingInheritedPropertiesSupportConvention.<>c__DisplayClass8();
					CS$<>8__locals1.property = enumerator.Current;
					MappingFragment fragment = MappingInheritedPropertiesSupportConvention.GetFragmentForPropertyMapping(entityTypeMapping, CS$<>8__locals1.property);
					if (fragment != null)
					{
						for (EntityType entityType = (EntityType)entityTypeMapping.EntityType.BaseType; entityType != null; entityType = (EntityType)entityType.BaseType)
						{
							if ((from baseTypeMapping in databaseMapping.GetEntityTypeMappings(entityType)
							select MappingInheritedPropertiesSupportConvention.GetFragmentForPropertyMapping(baseTypeMapping, CS$<>8__locals1.property)).Any((MappingFragment baseFragment) => baseFragment != null && baseFragment.Table != fragment.Table))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x0018DFF8 File Offset: 0x0018C1F8
		private static MappingFragment GetFragmentForPropertyMapping(EntityTypeMapping entityTypeMapping, EdmProperty property)
		{
			return entityTypeMapping.MappingFragments.SingleOrDefault((MappingFragment tmf) => tmf.ColumnMappings.Any((ColumnMappingBuilder pm) => pm.PropertyPath.Last<EdmProperty>() == property));
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x0018E04C File Offset: 0x0018C24C
		private static bool HasBaseWithIsTypeOf(EntitySetMapping entitySetMapping, EntityType entityType)
		{
			EdmType baseType;
			for (baseType = entityType.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if ((from etm in entitySetMapping.EntityTypeMappings
				where etm.EntityType == baseType
				select etm).Any((EntityTypeMapping etm) => etm.IsHierarchyMapping))
				{
					return true;
				}
			}
			return false;
		}
	}
}
