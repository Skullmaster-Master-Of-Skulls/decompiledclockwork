using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x02000820 RID: 2080
	internal class PropertyMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x06005D6F RID: 23919 RVA: 0x00193BC5 File Offset: 0x00191DC5
		public PropertyMappingGenerator(DbProviderManifest providerManifest) : base(providerManifest)
		{
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x00193C30 File Offset: 0x00191E30
		public void Generate(EntityType entityType, IEnumerable<EdmProperty> properties, EntitySetMapping entitySetMapping, MappingFragment entityTypeMappingFragment, IList<EdmProperty> propertyPath, bool createNewColumn)
		{
			ReadOnlyMetadataCollection<EdmProperty> declaredProperties = entityType.GetRootType().DeclaredProperties;
			using (IEnumerator<EdmProperty> enumerator = properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty property = enumerator.Current;
					if (property.IsComplexType)
					{
						if (propertyPath.Any((EdmProperty p) => p.IsComplexType && p.ComplexType == property.ComplexType))
						{
							throw Error.CircularComplexTypeHierarchy();
						}
					}
					propertyPath.Add(property);
					if (property.IsComplexType)
					{
						this.Generate(entityType, property.ComplexType.Properties, entitySetMapping, entityTypeMappingFragment, propertyPath, createNewColumn);
					}
					else
					{
						EdmProperty edmProperty = (from pm in entitySetMapping.EntityTypeMappings.SelectMany((EntityTypeMapping etm) => etm.MappingFragments).SelectMany((MappingFragment etmf) => etmf.ColumnMappings)
						where pm.PropertyPath.SequenceEqual(propertyPath)
						select pm.ColumnProperty).FirstOrDefault<EdmProperty>();
						if (edmProperty == null || createNewColumn)
						{
							string columnName = string.Join("_", from p in propertyPath
							select p.Name);
							edmProperty = base.MapTableColumn(property, columnName, !declaredProperties.Contains(propertyPath.First<EdmProperty>()));
							entityTypeMappingFragment.Table.AddColumn(edmProperty);
							if (entityType.KeyProperties().Contains(property))
							{
								entityTypeMappingFragment.Table.AddKeyMember(edmProperty);
							}
						}
						entityTypeMappingFragment.AddColumnMapping(new ColumnMappingBuilder(edmProperty, propertyPath.ToList<EdmProperty>()));
					}
					propertyPath.Remove(property);
				}
			}
		}
	}
}
