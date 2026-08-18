using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002A0 RID: 672
	public enum OracleLpPseudoColumnType
	{
		// Token: 0x04001BD3 RID: 7123
		Unknown,
		// Token: 0x04001BD4 RID: 7124
		CONNECT_BY_ISCYCLE,
		// Token: 0x04001BD5 RID: 7125
		CONNECT_BY_ISLEAF,
		// Token: 0x04001BD6 RID: 7126
		CONNECT_BY_ROOT,
		// Token: 0x04001BD7 RID: 7127
		LEVEL,
		// Token: 0x04001BD8 RID: 7128
		ROWNUM,
		// Token: 0x04001BD9 RID: 7129
		ROWID,
		// Token: 0x04001BDA RID: 7130
		COLUMN_VALUE,
		// Token: 0x04001BDB RID: 7131
		OBJECT_ID,
		// Token: 0x04001BDC RID: 7132
		OBJECT_VALUE,
		// Token: 0x04001BDD RID: 7133
		ORA_ROWSCN,
		// Token: 0x04001BDE RID: 7134
		XMLDATA
	}
}
