using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F1 RID: 497
	internal class OracleLpTablePrimaryElementJsonTable : OracleLpTablePrimaryElement
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x000C4B80 File Offset: 0x000C2D80
		public override OracleLpTablePrimaryElementType TablePrimaryElementType
		{
			get
			{
				return OracleLpTablePrimaryElementType.JsonTable;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000C4B84 File Offset: 0x000C2D84
		public OracleLpTablePrimaryElementJsonTable(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000C4B90 File Offset: 0x000C2D90
		public override void Resolve()
		{
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000C4B94 File Offset: 0x000C2D94
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000C4B98 File Offset: 0x000C2D98
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}
	}
}
