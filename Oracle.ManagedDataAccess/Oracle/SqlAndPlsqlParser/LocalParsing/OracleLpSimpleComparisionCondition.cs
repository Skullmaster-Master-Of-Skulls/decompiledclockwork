using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000299 RID: 665
	internal class OracleLpSimpleComparisionCondition : OracleLpCondition, IOracleLpBinaryOperations
	{
		// Token: 0x060019AA RID: 6570 RVA: 0x0010976C File Offset: 0x0010796C
		internal OracleLpSimpleComparisionCondition(string condition) : base(condition)
		{
			base.ConditionType = OracleLpConditionType.SimpleComparisionCondition;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0010977C File Offset: 0x0010797C
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x00109784 File Offset: 0x00107984
		public OracleLpCondition LeftOperand { get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x00109790 File Offset: 0x00107990
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x00109798 File Offset: 0x00107998
		public OracleLpCondition RightOperand { get; set; }
	}
}
