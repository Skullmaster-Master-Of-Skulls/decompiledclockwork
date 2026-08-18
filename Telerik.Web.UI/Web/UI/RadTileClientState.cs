using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200090C RID: 2316
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadTileClientState
	{
		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x0600578F RID: 22415 RVA: 0x0010BA2E File Offset: 0x00109C2E
		// (set) Token: 0x06005790 RID: 22416 RVA: 0x0010BA36 File Offset: 0x00109C36
		public bool Selected { get; set; }

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x06005791 RID: 22417 RVA: 0x0010BA3F File Offset: 0x00109C3F
		// (set) Token: 0x06005792 RID: 22418 RVA: 0x0010BA47 File Offset: 0x00109C47
		public bool IsEnabled { get; set; }

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x06005793 RID: 22419 RVA: 0x0010BA50 File Offset: 0x00109C50
		// (set) Token: 0x06005794 RID: 22420 RVA: 0x0010BA58 File Offset: 0x00109C58
		public bool? Visible { get; set; }
	}
}
