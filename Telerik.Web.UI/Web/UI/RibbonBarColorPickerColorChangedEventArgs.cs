using System;
using System.Drawing;

namespace Telerik.Web.UI
{
	// Token: 0x02000E2B RID: 3627
	public class RibbonBarColorPickerColorChangedEventArgs : EventArgs
	{
		// Token: 0x17002B79 RID: 11129
		// (get) Token: 0x0600896A RID: 35178 RVA: 0x001F5C1B File Offset: 0x001F3E1B
		public RibbonBarColorPicker ColorPicker
		{
			get
			{
				return this._colorPicker;
			}
		}

		// Token: 0x17002B7A RID: 11130
		// (get) Token: 0x0600896B RID: 35179 RVA: 0x001F5C23 File Offset: 0x001F3E23
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002B7B RID: 11131
		// (get) Token: 0x0600896C RID: 35180 RVA: 0x001F5C2B File Offset: 0x001F3E2B
		public Color Color
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x0600896D RID: 35181 RVA: 0x001F5C33 File Offset: 0x001F3E33
		public RibbonBarColorPickerColorChangedEventArgs(Color color, RibbonBarColorPicker colorPicker, RibbonBarGroup group)
		{
			this._group = group;
			this._colorPicker = colorPicker;
			this._color = color;
		}

		// Token: 0x0400266D RID: 9837
		private RibbonBarGroup _group;

		// Token: 0x0400266E RID: 9838
		private RibbonBarColorPicker _colorPicker;

		// Token: 0x0400266F RID: 9839
		private Color _color;
	}
}
