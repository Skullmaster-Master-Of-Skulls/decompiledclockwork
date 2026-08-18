using System;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.BinXml
{
	// Token: 0x0200002D RID: 45
	internal class ObxmlTokenManagerContext
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000CF68 File Offset: 0x0000B168
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000CF70 File Offset: 0x0000B170
		private string[] m_TokenMgrStringIds { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000CF7C File Offset: 0x0000B17C
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000CF84 File Offset: 0x0000B184
		private object[] m_TokenMgrObjectIds { get; set; }

		// Token: 0x0600025E RID: 606 RVA: 0x0000CF90 File Offset: 0x0000B190
		internal ObxmlTokenManagerContext()
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		internal static ObxmlTokenManagerContext CreateDefaultTokenManagerContext(OracleConnection connection)
		{
			return new ObxmlTokenManagerContext(new string[]
			{
				"Token Set GUID",
				connection.ConnectionString
			}, new object[]
			{
				1,
				2
			});
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		internal ObxmlTokenManagerContext(string[] tokenMgrStringIds, object[] tokenMgrObjectIds)
		{
			this.m_TokenMgrStringIds = tokenMgrStringIds;
			this.m_TokenMgrObjectIds = tokenMgrObjectIds;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000D010 File Offset: 0x0000B210
		internal bool IsValid
		{
			get
			{
				return this.m_TokenMgrObjectIds != null && this.m_TokenMgrStringIds != null;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000D028 File Offset: 0x0000B228
		internal string PartitionId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.m_PartitionId))
				{
					return this.m_PartitionId;
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (this.m_TokenMgrStringIds != null)
				{
					foreach (string value in this.m_TokenMgrStringIds)
					{
						if (!string.IsNullOrEmpty(value))
						{
							stringBuilder.Append(value);
						}
					}
				}
				if (this.m_TokenMgrObjectIds != null)
				{
					foreach (object obj in this.m_TokenMgrObjectIds)
					{
						if (obj != null)
						{
							stringBuilder.Append(obj.ToString());
						}
					}
				}
				return this.m_PartitionId = stringBuilder.ToString();
			}
		}

		// Token: 0x040002EF RID: 751
		private string m_PartitionId = string.Empty;
	}
}
