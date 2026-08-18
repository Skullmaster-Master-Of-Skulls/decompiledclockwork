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
	// Token: 0x0200015E RID: 350
	[ClientScriptResource("Sys.Extended.UI.PopupControlBehavior", "PopupControl")]
	[TargetControlType(typeof(HtmlControl))]
	[RequiredScript(typeof(PopupExtender))]
	[Designer(typeof(PopupControlExtenderDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(WebControl))]
	[ToolboxBitmap(typeof(Accessor), "PopupControl.bmp")]
	public class PopupControlExtender : DynamicPopulateExtenderControlBase
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x00018028 File Offset: 0x00016228
		public PopupControlExtender()
		{
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00018030 File Offset: 0x00016230
		private PopupControlExtender(Page page)
		{
			this._proxyForCurrentPopup = page;
			this._pagePreRenderHandler = new EventHandler(this.Page_PreRender);
			this._proxyForCurrentPopup.PreRender += this._pagePreRenderHandler;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00018064 File Offset: 0x00016264
		public static PopupControlExtender GetProxyForCurrentPopup(Page page)
		{
			return new PopupControlExtender(page);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00018079 File Offset: 0x00016279
		public void Cancel()
		{
			this._closeString = "$$CANCEL$$";
			this._shouldClose = true;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001808D File Offset: 0x0001628D
		public void Commit(string result)
		{
			this._closeString = result;
			this._shouldClose = true;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001809D File Offset: 0x0001629D
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this._pagePreRenderHandler == null)
			{
				this._pagePreRenderHandler = new EventHandler(this.Page_PreRender);
				this.Page.PreRender += this._pagePreRenderHandler;
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000180D1 File Offset: 0x000162D1
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (this._shouldClose)
			{
				this.Close(this._closeString);
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000180E8 File Offset: 0x000162E8
		private void Close(string result)
		{
			if (this._proxyForCurrentPopup == null)
			{
				ScriptManager.GetCurrent(this.Page).RegisterDataItem(base.TargetControl, result);
				return;
			}
			LiteralControl literalControl = new LiteralControl();
			literalControl.ID = "_PopupControl_Proxy_ID_";
			this._proxyForCurrentPopup.Controls.Add(literalControl);
			ScriptManager.GetCurrent(this._proxyForCurrentPopup).RegisterDataItem(literalControl, result);
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00018149 File Offset: 0x00016349
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0001815B File Offset: 0x0001635B
		[Browsable(false)]
		[ClientPropertyName("extenderControlID")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00018169 File Offset: 0x00016369
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0001817B File Offset: 0x0001637B
		[ExtenderControlProperty]
		[ClientPropertyName("popupControlID")]
		[IDReferenceProperty(typeof(WebControl))]
		[RequiredProperty]
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

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00018189 File Offset: 0x00016389
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0001819B File Offset: 0x0001639B
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("commitProperty")]
		public string CommitProperty
		{
			get
			{
				return base.GetPropertyValue<string>("CommitProperty", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CommitProperty", value);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000181A9 File Offset: 0x000163A9
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x000181BB File Offset: 0x000163BB
		[ExtenderControlProperty]
		[ClientPropertyName("commitScript")]
		[DefaultValue("")]
		public string CommitScript
		{
			get
			{
				return base.GetPropertyValue<string>("CommitScript", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CommitScript", value);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x000181C9 File Offset: 0x000163C9
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x000181D7 File Offset: 0x000163D7
		[ClientPropertyName("position")]
		[ExtenderControlProperty]
		[DefaultValue(PopupControlPopupPosition.Center)]
		public PopupControlPopupPosition Position
		{
			get
			{
				return base.GetPropertyValue<PopupControlPopupPosition>("Position", PopupControlPopupPosition.Center);
			}
			set
			{
				base.SetPropertyValue<PopupControlPopupPosition>("Position", value);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000181E5 File Offset: 0x000163E5
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x000181F3 File Offset: 0x000163F3
		[ExtenderControlProperty]
		[ClientPropertyName("offsetX")]
		[DefaultValue(0)]
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

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00018201 File Offset: 0x00016401
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0001820F File Offset: 0x0001640F
		[ClientPropertyName("offsetY")]
		[ExtenderControlProperty]
		[DefaultValue(0)]
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001821D File Offset: 0x0001641D
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x00018230 File Offset: 0x00016430
		[ClientPropertyName("onShow")]
		[ExtenderControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(null)]
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

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00018244 File Offset: 0x00016444
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00018257 File Offset: 0x00016457
		[ExtenderControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onHide")]
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

		// Token: 0x06000950 RID: 2384 RVA: 0x0001826B File Offset: 0x0001646B
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.ResolveControlIDs(this._onShow);
			base.ResolveControlIDs(this._onHide);
		}

		// Token: 0x040003A1 RID: 929
		private bool _shouldClose;

		// Token: 0x040003A2 RID: 930
		private string _closeString;

		// Token: 0x040003A3 RID: 931
		private Page _proxyForCurrentPopup;

		// Token: 0x040003A4 RID: 932
		private EventHandler _pagePreRenderHandler;

		// Token: 0x040003A5 RID: 933
		private Animation _onShow;

		// Token: 0x040003A6 RID: 934
		private Animation _onHide;
	}
}
