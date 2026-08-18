using System;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200082B RID: 2091
	internal static class DbProviderInfoExtensions
	{
		// Token: 0x06005DC6 RID: 24006 RVA: 0x0019578C File Offset: 0x0019398C
		public static bool IsSqlCe(this DbProviderInfo providerInfo)
		{
			return !string.IsNullOrWhiteSpace(providerInfo.ProviderInvariantName) && providerInfo.ProviderInvariantName.StartsWith("System.Data.SqlServerCe", StringComparison.OrdinalIgnoreCase);
		}
	}
}
