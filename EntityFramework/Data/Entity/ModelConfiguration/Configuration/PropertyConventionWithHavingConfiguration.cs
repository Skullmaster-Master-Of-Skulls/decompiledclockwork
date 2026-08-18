using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B1 RID: 689
	public class PropertyConventionWithHavingConfiguration<T> where T : class
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x00079B38 File Offset: 0x00077D38
		internal PropertyConventionWithHavingConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<PropertyInfo, bool>> predicates, Func<PropertyInfo, T> capturingPredicate)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00079B55 File Offset: 0x00077D55
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x00079B5D File Offset: 0x00077D5D
		internal IEnumerable<Func<PropertyInfo, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00079B65 File Offset: 0x00077D65
		internal Func<PropertyInfo, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00079B70 File Offset: 0x00077D70
		public void Configure(Action<ConventionPrimitivePropertyConfiguration, T> propertyConfigurationAction)
		{
			Check.NotNull<Action<ConventionPrimitivePropertyConfiguration, T>>(propertyConfigurationAction, "propertyConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new PropertyConventionWithHaving<T>(this._predicates, this._capturingPredicate, propertyConfigurationAction)
			});
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00079BB1 File Offset: 0x00077DB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00079BB9 File Offset: 0x00077DB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00079BC2 File Offset: 0x00077DC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00079BCA File Offset: 0x00077DCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000874 RID: 2164
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000875 RID: 2165
		private readonly IEnumerable<Func<PropertyInfo, bool>> _predicates;

		// Token: 0x04000876 RID: 2166
		private readonly Func<PropertyInfo, T> _capturingPredicate;
	}
}
