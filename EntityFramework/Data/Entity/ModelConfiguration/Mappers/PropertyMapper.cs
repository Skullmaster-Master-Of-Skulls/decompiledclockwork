using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000826 RID: 2086
	internal sealed class PropertyMapper
	{
		// Token: 0x06005DA2 RID: 23970 RVA: 0x00194854 File Offset: 0x00192A54
		public PropertyMapper(TypeMapper typeMapper)
		{
			this._typeMapper = typeMapper;
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x00194864 File Offset: 0x00192A64
		public void Map(PropertyInfo propertyInfo, ComplexType complexType, Func<ComplexTypeConfiguration> complexTypeConfiguration)
		{
			EdmProperty edmProperty = this.MapPrimitiveOrComplexOrEnumProperty(propertyInfo, complexTypeConfiguration, true);
			if (edmProperty != null)
			{
				complexType.AddMember(edmProperty);
			}
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x00194888 File Offset: 0x00192A88
		public void Map(PropertyInfo propertyInfo, EntityType entityType, Func<EntityTypeConfiguration> entityTypeConfiguration)
		{
			EdmProperty edmProperty = this.MapPrimitiveOrComplexOrEnumProperty(propertyInfo, entityTypeConfiguration, false);
			if (edmProperty != null)
			{
				entityType.AddMember(edmProperty);
				return;
			}
			new NavigationPropertyMapper(this._typeMapper).Map(propertyInfo, entityType, entityTypeConfiguration);
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x001948C0 File Offset: 0x00192AC0
		internal bool MapIfNotNavigationProperty(PropertyInfo propertyInfo, EntityType entityType, Func<EntityTypeConfiguration> entityTypeConfiguration)
		{
			EdmProperty edmProperty = this.MapPrimitiveOrComplexOrEnumProperty(propertyInfo, entityTypeConfiguration, false);
			if (edmProperty != null)
			{
				entityType.AddMember(edmProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x00194920 File Offset: 0x00192B20
		private EdmProperty MapPrimitiveOrComplexOrEnumProperty(PropertyInfo propertyInfo, Func<StructuralTypeConfiguration> structuralTypeConfiguration, bool discoverComplexTypes = false)
		{
			EdmProperty edmProperty = propertyInfo.AsEdmPrimitiveProperty();
			if (edmProperty == null)
			{
				Type propertyType = propertyInfo.PropertyType;
				ComplexType complexType = this._typeMapper.MapComplexType(propertyType, discoverComplexTypes);
				if (complexType != null)
				{
					edmProperty = EdmProperty.CreateComplex(propertyInfo.Name, complexType);
				}
				else
				{
					bool nullable = propertyType.TryUnwrapNullableType(out propertyType);
					if (propertyType.IsEnum())
					{
						EnumType enumType = this._typeMapper.MapEnumType(propertyType);
						if (enumType != null)
						{
							edmProperty = EdmProperty.CreateEnum(propertyInfo.Name, enumType);
							edmProperty.Nullable = nullable;
						}
					}
				}
			}
			if (edmProperty != null)
			{
				edmProperty.SetClrPropertyInfo(propertyInfo);
				new AttributeMapper(this._typeMapper.MappingContext.AttributeProvider).Map(propertyInfo, edmProperty.GetMetadataProperties());
				if (!edmProperty.IsComplexType)
				{
					this._typeMapper.MappingContext.ConventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, () => structuralTypeConfiguration().Property(new PropertyPath(propertyInfo), null), this._typeMapper.MappingContext.ModelConfiguration);
				}
			}
			return edmProperty;
		}

		// Token: 0x04002501 RID: 9473
		private readonly TypeMapper _typeMapper;
	}
}
