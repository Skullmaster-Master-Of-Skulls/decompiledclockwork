using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000171 RID: 369
	internal class ReorderListDesigner : DataBoundControlDesigner
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0001AF70 File Offset: 0x00019170
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new ReorderListDesigner.ReorderListDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0001AF9D File Offset: 0x0001919D
		private object CurrentObject
		{
			get
			{
				return base.Component;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001AFA5 File Offset: 0x000191A5
		private ITemplate CurrentTemplate
		{
			get
			{
				if (this.CurrentTemplateDescriptor != null)
				{
					return (ITemplate)this.CurrentTemplateDescriptor.GetValue(base.Component);
				}
				return null;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0001AFC8 File Offset: 0x000191C8
		private PropertyDescriptor CurrentTemplateDescriptor
		{
			get
			{
				string name = ReorderListDesigner.TemplateItems[this.CurrentView].Name;
				return TypeDescriptor.GetProperties(base.Component)[name];
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001B004 File Offset: 0x00019204
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0001B03C File Offset: 0x0001923C
		private int CurrentView
		{
			get
			{
				object obj = base.DesignerState["CurrentView"];
				int num = (obj == null) ? 0 : ((int)obj);
				if (num >= ReorderListDesigner.TemplateItems.Length)
				{
					num = 0;
				}
				return num;
			}
			set
			{
				base.DesignerState["CurrentView"] = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001B054 File Offset: 0x00019254
		private string CurrentViewName
		{
			get
			{
				return ReorderListDesigner.TemplateItems[this.CurrentView].Name;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0001B070 File Offset: 0x00019270
		private ITemplate CurrentViewControlTemplate
		{
			get
			{
				return this.CurrentTemplate;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001B078 File Offset: 0x00019278
		private ReorderList ReorderList
		{
			get
			{
				return (ReorderList)base.Component;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001B088 File Offset: 0x00019288
		private TemplateDefinition TemplateDefinition
		{
			get
			{
				return new TemplateDefinition(this, this.CurrentViewName, this.ReorderList, this.CurrentViewName)
				{
					SupportsDataBinding = true
				};
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001B0B8 File Offset: 0x000192B8
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				if (this._templateGroups == null)
				{
					this._templateGroups = new TemplateGroupCollection();
					foreach (ReorderListDesigner.TemplateItem templateItem in ReorderListDesigner.TemplateItems)
					{
						TemplateGroup templateGroup = new TemplateGroup(templateItem.Name);
						templateGroup.AddTemplateDefinition(new TemplateDefinition(this, templateItem.Name, this.ReorderList, templateItem.Name)
						{
							SupportsDataBinding = templateItem.SupportsDataBinding
						});
						this._templateGroups.Add(templateGroup);
					}
				}
				templateGroups.AddRange(this._templateGroups);
				return templateGroups;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001B15E File Offset: 0x0001935E
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001B164 File Offset: 0x00019364
		private EditableDesignerRegion BuildRegion()
		{
			return new ReorderListDesigner.ReorderListDesignerRegion(this.CurrentObject, this.CurrentTemplate, this.CurrentTemplateDescriptor, this.TemplateDefinition)
			{
				Description = this.CurrentViewName
			};
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001B19C File Offset: 0x0001939C
		public override string GetDesignTimeHtml()
		{
			string result = string.Empty;
			if (this.CurrentViewControlTemplate != null)
			{
				ReorderList reorderList = (ReorderList)base.ViewControl;
				HybridDictionary hybridDictionary = new HybridDictionary(1);
				hybridDictionary["TemplateIndex"] = this.CurrentView;
				((IControlDesignerAccessor)reorderList).SetDesignModeState(hybridDictionary);
				result = base.GetDesignTimeHtml();
			}
			return result;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001B1F0 File Offset: 0x000193F0
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			string empty = string.Empty;
			regions.Add(this.BuildRegion());
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (this.CurrentTemplate == null)
			{
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<table cellspacing=0 cellpadding=0 border=0 style=\"display:inline-block\">\n                <tr>\n                    <td nowrap align=center valign=middle style=\"color:{0}; background-color:{1}; \">{2}</td>\n                </tr>\n                <tr>\n                    <td style=\"vertical-align:top;\" {3}='0'>{4}</td>\n                </tr>\n          </table>", new object[]
				{
					ColorTranslator.ToHtml(SystemColors.ControlText),
					ColorTranslator.ToHtml(SystemColors.Control),
					this.ReorderList.ID,
					DesignerRegion.DesignerRegionAttributeName,
					empty
				}));
			}
			else
			{
				DataList dataList = new DataList();
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "\n                <table cellspacing=0 cellpadding=0 border=0 style=\"display:inline-block;border:outset white 1px;\">\n                <tr>\n                    <td nowrap align=center valign=middle style=\"background-color:{6}; \"><span style=\"font:messagebox;color:{5}\"><b>{8}</b> - {7}</span></td>\n                </tr>               \n                <tr>                \n                <td>\n                  <table cellspacing=0 cellpadding=2 border=0 style=\"margin:2px;border:solid 1px buttonface\">\n                    <tr style=\"font:messagebox;background-color:lightblue;color:black\">\n                      <td style=\"border:solid 1px buttonshadow\">\n                        &nbsp;{0}&nbsp;&nbsp;&nbsp;\n                      </td>\n                    </tr>\n                    <tr style=\"{1}\" height=100%>\n                      <td style=\"{2}\">\n                        <div style=\"width:100%;height:100%\" {3}='0'>{4}</div>\n                      </td>\n                    </tr>\n                  </table>\n                </td>\n              </tr></table>", new object[]
				{
					this.CurrentViewName,
					dataList.HeaderStyle,
					this.ReorderList.ControlStyle,
					DesignerRegion.DesignerRegionAttributeName,
					empty,
					ColorTranslator.ToHtml(SystemColors.ControlText),
					ColorTranslator.ToHtml(SystemColors.Control),
					this.ReorderList.ID,
					this.ReorderList.GetType().Name
				}));
				dataList.Dispose();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001B320 File Offset: 0x00019520
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			ReorderListDesigner.ReorderListDesignerRegion reorderListDesignerRegion = region as ReorderListDesigner.ReorderListDesignerRegion;
			if (reorderListDesignerRegion != null)
			{
				ITemplate template = reorderListDesignerRegion.Template;
				if (template != null)
				{
					IDesignerHost host = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
					return ControlPersister.PersistTemplate(template, host);
				}
			}
			return base.GetEditableDesignerRegionContent(region);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001B370 File Offset: 0x00019570
		protected override string GetEmptyDesignTimeHtml()
		{
			string instruction = "<br />Empty " + ReorderListDesigner.TemplateItems[this.CurrentView].Name + "<br />";
			return base.CreatePlaceHolderDesignTimeHtml(instruction);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001B3AE File Offset: 0x000195AE
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml("Error rendering ReorderList:<br />" + e.Message);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001B3C8 File Offset: 0x000195C8
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			ReorderListDesigner.ReorderListDesignerRegion reorderListDesignerRegion = region as ReorderListDesigner.ReorderListDesignerRegion;
			if (reorderListDesignerRegion == null)
			{
				return;
			}
			IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			ITemplate template = ControlParser.ParseTemplate(designerHost, content);
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("SetEditableDesignerRegionContent"))
			{
				reorderListDesignerRegion.PropertyDescriptor.SetValue(reorderListDesignerRegion.Object, template);
				designerTransaction.Commit();
			}
			reorderListDesignerRegion.Template = template;
		}

		// Token: 0x040003EB RID: 1003
		private const string _designtimeHTML = "<table cellspacing=0 cellpadding=0 border=0 style=\"display:inline-block\">\n                <tr>\n                    <td nowrap align=center valign=middle style=\"color:{0}; background-color:{1}; \">{2}</td>\n                </tr>\n                <tr>\n                    <td style=\"vertical-align:top;\" {3}='0'>{4}</td>\n                </tr>\n          </table>";

		// Token: 0x040003EC RID: 1004
		private const string _designtimeHTML_Template = "\n                <table cellspacing=0 cellpadding=0 border=0 style=\"display:inline-block;border:outset white 1px;\">\n                <tr>\n                    <td nowrap align=center valign=middle style=\"background-color:{6}; \"><span style=\"font:messagebox;color:{5}\"><b>{8}</b> - {7}</span></td>\n                </tr>               \n                <tr>                \n                <td>\n                  <table cellspacing=0 cellpadding=2 border=0 style=\"margin:2px;border:solid 1px buttonface\">\n                    <tr style=\"font:messagebox;background-color:lightblue;color:black\">\n                      <td style=\"border:solid 1px buttonshadow\">\n                        &nbsp;{0}&nbsp;&nbsp;&nbsp;\n                      </td>\n                    </tr>\n                    <tr style=\"{1}\" height=100%>\n                      <td style=\"{2}\">\n                        <div style=\"width:100%;height:100%\" {3}='0'>{4}</div>\n                      </td>\n                    </tr>\n                  </table>\n                </td>\n              </tr></table>";

		// Token: 0x040003ED RID: 1005
		private const int DefaultTemplateIndex = 0;

		// Token: 0x040003EE RID: 1006
		private TemplateGroupCollection _templateGroups;

		// Token: 0x040003EF RID: 1007
		private static ReorderListDesigner.TemplateItem[] TemplateItems = new ReorderListDesigner.TemplateItem[]
		{
			new ReorderListDesigner.TemplateItem("ItemTemplate", true),
			new ReorderListDesigner.TemplateItem("EditItemTemplate", true),
			new ReorderListDesigner.TemplateItem("DragHandleTemplate", true),
			new ReorderListDesigner.TemplateItem("ReorderTemplate", false),
			new ReorderListDesigner.TemplateItem("InsertItemTemplate", true),
			new ReorderListDesigner.TemplateItem("EmptyListTemplate", false)
		};

		// Token: 0x02000172 RID: 370
		private struct TemplateItem
		{
			// Token: 0x06000A5D RID: 2653 RVA: 0x0001B4F4 File Offset: 0x000196F4
			public TemplateItem(string name, bool supportsDataBinding)
			{
				this.Name = name;
				this.SupportsDataBinding = supportsDataBinding;
			}

			// Token: 0x040003F0 RID: 1008
			public readonly string Name;

			// Token: 0x040003F1 RID: 1009
			public readonly bool SupportsDataBinding;
		}

		// Token: 0x02000173 RID: 371
		private class ReorderListDesignerRegion : TemplatedEditableDesignerRegion
		{
			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0001B504 File Offset: 0x00019704
			// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0001B50C File Offset: 0x0001970C
			public ITemplate Template
			{
				get
				{
					return this._template;
				}
				set
				{
					this._template = value;
				}
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0001B515 File Offset: 0x00019715
			public object Object
			{
				get
				{
					return this._object;
				}
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0001B51D File Offset: 0x0001971D
			public PropertyDescriptor PropertyDescriptor
			{
				get
				{
					return this._prop;
				}
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x0001B525 File Offset: 0x00019725
			public ReorderListDesignerRegion(object obj, ITemplate template, PropertyDescriptor descriptor, TemplateDefinition definition) : base(definition)
			{
				this._template = template;
				this._object = obj;
				this._prop = descriptor;
				base.EnsureSize = true;
			}

			// Token: 0x040003F2 RID: 1010
			private ITemplate _template;

			// Token: 0x040003F3 RID: 1011
			private object _object;

			// Token: 0x040003F4 RID: 1012
			private PropertyDescriptor _prop;
		}

		// Token: 0x02000174 RID: 372
		private class ReorderListDesignerActionList : DesignerActionList
		{
			// Token: 0x06000A63 RID: 2659 RVA: 0x0001B54B File Offset: 0x0001974B
			public ReorderListDesignerActionList(ReorderListDesigner designer) : base(designer.Component)
			{
				this._designer = designer;
			}

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001B560 File Offset: 0x00019760
			// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0001B58C File Offset: 0x0001978C
			[TypeConverter(typeof(ReorderListDesigner.ReorderListDesignerActionList.ReorderListViewTypeConverter))]
			public string View
			{
				get
				{
					return this._designer.CurrentViewName;
				}
				set
				{
					int num = Array.FindIndex<ReorderListDesigner.TemplateItem>(ReorderListDesigner.TemplateItems, (ReorderListDesigner.TemplateItem t) => t.Name == value);
					if (num != -1)
					{
						this._designer.CurrentView = num;
					}
					this._designer.UpdateDesignTimeHtml();
				}
			}

			// Token: 0x040003F5 RID: 1013
			private ReorderListDesigner _designer;

			// Token: 0x02000175 RID: 373
			private class ReorderListViewTypeConverter : TypeConverter
			{
				// Token: 0x06000A66 RID: 2662 RVA: 0x0001B5D8 File Offset: 0x000197D8
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					string[] array = new string[ReorderListDesigner.TemplateItems.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = ReorderListDesigner.TemplateItems[i].Name;
					}
					return new TypeConverter.StandardValuesCollection(array);
				}

				// Token: 0x06000A67 RID: 2663 RVA: 0x0001B61E File Offset: 0x0001981E
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x06000A68 RID: 2664 RVA: 0x0001B621 File Offset: 0x00019821
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}
			}
		}
	}
}
