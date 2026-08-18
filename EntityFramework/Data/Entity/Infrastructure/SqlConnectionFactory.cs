using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200075F RID: 1887
	public sealed class SqlConnectionFactory : IDbConnectionFactory
	{
		// Token: 0x0600553B RID: 21819 RVA: 0x00172C93 File Offset: 0x00170E93
		public SqlConnectionFactory()
		{
			this._baseConnectionString = "Data Source=.\\SQLEXPRESS; Integrated Security=True; MultipleActiveResultSets=True;";
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00172CA6 File Offset: 0x00170EA6
		public SqlConnectionFactory(string baseConnectionString)
		{
			Check.NotNull<string>(baseConnectionString, "baseConnectionString");
			this._baseConnectionString = baseConnectionString;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600553D RID: 21821 RVA: 0x00172CC1 File Offset: 0x00170EC1
		// (set) Token: 0x0600553E RID: 21822 RVA: 0x00172CDD File Offset: 0x00170EDD
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Func<string, DbProviderFactory> ProviderFactory
		{
			get
			{
				return this._providerFactoryCreator ?? new Func<string, DbProviderFactory>(DbConfiguration.DependencyResolver.GetService<DbProviderFactory>);
			}
			set
			{
				this._providerFactoryCreator = value;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600553F RID: 21823 RVA: 0x00172CE6 File Offset: 0x00170EE6
		public string BaseConnectionString
		{
			get
			{
				return this._baseConnectionString;
			}
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x00172CF0 File Offset: 0x00170EF0
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public DbConnection CreateConnection(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			string value = nameOrConnectionString;
			if (!DbHelpers.TreatAsConnectionString(nameOrConnectionString))
			{
				if (nameOrConnectionString.EndsWith(".mdf", true, null))
				{
					throw Error.SqlConnectionFactory_MdfNotSupported(nameOrConnectionString);
				}
				value = new SqlConnectionStringBuilder(this.BaseConnectionString)
				{
					InitialCatalog = nameOrConnectionString
				}.ConnectionString;
			}
			DbConnection dbConnection = null;
			try
			{
				dbConnection = this.ProviderFactory("System.Data.SqlClient").CreateConnection();
				DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(value));
			}
			catch
			{
				dbConnection = new SqlConnection();
				DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(value));
			}
			return dbConnection;
		}

		// Token: 0x040022A9 RID: 8873
		private readonly string _baseConnectionString;

		// Token: 0x040022AA RID: 8874
		private Func<string, DbProviderFactory> _providerFactoryCreator;
	}
}
