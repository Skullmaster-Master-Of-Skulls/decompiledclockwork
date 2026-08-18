using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F8 RID: 760
	internal class OracleLpQteTableCollectionExpression : OracleLpQteNamedObject
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0010D1F8 File Offset: 0x0010B3F8
		public override OracleLpQueryTableExpressionType QueryTableExpressionType
		{
			get
			{
				return OracleLpQueryTableExpressionType.TableCollectionExpression;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0010D1FC File Offset: 0x0010B3FC
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0010D204 File Offset: 0x0010B404
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

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0010D214 File Offset: 0x0010B414
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0010D21C File Offset: 0x0010B41C
		public OracleLpCollectionExpression CollectionExpression
		{
			get
			{
				return this.m_vCollectionExpression;
			}
			set
			{
				this.m_vCollectionExpression = value;
				if (this.m_vCollectionExpression != null)
				{
					this.m_vCollectionExpression.Parent = this;
				}
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0010D23C File Offset: 0x0010B43C
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x0010D26C File Offset: 0x0010B46C
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

		// Token: 0x06001B42 RID: 6978 RVA: 0x0010D270 File Offset: 0x0010B470
		public OracleLpQteTableCollectionExpression(OracleLpStatementElement tr) : base(tr)
		{
		}

		// Token: 0x04001D3F RID: 7487
		protected OracleLpTextFragment m_vExpressionText;

		// Token: 0x04001D40 RID: 7488
		protected OracleLpCollectionExpression m_vCollectionExpression;
	}
}
