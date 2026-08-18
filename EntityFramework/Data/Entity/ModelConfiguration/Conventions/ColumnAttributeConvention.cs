using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E1 RID: 2017
	public class ColumnAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<ColumnAttribute>
	{
		// Token: 0x06005C05 RID: 23557 RVA: 0x0018BE98 File Offset: 0x0018A098
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, ColumnAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<ColumnAttribute>(attribute, "attribute");
			if (!string.IsNullOrWhiteSpace(attribute.Name))
			{
				configuration.HasColumnName(attribute.Name);
			}
			if (!string.IsNullOrWhiteSpace(attribute.TypeName))
			{
				configuration.HasColumnType(attribute.TypeName);
			}
			if (attribute.Order >= 0)
			{
				configuration.HasColumnOrder(attribute.Order);
			}
		}
	}
}
