using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EBD RID: 3773
	public sealed class ImageEditorToolCollection : StronglyTypedStateManagedCollection<ImageEditorTool>
	{
		// Token: 0x06009009 RID: 36873 RVA: 0x00206F3D File Offset: 0x0020513D
		internal ImageEditorToolCollection()
		{
		}

		// Token: 0x0600900A RID: 36874 RVA: 0x00206F48 File Offset: 0x00205148
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
