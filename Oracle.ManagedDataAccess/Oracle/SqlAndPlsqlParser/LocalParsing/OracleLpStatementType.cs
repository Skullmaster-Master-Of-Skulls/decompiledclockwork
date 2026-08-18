using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x0200029D RID: 669
	public enum OracleLpStatementType
	{
		// Token: 0x04001BB7 RID: 7095
		Unknown,
		// Token: 0x04001BB8 RID: 7096
		Create,
		// Token: 0x04001BB9 RID: 7097
		Insert,
		// Token: 0x04001BBA RID: 7098
		Update,
		// Token: 0x04001BBB RID: 7099
		Delete,
		// Token: 0x04001BBC RID: 7100
		Merge,
		// Token: 0x04001BBD RID: 7101
		Select,
		// Token: 0x04001BBE RID: 7102
		BlockStatement,
		// Token: 0x04001BBF RID: 7103
		Call,
		// Token: 0x04001BC0 RID: 7104
		ExplainPlan,
		// Token: 0x04001BC1 RID: 7105
		Execute,
		// Token: 0x04001BC2 RID: 7106
		Fetch,
		// Token: 0x04001BC3 RID: 7107
		Close,
		// Token: 0x04001BC4 RID: 7108
		Open,
		// Token: 0x04001BC5 RID: 7109
		OpenCursor
	}
}
