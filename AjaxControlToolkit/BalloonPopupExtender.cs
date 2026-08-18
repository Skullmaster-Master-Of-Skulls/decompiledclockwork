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
	// Token: 0x02000055 RID: 85
	[ToolboxBitmap(typeof(Accessor), "BalloonPopup.bmp")]
	[RequiredScript(typeof(PopupExtender))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(WebControl))]
	[ClientCssResource("BalloonPopup.Cloud")]
	[ClientScriptResource("Sys.Extended.UI.BalloonPopupControlBehavior", "BalloonPopup")]
	[Designer(typeof(BalloonPopupExtenderDesigner))]
	[ClientCssResource("BalloonPopup.Rectangle")]
	public class BalloonPopupExtender : DynamicPopulateExtenderControlBase
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00009B7F File Offset: 0x00007D7F
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00009B91 File Offset: 0x00007D91
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string ExtenderControlID
		{
			get
			{
				return base.GetPropertyValue<string>("ExtenderControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ExtenderControlID", value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00009B9F File Offset: 0x00007D9F
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00009BB1 File Offset: 0x00007DB1
		[IDReferenceProperty(typeof(WebControl))]
		[RequiredProperty]
		[ClientPropertyName("balloonPopupControlID")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string BalloonPopupControlID
		{
			get
			{
				return base.GetPropertyValue<string>("BalloonPopupControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("BalloonPopupControlID", value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00009BBF File Offset: 0x00007DBF
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00009BC7 File Offset: 0x00007DC7
		[ClientPropertyName("balloonPopupPosition")]
		[ExtenderControlProperty]
		[DefaultValue(BalloonPopupPosition.Auto)]
		public BalloonPopupPosition Position { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00009BD0 File Offset: 0x00007DD0
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00009BD8 File Offset: 0x00007DD8
		[ClientPropertyName("balloonPopupStyle")]
		[DefaultValue(BalloonPopupStyle.Rectangle)]
		[ExtenderControlProperty]
		public BalloonPopupStyle BalloonStyle { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00009BE1 File Offset: 0x00007DE1
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00009BEF File Offset: 0x00007DEF
		[DefaultValue(0)]
		[ClientPropertyName("offsetX")]
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00009BFD File Offset: 0x00007DFD
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00009C0B File Offset: 0x00007E0B
		[DefaultValue(0)]
		[ClientPropertyName("offsetY")]
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00009C19 File Offset: 0x00007E19
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00009C2C File Offset: 0x00007E2C
		[ClientPropertyName("onShow")]
		[ExtenderControlProperty]
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00009C40 File Offset: 0x00007E40
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00009C53 File Offset: 0x00007E53
		[ClientPropertyName("onHide")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		[Browsable(false)]
		[DefaultValue(null)]
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

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00009C67 File Offset: 0x00007E67
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00009C75 File Offset: 0x00007E75
		[ClientPropertyName("displayOnMouseOver")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		public bool DisplayOnMouseOver
		{
			get
			{
				return base.GetPropertyValue<bool>("DisplayOnMouseOver", false);
			}
			set
			{
				base.SetPropertyValue<bool>("DisplayOnMouseOver", value);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00009C83 File Offset: 0x00007E83
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00009C91 File Offset: 0x00007E91
		[ClientPropertyName("displayOnFocus")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool DisplayOnFocus
		{
			get
			{
				return base.GetPropertyValue<bool>("DisplayOnFocus", false);
			}
			set
			{
				base.SetPropertyValue<bool>("DisplayOnFocus", value);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00009C9F File Offset: 0x00007E9F
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00009CAD File Offset: 0x00007EAD
		[DefaultValue(true)]
		[ClientPropertyName("displayOnClick")]
		[ExtenderControlProperty]
		public bool DisplayOnClick
		{
			get
			{
				return base.GetPropertyValue<bool>("DisplayOnClick", true);
			}
			set
			{
				base.SetPropertyValue<bool>("DisplayOnClick", value);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00009CBB File Offset: 0x00007EBB
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00009CC9 File Offset: 0x00007EC9
		[ExtenderControlProperty]
		[ClientPropertyName("balloonSize")]
		[DefaultValue(BalloonPopupSize.Small)]
		public BalloonPopupSize BalloonSize
		{
			get
			{
				return base.GetPropertyValue<BalloonPopupSize>("BalloonSize", BalloonPopupSize.Small);
			}
			set
			{
				base.SetPropertyValue<BalloonPopupSize>("BalloonSize", value);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00009CD7 File Offset: 0x00007ED7
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00009CE5 File Offset: 0x00007EE5
		[ClientPropertyName("useShadow")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		public bool UseShadow
		{
			get
			{
				return base.GetPropertyValue<bool>("UseShadow", true);
			}
			set
			{
				base.SetPropertyValue<bool>("UseShadow", value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00009CF3 File Offset: 0x00007EF3
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00009CFB File Offset: 0x00007EFB
		[DefaultValue("")]
		public string CustomCssUrl { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00009D04 File Offset: 0x00007F04
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00009D12 File Offset: 0x00007F12
		[ExtenderControlProperty]
		[Description("Scroll bars behavior when content is overflow")]
		[DefaultValue(ScrollBars.Auto)]
		[Category("Behavior")]
		[ClientPropertyName("scrollBars")]
		public ScrollBars ScrollBars
		{
			get
			{
				return base.GetPropertyValue<ScrollBars>("ScrollBars", ScrollBars.Auto);
			}
			set
			{
				base.SetPropertyValue<ScrollBars>("ScrollBars", value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00009D20 File Offset: 0x00007F20
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00009D32 File Offset: 0x00007F32
		[ClientPropertyName("customClassName")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string CustomClassName
		{
			get
			{
				return base.GetPropertyValue<string>("CustomClassName", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CustomClassName", value);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009D40 File Offset: 0x00007F40
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.BalloonStyle == BalloonPopupStyle.Custom)
			{
				if (this.CustomCssUrl == string.Empty)
				{
					throw new ArgumentException("Must pass CustomCssUrl value.");
				}
				if (this.CustomClassName == string.Empty)
				{
					throw new ArgumentException("Must pass CustomClassName value.");
				}
				bool flag = false;
				foreach (object obj in this.Page.Header.Controls)
				{
					Control control = (Control)obj;
					if (control.ID == "customCssUrl")
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					HtmlLink htmlLink = new HtmlLink();
					htmlLink.Href = base.ResolveUrl(this.CustomCssUrl);
					htmlLink.Attributes["id"] = "customCssUrl";
					htmlLink.Attributes["rel"] = "stylesheet";
					htmlLink.Attributes["type"] = "text/css";
					htmlLink.Attributes["media"] = "all";
					this.Page.Header.Controls.Add(htmlLink);
				}
			}
		}

		// Token: 0x040000F3 RID: 243
		private Animation _onHide;

		// Token: 0x040000F4 RID: 244
		private Animation _onShow;
	}
}
