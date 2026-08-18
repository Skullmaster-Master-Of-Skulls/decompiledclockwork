using System;
using Spire.DataExport.Common;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x0200018B RID: 395
	public class XLSExportRowEventArgs : EventArgs
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x000725C8 File Offset: 0x000715C8
		public XLSExportRowEventArgs(int Sheet, RowExport RowExport, bool Accept)
		{
			this.ᜀ = Sheet;
			this.ᜁ = RowExport;
			this.ᜂ = Accept;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000725F0 File Offset: 0x000715F0
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00072634 File Offset: 0x00071634
		public RowExport RowExport
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00072678 File Offset: 0x00071678
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x000726BC File Offset: 0x000716BC
		public bool Accept
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

		// Token: 0x0400084F RID: 2127
		private int ᜀ;

		// Token: 0x04000850 RID: 2128
		private byte[] \u25D8\u00A3\u0088\u009E;

		// Token: 0x04000851 RID: 2129
		private bool \u2609\u00A2\u009A\u00AE;

		// Token: 0x04000852 RID: 2130
		private RowExport ᜁ;

		// Token: 0x04000853 RID: 2131
		private byte \u25D8\u00AB\u008D\u00A2;

		// Token: 0x04000854 RID: 2132
		private byte \u25D8\u0088\u008E\u00AD;

		// Token: 0x04000855 RID: 2133
		private long[] \u25D8\u00A5\u0084\u00A8;

		// Token: 0x04000856 RID: 2134
		private int \u2609ªª\u009F;

		// Token: 0x04000857 RID: 2135
		private bool ᜂ;
	}
}
