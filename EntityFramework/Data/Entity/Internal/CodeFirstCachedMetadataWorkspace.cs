using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000763 RID: 1891
	internal class CodeFirstCachedMetadataWorkspace : ICachedMetadataWorkspace
	{
		// Token: 0x0600554A RID: 21834 RVA: 0x00172DEC File Offset: 0x00170FEC
		public CodeFirstCachedMetadataWorkspace(DbDatabaseMapping databaseMapping)
		{
			this._providerInfo = databaseMapping.ProviderInfo;
			this._metadataWorkspace = databaseMapping.ToMetadataWorkspace();
			this._assemblies = (from t in databaseMapping.Model.GetClrTypes()
			select t.Assembly()).Distinct<Assembly>().ToList<Assembly>();
			this._defaultContainerName = databaseMapping.Model.Containers.First<EntityContainer>().Name;
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x00172E70 File Offset: 0x00171070
		public MetadataWorkspace GetMetadataWorkspace(DbConnection connection)
		{
			string providerInvariantName = connection.GetProviderInvariantName();
			if (!string.Equals(this._providerInfo.ProviderInvariantName, providerInvariantName, StringComparison.Ordinal))
			{
				throw Error.CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported();
			}
			return this._metadataWorkspace;
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x0600554C RID: 21836 RVA: 0x00172EA4 File Offset: 0x001710A4
		public string DefaultContainerName
		{
			get
			{
				return this._defaultContainerName;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x0600554D RID: 21837 RVA: 0x00172EAC File Offset: 0x001710AC
		public IEnumerable<Assembly> Assemblies
		{
			get
			{
				return this._assemblies;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600554E RID: 21838 RVA: 0x00172EB4 File Offset: 0x001710B4
		public DbProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
		}

		// Token: 0x040022AB RID: 8875
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x040022AC RID: 8876
		private readonly IEnumerable<Assembly> _assemblies;

		// Token: 0x040022AD RID: 8877
		private readonly DbProviderInfo _providerInfo;

		// Token: 0x040022AE RID: 8878
		private readonly string _defaultContainerName;
	}
}
