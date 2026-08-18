using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F14 RID: 3860
	public interface IRadTabContainer
	{
		// Token: 0x17002E3A RID: 11834
		// (get) Token: 0x0600924D RID: 37453
		IRadTabContainer Owner { get; }

		// Token: 0x17002E3B RID: 11835
		// (get) Token: 0x0600924E RID: 37454
		RadTabCollection Tabs { get; }

		// Token: 0x17002E3C RID: 11836
		// (get) Token: 0x0600924F RID: 37455
		// (set) Token: 0x06009250 RID: 37456
		int SelectedIndex { get; set; }

		// Token: 0x17002E3D RID: 11837
		// (get) Token: 0x06009251 RID: 37457
		RadTab SelectedTab { get; }

		// Token: 0x17002E3E RID: 11838
		// (get) Token: 0x06009252 RID: 37458
		// (set) Token: 0x06009253 RID: 37459
		bool ScrollChildren { get; set; }

		// Token: 0x17002E3F RID: 11839
		// (get) Token: 0x06009254 RID: 37460
		// (set) Token: 0x06009255 RID: 37461
		bool PerTabScrolling { get; set; }

		// Token: 0x17002E40 RID: 11840
		// (get) Token: 0x06009256 RID: 37462
		// (set) Token: 0x06009257 RID: 37463
		TabStripScrollButtonsPosition ScrollButtonsPosition { get; set; }

		// Token: 0x17002E41 RID: 11841
		// (get) Token: 0x06009258 RID: 37464
		// (set) Token: 0x06009259 RID: 37465
		int ScrollPosition { get; set; }
	}
}
