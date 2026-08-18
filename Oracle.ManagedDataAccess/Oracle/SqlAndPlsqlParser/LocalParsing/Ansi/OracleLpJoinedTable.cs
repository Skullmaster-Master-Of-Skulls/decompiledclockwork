using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001EB RID: 491
	internal class OracleLpJoinedTable : OracleLpStatementDataContainer
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x000C49C4 File Offset: 0x000C2BC4
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.JoinedTable;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000C49C8 File Offset: 0x000C2BC8
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000C49D0 File Offset: 0x000C2BD0
		public OracleLpTableReferenceAnsi TableReference
		{
			get
			{
				return this.m_vTableReference;
			}
			set
			{
				this.m_vTableReference = value;
				if (this.m_vTableReference != null)
				{
					this.m_vTableReference.Parent = this;
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x000C49F0 File Offset: 0x000C2BF0
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x000C49F8 File Offset: 0x000C2BF8
		public OracleLpBaseAnsiJoinClause JoinClause
		{
			get
			{
				return this.m_vJoinClause;
			}
			set
			{
				this.m_vJoinClause = value;
				if (this.m_vJoinClause != null)
				{
					this.m_vJoinClause.Parent = this;
				}
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000C4A18 File Offset: 0x000C2C18
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				if (this.m_vColumnDescriptors == null)
				{
					this.Resolve();
				}
				return this.m_vColumnDescriptors;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000C4A30 File Offset: 0x000C2C30
		public OracleLpJoinedTable(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000C4A3C File Offset: 0x000C2C3C
		public override void Resolve()
		{
			this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
			this.m_vColumnDescriptors.AddRange(this.m_vTableReference.ColumnDescriptors);
			this.m_vColumnDescriptors.AddRange(this.m_vJoinClause.ColumnDescriptors);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000C4A78 File Offset: 0x000C2C78
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTableReference.RetrieveNamedObjectReferences(statement);
			this.m_vJoinClause.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000C4A94 File Offset: 0x000C2C94
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}

		// Token: 0x0400142B RID: 5163
		protected OracleLpTableReferenceAnsi m_vTableReference;

		// Token: 0x0400142C RID: 5164
		protected OracleLpBaseAnsiJoinClause m_vJoinClause;

		// Token: 0x0400142D RID: 5165
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
