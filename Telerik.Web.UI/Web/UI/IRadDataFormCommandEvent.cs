using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001D9 RID: 473
	public interface IRadDataFormCommandEvent
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060010EE RID: 4334
		// (set) Token: 0x060010EF RID: 4335
		bool Canceled { get; set; }

		// Token: 0x060010F0 RID: 4336
		void ExecuteCommand(object source);
	}
}
