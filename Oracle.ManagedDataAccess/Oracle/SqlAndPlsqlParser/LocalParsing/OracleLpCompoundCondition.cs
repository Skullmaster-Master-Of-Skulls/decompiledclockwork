using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x0200029A RID: 666
	internal class OracleLpCompoundCondition : OracleLpCondition, IOracleLpBinaryOperations
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x001097A4 File Offset: 0x001079A4
		internal OracleLpCompoundCondition(string condition) : base(condition)
		{
			base.ConditionType = OracleLpConditionType.CompoundCondition;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x001097B4 File Offset: 0x001079B4
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x001097BC File Offset: 0x001079BC
		public OracleLpCondition LeftOperand { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x001097C8 File Offset: 0x001079C8
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x001097D0 File Offset: 0x001079D0
		public OracleLpCondition RightOperand { get; set; }
	}
}
