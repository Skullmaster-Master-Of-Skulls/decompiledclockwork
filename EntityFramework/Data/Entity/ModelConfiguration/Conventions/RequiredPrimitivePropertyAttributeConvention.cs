using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007EA RID: 2026
	public class RequiredPrimitivePropertyAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<RequiredAttribute>
	{
		// Token: 0x06005C17 RID: 23575 RVA: 0x0018C33A File Offset: 0x0018A53A
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, RequiredAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<RequiredAttribute>(attribute, "attribute");
			configuration.IsRequired();
		}
	}
}
