using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000762 RID: 1890
	internal interface ICachedMetadataWorkspace
	{
		// Token: 0x06005546 RID: 21830
		MetadataWorkspace GetMetadataWorkspace(DbConnection storeConnection);

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06005547 RID: 21831
		IEnumerable<Assembly> Assemblies { get; }

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06005548 RID: 21832
		string DefaultContainerName { get; }

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06005549 RID: 21833
		DbProviderInfo ProviderInfo { get; }
	}
}
