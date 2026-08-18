using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003E8 RID: 1000
	[Guid("4cf68d33-e3f2-4964-b85e-945b4f7e2f21")]
	[ComImport]
	internal interface INotifyCollectionChangedEventArgs
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002622 RID: 9762
		NotifyCollectionChangedAction Action { get; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002623 RID: 9763
		IList NewItems { get; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002624 RID: 9764
		IList OldItems { get; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002625 RID: 9765
		int NewStartingIndex { get; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002626 RID: 9766
		int OldStartingIndex { get; }
	}
}
