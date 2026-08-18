using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001EE RID: 494
	internal abstract class OracleLpTablePrimaryElement : OracleLpStatementDataContainer
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x000C4AB0 File Offset: 0x000C2CB0
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.TablePrimaryElement;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x000C4AB4 File Offset: 0x000C2CB4
		public virtual OracleLpTablePrimaryElementType TablePrimaryElementType
		{
			get
			{
				return OracleLpTablePrimaryElementType.None;
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000C4AB8 File Offset: 0x000C2CB8
		public OracleLpTablePrimaryElement(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000C4AC4 File Offset: 0x000C2CC4
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}
	}
}
