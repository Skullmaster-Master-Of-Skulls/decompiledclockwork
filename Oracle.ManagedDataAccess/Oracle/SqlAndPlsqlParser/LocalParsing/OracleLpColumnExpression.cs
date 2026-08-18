using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002BC RID: 700
	internal class OracleLpColumnExpression : OracleLpSimpleExpression
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00109F8C File Offset: 0x0010818C
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x00109F94 File Offset: 0x00108194
		public bool Plus
		{
			get
			{
				return this.m_vPlus;
			}
			set
			{
				this.m_vPlus = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00109FA0 File Offset: 0x001081A0
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x00109FA8 File Offset: 0x001081A8
		public OracleLpColumn Column
		{
			get
			{
				return this.m_vColumn;
			}
			set
			{
				this.m_vColumn = value;
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00109FB4 File Offset: 0x001081B4
		public OracleLpColumnExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vSimpleExpressionType = OracleLpSimpleExpressionType.COLUMN;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00109FC4 File Offset: 0x001081C4
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00109FC8 File Offset: 0x001081C8
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			this.m_vColumn.ToString(sb);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00109FE0 File Offset: 0x001081E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			OracleLpName oracleLpName = this.m_vColumn.SchemaName;
			if (oracleLpName != null)
			{
				stringBuilder.Append(oracleLpName.DbName);
				stringBuilder.Append('.');
			}
			oracleLpName = this.m_vColumn.ParentObjectName;
			if (oracleLpName != null)
			{
				stringBuilder.Append(oracleLpName.DbName);
				stringBuilder.Append('.');
			}
			stringBuilder.Append(this.m_vColumn.Name.DbName);
			return stringBuilder.ToString();
		}

		// Token: 0x04001C62 RID: 7266
		protected bool m_vPlus;

		// Token: 0x04001C63 RID: 7267
		protected OracleLpColumn m_vColumn;
	}
}
