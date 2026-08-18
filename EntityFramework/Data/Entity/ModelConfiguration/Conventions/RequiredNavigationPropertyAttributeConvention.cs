using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E9 RID: 2025
	public class RequiredNavigationPropertyAttributeConvention : Convention
	{
		// Token: 0x06005C15 RID: 23573 RVA: 0x0018C2C0 File Offset: 0x0018A4C0
		internal override void ApplyPropertyConfiguration(PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
		{
			if (propertyInfo.IsValidEdmNavigationProperty() && !propertyInfo.PropertyType.IsCollection() && this._attributeProvider.GetAttributes(propertyInfo).OfType<RequiredAttribute>().Any<RequiredAttribute>())
			{
				NavigationPropertyConfiguration navigationPropertyConfiguration = (NavigationPropertyConfiguration)propertyConfiguration();
				if (navigationPropertyConfiguration.RelationshipMultiplicity == null)
				{
					navigationPropertyConfiguration.RelationshipMultiplicity = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
				}
			}
		}

		// Token: 0x04002483 RID: 9347
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
