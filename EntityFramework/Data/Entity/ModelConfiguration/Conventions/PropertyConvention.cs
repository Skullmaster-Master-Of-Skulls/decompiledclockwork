using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020002C5 RID: 709
	internal class PropertyConvention : PropertyConventionBase
	{
		// Token: 0x0600192E RID: 6446 RVA: 0x0007CC85 File Offset: 0x0007AE85
		public PropertyConvention(IEnumerable<Func<PropertyInfo, bool>> predicates, Action<ConventionPrimitivePropertyConfiguration> propertyConfigurationAction) : base(predicates)
		{
			this._propertyConfigurationAction = propertyConfigurationAction;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0007CC95 File Offset: 0x0007AE95
		internal Action<ConventionPrimitivePropertyConfiguration> PropertyConfigurationAction
		{
			get
			{
				return this._propertyConfigurationAction;
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0007CC9D File Offset: 0x0007AE9D
		protected override void ApplyCore(PropertyInfo memberInfo, Func<System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._propertyConfigurationAction(new ConventionPrimitivePropertyConfiguration(memberInfo, configuration));
		}

		// Token: 0x040008A6 RID: 2214
		private readonly Action<ConventionPrimitivePropertyConfiguration> _propertyConfigurationAction;
	}
}
