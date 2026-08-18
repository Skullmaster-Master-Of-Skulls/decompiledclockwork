using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002EA RID: 746
	public enum OracleLpStatementClauseType
	{
		// Token: 0x04001CF6 RID: 7414
		Unknown,
		// Token: 0x04001CF7 RID: 7415
		ReturningClause,
		// Token: 0x04001CF8 RID: 7416
		WhereClause,
		// Token: 0x04001CF9 RID: 7417
		SelectList,
		// Token: 0x04001CFA RID: 7418
		SelectIntoList,
		// Token: 0x04001CFB RID: 7419
		OrderByClause,
		// Token: 0x04001CFC RID: 7420
		Declare,
		// Token: 0x04001CFD RID: 7421
		SequenceOfStatements,
		// Token: 0x04001CFE RID: 7422
		ExceptionHandlers,
		// Token: 0x04001CFF RID: 7423
		Subquery,
		// Token: 0x04001D00 RID: 7424
		ValuesClause,
		// Token: 0x04001D01 RID: 7425
		UpdateSetClause,
		// Token: 0x04001D02 RID: 7426
		CallIntoStatement,
		// Token: 0x04001D03 RID: 7427
		HierarchicalQueryClause,
		// Token: 0x04001D04 RID: 7428
		FromClause,
		// Token: 0x04001D05 RID: 7429
		HavingClause,
		// Token: 0x04001D06 RID: 7430
		CreateTable,
		// Token: 0x04001D07 RID: 7431
		CreatePlsql
	}
}
