using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020012A0 RID: 4768
	[RequiredScript(typeof(DialogControlInitializer))]
	[ToolboxItem(false)]
	public class Help : UserControlBase
	{
		// Token: 0x17004091 RID: 16529
		// (get) Token: 0x0600C7D8 RID: 51160 RVA: 0x002C8035 File Offset: 0x002C6235
		public override string DialogName
		{
			get
			{
				return "Help";
			}
		}
	}
}
