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
	// Token: 0x0200007E RID: 126
	[ClientCssResource("DropDown")]
	[TargetControlType(typeof(HtmlControl))]
	[RequiredScript(typeof(PopupExtender))]
	[RequiredScript(typeof(HoverExtender))]
	[RequiredScript(typeof(AnimationExtender))]
	[TargetControlType(typeof(WebControl))]
	[ClientScriptResource("Sys.Extended.UI.DropDownBehavior", "DropDown")]
	[Designer(typeof(DropDownExtenderDesigner))]
	[ToolboxBitmap(typeof(Accessor), "DropDown.bmp")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class DropDownExtender : DynamicPopulateExtenderControlBase
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000C568 File Offset: 0x0000A768
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000C588 File Offset: 0x0000A788
		[ClientPropertyName("dropDownControl")]
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(Control))]
		[DefaultValue("")]
		[ElementReference]
		public string DropDownControlID
		{
			get
			{
				return (string)(this.ViewState["DropDownControlID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DropDownControlID"] = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000C59B File Offset: 0x0000A79B
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		[ClientPropertyName("highlightBorderColor")]
		[DefaultValue(typeof(Color), "")]
		[ExtenderControlProperty]
		public Color HighlightBorderColor
		{
			get
			{
				return (Color)(this.ViewState["HighlightBorderColor"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["HighlightBorderColor"] = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000C5FD File Offset: 0x0000A7FD
		[ExtenderControlProperty]
		[DefaultValue(typeof(Color), "")]
		[ClientPropertyName("highlightBackgroundColor")]
		public Color HighlightBackColor
		{
			get
			{
				return (Color)(this.ViewState["HighlightBackColor"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["HighlightBackColor"] = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000C615 File Offset: 0x0000A815
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000C63A File Offset: 0x0000A83A
		[ExtenderControlProperty]
		[DefaultValue(typeof(Color), "")]
		[ClientPropertyName("dropArrowBackgroundColor")]
		public Color DropArrowBackColor
		{
			get
			{
				return (Color)(this.ViewState["DropArrowBackColor"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["DropArrowBackColor"] = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000C652 File Offset: 0x0000A852
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000C672 File Offset: 0x0000A872
		[DefaultValue("")]
		[UrlProperty]
		[ExtenderControlProperty]
		[ClientPropertyName("dropArrowImageUrl")]
		public string DropArrowImageUrl
		{
			get
			{
				return (string)(this.ViewState["DropArrowImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DropArrowImageUrl"] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000C685 File Offset: 0x0000A885
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000C6AA File Offset: 0x0000A8AA
		[ExtenderControlProperty]
		[ClientPropertyName("dropArrowWidth")]
		[DefaultValue(typeof(Unit), "")]
		public Unit DropArrowWidth
		{
			get
			{
				return (Unit)(this.ViewState["DropArrowWidth"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["DropArrowWidth"] = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000C6E2 File Offset: 0x0000A8E2
		[Category("Behavior")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		[ClientPropertyName("popup")]
		public string OnClientPopup
		{
			get
			{
				return (string)(this.ViewState["OnClientPopup"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPopup"] = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000C6F5 File Offset: 0x0000A8F5
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000C715 File Offset: 0x0000A915
		[DefaultValue("")]
		[Category("Behavior")]
		[ExtenderControlEvent]
		[ClientPropertyName("populating")]
		public string OnClientPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPopulating"] = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000C728 File Offset: 0x0000A928
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000C748 File Offset: 0x0000A948
		[ClientPropertyName("populated")]
		[DefaultValue("")]
		[Category("Behavior")]
		[ExtenderControlEvent]
		public string OnClientPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPopulated"] = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000C75B File Offset: 0x0000A95B
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000C76E File Offset: 0x0000A96E
		[ExtenderControlProperty]
		[ClientPropertyName("onShow")]
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000C782 File Offset: 0x0000A982
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000C795 File Offset: 0x0000A995
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		[ClientPropertyName("onHide")]
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

		// Token: 0x06000459 RID: 1113 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if ((!string.IsNullOrEmpty(base.DynamicContextKey) || !string.IsNullOrEmpty(base.DynamicServicePath) || !string.IsNullOrEmpty(base.DynamicServiceMethod)) && string.IsNullOrEmpty(base.DynamicControlID))
			{
				base.DynamicControlID = this.DropDownControlID;
			}
			base.ResolveControlIDs(this._onShow);
			base.ResolveControlIDs(this._onHide);
		}

		// Token: 0x04000143 RID: 323
		private Animation _onShow;

		// Token: 0x04000144 RID: 324
		private Animation _onHide;
	}
}
