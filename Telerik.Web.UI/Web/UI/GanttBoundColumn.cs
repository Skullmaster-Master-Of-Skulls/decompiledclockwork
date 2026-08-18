using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Gantt;

namespace Telerik.Web.UI
{
	// Token: 0x020002F5 RID: 757
	public class GanttBoundColumn : StateManager, IBoundColumn, IMarkableStateManager, IStateManager
	{
		// Token: 0x06001A08 RID: 6664 RVA: 0x00054E5F File Offset: 0x0005305F
		public GanttBoundColumn()
		{
			this._required = false;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00054E79 File Offset: 0x00053079
		public GanttBoundColumn(string uniqueName, bool required)
		{
			this._required = required;
			this._uniqueName = uniqueName;
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x00054E9A File Offset: 0x0005309A
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x00054EBA File Offset: 0x000530BA
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("The data field in the underlying datasource that this column represents.")]
		public string DataField
		{
			get
			{
				return (string)(base.ViewState["DataField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00054ECD File Offset: 0x000530CD
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x00054EED File Offset: 0x000530ED
		[Description("Gets or sets the string that specifies the display format for items in the column.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string DataFormatString
		{
			get
			{
				return (string)(base.ViewState["DataFormatString"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00054F00 File Offset: 0x00053100
		// (set) Token: 0x06001A0F RID: 6671 RVA: 0x00054F21 File Offset: 0x00053121
		[Description("The type of the data from the DataField as it is in the DataSource")]
		[TypeConverter(typeof(DataTypeConvertor))]
		[DefaultValue("")]
		[Category("Behavior")]
		public DataType DataType
		{
			get
			{
				return (DataType)(base.ViewState["DataType"] ?? DataType.String);
			}
			set
			{
				base.ViewState["DataType"] = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00054F39 File Offset: 0x00053139
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x00054F59 File Offset: 0x00053159
		[DefaultValue("")]
		[Description("The title text that will be displayed in the column's header")]
		[Localizable(true)]
		[Category("Appearance")]
		public string HeaderText
		{
			get
			{
				return (string)(base.ViewState["HeaderText"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00054F6C File Offset: 0x0005316C
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x00054F8D File Offset: 0x0005318D
		[Category("Behavior")]
		[Description("Value indicating whether sorting is enabled for this column")]
		[DefaultValue(true)]
		public bool AllowSorting
		{
			get
			{
				return (bool)(base.ViewState["AllowSorting"] ?? true);
			}
			set
			{
				base.ViewState["AllowSorting"] = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00054FA5 File Offset: 0x000531A5
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x00054FC6 File Offset: 0x000531C6
		[Description("Value indicating whether editing is enabled for this column")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AllowEdit
		{
			get
			{
				return (bool)(base.ViewState["AllowEditing"] ?? true);
			}
			set
			{
				base.ViewState["AllowEditing"] = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x00054FDE File Offset: 0x000531DE
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x00055008 File Offset: 0x00053208
		[Description("The width of the column")]
		[DefaultValue(150)]
		[Category("Appearance")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Pixel(150));
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00055020 File Offset: 0x00053220
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x00055041 File Offset: 0x00053241
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Value indicating if the column and would be rendered.")]
		public virtual bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00055059 File Offset: 0x00053259
		[Description("Validation settings for the column")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public IColumnValidation Validation
		{
			get
			{
				if (this._validationSettings == null)
				{
					this._validationSettings = new ColumnValidationSettings(this._required);
				}
				return this._validationSettings;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0005507A File Offset: 0x0005327A
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x00055082 File Offset: 0x00053282
		[DefaultValue("")]
		[Description("")]
		[Category("Behavior")]
		public string UniqueName
		{
			get
			{
				return this._uniqueName;
			}
			set
			{
				this._uniqueName = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0005508B File Offset: 0x0005328B
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x000550AB File Offset: 0x000532AB
		[ClientControlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of the RadGantt column.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ClientPropertyName("template")]
		[Browsable(false)]
		public string ClientTemplate
		{
			get
			{
				return (string)(base.ViewState["ClientTemplate"] ?? "");
			}
			set
			{
				base.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000550BE File Offset: 0x000532BE
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x000550DE File Offset: 0x000532DE
		[DefaultValue("")]
		[Browsable(false)]
		[Description("Gets or sets the HTML template of the RadGantt column header.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ClientControlProperty]
		[ClientPropertyName("headerTemplate")]
		public string ClientHeaderTemplate
		{
			get
			{
				return (string)(base.ViewState["ClientHeaderTemplate"] ?? "");
			}
			set
			{
				base.ViewState["ClientHeaderTemplate"] = value;
			}
		}

		// Token: 0x040006B3 RID: 1715
		private IColumnValidation _validationSettings;

		// Token: 0x040006B4 RID: 1716
		private readonly bool _required;

		// Token: 0x040006B5 RID: 1717
		private string _uniqueName = "";
	}
}
