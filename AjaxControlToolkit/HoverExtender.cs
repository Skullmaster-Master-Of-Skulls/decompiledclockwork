using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit
{
	// Token: 0x020000A3 RID: 163
	[ClientScriptResource("Sys.Extended.UI.HoverBehavior", "Hover")]
	[TargetControlType(typeof(HtmlControl))]
	[ToolboxItem(false)]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[Designer(typeof(HoverExtenderDesigner))]
	[TargetControlType(typeof(WebControl))]
	public class HoverExtender : ExtenderControlBase
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0000D6FE File Offset: 0x0000B8FE
		[ExtenderControlProperty]
		[ClientPropertyName("hoverDelay")]
		[DefaultValue(0)]
		public int HoverDelay
		{
			get
			{
				return base.GetPropertyValue<int>("hoverDelay", 0);
			}
			set
			{
				base.SetPropertyValue<int>("hoverDelay", value);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000D70C File Offset: 0x0000B90C
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0000D71E File Offset: 0x0000B91E
		[ClientPropertyName("hoverScript")]
		[ExtenderControlProperty]
		public string HoverScript
		{
			get
			{
				return base.GetPropertyValue<string>("HoverScript", "");
			}
			set
			{
				base.SetPropertyValue<string>("HoverScript", value);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000D72C File Offset: 0x0000B92C
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0000D73A File Offset: 0x0000B93A
		[ExtenderControlProperty]
		[DefaultValue(0)]
		[ClientPropertyName("unhoverDelay")]
		public int UnhoverDelay
		{
			get
			{
				return base.GetPropertyValue<int>("UnhoverDelay", 0);
			}
			set
			{
				base.SetPropertyValue<int>("UnhoverDelay", value);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000D748 File Offset: 0x0000B948
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0000D75A File Offset: 0x0000B95A
		[ClientPropertyName("unhoverScript")]
		[ExtenderControlProperty]
		public string UnhoverScript
		{
			get
			{
				return base.GetPropertyValue<string>("UnhoverScript", "");
			}
			set
			{
				base.SetPropertyValue<string>("UnhoverScript", value);
			}
		}
	}
}
