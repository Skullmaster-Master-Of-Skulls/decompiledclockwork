using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F8 RID: 504
	internal class OracleLpTableReferenceAnsiJoinedTable : OracleLpTableReferenceAnsi
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x000C4D84 File Offset: 0x000C2F84
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vJoinedTable.ColumnDescriptors;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x000C4D94 File Offset: 0x000C2F94
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x000C4D9C File Offset: 0x000C2F9C
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

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x000C4DBC File Offset: 0x000C2FBC
		internal override List<OracleLpTablePrimary> TablePrimaryList
		{
			get
			{
				if (this.m_vTablePrimaryList == null)
				{
					this.m_vTablePrimaryList = new List<OracleLpTablePrimary>();
					this.m_vTablePrimaryList.AddRange(this.JoinedTable.TableReference.TablePrimaryList);
					this.m_vTablePrimaryList.AddRange(this.JoinedTable.JoinClause.TablePrimaryList);
				}
				return this.m_vTablePrimaryList;
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000C4E18 File Offset: 0x000C3018
		public OracleLpTableReferenceAnsiJoinedTable(OracleLpStatementElement se) : base(se)
		{
			this.m_vTableReferenceType = OracleLpTableReferenceAnsiType.JoinedTable;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x000C4E28 File Offset: 0x000C3028
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000C4E44 File Offset: 0x000C3044
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vJoinedTable.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x04001443 RID: 5187
		protected OracleLpJoinedTable m_vJoinedTable;
	}
}
