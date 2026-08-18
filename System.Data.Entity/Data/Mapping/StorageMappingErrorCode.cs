using System;

namespace System.Data.Mapping
{
	// Token: 0x0200024A RID: 586
	internal enum StorageMappingErrorCode
	{
		// Token: 0x04001038 RID: 4152
		Value = 2000,
		// Token: 0x04001039 RID: 4153
		InvalidContent,
		// Token: 0x0400103A RID: 4154
		InvalidEntityContainer,
		// Token: 0x0400103B RID: 4155
		InvalidEntitySet,
		// Token: 0x0400103C RID: 4156
		InvalidEntityType,
		// Token: 0x0400103D RID: 4157
		InvalidAssociationSet,
		// Token: 0x0400103E RID: 4158
		InvalidAssociationType,
		// Token: 0x0400103F RID: 4159
		InvalidTable,
		// Token: 0x04001040 RID: 4160
		InvalidComplexType,
		// Token: 0x04001041 RID: 4161
		InvalidEdmMember,
		// Token: 0x04001042 RID: 4162
		InvalidStorageMember,
		// Token: 0x04001043 RID: 4163
		TableMappingFragmentExpected,
		// Token: 0x04001044 RID: 4164
		SetMappingExpected,
		// Token: 0x04001045 RID: 4165
		DuplicateSetMapping = 2014,
		// Token: 0x04001046 RID: 4166
		DuplicateTypeMapping,
		// Token: 0x04001047 RID: 4167
		ConditionError,
		// Token: 0x04001048 RID: 4168
		RootMappingElementMissing = 2018,
		// Token: 0x04001049 RID: 4169
		IncompatibleMemberMapping,
		// Token: 0x0400104A RID: 4170
		InvalidEnumValue = 2023,
		// Token: 0x0400104B RID: 4171
		XmlSchemaParsingError,
		// Token: 0x0400104C RID: 4172
		XmlSchemaValidationError,
		// Token: 0x0400104D RID: 4173
		AmbiguousModificationFunctionMappingForAssociationSet,
		// Token: 0x0400104E RID: 4174
		MissingSetClosureInModificationFunctionMapping,
		// Token: 0x0400104F RID: 4175
		MissingModificationFunctionMappingForEntityType,
		// Token: 0x04001050 RID: 4176
		InvalidTableNameAttributeWithModificationFunctionMapping,
		// Token: 0x04001051 RID: 4177
		InvalidModificationFunctionMappingForMultipleTypes,
		// Token: 0x04001052 RID: 4178
		AmbiguousResultBindingInModificationFunctionMapping,
		// Token: 0x04001053 RID: 4179
		InvalidAssociationSetRoleInModificationFunctionMapping,
		// Token: 0x04001054 RID: 4180
		InvalidAssociationSetCardinalityInModificationFunctionMapping,
		// Token: 0x04001055 RID: 4181
		RedundantEntityTypeMappingInModificationFunctionMapping,
		// Token: 0x04001056 RID: 4182
		MissingVersionInModificationFunctionMapping,
		// Token: 0x04001057 RID: 4183
		InvalidVersionInModificationFunctionMapping,
		// Token: 0x04001058 RID: 4184
		InvalidParameterInModificationFunctionMapping,
		// Token: 0x04001059 RID: 4185
		ParameterBoundTwiceInModificationFunctionMapping,
		// Token: 0x0400105A RID: 4186
		CSpaceMemberMappedToMultipleSSpaceMemberWithDifferentTypes,
		// Token: 0x0400105B RID: 4187
		NoEquivalentStorePrimitiveTypeFound,
		// Token: 0x0400105C RID: 4188
		NoEquivalentStorePrimitiveTypeWithFacetsFound,
		// Token: 0x0400105D RID: 4189
		InvalidModificationFunctionMappingPropertyParameterTypeMismatch,
		// Token: 0x0400105E RID: 4190
		InvalidModificationFunctionMappingMultipleEndsOfAssociationMapped,
		// Token: 0x0400105F RID: 4191
		InvalidModificationFunctionMappingUnknownFunction,
		// Token: 0x04001060 RID: 4192
		InvalidModificationFunctionMappingAmbiguousFunction,
		// Token: 0x04001061 RID: 4193
		InvalidModificationFunctionMappingNotValidFunction,
		// Token: 0x04001062 RID: 4194
		InvalidModificationFunctionMappingNotValidFunctionParameter,
		// Token: 0x04001063 RID: 4195
		InvalidModificationFunctionMappingAssociationSetNotMappedForOperation,
		// Token: 0x04001064 RID: 4196
		InvalidModificationFunctionMappingAssociationEndMappingInvalidForEntityType,
		// Token: 0x04001065 RID: 4197
		MappingFunctionImportStoreFunctionDoesNotExist,
		// Token: 0x04001066 RID: 4198
		MappingFunctionImportStoreFunctionAmbiguous,
		// Token: 0x04001067 RID: 4199
		MappingFunctionImportFunctionImportDoesNotExist,
		// Token: 0x04001068 RID: 4200
		MappingFunctionImportFunctionImportMappedMultipleTimes,
		// Token: 0x04001069 RID: 4201
		MappingFunctionImportTargetFunctionMustBeNonComposable,
		// Token: 0x0400106A RID: 4202
		MappingFunctionImportTargetParameterHasNoCorrespondingImportParameter,
		// Token: 0x0400106B RID: 4203
		MappingFunctionImportImportParameterHasNoCorrespondingTargetParameter,
		// Token: 0x0400106C RID: 4204
		MappingFunctionImportIncompatibleParameterMode,
		// Token: 0x0400106D RID: 4205
		MappingFunctionImportIncompatibleParameterType,
		// Token: 0x0400106E RID: 4206
		MappingFunctionImportRowsAffectedParameterDoesNotExist,
		// Token: 0x0400106F RID: 4207
		MappingFunctionImportRowsAffectedParameterHasWrongType,
		// Token: 0x04001070 RID: 4208
		MappingFunctionImportRowsAffectedParameterHasWrongMode,
		// Token: 0x04001071 RID: 4209
		EmptyContainerMapping,
		// Token: 0x04001072 RID: 4210
		EmptySetMapping,
		// Token: 0x04001073 RID: 4211
		TableNameAttributeWithQueryView,
		// Token: 0x04001074 RID: 4212
		EmptyQueryView,
		// Token: 0x04001075 RID: 4213
		PropertyMapsWithQueryView,
		// Token: 0x04001076 RID: 4214
		MissingSetClosureInQueryViews,
		// Token: 0x04001077 RID: 4215
		InvalidQueryView,
		// Token: 0x04001078 RID: 4216
		InvalidQueryViewResultType,
		// Token: 0x04001079 RID: 4217
		ItemWithSameNameExistsBothInCSpaceAndSSpace,
		// Token: 0x0400107A RID: 4218
		MappingUnsupportedExpressionKindQueryView,
		// Token: 0x0400107B RID: 4219
		MappingUnsupportedScanTargetQueryView,
		// Token: 0x0400107C RID: 4220
		MappingUnsupportedPropertyKindQueryView,
		// Token: 0x0400107D RID: 4221
		MappingUnsupportedInitializationQueryView,
		// Token: 0x0400107E RID: 4222
		MappingFunctionImportEntityTypeMappingForFunctionNotReturningEntitySet,
		// Token: 0x0400107F RID: 4223
		MappingFunctionImportAmbiguousTypeConditions,
		// Token: 0x04001080 RID: 4224
		MappingOfAbstractType = 2078,
		// Token: 0x04001081 RID: 4225
		StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping,
		// Token: 0x04001082 RID: 4226
		TypeNameForFirstQueryView,
		// Token: 0x04001083 RID: 4227
		NoTypeNameForTypeSpecificQueryView,
		// Token: 0x04001084 RID: 4228
		QueryViewExistsForEntitySetAndType,
		// Token: 0x04001085 RID: 4229
		TypeNameContainsMultipleTypesForQueryView,
		// Token: 0x04001086 RID: 4230
		IsTypeOfQueryViewForBaseType,
		// Token: 0x04001087 RID: 4231
		InvalidTypeInScalarProperty,
		// Token: 0x04001088 RID: 4232
		AlreadyMappedStorageEntityContainer,
		// Token: 0x04001089 RID: 4233
		UnsupportedQueryViewInEntityContainerMapping,
		// Token: 0x0400108A RID: 4234
		MappingAllQueryViewAtCompileTime,
		// Token: 0x0400108B RID: 4235
		MappingNoViewsCanBeGenerated,
		// Token: 0x0400108C RID: 4236
		MappingStoreProviderReturnsNullEdmType,
		// Token: 0x0400108D RID: 4237
		DuplicateMemberMapping = 2092,
		// Token: 0x0400108E RID: 4238
		MappingFunctionImportUnexpectedEntityTypeMapping,
		// Token: 0x0400108F RID: 4239
		MappingFunctionImportUnexpectedComplexTypeMapping,
		// Token: 0x04001090 RID: 4240
		DistinctFragmentInReadWriteContainer = 2096,
		// Token: 0x04001091 RID: 4241
		EntitySetMismatchOnAssociationSetEnd,
		// Token: 0x04001092 RID: 4242
		InvalidModificationFunctionMappingAssociationEndForeignKey,
		// Token: 0x04001093 RID: 4243
		CannotLoadDifferentVersionOfSchemaInTheSameItemCollection = 2100,
		// Token: 0x04001094 RID: 4244
		MappingDifferentMappingEdmStoreVersion,
		// Token: 0x04001095 RID: 4245
		MappingDifferentEdmStoreVersion,
		// Token: 0x04001096 RID: 4246
		UnmappedFunctionImport,
		// Token: 0x04001097 RID: 4247
		MappingFunctionImportReturnTypePropertyNotMapped,
		// Token: 0x04001098 RID: 4248
		InvalidType = 2106,
		// Token: 0x04001099 RID: 4249
		MappingFunctionImportTVFExpected = 2108,
		// Token: 0x0400109A RID: 4250
		MappingFunctionImportScalarMappingTypeMismatch,
		// Token: 0x0400109B RID: 4251
		MappingFunctionImportScalarMappingToMulticolumnTVF,
		// Token: 0x0400109C RID: 4252
		MappingFunctionImportTargetFunctionMustBeComposable,
		// Token: 0x0400109D RID: 4253
		UnsupportedFunctionCallInQueryView,
		// Token: 0x0400109E RID: 4254
		FunctionResultMappingCountMismatch,
		// Token: 0x0400109F RID: 4255
		MappingFunctionImportCannotInferTargetFunctionKeys
	}
}
