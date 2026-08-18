using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000290 RID: 656
	internal class OracleLpAnalyticFunction
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x001092B8 File Offset: 0x001074B8
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x001092C0 File Offset: 0x001074C0
		public OracleLpAnalyticClause AnalyticClause
		{
			get
			{
				return this.m_vAnalyticClause;
			}
			set
			{
				this.m_vAnalyticClause = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x001092CC File Offset: 0x001074CC
		public List<OracleLpAnalyticFunctionArgument> Arguments
		{
			get
			{
				if (this.m_vArguments == null)
				{
					this.m_vArguments = new List<OracleLpAnalyticFunctionArgument>();
				}
				return this.m_vArguments;
			}
		}

		// Token: 0x04001B87 RID: 7047
		protected OracleLpAnalyticClause m_vAnalyticClause;

		// Token: 0x04001B88 RID: 7048
		protected List<OracleLpAnalyticFunctionArgument> m_vArguments;
	}
}
