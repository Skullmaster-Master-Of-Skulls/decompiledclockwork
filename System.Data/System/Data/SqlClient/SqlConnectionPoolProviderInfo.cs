using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x020002D3 RID: 723
	internal sealed class SqlConnectionPoolProviderInfo : DbConnectionPoolProviderInfo
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x00299BE8 File Offset: 0x00298FE8
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x00299C08 File Offset: 0x00299008
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

		// Token: 0x04001799 RID: 6041
		private string _instanceName;
	}
}
