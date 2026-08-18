using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200009C RID: 156
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.FilteredTextBoxBehavior", "FilteredTextBox")]
	[DefaultProperty("ValidChars")]
	[ToolboxBitmap(typeof(Accessor), "FilteredTextBox.bmp")]
	[TargetControlType(typeof(TextBox))]
	[Designer(typeof(FilteredTextBoxExtenderDesigner))]
	public class FilteredTextBoxExtender : ExtenderControlBase
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000D373 File Offset: 0x0000B573
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000D381 File Offset: 0x0000B581
		[ClientPropertyName("filterType")]
		[ExtenderControlProperty]
		[DefaultValue(FilterTypes.Custom)]
		public FilterTypes FilterType
		{
			get
			{
				return base.GetPropertyValue<FilterTypes>("FilterType", FilterTypes.Custom);
			}
			set
			{
				base.SetPropertyValue<FilterTypes>("FilterType", value);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000D38F File Offset: 0x0000B58F
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000D39D File Offset: 0x0000B59D
		[DefaultValue(FilterModes.ValidChars)]
		[ExtenderControlProperty]
		[ClientPropertyName("filterMode")]
		public FilterModes FilterMode
		{
			get
			{
				return base.GetPropertyValue<FilterModes>("FilterMode", FilterModes.ValidChars);
			}
			set
			{
				base.SetPropertyValue<FilterModes>("FilterMode", value);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000D3AB File Offset: 0x0000B5AB
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000D3BD File Offset: 0x0000B5BD
		[ClientPropertyName("validChars")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string ValidChars
		{
			get
			{
				return base.GetPropertyValue<string>("ValidChars", "");
			}
			set
			{
				base.SetPropertyValue<string>("ValidChars", value);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000D3CB File Offset: 0x0000B5CB
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000D3DD File Offset: 0x0000B5DD
		[ClientPropertyName("invalidChars")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string InvalidChars
		{
			get
			{
				return base.GetPropertyValue<string>("InvalidChars", "");
			}
			set
			{
				base.SetPropertyValue<string>("InvalidChars", value);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000D3EB File Offset: 0x0000B5EB
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000D3FD File Offset: 0x0000B5FD
		[DefaultValue(250)]
		[ClientPropertyName("filterInterval")]
		[ExtenderControlProperty]
		public int FilterInterval
		{
			get
			{
				return base.GetPropertyValue<int>("FilterInterval", 250);
			}
			set
			{
				base.SetPropertyValue<int>("FilterInterval", value);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000D40C File Offset: 0x0000B60C
		protected override bool CheckIfValid(bool throwException)
		{
			if (this.FilterType != FilterTypes.Custom || ((this.FilterMode != FilterModes.ValidChars || !string.IsNullOrEmpty(this.ValidChars)) && (this.FilterMode != FilterModes.InvalidChars || !string.IsNullOrEmpty(this.InvalidChars))))
			{
				return base.CheckIfValid(throwException);
			}
			if (throwException)
			{
				throw new InvalidOperationException("If FilterTypes.Custom is specified, please provide a value for ValidChars or InvalidChars");
			}
			return false;
		}
	}
}
