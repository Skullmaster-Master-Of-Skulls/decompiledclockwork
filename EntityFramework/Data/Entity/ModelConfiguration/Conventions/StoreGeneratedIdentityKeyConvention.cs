using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000804 RID: 2052
	public class StoreGeneratedIdentityKeyConvention : IConceptualModelConvention<EntityType>, IConvention
	{
		// Token: 0x06005C83 RID: 23683 RVA: 0x0018F8D8 File Offset: 0x0018DAD8
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.BaseType == null && item.KeyProperties.Count == 1)
			{
				if (!(from p in item.DeclaredProperties
				let sgp = p.GetStoreGeneratedPattern()
				where sgp != null && sgp == StoreGeneratedPattern.Identity
				select sgp).Any<StoreGeneratedPattern?>())
				{
					EdmProperty property = item.KeyProperties.Single<EdmProperty>();
					if (property.GetStoreGeneratedPattern() == null && property.PrimitiveType != null && StoreGeneratedIdentityKeyConvention._applicableTypes.Contains(property.PrimitiveType.PrimitiveTypeKind))
					{
						if (!model.ConceptualModel.AssociationTypes.Any((AssociationType a) => StoreGeneratedIdentityKeyConvention.IsNonTableSplittingForeignKey(a, property)) && !StoreGeneratedIdentityKeyConvention.ParentOfTpc(item, model.ConceptualModel))
						{
							property.SetStoreGeneratedPattern(StoreGeneratedPattern.Identity);
						}
					}
				}
			}
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x0018FA24 File Offset: 0x0018DC24
		private static bool IsNonTableSplittingForeignKey(AssociationType association, EdmProperty property)
		{
			if (association.Constraint != null && association.Constraint.ToProperties.Contains(property))
			{
				EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)association.SourceEnd.GetEntityType().GetConfiguration();
				EntityTypeConfiguration entityTypeConfiguration2 = (EntityTypeConfiguration)association.TargetEnd.GetEntityType().GetConfiguration();
				return entityTypeConfiguration == null || entityTypeConfiguration2 == null || entityTypeConfiguration.GetTableName() == null || entityTypeConfiguration2.GetTableName() == null || !entityTypeConfiguration.GetTableName().Equals(entityTypeConfiguration2.GetTableName());
			}
			return false;
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x0018FC10 File Offset: 0x0018DE10
		private static bool ParentOfTpc(EntityType entityType, EdmModel model)
		{
			return (from et in model.EntityTypes
			where et.GetRootType() == entityType
			select et into e
			let configuration = e.GetConfiguration() as EntityTypeConfiguration
			where configuration != null && configuration.IsMappingAnyInheritedProperty(e)
			select e).Any<EntityType>();
		}

		// Token: 0x040024B1 RID: 9393
		private static readonly IEnumerable<PrimitiveTypeKind> _applicableTypes = new PrimitiveTypeKind[]
		{
			PrimitiveTypeKind.Int16,
			PrimitiveTypeKind.Int32,
			PrimitiveTypeKind.Int64
		};
	}
}
