using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001CE RID: 462
	internal class TypeConventionWithHaving<T, TValue> : TypeConventionWithHavingBase<TValue> where T : class where TValue : class
	{
		// Token: 0x06000F59 RID: 3929 RVA: 0x0004142C File Offset: 0x0003F62C
		public TypeConventionWithHaving(IEnumerable<Func<Type, bool>> predicates, Func<Type, TValue> capturingPredicate, Action<ConventionTypeConfiguration<T>, TValue> entityConfigurationAction) : base(predicates.Prepend(TypeConvention<T>.OfTypePredicate), capturingPredicate)
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00041447 File Offset: 0x0003F647
		internal Action<ConventionTypeConfiguration<T>, TValue> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0004144F File Offset: 0x0003F64F
		protected override void InvokeAction(Type memberInfo, ModelConfiguration modelConfiguration, TValue value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, modelConfiguration), value);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00041464 File Offset: 0x0003F664
		protected override void InvokeAction(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration, TValue value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0004147B File Offset: 0x0003F67B
		protected override void InvokeAction(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration, TValue value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x04000426 RID: 1062
		private readonly Action<ConventionTypeConfiguration<T>, TValue> _entityConfigurationAction;
	}
}
