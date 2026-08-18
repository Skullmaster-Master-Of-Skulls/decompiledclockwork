using System;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000032 RID: 50
	public sealed class OracleXmlQueryProperties : ICloneable
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public OracleXmlQueryProperties()
		{
			this.m_maxRows = -1;
			this.m_rootTag = "ROWSET";
			this.m_rowTag = "ROW";
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000F300 File Offset: 0x0000D500
		public int MaxRows
		{
			get
			{
				return this.m_maxRows;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException();
				}
				this.m_maxRows = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000F314 File Offset: 0x0000D514
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000F31C File Offset: 0x0000D51C
		public string RootTag
		{
			get
			{
				return this.m_rootTag;
			}
			set
			{
				this.m_rootTag = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000F328 File Offset: 0x0000D528
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000F330 File Offset: 0x0000D530
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000F33C File Offset: 0x0000D53C
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000F344 File Offset: 0x0000D544
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000F350 File Offset: 0x0000D550
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000F358 File Offset: 0x0000D558
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

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F364 File Offset: 0x0000D564
		public object Clone()
		{
			return new OracleXmlQueryProperties
			{
				m_maxRows = this.m_maxRows,
				m_rootTag = this.m_rootTag,
				m_rowTag = this.m_rowTag,
				m_xslt = this.m_xslt,
				m_xsltParams = this.m_xsltParams
			};
		}

		// Token: 0x04000316 RID: 790
		private int m_maxRows;

		// Token: 0x04000317 RID: 791
		private string m_rootTag;

		// Token: 0x04000318 RID: 792
		private string m_rowTag;

		// Token: 0x04000319 RID: 793
		private string m_xslt;

		// Token: 0x0400031A RID: 794
		private string m_xsltParams;
	}
}
