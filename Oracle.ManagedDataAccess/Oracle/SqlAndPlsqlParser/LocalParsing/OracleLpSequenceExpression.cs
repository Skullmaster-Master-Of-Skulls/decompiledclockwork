using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002BD RID: 701
	internal class OracleLpSequenceExpression : OracleLpSimpleExpression
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0010A05C File Offset: 0x0010825C
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x0010A064 File Offset: 0x00108264
		public OracleLpSequenceExpressionType SequenceExpressionType
		{
			get
			{
				return this.m_vSequenceExpressionType;
			}
			set
			{
				this.m_vSequenceExpressionType = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0010A070 File Offset: 0x00108270
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x0010A078 File Offset: 0x00108278
		public string SequenceName
		{
			get
			{
				return this.m_vSequenceName;
			}
			set
			{
				this.m_vSequenceName = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0010A084 File Offset: 0x00108284
		// (set) Token: 0x06001A0F RID: 6671 RVA: 0x0010A08C File Offset: 0x0010828C
		public string SchemaName
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

		// Token: 0x06001A10 RID: 6672 RVA: 0x0010A098 File Offset: 0x00108298
		public OracleLpSequenceExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vSimpleExpressionType = OracleLpSimpleExpressionType.SEQUENCE;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0010A0A8 File Offset: 0x001082A8
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  Schema: ").Append((this.m_vSchemaName == null) ? "none" : this.m_vSchemaName);
			sb.Append("  Sequence: ").Append(this.m_vSequenceName);
			sb.Append(".").Append(this.m_vSequenceExpressionType);
		}

		// Token: 0x04001C64 RID: 7268
		protected OracleLpSequenceExpressionType m_vSequenceExpressionType;

		// Token: 0x04001C65 RID: 7269
		protected string m_vSequenceName;

		// Token: 0x04001C66 RID: 7270
		protected string m_vSchemaName;
	}
}
