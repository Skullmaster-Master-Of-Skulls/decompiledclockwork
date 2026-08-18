using System;
using Spire.DataExport.XLS;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x0200018E RID: 398
	public class AggregateParamsEventArgs : EventArgs
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x00072A6C File Offset: 0x00071A6C
		public AggregateParamsEventArgs(int Sheet, int Col, CellFormat Format, string FormatText, string Value)
		{
			this.ᜀ = Sheet;
			this.ᜁ = Col;
			this.ᜂ = Format;
			this.ᜃ = FormatText;
			this.ᜄ = Value;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00072ABC File Offset: 0x00071ABC
		public int Sheet
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00072B00 File Offset: 0x00071B00
		public int Col
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00072B44 File Offset: 0x00071B44
		public CellFormat Format
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00072B88 File Offset: 0x00071B88
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00072BCC File Offset: 0x00071BCC
		public string FormatText
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00072C10 File Offset: 0x00071C10
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x00072C54 File Offset: 0x00071C54
		public string Value
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x04000865 RID: 2149
		private string \u2593\u0091\u008D\u009D;

		// Token: 0x04000866 RID: 2150
		private byte \u25D8\u00B0\u008F\u008F;

		// Token: 0x04000867 RID: 2151
		private byte \u2609\u0086\u00AF\u009F;

		// Token: 0x04000868 RID: 2152
		private bool[] \u2593\u00A7\u00AD\u009F;

		// Token: 0x04000869 RID: 2153
		private int ᜀ;

		// Token: 0x0400086A RID: 2154
		private int ᜁ;

		// Token: 0x0400086B RID: 2155
		private CellFormat ᜂ;

		// Token: 0x0400086C RID: 2156
		private string ᜃ = string.Empty;

		// Token: 0x0400086D RID: 2157
		private string[] \u2460\u00A1\u008B\u0086;

		// Token: 0x0400086E RID: 2158
		private string ᜄ = string.Empty;
	}
}
