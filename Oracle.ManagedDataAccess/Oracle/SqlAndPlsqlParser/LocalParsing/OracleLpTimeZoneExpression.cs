using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B9 RID: 697
	internal class OracleLpTimeZoneExpression : OracleLpExpression
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00109D84 File Offset: 0x00107F84
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x00109D8C File Offset: 0x00107F8C
		public OracleLpTimeZoneExpressionType TZExpressionType
		{
			get
			{
				return this.m_vTZExpressionType;
			}
			set
			{
				this.m_vTZExpressionType = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00109D98 File Offset: 0x00107F98
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00109DA0 File Offset: 0x00107FA0
		public OracleLpExpression TZExpression
		{
			get
			{
				return this.m_vTZExpression;
			}
			set
			{
				this.m_vTZExpression = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00109DAC File Offset: 0x00107FAC
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00109DB4 File Offset: 0x00107FB4
		public string TZLiteral
		{
			get
			{
				return this.m_vTZLiteral;
			}
			set
			{
				this.m_vTZLiteral = value;
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00109DC0 File Offset: 0x00107FC0
		public OracleLpTimeZoneExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.UNDEFINED;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00109DD0 File Offset: 0x00107FD0
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00109DD4 File Offset: 0x00107FD4
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = new List<OracleLpExpression>();
				OracleLpTimeZoneExpressionType vTZExpressionType = this.m_vTZExpressionType;
				if (vTZExpressionType == OracleLpTimeZoneExpressionType.EXPRESSION)
				{
					this.m_vAllTerminalExpressions.Add(this.m_vTZExpression);
				}
				else
				{
					this.m_vAllTerminalExpressions.Add(this);
				}
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00109E24 File Offset: 0x00108024
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  TIME ZONE: ");
			switch (this.m_vTZExpressionType)
			{
			case OracleLpTimeZoneExpressionType.DBTIMEZONE:
				sb.Append("DBTIMEZONE");
				return;
			case OracleLpTimeZoneExpressionType.EXPRESSION:
				this.m_vTZExpression.ToString(sb);
				return;
			case OracleLpTimeZoneExpressionType.STRING_LITERAL:
				sb.Append(this.m_vTZLiteral);
				return;
			default:
				return;
			}
		}

		// Token: 0x04001C5E RID: 7262
		protected OracleLpTimeZoneExpressionType m_vTZExpressionType;

		// Token: 0x04001C5F RID: 7263
		protected OracleLpExpression m_vTZExpression;

		// Token: 0x04001C60 RID: 7264
		protected string m_vTZLiteral;
	}
}
