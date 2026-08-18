using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001D6 RID: 470
	internal sealed class SqlInternalConnectionTds : SqlInternalConnection, IDisposable
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x000D0138 File Offset: 0x000CF538
		internal SessionData CurrentSessionData
		{
			get
			{
				if (this._currentSessionData != null)
				{
					this._currentSessionData._database = base.CurrentDatabase;
					this._currentSessionData._language = this._currentLanguage;
				}
				return this._currentSessionData;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x000D0178 File Offset: 0x000CF578
		internal SqlConnectionTimeoutErrorInternal TimeoutErrorInternal
		{
			get
			{
				return this.timeoutErrorInternal;
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000D018C File Offset: 0x000CF58C
		static SqlInternalConnectionTds()
		{
			SqlInternalConnectionTds.populateTransientErrors();
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000D01C4 File Offset: 0x000CF5C4
		internal SqlInternalConnectionTds(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, object providerInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString userConnectionOptions = null, SessionData reconnectSessionData = null, DbConnectionPool pool = null, string accessToken = null, bool applyTransientFaultHandling = false, SqlAuthenticationProviderManager sqlAuthProviderManager = null) : base(connectionOptions)
		{
			this._dbConnectionPool = pool;
			if (connectionOptions.ConnectRetryCount > 0)
			{
				this._recoverySessionData = reconnectSessionData;
				if (reconnectSessionData == null)
				{
					this._currentSessionData = new SessionData();
				}
				else
				{
					this._currentSessionData = new SessionData(this._recoverySessionData);
					this._originalDatabase = this._recoverySessionData._initialDatabase;
					this._originalLanguage = this._recoverySessionData._initialLanguage;
				}
			}
			if (connectionOptions.UserInstance && InOutOfProcHelper.InProc)
			{
				throw SQL.UserInstanceNotAvailableInProc();
			}
			if (accessToken != null)
			{
				this._accessTokenInBytes = Encoding.Unicode.GetBytes(accessToken);
			}
			this._activeDirectoryAuthTimeoutRetryHelper = new ActiveDirectoryAuthenticationTimeoutRetryHelper();
			this._sqlAuthenticationProviderManager = (sqlAuthProviderManager ?? SqlAuthenticationProviderManager.Instance);
			this._identity = identity;
			this._poolGroupProviderInfo = (SqlConnectionPoolGroupProviderInfo)providerInfo;
			this._fResetConnection = connectionOptions.ConnectionReset;
			if (this._fResetConnection && this._recoverySessionData == null)
			{
				this._originalDatabase = connectionOptions.InitialCatalog;
				this._originalLanguage = connectionOptions.CurrentLanguage;
			}
			this.timeoutErrorInternal = new SqlConnectionTimeoutErrorInternal();
			this._credential = credential;
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._timeout = TimeoutTimer.StartSecondsTimeout(connectionOptions.ConnectTimeout);
				int num = applyTransientFaultHandling ? (connectionOptions.ConnectRetryCount + 1) : 1;
				int num2 = connectionOptions.ConnectRetryInterval * 1000;
				for (int i = 0; i < num; i++)
				{
					try
					{
						this.OpenLoginEnlist(this._timeout, connectionOptions, credential, newPassword, newSecurePassword, redirectedUserInstance);
						break;
					}
					catch (SqlException ex)
					{
						if (i + 1 == num || !applyTransientFaultHandling || this._timeout.IsExpired || this._timeout.MillisecondsRemaining < (long)num2 || !this.IsTransientError(ex))
						{
							throw ex;
						}
						Thread.Sleep(num2);
					}
				}
			}
			catch (OutOfMemoryException)
			{
				base.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				base.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				base.DoomThisConnection();
				throw;
			}
			finally
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionTds.ctor|ADV> %d#, constructed new TDS internal connection\n", base.ObjectID);
			}
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000D0478 File Offset: 0x000CF878
		private static void populateTransientErrors()
		{
			SqlInternalConnectionTds.transientErrors.Add(4060);
			SqlInternalConnectionTds.transientErrors.Add(10928);
			SqlInternalConnectionTds.transientErrors.Add(10929);
			SqlInternalConnectionTds.transientErrors.Add(40197);
			SqlInternalConnectionTds.transientErrors.Add(40020);
			SqlInternalConnectionTds.transientErrors.Add(40143);
			SqlInternalConnectionTds.transientErrors.Add(40166);
			SqlInternalConnectionTds.transientErrors.Add(40540);
			SqlInternalConnectionTds.transientErrors.Add(40501);
			SqlInternalConnectionTds.transientErrors.Add(40613);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000D0528 File Offset: 0x000CF928
		private bool IsTransientError(SqlException exc)
		{
			if (exc == null)
			{
				return false;
			}
			foreach (object obj in exc.Errors)
			{
				SqlError sqlError = (SqlError)obj;
				if (SqlInternalConnectionTds.transientErrors.Contains(sqlError.Number))
				{
					if (!LocalAppContextSwitches.DisablePooledConnectionResetOnTransientError)
					{
						base.UnDoomThisConnection();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000D05B4 File Offset: 0x000CF9B4
		internal Guid ClientConnectionId
		{
			get
			{
				return this._clientConnectionId;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x000D05C8 File Offset: 0x000CF9C8
		internal Guid OriginalClientConnectionId
		{
			get
			{
				return this._originalClientConnectionId;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x000D05DC File Offset: 0x000CF9DC
		internal string RoutingDestination
		{
			get
			{
				return this._routingDestination;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x000D05F0 File Offset: 0x000CF9F0
		internal RoutingInfo RoutingInfo
		{
			get
			{
				return this._routingInfo;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x000D0604 File Offset: 0x000CFA04
		internal override SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._parser.CurrentTransaction;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x000D061C File Offset: 0x000CFA1C
		internal override SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				if (!this._parser._fResetConnection)
				{
					return this.CurrentTransaction;
				}
				return null;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x000D0640 File Offset: 0x000CFA40
		internal override SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this._parser.PendingTransaction;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x000D0658 File Offset: 0x000CFA58
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x000D066C File Offset: 0x000CFA6C
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x000D0680 File Offset: 0x000CFA80
		internal override bool IsLockedForBulkCopy
		{
			get
			{
				return !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x000D06AC File Offset: 0x000CFAAC
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot && (!this.IsKatmaiOrNewer || base.Pool == null);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x000D06D8 File Offset: 0x000CFAD8
		internal override bool IsShiloh
		{
			get
			{
				return this._loginAck.isVersion8;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x000D06F0 File Offset: 0x000CFAF0
		internal override bool IsYukonOrNewer
		{
			get
			{
				return this._parser.IsYukonOrNewer;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x000D0708 File Offset: 0x000CFB08
		internal override bool IsKatmaiOrNewer
		{
			get
			{
				return this._parser.IsKatmaiOrNewer;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x000D0720 File Offset: 0x000CFB20
		internal int PacketSize
		{
			get
			{
				return this._currentPacketSize;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000D0734 File Offset: 0x000CFB34
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000D0748 File Offset: 0x000CFB48
		internal string ServerProvidedFailOverPartner
		{
			get
			{
				return this._currentFailoverPartner;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x000D075C File Offset: 0x000CFB5C
		internal SqlConnectionPoolGroupProviderInfo PoolGroupProviderInfo
		{
			get
			{
				return this._poolGroupProviderInfo;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000D0770 File Offset: 0x000CFB70
		protected override bool ReadyToPrepareTransaction
		{
			get
			{
				return base.FindLiveReader(null) == null;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x000D078C File Offset: 0x000CFB8C
		public override string ServerVersion
		{
			get
			{
				return string.Format(null, "{0:00}.{1:00}.{2:0000}", new object[]
				{
					this._loginAck.majorVersion,
					(short)this._loginAck.minorVersion,
					this._loginAck.buildNum
				});
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000D07E4 File Offset: 0x000CFBE4
		protected override bool UnbindOnTransactionCompletion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x000D07F4 File Offset: 0x000CFBF4
		protected override void ChangeDatabaseInternal(string database)
		{
			database = SqlConnection.FixupDatabaseTransactionName(database);
			Task task = this._parser.TdsExecuteSQLBatch("use " + database, base.ConnectionOptions.ConnectTimeout, null, this._parser._physicalStateObj, true, false, null);
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000D0858 File Offset: 0x000CFC58
		public override void Dispose()
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionTds.Dispose|ADV> %d# disposing\n", base.ObjectID);
			}
			try
			{
				TdsParser tdsParser = Interlocked.Exchange<TdsParser>(ref this._parser, null);
				if (tdsParser != null)
				{
					tdsParser.Disconnect();
				}
			}
			finally
			{
				this._loginAck = null;
				this._fConnectionOpen = false;
			}
			base.Dispose();
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000D08C8 File Offset: 0x000CFCC8
		internal override void ValidateConnectionForExecute(SqlCommand command)
		{
			TdsParser parser = this._parser;
			if (parser == null || parser.State == TdsParserState.Broken || parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			SqlDataReader sqlDataReader = null;
			if (parser.MARSOn)
			{
				if (command != null)
				{
					sqlDataReader = base.FindLiveReader(command);
				}
			}
			else
			{
				if (this._asyncCommandCount > 0)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				sqlDataReader = base.FindLiveReader(null);
			}
			if (sqlDataReader != null)
			{
				throw ADP.OpenReaderExists();
			}
			if (!parser.MARSOn && parser._physicalStateObj._pendingData)
			{
				parser.DrainData(parser._physicalStateObj);
			}
			parser.RollbackOrphanedAPITransactions();
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000D0954 File Offset: 0x000CFD54
		internal void CheckEnlistedTransactionBinding()
		{
			Transaction enlistedTransaction = base.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				bool flag = base.ConnectionOptions.TransactionBinding == SqlConnectionString.TransactionBindingEnum.ExplicitUnbind;
				if (flag)
				{
					Transaction obj = Transaction.Current;
					if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active || !enlistedTransaction.Equals(obj))
					{
						throw ADP.TransactionConnectionMismatch();
					}
				}
				else if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					if (base.EnlistedTransactionDisposed)
					{
						base.DetachTransaction(enlistedTransaction, true);
						return;
					}
					throw ADP.TransactionCompletedButNotDisposed();
				}
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000D09CC File Offset: 0x000CFDCC
		internal override bool IsConnectionAlive(bool throwOnException)
		{
			return this._parser._physicalStateObj.IsConnectionAlive(throwOnException);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000D09F0 File Offset: 0x000CFDF0
		protected override void Activate(Transaction transaction)
		{
			this.FailoverPermissionDemand();
			if (null != transaction)
			{
				if (base.ConnectionOptions.Enlist)
				{
					base.Enlist(transaction);
					return;
				}
			}
			else
			{
				base.Enlist(null);
			}
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000D0A28 File Offset: 0x000CFE28
		protected override void InternalDeactivate()
		{
			if (this._asyncCommandCount != 0)
			{
				base.DoomThisConnection();
			}
			if (!this.IsNonPoolableTransactionRoot && this._parser != null)
			{
				this._parser.Deactivate(base.IsConnectionDoomed);
				if (!base.IsConnectionDoomed)
				{
					this.ResetConnection();
				}
			}
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000D0A74 File Offset: 0x000CFE74
		private void ResetConnection()
		{
			if (this._fResetConnection)
			{
				if (this.IsShiloh)
				{
					this._parser.PrepareResetConnection(this.IsTransactionRoot && !this.IsNonPoolableTransactionRoot);
				}
				else if (!base.IsEnlistedInTransaction)
				{
					try
					{
						Task task = this._parser.TdsExecuteSQLBatch("sp_reset_connection", 30, null, this._parser._physicalStateObj, true, false, null);
						this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						base.DoomThisConnection();
						ADP.TraceExceptionWithoutRethrow(e);
					}
				}
				base.CurrentDatabase = this._originalDatabase;
				this._currentLanguage = this._originalLanguage;
			}
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000D0B4C File Offset: 0x000CFF4C
		internal void DecrementAsyncCount()
		{
			Interlocked.Decrement(ref this._asyncCommandCount);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000D0B68 File Offset: 0x000CFF68
		internal void IncrementAsyncCount()
		{
			Interlocked.Increment(ref this._asyncCommandCount);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000D0B84 File Offset: 0x000CFF84
		internal override void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			TdsParser parser = this.Parser;
			if (parser != null)
			{
				parser.DisconnectTransaction(internalTransaction);
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000D0BA4 File Offset: 0x000CFFA4
		internal void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso)
		{
			this.ExecuteTransaction(transactionRequest, name, iso, null, false);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000D0BBC File Offset: 0x000CFFBC
		internal override void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			if (base.IsConnectionDoomed)
			{
				if (transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback)
				{
					return;
				}
				throw SQL.ConnectionDoomed();
			}
			else
			{
				if ((transactionRequest == SqlInternalConnection.TransactionRequest.Commit || transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback) && !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock)
				{
					throw SQL.ConnectionLockedForBcpEvent();
				}
				string transactionName = (name == null) ? string.Empty : name;
				if (!this._parser.IsYukonOrNewer)
				{
					this.ExecuteTransactionPreYukon(transactionRequest, transactionName, iso, internalTransaction);
					return;
				}
				this.ExecuteTransactionYukon(transactionRequest, transactionName, iso, internalTransaction, isDelegateControlRequest);
				return;
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000D0C44 File Offset: 0x000D0044
		internal void ExecuteTransactionPreYukon(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, IsolationLevel iso, SqlInternalTransaction internalTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (iso <= IsolationLevel.ReadUncommitted)
			{
				if (iso == IsolationLevel.Unspecified)
				{
					goto IL_DD;
				}
				if (iso == IsolationLevel.Chaos)
				{
					throw SQL.NotSupportedIsolationLevel(iso);
				}
				if (iso == IsolationLevel.ReadUncommitted)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
					stringBuilder.Append(";");
					goto IL_DD;
				}
			}
			else if (iso <= IsolationLevel.RepeatableRead)
			{
				if (iso == IsolationLevel.ReadCommitted)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL READ COMMITTED");
					stringBuilder.Append(";");
					goto IL_DD;
				}
				if (iso == IsolationLevel.RepeatableRead)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL REPEATABLE READ");
					stringBuilder.Append(";");
					goto IL_DD;
				}
			}
			else
			{
				if (iso == IsolationLevel.Serializable)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE");
					stringBuilder.Append(";");
					goto IL_DD;
				}
				if (iso == IsolationLevel.Snapshot)
				{
					throw SQL.SnapshotNotSupported(IsolationLevel.Snapshot);
				}
			}
			throw ADP.InvalidIsolationLevel(iso);
			IL_DD:
			if (!ADP.IsEmpty(transactionName))
			{
				transactionName = " " + SqlConnection.FixupDatabaseTransactionName(transactionName);
			}
			switch (transactionRequest)
			{
			case SqlInternalConnection.TransactionRequest.Begin:
				stringBuilder.Append("BEGIN TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.Commit:
				stringBuilder.Append("COMMIT TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.Rollback:
				stringBuilder.Append("ROLLBACK TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.IfRollback:
				stringBuilder.Append("IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.Save:
				stringBuilder.Append("SAVE TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			}
			Task task = this._parser.TdsExecuteSQLBatch(stringBuilder.ToString(), base.ConnectionOptions.ConnectTimeout, null, this._parser._physicalStateObj, true, false, null);
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
			if (transactionRequest == SqlInternalConnection.TransactionRequest.Begin)
			{
				this._parser.CurrentTransaction = internalTransaction;
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000D0E2C File Offset: 0x000D022C
		internal void ExecuteTransactionYukon(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			TdsEnums.TransactionManagerRequestType request = TdsEnums.TransactionManagerRequestType.Begin;
			if (iso <= IsolationLevel.ReadUncommitted)
			{
				if (iso == IsolationLevel.Unspecified)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.Unspecified;
					goto IL_7E;
				}
				if (iso == IsolationLevel.Chaos)
				{
					throw SQL.NotSupportedIsolationLevel(iso);
				}
				if (iso == IsolationLevel.ReadUncommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.ReadUncommitted;
					goto IL_7E;
				}
			}
			else if (iso <= IsolationLevel.RepeatableRead)
			{
				if (iso == IsolationLevel.ReadCommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.ReadCommitted;
					goto IL_7E;
				}
				if (iso == IsolationLevel.RepeatableRead)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.RepeatableRead;
					goto IL_7E;
				}
			}
			else
			{
				if (iso == IsolationLevel.Serializable)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.Serializable;
					goto IL_7E;
				}
				if (iso == IsolationLevel.Snapshot)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.Snapshot;
					goto IL_7E;
				}
			}
			throw ADP.InvalidIsolationLevel(iso);
			IL_7E:
			TdsParserStateObject tdsParserStateObject = this._parser._physicalStateObj;
			TdsParser parser = this._parser;
			bool flag = false;
			bool releaseConnectionLock = false;
			if (!this.ThreadHasParserLockForClose)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
				releaseConnectionLock = true;
			}
			try
			{
				switch (transactionRequest)
				{
				case SqlInternalConnection.TransactionRequest.Begin:
					request = TdsEnums.TransactionManagerRequestType.Begin;
					break;
				case SqlInternalConnection.TransactionRequest.Promote:
					request = TdsEnums.TransactionManagerRequestType.Promote;
					break;
				case SqlInternalConnection.TransactionRequest.Commit:
					request = TdsEnums.TransactionManagerRequestType.Commit;
					break;
				case SqlInternalConnection.TransactionRequest.Rollback:
				case SqlInternalConnection.TransactionRequest.IfRollback:
					request = TdsEnums.TransactionManagerRequestType.Rollback;
					break;
				case SqlInternalConnection.TransactionRequest.Save:
					request = TdsEnums.TransactionManagerRequestType.Save;
					break;
				}
				if ((internalTransaction != null && internalTransaction.RestoreBrokenConnection) & releaseConnectionLock)
				{
					Task task = internalTransaction.Parent.Connection.ValidateAndReconnect(delegate
					{
						this.ThreadHasParserLockForClose = false;
						this._parserLock.Release();
						releaseConnectionLock = false;
					}, 0);
					if (task != null)
					{
						AsyncHelper.WaitForCompletion(task, 0, null, true);
						internalTransaction.ConnectionHasBeenRestored = true;
						return;
					}
				}
				if (internalTransaction != null && internalTransaction.IsDelegated)
				{
					if (this._parser.MARSOn)
					{
						tdsParserStateObject = this._parser.GetSession(this);
						flag = true;
					}
					else if (internalTransaction.OpenResultsCount != 0)
					{
						throw SQL.CannotCompleteDelegatedTransactionWithOpenResults(this);
					}
				}
				TdsEnums.TransactionManagerIsolationLevel isoLevel;
				this._parser.TdsExecuteTransactionManagerRequest(null, request, transactionName, isoLevel, base.ConnectionOptions.ConnectTimeout, internalTransaction, tdsParserStateObject, isDelegateControlRequest);
			}
			finally
			{
				if (flag)
				{
					parser.PutSession(tdsParserStateObject);
				}
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000D1020 File Offset: 0x000D0420
		internal override void DelegatedTransactionEnded()
		{
			base.DelegatedTransactionEnded();
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000D1034 File Offset: 0x000D0434
		protected override byte[] GetDTCAddress()
		{
			return this._parser.GetDTCAddress(base.ConnectionOptions.ConnectTimeout, this._parser.GetSession(this));
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000D1068 File Offset: 0x000D0468
		protected override void PropagateTransactionCookie(byte[] cookie)
		{
			this._parser.PropagateDistributedTransaction(cookie, base.ConnectionOptions.ConnectTimeout, this._parser._physicalStateObj);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000D1098 File Offset: 0x000D0498
		private void CompleteLogin(bool enlistOK)
		{
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
			if (this._routingInfo == null)
			{
				if (this._federatedAuthenticationRequested && !this._federatedAuthenticationAcknowledged)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.CompleteLogin|ERR> %d#, Server did not acknowledge the federated authentication request\n", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.FedAuthNotAcknowledged);
				}
				if (this._federatedAuthenticationInfoRequested && !this._federatedAuthenticationInfoReceived)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.CompleteLogin|ERR> %d#, Server never sent the requested federated authentication info\n", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.FedAuthInfoNotReceived);
				}
				if (!this._sessionRecoveryAcknowledged)
				{
					this._currentSessionData = null;
					if (this._recoverySessionData != null)
					{
						throw SQL.CR_NoCRAckAtReconnection(this);
					}
				}
				if (this._currentSessionData != null && this._recoverySessionData == null)
				{
					this._currentSessionData._initialDatabase = base.CurrentDatabase;
					this._currentSessionData._initialCollation = this._currentSessionData._collation;
					this._currentSessionData._initialLanguage = this._currentLanguage;
				}
				bool flag = this._parser.EncryptionOptions == EncryptionOptions.ON;
				if (this._recoverySessionData != null && this._recoverySessionData._encrypted != flag)
				{
					throw SQL.CR_EncryptionChanged(this);
				}
				if (this._currentSessionData != null)
				{
					this._currentSessionData._encrypted = flag;
				}
				this._recoverySessionData = null;
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_EnableMars;
			this._parser.EnableMars();
			this._fConnectionOpen = true;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionTds.CompleteLogin|ADV> Post-Login Phase: Server connection obtained.\n");
			}
			if (enlistOK && base.ConnectionOptions.Enlist)
			{
				this._parser._physicalStateObj.SniContext = SniContext.Snix_AutoEnlist;
				Transaction currentTransaction = ADP.GetCurrentTransaction();
				base.Enlist(currentTransaction);
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000D123C File Offset: 0x000D063C
		private void Login(ServerInfo server, TimeoutTimer timeout, string newPassword, SecureString newSecurePassword)
		{
			SqlLogin sqlLogin = new SqlLogin();
			base.CurrentDatabase = server.ResolvedDatabaseName;
			this._currentPacketSize = base.ConnectionOptions.PacketSize;
			this._currentLanguage = base.ConnectionOptions.CurrentLanguage;
			int timeout2 = 0;
			if (!timeout.IsInfinite)
			{
				long num = timeout.MillisecondsRemaining / 1000L;
				if (num == 0L && LocalAppContextSwitches.UseMinimumLoginTimeout)
				{
					num = 1L;
				}
				if (2147483647L > num)
				{
					timeout2 = (int)num;
				}
			}
			sqlLogin.authentication = base.ConnectionOptions.Authentication;
			sqlLogin.timeout = timeout2;
			sqlLogin.userInstance = base.ConnectionOptions.UserInstance;
			sqlLogin.hostName = base.ConnectionOptions.ObtainWorkstationId();
			sqlLogin.userName = base.ConnectionOptions.UserID;
			sqlLogin.password = base.ConnectionOptions.Password;
			sqlLogin.applicationName = base.ConnectionOptions.ApplicationName;
			sqlLogin.language = this._currentLanguage;
			if (!sqlLogin.userInstance)
			{
				sqlLogin.database = base.CurrentDatabase;
				sqlLogin.attachDBFilename = base.ConnectionOptions.AttachDBFilename;
			}
			sqlLogin.serverName = server.UserServerName;
			sqlLogin.useReplication = base.ConnectionOptions.Replication;
			sqlLogin.useSSPI = (base.ConnectionOptions.IntegratedSecurity || (base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated && !this._fedAuthRequired));
			sqlLogin.packetSize = this._currentPacketSize;
			sqlLogin.newPassword = newPassword;
			sqlLogin.readOnlyIntent = (base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly);
			sqlLogin.credential = this._credential;
			if (newSecurePassword != null)
			{
				sqlLogin.newSecurePassword = newSecurePassword;
			}
			TdsEnums.FeatureExtension featureExtension = TdsEnums.FeatureExtension.None;
			if (base.ConnectionOptions.ConnectRetryCount > 0)
			{
				featureExtension |= TdsEnums.FeatureExtension.SessionRecovery;
				this._sessionRecoveryRequested = true;
			}
			if (base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryPassword || base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive || (base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated && this._fedAuthRequired))
			{
				featureExtension |= TdsEnums.FeatureExtension.FedAuth;
				this._federatedAuthenticationInfoRequested = true;
				this._fedAuthFeatureExtensionData = new FederatedAuthenticationFeatureExtensionData?(new FederatedAuthenticationFeatureExtensionData
				{
					libraryType = TdsEnums.FedAuthLibrary.ADAL,
					authentication = base.ConnectionOptions.Authentication,
					fedAuthRequiredPreLoginResponse = this._fedAuthRequired
				});
			}
			if (this._accessTokenInBytes != null)
			{
				featureExtension |= TdsEnums.FeatureExtension.FedAuth;
				this._fedAuthFeatureExtensionData = new FederatedAuthenticationFeatureExtensionData?(new FederatedAuthenticationFeatureExtensionData
				{
					libraryType = TdsEnums.FedAuthLibrary.SecurityToken,
					fedAuthRequiredPreLoginResponse = this._fedAuthRequired,
					accessToken = this._accessTokenInBytes
				});
				this._federatedAuthenticationRequested = true;
			}
			featureExtension |= (TdsEnums.FeatureExtension.Tce | TdsEnums.FeatureExtension.GlobalTransactions);
			if (base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly)
			{
				featureExtension |= TdsEnums.FeatureExtension.AzureSQLSupport;
			}
			this._parser.TdsLogin(sqlLogin, featureExtension, this._recoverySessionData, this._fedAuthFeatureExtensionData);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000D14E8 File Offset: 0x000D08E8
		private void LoginFailure()
		{
			Bid.Trace("<sc.SqlInternalConnectionTds.LoginFailure|RES|CPOOL> %d#\n", base.ObjectID);
			if (this._parser != null)
			{
				this._parser.Disconnect();
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000D1518 File Offset: 0x000D0918
		private void OpenLoginEnlist(TimeoutTimer timeout, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance)
		{
			ServerInfo serverInfo = new ServerInfo(connectionOptions);
			bool flag;
			string failoverPartner;
			if (this.PoolGroupProviderInfo != null)
			{
				flag = this.PoolGroupProviderInfo.UseFailoverPartner;
				failoverPartner = this.PoolGroupProviderInfo.FailoverPartner;
			}
			else
			{
				flag = false;
				failoverPartner = base.ConnectionOptions.FailoverPartner;
			}
			this.timeoutErrorInternal.SetInternalSourceType(flag ? SqlConnectionInternalSourceType.Failover : SqlConnectionInternalSourceType.Principle);
			bool flag2 = !ADP.IsEmpty(failoverPartner);
			try
			{
				this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
				if (flag2)
				{
					this.timeoutErrorInternal.SetFailoverScenario(true);
					this.LoginWithFailover(flag, serverInfo, failoverPartner, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				else
				{
					this.timeoutErrorInternal.SetFailoverScenario(false);
					this.LoginNoFailover(serverInfo, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				if (!base.IsAzureSQLConnection && base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly)
				{
					if (!string.IsNullOrEmpty(base.ConnectionOptions.FailoverPartner))
					{
						throw SQL.ROR_FailoverNotSupportedConnString();
					}
					if (this.ServerProvidedFailOverPartner != null)
					{
						throw SQL.ROR_FailoverNotSupportedServer(this);
					}
				}
				this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					this.LoginFailure();
				}
				throw;
			}
			this.timeoutErrorInternal.SetAllCompleteMarker();
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000D164C File Offset: 0x000D0A4C
		private bool IsDoNotRetryConnectError(SqlException exc)
		{
			return 18456 == exc.Number || 18488 == exc.Number || 1346 == exc.Number || exc._doNotReconnect;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000D1688 File Offset: 0x000D0A88
		private void LoginNoFailover(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			int num = 0;
			ServerInfo serverInfo2 = serverInfo;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionTds.LoginNoFailover|ADV> %d#, host=%ls\n", base.ObjectID, serverInfo.UserServerName);
			}
			int num2 = 100;
			this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance, connectionOptions);
			bool flag = this.ShouldDisableTnir(connectionOptions);
			long num3 = 0L;
			bool flag2 = connectionOptions.MultiSubnetFailover || (connectionOptions.TransparentNetworkIPResolution && !flag);
			int num5;
			TimeoutTimer timeoutTimer;
			TimeoutTimer timeout2;
			checked
			{
				if (flag2)
				{
					float num4 = connectionOptions.MultiSubnetFailover ? 0.08f : 0.125f;
					if (timeout.IsInfinite)
					{
						num3 = (long)(unchecked(num4 * 15000f));
					}
					else
					{
						num3 = (long)(unchecked(num4 * (float)timeout.MillisecondsRemaining));
					}
				}
				num5 = 0;
				timeoutTimer = null;
				timeout2 = timeout;
			}
			for (;;)
			{
				bool flag3 = connectionOptions.TransparentNetworkIPResolution && !flag && num5 == 1;
				if (flag2)
				{
					int num6;
					num5 = (num6 = num5 + 1);
					if (connectionOptions.TransparentNetworkIPResolution)
					{
						num6 = 1 << num5 - 1;
					}
					long num7 = checked(num3 * unchecked((long)num6));
					long millisecondsRemaining = timeout.MillisecondsRemaining;
					if (flag3)
					{
						num7 = Math.Max(500L, num7);
					}
					if (num7 > millisecondsRemaining)
					{
						num7 = millisecondsRemaining;
					}
					timeoutTimer = TimeoutTimer.StartMillisecondsTimeout(num7);
				}
				if (this._parser != null)
				{
					this._parser.Disconnect();
				}
				this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
				try
				{
					if (flag2)
					{
						timeout2 = timeoutTimer;
					}
					this.AttemptOneLogin(serverInfo, newPassword, newSecurePassword, !flag2, timeout2, false, flag3, flag);
					if (connectionOptions.MultiSubnetFailover && this.ServerProvidedFailOverPartner != null)
					{
						throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
					}
					if (this._routingInfo == null)
					{
						goto IL_338;
					}
					Bid.Trace("<sc.SqlInternalConnectionTds.LoginNoFailover> Routed to %ls", serverInfo.ExtendedServerName);
					if (num > 10)
					{
						throw SQL.ROR_RecursiveRoutingNotSupported(this);
					}
					if (timeout.IsExpired)
					{
						throw SQL.ROR_TimeoutAfterRoutingInfo(this);
					}
					serverInfo = new ServerInfo(base.ConnectionOptions, this._routingInfo, serverInfo.ResolvedServerName);
					this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.RoutingDestination);
					this._originalClientConnectionId = this._clientConnectionId;
					this._routingDestination = serverInfo.UserServerName;
					this._currentPacketSize = base.ConnectionOptions.PacketSize;
					this._currentLanguage = (this._originalLanguage = base.ConnectionOptions.CurrentLanguage);
					base.CurrentDatabase = (this._originalDatabase = base.ConnectionOptions.InitialCatalog);
					this._currentFailoverPartner = null;
					this._instanceName = string.Empty;
					num++;
					continue;
				}
				catch (SqlException ex)
				{
					if (this.AttemptRetryADAuthWithTimeoutError(ex, connectionOptions, timeout))
					{
						continue;
					}
					if (this._parser == null || this._parser.State != TdsParserState.Closed || this.IsDoNotRetryConnectError(ex) || timeout.IsExpired)
					{
						throw;
					}
					if (timeout.MillisecondsRemaining <= (long)num2)
					{
						throw;
					}
				}
				if (this.ServerProvidedFailOverPartner != null)
				{
					break;
				}
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.LoginNoFailover|ADV> %d#, sleeping %d{milisec}\n", base.ObjectID, num2);
				}
				Thread.Sleep(num2);
				num2 = ((num2 < 500) ? (num2 * 2) : 1000);
			}
			if (connectionOptions.MultiSubnetFailover)
			{
				throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
			}
			this.timeoutErrorInternal.ResetAndRestartPhase();
			this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
			this.timeoutErrorInternal.SetFailoverScenario(true);
			this.LoginWithFailover(true, serverInfo, this.ServerProvidedFailOverPartner, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
			return;
			IL_338:
			this._activeDirectoryAuthTimeoutRetryHelper.State = ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn;
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverCheck(this, false, connectionOptions, this.ServerProvidedFailOverPartner);
			}
			base.CurrentDataSource = serverInfo2.UserServerName;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000D1A20 File Offset: 0x000D0E20
		private bool ShouldDisableTnir(SqlConnectionString connectionOptions)
		{
			bool flag = ADP.IsAzureSqlServerEndpoint(connectionOptions.DataSource);
			bool flag2 = this._accessTokenInBytes != null || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryPassword || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive;
			return connectionOptions.Parsetable["transparentnetworkipresolution"] == null && (flag || flag2);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000D1A7C File Offset: 0x000D0E7C
		private bool AttemptRetryADAuthWithTimeoutError(SqlException sqlex, SqlConnectionString connectionOptions, TimeoutTimer timeout)
		{
			if (!this._activeDirectoryAuthTimeoutRetryHelper.CanRetryWithSqlException(sqlex))
			{
				return false;
			}
			timeout.Reset();
			this._dbConnectionPoolAuthenticationContextKey = null;
			base.UnDoomThisConnection();
			this._activeDirectoryAuthTimeoutRetryHelper.State = ActiveDirectoryAuthenticationTimeoutRetryState.Retrying;
			return true;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000D1ABC File Offset: 0x000D0EBC
		private void LoginWithFailover(bool useFailoverHost, ServerInfo primaryServerInfo, string failoverHost, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionTds.LoginWithFailover|ADV> %d#, useFailover=%d{bool}, primary=", base.ObjectID, useFailoverHost);
				Bid.PutStr(primaryServerInfo.UserServerName);
				Bid.PutStr(", failover=");
				Bid.PutStr(failoverHost);
				Bid.PutStr("\n");
			}
			int num = 100;
			string networkLibrary = base.ConnectionOptions.NetworkLibrary;
			ServerInfo serverInfo = new ServerInfo(connectionOptions, failoverHost);
			this.ResolveExtendedServerName(primaryServerInfo, !redirectedUserInstance, connectionOptions);
			if (this.ServerProvidedFailOverPartner == null)
			{
				this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance && failoverHost != primaryServerInfo.UserServerName, connectionOptions);
			}
			long num2;
			bool flag;
			int num3;
			checked
			{
				if (timeout.IsInfinite)
				{
					num2 = (long)(unchecked(0.08f * (float)ADP.TimerFromSeconds(15)));
				}
				else
				{
					num2 = (long)(unchecked(0.08f * (float)timeout.MillisecondsRemaining));
				}
				flag = false;
				num3 = 0;
			}
			for (;;)
			{
				long num4 = checked(num2 * unchecked((long)(checked(num3 / 2 + 1))));
				long millisecondsRemaining = timeout.MillisecondsRemaining;
				if (num4 > millisecondsRemaining)
				{
					num4 = millisecondsRemaining;
				}
				TimeoutTimer timeout2 = TimeoutTimer.StartMillisecondsTimeout(num4);
				if (this._parser != null)
				{
					this._parser.Disconnect();
				}
				this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
				ServerInfo serverInfo2;
				if (useFailoverHost)
				{
					if (!flag)
					{
						this.FailoverPermissionDemand();
						flag = true;
					}
					if (this.ServerProvidedFailOverPartner != null && serverInfo.ResolvedServerName != this.ServerProvidedFailOverPartner)
					{
						if (Bid.AdvancedOn)
						{
							Bid.Trace("<sc.SqlInternalConnectionTds.LoginWithFailover|ADV> %d#, new failover partner=%ls\n", base.ObjectID, this.ServerProvidedFailOverPartner);
						}
						serverInfo.SetDerivedNames(networkLibrary, this.ServerProvidedFailOverPartner);
					}
					serverInfo2 = serverInfo;
					this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
				}
				else
				{
					serverInfo2 = primaryServerInfo;
					this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Principle);
				}
				try
				{
					this.AttemptOneLogin(serverInfo2, newPassword, newSecurePassword, false, timeout2, true, true, false);
					int num5 = 0;
					while (this._routingInfo != null)
					{
						if (num5 > 10)
						{
							throw SQL.ROR_RecursiveRoutingNotSupported(this);
						}
						num5++;
						Bid.Trace("<sc.SqlInternalConnectionTds.LoginWithFailover> Routed to %ls", this._routingInfo.ServerName);
						if (this._parser != null)
						{
							this._parser.Disconnect();
						}
						this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
						serverInfo2 = new ServerInfo(base.ConnectionOptions, this._routingInfo, serverInfo2.ResolvedServerName);
						this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.RoutingDestination);
						this._originalClientConnectionId = this._clientConnectionId;
						this._routingDestination = serverInfo2.UserServerName;
						this._currentPacketSize = base.ConnectionOptions.PacketSize;
						this._currentLanguage = (this._originalLanguage = base.ConnectionOptions.CurrentLanguage);
						base.CurrentDatabase = (this._originalDatabase = base.ConnectionOptions.InitialCatalog);
						this._currentFailoverPartner = null;
						this._instanceName = string.Empty;
						this.AttemptOneLogin(serverInfo2, newPassword, newSecurePassword, false, timeout2, true, true, false);
					}
					break;
				}
				catch (SqlException ex)
				{
					if (this.AttemptRetryADAuthWithTimeoutError(ex, connectionOptions, timeout))
					{
						continue;
					}
					if (this.IsDoNotRetryConnectError(ex) || timeout.IsExpired)
					{
						throw;
					}
					if (!ADP.IsAzureSqlServerEndpoint(connectionOptions.DataSource) && base.IsConnectionDoomed)
					{
						throw;
					}
					if (1 == num3 % 2 && timeout.MillisecondsRemaining <= (long)num)
					{
						throw;
					}
				}
				if (1 == num3 % 2)
				{
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.SqlInternalConnectionTds.LoginWithFailover|ADV> %d#, sleeping %d{milisec}\n", base.ObjectID, num);
					}
					Thread.Sleep(num);
					num = ((num < 500) ? (num * 2) : 1000);
				}
				num3++;
				useFailoverHost = !useFailoverHost;
			}
			this._activeDirectoryAuthTimeoutRetryHelper.State = ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn;
			if (useFailoverHost && this.ServerProvidedFailOverPartner == null)
			{
				throw SQL.InvalidPartnerConfiguration(failoverHost, base.CurrentDatabase);
			}
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverCheck(this, useFailoverHost, connectionOptions, this.ServerProvidedFailOverPartner);
			}
			base.CurrentDataSource = (useFailoverHost ? failoverHost : primaryServerInfo.UserServerName);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000D1E8C File Offset: 0x000D128C
		private void ResolveExtendedServerName(ServerInfo serverInfo, bool aliasLookup, SqlConnectionString options)
		{
			if (serverInfo.ExtendedServerName == null)
			{
				string text = serverInfo.UserServerName;
				string text2 = serverInfo.UserProtocol;
				if (aliasLookup)
				{
					if (this._currentSessionData != null && !string.IsNullOrEmpty(text))
					{
						Tuple<string, string> tuple;
						if (this._currentSessionData._resolvedAliases.TryGetValue(text, out tuple))
						{
							text = tuple.Item1;
							text2 = tuple.Item2;
						}
						else
						{
							TdsParserStaticMethods.AliasRegistryLookup(ref text, ref text2);
							this._currentSessionData._resolvedAliases.Add(serverInfo.UserServerName, new Tuple<string, string>(text, text2));
						}
					}
					else
					{
						TdsParserStaticMethods.AliasRegistryLookup(ref text, ref text2);
					}
					if (options.EnforceLocalHost)
					{
						SqlConnectionString.VerifyLocalHostAndFixup(ref text, true, true);
					}
				}
				serverInfo.SetDerivedNames(text2, text);
			}
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000D1F38 File Offset: 0x000D1338
		private void AttemptOneLogin(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool ignoreSniOpenTimeout, TimeoutTimer timeout, bool withFailover = false, bool isFirstTransparentAttempt = true, bool disableTnir = false)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionTds.AttemptOneLogin|ADV> %d#, timout=%I64d{msec}, server=", base.ObjectID, timeout.MillisecondsRemaining);
				Bid.PutStr(serverInfo.ExtendedServerName);
				Bid.Trace("\n");
			}
			this._routingInfo = null;
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Connect;
			this._parser.Connect(serverInfo, this, ignoreSniOpenTimeout, timeout.LegacyTimerExpire, base.ConnectionOptions.Encrypt, base.ConnectionOptions.TrustServerCertificate, base.ConnectionOptions.IntegratedSecurity, withFailover, isFirstTransparentAttempt, base.ConnectionOptions.Authentication, disableTnir, this._sqlAuthenticationProviderManager);
			this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake);
			this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.LoginBegin);
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
			this.Login(serverInfo, timeout, newPassword, newSecurePassword);
			this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth);
			this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			this.CompleteLogin(!base.ConnectionOptions.Pooling);
			this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000D204C File Offset: 0x000D144C
		internal void FailoverPermissionDemand()
		{
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverPermissionDemand();
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000D206C File Offset: 0x000D146C
		protected override object ObtainAdditionalLocksForClose()
		{
			bool flag = !this.ThreadHasParserLockForClose;
			if (flag)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
			}
			return flag;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000D20A0 File Offset: 0x000D14A0
		protected override void ReleaseAdditionalLocksForClose(object lockToken)
		{
			if ((bool)lockToken)
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000D20C8 File Offset: 0x000D14C8
		internal bool GetSessionAndReconnectIfNeeded(SqlConnection parent, int timeout = 0)
		{
			if (this.ThreadHasParserLockForClose)
			{
				return false;
			}
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			bool releaseConnectionLock = true;
			bool result;
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Task task = parent.ValidateAndReconnect(delegate
					{
						this.ThreadHasParserLockForClose = false;
						this._parserLock.Release();
						releaseConnectionLock = false;
					}, timeout);
					if (task != null)
					{
						AsyncHelper.WaitForCompletion(task, timeout, null, true);
						result = true;
					}
					else
					{
						result = false;
					}
				}
				catch (OutOfMemoryException)
				{
					base.DoomThisConnection();
					throw;
				}
				catch (StackOverflowException)
				{
					base.DoomThisConnection();
					throw;
				}
				catch (ThreadAbortException)
				{
					base.DoomThisConnection();
					throw;
				}
			}
			finally
			{
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
			return result;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000D21D8 File Offset: 0x000D15D8
		internal void BreakConnection()
		{
			SqlConnection connection = base.Connection;
			Bid.Trace("<sc.SqlInternalConnectionTds.BreakConnection|RES|CPOOL> %d#, Breaking connection.\n", base.ObjectID);
			base.DoomThisConnection();
			if (connection != null)
			{
				connection.Close();
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000D220C File Offset: 0x000D160C
		internal bool IgnoreEnvChange
		{
			get
			{
				return this._routingInfo != null;
			}
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000D2224 File Offset: 0x000D1624
		internal void OnEnvChange(SqlEnvChange rec)
		{
			switch (rec.type)
			{
			case 1:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalDatabase = rec.newValue;
				}
				base.CurrentDatabase = rec.newValue;
				return;
			case 2:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalLanguage = rec.newValue;
				}
				this._currentLanguage = rec.newValue;
				return;
			case 3:
			case 5:
			case 6:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 14:
			case 16:
			case 17:
				break;
			case 4:
				this._currentPacketSize = int.Parse(rec.newValue, CultureInfo.InvariantCulture);
				return;
			case 7:
				if (this._currentSessionData != null)
				{
					this._currentSessionData._collation = rec.newCollation;
					return;
				}
				break;
			case 13:
				this._currentFailoverPartner = rec.newValue;
				return;
			case 15:
				base.PromotedDTCToken = rec.newBinValue;
				return;
			case 18:
				if (this._currentSessionData != null)
				{
					this._currentSessionData.Reset();
					return;
				}
				break;
			case 19:
				this._instanceName = rec.newValue;
				return;
			case 20:
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnEnvChange> %d#, Received routing info\n", base.ObjectID);
				}
				if (string.IsNullOrEmpty(rec.newRoutingInfo.ServerName) || rec.newRoutingInfo.Protocol != 0 || rec.newRoutingInfo.Port == 0)
				{
					throw SQL.ROR_InvalidRoutingInfo(this);
				}
				this._routingInfo = rec.newRoutingInfo;
				break;
			default:
				return;
			}
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000D23A8 File Offset: 0x000D17A8
		internal void OnLoginAck(SqlLoginAck rec)
		{
			this._loginAck = rec;
			if (this._recoverySessionData != null && this._recoverySessionData._tdsVersion != rec.tdsVersion)
			{
				throw SQL.CR_TDSVersionNotPreserved(this);
			}
			if (this._currentSessionData != null)
			{
				this._currentSessionData._tdsVersion = rec.tdsVersion;
			}
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000D23F8 File Offset: 0x000D17F8
		internal void OnFedAuthInfo(SqlFedAuthInfo fedAuthInfo)
		{
			Bid.Trace("<sc.SqlInternalConnectionTds.OnFedAuthInfo> %d#, Generating federated authentication token\n", base.ObjectID);
			DbConnectionPoolAuthenticationContext dbConnectionPoolAuthenticationContext = null;
			bool flag = false;
			bool flag2 = false;
			SqlFedAuthToken sqlFedAuthToken = null;
			if (this._dbConnectionPool != null)
			{
				this._dbConnectionPoolAuthenticationContextKey = new DbConnectionPoolAuthenticationContextKey(fedAuthInfo.stsurl, fedAuthInfo.spn);
				if (this._dbConnectionPool.AuthenticationContexts.TryGetValue(this._dbConnectionPoolAuthenticationContextKey, out dbConnectionPoolAuthenticationContext))
				{
					TimeSpan t = dbConnectionPoolAuthenticationContext.ExpirationTime.Subtract(DateTime.UtcNow);
					if (t <= SqlInternalConnectionTds._dbAuthenticationContextUnLockedRefreshTimeSpan)
					{
						Bid.Trace("<sc.SqlInternalConnectionTds.OnFedAuthInfo> %d#, The expiration time is less than 10 mins, so trying to get new access token regardless of if an other thread is also trying to update it.The expiration time is %s. Current Time is %s.\n", base.ObjectID, dbConnectionPoolAuthenticationContext.ExpirationTime.ToLongTimeString(), DateTime.UtcNow.ToLongTimeString());
						flag = true;
					}
					else if (t <= SqlInternalConnectionTds._dbAuthenticationContextLockedRefreshTimeSpan)
					{
						if (Bid.AdvancedOn)
						{
							Bid.Trace("<sc.SqlInternalConnectionTds.OnFedAuthInfo> %d#, The authentication context needs a refresh.The expiration time is %s. Current Time is %s.\n", base.ObjectID, dbConnectionPoolAuthenticationContext.ExpirationTime.ToLongTimeString(), DateTime.UtcNow.ToLongTimeString());
						}
						flag2 = this.TryGetFedAuthTokenLocked(fedAuthInfo, dbConnectionPoolAuthenticationContext, out sqlFedAuthToken);
						if (flag2)
						{
							Bid.Trace("<sc.SqlInternalConnectionTds.OnFedAuthInfo> %d#, The attempt to get a new access token succeeded under the locked mode.");
						}
					}
					else if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.SqlInternalConnectionTds.OnFedAuthInfo> %d#, Found an authentication context in the cache that does not need a refresh at this time. Re-using the cached token.\n", base.ObjectID);
					}
				}
			}
			if (dbConnectionPoolAuthenticationContext == null || flag)
			{
				sqlFedAuthToken = this.GetFedAuthToken(fedAuthInfo);
				if (this._dbConnectionPool != null)
				{
				}
			}
			else if (!flag2)
			{
				sqlFedAuthToken = new SqlFedAuthToken();
				sqlFedAuthToken.accessToken = dbConnectionPoolAuthenticationContext.AccessToken;
			}
			this._parser.SendFedAuthToken(sqlFedAuthToken);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000D2560 File Offset: 0x000D1960
		internal bool TryGetFedAuthTokenLocked(SqlFedAuthInfo fedAuthInfo, DbConnectionPoolAuthenticationContext dbConnectionPoolAuthenticationContext, out SqlFedAuthToken fedAuthToken)
		{
			fedAuthToken = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (dbConnectionPoolAuthenticationContext.LockToUpdate())
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.TryGetFedAuthTokenLocked> %d#, Acquired the lock to update the authentication context.The expiration time is %s. Current Time is %s.\n", base.ObjectID, dbConnectionPoolAuthenticationContext.ExpirationTime.ToLongTimeString(), DateTime.UtcNow.ToLongTimeString());
					flag = true;
				}
				else
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.TryGetFedAuthTokenLocked> %d#, Refreshing the context is already in progress by another thread.\n", base.ObjectID);
				}
				if (flag)
				{
					fedAuthToken = this.GetFedAuthToken(fedAuthInfo);
				}
			}
			finally
			{
				if (flag)
				{
					dbConnectionPoolAuthenticationContext.ReleaseLockToUpdate();
				}
			}
			return flag;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000D25F4 File Offset: 0x000D19F4
		internal SqlFedAuthToken GetFedAuthToken(SqlFedAuthInfo fedAuthInfo)
		{
			int num = 100;
			int num2 = 0;
			SqlFedAuthToken fedAuthToken = new SqlFedAuthToken();
			string text = null;
			SqlAuthenticationProvider authProvider = this._sqlAuthenticationProviderManager.GetProvider(base.ConnectionOptions.Authentication);
			if (authProvider == null)
			{
				throw SQL.CannotFindAuthProvider(base.ConnectionOptions.Authentication.ToString());
			}
			for (;;)
			{
				num2++;
				try
				{
					SqlAuthenticationParameters.Builder authParamsBuilder = new SqlAuthenticationParameters.Builder(base.ConnectionOptions.Authentication, fedAuthInfo.spn, fedAuthInfo.stsurl, base.ConnectionOptions.DataSource, base.ConnectionOptions.InitialCatalog).WithConnectionId(this._clientConnectionId);
					switch (base.ConnectionOptions.Authentication)
					{
					case SqlAuthenticationMethod.ActiveDirectoryPassword:
						if (this._activeDirectoryAuthTimeoutRetryHelper.State == ActiveDirectoryAuthenticationTimeoutRetryState.Retrying)
						{
							fedAuthToken = this._activeDirectoryAuthTimeoutRetryHelper.CachedToken;
						}
						else
						{
							if (this._credential != null)
							{
								text = this._credential.UserId;
								authParamsBuilder.WithUserId(text).WithPassword(this._credential.Password);
								Task.Run<SqlFedAuthToken>(() => fedAuthToken = authProvider.AcquireTokenAsync(authParamsBuilder).Result.ToSqlFedAuthToken()).Wait();
							}
							else
							{
								text = base.ConnectionOptions.UserID;
								authParamsBuilder.WithUserId(text).WithPassword(base.ConnectionOptions.Password);
								Task.Run<SqlFedAuthToken>(() => fedAuthToken = authProvider.AcquireTokenAsync(authParamsBuilder).Result.ToSqlFedAuthToken()).Wait();
							}
							this._activeDirectoryAuthTimeoutRetryHelper.CachedToken = fedAuthToken;
						}
						break;
					case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
						text = "NT Authority\\Anonymous Logon";
						if (this._activeDirectoryAuthTimeoutRetryHelper.State == ActiveDirectoryAuthenticationTimeoutRetryState.Retrying)
						{
							fedAuthToken = this._activeDirectoryAuthTimeoutRetryHelper.CachedToken;
						}
						else
						{
							Task.Run<SqlFedAuthToken>(() => fedAuthToken = authProvider.AcquireTokenAsync(authParamsBuilder).Result.ToSqlFedAuthToken()).Wait();
							this._activeDirectoryAuthTimeoutRetryHelper.CachedToken = fedAuthToken;
						}
						break;
					case SqlAuthenticationMethod.ActiveDirectoryInteractive:
						if (this._activeDirectoryAuthTimeoutRetryHelper.State == ActiveDirectoryAuthenticationTimeoutRetryState.Retrying)
						{
							fedAuthToken = this._activeDirectoryAuthTimeoutRetryHelper.CachedToken;
						}
						else
						{
							authParamsBuilder.WithUserId(base.ConnectionOptions.UserID);
							Task.Run<SqlFedAuthToken>(() => fedAuthToken = authProvider.AcquireTokenAsync(authParamsBuilder).Result.ToSqlFedAuthToken()).Wait();
							this._activeDirectoryAuthTimeoutRetryHelper.CachedToken = fedAuthToken;
						}
						break;
					default:
						throw new InvalidOperationException(string.Format("Failed to get a token with unsupported auth method {0}.", base.ConnectionOptions.Authentication));
					}
				}
				catch (AdalException ex)
				{
					uint category = ex.GetCategory();
					if (2U != category || this._timeout.IsExpired || this._timeout.MillisecondsRemaining <= (long)num)
					{
						string text2 = ex.GetStatus().ToString("X");
						Bid.Trace("<sc.SqlInternalConnectionTds.GetFedAuthToken.ADALException category:> %d#  <error:> %s#\n", (int)category, text2);
						SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
						sqlErrorCollection.Add(new SqlError(0, 0, 11, base.ConnectionOptions.DataSource, Res.GetString("SQL_ADALFailure", new object[]
						{
							text,
							base.ConnectionOptions.Authentication.ToString("G")
						}), "ADALGetAccessToken", 0));
						string @string = Res.GetString("SQL_ADALInnerException", new object[]
						{
							text2,
							ex.GetState()
						});
						sqlErrorCollection.Add(new SqlError(0, 0, 11, base.ConnectionOptions.DataSource, @string, "ADALGetAccessToken", 0));
						if (!string.IsNullOrEmpty(ex.Message))
						{
							sqlErrorCollection.Add(new SqlError(0, 0, 11, base.ConnectionOptions.DataSource, ex.Message, "ADALGetAccessToken", 0));
						}
						SqlException ex2 = SqlException.CreateException(sqlErrorCollection, "", this, null);
						throw ex2;
					}
					Bid.Trace("<sc.SqlInternalConnectionTds.GetFedAuthToken|ADV> %d#, sleeping %d{Milliseconds}\n", base.ObjectID, num);
					Bid.Trace("<sc.SqlInternalConnectionTds.GetFedAuthToken|ADV> %d#, remaining %d{Milliseconds}\n", base.ObjectID, this._timeout.MillisecondsRemaining);
					Thread.Sleep(num);
					num *= 2;
					continue;
				}
				break;
			}
			if (this._dbConnectionPool != null)
			{
				DateTime expirationTime = DateTime.FromFileTimeUtc(fedAuthToken.expirationFileTime);
				this._newDbConnectionPoolAuthenticationContext = new DbConnectionPoolAuthenticationContext(fedAuthToken.accessToken, expirationTime);
			}
			Bid.Trace("<sc.SqlInternalConnectionTds.GetFedAuthToken> %d#, Finished generating federated authentication token.\n", base.ObjectID);
			return fedAuthToken;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000D2A78 File Offset: 0x000D1E78
		internal void OnFeatureExtAck(int featureId, byte[] data)
		{
			if (this._routingInfo != null)
			{
				return;
			}
			switch (featureId)
			{
			case 1:
			{
				if (!this._sessionRecoveryRequested)
				{
					throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnrequestedFeatureAckReceived, featureId);
				}
				this._sessionRecoveryAcknowledged = true;
				int i = 0;
				while (i < data.Length)
				{
					byte b = data[i];
					i++;
					byte b2 = data[i];
					i++;
					int num;
					if (b2 == 255)
					{
						num = BitConverter.ToInt32(data, i);
						i += 4;
					}
					else
					{
						num = (int)b2;
					}
					byte[] array = new byte[num];
					Buffer.BlockCopy(data, i, array, 0, num);
					i += num;
					if (this._recoverySessionData == null)
					{
						this._currentSessionData._initialState[(int)b] = array;
					}
					else
					{
						this._currentSessionData._delta[(int)b] = new SessionStateRecord
						{
							_data = array,
							_dataLength = num,
							_recoverable = true,
							_version = 0U
						};
						this._currentSessionData._deltaDirty = true;
					}
				}
				return;
			}
			case 2:
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck> %d#, Received feature extension acknowledgement for federated authentication\n", base.ObjectID);
				}
				if (!this._federatedAuthenticationRequested)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> %d#, Did not request federated authentication\n", base.ObjectID);
					throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnrequestedFeatureAckReceived, featureId);
				}
				TdsEnums.FedAuthLibrary libraryType = this._fedAuthFeatureExtensionData.Value.libraryType;
				if (libraryType - TdsEnums.FedAuthLibrary.SecurityToken > 1)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> %d#, Attempting to use unknown federated authentication library\n", base.ObjectID);
					throw SQL.ParsingErrorLibraryType(ParsingErrorState.FedAuthFeatureAckUnknownLibraryType, (int)this._fedAuthFeatureExtensionData.Value.libraryType);
				}
				if (data.Length != 0)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> %d#, Federated authentication feature extension ack for ADAL and Security Token includes extra data\n", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.FedAuthFeatureAckContainsExtraData);
				}
				this._federatedAuthenticationAcknowledged = true;
				if (this._newDbConnectionPoolAuthenticationContext != null)
				{
					DbConnectionPoolAuthenticationContext dbConnectionPoolAuthenticationContext = this._dbConnectionPool.AuthenticationContexts.AddOrUpdate(this._dbConnectionPoolAuthenticationContextKey, this._newDbConnectionPoolAuthenticationContext, (DbConnectionPoolAuthenticationContextKey key, DbConnectionPoolAuthenticationContext oldValue) => DbConnectionPoolAuthenticationContext.ChooseAuthenticationContextToUpdate(oldValue, this._newDbConnectionPoolAuthenticationContext));
					return;
				}
				return;
			}
			case 4:
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck> %d#, Received feature extension acknowledgement for TCE\n", base.ObjectID);
				}
				if (data.Length < 1)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> %d#, Unknown version number for TCE\n", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.TceUnknownVersion);
				}
				byte b3 = data[0];
				if (b3 == 0 || b3 > 2)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> %d#, Invalid version number for TCE\n", base.ObjectID);
					throw SQL.ParsingErrorValue(ParsingErrorState.TceInvalidVersion, (int)b3);
				}
				this._tceVersionSupported = b3;
				this._parser.IsColumnEncryptionSupported = true;
				this._parser.TceVersionSupported = this._tceVersionSupported;
				if (data.Length > 1)
				{
					this._parser.EnclaveType = Encoding.Unicode.GetString(data, 2, data.Length - 2);
					return;
				}
				return;
			}
			case 5:
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck> %d#, Received feature extension acknowledgement for GlobalTransactions\n", base.ObjectID);
				}
				if (data.Length < 1)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> %d#, Unknown version number for GlobalTransactions\n", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				base.IsGlobalTransaction = true;
				if (1 == data[0])
				{
					base.IsGlobalTransactionsEnabledForServer = true;
					return;
				}
				return;
			case 8:
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck> %d#, Received feature extension acknowledgement for AzureSQLSupport\n", base.ObjectID);
				}
				if (data.Length < 1)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> %d#, Unknown token for AzureSQLSupport\n", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				base.IsAzureSQLConnection = true;
				if ((data[0] & 1) == 1 && Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionTds.OnFeatureExtAck> %d#, FailoverPartner enabled with Readonly intent for AzureSQL DB\n", base.ObjectID);
					return;
				}
				return;
			}
			throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnknownFeatureAck, featureId);
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000D2DA0 File Offset: 0x000D21A0
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x000D2DC0 File Offset: 0x000D21C0
		internal bool ThreadHasParserLockForClose
		{
			get
			{
				return this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId;
			}
			set
			{
				if (value)
				{
					this._threadIdOwningParserLock = Thread.CurrentThread.ManagedThreadId;
					return;
				}
				if (this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId)
				{
					this._threadIdOwningParserLock = -1;
				}
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000D2DFC File Offset: 0x000D21FC
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x040010E0 RID: 4320
		internal const int _maxNumberOfRedirectRoute = 10;

		// Token: 0x040010E1 RID: 4321
		private readonly SqlConnectionPoolGroupProviderInfo _poolGroupProviderInfo;

		// Token: 0x040010E2 RID: 4322
		private TdsParser _parser;

		// Token: 0x040010E3 RID: 4323
		private SqlLoginAck _loginAck;

		// Token: 0x040010E4 RID: 4324
		private SqlCredential _credential;

		// Token: 0x040010E5 RID: 4325
		private FederatedAuthenticationFeatureExtensionData? _fedAuthFeatureExtensionData;

		// Token: 0x040010E6 RID: 4326
		private bool _sessionRecoveryRequested;

		// Token: 0x040010E7 RID: 4327
		internal bool _sessionRecoveryAcknowledged;

		// Token: 0x040010E8 RID: 4328
		internal SessionData _currentSessionData;

		// Token: 0x040010E9 RID: 4329
		private SessionData _recoverySessionData;

		// Token: 0x040010EA RID: 4330
		internal bool _fedAuthRequired;

		// Token: 0x040010EB RID: 4331
		internal bool _federatedAuthenticationRequested;

		// Token: 0x040010EC RID: 4332
		internal bool _federatedAuthenticationAcknowledged;

		// Token: 0x040010ED RID: 4333
		internal bool _federatedAuthenticationInfoRequested;

		// Token: 0x040010EE RID: 4334
		internal bool _federatedAuthenticationInfoReceived;

		// Token: 0x040010EF RID: 4335
		private readonly ActiveDirectoryAuthenticationTimeoutRetryHelper _activeDirectoryAuthTimeoutRetryHelper;

		// Token: 0x040010F0 RID: 4336
		private readonly SqlAuthenticationProviderManager _sqlAuthenticationProviderManager;

		// Token: 0x040010F1 RID: 4337
		internal byte _tceVersionSupported;

		// Token: 0x040010F2 RID: 4338
		internal byte[] _accessTokenInBytes;

		// Token: 0x040010F3 RID: 4339
		private DbConnectionPool _dbConnectionPool;

		// Token: 0x040010F4 RID: 4340
		private DbConnectionPoolAuthenticationContext _newDbConnectionPoolAuthenticationContext;

		// Token: 0x040010F5 RID: 4341
		private DbConnectionPoolAuthenticationContextKey _dbConnectionPoolAuthenticationContextKey;

		// Token: 0x040010F6 RID: 4342
		private static readonly TimeSpan _dbAuthenticationContextLockedRefreshTimeSpan = new TimeSpan(0, 45, 0);

		// Token: 0x040010F7 RID: 4343
		private static readonly TimeSpan _dbAuthenticationContextUnLockedRefreshTimeSpan = new TimeSpan(0, 10, 0);

		// Token: 0x040010F8 RID: 4344
		private readonly TimeoutTimer _timeout;

		// Token: 0x040010F9 RID: 4345
		private static HashSet<int> transientErrors = new HashSet<int>();

		// Token: 0x040010FA RID: 4346
		private bool _fConnectionOpen;

		// Token: 0x040010FB RID: 4347
		private bool _fResetConnection;

		// Token: 0x040010FC RID: 4348
		private string _originalDatabase;

		// Token: 0x040010FD RID: 4349
		private string _currentFailoverPartner;

		// Token: 0x040010FE RID: 4350
		private string _originalLanguage;

		// Token: 0x040010FF RID: 4351
		private string _currentLanguage;

		// Token: 0x04001100 RID: 4352
		private int _currentPacketSize;

		// Token: 0x04001101 RID: 4353
		private int _asyncCommandCount;

		// Token: 0x04001102 RID: 4354
		private string _instanceName = string.Empty;

		// Token: 0x04001103 RID: 4355
		private DbConnectionPoolIdentity _identity;

		// Token: 0x04001104 RID: 4356
		internal SqlInternalConnectionTds.SyncAsyncLock _parserLock = new SqlInternalConnectionTds.SyncAsyncLock();

		// Token: 0x04001105 RID: 4357
		private int _threadIdOwningParserLock = -1;

		// Token: 0x04001106 RID: 4358
		private SqlConnectionTimeoutErrorInternal timeoutErrorInternal;

		// Token: 0x04001107 RID: 4359
		internal Guid _clientConnectionId = Guid.Empty;

		// Token: 0x04001108 RID: 4360
		private RoutingInfo _routingInfo;

		// Token: 0x04001109 RID: 4361
		private Guid _originalClientConnectionId = Guid.Empty;

		// Token: 0x0400110A RID: 4362
		private string _routingDestination;

		// Token: 0x020003C6 RID: 966
		internal class SyncAsyncLock
		{
			// Token: 0x0600352D RID: 13613 RVA: 0x00144158 File Offset: 0x00143558
			internal void Wait(bool canReleaseFromAnyThread)
			{
				Monitor.Enter(this.semaphore);
				if (canReleaseFromAnyThread || this.semaphore.CurrentCount == 0)
				{
					this.semaphore.Wait();
					if (canReleaseFromAnyThread)
					{
						Monitor.Exit(this.semaphore);
						return;
					}
					this.semaphore.Release();
				}
			}

			// Token: 0x0600352E RID: 13614 RVA: 0x001441A8 File Offset: 0x001435A8
			internal void Wait(bool canReleaseFromAnyThread, int timeout, ref bool lockTaken)
			{
				lockTaken = false;
				bool flag = false;
				try
				{
					Monitor.TryEnter(this.semaphore, timeout, ref flag);
					if (flag)
					{
						if (canReleaseFromAnyThread || this.semaphore.CurrentCount == 0)
						{
							if (this.semaphore.Wait(timeout))
							{
								if (canReleaseFromAnyThread)
								{
									Monitor.Exit(this.semaphore);
									flag = false;
								}
								else
								{
									this.semaphore.Release();
								}
								lockTaken = true;
							}
						}
						else
						{
							lockTaken = true;
						}
					}
				}
				finally
				{
					if (!lockTaken && flag)
					{
						Monitor.Exit(this.semaphore);
					}
				}
			}

			// Token: 0x0600352F RID: 13615 RVA: 0x00144244 File Offset: 0x00143644
			internal void Release()
			{
				if (this.semaphore.CurrentCount == 0)
				{
					this.semaphore.Release();
					return;
				}
				Monitor.Exit(this.semaphore);
			}

			// Token: 0x1700085A RID: 2138
			// (get) Token: 0x06003530 RID: 13616 RVA: 0x00144278 File Offset: 0x00143678
			internal bool CanBeReleasedFromAnyThread
			{
				get
				{
					return this.semaphore.CurrentCount == 0;
				}
			}

			// Token: 0x06003531 RID: 13617 RVA: 0x00144294 File Offset: 0x00143694
			internal bool ThreadMayHaveLock()
			{
				return Monitor.IsEntered(this.semaphore) || this.semaphore.CurrentCount == 0;
			}

			// Token: 0x040020D8 RID: 8408
			private SemaphoreSlim semaphore = new SemaphoreSlim(1);
		}
	}
}
