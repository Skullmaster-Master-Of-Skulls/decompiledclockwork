using System;

namespace ReportFunctions
{
	// Token: 0x0200000A RID: 10
	public class ColumnIndexClass
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00005484 File Offset: 0x00004484
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000549C File Offset: 0x0000449C
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000054A8 File Offset: 0x000044A8
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000054C0 File Offset: 0x000044C0
		public bool Encrypted
		{
			get
			{
				return this.encrypted;
			}
			set
			{
				this.encrypted = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000054CC File Offset: 0x000044CC
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000054E4 File Offset: 0x000044E4
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000054F0 File Offset: 0x000044F0
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00005508 File Offset: 0x00004508
		public string ColName
		{
			get
			{
				return this.colName;
			}
			set
			{
				this.colName = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00005514 File Offset: 0x00004514
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000552C File Offset: 0x0000452C
		public string ParamName
		{
			get
			{
				return this.paramName;
			}
			set
			{
				this.paramName = value;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005536 File Offset: 0x00004536
		public ColumnIndexClass(int index, string colName)
		{
			this.index = index;
			this.colName = colName;
			this.visible = true;
			this.encrypted = false;
			this.paramName = "";
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005568 File Offset: 0x00004568
		public ColumnIndexClass(int index, string colName, string paramName, bool encrypted)
		{
			this.encrypted = encrypted;
			this.index = index;
			this.colName = colName;
			this.visible = true;
			this.paramName = paramName;
		}

		// Token: 0x040000D0 RID: 208
		private int index;

		// Token: 0x040000D1 RID: 209
		private bool encrypted;

		// Token: 0x040000D2 RID: 210
		private bool visible;

		// Token: 0x040000D3 RID: 211
		private string colName;

		// Token: 0x040000D4 RID: 212
		private string paramName;
	}
}
