using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000298 RID: 664
	internal interface IOracleLpBinaryOperations
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060019A6 RID: 6566
		// (set) Token: 0x060019A7 RID: 6567
		OracleLpCondition LeftOperand { get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060019A8 RID: 6568
		// (set) Token: 0x060019A9 RID: 6569
		OracleLpCondition RightOperand { get; set; }
	}
}
