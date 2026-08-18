using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Mappers;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E4 RID: 2020
	public class ForeignKeyPrimitivePropertyAttributeConvention : PropertyAttributeConfigurationConvention<ForeignKeyAttribute>
	{
		// Token: 0x06005C0B RID: 23563 RVA: 0x0018BF98 File Offset: 0x0018A198
		public override void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, ForeignKeyAttribute attribute)
		{
			Check.NotNull<PropertyInfo>(memberInfo, "memberInfo");
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<ForeignKeyAttribute>(attribute, "attribute");
			if (memberInfo.IsValidEdmScalarProperty())
			{
				PropertyInfo propertyInfo = (from pi in new PropertyFilter(DbModelBuilderVersion.Latest).GetProperties(configuration.ClrType, false, null, null, false)
				where pi.Name.Equals(attribute.Name, StringComparison.Ordinal)
				select pi).SingleOrDefault<PropertyInfo>();
				if (propertyInfo == null)
				{
					throw Error.ForeignKeyAttributeConvention_InvalidNavigationProperty(memberInfo.Name, configuration.ClrType, attribute.Name);
				}
				ConventionNavigationPropertyConfiguration conventionNavigationPropertyConfiguration = configuration.NavigationProperty(propertyInfo);
				conventionNavigationPropertyConfiguration.HasConstraint<ForeignKeyConstraintConfiguration>(delegate(ForeignKeyConstraintConfiguration fk)
				{
					fk.AddColumn(memberInfo);
				});
			}
		}
	}
}
