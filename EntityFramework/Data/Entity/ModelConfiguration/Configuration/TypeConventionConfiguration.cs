using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001B7 RID: 439
	public class TypeConventionConfiguration
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003FDFF File Offset: 0x0003DFFF
		internal TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration) : this(conventionsConfiguration, Enumerable.Empty<Func<Type, bool>>())
		{
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003FE0D File Offset: 0x0003E00D
		private TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0003FE23 File Offset: 0x0003E023
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0003FE2B File Offset: 0x0003E02B
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003FE33 File Offset: 0x0003E033
		public TypeConventionConfiguration Where(Func<Type, bool> predicate)
		{
			Check.NotNull<Func<Type, bool>>(predicate, "predicate");
			return new TypeConventionConfiguration(this._conventionsConfiguration, this._predicates.Append(predicate));
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003FE58 File Offset: 0x0003E058
		public TypeConventionWithHavingConfiguration<T> Having<T>(Func<Type, T> capturingPredicate) where T : class
		{
			Check.NotNull<Func<Type, T>>(capturingPredicate, "capturingPredicate");
			return new TypeConventionWithHavingConfiguration<T>(this._conventionsConfiguration, this._predicates, capturingPredicate);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003FE78 File Offset: 0x0003E078
		public void Configure(Action<ConventionTypeConfiguration> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConvention(this._predicates, entityConfigurationAction)
			});
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003FEB3 File Offset: 0x0003E0B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003FEBB File Offset: 0x0003E0BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003FEC4 File Offset: 0x0003E0C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003FECC File Offset: 0x0003E0CC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040003FA RID: 1018
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x040003FB RID: 1019
		private readonly IEnumerable<Func<Type, bool>> _predicates;
	}
}
