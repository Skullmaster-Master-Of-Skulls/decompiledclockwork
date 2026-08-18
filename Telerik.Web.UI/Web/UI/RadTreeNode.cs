using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Web.UI.TreeView.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x0200129D RID: 4765
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	[XmlRoot("Node")]
	public class RadTreeNode : NavigationItem, IRadTreeNodeContainer, ICloneable, ITreeNodeBase
	{
		// Token: 0x1700405F RID: 16479
		// (get) Token: 0x0600C74D RID: 51021 RVA: 0x002C67D8 File Offset: 0x002C49D8
		private IList<PropertyDescriptor> ClientPersistedPropertyCache
		{
			get
			{
				if (RadTreeNode._clientPersistPropertiesCache == null)
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(RadTreeNode), new Attribute[]
					{
						new ClientPersistedPropertyAttribute()
					});
					RadTreeNode._clientPersistPropertiesCache = new List<PropertyDescriptor>();
					foreach (object obj in properties)
					{
						PropertyDescriptor item = (PropertyDescriptor)obj;
						RadTreeNode._clientPersistPropertiesCache.Add(item);
					}
				}
				return RadTreeNode._clientPersistPropertiesCache;
			}
		}

		// Token: 0x17004060 RID: 16480
		// (get) Token: 0x0600C74E RID: 51022 RVA: 0x002C686C File Offset: 0x002C4A6C
		// (set) Token: 0x0600C74F RID: 51023 RVA: 0x002C6874 File Offset: 0x002C4A74
		internal bool SkipLogging
		{
			get
			{
				return this._skipLogging;
			}
			set
			{
				this._skipLogging = value;
			}
		}

		// Token: 0x17004061 RID: 16481
		// (get) Token: 0x0600C750 RID: 51024 RVA: 0x002C687D File Offset: 0x002C4A7D
		protected virtual IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateNodeRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x0600C751 RID: 51025 RVA: 0x002C6899 File Offset: 0x002C4A99
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadTreeNodeCollection(this);
		}

		// Token: 0x17004062 RID: 16482
		// (get) Token: 0x0600C752 RID: 51026 RVA: 0x002C68A4 File Offset: 0x002C4AA4
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
					},
					{
						"ImageExpanded",
						"ExpandedImageUrl"
					}
				};
			}
		}

		// Token: 0x0600C753 RID: 51027 RVA: 0x002C68E8 File Offset: 0x002C4AE8
		protected internal override void SetItemContainer(ControlItemContainer itemContainer)
		{
			base.SetItemContainer(itemContainer);
			if (this._selected)
			{
				this.Selected = true;
				this._selected = false;
			}
			if (this.TreeView.TriStateCheckBoxes)
			{
				this.UpdateCheckedState();
			}
		}

		// Token: 0x0600C754 RID: 51028 RVA: 0x002C691C File Offset: 0x002C4B1C
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			foreach (PropertyDescriptor propertyDescriptor in this.ClientPersistedPropertyCache)
			{
				string key = propertyDescriptor.Name[0].ToString().ToLowerInvariant() + propertyDescriptor.Name.Substring(1);
				object obj;
				if (dictionary.TryGetValue(key, out obj) && obj != null)
				{
					if (propertyDescriptor.Name == "CssClass")
					{
						this.CssClass = (string)obj;
					}
					else if (propertyDescriptor.PropertyType.IsEnum)
					{
						this.ViewState[propertyDescriptor.Name] = Enum.Parse(propertyDescriptor.PropertyType, obj.ToString());
					}
					else if (propertyDescriptor.PropertyType == typeof(bool))
					{
						this.ViewState[propertyDescriptor.Name] = Convert.ToBoolean(obj);
					}
					else
					{
						if (propertyDescriptor.Name == "Text")
						{
							obj = HttpUtility.HtmlDecode(obj.ToString());
							if (this.TreeView != null && this.TreeView.EnableNodeTextHtmlEncoding)
							{
								obj = HttpUtility.HtmlDecode(obj.ToString());
							}
						}
						this.ViewState[propertyDescriptor.Name] = obj.ToString();
					}
				}
			}
			if (dictionary.ContainsKey("attributes"))
			{
				IDictionary<string, object> dictionary2 = (IDictionary<string, object>)dictionary["attributes"];
				foreach (string key2 in dictionary2.Keys)
				{
					object obj2 = dictionary2[key2];
					if (obj2 != null)
					{
						base.Attributes[key2] = obj2.ToString();
					}
				}
			}
		}

		// Token: 0x0600C755 RID: 51029 RVA: 0x002C6B24 File Offset: 0x002C4D24
		internal override void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(properties, dataItem, dataMember, depth);
			IHierarchyData hierarchyData = dataItem as IHierarchyData;
			if (hierarchyData != null && string.Equals(hierarchyData.Path, this.TreeView.CurrentSiteMapUrl, StringComparison.OrdinalIgnoreCase))
			{
				this.ExpandParentNodes();
				this.Selected = true;
				this.Expanded = true;
			}
		}

		// Token: 0x0600C756 RID: 51030 RVA: 0x002C6B73 File Offset: 0x002C4D73
		protected internal virtual IRenderer CreateNodeRenderer()
		{
			return RendererFactory.CreateNodeRenderer(this);
		}

		// Token: 0x17004063 RID: 16483
		// (get) Token: 0x0600C757 RID: 51031 RVA: 0x002C6B7B File Offset: 0x002C4D7B
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x0600C758 RID: 51032 RVA: 0x002C6B7F File Offset: 0x002C4D7F
		internal void Render(int index, HtmlTextWriter writer)
		{
			(this.Renderer as TreeNodeRenderBase).Render(index, writer);
		}

		// Token: 0x0600C759 RID: 51033 RVA: 0x002C6B93 File Offset: 0x002C4D93
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x0600C75A RID: 51034 RVA: 0x002C6BA1 File Offset: 0x002C4DA1
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600C75B RID: 51035 RVA: 0x002C6BAF File Offset: 0x002C4DAF
		internal void CallBaseRenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x17004064 RID: 16484
		// (get) Token: 0x0600C75C RID: 51036 RVA: 0x002C6BB8 File Offset: 0x002C4DB8
		internal bool ShouldRenderChildren
		{
			get
			{
				return this.HasVisibleChildren && (this.ExpandMode != TreeNodeExpandMode.ServerSideCallBack || this.Expanded);
			}
		}

		// Token: 0x17004065 RID: 16485
		// (get) Token: 0x0600C75D RID: 51037 RVA: 0x002C6BD5 File Offset: 0x002C4DD5
		internal bool HasVisibleChildren
		{
			get
			{
				return base.Children.VisibleItems.Count > 0;
			}
		}

		// Token: 0x17004066 RID: 16486
		// (get) Token: 0x0600C75E RID: 51038 RVA: 0x002C6BEC File Offset: 0x002C4DEC
		internal string CurrentImageUrl
		{
			get
			{
				if ((!this.Enabled || !this.TreeView.IsControlEnabled) && !string.IsNullOrEmpty(this.DisabledImageUrl))
				{
					return this.DisabledImageUrl;
				}
				if (this.Selected && !string.IsNullOrEmpty(this.SelectedImageUrl))
				{
					return this.SelectedImageUrl;
				}
				if (this.Expanded && this.Nodes.Count > 0 && !string.IsNullOrEmpty(this.ExpandedImageUrl))
				{
					return this.ExpandedImageUrl;
				}
				return this.ImageUrl;
			}
		}

		// Token: 0x0600C75F RID: 51039 RVA: 0x002C6C6E File Offset: 0x002C4E6E
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17004067 RID: 16487
		// (get) Token: 0x0600C760 RID: 51040 RVA: 0x002C6C76 File Offset: 0x002C4E76
		// (set) Token: 0x0600C761 RID: 51041 RVA: 0x002C6C7E File Offset: 0x002C4E7E
		[Browsable(false)]
		public IRadTreeNodeContainer Owner
		{
			get
			{
				return this._owner;
			}
			internal set
			{
				this._owner = value;
			}
		}

		// Token: 0x0600C762 RID: 51042 RVA: 0x002C6CA4 File Offset: 0x002C4EA4
		private static TreeNodeCheckState CalculateCheckedState(RadTreeNode node)
		{
			int count = node.GetChildren<RadTreeNode>((RadTreeNode child) => child.Checkable).Count;
			if (node.Nodes.Count != 0 && count != 0)
			{
				int count2 = node.GetChildren<RadTreeNode>((RadTreeNode child) => child.Checked && child.Checkable).Count;
				TreeNodeCheckState result = TreeNodeCheckState.Unchecked;
				if (count2 == count)
				{
					result = TreeNodeCheckState.Checked;
				}
				else if (count2 > 0)
				{
					result = TreeNodeCheckState.Indeterminate;
				}
				return result;
			}
			if (!node.Checked)
			{
				return TreeNodeCheckState.Unchecked;
			}
			return TreeNodeCheckState.Checked;
		}

		// Token: 0x0600C763 RID: 51043 RVA: 0x002C6D30 File Offset: 0x002C4F30
		private void UpdateCheckedState()
		{
			for (RadTreeNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				TreeNodeCheckState treeNodeCheckState = RadTreeNode.CalculateCheckedState(parentNode);
				parentNode.SetChecked(treeNodeCheckState != TreeNodeCheckState.Unchecked);
			}
		}

		// Token: 0x0600C764 RID: 51044 RVA: 0x002C6D64 File Offset: 0x002C4F64
		private void SetChecked(bool value)
		{
			this.ViewState["Checked"] = value;
		}

		// Token: 0x0600C765 RID: 51045 RVA: 0x002C6D7C File Offset: 0x002C4F7C
		public RadTreeNode()
		{
		}

		// Token: 0x0600C766 RID: 51046 RVA: 0x002C6D84 File Offset: 0x002C4F84
		public RadTreeNode(string text)
		{
			this.Text = text;
		}

		// Token: 0x0600C767 RID: 51047 RVA: 0x002C6D93 File Offset: 0x002C4F93
		public RadTreeNode(string text, string value) : this(text)
		{
			this.Value = value;
		}

		// Token: 0x0600C768 RID: 51048 RVA: 0x002C6DA3 File Offset: 0x002C4FA3
		public RadTreeNode(string text, string value, string navigateUrl) : this(text, value)
		{
			this.NavigateUrl = navigateUrl;
		}

		// Token: 0x17004068 RID: 16488
		// (get) Token: 0x0600C769 RID: 51049 RVA: 0x002C6DB4 File Offset: 0x002C4FB4
		// (set) Token: 0x0600C76A RID: 51050 RVA: 0x002C6DBC File Offset: 0x002C4FBC
		[ClientPersistedProperty]
		[Description("The CSS class of the node")]
		[DefaultValue("")]
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

		// Token: 0x17004069 RID: 16489
		// (get) Token: 0x0600C76B RID: 51051 RVA: 0x002C6DC5 File Offset: 0x002C4FC5
		// (set) Token: 0x0600C76C RID: 51052 RVA: 0x002C6DE5 File Offset: 0x002C4FE5
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

		// Token: 0x1700406A RID: 16490
		// (get) Token: 0x0600C76D RID: 51053 RVA: 0x002C6DF8 File Offset: 0x002C4FF8
		// (set) Token: 0x0600C76E RID: 51054 RVA: 0x002C6E19 File Offset: 0x002C5019
		[ClientPersistedProperty]
		[DefaultValue(true)]
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

		// Token: 0x1700406B RID: 16491
		// (get) Token: 0x0600C76F RID: 51055 RVA: 0x002C6E38 File Offset: 0x002C5038
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public RadTreeNodeCollection Nodes
		{
			get
			{
				return (RadTreeNodeCollection)base.Children;
			}
		}

		// Token: 0x1700406C RID: 16492
		// (get) Token: 0x0600C770 RID: 51056 RVA: 0x002C6E45 File Offset: 0x002C5045
		// (set) Token: 0x0600C771 RID: 51057 RVA: 0x002C6E4D File Offset: 0x002C504D
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

		// Token: 0x1700406D RID: 16493
		// (get) Token: 0x0600C772 RID: 51058 RVA: 0x002C6E56 File Offset: 0x002C5056
		// (set) Token: 0x0600C773 RID: 51059 RVA: 0x002C6E5E File Offset: 0x002C505E
		[ClientPersistedProperty]
		[Description("The text of the node")]
		[Localizable(true)]
		[DefaultValue("")]
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

		// Token: 0x1700406E RID: 16494
		// (get) Token: 0x0600C774 RID: 51060 RVA: 0x002C6E67 File Offset: 0x002C5067
		// (set) Token: 0x0600C775 RID: 51061 RVA: 0x002C6E6F File Offset: 0x002C506F
		[Description("Custom data associated with the node")]
		[Localizable(true)]
		[DefaultValue("")]
		[ClientPersistedProperty]
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

		// Token: 0x1700406F RID: 16495
		// (get) Token: 0x0600C776 RID: 51062 RVA: 0x002C6E78 File Offset: 0x002C5078
		// (set) Token: 0x0600C777 RID: 51063 RVA: 0x002C6E80 File Offset: 0x002C5080
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPersistedProperty]
		[Localizable(true)]
		[UrlProperty]
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

		// Token: 0x17004070 RID: 16496
		// (get) Token: 0x0600C778 RID: 51064 RVA: 0x002C6E89 File Offset: 0x002C5089
		// (set) Token: 0x0600C779 RID: 51065 RVA: 0x002C6E91 File Offset: 0x002C5091
		[TypeConverter(typeof(TargetConverter))]
		[Description("The target window or frame")]
		[ClientPersistedProperty]
		[DefaultValue("")]
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

		// Token: 0x17004071 RID: 16497
		// (get) Token: 0x0600C77A RID: 51066 RVA: 0x002C6E9A File Offset: 0x002C509A
		// (set) Token: 0x0600C77B RID: 51067 RVA: 0x002C6EBA File Offset: 0x002C50BA
		[UrlProperty]
		[Localizable(true)]
		[Description("The URL of the image displayed for the tab.")]
		[ClientPersistedProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17004072 RID: 16498
		// (get) Token: 0x0600C77C RID: 51068 RVA: 0x002C6ECD File Offset: 0x002C50CD
		// (set) Token: 0x0600C77D RID: 51069 RVA: 0x002C6EED File Offset: 0x002C50ED
		[DefaultValue("")]
		[Description("Specify custom data associated with the node")]
		[ClientPersistedProperty]
		public string Category
		{
			get
			{
				return (string)(this.ViewState["Category"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Category"] = value;
			}
		}

		// Token: 0x17004073 RID: 16499
		// (get) Token: 0x0600C77E RID: 51070 RVA: 0x002C6F00 File Offset: 0x002C5100
		// (set) Token: 0x0600C77F RID: 51071 RVA: 0x002C6F20 File Offset: 0x002C5120
		[Description("Applied when the node is hovered")]
		[ClientPersistedProperty]
		[DefaultValue("")]
		[Category("Appearance")]
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

		// Token: 0x17004074 RID: 16500
		// (get) Token: 0x0600C780 RID: 51072 RVA: 0x002C6F33 File Offset: 0x002C5133
		// (set) Token: 0x0600C781 RID: 51073 RVA: 0x002C6F53 File Offset: 0x002C5153
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Applied when the node is disabled")]
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

		// Token: 0x17004075 RID: 16501
		// (get) Token: 0x0600C782 RID: 51074 RVA: 0x002C6F66 File Offset: 0x002C5166
		// (set) Token: 0x0600C783 RID: 51075 RVA: 0x002C6F86 File Offset: 0x002C5186
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Applied to the content wrapper of the node")]
		[ClientPersistedProperty]
		public string ContentCssClass
		{
			get
			{
				return (string)(this.ViewState["ContentCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ContentCssClass"] = value;
			}
		}

		// Token: 0x17004076 RID: 16502
		// (get) Token: 0x0600C784 RID: 51076 RVA: 0x002C6F99 File Offset: 0x002C5199
		// (set) Token: 0x0600C785 RID: 51077 RVA: 0x002C6FB9 File Offset: 0x002C51B9
		[ClientPersistedProperty]
		[Description("Applied when the node is selected")]
		[Category("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17004077 RID: 16503
		// (get) Token: 0x0600C786 RID: 51078 RVA: 0x002C6FCC File Offset: 0x002C51CC
		// (set) Token: 0x0600C787 RID: 51079 RVA: 0x002C6FEC File Offset: 0x002C51EC
		[Description("The image used when the node is expanded.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[ClientPersistedProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		public string ExpandedImageUrl
		{
			get
			{
				return (string)(this.ViewState["ExpandedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ExpandedImageUrl"] = value;
			}
		}

		// Token: 0x17004078 RID: 16504
		// (get) Token: 0x0600C788 RID: 51080 RVA: 0x002C6FFF File Offset: 0x002C51FF
		// (set) Token: 0x0600C789 RID: 51081 RVA: 0x002C701F File Offset: 0x002C521F
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPersistedProperty]
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

		// Token: 0x17004079 RID: 16505
		// (get) Token: 0x0600C78A RID: 51082 RVA: 0x002C7032 File Offset: 0x002C5232
		// (set) Token: 0x0600C78B RID: 51083 RVA: 0x002C7052 File Offset: 0x002C5252
		[UrlProperty]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x1700407A RID: 16506
		// (get) Token: 0x0600C78C RID: 51084 RVA: 0x002C7065 File Offset: 0x002C5265
		// (set) Token: 0x0600C78D RID: 51085 RVA: 0x002C7085 File Offset: 0x002C5285
		[Description("The image used when the node is hovered.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Appearance")]
		[UrlProperty]
		[ClientPersistedProperty]
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

		// Token: 0x1700407B RID: 16507
		// (get) Token: 0x0600C78E RID: 51086 RVA: 0x002C7098 File Offset: 0x002C5298
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Level
		{
			get
			{
				if (this.Owner is RadTreeView)
				{
					return 0;
				}
				return ((RadTreeNode)this.Owner).Level + 1;
			}
		}

		// Token: 0x1700407C RID: 16508
		// (get) Token: 0x0600C78F RID: 51087 RVA: 0x002C70BB File Offset: 0x002C52BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadTreeView TreeView
		{
			get
			{
				return (RadTreeView)base.Container;
			}
		}

		// Token: 0x1700407D RID: 16509
		// (get) Token: 0x0600C790 RID: 51088 RVA: 0x002C70C8 File Offset: 0x002C52C8
		// (set) Token: 0x0600C791 RID: 51089 RVA: 0x002C70E9 File Offset: 0x002C52E9
		[ClientPersistedProperty]
		[DefaultValue(true)]
		[Description("Whether the node should postback")]
		public bool PostBack
		{
			get
			{
				return (bool)(this.ViewState["PostBack"] ?? true);
			}
			set
			{
				this.ViewState["PostBack"] = value;
			}
		}

		// Token: 0x1700407E RID: 16510
		// (get) Token: 0x0600C792 RID: 51090 RVA: 0x002C7101 File Offset: 0x002C5301
		// (set) Token: 0x0600C793 RID: 51091 RVA: 0x002C7122 File Offset: 0x002C5322
		[Category("Behavior")]
		[Description("The image used when the node is expanded")]
		[DefaultValue(false)]
		public bool Expanded
		{
			get
			{
				return (bool)(this.ViewState["Expanded"] ?? false);
			}
			set
			{
				this.ViewState["Expanded"] = value;
			}
		}

		// Token: 0x1700407F RID: 16511
		// (get) Token: 0x0600C794 RID: 51092 RVA: 0x002C713A File Offset: 0x002C533A
		// (set) Token: 0x0600C795 RID: 51093 RVA: 0x002C715C File Offset: 0x002C535C
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Whether the node is checked or not")]
		public bool Checked
		{
			get
			{
				return (bool)(this.ViewState["Checked"] ?? false);
			}
			set
			{
				this.SetChecked(value);
				if (this.TreeView != null && this.TreeView.TriStateCheckBoxes)
				{
					if (this.TreeView.CheckChildNodes)
					{
						foreach (object obj in this.Nodes)
						{
							RadTreeNode radTreeNode = (RadTreeNode)obj;
							radTreeNode.Checked = value;
						}
					}
					this.UpdateCheckedState();
				}
			}
		}

		// Token: 0x17004080 RID: 16512
		// (get) Token: 0x0600C796 RID: 51094 RVA: 0x002C71E4 File Offset: 0x002C53E4
		[Browsable(false)]
		public TreeNodeCheckState CheckState
		{
			get
			{
				return RadTreeNode.CalculateCheckedState(this);
			}
		}

		// Token: 0x17004081 RID: 16513
		// (get) Token: 0x0600C797 RID: 51095 RVA: 0x002C71EC File Offset: 0x002C53EC
		// (set) Token: 0x0600C798 RID: 51096 RVA: 0x002C720D File Offset: 0x002C540D
		[DefaultValue(true)]
		[Description("Whether the node is checkable or not")]
		[Category("Behavior")]
		[ClientPersistedProperty]
		public bool Checkable
		{
			get
			{
				return (bool)(this.ViewState["Checkable"] ?? true);
			}
			set
			{
				this.ViewState["Checkable"] = value;
			}
		}

		// Token: 0x17004082 RID: 16514
		// (get) Token: 0x0600C799 RID: 51097 RVA: 0x002C7225 File Offset: 0x002C5425
		// (set) Token: 0x0600C79A RID: 51098 RVA: 0x002C7258 File Offset: 0x002C5458
		[Description("Whether the node is selected or not")]
		[DefaultValue(false)]
		[Category("Behavior")]
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
				if (value && this.TreeView != null && !this.TreeView.MultipleSelect)
				{
					this.TreeView.UnselectAllNodes();
				}
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x17004083 RID: 16515
		// (get) Token: 0x0600C79B RID: 51099 RVA: 0x002C72AE File Offset: 0x002C54AE
		// (set) Token: 0x0600C79C RID: 51100 RVA: 0x002C72CF File Offset: 0x002C54CF
		[ClientPersistedProperty]
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Enable/disable dragging this node")]
		public bool AllowDrag
		{
			get
			{
				return (bool)(this.ViewState["AllowDrag"] ?? true);
			}
			set
			{
				this.ViewState["AllowDrag"] = value;
			}
		}

		// Token: 0x17004084 RID: 16516
		// (get) Token: 0x0600C79D RID: 51101 RVA: 0x002C72E7 File Offset: 0x002C54E7
		// (set) Token: 0x0600C79E RID: 51102 RVA: 0x002C7308 File Offset: 0x002C5508
		[Description("Enable/disable dropping nodes over this node")]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientPersistedProperty]
		public bool AllowDrop
		{
			get
			{
				return (bool)(this.ViewState["AllowDrop"] ?? true);
			}
			set
			{
				this.ViewState["AllowDrop"] = value;
			}
		}

		// Token: 0x17004085 RID: 16517
		// (get) Token: 0x0600C79F RID: 51103 RVA: 0x002C7320 File Offset: 0x002C5520
		// (set) Token: 0x0600C7A0 RID: 51104 RVA: 0x002C7341 File Offset: 0x002C5541
		[Description("Enable/disable node editing")]
		[ClientPersistedProperty]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool AllowEdit
		{
			get
			{
				return (bool)(this.ViewState["AllowEdit"] ?? true);
			}
			set
			{
				this.ViewState["AllowEdit"] = value;
			}
		}

		// Token: 0x17004086 RID: 16518
		// (get) Token: 0x0600C7A1 RID: 51105 RVA: 0x002C7359 File Offset: 0x002C5559
		// (set) Token: 0x0600C7A2 RID: 51106 RVA: 0x002C737A File Offset: 0x002C557A
		[ClientPersistedProperty]
		[DefaultValue(TreeNodeExpandMode.ClientSide)]
		[Description("The expand behavior of the node")]
		[Category("Behavior")]
		public TreeNodeExpandMode ExpandMode
		{
			get
			{
				return (TreeNodeExpandMode)(this.ViewState["ExpandMode"] ?? TreeNodeExpandMode.ClientSide);
			}
			set
			{
				this.ViewState["ExpandMode"] = value;
			}
		}

		// Token: 0x17004087 RID: 16519
		// (get) Token: 0x0600C7A3 RID: 51107 RVA: 0x002C7392 File Offset: 0x002C5592
		// (set) Token: 0x0600C7A4 RID: 51108 RVA: 0x002C73B2 File Offset: 0x002C55B2
		[DefaultValue("")]
		public string LongDesc
		{
			get
			{
				return (string)(this.ViewState["LongDesc"] ?? string.Empty);
			}
			set
			{
				this.ViewState["LongDesc"] = value;
			}
		}

		// Token: 0x17004088 RID: 16520
		// (get) Token: 0x0600C7A5 RID: 51109 RVA: 0x002C73C5 File Offset: 0x002C55C5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadTreeNode Prev
		{
			get
			{
				if (this.Owner == null)
				{
					throw new Exception("In order to use Prev/Next you need to add the node to a valid node hierrarchy bound to a RadTreeView");
				}
				if (base.Index > 0)
				{
					return this.Owner.Nodes[base.Index - 1];
				}
				return null;
			}
		}

		// Token: 0x17004089 RID: 16521
		// (get) Token: 0x0600C7A6 RID: 51110 RVA: 0x002C7400 File Offset: 0x002C5600
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadTreeNode Next
		{
			get
			{
				if (this.Owner == null)
				{
					throw new Exception("In order to use Prev/Next you need to add the node to a valid node hierrarchy bound to a RadTreeView");
				}
				if (base.Index < this.Owner.Nodes.Count - 1)
				{
					return this.Owner.Nodes[base.Index + 1];
				}
				return null;
			}
		}

		// Token: 0x1700408A RID: 16522
		// (get) Token: 0x0600C7A7 RID: 51111 RVA: 0x002C7454 File Offset: 0x002C5654
		// (set) Token: 0x0600C7A8 RID: 51112 RVA: 0x002C745C File Offset: 0x002C565C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		[TemplateContainer(typeof(RadTreeNode))]
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

		// Token: 0x1700408B RID: 16523
		// (get) Token: 0x0600C7A9 RID: 51113 RVA: 0x002C7465 File Offset: 0x002C5665
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FullPath
		{
			get
			{
				return this.GetFullPath("/");
			}
		}

		// Token: 0x1700408C RID: 16524
		// (get) Token: 0x0600C7AA RID: 51114 RVA: 0x002C7472 File Offset: 0x002C5672
		[Browsable(false)]
		public RadTreeNode ParentNode
		{
			get
			{
				return this.Owner as RadTreeNode;
			}
		}

		// Token: 0x1700408D RID: 16525
		// (get) Token: 0x0600C7AB RID: 51115 RVA: 0x002C747F File Offset: 0x002C567F
		// (set) Token: 0x0600C7AC RID: 51116 RVA: 0x002C749F File Offset: 0x002C569F
		[Description("Specifies the ID of the RadTreeViewContextMenu that will be displayed when node right-clicked/")]
		[TypeConverter(typeof(TreeNodeContextMenuIDConverter))]
		[ClientPersistedProperty]
		[DefaultValue("")]
		public string ContextMenuID
		{
			get
			{
				return ((string)this.ViewState["ContextMenuID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ContextMenuID"] = value;
			}
		}

		// Token: 0x1700408E RID: 16526
		// (get) Token: 0x0600C7AD RID: 51117 RVA: 0x002C74B2 File Offset: 0x002C56B2
		// (set) Token: 0x0600C7AE RID: 51118 RVA: 0x002C74D3 File Offset: 0x002C56D3
		[ClientPersistedProperty]
		[DefaultValue(true)]
		[Description("Specifies if context menu will be displayed when this node right-clicked.")]
		public bool EnableContextMenu
		{
			get
			{
				return (bool)(this.ViewState["EnableContextMenu"] ?? true);
			}
			set
			{
				this.ViewState["EnableContextMenu"] = value;
			}
		}

		// Token: 0x0600C7AF RID: 51119 RVA: 0x002C74EC File Offset: 0x002C56EC
		public string GetFullPath(string delimiter)
		{
			string text = string.Empty;
			Stack stack = new Stack();
			stack.Push(this.Text);
			for (RadTreeNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				stack.Push(parentNode.Text);
			}
			foreach (object obj in stack)
			{
				string str = (string)obj;
				text = text + str + delimiter;
			}
			return text.Substring(0, text.Length - delimiter.Length);
		}

		// Token: 0x0600C7B0 RID: 51120 RVA: 0x002C7594 File Offset: 0x002C5794
		public void CollapseChildNodes()
		{
			foreach (object obj in this.Nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				radTreeNode.Expanded = false;
				radTreeNode.CollapseChildNodes();
			}
		}

		// Token: 0x0600C7B1 RID: 51121 RVA: 0x002C75F4 File Offset: 0x002C57F4
		public void CollapseParentNodes()
		{
			for (RadTreeNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				parentNode.Expanded = false;
			}
		}

		// Token: 0x0600C7B2 RID: 51122 RVA: 0x002C761B File Offset: 0x002C581B
		public void InsertBefore(RadTreeNode node)
		{
			this.Owner.Nodes.Insert(this.Owner.Nodes.IndexOf(this), node);
		}

		// Token: 0x0600C7B3 RID: 51123 RVA: 0x002C763F File Offset: 0x002C583F
		public void InsertAfter(RadTreeNode node)
		{
			this.Owner.Nodes.Insert(this.Owner.Nodes.IndexOf(this) + 1, node);
		}

		// Token: 0x0600C7B4 RID: 51124 RVA: 0x002C7668 File Offset: 0x002C5868
		public bool IsAncestorOf(RadTreeNode node)
		{
			for (RadTreeNode parentNode = node.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				if (parentNode == this)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C7B5 RID: 51125 RVA: 0x002C768F File Offset: 0x002C588F
		public bool IsDescendantOf(RadTreeNode node)
		{
			return node.IsAncestorOf(this);
		}

		// Token: 0x0600C7B6 RID: 51126 RVA: 0x002C7698 File Offset: 0x002C5898
		public void Toggle()
		{
			this.Expanded = !this.Expanded;
		}

		// Token: 0x0600C7B7 RID: 51127 RVA: 0x002C76AC File Offset: 0x002C58AC
		public void ExpandChildNodes()
		{
			foreach (object obj in this.Nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				radTreeNode.Expanded = true;
				radTreeNode.ExpandChildNodes();
			}
		}

		// Token: 0x0600C7B8 RID: 51128 RVA: 0x002C770C File Offset: 0x002C590C
		public void ExpandParentNodes()
		{
			for (RadTreeNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				parentNode.Expanded = true;
			}
		}

		// Token: 0x0600C7B9 RID: 51129 RVA: 0x002C7733 File Offset: 0x002C5933
		public void Remove()
		{
			if (this.Owner != null)
			{
				this.Owner.Nodes.Remove(this);
			}
		}

		// Token: 0x0600C7BA RID: 51130 RVA: 0x002C7750 File Offset: 0x002C5950
		public RadTreeNode Clone()
		{
			RadTreeNode radTreeNode = new RadTreeNode();
			foreach (object obj in TypeDescriptor.GetProperties(this))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.Name == "ID"))
				{
					if (propertyDescriptor.Name == "Font")
					{
						radTreeNode.Font.CopyFrom(this.Font);
					}
					else if (!propertyDescriptor.IsReadOnly)
					{
						propertyDescriptor.SetValue(radTreeNode, propertyDescriptor.GetValue(this));
					}
				}
			}
			foreach (object obj2 in base.Attributes.Keys)
			{
				string key = (string)obj2;
				radTreeNode.Attributes[key] = base.Attributes[key];
			}
			foreach (object obj3 in this.Nodes)
			{
				RadTreeNode radTreeNode2 = (RadTreeNode)obj3;
				radTreeNode.Nodes.Add(radTreeNode2.Clone());
			}
			return radTreeNode;
		}

		// Token: 0x0600C7BB RID: 51131 RVA: 0x002C78BC File Offset: 0x002C5ABC
		public void CheckChildNodes()
		{
			foreach (object obj in this.Nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				if (radTreeNode.Checkable)
				{
					radTreeNode.Checked = true;
				}
				radTreeNode.CheckChildNodes();
			}
		}

		// Token: 0x0600C7BC RID: 51132 RVA: 0x002C7924 File Offset: 0x002C5B24
		public void UncheckChildNodes()
		{
			foreach (object obj in this.Nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				if (radTreeNode.Checkable)
				{
					radTreeNode.Checked = false;
				}
				radTreeNode.UncheckChildNodes();
			}
		}

		// Token: 0x0600C7BD RID: 51133 RVA: 0x002C798C File Offset: 0x002C5B8C
		public IList<RadTreeNode> GetAllNodes()
		{
			return base.GetAllChildren<RadTreeNode>();
		}

		// Token: 0x0400348A RID: 13450
		[ThreadStatic]
		private static IList<PropertyDescriptor> _clientPersistPropertiesCache;

		// Token: 0x0400348B RID: 13451
		private bool _skipLogging;

		// Token: 0x0400348C RID: 13452
		private IRenderer _renderer;

		// Token: 0x0400348D RID: 13453
		private bool _selected;

		// Token: 0x0400348E RID: 13454
		private IRadTreeNodeContainer _owner;
	}
}
