using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F3 RID: 499
	internal class OracleLpTablePrimaryTablePrimaryElement : OracleLpTablePrimary
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000C4BCC File Offset: 0x000C2DCC
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x000C4BD4 File Offset: 0x000C2DD4
		public OracleLpName Alias
		{
			get
			{
				return this.m_vAlias;
			}
			internal set
			{
				this.m_vAlias = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000C4BE0 File Offset: 0x000C2DE0
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x000C4BE8 File Offset: 0x000C2DE8
		public OracleLpTablePrimaryElement TablePrimaryElement
		{
			get
			{
				return this.m_vTablePrimaryElement;
			}
			set
			{
				this.m_vTablePrimaryElement = value;
				if (this.m_vTablePrimaryElement != null)
				{
					this.m_vTablePrimaryElement.Parent = this;
				}
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x000C4C08 File Offset: 0x000C2E08
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vTablePrimaryElement.ColumnDescriptors;
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000C4C18 File Offset: 0x000C2E18
		public OracleLpTablePrimaryTablePrimaryElement(OracleLpStatementElement se) : base(se)
		{
			this.m_vTablePrimaryType = OracleLpTablePrimaryType.TablePrimaryElement;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000C4C28 File Offset: 0x000C2E28
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTablePrimaryElement.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000C4C38 File Offset: 0x000C2E38
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}

		// Token: 0x04001439 RID: 5177
		protected OracleLpName m_vAlias;

		// Token: 0x0400143A RID: 5178
		protected OracleLpTablePrimaryElement m_vTablePrimaryElement;
	}
}
