using System;
using Spire.DataExport.RTF;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x02000187 RID: 391
	public class RTFTitleStyleEventArgs : EventArgs
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x00072108 File Offset: 0x00071108
		public RTFTitleStyleEventArgs(int ColNo, RTFStyle Style)
		{
			this.ᜀ = ColNo;
			this.ᜁ = Style;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0007212C File Offset: 0x0007112C
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
				return this.ᜀ;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00072170 File Offset: 0x00071170
		public RTFStyle Style
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

		// Token: 0x04000838 RID: 2104
		private float \u2609\u00B0\u0087\u0091;

		// Token: 0x04000839 RID: 2105
		private byte \u2593\u00A1\u0081\u00AE;

		// Token: 0x0400083A RID: 2106
		private float[] \u2460\u0089\u00AE\u009A;

		// Token: 0x0400083B RID: 2107
		private float[] \u2609\u009F\u0088\u00A3;

		// Token: 0x0400083C RID: 2108
		private int ᜀ;

		// Token: 0x0400083D RID: 2109
		private RTFStyle ᜁ;
	}
}
