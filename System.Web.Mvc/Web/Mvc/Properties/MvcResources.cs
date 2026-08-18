using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc.Properties
{
	// Token: 0x02000047 RID: 71
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class MvcResources
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000665A File Offset: 0x0000485A
		internal MvcResources()
		{
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006664 File Offset: 0x00004864
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(MvcResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.Mvc.Properties.MvcResources", typeof(MvcResources).Assembly);
					MvcResources.resourceMan = resourceManager;
				}
				return MvcResources.resourceMan;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000066A3 File Offset: 0x000048A3
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000066AA File Offset: 0x000048AA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return MvcResources.resourceCulture;
			}
			set
			{
				MvcResources.resourceCulture = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000066B2 File Offset: 0x000048B2
		internal static string ActionMethodSelector_AmbiguousMatch
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ActionMethodSelector_AmbiguousMatch", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000066C8 File Offset: 0x000048C8
		internal static string ActionMethodSelector_AmbiguousMatchType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ActionMethodSelector_AmbiguousMatchType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000066DE File Offset: 0x000048DE
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000066F4 File Offset: 0x000048F4
		internal static string AsyncActionDescriptor_CannotExecuteSynchronously
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AsyncActionDescriptor_CannotExecuteSynchronously", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000670A File Offset: 0x0000490A
		internal static string AsyncActionMethodSelector_AmbiguousMethodMatch
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AsyncActionMethodSelector_AmbiguousMethodMatch", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006720 File Offset: 0x00004920
		internal static string AsyncActionMethodSelector_CouldNotFindMethod
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AsyncActionMethodSelector_CouldNotFindMethod", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006736 File Offset: 0x00004936
		internal static string AsyncCommon_AsyncResultAlreadyConsumed
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AsyncCommon_AsyncResultAlreadyConsumed", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000674C File Offset: 0x0000494C
		internal static string AsyncCommon_ControllerMustImplementIAsyncManagerContainer
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AsyncCommon_ControllerMustImplementIAsyncManagerContainer", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006762 File Offset: 0x00004962
		internal static string AsyncCommon_InvalidAsyncResult
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AsyncCommon_InvalidAsyncResult", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006778 File Offset: 0x00004978
		internal static string AsyncCommon_InvalidTimeout
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AsyncCommon_InvalidTimeout", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000678E File Offset: 0x0000498E
		internal static string AttributeRouting_CouldNotInferAreaNameFromMissingNamespace
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AttributeRouting_CouldNotInferAreaNameFromMissingNamespace", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000067A4 File Offset: 0x000049A4
		internal static string AuthorizeAttribute_CannotUseWithinChildActionCache
		{
			get
			{
				return MvcResources.ResourceManager.GetString("AuthorizeAttribute_CannotUseWithinChildActionCache", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000067BA File Offset: 0x000049BA
		internal static string ChildActionOnlyAttribute_MustBeInChildRequest
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ChildActionOnlyAttribute_MustBeInChildRequest", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000067D0 File Offset: 0x000049D0
		internal static string ClientDataTypeModelValidatorProvider_FieldMustBeDate
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ClientDataTypeModelValidatorProvider_FieldMustBeDate", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000067E6 File Offset: 0x000049E6
		internal static string ClientDataTypeModelValidatorProvider_FieldMustBeNumeric
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ClientDataTypeModelValidatorProvider_FieldMustBeNumeric", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000067FC File Offset: 0x000049FC
		internal static string Common_NoRouteMatched
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_NoRouteMatched", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006812 File Offset: 0x00004A12
		internal static string Common_NullOrEmpty
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_NullOrEmpty", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006828 File Offset: 0x00004A28
		internal static string Common_PartialViewNotFound
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_PartialViewNotFound", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000683E File Offset: 0x00004A3E
		internal static string Common_PropertyCannotBeNullOrEmpty
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_PropertyCannotBeNullOrEmpty", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006854 File Offset: 0x00004A54
		internal static string Common_PropertyNotFound
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_PropertyNotFound", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000686A File Offset: 0x00004A6A
		internal static string Common_TriState_False
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_TriState_False", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006880 File Offset: 0x00004A80
		internal static string Common_TriState_NotSet
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_TriState_NotSet", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006896 File Offset: 0x00004A96
		internal static string Common_TriState_True
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_TriState_True", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000068AC File Offset: 0x00004AAC
		internal static string Common_TypeMustDriveFromType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_TypeMustDriveFromType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000068C2 File Offset: 0x00004AC2
		internal static string Common_ValueNotValidForProperty
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_ValueNotValidForProperty", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000068D8 File Offset: 0x00004AD8
		internal static string Common_ViewNotFound
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Common_ViewNotFound", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000068EE File Offset: 0x00004AEE
		internal static string CompareAttribute_MustMatch
		{
			get
			{
				return MvcResources.ResourceManager.GetString("CompareAttribute_MustMatch", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00006904 File Offset: 0x00004B04
		internal static string CompareAttribute_UnknownProperty
		{
			get
			{
				return MvcResources.ResourceManager.GetString("CompareAttribute_UnknownProperty", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000691A File Offset: 0x00004B1A
		internal static string Controller_UnknownAction
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Controller_UnknownAction", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006930 File Offset: 0x00004B30
		internal static string Controller_UnknownAction_NoActionName
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Controller_UnknownAction_NoActionName", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006946 File Offset: 0x00004B46
		internal static string Controller_UpdateModel_UpdateUnsuccessful
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Controller_UpdateModel_UpdateUnsuccessful", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000695C File Offset: 0x00004B5C
		internal static string Controller_Validate_ValidationFailed
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Controller_Validate_ValidationFailed", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006972 File Offset: 0x00004B72
		internal static string ControllerBase_CannotExecuteWithNullHttpContext
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ControllerBase_CannotExecuteWithNullHttpContext", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006988 File Offset: 0x00004B88
		internal static string ControllerBase_CannotHandleMultipleRequests
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ControllerBase_CannotHandleMultipleRequests", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000699E File Offset: 0x00004B9E
		internal static string ControllerBuilder_ErrorCreatingControllerFactory
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ControllerBuilder_ErrorCreatingControllerFactory", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000069B4 File Offset: 0x00004BB4
		internal static string ControllerBuilder_FactoryReturnedNull
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ControllerBuilder_FactoryReturnedNull", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000069CA File Offset: 0x00004BCA
		internal static string ControllerBuilder_MissingIControllerFactory
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ControllerBuilder_MissingIControllerFactory", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000069E0 File Offset: 0x00004BE0
		internal static string CshtmlView_ViewCouldNotBeCreated
		{
			get
			{
				return MvcResources.ResourceManager.GetString("CshtmlView_ViewCouldNotBeCreated", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000069F6 File Offset: 0x00004BF6
		internal static string CshtmlView_WrongViewBase
		{
			get
			{
				return MvcResources.ResourceManager.GetString("CshtmlView_WrongViewBase", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006A0C File Offset: 0x00004C0C
		internal static string DataAnnotationsModelMetadataProvider_UnknownProperty
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DataAnnotationsModelMetadataProvider_UnknownProperty", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006A22 File Offset: 0x00004C22
		internal static string DataAnnotationsModelMetadataProvider_UnreadableProperty
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DataAnnotationsModelMetadataProvider_UnreadableProperty", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006A38 File Offset: 0x00004C38
		internal static string DataAnnotationsModelValidatorProvider_ConstructorRequirements
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DataAnnotationsModelValidatorProvider_ConstructorRequirements", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006A4E File Offset: 0x00004C4E
		internal static string DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006A64 File Offset: 0x00004C64
		internal static string DefaultControllerFactory_ControllerNameAmbiguous_WithoutRouteUrl
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultControllerFactory_ControllerNameAmbiguous_WithoutRouteUrl", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006A7A File Offset: 0x00004C7A
		internal static string DefaultControllerFactory_ControllerNameAmbiguous_WithRouteUrl
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultControllerFactory_ControllerNameAmbiguous_WithRouteUrl", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006A90 File Offset: 0x00004C90
		internal static string DefaultControllerFactory_DirectRouteAmbiguous
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultControllerFactory_DirectRouteAmbiguous", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006AA6 File Offset: 0x00004CA6
		internal static string DefaultControllerFactory_ErrorCreatingController
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultControllerFactory_ErrorCreatingController", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006ABC File Offset: 0x00004CBC
		internal static string DefaultControllerFactory_NoControllerFound
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultControllerFactory_NoControllerFound", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00006AD2 File Offset: 0x00004CD2
		internal static string DefaultControllerFactory_TypeDoesNotSubclassControllerBase
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultControllerFactory_TypeDoesNotSubclassControllerBase", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006AE8 File Offset: 0x00004CE8
		internal static string DefaultInlineConstraintResolver_AmbiguousCtors
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultInlineConstraintResolver_AmbiguousCtors", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006AFE File Offset: 0x00004CFE
		internal static string DefaultInlineConstraintResolver_CouldNotFindCtor
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultInlineConstraintResolver_CouldNotFindCtor", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006B14 File Offset: 0x00004D14
		internal static string DefaultInlineConstraintResolver_TypeNotConstraint
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultInlineConstraintResolver_TypeNotConstraint", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006B2A File Offset: 0x00004D2A
		internal static string DefaultModelBinder_ValueInvalid
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultModelBinder_ValueInvalid", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006B40 File Offset: 0x00004D40
		internal static string DefaultModelBinder_ValueRequired
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultModelBinder_ValueRequired", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006B56 File Offset: 0x00004D56
		internal static string DefaultViewLocationCache_NegativeTimeSpan
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DefaultViewLocationCache_NegativeTimeSpan", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006B6C File Offset: 0x00004D6C
		internal static string DependencyResolver_DoesNotImplementICommonServiceLocator
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DependencyResolver_DoesNotImplementICommonServiceLocator", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006B82 File Offset: 0x00004D82
		internal static string DirectRoute_AmbiguousMatch
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DirectRoute_AmbiguousMatch", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006B98 File Offset: 0x00004D98
		internal static string DirectRoute_InvalidParameter_Action
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DirectRoute_InvalidParameter_Action", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006BAE File Offset: 0x00004DAE
		internal static string DirectRoute_InvalidParameter_Controller
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DirectRoute_InvalidParameter_Controller", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006BC4 File Offset: 0x00004DC4
		internal static string DirectRoute_MissingActionDescriptors
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DirectRoute_MissingActionDescriptors", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00006BDA File Offset: 0x00004DDA
		internal static string DirectRoute_MissingControllerDescriptor
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DirectRoute_MissingControllerDescriptor", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006BF0 File Offset: 0x00004DF0
		internal static string DirectRoute_MissingControllerType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DirectRoute_MissingControllerType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006C06 File Offset: 0x00004E06
		internal static string DirectRoute_RouteHandlerNotSupported
		{
			get
			{
				return MvcResources.ResourceManager.GetString("DirectRoute_RouteHandlerNotSupported", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006C1C File Offset: 0x00004E1C
		internal static string EnumHelper_InvalidMetadataParameter
		{
			get
			{
				return MvcResources.ResourceManager.GetString("EnumHelper_InvalidMetadataParameter", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006C32 File Offset: 0x00004E32
		internal static string EnumHelper_InvalidParameterType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("EnumHelper_InvalidParameterType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006C48 File Offset: 0x00004E48
		internal static string EnumHelper_InvalidValueParameter
		{
			get
			{
				return MvcResources.ResourceManager.GetString("EnumHelper_InvalidValueParameter", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006C5E File Offset: 0x00004E5E
		internal static string ExceptionViewAttribute_NonExceptionType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ExceptionViewAttribute_NonExceptionType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006C74 File Offset: 0x00004E74
		internal static string ExpressionHelper_InvalidIndexerExpression
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ExpressionHelper_InvalidIndexerExpression", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006C8A File Offset: 0x00004E8A
		internal static string FilterAttribute_OrderOutOfRange
		{
			get
			{
				return MvcResources.ResourceManager.GetString("FilterAttribute_OrderOutOfRange", MvcResources.resourceCulture);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006CA0 File Offset: 0x00004EA0
		internal static string GlobalFilterCollection_UnsupportedFilterInstance
		{
			get
			{
				return MvcResources.ResourceManager.GetString("GlobalFilterCollection_UnsupportedFilterInstance", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006CB6 File Offset: 0x00004EB6
		internal static string HtmlHelper_InvalidHttpMethod
		{
			get
			{
				return MvcResources.ResourceManager.GetString("HtmlHelper_InvalidHttpMethod", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006CCC File Offset: 0x00004ECC
		internal static string HtmlHelper_InvalidHttpVerb
		{
			get
			{
				return MvcResources.ResourceManager.GetString("HtmlHelper_InvalidHttpVerb", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006CE2 File Offset: 0x00004EE2
		internal static string HtmlHelper_MissingSelectData
		{
			get
			{
				return MvcResources.ResourceManager.GetString("HtmlHelper_MissingSelectData", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006CF8 File Offset: 0x00004EF8
		internal static string HtmlHelper_SelectExpressionNotEnumerable
		{
			get
			{
				return MvcResources.ResourceManager.GetString("HtmlHelper_SelectExpressionNotEnumerable", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006D0E File Offset: 0x00004F0E
		internal static string HtmlHelper_TextAreaParameterOutOfRange
		{
			get
			{
				return MvcResources.ResourceManager.GetString("HtmlHelper_TextAreaParameterOutOfRange", MvcResources.resourceCulture);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006D24 File Offset: 0x00004F24
		internal static string HtmlHelper_WrongSelectDataType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("HtmlHelper_WrongSelectDataType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006D3A File Offset: 0x00004F3A
		internal static string HttpRouteBuilder_CouldNotResolveConstraint
		{
			get
			{
				return MvcResources.ResourceManager.GetString("HttpRouteBuilder_CouldNotResolveConstraint", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006D50 File Offset: 0x00004F50
		internal static string JQuerySyntaxMissingClosingBracket
		{
			get
			{
				return MvcResources.ResourceManager.GetString("JQuerySyntaxMissingClosingBracket", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00006D66 File Offset: 0x00004F66
		internal static string JsonRequest_GetNotAllowed
		{
			get
			{
				return MvcResources.ResourceManager.GetString("JsonRequest_GetNotAllowed", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006D7C File Offset: 0x00004F7C
		internal static string JsonValueProviderFactory_RequestTooLarge
		{
			get
			{
				return MvcResources.ResourceManager.GetString("JsonValueProviderFactory_RequestTooLarge", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006D92 File Offset: 0x00004F92
		internal static string ModelBinderAttribute_ErrorCreatingModelBinder
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ModelBinderAttribute_ErrorCreatingModelBinder", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006DA8 File Offset: 0x00004FA8
		internal static string ModelBinderAttribute_TypeNotIModelBinder
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ModelBinderAttribute_TypeNotIModelBinder", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006DBE File Offset: 0x00004FBE
		internal static string ModelBinderDictionary_MultipleAttributes
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ModelBinderDictionary_MultipleAttributes", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006DD4 File Offset: 0x00004FD4
		internal static string ModelMetadata_PropertyNotSettable
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ModelMetadata_PropertyNotSettable", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00006DEA File Offset: 0x00004FEA
		internal static string MvcForm_ConstructorObsolete
		{
			get
			{
				return MvcResources.ResourceManager.GetString("MvcForm_ConstructorObsolete", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006E00 File Offset: 0x00005000
		internal static string MvcRazorCodeParser_CannotHaveModelAndInheritsKeyword
		{
			get
			{
				return MvcResources.ResourceManager.GetString("MvcRazorCodeParser_CannotHaveModelAndInheritsKeyword", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006E16 File Offset: 0x00005016
		internal static string MvcRazorCodeParser_ModelKeywordMustBeFollowedByTypeName
		{
			get
			{
				return MvcResources.ResourceManager.GetString("MvcRazorCodeParser_ModelKeywordMustBeFollowedByTypeName", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006E2C File Offset: 0x0000502C
		internal static string MvcRazorCodeParser_OnlyOneModelStatementIsAllowed
		{
			get
			{
				return MvcResources.ResourceManager.GetString("MvcRazorCodeParser_OnlyOneModelStatementIsAllowed", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006E42 File Offset: 0x00005042
		internal static string MvcRouteHandler_RouteValuesHasNoController
		{
			get
			{
				return MvcResources.ResourceManager.GetString("MvcRouteHandler_RouteValuesHasNoController", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006E58 File Offset: 0x00005058
		internal static string OutputCacheAttribute_CannotNestChildCache
		{
			get
			{
				return MvcResources.ResourceManager.GetString("OutputCacheAttribute_CannotNestChildCache", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006E6E File Offset: 0x0000506E
		internal static string OutputCacheAttribute_ChildAction_UnsupportedSetting
		{
			get
			{
				return MvcResources.ResourceManager.GetString("OutputCacheAttribute_ChildAction_UnsupportedSetting", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006E84 File Offset: 0x00005084
		internal static string OutputCacheAttribute_InvalidDuration
		{
			get
			{
				return MvcResources.ResourceManager.GetString("OutputCacheAttribute_InvalidDuration", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006E9A File Offset: 0x0000509A
		internal static string OutputCacheAttribute_InvalidVaryByParam
		{
			get
			{
				return MvcResources.ResourceManager.GetString("OutputCacheAttribute_InvalidVaryByParam", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006EB0 File Offset: 0x000050B0
		internal static string RedirectAction_CannotRedirectInChildAction
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RedirectAction_CannotRedirectInChildAction", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006EC6 File Offset: 0x000050C6
		internal static string ReflectedActionDescriptor_CannotCallInstanceMethodOnNonControllerType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedActionDescriptor_CannotCallInstanceMethodOnNonControllerType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006EDC File Offset: 0x000050DC
		internal static string ReflectedActionDescriptor_CannotCallMethodsWithOutOrRefParameters
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedActionDescriptor_CannotCallMethodsWithOutOrRefParameters", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006EF2 File Offset: 0x000050F2
		internal static string ReflectedActionDescriptor_CannotCallOpenGenericMethods
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedActionDescriptor_CannotCallOpenGenericMethods", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006F08 File Offset: 0x00005108
		internal static string ReflectedActionDescriptor_CannotCallStaticMethod
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedActionDescriptor_CannotCallStaticMethod", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006F1E File Offset: 0x0000511E
		internal static string ReflectedActionDescriptor_ParameterCannotBeNull
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterCannotBeNull", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006F34 File Offset: 0x00005134
		internal static string ReflectedActionDescriptor_ParameterNotInDictionary
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterNotInDictionary", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006F4A File Offset: 0x0000514A
		internal static string ReflectedActionDescriptor_ParameterValueHasWrongType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterValueHasWrongType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006F60 File Offset: 0x00005160
		internal static string ReflectedParameterBindingInfo_MultipleConverterAttributes
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ReflectedParameterBindingInfo_MultipleConverterAttributes", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006F76 File Offset: 0x00005176
		internal static string RemoteAttribute_NoUrlFound
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RemoteAttribute_NoUrlFound", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006F8C File Offset: 0x0000518C
		internal static string RemoteAttribute_RemoteValidationFailed
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RemoteAttribute_RemoteValidationFailed", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006FA2 File Offset: 0x000051A2
		internal static string RequireHttpsAttribute_MustUseSsl
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RequireHttpsAttribute_MustUseSsl", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006FB8 File Offset: 0x000051B8
		internal static string Route_CannotHaveCatchAllInMultiSegment
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_CannotHaveCatchAllInMultiSegment", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006FCE File Offset: 0x000051CE
		internal static string Route_CannotHaveConsecutiveParameters
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_CannotHaveConsecutiveParameters", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006FE4 File Offset: 0x000051E4
		internal static string Route_CannotHaveConsecutiveSeparators
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_CannotHaveConsecutiveSeparators", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006FFA File Offset: 0x000051FA
		internal static string Route_CatchAllMustBeLast
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_CatchAllMustBeLast", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007010 File Offset: 0x00005210
		internal static string Route_InvalidConstraint
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_InvalidConstraint", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007026 File Offset: 0x00005226
		internal static string Route_InvalidParameterName
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_InvalidParameterName", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000703C File Offset: 0x0000523C
		internal static string Route_InvalidRouteTemplate
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_InvalidRouteTemplate", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007052 File Offset: 0x00005252
		internal static string Route_MismatchedParameter
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_MismatchedParameter", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007068 File Offset: 0x00005268
		internal static string Route_RepeatedParameter
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Route_RepeatedParameter", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000707E File Offset: 0x0000527E
		internal static string RouteAreaPrefix_CannotEnd_WithForwardSlash
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RouteAreaPrefix_CannotEnd_WithForwardSlash", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007094 File Offset: 0x00005294
		internal static string RoutePrefix_CannotStartOrEnd_WithForwardSlash
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RoutePrefix_CannotStartOrEnd_WithForwardSlash", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000070AA File Offset: 0x000052AA
		internal static string RoutePrefix_CannotSupportMultiRoutePrefix
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RoutePrefix_CannotSupportMultiRoutePrefix", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000070C0 File Offset: 0x000052C0
		internal static string RoutePrefix_PrefixCannotBeNull
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RoutePrefix_PrefixCannotBeNull", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000070D6 File Offset: 0x000052D6
		internal static string RouteTemplate_CannotStart_WithForwardSlash
		{
			get
			{
				return MvcResources.ResourceManager.GetString("RouteTemplate_CannotStart_WithForwardSlash", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000070EC File Offset: 0x000052EC
		internal static string SelectExtensions_InvalidExpressionParameterNoMetadata
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterNoMetadata", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007102 File Offset: 0x00005302
		internal static string SelectExtensions_InvalidExpressionParameterNoModelType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterNoModelType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007118 File Offset: 0x00005318
		internal static string SelectExtensions_InvalidExpressionParameterType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000712E File Offset: 0x0000532E
		internal static string SelectExtensions_InvalidExpressionParameterTypeHasFlags
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterTypeHasFlags", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007144 File Offset: 0x00005344
		internal static string SessionStateTempDataProvider_SessionStateDisabled
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SessionStateTempDataProvider_SessionStateDisabled", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000715A File Offset: 0x0000535A
		internal static string SingleServiceResolver_CannotRegisterTwoInstances
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SingleServiceResolver_CannotRegisterTwoInstances", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007170 File Offset: 0x00005370
		internal static string SubRouteCollection_DuplicateRouteName
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SubRouteCollection_DuplicateRouteName", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007186 File Offset: 0x00005386
		internal static string SynchronizationContextUtil_ExceptionThrown
		{
			get
			{
				return MvcResources.ResourceManager.GetString("SynchronizationContextUtil_ExceptionThrown", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000719C File Offset: 0x0000539C
		internal static string TaskAsyncActionDescriptor_CannotExecuteSynchronously
		{
			get
			{
				return MvcResources.ResourceManager.GetString("TaskAsyncActionDescriptor_CannotExecuteSynchronously", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000071B2 File Offset: 0x000053B2
		internal static string TemplateHelpers_NoTemplate
		{
			get
			{
				return MvcResources.ResourceManager.GetString("TemplateHelpers_NoTemplate", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000071C8 File Offset: 0x000053C8
		internal static string TemplateHelpers_TemplateLimitations
		{
			get
			{
				return MvcResources.ResourceManager.GetString("TemplateHelpers_TemplateLimitations", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000071DE File Offset: 0x000053DE
		internal static string Templates_TypeMustImplementIEnumerable
		{
			get
			{
				return MvcResources.ResourceManager.GetString("Templates_TypeMustImplementIEnumerable", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000071F4 File Offset: 0x000053F4
		internal static string TypeCache_DoNotModify
		{
			get
			{
				return MvcResources.ResourceManager.GetString("TypeCache_DoNotModify", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000720A File Offset: 0x0000540A
		internal static string TypeHelpers_CannotCreateInstance
		{
			get
			{
				return MvcResources.ResourceManager.GetString("TypeHelpers_CannotCreateInstance", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007220 File Offset: 0x00005420
		internal static string TypeMethodMustNotReturnNull
		{
			get
			{
				return MvcResources.ResourceManager.GetString("TypeMethodMustNotReturnNull", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007236 File Offset: 0x00005436
		internal static string ValidatableObjectAdapter_IncompatibleType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ValidatableObjectAdapter_IncompatibleType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000724C File Offset: 0x0000544C
		internal static string ValueProviderResult_ConversionThrew
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ValueProviderResult_ConversionThrew", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007262 File Offset: 0x00005462
		internal static string ValueProviderResult_NoConverterExists
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ValueProviderResult_NoConverterExists", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007278 File Offset: 0x00005478
		internal static string ViewDataDictionary_ModelCannotBeNull
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ViewDataDictionary_ModelCannotBeNull", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000728E File Offset: 0x0000548E
		internal static string ViewDataDictionary_WrongTModelType
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ViewDataDictionary_WrongTModelType", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000072A4 File Offset: 0x000054A4
		internal static string ViewMasterPage_RequiresViewPage
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ViewMasterPage_RequiresViewPage", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000072BA File Offset: 0x000054BA
		internal static string ViewPageHttpHandlerWrapper_ExceptionOccurred
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ViewPageHttpHandlerWrapper_ExceptionOccurred", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000072D0 File Offset: 0x000054D0
		internal static string ViewStartPage_RequiresMvcRazorView
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ViewStartPage_RequiresMvcRazorView", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000072E6 File Offset: 0x000054E6
		internal static string ViewUserControl_RequiresViewDataProvider
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ViewUserControl_RequiresViewDataProvider", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000072FC File Offset: 0x000054FC
		internal static string ViewUserControl_RequiresViewPage
		{
			get
			{
				return MvcResources.ResourceManager.GetString("ViewUserControl_RequiresViewPage", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00007312 File Offset: 0x00005512
		internal static string WebFormViewEngine_UserControlCannotHaveMaster
		{
			get
			{
				return MvcResources.ResourceManager.GetString("WebFormViewEngine_UserControlCannotHaveMaster", MvcResources.resourceCulture);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007328 File Offset: 0x00005528
		internal static string WebFormViewEngine_WrongViewBase
		{
			get
			{
				return MvcResources.ResourceManager.GetString("WebFormViewEngine_WrongViewBase", MvcResources.resourceCulture);
			}
		}

		// Token: 0x04000055 RID: 85
		private static ResourceManager resourceMan;

		// Token: 0x04000056 RID: 86
		private static CultureInfo resourceCulture;
	}
}
