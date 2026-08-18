using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001B9 RID: 441
	public class TypeConventionWithHavingConfiguration<T, TValue> where T : class where TValue : class
	{
		// Token: 0x06000ECA RID: 3786 RVA: 0x0003FFAC File Offset: 0x0003E1AC
		internal TypeConventionWithHavingConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates, Func<Type, TValue> capturingPredicate)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0003FFC9 File Offset: 0x0003E1C9
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0003FFD1 File Offset: 0x0003E1D1
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003FFD9 File Offset: 0x0003E1D9
		internal Func<Type, TValue> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public void Configure(Action<ConventionTypeConfiguration<T>, TValue> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration<T>, TValue>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConventionWithHaving<T, TValue>(this._predicates, this._capturingPredicate, entityConfigurationAction)
			});
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00040025 File Offset: 0x0003E225
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0004002D File Offset: 0x0003E22D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00040036 File Offset: 0x0003E236
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0004003E File Offset: 0x0003E23E
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040003FE RID: 1022
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x040003FF RID: 1023
		private readonly IEnumerable<Func<Type, bool>> _predicates;

		// Token: 0x04000400 RID: 1024
		private readonly Func<Type, TValue> _capturingPredicate;
	}
}
