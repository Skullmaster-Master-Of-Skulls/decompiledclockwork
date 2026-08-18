using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002BF RID: 703
	internal class OracleLpPseudoColumnExpression : OracleLpSimpleExpression
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x0010A174 File Offset: 0x00108374
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x0010A17C File Offset: 0x0010837C
		public OracleLpPseudoColumnExpressionType PseudoColumnExpressionType
		{
			get
			{
				return this.m_vPseudoColumnExpressionType;
			}
			set
			{
				this.m_vPseudoColumnExpressionType = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0010A188 File Offset: 0x00108388
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x0010A190 File Offset: 0x00108390
		public OracleLpName ParentObjectName
		{
			get
			{
				return this.m_vParentObjectName;
			}
			set
			{
				this.m_vParentObjectName = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0010A19C File Offset: 0x0010839C
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x0010A1A4 File Offset: 0x001083A4
		public OracleLpName SchemaName
		{
			get
			{
				return this.m_vSchemaName;
			}
			set
			{
				this.m_vSchemaName = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0010A1B0 File Offset: 0x001083B0
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x0010A1B8 File Offset: 0x001083B8
		public OracleLpExpression Expression
		{
			get
			{
				return this.m_vExpression;
			}
			set
			{
				this.m_vExpression = value;
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0010A1C4 File Offset: 0x001083C4
		public OracleLpPseudoColumnExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vSimpleExpressionType = OracleLpSimpleExpressionType.PSEUDOCOLUMN;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0010A1D4 File Offset: 0x001083D4
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			if (this.m_vSchemaName == null)
			{
				sb.Append("  Schema: ").Append(this.m_vSchemaName);
			}
			if (this.m_vParentObjectName == null)
			{
				sb.Append("  Parent: ").Append(this.m_vParentObjectName);
			}
			sb.Append("  Type: ").Append(this.m_vPseudoColumnExpressionType);
		}

		// Token: 0x04001C68 RID: 7272
		protected OracleLpPseudoColumnExpressionType m_vPseudoColumnExpressionType;

		// Token: 0x04001C69 RID: 7273
		protected OracleLpName m_vParentObjectName;

		// Token: 0x04001C6A RID: 7274
		protected OracleLpName m_vSchemaName;

		// Token: 0x04001C6B RID: 7275
		protected OracleLpExpression m_vExpression;
	}
}
