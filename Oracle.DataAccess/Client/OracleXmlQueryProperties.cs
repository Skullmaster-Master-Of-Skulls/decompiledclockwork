using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000037 RID: 55
	public sealed class OracleXmlQueryProperties : ICloneable
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0001CD54 File Offset: 0x0001BD54
		static OracleXmlQueryProperties()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0001CD62 File Offset: 0x0001BD62
		public OracleXmlQueryProperties()
		{
			this.m_maxRows = -1;
			this.m_rootTag = "ROWSET";
			this.m_rowTag = "ROW";
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0001CD87 File Offset: 0x0001BD87
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0001CD8F File Offset: 0x0001BD8F
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0001CDA2 File Offset: 0x0001BDA2
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0001CDAA File Offset: 0x0001BDAA
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0001CDB3 File Offset: 0x0001BDB3
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0001CDBB File Offset: 0x0001BDBB
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0001CDC4 File Offset: 0x0001BDC4
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0001CDCC File Offset: 0x0001BDCC
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0001CDD5 File Offset: 0x0001BDD5
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0001CDDD File Offset: 0x0001BDDD
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

		// Token: 0x06000263 RID: 611 RVA: 0x0001CDE8 File Offset: 0x0001BDE8
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

		// Token: 0x040001B5 RID: 437
		private int m_maxRows;

		// Token: 0x040001B6 RID: 438
		private string m_rootTag;

		// Token: 0x040001B7 RID: 439
		private string m_rowTag;

		// Token: 0x040001B8 RID: 440
		private string m_xslt;

		// Token: 0x040001B9 RID: 441
		private string m_xsltParams;
	}
}
