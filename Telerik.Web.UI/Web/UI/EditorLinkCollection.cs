using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001078 RID: 4216
	public sealed class EditorLinkCollection : StronglyTypedStateManagedCollection<EditorLink>
	{
		// Token: 0x0600A9CF RID: 43471 RVA: 0x0024DD30 File Offset: 0x0024BF30
		internal EditorLinkCollection()
		{
		}

		// Token: 0x0600A9D0 RID: 43472 RVA: 0x0024DD38 File Offset: 0x0024BF38
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
