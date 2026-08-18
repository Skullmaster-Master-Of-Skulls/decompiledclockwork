using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D7 RID: 1239
	internal static class EdmConstants
	{
		// Token: 0x040010E3 RID: 4323
		internal const string EdmNamespace = "Edm";

		// Token: 0x040010E4 RID: 4324
		internal const string ClrPrimitiveTypeNamespace = "System";

		// Token: 0x040010E5 RID: 4325
		internal const string TransientNamespace = "Transient";

		// Token: 0x040010E6 RID: 4326
		internal const int NumPrimitiveTypes = 31;

		// Token: 0x040010E7 RID: 4327
		internal const int NumBuiltInTypes = 40;

		// Token: 0x040010E8 RID: 4328
		internal const int MaxLength = 256;

		// Token: 0x040010E9 RID: 4329
		internal const string AssociationEnd = "AssociationEnd";

		// Token: 0x040010EA RID: 4330
		internal const string AssociationSetType = "AssocationSetType";

		// Token: 0x040010EB RID: 4331
		internal const string AssociationSetEndType = "AssociationSetEndType";

		// Token: 0x040010EC RID: 4332
		internal const string AssociationType = "AssociationType";

		// Token: 0x040010ED RID: 4333
		internal const string BaseEntitySetType = "BaseEntitySetType";

		// Token: 0x040010EE RID: 4334
		internal const string CollectionType = "CollectionType";

		// Token: 0x040010EF RID: 4335
		internal const string ComplexType = "ComplexType";

		// Token: 0x040010F0 RID: 4336
		internal const string DeleteAction = "DeleteAction";

		// Token: 0x040010F1 RID: 4337
		internal const string DeleteBehavior = "DeleteBehavior";

		// Token: 0x040010F2 RID: 4338
		internal const string Documentation = "Documentation";

		// Token: 0x040010F3 RID: 4339
		internal const string EdmType = "EdmType";

		// Token: 0x040010F4 RID: 4340
		internal const string ElementType = "ElementType";

		// Token: 0x040010F5 RID: 4341
		internal const string EntityContainerType = "EntityContainerType";

		// Token: 0x040010F6 RID: 4342
		internal const string EntitySetType = "EntitySetType";

		// Token: 0x040010F7 RID: 4343
		internal const string EntityType = "EntityType";

		// Token: 0x040010F8 RID: 4344
		internal const string EnumerationMember = "EnumMember";

		// Token: 0x040010F9 RID: 4345
		internal const string EnumerationType = "EnumType";

		// Token: 0x040010FA RID: 4346
		internal const string Facet = "Facet";

		// Token: 0x040010FB RID: 4347
		internal const string Function = "EdmFunction";

		// Token: 0x040010FC RID: 4348
		internal const string FunctionParameter = "FunctionParameter";

		// Token: 0x040010FD RID: 4349
		internal const string GlobalItem = "GlobalItem";

		// Token: 0x040010FE RID: 4350
		internal const string ItemAttribute = "MetadataProperty";

		// Token: 0x040010FF RID: 4351
		internal const string ItemType = "ItemType";

		// Token: 0x04001100 RID: 4352
		internal const string Member = "EdmMember";

		// Token: 0x04001101 RID: 4353
		internal const string NavigationProperty = "NavigationProperty";

		// Token: 0x04001102 RID: 4354
		internal const string OperationBehavior = "OperationBehavior";

		// Token: 0x04001103 RID: 4355
		internal const string OperationBehaviors = "OperationBehaviors";

		// Token: 0x04001104 RID: 4356
		internal const string ParameterMode = "ParameterMode";

		// Token: 0x04001105 RID: 4357
		internal const string PrimitiveType = "PrimitiveType";

		// Token: 0x04001106 RID: 4358
		internal const string PrimitiveTypeKind = "PrimitiveTypeKind";

		// Token: 0x04001107 RID: 4359
		internal const string Property = "EdmProperty";

		// Token: 0x04001108 RID: 4360
		internal const string ProviderManifest = "ProviderManifest";

		// Token: 0x04001109 RID: 4361
		internal const string ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x0400110A RID: 4362
		internal const string RefType = "RefType";

		// Token: 0x0400110B RID: 4363
		internal const string RelationshipEnd = "RelationshipEnd";

		// Token: 0x0400110C RID: 4364
		internal const string RelationshipMultiplicity = "RelationshipMultiplicity";

		// Token: 0x0400110D RID: 4365
		internal const string RelationshipSet = "RelationshipSet";

		// Token: 0x0400110E RID: 4366
		internal const string RelationshipType = "RelationshipType";

		// Token: 0x0400110F RID: 4367
		internal const string ReturnParameter = "ReturnParameter";

		// Token: 0x04001110 RID: 4368
		internal const string Role = "Role";

		// Token: 0x04001111 RID: 4369
		internal const string RowType = "RowType";

		// Token: 0x04001112 RID: 4370
		internal const string SimpleType = "SimpleType";

		// Token: 0x04001113 RID: 4371
		internal const string StructuralType = "StructuralType";

		// Token: 0x04001114 RID: 4372
		internal const string TypeUsage = "TypeUsage";

		// Token: 0x04001115 RID: 4373
		internal const string Utc = "Utc";

		// Token: 0x04001116 RID: 4374
		internal const string Unspecified = "Unspecified";

		// Token: 0x04001117 RID: 4375
		internal const string Local = "Local";

		// Token: 0x04001118 RID: 4376
		internal const string One = "One";

		// Token: 0x04001119 RID: 4377
		internal const string ZeroToOne = "ZeroToOne";

		// Token: 0x0400111A RID: 4378
		internal const string Many = "Many";

		// Token: 0x0400111B RID: 4379
		internal const string In = "In";

		// Token: 0x0400111C RID: 4380
		internal const string Out = "Out";

		// Token: 0x0400111D RID: 4381
		internal const string InOut = "InOut";

		// Token: 0x0400111E RID: 4382
		internal const string None = "None";

		// Token: 0x0400111F RID: 4383
		internal const string Cascade = "Cascade";

		// Token: 0x04001120 RID: 4384
		internal const string NoneCollectionKind = "None";

		// Token: 0x04001121 RID: 4385
		internal const string ListCollectionKind = "List";

		// Token: 0x04001122 RID: 4386
		internal const string BagCollectionKind = "Bag";

		// Token: 0x04001123 RID: 4387
		internal const string MaxMaxLength = "Max";

		// Token: 0x04001124 RID: 4388
		internal const string VariableSrid = "Variable";

		// Token: 0x04001125 RID: 4389
		internal const string AssociationSetEnds = "AssociationSetEnds";

		// Token: 0x04001126 RID: 4390
		internal const string Child = "Child";

		// Token: 0x04001127 RID: 4391
		internal const string DefaultValue = "DefaultValue";

		// Token: 0x04001128 RID: 4392
		internal const string Ends = "Ends";

		// Token: 0x04001129 RID: 4393
		internal const string EntitySet = "EntitySet";

		// Token: 0x0400112A RID: 4394
		internal const string AssociationSet = "AssociationSet";

		// Token: 0x0400112B RID: 4395
		internal const string EntitySets = "EntitySets";

		// Token: 0x0400112C RID: 4396
		internal const string Facets = "Facets";

		// Token: 0x0400112D RID: 4397
		internal const string FromProperties = "FromProperties";

		// Token: 0x0400112E RID: 4398
		internal const string FromRole = "FromRole";

		// Token: 0x0400112F RID: 4399
		internal const string IsParent = "IsParent";

		// Token: 0x04001130 RID: 4400
		internal const string KeyMembers = "KeyMembers";

		// Token: 0x04001131 RID: 4401
		internal const string Members = "Members";

		// Token: 0x04001132 RID: 4402
		internal const string Mode = "Mode";

		// Token: 0x04001133 RID: 4403
		internal const string Nullable = "Nullable";

		// Token: 0x04001134 RID: 4404
		internal const string Parameters = "Parameters";

		// Token: 0x04001135 RID: 4405
		internal const string Parent = "Parent";

		// Token: 0x04001136 RID: 4406
		internal const string Properties = "Properties";

		// Token: 0x04001137 RID: 4407
		internal const string ToProperties = "ToProperties";

		// Token: 0x04001138 RID: 4408
		internal const string ToRole = "ToRole";

		// Token: 0x04001139 RID: 4409
		internal const string ReferentialConstraints = "ReferentialConstraints";

		// Token: 0x0400113A RID: 4410
		internal const string RelationshipTypeName = "RelationshipTypeName";

		// Token: 0x0400113B RID: 4411
		internal const string ReturnType = "ReturnType";

		// Token: 0x0400113C RID: 4412
		internal const string ToEndMemberName = "ToEndMemberName";

		// Token: 0x0400113D RID: 4413
		internal const string CollectionKind = "CollectionKind";

		// Token: 0x0400113E RID: 4414
		internal const string Binary = "Binary";

		// Token: 0x0400113F RID: 4415
		internal const string Boolean = "Boolean";

		// Token: 0x04001140 RID: 4416
		internal const string Byte = "Byte";

		// Token: 0x04001141 RID: 4417
		internal const string DateTime = "DateTime";

		// Token: 0x04001142 RID: 4418
		internal const string Decimal = "Decimal";

		// Token: 0x04001143 RID: 4419
		internal const string Double = "Double";

		// Token: 0x04001144 RID: 4420
		internal const string Geometry = "Geometry";

		// Token: 0x04001145 RID: 4421
		internal const string GeometryPoint = "GeometryPoint";

		// Token: 0x04001146 RID: 4422
		internal const string GeometryLineString = "GeometryLineString";

		// Token: 0x04001147 RID: 4423
		internal const string GeometryPolygon = "GeometryPolygon";

		// Token: 0x04001148 RID: 4424
		internal const string GeometryMultiPoint = "GeometryMultiPoint";

		// Token: 0x04001149 RID: 4425
		internal const string GeometryMultiLineString = "GeometryMultiLineString";

		// Token: 0x0400114A RID: 4426
		internal const string GeometryMultiPolygon = "GeometryMultiPolygon";

		// Token: 0x0400114B RID: 4427
		internal const string GeometryCollection = "GeometryCollection";

		// Token: 0x0400114C RID: 4428
		internal const string Geography = "Geography";

		// Token: 0x0400114D RID: 4429
		internal const string GeographyPoint = "GeographyPoint";

		// Token: 0x0400114E RID: 4430
		internal const string GeographyLineString = "GeographyLineString";

		// Token: 0x0400114F RID: 4431
		internal const string GeographyPolygon = "GeographyPolygon";

		// Token: 0x04001150 RID: 4432
		internal const string GeographyMultiPoint = "GeographyMultiPoint";

		// Token: 0x04001151 RID: 4433
		internal const string GeographyMultiLineString = "GeographyMultiLineString";

		// Token: 0x04001152 RID: 4434
		internal const string GeographyMultiPolygon = "GeographyMultiPolygon";

		// Token: 0x04001153 RID: 4435
		internal const string GeographyCollection = "GeographyCollection";

		// Token: 0x04001154 RID: 4436
		internal const string Guid = "Guid";

		// Token: 0x04001155 RID: 4437
		internal const string Single = "Single";

		// Token: 0x04001156 RID: 4438
		internal const string SByte = "SByte";

		// Token: 0x04001157 RID: 4439
		internal const string Int16 = "Int16";

		// Token: 0x04001158 RID: 4440
		internal const string Int32 = "Int32";

		// Token: 0x04001159 RID: 4441
		internal const string Int64 = "Int64";

		// Token: 0x0400115A RID: 4442
		internal const string Money = "Money";

		// Token: 0x0400115B RID: 4443
		internal const string Null = "Null";

		// Token: 0x0400115C RID: 4444
		internal const string String = "String";

		// Token: 0x0400115D RID: 4445
		internal const string DateTimeOffset = "DateTimeOffset";

		// Token: 0x0400115E RID: 4446
		internal const string Time = "Time";

		// Token: 0x0400115F RID: 4447
		internal const string UInt16 = "UInt16";

		// Token: 0x04001160 RID: 4448
		internal const string UInt32 = "UInt32";

		// Token: 0x04001161 RID: 4449
		internal const string UInt64 = "UInt64";

		// Token: 0x04001162 RID: 4450
		internal const string Xml = "Xml";

		// Token: 0x04001163 RID: 4451
		internal const string Name = "Name";

		// Token: 0x04001164 RID: 4452
		internal const string Namespace = "Namespace";

		// Token: 0x04001165 RID: 4453
		internal const string Abstract = "Abstract";

		// Token: 0x04001166 RID: 4454
		internal const string BaseType = "BaseType";

		// Token: 0x04001167 RID: 4455
		internal const string Sealed = "Sealed";

		// Token: 0x04001168 RID: 4456
		internal const string ItemAttributes = "MetadataProperties";

		// Token: 0x04001169 RID: 4457
		internal const string Type = "Type";

		// Token: 0x0400116A RID: 4458
		internal const string Schema = "Schema";

		// Token: 0x0400116B RID: 4459
		internal const string Table = "Table";

		// Token: 0x0400116C RID: 4460
		internal const string FacetType = "FacetType";

		// Token: 0x0400116D RID: 4461
		internal const string Value = "Value";

		// Token: 0x0400116E RID: 4462
		internal const string EnumMembers = "EnumMembers";

		// Token: 0x0400116F RID: 4463
		internal const string BuiltInAttribute = "BuiltInAttribute";

		// Token: 0x04001170 RID: 4464
		internal const string StoreFunctionNamespace = "StoreFunctionNamespace";

		// Token: 0x04001171 RID: 4465
		internal const string ParameterTypeSemanticsAttribute = "ParameterTypeSemanticsAttribute";

		// Token: 0x04001172 RID: 4466
		internal const string ParameterTypeSemantics = "ParameterTypeSemantics";

		// Token: 0x04001173 RID: 4467
		internal const string NiladicFunctionAttribute = "NiladicFunctionAttribute";

		// Token: 0x04001174 RID: 4468
		internal const string IsComposableFunctionAttribute = "IsComposable";

		// Token: 0x04001175 RID: 4469
		internal const string CommandTextFunctionAttribyte = "CommandText";

		// Token: 0x04001176 RID: 4470
		internal const string StoreFunctionNameAttribute = "StoreFunctionNameAttribute";

		// Token: 0x04001177 RID: 4471
		internal const string WebHomeSymbol = "~";

		// Token: 0x04001178 RID: 4472
		internal const string Summary = "Summary";

		// Token: 0x04001179 RID: 4473
		internal const string LongDescription = "LongDescription";

		// Token: 0x0400117A RID: 4474
		internal static readonly EdmConstants.Unbounded UnboundedValue = EdmConstants.Unbounded.Instance;

		// Token: 0x0400117B RID: 4475
		internal static readonly EdmConstants.Variable VariableValue = EdmConstants.Variable.Instance;

		// Token: 0x020004D8 RID: 1240
		internal class Unbounded
		{
			// Token: 0x06002DA5 RID: 11685 RVA: 0x000DC697 File Offset: 0x000DA897
			private Unbounded()
			{
			}

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000DC69F File Offset: 0x000DA89F
			internal static EdmConstants.Unbounded Instance
			{
				get
				{
					return EdmConstants.Unbounded._instance;
				}
			}

			// Token: 0x06002DA7 RID: 11687 RVA: 0x000DC6A6 File Offset: 0x000DA8A6
			public override string ToString()
			{
				return "Max";
			}

			// Token: 0x0400117C RID: 4476
			private static readonly EdmConstants.Unbounded _instance = new EdmConstants.Unbounded();
		}

		// Token: 0x020004D9 RID: 1241
		internal class Variable
		{
			// Token: 0x06002DA9 RID: 11689 RVA: 0x000DC6B9 File Offset: 0x000DA8B9
			private Variable()
			{
			}

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000DC6C1 File Offset: 0x000DA8C1
			internal static EdmConstants.Variable Instance
			{
				get
				{
					return EdmConstants.Variable._instance;
				}
			}

			// Token: 0x06002DAB RID: 11691 RVA: 0x000DC6C8 File Offset: 0x000DA8C8
			public override string ToString()
			{
				return "Variable";
			}

			// Token: 0x0400117D RID: 4477
			private static readonly EdmConstants.Variable _instance = new EdmConstants.Variable();
		}
	}
}
