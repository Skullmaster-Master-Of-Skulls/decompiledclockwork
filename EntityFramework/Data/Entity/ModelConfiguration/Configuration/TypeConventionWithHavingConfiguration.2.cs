using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001BA RID: 442
	public class TypeConventionWithHavingConfiguration<T> where T : class
	{
		// Token: 0x06000ED3 RID: 3795 RVA: 0x00040046 File Offset: 0x0003E246
		internal TypeConventionWithHavingConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates, Func<Type, T> capturingPredicate)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00040063 File Offset: 0x0003E263
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0004006B File Offset: 0x0003E26B
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00040073 File Offset: 0x0003E273
		internal Func<Type, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004007C File Offset: 0x0003E27C
		public void Configure(Action<ConventionTypeConfiguration, T> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration, T>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConventionWithHaving<T>(this._predicates, this._capturingPredicate, entityConfigurationAction)
			});
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000400BD File Offset: 0x0003E2BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000400C5 File Offset: 0x0003E2C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000400CE File Offset: 0x0003E2CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000400D6 File Offset: 0x0003E2D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000401 RID: 1025
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000402 RID: 1026
		private readonly IEnumerable<Func<Type, bool>> _predicates;

		// Token: 0x04000403 RID: 1027
		private readonly Func<Type, T> _capturingPredicate;
	}
}
