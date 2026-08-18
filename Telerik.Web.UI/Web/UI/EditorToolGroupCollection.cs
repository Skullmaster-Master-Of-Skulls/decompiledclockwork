using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012A5 RID: 4773
	public sealed class EditorToolGroupCollection : StronglyTypedStateManagedCollection<EditorToolGroup>
	{
		// Token: 0x0600C7FA RID: 51194 RVA: 0x002C8ED5 File Offset: 0x002C70D5
		internal EditorToolGroupCollection()
		{
		}

		// Token: 0x0600C7FB RID: 51195 RVA: 0x002C8EE0 File Offset: 0x002C70E0
		protected override void SetDirtyObject(object o)
		{
			StateManager stateManager = o as StateManager;
			if (stateManager != null)
			{
				stateManager.SetDirty();
			}
		}
	}
}
