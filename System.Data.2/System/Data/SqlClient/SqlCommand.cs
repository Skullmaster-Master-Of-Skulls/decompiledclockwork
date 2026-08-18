using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001B2 RID: 434
	[Designer("Microsoft.VSDesigner.Data.VS.SqlCommandDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RecordsAffected")]
	[ToolboxItem(true)]
	public sealed class SqlCommand : DbCommand, ICloneable
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x000B1FB4 File Offset: 0x000B13B4
		internal bool InPrepare
		{
			get
			{
				return this._inPrepare;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x000B1FC8 File Offset: 0x000B13C8
		internal bool IsColumnEncryptionEnabled
		{
			get
			{
				return (this._columnEncryptionSetting == SqlCommandColumnEncryptionSetting.Enabled || (this._columnEncryptionSetting == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._activeConnection.IsColumnEncryptionSettingEnabled)) && this._activeConnection.Parser != null && this._activeConnection.Parser.IsColumnEncryptionSupported;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x000B2014 File Offset: 0x000B1414
		internal bool ShouldUseEnclaveBasedWorkflow
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this._activeConnection.EnclaveAttestationUrl) && this.IsColumnEncryptionEnabled;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x000B203C File Offset: 0x000B143C
		private SqlCommand.CachedAsyncState cachedAsyncState
		{
			get
			{
				if (this._cachedAsyncState == null)
				{
					this._cachedAsyncState = new SqlCommand.CachedAsyncState();
				}
				return this._cachedAsyncState;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x000B2064 File Offset: 0x000B1464
		internal bool IsDescribeParameterEncryptionRPCCurrentlyInProgress
		{
			get
			{
				return this._isDescribeParameterEncryptionRPCCurrentlyInProgress;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x000B2078 File Offset: 0x000B1478
		// (set) Token: 0x06001945 RID: 6469 RVA: 0x000B208C File Offset: 0x000B148C
		internal bool CachingQueryMetadataPostponed { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000B20A0 File Offset: 0x000B14A0
		private SqlCommand.CommandEventSink EventSink
		{
			get
			{
				if (this._smiEventSink == null)
				{
					this._smiEventSink = new SqlCommand.CommandEventSink(this);
				}
				this._smiEventSink.Parent = this.InternalSmiConnection.CurrentEventSink;
				return this._smiEventSink;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x000B20E0 File Offset: 0x000B14E0
		private SmiEventSink_DeferedProcessing OutParamEventSink
		{
			get
			{
				if (this._outParamEventSink == null)
				{
					this._outParamEventSink = new SmiEventSink_DeferedProcessing(this.EventSink);
				}
				else
				{
					this._outParamEventSink.Parent = this.EventSink;
				}
				return this._outParamEventSink;
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x000B2120 File Offset: 0x000B1520
		public SqlCommand()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x000B2190 File Offset: 0x000B1590
		public SqlCommand(string cmdText) : this()
		{
			this.CommandText = cmdText;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x000B21AC File Offset: 0x000B15AC
		public SqlCommand(string cmdText, SqlConnection connection) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x000B21D0 File Offset: 0x000B15D0
		public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000B21F8 File Offset: 0x000B15F8
		public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting) : this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
			this._columnEncryptionSetting = columnEncryptionSetting;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000B2228 File Offset: 0x000B1628
		private SqlCommand(SqlCommand from) : this()
		{
			this.CommandText = from.CommandText;
			this.CommandTimeout = from.CommandTimeout;
			this.CommandType = from.CommandType;
			this.Connection = from.Connection;
			this.DesignTimeVisible = from.DesignTimeVisible;
			this.Transaction = from.Transaction;
			this.UpdatedRowSource = from.UpdatedRowSource;
			this._columnEncryptionSetting = from.ColumnEncryptionSetting;
			SqlParameterCollection parameters = this.Parameters;
			foreach (object obj in from.Parameters)
			{
				parameters.Add((obj is ICloneable) ? (obj as ICloneable).Clone() : obj);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x000B230C File Offset: 0x000B170C
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x000B2320 File Offset: 0x000B1720
		[DefaultValue(null)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_Connection")]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqlConnection Connection
		{
			get
			{
				return this._activeConnection;
			}
			set
			{
				if (this._activeConnection != value && this._activeConnection != null && this.cachedAsyncState.PendingAsyncOperation)
				{
					throw SQL.CannotModifyPropertyAsyncOperationInProgress("Connection");
				}
				if (this._transaction != null && this._transaction.Connection == null)
				{
					this._transaction = null;
				}
				this._smiRequestContext = null;
				if (this.IsPrepared && this._activeConnection != value && this._activeConnection != null)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this.Unprepare();
					}
					catch (OutOfMemoryException)
					{
						this._activeConnection.InnerConnection.DoomThisConnection();
						throw;
					}
					catch (StackOverflowException)
					{
						this._activeConnection.InnerConnection.DoomThisConnection();
						throw;
					}
					catch (ThreadAbortException)
					{
						this._activeConnection.InnerConnection.DoomThisConnection();
						throw;
					}
					catch (Exception)
					{
					}
					finally
					{
						this._prepareHandle = -1;
						this._execType = SqlCommand.EXECTYPE.UNPREPARED;
					}
				}
				this._activeConnection = value;
				Bid.Trace("<sc.SqlCommand.set_Connection|API> %d#, %d#\n", this.ObjectID, (value != null) ? value.ObjectID : -1);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000B2494 File Offset: 0x000B1894
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x000B24A8 File Offset: 0x000B18A8
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (SqlConnection)value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x000B24C4 File Offset: 0x000B18C4
		private SqlInternalConnectionSmi InternalSmiConnection
		{
			get
			{
				return (SqlInternalConnectionSmi)this._activeConnection.InnerConnection;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x000B24E4 File Offset: 0x000B18E4
		private SqlInternalConnectionTds InternalTdsConnection
		{
			get
			{
				return (SqlInternalConnectionTds)this._activeConnection.InnerConnection;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x000B2504 File Offset: 0x000B1904
		private bool IsShiloh
		{
			get
			{
				return this._activeConnection != null && this._activeConnection.IsShiloh;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x000B2528 File Offset: 0x000B1928
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x000B253C File Offset: 0x000B193C
		[ResDescription("SqlCommand_NotificationAutoEnlist")]
		[DefaultValue(true)]
		[ResCategory("DataCategory_Notification")]
		public bool NotificationAutoEnlist
		{
			get
			{
				return this._notificationAutoEnlist;
			}
			set
			{
				this._notificationAutoEnlist = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x000B2550 File Offset: 0x000B1950
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x000B2564 File Offset: 0x000B1964
		[ResCategory("DataCategory_Notification")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ResDescription("SqlCommand_Notification")]
		public SqlNotificationRequest Notification
		{
			get
			{
				return this._notification;
			}
			set
			{
				Bid.Trace("<sc.SqlCommand.set_Notification|API> %d#\n", this.ObjectID);
				this._sqlDep = null;
				this._notification = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x000B2590 File Offset: 0x000B1990
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._activeConnection != null && this._activeConnection.StatisticsEnabled)
				{
					return this._activeConnection.Statistics;
				}
				return null;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x000B25C0 File Offset: 0x000B19C0
		// (set) Token: 0x0600195B RID: 6491 RVA: 0x000B25F0 File Offset: 0x000B19F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DbCommand_Transaction")]
		[Browsable(false)]
		public new SqlTransaction Transaction
		{
			get
			{
				if (this._transaction != null && this._transaction.Connection == null)
				{
					this._transaction = null;
				}
				return this._transaction;
			}
			set
			{
				if (this._transaction != value && this._activeConnection != null && this.cachedAsyncState.PendingAsyncOperation)
				{
					throw SQL.CannotModifyPropertyAsyncOperationInProgress("Transaction");
				}
				Bid.Trace("<sc.SqlCommand.set_Transaction|API> %d#\n", this.ObjectID);
				this._transaction = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x000B2640 File Offset: 0x000B1A40
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x000B2654 File Offset: 0x000B1A54
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (SqlTransaction)value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x000B2670 File Offset: 0x000B1A70
		// (set) Token: 0x0600195F RID: 6495 RVA: 0x000B2690 File Offset: 0x000B1A90
		[ResCategory("DataCategory_Data")]
		[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlCommandTextEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[ResDescription("DbCommand_CommandText")]
		[RefreshProperties(RefreshProperties.All)]
		public override string CommandText
		{
			get
			{
				string commandText = this._commandText;
				if (commandText == null)
				{
					return ADP.StrEmpty;
				}
				return commandText;
			}
			set
			{
				if (Bid.TraceOn)
				{
					Bid.Trace("<sc.SqlCommand.set_CommandText|API> %d#, '", this.ObjectID);
					Bid.PutStr(value);
					Bid.Trace("'\n");
				}
				if (ADP.SrcCompare(this._commandText, value) != 0)
				{
					this.PropertyChanging();
					this._commandText = value;
				}
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x000B26E0 File Offset: 0x000B1AE0
		[ResDescription("TCE_SqlCommand_ColumnEncryptionSetting")]
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlCommandColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				return this._columnEncryptionSetting;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x000B26F4 File Offset: 0x000B1AF4
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x000B2708 File Offset: 0x000B1B08
		[ResDescription("DbCommand_CommandTimeout")]
		[ResCategory("DataCategory_Data")]
		public override int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				Bid.Trace("<sc.SqlCommand.set_CommandTimeout|API> %d#, %d\n", this.ObjectID, value);
				if (value < 0)
				{
					throw ADP.InvalidCommandTimeout(value);
				}
				if (value != this._commandTimeout)
				{
					this.PropertyChanging();
					this._commandTimeout = value;
				}
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000B2748 File Offset: 0x000B1B48
		public void ResetCommandTimeout()
		{
			if (30 != this._commandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = 30;
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000B2770 File Offset: 0x000B1B70
		private bool ShouldSerializeCommandTimeout()
		{
			return 30 != this._commandTimeout;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x000B278C File Offset: 0x000B1B8C
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x000B27A8 File Offset: 0x000B1BA8
		[ResDescription("DbCommand_CommandType")]
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		public override CommandType CommandType
		{
			get
			{
				CommandType commandType = this._commandType;
				if (commandType == (CommandType)0)
				{
					return CommandType.Text;
				}
				return commandType;
			}
			set
			{
				Bid.Trace("<sc.SqlCommand.set_CommandType|API> %d#, %d{ds.CommandType}\n", this.ObjectID, (int)value);
				if (this._commandType == value)
				{
					return;
				}
				if (value == CommandType.Text || value == CommandType.StoredProcedure)
				{
					this.PropertyChanging();
					this._commandType = value;
					return;
				}
				if (value != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(value);
				}
				throw SQL.NotSupportedCommandType(value);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x000B2800 File Offset: 0x000B1C00
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x000B2818 File Offset: 0x000B1C18
		[DefaultValue(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[Browsable(false)]
		public override bool DesignTimeVisible
		{
			get
			{
				return !this._designTimeInvisible;
			}
			set
			{
				this._designTimeInvisible = !value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x000B2838 File Offset: 0x000B1C38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResDescription("DbCommand_Parameters")]
		[ResCategory("DataCategory_Data")]
		public new SqlParameterCollection Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = new SqlParameterCollection();
				}
				return this._parameters;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x000B2860 File Offset: 0x000B1C60
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x000B2874 File Offset: 0x000B1C74
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x000B2888 File Offset: 0x000B1C88
		[ResDescription("DbCommand_UpdatedRowSource")]
		[DefaultValue(UpdateRowSource.Both)]
		[ResCategory("DataCategory_Update")]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				if (value <= UpdateRowSource.Both)
				{
					this._updatedRowSource = value;
					return;
				}
				throw ADP.InvalidUpdateRowSource(value);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600196D RID: 6509 RVA: 0x000B28A8 File Offset: 0x000B1CA8
		// (remove) Token: 0x0600196E RID: 6510 RVA: 0x000B28CC File Offset: 0x000B1CCC
		[ResCategory("DataCategory_StatementCompleted")]
		[ResDescription("DbCommand_StatementCompleted")]
		public event StatementCompletedEventHandler StatementCompleted
		{
			add
			{
				this._statementCompletedEventHandler = (StatementCompletedEventHandler)Delegate.Combine(this._statementCompletedEventHandler, value);
			}
			remove
			{
				this._statementCompletedEventHandler = (StatementCompletedEventHandler)Delegate.Remove(this._statementCompletedEventHandler, value);
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000B28F0 File Offset: 0x000B1CF0
		internal void OnStatementCompleted(int recordCount)
		{
			if (0 <= recordCount)
			{
				StatementCompletedEventHandler statementCompletedEventHandler = this._statementCompletedEventHandler;
				if (statementCompletedEventHandler != null)
				{
					try
					{
						Bid.Trace("<sc.SqlCommand.OnStatementCompleted|INFO> %d#, recordCount=%d\n", this.ObjectID, recordCount);
						statementCompletedEventHandler(this, new StatementCompletedEventArgs(recordCount));
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableOrSecurityExceptionType(e))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(e);
					}
				}
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000B295C File Offset: 0x000B1D5C
		private void PropertyChanging()
		{
			this.IsDirty = true;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000B2970 File Offset: 0x000B1D70
		public override void Prepare()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			if (this._activeConnection != null && this._activeConnection.IsContextConnection)
			{
				return;
			}
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.Prepare|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlCommand.Prepare|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			statistics = SqlStatistics.StartTimer(this.Statistics);
			if ((this.IsPrepared && !this.IsDirty) || this.CommandType == CommandType.StoredProcedure || (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0))
			{
				if (this.Statistics != null)
				{
					this.Statistics.SafeIncrement(ref this.Statistics._prepares);
				}
				this._hiddenPrepare = false;
			}
			else
			{
				this.ValidateCommand("Prepare", false);
				bool flag = true;
				TdsParser target = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
					this.GetStateObject(null);
					if (this._parameters != null)
					{
						int count = this._parameters.Count;
						for (int i = 0; i < count; i++)
						{
							this._parameters[i].Prepare(this);
						}
					}
					this.InternalPrepare();
				}
				catch (OutOfMemoryException e)
				{
					flag = false;
					this._activeConnection.Abort(e);
					throw;
				}
				catch (StackOverflowException e2)
				{
					flag = false;
					this._activeConnection.Abort(e2);
					throw;
				}
				catch (ThreadAbortException e3)
				{
					flag = false;
					this._activeConnection.Abort(e3);
					SqlInternalConnection.BestEffortCleanup(target);
					throw;
				}
				catch (Exception e4)
				{
					flag = ADP.IsCatchableExceptionType(e4);
					throw;
				}
				finally
				{
					if (flag)
					{
						this._hiddenPrepare = false;
						this.ReliablePutStateObject();
					}
				}
			}
			SqlStatistics.StopTimer(statistics);
			Bid.ScopeLeave(ref intPtr);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000B2B84 File Offset: 0x000B1F84
		private void InternalPrepare()
		{
			if (this.IsDirty)
			{
				this.Unprepare();
				this.IsDirty = false;
			}
			this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
			this._preparedConnectionCloseCount = this._activeConnection.CloseCount;
			this._preparedConnectionReconnectCount = this._activeConnection.ReconnectCount;
			if (this.Statistics != null)
			{
				this.Statistics.SafeIncrement(ref this.Statistics._prepares);
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000B2BF0 File Offset: 0x000B1FF0
		internal void Unprepare()
		{
			if (this._activeConnection.IsContextConnection)
			{
				return;
			}
			this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
			if (this._activeConnection.CloseCount != this._preparedConnectionCloseCount || this._activeConnection.ReconnectCount != this._preparedConnectionReconnectCount)
			{
				this._prepareHandle = -1;
			}
			this._cachedMetaData = null;
			Bid.Trace("<sc.SqlCommand.Prepare|INFO> %d#, Command unprepared.\n", this.ObjectID);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000B2C58 File Offset: 0x000B2058
		public override void Cancel()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.Cancel|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlCommand.Cancel|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			SqlStatistics statistics = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				TaskCompletionSource<object> reconnectionCompletionSource = this._reconnectionCompletionSource;
				if (reconnectionCompletionSource == null || !reconnectionCompletionSource.TrySetCanceled())
				{
					if (this._activeConnection != null)
					{
						SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
						if (sqlInternalConnectionTds != null)
						{
							SqlInternalConnectionTds obj = sqlInternalConnectionTds;
							lock (obj)
							{
								if (sqlInternalConnectionTds == this._activeConnection.InnerConnection as SqlInternalConnectionTds)
								{
									if (sqlInternalConnectionTds.Parser != null)
									{
										TdsParser target = null;
										RuntimeHelpers.PrepareConstrainedRegions();
										try
										{
											target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
											if (!this._pendingCancel)
											{
												this._pendingCancel = true;
												TdsParserStateObject stateObj = this._stateObj;
												if (stateObj != null)
												{
													stateObj.Cancel(this.ObjectID);
												}
												else
												{
													SqlDataReader sqlDataReader = sqlInternalConnectionTds.FindLiveReader(this);
													if (sqlDataReader != null)
													{
														sqlDataReader.Cancel(this.ObjectID);
													}
												}
											}
										}
										catch (OutOfMemoryException e)
										{
											this._activeConnection.Abort(e);
											throw;
										}
										catch (StackOverflowException e2)
										{
											this._activeConnection.Abort(e2);
											throw;
										}
										catch (ThreadAbortException e3)
										{
											this._activeConnection.Abort(e3);
											SqlInternalConnection.BestEffortCleanup(target);
											throw;
										}
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000B2E40 File Offset: 0x000B2240
		public new SqlParameter CreateParameter()
		{
			return new SqlParameter();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000B2E54 File Offset: 0x000B2254
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000B2E68 File Offset: 0x000B2268
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._cachedMetaData = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000B2E88 File Offset: 0x000B2288
		public override object ExecuteScalar()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.ExecuteScalar|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteScalar|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			bool success = false;
			int? sqlExceptionNumber = null;
			object result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.WriteBeginExecuteEvent();
				SqlDataReader ds = this.RunExecuteReader(CommandBehavior.Default, RunBehavior.ReturnImmediately, true, "ExecuteScalar");
				object obj = this.CompleteExecuteScalar(ds, false);
				success = true;
				result = obj;
			}
			catch (SqlException ex)
			{
				sqlExceptionNumber = new int?(ex.Number);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
				this.WriteEndExecuteEvent(success, sqlExceptionNumber, true);
			}
			return result;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000B2F6C File Offset: 0x000B236C
		private object CompleteExecuteScalar(SqlDataReader ds, bool returnSqlValue)
		{
			object result = null;
			try
			{
				if (ds.Read() && ds.FieldCount > 0)
				{
					if (returnSqlValue)
					{
						result = ds.GetSqlValue(0);
					}
					else
					{
						result = ds.GetValue(0);
					}
				}
			}
			finally
			{
				ds.Close();
			}
			return result;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000B2FC8 File Offset: 0x000B23C8
		public override int ExecuteNonQuery()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.ExecuteNonQuery|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteNonQuery|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			bool success = false;
			int? sqlExceptionNumber = null;
			int rowsAffected;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.WriteBeginExecuteEvent();
				bool flag;
				this.InternalExecuteNonQuery(null, "ExecuteNonQuery", false, this.CommandTimeout, out flag, false, false);
				success = true;
				rowsAffected = this._rowsAffected;
			}
			catch (SqlException ex)
			{
				sqlExceptionNumber = new int?(ex.Number);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
				this.WriteEndExecuteEvent(success, sqlExceptionNumber, true);
			}
			return rowsAffected;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000B30AC File Offset: 0x000B24AC
		internal void ExecuteToPipe(SmiContext pipeContext)
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.ExecuteToPipe|INFO> %d#", this.ObjectID);
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				bool flag;
				this.InternalExecuteNonQuery(null, "ExecuteNonQuery", true, this.CommandTimeout, out flag, false, false);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000B3130 File Offset: 0x000B2530
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteNonQuery()
		{
			return this.BeginExecuteNonQuery(null, null);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000B3148 File Offset: 0x000B2548
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.BeginExecuteNonQuery|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			SqlConnection.ExecutePermission.Demand();
			return this.BeginExecuteNonQueryInternal(CommandBehavior.Default, callback, stateObject, 0, false, false);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000B317C File Offset: 0x000B257C
		private IAsyncResult BeginExecuteNonQueryAsync(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteNonQueryInternal(CommandBehavior.Default, callback, stateObject, this.CommandTimeout, false, true);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000B319C File Offset: 0x000B259C
		private IAsyncResult BeginExecuteNonQueryInternal(CommandBehavior behavior, AsyncCallback callback, object stateObject, int timeout, bool inRetry, bool asyncWrite = false)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>(stateObject);
			TaskCompletionSource<object> localCompletion = new TaskCompletionSource<object>(stateObject);
			if (!inRetry)
			{
				this._pendingCancel = false;
				this.ValidateAsyncCommand();
			}
			SqlStatistics statistics = null;
			IAsyncResult task2;
			try
			{
				if (!inRetry)
				{
					statistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
				}
				bool usedCache;
				try
				{
					Task task = this.InternalExecuteNonQuery(localCompletion, "BeginExecuteNonQuery", false, timeout, out usedCache, asyncWrite, inRetry);
					if (task != null)
					{
						AsyncHelper.ContinueTask(task, localCompletion, delegate
						{
							this.BeginExecuteNonQueryInternalReadStage(localCompletion);
						}, null, null, null, null, null);
					}
					else
					{
						this.BeginExecuteNonQueryInternalReadStage(localCompletion);
					}
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(e))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				if (!this.TriggerInternalEndAndRetryIfNecessary(behavior, stateObject, timeout, "EndExecuteNonQuery", usedCache, inRetry, asyncWrite, taskCompletionSource, localCompletion, new Func<IAsyncResult, string, bool, object>(this.InternalEndExecuteNonQuery), new Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult>(this.BeginExecuteNonQueryInternal)))
				{
					taskCompletionSource = localCompletion;
				}
				if (callback != null)
				{
					taskCompletionSource.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = taskCompletionSource.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return task2;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000B3308 File Offset: 0x000B2708
		private void BeginExecuteNonQueryInternalReadStage(TaskCompletionSource<object> completion)
		{
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteNonQuery", this._activeConnection);
				this._stateObj.ReadSni(completion);
			}
			catch (OutOfMemoryException e)
			{
				this._activeConnection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._activeConnection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			catch (Exception)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000B3408 File Offset: 0x000B2808
		private void VerifyEndExecuteState(Task completionTask, string endMethod, bool fullCheckForColumnEncryption = false)
		{
			if (completionTask == null)
			{
				throw ADP.ArgumentNull("asyncResult");
			}
			if (completionTask.IsCanceled)
			{
				if (this._stateObj == null)
				{
					throw SQL.CR_ReconnectionCancelled();
				}
				this._stateObj.Parser.State = TdsParserState.Broken;
				this._stateObj.Parser.Connection.BreakConnection();
				this._stateObj.Parser.ThrowExceptionAndWarning(this._stateObj, false, false);
			}
			else if (completionTask.IsFaulted)
			{
				throw completionTask.Exception.InnerException;
			}
			if (this.IsColumnEncryptionEnabled && !fullCheckForColumnEncryption)
			{
				if (this._activeConnection.State != ConnectionState.Open)
				{
					throw ADP.ClosedConnectionError();
				}
				return;
			}
			else
			{
				if (this.cachedAsyncState.EndMethodName == null)
				{
					throw ADP.MethodCalledTwice(endMethod);
				}
				if (endMethod != this.cachedAsyncState.EndMethodName)
				{
					throw ADP.MismatchedAsyncResult(this.cachedAsyncState.EndMethodName, endMethod);
				}
				if (this._activeConnection.State != ConnectionState.Open || !this.cachedAsyncState.IsActiveConnectionValid(this._activeConnection))
				{
					throw ADP.ClosedConnectionError();
				}
				return;
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000B3510 File Offset: 0x000B2910
		private void WaitForAsyncResults(IAsyncResult asyncResult, bool isInternal)
		{
			Task task = (Task)asyncResult;
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			if (this._stateObj != null)
			{
				this._stateObj._networkPacketTaskSource = null;
			}
			if (!isInternal && (!this.IsColumnEncryptionEnabled || !task.IsFaulted))
			{
				this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
			}
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000B3570 File Offset: 0x000B2970
		public int EndExecuteNonQuery(IAsyncResult asyncResult)
		{
			int result;
			try
			{
				result = this.EndExecuteNonQueryInternal(asyncResult);
			}
			finally
			{
				Bid.CorrelationTrace("<sc.SqlCommand.EndExecuteNonQuery|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			}
			return result;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000B35B8 File Offset: 0x000B29B8
		private void ThrowIfReconnectionHasBeenCanceled()
		{
			if (this._stateObj == null)
			{
				TaskCompletionSource<object> reconnectionCompletionSource = this._reconnectionCompletionSource;
				if (reconnectionCompletionSource != null && reconnectionCompletionSource.Task.IsCanceled)
				{
					throw SQL.CR_ReconnectionCancelled();
				}
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000B35EC File Offset: 0x000B29EC
		private int EndExecuteNonQueryAsync(IAsyncResult asyncResult)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.EndExecuteNonQueryAsync|Info|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			if (!this._internalEndExecuteInitiated)
			{
				TdsParserStateObject stateObj = this._stateObj;
				lock (stateObj)
				{
					return this.EndExecuteNonQueryInternal(asyncResult);
				}
			}
			return this.EndExecuteNonQueryInternal(asyncResult);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000B3680 File Offset: 0x000B2A80
		private int EndExecuteNonQueryInternal(IAsyncResult asyncResult)
		{
			SqlStatistics statistics = null;
			bool success = false;
			int? sqlExceptionNumber = null;
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				int num = (int)this.InternalEndExecuteNonQuery(asyncResult, "EndExecuteNonQuery", false);
				success = true;
				result = num;
			}
			catch (SqlException ex)
			{
				sqlExceptionNumber = new int?(ex.Number);
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
			catch (Exception e)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(e))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				this.WriteEndExecuteEvent(success, sqlExceptionNumber, false);
			}
			return result;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000B3774 File Offset: 0x000B2B74
		private object InternalEndExecuteNonQuery(IAsyncResult asyncResult, string endMethod, bool isInternal)
		{
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			object result;
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.VerifyEndExecuteState((Task)asyncResult, endMethod, false);
				this.WaitForAsyncResults(asyncResult, isInternal);
				if (this.IsColumnEncryptionEnabled)
				{
					this.VerifyEndExecuteState((Task)asyncResult, endMethod, true);
				}
				bool flag = true;
				try
				{
					if (!isInternal)
					{
						this.NotifyDependency();
						if (this._internalEndExecuteInitiated)
						{
							this.cachedAsyncState.ResetAsyncState();
							return this._rowsAffected;
						}
					}
					this.CheckThrowSNIException();
					if (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
					{
						try
						{
							bool flag2;
							if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag2))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
							goto IL_D8;
						}
						finally
						{
							if (!isInternal)
							{
								this.cachedAsyncState.ResetAsyncState();
							}
						}
					}
					SqlDataReader sqlDataReader = this.CompleteAsyncExecuteReader(isInternal, false);
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
					}
					IL_D8:;
				}
				catch (Exception e)
				{
					flag = ADP.IsCatchableExceptionType(e);
					throw;
				}
				finally
				{
					if (flag)
					{
						this.PutStateObject();
					}
				}
				result = this._rowsAffected;
			}
			catch (OutOfMemoryException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (StackOverflowException e3)
			{
				this._activeConnection.Abort(e3);
				throw;
			}
			catch (ThreadAbortException e4)
			{
				this._activeConnection.Abort(e4);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			return result;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000B3958 File Offset: 0x000B2D58
		private Task InternalExecuteNonQuery(TaskCompletionSource<object> completion, string methodName, bool sendToPipe, int timeout, out bool usedCache, bool asyncWrite = false, bool inRetry = false)
		{
			bool async = completion != null;
			usedCache = false;
			SqlStatistics statistics = this.Statistics;
			this._rowsAffected = -1;
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			Task result;
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				if (!inRetry)
				{
					this.ValidateCommand(methodName, async);
				}
				this.CheckNotificationStateAndAutoEnlist();
				Task task = null;
				if (this._activeConnection.IsContextConnection)
				{
					if (statistics != null)
					{
						statistics.SafeIncrement(ref statistics._unpreparedExecs);
					}
					this.RunExecuteNonQuerySmi(sendToPipe);
				}
				else if (!this.ShouldUseEnclaveBasedWorkflow && !this.BatchRPCMode && CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
				{
					if (statistics != null)
					{
						if (!this.IsDirty && this.IsPrepared)
						{
							statistics.SafeIncrement(ref statistics._preparedExecs);
						}
						else
						{
							statistics.SafeIncrement(ref statistics._unpreparedExecs);
						}
					}
					task = this.RunExecuteNonQueryTds(methodName, async, timeout, asyncWrite);
				}
				else
				{
					Bid.Trace("<sc.SqlCommand.ExecuteNonQuery|INFO> %d#, Command executed as RPC.\n", this.ObjectID);
					SqlDataReader reader = this.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, methodName, completion, timeout, out task, out usedCache, asyncWrite, inRetry);
					if (reader != null)
					{
						if (task != null)
						{
							task = AsyncHelper.CreateContinuationTask(task, delegate()
							{
								reader.Close();
							}, null, null);
						}
						else
						{
							reader.Close();
						}
					}
				}
				result = task;
			}
			catch (OutOfMemoryException e)
			{
				this._activeConnection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._activeConnection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			return result;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000B3B24 File Offset: 0x000B2F24
		public XmlReader ExecuteXmlReader()
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.ExecuteXmlReader|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteXmlReader|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			bool success = false;
			int? sqlExceptionNumber = null;
			XmlReader result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.WriteBeginExecuteEvent();
				SqlDataReader ds = this.RunExecuteReader(CommandBehavior.SequentialAccess, RunBehavior.ReturnImmediately, true, "ExecuteXmlReader");
				XmlReader xmlReader = this.CompleteXmlReader(ds);
				success = true;
				result = xmlReader;
			}
			catch (SqlException ex)
			{
				sqlExceptionNumber = new int?(ex.Number);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
				this.WriteEndExecuteEvent(success, sqlExceptionNumber, true);
			}
			return result;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000B3C08 File Offset: 0x000B3008
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteXmlReader()
		{
			return this.BeginExecuteXmlReader(null, null);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000B3C20 File Offset: 0x000B3020
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteXmlReader(AsyncCallback callback, object stateObject)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.BeginExecuteXmlReader|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			SqlConnection.ExecutePermission.Demand();
			return this.BeginExecuteXmlReaderInternal(CommandBehavior.SequentialAccess, callback, stateObject, 0, false, false);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000B3C54 File Offset: 0x000B3054
		private IAsyncResult BeginExecuteXmlReaderAsync(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteXmlReaderInternal(CommandBehavior.SequentialAccess, callback, stateObject, this.CommandTimeout, false, true);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000B3C74 File Offset: 0x000B3074
		private IAsyncResult BeginExecuteXmlReaderInternal(CommandBehavior behavior, AsyncCallback callback, object stateObject, int timeout, bool inRetry, bool asyncWrite = false)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>(stateObject);
			TaskCompletionSource<object> localCompletion = new TaskCompletionSource<object>(stateObject);
			if (!inRetry)
			{
				this._pendingCancel = false;
				this.ValidateAsyncCommand();
			}
			SqlStatistics statistics = null;
			IAsyncResult task2;
			try
			{
				if (!inRetry)
				{
					statistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
				}
				Task task;
				bool usedCache;
				try
				{
					this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, "BeginExecuteXmlReader", localCompletion, timeout, out task, out usedCache, asyncWrite, inRetry);
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(e))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				if (task != null)
				{
					AsyncHelper.ContinueTask(task, localCompletion, delegate
					{
						this.BeginExecuteXmlReaderInternalReadStage(localCompletion);
					}, null, null, null, null, null);
				}
				else
				{
					this.BeginExecuteXmlReaderInternalReadStage(localCompletion);
				}
				if (!this.TriggerInternalEndAndRetryIfNecessary(behavior, stateObject, timeout, "EndExecuteXmlReader", usedCache, inRetry, asyncWrite, taskCompletionSource, localCompletion, new Func<IAsyncResult, string, bool, object>(this.InternalEndExecuteReader), new Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult>(this.BeginExecuteXmlReaderInternal)))
				{
					taskCompletionSource = localCompletion;
				}
				if (callback != null)
				{
					taskCompletionSource.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = taskCompletionSource.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return task2;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000B3DE4 File Offset: 0x000B31E4
		private void BeginExecuteXmlReaderInternalReadStage(TaskCompletionSource<object> completion)
		{
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteXmlReader", this._activeConnection);
				this._stateObj.ReadSni(completion);
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				completion.TrySetException(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				completion.TrySetException(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(target);
				completion.TrySetException(ex3);
				throw;
			}
			catch (Exception exception)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				completion.TrySetException(exception);
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000B3F08 File Offset: 0x000B3308
		public XmlReader EndExecuteXmlReader(IAsyncResult asyncResult)
		{
			XmlReader result;
			try
			{
				result = this.EndExecuteXmlReaderInternal(asyncResult);
			}
			finally
			{
				Bid.CorrelationTrace("<sc.SqlCommand.EndExecuteXmlReader|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			}
			return result;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000B3F50 File Offset: 0x000B3350
		private XmlReader EndExecuteXmlReaderAsync(IAsyncResult asyncResult)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.EndExecuteXmlReaderAsync|Info|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			if (!this._internalEndExecuteInitiated)
			{
				TdsParserStateObject stateObj = this._stateObj;
				lock (stateObj)
				{
					return this.EndExecuteXmlReaderInternal(asyncResult);
				}
			}
			return this.EndExecuteXmlReaderInternal(asyncResult);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000B3FE4 File Offset: 0x000B33E4
		private XmlReader EndExecuteXmlReaderInternal(IAsyncResult asyncResult)
		{
			bool success = false;
			int? sqlExceptionNumber = null;
			XmlReader result;
			try
			{
				XmlReader xmlReader = this.CompleteXmlReader(this.InternalEndExecuteReader(asyncResult, "EndExecuteXmlReader", false));
				success = true;
				result = xmlReader;
			}
			catch (SqlException ex)
			{
				sqlExceptionNumber = new int?(ex.Number);
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
			catch (Exception e)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(e))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				this.WriteEndExecuteEvent(success, sqlExceptionNumber, false);
			}
			return result;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000B40C4 File Offset: 0x000B34C4
		private XmlReader CompleteXmlReader(SqlDataReader ds)
		{
			XmlReader xmlReader = null;
			SmiExtendedMetaData[] internalSmiMetaData = ds.GetInternalSmiMetaData();
			bool flag = internalSmiMetaData != null && internalSmiMetaData.Length == 1 && (internalSmiMetaData[0].SqlDbType == SqlDbType.NText || internalSmiMetaData[0].SqlDbType == SqlDbType.NVarChar || internalSmiMetaData[0].SqlDbType == SqlDbType.Xml);
			if (flag)
			{
				try
				{
					SqlStream sqlStream = new SqlStream(ds, true, internalSmiMetaData[0].SqlDbType != SqlDbType.Xml);
					xmlReader = sqlStream.ToXmlReader();
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						ds.Close();
					}
					throw;
				}
			}
			if (xmlReader == null)
			{
				ds.Close();
				throw SQL.NonXmlResult();
			}
			return xmlReader;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000B4174 File Offset: 0x000B3574
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader()
		{
			return this.BeginExecuteReader(null, null, CommandBehavior.Default);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000B418C File Offset: 0x000B358C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteReader(callback, stateObject, CommandBehavior.Default);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000B41A4 File Offset: 0x000B35A4
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteDbDataReader|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			return this.ExecuteReader(behavior, "ExecuteReader");
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000B41D0 File Offset: 0x000B35D0
		public new SqlDataReader ExecuteReader()
		{
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.ExecuteReader|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteReader|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			SqlDataReader result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				result = this.ExecuteReader(CommandBehavior.Default, "ExecuteReader");
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000B4248 File Offset: 0x000B3648
		public new SqlDataReader ExecuteReader(CommandBehavior behavior)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommand.ExecuteReader|API> %d#, behavior=%d{ds.CommandBehavior}", this.ObjectID, (int)behavior);
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteReader|API|Correlation> ObjectID%d#, behavior=%d{ds.CommandBehavior}, ActivityID %ls\n", this.ObjectID, (int)behavior);
			SqlDataReader result;
			try
			{
				result = this.ExecuteReader(behavior, "ExecuteReader");
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000B42B0 File Offset: 0x000B36B0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(CommandBehavior behavior)
		{
			return this.BeginExecuteReader(null, null, behavior);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000B42C8 File Offset: 0x000B36C8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.BeginExecuteReader|API|Correlation> ObjectID%d#, behavior=%d{ds.CommandBehavior}, ActivityID %ls\n", this.ObjectID, (int)behavior);
			SqlConnection.ExecutePermission.Demand();
			return this.BeginExecuteReaderInternal(behavior, callback, stateObject, 0, false, false);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000B42FC File Offset: 0x000B36FC
		internal SqlDataReader ExecuteReader(CommandBehavior behavior, string method)
		{
			SqlConnection.ExecutePermission.Demand();
			this._pendingCancel = false;
			SqlStatistics statistics = null;
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool success = false;
			int? sqlExceptionNumber = null;
			SqlDataReader result;
			try
			{
				this.WriteBeginExecuteEvent();
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				statistics = SqlStatistics.StartTimer(this.Statistics);
				SqlDataReader sqlDataReader = this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, method);
				success = true;
				result = sqlDataReader;
			}
			catch (SqlException ex)
			{
				sqlExceptionNumber = new int?(ex.Number);
				throw;
			}
			catch (OutOfMemoryException e)
			{
				this._activeConnection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._activeConnection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				this.WriteEndExecuteEvent(success, sqlExceptionNumber, true);
			}
			return result;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000B4444 File Offset: 0x000B3844
		public SqlDataReader EndExecuteReader(IAsyncResult asyncResult)
		{
			SqlDataReader result;
			try
			{
				result = this.EndExecuteReaderInternal(asyncResult);
			}
			finally
			{
				Bid.CorrelationTrace("<sc.SqlCommand.EndExecuteReader|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			}
			return result;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000B448C File Offset: 0x000B388C
		private SqlDataReader EndExecuteReaderAsync(IAsyncResult asyncResult)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.EndExecuteReaderAsync|Info|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			if (!this._internalEndExecuteInitiated)
			{
				TdsParserStateObject stateObj = this._stateObj;
				lock (stateObj)
				{
					return this.EndExecuteReaderInternal(asyncResult);
				}
			}
			return this.EndExecuteReaderInternal(asyncResult);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000B4520 File Offset: 0x000B3920
		private SqlDataReader EndExecuteReaderInternal(IAsyncResult asyncResult)
		{
			SqlStatistics statistics = null;
			bool success = false;
			int? sqlExceptionNumber = null;
			SqlDataReader result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				SqlDataReader sqlDataReader = this.InternalEndExecuteReader(asyncResult, "EndExecuteReader", false);
				success = true;
				result = sqlDataReader;
			}
			catch (SqlException ex)
			{
				sqlExceptionNumber = new int?(ex.Number);
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
			catch (Exception e)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(e))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				this.WriteEndExecuteEvent(success, sqlExceptionNumber, false);
			}
			return result;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000B4610 File Offset: 0x000B3A10
		private IAsyncResult BeginExecuteReaderAsync(CommandBehavior behavior, AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteReaderInternal(behavior, callback, stateObject, this.CommandTimeout, false, true);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000B4630 File Offset: 0x000B3A30
		private IAsyncResult BeginExecuteReaderInternal(CommandBehavior behavior, AsyncCallback callback, object stateObject, int timeout, bool inRetry, bool asyncWrite = false)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>(stateObject);
			TaskCompletionSource<object> localCompletion = new TaskCompletionSource<object>(stateObject);
			if (!inRetry)
			{
				this._pendingCancel = false;
			}
			SqlStatistics statistics = null;
			IAsyncResult task2;
			try
			{
				if (!inRetry)
				{
					statistics = SqlStatistics.StartTimer(this.Statistics);
					this.WriteBeginExecuteEvent();
					this.ValidateAsyncCommand();
				}
				Task task = null;
				bool usedCache;
				try
				{
					this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, "BeginExecuteReader", localCompletion, timeout, out task, out usedCache, asyncWrite, inRetry);
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(e))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				if (task != null)
				{
					AsyncHelper.ContinueTask(task, localCompletion, delegate
					{
						this.BeginExecuteReaderInternalReadStage(localCompletion);
					}, null, null, null, null, null);
				}
				else
				{
					this.BeginExecuteReaderInternalReadStage(localCompletion);
				}
				if (!this.TriggerInternalEndAndRetryIfNecessary(behavior, stateObject, timeout, "EndExecuteReader", usedCache, inRetry, asyncWrite, taskCompletionSource, localCompletion, new Func<IAsyncResult, string, bool, object>(this.InternalEndExecuteReader), new Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult>(this.BeginExecuteReaderInternal)))
				{
					taskCompletionSource = localCompletion;
				}
				if (callback != null)
				{
					taskCompletionSource.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = taskCompletionSource.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return task2;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000B47A0 File Offset: 0x000B3BA0
		private bool TriggerInternalEndAndRetryIfNecessary(CommandBehavior behavior, object stateObject, int timeout, string endMethod, bool usedCache, bool inRetry, bool asyncWrite, TaskCompletionSource<object> globalCompletion, TaskCompletionSource<object> localCompletion, Func<IAsyncResult, string, bool, object> endFunc, Func<CommandBehavior, AsyncCallback, object, int, bool, bool, IAsyncResult> retryFunc)
		{
			if (this.IsColumnEncryptionEnabled && !inRetry && (usedCache || this.ShouldUseEnclaveBasedWorkflow))
			{
				long firstAttemptStart = ADP.TimerCurrent();
				Action<Task<object>> <>9__1;
				localCompletion.Task.ContinueWith(delegate(Task<object> tsk)
				{
					if (tsk.IsFaulted)
					{
						globalCompletion.TrySetException(tsk.Exception.InnerException);
						return;
					}
					if (tsk.IsCanceled)
					{
						globalCompletion.TrySetCanceled();
						return;
					}
					try
					{
						this._internalEndExecuteInitiated = true;
						TdsParserStateObject stateObj = this._stateObj;
						lock (stateObj)
						{
							endFunc(tsk, endMethod, true);
						}
						globalCompletion.TrySetResult(tsk.Result);
					}
					catch (Exception ex)
					{
						if (ADP.IsCatchableExceptionType(ex))
						{
							this.ReliablePutStateObject();
						}
						bool flag2 = ex is EnclaveDelegate.RetriableEnclaveQueryExecutionException;
						if (ex is SqlException)
						{
							SqlException ex2 = ex as SqlException;
							for (int i = 0; i < ex2.Errors.Count; i++)
							{
								if ((usedCache && ex2.Errors[i].Number == 33514) || (this.ShouldUseEnclaveBasedWorkflow && ex2.Errors[i].Number == 33195))
								{
									flag2 = true;
									break;
								}
							}
						}
						if (!flag2)
						{
							if (this._cachedAsyncState != null)
							{
								this._cachedAsyncState.ResetAsyncState();
							}
							this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
							globalCompletion.TrySetException(ex);
						}
						else
						{
							SqlQueryMetadataCache.GetInstance().InvalidateCacheEntry(this);
							if (this.ShouldUseEnclaveBasedWorkflow && this.enclavePackage != null)
							{
								string enclaveDataSource = SqlCommand.GetEnclaveDataSource(this._activeConnection.DataSource, this._activeConnection.Database);
								EnclaveDelegate.Instance.InvalidateEnclaveSession(this._activeConnection.Parser.EnclaveType, enclaveDataSource, this._activeConnection.EnclaveAttestationUrl, this.enclavePackage.EnclaveSession);
							}
							try
							{
								this._internalEndExecuteInitiated = false;
								Task<object> task = (Task<object>)retryFunc(behavior, null, stateObject, TdsParserStaticMethods.GetRemainingTimeout(timeout, firstAttemptStart), true, asyncWrite);
								Task<object> task2 = task;
								Action<Task<object>> continuationAction;
								if ((continuationAction = <>9__1) == null)
								{
									continuationAction = (<>9__1 = delegate(Task<object> retryTsk)
									{
										if (retryTsk.IsFaulted)
										{
											globalCompletion.TrySetException(retryTsk.Exception.InnerException);
											return;
										}
										if (retryTsk.IsCanceled)
										{
											globalCompletion.TrySetCanceled();
											return;
										}
										globalCompletion.TrySetResult(retryTsk.Result);
									});
								}
								task2.ContinueWith(continuationAction, TaskScheduler.Default);
							}
							catch (Exception exception)
							{
								globalCompletion.TrySetException(exception);
							}
						}
					}
				}, TaskScheduler.Default);
				return true;
			}
			return false;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000B4854 File Offset: 0x000B3C54
		private void BeginExecuteReaderInternalReadStage(TaskCompletionSource<object> completion)
		{
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteReader", this._activeConnection);
				this._stateObj.ReadSni(completion);
			}
			catch (OutOfMemoryException ex)
			{
				this._activeConnection.Abort(ex);
				completion.TrySetException(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._activeConnection.Abort(ex2);
				completion.TrySetException(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._activeConnection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(target);
				completion.TrySetException(ex3);
				throw;
			}
			catch (Exception exception)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				completion.TrySetException(exception);
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000B4978 File Offset: 0x000B3D78
		private SqlDataReader InternalEndExecuteReader(IAsyncResult asyncResult, string endMethod, bool isInternal)
		{
			this.VerifyEndExecuteState((Task)asyncResult, endMethod, false);
			this.WaitForAsyncResults(asyncResult, isInternal);
			if (this.IsColumnEncryptionEnabled)
			{
				this.VerifyEndExecuteState((Task)asyncResult, endMethod, true);
			}
			this.CheckThrowSNIException();
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SqlDataReader result;
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				SqlDataReader sqlDataReader = this.CompleteAsyncExecuteReader(isInternal, false);
				result = sqlDataReader;
			}
			catch (OutOfMemoryException e)
			{
				this._activeConnection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._activeConnection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			return result;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000B4A64 File Offset: 0x000B3E64
		public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteNonQueryAsync|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			SqlConnection.ExecutePermission.Demand();
			TaskCompletionSource<int> source = new TaskCompletionSource<int>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(new Action(base.CancelIgnoreFailure));
			}
			Task<int> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<int>(ref task);
				Task<int>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecuteNonQueryAsync), new Func<IAsyncResult, int>(this.EndExecuteNonQueryAsync), null).ContinueWith(delegate(Task<int> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
						return;
					}
					source.SetResult(t.Result);
				}, TaskScheduler.Default);
			}
			catch (Exception exception)
			{
				source.SetException(exception);
			}
			return task;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000B4B68 File Offset: 0x000B3F68
		protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(behavior, cancellationToken).ContinueWith<DbDataReader>(delegate(Task<SqlDataReader> result)
			{
				if (result.IsFaulted)
				{
					throw result.Exception.InnerException;
				}
				return result.Result;
			}, CancellationToken.None, TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000B4BB0 File Offset: 0x000B3FB0
		public new Task<SqlDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000B4BCC File Offset: 0x000B3FCC
		public new Task<SqlDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000B4BE8 File Offset: 0x000B3FE8
		public new Task<SqlDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000B4C00 File Offset: 0x000B4000
		public new Task<SqlDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteReaderAsync|API|Correlation> ObjectID%d#, behavior=%d{ds.CommandBehavior}, ActivityID %ls\n", this.ObjectID, (int)behavior);
			SqlConnection.ExecutePermission.Demand();
			TaskCompletionSource<SqlDataReader> source = new TaskCompletionSource<SqlDataReader>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(new Action(base.CancelIgnoreFailure));
			}
			Task<SqlDataReader> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<SqlDataReader>(ref task);
				Task<SqlDataReader>.Factory.FromAsync<CommandBehavior>(new Func<CommandBehavior, AsyncCallback, object, IAsyncResult>(this.BeginExecuteReaderAsync), new Func<IAsyncResult, SqlDataReader>(this.EndExecuteReaderAsync), behavior, null).ContinueWith(delegate(Task<SqlDataReader> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
						return;
					}
					source.SetResult(t.Result);
				}, TaskScheduler.Default);
			}
			catch (Exception exception)
			{
				source.SetException(exception);
			}
			return task;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000B4D04 File Offset: 0x000B4104
		public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(cancellationToken).ContinueWith<Task<object>>(delegate(Task<SqlDataReader> executeTask)
			{
				TaskCompletionSource<object> source = new TaskCompletionSource<object>();
				if (executeTask.IsCanceled)
				{
					source.SetCanceled();
				}
				else if (executeTask.IsFaulted)
				{
					source.SetException(executeTask.Exception.InnerException);
				}
				else
				{
					SqlDataReader reader = executeTask.Result;
					reader.ReadAsync(cancellationToken).ContinueWith(delegate(Task<bool> readTask)
					{
						try
						{
							if (readTask.IsCanceled)
							{
								reader.Dispose();
								source.SetCanceled();
							}
							else if (readTask.IsFaulted)
							{
								reader.Dispose();
								source.SetException(readTask.Exception.InnerException);
							}
							else
							{
								Exception ex = null;
								object result = null;
								try
								{
									bool result2 = readTask.Result;
									if (result2 && reader.FieldCount > 0)
									{
										try
										{
											result = reader.GetValue(0);
										}
										catch (Exception ex2)
										{
											ex = ex2;
										}
									}
								}
								finally
								{
									reader.Dispose();
								}
								if (ex != null)
								{
									source.SetException(ex);
								}
								else
								{
									source.SetResult(result);
								}
							}
						}
						catch (Exception exception)
						{
							source.SetException(exception);
						}
					}, TaskScheduler.Default);
				}
				return source.Task;
			}, TaskScheduler.Default).Unwrap<object>();
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000B4D48 File Offset: 0x000B4148
		public Task<XmlReader> ExecuteXmlReaderAsync()
		{
			return this.ExecuteXmlReaderAsync(CancellationToken.None);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000B4D60 File Offset: 0x000B4160
		public Task<XmlReader> ExecuteXmlReaderAsync(CancellationToken cancellationToken)
		{
			Bid.CorrelationTrace("<sc.SqlCommand.ExecuteXmlReaderAsync|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			SqlConnection.ExecutePermission.Demand();
			TaskCompletionSource<XmlReader> source = new TaskCompletionSource<XmlReader>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(new Action(base.CancelIgnoreFailure));
			}
			Task<XmlReader> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<XmlReader>(ref task);
				Task<XmlReader>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecuteXmlReaderAsync), new Func<IAsyncResult, XmlReader>(this.EndExecuteXmlReaderAsync), null).ContinueWith(delegate(Task<XmlReader> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
						return;
					}
					source.SetResult(t.Result);
				}, TaskScheduler.Default);
			}
			catch (Exception exception)
			{
				source.SetException(exception);
			}
			return task;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000B4E64 File Offset: 0x000B4264
		private static string UnquoteProcedurePart(string part)
		{
			if (part != null && 2 <= part.Length && '[' == part[0] && ']' == part[part.Length - 1])
			{
				part = part.Substring(1, part.Length - 2);
				part = part.Replace("]]", "]");
			}
			return part;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000B4EC0 File Offset: 0x000B42C0
		private static string UnquoteProcedureName(string name, out object groupNumber)
		{
			groupNumber = null;
			string text = name;
			if (text != null)
			{
				if (char.IsDigit(text[text.Length - 1]))
				{
					int num = text.LastIndexOf(';');
					if (num != -1)
					{
						string s = text.Substring(num + 1);
						int num2 = 0;
						if (int.TryParse(s, out num2))
						{
							groupNumber = num2;
							text = text.Substring(0, num);
						}
					}
				}
				text = SqlCommand.UnquoteProcedurePart(text);
			}
			return text;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000B4F28 File Offset: 0x000B4328
		internal void DeriveParameters()
		{
			CommandType commandType = this.CommandType;
			if (commandType == CommandType.Text)
			{
				throw ADP.DeriveParametersNotSupported(this);
			}
			if (commandType != CommandType.StoredProcedure)
			{
				if (commandType != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(this.CommandType);
				}
				throw ADP.DeriveParametersNotSupported(this);
			}
			else
			{
				this.ValidateCommand("DeriveParameters", false);
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this.CommandText, "[\"", "]\"", "SQL_SqlCommandCommandText", false);
				if (array[3] == null || ADP.IsEmpty(array[3]))
				{
					throw ADP.NoStoredProcedureExists(this.CommandText);
				}
				SqlCommand sqlCommand = null;
				StringBuilder stringBuilder = new StringBuilder();
				if (!ADP.IsEmpty(array[0]))
				{
					SqlCommandSet.BuildStoredProcedureName(stringBuilder, array[0]);
					stringBuilder.Append(".");
				}
				if (ADP.IsEmpty(array[1]))
				{
					array[1] = this.Connection.Database;
				}
				SqlCommandSet.BuildStoredProcedureName(stringBuilder, array[1]);
				stringBuilder.Append(".");
				string[] array2;
				bool flag;
				if (this.Connection.IsKatmaiOrNewer)
				{
					stringBuilder.Append("[sys].[").Append("sp_procedure_params_100_managed").Append("]");
					array2 = SqlCommand.KatmaiProcParamsNames;
					flag = true;
				}
				else
				{
					if (this.Connection.IsYukonOrNewer)
					{
						stringBuilder.Append("[sys].[").Append("sp_procedure_params_managed").Append("]");
					}
					else
					{
						stringBuilder.Append(".[").Append("sp_procedure_params_rowset").Append("]");
					}
					array2 = SqlCommand.PreKatmaiProcParamsNames;
					flag = false;
				}
				sqlCommand = new SqlCommand(stringBuilder.ToString(), this.Connection, this.Transaction);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.Add(new SqlParameter("@procedure_name", SqlDbType.NVarChar, 255));
				object obj;
				sqlCommand.Parameters[0].Value = SqlCommand.UnquoteProcedureName(array[3], out obj);
				if (obj != null)
				{
					SqlParameter sqlParameter = sqlCommand.Parameters.Add(new SqlParameter("@group_number", SqlDbType.Int));
					sqlParameter.Value = obj;
				}
				if (!ADP.IsEmpty(array[2]))
				{
					SqlParameter sqlParameter2 = sqlCommand.Parameters.Add(new SqlParameter("@procedure_schema", SqlDbType.NVarChar, 255));
					sqlParameter2.Value = SqlCommand.UnquoteProcedurePart(array[2]);
				}
				SqlDataReader sqlDataReader = null;
				List<SqlParameter> list = new List<SqlParameter>();
				bool flag2 = true;
				try
				{
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						SqlParameter sqlParameter3 = new SqlParameter();
						sqlParameter3.ParameterName = (string)sqlDataReader[array2[0]];
						if (flag)
						{
							sqlParameter3.SqlDbType = (SqlDbType)((short)sqlDataReader[array2[3]]);
							SqlDbType sqlDbType = sqlParameter3.SqlDbType;
							if (sqlDbType <= SqlDbType.NText)
							{
								if (sqlDbType != SqlDbType.Image)
								{
									if (sqlDbType != SqlDbType.NText)
									{
										goto IL_2F3;
									}
									sqlParameter3.SqlDbType = SqlDbType.NVarChar;
									goto IL_2F3;
								}
							}
							else
							{
								if (sqlDbType == SqlDbType.Text)
								{
									sqlParameter3.SqlDbType = SqlDbType.VarChar;
									goto IL_2F3;
								}
								if (sqlDbType != SqlDbType.Timestamp)
								{
									goto IL_2F3;
								}
							}
							sqlParameter3.SqlDbType = SqlDbType.VarBinary;
						}
						else
						{
							sqlParameter3.SqlDbType = MetaType.GetSqlDbTypeFromOleDbType((short)sqlDataReader[array2[2]], ADP.IsNull(sqlDataReader[array2[9]]) ? ADP.StrEmpty : ((string)sqlDataReader[array2[9]]));
						}
						IL_2F3:
						object obj2 = sqlDataReader[array2[4]];
						if (obj2 is int)
						{
							int num = (int)obj2;
							if (num == 0 && (sqlParameter3.SqlDbType == SqlDbType.NVarChar || sqlParameter3.SqlDbType == SqlDbType.VarBinary || sqlParameter3.SqlDbType == SqlDbType.VarChar))
							{
								num = -1;
							}
							sqlParameter3.Size = num;
						}
						sqlParameter3.Direction = this.ParameterDirectionFromOleDbDirection((short)sqlDataReader[array2[1]]);
						if (sqlParameter3.SqlDbType == SqlDbType.Decimal)
						{
							sqlParameter3.ScaleInternal = (byte)((short)sqlDataReader[array2[6]] & 255);
							sqlParameter3.PrecisionInternal = (byte)((short)sqlDataReader[array2[5]] & 255);
						}
						if (SqlDbType.Udt == sqlParameter3.SqlDbType)
						{
							string text;
							if (flag)
							{
								text = (string)sqlDataReader[array2[9]];
							}
							else
							{
								text = (string)sqlDataReader[array2[13]];
							}
							SqlParameter sqlParameter4 = sqlParameter3;
							string[] array3 = new string[5];
							int num2 = 0;
							object obj3 = sqlDataReader[array2[7]];
							array3[num2] = ((obj3 != null) ? obj3.ToString() : null);
							array3[1] = ".";
							int num3 = 2;
							object obj4 = sqlDataReader[array2[8]];
							array3[num3] = ((obj4 != null) ? obj4.ToString() : null);
							array3[3] = ".";
							array3[4] = text;
							sqlParameter4.UdtTypeName = string.Concat(array3);
						}
						if (SqlDbType.Structured == sqlParameter3.SqlDbType)
						{
							SqlParameter sqlParameter5 = sqlParameter3;
							string[] array4 = new string[5];
							int num4 = 0;
							object obj5 = sqlDataReader[array2[7]];
							array4[num4] = ((obj5 != null) ? obj5.ToString() : null);
							array4[1] = ".";
							int num5 = 2;
							object obj6 = sqlDataReader[array2[8]];
							array4[num5] = ((obj6 != null) ? obj6.ToString() : null);
							array4[3] = ".";
							int num6 = 4;
							object obj7 = sqlDataReader[array2[9]];
							array4[num6] = ((obj7 != null) ? obj7.ToString() : null);
							sqlParameter5.TypeName = string.Concat(array4);
						}
						if (SqlDbType.Xml == sqlParameter3.SqlDbType)
						{
							object obj8 = sqlDataReader[array2[10]];
							sqlParameter3.XmlSchemaCollectionDatabase = (ADP.IsNull(obj8) ? string.Empty : ((string)obj8));
							obj8 = sqlDataReader[array2[11]];
							sqlParameter3.XmlSchemaCollectionOwningSchema = (ADP.IsNull(obj8) ? string.Empty : ((string)obj8));
							obj8 = sqlDataReader[array2[12]];
							sqlParameter3.XmlSchemaCollectionName = (ADP.IsNull(obj8) ? string.Empty : ((string)obj8));
						}
						if (MetaType._IsVarTime(sqlParameter3.SqlDbType))
						{
							object obj9 = sqlDataReader[array2[14]];
							if (obj9 is int)
							{
								sqlParameter3.ScaleInternal = (byte)((int)obj9 & 255);
							}
						}
						list.Add(sqlParameter3);
					}
				}
				catch (Exception e)
				{
					flag2 = ADP.IsCatchableExceptionType(e);
					throw;
				}
				finally
				{
					if (flag2)
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						sqlCommand.Connection = null;
					}
				}
				if (list.Count == 0)
				{
					throw ADP.NoStoredProcedureExists(this.CommandText);
				}
				this.Parameters.Clear();
				foreach (SqlParameter value in list)
				{
					this._parameters.Add(value);
				}
				return;
			}
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000B556C File Offset: 0x000B496C
		private ParameterDirection ParameterDirectionFromOleDbDirection(short oledbDirection)
		{
			switch (oledbDirection)
			{
			case 2:
				return ParameterDirection.InputOutput;
			case 3:
				return ParameterDirection.Output;
			case 4:
				return ParameterDirection.ReturnValue;
			default:
				return ParameterDirection.Input;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x000B5598 File Offset: 0x000B4998
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._cachedMetaData;
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x000B55AC File Offset: 0x000B49AC
		private void CheckNotificationStateAndAutoEnlist()
		{
			if (this.NotificationAutoEnlist && this._activeConnection.IsYukonOrNewer)
			{
				string text = SqlCommand.SqlNotificationContext();
				if (!ADP.IsEmpty(text))
				{
					SqlDependency sqlDependency = SqlDependencyPerAppDomainDispatcher.SingletonInstance.LookupDependencyEntry(text);
					if (sqlDependency != null)
					{
						sqlDependency.AddCommandDependency(this);
					}
				}
			}
			if (this.Notification != null && this._sqlDep != null)
			{
				if (this._sqlDep.Options == null)
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
					SqlDependency.IdentityUserNamePair identityUser;
					if (sqlInternalConnectionTds.Identity != null)
					{
						identityUser = new SqlDependency.IdentityUserNamePair(sqlInternalConnectionTds.Identity, null);
					}
					else
					{
						identityUser = new SqlDependency.IdentityUserNamePair(null, sqlInternalConnectionTds.ConnectionOptions.UserID);
					}
					this.Notification.Options = SqlDependency.GetDefaultComposedOptions(this._activeConnection.DataSource, this.InternalTdsConnection.ServerProvidedFailOverPartner, identityUser, this._activeConnection.Database);
				}
				this.Notification.UserData = this._sqlDep.ComputeHashAndAddToDispatcher(this);
				this._sqlDep.AddToServerList(this._activeConnection.DataSource);
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000B56B0 File Offset: 0x000B4AB0
		[SecurityPermission(SecurityAction.Assert, Infrastructure = true)]
		internal static string SqlNotificationContext()
		{
			return CallContext.GetData("MS.SqlDependencyCookie") as string;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000B56CC File Offset: 0x000B4ACC
		private Task RunExecuteNonQueryTds(string methodName, bool async, int timeout, bool asyncWrite)
		{
			bool flag = true;
			try
			{
				Task task = this._activeConnection.ValidateAndReconnect(null, timeout);
				if (task != null)
				{
					long reconnectionStart = ADP.TimerCurrent();
					if (async)
					{
						TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
						this._activeConnection.RegisterWaitingForReconnect(completion.Task);
						this._reconnectionCompletionSource = completion;
						CancellationTokenSource timeoutCTS = new CancellationTokenSource();
						AsyncHelper.SetTimeoutException(completion, timeout, new Func<Exception>(SQL.CR_ReconnectTimeout), timeoutCTS.Token);
						Action <>9__2;
						AsyncHelper.ContinueTask(task, completion, delegate
						{
							TaskCompletionSource<object> completion;
							if (completion.Task.IsCompleted)
							{
								return;
							}
							Interlocked.CompareExchange<TaskCompletionSource<object>>(ref this._reconnectionCompletionSource, null, completion);
							timeoutCTS.Cancel();
							Task task3 = this.RunExecuteNonQueryTds(methodName, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart), asyncWrite);
							if (task3 == null)
							{
								completion.SetResult(null);
								return;
							}
							Task task4 = task3;
							completion = completion;
							Action onSuccess;
							if ((onSuccess = <>9__2) == null)
							{
								onSuccess = (<>9__2 = delegate()
								{
									completion.SetResult(null);
								});
							}
							AsyncHelper.ContinueTask(task4, completion, onSuccess, null, null, null, null, null);
						}, null, null, null, null, this._activeConnection);
						return completion.Task;
					}
					AsyncHelper.WaitForCompletion(task, timeout, delegate
					{
						throw SQL.CR_ReconnectTimeout();
					}, true);
					timeout = TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart);
				}
				if (asyncWrite)
				{
					this._activeConnection.AddWeakReference(this, 2);
				}
				this.GetStateObject(null);
				this.ResetEncryptionState();
				Bid.Trace("<sc.SqlCommand.ExecuteNonQuery|INFO> %d#, Command executed as SQLBATCH.\n", this.ObjectID);
				Task task2 = this._stateObj.Parser.TdsExecuteSQLBatch(this.CommandText, timeout, this.Notification, this._stateObj, true, false, null);
				this.NotifyDependency();
				bool flag2;
				if (async)
				{
					this._activeConnection.GetOpenTdsConnection(methodName).IncrementAsyncCount();
				}
				else if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag2))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				if (flag && !async)
				{
					this.PutStateObject();
				}
			}
			return null;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000B5914 File Offset: 0x000B4D14
		private void RunExecuteNonQuerySmi(bool sendToPipe)
		{
			SqlInternalConnectionSmi internalSmiConnection = this.InternalSmiConnection;
			SmiRequestExecutor smiRequestExecutor = null;
			try
			{
				smiRequestExecutor = this.SetUpSmiRequest(internalSmiConnection);
				SmiExecuteType executeType;
				if (sendToPipe)
				{
					executeType = SmiExecuteType.ToPipe;
				}
				else
				{
					executeType = SmiExecuteType.NonQuery;
				}
				SmiEventStream smiEventStream = null;
				bool flag = true;
				try
				{
					long num;
					Transaction associatedTransaction;
					internalSmiConnection.GetCurrentTransactionPair(out num, out associatedTransaction);
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.SqlCommand.RunExecuteNonQuerySmi|ADV> %d#, innerConnection=%d#, transactionId=0x%I64x, cmdBehavior=%d.\n", this.ObjectID, internalSmiConnection.ObjectID, num, 0);
					}
					if (SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL)
					{
						smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, associatedTransaction, CommandBehavior.Default, executeType);
					}
					else
					{
						smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, CommandBehavior.Default, executeType);
					}
					while (smiEventStream.HasEvents)
					{
						smiEventStream.ProcessEvent(this.EventSink);
					}
				}
				catch (Exception e)
				{
					flag = ADP.IsCatchableExceptionType(e);
					throw;
				}
				finally
				{
					if (smiEventStream != null && flag)
					{
						smiEventStream.Close(this.EventSink);
					}
				}
				this.EventSink.ProcessMessagesAndThrow();
			}
			finally
			{
				if (smiRequestExecutor != null)
				{
					smiRequestExecutor.Close(this.EventSink);
					this.EventSink.ProcessMessagesAndThrow(true);
				}
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000B5A58 File Offset: 0x000B4E58
		private void ResetEncryptionState()
		{
			this.ClearDescribeParameterEncryptionRequests();
			this._internalEndExecuteInitiated = false;
			this.CachingQueryMetadataPostponed = false;
			if (this._parameters != null)
			{
				for (int i = 0; i < this._parameters.Count; i++)
				{
					this._parameters[i].CipherMetadata = null;
					this._parameters[i].HasReceivedMetadata = false;
				}
			}
			this.keysToBeSentToEnclave.Clear();
			this.enclavePackage = null;
			this.requiresEnclaveComputations = false;
			this.enclaveAttestationParameters = null;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000B5AE0 File Offset: 0x000B4EE0
		private void PrepareTransparentEncryptionFinallyBlock(bool closeDataReader, bool clearDataStructures, bool decrementAsyncCount, bool wasDescribeParameterEncryptionNeeded, ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap, SqlDataReader describeParameterEncryptionDataReader)
		{
			if (clearDataStructures)
			{
				this.ClearDescribeParameterEncryptionRequests();
				if (describeParameterEncryptionRpcOriginalRpcMap != null)
				{
					describeParameterEncryptionRpcOriginalRpcMap = null;
				}
			}
			if (decrementAsyncCount)
			{
				SqlInternalConnectionTds openTdsConnection = this._activeConnection.GetOpenTdsConnection();
				if (openTdsConnection != null)
				{
					openTdsConnection.DecrementAsyncCount();
				}
			}
			if (closeDataReader && describeParameterEncryptionDataReader != null)
			{
				describeParameterEncryptionDataReader.Close();
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000B5B24 File Offset: 0x000B4F24
		private void PrepareForTransparentEncryption(CommandBehavior cmdBehavior, bool returnStream, bool async, int timeout, TaskCompletionSource<object> completion, out Task returnTask, bool asyncWrite, out bool usedCache, bool inRetry)
		{
			Task task = null;
			bool describeParameterEncryptionNeeded = false;
			SqlDataReader describeParameterEncryptionDataReader = null;
			returnTask = null;
			usedCache = false;
			if (!this.BatchRPCMode && !inRetry && this._parameters != null && this._parameters.Count > 0 && SqlQueryMetadataCache.GetInstance().GetQueryMetadataIfExists(this))
			{
				usedCache = true;
				return;
			}
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap = null;
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				try
				{
					describeParameterEncryptionDataReader = this.TryFetchInputParameterEncryptionInfo(timeout, async, asyncWrite, out describeParameterEncryptionNeeded, out task, out describeParameterEncryptionRpcOriginalRpcMap);
					if (describeParameterEncryptionNeeded)
					{
						flag2 = async;
						if (task != null)
						{
							flag = false;
							returnTask = AsyncHelper.CreateContinuationTask(task, delegate()
							{
								bool flag4 = true;
								bool flag5 = true;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									this.CheckThrowSNIException();
									SqlInternalConnectionTds openTdsConnection = this._activeConnection.GetOpenTdsConnection();
									if (openTdsConnection != null)
									{
										openTdsConnection.DecrementAsyncCount();
										flag5 = false;
									}
									describeParameterEncryptionDataReader = this.CompleteAsyncExecuteReader(false, true);
									this.ReadDescribeEncryptionParameterResults(describeParameterEncryptionDataReader, describeParameterEncryptionRpcOriginalRpcMap);
								}
								catch (Exception e6)
								{
									flag4 = ADP.IsCatchableExceptionType(e6);
									throw;
								}
								finally
								{
									SqlCommand <>4__this = this;
									bool closeDataReader2 = flag4;
									bool decrementAsyncCount2 = flag5;
									<>4__this.PrepareTransparentEncryptionFinallyBlock(closeDataReader2, flag4, decrementAsyncCount2, describeParameterEncryptionNeeded, describeParameterEncryptionRpcOriginalRpcMap, describeParameterEncryptionDataReader);
								}
							}, null, delegate(Exception exception)
							{
								if (this._cachedAsyncState != null)
								{
									this._cachedAsyncState.ResetAsyncState();
								}
								if (exception != null)
								{
									throw exception;
								}
							});
							flag2 = false;
						}
						else if (async)
						{
							flag = false;
							returnTask = Task.Run(delegate()
							{
								bool flag4 = true;
								bool flag5 = true;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									this.CheckThrowSNIException();
									SqlInternalConnectionTds openTdsConnection = this._activeConnection.GetOpenTdsConnection();
									if (openTdsConnection != null)
									{
										openTdsConnection.DecrementAsyncCount();
										flag5 = false;
									}
									describeParameterEncryptionDataReader = this.CompleteAsyncExecuteReader(false, true);
									this.ReadDescribeEncryptionParameterResults(describeParameterEncryptionDataReader, describeParameterEncryptionRpcOriginalRpcMap);
								}
								catch (Exception e6)
								{
									flag4 = ADP.IsCatchableExceptionType(e6);
									throw;
								}
								finally
								{
									SqlCommand <>4__this = this;
									bool closeDataReader2 = flag4;
									bool decrementAsyncCount2 = flag5;
									<>4__this.PrepareTransparentEncryptionFinallyBlock(closeDataReader2, flag4, decrementAsyncCount2, describeParameterEncryptionNeeded, describeParameterEncryptionRpcOriginalRpcMap, describeParameterEncryptionDataReader);
								}
							});
							flag2 = false;
						}
						else
						{
							this.ReadDescribeEncryptionParameterResults(describeParameterEncryptionDataReader, describeParameterEncryptionRpcOriginalRpcMap);
						}
					}
				}
				catch (Exception e)
				{
					flag = ADP.IsCatchableExceptionType(e);
					flag3 = true;
					throw;
				}
				finally
				{
					bool closeDataReader = (flag && !async) || flag3;
					bool decrementAsyncCount = flag2 && flag3;
					this.PrepareTransparentEncryptionFinallyBlock(closeDataReader, (flag && !async) || flag3, decrementAsyncCount, describeParameterEncryptionNeeded, describeParameterEncryptionRpcOriginalRpcMap, describeParameterEncryptionDataReader);
				}
			}
			catch (OutOfMemoryException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (StackOverflowException e3)
			{
				this._activeConnection.Abort(e3);
				throw;
			}
			catch (ThreadAbortException e4)
			{
				this._activeConnection.Abort(e4);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			catch (Exception e5)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(e5))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000B5D84 File Offset: 0x000B5184
		private SqlDataReader TryFetchInputParameterEncryptionInfo(int timeout, bool async, bool asyncWrite, out bool inputParameterEncryptionNeeded, out Task task, out ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap)
		{
			inputParameterEncryptionNeeded = false;
			task = null;
			describeParameterEncryptionRpcOriginalRpcMap = null;
			byte[] array = null;
			if (this.ShouldUseEnclaveBasedWorkflow)
			{
				string enclaveType = this._activeConnection.Parser.EnclaveType;
				string dataSource = this._activeConnection.DataSource;
				string enclaveDataSource = SqlCommand.GetEnclaveDataSource(this._activeConnection.DataSource, this._activeConnection.Database);
				string enclaveAttestationUrl = this._activeConnection.EnclaveAttestationUrl;
				SqlEnclaveSession sqlEnclaveSession = null;
				EnclaveDelegate.Instance.GetEnclaveSession(enclaveType, enclaveDataSource, enclaveAttestationUrl, out sqlEnclaveSession);
				if (sqlEnclaveSession == null)
				{
					this.enclaveAttestationParameters = EnclaveDelegate.Instance.GetAttestationParameters(enclaveType, dataSource, enclaveAttestationUrl);
					array = EnclaveDelegate.Instance.GetSerializedAttestationParameters(this.enclaveAttestationParameters, enclaveType);
				}
			}
			if (this.BatchRPCMode)
			{
				Dictionary<_SqlRPC, _SqlRPC> dictionary = new Dictionary<_SqlRPC, _SqlRPC>();
				for (int i = 0; i < this._SqlRPCBatchArray.Length; i++)
				{
					if (this._SqlRPCBatchArray[i].parameters.Length > 1)
					{
						this._SqlRPCBatchArray[i].needsFetchParameterEncryptionMetadata = true;
						_SqlRPC key = new _SqlRPC();
						this.PrepareDescribeParameterEncryptionRequest(this._SqlRPCBatchArray[i], ref key, (i == 0) ? array : null);
						dictionary.Add(key, this._SqlRPCBatchArray[i]);
					}
				}
				describeParameterEncryptionRpcOriginalRpcMap = new ReadOnlyDictionary<_SqlRPC, _SqlRPC>(dictionary);
				if (describeParameterEncryptionRpcOriginalRpcMap.Count == 0)
				{
					return null;
				}
				inputParameterEncryptionNeeded = true;
				this._sqlRPCParameterEncryptionReqArray = describeParameterEncryptionRpcOriginalRpcMap.Keys.ToArray<_SqlRPC>();
			}
			else if (this.ShouldUseEnclaveBasedWorkflow || this.GetParameterCount(this._parameters) != 0)
			{
				inputParameterEncryptionNeeded = true;
				this._sqlRPCParameterEncryptionReqArray = new _SqlRPC[1];
				_SqlRPC sqlRPC = null;
				this.GetRPCObject(this.GetParameterCount(this._parameters), ref sqlRPC, false);
				sqlRPC.rpcName = this.CommandText;
				int num = 0;
				if (this._parameters != null)
				{
					foreach (object obj in this._parameters)
					{
						SqlParameter sqlParameter = (SqlParameter)obj;
						sqlRPC.parameters[num++] = sqlParameter;
					}
				}
				this.PrepareDescribeParameterEncryptionRequest(sqlRPC, ref this._sqlRPCParameterEncryptionReqArray[0], array);
			}
			if (inputParameterEncryptionNeeded)
			{
				this._isDescribeParameterEncryptionRPCCurrentlyInProgress = true;
				return this.RunExecuteReaderTds(CommandBehavior.Default, RunBehavior.ReturnImmediately, true, async, timeout, out task, asyncWrite, false, null, true);
			}
			return null;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000B5FCC File Offset: 0x000B53CC
		private SqlParameter GetSqlParameterWithQueryText(string queryText)
		{
			return new SqlParameter(null, (queryText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, queryText.Length)
			{
				Value = queryText
			};
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000B6004 File Offset: 0x000B5404
		private void PrepareDescribeParameterEncryptionRequest(_SqlRPC originalRpcRequest, ref _SqlRPC describeParameterEncryptionRequest, byte[] attestationParameters = null)
		{
			this.GetRPCObject((attestationParameters == null) ? 2 : 3, ref describeParameterEncryptionRequest, true);
			describeParameterEncryptionRequest.rpcName = "sp_describe_parameter_encryption";
			SqlParameter sqlParameter;
			if (this.BatchRPCMode)
			{
				string text = (string)originalRpcRequest.parameters[0].Value;
				sqlParameter = this.GetSqlParameterWithQueryText(text);
			}
			else
			{
				string text = originalRpcRequest.rpcName;
				if (this.CommandType == CommandType.StoredProcedure)
				{
					sqlParameter = this.BuildStoredProcedureStatementForColumnEncryption(text, originalRpcRequest.parameters);
				}
				else
				{
					sqlParameter = this.GetSqlParameterWithQueryText(text);
				}
			}
			describeParameterEncryptionRequest.parameters[0] = sqlParameter;
			string text2 = null;
			if (this.BatchRPCMode)
			{
				if (originalRpcRequest.parameters.Length > 1)
				{
					text2 = (string)originalRpcRequest.parameters[1].Value;
				}
			}
			else
			{
				SqlParameterCollection sqlParameterCollection = new SqlParameterCollection();
				if (this._parameters != null)
				{
					for (int i = 0; i < this._parameters.Count; i++)
					{
						SqlParameter sqlParameter2 = originalRpcRequest.parameters[i];
						sqlParameterCollection.Add(new SqlParameter(sqlParameter2.ParameterName, sqlParameter2.SqlDbType, sqlParameter2.Size, sqlParameter2.Direction, sqlParameter2.Precision, sqlParameter2.Scale, sqlParameter2.SourceColumn, sqlParameter2.SourceVersion, sqlParameter2.SourceColumnNullMapping, sqlParameter2.Value, sqlParameter2.XmlSchemaCollectionDatabase, sqlParameter2.XmlSchemaCollectionOwningSchema, sqlParameter2.XmlSchemaCollectionName)
						{
							CompareInfo = sqlParameter2.CompareInfo,
							TypeName = sqlParameter2.TypeName,
							UdtTypeName = sqlParameter2.UdtTypeName,
							IsNullable = sqlParameter2.IsNullable,
							LocaleId = sqlParameter2.LocaleId,
							Offset = sqlParameter2.Offset
						});
					}
				}
				TdsParser tdsParser = null;
				if (this._activeConnection.Parser != null)
				{
					tdsParser = this._activeConnection.Parser;
					if (tdsParser == null || tdsParser.State == TdsParserState.Broken || tdsParser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
				}
				text2 = this.BuildParamList(tdsParser, sqlParameterCollection, true);
			}
			sqlParameter = new SqlParameter(null, (text2.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, text2.Length);
			sqlParameter.Value = text2;
			describeParameterEncryptionRequest.parameters[1] = sqlParameter;
			if (attestationParameters != null)
			{
				SqlParameter sqlParameter3 = new SqlParameter(null, SqlDbType.VarBinary)
				{
					Direction = ParameterDirection.Input,
					Size = attestationParameters.Length,
					Value = attestationParameters
				};
				describeParameterEncryptionRequest.parameters[2] = sqlParameter3;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000B6244 File Offset: 0x000B5644
		private void ReadDescribeEncryptionParameterResults(SqlDataReader ds, ReadOnlyDictionary<_SqlRPC, _SqlRPC> describeParameterEncryptionRpcOriginalRpcMap)
		{
			_SqlRPC sqlRPC = null;
			Dictionary<int, SqlTceCipherInfoEntry> dictionary = new Dictionary<int, SqlTceCipherInfoEntry>();
			int num = 0;
			while (!this.BatchRPCMode || num < this._sqlRPCParameterEncryptionReqArray.Length)
			{
				bool flag = true;
				while (ds.Read())
				{
					int @int = ds.GetInt32(0);
					SqlTceCipherInfoEntry value;
					if (!dictionary.TryGetValue(@int, out value))
					{
						value = new SqlTceCipherInfoEntry(@int);
						dictionary.Add(@int, value);
					}
					byte[] array = null;
					int num2 = (int)ds.GetBytes(5, 0L, array, 0, 0);
					array = new byte[num2];
					ds.GetBytes(5, 0L, array, 0, num2);
					byte[] array2 = new byte[8];
					ds.GetBytes(4, 0L, array2, 0, array2.Length);
					string @string = ds.GetString(6);
					string string2 = ds.GetString(7);
					value.Add(array, ds.GetInt32(1), ds.GetInt32(2), ds.GetInt32(3), array2, string2, @string, ds.GetString(8));
					bool flag2 = false;
					if (this._activeConnection.Parser.TceVersionSupported >= 2)
					{
						flag2 = ds.GetBoolean(9);
					}
					else
					{
						flag = false;
					}
					if (flag2)
					{
						if (string.IsNullOrWhiteSpace(this.Connection.EnclaveAttestationUrl))
						{
							throw SQL.NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe(this._activeConnection.Parser.EnclaveType);
						}
						byte[] array3 = null;
						if (!ds.IsDBNull(10))
						{
							int num3 = (int)ds.GetBytes(10, 0L, array3, 0, 0);
							array3 = new byte[num3];
							ds.GetBytes(10, 0L, array3, 0, num3);
						}
						string dataSource = this._activeConnection.DataSource;
						SqlSecurityUtility.VerifyColumnMasterKeySignature(@string, string2, dataSource, flag2, array3);
						int num4 = @int;
						SqlTceCipherInfoEntry value2;
						if (!dictionary.TryGetValue(num4, out value2))
						{
							throw SQL.InvalidEncryptionKeyOrdinalEnclaveMetadata(num4, dictionary.Count);
						}
						if (!this.keysToBeSentToEnclave.ContainsKey(@int))
						{
							this.keysToBeSentToEnclave.Add(@int, value2);
						}
						this.requiresEnclaveComputations = true;
					}
				}
				if (!flag && !ds.NextResult())
				{
					throw SQL.UnexpectedDescribeParamFormatParameterMetadata();
				}
				if (this.BatchRPCMode)
				{
					sqlRPC = null;
					bool flag3 = describeParameterEncryptionRpcOriginalRpcMap.TryGetValue(this._sqlRPCParameterEncryptionReqArray[num++], out sqlRPC);
				}
				else
				{
					sqlRPC = this._rpcArrayOf1[0];
				}
				int num5 = this.BatchRPCMode ? 2 : 0;
				if (flag)
				{
					if (!ds.NextResult())
					{
						goto IL_319;
					}
				}
				int num6;
				while (ds.Read())
				{
					string string3 = ds.GetString(1);
					num6 = num5;
					while (num6 < sqlRPC.parameters.Length && sqlRPC.parameters[num6] != null)
					{
						SqlParameter sqlParameter = sqlRPC.parameters[num6];
						if (sqlParameter.ParameterNameFixed.Equals(string3, StringComparison.Ordinal))
						{
							sqlParameter.HasReceivedMetadata = true;
							byte @byte = ds.GetByte(3);
							if (@byte == 0)
							{
								break;
							}
							byte byte2 = ds.GetByte(2);
							int int2 = ds.GetInt32(4);
							byte byte3 = ds.GetByte(5);
							SqlTceCipherInfoEntry value;
							if (!dictionary.TryGetValue(int2, out value))
							{
								throw SQL.InvalidEncryptionKeyOrdinalParameterMetadata(int2, dictionary.Count);
							}
							sqlParameter.CipherMetadata = new SqlCipherMetadata(new SqlTceCipherInfoEntry?(value), ushort.MaxValue, byte2, null, @byte, byte3);
							SqlSecurityUtility.DecryptSymmetricKey(sqlParameter.CipherMetadata, this._activeConnection.DataSource);
							byte[] paramoptions = sqlRPC.paramoptions;
							int num7 = num6;
							paramoptions[num7] |= 8;
							break;
						}
						else
						{
							num6++;
						}
					}
				}
				IL_319:
				num6 = num5;
				while (num6 < sqlRPC.parameters.Length && sqlRPC.parameters[num6] != null)
				{
					if (!sqlRPC.parameters[num6].HasReceivedMetadata && sqlRPC.parameters[num6].Direction != ParameterDirection.ReturnValue)
					{
						throw SQL.ParamEncryptionMetadataMissing(sqlRPC.parameters[num6].ParameterName, sqlRPC.GetCommandTextOrRpcName());
					}
					num6++;
				}
				if (this.ShouldUseEnclaveBasedWorkflow && this.enclaveAttestationParameters != null && this.requiresEnclaveComputations)
				{
					if (!ds.NextResult())
					{
						throw SQL.UnexpectedDescribeParamFormatAttestationInfo(this._activeConnection.Parser.EnclaveType);
					}
					bool flag4 = false;
					while (ds.Read())
					{
						if (flag4)
						{
							throw SQL.MultipleRowsReturnedForAttestationInfo();
						}
						int num8 = (int)ds.GetBytes(0, 0L, null, 0, 0);
						byte[] array4 = new byte[num8];
						ds.GetBytes(0, 0L, array4, 0, num8);
						string enclaveType = this._activeConnection.Parser.EnclaveType;
						string enclaveDataSource = SqlCommand.GetEnclaveDataSource(this._activeConnection.DataSource, this._activeConnection.Database);
						string enclaveAttestationUrl = this._activeConnection.EnclaveAttestationUrl;
						EnclaveDelegate.Instance.CreateEnclaveSession(enclaveType, enclaveDataSource, enclaveAttestationUrl, array4, this.enclaveAttestationParameters);
						this.enclaveAttestationParameters = null;
						flag4 = true;
					}
					if (!flag4)
					{
						throw SQL.AttestationInfoNotReturnedFromSqlServer(this._activeConnection.Parser.EnclaveType, this._activeConnection.EnclaveAttestationUrl);
					}
				}
				sqlRPC.needsFetchParameterEncryptionMetadata = false;
				if (!ds.NextResult())
				{
					break;
				}
			}
			if (this.BatchRPCMode)
			{
				for (int i = 0; i < this._SqlRPCBatchArray.Length; i++)
				{
					if (this._SqlRPCBatchArray[i].needsFetchParameterEncryptionMetadata)
					{
						throw SQL.ProcEncryptionMetadataMissing(this._SqlRPCBatchArray[i].rpcName);
					}
				}
			}
			if (!this.BatchRPCMode && !this.requiresEnclaveComputations && this._parameters != null && this._parameters.Count > 0)
			{
				SqlQueryMetadataCache.GetInstance().AddQueryMetadata(this, true);
			}
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000B6750 File Offset: 0x000B5B50
		internal SqlDataReader RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, string method)
		{
			Task task;
			bool flag;
			return this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, method, null, this.CommandTimeout, out task, out flag, false, false);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000B6778 File Offset: 0x000B5B78
		internal SqlDataReader RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, string method, TaskCompletionSource<object> completion, int timeout, out Task task, out bool usedCache, bool asyncWrite = false, bool inRetry = false)
		{
			bool flag = completion != null;
			usedCache = false;
			task = null;
			this._rowsAffected = -1;
			this._rowsAffectedBySpDescribeParameterEncryption = -1;
			if ((CommandBehavior.SingleRow & cmdBehavior) != CommandBehavior.Default)
			{
				cmdBehavior |= CommandBehavior.SingleResult;
			}
			if (!inRetry)
			{
				this.ValidateCommand(method, flag);
			}
			this.CheckNotificationStateAndAutoEnlist();
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SqlDataReader result;
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				SqlStatistics statistics = this.Statistics;
				if (statistics != null)
				{
					if ((!this.IsDirty && this.IsPrepared && !this._hiddenPrepare) || (this.IsPrepared && this._execType == SqlCommand.EXECTYPE.PREPAREPENDING))
					{
						statistics.SafeIncrement(ref statistics._preparedExecs);
					}
					else
					{
						statistics.SafeIncrement(ref statistics._unpreparedExecs);
					}
				}
				this.ResetEncryptionState();
				if (this._activeConnection.IsContextConnection)
				{
					result = this.RunExecuteReaderSmi(cmdBehavior, runBehavior, returnStream);
				}
				else
				{
					if (this.IsColumnEncryptionEnabled)
					{
						Task describeParameterEncryptionTask = null;
						this.PrepareForTransparentEncryption(cmdBehavior, returnStream, flag, timeout, completion, out describeParameterEncryptionTask, asyncWrite && flag, out usedCache, inRetry);
						long start = ADP.TimerCurrent();
						try
						{
							return this.RunExecuteReaderTdsWithTransparentParameterEncryption(cmdBehavior, runBehavior, returnStream, flag, timeout, out task, asyncWrite && flag, inRetry, null, false, describeParameterEncryptionTask);
						}
						catch (EnclaveDelegate.RetriableEnclaveQueryExecutionException)
						{
							if (inRetry || flag)
							{
								throw;
							}
							SqlQueryMetadataCache.GetInstance().InvalidateCacheEntry(this);
							if (this.ShouldUseEnclaveBasedWorkflow && this.enclavePackage != null)
							{
								string enclaveDataSource = SqlCommand.GetEnclaveDataSource(this._activeConnection.DataSource, this._activeConnection.Database);
								EnclaveDelegate.Instance.InvalidateEnclaveSession(this._activeConnection.Parser.EnclaveType, enclaveDataSource, this._activeConnection.EnclaveAttestationUrl, this.enclavePackage.EnclaveSession);
							}
							return this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, method, null, TdsParserStaticMethods.GetRemainingTimeout(timeout, start), out task, out usedCache, flag, true);
						}
						catch (SqlException ex)
						{
							if (inRetry || flag || (!usedCache && !this.ShouldUseEnclaveBasedWorkflow))
							{
								throw;
							}
							bool flag2 = false;
							for (int i = 0; i < ex.Errors.Count; i++)
							{
								if ((usedCache && ex.Errors[i].Number == 33514) || (this.ShouldUseEnclaveBasedWorkflow && ex.Errors[i].Number == 33195))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								throw;
							}
							SqlQueryMetadataCache.GetInstance().InvalidateCacheEntry(this);
							if (this.ShouldUseEnclaveBasedWorkflow && this.enclavePackage != null)
							{
								string enclaveDataSource2 = SqlCommand.GetEnclaveDataSource(this._activeConnection.DataSource, this._activeConnection.Database);
								EnclaveDelegate.Instance.InvalidateEnclaveSession(this._activeConnection.Parser.EnclaveType, enclaveDataSource2, this._activeConnection.EnclaveAttestationUrl, this.enclavePackage.EnclaveSession);
							}
							return this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, method, null, TdsParserStaticMethods.GetRemainingTimeout(timeout, start), out task, out usedCache, flag, true);
						}
					}
					result = this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, flag, timeout, out task, asyncWrite && flag, inRetry, null, false);
				}
			}
			catch (OutOfMemoryException e)
			{
				this._activeConnection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._activeConnection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			return result;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000B6AFC File Offset: 0x000B5EFC
		private SqlDataReader RunExecuteReaderTdsWithTransparentParameterEncryption(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, bool async, int timeout, out Task task, bool asyncWrite, bool inRetry, SqlDataReader ds = null, bool describeParameterEncryptionRequest = false, Task describeParameterEncryptionTask = null)
		{
			if (ds == null & returnStream)
			{
				ds = new SqlDataReader(this, cmdBehavior);
			}
			if (describeParameterEncryptionTask != null)
			{
				long parameterEncryptionStart = ADP.TimerCurrent();
				TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
				Action <>9__3;
				AsyncHelper.ContinueTask(describeParameterEncryptionTask, completion, delegate
				{
					Task task2 = null;
					this.GenerateEnclavePackage();
					this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, parameterEncryptionStart), out task2, asyncWrite, inRetry, ds, false);
					TaskCompletionSource<object> completion;
					if (task2 == null)
					{
						completion.SetResult(null);
						return;
					}
					Task task3 = task2;
					completion = completion;
					Action onSuccess;
					if ((onSuccess = <>9__3) == null)
					{
						onSuccess = (<>9__3 = delegate()
						{
							completion.SetResult(null);
						});
					}
					AsyncHelper.ContinueTask(task3, completion, onSuccess, null, null, null, null, null);
				}, null, delegate(Exception exception)
				{
					if (this._cachedAsyncState != null)
					{
						this._cachedAsyncState.ResetAsyncState();
					}
					if (exception != null)
					{
						throw exception;
					}
				}, delegate
				{
					if (this._cachedAsyncState != null)
					{
						this._cachedAsyncState.ResetAsyncState();
					}
				}, null, this._activeConnection);
				task = completion.Task;
				return ds;
			}
			this.GenerateEnclavePackage();
			return this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, async, timeout, out task, asyncWrite, inRetry, ds, false);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000B6C40 File Offset: 0x000B6040
		private void GenerateEnclavePackage()
		{
			if (this.keysToBeSentToEnclave == null || this.keysToBeSentToEnclave.Count <= 0)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(this._activeConnection.EnclaveAttestationUrl))
			{
				throw SQL.NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage(this._activeConnection.Parser.EnclaveType);
			}
			string enclaveType = this._activeConnection.Parser.EnclaveType;
			if (string.IsNullOrWhiteSpace(enclaveType))
			{
				throw SQL.EnclaveTypeNullForEnclaveBasedQuery();
			}
			try
			{
				string enclaveDataSource = SqlCommand.GetEnclaveDataSource(this._activeConnection.DataSource, this._activeConnection.Database);
				this.enclavePackage = EnclaveDelegate.Instance.GenerateEnclavePackage(this.keysToBeSentToEnclave, this.CommandText, enclaveType, enclaveDataSource, this._activeConnection.EnclaveAttestationUrl);
			}
			catch (EnclaveDelegate.RetriableEnclaveQueryExecutionException)
			{
				throw;
			}
			catch (Exception innerExeption)
			{
				throw SQL.ExceptionWhenGeneratingEnclavePackage(innerExeption);
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000B6D38 File Offset: 0x000B6138
		private static string GetEnclaveDataSource(string serverName, string databaseName)
		{
			return serverName + "+" + databaseName;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000B6D54 File Offset: 0x000B6154
		private SqlDataReader RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, bool async, int timeout, out Task task, bool asyncWrite, bool inRetry, SqlDataReader ds = null, bool describeParameterEncryptionRequest = false)
		{
			if (ds == null & returnStream)
			{
				ds = new SqlDataReader(this, cmdBehavior);
			}
			Task task2 = this._activeConnection.ValidateAndReconnect(null, timeout);
			if (task2 != null)
			{
				long reconnectionStart = ADP.TimerCurrent();
				if (async)
				{
					TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
					this._activeConnection.RegisterWaitingForReconnect(completion.Task);
					this._reconnectionCompletionSource = completion;
					CancellationTokenSource timeoutCTS = new CancellationTokenSource();
					AsyncHelper.SetTimeoutException(completion, timeout, new Func<Exception>(SQL.CR_ReconnectTimeout), timeoutCTS.Token);
					Action <>9__2;
					AsyncHelper.ContinueTask(task2, completion, delegate
					{
						TaskCompletionSource<object> completion;
						if (completion.Task.IsCompleted)
						{
							return;
						}
						Interlocked.CompareExchange<TaskCompletionSource<object>>(ref this._reconnectionCompletionSource, null, completion);
						timeoutCTS.Cancel();
						Task task5;
						this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart), out task5, asyncWrite, inRetry, ds, false);
						if (task5 == null)
						{
							completion.SetResult(null);
							return;
						}
						Task task6 = task5;
						completion = completion;
						Action onSuccess;
						if ((onSuccess = <>9__2) == null)
						{
							onSuccess = (<>9__2 = delegate()
							{
								completion.SetResult(null);
							});
						}
						AsyncHelper.ContinueTask(task6, completion, onSuccess, null, null, null, null, null);
					}, null, null, null, null, this._activeConnection);
					task = completion.Task;
					return ds;
				}
				AsyncHelper.WaitForCompletion(task2, timeout, delegate
				{
					throw SQL.CR_ReconnectTimeout();
				}, true);
				timeout = TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart);
			}
			bool inSchema = (cmdBehavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
			_SqlRPC sqlRPC = null;
			task = null;
			string optionSettings = null;
			bool flag = true;
			bool flag2 = false;
			if (async && !inRetry)
			{
				this._activeConnection.GetOpenTdsConnection().IncrementAsyncCount();
				flag2 = true;
			}
			try
			{
				if (asyncWrite)
				{
					this._activeConnection.AddWeakReference(this, 2);
				}
				this.GetStateObject(null);
				Task task3;
				if (describeParameterEncryptionRequest)
				{
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._sqlRPCParameterEncryptionReqArray, timeout, inSchema, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				else if (this.BatchRPCMode)
				{
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._SqlRPCBatchArray, timeout, inSchema, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				else if (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
				{
					if (returnStream)
					{
						Bid.Trace("<sc.SqlCommand.ExecuteReader|INFO> %d#, Command executed as SQLBATCH.\n", this.ObjectID);
					}
					string text = this.GetCommandText(cmdBehavior) + this.GetResetOptionsString(cmdBehavior);
					if (this.requiresEnclaveComputations)
					{
						if (this.enclavePackage == null)
						{
							throw SQL.NullEnclavePackageForEnclaveBasedQuery(this._activeConnection.Parser.EnclaveType, this._activeConnection.EnclaveAttestationUrl);
						}
						task3 = this._stateObj.Parser.TdsExecuteSQLBatch(text, timeout, this.Notification, this._stateObj, !asyncWrite, false, this.enclavePackage.EnclavePackageBytes);
					}
					else
					{
						task3 = this._stateObj.Parser.TdsExecuteSQLBatch(text, timeout, this.Notification, this._stateObj, !asyncWrite, false, null);
					}
				}
				else if (CommandType.Text == this.CommandType)
				{
					if (this.IsDirty)
					{
						if (this._execType == SqlCommand.EXECTYPE.PREPARED)
						{
							this._hiddenPrepare = true;
						}
						this.Unprepare();
						this.IsDirty = false;
					}
					if (this._execType == SqlCommand.EXECTYPE.PREPARED)
					{
						sqlRPC = this.BuildExecute(inSchema);
					}
					else if (this._execType == SqlCommand.EXECTYPE.PREPAREPENDING)
					{
						sqlRPC = this.BuildPrepExec(cmdBehavior);
						this._execType = SqlCommand.EXECTYPE.PREPARED;
						this._preparedConnectionCloseCount = this._activeConnection.CloseCount;
						this._preparedConnectionReconnectCount = this._activeConnection.ReconnectCount;
						this._inPrepare = true;
					}
					else
					{
						this.BuildExecuteSql(cmdBehavior, null, this._parameters, ref sqlRPC);
					}
					if (this._activeConnection.IsShiloh)
					{
						sqlRPC.options = 2;
					}
					if (returnStream)
					{
						Bid.Trace("<sc.SqlCommand.ExecuteReader|INFO> %d#, Command executed as RPC.\n", this.ObjectID);
					}
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._rpcArrayOf1, timeout, inSchema, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				else
				{
					this.BuildRPC(inSchema, this._parameters, ref sqlRPC);
					optionSettings = this.GetSetOptionsString(cmdBehavior);
					if (returnStream)
					{
						Bid.Trace("<sc.SqlCommand.ExecuteReader|INFO> %d#, Command executed as RPC.\n", this.ObjectID);
					}
					if (optionSettings != null)
					{
						Task task4 = this._stateObj.Parser.TdsExecuteSQLBatch(optionSettings, timeout, this.Notification, this._stateObj, true, false, null);
						bool flag3;
						if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag3))
						{
							throw SQL.SynchronousCallMayNotPend();
						}
						optionSettings = this.GetResetOptionsString(cmdBehavior);
					}
					this._activeConnection.CheckSQLDebug();
					task3 = this._stateObj.Parser.TdsExecuteRPC(this, this._rpcArrayOf1, timeout, inSchema, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				if (async)
				{
					flag2 = false;
					if (task3 != null)
					{
						task = AsyncHelper.CreateContinuationTask(task3, delegate()
						{
							this._activeConnection.GetOpenTdsConnection();
							this.cachedAsyncState.SetAsyncReaderState(ds, runBehavior, optionSettings);
						}, null, delegate(Exception exc)
						{
							this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
						});
					}
					else
					{
						this.cachedAsyncState.SetAsyncReaderState(ds, runBehavior, optionSettings);
					}
				}
				else
				{
					this.FinishExecuteReader(ds, runBehavior, optionSettings, false, false, !describeParameterEncryptionRequest);
				}
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				if (flag2)
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
					if (sqlInternalConnectionTds != null)
					{
						sqlInternalConnectionTds.DecrementAsyncCount();
					}
				}
				throw;
			}
			finally
			{
				if (flag && !async)
				{
					this.PutStateObject();
				}
			}
			return ds;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000B73F4 File Offset: 0x000B67F4
		private SqlDataReader RunExecuteReaderSmi(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream)
		{
			SqlInternalConnectionSmi internalSmiConnection = this.InternalSmiConnection;
			SmiEventStream smiEventStream = null;
			SqlDataReader sqlDataReader = null;
			SmiRequestExecutor smiRequestExecutor = null;
			try
			{
				smiRequestExecutor = this.SetUpSmiRequest(internalSmiConnection);
				long num;
				Transaction associatedTransaction;
				internalSmiConnection.GetCurrentTransactionPair(out num, out associatedTransaction);
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlCommand.RunExecuteReaderSmi|ADV> %d#, innerConnection=%d#, transactionId=0x%I64x, commandBehavior=%d.\n", this.ObjectID, internalSmiConnection.ObjectID, num, (int)cmdBehavior);
				}
				if (SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL)
				{
					smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, associatedTransaction, cmdBehavior, SmiExecuteType.Reader);
				}
				else
				{
					smiEventStream = smiRequestExecutor.Execute(internalSmiConnection.SmiConnection, num, cmdBehavior, SmiExecuteType.Reader);
				}
				if ((runBehavior & RunBehavior.UntilDone) != (RunBehavior)0)
				{
					while (smiEventStream.HasEvents)
					{
						smiEventStream.ProcessEvent(this.EventSink);
					}
					smiEventStream.Close(this.EventSink);
				}
				if (returnStream)
				{
					sqlDataReader = new SqlDataReaderSmi(smiEventStream, this, cmdBehavior, internalSmiConnection, this.EventSink, smiRequestExecutor);
					sqlDataReader.NextResult();
					this._activeConnection.AddWeakReference(sqlDataReader, 1);
				}
				this.EventSink.ProcessMessagesAndThrow();
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableOrSecurityExceptionType(e))
				{
					throw;
				}
				if (smiEventStream != null)
				{
					smiEventStream.Close(this.EventSink);
				}
				if (smiRequestExecutor != null)
				{
					smiRequestExecutor.Close(this.EventSink);
					this.EventSink.ProcessMessagesAndThrow(true);
				}
				throw;
			}
			return sqlDataReader;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000B752C File Offset: 0x000B692C
		private SqlDataReader CompleteAsyncExecuteReader(bool isInternal = false, bool forDescribeParameterEncryption = false)
		{
			SqlDataReader cachedAsyncReader = this.cachedAsyncState.CachedAsyncReader;
			bool flag = true;
			try
			{
				this.FinishExecuteReader(cachedAsyncReader, this.cachedAsyncState.CachedRunBehavior, this.cachedAsyncState.CachedSetOptions, isInternal, forDescribeParameterEncryption, !forDescribeParameterEncryption);
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				if (flag)
				{
					if (!isInternal)
					{
						this.cachedAsyncState.ResetAsyncState();
					}
					this.PutStateObject();
				}
			}
			return cachedAsyncReader;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000B75C8 File Offset: 0x000B69C8
		private void FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, string resetOptionsString, bool isInternal, bool forDescribeParameterEncryption, bool shouldCacheForAlwaysEncrypted = true)
		{
			if (!isInternal && !forDescribeParameterEncryption)
			{
				this.NotifyDependency();
				if (this._internalEndExecuteInitiated)
				{
					return;
				}
			}
			if (runBehavior == RunBehavior.UntilDone)
			{
				try
				{
					bool flag;
					if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, ds, null, this._stateObj, out flag))
					{
						throw SQL.SynchronousCallMayNotPend();
					}
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						if (this._inPrepare)
						{
							this._inPrepare = false;
							this.IsDirty = true;
							this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
						}
						if (ds != null)
						{
							ds.Close();
						}
					}
					throw;
				}
			}
			if (ds != null)
			{
				ds.Bind(this._stateObj);
				this._stateObj = null;
				ds.ResetOptionsString = resetOptionsString;
				this._activeConnection.AddWeakReference(ds, 1);
				try
				{
					if (shouldCacheForAlwaysEncrypted)
					{
						this._cachedMetaData = ds.MetaData;
					}
					else
					{
						_SqlMetaDataSet metaData = ds.MetaData;
					}
					ds.IsInitialized = true;
				}
				catch (Exception e2)
				{
					if (ADP.IsCatchableExceptionType(e2))
					{
						if (this._inPrepare)
						{
							this._inPrepare = false;
							this.IsDirty = true;
							this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
						}
						ds.Close();
					}
					throw;
				}
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000B7700 File Offset: 0x000B6B00
		private void NotifyDependency()
		{
			if (this._sqlDep != null)
			{
				this._sqlDep.StartTimer(this.Notification);
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000B7728 File Offset: 0x000B6B28
		public SqlCommand Clone()
		{
			SqlCommand sqlCommand = new SqlCommand(this);
			Bid.Trace("<sc.SqlCommand.Clone|API> %d#, clone=%d#\n", this.ObjectID, sqlCommand.ObjectID);
			return sqlCommand;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000B7754 File Offset: 0x000B6B54
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000B7768 File Offset: 0x000B6B68
		private void RegisterForConnectionCloseNotification<T>(ref Task<T> outterTask)
		{
			SqlConnection activeConnection = this._activeConnection;
			if (activeConnection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			activeConnection.RegisterForConnectionCloseNotification<T>(ref outterTask, this, 2);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000B7790 File Offset: 0x000B6B90
		private void ValidateCommand(string method, bool async)
		{
			if (this._activeConnection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
			if (((this.ColumnEncryptionSetting == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._activeConnection.IsColumnEncryptionSettingEnabled) || this.ColumnEncryptionSetting == SqlCommandColumnEncryptionSetting.Enabled || this.ColumnEncryptionSetting == SqlCommandColumnEncryptionSetting.ResultSetOnly) && sqlInternalConnectionTds != null && sqlInternalConnectionTds.Parser != null && !sqlInternalConnectionTds.Parser.IsColumnEncryptionSupported)
			{
				throw SQL.TceNotSupported();
			}
			if (sqlInternalConnectionTds != null)
			{
				TdsParser parser = sqlInternalConnectionTds.Parser;
				if (parser == null || parser.State == TdsParserState.Closed)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Closed);
				}
				if (parser.State != TdsParserState.OpenLoggedIn)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Broken);
				}
			}
			else
			{
				if (this._activeConnection.State == ConnectionState.Closed)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Closed);
				}
				if (this._activeConnection.State == ConnectionState.Broken)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Broken);
				}
			}
			this.ValidateAsyncCommand();
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this._activeConnection.ValidateConnectionForExecute(method, this);
			}
			catch (OutOfMemoryException e)
			{
				this._activeConnection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._activeConnection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			if (this._transaction != null && this._transaction.Connection == null)
			{
				this._transaction = null;
			}
			if (this._activeConnection.HasLocalTransactionFromAPI && this._transaction == null)
			{
				throw ADP.TransactionRequired(method);
			}
			if (this._transaction != null && this._activeConnection != this._transaction.Connection)
			{
				throw ADP.TransactionConnectionMismatch();
			}
			if (ADP.IsEmpty(this.CommandText))
			{
				throw ADP.CommandTextRequired(method);
			}
			if (this.Notification != null && !this._activeConnection.IsYukonOrNewer)
			{
				throw SQL.NotificationsRequireYukon();
			}
			if (async && this._activeConnection.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000B79B0 File Offset: 0x000B6DB0
		private void ValidateAsyncCommand()
		{
			if (this.cachedAsyncState.PendingAsyncOperation)
			{
				if (this.cachedAsyncState.IsActiveConnectionValid(this._activeConnection))
				{
					throw SQL.PendingBeginXXXExists();
				}
				this._stateObj = null;
				this.cachedAsyncState.ResetAsyncState();
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000B79F8 File Offset: 0x000B6DF8
		private void GetStateObject(TdsParser parser = null)
		{
			if (this._pendingCancel)
			{
				this._pendingCancel = false;
				throw SQL.OperationCancelled();
			}
			if (parser == null)
			{
				parser = this._activeConnection.Parser;
				if (parser == null || parser.State == TdsParserState.Broken || parser.State == TdsParserState.Closed)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			TdsParserStateObject session = parser.GetSession(this);
			session.StartSession(this.ObjectID);
			this._stateObj = session;
			if (this._pendingCancel)
			{
				this._pendingCancel = false;
				throw SQL.OperationCancelled();
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000B7A7C File Offset: 0x000B6E7C
		private void ReliablePutStateObject()
		{
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._activeConnection);
				this.PutStateObject();
			}
			catch (OutOfMemoryException e)
			{
				this._activeConnection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._activeConnection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._activeConnection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000B7B2C File Offset: 0x000B6F2C
		private void PutStateObject()
		{
			TdsParserStateObject stateObj = this._stateObj;
			this._stateObj = null;
			if (stateObj != null)
			{
				stateObj.CloseSession();
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000B7B50 File Offset: 0x000B6F50
		internal void OnDoneDescribeParameterEncryptionProc(TdsParserStateObject stateObj)
		{
			if (this.BatchRPCMode)
			{
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].cumulativeRecordsAffected = this._rowsAffected;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].recordsAffected = new int?((0 < this._currentlyExecutingDescribeParameterEncryptionRPC && 0 <= this._rowsAffected) ? (this._rowsAffected - Math.Max(this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC - 1].cumulativeRecordsAffected, 0)) : this._rowsAffected);
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].errorsIndexStart = ((0 < this._currentlyExecutingDescribeParameterEncryptionRPC) ? this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC - 1].errorsIndexEnd : 0);
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].errorsIndexEnd = stateObj.ErrorCount;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].errors = stateObj._errors;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].warningsIndexStart = ((0 < this._currentlyExecutingDescribeParameterEncryptionRPC) ? this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC - 1].warningsIndexEnd : 0);
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].warningsIndexEnd = stateObj.WarningCount;
				this._sqlRPCParameterEncryptionReqArray[this._currentlyExecutingDescribeParameterEncryptionRPC].warnings = stateObj._warnings;
				this._currentlyExecutingDescribeParameterEncryptionRPC++;
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000B7CA4 File Offset: 0x000B70A4
		internal void OnDoneProc()
		{
			if (this.BatchRPCMode)
			{
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].cumulativeRecordsAffected = this._rowsAffected;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].recordsAffected = new int?((0 < this._currentlyExecutingBatch && 0 <= this._rowsAffected) ? (this._rowsAffected - Math.Max(this._SqlRPCBatchArray[this._currentlyExecutingBatch - 1].cumulativeRecordsAffected, 0)) : this._rowsAffected);
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].errorsIndexStart = ((0 < this._currentlyExecutingBatch) ? this._SqlRPCBatchArray[this._currentlyExecutingBatch - 1].errorsIndexEnd : 0);
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].errorsIndexEnd = this._stateObj.ErrorCount;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].errors = this._stateObj._errors;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].warningsIndexStart = ((0 < this._currentlyExecutingBatch) ? this._SqlRPCBatchArray[this._currentlyExecutingBatch - 1].warningsIndexEnd : 0);
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].warningsIndexEnd = this._stateObj.WarningCount;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].warnings = this._stateObj._warnings;
				this._currentlyExecutingBatch++;
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000B7E0C File Offset: 0x000B720C
		internal void OnReturnStatus(int status)
		{
			if (this._inPrepare)
			{
				return;
			}
			if (this.IsDescribeParameterEncryptionRPCCurrentlyInProgress)
			{
				return;
			}
			SqlParameterCollection sqlParameterCollection = this._parameters;
			if (this.BatchRPCMode)
			{
				if (this._parameterCollectionList.Count > this._currentlyExecutingBatch)
				{
					sqlParameterCollection = this._parameterCollectionList[this._currentlyExecutingBatch];
				}
				else
				{
					sqlParameterCollection = null;
				}
			}
			int parameterCount = this.GetParameterCount(sqlParameterCollection);
			int i = 0;
			while (i < parameterCount)
			{
				SqlParameter sqlParameter = sqlParameterCollection[i];
				if (sqlParameter.Direction == ParameterDirection.ReturnValue)
				{
					object value = sqlParameter.Value;
					if (value != null && value.GetType() == typeof(SqlInt32))
					{
						sqlParameter.Value = new SqlInt32(status);
					}
					else
					{
						sqlParameter.Value = status;
					}
					if (!this.BatchRPCMode && this.CachingQueryMetadataPostponed && !this.requiresEnclaveComputations && this._parameters != null && this._parameters.Count > 0)
					{
						SqlQueryMetadataCache.GetInstance().AddQueryMetadata(this, false);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000B7F10 File Offset: 0x000B7310
		internal void OnReturnValue(SqlReturnValue rec, TdsParserStateObject stateObj)
		{
			if (this._inPrepare)
			{
				if (!rec.value.IsNull)
				{
					this._prepareHandle = rec.value.Int32;
				}
				this._inPrepare = false;
				return;
			}
			SqlParameterCollection currentParameterCollection = this.GetCurrentParameterCollection();
			int parameterCount = this.GetParameterCount(currentParameterCollection);
			SqlParameter parameterForOutputValueExtraction = this.GetParameterForOutputValueExtraction(currentParameterCollection, rec.parameter, parameterCount);
			if (parameterForOutputValueExtraction != null)
			{
				if (rec.cipherMD != null && parameterForOutputValueExtraction.CipherMetadata != null && (parameterForOutputValueExtraction.Direction == ParameterDirection.Output || parameterForOutputValueExtraction.Direction == ParameterDirection.InputOutput || parameterForOutputValueExtraction.Direction == ParameterDirection.ReturnValue))
				{
					if (rec.tdsType != 165)
					{
						throw SQL.InvalidDataTypeForEncryptedParameter(parameterForOutputValueExtraction.ParameterNameFixed, (int)rec.tdsType, 165);
					}
					TdsParser parser = this._activeConnection.Parser;
					if (parser == null || parser.State == TdsParserState.Closed || parser.State == TdsParserState.Broken)
					{
						throw ADP.ClosedConnectionError();
					}
					if (!rec.value.IsNull)
					{
						try
						{
							rec.cipherMD.EncryptionInfo = parameterForOutputValueExtraction.CipherMetadata.EncryptionInfo;
							byte[] array = SqlSecurityUtility.DecryptWithKey(rec.value.ByteArray, rec.cipherMD, this._activeConnection.DataSource);
							if (array != null)
							{
								SqlBuffer sqlBuffer = new SqlBuffer();
								parser.DeserializeUnencryptedValue(sqlBuffer, array, rec, stateObj, rec.NormalizationRuleVersion);
								parameterForOutputValueExtraction.SetSqlBuffer(sqlBuffer);
							}
							return;
						}
						catch (Exception e)
						{
							throw SQL.ParamDecryptionFailed(parameterForOutputValueExtraction.ParameterNameFixed, null, e);
						}
					}
					SqlBuffer sqlBuffer2 = new SqlBuffer();
					TdsParser.GetNullSqlValue(sqlBuffer2, rec, SqlCommandColumnEncryptionSetting.Enabled, parser.Connection);
					parameterForOutputValueExtraction.SetSqlBuffer(sqlBuffer2);
					return;
				}
				else
				{
					object value = parameterForOutputValueExtraction.Value;
					if (SqlDbType.Udt == parameterForOutputValueExtraction.SqlDbType)
					{
						try
						{
							this.Connection.CheckGetExtendedUDTInfo(rec, true);
							object value2;
							if (rec.value.IsNull)
							{
								value2 = DBNull.Value;
							}
							else
							{
								value2 = rec.value.ByteArray;
							}
							parameterForOutputValueExtraction.Value = this.Connection.GetUdtValue(value2, rec, false);
						}
						catch (FileNotFoundException udtLoadError)
						{
							parameterForOutputValueExtraction.SetUdtLoadError(udtLoadError);
						}
						catch (FileLoadException udtLoadError2)
						{
							parameterForOutputValueExtraction.SetUdtLoadError(udtLoadError2);
						}
						return;
					}
					parameterForOutputValueExtraction.SetSqlBuffer(rec.value);
					MetaType metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(rec.type, rec.isMultiValued);
					if (rec.type == SqlDbType.Decimal)
					{
						parameterForOutputValueExtraction.ScaleInternal = rec.scale;
						parameterForOutputValueExtraction.PrecisionInternal = rec.precision;
					}
					else if (metaTypeFromSqlDbType.IsVarTime)
					{
						parameterForOutputValueExtraction.ScaleInternal = rec.scale;
					}
					else if (rec.type == SqlDbType.Xml)
					{
						SqlCachedBuffer sqlCachedBuffer = parameterForOutputValueExtraction.Value as SqlCachedBuffer;
						if (sqlCachedBuffer != null)
						{
							parameterForOutputValueExtraction.Value = sqlCachedBuffer.ToString();
						}
					}
					if (rec.collation != null)
					{
						parameterForOutputValueExtraction.Collation = rec.collation;
					}
				}
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000B81E8 File Offset: 0x000B75E8
		internal void OnParametersAvailableSmi(SmiParameterMetaData[] paramMetaData, ITypedGettersV3 parameterValues)
		{
			for (int i = 0; i < paramMetaData.Length; i++)
			{
				this.OnParameterAvailableSmi(paramMetaData[i], parameterValues, i);
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000B8210 File Offset: 0x000B7610
		internal void OnParameterAvailableSmi(SmiParameterMetaData metaData, ITypedGettersV3 parameterValues, int ordinal)
		{
			if (ParameterDirection.Input != metaData.Direction)
			{
				string paramName = null;
				if (ParameterDirection.ReturnValue != metaData.Direction)
				{
					paramName = metaData.Name;
				}
				SqlParameterCollection currentParameterCollection = this.GetCurrentParameterCollection();
				int parameterCount = this.GetParameterCount(currentParameterCollection);
				SqlParameter parameterForOutputValueExtraction = this.GetParameterForOutputValueExtraction(currentParameterCollection, paramName, parameterCount);
				if (parameterForOutputValueExtraction != null)
				{
					parameterForOutputValueExtraction.LocaleId = (int)metaData.LocaleId;
					parameterForOutputValueExtraction.CompareInfo = metaData.CompareOptions;
					SqlBuffer sqlBuffer = new SqlBuffer();
					object obj;
					if (this._activeConnection.IsKatmaiOrNewer)
					{
						obj = ValueUtilsSmi.GetOutputParameterV200Smi(this.OutParamEventSink, (SmiTypedGetterSetter)parameterValues, ordinal, metaData, this._smiRequestContext, sqlBuffer);
					}
					else
					{
						obj = ValueUtilsSmi.GetOutputParameterV3Smi(this.OutParamEventSink, parameterValues, ordinal, metaData, this._smiRequestContext, sqlBuffer);
					}
					if (obj != null)
					{
						parameterForOutputValueExtraction.Value = obj;
						return;
					}
					parameterForOutputValueExtraction.SetSqlBuffer(sqlBuffer);
				}
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000B82D0 File Offset: 0x000B76D0
		private SqlParameterCollection GetCurrentParameterCollection()
		{
			if (!this.BatchRPCMode)
			{
				return this._parameters;
			}
			if (this._parameterCollectionList.Count > this._currentlyExecutingBatch)
			{
				return this._parameterCollectionList[this._currentlyExecutingBatch];
			}
			return null;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000B8314 File Offset: 0x000B7714
		private SqlParameter GetParameterForOutputValueExtraction(SqlParameterCollection parameters, string paramName, int paramCount)
		{
			SqlParameter sqlParameter = null;
			bool flag = false;
			if (paramName == null)
			{
				for (int i = 0; i < paramCount; i++)
				{
					sqlParameter = parameters[i];
					if (sqlParameter.Direction == ParameterDirection.ReturnValue)
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < paramCount; j++)
				{
					sqlParameter = parameters[j];
					if (sqlParameter.Direction != ParameterDirection.Input && sqlParameter.Direction != ParameterDirection.ReturnValue && paramName == sqlParameter.ParameterNameFixed)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				return sqlParameter;
			}
			return null;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000B838C File Offset: 0x000B778C
		private void GetRPCObject(int paramCount, ref _SqlRPC rpc, bool forSpDescribeParameterEncryption = false)
		{
			if (rpc == null)
			{
				if (!forSpDescribeParameterEncryption)
				{
					if (this._rpcArrayOf1 == null)
					{
						this._rpcArrayOf1 = new _SqlRPC[1];
						this._rpcArrayOf1[0] = new _SqlRPC();
					}
					rpc = this._rpcArrayOf1[0];
				}
				else
				{
					if (this._rpcForEncryption == null)
					{
						this._rpcForEncryption = new _SqlRPC();
					}
					rpc = this._rpcForEncryption;
				}
			}
			rpc.ProcID = 0;
			rpc.rpcName = null;
			rpc.options = 0;
			rpc.recordsAffected = null;
			rpc.cumulativeRecordsAffected = -1;
			rpc.errorsIndexStart = 0;
			rpc.errorsIndexEnd = 0;
			rpc.errors = null;
			rpc.warningsIndexStart = 0;
			rpc.warningsIndexEnd = 0;
			rpc.warnings = null;
			rpc.needsFetchParameterEncryptionMetadata = false;
			if (rpc.parameters == null || rpc.parameters.Length < paramCount)
			{
				rpc.parameters = new SqlParameter[paramCount];
			}
			else if (rpc.parameters.Length > paramCount)
			{
				rpc.parameters[paramCount] = null;
			}
			if (rpc.paramoptions == null || rpc.paramoptions.Length < paramCount)
			{
				rpc.paramoptions = new byte[paramCount];
				return;
			}
			for (int i = 0; i < paramCount; i++)
			{
				rpc.paramoptions[i] = 0;
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000B84C0 File Offset: 0x000B78C0
		private void SetUpRPCParameters(_SqlRPC rpc, int startCount, bool inSchema, SqlParameterCollection parameters)
		{
			int parameterCount = this.GetParameterCount(parameters);
			int num = startCount;
			TdsParser parser = this._activeConnection.Parser;
			bool isYukonOrNewer = parser.IsYukonOrNewer;
			for (int i = 0; i < parameterCount; i++)
			{
				SqlParameter sqlParameter = parameters[i];
				sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
				if (!sqlParameter.ValidateTypeLengths(isYukonOrNewer).IsPlp && sqlParameter.Direction != ParameterDirection.Output)
				{
					sqlParameter.FixStreamDataForNonPLP();
				}
				if (SqlCommand.ShouldSendParameter(sqlParameter, false))
				{
					rpc.parameters[num] = sqlParameter;
					if (sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Output)
					{
						rpc.paramoptions[num] = 1;
					}
					if (sqlParameter.CipherMetadata != null)
					{
						byte[] paramoptions = rpc.paramoptions;
						int num2 = num;
						paramoptions[num2] |= 8;
					}
					if (sqlParameter.Direction != ParameterDirection.Output && sqlParameter.Value == null && (!inSchema || SqlDbType.Structured == sqlParameter.SqlDbType))
					{
						byte[] paramoptions2 = rpc.paramoptions;
						int num3 = num;
						paramoptions2[num3] |= 2;
					}
					num++;
				}
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000B85B4 File Offset: 0x000B79B4
		private _SqlRPC BuildPrepExec(CommandBehavior behavior)
		{
			int num = 3;
			int num2 = this.CountSendableParameters(this._parameters);
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(num2 + num, ref sqlRPC, false);
			sqlRPC.ProcID = 13;
			sqlRPC.rpcName = "sp_prepexec";
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.InputOutput;
			sqlParameter.Value = this._prepareHandle;
			sqlRPC.parameters[0] = sqlParameter;
			sqlRPC.paramoptions[0] = 1;
			string text = this.BuildParamList(this._stateObj.Parser, this._parameters, false);
			sqlParameter = new SqlParameter(null, (text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, text.Length);
			sqlParameter.Value = text;
			sqlRPC.parameters[1] = sqlParameter;
			string commandText = this.GetCommandText(behavior);
			sqlParameter = new SqlParameter(null, (commandText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, commandText.Length);
			sqlParameter.Value = commandText;
			sqlRPC.parameters[2] = sqlParameter;
			this.SetUpRPCParameters(sqlRPC, num, false, this._parameters);
			return sqlRPC;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000B86C0 File Offset: 0x000B7AC0
		private static bool ShouldSendParameter(SqlParameter p, bool includeReturnValue = false)
		{
			ParameterDirection direction = p.Direction;
			return direction - ParameterDirection.Input <= 2 || (direction == ParameterDirection.ReturnValue && includeReturnValue);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x000B86E4 File Offset: 0x000B7AE4
		private int CountSendableParameters(SqlParameterCollection parameters)
		{
			int num = 0;
			if (parameters != null)
			{
				int count = parameters.Count;
				for (int i = 0; i < count; i++)
				{
					if (SqlCommand.ShouldSendParameter(parameters[i], false))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x000B8720 File Offset: 0x000B7B20
		private int GetParameterCount(SqlParameterCollection parameters)
		{
			if (parameters == null)
			{
				return 0;
			}
			return parameters.Count;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x000B8738 File Offset: 0x000B7B38
		private void BuildRPC(bool inSchema, SqlParameterCollection parameters, ref _SqlRPC rpc)
		{
			int paramCount = this.CountSendableParameters(parameters);
			this.GetRPCObject(paramCount, ref rpc, false);
			rpc.rpcName = this.CommandText;
			this.SetUpRPCParameters(rpc, 0, inSchema, parameters);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000B8770 File Offset: 0x000B7B70
		private _SqlRPC BuildUnprepare()
		{
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(1, ref sqlRPC, false);
			sqlRPC.ProcID = 15;
			sqlRPC.rpcName = "sp_unprepare";
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.Int);
			sqlParameter.Value = this._prepareHandle;
			sqlRPC.parameters[0] = sqlParameter;
			return sqlRPC;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000B87C0 File Offset: 0x000B7BC0
		private _SqlRPC BuildExecute(bool inSchema)
		{
			int num = 1;
			int num2 = this.CountSendableParameters(this._parameters);
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(num2 + num, ref sqlRPC, false);
			sqlRPC.ProcID = 12;
			sqlRPC.rpcName = "sp_execute";
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.Int);
			sqlParameter.Value = this._prepareHandle;
			sqlRPC.parameters[0] = sqlParameter;
			this.SetUpRPCParameters(sqlRPC, num, inSchema, this._parameters);
			return sqlRPC;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000B8830 File Offset: 0x000B7C30
		private void BuildExecuteSql(CommandBehavior behavior, string commandText, SqlParameterCollection parameters, ref _SqlRPC rpc)
		{
			int num = this.CountSendableParameters(parameters);
			int num2;
			if (num > 0)
			{
				num2 = 2;
			}
			else
			{
				num2 = 1;
			}
			this.GetRPCObject(num + num2, ref rpc, false);
			rpc.ProcID = 10;
			rpc.rpcName = "sp_executesql";
			if (commandText == null)
			{
				commandText = this.GetCommandText(behavior);
			}
			SqlParameter sqlParameter = new SqlParameter(null, (commandText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, commandText.Length);
			sqlParameter.Value = commandText;
			rpc.parameters[0] = sqlParameter;
			if (num > 0)
			{
				string text = this.BuildParamList(this._stateObj.Parser, this.BatchRPCMode ? parameters : this._parameters, false);
				sqlParameter = new SqlParameter(null, (text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, text.Length);
				sqlParameter.Value = text;
				rpc.parameters[1] = sqlParameter;
				bool inSchema = (behavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
				this.SetUpRPCParameters(rpc, num2, inSchema, parameters);
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000B8924 File Offset: 0x000B7D24
		private SqlParameter BuildStoredProcedureStatementForColumnEncryption(string storedProcedureName, SqlParameter[] parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EXEC ");
			SqlParameter sqlParameter = null;
			foreach (SqlParameter sqlParameter2 in parameters)
			{
				if (sqlParameter2.Direction == ParameterDirection.ReturnValue)
				{
					sqlParameter = sqlParameter2;
					break;
				}
			}
			if (sqlParameter != null)
			{
				stringBuilder.AppendFormat("{0}=", sqlParameter.ParameterNameFixed);
			}
			stringBuilder.Append(this.ParseAndQuoteIdentifier(storedProcedureName, false));
			int j = 0;
			if (parameters.Count<SqlParameter>() > 0)
			{
				while (j < parameters.Count<SqlParameter>() && parameters[j].Direction == ParameterDirection.ReturnValue)
				{
					j++;
				}
				if (j < parameters.Count<SqlParameter>())
				{
					stringBuilder.AppendFormat(" {0}={0}", parameters[j].ParameterNameFixed);
					if (parameters[j].Direction == ParameterDirection.Output || parameters[j].Direction == ParameterDirection.InputOutput)
					{
						stringBuilder.AppendFormat(" OUTPUT", new object[0]);
					}
				}
			}
			for (j++; j < parameters.Count<SqlParameter>(); j++)
			{
				if (parameters[j].Direction != ParameterDirection.ReturnValue)
				{
					stringBuilder.AppendFormat(", {0}={0}", parameters[j].ParameterNameFixed);
					if (parameters[j].Direction == ParameterDirection.Output || parameters[j].Direction == ParameterDirection.InputOutput)
					{
						stringBuilder.AppendFormat(" OUTPUT", new object[0]);
					}
				}
			}
			return new SqlParameter(null, (stringBuilder.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, stringBuilder.Length)
			{
				Value = stringBuilder.ToString()
			};
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000B8A88 File Offset: 0x000B7E88
		internal string BuildParamList(TdsParser parser, SqlParameterCollection parameters, bool includeReturnValue = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool isYukonOrNewer = parser.IsYukonOrNewer;
			int count = parameters.Count;
			for (int i = 0; i < count; i++)
			{
				SqlParameter sqlParameter = parameters[i];
				sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
				if (SqlCommand.ShouldSendParameter(sqlParameter, includeReturnValue))
				{
					if (flag)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(sqlParameter.ParameterNameFixed);
					MetaType metaType = sqlParameter.InternalMetaType;
					stringBuilder.Append(" ");
					if (metaType.SqlDbType == SqlDbType.Udt)
					{
						string udtTypeName = sqlParameter.UdtTypeName;
						if (ADP.IsEmpty(udtTypeName))
						{
							throw SQL.MustSetUdtTypeNameForUdtParams();
						}
						stringBuilder.Append(this.ParseAndQuoteIdentifier(udtTypeName, true));
					}
					else if (metaType.SqlDbType == SqlDbType.Structured)
					{
						string typeName = sqlParameter.TypeName;
						if (ADP.IsEmpty(typeName))
						{
							throw SQL.MustSetTypeNameForParam(metaType.TypeName, sqlParameter.ParameterNameFixed);
						}
						stringBuilder.Append(this.ParseAndQuoteIdentifier(typeName, false));
						stringBuilder.Append(" READONLY");
					}
					else
					{
						metaType = sqlParameter.ValidateTypeLengths(isYukonOrNewer);
						if (!metaType.IsPlp && sqlParameter.Direction != ParameterDirection.Output)
						{
							sqlParameter.FixStreamDataForNonPLP();
						}
						stringBuilder.Append(metaType.TypeName);
					}
					flag = true;
					if (metaType.SqlDbType == SqlDbType.Decimal)
					{
						byte b = sqlParameter.GetActualPrecision();
						byte actualScale = sqlParameter.GetActualScale();
						stringBuilder.Append('(');
						if (b == 0)
						{
							if (this.IsShiloh)
							{
								b = 29;
							}
							else
							{
								b = 28;
							}
						}
						stringBuilder.Append(b);
						stringBuilder.Append(',');
						stringBuilder.Append(actualScale);
						stringBuilder.Append(')');
					}
					else if (metaType.IsVarTime)
					{
						byte actualScale2 = sqlParameter.GetActualScale();
						stringBuilder.Append('(');
						stringBuilder.Append(actualScale2);
						stringBuilder.Append(')');
					}
					else if (!metaType.IsFixed && !metaType.IsLong && metaType.SqlDbType != SqlDbType.Timestamp && metaType.SqlDbType != SqlDbType.Udt && SqlDbType.Structured != metaType.SqlDbType)
					{
						int num = sqlParameter.Size;
						stringBuilder.Append('(');
						if (metaType.IsAnsiType)
						{
							object coercedValue = sqlParameter.GetCoercedValue();
							string text = null;
							if (coercedValue != null && DBNull.Value != coercedValue)
							{
								text = (coercedValue as string);
								if (text == null)
								{
									SqlString sqlString = (coercedValue is SqlString) ? ((SqlString)coercedValue) : SqlString.Null;
									if (!sqlString.IsNull)
									{
										text = sqlString.Value;
									}
								}
							}
							if (text != null)
							{
								int encodingCharLength = parser.GetEncodingCharLength(text, sqlParameter.GetActualSize(), sqlParameter.Offset, null);
								if (encodingCharLength > num)
								{
									num = encodingCharLength;
								}
							}
						}
						if (num == 0)
						{
							num = (metaType.IsSizeInCharacters ? 4000 : 8000);
						}
						stringBuilder.Append(num);
						stringBuilder.Append(')');
					}
					else if (metaType.IsPlp && metaType.SqlDbType != SqlDbType.Xml && metaType.SqlDbType != SqlDbType.Udt)
					{
						stringBuilder.Append("(max) ");
					}
					if (sqlParameter.Direction != ParameterDirection.Input)
					{
						stringBuilder.Append(" output");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000B8D94 File Offset: 0x000B8194
		private string ParseAndQuoteIdentifier(string identifier, bool isUdtTypeName)
		{
			string[] array = SqlParameter.ParseTypeName(identifier, isUdtTypeName);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append('.');
				}
				if (array[i] != null && array[i].Length != 0)
				{
					stringBuilder.Append(ADP.BuildQuotedString("[", "]", array[i]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000B8E00 File Offset: 0x000B8200
		private string GetSetOptionsString(CommandBehavior behavior)
		{
			string text = null;
			if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly) || CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
			{
				text = " SET FMTONLY OFF;";
				if (CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
				{
					text += " SET NO_BROWSETABLE ON;";
				}
				if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly))
				{
					text += " SET FMTONLY ON;";
				}
			}
			return text;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000B8E48 File Offset: 0x000B8248
		private string GetResetOptionsString(CommandBehavior behavior)
		{
			string text = null;
			if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly))
			{
				text += " SET FMTONLY OFF;";
			}
			if (CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
			{
				text += " SET NO_BROWSETABLE OFF;";
			}
			return text;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000B8E7C File Offset: 0x000B827C
		private string GetCommandText(CommandBehavior behavior)
		{
			return this.GetSetOptionsString(behavior) + this.CommandText;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000B8E9C File Offset: 0x000B829C
		private _SqlRPC BuildPrepare(CommandBehavior behavior)
		{
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(3, ref sqlRPC, false);
			sqlRPC.ProcID = 11;
			sqlRPC.rpcName = "sp_prepare";
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.Output;
			sqlRPC.parameters[0] = sqlParameter;
			sqlRPC.paramoptions[0] = 1;
			string text = this.BuildParamList(this._stateObj.Parser, this._parameters, false);
			sqlParameter = new SqlParameter(null, (text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, text.Length);
			sqlParameter.Value = text;
			sqlRPC.parameters[1] = sqlParameter;
			string commandText = this.GetCommandText(behavior);
			sqlParameter = new SqlParameter(null, (commandText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, commandText.Length);
			sqlParameter.Value = commandText;
			sqlRPC.parameters[2] = sqlParameter;
			return sqlRPC;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000B8F70 File Offset: 0x000B8370
		internal void CheckThrowSNIException()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.CheckThrowSNIException();
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000B8F90 File Offset: 0x000B8390
		internal void OnConnectionClosed()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.OnConnectionClosed();
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000B8FB0 File Offset: 0x000B83B0
		internal TdsParserStateObject StateObject
		{
			get
			{
				return this._stateObj;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x000B8FC4 File Offset: 0x000B83C4
		private bool IsPrepared
		{
			get
			{
				return this._execType > SqlCommand.EXECTYPE.UNPREPARED;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x000B8FDC File Offset: 0x000B83DC
		private bool IsUserPrepared
		{
			get
			{
				return this.IsPrepared && !this._hiddenPrepare && !this.IsDirty;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x000B9004 File Offset: 0x000B8404
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x000B9068 File Offset: 0x000B8468
		internal bool IsDirty
		{
			get
			{
				SqlConnection activeConnection = this._activeConnection;
				return this.IsPrepared && (this._dirty || (this._parameters != null && this._parameters.IsDirty) || (activeConnection != null && (activeConnection.CloseCount != this._preparedConnectionCloseCount || activeConnection.ReconnectCount != this._preparedConnectionReconnectCount)));
			}
			set
			{
				this._dirty = (value && this.IsPrepared);
				if (this._parameters != null)
				{
					this._parameters.IsDirty = this._dirty;
				}
				this._cachedMetaData = null;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x000B90A8 File Offset: 0x000B84A8
		// (set) Token: 0x060019EF RID: 6639 RVA: 0x000B90BC File Offset: 0x000B84BC
		internal int RowsAffectedByDescribeParameterEncryption
		{
			get
			{
				return this._rowsAffectedBySpDescribeParameterEncryption;
			}
			set
			{
				if (-1 == this._rowsAffectedBySpDescribeParameterEncryption)
				{
					this._rowsAffectedBySpDescribeParameterEncryption = value;
					return;
				}
				if (0 < value)
				{
					this._rowsAffectedBySpDescribeParameterEncryption += value;
				}
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x000B90EC File Offset: 0x000B84EC
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x000B9100 File Offset: 0x000B8500
		internal int InternalRecordsAffected
		{
			get
			{
				return this._rowsAffected;
			}
			set
			{
				if (-1 == this._rowsAffected)
				{
					this._rowsAffected = value;
					return;
				}
				if (0 < value)
				{
					this._rowsAffected += value;
				}
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x000B9130 File Offset: 0x000B8530
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x000B9144 File Offset: 0x000B8544
		internal bool BatchRPCMode
		{
			get
			{
				return this._batchRPCMode;
			}
			set
			{
				this._batchRPCMode = value;
				if (!this._batchRPCMode)
				{
					this.ClearBatchCommand();
					return;
				}
				if (this._RPCList == null)
				{
					this._RPCList = new List<_SqlRPC>();
				}
				if (this._parameterCollectionList == null)
				{
					this._parameterCollectionList = new List<SqlParameterCollection>();
				}
			}
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000B9190 File Offset: 0x000B8590
		private void ClearDescribeParameterEncryptionRequests()
		{
			this._sqlRPCParameterEncryptionReqArray = null;
			this._currentlyExecutingDescribeParameterEncryptionRPC = 0;
			this._isDescribeParameterEncryptionRPCCurrentlyInProgress = false;
			this._rowsAffectedBySpDescribeParameterEncryption = -1;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000B91BC File Offset: 0x000B85BC
		internal void ClearBatchCommand()
		{
			List<_SqlRPC> rpclist = this._RPCList;
			if (rpclist != null)
			{
				rpclist.Clear();
			}
			if (this._parameterCollectionList != null)
			{
				this._parameterCollectionList.Clear();
			}
			this._SqlRPCBatchArray = null;
			this._currentlyExecutingBatch = 0;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000B91FC File Offset: 0x000B85FC
		private void SetColumnEncryptionSetting(SqlCommandColumnEncryptionSetting newColumnEncryptionSetting)
		{
			if (!this._wasBatchModeColumnEncryptionSettingSetOnce)
			{
				this._columnEncryptionSetting = newColumnEncryptionSetting;
				this._wasBatchModeColumnEncryptionSettingSetOnce = true;
				return;
			}
			if (this._columnEncryptionSetting != newColumnEncryptionSetting)
			{
				throw SQL.BatchedUpdateColumnEncryptionSettingMismatch();
			}
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000B9230 File Offset: 0x000B8630
		internal void AddBatchCommand(string commandText, SqlParameterCollection parameters, CommandType cmdType, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			_SqlRPC item = new _SqlRPC();
			this.CommandText = commandText;
			this.CommandType = cmdType;
			this.SetColumnEncryptionSetting(columnEncryptionSetting);
			this.GetStateObject(null);
			if (cmdType == CommandType.StoredProcedure)
			{
				this.BuildRPC(false, parameters, ref item);
			}
			else
			{
				this.BuildExecuteSql(CommandBehavior.Default, commandText, parameters, ref item);
			}
			this._RPCList.Add(item);
			this._parameterCollectionList.Add(parameters);
			this.ReliablePutStateObject();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000B929C File Offset: 0x000B869C
		internal int ExecuteBatchRPCCommand()
		{
			this._SqlRPCBatchArray = this._RPCList.ToArray();
			this._currentlyExecutingBatch = 0;
			return this.ExecuteNonQuery();
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000B92C8 File Offset: 0x000B86C8
		internal int? GetRecordsAffected(int commandIndex)
		{
			return this._SqlRPCBatchArray[commandIndex].recordsAffected;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000B92E4 File Offset: 0x000B86E4
		internal SqlException GetErrors(int commandIndex)
		{
			SqlException result = null;
			int num = this._SqlRPCBatchArray[commandIndex].errorsIndexEnd - this._SqlRPCBatchArray[commandIndex].errorsIndexStart;
			if (0 < num)
			{
				SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
				for (int i = this._SqlRPCBatchArray[commandIndex].errorsIndexStart; i < this._SqlRPCBatchArray[commandIndex].errorsIndexEnd; i++)
				{
					sqlErrorCollection.Add(this._SqlRPCBatchArray[commandIndex].errors[i]);
				}
				for (int j = this._SqlRPCBatchArray[commandIndex].warningsIndexStart; j < this._SqlRPCBatchArray[commandIndex].warningsIndexEnd; j++)
				{
					sqlErrorCollection.Add(this._SqlRPCBatchArray[commandIndex].warnings[j]);
				}
				result = SqlException.CreateException(sqlErrorCollection, this.Connection.ServerVersion, this.Connection.ClientConnectionId, null);
			}
			return result;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000B93B8 File Offset: 0x000B87B8
		private SmiRequestExecutor SetUpSmiRequest(SqlInternalConnectionSmi innerConnection)
		{
			if (this.Notification != null)
			{
				throw SQL.NotificationsNotAvailableOnContextConnection();
			}
			SmiParameterMetaData[] array = null;
			ParameterPeekAheadValue[] array2 = null;
			int parameterCount = this.GetParameterCount(this.Parameters);
			if (0 < parameterCount)
			{
				array = new SmiParameterMetaData[parameterCount];
				array2 = new ParameterPeekAheadValue[parameterCount];
				for (int i = 0; i < parameterCount; i++)
				{
					SqlParameter sqlParameter = this.Parameters[i];
					sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
					array[i] = sqlParameter.MetaDataForSmi(out array2[i]);
					if (!innerConnection.IsKatmaiOrNewer)
					{
						MetaType metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(array[i].SqlDbType, array[i].IsMultiValued);
						if (!metaTypeFromSqlDbType.Is90Supported)
						{
							throw ADP.VersionDoesNotSupportDataType(metaTypeFromSqlDbType.TypeName);
						}
					}
				}
			}
			CommandType commandType = this.CommandType;
			this._smiRequestContext = innerConnection.InternalContext;
			SmiRequestExecutor smiRequestExecutor = this._smiRequestContext.CreateRequestExecutor(this.CommandText, commandType, array, this.EventSink);
			this.EventSink.ProcessMessagesAndThrow();
			for (int j = 0; j < parameterCount; j++)
			{
				if (ParameterDirection.Output != array[j].Direction && ParameterDirection.ReturnValue != array[j].Direction)
				{
					SqlParameter sqlParameter2 = this.Parameters[j];
					object obj = sqlParameter2.GetCoercedValue();
					if (obj is XmlDataFeed && array[j].SqlDbType != SqlDbType.Xml)
					{
						obj = MetaType.GetStringFromXml(((XmlDataFeed)obj)._source);
					}
					ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(array[j].SqlDbType, array[j].IsMultiValued, obj, null, SmiContextFactory.Instance.NegotiatedSmiVersion);
					if (CommandType.StoredProcedure == commandType && ExtendedClrTypeCode.Empty == extendedClrTypeCode)
					{
						smiRequestExecutor.SetDefault(j);
					}
					else
					{
						int size = sqlParameter2.Size;
						if (size != 0 && (long)size != -1L && !sqlParameter2.SizeInferred)
						{
							SqlDbType sqlDbType = array[j].SqlDbType;
							if (sqlDbType != SqlDbType.Image)
							{
								switch (sqlDbType)
								{
								case SqlDbType.NText:
									if (size != 1073741823)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_2EC;
								case SqlDbType.NVarChar:
									if (size > 0 && size != 1073741823 && array[j].MaxLength == -1L)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_2EC;
								case SqlDbType.Real:
								case SqlDbType.UniqueIdentifier:
								case SqlDbType.SmallDateTime:
								case SqlDbType.SmallInt:
								case SqlDbType.SmallMoney:
								case SqlDbType.TinyInt:
								case (SqlDbType)24:
									goto IL_2EC;
								case SqlDbType.Text:
									break;
								case SqlDbType.Timestamp:
									if ((long)size < SmiMetaData.DefaultTimestamp.MaxLength)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_2EC;
								case SqlDbType.VarBinary:
								case SqlDbType.VarChar:
									if (size > 0 && size != 2147483647 && array[j].MaxLength == -1L)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_2EC;
								case SqlDbType.Variant:
								{
									if (obj == null)
									{
										goto IL_2EC;
									}
									MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(obj, true);
									if ((metaTypeFromValue.IsNCharType && (long)size < 4000L) || (metaTypeFromValue.IsBinType && (long)size < 8000L) || (metaTypeFromValue.IsAnsiType && (long)size < 8000L))
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_2EC;
								}
								case SqlDbType.Xml:
									if (obj != null && ExtendedClrTypeCode.SqlXml != extendedClrTypeCode)
									{
										throw SQL.ParameterSizeRestrictionFailure(j);
									}
									goto IL_2EC;
								default:
									goto IL_2EC;
								}
							}
							if (size != 2147483647)
							{
								throw SQL.ParameterSizeRestrictionFailure(j);
							}
						}
						IL_2EC:
						if (innerConnection.IsKatmaiOrNewer)
						{
							ValueUtilsSmi.SetCompatibleValueV200(this.EventSink, smiRequestExecutor, j, array[j], obj, extendedClrTypeCode, sqlParameter2.Offset, sqlParameter2.Size, array2[j]);
						}
						else
						{
							ValueUtilsSmi.SetCompatibleValue(this.EventSink, smiRequestExecutor, j, array[j], obj, extendedClrTypeCode, sqlParameter2.Offset);
						}
					}
				}
			}
			return smiRequestExecutor;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000B970C File Offset: 0x000B8B0C
		private void WriteBeginExecuteEvent()
		{
			if (SqlEventSource.Log.IsEnabled() && this.Connection != null)
			{
				string commandText = (this.CommandType == CommandType.StoredProcedure) ? this.CommandText : string.Empty;
				SqlEventSource.Log.BeginExecute(this.GetHashCode(), this.Connection.DataSource, this.Connection.Database, commandText);
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000B976C File Offset: 0x000B8B6C
		private void WriteEndExecuteEvent(bool success, int? sqlExceptionNumber, bool synchronous)
		{
			if (SqlEventSource.Log.IsEnabled())
			{
				int num = success ? 1 : 0;
				int num2 = (sqlExceptionNumber != null) ? 2 : 0;
				int num3 = synchronous ? 4 : 0;
				int compositeState = num | num2 | num3;
				SqlEventSource.Log.EndExecute(this.GetHashCode(), compositeState, sqlExceptionNumber.GetValueOrDefault());
			}
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000B97C4 File Offset: 0x000B8BC4
		// Note: this type is marked as 'beforefieldinit'.
		static SqlCommand()
		{
			string[] array = new string[15];
			array[0] = "PARAMETER_NAME";
			array[1] = "PARAMETER_TYPE";
			array[2] = "DATA_TYPE";
			array[4] = "CHARACTER_MAXIMUM_LENGTH";
			array[5] = "NUMERIC_PRECISION";
			array[6] = "NUMERIC_SCALE";
			array[7] = "UDT_CATALOG";
			array[8] = "UDT_SCHEMA";
			array[9] = "TYPE_NAME";
			array[10] = "XML_CATALOGNAME";
			array[11] = "XML_SCHEMANAME";
			array[12] = "XML_SCHEMACOLLECTIONNAME";
			array[13] = "UDT_NAME";
			SqlCommand.PreKatmaiProcParamsNames = array;
			SqlCommand.KatmaiProcParamsNames = new string[]
			{
				"PARAMETER_NAME",
				"PARAMETER_TYPE",
				null,
				"MANAGED_DATA_TYPE",
				"CHARACTER_MAXIMUM_LENGTH",
				"NUMERIC_PRECISION",
				"NUMERIC_SCALE",
				"TYPE_CATALOG_NAME",
				"TYPE_SCHEMA_NAME",
				"TYPE_NAME",
				"XML_CATALOGNAME",
				"XML_SCHEMANAME",
				"XML_SCHEMACOLLECTIONNAME",
				null,
				"SS_DATETIME_PRECISION"
			};
		}

		// Token: 0x04000F0A RID: 3850
		private static int _objectTypeCount;

		// Token: 0x04000F0B RID: 3851
		internal readonly int ObjectID = Interlocked.Increment(ref SqlCommand._objectTypeCount);

		// Token: 0x04000F0C RID: 3852
		private string _commandText;

		// Token: 0x04000F0D RID: 3853
		private CommandType _commandType;

		// Token: 0x04000F0E RID: 3854
		private int _commandTimeout = 30;

		// Token: 0x04000F0F RID: 3855
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x04000F10 RID: 3856
		private bool _designTimeInvisible;

		// Token: 0x04000F11 RID: 3857
		private bool _wasBatchModeColumnEncryptionSettingSetOnce;

		// Token: 0x04000F12 RID: 3858
		private SqlCommandColumnEncryptionSetting _columnEncryptionSetting;

		// Token: 0x04000F13 RID: 3859
		internal SqlDependency _sqlDep;

		// Token: 0x04000F14 RID: 3860
		private bool _inPrepare;

		// Token: 0x04000F15 RID: 3861
		private int _prepareHandle = -1;

		// Token: 0x04000F16 RID: 3862
		private bool _hiddenPrepare;

		// Token: 0x04000F17 RID: 3863
		private int _preparedConnectionCloseCount = -1;

		// Token: 0x04000F18 RID: 3864
		private int _preparedConnectionReconnectCount = -1;

		// Token: 0x04000F19 RID: 3865
		private SqlParameterCollection _parameters;

		// Token: 0x04000F1A RID: 3866
		private SqlConnection _activeConnection;

		// Token: 0x04000F1B RID: 3867
		private bool _dirty;

		// Token: 0x04000F1C RID: 3868
		private SqlCommand.EXECTYPE _execType;

		// Token: 0x04000F1D RID: 3869
		private _SqlRPC[] _rpcArrayOf1;

		// Token: 0x04000F1E RID: 3870
		private _SqlRPC _rpcForEncryption;

		// Token: 0x04000F1F RID: 3871
		private _SqlMetaDataSet _cachedMetaData;

		// Token: 0x04000F20 RID: 3872
		private Dictionary<int, SqlTceCipherInfoEntry> keysToBeSentToEnclave = new Dictionary<int, SqlTceCipherInfoEntry>();

		// Token: 0x04000F21 RID: 3873
		private bool requiresEnclaveComputations;

		// Token: 0x04000F22 RID: 3874
		internal EnclaveDelegate.EnclavePackage enclavePackage;

		// Token: 0x04000F23 RID: 3875
		private SqlEnclaveAttestationParameters enclaveAttestationParameters;

		// Token: 0x04000F24 RID: 3876
		private TaskCompletionSource<object> _reconnectionCompletionSource;

		// Token: 0x04000F25 RID: 3877
		private SqlCommand.CachedAsyncState _cachedAsyncState;

		// Token: 0x04000F26 RID: 3878
		internal int _rowsAffected = -1;

		// Token: 0x04000F27 RID: 3879
		private int _rowsAffectedBySpDescribeParameterEncryption = -1;

		// Token: 0x04000F28 RID: 3880
		private SqlNotificationRequest _notification;

		// Token: 0x04000F29 RID: 3881
		private bool _notificationAutoEnlist = true;

		// Token: 0x04000F2A RID: 3882
		private SqlTransaction _transaction;

		// Token: 0x04000F2B RID: 3883
		private StatementCompletedEventHandler _statementCompletedEventHandler;

		// Token: 0x04000F2C RID: 3884
		private TdsParserStateObject _stateObj;

		// Token: 0x04000F2D RID: 3885
		private volatile bool _pendingCancel;

		// Token: 0x04000F2E RID: 3886
		private bool _batchRPCMode;

		// Token: 0x04000F2F RID: 3887
		private List<_SqlRPC> _RPCList;

		// Token: 0x04000F30 RID: 3888
		private _SqlRPC[] _SqlRPCBatchArray;

		// Token: 0x04000F31 RID: 3889
		private _SqlRPC[] _sqlRPCParameterEncryptionReqArray;

		// Token: 0x04000F32 RID: 3890
		private List<SqlParameterCollection> _parameterCollectionList;

		// Token: 0x04000F33 RID: 3891
		private int _currentlyExecutingBatch;

		// Token: 0x04000F34 RID: 3892
		private int _currentlyExecutingDescribeParameterEncryptionRPC;

		// Token: 0x04000F35 RID: 3893
		private bool _isDescribeParameterEncryptionRPCCurrentlyInProgress;

		// Token: 0x04000F36 RID: 3894
		private volatile bool _internalEndExecuteInitiated;

		// Token: 0x04000F38 RID: 3896
		private SmiContext _smiRequestContext;

		// Token: 0x04000F39 RID: 3897
		private SqlCommand.CommandEventSink _smiEventSink;

		// Token: 0x04000F3A RID: 3898
		private SmiEventSink_DeferedProcessing _outParamEventSink;

		// Token: 0x04000F3B RID: 3899
		internal static readonly string[] PreKatmaiProcParamsNames;

		// Token: 0x04000F3C RID: 3900
		internal static readonly string[] KatmaiProcParamsNames;

		// Token: 0x0200038D RID: 909
		private enum EXECTYPE
		{
			// Token: 0x04001FB4 RID: 8116
			UNPREPARED,
			// Token: 0x04001FB5 RID: 8117
			PREPAREPENDING,
			// Token: 0x04001FB6 RID: 8118
			PREPARED
		}

		// Token: 0x0200038E RID: 910
		private class CachedAsyncState
		{
			// Token: 0x06003492 RID: 13458 RVA: 0x00141640 File Offset: 0x00140A40
			internal CachedAsyncState()
			{
			}

			// Token: 0x17000850 RID: 2128
			// (get) Token: 0x06003493 RID: 13459 RVA: 0x00141664 File Offset: 0x00140A64
			internal SqlDataReader CachedAsyncReader
			{
				get
				{
					return this._cachedAsyncReader;
				}
			}

			// Token: 0x17000851 RID: 2129
			// (get) Token: 0x06003494 RID: 13460 RVA: 0x00141678 File Offset: 0x00140A78
			internal RunBehavior CachedRunBehavior
			{
				get
				{
					return this._cachedRunBehavior;
				}
			}

			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x06003495 RID: 13461 RVA: 0x0014168C File Offset: 0x00140A8C
			internal string CachedSetOptions
			{
				get
				{
					return this._cachedSetOptions;
				}
			}

			// Token: 0x17000853 RID: 2131
			// (get) Token: 0x06003496 RID: 13462 RVA: 0x001416A0 File Offset: 0x00140AA0
			internal bool PendingAsyncOperation
			{
				get
				{
					return this._cachedAsyncResult != null;
				}
			}

			// Token: 0x17000854 RID: 2132
			// (get) Token: 0x06003497 RID: 13463 RVA: 0x001416B8 File Offset: 0x00140AB8
			internal string EndMethodName
			{
				get
				{
					return this._cachedEndMethod;
				}
			}

			// Token: 0x06003498 RID: 13464 RVA: 0x001416CC File Offset: 0x00140ACC
			internal bool IsActiveConnectionValid(SqlConnection activeConnection)
			{
				return this._cachedAsyncConnection == activeConnection && this._cachedAsyncCloseCount == activeConnection.CloseCount;
			}

			// Token: 0x06003499 RID: 13465 RVA: 0x001416F4 File Offset: 0x00140AF4
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void ResetAsyncState()
			{
				this._cachedAsyncCloseCount = -1;
				this._cachedAsyncResult = null;
				if (this._cachedAsyncConnection != null)
				{
					this._cachedAsyncConnection.AsyncCommandInProgress = false;
					this._cachedAsyncConnection = null;
				}
				this._cachedAsyncReader = null;
				this._cachedRunBehavior = RunBehavior.ReturnImmediately;
				this._cachedSetOptions = null;
				this._cachedEndMethod = null;
			}

			// Token: 0x0600349A RID: 13466 RVA: 0x00141748 File Offset: 0x00140B48
			internal void SetActiveConnectionAndResult(TaskCompletionSource<object> completion, string endMethod, SqlConnection activeConnection)
			{
				TdsParser parser = activeConnection.Parser;
				if (parser == null || parser.State == TdsParserState.Closed || parser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
				this._cachedAsyncCloseCount = activeConnection.CloseCount;
				this._cachedAsyncResult = completion;
				if (activeConnection != null && !parser.MARSOn && activeConnection.AsyncCommandInProgress)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				this._cachedAsyncConnection = activeConnection;
				this._cachedAsyncConnection.AsyncCommandInProgress = true;
				this._cachedEndMethod = endMethod;
			}

			// Token: 0x0600349B RID: 13467 RVA: 0x001417BC File Offset: 0x00140BBC
			internal void SetAsyncReaderState(SqlDataReader ds, RunBehavior runBehavior, string optionSettings)
			{
				this._cachedAsyncReader = ds;
				this._cachedRunBehavior = runBehavior;
				this._cachedSetOptions = optionSettings;
			}

			// Token: 0x04001FB7 RID: 8119
			private int _cachedAsyncCloseCount = -1;

			// Token: 0x04001FB8 RID: 8120
			private TaskCompletionSource<object> _cachedAsyncResult;

			// Token: 0x04001FB9 RID: 8121
			private SqlConnection _cachedAsyncConnection;

			// Token: 0x04001FBA RID: 8122
			private SqlDataReader _cachedAsyncReader;

			// Token: 0x04001FBB RID: 8123
			private RunBehavior _cachedRunBehavior = RunBehavior.ReturnImmediately;

			// Token: 0x04001FBC RID: 8124
			private string _cachedSetOptions;

			// Token: 0x04001FBD RID: 8125
			private string _cachedEndMethod;
		}

		// Token: 0x0200038F RID: 911
		private sealed class CommandEventSink : SmiEventSink_Default
		{
			// Token: 0x0600349C RID: 13468 RVA: 0x001417E0 File Offset: 0x00140BE0
			internal CommandEventSink(SqlCommand command)
			{
				this._command = command;
			}

			// Token: 0x0600349D RID: 13469 RVA: 0x001417FC File Offset: 0x00140BFC
			internal override void StatementCompleted(int rowsAffected)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlCommand.CommandEventSink.StatementCompleted|ADV> %d#, rowsAffected=%d.\n", this._command.ObjectID, rowsAffected);
				}
				this._command.InternalRecordsAffected = rowsAffected;
			}

			// Token: 0x0600349E RID: 13470 RVA: 0x00141834 File Offset: 0x00140C34
			internal override void BatchCompleted()
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlCommand.CommandEventSink.BatchCompleted|ADV> %d#.\n", this._command.ObjectID);
				}
			}

			// Token: 0x0600349F RID: 13471 RVA: 0x00141860 File Offset: 0x00140C60
			internal override void ParametersAvailable(SmiParameterMetaData[] metaData, ITypedGettersV3 parameterValues)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlCommand.CommandEventSink.ParametersAvailable|ADV> %d# metaData.Length=%d.\n", this._command.ObjectID, (metaData != null) ? metaData.Length : -1);
					if (metaData != null)
					{
						for (int i = 0; i < metaData.Length; i++)
						{
							Bid.Trace("<sc.SqlCommand.CommandEventSink.ParametersAvailable|ADV> %d#, metaData[%d] is %ls%ls\n", this._command.ObjectID, i, metaData[i].GetType().ToString(), metaData[i].TraceString());
						}
					}
				}
				this._command.OnParametersAvailableSmi(metaData, parameterValues);
			}

			// Token: 0x060034A0 RID: 13472 RVA: 0x001418DC File Offset: 0x00140CDC
			internal override void ParameterAvailable(SmiParameterMetaData metaData, SmiTypedGetterSetter parameterValues, int ordinal)
			{
				if (Bid.AdvancedOn && metaData != null)
				{
					Bid.Trace("<sc.SqlCommand.CommandEventSink.ParameterAvailable|ADV> %d#, metaData[%d] is %ls%ls\n", this._command.ObjectID, ordinal, metaData.GetType().ToString(), metaData.TraceString());
				}
				this._command.OnParameterAvailableSmi(metaData, parameterValues, ordinal);
			}

			// Token: 0x04001FBE RID: 8126
			private SqlCommand _command;
		}
	}
}
