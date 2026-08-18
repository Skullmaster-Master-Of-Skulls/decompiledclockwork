using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Transactions;

namespace System.Data.OleDb
{
	// Token: 0x02000243 RID: 579
	[DefaultEvent("InfoMessage")]
	public sealed class OleDbConnection : DbConnection, ICloneable, IDbConnection, IDisposable
	{
		// Token: 0x06002444 RID: 9284 RVA: 0x000F8B4C File Offset: 0x000F7F4C
		public OleDbConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000F8B68 File Offset: 0x000F7F68
		private OleDbConnection(OleDbConnection connection) : this()
		{
			this.CopyFrom(connection);
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x000F8B84 File Offset: 0x000F7F84
		// (set) Token: 0x06002447 RID: 9287 RVA: 0x000F8B98 File Offset: 0x000F7F98
		[RecommendedAsConfigurable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[Editor("Microsoft.VSDesigner.Data.ADO.Design.OleDbConnectionStringEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("OleDbConnection_ConnectionString")]
		[ResCategory("DataCategory_Data")]
		[SettingsBindable(true)]
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

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x000F8BAC File Offset: 0x000F7FAC
		private OleDbConnectionString OleDbConnectionStringValue
		{
			get
			{
				return (OleDbConnectionString)this.ConnectionOptions;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000F8BC4 File Offset: 0x000F7FC4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("OleDbConnection_ConnectionTimeout")]
		public override int ConnectionTimeout
		{
			get
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnection.get_ConnectionTimeout|API> %d#\n", this.ObjectID);
				int result;
				try
				{
					object obj;
					if (this.IsOpen)
					{
						obj = this.GetDataSourceValue(OleDbPropertySetGuid.DBInit, 66);
					}
					else
					{
						OleDbConnectionString oleDbConnectionStringValue = this.OleDbConnectionStringValue;
						obj = ((oleDbConnectionStringValue != null) ? oleDbConnectionStringValue.ConnectTimeout : 15);
					}
					if (obj != null)
					{
						result = Convert.ToInt32(obj, CultureInfo.InvariantCulture);
					}
					else
					{
						result = 15;
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
				return result;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x000F8C54 File Offset: 0x000F8054
		[ResDescription("OleDbConnection_Database")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnection.get_Database|API> %d#\n", this.ObjectID);
				string result;
				try
				{
					OleDbConnectionString oleDbConnectionString = (OleDbConnectionString)this.UserConnectionOptions;
					object obj = (oleDbConnectionString != null) ? oleDbConnectionString.InitialCatalog : ADP.StrEmpty;
					if (obj != null && !((string)obj).StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
					{
						OleDbConnectionInternal openConnection = this.GetOpenConnection();
						if (openConnection != null)
						{
							if (openConnection.HasSession)
							{
								obj = this.GetDataSourceValue(OleDbPropertySetGuid.DataSource, 37);
							}
							else
							{
								obj = this.GetDataSourceValue(OleDbPropertySetGuid.DBInit, 233);
							}
						}
						else
						{
							oleDbConnectionString = this.OleDbConnectionStringValue;
							obj = ((oleDbConnectionString != null) ? oleDbConnectionString.InitialCatalog : ADP.StrEmpty);
						}
					}
					result = Convert.ToString(obj, CultureInfo.InvariantCulture);
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
				return result;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x000F8D28 File Offset: 0x000F8128
		[Browsable(true)]
		[ResDescription("OleDbConnection_DataSource")]
		public override string DataSource
		{
			get
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnection.get_DataSource|API> %d#\n", this.ObjectID);
				string result;
				try
				{
					OleDbConnectionString oleDbConnectionString = (OleDbConnectionString)this.UserConnectionOptions;
					object obj = (oleDbConnectionString != null) ? oleDbConnectionString.DataSource : ADP.StrEmpty;
					if (obj != null && !((string)obj).StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
					{
						if (this.IsOpen)
						{
							obj = this.GetDataSourceValue(OleDbPropertySetGuid.DBInit, 59);
							if (obj == null || (obj is string && (obj as string).Length == 0))
							{
								obj = this.GetDataSourceValue(OleDbPropertySetGuid.DataSourceInfo, 38);
							}
						}
						else
						{
							oleDbConnectionString = this.OleDbConnectionStringValue;
							obj = ((oleDbConnectionString != null) ? oleDbConnectionString.DataSource : ADP.StrEmpty);
						}
					}
					result = Convert.ToString(obj, CultureInfo.InvariantCulture);
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
				return result;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x000F8E04 File Offset: 0x000F8204
		internal bool IsOpen
		{
			get
			{
				return this.GetOpenConnection() != null;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x000F8E1C File Offset: 0x000F821C
		internal OleDbTransaction LocalTransaction
		{
			set
			{
				OleDbConnectionInternal openConnection = this.GetOpenConnection();
				if (openConnection != null)
				{
					openConnection.LocalTransaction = value;
				}
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x000F8E3C File Offset: 0x000F823C
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("OleDbConnection_Provider")]
		public string Provider
		{
			get
			{
				Bid.Trace("<oledb.OleDbConnection.get_Provider|API> %d#\n", this.ObjectID);
				OleDbConnectionString oleDbConnectionStringValue = this.OleDbConnectionStringValue;
				string text = (oleDbConnectionStringValue != null) ? oleDbConnectionStringValue.ConvertValueToString("provider", null) : null;
				if (text == null)
				{
					return ADP.StrEmpty;
				}
				return text;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x000F8E80 File Offset: 0x000F8280
		internal OleDbConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return (OleDbConnectionPoolGroupProviderInfo)this.PoolGroup.ProviderInfo;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x000F8EA0 File Offset: 0x000F82A0
		[ResDescription("OleDbConnection_ServerVersion")]
		public override string ServerVersion
		{
			get
			{
				return this.InnerConnection.ServerVersion;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000F8EB8 File Offset: 0x000F82B8
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

		// Token: 0x06002452 RID: 9298 RVA: 0x000F8ED0 File Offset: 0x000F82D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void ResetState()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbCommand.ResetState|API> %d#\n", this.ObjectID);
			try
			{
				if (this.IsOpen)
				{
					object dataSourcePropertyValue = this.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 244);
					if (dataSourcePropertyValue is int)
					{
						switch ((int)dataSourcePropertyValue)
						{
						case 0:
						case 2:
							this.GetOpenConnection().DoomThisConnection();
							this.NotifyWeakReference(-1);
							this.Close();
							break;
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06002453 RID: 9299 RVA: 0x000F8F6C File Offset: 0x000F836C
		// (remove) Token: 0x06002454 RID: 9300 RVA: 0x000F8F8C File Offset: 0x000F838C
		[ResDescription("DbConnection_InfoMessage")]
		[ResCategory("DataCategory_InfoMessage")]
		public event OleDbInfoMessageEventHandler InfoMessage
		{
			add
			{
				base.Events.AddHandler(OleDbConnection.EventInfoMessage, value);
			}
			remove
			{
				base.Events.RemoveHandler(OleDbConnection.EventInfoMessage, value);
			}
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000F8FAC File Offset: 0x000F83AC
		internal UnsafeNativeMethods.ICommandText ICommandText()
		{
			return this.GetOpenConnection().ICommandText();
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000F8FC4 File Offset: 0x000F83C4
		private IDBPropertiesWrapper IDBProperties()
		{
			return this.GetOpenConnection().IDBProperties();
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000F8FDC File Offset: 0x000F83DC
		internal IOpenRowsetWrapper IOpenRowset()
		{
			return this.GetOpenConnection().IOpenRowset();
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000F8FF4 File Offset: 0x000F83F4
		internal int SqlSupport()
		{
			return this.OleDbConnectionStringValue.GetSqlSupport(this);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000F9010 File Offset: 0x000F8410
		internal bool SupportMultipleResults()
		{
			return this.OleDbConnectionStringValue.GetSupportMultipleResults(this);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000F902C File Offset: 0x000F842C
		internal bool SupportIRow(OleDbCommand cmd)
		{
			return this.OleDbConnectionStringValue.GetSupportIRow(this, cmd);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000F9048 File Offset: 0x000F8448
		internal int QuotedIdentifierCase()
		{
			object dataSourcePropertyValue = this.GetDataSourcePropertyValue(OleDbPropertySetGuid.DataSourceInfo, 100);
			int result;
			if (dataSourcePropertyValue is int)
			{
				result = (int)dataSourcePropertyValue;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000F9078 File Offset: 0x000F8478
		public new OleDbTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000F908C File Offset: 0x000F848C
		public new OleDbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return (OleDbTransaction)this.InnerConnection.BeginTransaction(isolationLevel);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000F90AC File Offset: 0x000F84AC
		public override void ChangeDatabase(string value)
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnection.ChangeDatabase|API> %d#, value='%ls'\n", this.ObjectID, value);
			try
			{
				this.CheckStateOpen("ChangeDatabase");
				if (value == null || value.Trim().Length == 0)
				{
					throw ADP.EmptyDatabaseName();
				}
				this.SetDataSourcePropertyValue(OleDbPropertySetGuid.DataSource, 37, "current catalog", true, value);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000F9134 File Offset: 0x000F8534
		internal void CheckStateOpen(string method)
		{
			ConnectionState state = this.State;
			if (ConnectionState.Open != state)
			{
				throw ADP.OpenConnectionRequired(method, state);
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000F9154 File Offset: 0x000F8554
		object ICloneable.Clone()
		{
			OleDbConnection oleDbConnection = new OleDbConnection(this);
			Bid.Trace("<oledb.OleDbConnection.Clone|API> %d#, clone=%d#\n", this.ObjectID, oleDbConnection.ObjectID);
			return oleDbConnection;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000F9180 File Offset: 0x000F8580
		public override void Close()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000F91A0 File Offset: 0x000F85A0
		public new OleDbCommand CreateCommand()
		{
			return new OleDbCommand("", this);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000F91B8 File Offset: 0x000F85B8
		private void DisposeMe(bool disposing)
		{
			if (disposing && base.DesignMode)
			{
				OleDbConnection.ReleaseObjectPool();
			}
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000F91D8 File Offset: 0x000F85D8
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.OleDbConnection.BeginDbTransaction|API> %d#, isolationLevel=%d{ds.IsolationLevel}", this.ObjectID, (int)isolationLevel);
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

		// Token: 0x06002465 RID: 9317 RVA: 0x000F9234 File Offset: 0x000F8634
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			this.EnlistDistributedTransactionHelper(transaction);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000F9248 File Offset: 0x000F8648
		internal object GetDataSourcePropertyValue(Guid propertySet, int propertyID)
		{
			OleDbConnectionInternal openConnection = this.GetOpenConnection();
			return openConnection.GetDataSourcePropertyValue(propertySet, propertyID);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000F9264 File Offset: 0x000F8664
		internal object GetDataSourceValue(Guid propertySet, int propertyID)
		{
			object obj = this.GetDataSourcePropertyValue(propertySet, propertyID);
			if (obj is OleDbPropertyStatus || Convert.IsDBNull(obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000F9290 File Offset: 0x000F8690
		private OleDbConnectionInternal GetOpenConnection()
		{
			DbConnectionInternal innerConnection = this.InnerConnection;
			return innerConnection as OleDbConnectionInternal;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000F92AC File Offset: 0x000F86AC
		internal void GetLiteralQuotes(string method, out string quotePrefix, out string quoteSuffix)
		{
			this.CheckStateOpen(method);
			OleDbConnectionPoolGroupProviderInfo providerInfo = this.ProviderInfo;
			if (providerInfo.HasQuoteFix)
			{
				quotePrefix = providerInfo.QuotePrefix;
				quoteSuffix = providerInfo.QuoteSuffix;
				return;
			}
			OleDbConnectionInternal openConnection = this.GetOpenConnection();
			quotePrefix = openConnection.GetLiteralInfo(15);
			quoteSuffix = openConnection.GetLiteralInfo(28);
			if (quotePrefix == null)
			{
				quotePrefix = "";
			}
			if (quoteSuffix == null)
			{
				quoteSuffix = quotePrefix;
			}
			providerInfo.SetQuoteFix(quotePrefix, quoteSuffix);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000F9318 File Offset: 0x000F8718
		public DataTable GetOleDbSchemaTable(Guid schema, object[] restrictions)
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnection.GetOleDbSchemaTable|API> %d#, schema=%ls, restrictions\n", this.ObjectID, schema);
			DataTable result;
			try
			{
				this.CheckStateOpen("GetOleDbSchemaTable");
				OleDbConnectionInternal openConnection = this.GetOpenConnection();
				if (OleDbSchemaGuid.DbInfoLiterals == schema)
				{
					if (restrictions != null && restrictions.Length != 0)
					{
						throw ODB.InvalidRestrictionsDbInfoLiteral("restrictions");
					}
					result = openConnection.BuildInfoLiterals();
				}
				else if (OleDbSchemaGuid.SchemaGuids == schema)
				{
					if (restrictions != null && restrictions.Length != 0)
					{
						throw ODB.InvalidRestrictionsSchemaGuids("restrictions");
					}
					result = openConnection.BuildSchemaGuids();
				}
				else if (OleDbSchemaGuid.DbInfoKeywords == schema)
				{
					if (restrictions != null && restrictions.Length != 0)
					{
						throw ODB.InvalidRestrictionsDbInfoKeywords("restrictions");
					}
					result = openConnection.BuildInfoKeywords();
				}
				else
				{
					if (!openConnection.SupportSchemaRowset(schema))
					{
						using (IDBSchemaRowsetWrapper idbschemaRowsetWrapper = openConnection.IDBSchemaRowset())
						{
							if (idbschemaRowsetWrapper.Value == null)
							{
								throw ODB.SchemaRowsetsNotSupported(this.Provider);
							}
						}
						throw ODB.NotSupportedSchemaTable(schema, this);
					}
					result = openConnection.GetSchemaRowset(schema, restrictions);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000F945C File Offset: 0x000F885C
		internal DataTable GetSchemaRowset(Guid schema, object[] restrictions)
		{
			return this.GetOpenConnection().GetSchemaRowset(schema, restrictions);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000F9478 File Offset: 0x000F8878
		internal bool HasLiveReader(OleDbCommand cmd)
		{
			bool result = false;
			OleDbConnectionInternal openConnection = this.GetOpenConnection();
			if (openConnection != null)
			{
				result = openConnection.HasLiveReader(cmd);
			}
			return result;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000F949C File Offset: 0x000F889C
		internal void OnInfoMessage(UnsafeNativeMethods.IErrorInfo errorInfo, OleDbHResult errorCode)
		{
			OleDbInfoMessageEventHandler oleDbInfoMessageEventHandler = (OleDbInfoMessageEventHandler)base.Events[OleDbConnection.EventInfoMessage];
			if (oleDbInfoMessageEventHandler != null)
			{
				try
				{
					OleDbException exception = OleDbException.CreateException(errorInfo, errorCode, null);
					OleDbInfoMessageEventArgs oleDbInfoMessageEventArgs = new OleDbInfoMessageEventArgs(exception);
					if (Bid.TraceOn)
					{
						Bid.Trace("<oledb.OledbConnection.OnInfoMessage|API|INFO> %d#, Message='%ls'\n", this.ObjectID, oleDbInfoMessageEventArgs.Message);
					}
					oleDbInfoMessageEventHandler(this, oleDbInfoMessageEventArgs);
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

		// Token: 0x0600246E RID: 9326 RVA: 0x000F9528 File Offset: 0x000F8928
		public override void Open()
		{
			this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			if ((2 & ((OleDbConnectionString)this.ConnectionOptions).OleDbServices) != 0 && ADP.NeedManualEnlistment())
			{
				this.GetOpenConnection().EnlistTransactionInternal(Transaction.Current);
			}
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000F9574 File Offset: 0x000F8974
		internal void SetDataSourcePropertyValue(Guid propertySet, int propertyID, string description, bool required, object value)
		{
			this.CheckStateOpen("SetProperties");
			using (IDBPropertiesWrapper idbpropertiesWrapper = this.IDBProperties())
			{
				using (DBPropSet dbpropSet = DBPropSet.CreateProperty(propertySet, propertyID, required, value))
				{
					Bid.Trace("<oledb.IDBProperties.SetProperties|API|OLEDB> %d#\n", this.ObjectID);
					OleDbHResult oleDbHResult = idbpropertiesWrapper.Value.SetProperties(dbpropSet.PropertySetCount, dbpropSet);
					Bid.Trace("<oledb.IDBProperties.SetProperties|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
					if (oleDbHResult < OleDbHResult.S_OK)
					{
						Exception ex = OleDbConnection.ProcessResults(oleDbHResult, null, this);
						if (OleDbHResult.DB_E_ERRORSOCCURRED == oleDbHResult)
						{
							StringBuilder stringBuilder = new StringBuilder();
							tagDBPROP[] propertySet2 = dbpropSet.GetPropertySet(0, out propertySet);
							ODB.PropsetSetFailure(stringBuilder, description, propertySet2[0].dwStatus);
							ex = ODB.PropsetSetFailure(stringBuilder.ToString(), ex);
						}
						if (ex != null)
						{
							throw ex;
						}
					}
					else
					{
						SafeNativeMethods.Wrapper.ClearErrorInfo();
					}
				}
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000F966C File Offset: 0x000F8A6C
		internal bool SupportSchemaRowset(Guid schema)
		{
			return this.GetOpenConnection().SupportSchemaRowset(schema);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000F9688 File Offset: 0x000F8A88
		internal OleDbTransaction ValidateTransaction(OleDbTransaction transaction, string method)
		{
			return this.GetOpenConnection().ValidateTransaction(transaction, method);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000F96A4 File Offset: 0x000F8AA4
		internal static Exception ProcessResults(OleDbHResult hresult, OleDbConnection connection, object src)
		{
			if (OleDbHResult.S_OK <= hresult && (connection == null || connection.Events[OleDbConnection.EventInfoMessage] == null))
			{
				SafeNativeMethods.Wrapper.ClearErrorInfo();
				return null;
			}
			Exception ex = null;
			UnsafeNativeMethods.IErrorInfo errorInfo = null;
			if (UnsafeNativeMethods.GetErrorInfo(0, out errorInfo) == OleDbHResult.S_OK && errorInfo != null)
			{
				if (hresult < OleDbHResult.S_OK)
				{
					ex = OleDbException.CreateException(errorInfo, hresult, null);
					if (OleDbHResult.DB_E_OBJECTOPEN == hresult)
					{
						ex = ADP.OpenReaderExists(ex);
					}
					OleDbConnection.ResetState(connection);
				}
				else if (connection != null)
				{
					connection.OnInfoMessage(errorInfo, hresult);
				}
				else
				{
					Bid.Trace("<oledb.OledbConnection|WARN|INFO> ErrorInfo available, but not connection %08X{HRESULT}\n", hresult);
				}
				Marshal.ReleaseComObject(errorInfo);
			}
			else if (OleDbHResult.S_OK < hresult)
			{
				Bid.Trace("<oledb.OledbConnection|ERR|INFO> ErrorInfo not available %08X{HRESULT}\n", hresult);
			}
			else if (hresult < OleDbHResult.S_OK)
			{
				ex = ODB.NoErrorInformation((connection != null) ? connection.Provider : null, hresult, null);
				OleDbConnection.ResetState(connection);
			}
			if (ex != null)
			{
				ADP.TraceExceptionAsReturnValue(ex);
			}
			return ex;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000F9764 File Offset: 0x000F8B64
		public static void ReleaseObjectPool()
		{
			new OleDbPermission(PermissionState.Unrestricted).Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnection.ReleaseObjectPool|API>\n");
			try
			{
				OleDbConnectionString.ReleaseObjectPool();
				OleDbConnectionInternal.ReleaseObjectPool();
				OleDbConnectionFactory.SingletonInstance.ClearAllPools();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000F97C4 File Offset: 0x000F8BC4
		private static void ResetState(OleDbConnection connection)
		{
			if (connection != null)
			{
				connection.ResetState();
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000F97DC File Offset: 0x000F8BDC
		public OleDbConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000F9810 File Offset: 0x000F8C10
		private void CopyFrom(OleDbConnection connection)
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

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x000F9864 File Offset: 0x000F8C64
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000F9878 File Offset: 0x000F8C78
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OleDbConnection._connectionFactory;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000F988C File Offset: 0x000F8C8C
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

		// Token: 0x0600247A RID: 9338 RVA: 0x000F98AC File Offset: 0x000F8CAC
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

		// Token: 0x0600247B RID: 9339 RVA: 0x000F98EC File Offset: 0x000F8CEC
		private void ConnectionString_Set(string value)
		{
			DbConnectionPoolKey key = new DbConnectionPoolKey(value);
			this.ConnectionString_Set(key);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000F9908 File Offset: 0x000F8D08
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

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000F99A0 File Offset: 0x000F8DA0
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000F99B4 File Offset: 0x000F8DB4
		// (set) Token: 0x0600247F RID: 9343 RVA: 0x000F99C8 File Offset: 0x000F8DC8
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

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000F99DC File Offset: 0x000F8DDC
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000F99F0 File Offset: 0x000F8DF0
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

		// Token: 0x06002482 RID: 9346 RVA: 0x000F9A5C File Offset: 0x000F8E5C
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000F9A78 File Offset: 0x000F8E78
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

		// Token: 0x06002484 RID: 9348 RVA: 0x000F9ADC File Offset: 0x000F8EDC
		private static CodeAccessPermission CreateExecutePermission()
		{
			DBDataPermission dbdataPermission = (DBDataPermission)OleDbConnectionFactory.SingletonInstance.ProviderFactory.CreatePermission(PermissionState.None);
			dbdataPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return dbdataPermission;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000F9B14 File Offset: 0x000F8F14
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

		// Token: 0x06002486 RID: 9350 RVA: 0x000F9B48 File Offset: 0x000F8F48
		private void EnlistDistributedTransactionHelper(ITransaction transaction)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(OleDbConnection.ExecutePermission);
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

		// Token: 0x06002487 RID: 9351 RVA: 0x000F9BB0 File Offset: 0x000F8FB0
		public override void EnlistTransaction(Transaction transaction)
		{
			OleDbConnection.ExecutePermission.Demand();
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

		// Token: 0x06002488 RID: 9352 RVA: 0x000F9C20 File Offset: 0x000F9020
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000F9C40 File Offset: 0x000F9040
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000F9C54 File Offset: 0x000F9054
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000F9C70 File Offset: 0x000F9070
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000F9C88 File Offset: 0x000F9088
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			OleDbConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000F9CBC File Offset: 0x000F90BC
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000F9CD8 File Offset: 0x000F90D8
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

		// Token: 0x0600248F RID: 9359 RVA: 0x000F9D18 File Offset: 0x000F9118
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000F9D34 File Offset: 0x000F9134
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

		// Token: 0x06002491 RID: 9361 RVA: 0x000F9DAC File Offset: 0x000F91AC
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000F9DCC File Offset: 0x000F91CC
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000F9DE0 File Offset: 0x000F91E0
		[Conditional("DEBUG")]
		internal static void VerifyExecutePermission()
		{
			try
			{
				OleDbConnection.ExecutePermission.Demand();
			}
			catch (SecurityException)
			{
				throw;
			}
		}

		// Token: 0x0400159A RID: 5530
		private static readonly object EventInfoMessage = new object();

		// Token: 0x0400159B RID: 5531
		private static readonly DbConnectionFactory _connectionFactory = OleDbConnectionFactory.SingletonInstance;

		// Token: 0x0400159C RID: 5532
		internal static readonly CodeAccessPermission ExecutePermission = OleDbConnection.CreateExecutePermission();

		// Token: 0x0400159D RID: 5533
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x0400159E RID: 5534
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x0400159F RID: 5535
		private DbConnectionInternal _innerConnection;

		// Token: 0x040015A0 RID: 5536
		private int _closeCount;

		// Token: 0x040015A1 RID: 5537
		private static int _objectTypeCount;

		// Token: 0x040015A2 RID: 5538
		internal readonly int ObjectID = Interlocked.Increment(ref OleDbConnection._objectTypeCount);
	}
}
