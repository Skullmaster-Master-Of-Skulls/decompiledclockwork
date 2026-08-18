using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Resources
{
	// Token: 0x020000D9 RID: 217
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class AtlasWeb
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00002050 File Offset: 0x00000250
		internal AtlasWeb()
		{
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00027308 File Offset: 0x00025508
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (AtlasWeb.resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.Resources.AtlasWeb", typeof(AtlasWeb).Assembly);
					AtlasWeb.resourceMan = resourceManager;
				}
				return AtlasWeb.resourceMan;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00027341 File Offset: 0x00025541
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00027348 File Offset: 0x00025548
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return AtlasWeb.resourceCulture;
			}
			set
			{
				AtlasWeb.resourceCulture = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00027350 File Offset: 0x00025550
		internal static string ApplicationServiceManager_Path
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ApplicationServiceManager_Path", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00027366 File Offset: 0x00025566
		internal static string AppService_Disabled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AppService_Disabled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002737C File Offset: 0x0002557C
		internal static string AppService_MultiplePaths
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AppService_MultiplePaths", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00027392 File Offset: 0x00025592
		internal static string AppService_RequiredSSL
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AppService_RequiredSSL", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000273A8 File Offset: 0x000255A8
		internal static string AppService_UnknownProfileProperty
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AppService_UnknownProfileProperty", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x000273BE File Offset: 0x000255BE
		internal static string ArgumentMustBeCurrentUser
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ArgumentMustBeCurrentUser", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000273D4 File Offset: 0x000255D4
		internal static string ArgumentMustBeNull
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ArgumentMustBeNull", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000273EA File Offset: 0x000255EA
		internal static string AsyncPostBackTrigger_CannotFindEvent
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AsyncPostBackTrigger_CannotFindEvent", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00027400 File Offset: 0x00025600
		internal static string AsyncPostBackTrigger_EventName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AsyncPostBackTrigger_EventName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00027416 File Offset: 0x00025616
		internal static string AsyncPostBackTrigger_InvalidEvent
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AsyncPostBackTrigger_InvalidEvent", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002742C File Offset: 0x0002562C
		internal static string AttributeNotRecognized
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("AttributeNotRecognized", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00027442 File Offset: 0x00025642
		internal static string Category_Sorting
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Category_Sorting", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00027458 File Offset: 0x00025658
		internal static string ClientService_BadJsonResponse
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ClientService_BadJsonResponse", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002746E File Offset: 0x0002566E
		internal static string Common_ArgumentInvalidType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Common_ArgumentInvalidType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00027484 File Offset: 0x00025684
		internal static string Common_GreaterThanOrEqualToZero
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Common_GreaterThanOrEqualToZero", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002749A File Offset: 0x0002569A
		internal static string Common_GreaterThanOrEqualToZeroAndLessThanOrEqualToOne
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Common_GreaterThanOrEqualToZeroAndLessThanOrEqualToOne", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000274B0 File Offset: 0x000256B0
		internal static string Common_NullOrEmpty
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Common_NullOrEmpty", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000274C6 File Offset: 0x000256C6
		internal static string Common_PageCannotBeNull
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Common_PageCannotBeNull", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000274DC File Offset: 0x000256DC
		internal static string Common_ScriptManagerRequired
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Common_ScriptManagerRequired", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000274F2 File Offset: 0x000256F2
		internal static string CompositeScriptReference_Scripts
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("CompositeScriptReference_Scripts", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00027508 File Offset: 0x00025708
		internal static string ConvertersCollection_NotJavaScriptConverter
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ConvertersCollection_NotJavaScriptConverter", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002751E File Offset: 0x0002571E
		internal static string ConvertersCollection_UnknownType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ConvertersCollection_UnknownType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00027534 File Offset: 0x00025734
		internal static string DataBoundControlHelper_NoNamingContainer
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataBoundControlHelper_NoNamingContainer", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0002754A File Offset: 0x0002574A
		internal static string DataPager_ControlIsntPageable
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_ControlIsntPageable", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00027560 File Offset: 0x00025760
		internal static string DataPager_Fields
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_Fields", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00027576 File Offset: 0x00025776
		internal static string DataPager_NoNamingContainer
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_NoNamingContainer", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002758C File Offset: 0x0002578C
		internal static string DataPager_NoPageableItemContainer
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_NoPageableItemContainer", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000275A2 File Offset: 0x000257A2
		internal static string DataPager_PageableItemContainerNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_PageableItemContainerNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000275B8 File Offset: 0x000257B8
		internal static string DataPager_PagedControlID
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_PagedControlID", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000275CE File Offset: 0x000257CE
		internal static string DataPager_PagePropertiesCannotBeSet
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_PagePropertiesCannotBeSet", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000275E4 File Offset: 0x000257E4
		internal static string DataPager_PageSize
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_PageSize", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000275FA File Offset: 0x000257FA
		internal static string DataPager_QueryStringField
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPager_QueryStringField", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00027610 File Offset: 0x00025810
		internal static string DataPagerField_Visible
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataPagerField_Visible", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00027626 File Offset: 0x00025826
		internal static string DataSourceControlExtender_TargetControlIDMustBeSpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataSourceControlExtender_TargetControlIDMustBeSpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002763C File Offset: 0x0002583C
		internal static string DataSourceControlExtender_TargetControlMustImplementIDataSource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DataSourceControlExtender_TargetControlMustImplementIDataSource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00027652 File Offset: 0x00025852
		internal static string DynamicControlBase_ConvertEmptyStringToNull
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicControlBase_ConvertEmptyStringToNull", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00027668 File Offset: 0x00025868
		internal static string DynamicControlBase_DataField
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicControlBase_DataField", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002767E File Offset: 0x0002587E
		internal static string DynamicControlBase_DataFormatString
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicControlBase_DataFormatString", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00027694 File Offset: 0x00025894
		internal static string DynamicControlBase_HtmlEncode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicControlBase_HtmlEncode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000276AA File Offset: 0x000258AA
		internal static string DynamicControlBase_NullDisplayText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicControlBase_NullDisplayText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000276C0 File Offset: 0x000258C0
		internal static string DynamicControlBase_UIHint
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicControlBase_UIHint", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000276D6 File Offset: 0x000258D6
		internal static string DynamicControlBase_ValidationGroup
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicControlBase_ValidationGroup", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000276EC File Offset: 0x000258EC
		internal static string DynamicFilterRepeater_DynamicFilterContainerId
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicFilterRepeater_DynamicFilterContainerId", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00027702 File Offset: 0x00025902
		internal static string DynamicNavigatorDataSource_NoAccessibleTablesFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicNavigatorDataSource_NoAccessibleTablesFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00027718 File Offset: 0x00025918
		internal static string DynamicNavigatorDataSource_NoModelsRegistered
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicNavigatorDataSource_NoModelsRegistered", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002772E File Offset: 0x0002592E
		internal static string DynamicNavigatorDataSource_NoTablesInModels
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("DynamicNavigatorDataSource_NoTablesInModels", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00027744 File Offset: 0x00025944
		internal static string ExpressionParser_AmbiguousConstructorInvocation
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_AmbiguousConstructorInvocation", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002775A File Offset: 0x0002595A
		internal static string ExpressionParser_AmbiguousIndexerInvocation
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_AmbiguousIndexerInvocation", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00027770 File Offset: 0x00025970
		internal static string ExpressionParser_AmbiguousMethodInvocation
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_AmbiguousMethodInvocation", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00027786 File Offset: 0x00025986
		internal static string ExpressionParser_ArgsIncompatibleWithLambda
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_ArgsIncompatibleWithLambda", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002779C File Offset: 0x0002599C
		internal static string ExpressionParser_BothTypesConvertToOther
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_BothTypesConvertToOther", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000277B2 File Offset: 0x000259B2
		internal static string ExpressionParser_CannotConvertValue
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_CannotConvertValue", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x000277C8 File Offset: 0x000259C8
		internal static string ExpressionParser_CannotIndexMultipleDimensionalArray
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_CannotIndexMultipleDimensionalArray", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x000277DE File Offset: 0x000259DE
		internal static string ExpressionParser_CloseBracketOrCommaExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_CloseBracketOrCommaExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000277F4 File Offset: 0x000259F4
		internal static string ExpressionParser_CloseParenOrCommaExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_CloseParenOrCommaExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002780A File Offset: 0x00025A0A
		internal static string ExpressionParser_CloseParenOrOperatorExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_CloseParenOrOperatorExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00027820 File Offset: 0x00025A20
		internal static string ExpressionParser_ColonExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_ColonExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00027836 File Offset: 0x00025A36
		internal static string ExpressionParser_DigitExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_DigitExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002784C File Offset: 0x00025A4C
		internal static string ExpressionParser_DotOrOpenParenExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_DotOrOpenParenExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00027862 File Offset: 0x00025A62
		internal static string ExpressionParser_DuplicateIdentifier
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_DuplicateIdentifier", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00027878 File Offset: 0x00025A78
		internal static string ExpressionParser_ExpressionExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_ExpressionExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002788E File Offset: 0x00025A8E
		internal static string ExpressionParser_ExpressionTypeMismatch
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_ExpressionTypeMismatch", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x000278A4 File Offset: 0x00025AA4
		internal static string ExpressionParser_FirstExprMustBeBool
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_FirstExprMustBeBool", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000278BA File Offset: 0x00025ABA
		internal static string ExpressionParser_IdentifierExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_IdentifierExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000278D0 File Offset: 0x00025AD0
		internal static string ExpressionParser_IifRequiresThreeArgs
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_IifRequiresThreeArgs", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x000278E6 File Offset: 0x00025AE6
		internal static string ExpressionParser_IncompatibleOperand
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_IncompatibleOperand", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000278FC File Offset: 0x00025AFC
		internal static string ExpressionParser_IncompatibleOperands
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_IncompatibleOperands", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00027912 File Offset: 0x00025B12
		internal static string ExpressionParser_InvalidCharacter
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_InvalidCharacter", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00027928 File Offset: 0x00025B28
		internal static string ExpressionParser_InvalidCharacterLiteral
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_InvalidCharacterLiteral", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0002793E File Offset: 0x00025B3E
		internal static string ExpressionParser_InvalidIndex
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_InvalidIndex", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00027954 File Offset: 0x00025B54
		internal static string ExpressionParser_InvalidIntegerLiteral
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_InvalidIntegerLiteral", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002796A File Offset: 0x00025B6A
		internal static string ExpressionParser_InvalidRealLiteral
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_InvalidRealLiteral", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00027980 File Offset: 0x00025B80
		internal static string ExpressionParser_MethodIsVoid
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_MethodIsVoid", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00027996 File Offset: 0x00025B96
		internal static string ExpressionParser_MethodsAreInaccessible
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_MethodsAreInaccessible", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000279AC File Offset: 0x00025BAC
		internal static string ExpressionParser_MissingAsClause
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_MissingAsClause", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x000279C2 File Offset: 0x00025BC2
		internal static string ExpressionParser_NeitherTypeConvertsToOther
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_NeitherTypeConvertsToOther", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000279D8 File Offset: 0x00025BD8
		internal static string ExpressionParser_NoApplicableAggregate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_NoApplicableAggregate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x000279EE File Offset: 0x00025BEE
		internal static string ExpressionParser_NoApplicableIndexer
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_NoApplicableIndexer", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00027A04 File Offset: 0x00025C04
		internal static string ExpressionParser_NoApplicableMethod
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_NoApplicableMethod", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00027A1A File Offset: 0x00025C1A
		internal static string ExpressionParser_NoItInScope
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_NoItInScope", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00027A30 File Offset: 0x00025C30
		internal static string ExpressionParser_NoMatchingConstructor
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_NoMatchingConstructor", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00027A46 File Offset: 0x00025C46
		internal static string ExpressionParser_OpenBracketExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_OpenBracketExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00027A5C File Offset: 0x00025C5C
		internal static string ExpressionParser_OpenParenExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_OpenParenExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00027A72 File Offset: 0x00025C72
		internal static string ExpressionParser_SyntaxError
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_SyntaxError", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00027A88 File Offset: 0x00025C88
		internal static string ExpressionParser_TokenExpected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_TokenExpected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00027A9E File Offset: 0x00025C9E
		internal static string ExpressionParser_TypeHasNoNullableForm
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_TypeHasNoNullableForm", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00027AB4 File Offset: 0x00025CB4
		internal static string ExpressionParser_UnknownIdentifier
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_UnknownIdentifier", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00027ACA File Offset: 0x00025CCA
		internal static string ExpressionParser_UnknownPropertyOrField
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_UnknownPropertyOrField", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00027AE0 File Offset: 0x00025CE0
		internal static string ExpressionParser_UnterminatedStringLiteral
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExpressionParser_UnterminatedStringLiteral", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00027AF6 File Offset: 0x00025CF6
		internal static string Expressions_DataFieldRequired
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Expressions_DataFieldRequired", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00027B0C File Offset: 0x00025D0C
		internal static string ExtenderControl_TargetControlDifferentUpdatePanel
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExtenderControl_TargetControlDifferentUpdatePanel", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00027B22 File Offset: 0x00025D22
		internal static string ExtenderControl_TargetControlID
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExtenderControl_TargetControlID", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00027B38 File Offset: 0x00025D38
		internal static string ExtenderControl_TargetControlIDEmpty
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExtenderControl_TargetControlIDEmpty", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00027B4E File Offset: 0x00025D4E
		internal static string ExtenderControl_TargetControlIDInvalid
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ExtenderControl_TargetControlIDInvalid", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00027B64 File Offset: 0x00025D64
		internal static string FilterRepeater_TableName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("FilterRepeater_TableName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00027B7A File Offset: 0x00025D7A
		internal static string JSON_ArrayTypeNotSupported
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_ArrayTypeNotSupported", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00027B90 File Offset: 0x00025D90
		internal static string JSON_BadEscape
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_BadEscape", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00027BA6 File Offset: 0x00025DA6
		internal static string JSON_CannotConvertObjectToType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_CannotConvertObjectToType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00027BBC File Offset: 0x00025DBC
		internal static string JSON_CannotCreateListType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_CannotCreateListType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00027BD2 File Offset: 0x00025DD2
		internal static string JSON_CannotSerializeMemberGeneric
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_CannotSerializeMemberGeneric", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00027BE8 File Offset: 0x00025DE8
		internal static string JSON_CircularReference
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_CircularReference", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00027BFE File Offset: 0x00025DFE
		internal static string JSON_DepthLimitExceeded
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_DepthLimitExceeded", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00027C14 File Offset: 0x00025E14
		internal static string JSON_DeserializerTypeMismatch
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_DeserializerTypeMismatch", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00027C2A File Offset: 0x00025E2A
		internal static string JSON_DictionaryTypeNotSupported
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_DictionaryTypeNotSupported", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00027C40 File Offset: 0x00025E40
		internal static string JSON_ExpectedOpenBrace
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_ExpectedOpenBrace", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00027C56 File Offset: 0x00025E56
		internal static string JSON_IllegalPrimitive
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_IllegalPrimitive", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00027C6C File Offset: 0x00025E6C
		internal static string JSON_InvalidArrayEnd
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidArrayEnd", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00027C82 File Offset: 0x00025E82
		internal static string JSON_InvalidArrayExpectComma
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidArrayExpectComma", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00027C98 File Offset: 0x00025E98
		internal static string JSON_InvalidArrayExtraComma
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidArrayExtraComma", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00027CAE File Offset: 0x00025EAE
		internal static string JSON_InvalidArrayStart
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidArrayStart", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00027CC4 File Offset: 0x00025EC4
		internal static string JSON_InvalidEnumType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidEnumType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00027CDA File Offset: 0x00025EDA
		internal static string JSON_InvalidMaxJsonLength
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidMaxJsonLength", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00027CF0 File Offset: 0x00025EF0
		internal static string JSON_InvalidMemberName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidMemberName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00027D06 File Offset: 0x00025F06
		internal static string JSON_InvalidObject
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidObject", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00027D1C File Offset: 0x00025F1C
		internal static string JSON_InvalidRecursionLimit
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_InvalidRecursionLimit", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00027D32 File Offset: 0x00025F32
		internal static string JSON_MaxJsonLengthExceeded
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_MaxJsonLengthExceeded", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00027D48 File Offset: 0x00025F48
		internal static string JSON_NoConstructor
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_NoConstructor", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00027D5E File Offset: 0x00025F5E
		internal static string JSON_StringNotQuoted
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_StringNotQuoted", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00027D74 File Offset: 0x00025F74
		internal static string JSON_UnterminatedString
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_UnterminatedString", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00027D8A File Offset: 0x00025F8A
		internal static string JSON_ValueTypeCannotBeNull
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("JSON_ValueTypeCannotBeNull", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00027DA0 File Offset: 0x00025FA0
		internal static string LinqDataSource_AutoGenerateOrderByClause
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_AutoGenerateOrderByClause", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00027DB6 File Offset: 0x00025FB6
		internal static string LinqDataSource_AutoGenerateWhereClause
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_AutoGenerateWhereClause", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00027DCC File Offset: 0x00025FCC
		internal static string LinqDataSource_AutoPage
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_AutoPage", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00027DE2 File Offset: 0x00025FE2
		internal static string LinqDataSource_AutoSort
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_AutoSort", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00027DF8 File Offset: 0x00025FF8
		internal static string LinqDataSource_ContextCreated
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_ContextCreated", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00027E0E File Offset: 0x0002600E
		internal static string LinqDataSource_ContextCreating
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_ContextCreating", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00027E24 File Offset: 0x00026024
		internal static string LinqDataSource_ContextDisposing
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_ContextDisposing", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00027E3A File Offset: 0x0002603A
		internal static string LinqDataSource_ContextTypeName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_ContextTypeName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00027E50 File Offset: 0x00026050
		internal static string LinqDataSource_Deleted
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Deleted", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00027E66 File Offset: 0x00026066
		internal static string LinqDataSource_DeleteParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_DeleteParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00027E7C File Offset: 0x0002607C
		internal static string LinqDataSource_Deleting
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Deleting", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00027E92 File Offset: 0x00026092
		internal static string LinqDataSource_Description
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Description", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00027EA8 File Offset: 0x000260A8
		internal static string LinqDataSource_DisplayName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_DisplayName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00027EBE File Offset: 0x000260BE
		internal static string LinqDataSource_EnableDelete
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_EnableDelete", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00027ED4 File Offset: 0x000260D4
		internal static string LinqDataSource_EnableInsert
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_EnableInsert", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00027EEA File Offset: 0x000260EA
		internal static string LinqDataSource_EnableObjectTracking
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_EnableObjectTracking", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00027F00 File Offset: 0x00026100
		internal static string LinqDataSource_EnableUpdate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_EnableUpdate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00027F16 File Offset: 0x00026116
		internal static string LinqDataSource_GroupBy
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_GroupBy", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00027F2C File Offset: 0x0002612C
		internal static string LinqDataSource_GroupByParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_GroupByParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00027F42 File Offset: 0x00026142
		internal static string LinqDataSource_Inserted
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Inserted", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00027F58 File Offset: 0x00026158
		internal static string LinqDataSource_Inserting
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Inserting", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00027F6E File Offset: 0x0002616E
		internal static string LinqDataSource_InsertParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_InsertParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00027F84 File Offset: 0x00026184
		internal static string LinqDataSource_InvalidViewName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_InvalidViewName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00027F9A File Offset: 0x0002619A
		internal static string LinqDataSource_OrderBy
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_OrderBy", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00027FB0 File Offset: 0x000261B0
		internal static string LinqDataSource_OrderByParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_OrderByParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00027FC6 File Offset: 0x000261C6
		internal static string LinqDataSource_OrderGroupsBy
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_OrderGroupsBy", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00027FDC File Offset: 0x000261DC
		internal static string LinqDataSource_OrderGroupsByParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_OrderGroupsByParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00027FF2 File Offset: 0x000261F2
		internal static string LinqDataSource_Select
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Select", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00028008 File Offset: 0x00026208
		internal static string LinqDataSource_Selected
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Selected", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002801E File Offset: 0x0002621E
		internal static string LinqDataSource_Selecting
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Selecting", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00028034 File Offset: 0x00026234
		internal static string LinqDataSource_SelectParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_SelectParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002804A File Offset: 0x0002624A
		internal static string LinqDataSource_StoreOriginalValuesInViewState
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_StoreOriginalValuesInViewState", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00028060 File Offset: 0x00026260
		internal static string LinqDataSource_TableName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_TableName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00028076 File Offset: 0x00026276
		internal static string LinqDataSource_Updated
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Updated", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002808C File Offset: 0x0002628C
		internal static string LinqDataSource_UpdateParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_UpdateParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x000280A2 File Offset: 0x000262A2
		internal static string LinqDataSource_Updating
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Updating", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x000280B8 File Offset: 0x000262B8
		internal static string LinqDataSource_Where
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_Where", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x000280CE File Offset: 0x000262CE
		internal static string LinqDataSource_WhereParameters
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSource_WhereParameters", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x000280E4 File Offset: 0x000262E4
		internal static string LinqDataSourceValidationException_ValidationFailed
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceValidationException_ValidationFailed", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x000280FA File Offset: 0x000262FA
		internal static string LinqDataSourceView_CannotConvertType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_CannotConvertType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00028110 File Offset: 0x00026310
		internal static string LinqDataSourceView_ContextTypeNameChanged
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_ContextTypeNameChanged", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00028126 File Offset: 0x00026326
		internal static string LinqDataSourceView_ContextTypeNameNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_ContextTypeNameNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002813C File Offset: 0x0002633C
		internal static string LinqDataSourceView_ContextTypeNameNotSpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_ContextTypeNameNotSpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00028152 File Offset: 0x00026352
		internal static string LinqDataSourceView_DeleteNotSupported
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_DeleteNotSupported", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00028168 File Offset: 0x00026368
		internal static string LinqDataSourceView_EnableObjectTrackingChanged
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_EnableObjectTrackingChanged", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002817E File Offset: 0x0002637E
		internal static string LinqDataSourceView_GroupByNotSupportedOnEdit
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_GroupByNotSupportedOnEdit", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00028194 File Offset: 0x00026394
		internal static string LinqDataSourceView_InsertNotSupported
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_InsertNotSupported", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x000281AA File Offset: 0x000263AA
		internal static string LinqDataSourceView_InsertRequiresValues
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_InsertRequiresValues", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000281C0 File Offset: 0x000263C0
		internal static string LinqDataSourceView_InvalidContextType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_InvalidContextType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000281D6 File Offset: 0x000263D6
		internal static string LinqDataSourceView_InvalidOrderByFieldName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_InvalidOrderByFieldName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x000281EC File Offset: 0x000263EC
		internal static string LinqDataSourceView_InvalidParameterName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_InvalidParameterName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00028202 File Offset: 0x00026402
		internal static string LinqDataSourceView_InvalidTablePropertyType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_InvalidTablePropertyType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00028218 File Offset: 0x00026418
		internal static string LinqDataSourceView_OrderByAlreadySpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_OrderByAlreadySpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0002822E File Offset: 0x0002642E
		internal static string LinqDataSourceView_OrderGroupsByRequiresGroupBy
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_OrderGroupsByRequiresGroupBy", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00028244 File Offset: 0x00026444
		internal static string LinqDataSourceView_OriginalValuesNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_OriginalValuesNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002825A File Offset: 0x0002645A
		internal static string LinqDataSourceView_PagingNotHandled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_PagingNotHandled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00028270 File Offset: 0x00026470
		internal static string LinqDataSourceView_ParametersMustBeNamed
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_ParametersMustBeNamed", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00028286 File Offset: 0x00026486
		internal static string LinqDataSourceView_SelectNewNotSupportedOnEdit
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_SelectNewNotSupportedOnEdit", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0002829C File Offset: 0x0002649C
		internal static string LinqDataSourceView_TableCannotBeStatic
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_TableCannotBeStatic", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000282B2 File Offset: 0x000264B2
		internal static string LinqDataSourceView_TableNameChanged
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_TableNameChanged", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x000282C8 File Offset: 0x000264C8
		internal static string LinqDataSourceView_TableNameNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_TableNameNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x000282DE File Offset: 0x000264DE
		internal static string LinqDataSourceView_TableNameNotSpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_TableNameNotSpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x000282F4 File Offset: 0x000264F4
		internal static string LinqDataSourceView_UpdateNotSupported
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_UpdateNotSupported", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002830A File Offset: 0x0002650A
		internal static string LinqDataSourceView_ValidationFailed
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_ValidationFailed", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00028320 File Offset: 0x00026520
		internal static string LinqDataSourceView_WhereAlreadySpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("LinqDataSourceView_WhereAlreadySpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00028336 File Offset: 0x00026536
		internal static string ListView_AlternatingItemTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_AlternatingItemTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002834C File Offset: 0x0002654C
		internal static string ListView_ContainerNameMustNotBeEmpty
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_ContainerNameMustNotBeEmpty", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00028362 File Offset: 0x00026562
		internal static string ListView_ConvertEmptyStringToNull
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_ConvertEmptyStringToNull", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00028378 File Offset: 0x00026578
		internal static string ListView_DataKeyNames
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_DataKeyNames", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002838E File Offset: 0x0002658E
		internal static string ListView_DataKeyNamesMustBeSpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_DataKeyNamesMustBeSpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000283A4 File Offset: 0x000265A4
		internal static string ListView_DataKeys
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_DataKeys", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000283BA File Offset: 0x000265BA
		internal static string ListView_DataSourceDoesntSupportPaging
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_DataSourceDoesntSupportPaging", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000283D0 File Offset: 0x000265D0
		internal static string ListView_DataSourceMustBeCollectionWhenNotDataBinding
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_DataSourceMustBeCollectionWhenNotDataBinding", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x000283E6 File Offset: 0x000265E6
		internal static string ListView_EditIndex
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EditIndex", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x000283FC File Offset: 0x000265FC
		internal static string ListView_EditItem
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EditItem", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00028412 File Offset: 0x00026612
		internal static string ListView_EditItemTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EditItemTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00028428 File Offset: 0x00026628
		internal static string ListView_EmptyDataTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EmptyDataTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0002843E File Offset: 0x0002663E
		internal static string ListView_EmptyItemTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EmptyItemTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00028454 File Offset: 0x00026654
		internal static string ListView_EnableDataBoundControlManager
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EnableDataBoundControlManager", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0002846A File Offset: 0x0002666A
		internal static string ListView_EnableModelValidation
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EnableModelValidation", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00028480 File Offset: 0x00026680
		internal static string ListView_EnablePersistedSelection
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_EnablePersistedSelection", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00028496 File Offset: 0x00026696
		internal static string ListView_GroupContainerID
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_GroupContainerID", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x000284AC File Offset: 0x000266AC
		internal static string ListView_GroupItemCount
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_GroupItemCount", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x000284C2 File Offset: 0x000266C2
		internal static string ListView_GroupItemCountNoGroupTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_GroupItemCountNoGroupTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x000284D8 File Offset: 0x000266D8
		internal static string ListView_GroupSeparatorTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_GroupSeparatorTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x000284EE File Offset: 0x000266EE
		internal static string ListView_GroupTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_GroupTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00028504 File Offset: 0x00026704
		internal static string ListView_InsertItem
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InsertItem", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002851A File Offset: 0x0002671A
		internal static string ListView_InsertItemPosition
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InsertItemPosition", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00028530 File Offset: 0x00026730
		internal static string ListView_InsertItemTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InsertItemTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00028546 File Offset: 0x00026746
		internal static string ListView_InsertTemplateRequired
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InsertTemplateRequired", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0002855C File Offset: 0x0002675C
		internal static string ListView_InvalidCancel
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InvalidCancel", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00028572 File Offset: 0x00026772
		internal static string ListView_InvalidCommand
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InvalidCommand", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00028588 File Offset: 0x00026788
		internal static string ListView_InvalidDelete
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InvalidDelete", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0002859E File Offset: 0x0002679E
		internal static string ListView_InvalidEdit
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InvalidEdit", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000285B4 File Offset: 0x000267B4
		internal static string ListView_InvalidInsert
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InvalidInsert", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000285CA File Offset: 0x000267CA
		internal static string ListView_InvalidSelect
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InvalidSelect", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000285E0 File Offset: 0x000267E0
		internal static string ListView_InvalidUpdate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_InvalidUpdate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000285F6 File Offset: 0x000267F6
		internal static string ListView_ItemPlaceholderID
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_ItemPlaceholderID", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002860C File Offset: 0x0002680C
		internal static string ListView_Items
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_Items", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00028622 File Offset: 0x00026822
		internal static string ListView_ItemSeparatorTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_ItemSeparatorTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00028638 File Offset: 0x00026838
		internal static string ListView_ItemsNotDataItems
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_ItemsNotDataItems", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002864E File Offset: 0x0002684E
		internal static string ListView_ItemTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_ItemTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00028664 File Offset: 0x00026864
		internal static string ListView_ItemTemplateRequired
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_ItemTemplateRequired", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002867A File Offset: 0x0002687A
		internal static string ListView_LayoutTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_LayoutTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00028690 File Offset: 0x00026890
		internal static string ListView_Missing_VirtualItemCount
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_Missing_VirtualItemCount", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000286A6 File Offset: 0x000268A6
		internal static string ListView_NeedICollectionOrTotalRowCount
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_NeedICollectionOrTotalRowCount", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x000286BC File Offset: 0x000268BC
		internal static string ListView_NoGroupPlaceholder
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_NoGroupPlaceholder", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000286D2 File Offset: 0x000268D2
		internal static string ListView_NoInsertItem
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_NoInsertItem", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x000286E8 File Offset: 0x000268E8
		internal static string ListView_NoItemPlaceholder
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_NoItemPlaceholder", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x000286FE File Offset: 0x000268FE
		internal static string ListView_NullView
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_NullView", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00028714 File Offset: 0x00026914
		internal static string ListView_OnItemCanceling
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemCanceling", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002872A File Offset: 0x0002692A
		internal static string ListView_OnItemCommand
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemCommand", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00028740 File Offset: 0x00026940
		internal static string ListView_OnItemCreated
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemCreated", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00028756 File Offset: 0x00026956
		internal static string ListView_OnItemDataBound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemDataBound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002876C File Offset: 0x0002696C
		internal static string ListView_OnItemDeleted
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemDeleted", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00028782 File Offset: 0x00026982
		internal static string ListView_OnItemDeleting
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemDeleting", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00028798 File Offset: 0x00026998
		internal static string ListView_OnItemEditing
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemEditing", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000287AE File Offset: 0x000269AE
		internal static string ListView_OnItemInserted
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemInserted", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x000287C4 File Offset: 0x000269C4
		internal static string ListView_OnItemInserting
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemInserting", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x000287DA File Offset: 0x000269DA
		internal static string ListView_OnItemUpdated
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemUpdated", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000287F0 File Offset: 0x000269F0
		internal static string ListView_OnItemUpdating
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnItemUpdating", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00028806 File Offset: 0x00026A06
		internal static string ListView_OnLayoutCreated
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnLayoutCreated", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002881C File Offset: 0x00026A1C
		internal static string ListView_OnPagePropertiesChanged
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnPagePropertiesChanged", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00028832 File Offset: 0x00026A32
		internal static string ListView_OnPagePropertiesChanging
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnPagePropertiesChanging", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00028848 File Offset: 0x00026A48
		internal static string ListView_OnSelectedIndexChanged
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnSelectedIndexChanged", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002885E File Offset: 0x00026A5E
		internal static string ListView_OnSelectedIndexChanging
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnSelectedIndexChanging", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00028874 File Offset: 0x00026A74
		internal static string ListView_OnSorted
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnSorted", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002888A File Offset: 0x00026A8A
		internal static string ListView_OnSorting
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_OnSorting", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x000288A0 File Offset: 0x00026AA0
		internal static string ListView_PersistedSelectionRequiresDataKeysNames
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_PersistedSelectionRequiresDataKeysNames", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000288B6 File Offset: 0x00026AB6
		internal static string ListView_SelectedIndex
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_SelectedIndex", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x000288CC File Offset: 0x00026ACC
		internal static string ListView_SelectedItemTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_SelectedItemTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000288E2 File Offset: 0x00026AE2
		internal static string ListView_SortDirection
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_SortDirection", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x000288F8 File Offset: 0x00026AF8
		internal static string ListView_SortExpression
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_SortExpression", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002890E File Offset: 0x00026B0E
		internal static string ListView_StyleNotSupported
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_StyleNotSupported", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00028924 File Offset: 0x00026B24
		internal static string ListView_StylePropertiesNotSupported
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_StylePropertiesNotSupported", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002893A File Offset: 0x00026B3A
		internal static string ListView_UnhandledEvent
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListView_UnhandledEvent", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00028950 File Offset: 0x00026B50
		internal static string ListViewPagedDataSource_CannotGetCount
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListViewPagedDataSource_CannotGetCount", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00028966 File Offset: 0x00026B66
		internal static string ListViewPagedDataSource_EnumeratorMoveNextNotCalled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ListViewPagedDataSource_EnumeratorMoveNextNotCalled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002897C File Offset: 0x00026B7C
		internal static string MethodExpression_ChangingTheReturnTypeIsNotAllowed
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("MethodExpression_ChangingTheReturnTypeIsNotAllowed", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00028992 File Offset: 0x00026B92
		internal static string MethodExpression_DataSourceMustBeIDynamicDataSource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("MethodExpression_DataSourceMustBeIDynamicDataSource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x000289A8 File Offset: 0x00026BA8
		internal static string MethodExpression_FirstParamterMustBeCorrectType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("MethodExpression_FirstParamterMustBeCorrectType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x000289BE File Offset: 0x00026BBE
		internal static string MethodExpression_MethodMustBeStatic
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("MethodExpression_MethodMustBeStatic", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x000289D4 File Offset: 0x00026BD4
		internal static string MethodExpression_MethodNameMustBeSpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("MethodExpression_MethodNameMustBeSpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000289EA File Offset: 0x00026BEA
		internal static string MethodExpression_MethodNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("MethodExpression_MethodNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00028A00 File Offset: 0x00026C00
		internal static string MethodExpression_ParameterNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("MethodExpression_ParameterNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00028A16 File Offset: 0x00026C16
		internal static string NextPreviousPagerField_ButtonCssClass
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_ButtonCssClass", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00028A2C File Offset: 0x00026C2C
		internal static string NextPreviousPagerField_ButtonType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_ButtonType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00028A42 File Offset: 0x00026C42
		internal static string NextPreviousPagerField_FirstPageImageUrl
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_FirstPageImageUrl", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00028A58 File Offset: 0x00026C58
		internal static string NextPreviousPagerField_FirstPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_FirstPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00028A6E File Offset: 0x00026C6E
		internal static string NextPreviousPagerField_LastPageImageUrl
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_LastPageImageUrl", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00028A84 File Offset: 0x00026C84
		internal static string NextPreviousPagerField_LastPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_LastPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00028A9A File Offset: 0x00026C9A
		internal static string NextPreviousPagerField_NextPageImageUrl
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_NextPageImageUrl", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00028AB0 File Offset: 0x00026CB0
		internal static string NextPreviousPagerField_NextPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_NextPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00028AC6 File Offset: 0x00026CC6
		internal static string NextPreviousPagerField_PreviousPageImageUrl
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_PreviousPageImageUrl", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00028ADC File Offset: 0x00026CDC
		internal static string NextPreviousPagerField_PreviousPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_PreviousPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00028AF2 File Offset: 0x00026CF2
		internal static string NextPreviousPagerField_RenderDisabledButtonsAsLabels
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_RenderDisabledButtonsAsLabels", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00028B08 File Offset: 0x00026D08
		internal static string NextPreviousPagerField_RenderNonBreakingSpacesBetweenControls
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_RenderNonBreakingSpacesBetweenControls", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00028B1E File Offset: 0x00026D1E
		internal static string NextPreviousPagerField_ShowFirstPageButton
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_ShowFirstPageButton", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00028B34 File Offset: 0x00026D34
		internal static string NextPreviousPagerField_ShowLastPageButton
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_ShowLastPageButton", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00028B4A File Offset: 0x00026D4A
		internal static string NextPreviousPagerField_ShowNextPageButton
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_ShowNextPageButton", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00028B60 File Offset: 0x00026D60
		internal static string NextPreviousPagerField_ShowPreviousPageButton
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPreviousPagerField_ShowPreviousPageButton", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00028B76 File Offset: 0x00026D76
		internal static string NextPrevPagerField_DefaultFirstPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPrevPagerField_DefaultFirstPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00028B8C File Offset: 0x00026D8C
		internal static string NextPrevPagerField_DefaultLastPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPrevPagerField_DefaultLastPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00028BA2 File Offset: 0x00026DA2
		internal static string NextPrevPagerField_DefaultNextPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPrevPagerField_DefaultNextPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00028BB8 File Offset: 0x00026DB8
		internal static string NextPrevPagerField_DefaultPreviousPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NextPrevPagerField_DefaultPreviousPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00028BCE File Offset: 0x00026DCE
		internal static string NumericPagerField_ButtonCount
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_ButtonCount", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00028BE4 File Offset: 0x00026DE4
		internal static string NumericPagerField_ButtonType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_ButtonType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00028BFA File Offset: 0x00026DFA
		internal static string NumericPagerField_CurrentPageLabelCssClass
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_CurrentPageLabelCssClass", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00028C10 File Offset: 0x00026E10
		internal static string NumericPagerField_DefaultNextPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_DefaultNextPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00028C26 File Offset: 0x00026E26
		internal static string NumericPagerField_DefaultPreviousPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_DefaultPreviousPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00028C3C File Offset: 0x00026E3C
		internal static string NumericPagerField_NextPageImageUrl
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_NextPageImageUrl", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00028C52 File Offset: 0x00026E52
		internal static string NumericPagerField_NextPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_NextPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00028C68 File Offset: 0x00026E68
		internal static string NumericPagerField_NextPreviousButtonCssClass
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_NextPreviousButtonCssClass", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00028C7E File Offset: 0x00026E7E
		internal static string NumericPagerField_NumericButtonCssClass
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_NumericButtonCssClass", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00028C94 File Offset: 0x00026E94
		internal static string NumericPagerField_PreviousPageImageUrl
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_PreviousPageImageUrl", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00028CAA File Offset: 0x00026EAA
		internal static string NumericPagerField_PreviousPageText
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_PreviousPageText", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00028CC0 File Offset: 0x00026EC0
		internal static string NumericPagerField_RenderNonBreakingSpacesBetweenControls
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("NumericPagerField_RenderNonBreakingSpacesBetweenControls", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00028CD6 File Offset: 0x00026ED6
		internal static string OfTypeExpression_CannotFindType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("OfTypeExpression_CannotFindType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00028CEC File Offset: 0x00026EEC
		internal static string OfTypeExpression_TypeNameNotSpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("OfTypeExpression_TypeNameNotSpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00028D02 File Offset: 0x00026F02
		internal static string PageRequestManager_RegisterDataItemInNonAsyncRequest
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("PageRequestManager_RegisterDataItemInNonAsyncRequest", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00028D18 File Offset: 0x00026F18
		internal static string PageRequestManager_RegisterDataItemTwice
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("PageRequestManager_RegisterDataItemTwice", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00028D2E File Offset: 0x00026F2E
		internal static string PagerFieldCollection_InvalidType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("PagerFieldCollection_InvalidType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00028D44 File Offset: 0x00026F44
		internal static string PagerFieldCollection_InvalidTypeIndex
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("PagerFieldCollection_InvalidTypeIndex", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00028D5A File Offset: 0x00026F5A
		internal static string ParseException_ParseExceptionFormat
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ParseException_ParseExceptionFormat", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00028D70 File Offset: 0x00026F70
		internal static string ProfileServiceManager_LoadProperitesWithNonDefaultPath
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ProfileServiceManager_LoadProperitesWithNonDefaultPath", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00028D86 File Offset: 0x00026F86
		internal static string ProfileServiceManager_LoadProperties
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ProfileServiceManager_LoadProperties", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00028D9C File Offset: 0x00026F9C
		internal static string ProxyGenerator_UnsupportedType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ProxyGenerator_UnsupportedType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00028DB2 File Offset: 0x00026FB2
		internal static string ProxyHelper_BadStatusCode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ProxyHelper_BadStatusCode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00028DC8 File Offset: 0x00026FC8
		internal static string QueryExtender_DataSourceMustBeIQueryableDataSource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("QueryExtender_DataSourceMustBeIQueryableDataSource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00028DDE File Offset: 0x00026FDE
		internal static string QueryExtender_Expressions
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("QueryExtender_Expressions", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00028DF4 File Offset: 0x00026FF4
		internal static string RangeExpression_MaximumValueRequired
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("RangeExpression_MaximumValueRequired", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00028E0A File Offset: 0x0002700A
		internal static string RangeExpression_MinimumValueRequired
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("RangeExpression_MinimumValueRequired", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00028E20 File Offset: 0x00027020
		internal static string RangeExpression_RangeTypeMustBeSpecified
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("RangeExpression_RangeTypeMustBeSpecified", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00028E36 File Offset: 0x00027036
		internal static string RoleService_RoleProviderNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("RoleService_RoleProviderNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00028E4C File Offset: 0x0002704C
		internal static string RoleService_RolesFeatureNotEnabled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("RoleService_RolesFeatureNotEnabled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00028E62 File Offset: 0x00027062
		internal static string RoleServiceManager_LoadRoles
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("RoleServiceManager_LoadRoles", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00028E78 File Offset: 0x00027078
		internal static string RoleServiceManager_LoadRolesWithNonDefaultPath
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("RoleServiceManager_LoadRolesWithNonDefaultPath", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00028E8E File Offset: 0x0002708E
		internal static string ScriptControlDescriptor_IDNotSettable
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlDescriptor_IDNotSettable", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00028EA4 File Offset: 0x000270A4
		internal static string ScriptControlManager_ExtenderControlNotRegistered
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_ExtenderControlNotRegistered", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00028EBA File Offset: 0x000270BA
		internal static string ScriptControlManager_NoTargetControlTypes
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_NoTargetControlTypes", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00028ED0 File Offset: 0x000270D0
		internal static string ScriptControlManager_RegisterExtenderControlTooEarly
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_RegisterExtenderControlTooEarly", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00028EE6 File Offset: 0x000270E6
		internal static string ScriptControlManager_RegisterExtenderControlTooLate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_RegisterExtenderControlTooLate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00028EFC File Offset: 0x000270FC
		internal static string ScriptControlManager_RegisterScriptControlTooEarly
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_RegisterScriptControlTooEarly", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00028F12 File Offset: 0x00027112
		internal static string ScriptControlManager_RegisterScriptControlTooLate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_RegisterScriptControlTooLate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00028F28 File Offset: 0x00027128
		internal static string ScriptControlManager_ScriptControlNotRegistered
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_ScriptControlNotRegistered", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00028F3E File Offset: 0x0002713E
		internal static string ScriptControlManager_TargetControlTypeInvalid
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptControlManager_TargetControlTypeInvalid", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00028F54 File Offset: 0x00027154
		internal static string ScriptManager_AjaxFrameworkAssembly
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AjaxFrameworkAssembly", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00028F6A File Offset: 0x0002716A
		internal static string ScriptManager_AjaxFrameworkMode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AjaxFrameworkMode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00028F80 File Offset: 0x00027180
		internal static string ScriptManager_AllowCustomErrorsRedirect
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AllowCustomErrorsRedirect", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00028F96 File Offset: 0x00027196
		internal static string ScriptManager_AsyncPostBackError
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AsyncPostBackError", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00028FAC File Offset: 0x000271AC
		internal static string ScriptManager_AsyncPostBackErrorMessage
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AsyncPostBackErrorMessage", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00028FC2 File Offset: 0x000271C2
		internal static string ScriptManager_AsyncPostBackNotInPartialRenderingMode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AsyncPostBackNotInPartialRenderingMode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00028FD8 File Offset: 0x000271D8
		internal static string ScriptManager_AsyncPostBackTimeout
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AsyncPostBackTimeout", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00028FEE File Offset: 0x000271EE
		internal static string ScriptManager_AuthenticationService
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_AuthenticationService", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00029004 File Offset: 0x00027204
		internal static string ScriptManager_CannotAddHistoryPointOutsideOfAsyncPostBack
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotAddHistoryPointOutsideOfAsyncPostBack", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0002901A File Offset: 0x0002721A
		internal static string ScriptManager_CannotAddHistoryPointWithHistoryDisabled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotAddHistoryPointWithHistoryDisabled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00029030 File Offset: 0x00027230
		internal static string ScriptManager_CannotChangeAjaxFrameworkMode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotChangeAjaxFrameworkMode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00029046 File Offset: 0x00027246
		internal static string ScriptManager_CannotChangeEnableCdn
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotChangeEnableCdn", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002905C File Offset: 0x0002725C
		internal static string ScriptManager_CannotChangeEnableCdnFallback
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotChangeEnableCdnFallback", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00029072 File Offset: 0x00027272
		internal static string ScriptManager_CannotChangeEnableHistory
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotChangeEnableHistory", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00029088 File Offset: 0x00027288
		internal static string ScriptManager_CannotChangeEnablePartialRendering
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotChangeEnablePartialRendering", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002909E File Offset: 0x0002729E
		internal static string ScriptManager_CannotChangeEnableScriptGlobalization
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotChangeEnableScriptGlobalization", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000290B4 File Offset: 0x000272B4
		internal static string ScriptManager_CannotChangeSupportsPartialRendering
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotChangeSupportsPartialRendering", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x000290CA File Offset: 0x000272CA
		internal static string ScriptManager_CannotRegisterBothPostBacks
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotRegisterBothPostBacks", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000290E0 File Offset: 0x000272E0
		internal static string ScriptManager_CannotRegisterScriptInMultipleCompositeReferences
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotRegisterScriptInMultipleCompositeReferences", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x000290F6 File Offset: 0x000272F6
		internal static string ScriptManager_CannotSetSupportsPartialRenderingWhenDisabled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CannotSetSupportsPartialRenderingWhenDisabled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0002910C File Offset: 0x0002730C
		internal static string ScriptManager_ClientNavigateHandler
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_ClientNavigateHandler", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00029122 File Offset: 0x00027322
		internal static string ScriptManager_CompositeScript
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_CompositeScript", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00029138 File Offset: 0x00027338
		internal static string ScriptManager_EmptyPageUrl
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EmptyPageUrl", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002914E File Offset: 0x0002734E
		internal static string ScriptManager_EnableCdn
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnableCdn", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00029164 File Offset: 0x00027364
		internal static string ScriptManager_EnableCdnFallback
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnableCdnFallback", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002917A File Offset: 0x0002737A
		internal static string ScriptManager_EnableHistory
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnableHistory", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00029190 File Offset: 0x00027390
		internal static string ScriptManager_EnablePageMethods
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnablePageMethods", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000291A6 File Offset: 0x000273A6
		internal static string ScriptManager_EnablePartialRendering
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnablePartialRendering", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000291BC File Offset: 0x000273BC
		internal static string ScriptManager_EnableScriptGlobalization
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnableScriptGlobalization", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000291D2 File Offset: 0x000273D2
		internal static string ScriptManager_EnableScriptLocalization
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnableScriptLocalization", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000291E8 File Offset: 0x000273E8
		internal static string ScriptManager_EnableSecureHistoryState
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_EnableSecureHistoryState", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x000291FE File Offset: 0x000273FE
		internal static string ScriptManager_FrameworkFailedToLoad
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_FrameworkFailedToLoad", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00029214 File Offset: 0x00027414
		internal static string ScriptManager_InvalidControlRegistration
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_InvalidControlRegistration", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0002922A File Offset: 0x0002742A
		internal static string ScriptManager_LoadScriptsBeforeUI
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_LoadScriptsBeforeUI", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00029240 File Offset: 0x00027440
		internal static string ScriptManager_MustHaveGreaterVersion
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_MustHaveGreaterVersion", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00029256 File Offset: 0x00027456
		internal static string ScriptManager_Navigate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_Navigate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002926C File Offset: 0x0002746C
		internal static string ScriptManager_OnlyOneScriptManager
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_OnlyOneScriptManager", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00029282 File Offset: 0x00027482
		internal static string ScriptManager_PageUntitled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_PageUntitled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00029298 File Offset: 0x00027498
		internal static string ScriptManager_ProfileService
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_ProfileService", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x000292AE File Offset: 0x000274AE
		internal static string ScriptManager_ResolveCompositeScriptReference
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_ResolveCompositeScriptReference", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x000292C4 File Offset: 0x000274C4
		internal static string ScriptManager_ResolveScriptReference
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_ResolveScriptReference", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x000292DA File Offset: 0x000274DA
		internal static string ScriptManager_RoleService
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_RoleService", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x000292F0 File Offset: 0x000274F0
		internal static string ScriptManager_ScriptMode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_ScriptMode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00029306 File Offset: 0x00027506
		internal static string ScriptManager_ScriptPath
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_ScriptPath", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002931C File Offset: 0x0002751C
		internal static string ScriptManager_Scripts
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_Scripts", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00029332 File Offset: 0x00027532
		internal static string ScriptManager_Services
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_Services", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00029348 File Offset: 0x00027548
		internal static string ScriptManager_UpdatePanelNotRegistered
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptManager_UpdatePanelNotRegistered", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0002935E File Offset: 0x0002755E
		internal static string ScriptReference_Assembly
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_Assembly", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00029374 File Offset: 0x00027574
		internal static string ScriptReference_AssemblyRequiresName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_AssemblyRequiresName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0002938A File Offset: 0x0002758A
		internal static string ScriptReference_IgnoreScriptPath
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_IgnoreScriptPath", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000293A0 File Offset: 0x000275A0
		internal static string ScriptReference_InvalidReleaseScriptName
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_InvalidReleaseScriptName", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000293B6 File Offset: 0x000275B6
		internal static string ScriptReference_InvalidReleaseScriptPath
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_InvalidReleaseScriptPath", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x000293CC File Offset: 0x000275CC
		internal static string ScriptReference_Name
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_Name", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000293E2 File Offset: 0x000275E2
		internal static string ScriptReference_NameAndPathCannotBeEmpty
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_NameAndPathCannotBeEmpty", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x000293F8 File Offset: 0x000275F8
		internal static string ScriptReference_NotifyScriptLoaded
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_NotifyScriptLoaded", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002940E File Offset: 0x0002760E
		internal static string ScriptReference_Path
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_Path", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00029424 File Offset: 0x00027624
		internal static string ScriptReference_ResourceRequiresAjaxAssembly
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_ResourceRequiresAjaxAssembly", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002943A File Offset: 0x0002763A
		internal static string ScriptReference_ResourceUICultures
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_ResourceUICultures", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00029450 File Offset: 0x00027650
		internal static string ScriptReference_ScriptMode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptReference_ScriptMode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00029466 File Offset: 0x00027666
		internal static string ScriptRegistrationManager_ControlNotOnPage
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptRegistrationManager_ControlNotOnPage", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002947C File Offset: 0x0002767C
		internal static string ScriptRegistrationManager_InvalidChars
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptRegistrationManager_InvalidChars", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00029492 File Offset: 0x00027692
		internal static string ScriptRegistrationManager_NoCloseTag
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptRegistrationManager_NoCloseTag", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000294A8 File Offset: 0x000276A8
		internal static string ScriptRegistrationManager_NoTags
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptRegistrationManager_NoTags", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x000294BE File Offset: 0x000276BE
		internal static string ScriptResourceDefinition_InvalidPath
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptResourceDefinition_InvalidPath", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x000294D4 File Offset: 0x000276D4
		internal static string ScriptResourceDefinition_NameAndPathCannotBeEmpty
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptResourceDefinition_NameAndPathCannotBeEmpty", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000294EA File Offset: 0x000276EA
		internal static string ScriptResourceHandler_DuplicateScriptResources
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptResourceHandler_DuplicateScriptResources", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00029500 File Offset: 0x00027700
		internal static string ScriptResourceHandler_InvalidRequest
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptResourceHandler_InvalidRequest", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00029516 File Offset: 0x00027716
		internal static string ScriptResourceHandler_ResourceUrlTooLong
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptResourceHandler_ResourceUrlTooLong", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002952C File Offset: 0x0002772C
		internal static string ScriptResourceHandler_TypeNameMismatch
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptResourceHandler_TypeNameMismatch", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00029542 File Offset: 0x00027742
		internal static string ScriptResourceHandler_UnknownResource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ScriptResourceHandler_UnknownResource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00029558 File Offset: 0x00027758
		internal static string SearchExpression_ParameterRequired
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("SearchExpression_ParameterRequired", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0002956E File Offset: 0x0002776E
		internal static string ServiceReference_InlineScript
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ServiceReference_InlineScript", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00029584 File Offset: 0x00027784
		internal static string ServiceReference_Path
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ServiceReference_Path", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002959A File Offset: 0x0002779A
		internal static string ServiceReference_PathCannotBeEmpty
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ServiceReference_PathCannotBeEmpty", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x000295B0 File Offset: 0x000277B0
		internal static string ServiceUriNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("ServiceUriNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000295C6 File Offset: 0x000277C6
		internal static string SqlHelper_SqlEverywhereNotInstalled
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("SqlHelper_SqlEverywhereNotInstalled", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x000295DC File Offset: 0x000277DC
		internal static string TemplatePagerField_OnPagerCommand
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("TemplatePagerField_OnPagerCommand", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000295F2 File Offset: 0x000277F2
		internal static string TemplatePagerField_PagerTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("TemplatePagerField_PagerTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00029608 File Offset: 0x00027808
		internal static string TemplatePagerField_UnhandledEvent
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("TemplatePagerField_UnhandledEvent", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002961E File Offset: 0x0002781E
		internal static string Timer_IntervalMustBeGreaterThanZero
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Timer_IntervalMustBeGreaterThanZero", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00029634 File Offset: 0x00027834
		internal static string Timer_TimerEnable
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Timer_TimerEnable", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002964A File Offset: 0x0002784A
		internal static string Timer_TimerInterval
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Timer_TimerInterval", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00029660 File Offset: 0x00027860
		internal static string Timer_TimerTick
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("Timer_TimerTick", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00029676 File Offset: 0x00027876
		internal static string UnhandledExceptionEventLogMessage
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UnhandledExceptionEventLogMessage", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002968C File Offset: 0x0002788C
		internal static string UpdatePanel_CannotModifyControlCollection
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_CannotModifyControlCollection", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000296A2 File Offset: 0x000278A2
		internal static string UpdatePanel_CannotSetContentTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_CannotSetContentTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x000296B8 File Offset: 0x000278B8
		internal static string UpdatePanel_ChildrenAsTriggers
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_ChildrenAsTriggers", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000296CE File Offset: 0x000278CE
		internal static string UpdatePanel_ChildrenTriggersAndUpdateAlways
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_ChildrenTriggersAndUpdateAlways", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x000296E4 File Offset: 0x000278E4
		internal static string UpdatePanel_RenderMode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_RenderMode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000296FA File Offset: 0x000278FA
		internal static string UpdatePanel_SetPartialRenderingModeCalledOnce
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_SetPartialRenderingModeCalledOnce", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00029710 File Offset: 0x00027910
		internal static string UpdatePanel_Triggers
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_Triggers", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00029726 File Offset: 0x00027926
		internal static string UpdatePanel_UpdateConditional
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_UpdateConditional", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002973C File Offset: 0x0002793C
		internal static string UpdatePanel_UpdateMode
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_UpdateMode", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00029752 File Offset: 0x00027952
		internal static string UpdatePanel_UpdateTooLate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanel_UpdateTooLate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00029768 File Offset: 0x00027968
		internal static string UpdatePanelControlTrigger_ControlID
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanelControlTrigger_ControlID", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0002977E File Offset: 0x0002797E
		internal static string UpdatePanelControlTrigger_ControlNotFound
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanelControlTrigger_ControlNotFound", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00029794 File Offset: 0x00027994
		internal static string UpdatePanelControlTrigger_NoControlID
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdatePanelControlTrigger_NoControlID", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000297AA File Offset: 0x000279AA
		internal static string UpdateProgress_AssociatedUpdatePanelID
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdateProgress_AssociatedUpdatePanelID", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x000297C0 File Offset: 0x000279C0
		internal static string UpdateProgress_DisplayAfter
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdateProgress_DisplayAfter", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000297D6 File Offset: 0x000279D6
		internal static string UpdateProgress_DisplayAfterInvalid
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdateProgress_DisplayAfterInvalid", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x000297EC File Offset: 0x000279EC
		internal static string UpdateProgress_DynamicLayout
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdateProgress_DynamicLayout", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00029802 File Offset: 0x00027A02
		internal static string UpdateProgress_NoUpdatePanel
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdateProgress_NoUpdatePanel", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00029818 File Offset: 0x00027A18
		internal static string UpdateProgress_ProgressTemplate
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UpdateProgress_ProgressTemplate", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002982E File Offset: 0x00027A2E
		internal static string UserIsNotAuthenticated
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("UserIsNotAuthenticated", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00029844 File Offset: 0x00027A44
		internal static string WebResourceUtil_AssemblyDoesNotContainDebugWebResource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebResourceUtil_AssemblyDoesNotContainDebugWebResource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002985A File Offset: 0x00027A5A
		internal static string WebResourceUtil_AssemblyDoesNotContainEmbeddedResource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebResourceUtil_AssemblyDoesNotContainEmbeddedResource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00029870 File Offset: 0x00027A70
		internal static string WebResourceUtil_AssemblyDoesNotContainReleaseWebResource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebResourceUtil_AssemblyDoesNotContainReleaseWebResource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00029886 File Offset: 0x00027A86
		internal static string WebResourceUtil_SystemWebExtensionsDoesNotContainReleaseWebResource
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebResourceUtil_SystemWebExtensionsDoesNotContainReleaseWebResource", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002989C File Offset: 0x00027A9C
		internal static string WebService_Error
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_Error", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x000298B2 File Offset: 0x00027AB2
		internal static string WebService_InvalidGenerateScriptType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_InvalidGenerateScriptType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x000298C8 File Offset: 0x00027AC8
		internal static string WebService_InvalidInlineVirtualPath
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_InvalidInlineVirtualPath", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000298DE File Offset: 0x00027ADE
		internal static string WebService_InvalidVerbRequest
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_InvalidVerbRequest", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000298F4 File Offset: 0x00027AF4
		internal static string WebService_InvalidWebServiceCall
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_InvalidWebServiceCall", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0002990A File Offset: 0x00027B0A
		internal static string WebService_InvalidXmlReturnType
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_InvalidXmlReturnType", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00029920 File Offset: 0x00027B20
		internal static string WebService_MissingArg
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_MissingArg", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00029936 File Offset: 0x00027B36
		internal static string WebService_NoScriptServiceAttribute
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_NoScriptServiceAttribute", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0002994C File Offset: 0x00027B4C
		internal static string WebService_NoWebServiceData
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_NoWebServiceData", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00029962 File Offset: 0x00027B62
		internal static string WebService_NoWebServiceDataInlineScript
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_NoWebServiceDataInlineScript", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00029978 File Offset: 0x00027B78
		internal static string WebService_RedirectError
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_RedirectError", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0002998E File Offset: 0x00027B8E
		internal static string WebService_UnknownWebMethod
		{
			get
			{
				return AtlasWeb.ResourceManager.GetString("WebService_UnknownWebMethod", AtlasWeb.resourceCulture);
			}
		}

		// Token: 0x04000366 RID: 870
		private static ResourceManager resourceMan;

		// Token: 0x04000367 RID: 871
		private static CultureInfo resourceCulture;
	}
}
