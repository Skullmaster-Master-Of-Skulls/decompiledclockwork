using System;

namespace System.ComponentModel
{
	// Token: 0x02000578 RID: 1400
	public interface ISupportInitializeNotification : ISupportInitialize
	{
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060033ED RID: 13293
		bool IsInitialized { get; }

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060033EE RID: 13294
		// (remove) Token: 0x060033EF RID: 13295
		event EventHandler Initialized;
	}
}
