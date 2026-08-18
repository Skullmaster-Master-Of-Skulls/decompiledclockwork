using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B0 RID: 688
	public class PropertyConventionConfiguration
	{
		// Token: 0x06001826 RID: 6182 RVA: 0x00079A60 File Offset: 0x00077C60
		internal PropertyConventionConfiguration(ConventionsConfiguration conventionsConfiguration) : this(conventionsConfiguration, Enumerable.Empty<Func<PropertyInfo, bool>>())
		{
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00079A6E File Offset: 0x00077C6E
		private PropertyConventionConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<PropertyInfo, bool>> predicates)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x00079A84 File Offset: 0x00077C84
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x00079A8C File Offset: 0x00077C8C
		internal IEnumerable<Func<PropertyInfo, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00079A94 File Offset: 0x00077C94
		public PropertyConventionConfiguration Where(Func<PropertyInfo, bool> predicate)
		{
			Check.NotNull<Func<PropertyInfo, bool>>(predicate, "predicate");
			return new PropertyConventionConfiguration(this._conventionsConfiguration, this._predicates.Append(predicate));
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00079AB9 File Offset: 0x00077CB9
		public PropertyConventionWithHavingConfiguration<T> Having<T>(Func<PropertyInfo, T> capturingPredicate) where T : class
		{
			Check.NotNull<Func<PropertyInfo, T>>(capturingPredicate, "capturingPredicate");
			return new PropertyConventionWithHavingConfiguration<T>(this._conventionsConfiguration, this._predicates, capturingPredicate);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00079ADC File Offset: 0x00077CDC
		public void Configure(Action<ConventionPrimitivePropertyConfiguration> propertyConfigurationAction)
		{
			Check.NotNull<Action<ConventionPrimitivePropertyConfiguration>>(propertyConfigurationAction, "propertyConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new PropertyConvention(this._predicates, propertyConfigurationAction)
			});
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00079B17 File Offset: 0x00077D17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00079B1F File Offset: 0x00077D1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00079B28 File Offset: 0x00077D28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00079B30 File Offset: 0x00077D30
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000872 RID: 2162
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000873 RID: 2163
		private readonly IEnumerable<Func<PropertyInfo, bool>> _predicates;
	}
}
