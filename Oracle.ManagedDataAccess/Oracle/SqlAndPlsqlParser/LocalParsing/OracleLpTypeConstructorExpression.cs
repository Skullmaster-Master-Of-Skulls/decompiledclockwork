using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C4 RID: 708
	internal class OracleLpTypeConstructorExpression : OracleLpExpression
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0010A3F8 File Offset: 0x001085F8
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x0010A400 File Offset: 0x00108600
		public OracleLpName Name
		{
			get
			{
				return this.m_vName;
			}
			set
			{
				this.m_vName = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0010A40C File Offset: 0x0010860C
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x0010A414 File Offset: 0x00108614
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0010A420 File Offset: 0x00108620
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x0010A428 File Offset: 0x00108628
		public bool New
		{
			get
			{
				return this.m_vNew;
			}
			set
			{
				this.m_vNew = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0010A434 File Offset: 0x00108634
		public List<OracleLpExpression> Parameters
		{
			get
			{
				return this.m_vParameters;
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0010A43C File Offset: 0x0010863C
		public OracleLpTypeConstructorExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.TYPE_CONSTRUCTOR_EXPRESSION;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0010A450 File Offset: 0x00108650
		public void CreateParametersList()
		{
			if (this.m_vParameters == null)
			{
				this.m_vParameters = new List<OracleLpExpression>();
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0010A468 File Offset: 0x00108668
		public void ParametersChanged()
		{
			this.m_vAllTerminalExpressions = null;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0010A474 File Offset: 0x00108674
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				List<OracleLpExpression> list = new List<OracleLpExpression>();
				this.m_vAllTerminalExpressions = list;
				if (this.m_vParameters != null)
				{
					foreach (OracleLpExpression oracleLpExpression in this.m_vParameters)
					{
						list.AddRange(oracleLpExpression.GetAllTerminalExpressions());
					}
				}
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x04001C73 RID: 7283
		protected OracleLpName m_vName;

		// Token: 0x04001C74 RID: 7284
		protected OracleLpName m_vParentObjectName;

		// Token: 0x04001C75 RID: 7285
		protected bool m_vNew;

		// Token: 0x04001C76 RID: 7286
		protected List<OracleLpExpression> m_vParameters;
	}
}
