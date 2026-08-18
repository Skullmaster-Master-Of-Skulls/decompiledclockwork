using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.IO;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200075E RID: 1886
	public sealed class SqlCeConnectionFactory : IDbConnectionFactory
	{
		// Token: 0x06005535 RID: 21813 RVA: 0x00172B10 File Offset: 0x00170D10
		public SqlCeConnectionFactory(string providerInvariantName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this._providerInvariantName = providerInvariantName;
			this._databaseDirectory = "|DataDirectory|";
			this._baseConnectionString = "";
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00172B44 File Offset: 0x00170D44
		public SqlCeConnectionFactory(string providerInvariantName, string databaseDirectory, string baseConnectionString)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<string>(databaseDirectory, "databaseDirectory");
			Check.NotNull<string>(baseConnectionString, "baseConnectionString");
			this._providerInvariantName = providerInvariantName;
			this._databaseDirectory = databaseDirectory;
			this._baseConnectionString = baseConnectionString;
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06005537 RID: 21815 RVA: 0x00172B90 File Offset: 0x00170D90
		public string DatabaseDirectory
		{
			get
			{
				return this._databaseDirectory;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06005538 RID: 21816 RVA: 0x00172B98 File Offset: 0x00170D98
		public string BaseConnectionString
		{
			get
			{
				return this._baseConnectionString;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06005539 RID: 21817 RVA: 0x00172BA0 File Offset: 0x00170DA0
		public string ProviderInvariantName
		{
			get
			{
				return this._providerInvariantName;
			}
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00172BA8 File Offset: 0x00170DA8
		public DbConnection CreateConnection(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			DbProviderFactory service = DbConfiguration.DependencyResolver.GetService(this.ProviderInvariantName);
			DbConnection dbConnection = service.CreateConnection();
			if (dbConnection == null)
			{
				throw Error.DbContext_ProviderReturnedNullConnection();
			}
			string value;
			if (DbHelpers.TreatAsConnectionString(nameOrConnectionString))
			{
				value = nameOrConnectionString;
			}
			else
			{
				if (!nameOrConnectionString.EndsWith(".sdf", true, null))
				{
					nameOrConnectionString += ".sdf";
				}
				string text = (this.DatabaseDirectory.StartsWith("|", StringComparison.Ordinal) && this.DatabaseDirectory.EndsWith("|", StringComparison.Ordinal)) ? (this.DatabaseDirectory + nameOrConnectionString) : Path.Combine(this.DatabaseDirectory, nameOrConnectionString);
				value = string.Format(CultureInfo.InvariantCulture, "Data Source={0}; {1}", new object[]
				{
					text,
					this.BaseConnectionString
				});
			}
			DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(value));
			return dbConnection;
		}

		// Token: 0x040022A6 RID: 8870
		private readonly string _databaseDirectory;

		// Token: 0x040022A7 RID: 8871
		private readonly string _baseConnectionString;

		// Token: 0x040022A8 RID: 8872
		private readonly string _providerInvariantName;
	}
}
