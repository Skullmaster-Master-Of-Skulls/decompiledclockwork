using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FC1 RID: 4033
	[ToolboxData("<{0}:RadSplitBar Runat=server></{0}:RadSplitBar>")]
	[RequiredScript(typeof(ResizeExtender))]
	[ClientScriptResource("Telerik.Web.UI.RadSplitBar", "Telerik.Web.UI.Splitter.RadSplitterScripts.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Container")]
	[ToolboxBitmap(typeof(RadSplitBar), "Telerik.Web.UI.Splitter.png")]
	public class RadSplitBar : SplitterItem
	{
		// Token: 0x170031A1 RID: 12705
		// (get) Token: 0x06009CA3 RID: 40099 RVA: 0x0022E0C1 File Offset: 0x0022C2C1
		// (set) Token: 0x06009CA4 RID: 40100 RVA: 0x0022E0E2 File Offset: 0x0022C2E2
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Sets/gets the collapse mode of the splitbar")]
		[DefaultValue(SplitBarCollapseMode.None)]
		public SplitBarCollapseMode CollapseMode
		{
			get
			{
				return (SplitBarCollapseMode)(this.ViewState["CollapseMode"] ?? SplitBarCollapseMode.None);
			}
			set
			{
				this.ViewState["CollapseMode"] = value;
			}
		}

		// Token: 0x170031A2 RID: 12706
		// (get) Token: 0x06009CA5 RID: 40101 RVA: 0x0022E0FA File Offset: 0x0022C2FA
		// (set) Token: 0x06009CA6 RID: 40102 RVA: 0x0022E11B File Offset: 0x0022C31B
		[Description("Sets/gets whether the resize bar will be active")]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
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

		// Token: 0x170031A3 RID: 12707
		// (get) Token: 0x06009CA7 RID: 40103 RVA: 0x0022E133 File Offset: 0x0022C333
		// (set) Token: 0x06009CA8 RID: 40104 RVA: 0x0022E154 File Offset: 0x0022C354
		[Category("Appearance")]
		[Description("Sets/gets the step in px in which the resize bar will be moved when dragged.")]
		[DefaultValue(0)]
		[ClientControlProperty]
		public int ResizeStep
		{
			get
			{
				return (int)(this.ViewState["ResizeStep"] ?? 0);
			}
			set
			{
				this.ViewState["ResizeStep"] = value;
			}
		}

		// Token: 0x170031A4 RID: 12708
		// (get) Token: 0x06009CA9 RID: 40105 RVA: 0x0022E16C File Offset: 0x0022C36C
		[Browsable(false)]
		public RadSplitter Splitter
		{
			get
			{
				RadSplitter radSplitter = this.Parent as RadSplitter;
				if (radSplitter == null)
				{
					throw new NotSupportedException(string.Format("{0} must be placed inside a RadSplitter control.", base.GetType().Name));
				}
				return radSplitter;
			}
		}

		// Token: 0x170031A5 RID: 12709
		// (get) Token: 0x06009CAA RID: 40106 RVA: 0x0022E1A4 File Offset: 0x0022C3A4
		// (set) Token: 0x06009CAB RID: 40107 RVA: 0x0022E1C4 File Offset: 0x0022C3C4
		[Localizable(true)]
		[DefaultValue("Collapse/expand the {0} pane")]
		public string CollapseExpandPaneText
		{
			get
			{
				return (string)(this.ViewState["CollapseExpandPaneText"] ?? "Collapse/expand the {0} pane");
			}
			set
			{
				this.ViewState["CollapseExpandPaneText"] = value;
			}
		}

		// Token: 0x170031A6 RID: 12710
		// (get) Token: 0x06009CAC RID: 40108 RVA: 0x0022E1D7 File Offset: 0x0022C3D7
		// (set) Token: 0x06009CAD RID: 40109 RVA: 0x0022E205 File Offset: 0x0022C405
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadSplitBarAdjacentPanesNames AdjacentPanesNames
		{
			get
			{
				if (this._panesNames == null)
				{
					this._panesNames = new RadSplitBarAdjacentPanesNames();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._panesNames).TrackViewState();
					}
				}
				return this._panesNames;
			}
			set
			{
				this._panesNames = value;
			}
		}

		// Token: 0x06009CAE RID: 40110 RVA: 0x0022E210 File Offset: 0x0022C410
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.AdjacentPanesNames).LoadViewState(array[1]);
		}

		// Token: 0x06009CAF RID: 40111 RVA: 0x0022E23C File Offset: 0x0022C43C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.AdjacentPanesNames).SaveViewState()
			};
		}

		// Token: 0x06009CB0 RID: 40112 RVA: 0x0022E26A File Offset: 0x0022C46A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.AdjacentPanesNames).TrackViewState();
		}

		// Token: 0x06009CB1 RID: 40113 RVA: 0x0022E280 File Offset: 0x0022C480
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = this.Splitter.IsHorizontal();
			string text = flag ? "rspResizeBarHorizontal" : "rspResizeBar";
			string cssClass = this.CssClass;
			if (!string.IsNullOrEmpty(cssClass))
			{
				text = string.Format("{0} {1}", text, cssClass);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			int borderSize = this.Splitter.BorderSize;
			string value = string.Format("{0}px", (borderSize > 0) ? borderSize : 1);
			if (!this.Splitter.IsNested() && borderSize > 0)
			{
				writer.AddStyleAttribute("border-right-width", value);
				writer.AddStyleAttribute("border-bottom-width", value);
			}
			else if (flag)
			{
				writer.AddStyleAttribute("border-bottom-width", value);
				if (borderSize == 0)
				{
					writer.AddStyleAttribute("border-top-width", value);
				}
			}
			else
			{
				writer.AddStyleAttribute("border-right-width", value);
				if (borderSize == 0)
				{
					writer.AddStyleAttribute("border-left-width", value);
				}
			}
			if (this.Splitter.BorderStyle != BorderStyle.NotSet)
			{
				writer.AddStyleAttribute("border-style", this.Splitter.BorderStyle.ToString());
			}
			if (!this.Splitter.BorderColor.IsEmpty)
			{
				WebColorConverter webColorConverter = new WebColorConverter();
				writer.AddStyleAttribute("border-color", webColorConverter.ConvertToString(this.Splitter.BorderColor));
			}
		}

		// Token: 0x06009CB2 RID: 40114 RVA: 0x0022E3DC File Offset: 0x0022C5DC
		protected override void ControlPreRender()
		{
			if (!base.DesignMode && (this.GetPreviousPaneId() == null || this.GetNextPaneId() == null))
			{
				this.EnableResize = false;
			}
			if (this.Page != null && this.Page.Form != null && this.RegisterWithScriptManager && base.ScriptManager != null && base.ScriptManager.LoadScriptsBeforeUI)
			{
				this.RegisterInitializeScriptWithScriptManager();
			}
			base.ControlPreRender();
		}

		// Token: 0x06009CB3 RID: 40115 RVA: 0x0022E446 File Offset: 0x0022C646
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				if (this.Splitter.IsHorizontal())
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				this.AddAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
			}
			else
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			BaseClass.RenderVersionStamp(writer);
		}

		// Token: 0x06009CB4 RID: 40116 RVA: 0x0022E484 File Offset: 0x0022C684
		protected void RenderCollapseBars(HtmlTextWriter writer)
		{
			SplitBarCollapseMode collapseMode = this.CollapseMode;
			if (collapseMode != SplitBarCollapseMode.None)
			{
				bool flag = this.CollapseMode == SplitBarCollapseMode.Both;
				if (collapseMode == SplitBarCollapseMode.Forward || flag)
				{
					this.RenderCollapseBar(writer, SplitterCollapseDirection.Forward);
				}
				if (collapseMode == SplitBarCollapseMode.Backward || flag)
				{
					this.RenderCollapseBar(writer, SplitterCollapseDirection.Backward);
				}
			}
			bool flag2 = this.Splitter.IsHorizontal();
			Unit splitBarsSize = this.Splitter.SplitBarsSize;
			Unit unit = (splitBarsSize != Unit.Empty) ? splitBarsSize : Unit.Pixel(this.GetDefaultSize());
			string arg = flag2 ? "1px" : unit.ToString();
			string arg2 = flag2 ? unit.ToString() : "1px";
			writer.WriteBeginTag("input");
			writer.WriteAttribute("id", string.Format("RAD_SPLITBAR_SPACER_{0}", this.ClientID));
			writer.WriteAttribute("class", "rspCollapseBarSpacer");
			writer.WriteAttribute("style", string.Format("width:{0};height:{1};line-height:{1};", arg, arg2));
			writer.WriteAttribute("type", "button");
			writer.WriteAttribute("value", " ");
			writer.Write(" />");
		}

		// Token: 0x06009CB5 RID: 40117 RVA: 0x0022E5A5 File Offset: 0x0022C7A5
		protected void RenderCollapseBar(HtmlTextWriter writer, SplitterCollapseDirection direction)
		{
			if (this.Splitter.ResolvedRenderMode == RenderMode.Classic)
			{
				this.RenderCollapseBar_Classic(writer, direction);
				return;
			}
			this.RenderCollapseBar_Lightweight(writer, direction);
		}

		// Token: 0x06009CB6 RID: 40118 RVA: 0x0022E5C8 File Offset: 0x0022C7C8
		protected void RenderCollapseBar_Classic(HtmlTextWriter writer, SplitterCollapseDirection direction)
		{
			writer.WriteBeginTag("input");
			writer.WriteAttribute("id", string.Format("RAD_SPLITTER_BAR_COLLAPSE_{0}_{1}", direction, this.ClientID));
			writer.WriteAttribute("class", this.GetSplitButtonCssClass(direction));
			writer.WriteAttribute("title", string.Format(this.CollapseExpandPaneText, this.GetAdjacentPaneName(direction)));
			writer.WriteAttribute("type", "button");
			writer.WriteAttribute("value", " ");
			writer.Write(" />");
		}

		// Token: 0x06009CB7 RID: 40119 RVA: 0x0022E65C File Offset: 0x0022C85C
		protected void RenderCollapseBar_Lightweight(HtmlTextWriter writer, SplitterCollapseDirection direction)
		{
			writer.WriteBeginTag("span");
			writer.WriteAttribute("id", string.Format("RAD_SPLITTER_BAR_COLLAPSE_{0}_{1}", direction, this.ClientID));
			writer.WriteAttribute("class", this.GetSplitButtonCssClass(direction));
			writer.WriteAttribute("title", string.Format(this.CollapseExpandPaneText, this.GetAdjacentPaneName(direction)));
			writer.Write('>');
			writer.WriteEndTag("span");
		}

		// Token: 0x06009CB8 RID: 40120 RVA: 0x0022E6D8 File Offset: 0x0022C8D8
		private string GetSplitButtonCssClass(SplitterCollapseDirection direction)
		{
			bool flag = this.Splitter.IsHorizontal();
			bool flag2 = direction == SplitterCollapseDirection.Forward;
			if (!flag)
			{
				if (!flag2)
				{
					return "rspCollapseBarExpand";
				}
				return "rspCollapseBarCollapse";
			}
			else
			{
				if (!flag2)
				{
					return "rspCollapseBarHorizontalExpand";
				}
				return "rspCollapseBarHorizontalCollapse";
			}
		}

		// Token: 0x06009CB9 RID: 40121 RVA: 0x0022E718 File Offset: 0x0022C918
		private string GetAdjacentPaneName(SplitterCollapseDirection direction)
		{
			bool flag = this.Splitter.IsHorizontal();
			bool flag2 = direction == SplitterCollapseDirection.Forward;
			if (flag)
			{
				if (!flag2)
				{
					return this.AdjacentPanesNames.BottomPaneName;
				}
				return this.AdjacentPanesNames.TopPaneName;
			}
			else
			{
				if (!flag2)
				{
					return this.AdjacentPanesNames.RightPaneName;
				}
				return this.AdjacentPanesNames.LeftPaneName;
			}
		}

		// Token: 0x06009CBA RID: 40122 RVA: 0x0022E770 File Offset: 0x0022C970
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderCollapseBars(writer);
			if ((this.Page != null && this.Page.Form == null) || !this.RegisterWithScriptManager)
			{
				string text = string.Format("<script type=\"text/javascript\">{0}</script>", this.GetInitializeScript());
				LiteralControl literalControl = new LiteralControl(text);
				literalControl.RenderControl(writer);
			}
		}

		// Token: 0x06009CBB RID: 40123 RVA: 0x0022E7C0 File Offset: 0x0022C9C0
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderClientStateField(writer);
			writer.RenderEndTag();
			BaseClass.RenderAjaxCssReferences(this, writer);
			if (!base.DesignMode)
			{
				writer.RenderEndTag();
				if (this.Splitter.IsHorizontal())
				{
					writer.RenderEndTag();
					return;
				}
			}
			else
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06009CBC RID: 40124 RVA: 0x0022E820 File Offset: 0x0022CA20
		private string GetPreviousPaneId()
		{
			if (base.Index == 0)
			{
				return null;
			}
			for (int i = base.Index - 1; i > -1; i--)
			{
				SplitterItem splitterItem = this.Splitter.Items[i];
				if (splitterItem is RadPane && splitterItem.Visible)
				{
					return splitterItem.ClientID;
				}
			}
			return null;
		}

		// Token: 0x06009CBD RID: 40125 RVA: 0x0022E874 File Offset: 0x0022CA74
		private string GetNextPaneId()
		{
			if (base.Index == this.Splitter.Items.Count - 1)
			{
				return null;
			}
			for (int i = base.Index + 1; i < this.Splitter.Items.Count; i++)
			{
				SplitterItem splitterItem = this.Splitter.Items[i];
				if (splitterItem is RadPane && splitterItem.Visible)
				{
					return splitterItem.ClientID;
				}
			}
			return null;
		}

		// Token: 0x06009CBE RID: 40126 RVA: 0x0022E8E9 File Offset: 0x0022CAE9
		private Unit GetWidth()
		{
			if (!this.Splitter.IsHorizontal())
			{
				return this.Splitter.SplitBarsSize;
			}
			return this.Splitter.GetInnerSize(false);
		}

		// Token: 0x06009CBF RID: 40127 RVA: 0x0022E910 File Offset: 0x0022CB10
		private Unit GetHeight()
		{
			if (!this.Splitter.IsHorizontal())
			{
				return this.Splitter.GetInnerSize(false);
			}
			return this.Splitter.SplitBarsSize;
		}

		// Token: 0x06009CC0 RID: 40128 RVA: 0x0022E938 File Offset: 0x0022CB38
		private int GetIndexInSplitBars()
		{
			int num = 0;
			for (int i = 0; i < base.Index; i++)
			{
				SplitterItem splitterItem = this.Splitter.Items[i];
				if (splitterItem is RadSplitBar)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06009CC1 RID: 40129 RVA: 0x0022E978 File Offset: 0x0022CB78
		private string GetInitializeScript()
		{
			string text = this.GetPreviousPaneId();
			string text2 = this.GetNextPaneId();
			if (text == null)
			{
				text = string.Empty;
			}
			if (text2 == null)
			{
				text2 = string.Empty;
			}
			return string.Format("Telerik.Web.UI.RadSplitBar._preInitialize(\"{0}\", \"{1}\", \"{2}\", \"{3}\", {4}, {5});", new object[]
			{
				this.ClientID,
				this.Splitter.ClientID,
				text,
				text2,
				base.Index,
				this.GetIndexInSplitBars()
			});
		}

		// Token: 0x06009CC2 RID: 40130 RVA: 0x0022E9F4 File Offset: 0x0022CBF4
		internal override void RegisterInitializeScriptWithScriptManager()
		{
			string initializeScript = this.GetInitializeScript();
			ScriptManager.RegisterStartupScript(this.Page, typeof(RadSplitBar), this.ClientID + initializeScript, initializeScript, true);
		}

		// Token: 0x06009CC3 RID: 40131 RVA: 0x0022EA2B File Offset: 0x0022CC2B
		internal int GetDefaultSize()
		{
			if (!this.Splitter.IsTouchSkin())
			{
				return 4;
			}
			return 14;
		}

		// Token: 0x06009CC4 RID: 40132 RVA: 0x0022EA3E File Offset: 0x0022CC3E
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_liveResize", this.Splitter.LiveResize);
		}

		// Token: 0x06009CC5 RID: 40133 RVA: 0x0022EA64 File Offset: 0x0022CC64
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<SplitBarCollapseMode>(descriptor, "collapseMode", this.CollapseMode, SplitBarCollapseMode.None);
			base.DescribeProperty<bool>(descriptor, "enableResize", this.EnableResize, true);
			base.DescribeProperty<int>(descriptor, "resizeStep", this.ResizeStep, 0);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009CC6 RID: 40134 RVA: 0x0022EAB1 File Offset: 0x0022CCB1
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002C12 RID: 11282
		private RadSplitBarAdjacentPanesNames _panesNames;
	}
}
