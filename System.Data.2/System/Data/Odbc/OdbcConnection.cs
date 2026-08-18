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
using System.Text;
using System.Threading;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x02000290 RID: 656
	[DefaultEvent("InfoMessage")]
	public sealed class OdbcConnection : DbConnection, ICloneable
	{
		// Token: 0x06002796 RID: 10134 RVA: 0x0010AF1C File Offset: 0x0010A31C
		public OdbcConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0010AF38 File Offset: 0x0010A338
		private OdbcConnection(OdbcConnection connection) : this()
		{
			this.CopyFrom(connection);
			this.connectionTimeout = connection.connectionTimeout;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x0010AF60 File Offset: 0x0010A360
		// (set) Token: 0x06002799 RID: 10137 RVA: 0x0010AF74 File Offset: 0x0010A374
		internal OdbcConnectionHandle ConnectionHandle
		{
			get
			{
				return this._connectionHandle;
			}
			set
			{
				this._connectionHandle = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x0010AF88 File Offset: 0x0010A388
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x0010AF9C File Offset: 0x0010A39C
		[Editor("Microsoft.VSDesigner.Data.Odbc.Design.OdbcConnectionStringEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("OdbcConnection_ConnectionString")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x0010AFB0 File Offset: 0x0010A3B0
		// (set) Token: 0x0600279D RID: 10141 RVA: 0x0010AFC4 File Offset: 0x0010A3C4
		[DefaultValue(15)]
		[ResDescription("OdbcConnection_ConnectionTimeout")]
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ConnectionTimeout
		{
			get
			{
				return this.connectionTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ODBC.NegativeArgument();
				}
				if (this.IsOpen)
				{
					throw ODBC.CantSetPropertyOnOpenConnection();
				}
				this.connectionTimeout = value;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x0010AFF0 File Offset: 0x0010A3F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("OdbcConnection_Database")]
		public override string Database
		{
			get
			{
				if (this.IsOpen && !this.ProviderInfo.NoCurrentCatalog)
				{
					return this.GetConnectAttrString(ODBC32.SQL_ATTR.CURRENT_CATALOG);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x0010B020 File Offset: 0x0010A420
		[ResDescription("OdbcConnection_DataSource")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DataSource
		{
			get
			{
				if (this.IsOpen)
				{
					return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SERVER_NAME, true);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x0010B044 File Offset: 0x0010A444
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ResDescription("OdbcConnection_ServerVersion")]
		public override string ServerVersion
		{
			get
			{
				return this.InnerConnection.ServerVersion;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x0010B05C File Offset: 0x0010A45C
		[ResDescription("DbConnection_State")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ConnectionState State
		{
			get
			{
				return this.InnerConnection.State;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x0010B074 File Offset: 0x0010A474
		internal OdbcConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return (OdbcConnectionPoolGroupProviderInfo)this.PoolGroup.ProviderInfo;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x0010B094 File Offset: 0x0010A494
		internal ConnectionState InternalState
		{
			get
			{
				return this.State | this._extraState;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x0010B0B0 File Offset: 0x0010A4B0
		internal bool IsOpen
		{
			get
			{
				return this.InnerConnection is OdbcConnectionOpen;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x0010B0CC File Offset: 0x0010A4CC
		// (set) Token: 0x060027A6 RID: 10150 RVA: 0x0010B0F8 File Offset: 0x0010A4F8
		internal OdbcTransaction LocalTransaction
		{
			get
			{
				OdbcTransaction result = null;
				if (this.weakTransaction != null)
				{
					result = (OdbcTransaction)this.weakTransaction.Target;
				}
				return result;
			}
			set
			{
				this.weakTransaction = null;
				if (value != null)
				{
					this.weakTransaction = new WeakReference(value);
				}
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x0010B11C File Offset: 0x0010A51C
		[Browsable(false)]
		[ResDescription("OdbcConnection_Driver")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Driver
		{
			get
			{
				if (this.IsOpen)
				{
					if (this.ProviderInfo.DriverName == null)
					{
						this.ProviderInfo.DriverName = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_NAME);
					}
					return this.ProviderInfo.DriverName;
				}
				return ADP.StrEmpty;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x0010B164 File Offset: 0x0010A564
		internal bool IsV3Driver
		{
			get
			{
				if (this.ProviderInfo.DriverVersion == null)
				{
					this.ProviderInfo.DriverVersion = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_ODBC_VER);
					if (this.ProviderInfo.DriverVersion != null && this.ProviderInfo.DriverVersion.Length >= 2)
					{
						try
						{
							this.ProviderInfo.IsV3Driver = (int.Parse(this.ProviderInfo.DriverVersion.Substring(0, 2), CultureInfo.InvariantCulture) >= 3);
							goto IL_97;
						}
						catch (FormatException e)
						{
							this.ProviderInfo.IsV3Driver = false;
							ADP.TraceExceptionWithoutRethrow(e);
							goto IL_97;
						}
					}
					this.ProviderInfo.DriverVersion = "";
				}
				IL_97:
				return this.ProviderInfo.IsV3Driver;
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060027A9 RID: 10153 RVA: 0x0010B230 File Offset: 0x0010A630
		// (remove) Token: 0x060027AA RID: 10154 RVA: 0x0010B254 File Offset: 0x0010A654
		[ResDescription("DbConnection_InfoMessage")]
		[ResCategory("DataCategory_InfoMessage")]
		public event OdbcInfoMessageEventHandler InfoMessage
		{
			add
			{
				this.infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Combine(this.infoMessageEventHandler, value);
			}
			remove
			{
				this.infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Remove(this.infoMessageEventHandler, value);
			}
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x0010B278 File Offset: 0x0010A678
		internal char EscapeChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasEscapeChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SEARCH_PATTERN_ESCAPE);
				this.ProviderInfo.EscapeChar = ((infoStringUnhandled.Length == 1) ? infoStringUnhandled[0] : this.QuoteChar(method)[0]);
			}
			return this.ProviderInfo.EscapeChar;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x0010B2D8 File Offset: 0x0010A6D8
		internal string QuoteChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasQuoteChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.IDENTIFIER_QUOTE_CHAR);
				this.ProviderInfo.QuoteChar = ((1 == infoStringUnhandled.Length) ? infoStringUnhandled : "\0");
			}
			return this.ProviderInfo.QuoteChar;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0010B32C File Offset: 0x0010A72C
		public new OdbcTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x0010B340 File Offset: 0x0010A740
		public new OdbcTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return (OdbcTransaction)this.InnerConnection.BeginTransaction(isolevel);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0010B360 File Offset: 0x0010A760
		private void RollbackDeadTransaction()
		{
			WeakReference weakReference = this.weakTransaction;
			if (weakReference != null && !weakReference.IsAlive)
			{
				this.weakTransaction = null;
				this.ConnectionHandle.CompleteTransaction(1);
			}
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x0010B394 File Offset: 0x0010A794
		public override void ChangeDatabase(string value)
		{
			this.InnerConnection.ChangeDatabase(value);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0010B3B0 File Offset: 0x0010A7B0
		internal void CheckState(string method)
		{
			ConnectionState internalState = this.InternalState;
			if (ConnectionState.Open != internalState)
			{
				throw ADP.OpenConnectionRequired(method, internalState);
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0010B3D0 File Offset: 0x0010A7D0
		object ICloneable.Clone()
		{
			OdbcConnection odbcConnection = new OdbcConnection(this);
			Bid.Trace("<odbc.OdbcConnection.Clone|API> %d#, clone=%d#\n", this.ObjectID, odbcConnection.ObjectID);
			return odbcConnection;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0010B3FC File Offset: 0x0010A7FC
		internal bool ConnectionIsAlive(Exception innerException)
		{
			if (this.IsOpen)
			{
				if (!this.ProviderInfo.NoConnectionDead)
				{
					int connectAttr = this.GetConnectAttr(ODBC32.SQL_ATTR.CONNECTION_DEAD, ODBC32.HANDLER.IGNORE);
					if (1 == connectAttr)
					{
						this.Close();
						throw ADP.ConnectionIsDisabled(innerException);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x0010B440 File Offset: 0x0010A840
		public new OdbcCommand CreateCommand()
		{
			return new OdbcCommand(string.Empty, this);
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x0010B458 File Offset: 0x0010A858
		internal OdbcStatementHandle CreateStatementHandle()
		{
			return new OdbcStatementHandle(this.ConnectionHandle);
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x0010B470 File Offset: 0x0010A870
		public override void Close()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
			OdbcConnectionHandle connectionHandle = this._connectionHandle;
			if (connectionHandle != null)
			{
				this._connectionHandle = null;
				WeakReference weakReference = this.weakTransaction;
				if (weakReference != null)
				{
					this.weakTransaction = null;
					IDisposable disposable = weakReference.Target as OdbcTransaction;
					if (disposable != null && weakReference.IsAlive)
					{
						disposable.Dispose();
					}
				}
				connectionHandle.Dispose();
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0010B4D4 File Offset: 0x0010A8D4
		private void DisposeMe(bool disposing)
		{
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0010B4E4 File Offset: 0x0010A8E4
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			this.EnlistDistributedTransactionHelper(transaction);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0010B4F8 File Offset: 0x0010A8F8
		internal string GetConnectAttrString(ODBC32.SQL_ATTR attribute)
		{
			string result = "";
			int num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				if (array.Length + 2 <= num)
				{
					array = new byte[num + 2];
					connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				}
				if (connectionAttribute == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == connectionAttribute)
				{
					result = Encoding.Unicode.GetString(array, 0, Math.Min(num, array.Length));
				}
				else if (connectionAttribute == ODBC32.RetCode.ERROR)
				{
					string diagSqlState = this.GetDiagSqlState();
					if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
					{
						this.FlagUnsupportedConnectAttr(attribute);
					}
				}
			}
			return result;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0010B5AC File Offset: 0x0010A9AC
		internal int GetConnectAttr(ODBC32.SQL_ATTR attribute, ODBC32.HANDLER handler)
		{
			int result = -1;
			int num = 0;
			byte[] array = new byte[4];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				if (connectionAttribute == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == connectionAttribute)
				{
					result = BitConverter.ToInt32(array, 0);
				}
				else
				{
					if (connectionAttribute == ODBC32.RetCode.ERROR)
					{
						string diagSqlState = this.GetDiagSqlState();
						if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
						{
							this.FlagUnsupportedConnectAttr(attribute);
						}
					}
					if (handler == ODBC32.HANDLER.THROW)
					{
						this.HandleError(connectionHandle, connectionAttribute);
					}
				}
			}
			return result;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0010B634 File Offset: 0x0010AA34
		private string GetDiagSqlState()
		{
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			string result;
			connectionHandle.GetDiagnosticField(out result);
			return result;
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0010B654 File Offset: 0x0010AA54
		internal ODBC32.RetCode GetInfoInt16Unhandled(ODBC32.SQL_INFO info, out short resultValue)
		{
			byte[] array = new byte[2];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt16(array, 0);
			return info2;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x0010B680 File Offset: 0x0010AA80
		internal ODBC32.RetCode GetInfoInt32Unhandled(ODBC32.SQL_INFO info, out int resultValue)
		{
			byte[] array = new byte[4];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt32(array, 0);
			return info2;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x0010B6AC File Offset: 0x0010AAAC
		private int GetInfoInt32Unhandled(ODBC32.SQL_INFO infotype)
		{
			byte[] array = new byte[4];
			this.ConnectionHandle.GetInfo1(infotype, array);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0010B6D8 File Offset: 0x0010AAD8
		internal string GetInfoStringUnhandled(ODBC32.SQL_INFO info)
		{
			return this.GetInfoStringUnhandled(info, false);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x0010B6F0 File Offset: 0x0010AAF0
		private string GetInfoStringUnhandled(ODBC32.SQL_INFO info, bool handleError)
		{
			string result = null;
			short num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode info2 = connectionHandle.GetInfo2(info, array, out num);
				if (array.Length < (int)(num - 2))
				{
					array = new byte[(int)(num + 2)];
					info2 = connectionHandle.GetInfo2(info, array, out num);
				}
				if (info2 == ODBC32.RetCode.SUCCESS || info2 == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					result = Encoding.Unicode.GetString(array, 0, Math.Min((int)num, array.Length));
				}
				else if (handleError)
				{
					this.HandleError(this.ConnectionHandle, info2);
				}
			}
			else if (handleError)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x0010B77C File Offset: 0x0010AB7C
		internal Exception HandleErrorNoThrow(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				if (retcode != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					OdbcException ex = OdbcException.CreateException(ODBC32.GetDiagErrors(null, hrHandle, retcode), retcode);
					if (ex != null)
					{
						ex.Errors.SetSource(this.Driver);
					}
					this.ConnectionIsAlive(ex);
					return ex;
				}
				if (this.infoMessageEventHandler != null)
				{
					OdbcErrorCollection diagErrors = ODBC32.GetDiagErrors(null, hrHandle, retcode);
					diagErrors.SetSource(this.Driver);
					this.OnInfoMessage(new OdbcInfoMessageEventArgs(diagErrors));
				}
			}
			return null;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0010B7EC File Offset: 0x0010ABEC
		internal void HandleError(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			Exception ex = this.HandleErrorNoThrow(hrHandle, retcode);
			if (retcode > ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				throw ex;
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0010B808 File Offset: 0x0010AC08
		public override void Open()
		{
			this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			if (ADP.NeedManualEnlistment())
			{
				this.EnlistTransaction(Transaction.Current);
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x0010B83C File Offset: 0x0010AC3C
		private void OnInfoMessage(OdbcInfoMessageEventArgs args)
		{
			if (this.infoMessageEventHandler != null)
			{
				try
				{
					this.infoMessageEventHandler(this, args);
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

		// Token: 0x060027C5 RID: 10181 RVA: 0x0010B890 File Offset: 0x0010AC90
		public static void ReleaseObjectPool()
		{
			new OdbcPermission(PermissionState.Unrestricted).Demand();
			OdbcEnvironment.ReleaseObjectPool();
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x0010B8B0 File Offset: 0x0010ACB0
		internal OdbcTransaction SetStateExecuting(string method, OdbcTransaction transaction)
		{
			if (this.weakTransaction != null)
			{
				OdbcTransaction odbcTransaction = this.weakTransaction.Target as OdbcTransaction;
				if (transaction != odbcTransaction)
				{
					if (transaction == null)
					{
						throw ADP.TransactionRequired(method);
					}
					if (this != transaction.Connection)
					{
						throw ADP.TransactionConnectionMismatch();
					}
					transaction = null;
				}
			}
			else if (transaction != null)
			{
				if (transaction.Connection != null)
				{
					throw ADP.TransactionConnectionMismatch();
				}
				transaction = null;
			}
			ConnectionState internalState = this.InternalState;
			if (ConnectionState.Open != internalState)
			{
				this.NotifyWeakReference(1);
				internalState = this.InternalState;
				if (ConnectionState.Open != internalState)
				{
					if ((ConnectionState.Fetching & internalState) != ConnectionState.Closed)
					{
						throw ADP.OpenReaderExists();
					}
					throw ADP.OpenConnectionRequired(method, internalState);
				}
			}
			return transaction;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x0010B940 File Offset: 0x0010AD40
		internal void SetSupportedType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
			this.ProviderInfo.SupportedSQLTypes |= (int)sql_CVT;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0010B9AC File Offset: 0x0010ADAC
		internal void FlagRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			this.ProviderInfo.RestrictedSQLBindTypes |= (int)sql_CVT;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0010B9DC File Offset: 0x0010ADDC
		internal void FlagUnsupportedConnectAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.CURRENT_CATALOG)
			{
				this.ProviderInfo.NoCurrentCatalog = true;
				return;
			}
			if (Attribute != ODBC32.SQL_ATTR.CONNECTION_DEAD)
			{
				return;
			}
			this.ProviderInfo.NoConnectionDead = true;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0010BA10 File Offset: 0x0010AE10
		internal void FlagUnsupportedStmtAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.QUERY_TIMEOUT)
			{
				this.ProviderInfo.NoQueryTimeout = true;
				return;
			}
			if (Attribute == ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION)
			{
				this.ProviderInfo.NoSqlSoptSSHiddenColumns = true;
				return;
			}
			if (Attribute != (ODBC32.SQL_ATTR)1228)
			{
				return;
			}
			this.ProviderInfo.NoSqlSoptSSNoBrowseTable = true;
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0010BA58 File Offset: 0x0010AE58
		internal void FlagUnsupportedColAttr(ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId)
		{
			if (this.IsV3Driver && v3FieldId == (ODBC32.SQL_DESC)1212)
			{
				this.ProviderInfo.NoSqlCASSColumnKey = true;
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0010BA84 File Offset: 0x0010AE84
		internal bool SQLGetFunctions(ODBC32.SQL_API odbcFunction)
		{
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				short result;
				ODBC32.RetCode functions = connectionHandle.GetFunctions(odbcFunction, out result);
				if (functions != ODBC32.RetCode.SUCCESS)
				{
					this.HandleError(connectionHandle, functions);
				}
				return result != 0;
			}
			throw ODBC.ConnectionClosed();
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0010BAC0 File Offset: 0x0010AEC0
		internal bool TestTypeSupport(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CONVERT infotype;
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				infotype = ODBC32.SQL_CONVERT.LONGVARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				infotype = ODBC32.SQL_CONVERT.VARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				infotype = ODBC32.SQL_CONVERT.CHAR;
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return false;
				}
				infotype = ODBC32.SQL_CONVERT.NUMERIC;
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			if ((this.ProviderInfo.TestedSQLTypes & (int)sql_CVT) == 0)
			{
				int num = this.GetInfoInt32Unhandled((ODBC32.SQL_INFO)infotype);
				num &= (int)sql_CVT;
				this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
				this.ProviderInfo.SupportedSQLTypes |= num;
			}
			return (this.ProviderInfo.SupportedSQLTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0010BB64 File Offset: 0x0010AF64
		internal bool TestRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return false;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			return (this.ProviderInfo.RestrictedSQLBindTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x0010BB98 File Offset: 0x0010AF98
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.OdbcConnection.BeginDbTransaction|API> %d#, isolationLevel=%d{ds.IsolationLevel}", this.ObjectID, (int)isolationLevel);
			DbTransaction result;
			try
			{
				DbTransaction dbTransaction = this.InnerConnection.BeginTransaction(isolationLevel);
				GC.KeepAlive(this);
				result = dbTransaction;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0010BBF4 File Offset: 0x0010AFF4
		internal OdbcTransaction Open_BeginTransaction(IsolationLevel isolevel)
		{
			OdbcConnection.ExecutePermission.Demand();
			this.CheckState("BeginTransaction");
			this.RollbackDeadTransaction();
			if (this.weakTransaction != null && this.weakTransaction.IsAlive)
			{
				throw ADP.ParallelTransactionsNotSupported(this);
			}
			if (isolevel <= IsolationLevel.ReadUncommitted)
			{
				if (isolevel == IsolationLevel.Unspecified)
				{
					goto IL_8C;
				}
				if (isolevel == IsolationLevel.Chaos)
				{
					throw ODBC.NotSupportedIsolationLevel(isolevel);
				}
				if (isolevel == IsolationLevel.ReadUncommitted)
				{
					goto IL_8C;
				}
			}
			else if (isolevel <= IsolationLevel.RepeatableRead)
			{
				if (isolevel == IsolationLevel.ReadCommitted || isolevel == IsolationLevel.RepeatableRead)
				{
					goto IL_8C;
				}
			}
			else if (isolevel == IsolationLevel.Serializable || isolevel == IsolationLevel.Snapshot)
			{
				goto IL_8C;
			}
			throw ADP.InvalidIsolationLevel(isolevel);
			IL_8C:
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.BeginTransaction(ref isolevel);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				this.HandleError(connectionHandle, retCode);
			}
			OdbcTransaction odbcTransaction = new OdbcTransaction(this, isolevel, connectionHandle);
			this.weakTransaction = new WeakReference(odbcTransaction);
			return odbcTransaction;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0010BCC0 File Offset: 0x0010B0C0
		internal void Open_ChangeDatabase(string value)
		{
			OdbcConnection.ExecutePermission.Demand();
			this.CheckState("ChangeDatabase");
			if (value == null || value.Trim().Length == 0)
			{
				throw ADP.EmptyDatabaseName();
			}
			if (1024 < value.Length * 2 + 2)
			{
				throw ADP.DatabaseNameTooLong();
			}
			this.RollbackDeadTransaction();
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.SetConnectionAttribute3(ODBC32.SQL_ATTR.CURRENT_CATALOG, value, checked(value.Length * 2));
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				this.HandleError(connectionHandle, retCode);
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0010BD3C File Offset: 0x0010B13C
		internal void Open_EnlistTransaction(Transaction transaction)
		{
			if (this.weakTransaction != null && this.weakTransaction.IsAlive)
			{
				throw ADP.LocalTransactionPresent();
			}
			IDtcTransaction oletxTransaction = ADP.GetOletxTransaction(transaction);
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode;
			if (oletxTransaction == null)
			{
				retCode = connectionHandle.SetConnectionAttribute2(ODBC32.SQL_ATTR.SQL_COPT_SS_ENLIST_IN_DTC, (IntPtr)0, 1);
			}
			else
			{
				retCode = connectionHandle.SetConnectionAttribute4(ODBC32.SQL_ATTR.SQL_COPT_SS_ENLIST_IN_DTC, oletxTransaction, 1);
			}
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				this.HandleError(connectionHandle, retCode);
			}
			((OdbcConnectionOpen)this.InnerConnection).EnlistedTransaction = transaction;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0010BDB4 File Offset: 0x0010B1B4
		internal string Open_GetServerVersion()
		{
			return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER, true);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x0010BDCC File Offset: 0x0010B1CC
		public OdbcConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0010BE08 File Offset: 0x0010B208
		private void CopyFrom(OdbcConnection connection)
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

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x0010BE5C File Offset: 0x0010B25C
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x0010BE70 File Offset: 0x0010B270
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OdbcConnection._connectionFactory;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x0010BE84 File Offset: 0x0010B284
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

		// Token: 0x060027D9 RID: 10201 RVA: 0x0010BEA4 File Offset: 0x0010B2A4
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

		// Token: 0x060027DA RID: 10202 RVA: 0x0010BEE4 File Offset: 0x0010B2E4
		private void ConnectionString_Set(string value)
		{
			DbConnectionPoolKey key = new DbConnectionPoolKey(value);
			this.ConnectionString_Set(key);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x0010BF00 File Offset: 0x0010B300
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions dbConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref dbConnectionOptions);
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

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x0010BF98 File Offset: 0x0010B398
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x0010BFAC File Offset: 0x0010B3AC
		// (set) Token: 0x060027DE RID: 10206 RVA: 0x0010BFC0 File Offset: 0x0010B3C0
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

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060027DF RID: 10207 RVA: 0x0010BFD4 File Offset: 0x0010B3D4
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0010BFE8 File Offset: 0x0010B3E8
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

		// Token: 0x060027E1 RID: 10209 RVA: 0x0010C054 File Offset: 0x0010B454
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0010C070 File Offset: 0x0010B470
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

		// Token: 0x060027E3 RID: 10211 RVA: 0x0010C0D4 File Offset: 0x0010B4D4
		private static CodeAccessPermission CreateExecutePermission()
		{
			DBDataPermission dbdataPermission = (DBDataPermission)OdbcConnectionFactory.SingletonInstance.ProviderFactory.CreatePermission(PermissionState.None);
			dbdataPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return dbdataPermission;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0010C10C File Offset: 0x0010B50C
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

		// Token: 0x060027E5 RID: 10213 RVA: 0x0010C140 File Offset: 0x0010B540
		private void EnlistDistributedTransactionHelper(ITransaction transaction)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(OdbcConnection.ExecutePermission);
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

		// Token: 0x060027E6 RID: 10214 RVA: 0x0010C1A8 File Offset: 0x0010B5A8
		public override void EnlistTransaction(Transaction transaction)
		{
			OdbcConnection.ExecutePermission.Demand();
			Bid.Trace("<prov.DbConnectionHelper.EnlistTransaction|RES|TRAN> %d#, Connection enlisting in a transaction.\n", this.ObjectID);
			DbConnectionInternal innerConnection = this.InnerConnection;
			Transaction enlistedTransaction = innerConnection.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				if (enlistedTransaction.Equals(transaction))
				{
					return;
				}
				if (enlistedTransaction.TransactionInformation.Status == System.Transactions.TransactionStatus.Active)
				{
					throw ADP.TransactionPresent();
				}
			}
			this.InnerConnection.EnlistTransaction(transaction);
			GC.KeepAlive(this);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0010C218 File Offset: 0x0010B618
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0010C238 File Offset: 0x0010B638
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0010C24C File Offset: 0x0010B64C
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0010C268 File Offset: 0x0010B668
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0010C280 File Offset: 0x0010B680
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			OdbcConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0010C2B4 File Offset: 0x0010B6B4
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0010C2D0 File Offset: 0x0010B6D0
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

		// Token: 0x060027EE RID: 10222 RVA: 0x0010C310 File Offset: 0x0010B710
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0010C32C File Offset: 0x0010B72C
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

		// Token: 0x060027F0 RID: 10224 RVA: 0x0010C3A4 File Offset: 0x0010B7A4
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x0010C3C4 File Offset: 0x0010B7C4
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x0010C3D8 File Offset: 0x0010B7D8
		[Conditional("DEBUG")]
		internal static void VerifyExecutePermission()
		{
			try
			{
				OdbcConnection.ExecutePermission.Demand();
			}
			catch (SecurityException)
			{
				throw;
			}
		}

		// Token: 0x04001A5D RID: 6749
		private int connectionTimeout = 15;

		// Token: 0x04001A5E RID: 6750
		private OdbcInfoMessageEventHandler infoMessageEventHandler;

		// Token: 0x04001A5F RID: 6751
		private WeakReference weakTransaction;

		// Token: 0x04001A60 RID: 6752
		private OdbcConnectionHandle _connectionHandle;

		// Token: 0x04001A61 RID: 6753
		private ConnectionState _extraState;

		// Token: 0x04001A62 RID: 6754
		private static readonly DbConnectionFactory _connectionFactory = OdbcConnectionFactory.SingletonInstance;

		// Token: 0x04001A63 RID: 6755
		internal static readonly CodeAccessPermission ExecutePermission = OdbcConnection.CreateExecutePermission();

		// Token: 0x04001A64 RID: 6756
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04001A65 RID: 6757
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x04001A66 RID: 6758
		private DbConnectionInternal _innerConnection;

		// Token: 0x04001A67 RID: 6759
		private int _closeCount;

		// Token: 0x04001A68 RID: 6760
		private static int _objectTypeCount;

		// Token: 0x04001A69 RID: 6761
		internal readonly int ObjectID = Interlocked.Increment(ref OdbcConnection._objectTypeCount);
	}
}
