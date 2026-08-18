using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Conventions.Sets;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C1 RID: 449
	public class Convention : IConvention
	{
		// Token: 0x06000F1A RID: 3866 RVA: 0x00040B81 File Offset: 0x0003ED81
		public Convention()
		{
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00040B99 File Offset: 0x0003ED99
		internal Convention(ConventionsConfiguration conventionsConfiguration)
		{
			this._conventionsConfiguration = conventionsConfiguration;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		public TypeConventionConfiguration Types()
		{
			return new TypeConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00040BC5 File Offset: 0x0003EDC5
		public TypeConventionConfiguration<T> Types<T>() where T : class
		{
			return new TypeConventionConfiguration<T>(this._conventionsConfiguration);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00040BD2 File Offset: 0x0003EDD2
		public PropertyConventionConfiguration Properties()
		{
			return new PropertyConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00040C0C File Offset: 0x0003EE0C
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public PropertyConventionConfiguration Properties<T>()
		{
			if (!typeof(T).IsValidEdmScalarType())
			{
				throw Error.ModelBuilder_PropertyFilterTypeMustBePrimitive(typeof(T));
			}
			PropertyConventionConfiguration propertyConventionConfiguration = new PropertyConventionConfiguration(this._conventionsConfiguration);
			return propertyConventionConfiguration.Where(delegate(PropertyInfo p)
			{
				Type left;
				p.PropertyType.TryUnwrapNullableType(out left);
				return left == typeof(T);
			});
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00040C58 File Offset: 0x0003EE58
		internal virtual void ApplyModelConfiguration(ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyModelConfiguration(modelConfiguration);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00040C66 File Offset: 0x0003EE66
		internal virtual void ApplyModelConfiguration(Type type, ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyModelConfiguration(type, modelConfiguration);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00040C75 File Offset: 0x0003EE75
		internal virtual void ApplyTypeConfiguration<TStructuralTypeConfiguration>(Type type, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			this._conventionsConfiguration.ApplyTypeConfiguration<TStructuralTypeConfiguration>(type, structuralTypeConfiguration, modelConfiguration);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00040C85 File Offset: 0x0003EE85
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, modelConfiguration);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00040C94 File Offset: 0x0003EE94
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
		{
			this._conventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, propertyConfiguration, modelConfiguration);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00040CA4 File Offset: 0x0003EEA4
		internal virtual void ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(PropertyInfo propertyInfo, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			this._conventionsConfiguration.ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(propertyInfo, structuralTypeConfiguration, modelConfiguration);
		}

		// Token: 0x04000419 RID: 1049
		private readonly ConventionsConfiguration _conventionsConfiguration = new ConventionsConfiguration(new ConventionSet());
	}
}
