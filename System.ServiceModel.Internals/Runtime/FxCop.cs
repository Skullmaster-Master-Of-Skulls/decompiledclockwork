using System;

namespace System.Runtime
{
	// Token: 0x0200001A RID: 26
	internal static class FxCop
	{
		// Token: 0x0200006A RID: 106
		public static class Category
		{
			// Token: 0x040001DE RID: 478
			public const string Design = "Microsoft.Design";

			// Token: 0x040001DF RID: 479
			public const string Globalization = "Microsoft.Globalization";

			// Token: 0x040001E0 RID: 480
			public const string Maintainability = "Microsoft.Maintainability";

			// Token: 0x040001E1 RID: 481
			public const string MSInternal = "Microsoft.MSInternal";

			// Token: 0x040001E2 RID: 482
			public const string Naming = "Microsoft.Naming";

			// Token: 0x040001E3 RID: 483
			public const string Performance = "Microsoft.Performance";

			// Token: 0x040001E4 RID: 484
			public const string Reliability = "Microsoft.Reliability";

			// Token: 0x040001E5 RID: 485
			public const string Security = "Microsoft.Security";

			// Token: 0x040001E6 RID: 486
			public const string Usage = "Microsoft.Usage";

			// Token: 0x040001E7 RID: 487
			public const string Configuration = "Configuration";

			// Token: 0x040001E8 RID: 488
			public const string ReliabilityBasic = "Reliability";

			// Token: 0x040001E9 RID: 489
			public const string Xaml = "XAML";
		}

		// Token: 0x0200006B RID: 107
		public static class Rule
		{
			// Token: 0x040001EA RID: 490
			public const string AptcaMethodsShouldOnlyCallAptcaMethods = "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods";

			// Token: 0x040001EB RID: 491
			public const string AssembliesShouldHaveValidStrongNames = "CA2210:AssembliesShouldHaveValidStrongNames";

			// Token: 0x040001EC RID: 492
			public const string AvoidCallingProblematicMethods = "CA2001:AvoidCallingProblematicMethods";

			// Token: 0x040001ED RID: 493
			public const string AvoidExcessiveComplexity = "CA1502:AvoidExcessiveComplexity";

			// Token: 0x040001EE RID: 494
			public const string AvoidNamespacesWithFewTypes = "CA1020:AvoidNamespacesWithFewTypes";

			// Token: 0x040001EF RID: 495
			public const string AvoidOutParameters = "CA1021:AvoidOutParameters";

			// Token: 0x040001F0 RID: 496
			public const string AvoidUncalledPrivateCode = "CA1811:AvoidUncalledPrivateCode";

			// Token: 0x040001F1 RID: 497
			public const string AvoidUninstantiatedInternalClasses = "CA1812:AvoidUninstantiatedInternalClasses";

			// Token: 0x040001F2 RID: 498
			public const string AvoidUnsealedAttributes = "CA1813:AvoidUnsealedAttributes";

			// Token: 0x040001F3 RID: 499
			public const string CollectionPropertiesShouldBeReadOnly = "CA2227:CollectionPropertiesShouldBeReadOnly";

			// Token: 0x040001F4 RID: 500
			public const string CollectionsShouldImplementGenericInterface = "CA1010:CollectionsShouldImplementGenericInterface";

			// Token: 0x040001F5 RID: 501
			public const string ConfigurationPropertyAttributeRule = "Configuration102:ConfigurationPropertyAttributeRule";

			// Token: 0x040001F6 RID: 502
			public const string ConfigurationValidatorAttributeRule = "Configuration104:ConfigurationValidatorAttributeRule";

			// Token: 0x040001F7 RID: 503
			public const string ConsiderPassingBaseTypesAsParameters = "CA1011:ConsiderPassingBaseTypesAsParameters";

			// Token: 0x040001F8 RID: 504
			public const string CommunicationObjectThrowIf = "Reliability106";

			// Token: 0x040001F9 RID: 505
			public const string ConfigurationPropertyNameRule = "Configuration103:ConfigurationPropertyNameRule";

			// Token: 0x040001FA RID: 506
			public const string DefaultParametersShouldNotBeUsed = "CA1026:DefaultParametersShouldNotBeUsed";

			// Token: 0x040001FB RID: 507
			public const string DefineAccessorsForAttributeArguments = "CA1019:DefineAccessorsForAttributeArguments";

			// Token: 0x040001FC RID: 508
			public const string DiagnosticsUtilityIsFatal = "Reliability108";

			// Token: 0x040001FD RID: 509
			public const string DisposableFieldsShouldBeDisposed = "CA2213:DisposableFieldsShouldBeDisposed";

			// Token: 0x040001FE RID: 510
			public const string DoNotCallOverridableMethodsInConstructors = "CA2214:DoNotCallOverridableMethodsInConstructors";

			// Token: 0x040001FF RID: 511
			public const string DoNotCatchGeneralExceptionTypes = "CA1031:DoNotCatchGeneralExceptionTypes";

			// Token: 0x04000200 RID: 512
			public const string DoNotDeclareReadOnlyMutableReferenceTypes = "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes";

			// Token: 0x04000201 RID: 513
			public const string DoNotDeclareVisibleInstanceFields = "CA1051:DoNotDeclareVisibleInstanceFields";

			// Token: 0x04000202 RID: 514
			public const string DoNotLockOnObjectsWithWeakIdentity = "CA2002:DoNotLockOnObjectsWithWeakIdentity";

			// Token: 0x04000203 RID: 515
			public const string DoNotIgnoreMethodResults = "CA1806:DoNotIgnoreMethodResults";

			// Token: 0x04000204 RID: 516
			public const string DoNotIndirectlyExposeMethodsWithLinkDemands = "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands";

			// Token: 0x04000205 RID: 517
			public const string DoNotPassLiteralsAsLocalizedParameters = "CA1303:DoNotPassLiteralsAsLocalizedParameters";

			// Token: 0x04000206 RID: 518
			public const string DoNotRaiseReservedExceptionTypes = "CA2201:DoNotRaiseReservedExceptionTypes";

			// Token: 0x04000207 RID: 519
			public const string EnumsShouldHaveZeroValue = "CA1008:EnumsShouldHaveZeroValue";

			// Token: 0x04000208 RID: 520
			public const string FlagsEnumsShouldHavePluralNames = "CA1714:FlagsEnumsShouldHavePluralNames";

			// Token: 0x04000209 RID: 521
			public const string GenericMethodsShouldProvideTypeParameter = "CA1004:GenericMethodsShouldProvideTypeParameter";

			// Token: 0x0400020A RID: 522
			public const string IdentifiersShouldBeSpelledCorrectly = "CA1704:IdentifiersShouldBeSpelledCorrectly";

			// Token: 0x0400020B RID: 523
			public const string IdentifiersShouldHaveCorrectSuffix = "CA1710:IdentifiersShouldHaveCorrectSuffix";

			// Token: 0x0400020C RID: 524
			public const string IdentifiersShouldNotContainTypeNames = "CA1720:IdentifiersShouldNotContainTypeNames";

			// Token: 0x0400020D RID: 525
			public const string IdentifiersShouldNotHaveIncorrectSuffix = "CA1711:IdentifiersShouldNotHaveIncorrectSuffix";

			// Token: 0x0400020E RID: 526
			public const string IdentifiersShouldNotMatchKeywords = "CA1716:IdentifiersShouldNotMatchKeywords";

			// Token: 0x0400020F RID: 527
			public const string ImplementStandardExceptionConstructors = "CA1032:ImplementStandardExceptionConstructors";

			// Token: 0x04000210 RID: 528
			public const string InstantiateArgumentExceptionsCorrectly = "CA2208:InstantiateArgumentExceptionsCorrectly";

			// Token: 0x04000211 RID: 529
			public const string InitializeReferenceTypeStaticFieldsInline = "CA1810:InitializeReferenceTypeStaticFieldsInline";

			// Token: 0x04000212 RID: 530
			public const string InterfaceMethodsShouldBeCallableByChildTypes = "CA1033:InterfaceMethodsShouldBeCallableByChildTypes";

			// Token: 0x04000213 RID: 531
			public const string MarkISerializableTypesWithSerializable = "CA2237:MarkISerializableTypesWithSerializable";

			// Token: 0x04000214 RID: 532
			public const string InvariantAssertRule = "Reliability101:InvariantAssertRule";

			// Token: 0x04000215 RID: 533
			public const string IsFatalRule = "Reliability108:IsFatalRule";

			// Token: 0x04000216 RID: 534
			public const string MarkMembersAsStatic = "CA1822:MarkMembersAsStatic";

			// Token: 0x04000217 RID: 535
			public const string NestedTypesShouldNotBeVisible = "CA1034:NestedTypesShouldNotBeVisible";

			// Token: 0x04000218 RID: 536
			public const string NormalizeStringsToUppercase = "CA1308:NormalizeStringsToUppercase";

			// Token: 0x04000219 RID: 537
			public const string OperatorOverloadsHaveNamedAlternates = "CA2225:OperatorOverloadsHaveNamedAlternates";

			// Token: 0x0400021A RID: 538
			public const string PropertyNamesShouldNotMatchGetMethods = "CA1721:PropertyNamesShouldNotMatchGetMethods";

			// Token: 0x0400021B RID: 539
			public const string PropertyTypesMustBeXamlVisible = "XAML1002:PropertyTypesMustBeXamlVisible";

			// Token: 0x0400021C RID: 540
			public const string PropertyExternalTypesMustBeKnown = "XAML1010:PropertyExternalTypesMustBeKnown";

			// Token: 0x0400021D RID: 541
			public const string ReplaceRepetitiveArgumentsWithParamsArray = "CA1025:ReplaceRepetitiveArgumentsWithParamsArray";

			// Token: 0x0400021E RID: 542
			public const string ResourceStringsShouldBeSpelledCorrectly = "CA1703:ResourceStringsShouldBeSpelledCorrectly";

			// Token: 0x0400021F RID: 543
			public const string ReviewSuppressUnmanagedCodeSecurityUsage = "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage";

			// Token: 0x04000220 RID: 544
			public const string ReviewUnusedParameters = "CA1801:ReviewUnusedParameters";

			// Token: 0x04000221 RID: 545
			public const string SecureAsserts = "CA2106:SecureAsserts";

			// Token: 0x04000222 RID: 546
			public const string SecureGetObjectDataOverrides = "CA2110:SecureGetObjectDataOverrides";

			// Token: 0x04000223 RID: 547
			public const string ShortAcronymsShouldBeUppercase = "CA1706:ShortAcronymsShouldBeUppercase";

			// Token: 0x04000224 RID: 548
			public const string SpecifyIFormatProvider = "CA1305:SpecifyIFormatProvider";

			// Token: 0x04000225 RID: 549
			public const string SpecifyMarshalingForPInvokeStringArguments = "CA2101:SpecifyMarshalingForPInvokeStringArguments";

			// Token: 0x04000226 RID: 550
			public const string StaticHolderTypesShouldNotHaveConstructors = "CA1053:StaticHolderTypesShouldNotHaveConstructors";

			// Token: 0x04000227 RID: 551
			public const string SystemAndMicrosoftNamespacesRequireApproval = "CA:SystemAndMicrosoftNamespacesRequireApproval";

			// Token: 0x04000228 RID: 552
			public const string UsePropertiesWhereAppropriate = "CA1024:UsePropertiesWhereAppropriate";

			// Token: 0x04000229 RID: 553
			public const string UriPropertiesShouldNotBeStrings = "CA1056:UriPropertiesShouldNotBeStrings";

			// Token: 0x0400022A RID: 554
			public const string VariableNamesShouldNotMatchFieldNames = "CA1500:VariableNamesShouldNotMatchFieldNames";

			// Token: 0x0400022B RID: 555
			public const string ThunkCallbackRule = "Reliability109:ThunkCallbackRule";

			// Token: 0x0400022C RID: 556
			public const string TransparentMethodsMustNotReferenceCriticalCode = "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule";

			// Token: 0x0400022D RID: 557
			public const string TypeConvertersMustBePublic = "XAML1004:TypeConvertersMustBePublic";

			// Token: 0x0400022E RID: 558
			public const string TypesMustHaveXamlCallableConstructors = "XAML1007:TypesMustHaveXamlCallableConstructors";

			// Token: 0x0400022F RID: 559
			public const string TypeNamesShouldNotMatchNamespaces = "CA1724:TypeNamesShouldNotMatchNamespaces";

			// Token: 0x04000230 RID: 560
			public const string TypesShouldHavePublicParameterlessConstructors = "XAML1009:TypesShouldHavePublicParameterlessConstructors";

			// Token: 0x04000231 RID: 561
			public const string UseEventsWhereAppropriate = "CA1030:UseEventsWhereAppropriate";

			// Token: 0x04000232 RID: 562
			public const string UseNewGuidHelperRule = "Reliability113:UseNewGuidHelperRule";

			// Token: 0x04000233 RID: 563
			public const string WrapExceptionsRule = "Reliability102:WrapExceptionsRule";
		}
	}
}
