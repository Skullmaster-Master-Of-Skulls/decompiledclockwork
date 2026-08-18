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
	// Token: 0x02000032 RID: 50
	[ToolboxBitmap(typeof(Accessor), "Animation.bmp")]
	[TargetControlType(typeof(HtmlControl))]
	[Designer(typeof(AnimationExtenderDesigner))]
	[RequiredScript(typeof(AnimationScripts))]
	[ClientScriptResource("Sys.Extended.UI.Animation.AnimationBehavior", "Animation")]
	[TargetControlType(typeof(WebControl))]
	public class AnimationExtender : AnimationExtenderControlBase
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006B00 File Offset: 0x00004D00
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00006B13 File Offset: 0x00004D13
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onLoad")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public new Animation OnLoad
		{
			get
			{
				return base.GetAnimation(ref this._onLoad, "OnLoad");
			}
			set
			{
				base.SetAnimation(ref this._onLoad, "OnLoad", value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006B27 File Offset: 0x00004D27
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00006B3A File Offset: 0x00004D3A
		[ClientPropertyName("onClick")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[ExtenderControlProperty]
		public Animation OnClick
		{
			get
			{
				return base.GetAnimation(ref this._onClick, "OnClick");
			}
			set
			{
				base.SetAnimation(ref this._onClick, "OnClick", value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006B4E File Offset: 0x00004D4E
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006B61 File Offset: 0x00004D61
		[DefaultValue(null)]
		[Browsable(false)]
		[ClientPropertyName("onMouseOver")]
		[ExtenderControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Animation OnMouseOver
		{
			get
			{
				return base.GetAnimation(ref this._onMouseOver, "OnMouseOver");
			}
			set
			{
				base.SetAnimation(ref this._onMouseOver, "OnMouseOver", value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006B75 File Offset: 0x00004D75
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00006B88 File Offset: 0x00004D88
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onMouseOut")]
		[ExtenderControlProperty]
		[DefaultValue(null)]
		[Browsable(false)]
		public Animation OnMouseOut
		{
			get
			{
				return base.GetAnimation(ref this._onMouseOut, "OnMouseOut");
			}
			set
			{
				base.SetAnimation(ref this._onMouseOut, "OnMouseOut", value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006B9C File Offset: 0x00004D9C
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00006BAF File Offset: 0x00004DAF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(null)]
		[ClientPropertyName("onHoverOver")]
		[ExtenderControlProperty]
		public Animation OnHoverOver
		{
			get
			{
				return base.GetAnimation(ref this._onHoverOver, "OnHoverOver");
			}
			set
			{
				base.SetAnimation(ref this._onHoverOver, "OnHoverOver", value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006BC3 File Offset: 0x00004DC3
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006BD6 File Offset: 0x00004DD6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		[ClientPropertyName("onHoverOut")]
		[DefaultValue(null)]
		[Browsable(false)]
		public Animation OnHoverOut
		{
			get
			{
				return base.GetAnimation(ref this._onHoverOut, "OnHoverOut");
			}
			set
			{
				base.SetAnimation(ref this._onHoverOut, "OnHoverOut", value);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006BEC File Offset: 0x00004DEC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.ResolveControlIDs(this._onLoad);
			base.ResolveControlIDs(this._onClick);
			base.ResolveControlIDs(this._onMouseOver);
			base.ResolveControlIDs(this._onMouseOut);
			base.ResolveControlIDs(this._onHoverOver);
			base.ResolveControlIDs(this._onHoverOut);
		}

		// Token: 0x0400008E RID: 142
		private Animation _onLoad;

		// Token: 0x0400008F RID: 143
		private Animation _onClick;

		// Token: 0x04000090 RID: 144
		private Animation _onMouseOver;

		// Token: 0x04000091 RID: 145
		private Animation _onMouseOut;

		// Token: 0x04000092 RID: 146
		private Animation _onHoverOver;

		// Token: 0x04000093 RID: 147
		private Animation _onHoverOut;
	}
}
