using System;
using Spire.DataExport.XLS;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x02000192 RID: 402
	public class TitleParamsEventArgs : EventArgs
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0007305C File Offset: 0x0007205C
		public TitleParamsEventArgs(int Sheet, int Col, CellFormat Format, string Caption)
		{
			this.ᜀ = Sheet;
			this.ᜁ = Col;
			this.ᜂ = Format;
			this.ᜃ = Caption;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00073098 File Offset: 0x00072098
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x000730DC File Offset: 0x000720DC
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00073120 File Offset: 0x00072120
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00073164 File Offset: 0x00072164
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x000731A8 File Offset: 0x000721A8
		public string Caption
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

		// Token: 0x04000880 RID: 2176
		private int ᜀ;

		// Token: 0x04000881 RID: 2177
		private int ᜁ;

		// Token: 0x04000882 RID: 2178
		private byte \u2460\u008C\u0096\u00AC;

		// Token: 0x04000883 RID: 2179
		private CellFormat ᜂ;

		// Token: 0x04000884 RID: 2180
		private string ᜃ = string.Empty;
	}
}
