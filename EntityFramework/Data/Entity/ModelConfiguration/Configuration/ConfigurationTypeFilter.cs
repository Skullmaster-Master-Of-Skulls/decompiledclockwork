using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002AE RID: 686
	internal class ConfigurationTypeFilter
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x0007996C File Offset: 0x00077B6C
		public virtual bool IsEntityTypeConfiguration(Type type)
		{
			return ConfigurationTypeFilter.IsStructuralTypeConfiguration(type, typeof(EntityTypeConfiguration<>));
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0007997E File Offset: 0x00077B7E
		public virtual bool IsComplexTypeConfiguration(Type type)
		{
			return ConfigurationTypeFilter.IsStructuralTypeConfiguration(type, typeof(ComplexTypeConfiguration<>));
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00079990 File Offset: 0x00077B90
		private static bool IsStructuralTypeConfiguration(Type type, Type structuralTypeConfiguration)
		{
			return !type.IsAbstract() && type.TryGetElementType(structuralTypeConfiguration) != null;
		}
	}
}
