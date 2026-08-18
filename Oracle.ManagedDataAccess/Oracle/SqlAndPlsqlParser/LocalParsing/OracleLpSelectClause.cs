using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002E6 RID: 742
	internal class OracleLpSelectClause : OracleLpStatementElement, IOracleLpColumnDescriptorContainer
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0010C7A8 File Offset: 0x0010A9A8
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.SelectClause;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0010C7AC File Offset: 0x0010A9AC
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x0010C7B4 File Offset: 0x0010A9B4
		public OracleLpSelectionType SelectionType
		{
			get
			{
				return this.m_vSelectionType;
			}
			set
			{
				this.m_vSelectionType = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0010C7C0 File Offset: 0x0010A9C0
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0010C7C8 File Offset: 0x0010A9C8
		public bool BulkCollect
		{
			get
			{
				return this.m_vBulkCollect;
			}
			set
			{
				this.m_vBulkCollect = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0010C7D4 File Offset: 0x0010A9D4
		public List<OracleLpSelectTerm> SelectList
		{
			get
			{
				return this.m_vSelectList;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0010C7DC File Offset: 0x0010A9DC
		public List<OracleLpName> IntoList
		{
			get
			{
				return this.m_vIntoList;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0010C7E4 File Offset: 0x0010A9E4
		public List<OracleLpColumnDescriptor> ColumnDescriptors
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

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0010C7FC File Offset: 0x0010A9FC
		public OracleLpSelectClause(OracleLpQueryBlock parent) : base(parent)
		{
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0010C81C File Offset: 0x0010AA1C
		public virtual void Resolve()
		{
			this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
			foreach (OracleLpSelectTerm oracleLpSelectTerm in this.m_vSelectList)
			{
				this.m_vColumnDescriptors.AddRange(oracleLpSelectTerm.ColumnDescriptors);
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0010C884 File Offset: 0x0010AA84
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("Select List:\n");
			this.m_vSelectList.ForEach(delegate(OracleLpSelectTerm st)
			{
				st.ToString(sb);
			});
		}

		// Token: 0x04001CEC RID: 7404
		protected OracleLpSelectionType m_vSelectionType;

		// Token: 0x04001CED RID: 7405
		protected bool m_vBulkCollect;

		// Token: 0x04001CEE RID: 7406
		protected List<OracleLpSelectTerm> m_vSelectList = new List<OracleLpSelectTerm>();

		// Token: 0x04001CEF RID: 7407
		protected List<OracleLpName> m_vIntoList = new List<OracleLpName>();

		// Token: 0x04001CF0 RID: 7408
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
