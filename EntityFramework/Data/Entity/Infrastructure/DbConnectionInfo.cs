using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x020006B2 RID: 1714
	[Serializable]
	public class DbConnectionInfo
	{
		// Token: 0x06004464 RID: 17508 RVA: 0x00143F04 File Offset: 0x00142104
		public DbConnectionInfo(string connectionName)
		{
			Check.NotEmpty(connectionName, "connectionName");
			this._connectionName = connectionName;
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x00143F1F File Offset: 0x0014211F
		public DbConnectionInfo(string connectionString, string providerInvariantName)
		{
			Check.NotEmpty(connectionString, "connectionString");
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this._connectionString = connectionString;
			this._providerInvariantName = providerInvariantName;
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x00143F50 File Offset: 0x00142150
		internal ConnectionStringSettings GetConnectionString(AppConfig config)
		{
			if (this._connectionName == null)
			{
				return new ConnectionStringSettings(null, this._connectionString, this._providerInvariantName);
			}
			ConnectionStringSettings connectionString = config.GetConnectionString(this._connectionName);
			if (connectionString == null)
			{
				throw Error.DbConnectionInfo_ConnectionStringNotFound(this._connectionName);
			}
			return connectionString;
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x00143F95 File Offset: 0x00142195
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x00143F9D File Offset: 0x0014219D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x00143FA6 File Offset: 0x001421A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x00143FAE File Offset: 0x001421AE
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04001930 RID: 6448
		private readonly string _connectionName;

		// Token: 0x04001931 RID: 6449
		private readonly string _connectionString;

		// Token: 0x04001932 RID: 6450
		private readonly string _providerInvariantName;
	}
}
