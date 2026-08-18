using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001B8 RID: 440
	public class TypeConventionConfiguration<T> where T : class
	{
		// Token: 0x06000EBF RID: 3775 RVA: 0x0003FED4 File Offset: 0x0003E0D4
		internal TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration) : this(conventionsConfiguration, Enumerable.Empty<Func<Type, bool>>())
		{
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003FEE2 File Offset: 0x0003E0E2
		private TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0003FEF8 File Offset: 0x0003E0F8
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0003FF00 File Offset: 0x0003E100
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003FF08 File Offset: 0x0003E108
		public TypeConventionConfiguration<T> Where(Func<Type, bool> predicate)
		{
			Check.NotNull<Func<Type, bool>>(predicate, "predicate");
			return new TypeConventionConfiguration<T>(this._conventionsConfiguration, this._predicates.Append(predicate));
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003FF2D File Offset: 0x0003E12D
		public TypeConventionWithHavingConfiguration<T, TValue> Having<TValue>(Func<Type, TValue> capturingPredicate) where TValue : class
		{
			Check.NotNull<Func<Type, TValue>>(capturingPredicate, "capturingPredicate");
			return new TypeConventionWithHavingConfiguration<T, TValue>(this._conventionsConfiguration, this._predicates, capturingPredicate);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003FF50 File Offset: 0x0003E150
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public void Configure(Action<ConventionTypeConfiguration<T>> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration<T>>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConvention<T>(this._predicates, entityConfigurationAction)
			});
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003FF8B File Offset: 0x0003E18B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003FF93 File Offset: 0x0003E193
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003FF9C File Offset: 0x0003E19C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003FFA4 File Offset: 0x0003E1A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040003FC RID: 1020
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x040003FD RID: 1021
		private readonly IEnumerable<Func<Type, bool>> _predicates;
	}
}
