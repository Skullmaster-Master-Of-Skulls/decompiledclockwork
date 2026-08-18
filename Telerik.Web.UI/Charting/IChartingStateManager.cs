using System;

namespace Telerik.Charting
{
	// Token: 0x020016D5 RID: 5845
	public interface IChartingStateManager
	{
		// Token: 0x0600E1B7 RID: 57783
		void LoadViewState(object state);

		// Token: 0x0600E1B8 RID: 57784
		object SaveViewState();

		// Token: 0x0600E1B9 RID: 57785
		void TrackViewState();

		// Token: 0x17004537 RID: 17719
		// (get) Token: 0x0600E1BA RID: 57786
		bool IsTrackingViewState { get; }
	}
}
