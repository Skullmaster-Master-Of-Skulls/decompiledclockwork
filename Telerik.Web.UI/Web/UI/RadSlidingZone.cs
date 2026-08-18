using System;
using System.Collections;
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
	// Token: 0x02000FC0 RID: 4032
	[ToolboxBitmap(typeof(RadSlidingZone), "Telerik.Web.UI.Splitter.png")]
	[ClientScriptResource("Telerik.Web.UI.RadSlidingZone", "Telerik.Web.UI.Splitter.RadSlidingZoneScripts.js")]
	[Designer("Telerik.Web.Design.RadSlidingZoneDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Container")]
	[ToolboxData("<{0}:RadSlidingZone Runat=server></{0}:RadSlidingZone>")]
	[PersistChildren(true)]
	[DefaultProperty("Items")]
	[ParseChildren(typeof(SplitterItem))]
	public class RadSlidingZone : SplitterItemsContainer, IPostBackDataHandler
	{
		// Token: 0x17003193 RID: 12691
		// (get) Token: 0x06009C7A RID: 40058 RVA: 0x0022D6F4 File Offset: 0x0022B8F4
		// (set) Token: 0x06009C7B RID: 40059 RVA: 0x0022D767 File Offset: 0x0022B967
		[ClientPropertyName("_height")]
		[Description("Sets/gets the height of the sliding zone")]
		[Category("Behavior")]
		[ClientControlProperty]
		public override Unit Height
		{
			get
			{
				if (this.ViewState["Height"] != null && !string.IsNullOrEmpty(this.ViewState["Height"].ToString()))
				{
					return (Unit)this.ViewState["Height"];
				}
				if (!this.IsHorizontalSlide())
				{
					return Unit.Pixel(this.GetDefaultSize());
				}
				return Unit.Percentage(100.0);
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17003194 RID: 12692
		// (get) Token: 0x06009C7C RID: 40060 RVA: 0x0022D780 File Offset: 0x0022B980
		// (set) Token: 0x06009C7D RID: 40061 RVA: 0x0022D7F3 File Offset: 0x0022B9F3
		[Description("Sets/gets the width of the sliding zone")]
		[ClientControlProperty]
		[ClientPropertyName("_width")]
		[Category("Behavior")]
		public override Unit Width
		{
			get
			{
				if (this.ViewState["Width"] != null && !string.IsNullOrEmpty(this.ViewState["Width"].ToString()))
				{
					return (Unit)this.ViewState["Width"];
				}
				if (!this.IsHorizontalSlide())
				{
					return Unit.Percentage(100.0);
				}
				return Unit.Pixel(this.GetDefaultSize());
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17003195 RID: 12693
		// (get) Token: 0x06009C7E RID: 40062 RVA: 0x0022D80B File Offset: 0x0022BA0B
		// (set) Token: 0x06009C7F RID: 40063 RVA: 0x0022D836 File Offset: 0x0022BA36
		[DefaultValue(false)]
		[Description("Sets/gets whether the pane should be clicked in order to open")]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool ClickToOpen
		{
			get
			{
				return this.ViewState["ClickToOpen"] != null && (bool)this.ViewState["ClickToOpen"];
			}
			set
			{
				this.ViewState["ClickToOpen"] = value;
			}
		}

		// Token: 0x17003196 RID: 12694
		// (get) Token: 0x06009C80 RID: 40064 RVA: 0x0022D84E File Offset: 0x0022BA4E
		// (set) Token: 0x06009C81 RID: 40065 RVA: 0x0022D87D File Offset: 0x0022BA7D
		[DefaultValue("")]
		[Category("Behavior")]
		[IDReferenceProperty(typeof(WebControl))]
		[ClientControlProperty]
		[ClientPropertyName("_initiallyDockedPaneId")]
		[SimplePersistenceSetting]
		[Description("Sets/gets the id of the pane that is will be displayed docked")]
		public string DockedPaneId
		{
			get
			{
				if (this.ViewState["DockedPaneId"] == null)
				{
					return "";
				}
				return (string)this.ViewState["DockedPaneId"];
			}
			set
			{
				this.ViewState["DockedPaneId"] = value;
			}
		}

		// Token: 0x17003197 RID: 12695
		// (get) Token: 0x06009C82 RID: 40066 RVA: 0x0022D890 File Offset: 0x0022BA90
		// (set) Token: 0x06009C83 RID: 40067 RVA: 0x0022D8BF File Offset: 0x0022BABF
		[DefaultValue("")]
		[Category("Behavior")]
		[IDReferenceProperty(typeof(WebControl))]
		[ClientControlProperty]
		[ClientPropertyName("_initiallyExpandedPaneId")]
		[SimplePersistenceSetting]
		[Description("Sets/gets the id of the pane that is will be expanded")]
		public string ExpandedPaneId
		{
			get
			{
				if (this.ViewState["ExpandedPaneId"] == null)
				{
					return "";
				}
				return (string)this.ViewState["ExpandedPaneId"];
			}
			set
			{
				this.ViewState["ExpandedPaneId"] = value;
			}
		}

		// Token: 0x17003198 RID: 12696
		// (get) Token: 0x06009C84 RID: 40068 RVA: 0x0022D8D2 File Offset: 0x0022BAD2
		// (set) Token: 0x06009C85 RID: 40069 RVA: 0x0022D90C File Offset: 0x0022BB0C
		[Category("Behavior")]
		[DefaultValue(SplitterSlideDirection.Right)]
		[ClientControlProperty]
		[ClientPropertyName("_slideDirection")]
		[Description("Sets/gets the direction in which the panes will slide")]
		public SplitterSlideDirection SlideDirection
		{
			get
			{
				if (this.ViewState["SlideDirection"] != null)
				{
					return (SplitterSlideDirection)this.ViewState["SlideDirection"];
				}
				if (!this.Splitter.IsHorizontal())
				{
					return SplitterSlideDirection.Right;
				}
				return SplitterSlideDirection.Bottom;
			}
			set
			{
				this.ViewState["SlideDirection"] = value;
			}
		}

		// Token: 0x17003199 RID: 12697
		// (get) Token: 0x06009C86 RID: 40070 RVA: 0x0022D924 File Offset: 0x0022BB24
		// (set) Token: 0x06009C87 RID: 40071 RVA: 0x0022D94F File Offset: 0x0022BB4F
		[Category("Appearance")]
		[Description("Sets/gets the step in px in which the resize bar will be moved when dragged.")]
		[ClientControlProperty]
		[DefaultValue(0)]
		public int ResizeStep
		{
			get
			{
				if (this.ViewState["ResizeStep"] == null)
				{
					return 0;
				}
				return (int)this.ViewState["ResizeStep"];
			}
			set
			{
				this.ViewState["ResizeStep"] = value;
			}
		}

		// Token: 0x1700319A RID: 12698
		// (get) Token: 0x06009C88 RID: 40072 RVA: 0x0022D967 File Offset: 0x0022BB67
		// (set) Token: 0x06009C89 RID: 40073 RVA: 0x0022D996 File Offset: 0x0022BB96
		[DefaultValue(300)]
		[Description("Sets/gets the duration of the slide animation in milliseconds.")]
		[ClientControlProperty]
		[Category("Appearance")]
		public int SlideDuration
		{
			get
			{
				if (this.ViewState["SlideDuration"] == null)
				{
					return 300;
				}
				return (int)this.ViewState["SlideDuration"];
			}
			set
			{
				this.ViewState["SlideDuration"] = value;
			}
		}

		// Token: 0x1700319B RID: 12699
		// (get) Token: 0x06009C8A RID: 40074 RVA: 0x0022D9AE File Offset: 0x0022BBAE
		// (set) Token: 0x06009C8B RID: 40075 RVA: 0x0022D9B6 File Offset: 0x0022BBB6
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientLoad property instead.", false)]
		public string OnClientLoaded
		{
			get
			{
				return this.OnClientLoad;
			}
			set
			{
				this.OnClientLoad = value;
			}
		}

		// Token: 0x1700319C RID: 12700
		// (get) Token: 0x06009C8C RID: 40076 RVA: 0x0022D9BF File Offset: 0x0022BBBF
		// (set) Token: 0x06009C8D RID: 40077 RVA: 0x0022D9DF File Offset: 0x0022BBDF
		[Description("The name of the javascript function called when the initialization of the sliding zone is done.")]
		[ClientPropertyName("load")]
		[DefaultValue("")]
		[Category("ClientSideEvents")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x06009C8E RID: 40078 RVA: 0x0022D9F2 File Offset: 0x0022BBF2
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.PreRenderComplete += this.Page_PreRenderComplete;
			}
		}

		// Token: 0x06009C8F RID: 40079 RVA: 0x0022DA1C File Offset: 0x0022BC1C
		protected override void ControlPreRender()
		{
			if (this.Page != null && this.Page.Form != null && this.RegisterWithScriptManager && base.ScriptManager != null && base.ScriptManager.LoadScriptsBeforeUI)
			{
				this.RegisterInitializeScriptWithScriptManager();
			}
			RadPane parentPane = base.GetParentPane();
			if (parentPane == null)
			{
				throw new NotSupportedException(string.Format("{0} must be placed inside a RadPane control.", base.GetType().Name));
			}
			parentPane.ChildSlidingZoneID = this.ClientID;
			base.ControlPreRender();
		}

		// Token: 0x06009C90 RID: 40080 RVA: 0x0022DA98 File Offset: 0x0022BC98
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

		// Token: 0x06009C91 RID: 40081 RVA: 0x0022DAE8 File Offset: 0x0022BCE8
		private void RenderPaneTabs(HtmlTextWriter writer)
		{
			ArrayList panes = this.GetPanes();
			foreach (object obj in panes)
			{
				RadSlidingPane radSlidingPane = (RadSlidingPane)obj;
				if (radSlidingPane.Visible)
				{
					writer.Write(radSlidingPane.GetTabHtml());
				}
			}
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x0022DB50 File Offset: 0x0022BD50
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			this.RenderClientStateField(writer);
			BaseClass.RenderAjaxCssReferences(this, writer);
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x0022DB60 File Offset: 0x0022BD60
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.GetParentPane().Scrolling = SplitterPaneScrolling.None;
			bool flag = this.IsHorizontalSlide();
			bool flag2 = !flag;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rspSlideZone {0}", flag ? "rspSlideZoneHorizontal" : "rspSlideZoneVertical"));
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			string arg = string.Format(" style=\"vertical-align:top;{0}\"", flag2 ? "" : "display:none;");
			if (flag)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			string value = string.Format("<td id = \"RAD_SLIDING_ZONE_PANES_CONTAINER_{0}\"{1}>", this.ClientID, arg);
			string value2 = string.Format("</td>", new object[0]);
			if (this.SlideDirection == SplitterSlideDirection.Left || this.SlideDirection == SplitterSlideDirection.Top)
			{
				if (flag2)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				writer.Write(value);
				this.RenderPanes(writer);
				writer.Write(value2);
				if (flag2)
				{
					writer.RenderEndTag();
				}
			}
			if (flag2)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			string arg2 = "";
			switch (this.SlideDirection)
			{
			case SplitterSlideDirection.Left:
				arg2 = " rspRight";
				break;
			case SplitterSlideDirection.Top:
				arg2 = " rspTop";
				break;
			case SplitterSlideDirection.Bottom:
				arg2 = " rspBottom";
				break;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rspTabsContainer{0}", arg2));
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("RAD_SLIDING_ZONE_TABS_CONTAINER_{0}", this.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderPaneTabs(writer);
			writer.RenderEndTag();
			if (flag2)
			{
				writer.RenderEndTag();
			}
			if (this.SlideDirection == SplitterSlideDirection.Right || this.SlideDirection == SplitterSlideDirection.Bottom)
			{
				if (flag2)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				writer.Write(value);
				this.RenderPanes(writer);
				writer.Write(value2);
				if (flag2)
				{
					writer.RenderEndTag();
				}
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x0022DD3C File Offset: 0x0022BF3C
		protected void RenderPanes(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			ArrayList panes = this.GetPanes();
			foreach (object obj in panes)
			{
				RadSlidingPane radSlidingPane = (RadSlidingPane)obj;
				if (radSlidingPane.Visible)
				{
					radSlidingPane.RenderControl(writer);
				}
			}
		}

		// Token: 0x06009C95 RID: 40085 RVA: 0x0022DDA8 File Offset: 0x0022BFA8
		internal int GetDefaultSize()
		{
			if (!this.Splitter.IsTouchSkin())
			{
				return 22;
			}
			return 40;
		}

		// Token: 0x1700319D RID: 12701
		// (get) Token: 0x06009C96 RID: 40086 RVA: 0x0022DDBC File Offset: 0x0022BFBC
		[Browsable(false)]
		public RadSplitter Splitter
		{
			get
			{
				RadPane parentPane = base.GetParentPane();
				if (parentPane == null)
				{
					throw new NotSupportedException(string.Format("{0} must be placed inside a RadPane control.", base.GetType().Name));
				}
				return parentPane.Splitter;
			}
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x0022DDF4 File Offset: 0x0022BFF4
		protected bool IsHorizontalSlide()
		{
			SplitterSlideDirection slideDirection = this.SlideDirection;
			return slideDirection == SplitterSlideDirection.Left || slideDirection == SplitterSlideDirection.Right;
		}

		// Token: 0x06009C98 RID: 40088 RVA: 0x0022DE14 File Offset: 0x0022C014
		public ArrayList GetPanes()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.Items)
			{
				SplitterItem splitterItem = (SplitterItem)obj;
				if (splitterItem is RadSlidingPane)
				{
					arrayList.Add(splitterItem);
				}
			}
			return arrayList;
		}

		// Token: 0x06009C99 RID: 40089 RVA: 0x0022DE80 File Offset: 0x0022C080
		public RadSlidingPane GetPaneById(string paneId)
		{
			return (RadSlidingPane)base.GetItemById(paneId);
		}

		// Token: 0x06009C9A RID: 40090 RVA: 0x0022DE90 File Offset: 0x0022C090
		private string GetInitializeScript()
		{
			RadPane parentPane = base.GetParentPane();
			if (parentPane == null)
			{
				throw new NotSupportedException(string.Format("{0} must be placed inside a RadPane control.", base.GetType().Name));
			}
			return string.Format("Telerik.Web.UI.RadSlidingZone._preInitialize(\"{0}\", \"{1}\", \"{2}\");", this.ClientID, this.Splitter.ClientID, parentPane.ClientID);
		}

		// Token: 0x06009C9B RID: 40091 RVA: 0x0022DEE4 File Offset: 0x0022C0E4
		protected override void RegisterInitializeScriptWithScriptManager()
		{
			string initializeScript = this.GetInitializeScript();
			ScriptManager.RegisterStartupScript(this.Page, typeof(RadSlidingZone), this.ClientID + initializeScript, initializeScript, true);
		}

		// Token: 0x1700319E RID: 12702
		// (get) Token: 0x06009C9C RID: 40092 RVA: 0x0022DF1B File Offset: 0x0022C11B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new string Skin
		{
			get
			{
				return this._skin;
			}
		}

		// Token: 0x1700319F RID: 12703
		// (get) Token: 0x06009C9D RID: 40093 RVA: 0x0022DF23 File Offset: 0x0022C123
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170031A0 RID: 12704
		// (get) Token: 0x06009C9E RID: 40094 RVA: 0x0022DF26 File Offset: 0x0022C126
		[DefaultValue(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009C9F RID: 40095 RVA: 0x0022DF2C File Offset: 0x0022C12C
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			try
			{
				base.LoadClientState(clientState);
				this.ClickToOpen = (bool)clientState["clickToOpen"];
				this.ResizeStep = (int)clientState["resizeStep"];
				this.SlideDuration = (int)clientState["slideDuration"];
				this.DockedPaneId = clientState["dockedPaneId"].ToString();
				this.ExpandedPaneId = clientState["expandedPaneId"].ToString();
			}
			catch
			{
			}
		}

		// Token: 0x06009CA0 RID: 40096 RVA: 0x0022DFC4 File Offset: 0x0022C1C4
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "clickToOpen", this.ClickToOpen, false);
			base.DescribeIDReferenceProperty(descriptor, "_initiallyDockedPaneId", this.DockedPaneId);
			base.DescribeIDReferenceProperty(descriptor, "_initiallyExpandedPaneId", this.ExpandedPaneId);
			base.DescribeProperty<string>(descriptor, "_height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<int>(descriptor, "resizeStep", this.ResizeStep, 0);
			base.DescribeProperty<SplitterSlideDirection>(descriptor, "_slideDirection", this.SlideDirection, SplitterSlideDirection.Right);
			base.DescribeProperty<int>(descriptor, "slideDuration", this.SlideDuration, 300);
			base.DescribeProperty<string>(descriptor, "_width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009CA1 RID: 40097 RVA: 0x0022E094 File Offset: 0x0022C294
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002C11 RID: 11281
		private string _skin = string.Empty;
	}
}
