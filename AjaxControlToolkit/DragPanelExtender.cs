using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200007A RID: 122
	[TargetControlType(typeof(WebControl))]
	[Designer(typeof(DragPanelExtenderDesigner))]
	[RequiredScript(typeof(DragDropScripts))]
	[ClientScriptResource("Sys.Extended.UI.FloatingBehavior", "FloatingBehavior")]
	[ToolboxBitmap(typeof(Accessor), "DragPanel.bmp")]
	public class DragPanelExtender : ExtenderControlBase
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000C514 File Offset: 0x0000A714
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000C542 File Offset: 0x0000A742
		[ExtenderControlProperty]
		[RequiredProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[ElementReference]
		[ClientPropertyName("handle")]
		public string DragHandleID
		{
			get
			{
				string text = base.GetPropertyValue<string>("DragHandleID", "");
				if (string.IsNullOrEmpty(text))
				{
					text = base.TargetControlID;
				}
				return text;
			}
			set
			{
				base.SetPropertyValue<string>("DragHandleID", value);
			}
		}
	}
}
