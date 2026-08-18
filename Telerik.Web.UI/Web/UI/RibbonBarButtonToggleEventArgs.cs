using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F33 RID: 3891
	public class RibbonBarButtonToggleEventArgs : EventArgs
	{
		// Token: 0x17002EE4 RID: 12004
		// (get) Token: 0x0600944C RID: 37964 RVA: 0x00213FAC File Offset: 0x002121AC
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17002EE5 RID: 12005
		// (get) Token: 0x0600944D RID: 37965 RVA: 0x00213FB4 File Offset: 0x002121B4
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002EE6 RID: 12006
		// (get) Token: 0x0600944E RID: 37966 RVA: 0x00213FBC File Offset: 0x002121BC
		public RibbonBarToggleButton Button
		{
			get
			{
				return this._button;
			}
		}

		// Token: 0x0600944F RID: 37967 RVA: 0x00213FC4 File Offset: 0x002121C4
		public RibbonBarButtonToggleEventArgs(RibbonBarToggleButton button, RibbonBarGroup group)
		{
			this._group = group;
			this._button = button;
			this._index = group.GetFunctionalItems().IndexOf(button);
		}

		// Token: 0x04002A77 RID: 10871
		private RibbonBarGroup _group;

		// Token: 0x04002A78 RID: 10872
		private RibbonBarToggleButton _button;

		// Token: 0x04002A79 RID: 10873
		private int _index;
	}
}
