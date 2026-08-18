using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E41 RID: 3649
	public class RibbonBarSelectedTabChangeEventArgs : EventArgs
	{
		// Token: 0x17002BD0 RID: 11216
		// (get) Token: 0x06008AAB RID: 35499 RVA: 0x001F9EAB File Offset: 0x001F80AB
		// (set) Token: 0x06008AAC RID: 35500 RVA: 0x001F9EB3 File Offset: 0x001F80B3
		public RibbonBarTab PreviouslySelectedTab { get; private set; }

		// Token: 0x17002BD1 RID: 11217
		// (get) Token: 0x06008AAD RID: 35501 RVA: 0x001F9EBC File Offset: 0x001F80BC
		// (set) Token: 0x06008AAE RID: 35502 RVA: 0x001F9EC4 File Offset: 0x001F80C4
		public RibbonBarTab Tab { get; private set; }

		// Token: 0x06008AAF RID: 35503 RVA: 0x001F9ECD File Offset: 0x001F80CD
		public RibbonBarSelectedTabChangeEventArgs(RibbonBarTab previouslySelectedTab, RibbonBarTab selectedTab)
		{
			this.Tab = selectedTab;
			this.PreviouslySelectedTab = previouslySelectedTab;
		}
	}
}
