using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007E7 RID: 2023
	public class MaxLengthAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<MaxLengthAttribute>
	{
		// Token: 0x06005C11 RID: 23569 RVA: 0x0018C214 File Offset: 0x0018A414
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, MaxLengthAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<MaxLengthAttribute>(attribute, "attribute");
			PropertyInfo clrPropertyInfo = configuration.ClrPropertyInfo;
			if (attribute.Length == 0 || attribute.Length < -1)
			{
				throw Error.MaxLengthAttributeConvention_InvalidMaxLength(clrPropertyInfo.Name, clrPropertyInfo.ReflectedType);
			}
			if (attribute.Length == -1)
			{
				configuration.IsMaxLength();
				return;
			}
			configuration.HasMaxLength(attribute.Length);
		}

		// Token: 0x04002482 RID: 9346
		private const int MaxLengthIndicator = -1;
	}
}
