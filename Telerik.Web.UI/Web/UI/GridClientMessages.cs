using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x020010D1 RID: 4305
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridClientMessages : ObjectWithState
	{
		// Token: 0x0600B069 RID: 45161 RVA: 0x002629C0 File Offset: 0x00260BC0
		public GridClientMessages(RadGrid ownerGrid, StateBag ownerStateBag) : base("cs_msg_", ownerStateBag)
		{
			this._ownerGrid = ownerGrid;
		}

		// Token: 0x0600B06A RID: 45162 RVA: 0x002629D5 File Offset: 0x00260BD5
		public GridClientMessages(StateBag ownerStateBag) : this(null, ownerStateBag)
		{
		}

		// Token: 0x0600B06B RID: 45163 RVA: 0x002629DF File Offset: 0x00260BDF
		private string GetLocalizationString(TFunc<GridStrings, string> extractor, string defaultValue)
		{
			if (this._ownerGrid != null)
			{
				return extractor(this._ownerGrid.Localization);
			}
			return defaultValue;
		}

		// Token: 0x1700391E RID: 14622
		// (get) Token: 0x0600B06C RID: 45164 RVA: 0x00262A04 File Offset: 0x00260C04
		// (set) Token: 0x0600B06D RID: 45165 RVA: 0x00262A54 File Offset: 0x00260C54
		[Description("Drop here to reorder tooltip")]
		[DefaultValue("Drop here to reorder")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string DropHereToReorder
		{
			get
			{
				object obj = base.ViewState["_dhtr"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.DropHereToReorder, "Drop here to reorder");
			}
			set
			{
				base.ViewState["_dhtr"] = value;
			}
		}

		// Token: 0x1700391F RID: 14623
		// (get) Token: 0x0600B06E RID: 45166 RVA: 0x00262A70 File Offset: 0x00260C70
		// (set) Token: 0x0600B06F RID: 45167 RVA: 0x00262AC0 File Offset: 0x00260CC0
		[Description("Drag to group or reorder tooltip")]
		[Localizable(true)]
		[DefaultValue("Drag to group or reorder")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual string DragToGroupOrReorder
		{
			get
			{
				object obj = base.ViewState["_dtgr"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.DragToGroupOrReorder, "Drag to group or reorder");
			}
			set
			{
				base.ViewState["_dtgr"] = value;
			}
		}

		// Token: 0x17003920 RID: 14624
		// (get) Token: 0x0600B070 RID: 45168 RVA: 0x00262ADC File Offset: 0x00260CDC
		// (set) Token: 0x0600B071 RID: 45169 RVA: 0x00262B2C File Offset: 0x00260D2C
		[Description("Drag to resize tooltip")]
		[DefaultValue("Drag to resize")]
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
				return this.GetLocalizationString((GridStrings loc) => loc.DragToResize, "Drag to resize");
			}
			set
			{
				base.ViewState["_dtr"] = value;
			}
		}

		// Token: 0x17003921 RID: 14625
		// (get) Token: 0x0600B072 RID: 45170 RVA: 0x00262B48 File Offset: 0x00260D48
		// (set) Token: 0x0600B073 RID: 45171 RVA: 0x00262B98 File Offset: 0x00260D98
		[Localizable(true)]
		[Category("Client")]
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
		[NotifyParentProperty(true)]
		[Description("The format string used for the tooltip when using Ajax scroll paging or the Slider pager")]
		public string PagerTooltipFormatString
		{
			get
			{
				object obj = base.ViewState["PagerTooltipFormatString"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.PagerTooltipFormatString, "Page <strong>{0}</strong> of <strong>{1}</strong>");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PagerTooltipFormatString"] = value;
			}
		}

		// Token: 0x17003922 RID: 14626
		// (get) Token: 0x0600B074 RID: 45172 RVA: 0x00262BB4 File Offset: 0x00260DB4
		// (set) Token: 0x0600B075 RID: 45173 RVA: 0x00262C04 File Offset: 0x00260E04
		[Localizable(true)]
		[Category("Client")]
		[DefaultValue("Width: <strong>{0}</strong> <em>pixels</em>")]
		[NotifyParentProperty(true)]
		[Description("he format string used for the tooltip when resizing a column")]
		public string ColumnResizeTooltipFormatString
		{
			get
			{
				object obj = base.ViewState["ColumnResizeTooltipFormatString"];
				if (obj == null)
				{
					return this.GetLocalizationString((GridStrings loc) => loc.ColumnResizeTooltipFormatString, "Width: <strong>{0}</strong> <em>pixels</em>");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ColumnResizeTooltipFormatString"] = value;
			}
		}

		// Token: 0x04002E53 RID: 11859
		private readonly RadGrid _ownerGrid;
	}
}
