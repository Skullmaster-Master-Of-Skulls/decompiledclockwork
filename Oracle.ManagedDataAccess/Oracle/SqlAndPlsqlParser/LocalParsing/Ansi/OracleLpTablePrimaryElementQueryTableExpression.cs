using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001EF RID: 495
	internal class OracleLpTablePrimaryElementQueryTableExpression : OracleLpTablePrimaryElement
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x000C4AE0 File Offset: 0x000C2CE0
		public override OracleLpTablePrimaryElementType TablePrimaryElementType
		{
			get
			{
				return OracleLpTablePrimaryElementType.QueryTableExpression;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x000C4AE4 File Offset: 0x000C2CE4
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x000C4AEC File Offset: 0x000C2CEC
		public OracleLpQueryTableExpression QueryTableExpression
		{
			get
			{
				return this.m_vQueryTableExpression;
			}
			set
			{
				this.m_vQueryTableExpression = value;
				if (this.m_vQueryTableExpression != null)
				{
					this.m_vQueryTableExpression.Parent = this;
				}
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x000C4B0C File Offset: 0x000C2D0C
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vQueryTableExpression.ColumnDescriptors;
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000C4B1C File Offset: 0x000C2D1C
		public OracleLpTablePrimaryElementQueryTableExpression(OracleLpStatementElement se) : base(se)
		{
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000C4B28 File Offset: 0x000C2D28
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vQueryTableExpression.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000C4B38 File Offset: 0x000C2D38
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
		}

		// Token: 0x04001437 RID: 5175
		protected OracleLpQueryTableExpression m_vQueryTableExpression;
	}
}
