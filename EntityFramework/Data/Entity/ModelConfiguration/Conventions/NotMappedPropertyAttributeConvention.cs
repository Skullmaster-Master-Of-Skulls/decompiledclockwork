using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E8 RID: 2024
	public class NotMappedPropertyAttributeConvention : PropertyAttributeConfigurationConvention<NotMappedAttribute>
	{
		// Token: 0x06005C13 RID: 23571 RVA: 0x0018C289 File Offset: 0x0018A489
		public override void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, NotMappedAttribute attribute)
		{
			Check.NotNull<PropertyInfo>(memberInfo, "memberInfo");
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<NotMappedAttribute>(attribute, "attribute");
			configuration.Ignore(memberInfo);
		}
	}
}
