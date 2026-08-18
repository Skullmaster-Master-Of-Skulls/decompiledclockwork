using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor
{
	// Token: 0x020000D6 RID: 214
	[ValidationProperty("Content")]
	[RequiredScript(typeof(HtmlEditor))]
	[RequiredScript(typeof(DesignPanel))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.EditPanel", "HtmlEditor.EditPanel")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(Events))]
	[RequiredScript(typeof(Enums))]
	[RequiredScript(typeof(PreviewPanel))]
	[RequiredScript(typeof(HtmlPanel))]
	public abstract class EditPanel : ScriptControlBase, IPostBackEventHandler
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00010274 File Offset: 0x0000E474
		protected EditPanel() : base(false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600060F RID: 1551 RVA: 0x000102B0 File Offset: 0x0000E4B0
		// (remove) Token: 0x06000610 RID: 1552 RVA: 0x000102C3 File Offset: 0x0000E4C3
		[Category("Behavior")]
		public event ContentChangedEventHandler ContentChanged
		{
			add
			{
				this.Events.AddHandler(EditPanel.EventContentChanged, value);
			}
			remove
			{
				this.Events.RemoveHandler(EditPanel.EventContentChanged, value);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000102D8 File Offset: 0x0000E4D8
		protected virtual void OnRaiseContentChanged(EventArgs e)
		{
			ContentChangedEventHandler contentChangedEventHandler = (ContentChangedEventHandler)this.Events[EditPanel.EventContentChanged];
			if (contentChangedEventHandler != null)
			{
				contentChangedEventHandler(this, e);
				return;
			}
			base.RaiseBubbleEvent(this, new CommandEventArgs("contentchanged", string.Empty));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001031D File Offset: 0x0000E51D
		protected override void RaisePostDataChangedEvent()
		{
			if (this._contentChanged)
			{
				this.OnRaiseContentChanged(EventArgs.Empty);
				this._contentChanged = false;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001033C File Offset: 0x0000E53C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			base.LoadPostData(postDataKey, postCollection);
			bool flag = false;
			string text = postCollection[this.ContentForceId];
			if (!string.IsNullOrEmpty(text))
			{
				flag = true;
			}
			text = postCollection[this.ActiveModeId];
			if (!string.IsNullOrEmpty(text))
			{
				this.ActiveMode = (ActiveModeType)long.Parse(text, CultureInfo.InvariantCulture);
			}
			this._contentChanged = false;
			text = postCollection[this.ContentId];
			if (text != null && flag)
			{
				string text2 = text.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&amp;", "&");
				if (text2 == "<br />")
				{
					text2 = string.Empty;
				}
				this._contentChanged = (this.Content.Replace("\n", string.Empty).Replace("\r", string.Empty) != text2.Replace("\n", string.Empty).Replace("\r", string.Empty));
				this.Content = text2;
			}
			text = postCollection[this.ContentChangedId];
			if (!string.IsNullOrEmpty(text))
			{
				this._contentChanged = true;
			}
			return this._contentChanged;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001047A File Offset: 0x0000E67A
		public void RaisePostBackEvent(string eventArgument)
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001047C File Offset: 0x0000E67C
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			return true;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00010480 File Offset: 0x0000E680
		private bool isDesign
		{
			get
			{
				bool result;
				try
				{
					bool flag = this.Context == null || (base.Site != null && base.Site.DesignMode);
					result = flag;
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000104D0 File Offset: 0x0000E6D0
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x000104F1 File Offset: 0x0000E6F1
		[DefaultValue(false)]
		[ClientPropertyName("suppressTabInDesignMode")]
		[Category("Behavior")]
		[ExtenderControlProperty]
		public bool SuppressTabInDesignMode
		{
			get
			{
				return (bool)(this.ViewState["SuppressTabInDesignMode"] ?? false);
			}
			set
			{
				this.ViewState["SuppressTabInDesignMode"] = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00010509 File Offset: 0x0000E709
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0001052A File Offset: 0x0000E72A
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool IgnoreTab
		{
			get
			{
				return (bool)(this.ViewState["IgnoreTab"] ?? false);
			}
			set
			{
				this.ViewState["IgnoreTab"] = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00010542 File Offset: 0x0000E742
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00010563 File Offset: 0x0000E763
		[ClientPropertyName("noUnicode")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		public bool NoUnicode
		{
			get
			{
				return (bool)(this.ViewState["NoUnicode"] ?? false);
			}
			set
			{
				this.ViewState["NoUnicode"] = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001057B File Offset: 0x0000E77B
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0001059C File Offset: 0x0000E79C
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("noScript")]
		[Category("Behavior")]
		public bool NoScript
		{
			get
			{
				return (bool)(this.ViewState["NoScript"] ?? false);
			}
			set
			{
				this.ViewState["NoScript"] = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000105B4 File Offset: 0x0000E7B4
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x000105D5 File Offset: 0x0000E7D5
		[ClientPropertyName("initialCleanUp")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[Category("Behavior")]
		public bool InitialCleanUp
		{
			get
			{
				return (bool)(this.ViewState["InitialCleanUp"] ?? false);
			}
			set
			{
				this.ViewState["InitialCleanUp"] = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x000105ED File Offset: 0x0000E7ED
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001060D File Offset: 0x0000E80D
		[Category("Appearance")]
		[DefaultValue("ajax__htmleditor_htmlpanel_default")]
		public string HtmlPanelCssClass
		{
			get
			{
				return (string)(this.ViewState["HtmlPanelCssClass"] ?? "ajax__htmleditor_htmlpanel_default");
			}
			set
			{
				this.ViewState["HtmlPanelCssClass"] = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00010620 File Offset: 0x0000E820
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00010640 File Offset: 0x0000E840
		[Category("Appearance")]
		[DefaultValue("")]
		public string DocumentCssPath
		{
			get
			{
				return (string)(this.ViewState["DocumentCssPath"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DocumentCssPath"] = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00010653 File Offset: 0x0000E853
		[ClientPropertyName("documentCssPath")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public string ClientDocumentCssPath
		{
			get
			{
				return this.getClientCSSPath(this.DocumentCssPath, "Document");
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00010666 File Offset: 0x0000E866
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClientDocumentCssPath()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001066E File Offset: 0x0000E86E
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0001068E File Offset: 0x0000E88E
		[Category("Appearance")]
		[DefaultValue("")]
		public string DesignPanelCssPath
		{
			get
			{
				return (string)(this.ViewState["DesignPanelCssPath"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DesignPanelCssPath"] = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000106A1 File Offset: 0x0000E8A1
		[ExtenderControlProperty]
		[ClientPropertyName("designPanelCssPath")]
		[Browsable(false)]
		public string ClientDesignPanelCssPath
		{
			get
			{
				return this.getClientCSSPath(this.DesignPanelCssPath, "DesignPanel");
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000106B4 File Offset: 0x0000E8B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClientDesignPanelCssPath()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000106BC File Offset: 0x0000E8BC
		[ExtenderControlProperty]
		[Browsable(false)]
		[ClientPropertyName("imagePath_1x1")]
		public string ImagePath_1X1
		{
			get
			{
				return ToolkitResourceManager.GetImageHref("HtmlEditor.Ed-1x1.gif", this, true);
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000106CA File Offset: 0x0000E8CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeImagePath_1X1()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000106D2 File Offset: 0x0000E8D2
		[ClientPropertyName("imagePath_flash")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public string ImagePath_Flash
		{
			get
			{
				return ToolkitResourceManager.GetImageHref("HtmlEditor.Ed-Flash.gif", this, true);
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000106E0 File Offset: 0x0000E8E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeImagePath_Flash()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000106E8 File Offset: 0x0000E8E8
		[ClientPropertyName("imagePath_media")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public string ImagePath_Media
		{
			get
			{
				return ToolkitResourceManager.GetImageHref("HtmlEditor.Ed-Media.gif", this, true);
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000106F6 File Offset: 0x0000E8F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeImagePath_Media()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000106FE File Offset: 0x0000E8FE
		[ExtenderControlProperty]
		[Browsable(false)]
		[ClientPropertyName("imagePath_anchor")]
		public string ImagePath_Anchor
		{
			get
			{
				return ToolkitResourceManager.GetImageHref("HtmlEditor.Ed-Anchor.gif", this, true);
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001070C File Offset: 0x0000E90C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeImagePath_Anchor()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00010714 File Offset: 0x0000E914
		[Browsable(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("imagePath_placeHolder")]
		public string ImagePath_Placeholder
		{
			get
			{
				return ToolkitResourceManager.GetImageHref("HtmlEditor.Ed-Placeholder.gif", this, true);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00010722 File Offset: 0x0000E922
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeImagePath_Placeholder()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001072A File Offset: 0x0000E92A
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0001074B File Offset: 0x0000E94B
		[DefaultValue(true)]
		[ExtenderControlProperty]
		[ClientPropertyName("autofocus")]
		[Category("Behavior")]
		public bool AutoFocus
		{
			get
			{
				return (bool)(this.ViewState["AutoFocus"] ?? true);
			}
			set
			{
				this.ViewState["AutoFocus"] = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00010763 File Offset: 0x0000E963
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00010783 File Offset: 0x0000E983
		[Category("Appearance")]
		[DefaultValue("")]
		public string Content
		{
			get
			{
				return (string)(this.ViewState["Content"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Content"] = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00010796 File Offset: 0x0000E996
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x000107B7 File Offset: 0x0000E9B7
		[DefaultValue(ActiveModeType.Design)]
		[Category("Behavior")]
		public ActiveModeType ActiveMode
		{
			get
			{
				return (ActiveModeType)(this.ViewState["ActiveMode"] ?? ActiveModeType.Design);
			}
			set
			{
				this.ViewState["ActiveMode"] = value;
				if (this._designer != null && this.isDesign)
				{
					this.RefreshDesigner();
				}
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x000107E5 File Offset: 0x0000E9E5
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00010805 File Offset: 0x0000EA05
		[Category("Behavior")]
		[ExtenderControlEvent]
		[ClientPropertyName("activeModeChanged")]
		[DefaultValue("")]
		public string OnClientActiveModeChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientActiveModeChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientActiveModeChanged"] = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00010818 File Offset: 0x0000EA18
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00010838 File Offset: 0x0000EA38
		[ClientPropertyName("beforeActiveModeChanged")]
		[Category("Behavior")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public string OnClientBeforeActiveModeChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientBeforeActiveModeChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientBeforeActiveModeChanged"] = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001084B File Offset: 0x0000EA4B
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "100%")]
		public override Unit Height
		{
			get
			{
				return Unit.Percentage(100.0);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001085B File Offset: 0x0000EA5B
		[DefaultValue(typeof(Unit), "100%")]
		[Category("Appearance")]
		public override Unit Width
		{
			get
			{
				return Unit.Percentage(100.0);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001086C File Offset: 0x0000EA6C
		[Browsable(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("modePanelIds")]
		public string ClientModePanelIds
		{
			get
			{
				string text = string.Empty;
				for (int i = 0; i < this.ModePanels.Length; i++)
				{
					if (i > 0)
					{
						text += ";";
					}
					text += this.ModePanels[i].ClientID;
				}
				return text;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000108B7 File Offset: 0x0000EAB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClientModePanelIds()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x000108BF File Offset: 0x0000EABF
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x000108DA File Offset: 0x0000EADA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		internal Collection<Toolbar> Toolbars
		{
			get
			{
				if (this._toolbars == null)
				{
					this._toolbars = new Collection<Toolbar>();
				}
				return this._toolbars;
			}
			set
			{
				this._toolbars = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x000108E4 File Offset: 0x0000EAE4
		[ExtenderControlProperty]
		[ClientPropertyName("toolbarIds")]
		[Browsable(false)]
		public string ToolbarIds
		{
			get
			{
				string text = string.Empty;
				for (int i = 0; i < this.Toolbars.Count; i++)
				{
					if (i > 0)
					{
						text += ";";
					}
					text += this.Toolbars[i].ClientID;
				}
				return text;
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00010936 File Offset: 0x0000EB36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeToolbarIds()
		{
			return base.IsRenderingScript;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001093E File Offset: 0x0000EB3E
		internal new EventHandlerList Events
		{
			get
			{
				return base.Events;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00010946 File Offset: 0x0000EB46
		protected string ContentChangedId
		{
			get
			{
				return "_contentChanged_" + this.ClientID;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00010958 File Offset: 0x0000EB58
		protected string ContentId
		{
			get
			{
				return "_content_" + this.ClientID;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001096A File Offset: 0x0000EB6A
		protected string ContentForceId
		{
			get
			{
				return "_contentForce_" + this.ClientID;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001097C File Offset: 0x0000EB7C
		protected string ActiveModeId
		{
			get
			{
				return "_activeMode_" + this.ClientID;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001098E File Offset: 0x0000EB8E
		protected void RefreshDesigner()
		{
			if (this._designer != null && this.isDesign)
			{
				this._designer.UpdateDesignTimeHtml();
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000109AB File Offset: 0x0000EBAB
		public void SetDesigner(ControlDesigner designer)
		{
			this._designer = designer;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000109B4 File Offset: 0x0000EBB4
		protected string LocalResolveUrl(string path)
		{
			string input = base.ResolveUrl(path);
			Regex regex = new Regex("(\\(S\\([A-Za-z0-9_]+\\)\\)/)", RegexOptions.Compiled);
			return regex.Replace(input, string.Empty);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000109E4 File Offset: 0x0000EBE4
		internal string getClientCSSPath(string pathN, string name)
		{
			string result = string.Empty;
			string path = string.Empty;
			bool flag = false;
			string text = (pathN.Length > 0) ? this.LocalResolveUrl(pathN) : string.Empty;
			if (text.Length > 0)
			{
				try
				{
					path = HttpContext.Current.Server.MapPath(text);
					if (File.Exists(path))
					{
						flag = true;
					}
				}
				catch
				{
				}
			}
			if (flag)
			{
				result = text;
			}
			else
			{
				result = base.ResolveClientUrl(ToolkitResourceManager.GetStyleHref("HtmlEditor." + name, this));
			}
			return result;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00010A70 File Offset: 0x0000EC70
		internal static bool IE(Page page)
		{
			bool result;
			try
			{
				if (page.Request.Browser.Browser.IndexOf("IE", StringComparison.OrdinalIgnoreCase) > -1)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00010ABC File Offset: 0x0000ECBC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptManager.RegisterHiddenField(this, this.ContentChangedId, string.Empty);
			ScriptManager.RegisterHiddenField(this, this.ContentForceId, "1");
			ScriptManager.RegisterHiddenField(this, this.ContentId, this.Content.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;"));
			ScriptManager.RegisterHiddenField(this, this.ActiveModeId, ((int)this.ActiveMode).ToString(CultureInfo.InvariantCulture));
			this.Page.RegisterRequiresPostBack(this);
			for (int i = 0; i < this.Controls.Count; i++)
			{
				if (this.IgnoreTab)
				{
					ModePanel modePanel = this.Controls[i] as ModePanel;
					modePanel.Attributes.Add("tabindex", "-1");
				}
				if (this.Controls[i].GetType() == typeof(HtmlPanel))
				{
					(this.Controls[i] as HtmlPanel).CssClass = this.HtmlPanelCssClass;
				}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddElementProperty("contentChangedElement", this.ContentChangedId);
			descriptor.AddElementProperty("contentForceElement", this.ContentForceId);
			descriptor.AddElementProperty("contentElement", this.ContentId);
			descriptor.AddElementProperty("activeModeElement", this.ActiveModeId);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00010C48 File Offset: 0x0000EE48
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.Style.Add(HtmlTextWriterStyle.Height, Unit.Percentage(100.0).ToString());
			base.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(100.0).ToString());
			if (!this.isDesign)
			{
				for (int i = 0; i < this.ModePanels.Length; i++)
				{
					this.ModePanels[i].setEditPanel(this);
					this.Controls.Add(this.ModePanels[i]);
				}
				return;
			}
			this.Controls.Add(this.ModePanels[0]);
		}

		// Token: 0x040002DD RID: 733
		public static readonly object EventContentChanged = new object();

		// Token: 0x040002DE RID: 734
		private bool _contentChanged;

		// Token: 0x040002DF RID: 735
		private readonly ModePanel[] ModePanels = new ModePanel[]
		{
			new DesignPanel(),
			new HtmlPanel(),
			new PreviewPanel()
		};

		// Token: 0x040002E0 RID: 736
		private Collection<Toolbar> _toolbars;

		// Token: 0x040002E1 RID: 737
		private ControlDesigner _designer;
	}
}
