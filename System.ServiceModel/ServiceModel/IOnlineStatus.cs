using System;

namespace System.ServiceModel
{
	// Token: 0x02000168 RID: 360
	public interface IOnlineStatus
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000AB3 RID: 2739
		// (remove) Token: 0x06000AB4 RID: 2740
		event EventHandler Offline;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000AB5 RID: 2741
		// (remove) Token: 0x06000AB6 RID: 2742
		event EventHandler Online;

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000AB7 RID: 2743
		bool IsOnline { get; }
	}
}
