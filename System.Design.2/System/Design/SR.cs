using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Design
{
	// Token: 0x02000286 RID: 646
	internal sealed class SR
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x0008B12E File Offset: 0x0008932E
		internal SR()
		{
			this.resources = new ResourceManager("System.Design", base.GetType().Assembly);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0008B154 File Offset: 0x00089354
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR value = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, value, null);
			}
			return SR.loader;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00003598 File Offset: 0x00001798
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0008B180 File Offset: 0x00089380
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0008B18C File Offset: 0x0008938C
		public static string GetString(string name, params object[] args)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			string @string = sr.resources.GetString(name, SR.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0008B20C File Offset: 0x0008940C
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0008B235 File Offset: 0x00089435
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0008B240 File Offset: 0x00089440
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x04000E14 RID: 3604
		internal const string VerbEditorDefault = "VerbEditorDefault";

		// Token: 0x04000E15 RID: 3605
		internal const string WorkingDirectoryEditorLabel = "WorkingDirectoryEditorLabel";

		// Token: 0x04000E16 RID: 3606
		internal const string FSWPathEditorLabel = "FSWPathEditorLabel";

		// Token: 0x04000E17 RID: 3607
		internal const string BinaryEditorFileError = "BinaryEditorFileError";

		// Token: 0x04000E18 RID: 3608
		internal const string BinaryEditorTitle = "BinaryEditorTitle";

		// Token: 0x04000E19 RID: 3609
		internal const string BinaryEditorAllFiles = "BinaryEditorAllFiles";

		// Token: 0x04000E1A RID: 3610
		internal const string BinaryEditorSaveFile = "BinaryEditorSaveFile";

		// Token: 0x04000E1B RID: 3611
		internal const string BinaryEditorFileName = "BinaryEditorFileName";

		// Token: 0x04000E1C RID: 3612
		internal const string AnchorEditorAccName = "AnchorEditorAccName";

		// Token: 0x04000E1D RID: 3613
		internal const string AnchorEditorRightAccName = "AnchorEditorRightAccName";

		// Token: 0x04000E1E RID: 3614
		internal const string AnchorEditorLeftAccName = "AnchorEditorLeftAccName";

		// Token: 0x04000E1F RID: 3615
		internal const string AnchorEditorTopAccName = "AnchorEditorTopAccName";

		// Token: 0x04000E20 RID: 3616
		internal const string AnchorEditorBottomAccName = "AnchorEditorBottomAccName";

		// Token: 0x04000E21 RID: 3617
		internal const string CollectionEditorCaption = "CollectionEditorCaption";

		// Token: 0x04000E22 RID: 3618
		internal const string CollectionEditorProperties = "CollectionEditorProperties";

		// Token: 0x04000E23 RID: 3619
		internal const string CollectionEditorPropertiesMultiSelect = "CollectionEditorPropertiesMultiSelect";

		// Token: 0x04000E24 RID: 3620
		internal const string CollectionEditorPropertiesNone = "CollectionEditorPropertiesNone";

		// Token: 0x04000E25 RID: 3621
		internal const string CollectionEditorCantRemoveItem = "CollectionEditorCantRemoveItem";

		// Token: 0x04000E26 RID: 3622
		internal const string CollectionEditorUndoBatchDesc = "CollectionEditorUndoBatchDesc";

		// Token: 0x04000E27 RID: 3623
		internal const string CollectionEditorInheritedReadOnlySelection = "CollectionEditorInheritedReadOnlySelection";

		// Token: 0x04000E28 RID: 3624
		internal const string DockEditorAccName = "DockEditorAccName";

		// Token: 0x04000E29 RID: 3625
		internal const string DockEditorNoneAccName = "DockEditorNoneAccName";

		// Token: 0x04000E2A RID: 3626
		internal const string DockEditorRightAccName = "DockEditorRightAccName";

		// Token: 0x04000E2B RID: 3627
		internal const string DockEditorLeftAccName = "DockEditorLeftAccName";

		// Token: 0x04000E2C RID: 3628
		internal const string DockEditorTopAccName = "DockEditorTopAccName";

		// Token: 0x04000E2D RID: 3629
		internal const string DockEditorBottomAccName = "DockEditorBottomAccName";

		// Token: 0x04000E2E RID: 3630
		internal const string DockEditorFillAccName = "DockEditorFillAccName";

		// Token: 0x04000E2F RID: 3631
		internal const string DesignSurfaceNoRootComponent = "DesignSurfaceNoRootComponent";

		// Token: 0x04000E30 RID: 3632
		internal const string DesignSurfaceServiceIsFixed = "DesignSurfaceServiceIsFixed";

		// Token: 0x04000E31 RID: 3633
		internal const string DesignSurfaceFatalError = "DesignSurfaceFatalError";

		// Token: 0x04000E32 RID: 3634
		internal const string DesignSurfaceContainerDispose = "DesignSurfaceContainerDispose";

		// Token: 0x04000E33 RID: 3635
		internal const string DesignSurfaceDesignerNotLoaded = "DesignSurfaceDesignerNotLoaded";

		// Token: 0x04000E34 RID: 3636
		internal const string DesignSurfaceNoSupportedTechnology = "DesignSurfaceNoSupportedTechnology";

		// Token: 0x04000E35 RID: 3637
		internal const string DesignerHostUnloading = "DesignerHostUnloading";

		// Token: 0x04000E36 RID: 3638
		internal const string DesignerHostCyclicAdd = "DesignerHostCyclicAdd";

		// Token: 0x04000E37 RID: 3639
		internal const string DesignerHostNoTopLevelDesigner = "DesignerHostNoTopLevelDesigner";

		// Token: 0x04000E38 RID: 3640
		internal const string DesignerHostDuplicateName = "DesignerHostDuplicateName";

		// Token: 0x04000E39 RID: 3641
		internal const string DesignerHostFailedComponentCreate = "DesignerHostFailedComponentCreate";

		// Token: 0x04000E3A RID: 3642
		internal const string DesignerHostCantDestroyInheritedComponent = "DesignerHostCantDestroyInheritedComponent";

		// Token: 0x04000E3B RID: 3643
		internal const string DesignerHostDestroyComponentTransaction = "DesignerHostDestroyComponentTransaction";

		// Token: 0x04000E3C RID: 3644
		internal const string DesignerHostNoBaseClass = "DesignerHostNoBaseClass";

		// Token: 0x04000E3D RID: 3645
		internal const string DesignerHostLoaderSpecified = "DesignerHostLoaderSpecified";

		// Token: 0x04000E3E RID: 3646
		internal const string DesignerHostNestedTransaction = "DesignerHostNestedTransaction";

		// Token: 0x04000E3F RID: 3647
		internal const string DesignerHostGenericTransactionName = "DesignerHostGenericTransactionName";

		// Token: 0x04000E40 RID: 3648
		internal const string DesignerHostDesignerNeedsComponent = "DesignerHostDesignerNeedsComponent";

		// Token: 0x04000E41 RID: 3649
		internal const string DesignerOptionsMissingServiceContainer = "DesignerOptionsMissingServiceContainer";

		// Token: 0x04000E42 RID: 3650
		internal const string DesignerOptionsExistingOptionsService = "DesignerOptionsExistingOptionsService";

		// Token: 0x04000E43 RID: 3651
		internal const string DesignerOptionsUnableToCreateOptionService = "DesignerOptionsUnableToCreateOptionService";

		// Token: 0x04000E44 RID: 3652
		internal const string BasicDesignerLoaderAlreadyLoaded = "BasicDesignerLoaderAlreadyLoaded";

		// Token: 0x04000E45 RID: 3653
		internal const string BasicDesignerLoaderDifferentHost = "BasicDesignerLoaderDifferentHost";

		// Token: 0x04000E46 RID: 3654
		internal const string BasicDesignerLoaderMissingService = "BasicDesignerLoaderMissingService";

		// Token: 0x04000E47 RID: 3655
		internal const string BasicDesignerLoaderNotInitialized = "BasicDesignerLoaderNotInitialized";

		// Token: 0x04000E48 RID: 3656
		internal const string CodeDomDesignerLoaderNoLanguageSupport = "CodeDomDesignerLoaderNoLanguageSupport";

		// Token: 0x04000E49 RID: 3657
		internal const string CodeDomDesignerLoaderDocumentFailureTypeNotFound = "CodeDomDesignerLoaderDocumentFailureTypeNotFound";

		// Token: 0x04000E4A RID: 3658
		internal const string CodeDomDesignerLoaderDocumentFailureTypeNotDesignable = "CodeDomDesignerLoaderDocumentFailureTypeNotDesignable";

		// Token: 0x04000E4B RID: 3659
		internal const string CodeDomDesignerLoaderDocumentFailureTypeDesignerNotInstalled = "CodeDomDesignerLoaderDocumentFailureTypeDesignerNotInstalled";

		// Token: 0x04000E4C RID: 3660
		internal const string CodeDomDesignerLoaderNoRootSerializer = "CodeDomDesignerLoaderNoRootSerializer";

		// Token: 0x04000E4D RID: 3661
		internal const string CodeDomDesignerLoaderNoRootSerializerWithFailures = "CodeDomDesignerLoaderNoRootSerializerWithFailures";

		// Token: 0x04000E4E RID: 3662
		internal const string CodeDomDesignerLoaderInvalidIdentifier = "CodeDomDesignerLoaderInvalidIdentifier";

		// Token: 0x04000E4F RID: 3663
		internal const string CodeDomDesignerLoaderInvalidBlankIdentifier = "CodeDomDesignerLoaderInvalidBlankIdentifier";

		// Token: 0x04000E50 RID: 3664
		internal const string CodeDomDesignerLoaderDupComponentName = "CodeDomDesignerLoaderDupComponentName";

		// Token: 0x04000E51 RID: 3665
		internal const string CodeDomDesignerLoaderBadSerializationObject = "CodeDomDesignerLoaderBadSerializationObject";

		// Token: 0x04000E52 RID: 3666
		internal const string CodeDomDesignerLoaderPropModifiers = "CodeDomDesignerLoaderPropModifiers";

		// Token: 0x04000E53 RID: 3667
		internal const string CodeDomDesignerLoaderPropGenerateMember = "CodeDomDesignerLoaderPropGenerateMember";

		// Token: 0x04000E54 RID: 3668
		internal const string CodeDomDesignerLoaderNoTypeResolution = "CodeDomDesignerLoaderNoTypeResolution";

		// Token: 0x04000E55 RID: 3669
		internal const string CodeDomDesignerLoaderSerializerTypeNotFirstType = "CodeDomDesignerLoaderSerializerTypeNotFirstType";

		// Token: 0x04000E56 RID: 3670
		internal const string CodeDomComponentSerializationServiceUnknownStore = "CodeDomComponentSerializationServiceUnknownStore";

		// Token: 0x04000E57 RID: 3671
		internal const string CodeDomComponentSerializationServiceClosedStore = "CodeDomComponentSerializationServiceClosedStore";

		// Token: 0x04000E58 RID: 3672
		internal const string CodeDomComponentSerializationServiceDeserializationError = "CodeDomComponentSerializationServiceDeserializationError";

		// Token: 0x04000E59 RID: 3673
		internal const string DesignerActionPanel_CouldNotFindProperty = "DesignerActionPanel_CouldNotFindProperty";

		// Token: 0x04000E5A RID: 3674
		internal const string DesignerActionPanel_CouldNotFindMethod = "DesignerActionPanel_CouldNotFindMethod";

		// Token: 0x04000E5B RID: 3675
		internal const string DesignerActionPanel_CouldNotConvertValue = "DesignerActionPanel_CouldNotConvertValue";

		// Token: 0x04000E5C RID: 3676
		internal const string DesignerActionPanel_ErrorActivatingDropDown = "DesignerActionPanel_ErrorActivatingDropDown";

		// Token: 0x04000E5D RID: 3677
		internal const string DesignerActionPanel_ErrorSettingValue = "DesignerActionPanel_ErrorSettingValue";

		// Token: 0x04000E5E RID: 3678
		internal const string DesignerActionPanel_ErrorInvokingAction = "DesignerActionPanel_ErrorInvokingAction";

		// Token: 0x04000E5F RID: 3679
		internal const string DesignerActionPanel_DefaultPanelTitle = "DesignerActionPanel_DefaultPanelTitle";

		// Token: 0x04000E60 RID: 3680
		internal const string ExtenderProviderServiceDuplicateProvider = "ExtenderProviderServiceDuplicateProvider";

		// Token: 0x04000E61 RID: 3681
		internal const string EventBindingServiceMissingService = "EventBindingServiceMissingService";

		// Token: 0x04000E62 RID: 3682
		internal const string EventBindingServiceEventReadOnly = "EventBindingServiceEventReadOnly";

		// Token: 0x04000E63 RID: 3683
		internal const string EventBindingServiceBadArgType = "EventBindingServiceBadArgType";

		// Token: 0x04000E64 RID: 3684
		internal const string EventBindingServiceNoSite = "EventBindingServiceNoSite";

		// Token: 0x04000E65 RID: 3685
		internal const string EventBindingServiceSetValue = "EventBindingServiceSetValue";

		// Token: 0x04000E66 RID: 3686
		internal const string SerializationManagerDuplicateComponentDecl = "SerializationManagerDuplicateComponentDecl";

		// Token: 0x04000E67 RID: 3687
		internal const string SerializationManagerNoMatchingCtor = "SerializationManagerNoMatchingCtor";

		// Token: 0x04000E68 RID: 3688
		internal const string SerializationManagerNameInUse = "SerializationManagerNameInUse";

		// Token: 0x04000E69 RID: 3689
		internal const string SerializationManagerObjectHasName = "SerializationManagerObjectHasName";

		// Token: 0x04000E6A RID: 3690
		internal const string SerializationManagerAreadyInSession = "SerializationManagerAreadyInSession";

		// Token: 0x04000E6B RID: 3691
		internal const string SerializationManagerNoSession = "SerializationManagerNoSession";

		// Token: 0x04000E6C RID: 3692
		internal const string SerializationManagerWithinSession = "SerializationManagerWithinSession";

		// Token: 0x04000E6D RID: 3693
		internal const string UndoEngineMissingService = "UndoEngineMissingService";

		// Token: 0x04000E6E RID: 3694
		internal const string UndoEngineComponentChange0 = "UndoEngineComponentChange0";

		// Token: 0x04000E6F RID: 3695
		internal const string UndoEngineComponentChange1 = "UndoEngineComponentChange1";

		// Token: 0x04000E70 RID: 3696
		internal const string UndoEngineComponentChange2 = "UndoEngineComponentChange2";

		// Token: 0x04000E71 RID: 3697
		internal const string UndoEngineComponentAdd0 = "UndoEngineComponentAdd0";

		// Token: 0x04000E72 RID: 3698
		internal const string UndoEngineComponentAdd1 = "UndoEngineComponentAdd1";

		// Token: 0x04000E73 RID: 3699
		internal const string UndoEngineComponentRemove0 = "UndoEngineComponentRemove0";

		// Token: 0x04000E74 RID: 3700
		internal const string UndoEngineComponentRemove1 = "UndoEngineComponentRemove1";

		// Token: 0x04000E75 RID: 3701
		internal const string UndoEngineComponentRename = "UndoEngineComponentRename";

		// Token: 0x04000E76 RID: 3702
		internal const string BehaviorServiceResizeControl = "BehaviorServiceResizeControl";

		// Token: 0x04000E77 RID: 3703
		internal const string BehaviorServiceResizeControls = "BehaviorServiceResizeControls";

		// Token: 0x04000E78 RID: 3704
		internal const string BehaviorServiceMoveControl = "BehaviorServiceMoveControl";

		// Token: 0x04000E79 RID: 3705
		internal const string BehaviorServiceMoveControls = "BehaviorServiceMoveControls";

		// Token: 0x04000E7A RID: 3706
		internal const string BehaviorServiceCopyControl = "BehaviorServiceCopyControl";

		// Token: 0x04000E7B RID: 3707
		internal const string BehaviorServiceCopyControls = "BehaviorServiceCopyControls";

		// Token: 0x04000E7C RID: 3708
		internal const string MultilineStringEditorWatermark = "MultilineStringEditorWatermark";

		// Token: 0x04000E7D RID: 3709
		internal const string ComponentDesignerAddEvent = "ComponentDesignerAddEvent";

		// Token: 0x04000E7E RID: 3710
		internal const string LocalizerManualReload = "LocalizerManualReload";

		// Token: 0x04000E7F RID: 3711
		internal const string LocalizingCannotAdd = "LocalizingCannotAdd";

		// Token: 0x04000E80 RID: 3712
		internal const string LocalizeDesigner_RegionWatermark = "LocalizeDesigner_RegionWatermark";

		// Token: 0x04000E81 RID: 3713
		internal const string LocalizationProviderLocalizableDescr = "LocalizationProviderLocalizableDescr";

		// Token: 0x04000E82 RID: 3714
		internal const string LocalizationProviderLanguageDescr = "LocalizationProviderLanguageDescr";

		// Token: 0x04000E83 RID: 3715
		internal const string LocalizationProviderManualReload = "LocalizationProviderManualReload";

		// Token: 0x04000E84 RID: 3716
		internal const string LocalizationProviderMissingService = "LocalizationProviderMissingService";

		// Token: 0x04000E85 RID: 3717
		internal const string IntegerCollectionEditorTitle = "IntegerCollectionEditorTitle";

		// Token: 0x04000E86 RID: 3718
		internal const string InheritanceServiceReadOnlyCollection = "InheritanceServiceReadOnlyCollection";

		// Token: 0x04000E87 RID: 3719
		internal const string CancelCaption = "CancelCaption";

		// Token: 0x04000E88 RID: 3720
		internal const string OKCaption = "OKCaption";

		// Token: 0x04000E89 RID: 3721
		internal const string HelpCaption = "HelpCaption";

		// Token: 0x04000E8A RID: 3722
		internal const string DataFieldCollectionEditorTitle = "DataFieldCollectionEditorTitle";

		// Token: 0x04000E8B RID: 3723
		internal const string DataFieldCollectionAvailableFields = "DataFieldCollectionAvailableFields";

		// Token: 0x04000E8C RID: 3724
		internal const string DataFieldCollectionSelectedFields = "DataFieldCollectionSelectedFields";

		// Token: 0x04000E8D RID: 3725
		internal const string DataFieldCollection_MoveUp = "DataFieldCollection_MoveUp";

		// Token: 0x04000E8E RID: 3726
		internal const string DataFieldCollection_MoveUpDesc = "DataFieldCollection_MoveUpDesc";

		// Token: 0x04000E8F RID: 3727
		internal const string DataFieldCollection_MoveDown = "DataFieldCollection_MoveDown";

		// Token: 0x04000E90 RID: 3728
		internal const string DataFieldCollection_MoveDownDesc = "DataFieldCollection_MoveDownDesc";

		// Token: 0x04000E91 RID: 3729
		internal const string DataFieldCollection_MoveLeft = "DataFieldCollection_MoveLeft";

		// Token: 0x04000E92 RID: 3730
		internal const string DataFieldCollection_MoveLeftDesc = "DataFieldCollection_MoveLeftDesc";

		// Token: 0x04000E93 RID: 3731
		internal const string DataFieldCollection_MoveRight = "DataFieldCollection_MoveRight";

		// Token: 0x04000E94 RID: 3732
		internal const string DataFieldCollection_MoveRightDesc = "DataFieldCollection_MoveRightDesc";

		// Token: 0x04000E95 RID: 3733
		internal const string SerializerBadElementType = "SerializerBadElementType";

		// Token: 0x04000E96 RID: 3734
		internal const string SerializerBadElementTypes = "SerializerBadElementTypes";

		// Token: 0x04000E97 RID: 3735
		internal const string SerializerMissingService = "SerializerMissingService";

		// Token: 0x04000E98 RID: 3736
		internal const string SerializerNoSerializerForComponent = "SerializerNoSerializerForComponent";

		// Token: 0x04000E99 RID: 3737
		internal const string SerializerLostStatements = "SerializerLostStatements";

		// Token: 0x04000E9A RID: 3738
		internal const string SerializerTypeNotFound = "SerializerTypeNotFound";

		// Token: 0x04000E9B RID: 3739
		internal const string SerializerTypeAbstract = "SerializerTypeAbstract";

		// Token: 0x04000E9C RID: 3740
		internal const string SerializerUndeclaredName = "SerializerUndeclaredName";

		// Token: 0x04000E9D RID: 3741
		internal const string SerializerNoSuchEvent = "SerializerNoSuchEvent";

		// Token: 0x04000E9E RID: 3742
		internal const string SerializerNoSuchField = "SerializerNoSuchField";

		// Token: 0x04000E9F RID: 3743
		internal const string SerializerNoSuchProperty = "SerializerNoSuchProperty";

		// Token: 0x04000EA0 RID: 3744
		internal const string SerializerNullNestedProperty = "SerializerNullNestedProperty";

		// Token: 0x04000EA1 RID: 3745
		internal const string SerializerInvalidArrayRank = "SerializerInvalidArrayRank";

		// Token: 0x04000EA2 RID: 3746
		internal const string SerializerResourceException = "SerializerResourceException";

		// Token: 0x04000EA3 RID: 3747
		internal const string SerializerResourceExceptionInvariant = "SerializerResourceExceptionInvariant";

		// Token: 0x04000EA4 RID: 3748
		internal const string SerializerPropertyGenFailed = "SerializerPropertyGenFailed";

		// Token: 0x04000EA5 RID: 3749
		internal const string SerializerFieldTargetEvalFailed = "SerializerFieldTargetEvalFailed";

		// Token: 0x04000EA6 RID: 3750
		internal const string SerializerMemberTypeNotSerializable = "SerializerMemberTypeNotSerializable";

		// Token: 0x04000EA7 RID: 3751
		internal const string SerializerNoRootExpression = "SerializerNoRootExpression";

		// Token: 0x04000EA8 RID: 3752
		internal const string AXAbout = "AXAbout";

		// Token: 0x04000EA9 RID: 3753
		internal const string AXCannotLoadTypeLib = "AXCannotLoadTypeLib";

		// Token: 0x04000EAA RID: 3754
		internal const string AXCannotOverwriteFile = "AXCannotOverwriteFile";

		// Token: 0x04000EAB RID: 3755
		internal const string AXReadOnlyFile = "AXReadOnlyFile";

		// Token: 0x04000EAC RID: 3756
		internal const string AXCompilerError = "AXCompilerError";

		// Token: 0x04000EAD RID: 3757
		internal const string Ax_Control = "Ax_Control";

		// Token: 0x04000EAE RID: 3758
		internal const string AXEdit = "AXEdit";

		// Token: 0x04000EAF RID: 3759
		internal const string AxImportFailed = "AxImportFailed";

		// Token: 0x04000EB0 RID: 3760
		internal const string AXNoActiveXControls = "AXNoActiveXControls";

		// Token: 0x04000EB1 RID: 3761
		internal const string AXNotRegistered = "AXNotRegistered";

		// Token: 0x04000EB2 RID: 3762
		internal const string AXNotValidControl = "AXNotValidControl";

		// Token: 0x04000EB3 RID: 3763
		internal const string AxImpNoDefaultValue = "AxImpNoDefaultValue";

		// Token: 0x04000EB4 RID: 3764
		internal const string AxImpUnrecognizedDefaultValueType = "AxImpUnrecognizedDefaultValueType";

		// Token: 0x04000EB5 RID: 3765
		internal const string AXProperties = "AXProperties";

		// Token: 0x04000EB6 RID: 3766
		internal const string AXVerbPrefix = "AXVerbPrefix";

		// Token: 0x04000EB7 RID: 3767
		internal const string AdvancedBindingPropertyDescriptorDesc = "AdvancedBindingPropertyDescriptorDesc";

		// Token: 0x04000EB8 RID: 3768
		internal const string AdvancedBindingPropertyDescName = "AdvancedBindingPropertyDescName";

		// Token: 0x04000EB9 RID: 3769
		internal const string AutoAdjustMargins = "AutoAdjustMargins";

		// Token: 0x04000EBA RID: 3770
		internal const string BaseNodeName = "BaseNodeName";

		// Token: 0x04000EBB RID: 3771
		internal const string BindingFormattingDialogAllTreeNode = "BindingFormattingDialogAllTreeNode";

		// Token: 0x04000EBC RID: 3772
		internal const string BindingFormattingDialogBindingPickerAccName = "BindingFormattingDialogBindingPickerAccName";

		// Token: 0x04000EBD RID: 3773
		internal const string BindingFormattingDialogCommonTreeNode = "BindingFormattingDialogCommonTreeNode";

		// Token: 0x04000EBE RID: 3774
		internal const string BindingFormattingDialogCustomFormat = "BindingFormattingDialogCustomFormat";

		// Token: 0x04000EBF RID: 3775
		internal const string BindingFormattingDialogCustomFormatAccessibleDescription = "BindingFormattingDialogCustomFormatAccessibleDescription";

		// Token: 0x04000EC0 RID: 3776
		internal const string BindingFormattingDialogDataSourcePickerDropDownAccName = "BindingFormattingDialogDataSourcePickerDropDownAccName";

		// Token: 0x04000EC1 RID: 3777
		internal const string BindingFormattingDialogDecimalPlaces = "BindingFormattingDialogDecimalPlaces";

		// Token: 0x04000EC2 RID: 3778
		internal const string BindingFormattingDialogFormatTypeCurrency = "BindingFormattingDialogFormatTypeCurrency";

		// Token: 0x04000EC3 RID: 3779
		internal const string BindingFormattingDialogFormatTypeCurrencyExplanation = "BindingFormattingDialogFormatTypeCurrencyExplanation";

		// Token: 0x04000EC4 RID: 3780
		internal const string BindingFormattingDialogFormatTypeCustom = "BindingFormattingDialogFormatTypeCustom";

		// Token: 0x04000EC5 RID: 3781
		internal const string BindingFormattingDialogFormatTypeCustomExplanation = "BindingFormattingDialogFormatTypeCustomExplanation";

		// Token: 0x04000EC6 RID: 3782
		internal const string BindingFormattingDialogFormatTypeCustomInvalidFormat = "BindingFormattingDialogFormatTypeCustomInvalidFormat";

		// Token: 0x04000EC7 RID: 3783
		internal const string BindingFormattingDialogFormatTypeDateTime = "BindingFormattingDialogFormatTypeDateTime";

		// Token: 0x04000EC8 RID: 3784
		internal const string BindingFormattingDialogFormatTypeDateTimeExplanation = "BindingFormattingDialogFormatTypeDateTimeExplanation";

		// Token: 0x04000EC9 RID: 3785
		internal const string BindingFormattingDialogFormatTypeNoFormatting = "BindingFormattingDialogFormatTypeNoFormatting";

		// Token: 0x04000ECA RID: 3786
		internal const string BindingFormattingDialogFormatTypeNoFormattingExplanation = "BindingFormattingDialogFormatTypeNoFormattingExplanation";

		// Token: 0x04000ECB RID: 3787
		internal const string BindingFormattingDialogFormatTypeNumeric = "BindingFormattingDialogFormatTypeNumeric";

		// Token: 0x04000ECC RID: 3788
		internal const string BindingFormattingDialogFormatTypeNumericExplanation = "BindingFormattingDialogFormatTypeNumericExplanation";

		// Token: 0x04000ECD RID: 3789
		internal const string BindingFormattingDialogFormatTypeScientific = "BindingFormattingDialogFormatTypeScientific";

		// Token: 0x04000ECE RID: 3790
		internal const string BindingFormattingDialogFormatTypeScientificExplanation = "BindingFormattingDialogFormatTypeScientificExplanation";

		// Token: 0x04000ECF RID: 3791
		internal const string BindingFormattingDialogList = "BindingFormattingDialogList";

		// Token: 0x04000ED0 RID: 3792
		internal const string BindingFormattingDialogNullValue = "BindingFormattingDialogNullValue";

		// Token: 0x04000ED1 RID: 3793
		internal const string BindingFormattingDialogType = "BindingFormattingDialogType";

		// Token: 0x04000ED2 RID: 3794
		internal const string CellStyleBuilderPreview = "CellStyleBuilderPreview";

		// Token: 0x04000ED3 RID: 3795
		internal const string CellStyleBuilderPreviewText = "CellStyleBuilderPreviewText";

		// Token: 0x04000ED4 RID: 3796
		internal const string CellStyleBuilderTitle = "CellStyleBuilderTitle";

		// Token: 0x04000ED5 RID: 3797
		internal const string CellStyleBuilderNormalPreviewAccName = "CellStyleBuilderNormalPreviewAccName";

		// Token: 0x04000ED6 RID: 3798
		internal const string CellStyleBuilderSelectedPreviewAccName = "CellStyleBuilderSelectedPreviewAccName";

		// Token: 0x04000ED7 RID: 3799
		internal const string CommandSetAlignByPrimary = "CommandSetAlignByPrimary";

		// Token: 0x04000ED8 RID: 3800
		internal const string CommandSetAlignToGrid = "CommandSetAlignToGrid";

		// Token: 0x04000ED9 RID: 3801
		internal const string CommandSetBringToFront = "CommandSetBringToFront";

		// Token: 0x04000EDA RID: 3802
		internal const string CommandSetCutMultiple = "CommandSetCutMultiple";

		// Token: 0x04000EDB RID: 3803
		internal const string CommandSetDelete = "CommandSetDelete";

		// Token: 0x04000EDC RID: 3804
		internal const string CommandSetError = "CommandSetError";

		// Token: 0x04000EDD RID: 3805
		internal const string CommandSetFormatSpacing = "CommandSetFormatSpacing";

		// Token: 0x04000EDE RID: 3806
		internal const string CommandSetLockControls = "CommandSetLockControls";

		// Token: 0x04000EDF RID: 3807
		internal const string CommandSetPaste = "CommandSetPaste";

		// Token: 0x04000EE0 RID: 3808
		internal const string CommandSetSendToBack = "CommandSetSendToBack";

		// Token: 0x04000EE1 RID: 3809
		internal const string CommandSetSize = "CommandSetSize";

		// Token: 0x04000EE2 RID: 3810
		internal const string CommandSetSizeToGrid = "CommandSetSizeToGrid";

		// Token: 0x04000EE3 RID: 3811
		internal const string CommandSetUnknownSpacingCommand = "CommandSetUnknownSpacingCommand";

		// Token: 0x04000EE4 RID: 3812
		internal const string CompositionDesignerWaterMark = "CompositionDesignerWaterMark";

		// Token: 0x04000EE5 RID: 3813
		internal const string CompositionDesignerWaterMarkFirstLink = "CompositionDesignerWaterMarkFirstLink";

		// Token: 0x04000EE6 RID: 3814
		internal const string CompositionDesignerWaterMarkSecondLink = "CompositionDesignerWaterMarkSecondLink";

		// Token: 0x04000EE7 RID: 3815
		internal const string DataGridAdvancedBindingString = "DataGridAdvancedBindingString";

		// Token: 0x04000EE8 RID: 3816
		internal const string DataGridNoneString = "DataGridNoneString";

		// Token: 0x04000EE9 RID: 3817
		internal const string DataGridPopulateError = "DataGridPopulateError";

		// Token: 0x04000EEA RID: 3818
		internal const string DataGridAutoFormatString = "DataGridAutoFormatString";

		// Token: 0x04000EEB RID: 3819
		internal const string DataGridAutoFormatUndoTitle = "DataGridAutoFormatUndoTitle";

		// Token: 0x04000EEC RID: 3820
		internal const string DataGridAutoFormatSchemeName256Color1 = "DataGridAutoFormatSchemeName256Color1";

		// Token: 0x04000EED RID: 3821
		internal const string DataGridAutoFormatSchemeName256Color2 = "DataGridAutoFormatSchemeName256Color2";

		// Token: 0x04000EEE RID: 3822
		internal const string DataGridAutoFormatSchemeNameClassic = "DataGridAutoFormatSchemeNameClassic";

		// Token: 0x04000EEF RID: 3823
		internal const string DataGridAutoFormatSchemeNameColorful1 = "DataGridAutoFormatSchemeNameColorful1";

		// Token: 0x04000EF0 RID: 3824
		internal const string DataGridAutoFormatSchemeNameColorful2 = "DataGridAutoFormatSchemeNameColorful2";

		// Token: 0x04000EF1 RID: 3825
		internal const string DataGridAutoFormatSchemeNameColorful3 = "DataGridAutoFormatSchemeNameColorful3";

		// Token: 0x04000EF2 RID: 3826
		internal const string DataGridAutoFormatSchemeNameColorful4 = "DataGridAutoFormatSchemeNameColorful4";

		// Token: 0x04000EF3 RID: 3827
		internal const string DataGridAutoFormatSchemeNameDefault = "DataGridAutoFormatSchemeNameDefault";

		// Token: 0x04000EF4 RID: 3828
		internal const string DataGridAutoFormatSchemeNameProfessional1 = "DataGridAutoFormatSchemeNameProfessional1";

		// Token: 0x04000EF5 RID: 3829
		internal const string DataGridAutoFormatSchemeNameProfessional2 = "DataGridAutoFormatSchemeNameProfessional2";

		// Token: 0x04000EF6 RID: 3830
		internal const string DataGridAutoFormatSchemeNameProfessional3 = "DataGridAutoFormatSchemeNameProfessional3";

		// Token: 0x04000EF7 RID: 3831
		internal const string DataGridAutoFormatSchemeNameProfessional4 = "DataGridAutoFormatSchemeNameProfessional4";

		// Token: 0x04000EF8 RID: 3832
		internal const string DataGridAutoFormatSchemeNameSimple = "DataGridAutoFormatSchemeNameSimple";

		// Token: 0x04000EF9 RID: 3833
		internal const string DataGridAutoFormatTableFirstColumn = "DataGridAutoFormatTableFirstColumn";

		// Token: 0x04000EFA RID: 3834
		internal const string DataGridAutoFormatTableSecondColumn = "DataGridAutoFormatTableSecondColumn";

		// Token: 0x04000EFB RID: 3835
		internal const string DataGridShowAllString = "DataGridShowAllString";

		// Token: 0x04000EFC RID: 3836
		internal const string DataSourceLocksItems = "DataSourceLocksItems";

		// Token: 0x04000EFD RID: 3837
		internal const string DesignBindingBadParseString = "DesignBindingBadParseString";

		// Token: 0x04000EFE RID: 3838
		internal const string DesignBindingContextRequiredWhenParsing = "DesignBindingContextRequiredWhenParsing";

		// Token: 0x04000EFF RID: 3839
		internal const string DesignBindingComponentNotFound = "DesignBindingComponentNotFound";

		// Token: 0x04000F00 RID: 3840
		internal const string DesignBindingPickerAccessibleName = "DesignBindingPickerAccessibleName";

		// Token: 0x04000F01 RID: 3841
		internal const string DesignBindingPickerAddProjDataSourceLabel = "DesignBindingPickerAddProjDataSourceLabel";

		// Token: 0x04000F02 RID: 3842
		internal const string DesignBindingPickerHelpGenAddDataSrc = "DesignBindingPickerHelpGenAddDataSrc";

		// Token: 0x04000F03 RID: 3843
		internal const string DesignBindingPickerHelpGenCurrentBinding = "DesignBindingPickerHelpGenCurrentBinding";

		// Token: 0x04000F04 RID: 3844
		internal const string DesignBindingPickerHelpGenPickBindSrc = "DesignBindingPickerHelpGenPickBindSrc";

		// Token: 0x04000F05 RID: 3845
		internal const string DesignBindingPickerHelpGenPickDataSrc = "DesignBindingPickerHelpGenPickDataSrc";

		// Token: 0x04000F06 RID: 3846
		internal const string DesignBindingPickerHelpGenPickMember = "DesignBindingPickerHelpGenPickMember";

		// Token: 0x04000F07 RID: 3847
		internal const string DesignBindingPickerHelpNodeBindSrcDM1 = "DesignBindingPickerHelpNodeBindSrcDM1";

		// Token: 0x04000F08 RID: 3848
		internal const string DesignBindingPickerHelpNodeBindSrcDS0 = "DesignBindingPickerHelpNodeBindSrcDS0";

		// Token: 0x04000F09 RID: 3849
		internal const string DesignBindingPickerHelpNodeBindSrcDS1 = "DesignBindingPickerHelpNodeBindSrcDS1";

		// Token: 0x04000F0A RID: 3850
		internal const string DesignBindingPickerHelpNodeBindSrcLM1 = "DesignBindingPickerHelpNodeBindSrcLM1";

		// Token: 0x04000F0B RID: 3851
		internal const string DesignBindingPickerHelpNodeFormInstDM1 = "DesignBindingPickerHelpNodeFormInstDM1";

		// Token: 0x04000F0C RID: 3852
		internal const string DesignBindingPickerHelpNodeFormInstDS0 = "DesignBindingPickerHelpNodeFormInstDS0";

		// Token: 0x04000F0D RID: 3853
		internal const string DesignBindingPickerHelpNodeFormInstDS1 = "DesignBindingPickerHelpNodeFormInstDS1";

		// Token: 0x04000F0E RID: 3854
		internal const string DesignBindingPickerHelpNodeFormInstLM0 = "DesignBindingPickerHelpNodeFormInstLM0";

		// Token: 0x04000F0F RID: 3855
		internal const string DesignBindingPickerHelpNodeFormInstLM1 = "DesignBindingPickerHelpNodeFormInstLM1";

		// Token: 0x04000F10 RID: 3856
		internal const string DesignBindingPickerHelpNodeInstances = "DesignBindingPickerHelpNodeInstances";

		// Token: 0x04000F11 RID: 3857
		internal const string DesignBindingPickerHelpNodeNone = "DesignBindingPickerHelpNodeNone";

		// Token: 0x04000F12 RID: 3858
		internal const string DesignBindingPickerHelpNodeOther = "DesignBindingPickerHelpNodeOther";

		// Token: 0x04000F13 RID: 3859
		internal const string DesignBindingPickerHelpNodeProject = "DesignBindingPickerHelpNodeProject";

		// Token: 0x04000F14 RID: 3860
		internal const string DesignBindingPickerHelpNodeProjectDM1 = "DesignBindingPickerHelpNodeProjectDM1";

		// Token: 0x04000F15 RID: 3861
		internal const string DesignBindingPickerHelpNodeProjectDS0 = "DesignBindingPickerHelpNodeProjectDS0";

		// Token: 0x04000F16 RID: 3862
		internal const string DesignBindingPickerHelpNodeProjectDS1 = "DesignBindingPickerHelpNodeProjectDS1";

		// Token: 0x04000F17 RID: 3863
		internal const string DesignBindingPickerHelpNodeProjectLM0 = "DesignBindingPickerHelpNodeProjectLM0";

		// Token: 0x04000F18 RID: 3864
		internal const string DesignBindingPickerHelpNodeProjectLM1 = "DesignBindingPickerHelpNodeProjectLM1";

		// Token: 0x04000F19 RID: 3865
		internal const string DesignBindingPickerHelpNodeProjectGroup = "DesignBindingPickerHelpNodeProjectGroup";

		// Token: 0x04000F1A RID: 3866
		internal const string DesignBindingPickerNodeNone = "DesignBindingPickerNodeNone";

		// Token: 0x04000F1B RID: 3867
		internal const string DesignBindingPickerNodeOther = "DesignBindingPickerNodeOther";

		// Token: 0x04000F1C RID: 3868
		internal const string DesignBindingPickerNodeProject = "DesignBindingPickerNodeProject";

		// Token: 0x04000F1D RID: 3869
		internal const string DesignBindingPickerNodeInstances = "DesignBindingPickerNodeInstances";

		// Token: 0x04000F1E RID: 3870
		internal const string DesignBindingPickerTreeViewAccessibleName = "DesignBindingPickerTreeViewAccessibleName";

		// Token: 0x04000F1F RID: 3871
		internal const string DesignerBatchCreateTool = "DesignerBatchCreateTool";

		// Token: 0x04000F20 RID: 3872
		internal const string DesignerCantParentType = "DesignerCantParentType";

		// Token: 0x04000F21 RID: 3873
		internal const string DesignerDefaultTab = "DesignerDefaultTab";

		// Token: 0x04000F22 RID: 3874
		internal const string UserControlTab = "UserControlTab";

		// Token: 0x04000F23 RID: 3875
		internal const string DesignerShortcutDockInParent = "DesignerShortcutDockInParent";

		// Token: 0x04000F24 RID: 3876
		internal const string DesignerShortcutUndockInParent = "DesignerShortcutUndockInParent";

		// Token: 0x04000F25 RID: 3877
		internal const string DesignerShortcutDockInForm = "DesignerShortcutDockInForm";

		// Token: 0x04000F26 RID: 3878
		internal const string DesignerShortcutDockInUserControl = "DesignerShortcutDockInUserControl";

		// Token: 0x04000F27 RID: 3879
		internal const string DesignerShortcutReparentControls = "DesignerShortcutReparentControls";

		// Token: 0x04000F28 RID: 3880
		internal const string DesignerShortcutHorizontalOrientation = "DesignerShortcutHorizontalOrientation";

		// Token: 0x04000F29 RID: 3881
		internal const string DesignerShortcutVerticalOrientation = "DesignerShortcutVerticalOrientation";

		// Token: 0x04000F2A RID: 3882
		internal const string DesignerNoUserControl = "DesignerNoUserControl";

		// Token: 0x04000F2B RID: 3883
		internal const string DesignerPropName = "DesignerPropName";

		// Token: 0x04000F2C RID: 3884
		internal const string DesignerBeginDragNotCalled = "DesignerBeginDragNotCalled";

		// Token: 0x04000F2D RID: 3885
		internal const string DesignerInheritedReadOnly = "DesignerInheritedReadOnly";

		// Token: 0x04000F2E RID: 3886
		internal const string DesignerInherited = "DesignerInherited";

		// Token: 0x04000F2F RID: 3887
		internal const string DesignerPropNotFound = "DesignerPropNotFound";

		// Token: 0x04000F30 RID: 3888
		internal const string TypeNotFoundInTargetFramework = "TypeNotFoundInTargetFramework";

		// Token: 0x04000F31 RID: 3889
		internal const string DragDropDragComponents = "DragDropDragComponents";

		// Token: 0x04000F32 RID: 3890
		internal const string DragDropMoveComponent = "DragDropMoveComponent";

		// Token: 0x04000F33 RID: 3891
		internal const string DragDropMoveComponents = "DragDropMoveComponents";

		// Token: 0x04000F34 RID: 3892
		internal const string DragDropSizeComponent = "DragDropSizeComponent";

		// Token: 0x04000F35 RID: 3893
		internal const string DragDropSizeComponents = "DragDropSizeComponents";

		// Token: 0x04000F36 RID: 3894
		internal const string DragDropDropComponents = "DragDropDropComponents";

		// Token: 0x04000F37 RID: 3895
		internal const string DragDropSetDataError = "DragDropSetDataError";

		// Token: 0x04000F38 RID: 3896
		internal const string GenericFileFilter = "GenericFileFilter";

		// Token: 0x04000F39 RID: 3897
		internal const string GenericOpenFile = "GenericOpenFile";

		// Token: 0x04000F3A RID: 3898
		internal const string DataGridViewAdd = "DataGridViewAdd";

		// Token: 0x04000F3B RID: 3899
		internal const string DataGridViewAddColumn = "DataGridViewAddColumn";

		// Token: 0x04000F3C RID: 3900
		internal const string DataGridViewAddColumnDialogTitle = "DataGridViewAddColumnDialogTitle";

		// Token: 0x04000F3D RID: 3901
		internal const string DataGridViewAddColumnTransactionString = "DataGridViewAddColumnTransactionString";

		// Token: 0x04000F3E RID: 3902
		internal const string DataGridViewAddColumnVerb = "DataGridViewAddColumnVerb";

		// Token: 0x04000F3F RID: 3903
		internal const string DataGridViewBoundColumnProperties = "DataGridViewBoundColumnProperties";

		// Token: 0x04000F40 RID: 3904
		internal const string DataGridViewChooseDataSource = "DataGridViewChooseDataSource";

		// Token: 0x04000F41 RID: 3905
		internal const string DataGridViewColumnTypePropertyDescription = "DataGridViewColumnTypePropertyDescription";

		// Token: 0x04000F42 RID: 3906
		internal const string DataGridViewColumnCollectionTransaction = "DataGridViewColumnCollectionTransaction";

		// Token: 0x04000F43 RID: 3907
		internal const string DataGridViewDataSourceNoLongerValid = "DataGridViewDataSourceNoLongerValid";

		// Token: 0x04000F44 RID: 3908
		internal const string DataGridViewDeleteAccName = "DataGridViewDeleteAccName";

		// Token: 0x04000F45 RID: 3909
		internal const string DataGridViewDuplicateColumnName = "DataGridViewDuplicateColumnName";

		// Token: 0x04000F46 RID: 3910
		internal const string DataGridViewChooseDataSourceTransactionString = "DataGridViewChooseDataSourceTransactionString";

		// Token: 0x04000F47 RID: 3911
		internal const string DataGridViewDisableAddingTransactionString = "DataGridViewDisableAddingTransactionString";

		// Token: 0x04000F48 RID: 3912
		internal const string DataGridViewDisableColumnReorderingTransactionString = "DataGridViewDisableColumnReorderingTransactionString";

		// Token: 0x04000F49 RID: 3913
		internal const string DataGridViewDisableDeletingTransactionString = "DataGridViewDisableDeletingTransactionString";

		// Token: 0x04000F4A RID: 3914
		internal const string DataGridViewDisableEditingTransactionString = "DataGridViewDisableEditingTransactionString";

		// Token: 0x04000F4B RID: 3915
		internal const string DataGridViewEditColumnsTransactionString = "DataGridViewEditColumnsTransactionString";

		// Token: 0x04000F4C RID: 3916
		internal const string DataGridViewEnableAdding = "DataGridViewEnableAdding";

		// Token: 0x04000F4D RID: 3917
		internal const string DataGridViewEnableAddingTransactionString = "DataGridViewEnableAddingTransactionString";

		// Token: 0x04000F4E RID: 3918
		internal const string DataGridViewEnableDeleting = "DataGridViewEnableDeleting";

		// Token: 0x04000F4F RID: 3919
		internal const string DataGridViewEnableDeletingTransactionString = "DataGridViewEnableDeletingTransactionString";

		// Token: 0x04000F50 RID: 3920
		internal const string DataGridViewEnableEditing = "DataGridViewEnableEditing";

		// Token: 0x04000F51 RID: 3921
		internal const string DataGridViewEnableEditingTransactionString = "DataGridViewEnableEditingTransactionString";

		// Token: 0x04000F52 RID: 3922
		internal const string DataGridViewEditingTransactionString = "DataGridViewEditingTransactionString";

		// Token: 0x04000F53 RID: 3923
		internal const string DataGridViewEnableColumnReordering = "DataGridViewEnableColumnReordering";

		// Token: 0x04000F54 RID: 3924
		internal const string DataGridViewEnableColumnReorderingTransactionString = "DataGridViewEnableColumnReorderingTransactionString";

		// Token: 0x04000F55 RID: 3925
		internal const string DataGridView_Cancel = "DataGridView_Cancel";

		// Token: 0x04000F56 RID: 3926
		internal const string DataGridView_Close = "DataGridView_Close";

		// Token: 0x04000F57 RID: 3927
		internal const string DataGridViewEditColumnsVerb = "DataGridViewEditColumnsVerb";

		// Token: 0x04000F58 RID: 3928
		internal const string DataGridViewEditColumns = "DataGridViewEditColumns";

		// Token: 0x04000F59 RID: 3929
		internal const string DataGridViewFrozen = "DataGridViewFrozen";

		// Token: 0x04000F5A RID: 3930
		internal const string DataGridViewDataBoundColumn = "DataGridViewDataBoundColumn";

		// Token: 0x04000F5B RID: 3931
		internal const string DataGridViewDataSourceColumns = "DataGridViewDataSourceColumns";

		// Token: 0x04000F5C RID: 3932
		internal const string DataGridViewHeaderText = "DataGridViewHeaderText";

		// Token: 0x04000F5D RID: 3933
		internal const string DataGridViewHelp = "DataGridViewHelp";

		// Token: 0x04000F5E RID: 3934
		internal const string DataGridViewMoveDownAccName = "DataGridViewMoveDownAccName";

		// Token: 0x04000F5F RID: 3935
		internal const string DataGridViewMoveUpAccName = "DataGridViewMoveUpAccName";

		// Token: 0x04000F60 RID: 3936
		internal const string DataGridViewName = "DataGridViewName";

		// Token: 0x04000F61 RID: 3937
		internal const string DataGridViewNormalLabel = "DataGridViewNormalLabel";

		// Token: 0x04000F62 RID: 3938
		internal const string DataGridView_OK = "DataGridView_OK";

		// Token: 0x04000F63 RID: 3939
		internal const string DataGridViewProperties = "DataGridViewProperties";

		// Token: 0x04000F64 RID: 3940
		internal const string DataGridViewReadOnly = "DataGridViewReadOnly";

		// Token: 0x04000F65 RID: 3941
		internal const string DataGridViewSelectedColumns = "DataGridViewSelectedColumns";

		// Token: 0x04000F66 RID: 3942
		internal const string DataGridViewSelectedLabel = "DataGridViewSelectedLabel";

		// Token: 0x04000F67 RID: 3943
		internal const string DataGridViewType = "DataGridViewType";

		// Token: 0x04000F68 RID: 3944
		internal const string DataGridViewUnboundColumn = "DataGridViewUnboundColumn";

		// Token: 0x04000F69 RID: 3945
		internal const string DataGridViewUnboundColumnProperties = "DataGridViewUnboundColumnProperties";

		// Token: 0x04000F6A RID: 3946
		internal const string DataGridViewVisible = "DataGridViewVisible";

		// Token: 0x04000F6B RID: 3947
		internal const string FailedToCreateComponent = "FailedToCreateComponent";

		// Token: 0x04000F6C RID: 3948
		internal const string FormatStringDialogTitle = "FormatStringDialogTitle";

		// Token: 0x04000F6D RID: 3949
		internal const string HelpProviderEditorFilter = "HelpProviderEditorFilter";

		// Token: 0x04000F6E RID: 3950
		internal const string HelpProviderEditorTitle = "HelpProviderEditorTitle";

		// Token: 0x04000F6F RID: 3951
		internal const string imageFileDescription = "imageFileDescription";

		// Token: 0x04000F70 RID: 3952
		internal const string ImageListDesignerBadImageListImage = "ImageListDesignerBadImageListImage";

		// Token: 0x04000F71 RID: 3953
		internal const string ImageCollectionEditorFormText = "ImageCollectionEditorFormText";

		// Token: 0x04000F72 RID: 3954
		internal const string IntegerCollectionEditorCancelCaption = "IntegerCollectionEditorCancelCaption";

		// Token: 0x04000F73 RID: 3955
		internal const string IntegerCollectionEditorInstruction = "IntegerCollectionEditorInstruction";

		// Token: 0x04000F74 RID: 3956
		internal const string IntegerCollectionEditorOKCaption = "IntegerCollectionEditorOKCaption";

		// Token: 0x04000F75 RID: 3957
		internal const string IntegerCollectionEditorHelpCaption = "IntegerCollectionEditorHelpCaption";

		// Token: 0x04000F76 RID: 3958
		internal const string InvalidArgument = "InvalidArgument";

		// Token: 0x04000F77 RID: 3959
		internal const string InvalidArgumentType = "InvalidArgumentType";

		// Token: 0x04000F78 RID: 3960
		internal const string InvalidBoundArgument = "InvalidBoundArgument";

		// Token: 0x04000F79 RID: 3961
		internal const string LinkAreaEditorCancel = "LinkAreaEditorCancel";

		// Token: 0x04000F7A RID: 3962
		internal const string LinkAreaEditorCaption = "LinkAreaEditorCaption";

		// Token: 0x04000F7B RID: 3963
		internal const string LinkAreaEditorDescription = "LinkAreaEditorDescription";

		// Token: 0x04000F7C RID: 3964
		internal const string LinkAreaEditorOK = "LinkAreaEditorOK";

		// Token: 0x04000F7D RID: 3965
		internal const string ListViewItemBaseName = "ListViewItemBaseName";

		// Token: 0x04000F7E RID: 3966
		internal const string ListViewSubItemBaseName = "ListViewSubItemBaseName";

		// Token: 0x04000F7F RID: 3967
		internal const string MaskDescriptorNullOrEmptyRequiredProperty = "MaskDescriptorNullOrEmptyRequiredProperty";

		// Token: 0x04000F80 RID: 3968
		internal const string MaskDescriptorNull = "MaskDescriptorNull";

		// Token: 0x04000F81 RID: 3969
		internal const string MaskDescriptorNotMaskFullErrorMsg = "MaskDescriptorNotMaskFullErrorMsg";

		// Token: 0x04000F82 RID: 3970
		internal const string MaskDescriptorValidatingTypeNone = "MaskDescriptorValidatingTypeNone";

		// Token: 0x04000F83 RID: 3971
		internal const string MaskDesignerDialogCustomEntry = "MaskDesignerDialogCustomEntry";

		// Token: 0x04000F84 RID: 3972
		internal const string MaskDesignerDialogDataFormat = "MaskDesignerDialogDataFormat";

		// Token: 0x04000F85 RID: 3973
		internal const string MaskDesignerDialogDlgCaption = "MaskDesignerDialogDlgCaption";

		// Token: 0x04000F86 RID: 3974
		internal const string MaskDesignerDialogMaskDescription = "MaskDesignerDialogMaskDescription";

		// Token: 0x04000F87 RID: 3975
		internal const string MaskDesignerDialogValidatingType = "MaskDesignerDialogValidatingType";

		// Token: 0x04000F88 RID: 3976
		internal const string MaskedTextBoxDesignerVerbsSetMaskDesc = "MaskedTextBoxDesignerVerbsSetMaskDesc";

		// Token: 0x04000F89 RID: 3977
		internal const string MaskedTextBoxTextEditorErrorFormatString = "MaskedTextBoxTextEditorErrorFormatString";

		// Token: 0x04000F8A RID: 3978
		internal const string MaskedTextBoxHintAsciiCharacterExpected = "MaskedTextBoxHintAsciiCharacterExpected";

		// Token: 0x04000F8B RID: 3979
		internal const string MaskedTextBoxHintAlphanumericCharacterExpected = "MaskedTextBoxHintAlphanumericCharacterExpected";

		// Token: 0x04000F8C RID: 3980
		internal const string MaskedTextBoxHintDigitExpected = "MaskedTextBoxHintDigitExpected";

		// Token: 0x04000F8D RID: 3981
		internal const string MaskedTextBoxHintSignedDigitExpected = "MaskedTextBoxHintSignedDigitExpected";

		// Token: 0x04000F8E RID: 3982
		internal const string MaskedTextBoxHintLetterExpected = "MaskedTextBoxHintLetterExpected";

		// Token: 0x04000F8F RID: 3983
		internal const string MaskedTextBoxHintPromptCharNotAllowed = "MaskedTextBoxHintPromptCharNotAllowed";

		// Token: 0x04000F90 RID: 3984
		internal const string MaskedTextBoxHintUnavailableEditPosition = "MaskedTextBoxHintUnavailableEditPosition";

		// Token: 0x04000F91 RID: 3985
		internal const string MaskedTextBoxHintNonEditPosition = "MaskedTextBoxHintNonEditPosition";

		// Token: 0x04000F92 RID: 3986
		internal const string MaskedTextBoxHintPositionOutOfRange = "MaskedTextBoxHintPositionOutOfRange";

		// Token: 0x04000F93 RID: 3987
		internal const string MaskedTextBoxHintInvalidInput = "MaskedTextBoxHintInvalidInput";

		// Token: 0x04000F94 RID: 3988
		internal const string MenuCommandService_DuplicateCommand = "MenuCommandService_DuplicateCommand";

		// Token: 0x04000F95 RID: 3989
		internal const string lockedDescr = "lockedDescr";

		// Token: 0x04000F96 RID: 3990
		internal const string ParentControlDesignerDrawGridDescr = "ParentControlDesignerDrawGridDescr";

		// Token: 0x04000F97 RID: 3991
		internal const string ParentControlDesignerSnapToGridDescr = "ParentControlDesignerSnapToGridDescr";

		// Token: 0x04000F98 RID: 3992
		internal const string ParentControlDesignerGridSizeDescr = "ParentControlDesignerGridSizeDescr";

		// Token: 0x04000F99 RID: 3993
		internal const string ParentControlDesignerLanguageDescr = "ParentControlDesignerLanguageDescr";

		// Token: 0x04000F9A RID: 3994
		internal const string ParentControlDesignerLassoShortcutRedo = "ParentControlDesignerLassoShortcutRedo";

		// Token: 0x04000F9B RID: 3995
		internal const string PerformAutoAnchor = "PerformAutoAnchor";

		// Token: 0x04000F9C RID: 3996
		internal const string RtfFileFilter = "RtfFileFilter";

		// Token: 0x04000F9D RID: 3997
		internal const string RtfOpenFile = "RtfOpenFile";

		// Token: 0x04000F9E RID: 3998
		internal const string SelectedPathEditorLabel = "SelectedPathEditorLabel";

		// Token: 0x04000F9F RID: 3999
		internal const string ShortcutKeys_InvalidKey = "ShortcutKeys_InvalidKey";

		// Token: 0x04000FA0 RID: 4000
		internal const string SoundPathWavFile = "SoundPathWavFile";

		// Token: 0x04000FA1 RID: 4001
		internal const string SoundPathEditorOpenFile = "SoundPathEditorOpenFile";

		// Token: 0x04000FA2 RID: 4002
		internal const string SoundPlayNowString = "SoundPlayNowString";

		// Token: 0x04000FA3 RID: 4003
		internal const string SplitContainerReplaceString = "SplitContainerReplaceString";

		// Token: 0x04000FA4 RID: 4004
		internal const string SplitContainerReplaceCaption = "SplitContainerReplaceCaption";

		// Token: 0x04000FA5 RID: 4005
		internal const string SplitterHorizontalOrientation = "SplitterHorizontalOrientation";

		// Token: 0x04000FA6 RID: 4006
		internal const string SplitterVerticalOrientation = "SplitterVerticalOrientation";

		// Token: 0x04000FA7 RID: 4007
		internal const string TabControlAdd = "TabControlAdd";

		// Token: 0x04000FA8 RID: 4008
		internal const string TabControlAddTab = "TabControlAddTab";

		// Token: 0x04000FA9 RID: 4009
		internal const string TabControlRemoveTab = "TabControlRemoveTab";

		// Token: 0x04000FAA RID: 4010
		internal const string TabControlRemove = "TabControlRemove";

		// Token: 0x04000FAB RID: 4011
		internal const string TabControlInvalidTabPageType = "TabControlInvalidTabPageType";

		// Token: 0x04000FAC RID: 4012
		internal const string TableLayoutPanelFullDesc = "TableLayoutPanelFullDesc";

		// Token: 0x04000FAD RID: 4013
		internal const string TableLayoutPanelSpanDesc = "TableLayoutPanelSpanDesc";

		// Token: 0x04000FAE RID: 4014
		internal const string TableLayoutPanelRowColResize = "TableLayoutPanelRowColResize";

		// Token: 0x04000FAF RID: 4015
		internal const string TableLayoutPanelDesignerChangeSizeTypeUndoUnit = "TableLayoutPanelDesignerChangeSizeTypeUndoUnit";

		// Token: 0x04000FB0 RID: 4016
		internal const string TableLayoutPanelDesignerClearAnchor = "TableLayoutPanelDesignerClearAnchor";

		// Token: 0x04000FB1 RID: 4017
		internal const string TableLayoutPanelDesignerClearDock = "TableLayoutPanelDesignerClearDock";

		// Token: 0x04000FB2 RID: 4018
		internal const string TableLayoutPanelDesignerAddColumn = "TableLayoutPanelDesignerAddColumn";

		// Token: 0x04000FB3 RID: 4019
		internal const string TableLayoutPanelDesignerAddRow = "TableLayoutPanelDesignerAddRow";

		// Token: 0x04000FB4 RID: 4020
		internal const string TableLayoutPanelDesignerRemoveColumn = "TableLayoutPanelDesignerRemoveColumn";

		// Token: 0x04000FB5 RID: 4021
		internal const string TableLayoutPanelDesignerRemoveRow = "TableLayoutPanelDesignerRemoveRow";

		// Token: 0x04000FB6 RID: 4022
		internal const string TableLayoutPanelDesignerEditRowAndCol = "TableLayoutPanelDesignerEditRowAndCol";

		// Token: 0x04000FB7 RID: 4023
		internal const string TableLayoutPanelDesignerRowMenu = "TableLayoutPanelDesignerRowMenu";

		// Token: 0x04000FB8 RID: 4024
		internal const string TableLayoutPanelDesignerColMenu = "TableLayoutPanelDesignerColMenu";

		// Token: 0x04000FB9 RID: 4025
		internal const string TableLayoutPanelDesignerAddMenu = "TableLayoutPanelDesignerAddMenu";

		// Token: 0x04000FBA RID: 4026
		internal const string TableLayoutPanelDesignerInsertMenu = "TableLayoutPanelDesignerInsertMenu";

		// Token: 0x04000FBB RID: 4027
		internal const string TableLayoutPanelDesignerDeleteMenu = "TableLayoutPanelDesignerDeleteMenu";

		// Token: 0x04000FBC RID: 4028
		internal const string TableLayoutPanelDesignerLabelMenu = "TableLayoutPanelDesignerLabelMenu";

		// Token: 0x04000FBD RID: 4029
		internal const string TableLayoutPanelDesignerDontBoldLabel = "TableLayoutPanelDesignerDontBoldLabel";

		// Token: 0x04000FBE RID: 4030
		internal const string TableLayoutPanelDesignerAbsoluteMenu = "TableLayoutPanelDesignerAbsoluteMenu";

		// Token: 0x04000FBF RID: 4031
		internal const string TableLayoutPanelDesignerPercentageMenu = "TableLayoutPanelDesignerPercentageMenu";

		// Token: 0x04000FC0 RID: 4032
		internal const string TableLayoutPanelDesignerAutoSizeMenu = "TableLayoutPanelDesignerAutoSizeMenu";

		// Token: 0x04000FC1 RID: 4033
		internal const string TableLayoutPanelDesignerContextMenuCut = "TableLayoutPanelDesignerContextMenuCut";

		// Token: 0x04000FC2 RID: 4034
		internal const string TableLayoutPanelDesignerContextMenuCopy = "TableLayoutPanelDesignerContextMenuCopy";

		// Token: 0x04000FC3 RID: 4035
		internal const string TableLayoutPanelDesignerContextMenuDelete = "TableLayoutPanelDesignerContextMenuDelete";

		// Token: 0x04000FC4 RID: 4036
		internal const string TableLayoutPanelDesignerAddColumnUndoUnit = "TableLayoutPanelDesignerAddColumnUndoUnit";

		// Token: 0x04000FC5 RID: 4037
		internal const string TableLayoutPanelDesignerAddRowUndoUnit = "TableLayoutPanelDesignerAddRowUndoUnit";

		// Token: 0x04000FC6 RID: 4038
		internal const string TableLayoutPanelDesignerRemoveColumnUndoUnit = "TableLayoutPanelDesignerRemoveColumnUndoUnit";

		// Token: 0x04000FC7 RID: 4039
		internal const string TableLayoutPanelDesignerRemoveRowUndoUnit = "TableLayoutPanelDesignerRemoveRowUndoUnit";

		// Token: 0x04000FC8 RID: 4040
		internal const string TableLayoutPanelDesignerControlsSwapped = "TableLayoutPanelDesignerControlsSwapped";

		// Token: 0x04000FC9 RID: 4041
		internal const string TableLayoutPanelDesignerInvalidColumnRowCount = "TableLayoutPanelDesignerInvalidColumnRowCount";

		// Token: 0x04000FCA RID: 4042
		internal const string ToolStripTemplateNodeImageResetCaption = "ToolStripTemplateNodeImageResetCaption";

		// Token: 0x04000FCB RID: 4043
		internal const string ToolStripTemplateNodeImageResetString = "ToolStripTemplateNodeImageResetString";

		// Token: 0x04000FCC RID: 4044
		internal const string ToolStripItemPropertyChangeTransaction = "ToolStripItemPropertyChangeTransaction";

		// Token: 0x04000FCD RID: 4045
		internal const string ToolStripInsertItemsVerb = "ToolStripInsertItemsVerb";

		// Token: 0x04000FCE RID: 4046
		internal const string ToolStripSelectAllVerb = "ToolStripSelectAllVerb";

		// Token: 0x04000FCF RID: 4047
		internal const string ToolStripDropDownDesignerDropDownMenu = "ToolStripDropDownDesignerDropDownMenu";

		// Token: 0x04000FD0 RID: 4048
		internal const string ToolStripMorphingItemTransaction = "ToolStripMorphingItemTransaction";

		// Token: 0x04000FD1 RID: 4049
		internal const string ToolStripCreatingNewItemTransaction = "ToolStripCreatingNewItemTransaction";

		// Token: 0x04000FD2 RID: 4050
		internal const string ToolStripInsertingIntoDropDownTransaction = "ToolStripInsertingIntoDropDownTransaction";

		// Token: 0x04000FD3 RID: 4051
		internal const string ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue = "ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue";

		// Token: 0x04000FD4 RID: 4052
		internal const string ToolStripSelectMenuItem = "ToolStripSelectMenuItem";

		// Token: 0x04000FD5 RID: 4053
		internal const string ToolStripPanelGlyphUnsupportedDock = "ToolStripPanelGlyphUnsupportedDock";

		// Token: 0x04000FD6 RID: 4054
		internal const string WindowsFormsAddEvent = "WindowsFormsAddEvent";

		// Token: 0x04000FD7 RID: 4055
		internal const string WindowsFormsCommandCenterX = "WindowsFormsCommandCenterX";

		// Token: 0x04000FD8 RID: 4056
		internal const string WindowsFormsCommandCenterY = "WindowsFormsCommandCenterY";

		// Token: 0x04000FD9 RID: 4057
		internal const string WindowsFormsTabOrderReadOnly = "WindowsFormsTabOrderReadOnly";

		// Token: 0x04000FDA RID: 4058
		internal const string OK = "OK";

		// Token: 0x04000FDB RID: 4059
		internal const string Cancel = "Cancel";

		// Token: 0x04000FDC RID: 4060
		internal const string Value = "Value";

		// Token: 0x04000FDD RID: 4061
		internal const string None = "None";

		// Token: 0x04000FDE RID: 4062
		internal const string Default = "Default";

		// Token: 0x04000FDF RID: 4063
		internal const string Custom = "Custom";

		// Token: 0x04000FE0 RID: 4064
		internal const string Edit = "Edit";

		// Token: 0x04000FE1 RID: 4065
		internal const string None_lc = "None_lc";

		// Token: 0x04000FE2 RID: 4066
		internal const string Control_ErrorRendering = "Control_ErrorRendering";

		// Token: 0x04000FE3 RID: 4067
		internal const string Control_ErrorRenderingShort = "Control_ErrorRenderingShort";

		// Token: 0x04000FE4 RID: 4068
		internal const string Control_Expressions = "Control_Expressions";

		// Token: 0x04000FE5 RID: 4069
		internal const string Control_CanOnlyBePlacedInside = "Control_CanOnlyBePlacedInside";

		// Token: 0x04000FE6 RID: 4070
		internal const string ControlDesigner_DesignTimeHtmlError = "ControlDesigner_DesignTimeHtmlError";

		// Token: 0x04000FE7 RID: 4071
		internal const string ControlDesigner_UnhandledException = "ControlDesigner_UnhandledException";

		// Token: 0x04000FE8 RID: 4072
		internal const string ControlDesigner_TransactedChangeRequiresServiceProvider = "ControlDesigner_TransactedChangeRequiresServiceProvider";

		// Token: 0x04000FE9 RID: 4073
		internal const string ControlDesigner_CouldNotGetExpressionBuilder = "ControlDesigner_CouldNotGetExpressionBuilder";

		// Token: 0x04000FEA RID: 4074
		internal const string ControlDesigner_CouldNotGetDesignTimeResourceProviderFactory = "ControlDesigner_CouldNotGetDesignTimeResourceProviderFactory";

		// Token: 0x04000FEB RID: 4075
		internal const string ControlDesigner_ArgumentMustBeOfType = "ControlDesigner_ArgumentMustBeOfType";

		// Token: 0x04000FEC RID: 4076
		internal const string ControlDesigner_EditDataBindingsRequiresID = "ControlDesigner_EditDataBindingsRequiresID";

		// Token: 0x04000FED RID: 4077
		internal const string UnsettableComboBox_NotSet = "UnsettableComboBox_NotSet";

		// Token: 0x04000FEE RID: 4078
		internal const string ControlLocalizer_RequiresFilterService = "ControlLocalizer_RequiresFilterService";

		// Token: 0x04000FEF RID: 4079
		internal const string Wizard_NextButton = "Wizard_NextButton";

		// Token: 0x04000FF0 RID: 4080
		internal const string Wizard_PreviousButton = "Wizard_PreviousButton";

		// Token: 0x04000FF1 RID: 4081
		internal const string Wizard_CancelButton = "Wizard_CancelButton";

		// Token: 0x04000FF2 RID: 4082
		internal const string Wizard_FinishButton = "Wizard_FinishButton";

		// Token: 0x04000FF3 RID: 4083
		internal const string WizardAFmt_Scheme_Default = "WizardAFmt_Scheme_Default";

		// Token: 0x04000FF4 RID: 4084
		internal const string WizardAFmt_Scheme_Classic = "WizardAFmt_Scheme_Classic";

		// Token: 0x04000FF5 RID: 4085
		internal const string WizardAFmt_Scheme_Simple = "WizardAFmt_Scheme_Simple";

		// Token: 0x04000FF6 RID: 4086
		internal const string WizardAFmt_Scheme_Professional = "WizardAFmt_Scheme_Professional";

		// Token: 0x04000FF7 RID: 4087
		internal const string WizardAFmt_Scheme_Colorful = "WizardAFmt_Scheme_Colorful";

		// Token: 0x04000FF8 RID: 4088
		internal const string Wizard_StepsView = "Wizard_StepsView";

		// Token: 0x04000FF9 RID: 4089
		internal const string Wizard_StepsViewDescription = "Wizard_StepsViewDescription";

		// Token: 0x04000FFA RID: 4090
		internal const string CreateUserWizard_ConvertToCustomNavigationTemplate = "CreateUserWizard_ConvertToCustomNavigationTemplate";

		// Token: 0x04000FFB RID: 4091
		internal const string CreateUserWizard_CustomizeCreateUserStep = "CreateUserWizard_CustomizeCreateUserStep";

		// Token: 0x04000FFC RID: 4092
		internal const string CreateUserWizard_CustomizeCreateUserStepDescription = "CreateUserWizard_CustomizeCreateUserStepDescription";

		// Token: 0x04000FFD RID: 4093
		internal const string CreateUserWizard_CustomizeCompleteStep = "CreateUserWizard_CustomizeCompleteStep";

		// Token: 0x04000FFE RID: 4094
		internal const string CreateUserWizard_CustomizeCompleteStepDescription = "CreateUserWizard_CustomizeCompleteStepDescription";

		// Token: 0x04000FFF RID: 4095
		internal const string CreateUserWizard_ResetCreateUserStepVerb = "CreateUserWizard_ResetCreateUserStepVerb";

		// Token: 0x04001000 RID: 4096
		internal const string CreateUserWizard_ResetCreateUserStepVerbDescription = "CreateUserWizard_ResetCreateUserStepVerbDescription";

		// Token: 0x04001001 RID: 4097
		internal const string CreateUserWizard_ResetCompleteStepVerb = "CreateUserWizard_ResetCompleteStepVerb";

		// Token: 0x04001002 RID: 4098
		internal const string CreateUserWizard_ResetCompleteStepVerbDescription = "CreateUserWizard_ResetCompleteStepVerbDescription";

		// Token: 0x04001003 RID: 4099
		internal const string CreateUserWizard_NavigateToStep = "CreateUserWizard_NavigateToStep";

		// Token: 0x04001004 RID: 4100
		internal const string CreateUserWizardAutoFormat_UserName = "CreateUserWizardAutoFormat_UserName";

		// Token: 0x04001005 RID: 4101
		internal const string CreateUserWizardAutoFormat_HelpPageText = "CreateUserWizardAutoFormat_HelpPageText";

		// Token: 0x04001006 RID: 4102
		internal const string CreateUserWizardStepCollectionEditor_Caption = "CreateUserWizardStepCollectionEditor_Caption";

		// Token: 0x04001007 RID: 4103
		internal const string Wizard_ConvertToStartNavigationTemplate = "Wizard_ConvertToStartNavigationTemplate";

		// Token: 0x04001008 RID: 4104
		internal const string Wizard_ConvertToStepNavigationTemplate = "Wizard_ConvertToStepNavigationTemplate";

		// Token: 0x04001009 RID: 4105
		internal const string Wizard_ConvertToFinishNavigationTemplate = "Wizard_ConvertToFinishNavigationTemplate";

		// Token: 0x0400100A RID: 4106
		internal const string Wizard_ConvertToSideBarTemplate = "Wizard_ConvertToSideBarTemplate";

		// Token: 0x0400100B RID: 4107
		internal const string Wizard_ConvertToCustomNavigationTemplate = "Wizard_ConvertToCustomNavigationTemplate";

		// Token: 0x0400100C RID: 4108
		internal const string Wizard_ConvertToTemplateDescription = "Wizard_ConvertToTemplateDescription";

		// Token: 0x0400100D RID: 4109
		internal const string Wizard_ResetCustomNavigationTemplate = "Wizard_ResetCustomNavigationTemplate";

		// Token: 0x0400100E RID: 4110
		internal const string Wizard_ResetStartNavigationTemplate = "Wizard_ResetStartNavigationTemplate";

		// Token: 0x0400100F RID: 4111
		internal const string Wizard_ResetStepNavigationTemplate = "Wizard_ResetStepNavigationTemplate";

		// Token: 0x04001010 RID: 4112
		internal const string Wizard_ResetFinishNavigationTemplate = "Wizard_ResetFinishNavigationTemplate";

		// Token: 0x04001011 RID: 4113
		internal const string Wizard_ResetSideBarTemplate = "Wizard_ResetSideBarTemplate";

		// Token: 0x04001012 RID: 4114
		internal const string Wizard_ResetDescription = "Wizard_ResetDescription";

		// Token: 0x04001013 RID: 4115
		internal const string Wizard_StartWizardStepCollectionEditor = "Wizard_StartWizardStepCollectionEditor";

		// Token: 0x04001014 RID: 4116
		internal const string Wizard_StartWizardStepCollectionEditorDescription = "Wizard_StartWizardStepCollectionEditorDescription";

		// Token: 0x04001015 RID: 4117
		internal const string Wizard_OnViewChanged = "Wizard_OnViewChanged";

		// Token: 0x04001016 RID: 4118
		internal const string Wizard_InvalidRegion = "Wizard_InvalidRegion";

		// Token: 0x04001017 RID: 4119
		internal const string UIServiceHelper_ErrorCaption = "UIServiceHelper_ErrorCaption";

		// Token: 0x04001018 RID: 4120
		internal const string Designer_DataBindingsVerb = "Designer_DataBindingsVerb";

		// Token: 0x04001019 RID: 4121
		internal const string Designer_DataBindingsVerbDesc = "Designer_DataBindingsVerbDesc";

		// Token: 0x0400101A RID: 4122
		internal const string MdbDataFileEditor_Ellipses = "MdbDataFileEditor_Ellipses";

		// Token: 0x0400101B RID: 4123
		internal const string MdbDataFileEditor_Caption = "MdbDataFileEditor_Caption";

		// Token: 0x0400101C RID: 4124
		internal const string MdbDataFileEditor_Filter = "MdbDataFileEditor_Filter";

		// Token: 0x0400101D RID: 4125
		internal const string XmlDataFileEditor_Ellipses = "XmlDataFileEditor_Ellipses";

		// Token: 0x0400101E RID: 4126
		internal const string XmlDataFileEditor_Caption = "XmlDataFileEditor_Caption";

		// Token: 0x0400101F RID: 4127
		internal const string XmlDataFileEditor_Filter = "XmlDataFileEditor_Filter";

		// Token: 0x04001020 RID: 4128
		internal const string XsdSchemaFileEditor_Ellipses = "XsdSchemaFileEditor_Ellipses";

		// Token: 0x04001021 RID: 4129
		internal const string XsdSchemaFileEditor_Caption = "XsdSchemaFileEditor_Caption";

		// Token: 0x04001022 RID: 4130
		internal const string XsdSchemaFileEditor_Filter = "XsdSchemaFileEditor_Filter";

		// Token: 0x04001023 RID: 4131
		internal const string XslTransformFileEditor_Ellipses = "XslTransformFileEditor_Ellipses";

		// Token: 0x04001024 RID: 4132
		internal const string XslTransformFileEditor_Caption = "XslTransformFileEditor_Caption";

		// Token: 0x04001025 RID: 4133
		internal const string XslTransformFileEditor_Filter = "XslTransformFileEditor_Filter";

		// Token: 0x04001026 RID: 4134
		internal const string UserControlFileEditor_Caption = "UserControlFileEditor_Caption";

		// Token: 0x04001027 RID: 4135
		internal const string UserControlFileEditor_Filter = "UserControlFileEditor_Filter";

		// Token: 0x04001028 RID: 4136
		internal const string ConnectionStringEditor_Title = "ConnectionStringEditor_Title";

		// Token: 0x04001029 RID: 4137
		internal const string ConnectionStringEditor_HelpLabel = "ConnectionStringEditor_HelpLabel";

		// Token: 0x0400102A RID: 4138
		internal const string ConnectionStringEditor_NewConnection = "ConnectionStringEditor_NewConnection";

		// Token: 0x0400102B RID: 4139
		internal const string ConfigureDataSource_Title = "ConfigureDataSource_Title";

		// Token: 0x0400102C RID: 4140
		internal const string DataSource_DebugService_FailedCall = "DataSource_DebugService_FailedCall";

		// Token: 0x0400102D RID: 4141
		internal const string DataSource_CannotResumeEvents = "DataSource_CannotResumeEvents";

		// Token: 0x0400102E RID: 4142
		internal const string DataSource_ConfigureTransactionDescription = "DataSource_ConfigureTransactionDescription";

		// Token: 0x0400102F RID: 4143
		internal const string DataSourceDesigner_RefreshSchema = "DataSourceDesigner_RefreshSchema";

		// Token: 0x04001030 RID: 4144
		internal const string DataSourceDesigner_RefreshSchemaNoHotkey = "DataSourceDesigner_RefreshSchemaNoHotkey";

		// Token: 0x04001031 RID: 4145
		internal const string DataSourceDesigner_DataActionGroup = "DataSourceDesigner_DataActionGroup";

		// Token: 0x04001032 RID: 4146
		internal const string DataSourceDesigner_ConfigureDataSourceVerb = "DataSourceDesigner_ConfigureDataSourceVerb";

		// Token: 0x04001033 RID: 4147
		internal const string DataSourceDesigner_RefreshSchemaVerb = "DataSourceDesigner_RefreshSchemaVerb";

		// Token: 0x04001034 RID: 4148
		internal const string DataSourceDesigner_ConfigureDataSourceVerbDesc = "DataSourceDesigner_ConfigureDataSourceVerbDesc";

		// Token: 0x04001035 RID: 4149
		internal const string DataSourceDesigner_RefreshSchemaVerbDesc = "DataSourceDesigner_RefreshSchemaVerbDesc";

		// Token: 0x04001036 RID: 4150
		internal const string HierarchicalDataBoundControlDesigner_SampleRoot = "HierarchicalDataBoundControlDesigner_SampleRoot";

		// Token: 0x04001037 RID: 4151
		internal const string HierarchicalDataBoundControlDesigner_SampleParent = "HierarchicalDataBoundControlDesigner_SampleParent";

		// Token: 0x04001038 RID: 4152
		internal const string HierarchicalDataBoundControlDesigner_SampleLeaf = "HierarchicalDataBoundControlDesigner_SampleLeaf";

		// Token: 0x04001039 RID: 4153
		internal const string SqlDataSourceQueryConverter_Text = "SqlDataSourceQueryConverter_Text";

		// Token: 0x0400103A RID: 4154
		internal const string SqlDataSourceDesigner_EditQueryTransactionDescription = "SqlDataSourceDesigner_EditQueryTransactionDescription";

		// Token: 0x0400103B RID: 4155
		internal const string SqlDataSourceDesigner_DeleteQuery = "SqlDataSourceDesigner_DeleteQuery";

		// Token: 0x0400103C RID: 4156
		internal const string SqlDataSourceDesigner_InsertQuery = "SqlDataSourceDesigner_InsertQuery";

		// Token: 0x0400103D RID: 4157
		internal const string SqlDataSourceDesigner_SelectQuery = "SqlDataSourceDesigner_SelectQuery";

		// Token: 0x0400103E RID: 4158
		internal const string SqlDataSourceDesigner_SelectCountQuery = "SqlDataSourceDesigner_SelectCountQuery";

		// Token: 0x0400103F RID: 4159
		internal const string SqlDataSourceDesigner_UpdateQuery = "SqlDataSourceDesigner_UpdateQuery";

		// Token: 0x04001040 RID: 4160
		internal const string SqlDataSourceDesigner_CannotGetSchema = "SqlDataSourceDesigner_CannotGetSchema";

		// Token: 0x04001041 RID: 4161
		internal const string SqlDataSourceDesigner_CouldNotCreateConnection = "SqlDataSourceDesigner_CouldNotCreateConnection";

		// Token: 0x04001042 RID: 4162
		internal const string SqlDataSourceDesigner_NoCommand = "SqlDataSourceDesigner_NoCommand";

		// Token: 0x04001043 RID: 4163
		internal const string SqlDataSourceDesigner_InferStoredProcedureNotSupported = "SqlDataSourceDesigner_InferStoredProcedureNotSupported";

		// Token: 0x04001044 RID: 4164
		internal const string SqlDataSourceDesigner_InferStoredProcedureError = "SqlDataSourceDesigner_InferStoredProcedureError";

		// Token: 0x04001045 RID: 4165
		internal const string SqlDataSourceDesigner_RefreshSchemaRequiresSettings = "SqlDataSourceDesigner_RefreshSchemaRequiresSettings";

		// Token: 0x04001046 RID: 4166
		internal const string SqlDataSource_General_PreviewLabel = "SqlDataSource_General_PreviewLabel";

		// Token: 0x04001047 RID: 4167
		internal const string SqlDataSourceRefreshSchemaForm_Title = "SqlDataSourceRefreshSchemaForm_Title";

		// Token: 0x04001048 RID: 4168
		internal const string SqlDataSourceRefreshSchemaForm_HelpLabel = "SqlDataSourceRefreshSchemaForm_HelpLabel";

		// Token: 0x04001049 RID: 4169
		internal const string SqlDataSourceRefreshSchemaForm_ParametersLabel = "SqlDataSourceRefreshSchemaForm_ParametersLabel";

		// Token: 0x0400104A RID: 4170
		internal const string SqlDataSourceConnectionPanel_ProviderNotFound = "SqlDataSourceConnectionPanel_ProviderNotFound";

		// Token: 0x0400104B RID: 4171
		internal const string SqlDataSourceConnectionPanel_CouldNotGetConnectionSchema = "SqlDataSourceConnectionPanel_CouldNotGetConnectionSchema";

		// Token: 0x0400104C RID: 4172
		internal const string SqlDataSourceSaveConfiguredConnectionPanel_HelpLabel = "SqlDataSourceSaveConfiguredConnectionPanel_HelpLabel";

		// Token: 0x0400104D RID: 4173
		internal const string SqlDataSourceSaveConfiguredConnectionPanel_NameTextBoxDescription = "SqlDataSourceSaveConfiguredConnectionPanel_NameTextBoxDescription";

		// Token: 0x0400104E RID: 4174
		internal const string SqlDataSourceSaveConfiguredConnectionPanel_SaveLabel = "SqlDataSourceSaveConfiguredConnectionPanel_SaveLabel";

		// Token: 0x0400104F RID: 4175
		internal const string SqlDataSourceSaveConfiguredConnectionPanel_SaveCheckBox = "SqlDataSourceSaveConfiguredConnectionPanel_SaveCheckBox";

		// Token: 0x04001050 RID: 4176
		internal const string SqlDataSourceSaveConfiguredConnectionPanel_PanelCaption = "SqlDataSourceSaveConfiguredConnectionPanel_PanelCaption";

		// Token: 0x04001051 RID: 4177
		internal const string SqlDataSourceSaveConfiguredConnectionPanel_DuplicateName = "SqlDataSourceSaveConfiguredConnectionPanel_DuplicateName";

		// Token: 0x04001052 RID: 4178
		internal const string SqlDataSourceSaveConfiguredConnectionPanel_CouldNotSaveConnection = "SqlDataSourceSaveConfiguredConnectionPanel_CouldNotSaveConnection";

		// Token: 0x04001053 RID: 4179
		internal const string SqlDataSourceDataConnectionChooserPanel_PanelCaption = "SqlDataSourceDataConnectionChooserPanel_PanelCaption";

		// Token: 0x04001054 RID: 4180
		internal const string SqlDataSourceDataConnectionChooserPanel_NewConnectionButton = "SqlDataSourceDataConnectionChooserPanel_NewConnectionButton";

		// Token: 0x04001055 RID: 4181
		internal const string SqlDataSourceDataConnectionChooserPanel_ChooseLabel = "SqlDataSourceDataConnectionChooserPanel_ChooseLabel";

		// Token: 0x04001056 RID: 4182
		internal const string SqlDataSourceDataConnectionChooserPanel_ConnectionStringLabel = "SqlDataSourceDataConnectionChooserPanel_ConnectionStringLabel";

		// Token: 0x04001057 RID: 4183
		internal const string SqlDataSourceDataConnectionChooserPanel_CustomConnectionName = "SqlDataSourceDataConnectionChooserPanel_CustomConnectionName";

		// Token: 0x04001058 RID: 4184
		internal const string SqlDataSourceDataConnectionChooserPanel_DetailsButtonName = "SqlDataSourceDataConnectionChooserPanel_DetailsButtonName";

		// Token: 0x04001059 RID: 4185
		internal const string SqlDataSourceDataConnectionChooserPanel_DetailsButtonDesc = "SqlDataSourceDataConnectionChooserPanel_DetailsButtonDesc";

		// Token: 0x0400105A RID: 4186
		internal const string SqlDataSourceQueryEditorForm_CommandLabel = "SqlDataSourceQueryEditorForm_CommandLabel";

		// Token: 0x0400105B RID: 4187
		internal const string SqlDataSourceQueryEditorForm_InferParametersButton = "SqlDataSourceQueryEditorForm_InferParametersButton";

		// Token: 0x0400105C RID: 4188
		internal const string SqlDataSourceQueryEditorForm_QueryBuilderButton = "SqlDataSourceQueryEditorForm_QueryBuilderButton";

		// Token: 0x0400105D RID: 4189
		internal const string SqlDataSourceQueryEditorForm_Caption = "SqlDataSourceQueryEditorForm_Caption";

		// Token: 0x0400105E RID: 4190
		internal const string SqlDataSourceQueryEditorForm_InferNeedsCommand = "SqlDataSourceQueryEditorForm_InferNeedsCommand";

		// Token: 0x0400105F RID: 4191
		internal const string SqlDataSourceQueryEditorForm_QueryBuilderNeedsConnectionString = "SqlDataSourceQueryEditorForm_QueryBuilderNeedsConnectionString";

		// Token: 0x04001060 RID: 4192
		internal const string SqlDataSourceConfigureFilterForm_ColumnLabel = "SqlDataSourceConfigureFilterForm_ColumnLabel";

		// Token: 0x04001061 RID: 4193
		internal const string SqlDataSourceConfigureFilterForm_OperatorLabel = "SqlDataSourceConfigureFilterForm_OperatorLabel";

		// Token: 0x04001062 RID: 4194
		internal const string SqlDataSourceConfigureFilterForm_ExpressionLabel = "SqlDataSourceConfigureFilterForm_ExpressionLabel";

		// Token: 0x04001063 RID: 4195
		internal const string SqlDataSourceConfigureFilterForm_ValueLabel = "SqlDataSourceConfigureFilterForm_ValueLabel";

		// Token: 0x04001064 RID: 4196
		internal const string SqlDataSourceConfigureFilterForm_ExpressionColumnHeader = "SqlDataSourceConfigureFilterForm_ExpressionColumnHeader";

		// Token: 0x04001065 RID: 4197
		internal const string SqlDataSourceConfigureFilterForm_ValueColumnHeader = "SqlDataSourceConfigureFilterForm_ValueColumnHeader";

		// Token: 0x04001066 RID: 4198
		internal const string SqlDataSourceConfigureFilterForm_ParameterPropertiesGroup = "SqlDataSourceConfigureFilterForm_ParameterPropertiesGroup";

		// Token: 0x04001067 RID: 4199
		internal const string SqlDataSourceConfigureFilterForm_SourceLabel = "SqlDataSourceConfigureFilterForm_SourceLabel";

		// Token: 0x04001068 RID: 4200
		internal const string SqlDataSourceConfigureFilterForm_WhereLabel = "SqlDataSourceConfigureFilterForm_WhereLabel";

		// Token: 0x04001069 RID: 4201
		internal const string SqlDataSourceConfigureFilterForm_AddButton = "SqlDataSourceConfigureFilterForm_AddButton";

		// Token: 0x0400106A RID: 4202
		internal const string SqlDataSourceConfigureFilterForm_HelpLabel = "SqlDataSourceConfigureFilterForm_HelpLabel";

		// Token: 0x0400106B RID: 4203
		internal const string SqlDataSourceConfigureFilterForm_RemoveButton = "SqlDataSourceConfigureFilterForm_RemoveButton";

		// Token: 0x0400106C RID: 4204
		internal const string SqlDataSourceConfigureFilterForm_Caption = "SqlDataSourceConfigureFilterForm_Caption";

		// Token: 0x0400106D RID: 4205
		internal const string SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue = "SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue";

		// Token: 0x0400106E RID: 4206
		internal const string SqlDataSourceConfigureFilterForm_StaticParameterEditor_ValueLabel = "SqlDataSourceConfigureFilterForm_StaticParameterEditor_ValueLabel";

		// Token: 0x0400106F RID: 4207
		internal const string SqlDataSourceConfigureFilterForm_CookieParameterEditor_CookieNameLabel = "SqlDataSourceConfigureFilterForm_CookieParameterEditor_CookieNameLabel";

		// Token: 0x04001070 RID: 4208
		internal const string SqlDataSourceConfigureFilterForm_ControlParameterEditor_ControlIDLabel = "SqlDataSourceConfigureFilterForm_ControlParameterEditor_ControlIDLabel";

		// Token: 0x04001071 RID: 4209
		internal const string SqlDataSourceConfigureFilterForm_FormParameterEditor_FormFieldLabel = "SqlDataSourceConfigureFilterForm_FormParameterEditor_FormFieldLabel";

		// Token: 0x04001072 RID: 4210
		internal const string SqlDataSourceConfigureFilterForm_QueryStringParameterEditor_QueryStringFieldLabel = "SqlDataSourceConfigureFilterForm_QueryStringParameterEditor_QueryStringFieldLabel";

		// Token: 0x04001073 RID: 4211
		internal const string SqlDataSourceConfigureFilterForm_RouteParameterEditor_RouteKeyLabel = "SqlDataSourceConfigureFilterForm_RouteParameterEditor_RouteKeyLabel";

		// Token: 0x04001074 RID: 4212
		internal const string SqlDataSourceConfigureFilterForm_SessionParameterEditor_SessionFieldLabel = "SqlDataSourceConfigureFilterForm_SessionParameterEditor_SessionFieldLabel";

		// Token: 0x04001075 RID: 4213
		internal const string SqlDataSourceConfigureFilterForm_ProfileParameterEditor_PropertyNameLabel = "SqlDataSourceConfigureFilterForm_ProfileParameterEditor_PropertyNameLabel";

		// Token: 0x04001076 RID: 4214
		internal const string SqlDataSourceConfigureSortForm_HelpLabel = "SqlDataSourceConfigureSortForm_HelpLabel";

		// Token: 0x04001077 RID: 4215
		internal const string SqlDataSourceConfigureSortForm_SortByLabel = "SqlDataSourceConfigureSortForm_SortByLabel";

		// Token: 0x04001078 RID: 4216
		internal const string SqlDataSourceConfigureSortForm_ThenByLabel = "SqlDataSourceConfigureSortForm_ThenByLabel";

		// Token: 0x04001079 RID: 4217
		internal const string SqlDataSourceConfigureSortForm_AscendingLabel = "SqlDataSourceConfigureSortForm_AscendingLabel";

		// Token: 0x0400107A RID: 4218
		internal const string SqlDataSourceConfigureSortForm_DescendingLabel = "SqlDataSourceConfigureSortForm_DescendingLabel";

		// Token: 0x0400107B RID: 4219
		internal const string SqlDataSourceConfigureSortForm_Caption = "SqlDataSourceConfigureSortForm_Caption";

		// Token: 0x0400107C RID: 4220
		internal const string SqlDataSourceConfigureSortForm_SortDirection1 = "SqlDataSourceConfigureSortForm_SortDirection1";

		// Token: 0x0400107D RID: 4221
		internal const string SqlDataSourceConfigureSortForm_SortDirection2 = "SqlDataSourceConfigureSortForm_SortDirection2";

		// Token: 0x0400107E RID: 4222
		internal const string SqlDataSourceConfigureSortForm_SortDirection3 = "SqlDataSourceConfigureSortForm_SortDirection3";

		// Token: 0x0400107F RID: 4223
		internal const string SqlDataSourceConfigureSortForm_SortColumn1 = "SqlDataSourceConfigureSortForm_SortColumn1";

		// Token: 0x04001080 RID: 4224
		internal const string SqlDataSourceConfigureSortForm_SortColumn2 = "SqlDataSourceConfigureSortForm_SortColumn2";

		// Token: 0x04001081 RID: 4225
		internal const string SqlDataSourceConfigureSortForm_SortColumn3 = "SqlDataSourceConfigureSortForm_SortColumn3";

		// Token: 0x04001082 RID: 4226
		internal const string SqlDataSourceConfigureSortForm_SortNone = "SqlDataSourceConfigureSortForm_SortNone";

		// Token: 0x04001083 RID: 4227
		internal const string SqlDataSourceConfigureParametersPanel_PanelCaption = "SqlDataSourceConfigureParametersPanel_PanelCaption";

		// Token: 0x04001084 RID: 4228
		internal const string SqlDataSourceConfigureParametersPanel_HelpLabel = "SqlDataSourceConfigureParametersPanel_HelpLabel";

		// Token: 0x04001085 RID: 4229
		internal const string SqlDataSourceSummaryPanel_PanelCaption = "SqlDataSourceSummaryPanel_PanelCaption";

		// Token: 0x04001086 RID: 4230
		internal const string SqlDataSourceSummaryPanel_TestQueryButton = "SqlDataSourceSummaryPanel_TestQueryButton";

		// Token: 0x04001087 RID: 4231
		internal const string SqlDataSourceSummaryPanel_HelpLabel = "SqlDataSourceSummaryPanel_HelpLabel";

		// Token: 0x04001088 RID: 4232
		internal const string SqlDataSourceSummaryPanel_ResultsAccessibleName = "SqlDataSourceSummaryPanel_ResultsAccessibleName";

		// Token: 0x04001089 RID: 4233
		internal const string SqlDataSourceSummaryPanel_CouldNotCreateConnection = "SqlDataSourceSummaryPanel_CouldNotCreateConnection";

		// Token: 0x0400108A RID: 4234
		internal const string SqlDataSourceSummaryPanel_CannotExecuteQueryNoTables = "SqlDataSourceSummaryPanel_CannotExecuteQueryNoTables";

		// Token: 0x0400108B RID: 4235
		internal const string SqlDataSourceSummaryPanel_CannotExecuteQuery = "SqlDataSourceSummaryPanel_CannotExecuteQuery";

		// Token: 0x0400108C RID: 4236
		internal const string SqlDataSourceConfigureSelectPanel_PanelCaption = "SqlDataSourceConfigureSelectPanel_PanelCaption";

		// Token: 0x0400108D RID: 4237
		internal const string SqlDataSourceConfigureSelectPanel_RetrieveDataLabel = "SqlDataSourceConfigureSelectPanel_RetrieveDataLabel";

		// Token: 0x0400108E RID: 4238
		internal const string SqlDataSourceConfigureSelectPanel_TableLabel = "SqlDataSourceConfigureSelectPanel_TableLabel";

		// Token: 0x0400108F RID: 4239
		internal const string SqlDataSourceConfigureSelectPanel_CustomSqlLabel = "SqlDataSourceConfigureSelectPanel_CustomSqlLabel";

		// Token: 0x04001090 RID: 4240
		internal const string SqlDataSourceConfigureSelectPanel_TableNameLabel = "SqlDataSourceConfigureSelectPanel_TableNameLabel";

		// Token: 0x04001091 RID: 4241
		internal const string SqlDataSourceConfigureSelectPanel_FieldsLabel = "SqlDataSourceConfigureSelectPanel_FieldsLabel";

		// Token: 0x04001092 RID: 4242
		internal const string SqlDataSourceConfigureSelectPanel_SortButton = "SqlDataSourceConfigureSelectPanel_SortButton";

		// Token: 0x04001093 RID: 4243
		internal const string SqlDataSourceConfigureSelectPanel_FilterLabel = "SqlDataSourceConfigureSelectPanel_FilterLabel";

		// Token: 0x04001094 RID: 4244
		internal const string SqlDataSourceConfigureSelectPanel_SelectDistinctLabel = "SqlDataSourceConfigureSelectPanel_SelectDistinctLabel";

		// Token: 0x04001095 RID: 4245
		internal const string SqlDataSourceConfigureSelectPanel_AdvancedOptions = "SqlDataSourceConfigureSelectPanel_AdvancedOptions";

		// Token: 0x04001096 RID: 4246
		internal const string SqlDataSourceConfigureSelectPanel_CouldNotGetTableSchema = "SqlDataSourceConfigureSelectPanel_CouldNotGetTableSchema";

		// Token: 0x04001097 RID: 4247
		internal const string SqlDataSourceAdvancedOptionsForm_HelpLabel = "SqlDataSourceAdvancedOptionsForm_HelpLabel";

		// Token: 0x04001098 RID: 4248
		internal const string SqlDataSourceAdvancedOptionsForm_GenerateCheckBox = "SqlDataSourceAdvancedOptionsForm_GenerateCheckBox";

		// Token: 0x04001099 RID: 4249
		internal const string SqlDataSourceAdvancedOptionsForm_GenerateHelpLabel = "SqlDataSourceAdvancedOptionsForm_GenerateHelpLabel";

		// Token: 0x0400109A RID: 4250
		internal const string SqlDataSourceAdvancedOptionsForm_OptimisticCheckBox = "SqlDataSourceAdvancedOptionsForm_OptimisticCheckBox";

		// Token: 0x0400109B RID: 4251
		internal const string SqlDataSourceAdvancedOptionsForm_OptimisticLabel = "SqlDataSourceAdvancedOptionsForm_OptimisticLabel";

		// Token: 0x0400109C RID: 4252
		internal const string SqlDataSourceAdvancedOptionsForm_Caption = "SqlDataSourceAdvancedOptionsForm_Caption";

		// Token: 0x0400109D RID: 4253
		internal const string SqlDataSourceCustomCommandEditor_QueryBuilderButton = "SqlDataSourceCustomCommandEditor_QueryBuilderButton";

		// Token: 0x0400109E RID: 4254
		internal const string SqlDataSourceCustomCommandEditor_SqlLabel = "SqlDataSourceCustomCommandEditor_SqlLabel";

		// Token: 0x0400109F RID: 4255
		internal const string SqlDataSourceCustomCommandEditor_StoredProcedureLabel = "SqlDataSourceCustomCommandEditor_StoredProcedureLabel";

		// Token: 0x040010A0 RID: 4256
		internal const string SqlDataSourceCustomCommandEditor_NoConnectionString = "SqlDataSourceCustomCommandEditor_NoConnectionString";

		// Token: 0x040010A1 RID: 4257
		internal const string SqlDataSourceCustomCommandEditor_CouldNotGetStoredProcedureSchema = "SqlDataSourceCustomCommandEditor_CouldNotGetStoredProcedureSchema";

		// Token: 0x040010A2 RID: 4258
		internal const string SqlDataSourceCustomCommandPanel_HelpLabel = "SqlDataSourceCustomCommandPanel_HelpLabel";

		// Token: 0x040010A3 RID: 4259
		internal const string SqlDataSourceCustomCommandPanel_PanelCaption = "SqlDataSourceCustomCommandPanel_PanelCaption";

		// Token: 0x040010A4 RID: 4260
		internal const string SqlDataSourceParameterValueEditorForm_HelpLabel = "SqlDataSourceParameterValueEditorForm_HelpLabel";

		// Token: 0x040010A5 RID: 4261
		internal const string SqlDataSourceParameterValueEditorForm_ParametersGridAccessibleName = "SqlDataSourceParameterValueEditorForm_ParametersGridAccessibleName";

		// Token: 0x040010A6 RID: 4262
		internal const string SqlDataSourceParameterValueEditorForm_Caption = "SqlDataSourceParameterValueEditorForm_Caption";

		// Token: 0x040010A7 RID: 4263
		internal const string SqlDataSourceParameterValueEditorForm_DbTypeColumnHeader = "SqlDataSourceParameterValueEditorForm_DbTypeColumnHeader";

		// Token: 0x040010A8 RID: 4264
		internal const string SqlDataSourceParameterValueEditorForm_ParameterColumnHeader = "SqlDataSourceParameterValueEditorForm_ParameterColumnHeader";

		// Token: 0x040010A9 RID: 4265
		internal const string SqlDataSourceParameterValueEditorForm_TypeColumnHeader = "SqlDataSourceParameterValueEditorForm_TypeColumnHeader";

		// Token: 0x040010AA RID: 4266
		internal const string SqlDataSourceParameterValueEditorForm_ValueColumnHeader = "SqlDataSourceParameterValueEditorForm_ValueColumnHeader";

		// Token: 0x040010AB RID: 4267
		internal const string SqlDataSourceParameterValueEditorForm_InvalidParameter = "SqlDataSourceParameterValueEditorForm_InvalidParameter";

		// Token: 0x040010AC RID: 4268
		internal const string AccessDataSourceConnectionChooserPanel_PanelCaption = "AccessDataSourceConnectionChooserPanel_PanelCaption";

		// Token: 0x040010AD RID: 4269
		internal const string AccessDataSourceConnectionChooserPanel_DataFileLabel = "AccessDataSourceConnectionChooserPanel_DataFileLabel";

		// Token: 0x040010AE RID: 4270
		internal const string AccessDataSourceConnectionChooserPanel_HelpLabel = "AccessDataSourceConnectionChooserPanel_HelpLabel";

		// Token: 0x040010AF RID: 4271
		internal const string AccessDataSourceConnectionChooserPanel_BrowseButton = "AccessDataSourceConnectionChooserPanel_BrowseButton";

		// Token: 0x040010B0 RID: 4272
		internal const string AccessDataSourceConnectionChooserPanel_FileNotFound = "AccessDataSourceConnectionChooserPanel_FileNotFound";

		// Token: 0x040010B1 RID: 4273
		internal const string XmlDataSourceConfigureDataSourceForm_HelpLabel = "XmlDataSourceConfigureDataSourceForm_HelpLabel";

		// Token: 0x040010B2 RID: 4274
		internal const string XmlDataSourceConfigureDataSourceForm_DataFileLabel = "XmlDataSourceConfigureDataSourceForm_DataFileLabel";

		// Token: 0x040010B3 RID: 4275
		internal const string XmlDataSourceConfigureDataSourceForm_TransformFileLabel = "XmlDataSourceConfigureDataSourceForm_TransformFileLabel";

		// Token: 0x040010B4 RID: 4276
		internal const string XmlDataSourceConfigureDataSourceForm_TransformFileHelpLabel = "XmlDataSourceConfigureDataSourceForm_TransformFileHelpLabel";

		// Token: 0x040010B5 RID: 4277
		internal const string XmlDataSourceConfigureDataSourceForm_XPathExpressionLabel = "XmlDataSourceConfigureDataSourceForm_XPathExpressionLabel";

		// Token: 0x040010B6 RID: 4278
		internal const string XmlDataSourceConfigureDataSourceForm_XPathExpressionHelpLabel = "XmlDataSourceConfigureDataSourceForm_XPathExpressionHelpLabel";

		// Token: 0x040010B7 RID: 4279
		internal const string XmlDataSourceConfigureDataSourceForm_Browse = "XmlDataSourceConfigureDataSourceForm_Browse";

		// Token: 0x040010B8 RID: 4280
		internal const string ObjectDataSourceDesigner_CannotGetSchema = "ObjectDataSourceDesigner_CannotGetSchema";

		// Token: 0x040010B9 RID: 4281
		internal const string ObjectDataSourceDesigner_CannotGetType = "ObjectDataSourceDesigner_CannotGetType";

		// Token: 0x040010BA RID: 4282
		internal const string ObjectDataSource_General_MethodSignatureLabel = "ObjectDataSource_General_MethodSignatureLabel";

		// Token: 0x040010BB RID: 4283
		internal const string ObjectDataSourceConfigureParametersPanel_PanelCaption = "ObjectDataSourceConfigureParametersPanel_PanelCaption";

		// Token: 0x040010BC RID: 4284
		internal const string ObjectDataSourceConfigureParametersPanel_HelpLabel = "ObjectDataSourceConfigureParametersPanel_HelpLabel";

		// Token: 0x040010BD RID: 4285
		internal const string ObjectDataSourceChooseMethodsPanel_PanelCaption = "ObjectDataSourceChooseMethodsPanel_PanelCaption";

		// Token: 0x040010BE RID: 4286
		internal const string ObjectDataSourceChooseMethodsPanel_IncompatibleDataObjectTypes = "ObjectDataSourceChooseMethodsPanel_IncompatibleDataObjectTypes";

		// Token: 0x040010BF RID: 4287
		internal const string ObjectDataSourceMethodEditor_DeleteHelpLabel = "ObjectDataSourceMethodEditor_DeleteHelpLabel";

		// Token: 0x040010C0 RID: 4288
		internal const string ObjectDataSourceMethodEditor_InsertHelpLabel = "ObjectDataSourceMethodEditor_InsertHelpLabel";

		// Token: 0x040010C1 RID: 4289
		internal const string ObjectDataSourceMethodEditor_SelectHelpLabel = "ObjectDataSourceMethodEditor_SelectHelpLabel";

		// Token: 0x040010C2 RID: 4290
		internal const string ObjectDataSourceMethodEditor_UpdateHelpLabel = "ObjectDataSourceMethodEditor_UpdateHelpLabel";

		// Token: 0x040010C3 RID: 4291
		internal const string ObjectDataSourceMethodEditor_MethodLabel = "ObjectDataSourceMethodEditor_MethodLabel";

		// Token: 0x040010C4 RID: 4292
		internal const string ObjectDataSourceMethodEditor_SignatureFormat = "ObjectDataSourceMethodEditor_SignatureFormat";

		// Token: 0x040010C5 RID: 4293
		internal const string ObjectDataSourceMethodEditor_NoMethod = "ObjectDataSourceMethodEditor_NoMethod";

		// Token: 0x040010C6 RID: 4294
		internal const string ObjectDataSourceChooseTypePanel_PanelCaption = "ObjectDataSourceChooseTypePanel_PanelCaption";

		// Token: 0x040010C7 RID: 4295
		internal const string ObjectDataSourceChooseTypePanel_HelpLabel = "ObjectDataSourceChooseTypePanel_HelpLabel";

		// Token: 0x040010C8 RID: 4296
		internal const string ObjectDataSourceChooseTypePanel_NameLabel = "ObjectDataSourceChooseTypePanel_NameLabel";

		// Token: 0x040010C9 RID: 4297
		internal const string ObjectDataSourceChooseTypePanel_ExampleLabel = "ObjectDataSourceChooseTypePanel_ExampleLabel";

		// Token: 0x040010CA RID: 4298
		internal const string ObjectDataSourceChooseTypePanel_FilterCheckBox = "ObjectDataSourceChooseTypePanel_FilterCheckBox";

		// Token: 0x040010CB RID: 4299
		internal const string ParameterCollectionEditor_InvalidParameters = "ParameterCollectionEditor_InvalidParameters";

		// Token: 0x040010CC RID: 4300
		internal const string ParameterCollectionEditorForm_Caption = "ParameterCollectionEditorForm_Caption";

		// Token: 0x040010CD RID: 4301
		internal const string ParameterEditorUserControl_ParametersLabel = "ParameterEditorUserControl_ParametersLabel";

		// Token: 0x040010CE RID: 4302
		internal const string ParameterEditorUserControl_PropertiesLabel = "ParameterEditorUserControl_PropertiesLabel";

		// Token: 0x040010CF RID: 4303
		internal const string ParameterEditorUserControl_AddButton = "ParameterEditorUserControl_AddButton";

		// Token: 0x040010D0 RID: 4304
		internal const string ParameterEditorUserControl_SourceLabel = "ParameterEditorUserControl_SourceLabel";

		// Token: 0x040010D1 RID: 4305
		internal const string ParameterEditorUserControl_ParameterNameColumnHeader = "ParameterEditorUserControl_ParameterNameColumnHeader";

		// Token: 0x040010D2 RID: 4306
		internal const string ParameterEditorUserControl_ParameterValueColumnHeader = "ParameterEditorUserControl_ParameterValueColumnHeader";

		// Token: 0x040010D3 RID: 4307
		internal const string ParameterEditorUserControl_MoveParameterUp = "ParameterEditorUserControl_MoveParameterUp";

		// Token: 0x040010D4 RID: 4308
		internal const string ParameterEditorUserControl_MoveParameterDown = "ParameterEditorUserControl_MoveParameterDown";

		// Token: 0x040010D5 RID: 4309
		internal const string ParameterEditorUserControl_DeleteParameter = "ParameterEditorUserControl_DeleteParameter";

		// Token: 0x040010D6 RID: 4310
		internal const string ParameterEditorUserControl_ControlParameterExpressionUnknown = "ParameterEditorUserControl_ControlParameterExpressionUnknown";

		// Token: 0x040010D7 RID: 4311
		internal const string ParameterEditorUserControl_CookieParameterExpressionUnknown = "ParameterEditorUserControl_CookieParameterExpressionUnknown";

		// Token: 0x040010D8 RID: 4312
		internal const string ParameterEditorUserControl_FormParameterExpressionUnknown = "ParameterEditorUserControl_FormParameterExpressionUnknown";

		// Token: 0x040010D9 RID: 4313
		internal const string ParameterEditorUserControl_QueryStringParameterExpressionUnknown = "ParameterEditorUserControl_QueryStringParameterExpressionUnknown";

		// Token: 0x040010DA RID: 4314
		internal const string ParameterEditorUserControl_SessionParameterExpressionUnknown = "ParameterEditorUserControl_SessionParameterExpressionUnknown";

		// Token: 0x040010DB RID: 4315
		internal const string ParameterEditorUserControl_ProfileParameterExpressionUnknown = "ParameterEditorUserControl_ProfileParameterExpressionUnknown";

		// Token: 0x040010DC RID: 4316
		internal const string ParameterEditorUserControl_RouteParameterExpressionUnknown = "ParameterEditorUserControl_RouteParameterExpressionUnknown";

		// Token: 0x040010DD RID: 4317
		internal const string ParameterEditorUserControl_ShowAdvancedProperties = "ParameterEditorUserControl_ShowAdvancedProperties";

		// Token: 0x040010DE RID: 4318
		internal const string ParameterEditorUserControl_HideAdvancedPropertiesLabel = "ParameterEditorUserControl_HideAdvancedPropertiesLabel";

		// Token: 0x040010DF RID: 4319
		internal const string ParameterEditorUserControl_AdvancedProperties = "ParameterEditorUserControl_AdvancedProperties";

		// Token: 0x040010E0 RID: 4320
		internal const string ParameterEditorUserControl_ParameterDefaultValue = "ParameterEditorUserControl_ParameterDefaultValue";

		// Token: 0x040010E1 RID: 4321
		internal const string ParameterEditorUserControl_ControlParameterControlID = "ParameterEditorUserControl_ControlParameterControlID";

		// Token: 0x040010E2 RID: 4322
		internal const string ParameterEditorUserControl_CookieParameterCookieName = "ParameterEditorUserControl_CookieParameterCookieName";

		// Token: 0x040010E3 RID: 4323
		internal const string ParameterEditorUserControl_FormParameterFormField = "ParameterEditorUserControl_FormParameterFormField";

		// Token: 0x040010E4 RID: 4324
		internal const string ParameterEditorUserControl_SessionParameterSessionField = "ParameterEditorUserControl_SessionParameterSessionField";

		// Token: 0x040010E5 RID: 4325
		internal const string ParameterEditorUserControl_QueryStringParameterQueryStringField = "ParameterEditorUserControl_QueryStringParameterQueryStringField";

		// Token: 0x040010E6 RID: 4326
		internal const string ParameterEditorUserControl_ProfilePropertyName = "ParameterEditorUserControl_ProfilePropertyName";

		// Token: 0x040010E7 RID: 4327
		internal const string ParameterEditorUserControl_RouteParameterRouteKey = "ParameterEditorUserControl_RouteParameterRouteKey";

		// Token: 0x040010E8 RID: 4328
		internal const string DBDlg_Text = "DBDlg_Text";

		// Token: 0x040010E9 RID: 4329
		internal const string DBDlg_Inst = "DBDlg_Inst";

		// Token: 0x040010EA RID: 4330
		internal const string DBDlg_BindableProps = "DBDlg_BindableProps";

		// Token: 0x040010EB RID: 4331
		internal const string DBDlg_ShowAll = "DBDlg_ShowAll";

		// Token: 0x040010EC RID: 4332
		internal const string DBDlg_TwoWay = "DBDlg_TwoWay";

		// Token: 0x040010ED RID: 4333
		internal const string DBDlg_OK = "DBDlg_OK";

		// Token: 0x040010EE RID: 4334
		internal const string DBDlg_Cancel = "DBDlg_Cancel";

		// Token: 0x040010EF RID: 4335
		internal const string DBDlg_Help = "DBDlg_Help";

		// Token: 0x040010F0 RID: 4336
		internal const string DBDlg_BindingGroup = "DBDlg_BindingGroup";

		// Token: 0x040010F1 RID: 4337
		internal const string DBDlg_FieldBinding = "DBDlg_FieldBinding";

		// Token: 0x040010F2 RID: 4338
		internal const string DBDlg_Field = "DBDlg_Field";

		// Token: 0x040010F3 RID: 4339
		internal const string DBDlg_Format = "DBDlg_Format";

		// Token: 0x040010F4 RID: 4340
		internal const string DBDlg_Sample = "DBDlg_Sample";

		// Token: 0x040010F5 RID: 4341
		internal const string DBDlg_CustomBinding = "DBDlg_CustomBinding";

		// Token: 0x040010F6 RID: 4342
		internal const string DBDlg_Expr = "DBDlg_Expr";

		// Token: 0x040010F7 RID: 4343
		internal const string DBDlg_RefreshSchema = "DBDlg_RefreshSchema";

		// Token: 0x040010F8 RID: 4344
		internal const string DBDlg_Unbound = "DBDlg_Unbound";

		// Token: 0x040010F9 RID: 4345
		internal const string DBDlg_Fmt_None = "DBDlg_Fmt_None";

		// Token: 0x040010FA RID: 4346
		internal const string DBDlg_Fmt_General = "DBDlg_Fmt_General";

		// Token: 0x040010FB RID: 4347
		internal const string DBDlg_Fmt_ShortDate = "DBDlg_Fmt_ShortDate";

		// Token: 0x040010FC RID: 4348
		internal const string DBDlg_Fmt_LongDate = "DBDlg_Fmt_LongDate";

		// Token: 0x040010FD RID: 4349
		internal const string DBDlg_Fmt_ShortTime = "DBDlg_Fmt_ShortTime";

		// Token: 0x040010FE RID: 4350
		internal const string DBDlg_Fmt_LongTime = "DBDlg_Fmt_LongTime";

		// Token: 0x040010FF RID: 4351
		internal const string DBDlg_Fmt_DateTime = "DBDlg_Fmt_DateTime";

		// Token: 0x04001100 RID: 4352
		internal const string DBDlg_Fmt_FullDate = "DBDlg_Fmt_FullDate";

		// Token: 0x04001101 RID: 4353
		internal const string DBDlg_Fmt_Decimal = "DBDlg_Fmt_Decimal";

		// Token: 0x04001102 RID: 4354
		internal const string DBDlg_Fmt_Numeric = "DBDlg_Fmt_Numeric";

		// Token: 0x04001103 RID: 4355
		internal const string DBDlg_Fmt_Fixed = "DBDlg_Fmt_Fixed";

		// Token: 0x04001104 RID: 4356
		internal const string DBDlg_Fmt_Currency = "DBDlg_Fmt_Currency";

		// Token: 0x04001105 RID: 4357
		internal const string DBDlg_Fmt_Scientific = "DBDlg_Fmt_Scientific";

		// Token: 0x04001106 RID: 4358
		internal const string DBDlg_Fmt_Hexadecimal = "DBDlg_Fmt_Hexadecimal";

		// Token: 0x04001107 RID: 4359
		internal const string DBDlg_InvalidFormat = "DBDlg_InvalidFormat";

		// Token: 0x04001108 RID: 4360
		internal const string ExpressionBindingsDialog_Text = "ExpressionBindingsDialog_Text";

		// Token: 0x04001109 RID: 4361
		internal const string ExpressionBindingsDialog_None = "ExpressionBindingsDialog_None";

		// Token: 0x0400110A RID: 4362
		internal const string ExpressionBindingsDialog_Inst = "ExpressionBindingsDialog_Inst";

		// Token: 0x0400110B RID: 4363
		internal const string ExpressionBindingsDialog_BindableProps = "ExpressionBindingsDialog_BindableProps";

		// Token: 0x0400110C RID: 4364
		internal const string ExpressionBindingsDialog_OK = "ExpressionBindingsDialog_OK";

		// Token: 0x0400110D RID: 4365
		internal const string ExpressionBindingsDialog_Cancel = "ExpressionBindingsDialog_Cancel";

		// Token: 0x0400110E RID: 4366
		internal const string ExpressionBindingsDialog_ExpressionType = "ExpressionBindingsDialog_ExpressionType";

		// Token: 0x0400110F RID: 4367
		internal const string ExpressionBindingsDialog_Properties = "ExpressionBindingsDialog_Properties";

		// Token: 0x04001110 RID: 4368
		internal const string ExpressionBindingsDialog_UndefinedExpressionPrefix = "ExpressionBindingsDialog_UndefinedExpressionPrefix";

		// Token: 0x04001111 RID: 4369
		internal const string ExpressionBindingsDialog_GeneratedExpression = "ExpressionBindingsDialog_GeneratedExpression";

		// Token: 0x04001112 RID: 4370
		internal const string BDL_PrivateDataSource = "BDL_PrivateDataSource";

		// Token: 0x04001113 RID: 4371
		internal const string BDL_PrivateDataSourceT = "BDL_PrivateDataSourceT";

		// Token: 0x04001114 RID: 4372
		internal const string BDL_TemplateModePropBuilder = "BDL_TemplateModePropBuilder";

		// Token: 0x04001115 RID: 4373
		internal const string BDL_PropertyBuilder = "BDL_PropertyBuilder";

		// Token: 0x04001116 RID: 4374
		internal const string BDL_PropertyBuilderVerb = "BDL_PropertyBuilderVerb";

		// Token: 0x04001117 RID: 4375
		internal const string BDL_PropertyBuilderDesc = "BDL_PropertyBuilderDesc";

		// Token: 0x04001118 RID: 4376
		internal const string BDL_BehaviorGroup = "BDL_BehaviorGroup";

		// Token: 0x04001119 RID: 4377
		internal const string BDLAF_Title = "BDLAF_Title";

		// Token: 0x0400111A RID: 4378
		internal const string BDLAF_SchemeName = "BDLAF_SchemeName";

		// Token: 0x0400111B RID: 4379
		internal const string BDLAF_Preview = "BDLAF_Preview";

		// Token: 0x0400111C RID: 4380
		internal const string BDLAF_OK = "BDLAF_OK";

		// Token: 0x0400111D RID: 4381
		internal const string BDLAF_Cancel = "BDLAF_Cancel";

		// Token: 0x0400111E RID: 4382
		internal const string BDLAF_Help = "BDLAF_Help";

		// Token: 0x0400111F RID: 4383
		internal const string BDLAF_Column1 = "BDLAF_Column1";

		// Token: 0x04001120 RID: 4384
		internal const string BDLAF_Column2 = "BDLAF_Column2";

		// Token: 0x04001121 RID: 4385
		internal const string BDLAF_Header = "BDLAF_Header";

		// Token: 0x04001122 RID: 4386
		internal const string BDLAF_Footer = "BDLAF_Footer";

		// Token: 0x04001123 RID: 4387
		internal const string BDLAF_Apply = "BDLAF_Apply";

		// Token: 0x04001124 RID: 4388
		internal const string BDLAF_AutoFormats = "BDLAF_AutoFormats";

		// Token: 0x04001125 RID: 4389
		internal const string BDLAF_Skins = "BDLAF_Skins";

		// Token: 0x04001126 RID: 4390
		internal const string BDLAF_DefaultSkin = "BDLAF_DefaultSkin";

		// Token: 0x04001127 RID: 4391
		internal const string BDLAF_NoSkin = "BDLAF_NoSkin";

		// Token: 0x04001128 RID: 4392
		internal const string BDLAF_Couldnotgeneratepreview = "BDLAF_Couldnotgeneratepreview";

		// Token: 0x04001129 RID: 4393
		internal const string BDLAF_RemoveFormatting = "BDLAF_RemoveFormatting";

		// Token: 0x0400112A RID: 4394
		internal const string BDLScheme_Empty = "BDLScheme_Empty";

		// Token: 0x0400112B RID: 4395
		internal const string BDLScheme_Colorful1 = "BDLScheme_Colorful1";

		// Token: 0x0400112C RID: 4396
		internal const string BDLScheme_Colorful2 = "BDLScheme_Colorful2";

		// Token: 0x0400112D RID: 4397
		internal const string BDLScheme_Colorful3 = "BDLScheme_Colorful3";

		// Token: 0x0400112E RID: 4398
		internal const string BDLScheme_Colorful4 = "BDLScheme_Colorful4";

		// Token: 0x0400112F RID: 4399
		internal const string BDLScheme_Colorful5 = "BDLScheme_Colorful5";

		// Token: 0x04001130 RID: 4400
		internal const string BDLScheme_Professional1 = "BDLScheme_Professional1";

		// Token: 0x04001131 RID: 4401
		internal const string BDLScheme_Professional2 = "BDLScheme_Professional2";

		// Token: 0x04001132 RID: 4402
		internal const string BDLScheme_Professional3 = "BDLScheme_Professional3";

		// Token: 0x04001133 RID: 4403
		internal const string BDLScheme_Simple1 = "BDLScheme_Simple1";

		// Token: 0x04001134 RID: 4404
		internal const string BDLScheme_Simple2 = "BDLScheme_Simple2";

		// Token: 0x04001135 RID: 4405
		internal const string BDLScheme_Simple3 = "BDLScheme_Simple3";

		// Token: 0x04001136 RID: 4406
		internal const string BDLScheme_Classic1 = "BDLScheme_Classic1";

		// Token: 0x04001137 RID: 4407
		internal const string BDLScheme_Classic2 = "BDLScheme_Classic2";

		// Token: 0x04001138 RID: 4408
		internal const string BDLScheme_Consistent1 = "BDLScheme_Consistent1";

		// Token: 0x04001139 RID: 4409
		internal const string BDLScheme_Consistent2 = "BDLScheme_Consistent2";

		// Token: 0x0400113A RID: 4410
		internal const string BDLScheme_Consistent3 = "BDLScheme_Consistent3";

		// Token: 0x0400113B RID: 4411
		internal const string BDLScheme_Consistent4 = "BDLScheme_Consistent4";

		// Token: 0x0400113C RID: 4412
		internal const string BDLBor_Text = "BDLBor_Text";

		// Token: 0x0400113D RID: 4413
		internal const string BDLBor_Desc = "BDLBor_Desc";

		// Token: 0x0400113E RID: 4414
		internal const string BDLBor_CellMarginsGroup = "BDLBor_CellMarginsGroup";

		// Token: 0x0400113F RID: 4415
		internal const string BDLBor_CellPadding = "BDLBor_CellPadding";

		// Token: 0x04001140 RID: 4416
		internal const string BDLBor_CellSpacing = "BDLBor_CellSpacing";

		// Token: 0x04001141 RID: 4417
		internal const string BDLBor_BorderLinesGroup = "BDLBor_BorderLinesGroup";

		// Token: 0x04001142 RID: 4418
		internal const string BDLBor_GridLines = "BDLBor_GridLines";

		// Token: 0x04001143 RID: 4419
		internal const string BDLBor_GL_Horz = "BDLBor_GL_Horz";

		// Token: 0x04001144 RID: 4420
		internal const string BDLBor_GL_Vert = "BDLBor_GL_Vert";

		// Token: 0x04001145 RID: 4421
		internal const string BDLBor_GL_Both = "BDLBor_GL_Both";

		// Token: 0x04001146 RID: 4422
		internal const string BDLBor_GL_None = "BDLBor_GL_None";

		// Token: 0x04001147 RID: 4423
		internal const string BDLBor_BorderColor = "BDLBor_BorderColor";

		// Token: 0x04001148 RID: 4424
		internal const string BDLBor_BorderWidth = "BDLBor_BorderWidth";

		// Token: 0x04001149 RID: 4425
		internal const string BDLBor_ChooseColorButton = "BDLBor_ChooseColorButton";

		// Token: 0x0400114A RID: 4426
		internal const string BDLBor_ChooseColorDesc = "BDLBor_ChooseColorDesc";

		// Token: 0x0400114B RID: 4427
		internal const string BDLBor_BorderWidthValueDesc = "BDLBor_BorderWidthValueDesc";

		// Token: 0x0400114C RID: 4428
		internal const string BDLBor_BorderWidthValueName = "BDLBor_BorderWidthValueName";

		// Token: 0x0400114D RID: 4429
		internal const string BDLBor_BorderWidthUnitDesc = "BDLBor_BorderWidthUnitDesc";

		// Token: 0x0400114E RID: 4430
		internal const string BDLBor_BorderWidthUnitName = "BDLBor_BorderWidthUnitName";

		// Token: 0x0400114F RID: 4431
		internal const string BDLFmt_Text = "BDLFmt_Text";

		// Token: 0x04001150 RID: 4432
		internal const string BDLFmt_Desc = "BDLFmt_Desc";

		// Token: 0x04001151 RID: 4433
		internal const string BDLFmt_Objects = "BDLFmt_Objects";

		// Token: 0x04001152 RID: 4434
		internal const string BDLFmt_AppearanceGroup = "BDLFmt_AppearanceGroup";

		// Token: 0x04001153 RID: 4435
		internal const string BDLFmt_ForeColor = "BDLFmt_ForeColor";

		// Token: 0x04001154 RID: 4436
		internal const string BDLFmt_BackColor = "BDLFmt_BackColor";

		// Token: 0x04001155 RID: 4437
		internal const string BDLFmt_FontName = "BDLFmt_FontName";

		// Token: 0x04001156 RID: 4438
		internal const string BDLFmt_FontSize = "BDLFmt_FontSize";

		// Token: 0x04001157 RID: 4439
		internal const string BDLFmt_FS_Smaller = "BDLFmt_FS_Smaller";

		// Token: 0x04001158 RID: 4440
		internal const string BDLFmt_FS_Larger = "BDLFmt_FS_Larger";

		// Token: 0x04001159 RID: 4441
		internal const string BDLFmt_FS_XXSmall = "BDLFmt_FS_XXSmall";

		// Token: 0x0400115A RID: 4442
		internal const string BDLFmt_FS_XSmall = "BDLFmt_FS_XSmall";

		// Token: 0x0400115B RID: 4443
		internal const string BDLFmt_FS_Small = "BDLFmt_FS_Small";

		// Token: 0x0400115C RID: 4444
		internal const string BDLFmt_FS_Medium = "BDLFmt_FS_Medium";

		// Token: 0x0400115D RID: 4445
		internal const string BDLFmt_FS_Large = "BDLFmt_FS_Large";

		// Token: 0x0400115E RID: 4446
		internal const string BDLFmt_FS_XLarge = "BDLFmt_FS_XLarge";

		// Token: 0x0400115F RID: 4447
		internal const string BDLFmt_FS_XXLarge = "BDLFmt_FS_XXLarge";

		// Token: 0x04001160 RID: 4448
		internal const string BDLFmt_FS_Custom = "BDLFmt_FS_Custom";

		// Token: 0x04001161 RID: 4449
		internal const string BDLFmt_FontBold = "BDLFmt_FontBold";

		// Token: 0x04001162 RID: 4450
		internal const string BDLFmt_FontItalic = "BDLFmt_FontItalic";

		// Token: 0x04001163 RID: 4451
		internal const string BDLFmt_FontUnderline = "BDLFmt_FontUnderline";

		// Token: 0x04001164 RID: 4452
		internal const string BDLFmt_FontStrikeout = "BDLFmt_FontStrikeout";

		// Token: 0x04001165 RID: 4453
		internal const string BDLFmt_FontOverline = "BDLFmt_FontOverline";

		// Token: 0x04001166 RID: 4454
		internal const string BDLFmt_AlignmentGroup = "BDLFmt_AlignmentGroup";

		// Token: 0x04001167 RID: 4455
		internal const string BDLFmt_HorzAlign = "BDLFmt_HorzAlign";

		// Token: 0x04001168 RID: 4456
		internal const string BDLFmt_HA_Left = "BDLFmt_HA_Left";

		// Token: 0x04001169 RID: 4457
		internal const string BDLFmt_HA_Center = "BDLFmt_HA_Center";

		// Token: 0x0400116A RID: 4458
		internal const string BDLFmt_HA_Right = "BDLFmt_HA_Right";

		// Token: 0x0400116B RID: 4459
		internal const string BDLFmt_HA_Justify = "BDLFmt_HA_Justify";

		// Token: 0x0400116C RID: 4460
		internal const string BDLFmt_VertAlign = "BDLFmt_VertAlign";

		// Token: 0x0400116D RID: 4461
		internal const string BDLFmt_VA_Top = "BDLFmt_VA_Top";

		// Token: 0x0400116E RID: 4462
		internal const string BDLFmt_VA_Middle = "BDLFmt_VA_Middle";

		// Token: 0x0400116F RID: 4463
		internal const string BDLFmt_VA_Bottom = "BDLFmt_VA_Bottom";

		// Token: 0x04001170 RID: 4464
		internal const string BDLFmt_LayoutGroup = "BDLFmt_LayoutGroup";

		// Token: 0x04001171 RID: 4465
		internal const string BDLFmt_Width = "BDLFmt_Width";

		// Token: 0x04001172 RID: 4466
		internal const string BDLFmt_AllowWrapping = "BDLFmt_AllowWrapping";

		// Token: 0x04001173 RID: 4467
		internal const string BDLFmt_Node_EntireDG = "BDLFmt_Node_EntireDG";

		// Token: 0x04001174 RID: 4468
		internal const string BDLFmt_Node_EntireDL = "BDLFmt_Node_EntireDL";

		// Token: 0x04001175 RID: 4469
		internal const string BDLFmt_Node_Header = "BDLFmt_Node_Header";

		// Token: 0x04001176 RID: 4470
		internal const string BDLFmt_Node_Footer = "BDLFmt_Node_Footer";

		// Token: 0x04001177 RID: 4471
		internal const string BDLFmt_Node_Pager = "BDLFmt_Node_Pager";

		// Token: 0x04001178 RID: 4472
		internal const string BDLFmt_Node_Items = "BDLFmt_Node_Items";

		// Token: 0x04001179 RID: 4473
		internal const string BDLFmt_Node_Separators = "BDLFmt_Node_Separators";

		// Token: 0x0400117A RID: 4474
		internal const string BDLFmt_Node_NormalItems = "BDLFmt_Node_NormalItems";

		// Token: 0x0400117B RID: 4475
		internal const string BDLFmt_Node_AltItems = "BDLFmt_Node_AltItems";

		// Token: 0x0400117C RID: 4476
		internal const string BDLFmt_Node_SelItems = "BDLFmt_Node_SelItems";

		// Token: 0x0400117D RID: 4477
		internal const string BDLFmt_Node_EditItems = "BDLFmt_Node_EditItems";

		// Token: 0x0400117E RID: 4478
		internal const string BDLFmt_Node_Columns = "BDLFmt_Node_Columns";

		// Token: 0x0400117F RID: 4479
		internal const string BDLFmt_ChooseColorButton = "BDLFmt_ChooseColorButton";

		// Token: 0x04001180 RID: 4480
		internal const string BDLFmt_ChooseForeColorDesc = "BDLFmt_ChooseForeColorDesc";

		// Token: 0x04001181 RID: 4481
		internal const string BDLFmt_ChooseBackColorDesc = "BDLFmt_ChooseBackColorDesc";

		// Token: 0x04001182 RID: 4482
		internal const string BDLFmt_FontSizeValueDesc = "BDLFmt_FontSizeValueDesc";

		// Token: 0x04001183 RID: 4483
		internal const string BDLFmt_FontSizeValueName = "BDLFmt_FontSizeValueName";

		// Token: 0x04001184 RID: 4484
		internal const string BDLFmt_FontSizeUnitDesc = "BDLFmt_FontSizeUnitDesc";

		// Token: 0x04001185 RID: 4485
		internal const string BDLFmt_FontSizeUnitName = "BDLFmt_FontSizeUnitName";

		// Token: 0x04001186 RID: 4486
		internal const string BDLFmt_WidthValueDesc = "BDLFmt_WidthValueDesc";

		// Token: 0x04001187 RID: 4487
		internal const string BDLFmt_WidthValueName = "BDLFmt_WidthValueName";

		// Token: 0x04001188 RID: 4488
		internal const string BDLFmt_WidthUnitDesc = "BDLFmt_WidthUnitDesc";

		// Token: 0x04001189 RID: 4489
		internal const string BDLFmt_WidthUnitName = "BDLFmt_WidthUnitName";

		// Token: 0x0400118A RID: 4490
		internal const string CalAFmt_Title = "CalAFmt_Title";

		// Token: 0x0400118B RID: 4491
		internal const string CalAFmt_SchemeName = "CalAFmt_SchemeName";

		// Token: 0x0400118C RID: 4492
		internal const string CalAFmt_Preview = "CalAFmt_Preview";

		// Token: 0x0400118D RID: 4493
		internal const string CalAFmt_OK = "CalAFmt_OK";

		// Token: 0x0400118E RID: 4494
		internal const string CalAFmt_Cancel = "CalAFmt_Cancel";

		// Token: 0x0400118F RID: 4495
		internal const string CalAFmt_Help = "CalAFmt_Help";

		// Token: 0x04001190 RID: 4496
		internal const string CalAFmt_Scheme_Default = "CalAFmt_Scheme_Default";

		// Token: 0x04001191 RID: 4497
		internal const string CalAFmt_Scheme_Simple = "CalAFmt_Scheme_Simple";

		// Token: 0x04001192 RID: 4498
		internal const string CalAFmt_Scheme_Professional1 = "CalAFmt_Scheme_Professional1";

		// Token: 0x04001193 RID: 4499
		internal const string CalAFmt_Scheme_Professional2 = "CalAFmt_Scheme_Professional2";

		// Token: 0x04001194 RID: 4500
		internal const string CalAFmt_Scheme_Classic = "CalAFmt_Scheme_Classic";

		// Token: 0x04001195 RID: 4501
		internal const string CalAFmt_Scheme_Colorful1 = "CalAFmt_Scheme_Colorful1";

		// Token: 0x04001196 RID: 4502
		internal const string CalAFmt_Scheme_Colorful2 = "CalAFmt_Scheme_Colorful2";

		// Token: 0x04001197 RID: 4503
		internal const string CreateDataSource_Title = "CreateDataSource_Title";

		// Token: 0x04001198 RID: 4504
		internal const string CreateDataSource_Caption = "CreateDataSource_Caption";

		// Token: 0x04001199 RID: 4505
		internal const string CreateDataSource_Description = "CreateDataSource_Description";

		// Token: 0x0400119A RID: 4506
		internal const string CreateDataSource_SelectType = "CreateDataSource_SelectType";

		// Token: 0x0400119B RID: 4507
		internal const string CreateDataSource_SelectTypeDesc = "CreateDataSource_SelectTypeDesc";

		// Token: 0x0400119C RID: 4508
		internal const string CreateDataSource_ID = "CreateDataSource_ID";

		// Token: 0x0400119D RID: 4509
		internal const string CreateDataSource_NameNotValid = "CreateDataSource_NameNotValid";

		// Token: 0x0400119E RID: 4510
		internal const string CreateDataSource_NameNotUnique = "CreateDataSource_NameNotUnique";

		// Token: 0x0400119F RID: 4511
		internal const string DataSourceIDChromeConverter_NoDataSource = "DataSourceIDChromeConverter_NoDataSource";

		// Token: 0x040011A0 RID: 4512
		internal const string DataSourceIDChromeConverter_NewDataSource = "DataSourceIDChromeConverter_NewDataSource";

		// Token: 0x040011A1 RID: 4513
		internal const string DCFAdd_Title = "DCFAdd_Title";

		// Token: 0x040011A2 RID: 4514
		internal const string DCFAdd_ChooseField = "DCFAdd_ChooseField";

		// Token: 0x040011A3 RID: 4515
		internal const string DCFAdd_HeaderText = "DCFAdd_HeaderText";

		// Token: 0x040011A4 RID: 4516
		internal const string DCFAdd_DataField = "DCFAdd_DataField";

		// Token: 0x040011A5 RID: 4517
		internal const string DCFAdd_ButtonType = "DCFAdd_ButtonType";

		// Token: 0x040011A6 RID: 4518
		internal const string DCFAdd_CommandName = "DCFAdd_CommandName";

		// Token: 0x040011A7 RID: 4519
		internal const string DCFAdd_Text = "DCFAdd_Text";

		// Token: 0x040011A8 RID: 4520
		internal const string DCFAdd_CommandButtons = "DCFAdd_CommandButtons";

		// Token: 0x040011A9 RID: 4521
		internal const string DCFAdd_EditUpdate = "DCFAdd_EditUpdate";

		// Token: 0x040011AA RID: 4522
		internal const string DCFAdd_Delete = "DCFAdd_Delete";

		// Token: 0x040011AB RID: 4523
		internal const string DCFAdd_NewInsert = "DCFAdd_NewInsert";

		// Token: 0x040011AC RID: 4524
		internal const string DCFAdd_Select = "DCFAdd_Select";

		// Token: 0x040011AD RID: 4525
		internal const string DCFAdd_ShowCancel = "DCFAdd_ShowCancel";

		// Token: 0x040011AE RID: 4526
		internal const string DCFAdd_DeleteDesc = "DCFAdd_DeleteDesc";

		// Token: 0x040011AF RID: 4527
		internal const string DCFAdd_SelectDesc = "DCFAdd_SelectDesc";

		// Token: 0x040011B0 RID: 4528
		internal const string DCFAdd_ShowCancelDesc = "DCFAdd_ShowCancelDesc";

		// Token: 0x040011B1 RID: 4529
		internal const string DCFAdd_EditUpdateDesc = "DCFAdd_EditUpdateDesc";

		// Token: 0x040011B2 RID: 4530
		internal const string DCFAdd_NewInsertDesc = "DCFAdd_NewInsertDesc";

		// Token: 0x040011B3 RID: 4531
		internal const string DCFAdd_ReadOnly = "DCFAdd_ReadOnly";

		// Token: 0x040011B4 RID: 4532
		internal const string DCFAdd_ImageMode = "DCFAdd_ImageMode";

		// Token: 0x040011B5 RID: 4533
		internal const string DCFAdd_DataMode = "DCFAdd_DataMode";

		// Token: 0x040011B6 RID: 4534
		internal const string DCFAdd_LinkMode = "DCFAdd_LinkMode";

		// Token: 0x040011B7 RID: 4535
		internal const string DCFAdd_LinkFormatString = "DCFAdd_LinkFormatString";

		// Token: 0x040011B8 RID: 4536
		internal const string DCFAdd_ExampleFormatString = "DCFAdd_ExampleFormatString";

		// Token: 0x040011B9 RID: 4537
		internal const string DCFAdd_HyperlinkText = "DCFAdd_HyperlinkText";

		// Token: 0x040011BA RID: 4538
		internal const string DCFAdd_HyperlinkURL = "DCFAdd_HyperlinkURL";

		// Token: 0x040011BB RID: 4539
		internal const string DCFAdd_SpecifyText = "DCFAdd_SpecifyText";

		// Token: 0x040011BC RID: 4540
		internal const string DCFAdd_BindText = "DCFAdd_BindText";

		// Token: 0x040011BD RID: 4541
		internal const string DCFAdd_TextFormatString = "DCFAdd_TextFormatString";

		// Token: 0x040011BE RID: 4542
		internal const string DCFAdd_TextFormatStringExample = "DCFAdd_TextFormatStringExample";

		// Token: 0x040011BF RID: 4543
		internal const string DCFAdd_SpecifyURL = "DCFAdd_SpecifyURL";

		// Token: 0x040011C0 RID: 4544
		internal const string DCFAdd_BindURL = "DCFAdd_BindURL";

		// Token: 0x040011C1 RID: 4545
		internal const string DCFAdd_URLFormatString = "DCFAdd_URLFormatString";

		// Token: 0x040011C2 RID: 4546
		internal const string DCFAdd_URLFormatStringExample = "DCFAdd_URLFormatStringExample";

		// Token: 0x040011C3 RID: 4547
		internal const string DCFEditor_Text = "DCFEditor_Text";

		// Token: 0x040011C4 RID: 4548
		internal const string DCFEditor_AutoGen = "DCFEditor_AutoGen";

		// Token: 0x040011C5 RID: 4549
		internal const string DCFEditor_AvailableFields = "DCFEditor_AvailableFields";

		// Token: 0x040011C6 RID: 4550
		internal const string DCFEditor_SelectedFields = "DCFEditor_SelectedFields";

		// Token: 0x040011C7 RID: 4551
		internal const string DCFEditor_FieldProps = "DCFEditor_FieldProps";

		// Token: 0x040011C8 RID: 4552
		internal const string DCFEditor_FieldPropsFormat = "DCFEditor_FieldPropsFormat";

		// Token: 0x040011C9 RID: 4553
		internal const string DCFEditor_Add = "DCFEditor_Add";

		// Token: 0x040011CA RID: 4554
		internal const string DCFEditor_MoveFieldUpName = "DCFEditor_MoveFieldUpName";

		// Token: 0x040011CB RID: 4555
		internal const string DCFEditor_MoveFieldDownName = "DCFEditor_MoveFieldDownName";

		// Token: 0x040011CC RID: 4556
		internal const string DCFEditor_DeleteFieldName = "DCFEditor_DeleteFieldName";

		// Token: 0x040011CD RID: 4557
		internal const string DCFEditor_MoveFieldUpDesc = "DCFEditor_MoveFieldUpDesc";

		// Token: 0x040011CE RID: 4558
		internal const string DCFEditor_MoveFieldDownDesc = "DCFEditor_MoveFieldDownDesc";

		// Token: 0x040011CF RID: 4559
		internal const string DCFEditor_DeleteFieldDesc = "DCFEditor_DeleteFieldDesc";

		// Token: 0x040011D0 RID: 4560
		internal const string DCFEditor_Templatize = "DCFEditor_Templatize";

		// Token: 0x040011D1 RID: 4561
		internal const string DCFEditor_Node_AllFields = "DCFEditor_Node_AllFields";

		// Token: 0x040011D2 RID: 4562
		internal const string DCFEditor_Node_Bound = "DCFEditor_Node_Bound";

		// Token: 0x040011D3 RID: 4563
		internal const string DCFEditor_Node_Button = "DCFEditor_Node_Button";

		// Token: 0x040011D4 RID: 4564
		internal const string DCFEditor_Node_Command = "DCFEditor_Node_Command";

		// Token: 0x040011D5 RID: 4565
		internal const string DCFEditor_Node_CheckBox = "DCFEditor_Node_CheckBox";

		// Token: 0x040011D6 RID: 4566
		internal const string DCFEditor_Node_HyperLink = "DCFEditor_Node_HyperLink";

		// Token: 0x040011D7 RID: 4567
		internal const string DCFEditor_Node_Template = "DCFEditor_Node_Template";

		// Token: 0x040011D8 RID: 4568
		internal const string DCFEditor_Node_Select = "DCFEditor_Node_Select";

		// Token: 0x040011D9 RID: 4569
		internal const string DCFEditor_Node_Edit = "DCFEditor_Node_Edit";

		// Token: 0x040011DA RID: 4570
		internal const string DCFEditor_Node_Insert = "DCFEditor_Node_Insert";

		// Token: 0x040011DB RID: 4571
		internal const string DCFEditor_Node_Delete = "DCFEditor_Node_Delete";

		// Token: 0x040011DC RID: 4572
		internal const string DCFEditor_Node_Image = "DCFEditor_Node_Image";

		// Token: 0x040011DD RID: 4573
		internal const string DCFEditor_Button = "DCFEditor_Button";

		// Token: 0x040011DE RID: 4574
		internal const string DCFEditor_HyperLink = "DCFEditor_HyperLink";

		// Token: 0x040011DF RID: 4575
		internal const string DesignTimeSiteMapProvider_RootNodeText = "DesignTimeSiteMapProvider_RootNodeText";

		// Token: 0x040011E0 RID: 4576
		internal const string DesignTimeSiteMapProvider_ParentNodeText = "DesignTimeSiteMapProvider_ParentNodeText";

		// Token: 0x040011E1 RID: 4577
		internal const string DesignTimeSiteMapProvider_SiblingNodeText = "DesignTimeSiteMapProvider_SiblingNodeText";

		// Token: 0x040011E2 RID: 4578
		internal const string DesignTimeSiteMapProvider_CurrentNodeText = "DesignTimeSiteMapProvider_CurrentNodeText";

		// Token: 0x040011E3 RID: 4579
		internal const string DesignTimeSiteMapProvider_ChildNodeText = "DesignTimeSiteMapProvider_ChildNodeText";

		// Token: 0x040011E4 RID: 4580
		internal const string DesignTimeSiteMapProvider_Duplicate_Url = "DesignTimeSiteMapProvider_Duplicate_Url";

		// Token: 0x040011E5 RID: 4581
		internal const string DGGen_Text = "DGGen_Text";

		// Token: 0x040011E6 RID: 4582
		internal const string DGGen_Desc = "DGGen_Desc";

		// Token: 0x040011E7 RID: 4583
		internal const string DGGen_DataGroup = "DGGen_DataGroup";

		// Token: 0x040011E8 RID: 4584
		internal const string DGGen_DataSource = "DGGen_DataSource";

		// Token: 0x040011E9 RID: 4585
		internal const string DGGen_DataMember = "DGGen_DataMember";

		// Token: 0x040011EA RID: 4586
		internal const string DGGen_DSUnbound = "DGGen_DSUnbound";

		// Token: 0x040011EB RID: 4587
		internal const string DGGen_DataKey = "DGGen_DataKey";

		// Token: 0x040011EC RID: 4588
		internal const string DGGen_DKNone = "DGGen_DKNone";

		// Token: 0x040011ED RID: 4589
		internal const string DGGen_DMNone = "DGGen_DMNone";

		// Token: 0x040011EE RID: 4590
		internal const string DGGen_HeaderFooterGroup = "DGGen_HeaderFooterGroup";

		// Token: 0x040011EF RID: 4591
		internal const string DGGen_ShowHeader = "DGGen_ShowHeader";

		// Token: 0x040011F0 RID: 4592
		internal const string DGGen_ShowFooter = "DGGen_ShowFooter";

		// Token: 0x040011F1 RID: 4593
		internal const string DGGen_BehaviorGroup = "DGGen_BehaviorGroup";

		// Token: 0x040011F2 RID: 4594
		internal const string DGGen_AllowSorting = "DGGen_AllowSorting";

		// Token: 0x040011F3 RID: 4595
		internal const string DGGen_AutoColumnInfo = "DGGen_AutoColumnInfo";

		// Token: 0x040011F4 RID: 4596
		internal const string DGGen_CustomColumnInfo = "DGGen_CustomColumnInfo";

		// Token: 0x040011F5 RID: 4597
		internal const string DGPg_Text = "DGPg_Text";

		// Token: 0x040011F6 RID: 4598
		internal const string DGPg_Desc = "DGPg_Desc";

		// Token: 0x040011F7 RID: 4599
		internal const string DGPg_PagingGroup = "DGPg_PagingGroup";

		// Token: 0x040011F8 RID: 4600
		internal const string DGPg_AllowPaging = "DGPg_AllowPaging";

		// Token: 0x040011F9 RID: 4601
		internal const string DGPg_AllowCustomPaging = "DGPg_AllowCustomPaging";

		// Token: 0x040011FA RID: 4602
		internal const string DGPg_PageSize = "DGPg_PageSize";

		// Token: 0x040011FB RID: 4603
		internal const string DGPg_Rows = "DGPg_Rows";

		// Token: 0x040011FC RID: 4604
		internal const string DGPg_NavigationGroup = "DGPg_NavigationGroup";

		// Token: 0x040011FD RID: 4605
		internal const string DGPg_Visible = "DGPg_Visible";

		// Token: 0x040011FE RID: 4606
		internal const string DGPg_Position = "DGPg_Position";

		// Token: 0x040011FF RID: 4607
		internal const string DGPg_Pos_Top = "DGPg_Pos_Top";

		// Token: 0x04001200 RID: 4608
		internal const string DGPg_Pos_Bottom = "DGPg_Pos_Bottom";

		// Token: 0x04001201 RID: 4609
		internal const string DGPg_Pos_TopBottom = "DGPg_Pos_TopBottom";

		// Token: 0x04001202 RID: 4610
		internal const string DGPg_Mode = "DGPg_Mode";

		// Token: 0x04001203 RID: 4611
		internal const string DGPg_Mode_Buttons = "DGPg_Mode_Buttons";

		// Token: 0x04001204 RID: 4612
		internal const string DGPg_Mode_Numbers = "DGPg_Mode_Numbers";

		// Token: 0x04001205 RID: 4613
		internal const string DGPg_NextPage = "DGPg_NextPage";

		// Token: 0x04001206 RID: 4614
		internal const string DGPg_PrevPage = "DGPg_PrevPage";

		// Token: 0x04001207 RID: 4615
		internal const string DGPg_ButtonCount = "DGPg_ButtonCount";

		// Token: 0x04001208 RID: 4616
		internal const string DGCol_Text = "DGCol_Text";

		// Token: 0x04001209 RID: 4617
		internal const string DGCol_Desc = "DGCol_Desc";

		// Token: 0x0400120A RID: 4618
		internal const string DGCol_AutoGen = "DGCol_AutoGen";

		// Token: 0x0400120B RID: 4619
		internal const string DGCol_ColListGroup = "DGCol_ColListGroup";

		// Token: 0x0400120C RID: 4620
		internal const string DGCol_AvailableCols = "DGCol_AvailableCols";

		// Token: 0x0400120D RID: 4621
		internal const string DGCol_SelectedCols = "DGCol_SelectedCols";

		// Token: 0x0400120E RID: 4622
		internal const string DGCol_ColumnPropsGroup1 = "DGCol_ColumnPropsGroup1";

		// Token: 0x0400120F RID: 4623
		internal const string DGCol_ColumnPropsGroup2 = "DGCol_ColumnPropsGroup2";

		// Token: 0x04001210 RID: 4624
		internal const string DGCol_HeaderText = "DGCol_HeaderText";

		// Token: 0x04001211 RID: 4625
		internal const string DGCol_HeaderImage = "DGCol_HeaderImage";

		// Token: 0x04001212 RID: 4626
		internal const string DGCol_FooterText = "DGCol_FooterText";

		// Token: 0x04001213 RID: 4627
		internal const string DGCol_SortExpr = "DGCol_SortExpr";

		// Token: 0x04001214 RID: 4628
		internal const string DGCol_Visible = "DGCol_Visible";

		// Token: 0x04001215 RID: 4629
		internal const string DGCol_Templatize = "DGCol_Templatize";

		// Token: 0x04001216 RID: 4630
		internal const string DGCol_Node = "DGCol_Node";

		// Token: 0x04001217 RID: 4631
		internal const string DGCol_Node_DataFields = "DGCol_Node_DataFields";

		// Token: 0x04001218 RID: 4632
		internal const string DGCol_Node_AllFields = "DGCol_Node_AllFields";

		// Token: 0x04001219 RID: 4633
		internal const string DGCol_Node_Bound = "DGCol_Node_Bound";

		// Token: 0x0400121A RID: 4634
		internal const string DGCol_Node_Button = "DGCol_Node_Button";

		// Token: 0x0400121B RID: 4635
		internal const string DGCol_Node_Select = "DGCol_Node_Select";

		// Token: 0x0400121C RID: 4636
		internal const string DGCol_Node_Edit = "DGCol_Node_Edit";

		// Token: 0x0400121D RID: 4637
		internal const string DGCol_Node_Delete = "DGCol_Node_Delete";

		// Token: 0x0400121E RID: 4638
		internal const string DGCol_Node_HyperLink = "DGCol_Node_HyperLink";

		// Token: 0x0400121F RID: 4639
		internal const string DGCol_Node_Template = "DGCol_Node_Template";

		// Token: 0x04001220 RID: 4640
		internal const string DGCol_DFC_DataField = "DGCol_DFC_DataField";

		// Token: 0x04001221 RID: 4641
		internal const string DGCol_DFC_DataFormat = "DGCol_DFC_DataFormat";

		// Token: 0x04001222 RID: 4642
		internal const string DGCol_DFC_ReadOnly = "DGCol_DFC_ReadOnly";

		// Token: 0x04001223 RID: 4643
		internal const string DGCol_BC_Text = "DGCol_BC_Text";

		// Token: 0x04001224 RID: 4644
		internal const string DGCol_BC_DataTextField = "DGCol_BC_DataTextField";

		// Token: 0x04001225 RID: 4645
		internal const string DGCol_BC_DataTextFormat = "DGCol_BC_DataTextFormat";

		// Token: 0x04001226 RID: 4646
		internal const string DGCol_BC_Command = "DGCol_BC_Command";

		// Token: 0x04001227 RID: 4647
		internal const string DGCol_BC_ButtonType = "DGCol_BC_ButtonType";

		// Token: 0x04001228 RID: 4648
		internal const string DGCol_BC_BT_Link = "DGCol_BC_BT_Link";

		// Token: 0x04001229 RID: 4649
		internal const string DGCol_BC_BT_Push = "DGCol_BC_BT_Push";

		// Token: 0x0400122A RID: 4650
		internal const string DGCol_HC_Text = "DGCol_HC_Text";

		// Token: 0x0400122B RID: 4651
		internal const string DGCol_HC_DataTextField = "DGCol_HC_DataTextField";

		// Token: 0x0400122C RID: 4652
		internal const string DGCol_HC_DataTextFormat = "DGCol_HC_DataTextFormat";

		// Token: 0x0400122D RID: 4653
		internal const string DGCol_HC_URL = "DGCol_HC_URL";

		// Token: 0x0400122E RID: 4654
		internal const string DGCol_HC_DataURLField = "DGCol_HC_DataURLField";

		// Token: 0x0400122F RID: 4655
		internal const string DGCol_HC_DataURLFormat = "DGCol_HC_DataURLFormat";

		// Token: 0x04001230 RID: 4656
		internal const string DGCol_HC_Target = "DGCol_HC_Target";

		// Token: 0x04001231 RID: 4657
		internal const string DGCol_EC_Edit = "DGCol_EC_Edit";

		// Token: 0x04001232 RID: 4658
		internal const string DGCol_EC_Update = "DGCol_EC_Update";

		// Token: 0x04001233 RID: 4659
		internal const string DGCol_EC_Cancel = "DGCol_EC_Cancel";

		// Token: 0x04001234 RID: 4660
		internal const string DGCol_EC_ButtonType = "DGCol_EC_ButtonType";

		// Token: 0x04001235 RID: 4661
		internal const string DGCol_EC_BT_Link = "DGCol_EC_BT_Link";

		// Token: 0x04001236 RID: 4662
		internal const string DGCol_EC_BT_Push = "DGCol_EC_BT_Push";

		// Token: 0x04001237 RID: 4663
		internal const string DGCol_Button = "DGCol_Button";

		// Token: 0x04001238 RID: 4664
		internal const string DGCol_SelectButton = "DGCol_SelectButton";

		// Token: 0x04001239 RID: 4665
		internal const string DGCol_DeleteButton = "DGCol_DeleteButton";

		// Token: 0x0400123A RID: 4666
		internal const string DGCol_EditButton = "DGCol_EditButton";

		// Token: 0x0400123B RID: 4667
		internal const string DGCol_UpdateButton = "DGCol_UpdateButton";

		// Token: 0x0400123C RID: 4668
		internal const string DGCol_CancelButton = "DGCol_CancelButton";

		// Token: 0x0400123D RID: 4669
		internal const string DGCol_HyperLink = "DGCol_HyperLink";

		// Token: 0x0400123E RID: 4670
		internal const string DGCol_URLPFilter = "DGCol_URLPFilter";

		// Token: 0x0400123F RID: 4671
		internal const string DGCol_URLPCaption = "DGCol_URLPCaption";

		// Token: 0x04001240 RID: 4672
		internal const string DGCol_AddColButtonDesc = "DGCol_AddColButtonDesc";

		// Token: 0x04001241 RID: 4673
		internal const string DGCol_MoveColumnUpButtonDesc = "DGCol_MoveColumnUpButtonDesc";

		// Token: 0x04001242 RID: 4674
		internal const string DGCol_MoveColumnDownButtonDesc = "DGCol_MoveColumnDownButtonDesc";

		// Token: 0x04001243 RID: 4675
		internal const string DGCol_DeleteColumnButtonDesc = "DGCol_DeleteColumnButtonDesc";

		// Token: 0x04001244 RID: 4676
		internal const string DGCol_HeaderImagePickerDesc = "DGCol_HeaderImagePickerDesc";

		// Token: 0x04001245 RID: 4677
		internal const string DataList_NoTemplatesInst = "DataList_NoTemplatesInst";

		// Token: 0x04001246 RID: 4678
		internal const string DataList_NoTemplatesInst2 = "DataList_NoTemplatesInst2";

		// Token: 0x04001247 RID: 4679
		internal const string DataList_HeaderFooterTemplates = "DataList_HeaderFooterTemplates";

		// Token: 0x04001248 RID: 4680
		internal const string DataList_ItemTemplates = "DataList_ItemTemplates";

		// Token: 0x04001249 RID: 4681
		internal const string DataList_SeparatorTemplate = "DataList_SeparatorTemplate";

		// Token: 0x0400124A RID: 4682
		internal const string DataList_RefreshSchemaTransaction = "DataList_RefreshSchemaTransaction";

		// Token: 0x0400124B RID: 4683
		internal const string DataList_RegenerateTemplates = "DataList_RegenerateTemplates";

		// Token: 0x0400124C RID: 4684
		internal const string DataList_ClearTemplates = "DataList_ClearTemplates";

		// Token: 0x0400124D RID: 4685
		internal const string DataList_ClearTemplatesCaption = "DataList_ClearTemplatesCaption";

		// Token: 0x0400124E RID: 4686
		internal const string DLGen_Text = "DLGen_Text";

		// Token: 0x0400124F RID: 4687
		internal const string DLGen_Desc = "DLGen_Desc";

		// Token: 0x04001250 RID: 4688
		internal const string DLGen_DataGroup = "DLGen_DataGroup";

		// Token: 0x04001251 RID: 4689
		internal const string DLGen_DataSource = "DLGen_DataSource";

		// Token: 0x04001252 RID: 4690
		internal const string DLGen_DataMember = "DLGen_DataMember";

		// Token: 0x04001253 RID: 4691
		internal const string DLGen_DSUnbound = "DLGen_DSUnbound";

		// Token: 0x04001254 RID: 4692
		internal const string DLGen_DataKey = "DLGen_DataKey";

		// Token: 0x04001255 RID: 4693
		internal const string DLGen_DKNone = "DLGen_DKNone";

		// Token: 0x04001256 RID: 4694
		internal const string DLGen_DMNone = "DLGen_DMNone";

		// Token: 0x04001257 RID: 4695
		internal const string DLGen_HeaderFooterGroup = "DLGen_HeaderFooterGroup";

		// Token: 0x04001258 RID: 4696
		internal const string DLGen_ShowHeader = "DLGen_ShowHeader";

		// Token: 0x04001259 RID: 4697
		internal const string DLGen_ShowFooter = "DLGen_ShowFooter";

		// Token: 0x0400125A RID: 4698
		internal const string DLGen_RepeatLayoutGroup = "DLGen_RepeatLayoutGroup";

		// Token: 0x0400125B RID: 4699
		internal const string DLGen_RepeatColumns = "DLGen_RepeatColumns";

		// Token: 0x0400125C RID: 4700
		internal const string DLGen_RepeatDirection = "DLGen_RepeatDirection";

		// Token: 0x0400125D RID: 4701
		internal const string DLGen_RD_Horz = "DLGen_RD_Horz";

		// Token: 0x0400125E RID: 4702
		internal const string DLGen_RD_Vert = "DLGen_RD_Vert";

		// Token: 0x0400125F RID: 4703
		internal const string DLGen_RepeatLayout = "DLGen_RepeatLayout";

		// Token: 0x04001260 RID: 4704
		internal const string DLGen_RL_Table = "DLGen_RL_Table";

		// Token: 0x04001261 RID: 4705
		internal const string DLGen_RL_Flow = "DLGen_RL_Flow";

		// Token: 0x04001262 RID: 4706
		internal const string DLGen_ExtractRows = "DLGen_ExtractRows";

		// Token: 0x04001263 RID: 4707
		internal const string DLGen_Templates = "DLGen_Templates";

		// Token: 0x04001264 RID: 4708
		internal const string DVScheme_Empty = "DVScheme_Empty";

		// Token: 0x04001265 RID: 4709
		internal const string DVScheme_Colorful1 = "DVScheme_Colorful1";

		// Token: 0x04001266 RID: 4710
		internal const string DVScheme_Colorful2 = "DVScheme_Colorful2";

		// Token: 0x04001267 RID: 4711
		internal const string DVScheme_Colorful3 = "DVScheme_Colorful3";

		// Token: 0x04001268 RID: 4712
		internal const string DVScheme_Colorful4 = "DVScheme_Colorful4";

		// Token: 0x04001269 RID: 4713
		internal const string DVScheme_Colorful5 = "DVScheme_Colorful5";

		// Token: 0x0400126A RID: 4714
		internal const string DVScheme_Professional1 = "DVScheme_Professional1";

		// Token: 0x0400126B RID: 4715
		internal const string DVScheme_Professional2 = "DVScheme_Professional2";

		// Token: 0x0400126C RID: 4716
		internal const string DVScheme_Professional3 = "DVScheme_Professional3";

		// Token: 0x0400126D RID: 4717
		internal const string DVScheme_Simple1 = "DVScheme_Simple1";

		// Token: 0x0400126E RID: 4718
		internal const string DVScheme_Simple2 = "DVScheme_Simple2";

		// Token: 0x0400126F RID: 4719
		internal const string DVScheme_Simple3 = "DVScheme_Simple3";

		// Token: 0x04001270 RID: 4720
		internal const string DVScheme_Classic1 = "DVScheme_Classic1";

		// Token: 0x04001271 RID: 4721
		internal const string DVScheme_Classic2 = "DVScheme_Classic2";

		// Token: 0x04001272 RID: 4722
		internal const string DVScheme_Consistent1 = "DVScheme_Consistent1";

		// Token: 0x04001273 RID: 4723
		internal const string DVScheme_Consistent2 = "DVScheme_Consistent2";

		// Token: 0x04001274 RID: 4724
		internal const string DVScheme_Consistent3 = "DVScheme_Consistent3";

		// Token: 0x04001275 RID: 4725
		internal const string DVScheme_Consistent4 = "DVScheme_Consistent4";

		// Token: 0x04001276 RID: 4726
		internal const string FVScheme_Empty = "FVScheme_Empty";

		// Token: 0x04001277 RID: 4727
		internal const string FVScheme_Colorful1 = "FVScheme_Colorful1";

		// Token: 0x04001278 RID: 4728
		internal const string FVScheme_Colorful2 = "FVScheme_Colorful2";

		// Token: 0x04001279 RID: 4729
		internal const string FVScheme_Colorful3 = "FVScheme_Colorful3";

		// Token: 0x0400127A RID: 4730
		internal const string FVScheme_Colorful4 = "FVScheme_Colorful4";

		// Token: 0x0400127B RID: 4731
		internal const string FVScheme_Colorful5 = "FVScheme_Colorful5";

		// Token: 0x0400127C RID: 4732
		internal const string FVScheme_Professional1 = "FVScheme_Professional1";

		// Token: 0x0400127D RID: 4733
		internal const string FVScheme_Professional2 = "FVScheme_Professional2";

		// Token: 0x0400127E RID: 4734
		internal const string FVScheme_Professional3 = "FVScheme_Professional3";

		// Token: 0x0400127F RID: 4735
		internal const string FVScheme_Simple1 = "FVScheme_Simple1";

		// Token: 0x04001280 RID: 4736
		internal const string FVScheme_Simple2 = "FVScheme_Simple2";

		// Token: 0x04001281 RID: 4737
		internal const string FVScheme_Simple3 = "FVScheme_Simple3";

		// Token: 0x04001282 RID: 4738
		internal const string FVScheme_Classic1 = "FVScheme_Classic1";

		// Token: 0x04001283 RID: 4739
		internal const string FVScheme_Classic2 = "FVScheme_Classic2";

		// Token: 0x04001284 RID: 4740
		internal const string FVScheme_Consistent1 = "FVScheme_Consistent1";

		// Token: 0x04001285 RID: 4741
		internal const string FVScheme_Consistent2 = "FVScheme_Consistent2";

		// Token: 0x04001286 RID: 4742
		internal const string FVScheme_Consistent3 = "FVScheme_Consistent3";

		// Token: 0x04001287 RID: 4743
		internal const string FVScheme_Consistent4 = "FVScheme_Consistent4";

		// Token: 0x04001288 RID: 4744
		internal const string Repeater_NoTemplatesInst = "Repeater_NoTemplatesInst";

		// Token: 0x04001289 RID: 4745
		internal const string BaseDataBoundControl_CreateDataSourceTransaction = "BaseDataBoundControl_CreateDataSourceTransaction";

		// Token: 0x0400128A RID: 4746
		internal const string BaseDataBoundControl_ConfigureDataVerb = "BaseDataBoundControl_ConfigureDataVerb";

		// Token: 0x0400128B RID: 4747
		internal const string BaseDataBoundControl_ConfigureDataVerbDesc = "BaseDataBoundControl_ConfigureDataVerbDesc";

		// Token: 0x0400128C RID: 4748
		internal const string BaseDataBoundControl_DataActionGroup = "BaseDataBoundControl_DataActionGroup";

		// Token: 0x0400128D RID: 4749
		internal const string ExpressionEditor_ExpressionBound = "ExpressionEditor_ExpressionBound";

		// Token: 0x0400128E RID: 4750
		internal const string AppSettingExpressionEditor_AppSetting = "AppSettingExpressionEditor_AppSetting";

		// Token: 0x0400128F RID: 4751
		internal const string ConnectionStringsExpressionEditor_ConnectionName = "ConnectionStringsExpressionEditor_ConnectionName";

		// Token: 0x04001290 RID: 4752
		internal const string ConnectionStringsExpressionEditor_ConnectionType = "ConnectionStringsExpressionEditor_ConnectionType";

		// Token: 0x04001291 RID: 4753
		internal const string ExpressionEditor_Expression = "ExpressionEditor_Expression";

		// Token: 0x04001292 RID: 4754
		internal const string ResourceExpressionEditorSheet_ClassKey = "ResourceExpressionEditorSheet_ClassKey";

		// Token: 0x04001293 RID: 4755
		internal const string ResourceExpressionEditorSheet_ResourceKey = "ResourceExpressionEditorSheet_ResourceKey";

		// Token: 0x04001294 RID: 4756
		internal const string ResourceExpressionEditorSheet_InvalidResourceKey = "ResourceExpressionEditorSheet_InvalidResourceKey";

		// Token: 0x04001295 RID: 4757
		internal const string RouteValueExpressionEditorSheet_RouteValue = "RouteValueExpressionEditorSheet_RouteValue";

		// Token: 0x04001296 RID: 4758
		internal const string RouteUrlExpressionEditor_InvalidExpression = "RouteUrlExpressionEditor_InvalidExpression";

		// Token: 0x04001297 RID: 4759
		internal const string RouteUrlExpressionEditorSheet_RouteName = "RouteUrlExpressionEditorSheet_RouteName";

		// Token: 0x04001298 RID: 4760
		internal const string RouteUrlExpressionEditorSheet_RouteValues = "RouteUrlExpressionEditorSheet_RouteValues";

		// Token: 0x04001299 RID: 4761
		internal const string ControlDesigner_WndProcException = "ControlDesigner_WndProcException";

		// Token: 0x0400129A RID: 4762
		internal const string DataBoundControl_SchemaRefreshedWarning = "DataBoundControl_SchemaRefreshedWarning";

		// Token: 0x0400129B RID: 4763
		internal const string DataBoundControl_SchemaRefreshedWarningNoDataSource = "DataBoundControl_SchemaRefreshedWarningNoDataSource";

		// Token: 0x0400129C RID: 4764
		internal const string DataBoundControl_SchemaRefreshedCaption = "DataBoundControl_SchemaRefreshedCaption";

		// Token: 0x0400129D RID: 4765
		internal const string DataBoundControl_GridView = "DataBoundControl_GridView";

		// Token: 0x0400129E RID: 4766
		internal const string DataBoundControl_DetailsView = "DataBoundControl_DetailsView";

		// Token: 0x0400129F RID: 4767
		internal const string DataBoundControl_FormView = "DataBoundControl_FormView";

		// Token: 0x040012A0 RID: 4768
		internal const string DataBoundControl_Column = "DataBoundControl_Column";

		// Token: 0x040012A1 RID: 4769
		internal const string DataBoundControl_Row = "DataBoundControl_Row";

		// Token: 0x040012A2 RID: 4770
		internal const string DataBoundControlActionList_SetDataSourceIDTransaction = "DataBoundControlActionList_SetDataSourceIDTransaction";

		// Token: 0x040012A3 RID: 4771
		internal const string GridView_EditFieldsTransaction = "GridView_EditFieldsTransaction";

		// Token: 0x040012A4 RID: 4772
		internal const string GridView_AddNewFieldTransaction = "GridView_AddNewFieldTransaction";

		// Token: 0x040012A5 RID: 4773
		internal const string GridView_EnableEditingTransaction = "GridView_EnableEditingTransaction";

		// Token: 0x040012A6 RID: 4774
		internal const string GridView_EnableDeletingTransaction = "GridView_EnableDeletingTransaction";

		// Token: 0x040012A7 RID: 4775
		internal const string GridView_EnableSortingTransaction = "GridView_EnableSortingTransaction";

		// Token: 0x040012A8 RID: 4776
		internal const string GridView_EnableSelectionTransaction = "GridView_EnableSelectionTransaction";

		// Token: 0x040012A9 RID: 4777
		internal const string GridView_EnablePagingTransaction = "GridView_EnablePagingTransaction";

		// Token: 0x040012AA RID: 4778
		internal const string GridView_MoveLeftTransaction = "GridView_MoveLeftTransaction";

		// Token: 0x040012AB RID: 4779
		internal const string GridView_MoveRightTransaction = "GridView_MoveRightTransaction";

		// Token: 0x040012AC RID: 4780
		internal const string GridView_RemoveFieldTransaction = "GridView_RemoveFieldTransaction";

		// Token: 0x040012AD RID: 4781
		internal const string GridView_SchemaRefreshedTransaction = "GridView_SchemaRefreshedTransaction";

		// Token: 0x040012AE RID: 4782
		internal const string GridView_EditFieldsVerb = "GridView_EditFieldsVerb";

		// Token: 0x040012AF RID: 4783
		internal const string GridView_AddNewFieldVerb = "GridView_AddNewFieldVerb";

		// Token: 0x040012B0 RID: 4784
		internal const string GridView_RemoveFieldVerb = "GridView_RemoveFieldVerb";

		// Token: 0x040012B1 RID: 4785
		internal const string GridView_MoveFieldLeftVerb = "GridView_MoveFieldLeftVerb";

		// Token: 0x040012B2 RID: 4786
		internal const string GridView_MoveFieldRightVerb = "GridView_MoveFieldRightVerb";

		// Token: 0x040012B3 RID: 4787
		internal const string GridView_EditFieldsDesc = "GridView_EditFieldsDesc";

		// Token: 0x040012B4 RID: 4788
		internal const string GridView_AddNewFieldDesc = "GridView_AddNewFieldDesc";

		// Token: 0x040012B5 RID: 4789
		internal const string GridView_RemoveFieldDesc = "GridView_RemoveFieldDesc";

		// Token: 0x040012B6 RID: 4790
		internal const string GridView_MoveFieldLeftDesc = "GridView_MoveFieldLeftDesc";

		// Token: 0x040012B7 RID: 4791
		internal const string GridView_MoveFieldRightDesc = "GridView_MoveFieldRightDesc";

		// Token: 0x040012B8 RID: 4792
		internal const string GridView_Field = "GridView_Field";

		// Token: 0x040012B9 RID: 4793
		internal const string GridView_EnablePaging = "GridView_EnablePaging";

		// Token: 0x040012BA RID: 4794
		internal const string GridView_EnableSorting = "GridView_EnableSorting";

		// Token: 0x040012BB RID: 4795
		internal const string GridView_EnableEditing = "GridView_EnableEditing";

		// Token: 0x040012BC RID: 4796
		internal const string GridView_EnableDeleting = "GridView_EnableDeleting";

		// Token: 0x040012BD RID: 4797
		internal const string GridView_EnableSelection = "GridView_EnableSelection";

		// Token: 0x040012BE RID: 4798
		internal const string GridView_EnablePagingDesc = "GridView_EnablePagingDesc";

		// Token: 0x040012BF RID: 4799
		internal const string GridView_EnableSortingDesc = "GridView_EnableSortingDesc";

		// Token: 0x040012C0 RID: 4800
		internal const string GridView_EnableEditingDesc = "GridView_EnableEditingDesc";

		// Token: 0x040012C1 RID: 4801
		internal const string GridView_EnableDeletingDesc = "GridView_EnableDeletingDesc";

		// Token: 0x040012C2 RID: 4802
		internal const string GridView_EnableSelectionDesc = "GridView_EnableSelectionDesc";

		// Token: 0x040012C3 RID: 4803
		internal const string DataControls_SchemaRefreshedTransaction = "DataControls_SchemaRefreshedTransaction";

		// Token: 0x040012C4 RID: 4804
		internal const string DetailsView_EditFieldsTransaction = "DetailsView_EditFieldsTransaction";

		// Token: 0x040012C5 RID: 4805
		internal const string DetailsView_AddNewFieldTransaction = "DetailsView_AddNewFieldTransaction";

		// Token: 0x040012C6 RID: 4806
		internal const string DetailsView_EnableEditingTransaction = "DetailsView_EnableEditingTransaction";

		// Token: 0x040012C7 RID: 4807
		internal const string DetailsView_EnableDeletingTransaction = "DetailsView_EnableDeletingTransaction";

		// Token: 0x040012C8 RID: 4808
		internal const string DetailsView_EnableInsertingTransaction = "DetailsView_EnableInsertingTransaction";

		// Token: 0x040012C9 RID: 4809
		internal const string DetailsView_EnablePagingTransaction = "DetailsView_EnablePagingTransaction";

		// Token: 0x040012CA RID: 4810
		internal const string DetailsView_MoveUpTransaction = "DetailsView_MoveUpTransaction";

		// Token: 0x040012CB RID: 4811
		internal const string DetailsView_MoveDownTransaction = "DetailsView_MoveDownTransaction";

		// Token: 0x040012CC RID: 4812
		internal const string DetailsView_RemoveFieldTransaction = "DetailsView_RemoveFieldTransaction";

		// Token: 0x040012CD RID: 4813
		internal const string DetailsView_EditFieldsVerb = "DetailsView_EditFieldsVerb";

		// Token: 0x040012CE RID: 4814
		internal const string DetailsView_AddNewFieldVerb = "DetailsView_AddNewFieldVerb";

		// Token: 0x040012CF RID: 4815
		internal const string DetailsView_RemoveFieldVerb = "DetailsView_RemoveFieldVerb";

		// Token: 0x040012D0 RID: 4816
		internal const string DetailsView_MoveFieldUpVerb = "DetailsView_MoveFieldUpVerb";

		// Token: 0x040012D1 RID: 4817
		internal const string DetailsView_MoveFieldDownVerb = "DetailsView_MoveFieldDownVerb";

		// Token: 0x040012D2 RID: 4818
		internal const string DetailsView_Field = "DetailsView_Field";

		// Token: 0x040012D3 RID: 4819
		internal const string DetailsView_EnablePaging = "DetailsView_EnablePaging";

		// Token: 0x040012D4 RID: 4820
		internal const string DetailsView_EnableEditing = "DetailsView_EnableEditing";

		// Token: 0x040012D5 RID: 4821
		internal const string DetailsView_EnableDeleting = "DetailsView_EnableDeleting";

		// Token: 0x040012D6 RID: 4822
		internal const string DetailsView_EnableInserting = "DetailsView_EnableInserting";

		// Token: 0x040012D7 RID: 4823
		internal const string DetailsView_EditFieldsDesc = "DetailsView_EditFieldsDesc";

		// Token: 0x040012D8 RID: 4824
		internal const string DetailsView_AddNewFieldDesc = "DetailsView_AddNewFieldDesc";

		// Token: 0x040012D9 RID: 4825
		internal const string DetailsView_RemoveFieldDesc = "DetailsView_RemoveFieldDesc";

		// Token: 0x040012DA RID: 4826
		internal const string DetailsView_MoveFieldUpDesc = "DetailsView_MoveFieldUpDesc";

		// Token: 0x040012DB RID: 4827
		internal const string DetailsView_MoveFieldDownDesc = "DetailsView_MoveFieldDownDesc";

		// Token: 0x040012DC RID: 4828
		internal const string DetailsView_EnablePagingDesc = "DetailsView_EnablePagingDesc";

		// Token: 0x040012DD RID: 4829
		internal const string DetailsView_EnableEditingDesc = "DetailsView_EnableEditingDesc";

		// Token: 0x040012DE RID: 4830
		internal const string DetailsView_EnableDeletingDesc = "DetailsView_EnableDeletingDesc";

		// Token: 0x040012DF RID: 4831
		internal const string DetailsView_EnableInsertingDesc = "DetailsView_EnableInsertingDesc";

		// Token: 0x040012E0 RID: 4832
		internal const string FormView_EnablePagingTransaction = "FormView_EnablePagingTransaction";

		// Token: 0x040012E1 RID: 4833
		internal const string FormView_EnablePaging = "FormView_EnablePaging";

		// Token: 0x040012E2 RID: 4834
		internal const string FormView_EnablePagingDesc = "FormView_EnablePagingDesc";

		// Token: 0x040012E3 RID: 4835
		internal const string FormView_EnableDynamicData = "FormView_EnableDynamicData";

		// Token: 0x040012E4 RID: 4836
		internal const string FormView_EnableDynamicDataDesc = "FormView_EnableDynamicDataDesc";

		// Token: 0x040012E5 RID: 4837
		internal const string FormView_SchemaRefreshedWarning = "FormView_SchemaRefreshedWarning";

		// Token: 0x040012E6 RID: 4838
		internal const string FormView_SchemaRefreshedWarningNoDataSource = "FormView_SchemaRefreshedWarningNoDataSource";

		// Token: 0x040012E7 RID: 4839
		internal const string FormView_SchemaRefreshedWarningGenerate = "FormView_SchemaRefreshedWarningGenerate";

		// Token: 0x040012E8 RID: 4840
		internal const string FormView_SchemaRefreshedCaption = "FormView_SchemaRefreshedCaption";

		// Token: 0x040012E9 RID: 4841
		internal const string FormView_Edit = "FormView_Edit";

		// Token: 0x040012EA RID: 4842
		internal const string FormView_Update = "FormView_Update";

		// Token: 0x040012EB RID: 4843
		internal const string FormView_Cancel = "FormView_Cancel";

		// Token: 0x040012EC RID: 4844
		internal const string FormView_Delete = "FormView_Delete";

		// Token: 0x040012ED RID: 4845
		internal const string FormView_New = "FormView_New";

		// Token: 0x040012EE RID: 4846
		internal const string FormView_Insert = "FormView_Insert";

		// Token: 0x040012EF RID: 4847
		internal const string ListControlCreateDataSource_Title = "ListControlCreateDataSource_Title";

		// Token: 0x040012F0 RID: 4848
		internal const string ListControlCreateDataSource_Caption = "ListControlCreateDataSource_Caption";

		// Token: 0x040012F1 RID: 4849
		internal const string ListControlCreateDataSource_Description = "ListControlCreateDataSource_Description";

		// Token: 0x040012F2 RID: 4850
		internal const string ListControlCreateDataSource_SelectDataSource = "ListControlCreateDataSource_SelectDataSource";

		// Token: 0x040012F3 RID: 4851
		internal const string ListControlCreateDataSource_SelectDataTextField = "ListControlCreateDataSource_SelectDataTextField";

		// Token: 0x040012F4 RID: 4852
		internal const string ListControlCreateDataSource_SelectDataValueField = "ListControlCreateDataSource_SelectDataValueField";

		// Token: 0x040012F5 RID: 4853
		internal const string ListControl_ConfigureDataVerb = "ListControl_ConfigureDataVerb";

		// Token: 0x040012F6 RID: 4854
		internal const string ListControlDesigner_ConnectToDataSource = "ListControlDesigner_ConnectToDataSource";

		// Token: 0x040012F7 RID: 4855
		internal const string ListControl_EnableAutoPostBack = "ListControl_EnableAutoPostBack";

		// Token: 0x040012F8 RID: 4856
		internal const string ListControl_EnableAutoPostBackDesc = "ListControl_EnableAutoPostBackDesc";

		// Token: 0x040012F9 RID: 4857
		internal const string ListControl_EditItems = "ListControl_EditItems";

		// Token: 0x040012FA RID: 4858
		internal const string ListControl_EditItemsDesc = "ListControl_EditItemsDesc";

		// Token: 0x040012FB RID: 4859
		internal const string ListControlDesigner_EditItems = "ListControlDesigner_EditItems";

		// Token: 0x040012FC RID: 4860
		internal const string ContainerControlDesigner_RegionWatermark = "ContainerControlDesigner_RegionWatermark";

		// Token: 0x040012FD RID: 4861
		internal const string ContentPlaceHolder_Invalid_RootComponent = "ContentPlaceHolder_Invalid_RootComponent";

		// Token: 0x040012FE RID: 4862
		internal const string Content_CreateBlankContent = "Content_CreateBlankContent";

		// Token: 0x040012FF RID: 4863
		internal const string Content_ClearRegion = "Content_ClearRegion";

		// Token: 0x04001300 RID: 4864
		internal const string RenderOuterTable_RemoveOuterTableWarning = "RenderOuterTable_RemoveOuterTableWarning";

		// Token: 0x04001301 RID: 4865
		internal const string RenderOuterTable_RemoveOuterTableCaption = "RenderOuterTable_RemoveOuterTableCaption";

		// Token: 0x04001302 RID: 4866
		internal const string RenderOuterTableHelper_ResetProperties = "RenderOuterTableHelper_ResetProperties";

		// Token: 0x04001303 RID: 4867
		internal const string SiteMapPathAFmt_Scheme_Default = "SiteMapPathAFmt_Scheme_Default";

		// Token: 0x04001304 RID: 4868
		internal const string SiteMapPathAFmt_Scheme_Colorful = "SiteMapPathAFmt_Scheme_Colorful";

		// Token: 0x04001305 RID: 4869
		internal const string SiteMapPathAFmt_Scheme_Simple = "SiteMapPathAFmt_Scheme_Simple";

		// Token: 0x04001306 RID: 4870
		internal const string SiteMapPathAFmt_Scheme_Professional = "SiteMapPathAFmt_Scheme_Professional";

		// Token: 0x04001307 RID: 4871
		internal const string SiteMapPathAFmt_Scheme_Classic = "SiteMapPathAFmt_Scheme_Classic";

		// Token: 0x04001308 RID: 4872
		internal const string ImageGeneratorUrlEditor_Filter = "ImageGeneratorUrlEditor_Filter";

		// Token: 0x04001309 RID: 4873
		internal const string WebControls_ConvertToTemplate = "WebControls_ConvertToTemplate";

		// Token: 0x0400130A RID: 4874
		internal const string WebControls_ConvertToTemplateDescription = "WebControls_ConvertToTemplateDescription";

		// Token: 0x0400130B RID: 4875
		internal const string WebControls_ConvertToTemplateDescriptionViews = "WebControls_ConvertToTemplateDescriptionViews";

		// Token: 0x0400130C RID: 4876
		internal const string WebControls_Reset = "WebControls_Reset";

		// Token: 0x0400130D RID: 4877
		internal const string WebControls_ResetDescription = "WebControls_ResetDescription";

		// Token: 0x0400130E RID: 4878
		internal const string WebControls_ResetDescriptionViews = "WebControls_ResetDescriptionViews";

		// Token: 0x0400130F RID: 4879
		internal const string WebControls_Views = "WebControls_Views";

		// Token: 0x04001310 RID: 4880
		internal const string WebControls_ViewsDescription = "WebControls_ViewsDescription";

		// Token: 0x04001311 RID: 4881
		internal const string ChangePassword_ChangePasswordView = "ChangePassword_ChangePasswordView";

		// Token: 0x04001312 RID: 4882
		internal const string ChangePassword_SuccessView = "ChangePassword_SuccessView";

		// Token: 0x04001313 RID: 4883
		internal const string ChangePasswordAutoFormat_UserName = "ChangePasswordAutoFormat_UserName";

		// Token: 0x04001314 RID: 4884
		internal const string ChangePasswordAutoFormat_HelpPageText = "ChangePasswordAutoFormat_HelpPageText";

		// Token: 0x04001315 RID: 4885
		internal const string ChangePasswordScheme_Empty = "ChangePasswordScheme_Empty";

		// Token: 0x04001316 RID: 4886
		internal const string ChangePasswordScheme_Classic = "ChangePasswordScheme_Classic";

		// Token: 0x04001317 RID: 4887
		internal const string ChangePasswordScheme_Elegant = "ChangePasswordScheme_Elegant";

		// Token: 0x04001318 RID: 4888
		internal const string ChangePasswordScheme_Simple = "ChangePasswordScheme_Simple";

		// Token: 0x04001319 RID: 4889
		internal const string ChangePasswordScheme_Professional = "ChangePasswordScheme_Professional";

		// Token: 0x0400131A RID: 4890
		internal const string ChangePasswordScheme_Colorful = "ChangePasswordScheme_Colorful";

		// Token: 0x0400131B RID: 4891
		internal const string Login_LaunchWebAdmin = "Login_LaunchWebAdmin";

		// Token: 0x0400131C RID: 4892
		internal const string Login_LaunchWebAdminDescription = "Login_LaunchWebAdminDescription";

		// Token: 0x0400131D RID: 4893
		internal const string LoginScheme_Empty = "LoginScheme_Empty";

		// Token: 0x0400131E RID: 4894
		internal const string LoginScheme_Classic = "LoginScheme_Classic";

		// Token: 0x0400131F RID: 4895
		internal const string LoginScheme_Elegant = "LoginScheme_Elegant";

		// Token: 0x04001320 RID: 4896
		internal const string LoginScheme_Simple = "LoginScheme_Simple";

		// Token: 0x04001321 RID: 4897
		internal const string LoginScheme_Professional = "LoginScheme_Professional";

		// Token: 0x04001322 RID: 4898
		internal const string LoginScheme_Colorful = "LoginScheme_Colorful";

		// Token: 0x04001323 RID: 4899
		internal const string LoginAutoFormat_UserName = "LoginAutoFormat_UserName";

		// Token: 0x04001324 RID: 4900
		internal const string LoginAutoFormat_HelpPageText = "LoginAutoFormat_HelpPageText";

		// Token: 0x04001325 RID: 4901
		internal const string CreateUserWizardScheme_Empty = "CreateUserWizardScheme_Empty";

		// Token: 0x04001326 RID: 4902
		internal const string CreateUserWizardScheme_Classic = "CreateUserWizardScheme_Classic";

		// Token: 0x04001327 RID: 4903
		internal const string CreateUserWizardScheme_Elegant = "CreateUserWizardScheme_Elegant";

		// Token: 0x04001328 RID: 4904
		internal const string CreateUserWizardScheme_Simple = "CreateUserWizardScheme_Simple";

		// Token: 0x04001329 RID: 4905
		internal const string CreateUserWizardScheme_Professional = "CreateUserWizardScheme_Professional";

		// Token: 0x0400132A RID: 4906
		internal const string CreateUserWizardScheme_Colorful = "CreateUserWizardScheme_Colorful";

		// Token: 0x0400132B RID: 4907
		internal const string LoginStatus_LoggedOutView = "LoginStatus_LoggedOutView";

		// Token: 0x0400132C RID: 4908
		internal const string LoginStatus_LoggedInView = "LoginStatus_LoggedInView";

		// Token: 0x0400132D RID: 4909
		internal const string LoginView_EditRoleGroups = "LoginView_EditRoleGroups";

		// Token: 0x0400132E RID: 4910
		internal const string LoginView_EditRoleGroupsDescription = "LoginView_EditRoleGroupsDescription";

		// Token: 0x0400132F RID: 4911
		internal const string LoginView_EditRoleGroupsTransactionDescription = "LoginView_EditRoleGroupsTransactionDescription";

		// Token: 0x04001330 RID: 4912
		internal const string LoginView_ErrorRendering = "LoginView_ErrorRendering";

		// Token: 0x04001331 RID: 4913
		internal const string LoginView_AnonymousTemplateEmpty = "LoginView_AnonymousTemplateEmpty";

		// Token: 0x04001332 RID: 4914
		internal const string LoginView_LoggedInTemplateEmpty = "LoginView_LoggedInTemplateEmpty";

		// Token: 0x04001333 RID: 4915
		internal const string LoginView_RoleGroupTemplateEmpty = "LoginView_RoleGroupTemplateEmpty";

		// Token: 0x04001334 RID: 4916
		internal const string LoginView_NoTemplateInst = "LoginView_NoTemplateInst";

		// Token: 0x04001335 RID: 4917
		internal const string UserControlDesignerHost_ComponentAlreadyExists = "UserControlDesignerHost_ComponentAlreadyExists";

		// Token: 0x04001336 RID: 4918
		internal const string MenuDesigner_DataActionGroup = "MenuDesigner_DataActionGroup";

		// Token: 0x04001337 RID: 4919
		internal const string MenuDesigner_EditBindingsTransactionDescription = "MenuDesigner_EditBindingsTransactionDescription";

		// Token: 0x04001338 RID: 4920
		internal const string MenuDesigner_EditMenuItemsTransactionDescription = "MenuDesigner_EditMenuItemsTransactionDescription";

		// Token: 0x04001339 RID: 4921
		internal const string MenuDesigner_EditBindings = "MenuDesigner_EditBindings";

		// Token: 0x0400133A RID: 4922
		internal const string MenuDesigner_EditBindingsDescription = "MenuDesigner_EditBindingsDescription";

		// Token: 0x0400133B RID: 4923
		internal const string MenuDesigner_EditMenuItems = "MenuDesigner_EditMenuItems";

		// Token: 0x0400133C RID: 4924
		internal const string MenuDesigner_EditMenuItemsDescription = "MenuDesigner_EditMenuItemsDescription";

		// Token: 0x0400133D RID: 4925
		internal const string MenuDesigner_CreateLineImages = "MenuDesigner_CreateLineImages";

		// Token: 0x0400133E RID: 4926
		internal const string MenuDesigner_Empty = "MenuDesigner_Empty";

		// Token: 0x0400133F RID: 4927
		internal const string MenuDesigner_EmptyDataBinding = "MenuDesigner_EmptyDataBinding";

		// Token: 0x04001340 RID: 4928
		internal const string MenuDesigner_Error = "MenuDesigner_Error";

		// Token: 0x04001341 RID: 4929
		internal const string MenuDesigner_EditNodesTransactionDescription = "MenuDesigner_EditNodesTransactionDescription";

		// Token: 0x04001342 RID: 4930
		internal const string MenuDesigner_EditNodes = "MenuDesigner_EditNodes";

		// Token: 0x04001343 RID: 4931
		internal const string MenuDesigner_ViewsDescription = "MenuDesigner_ViewsDescription";

		// Token: 0x04001344 RID: 4932
		internal const string MenuDesigner_ConvertToDynamicTemplate = "MenuDesigner_ConvertToDynamicTemplate";

		// Token: 0x04001345 RID: 4933
		internal const string MenuDesigner_ConvertToDynamicTemplateDescription = "MenuDesigner_ConvertToDynamicTemplateDescription";

		// Token: 0x04001346 RID: 4934
		internal const string MenuDesigner_ResetDynamicTemplate = "MenuDesigner_ResetDynamicTemplate";

		// Token: 0x04001347 RID: 4935
		internal const string MenuDesigner_ResetDynamicTemplateDescription = "MenuDesigner_ResetDynamicTemplateDescription";

		// Token: 0x04001348 RID: 4936
		internal const string MenuDesigner_ConvertToStaticTemplate = "MenuDesigner_ConvertToStaticTemplate";

		// Token: 0x04001349 RID: 4937
		internal const string MenuDesigner_ConvertToStaticTemplateDescription = "MenuDesigner_ConvertToStaticTemplateDescription";

		// Token: 0x0400134A RID: 4938
		internal const string MenuDesigner_ResetStaticTemplate = "MenuDesigner_ResetStaticTemplate";

		// Token: 0x0400134B RID: 4939
		internal const string MenuDesigner_ResetStaticTemplateDescription = "MenuDesigner_ResetStaticTemplateDescription";

		// Token: 0x0400134C RID: 4940
		internal const string Menu_StaticView = "Menu_StaticView";

		// Token: 0x0400134D RID: 4941
		internal const string Menu_DynamicView = "Menu_DynamicView";

		// Token: 0x0400134E RID: 4942
		internal const string MenuItemCollectionEditor_AddRoot = "MenuItemCollectionEditor_AddRoot";

		// Token: 0x0400134F RID: 4943
		internal const string MenuItemCollectionEditor_AddChild = "MenuItemCollectionEditor_AddChild";

		// Token: 0x04001350 RID: 4944
		internal const string MenuItemCollectionEditor_Remove = "MenuItemCollectionEditor_Remove";

		// Token: 0x04001351 RID: 4945
		internal const string MenuItemCollectionEditor_MoveDown = "MenuItemCollectionEditor_MoveDown";

		// Token: 0x04001352 RID: 4946
		internal const string MenuItemCollectionEditor_MoveUp = "MenuItemCollectionEditor_MoveUp";

		// Token: 0x04001353 RID: 4947
		internal const string MenuItemCollectionEditor_Indent = "MenuItemCollectionEditor_Indent";

		// Token: 0x04001354 RID: 4948
		internal const string MenuItemCollectionEditor_Unindent = "MenuItemCollectionEditor_Unindent";

		// Token: 0x04001355 RID: 4949
		internal const string MenuItemCollectionEditor_OK = "MenuItemCollectionEditor_OK";

		// Token: 0x04001356 RID: 4950
		internal const string MenuItemCollectionEditor_Cancel = "MenuItemCollectionEditor_Cancel";

		// Token: 0x04001357 RID: 4951
		internal const string MenuItemCollectionEditor_Nodes = "MenuItemCollectionEditor_Nodes";

		// Token: 0x04001358 RID: 4952
		internal const string MenuItemCollectionEditor_Properties = "MenuItemCollectionEditor_Properties";

		// Token: 0x04001359 RID: 4953
		internal const string MenuItemCollectionEditor_PropertyGrid = "MenuItemCollectionEditor_PropertyGrid";

		// Token: 0x0400135A RID: 4954
		internal const string MenuItemCollectionEditor_Title = "MenuItemCollectionEditor_Title";

		// Token: 0x0400135B RID: 4955
		internal const string MenuItemCollectionEditor_NewNodeText = "MenuItemCollectionEditor_NewNodeText";

		// Token: 0x0400135C RID: 4956
		internal const string MenuItemCollectionEditor_CantSelect = "MenuItemCollectionEditor_CantSelect";

		// Token: 0x0400135D RID: 4957
		internal const string MenuBindingsEditor_Apply = "MenuBindingsEditor_Apply";

		// Token: 0x0400135E RID: 4958
		internal const string MenuBindingsEditor_AddBinding = "MenuBindingsEditor_AddBinding";

		// Token: 0x0400135F RID: 4959
		internal const string MenuBindingsEditor_AutoGenerateBindings = "MenuBindingsEditor_AutoGenerateBindings";

		// Token: 0x04001360 RID: 4960
		internal const string MenuBindingsEditor_Bindings = "MenuBindingsEditor_Bindings";

		// Token: 0x04001361 RID: 4961
		internal const string MenuBindingsEditor_BindingProperties = "MenuBindingsEditor_BindingProperties";

		// Token: 0x04001362 RID: 4962
		internal const string MenuBindingsEditor_Cancel = "MenuBindingsEditor_Cancel";

		// Token: 0x04001363 RID: 4963
		internal const string MenuBindingsEditor_EmptyBindingText = "MenuBindingsEditor_EmptyBindingText";

		// Token: 0x04001364 RID: 4964
		internal const string MenuBindingsEditor_OK = "MenuBindingsEditor_OK";

		// Token: 0x04001365 RID: 4965
		internal const string MenuBindingsEditor_Schema = "MenuBindingsEditor_Schema";

		// Token: 0x04001366 RID: 4966
		internal const string MenuBindingsEditor_Title = "MenuBindingsEditor_Title";

		// Token: 0x04001367 RID: 4967
		internal const string MenuBindingsEditor_MoveBindingUpName = "MenuBindingsEditor_MoveBindingUpName";

		// Token: 0x04001368 RID: 4968
		internal const string MenuBindingsEditor_MoveBindingUpDescription = "MenuBindingsEditor_MoveBindingUpDescription";

		// Token: 0x04001369 RID: 4969
		internal const string MenuBindingsEditor_MoveBindingDownName = "MenuBindingsEditor_MoveBindingDownName";

		// Token: 0x0400136A RID: 4970
		internal const string MenuBindingsEditor_MoveBindingDownDescription = "MenuBindingsEditor_MoveBindingDownDescription";

		// Token: 0x0400136B RID: 4971
		internal const string MenuBindingsEditor_DeleteBindingName = "MenuBindingsEditor_DeleteBindingName";

		// Token: 0x0400136C RID: 4972
		internal const string MenuBindingsEditor_DeleteBindingDescription = "MenuBindingsEditor_DeleteBindingDescription";

		// Token: 0x0400136D RID: 4973
		internal const string MenuScheme_Empty = "MenuScheme_Empty";

		// Token: 0x0400136E RID: 4974
		internal const string MenuScheme_Classic = "MenuScheme_Classic";

		// Token: 0x0400136F RID: 4975
		internal const string MenuScheme_Professional = "MenuScheme_Professional";

		// Token: 0x04001370 RID: 4976
		internal const string MenuScheme_Colorful = "MenuScheme_Colorful";

		// Token: 0x04001371 RID: 4977
		internal const string MenuScheme_Simple = "MenuScheme_Simple";

		// Token: 0x04001372 RID: 4978
		internal const string PagerScheme_Empty = "PagerScheme_Empty";

		// Token: 0x04001373 RID: 4979
		internal const string PagerScheme_Classic = "PagerScheme_Classic";

		// Token: 0x04001374 RID: 4980
		internal const string PagerScheme_Professional = "PagerScheme_Professional";

		// Token: 0x04001375 RID: 4981
		internal const string PagerScheme_Colorful = "PagerScheme_Colorful";

		// Token: 0x04001376 RID: 4982
		internal const string PagerScheme_Simple = "PagerScheme_Simple";

		// Token: 0x04001377 RID: 4983
		internal const string PasswordRecoveryScheme_Empty = "PasswordRecoveryScheme_Empty";

		// Token: 0x04001378 RID: 4984
		internal const string PasswordRecoveryScheme_Classic = "PasswordRecoveryScheme_Classic";

		// Token: 0x04001379 RID: 4985
		internal const string PasswordRecoveryScheme_Elegant = "PasswordRecoveryScheme_Elegant";

		// Token: 0x0400137A RID: 4986
		internal const string PasswordRecoveryScheme_Simple = "PasswordRecoveryScheme_Simple";

		// Token: 0x0400137B RID: 4987
		internal const string PasswordRecoveryScheme_Professional = "PasswordRecoveryScheme_Professional";

		// Token: 0x0400137C RID: 4988
		internal const string PasswordRecoveryScheme_Colorful = "PasswordRecoveryScheme_Colorful";

		// Token: 0x0400137D RID: 4989
		internal const string PasswordRecovery_QuestionView = "PasswordRecovery_QuestionView";

		// Token: 0x0400137E RID: 4990
		internal const string PasswordRecovery_SuccessView = "PasswordRecovery_SuccessView";

		// Token: 0x0400137F RID: 4991
		internal const string PasswordRecovery_UserNameView = "PasswordRecovery_UserNameView";

		// Token: 0x04001380 RID: 4992
		internal const string PasswordRecoveryAutoFormat_UserName = "PasswordRecoveryAutoFormat_UserName";

		// Token: 0x04001381 RID: 4993
		internal const string PasswordRecoveryAutoFormat_HelpPageText = "PasswordRecoveryAutoFormat_HelpPageText";

		// Token: 0x04001382 RID: 4994
		internal const string MailFilePicker_Caption = "MailFilePicker_Caption";

		// Token: 0x04001383 RID: 4995
		internal const string MailFilePicker_Filter = "MailFilePicker_Filter";

		// Token: 0x04001384 RID: 4996
		internal const string Xml_Inst = "Xml_Inst";

		// Token: 0x04001385 RID: 4997
		internal const string MailDefinitionBodyFileNameEditor_DefaultCaption = "MailDefinitionBodyFileNameEditor_DefaultCaption";

		// Token: 0x04001386 RID: 4998
		internal const string MailDefinitionBodyFileNameEditor_DefaultFilter = "MailDefinitionBodyFileNameEditor_DefaultFilter";

		// Token: 0x04001387 RID: 4999
		internal const string UrlPicker_DefaultCaption = "UrlPicker_DefaultCaption";

		// Token: 0x04001388 RID: 5000
		internal const string UrlPicker_DefaultFilter = "UrlPicker_DefaultFilter";

		// Token: 0x04001389 RID: 5001
		internal const string UrlPicker_ImageCaption = "UrlPicker_ImageCaption";

		// Token: 0x0400138A RID: 5002
		internal const string UrlPicker_ImageFilter = "UrlPicker_ImageFilter";

		// Token: 0x0400138B RID: 5003
		internal const string UrlPicker_XmlCaption = "UrlPicker_XmlCaption";

		// Token: 0x0400138C RID: 5004
		internal const string UrlPicker_XmlFilter = "UrlPicker_XmlFilter";

		// Token: 0x0400138D RID: 5005
		internal const string UrlPicker_XslCaption = "UrlPicker_XslCaption";

		// Token: 0x0400138E RID: 5006
		internal const string UrlPicker_XslFilter = "UrlPicker_XslFilter";

		// Token: 0x0400138F RID: 5007
		internal const string XMLFilePicker_Caption = "XMLFilePicker_Caption";

		// Token: 0x04001390 RID: 5008
		internal const string XMLFilePicker_Filter = "XMLFilePicker_Filter";

		// Token: 0x04001391 RID: 5009
		internal const string DataBindingGlyph_ToolTip = "DataBindingGlyph_ToolTip";

		// Token: 0x04001392 RID: 5010
		internal const string ExpressionBindingGlyph_ToolTip = "ExpressionBindingGlyph_ToolTip";

		// Token: 0x04001393 RID: 5011
		internal const string ImplicitExpressionBindingGlyph_ToolTip = "ImplicitExpressionBindingGlyph_ToolTip";

		// Token: 0x04001394 RID: 5012
		internal const string TemplateEdit_Tip = "TemplateEdit_Tip";

		// Token: 0x04001395 RID: 5013
		internal const string RegexEditor_Title = "RegexEditor_Title";

		// Token: 0x04001396 RID: 5014
		internal const string RegexEditor_StdExp = "RegexEditor_StdExp";

		// Token: 0x04001397 RID: 5015
		internal const string RegexEditor_Validate = "RegexEditor_Validate";

		// Token: 0x04001398 RID: 5016
		internal const string RegexEditor_SampleInput = "RegexEditor_SampleInput";

		// Token: 0x04001399 RID: 5017
		internal const string RegexEditor_TestExpression = "RegexEditor_TestExpression";

		// Token: 0x0400139A RID: 5018
		internal const string RegexEditor_ValidationExpression = "RegexEditor_ValidationExpression";

		// Token: 0x0400139B RID: 5019
		internal const string RegexEditor_InputValid = "RegexEditor_InputValid";

		// Token: 0x0400139C RID: 5020
		internal const string RegexEditor_InputInvalid = "RegexEditor_InputInvalid";

		// Token: 0x0400139D RID: 5021
		internal const string RegexEditor_BadExpression = "RegexEditor_BadExpression";

		// Token: 0x0400139E RID: 5022
		internal const string RegexEditor_Help = "RegexEditor_Help";

		// Token: 0x0400139F RID: 5023
		internal const string RegexCanned_Custom = "RegexCanned_Custom";

		// Token: 0x040013A0 RID: 5024
		internal const string RegexCanned_Zip = "RegexCanned_Zip";

		// Token: 0x040013A1 RID: 5025
		internal const string RegexCanned_SocialSecurity = "RegexCanned_SocialSecurity";

		// Token: 0x040013A2 RID: 5026
		internal const string RegexCanned_USPhone = "RegexCanned_USPhone";

		// Token: 0x040013A3 RID: 5027
		internal const string RegexCanned_Email = "RegexCanned_Email";

		// Token: 0x040013A4 RID: 5028
		internal const string RegexCanned_URL = "RegexCanned_URL";

		// Token: 0x040013A5 RID: 5029
		internal const string RegexCanned_FrZip = "RegexCanned_FrZip";

		// Token: 0x040013A6 RID: 5030
		internal const string RegexCanned_FrPhone = "RegexCanned_FrPhone";

		// Token: 0x040013A7 RID: 5031
		internal const string RegexCanned_DeZip = "RegexCanned_DeZip";

		// Token: 0x040013A8 RID: 5032
		internal const string RegexCanned_DePhone = "RegexCanned_DePhone";

		// Token: 0x040013A9 RID: 5033
		internal const string RegexCanned_JpnZip = "RegexCanned_JpnZip";

		// Token: 0x040013AA RID: 5034
		internal const string RegexCanned_JpnPhone = "RegexCanned_JpnPhone";

		// Token: 0x040013AB RID: 5035
		internal const string RegexCanned_PrcZip = "RegexCanned_PrcZip";

		// Token: 0x040013AC RID: 5036
		internal const string RegexCanned_PrcPhone = "RegexCanned_PrcPhone";

		// Token: 0x040013AD RID: 5037
		internal const string RegexCanned_PrcSocialSecurity = "RegexCanned_PrcSocialSecurity";

		// Token: 0x040013AE RID: 5038
		internal const string RegexCanned_Zip_Format = "RegexCanned_Zip_Format";

		// Token: 0x040013AF RID: 5039
		internal const string RegexCanned_SocialSecurity_Format = "RegexCanned_SocialSecurity_Format";

		// Token: 0x040013B0 RID: 5040
		internal const string RegexCanned_USPhone_Format = "RegexCanned_USPhone_Format";

		// Token: 0x040013B1 RID: 5041
		internal const string RegexCanned_FrZip_Format = "RegexCanned_FrZip_Format";

		// Token: 0x040013B2 RID: 5042
		internal const string RegexCanned_FrPhone_Format = "RegexCanned_FrPhone_Format";

		// Token: 0x040013B3 RID: 5043
		internal const string RegexCanned_DeZip_Format = "RegexCanned_DeZip_Format";

		// Token: 0x040013B4 RID: 5044
		internal const string RegexCanned_DePhone_Format = "RegexCanned_DePhone_Format";

		// Token: 0x040013B5 RID: 5045
		internal const string RegexCanned_JpnZip_Format = "RegexCanned_JpnZip_Format";

		// Token: 0x040013B6 RID: 5046
		internal const string RegexCanned_JpnPhone_Format = "RegexCanned_JpnPhone_Format";

		// Token: 0x040013B7 RID: 5047
		internal const string RegexCanned_PrcZip_Format = "RegexCanned_PrcZip_Format";

		// Token: 0x040013B8 RID: 5048
		internal const string RegexCanned_PrcPhone_Format = "RegexCanned_PrcPhone_Format";

		// Token: 0x040013B9 RID: 5049
		internal const string RegexCanned_PrcSocialSecurity_Format = "RegexCanned_PrcSocialSecurity_Format";

		// Token: 0x040013BA RID: 5050
		internal const string TemplateEditableDesignerRegion_CannotSetSupportsDataBinding = "TemplateEditableDesignerRegion_CannotSetSupportsDataBinding";

		// Token: 0x040013BB RID: 5051
		internal const string TemplateDefinition_InvalidTemplateProperty = "TemplateDefinition_InvalidTemplateProperty";

		// Token: 0x040013BC RID: 5052
		internal const string WrongType = "WrongType";

		// Token: 0x040013BD RID: 5053
		internal const string Toolbox_OnWebformsPage = "Toolbox_OnWebformsPage";

		// Token: 0x040013BE RID: 5054
		internal const string Toolbox_BadAttributeType = "Toolbox_BadAttributeType";

		// Token: 0x040013BF RID: 5055
		internal const string TreeViewImageGenerator_ExpandImage = "TreeViewImageGenerator_ExpandImage";

		// Token: 0x040013C0 RID: 5056
		internal const string TreeViewImageGenerator_CollapseImage = "TreeViewImageGenerator_CollapseImage";

		// Token: 0x040013C1 RID: 5057
		internal const string TreeViewImageGenerator_NoExpandImage = "TreeViewImageGenerator_NoExpandImage";

		// Token: 0x040013C2 RID: 5058
		internal const string TreeViewImageGenerator_Preview = "TreeViewImageGenerator_Preview";

		// Token: 0x040013C3 RID: 5059
		internal const string TreeViewImageGenerator_Properties = "TreeViewImageGenerator_Properties";

		// Token: 0x040013C4 RID: 5060
		internal const string TreeViewImageGenerator_SampleRoot = "TreeViewImageGenerator_SampleRoot";

		// Token: 0x040013C5 RID: 5061
		internal const string TreeViewImageGenerator_SampleParent = "TreeViewImageGenerator_SampleParent";

		// Token: 0x040013C6 RID: 5062
		internal const string TreeViewImageGenerator_SampleLeaf = "TreeViewImageGenerator_SampleLeaf";

		// Token: 0x040013C7 RID: 5063
		internal const string TreeViewImageGenerator_FolderName = "TreeViewImageGenerator_FolderName";

		// Token: 0x040013C8 RID: 5064
		internal const string TreeViewImageGenerator_DefaultFolderName = "TreeViewImageGenerator_DefaultFolderName";

		// Token: 0x040013C9 RID: 5065
		internal const string TreeViewImageGenerator_Title = "TreeViewImageGenerator_Title";

		// Token: 0x040013CA RID: 5066
		internal const string TreeViewImageGenerator_LineColor = "TreeViewImageGenerator_LineColor";

		// Token: 0x040013CB RID: 5067
		internal const string TreeViewImageGenerator_LineStyle = "TreeViewImageGenerator_LineStyle";

		// Token: 0x040013CC RID: 5068
		internal const string TreeViewImageGenerator_LineWidth = "TreeViewImageGenerator_LineWidth";

		// Token: 0x040013CD RID: 5069
		internal const string TreeViewImageGenerator_LineImageHeight = "TreeViewImageGenerator_LineImageHeight";

		// Token: 0x040013CE RID: 5070
		internal const string TreeViewImageGenerator_LineImageWidth = "TreeViewImageGenerator_LineImageWidth";

		// Token: 0x040013CF RID: 5071
		internal const string TreeViewImageGenerator_LineImagesGenerated = "TreeViewImageGenerator_LineImagesGenerated";

		// Token: 0x040013D0 RID: 5072
		internal const string TreeViewImageGenerator_MissingFolderName = "TreeViewImageGenerator_MissingFolderName";

		// Token: 0x040013D1 RID: 5073
		internal const string TreeViewImageGenerator_NonExistentFolderName = "TreeViewImageGenerator_NonExistentFolderName";

		// Token: 0x040013D2 RID: 5074
		internal const string TreeViewImageGenerator_ProgressBarName = "TreeViewImageGenerator_ProgressBarName";

		// Token: 0x040013D3 RID: 5075
		internal const string TreeViewImageGenerator_ImagePickerFilter = "TreeViewImageGenerator_ImagePickerFilter";

		// Token: 0x040013D4 RID: 5076
		internal const string TreeViewImageGenerator_TransparentColor = "TreeViewImageGenerator_TransparentColor";

		// Token: 0x040013D5 RID: 5077
		internal const string TreeViewImageGenerator_ErrorCreatingFolder = "TreeViewImageGenerator_ErrorCreatingFolder";

		// Token: 0x040013D6 RID: 5078
		internal const string TreeViewImageGenerator_InvalidFolderName = "TreeViewImageGenerator_InvalidFolderName";

		// Token: 0x040013D7 RID: 5079
		internal const string TreeViewImageGenerator_DocumentExists = "TreeViewImageGenerator_DocumentExists";

		// Token: 0x040013D8 RID: 5080
		internal const string TreeViewImageGenerator_ErrorWriting = "TreeViewImageGenerator_ErrorWriting";

		// Token: 0x040013D9 RID: 5081
		internal const string TreeViewImageGenerator_InvalidValue = "TreeViewImageGenerator_InvalidValue";

		// Token: 0x040013DA RID: 5082
		internal const string TreeViewImageGenerator_CouldNotOpenImage = "TreeViewImageGenerator_CouldNotOpenImage";

		// Token: 0x040013DB RID: 5083
		internal const string TreeViewImageGenerator_Yes = "TreeViewImageGenerator_Yes";

		// Token: 0x040013DC RID: 5084
		internal const string TreeViewImageGenerator_No = "TreeViewImageGenerator_No";

		// Token: 0x040013DD RID: 5085
		internal const string TreeViewImageGenerator_YesToAll = "TreeViewImageGenerator_YesToAll";

		// Token: 0x040013DE RID: 5086
		internal const string TreeViewImageGenerator_HelpText = "TreeViewImageGenerator_HelpText";

		// Token: 0x040013DF RID: 5087
		internal const string TreeNodeCollectionEditor_AddRoot = "TreeNodeCollectionEditor_AddRoot";

		// Token: 0x040013E0 RID: 5088
		internal const string TreeNodeCollectionEditor_AddChild = "TreeNodeCollectionEditor_AddChild";

		// Token: 0x040013E1 RID: 5089
		internal const string TreeNodeCollectionEditor_Remove = "TreeNodeCollectionEditor_Remove";

		// Token: 0x040013E2 RID: 5090
		internal const string TreeNodeCollectionEditor_MoveDown = "TreeNodeCollectionEditor_MoveDown";

		// Token: 0x040013E3 RID: 5091
		internal const string TreeNodeCollectionEditor_MoveUp = "TreeNodeCollectionEditor_MoveUp";

		// Token: 0x040013E4 RID: 5092
		internal const string TreeNodeCollectionEditor_Indent = "TreeNodeCollectionEditor_Indent";

		// Token: 0x040013E5 RID: 5093
		internal const string TreeNodeCollectionEditor_Unindent = "TreeNodeCollectionEditor_Unindent";

		// Token: 0x040013E6 RID: 5094
		internal const string TreeNodeCollectionEditor_OK = "TreeNodeCollectionEditor_OK";

		// Token: 0x040013E7 RID: 5095
		internal const string TreeNodeCollectionEditor_Cancel = "TreeNodeCollectionEditor_Cancel";

		// Token: 0x040013E8 RID: 5096
		internal const string TreeNodeCollectionEditor_Nodes = "TreeNodeCollectionEditor_Nodes";

		// Token: 0x040013E9 RID: 5097
		internal const string TreeNodeCollectionEditor_Properties = "TreeNodeCollectionEditor_Properties";

		// Token: 0x040013EA RID: 5098
		internal const string TreeNodeCollectionEditor_Title = "TreeNodeCollectionEditor_Title";

		// Token: 0x040013EB RID: 5099
		internal const string TreeNodeCollectionEditor_NewNodeText = "TreeNodeCollectionEditor_NewNodeText";

		// Token: 0x040013EC RID: 5100
		internal const string TreeViewBindingsEditor_Apply = "TreeViewBindingsEditor_Apply";

		// Token: 0x040013ED RID: 5101
		internal const string TreeViewBindingsEditor_AddBinding = "TreeViewBindingsEditor_AddBinding";

		// Token: 0x040013EE RID: 5102
		internal const string TreeViewBindingsEditor_AutoGenerateBindings = "TreeViewBindingsEditor_AutoGenerateBindings";

		// Token: 0x040013EF RID: 5103
		internal const string TreeViewBindingsEditor_Bindings = "TreeViewBindingsEditor_Bindings";

		// Token: 0x040013F0 RID: 5104
		internal const string TreeViewBindingsEditor_BindingProperties = "TreeViewBindingsEditor_BindingProperties";

		// Token: 0x040013F1 RID: 5105
		internal const string TreeViewBindingsEditor_Cancel = "TreeViewBindingsEditor_Cancel";

		// Token: 0x040013F2 RID: 5106
		internal const string TreeViewBindingsEditor_EmptyBindingText = "TreeViewBindingsEditor_EmptyBindingText";

		// Token: 0x040013F3 RID: 5107
		internal const string TreeViewBindingsEditor_OK = "TreeViewBindingsEditor_OK";

		// Token: 0x040013F4 RID: 5108
		internal const string TreeViewBindingsEditor_Schema = "TreeViewBindingsEditor_Schema";

		// Token: 0x040013F5 RID: 5109
		internal const string TreeViewBindingsEditor_Title = "TreeViewBindingsEditor_Title";

		// Token: 0x040013F6 RID: 5110
		internal const string TreeViewDesigner_CreateLineImagesTransactionDescription = "TreeViewDesigner_CreateLineImagesTransactionDescription";

		// Token: 0x040013F7 RID: 5111
		internal const string TreeViewDesigner_DataActionGroup = "TreeViewDesigner_DataActionGroup";

		// Token: 0x040013F8 RID: 5112
		internal const string TreeViewDesigner_EditBindingsTransactionDescription = "TreeViewDesigner_EditBindingsTransactionDescription";

		// Token: 0x040013F9 RID: 5113
		internal const string TreeViewDesigner_EditNodesTransactionDescription = "TreeViewDesigner_EditNodesTransactionDescription";

		// Token: 0x040013FA RID: 5114
		internal const string TreeViewDesigner_EditNodesDescription = "TreeViewDesigner_EditNodesDescription";

		// Token: 0x040013FB RID: 5115
		internal const string TreeViewDesigner_EditBindings = "TreeViewDesigner_EditBindings";

		// Token: 0x040013FC RID: 5116
		internal const string TreeViewDesigner_EditBindingsDescription = "TreeViewDesigner_EditBindingsDescription";

		// Token: 0x040013FD RID: 5117
		internal const string TreeViewDesigner_EditNodes = "TreeViewDesigner_EditNodes";

		// Token: 0x040013FE RID: 5118
		internal const string TreeViewDesigner_CreateLineImages = "TreeViewDesigner_CreateLineImages";

		// Token: 0x040013FF RID: 5119
		internal const string TreeViewDesigner_CreateLineImagesDescription = "TreeViewDesigner_CreateLineImagesDescription";

		// Token: 0x04001400 RID: 5120
		internal const string TreeViewDesigner_Empty = "TreeViewDesigner_Empty";

		// Token: 0x04001401 RID: 5121
		internal const string TreeViewDesigner_EmptyDataBinding = "TreeViewDesigner_EmptyDataBinding";

		// Token: 0x04001402 RID: 5122
		internal const string TreeViewDesigner_Error = "TreeViewDesigner_Error";

		// Token: 0x04001403 RID: 5123
		internal const string TreeViewDesigner_ShowLines = "TreeViewDesigner_ShowLines";

		// Token: 0x04001404 RID: 5124
		internal const string TreeViewDesigner_ShowLinesDescription = "TreeViewDesigner_ShowLinesDescription";

		// Token: 0x04001405 RID: 5125
		internal const string TreeViewBindingsEditor_MoveBindingUpName = "TreeViewBindingsEditor_MoveBindingUpName";

		// Token: 0x04001406 RID: 5126
		internal const string TreeViewBindingsEditor_MoveBindingUpDescription = "TreeViewBindingsEditor_MoveBindingUpDescription";

		// Token: 0x04001407 RID: 5127
		internal const string TreeViewBindingsEditor_MoveBindingDownName = "TreeViewBindingsEditor_MoveBindingDownName";

		// Token: 0x04001408 RID: 5128
		internal const string TreeViewBindingsEditor_MoveBindingDownDescription = "TreeViewBindingsEditor_MoveBindingDownDescription";

		// Token: 0x04001409 RID: 5129
		internal const string TreeViewBindingsEditor_DeleteBindingName = "TreeViewBindingsEditor_DeleteBindingName";

		// Token: 0x0400140A RID: 5130
		internal const string TreeViewBindingsEditor_DeleteBindingDescription = "TreeViewBindingsEditor_DeleteBindingDescription";

		// Token: 0x0400140B RID: 5131
		internal const string TVScheme_Empty = "TVScheme_Empty";

		// Token: 0x0400140C RID: 5132
		internal const string TVScheme_XP_File_Explorer = "TVScheme_XP_File_Explorer";

		// Token: 0x0400140D RID: 5133
		internal const string TVScheme_MSDN = "TVScheme_MSDN";

		// Token: 0x0400140E RID: 5134
		internal const string TVScheme_Windows_Help = "TVScheme_Windows_Help";

		// Token: 0x0400140F RID: 5135
		internal const string TVScheme_Simple = "TVScheme_Simple";

		// Token: 0x04001410 RID: 5136
		internal const string TVScheme_Simple2 = "TVScheme_Simple2";

		// Token: 0x04001411 RID: 5137
		internal const string TVScheme_BulletedList = "TVScheme_BulletedList";

		// Token: 0x04001412 RID: 5138
		internal const string TVScheme_BulletedList2 = "TVScheme_BulletedList2";

		// Token: 0x04001413 RID: 5139
		internal const string TVScheme_BulletedList3 = "TVScheme_BulletedList3";

		// Token: 0x04001414 RID: 5140
		internal const string TVScheme_BulletedList4 = "TVScheme_BulletedList4";

		// Token: 0x04001415 RID: 5141
		internal const string TVScheme_BulletedList5 = "TVScheme_BulletedList5";

		// Token: 0x04001416 RID: 5142
		internal const string TVScheme_BulletedList6 = "TVScheme_BulletedList6";

		// Token: 0x04001417 RID: 5143
		internal const string TVScheme_Arrows = "TVScheme_Arrows";

		// Token: 0x04001418 RID: 5144
		internal const string TVScheme_Arrows2 = "TVScheme_Arrows2";

		// Token: 0x04001419 RID: 5145
		internal const string TVScheme_TOC = "TVScheme_TOC";

		// Token: 0x0400141A RID: 5146
		internal const string TVScheme_News = "TVScheme_News";

		// Token: 0x0400141B RID: 5147
		internal const string TVScheme_Contacts = "TVScheme_Contacts";

		// Token: 0x0400141C RID: 5148
		internal const string TVScheme_Inbox = "TVScheme_Inbox";

		// Token: 0x0400141D RID: 5149
		internal const string TVScheme_Events = "TVScheme_Events";

		// Token: 0x0400141E RID: 5150
		internal const string TVScheme_FAQ = "TVScheme_FAQ";

		// Token: 0x0400141F RID: 5151
		internal const string UserControlDesigner_MissingID = "UserControlDesigner_MissingID";

		// Token: 0x04001420 RID: 5152
		internal const string UserControlDesigner_EditUserControl = "UserControlDesigner_EditUserControl";

		// Token: 0x04001421 RID: 5153
		internal const string UserControlDesigner_Refresh = "UserControlDesigner_Refresh";

		// Token: 0x04001422 RID: 5154
		internal const string UserControlDesigner_NotFound = "UserControlDesigner_NotFound";

		// Token: 0x04001423 RID: 5155
		internal const string UserControlDesigner_CyclicError = "UserControlDesigner_CyclicError";

		// Token: 0x04001424 RID: 5156
		internal const string WebPartScheme_Empty = "WebPartScheme_Empty";

		// Token: 0x04001425 RID: 5157
		internal const string WebPartScheme_Professional = "WebPartScheme_Professional";

		// Token: 0x04001426 RID: 5158
		internal const string WebPartScheme_Simple = "WebPartScheme_Simple";

		// Token: 0x04001427 RID: 5159
		internal const string WebPartScheme_Classic = "WebPartScheme_Classic";

		// Token: 0x04001428 RID: 5160
		internal const string WebPartScheme_Colorful = "WebPartScheme_Colorful";

		// Token: 0x04001429 RID: 5161
		internal const string CatalogZoneDesigner_OnlyCatalogParts = "CatalogZoneDesigner_OnlyCatalogParts";

		// Token: 0x0400142A RID: 5162
		internal const string CatalogZoneDesigner_Empty = "CatalogZoneDesigner_Empty";

		// Token: 0x0400142B RID: 5163
		internal const string DesignerCatalogPartChrome_TypeCatalogPart = "DesignerCatalogPartChrome_TypeCatalogPart";

		// Token: 0x0400142C RID: 5164
		internal const string DesignerEditorPartChrome_TypeEditorPart = "DesignerEditorPartChrome_TypeEditorPart";

		// Token: 0x0400142D RID: 5165
		internal const string EditorZoneDesigner_OnlyEditorParts = "EditorZoneDesigner_OnlyEditorParts";

		// Token: 0x0400142E RID: 5166
		internal const string EditorZoneDesigner_Empty = "EditorZoneDesigner_Empty";

		// Token: 0x0400142F RID: 5167
		internal const string DeclarativeCatalogPartDesigner_Empty = "DeclarativeCatalogPartDesigner_Empty";

		// Token: 0x04001430 RID: 5168
		internal const string ToolZoneDesigner_ViewInBrowseMode = "ToolZoneDesigner_ViewInBrowseMode";

		// Token: 0x04001431 RID: 5169
		internal const string ToolZoneDesigner_ViewInBrowseModeDesc = "ToolZoneDesigner_ViewInBrowseModeDesc";

		// Token: 0x04001432 RID: 5170
		internal const string WebPartZoneAutoFormat_SampleWebPartTitle = "WebPartZoneAutoFormat_SampleWebPartTitle";

		// Token: 0x04001433 RID: 5171
		internal const string WebPartZoneAutoFormat_SampleWebPartContents = "WebPartZoneAutoFormat_SampleWebPartContents";

		// Token: 0x04001434 RID: 5172
		internal const string CatalogZone_SampleWebPartTitle = "CatalogZone_SampleWebPartTitle";

		// Token: 0x04001435 RID: 5173
		internal const string WebPartZoneDesigner_Empty = "WebPartZoneDesigner_Empty";

		// Token: 0x04001436 RID: 5174
		internal const string WebPartZoneDesigner_Error = "WebPartZoneDesigner_Error";

		// Token: 0x04001437 RID: 5175
		internal const string RTL = "RTL";

		// Token: 0x04001438 RID: 5176
		internal const string Sample_Column = "Sample_Column";

		// Token: 0x04001439 RID: 5177
		internal const string Sample_Databound_Column = "Sample_Databound_Column";

		// Token: 0x0400143A RID: 5178
		internal const string Sample_Databound_Text = "Sample_Databound_Text";

		// Token: 0x0400143B RID: 5179
		internal const string Sample_Databound_Text_Alt = "Sample_Databound_Text_Alt";

		// Token: 0x0400143C RID: 5180
		internal const string Sample_Unbound_Text = "Sample_Unbound_Text";

		// Token: 0x0400143D RID: 5181
		internal const string DesignTimeData_BadDataMember = "DesignTimeData_BadDataMember";

		// Token: 0x0400143E RID: 5182
		internal const string TrayLineUpIcons = "TrayLineUpIcons";

		// Token: 0x0400143F RID: 5183
		internal const string TrayAutoArrange = "TrayAutoArrange";

		// Token: 0x04001440 RID: 5184
		internal const string TrayShowLargeIcons = "TrayShowLargeIcons";

		// Token: 0x04001441 RID: 5185
		internal const string StringDictionaryEditorTitle = "StringDictionaryEditorTitle";

		// Token: 0x04001442 RID: 5186
		internal const string StartFileNameEditorTitle = "StartFileNameEditorTitle";

		// Token: 0x04001443 RID: 5187
		internal const string StartFileNameEditorAllFiles = "StartFileNameEditorAllFiles";

		// Token: 0x04001444 RID: 5188
		internal const string ToolStripItemCollectionEditorVerb = "ToolStripItemCollectionEditorVerb";

		// Token: 0x04001445 RID: 5189
		internal const string ToolStripDropDownItemCollectionEditorVerb = "ToolStripDropDownItemCollectionEditorVerb";

		// Token: 0x04001446 RID: 5190
		internal const string ToolStripItemCollectionEditorLabelNone = "ToolStripItemCollectionEditorLabelNone";

		// Token: 0x04001447 RID: 5191
		internal const string ToolStripItemCollectionEditorLabelMultipleItems = "ToolStripItemCollectionEditorLabelMultipleItems";

		// Token: 0x04001448 RID: 5192
		internal const string ContextMenuViewCode = "ContextMenuViewCode";

		// Token: 0x04001449 RID: 5193
		internal const string ContextMenuDocumentOutline = "ContextMenuDocumentOutline";

		// Token: 0x0400144A RID: 5194
		internal const string ContextMenuBringToFront = "ContextMenuBringToFront";

		// Token: 0x0400144B RID: 5195
		internal const string ContextMenuSendToBack = "ContextMenuSendToBack";

		// Token: 0x0400144C RID: 5196
		internal const string ContextMenuAlignToGrid = "ContextMenuAlignToGrid";

		// Token: 0x0400144D RID: 5197
		internal const string ContextMenuLockControls = "ContextMenuLockControls";

		// Token: 0x0400144E RID: 5198
		internal const string ContextMenuSelect = "ContextMenuSelect";

		// Token: 0x0400144F RID: 5199
		internal const string ContextMenuCut = "ContextMenuCut";

		// Token: 0x04001450 RID: 5200
		internal const string ContextMenuCopy = "ContextMenuCopy";

		// Token: 0x04001451 RID: 5201
		internal const string ContextMenuPaste = "ContextMenuPaste";

		// Token: 0x04001452 RID: 5202
		internal const string ContextMenuDelete = "ContextMenuDelete";

		// Token: 0x04001453 RID: 5203
		internal const string ContextMenuProperties = "ContextMenuProperties";

		// Token: 0x04001454 RID: 5204
		internal const string ToolStripItemContextMenuSetImage = "ToolStripItemContextMenuSetImage";

		// Token: 0x04001455 RID: 5205
		internal const string ToolStripItemContextMenuConvertTo = "ToolStripItemContextMenuConvertTo";

		// Token: 0x04001456 RID: 5206
		internal const string ToolStripItemContextMenuInsert = "ToolStripItemContextMenuInsert";

		// Token: 0x04001457 RID: 5207
		internal const string ToolStripActionList_Name = "ToolStripActionList_Name";

		// Token: 0x04001458 RID: 5208
		internal const string ToolStripActionList_NameDesc = "ToolStripActionList_NameDesc";

		// Token: 0x04001459 RID: 5209
		internal const string ToolStripActionList_Behavior = "ToolStripActionList_Behavior";

		// Token: 0x0400145A RID: 5210
		internal const string ToolStripActionList_Visible = "ToolStripActionList_Visible";

		// Token: 0x0400145B RID: 5211
		internal const string ToolStripActionList_VisibleDesc = "ToolStripActionList_VisibleDesc";

		// Token: 0x0400145C RID: 5212
		internal const string ToolStripActionList_ShowItemToolTips = "ToolStripActionList_ShowItemToolTips";

		// Token: 0x0400145D RID: 5213
		internal const string ToolStripActionList_ShowItemToolTipsDesc = "ToolStripActionList_ShowItemToolTipsDesc";

		// Token: 0x0400145E RID: 5214
		internal const string ToolStripActionList_AllowItemReorder = "ToolStripActionList_AllowItemReorder";

		// Token: 0x0400145F RID: 5215
		internal const string ToolStripActionList_AllowItemReorderDesc = "ToolStripActionList_AllowItemReorderDesc";

		// Token: 0x04001460 RID: 5216
		internal const string ToolStripActionList_CanOverflow = "ToolStripActionList_CanOverflow";

		// Token: 0x04001461 RID: 5217
		internal const string ToolStripActionList_CanOverflowDesc = "ToolStripActionList_CanOverflowDesc";

		// Token: 0x04001462 RID: 5218
		internal const string ToolStripActionList_Layout = "ToolStripActionList_Layout";

		// Token: 0x04001463 RID: 5219
		internal const string ToolStripActionList_Dock = "ToolStripActionList_Dock";

		// Token: 0x04001464 RID: 5220
		internal const string ToolStripActionList_DockDesc = "ToolStripActionList_DockDesc";

		// Token: 0x04001465 RID: 5221
		internal const string ToolStripActionList_Raft = "ToolStripActionList_Raft";

		// Token: 0x04001466 RID: 5222
		internal const string ToolStripActionList_RaftDesc = "ToolStripActionList_RaftDesc";

		// Token: 0x04001467 RID: 5223
		internal const string ToolStripActionList_RenderMode = "ToolStripActionList_RenderMode";

		// Token: 0x04001468 RID: 5224
		internal const string ToolStripActionList_RenderModeDesc = "ToolStripActionList_RenderModeDesc";

		// Token: 0x04001469 RID: 5225
		internal const string ToolStripActionList_GripStyle = "ToolStripActionList_GripStyle";

		// Token: 0x0400146A RID: 5226
		internal const string ToolStripActionList_GripStyleDesc = "ToolStripActionList_GripStyleDesc";

		// Token: 0x0400146B RID: 5227
		internal const string ToolStripActionList_Stretch = "ToolStripActionList_Stretch";

		// Token: 0x0400146C RID: 5228
		internal const string ToolStripActionList_StretchDesc = "ToolStripActionList_StretchDesc";

		// Token: 0x0400146D RID: 5229
		internal const string ToolStripActionList_SizingGrip = "ToolStripActionList_SizingGrip";

		// Token: 0x0400146E RID: 5230
		internal const string ToolStripActionList_SizingGripDesc = "ToolStripActionList_SizingGripDesc";

		// Token: 0x0400146F RID: 5231
		internal const string ToolStripContainerActionList_Show = "ToolStripContainerActionList_Show";

		// Token: 0x04001470 RID: 5232
		internal const string ToolStripContainerActionList_Visible = "ToolStripContainerActionList_Visible";

		// Token: 0x04001471 RID: 5233
		internal const string ToolStripContainerActionList_Top = "ToolStripContainerActionList_Top";

		// Token: 0x04001472 RID: 5234
		internal const string ToolStripContainerActionList_TopDesc = "ToolStripContainerActionList_TopDesc";

		// Token: 0x04001473 RID: 5235
		internal const string ToolStripContainerActionList_Bottom = "ToolStripContainerActionList_Bottom";

		// Token: 0x04001474 RID: 5236
		internal const string ToolStripContainerActionList_BottomDesc = "ToolStripContainerActionList_BottomDesc";

		// Token: 0x04001475 RID: 5237
		internal const string ToolStripContainerActionList_Left = "ToolStripContainerActionList_Left";

		// Token: 0x04001476 RID: 5238
		internal const string ToolStripContainerActionList_LeftDesc = "ToolStripContainerActionList_LeftDesc";

		// Token: 0x04001477 RID: 5239
		internal const string ToolStripContainerActionList_Right = "ToolStripContainerActionList_Right";

		// Token: 0x04001478 RID: 5240
		internal const string ToolStripContainerActionList_RightDesc = "ToolStripContainerActionList_RightDesc";

		// Token: 0x04001479 RID: 5241
		internal const string ContextMenuStripActionList_ShowImageMargin = "ContextMenuStripActionList_ShowImageMargin";

		// Token: 0x0400147A RID: 5242
		internal const string ContextMenuStripActionList_ShowImageMarginDesc = "ContextMenuStripActionList_ShowImageMarginDesc";

		// Token: 0x0400147B RID: 5243
		internal const string ContextMenuStripActionList_ShowCheckMargin = "ContextMenuStripActionList_ShowCheckMargin";

		// Token: 0x0400147C RID: 5244
		internal const string ContextMenuStripActionList_ShowCheckMarginDesc = "ContextMenuStripActionList_ShowCheckMarginDesc";

		// Token: 0x0400147D RID: 5245
		internal const string ContextMenuStripActionList_ShowShortCuts = "ContextMenuStripActionList_ShowShortCuts";

		// Token: 0x0400147E RID: 5246
		internal const string ContextMenuStripActionList_ShowShortCutsDesc = "ContextMenuStripActionList_ShowShortCutsDesc";

		// Token: 0x0400147F RID: 5247
		internal const string ToolStripDesignerTransactionAddingItem = "ToolStripDesignerTransactionAddingItem";

		// Token: 0x04001480 RID: 5248
		internal const string ToolStripDesignerTransactionRemovingItem = "ToolStripDesignerTransactionRemovingItem";

		// Token: 0x04001481 RID: 5249
		internal const string ToolStripDesignerSelectToolStripTransaction = "ToolStripDesignerSelectToolStripTransaction";

		// Token: 0x04001482 RID: 5250
		internal const string ToolStripDesignerStandardItemsVerb = "ToolStripDesignerStandardItemsVerb";

		// Token: 0x04001483 RID: 5251
		internal const string ToolStripDesignerEmbedVerb = "ToolStripDesignerEmbedVerb";

		// Token: 0x04001484 RID: 5252
		internal const string ToolStripDesignerStandardItemsVerbDesc = "ToolStripDesignerStandardItemsVerbDesc";

		// Token: 0x04001485 RID: 5253
		internal const string ToolStripDesignerEmbedVerbDesc = "ToolStripDesignerEmbedVerbDesc";

		// Token: 0x04001486 RID: 5254
		internal const string ToolStripDesignerInsertItemsVerb = "ToolStripDesignerInsertItemsVerb";

		// Token: 0x04001487 RID: 5255
		internal const string ToolStripAddingItem = "ToolStripAddingItem";

		// Token: 0x04001488 RID: 5256
		internal const string ToolStripDesignerSelectAllVerb = "ToolStripDesignerSelectAllVerb";

		// Token: 0x04001489 RID: 5257
		internal const string ToolStripSeparatorError = "ToolStripSeparatorError";

		// Token: 0x0400148A RID: 5258
		internal const string ToolStripCircularReferenceError = "ToolStripCircularReferenceError";

		// Token: 0x0400148B RID: 5259
		internal const string ToolStripDesignerTemplateNodeEnterText = "ToolStripDesignerTemplateNodeEnterText";

		// Token: 0x0400148C RID: 5260
		internal const string ToolStripDesignerTemplateNodeSplitButtonToolTip = "ToolStripDesignerTemplateNodeSplitButtonToolTip";

		// Token: 0x0400148D RID: 5261
		internal const string ToolStripDesignerTemplateNodeLabelToolTip = "ToolStripDesignerTemplateNodeLabelToolTip";

		// Token: 0x0400148E RID: 5262
		internal const string ToolStripDesignerTemplateNodeSplitButtonStatusStripToolTip = "ToolStripDesignerTemplateNodeSplitButtonStatusStripToolTip";

		// Token: 0x0400148F RID: 5263
		internal const string ToolStripDesignerFailedToLoadItemType = "ToolStripDesignerFailedToLoadItemType";

		// Token: 0x04001490 RID: 5264
		internal const string ToolStripDesignerToolStripItemsOnly = "ToolStripDesignerToolStripItemsOnly";

		// Token: 0x04001491 RID: 5265
		internal const string StandardMenuTitle = "StandardMenuTitle";

		// Token: 0x04001492 RID: 5266
		internal const string StandardMenuStripTitle = "StandardMenuStripTitle";

		// Token: 0x04001493 RID: 5267
		internal const string StandardMenuFile = "StandardMenuFile";

		// Token: 0x04001494 RID: 5268
		internal const string StandardMenuNew = "StandardMenuNew";

		// Token: 0x04001495 RID: 5269
		internal const string StandardMenuOpen = "StandardMenuOpen";

		// Token: 0x04001496 RID: 5270
		internal const string StandardMenuSave = "StandardMenuSave";

		// Token: 0x04001497 RID: 5271
		internal const string StandardMenuSaveAs = "StandardMenuSaveAs";

		// Token: 0x04001498 RID: 5272
		internal const string StandardMenuPrint = "StandardMenuPrint";

		// Token: 0x04001499 RID: 5273
		internal const string StandardMenuPrintPreview = "StandardMenuPrintPreview";

		// Token: 0x0400149A RID: 5274
		internal const string StandardMenuExit = "StandardMenuExit";

		// Token: 0x0400149B RID: 5275
		internal const string StandardMenuEdit = "StandardMenuEdit";

		// Token: 0x0400149C RID: 5276
		internal const string StandardMenuUndo = "StandardMenuUndo";

		// Token: 0x0400149D RID: 5277
		internal const string StandardMenuRedo = "StandardMenuRedo";

		// Token: 0x0400149E RID: 5278
		internal const string StandardMenuCut = "StandardMenuCut";

		// Token: 0x0400149F RID: 5279
		internal const string StandardToolCut = "StandardToolCut";

		// Token: 0x040014A0 RID: 5280
		internal const string StandardMenuCopy = "StandardMenuCopy";

		// Token: 0x040014A1 RID: 5281
		internal const string StandardMenuPaste = "StandardMenuPaste";

		// Token: 0x040014A2 RID: 5282
		internal const string StandardMenuDelete = "StandardMenuDelete";

		// Token: 0x040014A3 RID: 5283
		internal const string StandardMenuSelectAll = "StandardMenuSelectAll";

		// Token: 0x040014A4 RID: 5284
		internal const string StandardMenuTools = "StandardMenuTools";

		// Token: 0x040014A5 RID: 5285
		internal const string StandardMenuCustomize = "StandardMenuCustomize";

		// Token: 0x040014A6 RID: 5286
		internal const string StandardMenuOptions = "StandardMenuOptions";

		// Token: 0x040014A7 RID: 5287
		internal const string StandardMenuHelp = "StandardMenuHelp";

		// Token: 0x040014A8 RID: 5288
		internal const string StandardToolHelp = "StandardToolHelp";

		// Token: 0x040014A9 RID: 5289
		internal const string StandardMenuContents = "StandardMenuContents";

		// Token: 0x040014AA RID: 5290
		internal const string StandardMenuIndex = "StandardMenuIndex";

		// Token: 0x040014AB RID: 5291
		internal const string StandardMenuSearch = "StandardMenuSearch";

		// Token: 0x040014AC RID: 5292
		internal const string StandardMenuAbout = "StandardMenuAbout";

		// Token: 0x040014AD RID: 5293
		internal const string StandardMenuCreateDesc = "StandardMenuCreateDesc";

		// Token: 0x040014AE RID: 5294
		internal const string CG_DataSetGeneratorFail_InputFileEmpty = "CG_DataSetGeneratorFail_InputFileEmpty";

		// Token: 0x040014AF RID: 5295
		internal const string CG_DataSetGeneratorFail_DatasetNull = "CG_DataSetGeneratorFail_DatasetNull";

		// Token: 0x040014B0 RID: 5296
		internal const string CG_DataSetGeneratorFail_CodeGeneratorNull = "CG_DataSetGeneratorFail_CodeGeneratorNull";

		// Token: 0x040014B1 RID: 5297
		internal const string CG_DataSetGeneratorFail_CodeNamespaceNull = "CG_DataSetGeneratorFail_CodeNamespaceNull";

		// Token: 0x040014B2 RID: 5298
		internal const string CG_DataSetGeneratorFail_UnableToConvertToDataSet = "CG_DataSetGeneratorFail_UnableToConvertToDataSet";

		// Token: 0x040014B3 RID: 5299
		internal const string CG_DataSetGeneratorFail_FailToGenerateCode = "CG_DataSetGeneratorFail_FailToGenerateCode";

		// Token: 0x040014B4 RID: 5300
		internal const string CG_TypeCantBeNull = "CG_TypeCantBeNull";

		// Token: 0x040014B5 RID: 5301
		internal const string CG_NoCtor0 = "CG_NoCtor0";

		// Token: 0x040014B6 RID: 5302
		internal const string CG_NoCtor1 = "CG_NoCtor1";

		// Token: 0x040014B7 RID: 5303
		internal const string CG_MainSelectCommandNotSet = "CG_MainSelectCommandNotSet";

		// Token: 0x040014B8 RID: 5304
		internal const string CG_UnableToReadExtProperties = "CG_UnableToReadExtProperties";

		// Token: 0x040014B9 RID: 5305
		internal const string CG_UnableToConvertSqlDbTypeToSqlType = "CG_UnableToConvertSqlDbTypeToSqlType";

		// Token: 0x040014BA RID: 5306
		internal const string CG_UnableToConvertDbTypeToUrtType = "CG_UnableToConvertDbTypeToUrtType";

		// Token: 0x040014BB RID: 5307
		internal const string CG_RowColumnPropertyNameFixup = "CG_RowColumnPropertyNameFixup";

		// Token: 0x040014BC RID: 5308
		internal const string CG_DataSourceClassNameFixup = "CG_DataSourceClassNameFixup";

		// Token: 0x040014BD RID: 5309
		internal const string CG_TablePropertyNameFixup = "CG_TablePropertyNameFixup";

		// Token: 0x040014BE RID: 5310
		internal const string CG_TableSourceNameFixup = "CG_TableSourceNameFixup";

		// Token: 0x040014BF RID: 5311
		internal const string CG_EmptyDSName = "CG_EmptyDSName";

		// Token: 0x040014C0 RID: 5312
		internal const string CG_ColumnIsDBNull = "CG_ColumnIsDBNull";

		// Token: 0x040014C1 RID: 5313
		internal const string CG_ParameterIsDBNull = "CG_ParameterIsDBNull";

		// Token: 0x040014C2 RID: 5314
		internal const string CG_TableAdapterManagerNeedsSameConnString = "CG_TableAdapterManagerNeedsSameConnString";

		// Token: 0x040014C3 RID: 5315
		internal const string CG_TableAdapterManagerHasNoConnection = "CG_TableAdapterManagerHasNoConnection";

		// Token: 0x040014C4 RID: 5316
		internal const string CG_TableAdapterManagerNotSupportTransaction = "CG_TableAdapterManagerNotSupportTransaction";

		// Token: 0x040014C5 RID: 5317
		internal const string DTDS_CouldNotDeserializeConnection = "DTDS_CouldNotDeserializeConnection";

		// Token: 0x040014C6 RID: 5318
		internal const string DTDS_CouldNotDeserializeXmlElement = "DTDS_CouldNotDeserializeXmlElement";

		// Token: 0x040014C7 RID: 5319
		internal const string DTDS_NameIsRequired = "DTDS_NameIsRequired";

		// Token: 0x040014C8 RID: 5320
		internal const string DTDS_NameConflict = "DTDS_NameConflict";

		// Token: 0x040014C9 RID: 5321
		internal const string DTDS_TableNotMatch = "DTDS_TableNotMatch";

		// Token: 0x040014CA RID: 5322
		internal const string DD_E_TableDirectValidForOleDbOnly = "DD_E_TableDirectValidForOleDbOnly";

		// Token: 0x040014CB RID: 5323
		internal const string CM_NameNotEmptyExcption = "CM_NameNotEmptyExcption";

		// Token: 0x040014CC RID: 5324
		internal const string CM_NameTooLongExcption = "CM_NameTooLongExcption";

		// Token: 0x040014CD RID: 5325
		internal const string CM_NameInvalid = "CM_NameInvalid";

		// Token: 0x040014CE RID: 5326
		internal const string CM_NameExist = "CM_NameExist";

		// Token: 0x040014CF RID: 5327
		internal const string PropertiesCategoryName = "PropertiesCategoryName";

		// Token: 0x040014D0 RID: 5328
		internal const string LinksCategoryName = "LinksCategoryName";

		// Token: 0x040014D1 RID: 5329
		internal const string ItemsCategoryName = "ItemsCategoryName";

		// Token: 0x040014D2 RID: 5330
		internal const string DataCategoryName = "DataCategoryName";

		// Token: 0x040014D3 RID: 5331
		internal const string ImageListActionListImageSizeDisplayName = "ImageListActionListImageSizeDisplayName";

		// Token: 0x040014D4 RID: 5332
		internal const string ImageListActionListImageSizeDescription = "ImageListActionListImageSizeDescription";

		// Token: 0x040014D5 RID: 5333
		internal const string ImageListActionListColorDepthDisplayName = "ImageListActionListColorDepthDisplayName";

		// Token: 0x040014D6 RID: 5334
		internal const string ImageListActionListColorDepthDescription = "ImageListActionListColorDepthDescription";

		// Token: 0x040014D7 RID: 5335
		internal const string ImageListActionListChooseImagesDisplayName = "ImageListActionListChooseImagesDisplayName";

		// Token: 0x040014D8 RID: 5336
		internal const string ImageListActionListChooseImagesDescription = "ImageListActionListChooseImagesDescription";

		// Token: 0x040014D9 RID: 5337
		internal const string ListControlUnboundActionListEditItemsDisplayName = "ListControlUnboundActionListEditItemsDisplayName";

		// Token: 0x040014DA RID: 5338
		internal const string ListControlUnboundActionListEditItemsDescription = "ListControlUnboundActionListEditItemsDescription";

		// Token: 0x040014DB RID: 5339
		internal const string ListViewActionListEditItemsDisplayName = "ListViewActionListEditItemsDisplayName";

		// Token: 0x040014DC RID: 5340
		internal const string ListViewActionListEditItemsDescription = "ListViewActionListEditItemsDescription";

		// Token: 0x040014DD RID: 5341
		internal const string ListViewActionListEditColumnsDisplayName = "ListViewActionListEditColumnsDisplayName";

		// Token: 0x040014DE RID: 5342
		internal const string ListViewActionListEditColumnsDescription = "ListViewActionListEditColumnsDescription";

		// Token: 0x040014DF RID: 5343
		internal const string ListViewActionListEditGroupsDisplayName = "ListViewActionListEditGroupsDisplayName";

		// Token: 0x040014E0 RID: 5344
		internal const string ListViewActionListEditGroupsDescription = "ListViewActionListEditGroupsDescription";

		// Token: 0x040014E1 RID: 5345
		internal const string ListViewActionListViewDisplayName = "ListViewActionListViewDisplayName";

		// Token: 0x040014E2 RID: 5346
		internal const string ListViewActionListViewDescription = "ListViewActionListViewDescription";

		// Token: 0x040014E3 RID: 5347
		internal const string ListViewActionListSmallImagesDisplayName = "ListViewActionListSmallImagesDisplayName";

		// Token: 0x040014E4 RID: 5348
		internal const string ListViewActionListSmallImagesDescription = "ListViewActionListSmallImagesDescription";

		// Token: 0x040014E5 RID: 5349
		internal const string ListViewActionListLargeImagesDisplayName = "ListViewActionListLargeImagesDisplayName";

		// Token: 0x040014E6 RID: 5350
		internal const string ListViewActionListLargeImagesDescription = "ListViewActionListLargeImagesDescription";

		// Token: 0x040014E7 RID: 5351
		internal const string BoundModeHeader = "BoundModeHeader";

		// Token: 0x040014E8 RID: 5352
		internal const string UnBoundModeHeader = "UnBoundModeHeader";

		// Token: 0x040014E9 RID: 5353
		internal const string BoundModeDisplayName = "BoundModeDisplayName";

		// Token: 0x040014EA RID: 5354
		internal const string BoundModeDescription = "BoundModeDescription";

		// Token: 0x040014EB RID: 5355
		internal const string DataSourceDisplayName = "DataSourceDisplayName";

		// Token: 0x040014EC RID: 5356
		internal const string DataSourceDescription = "DataSourceDescription";

		// Token: 0x040014ED RID: 5357
		internal const string DisplayMemberDisplayName = "DisplayMemberDisplayName";

		// Token: 0x040014EE RID: 5358
		internal const string DisplayMemberDescription = "DisplayMemberDescription";

		// Token: 0x040014EF RID: 5359
		internal const string ValueMemberDisplayName = "ValueMemberDisplayName";

		// Token: 0x040014F0 RID: 5360
		internal const string ValueMemberDescription = "ValueMemberDescription";

		// Token: 0x040014F1 RID: 5361
		internal const string BoundSelectedValueDisplayName = "BoundSelectedValueDisplayName";

		// Token: 0x040014F2 RID: 5362
		internal const string BoundSelectedValueDescription = "BoundSelectedValueDescription";

		// Token: 0x040014F3 RID: 5363
		internal const string EditItemDisplayName = "EditItemDisplayName";

		// Token: 0x040014F4 RID: 5364
		internal const string EditItemDescription = "EditItemDescription";

		// Token: 0x040014F5 RID: 5365
		internal const string ChooseImageDisplayName = "ChooseImageDisplayName";

		// Token: 0x040014F6 RID: 5366
		internal const string ChooseImageDescription = "ChooseImageDescription";

		// Token: 0x040014F7 RID: 5367
		internal const string SizeModeDisplayName = "SizeModeDisplayName";

		// Token: 0x040014F8 RID: 5368
		internal const string SizeModeDescription = "SizeModeDescription";

		// Token: 0x040014F9 RID: 5369
		internal const string EditLinesDisplayName = "EditLinesDisplayName";

		// Token: 0x040014FA RID: 5370
		internal const string EditLinesDescription = "EditLinesDescription";

		// Token: 0x040014FB RID: 5371
		internal const string MultiLineDisplayName = "MultiLineDisplayName";

		// Token: 0x040014FC RID: 5372
		internal const string MultiLineDescription = "MultiLineDescription";

		// Token: 0x040014FD RID: 5373
		internal const string ChooseIconDisplayName = "ChooseIconDisplayName";

		// Token: 0x040014FE RID: 5374
		internal const string InvokeNodesDialogDisplayName = "InvokeNodesDialogDisplayName";

		// Token: 0x040014FF RID: 5375
		internal const string InvokeNodesDialogDescription = "InvokeNodesDialogDescription";

		// Token: 0x04001500 RID: 5376
		internal const string ImageListDisplayName = "ImageListDisplayName";

		// Token: 0x04001501 RID: 5377
		internal const string ImageListDescription = "ImageListDescription";

		// Token: 0x04001502 RID: 5378
		internal const string DesignerOptions_LayoutSettings = "DesignerOptions_LayoutSettings";

		// Token: 0x04001503 RID: 5379
		internal const string DesignerOptions_ObjectBoundSmartTagSettings = "DesignerOptions_ObjectBoundSmartTagSettings";

		// Token: 0x04001504 RID: 5380
		internal const string DesignerOptions_GridSizeDesc = "DesignerOptions_GridSizeDesc";

		// Token: 0x04001505 RID: 5381
		internal const string DesignerOptions_GridSizeDisplayName = "DesignerOptions_GridSizeDisplayName";

		// Token: 0x04001506 RID: 5382
		internal const string DesignerOptions_ShowGridDesc = "DesignerOptions_ShowGridDesc";

		// Token: 0x04001507 RID: 5383
		internal const string DesignerOptions_ShowGridDisplayName = "DesignerOptions_ShowGridDisplayName";

		// Token: 0x04001508 RID: 5384
		internal const string DesignerOptions_SnapToGridDesc = "DesignerOptions_SnapToGridDesc";

		// Token: 0x04001509 RID: 5385
		internal const string DesignerOptions_SnapToGridDisplayName = "DesignerOptions_SnapToGridDisplayName";

		// Token: 0x0400150A RID: 5386
		internal const string DesignerOptions_UseSnapLines = "DesignerOptions_UseSnapLines";

		// Token: 0x0400150B RID: 5387
		internal const string DesignerOptions_UseSmartTags = "DesignerOptions_UseSmartTags";

		// Token: 0x0400150C RID: 5388
		internal const string DesignerOptions_ObjectBoundSmartTagAutoShow = "DesignerOptions_ObjectBoundSmartTagAutoShow";

		// Token: 0x0400150D RID: 5389
		internal const string DesignerOptions_ObjectBoundSmartTagAutoShowDisplayName = "DesignerOptions_ObjectBoundSmartTagAutoShowDisplayName";

		// Token: 0x0400150E RID: 5390
		internal const string DesignerOptions_CodeGenSettings = "DesignerOptions_CodeGenSettings";

		// Token: 0x0400150F RID: 5391
		internal const string DesignerOptions_OptimizedCodeGen = "DesignerOptions_OptimizedCodeGen";

		// Token: 0x04001510 RID: 5392
		internal const string DesignerOptions_CodeGenDisplay = "DesignerOptions_CodeGenDisplay";

		// Token: 0x04001511 RID: 5393
		internal const string DesignerOptions_EnableInSituEditingDisplay = "DesignerOptions_EnableInSituEditingDisplay";

		// Token: 0x04001512 RID: 5394
		internal const string DesignerOptions_EnableInSituEditingCat = "DesignerOptions_EnableInSituEditingCat";

		// Token: 0x04001513 RID: 5395
		internal const string DesignerOptions_EnableInSituEditingDesc = "DesignerOptions_EnableInSituEditingDesc";

		// Token: 0x04001514 RID: 5396
		internal const string ClassComments1 = "ClassComments1";

		// Token: 0x04001515 RID: 5397
		internal const string ClassComments2 = "ClassComments2";

		// Token: 0x04001516 RID: 5398
		internal const string ClassComments3 = "ClassComments3";

		// Token: 0x04001517 RID: 5399
		internal const string ClassComments4 = "ClassComments4";

		// Token: 0x04001518 RID: 5400
		internal const string ClassDocComment = "ClassDocComment";

		// Token: 0x04001519 RID: 5401
		internal const string StringPropertyComment = "StringPropertyComment";

		// Token: 0x0400151A RID: 5402
		internal const string StringPropertyTruncatedComment = "StringPropertyTruncatedComment";

		// Token: 0x0400151B RID: 5403
		internal const string NonStringPropertyComment = "NonStringPropertyComment";

		// Token: 0x0400151C RID: 5404
		internal const string NonStringPropertyDetailedComment = "NonStringPropertyDetailedComment";

		// Token: 0x0400151D RID: 5405
		internal const string CulturePropertyComment1 = "CulturePropertyComment1";

		// Token: 0x0400151E RID: 5406
		internal const string CulturePropertyComment2 = "CulturePropertyComment2";

		// Token: 0x0400151F RID: 5407
		internal const string ResMgrPropertyComment = "ResMgrPropertyComment";

		// Token: 0x04001520 RID: 5408
		internal const string MismatchedResourceName = "MismatchedResourceName";

		// Token: 0x04001521 RID: 5409
		internal const string InvalidIdentifier = "InvalidIdentifier";

		// Token: 0x04001522 RID: 5410
		internal const string DirectiveRegistry_UnknownFramework = "DirectiveRegistry_UnknownFramework";

		// Token: 0x04001523 RID: 5411
		private static SR loader;

		// Token: 0x04001524 RID: 5412
		private ResourceManager resources;
	}
}
