using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C5 RID: 453
	public abstract class PrimitivePropertyAttributeConfigurationConvention<TAttribute> : Convention where TAttribute : Attribute
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x00040F4C File Offset: 0x0003F14C
		protected PrimitivePropertyAttributeConfigurationConvention()
		{
			base.Properties().Having<IEnumerable<TAttribute>>((PropertyInfo pi) => this._attributeProvider.GetAttributes(pi).OfType<TAttribute>()).Configure(delegate(ConventionPrimitivePropertyConfiguration configuration, IEnumerable<TAttribute> attributes)
			{
				foreach (TAttribute attribute in attributes)
				{
					this.Apply(configuration, attribute);
				}
			});
		}

		// Token: 0x06000F2D RID: 3885
		public abstract void Apply(ConventionPrimitivePropertyConfiguration configuration, TAttribute attribute);

		// Token: 0x0400041C RID: 1052
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
