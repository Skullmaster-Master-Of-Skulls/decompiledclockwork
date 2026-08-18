using System;

namespace Telerik.Web.UI.Menu.Views
{
	// Token: 0x020005E2 RID: 1506
	[ViewDescriptor(typeof(RadMenu), "Telerik.Web.UI.Menu.Views.ClassicView.js", RenderMode.Classic, LoadOrder = 1)]
	[ViewDescriptor(typeof(RadMenu), "Telerik.Web.UI.Menu.MenuItem.RadMenuItem.js", RenderMode.Classic, LoadOrder = 0)]
	public static class ClassicView
	{
		// Token: 0x04000EBD RID: 3773
		public const string PrototypeResourceName = "Telerik.Web.UI.Menu.MenuItem.RadMenuItem.js";

		// Token: 0x04000EBE RID: 3774
		public const string ViewResourceName = "Telerik.Web.UI.Menu.Views.ClassicView.js";
	}
}
