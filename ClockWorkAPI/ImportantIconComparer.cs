using System;
using System.Collections;

namespace ClockWorkAPI
{
	// Token: 0x02000005 RID: 5
	public class ImportantIconComparer : IComparer
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002338 File Offset: 0x00001338
		public ImportantIconComparer()
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002343 File Offset: 0x00001343
		public ImportantIconComparer(int[] ImportantIconIds)
		{
			this.importantIconIds = ImportantIconIds;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002358 File Offset: 0x00001358
		public int Compare(object x, object y)
		{
			Icon icon = (Icon)x;
			Icon icon2 = (Icon)y;
			int num = Array.IndexOf<int>(this.importantIconIds, icon.IconID);
			int value = Array.IndexOf<int>(this.importantIconIds, icon2.IconID);
			return -num.CompareTo(value);
		}

		// Token: 0x04000002 RID: 2
		private int[] importantIconIds;
	}
}
