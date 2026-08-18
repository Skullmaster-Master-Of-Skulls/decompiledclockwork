using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000824 RID: 2084
	internal sealed class NavigationPropertyMapper
	{
		// Token: 0x06005D95 RID: 23957 RVA: 0x00194233 File Offset: 0x00192433
		public NavigationPropertyMapper(TypeMapper typeMapper)
		{
			this._typeMapper = typeMapper;
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x00194264 File Offset: 0x00192464
		public void Map(PropertyInfo propertyInfo, EntityType entityType, Func<EntityTypeConfiguration> entityTypeConfiguration)
		{
			Type propertyType = propertyInfo.PropertyType;
			RelationshipMultiplicity relationshipMultiplicity = RelationshipMultiplicity.ZeroOrOne;
			if (propertyType.IsCollection(out propertyType))
			{
				relationshipMultiplicity = RelationshipMultiplicity.Many;
			}
			EntityType entityType2 = this._typeMapper.MapEntityType(propertyType);
			if (entityType2 != null)
			{
				RelationshipMultiplicity sourceAssociationEndKind = relationshipMultiplicity.IsMany() ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.Many;
				AssociationType associationType = this._typeMapper.MappingContext.Model.AddAssociationType(entityType.Name + "_" + propertyInfo.Name, entityType, sourceAssociationEndKind, entityType2, relationshipMultiplicity, this._typeMapper.MappingContext.ModelConfiguration.ModelNamespace);
				associationType.SourceEnd.SetClrPropertyInfo(propertyInfo);
				this._typeMapper.MappingContext.Model.AddAssociationSet(associationType.Name, associationType);
				NavigationProperty navigationProperty = entityType.AddNavigationProperty(propertyInfo.Name, associationType);
				navigationProperty.SetClrPropertyInfo(propertyInfo);
				this._typeMapper.MappingContext.ConventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, () => entityTypeConfiguration().Navigation(propertyInfo), this._typeMapper.MappingContext.ModelConfiguration);
				new AttributeMapper(this._typeMapper.MappingContext.AttributeProvider).Map(propertyInfo, navigationProperty.GetMetadataProperties());
			}
		}

		// Token: 0x040024FB RID: 9467
		private readonly TypeMapper _typeMapper;
	}
}
