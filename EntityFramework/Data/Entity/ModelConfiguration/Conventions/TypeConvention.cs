using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001CB RID: 459
	internal class TypeConvention : TypeConventionBase
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0004129D File Offset: 0x0003F49D
		public TypeConvention(IEnumerable<Func<Type, bool>> predicates, Action<ConventionTypeConfiguration> entityConfigurationAction) : base(predicates)
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000412AD File Offset: 0x0003F4AD
		internal Action<ConventionTypeConfiguration> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000412B5 File Offset: 0x0003F4B5
		protected override void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, modelConfiguration));
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000412C9 File Offset: 0x0003F4C9
		protected override void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000412DE File Offset: 0x0003F4DE
		protected override void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x04000421 RID: 1057
		private readonly Action<ConventionTypeConfiguration> _entityConfigurationAction;
	}
}
