using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003E7 RID: 999
	internal static class MslConstructs
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
		public static string GetMslNamespace(double version)
		{
			if (object.Equals(version, 1.0))
			{
				return "urn:schemas-microsoft-com:windows:storage:mapping:CS";
			}
			if (object.Equals(version, 2.0))
			{
				return "http://schemas.microsoft.com/ado/2008/09/mapping/cs";
			}
			return "http://schemas.microsoft.com/ado/2009/11/mapping/cs";
		}

		// Token: 0x04000D63 RID: 3427
		internal const string NamespaceUriV1 = "urn:schemas-microsoft-com:windows:storage:mapping:CS";

		// Token: 0x04000D64 RID: 3428
		internal const string NamespaceUriV2 = "http://schemas.microsoft.com/ado/2008/09/mapping/cs";

		// Token: 0x04000D65 RID: 3429
		internal const string NamespaceUriV3 = "http://schemas.microsoft.com/ado/2009/11/mapping/cs";

		// Token: 0x04000D66 RID: 3430
		internal const double MappingVersionV1 = 1.0;

		// Token: 0x04000D67 RID: 3431
		internal const double MappingVersionV2 = 2.0;

		// Token: 0x04000D68 RID: 3432
		internal const double MappingVersionV3 = 3.0;

		// Token: 0x04000D69 RID: 3433
		internal const string MappingElement = "Mapping";

		// Token: 0x04000D6A RID: 3434
		internal const string GenerateUpdateViews = "GenerateUpdateViews";

		// Token: 0x04000D6B RID: 3435
		internal const string MappingSpaceAttribute = "Space";

		// Token: 0x04000D6C RID: 3436
		internal const string EntityContainerMappingElement = "EntityContainerMapping";

		// Token: 0x04000D6D RID: 3437
		internal const string CdmEntityContainerAttribute = "CdmEntityContainer";

		// Token: 0x04000D6E RID: 3438
		internal const string StorageEntityContainerAttribute = "StorageEntityContainer";

		// Token: 0x04000D6F RID: 3439
		internal const string AliasElement = "Alias";

		// Token: 0x04000D70 RID: 3440
		internal const string AliasKeyAttribute = "Key";

		// Token: 0x04000D71 RID: 3441
		internal const string AliasValueAttribute = "Value";

		// Token: 0x04000D72 RID: 3442
		internal const string EntitySetMappingElement = "EntitySetMapping";

		// Token: 0x04000D73 RID: 3443
		internal const string EntitySetMappingNameAttribute = "Name";

		// Token: 0x04000D74 RID: 3444
		internal const string EntitySetMappingTypeNameAttribute = "TypeName";

		// Token: 0x04000D75 RID: 3445
		internal const string EntitySetMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x04000D76 RID: 3446
		internal const string EntityTypeMappingElement = "EntityTypeMapping";

		// Token: 0x04000D77 RID: 3447
		internal const string QueryViewElement = "QueryView";

		// Token: 0x04000D78 RID: 3448
		internal const string EntityTypeMappingTypeNameAttribute = "TypeName";

		// Token: 0x04000D79 RID: 3449
		internal const string EntityTypeMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x04000D7A RID: 3450
		internal const string AssociationSetMappingElement = "AssociationSetMapping";

		// Token: 0x04000D7B RID: 3451
		internal const string AssociationSetMappingNameAttribute = "Name";

		// Token: 0x04000D7C RID: 3452
		internal const string AssociationSetMappingTypeNameAttribute = "TypeName";

		// Token: 0x04000D7D RID: 3453
		internal const string AssociationSetMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x04000D7E RID: 3454
		internal const string EndPropertyMappingElement = "EndProperty";

		// Token: 0x04000D7F RID: 3455
		internal const string EndPropertyMappingNameAttribute = "Name";

		// Token: 0x04000D80 RID: 3456
		internal const string CompositionSetMappingNameAttribute = "Name";

		// Token: 0x04000D81 RID: 3457
		internal const string CompositionSetMappingTypeNameAttribute = "TypeName";

		// Token: 0x04000D82 RID: 3458
		internal const string CompositionSetMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x04000D83 RID: 3459
		internal const string FunctionImportMappingElement = "FunctionImportMapping";

		// Token: 0x04000D84 RID: 3460
		internal const string FunctionImportMappingFunctionNameAttribute = "FunctionName";

		// Token: 0x04000D85 RID: 3461
		internal const string FunctionImportMappingFunctionImportNameAttribute = "FunctionImportName";

		// Token: 0x04000D86 RID: 3462
		internal const string CompositionSetParentEndName = "Parent";

		// Token: 0x04000D87 RID: 3463
		internal const string CompositionSetChildEndName = "Child";

		// Token: 0x04000D88 RID: 3464
		internal const string MappingFragmentElement = "MappingFragment";

		// Token: 0x04000D89 RID: 3465
		internal const string MappingFragmentStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x04000D8A RID: 3466
		internal const string MappingFragmentMakeColumnsDistinctAttribute = "MakeColumnsDistinct";

		// Token: 0x04000D8B RID: 3467
		internal const string ScalarPropertyElement = "ScalarProperty";

		// Token: 0x04000D8C RID: 3468
		internal const string ScalarPropertyNameAttribute = "Name";

		// Token: 0x04000D8D RID: 3469
		internal const string ScalarPropertyColumnNameAttribute = "ColumnName";

		// Token: 0x04000D8E RID: 3470
		internal const string ScalarPropertyValueAttribute = "Value";

		// Token: 0x04000D8F RID: 3471
		internal const string ComplexPropertyElement = "ComplexProperty";

		// Token: 0x04000D90 RID: 3472
		internal const string AssociationEndElement = "AssociationEnd";

		// Token: 0x04000D91 RID: 3473
		internal const string ComplexPropertyNameAttribute = "Name";

		// Token: 0x04000D92 RID: 3474
		internal const string ComplexPropertyTypeNameAttribute = "TypeName";

		// Token: 0x04000D93 RID: 3475
		internal const string ComplexPropertyIsPartialAttribute = "IsPartial";

		// Token: 0x04000D94 RID: 3476
		internal const string ComplexTypeMappingElement = "ComplexTypeMapping";

		// Token: 0x04000D95 RID: 3477
		internal const string ComplexTypeMappingTypeNameAttribute = "TypeName";

		// Token: 0x04000D96 RID: 3478
		internal const string ConditionElement = "Condition";

		// Token: 0x04000D97 RID: 3479
		internal const string ConditionNameAttribute = "Name";

		// Token: 0x04000D98 RID: 3480
		internal const string ConditionValueAttribute = "Value";

		// Token: 0x04000D99 RID: 3481
		internal const string ConditionColumnNameAttribute = "ColumnName";

		// Token: 0x04000D9A RID: 3482
		internal const string ConditionIsNullAttribute = "IsNull";

		// Token: 0x04000D9B RID: 3483
		internal const string CollectionPropertyNameAttribute = "Name";

		// Token: 0x04000D9C RID: 3484
		internal const string CollectionPropertyIsPartialAttribute = "IsPartial";

		// Token: 0x04000D9D RID: 3485
		internal const string ResourceXsdNameV1 = "System.Data.Resources.CSMSL_1.xsd";

		// Token: 0x04000D9E RID: 3486
		internal const string ResourceXsdNameV2 = "System.Data.Resources.CSMSL_2.xsd";

		// Token: 0x04000D9F RID: 3487
		internal const string ResourceXsdNameV3 = "System.Data.Resources.CSMSL_3.xsd";

		// Token: 0x04000DA0 RID: 3488
		internal const string IsTypeOf = "IsTypeOf(";

		// Token: 0x04000DA1 RID: 3489
		internal const string IsTypeOfTerminal = ")";

		// Token: 0x04000DA2 RID: 3490
		internal const string IsTypeOfOnly = "IsTypeOfOnly(";

		// Token: 0x04000DA3 RID: 3491
		internal const string IsTypeOfOnlyTerminal = ")";

		// Token: 0x04000DA4 RID: 3492
		internal const string ModificationFunctionMappingElement = "ModificationFunctionMapping";

		// Token: 0x04000DA5 RID: 3493
		internal const string DeleteFunctionElement = "DeleteFunction";

		// Token: 0x04000DA6 RID: 3494
		internal const string InsertFunctionElement = "InsertFunction";

		// Token: 0x04000DA7 RID: 3495
		internal const string UpdateFunctionElement = "UpdateFunction";

		// Token: 0x04000DA8 RID: 3496
		internal const string FunctionNameAttribute = "FunctionName";

		// Token: 0x04000DA9 RID: 3497
		internal const string RowsAffectedParameterAttribute = "RowsAffectedParameter";

		// Token: 0x04000DAA RID: 3498
		internal const string ParameterNameAttribute = "ParameterName";

		// Token: 0x04000DAB RID: 3499
		internal const string ParameterVersionAttribute = "Version";

		// Token: 0x04000DAC RID: 3500
		internal const string ParameterVersionAttributeCurrentValue = "Current";

		// Token: 0x04000DAD RID: 3501
		internal const string ParameterVersionAttributeOriginalValue = "Original";

		// Token: 0x04000DAE RID: 3502
		internal const string AssociationSetAttribute = "AssociationSet";

		// Token: 0x04000DAF RID: 3503
		internal const string FromAttribute = "From";

		// Token: 0x04000DB0 RID: 3504
		internal const string ToAttribute = "To";

		// Token: 0x04000DB1 RID: 3505
		internal const string ResultBindingElement = "ResultBinding";

		// Token: 0x04000DB2 RID: 3506
		internal const string ResultBindingPropertyNameAttribute = "Name";

		// Token: 0x04000DB3 RID: 3507
		internal const string ResultBindingColumnNameAttribute = "ColumnName";

		// Token: 0x04000DB4 RID: 3508
		internal const char TypeNameSperator = ';';

		// Token: 0x04000DB5 RID: 3509
		internal const char IdentitySeperator = ':';

		// Token: 0x04000DB6 RID: 3510
		internal const string EntityViewGenerationTypeName = "Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets";

		// Token: 0x04000DB7 RID: 3511
		internal const string FunctionImportMappingResultMapping = "ResultMapping";
	}
}
