using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace UnivOleDb22
{
	// Token: 0x0200000A RID: 10
	public class UnivDataReader
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004E7C File Offset: 0x00003E7C
		public UnivDataReader(dbName dbName, object dataReader)
		{
			this.dataReader = dataReader;
			this.myDbName = dbName;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004E94 File Offset: 0x00003E94
		public bool Read()
		{
			bool flag = this.dataReader == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				dbName dbName = this.myDbName;
				dbName dbName2 = dbName;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 != dbName.MSSQL)
					{
						result = false;
					}
					else
					{
						SqlDataReader sqlDataReader = (SqlDataReader)this.dataReader;
						result = sqlDataReader.Read();
					}
				}
				else
				{
					OleDbDataReader oleDbDataReader = (OleDbDataReader)this.dataReader;
					result = oleDbDataReader.Read();
				}
			}
			return result;
		}

		// Token: 0x17000012 RID: 18
		public object this[int index]
		{
			get
			{
				dbName dbName = this.myDbName;
				dbName dbName2 = dbName;
				object result;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 != dbName.MSSQL)
					{
						result = null;
					}
					else
					{
						SqlDataReader sqlDataReader = (SqlDataReader)this.dataReader;
						result = sqlDataReader[index];
					}
				}
				else
				{
					OleDbDataReader oleDbDataReader = (OleDbDataReader)this.dataReader;
					result = oleDbDataReader[index];
				}
				return result;
			}
		}

		// Token: 0x17000013 RID: 19
		public object this[string name]
		{
			get
			{
				dbName dbName = this.myDbName;
				dbName dbName2 = dbName;
				object result;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 != dbName.MSSQL)
					{
						result = null;
					}
					else
					{
						SqlDataReader sqlDataReader = (SqlDataReader)this.dataReader;
						result = sqlDataReader[name];
					}
				}
				else
				{
					OleDbDataReader oleDbDataReader = (OleDbDataReader)this.dataReader;
					result = oleDbDataReader[name];
				}
				return result;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004FAC File Offset: 0x00003FAC
		public void Close()
		{
			dbName dbName = this.myDbName;
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlDataReader sqlDataReader = (SqlDataReader)this.dataReader;
					sqlDataReader.Close();
				}
			}
			else
			{
				OleDbDataReader oleDbDataReader = (OleDbDataReader)this.dataReader;
				oleDbDataReader.Close();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004FF8 File Offset: 0x00003FF8
		public DataTable ToDataTable(DataTable t2, int[] colMapping)
		{
			DataTable dataTable = t2.Clone();
			ArrayList arrayList = this.ToItemArrays();
			foreach (object obj in arrayList)
			{
				object[] array = (object[])obj;
				object[] array2 = new object[t2.Columns.Count];
				for (int i = 0; i < array2.Length; i++)
				{
					bool flag = colMapping[i] >= 0;
					if (flag)
					{
						array2[i] = array[colMapping[i]];
					}
					else
					{
						array2[i] = null;
					}
				}
				dataTable.Rows.Add(array2);
			}
			arrayList.Clear();
			arrayList = null;
			return dataTable;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000050CC File Offset: 0x000040CC
		public DataTable ToDataTable()
		{
			DataTable dataTable = new DataTable();
			int fieldCount = this.FieldCount;
			ArrayList arrayList = this.ToItemArrays();
			for (int i = 0; i < fieldCount; i++)
			{
				bool flag = false;
				for (int j = 0; j < arrayList.Count; j++)
				{
					object[] array = (object[])arrayList[j];
					bool flag2 = array[j] != null;
					if (flag2)
					{
						dataTable.Columns.Add("c" + i.ToString(), array.GetType());
						flag = true;
						break;
					}
				}
				bool flag3 = !flag;
				if (flag3)
				{
					dataTable.Columns.Add("c" + i.ToString(), typeof(string));
				}
			}
			foreach (object obj in arrayList)
			{
				object[] values = (object[])obj;
				dataTable.Rows.Add(values);
			}
			arrayList.Clear();
			arrayList = null;
			return dataTable;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000520C File Offset: 0x0000420C
		public ArrayList ToItemArrays()
		{
			ArrayList arrayList = new ArrayList();
			dbName dbName = this.myDbName;
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlDataReader sqlDataReader = (SqlDataReader)this.dataReader;
					while (sqlDataReader.Read())
					{
						object[] array = new object[sqlDataReader.FieldCount];
						for (int i = 0; i < sqlDataReader.FieldCount; i++)
						{
							array[i] = sqlDataReader[i];
						}
						arrayList.Add(array);
					}
					sqlDataReader.Close();
				}
			}
			else
			{
				OleDbDataReader oleDbDataReader = (OleDbDataReader)this.dataReader;
				while (oleDbDataReader.Read())
				{
					object[] array = new object[oleDbDataReader.FieldCount];
					for (int j = 0; j < oleDbDataReader.FieldCount; j++)
					{
						array[j] = oleDbDataReader[j];
					}
					arrayList.Add(array);
				}
				oleDbDataReader.Close();
			}
			return arrayList;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005304 File Offset: 0x00004304
		public int FieldCount
		{
			get
			{
				dbName dbName = this.myDbName;
				dbName dbName2 = dbName;
				int result;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 != dbName.MSSQL)
					{
						result = 0;
					}
					else
					{
						SqlDataReader sqlDataReader = (SqlDataReader)this.dataReader;
						result = sqlDataReader.FieldCount;
					}
				}
				else
				{
					OleDbDataReader oleDbDataReader = (OleDbDataReader)this.dataReader;
					result = oleDbDataReader.FieldCount;
				}
				return result;
			}
		}

		// Token: 0x0400002C RID: 44
		private object dataReader;

		// Token: 0x0400002D RID: 45
		private dbName myDbName;
	}
}
