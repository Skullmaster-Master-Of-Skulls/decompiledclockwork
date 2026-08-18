using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace UnivOleDb.UnivMSAccess
{
	// Token: 0x02000025 RID: 37
	public class UnivMSAccess_DataReader : UnivDataReader
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00008FFF File Offset: 0x00007FFF
		public UnivMSAccess_DataReader(OleDbDataReader reader)
		{
			this.myDataReader = reader;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009010 File Offset: 0x00008010
		public OleDbDataReader DataReader
		{
			get
			{
				return this.myDataReader;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009028 File Offset: 0x00008028
		public IDataReader GetNativeDataReader()
		{
			return this.myDataReader;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009040 File Offset: 0x00008040
		public bool Read()
		{
			bool flag = this.myDataReader == null;
			return !flag && this.myDataReader.Read();
		}

		// Token: 0x1700005F RID: 95
		public object this[int index]
		{
			get
			{
				return this.myDataReader[index];
			}
		}

		// Token: 0x17000060 RID: 96
		public object this[string name]
		{
			get
			{
				return this.myDataReader[name];
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000090AE File Offset: 0x000080AE
		public void Close()
		{
			this.myDataReader.Close();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000090C0 File Offset: 0x000080C0
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00009140 File Offset: 0x00008140
		public int FieldCount
		{
			get
			{
				return this.myDataReader.FieldCount;
			}
		}

		// Token: 0x0400006D RID: 109
		private OleDbDataReader myDataReader;
	}
}
