using System;

namespace System.Windows.Forms
{
	// Token: 0x0200028B RID: 651
	public interface IContainerControl
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002996 RID: 10646
		// (set) Token: 0x06002997 RID: 10647
		Control ActiveControl { get; set; }

		// Token: 0x06002998 RID: 10648
		bool ActivateControl(Control active);
	}
}
