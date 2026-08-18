using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001AAF RID: 6831
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class SiteMapLevelSetting : StateManager
	{
		// Token: 0x06010826 RID: 67622 RVA: 0x003B0249 File Offset: 0x003AE449
		public SiteMapLevelSetting()
		{
		}

		// Token: 0x06010827 RID: 67623 RVA: 0x003B0251 File Offset: 0x003AE451
		public SiteMapLevelSetting(int level) : this(level, SiteMapLayout.List)
		{
		}

		// Token: 0x06010828 RID: 67624 RVA: 0x003B025B File Offset: 0x003AE45B
		public SiteMapLevelSetting(SiteMapLayout layout) : this(-1, layout)
		{
		}

		// Token: 0x06010829 RID: 67625 RVA: 0x003B0265 File Offset: 0x003AE465
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public SiteMapLevelSetting(int level, SiteMapLayout layout)
		{
			this.Level = level;
			this.Layout = layout;
		}

		// Token: 0x17005037 RID: 20535
		// (get) Token: 0x0601082A RID: 67626 RVA: 0x003B027B File Offset: 0x003AE47B
		// (set) Token: 0x0601082B RID: 67627 RVA: 0x003B029C File Offset: 0x003AE49C
		[NotifyParentProperty(true)]
		[DefaultValue(-1)]
		public virtual int Level
		{
			get
			{
				return (int)(base.ViewState["Level"] ?? -1);
			}
			set
			{
				base.ViewState["Level"] = value;
			}
		}

		// Token: 0x17005038 RID: 20536
		// (get) Token: 0x0601082C RID: 67628 RVA: 0x003B02B4 File Offset: 0x003AE4B4
		// (set) Token: 0x0601082D RID: 67629 RVA: 0x003B02D5 File Offset: 0x003AE4D5
		[NotifyParentProperty(true)]
		[DefaultValue(SiteMapLayout.List)]
		public SiteMapLayout Layout
		{
			get
			{
				return (SiteMapLayout)(base.ViewState["Layout"] ?? SiteMapLayout.List);
			}
			set
			{
				base.ViewState["Layout"] = value;
			}
		}

		// Token: 0x17005039 RID: 20537
		// (get) Token: 0x0601082E RID: 67630 RVA: 0x003B02ED File Offset: 0x003AE4ED
		// (set) Token: 0x0601082F RID: 67631 RVA: 0x003B030E File Offset: 0x003AE50E
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public int MaximumNodes
		{
			get
			{
				return (int)(base.ViewState["MaximumNodes"] ?? 0);
			}
			set
			{
				base.ViewState["MaximumNodes"] = value;
			}
		}

		// Token: 0x1700503A RID: 20538
		// (get) Token: 0x06010830 RID: 67632 RVA: 0x003B0326 File Offset: 0x003AE526
		// (set) Token: 0x06010831 RID: 67633 RVA: 0x003B034B File Offset: 0x003AE54B
		[NotifyParentProperty(true)]
		[Description("The width of the specified level")]
		[DefaultValue(typeof(Unit), "")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700503B RID: 20539
		// (get) Token: 0x06010832 RID: 67634 RVA: 0x003B0363 File Offset: 0x003AE563
		// (set) Token: 0x06010833 RID: 67635 RVA: 0x003B0383 File Offset: 0x003AE583
		[DefaultValue(" |")]
		[NotifyParentProperty(true)]
		public string SeparatorText
		{
			get
			{
				return (string)(base.ViewState["SeparatorText"] ?? " |");
			}
			set
			{
				base.ViewState["SeparatorText"] = value;
			}
		}

		// Token: 0x1700503C RID: 20540
		// (get) Token: 0x06010834 RID: 67636 RVA: 0x003B0396 File Offset: 0x003AE596
		[Description("Specific settings for the List layout mode")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		public SiteMapListLayoutSetting ListLayout
		{
			get
			{
				if (this._listLayout == null)
				{
					this._listLayout = new SiteMapListLayoutSetting("listLayout", base.ViewState);
				}
				return this._listLayout;
			}
		}

		// Token: 0x1700503D RID: 20541
		// (get) Token: 0x06010835 RID: 67637 RVA: 0x003B03BC File Offset: 0x003AE5BC
		// (set) Token: 0x06010836 RID: 67638 RVA: 0x003B03DC File Offset: 0x003AE5DC
		[DefaultValue("")]
		[Description("Specific the URL to an image which is displayed next to all the nodes of a given level")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public string ImageUrl
		{
			get
			{
				return (string)(base.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x1700503E RID: 20542
		// (get) Token: 0x06010837 RID: 67639 RVA: 0x003B03EF File Offset: 0x003AE5EF
		// (set) Token: 0x06010838 RID: 67640 RVA: 0x003B03F7 File Offset: 0x003AE5F7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[TemplateContainer(typeof(RadSiteMapNode))]
		public ITemplate NodeTemplate { get; set; }

		// Token: 0x1700503F RID: 20543
		// (get) Token: 0x06010839 RID: 67641 RVA: 0x003B0400 File Offset: 0x003AE600
		// (set) Token: 0x0601083A RID: 67642 RVA: 0x003B0408 File Offset: 0x003AE608
		[Bindable(false)]
		[Browsable(false)]
		[TemplateContainer(typeof(RadSiteMapNode))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ITemplate SeparatorTemplate { get; set; }

		// Token: 0x040049ED RID: 18925
		private SiteMapListLayoutSetting _listLayout;
	}
}
