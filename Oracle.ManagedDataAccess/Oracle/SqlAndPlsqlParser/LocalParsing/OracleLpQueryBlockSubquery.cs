using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F0 RID: 752
	internal class OracleLpQueryBlockSubquery : OracleLpSubquery
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0010CBB8 File Offset: 0x0010ADB8
		public override OracleLpSubqueryType SubqueryType
		{
			get
			{
				return OracleLpSubqueryType.QueryBlock;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0010CBBC File Offset: 0x0010ADBC
		public OracleLpQueryBlock QueryBlock
		{
			get
			{
				return this.m_vQueryBlock;
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0010CBC4 File Offset: 0x0010ADC4
		public OracleLpQueryBlockSubquery(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vQueryBlock = new OracleLpQueryBlock(this);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0010CBDC File Offset: 0x0010ADDC
		public override void Resolve()
		{
			this.m_vColumnDescriptors = this.m_vQueryBlock.ColumnDescriptors;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0010CBF0 File Offset: 0x0010ADF0
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vQueryBlock.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0010CC00 File Offset: 0x0010AE00
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			this.m_vQueryBlock.ToString(sb);
		}

		// Token: 0x04001D2A RID: 7466
		private OracleLpQueryBlock m_vQueryBlock;
	}
}
