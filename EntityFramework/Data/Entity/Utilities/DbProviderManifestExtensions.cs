using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200082C RID: 2092
	internal static class DbProviderManifestExtensions
	{
		// Token: 0x06005DC7 RID: 24007 RVA: 0x001957CC File Offset: 0x001939CC
		public static PrimitiveType GetStoreTypeFromName(this DbProviderManifest providerManifest, string name)
		{
			return providerManifest.GetStoreTypes().Single((PrimitiveType p) => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
		}
	}
}
