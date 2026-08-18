using System;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004E5 RID: 1253
	internal class CategoryEntry
	{
		// Token: 0x06002F76 RID: 12150 RVA: 0x000D633C File Offset: 0x000D453C
		internal CategoryEntry(NativeMethods.PERF_OBJECT_TYPE perfObject)
		{
			this.NameIndex = perfObject.ObjectNameTitleIndex;
			this.HelpIndex = perfObject.ObjectHelpTitleIndex;
			this.CounterIndexes = new int[perfObject.NumCounters];
			this.HelpIndexes = new int[perfObject.NumCounters];
		}

		// Token: 0x040027F2 RID: 10226
		internal int NameIndex;

		// Token: 0x040027F3 RID: 10227
		internal int HelpIndex;

		// Token: 0x040027F4 RID: 10228
		internal int[] CounterIndexes;

		// Token: 0x040027F5 RID: 10229
		internal int[] HelpIndexes;
	}
}
