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
	// Token: 0x02000214 RID: 532
	[DefaultEvent("InfoMessage")]
	public sealed class OleDbConnection : DbConnection, ICloneable, IDbConnection, IDisposable
	{
		// Token: 0x06001E1A RID: 7706 RVA: 0x002728D8 File Offset: 0x00271CD8
		public OleDbConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x002728F8 File Offset: 0x00271CF8
		private OleDbConnection(OleDbConnection connection) : this()
		{
			this.CopyFrom(connection);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x00272918 File Offset: 0x00271D18
		// (set) Token: 0x06001E1D RID: 7709 RVA: 0x00272938 File Offset: 0x00271D38
		[RecommendedAsConfigurable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[Editor("Microsoft.VSDesigner.Data.ADO.Design.OleDbConnectionStringEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Data")]
		[ResDescription("OleDbConnection_ConnectionString")]
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00272958 File Offset: 0x00271D58
		private OleDbConnectionString OleDbConnectionStringValue
		{
			get
			{
				return (OleDbConnectionString)this.ConnectionOptions;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00272978 File Offset: 0x00271D78
		[ResDescription("OleDbConnection_ConnectionTimeout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x00272A08 File Offset: 0x00271E08
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("OleDbConnection_Database")]
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x00272AE8 File Offset: 0x00271EE8
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x00272BC8 File Offset: 0x00271FC8
		internal bool IsOpen
		{
			get
			{
				return null != this.GetOpenConnection();
			}
		}

		// Token: 0x17000415 RID: 1045
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x00272BE8 File Offset: 0x00271FE8
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

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00272C08 File Offset: 0x00272008
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("OleDbConnection_Provider")]
		[Browsable(true)]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00272C58 File Offset: 0x00272058
		internal OleDbConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return (OleDbConnectionPoolGroupProviderInfo)this.PoolGroup.ProviderInfo;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x00272C78 File Offset: 0x00272078
		[ResDescription("OleDbConnection_ServerVersion")]
		public override string ServerVersion
		{
			get
			{
				return this.InnerConnection.ServerVersion;
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00272C98 File Offset: 0x00272098
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
		// (add) Token: 0x06001E28 RID: 7720 RVA: 0x00272D38 File Offset: 0x00272138
		// (remove) Token: 0x06001E29 RID: 7721 RVA: 0x00272D58 File Offset: 0x00272158
		[ResCategory("DataCategory_InfoMessage")]
		[ResDescription("DbConnection_InfoMessage")]
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

		// Token: 0x06001E2A RID: 7722 RVA: 0x00272D78 File Offset: 0x00272178
		internal UnsafeNativeMethods.ICommandText ICommandText()
		{
			return this.GetOpenConnection().ICommandText();
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00272D98 File Offset: 0x00272198
		private IDBPropertiesWrapper IDBProperties()
		{
			return this.GetOpenConnection().IDBProperties();
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00272DB8 File Offset: 0x002721B8
		internal IOpenRowsetWrapper IOpenRowset()
		{
			return this.GetOpenConnection().IOpenRowset();
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00272DD8 File Offset: 0x002721D8
		internal int SqlSupport()
		{
			return this.OleDbConnectionStringValue.GetSqlSupport(this);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00272DF8 File Offset: 0x002721F8
		internal bool SupportMultipleResults()
		{
			return this.OleDbConnectionStringValue.GetSupportMultipleResults(this);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00272E18 File Offset: 0x00272218
		internal bool SupportIRow(OleDbCommand cmd)
		{
			return this.OleDbConnectionStringValue.GetSupportIRow(this, cmd);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00272E38 File Offset: 0x00272238
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

		// Token: 0x06001E31 RID: 7729 RVA: 0x00272E68 File Offset: 0x00272268
		public new OleDbTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00272E88 File Offset: 0x00272288
		public new OleDbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return (OleDbTransaction)this.InnerConnection.BeginTransaction(isolationLevel);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00272EA8 File Offset: 0x002722A8
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

		// Token: 0x06001E34 RID: 7732 RVA: 0x00272F38 File Offset: 0x00272338
		internal void CheckStateOpen(string method)
		{
			ConnectionState state = this.State;
			if (ConnectionState.Open != state)
			{
				throw ADP.OpenConnectionRequired(method, state);
			}
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00272F58 File Offset: 0x00272358
		object ICloneable.Clone()
		{
			OleDbConnection oleDbConnection = new OleDbConnection(this);
			Bid.Trace("<oledb.OleDbConnection.Clone|API> %d#, clone=%d#\n", this.ObjectID, oleDbConnection.ObjectID);
			return oleDbConnection;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00272F88 File Offset: 0x00272388
		public override void Close()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00272FA8 File Offset: 0x002723A8
		public new OleDbCommand CreateCommand()
		{
			return new OleDbCommand("", this);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00272FC8 File Offset: 0x002723C8
		private void DisposeMe(bool disposing)
		{
			if (disposing && base.DesignMode)
			{
				OleDbConnection.ReleaseObjectPool();
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00272FE8 File Offset: 0x002723E8
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			this.EnlistDistributedTransactionHelper(transaction);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00273008 File Offset: 0x00272408
		internal object GetDataSourcePropertyValue(Guid propertySet, int propertyID)
		{
			OleDbConnectionInternal openConnection = this.GetOpenConnection();
			return openConnection.GetDataSourcePropertyValue(propertySet, propertyID);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00273028 File Offset: 0x00272428
		internal object GetDataSourceValue(Guid propertySet, int propertyID)
		{
			object obj = this.GetDataSourcePropertyValue(propertySet, propertyID);
			if (obj is OleDbPropertyStatus || Convert.IsDBNull(obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00273058 File Offset: 0x00272458
		private OleDbConnectionInternal GetOpenConnection()
		{
			DbConnectionInternal innerConnection = this.InnerConnection;
			return innerConnection as OleDbConnectionInternal;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00273078 File Offset: 0x00272478
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

		// Token: 0x06001E3E RID: 7742 RVA: 0x002730E8 File Offset: 0x002724E8
		public DataTable GetOleDbSchemaTable(Guid schema, object[] restrictions)
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnection.GetOleDbSchemaTable|API> %d#, schema=%p{GUID}, restrictions\n", this.ObjectID, schema);
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

		// Token: 0x06001E3F RID: 7743 RVA: 0x00273238 File Offset: 0x00272638
		internal DataTable GetSchemaRowset(Guid schema, object[] restrictions)
		{
			return this.GetOpenConnection().GetSchemaRowset(schema, restrictions);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00273258 File Offset: 0x00272658
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

		// Token: 0x06001E41 RID: 7745 RVA: 0x00273288 File Offset: 0x00272688
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

		// Token: 0x06001E42 RID: 7746 RVA: 0x00273318 File Offset: 0x00272718
		public override void Open()
		{
			this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			if ((2 & ((OleDbConnectionString)this.ConnectionOptions).OleDbServices) != 0 && ADP.NeedManualEnlistment())
			{
				this.GetOpenConnection().EnlistTransactionInternal(Transaction.Current, true);
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00273368 File Offset: 0x00272768
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

		// Token: 0x06001E44 RID: 7748 RVA: 0x00273468 File Offset: 0x00272868
		internal bool SupportSchemaRowset(Guid schema)
		{
			return this.GetOpenConnection().SupportSchemaRowset(schema);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00273488 File Offset: 0x00272888
		internal OleDbTransaction ValidateTransaction(OleDbTransaction transaction, string method)
		{
			return this.GetOpenConnection().ValidateTransaction(transaction, method);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x002734A8 File Offset: 0x002728A8
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

		// Token: 0x06001E47 RID: 7751 RVA: 0x00273568 File Offset: 0x00272968
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

		// Token: 0x06001E48 RID: 7752 RVA: 0x002735C8 File Offset: 0x002729C8
		private static void ResetState(OleDbConnection connection)
		{
			if (connection != null)
			{
				connection.ResetState();
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x002735E8 File Offset: 0x002729E8
		public OleDbConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00273628 File Offset: 0x00272A28
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

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00273688 File Offset: 0x00272A88
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x002736A8 File Offset: 0x00272AA8
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OleDbConnection._connectionFactory;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x002736C8 File Offset: 0x00272AC8
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

		// Token: 0x06001E4E RID: 7758 RVA: 0x002736E8 File Offset: 0x00272AE8
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

		// Token: 0x06001E4F RID: 7759 RVA: 0x00273728 File Offset: 0x00272B28
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x002737C8 File Offset: 0x00272BC8
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x002737E8 File Offset: 0x00272BE8
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x00273808 File Offset: 0x00272C08
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

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x00273828 File Offset: 0x00272C28
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x00273848 File Offset: 0x00272C48
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00273868 File Offset: 0x00272C68
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

		// Token: 0x06001E56 RID: 7766 RVA: 0x002738D8 File Offset: 0x00272CD8
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x002738F8 File Offset: 0x00272CF8
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

		// Token: 0x06001E58 RID: 7768 RVA: 0x00273958 File Offset: 0x00272D58
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

		// Token: 0x06001E59 RID: 7769 RVA: 0x002739C8 File Offset: 0x00272DC8
		private static CodeAccessPermission CreateExecutePermission()
		{
			DBDataPermission dbdataPermission = (DBDataPermission)OleDbConnectionFactory.SingletonInstance.ProviderFactory.CreatePermission(PermissionState.None);
			dbdataPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return dbdataPermission;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00273A08 File Offset: 0x00272E08
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

		// Token: 0x06001E5B RID: 7771 RVA: 0x00273A48 File Offset: 0x00272E48
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

		// Token: 0x06001E5C RID: 7772 RVA: 0x00273AB8 File Offset: 0x00272EB8
		public override void EnlistTransaction(Transaction transaction)
		{
			OleDbConnection.ExecutePermission.Demand();
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

		// Token: 0x06001E5D RID: 7773 RVA: 0x00273B18 File Offset: 0x00272F18
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00273B38 File Offset: 0x00272F38
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x00273B58 File Offset: 0x00272F58
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00273B78 File Offset: 0x00272F78
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00273B98 File Offset: 0x00272F98
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			OleDbConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x00273BD8 File Offset: 0x00272FD8
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00273BF8 File Offset: 0x00272FF8
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

		// Token: 0x06001E64 RID: 7780 RVA: 0x00273C38 File Offset: 0x00273038
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00273C58 File Offset: 0x00273058
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

		// Token: 0x06001E66 RID: 7782 RVA: 0x00273CD8 File Offset: 0x002730D8
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x00273CF8 File Offset: 0x002730F8
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00273D18 File Offset: 0x00273118
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

		// Token: 0x0400126C RID: 4716
		private static readonly object EventInfoMessage = new object();

		// Token: 0x0400126D RID: 4717
		private static readonly DbConnectionFactory _connectionFactory = OleDbConnectionFactory.SingletonInstance;

		// Token: 0x0400126E RID: 4718
		internal static readonly CodeAccessPermission ExecutePermission = OleDbConnection.CreateExecutePermission();

		// Token: 0x0400126F RID: 4719
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04001270 RID: 4720
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x04001271 RID: 4721
		private DbConnectionInternal _innerConnection;

		// Token: 0x04001272 RID: 4722
		private int _closeCount;

		// Token: 0x04001273 RID: 4723
		private static int _objectTypeCount;

		// Token: 0x04001274 RID: 4724
		internal readonly int ObjectID = Interlocked.Increment(ref OleDbConnection._objectTypeCount);
	}
}
