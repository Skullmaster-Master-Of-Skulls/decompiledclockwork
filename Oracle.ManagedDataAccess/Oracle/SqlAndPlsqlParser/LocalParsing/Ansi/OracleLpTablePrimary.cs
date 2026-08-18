using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F2 RID: 498
	internal abstract class OracleLpTablePrimary : OracleLpStatementDataContainer
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x000C4BB4 File Offset: 0x000C2DB4
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.TablePrimary;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000C4BB8 File Offset: 0x000C2DB8
		public OracleLpTablePrimaryType TablePrimaryType
		{
			get
			{
				return this.m_vTablePrimaryType;
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000C4BC0 File Offset: 0x000C2DC0
		public OracleLpTablePrimary(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x04001438 RID: 5176
		protected OracleLpTablePrimaryType m_vTablePrimaryType;
	}
}
