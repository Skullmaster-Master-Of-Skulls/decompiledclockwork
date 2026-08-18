using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E6 RID: 2022
	public class KeyAttributeConvention : Convention
	{
		// Token: 0x06005C0F RID: 23567 RVA: 0x0018C19C File Offset: 0x0018A39C
		internal override void ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(PropertyInfo propertyInfo, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			if (typeof(TStructuralTypeConfiguration) == typeof(EntityTypeConfiguration) && this._attributeProvider.GetAttributes(propertyInfo).OfType<KeyAttribute>().Any<KeyAttribute>())
			{
				EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)((object)structuralTypeConfiguration());
				if (propertyInfo.IsValidEdmScalarProperty())
				{
					entityTypeConfiguration.Key(propertyInfo);
				}
			}
		}

		// Token: 0x04002481 RID: 9345
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
