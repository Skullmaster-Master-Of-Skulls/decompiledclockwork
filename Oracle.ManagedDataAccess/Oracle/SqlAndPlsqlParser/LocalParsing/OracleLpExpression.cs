using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B5 RID: 693
	internal class OracleLpExpression : OracleLpStatementElement
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00109A90 File Offset: 0x00107C90
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.Expression;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00109A94 File Offset: 0x00107C94
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x00109A9C File Offset: 0x00107C9C
		public OracleLpExpressionType ExpressionType
		{
			get
			{
				return this.m_vExpressionType;
			}
			set
			{
				this.m_vExpressionType = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00109AA8 File Offset: 0x00107CA8
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x00109AB0 File Offset: 0x00107CB0
		public OracleLpExpression ParentExpression
		{
			get
			{
				return this.m_vParentExpression;
			}
			set
			{
				this.m_vParentExpression = value;
				base.Parent = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00109AC0 File Offset: 0x00107CC0
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x00109AC8 File Offset: 0x00107CC8
		public OracleLpTextFragment Text
		{
			get
			{
				return this.m_vText;
			}
			set
			{
				this.m_vText = value;
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00109AD4 File Offset: 0x00107CD4
		public OracleLpExpression(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00109AE0 File Offset: 0x00107CE0
		public virtual void EvaluateDatatype()
		{
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00109AE4 File Offset: 0x00107CE4
		public virtual IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			return OracleLpExpression.s_cEmptyExpressionList;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00109AEC File Offset: 0x00107CEC
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("  ExprType: ");
			sb.Append(this.ExpressionType);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00109B28 File Offset: 0x00107D28
		public override string ToString()
		{
			if (this.m_vText != null)
			{
				return this.m_vText.Fragment;
			}
			return null;
		}

		// Token: 0x04001C54 RID: 7252
		protected static IList<OracleLpExpression> s_cEmptyExpressionList = new List<OracleLpExpression>().AsReadOnly();

		// Token: 0x04001C55 RID: 7253
		protected IList<OracleLpExpression> m_vAllTerminalExpressions;

		// Token: 0x04001C56 RID: 7254
		protected OracleLpExpressionType m_vExpressionType;

		// Token: 0x04001C57 RID: 7255
		protected OracleLpExpression m_vParentExpression;

		// Token: 0x04001C58 RID: 7256
		protected OracleLpTextFragment m_vText;
	}
}
