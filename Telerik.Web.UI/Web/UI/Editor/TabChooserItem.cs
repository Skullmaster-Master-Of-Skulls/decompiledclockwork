using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D6 RID: 726
	public class TabChooserItem : StateManager
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x000530BC File Offset: 0x000512BC
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x000530DC File Offset: 0x000512DC
		[Description("Gets or sets the Name of the tab.")]
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return ((string)base.ViewState["Name"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}
	}
}
