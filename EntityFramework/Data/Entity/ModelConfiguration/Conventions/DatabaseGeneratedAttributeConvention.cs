using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E3 RID: 2019
	public class DatabaseGeneratedAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<DatabaseGeneratedAttribute>
	{
		// Token: 0x06005C09 RID: 23561 RVA: 0x0018BF38 File Offset: 0x0018A138
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DatabaseGeneratedAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<DatabaseGeneratedAttribute>(attribute, "attribute");
			configuration.HasDatabaseGeneratedOption(attribute.DatabaseGeneratedOption);
		}
	}
}
