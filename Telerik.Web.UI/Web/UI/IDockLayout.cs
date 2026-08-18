using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000FB2 RID: 4018
	public interface IDockLayout
	{
		// Token: 0x06009A51 RID: 39505
		void RegisterDock(RadDock dock);

		// Token: 0x06009A52 RID: 39506
		void UnRegisterDock(RadDock dock);

		// Token: 0x06009A53 RID: 39507
		void RegisterDockZone(RadDockZone zone);

		// Token: 0x06009A54 RID: 39508
		void UnRegisterDockZone(RadDockZone zone);

		// Token: 0x06009A55 RID: 39509
		void SetDockParent(RadDock dock, string newParentClientID);
	}
}
