using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200051B RID: 1307
	internal sealed class WizardSideBarListControlItemEventArgs : EventArgs
	{
		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x000D868B File Offset: 0x000D688B
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x000D8693 File Offset: 0x000D6893
		public WizardSideBarListControlItem Item { get; private set; }

		// Token: 0x06004243 RID: 16963 RVA: 0x000D869C File Offset: 0x000D689C
		public WizardSideBarListControlItemEventArgs(WizardSideBarListControlItem item)
		{
			this.Item = item;
		}
	}
}
