using System;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000215 RID: 533
	public sealed class TabSheetMovedEventArgs : EventArgs
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x00105D64 File Offset: 0x00104D64
		private TabSheetMovedEventArgs()
		{
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00105D78 File Offset: 0x00104D78
		public TabSheetMovedEventArgs(int oldIndex, int newIndex)
		{
			this.ᜀ = oldIndex;
			this.ᜁ = newIndex;
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00105D9C File Offset: 0x00104D9C
		public int OldIndex
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

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x00105DE0 File Offset: 0x00104DE0
		public int NewIndex
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

		// Token: 0x040010E0 RID: 4320
		private byte \u25D8\u0084\u0081\u0082;

		// Token: 0x040010E1 RID: 4321
		private string \u25D9\u0098\u00A9\u009F;

		// Token: 0x040010E2 RID: 4322
		private int ᜀ;

		// Token: 0x040010E3 RID: 4323
		private int ᜁ;
	}
}
