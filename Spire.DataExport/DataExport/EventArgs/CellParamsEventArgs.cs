using System;
using System.Drawing;
using Spire.DataExport.Common;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x02000189 RID: 393
	public class CellParamsEventArgs : EventArgs
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x000722A4 File Offset: 0x000712A4
		public CellParamsEventArgs(int RecNo, int ColNo, string Value, ColumAlign Align, Font Font, Color Background)
		{
			this.ᜀ = RecNo;
			this.ᜁ = ColNo;
			this.ᜂ = Value;
			this.ᜃ = Align;
			this.ᜄ = Font;
			this.ᜅ = Background;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x000722FC File Offset: 0x000712FC
		public int RecNo
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
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00072340 File Offset: 0x00071340
		public int ColNo
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
				return this.ᜁ;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00072384 File Offset: 0x00071384
		public string Value
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
				return this.ᜂ;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000723C8 File Offset: 0x000713C8
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x0007240C File Offset: 0x0007140C
		public ColumAlign Align
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00072450 File Offset: 0x00071450
		public Font Font
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
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00072494 File Offset: 0x00071494
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x000724D8 File Offset: 0x000714D8
		public Color Background
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x04000843 RID: 2115
		private int ᜀ;

		// Token: 0x04000844 RID: 2116
		private int ᜁ;

		// Token: 0x04000845 RID: 2117
		private string ᜂ = string.Empty;

		// Token: 0x04000846 RID: 2118
		private ColumAlign ᜃ;

		// Token: 0x04000847 RID: 2119
		private bool \u2460\u0093\u00AF\u008A;

		// Token: 0x04000848 RID: 2120
		private Font ᜄ;

		// Token: 0x04000849 RID: 2121
		private Color ᜅ = Color.Empty;
	}
}
