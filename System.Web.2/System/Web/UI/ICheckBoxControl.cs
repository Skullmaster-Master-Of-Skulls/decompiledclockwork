using System;

namespace System.Web.UI
{
	// Token: 0x0200032F RID: 815
	public interface ICheckBoxControl
	{
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060025E5 RID: 9701
		// (set) Token: 0x060025E6 RID: 9702
		bool Checked { get; set; }

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060025E7 RID: 9703
		// (remove) Token: 0x060025E8 RID: 9704
		event EventHandler CheckedChanged;
	}
}
