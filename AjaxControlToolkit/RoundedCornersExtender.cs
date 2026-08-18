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
	// Token: 0x020001A0 RID: 416
	[TargetControlType(typeof(HtmlControl))]
	[Designer(typeof(RoundedCornersExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.RoundedCornersBehavior", "RoundedCorners")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(WebControl))]
	[ToolboxBitmap(typeof(Accessor), "RoundedCorners.bmp")]
	public class RoundedCornersExtender : ExtenderControlBase
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0001FC84 File Offset: 0x0001DE84
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0001FC92 File Offset: 0x0001DE92
		[ClientPropertyName("radius")]
		[DefaultValue(5)]
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

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0001FCAF File Offset: 0x0001DEAF
		[DefaultValue(BoxCorners.All)]
		[ClientPropertyName("corners")]
		[ExtenderControlProperty]
		public BoxCorners Corners
		{
			get
			{
				return base.GetPropertyValue<BoxCorners>("Corners", BoxCorners.All);
			}
			set
			{
				base.SetPropertyValue<BoxCorners>("Corners", value);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001FCBD File Offset: 0x0001DEBD
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0001FCCF File Offset: 0x0001DECF
		[ClientPropertyName("color")]
		[DefaultValue(typeof(Color), "")]
		[ExtenderControlProperty]
		public Color Color
		{
			get
			{
				return base.GetPropertyValue<Color>("Color", Color.Empty);
			}
			set
			{
				base.SetPropertyValue<Color>("Color", value);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001FCDD File Offset: 0x0001DEDD
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0001FCEF File Offset: 0x0001DEEF
		[ClientPropertyName("borderColor")]
		[ExtenderControlProperty]
		[DefaultValue(typeof(Color), "")]
		public Color BorderColor
		{
			get
			{
				return base.GetPropertyValue<Color>("BorderColor", Color.Empty);
			}
			set
			{
				base.SetPropertyValue<Color>("BorderColor", value);
			}
		}
	}
}
