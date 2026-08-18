using System;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200072B RID: 1835
	internal static class DbModelBuilderVersionExtensions
	{
		// Token: 0x06004B68 RID: 19304 RVA: 0x00161ABC File Offset: 0x0015FCBC
		public static double GetEdmVersion(this DbModelBuilderVersion modelBuilderVersion)
		{
			switch (modelBuilderVersion)
			{
			case DbModelBuilderVersion.Latest:
			case DbModelBuilderVersion.V5_0:
			case DbModelBuilderVersion.V6_0:
				return 3.0;
			case DbModelBuilderVersion.V4_1:
			case DbModelBuilderVersion.V5_0_Net4:
				return 2.0;
			default:
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00161B05 File Offset: 0x0015FD05
		public static bool IsEF6OrHigher(this DbModelBuilderVersion modelBuilderVersion)
		{
			return modelBuilderVersion >= DbModelBuilderVersion.V6_0 || modelBuilderVersion == DbModelBuilderVersion.Latest;
		}
	}
}
