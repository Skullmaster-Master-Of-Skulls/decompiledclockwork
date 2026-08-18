using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020002C4 RID: 708
	internal abstract class PropertyConventionBase : IConfigurationConvention<PropertyInfo, System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration>, IConvention
	{
		// Token: 0x0600192A RID: 6442 RVA: 0x0007CC15 File Offset: 0x0007AE15
		public PropertyConventionBase(IEnumerable<Func<PropertyInfo, bool>> predicates)
		{
			this._predicates = predicates;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0007CC24 File Offset: 0x0007AE24
		internal IEnumerable<Func<PropertyInfo, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0007CC44 File Offset: 0x0007AE44
		public void Apply(PropertyInfo memberInfo, Func<System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<PropertyInfo, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, configuration, modelConfiguration);
			}
		}

		// Token: 0x0600192D RID: 6445
		protected abstract void ApplyCore(PropertyInfo memberInfo, Func<System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration);

		// Token: 0x040008A5 RID: 2213
		private readonly IEnumerable<Func<PropertyInfo, bool>> _predicates;
	}
}
