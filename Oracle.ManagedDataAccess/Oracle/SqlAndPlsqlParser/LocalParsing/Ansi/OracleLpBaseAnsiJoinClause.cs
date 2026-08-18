using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E0 RID: 480
	internal abstract class OracleLpBaseAnsiJoinClause : OracleLpStatementDataContainer
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x000C438C File Offset: 0x000C258C
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.JoinClause;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000C4390 File Offset: 0x000C2590
		internal virtual OracleLpJoinClauseConditionalType ConditionalType
		{
			get
			{
				return OracleLpJoinClauseConditionalType.None;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x000C4394 File Offset: 0x000C2594
		public virtual OracleLpJoinClauseType ClauseType
		{
			get
			{
				return OracleLpJoinClauseType.None;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060011C8 RID: 4552
		internal abstract List<OracleLpTablePrimary> TablePrimaryList { get; }

		// Token: 0x060011C9 RID: 4553 RVA: 0x000C4398 File Offset: 0x000C2598
		public OracleLpBaseAnsiJoinClause(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x0400141B RID: 5147
		protected List<OracleLpTablePrimary> m_vTablePrimaryList;
	}
}
