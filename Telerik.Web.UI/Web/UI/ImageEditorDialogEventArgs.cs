using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E4C RID: 3660
	public class ImageEditorDialogEventArgs : EventArgs
	{
		// Token: 0x17002BD8 RID: 11224
		// (get) Token: 0x06008AD8 RID: 35544 RVA: 0x001F9FC7 File Offset: 0x001F81C7
		// (set) Token: 0x06008AD9 RID: 35545 RVA: 0x001F9FCF File Offset: 0x001F81CF
		public Control Panel { get; private set; }

		// Token: 0x17002BD9 RID: 11225
		// (get) Token: 0x06008ADA RID: 35546 RVA: 0x001F9FD8 File Offset: 0x001F81D8
		// (set) Token: 0x06008ADB RID: 35547 RVA: 0x001F9FE0 File Offset: 0x001F81E0
		public string DialogName { get; private set; }

		// Token: 0x06008ADC RID: 35548 RVA: 0x001F9FE9 File Offset: 0x001F81E9
		public ImageEditorDialogEventArgs(string dialogName, Control panel)
		{
			this.DialogName = dialogName;
			this.Panel = panel;
		}
	}
}
