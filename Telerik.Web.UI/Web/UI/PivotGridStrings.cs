using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000E12 RID: 3602
	internal class PivotGridStrings : LocalizationStrings
	{
		// Token: 0x060085F3 RID: 34291 RVA: 0x001E8342 File Offset: 0x001E6542
		public PivotGridStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x17002A66 RID: 10854
		// (get) Token: 0x060085F4 RID: 34292 RVA: 0x001E8352 File Offset: 0x001E6552
		// (set) Token: 0x060085F5 RID: 34293 RVA: 0x001E835F File Offset: 0x001E655F
		[NotifyParentProperty(true)]
		[DefaultValue("Drop Row Fields Here")]
		public string RowHeaderZoneText
		{
			get
			{
				return this.GetString("RowHeaderZoneText");
			}
			set
			{
				this.SetString("RowHeaderZoneText", value);
			}
		}

		// Token: 0x17002A67 RID: 10855
		// (get) Token: 0x060085F6 RID: 34294 RVA: 0x001E836D File Offset: 0x001E656D
		// (set) Token: 0x060085F7 RID: 34295 RVA: 0x001E837A File Offset: 0x001E657A
		[DefaultValue("Drop Filter Fields Here")]
		[NotifyParentProperty(true)]
		public string FilterHeaderZoneText
		{
			get
			{
				return this.GetString("FilterHeaderZoneText");
			}
			set
			{
				this.SetString("FilterHeaderZoneText", value);
			}
		}

		// Token: 0x17002A68 RID: 10856
		// (get) Token: 0x060085F8 RID: 34296 RVA: 0x001E8388 File Offset: 0x001E6588
		// (set) Token: 0x060085F9 RID: 34297 RVA: 0x001E8395 File Offset: 0x001E6595
		[NotifyParentProperty(true)]
		[DefaultValue("Drop Column Fields Here")]
		public string ColumnHeaderZoneText
		{
			get
			{
				return this.GetString("ColumnHeaderZoneText");
			}
			set
			{
				this.SetString("ColumnHeaderZoneText", value);
			}
		}

		// Token: 0x17002A69 RID: 10857
		// (get) Token: 0x060085FA RID: 34298 RVA: 0x001E83A3 File Offset: 0x001E65A3
		// (set) Token: 0x060085FB RID: 34299 RVA: 0x001E83B0 File Offset: 0x001E65B0
		[NotifyParentProperty(true)]
		[DefaultValue("Drop Data Fields Here")]
		public string DataHeaderZoneText
		{
			get
			{
				return this.GetString("DataHeaderZoneText");
			}
			set
			{
				this.SetString("DataHeaderZoneText", value);
			}
		}

		// Token: 0x17002A6A RID: 10858
		// (get) Token: 0x060085FC RID: 34300 RVA: 0x001E83BE File Offset: 0x001E65BE
		// (set) Token: 0x060085FD RID: 34301 RVA: 0x001E83CB File Offset: 0x001E65CB
		[DefaultValue("First Page")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17002A6B RID: 10859
		// (get) Token: 0x060085FE RID: 34302 RVA: 0x001E83D9 File Offset: 0x001E65D9
		// (set) Token: 0x060085FF RID: 34303 RVA: 0x001E83E6 File Offset: 0x001E65E6
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

		// Token: 0x17002A6C RID: 10860
		// (get) Token: 0x06008600 RID: 34304 RVA: 0x001E83F4 File Offset: 0x001E65F4
		// (set) Token: 0x06008601 RID: 34305 RVA: 0x001E8401 File Offset: 0x001E6601
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

		// Token: 0x17002A6D RID: 10861
		// (get) Token: 0x06008602 RID: 34306 RVA: 0x001E840F File Offset: 0x001E660F
		// (set) Token: 0x06008603 RID: 34307 RVA: 0x001E841C File Offset: 0x001E661C
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

		// Token: 0x17002A6E RID: 10862
		// (get) Token: 0x06008604 RID: 34308 RVA: 0x001E842A File Offset: 0x001E662A
		// (set) Token: 0x06008605 RID: 34309 RVA: 0x001E8437 File Offset: 0x001E6637
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

		// Token: 0x17002A6F RID: 10863
		// (get) Token: 0x06008606 RID: 34310 RVA: 0x001E8445 File Offset: 0x001E6645
		// (set) Token: 0x06008607 RID: 34311 RVA: 0x001E8452 File Offset: 0x001E6652
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

		// Token: 0x17002A70 RID: 10864
		// (get) Token: 0x06008608 RID: 34312 RVA: 0x001E8460 File Offset: 0x001E6660
		// (set) Token: 0x06008609 RID: 34313 RVA: 0x001E846D File Offset: 0x001E666D
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

		// Token: 0x17002A71 RID: 10865
		// (get) Token: 0x0600860A RID: 34314 RVA: 0x001E847B File Offset: 0x001E667B
		// (set) Token: 0x0600860B RID: 34315 RVA: 0x001E8488 File Offset: 0x001E6688
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

		// Token: 0x17002A72 RID: 10866
		// (get) Token: 0x0600860C RID: 34316 RVA: 0x001E8496 File Offset: 0x001E6696
		// (set) Token: 0x0600860D RID: 34317 RVA: 0x001E84A3 File Offset: 0x001E66A3
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

		// Token: 0x17002A73 RID: 10867
		// (get) Token: 0x0600860E RID: 34318 RVA: 0x001E84B1 File Offset: 0x001E66B1
		// (set) Token: 0x0600860F RID: 34319 RVA: 0x001E84BE File Offset: 0x001E66BE
		[DefaultValue("Previous Page")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17002A74 RID: 10868
		// (get) Token: 0x06008610 RID: 34320 RVA: 0x001E84CC File Offset: 0x001E66CC
		// (set) Token: 0x06008611 RID: 34321 RVA: 0x001E84D9 File Offset: 0x001E66D9
		[NotifyParentProperty(true)]
		[DefaultValue("Increase")]
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

		// Token: 0x17002A75 RID: 10869
		// (get) Token: 0x06008612 RID: 34322 RVA: 0x001E84E7 File Offset: 0x001E66E7
		// (set) Token: 0x06008613 RID: 34323 RVA: 0x001E84F4 File Offset: 0x001E66F4
		[DefaultValue("Decrease")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17002A76 RID: 10870
		// (get) Token: 0x06008614 RID: 34324 RVA: 0x001E8502 File Offset: 0x001E6702
		// (set) Token: 0x06008615 RID: 34325 RVA: 0x001E850F File Offset: 0x001E670F
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

		// Token: 0x17002A77 RID: 10871
		// (get) Token: 0x06008616 RID: 34326 RVA: 0x001E851D File Offset: 0x001E671D
		// (set) Token: 0x06008617 RID: 34327 RVA: 0x001E852A File Offset: 0x001E672A
		[NotifyParentProperty(true)]
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
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

		// Token: 0x17002A78 RID: 10872
		// (get) Token: 0x06008618 RID: 34328 RVA: 0x001E8538 File Offset: 0x001E6738
		// (set) Token: 0x06008619 RID: 34329 RVA: 0x001E8545 File Offset: 0x001E6745
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

		// Token: 0x17002A79 RID: 10873
		// (get) Token: 0x0600861A RID: 34330 RVA: 0x001E8553 File Offset: 0x001E6753
		// (set) Token: 0x0600861B RID: 34331 RVA: 0x001E8560 File Offset: 0x001E6760
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

		// Token: 0x17002A7A RID: 10874
		// (get) Token: 0x0600861C RID: 34332 RVA: 0x001E856E File Offset: 0x001E676E
		// (set) Token: 0x0600861D RID: 34333 RVA: 0x001E857B File Offset: 0x001E677B
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

		// Token: 0x17002A7B RID: 10875
		// (get) Token: 0x0600861E RID: 34334 RVA: 0x001E8589 File Offset: 0x001E6789
		// (set) Token: 0x0600861F RID: 34335 RVA: 0x001E8596 File Offset: 0x001E6796
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

		// Token: 0x17002A7C RID: 10876
		// (get) Token: 0x06008620 RID: 34336 RVA: 0x001E85A4 File Offset: 0x001E67A4
		// (set) Token: 0x06008621 RID: 34337 RVA: 0x001E85B1 File Offset: 0x001E67B1
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

		// Token: 0x17002A7D RID: 10877
		// (get) Token: 0x06008622 RID: 34338 RVA: 0x001E85BF File Offset: 0x001E67BF
		// (set) Token: 0x06008623 RID: 34339 RVA: 0x001E85CC File Offset: 0x001E67CC
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

		// Token: 0x17002A7E RID: 10878
		// (get) Token: 0x06008624 RID: 34340 RVA: 0x001E85DA File Offset: 0x001E67DA
		// (set) Token: 0x06008625 RID: 34341 RVA: 0x001E85E7 File Offset: 0x001E67E7
		[NotifyParentProperty(true)]
		[DefaultValue("Refresh")]
		public string ZoneContextMenuRefresh
		{
			get
			{
				return this.GetString("ZoneContextMenuRefresh");
			}
			set
			{
				this.SetString("ZoneContextMenuRefresh", value);
			}
		}

		// Token: 0x17002A7F RID: 10879
		// (get) Token: 0x06008626 RID: 34342 RVA: 0x001E85F5 File Offset: 0x001E67F5
		// (set) Token: 0x06008627 RID: 34343 RVA: 0x001E8602 File Offset: 0x001E6802
		[NotifyParentProperty(true)]
		[DefaultValue("Hide")]
		public string ZoneContextMenuHide
		{
			get
			{
				return this.GetString("ZoneContextMenuHide");
			}
			set
			{
				this.SetString("ZoneContextMenuHide", value);
			}
		}

		// Token: 0x17002A80 RID: 10880
		// (get) Token: 0x06008628 RID: 34344 RVA: 0x001E8610 File Offset: 0x001E6810
		// (set) Token: 0x06008629 RID: 34345 RVA: 0x001E861D File Offset: 0x001E681D
		[NotifyParentProperty(true)]
		[DefaultValue("Summarize By Settings")]
		public string ZoneContextMenuSummarizeBySettings
		{
			get
			{
				return this.GetString("ZoneContextMenuSummarizeBySettings");
			}
			set
			{
				this.SetString("ZoneContextMenuSummarizeBySettings", value);
			}
		}

		// Token: 0x17002A81 RID: 10881
		// (get) Token: 0x0600862A RID: 34346 RVA: 0x001E862B File Offset: 0x001E682B
		// (set) Token: 0x0600862B RID: 34347 RVA: 0x001E8638 File Offset: 0x001E6838
		[NotifyParentProperty(true)]
		[DefaultValue("Show Fields Window")]
		public string ZoneContextMenuShowFieldsWindow
		{
			get
			{
				return this.GetString("ZoneContextMenuShowFieldsWindow");
			}
			set
			{
				this.SetString("ZoneContextMenuShowFieldsWindow", value);
			}
		}

		// Token: 0x17002A82 RID: 10882
		// (get) Token: 0x0600862C RID: 34348 RVA: 0x001E8646 File Offset: 0x001E6846
		// (set) Token: 0x0600862D RID: 34349 RVA: 0x001E8653 File Offset: 0x001E6853
		[DefaultValue("Hide Fields Window")]
		[NotifyParentProperty(true)]
		public string ZoneContextMenuHideFieldsWindow
		{
			get
			{
				return this.GetString("ZoneContextMenuHideFieldsWindow");
			}
			set
			{
				this.SetString("ZoneContextMenuHideFieldsWindow", value);
			}
		}

		// Token: 0x17002A83 RID: 10883
		// (get) Token: 0x0600862E RID: 34350 RVA: 0x001E8661 File Offset: 0x001E6861
		// (set) Token: 0x0600862F RID: 34351 RVA: 0x001E866E File Offset: 0x001E686E
		[NotifyParentProperty(true)]
		[DefaultValue("RadPivotGrid Fields Window")]
		public string FieldsWindowTitle
		{
			get
			{
				return this.GetString("FieldsWindowTitle");
			}
			set
			{
				this.SetString("FieldsWindowTitle", value);
			}
		}

		// Token: 0x17002A84 RID: 10884
		// (get) Token: 0x06008630 RID: 34352 RVA: 0x001E867C File Offset: 0x001E687C
		// (set) Token: 0x06008631 RID: 34353 RVA: 0x001E8689 File Offset: 0x001E6889
		[DefaultValue("Clear Filters")]
		[NotifyParentProperty(true)]
		public string ClearFiltersText
		{
			get
			{
				return this.GetString("ClearFiltersText");
			}
			set
			{
				base.SetString("ClearFiltersText", value);
			}
		}

		// Token: 0x17002A85 RID: 10885
		// (get) Token: 0x06008632 RID: 34354 RVA: 0x001E8697 File Offset: 0x001E6897
		// (set) Token: 0x06008633 RID: 34355 RVA: 0x001E86A4 File Offset: 0x001E68A4
		[DefaultValue("Contains...")]
		[NotifyParentProperty(true)]
		public string ContainsText
		{
			get
			{
				return this.GetString("ContainsText");
			}
			set
			{
				base.SetString("ContainsText", value);
			}
		}

		// Token: 0x17002A86 RID: 10886
		// (get) Token: 0x06008634 RID: 34356 RVA: 0x001E86B2 File Offset: 0x001E68B2
		// (set) Token: 0x06008635 RID: 34357 RVA: 0x001E86BF File Offset: 0x001E68BF
		[DefaultValue("Does Not Contain...")]
		[NotifyParentProperty(true)]
		public string DoesNotContainText
		{
			get
			{
				return this.GetString("DoesNotContainText");
			}
			set
			{
				base.SetString("DoesNotContainText", value);
			}
		}

		// Token: 0x17002A87 RID: 10887
		// (get) Token: 0x06008636 RID: 34358 RVA: 0x001E86CD File Offset: 0x001E68CD
		// (set) Token: 0x06008637 RID: 34359 RVA: 0x001E86DA File Offset: 0x001E68DA
		[NotifyParentProperty(true)]
		[DefaultValue("Begins With...")]
		public string BeginsWithText
		{
			get
			{
				return this.GetString("BeginsWithText");
			}
			set
			{
				base.SetString("BeginsWithText", value);
			}
		}

		// Token: 0x17002A88 RID: 10888
		// (get) Token: 0x06008638 RID: 34360 RVA: 0x001E86E8 File Offset: 0x001E68E8
		// (set) Token: 0x06008639 RID: 34361 RVA: 0x001E86F5 File Offset: 0x001E68F5
		[DefaultValue("Does Not Begin With...")]
		[NotifyParentProperty(true)]
		public string DoesNotBeginWithText
		{
			get
			{
				return this.GetString("DoesNotBeginWithText");
			}
			set
			{
				base.SetString("DoesNotBeginWithText", value);
			}
		}

		// Token: 0x17002A89 RID: 10889
		// (get) Token: 0x0600863A RID: 34362 RVA: 0x001E8703 File Offset: 0x001E6903
		// (set) Token: 0x0600863B RID: 34363 RVA: 0x001E8710 File Offset: 0x001E6910
		[NotifyParentProperty(true)]
		[DefaultValue("Ends With...")]
		public string EndsWithText
		{
			get
			{
				return this.GetString("EndsWithText");
			}
			set
			{
				base.SetString("EndsWithText", value);
			}
		}

		// Token: 0x17002A8A RID: 10890
		// (get) Token: 0x0600863C RID: 34364 RVA: 0x001E871E File Offset: 0x001E691E
		// (set) Token: 0x0600863D RID: 34365 RVA: 0x001E872B File Offset: 0x001E692B
		[DefaultValue("Does Not End With...")]
		[NotifyParentProperty(true)]
		public string DoesNotEndWithText
		{
			get
			{
				return this.GetString("DoesNotEndWithText");
			}
			set
			{
				base.SetString("DoesNotEndWithText", value);
			}
		}

		// Token: 0x17002A8B RID: 10891
		// (get) Token: 0x0600863E RID: 34366 RVA: 0x001E8739 File Offset: 0x001E6939
		// (set) Token: 0x0600863F RID: 34367 RVA: 0x001E8746 File Offset: 0x001E6946
		[DefaultValue("Equals...")]
		[NotifyParentProperty(true)]
		public string EqualsText
		{
			get
			{
				return this.GetString("EqualsText");
			}
			set
			{
				base.SetString("EqualsText", value);
			}
		}

		// Token: 0x17002A8C RID: 10892
		// (get) Token: 0x06008640 RID: 34368 RVA: 0x001E8754 File Offset: 0x001E6954
		// (set) Token: 0x06008641 RID: 34369 RVA: 0x001E8761 File Offset: 0x001E6961
		[NotifyParentProperty(true)]
		[DefaultValue("Does Not Equal...")]
		public string DoesNotEqualText
		{
			get
			{
				return this.GetString("DoesNotEqualText");
			}
			set
			{
				base.SetString("DoesNotEqualText", value);
			}
		}

		// Token: 0x17002A8D RID: 10893
		// (get) Token: 0x06008642 RID: 34370 RVA: 0x001E876F File Offset: 0x001E696F
		// (set) Token: 0x06008643 RID: 34371 RVA: 0x001E877C File Offset: 0x001E697C
		[NotifyParentProperty(true)]
		[DefaultValue("Greater Than...")]
		public string IsGreaterThanText
		{
			get
			{
				return this.GetString("IsGreaterThanText");
			}
			set
			{
				base.SetString("IsGreaterThanText", value);
			}
		}

		// Token: 0x17002A8E RID: 10894
		// (get) Token: 0x06008644 RID: 34372 RVA: 0x001E878A File Offset: 0x001E698A
		// (set) Token: 0x06008645 RID: 34373 RVA: 0x001E8797 File Offset: 0x001E6997
		[DefaultValue("Less Than...")]
		[NotifyParentProperty(true)]
		public string IsLessThanText
		{
			get
			{
				return this.GetString("IsLessThanText");
			}
			set
			{
				base.SetString("IsLessThanText", value);
			}
		}

		// Token: 0x17002A8F RID: 10895
		// (get) Token: 0x06008646 RID: 34374 RVA: 0x001E87A5 File Offset: 0x001E69A5
		// (set) Token: 0x06008647 RID: 34375 RVA: 0x001E87B2 File Offset: 0x001E69B2
		[NotifyParentProperty(true)]
		[DefaultValue("Greater Than Or Equal To...")]
		public string IsGreaterThanOrEqualToText
		{
			get
			{
				return this.GetString("IsGreaterThanOrEqualToText");
			}
			set
			{
				base.SetString("IsGreaterThanOrEqualToText", value);
			}
		}

		// Token: 0x17002A90 RID: 10896
		// (get) Token: 0x06008648 RID: 34376 RVA: 0x001E87C0 File Offset: 0x001E69C0
		// (set) Token: 0x06008649 RID: 34377 RVA: 0x001E87CD File Offset: 0x001E69CD
		[NotifyParentProperty(true)]
		[DefaultValue("Less Than Or Equal To...")]
		public string IsLessThanOrEqualToText
		{
			get
			{
				return this.GetString("IsLessThanOrEqualToText");
			}
			set
			{
				base.SetString("IsLessThanOrEqualToText", value);
			}
		}

		// Token: 0x17002A91 RID: 10897
		// (get) Token: 0x0600864A RID: 34378 RVA: 0x001E87DB File Offset: 0x001E69DB
		// (set) Token: 0x0600864B RID: 34379 RVA: 0x001E87E8 File Offset: 0x001E69E8
		[DefaultValue("Between...")]
		[NotifyParentProperty(true)]
		public string IsBetweenText
		{
			get
			{
				return this.GetString("IsBetweenText");
			}
			set
			{
				base.SetString("IsBetweenText", value);
			}
		}

		// Token: 0x17002A92 RID: 10898
		// (get) Token: 0x0600864C RID: 34380 RVA: 0x001E87F6 File Offset: 0x001E69F6
		// (set) Token: 0x0600864D RID: 34381 RVA: 0x001E8803 File Offset: 0x001E6A03
		[DefaultValue("Not Between...")]
		[NotifyParentProperty(true)]
		public string IsNotBetweenText
		{
			get
			{
				return this.GetString("IsNotBetweenText");
			}
			set
			{
				base.SetString("IsNotBetweenText", value);
			}
		}

		// Token: 0x17002A93 RID: 10899
		// (get) Token: 0x0600864E RID: 34382 RVA: 0x001E8811 File Offset: 0x001E6A11
		// (set) Token: 0x0600864F RID: 34383 RVA: 0x001E881E File Offset: 0x001E6A1E
		[NotifyParentProperty(true)]
		[DefaultValue("Top...")]
		public string TopText
		{
			get
			{
				return this.GetString("TopText");
			}
			set
			{
				base.SetString("TopText", value);
			}
		}

		// Token: 0x17002A94 RID: 10900
		// (get) Token: 0x06008650 RID: 34384 RVA: 0x001E882C File Offset: 0x001E6A2C
		// (set) Token: 0x06008651 RID: 34385 RVA: 0x001E8839 File Offset: 0x001E6A39
		[DefaultValue("Clear Filter From")]
		[NotifyParentProperty(true)]
		public string ClearFilterFromText
		{
			get
			{
				return this.GetString("ClearFilterFromText");
			}
			set
			{
				base.SetString("ClearFilterFromText", value);
			}
		}

		// Token: 0x17002A95 RID: 10901
		// (get) Token: 0x06008652 RID: 34386 RVA: 0x001E8847 File Offset: 0x001E6A47
		// (set) Token: 0x06008653 RID: 34387 RVA: 0x001E8854 File Offset: 0x001E6A54
		[DefaultValue("Label Filters")]
		[NotifyParentProperty(true)]
		public string LabelFiltersText
		{
			get
			{
				return this.GetString("LabelFiltersText");
			}
			set
			{
				base.SetString("LabelFiltersText", value);
			}
		}

		// Token: 0x17002A96 RID: 10902
		// (get) Token: 0x06008654 RID: 34388 RVA: 0x001E8862 File Offset: 0x001E6A62
		// (set) Token: 0x06008655 RID: 34389 RVA: 0x001E886F File Offset: 0x001E6A6F
		[DefaultValue("Value Filters")]
		[NotifyParentProperty(true)]
		public string ValueFiltersText
		{
			get
			{
				return this.GetString("ValueFiltersText");
			}
			set
			{
				base.SetString("ValueFiltersText", value);
			}
		}

		// Token: 0x17002A97 RID: 10903
		// (get) Token: 0x06008656 RID: 34390 RVA: 0x001E887D File Offset: 0x001E6A7D
		// (set) Token: 0x06008657 RID: 34391 RVA: 0x001E888A File Offset: 0x001E6A8A
		[DefaultValue("OK")]
		[NotifyParentProperty(true)]
		public string FilterDialogOKButtonText
		{
			get
			{
				return this.GetString("FilterDialogOKButtonText");
			}
			set
			{
				base.SetString("FilterDialogOKButtonText", value);
			}
		}

		// Token: 0x17002A98 RID: 10904
		// (get) Token: 0x06008658 RID: 34392 RVA: 0x001E8898 File Offset: 0x001E6A98
		// (set) Token: 0x06008659 RID: 34393 RVA: 0x001E88A5 File Offset: 0x001E6AA5
		[DefaultValue("Cancel")]
		[NotifyParentProperty(true)]
		public string FilterDialogCancelButtonText
		{
			get
			{
				return this.GetString("FilterDialogCancelButtonText");
			}
			set
			{
				base.SetString("FilterDialogCancelButtonText", value);
			}
		}

		// Token: 0x17002A99 RID: 10905
		// (get) Token: 0x0600865A RID: 34394 RVA: 0x001E88B3 File Offset: 0x001E6AB3
		// (set) Token: 0x0600865B RID: 34395 RVA: 0x001E88C0 File Offset: 0x001E6AC0
		[NotifyParentProperty(true)]
		[DefaultValue("and")]
		public string FilterDialogAndLabelText
		{
			get
			{
				return this.GetString("FilterDialogAndLabelText");
			}
			set
			{
				base.SetString("FilterDialogAndLabelText", value);
			}
		}

		// Token: 0x17002A9A RID: 10906
		// (get) Token: 0x0600865C RID: 34396 RVA: 0x001E88CE File Offset: 0x001E6ACE
		// (set) Token: 0x0600865D RID: 34397 RVA: 0x001E88DB File Offset: 0x001E6ADB
		[DefaultValue("by")]
		[NotifyParentProperty(true)]
		public string FilterDialogByLabelText
		{
			get
			{
				return this.GetString("FilterDialogByLabelText");
			}
			set
			{
				base.SetString("FilterDialogByLabelText", value);
			}
		}

		// Token: 0x17002A9B RID: 10907
		// (get) Token: 0x0600865E RID: 34398 RVA: 0x001E88E9 File Offset: 0x001E6AE9
		// (set) Token: 0x0600865F RID: 34399 RVA: 0x001E88F6 File Offset: 0x001E6AF6
		[NotifyParentProperty(true)]
		[DefaultValue("Bottom")]
		public string BottomText
		{
			get
			{
				return this.GetString("BottomText");
			}
			set
			{
				base.SetString("BottomText", value);
			}
		}

		// Token: 0x17002A9C RID: 10908
		// (get) Token: 0x06008660 RID: 34400 RVA: 0x001E8904 File Offset: 0x001E6B04
		// (set) Token: 0x06008661 RID: 34401 RVA: 0x001E8911 File Offset: 0x001E6B11
		[DefaultValue("Includes")]
		[NotifyParentProperty(true)]
		public string IncludesText
		{
			get
			{
				return this.GetString("IncludesText");
			}
			set
			{
				base.SetString("IncludesText", value);
			}
		}

		// Token: 0x17002A9D RID: 10909
		// (get) Token: 0x06008662 RID: 34402 RVA: 0x001E891F File Offset: 0x001E6B1F
		// (set) Token: 0x06008663 RID: 34403 RVA: 0x001E892C File Offset: 0x001E6B2C
		[DefaultValue("Excludes")]
		[NotifyParentProperty(true)]
		public string ExcludesText
		{
			get
			{
				return this.GetString("ExcludesText");
			}
			set
			{
				base.SetString("ExcludesText", value);
			}
		}

		// Token: 0x17002A9E RID: 10910
		// (get) Token: 0x06008664 RID: 34404 RVA: 0x001E893A File Offset: 0x001E6B3A
		// (set) Token: 0x06008665 RID: 34405 RVA: 0x001E8947 File Offset: 0x001E6B47
		[DefaultValue("Does Not Include")]
		[NotifyParentProperty(true)]
		public string DoesNotIncludeText
		{
			get
			{
				return this.GetString("DoesNotIncludeText");
			}
			set
			{
				base.SetString("DoesNotIncludeText", value);
			}
		}

		// Token: 0x17002A9F RID: 10911
		// (get) Token: 0x06008666 RID: 34406 RVA: 0x001E8955 File Offset: 0x001E6B55
		// (set) Token: 0x06008667 RID: 34407 RVA: 0x001E8962 File Offset: 0x001E6B62
		[DefaultValue("Items")]
		[NotifyParentProperty(true)]
		public string ItemsText
		{
			get
			{
				return this.GetString("ItemsText");
			}
			set
			{
				base.SetString("ItemsText", value);
			}
		}

		// Token: 0x17002AA0 RID: 10912
		// (get) Token: 0x06008668 RID: 34408 RVA: 0x001E8970 File Offset: 0x001E6B70
		// (set) Token: 0x06008669 RID: 34409 RVA: 0x001E897D File Offset: 0x001E6B7D
		[NotifyParentProperty(true)]
		[DefaultValue("Percent")]
		public string PercentText
		{
			get
			{
				return this.GetString("PercentText");
			}
			set
			{
				base.SetString("PercentText", value);
			}
		}

		// Token: 0x17002AA1 RID: 10913
		// (get) Token: 0x0600866A RID: 34410 RVA: 0x001E898B File Offset: 0x001E6B8B
		// (set) Token: 0x0600866B RID: 34411 RVA: 0x001E8998 File Offset: 0x001E6B98
		[DefaultValue("Sum")]
		[NotifyParentProperty(true)]
		public string SumText
		{
			get
			{
				return this.GetString("SumText");
			}
			set
			{
				base.SetString("SumText", value);
			}
		}

		// Token: 0x17002AA2 RID: 10914
		// (get) Token: 0x0600866C RID: 34412 RVA: 0x001E89A6 File Offset: 0x001E6BA6
		// (set) Token: 0x0600866D RID: 34413 RVA: 0x001E89B3 File Offset: 0x001E6BB3
		[DefaultValue("(Select All)")]
		[NotifyParentProperty(true)]
		public string SelectAllText
		{
			get
			{
				return this.GetString("SelectAllText");
			}
			set
			{
				base.SetString("SelectAllText", value);
			}
		}

		// Token: 0x17002AA3 RID: 10915
		// (get) Token: 0x0600866E RID: 34414 RVA: 0x001E89C1 File Offset: 0x001E6BC1
		// (set) Token: 0x0600866F RID: 34415 RVA: 0x001E89CE File Offset: 0x001E6BCE
		[DefaultValue("Value filter")]
		[NotifyParentProperty(true)]
		public string ValueFilterText
		{
			get
			{
				return this.GetString("ValueFilter");
			}
			set
			{
				base.SetString("ValueFilter", value);
			}
		}

		// Token: 0x17002AA4 RID: 10916
		// (get) Token: 0x06008670 RID: 34416 RVA: 0x001E89DC File Offset: 0x001E6BDC
		// (set) Token: 0x06008671 RID: 34417 RVA: 0x001E89E9 File Offset: 0x001E6BE9
		[NotifyParentProperty(true)]
		[DefaultValue("Label filter")]
		public string LabelFilterText
		{
			get
			{
				return this.GetString("LabelFilter");
			}
			set
			{
				base.SetString("LabelFilter", value);
			}
		}

		// Token: 0x17002AA5 RID: 10917
		// (get) Token: 0x06008672 RID: 34418 RVA: 0x001E89F7 File Offset: 0x001E6BF7
		// (set) Token: 0x06008673 RID: 34419 RVA: 0x001E8A04 File Offset: 0x001E6C04
		[DefaultValue("Filter Window")]
		[NotifyParentProperty(true)]
		public string FilterWindowText
		{
			get
			{
				return this.GetString("FilterWindow");
			}
			set
			{
				base.SetString("FilterWindow", value);
			}
		}

		// Token: 0x17002AA6 RID: 10918
		// (get) Token: 0x06008674 RID: 34420 RVA: 0x001E8A12 File Offset: 0x001E6C12
		// (set) Token: 0x06008675 RID: 34421 RVA: 0x001E8A1F File Offset: 0x001E6C1F
		[NotifyParentProperty(true)]
		[DefaultValue("Change Layout")]
		public string ConfigurationPanelChangeLayoutButtonText
		{
			get
			{
				return this.GetString("ConfigurationPanelChangeLayoutButtonText");
			}
			set
			{
				this.SetString("ConfigurationPanelChangeLayoutButtonText", value);
			}
		}

		// Token: 0x17002AA7 RID: 10919
		// (get) Token: 0x06008676 RID: 34422 RVA: 0x001E8A2D File Offset: 0x001E6C2D
		// (set) Token: 0x06008677 RID: 34423 RVA: 0x001E8A3A File Offset: 0x001E6C3A
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ShowHideCheckBoxToolTip
		{
			get
			{
				return this.GetString("ShowHideCheckBoxToolTip");
			}
			set
			{
				this.SetString("ShowHideCheckBoxToolTip", value);
			}
		}

		// Token: 0x17002AA8 RID: 10920
		// (get) Token: 0x06008678 RID: 34424 RVA: 0x001E8A48 File Offset: 0x001E6C48
		// (set) Token: 0x06008679 RID: 34425 RVA: 0x001E8A55 File Offset: 0x001E6C55
		[NotifyParentProperty(true)]
		[DefaultValue("Defer Layout Update")]
		public string ConfigurationPanelDeferLayoutUpdateCheckBoxText
		{
			get
			{
				return this.GetString("ConfigurationPanelDeferLayoutUpdateCheckBoxText");
			}
			set
			{
				this.SetString("ConfigurationPanelDeferLayoutUpdateCheckBoxText", value);
			}
		}

		// Token: 0x17002AA9 RID: 10921
		// (get) Token: 0x0600867A RID: 34426 RVA: 0x001E8A63 File Offset: 0x001E6C63
		// (set) Token: 0x0600867B RID: 34427 RVA: 0x001E8A70 File Offset: 0x001E6C70
		[DefaultValue("Update")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelUpdateButtonText
		{
			get
			{
				return this.GetString("ConfigurationPanelUpdateButtonText");
			}
			set
			{
				this.SetString("ConfigurationPanelUpdateButtonText", value);
			}
		}

		// Token: 0x17002AAA RID: 10922
		// (get) Token: 0x0600867C RID: 34428 RVA: 0x001E8A7E File Offset: 0x001E6C7E
		// (set) Token: 0x0600867D RID: 34429 RVA: 0x001E8A8B File Offset: 0x001E6C8B
		[NotifyParentProperty(true)]
		[DefaultValue("Stacked")]
		public string ConfigurationPanelContextMenuStacked
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuStacked");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuStacked", value);
			}
		}

		// Token: 0x17002AAB RID: 10923
		// (get) Token: 0x0600867E RID: 34430 RVA: 0x001E8A99 File Offset: 0x001E6C99
		// (set) Token: 0x0600867F RID: 34431 RVA: 0x001E8AA6 File Offset: 0x001E6CA6
		[DefaultValue("Side-By-Side")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuSideBySide
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuSideBySide");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuSideBySide", value);
			}
		}

		// Token: 0x17002AAC RID: 10924
		// (get) Token: 0x06008680 RID: 34432 RVA: 0x001E8AB4 File Offset: 0x001E6CB4
		// (set) Token: 0x06008681 RID: 34433 RVA: 0x001E8AC1 File Offset: 0x001E6CC1
		[DefaultValue("Two-By-Two")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuTwoByTwo
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuTwoByTwo");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuTwoByTwo", value);
			}
		}

		// Token: 0x17002AAD RID: 10925
		// (get) Token: 0x06008682 RID: 34434 RVA: 0x001E8ACF File Offset: 0x001E6CCF
		// (set) Token: 0x06008683 RID: 34435 RVA: 0x001E8ADC File Offset: 0x001E6CDC
		[DefaultValue("One-By-Four")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuOneByFour
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuOneByFour");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuOneByFour", value);
			}
		}

		// Token: 0x17002AAE RID: 10926
		// (get) Token: 0x06008684 RID: 34436 RVA: 0x001E8AEA File Offset: 0x001E6CEA
		// (set) Token: 0x06008685 RID: 34437 RVA: 0x001E8AF7 File Offset: 0x001E6CF7
		[DefaultValue("Move Up")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuMoveUp
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveUp");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveUp", value);
			}
		}

		// Token: 0x17002AAF RID: 10927
		// (get) Token: 0x06008686 RID: 34438 RVA: 0x001E8B05 File Offset: 0x001E6D05
		// (set) Token: 0x06008687 RID: 34439 RVA: 0x001E8B12 File Offset: 0x001E6D12
		[DefaultValue("Move Down")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuMoveDown
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveDown");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveDown", value);
			}
		}

		// Token: 0x17002AB0 RID: 10928
		// (get) Token: 0x06008688 RID: 34440 RVA: 0x001E8B20 File Offset: 0x001E6D20
		// (set) Token: 0x06008689 RID: 34441 RVA: 0x001E8B2D File Offset: 0x001E6D2D
		[DefaultValue("Move to Beginning")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuMoveToBeginning
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveToBeginning");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveToBeginning", value);
			}
		}

		// Token: 0x17002AB1 RID: 10929
		// (get) Token: 0x0600868A RID: 34442 RVA: 0x001E8B3B File Offset: 0x001E6D3B
		// (set) Token: 0x0600868B RID: 34443 RVA: 0x001E8B48 File Offset: 0x001E6D48
		[NotifyParentProperty(true)]
		[DefaultValue("Move to End")]
		public string ConfigurationPanelContextMenuMoveToEnd
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveToEnd");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveToEnd", value);
			}
		}

		// Token: 0x17002AB2 RID: 10930
		// (get) Token: 0x0600868C RID: 34444 RVA: 0x001E8B56 File Offset: 0x001E6D56
		// (set) Token: 0x0600868D RID: 34445 RVA: 0x001E8B63 File Offset: 0x001E6D63
		[DefaultValue("Move to Filter Fields")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuMoveToFilterFields
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveToFilterFields");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveToFilterFields", value);
			}
		}

		// Token: 0x17002AB3 RID: 10931
		// (get) Token: 0x0600868E RID: 34446 RVA: 0x001E8B71 File Offset: 0x001E6D71
		// (set) Token: 0x0600868F RID: 34447 RVA: 0x001E8B7E File Offset: 0x001E6D7E
		[NotifyParentProperty(true)]
		[DefaultValue("Move to Row Fields")]
		public string ConfigurationPanelContextMenuMoveToRowFields
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveToRowFields");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveToRowFields", value);
			}
		}

		// Token: 0x17002AB4 RID: 10932
		// (get) Token: 0x06008690 RID: 34448 RVA: 0x001E8B8C File Offset: 0x001E6D8C
		// (set) Token: 0x06008691 RID: 34449 RVA: 0x001E8B99 File Offset: 0x001E6D99
		[DefaultValue("Move to Column Fields")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuMoveToColumnFields
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveToColumnFields");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveToColumnFields", value);
			}
		}

		// Token: 0x17002AB5 RID: 10933
		// (get) Token: 0x06008692 RID: 34450 RVA: 0x001E8BA7 File Offset: 0x001E6DA7
		// (set) Token: 0x06008693 RID: 34451 RVA: 0x001E8BB4 File Offset: 0x001E6DB4
		[DefaultValue("Move to Aggregate Fields")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuMoveToAggregateFields
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuMoveToAggregateFields");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuMoveToAggregateFields", value);
			}
		}

		// Token: 0x17002AB6 RID: 10934
		// (get) Token: 0x06008694 RID: 34452 RVA: 0x001E8BC2 File Offset: 0x001E6DC2
		// (set) Token: 0x06008695 RID: 34453 RVA: 0x001E8BCF File Offset: 0x001E6DCF
		[NotifyParentProperty(true)]
		[DefaultValue("Hide Field")]
		public string ConfigurationPanelContextMenuHideField
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuHideField");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuHideField", value);
			}
		}

		// Token: 0x17002AB7 RID: 10935
		// (get) Token: 0x06008696 RID: 34454 RVA: 0x001E8BDD File Offset: 0x001E6DDD
		// (set) Token: 0x06008697 RID: 34455 RVA: 0x001E8BEA File Offset: 0x001E6DEA
		[DefaultValue("Summarize By Settings")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelContextMenuSummarizeBySettings
		{
			get
			{
				return this.GetString("ConfigurationPanelContextMenuSummarizeBySettings");
			}
			set
			{
				this.SetString("ConfigurationPanelContextMenuSummarizeBySettings", value);
			}
		}

		// Token: 0x17002AB8 RID: 10936
		// (get) Token: 0x06008698 RID: 34456 RVA: 0x001E8BF8 File Offset: 0x001E6DF8
		// (set) Token: 0x06008699 RID: 34457 RVA: 0x001E8C05 File Offset: 0x001E6E05
		[NotifyParentProperty(true)]
		[DefaultValue("All Fields")]
		public string ConfigurationPanelAllFieldsText
		{
			get
			{
				return this.GetString("ConfigurationPanelAllFieldsText");
			}
			set
			{
				this.SetString("ConfigurationPanelAllFieldsText", value);
			}
		}

		// Token: 0x17002AB9 RID: 10937
		// (get) Token: 0x0600869A RID: 34458 RVA: 0x001E8C13 File Offset: 0x001E6E13
		// (set) Token: 0x0600869B RID: 34459 RVA: 0x001E8C20 File Offset: 0x001E6E20
		[DefaultValue("Filter Fields")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelFilterFieldsText
		{
			get
			{
				return this.GetString("ConfigurationPanelFilterFieldsText");
			}
			set
			{
				this.SetString("ConfigurationPanelFilterFieldsText", value);
			}
		}

		// Token: 0x17002ABA RID: 10938
		// (get) Token: 0x0600869C RID: 34460 RVA: 0x001E8C2E File Offset: 0x001E6E2E
		// (set) Token: 0x0600869D RID: 34461 RVA: 0x001E8C3B File Offset: 0x001E6E3B
		[DefaultValue("Row Fields")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelRowFieldsText
		{
			get
			{
				return this.GetString("ConfigurationPanelRowFieldsText");
			}
			set
			{
				this.SetString("ConfigurationPanelRowFieldsText", value);
			}
		}

		// Token: 0x17002ABB RID: 10939
		// (get) Token: 0x0600869E RID: 34462 RVA: 0x001E8C49 File Offset: 0x001E6E49
		// (set) Token: 0x0600869F RID: 34463 RVA: 0x001E8C56 File Offset: 0x001E6E56
		[DefaultValue("Column Fields")]
		[NotifyParentProperty(true)]
		public string ConfigurationPanelColumnFieldsText
		{
			get
			{
				return this.GetString("ConfigurationPanelColumnFieldsText");
			}
			set
			{
				this.SetString("ConfigurationPanelColumnFieldsText", value);
			}
		}

		// Token: 0x17002ABC RID: 10940
		// (get) Token: 0x060086A0 RID: 34464 RVA: 0x001E8C64 File Offset: 0x001E6E64
		// (set) Token: 0x060086A1 RID: 34465 RVA: 0x001E8C71 File Offset: 0x001E6E71
		[NotifyParentProperty(true)]
		[DefaultValue("Aggregate Fields")]
		public string ConfigurationPanelAggregateFieldsText
		{
			get
			{
				return this.GetString("ConfigurationPanelAggregateFieldsText");
			}
			set
			{
				this.SetString("ConfigurationPanelAggregateFieldsText", value);
			}
		}

		// Token: 0x17002ABD RID: 10941
		// (get) Token: 0x060086A2 RID: 34466 RVA: 0x001E8C7F File Offset: 0x001E6E7F
		// (set) Token: 0x060086A3 RID: 34467 RVA: 0x001E8C8C File Offset: 0x001E6E8C
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string OlapUncategorizedFolderName
		{
			get
			{
				return this.GetString("OlapUncategorizedFolderName");
			}
			set
			{
				this.SetString("OlapUncategorizedFolderName", value);
			}
		}

		// Token: 0x17002ABE RID: 10942
		// (get) Token: 0x060086A4 RID: 34468 RVA: 0x001E8C9A File Offset: 0x001E6E9A
		// (set) Token: 0x060086A5 RID: 34469 RVA: 0x001E8CA7 File Offset: 0x001E6EA7
		[NotifyParentProperty(true)]
		[DefaultValue("OK")]
		public string FieldSettingsWindowOKButton
		{
			get
			{
				return this.GetString("FieldSettingsWindowOKButton");
			}
			set
			{
				this.SetString("FieldSettingsWindowOKButton", value);
			}
		}

		// Token: 0x17002ABF RID: 10943
		// (get) Token: 0x060086A6 RID: 34470 RVA: 0x001E8CB5 File Offset: 0x001E6EB5
		// (set) Token: 0x060086A7 RID: 34471 RVA: 0x001E8CC2 File Offset: 0x001E6EC2
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		public string FieldSettingsWindowCancelButton
		{
			get
			{
				return this.GetString("FieldSettingsWindowCancelButton");
			}
			set
			{
				this.SetString("FieldSettingsWindowCancelButton", value);
			}
		}

		// Token: 0x17002AC0 RID: 10944
		// (get) Token: 0x060086A8 RID: 34472 RVA: 0x001E8CD0 File Offset: 0x001E6ED0
		// (set) Token: 0x060086A9 RID: 34473 RVA: 0x001E8CDD File Offset: 0x001E6EDD
		[DefaultValue("Summarize By Settings")]
		[NotifyParentProperty(true)]
		public string SummarizeBySettingsTitle
		{
			get
			{
				return this.GetString("SummarizeBySettingsTitle");
			}
			set
			{
				this.SetString("SummarizeBySettingsTitle", value);
			}
		}

		// Token: 0x17002AC1 RID: 10945
		// (get) Token: 0x060086AA RID: 34474 RVA: 0x001E8CEB File Offset: 0x001E6EEB
		// (set) Token: 0x060086AB RID: 34475 RVA: 0x001E8CF8 File Offset: 0x001E6EF8
		[DefaultValue("Column fields")]
		[NotifyParentProperty(true)]
		public string ColumnGroupedFieldsTitle
		{
			get
			{
				return this.GetString("ColumnGroupedFieldsTitle");
			}
			set
			{
				this.SetString("ColumnGroupedFieldsTitle", value);
			}
		}

		// Token: 0x17002AC2 RID: 10946
		// (get) Token: 0x060086AC RID: 34476 RVA: 0x001E8D06 File Offset: 0x001E6F06
		// (set) Token: 0x060086AD RID: 34477 RVA: 0x001E8D13 File Offset: 0x001E6F13
		[DefaultValue("Row fields")]
		[NotifyParentProperty(true)]
		public string RowGroupedFieldsTitle
		{
			get
			{
				return this.GetString("RowGroupedFieldsTitle");
			}
			set
			{
				this.SetString("RowGroupedFieldsTitle", value);
			}
		}

		// Token: 0x17002AC3 RID: 10947
		// (get) Token: 0x060086AE RID: 34478 RVA: 0x001E8D21 File Offset: 0x001E6F21
		// (set) Token: 0x060086AF RID: 34479 RVA: 0x001E8D2E File Offset: 0x001E6F2E
		[DefaultValue("Aggregate fields")]
		[NotifyParentProperty(true)]
		public string AggregateGroupedFieldsTitle
		{
			get
			{
				return this.GetString("AggregateGroupedFieldsTitle");
			}
			set
			{
				this.SetString("AggregateGroupedFieldsTitle", value);
			}
		}

		// Token: 0x17002AC4 RID: 10948
		// (get) Token: 0x060086B0 RID: 34480 RVA: 0x001E8D3C File Offset: 0x001E6F3C
		// (set) Token: 0x060086B1 RID: 34481 RVA: 0x001E8D49 File Offset: 0x001E6F49
		[DefaultValue("Filter fields")]
		[NotifyParentProperty(true)]
		public string FilterGroupedFieldsTitle
		{
			get
			{
				return this.GetString("FilterGroupedFieldsTitle");
			}
			set
			{
				this.SetString("FilterGroupedFieldsTitle", value);
			}
		}

		// Token: 0x17002AC5 RID: 10949
		// (get) Token: 0x060086B2 RID: 34482 RVA: 0x001E8D57 File Offset: 0x001E6F57
		// (set) Token: 0x060086B3 RID: 34483 RVA: 0x001E8D64 File Offset: 0x001E6F64
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string OuterTableCaption
		{
			get
			{
				return this.GetString("OuterTableCaption");
			}
			set
			{
				this.SetString("OuterTableCaption", value);
			}
		}

		// Token: 0x17002AC6 RID: 10950
		// (get) Token: 0x060086B4 RID: 34484 RVA: 0x001E8D72 File Offset: 0x001E6F72
		// (set) Token: 0x060086B5 RID: 34485 RVA: 0x001E8D7F File Offset: 0x001E6F7F
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string OuterTableSummary
		{
			get
			{
				return this.GetString("OuterTableSummary");
			}
			set
			{
				this.SetString("OuterTableSummary", value);
			}
		}

		// Token: 0x17002AC7 RID: 10951
		// (get) Token: 0x060086B6 RID: 34486 RVA: 0x001E8D8D File Offset: 0x001E6F8D
		// (set) Token: 0x060086B7 RID: 34487 RVA: 0x001E8D9A File Offset: 0x001E6F9A
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ColumnHeaderTableCaption
		{
			get
			{
				return this.GetString("ColumnHeaderTableCaption");
			}
			set
			{
				this.SetString("ColumnHeaderTableCaption", value);
			}
		}

		// Token: 0x17002AC8 RID: 10952
		// (get) Token: 0x060086B8 RID: 34488 RVA: 0x001E8DA8 File Offset: 0x001E6FA8
		// (set) Token: 0x060086B9 RID: 34489 RVA: 0x001E8DB5 File Offset: 0x001E6FB5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ColumnHeaderTableSummary
		{
			get
			{
				return this.GetString("ColumnHeaderTableSummary");
			}
			set
			{
				this.SetString("ColumnHeaderTableSummary", value);
			}
		}

		// Token: 0x17002AC9 RID: 10953
		// (get) Token: 0x060086BA RID: 34490 RVA: 0x001E8DC3 File Offset: 0x001E6FC3
		// (set) Token: 0x060086BB RID: 34491 RVA: 0x001E8DD0 File Offset: 0x001E6FD0
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string DataTableCaption
		{
			get
			{
				return this.GetString("DataTableCaption");
			}
			set
			{
				this.SetString("DataTableCaption", value);
			}
		}

		// Token: 0x17002ACA RID: 10954
		// (get) Token: 0x060086BC RID: 34492 RVA: 0x001E8DDE File Offset: 0x001E6FDE
		// (set) Token: 0x060086BD RID: 34493 RVA: 0x001E8DEB File Offset: 0x001E6FEB
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string DataTableSummary
		{
			get
			{
				return this.GetString("DataTableSummary");
			}
			set
			{
				this.SetString("DataTableSummary", value);
			}
		}

		// Token: 0x17002ACB RID: 10955
		// (get) Token: 0x060086BE RID: 34494 RVA: 0x001E8DF9 File Offset: 0x001E6FF9
		// (set) Token: 0x060086BF RID: 34495 RVA: 0x001E8E06 File Offset: 0x001E7006
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string WrapperTableSummary
		{
			get
			{
				return this.GetString("WrapperTableSummary");
			}
			set
			{
				this.SetString("WrapperTableSummary", value);
			}
		}

		// Token: 0x17002ACC RID: 10956
		// (get) Token: 0x060086C0 RID: 34496 RVA: 0x001E8E14 File Offset: 0x001E7014
		// (set) Token: 0x060086C1 RID: 34497 RVA: 0x001E8E21 File Offset: 0x001E7021
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string WrapperTableCaption
		{
			get
			{
				return this.GetString("WrapperTableCaption");
			}
			set
			{
				this.SetString("WrapperTableCaption", value);
			}
		}

		// Token: 0x17002ACD RID: 10957
		// (get) Token: 0x060086C2 RID: 34498 RVA: 0x001E8E2F File Offset: 0x001E702F
		// (set) Token: 0x060086C3 RID: 34499 RVA: 0x001E8E3C File Offset: 0x001E703C
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string RowHeaderTableCaption
		{
			get
			{
				return this.GetString("RowHeaderTableCaption");
			}
			set
			{
				this.SetString("RowHeaderTableCaption", value);
			}
		}

		// Token: 0x17002ACE RID: 10958
		// (get) Token: 0x060086C4 RID: 34500 RVA: 0x001E8E4A File Offset: 0x001E704A
		// (set) Token: 0x060086C5 RID: 34501 RVA: 0x001E8E57 File Offset: 0x001E7057
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string RowHeaderTableSummary
		{
			get
			{
				return this.GetString("RowHeaderTableSummary");
			}
			set
			{
				this.SetString("RowHeaderTableSummary", value);
			}
		}

		// Token: 0x17002ACF RID: 10959
		// (get) Token: 0x060086C6 RID: 34502 RVA: 0x001E8E65 File Offset: 0x001E7065
		// (set) Token: 0x060086C7 RID: 34503 RVA: 0x001E8E72 File Offset: 0x001E7072
		[DefaultValue("Value: ")]
		[NotifyParentProperty(true)]
		public string ToolTipsValueText
		{
			get
			{
				return this.GetString("ToolTipsValueText");
			}
			set
			{
				this.SetString("ToolTipsValueText", value);
			}
		}

		// Token: 0x17002AD0 RID: 10960
		// (get) Token: 0x060086C8 RID: 34504 RVA: 0x001E8E80 File Offset: 0x001E7080
		// (set) Token: 0x060086C9 RID: 34505 RVA: 0x001E8E8D File Offset: 0x001E708D
		[NotifyParentProperty(true)]
		[DefaultValue("Row: ")]
		public string ToolTipsRowText
		{
			get
			{
				return this.GetString("ToolTipsRowText");
			}
			set
			{
				this.SetString("ToolTipsRowText", value);
			}
		}

		// Token: 0x17002AD1 RID: 10961
		// (get) Token: 0x060086CA RID: 34506 RVA: 0x001E8E9B File Offset: 0x001E709B
		// (set) Token: 0x060086CB RID: 34507 RVA: 0x001E8EA8 File Offset: 0x001E70A8
		[DefaultValue("Column: ")]
		[NotifyParentProperty(true)]
		public string ToolTipsColumnText
		{
			get
			{
				return this.GetString("ToolTipsColumnText");
			}
			set
			{
				this.SetString("ToolTipsColumnText", value);
			}
		}

		// Token: 0x17002AD2 RID: 10962
		// (get) Token: 0x060086CC RID: 34508 RVA: 0x001E8EB6 File Offset: 0x001E70B6
		// (set) Token: 0x060086CD RID: 34509 RVA: 0x001E8EC3 File Offset: 0x001E70C3
		[NotifyParentProperty(true)]
		[DefaultValue("Drag to resize")]
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

		// Token: 0x17002AD3 RID: 10963
		// (get) Token: 0x060086CE RID: 34510 RVA: 0x001E8ED1 File Offset: 0x001E70D1
		// (set) Token: 0x060086CF RID: 34511 RVA: 0x001E8EDE File Offset: 0x001E70DE
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

		// Token: 0x17002AD4 RID: 10964
		// (get) Token: 0x060086D0 RID: 34512 RVA: 0x001E8EEC File Offset: 0x001E70EC
		// (set) Token: 0x060086D1 RID: 34513 RVA: 0x001E8EF9 File Offset: 0x001E70F9
		[NotifyParentProperty(true)]
		[DefaultValue("Sorted asc")]
		public string SortIconAscTooltip
		{
			get
			{
				return this.GetString("SortIconAscTooltip");
			}
			set
			{
				this.SetString("SortIconAscTooltip", value);
			}
		}

		// Token: 0x17002AD5 RID: 10965
		// (get) Token: 0x060086D2 RID: 34514 RVA: 0x001E8F07 File Offset: 0x001E7107
		// (set) Token: 0x060086D3 RID: 34515 RVA: 0x001E8F14 File Offset: 0x001E7114
		[NotifyParentProperty(true)]
		[DefaultValue("Sort descending")]
		public string SortIconAscText
		{
			get
			{
				return this.GetString("SortIconAscText");
			}
			set
			{
				this.SetString("SortIconAscText", value);
			}
		}

		// Token: 0x17002AD6 RID: 10966
		// (get) Token: 0x060086D4 RID: 34516 RVA: 0x001E8F22 File Offset: 0x001E7122
		// (set) Token: 0x060086D5 RID: 34517 RVA: 0x001E8F2F File Offset: 0x001E712F
		[NotifyParentProperty(true)]
		[DefaultValue("Open filter window")]
		public string OpenFilterWindowTooltip
		{
			get
			{
				return this.GetString("OpenFilterWindowTooltip");
			}
			set
			{
				this.SetString("OpenFilterWindowTooltip", value);
			}
		}

		// Token: 0x17002AD7 RID: 10967
		// (get) Token: 0x060086D6 RID: 34518 RVA: 0x001E8F3D File Offset: 0x001E713D
		// (set) Token: 0x060086D7 RID: 34519 RVA: 0x001E8F4A File Offset: 0x001E714A
		[NotifyParentProperty(true)]
		[DefaultValue("Sorted desc")]
		public string SortIconDescTooltip
		{
			get
			{
				return this.GetString("SortIconDescTooltip");
			}
			set
			{
				this.SetString("SortIconDescTooltip", value);
			}
		}

		// Token: 0x17002AD8 RID: 10968
		// (get) Token: 0x060086D8 RID: 34520 RVA: 0x001E8F58 File Offset: 0x001E7158
		// (set) Token: 0x060086D9 RID: 34521 RVA: 0x001E8F65 File Offset: 0x001E7165
		[DefaultValue("Sort ascending")]
		[NotifyParentProperty(true)]
		public string SortIconDescText
		{
			get
			{
				return this.GetString("SortIconDescText");
			}
			set
			{
				this.SetString("SortIconDescText", value);
			}
		}

		// Token: 0x17002AD9 RID: 10969
		// (get) Token: 0x060086DA RID: 34522 RVA: 0x001E8F73 File Offset: 0x001E7173
		// (set) Token: 0x060086DB RID: 34523 RVA: 0x001E8F80 File Offset: 0x001E7180
		[NotifyParentProperty(true)]
		[DefaultValue("OK")]
		public string FiltersWindowOKButtonText
		{
			get
			{
				return this.GetString("FiltersWindowOKButtonText");
			}
			set
			{
				this.SetString("FiltersWindowOKButtonText", value);
			}
		}

		// Token: 0x17002ADA RID: 10970
		// (get) Token: 0x060086DC RID: 34524 RVA: 0x001E8F8E File Offset: 0x001E718E
		// (set) Token: 0x060086DD RID: 34525 RVA: 0x001E8F9B File Offset: 0x001E719B
		[NotifyParentProperty(true)]
		[DefaultValue("Ignore Case")]
		public string FiltersWindowIgnoreCaseCheckBoxText
		{
			get
			{
				return this.GetString("FiltersWindowIgnoreCaseCheckBoxText");
			}
			set
			{
				this.SetString("FiltersWindowIgnoreCaseCheckBoxText", value);
			}
		}

		// Token: 0x17002ADB RID: 10971
		// (get) Token: 0x060086DE RID: 34526 RVA: 0x001E8FA9 File Offset: 0x001E71A9
		// (set) Token: 0x060086DF RID: 34527 RVA: 0x001E8FB6 File Offset: 0x001E71B6
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		public string FilterWindowCancelButtonText
		{
			get
			{
				return this.GetString("FilterWindowCancelButtonText");
			}
			set
			{
				this.SetString("FilterWindowCancelButtonText", value);
			}
		}

		// Token: 0x17002ADC RID: 10972
		// (get) Token: 0x060086E0 RID: 34528 RVA: 0x001E8FC4 File Offset: 0x001E71C4
		// (set) Token: 0x060086E1 RID: 34529 RVA: 0x001E8FD1 File Offset: 0x001E71D1
		[DefaultValue("Expand")]
		[NotifyParentProperty(true)]
		public string ExpandButtonToolTip
		{
			get
			{
				return this.GetString("ExpandButtonToolTip");
			}
			set
			{
				this.SetString("ExpandButtonToolTip", value);
			}
		}

		// Token: 0x17002ADD RID: 10973
		// (get) Token: 0x060086E2 RID: 34530 RVA: 0x001E8FDF File Offset: 0x001E71DF
		// (set) Token: 0x060086E3 RID: 34531 RVA: 0x001E8FEC File Offset: 0x001E71EC
		[NotifyParentProperty(true)]
		[DefaultValue("Collapse")]
		public string CollapseButtonToolTip
		{
			get
			{
				return this.GetString("CollapseButtonToolTip");
			}
			set
			{
				this.SetString("CollapseButtonToolTip", value);
			}
		}

		// Token: 0x17002ADE RID: 10974
		// (get) Token: 0x060086E4 RID: 34532 RVA: 0x001E8FFA File Offset: 0x001E71FA
		// (set) Token: 0x060086E5 RID: 34533 RVA: 0x001E9007 File Offset: 0x001E7207
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

		// Token: 0x17002ADF RID: 10975
		// (get) Token: 0x060086E6 RID: 34534 RVA: 0x001E9015 File Offset: 0x001E7215
		// (set) Token: 0x060086E7 RID: 34535 RVA: 0x001E9022 File Offset: 0x001E7222
		[NotifyParentProperty(true)]
		[DefaultValue("Grand Total")]
		public string GrandTotalText
		{
			get
			{
				return this.GetString("GrandTotalText");
			}
			set
			{
				this.SetString("GrandTotalText", value);
			}
		}

		// Token: 0x17002AE0 RID: 10976
		// (get) Token: 0x060086E8 RID: 34536 RVA: 0x001E9030 File Offset: 0x001E7230
		// (set) Token: 0x060086E9 RID: 34537 RVA: 0x001E903D File Offset: 0x001E723D
		[DefaultValue("Total {0}")]
		[NotifyParentProperty(true)]
		public string TotalValueFormat
		{
			get
			{
				return this.GetString("TotalValueFormat");
			}
			set
			{
				this.SetString("TotalValueFormat", value);
			}
		}

		// Token: 0x17002AE1 RID: 10977
		// (get) Token: 0x060086EA RID: 34538 RVA: 0x001E904B File Offset: 0x001E724B
		// (set) Token: 0x060086EB RID: 34539 RVA: 0x001E9058 File Offset: 0x001E7258
		[NotifyParentProperty(true)]
		[DefaultValue("{0} Total")]
		public string ValueTotalFormat
		{
			get
			{
				return this.GetString("ValueTotalFormat");
			}
			set
			{
				this.SetString("ValueTotalFormat", value);
			}
		}

		// Token: 0x17002AE2 RID: 10978
		// (get) Token: 0x060086EC RID: 34540 RVA: 0x001E9066 File Offset: 0x001E7266
		// (set) Token: 0x060086ED RID: 34541 RVA: 0x001E9073 File Offset: 0x001E7273
		[DefaultValue("Error")]
		[NotifyParentProperty(true)]
		public string ErrorValueText
		{
			get
			{
				return this.GetString("ErrorValueText");
			}
			set
			{
				this.SetString("ErrorValueText", value);
			}
		}

		// Token: 0x060086EE RID: 34542 RVA: 0x001E9081 File Offset: 0x001E7281
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x0400254A RID: 9546
		private readonly LocalizationProvider _localizationProvider;
	}
}
