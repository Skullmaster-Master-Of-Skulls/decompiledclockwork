using System;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000619 RID: 1561
	public class NameIndexChangedEventArgs : EventArgs
	{
		// Token: 0x06005EAC RID: 24236 RVA: 0x003B2444 File Offset: 0x003B1444
		private NameIndexChangedEventArgs()
		{
		}

		// Token: 0x06005EAD RID: 24237 RVA: 0x003B2458 File Offset: 0x003B1458
		public NameIndexChangedEventArgs(int oldIndex, int newIndex)
		{
			this.ᜀ = oldIndex;
			this.ᜁ = newIndex;
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06005EAE RID: 24238 RVA: 0x003B247C File Offset: 0x003B147C
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

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x003B24C0 File Offset: 0x003B14C0
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

		// Token: 0x04002D79 RID: 11641
		private int ᜀ;

		// Token: 0x04002D7A RID: 11642
		private bool[] \u25D9\u00AF\u009C\u0091;

		// Token: 0x04002D7B RID: 11643
		private bool \u2609\u00AF\u00A0\u0081;

		// Token: 0x04002D7C RID: 11644
		private byte[] \u2460\u0094\u00A8\u008A;

		// Token: 0x04002D7D RID: 11645
		private int ᜁ;
	}
}
