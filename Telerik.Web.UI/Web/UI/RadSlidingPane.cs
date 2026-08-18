using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FBF RID: 4031
	[Designer("Telerik.Web.Design.RadSlidingPaneDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredScript(typeof(ResizeExtender))]
	[ToolboxBitmap(typeof(RadSlidingPane), "Telerik.Web.UI.Splitter.png")]
	[ParseChildren(false)]
	[ClientScriptResource("Telerik.Web.UI.RadSlidingPane", "Telerik.Web.UI.Splitter.RadSlidingZoneScripts.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(jSlide))]
	[RequiredScript(typeof(PopupBehavior))]
	[PersistChildren(true)]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxData("<{0}:RadSlidingPane Runat=server></{0}:RadSlidingPane>")]
	[TelerikToolboxCategory("Container")]
	public class RadSlidingPane : SplitterPaneBase, IPostBackDataHandler
	{
		// Token: 0x1700317D RID: 12669
		// (get) Token: 0x06009C41 RID: 40001 RVA: 0x0022C9BD File Offset: 0x0022ABBD
		// (set) Token: 0x06009C42 RID: 40002 RVA: 0x0022C9DF File Offset: 0x0022ABDF
		[Category("Appearance")]
		[Description("Sets/gets the min height to which the pane can be resized")]
		[ClientControlProperty]
		[DefaultValue(60)]
		public override int MinHeight
		{
			get
			{
				return (int)(this.ViewState["MinHeight"] ?? 60);
			}
			set
			{
				this.ViewState["MinHeight"] = value;
			}
		}

		// Token: 0x1700317E RID: 12670
		// (get) Token: 0x06009C43 RID: 40003 RVA: 0x0022C9F8 File Offset: 0x0022ABF8
		// (set) Token: 0x06009C44 RID: 40004 RVA: 0x0022CA4A File Offset: 0x0022AC4A
		[Description("Sets/gets the height of the sliding pane")]
		[DefaultValue(typeof(Unit), "150px")]
		[ClientControlProperty]
		[SimplePersistenceSetting]
		[Category("Behavior")]
		public override Unit Height
		{
			get
			{
				object obj = this.ViewState["Height"];
				if (obj == null || string.IsNullOrEmpty(obj.ToString()) || obj.ToString().IndexOf("%") > -1)
				{
					return Unit.Pixel(150);
				}
				return (Unit)obj;
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700317F RID: 12671
		// (get) Token: 0x06009C45 RID: 40005 RVA: 0x0022CA62 File Offset: 0x0022AC62
		// (set) Token: 0x06009C46 RID: 40006 RVA: 0x0022CA84 File Offset: 0x0022AC84
		[Description("Sets/gets the min width to which the pane can be resized")]
		[Category("Appearance")]
		[ClientControlProperty]
		[DefaultValue(60)]
		public override int MinWidth
		{
			get
			{
				return (int)(this.ViewState["MinWidth"] ?? 60);
			}
			set
			{
				this.ViewState["MinWidth"] = value;
			}
		}

		// Token: 0x17003180 RID: 12672
		// (get) Token: 0x06009C47 RID: 40007 RVA: 0x0022CA9C File Offset: 0x0022AC9C
		// (set) Token: 0x06009C48 RID: 40008 RVA: 0x0022CAEE File Offset: 0x0022ACEE
		[Description("Sets/gets the width of the sliding pane")]
		[DefaultValue(typeof(Unit), "150px")]
		[ClientControlProperty]
		[SimplePersistenceSetting]
		[Category("Behavior")]
		public override Unit Width
		{
			get
			{
				object obj = this.ViewState["Width"];
				if (obj == null || string.IsNullOrEmpty(obj.ToString()) || obj.ToString().IndexOf("%") > -1)
				{
					return Unit.Pixel(150);
				}
				return (Unit)obj;
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17003181 RID: 12673
		// (get) Token: 0x06009C49 RID: 40009 RVA: 0x0022CB06 File Offset: 0x0022AD06
		// (set) Token: 0x06009C4A RID: 40010 RVA: 0x0022CB27 File Offset: 0x0022AD27
		[Category("Behavior")]
		[Description("Sets/gets whether the resize bar will be active")]
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool EnableResize
		{
			get
			{
				return (bool)(this.ViewState["EnableResize"] ?? true);
			}
			set
			{
				this.ViewState["EnableResize"] = value;
			}
		}

		// Token: 0x17003182 RID: 12674
		// (get) Token: 0x06009C4B RID: 40011 RVA: 0x0022CB3F File Offset: 0x0022AD3F
		// (set) Token: 0x06009C4C RID: 40012 RVA: 0x0022CB60 File Offset: 0x0022AD60
		[Description("Sets/gets whether the sliding pane will automatically dock on open")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool DockOnOpen
		{
			get
			{
				return (bool)(this.ViewState["DockOnOpen"] ?? false);
			}
			set
			{
				this.ViewState["DockOnOpen"] = value;
			}
		}

		// Token: 0x17003183 RID: 12675
		// (get) Token: 0x06009C4D RID: 40013 RVA: 0x0022CB78 File Offset: 0x0022AD78
		// (set) Token: 0x06009C4E RID: 40014 RVA: 0x0022CB98 File Offset: 0x0022AD98
		[UrlProperty]
		[Description("The URL for the image of the Pane.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string IconUrl
		{
			get
			{
				return (string)(this.ViewState["IconUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["IconUrl"] = value;
			}
		}

		// Token: 0x17003184 RID: 12676
		// (get) Token: 0x06009C4F RID: 40015 RVA: 0x0022CBAB File Offset: 0x0022ADAB
		// (set) Token: 0x06009C50 RID: 40016 RVA: 0x0022CBCC File Offset: 0x0022ADCC
		[DefaultValue(SplitterSlidePaneTabView.TextAndImage)]
		[Description("Sets/gets way the tab of the pane is rendered")]
		[Category("Behavior")]
		public SplitterSlidePaneTabView TabView
		{
			get
			{
				return (SplitterSlidePaneTabView)(this.ViewState["TabView"] ?? SplitterSlidePaneTabView.TextAndImage);
			}
			set
			{
				this.ViewState["TabView"] = value;
			}
		}

		// Token: 0x17003185 RID: 12677
		// (get) Token: 0x06009C51 RID: 40017 RVA: 0x0022CBE4 File Offset: 0x0022ADE4
		// (set) Token: 0x06009C52 RID: 40018 RVA: 0x0022CC05 File Offset: 0x0022AE05
		[Description("Sets/gets whether the pane can be docked")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool EnableDock
		{
			get
			{
				return (bool)(this.ViewState["EnableDock"] ?? true);
			}
			set
			{
				this.ViewState["EnableDock"] = value;
			}
		}

		// Token: 0x17003186 RID: 12678
		// (get) Token: 0x06009C53 RID: 40019 RVA: 0x0022CC1D File Offset: 0x0022AE1D
		// (set) Token: 0x06009C54 RID: 40020 RVA: 0x0022CC3D File Offset: 0x0022AE3D
		[DefaultValue("")]
		[Localizable(true)]
		[Description("The title that will be displayed when the pane is docked/docked.")]
		[ClientControlProperty]
		[Category("Appearance")]
		public string Title
		{
			get
			{
				return (string)(this.ViewState["Title"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x17003187 RID: 12679
		// (get) Token: 0x06009C55 RID: 40021 RVA: 0x0022CC50 File Offset: 0x0022AE50
		// (set) Token: 0x06009C56 RID: 40022 RVA: 0x0022CC70 File Offset: 0x0022AE70
		[Localizable(true)]
		[DefaultValue("Resize")]
		public string ResizeText
		{
			get
			{
				return (string)(this.ViewState["ResizeText"] ?? "Resize");
			}
			set
			{
				this.ViewState["ResizeText"] = value;
			}
		}

		// Token: 0x17003188 RID: 12680
		// (get) Token: 0x06009C57 RID: 40023 RVA: 0x0022CC83 File Offset: 0x0022AE83
		// (set) Token: 0x06009C58 RID: 40024 RVA: 0x0022CCA3 File Offset: 0x0022AEA3
		[Localizable(true)]
		[DefaultValue("Undock")]
		public string UndockText
		{
			get
			{
				return (string)(this.ViewState["UndockText"] ?? "Undock");
			}
			set
			{
				this.ViewState["UndockText"] = value;
			}
		}

		// Token: 0x17003189 RID: 12681
		// (get) Token: 0x06009C59 RID: 40025 RVA: 0x0022CCB6 File Offset: 0x0022AEB6
		// (set) Token: 0x06009C5A RID: 40026 RVA: 0x0022CCD6 File Offset: 0x0022AED6
		[DefaultValue("Dock")]
		[Localizable(true)]
		public string DockText
		{
			get
			{
				return (string)(this.ViewState["DockText"] ?? "Dock");
			}
			set
			{
				this.ViewState["DockText"] = value;
			}
		}

		// Token: 0x1700318A RID: 12682
		// (get) Token: 0x06009C5B RID: 40027 RVA: 0x0022CCE9 File Offset: 0x0022AEE9
		// (set) Token: 0x06009C5C RID: 40028 RVA: 0x0022CD09 File Offset: 0x0022AF09
		[Localizable(true)]
		[DefaultValue("Collapse")]
		public string CollapseText
		{
			get
			{
				return (string)(this.ViewState["CollapseText"] ?? "Collapse");
			}
			set
			{
				this.ViewState["CollapseText"] = value;
			}
		}

		// Token: 0x1700318B RID: 12683
		// (get) Token: 0x06009C5D RID: 40029 RVA: 0x0022CD1C File Offset: 0x0022AF1C
		// (set) Token: 0x06009C5E RID: 40030 RVA: 0x0022CD3D File Offset: 0x0022AF3D
		[Category("Behavior")]
		[Bindable(true)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Description("Specifies whether the sliding pane will create an overlay element to ensure it will be displayed over a flash element.")]
		[ClientControlProperty]
		public bool Overlay
		{
			get
			{
				return (bool)(this.ViewState["Overlay"] ?? false);
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x1700318C RID: 12684
		// (get) Token: 0x06009C5F RID: 40031 RVA: 0x0022CD55 File Offset: 0x0022AF55
		// (set) Token: 0x06009C60 RID: 40032 RVA: 0x0022CD75 File Offset: 0x0022AF75
		[DefaultValue("")]
		[Description("The name of the javascript function called when the pane is docked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("docked")]
		[Category("Client-side events")]
		public string OnClientDocked
		{
			get
			{
				return ((string)this.ViewState["OnClientDocked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientDocked"] = value;
			}
		}

		// Token: 0x1700318D RID: 12685
		// (get) Token: 0x06009C61 RID: 40033 RVA: 0x0022CD88 File Offset: 0x0022AF88
		// (set) Token: 0x06009C62 RID: 40034 RVA: 0x0022CDA8 File Offset: 0x0022AFA8
		[ClientPropertyName("undocked")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when the pane is undocked.")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientUndocked
		{
			get
			{
				return ((string)this.ViewState["OnClientUndocked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientUndocked"] = value;
			}
		}

		// Token: 0x1700318E RID: 12686
		// (get) Token: 0x06009C63 RID: 40035 RVA: 0x0022CDBB File Offset: 0x0022AFBB
		// (set) Token: 0x06009C64 RID: 40036 RVA: 0x0022CDC3 File Offset: 0x0022AFC3
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientDocking property instead.", false)]
		public string OnClientBeforeDock
		{
			get
			{
				return this.OnClientDocking;
			}
			set
			{
				this.OnClientDocking = value;
			}
		}

		// Token: 0x1700318F RID: 12687
		// (get) Token: 0x06009C65 RID: 40037 RVA: 0x0022CDCC File Offset: 0x0022AFCC
		// (set) Token: 0x06009C66 RID: 40038 RVA: 0x0022CDEC File Offset: 0x0022AFEC
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called before the pane is docked.")]
		[ClientPropertyName("docking")]
		[DefaultValue("")]
		public string OnClientDocking
		{
			get
			{
				return ((string)this.ViewState["OnClientDocking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientDocking"] = value;
			}
		}

		// Token: 0x17003190 RID: 12688
		// (get) Token: 0x06009C67 RID: 40039 RVA: 0x0022CDFF File Offset: 0x0022AFFF
		// (set) Token: 0x06009C68 RID: 40040 RVA: 0x0022CE07 File Offset: 0x0022B007
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientUndocking property instead.", false)]
		public string OnClientBeforeUndock
		{
			get
			{
				return this.OnClientUndocking;
			}
			set
			{
				this.OnClientUndocking = value;
			}
		}

		// Token: 0x17003191 RID: 12689
		// (get) Token: 0x06009C69 RID: 40041 RVA: 0x0022CE10 File Offset: 0x0022B010
		// (set) Token: 0x06009C6A RID: 40042 RVA: 0x0022CE30 File Offset: 0x0022B030
		[DefaultValue("")]
		[Description("The name of the javascript function called before the pane is undocked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[ClientPropertyName("undocking")]
		public string OnClientUndocking
		{
			get
			{
				return ((string)this.ViewState["OnClientUndocking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientUndocking"] = value;
			}
		}

		// Token: 0x17003192 RID: 12690
		// (get) Token: 0x06009C6B RID: 40043 RVA: 0x0022CE44 File Offset: 0x0022B044
		[Browsable(false)]
		public RadSlidingZone SlidingZone
		{
			get
			{
				RadSlidingZone radSlidingZone = this.Parent as RadSlidingZone;
				if (radSlidingZone == null)
				{
					throw new NotSupportedException(string.Format("{0} must be placed inside a RadSlidingZone control.", base.GetType().Name));
				}
				return radSlidingZone;
			}
		}

		// Token: 0x06009C6C RID: 40044 RVA: 0x0022CE7C File Offset: 0x0022B07C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x06009C6D RID: 40045 RVA: 0x0022CE7E File Offset: 0x0022B07E
		protected override void ControlPreRender()
		{
			if (this.Page != null && this.Page.Form != null && this.RegisterWithScriptManager && base.ScriptManager != null && base.ScriptManager.LoadScriptsBeforeUI)
			{
				this.RegisterInitializeScriptWithScriptManager();
			}
			base.ControlPreRender();
		}

		// Token: 0x06009C6E RID: 40046 RVA: 0x0022CEC0 File Offset: 0x0022B0C0
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			if ((this.Page != null && this.Page.Form == null) || !this.RegisterWithScriptManager)
			{
				string text = string.Format("<script type=\"text/javascript\">{0}</script>", this.GetInitializeScript());
				LiteralControl literalControl = new LiteralControl(text);
				literalControl.RenderControl(writer);
			}
		}

		// Token: 0x06009C6F RID: 40047 RVA: 0x0022CF10 File Offset: 0x0022B110
		protected override void RenderContents(HtmlTextWriter writer)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			if (this.SlidingZone.SlideDirection == SplitterSlideDirection.Left || this.SlidingZone.SlideDirection == SplitterSlideDirection.Right)
			{
				text = "width:4px;";
				text3 = "rspSlideContainerResize";
			}
			if (this.SlidingZone.SlideDirection == SplitterSlideDirection.Top || this.SlidingZone.SlideDirection == SplitterSlideDirection.Bottom)
			{
				text2 = "height:3px;";
				text3 = "rspSlideContainerResizeHorizontal";
			}
			string text4 = string.Format("<td id=\"RAD_SPLITTER_SLIDING_ZONE_RESIZE_{0}\" style=\"font-size:1px;line-height:1px;\" class=\"{3}\" title=\"{4}\"><div style=\"{1}{2}\"></div></td>", new object[]
			{
				this.ClientID,
				text,
				text2,
				text3,
				this.ResizeText
			});
			string text5 = (this.SlidingZone.SlideDirection == SplitterSlideDirection.Left) ? text4 : "";
			string text6 = (this.SlidingZone.SlideDirection == SplitterSlideDirection.Right) ? text4 : "";
			string text7 = (this.SlidingZone.SlideDirection == SplitterSlideDirection.Top) ? ("<tr>" + text4 + "</tr>") : "";
			string arg = (this.SlidingZone.SlideDirection == SplitterSlideDirection.Bottom) ? ("<tr>" + text4 + "</tr>") : "";
			string text8 = "z-index:";
			text8 += ((base.Style["z-index"] != null) ? base.Style["z-index"] : "2000");
			string value = string.Format("<div id=\"{0}\" style=\"{8};\" class=\"rspSlidePane\">\r\n\t\t\t\t\t<table class=\"rspSlideContainer\" style=\"width:1px;height:1px\">\r\n\t\t\t\t\t\t{4}\r\n\t\t\t\t\t\t<tr class=\"rspSlideHeader\">\r\n\t\t\t\t\t\t\t{1}\r\n\t\t\t\t\t\t\t<td><table style=\"width:100%;\">\r\n\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t\t<td class=\"rspSlideTitleContainer\">\r\n\t\t\t\t\t\t\t\t\t\t<div class=\"rspSlideTitle\" id=\"RAD_SPLITTER_SLIDING_TITLE_{0}\">{3}</div></td>\r\n\t\t\t\t\t\t\t\t\t<td class=\"rspSlideHeaderIconWrapper\">\r\n\t\t\t\t\t\t\t\t\t\t<div id=\"RAD_SPLITTER_SLIDING_PANE_UNDOCK_{0}\" class=\"rspSlideHeaderUndockIcon\" title=\"{5}\">&nbsp;</div>\r\n\t\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t\t<td class=\"rspSlideHeaderIconWrapper\">\r\n\t\t\t\t\t\t\t\t\t\t<div id=\"RAD_SPLITTER_SLIDING_PANE_DOCK_{0}\" title=\"{6}\" class=\"rspSlideHeaderDockIcon\">&nbsp;</div>\r\n\t\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t\t<td class=\"rspSlideHeaderIconWrapper\">\r\n\t\t\t\t\t\t\t\t\t\t<div title=\"{7}\" id=\"RAD_SPLITTER_SLIDING_PANE_COLLAPSE_{0}\" class=\"rspSlideHeaderCollapseIcon\">&nbsp;</div>\r\n\t\t\t\t\t\t\t\t\t</td>\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t</table></td>\r\n\t\t\t\t\t\t\t{2}\r\n\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t<td style=\"text-align:left;\">", new object[]
			{
				this.ClientID,
				text5,
				text6,
				this.Title,
				text7,
				this.UndockText,
				this.DockText,
				this.CollapseText,
				text8
			});
			string value2 = string.Format("</td>\r\n\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t{0}\r\n\t\t\t\t\t</table>\r\n\t\t\t\t</div>", arg);
			writer.Write(value);
			string text9 = "rspSlideContent" + (string.IsNullOrEmpty(this.CssClass) ? "" : string.Format(" {0}", this.CssClass));
			string value3 = string.Format("<div class=\"{0}\" id=\"RAD_SLIDING_PANE_CONTENT_{1}\" style=\"{2}{3}{4}width:1px;height:1px\">", new object[]
			{
				text9,
				this.ClientID,
				base.GetScrollOverflowStyle(),
				base.GetBackColorStyle(),
				base.GetForeColorStyle()
			});
			writer.Write(value3);
			base.RenderContents(writer);
			writer.Write("</div>");
			writer.Write(value2);
		}

		// Token: 0x06009C70 RID: 40048 RVA: 0x0022D17B File Offset: 0x0022B37B
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			this.RenderClientStateField(writer);
			BaseClass.RenderAjaxCssReferences(this, writer);
		}

		// Token: 0x06009C71 RID: 40049 RVA: 0x0022D18C File Offset: 0x0022B38C
		protected internal virtual string GetTabHtml()
		{
			string iconUrl = this.getIconUrl();
			string text = string.Format("<img src=\"{1}\" alt=\"{2}\" id=\"RAD_SLIDING_PANE_ICON_{0}\" class=\"rspPaneTabIcon\"/>", this.ClientID, iconUrl, this.Title);
			bool flag = this.SlidingZone.SlideDirection == SplitterSlideDirection.Left || this.SlidingZone.SlideDirection == SplitterSlideDirection.Right;
			string text2 = string.Empty;
			if (!base.DesignMode)
			{
				text2 = ((!flag) ? string.Format("style=\"line-height:{0};\"", this.SlidingZone.Height.ToString()) : string.Format("style=\"line-height:{0};\"", this.SlidingZone.Width.ToString()));
			}
			string text3 = string.Format("<span class=\"rspPaneTabText{3}\" id=\"RAD_SLIDING_PANE_TEXT_{0}\" {2}>{1}</span>", new object[]
			{
				this.ClientID,
				this.Title,
				text2,
				flag ? " rspRotatedTabText" : ""
			});
			string text4 = "";
			switch (this.TabView)
			{
			case SplitterSlidePaneTabView.TextAndImage:
			{
				string arg = "";
				string arg2 = (!string.IsNullOrEmpty(iconUrl)) ? text : "";
				text4 = string.Format("{0}{1}{2}", arg2, arg, text3);
				break;
			}
			case SplitterSlidePaneTabView.TextOnly:
				text4 = text3;
				break;
			case SplitterSlidePaneTabView.ImageOnly:
				text4 = text;
				break;
			}
			string text5 = flag ? ("width:" + (this.SlidingZone.Width.Value - 1.0) + "px;") : "";
			string text6 = (!flag) ? ("height:" + (this.SlidingZone.Height.Value - 1.0) + "px;") : "";
			string text7 = (flag && !this.SlidingZone.Splitter.Browser.IsBrowser("IE")) ? "text-align: left;" : "";
			return string.Format("<div class=\"rspPaneTabContainer\" id=\"RAD_SLIDING_PANE_TAB_{0}\" style=\"{2}{3}{4}\" title=\"{5}\">{1}</div>", new object[]
			{
				this.ClientID,
				text4,
				text5,
				text6,
				text7,
				this.Title
			});
		}

		// Token: 0x06009C72 RID: 40050 RVA: 0x0022D3C0 File Offset: 0x0022B5C0
		private string getIconUrl()
		{
			string text = this.IconUrl;
			if (this.Page != null)
			{
				text = base.ResolveClientUrl(text);
			}
			return text;
		}

		// Token: 0x06009C73 RID: 40051 RVA: 0x0022D3E8 File Offset: 0x0022B5E8
		private string GetInitializeScript()
		{
			RadPane parentPane = this.SlidingZone.GetParentPane();
			if (parentPane == null)
			{
				throw new NotSupportedException(string.Format("{0} must be placed inside a RadPane control.", base.GetType().Name));
			}
			RadSlidingZone slidingZone = this.SlidingZone;
			return string.Format("Telerik.Web.UI.RadSlidingPane._preInitialize(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");", new object[]
			{
				this.ClientID,
				slidingZone.Splitter.ClientID,
				slidingZone.ClientID,
				parentPane.ClientID,
				base.Index
			});
		}

		// Token: 0x06009C74 RID: 40052 RVA: 0x0022D470 File Offset: 0x0022B670
		internal override void RegisterInitializeScriptWithScriptManager()
		{
			string initializeScript = this.GetInitializeScript();
			ScriptManager.RegisterStartupScript(this.Page, typeof(RadSlidingPane), this.ClientID + initializeScript, initializeScript, true);
		}

		// Token: 0x06009C75 RID: 40053 RVA: 0x0022D4A8 File Offset: 0x0022B6A8
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			try
			{
				base.LoadClientState(clientState);
				this.Width = Unit.Parse(clientState["width"].ToString());
				this.Height = Unit.Parse(clientState["height"].ToString());
				this.Title = clientState["title"].ToString();
				this.EnableResize = (bool)clientState["enableResize"];
				this.EnableDock = (bool)clientState["enableDock"];
			}
			catch
			{
			}
		}

		// Token: 0x06009C76 RID: 40054 RVA: 0x0022D54C File Offset: 0x0022B74C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			RadSlidingZone slidingZone = this.SlidingZone;
			descriptor.AddProperty("_clickToOpen", slidingZone.ClickToOpen);
			descriptor.AddProperty("_slideDirection", slidingZone.SlideDirection);
			descriptor.AddProperty("_slideDuration", slidingZone.SlideDuration);
		}

		// Token: 0x06009C77 RID: 40055 RVA: 0x0022D5AC File Offset: 0x0022B7AC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "dockOnOpen", this.DockOnOpen, false);
			base.DescribeProperty<bool>(descriptor, "enableDock", this.EnableDock, true);
			base.DescribeProperty<bool>(descriptor, "enableResize", this.EnableResize, true);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "150px");
			base.DescribeProperty<int>(descriptor, "minHeight", this.MinHeight, 60);
			base.DescribeProperty<int>(descriptor, "minWidth", this.MinWidth, 60);
			base.DescribeProperty<bool>(descriptor, "overlay", this.Overlay, false);
			base.DescribeProperty<string>(descriptor, "title", this.Title, "");
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "150px");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009C78 RID: 40056 RVA: 0x0022D694 File Offset: 0x0022B894
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "docked", this.OnClientDocked);
			RadWebControl.DescribeEvent(descriptor, "docking", this.OnClientDocking);
			RadWebControl.DescribeEvent(descriptor, "undocked", this.OnClientUndocked);
			RadWebControl.DescribeEvent(descriptor, "undocking", this.OnClientUndocking);
			base.DescribeClientEvents(descriptor);
		}
	}
}
