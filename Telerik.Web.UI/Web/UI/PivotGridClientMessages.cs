using System;
using System.ComponentModel;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02000DEC RID: 3564
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridClientMessages : StateManager
	{
		// Token: 0x0600845F RID: 33887 RVA: 0x001E2DB4 File Offset: 0x001E0FB4
		public PivotGridClientMessages(RadPivotGrid ownerPivotGrid)
		{
			this._ownerPivotGrid = ownerPivotGrid;
		}

		// Token: 0x170029DB RID: 10715
		// (get) Token: 0x06008460 RID: 33888 RVA: 0x001E2DCC File Offset: 0x001E0FCC
		// (set) Token: 0x06008461 RID: 33889 RVA: 0x001E2E1C File Offset: 0x001E101C
		[NotifyParentProperty(true)]
		[Description("Expand button tooltip")]
		[DefaultValue("Expand")]
		[Category("Client")]
		[Localizable(true)]
		public virtual string ExpandButtonToolTip
		{
			get
			{
				object obj = base.ViewState["ExpandButtonToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((PivotGridStrings loc) => loc.ExpandButtonToolTip, "Expand");
			}
			set
			{
				base.ViewState["ExpandButtonToolTip"] = value;
			}
		}

		// Token: 0x170029DC RID: 10716
		// (get) Token: 0x06008462 RID: 33890 RVA: 0x001E2E38 File Offset: 0x001E1038
		// (set) Token: 0x06008463 RID: 33891 RVA: 0x001E2E88 File Offset: 0x001E1088
		[Description("Collapse button tooltip")]
		[DefaultValue("Collapse")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string CollapseButtonToolTip
		{
			get
			{
				object obj = base.ViewState["CollapseButtonToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((PivotGridStrings loc) => loc.CollapseButtonToolTip, "Collapse");
			}
			set
			{
				base.ViewState["CollapseButtonToolTip"] = value;
			}
		}

		// Token: 0x170029DD RID: 10717
		// (get) Token: 0x06008464 RID: 33892 RVA: 0x001E2EA4 File Offset: 0x001E10A4
		// (set) Token: 0x06008465 RID: 33893 RVA: 0x001E2EF4 File Offset: 0x001E10F4
		[Category("Client")]
		[DefaultValue("Drag to reorder")]
		[Description("Drag to reorder tooltip")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string DragToReorder
		{
			get
			{
				object obj = base.ViewState["DragToReorder"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((PivotGridStrings loc) => loc.DragToReorder, "Drag to reorder");
			}
			set
			{
				base.ViewState["DragToReorder"] = value;
			}
		}

		// Token: 0x170029DE RID: 10718
		// (get) Token: 0x06008466 RID: 33894 RVA: 0x001E2F10 File Offset: 0x001E1110
		// (set) Token: 0x06008467 RID: 33895 RVA: 0x001E2F60 File Offset: 0x001E1160
		[DefaultValue("Drag to resize")]
		[Description("Drag to resize tooltip")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string DragToResize
		{
			get
			{
				object obj = base.ViewState["_dtr"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((PivotGridStrings loc) => loc.DragToResize, "Drag to resize");
			}
			set
			{
				base.ViewState["_dtr"] = value;
			}
		}

		// Token: 0x170029DF RID: 10719
		// (get) Token: 0x06008468 RID: 33896 RVA: 0x001E2F7C File Offset: 0x001E117C
		// (set) Token: 0x06008469 RID: 33897 RVA: 0x001E2FCC File Offset: 0x001E11CC
		[DefaultValue("Width: <strong>{0}</strong> <em>pixels</em>")]
		[Description("the format string used for the tooltip when resizing a column")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Client")]
		public string ColumnResizeTooltipFormatString
		{
			get
			{
				object obj = base.ViewState["ColumnResizeTooltipFormatString"];
				if (obj == null)
				{
					return this.GetLocalizationString((PivotGridStrings loc) => loc.ColumnResizeTooltipFormatString, "Width: <strong>{0}</strong> <em>pixels</em>");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ColumnResizeTooltipFormatString"] = value;
			}
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x001E2FDF File Offset: 0x001E11DF
		private string GetLocalizationString(TFunc<PivotGridStrings, string> extractor, string defaultValue)
		{
			if (this._ownerPivotGrid != null)
			{
				return extractor(this._ownerPivotGrid.Localization);
			}
			return defaultValue;
		}

		// Token: 0x040024C0 RID: 9408
		private readonly RadPivotGrid _ownerPivotGrid;
	}
}
