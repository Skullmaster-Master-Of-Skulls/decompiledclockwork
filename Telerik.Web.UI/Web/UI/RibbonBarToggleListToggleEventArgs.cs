using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F2F RID: 3887
	public class RibbonBarToggleListToggleEventArgs : EventArgs
	{
		// Token: 0x17002ED8 RID: 11992
		// (get) Token: 0x06009429 RID: 37929 RVA: 0x00213B1D File Offset: 0x00211D1D
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17002ED9 RID: 11993
		// (get) Token: 0x0600942A RID: 37930 RVA: 0x00213B25 File Offset: 0x00211D25
		public RibbonBarToggleList ToggleList
		{
			get
			{
				return this._toggleList;
			}
		}

		// Token: 0x17002EDA RID: 11994
		// (get) Token: 0x0600942B RID: 37931 RVA: 0x00213B2D File Offset: 0x00211D2D
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002EDB RID: 11995
		// (get) Token: 0x0600942C RID: 37932 RVA: 0x00213B35 File Offset: 0x00211D35
		public RibbonBarToggleButton ToggleButton
		{
			get
			{
				return this._toggleButton;
			}
		}

		// Token: 0x17002EDC RID: 11996
		// (get) Token: 0x0600942D RID: 37933 RVA: 0x00213B3D File Offset: 0x00211D3D
		public RibbonBarToggleButton[] ToggleListButtons
		{
			get
			{
				return this._toggleListButtons;
			}
		}

		// Token: 0x0600942E RID: 37934 RVA: 0x00213B45 File Offset: 0x00211D45
		public RibbonBarToggleListToggleEventArgs(RibbonBarToggleButton toggleButton, RibbonBarToggleList toggleList, RibbonBarGroup group)
		{
			this._group = group;
			this._toggleList = toggleList;
			this._toggleButton = toggleButton;
			this._toggleListButtons = toggleList.ToggleButtons.ToArray();
			this._index = toggleList.ToggleButtons.IndexOf(toggleButton);
		}

		// Token: 0x04002A6E RID: 10862
		private RibbonBarGroup _group;

		// Token: 0x04002A6F RID: 10863
		private RibbonBarToggleList _toggleList;

		// Token: 0x04002A70 RID: 10864
		private RibbonBarToggleButton _toggleButton;

		// Token: 0x04002A71 RID: 10865
		private RibbonBarToggleButton[] _toggleListButtons;

		// Token: 0x04002A72 RID: 10866
		private int _index;
	}
}
