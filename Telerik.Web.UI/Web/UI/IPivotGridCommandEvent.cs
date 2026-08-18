using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C2E RID: 3118
	public interface IPivotGridCommandEvent
	{
		// Token: 0x17002676 RID: 9846
		// (get) Token: 0x0600764D RID: 30285
		// (set) Token: 0x0600764E RID: 30286
		bool Canceled { get; set; }

		// Token: 0x0600764F RID: 30287
		void ExecuteCommand(object source);
	}
}
