using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001CD RID: 461
	internal abstract class TypeConventionWithHavingBase<T> : TypeConventionBase where T : class
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x00041390 File Offset: 0x0003F590
		public TypeConventionWithHavingBase(IEnumerable<Func<Type, bool>> predicates, Func<Type, T> capturingPredicate) : base(predicates)
		{
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x000413A0 File Offset: 0x0003F5A0
		internal Func<Type, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000413A8 File Offset: 0x0003F5A8
		protected override void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this.InvokeAction(memberInfo, modelConfiguration, t);
			}
		}

		// Token: 0x06000F54 RID: 3924
		protected abstract void InvokeAction(Type memberInfo, ModelConfiguration configuration, T value);

		// Token: 0x06000F55 RID: 3925 RVA: 0x000413D4 File Offset: 0x0003F5D4
		protected sealed override void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this.InvokeAction(memberInfo, configuration, modelConfiguration, t);
			}
		}

		// Token: 0x06000F56 RID: 3926
		protected abstract void InvokeAction(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value);

		// Token: 0x06000F57 RID: 3927 RVA: 0x00041400 File Offset: 0x0003F600
		protected override void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this.InvokeAction(memberInfo, configuration, modelConfiguration, t);
			}
		}

		// Token: 0x06000F58 RID: 3928
		protected abstract void InvokeAction(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value);

		// Token: 0x04000425 RID: 1061
		private readonly Func<Type, T> _capturingPredicate;
	}
}
