using System;

namespace Telerik.Web.UI.ButtonBase
{
	// Token: 0x0200001C RID: 28
	public interface ICheckableButton
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000199 RID: 409
		// (set) Token: 0x0600019A RID: 410
		bool? Checked { get; set; }

		// Token: 0x0600019B RID: 411
		void OnCheckedChanged(EventArgs eventArgs);

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600019C RID: 412
		// (set) Token: 0x0600019D RID: 413
		string OnClientCheckedChanging { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600019E RID: 414
		// (set) Token: 0x0600019F RID: 415
		string OnClientCheckedChanged { get; set; }
	}
}
