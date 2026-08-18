using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Spire.DataExport.Common;

namespace Spire.DataExport.Access
{
	// Token: 0x020001EF RID: 495
	public class TableCreatedEventArgs
	{
		// Token: 0x06000F24 RID: 3876 RVA: 0x000A6438 File Offset: 0x000A5438
		internal TableCreatedEventArgs(string A_0, List<TableColumn> A_1, ExportSource A_2, object A_3, DataTable A_4)
		{
			this.TableName = A_0;
			this.Columns = A_1.ToArray();
			this.DataSourceType = A_2;
			this.DataSource = A_3;
			this.SchemaTable = A_4;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x000A6478 File Offset: 0x000A5478
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x000A64BC File Offset: 0x000A54BC
		public string TableName
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
			[CompilerGenerated]
			private set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x000A6500 File Offset: 0x000A5500
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x000A6544 File Offset: 0x000A5544
		public TableColumn[] Columns
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
			[CompilerGenerated]
			private set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x000A6588 File Offset: 0x000A5588
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x000A65CC File Offset: 0x000A55CC
		public ExportSource DataSourceType
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜂ;
			}
			[CompilerGenerated]
			private set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x000A6610 File Offset: 0x000A5610
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x000A6654 File Offset: 0x000A5654
		public object DataSource
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜃ;
			}
			[CompilerGenerated]
			private set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000A6698 File Offset: 0x000A5698
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x000A66DC File Offset: 0x000A56DC
		public DataTable SchemaTable
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜄ;
			}
			[CompilerGenerated]
			private set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x04000B6B RID: 2923
		private long \u2609\u00A0\u0092\u0086;

		// Token: 0x04000B6C RID: 2924
		private float \u2593\u008A\u00A0\u0096;

		// Token: 0x04000B6D RID: 2925
		private float \u25D8\u00A6\u00A9\u0080;

		// Token: 0x04000B6E RID: 2926
		private float \u25D8\u0086\u008C\u00AB;

		// Token: 0x04000B6F RID: 2927
		private long \u25D9\u00AE\u00A2\u00A9;

		// Token: 0x04000B70 RID: 2928
		private bool \u2609\u008B\u009E\u009C;

		// Token: 0x04000B71 RID: 2929
		[CompilerGenerated]
		private string ᜀ;

		// Token: 0x04000B72 RID: 2930
		[CompilerGenerated]
		private TableColumn[] ᜁ;

		// Token: 0x04000B73 RID: 2931
		[CompilerGenerated]
		private ExportSource ᜂ;

		// Token: 0x04000B74 RID: 2932
		[CompilerGenerated]
		private object ᜃ;

		// Token: 0x04000B75 RID: 2933
		[CompilerGenerated]
		private DataTable ᜄ;
	}
}
