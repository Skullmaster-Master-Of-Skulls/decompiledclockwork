using System;

namespace Telerik.Web.UI
{
	// Token: 0x020004B2 RID: 1202
	public interface IGridCommandEvent
	{
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06002ACE RID: 10958
		// (set) Token: 0x06002ACF RID: 10959
		bool Canceled { get; set; }

		// Token: 0x06002AD0 RID: 10960
		void ExecuteCommand(object source);
	}
}
