using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001905 RID: 6405
	internal class GridStrings : LocalizationStrings
	{
		// Token: 0x0600F73A RID: 63290 RVA: 0x003813FB File Offset: 0x0037F5FB
		public GridStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x0600F73B RID: 63291 RVA: 0x0038140B File Offset: 0x0037F60B
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x17004A79 RID: 19065
		// (get) Token: 0x0600F73C RID: 63292 RVA: 0x00381424 File Offset: 0x0037F624
		// (set) Token: 0x0600F73D RID: 63293 RVA: 0x00381431 File Offset: 0x0037F631
		[DefaultValue("Filter")]
		[NotifyParentProperty(true)]
		public string FilterImageToolTip
		{
			get
			{
				return this.GetString("FilterImageToolTip");
			}
			set
			{
				this.SetString("FilterImageToolTip", value);
			}
		}

		// Token: 0x17004A7A RID: 19066
		// (get) Token: 0x0600F73E RID: 63294 RVA: 0x0038143F File Offset: 0x0037F63F
		// (set) Token: 0x0600F73F RID: 63295 RVA: 0x0038144C File Offset: 0x0037F64C
		[DefaultValue("From: ")]
		[NotifyParentProperty(true)]
		public string RangeFilteringFromText
		{
			get
			{
				return this.GetString("RangeFilteringFromText");
			}
			set
			{
				this.SetString("RangeFilteringFromText", value);
			}
		}

		// Token: 0x17004A7B RID: 19067
		// (get) Token: 0x0600F740 RID: 63296 RVA: 0x0038145A File Offset: 0x0037F65A
		// (set) Token: 0x0600F741 RID: 63297 RVA: 0x00381467 File Offset: 0x0037F667
		[NotifyParentProperty(true)]
		[DefaultValue("To: ")]
		public string RangeFilteringToText
		{
			get
			{
				return this.GetString("RangeFilteringToText");
			}
			set
			{
				this.SetString("RangeFilteringToText", value);
			}
		}

		// Token: 0x17004A7C RID: 19068
		// (get) Token: 0x0600F742 RID: 63298 RVA: 0x00381475 File Offset: 0x0037F675
		// (set) Token: 0x0600F743 RID: 63299 RVA: 0x00381482 File Offset: 0x0037F682
		[DefaultValue("Edit")]
		[NotifyParentProperty(true)]
		public string EditText
		{
			get
			{
				return this.GetString("EditText");
			}
			set
			{
				this.SetString("EditText", value);
			}
		}

		// Token: 0x17004A7D RID: 19069
		// (get) Token: 0x0600F744 RID: 63300 RVA: 0x00381490 File Offset: 0x0037F690
		// (set) Token: 0x0600F745 RID: 63301 RVA: 0x0038149D File Offset: 0x0037F69D
		[DefaultValue("Insert")]
		[NotifyParentProperty(true)]
		public string InsertText
		{
			get
			{
				return this.GetString("InsertText");
			}
			set
			{
				this.SetString("InsertText", value);
			}
		}

		// Token: 0x17004A7E RID: 19070
		// (get) Token: 0x0600F746 RID: 63302 RVA: 0x003814AB File Offset: 0x0037F6AB
		// (set) Token: 0x0600F747 RID: 63303 RVA: 0x003814B8 File Offset: 0x0037F6B8
		[DefaultValue("Update")]
		[NotifyParentProperty(true)]
		public string UpdateText
		{
			get
			{
				return this.GetString("UpdateText");
			}
			set
			{
				this.SetString("UpdateText", value);
			}
		}

		// Token: 0x17004A7F RID: 19071
		// (get) Token: 0x0600F748 RID: 63304 RVA: 0x003814C6 File Offset: 0x0037F6C6
		// (set) Token: 0x0600F749 RID: 63305 RVA: 0x003814D3 File Offset: 0x0037F6D3
		[DefaultValue("Cancel")]
		[NotifyParentProperty(true)]
		public string CancelText
		{
			get
			{
				return this.GetString("CancelText");
			}
			set
			{
				this.SetString("CancelText", value);
			}
		}

		// Token: 0x17004A80 RID: 19072
		// (get) Token: 0x0600F74A RID: 63306 RVA: 0x003814E1 File Offset: 0x0037F6E1
		// (set) Token: 0x0600F74B RID: 63307 RVA: 0x003814EE File Offset: 0x0037F6EE
		[DefaultValue("Delete")]
		[NotifyParentProperty(true)]
		public string DeleteText
		{
			get
			{
				return this.GetString("DeleteText");
			}
			set
			{
				this.SetString("DeleteText", value);
			}
		}

		// Token: 0x17004A81 RID: 19073
		// (get) Token: 0x0600F74C RID: 63308 RVA: 0x003814FC File Offset: 0x0037F6FC
		// (set) Token: 0x0600F74D RID: 63309 RVA: 0x00381509 File Offset: 0x0037F709
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Caption
		{
			get
			{
				return this.GetString("Caption");
			}
			set
			{
				this.SetString("Caption", value);
			}
		}

		// Token: 0x17004A82 RID: 19074
		// (get) Token: 0x0600F74E RID: 63310 RVA: 0x00381517 File Offset: 0x0037F717
		// (set) Token: 0x0600F74F RID: 63311 RVA: 0x00381524 File Offset: 0x0037F724
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Summary
		{
			get
			{
				return this.GetString("Summary");
			}
			set
			{
				this.SetString("Summary", value);
			}
		}

		// Token: 0x17004A83 RID: 19075
		// (get) Token: 0x0600F750 RID: 63312 RVA: 0x00381532 File Offset: 0x0037F732
		// (set) Token: 0x0600F751 RID: 63313 RVA: 0x0038153F File Offset: 0x0037F73F
		[NotifyParentProperty(true)]
		[DefaultValue("No records to display.")]
		public string NoMasterRecordsText
		{
			get
			{
				return this.GetString("NoMasterRecordsText");
			}
			set
			{
				this.SetString("NoMasterRecordsText", value);
			}
		}

		// Token: 0x17004A84 RID: 19076
		// (get) Token: 0x0600F752 RID: 63314 RVA: 0x0038154D File Offset: 0x0037F74D
		// (set) Token: 0x0600F753 RID: 63315 RVA: 0x0038155A File Offset: 0x0037F75A
		[NotifyParentProperty(true)]
		[DefaultValue("No child records to display.")]
		public string NoDetailRecordsText
		{
			get
			{
				return this.GetString("NoDetailRecordsText");
			}
			set
			{
				this.SetString("NoDetailRecordsText", value);
			}
		}

		// Token: 0x17004A85 RID: 19077
		// (get) Token: 0x0600F754 RID: 63316 RVA: 0x00381568 File Offset: 0x0037F768
		// (set) Token: 0x0600F755 RID: 63317 RVA: 0x00381575 File Offset: 0x0037F775
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string FilterExpression
		{
			get
			{
				return this.GetString("FilterExpression");
			}
			set
			{
				this.SetString("FilterExpression", value);
			}
		}

		// Token: 0x17004A86 RID: 19078
		// (get) Token: 0x0600F756 RID: 63318 RVA: 0x00381583 File Offset: 0x0037F783
		// (set) Token: 0x0600F757 RID: 63319 RVA: 0x00381590 File Offset: 0x0037F790
		[NotifyParentProperty(true)]
		[DefaultValue("Add new record")]
		public string AddNewRecordText
		{
			get
			{
				return this.GetString("AddNewRecordText");
			}
			set
			{
				this.SetString("AddNewRecordText", value);
			}
		}

		// Token: 0x17004A87 RID: 19079
		// (get) Token: 0x0600F758 RID: 63320 RVA: 0x0038159E File Offset: 0x0037F79E
		// (set) Token: 0x0600F759 RID: 63321 RVA: 0x003815AB File Offset: 0x0037F7AB
		[NotifyParentProperty(true)]
		[DefaultValue("Save changes")]
		public string SaveChangesText
		{
			get
			{
				return this.GetString("SaveChangesText");
			}
			set
			{
				this.SetString("SaveChangesText", value);
			}
		}

		// Token: 0x17004A88 RID: 19080
		// (get) Token: 0x0600F75A RID: 63322 RVA: 0x003815B9 File Offset: 0x0037F7B9
		// (set) Token: 0x0600F75B RID: 63323 RVA: 0x003815C6 File Offset: 0x0037F7C6
		[DefaultValue("Cancel changes")]
		[NotifyParentProperty(true)]
		public string CancelChangesText
		{
			get
			{
				return this.GetString("CancelChangesText");
			}
			set
			{
				this.SetString("CancelChangesText", value);
			}
		}

		// Token: 0x17004A89 RID: 19081
		// (get) Token: 0x0600F75C RID: 63324 RVA: 0x003815D4 File Offset: 0x0037F7D4
		// (set) Token: 0x0600F75D RID: 63325 RVA: 0x003815E1 File Offset: 0x0037F7E1
		[DefaultValue("Refresh")]
		[NotifyParentProperty(true)]
		public string Refresh
		{
			get
			{
				return this.GetString("Refresh");
			}
			set
			{
				this.SetString("Refresh", value);
			}
		}

		// Token: 0x17004A8A RID: 19082
		// (get) Token: 0x0600F75E RID: 63326 RVA: 0x003815EF File Offset: 0x0037F7EF
		// (set) Token: 0x0600F75F RID: 63327 RVA: 0x003815FC File Offset: 0x0037F7FC
		[DefaultValue("Export to Excel")]
		[NotifyParentProperty(true)]
		public string ExportToExcelText
		{
			get
			{
				return this.GetString("ExportToExcelText");
			}
			set
			{
				this.SetString("ExportToExcelText", value);
			}
		}

		// Token: 0x17004A8B RID: 19083
		// (get) Token: 0x0600F760 RID: 63328 RVA: 0x0038160A File Offset: 0x0037F80A
		// (set) Token: 0x0600F761 RID: 63329 RVA: 0x00381617 File Offset: 0x0037F817
		[DefaultValue("Export to Word")]
		[NotifyParentProperty(true)]
		public string ExportToWordText
		{
			get
			{
				return this.GetString("ExportToWordText");
			}
			set
			{
				this.SetString("ExportToWordText", value);
			}
		}

		// Token: 0x17004A8C RID: 19084
		// (get) Token: 0x0600F762 RID: 63330 RVA: 0x00381625 File Offset: 0x0037F825
		// (set) Token: 0x0600F763 RID: 63331 RVA: 0x00381632 File Offset: 0x0037F832
		[NotifyParentProperty(true)]
		[DefaultValue("Export to Pdf")]
		public string ExportToPdfText
		{
			get
			{
				return this.GetString("ExportToPdfText");
			}
			set
			{
				this.SetString("ExportToPdfText", value);
			}
		}

		// Token: 0x17004A8D RID: 19085
		// (get) Token: 0x0600F764 RID: 63332 RVA: 0x00381640 File Offset: 0x0037F840
		// (set) Token: 0x0600F765 RID: 63333 RVA: 0x0038164D File Offset: 0x0037F84D
		[DefaultValue("Export to CSV")]
		[NotifyParentProperty(true)]
		public string ExportToCsvText
		{
			get
			{
				return this.GetString("ExportToCsvText");
			}
			set
			{
				this.SetString("ExportToCsvText", value);
			}
		}

		// Token: 0x17004A8E RID: 19086
		// (get) Token: 0x0600F766 RID: 63334 RVA: 0x0038165B File Offset: 0x0037F85B
		// (set) Token: 0x0600F767 RID: 63335 RVA: 0x00381668 File Offset: 0x0037F868
		[NotifyParentProperty(true)]
		[DefaultValue("Print RadGrid")]
		public string PrintGridText
		{
			get
			{
				return this.GetString("PrintGridText");
			}
			set
			{
				this.SetString("PrintGridText", value);
			}
		}

		// Token: 0x17004A8F RID: 19087
		// (get) Token: 0x0600F768 RID: 63336 RVA: 0x00381676 File Offset: 0x0037F876
		// (set) Token: 0x0600F769 RID: 63337 RVA: 0x00381683 File Offset: 0x0037F883
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string AddNewRecordImageUrl
		{
			get
			{
				return this.GetString("AddNewRecordImageUrl");
			}
			set
			{
				this.SetString("AddNewRecordImageUrl", value);
			}
		}

		// Token: 0x17004A90 RID: 19088
		// (get) Token: 0x0600F76A RID: 63338 RVA: 0x00381691 File Offset: 0x0037F891
		// (set) Token: 0x0600F76B RID: 63339 RVA: 0x0038169E File Offset: 0x0037F89E
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string RefreshImageUrl
		{
			get
			{
				return this.GetString("RefreshImageUrl");
			}
			set
			{
				this.SetString("RefreshImageUrl", value);
			}
		}

		// Token: 0x17004A91 RID: 19089
		// (get) Token: 0x0600F76C RID: 63340 RVA: 0x003816AC File Offset: 0x0037F8AC
		// (set) Token: 0x0600F76D RID: 63341 RVA: 0x003816B9 File Offset: 0x0037F8B9
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string ExportToExcelImageUrl
		{
			get
			{
				return this.GetString("ExportToExcelImageUrl");
			}
			set
			{
				this.SetString("ExportToExcelImageUrl", value);
			}
		}

		// Token: 0x17004A92 RID: 19090
		// (get) Token: 0x0600F76E RID: 63342 RVA: 0x003816C7 File Offset: 0x0037F8C7
		// (set) Token: 0x0600F76F RID: 63343 RVA: 0x003816D4 File Offset: 0x0037F8D4
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string ExportToWordImageUrl
		{
			get
			{
				return this.GetString("ExportToWordImageUrl");
			}
			set
			{
				this.SetString("ExportToWordImageUrl", value);
			}
		}

		// Token: 0x17004A93 RID: 19091
		// (get) Token: 0x0600F770 RID: 63344 RVA: 0x003816E2 File Offset: 0x0037F8E2
		// (set) Token: 0x0600F771 RID: 63345 RVA: 0x003816EF File Offset: 0x0037F8EF
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ExportToPdfImageUrl
		{
			get
			{
				return this.GetString("ExportToPdfImageUrl");
			}
			set
			{
				this.SetString("ExportToPdfImageUrl", value);
			}
		}

		// Token: 0x17004A94 RID: 19092
		// (get) Token: 0x0600F772 RID: 63346 RVA: 0x003816FD File Offset: 0x0037F8FD
		// (set) Token: 0x0600F773 RID: 63347 RVA: 0x0038170A File Offset: 0x0037F90A
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ExportToCsvImageUrl
		{
			get
			{
				return this.GetString("ExportToCsvImageUrl");
			}
			set
			{
				this.SetString("ExportToCsvImageUrl", value);
			}
		}

		// Token: 0x17004A95 RID: 19093
		// (get) Token: 0x0600F774 RID: 63348 RVA: 0x00381718 File Offset: 0x0037F918
		// (set) Token: 0x0600F775 RID: 63349 RVA: 0x00381725 File Offset: 0x0037F925
		[DefaultValue("Close")]
		[NotifyParentProperty(true)]
		public string CloseText
		{
			get
			{
				return this.GetString("CloseText");
			}
			set
			{
				this.SetString("CloseText", value);
			}
		}

		// Token: 0x17004A96 RID: 19094
		// (get) Token: 0x0600F776 RID: 63350 RVA: 0x00381733 File Offset: 0x0037F933
		// (set) Token: 0x0600F777 RID: 63351 RVA: 0x00381740 File Offset: 0x0037F940
		[NotifyParentProperty(true)]
		[DefaultValue("Prev")]
		public string PrevFrozenColumnText
		{
			get
			{
				return this.GetString("PrevFrozenColumnText");
			}
			set
			{
				this.SetString("PrevFrozenColumnText", value);
			}
		}

		// Token: 0x17004A97 RID: 19095
		// (get) Token: 0x0600F778 RID: 63352 RVA: 0x0038174E File Offset: 0x0037F94E
		// (set) Token: 0x0600F779 RID: 63353 RVA: 0x0038175B File Offset: 0x0037F95B
		[DefaultValue("Next")]
		[NotifyParentProperty(true)]
		public string NextFrozenColumnText
		{
			get
			{
				return this.GetString("NextFrozenColumnText");
			}
			set
			{
				this.SetString("NextFrozenColumnText", value);
			}
		}

		// Token: 0x17004A98 RID: 19096
		// (get) Token: 0x0600F77A RID: 63354 RVA: 0x00381769 File Offset: 0x0037F969
		// (set) Token: 0x0600F77B RID: 63355 RVA: 0x00381776 File Offset: 0x0037F976
		[NotifyParentProperty(true)]
		[DefaultValue("Drag a column header and drop it here to group by that column")]
		public string GroupPanelText
		{
			get
			{
				return this.GetString("GroupPanelText");
			}
			set
			{
				this.SetString("GroupPanelText", value);
			}
		}

		// Token: 0x17004A99 RID: 19097
		// (get) Token: 0x0600F77C RID: 63356 RVA: 0x00381784 File Offset: 0x0037F984
		// (set) Token: 0x0600F77D RID: 63357 RVA: 0x00381791 File Offset: 0x0037F991
		[NotifyParentProperty(true)]
		[DefaultValue(" Group continues on the next page.")]
		public string GroupContinuesFormatString
		{
			get
			{
				return this.GetString("GroupContinuesFormatString");
			}
			set
			{
				this.SetString("GroupContinuesFormatString", value);
			}
		}

		// Token: 0x17004A9A RID: 19098
		// (get) Token: 0x0600F77E RID: 63358 RVA: 0x0038179F File Offset: 0x0037F99F
		// (set) Token: 0x0600F77F RID: 63359 RVA: 0x003817AC File Offset: 0x0037F9AC
		[DefaultValue("... group continued from the previous page. ")]
		[NotifyParentProperty(true)]
		public string GroupContinuedFormatString
		{
			get
			{
				return this.GetString("GroupContinuedFormatString");
			}
			set
			{
				this.SetString("GroupContinuedFormatString", value);
			}
		}

		// Token: 0x17004A9B RID: 19099
		// (get) Token: 0x0600F780 RID: 63360 RVA: 0x003817BA File Offset: 0x0037F9BA
		// (set) Token: 0x0600F781 RID: 63361 RVA: 0x003817C7 File Offset: 0x0037F9C7
		[DefaultValue("Showing {0} of {1} items.")]
		[NotifyParentProperty(true)]
		public string GroupSplitDisplayFormat
		{
			get
			{
				return this.GetString("GroupSplitDisplayFormat");
			}
			set
			{
				this.SetString("GroupSplitDisplayFormat", value);
			}
		}

		// Token: 0x17004A9C RID: 19100
		// (get) Token: 0x0600F782 RID: 63362 RVA: 0x003817D5 File Offset: 0x0037F9D5
		// (set) Token: 0x0600F783 RID: 63363 RVA: 0x003817E2 File Offset: 0x0037F9E2
		[DefaultValue(" ({0})")]
		[NotifyParentProperty(true)]
		public string GroupSplitFormat
		{
			get
			{
				return this.GetString("GroupSplitFormat");
			}
			set
			{
				this.SetString("GroupSplitFormat", value);
			}
		}

		// Token: 0x17004A9D RID: 19101
		// (get) Token: 0x0600F784 RID: 63364 RVA: 0x003817F0 File Offset: 0x0037F9F0
		// (set) Token: 0x0600F785 RID: 63365 RVA: 0x003817FD File Offset: 0x0037F9FD
		[NotifyParentProperty(true)]
		[DefaultValue("; ")]
		public string GroupByFieldsSeparator
		{
			get
			{
				return this.GetString("GroupByFieldsSeparator");
			}
			set
			{
				this.SetString("GroupByFieldsSeparator", value);
			}
		}

		// Token: 0x17004A9E RID: 19102
		// (get) Token: 0x0600F786 RID: 63366 RVA: 0x0038180B File Offset: 0x0037FA0B
		// (set) Token: 0x0600F787 RID: 63367 RVA: 0x00381818 File Offset: 0x0037FA18
		[NotifyParentProperty(true)]
		[DefaultValue("Expand")]
		public string ExpandTooltip
		{
			get
			{
				return this.GetString("ExpandTooltip");
			}
			set
			{
				this.SetString("ExpandTooltip", value);
			}
		}

		// Token: 0x17004A9F RID: 19103
		// (get) Token: 0x0600F788 RID: 63368 RVA: 0x00381826 File Offset: 0x0037FA26
		// (set) Token: 0x0600F789 RID: 63369 RVA: 0x00381833 File Offset: 0x0037FA33
		[DefaultValue("Expand all groups")]
		[NotifyParentProperty(true)]
		public string ExpandAllTooltip
		{
			get
			{
				return this.GetString("ExpandAllTooltip");
			}
			set
			{
				this.SetString("ExpandAllTooltip", value);
			}
		}

		// Token: 0x17004AA0 RID: 19104
		// (get) Token: 0x0600F78A RID: 63370 RVA: 0x00381841 File Offset: 0x0037FA41
		// (set) Token: 0x0600F78B RID: 63371 RVA: 0x0038184E File Offset: 0x0037FA4E
		[DefaultValue("Collapse group")]
		[NotifyParentProperty(true)]
		public string CollapseTooltip
		{
			get
			{
				return this.GetString("CollapseTooltip");
			}
			set
			{
				this.SetString("CollapseTooltip", value);
			}
		}

		// Token: 0x17004AA1 RID: 19105
		// (get) Token: 0x0600F78C RID: 63372 RVA: 0x0038185C File Offset: 0x0037FA5C
		// (set) Token: 0x0600F78D RID: 63373 RVA: 0x00381869 File Offset: 0x0037FA69
		[DefaultValue("Collapse all groups")]
		[NotifyParentProperty(true)]
		public string CollapseAllTooltip
		{
			get
			{
				return this.GetString("CollapseAllTooltip");
			}
			set
			{
				this.SetString("CollapseAllTooltip", value);
			}
		}

		// Token: 0x17004AA2 RID: 19106
		// (get) Token: 0x0600F78E RID: 63374 RVA: 0x00381877 File Offset: 0x0037FA77
		// (set) Token: 0x0600F78F RID: 63375 RVA: 0x00381884 File Offset: 0x0037FA84
		[NotifyParentProperty(true)]
		[DefaultValue("Drag out of the bar to ungroup")]
		public string UnGroupTooltip
		{
			get
			{
				return this.GetString("UnGroupTooltip");
			}
			set
			{
				this.SetString("UnGroupTooltip", value);
			}
		}

		// Token: 0x17004AA3 RID: 19107
		// (get) Token: 0x0600F790 RID: 63376 RVA: 0x00381892 File Offset: 0x0037FA92
		// (set) Token: 0x0600F791 RID: 63377 RVA: 0x0038189F File Offset: 0x0037FA9F
		[NotifyParentProperty(true)]
		[DefaultValue("Click here to ungroup")]
		public string UnGroupButtonTooltip
		{
			get
			{
				return this.GetString("UnGroupButtonTooltip");
			}
			set
			{
				this.SetString("UnGroupButtonTooltip", value);
			}
		}

		// Token: 0x17004AA4 RID: 19108
		// (get) Token: 0x0600F792 RID: 63378 RVA: 0x003818AD File Offset: 0x0037FAAD
		// (set) Token: 0x0600F793 RID: 63379 RVA: 0x003818BA File Offset: 0x0037FABA
		[DefaultValue("Drop here to reorder")]
		[NotifyParentProperty(true)]
		public string DropHereToReorder
		{
			get
			{
				return this.GetString("DropHereToReorder");
			}
			set
			{
				this.SetString("DropHereToReorder", value);
			}
		}

		// Token: 0x17004AA5 RID: 19109
		// (get) Token: 0x0600F794 RID: 63380 RVA: 0x003818C8 File Offset: 0x0037FAC8
		// (set) Token: 0x0600F795 RID: 63381 RVA: 0x003818D5 File Offset: 0x0037FAD5
		[DefaultValue("Drag to group or reorder")]
		[NotifyParentProperty(true)]
		public string DragToGroupOrReorder
		{
			get
			{
				return this.GetString("DragToGroupOrReorder");
			}
			set
			{
				this.SetString("DragToGroupOrReorder", value);
			}
		}

		// Token: 0x17004AA6 RID: 19110
		// (get) Token: 0x0600F796 RID: 63382 RVA: 0x003818E3 File Offset: 0x0037FAE3
		// (set) Token: 0x0600F797 RID: 63383 RVA: 0x003818F0 File Offset: 0x0037FAF0
		[DefaultValue("Drag to resize")]
		[NotifyParentProperty(true)]
		public string DragToResize
		{
			get
			{
				return this.GetString("DragToResize");
			}
			set
			{
				this.SetString("DragToResize", value);
			}
		}

		// Token: 0x17004AA7 RID: 19111
		// (get) Token: 0x0600F798 RID: 63384 RVA: 0x003818FE File Offset: 0x0037FAFE
		// (set) Token: 0x0600F799 RID: 63385 RVA: 0x0038190B File Offset: 0x0037FB0B
		[NotifyParentProperty(true)]
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
		public string PagerTooltipFormatString
		{
			get
			{
				return this.GetString("PagerTooltipFormatString");
			}
			set
			{
				this.SetString("PagerTooltipFormatString", value);
			}
		}

		// Token: 0x17004AA8 RID: 19112
		// (get) Token: 0x0600F79A RID: 63386 RVA: 0x00381919 File Offset: 0x0037FB19
		// (set) Token: 0x0600F79B RID: 63387 RVA: 0x00381926 File Offset: 0x0037FB26
		[NotifyParentProperty(true)]
		[DefaultValue("Width: <strong>{0}</strong> <em>pixels</em>")]
		public string ColumnResizeTooltipFormatString
		{
			get
			{
				return this.GetString("ColumnResizeTooltipFormatString");
			}
			set
			{
				this.SetString("ColumnResizeTooltipFormatString", value);
			}
		}

		// Token: 0x17004AA9 RID: 19113
		// (get) Token: 0x0600F79C RID: 63388 RVA: 0x00381934 File Offset: 0x0037FB34
		// (set) Token: 0x0600F79D RID: 63389 RVA: 0x00381941 File Offset: 0x0037FB41
		[DefaultValue("Expand")]
		[NotifyParentProperty(true)]
		public string HierarchyExpandTooltip
		{
			get
			{
				return this.GetString("HierarchyExpandTooltip");
			}
			set
			{
				this.SetString("HierarchyExpandTooltip", value);
			}
		}

		// Token: 0x17004AAA RID: 19114
		// (get) Token: 0x0600F79E RID: 63390 RVA: 0x0038194F File Offset: 0x0037FB4F
		// (set) Token: 0x0600F79F RID: 63391 RVA: 0x0038195C File Offset: 0x0037FB5C
		[NotifyParentProperty(true)]
		[DefaultValue("Expand all")]
		public string HierarchyExpandAllTooltip
		{
			get
			{
				return this.GetString("HierarchyExpandAllTooltip");
			}
			set
			{
				this.SetString("HierarchyExpandAllTooltip", value);
			}
		}

		// Token: 0x17004AAB RID: 19115
		// (get) Token: 0x0600F7A0 RID: 63392 RVA: 0x0038196A File Offset: 0x0037FB6A
		// (set) Token: 0x0600F7A1 RID: 63393 RVA: 0x00381977 File Offset: 0x0037FB77
		[NotifyParentProperty(true)]
		[DefaultValue("Collapse")]
		public string HierarchyCollapseTooltip
		{
			get
			{
				return this.GetString("HierarchyCollapseTooltip");
			}
			set
			{
				this.SetString("HierarchyCollapseTooltip", value);
			}
		}

		// Token: 0x17004AAC RID: 19116
		// (get) Token: 0x0600F7A2 RID: 63394 RVA: 0x00381985 File Offset: 0x0037FB85
		// (set) Token: 0x0600F7A3 RID: 63395 RVA: 0x00381992 File Offset: 0x0037FB92
		[DefaultValue("Collapse all")]
		[NotifyParentProperty(true)]
		public string HierarchyCollapseAllTooltip
		{
			get
			{
				return this.GetString("HierarchyCollapseAllTooltip");
			}
			set
			{
				this.SetString("HierarchyCollapseAllTooltip", value);
			}
		}

		// Token: 0x17004AAD RID: 19117
		// (get) Token: 0x0600F7A4 RID: 63396 RVA: 0x003819A0 File Offset: 0x0037FBA0
		// (set) Token: 0x0600F7A5 RID: 63397 RVA: 0x003819AD File Offset: 0x0037FBAD
		[DefaultValue("Self reference expand")]
		[NotifyParentProperty(true)]
		public string HierarchySelfExpandTooltip
		{
			get
			{
				return this.GetString("HierarchySelfExpandTooltip");
			}
			set
			{
				this.SetString("HierarchySelfExpandTooltip", value);
			}
		}

		// Token: 0x17004AAE RID: 19118
		// (get) Token: 0x0600F7A6 RID: 63398 RVA: 0x003819BB File Offset: 0x0037FBBB
		// (set) Token: 0x0600F7A7 RID: 63399 RVA: 0x003819C8 File Offset: 0x0037FBC8
		[DefaultValue("Self reference collapse")]
		[NotifyParentProperty(true)]
		public string HierarchySelfCollapseTooltip
		{
			get
			{
				return this.GetString("HierarchySelfCollapseTooltip");
			}
			set
			{
				this.SetString("HierarchySelfCollapseTooltip", value);
			}
		}

		// Token: 0x17004AAF RID: 19119
		// (get) Token: 0x0600F7A8 RID: 63400 RVA: 0x003819D6 File Offset: 0x0037FBD6
		// (set) Token: 0x0600F7A9 RID: 63401 RVA: 0x003819E3 File Offset: 0x0037FBE3
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string NextPageText
		{
			get
			{
				return this.GetString("NextPageText");
			}
			set
			{
				this.SetString("NextPageText", value);
			}
		}

		// Token: 0x17004AB0 RID: 19120
		// (get) Token: 0x0600F7AA RID: 63402 RVA: 0x003819F1 File Offset: 0x0037FBF1
		// (set) Token: 0x0600F7AB RID: 63403 RVA: 0x003819FE File Offset: 0x0037FBFE
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string LastPageText
		{
			get
			{
				return this.GetString("LastPageText");
			}
			set
			{
				this.SetString("LastPageText", value);
			}
		}

		// Token: 0x17004AB1 RID: 19121
		// (get) Token: 0x0600F7AC RID: 63404 RVA: 0x00381A0C File Offset: 0x0037FC0C
		// (set) Token: 0x0600F7AD RID: 63405 RVA: 0x00381A19 File Offset: 0x0037FC19
		[NotifyParentProperty(true)]
		[DefaultValue("First Page")]
		public string FirstPageToolTip
		{
			get
			{
				return this.GetString("FirstPageToolTip");
			}
			set
			{
				this.SetString("FirstPageToolTip", value);
			}
		}

		// Token: 0x17004AB2 RID: 19122
		// (get) Token: 0x0600F7AE RID: 63406 RVA: 0x00381A27 File Offset: 0x0037FC27
		// (set) Token: 0x0600F7AF RID: 63407 RVA: 0x00381A34 File Offset: 0x0037FC34
		[DefaultValue("Next Page")]
		[NotifyParentProperty(true)]
		public string NextPageToolTip
		{
			get
			{
				return this.GetString("NextPageToolTip");
			}
			set
			{
				this.SetString("NextPageToolTip", value);
			}
		}

		// Token: 0x17004AB3 RID: 19123
		// (get) Token: 0x0600F7B0 RID: 63408 RVA: 0x00381A42 File Offset: 0x0037FC42
		// (set) Token: 0x0600F7B1 RID: 63409 RVA: 0x00381A4F File Offset: 0x0037FC4F
		[DefaultValue("Last Page")]
		[NotifyParentProperty(true)]
		public string LastPageToolTip
		{
			get
			{
				return this.GetString("LastPageToolTip");
			}
			set
			{
				this.SetString("LastPageToolTip", value);
			}
		}

		// Token: 0x17004AB4 RID: 19124
		// (get) Token: 0x0600F7B2 RID: 63410 RVA: 0x00381A5D File Offset: 0x0037FC5D
		// (set) Token: 0x0600F7B3 RID: 63411 RVA: 0x00381A6A File Offset: 0x0037FC6A
		[NotifyParentProperty(true)]
		[DefaultValue("Previous Page")]
		public string PrevPageToolTip
		{
			get
			{
				return this.GetString("PrevPageToolTip");
			}
			set
			{
				this.SetString("PrevPageToolTip", value);
			}
		}

		// Token: 0x17004AB5 RID: 19125
		// (get) Token: 0x0600F7B4 RID: 63412 RVA: 0x00381A78 File Offset: 0x0037FC78
		// (set) Token: 0x0600F7B5 RID: 63413 RVA: 0x00381A85 File Offset: 0x0037FC85
		[NotifyParentProperty(true)]
		[DefaultValue("Next Pages")]
		public string NextPagesToolTip
		{
			get
			{
				return this.GetString("NextPagesToolTip");
			}
			set
			{
				this.SetString("NextPagesToolTip", value);
			}
		}

		// Token: 0x17004AB6 RID: 19126
		// (get) Token: 0x0600F7B6 RID: 63414 RVA: 0x00381A93 File Offset: 0x0037FC93
		// (set) Token: 0x0600F7B7 RID: 63415 RVA: 0x00381AA0 File Offset: 0x0037FCA0
		[DefaultValue("Previous Pages")]
		[NotifyParentProperty(true)]
		public string PrevPagesToolTip
		{
			get
			{
				return this.GetString("PrevPagesToolTip");
			}
			set
			{
				this.SetString("PrevPagesToolTip", value);
			}
		}

		// Token: 0x17004AB7 RID: 19127
		// (get) Token: 0x0600F7B8 RID: 63416 RVA: 0x00381AAE File Offset: 0x0037FCAE
		// (set) Token: 0x0600F7B9 RID: 63417 RVA: 0x00381ABB File Offset: 0x0037FCBB
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string GoToPageTextBoxToolTip
		{
			get
			{
				return this.GetString("GoToPageTextBoxToolTip");
			}
			set
			{
				this.SetString("GoToPageTextBoxToolTip", value);
			}
		}

		// Token: 0x17004AB8 RID: 19128
		// (get) Token: 0x0600F7BA RID: 63418 RVA: 0x00381AC9 File Offset: 0x0037FCC9
		// (set) Token: 0x0600F7BB RID: 63419 RVA: 0x00381AD6 File Offset: 0x0037FCD6
		[NotifyParentProperty(true)]
		[DefaultValue("Go to Page")]
		public string GoToPageButtonToolTip
		{
			get
			{
				return this.GetString("GoToPageButtonToolTip");
			}
			set
			{
				this.SetString("GoToPageButtonToolTip", value);
			}
		}

		// Token: 0x17004AB9 RID: 19129
		// (get) Token: 0x0600F7BC RID: 63420 RVA: 0x00381AE4 File Offset: 0x0037FCE4
		// (set) Token: 0x0600F7BD RID: 63421 RVA: 0x00381AF1 File Offset: 0x0037FCF1
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string ChangePageSizeTextBoxToolTip
		{
			get
			{
				return this.GetString("ChangePageSizeTextBoxToolTip");
			}
			set
			{
				this.SetString("ChangePageSizeTextBoxToolTip", value);
			}
		}

		// Token: 0x17004ABA RID: 19130
		// (get) Token: 0x0600F7BE RID: 63422 RVA: 0x00381AFF File Offset: 0x0037FCFF
		// (set) Token: 0x0600F7BF RID: 63423 RVA: 0x00381B0C File Offset: 0x0037FD0C
		[NotifyParentProperty(true)]
		[DefaultValue("Change Page Size")]
		public string ChangePageSizeButtonToolTip
		{
			get
			{
				return this.GetString("ChangePageSizeButtonToolTip");
			}
			set
			{
				this.SetString("ChangePageSizeButtonToolTip", value);
			}
		}

		// Token: 0x17004ABB RID: 19131
		// (get) Token: 0x0600F7C0 RID: 63424 RVA: 0x00381B1A File Offset: 0x0037FD1A
		// (set) Token: 0x0600F7C1 RID: 63425 RVA: 0x00381B27 File Offset: 0x0037FD27
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string ChangePageSizeComboBoxTableSummary
		{
			get
			{
				return this.GetString("ChangePageSizeComboBoxTableSummary");
			}
			set
			{
				this.SetString("ChangePageSizeComboBoxTableSummary", value);
			}
		}

		// Token: 0x17004ABC RID: 19132
		// (get) Token: 0x0600F7C2 RID: 63426 RVA: 0x00381B35 File Offset: 0x0037FD35
		// (set) Token: 0x0600F7C3 RID: 63427 RVA: 0x00381B42 File Offset: 0x0037FD42
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ChangePageSizeComboBoxToolTip
		{
			get
			{
				return this.GetString("ChangePageSizeComboBoxToolTip");
			}
			set
			{
				this.SetString("ChangePageSizeComboBoxToolTip", value);
			}
		}

		// Token: 0x17004ABD RID: 19133
		// (get) Token: 0x0600F7C4 RID: 63428 RVA: 0x00381B50 File Offset: 0x0037FD50
		// (set) Token: 0x0600F7C5 RID: 63429 RVA: 0x00381B5D File Offset: 0x0037FD5D
		[NotifyParentProperty(true)]
		[DefaultValue("Page size:")]
		public string PageSizeLabelText
		{
			get
			{
				return this.GetString("PageSizeLabelText");
			}
			set
			{
				this.SetString("PageSizeLabelText", value);
			}
		}

		// Token: 0x17004ABE RID: 19134
		// (get) Token: 0x0600F7C6 RID: 63430 RVA: 0x00381B6B File Offset: 0x0037FD6B
		// (set) Token: 0x0600F7C7 RID: 63431 RVA: 0x00381B78 File Offset: 0x0037FD78
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string PrevPageText
		{
			get
			{
				return this.GetString("PrevPageText");
			}
			set
			{
				this.SetString("PrevPageText", value);
			}
		}

		// Token: 0x17004ABF RID: 19135
		// (get) Token: 0x0600F7C8 RID: 63432 RVA: 0x00381B86 File Offset: 0x0037FD86
		// (set) Token: 0x0600F7C9 RID: 63433 RVA: 0x00381B93 File Offset: 0x0037FD93
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string FirstPageText
		{
			get
			{
				return this.GetString("FirstPageText");
			}
			set
			{
				this.SetString("FirstPageText", value);
			}
		}

		// Token: 0x17004AC0 RID: 19136
		// (get) Token: 0x0600F7CA RID: 63434 RVA: 0x00381BA1 File Offset: 0x0037FDA1
		// (set) Token: 0x0600F7CB RID: 63435 RVA: 0x00381BAE File Offset: 0x0037FDAE
		[DefaultValue("Change page: {4} &nbsp;Page <strong>{0}</strong> of <strong>{1}</strong>, items <strong>{2}</strong> to <strong>{3}</strong> of <strong>{5}</strong>.")]
		[NotifyParentProperty(true)]
		public string PagerTextFormat
		{
			get
			{
				return this.GetString("PagerTextFormat");
			}
			set
			{
				this.SetString("PagerTextFormat", value);
			}
		}

		// Token: 0x17004AC1 RID: 19137
		// (get) Token: 0x0600F7CC RID: 63436 RVA: 0x00381BBC File Offset: 0x0037FDBC
		// (set) Token: 0x0600F7CD RID: 63437 RVA: 0x00381BC9 File Offset: 0x0037FDC9
		[NotifyParentProperty(true)]
		[DefaultValue("Page:")]
		public string GoToPageLabelText
		{
			get
			{
				return this.GetString("GoToPageLabelText");
			}
			set
			{
				this.SetString("GoToPageLabelText", value);
			}
		}

		// Token: 0x17004AC2 RID: 19138
		// (get) Token: 0x0600F7CE RID: 63438 RVA: 0x00381BD7 File Offset: 0x0037FDD7
		// (set) Token: 0x0600F7CF RID: 63439 RVA: 0x00381BE4 File Offset: 0x0037FDE4
		[NotifyParentProperty(true)]
		[DefaultValue("of {0}")]
		public string PageOfLabelText
		{
			get
			{
				return this.GetString("PageOfLabelText");
			}
			set
			{
				this.SetString("PageOfLabelText", value);
			}
		}

		// Token: 0x17004AC3 RID: 19139
		// (get) Token: 0x0600F7D0 RID: 63440 RVA: 0x00381BF2 File Offset: 0x0037FDF2
		// (set) Token: 0x0600F7D1 RID: 63441 RVA: 0x00381BFF File Offset: 0x0037FDFF
		[DefaultValue("Go")]
		[NotifyParentProperty(true)]
		public string GoToPageLinkButtonText
		{
			get
			{
				return this.GetString("GoToPageLinkButtonText");
			}
			set
			{
				this.SetString("GoToPageLinkButtonText", value);
			}
		}

		// Token: 0x17004AC4 RID: 19140
		// (get) Token: 0x0600F7D2 RID: 63442 RVA: 0x00381C0D File Offset: 0x0037FE0D
		// (set) Token: 0x0600F7D3 RID: 63443 RVA: 0x00381C1A File Offset: 0x0037FE1A
		[DefaultValue("Page size:")]
		[NotifyParentProperty(true)]
		public string ChangePageSizeLabelText
		{
			get
			{
				return this.GetString("ChangePageSizeLabelText");
			}
			set
			{
				this.SetString("ChangePageSizeLabelText", value);
			}
		}

		// Token: 0x17004AC5 RID: 19141
		// (get) Token: 0x0600F7D4 RID: 63444 RVA: 0x00381C28 File Offset: 0x0037FE28
		// (set) Token: 0x0600F7D5 RID: 63445 RVA: 0x00381C35 File Offset: 0x0037FE35
		[NotifyParentProperty(true)]
		[DefaultValue("Change")]
		public string ChangePageSizeLinkButtonText
		{
			get
			{
				return this.GetString("ChangePageSizeLinkButtonText");
			}
			set
			{
				this.SetString("ChangePageSizeLinkButtonText", value);
			}
		}

		// Token: 0x17004AC6 RID: 19142
		// (get) Token: 0x0600F7D6 RID: 63446 RVA: 0x00381C43 File Offset: 0x0037FE43
		// (set) Token: 0x0600F7D7 RID: 63447 RVA: 0x00381C50 File Offset: 0x0037FE50
		[DefaultValue("Increase")]
		[NotifyParentProperty(true)]
		public string SliderIncreaseText
		{
			get
			{
				return this.GetString("SliderIncreaseText");
			}
			set
			{
				this.SetString("SliderIncreaseText", value);
			}
		}

		// Token: 0x17004AC7 RID: 19143
		// (get) Token: 0x0600F7D8 RID: 63448 RVA: 0x00381C5E File Offset: 0x0037FE5E
		// (set) Token: 0x0600F7D9 RID: 63449 RVA: 0x00381C6B File Offset: 0x0037FE6B
		[DefaultValue("Descrease")]
		[NotifyParentProperty(true)]
		public string SliderDecreaseText
		{
			get
			{
				return this.GetString("SliderDecreaseText");
			}
			set
			{
				this.SetString("SliderDecreaseText", value);
			}
		}

		// Token: 0x17004AC8 RID: 19144
		// (get) Token: 0x0600F7DA RID: 63450 RVA: 0x00381C79 File Offset: 0x0037FE79
		// (set) Token: 0x0600F7DB RID: 63451 RVA: 0x00381C86 File Offset: 0x0037FE86
		[NotifyParentProperty(true)]
		[DefaultValue("Drag")]
		public string SliderDragText
		{
			get
			{
				return this.GetString("SliderDragText");
			}
			set
			{
				this.SetString("SliderDragText", value);
			}
		}

		// Token: 0x17004AC9 RID: 19145
		// (get) Token: 0x0600F7DC RID: 63452 RVA: 0x00381C94 File Offset: 0x0037FE94
		// (set) Token: 0x0600F7DD RID: 63453 RVA: 0x00381CA1 File Offset: 0x0037FEA1
		[DefaultValue("Click here to sort")]
		[NotifyParentProperty(true)]
		public string SortToolTip
		{
			get
			{
				return this.GetString("SortToolTip");
			}
			set
			{
				this.SetString("SortToolTip", value);
			}
		}

		// Token: 0x17004ACA RID: 19146
		// (get) Token: 0x0600F7DE RID: 63454 RVA: 0x00381CAF File Offset: 0x0037FEAF
		// (set) Token: 0x0600F7DF RID: 63455 RVA: 0x00381CBC File Offset: 0x0037FEBC
		[NotifyParentProperty(true)]
		[DefaultValue("Sorted asc")]
		public string SortedAscToolTip
		{
			get
			{
				return this.GetString("SortedAscToolTip");
			}
			set
			{
				this.SetString("SortedAscToolTip", value);
			}
		}

		// Token: 0x17004ACB RID: 19147
		// (get) Token: 0x0600F7E0 RID: 63456 RVA: 0x00381CCA File Offset: 0x0037FECA
		// (set) Token: 0x0600F7E1 RID: 63457 RVA: 0x00381CD7 File Offset: 0x0037FED7
		[NotifyParentProperty(true)]
		[DefaultValue("Sorted desc")]
		public string SortedDescToolTip
		{
			get
			{
				return this.GetString("SortedDescToolTip");
			}
			set
			{
				this.SetString("SortedDescToolTip", value);
			}
		}

		// Token: 0x17004ACC RID: 19148
		// (get) Token: 0x0600F7E2 RID: 63458 RVA: 0x00381CE5 File Offset: 0x0037FEE5
		// (set) Token: 0x0600F7E3 RID: 63459 RVA: 0x00381CF2 File Offset: 0x0037FEF2
		[NotifyParentProperty(true)]
		[DefaultValue("Ready")]
		public string StatusReadyText
		{
			get
			{
				return this.GetString("StatusReadyText");
			}
			set
			{
				this.SetString("StatusReadyText", value);
			}
		}

		// Token: 0x17004ACD RID: 19149
		// (get) Token: 0x0600F7E4 RID: 63460 RVA: 0x00381D00 File Offset: 0x0037FF00
		// (set) Token: 0x0600F7E5 RID: 63461 RVA: 0x00381D0D File Offset: 0x0037FF0D
		[DefaultValue("Loading...")]
		[NotifyParentProperty(true)]
		public string LoadingText
		{
			get
			{
				return this.GetString("LoadingText");
			}
			set
			{
				this.SetString("LoadingText", value);
			}
		}

		// Token: 0x0600F7E6 RID: 63462 RVA: 0x00381D1B File Offset: 0x0037FF1B
		public string GetStringFromViewState(string key)
		{
			return base.GetString(key);
		}

		// Token: 0x17004ACE RID: 19150
		// (get) Token: 0x0600F7E7 RID: 63463 RVA: 0x00381D24 File Offset: 0x0037FF24
		// (set) Token: 0x0600F7E8 RID: 63464 RVA: 0x00381D31 File Offset: 0x0037FF31
		[DefaultValue("NoFilter")]
		public string NoFilterText
		{
			get
			{
				return this.GetStringFromViewState("NoFilterText");
			}
			set
			{
				base.SetString("NoFilterText", value);
			}
		}

		// Token: 0x17004ACF RID: 19151
		// (get) Token: 0x0600F7E9 RID: 63465 RVA: 0x00381D3F File Offset: 0x0037FF3F
		// (set) Token: 0x0600F7EA RID: 63466 RVA: 0x00381D4C File Offset: 0x0037FF4C
		[DefaultValue("Contains")]
		public string ContainsText
		{
			get
			{
				return this.GetStringFromViewState("ContainsText");
			}
			set
			{
				base.SetString("ContainsText", value);
			}
		}

		// Token: 0x17004AD0 RID: 19152
		// (get) Token: 0x0600F7EB RID: 63467 RVA: 0x00381D5A File Offset: 0x0037FF5A
		// (set) Token: 0x0600F7EC RID: 63468 RVA: 0x00381D67 File Offset: 0x0037FF67
		[DefaultValue("DoesNotContain")]
		public string DoesNotContainText
		{
			get
			{
				return this.GetStringFromViewState("DoesNotContainText");
			}
			set
			{
				base.SetString("DoesNotContainText", value);
			}
		}

		// Token: 0x17004AD1 RID: 19153
		// (get) Token: 0x0600F7ED RID: 63469 RVA: 0x00381D75 File Offset: 0x0037FF75
		// (set) Token: 0x0600F7EE RID: 63470 RVA: 0x00381D82 File Offset: 0x0037FF82
		[DefaultValue("StartsWith")]
		public string StartsWithText
		{
			get
			{
				return this.GetStringFromViewState("StartsWithText");
			}
			set
			{
				base.SetString("StartsWithText", value);
			}
		}

		// Token: 0x17004AD2 RID: 19154
		// (get) Token: 0x0600F7EF RID: 63471 RVA: 0x00381D90 File Offset: 0x0037FF90
		// (set) Token: 0x0600F7F0 RID: 63472 RVA: 0x00381D9D File Offset: 0x0037FF9D
		[DefaultValue("EndsWith")]
		public string EndsWithText
		{
			get
			{
				return this.GetStringFromViewState("EndsWithText");
			}
			set
			{
				base.SetString("EndsWithText", value);
			}
		}

		// Token: 0x17004AD3 RID: 19155
		// (get) Token: 0x0600F7F1 RID: 63473 RVA: 0x00381DAB File Offset: 0x0037FFAB
		// (set) Token: 0x0600F7F2 RID: 63474 RVA: 0x00381DB8 File Offset: 0x0037FFB8
		[DefaultValue("EqualTo")]
		public string EqualToText
		{
			get
			{
				return this.GetStringFromViewState("EqualToText");
			}
			set
			{
				base.SetString("EqualToText", value);
			}
		}

		// Token: 0x17004AD4 RID: 19156
		// (get) Token: 0x0600F7F3 RID: 63475 RVA: 0x00381DC6 File Offset: 0x0037FFC6
		// (set) Token: 0x0600F7F4 RID: 63476 RVA: 0x00381DD3 File Offset: 0x0037FFD3
		[DefaultValue("NotEqualTo")]
		public string NotEqualToText
		{
			get
			{
				return this.GetStringFromViewState("NotEqualToText");
			}
			set
			{
				base.SetString("NotEqualToText", value);
			}
		}

		// Token: 0x17004AD5 RID: 19157
		// (get) Token: 0x0600F7F5 RID: 63477 RVA: 0x00381DE1 File Offset: 0x0037FFE1
		// (set) Token: 0x0600F7F6 RID: 63478 RVA: 0x00381DEE File Offset: 0x0037FFEE
		[DefaultValue("GreaterThan")]
		public string GreaterThanText
		{
			get
			{
				return this.GetStringFromViewState("GreaterThanText");
			}
			set
			{
				base.SetString("GreaterThanText", value);
			}
		}

		// Token: 0x17004AD6 RID: 19158
		// (get) Token: 0x0600F7F7 RID: 63479 RVA: 0x00381DFC File Offset: 0x0037FFFC
		// (set) Token: 0x0600F7F8 RID: 63480 RVA: 0x00381E09 File Offset: 0x00380009
		[DefaultValue("LessThan")]
		public string LessThanText
		{
			get
			{
				return this.GetStringFromViewState("LessThanText");
			}
			set
			{
				base.SetString("LessThanText", value);
			}
		}

		// Token: 0x17004AD7 RID: 19159
		// (get) Token: 0x0600F7F9 RID: 63481 RVA: 0x00381E17 File Offset: 0x00380017
		// (set) Token: 0x0600F7FA RID: 63482 RVA: 0x00381E24 File Offset: 0x00380024
		[DefaultValue("GreaterThanOrEqualTo")]
		public string GreaterThanOrEqualToText
		{
			get
			{
				return this.GetStringFromViewState("GreaterThanOrEqualToText");
			}
			set
			{
				base.SetString("GreaterThanOrEqualToText", value);
			}
		}

		// Token: 0x17004AD8 RID: 19160
		// (get) Token: 0x0600F7FB RID: 63483 RVA: 0x00381E32 File Offset: 0x00380032
		// (set) Token: 0x0600F7FC RID: 63484 RVA: 0x00381E3F File Offset: 0x0038003F
		[DefaultValue("LessThanOrEqualTo")]
		public string LessThanOrEqualToText
		{
			get
			{
				return this.GetStringFromViewState("LessThanOrEqualToText");
			}
			set
			{
				base.SetString("LessThanOrEqualToText", value);
			}
		}

		// Token: 0x17004AD9 RID: 19161
		// (get) Token: 0x0600F7FD RID: 63485 RVA: 0x00381E4D File Offset: 0x0038004D
		// (set) Token: 0x0600F7FE RID: 63486 RVA: 0x00381E5A File Offset: 0x0038005A
		[DefaultValue("Between")]
		public string BetweenText
		{
			get
			{
				return this.GetStringFromViewState("BetweenText");
			}
			set
			{
				base.SetString("BetweenText", value);
			}
		}

		// Token: 0x17004ADA RID: 19162
		// (get) Token: 0x0600F7FF RID: 63487 RVA: 0x00381E68 File Offset: 0x00380068
		// (set) Token: 0x0600F800 RID: 63488 RVA: 0x00381E75 File Offset: 0x00380075
		[DefaultValue("NotBetween")]
		public string NotBetweenText
		{
			get
			{
				return this.GetStringFromViewState("NotBetweenText");
			}
			set
			{
				base.SetString("NotBetweenText", value);
			}
		}

		// Token: 0x17004ADB RID: 19163
		// (get) Token: 0x0600F801 RID: 63489 RVA: 0x00381E83 File Offset: 0x00380083
		// (set) Token: 0x0600F802 RID: 63490 RVA: 0x00381E90 File Offset: 0x00380090
		[DefaultValue("IsEmpty")]
		public string IsEmptyText
		{
			get
			{
				return this.GetStringFromViewState("IsEmptyText");
			}
			set
			{
				base.SetString("IsEmptyText", value);
			}
		}

		// Token: 0x17004ADC RID: 19164
		// (get) Token: 0x0600F803 RID: 63491 RVA: 0x00381E9E File Offset: 0x0038009E
		// (set) Token: 0x0600F804 RID: 63492 RVA: 0x00381EAB File Offset: 0x003800AB
		[DefaultValue("NotIsEmpty")]
		public string NotIsEmptyText
		{
			get
			{
				return this.GetStringFromViewState("NotIsEmptyText");
			}
			set
			{
				base.SetString("NotIsEmptyText", value);
			}
		}

		// Token: 0x17004ADD RID: 19165
		// (get) Token: 0x0600F805 RID: 63493 RVA: 0x00381EB9 File Offset: 0x003800B9
		// (set) Token: 0x0600F806 RID: 63494 RVA: 0x00381EC6 File Offset: 0x003800C6
		[DefaultValue("IsNull")]
		public string IsNullText
		{
			get
			{
				return this.GetStringFromViewState("IsNullText");
			}
			set
			{
				base.SetString("IsNullText", value);
			}
		}

		// Token: 0x17004ADE RID: 19166
		// (get) Token: 0x0600F807 RID: 63495 RVA: 0x00381ED4 File Offset: 0x003800D4
		// (set) Token: 0x0600F808 RID: 63496 RVA: 0x00381EE1 File Offset: 0x003800E1
		[DefaultValue("NotIsNull")]
		public string NotIsNullText
		{
			get
			{
				return this.GetStringFromViewState("NotIsNullText");
			}
			set
			{
				base.SetString("NotIsNullText", value);
			}
		}

		// Token: 0x17004ADF RID: 19167
		// (get) Token: 0x0600F809 RID: 63497 RVA: 0x00381EEF File Offset: 0x003800EF
		// (set) Token: 0x0600F80A RID: 63498 RVA: 0x00381EFC File Offset: 0x003800FC
		[DefaultValue("Custom")]
		public string CustomText
		{
			get
			{
				return this.GetStringFromViewState("CustomText");
			}
			set
			{
				base.SetString("CustomText", value);
			}
		}

		// Token: 0x17004AE0 RID: 19168
		// (get) Token: 0x0600F80B RID: 63499 RVA: 0x00381F0A File Offset: 0x0038010A
		// (set) Token: 0x0600F80C RID: 63500 RVA: 0x00381F17 File Offset: 0x00380117
		[DefaultValue("Sort Ascending")]
		public string HeaderContextMenuSortAsc
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuSortAsc");
			}
			set
			{
				base.SetString("HeaderContextMenuSortAsc", value);
			}
		}

		// Token: 0x17004AE1 RID: 19169
		// (get) Token: 0x0600F80D RID: 63501 RVA: 0x00381F25 File Offset: 0x00380125
		// (set) Token: 0x0600F80E RID: 63502 RVA: 0x00381F32 File Offset: 0x00380132
		[DefaultValue("Sort Descending")]
		public string HeaderContextMenuSortDesc
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuSortDesc");
			}
			set
			{
				base.SetString("HeaderContextMenuSortDesc", value);
			}
		}

		// Token: 0x17004AE2 RID: 19170
		// (get) Token: 0x0600F80F RID: 63503 RVA: 0x00381F40 File Offset: 0x00380140
		// (set) Token: 0x0600F810 RID: 63504 RVA: 0x00381F4D File Offset: 0x0038014D
		[DefaultValue("Clear Sorting")]
		public string HeaderContextMenuSortClear
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuSortClear");
			}
			set
			{
				base.SetString("HeaderContextMenuSortClear", value);
			}
		}

		// Token: 0x17004AE3 RID: 19171
		// (get) Token: 0x0600F811 RID: 63505 RVA: 0x00381F5B File Offset: 0x0038015B
		// (set) Token: 0x0600F812 RID: 63506 RVA: 0x00381F68 File Offset: 0x00380168
		[DefaultValue("Group By")]
		public string HeaderContextMenuGroupBy
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuGroupBy");
			}
			set
			{
				base.SetString("HeaderContextMenuGroupBy", value);
			}
		}

		// Token: 0x17004AE4 RID: 19172
		// (get) Token: 0x0600F813 RID: 63507 RVA: 0x00381F76 File Offset: 0x00380176
		// (set) Token: 0x0600F814 RID: 63508 RVA: 0x00381F83 File Offset: 0x00380183
		[DefaultValue("UnGroupBy")]
		public string HeaderContextMenuUnGroupBy
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuUnGroupBy");
			}
			set
			{
				base.SetString("HeaderContextMenuUnGroupBy", value);
			}
		}

		// Token: 0x17004AE5 RID: 19173
		// (get) Token: 0x0600F815 RID: 63509 RVA: 0x00381F91 File Offset: 0x00380191
		// (set) Token: 0x0600F816 RID: 63510 RVA: 0x00381F9E File Offset: 0x0038019E
		[DefaultValue("Freeze")]
		public string HeaderContextMenuFreeze
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuFreeze");
			}
			set
			{
				base.SetString("HeaderContextMenuFreeze", value);
			}
		}

		// Token: 0x17004AE6 RID: 19174
		// (get) Token: 0x0600F817 RID: 63511 RVA: 0x00381FAC File Offset: 0x003801AC
		// (set) Token: 0x0600F818 RID: 63512 RVA: 0x00381FB9 File Offset: 0x003801B9
		[DefaultValue("Unfreeze")]
		public string HeaderContextMenuUnfreeze
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuUnfreeze");
			}
			set
			{
				base.SetString("HeaderContextMenuUnfreeze", value);
			}
		}

		// Token: 0x17004AE7 RID: 19175
		// (get) Token: 0x0600F819 RID: 63513 RVA: 0x00381FC7 File Offset: 0x003801C7
		// (set) Token: 0x0600F81A RID: 63514 RVA: 0x00381FD4 File Offset: 0x003801D4
		[DefaultValue("Columns")]
		public string HeaderContextMenuColumns
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuColumns");
			}
			set
			{
				base.SetString("HeaderContextMenuColumns", value);
			}
		}

		// Token: 0x17004AE8 RID: 19176
		// (get) Token: 0x0600F81B RID: 63515 RVA: 0x00381FE2 File Offset: 0x003801E2
		// (set) Token: 0x0600F81C RID: 63516 RVA: 0x00381FEF File Offset: 0x003801EF
		[DefaultValue("Show rows with value that")]
		public string HeaderContextMenuRowsLabel
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuRowsLabel");
			}
			set
			{
				base.SetString("HeaderContextMenuRowsLabel", value);
			}
		}

		// Token: 0x17004AE9 RID: 19177
		// (get) Token: 0x0600F81D RID: 63517 RVA: 0x00381FFD File Offset: 0x003801FD
		// (set) Token: 0x0600F81E RID: 63518 RVA: 0x0038200A File Offset: 0x0038020A
		[DefaultValue("And")]
		public string HeaderContextMenuAndLabel
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuAndLabel");
			}
			set
			{
				base.SetString("HeaderContextMenuAndLabel", value);
			}
		}

		// Token: 0x17004AEA RID: 19178
		// (get) Token: 0x0600F81F RID: 63519 RVA: 0x00382018 File Offset: 0x00380218
		// (set) Token: 0x0600F820 RID: 63520 RVA: 0x00382025 File Offset: 0x00380225
		[DefaultValue("Filter")]
		public string HeaderContextMenuFilterButton
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuFilterButton");
			}
			set
			{
				base.SetString("HeaderContextMenuFilterButton", value);
			}
		}

		// Token: 0x17004AEB RID: 19179
		// (get) Token: 0x0600F821 RID: 63521 RVA: 0x00382033 File Offset: 0x00380233
		// (set) Token: 0x0600F822 RID: 63522 RVA: 0x00382040 File Offset: 0x00380240
		[DefaultValue("Clear Filter")]
		public string HeaderContextMenuClearButton
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuClearButton");
			}
			set
			{
				base.SetString("HeaderContextMenuClearButton", value);
			}
		}

		// Token: 0x17004AEC RID: 19180
		// (get) Token: 0x0600F823 RID: 63523 RVA: 0x0038204E File Offset: 0x0038024E
		// (set) Token: 0x0600F824 RID: 63524 RVA: 0x0038205B File Offset: 0x0038025B
		[DefaultValue("Filter")]
		public string HeaderContextMenuFilterItemText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuFilterItemText");
			}
			set
			{
				base.SetString("HeaderContextMenuFilterItemText", value);
			}
		}

		// Token: 0x17004AED RID: 19181
		// (get) Token: 0x0600F825 RID: 63525 RVA: 0x00382069 File Offset: 0x00380269
		// (set) Token: 0x0600F826 RID: 63526 RVA: 0x00382076 File Offset: 0x00380276
		[DefaultValue("Best Fit")]
		public string HeaderContextMenuBestFitText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuBestFitText");
			}
			set
			{
				base.SetString("HeaderContextMenuBestFitText", value);
			}
		}

		// Token: 0x17004AEE RID: 19182
		// (get) Token: 0x0600F827 RID: 63527 RVA: 0x00382084 File Offset: 0x00380284
		// (set) Token: 0x0600F828 RID: 63528 RVA: 0x00382091 File Offset: 0x00380291
		[DefaultValue("Aggregates")]
		public string HeaderContextMenuAggregates
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuAggregates");
			}
			set
			{
				base.SetString("HeaderContextMenuAggregates", value);
			}
		}

		// Token: 0x17004AEF RID: 19183
		// (get) Token: 0x0600F829 RID: 63529 RVA: 0x0038209F File Offset: 0x0038029F
		// (set) Token: 0x0600F82A RID: 63530 RVA: 0x003820AC File Offset: 0x003802AC
		[DefaultValue("None")]
		public string HeaderContextMenuNoneAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuNoneAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuNoneAggregateText", value);
			}
		}

		// Token: 0x17004AF0 RID: 19184
		// (get) Token: 0x0600F82B RID: 63531 RVA: 0x003820BA File Offset: 0x003802BA
		// (set) Token: 0x0600F82C RID: 63532 RVA: 0x003820C7 File Offset: 0x003802C7
		[DefaultValue("Sum")]
		public string HeaderContextMenuSumAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuSumAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuSumAggregateText", value);
			}
		}

		// Token: 0x17004AF1 RID: 19185
		// (get) Token: 0x0600F82D RID: 63533 RVA: 0x003820D5 File Offset: 0x003802D5
		// (set) Token: 0x0600F82E RID: 63534 RVA: 0x003820E2 File Offset: 0x003802E2
		[DefaultValue("Min")]
		public string HeaderContextMenuMinAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuMinAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuMinAggregateText", value);
			}
		}

		// Token: 0x17004AF2 RID: 19186
		// (get) Token: 0x0600F82F RID: 63535 RVA: 0x003820F0 File Offset: 0x003802F0
		// (set) Token: 0x0600F830 RID: 63536 RVA: 0x003820FD File Offset: 0x003802FD
		[DefaultValue("Max")]
		public string HeaderContextMenuMaxAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuMaxAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuMaxAggregateText", value);
			}
		}

		// Token: 0x17004AF3 RID: 19187
		// (get) Token: 0x0600F831 RID: 63537 RVA: 0x0038210B File Offset: 0x0038030B
		// (set) Token: 0x0600F832 RID: 63538 RVA: 0x00382118 File Offset: 0x00380318
		[DefaultValue("Last")]
		public string HeaderContextMenuLastAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuLastAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuLastAggregateText", value);
			}
		}

		// Token: 0x17004AF4 RID: 19188
		// (get) Token: 0x0600F833 RID: 63539 RVA: 0x00382126 File Offset: 0x00380326
		// (set) Token: 0x0600F834 RID: 63540 RVA: 0x00382133 File Offset: 0x00380333
		[DefaultValue("First")]
		public string HeaderContextMenuFirstAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuFirstAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuFirstAggregateText", value);
			}
		}

		// Token: 0x17004AF5 RID: 19189
		// (get) Token: 0x0600F835 RID: 63541 RVA: 0x00382141 File Offset: 0x00380341
		// (set) Token: 0x0600F836 RID: 63542 RVA: 0x0038214E File Offset: 0x0038034E
		[DefaultValue("Count")]
		public string HeaderContextMenuCountAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuCountAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuCountAggregateText", value);
			}
		}

		// Token: 0x17004AF6 RID: 19190
		// (get) Token: 0x0600F837 RID: 63543 RVA: 0x0038215C File Offset: 0x0038035C
		// (set) Token: 0x0600F838 RID: 63544 RVA: 0x00382169 File Offset: 0x00380369
		[DefaultValue("Avg")]
		public string HeaderContextMenuAvgAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuAvgAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuAvgAggregateText", value);
			}
		}

		// Token: 0x17004AF7 RID: 19191
		// (get) Token: 0x0600F839 RID: 63545 RVA: 0x00382177 File Offset: 0x00380377
		// (set) Token: 0x0600F83A RID: 63546 RVA: 0x00382184 File Offset: 0x00380384
		[DefaultValue("CountDistinct")]
		public string HeaderContextMenuCountDistinctAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuCountDistinctAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuCountDistinctAggregateText", value);
			}
		}

		// Token: 0x17004AF8 RID: 19192
		// (get) Token: 0x0600F83B RID: 63547 RVA: 0x00382192 File Offset: 0x00380392
		// (set) Token: 0x0600F83C RID: 63548 RVA: 0x0038219F File Offset: 0x0038039F
		[DefaultValue("Custom")]
		public string HeaderContextMenuCustomAggregateText
		{
			get
			{
				return this.GetStringFromViewState("HeaderContextMenuCustomAggregateText");
			}
			set
			{
				base.SetString("HeaderContextMenuCustomAggregateText", value);
			}
		}

		// Token: 0x17004AF9 RID: 19193
		// (get) Token: 0x0600F83D RID: 63549 RVA: 0x003821AD File Offset: 0x003803AD
		// (set) Token: 0x0600F83E RID: 63550 RVA: 0x003821BA File Offset: 0x003803BA
		[DefaultValue("Sum")]
		public string AggregateFunctionSum
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionSum");
			}
			set
			{
				base.SetString("AggregateFunctionSum", value);
			}
		}

		// Token: 0x17004AFA RID: 19194
		// (get) Token: 0x0600F83F RID: 63551 RVA: 0x003821C8 File Offset: 0x003803C8
		// (set) Token: 0x0600F840 RID: 63552 RVA: 0x003821D5 File Offset: 0x003803D5
		[DefaultValue("Min")]
		public string AggregateFunctionMin
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionMin");
			}
			set
			{
				base.SetString("AggregateFunctionMin", value);
			}
		}

		// Token: 0x17004AFB RID: 19195
		// (get) Token: 0x0600F841 RID: 63553 RVA: 0x003821E3 File Offset: 0x003803E3
		// (set) Token: 0x0600F842 RID: 63554 RVA: 0x003821F0 File Offset: 0x003803F0
		[DefaultValue("Max")]
		public string AggregateFunctionMax
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionMax");
			}
			set
			{
				base.SetString("AggregateFunctionMax", value);
			}
		}

		// Token: 0x17004AFC RID: 19196
		// (get) Token: 0x0600F843 RID: 63555 RVA: 0x003821FE File Offset: 0x003803FE
		// (set) Token: 0x0600F844 RID: 63556 RVA: 0x0038220B File Offset: 0x0038040B
		[DefaultValue("Last")]
		public string AggregateFunctionLast
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionLast");
			}
			set
			{
				base.SetString("AggregateFunctionLast", value);
			}
		}

		// Token: 0x17004AFD RID: 19197
		// (get) Token: 0x0600F845 RID: 63557 RVA: 0x00382219 File Offset: 0x00380419
		// (set) Token: 0x0600F846 RID: 63558 RVA: 0x00382226 File Offset: 0x00380426
		[DefaultValue("First")]
		public string AggregateFunctionFirst
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionFirst");
			}
			set
			{
				base.SetString("AggregateFunctionFirst", value);
			}
		}

		// Token: 0x17004AFE RID: 19198
		// (get) Token: 0x0600F847 RID: 63559 RVA: 0x00382234 File Offset: 0x00380434
		// (set) Token: 0x0600F848 RID: 63560 RVA: 0x00382241 File Offset: 0x00380441
		[DefaultValue("Count")]
		public string AggregateFunctionCount
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionCount");
			}
			set
			{
				base.SetString("AggregateFunctionCount", value);
			}
		}

		// Token: 0x17004AFF RID: 19199
		// (get) Token: 0x0600F849 RID: 63561 RVA: 0x0038224F File Offset: 0x0038044F
		// (set) Token: 0x0600F84A RID: 63562 RVA: 0x0038225C File Offset: 0x0038045C
		[DefaultValue("Avg")]
		public string AggregateFunctionAvg
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionAvg");
			}
			set
			{
				base.SetString("AggregateFunctionAvg", value);
			}
		}

		// Token: 0x17004B00 RID: 19200
		// (get) Token: 0x0600F84B RID: 63563 RVA: 0x0038226A File Offset: 0x0038046A
		// (set) Token: 0x0600F84C RID: 63564 RVA: 0x00382277 File Offset: 0x00380477
		[DefaultValue("CountDistinct")]
		public string AggregateFunctionCountDistinct
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionCountDistinct");
			}
			set
			{
				base.SetString("AggregateFunctionCountDistinct", value);
			}
		}

		// Token: 0x17004B01 RID: 19201
		// (get) Token: 0x0600F84D RID: 63565 RVA: 0x00382285 File Offset: 0x00380485
		// (set) Token: 0x0600F84E RID: 63566 RVA: 0x00382292 File Offset: 0x00380492
		[DefaultValue("Custom")]
		public string AggregateFunctionCustom
		{
			get
			{
				return this.GetStringFromViewState("AggregateFunctionCustom");
			}
			set
			{
				base.SetString("AggregateFunctionCustom", value);
			}
		}

		// Token: 0x17004B02 RID: 19202
		// (get) Token: 0x0600F84F RID: 63567 RVA: 0x003822A0 File Offset: 0x003804A0
		// (set) Token: 0x0600F850 RID: 63568 RVA: 0x003822AD File Offset: 0x003804AD
		[DefaultValue("Back")]
		public string MobileViewBackButtonText
		{
			get
			{
				return this.GetStringFromViewState("MobileViewBackButtonText");
			}
			set
			{
				base.SetString("MobileViewBackButtonText", value);
			}
		}

		// Token: 0x17004B03 RID: 19203
		// (get) Token: 0x0600F851 RID: 63569 RVA: 0x003822BB File Offset: 0x003804BB
		// (set) Token: 0x0600F852 RID: 63570 RVA: 0x003822C8 File Offset: 0x003804C8
		[DefaultValue("Cancel")]
		public string MobileViewCancelButtonText
		{
			get
			{
				return this.GetStringFromViewState("MobileViewCancelButtonText");
			}
			set
			{
				base.SetString("MobileViewCancelButtonText", value);
			}
		}

		// Token: 0x17004B04 RID: 19204
		// (get) Token: 0x0600F853 RID: 63571 RVA: 0x003822D6 File Offset: 0x003804D6
		// (set) Token: 0x0600F854 RID: 63572 RVA: 0x003822E3 File Offset: 0x003804E3
		[DefaultValue("Done")]
		public string MobileViewDoneButtonText
		{
			get
			{
				return this.GetStringFromViewState("MobileViewDoneButtonText");
			}
			set
			{
				base.SetString("MobileViewDoneButtonText", value);
			}
		}

		// Token: 0x17004B05 RID: 19205
		// (get) Token: 0x0600F855 RID: 63573 RVA: 0x003822F1 File Offset: 0x003804F1
		// (set) Token: 0x0600F856 RID: 63574 RVA: 0x003822FE File Offset: 0x003804FE
		[DefaultValue("Columns Display")]
		public string MobileColumnsViewTitle
		{
			get
			{
				return this.GetStringFromViewState("MobileColumnsViewTitle");
			}
			set
			{
				base.SetString("MobileColumnsViewTitle", value);
			}
		}

		// Token: 0x17004B06 RID: 19206
		// (get) Token: 0x0600F857 RID: 63575 RVA: 0x0038230C File Offset: 0x0038050C
		// (set) Token: 0x0600F858 RID: 63576 RVA: 0x00382319 File Offset: 0x00380519
		[DefaultValue("Show/Hide Columns and Drag the Icon to Reorder")]
		public string MobileColumnsViewDescription
		{
			get
			{
				return this.GetStringFromViewState("MobileColumnsViewDescription");
			}
			set
			{
				base.SetString("MobileColumnsViewDescription", value);
			}
		}

		// Token: 0x17004B07 RID: 19207
		// (get) Token: 0x0600F859 RID: 63577 RVA: 0x00382327 File Offset: 0x00380527
		// (set) Token: 0x0600F85A RID: 63578 RVA: 0x00382334 File Offset: 0x00380534
		[DefaultValue("Insert")]
		public string MobileInsertViewTitle
		{
			get
			{
				return this.GetStringFromViewState("MobileInsertViewTitle");
			}
			set
			{
				base.SetString("MobileInsertViewTitle", value);
			}
		}

		// Token: 0x17004B08 RID: 19208
		// (get) Token: 0x0600F85B RID: 63579 RVA: 0x00382342 File Offset: 0x00380542
		// (set) Token: 0x0600F85C RID: 63580 RVA: 0x0038234F File Offset: 0x0038054F
		[DefaultValue("Edit")]
		public string MobileEditViewTitle
		{
			get
			{
				return this.GetStringFromViewState("MobileEditViewTitle");
			}
			set
			{
				base.SetString("MobileEditViewTitle", value);
			}
		}

		// Token: 0x17004B09 RID: 19209
		// (get) Token: 0x0600F85D RID: 63581 RVA: 0x0038235D File Offset: 0x0038055D
		// (set) Token: 0x0600F85E RID: 63582 RVA: 0x0038236A File Offset: 0x0038056A
		[DefaultValue("Filter by {0}")]
		public string MobileFilterViewTitleFormat
		{
			get
			{
				return this.GetStringFromViewState("MobileFilterViewTitleFormat");
			}
			set
			{
				base.SetString("MobileFilterViewTitleFormat", value);
			}
		}

		// Token: 0x17004B0A RID: 19210
		// (get) Token: 0x0600F85F RID: 63583 RVA: 0x00382378 File Offset: 0x00380578
		// (set) Token: 0x0600F860 RID: 63584 RVA: 0x00382385 File Offset: 0x00380585
		[DefaultValue("Options")]
		public string MobileFilterViewOptionsText
		{
			get
			{
				return this.GetStringFromViewState("MobileFilterViewOptionsText");
			}
			set
			{
				base.SetString("MobileFilterViewOptionsText", value);
			}
		}

		// Token: 0x17004B0B RID: 19211
		// (get) Token: 0x0600F861 RID: 63585 RVA: 0x00382393 File Offset: 0x00380593
		// (set) Token: 0x0600F862 RID: 63586 RVA: 0x003823A0 File Offset: 0x003805A0
		[DefaultValue("Value")]
		public string MobileFilterViewValueText
		{
			get
			{
				return this.GetStringFromViewState("MobileFilterViewValueText");
			}
			set
			{
				base.SetString("MobileFilterViewValueText", value);
			}
		}

		// Token: 0x17004B0C RID: 19212
		// (get) Token: 0x0600F863 RID: 63587 RVA: 0x003823AE File Offset: 0x003805AE
		// (set) Token: 0x0600F864 RID: 63588 RVA: 0x003823BB File Offset: 0x003805BB
		[DefaultValue("View Groups")]
		public string MobileViewGroupsText
		{
			get
			{
				return this.GetStringFromViewState("MobileViewGroupsText");
			}
			set
			{
				base.SetString("MobileViewGroupsText", value);
			}
		}

		// Token: 0x040046C1 RID: 18113
		private readonly LocalizationProvider _localizationProvider;
	}
}
