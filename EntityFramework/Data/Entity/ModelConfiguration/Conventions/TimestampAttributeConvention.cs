using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007EC RID: 2028
	public class TimestampAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<TimestampAttribute>
	{
		// Token: 0x06005C1B RID: 23579 RVA: 0x0018C3C0 File Offset: 0x0018A5C0
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, TimestampAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<TimestampAttribute>(attribute, "attribute");
			configuration.IsRowVersion();
		}
	}
}
