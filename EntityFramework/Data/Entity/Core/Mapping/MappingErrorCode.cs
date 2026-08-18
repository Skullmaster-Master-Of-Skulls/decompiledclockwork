using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003DD RID: 989
	internal enum MappingErrorCode
	{
		// Token: 0x04000CB7 RID: 3255
		Value = 2000,
		// Token: 0x04000CB8 RID: 3256
		InvalidContent,
		// Token: 0x04000CB9 RID: 3257
		InvalidEntityContainer,
		// Token: 0x04000CBA RID: 3258
		InvalidEntitySet,
		// Token: 0x04000CBB RID: 3259
		InvalidEntityType,
		// Token: 0x04000CBC RID: 3260
		InvalidAssociationSet,
		// Token: 0x04000CBD RID: 3261
		InvalidAssociationType,
		// Token: 0x04000CBE RID: 3262
		InvalidTable,
		// Token: 0x04000CBF RID: 3263
		InvalidComplexType,
		// Token: 0x04000CC0 RID: 3264
		InvalidEdmMember,
		// Token: 0x04000CC1 RID: 3265
		InvalidStorageMember,
		// Token: 0x04000CC2 RID: 3266
		TableMappingFragmentExpected,
		// Token: 0x04000CC3 RID: 3267
		SetMappingExpected,
		// Token: 0x04000CC4 RID: 3268
		DuplicateSetMapping = 2014,
		// Token: 0x04000CC5 RID: 3269
		DuplicateTypeMapping,
		// Token: 0x04000CC6 RID: 3270
		ConditionError,
		// Token: 0x04000CC7 RID: 3271
		RootMappingElementMissing = 2018,
		// Token: 0x04000CC8 RID: 3272
		IncompatibleMemberMapping,
		// Token: 0x04000CC9 RID: 3273
		InvalidEnumValue = 2023,
		// Token: 0x04000CCA RID: 3274
		XmlSchemaParsingError,
		// Token: 0x04000CCB RID: 3275
		XmlSchemaValidationError,
		// Token: 0x04000CCC RID: 3276
		AmbiguousModificationFunctionMappingForAssociationSet,
		// Token: 0x04000CCD RID: 3277
		MissingSetClosureInModificationFunctionMapping,
		// Token: 0x04000CCE RID: 3278
		MissingModificationFunctionMappingForEntityType,
		// Token: 0x04000CCF RID: 3279
		InvalidTableNameAttributeWithModificationFunctionMapping,
		// Token: 0x04000CD0 RID: 3280
		InvalidModificationFunctionMappingForMultipleTypes,
		// Token: 0x04000CD1 RID: 3281
		AmbiguousResultBindingInModificationFunctionMapping,
		// Token: 0x04000CD2 RID: 3282
		InvalidAssociationSetRoleInModificationFunctionMapping,
		// Token: 0x04000CD3 RID: 3283
		InvalidAssociationSetCardinalityInModificationFunctionMapping,
		// Token: 0x04000CD4 RID: 3284
		RedundantEntityTypeMappingInModificationFunctionMapping,
		// Token: 0x04000CD5 RID: 3285
		MissingVersionInModificationFunctionMapping,
		// Token: 0x04000CD6 RID: 3286
		InvalidVersionInModificationFunctionMapping,
		// Token: 0x04000CD7 RID: 3287
		InvalidParameterInModificationFunctionMapping,
		// Token: 0x04000CD8 RID: 3288
		ParameterBoundTwiceInModificationFunctionMapping,
		// Token: 0x04000CD9 RID: 3289
		CSpaceMemberMappedToMultipleSSpaceMemberWithDifferentTypes,
		// Token: 0x04000CDA RID: 3290
		NoEquivalentStorePrimitiveTypeFound,
		// Token: 0x04000CDB RID: 3291
		NoEquivalentStorePrimitiveTypeWithFacetsFound,
		// Token: 0x04000CDC RID: 3292
		InvalidModificationFunctionMappingPropertyParameterTypeMismatch,
		// Token: 0x04000CDD RID: 3293
		InvalidModificationFunctionMappingMultipleEndsOfAssociationMapped,
		// Token: 0x04000CDE RID: 3294
		InvalidModificationFunctionMappingUnknownFunction,
		// Token: 0x04000CDF RID: 3295
		InvalidModificationFunctionMappingAmbiguousFunction,
		// Token: 0x04000CE0 RID: 3296
		InvalidModificationFunctionMappingNotValidFunction,
		// Token: 0x04000CE1 RID: 3297
		InvalidModificationFunctionMappingNotValidFunctionParameter,
		// Token: 0x04000CE2 RID: 3298
		InvalidModificationFunctionMappingAssociationSetNotMappedForOperation,
		// Token: 0x04000CE3 RID: 3299
		InvalidModificationFunctionMappingAssociationEndMappingInvalidForEntityType,
		// Token: 0x04000CE4 RID: 3300
		MappingFunctionImportStoreFunctionDoesNotExist,
		// Token: 0x04000CE5 RID: 3301
		MappingFunctionImportStoreFunctionAmbiguous,
		// Token: 0x04000CE6 RID: 3302
		MappingFunctionImportFunctionImportDoesNotExist,
		// Token: 0x04000CE7 RID: 3303
		MappingFunctionImportFunctionImportMappedMultipleTimes,
		// Token: 0x04000CE8 RID: 3304
		MappingFunctionImportTargetFunctionMustBeNonComposable,
		// Token: 0x04000CE9 RID: 3305
		MappingFunctionImportTargetParameterHasNoCorrespondingImportParameter,
		// Token: 0x04000CEA RID: 3306
		MappingFunctionImportImportParameterHasNoCorrespondingTargetParameter,
		// Token: 0x04000CEB RID: 3307
		MappingFunctionImportIncompatibleParameterMode,
		// Token: 0x04000CEC RID: 3308
		MappingFunctionImportIncompatibleParameterType,
		// Token: 0x04000CED RID: 3309
		MappingFunctionImportRowsAffectedParameterDoesNotExist,
		// Token: 0x04000CEE RID: 3310
		MappingFunctionImportRowsAffectedParameterHasWrongType,
		// Token: 0x04000CEF RID: 3311
		MappingFunctionImportRowsAffectedParameterHasWrongMode,
		// Token: 0x04000CF0 RID: 3312
		EmptyContainerMapping,
		// Token: 0x04000CF1 RID: 3313
		EmptySetMapping,
		// Token: 0x04000CF2 RID: 3314
		TableNameAttributeWithQueryView,
		// Token: 0x04000CF3 RID: 3315
		EmptyQueryView,
		// Token: 0x04000CF4 RID: 3316
		PropertyMapsWithQueryView,
		// Token: 0x04000CF5 RID: 3317
		MissingSetClosureInQueryViews,
		// Token: 0x04000CF6 RID: 3318
		InvalidQueryView,
		// Token: 0x04000CF7 RID: 3319
		InvalidQueryViewResultType,
		// Token: 0x04000CF8 RID: 3320
		ItemWithSameNameExistsBothInCSpaceAndSSpace,
		// Token: 0x04000CF9 RID: 3321
		MappingUnsupportedExpressionKindQueryView,
		// Token: 0x04000CFA RID: 3322
		MappingUnsupportedScanTargetQueryView,
		// Token: 0x04000CFB RID: 3323
		MappingUnsupportedPropertyKindQueryView,
		// Token: 0x04000CFC RID: 3324
		MappingUnsupportedInitializationQueryView,
		// Token: 0x04000CFD RID: 3325
		MappingFunctionImportEntityTypeMappingForFunctionNotReturningEntitySet,
		// Token: 0x04000CFE RID: 3326
		MappingFunctionImportAmbiguousTypeConditions,
		// Token: 0x04000CFF RID: 3327
		MappingOfAbstractType = 2078,
		// Token: 0x04000D00 RID: 3328
		StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping,
		// Token: 0x04000D01 RID: 3329
		TypeNameForFirstQueryView,
		// Token: 0x04000D02 RID: 3330
		NoTypeNameForTypeSpecificQueryView,
		// Token: 0x04000D03 RID: 3331
		QueryViewExistsForEntitySetAndType,
		// Token: 0x04000D04 RID: 3332
		TypeNameContainsMultipleTypesForQueryView,
		// Token: 0x04000D05 RID: 3333
		IsTypeOfQueryViewForBaseType,
		// Token: 0x04000D06 RID: 3334
		InvalidTypeInScalarProperty,
		// Token: 0x04000D07 RID: 3335
		AlreadyMappedStorageEntityContainer,
		// Token: 0x04000D08 RID: 3336
		UnsupportedQueryViewInEntityContainerMapping,
		// Token: 0x04000D09 RID: 3337
		MappingAllQueryViewAtCompileTime,
		// Token: 0x04000D0A RID: 3338
		MappingNoViewsCanBeGenerated,
		// Token: 0x04000D0B RID: 3339
		MappingStoreProviderReturnsNullEdmType,
		// Token: 0x04000D0C RID: 3340
		DuplicateMemberMapping = 2092,
		// Token: 0x04000D0D RID: 3341
		MappingFunctionImportUnexpectedEntityTypeMapping,
		// Token: 0x04000D0E RID: 3342
		MappingFunctionImportUnexpectedComplexTypeMapping,
		// Token: 0x04000D0F RID: 3343
		DistinctFragmentInReadWriteContainer = 2096,
		// Token: 0x04000D10 RID: 3344
		EntitySetMismatchOnAssociationSetEnd,
		// Token: 0x04000D11 RID: 3345
		InvalidModificationFunctionMappingAssociationEndForeignKey,
		// Token: 0x04000D12 RID: 3346
		CannotLoadDifferentVersionOfSchemaInTheSameItemCollection = 2100,
		// Token: 0x04000D13 RID: 3347
		MappingDifferentMappingEdmStoreVersion,
		// Token: 0x04000D14 RID: 3348
		MappingDifferentEdmStoreVersion,
		// Token: 0x04000D15 RID: 3349
		UnmappedFunctionImport,
		// Token: 0x04000D16 RID: 3350
		MappingFunctionImportReturnTypePropertyNotMapped,
		// Token: 0x04000D17 RID: 3351
		InvalidType = 2106,
		// Token: 0x04000D18 RID: 3352
		MappingFunctionImportTVFExpected = 2108,
		// Token: 0x04000D19 RID: 3353
		MappingFunctionImportScalarMappingTypeMismatch,
		// Token: 0x04000D1A RID: 3354
		MappingFunctionImportScalarMappingToMulticolumnTVF,
		// Token: 0x04000D1B RID: 3355
		MappingFunctionImportTargetFunctionMustBeComposable,
		// Token: 0x04000D1C RID: 3356
		UnsupportedFunctionCallInQueryView,
		// Token: 0x04000D1D RID: 3357
		FunctionResultMappingCountMismatch,
		// Token: 0x04000D1E RID: 3358
		MappingFunctionImportCannotInferTargetFunctionKeys
	}
}
