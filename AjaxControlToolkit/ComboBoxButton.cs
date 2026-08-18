using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x0200006F RID: 111
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ComboBoxButton : WebControl
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000BFE2 File Offset: 0x0000A1E2
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Button;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000BFE6 File Offset: 0x0000A1E6
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
		}
	}
}
