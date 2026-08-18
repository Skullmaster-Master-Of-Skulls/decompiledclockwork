using System;
using System.Data;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200024E RID: 590
	public class OracleQueryTypeAttribute : Attribute
	{
		// Token: 0x060011DC RID: 4572 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public OracleQueryTypeAttribute()
		{
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00018604 File Offset: 0x00016804
		public OracleQueryTypeAttribute(CommandType commandType)
		{
			this.CommandType = commandType;
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00018616 File Offset: 0x00016816
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0001861E File Offset: 0x0001681E
		public CommandType CommandType { get; set; }
	}
}
