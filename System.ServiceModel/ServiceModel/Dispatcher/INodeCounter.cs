using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F0 RID: 1264
	internal interface INodeCounter
	{
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002FBF RID: 12223
		// (set) Token: 0x06002FC0 RID: 12224
		int CounterMarker { get; set; }

		// Token: 0x17000B50 RID: 2896
		// (set) Token: 0x06002FC1 RID: 12225
		int MaxCounter { set; }

		// Token: 0x06002FC2 RID: 12226
		int ElapsedCount(int marker);

		// Token: 0x06002FC3 RID: 12227
		void Increase();

		// Token: 0x06002FC4 RID: 12228
		void IncreaseBy(int count);
	}
}
