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
	// Token: 0x020001D6 RID: 470
	[DefaultEvent("InfoMessage")]
	public sealed class OdbcConnection : DbConnection, ICloneable
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x0025B7C8 File Offset: 0x0025ABC8
		public OdbcConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0025B7E8 File Offset: 0x0025ABE8
		private OdbcConnection(OdbcConnection connection) : this()
		{
			this.CopyFrom(connection);
			this.connectionTimeout = connection.connectionTimeout;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0025B818 File Offset: 0x0025AC18
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x0025B838 File Offset: 0x0025AC38
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

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x0025B858 File Offset: 0x0025AC58
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x0025B878 File Offset: 0x0025AC78
		[Editor("Microsoft.VSDesigner.Data.Odbc.Design.OdbcConnectionStringEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[RecommendedAsConfigurable(true)]
		[DefaultValue("")]
		[ResDescription("OdbcConnection_ConnectionString")]
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

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x0025B898 File Offset: 0x0025AC98
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0025B8B8 File Offset: 0x0025ACB8
		[ResDescription("OdbcConnection_ConnectionTimeout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(15)]
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

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0025B8E8 File Offset: 0x0025ACE8
		[ResDescription("OdbcConnection_Database")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x0025B918 File Offset: 0x0025AD18
		[Browsable(false)]
		[ResDescription("OdbcConnection_DataSource")]
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

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0025B948 File Offset: 0x0025AD48
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

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0025B968 File Offset: 0x0025AD68
		internal OdbcConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return (OdbcConnectionPoolGroupProviderInfo)this.PoolGroup.ProviderInfo;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0025B988 File Offset: 0x0025AD88
		internal ConnectionState InternalState
		{
			get
			{
				return this.State | this._extraState;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0025B9A8 File Offset: 0x0025ADA8
		internal bool IsOpen
		{
			get
			{
				return this.InnerConnection is OdbcConnectionOpen;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0025B9C8 File Offset: 0x0025ADC8
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x0025B9F8 File Offset: 0x0025ADF8
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0025BA28 File Offset: 0x0025AE28
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

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0025BA78 File Offset: 0x0025AE78
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

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060019D9 RID: 6617 RVA: 0x0025BB48 File Offset: 0x0025AF48
		// (remove) Token: 0x060019DA RID: 6618 RVA: 0x0025BB78 File Offset: 0x0025AF78
		[ResCategory("DataCategory_InfoMessage")]
		[ResDescription("DbConnection_InfoMessage")]
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

		// Token: 0x060019DB RID: 6619 RVA: 0x0025BBA8 File Offset: 0x0025AFA8
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

		// Token: 0x060019DC RID: 6620 RVA: 0x0025BC08 File Offset: 0x0025B008
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

		// Token: 0x060019DD RID: 6621 RVA: 0x0025BC68 File Offset: 0x0025B068
		public new OdbcTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0025BC88 File Offset: 0x0025B088
		public new OdbcTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return (OdbcTransaction)this.InnerConnection.BeginTransaction(isolevel);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0025BCA8 File Offset: 0x0025B0A8
		private void RollbackDeadTransaction()
		{
			WeakReference weakReference = this.weakTransaction;
			if (weakReference != null && !weakReference.IsAlive)
			{
				this.weakTransaction = null;
				this.ConnectionHandle.CompleteTransaction(1);
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0025BCE8 File Offset: 0x0025B0E8
		public override void ChangeDatabase(string value)
		{
			this.InnerConnection.ChangeDatabase(value);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0025BD08 File Offset: 0x0025B108
		internal void CheckState(string method)
		{
			ConnectionState internalState = this.InternalState;
			if (ConnectionState.Open != internalState)
			{
				throw ADP.OpenConnectionRequired(method, internalState);
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0025BD28 File Offset: 0x0025B128
		object ICloneable.Clone()
		{
			OdbcConnection odbcConnection = new OdbcConnection(this);
			Bid.Trace("<odbc.OdbcConnection.Clone|API> %d#, clone=%d#\n", this.ObjectID, odbcConnection.ObjectID);
			return odbcConnection;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0025BD58 File Offset: 0x0025B158
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

		// Token: 0x060019E4 RID: 6628 RVA: 0x0025BDA8 File Offset: 0x0025B1A8
		public new OdbcCommand CreateCommand()
		{
			return new OdbcCommand(string.Empty, this);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0025BDC8 File Offset: 0x0025B1C8
		internal OdbcStatementHandle CreateStatementHandle()
		{
			return new OdbcStatementHandle(this.ConnectionHandle);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0025BDE8 File Offset: 0x0025B1E8
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

		// Token: 0x060019E7 RID: 6631 RVA: 0x0025BE58 File Offset: 0x0025B258
		private void DisposeMe(bool disposing)
		{
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0025BE68 File Offset: 0x0025B268
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			this.EnlistDistributedTransactionHelper(transaction);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0025BE88 File Offset: 0x0025B288
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

		// Token: 0x060019EA RID: 6634 RVA: 0x0025BF48 File Offset: 0x0025B348
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

		// Token: 0x060019EB RID: 6635 RVA: 0x0025BFD8 File Offset: 0x0025B3D8
		private string GetDiagSqlState()
		{
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			string result;
			connectionHandle.GetDiagnosticField(out result);
			return result;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0025BFF8 File Offset: 0x0025B3F8
		internal ODBC32.RetCode GetInfoInt16Unhandled(ODBC32.SQL_INFO info, out short resultValue)
		{
			byte[] array = new byte[2];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt16(array, 0);
			return info2;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0025C028 File Offset: 0x0025B428
		internal ODBC32.RetCode GetInfoInt32Unhandled(ODBC32.SQL_INFO info, out int resultValue)
		{
			byte[] array = new byte[4];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt32(array, 0);
			return info2;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0025C058 File Offset: 0x0025B458
		private int GetInfoInt32Unhandled(ODBC32.SQL_INFO infotype)
		{
			byte[] array = new byte[4];
			this.ConnectionHandle.GetInfo1(infotype, array);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0025C088 File Offset: 0x0025B488
		internal string GetInfoStringUnhandled(ODBC32.SQL_INFO info)
		{
			return this.GetInfoStringUnhandled(info, false);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0025C0A8 File Offset: 0x0025B4A8
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

		// Token: 0x060019F1 RID: 6641 RVA: 0x0025C138 File Offset: 0x0025B538
		internal Exception HandleErrorNoThrow(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			switch (retcode)
			{
			case ODBC32.RetCode.SUCCESS:
				break;
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				if (this.infoMessageEventHandler != null)
				{
					OdbcErrorCollection diagErrors = ODBC32.GetDiagErrors(null, hrHandle, retcode);
					diagErrors.SetSource(this.Driver);
					this.OnInfoMessage(new OdbcInfoMessageEventArgs(diagErrors));
				}
				break;
			default:
			{
				OdbcException ex = OdbcException.CreateException(ODBC32.GetDiagErrors(null, hrHandle, retcode), retcode);
				if (ex != null)
				{
					ex.Errors.SetSource(this.Driver);
				}
				this.ConnectionIsAlive(ex);
				return ex;
			}
			}
			return null;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0025C1B8 File Offset: 0x0025B5B8
		internal void HandleError(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			Exception ex = this.HandleErrorNoThrow(hrHandle, retcode);
			switch (retcode)
			{
			case ODBC32.RetCode.SUCCESS:
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				return;
			default:
				throw ex;
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0025C1E8 File Offset: 0x0025B5E8
		public override void Open()
		{
			this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			if (ADP.NeedManualEnlistment())
			{
				this.EnlistTransaction(Transaction.Current);
			}
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0025C228 File Offset: 0x0025B628
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

		// Token: 0x060019F5 RID: 6645 RVA: 0x0025C288 File Offset: 0x0025B688
		public static void ReleaseObjectPool()
		{
			new OdbcPermission(PermissionState.Unrestricted).Demand();
			OdbcEnvironment.ReleaseObjectPool();
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0025C2A8 File Offset: 0x0025B6A8
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

		// Token: 0x060019F7 RID: 6647 RVA: 0x0025C338 File Offset: 0x0025B738
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

		// Token: 0x060019F8 RID: 6648 RVA: 0x0025C3A8 File Offset: 0x0025B7A8
		internal void FlagRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.NUMERIC:
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			case ODBC32.SQL_TYPE.DECIMAL:
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
				break;
			default:
				return;
			}
			this.ProviderInfo.RestrictedSQLBindTypes |= (int)sql_CVT;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0025C3E8 File Offset: 0x0025B7E8
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

		// Token: 0x060019FA RID: 6650 RVA: 0x0025C428 File Offset: 0x0025B828
		internal void FlagUnsupportedStmtAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.QUERY_TIMEOUT)
			{
				this.ProviderInfo.NoQueryTimeout = true;
				return;
			}
			switch (Attribute)
			{
			case (ODBC32.SQL_ATTR)1227:
				this.ProviderInfo.NoSqlSoptSSHiddenColumns = true;
				return;
			case (ODBC32.SQL_ATTR)1228:
				this.ProviderInfo.NoSqlSoptSSNoBrowseTable = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0025C478 File Offset: 0x0025B878
		internal void FlagUnsupportedColAttr(ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId)
		{
			if (this.IsV3Driver)
			{
				if (v3FieldId != (ODBC32.SQL_DESC)1212)
				{
					return;
				}
				this.ProviderInfo.NoSqlCASSColumnKey = true;
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0025C4A8 File Offset: 0x0025B8A8
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
			throw ADP.InvalidOperation("what is the right exception to throw here?");
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0025C4E8 File Offset: 0x0025B8E8
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
			return 0 != (this.ProviderInfo.SupportedSQLTypes & (int)sql_CVT);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0025C598 File Offset: 0x0025B998
		internal bool TestRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.NUMERIC:
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			case ODBC32.SQL_TYPE.DECIMAL:
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
				break;
			default:
				return false;
			}
			return 0 != (this.ProviderInfo.RestrictedSQLBindTypes & (int)sql_CVT);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0025C5D8 File Offset: 0x0025B9D8
		internal OdbcTransaction Open_BeginTransaction(IsolationLevel isolevel)
		{
			OdbcConnection.ExecutePermission.Demand();
			this.CheckState("BeginTransaction");
			this.RollbackDeadTransaction();
			if (this.weakTransaction != null && this.weakTransaction.IsAlive)
			{
				throw ADP.ParallelTransactionsNotSupported(this);
			}
			IsolationLevel isolationLevel = isolevel;
			if (isolationLevel <= IsolationLevel.ReadUncommitted)
			{
				if (isolationLevel == IsolationLevel.Unspecified)
				{
					goto IL_8E;
				}
				if (isolationLevel == IsolationLevel.Chaos)
				{
					throw ODBC.NotSupportedIsolationLevel(isolevel);
				}
				if (isolationLevel == IsolationLevel.ReadUncommitted)
				{
					goto IL_8E;
				}
			}
			else if (isolationLevel <= IsolationLevel.RepeatableRead)
			{
				if (isolationLevel == IsolationLevel.ReadCommitted || isolationLevel == IsolationLevel.RepeatableRead)
				{
					goto IL_8E;
				}
			}
			else if (isolationLevel == IsolationLevel.Serializable || isolationLevel == IsolationLevel.Snapshot)
			{
				goto IL_8E;
			}
			throw ADP.InvalidIsolationLevel(isolevel);
			IL_8E:
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

		// Token: 0x06001A00 RID: 6656 RVA: 0x0025C6A8 File Offset: 0x0025BAA8
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

		// Token: 0x06001A01 RID: 6657 RVA: 0x0025C728 File Offset: 0x0025BB28
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

		// Token: 0x06001A02 RID: 6658 RVA: 0x0025C7A8 File Offset: 0x0025BBA8
		internal string Open_GetServerVersion()
		{
			return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER, true);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0025C7C8 File Offset: 0x0025BBC8
		public OdbcConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0025C808 File Offset: 0x0025BC08
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

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0025C868 File Offset: 0x0025BC68
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0025C888 File Offset: 0x0025BC88
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OdbcConnection._connectionFactory;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0025C8A8 File Offset: 0x0025BCA8
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

		// Token: 0x06001A08 RID: 6664 RVA: 0x0025C8C8 File Offset: 0x0025BCC8
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

		// Token: 0x06001A09 RID: 6665 RVA: 0x0025C908 File Offset: 0x0025BD08
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0025C9A8 File Offset: 0x0025BDA8
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0025C9C8 File Offset: 0x0025BDC8
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x0025C9E8 File Offset: 0x0025BDE8
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

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0025CA08 File Offset: 0x0025BE08
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0025CA28 File Offset: 0x0025BE28
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0025CA48 File Offset: 0x0025BE48
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

		// Token: 0x06001A10 RID: 6672 RVA: 0x0025CAB8 File Offset: 0x0025BEB8
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0025CAD8 File Offset: 0x0025BED8
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

		// Token: 0x06001A12 RID: 6674 RVA: 0x0025CB38 File Offset: 0x0025BF38
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

		// Token: 0x06001A13 RID: 6675 RVA: 0x0025CBA8 File Offset: 0x0025BFA8
		private static CodeAccessPermission CreateExecutePermission()
		{
			DBDataPermission dbdataPermission = (DBDataPermission)OdbcConnectionFactory.SingletonInstance.ProviderFactory.CreatePermission(PermissionState.None);
			dbdataPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return dbdataPermission;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0025CBE8 File Offset: 0x0025BFE8
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

		// Token: 0x06001A15 RID: 6677 RVA: 0x0025CC28 File Offset: 0x0025C028
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

		// Token: 0x06001A16 RID: 6678 RVA: 0x0025CC98 File Offset: 0x0025C098
		public override void EnlistTransaction(Transaction transaction)
		{
			OdbcConnection.ExecutePermission.Demand();
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

		// Token: 0x06001A17 RID: 6679 RVA: 0x0025CCF8 File Offset: 0x0025C0F8
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0025CD18 File Offset: 0x0025C118
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0025CD38 File Offset: 0x0025C138
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0025CD58 File Offset: 0x0025C158
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0025CD78 File Offset: 0x0025C178
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			OdbcConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0025CDB8 File Offset: 0x0025C1B8
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0025CDD8 File Offset: 0x0025C1D8
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

		// Token: 0x06001A1E RID: 6686 RVA: 0x0025CE18 File Offset: 0x0025C218
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0025CE38 File Offset: 0x0025C238
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

		// Token: 0x06001A20 RID: 6688 RVA: 0x0025CEB8 File Offset: 0x0025C2B8
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0025CED8 File Offset: 0x0025C2D8
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0025CEF8 File Offset: 0x0025C2F8
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

		// Token: 0x04000F92 RID: 3986
		private int connectionTimeout = 15;

		// Token: 0x04000F93 RID: 3987
		private OdbcInfoMessageEventHandler infoMessageEventHandler;

		// Token: 0x04000F94 RID: 3988
		private WeakReference weakTransaction;

		// Token: 0x04000F95 RID: 3989
		private OdbcConnectionHandle _connectionHandle;

		// Token: 0x04000F96 RID: 3990
		private ConnectionState _extraState;

		// Token: 0x04000F97 RID: 3991
		private static readonly DbConnectionFactory _connectionFactory = OdbcConnectionFactory.SingletonInstance;

		// Token: 0x04000F98 RID: 3992
		internal static readonly CodeAccessPermission ExecutePermission = OdbcConnection.CreateExecutePermission();

		// Token: 0x04000F99 RID: 3993
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04000F9A RID: 3994
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x04000F9B RID: 3995
		private DbConnectionInternal _innerConnection;

		// Token: 0x04000F9C RID: 3996
		private int _closeCount;

		// Token: 0x04000F9D RID: 3997
		private static int _objectTypeCount;

		// Token: 0x04000F9E RID: 3998
		internal readonly int ObjectID = Interlocked.Increment(ref OdbcConnection._objectTypeCount);
	}
}
