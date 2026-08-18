using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000296 RID: 662
	public sealed class OracleLpColumnDescriptor
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0010943C File Offset: 0x0010763C
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x00109444 File Offset: 0x00107644
		public bool IsAliased
		{
			get
			{
				return this.m_vIsAliased;
			}
			set
			{
				this.m_vIsAliased = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x00109450 File Offset: 0x00107650
		public bool IsExpression
		{
			get
			{
				return this.m_vColumnType == OracleLpColumnType.Expression;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x0010945C File Offset: 0x0010765C
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x00109464 File Offset: 0x00107664
		public bool IsHidden
		{
			get
			{
				return this.m_vIsHidden;
			}
			set
			{
				this.m_vIsHidden = value;
				this.m_vIsShowing = !this.m_vIsHidden;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x0010947C File Offset: 0x0010767C
		public bool IsReadOnly
		{
			get
			{
				return this.m_vColumnType != OracleLpColumnType.Column;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0010948C File Offset: 0x0010768C
		public bool IsRowID
		{
			get
			{
				return this.m_vColumnType == OracleLpColumnType.PseudoColumn && this.m_vPseudoColumnType == OracleLpPseudoColumnType.ROWID;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x001094A4 File Offset: 0x001076A4
		// (set) Token: 0x0600198E RID: 6542 RVA: 0x001094AC File Offset: 0x001076AC
		public bool IsShowing
		{
			get
			{
				return this.m_vIsShowing;
			}
			set
			{
				this.m_vIsShowing = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x001094B8 File Offset: 0x001076B8
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x001094C0 File Offset: 0x001076C0
		public OracleLpColumnType ColumnType
		{
			get
			{
				return this.m_vColumnType;
			}
			set
			{
				this.m_vColumnType = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x001094CC File Offset: 0x001076CC
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x001094D4 File Offset: 0x001076D4
		public OracleLpPseudoColumnType PseudoColumnType
		{
			get
			{
				return this.m_vPseudoColumnType;
			}
			set
			{
				this.m_vPseudoColumnType = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x001094E0 File Offset: 0x001076E0
		public List<OracleLpBindParameter> BindReferences
		{
			get
			{
				return this.m_vBindReferences;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x001094E8 File Offset: 0x001076E8
		public bool HasBindReferences
		{
			get
			{
				return this.m_vBindReferences != null && this.m_vBindReferences.Count != 0;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x00109508 File Offset: 0x00107708
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x00109510 File Offset: 0x00107710
		public OracleLpName ColumnName
		{
			get
			{
				return this.m_vColumnName;
			}
			set
			{
				this.m_vColumnName = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x0010951C File Offset: 0x0010771C
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x00109524 File Offset: 0x00107724
		public OracleLpName BaseColumnName
		{
			get
			{
				return this.m_vBaseColumnName;
			}
			set
			{
				this.m_vBaseColumnName = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00109530 File Offset: 0x00107730
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x00109538 File Offset: 0x00107738
		public OracleLpName BaseTableName
		{
			get
			{
				return this.m_vBaseTableName;
			}
			set
			{
				this.m_vBaseTableName = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00109544 File Offset: 0x00107744
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x0010954C File Offset: 0x0010774C
		public OracleLpName BaseSchemaName
		{
			get
			{
				return this.m_vBaseSchemaName;
			}
			set
			{
				this.m_vBaseSchemaName = value;
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00109558 File Offset: 0x00107758
		internal OracleLpColumnDescriptor()
		{
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00109568 File Offset: 0x00107768
		internal OracleLpColumnDescriptor(OracleLpColumnDescriptor desc)
		{
			this.m_vPseudoColumnType = desc.m_vPseudoColumnType;
			this.m_vIsHidden = desc.m_vIsHidden;
			this.m_vIsShowing = desc.m_vIsShowing;
			this.m_vIsAliased = desc.m_vIsAliased;
			this.m_vColumnType = desc.m_vColumnType;
			this.m_vColumnName = desc.m_vColumnName;
			this.m_vBaseTableName = desc.m_vBaseTableName;
			this.m_vBaseSchemaName = desc.m_vBaseSchemaName;
			this.m_vBaseColumnName = desc.m_vBaseColumnName;
			this.m_vBindReferences = desc.m_vBindReferences;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x001095FC File Offset: 0x001077FC
		public void AddBindReference(OracleLpBindParameter bp)
		{
			if (this.m_vBindReferences == null)
			{
				this.m_vBindReferences = new List<OracleLpBindParameter>();
			}
			this.m_vBindReferences.Add(bp);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00109620 File Offset: 0x00107820
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(string.Format("ColumnName: {0}, BaseColumnName: {1}, BaseTableName: {2}, BaseSchemaName: {3}\n", new object[]
			{
				this.m_vColumnName.DbName,
				(this.m_vBaseColumnName == null) ? "null" : (this.m_vBaseColumnName.DbName ?? "null"),
				(this.m_vBaseTableName == null) ? "null" : (this.m_vBaseTableName.DbName ?? "null"),
				(this.m_vBaseSchemaName == null) ? "null" : (this.m_vBaseSchemaName.DbName ?? "null")
			}));
			if (this.m_vBindReferences != null)
			{
				stringBuilder.Append("Bind references:\n");
				foreach (OracleLpBindParameter oracleLpBindParameter in this.m_vBindReferences)
				{
					stringBuilder.Append(oracleLpBindParameter.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001B93 RID: 7059
		private bool m_vIsAliased;

		// Token: 0x04001B94 RID: 7060
		private bool m_vIsHidden;

		// Token: 0x04001B95 RID: 7061
		private bool m_vIsShowing = true;

		// Token: 0x04001B96 RID: 7062
		private OracleLpColumnType m_vColumnType;

		// Token: 0x04001B97 RID: 7063
		private OracleLpPseudoColumnType m_vPseudoColumnType;

		// Token: 0x04001B98 RID: 7064
		private List<OracleLpBindParameter> m_vBindReferences;

		// Token: 0x04001B99 RID: 7065
		private OracleLpName m_vColumnName;

		// Token: 0x04001B9A RID: 7066
		private OracleLpName m_vBaseColumnName;

		// Token: 0x04001B9B RID: 7067
		private OracleLpName m_vBaseTableName;

		// Token: 0x04001B9C RID: 7068
		private OracleLpName m_vBaseSchemaName;
	}
}
