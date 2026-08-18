using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200006C RID: 108
	[Designer(typeof(ColorPickerExtenderDesigner))]
	[ToolboxBitmap(typeof(Accessor), "ColorPicker.bmp")]
	[ClientScriptResource("Sys.Extended.UI.ColorPickerBehavior", "ColorPicker")]
	[RequiredScript(typeof(CommonToolkitScripts), 0)]
	[RequiredScript(typeof(PopupExtender), 1)]
	[RequiredScript(typeof(ThreadingScripts), 2)]
	[TargetControlType(typeof(TextBox))]
	[ClientCssResource("ColorPicker")]
	public class ColorPickerExtender : ExtenderControlBase
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000B139 File Offset: 0x00009339
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000B147 File Offset: 0x00009347
		[ClientPropertyName("enabled")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		public virtual bool EnabledOnClient
		{
			get
			{
				return base.GetPropertyValue<bool>("EnabledOnClient", true);
			}
			set
			{
				base.SetPropertyValue<bool>("EnabledOnClient", value);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000B155 File Offset: 0x00009355
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000B167 File Offset: 0x00009367
		[IDReferenceProperty]
		[ExtenderControlProperty]
		[ClientPropertyName("button")]
		[ElementReference]
		[DefaultValue("")]
		public virtual string PopupButtonID
		{
			get
			{
				return base.GetPropertyValue<string>("PopupButtonID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PopupButtonID", value);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000B175 File Offset: 0x00009375
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000B187 File Offset: 0x00009387
		[ClientPropertyName("sample")]
		[IDReferenceProperty]
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ElementReference]
		public virtual string SampleControlID
		{
			get
			{
				return base.GetPropertyValue<string>("SampleControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("SampleControlID", value);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000B195 File Offset: 0x00009395
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000B1A3 File Offset: 0x000093A3
		[DefaultValue(PositioningMode.BottomLeft)]
		[Description("Indicates where you want the color picker displayed relative to the textbox.")]
		[ExtenderControlProperty]
		[ClientPropertyName("popupPosition")]
		public virtual PositioningMode PopupPosition
		{
			get
			{
				return base.GetPropertyValue<PositioningMode>("PopupPosition", PositioningMode.BottomLeft);
			}
			set
			{
				base.SetPropertyValue<PositioningMode>("PopupPosition", value);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000B1B1 File Offset: 0x000093B1
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000B1C3 File Offset: 0x000093C3
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("selectedColor")]
		public string SelectedColor
		{
			get
			{
				return base.GetPropertyValue<string>("SelectedColor", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("SelectedColor", value);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000B1D1 File Offset: 0x000093D1
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000B1E3 File Offset: 0x000093E3
		[ClientPropertyName("showing")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public virtual string OnClientShowing
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientShowing", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientShowing", value);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000B1F1 File Offset: 0x000093F1
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000B203 File Offset: 0x00009403
		[DefaultValue("")]
		[ExtenderControlEvent]
		[ClientPropertyName("shown")]
		public virtual string OnClientShown
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientShown", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientShown", value);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000B211 File Offset: 0x00009411
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000B223 File Offset: 0x00009423
		[ExtenderControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("hiding")]
		public virtual string OnClientHiding
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientHiding", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientHiding", value);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000B231 File Offset: 0x00009431
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000B243 File Offset: 0x00009443
		[ClientPropertyName("hidden")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public virtual string OnClientHidden
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientHidden", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientHidden", value);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000B251 File Offset: 0x00009451
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000B263 File Offset: 0x00009463
		[ClientPropertyName("colorSelectionChanged")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public virtual string OnClientColorSelectionChanged
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientColorSelectionChanged", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientColorSelectionChanged", value);
			}
		}
	}
}
