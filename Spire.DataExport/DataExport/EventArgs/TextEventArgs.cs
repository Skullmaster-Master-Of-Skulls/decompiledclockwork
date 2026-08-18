using System;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x0200018F RID: 399
	public class TextEventArgs : EventArgs
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x00072C98 File Offset: 0x00071C98
		public TextEventArgs(int Row, int Col, string Text)
		{
			this.ᜀ = Col;
			this.ᜁ = Row;
			this.ᜂ = Text;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00072CCC File Offset: 0x00071CCC
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
				return this.ᜀ;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00072D10 File Offset: 0x00071D10
		public int Row
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00072D54 File Offset: 0x00071D54
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00072D98 File Offset: 0x00071D98
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x0400086F RID: 2159
		private int ᜀ;

		// Token: 0x04000870 RID: 2160
		private byte[] \u2593\u00AE\u008C\u007F;

		// Token: 0x04000871 RID: 2161
		private int ᜁ;

		// Token: 0x04000872 RID: 2162
		private string ᜂ = string.Empty;
	}
}
