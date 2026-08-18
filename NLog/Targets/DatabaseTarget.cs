using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Transactions;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000154 RID: 340
	[Target("Database")]
	public sealed class DatabaseTarget : Target, IInstallable
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x0001C27C File Offset: 0x0001A47C
		public DatabaseTarget()
		{
			this.Parameters = new List<DatabaseParameterInfo>();
			this.InstallDdlCommands = new List<DatabaseCommandInfo>();
			this.UninstallDdlCommands = new List<DatabaseCommandInfo>();
			this.DBProvider = "sqlserver";
			this.DBHost = ".";
			this.ConnectionStringsSettings = System.Configuration.ConfigurationManager.ConnectionStrings;
			this.CommandType = CommandType.Text;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001C2DD File Offset: 0x0001A4DD
		public DatabaseTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0001C2EC File Offset: 0x0001A4EC
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
		[RequiredParameter]
		[DefaultValue("sqlserver")]
		public string DBProvider { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0001C2FD File Offset: 0x0001A4FD
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0001C305 File Offset: 0x0001A505
		public string ConnectionStringName { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0001C30E File Offset: 0x0001A50E
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0001C316 File Offset: 0x0001A516
		public Layout ConnectionString { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0001C31F File Offset: 0x0001A51F
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x0001C327 File Offset: 0x0001A527
		public Layout InstallConnectionString { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0001C330 File Offset: 0x0001A530
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x0001C338 File Offset: 0x0001A538
		[ArrayParameter(typeof(DatabaseCommandInfo), "install-command")]
		public IList<DatabaseCommandInfo> InstallDdlCommands { get; private set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0001C341 File Offset: 0x0001A541
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x0001C349 File Offset: 0x0001A549
		[ArrayParameter(typeof(DatabaseCommandInfo), "uninstall-command")]
		public IList<DatabaseCommandInfo> UninstallDdlCommands { get; private set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0001C352 File Offset: 0x0001A552
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x0001C35A File Offset: 0x0001A55A
		[DefaultValue(false)]
		public bool KeepConnection { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0001C363 File Offset: 0x0001A563
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0001C36B File Offset: 0x0001A56B
		[Obsolete("Obsolete - value will be ignored - logging code always runs outside of transaction. Will be removed in NLog 6.")]
		public bool? UseTransactions { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0001C374 File Offset: 0x0001A574
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x0001C37C File Offset: 0x0001A57C
		public Layout DBHost { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0001C385 File Offset: 0x0001A585
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0001C38D File Offset: 0x0001A58D
		public Layout DBUserName { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0001C396 File Offset: 0x0001A596
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0001C39E File Offset: 0x0001A59E
		public Layout DBPassword { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0001C3A7 File Offset: 0x0001A5A7
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x0001C3AF File Offset: 0x0001A5AF
		public Layout DBDatabase { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		[RequiredParameter]
		public Layout CommandText { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0001C3C9 File Offset: 0x0001A5C9
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x0001C3D1 File Offset: 0x0001A5D1
		[DefaultValue(CommandType.Text)]
		public CommandType CommandType { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0001C3DA File Offset: 0x0001A5DA
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x0001C3E2 File Offset: 0x0001A5E2
		[ArrayParameter(typeof(DatabaseParameterInfo), "parameter")]
		public IList<DatabaseParameterInfo> Parameters { get; private set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0001C3EB File Offset: 0x0001A5EB
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x0001C3F3 File Offset: 0x0001A5F3
		internal DbProviderFactory ProviderFactory { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0001C404 File Offset: 0x0001A604
		internal ConnectionStringSettingsCollection ConnectionStringsSettings { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0001C40D File Offset: 0x0001A60D
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x0001C415 File Offset: 0x0001A615
		internal Type ConnectionType { get; set; }

		// Token: 0x06000C58 RID: 3160 RVA: 0x0001C41E File Offset: 0x0001A61E
		public void Install(InstallationContext installationContext)
		{
			this.RunInstallCommands(installationContext, this.InstallDdlCommands);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0001C42D File Offset: 0x0001A62D
		public void Uninstall(InstallationContext installationContext)
		{
			this.RunInstallCommands(installationContext, this.UninstallDdlCommands);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0001C43C File Offset: 0x0001A63C
		public bool? IsInstalled(InstallationContext installationContext)
		{
			return null;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0001C454 File Offset: 0x0001A654
		internal IDbConnection OpenConnection(string connectionString)
		{
			IDbConnection dbConnection;
			if (this.ProviderFactory != null)
			{
				dbConnection = this.ProviderFactory.CreateConnection();
			}
			else
			{
				dbConnection = (IDbConnection)Activator.CreateInstance(this.ConnectionType);
			}
			dbConnection.ConnectionString = connectionString;
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0001C498 File Offset: 0x0001A698
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (this.UseTransactions != null)
			{
				InternalLogger.Warn("UseTransactions is obsolete and will not be used - will be removed in NLog 6");
			}
			bool flag = false;
			if (!string.IsNullOrEmpty(this.ConnectionStringName))
			{
				ConnectionStringSettings connectionStringSettings = this.ConnectionStringsSettings[this.ConnectionStringName];
				if (connectionStringSettings == null)
				{
					throw new NLogConfigurationException("Connection string '" + this.ConnectionStringName + "' is not declared in <connectionStrings /> section.");
				}
				this.ConnectionString = SimpleLayout.Escape(connectionStringSettings.ConnectionString);
				if (!string.IsNullOrEmpty(connectionStringSettings.ProviderName))
				{
					this.ProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
					flag = true;
				}
			}
			if (!flag)
			{
				foreach (object obj in DbProviderFactories.GetFactoryClasses().Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string a = (string)dataRow["InvariantName"];
					if (a == this.DBProvider)
					{
						this.ProviderFactory = DbProviderFactories.GetFactory(this.DBProvider);
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				string key;
				switch (key = this.DBProvider.ToUpper(CultureInfo.InvariantCulture))
				{
				case "SQLSERVER":
				case "MSSQL":
				case "MICROSOFT":
				case "MSDE":
					this.ConnectionType = DatabaseTarget.systemDataAssembly.GetType("System.Data.SqlClient.SqlConnection", true);
					return;
				case "OLEDB":
					this.ConnectionType = DatabaseTarget.systemDataAssembly.GetType("System.Data.OleDb.OleDbConnection", true);
					return;
				case "ODBC":
					this.ConnectionType = DatabaseTarget.systemDataAssembly.GetType("System.Data.Odbc.OdbcConnection", true);
					return;
				}
				this.ConnectionType = Type.GetType(this.DBProvider, true);
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		protected override void CloseTarget()
		{
			base.CloseTarget();
			InternalLogger.Trace("DatabaseTarget: close connection because of CloseTarget");
			this.CloseConnection();
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		protected override void Write(LogEventInfo logEvent)
		{
			try
			{
				this.WriteEventToDatabase(logEvent);
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error when writing to database.");
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Trace("DatabaseTarget: close connection because of error");
				this.CloseConnection();
				throw;
			}
			finally
			{
				if (!this.KeepConnection)
				{
					InternalLogger.Trace("DatabaseTarget: close connection (KeepConnection = false).");
					this.CloseConnection();
				}
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0001C768 File Offset: 0x0001A968
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			Dictionary<string, List<AsyncLogEventInfo>> dictionary = logEvents.BucketSort((AsyncLogEventInfo c) => this.BuildConnectionString(c.LogEvent));
			try
			{
				foreach (KeyValuePair<string, List<AsyncLogEventInfo>> keyValuePair in dictionary)
				{
					foreach (AsyncLogEventInfo asyncLogEventInfo in keyValuePair.Value)
					{
						try
						{
							this.WriteEventToDatabase(asyncLogEventInfo.LogEvent);
							asyncLogEventInfo.Continuation(null);
						}
						catch (Exception ex)
						{
							InternalLogger.Error(ex, "Error when writing to database.");
							if (ex.MustBeRethrownImmediately())
							{
								throw;
							}
							InternalLogger.Trace("DatabaseTarget: close connection because of exception");
							this.CloseConnection();
							asyncLogEventInfo.Continuation(ex);
							if (ex.MustBeRethrown())
							{
								throw;
							}
						}
					}
				}
			}
			finally
			{
				if (!this.KeepConnection)
				{
					InternalLogger.Trace("DatabaseTarget: close connection because of KeepConnection=false");
					this.CloseConnection();
				}
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0001C894 File Offset: 0x0001AA94
		private void WriteEventToDatabase(LogEventInfo logEvent)
		{
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
			{
				this.EnsureConnectionOpen(this.BuildConnectionString(logEvent));
				IDbCommand dbCommand = this.activeConnection.CreateCommand();
				dbCommand.CommandText = this.CommandText.Render(logEvent);
				dbCommand.CommandType = this.CommandType;
				InternalLogger.Trace("Executing {0}: {1}", new object[]
				{
					dbCommand.CommandType,
					dbCommand.CommandText
				});
				foreach (DatabaseParameterInfo databaseParameterInfo in this.Parameters)
				{
					IDbDataParameter dbDataParameter = dbCommand.CreateParameter();
					dbDataParameter.Direction = ParameterDirection.Input;
					if (databaseParameterInfo.Name != null)
					{
						dbDataParameter.ParameterName = databaseParameterInfo.Name;
					}
					if (databaseParameterInfo.Size != 0)
					{
						dbDataParameter.Size = databaseParameterInfo.Size;
					}
					if (databaseParameterInfo.Precision != 0)
					{
						dbDataParameter.Precision = databaseParameterInfo.Precision;
					}
					if (databaseParameterInfo.Scale != 0)
					{
						dbDataParameter.Scale = databaseParameterInfo.Scale;
					}
					string value = databaseParameterInfo.Layout.Render(logEvent);
					dbDataParameter.Value = value;
					dbCommand.Parameters.Add(dbDataParameter);
					InternalLogger.Trace("  Parameter: '{0}' = '{1}' ({2})", new object[]
					{
						dbDataParameter.ParameterName,
						dbDataParameter.Value,
						dbDataParameter.DbType
					});
				}
				int num = dbCommand.ExecuteNonQuery();
				InternalLogger.Trace("Finished execution, result = {0}", new object[]
				{
					num
				});
				transactionScope.Complete();
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0001CA68 File Offset: 0x0001AC68
		private string BuildConnectionString(LogEventInfo logEvent)
		{
			if (this.ConnectionString != null)
			{
				return this.ConnectionString.Render(logEvent);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Server=");
			stringBuilder.Append(this.DBHost.Render(logEvent));
			stringBuilder.Append(";");
			if (this.DBUserName == null)
			{
				stringBuilder.Append("Trusted_Connection=SSPI;");
			}
			else
			{
				stringBuilder.Append("User id=");
				stringBuilder.Append(this.DBUserName.Render(logEvent));
				stringBuilder.Append(";Password=");
				stringBuilder.Append(this.DBPassword.Render(logEvent));
				stringBuilder.Append(";");
			}
			if (this.DBDatabase != null)
			{
				stringBuilder.Append("Database=");
				stringBuilder.Append(this.DBDatabase.Render(logEvent));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0001CB48 File Offset: 0x0001AD48
		private void EnsureConnectionOpen(string connectionString)
		{
			if (this.activeConnection != null && this.activeConnectionString != connectionString)
			{
				InternalLogger.Trace("DatabaseTarget: close connection because of opening new.");
				this.CloseConnection();
			}
			if (this.activeConnection != null)
			{
				return;
			}
			InternalLogger.Trace("DatabaseTarget: open connection.");
			this.activeConnection = this.OpenConnection(connectionString);
			this.activeConnectionString = connectionString;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0001CBA2 File Offset: 0x0001ADA2
		private void CloseConnection()
		{
			if (this.activeConnection != null)
			{
				this.activeConnection.Close();
				this.activeConnection.Dispose();
				this.activeConnection = null;
				this.activeConnectionString = null;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0001CBD0 File Offset: 0x0001ADD0
		private void RunInstallCommands(InstallationContext installationContext, IEnumerable<DatabaseCommandInfo> commands)
		{
			LogEventInfo logEvent = installationContext.CreateLogEvent();
			try
			{
				foreach (DatabaseCommandInfo databaseCommandInfo in commands)
				{
					string connectionString;
					if (databaseCommandInfo.ConnectionString != null)
					{
						connectionString = databaseCommandInfo.ConnectionString.Render(logEvent);
					}
					else if (this.InstallConnectionString != null)
					{
						connectionString = this.InstallConnectionString.Render(logEvent);
					}
					else
					{
						connectionString = this.BuildConnectionString(logEvent);
					}
					this.EnsureConnectionOpen(connectionString);
					IDbCommand dbCommand = this.activeConnection.CreateCommand();
					dbCommand.CommandType = databaseCommandInfo.CommandType;
					dbCommand.CommandText = databaseCommandInfo.Text.Render(logEvent);
					try
					{
						installationContext.Trace("Executing {0} '{1}'", new object[]
						{
							dbCommand.CommandType,
							dbCommand.CommandText
						});
						dbCommand.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrownImmediately())
						{
							throw;
						}
						if (!databaseCommandInfo.IgnoreFailures && !installationContext.IgnoreFailures)
						{
							installationContext.Error(ex.Message, new object[0]);
							throw;
						}
						installationContext.Warning(ex.Message, new object[0]);
					}
				}
			}
			finally
			{
				InternalLogger.Trace("DatabaseTarget: close connection after install.");
				this.CloseConnection();
			}
		}

		// Token: 0x04000312 RID: 786
		private static Assembly systemDataAssembly = typeof(IDbConnection).Assembly;

		// Token: 0x04000313 RID: 787
		private IDbConnection activeConnection;

		// Token: 0x04000314 RID: 788
		private string activeConnectionString;
	}
}
