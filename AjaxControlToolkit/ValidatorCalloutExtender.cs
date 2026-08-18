using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x020001BB RID: 443
	[TargetControlType(typeof(IValidator))]
	[RequiredScript(typeof(AnimationExtender))]
	[ToolboxBitmap(typeof(Accessor), "ValidatorCallout.bmp")]
	[ClientCssResource("ValidatorCallout")]
	[ClientScriptResource("Sys.Extended.UI.ValidatorCalloutBehavior", "ValidatorCallout")]
	[Designer(typeof(ValidatorCalloutExtenderDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(PopupExtender))]
	public class ValidatorCalloutExtender : AnimationExtenderControlBase
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x00022964 File Offset: 0x00020B64
		public ValidatorCalloutExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00022973 File Offset: 0x00020B73
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0002299F File Offset: 0x00020B9F
		[DefaultValue("")]
		[UrlProperty]
		[ExtenderControlProperty]
		[ClientPropertyName("warningIconImageUrl")]
		public string WarningIconImageUrl
		{
			get
			{
				string result;
				if ((result = base.GetPropertyValue<string>("WarningIconImageUrl", null)) == null)
				{
					if (!base.DesignMode)
					{
						return ToolkitResourceManager.GetImageHref("ValidatorCallout.Alert-Large.gif", this, false);
					}
					result = string.Empty;
				}
				return result;
			}
			set
			{
				base.SetPropertyValue<string>("WarningIconImageUrl", value);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x000229AD File Offset: 0x00020BAD
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x000229D9 File Offset: 0x00020BD9
		[ExtenderControlProperty]
		[DefaultValue("")]
		[UrlProperty]
		[ClientPropertyName("closeImageUrl")]
		public string CloseImageUrl
		{
			get
			{
				string result;
				if ((result = base.GetPropertyValue<string>("CloseImageUrl", null)) == null)
				{
					if (!base.DesignMode)
					{
						return ToolkitResourceManager.GetImageHref("ValidatorCallout.Close.gif", this, false);
					}
					result = string.Empty;
				}
				return result;
			}
			set
			{
				base.SetPropertyValue<string>("CloseImageUrl", value);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000229E7 File Offset: 0x00020BE7
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x000229F9 File Offset: 0x00020BF9
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("cssClass")]
		public string CssClass
		{
			get
			{
				return base.GetPropertyValue<string>("CssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CssClass", value);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00022A07 File Offset: 0x00020C07
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00022A19 File Offset: 0x00020C19
		[ClientPropertyName("highlightCssClass")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string HighlightCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HighlightCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HighlightCssClass", value);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00022A27 File Offset: 0x00020C27
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00022A35 File Offset: 0x00020C35
		[ExtenderControlProperty]
		[ClientPropertyName("popupPosition")]
		[DefaultValue(ValidatorCalloutPosition.Right)]
		[Description("Indicates where you want the ValidatorCallout displayed.")]
		public virtual ValidatorCalloutPosition PopupPosition
		{
			get
			{
				return base.GetPropertyValue<ValidatorCalloutPosition>("PopupPosition", ValidatorCalloutPosition.Right);
			}
			set
			{
				base.SetPropertyValue<ValidatorCalloutPosition>("PopupPosition", value);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00022A43 File Offset: 0x00020C43
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x00022A55 File Offset: 0x00020C55
		[ClientPropertyName("width")]
		[ExtenderControlProperty]
		[DefaultValue(typeof(Unit), "")]
		public Unit Width
		{
			get
			{
				return base.GetPropertyValue<Unit>("Width", Unit.Empty);
			}
			set
			{
				base.SetPropertyValue<Unit>("Width", value);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00022A63 File Offset: 0x00020C63
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00022A76 File Offset: 0x00020C76
		[DefaultValue(null)]
		[ClientPropertyName("onShow")]
		[ExtenderControlProperty]
		[Browsable(false)]
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

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00022A8A File Offset: 0x00020C8A
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x00022A9D File Offset: 0x00020C9D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onHide")]
		[DefaultValue(null)]
		[Browsable(false)]
		[ExtenderControlProperty]
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

		// Token: 0x06000D02 RID: 3330 RVA: 0x00022AB4 File Offset: 0x00020CB4
		protected override void OnPreRender(EventArgs e)
		{
			BaseValidator baseValidator = base.TargetControl as BaseValidator;
			if (baseValidator != null && !baseValidator.IsValid)
			{
				base.ClientState = "INVALID";
			}
			else
			{
				base.ClientState = string.Empty;
			}
			base.OnPreRender(e);
			base.ResolveControlIDs(this._onShow);
			base.ResolveControlIDs(this._onHide);
		}

		// Token: 0x040004BE RID: 1214
		private Animation _onShow;

		// Token: 0x040004BF RID: 1215
		private Animation _onHide;
	}
}
