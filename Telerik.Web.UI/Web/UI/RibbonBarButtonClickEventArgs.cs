using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F41 RID: 3905
	public class RibbonBarButtonClickEventArgs : EventArgs
	{
		// Token: 0x17002F1C RID: 12060
		// (get) Token: 0x060094D8 RID: 38104 RVA: 0x00214C94 File Offset: 0x00212E94
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17002F1D RID: 12061
		// (get) Token: 0x060094D9 RID: 38105 RVA: 0x00214C9C File Offset: 0x00212E9C
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002F1E RID: 12062
		// (get) Token: 0x060094DA RID: 38106 RVA: 0x00214CA4 File Offset: 0x00212EA4
		public RibbonBarButton Button
		{
			get
			{
				return this._button;
			}
		}

		// Token: 0x060094DB RID: 38107 RVA: 0x00214CAC File Offset: 0x00212EAC
		public RibbonBarButtonClickEventArgs(RibbonBarButton button, RibbonBarGroup group)
		{
			this._button = button;
			this._group = group;
			this._index = group.GetFunctionalItems().IndexOf(button);
		}

		// Token: 0x04002A9C RID: 10908
		private RibbonBarButton _button;

		// Token: 0x04002A9D RID: 10909
		private RibbonBarGroup _group;

		// Token: 0x04002A9E RID: 10910
		private int _index = -1;
	}
}
