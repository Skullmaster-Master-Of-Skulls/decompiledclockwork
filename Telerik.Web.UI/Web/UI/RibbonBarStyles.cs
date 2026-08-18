using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000EC9 RID: 3785
	internal static class RibbonBarStyles
	{
		// Token: 0x06009047 RID: 36935 RVA: 0x002079E0 File Offset: 0x00205BE0
		internal static string Combine(params string[] classNames)
		{
			List<string> list = new List<string>(classNames);
			string[] value = list.FindAll((string className) => !string.IsNullOrEmpty(className)).ToArray();
			return string.Join(" ", value).Trim();
		}

		// Token: 0x0400283B RID: 10299
		public const string RibbonContentWrapOuterCssClass = "rrbContentWrapOut";

		// Token: 0x0400283C RID: 10300
		public const string RibbonContentWrapMiddleCssClass = "rrbContentWrapMid";

		// Token: 0x0400283D RID: 10301
		public const string RibbonContentWrapInnerCssClass = "rrbContentWrapIn";

		// Token: 0x0400283E RID: 10302
		public const string RibbonTabsContainerCssClass = "rrbTabs";

		// Token: 0x0400283F RID: 10303
		public const string RibbonTabStripCssClass = "rrbTabStrip";

		// Token: 0x04002840 RID: 10304
		public const string RibbonButtonAreaCssClass = "rrbButtonArea";

		// Token: 0x04002841 RID: 10305
		public const string RibbonButtonAreaInnerCssClass = "rrbButtonAreaIn";

		// Token: 0x04002842 RID: 10306
		public const string RibbonHiddenButtonAreaInnerCssClass = "rrbHiddenButtonAreaIn";

		// Token: 0x04002843 RID: 10307
		public const string RibbonBarResizeHandleCssClass = "rrbResizeHandle";

		// Token: 0x04002844 RID: 10308
		public const string RibbonBarToggleHandleCssClass = "rrbToggleHandle";

		// Token: 0x04002845 RID: 10309
		public const string RibbonBarDisabledItemCssClass = "rrbDisabled";

		// Token: 0x04002846 RID: 10310
		public const string RibbonBarExtendedChromeCssClass = "rrbExtendedChrome";

		// Token: 0x04002847 RID: 10311
		public const string RibbonBarKeyboardNavigationBox = "rrbKeyBox";

		// Token: 0x04002848 RID: 10312
		public const string DropDownPopupCssClass = "rrbPopup";

		// Token: 0x04002849 RID: 10313
		public const string RibbonBarUlCssClass = "rrbUL";

		// Token: 0x0400284A RID: 10314
		public const string RibbonBarWrapCssClass = "rrbWrap";

		// Token: 0x0400284B RID: 10315
		public const string RibbonBarImageCssClass = "rrbImage";

		// Token: 0x0400284C RID: 10316
		public const string RibbonBarTextCssClass = "rrbText";

		// Token: 0x0400284D RID: 10317
		public const string RibbonBarExpandedCssClass = "rrbExpanded";

		// Token: 0x0400284E RID: 10318
		public const string RibbonBarHiddenCssClass = "rrbHidden";

		// Token: 0x0400284F RID: 10319
		public const string RibbonBarSelectedCssClass = "rrbSelected";

		// Token: 0x04002850 RID: 10320
		public const string RibbonBarButtonCssClass = "rrbButton";

		// Token: 0x04002851 RID: 10321
		public const string RibbonBarItemCssClass = "rrbItem";

		// Token: 0x04002852 RID: 10322
		public const string RibbonBarSmallButtonCssClass = "rrbSmallButton";

		// Token: 0x04002853 RID: 10323
		public const string RibbonBarTitleCssClass = "rrbTitle";

		// Token: 0x04002854 RID: 10324
		public const string RibbonBarIconCssClass = "radIcon";

		// Token: 0x04002855 RID: 10325
		public const string RibbonBarIconUpCssClass = "radIconUp";

		// Token: 0x04002856 RID: 10326
		public const string RibbonBarIconDownCssClass = "radIconDown";

		// Token: 0x04002857 RID: 10327
		public const string RibbonBarIconRightCssClass = "radIconRight";

		// Token: 0x04002858 RID: 10328
		public const string RibbonBarIconExpandCssClass = "radIconExpand";

		// Token: 0x04002859 RID: 10329
		public const string RibbonBarQuickAccessToolbarCssClass = "rrbQat";

		// Token: 0x0400285A RID: 10330
		public const string RibbonBarHeaderCssClass = "rrbHeader";

		// Token: 0x0400285B RID: 10331
		public const string RibbonBarItemCheckboxCssClass = "rrbItemCheckbox";

		// Token: 0x0400285C RID: 10332
		public const string RibbonBarCheckedCssClass = "rrbChecked";

		// Token: 0x0400285D RID: 10333
		public const string RibbonBarItemRadioCssClass = "rrbItemRadio";

		// Token: 0x0400285E RID: 10334
		public const string RibbonBarLinkCssClass = "rrbLink";

		// Token: 0x0400285F RID: 10335
		public const string RibbonBarContextualItemCssClass = "rrbContextualItem";

		// Token: 0x04002860 RID: 10336
		public const string RibbonBarActiveCssClass = "rrbActive";

		// Token: 0x04002861 RID: 10337
		public const string RibbonBarArrowCssClass = "rrbArrow";

		// Token: 0x04002862 RID: 10338
		public const string RibbonBarLabelCssClass = "rrbLabel";

		// Token: 0x04002863 RID: 10339
		public const string RibbonBarCommandAreaCssClass = "rrbCommandArea";

		// Token: 0x04002864 RID: 10340
		public const string RibbonBarCommandsCssClass = "rrbCommands";

		// Token: 0x04002865 RID: 10341
		public const string RibbonBarCommandGroupCssClass = "rrbCommandGroup";

		// Token: 0x04002866 RID: 10342
		public const string RibbonBarButtonGroupCssClass = "rrbButtonGroup";

		// Token: 0x04002867 RID: 10343
		public const string RibbonBarMenuCssClass = "rrbMenu";

		// Token: 0x04002868 RID: 10344
		public const string RibbonBarInnerCssClass = "rrbInner";

		// Token: 0x04002869 RID: 10345
		public const string RibbonBarToggleCssClass = "rrbToggle";

		// Token: 0x0400286A RID: 10346
		public const string RibbonBarDescriptionCssClass = "rrbDescription";

		// Token: 0x0400286B RID: 10347
		public const string RibbonBarFooterCssClass = "rrbFooter";

		// Token: 0x0400286C RID: 10348
		public const string RibbonBarTemplateCssClass = "rrbTemplate";

		// Token: 0x0400286D RID: 10349
		public const string RibbonBarGroupCssClass = "rrbGroup";

		// Token: 0x02000ECA RID: 3786
		public static class RibbonBarTab
		{
			// Token: 0x0400286F RID: 10351
			public const string RibbonBarTabCssClass = "rrbTab";

			// Token: 0x04002870 RID: 10352
			public const string RibbonBarTabSelectedCssClass = "rrbSelectedTab";

			// Token: 0x04002871 RID: 10353
			public const string RibbonBarTabLabelCssClass = "rrbTabLabel";

			// Token: 0x04002872 RID: 10354
			public const string RibbonBarTabTextCssClass = "rrbTabText";
		}

		// Token: 0x02000ECB RID: 3787
		public static class QuickAccessToolbar
		{
			// Token: 0x04002873 RID: 10355
			public const string CssClass = "rrbQuickAccessToolbar";

			// Token: 0x04002874 RID: 10356
			public const string ItemDisplayNone = "none";

			// Token: 0x04002875 RID: 10357
			public const string ItemCssClass = "rrbQatItem";

			// Token: 0x04002876 RID: 10358
			public const string DropDownCssClass = "rrbQatDropDown";

			// Token: 0x04002877 RID: 10359
			public const string DropDownArrowLinkHref = "#";

			// Token: 0x04002878 RID: 10360
			public const string DropDownArrowCssClass = "rrbQatButton";

			// Token: 0x04002879 RID: 10361
			public const string DropDownSlideCssClass = "rrbSlide";

			// Token: 0x0400287A RID: 10362
			public const string MenuCssClass = "rrbMenu";

			// Token: 0x0400287B RID: 10363
			public const string MenuGroupCssClass = "rrbMenuGroup";

			// Token: 0x0400287C RID: 10364
			public const string MenuLabelCssClass = "rrbMenuLabel";

			// Token: 0x0400287D RID: 10365
			public const string MenuItemCssClass = "rrbMenuItem";

			// Token: 0x0400287E RID: 10366
			public const string MenuItemCheckboxCssClass = "rrbMenuItemCheckbox";

			// Token: 0x0400287F RID: 10367
			public const string MenuItemCheckboxCheckedCssClass = "rrbMenuItemCheckboxChecked";

			// Token: 0x04002880 RID: 10368
			public const string DropDownItemCheckedCssClass = "rrbChecked";

			// Token: 0x04002881 RID: 10369
			public const string DropDownItemInputTypeCheckbox = "checkbox";

			// Token: 0x04002882 RID: 10370
			public const string DropDownItemCheckBoxChecked = "checked";
		}

		// Token: 0x02000ECC RID: 3788
		public static class ApplicationMenu
		{
			// Token: 0x04002883 RID: 10371
			public const string HeaderBarCssClass = "rrbHeaderBar";

			// Token: 0x04002884 RID: 10372
			public const string HeaderCssClass = "rrbHeader";

			// Token: 0x04002885 RID: 10373
			public const string DropDownSlideCssClass = "rrbSlide";

			// Token: 0x04002886 RID: 10374
			public const string DropDownPopupCssClass = "rrbApplicationMenuPopup";

			// Token: 0x04002887 RID: 10375
			public const string MenuCssClass = "rrbMenu";

			// Token: 0x04002888 RID: 10376
			public const string ApplicationMenuCssClass = "rrbApplicationMenu";

			// Token: 0x04002889 RID: 10377
			public const string MenuGroupCssClass = "rrbMenuGroup";

			// Token: 0x0400288A RID: 10378
			public const string MenuItemCssClass = "rrbMenuItem";

			// Token: 0x0400288B RID: 10379
			public const string MenuItemInnerCssClass = "rrbMIInner";

			// Token: 0x0400288C RID: 10380
			public const string MenuItemImageCssClass = "rrbMIImage";

			// Token: 0x0400288D RID: 10381
			public const string MenuItemTextCssClass = "rrbMIText";

			// Token: 0x0400288E RID: 10382
			public const string MenuItemDescriptionCssClass = "rrbMIDesc";

			// Token: 0x0400288F RID: 10383
			public const string SplitMenuItemCssClass = "rrbSplitMenuItem";

			// Token: 0x04002890 RID: 10384
			public const string SplitMenuItemToggleCssClass = "rrbMIToggle";

			// Token: 0x04002891 RID: 10385
			public const string SplitMenuItemIconCssClass = "rrbIcon";

			// Token: 0x04002892 RID: 10386
			public const string FooterPaneCssClass = "rrbFooterPane";

			// Token: 0x04002893 RID: 10387
			public const string AuxiliaryPaneCssClass = "rrbAuxiliaryPane";

			// Token: 0x04002894 RID: 10388
			public const string AuxiliaryPaneContentCssClass = "rrbAPTemplate";
		}

		// Token: 0x02000ECD RID: 3789
		public static class RibbonBarContextualTabGroup
		{
			// Token: 0x04002895 RID: 10389
			public const string CssClass = "rrbContextualTab";

			// Token: 0x04002896 RID: 10390
			public const string ActiveCssClass = "rrbContextualTabActive";

			// Token: 0x04002897 RID: 10391
			public const string LabelCssClass = "rrbContextualTabLabel";

			// Token: 0x04002898 RID: 10392
			public const string LabelLinkHref = "#";
		}

		// Token: 0x02000ECE RID: 3790
		public static class RibbonBarGroup
		{
			// Token: 0x04002899 RID: 10393
			public const string RibbonGroupCssClass = "rrbButtonGroup";

			// Token: 0x0400289A RID: 10394
			public const string RibbonGroupInnerCssClass = "rrbButtonGroupIn";

			// Token: 0x0400289B RID: 10395
			public const string RibbonGroupTitleCssClass = "rrbGroupTitle";

			// Token: 0x0400289C RID: 10396
			public const string RibbonGroupLauncherCssClass = "rrbGroupLauncher";

			// Token: 0x0400289D RID: 10397
			public const string RibbonGroupTitleLinkHref = "#";
		}

		// Token: 0x02000ECF RID: 3791
		public static class RibbonBarClickableItem
		{
			// Token: 0x0400289E RID: 10398
			public const string RibbonBarButtonLinkHref = "#";

			// Token: 0x0400289F RID: 10399
			public const string RibbonBarButtonImageDefaultAltText = "Item Image";

			// Token: 0x040028A0 RID: 10400
			public const string RibbonBarButtonOuterCssClass = "rrbButtonOut";

			// Token: 0x040028A1 RID: 10401
			public const string RibbonBarButtonStripPartOuterCssClass = "rrbButtonStripPart";

			// Token: 0x040028A2 RID: 10402
			public const string RibbonBarButtonSizeSmallCssClass = "rrbButton";

			// Token: 0x040028A3 RID: 10403
			public const string RibbonBarButtonSizeMediumCssClass = "rrbMediumButton";

			// Token: 0x040028A4 RID: 10404
			public const string RibbonBarButtonSizeLargeCssClass = "rrbLargeButton";

			// Token: 0x040028A5 RID: 10405
			public const string RibbonBarImageRenderingDualModeCssClss = "rrbDualImage";

			// Token: 0x040028A6 RID: 10406
			public const string RibbonBarButtonMiddleCssClass = "rrbButtonMid";

			// Token: 0x040028A7 RID: 10407
			public const string RibbonBarButtonInnerCssClass = "rrbButtonIn";

			// Token: 0x040028A8 RID: 10408
			public const string RibbonBarButtonImagePlaceHolderCssClass = "rrbImagePlaceholder";

			// Token: 0x040028A9 RID: 10409
			public const string RibbonBarButtonImageCssClass = "rrbButtonImage";

			// Token: 0x040028AA RID: 10410
			public const string RibbonBarButtonTextCssClass = "rrbButtonText";

			// Token: 0x040028AB RID: 10411
			public const string RibbonBarButtonTextContentCssClass = "rrbTextContent";

			// Token: 0x040028AC RID: 10412
			public const string RibbonBarButtonBlankImagePath = "Telerik.Web.UI.Skins.Common.RibbonBar.NoImage.png";

			// Token: 0x040028AD RID: 10413
			public const string RibbonBarButtonBlankImageLargePath = "Telerik.Web.UI.Skins.Common.RibbonBar.NoImageLarge.png";

			// Token: 0x040028AE RID: 10414
			public const string RibbonBarButtonBlankDisabledImagePath = "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImage.png";

			// Token: 0x040028AF RID: 10415
			public const string RibbonBarButtonBlankDisabledImageLargePath = "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImageLarge.png";
		}

		// Token: 0x02000ED0 RID: 3792
		public static class RibbonBarMenuBaseItem
		{
			// Token: 0x040028B0 RID: 10416
			public const string RibbonBarItemArrowCssClass = "rrbButtonArrow";

			// Token: 0x040028B1 RID: 10417
			public const string RibbonBarItemIconCssClass = "rrbIcon";

			// Token: 0x040028B2 RID: 10418
			public const string RibbonBarMenuDropDownOuterCssClass = "rrbMenuGroupOut";

			// Token: 0x040028B3 RID: 10419
			public const string RibbonBarMenuDropDownMiddleCssClass = "rrbMenuGroupMid";

			// Token: 0x040028B4 RID: 10420
			public const string RibbonBarMenuDropDownInnerCssClass = "rrbMenuGroupIn";
		}

		// Token: 0x02000ED1 RID: 3793
		public static class RibbonBarMenu
		{
			// Token: 0x040028B5 RID: 10421
			public const string RibbonBarMenuTypeCssClass = "rrbMenuButton";
		}

		// Token: 0x02000ED2 RID: 3794
		public static class RibbonBarMenuItem
		{
			// Token: 0x040028B6 RID: 10422
			public const string RibbonBarMenuItemSubMenuTypeCssClass = "rrbSubMenu";
		}

		// Token: 0x02000ED3 RID: 3795
		public static class RibbonBarSplitButton
		{
			// Token: 0x040028B7 RID: 10423
			public const string RibbonBarItemSplitButtonCssClass = "rrbSplitButton";
		}

		// Token: 0x02000ED4 RID: 3796
		public static class RibbonBarButtonStrip
		{
			// Token: 0x040028B8 RID: 10424
			public const string RibbonBarButtonStripTypeCssClass = "rrbButtonStrip";

			// Token: 0x040028B9 RID: 10425
			public const string RibbonBarButtonStripOuterCssClass = "rrbButtonOut";
		}

		// Token: 0x02000ED5 RID: 3797
		public static class RibbonBarToggleButton
		{
			// Token: 0x040028BA RID: 10426
			public const string RibbonBarItemToggleButtonCssClass = "rrbToggleButton";

			// Token: 0x040028BB RID: 10427
			public const string RibbonBarToggleButtonToggledCssClass = "rrbToggled";
		}

		// Token: 0x02000ED6 RID: 3798
		public static class RibbonBarTemplateItem
		{
			// Token: 0x040028BC RID: 10428
			public const string RibbonBarTemplateItemCssClass = "rrbTemplateItem";

			// Token: 0x040028BD RID: 10429
			public const string RibbonBarTemplateItemLargeCssClass = "rrbTemplateItemLarge";
		}

		// Token: 0x02000ED7 RID: 3799
		public static class RibbonBarGallery
		{
			// Token: 0x040028BE RID: 10430
			public const string RibbonBarGalleryCssClass = "rrbGallery";

			// Token: 0x040028BF RID: 10431
			public const string RibbonBarGalleryScrollWrapCssClass = "rrbGalleryScrollWrap";

			// Token: 0x040028C0 RID: 10432
			public const string RibbonBarCategoryCssClass = "rrbCategory";

			// Token: 0x040028C1 RID: 10433
			public const string RibbonBarCategoryTitleCssClass = "rrbCategoryTitle";

			// Token: 0x040028C2 RID: 10434
			public const string RibbonBarGalleryItemCssClass = "rrbGalleryItem";

			// Token: 0x040028C3 RID: 10435
			public const string RibbonBarGalleryItemSelectedCssClass = "rrbGalleryItemSelected";

			// Token: 0x040028C4 RID: 10436
			public const string RibbonBarGalleryItemInnerCssClass = "rrbGalleryItemInner";

			// Token: 0x040028C5 RID: 10437
			public const string RibbonBarGalleryItemTextCssClass = "rrbGalleryItemText";

			// Token: 0x040028C6 RID: 10438
			public const string RibbonBarGalleryItemImageCssClass = "rrbGalleryItemImage";

			// Token: 0x040028C7 RID: 10439
			public const string RibbonBarGalleryItemTextPositionBottom = "rrbGalleryTextPositionBottom";

			// Token: 0x040028C8 RID: 10440
			public const string RibbonBarGalleryItemTextPositionInline = "rrbGalleryTextPositionInline";

			// Token: 0x040028C9 RID: 10441
			public const string RibbonBarGalleryItemTextPositionNone = "rrbGalleryTextPositionNone";

			// Token: 0x040028CA RID: 10442
			public const string RibbonBarActionsWrapperCssClass = "rrbGalleryActions";

			// Token: 0x040028CB RID: 10443
			public const string RibbonBarActionCssClass = "rrbGalleryAction";

			// Token: 0x040028CC RID: 10444
			public const string RibbonBarActionUpCssClass = "rrbGalleryActionUp";

			// Token: 0x040028CD RID: 10445
			public const string RibbonBarActionDownCssClass = "rrbGalleryActionDown";

			// Token: 0x040028CE RID: 10446
			public const string RibbonBarActionExpandCssClass = "rrbGalleryActionExpand";
		}
	}
}
