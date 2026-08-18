using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F3A RID: 3898
	public class RibbonBarLauncherClickEventArgs : EventArgs
	{
		// Token: 0x17002F12 RID: 12050
		// (get) Token: 0x060094BB RID: 38075 RVA: 0x00214B94 File Offset: 0x00212D94
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x060094BC RID: 38076 RVA: 0x00214B9C File Offset: 0x00212D9C
		public RibbonBarLauncherClickEventArgs(RibbonBarGroup group)
		{
			this._group = group;
		}

		// Token: 0x04002A92 RID: 10898
		private RibbonBarGroup _group;
	}
}
