using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002A5 RID: 677
	internal enum OracleLpPseudoColumnExpressionType
	{
		// Token: 0x04001C02 RID: 7170
		UNDEFINED,
		// Token: 0x04001C03 RID: 7171
		CONNECT_BY_ISCYCLE,
		// Token: 0x04001C04 RID: 7172
		CONNECT_BY_ISLEAF,
		// Token: 0x04001C05 RID: 7173
		CONNECT_BY_ROOT,
		// Token: 0x04001C06 RID: 7174
		LEVEL,
		// Token: 0x04001C07 RID: 7175
		ROWNUM,
		// Token: 0x04001C08 RID: 7176
		ROWID,
		// Token: 0x04001C09 RID: 7177
		COLUMN_VALUE,
		// Token: 0x04001C0A RID: 7178
		OBJECT_ID,
		// Token: 0x04001C0B RID: 7179
		OBJECT_VALUE,
		// Token: 0x04001C0C RID: 7180
		ORA_ROWSCN,
		// Token: 0x04001C0D RID: 7181
		XMLDATA
	}
}
