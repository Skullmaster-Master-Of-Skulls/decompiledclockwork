using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007AA RID: 1962
	public class ConfigurationRegistrar
	{
		// Token: 0x06005887 RID: 22663 RVA: 0x0017C48B File Offset: 0x0017A68B
		internal ConfigurationRegistrar(ModelConfiguration modelConfiguration)
		{
			this._modelConfiguration = modelConfiguration;
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0017C49A File Offset: 0x0017A69A
		public virtual ConfigurationRegistrar AddFromAssembly(Assembly assembly)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			new ConfigurationTypesFinder().AddConfigurationTypesToModel(assembly.GetAccessibleTypes(), this._modelConfiguration);
			return this;
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x0017C4BF File Offset: 0x0017A6BF
		public virtual ConfigurationRegistrar Add<TEntityType>(EntityTypeConfiguration<TEntityType> entityTypeConfiguration) where TEntityType : class
		{
			Check.NotNull<EntityTypeConfiguration<TEntityType>>(entityTypeConfiguration, "entityTypeConfiguration");
			this._modelConfiguration.Add((EntityTypeConfiguration)entityTypeConfiguration.Configuration);
			return this;
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x0017C4E4 File Offset: 0x0017A6E4
		public virtual ConfigurationRegistrar Add<TComplexType>(ComplexTypeConfiguration<TComplexType> complexTypeConfiguration) where TComplexType : class
		{
			Check.NotNull<ComplexTypeConfiguration<TComplexType>>(complexTypeConfiguration, "complexTypeConfiguration");
			this._modelConfiguration.Add((ComplexTypeConfiguration)complexTypeConfiguration.Configuration);
			return this;
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x0017C509 File Offset: 0x0017A709
		internal virtual IEnumerable<Type> GetConfiguredTypes()
		{
			return this._modelConfiguration.ConfiguredTypes.ToList<Type>();
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x0017C51B File Offset: 0x0017A71B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x0017C523 File Offset: 0x0017A723
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x0017C52C File Offset: 0x0017A72C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x0017C534 File Offset: 0x0017A734
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002382 RID: 9090
		private readonly ModelConfiguration _modelConfiguration;
	}
}
