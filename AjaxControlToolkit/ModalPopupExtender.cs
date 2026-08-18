using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000143 RID: 323
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxBitmap(typeof(Accessor), "ModalPopup.bmp")]
	[TargetControlType(typeof(WebControl))]
	[RequiredScript(typeof(DragPanelExtender))]
	[TargetControlType(typeof(HtmlControl))]
	[RequiredScript(typeof(DropShadowExtender))]
	[Designer(typeof(ModalPopupExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.ModalPopupBehavior", "ModalPopup")]
	[RequiredScript(typeof(AnimationExtender))]
	public class ModalPopupExtender : DynamicPopulateExtenderControlBase
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00016AA7 File Offset: 0x00014CA7
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x00016AB9 File Offset: 0x00014CB9
		[RequiredProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[ClientPropertyName("popupControlID")]
		[ExtenderControlProperty]
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

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00016AC7 File Offset: 0x00014CC7
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x00016AD9 File Offset: 0x00014CD9
		[ExtenderControlProperty]
		[ClientPropertyName("backgroundCssClass")]
		[DefaultValue("")]
		public string BackgroundCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("BackgroundCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("BackgroundCssClass", value);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00016AE7 File Offset: 0x00014CE7
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x00016AF9 File Offset: 0x00014CF9
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[DefaultValue("")]
		[ClientPropertyName("okControlID")]
		public string OkControlID
		{
			get
			{
				return base.GetPropertyValue<string>("OkControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OkControlID", value);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00016B07 File Offset: 0x00014D07
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x00016B19 File Offset: 0x00014D19
		[ExtenderControlProperty]
		[DefaultValue("")]
		[IDReferenceProperty(typeof(WebControl))]
		[ClientPropertyName("cancelControlID")]
		public string CancelControlID
		{
			get
			{
				return base.GetPropertyValue<string>("CancelControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CancelControlID", value);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00016B27 File Offset: 0x00014D27
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x00016B39 File Offset: 0x00014D39
		[DefaultValue("")]
		[ClientPropertyName("onOkScript")]
		[ExtenderControlProperty]
		public string OnOkScript
		{
			get
			{
				return base.GetPropertyValue<string>("OnOkScript", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnOkScript", value);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00016B47 File Offset: 0x00014D47
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x00016B59 File Offset: 0x00014D59
		[DefaultValue("")]
		[ClientPropertyName("onCancelScript")]
		[ExtenderControlProperty]
		public string OnCancelScript
		{
			get
			{
				return base.GetPropertyValue<string>("OnCancelScript", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnCancelScript", value);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00016B67 File Offset: 0x00014D67
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x00016B75 File Offset: 0x00014D75
		[DefaultValue(-1)]
		[ClientPropertyName("x")]
		[ExtenderControlProperty]
		public int X
		{
			get
			{
				return base.GetPropertyValue<int>("X", -1);
			}
			set
			{
				base.SetPropertyValue<int>("X", value);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00016B83 File Offset: 0x00014D83
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x00016B91 File Offset: 0x00014D91
		[ExtenderControlProperty]
		[ClientPropertyName("y")]
		[DefaultValue(-1)]
		public int Y
		{
			get
			{
				return base.GetPropertyValue<int>("Y", -1);
			}
			set
			{
				base.SetPropertyValue<int>("Y", value);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00016B9F File Offset: 0x00014D9F
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x00016BAD File Offset: 0x00014DAD
		[ClientPropertyName("drag")]
		[DefaultValue(false)]
		[Obsolete("The drag feature on modal popup will be automatically turned on if you specify the PopupDragHandleControlID property. Setting the Drag property is a noop")]
		[ExtenderControlProperty]
		public bool Drag
		{
			get
			{
				return base.GetPropertyValue<bool>("stringDrag", false);
			}
			set
			{
				base.SetPropertyValue<bool>("stringDrag", value);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00016BBB File Offset: 0x00014DBB
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x00016BCD File Offset: 0x00014DCD
		[IDReferenceProperty(typeof(WebControl))]
		[ExtenderControlProperty]
		[ClientPropertyName("popupDragHandleControlID")]
		[DefaultValue("")]
		public string PopupDragHandleControlID
		{
			get
			{
				return base.GetPropertyValue<string>("PopupDragHandleControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PopupDragHandleControlID", value);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00016BDB File Offset: 0x00014DDB
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x00016BE9 File Offset: 0x00014DE9
		[ExtenderControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("dropShadow")]
		public bool DropShadow
		{
			get
			{
				return base.GetPropertyValue<bool>("stringDropShadow", false);
			}
			set
			{
				base.SetPropertyValue<bool>("stringDropShadow", value);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00016BF7 File Offset: 0x00014DF7
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00016C05 File Offset: 0x00014E05
		[DefaultValue(ModalPopupRepositionMode.RepositionOnWindowResizeAndScroll)]
		[ClientPropertyName("repositionMode")]
		[ExtenderControlProperty]
		public ModalPopupRepositionMode RepositionMode
		{
			get
			{
				return base.GetPropertyValue<ModalPopupRepositionMode>("RepositionMode", ModalPopupRepositionMode.RepositionOnWindowResizeAndScroll);
			}
			set
			{
				base.SetPropertyValue<ModalPopupRepositionMode>("RepositionMode", value);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00016C13 File Offset: 0x00014E13
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00016C26 File Offset: 0x00014E26
		[Browsable(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("onShown")]
		public Animation OnShown
		{
			get
			{
				return base.GetAnimation(ref this._onShown, "OnShown");
			}
			set
			{
				base.SetAnimation(ref this._onShown, "OnShown", value);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00016C3A File Offset: 0x00014E3A
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00016C4D File Offset: 0x00014E4D
		[ExtenderControlProperty]
		[ClientPropertyName("onHidden")]
		[Browsable(false)]
		public Animation OnHidden
		{
			get
			{
				return base.GetAnimation(ref this._onHidden, "OnHidden");
			}
			set
			{
				base.SetAnimation(ref this._onHidden, "OnHidden", value);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00016C61 File Offset: 0x00014E61
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00016C74 File Offset: 0x00014E74
		[ClientPropertyName("onShowing")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public Animation OnShowing
		{
			get
			{
				return base.GetAnimation(ref this._onShowing, "OnShowing");
			}
			set
			{
				base.SetAnimation(ref this._onShowing, "OnShowing", value);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00016C88 File Offset: 0x00014E88
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x00016C9B File Offset: 0x00014E9B
		[ClientPropertyName("onHiding")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public Animation OnHiding
		{
			get
			{
				return base.GetAnimation(ref this._onHiding, "OnHiding");
			}
			set
			{
				base.SetAnimation(ref this._onHiding, "OnHiding", value);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00016CAF File Offset: 0x00014EAF
		public void Show()
		{
			this._show = new bool?(true);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00016CBD File Offset: 0x00014EBD
		public void Hide()
		{
			this._show = new bool?(false);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00016CCC File Offset: 0x00014ECC
		protected override void OnPreRender(EventArgs e)
		{
			if (this._show != null)
			{
				this.ChangeVisibility(this._show.Value);
			}
			base.ResolveControlIDs(this._onShown);
			base.ResolveControlIDs(this._onHidden);
			base.ResolveControlIDs(this._onShowing);
			base.ResolveControlIDs(this._onHiding);
			base.OnPreRender(e);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00016D30 File Offset: 0x00014F30
		private void ChangeVisibility(bool show)
		{
			if (base.TargetControl == null)
			{
				throw new ArgumentNullException("TargetControl", "TargetControl property cannot be null");
			}
			string text = show ? "show" : "hide";
			if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
			{
				ScriptManager.GetCurrent(this.Page).RegisterDataItem(base.TargetControl, text);
				return;
			}
			string script = string.Format(CultureInfo.InvariantCulture, "(function() {{var fn = function() {{Sys.Extended.UI.ModalPopupBehavior.invokeViaServer('{0}', {1}); Sys.Application.remove_load(fn);}};Sys.Application.add_load(fn);}})();", new object[]
			{
				base.BehaviorID,
				show ? "true" : "false"
			});
			ScriptManager.RegisterStartupScript(this, typeof(ModalPopupExtender), text + base.BehaviorID, script, true);
		}

		// Token: 0x0400035B RID: 859
		private bool? _show;

		// Token: 0x0400035C RID: 860
		private Animation _onHidden;

		// Token: 0x0400035D RID: 861
		private Animation _onShown;

		// Token: 0x0400035E RID: 862
		private Animation _onHiding;

		// Token: 0x0400035F RID: 863
		private Animation _onShowing;
	}
}
