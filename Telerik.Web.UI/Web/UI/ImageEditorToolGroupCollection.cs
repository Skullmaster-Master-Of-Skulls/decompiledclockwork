using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EBA RID: 3770
	public sealed class ImageEditorToolGroupCollection : StronglyTypedStateManagedCollection<ImageEditorToolGroup>
	{
		// Token: 0x06008FF0 RID: 36848 RVA: 0x00206D1A File Offset: 0x00204F1A
		internal ImageEditorToolGroupCollection()
		{
		}

		// Token: 0x06008FF1 RID: 36849 RVA: 0x00206D24 File Offset: 0x00204F24
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
