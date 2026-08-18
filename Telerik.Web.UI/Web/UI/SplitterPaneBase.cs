using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FBD RID: 4029
	[ClientScriptResource("Telerik.Web.UI.SplitterPaneBase", "Telerik.Web.UI.Splitter.RadSplitterScripts.js")]
	public abstract class SplitterPaneBase : SplitterItem
	{
		// Token: 0x17003160 RID: 12640
		// (get) Token: 0x06009BE0 RID: 39904 RVA: 0x0022B3FE File Offset: 0x002295FE
		// (set) Token: 0x06009BE1 RID: 39905 RVA: 0x0022B420 File Offset: 0x00229620
		[Category("Appearance")]
		[DefaultValue(20)]
		[ClientControlProperty]
		[Description("Sets/gets the min width to which the pane can be resized")]
		public virtual int MinWidth
		{
			get
			{
				return (int)(this.ViewState["MinWidth"] ?? 20);
			}
			set
			{
				this.ViewState["MinWidth"] = value;
			}
		}

		// Token: 0x17003161 RID: 12641
		// (get) Token: 0x06009BE2 RID: 39906 RVA: 0x0022B438 File Offset: 0x00229638
		// (set) Token: 0x06009BE3 RID: 39907 RVA: 0x0022B45D File Offset: 0x0022965D
		[DefaultValue(10000)]
		[ClientControlProperty]
		[Description("Sets/gets the max width to which the pane can be resized")]
		[Category("Appearance")]
		public int MaxWidth
		{
			get
			{
				return (int)(this.ViewState["MaxWidth"] ?? 10000);
			}
			set
			{
				this.ViewState["MaxWidth"] = value;
			}
		}

		// Token: 0x17003162 RID: 12642
		// (get) Token: 0x06009BE4 RID: 39908 RVA: 0x0022B475 File Offset: 0x00229675
		// (set) Token: 0x06009BE5 RID: 39909 RVA: 0x0022B497 File Offset: 0x00229697
		[Description("Sets/gets the min height to which the pane can be resized")]
		[DefaultValue(20)]
		[ClientControlProperty]
		[Category("Appearance")]
		public virtual int MinHeight
		{
			get
			{
				return (int)(this.ViewState["MinHeight"] ?? 20);
			}
			set
			{
				this.ViewState["MinHeight"] = value;
			}
		}

		// Token: 0x17003163 RID: 12643
		// (get) Token: 0x06009BE6 RID: 39910 RVA: 0x0022B4AF File Offset: 0x002296AF
		// (set) Token: 0x06009BE7 RID: 39911 RVA: 0x0022B4D4 File Offset: 0x002296D4
		[DefaultValue(10000)]
		[ClientControlProperty]
		[Description("Sets/gets the max height to which the pane can be resized")]
		[Category("Appearance")]
		public int MaxHeight
		{
			get
			{
				return (int)(this.ViewState["MaxHeight"] ?? 10000);
			}
			set
			{
				this.ViewState["MaxHeight"] = value;
			}
		}

		// Token: 0x17003164 RID: 12644
		// (get) Token: 0x06009BE8 RID: 39912 RVA: 0x0022B4EC File Offset: 0x002296EC
		// (set) Token: 0x06009BE9 RID: 39913 RVA: 0x0022B50D File Offset: 0x0022970D
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Sets/gets whether the content of the pane will get a scrollbars when it exceeds the pane area size")]
		[DefaultValue(SplitterPaneScrolling.Both)]
		public SplitterPaneScrolling Scrolling
		{
			get
			{
				return (SplitterPaneScrolling)(this.ViewState["Scrolling"] ?? SplitterPaneScrolling.Both);
			}
			set
			{
				this.ViewState["Scrolling"] = value;
			}
		}

		// Token: 0x17003165 RID: 12645
		// (get) Token: 0x06009BEA RID: 39914 RVA: 0x0022B525 File Offset: 0x00229725
		// (set) Token: 0x06009BEB RID: 39915 RVA: 0x0022B545 File Offset: 0x00229745
		[DefaultValue("")]
		[Description("The name of the javascript function called when the pane is collapsed.")]
		[ClientControlEvent]
		[ClientPropertyName("collapsed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientCollapsed
		{
			get
			{
				return ((string)this.ViewState["OnClientCollapsed"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCollapsed"] = value;
			}
		}

		// Token: 0x17003166 RID: 12646
		// (get) Token: 0x06009BEC RID: 39916 RVA: 0x0022B558 File Offset: 0x00229758
		// (set) Token: 0x06009BED RID: 39917 RVA: 0x0022B560 File Offset: 0x00229760
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientCollapsing property instead.", false)]
		public string OnClientBeforeCollapse
		{
			get
			{
				return this.OnClientCollapsing;
			}
			set
			{
				this.OnClientCollapsing = value;
			}
		}

		// Token: 0x17003167 RID: 12647
		// (get) Token: 0x06009BEE RID: 39918 RVA: 0x0022B569 File Offset: 0x00229769
		// (set) Token: 0x06009BEF RID: 39919 RVA: 0x0022B589 File Offset: 0x00229789
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("collapsing")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called before the pane is collapsed.")]
		public string OnClientCollapsing
		{
			get
			{
				return ((string)this.ViewState["OnClientCollapsing"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCollapsing"] = value;
			}
		}

		// Token: 0x17003168 RID: 12648
		// (get) Token: 0x06009BF0 RID: 39920 RVA: 0x0022B59C File Offset: 0x0022979C
		// (set) Token: 0x06009BF1 RID: 39921 RVA: 0x0022B5CB File Offset: 0x002297CB
		[ClientPropertyName("expanded")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Description("The name of the javascript function called when the pane is expanded.")]
		public string OnClientExpanded
		{
			get
			{
				if (this.ViewState["OnClientExpanded"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientExpanded"];
			}
			set
			{
				this.ViewState["OnClientExpanded"] = value;
			}
		}

		// Token: 0x17003169 RID: 12649
		// (get) Token: 0x06009BF2 RID: 39922 RVA: 0x0022B5DE File Offset: 0x002297DE
		// (set) Token: 0x06009BF3 RID: 39923 RVA: 0x0022B5E6 File Offset: 0x002297E6
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientExpanding property instead.", false)]
		public string OnClientBeforeExpand
		{
			get
			{
				return this.OnClientExpanding;
			}
			set
			{
				this.OnClientExpanding = value;
			}
		}

		// Token: 0x1700316A RID: 12650
		// (get) Token: 0x06009BF4 RID: 39924 RVA: 0x0022B5EF File Offset: 0x002297EF
		// (set) Token: 0x06009BF5 RID: 39925 RVA: 0x0022B60F File Offset: 0x0022980F
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called before the pane is expanded.")]
		[ClientControlEvent]
		[ClientPropertyName("expanding")]
		[DefaultValue("")]
		public string OnClientExpanding
		{
			get
			{
				return ((string)this.ViewState["OnClientExpanding"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientExpanding"] = value;
			}
		}

		// Token: 0x1700316B RID: 12651
		// (get) Token: 0x06009BF6 RID: 39926 RVA: 0x0022B622 File Offset: 0x00229822
		// (set) Token: 0x06009BF7 RID: 39927 RVA: 0x0022B651 File Offset: 0x00229851
		[Description("The name of the javascript function called when the pane is resized.")]
		[Category("Client-side events")]
		[ClientPropertyName("resized")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientResized
		{
			get
			{
				if (this.ViewState["OnClientResized"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientResized"];
			}
			set
			{
				this.ViewState["OnClientResized"] = value;
			}
		}

		// Token: 0x1700316C RID: 12652
		// (get) Token: 0x06009BF8 RID: 39928 RVA: 0x0022B664 File Offset: 0x00229864
		// (set) Token: 0x06009BF9 RID: 39929 RVA: 0x0022B66C File Offset: 0x0022986C
		[DefaultValue("")]
		[Obsolete("This property is now obsolete. Please use the OnClientResizing property instead.", false)]
		public string OnClientBeforeResize
		{
			get
			{
				return this.OnClientResizing;
			}
			set
			{
				this.OnClientResizing = value;
			}
		}

		// Token: 0x1700316D RID: 12653
		// (get) Token: 0x06009BFA RID: 39930 RVA: 0x0022B675 File Offset: 0x00229875
		// (set) Token: 0x06009BFB RID: 39931 RVA: 0x0022B695 File Offset: 0x00229895
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("resizing")]
		[Description("The name of the javascript function called before the pane is resized.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientResizing
		{
			get
			{
				return ((string)this.ViewState["OnClientResizing"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResizing"] = value;
			}
		}

		// Token: 0x1700316E RID: 12654
		// (get) Token: 0x06009BFC RID: 39932 RVA: 0x0022B6A8 File Offset: 0x002298A8
		// (set) Token: 0x06009BFD RID: 39933 RVA: 0x0022B6D3 File Offset: 0x002298D3
		[ClientControlProperty]
		[Description("Sets/gets whether the scrolls position will be persisted acrosss postbacks")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool PersistScrollPosition
		{
			get
			{
				return this.ViewState["PersistScrollPosition"] == null || (bool)this.ViewState["PersistScrollPosition"];
			}
			set
			{
				this.ViewState["PersistScrollPosition"] = value;
			}
		}

		// Token: 0x06009BFE RID: 39934 RVA: 0x0022B6EC File Offset: 0x002298EC
		internal string GetScrollOverflowStyle()
		{
			string result = "overflow:hidden;";
			if (this.Scrolling == SplitterPaneScrolling.Both)
			{
				result = "overflow:auto;";
			}
			else if (this.Scrolling == SplitterPaneScrolling.X)
			{
				result = "overflow-y:hidden;overflow-x:auto;";
			}
			else if (this.Scrolling == SplitterPaneScrolling.Y)
			{
				result = "overflow-y:auto;overflow-x:hidden;";
			}
			return result;
		}

		// Token: 0x06009BFF RID: 39935 RVA: 0x0022B734 File Offset: 0x00229934
		internal string GetBackColorStyle()
		{
			string result = "";
			if (!this.BackColor.IsEmpty)
			{
				WebColorConverter webColorConverter = new WebColorConverter();
				string arg = webColorConverter.ConvertToString(this.BackColor);
				result = string.Format("background-color:{0};", arg);
			}
			return result;
		}

		// Token: 0x06009C00 RID: 39936 RVA: 0x0022B77C File Offset: 0x0022997C
		internal string GetForeColorStyle()
		{
			string result = "";
			if (!this.ForeColor.IsEmpty)
			{
				WebColorConverter webColorConverter = new WebColorConverter();
				string arg = webColorConverter.ConvertToString(this.ForeColor);
				result = string.Format("color:{0};", arg);
			}
			return result;
		}

		// Token: 0x06009C01 RID: 39937 RVA: 0x0022B7C4 File Offset: 0x002299C4
		internal string GetBorderStyle(int panesBorderSize)
		{
			string arg = string.Format("border-width:{0}px;", (this.BorderWidth == Unit.Empty) ? ((double)panesBorderSize) : this.BorderWidth.Value);
			string arg2 = (this.BorderStyle == BorderStyle.NotSet) ? string.Empty : string.Format("border-style:{0};", this.BorderStyle.ToString());
			string arg3 = this.BorderColor.IsEmpty ? string.Empty : string.Format("border-color:{0};", new WebColorConverter().ConvertToString(this.BorderColor));
			return string.Format("{0}{1}{2}", arg, arg2, arg3);
		}

		// Token: 0x06009C02 RID: 39938 RVA: 0x0022B874 File Offset: 0x00229A74
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			try
			{
				this.MinWidth = (int)clientState["minWidth"];
				this.MaxWidth = (int)clientState["maxWidth"];
				this.MinHeight = (int)clientState["minHeight"];
				this.MaxHeight = (int)clientState["maxHeight"];
				this._scrollLeft = (int)clientState["_scrollLeft"];
				this._scrollTop = (int)clientState["_scrollTop"];
			}
			catch
			{
			}
		}

		// Token: 0x06009C03 RID: 39939 RVA: 0x0022B924 File Offset: 0x00229B24
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_scrollLeft", this._scrollLeft);
			descriptor.AddProperty("_scrollTop", this._scrollTop);
		}

		// Token: 0x06009C04 RID: 39940 RVA: 0x0022B95C File Offset: 0x00229B5C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "maxHeight", this.MaxHeight, 10000);
			base.DescribeProperty<int>(descriptor, "maxWidth", this.MaxWidth, 10000);
			base.DescribeProperty<int>(descriptor, "minHeight", this.MinHeight, 20);
			base.DescribeProperty<int>(descriptor, "minWidth", this.MinWidth, 20);
			base.DescribeProperty<bool>(descriptor, "persistScrollPosition", this.PersistScrollPosition, true);
			base.DescribeProperty<SplitterPaneScrolling>(descriptor, "scrolling", this.Scrolling, SplitterPaneScrolling.Both);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009C05 RID: 39941 RVA: 0x0022B9EC File Offset: 0x00229BEC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "collapsed", this.OnClientCollapsed);
			RadWebControl.DescribeEvent(descriptor, "collapsing", this.OnClientCollapsing);
			RadWebControl.DescribeEvent(descriptor, "expanded", this.OnClientExpanded);
			RadWebControl.DescribeEvent(descriptor, "expanding", this.OnClientExpanding);
			RadWebControl.DescribeEvent(descriptor, "resized", this.OnClientResized);
			RadWebControl.DescribeEvent(descriptor, "resizing", this.OnClientResizing);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002C0C RID: 11276
		private int _scrollLeft;

		// Token: 0x04002C0D RID: 11277
		private int _scrollTop;
	}
}
