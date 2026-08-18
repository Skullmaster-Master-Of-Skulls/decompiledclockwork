using System;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000033 RID: 51
	public sealed class OracleXmlSaveProperties : ICloneable
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		public OracleXmlSaveProperties()
		{
			this.m_rowTag = "ROW";
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public string[] KeyColumnsList
		{
			get
			{
				return this.m_keyColumnsList;
			}
			set
			{
				this.m_keyColumnsList = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000F3DC File Offset: 0x0000D5DC
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		public string RowTag
		{
			get
			{
				return this.m_rowTag;
			}
			set
			{
				this.m_rowTag = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000F408 File Offset: 0x0000D608
		public string Table
		{
			get
			{
				if (this.m_table != null)
				{
					return this.m_table;
				}
				return string.Empty;
			}
			set
			{
				this.m_table = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000F414 File Offset: 0x0000D614
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000F41C File Offset: 0x0000D61C
		public string[] UpdateColumnsList
		{
			get
			{
				return this.m_updateColumnsList;
			}
			set
			{
				this.m_updateColumnsList = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000F428 File Offset: 0x0000D628
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000F430 File Offset: 0x0000D630
		public string Xslt
		{
			get
			{
				return this.m_xslt;
			}
			set
			{
				this.m_xslt = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000F43C File Offset: 0x0000D63C
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000F444 File Offset: 0x0000D644
		public string XsltParams
		{
			get
			{
				return this.m_xsltParams;
			}
			set
			{
				this.m_xsltParams = value;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000F450 File Offset: 0x0000D650
		public object Clone()
		{
			return new OracleXmlSaveProperties
			{
				m_keyColumnsList = this.m_keyColumnsList,
				m_rowTag = this.m_rowTag,
				m_table = this.m_table,
				m_updateColumnsList = this.m_updateColumnsList,
				m_xslt = this.m_xslt,
				m_xsltParams = this.m_xsltParams
			};
		}

		// Token: 0x0400031B RID: 795
		private string[] m_keyColumnsList;

		// Token: 0x0400031C RID: 796
		private string m_rowTag;

		// Token: 0x0400031D RID: 797
		private string m_table;

		// Token: 0x0400031E RID: 798
		private string[] m_updateColumnsList;

		// Token: 0x0400031F RID: 799
		private string m_xslt;

		// Token: 0x04000320 RID: 800
		private string m_xsltParams;
	}
}
