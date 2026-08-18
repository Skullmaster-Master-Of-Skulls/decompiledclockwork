using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x02000136 RID: 310
	internal class SqlSessionStateStore : SessionStateStoreProviderBase
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x00030FAD File Offset: 0x0002F1AD
		internal SqlSessionStateStore()
		{
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0003495C File Offset: 0x00032B5C
		internal override void Initialize(string name, NameValueCollection config, IPartitionResolver partitionResolver)
		{
			this._partitionResolver = partitionResolver;
			this.Initialize(name, config);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00034970 File Offset: 0x00032B70
		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "SQL Server Session State Provider";
			}
			base.Initialize(name, config);
			if (!SqlSessionStateStore.s_oneTimeInited)
			{
				SqlSessionStateStore.s_lock.AcquireWriterLock();
				try
				{
					if (!SqlSessionStateStore.s_oneTimeInited)
					{
						this.OneTimeInit();
					}
				}
				finally
				{
					SqlSessionStateStore.s_lock.ReleaseWriterLock();
				}
			}
			if (!SqlSessionStateStore.s_usePartition)
			{
				this._partitionInfo = SqlSessionStateStore.s_singlePartitionInfo;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000349E4 File Offset: 0x00032BE4
		private void OneTimeInit()
		{
			SessionStateSection sessionState = RuntimeConfig.GetAppConfig().SessionState;
			SqlSessionStateStore.s_configPartitionResolverType = sessionState.PartitionResolverType;
			SqlSessionStateStore.s_configSqlConnectionFileName = sessionState.ElementInformation.Properties["sqlConnectionString"].Source;
			SqlSessionStateStore.s_configSqlConnectionLineNumber = sessionState.ElementInformation.Properties["sqlConnectionString"].LineNumber;
			SqlSessionStateStore.s_configAllowCustomSqlDatabase = sessionState.AllowCustomSqlDatabase;
			SqlSessionStateStore.s_configCompressionEnabled = sessionState.CompressionEnabled;
			if (this._partitionResolver == null)
			{
				string sqlConnectionString = sessionState.SqlConnectionString;
				SessionStateModule.ReadConnectionString(sessionState, ref sqlConnectionString, "sqlConnectionString");
				SqlSessionStateStore.s_singlePartitionInfo = (SqlSessionStateStore.SqlPartitionInfo)this.CreatePartitionInfo(sqlConnectionString);
			}
			else
			{
				SqlSessionStateStore.s_usePartition = true;
				SqlSessionStateStore.s_partitionManager = new PartitionManager(new CreatePartitionInfo(this.CreatePartitionInfo));
			}
			SqlSessionStateStore.s_commandTimeout = (int)sessionState.SqlCommandTimeout.TotalSeconds;
			SqlSessionStateStore.s_retryInterval = sessionState.SqlConnectionRetryInterval;
			SqlSessionStateStore.s_isClearPoolInProgress = 0;
			SqlSessionStateStore.s_onAppDomainUnload = new EventHandler(this.OnAppDomainUnload);
			Thread.GetDomain().DomainUnload += SqlSessionStateStore.s_onAppDomainUnload;
			SqlSessionStateStore.s_oneTimeInited = true;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00034AF1 File Offset: 0x00032CF1
		private void OnAppDomainUnload(object unusedObject, EventArgs unusedEventArgs)
		{
			Thread.GetDomain().DomainUnload -= SqlSessionStateStore.s_onAppDomainUnload;
			if (this._partitionResolver == null)
			{
				if (SqlSessionStateStore.s_singlePartitionInfo != null)
				{
					SqlSessionStateStore.s_singlePartitionInfo.Dispose();
					return;
				}
			}
			else if (SqlSessionStateStore.s_partitionManager != null)
			{
				SqlSessionStateStore.s_partitionManager.Dispose();
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00034B30 File Offset: 0x00032D30
		internal IPartitionInfo CreatePartitionInfo(string sqlConnectionString)
		{
			string text = null;
			SqlConnection sqlConnection;
			try
			{
				sqlConnection = new SqlConnection(sqlConnectionString);
			}
			catch (Exception ex)
			{
				if (SqlSessionStateStore.s_usePartition)
				{
					HttpException ex2 = new HttpException(SR.GetString("Error_parsing_sql_partition_resolver_string", new object[]
					{
						SqlSessionStateStore.s_configPartitionResolverType,
						ex.Message
					}), ex);
					ex2.SetFormatter(new UseLastUnhandledErrorFormatter(ex2));
					throw ex2;
				}
				throw new ConfigurationErrorsException(SR.GetString("Error_parsing_session_sqlConnectionString", new object[]
				{
					ex.Message
				}), ex, SqlSessionStateStore.s_configSqlConnectionFileName, SqlSessionStateStore.s_configSqlConnectionLineNumber);
			}
			string text2 = sqlConnection.Database;
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(sqlConnectionString);
			if (string.IsNullOrEmpty(text2))
			{
				text2 = sqlConnectionStringBuilder.AttachDBFilename;
				text = text2;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (!SqlSessionStateStore.s_configAllowCustomSqlDatabase)
				{
					if (SqlSessionStateStore.s_usePartition)
					{
						throw new HttpException(SR.GetString("No_database_allowed_in_sql_partition_resolver_string", new object[]
						{
							SqlSessionStateStore.s_configPartitionResolverType,
							sqlConnection.DataSource,
							text2
						}));
					}
					throw new ConfigurationErrorsException(SR.GetString("No_database_allowed_in_sqlConnectionString"), SqlSessionStateStore.s_configSqlConnectionFileName, SqlSessionStateStore.s_configSqlConnectionLineNumber);
				}
				else if (text != null)
				{
					HttpRuntime.CheckFilePermission(text, true);
				}
			}
			else
			{
				sqlConnectionString += ";Initial Catalog=ASPState";
			}
			return new SqlSessionStateStore.SqlPartitionInfo(new ResourcePool(new TimeSpan(0, 0, 5), int.MaxValue), sqlConnectionStringBuilder.IntegratedSecurity, sqlConnectionString);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00007722 File Offset: 0x00005922
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			return false;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00006164 File Offset: 0x00004364
		public override void Dispose()
		{
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00034C7C File Offset: 0x00032E7C
		public override void InitializeRequest(HttpContext context)
		{
			this._rqContext = context;
			this._rqOrigStreamLen = 0;
			if (SqlSessionStateStore.s_usePartition)
			{
				this._partitionInfo = null;
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00034C9A File Offset: 0x00032E9A
		public override void EndRequest(HttpContext context)
		{
			this._rqContext = null;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00034CA3 File Offset: 0x00032EA3
		public bool KnowForSureNotUsingIntegratedSecurity
		{
			get
			{
				return this._partitionInfo != null && !this._partitionInfo.UseIntegratedSecurity;
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00034CC0 File Offset: 0x00032EC0
		private bool CanUsePooling()
		{
			bool result;
			if (this.KnowForSureNotUsingIntegratedSecurity)
			{
				result = true;
			}
			else if (this._rqContext == null)
			{
				result = false;
			}
			else if (!this._rqContext.IsClientImpersonationConfigured)
			{
				result = true;
			}
			else if (HttpRuntime.IsOnUNCShareInternal)
			{
				result = false;
			}
			else
			{
				string serverVariable = this._rqContext.WorkerRequest.GetServerVariable("LOGON_USER");
				result = string.IsNullOrEmpty(serverVariable);
			}
			return result;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00034D28 File Offset: 0x00032F28
		private SqlSessionStateStore.SqlStateConnection GetConnection(string id, ref bool usePooling)
		{
			SqlSessionStateStore.SqlStateConnection sqlStateConnection = null;
			if (this._partitionInfo == null)
			{
				this._partitionInfo = (SqlSessionStateStore.SqlPartitionInfo)SqlSessionStateStore.s_partitionManager.GetPartition(this._partitionResolver, id);
			}
			usePooling = this.CanUsePooling();
			if (usePooling)
			{
				sqlStateConnection = (SqlSessionStateStore.SqlStateConnection)this._partitionInfo.RetrieveResource();
				if (sqlStateConnection != null && (sqlStateConnection.Connection.State & ConnectionState.Open) == ConnectionState.Closed)
				{
					sqlStateConnection.Dispose();
					sqlStateConnection = null;
				}
			}
			if (sqlStateConnection == null)
			{
				sqlStateConnection = new SqlSessionStateStore.SqlStateConnection(this._partitionInfo, SqlSessionStateStore.s_retryInterval);
			}
			return sqlStateConnection;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00034DA8 File Offset: 0x00032FA8
		private void DisposeOrReuseConnection(ref SqlSessionStateStore.SqlStateConnection conn, bool usePooling)
		{
			try
			{
				if (conn != null)
				{
					if (usePooling)
					{
						conn.ClearAllParameters();
						this._partitionInfo.StoreResource(conn);
						conn = null;
					}
				}
			}
			finally
			{
				if (conn != null)
				{
					conn.Dispose();
				}
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00034DF4 File Offset: 0x00032FF4
		internal static void ThrowSqlConnectionException(SqlConnection conn, Exception e)
		{
			if (SqlSessionStateStore.s_usePartition)
			{
				throw new HttpException(SR.GetString("Cant_connect_sql_session_database_partition_resolver", new object[]
				{
					SqlSessionStateStore.s_configPartitionResolverType,
					conn.DataSource,
					conn.Database
				}), e);
			}
			throw new HttpException(SR.GetString("Cant_connect_sql_session_database"), e);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00034E4C File Offset: 0x0003304C
		private SessionStateStoreData DoGet(HttpContext context, string id, bool getExclusive, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			bool flag = false;
			SqlSessionStateStore.SqlStateConnection sqlStateConnection = null;
			SqlCommand sqlCommand = null;
			bool usePooling = true;
			locked = false;
			lockId = null;
			lockAge = TimeSpan.Zero;
			actionFlags = SessionStateActions.None;
			byte[] array = null;
			sqlStateConnection = this.GetConnection(id, ref usePooling);
			if ((this._partitionInfo.SupportFlags & SqlSessionStateStore.SupportFlags.GetLockAge) != SqlSessionStateStore.SupportFlags.None)
			{
				flag = true;
			}
			SessionStateStoreData result;
			try
			{
				if (getExclusive)
				{
					sqlCommand = sqlStateConnection.TempGetExclusive;
				}
				else
				{
					sqlCommand = sqlStateConnection.TempGet;
				}
				sqlCommand.Parameters[0].Value = id + this._partitionInfo.AppSuffix;
				sqlCommand.Parameters[1].Value = Convert.DBNull;
				sqlCommand.Parameters[2].Value = Convert.DBNull;
				sqlCommand.Parameters[3].Value = Convert.DBNull;
				sqlCommand.Parameters[4].Value = Convert.DBNull;
				sqlCommand.Parameters[5].Value = Convert.DBNull;
				SqlDataReader sqlDataReader2;
				SqlDataReader sqlDataReader = sqlDataReader2 = SqlSessionStateStore.SqlExecuteReaderWithRetry(sqlCommand, CommandBehavior.Default);
				try
				{
					if (sqlDataReader != null)
					{
						try
						{
							if (sqlDataReader.Read())
							{
								array = (byte[])sqlDataReader[0];
							}
						}
						catch (Exception e)
						{
							SqlSessionStateStore.ThrowSqlConnectionException(sqlCommand.Connection, e);
						}
					}
				}
				finally
				{
					if (sqlDataReader2 != null)
					{
						((IDisposable)sqlDataReader2).Dispose();
					}
				}
				if (Convert.IsDBNull(sqlCommand.Parameters[2].Value))
				{
					result = null;
				}
				else
				{
					locked = (bool)sqlCommand.Parameters[2].Value;
					lockId = (int)sqlCommand.Parameters[4].Value;
					if (locked)
					{
						if (flag)
						{
							lockAge = new TimeSpan(0, 0, (int)sqlCommand.Parameters[3].Value);
						}
						else
						{
							DateTime d = (DateTime)sqlCommand.Parameters[3].Value;
							lockAge = DateTime.Now - d;
						}
						if (lockAge > new TimeSpan(0, 0, 31536000))
						{
							lockAge = TimeSpan.Zero;
						}
						result = null;
					}
					else
					{
						actionFlags = (SessionStateActions)sqlCommand.Parameters[5].Value;
						if (array == null)
						{
							array = (byte[])sqlCommand.Parameters[1].Value;
						}
						this.DisposeOrReuseConnection(ref sqlStateConnection, usePooling);
						MemoryStream memoryStream2;
						MemoryStream memoryStream = memoryStream2 = new MemoryStream(array);
						SessionStateStoreData sessionStateStoreData;
						try
						{
							sessionStateStoreData = SessionStateUtility.DeserializeStoreData(context, memoryStream, SqlSessionStateStore.s_configCompressionEnabled);
							this._rqOrigStreamLen = (int)memoryStream.Position;
						}
						finally
						{
							if (memoryStream2 != null)
							{
								((IDisposable)memoryStream2).Dispose();
							}
						}
						result = sessionStateStoreData;
					}
				}
			}
			finally
			{
				this.DisposeOrReuseConnection(ref sqlStateConnection, usePooling);
			}
			return result;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00035164 File Offset: 0x00033364
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			SessionIDManager.CheckIdLength(id, true);
			return this.DoGet(context, id, false, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0003517E File Offset: 0x0003337E
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			SessionIDManager.CheckIdLength(id, true);
			return this.DoGet(context, id, true, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00035198 File Offset: 0x00033398
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			bool usePooling = true;
			SqlSessionStateStore.SqlStateConnection sqlStateConnection = null;
			int num = (int)lockId;
			try
			{
				SessionIDManager.CheckIdLength(id, true);
				sqlStateConnection = this.GetConnection(id, ref usePooling);
				SqlCommand tempReleaseExclusive = sqlStateConnection.TempReleaseExclusive;
				tempReleaseExclusive.Parameters[0].Value = id + this._partitionInfo.AppSuffix;
				tempReleaseExclusive.Parameters[1].Value = num;
				SqlSessionStateStore.SqlExecuteNonQueryWithRetry(tempReleaseExclusive, false, null);
			}
			finally
			{
				this.DisposeOrReuseConnection(ref sqlStateConnection, usePooling);
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00035228 File Offset: 0x00033428
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			bool usePooling = true;
			SqlSessionStateStore.SqlStateConnection sqlStateConnection = null;
			try
			{
				SessionIDManager.CheckIdLength(id, true);
				byte[] value;
				int num;
				try
				{
					SessionStateUtility.SerializeStoreData(item, 7000, out value, out num, SqlSessionStateStore.s_configCompressionEnabled);
				}
				catch
				{
					if (!newItem)
					{
						this.ReleaseItemExclusive(context, id, lockId);
					}
					throw;
				}
				int num2;
				if (lockId == null)
				{
					num2 = 0;
				}
				else
				{
					num2 = (int)lockId;
				}
				sqlStateConnection = this.GetConnection(id, ref usePooling);
				SqlCommand sqlCommand;
				if (!newItem)
				{
					if (num <= 7000)
					{
						if (this._rqOrigStreamLen <= 7000)
						{
							sqlCommand = sqlStateConnection.TempUpdateShort;
						}
						else
						{
							sqlCommand = sqlStateConnection.TempUpdateShortNullLong;
						}
					}
					else if (this._rqOrigStreamLen <= 7000)
					{
						sqlCommand = sqlStateConnection.TempUpdateLongNullShort;
					}
					else
					{
						sqlCommand = sqlStateConnection.TempUpdateLong;
					}
				}
				else if (num <= 7000)
				{
					sqlCommand = sqlStateConnection.TempInsertShort;
				}
				else
				{
					sqlCommand = sqlStateConnection.TempInsertLong;
				}
				sqlCommand.Parameters[0].Value = id + this._partitionInfo.AppSuffix;
				sqlCommand.Parameters[1].Size = num;
				sqlCommand.Parameters[1].Value = value;
				sqlCommand.Parameters[2].Value = item.Timeout;
				if (!newItem)
				{
					sqlCommand.Parameters[3].Value = num2;
				}
				SqlSessionStateStore.SqlExecuteNonQueryWithRetry(sqlCommand, newItem, id);
			}
			finally
			{
				this.DisposeOrReuseConnection(ref sqlStateConnection, usePooling);
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000353B8 File Offset: 0x000335B8
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			bool usePooling = true;
			SqlSessionStateStore.SqlStateConnection sqlStateConnection = null;
			int num = (int)lockId;
			try
			{
				SessionIDManager.CheckIdLength(id, true);
				sqlStateConnection = this.GetConnection(id, ref usePooling);
				SqlCommand tempRemove = sqlStateConnection.TempRemove;
				tempRemove.Parameters[0].Value = id + this._partitionInfo.AppSuffix;
				tempRemove.Parameters[1].Value = num;
				SqlSessionStateStore.SqlExecuteNonQueryWithRetry(tempRemove, false, null);
			}
			finally
			{
				this.DisposeOrReuseConnection(ref sqlStateConnection, usePooling);
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00035448 File Offset: 0x00033648
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			bool usePooling = true;
			SqlSessionStateStore.SqlStateConnection sqlStateConnection = null;
			try
			{
				SessionIDManager.CheckIdLength(id, true);
				sqlStateConnection = this.GetConnection(id, ref usePooling);
				SqlCommand tempResetTimeout = sqlStateConnection.TempResetTimeout;
				tempResetTimeout.Parameters[0].Value = id + this._partitionInfo.AppSuffix;
				SqlSessionStateStore.SqlExecuteNonQueryWithRetry(tempResetTimeout, false, null);
			}
			finally
			{
				this.DisposeOrReuseConnection(ref sqlStateConnection, usePooling);
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00030FA2 File Offset: 0x0002F1A2
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return SessionStateUtility.CreateLegitStoreData(context, null, null, timeout);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000354BC File Offset: 0x000336BC
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			bool usePooling = true;
			SqlSessionStateStore.SqlStateConnection sqlStateConnection = null;
			try
			{
				SessionIDManager.CheckIdLength(id, true);
				byte[] value;
				int size;
				SessionStateUtility.SerializeStoreData(this.CreateNewStoreData(context, timeout), 7000, out value, out size, SqlSessionStateStore.s_configCompressionEnabled);
				sqlStateConnection = this.GetConnection(id, ref usePooling);
				SqlCommand tempInsertUninitializedItem = sqlStateConnection.TempInsertUninitializedItem;
				tempInsertUninitializedItem.Parameters[0].Value = id + this._partitionInfo.AppSuffix;
				tempInsertUninitializedItem.Parameters[1].Size = size;
				tempInsertUninitializedItem.Parameters[1].Value = value;
				tempInsertUninitializedItem.Parameters[2].Value = timeout;
				SqlSessionStateStore.SqlExecuteNonQueryWithRetry(tempInsertUninitializedItem, true, id);
			}
			finally
			{
				this.DisposeOrReuseConnection(ref sqlStateConnection, usePooling);
			}
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0003558C File Offset: 0x0003378C
		private static bool IsInsertPKException(SqlException ex, bool ignoreInsertPKException, string id)
		{
			return ex != null && ex.Number == 2627 && ignoreInsertPKException;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000355A8 File Offset: 0x000337A8
		private static bool IsFatalSqlException(SqlException ex)
		{
			return ex != null && (ex.Class >= 20 || ex.Number == 4060 || ex.Number == -2);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000355D1 File Offset: 0x000337D1
		private static void ClearFlagForClearPoolInProgress()
		{
			Interlocked.CompareExchange(ref SqlSessionStateStore.s_isClearPoolInProgress, 0, 1);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000355E0 File Offset: 0x000337E0
		private static bool CanRetry(SqlException ex, SqlConnection conn, ref bool isFirstAttempt, ref DateTime endRetryTime)
		{
			if (SqlSessionStateStore.s_retryInterval.Seconds <= 0)
			{
				return false;
			}
			if (!SqlSessionStateStore.IsFatalSqlException(ex))
			{
				if (!isFirstAttempt)
				{
					SqlSessionStateStore.ClearFlagForClearPoolInProgress();
				}
				return false;
			}
			if (isFirstAttempt)
			{
				if (Interlocked.CompareExchange(ref SqlSessionStateStore.s_isClearPoolInProgress, 1, 0) == 0)
				{
					SqlConnection.ClearPool(conn);
				}
				Thread.Sleep(5000);
				endRetryTime = DateTime.UtcNow.Add(SqlSessionStateStore.s_retryInterval);
				isFirstAttempt = false;
				return true;
			}
			if (DateTime.UtcNow > endRetryTime)
			{
				if (!isFirstAttempt)
				{
					SqlSessionStateStore.ClearFlagForClearPoolInProgress();
				}
				return false;
			}
			Thread.Sleep(1000);
			return true;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00035678 File Offset: 0x00033878
		private static int SqlExecuteNonQueryWithRetry(SqlCommand cmd, bool ignoreInsertPKException, string id)
		{
			bool flag = true;
			DateTime utcNow = DateTime.UtcNow;
			int result;
			for (;;)
			{
				try
				{
					if (cmd.Connection.State != ConnectionState.Open)
					{
						cmd.Connection.Open();
					}
					int num = cmd.ExecuteNonQuery();
					if (!flag)
					{
						SqlSessionStateStore.ClearFlagForClearPoolInProgress();
					}
					result = num;
				}
				catch (SqlException ex)
				{
					if (!SqlSessionStateStore.IsInsertPKException(ex, ignoreInsertPKException, id))
					{
						if (!SqlSessionStateStore.CanRetry(ex, cmd.Connection, ref flag, ref utcNow))
						{
							SqlSessionStateStore.ThrowSqlConnectionException(cmd.Connection, ex);
						}
						continue;
					}
					result = -1;
				}
				catch (Exception e)
				{
					SqlSessionStateStore.ThrowSqlConnectionException(cmd.Connection, e);
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0003571C File Offset: 0x0003391C
		private static SqlDataReader SqlExecuteReaderWithRetry(SqlCommand cmd, CommandBehavior cmdBehavior)
		{
			bool flag = true;
			DateTime utcNow = DateTime.UtcNow;
			SqlDataReader result;
			for (;;)
			{
				try
				{
					if (cmd.Connection.State != ConnectionState.Open)
					{
						cmd.Connection.Open();
					}
					SqlDataReader sqlDataReader = cmd.ExecuteReader(cmdBehavior);
					if (!flag)
					{
						SqlSessionStateStore.ClearFlagForClearPoolInProgress();
					}
					result = sqlDataReader;
				}
				catch (SqlException ex)
				{
					if (!SqlSessionStateStore.CanRetry(ex, cmd.Connection, ref flag, ref utcNow))
					{
						SqlSessionStateStore.ThrowSqlConnectionException(cmd.Connection, ex);
					}
					continue;
				}
				catch (Exception e)
				{
					SqlSessionStateStore.ThrowSqlConnectionException(cmd.Connection, e);
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x04001474 RID: 5236
		private static ReadWriteSpinLock s_lock;

		// Token: 0x04001475 RID: 5237
		private static int s_isClearPoolInProgress;

		// Token: 0x04001476 RID: 5238
		private static int s_commandTimeout;

		// Token: 0x04001477 RID: 5239
		private static TimeSpan s_retryInterval;

		// Token: 0x04001478 RID: 5240
		private static SqlSessionStateStore.SqlPartitionInfo s_singlePartitionInfo;

		// Token: 0x04001479 RID: 5241
		private static PartitionManager s_partitionManager;

		// Token: 0x0400147A RID: 5242
		private static bool s_oneTimeInited;

		// Token: 0x0400147B RID: 5243
		private static bool s_usePartition;

		// Token: 0x0400147C RID: 5244
		private static EventHandler s_onAppDomainUnload;

		// Token: 0x0400147D RID: 5245
		private static string s_configPartitionResolverType;

		// Token: 0x0400147E RID: 5246
		private static string s_configSqlConnectionFileName;

		// Token: 0x0400147F RID: 5247
		private static int s_configSqlConnectionLineNumber;

		// Token: 0x04001480 RID: 5248
		private static bool s_configAllowCustomSqlDatabase;

		// Token: 0x04001481 RID: 5249
		private static bool s_configCompressionEnabled;

		// Token: 0x04001482 RID: 5250
		private HttpContext _rqContext;

		// Token: 0x04001483 RID: 5251
		private int _rqOrigStreamLen;

		// Token: 0x04001484 RID: 5252
		private IPartitionResolver _partitionResolver;

		// Token: 0x04001485 RID: 5253
		private SqlSessionStateStore.SqlPartitionInfo _partitionInfo;

		// Token: 0x04001486 RID: 5254
		private const int ITEM_SHORT_LENGTH = 7000;

		// Token: 0x04001487 RID: 5255
		private const int SQL_ERROR_PRIMARY_KEY_VIOLATION = 2627;

		// Token: 0x04001488 RID: 5256
		private const int SQL_LOGIN_FAILED = 18456;

		// Token: 0x04001489 RID: 5257
		private const int SQL_LOGIN_FAILED_2 = 18452;

		// Token: 0x0400148A RID: 5258
		private const int SQL_LOGIN_FAILED_3 = 18450;

		// Token: 0x0400148B RID: 5259
		private const int SQL_CANNOT_OPEN_DATABASE_FOR_LOGIN = 4060;

		// Token: 0x0400148C RID: 5260
		private const int SQL_TIMEOUT_EXPIRED = -2;

		// Token: 0x0400148D RID: 5261
		private const int APP_SUFFIX_LENGTH = 8;

		// Token: 0x0400148E RID: 5262
		private const int FIRST_RETRY_SLEEP_TIME = 5000;

		// Token: 0x0400148F RID: 5263
		private const int RETRY_SLEEP_TIME = 1000;

		// Token: 0x04001490 RID: 5264
		private static int ID_LENGTH = SessionIDManager.SessionIDMaxLength + 8;

		// Token: 0x04001491 RID: 5265
		internal const int SQL_COMMAND_TIMEOUT_DEFAULT = 30;

		// Token: 0x02000901 RID: 2305
		internal enum SupportFlags : uint
		{
			// Token: 0x040036EF RID: 14063
			None,
			// Token: 0x040036F0 RID: 14064
			GetLockAge,
			// Token: 0x040036F1 RID: 14065
			Uninitialized = 4294967295U
		}

		// Token: 0x02000902 RID: 2306
		internal class SqlPartitionInfo : PartitionInfo
		{
			// Token: 0x060068BC RID: 26812 RVA: 0x00174CFE File Offset: 0x00172EFE
			internal SqlPartitionInfo(ResourcePool rpool, bool useIntegratedSecurity, string sqlConnectionString) : base(rpool)
			{
				this._useIntegratedSecurity = useIntegratedSecurity;
				this._sqlConnectionString = sqlConnectionString;
			}

			// Token: 0x17001D0F RID: 7439
			// (get) Token: 0x060068BD RID: 26813 RVA: 0x00174D27 File Offset: 0x00172F27
			internal bool UseIntegratedSecurity
			{
				get
				{
					return this._useIntegratedSecurity;
				}
			}

			// Token: 0x17001D10 RID: 7440
			// (get) Token: 0x060068BE RID: 26814 RVA: 0x00174D2F File Offset: 0x00172F2F
			internal string SqlConnectionString
			{
				get
				{
					return this._sqlConnectionString;
				}
			}

			// Token: 0x17001D11 RID: 7441
			// (get) Token: 0x060068BF RID: 26815 RVA: 0x00174D37 File Offset: 0x00172F37
			// (set) Token: 0x060068C0 RID: 26816 RVA: 0x00174D3F File Offset: 0x00172F3F
			internal SqlSessionStateStore.SupportFlags SupportFlags
			{
				get
				{
					return this._support;
				}
				set
				{
					this._support = value;
				}
			}

			// Token: 0x17001D12 RID: 7442
			// (get) Token: 0x060068C1 RID: 26817 RVA: 0x00174D48 File Offset: 0x00172F48
			protected override string TracingPartitionString
			{
				get
				{
					if (this._tracingPartitionString == null)
					{
						this._tracingPartitionString = new SqlConnectionStringBuilder(this._sqlConnectionString)
						{
							Password = string.Empty,
							UserID = string.Empty
						}.ConnectionString;
					}
					return this._tracingPartitionString;
				}
			}

			// Token: 0x17001D13 RID: 7443
			// (get) Token: 0x060068C2 RID: 26818 RVA: 0x00174D91 File Offset: 0x00172F91
			internal string AppSuffix
			{
				get
				{
					return this._appSuffix;
				}
			}

			// Token: 0x060068C3 RID: 26819 RVA: 0x00174D9C File Offset: 0x00172F9C
			private void GetServerSupportOptions(SqlConnection sqlConnection)
			{
				SqlSessionStateStore.SupportFlags supportFlags = SqlSessionStateStore.SupportFlags.None;
				bool flag = false;
				SqlDataReader sqlDataReader2;
				SqlDataReader sqlDataReader = sqlDataReader2 = SqlSessionStateStore.SqlExecuteReaderWithRetry(new SqlCommand("Select name from sysobjects where type = 'P' and name = 'TempGetVersion'", sqlConnection)
				{
					CommandType = CommandType.Text
				}, CommandBehavior.SingleRow);
				try
				{
					if (sqlDataReader.Read())
					{
						flag = true;
					}
				}
				finally
				{
					if (sqlDataReader2 != null)
					{
						((IDisposable)sqlDataReader2).Dispose();
					}
				}
				if (flag)
				{
					SqlCommand sqlCommand = new SqlCommand("dbo.GetMajorVersion", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlParameter sqlParameter = sqlCommand.Parameters.Add(new SqlParameter("@@ver", SqlDbType.Int));
					sqlParameter.Direction = ParameterDirection.Output;
					SqlSessionStateStore.SqlExecuteNonQueryWithRetry(sqlCommand, false, null);
					try
					{
						if ((int)sqlParameter.Value >= 8)
						{
							supportFlags |= SqlSessionStateStore.SupportFlags.GetLockAge;
						}
						this.SupportFlags = supportFlags;
					}
					catch (Exception e)
					{
						SqlSessionStateStore.ThrowSqlConnectionException(sqlConnection, e);
					}
					return;
				}
				if (SqlSessionStateStore.s_usePartition)
				{
					throw new HttpException(SR.GetString("Need_v2_SQL_Server_partition_resolver", new object[]
					{
						SqlSessionStateStore.s_configPartitionResolverType,
						sqlConnection.DataSource,
						sqlConnection.Database
					}));
				}
				throw new HttpException(SR.GetString("Need_v2_SQL_Server"));
			}

			// Token: 0x060068C4 RID: 26820 RVA: 0x00174EB0 File Offset: 0x001730B0
			internal void InitSqlInfo(SqlConnection sqlConnection)
			{
				if (this._sqlInfoInited)
				{
					return;
				}
				object @lock = this._lock;
				lock (@lock)
				{
					if (!this._sqlInfoInited)
					{
						this.GetServerSupportOptions(sqlConnection);
						SqlCommand sqlCommand = new SqlCommand("dbo.TempGetAppID", sqlConnection);
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						SqlParameter sqlParameter = sqlCommand.Parameters.Add(new SqlParameter("@appName", SqlDbType.VarChar, 280));
						sqlParameter.Value = HttpRuntime.AppDomainAppId;
						sqlParameter = sqlCommand.Parameters.Add(new SqlParameter("@appId", SqlDbType.Int));
						sqlParameter.Direction = ParameterDirection.Output;
						sqlParameter.Value = Convert.DBNull;
						sqlCommand.ExecuteNonQuery();
						this._appSuffix = ((int)sqlParameter.Value).ToString("x8", CultureInfo.InvariantCulture);
						this._sqlInfoInited = true;
					}
				}
			}

			// Token: 0x040036F2 RID: 14066
			private bool _useIntegratedSecurity;

			// Token: 0x040036F3 RID: 14067
			private string _sqlConnectionString;

			// Token: 0x040036F4 RID: 14068
			private string _tracingPartitionString;

			// Token: 0x040036F5 RID: 14069
			private SqlSessionStateStore.SupportFlags _support = (SqlSessionStateStore.SupportFlags)4294967295U;

			// Token: 0x040036F6 RID: 14070
			private string _appSuffix;

			// Token: 0x040036F7 RID: 14071
			private object _lock = new object();

			// Token: 0x040036F8 RID: 14072
			private bool _sqlInfoInited;

			// Token: 0x040036F9 RID: 14073
			private const string APP_SUFFIX_FORMAT = "x8";

			// Token: 0x040036FA RID: 14074
			private const int APPID_MAX = 280;

			// Token: 0x040036FB RID: 14075
			private const int SQL_2000_MAJ_VER = 8;
		}

		// Token: 0x02000903 RID: 2307
		private class SqlStateConnection : IDisposable
		{
			// Token: 0x060068C5 RID: 26821 RVA: 0x00174FA8 File Offset: 0x001731A8
			internal SqlStateConnection(SqlSessionStateStore.SqlPartitionInfo sqlPartitionInfo, TimeSpan retryInterval)
			{
				this._partitionInfo = sqlPartitionInfo;
				this._sqlConnection = new SqlConnection(sqlPartitionInfo.SqlConnectionString);
				bool flag = true;
				DateTime utcNow = DateTime.UtcNow;
				for (;;)
				{
					try
					{
						this._sqlConnection.Open();
						if (!flag)
						{
							SqlSessionStateStore.ClearFlagForClearPoolInProgress();
						}
					}
					catch (SqlException ex)
					{
						if (ex != null && (ex.Number == 18456 || ex.Number == 18452 || ex.Number == 18450))
						{
							SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(sqlPartitionInfo.SqlConnectionString);
							string text;
							if (sqlConnectionStringBuilder.IntegratedSecurity)
							{
								text = WindowsIdentity.GetCurrent().Name;
							}
							else
							{
								text = sqlConnectionStringBuilder.UserID;
							}
							HttpException ex2 = new HttpException(SR.GetString("Login_failed_sql_session_database", new object[]
							{
								text
							}), ex);
							ex2.SetFormatter(new UseLastUnhandledErrorFormatter(ex2));
							this.ClearConnectionAndThrow(ex2);
						}
						if (!SqlSessionStateStore.CanRetry(ex, this._sqlConnection, ref flag, ref utcNow))
						{
							this.ClearConnectionAndThrow(ex);
						}
						continue;
					}
					catch (Exception e)
					{
						this.ClearConnectionAndThrow(e);
						continue;
					}
					break;
				}
				try
				{
					this._partitionInfo.InitSqlInfo(this._sqlConnection);
					PerfCounters.IncrementCounter(AppPerfCounter.SESSION_SQL_SERVER_CONNECTIONS);
				}
				catch
				{
					this.Dispose();
					throw;
				}
			}

			// Token: 0x060068C6 RID: 26822 RVA: 0x001750F8 File Offset: 0x001732F8
			private void ClearConnectionAndThrow(Exception e)
			{
				SqlConnection sqlConnection = this._sqlConnection;
				this._sqlConnection = null;
				SqlSessionStateStore.ThrowSqlConnectionException(sqlConnection, e);
			}

			// Token: 0x060068C7 RID: 26823 RVA: 0x0017511C File Offset: 0x0017331C
			internal void ClearAllParameters()
			{
				this.ClearAllParameters(this._cmdTempGet);
				this.ClearAllParameters(this._cmdTempGetExclusive);
				this.ClearAllParameters(this._cmdTempReleaseExclusive);
				this.ClearAllParameters(this._cmdTempInsertShort);
				this.ClearAllParameters(this._cmdTempInsertLong);
				this.ClearAllParameters(this._cmdTempUpdateShort);
				this.ClearAllParameters(this._cmdTempUpdateShortNullLong);
				this.ClearAllParameters(this._cmdTempUpdateLong);
				this.ClearAllParameters(this._cmdTempUpdateLongNullShort);
				this.ClearAllParameters(this._cmdTempRemove);
				this.ClearAllParameters(this._cmdTempResetTimeout);
				this.ClearAllParameters(this._cmdTempInsertUninitializedItem);
			}

			// Token: 0x060068C8 RID: 26824 RVA: 0x001751BC File Offset: 0x001733BC
			internal void ClearAllParameters(SqlCommand cmd)
			{
				if (cmd == null)
				{
					return;
				}
				foreach (object obj in cmd.Parameters)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					sqlParameter.Value = Convert.DBNull;
				}
			}

			// Token: 0x17001D14 RID: 7444
			// (get) Token: 0x060068C9 RID: 26825 RVA: 0x00175220 File Offset: 0x00173420
			internal SqlCommand TempGet
			{
				get
				{
					if (this._cmdTempGet == null)
					{
						this._cmdTempGet = new SqlCommand("dbo.TempGetStateItem3", this._sqlConnection);
						this._cmdTempGet.CommandType = CommandType.StoredProcedure;
						this._cmdTempGet.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						if ((this._partitionInfo.SupportFlags & SqlSessionStateStore.SupportFlags.GetLockAge) != SqlSessionStateStore.SupportFlags.None)
						{
							this._cmdTempGet.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
							SqlParameter sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@locked", SqlDbType.Bit));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@lockAge", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@actionFlags", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
						}
						else
						{
							this._cmdTempGet.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
							SqlParameter sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@locked", SqlDbType.Bit));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@lockDate", SqlDbType.DateTime));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGet.Parameters.Add(new SqlParameter("@actionFlags", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
						}
					}
					return this._cmdTempGet;
				}
			}

			// Token: 0x17001D15 RID: 7445
			// (get) Token: 0x060068CA RID: 26826 RVA: 0x00175438 File Offset: 0x00173638
			internal SqlCommand TempGetExclusive
			{
				get
				{
					if (this._cmdTempGetExclusive == null)
					{
						this._cmdTempGetExclusive = new SqlCommand("dbo.TempGetStateItemExclusive3", this._sqlConnection);
						this._cmdTempGetExclusive.CommandType = CommandType.StoredProcedure;
						this._cmdTempGetExclusive.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						if ((this._partitionInfo.SupportFlags & SqlSessionStateStore.SupportFlags.GetLockAge) != SqlSessionStateStore.SupportFlags.None)
						{
							this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
							SqlParameter sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@locked", SqlDbType.Bit));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@lockAge", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@actionFlags", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
						}
						else
						{
							this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
							SqlParameter sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@locked", SqlDbType.Bit));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@lockDate", SqlDbType.DateTime));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
							sqlParameter = this._cmdTempGetExclusive.Parameters.Add(new SqlParameter("@actionFlags", SqlDbType.Int));
							sqlParameter.Direction = ParameterDirection.Output;
						}
					}
					return this._cmdTempGetExclusive;
				}
			}

			// Token: 0x17001D16 RID: 7446
			// (get) Token: 0x060068CB RID: 26827 RVA: 0x00175650 File Offset: 0x00173850
			internal SqlCommand TempReleaseExclusive
			{
				get
				{
					if (this._cmdTempReleaseExclusive == null)
					{
						this._cmdTempReleaseExclusive = new SqlCommand("dbo.TempReleaseStateItemExclusive", this._sqlConnection);
						this._cmdTempReleaseExclusive.CommandType = CommandType.StoredProcedure;
						this._cmdTempReleaseExclusive.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempReleaseExclusive.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempReleaseExclusive.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
					}
					return this._cmdTempReleaseExclusive;
				}
			}

			// Token: 0x17001D17 RID: 7447
			// (get) Token: 0x060068CC RID: 26828 RVA: 0x001756DC File Offset: 0x001738DC
			internal SqlCommand TempInsertLong
			{
				get
				{
					if (this._cmdTempInsertLong == null)
					{
						this._cmdTempInsertLong = new SqlCommand("dbo.TempInsertStateItemLong", this._sqlConnection);
						this._cmdTempInsertLong.CommandType = CommandType.StoredProcedure;
						this._cmdTempInsertLong.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempInsertLong.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempInsertLong.Parameters.Add(new SqlParameter("@itemLong", SqlDbType.Image, 8000));
						this._cmdTempInsertLong.Parameters.Add(new SqlParameter("@timeout", SqlDbType.Int));
					}
					return this._cmdTempInsertLong;
				}
			}

			// Token: 0x17001D18 RID: 7448
			// (get) Token: 0x060068CD RID: 26829 RVA: 0x0017578C File Offset: 0x0017398C
			internal SqlCommand TempInsertShort
			{
				get
				{
					if (this._cmdTempInsertShort == null)
					{
						this._cmdTempInsertShort = new SqlCommand("dbo.TempInsertStateItemShort", this._sqlConnection);
						this._cmdTempInsertShort.CommandType = CommandType.StoredProcedure;
						this._cmdTempInsertShort.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempInsertShort.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempInsertShort.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
						this._cmdTempInsertShort.Parameters.Add(new SqlParameter("@timeout", SqlDbType.Int));
					}
					return this._cmdTempInsertShort;
				}
			}

			// Token: 0x17001D19 RID: 7449
			// (get) Token: 0x060068CE RID: 26830 RVA: 0x0017583C File Offset: 0x00173A3C
			internal SqlCommand TempUpdateLong
			{
				get
				{
					if (this._cmdTempUpdateLong == null)
					{
						this._cmdTempUpdateLong = new SqlCommand("dbo.TempUpdateStateItemLong", this._sqlConnection);
						this._cmdTempUpdateLong.CommandType = CommandType.StoredProcedure;
						this._cmdTempUpdateLong.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempUpdateLong.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempUpdateLong.Parameters.Add(new SqlParameter("@itemLong", SqlDbType.Image, 8000));
						this._cmdTempUpdateLong.Parameters.Add(new SqlParameter("@timeout", SqlDbType.Int));
						this._cmdTempUpdateLong.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
					}
					return this._cmdTempUpdateLong;
				}
			}

			// Token: 0x17001D1A RID: 7450
			// (get) Token: 0x060068CF RID: 26831 RVA: 0x00175908 File Offset: 0x00173B08
			internal SqlCommand TempUpdateShort
			{
				get
				{
					if (this._cmdTempUpdateShort == null)
					{
						this._cmdTempUpdateShort = new SqlCommand("dbo.TempUpdateStateItemShort", this._sqlConnection);
						this._cmdTempUpdateShort.CommandType = CommandType.StoredProcedure;
						this._cmdTempUpdateShort.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempUpdateShort.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempUpdateShort.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
						this._cmdTempUpdateShort.Parameters.Add(new SqlParameter("@timeout", SqlDbType.Int));
						this._cmdTempUpdateShort.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
					}
					return this._cmdTempUpdateShort;
				}
			}

			// Token: 0x17001D1B RID: 7451
			// (get) Token: 0x060068D0 RID: 26832 RVA: 0x001759D4 File Offset: 0x00173BD4
			internal SqlCommand TempUpdateShortNullLong
			{
				get
				{
					if (this._cmdTempUpdateShortNullLong == null)
					{
						this._cmdTempUpdateShortNullLong = new SqlCommand("dbo.TempUpdateStateItemShortNullLong", this._sqlConnection);
						this._cmdTempUpdateShortNullLong.CommandType = CommandType.StoredProcedure;
						this._cmdTempUpdateShortNullLong.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempUpdateShortNullLong.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempUpdateShortNullLong.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
						this._cmdTempUpdateShortNullLong.Parameters.Add(new SqlParameter("@timeout", SqlDbType.Int));
						this._cmdTempUpdateShortNullLong.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
					}
					return this._cmdTempUpdateShortNullLong;
				}
			}

			// Token: 0x17001D1C RID: 7452
			// (get) Token: 0x060068D1 RID: 26833 RVA: 0x00175AA0 File Offset: 0x00173CA0
			internal SqlCommand TempUpdateLongNullShort
			{
				get
				{
					if (this._cmdTempUpdateLongNullShort == null)
					{
						this._cmdTempUpdateLongNullShort = new SqlCommand("dbo.TempUpdateStateItemLongNullShort", this._sqlConnection);
						this._cmdTempUpdateLongNullShort.CommandType = CommandType.StoredProcedure;
						this._cmdTempUpdateLongNullShort.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempUpdateLongNullShort.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempUpdateLongNullShort.Parameters.Add(new SqlParameter("@itemLong", SqlDbType.Image, 8000));
						this._cmdTempUpdateLongNullShort.Parameters.Add(new SqlParameter("@timeout", SqlDbType.Int));
						this._cmdTempUpdateLongNullShort.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
					}
					return this._cmdTempUpdateLongNullShort;
				}
			}

			// Token: 0x17001D1D RID: 7453
			// (get) Token: 0x060068D2 RID: 26834 RVA: 0x00175B6C File Offset: 0x00173D6C
			internal SqlCommand TempRemove
			{
				get
				{
					if (this._cmdTempRemove == null)
					{
						this._cmdTempRemove = new SqlCommand("dbo.TempRemoveStateItem", this._sqlConnection);
						this._cmdTempRemove.CommandType = CommandType.StoredProcedure;
						this._cmdTempRemove.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempRemove.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempRemove.Parameters.Add(new SqlParameter("@lockCookie", SqlDbType.Int));
					}
					return this._cmdTempRemove;
				}
			}

			// Token: 0x17001D1E RID: 7454
			// (get) Token: 0x060068D3 RID: 26835 RVA: 0x00175BF8 File Offset: 0x00173DF8
			internal SqlCommand TempInsertUninitializedItem
			{
				get
				{
					if (this._cmdTempInsertUninitializedItem == null)
					{
						this._cmdTempInsertUninitializedItem = new SqlCommand("dbo.TempInsertUninitializedItem", this._sqlConnection);
						this._cmdTempInsertUninitializedItem.CommandType = CommandType.StoredProcedure;
						this._cmdTempInsertUninitializedItem.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempInsertUninitializedItem.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
						this._cmdTempInsertUninitializedItem.Parameters.Add(new SqlParameter("@itemShort", SqlDbType.VarBinary, 7000));
						this._cmdTempInsertUninitializedItem.Parameters.Add(new SqlParameter("@timeout", SqlDbType.Int));
					}
					return this._cmdTempInsertUninitializedItem;
				}
			}

			// Token: 0x17001D1F RID: 7455
			// (get) Token: 0x060068D4 RID: 26836 RVA: 0x00175CA8 File Offset: 0x00173EA8
			internal SqlCommand TempResetTimeout
			{
				get
				{
					if (this._cmdTempResetTimeout == null)
					{
						this._cmdTempResetTimeout = new SqlCommand("dbo.TempResetTimeout", this._sqlConnection);
						this._cmdTempResetTimeout.CommandType = CommandType.StoredProcedure;
						this._cmdTempResetTimeout.CommandTimeout = SqlSessionStateStore.s_commandTimeout;
						this._cmdTempResetTimeout.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, SqlSessionStateStore.ID_LENGTH));
					}
					return this._cmdTempResetTimeout;
				}
			}

			// Token: 0x060068D5 RID: 26837 RVA: 0x00175D17 File Offset: 0x00173F17
			public void Dispose()
			{
				if (this._sqlConnection != null)
				{
					this._sqlConnection.Close();
					this._sqlConnection = null;
					PerfCounters.DecrementCounter(AppPerfCounter.SESSION_SQL_SERVER_CONNECTIONS);
				}
			}

			// Token: 0x17001D20 RID: 7456
			// (get) Token: 0x060068D6 RID: 26838 RVA: 0x00175D3A File Offset: 0x00173F3A
			internal SqlConnection Connection
			{
				get
				{
					return this._sqlConnection;
				}
			}

			// Token: 0x040036FC RID: 14076
			private SqlConnection _sqlConnection;

			// Token: 0x040036FD RID: 14077
			private SqlCommand _cmdTempGet;

			// Token: 0x040036FE RID: 14078
			private SqlCommand _cmdTempGetExclusive;

			// Token: 0x040036FF RID: 14079
			private SqlCommand _cmdTempReleaseExclusive;

			// Token: 0x04003700 RID: 14080
			private SqlCommand _cmdTempInsertShort;

			// Token: 0x04003701 RID: 14081
			private SqlCommand _cmdTempInsertLong;

			// Token: 0x04003702 RID: 14082
			private SqlCommand _cmdTempUpdateShort;

			// Token: 0x04003703 RID: 14083
			private SqlCommand _cmdTempUpdateShortNullLong;

			// Token: 0x04003704 RID: 14084
			private SqlCommand _cmdTempUpdateLong;

			// Token: 0x04003705 RID: 14085
			private SqlCommand _cmdTempUpdateLongNullShort;

			// Token: 0x04003706 RID: 14086
			private SqlCommand _cmdTempRemove;

			// Token: 0x04003707 RID: 14087
			private SqlCommand _cmdTempResetTimeout;

			// Token: 0x04003708 RID: 14088
			private SqlCommand _cmdTempInsertUninitializedItem;

			// Token: 0x04003709 RID: 14089
			private SqlSessionStateStore.SqlPartitionInfo _partitionInfo;
		}
	}
}
