using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F5 RID: 757
	internal abstract class OracleLpQueryTableExpression : OracleLpStatementDataContainer
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0010CDF4 File Offset: 0x0010AFF4
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.QueryTableExpression;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001B23 RID: 6947
		public abstract OracleLpQueryTableExpressionType QueryTableExpressionType { get; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0010CDF8 File Offset: 0x0010AFF8
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

		// Token: 0x06001B25 RID: 6949 RVA: 0x0010CE10 File Offset: 0x0010B010
		public OracleLpQueryTableExpression(OracleLpStatementElement tr) : base(tr)
		{
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0010CE1C File Offset: 0x0010B01C
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append("  ExpType: ");
			sb.Append(this.QueryTableExpressionType);
			sb.Append('\n');
		}

		// Token: 0x04001D39 RID: 7481
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
