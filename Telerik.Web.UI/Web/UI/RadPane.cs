using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FBE RID: 4030
	[Designer("Telerik.Web.Design.RadPaneDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadPane Runat=server></{0}:RadPane>")]
	[PersistChildren(true)]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ParseChildren(false)]
	[ToolboxBitmap(typeof(RadPane), "Telerik.Web.UI.Splitter.png")]
	[ClientScriptResource("Telerik.Web.UI.RadPane", "Telerik.Web.UI.Splitter.RadSplitterScripts.js")]
	[TelerikToolboxCategory("Container")]
	public class RadPane : SplitterPaneBase
	{
		// Token: 0x1700316F RID: 12655
		// (get) Token: 0x06009C07 RID: 39943 RVA: 0x0022BA6E File Offset: 0x00229C6E
		// (set) Token: 0x06009C08 RID: 39944 RVA: 0x0022BA8F File Offset: 0x00229C8F
		[ClientControlProperty]
		[Description("Sets/gets whether the pane is collapsed")]
		[SimplePersistenceSetting]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientPropertyName("_collapsed")]
		public bool Collapsed
		{
			get
			{
				return (bool)(this.ViewState["Collapsed"] ?? false);
			}
			set
			{
				this.ViewState["Collapsed"] = value;
			}
		}

		// Token: 0x17003170 RID: 12656
		// (get) Token: 0x06009C09 RID: 39945 RVA: 0x0022BAA7 File Offset: 0x00229CA7
		// (set) Token: 0x06009C0A RID: 39946 RVA: 0x0022BAC8 File Offset: 0x00229CC8
		[DefaultValue(false)]
		[Description("Sets/gets whether the pane is locked")]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool Locked
		{
			get
			{
				return (bool)(this.ViewState["Locked"] ?? false);
			}
			set
			{
				this.ViewState["Locked"] = value;
			}
		}

		// Token: 0x17003171 RID: 12657
		// (get) Token: 0x06009C0B RID: 39947 RVA: 0x0022BAE0 File Offset: 0x00229CE0
		// (set) Token: 0x06009C0C RID: 39948 RVA: 0x0022BB00 File Offset: 0x00229D00
		[UrlProperty]
		[Category("Appearance")]
		[ClientControlProperty]
		[Description("The URL of the page to load inside the pane.")]
		[DefaultValue("")]
		public string ContentUrl
		{
			get
			{
				return (string)(this.ViewState["ContentUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ContentUrl"] = base.ResolveUrl(value);
			}
		}

		// Token: 0x17003172 RID: 12658
		// (get) Token: 0x06009C0D RID: 39949 RVA: 0x0022BB19 File Offset: 0x00229D19
		// (set) Token: 0x06009C0E RID: 39950 RVA: 0x0022BB3A File Offset: 0x00229D3A
		[DefaultValue(true)]
		[ClientControlProperty]
		[Bindable(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the page that is loaded through the ContentUrl property should be shown during the loading process, or a loading sign is displayed instead")]
		[Browsable(true)]
		public bool ShowContentDuringLoad
		{
			get
			{
				return (bool)(this.ViewState["ShowContentDuringLoad"] ?? true);
			}
			set
			{
				this.ViewState["ShowContentDuringLoad"] = value;
			}
		}

		// Token: 0x17003173 RID: 12659
		// (get) Token: 0x06009C0F RID: 39951 RVA: 0x0022BB52 File Offset: 0x00229D52
		// (set) Token: 0x06009C10 RID: 39952 RVA: 0x0022BB77 File Offset: 0x00229D77
		[ClientPropertyName("_width")]
		[Description("Get/Set the Width of the pane.")]
		[TypeConverter(typeof(UnitConverter))]
		[ClientControlProperty]
		[Category("Appearance")]
		[SimplePersistenceSetting]
		[DefaultValue(typeof(Unit), "")]
		public override Unit Width
		{
			get
			{
				return (Unit)(this.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				if (value.ToString().Equals("100%"))
				{
					value = Unit.Empty;
				}
				this.ViewState["Width"] = value;
				this.OriginalWidth = value;
			}
		}

		// Token: 0x17003174 RID: 12660
		// (get) Token: 0x06009C11 RID: 39953 RVA: 0x0022BBB6 File Offset: 0x00229DB6
		// (set) Token: 0x06009C12 RID: 39954 RVA: 0x0022BBDB File Offset: 0x00229DDB
		[Description("Get/Set the Height of the pane.")]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		[ClientControlProperty]
		[ClientPropertyName("_height")]
		[SimplePersistenceSetting]
		[Category("Appearance")]
		public override Unit Height
		{
			get
			{
				return (Unit)(this.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				if (value.ToString().Equals("100%"))
				{
					value = Unit.Empty;
				}
				this.ViewState["Height"] = value;
				this.OriginalHeight = value;
			}
		}

		// Token: 0x17003175 RID: 12661
		// (get) Token: 0x06009C13 RID: 39955 RVA: 0x0022BC1A File Offset: 0x00229E1A
		// (set) Token: 0x06009C14 RID: 39956 RVA: 0x0022BC40 File Offset: 0x00229E40
		[Category("Appearance")]
		[DefaultValue(20)]
		[ClientControlProperty]
		[Description("Sets/gets the min width to which the pane can be resized")]
		public override int MinWidth
		{
			get
			{
				return (int)(this.ViewState["MinWidth"] ?? this.GetDefaultMinSize());
			}
			set
			{
				base.MinWidth = value;
			}
		}

		// Token: 0x17003176 RID: 12662
		// (get) Token: 0x06009C15 RID: 39957 RVA: 0x0022BC49 File Offset: 0x00229E49
		// (set) Token: 0x06009C16 RID: 39958 RVA: 0x0022BC6F File Offset: 0x00229E6F
		[DefaultValue(20)]
		[Description("Sets/gets the min height to which the pane can be resized")]
		[ClientControlProperty]
		[Category("Appearance")]
		public override int MinHeight
		{
			get
			{
				return (int)(this.ViewState["MinHeight"] ?? this.GetDefaultMinSize());
			}
			set
			{
				base.MinHeight = value;
			}
		}

		// Token: 0x17003177 RID: 12663
		// (get) Token: 0x06009C17 RID: 39959 RVA: 0x0022BC78 File Offset: 0x00229E78
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

		// Token: 0x17003178 RID: 12664
		// (get) Token: 0x06009C18 RID: 39960 RVA: 0x0022BCB0 File Offset: 0x00229EB0
		// (set) Token: 0x06009C19 RID: 39961 RVA: 0x0022BCB8 File Offset: 0x00229EB8
		[SimplePersistenceSetting]
		internal Unit ExpandedSize
		{
			get
			{
				return this.GetExpandedSize();
			}
			set
			{
				this.SetExpandedSize(value);
			}
		}

		// Token: 0x17003179 RID: 12665
		// (get) Token: 0x06009C1A RID: 39962 RVA: 0x0022BCC1 File Offset: 0x00229EC1
		// (set) Token: 0x06009C1B RID: 39963 RVA: 0x0022BCE7 File Offset: 0x00229EE7
		private Unit OriginalHeight
		{
			get
			{
				return (Unit)(this.ViewState["OriginalHeight"] ?? this.undefinedSize);
			}
			set
			{
				this.ViewState["OriginalHeight"] = value;
			}
		}

		// Token: 0x1700317A RID: 12666
		// (get) Token: 0x06009C1C RID: 39964 RVA: 0x0022BCFF File Offset: 0x00229EFF
		// (set) Token: 0x06009C1D RID: 39965 RVA: 0x0022BD25 File Offset: 0x00229F25
		private Unit OriginalWidth
		{
			get
			{
				return (Unit)(this.ViewState["OriginalWidth"] ?? this.undefinedSize);
			}
			set
			{
				this.ViewState["OriginalWidth"] = value;
			}
		}

		// Token: 0x1700317B RID: 12667
		// (get) Token: 0x06009C1E RID: 39966 RVA: 0x0022BD3D File Offset: 0x00229F3D
		// (set) Token: 0x06009C1F RID: 39967 RVA: 0x0022BD5E File Offset: 0x00229F5E
		private int CollapsedDirection
		{
			get
			{
				return (int)(this.ViewState["CollapsedDirection"] ?? 1);
			}
			set
			{
				this.ViewState["CollapsedDirection"] = value;
			}
		}

		// Token: 0x06009C20 RID: 39968 RVA: 0x0022BD78 File Offset: 0x00229F78
		protected override void ControlPreRender()
		{
			if (this.Page != null && this.Page.Form != null && this.RegisterWithScriptManager && base.ScriptManager != null && base.ScriptManager.LoadScriptsBeforeUI)
			{
				this.RegisterInitializeScriptWithScriptManager();
			}
			RadSplitter splitter = this.Splitter;
			if (splitter.IsHorizontal())
			{
				Unit width = splitter.Width;
				if (!width.IsEmpty && width.Type == UnitType.Pixel)
				{
					this.Width = splitter.GetInnerSize(false);
				}
			}
			else
			{
				Unit height = splitter.Height;
				if (!height.IsEmpty && height.Type == UnitType.Pixel)
				{
					this.Height = splitter.GetInnerSize(false);
				}
			}
			base.ControlPreRender();
		}

		// Token: 0x06009C21 RID: 39969 RVA: 0x0022BE24 File Offset: 0x0022A024
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			string text = this.Splitter.IsHorizontal() ? "rspPaneHorizontal" : "rspPane";
			if (base.Index == 0)
			{
				text += " rspFirstItem";
			}
			if (base.Index == this.Splitter.Items.Count - 1)
			{
				text += " rspLastItem";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			int borderSize = this.Splitter.BorderSize;
			string arg;
			if (!this.Splitter.IsNested())
			{
				arg = string.Format("border-right-width:{0}px;border-bottom-width:{0}px;", borderSize);
			}
			else if (this.Splitter.IsHorizontal())
			{
				arg = string.Format("border-bottom-width:{0}px;", borderSize);
			}
			else
			{
				arg = string.Format("border-right-width:{0}px;", borderSize);
			}
			string arg2 = (this.Splitter.BorderStyle == BorderStyle.NotSet) ? string.Empty : string.Format("border-style:{0};", this.Splitter.BorderStyle.ToString());
			string arg3 = this.Splitter.BorderColor.IsEmpty ? string.Empty : string.Format("border-color:{0};", new WebColorConverter().ConvertToString(this.Splitter.BorderColor));
			writer.AddAttribute(HtmlTextWriterAttribute.Style, string.Format("{0}{1}{2}", arg, arg2, arg3));
		}

		// Token: 0x06009C22 RID: 39970 RVA: 0x0022BF8C File Offset: 0x0022A18C
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!base.DesignMode && this.Splitter.IsHorizontal())
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("RAD_SPLITTER_PANE_TR_{0}", this.ClientID));
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			this.AddAttributesToRender(writer);
			if (!base.DesignMode)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
			}
			else
			{
				if (this.Splitter.IsHorizontal())
				{
					writer.AddStyleAttribute("display", "inline");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			BaseClass.RenderVersionStamp(writer);
			if ((this.Page != null && this.Page.Form == null) || !this.RegisterWithScriptManager)
			{
				string text = string.Format("<script type=\"text/javascript\">{0}</script>", this.GetInitializeScript());
				LiteralControl literalControl = new LiteralControl(text);
				literalControl.RenderControl(writer);
			}
		}

		// Token: 0x06009C23 RID: 39971 RVA: 0x0022C050 File Offset: 0x0022A250
		protected override void RenderContents(HtmlTextWriter writer)
		{
			string text = (!string.IsNullOrEmpty(this.CssClass)) ? string.Format(" class='{0}' ", this.CssClass) : "";
			writer.Write(string.Format("<div id=\"{0}\" {1} style=\"{2}{3}{4}{5}{6}{7}\">", new object[]
			{
				string.Format("RAD_SPLITTER_PANE_CONTENT_{0}", this.ClientID),
				text,
				this.GetWidthAttribute(),
				this.GetHeightAttribute(),
				base.GetScrollOverflowStyle(),
				base.GetBackColorStyle(),
				base.GetForeColorStyle(),
				base.GetBorderStyle(this.Splitter.PanesBorderSize)
			}));
			if (!this.IsExternalContent)
			{
				base.RenderContents(writer);
			}
			writer.Write("</div>");
		}

		// Token: 0x06009C24 RID: 39972 RVA: 0x0022C10C File Offset: 0x0022A30C
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			this.RenderClientStateField(writer);
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

		// Token: 0x06009C25 RID: 39973 RVA: 0x0022C144 File Offset: 0x0022A344
		private string GetWidthAttribute()
		{
			string result = "";
			Unit unit = this.GetWidth();
			if (!unit.IsEmpty && unit.Type == UnitType.Pixel)
			{
				if (!base.DesignMode && this.Splitter.Browser.IsBrowser("Safari") && unit.Value == 0.0)
				{
					unit = Unit.Pixel(1);
				}
				result = string.Format("width:{0};", unit);
			}
			return result;
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x0022C1BC File Offset: 0x0022A3BC
		private string GetHeightAttribute()
		{
			string result = "";
			Unit unit = this.GetHeight();
			if (!unit.IsEmpty && unit.Type == UnitType.Pixel)
			{
				if (!base.DesignMode && this.Splitter.Browser.IsBrowser("Safari") && unit.Value == 0.0)
				{
					unit = Unit.Pixel(1);
				}
				result = string.Format("height:{0};", unit);
			}
			return result;
		}

		// Token: 0x1700317C RID: 12668
		// (get) Token: 0x06009C27 RID: 39975 RVA: 0x0022C233 File Offset: 0x0022A433
		private bool IsExternalContent
		{
			get
			{
				return !string.IsNullOrEmpty(this.ContentUrl);
			}
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x0022C244 File Offset: 0x0022A444
		internal Unit GetWidth()
		{
			RadSplitter splitter = this.Splitter;
			Unit result = Unit.Empty;
			Unit width = this.Width;
			bool flag = !width.IsEmpty && width.Type == UnitType.Pixel;
			if (!splitter.IsHorizontal())
			{
				if (flag)
				{
					result = width;
				}
				else
				{
					result = splitter.GetNotFixedPaneSize(width, !splitter.IsFixedSize());
				}
			}
			else
			{
				result = splitter.GetInnerSize(false);
				if (result.IsEmpty || result.Type != UnitType.Pixel)
				{
					if (flag)
					{
						result = width;
					}
					else
					{
						result = splitter.GetInnerSize(true);
					}
				}
			}
			return result;
		}

		// Token: 0x06009C29 RID: 39977 RVA: 0x0022C2CC File Offset: 0x0022A4CC
		internal Unit GetHeight()
		{
			RadSplitter splitter = this.Splitter;
			Unit result = Unit.Empty;
			Unit height = this.Height;
			bool flag = !height.IsEmpty && height.Type == UnitType.Pixel;
			if (splitter.IsHorizontal())
			{
				if (flag)
				{
					result = height;
				}
				else
				{
					result = splitter.GetNotFixedPaneSize(height, !splitter.IsFixedSize());
				}
			}
			else
			{
				result = splitter.GetInnerSize(false);
				if (result.IsEmpty || result.Type != UnitType.Pixel)
				{
					if (flag)
					{
						result = height;
					}
					else
					{
						result = splitter.GetInnerSize(true);
					}
				}
			}
			return result;
		}

		// Token: 0x06009C2A RID: 39978 RVA: 0x0022C354 File Offset: 0x0022A554
		private int GetIndexInPanes()
		{
			int num = 0;
			SplitterItemsCollection items = this.Splitter.Items;
			int i = 0;
			int index = base.Index;
			while (i < index)
			{
				if (items[i] is RadPane)
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x06009C2B RID: 39979 RVA: 0x0022C398 File Offset: 0x0022A598
		private bool IsLastPane()
		{
			SplitterItemsCollection items = this.Splitter.Items;
			int i = base.Index + 1;
			int count = items.Count;
			while (i < count)
			{
				if (items[i] is RadPane)
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06009C2C RID: 39980 RVA: 0x0022C3DC File Offset: 0x0022A5DC
		internal Unit GetVarSize()
		{
			if (!this.Splitter.IsHorizontal())
			{
				return this.Width;
			}
			return this.Height;
		}

		// Token: 0x06009C2D RID: 39981 RVA: 0x0022C3F8 File Offset: 0x0022A5F8
		private Unit GetOriginalWidth()
		{
			if (!this.IsUndefinedSize(this.OriginalWidth))
			{
				return this.OriginalWidth;
			}
			return this.Width;
		}

		// Token: 0x06009C2E RID: 39982 RVA: 0x0022C415 File Offset: 0x0022A615
		private Unit GetOriginalHeight()
		{
			if (!this.IsUndefinedSize(this.OriginalHeight))
			{
				return this.OriginalHeight;
			}
			return this.Height;
		}

		// Token: 0x06009C2F RID: 39983 RVA: 0x0022C434 File Offset: 0x0022A634
		private Unit GetOriginalVarSize()
		{
			Unit unit = this.Splitter.IsHorizontal() ? this.OriginalHeight : this.OriginalWidth;
			if (!(unit != this.undefinedSize))
			{
				return this.GetVarSize();
			}
			return unit;
		}

		// Token: 0x06009C30 RID: 39984 RVA: 0x0022C474 File Offset: 0x0022A674
		internal bool IsFixedSize()
		{
			Unit originalVarSize = this.GetOriginalVarSize();
			return !originalVarSize.IsEmpty && originalVarSize.Type == UnitType.Pixel;
		}

		// Token: 0x06009C31 RID: 39985 RVA: 0x0022C4A0 File Offset: 0x0022A6A0
		internal bool IsFreeSize()
		{
			return this.GetOriginalWidth().IsEmpty || this.GetOriginalHeight().IsEmpty;
		}

		// Token: 0x06009C32 RID: 39986 RVA: 0x0022C4CD File Offset: 0x0022A6CD
		internal bool IsPercentagesSize()
		{
			return this.IsPercentageUnit(this.GetOriginalWidth()) || this.IsPercentageUnit(this.GetOriginalHeight());
		}

		// Token: 0x06009C33 RID: 39987 RVA: 0x0022C4EC File Offset: 0x0022A6EC
		private string GetPreviousSplitBarId()
		{
			int index = base.Index;
			if (index > 0)
			{
				SplitterItem splitterItem = this.Splitter.Items[index - 1];
				if (splitterItem is RadSplitBar)
				{
					return splitterItem.ClientID;
				}
			}
			return string.Empty;
		}

		// Token: 0x06009C34 RID: 39988 RVA: 0x0022C52C File Offset: 0x0022A72C
		private string GetNextSplitBarId()
		{
			int index = base.Index;
			SplitterItemsCollection items = this.Splitter.Items;
			if (index < items.Count - 1)
			{
				SplitterItem splitterItem = this.Splitter.Items[index + 1];
				if (splitterItem is RadSplitBar)
				{
					return splitterItem.ClientID;
				}
			}
			return string.Empty;
		}

		// Token: 0x06009C35 RID: 39989 RVA: 0x0022C580 File Offset: 0x0022A780
		private string GetInitializeScript()
		{
			return string.Format("Telerik.Web.UI.RadPane._preInitialize(\"{0}\", \"{1}\", \"{2}\", \"{3}\",  {4}, {5}, \"{6}\");", new object[]
			{
				this.ClientID,
				this.Splitter.ClientID,
				this.GetPreviousSplitBarId(),
				this.GetNextSplitBarId(),
				base.Index,
				this.GetIndexInPanes(),
				this.IsLastPane()
			});
		}

		// Token: 0x06009C36 RID: 39990 RVA: 0x0022C5F4 File Offset: 0x0022A7F4
		internal override void RegisterInitializeScriptWithScriptManager()
		{
			string initializeScript = this.GetInitializeScript();
			ScriptManager.RegisterStartupScript(this.Page, typeof(RadPane), this.ClientID + initializeScript, initializeScript, true);
		}

		// Token: 0x06009C37 RID: 39991 RVA: 0x0022C62C File Offset: 0x0022A82C
		internal int GetDefaultMinSize()
		{
			int result;
			try
			{
				result = (this.Splitter.IsTouchSkin() ? 40 : 20);
			}
			catch
			{
				result = 20;
			}
			return result;
		}

		// Token: 0x06009C38 RID: 39992 RVA: 0x0022C668 File Offset: 0x0022A868
		public Unit GetExpandedSize()
		{
			return (Unit)(this.ViewState["ExpandedSize"] ?? Unit.Empty);
		}

		// Token: 0x06009C39 RID: 39993 RVA: 0x0022C68D File Offset: 0x0022A88D
		public void SetExpandedSize(Unit value)
		{
			this.ViewState["ExpandedSize"] = value;
		}

		// Token: 0x06009C3A RID: 39994 RVA: 0x0022C6A8 File Offset: 0x0022A8A8
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			try
			{
				this.Width = Unit.Parse(clientState["width"].ToString());
				this.Height = Unit.Parse(clientState["height"].ToString());
				this.Locked = (bool)clientState["locked"];
				this.ContentUrl = clientState["contentUrl"].ToString();
				this.Collapsed = (bool)clientState["collapsed"];
				this.CollapsedDirection = (int)clientState["_collapsedDirection"];
				this.SetExpandedSize(Unit.Parse(clientState["_expandedSize"].ToString()));
				this.OriginalWidth = Unit.Parse(clientState["_originalWidth"].ToString());
				this.OriginalHeight = Unit.Parse(clientState["_originalHeight"].ToString());
			}
			catch
			{
			}
		}

		// Token: 0x06009C3B RID: 39995 RVA: 0x0022C7B0 File Offset: 0x0022A9B0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			if (this.OriginalWidth == this.undefinedSize)
			{
				this.OriginalWidth = this.Width;
			}
			if (this.OriginalHeight == this.undefinedSize)
			{
				this.OriginalHeight = this.Height;
			}
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_originalWidth", this.OriginalWidth.ToString());
			descriptor.AddProperty("_originalHeight", this.OriginalHeight.ToString());
			descriptor.AddProperty("_collapsedDirection", this.CollapsedDirection);
			descriptor.AddProperty("_expandedSize", this.GetExpandedSize().Value);
			if (!string.IsNullOrEmpty(this.ChildSplitterID))
			{
				descriptor.AddProperty("_childSplitterId", this.ChildSplitterID);
			}
			if (!string.IsNullOrEmpty(this.ChildSlidingZoneID))
			{
				descriptor.AddProperty("_childSlidingZoneId", this.ChildSlidingZoneID);
			}
		}

		// Token: 0x06009C3C RID: 39996 RVA: 0x0022C8B0 File Offset: 0x0022AAB0
		private bool IsPercentageUnit(Unit size)
		{
			return !size.IsEmpty && size.Type == UnitType.Percentage;
		}

		// Token: 0x06009C3D RID: 39997 RVA: 0x0022C8C7 File Offset: 0x0022AAC7
		private bool IsUndefinedSize(Unit size)
		{
			return size == this.undefinedSize;
		}

		// Token: 0x06009C3E RID: 39998 RVA: 0x0022C8D8 File Offset: 0x0022AAD8
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "_collapsed", this.Collapsed, false);
			base.DescribeProperty<string>(descriptor, "contentUrl", base.ResolveClientUrl(this.ContentUrl), "");
			base.DescribeProperty<string>(descriptor, "_height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<bool>(descriptor, "locked", this.Locked, false);
			base.DescribeProperty<bool>(descriptor, "showContentDuringLoad", this.ShowContentDuringLoad, true);
			base.DescribeProperty<string>(descriptor, "_width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009C3F RID: 39999 RVA: 0x0022C98A File Offset: 0x0022AB8A
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002C0E RID: 11278
		internal string ChildSplitterID = string.Empty;

		// Token: 0x04002C0F RID: 11279
		internal string ChildSlidingZoneID = string.Empty;

		// Token: 0x04002C10 RID: 11280
		private readonly Unit undefinedSize = Unit.Pixel(-1);
	}
}
