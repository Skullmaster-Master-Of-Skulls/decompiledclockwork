using System;
using System.Data;

namespace ReportFunctions
{
	// Token: 0x02000003 RID: 3
	public class ReportStep
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		public ReportStep()
		{
			this.Id = 0;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000106C
		public FunctionCode FunctionCode
		{
			get
			{
				return this.functionCode;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00001084
		public string Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000209C File Offset: 0x0000109C
		public ReportStep(FunctionCode functionCode, string parameters)
		{
			this.functionCode = functionCode;
			this.parameters = parameters;
			this.Id = 0;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C4 File Offset: 0x000010C4
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000010DC
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E8 File Offset: 0x000010E8
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020FF File Offset: 0x000010FF
		public int Id { get; set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002108 File Offset: 0x00001108
		public ReportStep(DataRow dr)
		{
			if (dr.Table.Columns.Contains("searchinfofunctionid"))
			{
				this.Id = ((dr["searchinfofunctionid"] == DBNull.Value) ? 0 : ((int)dr["searchinfofunctionid"]));
			}
			else
			{
				this.Id = 0;
			}
			this.parameters = dr[3].ToString().Trim();
			this.functionCode = (FunctionCode)((int)dr[2]);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021A4 File Offset: 0x000011A4
		public string FunctionName
		{
			get
			{
				return this.functionCode.ToString().Replace('_', ' ');
			}
		}

		// Token: 0x04000082 RID: 130
		private FunctionCode functionCode;

		// Token: 0x04000083 RID: 131
		private string parameters;

		// Token: 0x04000084 RID: 132
		private string custom;

		// Token: 0x04000085 RID: 133
		private string customSqlInjection;

		// Token: 0x04000086 RID: 134
		private string customSqlInjectionOperator;

		// Token: 0x04000087 RID: 135
		private int orderNum = 0;
	}
}
