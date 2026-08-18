using System;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000364 RID: 868
	internal enum ErrorCode
	{
		// Token: 0x04000A91 RID: 2705
		InvalidErrorCodeValue,
		// Token: 0x04000A92 RID: 2706
		SecurityError = 2,
		// Token: 0x04000A93 RID: 2707
		IOException = 4,
		// Token: 0x04000A94 RID: 2708
		XmlError,
		// Token: 0x04000A95 RID: 2709
		TooManyErrors,
		// Token: 0x04000A96 RID: 2710
		MalformedXml,
		// Token: 0x04000A97 RID: 2711
		UnexpectedXmlNodeType,
		// Token: 0x04000A98 RID: 2712
		UnexpectedXmlAttribute,
		// Token: 0x04000A99 RID: 2713
		UnexpectedXmlElement,
		// Token: 0x04000A9A RID: 2714
		TextNotAllowed,
		// Token: 0x04000A9B RID: 2715
		EmptyFile,
		// Token: 0x04000A9C RID: 2716
		XsdError,
		// Token: 0x04000A9D RID: 2717
		InvalidAlias,
		// Token: 0x04000A9E RID: 2718
		IntegerExpected = 16,
		// Token: 0x04000A9F RID: 2719
		InvalidName,
		// Token: 0x04000AA0 RID: 2720
		AlreadyDefined = 19,
		// Token: 0x04000AA1 RID: 2721
		ElementNotInSchema,
		// Token: 0x04000AA2 RID: 2722
		InvalidBaseType = 22,
		// Token: 0x04000AA3 RID: 2723
		NoConcreteDescendants,
		// Token: 0x04000AA4 RID: 2724
		CycleInTypeHierarchy,
		// Token: 0x04000AA5 RID: 2725
		InvalidVersionNumber,
		// Token: 0x04000AA6 RID: 2726
		InvalidSize,
		// Token: 0x04000AA7 RID: 2727
		InvalidBoolean,
		// Token: 0x04000AA8 RID: 2728
		BadType = 29,
		// Token: 0x04000AA9 RID: 2729
		InvalidVersioningClass = 32,
		// Token: 0x04000AAA RID: 2730
		InvalidVersionIntroduced,
		// Token: 0x04000AAB RID: 2731
		BadNamespace,
		// Token: 0x04000AAC RID: 2732
		UnresolvedReferenceSchema = 38,
		// Token: 0x04000AAD RID: 2733
		NotInNamespace = 40,
		// Token: 0x04000AAE RID: 2734
		NotUnnestedType,
		// Token: 0x04000AAF RID: 2735
		BadProperty,
		// Token: 0x04000AB0 RID: 2736
		UndefinedProperty,
		// Token: 0x04000AB1 RID: 2737
		InvalidPropertyType,
		// Token: 0x04000AB2 RID: 2738
		InvalidAsNestedType,
		// Token: 0x04000AB3 RID: 2739
		InvalidChangeUnit,
		// Token: 0x04000AB4 RID: 2740
		UnauthorizedAccessException,
		// Token: 0x04000AB5 RID: 2741
		MissingNamespaceAttribute = 50,
		// Token: 0x04000AB6 RID: 2742
		PrecisionOutOfRange,
		// Token: 0x04000AB7 RID: 2743
		ScaleOutOfRange,
		// Token: 0x04000AB8 RID: 2744
		DefaultNotAllowed,
		// Token: 0x04000AB9 RID: 2745
		InvalidDefault,
		// Token: 0x04000ABA RID: 2746
		RequiredFacetMissing,
		// Token: 0x04000ABB RID: 2747
		BadImageFormatException,
		// Token: 0x04000ABC RID: 2748
		MissingSchemaXml,
		// Token: 0x04000ABD RID: 2749
		BadPrecisionAndScale,
		// Token: 0x04000ABE RID: 2750
		InvalidChangeUnitUsage,
		// Token: 0x04000ABF RID: 2751
		NameTooLong,
		// Token: 0x04000AC0 RID: 2752
		CircularlyDefinedType,
		// Token: 0x04000AC1 RID: 2753
		InvalidAssociation,
		// Token: 0x04000AC2 RID: 2754
		FacetNotAllowedByType,
		// Token: 0x04000AC3 RID: 2755
		ConstantFacetSpecifiedInSchema,
		// Token: 0x04000AC4 RID: 2756
		BadNavigationProperty = 74,
		// Token: 0x04000AC5 RID: 2757
		InvalidKey,
		// Token: 0x04000AC6 RID: 2758
		InvalidMultiplicity = 92,
		// Token: 0x04000AC7 RID: 2759
		InvalidAction = 96,
		// Token: 0x04000AC8 RID: 2760
		InvalidOperation,
		// Token: 0x04000AC9 RID: 2761
		InvalidContainerTypeForEnd = 99,
		// Token: 0x04000ACA RID: 2762
		InvalidEndEntitySet,
		// Token: 0x04000ACB RID: 2763
		AmbiguousEntityContainerEnd,
		// Token: 0x04000ACC RID: 2764
		MissingExtentEntityContainerEnd,
		// Token: 0x04000ACD RID: 2765
		BadParameterDirection = 106,
		// Token: 0x04000ACE RID: 2766
		FailedInference,
		// Token: 0x04000ACF RID: 2767
		InvalidFacetInProviderManifest = 109,
		// Token: 0x04000AD0 RID: 2768
		InvalidRoleInRelationshipConstraint,
		// Token: 0x04000AD1 RID: 2769
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x04000AD2 RID: 2770
		TypeMismatchRelationshipConstraint,
		// Token: 0x04000AD3 RID: 2771
		InvalidMultiplicityInRoleInRelationshipConstraint,
		// Token: 0x04000AD4 RID: 2772
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x04000AD5 RID: 2773
		MissingPropertyInRelationshipConstraint,
		// Token: 0x04000AD6 RID: 2774
		MissingConstraintOnRelationshipType,
		// Token: 0x04000AD7 RID: 2775
		SameRoleReferredInReferentialConstraint = 119,
		// Token: 0x04000AD8 RID: 2776
		InvalidValueForParameterTypeSemantics,
		// Token: 0x04000AD9 RID: 2777
		InvalidRelationshipEndType,
		// Token: 0x04000ADA RID: 2778
		InvalidPrimitiveTypeKind,
		// Token: 0x04000ADB RID: 2779
		InvalidTypeConversionDestinationType = 124,
		// Token: 0x04000ADC RID: 2780
		ByteValueExpected,
		// Token: 0x04000ADD RID: 2781
		FunctionWithNonPrimitiveTypeNotSupported,
		// Token: 0x04000ADE RID: 2782
		PrecisionMoreThanAllowedMax,
		// Token: 0x04000ADF RID: 2783
		EntityKeyMustBeScalar,
		// Token: 0x04000AE0 RID: 2784
		EntityKeyTypeCurrentlyNotSupported,
		// Token: 0x04000AE1 RID: 2785
		NoPreferredMappingForPrimitiveTypeKind,
		// Token: 0x04000AE2 RID: 2786
		TooManyPreferredMappingsForPrimitiveTypeKind,
		// Token: 0x04000AE3 RID: 2787
		EndWithManyMultiplicityCannotHaveOperationsSpecified,
		// Token: 0x04000AE4 RID: 2788
		EntitySetTypeHasNoKeys,
		// Token: 0x04000AE5 RID: 2789
		InvalidNumberOfParametersForAggregateFunction,
		// Token: 0x04000AE6 RID: 2790
		InvalidParameterTypeForAggregateFunction,
		// Token: 0x04000AE7 RID: 2791
		ComposableFunctionOrFunctionImportWithoutReturnType,
		// Token: 0x04000AE8 RID: 2792
		NonComposableFunctionWithReturnType,
		// Token: 0x04000AE9 RID: 2793
		NonComposableFunctionAttributesNotValid,
		// Token: 0x04000AEA RID: 2794
		ComposableFunctionWithCommandText,
		// Token: 0x04000AEB RID: 2795
		FunctionDeclaresCommandTextAndStoreFunctionName,
		// Token: 0x04000AEC RID: 2796
		SystemNamespace,
		// Token: 0x04000AED RID: 2797
		EmptyDefiningQuery,
		// Token: 0x04000AEE RID: 2798
		TableAndSchemaAreMutuallyExclusiveWithDefiningQuery,
		// Token: 0x04000AEF RID: 2799
		ConcurrencyRedefinedOnSubTypeOfEntitySetType = 145,
		// Token: 0x04000AF0 RID: 2800
		FunctionImportUnsupportedReturnType,
		// Token: 0x04000AF1 RID: 2801
		FunctionImportUnknownEntitySet,
		// Token: 0x04000AF2 RID: 2802
		FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet,
		// Token: 0x04000AF3 RID: 2803
		FunctionImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x04000AF4 RID: 2804
		FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x04000AF5 RID: 2805
		InternalError = 152,
		// Token: 0x04000AF6 RID: 2806
		SimilarRelationshipEnd,
		// Token: 0x04000AF7 RID: 2807
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x04000AF8 RID: 2808
		AmbiguousFunctionReturnType = 156,
		// Token: 0x04000AF9 RID: 2809
		NullableComplexType,
		// Token: 0x04000AFA RID: 2810
		NonComplexCollections,
		// Token: 0x04000AFB RID: 2811
		KeyMissingOnEntityType,
		// Token: 0x04000AFC RID: 2812
		InvalidNamespaceInUsing,
		// Token: 0x04000AFD RID: 2813
		NeedNotUseSystemNamespaceInUsing,
		// Token: 0x04000AFE RID: 2814
		CannotUseSystemNamespaceAsAlias,
		// Token: 0x04000AFF RID: 2815
		InvalidNamespaceName,
		// Token: 0x04000B00 RID: 2816
		InvalidEntityContainerNameInExtends,
		// Token: 0x04000B01 RID: 2817
		InvalidNamespaceOrAliasSpecified = 166,
		// Token: 0x04000B02 RID: 2818
		EntityContainerCannotExtendItself,
		// Token: 0x04000B03 RID: 2819
		FailedToRetrieveProviderManifest,
		// Token: 0x04000B04 RID: 2820
		ProviderManifestTokenMismatch,
		// Token: 0x04000B05 RID: 2821
		ProviderManifestTokenNotFound,
		// Token: 0x04000B06 RID: 2822
		EmptyCommandText,
		// Token: 0x04000B07 RID: 2823
		InconsistentProvider,
		// Token: 0x04000B08 RID: 2824
		InconsistentProviderManifestToken,
		// Token: 0x04000B09 RID: 2825
		DuplicatedFunctionoverloads,
		// Token: 0x04000B0A RID: 2826
		InvalidProvider,
		// Token: 0x04000B0B RID: 2827
		FunctionWithNonEdmTypeNotSupported,
		// Token: 0x04000B0C RID: 2828
		ComplexTypeAsReturnTypeAndDefinedEntitySet,
		// Token: 0x04000B0D RID: 2829
		ComplexTypeAsReturnTypeAndNestedComplexProperty,
		// Token: 0x04000B0E RID: 2830
		FunctionImportComposableAndSideEffectingNotAllowed = 180,
		// Token: 0x04000B0F RID: 2831
		FunctionImportEntitySetAndEntitySetPathDeclared,
		// Token: 0x04000B10 RID: 2832
		FacetOnNonScalarType,
		// Token: 0x04000B11 RID: 2833
		IncorrectlyPlacedFacet,
		// Token: 0x04000B12 RID: 2834
		ReturnTypeNotDeclared,
		// Token: 0x04000B13 RID: 2835
		TypeNotDeclared,
		// Token: 0x04000B14 RID: 2836
		RowTypeWithoutProperty,
		// Token: 0x04000B15 RID: 2837
		ReturnTypeDeclaredAsAttributeAndElement,
		// Token: 0x04000B16 RID: 2838
		TypeDeclaredAsAttributeAndElement,
		// Token: 0x04000B17 RID: 2839
		ReferenceToNonEntityType,
		// Token: 0x04000B18 RID: 2840
		FunctionImportCollectionAndRefParametersNotAllowed,
		// Token: 0x04000B19 RID: 2841
		IncompatibleSchemaVersion,
		// Token: 0x04000B1A RID: 2842
		NoCodeGenNamespaceInStructuralAnnotation,
		// Token: 0x04000B1B RID: 2843
		AmbiguousFunctionAndType,
		// Token: 0x04000B1C RID: 2844
		CannotLoadDifferentVersionOfSchemaInTheSameItemCollection,
		// Token: 0x04000B1D RID: 2845
		BoolValueExpected,
		// Token: 0x04000B1E RID: 2846
		EndWithoutMultiplicity,
		// Token: 0x04000B1F RID: 2847
		TVFReturnTypeRowHasNonScalarProperty,
		// Token: 0x04000B20 RID: 2848
		FunctionImportNonNullableParametersNotAllowed = 201,
		// Token: 0x04000B21 RID: 2849
		FunctionWithDefiningExpressionAndEntitySetNotAllowed,
		// Token: 0x04000B22 RID: 2850
		FunctionEntityTypeScopeDoesNotMatchReturnType,
		// Token: 0x04000B23 RID: 2851
		InvalidEnumUnderlyingType,
		// Token: 0x04000B24 RID: 2852
		DuplicateEnumMember,
		// Token: 0x04000B25 RID: 2853
		CalculatedEnumValueOutOfRange,
		// Token: 0x04000B26 RID: 2854
		EnumMemberValueOutOfItsUnderylingTypeRange,
		// Token: 0x04000B27 RID: 2855
		InvalidSystemReferenceId,
		// Token: 0x04000B28 RID: 2856
		UnexpectedSpatialType
	}
}
