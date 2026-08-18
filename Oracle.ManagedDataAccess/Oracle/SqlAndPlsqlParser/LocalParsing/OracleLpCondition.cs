using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000297 RID: 663
	internal class OracleLpCondition
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0010972C File Offset: 0x0010792C
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x00109734 File Offset: 0x00107934
		internal string ConditionString { get; private set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00109740 File Offset: 0x00107940
		// (set) Token: 0x060019A4 RID: 6564 RVA: 0x00109748 File Offset: 0x00107948
		protected internal OracleLpConditionType ConditionType { get; set; }

		// Token: 0x060019A5 RID: 6565 RVA: 0x00109754 File Offset: 0x00107954
		public OracleLpCondition(string condition)
		{
			this.ConditionString = condition;
			this.Operation = OracleLpConditionOperation.NONE;
		}

		// Token: 0x04001B9D RID: 7069
		internal OracleLpConditionOperation Operation;
	}
}
