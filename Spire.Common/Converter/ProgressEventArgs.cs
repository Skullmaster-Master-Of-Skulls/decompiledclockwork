using System;

namespace Spire.Xls.Converter
{
	// Token: 0x0200001E RID: 30
	public class ProgressEventArgs : EventArgs
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00007968 File Offset: 0x00005B68
		public ProgressEventArgs(int noOfSheets, int activeSheetIndex, object source)
		{
			this.ᜂ = noOfSheets;
			this.ᜀ = activeSheetIndex;
			this.ᜁ = (float)(100 / noOfSheets * (activeSheetIndex + 1));
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00007998 File Offset: 0x00005B98
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000079DC File Offset: 0x00005BDC
		public float CurrentProgressChanged
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
				this.ᜁ = value;
			}
		}

		// Token: 0x0400004C RID: 76
		private float \u25D9\u00AF\u00A9\u009E;

		// Token: 0x0400004D RID: 77
		private byte \u25D8\u0080\u0092\u00A8;

		// Token: 0x0400004E RID: 78
		private int ᜀ;

		// Token: 0x0400004F RID: 79
		private bool[] \u2593\u008B\u009D\u0081;

		// Token: 0x04000050 RID: 80
		private float[] \u2609\u00A1\u009E\u00A7;

		// Token: 0x04000051 RID: 81
		private string \u2460\u00A2\u0096\u0099;

		// Token: 0x04000052 RID: 82
		private int[] \u2609\u009E\u009B\u0093;

		// Token: 0x04000053 RID: 83
		private float ᜁ;

		// Token: 0x04000054 RID: 84
		private int ᜂ;
	}
}
