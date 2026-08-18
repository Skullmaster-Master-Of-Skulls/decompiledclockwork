using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F0 RID: 496
	internal class OracleLpTablePrimaryElementContainers : OracleLpTablePrimaryElementQueryTableExpression
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000C4B54 File Offset: 0x000C2D54
		public override OracleLpTablePrimaryElementType TablePrimaryElementType
		{
			get
			{
				return OracleLpTablePrimaryElementType.ContainersClause;
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000C4B58 File Offset: 0x000C2D58
		public OracleLpTablePrimaryElementContainers(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x000C4B64 File Offset: 0x000C2D64
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}
	}
}
