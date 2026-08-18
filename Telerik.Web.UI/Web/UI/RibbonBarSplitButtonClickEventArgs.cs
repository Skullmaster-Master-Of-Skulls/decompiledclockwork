using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F3C RID: 3900
	public class RibbonBarSplitButtonClickEventArgs : EventArgs
	{
		// Token: 0x17002F13 RID: 12051
		// (get) Token: 0x060094C1 RID: 38081 RVA: 0x00214BAB File Offset: 0x00212DAB
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17002F14 RID: 12052
		// (get) Token: 0x060094C2 RID: 38082 RVA: 0x00214BB3 File Offset: 0x00212DB3
		public RibbonBarSplitButton SplitButton
		{
			get
			{
				return this._splitButton;
			}
		}

		// Token: 0x17002F15 RID: 12053
		// (get) Token: 0x060094C3 RID: 38083 RVA: 0x00214BBB File Offset: 0x00212DBB
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002F16 RID: 12054
		// (get) Token: 0x060094C4 RID: 38084 RVA: 0x00214BC3 File Offset: 0x00212DC3
		public RibbonBarButton Button
		{
			get
			{
				return this._button;
			}
		}

		// Token: 0x060094C5 RID: 38085 RVA: 0x00214BCB File Offset: 0x00212DCB
		public RibbonBarSplitButtonClickEventArgs(RibbonBarButton button, RibbonBarSplitButton splitButton, RibbonBarGroup group)
		{
			this._button = button;
			this._splitButton = splitButton;
			this._group = group;
			this._index = splitButton.Buttons.IndexOf(button);
		}

		// Token: 0x04002A93 RID: 10899
		private RibbonBarGroup _group;

		// Token: 0x04002A94 RID: 10900
		private RibbonBarSplitButton _splitButton;

		// Token: 0x04002A95 RID: 10901
		private RibbonBarButton _button;

		// Token: 0x04002A96 RID: 10902
		private int _index;
	}
}
