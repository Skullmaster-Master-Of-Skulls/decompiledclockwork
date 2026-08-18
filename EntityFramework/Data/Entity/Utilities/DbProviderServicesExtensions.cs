using System;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200072C RID: 1836
	internal static class DbProviderServicesExtensions
	{
		// Token: 0x06004B6A RID: 19306 RVA: 0x00161B14 File Offset: 0x0015FD14
		public static string GetProviderManifestTokenChecked(this DbProviderServices providerServices, DbConnection connection)
		{
			string providerManifestToken;
			try
			{
				providerManifestToken = providerServices.GetProviderManifestToken(connection);
			}
			catch (ProviderIncompatibleException innerException)
			{
				throw new ProviderIncompatibleException(Strings.FailedToGetProviderInformation, innerException);
			}
			return providerManifestToken;
		}
	}
}
