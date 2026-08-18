using System;
using System.ComponentModel;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200016C RID: 364
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxItem(false)]
	[ClientScriptResource("Sys.Extended.UI.DraggableListItem", "DraggableListItem")]
	[TargetControlType(typeof(ReorderListItem))]
	public class DraggableListItemExtender : ExtenderControlBase
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00018E34 File Offset: 0x00017034
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00018E46 File Offset: 0x00017046
		[ExtenderControlProperty]
		[ElementReference]
		[DefaultValue("")]
		[ClientPropertyName("handle")]
		[IDReferenceProperty(typeof(Control))]
		public string Handle
		{
			get
			{
				return base.GetPropertyValue<string>("handle", "");
			}
			set
			{
				base.SetPropertyValue<string>("handle", value);
			}
		}
	}
}
