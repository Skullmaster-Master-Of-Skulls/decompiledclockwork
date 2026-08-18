using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000019 RID: 25
	public sealed class OracleXmlSaveProperties : ICloneable
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0000F578 File Offset: 0x0000E578
		static OracleXmlSaveProperties()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000F586 File Offset: 0x0000E586
		public OracleXmlSaveProperties()
		{
			this.m_rowTag = "ROW";
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000F599 File Offset: 0x0000E599
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000F5A1 File Offset: 0x0000E5A1
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000F5AA File Offset: 0x0000E5AA
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000F5B2 File Offset: 0x0000E5B2
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000F5BB File Offset: 0x0000E5BB
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000F5D1 File Offset: 0x0000E5D1
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000F5DA File Offset: 0x0000E5DA
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000F5E2 File Offset: 0x0000E5E2
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000F5EB File Offset: 0x0000E5EB
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000F5F3 File Offset: 0x0000E5F3
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000F5FC File Offset: 0x0000E5FC
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000F604 File Offset: 0x0000E604
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

		// Token: 0x060000D6 RID: 214 RVA: 0x0000F610 File Offset: 0x0000E610
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

		// Token: 0x040000A6 RID: 166
		private string[] m_keyColumnsList;

		// Token: 0x040000A7 RID: 167
		private string m_rowTag;

		// Token: 0x040000A8 RID: 168
		private string m_table;

		// Token: 0x040000A9 RID: 169
		private string[] m_updateColumnsList;

		// Token: 0x040000AA RID: 170
		private string m_xslt;

		// Token: 0x040000AB RID: 171
		private string m_xsltParams;
	}
}
