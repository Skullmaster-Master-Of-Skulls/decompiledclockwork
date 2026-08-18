using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Properties
{
	// Token: 0x02000049 RID: 73
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class SRResources
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00007B98 File Offset: 0x00005D98
		internal SRResources()
		{
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007BA0 File Offset: 0x00005DA0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(SRResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.Http.Properties.SRResources", typeof(SRResources).Assembly);
					SRResources.resourceMan = resourceManager;
				}
				return SRResources.resourceMan;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007BDF File Offset: 0x00005DDF
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00007BE6 File Offset: 0x00005DE6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SRResources.resourceCulture;
			}
			set
			{
				SRResources.resourceCulture = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007BEE File Offset: 0x00005DEE
		internal static string ActionExecutor_UnexpectedTaskInstance
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionExecutor_UnexpectedTaskInstance", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007C04 File Offset: 0x00005E04
		internal static string ActionExecutor_WrappedTaskInstance
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionExecutor_WrappedTaskInstance", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007C1A File Offset: 0x00005E1A
		internal static string ActionFilterAttribute_MustSupplyResponseOrException
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionFilterAttribute_MustSupplyResponseOrException", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007C30 File Offset: 0x00005E30
		internal static string ActionSelector_AmbiguousMatchType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionSelector_AmbiguousMatchType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007C46 File Offset: 0x00005E46
		internal static string ApiController_RequestMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiController_RequestMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007C5C File Offset: 0x00005E5C
		internal static string ApiControllerActionInvoker_InvalidHttpActionResult
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionInvoker_InvalidHttpActionResult", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00007C72 File Offset: 0x00005E72
		internal static string ApiControllerActionInvoker_NullHttpActionResult
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionInvoker_NullHttpActionResult", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007C88 File Offset: 0x00005E88
		internal static string ApiControllerActionSelector_ActionNameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_ActionNameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00007C9E File Offset: 0x00005E9E
		internal static string ApiControllerActionSelector_ActionNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_ActionNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007CB4 File Offset: 0x00005EB4
		internal static string ApiControllerActionSelector_AmbiguousMatch
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_AmbiguousMatch", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007CCA File Offset: 0x00005ECA
		internal static string ApiControllerActionSelector_HttpMethodNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_HttpMethodNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007CE0 File Offset: 0x00005EE0
		internal static string AttributeRoutes_InvalidPrefix
		{
			get
			{
				return SRResources.ResourceManager.GetString("AttributeRoutes_InvalidPrefix", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007CF6 File Offset: 0x00005EF6
		internal static string AttributeRoutes_InvalidTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("AttributeRoutes_InvalidTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007D0C File Offset: 0x00005F0C
		internal static string AuthenticationFilterDidNothing
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterDidNothing", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007D22 File Offset: 0x00005F22
		internal static string AuthenticationFilterErrorResult
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterErrorResult", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007D38 File Offset: 0x00005F38
		internal static string AuthenticationFilterSetPrincipalToKnownIdentity
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterSetPrincipalToKnownIdentity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00007D4E File Offset: 0x00005F4E
		internal static string AuthenticationFilterSetPrincipalToUnknownIdentity
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterSetPrincipalToUnknownIdentity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007D64 File Offset: 0x00005F64
		internal static string BadRequest
		{
			get
			{
				return SRResources.ResourceManager.GetString("BadRequest", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00007D7A File Offset: 0x00005F7A
		internal static string BatchContentTypeMissing
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchContentTypeMissing", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007D90 File Offset: 0x00005F90
		internal static string BatchMediaTypeNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchMediaTypeNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007DA6 File Offset: 0x00005FA6
		internal static string BatchRequestMissingContent
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchRequestMissingContent", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00007DBC File Offset: 0x00005FBC
		internal static string CannotSupportSingletonInstance
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotSupportSingletonInstance", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007DD2 File Offset: 0x00005FD2
		internal static string CollectionParameterContainsNullElement
		{
			get
			{
				return SRResources.ResourceManager.GetString("CollectionParameterContainsNullElement", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007DE8 File Offset: 0x00005FE8
		internal static string Common_PropertyNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("Common_PropertyNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007DFE File Offset: 0x00005FFE
		internal static string Common_TypeMustDriveFromType
		{
			get
			{
				return SRResources.ResourceManager.GetString("Common_TypeMustDriveFromType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007E14 File Offset: 0x00006014
		internal static string ControllerNameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ControllerNameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007E2A File Offset: 0x0000602A
		internal static string DataAnnotationsModelValidatorProvider_ConstructorRequirements
		{
			get
			{
				return SRResources.ResourceManager.GetString("DataAnnotationsModelValidatorProvider_ConstructorRequirements", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007E40 File Offset: 0x00006040
		internal static string DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements
		{
			get
			{
				return SRResources.ResourceManager.GetString("DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007E56 File Offset: 0x00006056
		internal static string DefaultControllerFactory_ControllerNameAmbiguous_WithRouteTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultControllerFactory_ControllerNameAmbiguous_WithRouteTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007E6C File Offset: 0x0000606C
		internal static string DefaultControllerFactory_ControllerNameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultControllerFactory_ControllerNameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007E82 File Offset: 0x00006082
		internal static string DefaultControllerFactory_ErrorCreatingController
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultControllerFactory_ErrorCreatingController", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007E98 File Offset: 0x00006098
		internal static string DefaultInlineConstraintResolver_AmbiguousCtors
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultInlineConstraintResolver_AmbiguousCtors", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00007EAE File Offset: 0x000060AE
		internal static string DefaultInlineConstraintResolver_CouldNotFindCtor
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultInlineConstraintResolver_CouldNotFindCtor", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007EC4 File Offset: 0x000060C4
		internal static string DefaultInlineConstraintResolver_TypeNotConstraint
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultInlineConstraintResolver_TypeNotConstraint", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007EDA File Offset: 0x000060DA
		internal static string DefaultServices_InvalidServiceType
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultServices_InvalidServiceType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007EF0 File Offset: 0x000060F0
		internal static string DependencyResolver_BeginScopeReturnsNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("DependencyResolver_BeginScopeReturnsNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007F06 File Offset: 0x00006106
		internal static string DependencyResolverNoService
		{
			get
			{
				return SRResources.ResourceManager.GetString("DependencyResolverNoService", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007F1C File Offset: 0x0000611C
		internal static string DirectRoute_AmbiguousController
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_AmbiguousController", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007F32 File Offset: 0x00006132
		internal static string DirectRoute_HandlerNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_HandlerNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007F48 File Offset: 0x00006148
		internal static string DirectRoute_InvalidParameter_Action
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_InvalidParameter_Action", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007F5E File Offset: 0x0000615E
		internal static string DirectRoute_InvalidParameter_Controller
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_InvalidParameter_Controller", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007F74 File Offset: 0x00006174
		internal static string DirectRoute_MissingActionDescriptors
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_MissingActionDescriptors", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007F8A File Offset: 0x0000618A
		internal static string ErrorOccurred
		{
			get
			{
				return SRResources.ResourceManager.GetString("ErrorOccurred", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007FA0 File Offset: 0x000061A0
		internal static string HttpActionDescriptor_NoConverterForGenericParamterTypeExists
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpActionDescriptor_NoConverterForGenericParamterTypeExists", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007FB6 File Offset: 0x000061B6
		internal static string HttpControllerContext_ConfigurationMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpControllerContext_ConfigurationMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007FCC File Offset: 0x000061CC
		internal static string HttpRequestMessageExtensions_NoConfiguration
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRequestMessageExtensions_NoConfiguration", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007FE2 File Offset: 0x000061E2
		internal static string HttpRequestMessageExtensions_NoContentNegotiator
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRequestMessageExtensions_NoContentNegotiator", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00007FF8 File Offset: 0x000061F8
		internal static string HttpRequestMessageExtensions_NoMatchingFormatter
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRequestMessageExtensions_NoMatchingFormatter", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000800E File Offset: 0x0000620E
		internal static string HttpResponseExceptionMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpResponseExceptionMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00008024 File Offset: 0x00006224
		internal static string HttpRouteBuilder_CouldNotResolveConstraint
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRouteBuilder_CouldNotResolveConstraint", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000803A File Offset: 0x0000623A
		internal static string HttpServerDisposed
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpServerDisposed", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008050 File Offset: 0x00006250
		internal static string JQuerySyntaxMissingClosingBracket
		{
			get
			{
				return SRResources.ResourceManager.GetString("JQuerySyntaxMissingClosingBracket", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008066 File Offset: 0x00006266
		internal static string MaxHttpCollectionKeyLimitReached
		{
			get
			{
				return SRResources.ResourceManager.GetString("MaxHttpCollectionKeyLimitReached", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000807C File Offset: 0x0000627C
		internal static string MissingDataMemberIsRequired
		{
			get
			{
				return SRResources.ResourceManager.GetString("MissingDataMemberIsRequired", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008092 File Offset: 0x00006292
		internal static string MissingRequiredMember
		{
			get
			{
				return SRResources.ResourceManager.GetString("MissingRequiredMember", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000080A8 File Offset: 0x000062A8
		internal static string ModelBinderConfig_ValueInvalid
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderConfig_ValueInvalid", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000080BE File Offset: 0x000062BE
		internal static string ModelBinderConfig_ValueRequired
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderConfig_ValueRequired", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000080D4 File Offset: 0x000062D4
		internal static string ModelBinderProviderCollection_InvalidBinderType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderProviderCollection_InvalidBinderType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000080EA File Offset: 0x000062EA
		internal static string ModelBinderUtil_ModelCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00008100 File Offset: 0x00006300
		internal static string ModelBinderUtil_ModelInstanceIsWrong
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelInstanceIsWrong", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008116 File Offset: 0x00006316
		internal static string ModelBinderUtil_ModelMetadataCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelMetadataCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000812C File Offset: 0x0000632C
		internal static string ModelBinderUtil_ModelTypeIsWrong
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelTypeIsWrong", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008142 File Offset: 0x00006342
		internal static string ModelBindingContext_ModelMetadataMustBeSet
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBindingContext_ModelMetadataMustBeSet", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008158 File Offset: 0x00006358
		internal static string NoControllerCreated
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoControllerCreated", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000816E File Offset: 0x0000636E
		internal static string NoControllerSelected
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoControllerSelected", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008184 File Offset: 0x00006384
		internal static string NoRouteData
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoRouteData", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000819A File Offset: 0x0000639A
		internal static string Object_NotYetInitialized
		{
			get
			{
				return SRResources.ResourceManager.GetString("Object_NotYetInitialized", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000081B0 File Offset: 0x000063B0
		internal static string OptionalBodyParameterNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("OptionalBodyParameterNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000081C6 File Offset: 0x000063C6
		internal static string ParameterBindingCantHaveMultipleBodyParameters
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterBindingCantHaveMultipleBodyParameters", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000081DC File Offset: 0x000063DC
		internal static string ParameterBindingConflictingAttributes
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterBindingConflictingAttributes", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000081F2 File Offset: 0x000063F2
		internal static string ParameterBindingIllegalType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterBindingIllegalType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008208 File Offset: 0x00006408
		internal static string ReflectedActionDescriptor_ParameterCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000821E File Offset: 0x0000641E
		internal static string ReflectedActionDescriptor_ParameterNotInDictionary
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterNotInDictionary", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008234 File Offset: 0x00006434
		internal static string ReflectedActionDescriptor_ParameterValueHasWrongType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterValueHasWrongType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000824A File Offset: 0x0000644A
		internal static string ReflectedHttpActionDescriptor_CannotCallOpenGenericMethods
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedHttpActionDescriptor_CannotCallOpenGenericMethods", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008260 File Offset: 0x00006460
		internal static string Request_RequestContextMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("Request_RequestContextMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008276 File Offset: 0x00006476
		internal static string RequestContextConflict
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestContextConflict", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000828C File Offset: 0x0000648C
		internal static string RequestIsNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestIsNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000082A2 File Offset: 0x000064A2
		internal static string RequestNotAuthorized
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestNotAuthorized", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000082B8 File Offset: 0x000064B8
		internal static string ResourceNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ResourceNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000082CE File Offset: 0x000064CE
		internal static string ResponseMessageResultConverter_NullHttpResponseMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("ResponseMessageResultConverter_NullHttpResponseMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000082E4 File Offset: 0x000064E4
		internal static string Route_AddRemoveWithNoKeyNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_AddRemoveWithNoKeyNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000082FA File Offset: 0x000064FA
		internal static string Route_CannotHaveCatchAllInMultiSegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CannotHaveCatchAllInMultiSegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00008310 File Offset: 0x00006510
		internal static string Route_CannotHaveConsecutiveParameters
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CannotHaveConsecutiveParameters", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008326 File Offset: 0x00006526
		internal static string Route_CannotHaveConsecutiveSeparators
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CannotHaveConsecutiveSeparators", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000833C File Offset: 0x0000653C
		internal static string Route_CatchAllMustBeLast
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CatchAllMustBeLast", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008352 File Offset: 0x00006552
		internal static string Route_InvalidParameterName
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_InvalidParameterName", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00008368 File Offset: 0x00006568
		internal static string Route_InvalidRouteTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_InvalidRouteTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000837E File Offset: 0x0000657E
		internal static string Route_MismatchedParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_MismatchedParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00008394 File Offset: 0x00006594
		internal static string Route_RepeatedParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_RepeatedParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000083AA File Offset: 0x000065AA
		internal static string Route_ValidationMustBeStringOrCustomConstraint
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_ValidationMustBeStringOrCustomConstraint", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000083C0 File Offset: 0x000065C0
		internal static string RouteCollection_NameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("RouteCollection_NameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000083D6 File Offset: 0x000065D6
		internal static string RoutePrefix_CannotSupportMultiRoutePrefix
		{
			get
			{
				return SRResources.ResourceManager.GetString("RoutePrefix_CannotSupportMultiRoutePrefix", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000083EC File Offset: 0x000065EC
		internal static string RoutePrefix_PrefixCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("RoutePrefix_PrefixCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008402 File Offset: 0x00006602
		internal static string SubRouteCollection_DuplicateRouteName
		{
			get
			{
				return SRResources.ResourceManager.GetString("SubRouteCollection_DuplicateRouteName", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008418 File Offset: 0x00006618
		internal static string TraceActionFilterMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionFilterMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000842E File Offset: 0x0000662E
		internal static string TraceActionInvokeMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionInvokeMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008444 File Offset: 0x00006644
		internal static string TraceActionReturnValue
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionReturnValue", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000845A File Offset: 0x0000665A
		internal static string TraceActionSelectedMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionSelectedMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008470 File Offset: 0x00006670
		internal static string TraceBeginParameterBind
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceBeginParameterBind", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008486 File Offset: 0x00006686
		internal static string TraceCancelledMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceCancelledMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000849C File Offset: 0x0000669C
		internal static string TraceEndParameterBind
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceEndParameterBind", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000084B2 File Offset: 0x000066B2
		internal static string TraceEndParameterBindNoBind
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceEndParameterBindNoBind", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000084C8 File Offset: 0x000066C8
		internal static string TraceGetPerRequestFormatterEndMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestFormatterEndMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000084DE File Offset: 0x000066DE
		internal static string TraceGetPerRequestFormatterEndMessageNew
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestFormatterEndMessageNew", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000084F4 File Offset: 0x000066F4
		internal static string TraceGetPerRequestFormatterMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestFormatterMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000850A File Offset: 0x0000670A
		internal static string TraceGetPerRequestNullFormatterEndMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestNullFormatterEndMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008520 File Offset: 0x00006720
		internal static string TraceHttpControllerTypeResolverError
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceHttpControllerTypeResolverError", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00008536 File Offset: 0x00006736
		internal static string TraceInvokingAction
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceInvokingAction", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000854C File Offset: 0x0000674C
		internal static string TraceModelStateErrorMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceModelStateErrorMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00008562 File Offset: 0x00006762
		internal static string TraceModelStateInvalidMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceModelStateInvalidMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00008578 File Offset: 0x00006778
		internal static string TraceNegotiateFormatter
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceNegotiateFormatter", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000858E File Offset: 0x0000678E
		internal static string TraceNoneObjectMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceNoneObjectMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000085A4 File Offset: 0x000067A4
		internal static string TraceReadFromStreamMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceReadFromStreamMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000085BA File Offset: 0x000067BA
		internal static string TraceReadFromStreamValueMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceReadFromStreamValueMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000085D0 File Offset: 0x000067D0
		internal static string TraceRequestCompleteMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceRequestCompleteMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000085E6 File Offset: 0x000067E6
		internal static string TraceRouteMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceRouteMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000085FC File Offset: 0x000067FC
		internal static string TraceSelectedFormatter
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceSelectedFormatter", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008612 File Offset: 0x00006812
		internal static string TraceUnknownMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceUnknownMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008628 File Offset: 0x00006828
		internal static string TraceValidModelState
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceValidModelState", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000863E File Offset: 0x0000683E
		internal static string TraceWriteToStreamMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceWriteToStreamMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008654 File Offset: 0x00006854
		internal static string TypeInstanceMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeInstanceMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000866A File Offset: 0x0000686A
		internal static string TypeMethodMustNotReturnNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeMethodMustNotReturnNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008680 File Offset: 0x00006880
		internal static string TypePropertyMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypePropertyMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008696 File Offset: 0x00006896
		internal static string UnsupportedMediaType
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedMediaType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000086AC File Offset: 0x000068AC
		internal static string UnsupportedMediaTypeNoContentType
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedMediaTypeNoContentType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000086C2 File Offset: 0x000068C2
		internal static string UrlHelper_LinkMustNotReturnNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("UrlHelper_LinkMustNotReturnNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000086D8 File Offset: 0x000068D8
		internal static string ValidatableObjectAdapter_IncompatibleType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidatableObjectAdapter_IncompatibleType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000086EE File Offset: 0x000068EE
		internal static string Validation_ValueNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("Validation_ValueNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00008704 File Offset: 0x00006904
		internal static string ValidationAttributeOnField
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidationAttributeOnField", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000871A File Offset: 0x0000691A
		internal static string ValidationAttributeOnNonPublicProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidationAttributeOnNonPublicProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008730 File Offset: 0x00006930
		internal static string ValidModelState
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidModelState", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00008746 File Offset: 0x00006946
		internal static string ValueProviderFactory_Cannot_Create
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValueProviderFactory_Cannot_Create", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000875C File Offset: 0x0000695C
		internal static string ValueProviderResult_ConversionThrew
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValueProviderResult_ConversionThrew", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00008772 File Offset: 0x00006972
		internal static string ValueProviderResult_NoConverterExists
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValueProviderResult_NoConverterExists", SRResources.resourceCulture);
			}
		}

		// Token: 0x04000096 RID: 150
		private static ResourceManager resourceMan;

		// Token: 0x04000097 RID: 151
		private static CultureInfo resourceCulture;
	}
}
