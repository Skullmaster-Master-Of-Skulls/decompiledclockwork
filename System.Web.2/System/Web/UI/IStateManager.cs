using System;

namespace System.Web.UI
{
	// Token: 0x020002B6 RID: 694
	public interface IStateManager
	{
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001FCA RID: 8138
		bool IsTrackingViewState { get; }

		// Token: 0x06001FCB RID: 8139
		void LoadViewState(object state);

		// Token: 0x06001FCC RID: 8140
		object SaveViewState();

		// Token: 0x06001FCD RID: 8141
		void TrackViewState();
	}
}
