using System;

namespace TechnoPro.Common.DataTables.Entities
{
	// Token: 0x0200000A RID: 10
	public class TableJoinSingle
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public TableJoinSingle()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public TableJoinSingle(string table1, string col1, string table2, string col2, string newTableName, string[] columnsToPull)
		{
			this.Info1 = new JoinTableColumnInfo
			{
				TableName = table1,
				ColumnName = col1
			};
			this.Info2 = new JoinTableColumnInfo
			{
				TableName = table2,
				ColumnName = col2
			};
			this.NewTableName = newTableName;
			this.ColumnsToPull = columnsToPull;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002D46 File Offset: 0x00000F46
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002D4E File Offset: 0x00000F4E
		public string NewTableName { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D57 File Offset: 0x00000F57
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002D5F File Offset: 0x00000F5F
		public string[] ColumnsToPull { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D68 File Offset: 0x00000F68
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002D70 File Offset: 0x00000F70
		public JoinTableColumnInfo Info1 { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002D79 File Offset: 0x00000F79
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002D81 File Offset: 0x00000F81
		public JoinTableColumnInfo Info2 { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002D8A File Offset: 0x00000F8A
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002DAE File Offset: 0x00000FAE
		public string Table1Name
		{
			get
			{
				string result;
				if (this.Info1 != null)
				{
					if ((result = this.Info1.TableName) == null)
					{
						return "";
					}
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				if (this.Info1 == null)
				{
					this.Info1 = new JoinTableColumnInfo();
				}
				this.Info1.TableName = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002DCF File Offset: 0x00000FCF
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002DF3 File Offset: 0x00000FF3
		public string Table2Name
		{
			get
			{
				string result;
				if (this.Info2 != null)
				{
					if ((result = this.Info2.TableName) == null)
					{
						return "";
					}
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				if (this.Info2 == null)
				{
					this.Info2 = new JoinTableColumnInfo();
				}
				this.Info2.TableName = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002E38 File Offset: 0x00001038
		public string JoinCol1Name
		{
			get
			{
				string result;
				if (this.Info1 != null)
				{
					if ((result = this.Info1.ColumnName) == null)
					{
						return "";
					}
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				if (this.Info1 == null)
				{
					this.Info1 = new JoinTableColumnInfo();
				}
				this.Info1.ColumnName = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002E59 File Offset: 0x00001059
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002E7D File Offset: 0x0000107D
		public string JoinCol2Name
		{
			get
			{
				string result;
				if (this.Info2 != null)
				{
					if ((result = this.Info2.ColumnName) == null)
					{
						return "";
					}
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				if (this.Info2 == null)
				{
					this.Info2 = new JoinTableColumnInfo();
				}
				this.Info2.ColumnName = value;
			}
		}
	}
}
