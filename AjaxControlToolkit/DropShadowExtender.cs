using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000080 RID: 128
	[RequiredScript(typeof(TimerScript), 3)]
	[ClientScriptResource("Sys.Extended.UI.DropShadowBehavior", "DropShadow")]
	[RequiredScript(typeof(RoundedCornersExtender), 2)]
	[Designer(typeof(DropShadowExtenderDesigner))]
	[TargetControlType(typeof(WebControl))]
	[TargetControlType(typeof(HtmlControl))]
	[ToolboxBitmap(typeof(Accessor), "DropShadow.bmp")]
	[RequiredScript(typeof(CommonToolkitScripts), 1)]
	public class DropShadowExtender : ExtenderControlBase
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000C828 File Offset: 0x0000AA28
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000C83A File Offset: 0x0000AA3A
		[ClientPropertyName("opacity")]
		[DefaultValue(1f)]
		[ExtenderControlProperty]
		public float Opacity
		{
			get
			{
				return base.GetPropertyValue<float>("Opacity", 1f);
			}
			set
			{
				base.SetPropertyValue<float>("Opacity", value);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000C848 File Offset: 0x0000AA48
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000C856 File Offset: 0x0000AA56
		[ClientPropertyName("width")]
		[ExtenderControlProperty]
		[DefaultValue(5)]
		public int Width
		{
			get
			{
				return base.GetPropertyValue<int>("Width", 5);
			}
			set
			{
				base.SetPropertyValue<int>("Width", value);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000C864 File Offset: 0x0000AA64
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000C872 File Offset: 0x0000AA72
		[ExtenderControlProperty]
		[ClientPropertyName("trackPosition")]
		[DefaultValue(false)]
		public bool TrackPosition
		{
			get
			{
				return base.GetPropertyValue<bool>("TrackPosition", false);
			}
			set
			{
				base.SetPropertyValue<bool>("TrackPosition", value);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000C880 File Offset: 0x0000AA80
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000C88E File Offset: 0x0000AA8E
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("rounded")]
		public bool Rounded
		{
			get
			{
				return base.GetPropertyValue<bool>("Rounded", false);
			}
			set
			{
				base.SetPropertyValue<bool>("Rounded", value);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000C89C File Offset: 0x0000AA9C
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0000C8AA File Offset: 0x0000AAAA
		[DefaultValue(5)]
		[ClientPropertyName("radius")]
		[ExtenderControlProperty]
		public int Radius
		{
			get
			{
				return base.GetPropertyValue<int>("Radius", 5);
			}
			set
			{
				base.SetPropertyValue<int>("Radius", value);
			}
		}
	}
}
