using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200014A RID: 330
	[ToolboxBitmap(typeof(Accessor), "MutuallyExclusiveCheckBox.bmp")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.MutuallyExclusiveCheckBoxBehavior", "MutuallyExclusiveCheckBox")]
	[TargetControlType(typeof(ICheckBoxControl))]
	[Designer(typeof(MutuallyExclusiveCheckBoxExtenderDesigner))]
	public class MutuallyExclusiveCheckBoxExtender : ExtenderControlBase
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00017218 File Offset: 0x00015418
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001722A File Offset: 0x0001542A
		[ExtenderControlProperty]
		[RequiredProperty]
		[ClientPropertyName("key")]
		public string Key
		{
			get
			{
				return base.GetPropertyValue<string>("Key", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("Key", value);
			}
		}
	}
}
