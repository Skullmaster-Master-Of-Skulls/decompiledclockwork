using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E5 RID: 485
	internal interface IOracleLpAnsiOuterJoinClauseBase
	{
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060011E4 RID: 4580
		// (set) Token: 0x060011E5 RID: 4581
		bool Outer { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060011E6 RID: 4582
		// (set) Token: 0x060011E7 RID: 4583
		OracleLpOuterJoinType Type { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060011E8 RID: 4584
		// (set) Token: 0x060011E9 RID: 4585
		bool Natural { get; set; }
	}
}
