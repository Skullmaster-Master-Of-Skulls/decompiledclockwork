using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Transactions;

namespace System.Data.OleDb
{
	// Token: 0x02000245 RID: 581
	internal sealed class OleDbConnectionInternal : DbConnectionInternal, IDisposable
	{
		// Token: 0x060024A5 RID: 9381 RVA: 0x000FA0A4 File Offset: 0x000F94A4
		internal OleDbConnectionInternal(OleDbConnectionString constr, OleDbConnection connection)
		{
			this.ConnectionString = constr;
			if (constr.PossiblePrompt && !Environment.UserInteractive)
			{
				throw ODB.PossiblePromptNotUserInteractive();
			}
			try
			{
				OleDbServicesWrapper objectPool = OleDbConnectionInternal.GetObjectPool();
				this._datasrcwrp = new DataSourceWrapper();
				objectPool.GetDataSource(constr, ref this._datasrcwrp);
				if (connection != null)
				{
					this._sessionwrp = new SessionWrapper();
					OleDbHResult oleDbHResult = this._datasrcwrp.InitializeAndCreateSession(constr, ref this._sessionwrp);
					if (OleDbHResult.S_OK > oleDbHResult || this._sessionwrp.IsInvalid)
					{
						Exception ex = OleDbConnection.ProcessResults(oleDbHResult, null, null);
						throw ex;
					}
					OleDbConnection.ProcessResults(oleDbHResult, connection, connection);
				}
			}
			catch
			{
				if (this._sessionwrp != null)
				{
					this._sessionwrp.Dispose();
					this._sessionwrp = null;
				}
				if (this._datasrcwrp != null)
				{
					this._datasrcwrp.Dispose();
					this._datasrcwrp = null;
				}
				throw;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000FA194 File Offset: 0x000F9594
		internal OleDbConnection Connection
		{
			get
			{
				return (OleDbConnection)base.Owner;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000FA1AC File Offset: 0x000F95AC
		internal bool HasSession
		{
			get
			{
				return this._sessionwrp != null;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x000FA1C4 File Offset: 0x000F95C4
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x000FA1F0 File Offset: 0x000F95F0
		internal OleDbTransaction LocalTransaction
		{
			get
			{
				OleDbTransaction result = null;
				if (this.weakTransaction != null)
				{
					result = (OleDbTransaction)this.weakTransaction.Target;
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

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x000FA214 File Offset: 0x000F9614
		private string Provider
		{
			get
			{
				return this.ConnectionString.Provider;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x000FA22C File Offset: 0x000F962C
		public override string ServerVersion
		{
			get
			{
				object dataSourceValue = this.GetDataSourceValue(OleDbPropertySetGuid.DataSourceInfo, 41);
				return Convert.ToString(dataSourceValue, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000FA254 File Offset: 0x000F9654
		internal IDBPropertiesWrapper IDBProperties()
		{
			return this._datasrcwrp.IDBProperties(this);
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000FA270 File Offset: 0x000F9670
		internal IOpenRowsetWrapper IOpenRowset()
		{
			return this._sessionwrp.IOpenRowset(this);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000FA28C File Offset: 0x000F968C
		private IDBInfoWrapper IDBInfo()
		{
			return this._datasrcwrp.IDBInfo(this);
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000FA2A8 File Offset: 0x000F96A8
		internal IDBSchemaRowsetWrapper IDBSchemaRowset()
		{
			return this._sessionwrp.IDBSchemaRowset(this);
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000FA2C4 File Offset: 0x000F96C4
		internal ITransactionJoinWrapper ITransactionJoin()
		{
			return this._sessionwrp.ITransactionJoin(this);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000FA2E0 File Offset: 0x000F96E0
		internal UnsafeNativeMethods.ICommandText ICommandText()
		{
			object obj = null;
			OleDbHResult oleDbHResult = this._sessionwrp.CreateCommand(ref obj);
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				if (OleDbHResult.E_NOINTERFACE != oleDbHResult)
				{
					this.ProcessResults(oleDbHResult);
				}
				else
				{
					SafeNativeMethods.Wrapper.ClearErrorInfo();
				}
			}
			return (UnsafeNativeMethods.ICommandText)obj;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000FA320 File Offset: 0x000F9720
		protected override void Activate(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000FA334 File Offset: 0x000F9734
		public override DbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			OleDbConnection.ExecutePermission.Demand();
			OleDbConnection connection = this.Connection;
			if (this.LocalTransaction != null)
			{
				throw ADP.ParallelTransactionsNotSupported(connection);
			}
			object obj = null;
			OleDbTransaction oleDbTransaction;
			try
			{
				oleDbTransaction = new OleDbTransaction(connection, null, isolationLevel);
				Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|session> %d#, ITransactionLocal\n", base.ObjectID);
				obj = this._sessionwrp.ComWrapper();
				UnsafeNativeMethods.ITransactionLocal transactionLocal = obj as UnsafeNativeMethods.ITransactionLocal;
				if (transactionLocal == null)
				{
					throw ODB.TransactionsNotSupported(this.Provider, null);
				}
				oleDbTransaction.BeginInternal(transactionLocal);
			}
			finally
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(obj);
				}
			}
			this.LocalTransaction = oleDbTransaction;
			return oleDbTransaction;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000FA3D8 File Offset: 0x000F97D8
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new OleDbReferenceCollection();
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000FA3EC File Offset: 0x000F97EC
		protected override void Deactivate()
		{
			base.NotifyWeakReference(0);
			if (this._unEnlistDuringDeactivate)
			{
				this.EnlistTransactionInternal(null);
			}
			OleDbTransaction localTransaction = this.LocalTransaction;
			if (localTransaction != null)
			{
				this.LocalTransaction = null;
				localTransaction.Dispose();
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000FA428 File Offset: 0x000F9828
		public override void Dispose()
		{
			if (this._sessionwrp != null)
			{
				this._sessionwrp.Dispose();
			}
			if (this._datasrcwrp != null)
			{
				this._datasrcwrp.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000FA464 File Offset: 0x000F9864
		public override void EnlistTransaction(Transaction transaction)
		{
			OleDbConnection connection = this.Connection;
			if (this.LocalTransaction != null)
			{
				throw ADP.LocalTransactionPresent();
			}
			this.EnlistTransactionInternal(transaction);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000FA490 File Offset: 0x000F9890
		internal void EnlistTransactionInternal(Transaction transaction)
		{
			IDtcTransaction oletxTransaction = ADP.GetOletxTransaction(transaction);
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.ITransactionJoin.JoinTransaction|API|OLEDB> %d#\n", base.ObjectID);
			try
			{
				using (ITransactionJoinWrapper transactionJoinWrapper = this.ITransactionJoin())
				{
					if (transactionJoinWrapper.Value == null)
					{
						throw ODB.TransactionsNotSupported(this.Provider, null);
					}
					transactionJoinWrapper.Value.JoinTransaction(oletxTransaction, -1, 0, IntPtr.Zero);
					this._unEnlistDuringDeactivate = (null != transaction);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			base.EnlistedTransaction = transaction;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000FA54C File Offset: 0x000F994C
		internal object GetDataSourceValue(Guid propertySet, int propertyID)
		{
			object obj = this.GetDataSourcePropertyValue(propertySet, propertyID);
			if (obj is OleDbPropertyStatus || Convert.IsDBNull(obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000FA578 File Offset: 0x000F9978
		internal object GetDataSourcePropertyValue(Guid propertySet, int propertyID)
		{
			tagDBPROP[] propertySet2;
			using (IDBPropertiesWrapper idbpropertiesWrapper = this.IDBProperties())
			{
				using (PropertyIDSet propertyIDSet = new PropertyIDSet(propertySet, propertyID))
				{
					OleDbHResult oleDbHResult;
					using (DBPropSet dbpropSet = new DBPropSet(idbpropertiesWrapper.Value, propertyIDSet, ref oleDbHResult))
					{
						if (oleDbHResult < OleDbHResult.S_OK)
						{
							SafeNativeMethods.Wrapper.ClearErrorInfo();
						}
						propertySet2 = dbpropSet.GetPropertySet(0, out propertySet);
					}
				}
			}
			if (propertySet2[0].dwStatus == OleDbPropertyStatus.Ok)
			{
				return propertySet2[0].vValue;
			}
			return propertySet2[0].dwStatus;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000FA64C File Offset: 0x000F9A4C
		internal DataTable BuildInfoLiterals()
		{
			DataTable result;
			using (IDBInfoWrapper idbinfoWrapper = this.IDBInfo())
			{
				UnsafeNativeMethods.IDBInfo value = idbinfoWrapper.Value;
				if (value == null)
				{
					result = null;
				}
				else
				{
					DataTable dataTable = new DataTable("DbInfoLiterals");
					dataTable.Locale = CultureInfo.InvariantCulture;
					DataColumn dataColumn = new DataColumn("LiteralName", typeof(string));
					DataColumn column = new DataColumn("LiteralValue", typeof(string));
					DataColumn column2 = new DataColumn("InvalidChars", typeof(string));
					DataColumn column3 = new DataColumn("InvalidStartingChars", typeof(string));
					DataColumn column4 = new DataColumn("Literal", typeof(int));
					DataColumn column5 = new DataColumn("Maxlen", typeof(int));
					dataTable.Columns.Add(dataColumn);
					dataTable.Columns.Add(column);
					dataTable.Columns.Add(column2);
					dataTable.Columns.Add(column3);
					dataTable.Columns.Add(column4);
					dataTable.Columns.Add(column5);
					int num = 0;
					IntPtr ptrZero = ADP.PtrZero;
					OleDbHResult oleDbHResult;
					using (new DualCoTaskMem(value, null, ref num, ref ptrZero, ref oleDbHResult))
					{
						if (OleDbHResult.DB_E_ERRORSOCCURRED != oleDbHResult)
						{
							long num2 = ptrZero.ToInt64();
							tagDBLITERALINFO tagDBLITERALINFO = new tagDBLITERALINFO();
							int i = 0;
							while (i < num)
							{
								Marshal.PtrToStructure((IntPtr)num2, tagDBLITERALINFO);
								DataRow dataRow = dataTable.NewRow();
								DataRow dataRow2 = dataRow;
								DataColumn column6 = dataColumn;
								OleDbLiteral it = (OleDbLiteral)tagDBLITERALINFO.it;
								dataRow2[column6] = it.ToString();
								dataRow[column] = tagDBLITERALINFO.pwszLiteralValue;
								dataRow[column2] = tagDBLITERALINFO.pwszInvalidChars;
								dataRow[column3] = tagDBLITERALINFO.pwszInvalidStartingChars;
								dataRow[column4] = tagDBLITERALINFO.it;
								dataRow[column5] = tagDBLITERALINFO.cchMaxLen;
								dataTable.Rows.Add(dataRow);
								dataRow.AcceptChanges();
								i++;
								num2 += (long)ODB.SizeOf_tagDBLITERALINFO;
							}
							if (oleDbHResult < OleDbHResult.S_OK)
							{
								this.ProcessResults(oleDbHResult);
							}
						}
						else
						{
							SafeNativeMethods.Wrapper.ClearErrorInfo();
						}
					}
					result = dataTable;
				}
			}
			return result;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000FA8B0 File Offset: 0x000F9CB0
		internal DataTable BuildInfoKeywords()
		{
			DataTable dataTable = new DataTable("DbInfoKeywords");
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumn dataColumn = new DataColumn("Keyword", typeof(string));
			dataTable.Columns.Add(dataColumn);
			if (!this.AddInfoKeywordsToTable(dataTable, dataColumn))
			{
				dataTable = null;
			}
			return dataTable;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000FA904 File Offset: 0x000F9D04
		internal bool AddInfoKeywordsToTable(DataTable table, DataColumn keyword)
		{
			bool result;
			using (IDBInfoWrapper idbinfoWrapper = this.IDBInfo())
			{
				UnsafeNativeMethods.IDBInfo value = idbinfoWrapper.Value;
				if (value == null)
				{
					result = false;
				}
				else
				{
					Bid.Trace("<oledb.IDBInfo.GetKeywords|API|OLEDB> %d#\n", base.ObjectID);
					string text;
					OleDbHResult keywords = value.GetKeywords(out text);
					Bid.Trace("<oledb.IDBInfo.GetKeywords|API|OLEDB|RET> %08X{HRESULT}\n", keywords);
					if (keywords < OleDbHResult.S_OK)
					{
						this.ProcessResults(keywords);
					}
					if (text != null)
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							DataRow dataRow = table.NewRow();
							dataRow[keyword] = array[i];
							table.Rows.Add(dataRow);
							dataRow.AcceptChanges();
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000FA9D8 File Offset: 0x000F9DD8
		internal DataTable BuildSchemaGuids()
		{
			DataTable dataTable = new DataTable("SchemaGuids");
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumn column = new DataColumn("Schema", typeof(Guid));
			DataColumn column2 = new DataColumn("RestrictionSupport", typeof(int));
			dataTable.Columns.Add(column);
			dataTable.Columns.Add(column2);
			SchemaSupport[] schemaRowsetInformation = this.GetSchemaRowsetInformation();
			if (schemaRowsetInformation != null)
			{
				object[] array = new object[2];
				dataTable.BeginLoadData();
				for (int i = 0; i < schemaRowsetInformation.Length; i++)
				{
					array[0] = schemaRowsetInformation[i]._schemaRowset;
					array[1] = schemaRowsetInformation[i]._restrictions;
					dataTable.LoadDataRow(array, LoadOption.OverwriteChanges);
				}
				dataTable.EndLoadData();
			}
			return dataTable;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000FAAA0 File Offset: 0x000F9EA0
		internal string GetLiteralInfo(int literal)
		{
			string result;
			using (IDBInfoWrapper idbinfoWrapper = this.IDBInfo())
			{
				UnsafeNativeMethods.IDBInfo value = idbinfoWrapper.Value;
				if (value == null)
				{
					result = null;
				}
				else
				{
					string text = null;
					IntPtr ptrZero = ADP.PtrZero;
					int num = 0;
					OleDbHResult oleDbHResult;
					using (new DualCoTaskMem(value, new int[]
					{
						literal
					}, ref num, ref ptrZero, ref oleDbHResult))
					{
						if (OleDbHResult.DB_E_ERRORSOCCURRED != oleDbHResult)
						{
							if (1 == num && Marshal.ReadInt32(ptrZero, ODB.OffsetOf_tagDBLITERALINFO_it) == literal)
							{
								text = Marshal.PtrToStringUni(Marshal.ReadIntPtr(ptrZero, 0));
							}
							if (oleDbHResult < OleDbHResult.S_OK)
							{
								this.ProcessResults(oleDbHResult);
							}
						}
						else
						{
							SafeNativeMethods.Wrapper.ClearErrorInfo();
						}
					}
					result = text;
				}
			}
			return result;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000FAB7C File Offset: 0x000F9F7C
		internal SchemaSupport[] GetSchemaRowsetInformation()
		{
			OleDbConnectionString connectionString = this.ConnectionString;
			SchemaSupport[] array = connectionString.SchemaSupport;
			if (array != null)
			{
				return array;
			}
			SchemaSupport[] result;
			using (IDBSchemaRowsetWrapper idbschemaRowsetWrapper = this.IDBSchemaRowset())
			{
				UnsafeNativeMethods.IDBSchemaRowset value = idbschemaRowsetWrapper.Value;
				if (value == null)
				{
					result = null;
				}
				else
				{
					int num = 0;
					IntPtr ptrZero = ADP.PtrZero;
					IntPtr ptrZero2 = ADP.PtrZero;
					OleDbHResult oleDbHResult;
					using (new DualCoTaskMem(value, ref num, ref ptrZero, ref ptrZero2, ref oleDbHResult))
					{
						if (oleDbHResult < OleDbHResult.S_OK)
						{
							this.ProcessResults(oleDbHResult);
						}
						array = new SchemaSupport[num];
						if (ADP.PtrZero != ptrZero)
						{
							int i = 0;
							int num2 = 0;
							while (i < array.Length)
							{
								IntPtr ptr = ADP.IntPtrOffset(ptrZero, i * ODB.SizeOf_Guid);
								array[i]._schemaRowset = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
								i++;
								num2 += ODB.SizeOf_Guid;
							}
						}
						if (ADP.PtrZero != ptrZero2)
						{
							for (int j = 0; j < array.Length; j++)
							{
								array[j]._restrictions = Marshal.ReadInt32(ptrZero2, j * 4);
							}
						}
					}
					connectionString.SchemaSupport = array;
					result = array;
				}
			}
			return result;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000FACE0 File Offset: 0x000FA0E0
		internal DataTable GetSchemaRowset(Guid schema, object[] restrictions)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnectionInternal.GetSchemaRowset|INFO> %d#, schema=%ls, restrictions\n", base.ObjectID, schema);
			DataTable result;
			try
			{
				if (restrictions == null)
				{
					restrictions = new object[0];
				}
				DataTable dataTable = null;
				using (IDBSchemaRowsetWrapper idbschemaRowsetWrapper = this.IDBSchemaRowset())
				{
					UnsafeNativeMethods.IDBSchemaRowset value = idbschemaRowsetWrapper.Value;
					if (value == null)
					{
						throw ODB.SchemaRowsetsNotSupported(this.Provider);
					}
					UnsafeNativeMethods.IRowset rowset = null;
					Bid.Trace("<oledb.IDBSchemaRowset.GetRowset|API|OLEDB> %d#\n", base.ObjectID);
					OleDbHResult rowset2 = value.GetRowset(ADP.PtrZero, ref schema, restrictions.Length, restrictions, ref ODB.IID_IRowset, 0, ADP.PtrZero, out rowset);
					Bid.Trace("<oledb.IDBSchemaRowset.GetRowset|API|OLEDB|RET> %08X{HRESULT}\n", rowset2);
					if (rowset2 < OleDbHResult.S_OK)
					{
						this.ProcessResults(rowset2);
					}
					if (rowset != null)
					{
						using (OleDbDataReader oleDbDataReader = new OleDbDataReader(this.Connection, null, 0, CommandBehavior.Default))
						{
							oleDbDataReader.InitializeIRowset(rowset, ChapterHandle.DB_NULL_HCHAPTER, IntPtr.Zero);
							oleDbDataReader.BuildMetaInfo();
							oleDbDataReader.HasRowsRead();
							dataTable = new DataTable();
							dataTable.Locale = CultureInfo.InvariantCulture;
							dataTable.TableName = OleDbSchemaGuid.GetTextFromValue(schema);
							OleDbDataAdapter.FillDataTable(oleDbDataReader, new DataTable[]
							{
								dataTable
							});
						}
					}
					result = dataTable;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000FAE4C File Offset: 0x000FA24C
		internal bool HasLiveReader(OleDbCommand cmd)
		{
			OleDbDataReader oleDbDataReader = null;
			if (base.ReferenceCollection != null)
			{
				oleDbDataReader = base.ReferenceCollection.FindItem<OleDbDataReader>(2, (OleDbDataReader dataReader) => cmd == dataReader.Command);
			}
			return oleDbDataReader != null;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000FAE90 File Offset: 0x000FA290
		private void ProcessResults(OleDbHResult hr)
		{
			OleDbConnection connection = this.Connection;
			Exception ex = OleDbConnection.ProcessResults(hr, connection, connection);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000FAEB4 File Offset: 0x000FA2B4
		internal bool SupportSchemaRowset(Guid schema)
		{
			SchemaSupport[] schemaRowsetInformation = this.GetSchemaRowsetInformation();
			if (schemaRowsetInformation != null)
			{
				for (int i = 0; i < schemaRowsetInformation.Length; i++)
				{
					if (schema == schemaRowsetInformation[i]._schemaRowset)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000FAEF0 File Offset: 0x000FA2F0
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static object CreateInstanceDataLinks()
		{
			Type typeFromCLSID = Type.GetTypeFromCLSID(ODB.CLSID_DataLinks, true);
			return Activator.CreateInstance(typeFromCLSID, BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000FAF1C File Offset: 0x000FA31C
		private static OleDbServicesWrapper GetObjectPool()
		{
			OleDbServicesWrapper oleDbServicesWrapper = OleDbConnectionInternal.idataInitialize;
			if (oleDbServicesWrapper == null)
			{
				object obj = OleDbConnectionInternal.dataInitializeLock;
				lock (obj)
				{
					oleDbServicesWrapper = OleDbConnectionInternal.idataInitialize;
					if (oleDbServicesWrapper == null)
					{
						OleDbConnectionInternal.VersionCheck();
						object obj2;
						try
						{
							obj2 = OleDbConnectionInternal.CreateInstanceDataLinks();
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							throw ODB.MDACNotAvailable(ex);
						}
						if (obj2 == null)
						{
							throw ODB.MDACNotAvailable(null);
						}
						oleDbServicesWrapper = new OleDbServicesWrapper(obj2);
						OleDbConnectionInternal.idataInitialize = oleDbServicesWrapper;
					}
				}
			}
			return oleDbServicesWrapper;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000FAFCC File Offset: 0x000FA3CC
		private static void VersionCheck()
		{
			if (ApartmentState.Unknown == Thread.CurrentThread.GetApartmentState())
			{
				OleDbConnectionInternal.SetMTAApartmentState();
			}
			ADP.CheckVersionMDAC(false);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000FAFF4 File Offset: 0x000FA3F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void SetMTAApartmentState()
		{
			Thread.CurrentThread.SetApartmentState(ApartmentState.MTA);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000FB00C File Offset: 0x000FA40C
		public static void ReleaseObjectPool()
		{
			OleDbConnectionInternal.idataInitialize = null;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000FB024 File Offset: 0x000FA424
		internal OleDbTransaction ValidateTransaction(OleDbTransaction transaction, string method)
		{
			if (this.weakTransaction != null)
			{
				OleDbTransaction oleDbTransaction = (OleDbTransaction)this.weakTransaction.Target;
				if (oleDbTransaction != null && this.weakTransaction.IsAlive)
				{
					oleDbTransaction = OleDbTransaction.TransactionUpdate(oleDbTransaction);
				}
				if (oleDbTransaction != null)
				{
					if (transaction == null)
					{
						throw ADP.TransactionRequired(method);
					}
					OleDbTransaction oleDbTransaction2 = OleDbTransaction.TransactionLast(oleDbTransaction);
					if (oleDbTransaction2 == transaction)
					{
						return transaction;
					}
					if (oleDbTransaction2.Connection != transaction.Connection)
					{
						throw ADP.TransactionConnectionMismatch();
					}
					throw ADP.TransactionCompleted();
				}
				else
				{
					this.weakTransaction = null;
				}
			}
			else if (transaction != null && transaction.Connection != null)
			{
				throw ADP.TransactionConnectionMismatch();
			}
			return null;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000FB0B0 File Offset: 0x000FA4B0
		internal Dictionary<string, OleDbPropertyInfo> GetPropertyInfo(Guid[] propertySets)
		{
			bool hasSession = this.HasSession;
			OleDbConnectionString connectionString = this.ConnectionString;
			Dictionary<string, OleDbPropertyInfo> result = null;
			if (propertySets == null)
			{
				propertySets = new Guid[0];
			}
			using (PropertyIDSet propertyIDSet = new PropertyIDSet(propertySets))
			{
				using (IDBPropertiesWrapper idbpropertiesWrapper = this.IDBProperties())
				{
					using (PropertyInfoSet propertyInfoSet = new PropertyInfoSet(idbpropertiesWrapper.Value, propertyIDSet))
					{
						result = propertyInfoSet.GetValues();
					}
				}
			}
			return result;
		}

		// Token: 0x040015A6 RID: 5542
		private static volatile OleDbServicesWrapper idataInitialize;

		// Token: 0x040015A7 RID: 5543
		private static object dataInitializeLock = new object();

		// Token: 0x040015A8 RID: 5544
		internal readonly OleDbConnectionString ConnectionString;

		// Token: 0x040015A9 RID: 5545
		private readonly DataSourceWrapper _datasrcwrp;

		// Token: 0x040015AA RID: 5546
		private readonly SessionWrapper _sessionwrp;

		// Token: 0x040015AB RID: 5547
		private WeakReference weakTransaction;

		// Token: 0x040015AC RID: 5548
		private bool _unEnlistDuringDeactivate;
	}
}
