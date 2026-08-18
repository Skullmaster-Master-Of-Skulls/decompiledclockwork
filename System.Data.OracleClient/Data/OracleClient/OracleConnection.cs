using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Transactions;

namespace System.Data.OracleClient
{
	// Token: 0x02000054 RID: 84
	[DefaultEvent("InfoMessage")]
	public sealed class OracleConnection : DbConnection, ICloneable
	{
		// Token: 0x06000314 RID: 788 RVA: 0x000624C4 File Offset: 0x000618C4
		public OracleConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000624E4 File Offset: 0x000618E4
		internal OracleConnection(OracleConnection connection) : this()
		{
			this.CopyFrom(connection);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00062504 File Offset: 0x00061904
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00062524 File Offset: 0x00061924
		[RefreshProperties(RefreshProperties.All)]
		[Editor("Microsoft.VSDesigner.Data.Oracle.Design.OracleConnectionStringEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("OracleConnection_ConnectionString")]
		[RecommendedAsConfigurable(true)]
		[ResCategory("OracleCategory_Data")]
		[DefaultValue("")]
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00062544 File Offset: 0x00061944
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override int ConnectionTimeout
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00062554 File Offset: 0x00061954
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00062574 File Offset: 0x00061974
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("OracleConnection_DataSource")]
		public override string DataSource
		{
			get
			{
				OracleConnectionString oracleConnectionString = (OracleConnectionString)this.ConnectionOptions;
				string result = string.Empty;
				if (oracleConnectionString != null)
				{
					result = oracleConnectionString.DataSource;
				}
				return result;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600031B RID: 795 RVA: 0x000625A4 File Offset: 0x000619A4
		internal OciEnvironmentHandle EnvironmentHandle
		{
			get
			{
				return this.GetOpenInternalConnection().EnvironmentHandle;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000625C4 File Offset: 0x000619C4
		internal OciErrorHandle ErrorHandle
		{
			get
			{
				return this.GetOpenInternalConnection().ErrorHandle;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000625E4 File Offset: 0x000619E4
		internal bool HasTransaction
		{
			get
			{
				return this.GetOpenInternalConnection().HasTransaction;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00062604 File Offset: 0x00061A04
		internal TimeSpan ServerTimeZoneAdjustmentToUTC
		{
			get
			{
				return this.GetOpenInternalConnection().GetServerTimeZoneAdjustmentToUTC(this);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00062624 File Offset: 0x00061A24
		[Browsable(false)]
		[ResDescription("OracleConnection_ServerVersion")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ServerVersion
		{
			get
			{
				return this.GetOpenInternalConnection().ServerVersion;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00062644 File Offset: 0x00061A44
		internal bool ServerVersionAtLeastOracle8
		{
			get
			{
				return this.GetOpenInternalConnection().ServerVersionAtLeastOracle8;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00062664 File Offset: 0x00061A64
		internal bool ServerVersionAtLeastOracle9i
		{
			get
			{
				return this.GetOpenInternalConnection().ServerVersionAtLeastOracle9i;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00062684 File Offset: 0x00061A84
		internal OciServiceContextHandle ServiceContextHandle
		{
			get
			{
				return this.GetOpenInternalConnection().ServiceContextHandle;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000626A4 File Offset: 0x00061AA4
		internal OracleTransaction Transaction
		{
			get
			{
				return this.GetOpenInternalConnection().Transaction;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000626C4 File Offset: 0x00061AC4
		// (set) Token: 0x06000325 RID: 805 RVA: 0x000626E4 File Offset: 0x00061AE4
		internal TransactionState TransactionState
		{
			get
			{
				return this.GetOpenInternalConnection().TransactionState;
			}
			set
			{
				this.GetOpenInternalConnection().TransactionState = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00062704 File Offset: 0x00061B04
		internal bool UnicodeEnabled
		{
			get
			{
				return this.GetOpenInternalConnection().UnicodeEnabled;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00062724 File Offset: 0x00061B24
		public new OracleTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00062744 File Offset: 0x00061B44
		public new OracleTransaction BeginTransaction(IsolationLevel il)
		{
			return (OracleTransaction)base.BeginTransaction(il);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00062764 File Offset: 0x00061B64
		public override void ChangeDatabase(string value)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ora.OracleConnection.ChangeDatabase|API> %d#, value='%ls'\n", this.ObjectID, value);
			try
			{
				throw ADP.ChangeDatabaseNotSupported();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000627B4 File Offset: 0x00061BB4
		internal void CheckError(OciErrorHandle errorHandle, int rc)
		{
			OCI.RETURNCODE returncode = (OCI.RETURNCODE)rc;
			switch (returncode)
			{
			case OCI.RETURNCODE.OCI_INVALID_HANDLE:
				throw ADP.InvalidOperation(Res.GetString("ADP_InternalError", new object[]
				{
					rc
				}));
			case OCI.RETURNCODE.OCI_ERROR:
				break;
			case OCI.RETURNCODE.OCI_SUCCESS:
				goto IL_83;
			case OCI.RETURNCODE.OCI_SUCCESS_WITH_INFO:
			{
				OracleException exception = OracleException.CreateException(errorHandle, rc);
				OracleInfoMessageEventArgs infoMessageEvent = new OracleInfoMessageEventArgs(exception);
				this.OnInfoMessage(infoMessageEvent);
				return;
			}
			default:
				if (returncode != OCI.RETURNCODE.OCI_NO_DATA)
				{
					goto IL_83;
				}
				break;
			}
			Exception ex = ADP.OracleError(errorHandle, rc);
			if (errorHandle != null && errorHandle.ConnectionIsBroken)
			{
				OracleInternalConnection openInternalConnection = this.GetOpenInternalConnection();
				if (openInternalConnection != null)
				{
					openInternalConnection.ConnectionIsBroken();
				}
			}
			throw ex;
			IL_83:
			if (rc < 0 || rc == 99)
			{
				throw ADP.Simple(Res.GetString("ADP_UnexpectedReturnCode", new object[]
				{
					rc.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00062874 File Offset: 0x00061C74
		public static void ClearAllPools()
		{
			new OraclePermission(PermissionState.Unrestricted).Demand();
			OracleConnectionFactory.SingletonInstance.ClearAllPools();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000628A4 File Offset: 0x00061CA4
		public static void ClearPool(OracleConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionOptions userConnectionOptions = connection.UserConnectionOptions;
			if (userConnectionOptions != null)
			{
				userConnectionOptions.DemandPermission();
				OracleConnectionFactory.SingletonInstance.ClearPool(connection);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000628E4 File Offset: 0x00061CE4
		object ICloneable.Clone()
		{
			OracleConnection oracleConnection = new OracleConnection(this);
			Bid.Trace("<ora.OracleConnection.Clone|API> %d#, clone=%d#\n", this.ObjectID, oracleConnection.ObjectID);
			return oracleConnection;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00062914 File Offset: 0x00061D14
		public override void Close()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00062934 File Offset: 0x00061D34
		internal void Commit()
		{
			this.GetOpenInternalConnection().Commit();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00062954 File Offset: 0x00061D54
		public new OracleCommand CreateCommand()
		{
			return new OracleCommand
			{
				Connection = this
			};
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00062974 File Offset: 0x00061D74
		private void DisposeMe(bool disposing)
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00062984 File Offset: 0x00061D84
		public void EnlistDistributedTransaction(ITransaction distributedTransaction)
		{
			this.EnlistDistributedTransactionHelper(distributedTransaction);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000629A4 File Offset: 0x00061DA4
		internal byte[] GetBytes(string value, bool useNationalCharacterSet)
		{
			return this.GetOpenInternalConnection().GetBytes(value, useNationalCharacterSet);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000629C4 File Offset: 0x00061DC4
		internal OracleInternalConnection GetOpenInternalConnection()
		{
			DbConnectionInternal innerConnection = this.InnerConnection;
			if (innerConnection is OracleInternalConnection)
			{
				return innerConnection as OracleInternalConnection;
			}
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000629F4 File Offset: 0x00061DF4
		internal NativeBuffer GetScratchBuffer(int minSize)
		{
			return this.GetOpenInternalConnection().GetScratchBuffer(minSize);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00062A14 File Offset: 0x00061E14
		internal string GetString(byte[] bytearray)
		{
			return this.GetOpenInternalConnection().GetString(bytearray);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00062A34 File Offset: 0x00061E34
		internal string GetString(byte[] bytearray, bool useNationalCharacterSet)
		{
			return this.GetOpenInternalConnection().GetString(bytearray, useNationalCharacterSet);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00062A54 File Offset: 0x00061E54
		public override void Open()
		{
			this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			OracleInternalConnection oracleInternalConnection = this.InnerConnection as OracleInternalConnection;
			if (oracleInternalConnection != null)
			{
				oracleInternalConnection.FireDeferredInfoMessageEvents(this);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00062A94 File Offset: 0x00061E94
		internal void Rollback()
		{
			OracleInternalConnection oracleInternalConnection = this.InnerConnection as OracleInternalConnection;
			if (oracleInternalConnection != null)
			{
				oracleInternalConnection.Rollback();
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00062AC4 File Offset: 0x00061EC4
		internal void RollbackDeadTransaction()
		{
			this.GetOpenInternalConnection().RollbackDeadTransaction();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600033B RID: 827 RVA: 0x00062AE4 File Offset: 0x00061EE4
		// (remove) Token: 0x0600033C RID: 828 RVA: 0x00062B04 File Offset: 0x00061F04
		[ResCategory("OracleCategory_InfoMessage")]
		[ResDescription("OracleConnection_InfoMessage")]
		public event OracleInfoMessageEventHandler InfoMessage
		{
			add
			{
				base.Events.AddHandler(OracleConnection.EventInfoMessage, value);
			}
			remove
			{
				base.Events.RemoveHandler(OracleConnection.EventInfoMessage, value);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00062B24 File Offset: 0x00061F24
		internal void OnInfoMessage(OracleInfoMessageEventArgs infoMessageEvent)
		{
			OracleInfoMessageEventHandler oracleInfoMessageEventHandler = (OracleInfoMessageEventHandler)base.Events[OracleConnection.EventInfoMessage];
			if (oracleInfoMessageEventHandler != null)
			{
				oracleInfoMessageEventHandler(this, infoMessageEvent);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00062B54 File Offset: 0x00061F54
		public OracleConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00062B94 File Offset: 0x00061F94
		private void CopyFrom(OracleConnection connection)
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00062BF4 File Offset: 0x00061FF4
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00062C14 File Offset: 0x00062014
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OracleConnection._connectionFactory;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00062C34 File Offset: 0x00062034
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

		// Token: 0x06000343 RID: 835 RVA: 0x00062C54 File Offset: 0x00062054
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

		// Token: 0x06000344 RID: 836 RVA: 0x00062C94 File Offset: 0x00062094
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00062D34 File Offset: 0x00062134
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00062D54 File Offset: 0x00062154
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00062D74 File Offset: 0x00062174
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00062D94 File Offset: 0x00062194
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DbConnection_State")]
		public override ConnectionState State
		{
			get
			{
				return this.InnerConnection.State;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00062DB4 File Offset: 0x000621B4
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00062DD4 File Offset: 0x000621D4
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

		// Token: 0x0600034B RID: 843 RVA: 0x00062E44 File Offset: 0x00062244
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00062E64 File Offset: 0x00062264
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

		// Token: 0x0600034D RID: 845 RVA: 0x00062EC4 File Offset: 0x000622C4
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

		// Token: 0x0600034E RID: 846 RVA: 0x00062F34 File Offset: 0x00062334
		private static CodeAccessPermission CreateExecutePermission()
		{
			OraclePermission oraclePermission = new OraclePermission(PermissionState.None);
			oraclePermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return oraclePermission;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00062F64 File Offset: 0x00062364
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

		// Token: 0x06000350 RID: 848 RVA: 0x00062FA4 File Offset: 0x000623A4
		private void EnlistDistributedTransactionHelper(ITransaction transaction)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(OracleConnection.ExecutePermission);
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

		// Token: 0x06000351 RID: 849 RVA: 0x00063014 File Offset: 0x00062414
		public override void EnlistTransaction(Transaction transaction)
		{
			OracleConnection.ExecutePermission.Demand();
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

		// Token: 0x06000352 RID: 850 RVA: 0x00063074 File Offset: 0x00062474
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00063094 File Offset: 0x00062494
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000630B4 File Offset: 0x000624B4
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000630D4 File Offset: 0x000624D4
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000630F4 File Offset: 0x000624F4
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			OracleConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00063134 File Offset: 0x00062534
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00063154 File Offset: 0x00062554
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

		// Token: 0x06000359 RID: 857 RVA: 0x00063194 File Offset: 0x00062594
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000631B4 File Offset: 0x000625B4
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

		// Token: 0x0600035B RID: 859 RVA: 0x00063234 File Offset: 0x00062634
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00063254 File Offset: 0x00062654
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00063274 File Offset: 0x00062674
		[Conditional("DEBUG")]
		internal static void VerifyExecutePermission()
		{
			try
			{
				OracleConnection.ExecutePermission.Demand();
			}
			catch (SecurityException)
			{
				throw;
			}
		}

		// Token: 0x0400038A RID: 906
		private static readonly object EventInfoMessage = new object();

		// Token: 0x0400038B RID: 907
		private static readonly DbConnectionFactory _connectionFactory = OracleConnectionFactory.SingletonInstance;

		// Token: 0x0400038C RID: 908
		internal static readonly CodeAccessPermission ExecutePermission = OracleConnection.CreateExecutePermission();

		// Token: 0x0400038D RID: 909
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x0400038E RID: 910
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x0400038F RID: 911
		private DbConnectionInternal _innerConnection;

		// Token: 0x04000390 RID: 912
		private int _closeCount;

		// Token: 0x04000391 RID: 913
		private static int _objectTypeCount;

		// Token: 0x04000392 RID: 914
		internal readonly int ObjectID = Interlocked.Increment(ref OracleConnection._objectTypeCount);
	}
}
