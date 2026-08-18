using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001F7 RID: 503
	internal class OracleLpTableReferenceAnsiTablePrimary : OracleLpTableReferenceAnsi
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x000C4CE0 File Offset: 0x000C2EE0
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vTablePrimary.ColumnDescriptors;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000C4CF0 File Offset: 0x000C2EF0
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x000C4CF8 File Offset: 0x000C2EF8
		public OracleLpTablePrimary TablePrimary
		{
			get
			{
				return this.m_vTablePrimary;
			}
			set
			{
				this.m_vTablePrimary = value;
				if (this.m_vTablePrimary != null)
				{
					this.m_vTablePrimary.Parent = this;
				}
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000C4D18 File Offset: 0x000C2F18
		internal override List<OracleLpTablePrimary> TablePrimaryList
		{
			get
			{
				if (this.m_vTablePrimaryList == null)
				{
					this.m_vTablePrimaryList = new List<OracleLpTablePrimary>(1);
					this.m_vTablePrimaryList.Add(this.m_vTablePrimary);
				}
				return this.m_vTablePrimaryList;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000C4D48 File Offset: 0x000C2F48
		public OracleLpTableReferenceAnsiTablePrimary(OracleLpStatementElement se) : base(se)
		{
			this.m_vTableReferenceType = OracleLpTableReferenceAnsiType.TablePrimary;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000C4D58 File Offset: 0x000C2F58
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000C4D74 File Offset: 0x000C2F74
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTablePrimary.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x04001442 RID: 5186
		protected OracleLpTablePrimary m_vTablePrimary;
	}
}
