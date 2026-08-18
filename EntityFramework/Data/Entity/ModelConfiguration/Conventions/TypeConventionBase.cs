using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001CA RID: 458
	internal abstract class TypeConventionBase : IConfigurationConvention<Type, EntityTypeConfiguration>, IConfigurationConvention<Type, ComplexTypeConfiguration>, IConfigurationConvention<Type>, IConvention
	{
		// Token: 0x06000F3C RID: 3900 RVA: 0x0004117F File Offset: 0x0003F37F
		protected TypeConventionBase(IEnumerable<Func<Type, bool>> predicates)
		{
			this._predicates = predicates;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0004118E File Offset: 0x0003F38E
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000411AC File Offset: 0x0003F3AC
		public void Apply(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<Type, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, modelConfiguration);
			}
		}

		// Token: 0x06000F3F RID: 3903
		protected abstract void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration);

		// Token: 0x06000F40 RID: 3904 RVA: 0x00041204 File Offset: 0x0003F404
		public void Apply(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<Type, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, configuration, modelConfiguration);
			}
		}

		// Token: 0x06000F41 RID: 3905
		protected abstract void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration);

		// Token: 0x06000F42 RID: 3906 RVA: 0x0004125C File Offset: 0x0003F45C
		public void Apply(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<Type, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, configuration, modelConfiguration);
			}
		}

		// Token: 0x06000F43 RID: 3907
		protected abstract void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration);

		// Token: 0x04000420 RID: 1056
		private readonly IEnumerable<Func<Type, bool>> _predicates;
	}
}
