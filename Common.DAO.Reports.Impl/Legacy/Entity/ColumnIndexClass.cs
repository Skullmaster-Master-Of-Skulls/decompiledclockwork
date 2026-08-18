using System;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000010 RID: 16
	public class ColumnIndexClass
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00023A30 File Offset: 0x00021C30
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00023A48 File Offset: 0x00021C48
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00023A54 File Offset: 0x00021C54
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00023A6C File Offset: 0x00021C6C
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00023A78 File Offset: 0x00021C78
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00023A90 File Offset: 0x00021C90
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00023A9C File Offset: 0x00021C9C
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00023AB4 File Offset: 0x00021CB4
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00023AC0 File Offset: 0x00021CC0
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00023AD8 File Offset: 0x00021CD8
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

		// Token: 0x06000155 RID: 341 RVA: 0x00023AE2 File Offset: 0x00021CE2
		public ColumnIndexClass(int index, string colName)
		{
			this.index = index;
			this.colName = colName;
			this.visible = true;
			this.encrypted = false;
			this.paramName = "";
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00023B13 File Offset: 0x00021D13
		public ColumnIndexClass(int index, string colName, string paramName, bool encrypted)
		{
			this.encrypted = encrypted;
			this.index = index;
			this.colName = colName;
			this.visible = true;
			this.paramName = paramName;
		}

		// Token: 0x04000047 RID: 71
		private int index;

		// Token: 0x04000048 RID: 72
		private bool encrypted;

		// Token: 0x04000049 RID: 73
		private bool visible;

		// Token: 0x0400004A RID: 74
		private string colName;

		// Token: 0x0400004B RID: 75
		private string paramName;
	}
}
