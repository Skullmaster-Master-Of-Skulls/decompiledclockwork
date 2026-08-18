using System;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x0200018C RID: 396
	public class XLSTextEventArgs : EventArgs
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x00072700 File Offset: 0x00071700
		public XLSTextEventArgs(int Sheet, int Row, int Col, string Text)
		{
			this.ᜀ = Sheet;
			this.ᜁ = Col;
			this.ᜃ = Row;
			this.ᜂ = Text;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0007273C File Offset: 0x0007173C
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00072780 File Offset: 0x00071780
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000727C4 File Offset: 0x000717C4
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
				return this.ᜃ;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00072808 File Offset: 0x00071808
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0007284C File Offset: 0x0007184C
		public string Text
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
				this.ᜂ = value;
			}
		}

		// Token: 0x04000858 RID: 2136
		private int ᜀ;

		// Token: 0x04000859 RID: 2137
		private int ᜁ;

		// Token: 0x0400085A RID: 2138
		private bool \u25D9\u0082\u008D\u0088;

		// Token: 0x0400085B RID: 2139
		private string \u25D9\u0084ª\u00A0;

		// Token: 0x0400085C RID: 2140
		private string ᜂ = string.Empty;

		// Token: 0x0400085D RID: 2141
		private int ᜃ;
	}
}
