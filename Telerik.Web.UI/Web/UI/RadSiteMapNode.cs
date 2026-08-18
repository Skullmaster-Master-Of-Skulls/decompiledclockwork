using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001AB5 RID: 6837
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[XmlRoot("Node")]
	public class RadSiteMapNode : NavigationItem, IRadSiteMapNodeContainer
	{
		// Token: 0x06010845 RID: 67653 RVA: 0x003B0588 File Offset: 0x003AE788
		public RadSiteMapNode()
		{
		}

		// Token: 0x06010846 RID: 67654 RVA: 0x003B0590 File Offset: 0x003AE790
		public RadSiteMapNode(string text, string navigateUrl)
		{
			this.Text = text;
			this.NavigateUrl = navigateUrl;
		}

		// Token: 0x17005042 RID: 20546
		// (get) Token: 0x06010847 RID: 67655 RVA: 0x003B05A6 File Offset: 0x003AE7A6
		// (set) Token: 0x06010848 RID: 67656 RVA: 0x003B05AE File Offset: 0x003AE7AE
		[Localizable(true)]
		[ClientPersistedProperty]
		[DefaultValue("")]
		[Description("The text of the node")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17005043 RID: 20547
		// (get) Token: 0x06010849 RID: 67657 RVA: 0x003B05B7 File Offset: 0x003AE7B7
		// (set) Token: 0x0601084A RID: 67658 RVA: 0x003B05BF File Offset: 0x003AE7BF
		[Localizable(true)]
		[ClientPersistedProperty]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		public override string NavigateUrl
		{
			get
			{
				return base.NavigateUrl;
			}
			set
			{
				base.NavigateUrl = value;
			}
		}

		// Token: 0x17005044 RID: 20548
		// (get) Token: 0x0601084B RID: 67659 RVA: 0x003B05C8 File Offset: 0x003AE7C8
		// (set) Token: 0x0601084C RID: 67660 RVA: 0x003B05D0 File Offset: 0x003AE7D0
		[DefaultValue("")]
		[Description("The target window or frame")]
		[ClientPersistedProperty]
		[TypeConverter(typeof(TargetConverter))]
		public override string Target
		{
			get
			{
				return base.Target;
			}
			set
			{
				base.Target = value;
			}
		}

		// Token: 0x17005045 RID: 20549
		// (get) Token: 0x0601084D RID: 67661 RVA: 0x003B05D9 File Offset: 0x003AE7D9
		// (set) Token: 0x0601084E RID: 67662 RVA: 0x003B05E1 File Offset: 0x003AE7E1
		[ClientPersistedProperty]
		[DefaultValue("")]
		[Description("Custom data associated with the node")]
		[Localizable(true)]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x17005046 RID: 20550
		// (get) Token: 0x0601084F RID: 67663 RVA: 0x003B05EA File Offset: 0x003AE7EA
		// (set) Token: 0x06010850 RID: 67664 RVA: 0x003B060A File Offset: 0x003AE80A
		[ClientPersistedProperty]
		public override string ToolTip
		{
			get
			{
				return (string)(this.ViewState["ToolTip"] ?? "");
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17005047 RID: 20551
		// (get) Token: 0x06010851 RID: 67665 RVA: 0x003B061D File Offset: 0x003AE81D
		// (set) Token: 0x06010852 RID: 67666 RVA: 0x003B063E File Offset: 0x003AE83E
		[DefaultValue(true)]
		[ClientPersistedProperty]
		public override bool Enabled
		{
			get
			{
				return (bool)(this.ViewState["Enabled"] ?? true);
			}
			set
			{
				base.Enabled = value;
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17005048 RID: 20552
		// (get) Token: 0x06010853 RID: 67667 RVA: 0x003B065D File Offset: 0x003AE85D
		// (set) Token: 0x06010854 RID: 67668 RVA: 0x003B0665 File Offset: 0x003AE865
		[Browsable(false)]
		public override object DataItem
		{
			get
			{
				return base.DataItem;
			}
			set
			{
				base.DataItem = value;
			}
		}

		// Token: 0x17005049 RID: 20553
		// (get) Token: 0x06010855 RID: 67669 RVA: 0x003B066E File Offset: 0x003AE86E
		// (set) Token: 0x06010856 RID: 67670 RVA: 0x003B0676 File Offset: 0x003AE876
		[ClientPersistedProperty]
		[DefaultValue("")]
		[Description("The CSS class of the node")]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x1700504A RID: 20554
		// (get) Token: 0x06010857 RID: 67671 RVA: 0x003B067F File Offset: 0x003AE87F
		// (set) Token: 0x06010858 RID: 67672 RVA: 0x003B069F File Offset: 0x003AE89F
		[DefaultValue("")]
		[Description("Applied when the node is hovered")]
		[Category("Appearance")]
		[ClientPersistedProperty]
		public string HoveredCssClass
		{
			get
			{
				return (string)(this.ViewState["HoveredCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x1700504B RID: 20555
		// (get) Token: 0x06010859 RID: 67673 RVA: 0x003B06B2 File Offset: 0x003AE8B2
		// (set) Token: 0x0601085A RID: 67674 RVA: 0x003B06D2 File Offset: 0x003AE8D2
		[Description("Applied when the node is disabled")]
		[Category("Appearance")]
		[DefaultValue("")]
		[ClientPersistedProperty]
		public new string DisabledCssClass
		{
			get
			{
				return (string)(this.ViewState["DisabledCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x1700504C RID: 20556
		// (get) Token: 0x0601085B RID: 67675 RVA: 0x003B06E5 File Offset: 0x003AE8E5
		// (set) Token: 0x0601085C RID: 67676 RVA: 0x003B0705 File Offset: 0x003AE905
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Applied when the node is selected")]
		[ClientPersistedProperty]
		public string SelectedCssClass
		{
			get
			{
				return (string)(this.ViewState["SelectedCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SelectedCssClass"] = value;
			}
		}

		// Token: 0x1700504D RID: 20557
		// (get) Token: 0x0601085D RID: 67677 RVA: 0x003B0718 File Offset: 0x003AE918
		// (set) Token: 0x0601085E RID: 67678 RVA: 0x003B0738 File Offset: 0x003AE938
		[Category("Appearance")]
		[Description("The URL of the image displayed for the node.")]
		[ClientPersistedProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		[Localizable(true)]
		public override string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x1700504E RID: 20558
		// (get) Token: 0x0601085F RID: 67679 RVA: 0x003B074B File Offset: 0x003AE94B
		// (set) Token: 0x06010860 RID: 67680 RVA: 0x003B076B File Offset: 0x003AE96B
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		[ClientPersistedProperty]
		[Category("Appearance")]
		[Description("The image used when the node is hovered.")]
		public override string HoveredImageUrl
		{
			get
			{
				return (string)(this.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x1700504F RID: 20559
		// (get) Token: 0x06010861 RID: 67681 RVA: 0x003B077E File Offset: 0x003AE97E
		// (set) Token: 0x06010862 RID: 67682 RVA: 0x003B079E File Offset: 0x003AE99E
		[Description("The image used when the node is hovered.")]
		[DefaultValue("")]
		[UrlProperty]
		[ClientPersistedProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x17005050 RID: 20560
		// (get) Token: 0x06010863 RID: 67683 RVA: 0x003B07B1 File Offset: 0x003AE9B1
		// (set) Token: 0x06010864 RID: 67684 RVA: 0x003B07D1 File Offset: 0x003AE9D1
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPersistedProperty]
		[DefaultValue("")]
		[UrlProperty]
		[Category("Appearance")]
		[Description("The image used when the node is selected.")]
		public string SelectedImageUrl
		{
			get
			{
				return (string)(this.ViewState["SelectedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x17005051 RID: 20561
		// (get) Token: 0x06010865 RID: 67685 RVA: 0x003B07E4 File Offset: 0x003AE9E4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Level
		{
			get
			{
				if (this.Owner is RadSiteMap)
				{
					return 0;
				}
				return ((RadSiteMapNode)this.Owner).Level + 1;
			}
		}

		// Token: 0x17005052 RID: 20562
		// (get) Token: 0x06010866 RID: 67686 RVA: 0x003B0807 File Offset: 0x003AEA07
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public RadSiteMapNodeCollection Nodes
		{
			get
			{
				return (RadSiteMapNodeCollection)base.Children;
			}
		}

		// Token: 0x17005053 RID: 20563
		// (get) Token: 0x06010867 RID: 67687 RVA: 0x003B0814 File Offset: 0x003AEA14
		// (set) Token: 0x06010868 RID: 67688 RVA: 0x003B081C File Offset: 0x003AEA1C
		[Browsable(false)]
		public IRadSiteMapNodeContainer Owner { get; internal set; }

		// Token: 0x17005054 RID: 20564
		// (get) Token: 0x06010869 RID: 67689 RVA: 0x003B0825 File Offset: 0x003AEA25
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadSiteMap SiteMap
		{
			get
			{
				return (RadSiteMap)base.Container;
			}
		}

		// Token: 0x17005055 RID: 20565
		// (get) Token: 0x0601086A RID: 67690 RVA: 0x003B0832 File Offset: 0x003AEA32
		// (set) Token: 0x0601086B RID: 67691 RVA: 0x003B0862 File Offset: 0x003AEA62
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Whether the node is selected or not")]
		public bool Selected
		{
			get
			{
				if (base.Container == null)
				{
					return this._selected;
				}
				return (bool)(this.ViewState["Selected"] ?? false);
			}
			set
			{
				if (base.Container == null)
				{
					this._selected = value;
					return;
				}
				if (value && this.SiteMap != null)
				{
					this.SiteMap.ClearSelectedNode();
				}
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x17005056 RID: 20566
		// (get) Token: 0x0601086C RID: 67692 RVA: 0x003B08A0 File Offset: 0x003AEAA0
		// (set) Token: 0x0601086D RID: 67693 RVA: 0x003B08A8 File Offset: 0x003AEAA8
		[Bindable(false)]
		[TemplateContainer(typeof(RadSiteMapNode))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate NodeTemplate
		{
			get
			{
				return this.Template;
			}
			set
			{
				this.Template = value;
			}
		}

		// Token: 0x17005057 RID: 20567
		// (get) Token: 0x0601086E RID: 67694 RVA: 0x003B08B1 File Offset: 0x003AEAB1
		// (set) Token: 0x0601086F RID: 67695 RVA: 0x003B08B9 File Offset: 0x003AEAB9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(RadSiteMapNode))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		[Browsable(false)]
		public ITemplate SeparatorTemplate { get; set; }

		// Token: 0x17005058 RID: 20568
		// (get) Token: 0x06010870 RID: 67696 RVA: 0x003B08C2 File Offset: 0x003AEAC2
		[Browsable(false)]
		public RadSiteMapNode ParentNode
		{
			get
			{
				return this.Owner as RadSiteMapNode;
			}
		}

		// Token: 0x06010871 RID: 67697 RVA: 0x003B08CF File Offset: 0x003AEACF
		public void Remove()
		{
			if (this.Owner != null)
			{
				this.Owner.Nodes.Remove(this);
			}
		}

		// Token: 0x17005059 RID: 20569
		// (get) Token: 0x06010872 RID: 67698 RVA: 0x003B08EC File Offset: 0x003AEAEC
		protected internal override IDictionary<string, string> PropertyMappings
		{
			get
			{
				return new Dictionary<string, string>
				{
					{
						"Href",
						"NavigateUrl"
					},
					{
						"Image",
						"ImageUrl"
					}
				};
			}
		}

		// Token: 0x1700505A RID: 20570
		// (get) Token: 0x06010873 RID: 67699 RVA: 0x003B0922 File Offset: 0x003AEB22
		private SiteMapLevelSetting LevelSettings
		{
			get
			{
				if (this.SiteMap == null)
				{
					return new SiteMapLevelSetting();
				}
				return this.SiteMap.GetLevelSettings(this.Level);
			}
		}

		// Token: 0x1700505B RID: 20571
		// (get) Token: 0x06010874 RID: 67700 RVA: 0x003B0943 File Offset: 0x003AEB43
		private SiteMapLevelSetting NextLevelSettings
		{
			get
			{
				if (this.SiteMap == null)
				{
					return new SiteMapLevelSetting();
				}
				return this.SiteMap.GetLevelSettings(this.Level + 1);
			}
		}

		// Token: 0x06010875 RID: 67701 RVA: 0x003B0966 File Offset: 0x003AEB66
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadSiteMapNodeCollection(this);
		}

		// Token: 0x06010876 RID: 67702 RVA: 0x003B096E File Offset: 0x003AEB6E
		protected internal override void SetItemContainer(ControlItemContainer itemContainer)
		{
			base.SetItemContainer(itemContainer);
			if (this._selected)
			{
				this.Selected = true;
				this._selected = false;
			}
		}

		// Token: 0x06010877 RID: 67703 RVA: 0x003B0990 File Offset: 0x003AEB90
		internal override void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(properties, dataItem, dataMember, depth);
			IHierarchyData hierarchyData = dataItem as IHierarchyData;
			if (hierarchyData != null && string.Equals(hierarchyData.Path, this.SiteMap.CurrentSiteMapUrl, StringComparison.OrdinalIgnoreCase))
			{
				this.Selected = true;
			}
		}

		// Token: 0x06010878 RID: 67704 RVA: 0x003B09D2 File Offset: 0x003AEBD2
		internal void BreakRow()
		{
			this._enableBreakRow = true;
		}

		// Token: 0x1700505C RID: 20572
		// (get) Token: 0x06010879 RID: 67705 RVA: 0x003B09DC File Offset: 0x003AEBDC
		private bool IsLastChild
		{
			get
			{
				int num = (this.LevelSettings.MaximumNodes > 0) ? Math.Min(this.LevelSettings.MaximumNodes, this.Owner.Nodes.Count) : this.Owner.Nodes.Count;
				return this._cachedIndex == num - 1;
			}
		}

		// Token: 0x1700505D RID: 20573
		// (get) Token: 0x0601087A RID: 67706 RVA: 0x003B0A35 File Offset: 0x003AEC35
		private bool ShouldRenderSeparator
		{
			get
			{
				return this.LevelSettings.Layout == SiteMapLayout.Flow && !this.IsLastChild;
			}
		}

		// Token: 0x1700505E RID: 20574
		// (get) Token: 0x0601087B RID: 67707 RVA: 0x003B0A50 File Offset: 0x003AEC50
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x1700505F RID: 20575
		// (get) Token: 0x0601087C RID: 67708 RVA: 0x003B0A54 File Offset: 0x003AEC54
		// (set) Token: 0x0601087D RID: 67709 RVA: 0x003B0A5C File Offset: 0x003AEC5C
		internal bool SeparatorTemplateInstantiated { get; set; }

		// Token: 0x17005060 RID: 20576
		// (get) Token: 0x0601087E RID: 67710 RVA: 0x003B0A65 File Offset: 0x003AEC65
		// (set) Token: 0x0601087F RID: 67711 RVA: 0x003B0A6D File Offset: 0x003AEC6D
		internal Control SeparatorTemplateContainer { get; set; }

		// Token: 0x06010880 RID: 67712 RVA: 0x003B0A76 File Offset: 0x003AEC76
		protected override bool IsChildControl(Control control)
		{
			return base.IsChildControl(control) || control == this.SeparatorTemplateContainer;
		}

		// Token: 0x06010881 RID: 67713 RVA: 0x003B0A8C File Offset: 0x003AEC8C
		internal void Render(int index, HtmlTextWriter writer)
		{
			this._cachedIndex = index;
			this.RenderControl(writer);
		}

		// Token: 0x06010882 RID: 67714 RVA: 0x003B0A9C File Offset: 0x003AEC9C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Templated)
			{
				if (this.Controls.IsReadOnly)
				{
					base.RenderChildren(writer);
				}
				else
				{
					this.RenderTemplate(writer);
				}
			}
			else
			{
				this.RenderLink(writer);
			}
			this.RenderSeparator(writer);
			bool flag = this.NextLevelSettings.Layout == SiteMapLayout.List && this.NextLevelSettings.ListLayout.RepeatColumns > 1 && !this.NextLevelSettings.ListLayout.AlignRows;
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsmColumnWrap");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			RadSiteMap.RenderLevelNodes(writer, this.NextLevelSettings, this.Nodes, new RadSiteMap.RenderListDelegate(this.RenderChildList));
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06010883 RID: 67715 RVA: 0x003B0B54 File Offset: 0x003AED54
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			if (this.ShouldRenderSeparator)
			{
				writer.Write(" ");
			}
		}

		// Token: 0x06010884 RID: 67716 RVA: 0x003B0B70 File Offset: 0x003AED70
		private void RenderTemplate(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsmTemplate");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (!(control is RadSiteMapNode) && control != this.SeparatorTemplateContainer)
				{
					control.RenderControl(writer);
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x06010885 RID: 67717 RVA: 0x003B0BF8 File Offset: 0x003AEDF8
		private void RenderSeparator(HtmlTextWriter writer)
		{
			if (!this.ShouldRenderSeparator)
			{
				return;
			}
			if (this.SeparatorTemplateInstantiated)
			{
				this.SeparatorTemplateContainer.RenderControl(writer);
				return;
			}
			writer.Write(this.LevelSettings.SeparatorText);
		}

		// Token: 0x06010886 RID: 67718 RVA: 0x003B0C2C File Offset: 0x003AEE2C
		private void RenderLink(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsmLink");
			if (this.Enabled && this.SiteMap.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.ResolveClientUrl(this.NavigateUrl));
			}
			if (!string.IsNullOrEmpty(this.Target))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Target);
			}
			if (!string.IsNullOrEmpty(this.ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			if (this.ShouldRenderImage())
			{
				this.RenderImage(writer);
			}
			writer.Write(this.SiteMap.EnableTextHTMLEncoding ? HttpUtility.HtmlEncode(this.Text) : this.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06010887 RID: 67719 RVA: 0x003B0CE8 File Offset: 0x003AEEE8
		protected void RenderImage(HtmlTextWriter writer)
		{
			string relativeUrl = (!string.IsNullOrEmpty(this.ImageUrl)) ? this.ImageUrl : this.LevelSettings.ImageUrl;
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsmImage");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(relativeUrl));
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06010888 RID: 67720 RVA: 0x003B0D4C File Offset: 0x003AEF4C
		private bool ShouldRenderImage()
		{
			return !string.IsNullOrEmpty(this.LevelSettings.ImageUrl) || !string.IsNullOrEmpty(this.ImageUrl);
		}

		// Token: 0x06010889 RID: 67721 RVA: 0x003B0D70 File Offset: 0x003AEF70
		private void RenderChildList(HtmlTextWriter writer, IList<RadSiteMapNode> levelNodes)
		{
			if (this.LevelSettings.Layout == SiteMapLayout.Flow || levelNodes.Count == 0)
			{
				return;
			}
			if (!this.HasVisibleItems(levelNodes))
			{
				return;
			}
			Unit childListWidth = RadSiteMap.GetChildListWidth(this.NextLevelSettings);
			if (childListWidth != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, childListWidth.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadSiteMap.GetChildListClass(this.Level + 1, this.NextLevelSettings, this.SiteMap.ShowNodeLines));
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			for (int i = 0; i < levelNodes.Count; i++)
			{
				levelNodes[i].Render(i, writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0601088A RID: 67722 RVA: 0x003B0E20 File Offset: 0x003AF020
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetNodeClass());
			int repeatColumns = this.LevelSettings.ListLayout.RepeatColumns;
			double num = 6.0;
			if (this.LevelSettings.Layout == SiteMapLayout.List && repeatColumns > 1 && this.LevelSettings.ListLayout.AlignRows)
			{
				double num2 = Math.Floor(100.0 / (double)repeatColumns);
				if (this._enableBreakRow)
				{
					num2 += num2 * (num / 100.0);
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, num2.ToString("F0") + "%");
			}
		}

		// Token: 0x0601088B RID: 67723 RVA: 0x003B0EC4 File Offset: 0x003AF0C4
		private string GetNodeClass()
		{
			List<string> list = new List<string>(3);
			list.Add("rsmItem");
			if (!this.Enabled || !this.SiteMap.Enabled)
			{
				string item = string.Format("{0}{1}", "rsmDisabled", string.IsNullOrEmpty(this.DisabledCssClass) ? "" : (" " + this.DisabledCssClass));
				list.Add(item);
			}
			if (!string.IsNullOrEmpty(this.CssClass))
			{
				list.Add(this.CssClass);
			}
			if (this.IsLastChild && this.SiteMap.ShowNodeLines && this.LevelSettings.Layout == SiteMapLayout.List && this.LevelSettings.ListLayout.RepeatColumns == 1)
			{
				list.Add("rsmLast");
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x0601088C RID: 67724 RVA: 0x003B0F9C File Offset: 0x003AF19C
		private bool HasVisibleItems(IList<RadSiteMapNode> levelNodes)
		{
			foreach (RadSiteMapNode radSiteMapNode in levelNodes)
			{
				if (radSiteMapNode.Visible)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040049F7 RID: 18935
		private bool _selected;

		// Token: 0x040049F8 RID: 18936
		private bool _enableBreakRow;

		// Token: 0x040049F9 RID: 18937
		private int _cachedIndex;
	}
}
