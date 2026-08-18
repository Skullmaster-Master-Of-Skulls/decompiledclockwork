using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F9 RID: 761
	internal class OracleLpQteXMLTable : OracleLpQteNamedObject
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0010D27C File Offset: 0x0010B47C
		public override OracleLpQueryTableExpressionType QueryTableExpressionType
		{
			get
			{
				return OracleLpQueryTableExpressionType.XMLTable;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0010D280 File Offset: 0x0010B480
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x0010D288 File Offset: 0x0010B488
		public OracleLpTextFragment ExpressionText
		{
			get
			{
				return this.m_vExpressionText;
			}
			set
			{
				this.m_vExpressionText = value;
				this.m_vObjectName = null;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0010D298 File Offset: 0x0010B498
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0010D2C8 File Offset: 0x0010B4C8
		public override OracleLpName ObjectName
		{
			get
			{
				if (this.m_vExpressionText == null)
				{
					return null;
				}
				if (this.m_vObjectName == null)
				{
					this.m_vObjectName = new OracleLpName(this.m_vExpressionText.ToString());
				}
				return this.m_vObjectName;
			}
			set
			{
			}
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0010D2CC File Offset: 0x0010B4CC
		public OracleLpQteXMLTable(OracleLpStatementElement tr) : base(tr)
		{
		}

		// Token: 0x04001D41 RID: 7489
		protected OracleLpTextFragment m_vExpressionText;
	}
}
