using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007ED RID: 2029
	public class ComplexTypeAttributeConvention : TypeAttributeConfigurationConvention<ComplexTypeAttribute>
	{
		// Token: 0x06005C1D RID: 23581 RVA: 0x0018C3E9 File Offset: 0x0018A5E9
		public override void Apply(ConventionTypeConfiguration configuration, ComplexTypeAttribute attribute)
		{
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<ComplexTypeAttribute>(attribute, "attribute");
			configuration.IsComplexType();
		}
	}
}
