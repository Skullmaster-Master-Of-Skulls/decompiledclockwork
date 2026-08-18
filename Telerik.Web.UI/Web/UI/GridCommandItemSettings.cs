using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010E5 RID: 4325
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridCommandItemSettings : ObjectWithState
	{
		// Token: 0x0600B0F7 RID: 45303 RVA: 0x00265383 File Offset: 0x00263583
		public GridCommandItemSettings(StateBag ownerStateBag, GridTableView owner) : base("gcis_", ownerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x1700394F RID: 14671
		// (get) Token: 0x0600B0F8 RID: 45304 RVA: 0x00265398 File Offset: 0x00263598
		private GridStrings Localization
		{
			get
			{
				return this.owner.OwnerGrid.Localization;
			}
		}

		// Token: 0x17003950 RID: 14672
		// (get) Token: 0x0600B0F9 RID: 45305 RVA: 0x002653AA File Offset: 0x002635AA
		// (set) Token: 0x0600B0FA RID: 45306 RVA: 0x002653DF File Offset: 0x002635DF
		[DefaultValue("Add new record")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets text which will be used for the AddNewRecordButton. The default value is 'Add New Record'.")]
		public string AddNewRecordText
		{
			get
			{
				if (base.ViewState["AddNewRecordText"] == null)
				{
					return this.Localization.AddNewRecordText;
				}
				return (string)base.ViewState["AddNewRecordText"];
			}
			set
			{
				base.ViewState["AddNewRecordText"] = value;
			}
		}

		// Token: 0x17003951 RID: 14673
		// (get) Token: 0x0600B0FB RID: 45307 RVA: 0x002653F2 File Offset: 0x002635F2
		// (set) Token: 0x0600B0FC RID: 45308 RVA: 0x00265427 File Offset: 0x00263627
		[NotifyParentProperty(true)]
		[Description("Gets or sets text which will be used for the SaveChanges Button. The default value is 'Save Changes'.")]
		[Localizable(true)]
		[DefaultValue("Save changes")]
		public string SaveChangesText
		{
			get
			{
				if (base.ViewState["SaveChangesText"] == null)
				{
					return this.Localization.SaveChangesText;
				}
				return (string)base.ViewState["SaveChangesText"];
			}
			set
			{
				base.ViewState["SaveChangesText"] = value;
			}
		}

		// Token: 0x17003952 RID: 14674
		// (get) Token: 0x0600B0FD RID: 45309 RVA: 0x0026543A File Offset: 0x0026363A
		// (set) Token: 0x0600B0FE RID: 45310 RVA: 0x0026546F File Offset: 0x0026366F
		[DefaultValue("Cancel changes")]
		[Localizable(true)]
		[Description("Gets or sets text which will be used for the CancelChanges Button. The default value is 'Cancel Changes'.")]
		[NotifyParentProperty(true)]
		public string CancelChangesText
		{
			get
			{
				if (base.ViewState["CancelChangesText"] == null)
				{
					return this.Localization.CancelChangesText;
				}
				return (string)base.ViewState["CancelChangesText"];
			}
			set
			{
				base.ViewState["CancelChangesText"] = value;
			}
		}

		// Token: 0x17003953 RID: 14675
		// (get) Token: 0x0600B0FF RID: 45311 RVA: 0x00265482 File Offset: 0x00263682
		// (set) Token: 0x0600B100 RID: 45312 RVA: 0x002654B7 File Offset: 0x002636B7
		[Localizable(true)]
		[DefaultValue("Refresh")]
		[Description("Gets or sets text which will be used for the Refresh Button. The default value is 'Refresh'")]
		[NotifyParentProperty(true)]
		public string RefreshText
		{
			get
			{
				if (base.ViewState["RefreshText"] == null)
				{
					return this.Localization.Refresh;
				}
				return (string)base.ViewState["RefreshText"];
			}
			set
			{
				base.ViewState["RefreshText"] = value;
			}
		}

		// Token: 0x17003954 RID: 14676
		// (get) Token: 0x0600B101 RID: 45313 RVA: 0x002654CA File Offset: 0x002636CA
		// (set) Token: 0x0600B102 RID: 45314 RVA: 0x002654FF File Offset: 0x002636FF
		[NotifyParentProperty(true)]
		[DefaultValue("Prev")]
		[Localizable(true)]
		[Description("")]
		public string PrevFrozenColumnText
		{
			get
			{
				if (base.ViewState["PrevFrozenColumnText"] == null)
				{
					return this.Localization.PrevFrozenColumnText;
				}
				return (string)base.ViewState["PrevFrozenColumnText"];
			}
			set
			{
				base.ViewState["PrevFrozenColumnText"] = value;
			}
		}

		// Token: 0x17003955 RID: 14677
		// (get) Token: 0x0600B103 RID: 45315 RVA: 0x00265512 File Offset: 0x00263712
		// (set) Token: 0x0600B104 RID: 45316 RVA: 0x00265547 File Offset: 0x00263747
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Next")]
		[Description("")]
		public string NextFrozenColumnText
		{
			get
			{
				if (base.ViewState["NextFrozenColumnText"] == null)
				{
					return this.Localization.NextFrozenColumnText;
				}
				return (string)base.ViewState["NextFrozenColumnText"];
			}
			set
			{
				base.ViewState["NextFrozenColumnText"] = value;
			}
		}

		// Token: 0x17003956 RID: 14678
		// (get) Token: 0x0600B105 RID: 45317 RVA: 0x0026555A File Offset: 0x0026375A
		// (set) Token: 0x0600B106 RID: 45318 RVA: 0x0026558F File Offset: 0x0026378F
		[NotifyParentProperty(true)]
		[DefaultValue("Export to Excel")]
		[Localizable(true)]
		[Description("Gets or sets text which will be used for the ExportToExcel Button. The default value is 'Export To Excel'")]
		public string ExportToExcelText
		{
			get
			{
				if (base.ViewState["ExportToExcelText"] == null)
				{
					return this.Localization.ExportToExcelText;
				}
				return (string)base.ViewState["ExportToExcelText"];
			}
			set
			{
				base.ViewState["ExportToExcelText"] = value;
			}
		}

		// Token: 0x17003957 RID: 14679
		// (get) Token: 0x0600B107 RID: 45319 RVA: 0x002655A2 File Offset: 0x002637A2
		// (set) Token: 0x0600B108 RID: 45320 RVA: 0x002655D7 File Offset: 0x002637D7
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets text which will be used for the ExportToWord Button. The default value is 'Export To Word'.")]
		[DefaultValue("Export to Word")]
		public string ExportToWordText
		{
			get
			{
				if (base.ViewState["ExportToWordText"] == null)
				{
					return this.Localization.ExportToWordText;
				}
				return (string)base.ViewState["ExportToWordText"];
			}
			set
			{
				base.ViewState["ExportToWordText"] = value;
			}
		}

		// Token: 0x17003958 RID: 14680
		// (get) Token: 0x0600B109 RID: 45321 RVA: 0x002655EA File Offset: 0x002637EA
		// (set) Token: 0x0600B10A RID: 45322 RVA: 0x0026561F File Offset: 0x0026381F
		[NotifyParentProperty(true)]
		[Description("Gets or sets text which will be used for the ExportToPdf Button. The default value is 'Export To Pdf'.")]
		[DefaultValue("Export to PDF")]
		[Localizable(true)]
		public string ExportToPdfText
		{
			get
			{
				if (base.ViewState["ExportToPdfText"] == null)
				{
					return this.Localization.ExportToPdfText;
				}
				return (string)base.ViewState["ExportToPdfText"];
			}
			set
			{
				base.ViewState["ExportToPdfText"] = value;
			}
		}

		// Token: 0x17003959 RID: 14681
		// (get) Token: 0x0600B10B RID: 45323 RVA: 0x00265632 File Offset: 0x00263832
		// (set) Token: 0x0600B10C RID: 45324 RVA: 0x00265667 File Offset: 0x00263867
		[Localizable(true)]
		[DefaultValue("Export to CSV")]
		[Description("The Export To CSV button text. Default value Export to CSV")]
		[NotifyParentProperty(true)]
		public string ExportToCsvText
		{
			get
			{
				if (base.ViewState["ExportToCsvText"] == null)
				{
					return this.Localization.ExportToCsvText;
				}
				return (string)base.ViewState["ExportToCsvText"];
			}
			set
			{
				base.ViewState["ExportToCsvText"] = value;
			}
		}

		// Token: 0x1700395A RID: 14682
		// (get) Token: 0x0600B10D RID: 45325 RVA: 0x0026567A File Offset: 0x0026387A
		// (set) Token: 0x0600B10E RID: 45326 RVA: 0x002656AF File Offset: 0x002638AF
		[Localizable(true)]
		[DefaultValue("Print RadGrid")]
		[Description("The Pring Grid button text. Default value Print RadGrid")]
		[NotifyParentProperty(true)]
		public string PrintGridText
		{
			get
			{
				if (base.ViewState["PrintGridText"] == null)
				{
					return this.Localization.PrintGridText;
				}
				return (string)base.ViewState["PrintGridText"];
			}
			set
			{
				base.ViewState["PrintGridText"] = value;
			}
		}

		// Token: 0x1700395B RID: 14683
		// (get) Token: 0x0600B10F RID: 45327 RVA: 0x002656C4 File Offset: 0x002638C4
		// (set) Token: 0x0600B110 RID: 45328 RVA: 0x00265745 File Offset: 0x00263945
		[DefaultValue("")]
		[UrlProperty]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("The Add New Record image URL")]
		public virtual string AddNewRecordImageUrl
		{
			get
			{
				object obj = base.ViewState["AddNewRecordImageUrl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return string.Empty;
				}
				string addNewRecordImageUrl = this.Localization.AddNewRecordImageUrl;
				if (!string.IsNullOrEmpty(addNewRecordImageUrl))
				{
					return this.owner.OwnerGrid.ResolveUrl(addNewRecordImageUrl);
				}
				return this.owner.OwnerGrid.ResolveGridImageUrl("AddRecord.gif");
			}
			set
			{
				base.ViewState["AddNewRecordImageUrl"] = value;
			}
		}

		// Token: 0x0600B111 RID: 45329 RVA: 0x00265758 File Offset: 0x00263958
		protected virtual bool ShouldSerializeAddNewRecordImageUrl()
		{
			return this.owner != null && this.owner.OwnerGrid.ShouldSerializeImageUrl(this.AddNewRecordImageUrl);
		}

		// Token: 0x1700395C RID: 14684
		// (get) Token: 0x0600B112 RID: 45330 RVA: 0x0026577C File Offset: 0x0026397C
		// (set) Token: 0x0600B113 RID: 45331 RVA: 0x002657FD File Offset: 0x002639FD
		[UrlProperty]
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("The Refresh image URL")]
		public virtual string RefreshImageUrl
		{
			get
			{
				object obj = base.ViewState["RefreshImageUrl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return string.Empty;
				}
				string refreshImageUrl = this.Localization.RefreshImageUrl;
				if (!string.IsNullOrEmpty(refreshImageUrl))
				{
					return this.owner.OwnerGrid.ResolveUrl(refreshImageUrl);
				}
				return this.owner.OwnerGrid.ResolveGridImageUrl("Refresh.gif");
			}
			set
			{
				base.ViewState["RefreshImageUrl"] = value;
			}
		}

		// Token: 0x0600B114 RID: 45332 RVA: 0x00265810 File Offset: 0x00263A10
		protected virtual bool ShouldSerializeRefreshImageUrl()
		{
			return this.owner != null && this.owner.OwnerGrid.ShouldSerializeImageUrl(this.RefreshImageUrl);
		}

		// Token: 0x1700395D RID: 14685
		// (get) Token: 0x0600B115 RID: 45333 RVA: 0x00265834 File Offset: 0x00263A34
		// (set) Token: 0x0600B116 RID: 45334 RVA: 0x002658B5 File Offset: 0x00263AB5
		[DefaultValue("")]
		[UrlProperty]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("The Export To Excel image URL")]
		public virtual string ExportToExcelImageUrl
		{
			get
			{
				object obj = base.ViewState["ExportToExcelImageUrl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return string.Empty;
				}
				string exportToExcelImageUrl = this.Localization.ExportToExcelImageUrl;
				if (!string.IsNullOrEmpty(exportToExcelImageUrl))
				{
					return this.owner.OwnerGrid.ResolveUrl(exportToExcelImageUrl);
				}
				return this.owner.OwnerGrid.ResolveGridImageUrl("ExportToExcel.gif");
			}
			set
			{
				base.ViewState["ExportToExcelImageUrl"] = value;
			}
		}

		// Token: 0x1700395E RID: 14686
		// (get) Token: 0x0600B117 RID: 45335 RVA: 0x002658C8 File Offset: 0x00263AC8
		// (set) Token: 0x0600B118 RID: 45336 RVA: 0x00265949 File Offset: 0x00263B49
		[Localizable(true)]
		[DefaultValue("")]
		[UrlProperty]
		[NotifyParentProperty(true)]
		[Description("The Export To Word Button image URL.")]
		public virtual string ExportToWordImageUrl
		{
			get
			{
				object obj = base.ViewState["ExportToWordImageUrl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return string.Empty;
				}
				string exportToWordImageUrl = this.Localization.ExportToWordImageUrl;
				if (!string.IsNullOrEmpty(exportToWordImageUrl))
				{
					return this.owner.OwnerGrid.ResolveUrl(exportToWordImageUrl);
				}
				return this.owner.OwnerGrid.ResolveGridImageUrl("ExportToWord.gif");
			}
			set
			{
				base.ViewState["ExportToWordImageUrl"] = value;
			}
		}

		// Token: 0x1700395F RID: 14687
		// (get) Token: 0x0600B119 RID: 45337 RVA: 0x0026595C File Offset: 0x00263B5C
		// (set) Token: 0x0600B11A RID: 45338 RVA: 0x002659DD File Offset: 0x00263BDD
		[DefaultValue("")]
		[UrlProperty]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("The Export To Pdf image URL")]
		public virtual string ExportToPdfImageUrl
		{
			get
			{
				object obj = base.ViewState["ExportToPdfImageUrl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return string.Empty;
				}
				string exportToPdfImageUrl = this.Localization.ExportToPdfImageUrl;
				if (!string.IsNullOrEmpty(exportToPdfImageUrl))
				{
					return this.owner.OwnerGrid.ResolveUrl(exportToPdfImageUrl);
				}
				return this.owner.OwnerGrid.ResolveGridImageUrl("ExportToPdf.gif");
			}
			set
			{
				base.ViewState["ExportToPdfImageUrl"] = value;
			}
		}

		// Token: 0x17003960 RID: 14688
		// (get) Token: 0x0600B11B RID: 45339 RVA: 0x002659F0 File Offset: 0x00263BF0
		// (set) Token: 0x0600B11C RID: 45340 RVA: 0x00265A71 File Offset: 0x00263C71
		[Description("The Export To CSV image URL")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[UrlProperty]
		[Localizable(true)]
		public virtual string ExportToCsvImageUrl
		{
			get
			{
				object obj = base.ViewState["ExportToCsvImageUrl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.owner == null)
				{
					return string.Empty;
				}
				string exportToCsvImageUrl = this.Localization.ExportToCsvImageUrl;
				if (!string.IsNullOrEmpty(exportToCsvImageUrl))
				{
					return this.owner.OwnerGrid.ResolveUrl(exportToCsvImageUrl);
				}
				return this.owner.OwnerGrid.ResolveGridImageUrl("ExportToCsv.gif");
			}
			set
			{
				base.ViewState["ExportToCsvImageUrl"] = value;
			}
		}

		// Token: 0x17003961 RID: 14689
		// (get) Token: 0x0600B11D RID: 45341 RVA: 0x00265A84 File Offset: 0x00263C84
		// (set) Token: 0x0600B11E RID: 45342 RVA: 0x00265AAF File Offset: 0x00263CAF
		[NotifyParentProperty(true)]
		[Description("Indicates whether the default command item should expose the Add New Record button")]
		[DefaultValue(true)]
		public virtual bool ShowAddNewRecordButton
		{
			get
			{
				return base.ViewState["ShowAddNewRecordButton"] == null || (bool)base.ViewState["ShowAddNewRecordButton"];
			}
			set
			{
				base.ViewState["ShowAddNewRecordButton"] = value;
			}
		}

		// Token: 0x17003962 RID: 14690
		// (get) Token: 0x0600B11F RID: 45343 RVA: 0x00265AC7 File Offset: 0x00263CC7
		internal bool IsShowSaveChangesButtonSet
		{
			get
			{
				return base.ViewState["ShowSaveChangesButton"] != null;
			}
		}

		// Token: 0x17003963 RID: 14691
		// (get) Token: 0x0600B120 RID: 45344 RVA: 0x00265ADF File Offset: 0x00263CDF
		// (set) Token: 0x0600B121 RID: 45345 RVA: 0x00265B0A File Offset: 0x00263D0A
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Indicates whether the default command item should show the SaveChanges button")]
		public virtual bool ShowSaveChangesButton
		{
			get
			{
				return base.ViewState["ShowSaveChangesButton"] != null && (bool)base.ViewState["ShowSaveChangesButton"];
			}
			set
			{
				base.ViewState["ShowSaveChangesButton"] = value;
			}
		}

		// Token: 0x17003964 RID: 14692
		// (get) Token: 0x0600B122 RID: 45346 RVA: 0x00265B22 File Offset: 0x00263D22
		internal bool IsShowCancelChangesButtonSet
		{
			get
			{
				return base.ViewState["ShowCancelChangesButton"] != null;
			}
		}

		// Token: 0x17003965 RID: 14693
		// (get) Token: 0x0600B123 RID: 45347 RVA: 0x00265B3A File Offset: 0x00263D3A
		// (set) Token: 0x0600B124 RID: 45348 RVA: 0x00265B65 File Offset: 0x00263D65
		[NotifyParentProperty(true)]
		[Description("Indicates whether the default command item should show the CancelChanges button")]
		[DefaultValue(false)]
		public virtual bool ShowCancelChangesButton
		{
			get
			{
				return base.ViewState["ShowCancelChangesButton"] != null && (bool)base.ViewState["ShowCancelChangesButton"];
			}
			set
			{
				base.ViewState["ShowCancelChangesButton"] = value;
			}
		}

		// Token: 0x17003966 RID: 14694
		// (get) Token: 0x0600B125 RID: 45349 RVA: 0x00265B7D File Offset: 0x00263D7D
		// (set) Token: 0x0600B126 RID: 45350 RVA: 0x00265BA8 File Offset: 0x00263DA8
		[Description("Indicates whether the default command item should expose the Refresh button")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool ShowRefreshButton
		{
			get
			{
				return base.ViewState["ShowRefreshButton"] == null || (bool)base.ViewState["ShowRefreshButton"];
			}
			set
			{
				base.ViewState["ShowRefreshButton"] = value;
			}
		}

		// Token: 0x17003967 RID: 14695
		// (get) Token: 0x0600B127 RID: 45351 RVA: 0x00265BC0 File Offset: 0x00263DC0
		// (set) Token: 0x0600B128 RID: 45352 RVA: 0x00265BEB File Offset: 0x00263DEB
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Indicates whether the default command item should expose Export to Excel button")]
		public virtual bool ShowExportToExcelButton
		{
			get
			{
				return base.ViewState["ShowExportToExcelButton"] != null && (bool)base.ViewState["ShowExportToExcelButton"];
			}
			set
			{
				base.ViewState["ShowExportToExcelButton"] = value;
			}
		}

		// Token: 0x17003968 RID: 14696
		// (get) Token: 0x0600B129 RID: 45353 RVA: 0x00265C03 File Offset: 0x00263E03
		// (set) Token: 0x0600B12A RID: 45354 RVA: 0x00265C2E File Offset: 0x00263E2E
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Indicates whether the default command item should expose Export to Word button")]
		public virtual bool ShowExportToWordButton
		{
			get
			{
				return base.ViewState["ShowExportToWordButton"] != null && (bool)base.ViewState["ShowExportToWordButton"];
			}
			set
			{
				base.ViewState["ShowExportToWordButton"] = value;
			}
		}

		// Token: 0x17003969 RID: 14697
		// (get) Token: 0x0600B12B RID: 45355 RVA: 0x00265C46 File Offset: 0x00263E46
		// (set) Token: 0x0600B12C RID: 45356 RVA: 0x00265C71 File Offset: 0x00263E71
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Indicates whether the default command item should expose Export to PDF button")]
		public virtual bool ShowExportToPdfButton
		{
			get
			{
				return base.ViewState["ShowExportToPdfButton"] != null && (bool)base.ViewState["ShowExportToPdfButton"];
			}
			set
			{
				base.ViewState["ShowExportToPdfButton"] = value;
			}
		}

		// Token: 0x1700396A RID: 14698
		// (get) Token: 0x0600B12D RID: 45357 RVA: 0x00265C89 File Offset: 0x00263E89
		// (set) Token: 0x0600B12E RID: 45358 RVA: 0x00265CB4 File Offset: 0x00263EB4
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Indicates whether the default command item should expose Export to CSV button")]
		public virtual bool ShowExportToCsvButton
		{
			get
			{
				return base.ViewState["ShowExportToCsvButton"] != null && (bool)base.ViewState["ShowExportToCsvButton"];
			}
			set
			{
				base.ViewState["ShowExportToCsvButton"] = value;
			}
		}

		// Token: 0x1700396B RID: 14699
		// (get) Token: 0x0600B12F RID: 45359 RVA: 0x00265CCC File Offset: 0x00263ECC
		// (set) Token: 0x0600B130 RID: 45360 RVA: 0x00265CF7 File Offset: 0x00263EF7
		[Description("Indicates whether the default command item should expose a print button.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool ShowPrintButton
		{
			get
			{
				return base.ViewState["ShowPrintButton"] != null && (bool)base.ViewState["ShowPrintButton"];
			}
			set
			{
				base.ViewState["ShowPrintButton"] = value;
			}
		}

		// Token: 0x04002E7D RID: 11901
		private readonly GridTableView owner;
	}
}
