using System;
using Spire.DataExport.XLS;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x02000191 RID: 401
	public class DataParamsEventArgs : EventArgs
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x00072E80 File Offset: 0x00071E80
		public DataParamsEventArgs(int Sheet, int Col, int Row, CellFormat Format, string FormatText)
		{
			this.ᜀ = Sheet;
			this.ᜁ = Col;
			this.ᜂ = Row;
			this.ᜃ = Format;
			this.ᜄ = FormatText;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00072EC4 File Offset: 0x00071EC4
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00072F08 File Offset: 0x00071F08
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00072F4C File Offset: 0x00071F4C
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00072F90 File Offset: 0x00071F90
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00072FD4 File Offset: 0x00071FD4
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00073018 File Offset: 0x00072018
		public string FormatText
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

		// Token: 0x04000875 RID: 2165
		private long \u25D9\u0082\u0093\u009B;

		// Token: 0x04000876 RID: 2166
		private int ᜀ;

		// Token: 0x04000877 RID: 2167
		private int ᜁ;

		// Token: 0x04000878 RID: 2168
		private int ᜂ;

		// Token: 0x04000879 RID: 2169
		private string[] \u2609\u0097\u00A4\u009C;

		// Token: 0x0400087A RID: 2170
		private string[] \u2609\u0092\u0080\u00A9;

		// Token: 0x0400087B RID: 2171
		private CellFormat ᜃ;

		// Token: 0x0400087C RID: 2172
		private long[] \u2609\u009C\u00AF\u0089;

		// Token: 0x0400087D RID: 2173
		private long \u2460\u00A7\u0098\u0084;

		// Token: 0x0400087E RID: 2174
		private long \u2593\u00A2\u0093\u008D;

		// Token: 0x0400087F RID: 2175
		private string ᜄ = string.Empty;
	}
}
