using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace UnivOleDb.UnivSqlServer
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class UnivSqlServer_DataReader : UnivDataReader
	{
		// Token: 0x06000167 RID: 359 RVA: 0x000078E3 File Offset: 0x000068E3
		public UnivSqlServer_DataReader(SqlDataReader reader)
		{
			this.myDataReader = reader;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000078F4 File Offset: 0x000068F4
		public IDataReader GetNativeDataReader()
		{
			return this.myDataReader;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000790C File Offset: 0x0000690C
		public SqlDataReader DataReader
		{
			get
			{
				return this.myDataReader;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007924 File Offset: 0x00006924
		public bool Read()
		{
			bool flag = this.myDataReader == null;
			return !flag && this.myDataReader.Read();
		}

		// Token: 0x17000043 RID: 67
		public object this[int index]
		{
			get
			{
				return this.myDataReader[index];
			}
		}

		// Token: 0x17000044 RID: 68
		public object this[string name]
		{
			get
			{
				return this.myDataReader[name];
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007992 File Offset: 0x00006992
		public void Close()
		{
			this.myDataReader.Close();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000079A4 File Offset: 0x000069A4
		public ArrayList ToItemArrays()
		{
			ArrayList arrayList = new ArrayList();
			while (this.myDataReader.Read())
			{
				object[] array = new object[this.myDataReader.FieldCount];
				for (int i = 0; i < this.myDataReader.FieldCount; i++)
				{
					array[i] = this.myDataReader[i];
				}
				arrayList.Add(array);
			}
			this.myDataReader.Close();
			return arrayList;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007A24 File Offset: 0x00006A24
		public int FieldCount
		{
			get
			{
				return this.myDataReader.IsClosed ? 0 : this.myDataReader.FieldCount;
			}
		}

		// Token: 0x0400004D RID: 77
		private SqlDataReader myDataReader;
	}
}
