using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000EF8 RID: 3832
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadSiteMap), "Telerik.Web.UI.SiteMap.png")]
	[Designer("Telerik.Web.Design.RadSiteMapDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadSiteMap Runat=\"server\"></{0}:RadSiteMap>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("SiteMap", typeof(RadSiteMap))]
	[EmbeddedSkin("SiteMap", "Default", typeof(RadSiteMap))]
	[ClientScriptResource("Telerik.Web.UI.RadSiteMap", "Telerik.Web.UI.SiteMap.RadSiteMap.js")]
	[XmlRoot("SiteMap")]
	public class RadSiteMap : HierarchicalControlItemContainer, IRadSiteMapNodeContainer
	{
		// Token: 0x17002DFF RID: 11775
		// (get) Token: 0x06009139 RID: 37177 RVA: 0x0020B187 File Offset: 0x00209387
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Data")]
		public RadSiteMapNodeCollection Nodes
		{
			get
			{
				return (RadSiteMapNodeCollection)base.Children;
			}
		}

		// Token: 0x17002E00 RID: 11776
		// (get) Token: 0x0600913A RID: 37178 RVA: 0x0020B194 File Offset: 0x00209394
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadSiteMapNode SelectedNode
		{
			get
			{
				foreach (RadSiteMapNode radSiteMapNode in this.GetAllNodes())
				{
					if (radSiteMapNode.Selected)
					{
						return radSiteMapNode;
					}
				}
				return null;
			}
		}

		// Token: 0x17002E01 RID: 11777
		// (get) Token: 0x0600913B RID: 37179 RVA: 0x0020B1EC File Offset: 0x002093EC
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		[Description("Default level settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DefaultSiteMapLevelSetting DefaultLevelSettings
		{
			get
			{
				if (this._defaultLevelSettings == null)
				{
					this._defaultLevelSettings = new DefaultSiteMapLevelSetting();
				}
				return this._defaultLevelSettings;
			}
		}

		// Token: 0x17002E02 RID: 11778
		// (get) Token: 0x0600913C RID: 37180 RVA: 0x0020B207 File Offset: 0x00209407
		[DefaultValue(null)]
		[Description("A collection of LevelSettings objects that define the appearance of the nodes according to their level in the hierarchy")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SiteMapLevelSettingCollection LevelSettings
		{
			get
			{
				if (this._levelSettings == null)
				{
					this._levelSettings = new SiteMapLevelSettingCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._levelSettings).TrackViewState();
					}
				}
				return this._levelSettings;
			}
		}

		// Token: 0x17002E03 RID: 11779
		// (get) Token: 0x0600913D RID: 37181 RVA: 0x0020B235 File Offset: 0x00209435
		// (set) Token: 0x0600913E RID: 37182 RVA: 0x0020B23D File Offset: 0x0020943D
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool ShowNodeLines { get; set; }

		// Token: 0x17002E04 RID: 11780
		// (get) Token: 0x0600913F RID: 37183 RVA: 0x0020B246 File Offset: 0x00209446
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Category("Data")]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadSiteMapNodeBindingCollection DataBindings
		{
			get
			{
				return (RadSiteMapNodeBindingCollection)base.NavigationItemBindings;
			}
		}

		// Token: 0x17002E05 RID: 11781
		// (get) Token: 0x06009140 RID: 37184 RVA: 0x0020B253 File Offset: 0x00209453
		// (set) Token: 0x06009141 RID: 37185 RVA: 0x0020B274 File Offset: 0x00209474
		[Description("Specifies whether the text encoding when rendering site map item is enabled or not.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableTextHTMLEncoding
		{
			get
			{
				return (bool)(this.ViewState["EnableTextHTMLEncoding"] ?? false);
			}
			set
			{
				this.ViewState["EnableTextHTMLEncoding"] = value;
			}
		}

		// Token: 0x1400015D RID: 349
		// (add) Token: 0x06009142 RID: 37186 RVA: 0x0020B28C File Offset: 0x0020948C
		// (remove) Token: 0x06009143 RID: 37187 RVA: 0x0020B29F File Offset: 0x0020949F
		public event RadSiteMapNodeEventHandler NodeDataBound
		{
			add
			{
				base.Events.AddHandler(RadSiteMap.NodeDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadSiteMap.NodeDataBoundEvent, value);
			}
		}

		// Token: 0x06009144 RID: 37188 RVA: 0x0020B2B2 File Offset: 0x002094B2
		private void OnNodeDataBound(RadSiteMapNodeEventArgs e)
		{
			this.RaiseNodeEvent(RadSiteMap.NodeDataBoundEvent, e);
		}

		// Token: 0x1400015E RID: 350
		// (add) Token: 0x06009145 RID: 37189 RVA: 0x0020B2C0 File Offset: 0x002094C0
		// (remove) Token: 0x06009146 RID: 37190 RVA: 0x0020B2D3 File Offset: 0x002094D3
		public event RadSiteMapNodeEventHandler NodeCreated
		{
			add
			{
				base.Events.AddHandler(RadSiteMap.NodeCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadSiteMap.NodeCreatedEvent, value);
			}
		}

		// Token: 0x06009147 RID: 37191 RVA: 0x0020B2E6 File Offset: 0x002094E6
		private void OnNodeCreated(RadSiteMapNodeEventArgs e)
		{
			this.RaiseNodeEvent(RadSiteMap.NodeCreatedEvent, e);
		}

		// Token: 0x1400015F RID: 351
		// (add) Token: 0x06009148 RID: 37192 RVA: 0x0020B2F4 File Offset: 0x002094F4
		// (remove) Token: 0x06009149 RID: 37193 RVA: 0x0020B307 File Offset: 0x00209507
		public event RadSiteMapNodeEventHandler TemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadSiteMap.TemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadSiteMap.TemplateNeededEvent, value);
			}
		}

		// Token: 0x0600914A RID: 37194 RVA: 0x0020B31A File Offset: 0x0020951A
		protected virtual void OnTemplateNeeded(RadSiteMapNodeEventArgs e)
		{
			this.RaiseNodeEvent(RadSiteMap.TemplateNeededEvent, e);
		}

		// Token: 0x0600914B RID: 37195 RVA: 0x0020B328 File Offset: 0x00209528
		public void ClearSelectedNode()
		{
			foreach (RadSiteMapNode radSiteMapNode in this.GetAllNodes())
			{
				radSiteMapNode.Selected = false;
			}
		}

		// Token: 0x0600914C RID: 37196 RVA: 0x0020B378 File Offset: 0x00209578
		public IList<RadSiteMapNode> GetAllNodes()
		{
			return base.GetAllChildren<RadSiteMapNode>();
		}

		// Token: 0x17002E06 RID: 11782
		// (get) Token: 0x0600914D RID: 37197 RVA: 0x0020B380 File Offset: 0x00209580
		IRadSiteMapNodeContainer IRadSiteMapNodeContainer.Owner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600914E RID: 37198 RVA: 0x0020B383 File Offset: 0x00209583
		protected internal override ControlItem CreateItem()
		{
			return new RadSiteMapNode();
		}

		// Token: 0x0600914F RID: 37199 RVA: 0x0020B38A File Offset: 0x0020958A
		protected internal override void RaiseItemDataBound(ControlItem item)
		{
			this.OnNodeDataBound(new RadSiteMapNodeEventArgs((RadSiteMapNode)item));
		}

		// Token: 0x06009150 RID: 37200 RVA: 0x0020B39D File Offset: 0x0020959D
		protected override void RaiseItemCreated(ControlItem item)
		{
			this.OnNodeCreated(new RadSiteMapNodeEventArgs((RadSiteMapNode)item));
		}

		// Token: 0x06009151 RID: 37201 RVA: 0x0020B3B0 File Offset: 0x002095B0
		protected override void RaiseTemplateNeeded(ControlItem item)
		{
			this.OnTemplateNeeded(new RadSiteMapNodeEventArgs((RadSiteMapNode)item));
		}

		// Token: 0x06009152 RID: 37202 RVA: 0x0020B3C4 File Offset: 0x002095C4
		protected internal override void InitializeItem(ControlItem item)
		{
			RadSiteMapNode radSiteMapNode = (RadSiteMapNode)item;
			if (item.Template == null)
			{
				SiteMapLevelSetting levelSettings = this.GetLevelSettings(radSiteMapNode.Level);
				if (levelSettings.NodeTemplate != null)
				{
					radSiteMapNode.NodeTemplate = levelSettings.NodeTemplate;
				}
			}
			if (radSiteMapNode.SeparatorTemplate == null)
			{
				SiteMapLevelSetting levelSettings2 = this.GetLevelSettings(radSiteMapNode.Level);
				if (levelSettings2.SeparatorTemplate != null)
				{
					radSiteMapNode.SeparatorTemplate = levelSettings2.SeparatorTemplate;
				}
			}
			RadSiteMap.ApplySeparatorTemplate(radSiteMapNode);
			base.InitializeItem(item);
		}

		// Token: 0x06009153 RID: 37203 RVA: 0x0020B438 File Offset: 0x00209638
		private static void ApplySeparatorTemplate(RadSiteMapNode item)
		{
			if (item.SeparatorTemplateInstantiated)
			{
				return;
			}
			if (item.SeparatorTemplate == null)
			{
				return;
			}
			int num = item.Controls.Count;
			item.SeparatorTemplateContainer = new Control();
			item.SeparatorTemplate.InstantiateIn(item.SeparatorTemplateContainer);
			item.Controls.Add(item.SeparatorTemplateContainer);
			while (num > 0 && !item.Controls.IsReadOnly)
			{
				item.Controls.Add(item.Controls[0]);
				num--;
			}
			item.SeparatorTemplateInstantiated = true;
		}

		// Token: 0x06009154 RID: 37204 RVA: 0x0020B4C5 File Offset: 0x002096C5
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadSiteMapNodeCollection(this);
		}

		// Token: 0x06009155 RID: 37205 RVA: 0x0020B4CD File Offset: 0x002096CD
		protected override NavigationItemBindingCollection CreateDataBindings()
		{
			return new RadSiteMapNodeBindingCollection();
		}

		// Token: 0x17002E07 RID: 11783
		// (get) Token: 0x06009156 RID: 37206 RVA: 0x0020B4D4 File Offset: 0x002096D4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override KeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				throw new InvalidOperationException("SiteMap does not support KeyboardNavigationSettings");
			}
		}

		// Token: 0x06009157 RID: 37207 RVA: 0x0020B4E0 File Offset: 0x002096E0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x06009158 RID: 37208 RVA: 0x0020B4E4 File Offset: 0x002096E4
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new ScriptReference[]
			{
				new ScriptReference("Telerik.Web.UI.SiteMap.RadSiteMap.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x06009159 RID: 37209 RVA: 0x0020B510 File Offset: 0x00209710
		private void RaiseNodeEvent(object eventKey, RadSiteMapNodeEventArgs e)
		{
			RadSiteMapNodeEventHandler radSiteMapNodeEventHandler = (RadSiteMapNodeEventHandler)base.Events[eventKey];
			if (radSiteMapNodeEventHandler != null)
			{
				radSiteMapNodeEventHandler(this, e);
			}
		}

		// Token: 0x17002E08 RID: 11784
		// (get) Token: 0x0600915A RID: 37210 RVA: 0x0020B53C File Offset: 0x0020973C
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadSiteMap RadSiteMap_{0}";
				if (base.Attributes["dir"] == "rtl")
				{
					text += " RadSiteMap_rtl RadSiteMap_{0}_rtl";
				}
				return text;
			}
		}

		// Token: 0x17002E09 RID: 11785
		// (get) Token: 0x0600915B RID: 37211 RVA: 0x0020B578 File Offset: 0x00209778
		private string LevelsCssClass
		{
			get
			{
				if (this._levelsCssClass == null)
				{
					switch (this.GetTotalLevels())
					{
					case 0:
						this._levelsCssClass = "";
						break;
					case 1:
						this._levelsCssClass = "rsmOneLevel";
						break;
					case 2:
						this._levelsCssClass = "rsmTwoLevels";
						break;
					case 3:
						this._levelsCssClass = "rsmThreeLevels";
						break;
					default:
						this._levelsCssClass = "rsmManyLevels";
						break;
					}
				}
				return this._levelsCssClass;
			}
		}

		// Token: 0x0600915C RID: 37212 RVA: 0x0020B5F4 File Offset: 0x002097F4
		internal static IList<RadSiteMapNode> GetNodes(IList<RadSiteMapNode> nodes, int count)
		{
			if (count == 0 || nodes.Count == 0)
			{
				return nodes;
			}
			int num = Math.Min(nodes.Count, count);
			IList<RadSiteMapNode> list = new List<RadSiteMapNode>();
			for (int i = 0; i < num; i++)
			{
				list.Add(nodes[i]);
			}
			return list;
		}

		// Token: 0x0600915D RID: 37213 RVA: 0x0020B63B File Offset: 0x0020983B
		internal SiteMapLevelSetting GetLevelSettings(int level)
		{
			return this.LevelSettings.GetLevelSetting(level) ?? this.DefaultLevelSettings;
		}

		// Token: 0x0600915E RID: 37214 RVA: 0x0020B654 File Offset: 0x00209854
		internal static string GetChildListClass(int level, SiteMapLevelSetting levelSetting, bool showNodeLines)
		{
			List<string> list = new List<string>();
			if (levelSetting.Layout == SiteMapLayout.List)
			{
				list.Add("rsmList");
				if (levelSetting.ListLayout.RepeatColumns > 1)
				{
					if (levelSetting.ListLayout.AlignRows)
					{
						list.Add("rsmMultiColumn");
					}
					else
					{
						list.Add("rsmColumn");
					}
				}
				else if (showNodeLines)
				{
					list.Add("rsmNodeLines");
				}
			}
			else
			{
				list.Add("rsmFlow");
			}
			string text = "rsmLevel";
			if (level > 0)
			{
				text += level.ToString();
			}
			list.Add(text);
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x0600915F RID: 37215 RVA: 0x0020B6FC File Offset: 0x002098FC
		internal static Unit GetChildListWidth(SiteMapLevelSetting levelSetting)
		{
			int repeatColumns = levelSetting.ListLayout.RepeatColumns;
			if (levelSetting.Layout == SiteMapLayout.List && repeatColumns > 1 && !levelSetting.ListLayout.AlignRows)
			{
				return Unit.Percentage(Math.Floor(100.0 / (double)repeatColumns));
			}
			return levelSetting.Width;
		}

		// Token: 0x06009160 RID: 37216 RVA: 0x0020B74C File Offset: 0x0020994C
		private bool ShouldOverrideColumnCssClass()
		{
			bool result = this.DefaultLevelSettings.ListLayout.RepeatColumns > 1;
			foreach (object obj in this.LevelSettings)
			{
				SiteMapLevelSetting siteMapLevelSetting = (SiteMapLevelSetting)obj;
				if (siteMapLevelSetting.ListLayout.RepeatColumns > 1)
				{
					return true;
				}
			}
			return result;
		}

		// Token: 0x06009161 RID: 37217 RVA: 0x0020B7CC File Offset: 0x002099CC
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				if (this.ShouldOverrideColumnCssClass())
				{
					writer.Write("<style type=\"text/css\">");
					writer.Write("\r\n                            div.RadSiteMap\r\n                            {\r\n                                *display: inline !important;\r\n                            }\r\n                            div.RadSiteMap .rsmList,\r\n                            div.RadSiteMap .rsmItem\r\n                            {\r\n                                *display: block !important;\r\n                            }\r\n                            div.RadSiteMap .rsmList { margin-left: -1px; }\r\n                            div.RadSiteMap { padding-left: 1px; }\r\n                        ");
					writer.Write("</style>");
				}
			}
			if (this.Nodes.Count == 0)
			{
				return;
			}
			RadSiteMap.RenderLevelNodes(writer, this.GetLevelSettings(0), this.Nodes, new RadSiteMap.RenderListDelegate(this.RenderList));
		}

		// Token: 0x06009162 RID: 37218 RVA: 0x0020B84C File Offset: 0x00209A4C
		internal static void RenderLevelNodes(HtmlTextWriter writer, SiteMapLevelSetting levelSettings, IList<RadSiteMapNode> nodes, RadSiteMap.RenderListDelegate renderListDelegate)
		{
			IList<RadSiteMapNode> nodes2 = RadSiteMap.GetNodes(nodes, levelSettings.MaximumNodes);
			if (levelSettings.ListLayout.RepeatColumns == 1 || levelSettings.Layout == SiteMapLayout.Flow)
			{
				renderListDelegate(writer, nodes2);
				return;
			}
			if (!levelSettings.ListLayout.AlignRows)
			{
				int repeatColumns = levelSettings.ListLayout.RepeatColumns;
				for (int i = 0; i < repeatColumns; i++)
				{
					if (levelSettings.ListLayout.RepeatDirection == SiteMapRepeatDirection.Vertical)
					{
						renderListDelegate(writer, ControlItemContainer.Helpers.GetRowItems<RadSiteMapNode>(i, repeatColumns, nodes2));
					}
					else
					{
						renderListDelegate(writer, ControlItemContainer.Helpers.GetColumnItems<RadSiteMapNode>(i, repeatColumns, nodes2));
					}
				}
				return;
			}
			if (levelSettings.ListLayout.RepeatDirection == SiteMapRepeatDirection.Horizontal)
			{
				renderListDelegate(writer, nodes2);
				return;
			}
			int repeatColumns2 = levelSettings.ListLayout.RepeatColumns;
			int rowsCount = RadSiteMap.GetRowsCount(repeatColumns2, nodes.Count);
			List<RadSiteMapNode> list = new List<RadSiteMapNode>();
			if (RadSiteMap.ShouldUseFlattenedColumns(rowsCount, repeatColumns2, nodes2.Count))
			{
				for (int j = 0; j < rowsCount; j++)
				{
					IList<RadSiteMapNode> flattenedColumnItems = ControlItemContainer.Helpers.GetFlattenedColumnItems<RadSiteMapNode>(rowsCount, j, repeatColumns2, nodes);
					list.AddRange(RadSiteMap.BalanceColumns(flattenedColumnItems, repeatColumns2));
				}
			}
			else
			{
				for (int k = 0; k < repeatColumns2; k++)
				{
					list.AddRange(ControlItemContainer.Helpers.GetColumnItems<RadSiteMapNode>(k, repeatColumns2, nodes));
				}
			}
			renderListDelegate(writer, list);
		}

		// Token: 0x06009163 RID: 37219 RVA: 0x0020B97E File Offset: 0x00209B7E
		private static IList<RadSiteMapNode> BalanceColumns(IList<RadSiteMapNode> items, int numbersOfColumns)
		{
			if (items.Count < numbersOfColumns)
			{
				items[items.Count - 1].BreakRow();
			}
			return items;
		}

		// Token: 0x06009164 RID: 37220 RVA: 0x0020B99D File Offset: 0x00209B9D
		private static bool ShouldUseFlattenedColumns(int rowsCount, int columnsCount, int maxItemsCount)
		{
			return maxItemsCount < rowsCount * columnsCount;
		}

		// Token: 0x06009165 RID: 37221 RVA: 0x0020B9A8 File Offset: 0x00209BA8
		private static int GetRowsCount(int columns, int nodesCount)
		{
			if (nodesCount % columns != 0)
			{
				nodesCount += nodesCount / columns;
			}
			return nodesCount / columns;
		}

		// Token: 0x06009166 RID: 37222 RVA: 0x0020B9C8 File Offset: 0x00209BC8
		private void RenderList(HtmlTextWriter writer, IList<RadSiteMapNode> nodesToRender)
		{
			if (nodesToRender.Count == 0)
			{
				return;
			}
			SiteMapLevelSetting levelSettings = this.GetLevelSettings(0);
			string text = RadSiteMap.GetChildListClass(0, levelSettings, false);
			if (!string.IsNullOrEmpty(this.LevelsCssClass))
			{
				text = text + " " + this.LevelsCssClass;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			Unit childListWidth = RadSiteMap.GetChildListWidth(levelSettings);
			if (childListWidth != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, childListWidth.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			for (int i = 0; i < nodesToRender.Count; i++)
			{
				nodesToRender[i].Render(i, writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009167 RID: 37223 RVA: 0x0020BA70 File Offset: 0x00209C70
		private int GetTotalLevels()
		{
			IList<RadSiteMapNode> allNodes = this.GetAllNodes();
			if (allNodes.Count == 0)
			{
				return 0;
			}
			int num = 0;
			foreach (RadSiteMapNode radSiteMapNode in allNodes)
			{
				num = Math.Max(num, radSiteMapNode.Level);
			}
			num++;
			for (int i = 0; i < num; i++)
			{
				if (this.GetLevelSettings(i).Layout == SiteMapLayout.Flow)
				{
					num = i + 1;
					break;
				}
			}
			return num;
		}

		// Token: 0x06009169 RID: 37225 RVA: 0x0020BAFC File Offset: 0x00209CFC
		// Note: this type is marked as 'beforefieldinit'.
		static RadSiteMap()
		{
			RadSiteMap.NodeDataBoundEvent = new object();
			RadSiteMap.NodeCreatedEvent = new object();
			RadSiteMap.TemplateNeededEvent = new object();
		}

		// Token: 0x0400293C RID: 10556
		private DefaultSiteMapLevelSetting _defaultLevelSettings;

		// Token: 0x0400293D RID: 10557
		private SiteMapLevelSettingCollection _levelSettings;

		// Token: 0x04002941 RID: 10561
		private string _levelsCssClass;

		// Token: 0x02000EF9 RID: 3833
		internal static class Styles
		{
			// Token: 0x04002943 RID: 10563
			public const string OneLevel = "rsmOneLevel";

			// Token: 0x04002944 RID: 10564
			public const string TwoLevels = "rsmTwoLevels";

			// Token: 0x04002945 RID: 10565
			public const string ThreeLevels = "rsmThreeLevels";

			// Token: 0x04002946 RID: 10566
			public const string ManyLevels = "rsmManyLevels";

			// Token: 0x02000EFA RID: 3834
			public static class ChildList
			{
				// Token: 0x04002947 RID: 10567
				public const string FlowCssClass = "rsmFlow";

				// Token: 0x04002948 RID: 10568
				public const string ListCssClass = "rsmList";

				// Token: 0x04002949 RID: 10569
				public const string MultiColumnCssClass = "rsmMultiColumn";

				// Token: 0x0400294A RID: 10570
				public const string ColumnCssClass = "rsmColumn";

				// Token: 0x0400294B RID: 10571
				public const string WrapperCssClass = "rsmColumnWrap";

				// Token: 0x0400294C RID: 10572
				public const string NodeLinesCssClass = "rsmNodeLines";

				// Token: 0x0400294D RID: 10573
				public const string LevelCssClass = "rsmLevel";
			}

			// Token: 0x02000EFB RID: 3835
			public static class Node
			{
				// Token: 0x0400294E RID: 10574
				public const string CssClass = "rsmItem";

				// Token: 0x0400294F RID: 10575
				public const string LinkCssClass = "rsmLink";

				// Token: 0x04002950 RID: 10576
				public const string TemplateCssClass = "rsmTemplate";

				// Token: 0x04002951 RID: 10577
				public const string ImageCssClass = "rsmImage";

				// Token: 0x04002952 RID: 10578
				public const string LastNodeClass = "rsmLast";

				// Token: 0x04002953 RID: 10579
				public const string DisabledCssClass = "rsmDisabled";
			}
		}

		// Token: 0x02000EFC RID: 3836
		// (Invoke) Token: 0x0600916B RID: 37227
		internal delegate void RenderListDelegate(HtmlTextWriter writer, IList<RadSiteMapNode> nodes);
	}
}
