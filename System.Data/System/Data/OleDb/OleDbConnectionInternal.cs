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
	// Token: 0x02000216 RID: 534
	internal sealed class OleDbConnectionInternal : DbConnectionInternal, IDisposable
	{
		// Token: 0x06001E7A RID: 7802 RVA: 0x00274038 File Offset: 0x00273438
		internal OleDbConnectionInternal(OleDbConnectionString constr, OleDbConnection connection)
		{
			this.ConnectionString = constr;
			if (constr.PossiblePrompt && !Environment.UserInteractive)
			{
				throw ODB.PossiblePromptNotUserInteractive();
			}
			OleDbServicesWrapper objectPool = OleDbConnectionInternal.GetObjectPool();
			this._datasrcwrp = new DataSourceWrapper();
			objectPool.GetDataSource(constr, ref this._datasrcwrp);
			if (connection == null)
			{
				return;
			}
			this._sessionwrp = new SessionWrapper();
			OleDbHResult oleDbHResult = this._datasrcwrp.InitializeAndCreateSession(constr, ref this._sessionwrp);
			if (OleDbHResult.S_OK <= oleDbHResult && !this._sessionwrp.IsInvalid)
			{
				OleDbConnection.ProcessResults(oleDbHResult, connection, connection);
				return;
			}
			Exception ex = OleDbConnection.ProcessResults(oleDbHResult, null, null);
			throw ex;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x002740D8 File Offset: 0x002734D8
		internal OleDbConnection Connection
		{
			get
			{
				return (OleDbConnection)base.Owner;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x002740F8 File Offset: 0x002734F8
		internal bool HasSession
		{
			get
			{
				return null != this._sessionwrp;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00274118 File Offset: 0x00273518
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x00274148 File Offset: 0x00273548
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00274178 File Offset: 0x00273578
		private string Provider
		{
			get
			{
				return this.ConnectionString.Provider;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00274198 File Offset: 0x00273598
		public override string ServerVersion
		{
			get
			{
				object dataSourceValue = this.GetDataSourceValue(OleDbPropertySetGuid.DataSourceInfo, 41);
				return Convert.ToString(dataSourceValue, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x002741C8 File Offset: 0x002735C8
		internal IDBPropertiesWrapper IDBProperties()
		{
			return this._datasrcwrp.IDBProperties(this);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x002741E8 File Offset: 0x002735E8
		internal IOpenRowsetWrapper IOpenRowset()
		{
			return this._sessionwrp.IOpenRowset(this);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00274208 File Offset: 0x00273608
		private IDBInfoWrapper IDBInfo()
		{
			return this._datasrcwrp.IDBInfo(this);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00274228 File Offset: 0x00273628
		internal IDBSchemaRowsetWrapper IDBSchemaRowset()
		{
			return this._sessionwrp.IDBSchemaRowset(this);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00274248 File Offset: 0x00273648
		internal ITransactionJoinWrapper ITransactionJoin()
		{
			return this._sessionwrp.ITransactionJoin(this);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00274268 File Offset: 0x00273668
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

		// Token: 0x06001E87 RID: 7815 RVA: 0x002742A8 File Offset: 0x002736A8
		protected override void Activate(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x002742C8 File Offset: 0x002736C8
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

		// Token: 0x06001E89 RID: 7817 RVA: 0x00274378 File Offset: 0x00273778
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new OleDbReferenceCollection();
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00274398 File Offset: 0x00273798
		protected override void Deactivate()
		{
			base.NotifyWeakReference(0);
			if (this._forcedAutomaticEnlistment)
			{
				this.EnlistTransactionInternal(null, false);
			}
			OleDbTransaction localTransaction = this.LocalTransaction;
			if (localTransaction != null)
			{
				this.LocalTransaction = null;
				localTransaction.Dispose();
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x002743D8 File Offset: 0x002737D8
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

		// Token: 0x06001E8C RID: 7820 RVA: 0x00274418 File Offset: 0x00273818
		public override void EnlistTransaction(Transaction transaction)
		{
			OleDbConnection connection = this.Connection;
			if (this.LocalTransaction != null)
			{
				throw ADP.LocalTransactionPresent();
			}
			this.EnlistTransactionInternal(transaction, false);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00274448 File Offset: 0x00273848
		internal void EnlistTransactionInternal(Transaction transaction, bool forcedAutomatic)
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
					this._forcedAutomaticEnlistment = forcedAutomatic;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			base.EnlistedTransaction = transaction;
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00274508 File Offset: 0x00273908
		internal object GetDataSourceValue(Guid propertySet, int propertyID)
		{
			object obj = this.GetDataSourcePropertyValue(propertySet, propertyID);
			if (obj is OleDbPropertyStatus || Convert.IsDBNull(obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00274538 File Offset: 0x00273938
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

		// Token: 0x06001E90 RID: 7824 RVA: 0x00274618 File Offset: 0x00273A18
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
					DataColumn column = new DataColumn("LiteralName", typeof(string));
					DataColumn column2 = new DataColumn("LiteralValue", typeof(string));
					DataColumn column3 = new DataColumn("InvalidChars", typeof(string));
					DataColumn column4 = new DataColumn("InvalidStartingChars", typeof(string));
					DataColumn column5 = new DataColumn("Literal", typeof(int));
					DataColumn column6 = new DataColumn("Maxlen", typeof(int));
					dataTable.Columns.Add(column);
					dataTable.Columns.Add(column2);
					dataTable.Columns.Add(column3);
					dataTable.Columns.Add(column4);
					dataTable.Columns.Add(column5);
					dataTable.Columns.Add(column6);
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
								dataRow[column] = ((OleDbLiteral)tagDBLITERALINFO.it).ToString();
								dataRow[column2] = tagDBLITERALINFO.pwszLiteralValue;
								dataRow[column3] = tagDBLITERALINFO.pwszInvalidChars;
								dataRow[column4] = tagDBLITERALINFO.pwszInvalidStartingChars;
								dataRow[column5] = tagDBLITERALINFO.it;
								dataRow[column6] = tagDBLITERALINFO.cchMaxLen;
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

		// Token: 0x06001E91 RID: 7825 RVA: 0x00274878 File Offset: 0x00273C78
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

		// Token: 0x06001E92 RID: 7826 RVA: 0x002748D8 File Offset: 0x00273CD8
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

		// Token: 0x06001E93 RID: 7827 RVA: 0x002749B8 File Offset: 0x00273DB8
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

		// Token: 0x06001E94 RID: 7828 RVA: 0x00274A88 File Offset: 0x00273E88
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

		// Token: 0x06001E95 RID: 7829 RVA: 0x00274B78 File Offset: 0x00273F78
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

		// Token: 0x06001E96 RID: 7830 RVA: 0x00274CE8 File Offset: 0x002740E8
		internal DataTable GetSchemaRowset(Guid schema, object[] restrictions)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbConnectionInternal.GetSchemaRowset|INFO> %d#, schema=%p{GUID}, restrictions\n", base.ObjectID, schema);
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

		// Token: 0x06001E97 RID: 7831 RVA: 0x00274E58 File Offset: 0x00274258
		internal bool HasLiveReader(OleDbCommand cmd)
		{
			bool result = false;
			DbReferenceCollection referenceCollection = base.ReferenceCollection;
			if (referenceCollection != null)
			{
				foreach (object obj in referenceCollection.Filter(2))
				{
					OleDbDataReader oleDbDataReader = (OleDbDataReader)obj;
					if (oleDbDataReader != null && cmd == oleDbDataReader.Command)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00274EE8 File Offset: 0x002742E8
		private void ProcessResults(OleDbHResult hr)
		{
			OleDbConnection connection = this.Connection;
			Exception ex = OleDbConnection.ProcessResults(hr, connection, connection);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00274F18 File Offset: 0x00274318
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

		// Token: 0x06001E9A RID: 7834 RVA: 0x00274F58 File Offset: 0x00274358
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static object CreateInstanceDataLinks()
		{
			Type typeFromCLSID = Type.GetTypeFromCLSID(ODB.CLSID_DataLinks, true);
			return Activator.CreateInstance(typeFromCLSID, BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00274F88 File Offset: 0x00274388
		private static OleDbServicesWrapper GetObjectPool()
		{
			OleDbServicesWrapper oleDbServicesWrapper = OleDbConnectionInternal.idataInitialize;
			if (oleDbServicesWrapper == null)
			{
				lock (OleDbConnectionInternal.dataInitializeLock)
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

		// Token: 0x06001E9C RID: 7836 RVA: 0x00275038 File Offset: 0x00274438
		private static void VersionCheck()
		{
			if (ApartmentState.Unknown == Thread.CurrentThread.GetApartmentState())
			{
				OleDbConnectionInternal.SetMTAApartmentState();
			}
			ADP.CheckVersionMDAC(false);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00275068 File Offset: 0x00274468
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void SetMTAApartmentState()
		{
			Thread.CurrentThread.SetApartmentState(ApartmentState.MTA);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00275088 File Offset: 0x00274488
		public static void ReleaseObjectPool()
		{
			OleDbConnectionInternal.idataInitialize = null;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x002750A8 File Offset: 0x002744A8
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

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00275138 File Offset: 0x00274538
		internal Dictionary<string, OleDbPropertyInfo> GetPropertyInfo(Guid[] propertySets)
		{
			bool hasSession = this.HasSession;
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

		// Token: 0x04001278 RID: 4728
		private static volatile OleDbServicesWrapper idataInitialize;

		// Token: 0x04001279 RID: 4729
		private static object dataInitializeLock = new object();

		// Token: 0x0400127A RID: 4730
		internal readonly OleDbConnectionString ConnectionString;

		// Token: 0x0400127B RID: 4731
		private readonly DataSourceWrapper _datasrcwrp;

		// Token: 0x0400127C RID: 4732
		private readonly SessionWrapper _sessionwrp;

		// Token: 0x0400127D RID: 4733
		private WeakReference weakTransaction;

		// Token: 0x0400127E RID: 4734
		private bool _forcedAutomaticEnlistment;
	}
}
