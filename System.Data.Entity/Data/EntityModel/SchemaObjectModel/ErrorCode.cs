using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002EB RID: 747
	internal enum ErrorCode
	{
		// Token: 0x0400131A RID: 4890
		InvalidErrorCodeValue,
		// Token: 0x0400131B RID: 4891
		SecurityError = 2,
		// Token: 0x0400131C RID: 4892
		IOException = 4,
		// Token: 0x0400131D RID: 4893
		XmlError,
		// Token: 0x0400131E RID: 4894
		TooManyErrors,
		// Token: 0x0400131F RID: 4895
		MalformedXml,
		// Token: 0x04001320 RID: 4896
		UnexpectedXmlNodeType,
		// Token: 0x04001321 RID: 4897
		UnexpectedXmlAttribute,
		// Token: 0x04001322 RID: 4898
		UnexpectedXmlElement,
		// Token: 0x04001323 RID: 4899
		TextNotAllowed,
		// Token: 0x04001324 RID: 4900
		EmptyFile,
		// Token: 0x04001325 RID: 4901
		XsdError,
		// Token: 0x04001326 RID: 4902
		InvalidAlias,
		// Token: 0x04001327 RID: 4903
		IntegerExpected = 16,
		// Token: 0x04001328 RID: 4904
		InvalidName,
		// Token: 0x04001329 RID: 4905
		AlreadyDefined = 19,
		// Token: 0x0400132A RID: 4906
		ElementNotInSchema,
		// Token: 0x0400132B RID: 4907
		InvalidBaseType = 22,
		// Token: 0x0400132C RID: 4908
		NoConcreteDescendants,
		// Token: 0x0400132D RID: 4909
		CycleInTypeHierarchy,
		// Token: 0x0400132E RID: 4910
		InvalidVersionNumber,
		// Token: 0x0400132F RID: 4911
		InvalidSize,
		// Token: 0x04001330 RID: 4912
		InvalidBoolean,
		// Token: 0x04001331 RID: 4913
		BadType = 29,
		// Token: 0x04001332 RID: 4914
		InvalidVersioningClass = 32,
		// Token: 0x04001333 RID: 4915
		InvalidVersionIntroduced,
		// Token: 0x04001334 RID: 4916
		BadNamespace,
		// Token: 0x04001335 RID: 4917
		UnresolvedReferenceSchema = 38,
		// Token: 0x04001336 RID: 4918
		NotInNamespace = 40,
		// Token: 0x04001337 RID: 4919
		NotUnnestedType,
		// Token: 0x04001338 RID: 4920
		BadProperty,
		// Token: 0x04001339 RID: 4921
		UndefinedProperty,
		// Token: 0x0400133A RID: 4922
		InvalidPropertyType,
		// Token: 0x0400133B RID: 4923
		InvalidAsNestedType,
		// Token: 0x0400133C RID: 4924
		InvalidChangeUnit,
		// Token: 0x0400133D RID: 4925
		UnauthorizedAccessException,
		// Token: 0x0400133E RID: 4926
		MissingNamespaceAttribute = 50,
		// Token: 0x0400133F RID: 4927
		PrecisionOutOfRange,
		// Token: 0x04001340 RID: 4928
		ScaleOutOfRange,
		// Token: 0x04001341 RID: 4929
		DefaultNotAllowed,
		// Token: 0x04001342 RID: 4930
		InvalidDefault,
		// Token: 0x04001343 RID: 4931
		RequiredFacetMissing,
		// Token: 0x04001344 RID: 4932
		BadImageFormatException,
		// Token: 0x04001345 RID: 4933
		MissingSchemaXml,
		// Token: 0x04001346 RID: 4934
		BadPrecisionAndScale,
		// Token: 0x04001347 RID: 4935
		InvalidChangeUnitUsage,
		// Token: 0x04001348 RID: 4936
		NameTooLong,
		// Token: 0x04001349 RID: 4937
		CircularlyDefinedType,
		// Token: 0x0400134A RID: 4938
		InvalidAssociation,
		// Token: 0x0400134B RID: 4939
		FacetNotAllowedByType,
		// Token: 0x0400134C RID: 4940
		ConstantFacetSpecifiedInSchema,
		// Token: 0x0400134D RID: 4941
		BadNavigationProperty = 74,
		// Token: 0x0400134E RID: 4942
		InvalidKey,
		// Token: 0x0400134F RID: 4943
		InvalidMultiplicity = 92,
		// Token: 0x04001350 RID: 4944
		InvalidAction = 96,
		// Token: 0x04001351 RID: 4945
		InvalidOperation,
		// Token: 0x04001352 RID: 4946
		InvalidContainerTypeForEnd = 99,
		// Token: 0x04001353 RID: 4947
		InvalidEndEntitySet,
		// Token: 0x04001354 RID: 4948
		AmbiguousEntityContainerEnd,
		// Token: 0x04001355 RID: 4949
		MissingExtentEntityContainerEnd,
		// Token: 0x04001356 RID: 4950
		BadParameterDirection = 106,
		// Token: 0x04001357 RID: 4951
		FailedInference,
		// Token: 0x04001358 RID: 4952
		InvalidFacetInProviderManifest = 109,
		// Token: 0x04001359 RID: 4953
		InvalidRoleInRelationshipConstraint,
		// Token: 0x0400135A RID: 4954
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x0400135B RID: 4955
		TypeMismatchRelationshipConstaint,
		// Token: 0x0400135C RID: 4956
		InvalidMultiplicityInRoleInRelationshipConstraint,
		// Token: 0x0400135D RID: 4957
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x0400135E RID: 4958
		MissingPropertyInRelationshipConstraint,
		// Token: 0x0400135F RID: 4959
		MissingConstraintOnRelationshipType,
		// Token: 0x04001360 RID: 4960
		SameRoleReferredInReferentialConstraint = 119,
		// Token: 0x04001361 RID: 4961
		InvalidValueForParameterTypeSemantics,
		// Token: 0x04001362 RID: 4962
		InvalidRelationshipEndType,
		// Token: 0x04001363 RID: 4963
		InvalidPrimitiveTypeKind,
		// Token: 0x04001364 RID: 4964
		InvalidTypeConversionDestinationType = 124,
		// Token: 0x04001365 RID: 4965
		ByteValueExpected,
		// Token: 0x04001366 RID: 4966
		FunctionWithNonPrimitiveTypeNotSupported,
		// Token: 0x04001367 RID: 4967
		PrecisionMoreThanAllowedMax,
		// Token: 0x04001368 RID: 4968
		EntityKeyMustBeScalar,
		// Token: 0x04001369 RID: 4969
		EntityKeyTypeCurrentlyNotSupported,
		// Token: 0x0400136A RID: 4970
		NoPreferredMappingForPrimitiveTypeKind,
		// Token: 0x0400136B RID: 4971
		TooManyPreferredMappingsForPrimitiveTypeKind,
		// Token: 0x0400136C RID: 4972
		EndWithManyMultiplicityCannotHaveOperationsSpecified,
		// Token: 0x0400136D RID: 4973
		EntitySetTypeHasNoKeys,
		// Token: 0x0400136E RID: 4974
		InvalidNumberOfParametersForAggregateFunction,
		// Token: 0x0400136F RID: 4975
		InvalidParameterTypeForAggregateFunction,
		// Token: 0x04001370 RID: 4976
		ComposableFunctionOrFunctionImportWithoutReturnType,
		// Token: 0x04001371 RID: 4977
		NonComposableFunctionWithReturnType,
		// Token: 0x04001372 RID: 4978
		NonComposableFunctionAttributesNotValid,
		// Token: 0x04001373 RID: 4979
		ComposableFunctionWithCommandText,
		// Token: 0x04001374 RID: 4980
		FunctionDeclaresCommandTextAndStoreFunctionName,
		// Token: 0x04001375 RID: 4981
		SystemNamespace,
		// Token: 0x04001376 RID: 4982
		EmptyDefiningQuery,
		// Token: 0x04001377 RID: 4983
		TableAndSchemaAreMutuallyExclusiveWithDefiningQuery,
		// Token: 0x04001378 RID: 4984
		ConcurrencyRedefinedOnSubTypeOfEntitySetType = 145,
		// Token: 0x04001379 RID: 4985
		FunctionImportUnsupportedReturnType,
		// Token: 0x0400137A RID: 4986
		FunctionImportUnknownEntitySet,
		// Token: 0x0400137B RID: 4987
		FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet,
		// Token: 0x0400137C RID: 4988
		FunctionImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x0400137D RID: 4989
		FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x0400137E RID: 4990
		InternalError = 152,
		// Token: 0x0400137F RID: 4991
		SimilarRelationshipEnd,
		// Token: 0x04001380 RID: 4992
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x04001381 RID: 4993
		AmbiguousFunctionReturnType = 156,
		// Token: 0x04001382 RID: 4994
		NullableComplexType,
		// Token: 0x04001383 RID: 4995
		NonComplexCollections,
		// Token: 0x04001384 RID: 4996
		KeyMissingOnEntityType,
		// Token: 0x04001385 RID: 4997
		InvalidNamespaceInUsing,
		// Token: 0x04001386 RID: 4998
		NeedNotUseSystemNamespaceInUsing,
		// Token: 0x04001387 RID: 4999
		CannotUseSystemNamespaceAsAlias,
		// Token: 0x04001388 RID: 5000
		InvalidNamespaceName,
		// Token: 0x04001389 RID: 5001
		InvalidEntityContainerNameInExtends,
		// Token: 0x0400138A RID: 5002
		InvalidNamespaceOrAliasSpecified = 166,
		// Token: 0x0400138B RID: 5003
		EntityContainerCannotExtendItself,
		// Token: 0x0400138C RID: 5004
		FailedToRetrieveProviderManifest,
		// Token: 0x0400138D RID: 5005
		ProviderManifestTokenMismatch,
		// Token: 0x0400138E RID: 5006
		ProviderManifestTokenNotFound,
		// Token: 0x0400138F RID: 5007
		EmptyCommandText,
		// Token: 0x04001390 RID: 5008
		InconsistentProvider,
		// Token: 0x04001391 RID: 5009
		InconsistentProviderManifestToken,
		// Token: 0x04001392 RID: 5010
		DuplicatedFunctionoverloads,
		// Token: 0x04001393 RID: 5011
		InvalidProvider,
		// Token: 0x04001394 RID: 5012
		FunctionWithNonEdmTypeNotSupported,
		// Token: 0x04001395 RID: 5013
		ComplexTypeAsReturnTypeAndDefinedEntitySet,
		// Token: 0x04001396 RID: 5014
		ComplexTypeAsReturnTypeAndNestedComplexProperty,
		// Token: 0x04001397 RID: 5015
		FunctionImportComposableAndSideEffectingNotAllowed = 180,
		// Token: 0x04001398 RID: 5016
		FunctionImportEntitySetAndEntitySetPathDeclared,
		// Token: 0x04001399 RID: 5017
		FacetOnNonScalarType,
		// Token: 0x0400139A RID: 5018
		IncorrectlyPlacedFacet,
		// Token: 0x0400139B RID: 5019
		ReturnTypeNotDeclared,
		// Token: 0x0400139C RID: 5020
		TypeNotDeclared,
		// Token: 0x0400139D RID: 5021
		RowTypeWithoutProperty,
		// Token: 0x0400139E RID: 5022
		ReturnTypeDeclaredAsAttributeAndElement,
		// Token: 0x0400139F RID: 5023
		TypeDeclaredAsAttributeAndElement,
		// Token: 0x040013A0 RID: 5024
		ReferenceToNonEntityType,
		// Token: 0x040013A1 RID: 5025
		FunctionImportCollectionAndRefParametersNotAllowed,
		// Token: 0x040013A2 RID: 5026
		IncompatibleSchemaVersion,
		// Token: 0x040013A3 RID: 5027
		NoCodeGenNamespaceInStructuralAnnotation,
		// Token: 0x040013A4 RID: 5028
		AmbiguousFunctionAndType,
		// Token: 0x040013A5 RID: 5029
		CannotLoadDifferentVersionOfSchemaInTheSameItemCollection,
		// Token: 0x040013A6 RID: 5030
		BoolValueExpected,
		// Token: 0x040013A7 RID: 5031
		EndWithoutMultiplicity,
		// Token: 0x040013A8 RID: 5032
		TVFReturnTypeRowHasNonScalarProperty,
		// Token: 0x040013A9 RID: 5033
		FunctionImportNonNullableParametersNotAllowed = 201,
		// Token: 0x040013AA RID: 5034
		FunctionWithDefiningExpressionAndEntitySetNotAllowed,
		// Token: 0x040013AB RID: 5035
		FunctionEntityTypeScopeDoesNotMatchReturnType,
		// Token: 0x040013AC RID: 5036
		InvalidEnumUnderlyingType,
		// Token: 0x040013AD RID: 5037
		DuplicateEnumMember,
		// Token: 0x040013AE RID: 5038
		CalculatedEnumValueOutOfRange,
		// Token: 0x040013AF RID: 5039
		EnumMemberValueOutOfItsUnderylingTypeRange,
		// Token: 0x040013B0 RID: 5040
		InvalidSystemReferenceId,
		// Token: 0x040013B1 RID: 5041
		UnexpectedSpatialType
	}
}
