using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000301 RID: 769
	internal class OracleLpColumnMappedQueryName
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0010D638 File Offset: 0x0010B838
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x0010D640 File Offset: 0x0010B840
		internal OracleLpName Name
		{
			get
			{
				return this.m_vName;
			}
			set
			{
				this.m_vName = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0010D64C File Offset: 0x0010B84C
		internal List<OracleLpName> ColumnAliases
		{
			get
			{
				return this.m_vColumnAliases;
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0010D65C File Offset: 0x0010B85C
		public void AddColumnAlias(string alias)
		{
			if (this.m_vColumnAliases == null)
			{
				this.m_vColumnAliases = new List<OracleLpName>(1);
			}
			this.m_vColumnAliases.Add(new OracleLpName(alias));
		}

		// Token: 0x04001D51 RID: 7505
		protected OracleLpName m_vName;

		// Token: 0x04001D52 RID: 7506
		protected List<OracleLpName> m_vColumnAliases;
	}
}
