using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Transactions;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020002CB RID: 715
	[DefaultEvent("InfoMessage")]
	public sealed class SqlConnection : DbConnection, ICloneable
	{
		// Token: 0x06002478 RID: 9336 RVA: 0x00297378 File Offset: 0x00296778
		public SqlConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00297398 File Offset: 0x00296798
		private SqlConnection(SqlConnection connection)
		{
			this.ObjectID = Interlocked.Increment(ref SqlConnection._objectTypeCount);
			base..ctor();
			GC.SuppressFinalize(this);
			this.CopyFrom(connection);
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x002973C8 File Offset: 0x002967C8
		// (set) Token: 0x0600247B RID: 9339 RVA: 0x002973E8 File Offset: 0x002967E8
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlConnection_StatisticsEnabled")]
		[DefaultValue(false)]
		public bool StatisticsEnabled
		{
			get
			{
				return this._collectstats;
			}
			set
			{
				if (this.IsContextConnection)
				{
					if (value)
					{
						throw SQL.NotAvailableOnContextConnection();
					}
				}
				else
				{
					if (value)
					{
						if (ConnectionState.Open == this.State)
						{
							if (this._statistics == null)
							{
								this._statistics = new SqlStatistics();
								ADP.TimerCurrent(out this._statistics._openTimestamp);
							}
							this.Parser.Statistics = this._statistics;
						}
					}
					else if (this._statistics != null && ConnectionState.Open == this.State)
					{
						TdsParser parser = this.Parser;
						parser.Statistics = null;
						ADP.TimerCurrent(out this._statistics._closeTimestamp);
					}
					this._collectstats = value;
				}
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x00297488 File Offset: 0x00296888
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x002974A8 File Offset: 0x002968A8
		internal bool AsycCommandInProgress
		{
			get
			{
				return this._AsycCommandInProgress;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._AsycCommandInProgress = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x002974C8 File Offset: 0x002968C8
		internal bool IsContextConnection
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				bool result = false;
				if (sqlConnectionString != null)
				{
					result = sqlConnectionString.ContextConnection;
				}
				return result;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x002974F8 File Offset: 0x002968F8
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TransactionBinding;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x00297518 File Offset: 0x00296918
		internal SqlConnectionString.TypeSystem TypeSystem
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemVersion;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00297538 File Offset: 0x00296938
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x00297558 File Offset: 0x00296958
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x00297578 File Offset: 0x00296978
		[RecommendedAsConfigurable(true)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlConnection_ConnectionString")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlConnectionStringEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				this.ConnectionString_Set(value);
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x00297598 File Offset: 0x00296998
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("SqlConnection_ConnectionTimeout")]
		public override int ConnectionTimeout
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (sqlConnectionString == null)
				{
					return 15;
				}
				return sqlConnectionString.ConnectTimeout;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x002975C8 File Offset: 0x002969C8
		[ResDescription("SqlConnection_Database")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string result;
				if (sqlInternalConnection != null)
				{
					result = sqlInternalConnection.CurrentDatabase;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.InitialCatalog : "");
				}
				return result;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x00297618 File Offset: 0x00296A18
		[Browsable(true)]
		[ResDescription("SqlConnection_DataSource")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DataSource
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string result;
				if (sqlInternalConnection != null)
				{
					result = sqlInternalConnection.CurrentDataSource;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.DataSource : "");
				}
				return result;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x00297668 File Offset: 0x00296A68
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("SqlConnection_PacketSize")]
		public int PacketSize
		{
			get
			{
				if (this.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				int result;
				if (sqlInternalConnectionTds != null)
				{
					result = sqlInternalConnectionTds.PacketSize;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.PacketSize : 8000);
				}
				return result;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x002976C8 File Offset: 0x00296AC8
		[ResDescription("SqlConnection_ServerVersion")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ServerVersion
		{
			get
			{
				return this.GetOpenConnection().ServerVersion;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x002976E8 File Offset: 0x00296AE8
		internal SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00297708 File Offset: 0x00296B08
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlConnection_WorkstationId")]
		public string WorkstationId
		{
			get
			{
				if (this.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				string text = (sqlConnectionString != null) ? sqlConnectionString.WorkstationId : null;
				if (text == null)
				{
					text = Environment.MachineName;
				}
				return text;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600248B RID: 9355 RVA: 0x00297748 File Offset: 0x00296B48
		// (remove) Token: 0x0600248C RID: 9356 RVA: 0x00297768 File Offset: 0x00296B68
		[ResCategory("DataCategory_InfoMessage")]
		[ResDescription("DbConnection_InfoMessage")]
		public event SqlInfoMessageEventHandler InfoMessage
		{
			add
			{
				base.Events.AddHandler(SqlConnection.EventInfoMessage, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlConnection.EventInfoMessage, value);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x00297788 File Offset: 0x00296B88
		// (set) Token: 0x0600248E RID: 9358 RVA: 0x002977A8 File Offset: 0x00296BA8
		public bool FireInfoMessageEventOnUserErrors
		{
			get
			{
				return this._fireInfoMessageEventOnUserErrors;
			}
			set
			{
				this._fireInfoMessageEventOnUserErrors = value;
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x002977C8 File Offset: 0x00296BC8
		public new SqlTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, null);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x002977E8 File Offset: 0x00296BE8
		public new SqlTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginTransaction(iso, null);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00297808 File Offset: 0x00296C08
		public SqlTransaction BeginTransaction(string transactionName)
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, transactionName);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00297828 File Offset: 0x00296C28
		public SqlTransaction BeginTransaction(IsolationLevel iso, string transactionName)
		{
			SqlStatistics statistics = null;
			string a = ADP.IsEmpty(transactionName) ? "None" : transactionName;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.BeginTransaction|API> %d#, iso=%d{ds.IsolationLevel}, transactionName='%ls'\n", this.ObjectID, (int)iso, a);
			SqlTransaction result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				SqlTransaction sqlTransaction = this.GetOpenConnection().BeginSqlTransaction(iso, transactionName);
				GC.KeepAlive(this);
				result = sqlTransaction;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x002978B8 File Offset: 0x00296CB8
		public override void ChangeDatabase(string database)
		{
			SqlStatistics statistics = null;
			SNIHandle target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this);
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.InnerConnection.ChangeDatabase(database);
			}
			catch (OutOfMemoryException e)
			{
				this.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00297988 File Offset: 0x00296D88
		public static void ClearAllPools()
		{
			new SqlClientPermission(PermissionState.Unrestricted).Demand();
			SqlConnectionFactory.SingletonInstance.ClearAllPools();
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x002979B8 File Offset: 0x00296DB8
		public static void ClearPool(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionOptions userConnectionOptions = connection.UserConnectionOptions;
			if (userConnectionOptions != null)
			{
				userConnectionOptions.DemandPermission();
				if (connection.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlConnectionFactory.SingletonInstance.ClearPool(connection);
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00297A08 File Offset: 0x00296E08
		object ICloneable.Clone()
		{
			SqlConnection sqlConnection = new SqlConnection(this);
			Bid.Trace("<sc.SqlConnection.Clone|API> %d#, clone=%d#\n", this.ObjectID, sqlConnection.ObjectID);
			return sqlConnection;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00297A38 File Offset: 0x00296E38
		public override void Close()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.Close|API> %d#", this.ObjectID);
			try
			{
				SqlStatistics statistics = null;
				SNIHandle target = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					target = SqlInternalConnection.GetBestEffortCleanupTarget(this);
					statistics = SqlStatistics.StartTimer(this.Statistics);
					lock (this.InnerConnection)
					{
						this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
					}
					if (this.Statistics != null)
					{
						ADP.TimerCurrent(out this._statistics._closeTimestamp);
					}
				}
				catch (OutOfMemoryException e)
				{
					this.Abort(e);
					throw;
				}
				catch (StackOverflowException e2)
				{
					this.Abort(e2);
					throw;
				}
				catch (ThreadAbortException e3)
				{
					this.Abort(e3);
					SqlInternalConnection.BestEffortCleanup(target);
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(statistics);
				}
			}
			finally
			{
				SqlDebugContext sdc = this._sdc;
				this._sdc = null;
				Bid.ScopeLeave(ref intPtr);
				if (sdc != null)
				{
					sdc.Dispose();
				}
			}
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00297BA8 File Offset: 0x00296FA8
		public new SqlCommand CreateCommand()
		{
			return new SqlCommand(null, this);
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00297BC8 File Offset: 0x00296FC8
		private void DisposeMe(bool disposing)
		{
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00297BD8 File Offset: 0x00296FD8
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			this.EnlistDistributedTransactionHelper(transaction);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00297C08 File Offset: 0x00297008
		public override void Open()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.Open|API> %d#", this.ObjectID);
			try
			{
				if (this.StatisticsEnabled)
				{
					if (this._statistics == null)
					{
						this._statistics = new SqlStatistics();
					}
					else
					{
						this._statistics.ContinueOnNewConnection();
					}
				}
				SNIHandle target = null;
				SqlStatistics statistics = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					statistics = SqlStatistics.StartTimer(this.Statistics);
					this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
					target = SqlInternalConnection.GetBestEffortCleanupTarget(this);
					SqlInternalConnectionSmi sqlInternalConnectionSmi = this.InnerConnection as SqlInternalConnectionSmi;
					if (sqlInternalConnectionSmi != null)
					{
						sqlInternalConnectionSmi.AutomaticEnlistment();
					}
					else
					{
						if (this.StatisticsEnabled)
						{
							ADP.TimerCurrent(out this._statistics._openTimestamp);
							this.Parser.Statistics = this._statistics;
						}
						else
						{
							this.Parser.Statistics = null;
							this._statistics = null;
						}
						this.CompleteOpen();
					}
				}
				catch (OutOfMemoryException e)
				{
					this.Abort(e);
					throw;
				}
				catch (StackOverflowException e2)
				{
					this.Abort(e2);
					throw;
				}
				catch (ThreadAbortException e3)
				{
					this.Abort(e3);
					SqlInternalConnection.BestEffortCleanup(target);
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(statistics);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x00297D98 File Offset: 0x00297198
		internal bool HasLocalTransaction
		{
			get
			{
				return this.GetOpenConnection().HasLocalTransaction;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x00297DB8 File Offset: 0x002971B8
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				return this.GetOpenConnection().HasLocalTransactionFromAPI;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x00297DD8 File Offset: 0x002971D8
		internal bool IsShiloh
		{
			get
			{
				return this.GetOpenConnection().IsShiloh;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00297DF8 File Offset: 0x002971F8
		internal bool IsYukonOrNewer
		{
			get
			{
				return this.GetOpenConnection().IsYukonOrNewer;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x00297E18 File Offset: 0x00297218
		internal bool IsKatmaiOrNewer
		{
			get
			{
				return this.GetOpenConnection().IsKatmaiOrNewer;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x00297E38 File Offset: 0x00297238
		internal TdsParser Parser
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.GetOpenConnection() as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds == null)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				return sqlInternalConnectionTds.Parser;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x00297E68 File Offset: 0x00297268
		internal bool Asynchronous
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return sqlConnectionString != null && sqlConnectionString.Asynchronous;
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00297E98 File Offset: 0x00297298
		internal void AddPreparedCommand(SqlCommand cmd)
		{
			this.GetOpenConnection().AddPreparedCommand(cmd);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00297EB8 File Offset: 0x002972B8
		internal void ValidateConnectionForExecute(string method, SqlCommand command)
		{
			SqlInternalConnection openConnection = this.GetOpenConnection(method);
			openConnection.ValidateConnectionForExecute(command);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00297ED8 File Offset: 0x002972D8
		internal static string FixupDatabaseTransactionName(string name)
		{
			if (!ADP.IsEmpty(name))
			{
				return "[" + name.Replace("]", "]]") + "]";
			}
			return name;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00297F18 File Offset: 0x00297318
		internal void OnError(SqlException exception, bool breakConnection)
		{
			if (breakConnection && ConnectionState.Open == this.State)
			{
				Bid.Trace("<sc.SqlConnection.OnError|INFO> %d#, Connection broken.\n", this.ObjectID);
				this.Close();
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
			this.OnInfoMessage(new SqlInfoMessageEventArgs(exception));
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00297F68 File Offset: 0x00297368
		internal void RemovePreparedCommand(SqlCommand cmd)
		{
			this.GetOpenConnection().RemovePreparedCommand(cmd);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00297F88 File Offset: 0x00297388
		private void CompleteOpen()
		{
			if (!this.GetOpenConnection().IsYukonOrNewer && Debugger.IsAttached)
			{
				bool flag = false;
				try
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
					flag = true;
				}
				catch (SecurityException e)
				{
					ADP.TraceExceptionWithoutRethrow(e);
				}
				if (flag)
				{
					this.CheckSQLDebugOnConnect();
				}
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x00297FE8 File Offset: 0x002973E8
		internal SqlInternalConnection GetOpenConnection()
		{
			SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
			if (sqlInternalConnection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnection;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00298018 File Offset: 0x00297418
		internal SqlInternalConnection GetOpenConnection(string method)
		{
			DbConnectionInternal innerConnection = this.InnerConnection;
			SqlInternalConnection sqlInternalConnection = innerConnection as SqlInternalConnection;
			if (sqlInternalConnection == null)
			{
				throw ADP.OpenConnectionRequired(method, innerConnection.State);
			}
			return sqlInternalConnection;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00298048 File Offset: 0x00297448
		internal SqlInternalConnectionTds GetOpenTdsConnection()
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00298078 File Offset: 0x00297478
		internal SqlInternalConnectionTds GetOpenTdsConnection(string method)
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.OpenConnectionRequired(method, sqlInternalConnectionTds.State);
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x002980A8 File Offset: 0x002974A8
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent)
		{
			if (Bid.TraceOn)
			{
				Bid.Trace("<sc.SqlConnection.OnInfoMessage|API|INFO> %d#, Message='%ls'\n", this.ObjectID, (imevent != null) ? imevent.Message : "");
			}
			SqlInfoMessageEventHandler sqlInfoMessageEventHandler = (SqlInfoMessageEventHandler)base.Events[SqlConnection.EventInfoMessage];
			if (sqlInfoMessageEventHandler != null)
			{
				try
				{
					sqlInfoMessageEventHandler(this, imevent);
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

		// Token: 0x060024AE RID: 9390 RVA: 0x00298138 File Offset: 0x00297538
		private void CheckSQLDebugOnConnect()
		{
			uint currentProcessId = (uint)SafeNativeMethods.GetCurrentProcessId();
			string text;
			if (ADP.IsPlatformNT5)
			{
				text = "Global\\SqlClientSSDebug";
			}
			else
			{
				text = "SqlClientSSDebug";
			}
			text += currentProcessId.ToString(CultureInfo.InvariantCulture);
			IntPtr intPtr = NativeMethods.OpenFileMappingA(4, false, text);
			if (ADP.PtrZero != intPtr)
			{
				IntPtr intPtr2 = NativeMethods.MapViewOfFile(intPtr, 4, 0, 0, IntPtr.Zero);
				if (ADP.PtrZero != intPtr2)
				{
					SqlDebugContext sqlDebugContext = new SqlDebugContext();
					sqlDebugContext.hMemMap = intPtr;
					sqlDebugContext.pMemMap = intPtr2;
					sqlDebugContext.pid = currentProcessId;
					this.CheckSQLDebug(sqlDebugContext);
					this._sdc = sqlDebugContext;
				}
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x002981D8 File Offset: 0x002975D8
		internal void CheckSQLDebug()
		{
			if (this._sdc != null)
			{
				this.CheckSQLDebug(this._sdc);
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x00298208 File Offset: 0x00297608
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private void CheckSQLDebug(SqlDebugContext sdc)
		{
			uint currentThreadId = (uint)AppDomain.GetCurrentThreadId();
			SqlConnection.RefreshMemoryMappedData(sdc);
			if (!sdc.active && sdc.fOption)
			{
				sdc.active = true;
				sdc.tid = currentThreadId;
				try
				{
					this.IssueSQLDebug(1U, sdc.machineName, sdc.pid, sdc.dbgpid, sdc.sdiDllName, sdc.data);
					sdc.tid = 0U;
				}
				catch
				{
					sdc.active = false;
					throw;
				}
			}
			if (sdc.active)
			{
				if (!sdc.fOption)
				{
					sdc.Dispose();
					this.IssueSQLDebug(0U, null, 0U, 0U, null, null);
					return;
				}
				if (sdc.tid != currentThreadId)
				{
					sdc.tid = currentThreadId;
					try
					{
						this.IssueSQLDebug(2U, null, sdc.pid, sdc.tid, null, null);
					}
					catch
					{
						sdc.tid = 0U;
						throw;
					}
				}
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x00298308 File Offset: 0x00297708
		private void IssueSQLDebug(uint option, string machineName, uint pid, uint id, string sdiDllName, byte[] data)
		{
			if (this.GetOpenConnection().IsYukonOrNewer)
			{
				return;
			}
			SqlCommand sqlCommand = new SqlCommand("sp_sdidebug", this);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.VarChar, TdsEnums.SQLDEBUG_MODE_NAMES[(int)((UIntPtr)option)].Length);
			sqlParameter.Value = TdsEnums.SQLDEBUG_MODE_NAMES[(int)((UIntPtr)option)];
			sqlCommand.Parameters.Add(sqlParameter);
			if (option == 1U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.VarChar, sdiDllName.Length);
				sqlParameter.Value = sdiDllName;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter(null, SqlDbType.VarChar, machineName.Length);
				sqlParameter.Value = machineName;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			if (option != 0U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.Int);
				sqlParameter.Value = pid;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter(null, SqlDbType.Int);
				sqlParameter.Value = id;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			if (option == 1U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.VarBinary, (data != null) ? data.Length : 0);
				sqlParameter.Value = data;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			sqlCommand.ExecuteNonQuery();
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00298438 File Offset: 0x00297838
		public static void ChangePassword(string connectionString, string newPassword)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.ChangePassword|API>");
			try
			{
				if (ADP.IsEmpty(connectionString))
				{
					throw SQL.ChangePasswordArgumentMissing("connectionString");
				}
				if (ADP.IsEmpty(newPassword))
				{
					throw SQL.ChangePasswordArgumentMissing("newPassword");
				}
				if (128 < newPassword.Length)
				{
					throw ADP.InvalidArgumentLength("newPassword", 128);
				}
				SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(connectionString);
				if (sqlConnectionString.IntegratedSecurity)
				{
					throw SQL.ChangePasswordConflictsWithSSPI();
				}
				if (!ADP.IsEmpty(sqlConnectionString.AttachDBFilename))
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("attachdbfilename");
				}
				if (sqlConnectionString.ContextConnection)
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("context connection");
				}
				PermissionSet permissionSet = sqlConnectionString.CreatePermissionSet();
				permissionSet.Demand();
				using (SqlInternalConnectionTds sqlInternalConnectionTds = new SqlInternalConnectionTds(null, sqlConnectionString, null, newPassword, null, false))
				{
					if (!sqlInternalConnectionTds.IsYukonOrNewer)
					{
						throw SQL.ChangePasswordRequiresYukon();
					}
				}
				SqlConnectionFactory.SingletonInstance.ClearPool(connectionString);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00298558 File Offset: 0x00297958
		private static void RefreshMemoryMappedData(SqlDebugContext sdc)
		{
			MEMMAP memmap = (MEMMAP)Marshal.PtrToStructure(sdc.pMemMap, typeof(MEMMAP));
			sdc.dbgpid = memmap.dbgpid;
			sdc.fOption = (memmap.fOption == 1U);
			Encoding encoding = Encoding.GetEncoding(1252);
			sdc.machineName = encoding.GetString(memmap.rgbMachineName, 0, memmap.rgbMachineName.Length);
			sdc.sdiDllName = encoding.GetString(memmap.rgbDllName, 0, memmap.rgbDllName.Length);
			sdc.data = memmap.rgbData;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x002985F8 File Offset: 0x002979F8
		public void ResetStatistics()
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this.Statistics != null)
			{
				this.Statistics.Reset();
				if (ConnectionState.Open == this.State)
				{
					ADP.TimerCurrent(out this._statistics._openTimestamp);
				}
			}
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x00298648 File Offset: 0x00297A48
		public IDictionary RetrieveStatistics()
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this.Statistics != null)
			{
				this.UpdateStatistics();
				return this.Statistics.GetHashtable();
			}
			return new SqlStatistics().GetHashtable();
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00298688 File Offset: 0x00297A88
		private void UpdateStatistics()
		{
			if (ConnectionState.Open == this.State)
			{
				ADP.TimerCurrent(out this._statistics._closeTimestamp);
			}
			this.Statistics.UpdateStatistics();
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x002986C8 File Offset: 0x00297AC8
		internal static void CheckGetExtendedUDTInfo(SqlMetaDataPriv metaData, bool fThrow)
		{
			if (metaData.udtType == null)
			{
				metaData.udtType = Type.GetType(metaData.udtAssemblyQualifiedName, fThrow);
				if (fThrow && metaData.udtType == null)
				{
					throw SQL.UDTUnexpectedResult(metaData.udtAssemblyQualifiedName);
				}
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00298708 File Offset: 0x00297B08
		internal object GetUdtValue(object value, SqlMetaDataPriv metaData, bool returnDBNull)
		{
			if (returnDBNull && ADP.IsNull(value))
			{
				return DBNull.Value;
			}
			if (ADP.IsNull(value))
			{
				Type udtType = metaData.udtType;
				return udtType.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
			}
			MemoryStream s = new MemoryStream((byte[])value);
			return SerializationHelperSql9.Deserialize(s, metaData.udtType);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x00298778 File Offset: 0x00297B78
		internal byte[] GetBytes(object o)
		{
			Format format = Format.Native;
			int num = 0;
			return this.GetBytes(o, out format, out num);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x00298798 File Offset: 0x00297B98
		internal byte[] GetBytes(object o, out Format format, out int maxSize)
		{
			SqlUdtInfo infoFromType = AssemblyCache.GetInfoFromType(o.GetType());
			maxSize = infoFromType.MaxByteSize;
			format = infoFromType.SerializationFormat;
			if (maxSize < -1 || maxSize >= 65535)
			{
				throw new InvalidOperationException(o.GetType() + ": invalid Size");
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream((maxSize < 0) ? 0 : maxSize))
			{
				SerializationHelperSql9.Serialize(memoryStream, o);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00298838 File Offset: 0x00297C38
		public SqlConnection()
		{
			this.ObjectID = Interlocked.Increment(ref SqlConnection._objectTypeCount);
			base..ctor();
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00298878 File Offset: 0x00297C78
		private void CopyFrom(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x002988D8 File Offset: 0x00297CD8
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x002988F8 File Offset: 0x00297CF8
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return SqlConnection._connectionFactory;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x00298918 File Offset: 0x00297D18
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00298938 File Offset: 0x00297D38
		private string ConnectionString_Get()
		{
			Bid.Trace("<prov.DbConnectionHelper.ConnectionString_Get|API> %d#\n", this.ObjectID);
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00298978 File Offset: 0x00297D78
		private void ConnectionString_Set(string value)
		{
			DbConnectionOptions dbConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(value, null, ref dbConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = dbConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
			if (Bid.TraceOn)
			{
				string a = (dbConnectionOptions != null) ? dbConnectionOptions.UsersConnectionStringForTrace() : "";
				Bid.Trace("<prov.DbConnectionHelper.ConnectionString_Set|API> %d#, '%ls'\n", this.ObjectID, a);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00298A18 File Offset: 0x00297E18
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x00298A38 File Offset: 0x00297E38
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x00298A58 File Offset: 0x00297E58
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x00298A78 File Offset: 0x00297E78
		[Browsable(false)]
		[ResDescription("DbConnection_State")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				return this.InnerConnection.State;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x00298A98 File Offset: 0x00297E98
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x00298AB8 File Offset: 0x00297EB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
			if (e is OutOfMemoryException)
			{
				Bid.Trace("<prov.DbConnectionHelper.Abort|RES|INFO|CPOOL> %d#, Aborting operation due to asynchronous exception: %ls\n", this.ObjectID, "OutOfMemory");
				return;
			}
			Bid.Trace("<prov.DbConnectionHelper.Abort|RES|INFO|CPOOL> %d#, Aborting operation due to asynchronous exception: %ls\n", this.ObjectID, e.ToString());
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00298B28 File Offset: 0x00297F28
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00298B48 File Offset: 0x00297F48
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionHelper.BeginDbTransaction|API> %d#, isolationLevel=%d{ds.IsolationLevel}", this.ObjectID, (int)isolationLevel);
			DbTransaction result;
			try
			{
				DbTransaction dbTransaction = this.InnerConnection.BeginTransaction(isolationLevel);
				result = dbTransaction;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00298BA8 File Offset: 0x00297FA8
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionHelper.CreateDbCommand|API> %d#\n", this.ObjectID);
			try
			{
				DbProviderFactory providerFactory = this.ConnectionFactory.ProviderFactory;
				dbCommand = providerFactory.CreateCommand();
				dbCommand.Connection = this;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return dbCommand;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00298C18 File Offset: 0x00298018
		private static CodeAccessPermission CreateExecutePermission()
		{
			DBDataPermission dbdataPermission = (DBDataPermission)SqlConnectionFactory.SingletonInstance.ProviderFactory.CreatePermission(PermissionState.None);
			dbdataPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return dbdataPermission;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00298C58 File Offset: 0x00298058
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00298C98 File Offset: 0x00298098
		private void EnlistDistributedTransactionHelper(ITransaction transaction)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(SqlConnection.ExecutePermission);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Demand();
			Bid.Trace("<prov.DbConnectionHelper.EnlistDistributedTransactionHelper|RES|TRAN> %d#, Connection enlisting in a transaction.\n", this.ObjectID);
			Transaction transaction2 = null;
			if (transaction != null)
			{
				transaction2 = TransactionInterop.GetTransactionFromDtcTransaction((IDtcTransaction)transaction);
			}
			this.InnerConnection.EnlistTransaction(transaction2);
			GC.KeepAlive(this);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00298D08 File Offset: 0x00298108
		public override void EnlistTransaction(Transaction transaction)
		{
			SqlConnection.ExecutePermission.Demand();
			Bid.Trace("<prov.DbConnectionHelper.EnlistTransaction|RES|TRAN> %d#, Connection enlisting in a transaction.\n", this.ObjectID);
			DbConnectionInternal innerConnection = this.InnerConnection;
			if (!innerConnection.HasEnlistedTransaction)
			{
				innerConnection.EnlistTransaction(transaction);
				GC.KeepAlive(this);
				return;
			}
			if (innerConnection.EnlistedTransaction.Equals(transaction))
			{
				return;
			}
			throw ADP.TransactionPresent();
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00298D68 File Offset: 0x00298168
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00298D88 File Offset: 0x00298188
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00298DA8 File Offset: 0x002981A8
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00298DC8 File Offset: 0x002981C8
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00298DE8 File Offset: 0x002981E8
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			SqlConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00298E28 File Offset: 0x00298228
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00298E48 File Offset: 0x00298248
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = (poolGroup != null) ? poolGroup.ConnectionOptions : null;
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			userConnectionOptions.DemandPermission();
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00298E88 File Offset: 0x00298288
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00298EA8 File Offset: 0x002982A8
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00298F28 File Offset: 0x00298328
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00298F48 File Offset: 0x00298348
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00298F68 File Offset: 0x00298368
		[Conditional("DEBUG")]
		internal static void VerifyExecutePermission()
		{
			try
			{
				SqlConnection.ExecutePermission.Demand();
			}
			catch (SecurityException)
			{
				throw;
			}
		}

		// Token: 0x04001758 RID: 5976
		private static readonly object EventInfoMessage = new object();

		// Token: 0x04001759 RID: 5977
		private SqlDebugContext _sdc;

		// Token: 0x0400175A RID: 5978
		private bool _AsycCommandInProgress;

		// Token: 0x0400175B RID: 5979
		internal SqlStatistics _statistics;

		// Token: 0x0400175C RID: 5980
		private bool _collectstats;

		// Token: 0x0400175D RID: 5981
		private bool _fireInfoMessageEventOnUserErrors;

		// Token: 0x0400175E RID: 5982
		private static readonly DbConnectionFactory _connectionFactory = SqlConnectionFactory.SingletonInstance;

		// Token: 0x0400175F RID: 5983
		internal static readonly CodeAccessPermission ExecutePermission = SqlConnection.CreateExecutePermission();

		// Token: 0x04001760 RID: 5984
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04001761 RID: 5985
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x04001762 RID: 5986
		private DbConnectionInternal _innerConnection;

		// Token: 0x04001763 RID: 5987
		private int _closeCount;

		// Token: 0x04001764 RID: 5988
		private static int _objectTypeCount;

		// Token: 0x04001765 RID: 5989
		internal readonly int ObjectID;
	}
}
