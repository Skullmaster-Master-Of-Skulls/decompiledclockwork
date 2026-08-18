using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007EF RID: 2031
	public class TableAttributeConvention : TypeAttributeConfigurationConvention<TableAttribute>
	{
		// Token: 0x06005C21 RID: 23585 RVA: 0x0018C43C File Offset: 0x0018A63C
		public override void Apply(ConventionTypeConfiguration configuration, TableAttribute attribute)
		{
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<TableAttribute>(attribute, "attribute");
			if (string.IsNullOrWhiteSpace(attribute.Schema))
			{
				configuration.ToTable(attribute.Name);
				return;
			}
			configuration.ToTable(attribute.Name, attribute.Schema);
		}
	}
}
