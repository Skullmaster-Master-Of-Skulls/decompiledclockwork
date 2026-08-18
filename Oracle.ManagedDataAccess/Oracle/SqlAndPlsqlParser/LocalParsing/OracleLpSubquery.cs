using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002EF RID: 751
	internal abstract class OracleLpSubquery : OracleLpStatementDataContainer
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0010CB48 File Offset: 0x0010AD48
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.Subquery;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001B06 RID: 6918
		public abstract OracleLpSubqueryType SubqueryType { get; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x0010CB4C File Offset: 0x0010AD4C
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

		// Token: 0x06001B08 RID: 6920 RVA: 0x0010CB64 File Offset: 0x0010AD64
		public OracleLpSubquery(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0010CB70 File Offset: 0x0010AD70
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("Subquery Type:");
			sb.Append(this.SubqueryType.ToString());
			sb.Append('\n');
		}

		// Token: 0x04001D29 RID: 7465
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
