using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F4 RID: 500
	internal class OracleLpTablePrimaryJoinedTable : OracleLpTablePrimary
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x000C4C54 File Offset: 0x000C2E54
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x000C4C5C File Offset: 0x000C2E5C
		public OracleLpJoinedTable JoinedTable
		{
			get
			{
				return this.m_vJoinedTable;
			}
			set
			{
				this.m_vJoinedTable = value;
				if (this.m_vJoinedTable != null)
				{
					this.m_vJoinedTable.Parent = this;
				}
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x000C4C7C File Offset: 0x000C2E7C
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vJoinedTable.ColumnDescriptors;
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000C4C8C File Offset: 0x000C2E8C
		public OracleLpTablePrimaryJoinedTable(OracleLpStatementElement se) : base(se)
		{
			this.m_vTablePrimaryType = OracleLpTablePrimaryType.JoinedTable;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000C4C9C File Offset: 0x000C2E9C
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vJoinedTable.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000C4CAC File Offset: 0x000C2EAC
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}

		// Token: 0x0400143B RID: 5179
		protected OracleLpJoinedTable m_vJoinedTable;
	}
}
