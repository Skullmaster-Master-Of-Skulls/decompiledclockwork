using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B8 RID: 696
	internal class OracleLpDatetimeExpression : OracleLpExpression
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00109C64 File Offset: 0x00107E64
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x00109C6C File Offset: 0x00107E6C
		public OracleLpExpression Datetime
		{
			get
			{
				return this.m_vDatetime;
			}
			set
			{
				this.m_vDatetime = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00109C78 File Offset: 0x00107E78
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x00109C80 File Offset: 0x00107E80
		public OracleLpDatetimeExpressionType DatetimeExpessionType
		{
			get
			{
				return this.m_vDatetimeExpessionType;
			}
			set
			{
				if (value != OracleLpDatetimeExpressionType.TIME_ZONE)
				{
					this.m_vTimeZone = null;
				}
				this.m_vDatetimeExpessionType = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00109C94 File Offset: 0x00107E94
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x00109C9C File Offset: 0x00107E9C
		public OracleLpTimeZoneExpression TimeZone
		{
			get
			{
				return this.m_vTimeZone;
			}
			set
			{
				this.m_vTimeZone = value;
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00109CA8 File Offset: 0x00107EA8
		public OracleLpDatetimeExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.DATETIME_EXPRESSION;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00109CB8 File Offset: 0x00107EB8
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00109CBC File Offset: 0x00107EBC
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = new List<OracleLpExpression>();
				this.m_vAllTerminalExpressions.Add(this.m_vDatetime);
				if (this.m_vTimeZone != null)
				{
					this.m_vAllTerminalExpressions.Add(this.m_vTimeZone);
				}
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00109D0C File Offset: 0x00107F0C
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  Datetime expression: ");
			this.m_vDatetime.ToString(sb);
			sb.Append("  Type: ");
			switch (this.m_vDatetimeExpessionType)
			{
			case OracleLpDatetimeExpressionType.LOCAL:
				sb.Append("LOCAL");
				return;
			case OracleLpDatetimeExpressionType.TIME_ZONE:
				sb.Append("TIME ZONE ");
				this.m_vTimeZone.ToString(sb);
				return;
			default:
				return;
			}
		}

		// Token: 0x04001C5B RID: 7259
		protected OracleLpExpression m_vDatetime;

		// Token: 0x04001C5C RID: 7260
		protected OracleLpDatetimeExpressionType m_vDatetimeExpessionType;

		// Token: 0x04001C5D RID: 7261
		protected OracleLpTimeZoneExpression m_vTimeZone;
	}
}
