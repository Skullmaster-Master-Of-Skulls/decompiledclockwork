using System;
using Spire.DataExport.XLS;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x0200018D RID: 397
	public class HeaderFooterParamsEventArgs : EventArgs
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x00072890 File Offset: 0x00071890
		public HeaderFooterParamsEventArgs(int Sheet, int Col, int Row, CellFormat Format, string Str)
		{
			this.ᜀ = Sheet;
			this.ᜁ = Col;
			this.ᜂ = Row;
			this.ᜃ = Format;
			this.ᜄ = Str;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000728D4 File Offset: 0x000718D4
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00072918 File Offset: 0x00071918
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0007295C File Offset: 0x0007195C
		public int Row
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x000729A0 File Offset: 0x000719A0
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
				return this.ᜃ;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000729E4 File Offset: 0x000719E4
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x00072A28 File Offset: 0x00071A28
		public string Str
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

		// Token: 0x0400085E RID: 2142
		private int ᜀ;

		// Token: 0x0400085F RID: 2143
		private int ᜁ;

		// Token: 0x04000860 RID: 2144
		private long \u2609\u008D\u0098\u008A;

		// Token: 0x04000861 RID: 2145
		private int ᜂ;

		// Token: 0x04000862 RID: 2146
		private byte \u2609\u0097\u00AE\u009C;

		// Token: 0x04000863 RID: 2147
		private CellFormat ᜃ;

		// Token: 0x04000864 RID: 2148
		private string ᜄ = string.Empty;
	}
}
