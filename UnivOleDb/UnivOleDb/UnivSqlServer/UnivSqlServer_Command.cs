using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using TechnoPro.Common.ClientManager.Core.UnivThroughServer;
using TechnoPro.Common.ClientManager.ICore.UnivThroughServer;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace UnivOleDb.UnivSqlServer
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class UnivSqlServer_Command : UnivCommand, IDisposable
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00005DE0 File Offset: 0x00004DE0
		private void SetTimeout()
		{
			bool flag = this.myCommand != null;
			if (flag)
			{
				this.myCommand.CommandTimeout = 450;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005E0C File Offset: 0x00004E0C
		public UnivSqlServer_Command(string commandText, UnivSqlServer_Connection univConnection, UnivSqlServer_Transaction univTransaction)
		{
			this.myUnivConnection = univConnection;
			this.myCommand = new SqlCommand(commandText, this.myUnivConnection.Connection, univTransaction.Transaction);
			this.SetTimeout();
			this.myUnivParameters = new UnivSqlServer_ParameterCollection(this.myUnivConnection, this, this.myCommand.Parameters);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005E70 File Offset: 0x00004E70
		public UnivSqlServer_Command(string commandText, UnivSqlServer_Connection univConnection, UnivSqlServer_Transaction univTransaction, UnivSqlServer_ParameterCollection univParameters)
		{
			this.myUnivConnection = univConnection;
			this.myUnivParameters = univParameters;
			UnivSqlServer_Transaction univSqlServer_Transaction = (univTransaction != null) ? univTransaction : null;
			bool flag = univSqlServer_Transaction == null || univSqlServer_Transaction.Transaction == null;
			if (flag)
			{
				this.myCommand = new SqlCommand(commandText, this.myUnivConnection.Connection);
			}
			else
			{
				this.myCommand = new SqlCommand(commandText, this.myUnivConnection.Connection, univSqlServer_Transaction.Transaction);
			}
			this.SetTimeout();
			this.myUnivParameters = new UnivSqlServer_ParameterCollection(this.myUnivConnection, this, this.myCommand.Parameters);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005F14 File Offset: 0x00004F14
		public UnivSqlServer_Command(UnivSqlServer_Connection univConnection, SqlCommand command)
		{
			this.myUnivConnection = univConnection;
			bool flag = command == null;
			if (flag)
			{
				this.myCommand = new SqlCommand("", this.myUnivConnection.Connection);
			}
			else
			{
				this.myCommand = command;
			}
			this.SetTimeout();
			this.myUnivParameters = new UnivSqlServer_ParameterCollection(this.myUnivConnection, this, this.myCommand.Parameters);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005F88 File Offset: 0x00004F88
		~UnivSqlServer_Command()
		{
			this.Dispose(false);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005FBC File Offset: 0x00004FBC
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.myCommand.Dispose();
				this.myCommand = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005FEC File Offset: 0x00004FEC
		public string ToStringParametersExpanded()
		{
			return UnivOleDbFactory.ToStringParametersExpanded(this);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006004 File Offset: 0x00005004
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006018 File Offset: 0x00005018
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006035 File Offset: 0x00005035
		public string CommandText
		{
			get
			{
				return this.myCommand.CommandText;
			}
			set
			{
				this.myCommand.CommandText = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006048 File Offset: 0x00005048
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006065 File Offset: 0x00005065
		public int CommandTimeout
		{
			get
			{
				return this.myCommand.CommandTimeout;
			}
			set
			{
				this.myCommand.CommandTimeout = value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600010F RID: 271 RVA: 0x00006078 File Offset: 0x00005078
		// (remove) Token: 0x06000110 RID: 272 RVA: 0x000060B0 File Offset: 0x000050B0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000111 RID: 273 RVA: 0x000060E8 File Offset: 0x000050E8
		// (remove) Token: 0x06000112 RID: 274 RVA: 0x00006120 File Offset: 0x00005120
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006158 File Offset: 0x00005158
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006178 File Offset: 0x00005178
		public UnivTransaction Transaction
		{
			get
			{
				return this.myUnivConnection.Transaction;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.myCommand.Transaction = null;
				}
				else
				{
					this.myCommand.Transaction = ((UnivSqlServer_Transaction)value).Transaction;
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000061B4 File Offset: 0x000051B4
		public UnivParameterCollection Parameters
		{
			get
			{
				return this.myUnivParameters;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000061CC File Offset: 0x000051CC
		public UnivDataReader ExecuteReader2()
		{
			this.OnDatabaseAccessStarted();
			bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
			SqlDataReader reader;
			if (runThroughClockWorkServer)
			{
				string sqlCommandText;
				List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.CommandText, this.Parameters, out sqlCommandText);
				IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
				reader = (SqlDataReader)univThroughServerClientManager.ExecuteReader(sqlCommandText, parameters);
			}
			else
			{
				reader = this.myCommand.ExecuteReader();
			}
			return new UnivSqlServer_DataReader(reader);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006238 File Offset: 0x00005238
		public int ExecuteNonQuery(out string emsg)
		{
			int result;
			try
			{
				bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
				int num;
				if (runThroughClockWorkServer)
				{
					string sqlCommandText;
					List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.myCommand.CommandText, this.Parameters, out sqlCommandText);
					IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
					num = univThroughServerClientManager.ExecuteNonQuery(sqlCommandText, parameters);
				}
				else
				{
					num = this.ExecuteNonQuery();
				}
				emsg = null;
				result = num;
			}
			catch (Exception ex)
			{
				emsg = ex.ToString();
				result = 0;
			}
			return result;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000062B8 File Offset: 0x000052B8
		public int ExecuteNonQuery()
		{
			this.OnDatabaseAccessStarted();
			bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
			int result;
			if (runThroughClockWorkServer)
			{
				string sqlCommandText;
				List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.myCommand.CommandText, this.Parameters, out sqlCommandText);
				IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
				result = univThroughServerClientManager.ExecuteNonQuery(sqlCommandText, parameters);
			}
			else
			{
				this.myUnivConnection.Open();
				result = this.myCommand.ExecuteNonQuery();
				this.myUnivConnection.Close();
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006340 File Offset: 0x00005340
		public int ExecuteNonQuery2()
		{
			int result;
			try
			{
				bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
				if (runThroughClockWorkServer)
				{
					string sqlCommandText;
					List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.myCommand.CommandText, this.Parameters, out sqlCommandText);
					IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
					result = univThroughServerClientManager.ExecuteNonQuery(sqlCommandText, parameters);
				}
				else
				{
					bool isOpen = this.myUnivConnection.IsOpen;
					bool flag = !isOpen;
					if (flag)
					{
						this.myCommand.Connection.Open();
					}
					result = this.myCommand.ExecuteNonQuery();
					bool flag2 = !isOpen;
					if (flag2)
					{
						this.myCommand.Connection.Close();
					}
				}
			}
			catch (Exception ex)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006404 File Offset: 0x00005404
		public int ExecuteNonQuery2(out string emsg)
		{
			this.OnDatabaseAccessStarted();
			int result;
			try
			{
				bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
				if (runThroughClockWorkServer)
				{
					string sqlCommandText;
					List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.myCommand.CommandText, this.Parameters, out sqlCommandText);
					IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
					result = univThroughServerClientManager.ExecuteNonQuery(sqlCommandText, parameters);
				}
				else
				{
					this.myUnivConnection.Open();
					result = this.myCommand.ExecuteNonQuery();
					this.myUnivConnection.Close();
				}
				emsg = null;
			}
			catch (Exception ex)
			{
				result = 0;
				emsg = ex.ToString();
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000064B4 File Offset: 0x000054B4
		public object ExecuteScalar()
		{
			this.OnDatabaseAccessStarted();
			bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
			object result;
			if (runThroughClockWorkServer)
			{
				string sqlCommandText;
				List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.myCommand.CommandText, this.Parameters, out sqlCommandText);
				IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
				result = univThroughServerClientManager.ExecuteScalar(sqlCommandText, parameters);
			}
			else
			{
				result = this.myCommand.ExecuteScalar();
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006527 File Offset: 0x00005527
		public void OnDatabaseAccessStarted()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006527 File Offset: 0x00005527
		public void OnDatabaseAccessEnded()
		{
		}

		// Token: 0x04000033 RID: 51
		private UnivSqlServer_Connection myUnivConnection;

		// Token: 0x04000034 RID: 52
		private SqlCommand myCommand;

		// Token: 0x04000035 RID: 53
		private UnivSqlServer_ParameterCollection myUnivParameters;

		// Token: 0x04000036 RID: 54
		private bool disposed = false;
	}
}
