using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001B4 RID: 436
	internal class ConventionsTypeFilter
	{
		// Token: 0x06000E9A RID: 3738 RVA: 0x0003F77C File Offset: 0x0003D97C
		public virtual bool IsConvention(Type conventionType)
		{
			return ConventionsTypeFilter.IsConfigurationConvention(conventionType) || ConventionsTypeFilter.IsConceptualModelConvention(conventionType) || ConventionsTypeFilter.IsConceptualToStoreMappingConvention(conventionType) || ConventionsTypeFilter.IsStoreModelConvention(conventionType);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003F7A0 File Offset: 0x0003D9A0
		public static bool IsConfigurationConvention(Type conventionType)
		{
			return typeof(IConfigurationConvention).IsAssignableFrom(conventionType) || typeof(Convention).IsAssignableFrom(conventionType) || conventionType.GetGenericTypeImplementations(typeof(IConfigurationConvention<>)).Any<Type>() || conventionType.GetGenericTypeImplementations(typeof(IConfigurationConvention<, >)).Any<Type>();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003F7FF File Offset: 0x0003D9FF
		public static bool IsConceptualModelConvention(Type conventionType)
		{
			return conventionType.GetGenericTypeImplementations(typeof(IConceptualModelConvention<>)).Any<Type>();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003F816 File Offset: 0x0003DA16
		public static bool IsStoreModelConvention(Type conventionType)
		{
			return conventionType.GetGenericTypeImplementations(typeof(IStoreModelConvention<>)).Any<Type>();
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003F82D File Offset: 0x0003DA2D
		public static bool IsConceptualToStoreMappingConvention(Type conventionType)
		{
			return typeof(IDbMappingConvention).IsAssignableFrom(conventionType);
		}
	}
}
