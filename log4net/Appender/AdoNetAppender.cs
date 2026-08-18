using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000008 RID: 8
	public class AdoNetAppender : BufferingAppenderSkeleton
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000029ED File Offset: 0x00000BED
		public AdoNetAppender()
		{
			this.ConnectionType = "System.Data.OleDb.OleDbConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			this.UseTransactions = true;
			this.CommandType = CommandType.Text;
			this.m_parameters = new ArrayList();
			this.ReconnectOnError = false;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A20 File Offset: 0x00000C20
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A28 File Offset: 0x00000C28
		public string ConnectionString
		{
			get
			{
				return this.m_connectionString;
			}
			set
			{
				this.m_connectionString = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A31 File Offset: 0x00000C31
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A39 File Offset: 0x00000C39
		public string AppSettingsKey
		{
			get
			{
				return this.m_appSettingsKey;
			}
			set
			{
				this.m_appSettingsKey = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A42 File Offset: 0x00000C42
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002A4A File Offset: 0x00000C4A
		public string ConnectionStringName
		{
			get
			{
				return this.m_connectionStringName;
			}
			set
			{
				this.m_connectionStringName = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A53 File Offset: 0x00000C53
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002A5B File Offset: 0x00000C5B
		public string ConnectionType
		{
			get
			{
				return this.m_connectionType;
			}
			set
			{
				this.m_connectionType = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002A64 File Offset: 0x00000C64
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002A6C File Offset: 0x00000C6C
		public string CommandText
		{
			get
			{
				return this.m_commandText;
			}
			set
			{
				this.m_commandText = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002A75 File Offset: 0x00000C75
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002A7D File Offset: 0x00000C7D
		public CommandType CommandType
		{
			get
			{
				return this.m_commandType;
			}
			set
			{
				this.m_commandType = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002A86 File Offset: 0x00000C86
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002A8E File Offset: 0x00000C8E
		public bool UseTransactions
		{
			get
			{
				return this.m_useTransactions;
			}
			set
			{
				this.m_useTransactions = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002A97 File Offset: 0x00000C97
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002A9F File Offset: 0x00000C9F
		public SecurityContext SecurityContext
		{
			get
			{
				return this.m_securityContext;
			}
			set
			{
				this.m_securityContext = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public bool ReconnectOnError
		{
			get
			{
				return this.m_reconnectOnError;
			}
			set
			{
				this.m_reconnectOnError = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002AB9 File Offset: 0x00000CB9
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002AC1 File Offset: 0x00000CC1
		protected IDbConnection Connection
		{
			get
			{
				return this.m_dbConnection;
			}
			set
			{
				this.m_dbConnection = value;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002ACA File Offset: 0x00000CCA
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.SecurityContext == null)
			{
				this.SecurityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
			}
			this.InitializeDatabaseConnection();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AF1 File Offset: 0x00000CF1
		protected override void OnClose()
		{
			base.OnClose();
			this.DiposeConnection();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B00 File Offset: 0x00000D00
		protected override void SendBuffer(LoggingEvent[] events)
		{
			if (this.ReconnectOnError && (this.Connection == null || this.Connection.State != ConnectionState.Open))
			{
				LogLog.Debug(AdoNetAppender.declaringType, "Attempting to reconnect to database. Current Connection State: " + ((this.Connection == null) ? SystemInfo.NullText : this.Connection.State.ToString()));
				this.InitializeDatabaseConnection();
			}
			if (this.Connection != null && this.Connection.State == ConnectionState.Open)
			{
				if (this.UseTransactions)
				{
					using (IDbTransaction dbTransaction = this.Connection.BeginTransaction())
					{
						try
						{
							this.SendBuffer(dbTransaction, events);
							dbTransaction.Commit();
						}
						catch (Exception e)
						{
							try
							{
								dbTransaction.Rollback();
							}
							catch (Exception)
							{
							}
							this.ErrorHandler.Error("Exception while writing to database", e);
						}
						return;
					}
				}
				this.SendBuffer(null, events);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C00 File Offset: 0x00000E00
		public void AddParameter(AdoNetAppenderParameter parameter)
		{
			this.m_parameters.Add(parameter);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C10 File Offset: 0x00000E10
		protected virtual void SendBuffer(IDbTransaction dbTran, LoggingEvent[] events)
		{
			if (this.CommandText != null && this.CommandText.Trim() != "")
			{
				using (IDbCommand dbCommand = this.Connection.CreateCommand())
				{
					dbCommand.CommandText = this.CommandText;
					dbCommand.CommandType = this.CommandType;
					if (dbTran != null)
					{
						dbCommand.Transaction = dbTran;
					}
					dbCommand.Prepare();
					foreach (LoggingEvent loggingEvent in events)
					{
						dbCommand.Parameters.Clear();
						foreach (object obj in this.m_parameters)
						{
							AdoNetAppenderParameter adoNetAppenderParameter = (AdoNetAppenderParameter)obj;
							adoNetAppenderParameter.Prepare(dbCommand);
							adoNetAppenderParameter.FormatValue(dbCommand, loggingEvent);
						}
						dbCommand.ExecuteNonQuery();
					}
					return;
				}
			}
			using (IDbCommand dbCommand2 = this.Connection.CreateCommand())
			{
				if (dbTran != null)
				{
					dbCommand2.Transaction = dbTran;
				}
				foreach (LoggingEvent logEvent in events)
				{
					string logStatement = this.GetLogStatement(logEvent);
					LogLog.Debug(AdoNetAppender.declaringType, "LogStatement [" + logStatement + "]");
					dbCommand2.CommandText = logStatement;
					dbCommand2.ExecuteNonQuery();
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D98 File Offset: 0x00000F98
		protected virtual string GetLogStatement(LoggingEvent logEvent)
		{
			if (this.Layout == null)
			{
				this.ErrorHandler.Error("AdoNetAppender: No Layout specified.");
				return "";
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.Layout.Format(stringWriter, logEvent);
			return stringWriter.ToString();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DE4 File Offset: 0x00000FE4
		protected virtual IDbConnection CreateConnection(Type connectionType, string connectionString)
		{
			IDbConnection dbConnection = (IDbConnection)Activator.CreateInstance(connectionType);
			dbConnection.ConnectionString = connectionString;
			return dbConnection;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E08 File Offset: 0x00001008
		protected virtual string ResolveConnectionString(out string connectionStringContext)
		{
			if (this.ConnectionString != null && this.ConnectionString.Length > 0)
			{
				connectionStringContext = "ConnectionString";
				return this.ConnectionString;
			}
			if (!string.IsNullOrEmpty(this.ConnectionStringName))
			{
				ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[this.ConnectionStringName];
				if (connectionStringSettings != null)
				{
					connectionStringContext = "ConnectionStringName";
					return connectionStringSettings.ConnectionString;
				}
				throw new LogException("Unable to find [" + this.ConnectionStringName + "] ConfigurationManager.ConnectionStrings item");
			}
			else
			{
				if (this.AppSettingsKey == null || this.AppSettingsKey.Length <= 0)
				{
					connectionStringContext = "Unable to resolve connection string from ConnectionString, ConnectionStrings, or AppSettings.";
					return string.Empty;
				}
				connectionStringContext = "AppSettingsKey";
				string appSetting = SystemInfo.GetAppSetting(this.AppSettingsKey);
				if (appSetting == null || appSetting.Length == 0)
				{
					throw new LogException("Unable to find [" + this.AppSettingsKey + "] AppSettings key.");
				}
				return appSetting;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EE0 File Offset: 0x000010E0
		protected virtual Type ResolveConnectionType()
		{
			Type typeFromString;
			try
			{
				typeFromString = SystemInfo.GetTypeFromString(this.ConnectionType, true, false);
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error("Failed to load connection type [" + this.ConnectionType + "]", e);
				throw;
			}
			return typeFromString;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F34 File Offset: 0x00001134
		private void InitializeDatabaseConnection()
		{
			string text = "Unable to determine connection string context.";
			string text2 = string.Empty;
			try
			{
				this.DiposeConnection();
				text2 = this.ResolveConnectionString(out text);
				this.Connection = this.CreateConnection(this.ResolveConnectionType(), text2);
				using (this.SecurityContext.Impersonate(this))
				{
					this.Connection.Open();
				}
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error(string.Concat(new string[]
				{
					"Could not open database connection [",
					text2,
					"]. Connection string context [",
					text,
					"]."
				}), e);
				this.Connection = null;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FFC File Offset: 0x000011FC
		private void DiposeConnection()
		{
			if (this.Connection != null)
			{
				try
				{
					this.Connection.Close();
				}
				catch (Exception exception)
				{
					LogLog.Warn(AdoNetAppender.declaringType, "Exception while disposing cached connection object", exception);
				}
				this.Connection = null;
			}
		}

		// Token: 0x04000015 RID: 21
		protected ArrayList m_parameters;

		// Token: 0x04000016 RID: 22
		private SecurityContext m_securityContext;

		// Token: 0x04000017 RID: 23
		private IDbConnection m_dbConnection;

		// Token: 0x04000018 RID: 24
		private string m_connectionString;

		// Token: 0x04000019 RID: 25
		private string m_appSettingsKey;

		// Token: 0x0400001A RID: 26
		private string m_connectionStringName;

		// Token: 0x0400001B RID: 27
		private string m_connectionType;

		// Token: 0x0400001C RID: 28
		private string m_commandText;

		// Token: 0x0400001D RID: 29
		private CommandType m_commandType;

		// Token: 0x0400001E RID: 30
		private bool m_useTransactions;

		// Token: 0x0400001F RID: 31
		private bool m_reconnectOnError;

		// Token: 0x04000020 RID: 32
		private static readonly Type declaringType = typeof(AdoNetAppender);
	}
}
