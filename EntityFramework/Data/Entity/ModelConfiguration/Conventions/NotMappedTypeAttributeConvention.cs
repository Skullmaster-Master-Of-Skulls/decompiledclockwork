using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007EE RID: 2030
	public class NotMappedTypeAttributeConvention : TypeAttributeConfigurationConvention<NotMappedAttribute>
	{
		// Token: 0x06005C1F RID: 23583 RVA: 0x0018C412 File Offset: 0x0018A612
		public override void Apply(ConventionTypeConfiguration configuration, NotMappedAttribute attribute)
		{
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<NotMappedAttribute>(attribute, "attribute");
			configuration.Ignore();
		}
	}
}
