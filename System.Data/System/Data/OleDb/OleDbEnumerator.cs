using System;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data.OleDb
{
	// Token: 0x02000224 RID: 548
	public sealed class OleDbEnumerator
	{
		// Token: 0x06001F77 RID: 8055 RVA: 0x0027AEE8 File Offset: 0x0027A2E8
		public DataTable GetElements()
		{
			OleDbConnection.ExecutePermission.Demand();
			DataTable dataTable = new DataTable("MSDAENUM");
			dataTable.Locale = CultureInfo.InvariantCulture;
			OleDbDataReader rootEnumerator = OleDbEnumerator.GetRootEnumerator();
			OleDbDataAdapter.FillDataTable(rootEnumerator, new DataTable[]
			{
				dataTable
			});
			return dataTable;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0027AF38 File Offset: 0x0027A338
		public static OleDbDataReader GetEnumerator(Type type)
		{
			OleDbConnection.ExecutePermission.Demand();
			return OleDbEnumerator.GetEnumeratorFromType(type);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0027AF58 File Offset: 0x0027A358
		internal static OleDbDataReader GetEnumeratorFromType(Type type)
		{
			object value = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
			return OleDbEnumerator.GetEnumeratorReader(value);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0027AF88 File Offset: 0x0027A388
		private static OleDbDataReader GetEnumeratorReader(object value)
		{
			NativeMethods.ISourcesRowset sourcesRowset = null;
			try
			{
				sourcesRowset = (NativeMethods.ISourcesRowset)value;
			}
			catch (InvalidCastException)
			{
				throw ODB.ISourcesRowsetNotSupported();
			}
			if (sourcesRowset == null)
			{
				throw ODB.ISourcesRowsetNotSupported();
			}
			value = null;
			int cPropertySets = 0;
			IntPtr ptrZero = ADP.PtrZero;
			Bid.Trace("<oledb.ISourcesRowset.GetSourcesRowset|API|OLEDB> IID_IRowset\n");
			OleDbHResult sourcesRowset2 = sourcesRowset.GetSourcesRowset(ADP.PtrZero, ODB.IID_IRowset, cPropertySets, ptrZero, out value);
			Bid.Trace("<oledb.ISourcesRowset.GetSourcesRowset|API|OLEDB|RET> %08X{HRESULT}\n", sourcesRowset2);
			Exception ex = OleDbConnection.ProcessResults(sourcesRowset2, null, null);
			if (ex != null)
			{
				throw ex;
			}
			OleDbDataReader oleDbDataReader = new OleDbDataReader(null, null, 0, CommandBehavior.Default);
			oleDbDataReader.InitializeIRowset(value, ChapterHandle.DB_NULL_HCHAPTER, ADP.RecordsUnaffected);
			oleDbDataReader.BuildMetaInfo();
			oleDbDataReader.HasRowsRead();
			return oleDbDataReader;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0027B048 File Offset: 0x0027A448
		public static OleDbDataReader GetRootEnumerator()
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbEnumerator.GetRootEnumerator|API>\n");
			OleDbDataReader enumeratorFromType;
			try
			{
				Type typeFromProgID = Type.GetTypeFromProgID("MSDAENUM", true);
				enumeratorFromType = OleDbEnumerator.GetEnumeratorFromType(typeFromProgID);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return enumeratorFromType;
		}
	}
}
