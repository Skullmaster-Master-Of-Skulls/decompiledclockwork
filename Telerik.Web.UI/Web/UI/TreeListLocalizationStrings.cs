using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001273 RID: 4723
	internal class TreeListLocalizationStrings : LocalizationStrings
	{
		// Token: 0x0600C47F RID: 50303 RVA: 0x002BFC99 File Offset: 0x002BDE99
		public TreeListLocalizationStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x0600C480 RID: 50304 RVA: 0x002BFCA9 File Offset: 0x002BDEA9
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x17003F4C RID: 16204
		// (get) Token: 0x0600C481 RID: 50305 RVA: 0x002BFCC2 File Offset: 0x002BDEC2
		// (set) Token: 0x0600C482 RID: 50306 RVA: 0x002BFCCF File Offset: 0x002BDECF
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F4D RID: 16205
		// (get) Token: 0x0600C483 RID: 50307 RVA: 0x002BFCDD File Offset: 0x002BDEDD
		// (set) Token: 0x0600C484 RID: 50308 RVA: 0x002BFCEA File Offset: 0x002BDEEA
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F4E RID: 16206
		// (get) Token: 0x0600C485 RID: 50309 RVA: 0x002BFCF8 File Offset: 0x002BDEF8
		// (set) Token: 0x0600C486 RID: 50310 RVA: 0x002BFD05 File Offset: 0x002BDF05
		[NotifyParentProperty(true)]
		[DefaultValue("Export to Excel")]
		public string ExportToExcelText
		{
			get
			{
				return this.GetString("ExportToExcelText");
			}
			set
			{
				this.SetString("Export to Excel", value);
			}
		}

		// Token: 0x17003F4F RID: 16207
		// (get) Token: 0x0600C487 RID: 50311 RVA: 0x002BFD13 File Offset: 0x002BDF13
		// (set) Token: 0x0600C488 RID: 50312 RVA: 0x002BFD20 File Offset: 0x002BDF20
		[NotifyParentProperty(true)]
		[DefaultValue("Export to Word")]
		public string ExportToWordText
		{
			get
			{
				return this.GetString("ExportToWordText");
			}
			set
			{
				this.SetString("Export to Word", value);
			}
		}

		// Token: 0x17003F50 RID: 16208
		// (get) Token: 0x0600C489 RID: 50313 RVA: 0x002BFD2E File Offset: 0x002BDF2E
		// (set) Token: 0x0600C48A RID: 50314 RVA: 0x002BFD3B File Offset: 0x002BDF3B
		[DefaultValue("Export to PDF")]
		[NotifyParentProperty(true)]
		public string ExportToPdfText
		{
			get
			{
				return this.GetString("ExportToPdfText");
			}
			set
			{
				this.SetString("Export to PDF", value);
			}
		}

		// Token: 0x17003F51 RID: 16209
		// (get) Token: 0x0600C48B RID: 50315 RVA: 0x002BFD49 File Offset: 0x002BDF49
		// (set) Token: 0x0600C48C RID: 50316 RVA: 0x002BFD56 File Offset: 0x002BDF56
		[NotifyParentProperty(true)]
		[DefaultValue("Drop here to reorder")]
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

		// Token: 0x17003F52 RID: 16210
		// (get) Token: 0x0600C48D RID: 50317 RVA: 0x002BFD64 File Offset: 0x002BDF64
		// (set) Token: 0x0600C48E RID: 50318 RVA: 0x002BFD71 File Offset: 0x002BDF71
		[DefaultValue("Drag to reorder")]
		[NotifyParentProperty(true)]
		public string DragToReorder
		{
			get
			{
				return this.GetString("DragToReorder");
			}
			set
			{
				this.SetString("DragToReorder", value);
			}
		}

		// Token: 0x17003F53 RID: 16211
		// (get) Token: 0x0600C48F RID: 50319 RVA: 0x002BFD7F File Offset: 0x002BDF7F
		// (set) Token: 0x0600C490 RID: 50320 RVA: 0x002BFD8C File Offset: 0x002BDF8C
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

		// Token: 0x17003F54 RID: 16212
		// (get) Token: 0x0600C491 RID: 50321 RVA: 0x002BFD9A File Offset: 0x002BDF9A
		// (set) Token: 0x0600C492 RID: 50322 RVA: 0x002BFDA7 File Offset: 0x002BDFA7
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

		// Token: 0x17003F55 RID: 16213
		// (get) Token: 0x0600C493 RID: 50323 RVA: 0x002BFDB5 File Offset: 0x002BDFB5
		// (set) Token: 0x0600C494 RID: 50324 RVA: 0x002BFDC2 File Offset: 0x002BDFC2
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ExpandToolTip
		{
			get
			{
				return this.GetString("ExpandToolTip");
			}
			set
			{
				this.SetString("ExpandToolTip", value);
			}
		}

		// Token: 0x17003F56 RID: 16214
		// (get) Token: 0x0600C495 RID: 50325 RVA: 0x002BFDD0 File Offset: 0x002BDFD0
		// (set) Token: 0x0600C496 RID: 50326 RVA: 0x002BFDDD File Offset: 0x002BDFDD
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string CollapseToolTip
		{
			get
			{
				return this.GetString("CollapseToolTip");
			}
			set
			{
				this.SetString("CollapseToolTip", value);
			}
		}

		// Token: 0x17003F57 RID: 16215
		// (get) Token: 0x0600C497 RID: 50327 RVA: 0x002BFDEB File Offset: 0x002BDFEB
		// (set) Token: 0x0600C498 RID: 50328 RVA: 0x002BFDF8 File Offset: 0x002BDFF8
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

		// Token: 0x17003F58 RID: 16216
		// (get) Token: 0x0600C499 RID: 50329 RVA: 0x002BFE06 File Offset: 0x002BE006
		// (set) Token: 0x0600C49A RID: 50330 RVA: 0x002BFE13 File Offset: 0x002BE013
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

		// Token: 0x17003F59 RID: 16217
		// (get) Token: 0x0600C49B RID: 50331 RVA: 0x002BFE21 File Offset: 0x002BE021
		// (set) Token: 0x0600C49C RID: 50332 RVA: 0x002BFE2E File Offset: 0x002BE02E
		[DefaultValue("Add new record")]
		[NotifyParentProperty(true)]
		public string AddRecordText
		{
			get
			{
				return this.GetString("AddRecordText");
			}
			set
			{
				this.SetString("AddRecordText", value);
			}
		}

		// Token: 0x17003F5A RID: 16218
		// (get) Token: 0x0600C49D RID: 50333 RVA: 0x002BFE3C File Offset: 0x002BE03C
		// (set) Token: 0x0600C49E RID: 50334 RVA: 0x002BFE49 File Offset: 0x002BE049
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

		// Token: 0x17003F5B RID: 16219
		// (get) Token: 0x0600C49F RID: 50335 RVA: 0x002BFE57 File Offset: 0x002BE057
		// (set) Token: 0x0600C4A0 RID: 50336 RVA: 0x002BFE64 File Offset: 0x002BE064
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
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

		// Token: 0x17003F5C RID: 16220
		// (get) Token: 0x0600C4A1 RID: 50337 RVA: 0x002BFE72 File Offset: 0x002BE072
		// (set) Token: 0x0600C4A2 RID: 50338 RVA: 0x002BFE7F File Offset: 0x002BE07F
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

		// Token: 0x17003F5D RID: 16221
		// (get) Token: 0x0600C4A3 RID: 50339 RVA: 0x002BFE8D File Offset: 0x002BE08D
		// (set) Token: 0x0600C4A4 RID: 50340 RVA: 0x002BFE9A File Offset: 0x002BE09A
		[NotifyParentProperty(true)]
		[DefaultValue("Close")]
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

		// Token: 0x17003F5E RID: 16222
		// (get) Token: 0x0600C4A5 RID: 50341 RVA: 0x002BFEA8 File Offset: 0x002BE0A8
		// (set) Token: 0x0600C4A6 RID: 50342 RVA: 0x002BFEB5 File Offset: 0x002BE0B5
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

		// Token: 0x17003F5F RID: 16223
		// (get) Token: 0x0600C4A7 RID: 50343 RVA: 0x002BFEC3 File Offset: 0x002BE0C3
		// (set) Token: 0x0600C4A8 RID: 50344 RVA: 0x002BFED0 File Offset: 0x002BE0D0
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

		// Token: 0x17003F60 RID: 16224
		// (get) Token: 0x0600C4A9 RID: 50345 RVA: 0x002BFEDE File Offset: 0x002BE0DE
		// (set) Token: 0x0600C4AA RID: 50346 RVA: 0x002BFEEB File Offset: 0x002BE0EB
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

		// Token: 0x17003F61 RID: 16225
		// (get) Token: 0x0600C4AB RID: 50347 RVA: 0x002BFEF9 File Offset: 0x002BE0F9
		// (set) Token: 0x0600C4AC RID: 50348 RVA: 0x002BFF06 File Offset: 0x002BE106
		[NotifyParentProperty(true)]
		[DefaultValue("Next Page")]
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

		// Token: 0x17003F62 RID: 16226
		// (get) Token: 0x0600C4AD RID: 50349 RVA: 0x002BFF14 File Offset: 0x002BE114
		// (set) Token: 0x0600C4AE RID: 50350 RVA: 0x002BFF21 File Offset: 0x002BE121
		[DefaultValue("Increase")]
		[NotifyParentProperty(true)]
		public string PageSliderIncreaseToolTip
		{
			get
			{
				return this.GetString("PageSliderIncreaseToolTip");
			}
			set
			{
				this.SetString("PageSliderIncreaseToolTip", value);
			}
		}

		// Token: 0x17003F63 RID: 16227
		// (get) Token: 0x0600C4AF RID: 50351 RVA: 0x002BFF2F File Offset: 0x002BE12F
		// (set) Token: 0x0600C4B0 RID: 50352 RVA: 0x002BFF3C File Offset: 0x002BE13C
		[NotifyParentProperty(true)]
		[DefaultValue("Decrease")]
		public string PageSliderDecreaseToolTip
		{
			get
			{
				return this.GetString("PageSliderDecreaseToolTip");
			}
			set
			{
				this.SetString("PageSliderDecreaseToolTip", value);
			}
		}

		// Token: 0x17003F64 RID: 16228
		// (get) Token: 0x0600C4B1 RID: 50353 RVA: 0x002BFF4A File Offset: 0x002BE14A
		// (set) Token: 0x0600C4B2 RID: 50354 RVA: 0x002BFF57 File Offset: 0x002BE157
		[DefaultValue("Drag")]
		[NotifyParentProperty(true)]
		public string PageSliderDragToolTip
		{
			get
			{
				return this.GetString("PageSliderDragToolTip");
			}
			set
			{
				this.SetString("PageSliderDragToolTip", value);
			}
		}

		// Token: 0x17003F65 RID: 16229
		// (get) Token: 0x0600C4B3 RID: 50355 RVA: 0x002BFF65 File Offset: 0x002BE165
		// (set) Token: 0x0600C4B4 RID: 50356 RVA: 0x002BFF72 File Offset: 0x002BE172
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
		[NotifyParentProperty(true)]
		public string PageSliderPagerLabel
		{
			get
			{
				return this.GetString("PageSliderPagerLabel");
			}
			set
			{
				this.SetString("PageSliderPagerLabel", value);
			}
		}

		// Token: 0x17003F66 RID: 16230
		// (get) Token: 0x0600C4B5 RID: 50357 RVA: 0x002BFF80 File Offset: 0x002BE180
		// (set) Token: 0x0600C4B6 RID: 50358 RVA: 0x002BFF8D File Offset: 0x002BE18D
		[NotifyParentProperty(true)]
		[DefaultValue("Page size:")]
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

		// Token: 0x17003F67 RID: 16231
		// (get) Token: 0x0600C4B7 RID: 50359 RVA: 0x002BFF9B File Offset: 0x002BE19B
		// (set) Token: 0x0600C4B8 RID: 50360 RVA: 0x002BFFA8 File Offset: 0x002BE1A8
		[DefaultValue("Change")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F68 RID: 16232
		// (get) Token: 0x0600C4B9 RID: 50361 RVA: 0x002BFFB6 File Offset: 0x002BE1B6
		// (set) Token: 0x0600C4BA RID: 50362 RVA: 0x002BFFC3 File Offset: 0x002BE1C3
		[DefaultValue("Page:")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F69 RID: 16233
		// (get) Token: 0x0600C4BB RID: 50363 RVA: 0x002BFFD1 File Offset: 0x002BE1D1
		// (set) Token: 0x0600C4BC RID: 50364 RVA: 0x002BFFDE File Offset: 0x002BE1DE
		[NotifyParentProperty(true)]
		[DefaultValue("Go")]
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

		// Token: 0x17003F6A RID: 16234
		// (get) Token: 0x0600C4BD RID: 50365 RVA: 0x002BFFEC File Offset: 0x002BE1EC
		// (set) Token: 0x0600C4BE RID: 50366 RVA: 0x002BFFF9 File Offset: 0x002BE1F9
		[DefaultValue("of {0}")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F6B RID: 16235
		// (get) Token: 0x0600C4BF RID: 50367 RVA: 0x002C0007 File Offset: 0x002BE207
		// (set) Token: 0x0600C4C0 RID: 50368 RVA: 0x002C0014 File Offset: 0x002BE214
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

		// Token: 0x17003F6C RID: 16236
		// (get) Token: 0x0600C4C1 RID: 50369 RVA: 0x002C0022 File Offset: 0x002BE222
		// (set) Token: 0x0600C4C2 RID: 50370 RVA: 0x002C002F File Offset: 0x002BE22F
		[DefaultValue("Go to Page")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F6D RID: 16237
		// (get) Token: 0x0600C4C3 RID: 50371 RVA: 0x002C003D File Offset: 0x002BE23D
		// (set) Token: 0x0600C4C4 RID: 50372 RVA: 0x002C004A File Offset: 0x002BE24A
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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

		// Token: 0x17003F6E RID: 16238
		// (get) Token: 0x0600C4C5 RID: 50373 RVA: 0x002C0058 File Offset: 0x002BE258
		// (set) Token: 0x0600C4C6 RID: 50374 RVA: 0x002C0065 File Offset: 0x002BE265
		[DefaultValue("Change Page Size")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F6F RID: 16239
		// (get) Token: 0x0600C4C7 RID: 50375 RVA: 0x002C0073 File Offset: 0x002BE273
		// (set) Token: 0x0600C4C8 RID: 50376 RVA: 0x002C0080 File Offset: 0x002BE280
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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

		// Token: 0x17003F70 RID: 16240
		// (get) Token: 0x0600C4C9 RID: 50377 RVA: 0x002C008E File Offset: 0x002BE28E
		// (set) Token: 0x0600C4CA RID: 50378 RVA: 0x002C009B File Offset: 0x002BE29B
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F71 RID: 16241
		// (get) Token: 0x0600C4CB RID: 50379 RVA: 0x002C00A9 File Offset: 0x002BE2A9
		// (set) Token: 0x0600C4CC RID: 50380 RVA: 0x002C00B6 File Offset: 0x002BE2B6
		[NotifyParentProperty(true)]
		[DefaultValue("No records to display.")]
		public string NoRecordsText
		{
			get
			{
				return this.GetString("NoRecordsText");
			}
			set
			{
				this.SetString("NoRecordsText", value);
			}
		}

		// Token: 0x17003F72 RID: 16242
		// (get) Token: 0x0600C4CD RID: 50381 RVA: 0x002C00C4 File Offset: 0x002BE2C4
		// (set) Token: 0x0600C4CE RID: 50382 RVA: 0x002C00D1 File Offset: 0x002BE2D1
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

		// Token: 0x17003F73 RID: 16243
		// (get) Token: 0x0600C4CF RID: 50383 RVA: 0x002C00DF File Offset: 0x002BE2DF
		// (set) Token: 0x0600C4D0 RID: 50384 RVA: 0x002C00EC File Offset: 0x002BE2EC
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

		// Token: 0x17003F74 RID: 16244
		// (get) Token: 0x0600C4D1 RID: 50385 RVA: 0x002C00FA File Offset: 0x002BE2FA
		// (set) Token: 0x0600C4D2 RID: 50386 RVA: 0x002C0107 File Offset: 0x002BE307
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

		// Token: 0x17003F75 RID: 16245
		// (get) Token: 0x0600C4D3 RID: 50387 RVA: 0x002C0115 File Offset: 0x002BE315
		// (set) Token: 0x0600C4D4 RID: 50388 RVA: 0x002C0122 File Offset: 0x002BE322
		[DefaultValue("Back")]
		public string MobileViewBackButtonText
		{
			get
			{
				return this.GetString("MobileViewBackButtonText");
			}
			set
			{
				base.SetString("MobileViewBackButtonText", value);
			}
		}

		// Token: 0x17003F76 RID: 16246
		// (get) Token: 0x0600C4D5 RID: 50389 RVA: 0x002C0130 File Offset: 0x002BE330
		// (set) Token: 0x0600C4D6 RID: 50390 RVA: 0x002C013D File Offset: 0x002BE33D
		[DefaultValue("Cancel")]
		public string MobileViewCancelButtonText
		{
			get
			{
				return this.GetString("MobileViewCancelButtonText");
			}
			set
			{
				base.SetString("MobileViewCancelButtonText", value);
			}
		}

		// Token: 0x17003F77 RID: 16247
		// (get) Token: 0x0600C4D7 RID: 50391 RVA: 0x002C014B File Offset: 0x002BE34B
		// (set) Token: 0x0600C4D8 RID: 50392 RVA: 0x002C0158 File Offset: 0x002BE358
		[DefaultValue("Done")]
		public string MobileViewDoneButtonText
		{
			get
			{
				return this.GetString("MobileViewDoneButtonText");
			}
			set
			{
				base.SetString("MobileViewDoneButtonText", value);
			}
		}

		// Token: 0x17003F78 RID: 16248
		// (get) Token: 0x0600C4D9 RID: 50393 RVA: 0x002C0166 File Offset: 0x002BE366
		// (set) Token: 0x0600C4DA RID: 50394 RVA: 0x002C0173 File Offset: 0x002BE373
		[DefaultValue("Columns Display")]
		public string MobileColumnsViewTitle
		{
			get
			{
				return this.GetString("MobileColumnsViewTitle");
			}
			set
			{
				base.SetString("MobileColumnsViewTitle", value);
			}
		}

		// Token: 0x17003F79 RID: 16249
		// (get) Token: 0x0600C4DB RID: 50395 RVA: 0x002C0181 File Offset: 0x002BE381
		// (set) Token: 0x0600C4DC RID: 50396 RVA: 0x002C018E File Offset: 0x002BE38E
		[DefaultValue("Show/Hide Columns and Drag the Icon to Reorder")]
		public string MobileColumnsViewDescription
		{
			get
			{
				return this.GetString("MobileColumnsViewDescription");
			}
			set
			{
				base.SetString("MobileColumnsViewDescription", value);
			}
		}

		// Token: 0x17003F7A RID: 16250
		// (get) Token: 0x0600C4DD RID: 50397 RVA: 0x002C019C File Offset: 0x002BE39C
		// (set) Token: 0x0600C4DE RID: 50398 RVA: 0x002C01A9 File Offset: 0x002BE3A9
		[DefaultValue("Insert")]
		public string MobileInsertViewTitle
		{
			get
			{
				return this.GetString("MobileInsertViewTitle");
			}
			set
			{
				base.SetString("MobileInsertViewTitle", value);
			}
		}

		// Token: 0x17003F7B RID: 16251
		// (get) Token: 0x0600C4DF RID: 50399 RVA: 0x002C01B7 File Offset: 0x002BE3B7
		// (set) Token: 0x0600C4E0 RID: 50400 RVA: 0x002C01C4 File Offset: 0x002BE3C4
		[DefaultValue("Edit")]
		public string MobileEditViewTitle
		{
			get
			{
				return this.GetString("MobileEditViewTitle");
			}
			set
			{
				base.SetString("MobileEditViewTitle", value);
			}
		}

		// Token: 0x17003F7C RID: 16252
		// (get) Token: 0x0600C4E1 RID: 50401 RVA: 0x002C01D2 File Offset: 0x002BE3D2
		// (set) Token: 0x0600C4E2 RID: 50402 RVA: 0x002C01DF File Offset: 0x002BE3DF
		[DefaultValue("Export")]
		public string MobileExportViewTitle
		{
			get
			{
				return this.GetString("MobileExportViewTitle");
			}
			set
			{
				base.SetString("MobileExportViewTitle", value);
			}
		}

		// Token: 0x17003F7D RID: 16253
		// (get) Token: 0x0600C4E3 RID: 50403 RVA: 0x002C01ED File Offset: 0x002BE3ED
		// (set) Token: 0x0600C4E4 RID: 50404 RVA: 0x002C01FA File Offset: 0x002BE3FA
		[DefaultValue("Export options")]
		public string MobileExportViewDescription
		{
			get
			{
				return this.GetString("MobileExportViewDescription");
			}
			set
			{
				base.SetString("MobileExportViewDescription", value);
			}
		}

		// Token: 0x04003414 RID: 13332
		private readonly LocalizationProvider _localizationProvider;
	}
}
