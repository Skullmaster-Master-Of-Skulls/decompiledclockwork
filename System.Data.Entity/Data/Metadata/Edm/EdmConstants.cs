using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001CB RID: 459
	internal static class EdmConstants
	{
		// Token: 0x04000D50 RID: 3408
		internal const string EdmNamespace = "Edm";

		// Token: 0x04000D51 RID: 3409
		internal const string ClrPrimitiveTypeNamespace = "System";

		// Token: 0x04000D52 RID: 3410
		internal const string TransientNamespace = "Transient";

		// Token: 0x04000D53 RID: 3411
		internal const int NumPrimitiveTypes = 31;

		// Token: 0x04000D54 RID: 3412
		internal const int NumBuiltInTypes = 40;

		// Token: 0x04000D55 RID: 3413
		internal const int MaxLength = 256;

		// Token: 0x04000D56 RID: 3414
		internal const string AssociationEnd = "AssociationEnd";

		// Token: 0x04000D57 RID: 3415
		internal const string AssociationSetType = "AssocationSetType";

		// Token: 0x04000D58 RID: 3416
		internal const string AssociationSetEndType = "AssociationSetEndType";

		// Token: 0x04000D59 RID: 3417
		internal const string AssociationType = "AssociationType";

		// Token: 0x04000D5A RID: 3418
		internal const string BaseEntitySetType = "BaseEntitySetType";

		// Token: 0x04000D5B RID: 3419
		internal const string CollectionType = "CollectionType";

		// Token: 0x04000D5C RID: 3420
		internal const string ComplexType = "ComplexType";

		// Token: 0x04000D5D RID: 3421
		internal const string DeleteAction = "DeleteAction";

		// Token: 0x04000D5E RID: 3422
		internal const string DeleteBehavior = "DeleteBehavior";

		// Token: 0x04000D5F RID: 3423
		internal const string Documentation = "Documentation";

		// Token: 0x04000D60 RID: 3424
		internal const string EdmType = "EdmType";

		// Token: 0x04000D61 RID: 3425
		internal const string ElementType = "ElementType";

		// Token: 0x04000D62 RID: 3426
		internal const string EntityContainerType = "EntityContainerType";

		// Token: 0x04000D63 RID: 3427
		internal const string EntitySetType = "EntitySetType";

		// Token: 0x04000D64 RID: 3428
		internal const string EntityType = "EntityType";

		// Token: 0x04000D65 RID: 3429
		internal const string EnumerationMember = "EnumMember";

		// Token: 0x04000D66 RID: 3430
		internal const string EnumerationType = "EnumType";

		// Token: 0x04000D67 RID: 3431
		internal const string Facet = "Facet";

		// Token: 0x04000D68 RID: 3432
		internal const string Function = "EdmFunction";

		// Token: 0x04000D69 RID: 3433
		internal const string FunctionParameter = "FunctionParameter";

		// Token: 0x04000D6A RID: 3434
		internal const string GlobalItem = "GlobalItem";

		// Token: 0x04000D6B RID: 3435
		internal const string ItemAttribute = "MetadataProperty";

		// Token: 0x04000D6C RID: 3436
		internal const string ItemType = "ItemType";

		// Token: 0x04000D6D RID: 3437
		internal const string Member = "EdmMember";

		// Token: 0x04000D6E RID: 3438
		internal const string NavigationProperty = "NavigationProperty";

		// Token: 0x04000D6F RID: 3439
		internal const string OperationBehavior = "OperationBehavior";

		// Token: 0x04000D70 RID: 3440
		internal const string OperationBehaviors = "OperationBehaviors";

		// Token: 0x04000D71 RID: 3441
		internal const string ParameterMode = "ParameterMode";

		// Token: 0x04000D72 RID: 3442
		internal const string PrimitiveType = "PrimitiveType";

		// Token: 0x04000D73 RID: 3443
		internal const string PrimitiveTypeKind = "PrimitiveTypeKind";

		// Token: 0x04000D74 RID: 3444
		internal const string Property = "EdmProperty";

		// Token: 0x04000D75 RID: 3445
		internal const string ProviderManifest = "ProviderManifest";

		// Token: 0x04000D76 RID: 3446
		internal const string ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x04000D77 RID: 3447
		internal const string RefType = "RefType";

		// Token: 0x04000D78 RID: 3448
		internal const string RelationshipEnd = "RelationshipEnd";

		// Token: 0x04000D79 RID: 3449
		internal const string RelationshipMultiplicity = "RelationshipMultiplicity";

		// Token: 0x04000D7A RID: 3450
		internal const string RelationshipSet = "RelationshipSet";

		// Token: 0x04000D7B RID: 3451
		internal const string RelationshipType = "RelationshipType";

		// Token: 0x04000D7C RID: 3452
		internal const string ReturnParameter = "ReturnParameter";

		// Token: 0x04000D7D RID: 3453
		internal const string Role = "Role";

		// Token: 0x04000D7E RID: 3454
		internal const string RowType = "RowType";

		// Token: 0x04000D7F RID: 3455
		internal const string SimpleType = "SimpleType";

		// Token: 0x04000D80 RID: 3456
		internal const string StructuralType = "StructuralType";

		// Token: 0x04000D81 RID: 3457
		internal const string TypeUsage = "TypeUsage";

		// Token: 0x04000D82 RID: 3458
		internal const string Utc = "Utc";

		// Token: 0x04000D83 RID: 3459
		internal const string Unspecified = "Unspecified";

		// Token: 0x04000D84 RID: 3460
		internal const string Local = "Local";

		// Token: 0x04000D85 RID: 3461
		internal const string One = "One";

		// Token: 0x04000D86 RID: 3462
		internal const string ZeroToOne = "ZeroToOne";

		// Token: 0x04000D87 RID: 3463
		internal const string Many = "Many";

		// Token: 0x04000D88 RID: 3464
		internal const string In = "In";

		// Token: 0x04000D89 RID: 3465
		internal const string Out = "Out";

		// Token: 0x04000D8A RID: 3466
		internal const string InOut = "InOut";

		// Token: 0x04000D8B RID: 3467
		internal const string None = "None";

		// Token: 0x04000D8C RID: 3468
		internal const string Cascade = "Cascade";

		// Token: 0x04000D8D RID: 3469
		internal const string Restrict = "Restrict";

		// Token: 0x04000D8E RID: 3470
		internal const string NoneCollectionKind = "None";

		// Token: 0x04000D8F RID: 3471
		internal const string ListCollectionKind = "List";

		// Token: 0x04000D90 RID: 3472
		internal const string BagCollectionKind = "Bag";

		// Token: 0x04000D91 RID: 3473
		internal const string MaxMaxLength = "Max";

		// Token: 0x04000D92 RID: 3474
		internal const string VariableSrid = "Variable";

		// Token: 0x04000D93 RID: 3475
		internal const string AssociationSetEnds = "AssociationSetEnds";

		// Token: 0x04000D94 RID: 3476
		internal const string Child = "Child";

		// Token: 0x04000D95 RID: 3477
		internal const string DefaultValue = "DefaultValue";

		// Token: 0x04000D96 RID: 3478
		internal const string Ends = "Ends";

		// Token: 0x04000D97 RID: 3479
		internal const string EntitySet = "EntitySet";

		// Token: 0x04000D98 RID: 3480
		internal const string AssociationSet = "AssociationSet";

		// Token: 0x04000D99 RID: 3481
		internal const string EntitySets = "EntitySets";

		// Token: 0x04000D9A RID: 3482
		internal const string Facets = "Facets";

		// Token: 0x04000D9B RID: 3483
		internal const string FromProperties = "FromProperties";

		// Token: 0x04000D9C RID: 3484
		internal const string FromRole = "FromRole";

		// Token: 0x04000D9D RID: 3485
		internal const string IsParent = "IsParent";

		// Token: 0x04000D9E RID: 3486
		internal const string KeyMembers = "KeyMembers";

		// Token: 0x04000D9F RID: 3487
		internal const string Members = "Members";

		// Token: 0x04000DA0 RID: 3488
		internal const string Mode = "Mode";

		// Token: 0x04000DA1 RID: 3489
		internal const string Nullable = "Nullable";

		// Token: 0x04000DA2 RID: 3490
		internal const string Parameters = "Parameters";

		// Token: 0x04000DA3 RID: 3491
		internal const string Parent = "Parent";

		// Token: 0x04000DA4 RID: 3492
		internal const string Properties = "Properties";

		// Token: 0x04000DA5 RID: 3493
		internal const string ToProperties = "ToProperties";

		// Token: 0x04000DA6 RID: 3494
		internal const string ToRole = "ToRole";

		// Token: 0x04000DA7 RID: 3495
		internal const string ReferentialConstraints = "ReferentialConstraints";

		// Token: 0x04000DA8 RID: 3496
		internal const string RelationshipTypeName = "RelationshipTypeName";

		// Token: 0x04000DA9 RID: 3497
		internal const string ReturnType = "ReturnType";

		// Token: 0x04000DAA RID: 3498
		internal const string ToEndMemberName = "ToEndMemberName";

		// Token: 0x04000DAB RID: 3499
		internal const string CollectionKind = "CollectionKind";

		// Token: 0x04000DAC RID: 3500
		internal const string Binary = "Binary";

		// Token: 0x04000DAD RID: 3501
		internal const string Boolean = "Boolean";

		// Token: 0x04000DAE RID: 3502
		internal const string Byte = "Byte";

		// Token: 0x04000DAF RID: 3503
		internal const string DateTime = "DateTime";

		// Token: 0x04000DB0 RID: 3504
		internal const string Decimal = "Decimal";

		// Token: 0x04000DB1 RID: 3505
		internal const string Double = "Double";

		// Token: 0x04000DB2 RID: 3506
		internal const string Geometry = "Geometry";

		// Token: 0x04000DB3 RID: 3507
		internal const string GeometryPoint = "GeometryPoint";

		// Token: 0x04000DB4 RID: 3508
		internal const string GeometryLineString = "GeometryLineString";

		// Token: 0x04000DB5 RID: 3509
		internal const string GeometryPolygon = "GeometryPolygon";

		// Token: 0x04000DB6 RID: 3510
		internal const string GeometryMultiPoint = "GeometryMultiPoint";

		// Token: 0x04000DB7 RID: 3511
		internal const string GeometryMultiLineString = "GeometryMultiLineString";

		// Token: 0x04000DB8 RID: 3512
		internal const string GeometryMultiPolygon = "GeometryMultiPolygon";

		// Token: 0x04000DB9 RID: 3513
		internal const string GeometryCollection = "GeometryCollection";

		// Token: 0x04000DBA RID: 3514
		internal const string Geography = "Geography";

		// Token: 0x04000DBB RID: 3515
		internal const string GeographyPoint = "GeographyPoint";

		// Token: 0x04000DBC RID: 3516
		internal const string GeographyLineString = "GeographyLineString";

		// Token: 0x04000DBD RID: 3517
		internal const string GeographyPolygon = "GeographyPolygon";

		// Token: 0x04000DBE RID: 3518
		internal const string GeographyMultiPoint = "GeographyMultiPoint";

		// Token: 0x04000DBF RID: 3519
		internal const string GeographyMultiLineString = "GeographyMultiLineString";

		// Token: 0x04000DC0 RID: 3520
		internal const string GeographyMultiPolygon = "GeographyMultiPolygon";

		// Token: 0x04000DC1 RID: 3521
		internal const string GeographyCollection = "GeographyCollection";

		// Token: 0x04000DC2 RID: 3522
		internal const string Guid = "Guid";

		// Token: 0x04000DC3 RID: 3523
		internal const string Single = "Single";

		// Token: 0x04000DC4 RID: 3524
		internal const string SByte = "SByte";

		// Token: 0x04000DC5 RID: 3525
		internal const string Int16 = "Int16";

		// Token: 0x04000DC6 RID: 3526
		internal const string Int32 = "Int32";

		// Token: 0x04000DC7 RID: 3527
		internal const string Int64 = "Int64";

		// Token: 0x04000DC8 RID: 3528
		internal const string Money = "Money";

		// Token: 0x04000DC9 RID: 3529
		internal const string Null = "Null";

		// Token: 0x04000DCA RID: 3530
		internal const string String = "String";

		// Token: 0x04000DCB RID: 3531
		internal const string DateTimeOffset = "DateTimeOffset";

		// Token: 0x04000DCC RID: 3532
		internal const string Time = "Time";

		// Token: 0x04000DCD RID: 3533
		internal const string UInt16 = "UInt16";

		// Token: 0x04000DCE RID: 3534
		internal const string UInt32 = "UInt32";

		// Token: 0x04000DCF RID: 3535
		internal const string UInt64 = "UInt64";

		// Token: 0x04000DD0 RID: 3536
		internal const string Xml = "Xml";

		// Token: 0x04000DD1 RID: 3537
		internal const string Name = "Name";

		// Token: 0x04000DD2 RID: 3538
		internal const string Namespace = "Namespace";

		// Token: 0x04000DD3 RID: 3539
		internal const string Abstract = "Abstract";

		// Token: 0x04000DD4 RID: 3540
		internal const string BaseType = "BaseType";

		// Token: 0x04000DD5 RID: 3541
		internal const string Sealed = "Sealed";

		// Token: 0x04000DD6 RID: 3542
		internal const string ItemAttributes = "MetadataProperties";

		// Token: 0x04000DD7 RID: 3543
		internal const string Type = "Type";

		// Token: 0x04000DD8 RID: 3544
		internal const string Schema = "Schema";

		// Token: 0x04000DD9 RID: 3545
		internal const string Table = "Table";

		// Token: 0x04000DDA RID: 3546
		internal const string FacetType = "FacetType";

		// Token: 0x04000DDB RID: 3547
		internal const string Value = "Value";

		// Token: 0x04000DDC RID: 3548
		internal const string EnumMembers = "EnumMembers";

		// Token: 0x04000DDD RID: 3549
		internal const string BuiltInAttribute = "BuiltInAttribute";

		// Token: 0x04000DDE RID: 3550
		internal const string StoreFunctionNamespace = "StoreFunctionNamespace";

		// Token: 0x04000DDF RID: 3551
		internal const string ParameterTypeSemanticsAttribute = "ParameterTypeSemanticsAttribute";

		// Token: 0x04000DE0 RID: 3552
		internal const string ParameterTypeSemantics = "ParameterTypeSemantics";

		// Token: 0x04000DE1 RID: 3553
		internal const string NiladicFunctionAttribute = "NiladicFunctionAttribute";

		// Token: 0x04000DE2 RID: 3554
		internal const string IsComposableFunctionAttribute = "IsComposable";

		// Token: 0x04000DE3 RID: 3555
		internal const string CommandTextFunctionAttribyte = "CommandText";

		// Token: 0x04000DE4 RID: 3556
		internal const string StoreFunctionNameAttribute = "StoreFunctionNameAttribute";

		// Token: 0x04000DE5 RID: 3557
		internal const string WebHomeSymbol = "~";

		// Token: 0x04000DE6 RID: 3558
		internal const string Summary = "Summary";

		// Token: 0x04000DE7 RID: 3559
		internal const string LongDescription = "LongDescription";

		// Token: 0x04000DE8 RID: 3560
		internal static readonly EdmConstants.Unbounded UnboundedValue = EdmConstants.Unbounded.Instance;

		// Token: 0x04000DE9 RID: 3561
		internal static readonly EdmConstants.Variable VariableValue = EdmConstants.Variable.Instance;

		// Token: 0x02000515 RID: 1301
		internal class Unbounded
		{
			// Token: 0x06003DD6 RID: 15830 RVA: 0x00002050 File Offset: 0x00000250
			private Unbounded()
			{
			}

			// Token: 0x17000B10 RID: 2832
			// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x000E737F File Offset: 0x000E557F
			internal static EdmConstants.Unbounded Instance
			{
				get
				{
					return EdmConstants.Unbounded._instance;
				}
			}

			// Token: 0x06003DD8 RID: 15832 RVA: 0x000E7386 File Offset: 0x000E5586
			public override string ToString()
			{
				return "Max";
			}

			// Token: 0x04001B1B RID: 6939
			private static readonly EdmConstants.Unbounded _instance = new EdmConstants.Unbounded();
		}

		// Token: 0x02000516 RID: 1302
		internal class Variable
		{
			// Token: 0x06003DDA RID: 15834 RVA: 0x00002050 File Offset: 0x00000250
			private Variable()
			{
			}

			// Token: 0x17000B11 RID: 2833
			// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000E7399 File Offset: 0x000E5599
			internal static EdmConstants.Variable Instance
			{
				get
				{
					return EdmConstants.Variable._instance;
				}
			}

			// Token: 0x06003DDC RID: 15836 RVA: 0x000E73A0 File Offset: 0x000E55A0
			public override string ToString()
			{
				return "Variable";
			}

			// Token: 0x04001B1C RID: 6940
			private static readonly EdmConstants.Variable _instance = new EdmConstants.Variable();
		}
	}
}
