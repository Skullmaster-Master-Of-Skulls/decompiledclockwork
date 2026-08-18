using System;
using System.Data;
using System.Data.SqlClient;

namespace ClockWorkWebAPI
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class TestType
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		public int TestTypeId
		{
			get
			{
				return this.testTypeId;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public TestType(int testTypeId, string description)
		{
			this.testTypeId = testTypeId;
			this.description = description;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		public TestType(DataRow dr)
		{
			this.testTypeId = (int)dr["testtypeid"];
			this.description = (string)dr["title"];
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000FF24 File Offset: 0x0000E124
		public static TestType[] LoadTestTypes(db conn)
		{
			SqlDataAdapter da = conn.Da;
			da.SelectCommand.CommandText = "SELECT \ttesttypeid,title,description FROM web_tests_testtypes WHERE\tisactive=1 ORDER BY title";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			bool flag = dataTable.Rows.Count > 0;
			TestType[] result;
			if (flag)
			{
				TestType[] array = new TestType[dataTable.Rows.Count];
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					DataRow dr = dataTable.Rows[i];
					TestType testType = new TestType(dr);
					array[i] = testType;
				}
				result = array;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0400013E RID: 318
		private int testTypeId;

		// Token: 0x0400013F RID: 319
		private string description;
	}
}
