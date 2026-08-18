using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200110D RID: 4365
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridHierarchySettings : ObjectWithState
	{
		// Token: 0x0600B2B4 RID: 45748 RVA: 0x0026DCEB File Offset: 0x0026BEEB
		public GridHierarchySettings(RadGrid ownerGrid, StateBag ownerStateBag) : base("ghs_", ownerStateBag)
		{
			this._ownerGrid = ownerGrid;
		}

		// Token: 0x0600B2B5 RID: 45749 RVA: 0x0026DD00 File Offset: 0x0026BF00
		public GridHierarchySettings(StateBag ownerStateBag) : this(null, ownerStateBag)
		{
		}

		// Token: 0x0600B2B6 RID: 45750 RVA: 0x0026DD0A File Offset: 0x0026BF0A
		private string GetLocalizationString(TFunc<GridStrings, string> extractor, string defaultValue)
		{
			if (this._ownerGrid != null)
			{
				return extractor(this._ownerGrid.Localization);
			}
			return defaultValue;
		}

		// Token: 0x170039E2 RID: 14818
		// (get) Token: 0x0600B2B7 RID: 45751 RVA: 0x0026DD30 File Offset: 0x0026BF30
		// (set) Token: 0x0600B2B8 RID: 45752 RVA: 0x0026DD80 File Offset: 0x0026BF80
		[Localizable(true)]
		[DefaultValue("Expand")]
		[Description("")]
		[NotifyParentProperty(true)]
		public string ExpandTooltip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_extt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.HierarchyExpandTooltip, "Expand");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_extt"] = value;
			}
		}

		// Token: 0x170039E3 RID: 14819
		// (get) Token: 0x0600B2B9 RID: 45753 RVA: 0x0026DD9C File Offset: 0x0026BF9C
		// (set) Token: 0x0600B2BA RID: 45754 RVA: 0x0026DDEC File Offset: 0x0026BFEC
		[DefaultValue("Expand all")]
		[Description("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ExpandAllTooltip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_exatt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.HierarchyExpandAllTooltip, "Expand all");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_exatt"] = value;
			}
		}

		// Token: 0x170039E4 RID: 14820
		// (get) Token: 0x0600B2BB RID: 45755 RVA: 0x0026DE08 File Offset: 0x0026C008
		// (set) Token: 0x0600B2BC RID: 45756 RVA: 0x0026DE58 File Offset: 0x0026C058
		[DefaultValue("Collapse")]
		[Description("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string CollapseTooltip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_cltt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.HierarchyCollapseTooltip, "Collapse");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_cltt"] = value;
			}
		}

		// Token: 0x170039E5 RID: 14821
		// (get) Token: 0x0600B2BD RID: 45757 RVA: 0x0026DE74 File Offset: 0x0026C074
		// (set) Token: 0x0600B2BE RID: 45758 RVA: 0x0026DEC4 File Offset: 0x0026C0C4
		[DefaultValue("Collapse all")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("")]
		public string CollapseAllTooltip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_clatt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.HierarchyCollapseAllTooltip, "Collapse all");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_clatt"] = value;
			}
		}

		// Token: 0x170039E6 RID: 14822
		// (get) Token: 0x0600B2BF RID: 45759 RVA: 0x0026DEE0 File Offset: 0x0026C0E0
		// (set) Token: 0x0600B2C0 RID: 45760 RVA: 0x0026DF30 File Offset: 0x0026C130
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue("Self reference expand")]
		[Localizable(true)]
		public string SelfExpandTooltip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_sextt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.HierarchySelfExpandTooltip, "Self reference expand");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_sextt"] = value;
			}
		}

		// Token: 0x170039E7 RID: 14823
		// (get) Token: 0x0600B2C1 RID: 45761 RVA: 0x0026DF4C File Offset: 0x0026C14C
		// (set) Token: 0x0600B2C2 RID: 45762 RVA: 0x0026DF9C File Offset: 0x0026C19C
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Self reference collapse")]
		[Description("")]
		public string SelfCollapseTooltip
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_scltt"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.HierarchySelfCollapseTooltip, "Self reference collapse");
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_scltt"] = value;
			}
		}

		// Token: 0x04002F18 RID: 12056
		private readonly RadGrid _ownerGrid;
	}
}
