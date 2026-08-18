using System;
using Spire.DataExport.Common;

namespace Spire.DataExport.EventArgs
{
	// Token: 0x02000188 RID: 392
	public class ExportRowEventArgs : EventArgs
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x000721B4 File Offset: 0x000711B4
		public ExportRowEventArgs(RowExport RowExport, bool Accept)
		{
			this.ᜀ = RowExport;
			this.ᜁ = Accept;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x000721D8 File Offset: 0x000711D8
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
				return this.ᜀ;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0007221C File Offset: 0x0007121C
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00072260 File Offset: 0x00071260
		public bool Accept
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x0400083E RID: 2110
		private byte \u25D9\u0087\u009E\u008F;

		// Token: 0x0400083F RID: 2111
		private byte[] \u25D9\u008F\u0095\u0081;

		// Token: 0x04000840 RID: 2112
		private int \u25D9\u009D\u00ADª;

		// Token: 0x04000841 RID: 2113
		private RowExport ᜀ;

		// Token: 0x04000842 RID: 2114
		private bool ᜁ;
	}
}
