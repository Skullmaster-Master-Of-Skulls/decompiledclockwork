using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AE0 RID: 6880
	public class RadToolBarButtonEventArgs : EventArgs
	{
		// Token: 0x17005125 RID: 20773
		// (get) Token: 0x06010ACE RID: 68302 RVA: 0x003B75E8 File Offset: 0x003B57E8
		public RadToolBarButton Button
		{
			get
			{
				return this._button;
			}
		}

		// Token: 0x06010ACF RID: 68303 RVA: 0x003B75F0 File Offset: 0x003B57F0
		public RadToolBarButtonEventArgs(RadToolBarButton button)
		{
			this._button = button;
		}

		// Token: 0x04004A65 RID: 19045
		private RadToolBarButton _button;
	}
}
