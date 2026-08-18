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
	// Token: 0x020000A5 RID: 165
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(HoverExtender))]
	[TargetControlType(typeof(HtmlControl))]
	[ToolboxBitmap(typeof(Accessor), "HoverMenu.bmp")]
	[Designer(typeof(HoverMenuExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.HoverMenuBehavior", "HoverMenu")]
	[RequiredScript(typeof(PopupExtender))]
	[RequiredScript(typeof(AnimationExtender))]
	[TargetControlType(typeof(WebControl))]
	public class HoverMenuExtender : DynamicPopulateExtenderControlBase
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000D778 File Offset: 0x0000B978
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0000D78A File Offset: 0x0000B98A
		[IDReferenceProperty(typeof(WebControl))]
		[ExtenderControlProperty]
		[ClientPropertyName("popupElement")]
		[RequiredProperty]
		[ElementReference]
		[DefaultValue("")]
		public string PopupControlID
		{
			get
			{
				return base.GetPropertyValue<string>("PopupControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PopupControlID", value);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000D798 File Offset: 0x0000B998
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000D7AA File Offset: 0x0000B9AA
		[ExtenderControlProperty]
		[ClientPropertyName("hoverCssClass")]
		[DefaultValue("")]
		public string HoverCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HoverCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HoverCssClass", value);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000D7C6 File Offset: 0x0000B9C6
		[ClientPropertyName("offsetX")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int OffsetX
		{
			get
			{
				return base.GetPropertyValue<int>("OffsetX", 0);
			}
			set
			{
				base.SetPropertyValue<int>("OffsetX", value);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000D7E2 File Offset: 0x0000B9E2
		[ClientPropertyName("offsetY")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int OffsetY
		{
			get
			{
				return base.GetPropertyValue<int>("OffsetY", 0);
			}
			set
			{
				base.SetPropertyValue<int>("OffsetY", value);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0000D7FE File Offset: 0x0000B9FE
		[ClientPropertyName("popDelay")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int PopDelay
		{
			get
			{
				return base.GetPropertyValue<int>("PopDelay", 0);
			}
			set
			{
				base.SetPropertyValue<int>("PopDelay", value);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000D80C File Offset: 0x0000BA0C
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0000D81A File Offset: 0x0000BA1A
		[DefaultValue(0)]
		[ExtenderControlProperty]
		[ClientPropertyName("hoverDelay")]
		public int HoverDelay
		{
			get
			{
				return base.GetPropertyValue<int>("HoverDelay", 0);
			}
			set
			{
				base.SetPropertyValue<int>("HoverDelay", value);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000D828 File Offset: 0x0000BA28
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0000D836 File Offset: 0x0000BA36
		[DefaultValue(HoverMenuPopupPosition.Center)]
		[ClientPropertyName("popupPosition")]
		[ExtenderControlProperty]
		public HoverMenuPopupPosition PopupPosition
		{
			get
			{
				return base.GetPropertyValue<HoverMenuPopupPosition>("Position", HoverMenuPopupPosition.Center);
			}
			set
			{
				base.SetPropertyValue<HoverMenuPopupPosition>("Position", value);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000D844 File Offset: 0x0000BA44
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x0000D857 File Offset: 0x0000BA57
		[ClientPropertyName("onShow")]
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		public Animation OnShow
		{
			get
			{
				return base.GetAnimation(ref this._onShow, "OnShow");
			}
			set
			{
				base.SetAnimation(ref this._onShow, "OnShow", value);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000D86B File Offset: 0x0000BA6B
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0000D87E File Offset: 0x0000BA7E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("onHide")]
		[Browsable(false)]
		public Animation OnHide
		{
			get
			{
				return base.GetAnimation(ref this._onHide, "OnHide");
			}
			set
			{
				base.SetAnimation(ref this._onHide, "OnHide", value);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000D892 File Offset: 0x0000BA92
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.ResolveControlIDs(this._onShow);
			base.ResolveControlIDs(this._onHide);
		}

		// Token: 0x040002C4 RID: 708
		private Animation _onShow;

		// Token: 0x040002C5 RID: 709
		private Animation _onHide;
	}
}
