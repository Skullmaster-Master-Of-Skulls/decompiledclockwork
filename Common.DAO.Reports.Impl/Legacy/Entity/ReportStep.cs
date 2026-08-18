using System;
using System.Data;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000024 RID: 36
	public class ReportStep
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0002998B File Offset: 0x00027B8B
		public ReportStep()
		{
			this.Id = 0;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000299A4 File Offset: 0x00027BA4
		public eFunctionType FunctionCode
		{
			get
			{
				return this.functionCode;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000299BC File Offset: 0x00027BBC
		public string Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000299D4 File Offset: 0x00027BD4
		public ReportStep(eFunctionType functionCode, string parameters)
		{
			this.functionCode = functionCode;
			this.parameters = parameters;
			this.Id = 0;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000299FC File Offset: 0x00027BFC
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00029A14 File Offset: 0x00027C14
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00029A1E File Offset: 0x00027C1E
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00029A26 File Offset: 0x00027C26
		public int Id { get; set; }

		// Token: 0x06000284 RID: 644 RVA: 0x00029A30 File Offset: 0x00027C30
		public ReportStep(DataRow dr)
		{
			bool flag = dr.Table.Columns.Contains("searchinfofunctionid");
			if (flag)
			{
				this.Id = ((dr["searchinfofunctionid"] == DBNull.Value) ? 0 : ((int)dr["searchinfofunctionid"]));
			}
			else
			{
				this.Id = 0;
			}
			this.parameters = dr[3].ToString().Trim();
			this.functionCode = (eFunctionType)((int)dr[2]);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00029AC8 File Offset: 0x00027CC8
		public string FunctionName
		{
			get
			{
				return this.functionCode.ToString().Replace('_', ' ');
			}
		}

		// Token: 0x040000EE RID: 238
		private eFunctionType functionCode;

		// Token: 0x040000EF RID: 239
		private string parameters;

		// Token: 0x040000F0 RID: 240
		private string custom;

		// Token: 0x040000F1 RID: 241
		private string customSqlInjection;

		// Token: 0x040000F2 RID: 242
		private string customSqlInjectionOperator;

		// Token: 0x040000F3 RID: 243
		private int orderNum = 0;
	}
}
