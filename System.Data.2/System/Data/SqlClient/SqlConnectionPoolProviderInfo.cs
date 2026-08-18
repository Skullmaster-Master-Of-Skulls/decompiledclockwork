using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x020001BD RID: 445
	internal sealed class SqlConnectionPoolProviderInfo : DbConnectionPoolProviderInfo
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000BDE1C File Offset: 0x000BD21C
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x000BDE30 File Offset: 0x000BD230
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
			set
			{
				this._instanceName = value;
			}
		}

		// Token: 0x04000F9A RID: 3994
		private string _instanceName;
	}
}
