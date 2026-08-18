using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000C42 RID: 3138
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridConfigurationPanelSettings : StateManager
	{
		// Token: 0x060076A7 RID: 30375 RVA: 0x001B8C7E File Offset: 0x001B6E7E
		public PivotGridConfigurationPanelSettings(RadPivotGrid owner)
		{
			this.owner = owner;
		}

		// Token: 0x17002697 RID: 9879
		// (get) Token: 0x060076A8 RID: 30376 RVA: 0x001B8C8D File Offset: 0x001B6E8D
		// (set) Token: 0x060076A9 RID: 30377 RVA: 0x001B8CAE File Offset: 0x001B6EAE
		[Description("Enables\\disables if the drag drop in the configuration panel will be enabled.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue(true)]
		public bool EnableDragDrop
		{
			get
			{
				return (bool)(base.ViewState["EnableDragDrop"] ?? true);
			}
			set
			{
				base.ViewState["EnableDragDrop"] = value;
			}
		}

		// Token: 0x17002698 RID: 9880
		// (get) Token: 0x060076AA RID: 30378 RVA: 0x001B8CC6 File Offset: 0x001B6EC6
		// (set) Token: 0x060076AB RID: 30379 RVA: 0x001B8CE7 File Offset: 0x001B6EE7
		[Description("Enables\\disables if a context menu will be displayed when right clicking fields in the configuration panel.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public bool EnableFieldsContextMenu
		{
			get
			{
				return (bool)(base.ViewState["EnableFieldsContextMenu"] ?? true);
			}
			set
			{
				base.ViewState["EnableFieldsContextMenu"] = value;
			}
		}

		// Token: 0x17002699 RID: 9881
		// (get) Token: 0x060076AC RID: 30380 RVA: 0x001B8CFF File Offset: 0x001B6EFF
		// (set) Token: 0x060076AD RID: 30381 RVA: 0x001B8D20 File Offset: 0x001B6F20
		[DefaultValue(false)]
		[Category("Client")]
		[Description("Determines if the changes will be applied after every operation or only when clicking the Update button.")]
		[NotifyParentProperty(true)]
		public bool DefaultDeferedLayoutUpdate
		{
			get
			{
				return (bool)(base.ViewState["DeferedLayoutUpdate"] ?? false);
			}
			set
			{
				base.ViewState["DeferedLayoutUpdate"] = value;
			}
		}

		// Token: 0x1700269A RID: 9882
		// (get) Token: 0x060076AE RID: 30382 RVA: 0x001B8D38 File Offset: 0x001B6F38
		// (set) Token: 0x060076AF RID: 30383 RVA: 0x001B8D61 File Offset: 0x001B6F61
		[Bindable(true)]
		[Description("Gets or set a value indicating where the ConfigurationPanel will be places relative to the pivot grid.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(typeof(PivotGridConfigurationPanelPosition), "FieldsWindow")]
		public PivotGridConfigurationPanelPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj != null)
				{
					return (PivotGridConfigurationPanelPosition)obj;
				}
				return PivotGridConfigurationPanelPosition.FieldsWindow;
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x060076B0 RID: 30384 RVA: 0x001B8D7C File Offset: 0x001B6F7C
		// (set) Token: 0x060076B1 RID: 30385 RVA: 0x001B8DA5 File Offset: 0x001B6FA5
		[NotifyParentProperty(true)]
		[SimplePersistenceSetting]
		[Category("Behavior")]
		[Bindable(true)]
		[DefaultValue(PivotGridConfigurationPanelLayoutType.Stacked)]
		[Description("Gets or sets a value indicating whether the row header zone of the pivotgrid will be shown.")]
		public PivotGridConfigurationPanelLayoutType LayoutType
		{
			get
			{
				object obj = base.ViewState["LayoutType"];
				if (obj != null)
				{
					return (PivotGridConfigurationPanelLayoutType)obj;
				}
				return PivotGridConfigurationPanelLayoutType.Stacked;
			}
			set
			{
				base.ViewState["LayoutType"] = value;
			}
		}

		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x060076B2 RID: 30386 RVA: 0x001B8DC0 File Offset: 0x001B6FC0
		// (set) Token: 0x060076B3 RID: 30387 RVA: 0x001B8DF8 File Offset: 0x001B6FF8
		[Bindable(true)]
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public string ShowHideCheckBoxToolTip
		{
			get
			{
				object obj = base.ViewState["ShowHideCheckBoxToolTip"];
				if (obj == null)
				{
					return this.owner.Localization.ShowHideCheckBoxToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ShowHideCheckBoxToolTip"] = value;
			}
		}

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x060076B4 RID: 30388 RVA: 0x001B8E0B File Offset: 0x001B700B
		// (set) Token: 0x060076B5 RID: 30389 RVA: 0x001B8E2C File Offset: 0x001B702C
		[Description("A value indicating if the RadTreeView will use a Load-on-demand to load its nodes.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public bool EnableOlapTreeViewLoadOnDemand
		{
			get
			{
				return (bool)(base.ViewState["EnableOlapTreeViewLoadOnDemand"] ?? true);
			}
			set
			{
				base.ViewState["EnableOlapTreeViewLoadOnDemand"] = value;
			}
		}

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x060076B6 RID: 30390 RVA: 0x001B8E44 File Offset: 0x001B7044
		// (set) Token: 0x060076B7 RID: 30391 RVA: 0x001B8E7C File Offset: 0x001B707C
		[Category("Appearance")]
		[Description("The name of category under which all uncategorized fields are put.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string OlapUncategorizedFolderName
		{
			get
			{
				object obj = base.ViewState["OlapUncategorizedFolderName"];
				if (obj == null)
				{
					return this.owner.Localization.OlapUncategorizedFolderName;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["OlapUncategorizedFolderName"] = value;
			}
		}

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x060076B8 RID: 30392 RVA: 0x001B8E8F File Offset: 0x001B708F
		// (set) Token: 0x060076B9 RID: 30393 RVA: 0x001B8EB0 File Offset: 0x001B70B0
		[Category("Appearance")]
		[Description("Gets or sets a value indicating if all uncategorized fields coming from OLAP cube will be put under category folder or rendered directly as children")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool FlattenOlapUncategoriezedFields
		{
			get
			{
				return (bool)(base.ViewState["FlattenOlapUncategoriezedFields"] ?? false);
			}
			set
			{
				base.ViewState["FlattenOlapUncategoriezedFields"] = value;
			}
		}

		// Token: 0x040020A3 RID: 8355
		private readonly RadPivotGrid owner;
	}
}
