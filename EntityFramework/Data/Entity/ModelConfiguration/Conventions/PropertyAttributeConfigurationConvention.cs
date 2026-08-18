using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C6 RID: 454
	public abstract class PropertyAttributeConfigurationConvention<TAttribute> : Convention where TAttribute : Attribute
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x00041040 File Offset: 0x0003F240
		protected PropertyAttributeConfigurationConvention()
		{
			base.Types().Configure(delegate(ConventionTypeConfiguration ec)
			{
				foreach (PropertyInfo propertyInfo in ec.ClrType.GetInstanceProperties())
				{
					IList<Attribute> list = (IList<Attribute>)this._attributeProvider.GetAttributes(propertyInfo);
					for (int i = 0; i < list.Count; i++)
					{
						TAttribute tattribute = list[i] as TAttribute;
						if (tattribute != null)
						{
							this.Apply(propertyInfo, ec, tattribute);
						}
					}
				}
			});
		}

		// Token: 0x06000F31 RID: 3889
		public abstract void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, TAttribute attribute);

		// Token: 0x0400041D RID: 1053
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
