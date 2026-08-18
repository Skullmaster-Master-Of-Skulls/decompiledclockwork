using System;

namespace System.Windows.Forms
{
	// Token: 0x02000289 RID: 649
	public interface IButtonControl
	{
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002991 RID: 10641
		// (set) Token: 0x06002992 RID: 10642
		DialogResult DialogResult { get; set; }

		// Token: 0x06002993 RID: 10643
		void NotifyDefault(bool value);

		// Token: 0x06002994 RID: 10644
		void PerformClick();
	}
}
