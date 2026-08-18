using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F6 RID: 502
	internal abstract class OracleLpTableReferenceAnsi : OracleLpStatementDataContainer
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x000C4CC8 File Offset: 0x000C2EC8
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.TableReference;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x000C4CCC File Offset: 0x000C2ECC
		public OracleLpTableReferenceAnsiType TableReferenceType
		{
			get
			{
				return this.m_vTableReferenceType;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600123E RID: 4670
		internal abstract List<OracleLpTablePrimary> TablePrimaryList { get; }

		// Token: 0x0600123F RID: 4671 RVA: 0x000C4CD4 File Offset: 0x000C2ED4
		public OracleLpTableReferenceAnsi(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x04001440 RID: 5184
		protected OracleLpTableReferenceAnsiType m_vTableReferenceType;

		// Token: 0x04001441 RID: 5185
		protected List<OracleLpTablePrimary> m_vTablePrimaryList;
	}
}
