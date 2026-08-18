using System;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data.OleDb
{
	// Token: 0x0200024D RID: 589
	public sealed class OleDbEnumerator
	{
		// Token: 0x06002590 RID: 9616 RVA: 0x00100440 File Offset: 0x000FF840
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

		// Token: 0x06002591 RID: 9617 RVA: 0x00100484 File Offset: 0x000FF884
		public static OleDbDataReader GetEnumerator(Type type)
		{
			OleDbConnection.ExecutePermission.Demand();
			return OleDbEnumerator.GetEnumeratorFromType(type);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x001004A4 File Offset: 0x000FF8A4
		internal static OleDbDataReader GetEnumeratorFromType(Type type)
		{
			object value = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
			return OleDbEnumerator.GetEnumeratorReader(value);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x001004C8 File Offset: 0x000FF8C8
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

		// Token: 0x06002594 RID: 9620 RVA: 0x00100580 File Offset: 0x000FF980
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
