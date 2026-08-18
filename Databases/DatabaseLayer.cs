using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ClockWorkLogger;
using EncryptionClassLibrary;
using TechnoPro.Common.Configuration;

namespace Databases
{
	// Token: 0x02000005 RID: 5
	public class DatabaseLayer
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002784 File Offset: 0x00000984
		public DatabaseLayer()
		{
			this.DatabaseRole = eDatabaseConnectionStringName.ClockWork;
			this.ProviderName = ProviderNames.SqlClient;
			this.setupConnectionString();
			this.setupDbEncryption();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000027B0 File Offset: 0x000009B0
		public DatabaseLayer(eDatabaseConnectionStringName databaseRole)
		{
			this.DatabaseRole = databaseRole;
			this.ProviderName = ProviderNames.SqlClient;
			this.setupConnectionString();
			this.setupDbEncryption();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027DC File Offset: 0x000009DC
		public DatabaseLayer(string providerName, string connectionString, string databaseEncryptionPassword)
		{
			this.DatabaseRole = eDatabaseConnectionStringName.ClockWork;
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.EncryptionPassword = databaseEncryptionPassword;
			this.Encryption = EncryptionFactory.GetEncryption(this.GetDbEncryptionType(), databaseEncryptionPassword);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002819 File Offset: 0x00000A19
		public DatabaseLayer(string providerName, string connectionString, string encryptionPassword, IEncryption encryption)
		{
			this.DatabaseRole = eDatabaseConnectionStringName.ClockWork;
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.EncryptionPassword = encryptionPassword;
			this.Encryption = encryption;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000284C File Offset: 0x00000A4C
		public DatabaseLayer(eDatabaseConnectionStringName databaseRole, string providerName, string connectionString, string encryptionPassword, IEncryption encryption)
		{
			this.DatabaseRole = databaseRole;
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.EncryptionPassword = encryptionPassword;
			this.Encryption = encryption;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002880 File Offset: 0x00000A80
		public DatabaseLayer(eDatabaseConnectionStringName databaseRole, string providerName, string connectionString, string databaseEncryptionPassword)
		{
			this.DatabaseRole = databaseRole;
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.EncryptionPassword = databaseEncryptionPassword;
			this.Encryption = EncryptionFactory.GetEncryption(this.GetDbEncryptionType(), databaseEncryptionPassword);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028BF File Offset: 0x00000ABF
		[Obsolete("Use DatabaseLayerFactory properties instead")]
		public static DatabaseLayer GetInstance()
		{
			return DatabaseLayerFactory.ClockWork;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000028BF File Offset: 0x00000ABF
		[Obsolete("Use DatabaseLayerFactory properties instead")]
		public static DatabaseLayer CurrentInstance
		{
			get
			{
				return DatabaseLayerFactory.ClockWork;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000028C6 File Offset: 0x00000AC6
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000028CE File Offset: 0x00000ACE
		public eDatabaseConnectionStringName DatabaseRole { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000028D7 File Offset: 0x00000AD7
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000028DF File Offset: 0x00000ADF
		public string EncryptionPassword { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000028E8 File Offset: 0x00000AE8
		public static IEncryption GetDatabaseEncryption(string InstanceProviderName, string InstanceConnectionString, string databaseEncryptionPassword)
		{
			DatabaseLayer databaseLayer = new DatabaseLayer(eDatabaseConnectionStringName.ClockWork, InstanceProviderName, InstanceConnectionString, databaseEncryptionPassword);
			return databaseLayer.Encryption;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000290C File Offset: 0x00000B0C
		public bool TestDatabaseConnectivity(string query = null)
		{
			string query2 = string.IsNullOrEmpty(query) ? "EXEC sp_tables" : query;
			bool result;
			try
			{
				using (DataTable dataTable = this.ExecuteQuery(query2))
				{
					bool flag = dataTable != null;
					if (flag)
					{
						bool flag2 = dataTable.Columns.Count == 0;
						if (flag2)
						{
							CWLogger.Logger.Error("Missing ClockWork Database!!!");
							return false;
						}
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer::TestDatabaseConnectivity: Connection to Database role '{0}' failed: {1}", this.DatabaseRole, ex.ToString()), ex);
				result = false;
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsValidDbConnection(string providerName, string cs, out Exception ex)
		{
			ex = null;
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				using (DbConnection dbConnection = factory.CreateConnection())
				{
					dbConnection.ConnectionString = cs;
					dbConnection.Open();
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				return false;
			}
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A38 File Offset: 0x00000C38
		public DbProviderFactory GetFactory(string providerName)
		{
			return DbProviderFactories.GetFactory(providerName);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A50 File Offset: 0x00000C50
		public DbTransaction BeginDbTransaction()
		{
			DbConnection dbConnection = null;
			DbTransaction result;
			try
			{
				dbConnection = this.Connection;
				bool flag = dbConnection.State != ConnectionState.Open;
				if (flag)
				{
					dbConnection.Open();
				}
				result = dbConnection.BeginTransaction();
			}
			catch (DbException exception)
			{
				CWLogger.Logger.ErrorException(string.Format("Begin transaction:{0}: ", this.DatabaseRole), exception);
				bool flag2 = dbConnection != null && dbConnection.State > ConnectionState.Closed;
				if (flag2)
				{
					dbConnection.Close();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002ADC File Offset: 0x00000CDC
		public void CommitDbTransaction(DbTransaction transaction)
		{
			using (transaction.Connection)
			{
				try
				{
					transaction.Commit();
				}
				catch (DbException exception)
				{
					CWLogger.Logger.ErrorException(string.Format("Commit transaction:{0}: ", this.DatabaseRole), exception);
					this.RollbackDbTransaction(transaction);
					throw;
				}
				finally
				{
					if (transaction != null)
					{
						((IDisposable)transaction).Dispose();
					}
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B74 File Offset: 0x00000D74
		public void RollbackDbTransaction(DbTransaction transaction)
		{
			try
			{
				if (transaction != null)
				{
					transaction.Rollback();
				}
			}
			catch (DbException exception)
			{
				CWLogger.Logger.ErrorException(string.Format("Rollback transaction:{0}: ", this.DatabaseRole), exception);
				throw;
			}
			finally
			{
				bool flag = ((transaction != null) ? transaction.Connection : null) != null && transaction.Connection.State > ConnectionState.Closed;
				if (flag)
				{
					transaction.Connection.Close();
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C08 File Offset: 0x00000E08
		[Obsolete("Use BeginDbTransaction method instead")]
		public DbCommand BeginTransaction()
		{
			DbCommand dbCommand = null;
			DbConnection dbConnection = null;
			try
			{
				dbConnection = this.Connection;
				dbConnection.Open();
				dbCommand = dbConnection.CreateCommand();
				DbTransaction transaction = dbConnection.BeginTransaction();
				dbCommand.Connection = dbConnection;
				dbCommand.Transaction = transaction;
			}
			catch (DbException exception)
			{
				CWLogger.Logger.ErrorException(string.Format("Begin transaction:{0}: ", this.DatabaseRole), exception);
				bool flag = dbConnection != null && dbConnection.State > ConnectionState.Closed;
				if (flag)
				{
					dbConnection.Close();
					throw;
				}
			}
			return dbCommand;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[Obsolete("Use BeginDbTransaction method instead")]
		public bool CommitTransaction(DbCommand dbCommand)
		{
			bool result;
			using (dbCommand.Connection)
			{
				try
				{
					using (DbTransaction transaction = dbCommand.Transaction)
					{
						try
						{
							transaction.Commit();
							result = true;
						}
						catch (DbException exception)
						{
							CWLogger.Logger.ErrorException(string.Format("Commit transaction:{0}: ", this.DatabaseRole), exception);
							try
							{
								if (transaction != null)
								{
									transaction.Rollback();
								}
							}
							catch (DbException exception2)
							{
								CWLogger.Logger.ErrorException(string.Format("Commit transaction rollback:{0}: ", this.DatabaseRole), exception2);
								throw;
							}
							throw;
						}
					}
				}
				finally
				{
					if (dbCommand != null)
					{
						((IDisposable)dbCommand).Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D98 File Offset: 0x00000F98
		[Obsolete("Use BeginDbTransaction method instead")]
		public void RollbackTransaction(DbCommand dbCommand)
		{
			using (dbCommand.Connection)
			{
				try
				{
					using (DbTransaction transaction = dbCommand.Transaction)
					{
						try
						{
							if (transaction != null)
							{
								transaction.Rollback();
							}
						}
						catch (DbException exception)
						{
							CWLogger.Logger.ErrorException(string.Format("Rollback transaction:{0}: ", this.DatabaseRole), exception);
						}
					}
				}
				finally
				{
					if (dbCommand != null)
					{
						((IDisposable)dbCommand).Dispose();
					}
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E50 File Offset: 0x00001050
		public DbParameter GetParameter(string pName, DbType pType, object value)
		{
			DbParameter parameter = this.Parameter;
			bool flag = value == null;
			if (flag)
			{
				parameter.IsNullable = true;
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = value;
			}
			parameter.ParameterName = pName;
			parameter.DbType = pType;
			return parameter;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002EA4 File Offset: 0x000010A4
		public DbParameter GetParameter(string pName, DbType pType, object value, int size)
		{
			DbParameter parameter = this.Parameter;
			bool flag = value == null;
			if (flag)
			{
				parameter.IsNullable = true;
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = value;
			}
			parameter.ParameterName = pName;
			parameter.DbType = pType;
			parameter.Size = size;
			return parameter;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F00 File Offset: 0x00001100
		public DbParameter GetInOutParameter(string pName, DbType pType, object value)
		{
			DbParameter parameter = this.Parameter;
			bool flag = value == null;
			if (flag)
			{
				parameter.IsNullable = true;
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = value;
			}
			parameter.ParameterName = pName;
			parameter.DbType = pType;
			parameter.Direction = ParameterDirection.InputOutput;
			return parameter;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F5C File Offset: 0x0000115C
		public DbParameter GetOutputParameter(string pName, DbType pType, int size = 0)
		{
			DbParameter parameter = this.Parameter;
			parameter.ParameterName = pName;
			parameter.DbType = pType;
			parameter.Direction = ParameterDirection.Output;
			bool flag = size > 0;
			if (flag)
			{
				parameter.Size = size;
			}
			return parameter;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002FA0 File Offset: 0x000011A0
		public object ExecuteScalar(string query, string[] parametersName, object[] parametersValue, CommandOverrideSettings cmdOverrideSettings)
		{
			bool flag = parametersName.Length == 0 || parametersValue.Length == 0 || parametersValue.Length != parametersName.Length;
			object result;
			if (flag)
			{
				result = this.ExecuteScalar(query);
			}
			else
			{
				DbParameter[] array = new DbParameter[parametersName.Length];
				for (int i = 0; i < parametersName.Length; i++)
				{
					DbParameter parameter = this.Parameter;
					parameter.ParameterName = parametersName[i];
					parameter.Value = parametersValue[i];
					array[i] = parameter;
				}
				result = this.ExecuteScalar(query, cmdOverrideSettings, array);
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003024 File Offset: 0x00001224
		[DebuggerStepThrough]
		public Task<object> ExecuteScalarAsync(string query, string[] parametersName, object[] parametersValue, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteScalarAsync>d__33 <ExecuteScalarAsync>d__ = new DatabaseLayer.<ExecuteScalarAsync>d__33();
			<ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ExecuteScalarAsync>d__.<>4__this = this;
			<ExecuteScalarAsync>d__.query = query;
			<ExecuteScalarAsync>d__.parametersName = parametersName;
			<ExecuteScalarAsync>d__.parametersValue = parametersValue;
			<ExecuteScalarAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteScalarAsync>d__.<>1__state = -1;
			<ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteScalarAsync>d__33>(ref <ExecuteScalarAsync>d__);
			return <ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003088 File Offset: 0x00001288
		public object ExecuteScalar(string query, string[] parametersName, object[] parametersValue)
		{
			return this.ExecuteScalar(query, parametersName, parametersValue, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000030A4 File Offset: 0x000012A4
		[DebuggerStepThrough]
		public Task<object> ExecuteScalarAsync(string query, string[] parametersName, object[] parametersValue)
		{
			DatabaseLayer.<ExecuteScalarAsync>d__35 <ExecuteScalarAsync>d__ = new DatabaseLayer.<ExecuteScalarAsync>d__35();
			<ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ExecuteScalarAsync>d__.<>4__this = this;
			<ExecuteScalarAsync>d__.query = query;
			<ExecuteScalarAsync>d__.parametersName = parametersName;
			<ExecuteScalarAsync>d__.parametersValue = parametersValue;
			<ExecuteScalarAsync>d__.<>1__state = -1;
			<ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteScalarAsync>d__35>(ref <ExecuteScalarAsync>d__);
			return <ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003100 File Offset: 0x00001300
		public object ExecuteScalar(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			return DatabaseLayer.Retry<object>(() => this._ExecuteScalar(query, cmdOverrideSettings, parameters));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003148 File Offset: 0x00001348
		[DebuggerStepThrough]
		public Task<object> ExecuteScalarAsync(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteScalarAsync>d__37 <ExecuteScalarAsync>d__ = new DatabaseLayer.<ExecuteScalarAsync>d__37();
			<ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ExecuteScalarAsync>d__.<>4__this = this;
			<ExecuteScalarAsync>d__.query = query;
			<ExecuteScalarAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteScalarAsync>d__.parameters = parameters;
			<ExecuteScalarAsync>d__.<>1__state = -1;
			<ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteScalarAsync>d__37>(ref <ExecuteScalarAsync>d__);
			return <ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000031A4 File Offset: 0x000013A4
		public object ExecuteScalar(string query, params DbParameter[] parameters)
		{
			return this.ExecuteScalar(query, null, parameters);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031C0 File Offset: 0x000013C0
		[DebuggerStepThrough]
		public Task<object> ExecuteScalarAsync(string query, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteScalarAsync>d__39 <ExecuteScalarAsync>d__ = new DatabaseLayer.<ExecuteScalarAsync>d__39();
			<ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ExecuteScalarAsync>d__.<>4__this = this;
			<ExecuteScalarAsync>d__.query = query;
			<ExecuteScalarAsync>d__.parameters = parameters;
			<ExecuteScalarAsync>d__.<>1__state = -1;
			<ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteScalarAsync>d__39>(ref <ExecuteScalarAsync>d__);
			return <ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003214 File Offset: 0x00001414
		protected object _ExecuteScalar(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			object result;
			try
			{
				object obj = null;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						try
						{
							command.Connection = connection;
							bool flag = parameters != null && parameters.Length != 0;
							if (flag)
							{
								command.Parameters.AddRange(parameters);
							}
							command.CommandText = query;
							command.CommandType = CommandType.Text;
							obj = command.ExecuteScalar();
						}
						finally
						{
							bool flag2 = command != null;
							if (flag2)
							{
								command.Parameters.Clear();
							}
						}
					}
				}
				result = obj;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteScalar: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000331C File Offset: 0x0000151C
		[DebuggerStepThrough]
		protected Task<object> _ExecuteScalarAsync(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<_ExecuteScalarAsync>d__41 <_ExecuteScalarAsync>d__ = new DatabaseLayer.<_ExecuteScalarAsync>d__41();
			<_ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<_ExecuteScalarAsync>d__.<>4__this = this;
			<_ExecuteScalarAsync>d__.query = query;
			<_ExecuteScalarAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteScalarAsync>d__.parameters = parameters;
			<_ExecuteScalarAsync>d__.<>1__state = -1;
			<_ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteScalarAsync>d__41>(ref <_ExecuteScalarAsync>d__);
			return <_ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003378 File Offset: 0x00001578
		protected object _ExecuteScalar(string query, params DbParameter[] parameters)
		{
			return this._ExecuteScalar(query, null, parameters);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003394 File Offset: 0x00001594
		[DebuggerStepThrough]
		protected Task<object> _ExecuteScalarAsync(string query, params DbParameter[] parameters)
		{
			DatabaseLayer.<_ExecuteScalarAsync>d__43 <_ExecuteScalarAsync>d__ = new DatabaseLayer.<_ExecuteScalarAsync>d__43();
			<_ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<_ExecuteScalarAsync>d__.<>4__this = this;
			<_ExecuteScalarAsync>d__.query = query;
			<_ExecuteScalarAsync>d__.parameters = parameters;
			<_ExecuteScalarAsync>d__.<>1__state = -1;
			<_ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteScalarAsync>d__43>(ref <_ExecuteScalarAsync>d__);
			return <_ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000033E8 File Offset: 0x000015E8
		public object ExecuteScalar(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			return DatabaseLayer.Retry<object>(() => this._ExecuteScalar(query, cmdOverrideSettings));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003428 File Offset: 0x00001628
		[DebuggerStepThrough]
		public Task<object> ExecuteScalarAsync(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteScalarAsync>d__45 <ExecuteScalarAsync>d__ = new DatabaseLayer.<ExecuteScalarAsync>d__45();
			<ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ExecuteScalarAsync>d__.<>4__this = this;
			<ExecuteScalarAsync>d__.query = query;
			<ExecuteScalarAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteScalarAsync>d__.<>1__state = -1;
			<ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteScalarAsync>d__45>(ref <ExecuteScalarAsync>d__);
			return <ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000347C File Offset: 0x0000167C
		public object ExecuteScalar(string query)
		{
			return this.ExecuteScalar(query, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003498 File Offset: 0x00001698
		[DebuggerStepThrough]
		public Task<object> ExecuteScalarAsync(string query)
		{
			DatabaseLayer.<ExecuteScalarAsync>d__47 <ExecuteScalarAsync>d__ = new DatabaseLayer.<ExecuteScalarAsync>d__47();
			<ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ExecuteScalarAsync>d__.<>4__this = this;
			<ExecuteScalarAsync>d__.query = query;
			<ExecuteScalarAsync>d__.<>1__state = -1;
			<ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteScalarAsync>d__47>(ref <ExecuteScalarAsync>d__);
			return <ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000034E4 File Offset: 0x000016E4
		protected object _ExecuteScalar(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			object result;
			try
			{
				object obj = null;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						command.Connection = connection;
						command.CommandText = query;
						command.CommandType = CommandType.Text;
						obj = command.ExecuteScalar();
					}
				}
				result = obj;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteScalar: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000035A8 File Offset: 0x000017A8
		[DebuggerStepThrough]
		protected Task<object> _ExecuteScalarAsync(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<_ExecuteScalarAsync>d__49 <_ExecuteScalarAsync>d__ = new DatabaseLayer.<_ExecuteScalarAsync>d__49();
			<_ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<_ExecuteScalarAsync>d__.<>4__this = this;
			<_ExecuteScalarAsync>d__.query = query;
			<_ExecuteScalarAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteScalarAsync>d__.<>1__state = -1;
			<_ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteScalarAsync>d__49>(ref <_ExecuteScalarAsync>d__);
			return <_ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000035FC File Offset: 0x000017FC
		protected object _ExecuteScalar(string query)
		{
			return this._ExecuteScalar(query, null);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003618 File Offset: 0x00001818
		[DebuggerStepThrough]
		protected Task<object> _ExecuteScalarAsync(string query)
		{
			DatabaseLayer.<_ExecuteScalarAsync>d__51 <_ExecuteScalarAsync>d__ = new DatabaseLayer.<_ExecuteScalarAsync>d__51();
			<_ExecuteScalarAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<_ExecuteScalarAsync>d__.<>4__this = this;
			<_ExecuteScalarAsync>d__.query = query;
			<_ExecuteScalarAsync>d__.<>1__state = -1;
			<_ExecuteScalarAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteScalarAsync>d__51>(ref <_ExecuteScalarAsync>d__);
			return <_ExecuteScalarAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003664 File Offset: 0x00001864
		public int ExecuteStoredProcedure(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			return DatabaseLayer.Retry<int>(() => this._ExecuteStoredProcedure(storeProcedureName, cmdOverrideSettings, parameters));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000036AC File Offset: 0x000018AC
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__53 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__53();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteStoredProcedureAsync>d__.parameters = parameters;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__53>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003708 File Offset: 0x00001908
		public int ExecuteStoredProcedure(string storeProcedureName, params DbParameter[] parameters)
		{
			return this.ExecuteStoredProcedure(storeProcedureName, null, parameters);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003724 File Offset: 0x00001924
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__55 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__55();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.parameters = parameters;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__55>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003778 File Offset: 0x00001978
		public int ExecuteStoredProcedureTransaction(string storedProcedureName, DbTransaction transaction, params DbParameter[] parameters)
		{
			return this.ExecuteStoredProcedureTransaction(storedProcedureName, transaction, null, parameters);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003794 File Offset: 0x00001994
		public int ExecuteStoredProcedureTransaction(string storedProcedureName, DbTransaction transaction, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			int result = 0;
			using (DbCommand command = this.GetCommand(cmdOverrideSettings))
			{
				try
				{
					command.Connection = transaction.Connection;
					command.Transaction = transaction;
					bool flag = parameters != null && parameters.Length != 0;
					if (flag)
					{
						command.Parameters.AddRange(parameters);
					}
					command.CommandText = storedProcedureName;
					command.CommandType = CommandType.StoredProcedure;
					result = command.ExecuteNonQuery();
				}
				catch (DbException ex)
				{
					CWLogger.Logger.ErrorException(string.Format("Execute stored procedure transaction:{0}, errorType={1}, query='{2}' ", this.DatabaseRole, ex.ErrorCode, storedProcedureName), ex);
					this.RollbackDbTransaction(transaction);
					throw;
				}
				finally
				{
					if (command != null)
					{
						command.Parameters.Clear();
					}
				}
			}
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003888 File Offset: 0x00001A88
		protected int _ExecuteStoredProcedure(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			int result;
			try
			{
				int num = 0;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						try
						{
							command.Connection = connection;
							bool flag = parameters != null && parameters.Length != 0;
							if (flag)
							{
								command.Parameters.AddRange(parameters);
							}
							command.CommandText = storeProcedureName;
							command.CommandType = CommandType.StoredProcedure;
							num = command.ExecuteNonQuery();
						}
						finally
						{
							if (command != null)
							{
								command.Parameters.Clear();
							}
						}
					}
				}
				result = num;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteStoredProcedure: errorType='{0}', spName='{1}'. {2}", ex.ErrorCode, storeProcedureName, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000398C File Offset: 0x00001B8C
		[DebuggerStepThrough]
		protected Task<int> _ExecuteStoredProcedureAsync(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<_ExecuteStoredProcedureAsync>d__59 <_ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<_ExecuteStoredProcedureAsync>d__59();
			<_ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<_ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<_ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<_ExecuteStoredProcedureAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteStoredProcedureAsync>d__.parameters = parameters;
			<_ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<_ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteStoredProcedureAsync>d__59>(ref <_ExecuteStoredProcedureAsync>d__);
			return <_ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000039E8 File Offset: 0x00001BE8
		public int ExecuteStoredProcedure(string storeProcedureName, string[] inParametersName, object[] inParametersValue, string[] outParametersName, object[] outParametersValue, CommandOverrideSettings cmdOverrideSettings)
		{
			DbParameter[] array = null;
			bool flag = inParametersName != null && inParametersName.Length != 0 && inParametersValue != null && inParametersValue.Length != 0 && inParametersValue.Length == inParametersName.Length;
			if (flag)
			{
				array = new DbParameter[inParametersName.Length];
				for (int i = 0; i < inParametersName.Length; i++)
				{
					DbParameter parameter = this.Parameter;
					parameter.ParameterName = inParametersName[i];
					parameter.Value = inParametersValue[i];
					parameter.Direction = ParameterDirection.Input;
					array[i] = parameter;
				}
			}
			DbParameter[] array2 = null;
			bool flag2 = outParametersName != null && outParametersName.Length != 0 && outParametersValue != null && outParametersValue.Length != 0 && outParametersValue.Length == outParametersName.Length;
			if (flag2)
			{
				array2 = new DbParameter[outParametersName.Length];
				for (int j = 0; j < outParametersName.Length; j++)
				{
					DbParameter parameter = this.Parameter;
					parameter.ParameterName = outParametersName[j];
					parameter.Value = outParametersValue[j];
					parameter.Direction = ParameterDirection.Output;
					array2[j] = parameter;
				}
			}
			return (array == null && array2 == null) ? this.ExecuteStoredProcedure(storeProcedureName, cmdOverrideSettings) : this.ExecuteStoredProcedure(storeProcedureName, array, array2, cmdOverrideSettings);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003B04 File Offset: 0x00001D04
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, string[] inParametersName, object[] inParametersValue, string[] outParametersName, object[] outParametersValue, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__61 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__61();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.inParametersName = inParametersName;
			<ExecuteStoredProcedureAsync>d__.inParametersValue = inParametersValue;
			<ExecuteStoredProcedureAsync>d__.outParametersName = outParametersName;
			<ExecuteStoredProcedureAsync>d__.outParametersValue = outParametersValue;
			<ExecuteStoredProcedureAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__61>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003B78 File Offset: 0x00001D78
		public int ExecuteStoredProcedure(string storeProcedureName, string[] inParametersName, object[] inParametersValue, string[] outParametersName, object[] outParametersValue)
		{
			return this.ExecuteStoredProcedure(storeProcedureName, inParametersName, inParametersValue, outParametersName, outParametersValue, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003B98 File Offset: 0x00001D98
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, string[] inParametersName, object[] inParametersValue, string[] outParametersName, object[] outParametersValue)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__63 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__63();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.inParametersName = inParametersName;
			<ExecuteStoredProcedureAsync>d__.inParametersValue = inParametersValue;
			<ExecuteStoredProcedureAsync>d__.outParametersName = outParametersName;
			<ExecuteStoredProcedureAsync>d__.outParametersValue = outParametersValue;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__63>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003C04 File Offset: 0x00001E04
		public int ExecuteStoredProcedure(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			return DatabaseLayer.Retry<int>(() => this._ExecuteStoredProcedure(storeProcedureName, inParameters, outParameters, cmdOverrideSettings));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003C54 File Offset: 0x00001E54
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__65 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__65();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.inParameters = inParameters;
			<ExecuteStoredProcedureAsync>d__.outParameters = outParameters;
			<ExecuteStoredProcedureAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__65>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public int ExecuteStoredProcedure(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters)
		{
			return this.ExecuteStoredProcedure(storeProcedureName, inParameters, outParameters, null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003CD4 File Offset: 0x00001ED4
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__67 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__67();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.inParameters = inParameters;
			<ExecuteStoredProcedureAsync>d__.outParameters = outParameters;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__67>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003D30 File Offset: 0x00001F30
		protected int _ExecuteStoredProcedure(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			int result;
			try
			{
				int num = 0;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						try
						{
							command.Connection = connection;
							bool flag = inParameters != null && inParameters.Length != 0;
							if (flag)
							{
								command.Parameters.AddRange(inParameters);
							}
							bool flag2 = outParameters != null && outParameters.Length != 0;
							if (flag2)
							{
								command.Parameters.AddRange(outParameters);
							}
							command.CommandText = storeProcedureName;
							command.CommandType = CommandType.StoredProcedure;
							num = command.ExecuteNonQuery();
						}
						finally
						{
							bool flag3 = command != null;
							if (flag3)
							{
								command.Parameters.Clear();
							}
						}
					}
				}
				result = num;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteStoredProcedure: errorType='{0}', spName='{1}'. {2}", ex.ErrorCode, storeProcedureName, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003E58 File Offset: 0x00002058
		[DebuggerStepThrough]
		protected Task<int> _ExecuteStoredProcedureAsync(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<_ExecuteStoredProcedureAsync>d__69 <_ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<_ExecuteStoredProcedureAsync>d__69();
			<_ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<_ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<_ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<_ExecuteStoredProcedureAsync>d__.inParameters = inParameters;
			<_ExecuteStoredProcedureAsync>d__.outParameters = outParameters;
			<_ExecuteStoredProcedureAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<_ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteStoredProcedureAsync>d__69>(ref <_ExecuteStoredProcedureAsync>d__);
			return <_ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003EBC File Offset: 0x000020BC
		public int ExecuteStoredProcedure(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings)
		{
			return DatabaseLayer.Retry<int>(() => this._ExecuteStoredProcedure(storeProcedureName, cmdOverrideSettings));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003EFC File Offset: 0x000020FC
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__71 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__71();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__71>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003F50 File Offset: 0x00002150
		public int ExecuteStoredProcedure(string storeProcedureName)
		{
			return this.ExecuteStoredProcedure(storeProcedureName, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003F6C File Offset: 0x0000216C
		[DebuggerStepThrough]
		public Task<int> ExecuteStoredProcedureAsync(string storeProcedureName)
		{
			DatabaseLayer.<ExecuteStoredProcedureAsync>d__73 <ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureAsync>d__73();
			<ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureAsync>d__73>(ref <ExecuteStoredProcedureAsync>d__);
			return <ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected int _ExecuteStoredProcedure(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings)
		{
			int result;
			try
			{
				int num = 0;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						command.Connection = connection;
						command.CommandText = storeProcedureName;
						command.CommandType = CommandType.StoredProcedure;
						num = command.ExecuteNonQuery();
					}
				}
				result = num;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteStoredProcedure: errorType='{0}', spName='{1}'. {2}", ex.ErrorCode, storeProcedureName, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000407C File Offset: 0x0000227C
		[DebuggerStepThrough]
		protected Task<int> _ExecuteStoredProcedureAsync(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<_ExecuteStoredProcedureAsync>d__75 <_ExecuteStoredProcedureAsync>d__ = new DatabaseLayer.<_ExecuteStoredProcedureAsync>d__75();
			<_ExecuteStoredProcedureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<_ExecuteStoredProcedureAsync>d__.<>4__this = this;
			<_ExecuteStoredProcedureAsync>d__.storeProcedureName = storeProcedureName;
			<_ExecuteStoredProcedureAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteStoredProcedureAsync>d__.<>1__state = -1;
			<_ExecuteStoredProcedureAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteStoredProcedureAsync>d__75>(ref <_ExecuteStoredProcedureAsync>d__);
			return <_ExecuteStoredProcedureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000040D0 File Offset: 0x000022D0
		public IDataReader ExecuteStoredProcedureReader(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			return DatabaseLayer.Retry<IDataReader>(() => this._ExecuteStoredProcedureReader(storeProcedureName, inParameters, outParameters, cmdOverrideSettings));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004120 File Offset: 0x00002320
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteStoredProcedureReaderAsync(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__77 <ExecuteStoredProcedureReaderAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__77();
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteStoredProcedureReaderAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureReaderAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureReaderAsync>d__.inParameters = inParameters;
			<ExecuteStoredProcedureReaderAsync>d__.outParameters = outParameters;
			<ExecuteStoredProcedureReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteStoredProcedureReaderAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__77>(ref <ExecuteStoredProcedureReaderAsync>d__);
			return <ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004184 File Offset: 0x00002384
		public IDataReader ExecuteStoredProcedureReader(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters)
		{
			return this.ExecuteStoredProcedureReader(storeProcedureName, inParameters, outParameters, null);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000041A0 File Offset: 0x000023A0
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteStoredProcedureReaderAsync(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters)
		{
			DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__79 <ExecuteStoredProcedureReaderAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__79();
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteStoredProcedureReaderAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureReaderAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureReaderAsync>d__.inParameters = inParameters;
			<ExecuteStoredProcedureReaderAsync>d__.outParameters = outParameters;
			<ExecuteStoredProcedureReaderAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__79>(ref <ExecuteStoredProcedureReaderAsync>d__);
			return <ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000041FC File Offset: 0x000023FC
		public IDataReader ExecuteStoredProcedureReader(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			return DatabaseLayer.Retry<IDataReader>(() => this._ExecuteStoredProcedureReader(storeProcedureName, cmdOverrideSettings, parameters));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004244 File Offset: 0x00002444
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteStoredProcedureReaderAsync(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__81 <ExecuteStoredProcedureReaderAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__81();
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteStoredProcedureReaderAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureReaderAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteStoredProcedureReaderAsync>d__.parameters = parameters;
			<ExecuteStoredProcedureReaderAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__81>(ref <ExecuteStoredProcedureReaderAsync>d__);
			return <ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000042A0 File Offset: 0x000024A0
		public IDataReader ExecuteStoredProcedureReader(string storeProcedureName, params DbParameter[] parameters)
		{
			return this.ExecuteStoredProcedureReader(storeProcedureName, parameters, null);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000042BC File Offset: 0x000024BC
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteStoredProcedureReaderAsync(string storeProcedureName, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__83 <ExecuteStoredProcedureReaderAsync>d__ = new DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__83();
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteStoredProcedureReaderAsync>d__.<>4__this = this;
			<ExecuteStoredProcedureReaderAsync>d__.storeProcedureName = storeProcedureName;
			<ExecuteStoredProcedureReaderAsync>d__.parameters = parameters;
			<ExecuteStoredProcedureReaderAsync>d__.<>1__state = -1;
			<ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteStoredProcedureReaderAsync>d__83>(ref <ExecuteStoredProcedureReaderAsync>d__);
			return <ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004310 File Offset: 0x00002510
		protected IDataReader _ExecuteStoredProcedureReader(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			IDataReader result;
			try
			{
				IDataReader dataReader = null;
				DbConnection connection = this.Connection;
				connection.Open();
				using (DbCommand command = this.GetCommand(cmdOverrideSettings))
				{
					try
					{
						command.Connection = connection;
						bool flag = inParameters != null && inParameters.Length != 0;
						if (flag)
						{
							command.Parameters.AddRange(inParameters);
						}
						bool flag2 = outParameters != null && outParameters.Length != 0;
						if (flag2)
						{
							command.Parameters.AddRange(outParameters);
						}
						command.CommandText = storeProcedureName;
						command.CommandType = CommandType.StoredProcedure;
						dataReader = command.ExecuteReader((cmdOverrideSettings != null) ? cmdOverrideSettings.CmdBehavior : CommandBehavior.CloseConnection);
					}
					finally
					{
						bool flag3 = command != null;
						if (flag3)
						{
							command.Parameters.Clear();
						}
					}
				}
				result = dataReader;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteStoredProcedureReader: errorType='{0}', spName='{1}'. {2}", ex.ErrorCode, storeProcedureName, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004430 File Offset: 0x00002630
		[DebuggerStepThrough]
		protected Task<DbDataReader> _ExecuteStoredProcedureReaderAsync(string storeProcedureName, DbParameter[] inParameters, DbParameter[] outParameters, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<_ExecuteStoredProcedureReaderAsync>d__85 <_ExecuteStoredProcedureReaderAsync>d__ = new DatabaseLayer.<_ExecuteStoredProcedureReaderAsync>d__85();
			<_ExecuteStoredProcedureReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<_ExecuteStoredProcedureReaderAsync>d__.<>4__this = this;
			<_ExecuteStoredProcedureReaderAsync>d__.storeProcedureName = storeProcedureName;
			<_ExecuteStoredProcedureReaderAsync>d__.inParameters = inParameters;
			<_ExecuteStoredProcedureReaderAsync>d__.outParameters = outParameters;
			<_ExecuteStoredProcedureReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteStoredProcedureReaderAsync>d__.<>1__state = -1;
			<_ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteStoredProcedureReaderAsync>d__85>(ref <_ExecuteStoredProcedureReaderAsync>d__);
			return <_ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004494 File Offset: 0x00002694
		protected IDataReader _ExecuteStoredProcedureReader(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			IDataReader result;
			try
			{
				IDataReader dataReader = null;
				DbConnection connection = this.Connection;
				connection.Open();
				using (DbCommand command = this.GetCommand(cmdOverrideSettings))
				{
					try
					{
						command.Connection = connection;
						bool flag = parameters != null && parameters.Length != 0;
						if (flag)
						{
							command.Parameters.AddRange(parameters);
						}
						command.CommandText = storeProcedureName;
						command.CommandType = CommandType.StoredProcedure;
						dataReader = command.ExecuteReader((cmdOverrideSettings != null) ? cmdOverrideSettings.CmdBehavior : CommandBehavior.CloseConnection);
					}
					finally
					{
						bool flag2 = command != null;
						if (flag2)
						{
							command.Parameters.Clear();
						}
					}
				}
				result = dataReader;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteStoredProcedureReader: errorType='{0}', spName='{1}'. {2}", ex.ErrorCode, storeProcedureName, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004590 File Offset: 0x00002790
		[DebuggerStepThrough]
		protected Task<DbDataReader> _ExecuteStoredProcedureReaderAsync(string storeProcedureName, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<_ExecuteStoredProcedureReaderAsync>d__87 <_ExecuteStoredProcedureReaderAsync>d__ = new DatabaseLayer.<_ExecuteStoredProcedureReaderAsync>d__87();
			<_ExecuteStoredProcedureReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<_ExecuteStoredProcedureReaderAsync>d__.<>4__this = this;
			<_ExecuteStoredProcedureReaderAsync>d__.storeProcedureName = storeProcedureName;
			<_ExecuteStoredProcedureReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteStoredProcedureReaderAsync>d__.parameters = parameters;
			<_ExecuteStoredProcedureReaderAsync>d__.<>1__state = -1;
			<_ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteStoredProcedureReaderAsync>d__87>(ref <_ExecuteStoredProcedureReaderAsync>d__);
			return <_ExecuteStoredProcedureReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000045EC File Offset: 0x000027EC
		public int ExecuteNonQuery(string query, string[] parametersName, object[] parametersValue, CommandOverrideSettings cmdOverrideSettings)
		{
			bool flag = parametersName.Length == 0 || parametersValue.Length == 0 || parametersValue.Length != parametersName.Length;
			int result;
			if (flag)
			{
				result = this.ExecuteNonQuery(query, cmdOverrideSettings);
			}
			else
			{
				DbParameter[] array = new DbParameter[parametersName.Length];
				for (int i = 0; i < parametersName.Length; i++)
				{
					DbParameter parameter = this.Parameter;
					parameter.ParameterName = parametersName[i];
					parameter.Value = parametersValue[i];
					array[i] = parameter;
				}
				result = this.ExecuteNonQuery(query, cmdOverrideSettings, array);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004670 File Offset: 0x00002870
		[DebuggerStepThrough]
		public Task<int> ExecuteNonQueryAsync(string query, string[] parametersName, object[] parametersValue, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteNonQueryAsync>d__89 <ExecuteNonQueryAsync>d__ = new DatabaseLayer.<ExecuteNonQueryAsync>d__89();
			<ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteNonQueryAsync>d__.<>4__this = this;
			<ExecuteNonQueryAsync>d__.query = query;
			<ExecuteNonQueryAsync>d__.parametersName = parametersName;
			<ExecuteNonQueryAsync>d__.parametersValue = parametersValue;
			<ExecuteNonQueryAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteNonQueryAsync>d__.<>1__state = -1;
			<ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteNonQueryAsync>d__89>(ref <ExecuteNonQueryAsync>d__);
			return <ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000046D4 File Offset: 0x000028D4
		public int ExecuteNonQuery(string query, string[] parametersName, object[] parametersValue)
		{
			return this.ExecuteNonQuery(query, parametersName, parametersValue, null);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000046F0 File Offset: 0x000028F0
		[DebuggerStepThrough]
		public Task<int> ExecuteNonQueryAsync(string query, string[] parametersName, object[] parametersValue)
		{
			DatabaseLayer.<ExecuteNonQueryAsync>d__91 <ExecuteNonQueryAsync>d__ = new DatabaseLayer.<ExecuteNonQueryAsync>d__91();
			<ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteNonQueryAsync>d__.<>4__this = this;
			<ExecuteNonQueryAsync>d__.query = query;
			<ExecuteNonQueryAsync>d__.parametersName = parametersName;
			<ExecuteNonQueryAsync>d__.parametersValue = parametersValue;
			<ExecuteNonQueryAsync>d__.<>1__state = -1;
			<ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteNonQueryAsync>d__91>(ref <ExecuteNonQueryAsync>d__);
			return <ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000474C File Offset: 0x0000294C
		public int ExecuteNonQuery(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			return DatabaseLayer.Retry<int>(() => this._ExecuteNonQuery(query, cmdOverrideSettings, parameters));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004794 File Offset: 0x00002994
		[DebuggerStepThrough]
		public Task<int> ExecuteNonQueryAsync(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteNonQueryAsync>d__93 <ExecuteNonQueryAsync>d__ = new DatabaseLayer.<ExecuteNonQueryAsync>d__93();
			<ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteNonQueryAsync>d__.<>4__this = this;
			<ExecuteNonQueryAsync>d__.query = query;
			<ExecuteNonQueryAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteNonQueryAsync>d__.parameters = parameters;
			<ExecuteNonQueryAsync>d__.<>1__state = -1;
			<ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteNonQueryAsync>d__93>(ref <ExecuteNonQueryAsync>d__);
			return <ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000047F0 File Offset: 0x000029F0
		public int ExecuteNonQuery(string query, params DbParameter[] parameters)
		{
			return this.ExecuteNonQuery(query, null, parameters);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000480C File Offset: 0x00002A0C
		[DebuggerStepThrough]
		public Task<int> ExecuteNonQueryAsync(string query, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteNonQueryAsync>d__95 <ExecuteNonQueryAsync>d__ = new DatabaseLayer.<ExecuteNonQueryAsync>d__95();
			<ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteNonQueryAsync>d__.<>4__this = this;
			<ExecuteNonQueryAsync>d__.query = query;
			<ExecuteNonQueryAsync>d__.parameters = parameters;
			<ExecuteNonQueryAsync>d__.<>1__state = -1;
			<ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteNonQueryAsync>d__95>(ref <ExecuteNonQueryAsync>d__);
			return <ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004860 File Offset: 0x00002A60
		protected int _ExecuteNonQuery(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			int result;
			try
			{
				int num = 0;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						try
						{
							command.Connection = connection;
							bool flag = parameters != null && parameters.Length != 0;
							if (flag)
							{
								command.Parameters.AddRange(parameters);
							}
							command.CommandText = query;
							command.CommandType = CommandType.Text;
							num = command.ExecuteNonQuery();
						}
						finally
						{
							bool flag2 = command != null;
							if (flag2)
							{
								command.Parameters.Clear();
							}
						}
					}
				}
				result = num;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteNonQuery: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004968 File Offset: 0x00002B68
		[DebuggerStepThrough]
		protected Task<int> _ExecuteNonQueryAsync(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<_ExecuteNonQueryAsync>d__97 <_ExecuteNonQueryAsync>d__ = new DatabaseLayer.<_ExecuteNonQueryAsync>d__97();
			<_ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<_ExecuteNonQueryAsync>d__.<>4__this = this;
			<_ExecuteNonQueryAsync>d__.query = query;
			<_ExecuteNonQueryAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteNonQueryAsync>d__.parameters = parameters;
			<_ExecuteNonQueryAsync>d__.<>1__state = -1;
			<_ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteNonQueryAsync>d__97>(ref <_ExecuteNonQueryAsync>d__);
			return <_ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000049C4 File Offset: 0x00002BC4
		public int ExecuteNonQuery(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			return DatabaseLayer.Retry<int>(() => this._ExecuteNonQuery(query, cmdOverrideSettings));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004A04 File Offset: 0x00002C04
		[DebuggerStepThrough]
		public Task<int> ExecuteNonQueryAsync(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteNonQueryAsync>d__99 <ExecuteNonQueryAsync>d__ = new DatabaseLayer.<ExecuteNonQueryAsync>d__99();
			<ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteNonQueryAsync>d__.<>4__this = this;
			<ExecuteNonQueryAsync>d__.query = query;
			<ExecuteNonQueryAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteNonQueryAsync>d__.<>1__state = -1;
			<ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteNonQueryAsync>d__99>(ref <ExecuteNonQueryAsync>d__);
			return <ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004A58 File Offset: 0x00002C58
		public int ExecuteNonQuery(string query)
		{
			return this.ExecuteNonQuery(query, null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004A74 File Offset: 0x00002C74
		[DebuggerStepThrough]
		public Task<int> ExecuteNonQueryAsync(string query)
		{
			DatabaseLayer.<ExecuteNonQueryAsync>d__101 <ExecuteNonQueryAsync>d__ = new DatabaseLayer.<ExecuteNonQueryAsync>d__101();
			<ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ExecuteNonQueryAsync>d__.<>4__this = this;
			<ExecuteNonQueryAsync>d__.query = query;
			<ExecuteNonQueryAsync>d__.<>1__state = -1;
			<ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteNonQueryAsync>d__101>(ref <ExecuteNonQueryAsync>d__);
			return <ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004AC0 File Offset: 0x00002CC0
		protected int _ExecuteNonQuery(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			int result;
			try
			{
				int num = 0;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						command.Connection = connection;
						command.CommandText = query;
						command.CommandType = CommandType.Text;
						num = command.ExecuteNonQuery();
					}
				}
				result = num;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteNonQuery: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004B84 File Offset: 0x00002D84
		[DebuggerStepThrough]
		protected Task<int> _ExecuteNonQueryAsync(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<_ExecuteNonQueryAsync>d__103 <_ExecuteNonQueryAsync>d__ = new DatabaseLayer.<_ExecuteNonQueryAsync>d__103();
			<_ExecuteNonQueryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<_ExecuteNonQueryAsync>d__.<>4__this = this;
			<_ExecuteNonQueryAsync>d__.query = query;
			<_ExecuteNonQueryAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteNonQueryAsync>d__.<>1__state = -1;
			<_ExecuteNonQueryAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteNonQueryAsync>d__103>(ref <_ExecuteNonQueryAsync>d__);
			return <_ExecuteNonQueryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public int ExecuteNonQueryTransaction(string query, DbTransaction transaction, CommandOverrideSettings cmdOverrideSettings)
		{
			int result = 0;
			using (DbCommand command = this.GetCommand(cmdOverrideSettings))
			{
				try
				{
					command.Connection = transaction.Connection;
					command.Transaction = transaction;
					command.CommandText = query;
					command.CommandType = CommandType.Text;
					result = command.ExecuteNonQuery();
				}
				catch (DbException ex)
				{
					CWLogger.Logger.ErrorException(string.Format("Execute non query transaction:{0}: errorType={2}, query='{1}'", this.DatabaseRole, query, ex.ErrorCode), ex);
					this.RollbackDbTransaction(transaction);
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004C8C File Offset: 0x00002E8C
		public int ExecuteNonQueryTransaction(string query, DbTransaction transaction)
		{
			return this.ExecuteNonQueryTransaction(query, transaction, null);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public int ExecuteNonQueryTransaction(string query, DbTransaction transaction, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			int result = 0;
			using (DbCommand command = this.GetCommand(cmdOverrideSettings))
			{
				try
				{
					command.Connection = transaction.Connection;
					command.Transaction = transaction;
					bool flag = parameters != null && parameters.Length != 0;
					if (flag)
					{
						command.Parameters.AddRange(parameters);
					}
					command.CommandText = query;
					command.CommandType = CommandType.Text;
					result = command.ExecuteNonQuery();
				}
				catch (DbException ex)
				{
					CWLogger.Logger.ErrorException(string.Format("Execute non query transaction:{0}, errorType={1}, query='{2}' ", this.DatabaseRole, ex.ErrorCode, query), ex);
					this.RollbackDbTransaction(transaction);
					throw;
				}
				finally
				{
					command.Parameters.Clear();
				}
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004D98 File Offset: 0x00002F98
		public int ExecuteNonQueryTransaction(string query, DbTransaction transaction, params DbParameter[] parameters)
		{
			return this.ExecuteNonQueryTransaction(query, transaction, null, parameters);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public DataTable ExecuteQuery(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			return DatabaseLayer.Retry<DataTable>(() => this._ExecuteQuery(query, cmdOverrideSettings, parameters));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004DFC File Offset: 0x00002FFC
		public DataTable ExecuteQuery(string query, params DbParameter[] parameters)
		{
			return this.ExecuteQuery(query, null, parameters);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004E18 File Offset: 0x00003018
		protected DataTable _ExecuteQuery(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DataTable result;
			try
			{
				DataTable dataTable = null;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						try
						{
							command.Connection = connection;
							bool flag = parameters != null && parameters.Length != 0;
							if (flag)
							{
								command.Parameters.AddRange(parameters);
							}
							command.CommandText = query;
							command.CommandType = CommandType.Text;
							using (DbDataReader dbDataReader = command.ExecuteReader((cmdOverrideSettings != null) ? cmdOverrideSettings.CmdBehavior : CommandBehavior.CloseConnection))
							{
								dataTable = new DataTable();
								dataTable.Load(dbDataReader);
							}
						}
						finally
						{
							bool flag2 = command != null;
							if (flag2)
							{
								command.Parameters.Clear();
							}
						}
					}
				}
				result = dataTable;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteQuery: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004F5C File Offset: 0x0000315C
		public DataTable ExecuteQuery(string query, string[] parametersName, object[] parametersValue, CommandOverrideSettings cmdOverrideSettings)
		{
			bool flag = parametersName.Length == 0 || parametersValue.Length == 0 || parametersValue.Length != parametersName.Length;
			DataTable result;
			if (flag)
			{
				result = this.ExecuteQuery(query, cmdOverrideSettings);
			}
			else
			{
				DbParameter[] array = new DbParameter[parametersName.Length];
				for (int i = 0; i < parametersName.Length; i++)
				{
					DbParameter parameter = this.Parameter;
					parameter.ParameterName = parametersName[i];
					parameter.Value = parametersValue[i];
					array[i] = parameter;
				}
				result = this.ExecuteQuery(query, cmdOverrideSettings, array);
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004FE0 File Offset: 0x000031E0
		public DataTable ExecuteQuery(string query, string[] parametersName, object[] parametersValue)
		{
			return this.ExecuteQuery(query, parametersName, parametersValue, null);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004FFC File Offset: 0x000031FC
		public DataTable ExecuteQuery(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			return DatabaseLayer.Retry<DataTable>(() => this._ExecuteQuery(query, cmdOverrideSettings));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000503C File Offset: 0x0000323C
		public DataTable ExecuteQuery(string query)
		{
			return this.ExecuteQuery(query, null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005058 File Offset: 0x00003258
		protected DataTable _ExecuteQuery(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			DataTable result;
			try
			{
				DataTable dataTable = null;
				using (DbConnection connection = this.Connection)
				{
					connection.Open();
					using (DbCommand command = this.GetCommand(cmdOverrideSettings))
					{
						command.Connection = connection;
						command.CommandText = query;
						command.CommandType = CommandType.Text;
						using (DbDataReader dbDataReader = command.ExecuteReader((cmdOverrideSettings != null) ? cmdOverrideSettings.CmdBehavior : CommandBehavior.CloseConnection))
						{
							dataTable = new DataTable();
							dataTable.Load(dbDataReader);
						}
					}
					connection.Close();
				}
				result = dataTable;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteQuery: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005158 File Offset: 0x00003358
		public IDataReader ExecuteQueryReader(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			return DatabaseLayer.Retry<IDataReader>(() => this._ExecuteQueryReader(query, cmdOverrideSettings, parameters));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000051A0 File Offset: 0x000033A0
		public IDataReader ExecuteQueryReaderTransaction(string query, DbTransaction transaction, params DbParameter[] parameters)
		{
			return this.ExecuteQueryReaderTransaction(query, transaction, null, parameters);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000051BC File Offset: 0x000033BC
		public IDataReader ExecuteQueryReaderTransaction(string query, DbTransaction transaction, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			IDataReader result;
			try
			{
				IDataReader dataReader = null;
				using (DbCommand command = this.GetCommand(cmdOverrideSettings))
				{
					try
					{
						command.Connection = transaction.Connection;
						command.Transaction = transaction;
						bool flag = parameters != null && parameters.Length != 0;
						if (flag)
						{
							command.Parameters.AddRange(parameters);
						}
						command.CommandText = query;
						command.CommandType = CommandType.Text;
						dataReader = command.ExecuteReader();
					}
					finally
					{
						if (command != null)
						{
							command.Parameters.Clear();
						}
					}
				}
				result = dataReader;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: ExecuteQueryReaderTransaction: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				this.RollbackDbTransaction(transaction);
				throw;
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000052AC File Offset: 0x000034AC
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteQueryReaderAsync(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteQueryReaderAsync>d__119 <ExecuteQueryReaderAsync>d__ = new DatabaseLayer.<ExecuteQueryReaderAsync>d__119();
			<ExecuteQueryReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteQueryReaderAsync>d__.<>4__this = this;
			<ExecuteQueryReaderAsync>d__.query = query;
			<ExecuteQueryReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteQueryReaderAsync>d__.parameters = parameters;
			<ExecuteQueryReaderAsync>d__.<>1__state = -1;
			<ExecuteQueryReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteQueryReaderAsync>d__119>(ref <ExecuteQueryReaderAsync>d__);
			return <ExecuteQueryReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005308 File Offset: 0x00003508
		public IDataReader ExecuteQueryReader(string query, params DbParameter[] parameters)
		{
			return this.ExecuteQueryReader(query, null, parameters);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005324 File Offset: 0x00003524
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteQueryReaderAsync(string query, params DbParameter[] parameters)
		{
			DatabaseLayer.<ExecuteQueryReaderAsync>d__121 <ExecuteQueryReaderAsync>d__ = new DatabaseLayer.<ExecuteQueryReaderAsync>d__121();
			<ExecuteQueryReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteQueryReaderAsync>d__.<>4__this = this;
			<ExecuteQueryReaderAsync>d__.query = query;
			<ExecuteQueryReaderAsync>d__.parameters = parameters;
			<ExecuteQueryReaderAsync>d__.<>1__state = -1;
			<ExecuteQueryReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteQueryReaderAsync>d__121>(ref <ExecuteQueryReaderAsync>d__);
			return <ExecuteQueryReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005378 File Offset: 0x00003578
		protected IDataReader _ExecuteQueryReader(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			IDataReader result;
			try
			{
				IDataReader dataReader = null;
				DbConnection connection = this.Connection;
				connection.Open();
				using (DbCommand command = this.GetCommand(cmdOverrideSettings))
				{
					try
					{
						command.Connection = connection;
						bool flag = parameters != null && parameters.Length != 0;
						if (flag)
						{
							command.Parameters.AddRange(parameters);
						}
						command.CommandText = query;
						command.CommandType = CommandType.Text;
						dataReader = command.ExecuteReader((cmdOverrideSettings != null) ? cmdOverrideSettings.CmdBehavior : CommandBehavior.CloseConnection);
					}
					finally
					{
						bool flag2 = command != null;
						if (flag2)
						{
							command.Parameters.Clear();
						}
					}
				}
				result = dataReader;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteQueryReader: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005474 File Offset: 0x00003674
		[DebuggerStepThrough]
		protected Task<DbDataReader> _ExecuteQueryReaderAsync(string query, CommandOverrideSettings cmdOverrideSettings, params DbParameter[] parameters)
		{
			DatabaseLayer.<_ExecuteQueryReaderAsync>d__123 <_ExecuteQueryReaderAsync>d__ = new DatabaseLayer.<_ExecuteQueryReaderAsync>d__123();
			<_ExecuteQueryReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<_ExecuteQueryReaderAsync>d__.<>4__this = this;
			<_ExecuteQueryReaderAsync>d__.query = query;
			<_ExecuteQueryReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteQueryReaderAsync>d__.parameters = parameters;
			<_ExecuteQueryReaderAsync>d__.<>1__state = -1;
			<_ExecuteQueryReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteQueryReaderAsync>d__123>(ref <_ExecuteQueryReaderAsync>d__);
			return <_ExecuteQueryReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000054D0 File Offset: 0x000036D0
		public IDataReader ExecuteQueryReader(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			return DatabaseLayer.Retry<IDataReader>(() => this._ExecuteQueryReader(query, cmdOverrideSettings));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005510 File Offset: 0x00003710
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteQueryReaderAsync(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<ExecuteQueryReaderAsync>d__125 <ExecuteQueryReaderAsync>d__ = new DatabaseLayer.<ExecuteQueryReaderAsync>d__125();
			<ExecuteQueryReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteQueryReaderAsync>d__.<>4__this = this;
			<ExecuteQueryReaderAsync>d__.query = query;
			<ExecuteQueryReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<ExecuteQueryReaderAsync>d__.<>1__state = -1;
			<ExecuteQueryReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteQueryReaderAsync>d__125>(ref <ExecuteQueryReaderAsync>d__);
			return <ExecuteQueryReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005564 File Offset: 0x00003764
		public IDataReader ExecuteQueryReader(string query)
		{
			return this.ExecuteQueryReader(query, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005580 File Offset: 0x00003780
		[DebuggerStepThrough]
		public Task<DbDataReader> ExecuteQueryReaderAsync(string query)
		{
			DatabaseLayer.<ExecuteQueryReaderAsync>d__127 <ExecuteQueryReaderAsync>d__ = new DatabaseLayer.<ExecuteQueryReaderAsync>d__127();
			<ExecuteQueryReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<ExecuteQueryReaderAsync>d__.<>4__this = this;
			<ExecuteQueryReaderAsync>d__.query = query;
			<ExecuteQueryReaderAsync>d__.<>1__state = -1;
			<ExecuteQueryReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<ExecuteQueryReaderAsync>d__127>(ref <ExecuteQueryReaderAsync>d__);
			return <ExecuteQueryReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000055CC File Offset: 0x000037CC
		protected IDataReader _ExecuteQueryReader(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			IDataReader result;
			try
			{
				IDataReader dataReader = null;
				DbConnection connection = this.Connection;
				connection.Open();
				using (DbCommand command = this.GetCommand(cmdOverrideSettings))
				{
					command.Connection = connection;
					command.CommandText = query;
					command.CommandType = CommandType.Text;
					dataReader = command.ExecuteReader((cmdOverrideSettings != null) ? cmdOverrideSettings.CmdBehavior : CommandBehavior.CloseConnection);
				}
				result = dataReader;
			}
			catch (DbException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("DatabaseLayer:: _ExecuteQueryReader: errorType='{0}', query='{1}'. {2}", ex.ErrorCode, query, ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005684 File Offset: 0x00003884
		[DebuggerStepThrough]
		protected Task<DbDataReader> _ExecuteQueryReaderAsync(string query, CommandOverrideSettings cmdOverrideSettings)
		{
			DatabaseLayer.<_ExecuteQueryReaderAsync>d__129 <_ExecuteQueryReaderAsync>d__ = new DatabaseLayer.<_ExecuteQueryReaderAsync>d__129();
			<_ExecuteQueryReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
			<_ExecuteQueryReaderAsync>d__.<>4__this = this;
			<_ExecuteQueryReaderAsync>d__.query = query;
			<_ExecuteQueryReaderAsync>d__.cmdOverrideSettings = cmdOverrideSettings;
			<_ExecuteQueryReaderAsync>d__.<>1__state = -1;
			<_ExecuteQueryReaderAsync>d__.<>t__builder.Start<DatabaseLayer.<_ExecuteQueryReaderAsync>d__129>(ref <_ExecuteQueryReaderAsync>d__);
			return <_ExecuteQueryReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000056D8 File Offset: 0x000038D8
		private static T Retry<T>(Func<T> func)
		{
			int num = 3;
			T result;
			for (;;)
			{
				try
				{
					result = func();
					break;
				}
				catch (SqlException ex)
				{
					bool flag = ex.Number != 1205 && ex.Number != -2;
					if (flag)
					{
						throw;
					}
					Thread.Sleep(5);
					num--;
					bool flag2 = num <= 0;
					if (flag2)
					{
						throw;
					}
				}
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005750 File Offset: 0x00003950
		[DebuggerStepThrough]
		private static Task<T> RetryAsync<T>(Func<Task<T>> func)
		{
			DatabaseLayer.<RetryAsync>d__131<T> <RetryAsync>d__ = new DatabaseLayer.<RetryAsync>d__131<T>();
			<RetryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<RetryAsync>d__.func = func;
			<RetryAsync>d__.<>1__state = -1;
			<RetryAsync>d__.<>t__builder.Start<DatabaseLayer.<RetryAsync>d__131<T>>(ref <RetryAsync>d__);
			return <RetryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005794 File Offset: 0x00003994
		private static void Retry(Action action)
		{
			int num = 3;
			for (;;)
			{
				try
				{
					action();
				}
				catch (SqlException ex)
				{
					bool flag = ex.Number != 1205 && ex.Number != -2;
					if (flag)
					{
						throw;
					}
					Thread.Sleep(5);
					num--;
					bool flag2 = num <= 0;
					if (flag2)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005808 File Offset: 0x00003A08
		private void setupDbEncryption()
		{
			try
			{
				string[] array = new string[]
				{
					string.Format("{0}_dbpwd", this.DatabaseRole),
					"pwd",
					"dbpwd"
				};
				int num = 0;
				string appSettingsByNameUsingProtection;
				do
				{
					appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection(array[num++]);
				}
				while (string.IsNullOrEmpty(appSettingsByNameUsingProtection) && num < array.Length);
				bool flag = string.IsNullOrEmpty(appSettingsByNameUsingProtection) && this.DatabaseRole > eDatabaseConnectionStringName.ClockWork;
				if (flag)
				{
					DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
					this.Encryption = clockWork.Encryption;
					this.EncryptionPassword = clockWork.EncryptionPassword;
				}
				else
				{
					bool flag2 = !string.IsNullOrEmpty(appSettingsByNameUsingProtection);
					if (flag2)
					{
						this.Encryption = EncryptionFactory.GetEncryption(this.GetDbEncryptionType(), appSettingsByNameUsingProtection);
						this.EncryptionPassword = appSettingsByNameUsingProtection;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000058F4 File Offset: 0x00003AF4
		private EncryptionType GetDbEncryptionType()
		{
			EncryptionType result;
			try
			{
				bool flag = this.DatabaseRole > eDatabaseConnectionStringName.ClockWork;
				if (flag)
				{
					IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
					result = ((encryption != null) ? encryption.Name : EncryptionType.TripleDES_192bit);
				}
				else
				{
					object obj = this.ExecuteScalar("select safevalue from miscsafe where safekey='encryptiontype'");
					EncryptionType encryptionType;
					bool flag2 = !(obj is DBNull) && obj != null && Enum.TryParse<EncryptionType>((string)obj, out encryptionType);
					if (flag2)
					{
						result = encryptionType;
					}
					else
					{
						object obj2 = this.ExecuteScalar("select misccode from misc where misccode=1");
						encryptionType = ((obj2 is DBNull || obj2 == null) ? EncryptionType.TripleDES_192bit : EncryptionType.TripleDES_128bit);
						CWLogger.Logger.Debug("DatabaseLayer:{0}:GetDbEncryptionType:: {1}", this.DatabaseRole, Enum.GetName(typeof(EncryptionType), encryptionType));
						result = encryptionType;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.DebugException(string.Format("DatabaseLayer:{0}:GetDbEncryptionType:: {1}", this.DatabaseRole, ex.ToString()), ex);
				result = EncryptionType.TripleDES_192bit;
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000059F8 File Offset: 0x00003BF8
		private void setupConnectionString()
		{
			try
			{
				string connectionStringByNameUsingProtection = ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection(this.DatabaseRole.ToString());
				bool flag = !string.IsNullOrEmpty(connectionStringByNameUsingProtection);
				if (flag)
				{
					this.ConnectionString = connectionStringByNameUsingProtection;
				}
			}
			catch
			{
			}
			bool flag2 = this.DatabaseRole == eDatabaseConnectionStringName.ClockWork && string.IsNullOrEmpty(this.ConnectionString);
			if (flag2)
			{
				this.ConnectionString = this.GetLegacyClockWorkConnectionString();
			}
			bool flag3 = this.DatabaseRole != eDatabaseConnectionStringName.ClockWork && string.IsNullOrEmpty(this.ConnectionString);
			if (flag3)
			{
				string text = (DatabaseLayerFactory.ClockWork != null && !string.IsNullOrEmpty(DatabaseLayerFactory.ClockWork.ConnectionString)) ? DatabaseLayerFactory.ClockWork.ConnectionString : ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection(eDatabaseConnectionStringName.ClockWork.ToString());
				bool flag4 = string.IsNullOrEmpty(text);
				if (flag4)
				{
					text = this.GetLegacyClockWorkConnectionString();
				}
				bool flag5 = string.IsNullOrEmpty(text);
				if (!flag5)
				{
					bool flag6 = this.DatabaseRole == eDatabaseConnectionStringName.ClockWorkFiles;
					if (flag6)
					{
						DbConnectionStringBuilder connectionStringBuilder = this.ConnectionStringBuilder;
						connectionStringBuilder.ConnectionString = text;
						connectionStringBuilder["Initial Catalog"] = connectionStringBuilder["Initial Catalog"] + eDatabaseConnectionStringName.ClockWorkFiles.GetAttribute<DatabaseSuffixAttribute>().DatabaseNameSuffix;
						this.ConnectionString = connectionStringBuilder.ConnectionString;
					}
					bool flag7 = this.DatabaseRole == eDatabaseConnectionStringName.ClockWorkTracking;
					if (flag7)
					{
						DbConnectionStringBuilder connectionStringBuilder2 = this.ConnectionStringBuilder;
						connectionStringBuilder2.ConnectionString = text;
						connectionStringBuilder2["Initial Catalog"] = connectionStringBuilder2["Initial Catalog"] + eDatabaseConnectionStringName.ClockWorkTracking.GetAttribute<DatabaseSuffixAttribute>().DatabaseNameSuffix;
						this.ConnectionString = connectionStringBuilder2.ConnectionString;
					}
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005BB8 File Offset: 0x00003DB8
		private string GetLegacyClockWorkConnectionString()
		{
			try
			{
				string[] array = new string[]
				{
					"clockwork",
					"ClockWork",
					"Clockwork",
					"CLOCKWORK"
				};
				int num = 0;
				string connectionStringByNameUsingProtection;
				do
				{
					connectionStringByNameUsingProtection = ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection(array[num++]);
				}
				while (string.IsNullOrEmpty(connectionStringByNameUsingProtection) && num < array.Length);
				bool flag = !string.IsNullOrEmpty(connectionStringByNameUsingProtection);
				if (flag)
				{
					return connectionStringByNameUsingProtection;
				}
			}
			catch
			{
				return null;
			}
			return null;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005C4C File Offset: 0x00003E4C
		private DbConnection Connection
		{
			get
			{
				bool flag = this.Factory != null;
				DbConnection result;
				if (flag)
				{
					DbConnection dbConnection = this.Factory.CreateConnection();
					dbConnection.ConnectionString = this._connectionString;
					result = dbConnection;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005C8C File Offset: 0x00003E8C
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					this._connectionString = value;
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005CC8 File Offset: 0x00003EC8
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005CE0 File Offset: 0x00003EE0
		[Obsolete("Use Encryption instead")]
		public IEncryption TripleDES
		{
			get
			{
				return this.Encryption;
			}
			set
			{
				this.Encryption = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005CEB File Offset: 0x00003EEB
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00005CF3 File Offset: 0x00003EF3
		public IEncryption Encryption { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00005CFC File Offset: 0x00003EFC
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00005D04 File Offset: 0x00003F04
		public DbProviderFactory Factory { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00005D10 File Offset: 0x00003F10
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00005D30 File Offset: 0x00003F30
		public string ProviderName
		{
			get
			{
				return this.Factory.ToString();
			}
			set
			{
				try
				{
					this.Factory = this.GetFactory(value);
				}
				catch
				{
					this.Factory = null;
					throw;
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005D6C File Offset: 0x00003F6C
		public DbCommand Command
		{
			get
			{
				return this.Factory.CreateCommand();
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005D7C File Offset: 0x00003F7C
		protected DbCommand GetCommand(CommandOverrideSettings cmdOverrideSettings)
		{
			DbCommand command = this.Command;
			bool flag = cmdOverrideSettings != null;
			if (flag)
			{
				command.CommandTimeout = cmdOverrideSettings.CommandTimeoutInSeconds;
			}
			return command;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005DAD File Offset: 0x00003FAD
		public DbDataAdapter DataAdapter
		{
			get
			{
				return this.Factory.CreateDataAdapter();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00005DBA File Offset: 0x00003FBA
		public DbCommandBuilder CommandBuilder
		{
			get
			{
				return this.Factory.CreateCommandBuilder();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005DC7 File Offset: 0x00003FC7
		public DbParameter Parameter
		{
			get
			{
				return this.Factory.CreateParameter();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005DD4 File Offset: 0x00003FD4
		public DbConnectionStringBuilder ConnectionStringBuilder
		{
			get
			{
				return this.Factory.CreateConnectionStringBuilder();
			}
		}

		// Token: 0x04000003 RID: 3
		private string _connectionString;
	}
}
