using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001942 RID: 6466
	public interface IRadListViewCommandEvent
	{
		// Token: 0x17004BA7 RID: 19367
		// (get) Token: 0x0600FA65 RID: 64101
		// (set) Token: 0x0600FA66 RID: 64102
		bool Canceled { get; set; }

		// Token: 0x0600FA67 RID: 64103
		void ExecuteCommand(object source);
	}
}
