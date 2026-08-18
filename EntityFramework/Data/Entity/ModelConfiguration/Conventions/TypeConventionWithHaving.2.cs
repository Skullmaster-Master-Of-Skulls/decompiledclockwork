using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001CF RID: 463
	internal class TypeConventionWithHaving<T> : TypeConventionWithHavingBase<T> where T : class
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x00041492 File Offset: 0x0003F692
		public TypeConventionWithHaving(IEnumerable<Func<Type, bool>> predicates, Func<Type, T> capturingPredicate, Action<ConventionTypeConfiguration, T> entityConfigurationAction) : base(predicates, capturingPredicate)
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x000414A3 File Offset: 0x0003F6A3
		internal Action<ConventionTypeConfiguration, T> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000414AB File Offset: 0x0003F6AB
		protected override void InvokeAction(Type memberInfo, ModelConfiguration modelConfiguration, T value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, modelConfiguration), value);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000414C0 File Offset: 0x0003F6C0
		protected override void InvokeAction(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000414D7 File Offset: 0x0003F6D7
		protected override void InvokeAction(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x04000427 RID: 1063
		private readonly Action<ConventionTypeConfiguration, T> _entityConfigurationAction;
	}
}
