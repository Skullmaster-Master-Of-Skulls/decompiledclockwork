using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001CF RID: 463
	internal abstract class OracleLpCollectionExpression : OracleLpStatementElement
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x000C3E8C File Offset: 0x000C208C
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.CollectionExpression;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x000C3E90 File Offset: 0x000C2090
		internal virtual OracleLpCollectionExpressionType Type
		{
			get
			{
				return OracleLpCollectionExpressionType.None;
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000C3E94 File Offset: 0x000C2094
		public OracleLpCollectionExpression(OracleLpStatementElement p) : base(p)
		{
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000C3EA0 File Offset: 0x000C20A0
		internal override void ToString(StringBuilder sb)
		{
		}
	}
}
