using System;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200076B RID: 1899
	internal class CategoryEntry
	{
		// Token: 0x06003A93 RID: 14995 RVA: 0x000F8C84 File Offset: 0x000F7C84
		internal CategoryEntry(NativeMethods.PERF_OBJECT_TYPE perfObject)
		{
			this.NameIndex = perfObject.ObjectNameTitleIndex;
			this.HelpIndex = perfObject.ObjectHelpTitleIndex;
			this.CounterIndexes = new int[perfObject.NumCounters];
			this.HelpIndexes = new int[perfObject.NumCounters];
		}

		// Token: 0x04003346 RID: 13126
		internal int NameIndex;

		// Token: 0x04003347 RID: 13127
		internal int HelpIndex;

		// Token: 0x04003348 RID: 13128
		internal int[] CounterIndexes;

		// Token: 0x04003349 RID: 13129
		internal int[] HelpIndexes;
	}
}
