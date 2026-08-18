using System;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x02000265 RID: 613
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class ControlSkin
	{
		// Token: 0x06001D3F RID: 7487 RVA: 0x0005F1C4 File Offset: 0x0005D3C4
		public ControlSkin(Type controlType, ControlSkinDelegate themeDelegate)
		{
			this._controlType = controlType;
			this._controlSkinDelegate = themeDelegate;
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0005F1DA File Offset: 0x0005D3DA
		public Type ControlType
		{
			get
			{
				return this._controlType;
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0005F1E2 File Offset: 0x0005D3E2
		public void ApplySkin(Control control)
		{
			this._controlSkinDelegate(control);
		}

		// Token: 0x04001947 RID: 6471
		private Type _controlType;

		// Token: 0x04001948 RID: 6472
		private ControlSkinDelegate _controlSkinDelegate;
	}
}
