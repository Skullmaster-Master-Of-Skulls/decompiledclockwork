using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E2 RID: 2018
	public class ConcurrencyCheckAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<ConcurrencyCheckAttribute>
	{
		// Token: 0x06005C07 RID: 23559 RVA: 0x0018BF0F File Offset: 0x0018A10F
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, ConcurrencyCheckAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<ConcurrencyCheckAttribute>(attribute, "attribute");
			configuration.IsConcurrencyToken();
		}
	}
}
