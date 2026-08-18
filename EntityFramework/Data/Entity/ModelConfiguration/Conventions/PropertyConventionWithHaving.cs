using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020002C6 RID: 710
	internal class PropertyConventionWithHaving<T> : PropertyConventionBase where T : class
	{
		// Token: 0x06001931 RID: 6449 RVA: 0x0007CCB1 File Offset: 0x0007AEB1
		public PropertyConventionWithHaving(IEnumerable<Func<PropertyInfo, bool>> predicates, Func<PropertyInfo, T> capturingPredicate, Action<ConventionPrimitivePropertyConfiguration, T> propertyConfigurationAction) : base(predicates)
		{
			this._capturingPredicate = capturingPredicate;
			this._propertyConfigurationAction = propertyConfigurationAction;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0007CCC8 File Offset: 0x0007AEC8
		internal Func<PropertyInfo, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0007CCD0 File Offset: 0x0007AED0
		internal Action<ConventionPrimitivePropertyConfiguration, T> PropertyConfigurationAction
		{
			get
			{
				return this._propertyConfigurationAction;
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0007CCD8 File Offset: 0x0007AED8
		protected override void ApplyCore(PropertyInfo memberInfo, Func<System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this._propertyConfigurationAction(new ConventionPrimitivePropertyConfiguration(memberInfo, configuration), t);
			}
		}

		// Token: 0x040008A7 RID: 2215
		private readonly Func<PropertyInfo, T> _capturingPredicate;

		// Token: 0x040008A8 RID: 2216
		private readonly Action<ConventionPrimitivePropertyConfiguration, T> _propertyConfigurationAction;
	}
}
