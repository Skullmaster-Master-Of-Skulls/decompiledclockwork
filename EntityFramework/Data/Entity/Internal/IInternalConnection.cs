using System;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000769 RID: 1897
	internal interface IInternalConnection : IDisposable
	{
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600558C RID: 21900
		DbConnection Connection { get; }

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600558D RID: 21901
		string ConnectionKey { get; }

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x0600558E RID: 21902
		bool ConnectionHasModel { get; }

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x0600558F RID: 21903
		DbConnectionStringOrigin ConnectionStringOrigin { get; }

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06005590 RID: 21904
		// (set) Token: 0x06005591 RID: 21905
		AppConfig AppConfig { get; set; }

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06005592 RID: 21906
		// (set) Token: 0x06005593 RID: 21907
		string ProviderName { get; set; }

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06005594 RID: 21908
		string ConnectionStringName { get; }

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06005595 RID: 21909
		string OriginalConnectionString { get; }

		// Token: 0x06005596 RID: 21910
		ObjectContext CreateObjectContextFromConnectionModel();
	}
}
