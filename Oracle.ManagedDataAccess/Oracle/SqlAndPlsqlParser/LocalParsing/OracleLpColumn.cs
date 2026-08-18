using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000295 RID: 661
	internal class OracleLpColumn : OracleLpStatementElement
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0010939C File Offset: 0x0010759C
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.Column;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x001093A0 File Offset: 0x001075A0
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x001093A8 File Offset: 0x001075A8
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x001093B4 File Offset: 0x001075B4
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x001093BC File Offset: 0x001075BC
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x001093C8 File Offset: 0x001075C8
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x001093D0 File Offset: 0x001075D0
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

		// Token: 0x06001984 RID: 6532 RVA: 0x001093DC File Offset: 0x001075DC
		public OracleLpColumn(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x001093E8 File Offset: 0x001075E8
		internal override void ToString(StringBuilder sb)
		{
			sb.Append("  Name: ").Append(this.m_vName);
			sb.Append("  Parent: ").Append(this.m_vParentObjectName);
			sb.Append("  Schema: ").Append(this.m_vSchemaName);
		}

		// Token: 0x04001B90 RID: 7056
		protected OracleLpName m_vName;

		// Token: 0x04001B91 RID: 7057
		protected OracleLpName m_vParentObjectName;

		// Token: 0x04001B92 RID: 7058
		protected OracleLpName m_vSchemaName;
	}
}
