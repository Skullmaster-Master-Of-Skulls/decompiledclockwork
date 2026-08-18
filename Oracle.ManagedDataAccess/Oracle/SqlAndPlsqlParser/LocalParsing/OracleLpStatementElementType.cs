using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002EB RID: 747
	internal enum OracleLpStatementElementType
	{
		// Token: 0x04001D09 RID: 7433
		Statement,
		// Token: 0x04001D0A RID: 7434
		Subquery,
		// Token: 0x04001D0B RID: 7435
		QueryBlock,
		// Token: 0x04001D0C RID: 7436
		BindParameter,
		// Token: 0x04001D0D RID: 7437
		SelectClause,
		// Token: 0x04001D0E RID: 7438
		FromClause,
		// Token: 0x04001D0F RID: 7439
		SelectTerm,
		// Token: 0x04001D10 RID: 7440
		FromListTerm,
		// Token: 0x04001D11 RID: 7441
		TableReference,
		// Token: 0x04001D12 RID: 7442
		JoinClause,
		// Token: 0x04001D13 RID: 7443
		QueryTableExpression,
		// Token: 0x04001D14 RID: 7444
		Expression,
		// Token: 0x04001D15 RID: 7445
		Column,
		// Token: 0x04001D16 RID: 7446
		WithClause,
		// Token: 0x04001D17 RID: 7447
		SubqueryFactoringClause,
		// Token: 0x04001D18 RID: 7448
		SubqueryFactoringTerm,
		// Token: 0x04001D19 RID: 7449
		JoinedTable,
		// Token: 0x04001D1A RID: 7450
		TablePrimary,
		// Token: 0x04001D1B RID: 7451
		TablePrimaryElement,
		// Token: 0x04001D1C RID: 7452
		CollectionExpression
	}
}
