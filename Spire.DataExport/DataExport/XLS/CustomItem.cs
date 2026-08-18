using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001A7 RID: 423
	public abstract class CustomItem : CollectionItem
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x0007A2D8 File Offset: 0x000792D8
		public CustomItem()
		{
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000B93 RID: 2963
		public abstract ItemType ItemType { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0007A2EC File Offset: 0x000792EC
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x0007A330 File Offset: 0x00079330
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Tag
		{
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
			set
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0007A374 File Offset: 0x00079374
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0007A3B8 File Offset: 0x000793B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ExportSource ExportSource
		{
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
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜁ = value;
						num = 2;
						continue;
					case 2:
						goto IL_4A;
					}
					IL_1C:
					if (value != this.ᜁ)
					{
						num = 1;
						continue;
					}
					IL_4A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_60;
					}
				}
				IL_60:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0007A434 File Offset: 0x00079434
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0007A478 File Offset: 0x00079478
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IDbCommand Command
		{
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
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_52;
					case 2:
						this.ᜂ = value;
						num = 1;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.ᜂ)
					{
						num = 2;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0007A4F4 File Offset: 0x000794F4
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0007A538 File Offset: 0x00079538
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataTable DataTable
		{
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
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_52;
					case 2:
						this.ᜃ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					IL_1C:
					if (value != this.ᜃ)
					{
						num = 2;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0007A5B4 File Offset: 0x000795B4
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0007A5F8 File Offset: 0x000795F8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public CellExport CellExport
		{
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
			set
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0007A63C File Offset: 0x0007963C
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0007A680 File Offset: 0x00079680
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜅ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_52;
					case 1:
						this.ᜅ = value;
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.ᜅ)
					{
						num = 1;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x040008E4 RID: 2276
		private byte \u2593\u008F\u00A1\u00AE;

		// Token: 0x040008E5 RID: 2277
		private int ᜀ;

		// Token: 0x040008E6 RID: 2278
		private ExportSource ᜁ;

		// Token: 0x040008E7 RID: 2279
		private string \u25D8\u0096\u008F\u009F;

		// Token: 0x040008E8 RID: 2280
		private IDbCommand ᜂ;

		// Token: 0x040008E9 RID: 2281
		private DataTable ᜃ;

		// Token: 0x040008EA RID: 2282
		private bool \u2460\u008E\u00AC\u00A0;

		// Token: 0x040008EB RID: 2283
		private CellExport ᜄ;

		// Token: 0x040008EC RID: 2284
		private float[] \u25D8\u0097\u009D\u00AF;

		// Token: 0x040008ED RID: 2285
		private string[] \u2460\u00A3\u008E\u0098;

		// Token: 0x040008EE RID: 2286
		private ListView ᜅ;
	}
}
