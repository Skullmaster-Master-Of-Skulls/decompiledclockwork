using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000192 RID: 402
	public class TabContainerDesigner : ControlDesigner
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001D798 File Offset: 0x0001B998
		private TabContainer TabContainer
		{
			get
			{
				return (TabContainer)base.Component;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0001D7A8 File Offset: 0x0001B9A8
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new TabContainerDesigner.TabContainerDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
		private string CurrentTabID
		{
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					TabPanel tabPanel = this.TabContainer.FindControl(value) as TabPanel;
					if (tabPanel == null)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Can't find child tab '{0}'", new object[]
						{
							value
						}));
					}
					int num = -1;
					TabContainer tabContainer = this.TabContainer;
					for (int i = 0; i < tabContainer.Tabs.Count; i++)
					{
						if (tabContainer.Tabs[i] == tabPanel)
						{
							num = i;
							break;
						}
					}
					if (num != -1)
					{
						TypeDescriptor.GetProperties(tabContainer)["ActiveTabIndex"].SetValue(tabContainer, num);
					}
				}
				this.UpdateDesignTimeHtml();
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0001D881 File Offset: 0x0001BA81
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001D884 File Offset: 0x0001BA84
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			if (regions == null)
			{
				throw new ArgumentNullException("regions");
			}
			if (this.TabContainer.ActiveTab != null)
			{
				EditableDesignerRegion region = new EditableDesignerRegion(this, string.Format(CultureInfo.InvariantCulture, "c{0}", new object[]
				{
					this.TabContainer.ActiveTab.ID
				}));
				regions.Add(region);
				string tabContent = this.GetTabContent(this.TabContainer.ActiveTab, true);
				StringBuilder stringBuilder = new StringBuilder();
				int num = 2;
				foreach (object obj in this.TabContainer.Tabs)
				{
					TabPanel tabPanel = (TabPanel)obj;
					bool active = tabPanel.Active;
					string tabContent2 = this.GetTabContent(tabPanel, false);
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<div style='float:left;padding:2px;color:{3}; background-color:{4};{5};height:20px;' {0}='{1}'>{2}</div>", new object[]
					{
						DesignerRegion.DesignerRegionAttributeName,
						active ? 1 : num,
						string.Format(CultureInfo.InvariantCulture, active ? "{0}" : "<a style='padding:2px;border-top:thin white inset;border-left:thin white inset; border-right:thin white inset;' href='#'>{0}</a>", new object[]
						{
							tabContent2
						}),
						ColorTranslator.ToHtml(SystemColors.ControlText),
						active ? ColorTranslator.ToHtml(SystemColors.Window) : "transparent",
						active ? "border-left:thin white outset;border-right:thin white outset;" : string.Empty
					});
					if (active)
					{
						regions.Insert(1, new EditableDesignerRegion(this, string.Format(CultureInfo.InvariantCulture, "h{0}", new object[]
						{
							tabPanel.ID
						})));
					}
					else
					{
						DesignerRegion designerRegion = new DesignerRegion(this, string.Format(CultureInfo.InvariantCulture, "t{0}", new object[]
						{
							tabPanel.ID
						}));
						designerRegion.Selectable = true;
						num++;
						regions.Add(designerRegion);
					}
				}
				StringBuilder stringBuilder2 = new StringBuilder(1024);
				string text = (!this.TabContainer.Height.IsEmpty && this.TabContainer.Height.Type == UnitType.Pixel) ? ((this.TabContainer.Height.Value - 62.0).ToString() + "px") : "100%";
				stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "<div style=\"padding:2px;width:{7};height:{8}\">\n                <div style='text-align:center;color:{0}; background-color:{1};border-left:thin white outset; border-right:thin white outset;height:20px;'>{2}</div>\n                <div style='color:{3}; background-color:{4};border-left:thin white outset; border-right:thin white outset;height:24px;text-align:left;'>{5}</div>\n                <div style='clear:both;text-align:left;border-left:thin white outset; border-bottom:thin white outset; border-right:thin white outset;background-color:{10};height:{11}; padding:8px; overflow:{12};' {9}='0'>{6}</div>\n            </div>", new object[]
				{
					ColorTranslator.ToHtml(SystemColors.ControlText),
					ColorTranslator.ToHtml(SystemColors.ControlDark),
					this.TabContainer.ID,
					ColorTranslator.ToHtml(SystemColors.ControlText),
					ColorTranslator.ToHtml(SystemColors.Control),
					stringBuilder.ToString(),
					tabContent,
					this.TabContainer.Width,
					this.TabContainer.Height,
					DesignerRegion.DesignerRegionAttributeName,
					ColorTranslator.ToHtml(SystemColors.Window),
					text,
					this.HideOverflowContent ? "hidden" : "visible"
				}));
				return stringBuilder2.ToString();
			}
			StringBuilder stringBuilder3 = new StringBuilder(512);
			stringBuilder3.AppendFormat(CultureInfo.InvariantCulture, "<div style='display:inline-block;padding:2px;'>\n                    <div style='color:{0}; background-color:{1};border-left:thin white outset; border-right:thin white outset;'>{2}</div>\n                    <div style=\"text-align:center;border-left:thin white outset; border-bottom:thin white outset; border-right:thin white outset;\" {3}='0'>\n                        <a href='#'>Add New Tab</a>\n                    </div>\n                </div>", new object[]
			{
				ColorTranslator.ToHtml(SystemColors.ControlText),
				ColorTranslator.ToHtml(SystemColors.ControlDark),
				this.TabContainer.ID,
				DesignerRegion.DesignerRegionAttributeName
			});
			DesignerRegion region2 = new DesignerRegion(this, "#addtab");
			regions.Add(region2);
			return stringBuilder3.ToString();
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001DC64 File Offset: 0x0001BE64
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			bool isContent = region.Name[0] == 'c';
			string id = region.Name.Substring(1);
			TabPanel tab = (TabPanel)this.TabContainer.FindControl(id);
			return this.GetTabContent(tab, isContent);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001DCB8 File Offset: 0x0001BEB8
		private string GetTemplateContent(ITemplate template, string id)
		{
			TabContainerDesigner.DesignerPanel designerPanel = new TabContainerDesigner.DesignerPanel();
			designerPanel.ID = id;
			template.InstantiateIn(designerPanel);
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			StringBuilder stringBuilder = new StringBuilder(1024);
			foreach (object obj in designerPanel.Controls)
			{
				Control control = (Control)obj;
				stringBuilder.Append(ControlPersister.PersistControl(control, host));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001DD5C File Offset: 0x0001BF5C
		private string GetTabContent(TabPanel tab, bool isContent)
		{
			if (tab == null)
			{
				return string.Empty;
			}
			if (isContent)
			{
				if (tab.ContentTemplate == null)
				{
					return string.Empty;
				}
				return this.GetTemplateContent(tab.ContentTemplate, "_content");
			}
			else
			{
				if (tab.HeaderTemplate != null)
				{
					return this.GetTemplateContent(tab.HeaderTemplate, "_header");
				}
				return tab.HeaderText;
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.SetViewFlags(ViewFlags.TemplateEditing, true);
			foreach (object obj in this.TabContainer.Tabs)
			{
				TabPanel tabPanel = (TabPanel)obj;
				if (string.IsNullOrEmpty(tabPanel.ID))
				{
					throw new InvalidOperationException("TabPanels must have IDs set.");
				}
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001DE38 File Offset: 0x0001C038
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			bool flag = region.Name[0] == 'c';
			string id = region.Name.Substring(1);
			TabPanel panel = (TabPanel)this.TabContainer.FindControl(id);
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			TabContainerDesigner.PersistTemplateContent(panel, host, content, flag ? "ContentTemplate" : "HeaderTemplate");
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		private static void PersistTemplateContent(TabPanel panel, IDesignerHost host, string content, string propertyName)
		{
			ITemplate template = ControlParser.ParseTemplate(host, content);
			TabContainerDesigner.PersistTemplate(panel, host, template, propertyName);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		private static void PersistTemplate(TabPanel panel, IDesignerHost host, ITemplate template, string propertyName)
		{
			using (DesignerTransaction designerTransaction = host.CreateTransaction("SetEditableDesignerRegionContent"))
			{
				PropertyInfo property = panel.GetType().GetProperty(propertyName);
				if (!(property == null))
				{
					property.SetValue(panel, template, null);
					designerTransaction.Commit();
				}
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0001DF2C File Offset: 0x0001C12C
		protected override void OnClick(DesignerRegionMouseEventArgs e)
		{
			if (e.Region != null && e.Region.Name.StartsWith("t", StringComparison.Ordinal))
			{
				this.CurrentTabID = e.Region.Name.Substring(1);
			}
			else if (e.Region != null && e.Region.Name == "#addtab")
			{
				this.OnAddTabPanel();
			}
			base.OnClick(e);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		private void OnAddTabPanel()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return;
			}
			TabContainer tabContainer = this.TabContainer;
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("Add new TabPanel"))
			{
				TabPanel tabPanel = (TabPanel)designerHost.CreateComponent(typeof(TabPanel));
				if (tabPanel != null)
				{
					tabPanel.ID = TabContainerDesigner.GetUniqueName(typeof(TabPanel), tabContainer);
					tabPanel.HeaderText = tabPanel.ID;
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					try
					{
						componentChangeService.OnComponentChanging(tabContainer, TypeDescriptor.GetProperties(tabContainer)["Tabs"]);
						tabContainer.Tabs.Add(tabPanel);
					}
					finally
					{
						componentChangeService.OnComponentChanged(tabContainer, TypeDescriptor.GetProperties(tabContainer)["Tabs"], tabContainer.Tabs, tabContainer.Tabs);
					}
					TypeDescriptor.GetProperties(tabContainer)["ActiveTab"].SetValue(tabContainer, tabPanel);
					this.CurrentTabID = tabPanel.ID;
				}
				designerTransaction.Commit();
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001E0CC File Offset: 0x0001C2CC
		private static string GetUniqueName(Type t, Control parent)
		{
			string name = t.Name;
			int num = 1;
			while (parent.FindControl(name + num.ToString(CultureInfo.InvariantCulture)) != null)
			{
				num++;
			}
			return name + num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001E114 File Offset: 0x0001C314
		private void OnRemoveTabPanel()
		{
			TabContainer tabContainer = this.TabContainer;
			if (tabContainer.ActiveTab == null)
			{
				return;
			}
			int activeTabIndex = tabContainer.ActiveTabIndex;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("Remove TabPanel"))
				{
					TabPanel activeTab = tabContainer.ActiveTab;
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					try
					{
						componentChangeService.OnComponentChanging(tabContainer, TypeDescriptor.GetProperties(tabContainer)["Tabs"]);
						tabContainer.Tabs.Remove(activeTab);
					}
					finally
					{
						componentChangeService.OnComponentChanged(tabContainer, TypeDescriptor.GetProperties(tabContainer)["Tabs"], tabContainer.Tabs, tabContainer.Tabs);
					}
					activeTab.Dispose();
					if (tabContainer.Tabs.Count > 0)
					{
						TypeDescriptor.GetProperties(tabContainer)["ActiveTabIndex"].SetValue(tabContainer, Math.Min(activeTabIndex, tabContainer.Tabs.Count - 1));
					}
					this.UpdateDesignTimeHtml();
					designerTransaction.Commit();
				}
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0001E244 File Offset: 0x0001C444
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0001E270 File Offset: 0x0001C470
		[Category("Design")]
		[DesignOnly(true)]
		[DefaultValue(false)]
		[Description("Hide overflow content at design-time.")]
		public bool HideOverflowContent
		{
			get
			{
				object obj = base.DesignerState["HideOverflowContent"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.DesignerState["HideOverflowContent"];
				if (obj == null || (bool)obj != value)
				{
					base.DesignerState["HideOverflowContent"] = value;
					this.UpdateDesignTimeHtml();
				}
			}
		}

		// Token: 0x0400043E RID: 1086
		private const string TabLink = "<a style='padding:2px;border-top:thin white inset;border-left:thin white inset; border-right:thin white inset;' href='#'>{0}</a>";

		// Token: 0x0400043F RID: 1087
		private const string ActiveTabLink = "{0}";

		// Token: 0x04000440 RID: 1088
		private const string ClickRegionHtml = "<div style='float:left;padding:2px;color:{3}; background-color:{4};{5};height:20px;' {0}='{1}'>{2}</div>";

		// Token: 0x04000441 RID: 1089
		private const string DesignTimeHtml = "<div style=\"padding:2px;width:{7};height:{8}\">\n                <div style='text-align:center;color:{0}; background-color:{1};border-left:thin white outset; border-right:thin white outset;height:20px;'>{2}</div>\n                <div style='color:{3}; background-color:{4};border-left:thin white outset; border-right:thin white outset;height:24px;text-align:left;'>{5}</div>\n                <div style='clear:both;text-align:left;border-left:thin white outset; border-bottom:thin white outset; border-right:thin white outset;background-color:{10};height:{11}; padding:8px; overflow:{12};' {9}='0'>{6}</div>\n            </div>";

		// Token: 0x04000442 RID: 1090
		private const string EmptyDesignTimeHtml = "<div style='display:inline-block;padding:2px;'>\n                    <div style='color:{0}; background-color:{1};border-left:thin white outset; border-right:thin white outset;'>{2}</div>\n                    <div style=\"text-align:center;border-left:thin white outset; border-bottom:thin white outset; border-right:thin white outset;\" {3}='0'>\n                        <a href='#'>Add New Tab</a>\n                    </div>\n                </div>";

		// Token: 0x04000443 RID: 1091
		private const string AddTabName = "#addtab";

		// Token: 0x02000193 RID: 403
		private class TabContainerDesignerActionList : DesignerActionList
		{
			// Token: 0x06000B8A RID: 2954 RVA: 0x0001E2BE File Offset: 0x0001C4BE
			public TabContainerDesignerActionList(TabContainerDesigner designer) : base(designer.Component)
			{
				this._designer = designer;
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				DesignerActionMethodItem value = new DesignerActionMethodItem(this, "OnAddTabPanel", "Add Tab Panel", true);
				DesignerActionMethodItem value2 = new DesignerActionMethodItem(this, "OnRemoveTabPanel", "Remove Tab Panel", true);
				DesignerActionPropertyItem value3 = new DesignerActionPropertyItem("HideOverflowContent", "Hide overflow content at design-time");
				designerActionItemCollection.Add(value);
				designerActionItemCollection.Add(value2);
				designerActionItemCollection.Add(value3);
				return designerActionItemCollection;
			}

			// Token: 0x17000458 RID: 1112
			// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0001E334 File Offset: 0x0001C534
			// (set) Token: 0x06000B8D RID: 2957 RVA: 0x0001E341 File Offset: 0x0001C541
			public bool HideOverflowContent
			{
				get
				{
					return this._designer.HideOverflowContent;
				}
				set
				{
					this._designer.HideOverflowContent = value;
				}
			}

			// Token: 0x06000B8E RID: 2958 RVA: 0x0001E34F File Offset: 0x0001C54F
			private void OnAddTabPanel()
			{
				this._designer.OnAddTabPanel();
			}

			// Token: 0x06000B8F RID: 2959 RVA: 0x0001E35C File Offset: 0x0001C55C
			private void OnRemoveTabPanel()
			{
				this._designer.OnRemoveTabPanel();
			}

			// Token: 0x04000444 RID: 1092
			private TabContainerDesigner _designer;
		}

		// Token: 0x02000194 RID: 404
		internal class DesignerPanel : Panel, INamingContainer
		{
		}
	}
}
