using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200096F RID: 2415
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListCommandItemSettings : StateManager
	{
		// Token: 0x06005BCE RID: 23502 RVA: 0x00118218 File Offset: 0x00116418
		public TreeListCommandItemSettings(RadTreeList owner)
		{
			this._owner = owner;
		}

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x06005BCF RID: 23503 RVA: 0x00118228 File Offset: 0x00116428
		// (set) Token: 0x06005BD0 RID: 23504 RVA: 0x00118251 File Offset: 0x00116451
		[DefaultValue(true)]
		[Description("Determines whether the export to Excel button will be shown in the command item.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool ShowExportToExcelButton
		{
			get
			{
				object obj = base.ViewState["ShowExportToExcelButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowExportToExcelButton"] = value;
			}
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x06005BD1 RID: 23505 RVA: 0x0011826C File Offset: 0x0011646C
		// (set) Token: 0x06005BD2 RID: 23506 RVA: 0x00118295 File Offset: 0x00116495
		[Description("Determines whether the export to Word button will be shown in the command item.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool ShowExportToWordButton
		{
			get
			{
				object obj = base.ViewState["ShowExportToWordButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowExportToWordButton"] = value;
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x06005BD3 RID: 23507 RVA: 0x001182B0 File Offset: 0x001164B0
		// (set) Token: 0x06005BD4 RID: 23508 RVA: 0x001182D9 File Offset: 0x001164D9
		[Category("Behavior")]
		[Description("Determines whether the export to Pdf button will be shown in the command item.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool ShowExportToPdfButton
		{
			get
			{
				object obj = base.ViewState["ShowExportToPdfButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowExportToPdfButton"] = value;
			}
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x06005BD5 RID: 23509 RVA: 0x001182F1 File Offset: 0x001164F1
		// (set) Token: 0x06005BD6 RID: 23510 RVA: 0x0011832B File Offset: 0x0011652B
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Export to Excel")]
		[Description("Gets or sets text which will be used for the tooltip of the export button. The default value is 'Export to Excel'")]
		public string ExportToExcelText
		{
			get
			{
				if (base.ViewState["ExportToExcelText"] == null)
				{
					return this._owner.Localization.ExportToExcelText;
				}
				return (string)base.ViewState["ExportToExcelText"];
			}
			set
			{
				base.ViewState["ExportToExcelText"] = value;
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x06005BD7 RID: 23511 RVA: 0x0011833E File Offset: 0x0011653E
		// (set) Token: 0x06005BD8 RID: 23512 RVA: 0x00118378 File Offset: 0x00116578
		[DefaultValue("Export to Word")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets text which will be used for the tooltip of the export button. The default value is 'Export to Word'")]
		public string ExportToWordText
		{
			get
			{
				if (base.ViewState["ExportToWordText"] == null)
				{
					return this._owner.Localization.ExportToWordText;
				}
				return (string)base.ViewState["ExportToWordText"];
			}
			set
			{
				base.ViewState["ExportToWordText"] = value;
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x06005BD9 RID: 23513 RVA: 0x0011838B File Offset: 0x0011658B
		// (set) Token: 0x06005BDA RID: 23514 RVA: 0x001183C5 File Offset: 0x001165C5
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Export to PDF")]
		[Description("Gets or sets text which will be used for the tooltip of the export button. The default value is 'Export to PDF'")]
		public string ExportToPdfText
		{
			get
			{
				if (base.ViewState["ExportToPdfText"] == null)
				{
					return this._owner.Localization.ExportToPdfText;
				}
				return (string)base.ViewState["ExportToPdfText"];
			}
			set
			{
				base.ViewState["ExportToPdfText"] = value;
			}
		}

		// Token: 0x04001612 RID: 5650
		private readonly RadTreeList _owner;
	}
}
