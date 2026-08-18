using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000294 RID: 660
	public sealed class OracleLpBindParameter : OracleLpStatementElement
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00109300 File Offset: 0x00107500
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.BindParameter;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00109304 File Offset: 0x00107504
		public OracleLpBindParameterType ParameterType
		{
			get
			{
				return this.m_vType;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0010930C File Offset: 0x0010750C
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x00109314 File Offset: 0x00107514
		public OracleLpTextFragment Name
		{
			get
			{
				return this.m_vName;
			}
			internal set
			{
				this.m_vName = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00109320 File Offset: 0x00107520
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x00109328 File Offset: 0x00107528
		public int Position
		{
			get
			{
				return this.m_vPosition;
			}
			internal set
			{
				this.m_vPosition = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00109334 File Offset: 0x00107534
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0010933C File Offset: 0x0010753C
		public OracleLpStatementClauseType ParentClause
		{
			get
			{
				return this.m_vParentClause;
			}
			internal set
			{
				this.m_vParentClause = value;
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00109348 File Offset: 0x00107548
		internal OracleLpBindParameter(OracleLpStatement parent, OracleLpRelativeTextFragment name, int position, OracleLpBindParameterType type) : base(parent)
		{
			this.m_vName = name;
			this.m_vPosition = position;
			this.m_vType = type;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00109370 File Offset: 0x00107570
		public override string ToString()
		{
			return string.Format("\tParameter name: {0}, position: {1}, parent clause: {2}\n", this.m_vName, this.m_vPosition, this.m_vParentClause);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00109398 File Offset: 0x00107598
		internal override void ToString(StringBuilder sb)
		{
		}

		// Token: 0x04001B8C RID: 7052
		private OracleLpBindParameterType m_vType;

		// Token: 0x04001B8D RID: 7053
		private OracleLpTextFragment m_vName;

		// Token: 0x04001B8E RID: 7054
		private int m_vPosition = -1;

		// Token: 0x04001B8F RID: 7055
		private OracleLpStatementClauseType m_vParentClause;
	}
}
