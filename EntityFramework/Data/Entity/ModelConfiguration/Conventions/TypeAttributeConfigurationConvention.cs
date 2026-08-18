using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C7 RID: 455
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public abstract class TypeAttributeConfigurationConvention<TAttribute> : Convention where TAttribute : Attribute
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x000410E4 File Offset: 0x0003F2E4
		protected TypeAttributeConfigurationConvention()
		{
			base.Types().Having<IEnumerable<TAttribute>>((Type t) => this._attributeProvider.GetAttributes(t).OfType<TAttribute>()).Configure(delegate(ConventionTypeConfiguration configuration, IEnumerable<TAttribute> attributes)
			{
				foreach (TAttribute attribute in attributes)
				{
					this.Apply(configuration, attribute);
				}
			});
		}

		// Token: 0x06000F34 RID: 3892
		public abstract void Apply(ConventionTypeConfiguration configuration, TAttribute attribute);

		// Token: 0x0400041E RID: 1054
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
