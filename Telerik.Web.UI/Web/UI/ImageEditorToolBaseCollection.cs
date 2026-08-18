using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EBC RID: 3772
	public sealed class ImageEditorToolBaseCollection : StronglyTypedStateManagedCollection<ImageEditorToolBase>
	{
		// Token: 0x06009007 RID: 36871 RVA: 0x00206F16 File Offset: 0x00205116
		internal ImageEditorToolBaseCollection()
		{
		}

		// Token: 0x06009008 RID: 36872 RVA: 0x00206F20 File Offset: 0x00205120
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
