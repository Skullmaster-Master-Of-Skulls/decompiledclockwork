using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x020001A7 RID: 423
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(TextBox))]
	[ToolboxBitmap(typeof(Accessor), "TextBoxWatermark.bmp")]
	[ClientScriptResource("Sys.Extended.UI.TextBoxWatermarkBehavior", "TextBoxWatermark")]
	[Designer(typeof(TextBoxWatermarkExtenderDesigner))]
	public class TextBoxWatermarkExtender : ExtenderControlBase
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x0002000E File Offset: 0x0001E20E
		public TextBoxWatermarkExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00020020 File Offset: 0x0001E220
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ScriptManager.RegisterOnSubmitStatement(this, typeof(TextBoxWatermarkExtender), "TextBoxWatermarkExtenderOnSubmit", "null;");
			base.ClientState = ((string.Compare(this.Page.Form.DefaultFocus, base.TargetControlID, StringComparison.OrdinalIgnoreCase) == 0) ? "Focused" : null);
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0002007A File Offset: 0x0001E27A
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0002008C File Offset: 0x0001E28C
		[ClientPropertyName("watermarkText")]
		[ExtenderControlProperty]
		[RequiredProperty]
		[DefaultValue("")]
		public string WatermarkText
		{
			get
			{
				return base.GetPropertyValue<string>("WatermarkText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("WatermarkText", value);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0002009A File Offset: 0x0001E29A
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x000200AC File Offset: 0x0001E2AC
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("watermarkCssClass")]
		public string WatermarkCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("WatermarkCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("WatermarkCssClass", value);
			}
		}

		// Token: 0x04000480 RID: 1152
		private const string stringWatermarkText = "WatermarkText";

		// Token: 0x04000481 RID: 1153
		private const string stringWatermarkCssClass = "WatermarkCssClass";
	}
}
