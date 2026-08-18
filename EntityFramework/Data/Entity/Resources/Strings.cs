using System;
using System.CodeDom.Compiler;

namespace System.Data.Entity.Resources
{
	// Token: 0x0200072D RID: 1837
	[GeneratedCode("Resources.tt", "1.0.0.0")]
	internal static class Strings
	{
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06004B6B RID: 19307 RVA: 0x00161B4C File Offset: 0x0015FD4C
		internal static string AutomaticMigration
		{
			get
			{
				return EntityRes.GetString("AutomaticMigration");
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06004B6C RID: 19308 RVA: 0x00161B58 File Offset: 0x0015FD58
		internal static string BootstrapMigration
		{
			get
			{
				return EntityRes.GetString("BootstrapMigration");
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06004B6D RID: 19309 RVA: 0x00161B64 File Offset: 0x0015FD64
		internal static string InitialCreate
		{
			get
			{
				return EntityRes.GetString("InitialCreate");
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x00161B70 File Offset: 0x0015FD70
		internal static string AutomaticDataLoss
		{
			get
			{
				return EntityRes.GetString("AutomaticDataLoss");
			}
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00161B7C File Offset: 0x0015FD7C
		internal static string LoggingAutoMigrate(object p0)
		{
			return EntityRes.GetString("LoggingAutoMigrate", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x00161BA0 File Offset: 0x0015FDA0
		internal static string LoggingRevertAutoMigrate(object p0)
		{
			return EntityRes.GetString("LoggingRevertAutoMigrate", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00161BC4 File Offset: 0x0015FDC4
		internal static string LoggingApplyMigration(object p0)
		{
			return EntityRes.GetString("LoggingApplyMigration", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00161BE8 File Offset: 0x0015FDE8
		internal static string LoggingRevertMigration(object p0)
		{
			return EntityRes.GetString("LoggingRevertMigration", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06004B73 RID: 19315 RVA: 0x00161C0B File Offset: 0x0015FE0B
		internal static string LoggingSeedingDatabase
		{
			get
			{
				return EntityRes.GetString("LoggingSeedingDatabase");
			}
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00161C18 File Offset: 0x0015FE18
		internal static string LoggingPendingMigrations(object p0, object p1)
		{
			return EntityRes.GetString("LoggingPendingMigrations", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x00161C40 File Offset: 0x0015FE40
		internal static string LoggingPendingMigrationsDown(object p0, object p1)
		{
			return EntityRes.GetString("LoggingPendingMigrationsDown", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06004B76 RID: 19318 RVA: 0x00161C67 File Offset: 0x0015FE67
		internal static string LoggingNoExplicitMigrations
		{
			get
			{
				return EntityRes.GetString("LoggingNoExplicitMigrations");
			}
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00161C74 File Offset: 0x0015FE74
		internal static string LoggingAlreadyAtTarget(object p0)
		{
			return EntityRes.GetString("LoggingAlreadyAtTarget", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00161C98 File Offset: 0x0015FE98
		internal static string LoggingTargetDatabase(object p0)
		{
			return EntityRes.GetString("LoggingTargetDatabase", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00161CBC File Offset: 0x0015FEBC
		internal static string LoggingTargetDatabaseFormat(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("LoggingTargetDatabaseFormat", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06004B7A RID: 19322 RVA: 0x00161CEB File Offset: 0x0015FEEB
		internal static string LoggingExplicit
		{
			get
			{
				return EntityRes.GetString("LoggingExplicit");
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x00161CF7 File Offset: 0x0015FEF7
		internal static string UpgradingHistoryTable
		{
			get
			{
				return EntityRes.GetString("UpgradingHistoryTable");
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x00161D03 File Offset: 0x0015FF03
		internal static string MetadataOutOfDate
		{
			get
			{
				return EntityRes.GetString("MetadataOutOfDate");
			}
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00161D10 File Offset: 0x0015FF10
		internal static string MigrationNotFound(object p0)
		{
			return EntityRes.GetString("MigrationNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00161D34 File Offset: 0x0015FF34
		internal static string PartialFkOperation(object p0, object p1)
		{
			return EntityRes.GetString("PartialFkOperation", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00161D5C File Offset: 0x0015FF5C
		internal static string AutoNotValidTarget(object p0)
		{
			return EntityRes.GetString("AutoNotValidTarget", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00161D80 File Offset: 0x0015FF80
		internal static string AutoNotValidForScriptWindows(object p0)
		{
			return EntityRes.GetString("AutoNotValidForScriptWindows", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00161DA4 File Offset: 0x0015FFA4
		internal static string ContextNotConstructible(object p0)
		{
			return EntityRes.GetString("ContextNotConstructible", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00161DC8 File Offset: 0x0015FFC8
		internal static string AmbiguousMigrationName(object p0)
		{
			return EntityRes.GetString("AmbiguousMigrationName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06004B83 RID: 19331 RVA: 0x00161DEB File Offset: 0x0015FFEB
		internal static string AutomaticDisabledException
		{
			get
			{
				return EntityRes.GetString("AutomaticDisabledException");
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06004B84 RID: 19332 RVA: 0x00161DF7 File Offset: 0x0015FFF7
		internal static string DownScriptWindowsNotSupported
		{
			get
			{
				return EntityRes.GetString("DownScriptWindowsNotSupported");
			}
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x00161E04 File Offset: 0x00160004
		internal static string AssemblyMigrator_NoConfigurationWithName(object p0, object p1)
		{
			return EntityRes.GetString("AssemblyMigrator_NoConfigurationWithName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x00161E2C File Offset: 0x0016002C
		internal static string AssemblyMigrator_MultipleConfigurationsWithName(object p0, object p1)
		{
			return EntityRes.GetString("AssemblyMigrator_MultipleConfigurationsWithName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00161E54 File Offset: 0x00160054
		internal static string AssemblyMigrator_NoConfiguration(object p0)
		{
			return EntityRes.GetString("AssemblyMigrator_NoConfiguration", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x00161E78 File Offset: 0x00160078
		internal static string AssemblyMigrator_MultipleConfigurations(object p0)
		{
			return EntityRes.GetString("AssemblyMigrator_MultipleConfigurations", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x00161E9C File Offset: 0x0016009C
		internal static string MigrationsNamespaceNotUnderRootNamespace(object p0, object p1)
		{
			return EntityRes.GetString("MigrationsNamespaceNotUnderRootNamespace", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x00161EC4 File Offset: 0x001600C4
		internal static string UnableToDispatchAddOrUpdate(object p0)
		{
			return EntityRes.GetString("UnableToDispatchAddOrUpdate", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x00161EE8 File Offset: 0x001600E8
		internal static string NoSqlGeneratorForProvider(object p0)
		{
			return EntityRes.GetString("NoSqlGeneratorForProvider", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x00161F0C File Offset: 0x0016010C
		internal static string ToolingFacade_AssemblyNotFound(object p0)
		{
			return EntityRes.GetString("ToolingFacade_AssemblyNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x00161F30 File Offset: 0x00160130
		internal static string ArgumentIsNullOrWhitespace(object p0)
		{
			return EntityRes.GetString("ArgumentIsNullOrWhitespace", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x00161F54 File Offset: 0x00160154
		internal static string EntityTypeConfigurationMismatch(object p0)
		{
			return EntityRes.GetString("EntityTypeConfigurationMismatch", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x00161F78 File Offset: 0x00160178
		internal static string ComplexTypeConfigurationMismatch(object p0)
		{
			return EntityRes.GetString("ComplexTypeConfigurationMismatch", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x00161F9C File Offset: 0x0016019C
		internal static string KeyPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("KeyPropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x00161FC4 File Offset: 0x001601C4
		internal static string ForeignKeyPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ForeignKeyPropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x00161FEC File Offset: 0x001601EC
		internal static string PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("PropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x00162014 File Offset: 0x00160214
		internal static string NavigationPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("NavigationPropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0016203C File Offset: 0x0016023C
		internal static string InvalidPropertyExpression(object p0)
		{
			return EntityRes.GetString("InvalidPropertyExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x00162060 File Offset: 0x00160260
		internal static string InvalidComplexPropertyExpression(object p0)
		{
			return EntityRes.GetString("InvalidComplexPropertyExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x00162084 File Offset: 0x00160284
		internal static string InvalidPropertiesExpression(object p0)
		{
			return EntityRes.GetString("InvalidPropertiesExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x001620A8 File Offset: 0x001602A8
		internal static string InvalidComplexPropertiesExpression(object p0)
		{
			return EntityRes.GetString("InvalidComplexPropertiesExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x001620CC File Offset: 0x001602CC
		internal static string DuplicateStructuralTypeConfiguration(object p0)
		{
			return EntityRes.GetString("DuplicateStructuralTypeConfiguration", new object[]
			{
				p0
			});
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x001620F0 File Offset: 0x001602F0
		internal static string ConflictingPropertyConfiguration(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingPropertyConfiguration", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0016211C File Offset: 0x0016031C
		internal static string ConflictingTypeAnnotation(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ConflictingTypeAnnotation", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0016214C File Offset: 0x0016034C
		internal static string ConflictingColumnConfiguration(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingColumnConfiguration", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x00162178 File Offset: 0x00160378
		internal static string ConflictingConfigurationValue(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ConflictingConfigurationValue", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x001621A8 File Offset: 0x001603A8
		internal static string ConflictingAnnotationValue(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingAnnotationValue", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x001621D4 File Offset: 0x001603D4
		internal static string ConflictingIndexAttributeProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingIndexAttributeProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x00162200 File Offset: 0x00160400
		internal static string ConflictingIndexAttribute(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingIndexAttribute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x00162228 File Offset: 0x00160428
		internal static string ConflictingIndexAttributesOnProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ConflictingIndexAttributesOnProperty", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00162258 File Offset: 0x00160458
		internal static string IncompatibleTypes(object p0, object p1)
		{
			return EntityRes.GetString("IncompatibleTypes", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00162280 File Offset: 0x00160480
		internal static string AnnotationSerializeWrongType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AnnotationSerializeWrongType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x001622AC File Offset: 0x001604AC
		internal static string AnnotationSerializeBadFormat(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AnnotationSerializeBadFormat", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x001622D8 File Offset: 0x001604D8
		internal static string ConflictWhenConsolidating(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictWhenConsolidating", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x00162304 File Offset: 0x00160504
		internal static string OrderConflictWhenConsolidating(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("OrderConflictWhenConsolidating", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00162338 File Offset: 0x00160538
		internal static string CodeFirstInvalidComplexType(object p0)
		{
			return EntityRes.GetString("CodeFirstInvalidComplexType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0016235C File Offset: 0x0016055C
		internal static string InvalidEntityType(object p0)
		{
			return EntityRes.GetString("InvalidEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x00162380 File Offset: 0x00160580
		internal static string SimpleNameCollision(object p0, object p1, object p2)
		{
			return EntityRes.GetString("SimpleNameCollision", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x001623AC File Offset: 0x001605AC
		internal static string NavigationInverseItself(object p0, object p1)
		{
			return EntityRes.GetString("NavigationInverseItself", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x001623D4 File Offset: 0x001605D4
		internal static string ConflictingConstraint(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingConstraint", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x001623FC File Offset: 0x001605FC
		internal static string ConflictingInferredColumnType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingInferredColumnType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00162428 File Offset: 0x00160628
		internal static string ConflictingMapping(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingMapping", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00162450 File Offset: 0x00160650
		internal static string ConflictingCascadeDeleteOperation(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingCascadeDeleteOperation", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x00162478 File Offset: 0x00160678
		internal static string ConflictingMultiplicities(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingMultiplicities", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x001624A0 File Offset: 0x001606A0
		internal static string MaxLengthAttributeConvention_InvalidMaxLength(object p0, object p1)
		{
			return EntityRes.GetString("MaxLengthAttributeConvention_InvalidMaxLength", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x001624C8 File Offset: 0x001606C8
		internal static string StringLengthAttributeConvention_InvalidMaximumLength(object p0, object p1)
		{
			return EntityRes.GetString("StringLengthAttributeConvention_InvalidMaximumLength", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x001624F0 File Offset: 0x001606F0
		internal static string ModelGeneration_UnableToDetermineKeyOrder(object p0)
		{
			return EntityRes.GetString("ModelGeneration_UnableToDetermineKeyOrder", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x00162514 File Offset: 0x00160714
		internal static string ForeignKeyAttributeConvention_EmptyKey(object p0, object p1)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_EmptyKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0016253C File Offset: 0x0016073C
		internal static string ForeignKeyAttributeConvention_InvalidKey(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_InvalidKey", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x0016256C File Offset: 0x0016076C
		internal static string ForeignKeyAttributeConvention_InvalidNavigationProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_InvalidNavigationProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x00162598 File Offset: 0x00160798
		internal static string ForeignKeyAttributeConvention_OrderRequired(object p0)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_OrderRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x001625BC File Offset: 0x001607BC
		internal static string InversePropertyAttributeConvention_PropertyNotFound(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InversePropertyAttributeConvention_PropertyNotFound", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x001625EC File Offset: 0x001607EC
		internal static string InversePropertyAttributeConvention_SelfInverseDetected(object p0, object p1)
		{
			return EntityRes.GetString("InversePropertyAttributeConvention_SelfInverseDetected", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004BB8 RID: 19384 RVA: 0x00162613 File Offset: 0x00160813
		internal static string ValidationHeader
		{
			get
			{
				return EntityRes.GetString("ValidationHeader");
			}
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x00162620 File Offset: 0x00160820
		internal static string ValidationItemFormat(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ValidationItemFormat", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0016264C File Offset: 0x0016084C
		internal static string KeyRegisteredOnDerivedType(object p0, object p1)
		{
			return EntityRes.GetString("KeyRegisteredOnDerivedType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x00162674 File Offset: 0x00160874
		internal static string InvalidTableMapping(object p0, object p1)
		{
			return EntityRes.GetString("InvalidTableMapping", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0016269C File Offset: 0x0016089C
		internal static string InvalidTableMapping_NoTableName(object p0)
		{
			return EntityRes.GetString("InvalidTableMapping_NoTableName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x001626C0 File Offset: 0x001608C0
		internal static string InvalidChainedMappingSyntax(object p0)
		{
			return EntityRes.GetString("InvalidChainedMappingSyntax", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x001626E4 File Offset: 0x001608E4
		internal static string InvalidNotNullCondition(object p0, object p1)
		{
			return EntityRes.GetString("InvalidNotNullCondition", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x0016270C File Offset: 0x0016090C
		internal static string InvalidDiscriminatorType(object p0)
		{
			return EntityRes.GetString("InvalidDiscriminatorType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x00162730 File Offset: 0x00160930
		internal static string ConventionNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ConventionNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x00162758 File Offset: 0x00160958
		internal static string InvalidEntitySplittingProperties(object p0)
		{
			return EntityRes.GetString("InvalidEntitySplittingProperties", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0016277C File Offset: 0x0016097C
		internal static string ProviderNameNotFound(object p0)
		{
			return EntityRes.GetString("ProviderNameNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x001627A0 File Offset: 0x001609A0
		internal static string ProviderNotFound(object p0)
		{
			return EntityRes.GetString("ProviderNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x001627C4 File Offset: 0x001609C4
		internal static string InvalidDatabaseName(object p0)
		{
			return EntityRes.GetString("InvalidDatabaseName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x001627E8 File Offset: 0x001609E8
		internal static string EntityMappingConfiguration_DuplicateMapInheritedProperties(object p0)
		{
			return EntityRes.GetString("EntityMappingConfiguration_DuplicateMapInheritedProperties", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0016280C File Offset: 0x00160A0C
		internal static string EntityMappingConfiguration_DuplicateMappedProperties(object p0)
		{
			return EntityRes.GetString("EntityMappingConfiguration_DuplicateMappedProperties", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x00162830 File Offset: 0x00160A30
		internal static string EntityMappingConfiguration_DuplicateMappedProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityMappingConfiguration_DuplicateMappedProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x00162858 File Offset: 0x00160A58
		internal static string EntityMappingConfiguration_CannotMapIgnoredProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityMappingConfiguration_CannotMapIgnoredProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x00162880 File Offset: 0x00160A80
		internal static string EntityMappingConfiguration_InvalidTableSharing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityMappingConfiguration_InvalidTableSharing", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x001628AC File Offset: 0x00160AAC
		internal static string EntityMappingConfiguration_TPCWithIAsOnNonLeafType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityMappingConfiguration_TPCWithIAsOnNonLeafType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x001628D8 File Offset: 0x00160AD8
		internal static string CannotIgnoreMappedBaseProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("CannotIgnoreMappedBaseProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x00162904 File Offset: 0x00160B04
		internal static string ModelBuilder_KeyPropertiesMustBePrimitive(object p0, object p1)
		{
			return EntityRes.GetString("ModelBuilder_KeyPropertiesMustBePrimitive", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0016292C File Offset: 0x00160B2C
		internal static string TableNotFound(object p0)
		{
			return EntityRes.GetString("TableNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x00162950 File Offset: 0x00160B50
		internal static string IncorrectColumnCount(object p0)
		{
			return EntityRes.GetString("IncorrectColumnCount", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x00162974 File Offset: 0x00160B74
		internal static string BadKeyNameForAnnotation(object p0, object p1)
		{
			return EntityRes.GetString("BadKeyNameForAnnotation", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0016299C File Offset: 0x00160B9C
		internal static string BadAnnotationName(object p0)
		{
			return EntityRes.GetString("BadAnnotationName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x001629BF File Offset: 0x00160BBF
		internal static string CircularComplexTypeHierarchy
		{
			get
			{
				return EntityRes.GetString("CircularComplexTypeHierarchy");
			}
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x001629CC File Offset: 0x00160BCC
		internal static string UnableToDeterminePrincipal(object p0, object p1)
		{
			return EntityRes.GetString("UnableToDeterminePrincipal", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x001629F4 File Offset: 0x00160BF4
		internal static string UnmappedAbstractType(object p0)
		{
			return EntityRes.GetString("UnmappedAbstractType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x00162A18 File Offset: 0x00160C18
		internal static string UnsupportedHybridInheritanceMapping(object p0)
		{
			return EntityRes.GetString("UnsupportedHybridInheritanceMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00162A3C File Offset: 0x00160C3C
		internal static string OrphanedConfiguredTableDetected(object p0)
		{
			return EntityRes.GetString("OrphanedConfiguredTableDetected", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00162A60 File Offset: 0x00160C60
		internal static string BadTphMappingToSharedColumn(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return EntityRes.GetString("BadTphMappingToSharedColumn", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5,
				p6
			});
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00162AA0 File Offset: 0x00160CA0
		internal static string DuplicateConfiguredColumnOrder(object p0)
		{
			return EntityRes.GetString("DuplicateConfiguredColumnOrder", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x00162AC4 File Offset: 0x00160CC4
		internal static string UnsupportedUseOfV3Type(object p0, object p1)
		{
			return EntityRes.GetString("UnsupportedUseOfV3Type", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x00162AEC File Offset: 0x00160CEC
		internal static string MultiplePropertiesMatchedAsKeys(object p0, object p1)
		{
			return EntityRes.GetString("MultiplePropertiesMatchedAsKeys", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06004BDA RID: 19418 RVA: 0x00162B13 File Offset: 0x00160D13
		internal static string FailedToGetProviderInformation
		{
			get
			{
				return EntityRes.GetString("FailedToGetProviderInformation");
			}
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x00162B20 File Offset: 0x00160D20
		internal static string DbPropertyEntry_CannotGetCurrentValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyEntry_CannotGetCurrentValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x00162B48 File Offset: 0x00160D48
		internal static string DbPropertyEntry_CannotSetCurrentValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyEntry_CannotSetCurrentValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x00162B70 File Offset: 0x00160D70
		internal static string DbPropertyEntry_NotSupportedForDetached(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyEntry_NotSupportedForDetached", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x00162B9C File Offset: 0x00160D9C
		internal static string DbPropertyEntry_SettingEntityRefNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyEntry_SettingEntityRefNotSupported", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x00162BC8 File Offset: 0x00160DC8
		internal static string DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyEntry_NotSupportedForPropertiesNotInTheModel", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x00162BF4 File Offset: 0x00160DF4
		internal static string DbEntityEntry_NotSupportedForDetached(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotSupportedForDetached", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x00162C1C File Offset: 0x00160E1C
		internal static string DbSet_BadTypeForAddAttachRemove(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbSet_BadTypeForAddAttachRemove", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x00162C48 File Offset: 0x00160E48
		internal static string DbSet_BadTypeForCreate(object p0, object p1)
		{
			return EntityRes.GetString("DbSet_BadTypeForCreate", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x00162C70 File Offset: 0x00160E70
		internal static string DbEntity_BadTypeForCast(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbEntity_BadTypeForCast", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x00162C9C File Offset: 0x00160E9C
		internal static string DbMember_BadTypeForCast(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("DbMember_BadTypeForCast", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x00162CD0 File Offset: 0x00160ED0
		internal static string DbEntityEntry_UsedReferenceForCollectionProp(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_UsedReferenceForCollectionProp", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x00162CF8 File Offset: 0x00160EF8
		internal static string DbEntityEntry_UsedCollectionForReferenceProp(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_UsedCollectionForReferenceProp", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00162D20 File Offset: 0x00160F20
		internal static string DbEntityEntry_NotANavigationProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotANavigationProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x00162D48 File Offset: 0x00160F48
		internal static string DbEntityEntry_NotAScalarProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotAScalarProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00162D70 File Offset: 0x00160F70
		internal static string DbEntityEntry_NotAComplexProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotAComplexProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00162D98 File Offset: 0x00160F98
		internal static string DbEntityEntry_NotAProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotAProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x00162DC0 File Offset: 0x00160FC0
		internal static string DbEntityEntry_DottedPartNotComplex(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbEntityEntry_DottedPartNotComplex", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x00162DEC File Offset: 0x00160FEC
		internal static string DbEntityEntry_DottedPathMustBeProperty(object p0)
		{
			return EntityRes.GetString("DbEntityEntry_DottedPathMustBeProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00162E10 File Offset: 0x00161010
		internal static string DbEntityEntry_WrongGenericForNavProp(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbEntityEntry_WrongGenericForNavProp", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00162E40 File Offset: 0x00161040
		internal static string DbEntityEntry_WrongGenericForCollectionNavProp(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbEntityEntry_WrongGenericForCollectionNavProp", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x00162E70 File Offset: 0x00161070
		internal static string DbEntityEntry_WrongGenericForProp(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbEntityEntry_WrongGenericForProp", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x00162EA0 File Offset: 0x001610A0
		internal static string DbEntityEntry_BadPropertyExpression(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_BadPropertyExpression", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x00162EC7 File Offset: 0x001610C7
		internal static string DbContext_IndependentAssociationUpdateException
		{
			get
			{
				return EntityRes.GetString("DbContext_IndependentAssociationUpdateException");
			}
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x00162ED4 File Offset: 0x001610D4
		internal static string DbPropertyValues_CannotGetValuesForState(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotGetValuesForState", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00162EFC File Offset: 0x001610FC
		internal static string DbPropertyValues_CannotSetNullValue(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyValues_CannotSetNullValue", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x00162F28 File Offset: 0x00161128
		internal static string DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x00162F50 File Offset: 0x00161150
		internal static string DbPropertyValues_WrongTypeForAssignment(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbPropertyValues_WrongTypeForAssignment", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x00162F7F File Offset: 0x0016117F
		internal static string DbPropertyValues_PropertyValueNamesAreReadonly
		{
			get
			{
				return EntityRes.GetString("DbPropertyValues_PropertyValueNamesAreReadonly");
			}
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x00162F8C File Offset: 0x0016118C
		internal static string DbPropertyValues_PropertyDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_PropertyDoesNotExist", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x00162FB4 File Offset: 0x001611B4
		internal static string DbPropertyValues_AttemptToSetValuesFromWrongObject(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_AttemptToSetValuesFromWrongObject", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x00162FDC File Offset: 0x001611DC
		internal static string DbPropertyValues_AttemptToSetValuesFromWrongType(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_AttemptToSetValuesFromWrongType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06004BFA RID: 19450 RVA: 0x00163003 File Offset: 0x00161203
		internal static string DbPropertyValues_AttemptToSetNonValuesOnComplexProperty
		{
			get
			{
				return EntityRes.GetString("DbPropertyValues_AttemptToSetNonValuesOnComplexProperty");
			}
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x00163010 File Offset: 0x00161210
		internal static string DbPropertyValues_ComplexObjectCannotBeNull(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_ComplexObjectCannotBeNull", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x00163038 File Offset: 0x00161238
		internal static string DbPropertyValues_NestedPropertyValuesNull(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_NestedPropertyValuesNull", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x00163060 File Offset: 0x00161260
		internal static string DbPropertyValues_CannotSetPropertyOnNullCurrentValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotSetPropertyOnNullCurrentValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x00163088 File Offset: 0x00161288
		internal static string DbPropertyValues_CannotSetPropertyOnNullOriginalValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotSetPropertyOnNullOriginalValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x001630B0 File Offset: 0x001612B0
		internal static string DatabaseInitializationStrategy_ModelMismatch(object p0)
		{
			return EntityRes.GetString("DatabaseInitializationStrategy_ModelMismatch", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x001630D4 File Offset: 0x001612D4
		internal static string Database_DatabaseAlreadyExists(object p0)
		{
			return EntityRes.GetString("Database_DatabaseAlreadyExists", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x001630F7 File Offset: 0x001612F7
		internal static string Database_NonCodeFirstCompatibilityCheck
		{
			get
			{
				return EntityRes.GetString("Database_NonCodeFirstCompatibilityCheck");
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06004C02 RID: 19458 RVA: 0x00163103 File Offset: 0x00161303
		internal static string Database_NoDatabaseMetadata
		{
			get
			{
				return EntityRes.GetString("Database_NoDatabaseMetadata");
			}
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x00163110 File Offset: 0x00161310
		internal static string Database_BadLegacyInitializerEntry(object p0, object p1)
		{
			return EntityRes.GetString("Database_BadLegacyInitializerEntry", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x00163138 File Offset: 0x00161338
		internal static string Database_InitializeFromLegacyConfigFailed(object p0, object p1)
		{
			return EntityRes.GetString("Database_InitializeFromLegacyConfigFailed", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00163160 File Offset: 0x00161360
		internal static string Database_InitializeFromConfigFailed(object p0, object p1)
		{
			return EntityRes.GetString("Database_InitializeFromConfigFailed", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00163188 File Offset: 0x00161388
		internal static string ContextConfiguredMultipleTimes(object p0)
		{
			return EntityRes.GetString("ContextConfiguredMultipleTimes", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x001631AC File Offset: 0x001613AC
		internal static string SetConnectionFactoryFromConfigFailed(object p0)
		{
			return EntityRes.GetString("SetConnectionFactoryFromConfigFailed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004C08 RID: 19464 RVA: 0x001631CF File Offset: 0x001613CF
		internal static string DbContext_ContextUsedInModelCreating
		{
			get
			{
				return EntityRes.GetString("DbContext_ContextUsedInModelCreating");
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x001631DB File Offset: 0x001613DB
		internal static string DbContext_MESTNotSupported
		{
			get
			{
				return EntityRes.GetString("DbContext_MESTNotSupported");
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004C0A RID: 19466 RVA: 0x001631E7 File Offset: 0x001613E7
		internal static string DbContext_Disposed
		{
			get
			{
				return EntityRes.GetString("DbContext_Disposed");
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x001631F3 File Offset: 0x001613F3
		internal static string DbContext_ProviderReturnedNullConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_ProviderReturnedNullConnection");
			}
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00163200 File Offset: 0x00161400
		internal static string DbContext_ProviderNameMissing(object p0)
		{
			return EntityRes.GetString("DbContext_ProviderNameMissing", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x00163223 File Offset: 0x00161423
		internal static string DbContext_ConnectionFactoryReturnedNullConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_ConnectionFactoryReturnedNullConnection");
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x0016322F File Offset: 0x0016142F
		internal static string DbSet_WrongNumberOfKeyValuesPassed
		{
			get
			{
				return EntityRes.GetString("DbSet_WrongNumberOfKeyValuesPassed");
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x0016323B File Offset: 0x0016143B
		internal static string DbSet_WrongKeyValueType
		{
			get
			{
				return EntityRes.GetString("DbSet_WrongKeyValueType");
			}
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x00163248 File Offset: 0x00161448
		internal static string DbSet_WrongEntityTypeFound(object p0, object p1)
		{
			return EntityRes.GetString("DbSet_WrongEntityTypeFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x0016326F File Offset: 0x0016146F
		internal static string DbSet_MultipleAddedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("DbSet_MultipleAddedEntitiesFound");
			}
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x0016327C File Offset: 0x0016147C
		internal static string DbSet_DbSetUsedWithComplexType(object p0)
		{
			return EntityRes.GetString("DbSet_DbSetUsedWithComplexType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x001632A0 File Offset: 0x001614A0
		internal static string DbSet_PocoAndNonPocoMixedInSameAssembly(object p0)
		{
			return EntityRes.GetString("DbSet_PocoAndNonPocoMixedInSameAssembly", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x001632C4 File Offset: 0x001614C4
		internal static string DbSet_EntityTypeNotInModel(object p0)
		{
			return EntityRes.GetString("DbSet_EntityTypeNotInModel", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004C15 RID: 19477 RVA: 0x001632E7 File Offset: 0x001614E7
		internal static string DbQuery_BindingToDbQueryNotSupported
		{
			get
			{
				return EntityRes.GetString("DbQuery_BindingToDbQueryNotSupported");
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x001632F3 File Offset: 0x001614F3
		internal static string DbExtensions_InvalidIncludePathExpression
		{
			get
			{
				return EntityRes.GetString("DbExtensions_InvalidIncludePathExpression");
			}
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x00163300 File Offset: 0x00161500
		internal static string DbContext_ConnectionStringNotFound(object p0)
		{
			return EntityRes.GetString("DbContext_ConnectionStringNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x00163323 File Offset: 0x00161523
		internal static string DbContext_ConnectionHasModel
		{
			get
			{
				return EntityRes.GetString("DbContext_ConnectionHasModel");
			}
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x00163330 File Offset: 0x00161530
		internal static string DbCollectionEntry_CannotSetCollectionProp(object p0, object p1)
		{
			return EntityRes.GetString("DbCollectionEntry_CannotSetCollectionProp", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x00163357 File Offset: 0x00161557
		internal static string CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported
		{
			get
			{
				return EntityRes.GetString("CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported");
			}
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x00163364 File Offset: 0x00161564
		internal static string Mapping_MESTNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_MESTNotSupported", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x00163390 File Offset: 0x00161590
		internal static string DbModelBuilder_MissingRequiredCtor(object p0)
		{
			return EntityRes.GetString("DbModelBuilder_MissingRequiredCtor", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x001633B3 File Offset: 0x001615B3
		internal static string DbEntityValidationException_ValidationFailed
		{
			get
			{
				return EntityRes.GetString("DbEntityValidationException_ValidationFailed");
			}
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x001633C0 File Offset: 0x001615C0
		internal static string DbUnexpectedValidationException_ValidationAttribute(object p0, object p1)
		{
			return EntityRes.GetString("DbUnexpectedValidationException_ValidationAttribute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x001633E8 File Offset: 0x001615E8
		internal static string DbUnexpectedValidationException_IValidatableObject(object p0, object p1)
		{
			return EntityRes.GetString("DbUnexpectedValidationException_IValidatableObject", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x00163410 File Offset: 0x00161610
		internal static string SqlConnectionFactory_MdfNotSupported(object p0)
		{
			return EntityRes.GetString("SqlConnectionFactory_MdfNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x00163433 File Offset: 0x00161633
		internal static string Database_InitializationException
		{
			get
			{
				return EntityRes.GetString("Database_InitializationException");
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06004C22 RID: 19490 RVA: 0x0016343F File Offset: 0x0016163F
		internal static string EdmxWriter_EdmxFromObjectContextNotSupported
		{
			get
			{
				return EntityRes.GetString("EdmxWriter_EdmxFromObjectContextNotSupported");
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x0016344B File Offset: 0x0016164B
		internal static string EdmxWriter_EdmxFromModelFirstNotSupported
		{
			get
			{
				return EntityRes.GetString("EdmxWriter_EdmxFromModelFirstNotSupported");
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06004C24 RID: 19492 RVA: 0x00163457 File Offset: 0x00161657
		internal static string UnintentionalCodeFirstException_Message
		{
			get
			{
				return EntityRes.GetString("UnintentionalCodeFirstException_Message");
			}
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x00163464 File Offset: 0x00161664
		internal static string DbContextServices_MissingDefaultCtor(object p0)
		{
			return EntityRes.GetString("DbContextServices_MissingDefaultCtor", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06004C26 RID: 19494 RVA: 0x00163487 File Offset: 0x00161687
		internal static string CannotCallGenericSetWithProxyType
		{
			get
			{
				return EntityRes.GetString("CannotCallGenericSetWithProxyType");
			}
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x00163494 File Offset: 0x00161694
		internal static string EdmModel_Validator_Semantic_SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SystemNamespaceEncountered", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x001634B8 File Offset: 0x001616B8
		internal static string EdmModel_Validator_Semantic_SimilarRelationshipEnd(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SimilarRelationshipEnd", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x001634EC File Offset: 0x001616EC
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetNameReference(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetNameReference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x00163514 File Offset: 0x00161714
		internal static string EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x00163540 File Offset: 0x00161740
		internal static string EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x00163568 File Offset: 0x00161768
		internal static string EdmModel_Validator_Semantic_DuplicateEndName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEndName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0016358C File Offset: 0x0016178C
		internal static string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x001635B4 File Offset: 0x001617B4
		internal static string EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x001635D8 File Offset: 0x001617D8
		internal static string EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x001635FC File Offset: 0x001617FC
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypeAbstract", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x00163620 File Offset: 0x00161820
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x00163644 File Offset: 0x00161844
		internal static string EdmModel_Validator_Semantic_InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyNullablePart", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0016366C File Offset: 0x0016186C
		internal static string EdmModel_Validator_Semantic_EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityKeyMustBeScalar", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x00163694 File Offset: 0x00161894
		internal static string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x001636BC File Offset: 0x001618BC
		internal static string EdmModel_Validator_Semantic_KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyMissingOnEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x001636E0 File Offset: 0x001618E0
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x0016370B File Offset: 0x0016190B
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame");
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x00163717 File Offset: 0x00161917
		internal static string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x00163724 File Offset: 0x00161924
		internal static string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x0016374C File Offset: 0x0016194C
		internal static string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x00163770 File Offset: 0x00161970
		internal static string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x00163794 File Offset: 0x00161994
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x001637BC File Offset: 0x001619BC
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x001637E4 File Offset: 0x001619E4
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0016380C File Offset: 0x00161A0C
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x00163834 File Offset: 0x00161A34
		internal static string EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x00163860 File Offset: 0x00161A60
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x00163888 File Offset: 0x00161A88
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06004C43 RID: 19523 RVA: 0x001638AF File Offset: 0x00161AAF
		internal static string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x001638BC File Offset: 0x00161ABC
		internal static string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x001638F0 File Offset: 0x00161AF0
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x00163918 File Offset: 0x00161B18
		internal static string EdmModel_Validator_Semantic_NullableComplexType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NullableComplexType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0016393C File Offset: 0x00161B3C
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x00163960 File Offset: 0x00161B60
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x00163984 File Offset: 0x00161B84
		internal static string EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x001639A8 File Offset: 0x00161BA8
		internal static string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x001639D0 File Offset: 0x00161BD0
		internal static string EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x001639F4 File Offset: 0x00161BF4
		internal static string EdmModel_Validator_Semantic_CycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CycleInTypeHierarchy", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x00163A18 File Offset: 0x00161C18
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType_V1_1(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType_V1_1", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x00163A3C File Offset: 0x00161C3C
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType_V3(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType_V3", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06004C4F RID: 19535 RVA: 0x00163A5F File Offset: 0x00161C5F
		internal static string EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion");
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06004C50 RID: 19536 RVA: 0x00163A6B File Offset: 0x00161C6B
		internal static string EdmModel_Validator_Syntactic_MissingName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingName");
			}
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x00163A78 File Offset: 0x00161C78
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x00163A9C File Offset: 0x00161C9C
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06004C53 RID: 19539 RVA: 0x00163ABF File Offset: 0x00161CBF
		internal static string EdmModel_Validator_Syntactic_EdmAssociationType_AssocationEndMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationType_AssocationEndMustNotBeNull");
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06004C54 RID: 19540 RVA: 0x00163ACB File Offset: 0x00161CCB
		internal static string EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull");
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06004C55 RID: 19541 RVA: 0x00163AD7 File Offset: 0x00161CD7
		internal static string EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty");
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06004C56 RID: 19542 RVA: 0x00163AE3 File Offset: 0x00161CE3
		internal static string EdmModel_Validator_Syntactic_EdmNavigationProperty_AssocationMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmNavigationProperty_AssocationMustNotBeNull");
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x00163AEF File Offset: 0x00161CEF
		internal static string EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull");
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06004C58 RID: 19544 RVA: 0x00163AFB File Offset: 0x00161CFB
		internal static string EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull");
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06004C59 RID: 19545 RVA: 0x00163B07 File Offset: 0x00161D07
		internal static string EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull");
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06004C5A RID: 19546 RVA: 0x00163B13 File Offset: 0x00161D13
		internal static string EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull");
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06004C5B RID: 19547 RVA: 0x00163B1F File Offset: 0x00161D1F
		internal static string EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull");
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06004C5C RID: 19548 RVA: 0x00163B2B File Offset: 0x00161D2B
		internal static string EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull");
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06004C5D RID: 19549 RVA: 0x00163B37 File Offset: 0x00161D37
		internal static string EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid");
			}
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x00163B44 File Offset: 0x00161D44
		internal static string MetadataItem_InvalidDataSpace(object p0, object p1)
		{
			return EntityRes.GetString("MetadataItem_InvalidDataSpace", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06004C5F RID: 19551 RVA: 0x00163B6B File Offset: 0x00161D6B
		internal static string EdmModel_AddItem_NonMatchingNamespace
		{
			get
			{
				return EntityRes.GetString("EdmModel_AddItem_NonMatchingNamespace");
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06004C60 RID: 19552 RVA: 0x00163B77 File Offset: 0x00161D77
		internal static string Serializer_OneNamespaceAndOneContainer
		{
			get
			{
				return EntityRes.GetString("Serializer_OneNamespaceAndOneContainer");
			}
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x00163B84 File Offset: 0x00161D84
		internal static string MaxLengthAttribute_ValidationError(object p0, object p1)
		{
			return EntityRes.GetString("MaxLengthAttribute_ValidationError", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06004C62 RID: 19554 RVA: 0x00163BAB File Offset: 0x00161DAB
		internal static string MaxLengthAttribute_InvalidMaxLength
		{
			get
			{
				return EntityRes.GetString("MaxLengthAttribute_InvalidMaxLength");
			}
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x00163BB8 File Offset: 0x00161DB8
		internal static string MinLengthAttribute_ValidationError(object p0, object p1)
		{
			return EntityRes.GetString("MinLengthAttribute_ValidationError", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x00163BDF File Offset: 0x00161DDF
		internal static string MinLengthAttribute_InvalidMinLength
		{
			get
			{
				return EntityRes.GetString("MinLengthAttribute_InvalidMinLength");
			}
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x00163BEC File Offset: 0x00161DEC
		internal static string DbConnectionInfo_ConnectionStringNotFound(object p0)
		{
			return EntityRes.GetString("DbConnectionInfo_ConnectionStringNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06004C66 RID: 19558 RVA: 0x00163C0F File Offset: 0x00161E0F
		internal static string EagerInternalContext_CannotSetConnectionInfo
		{
			get
			{
				return EntityRes.GetString("EagerInternalContext_CannotSetConnectionInfo");
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06004C67 RID: 19559 RVA: 0x00163C1B File Offset: 0x00161E1B
		internal static string LazyInternalContext_CannotReplaceEfConnectionWithDbConnection
		{
			get
			{
				return EntityRes.GetString("LazyInternalContext_CannotReplaceEfConnectionWithDbConnection");
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06004C68 RID: 19560 RVA: 0x00163C27 File Offset: 0x00161E27
		internal static string LazyInternalContext_CannotReplaceDbConnectionWithEfConnection
		{
			get
			{
				return EntityRes.GetString("LazyInternalContext_CannotReplaceDbConnectionWithEfConnection");
			}
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x00163C34 File Offset: 0x00161E34
		internal static string EntityKey_EntitySetDoesNotMatch(object p0)
		{
			return EntityRes.GetString("EntityKey_EntitySetDoesNotMatch", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x00163C58 File Offset: 0x00161E58
		internal static string EntityKey_IncorrectNumberOfKeyValuePairs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectNumberOfKeyValuePairs", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x00163C84 File Offset: 0x00161E84
		internal static string EntityKey_IncorrectValueType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectValueType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x00163CB0 File Offset: 0x00161EB0
		internal static string EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x00163CD8 File Offset: 0x00161ED8
		internal static string EntityKey_MissingKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_MissingKeyValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06004C6E RID: 19566 RVA: 0x00163CFF File Offset: 0x00161EFF
		internal static string EntityKey_NoNullsAllowedInKeyValuePairs
		{
			get
			{
				return EntityRes.GetString("EntityKey_NoNullsAllowedInKeyValuePairs");
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06004C6F RID: 19567 RVA: 0x00163D0B File Offset: 0x00161F0B
		internal static string EntityKey_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("EntityKey_UnexpectedNull");
			}
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x00163D18 File Offset: 0x00161F18
		internal static string EntityKey_DoesntMatchKeyOnEntity(object p0)
		{
			return EntityRes.GetString("EntityKey_DoesntMatchKeyOnEntity", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06004C71 RID: 19569 RVA: 0x00163D3B File Offset: 0x00161F3B
		internal static string EntityKey_EntityKeyMustHaveValues
		{
			get
			{
				return EntityRes.GetString("EntityKey_EntityKeyMustHaveValues");
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06004C72 RID: 19570 RVA: 0x00163D47 File Offset: 0x00161F47
		internal static string EntityKey_InvalidQualifiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_InvalidQualifiedEntitySetName");
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06004C73 RID: 19571 RVA: 0x00163D53 File Offset: 0x00161F53
		internal static string EntityKey_MissingEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_MissingEntitySetName");
			}
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x00163D60 File Offset: 0x00161F60
		internal static string EntityKey_InvalidName(object p0)
		{
			return EntityRes.GetString("EntityKey_InvalidName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x00163D83 File Offset: 0x00161F83
		internal static string EntityKey_CannotChangeKey
		{
			get
			{
				return EntityRes.GetString("EntityKey_CannotChangeKey");
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x00163D8F File Offset: 0x00161F8F
		internal static string EntityTypesDoNotAgree
		{
			get
			{
				return EntityRes.GetString("EntityTypesDoNotAgree");
			}
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x00163D9C File Offset: 0x00161F9C
		internal static string EntityKey_NullKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_NullKeyValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x00163DC3 File Offset: 0x00161FC3
		internal static string EdmMembersDefiningTypeDoNotAgreeWithMetadataType
		{
			get
			{
				return EntityRes.GetString("EdmMembersDefiningTypeDoNotAgreeWithMetadataType");
			}
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x00163DD0 File Offset: 0x00161FD0
		internal static string CannotCallNoncomposableFunction(object p0)
		{
			return EntityRes.GetString("CannotCallNoncomposableFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x00163DF4 File Offset: 0x00161FF4
		internal static string EntityClient_ConnectionStringMissingInfo(object p0)
		{
			return EntityRes.GetString("EntityClient_ConnectionStringMissingInfo", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x00163E17 File Offset: 0x00162017
		internal static string EntityClient_ValueNotString
		{
			get
			{
				return EntityRes.GetString("EntityClient_ValueNotString");
			}
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x00163E24 File Offset: 0x00162024
		internal static string EntityClient_KeywordNotSupported(object p0)
		{
			return EntityRes.GetString("EntityClient_KeywordNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x00163E47 File Offset: 0x00162047
		internal static string EntityClient_NoCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoCommandText");
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x00163E53 File Offset: 0x00162053
		internal static string EntityClient_ConnectionStringNeededBeforeOperation
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStringNeededBeforeOperation");
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x00163E5F File Offset: 0x0016205F
		internal static string EntityClient_ConnectionNotOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionNotOpen");
			}
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x00163E6C File Offset: 0x0016206C
		internal static string EntityClient_DuplicateParameterNames(object p0)
		{
			return EntityRes.GetString("EntityClient_DuplicateParameterNames", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x00163E8F File Offset: 0x0016208F
		internal static string EntityClient_NoConnectionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForCommand");
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x00163E9B File Offset: 0x0016209B
		internal static string EntityClient_NoConnectionForAdapter
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForAdapter");
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x00163EA7 File Offset: 0x001620A7
		internal static string EntityClient_ClosedConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_ClosedConnectionForUpdate");
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004C84 RID: 19588 RVA: 0x00163EB3 File Offset: 0x001620B3
		internal static string EntityClient_InvalidNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidNamedConnection");
			}
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x00163EC0 File Offset: 0x001620C0
		internal static string EntityClient_NestedNamedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_NestedNamedConnection", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x00163EE4 File Offset: 0x001620E4
		internal static string EntityClient_InvalidStoreProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidStoreProvider", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x00163F07 File Offset: 0x00162107
		internal static string EntityClient_DataReaderIsStillOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_DataReaderIsStillOpen");
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x00163F13 File Offset: 0x00162113
		internal static string EntityClient_SettingsCannotBeChangedOnOpenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_SettingsCannotBeChangedOnOpenConnection");
			}
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x00163F20 File Offset: 0x00162120
		internal static string EntityClient_ExecutingOnClosedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_ExecutingOnClosedConnection", new object[]
			{
				p0
			});
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004C8A RID: 19594 RVA: 0x00163F43 File Offset: 0x00162143
		internal static string EntityClient_ConnectionStateClosed
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateClosed");
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x00163F4F File Offset: 0x0016214F
		internal static string EntityClient_ConnectionStateBroken
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateBroken");
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06004C8C RID: 19596 RVA: 0x00163F5B File Offset: 0x0016215B
		internal static string EntityClient_CannotCloneStoreProvider
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotCloneStoreProvider");
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x00163F67 File Offset: 0x00162167
		internal static string EntityClient_UnsupportedCommandType
		{
			get
			{
				return EntityRes.GetString("EntityClient_UnsupportedCommandType");
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x00163F73 File Offset: 0x00162173
		internal static string EntityClient_ErrorInClosingConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInClosingConnection");
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x00163F7F File Offset: 0x0016217F
		internal static string EntityClient_ErrorInBeginningTransaction
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInBeginningTransaction");
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x00163F8B File Offset: 0x0016218B
		internal static string EntityClient_ExtraParametersWithNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ExtraParametersWithNamedConnection");
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x00163F97 File Offset: 0x00162197
		internal static string EntityClient_CommandDefinitionPreparationFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionPreparationFailed");
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004C92 RID: 19602 RVA: 0x00163FA3 File Offset: 0x001621A3
		internal static string EntityClient_CommandDefinitionExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionExecutionFailed");
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06004C93 RID: 19603 RVA: 0x00163FAF File Offset: 0x001621AF
		internal static string EntityClient_CommandExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandExecutionFailed");
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x00163FBB File Offset: 0x001621BB
		internal static string EntityClient_StoreReaderFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_StoreReaderFailed");
			}
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x00163FC8 File Offset: 0x001621C8
		internal static string EntityClient_FailedToGetInformation(object p0)
		{
			return EntityRes.GetString("EntityClient_FailedToGetInformation", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004C96 RID: 19606 RVA: 0x00163FEB File Offset: 0x001621EB
		internal static string EntityClient_TooFewColumns
		{
			get
			{
				return EntityRes.GetString("EntityClient_TooFewColumns");
			}
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x00163FF8 File Offset: 0x001621F8
		internal static string EntityClient_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x0016401B File Offset: 0x0016221B
		internal static string EntityClient_EmptyParameterName
		{
			get
			{
				return EntityRes.GetString("EntityClient_EmptyParameterName");
			}
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x00164028 File Offset: 0x00162228
		internal static string EntityClient_ReturnedNullOnProviderMethod(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_ReturnedNullOnProviderMethod", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x0016404F File Offset: 0x0016224F
		internal static string EntityClient_CannotDeduceDbType
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotDeduceDbType");
			}
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x0016405C File Offset: 0x0016225C
		internal static string EntityClient_InvalidParameterDirection(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterDirection", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x00164080 File Offset: 0x00162280
		internal static string EntityClient_UnknownParameterType(object p0)
		{
			return EntityRes.GetString("EntityClient_UnknownParameterType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x001640A4 File Offset: 0x001622A4
		internal static string EntityClient_UnsupportedDbType(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnsupportedDbType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x001640CC File Offset: 0x001622CC
		internal static string EntityClient_IncompatibleNavigationPropertyResult(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_IncompatibleNavigationPropertyResult", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x001640F3 File Offset: 0x001622F3
		internal static string EntityClient_TransactionAlreadyStarted
		{
			get
			{
				return EntityRes.GetString("EntityClient_TransactionAlreadyStarted");
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x001640FF File Offset: 0x001622FF
		internal static string EntityClient_InvalidTransactionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidTransactionForCommand");
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x0016410B File Offset: 0x0016230B
		internal static string EntityClient_NoStoreConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoStoreConnectionForUpdate");
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x00164117 File Offset: 0x00162317
		internal static string EntityClient_CommandTreeMetadataIncompatible
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandTreeMetadataIncompatible");
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x00164123 File Offset: 0x00162323
		internal static string EntityClient_ProviderGeneralError
		{
			get
			{
				return EntityRes.GetString("EntityClient_ProviderGeneralError");
			}
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x00164130 File Offset: 0x00162330
		internal static string EntityClient_ProviderSpecificError(object p0)
		{
			return EntityRes.GetString("EntityClient_ProviderSpecificError", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x00164153 File Offset: 0x00162353
		internal static string EntityClient_FunctionImportEmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_FunctionImportEmptyCommandText");
			}
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x00164160 File Offset: 0x00162360
		internal static string EntityClient_UnableToFindFunctionImportContainer(object p0)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImportContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00164184 File Offset: 0x00162384
		internal static string EntityClient_UnableToFindFunctionImport(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImport", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x001641AC File Offset: 0x001623AC
		internal static string EntityClient_FunctionImportMustBeNonComposable(object p0)
		{
			return EntityRes.GetString("EntityClient_FunctionImportMustBeNonComposable", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x001641D0 File Offset: 0x001623D0
		internal static string EntityClient_UnmappedFunctionImport(object p0)
		{
			return EntityRes.GetString("EntityClient_UnmappedFunctionImport", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06004CAA RID: 19626 RVA: 0x001641F3 File Offset: 0x001623F3
		internal static string EntityClient_InvalidStoredProcedureCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidStoredProcedureCommandText");
			}
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x00164200 File Offset: 0x00162400
		internal static string EntityClient_ItemCollectionsNotRegisteredInWorkspace(object p0)
		{
			return EntityRes.GetString("EntityClient_ItemCollectionsNotRegisteredInWorkspace", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x00164224 File Offset: 0x00162424
		internal static string EntityClient_DbConnectionHasNoProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_DbConnectionHasNoProvider", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06004CAD RID: 19629 RVA: 0x00164247 File Offset: 0x00162447
		internal static string EntityClient_RequiresNonStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_RequiresNonStoreCommandTree");
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x00164253 File Offset: 0x00162453
		internal static string EntityClient_CannotReprepareCommandDefinitionBasedCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotReprepareCommandDefinitionBasedCommand");
			}
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x00164260 File Offset: 0x00162460
		internal static string EntityClient_EntityParameterEdmTypeNotScalar(object p0)
		{
			return EntityRes.GetString("EntityClient_EntityParameterEdmTypeNotScalar", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x00164284 File Offset: 0x00162484
		internal static string EntityClient_EntityParameterInconsistentEdmType(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_EntityParameterInconsistentEdmType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004CB1 RID: 19633 RVA: 0x001642AB File Offset: 0x001624AB
		internal static string EntityClient_CannotGetCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotGetCommandText");
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004CB2 RID: 19634 RVA: 0x001642B7 File Offset: 0x001624B7
		internal static string EntityClient_CannotSetCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotSetCommandText");
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x001642C3 File Offset: 0x001624C3
		internal static string EntityClient_CannotGetCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotGetCommandTree");
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x001642CF File Offset: 0x001624CF
		internal static string EntityClient_CannotSetCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotSetCommandTree");
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004CB5 RID: 19637 RVA: 0x001642DB File Offset: 0x001624DB
		internal static string ELinq_ExpressionMustBeIQueryable
		{
			get
			{
				return EntityRes.GetString("ELinq_ExpressionMustBeIQueryable");
			}
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x001642E8 File Offset: 0x001624E8
		internal static string ELinq_UnsupportedExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedExpressionType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0016430C File Offset: 0x0016250C
		internal static string ELinq_UnsupportedUseOfContextParameter(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedUseOfContextParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x00164330 File Offset: 0x00162530
		internal static string ELinq_UnboundParameterExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnboundParameterExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x00164353 File Offset: 0x00162553
		internal static string ELinq_UnsupportedConstructor
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedConstructor");
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x0016435F File Offset: 0x0016255F
		internal static string ELinq_UnsupportedInitializers
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInitializers");
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004CBB RID: 19643 RVA: 0x0016436B File Offset: 0x0016256B
		internal static string ELinq_UnsupportedBinding
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedBinding");
			}
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x00164378 File Offset: 0x00162578
		internal static string ELinq_UnsupportedMethod(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethod", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0016439C File Offset: 0x0016259C
		internal static string ELinq_UnsupportedMethodSuggestedAlternative(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethodSuggestedAlternative", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004CBE RID: 19646 RVA: 0x001643C3 File Offset: 0x001625C3
		internal static string ELinq_ThenByDoesNotFollowOrderBy
		{
			get
			{
				return EntityRes.GetString("ELinq_ThenByDoesNotFollowOrderBy");
			}
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x001643D0 File Offset: 0x001625D0
		internal static string ELinq_UnrecognizedMember(object p0)
		{
			return EntityRes.GetString("ELinq_UnrecognizedMember", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x001643F4 File Offset: 0x001625F4
		internal static string ELinq_UnresolvableFunctionForMethod(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethod", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0016441C File Offset: 0x0016261C
		internal static string ELinq_UnresolvableFunctionForMethodAmbiguousMatch(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodAmbiguousMatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x00164444 File Offset: 0x00162644
		internal static string ELinq_UnresolvableFunctionForMethodNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0016446C File Offset: 0x0016266C
		internal static string ELinq_UnresolvableFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x00164494 File Offset: 0x00162694
		internal static string ELinq_UnresolvableStoreFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x001644BC File Offset: 0x001626BC
		internal static string ELinq_UnresolvableFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x001644E0 File Offset: 0x001626E0
		internal static string ELinq_UnresolvableStoreFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x00164504 File Offset: 0x00162704
		internal static string ELinq_UnsupportedType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x00164528 File Offset: 0x00162728
		internal static string ELinq_UnsupportedNullConstant(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedNullConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x0016454C File Offset: 0x0016274C
		internal static string ELinq_UnsupportedConstant(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x00164570 File Offset: 0x00162770
		internal static string ELinq_UnsupportedCast(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedCast", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x00164598 File Offset: 0x00162798
		internal static string ELinq_UnsupportedIsOrAs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedIsOrAs", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x001645C3 File Offset: 0x001627C3
		internal static string ELinq_UnsupportedQueryableMethod
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedQueryableMethod");
			}
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x001645D0 File Offset: 0x001627D0
		internal static string ELinq_InvalidOfTypeResult(object p0)
		{
			return EntityRes.GetString("ELinq_InvalidOfTypeResult", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x001645F4 File Offset: 0x001627F4
		internal static string ELinq_UnsupportedNominalType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedNominalType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x00164618 File Offset: 0x00162818
		internal static string ELinq_UnsupportedEnumerableType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedEnumerableType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0016463C File Offset: 0x0016283C
		internal static string ELinq_UnsupportedHeterogeneousInitializers(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedHeterogeneousInitializers", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0016465F File Offset: 0x0016285F
		internal static string ELinq_UnsupportedDifferentContexts
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedDifferentContexts");
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06004CD2 RID: 19666 RVA: 0x0016466B File Offset: 0x0016286B
		internal static string ELinq_UnsupportedCastToDecimal
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedCastToDecimal");
			}
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x00164678 File Offset: 0x00162878
		internal static string ELinq_UnsupportedKeySelector(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedKeySelector", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x0016469B File Offset: 0x0016289B
		internal static string ELinq_CreateOrderedEnumerableNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_CreateOrderedEnumerableNotSupported");
			}
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x001646A8 File Offset: 0x001628A8
		internal static string ELinq_UnsupportedPassthrough(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedPassthrough", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x001646D0 File Offset: 0x001628D0
		internal static string ELinq_UnexpectedTypeForNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ELinq_UnexpectedTypeForNavigationProperty", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x001646FF File Offset: 0x001628FF
		internal static string ELinq_SkipWithoutOrder
		{
			get
			{
				return EntityRes.GetString("ELinq_SkipWithoutOrder");
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x0016470B File Offset: 0x0016290B
		internal static string ELinq_PropertyIndexNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_PropertyIndexNotSupported");
			}
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x00164718 File Offset: 0x00162918
		internal static string ELinq_NotPropertyOrField(object p0)
		{
			return EntityRes.GetString("ELinq_NotPropertyOrField", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0016473C File Offset: 0x0016293C
		internal static string ELinq_UnsupportedStringRemoveCase(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedStringRemoveCase", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x00164764 File Offset: 0x00162964
		internal static string ELinq_UnsupportedTrimStartTrimEndCase(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedTrimStartTrimEndCase", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x00164788 File Offset: 0x00162988
		internal static string ELinq_UnsupportedVBDatePartNonConstantInterval(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartNonConstantInterval", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x001647B0 File Offset: 0x001629B0
		internal static string ELinq_UnsupportedVBDatePartInvalidInterval(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartInvalidInterval", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x001647DC File Offset: 0x001629DC
		internal static string ELinq_UnsupportedAsUnicodeAndAsNonUnicode(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedAsUnicodeAndAsNonUnicode", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x00164800 File Offset: 0x00162A00
		internal static string ELinq_UnsupportedComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x00164824 File Offset: 0x00162A24
		internal static string ELinq_UnsupportedRefComparison(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedRefComparison", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0016484C File Offset: 0x00162A4C
		internal static string ELinq_UnsupportedRowComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x00164870 File Offset: 0x00162A70
		internal static string ELinq_UnsupportedRowMemberComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowMemberComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x00164894 File Offset: 0x00162A94
		internal static string ELinq_UnsupportedRowTypeComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowTypeComparison", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x001648B7 File Offset: 0x00162AB7
		internal static string ELinq_AnonymousType
		{
			get
			{
				return EntityRes.GetString("ELinq_AnonymousType");
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x001648C3 File Offset: 0x00162AC3
		internal static string ELinq_ClosureType
		{
			get
			{
				return EntityRes.GetString("ELinq_ClosureType");
			}
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x001648D0 File Offset: 0x00162AD0
		internal static string ELinq_UnhandledExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledExpressionType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x001648F4 File Offset: 0x00162AF4
		internal static string ELinq_UnhandledBindingType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledBindingType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x00164917 File Offset: 0x00162B17
		internal static string ELinq_UnsupportedNestedFirst
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedFirst");
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x00164923 File Offset: 0x00162B23
		internal static string ELinq_UnsupportedNestedSingle
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedSingle");
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x0016492F File Offset: 0x00162B2F
		internal static string ELinq_UnsupportedInclude
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInclude");
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x0016493B File Offset: 0x00162B3B
		internal static string ELinq_UnsupportedMergeAs
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedMergeAs");
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x00164947 File Offset: 0x00162B47
		internal static string ELinq_MethodNotDirectlyCallable
		{
			get
			{
				return EntityRes.GetString("ELinq_MethodNotDirectlyCallable");
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x00164953 File Offset: 0x00162B53
		internal static string ELinq_CycleDetected
		{
			get
			{
				return EntityRes.GetString("ELinq_CycleDetected");
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x00164960 File Offset: 0x00162B60
		internal static string ELinq_DbFunctionAttributedFunctionWithWrongReturnType(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_DbFunctionAttributedFunctionWithWrongReturnType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004CEF RID: 19695 RVA: 0x00164987 File Offset: 0x00162B87
		internal static string ELinq_DbFunctionDirectCall
		{
			get
			{
				return EntityRes.GetString("ELinq_DbFunctionDirectCall");
			}
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x00164994 File Offset: 0x00162B94
		internal static string ELinq_HasFlagArgumentAndSourceTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_HasFlagArgumentAndSourceTypeMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x001649BC File Offset: 0x00162BBC
		internal static string Elinq_ToStringNotSupportedForType(object p0)
		{
			return EntityRes.GetString("Elinq_ToStringNotSupportedForType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x001649DF File Offset: 0x00162BDF
		internal static string Elinq_ToStringNotSupportedForEnumsWithFlags
		{
			get
			{
				return EntityRes.GetString("Elinq_ToStringNotSupportedForEnumsWithFlags");
			}
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x001649EC File Offset: 0x00162BEC
		internal static string CompiledELinq_UnsupportedParameterTypes(object p0)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedParameterTypes", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x00164A10 File Offset: 0x00162C10
		internal static string CompiledELinq_UnsupportedNamedParameterType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x00164A38 File Offset: 0x00162C38
		internal static string CompiledELinq_UnsupportedNamedParameterUseAsType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterUseAsType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x00164A60 File Offset: 0x00162C60
		internal static string Update_UnsupportedExpressionKind(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExpressionKind", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x00164A88 File Offset: 0x00162C88
		internal static string Update_UnsupportedCastArgument(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedCastArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x00164AAC File Offset: 0x00162CAC
		internal static string Update_UnsupportedExtentType(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExtentType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x00164AD3 File Offset: 0x00162CD3
		internal static string Update_ConstraintCycle
		{
			get
			{
				return EntityRes.GetString("Update_ConstraintCycle");
			}
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x00164AE0 File Offset: 0x00162CE0
		internal static string Update_UnsupportedJoinType(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedJoinType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x00164B04 File Offset: 0x00162D04
		internal static string Update_UnsupportedProjection(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedProjection", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x00164B28 File Offset: 0x00162D28
		internal static string Update_ConcurrencyError(object p0)
		{
			return EntityRes.GetString("Update_ConcurrencyError", new object[]
			{
				p0
			});
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x00164B4C File Offset: 0x00162D4C
		internal static string Update_MissingEntity(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingEntity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x00164B74 File Offset: 0x00162D74
		internal static string Update_RelationshipCardinalityConstraintViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolation", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x00164BAD File Offset: 0x00162DAD
		internal static string Update_GeneralExecutionException
		{
			get
			{
				return EntityRes.GetString("Update_GeneralExecutionException");
			}
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x00164BBC File Offset: 0x00162DBC
		internal static string Update_MissingRequiredEntity(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingRequiredEntity", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x00164BE8 File Offset: 0x00162DE8
		internal static string Update_RelationshipCardinalityViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityViolation", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x00164C24 File Offset: 0x00162E24
		internal static string Update_NotSupportedComputedKeyColumn(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_NotSupportedComputedKeyColumn", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x00164C58 File Offset: 0x00162E58
		internal static string Update_AmbiguousServerGenIdentifier
		{
			get
			{
				return EntityRes.GetString("Update_AmbiguousServerGenIdentifier");
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004D04 RID: 19716 RVA: 0x00164C64 File Offset: 0x00162E64
		internal static string Update_WorkspaceMismatch
		{
			get
			{
				return EntityRes.GetString("Update_WorkspaceMismatch");
			}
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00164C70 File Offset: 0x00162E70
		internal static string Update_MissingRequiredRelationshipValue(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingRequiredRelationshipValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x00164C98 File Offset: 0x00162E98
		internal static string Update_MissingResultColumn(object p0)
		{
			return EntityRes.GetString("Update_MissingResultColumn", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x00164CBC File Offset: 0x00162EBC
		internal static string Update_NullReturnValueForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Update_NullReturnValueForNonNullableMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x00164CE4 File Offset: 0x00162EE4
		internal static string Update_ReturnValueHasUnexpectedType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Update_ReturnValueHasUnexpectedType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x00164D14 File Offset: 0x00162F14
		internal static string Update_UnableToConvertRowsAffectedParameter(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnableToConvertRowsAffectedParameter", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x00164D3C File Offset: 0x00162F3C
		internal static string Update_MappingNotFound(object p0)
		{
			return EntityRes.GetString("Update_MappingNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x00164D60 File Offset: 0x00162F60
		internal static string Update_ModifyingIdentityColumn(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_ModifyingIdentityColumn", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x00164D8C File Offset: 0x00162F8C
		internal static string Update_GeneratedDependent(object p0)
		{
			return EntityRes.GetString("Update_GeneratedDependent", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004D0D RID: 19725 RVA: 0x00164DAF File Offset: 0x00162FAF
		internal static string Update_ReferentialConstraintIntegrityViolation
		{
			get
			{
				return EntityRes.GetString("Update_ReferentialConstraintIntegrityViolation");
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004D0E RID: 19726 RVA: 0x00164DBB File Offset: 0x00162FBB
		internal static string Update_ErrorLoadingRecord
		{
			get
			{
				return EntityRes.GetString("Update_ErrorLoadingRecord");
			}
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x00164DC8 File Offset: 0x00162FC8
		internal static string Update_NullValue(object p0)
		{
			return EntityRes.GetString("Update_NullValue", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004D10 RID: 19728 RVA: 0x00164DEB File Offset: 0x00162FEB
		internal static string Update_CircularRelationships
		{
			get
			{
				return EntityRes.GetString("Update_CircularRelationships");
			}
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x00164DF8 File Offset: 0x00162FF8
		internal static string Update_RelationshipCardinalityConstraintViolationSingleValue(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolationSingleValue", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x00164E2C File Offset: 0x0016302C
		internal static string Update_MissingFunctionMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingFunctionMapping", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x00164E57 File Offset: 0x00163057
		internal static string Update_InvalidChanges
		{
			get
			{
				return EntityRes.GetString("Update_InvalidChanges");
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004D14 RID: 19732 RVA: 0x00164E63 File Offset: 0x00163063
		internal static string Update_DuplicateKeys
		{
			get
			{
				return EntityRes.GetString("Update_DuplicateKeys");
			}
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x00164E70 File Offset: 0x00163070
		internal static string Update_AmbiguousForeignKey(object p0)
		{
			return EntityRes.GetString("Update_AmbiguousForeignKey", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x00164E94 File Offset: 0x00163094
		internal static string Update_InsertingOrUpdatingReferenceToDeletedEntity(object p0)
		{
			return EntityRes.GetString("Update_InsertingOrUpdatingReferenceToDeletedEntity", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x00164EB7 File Offset: 0x001630B7
		internal static string ViewGen_Extent
		{
			get
			{
				return EntityRes.GetString("ViewGen_Extent");
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x00164EC3 File Offset: 0x001630C3
		internal static string ViewGen_Null
		{
			get
			{
				return EntityRes.GetString("ViewGen_Null");
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x00164ECF File Offset: 0x001630CF
		internal static string ViewGen_CommaBlank
		{
			get
			{
				return EntityRes.GetString("ViewGen_CommaBlank");
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004D1A RID: 19738 RVA: 0x00164EDB File Offset: 0x001630DB
		internal static string ViewGen_Entities
		{
			get
			{
				return EntityRes.GetString("ViewGen_Entities");
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x00164EE7 File Offset: 0x001630E7
		internal static string ViewGen_Tuples
		{
			get
			{
				return EntityRes.GetString("ViewGen_Tuples");
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004D1C RID: 19740 RVA: 0x00164EF3 File Offset: 0x001630F3
		internal static string ViewGen_NotNull
		{
			get
			{
				return EntityRes.GetString("ViewGen_NotNull");
			}
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x00164F00 File Offset: 0x00163100
		internal static string ViewGen_NegatedCellConstant(object p0)
		{
			return EntityRes.GetString("ViewGen_NegatedCellConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x00164F23 File Offset: 0x00163123
		internal static string ViewGen_Error
		{
			get
			{
				return EntityRes.GetString("ViewGen_Error");
			}
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x00164F30 File Offset: 0x00163130
		internal static string Viewgen_CannotGenerateQueryViewUnderNoValidation(object p0)
		{
			return EntityRes.GetString("Viewgen_CannotGenerateQueryViewUnderNoValidation", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x00164F54 File Offset: 0x00163154
		internal static string ViewGen_Missing_Sets_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Sets_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x00164F78 File Offset: 0x00163178
		internal static string ViewGen_Missing_Type_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Type_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x00164F9C File Offset: 0x0016319C
		internal static string ViewGen_Missing_Set_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Set_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x00164FC0 File Offset: 0x001631C0
		internal static string ViewGen_Concurrency_Derived_Class(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Derived_Class", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x00164FEC File Offset: 0x001631EC
		internal static string ViewGen_Concurrency_Invalid_Condition(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Invalid_Condition", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x00165014 File Offset: 0x00163214
		internal static string ViewGen_TableKey_Missing(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_TableKey_Missing", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x0016503C File Offset: 0x0016323C
		internal static string ViewGen_EntitySetKey_Missing(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySetKey_Missing", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x00165064 File Offset: 0x00163264
		internal static string ViewGen_AssociationSetKey_Missing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSetKey_Missing", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x00165090 File Offset: 0x00163290
		internal static string ViewGen_Cannot_Recover_Attributes(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Attributes", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x001650BC File Offset: 0x001632BC
		internal static string ViewGen_Cannot_Recover_Types(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Types", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x001650E4 File Offset: 0x001632E4
		internal static string ViewGen_Cannot_Disambiguate_MultiConstant(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Disambiguate_MultiConstant", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0016510C File Offset: 0x0016330C
		internal static string ViewGen_No_Default_Value(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x00165134 File Offset: 0x00163334
		internal static string ViewGen_No_Default_Value_For_Configuration(object p0)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value_For_Configuration", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x00165158 File Offset: 0x00163358
		internal static string ViewGen_KeyConstraint_Violation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Violation", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x00165194 File Offset: 0x00163394
		internal static string ViewGen_KeyConstraint_Update_Violation_EntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_EntitySet", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x001651C4 File Offset: 0x001633C4
		internal static string ViewGen_KeyConstraint_Update_Violation_AssociationSet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_AssociationSet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x001651F0 File Offset: 0x001633F0
		internal static string ViewGen_AssociationEndShouldBeMappedToKey(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_AssociationEndShouldBeMappedToKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x00165218 File Offset: 0x00163418
		internal static string ViewGen_Duplicate_CProperties(object p0)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0016523C File Offset: 0x0016343C
		internal static string ViewGen_Duplicate_CProperties_IsMapped(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties_IsMapped", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x00165264 File Offset: 0x00163464
		internal static string ViewGen_NotNull_No_Projected_Slot(object p0)
		{
			return EntityRes.GetString("ViewGen_NotNull_No_Projected_Slot", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00165288 File Offset: 0x00163488
		internal static string ViewGen_InvalidCondition(object p0)
		{
			return EntityRes.GetString("ViewGen_InvalidCondition", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x001652AC File Offset: 0x001634AC
		internal static string ViewGen_NonKeyProjectedWithOverlappingPartitions(object p0)
		{
			return EntityRes.GetString("ViewGen_NonKeyProjectedWithOverlappingPartitions", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x001652D0 File Offset: 0x001634D0
		internal static string ViewGen_CQ_PartitionConstraint(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_PartitionConstraint", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x001652F4 File Offset: 0x001634F4
		internal static string ViewGen_CQ_DomainConstraint(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_DomainConstraint", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x00165318 File Offset: 0x00163518
		internal static string ViewGen_ErrorLog(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0016533C File Offset: 0x0016353C
		internal static string ViewGen_ErrorLog2(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog2", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x00165360 File Offset: 0x00163560
		internal static string ViewGen_Foreign_Key_Missing_Table_Mapping(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Table_Mapping", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x00165388 File Offset: 0x00163588
		internal static string ViewGen_Foreign_Key_ParentTable_NotMappedToEnd(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ParentTable_NotMappedToEnd", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x001653C4 File Offset: 0x001635C4
		internal static string ViewGen_Foreign_Key(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x001653F8 File Offset: 0x001635F8
		internal static string ViewGen_Foreign_Key_UpperBound_MustBeOne(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_UpperBound_MustBeOne", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x00165424 File Offset: 0x00163624
		internal static string ViewGen_Foreign_Key_LowerBound_MustBeOne(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_LowerBound_MustBeOne", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x00165450 File Offset: 0x00163650
		internal static string ViewGen_Foreign_Key_Missing_Relationship_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Relationship_Mapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00165474 File Offset: 0x00163674
		internal static string ViewGen_Foreign_Key_Not_Guaranteed_InCSpace(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Not_Guaranteed_InCSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00165498 File Offset: 0x00163698
		internal static string ViewGen_Foreign_Key_ColumnOrder_Incorrect(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ColumnOrder_Incorrect", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5,
				p6,
				p7,
				p8
			});
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x001654E4 File Offset: 0x001636E4
		internal static string ViewGen_AssociationSet_AsUserString(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00165510 File Offset: 0x00163710
		internal static string ViewGen_AssociationSet_AsUserString_Negated(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString_Negated", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0016553C File Offset: 0x0016373C
		internal static string ViewGen_EntitySet_AsUserString(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x00165564 File Offset: 0x00163764
		internal static string ViewGen_EntitySet_AsUserString_Negated(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString_Negated", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004D46 RID: 19782 RVA: 0x0016558B File Offset: 0x0016378B
		internal static string ViewGen_EntityInstanceToken
		{
			get
			{
				return EntityRes.GetString("ViewGen_EntityInstanceToken");
			}
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x00165598 File Offset: 0x00163798
		internal static string Viewgen_ConfigurationErrorMsg(object p0)
		{
			return EntityRes.GetString("Viewgen_ConfigurationErrorMsg", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x001655BC File Offset: 0x001637BC
		internal static string ViewGen_HashOnMappingClosure_Not_Matching(object p0)
		{
			return EntityRes.GetString("ViewGen_HashOnMappingClosure_Not_Matching", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x001655E0 File Offset: 0x001637E0
		internal static string Viewgen_RightSideNotDisjoint(object p0)
		{
			return EntityRes.GetString("Viewgen_RightSideNotDisjoint", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x00165604 File Offset: 0x00163804
		internal static string Viewgen_QV_RewritingNotFound(object p0)
		{
			return EntityRes.GetString("Viewgen_QV_RewritingNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x00165628 File Offset: 0x00163828
		internal static string Viewgen_NullableMappingForNonNullableColumn(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_NullableMappingForNonNullableColumn", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x00165650 File Offset: 0x00163850
		internal static string Viewgen_ErrorPattern_ConditionMemberIsMapped(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_ConditionMemberIsMapped", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x00165674 File Offset: 0x00163874
		internal static string Viewgen_ErrorPattern_DuplicateConditionValue(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_DuplicateConditionValue", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00165698 File Offset: 0x00163898
		internal static string Viewgen_ErrorPattern_TableMappedToMultipleES(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_TableMappedToMultipleES", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004D4F RID: 19791 RVA: 0x001656C3 File Offset: 0x001638C3
		internal static string Viewgen_ErrorPattern_Partition_Disj_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Eq");
			}
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x001656D0 File Offset: 0x001638D0
		internal static string Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x001656F8 File Offset: 0x001638F8
		internal static string Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004D52 RID: 19794 RVA: 0x0016571F File Offset: 0x0016391F
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs_Ref");
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x0016572B File Offset: 0x0016392B
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs");
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x00165737 File Offset: 0x00163937
		internal static string Viewgen_ErrorPattern_Partition_Disj_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Unk");
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06004D55 RID: 19797 RVA: 0x00165743 File Offset: 0x00163943
		internal static string Viewgen_ErrorPattern_Partition_Eq_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Disj");
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x0016574F File Offset: 0x0016394F
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs_Ref");
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004D57 RID: 19799 RVA: 0x0016575B File Offset: 0x0016395B
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs");
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004D58 RID: 19800 RVA: 0x00165767 File Offset: 0x00163967
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk");
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004D59 RID: 19801 RVA: 0x00165773 File Offset: 0x00163973
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk_Association
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk_Association");
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x0016577F File Offset: 0x0016397F
		internal static string Viewgen_ErrorPattern_Partition_Sub_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Disj");
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004D5B RID: 19803 RVA: 0x0016578B File Offset: 0x0016398B
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq");
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x00165797 File Offset: 0x00163997
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq_Ref");
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004D5D RID: 19805 RVA: 0x001657A3 File Offset: 0x001639A3
		internal static string Viewgen_ErrorPattern_Partition_Sub_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Unk");
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004D5E RID: 19806 RVA: 0x001657AF File Offset: 0x001639AF
		internal static string Viewgen_NoJoinKeyOrFK
		{
			get
			{
				return EntityRes.GetString("Viewgen_NoJoinKeyOrFK");
			}
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x001657BC File Offset: 0x001639BC
		internal static string Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x001657E3 File Offset: 0x001639E3
		internal static string Validator_EmptyIdentity
		{
			get
			{
				return EntityRes.GetString("Validator_EmptyIdentity");
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004D61 RID: 19809 RVA: 0x001657EF File Offset: 0x001639EF
		internal static string Validator_CollectionHasNoTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionHasNoTypeUsage");
			}
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x001657FC File Offset: 0x001639FC
		internal static string Validator_NoKeyMembers(object p0)
		{
			return EntityRes.GetString("Validator_NoKeyMembers", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06004D63 RID: 19811 RVA: 0x0016581F File Offset: 0x00163A1F
		internal static string Validator_FacetTypeIsNull
		{
			get
			{
				return EntityRes.GetString("Validator_FacetTypeIsNull");
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x0016582B File Offset: 0x00163A2B
		internal static string Validator_MemberHasNullDeclaringType
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullDeclaringType");
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x00165837 File Offset: 0x00163A37
		internal static string Validator_MemberHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullTypeUsage");
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004D66 RID: 19814 RVA: 0x00165843 File Offset: 0x00163A43
		internal static string Validator_ItemAttributeHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_ItemAttributeHasNullTypeUsage");
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004D67 RID: 19815 RVA: 0x0016584F File Offset: 0x00163A4F
		internal static string Validator_RefTypeHasNullEntityType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypeHasNullEntityType");
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x0016585B File Offset: 0x00163A5B
		internal static string Validator_TypeUsageHasNullEdmType
		{
			get
			{
				return EntityRes.GetString("Validator_TypeUsageHasNullEdmType");
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004D69 RID: 19817 RVA: 0x00165867 File Offset: 0x00163A67
		internal static string Validator_BaseTypeHasMemberOfSameName
		{
			get
			{
				return EntityRes.GetString("Validator_BaseTypeHasMemberOfSameName");
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x00165873 File Offset: 0x00163A73
		internal static string Validator_CollectionTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionTypesCannotHaveBaseType");
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004D6B RID: 19819 RVA: 0x0016587F File Offset: 0x00163A7F
		internal static string Validator_RefTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypesCannotHaveBaseType");
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x0016588B File Offset: 0x00163A8B
		internal static string Validator_TypeHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoName");
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004D6D RID: 19821 RVA: 0x00165897 File Offset: 0x00163A97
		internal static string Validator_TypeHasNoNamespace
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoNamespace");
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x001658A3 File Offset: 0x00163AA3
		internal static string Validator_FacetHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_FacetHasNoName");
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x001658AF File Offset: 0x00163AAF
		internal static string Validator_MemberHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNoName");
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x001658BB File Offset: 0x00163ABB
		internal static string Validator_MetadataPropertyHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MetadataPropertyHasNoName");
			}
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x001658C8 File Offset: 0x00163AC8
		internal static string Validator_NullableEntityKeyProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_NullableEntityKeyProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x001658F0 File Offset: 0x00163AF0
		internal static string Validator_OSpace_InvalidNavPropReturnType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_InvalidNavPropReturnType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x0016591C File Offset: 0x00163B1C
		internal static string Validator_OSpace_ScalarPropertyNotPrimitive(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ScalarPropertyNotPrimitive", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x00165948 File Offset: 0x00163B48
		internal static string Validator_OSpace_ComplexPropertyNotComplex(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ComplexPropertyNotComplex", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x00165974 File Offset: 0x00163B74
		internal static string Validator_OSpace_Convention_MultipleTypesWithSameName(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MultipleTypesWithSameName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00165998 File Offset: 0x00163B98
		internal static string Validator_OSpace_Convention_NonPrimitiveTypeProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_NonPrimitiveTypeProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x001659C4 File Offset: 0x00163BC4
		internal static string Validator_OSpace_Convention_MissingRequiredProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingRequiredProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x001659EC File Offset: 0x00163BEC
		internal static string Validator_OSpace_Convention_BaseTypeIncompatible(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeIncompatible", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x00165A18 File Offset: 0x00163C18
		internal static string Validator_OSpace_Convention_MissingOSpaceType(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingOSpaceType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00165A3C File Offset: 0x00163C3C
		internal static string Validator_OSpace_Convention_RelationshipNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_RelationshipNotLoaded", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x00165A64 File Offset: 0x00163C64
		internal static string Validator_OSpace_Convention_AttributeAssemblyReferenced(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AttributeAssemblyReferenced", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00165A88 File Offset: 0x00163C88
		internal static string Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x00165AB4 File Offset: 0x00163CB4
		internal static string Validator_OSpace_Convention_AmbiguousClrType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AmbiguousClrType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x00165AE0 File Offset: 0x00163CE0
		internal static string Validator_OSpace_Convention_Struct(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_Struct", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x00165B08 File Offset: 0x00163D08
		internal static string Validator_OSpace_Convention_BaseTypeNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeNotLoaded", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x00165B30 File Offset: 0x00163D30
		internal static string Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x00165B57 File Offset: 0x00163D57
		internal static string Validator_OSpace_Convention_NonMatchingUnderlyingTypes
		{
			get
			{
				return EntityRes.GetString("Validator_OSpace_Convention_NonMatchingUnderlyingTypes");
			}
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x00165B64 File Offset: 0x00163D64
		internal static string Validator_UnsupportedEnumUnderlyingType(object p0)
		{
			return EntityRes.GetString("Validator_UnsupportedEnumUnderlyingType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x00165B87 File Offset: 0x00163D87
		internal static string ExtraInfo
		{
			get
			{
				return EntityRes.GetString("ExtraInfo");
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x00165B93 File Offset: 0x00163D93
		internal static string Metadata_General_Error
		{
			get
			{
				return EntityRes.GetString("Metadata_General_Error");
			}
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x00165BA0 File Offset: 0x00163DA0
		internal static string InvalidNumberOfParametersForAggregateFunction(object p0)
		{
			return EntityRes.GetString("InvalidNumberOfParametersForAggregateFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x00165BC4 File Offset: 0x00163DC4
		internal static string InvalidParameterTypeForAggregateFunction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidParameterTypeForAggregateFunction", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x00165BEC File Offset: 0x00163DEC
		internal static string InvalidSchemaEncountered(object p0)
		{
			return EntityRes.GetString("InvalidSchemaEncountered", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x00165C10 File Offset: 0x00163E10
		internal static string SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("SystemNamespaceEncountered", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x00165C34 File Offset: 0x00163E34
		internal static string NoCollectionForSpace(object p0)
		{
			return EntityRes.GetString("NoCollectionForSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x00165C57 File Offset: 0x00163E57
		internal static string OperationOnReadOnlyCollection
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyCollection");
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00165C63 File Offset: 0x00163E63
		internal static string OperationOnReadOnlyItem
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyItem");
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x00165C6F File Offset: 0x00163E6F
		internal static string EntitySetInAnotherContainer
		{
			get
			{
				return EntityRes.GetString("EntitySetInAnotherContainer");
			}
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x00165C7C File Offset: 0x00163E7C
		internal static string InvalidKeyMember(object p0)
		{
			return EntityRes.GetString("InvalidKeyMember", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x00165CA0 File Offset: 0x00163EA0
		internal static string InvalidFileExtension(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFileExtension", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00165CCC File Offset: 0x00163ECC
		internal static string NewTypeConflictsWithExistingType(object p0, object p1)
		{
			return EntityRes.GetString("NewTypeConflictsWithExistingType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004D90 RID: 19856 RVA: 0x00165CF3 File Offset: 0x00163EF3
		internal static string NotValidInputPath
		{
			get
			{
				return EntityRes.GetString("NotValidInputPath");
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004D91 RID: 19857 RVA: 0x00165CFF File Offset: 0x00163EFF
		internal static string UnableToDetermineApplicationContext
		{
			get
			{
				return EntityRes.GetString("UnableToDetermineApplicationContext");
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004D92 RID: 19858 RVA: 0x00165D0B File Offset: 0x00163F0B
		internal static string WildcardEnumeratorReturnedNull
		{
			get
			{
				return EntityRes.GetString("WildcardEnumeratorReturnedNull");
			}
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x00165D18 File Offset: 0x00163F18
		internal static string InvalidUseOfWebPath(object p0)
		{
			return EntityRes.GetString("InvalidUseOfWebPath", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x00165D3C File Offset: 0x00163F3C
		internal static string UnableToFindReflectedType(object p0, object p1)
		{
			return EntityRes.GetString("UnableToFindReflectedType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x00165D64 File Offset: 0x00163F64
		internal static string AssemblyMissingFromAssembliesToConsider(object p0)
		{
			return EntityRes.GetString("AssemblyMissingFromAssembliesToConsider", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x00165D87 File Offset: 0x00163F87
		internal static string UnableToLoadResource
		{
			get
			{
				return EntityRes.GetString("UnableToLoadResource");
			}
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x00165D94 File Offset: 0x00163F94
		internal static string EdmVersionNotSupportedByRuntime(object p0, object p1)
		{
			return EntityRes.GetString("EdmVersionNotSupportedByRuntime", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x00165DBB File Offset: 0x00163FBB
		internal static string AtleastOneSSDLNeeded
		{
			get
			{
				return EntityRes.GetString("AtleastOneSSDLNeeded");
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004D99 RID: 19865 RVA: 0x00165DC7 File Offset: 0x00163FC7
		internal static string InvalidMetadataPath
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataPath");
			}
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x00165DD4 File Offset: 0x00163FD4
		internal static string UnableToResolveAssembly(object p0)
		{
			return EntityRes.GetString("UnableToResolveAssembly", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x00165DF8 File Offset: 0x00163FF8
		internal static string DuplicatedFunctionoverloads(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatedFunctionoverloads", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x00165E20 File Offset: 0x00164020
		internal static string EntitySetNotInCSPace(object p0)
		{
			return EntityRes.GetString("EntitySetNotInCSPace", new object[]
			{
				p0
			});
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x00165E44 File Offset: 0x00164044
		internal static string TypeNotInEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInEntitySet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x00165E70 File Offset: 0x00164070
		internal static string TypeNotInAssociationSet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInAssociationSet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00165E9C File Offset: 0x0016409C
		internal static string DifferentSchemaVersionInCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DifferentSchemaVersionInCollection", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00165EC8 File Offset: 0x001640C8
		internal static string InvalidCollectionForMapping(object p0)
		{
			return EntityRes.GetString("InvalidCollectionForMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x00165EEB File Offset: 0x001640EB
		internal static string OnlyStoreConnectionsSupported
		{
			get
			{
				return EntityRes.GetString("OnlyStoreConnectionsSupported");
			}
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00165EF8 File Offset: 0x001640F8
		internal static string StoreItemCollectionMustHaveOneArtifact(object p0)
		{
			return EntityRes.GetString("StoreItemCollectionMustHaveOneArtifact", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x00165F1C File Offset: 0x0016411C
		internal static string CheckArgumentContainsNullFailed(object p0)
		{
			return EntityRes.GetString("CheckArgumentContainsNullFailed", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00165F40 File Offset: 0x00164140
		internal static string InvalidRelationshipSetName(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x00165F64 File Offset: 0x00164164
		internal static string InvalidEntitySetName(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x00165F88 File Offset: 0x00164188
		internal static string OnlyFunctionImportsCanBeAddedToEntityContainer(object p0)
		{
			return EntityRes.GetString("OnlyFunctionImportsCanBeAddedToEntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00165FAC File Offset: 0x001641AC
		internal static string ItemInvalidIdentity(object p0)
		{
			return EntityRes.GetString("ItemInvalidIdentity", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x00165FD0 File Offset: 0x001641D0
		internal static string ItemDuplicateIdentity(object p0)
		{
			return EntityRes.GetString("ItemDuplicateIdentity", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x00165FF3 File Offset: 0x001641F3
		internal static string NotStringTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotStringTypeForTypeUsage");
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004DAA RID: 19882 RVA: 0x00165FFF File Offset: 0x001641FF
		internal static string NotBinaryTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotBinaryTypeForTypeUsage");
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004DAB RID: 19883 RVA: 0x0016600B File Offset: 0x0016420B
		internal static string NotDateTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeTypeForTypeUsage");
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004DAC RID: 19884 RVA: 0x00166017 File Offset: 0x00164217
		internal static string NotDateTimeOffsetTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeOffsetTypeForTypeUsage");
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x00166023 File Offset: 0x00164223
		internal static string NotTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotTimeTypeForTypeUsage");
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x0016602F File Offset: 0x0016422F
		internal static string NotDecimalTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDecimalTypeForTypeUsage");
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004DAF RID: 19887 RVA: 0x0016603B File Offset: 0x0016423B
		internal static string ArrayTooSmall
		{
			get
			{
				return EntityRes.GetString("ArrayTooSmall");
			}
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x00166048 File Offset: 0x00164248
		internal static string MoreThanOneItemMatchesIdentity(object p0)
		{
			return EntityRes.GetString("MoreThanOneItemMatchesIdentity", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0016606C File Offset: 0x0016426C
		internal static string MissingDefaultValueForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MissingDefaultValueForConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x00166094 File Offset: 0x00164294
		internal static string MinAndMaxValueMustBeSameForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeSameForConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x001660BC File Offset: 0x001642BC
		internal static string BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x001660E4 File Offset: 0x001642E4
		internal static string MinAndMaxValueMustBeDifferentForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeDifferentForNonConstantFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0016610C File Offset: 0x0016430C
		internal static string MinAndMaxMustBePositive(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxMustBePositive", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x00166134 File Offset: 0x00164334
		internal static string MinMustBeLessThanMax(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MinMustBeLessThanMax", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x00166160 File Offset: 0x00164360
		internal static string SameRoleNameOnRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("SameRoleNameOnRelationshipAttribute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x00166188 File Offset: 0x00164388
		internal static string RoleTypeInEdmRelationshipAttributeIsInvalidType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RoleTypeInEdmRelationshipAttributeIsInvalidType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x001661B4 File Offset: 0x001643B4
		internal static string TargetRoleNameInNavigationPropertyNotValid(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TargetRoleNameInNavigationPropertyNotValid", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x001661E4 File Offset: 0x001643E4
		internal static string RelationshipNameInNavigationPropertyNotValid(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RelationshipNameInNavigationPropertyNotValid", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x00166210 File Offset: 0x00164410
		internal static string NestedClassNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("NestedClassNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x00166238 File Offset: 0x00164438
		internal static string NullParameterForEdmRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("NullParameterForEdmRelationshipAttribute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x00166260 File Offset: 0x00164460
		internal static string NullRelationshipNameforEdmRelationshipAttribute(object p0)
		{
			return EntityRes.GetString("NullRelationshipNameforEdmRelationshipAttribute", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x00166284 File Offset: 0x00164484
		internal static string NavigationPropertyRelationshipEndTypeMismatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("NavigationPropertyRelationshipEndTypeMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x001662B8 File Offset: 0x001644B8
		internal static string AllArtifactsMustTargetSameProvider_InvariantName(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_InvariantName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x001662E0 File Offset: 0x001644E0
		internal static string AllArtifactsMustTargetSameProvider_ManifestToken(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_ManifestToken", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x00166307 File Offset: 0x00164507
		internal static string ProviderManifestTokenNotFound
		{
			get
			{
				return EntityRes.GetString("ProviderManifestTokenNotFound");
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x00166313 File Offset: 0x00164513
		internal static string FailedToRetrieveProviderManifest
		{
			get
			{
				return EntityRes.GetString("FailedToRetrieveProviderManifest");
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x0016631F File Offset: 0x0016451F
		internal static string InvalidMaxLengthSize
		{
			get
			{
				return EntityRes.GetString("InvalidMaxLengthSize");
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x0016632B File Offset: 0x0016452B
		internal static string ArgumentMustBeCSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeCSpaceType");
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x00166337 File Offset: 0x00164537
		internal static string ArgumentMustBeOSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeOSpaceType");
			}
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x00166344 File Offset: 0x00164544
		internal static string FailedToFindOSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindOSpaceTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x00166368 File Offset: 0x00164568
		internal static string FailedToFindCSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindCSpaceTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x0016638C File Offset: 0x0016458C
		internal static string FailedToFindClrTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindClrTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x001663B0 File Offset: 0x001645B0
		internal static string GenericTypeNotSupported(object p0)
		{
			return EntityRes.GetString("GenericTypeNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x001663D4 File Offset: 0x001645D4
		internal static string InvalidEDMVersion(object p0)
		{
			return EntityRes.GetString("InvalidEDMVersion", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x001663F7 File Offset: 0x001645F7
		internal static string Mapping_General_Error
		{
			get
			{
				return EntityRes.GetString("Mapping_General_Error");
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x00166403 File Offset: 0x00164603
		internal static string Mapping_InvalidContent_General
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_General");
			}
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x00166410 File Offset: 0x00164610
		internal static string Mapping_InvalidContent_EntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x00166434 File Offset: 0x00164634
		internal static string Mapping_InvalidContent_StorageEntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_StorageEntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x00166458 File Offset: 0x00164658
		internal static string Mapping_AlreadyMapped_StorageEntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_AlreadyMapped_StorageEntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0016647C File Offset: 0x0016467C
		internal static string Mapping_InvalidContent_Entity_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Set", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x001664A0 File Offset: 0x001646A0
		internal static string Mapping_InvalidContent_Entity_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x001664C4 File Offset: 0x001646C4
		internal static string Mapping_InvalidContent_AbstractEntity_FunctionMapping(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_FunctionMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x001664E8 File Offset: 0x001646E8
		internal static string Mapping_InvalidContent_AbstractEntity_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x0016650C File Offset: 0x0016470C
		internal static string Mapping_InvalidContent_AbstractEntity_IsOfType(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_IsOfType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x00166530 File Offset: 0x00164730
		internal static string Mapping_InvalidContent_Entity_Type_For_Entity_Set(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type_For_Entity_Set", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x0016655C File Offset: 0x0016475C
		internal static string Mapping_Invalid_Association_Type_For_Association_Set(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_Association_Type_For_Association_Set", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x00166588 File Offset: 0x00164788
		internal static string Mapping_InvalidContent_Table(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Table", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x001665AC File Offset: 0x001647AC
		internal static string Mapping_InvalidContent_Complex_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Complex_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x001665D0 File Offset: 0x001647D0
		internal static string Mapping_InvalidContent_Association_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Set", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x001665F4 File Offset: 0x001647F4
		internal static string Mapping_InvalidContent_AssociationSet_Condition(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AssociationSet_Condition", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x00166618 File Offset: 0x00164818
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0016663C File Offset: 0x0016483C
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x00166660 File Offset: 0x00164860
		internal static string Mapping_InvalidContent_Association_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x00166684 File Offset: 0x00164884
		internal static string Mapping_InvalidContent_EndProperty(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EndProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06004DDF RID: 19935 RVA: 0x001666A7 File Offset: 0x001648A7
		internal static string Mapping_InvalidContent_Association_Type_Empty
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Association_Type_Empty");
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004DE0 RID: 19936 RVA: 0x001666B3 File Offset: 0x001648B3
		internal static string Mapping_InvalidContent_Table_Expected
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Table_Expected");
			}
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x001666C0 File Offset: 0x001648C0
		internal static string Mapping_InvalidContent_Cdm_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Cdm_Member", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x001666E4 File Offset: 0x001648E4
		internal static string Mapping_InvalidContent_Column(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Column", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x00166708 File Offset: 0x00164908
		internal static string Mapping_InvalidContent_End(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_End", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x0016672B File Offset: 0x0016492B
		internal static string Mapping_InvalidContent_Container_SubElement
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Container_SubElement");
			}
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x00166738 File Offset: 0x00164938
		internal static string Mapping_InvalidContent_Duplicate_Cdm_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Duplicate_Cdm_Member", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0016675C File Offset: 0x0016495C
		internal static string Mapping_InvalidContent_Duplicate_Condition_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Duplicate_Condition_Member", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004DE7 RID: 19943 RVA: 0x0016677F File Offset: 0x0016497F
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Members
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Members");
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x0016678B File Offset: 0x0016498B
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Members
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Members");
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004DE9 RID: 19945 RVA: 0x00166797 File Offset: 0x00164997
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Values
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Values");
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004DEA RID: 19946 RVA: 0x001667A3 File Offset: 0x001649A3
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Values
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Values");
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06004DEB RID: 19947 RVA: 0x001667AF File Offset: 0x001649AF
		internal static string Mapping_InvalidContent_ConditionMapping_NonScalar
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_NonScalar");
			}
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x001667BC File Offset: 0x001649BC
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x001667E4 File Offset: 0x001649E4
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidMember(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidMember", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x00166808 File Offset: 0x00164A08
		internal static string Mapping_InvalidContent_ConditionMapping_Computed(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Computed", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x0016682C File Offset: 0x00164A2C
		internal static string Mapping_InvalidContent_Emtpty_SetMap(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Emtpty_SetMap", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004DF0 RID: 19952 RVA: 0x0016684F File Offset: 0x00164A4F
		internal static string Mapping_InvalidContent_TypeMapping_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_TypeMapping_QueryView");
			}
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0016685C File Offset: 0x00164A5C
		internal static string Mapping_Default_OCMapping_Clr_Member(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00166888 File Offset: 0x00164A88
		internal static string Mapping_Default_OCMapping_Clr_Member2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member2", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x001668B4 File Offset: 0x00164AB4
		internal static string Mapping_Default_OCMapping_Invalid_MemberType(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Invalid_MemberType", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x001668F0 File Offset: 0x00164AF0
		internal static string Mapping_Default_OCMapping_MemberKind_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MemberKind_Mismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x0016692C File Offset: 0x00164B2C
		internal static string Mapping_Default_OCMapping_MultiplicityMismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MultiplicityMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00166968 File Offset: 0x00164B68
		internal static string Mapping_Default_OCMapping_Member_Count_Mismatch(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Count_Mismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x00166990 File Offset: 0x00164B90
		internal static string Mapping_Default_OCMapping_Member_Type_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Type_Mismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5,
				p6,
				p7
			});
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x001669D4 File Offset: 0x00164BD4
		internal static string Mapping_Enum_OCMapping_UnderlyingTypesMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_Enum_OCMapping_UnderlyingTypesMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x00166A04 File Offset: 0x00164C04
		internal static string Mapping_Enum_OCMapping_MemberMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_Enum_OCMapping_MemberMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x00166A34 File Offset: 0x00164C34
		internal static string Mapping_NotFound_EntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_NotFound_EntityContainer", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x00166A58 File Offset: 0x00164C58
		internal static string Mapping_Duplicate_CdmAssociationSet_StorageMap(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_CdmAssociationSet_StorageMap", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x00166A7C File Offset: 0x00164C7C
		internal static string Mapping_Invalid_CSRootElementMissing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_CSRootElementMissing", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x00166AA7 File Offset: 0x00164CA7
		internal static string Mapping_ConditionValueTypeMismatch
		{
			get
			{
				return EntityRes.GetString("Mapping_ConditionValueTypeMismatch");
			}
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x00166AB4 File Offset: 0x00164CB4
		internal static string Mapping_Storage_InvalidSpace(object p0)
		{
			return EntityRes.GetString("Mapping_Storage_InvalidSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x00166AD8 File Offset: 0x00164CD8
		internal static string Mapping_Invalid_Member_Mapping(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Invalid_Member_Mapping", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x00166B14 File Offset: 0x00164D14
		internal static string Mapping_Invalid_CSide_ScalarProperty(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_CSide_ScalarProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00166B38 File Offset: 0x00164D38
		internal static string Mapping_Duplicate_Type(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x00166B5C File Offset: 0x00164D5C
		internal static string Mapping_Duplicate_PropertyMap_CaseInsensitive(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_PropertyMap_CaseInsensitive", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x00166B80 File Offset: 0x00164D80
		internal static string Mapping_Enum_EmptyValue(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_EmptyValue", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x00166BA4 File Offset: 0x00164DA4
		internal static string Mapping_Enum_InvalidValue(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_InvalidValue", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x00166BC8 File Offset: 0x00164DC8
		internal static string Mapping_InvalidMappingSchema_Parsing(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_Parsing", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00166BEC File Offset: 0x00164DEC
		internal static string Mapping_InvalidMappingSchema_validation(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_validation", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x00166C10 File Offset: 0x00164E10
		internal static string Mapping_Object_InvalidType(object p0)
		{
			return EntityRes.GetString("Mapping_Object_InvalidType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x00166C34 File Offset: 0x00164E34
		internal static string Mapping_Provider_WrongConnectionType(object p0)
		{
			return EntityRes.GetString("Mapping_Provider_WrongConnectionType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x00166C58 File Offset: 0x00164E58
		internal static string Mapping_Views_For_Extent_Not_Generated(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Views_For_Extent_Not_Generated", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x00166C80 File Offset: 0x00164E80
		internal static string Mapping_TableName_QueryView(object p0)
		{
			return EntityRes.GetString("Mapping_TableName_QueryView", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x00166CA4 File Offset: 0x00164EA4
		internal static string Mapping_Empty_QueryView(object p0)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x00166CC8 File Offset: 0x00164EC8
		internal static string Mapping_Empty_QueryView_OfType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00166CF0 File Offset: 0x00164EF0
		internal static string Mapping_Empty_QueryView_OfTypeOnly(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfTypeOnly", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x00166D18 File Offset: 0x00164F18
		internal static string Mapping_QueryView_PropertyMaps(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_PropertyMaps", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x00166D3C File Offset: 0x00164F3C
		internal static string Mapping_Invalid_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x00166D64 File Offset: 0x00164F64
		internal static string Mapping_Invalid_QueryView2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView2", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x00166D8C File Offset: 0x00164F8C
		internal static string Mapping_Invalid_QueryView_Type(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView_Type", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x00166DAF File Offset: 0x00164FAF
		internal static string Mapping_TypeName_For_First_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_TypeName_For_First_QueryView");
			}
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x00166DBC File Offset: 0x00164FBC
		internal static string Mapping_AllQueryViewAtCompileTime(object p0)
		{
			return EntityRes.GetString("Mapping_AllQueryViewAtCompileTime", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x00166DE0 File Offset: 0x00164FE0
		internal static string Mapping_QueryViewMultipleTypeInTypeName(object p0)
		{
			return EntityRes.GetString("Mapping_QueryViewMultipleTypeInTypeName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x00166E04 File Offset: 0x00165004
		internal static string Mapping_QueryView_Duplicate_OfType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x00166E2C File Offset: 0x0016502C
		internal static string Mapping_QueryView_Duplicate_OfTypeOnly(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfTypeOnly", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x00166E54 File Offset: 0x00165054
		internal static string Mapping_QueryView_TypeName_Not_Defined(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_TypeName_Not_Defined", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x00166E78 File Offset: 0x00165078
		internal static string Mapping_QueryView_For_Base_Type(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_For_Base_Type", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x00166EA0 File Offset: 0x001650A0
		internal static string Mapping_UnsupportedExpressionKind_QueryView(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedExpressionKind_QueryView", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00166ECC File Offset: 0x001650CC
		internal static string Mapping_UnsupportedFunctionCall_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedFunctionCall_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00166EF4 File Offset: 0x001650F4
		internal static string Mapping_UnsupportedScanTarget_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedScanTarget_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00166F1C File Offset: 0x0016511C
		internal static string Mapping_UnsupportedPropertyKind_QueryView(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedPropertyKind_QueryView", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x00166F48 File Offset: 0x00165148
		internal static string Mapping_UnsupportedInitialization_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedInitialization_QueryView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x00166F70 File Offset: 0x00165170
		internal static string Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x00166FA0 File Offset: 0x001651A0
		internal static string Mapping_Invalid_Query_Views_MissingSetClosure(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Query_Views_MissingSetClosure", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x00166FC4 File Offset: 0x001651C4
		internal static string DbMappingViewCacheTypeAttribute_InvalidContextType(object p0)
		{
			return EntityRes.GetString("DbMappingViewCacheTypeAttribute_InvalidContextType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00166FE8 File Offset: 0x001651E8
		internal static string DbMappingViewCacheTypeAttribute_CacheTypeNotFound(object p0)
		{
			return EntityRes.GetString("DbMappingViewCacheTypeAttribute_CacheTypeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0016700C File Offset: 0x0016520C
		internal static string DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType(object p0)
		{
			return EntityRes.GetString("DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004E23 RID: 20003 RVA: 0x0016702F File Offset: 0x0016522F
		internal static string DbMappingViewCacheFactory_CreateFailure
		{
			get
			{
				return EntityRes.GetString("DbMappingViewCacheFactory_CreateFailure");
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x0016703C File Offset: 0x0016523C
		internal static string Generated_View_Type_Super_Class(object p0)
		{
			return EntityRes.GetString("Generated_View_Type_Super_Class", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x00167060 File Offset: 0x00165260
		internal static string Generated_Views_Invalid_Extent(object p0)
		{
			return EntityRes.GetString("Generated_Views_Invalid_Extent", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x00167083 File Offset: 0x00165283
		internal static string MappingViewCacheFactory_MustNotChange
		{
			get
			{
				return EntityRes.GetString("MappingViewCacheFactory_MustNotChange");
			}
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x00167090 File Offset: 0x00165290
		internal static string Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace(object p0)
		{
			return EntityRes.GetString("Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x001670B4 File Offset: 0x001652B4
		internal static string Mapping_AbstractTypeMappingToNonAbstractType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_AbstractTypeMappingToNonAbstractType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x001670DC File Offset: 0x001652DC
		internal static string Mapping_EnumTypeMappingToNonEnumType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_EnumTypeMappingToNonEnumType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x00167104 File Offset: 0x00165304
		internal static string StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004E2B RID: 20011 RVA: 0x0016712F File Offset: 0x0016532F
		internal static string Mapping_InvalidContent_IsTypeOfNotTerminated
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_IsTypeOfNotTerminated");
			}
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x0016713C File Offset: 0x0016533C
		internal static string Mapping_CannotMapCLRTypeMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_CannotMapCLRTypeMultipleTimes", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004E2D RID: 20013 RVA: 0x0016715F File Offset: 0x0016535F
		internal static string Mapping_ModificationFunction_In_Table_Context
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_In_Table_Context");
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x0016716B File Offset: 0x0016536B
		internal static string Mapping_ModificationFunction_Multiple_Types
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_Multiple_Types");
			}
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x00167178 File Offset: 0x00165378
		internal static string Mapping_ModificationFunction_UnknownFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_UnknownFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x0016719C File Offset: 0x0016539C
		internal static string Mapping_ModificationFunction_AmbiguousFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AmbiguousFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x001671C0 File Offset: 0x001653C0
		internal static string Mapping_ModificationFunction_NotValidFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_NotValidFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x001671E4 File Offset: 0x001653E4
		internal static string Mapping_ModificationFunction_NotValidFunctionParameter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_NotValidFunctionParameter", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00167210 File Offset: 0x00165410
		internal static string Mapping_ModificationFunction_MissingParameter(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingParameter", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00167238 File Offset: 0x00165438
		internal static string Mapping_ModificationFunction_AssociationSetDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetDoesNotExist", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x0016725C File Offset: 0x0016545C
		internal static string Mapping_ModificationFunction_AssociationSetRoleDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetRoleDoesNotExist", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00167280 File Offset: 0x00165480
		internal static string Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x001672A4 File Offset: 0x001654A4
		internal static string Mapping_ModificationFunction_AssociationSetCardinality(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetCardinality", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x001672C8 File Offset: 0x001654C8
		internal static string Mapping_ModificationFunction_ComplexTypeNotFound(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ComplexTypeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x001672EC File Offset: 0x001654EC
		internal static string Mapping_ModificationFunction_WrongComplexType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_WrongComplexType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x00167313 File Offset: 0x00165513
		internal static string Mapping_ModificationFunction_MissingVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_MissingVersion");
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x0016731F File Offset: 0x0016551F
		internal static string Mapping_ModificationFunction_VersionMustBeOriginal
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_VersionMustBeOriginal");
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004E3C RID: 20028 RVA: 0x0016732B File Offset: 0x0016552B
		internal static string Mapping_ModificationFunction_VersionMustBeCurrent
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_VersionMustBeCurrent");
			}
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x00167338 File Offset: 0x00165538
		internal static string Mapping_ModificationFunction_ParameterNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ParameterNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00167360 File Offset: 0x00165560
		internal static string Mapping_ModificationFunction_PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00167388 File Offset: 0x00165588
		internal static string Mapping_ModificationFunction_PropertyNotKey(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyNotKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x001673B0 File Offset: 0x001655B0
		internal static string Mapping_ModificationFunction_ParameterBoundTwice(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ParameterBoundTwice", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x001673D4 File Offset: 0x001655D4
		internal static string Mapping_ModificationFunction_RedundantEntityTypeMapping(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_RedundantEntityTypeMapping", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x001673F8 File Offset: 0x001655F8
		internal static string Mapping_ModificationFunction_MissingSetClosure(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingSetClosure", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x0016741C File Offset: 0x0016561C
		internal static string Mapping_ModificationFunction_MissingEntityType(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x00167440 File Offset: 0x00165640
		internal static string Mapping_ModificationFunction_PropertyParameterTypeMismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyParameterTypeMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x0016747C File Offset: 0x0016567C
		internal static string Mapping_ModificationFunction_AssociationSetAmbiguous(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetAmbiguous", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x001674A0 File Offset: 0x001656A0
		internal static string Mapping_ModificationFunction_MultipleEndsOfAssociationMapped(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MultipleEndsOfAssociationMapped", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x001674CC File Offset: 0x001656CC
		internal static string Mapping_ModificationFunction_AmbiguousResultBinding(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AmbiguousResultBinding", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x001674F4 File Offset: 0x001656F4
		internal static string Mapping_ModificationFunction_AssociationSetNotMappedForOperation(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetNotMappedForOperation", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x00167524 File Offset: 0x00165724
		internal static string Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00167550 File Offset: 0x00165750
		internal static string Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x00167574 File Offset: 0x00165774
		internal static string Mapping_StoreTypeMismatch_ScalarPropertyMapping(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_StoreTypeMismatch_ScalarPropertyMapping", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x0016759B File Offset: 0x0016579B
		internal static string Mapping_DistinctFlagInReadWriteContainer
		{
			get
			{
				return EntityRes.GetString("Mapping_DistinctFlagInReadWriteContainer");
			}
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x001675A8 File Offset: 0x001657A8
		internal static string Mapping_ProviderReturnsNullType(object p0)
		{
			return EntityRes.GetString("Mapping_ProviderReturnsNullType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x001675CB File Offset: 0x001657CB
		internal static string Mapping_DifferentEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentEdmStoreVersion");
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x001675D7 File Offset: 0x001657D7
		internal static string Mapping_DifferentMappingEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentMappingEdmStoreVersion");
			}
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x001675E4 File Offset: 0x001657E4
		internal static string Mapping_FunctionImport_StoreFunctionDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_StoreFunctionDoesNotExist", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00167608 File Offset: 0x00165808
		internal static string Mapping_FunctionImport_FunctionImportDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportDoesNotExist", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x00167630 File Offset: 0x00165830
		internal static string Mapping_FunctionImport_FunctionImportMappedMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportMappedMultipleTimes", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x00167654 File Offset: 0x00165854
		internal static string Mapping_FunctionImport_TargetFunctionMustBeNonComposable(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetFunctionMustBeNonComposable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x0016767C File Offset: 0x0016587C
		internal static string Mapping_FunctionImport_TargetFunctionMustBeComposable(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetFunctionMustBeComposable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x001676A4 File Offset: 0x001658A4
		internal static string Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x001676C8 File Offset: 0x001658C8
		internal static string Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x001676EC File Offset: 0x001658EC
		internal static string Mapping_FunctionImport_IncompatibleParameterMode(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterMode", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x00167718 File Offset: 0x00165918
		internal static string Mapping_FunctionImport_IncompatibleParameterType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x00167744 File Offset: 0x00165944
		internal static string Mapping_FunctionImport_IncompatibleEnumParameterType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleEnumParameterType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x00167774 File Offset: 0x00165974
		internal static string Mapping_FunctionImport_RowsAffectedParameterDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterDoesNotExist", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x0016779C File Offset: 0x0016599C
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x001677C4 File Offset: 0x001659C4
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongMode(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongMode", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x001677F4 File Offset: 0x001659F4
		internal static string Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x0016781C File Offset: 0x00165A1C
		internal static string Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x0016784C File Offset: 0x00165A4C
		internal static string Mapping_FunctionImport_ConditionValueTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ConditionValueTypeMismatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x00167878 File Offset: 0x00165A78
		internal static string Mapping_FunctionImport_UnsupportedType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnsupportedType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x001678A0 File Offset: 0x00165AA0
		internal static string Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x001678C4 File Offset: 0x00165AC4
		internal static string Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x001678EC File Offset: 0x00165AEC
		internal static string Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x00167910 File Offset: 0x00165B10
		internal static string Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x00167934 File Offset: 0x00165B34
		internal static string Mapping_FunctionImport_ResultMapping_InvalidSType(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidSType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x00167958 File Offset: 0x00165B58
		internal static string Mapping_FunctionImport_PropertyNotMapped(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_PropertyNotMapped", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x00167984 File Offset: 0x00165B84
		internal static string Mapping_FunctionImport_ImplicitMappingForAbstractReturnType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ImplicitMappingForAbstractReturnType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x001679AC File Offset: 0x00165BAC
		internal static string Mapping_FunctionImport_ScalarMappingToMulticolumnTVF(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ScalarMappingToMulticolumnTVF", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x001679D4 File Offset: 0x00165BD4
		internal static string Mapping_FunctionImport_ScalarMappingTypeMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ScalarMappingTypeMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x00167A04 File Offset: 0x00165C04
		internal static string Mapping_FunctionImport_UnreachableType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x00167A2C File Offset: 0x00165C2C
		internal static string Mapping_FunctionImport_UnreachableIsTypeOf(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableIsTypeOf", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x00167A54 File Offset: 0x00165C54
		internal static string Mapping_FunctionImport_FunctionAmbiguous(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionAmbiguous", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x00167A78 File Offset: 0x00165C78
		internal static string Mapping_FunctionImport_CannotInferTargetFunctionKeys(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_CannotInferTargetFunctionKeys", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x00167A9B File Offset: 0x00165C9B
		internal static string Entity_EntityCantHaveMultipleChangeTrackers
		{
			get
			{
				return EntityRes.GetString("Entity_EntityCantHaveMultipleChangeTrackers");
			}
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x00167AA8 File Offset: 0x00165CA8
		internal static string ComplexObject_NullableComplexTypesNotSupported(object p0)
		{
			return EntityRes.GetString("ComplexObject_NullableComplexTypesNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x00167ACB File Offset: 0x00165CCB
		internal static string ComplexObject_ComplexObjectAlreadyAttachedToParent
		{
			get
			{
				return EntityRes.GetString("ComplexObject_ComplexObjectAlreadyAttachedToParent");
			}
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x00167AD8 File Offset: 0x00165CD8
		internal static string ComplexObject_ComplexChangeRequestedOnScalarProperty(object p0)
		{
			return EntityRes.GetString("ComplexObject_ComplexChangeRequestedOnScalarProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x00167AFC File Offset: 0x00165CFC
		internal static string ObjectStateEntry_SetModifiedOnInvalidProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetModifiedOnInvalidProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x00167B1F File Offset: 0x00165D1F
		internal static string ObjectStateEntry_OriginalValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_OriginalValuesDoesNotExist");
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x00167B2B File Offset: 0x00165D2B
		internal static string ObjectStateEntry_CurrentValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CurrentValuesDoesNotExist");
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x00167B37 File Offset: 0x00165D37
		internal static string ObjectStateEntry_InvalidState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidState");
			}
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x00167B44 File Offset: 0x00165D44
		internal static string ObjectStateEntry_CannotModifyKeyProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004E77 RID: 20087 RVA: 0x00167B67 File Offset: 0x00165D67
		internal static string ObjectStateEntry_CantModifyRelationValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationValues");
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004E78 RID: 20088 RVA: 0x00167B73 File Offset: 0x00165D73
		internal static string ObjectStateEntry_CantModifyRelationState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationState");
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x00167B7F File Offset: 0x00165D7F
		internal static string ObjectStateEntry_CantModifyDetachedDeletedEntries
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyDetachedDeletedEntries");
			}
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x00167B8C File Offset: 0x00165D8C
		internal static string ObjectStateEntry_SetModifiedStates(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetModifiedStates", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004E7B RID: 20091 RVA: 0x00167BAF File Offset: 0x00165DAF
		internal static string ObjectStateEntry_CantSetEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantSetEntityKey");
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x00167BBB File Offset: 0x00165DBB
		internal static string ObjectStateEntry_CannotAccessKeyEntryValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotAccessKeyEntryValues");
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004E7D RID: 20093 RVA: 0x00167BC7 File Offset: 0x00165DC7
		internal static string ObjectStateEntry_CannotModifyKeyEntryState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyEntryState");
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x00167BD3 File Offset: 0x00165DD3
		internal static string ObjectStateEntry_CannotDeleteOnKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotDeleteOnKeyEntry");
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004E7F RID: 20095 RVA: 0x00167BDF File Offset: 0x00165DDF
		internal static string ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging");
			}
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x00167BEC File Offset: 0x00165DEC
		internal static string ObjectStateEntry_ChangeOnUnmappedProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x00167C10 File Offset: 0x00165E10
		internal static string ObjectStateEntry_ChangeOnUnmappedComplexProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedComplexProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x00167C34 File Offset: 0x00165E34
		internal static string ObjectStateEntry_ChangedInDifferentStateFromChanging(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangedInDifferentStateFromChanging", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x00167C5C File Offset: 0x00165E5C
		internal static string ObjectStateEntry_UnableToEnumerateCollection(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_UnableToEnumerateCollection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x00167C83 File Offset: 0x00165E83
		internal static string ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers");
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x00167C8F File Offset: 0x00165E8F
		internal static string ObjectStateEntry_InvalidTypeForComplexTypeProperty
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidTypeForComplexTypeProperty");
			}
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x00167C9C File Offset: 0x00165E9C
		internal static string ObjectStateEntry_ComplexObjectUsedMultipleTimes(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ComplexObjectUsedMultipleTimes", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x00167CC4 File Offset: 0x00165EC4
		internal static string ObjectStateEntry_SetOriginalComplexProperties(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalComplexProperties", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x00167CE8 File Offset: 0x00165EE8
		internal static string ObjectStateEntry_NullOriginalValueForNonNullableProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectStateEntry_NullOriginalValueForNonNullableProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x00167D14 File Offset: 0x00165F14
		internal static string ObjectStateEntry_SetOriginalPrimaryKey(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalPrimaryKey", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x00167D37 File Offset: 0x00165F37
		internal static string ObjectStateManager_NoEntryExistForEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_NoEntryExistForEntityKey");
			}
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x00167D44 File Offset: 0x00165F44
		internal static string ObjectStateManager_NoEntryExistsForObject(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_NoEntryExistsForObject", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004E8C RID: 20108 RVA: 0x00167D67 File Offset: 0x00165F67
		internal static string ObjectStateManager_EntityNotTracked
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityNotTracked");
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x00167D73 File Offset: 0x00165F73
		internal static string ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager");
			}
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00167D80 File Offset: 0x00165F80
		internal static string ObjectStateManager_ObjectStateManagerContainsThisEntityKey(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_ObjectStateManagerContainsThisEntityKey", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00167DA4 File Offset: 0x00165FA4
		internal static string ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity", new object[]
			{
				p0
			});
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x00167DC8 File Offset: 0x00165FC8
		internal static string ObjectStateManager_CannotFixUpKeyToExistingValues(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_CannotFixUpKeyToExistingValues", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x00167DEB File Offset: 0x00165FEB
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKey");
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x00167DF7 File Offset: 0x00165FF7
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach");
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x00167E03 File Offset: 0x00166003
		internal static string ObjectStateManager_InvalidKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_InvalidKey");
			}
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x00167E10 File Offset: 0x00166010
		internal static string ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x00167E37 File Offset: 0x00166037
		internal static string ObjectStateManager_AcceptChangesEntityKeyIsNotValid
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_AcceptChangesEntityKeyIsNotValid");
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004E96 RID: 20118 RVA: 0x00167E43 File Offset: 0x00166043
		internal static string ObjectStateManager_EntityConflictsWithKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityConflictsWithKeyEntry");
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x00167E4F File Offset: 0x0016604F
		internal static string ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity");
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x00167E5B File Offset: 0x0016605B
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityDeleted
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityDeleted");
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x00167E67 File Offset: 0x00166067
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityAdded
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityAdded");
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004E9A RID: 20122 RVA: 0x00167E73 File Offset: 0x00166073
		internal static string ObjectStateManager_CannotChangeRelationshipStateKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateKeyEntry");
			}
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x00167E80 File Offset: 0x00166080
		internal static string ObjectStateManager_ConflictingChangesOfRelationshipDetected(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_ConflictingChangesOfRelationshipDetected", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004E9C RID: 20124 RVA: 0x00167EA7 File Offset: 0x001660A7
		internal static string ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations");
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004E9D RID: 20125 RVA: 0x00167EB3 File Offset: 0x001660B3
		internal static string ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid");
			}
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x00167EC0 File Offset: 0x001660C0
		internal static string ObjectContext_ClientEntityRemovedFromStore(object p0)
		{
			return EntityRes.GetString("ObjectContext_ClientEntityRemovedFromStore", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004E9F RID: 20127 RVA: 0x00167EE3 File Offset: 0x001660E3
		internal static string ObjectContext_StoreEntityNotPresentInClient
		{
			get
			{
				return EntityRes.GetString("ObjectContext_StoreEntityNotPresentInClient");
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x00167EEF File Offset: 0x001660EF
		internal static string ObjectContext_InvalidConnectionString
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnectionString");
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004EA1 RID: 20129 RVA: 0x00167EFB File Offset: 0x001660FB
		internal static string ObjectContext_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnection");
			}
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x00167F08 File Offset: 0x00166108
		internal static string ObjectContext_InvalidDefaultContainerName(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidDefaultContainerName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x00167F2C File Offset: 0x0016612C
		internal static string ObjectContext_NthElementInAddedState(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementInAddedState", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00167F50 File Offset: 0x00166150
		internal static string ObjectContext_NthElementIsDuplicate(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00167F74 File Offset: 0x00166174
		internal static string ObjectContext_NthElementIsNull(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsNull", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00167F98 File Offset: 0x00166198
		internal static string ObjectContext_NthElementNotInObjectStateManager(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementNotInObjectStateManager", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x00167FBB File Offset: 0x001661BB
		internal static string ObjectContext_ObjectNotFound
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectNotFound");
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x00167FC7 File Offset: 0x001661C7
		internal static string ObjectContext_CannotDeleteEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDeleteEntityNotInObjectStateManager");
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004EA9 RID: 20137 RVA: 0x00167FD3 File Offset: 0x001661D3
		internal static string ObjectContext_CannotDetachEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDetachEntityNotInObjectStateManager");
			}
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x00167FE0 File Offset: 0x001661E0
		internal static string ObjectContext_EntitySetNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntitySetNotFoundForName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00168004 File Offset: 0x00166204
		internal static string ObjectContext_EntityContainerNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityContainerNotFoundForName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004EAC RID: 20140 RVA: 0x00168027 File Offset: 0x00166227
		internal static string ObjectContext_InvalidCommandTimeout
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidCommandTimeout");
			}
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00168034 File Offset: 0x00166234
		internal static string ObjectContext_NoMappingForEntityType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoMappingForEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x00168057 File Offset: 0x00166257
		internal static string ObjectContext_EntityAlreadyExistsInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityAlreadyExistsInObjectStateManager");
			}
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00168064 File Offset: 0x00166264
		internal static string ObjectContext_InvalidEntitySetInKey(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKey", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004EB0 RID: 20144 RVA: 0x00168093 File Offset: 0x00166293
		internal static string ObjectContext_CannotAttachEntityWithoutKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithoutKey");
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x0016809F File Offset: 0x0016629F
		internal static string ObjectContext_CannotAttachEntityWithTemporaryKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithTemporaryKey");
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x001680AB File Offset: 0x001662AB
		internal static string ObjectContext_EntitySetNameOrEntityKeyRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntitySetNameOrEntityKeyRequired");
			}
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x001680B8 File Offset: 0x001662B8
		internal static string ObjectContext_ExecuteFunctionTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionTypeMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x001680E0 File Offset: 0x001662E0
		internal static string ObjectContext_ExecuteFunctionCalledWithScalarFunction(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithScalarFunction", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x00168108 File Offset: 0x00166308
		internal static string ObjectContext_ExecuteFunctionCalledWithNonQueryFunction(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNonQueryFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0016812C File Offset: 0x0016632C
		internal static string ObjectContext_ExecuteFunctionCalledWithNullParameter(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNullParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x0016814F File Offset: 0x0016634F
		internal static string ObjectContext_ContainerQualifiedEntitySetNameRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ContainerQualifiedEntitySetNameRequired");
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004EB8 RID: 20152 RVA: 0x0016815B File Offset: 0x0016635B
		internal static string ObjectContext_CannotSetDefaultContainerName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotSetDefaultContainerName");
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x00168167 File Offset: 0x00166367
		internal static string ObjectContext_QualfiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_QualfiedEntitySetName");
			}
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00168174 File Offset: 0x00166374
		internal static string ObjectContext_EntitiesHaveDifferentType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_EntitiesHaveDifferentType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0016819C File Offset: 0x0016639C
		internal static string ObjectContext_EntityMustBeUnchangedOrModified(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModified", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x001681C0 File Offset: 0x001663C0
		internal static string ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x001681E4 File Offset: 0x001663E4
		internal static string ObjectContext_AcceptAllChangesFailure(object p0)
		{
			return EntityRes.GetString("ObjectContext_AcceptAllChangesFailure", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004EBE RID: 20158 RVA: 0x00168207 File Offset: 0x00166407
		internal static string ObjectContext_CommitWithConceptualNull
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CommitWithConceptualNull");
			}
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00168214 File Offset: 0x00166414
		internal static string ObjectContext_InvalidEntitySetOnEntity(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetOnEntity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x0016823C File Offset: 0x0016643C
		internal static string ObjectContext_InvalidObjectSetTypeForEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidObjectSetTypeForEntitySet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x00168268 File Offset: 0x00166468
		internal static string ObjectContext_InvalidEntitySetInKeyFromName(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKeyFromName", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x0016829C File Offset: 0x0016649C
		internal static string ObjectContext_ObjectDisposed
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectDisposed");
			}
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x001682A8 File Offset: 0x001664A8
		internal static string ObjectContext_CannotExplicitlyLoadDetachedRelationships(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotExplicitlyLoadDetachedRelationships", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x001682CC File Offset: 0x001664CC
		internal static string ObjectContext_CannotLoadReferencesUsingDifferentContext(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotLoadReferencesUsingDifferentContext", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x001682EF File Offset: 0x001664EF
		internal static string ObjectContext_SelectorExpressionMustBeMemberAccess
		{
			get
			{
				return EntityRes.GetString("ObjectContext_SelectorExpressionMustBeMemberAccess");
			}
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x001682FC File Offset: 0x001664FC
		internal static string ObjectContext_MultipleEntitySetsFoundInSingleContainer(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInSingleContainer", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x00168324 File Offset: 0x00166524
		internal static string ObjectContext_MultipleEntitySetsFoundInAllContainers(object p0)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInAllContainers", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x00168348 File Offset: 0x00166548
		internal static string ObjectContext_NoEntitySetFoundForType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoEntitySetFoundForType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x0016836C File Offset: 0x0016656C
		internal static string ObjectContext_EntityNotInObjectSet_Delete(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Delete", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x0016839C File Offset: 0x0016659C
		internal static string ObjectContext_EntityNotInObjectSet_Detach(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Detach", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x001683CB File Offset: 0x001665CB
		internal static string ObjectContext_InvalidEntityState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidEntityState");
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x001683D7 File Offset: 0x001665D7
		internal static string ObjectContext_InvalidRelationshipState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidRelationshipState");
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x001683E3 File Offset: 0x001665E3
		internal static string ObjectContext_EntityNotTrackedOrHasTempKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityNotTrackedOrHasTempKey");
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x001683EF File Offset: 0x001665EF
		internal static string ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues");
			}
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x001683FC File Offset: 0x001665FC
		internal static string ObjectContext_InvalidEntitySetForStoreQuery(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetForStoreQuery", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x00168428 File Offset: 0x00166628
		internal static string ObjectContext_InvalidTypeForStoreQuery(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidTypeForStoreQuery", new object[]
			{
				p0
			});
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x0016844C File Offset: 0x0016664C
		internal static string ObjectContext_TwoPropertiesMappedToSameColumn(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_TwoPropertiesMappedToSameColumn", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x00168473 File Offset: 0x00166673
		internal static string RelatedEnd_InvalidOwnerStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidOwnerStateForAttach");
			}
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x00168480 File Offset: 0x00166680
		internal static string RelatedEnd_InvalidNthElementNullForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementNullForAttach", new object[]
			{
				p0
			});
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x001684A4 File Offset: 0x001666A4
		internal static string RelatedEnd_InvalidNthElementContextForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementContextForAttach", new object[]
			{
				p0
			});
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x001684C8 File Offset: 0x001666C8
		internal static string RelatedEnd_InvalidNthElementStateForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementStateForAttach", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004ED6 RID: 20182 RVA: 0x001684EB File Offset: 0x001666EB
		internal static string RelatedEnd_InvalidEntityContextForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityContextForAttach");
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x001684F7 File Offset: 0x001666F7
		internal static string RelatedEnd_InvalidEntityStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityStateForAttach");
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x00168503 File Offset: 0x00166703
		internal static string RelatedEnd_UnableToAddEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddEntity");
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x0016850F File Offset: 0x0016670F
		internal static string RelatedEnd_UnableToRemoveEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToRemoveEntity");
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x0016851B File Offset: 0x0016671B
		internal static string RelatedEnd_UnableToAddRelationshipWithDeletedEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddRelationshipWithDeletedEntity");
			}
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x00168528 File Offset: 0x00166728
		internal static string RelatedEnd_CannotSerialize(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotSerialize", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x0016854C File Offset: 0x0016674C
		internal static string RelatedEnd_CannotAddToFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotAddToFixedSizeArray", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x00168570 File Offset: 0x00166770
		internal static string RelatedEnd_CannotRemoveFromFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotRemoveFromFixedSizeArray", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x00168593 File Offset: 0x00166793
		internal static string Materializer_PropertyIsNotNullable
		{
			get
			{
				return EntityRes.GetString("Materializer_PropertyIsNotNullable");
			}
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x001685A0 File Offset: 0x001667A0
		internal static string Materializer_PropertyIsNotNullableWithName(object p0)
		{
			return EntityRes.GetString("Materializer_PropertyIsNotNullableWithName", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x001685C4 File Offset: 0x001667C4
		internal static string Materializer_SetInvalidValue(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Materializer_SetInvalidValue", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x001685F4 File Offset: 0x001667F4
		internal static string Materializer_InvalidCastReference(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastReference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0016861C File Offset: 0x0016681C
		internal static string Materializer_InvalidCastNullable(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastNullable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x00168644 File Offset: 0x00166844
		internal static string Materializer_NullReferenceCast(object p0)
		{
			return EntityRes.GetString("Materializer_NullReferenceCast", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x00168668 File Offset: 0x00166868
		internal static string Materializer_RecyclingEntity(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Materializer_RecyclingEntity", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x00168698 File Offset: 0x00166898
		internal static string Materializer_AddedEntityAlreadyExists(object p0)
		{
			return EntityRes.GetString("Materializer_AddedEntityAlreadyExists", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x001686BB File Offset: 0x001668BB
		internal static string Materializer_CannotReEnumerateQueryResults
		{
			get
			{
				return EntityRes.GetString("Materializer_CannotReEnumerateQueryResults");
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x001686C7 File Offset: 0x001668C7
		internal static string Materializer_UnsupportedType
		{
			get
			{
				return EntityRes.GetString("Materializer_UnsupportedType");
			}
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x001686D4 File Offset: 0x001668D4
		internal static string Collections_NoRelationshipSetMatched(object p0)
		{
			return EntityRes.GetString("Collections_NoRelationshipSetMatched", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x001686F8 File Offset: 0x001668F8
		internal static string Collections_ExpectedCollectionGotReference(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Collections_ExpectedCollectionGotReference", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004EEA RID: 20202 RVA: 0x00168723 File Offset: 0x00166923
		internal static string Collections_InvalidEntityStateSource
		{
			get
			{
				return EntityRes.GetString("Collections_InvalidEntityStateSource");
			}
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x00168730 File Offset: 0x00166930
		internal static string Collections_InvalidEntityStateLoad(object p0)
		{
			return EntityRes.GetString("Collections_InvalidEntityStateLoad", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x00168754 File Offset: 0x00166954
		internal static string Collections_CannotFillTryDifferentMergeOption(object p0, object p1)
		{
			return EntityRes.GetString("Collections_CannotFillTryDifferentMergeOption", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x0016877B File Offset: 0x0016697B
		internal static string Collections_UnableToMergeCollections
		{
			get
			{
				return EntityRes.GetString("Collections_UnableToMergeCollections");
			}
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x00168788 File Offset: 0x00166988
		internal static string EntityReference_ExpectedReferenceGotCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityReference_ExpectedReferenceGotCollection", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x001687B4 File Offset: 0x001669B4
		internal static string EntityReference_CannotAddMoreThanOneEntityToEntityReference(object p0, object p1)
		{
			return EntityRes.GetString("EntityReference_CannotAddMoreThanOneEntityToEntityReference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x001687DB File Offset: 0x001669DB
		internal static string EntityReference_LessThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_LessThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x001687E7 File Offset: 0x001669E7
		internal static string EntityReference_MoreThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_MoreThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x001687F3 File Offset: 0x001669F3
		internal static string EntityReference_CannotChangeReferentialConstraintProperty
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotChangeReferentialConstraintProperty");
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x001687FF File Offset: 0x001669FF
		internal static string EntityReference_CannotSetSpecialKeys
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotSetSpecialKeys");
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x0016880B File Offset: 0x00166A0B
		internal static string EntityReference_EntityKeyValueMismatch
		{
			get
			{
				return EntityRes.GetString("EntityReference_EntityKeyValueMismatch");
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x00168817 File Offset: 0x00166A17
		internal static string RelatedEnd_RelatedEndNotFound
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_RelatedEndNotFound");
			}
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x00168824 File Offset: 0x00166A24
		internal static string RelatedEnd_RelatedEndNotAttachedToContext(object p0)
		{
			return EntityRes.GetString("RelatedEnd_RelatedEndNotAttachedToContext", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x00168847 File Offset: 0x00166A47
		internal static string RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd");
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x00168853 File Offset: 0x00166A53
		internal static string RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd");
			}
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x00168860 File Offset: 0x00166A60
		internal static string RelatedEnd_InvalidContainedType_Collection(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Collection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x00168888 File Offset: 0x00166A88
		internal static string RelatedEnd_InvalidContainedType_Reference(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Reference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x001688B0 File Offset: 0x00166AB0
		internal static string RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x001688D3 File Offset: 0x00166AD3
		internal static string RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts");
			}
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x001688E0 File Offset: 0x00166AE0
		internal static string RelatedEnd_MismatchedMergeOptionOnLoad(object p0)
		{
			return EntityRes.GetString("RelatedEnd_MismatchedMergeOptionOnLoad", new object[]
			{
				p0
			});
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x00168904 File Offset: 0x00166B04
		internal static string RelatedEnd_EntitySetIsNotValidForRelationship(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("RelatedEnd_EntitySetIsNotValidForRelationship", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x00168938 File Offset: 0x00166B38
		internal static string RelatedEnd_OwnerIsNull
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_OwnerIsNull");
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x00168944 File Offset: 0x00166B44
		internal static string RelationshipManager_UnableToRetrieveReferentialConstraintProperties
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnableToRetrieveReferentialConstraintProperties");
			}
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x00168950 File Offset: 0x00166B50
		internal static string RelationshipManager_InconsistentReferentialConstraintProperties(object p0, object p1)
		{
			return EntityRes.GetString("RelationshipManager_InconsistentReferentialConstraintProperties", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x00168977 File Offset: 0x00166B77
		internal static string RelationshipManager_CircularRelationshipsWithReferentialConstraints
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CircularRelationshipsWithReferentialConstraints");
			}
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x00168984 File Offset: 0x00166B84
		internal static string RelationshipManager_UnableToFindRelationshipTypeInMetadata(object p0)
		{
			return EntityRes.GetString("RelationshipManager_UnableToFindRelationshipTypeInMetadata", new object[]
			{
				p0
			});
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x001689A8 File Offset: 0x00166BA8
		internal static string RelationshipManager_InvalidTargetRole(object p0, object p1)
		{
			return EntityRes.GetString("RelationshipManager_InvalidTargetRole", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x001689CF File Offset: 0x00166BCF
		internal static string RelationshipManager_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNull");
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004F06 RID: 20230 RVA: 0x001689DB File Offset: 0x00166BDB
		internal static string RelationshipManager_InvalidRelationshipManagerOwner
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InvalidRelationshipManagerOwner");
			}
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x001689E8 File Offset: 0x00166BE8
		internal static string RelationshipManager_OwnerIsNotSourceType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("RelationshipManager_OwnerIsNotSourceType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004F08 RID: 20232 RVA: 0x00168A17 File Offset: 0x00166C17
		internal static string RelationshipManager_UnexpectedNullContext
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNullContext");
			}
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x00168A24 File Offset: 0x00166C24
		internal static string RelationshipManager_ReferenceAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_ReferenceAlreadyInitialized", new object[]
			{
				p0
			});
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x00168A48 File Offset: 0x00166C48
		internal static string RelationshipManager_RelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_RelationshipManagerAttached", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x00168A6B File Offset: 0x00166C6B
		internal static string RelationshipManager_InitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InitializeIsForDeserialization");
			}
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x00168A78 File Offset: 0x00166C78
		internal static string RelationshipManager_CollectionAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionAlreadyInitialized", new object[]
			{
				p0
			});
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x00168A9C File Offset: 0x00166C9C
		internal static string RelationshipManager_CollectionRelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionRelationshipManagerAttached", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x00168ABF File Offset: 0x00166CBF
		internal static string RelationshipManager_CollectionInitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CollectionInitializeIsForDeserialization");
			}
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x00168ACC File Offset: 0x00166CCC
		internal static string RelationshipManager_NavigationPropertyNotFound(object p0)
		{
			return EntityRes.GetString("RelationshipManager_NavigationPropertyNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004F10 RID: 20240 RVA: 0x00168AEF File Offset: 0x00166CEF
		internal static string RelationshipManager_CannotGetRelatEndForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CannotGetRelatEndForDetachedPocoEntity");
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x00168AFB File Offset: 0x00166CFB
		internal static string ObjectView_CannotReplacetheEntityorRow
		{
			get
			{
				return EntityRes.GetString("ObjectView_CannotReplacetheEntityorRow");
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x00168B07 File Offset: 0x00166D07
		internal static string ObjectView_IndexBasedInsertIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ObjectView_IndexBasedInsertIsNotSupported");
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004F13 RID: 20243 RVA: 0x00168B13 File Offset: 0x00166D13
		internal static string ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList");
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x00168B1F File Offset: 0x00166D1F
		internal static string ObjectView_AddNewOperationNotAllowedOnAbstractBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_AddNewOperationNotAllowedOnAbstractBindingList");
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x00168B2B File Offset: 0x00166D2B
		internal static string ObjectView_IncompatibleArgument
		{
			get
			{
				return EntityRes.GetString("ObjectView_IncompatibleArgument");
			}
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x00168B38 File Offset: 0x00166D38
		internal static string ObjectView_CannotResolveTheEntitySet(object p0)
		{
			return EntityRes.GetString("ObjectView_CannotResolveTheEntitySet", new object[]
			{
				p0
			});
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x00168B5C File Offset: 0x00166D5C
		internal static string CodeGen_ConstructorNoParameterless(object p0)
		{
			return EntityRes.GetString("CodeGen_ConstructorNoParameterless", new object[]
			{
				p0
			});
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x00168B7F File Offset: 0x00166D7F
		internal static string CodeGen_PropertyDeclaringTypeIsValueType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyDeclaringTypeIsValueType");
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x00168B8B File Offset: 0x00166D8B
		internal static string CodeGen_PropertyUnsupportedType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyUnsupportedType");
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x00168B97 File Offset: 0x00166D97
		internal static string CodeGen_PropertyIsIndexed
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsIndexed");
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x00168BA3 File Offset: 0x00166DA3
		internal static string CodeGen_PropertyIsStatic
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsStatic");
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x00168BAF File Offset: 0x00166DAF
		internal static string CodeGen_PropertyNoGetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoGetter");
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x00168BBB File Offset: 0x00166DBB
		internal static string CodeGen_PropertyNoSetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoSetter");
			}
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x00168BC8 File Offset: 0x00166DC8
		internal static string PocoEntityWrapper_UnableToSetFieldOrProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToSetFieldOrProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x00168BF0 File Offset: 0x00166DF0
		internal static string PocoEntityWrapper_UnexpectedTypeForNavigationProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnexpectedTypeForNavigationProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x00168C18 File Offset: 0x00166E18
		internal static string PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x00168C3F File Offset: 0x00166E3F
		internal static string GeneralQueryError
		{
			get
			{
				return EntityRes.GetString("GeneralQueryError");
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x00168C4B File Offset: 0x00166E4B
		internal static string CtxAlias
		{
			get
			{
				return EntityRes.GetString("CtxAlias");
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x00168C57 File Offset: 0x00166E57
		internal static string CtxAliasedNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxAliasedNamespaceImport");
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x00168C63 File Offset: 0x00166E63
		internal static string CtxAnd
		{
			get
			{
				return EntityRes.GetString("CtxAnd");
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x00168C6F File Offset: 0x00166E6F
		internal static string CtxAnyElement
		{
			get
			{
				return EntityRes.GetString("CtxAnyElement");
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x00168C7B File Offset: 0x00166E7B
		internal static string CtxApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxApplyClause");
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004F27 RID: 20263 RVA: 0x00168C87 File Offset: 0x00166E87
		internal static string CtxBetween
		{
			get
			{
				return EntityRes.GetString("CtxBetween");
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x00168C93 File Offset: 0x00166E93
		internal static string CtxCase
		{
			get
			{
				return EntityRes.GetString("CtxCase");
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004F29 RID: 20265 RVA: 0x00168C9F File Offset: 0x00166E9F
		internal static string CtxCaseElse
		{
			get
			{
				return EntityRes.GetString("CtxCaseElse");
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004F2A RID: 20266 RVA: 0x00168CAB File Offset: 0x00166EAB
		internal static string CtxCaseWhenThen
		{
			get
			{
				return EntityRes.GetString("CtxCaseWhenThen");
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004F2B RID: 20267 RVA: 0x00168CB7 File Offset: 0x00166EB7
		internal static string CtxCast
		{
			get
			{
				return EntityRes.GetString("CtxCast");
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004F2C RID: 20268 RVA: 0x00168CC3 File Offset: 0x00166EC3
		internal static string CtxCollatedOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxCollatedOrderByClauseItem");
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004F2D RID: 20269 RVA: 0x00168CCF File Offset: 0x00166ECF
		internal static string CtxCollectionTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxCollectionTypeDefinition");
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x00168CDB File Offset: 0x00166EDB
		internal static string CtxCommandExpression
		{
			get
			{
				return EntityRes.GetString("CtxCommandExpression");
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004F2F RID: 20271 RVA: 0x00168CE7 File Offset: 0x00166EE7
		internal static string CtxCreateRef
		{
			get
			{
				return EntityRes.GetString("CtxCreateRef");
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004F30 RID: 20272 RVA: 0x00168CF3 File Offset: 0x00166EF3
		internal static string CtxDeref
		{
			get
			{
				return EntityRes.GetString("CtxDeref");
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x00168CFF File Offset: 0x00166EFF
		internal static string CtxDivide
		{
			get
			{
				return EntityRes.GetString("CtxDivide");
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004F32 RID: 20274 RVA: 0x00168D0B File Offset: 0x00166F0B
		internal static string CtxElement
		{
			get
			{
				return EntityRes.GetString("CtxElement");
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x00168D17 File Offset: 0x00166F17
		internal static string CtxEquals
		{
			get
			{
				return EntityRes.GetString("CtxEquals");
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x00168D23 File Offset: 0x00166F23
		internal static string CtxEscapedIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxEscapedIdentifier");
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004F35 RID: 20277 RVA: 0x00168D2F File Offset: 0x00166F2F
		internal static string CtxExcept
		{
			get
			{
				return EntityRes.GetString("CtxExcept");
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x00168D3B File Offset: 0x00166F3B
		internal static string CtxExists
		{
			get
			{
				return EntityRes.GetString("CtxExists");
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x00168D47 File Offset: 0x00166F47
		internal static string CtxExpressionList
		{
			get
			{
				return EntityRes.GetString("CtxExpressionList");
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004F38 RID: 20280 RVA: 0x00168D53 File Offset: 0x00166F53
		internal static string CtxFlatten
		{
			get
			{
				return EntityRes.GetString("CtxFlatten");
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x00168D5F File Offset: 0x00166F5F
		internal static string CtxFromApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxFromApplyClause");
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x00168D6B File Offset: 0x00166F6B
		internal static string CtxFromClause
		{
			get
			{
				return EntityRes.GetString("CtxFromClause");
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004F3B RID: 20283 RVA: 0x00168D77 File Offset: 0x00166F77
		internal static string CtxFromClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseItem");
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x00168D83 File Offset: 0x00166F83
		internal static string CtxFromClauseList
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseList");
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x00168D8F File Offset: 0x00166F8F
		internal static string CtxFromJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxFromJoinClause");
			}
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x00168D9C File Offset: 0x00166F9C
		internal static string CtxFunction(object p0)
		{
			return EntityRes.GetString("CtxFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x00168DBF File Offset: 0x00166FBF
		internal static string CtxFunctionDefinition
		{
			get
			{
				return EntityRes.GetString("CtxFunctionDefinition");
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x00168DCB File Offset: 0x00166FCB
		internal static string CtxGreaterThan
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThan");
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x00168DD7 File Offset: 0x00166FD7
		internal static string CtxGreaterThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThanEqual");
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x00168DE3 File Offset: 0x00166FE3
		internal static string CtxGroupByClause
		{
			get
			{
				return EntityRes.GetString("CtxGroupByClause");
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004F43 RID: 20291 RVA: 0x00168DEF File Offset: 0x00166FEF
		internal static string CtxGroupPartition
		{
			get
			{
				return EntityRes.GetString("CtxGroupPartition");
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x00168DFB File Offset: 0x00166FFB
		internal static string CtxHavingClause
		{
			get
			{
				return EntityRes.GetString("CtxHavingClause");
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x00168E07 File Offset: 0x00167007
		internal static string CtxIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxIdentifier");
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x00168E13 File Offset: 0x00167013
		internal static string CtxIn
		{
			get
			{
				return EntityRes.GetString("CtxIn");
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004F47 RID: 20295 RVA: 0x00168E1F File Offset: 0x0016701F
		internal static string CtxIntersect
		{
			get
			{
				return EntityRes.GetString("CtxIntersect");
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x00168E2B File Offset: 0x0016702B
		internal static string CtxIsNotNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNotNull");
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004F49 RID: 20297 RVA: 0x00168E37 File Offset: 0x00167037
		internal static string CtxIsNotOf
		{
			get
			{
				return EntityRes.GetString("CtxIsNotOf");
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x00168E43 File Offset: 0x00167043
		internal static string CtxIsNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNull");
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x00168E4F File Offset: 0x0016704F
		internal static string CtxIsOf
		{
			get
			{
				return EntityRes.GetString("CtxIsOf");
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x00168E5B File Offset: 0x0016705B
		internal static string CtxJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinClause");
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004F4D RID: 20301 RVA: 0x00168E67 File Offset: 0x00167067
		internal static string CtxJoinOnClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinOnClause");
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004F4E RID: 20302 RVA: 0x00168E73 File Offset: 0x00167073
		internal static string CtxKey
		{
			get
			{
				return EntityRes.GetString("CtxKey");
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004F4F RID: 20303 RVA: 0x00168E7F File Offset: 0x0016707F
		internal static string CtxLessThan
		{
			get
			{
				return EntityRes.GetString("CtxLessThan");
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x00168E8B File Offset: 0x0016708B
		internal static string CtxLessThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxLessThanEqual");
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x00168E97 File Offset: 0x00167097
		internal static string CtxLike
		{
			get
			{
				return EntityRes.GetString("CtxLike");
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x00168EA3 File Offset: 0x001670A3
		internal static string CtxLimitSubClause
		{
			get
			{
				return EntityRes.GetString("CtxLimitSubClause");
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x00168EAF File Offset: 0x001670AF
		internal static string CtxLiteral
		{
			get
			{
				return EntityRes.GetString("CtxLiteral");
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x00168EBB File Offset: 0x001670BB
		internal static string CtxMemberAccess
		{
			get
			{
				return EntityRes.GetString("CtxMemberAccess");
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x00168EC7 File Offset: 0x001670C7
		internal static string CtxMethod
		{
			get
			{
				return EntityRes.GetString("CtxMethod");
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x00168ED3 File Offset: 0x001670D3
		internal static string CtxMinus
		{
			get
			{
				return EntityRes.GetString("CtxMinus");
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x00168EDF File Offset: 0x001670DF
		internal static string CtxModulus
		{
			get
			{
				return EntityRes.GetString("CtxModulus");
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x00168EEB File Offset: 0x001670EB
		internal static string CtxMultiply
		{
			get
			{
				return EntityRes.GetString("CtxMultiply");
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x00168EF7 File Offset: 0x001670F7
		internal static string CtxMultisetCtor
		{
			get
			{
				return EntityRes.GetString("CtxMultisetCtor");
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004F5A RID: 20314 RVA: 0x00168F03 File Offset: 0x00167103
		internal static string CtxNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImport");
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x00168F0F File Offset: 0x0016710F
		internal static string CtxNamespaceImportList
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImportList");
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x00168F1B File Offset: 0x0016711B
		internal static string CtxNavigate
		{
			get
			{
				return EntityRes.GetString("CtxNavigate");
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004F5D RID: 20317 RVA: 0x00168F27 File Offset: 0x00167127
		internal static string CtxNot
		{
			get
			{
				return EntityRes.GetString("CtxNot");
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004F5E RID: 20318 RVA: 0x00168F33 File Offset: 0x00167133
		internal static string CtxNotBetween
		{
			get
			{
				return EntityRes.GetString("CtxNotBetween");
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004F5F RID: 20319 RVA: 0x00168F3F File Offset: 0x0016713F
		internal static string CtxNotEqual
		{
			get
			{
				return EntityRes.GetString("CtxNotEqual");
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004F60 RID: 20320 RVA: 0x00168F4B File Offset: 0x0016714B
		internal static string CtxNotIn
		{
			get
			{
				return EntityRes.GetString("CtxNotIn");
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004F61 RID: 20321 RVA: 0x00168F57 File Offset: 0x00167157
		internal static string CtxNotLike
		{
			get
			{
				return EntityRes.GetString("CtxNotLike");
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x00168F63 File Offset: 0x00167163
		internal static string CtxNullLiteral
		{
			get
			{
				return EntityRes.GetString("CtxNullLiteral");
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x00168F6F File Offset: 0x0016716F
		internal static string CtxOfType
		{
			get
			{
				return EntityRes.GetString("CtxOfType");
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004F64 RID: 20324 RVA: 0x00168F7B File Offset: 0x0016717B
		internal static string CtxOfTypeOnly
		{
			get
			{
				return EntityRes.GetString("CtxOfTypeOnly");
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x00168F87 File Offset: 0x00167187
		internal static string CtxOr
		{
			get
			{
				return EntityRes.GetString("CtxOr");
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004F66 RID: 20326 RVA: 0x00168F93 File Offset: 0x00167193
		internal static string CtxOrderByClause
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClause");
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x00168F9F File Offset: 0x0016719F
		internal static string CtxOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClauseItem");
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x00168FAB File Offset: 0x001671AB
		internal static string CtxOverlaps
		{
			get
			{
				return EntityRes.GetString("CtxOverlaps");
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004F69 RID: 20329 RVA: 0x00168FB7 File Offset: 0x001671B7
		internal static string CtxParen
		{
			get
			{
				return EntityRes.GetString("CtxParen");
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004F6A RID: 20330 RVA: 0x00168FC3 File Offset: 0x001671C3
		internal static string CtxPlus
		{
			get
			{
				return EntityRes.GetString("CtxPlus");
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004F6B RID: 20331 RVA: 0x00168FCF File Offset: 0x001671CF
		internal static string CtxTypeNameWithTypeSpec
		{
			get
			{
				return EntityRes.GetString("CtxTypeNameWithTypeSpec");
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004F6C RID: 20332 RVA: 0x00168FDB File Offset: 0x001671DB
		internal static string CtxQueryExpression
		{
			get
			{
				return EntityRes.GetString("CtxQueryExpression");
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004F6D RID: 20333 RVA: 0x00168FE7 File Offset: 0x001671E7
		internal static string CtxQueryStatement
		{
			get
			{
				return EntityRes.GetString("CtxQueryStatement");
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x00168FF3 File Offset: 0x001671F3
		internal static string CtxRef
		{
			get
			{
				return EntityRes.GetString("CtxRef");
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004F6F RID: 20335 RVA: 0x00168FFF File Offset: 0x001671FF
		internal static string CtxRefTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRefTypeDefinition");
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x0016900B File Offset: 0x0016720B
		internal static string CtxRelationship
		{
			get
			{
				return EntityRes.GetString("CtxRelationship");
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004F71 RID: 20337 RVA: 0x00169017 File Offset: 0x00167217
		internal static string CtxRelationshipList
		{
			get
			{
				return EntityRes.GetString("CtxRelationshipList");
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x00169023 File Offset: 0x00167223
		internal static string CtxRowCtor
		{
			get
			{
				return EntityRes.GetString("CtxRowCtor");
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004F73 RID: 20339 RVA: 0x0016902F File Offset: 0x0016722F
		internal static string CtxRowTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRowTypeDefinition");
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x0016903B File Offset: 0x0016723B
		internal static string CtxSelectRowClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectRowClause");
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004F75 RID: 20341 RVA: 0x00169047 File Offset: 0x00167247
		internal static string CtxSelectValueClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectValueClause");
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x00169053 File Offset: 0x00167253
		internal static string CtxSet
		{
			get
			{
				return EntityRes.GetString("CtxSet");
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004F77 RID: 20343 RVA: 0x0016905F File Offset: 0x0016725F
		internal static string CtxSimpleIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxSimpleIdentifier");
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x0016906B File Offset: 0x0016726B
		internal static string CtxSkipSubClause
		{
			get
			{
				return EntityRes.GetString("CtxSkipSubClause");
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004F79 RID: 20345 RVA: 0x00169077 File Offset: 0x00167277
		internal static string CtxTopSubClause
		{
			get
			{
				return EntityRes.GetString("CtxTopSubClause");
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004F7A RID: 20346 RVA: 0x00169083 File Offset: 0x00167283
		internal static string CtxTreat
		{
			get
			{
				return EntityRes.GetString("CtxTreat");
			}
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x00169090 File Offset: 0x00167290
		internal static string CtxTypeCtor(object p0)
		{
			return EntityRes.GetString("CtxTypeCtor", new object[]
			{
				p0
			});
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x001690B3 File Offset: 0x001672B3
		internal static string CtxTypeName
		{
			get
			{
				return EntityRes.GetString("CtxTypeName");
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004F7D RID: 20349 RVA: 0x001690BF File Offset: 0x001672BF
		internal static string CtxUnaryMinus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryMinus");
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004F7E RID: 20350 RVA: 0x001690CB File Offset: 0x001672CB
		internal static string CtxUnaryPlus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryPlus");
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x001690D7 File Offset: 0x001672D7
		internal static string CtxUnion
		{
			get
			{
				return EntityRes.GetString("CtxUnion");
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004F80 RID: 20352 RVA: 0x001690E3 File Offset: 0x001672E3
		internal static string CtxUnionAll
		{
			get
			{
				return EntityRes.GetString("CtxUnionAll");
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x001690EF File Offset: 0x001672EF
		internal static string CtxWhereClause
		{
			get
			{
				return EntityRes.GetString("CtxWhereClause");
			}
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x001690FC File Offset: 0x001672FC
		internal static string CannotConvertNumericLiteral(object p0, object p1)
		{
			return EntityRes.GetString("CannotConvertNumericLiteral", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x00169123 File Offset: 0x00167323
		internal static string GenericSyntaxError
		{
			get
			{
				return EntityRes.GetString("GenericSyntaxError");
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x0016912F File Offset: 0x0016732F
		internal static string InFromClause
		{
			get
			{
				return EntityRes.GetString("InFromClause");
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x0016913B File Offset: 0x0016733B
		internal static string InGroupClause
		{
			get
			{
				return EntityRes.GetString("InGroupClause");
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x00169147 File Offset: 0x00167347
		internal static string InRowCtor
		{
			get
			{
				return EntityRes.GetString("InRowCtor");
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x00169153 File Offset: 0x00167353
		internal static string InSelectProjectionList
		{
			get
			{
				return EntityRes.GetString("InSelectProjectionList");
			}
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x00169160 File Offset: 0x00167360
		internal static string InvalidAliasName(object p0)
		{
			return EntityRes.GetString("InvalidAliasName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x00169183 File Offset: 0x00167383
		internal static string InvalidEmptyIdentifier
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyIdentifier");
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x0016918F File Offset: 0x0016738F
		internal static string InvalidEmptyQuery
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyQuery");
			}
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x0016919C File Offset: 0x0016739C
		internal static string InvalidEscapedIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifier", new object[]
			{
				p0
			});
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x001691C0 File Offset: 0x001673C0
		internal static string InvalidEscapedIdentifierUnbalanced(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifierUnbalanced", new object[]
			{
				p0
			});
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004F8D RID: 20365 RVA: 0x001691E3 File Offset: 0x001673E3
		internal static string InvalidOperatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidOperatorSymbol");
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x001691EF File Offset: 0x001673EF
		internal static string InvalidPunctuatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidPunctuatorSymbol");
			}
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x001691FC File Offset: 0x001673FC
		internal static string InvalidSimpleIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifier", new object[]
			{
				p0
			});
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x00169220 File Offset: 0x00167420
		internal static string InvalidSimpleIdentifierNonASCII(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifierNonASCII", new object[]
			{
				p0
			});
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004F91 RID: 20369 RVA: 0x00169243 File Offset: 0x00167443
		internal static string LocalizedCollection
		{
			get
			{
				return EntityRes.GetString("LocalizedCollection");
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x0016924F File Offset: 0x0016744F
		internal static string LocalizedColumn
		{
			get
			{
				return EntityRes.GetString("LocalizedColumn");
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x0016925B File Offset: 0x0016745B
		internal static string LocalizedComplex
		{
			get
			{
				return EntityRes.GetString("LocalizedComplex");
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x00169267 File Offset: 0x00167467
		internal static string LocalizedEntity
		{
			get
			{
				return EntityRes.GetString("LocalizedEntity");
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004F95 RID: 20373 RVA: 0x00169273 File Offset: 0x00167473
		internal static string LocalizedEntityContainerExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedEntityContainerExpression");
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004F96 RID: 20374 RVA: 0x0016927F File Offset: 0x0016747F
		internal static string LocalizedFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedFunction");
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004F97 RID: 20375 RVA: 0x0016928B File Offset: 0x0016748B
		internal static string LocalizedInlineFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedInlineFunction");
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x00169297 File Offset: 0x00167497
		internal static string LocalizedKeyword
		{
			get
			{
				return EntityRes.GetString("LocalizedKeyword");
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x001692A3 File Offset: 0x001674A3
		internal static string LocalizedLeft
		{
			get
			{
				return EntityRes.GetString("LocalizedLeft");
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x001692AF File Offset: 0x001674AF
		internal static string LocalizedLine
		{
			get
			{
				return EntityRes.GetString("LocalizedLine");
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x001692BB File Offset: 0x001674BB
		internal static string LocalizedMetadataMemberExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedMetadataMemberExpression");
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x001692C7 File Offset: 0x001674C7
		internal static string LocalizedNamespace
		{
			get
			{
				return EntityRes.GetString("LocalizedNamespace");
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004F9D RID: 20381 RVA: 0x001692D3 File Offset: 0x001674D3
		internal static string LocalizedNear
		{
			get
			{
				return EntityRes.GetString("LocalizedNear");
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004F9E RID: 20382 RVA: 0x001692DF File Offset: 0x001674DF
		internal static string LocalizedPrimitive
		{
			get
			{
				return EntityRes.GetString("LocalizedPrimitive");
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004F9F RID: 20383 RVA: 0x001692EB File Offset: 0x001674EB
		internal static string LocalizedReference
		{
			get
			{
				return EntityRes.GetString("LocalizedReference");
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004FA0 RID: 20384 RVA: 0x001692F7 File Offset: 0x001674F7
		internal static string LocalizedRight
		{
			get
			{
				return EntityRes.GetString("LocalizedRight");
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004FA1 RID: 20385 RVA: 0x00169303 File Offset: 0x00167503
		internal static string LocalizedRow
		{
			get
			{
				return EntityRes.GetString("LocalizedRow");
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004FA2 RID: 20386 RVA: 0x0016930F File Offset: 0x0016750F
		internal static string LocalizedTerm
		{
			get
			{
				return EntityRes.GetString("LocalizedTerm");
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004FA3 RID: 20387 RVA: 0x0016931B File Offset: 0x0016751B
		internal static string LocalizedType
		{
			get
			{
				return EntityRes.GetString("LocalizedType");
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004FA4 RID: 20388 RVA: 0x00169327 File Offset: 0x00167527
		internal static string LocalizedEnumMember
		{
			get
			{
				return EntityRes.GetString("LocalizedEnumMember");
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004FA5 RID: 20389 RVA: 0x00169333 File Offset: 0x00167533
		internal static string LocalizedValueExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedValueExpression");
			}
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x00169340 File Offset: 0x00167540
		internal static string AliasNameAlreadyUsed(object p0)
		{
			return EntityRes.GetString("AliasNameAlreadyUsed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x00169363 File Offset: 0x00167563
		internal static string AmbiguousFunctionArguments
		{
			get
			{
				return EntityRes.GetString("AmbiguousFunctionArguments");
			}
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x00169370 File Offset: 0x00167570
		internal static string AmbiguousMetadataMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AmbiguousMetadataMemberName", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x0016939C File Offset: 0x0016759C
		internal static string ArgumentTypesAreIncompatible(object p0, object p1)
		{
			return EntityRes.GetString("ArgumentTypesAreIncompatible", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004FAA RID: 20394 RVA: 0x001693C3 File Offset: 0x001675C3
		internal static string BetweenLimitsCannotBeUntypedNulls
		{
			get
			{
				return EntityRes.GetString("BetweenLimitsCannotBeUntypedNulls");
			}
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x001693D0 File Offset: 0x001675D0
		internal static string BetweenLimitsTypesAreNotCompatible(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotCompatible", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x001693F8 File Offset: 0x001675F8
		internal static string BetweenLimitsTypesAreNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotOrderComparable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x00169420 File Offset: 0x00167620
		internal static string BetweenValueIsNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenValueIsNotOrderComparable", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x00169447 File Offset: 0x00167647
		internal static string CannotCreateEmptyMultiset
		{
			get
			{
				return EntityRes.GetString("CannotCreateEmptyMultiset");
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004FAF RID: 20399 RVA: 0x00169453 File Offset: 0x00167653
		internal static string CannotCreateMultisetofNulls
		{
			get
			{
				return EntityRes.GetString("CannotCreateMultisetofNulls");
			}
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x00169460 File Offset: 0x00167660
		internal static string CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("CannotInstantiateAbstractType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x00169484 File Offset: 0x00167684
		internal static string CannotResolveNameToTypeOrFunction(object p0)
		{
			return EntityRes.GetString("CannotResolveNameToTypeOrFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x001694A7 File Offset: 0x001676A7
		internal static string ConcatBuiltinNotSupported
		{
			get
			{
				return EntityRes.GetString("ConcatBuiltinNotSupported");
			}
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x001694B4 File Offset: 0x001676B4
		internal static string CouldNotResolveIdentifier(object p0)
		{
			return EntityRes.GetString("CouldNotResolveIdentifier", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x001694D8 File Offset: 0x001676D8
		internal static string CreateRefTypeIdentifierMustBeASubOrSuperType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustBeASubOrSuperType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00169500 File Offset: 0x00167700
		internal static string CreateRefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustSpecifyAnEntityType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00169528 File Offset: 0x00167728
		internal static string DeRefArgIsNotOfRefType(object p0)
		{
			return EntityRes.GetString("DeRefArgIsNotOfRefType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0016954C File Offset: 0x0016774C
		internal static string DuplicatedInlineFunctionOverload(object p0)
		{
			return EntityRes.GetString("DuplicatedInlineFunctionOverload", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x0016956F File Offset: 0x0016776F
		internal static string ElementOperatorIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ElementOperatorIsNotSupported");
			}
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0016957C File Offset: 0x0016777C
		internal static string MemberDoesNotBelongToEntityContainer(object p0, object p1)
		{
			return EntityRes.GetString("MemberDoesNotBelongToEntityContainer", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x001695A3 File Offset: 0x001677A3
		internal static string ExpressionCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ExpressionCannotBeNull");
			}
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x001695B0 File Offset: 0x001677B0
		internal static string OfTypeExpressionElementTypeMustBeEntityType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeEntityType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x001695D8 File Offset: 0x001677D8
		internal static string OfTypeExpressionElementTypeMustBeNominalType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeNominalType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004FBD RID: 20413 RVA: 0x001695FF File Offset: 0x001677FF
		internal static string ExpressionMustBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeCollection");
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x0016960B File Offset: 0x0016780B
		internal static string ExpressionMustBeNumericType
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeNumericType");
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004FBF RID: 20415 RVA: 0x00169617 File Offset: 0x00167817
		internal static string ExpressionTypeMustBeBoolean
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeBoolean");
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004FC0 RID: 20416 RVA: 0x00169623 File Offset: 0x00167823
		internal static string ExpressionTypeMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeEqualComparable");
			}
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x00169630 File Offset: 0x00167830
		internal static string ExpressionTypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeEntityType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0016965C File Offset: 0x0016785C
		internal static string ExpressionTypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeNominalType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004FC3 RID: 20419 RVA: 0x00169687 File Offset: 0x00167887
		internal static string ExpressionTypeMustNotBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustNotBeCollection");
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004FC4 RID: 20420 RVA: 0x00169693 File Offset: 0x00167893
		internal static string ExprIsNotValidEntitySetForCreateRef
		{
			get
			{
				return EntityRes.GetString("ExprIsNotValidEntitySetForCreateRef");
			}
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x001696A0 File Offset: 0x001678A0
		internal static string FailedToResolveAggregateFunction(object p0)
		{
			return EntityRes.GetString("FailedToResolveAggregateFunction", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x001696C4 File Offset: 0x001678C4
		internal static string GeneralExceptionAsQueryInnerException(object p0)
		{
			return EntityRes.GetString("GeneralExceptionAsQueryInnerException", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x001696E7 File Offset: 0x001678E7
		internal static string GroupingKeysMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("GroupingKeysMustBeEqualComparable");
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x001696F3 File Offset: 0x001678F3
		internal static string GroupPartitionOutOfContext
		{
			get
			{
				return EntityRes.GetString("GroupPartitionOutOfContext");
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x001696FF File Offset: 0x001678FF
		internal static string HavingRequiresGroupClause
		{
			get
			{
				return EntityRes.GetString("HavingRequiresGroupClause");
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004FCA RID: 20426 RVA: 0x0016970B File Offset: 0x0016790B
		internal static string ImcompatibleCreateRefKeyElementType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyElementType");
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x00169717 File Offset: 0x00167917
		internal static string ImcompatibleCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyType");
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x00169723 File Offset: 0x00167923
		internal static string InnerJoinMustHaveOnPredicate
		{
			get
			{
				return EntityRes.GetString("InnerJoinMustHaveOnPredicate");
			}
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x00169730 File Offset: 0x00167930
		internal static string InvalidAssociationTypeForUnion(object p0)
		{
			return EntityRes.GetString("InvalidAssociationTypeForUnion", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x00169753 File Offset: 0x00167953
		internal static string InvalidCaseResultTypes
		{
			get
			{
				return EntityRes.GetString("InvalidCaseResultTypes");
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x0016975F File Offset: 0x0016795F
		internal static string InvalidCaseWhenThenNullType
		{
			get
			{
				return EntityRes.GetString("InvalidCaseWhenThenNullType");
			}
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0016976C File Offset: 0x0016796C
		internal static string InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("InvalidCast", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x00169793 File Offset: 0x00167993
		internal static string InvalidCastExpressionType
		{
			get
			{
				return EntityRes.GetString("InvalidCastExpressionType");
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x0016979F File Offset: 0x0016799F
		internal static string InvalidCastType
		{
			get
			{
				return EntityRes.GetString("InvalidCastType");
			}
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x001697AC File Offset: 0x001679AC
		internal static string InvalidComplexType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidComplexType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x001697DB File Offset: 0x001679DB
		internal static string InvalidCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("InvalidCreateRefKeyType");
			}
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x001697E8 File Offset: 0x001679E8
		internal static string InvalidCtorArgumentType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidCtorArgumentType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x00169814 File Offset: 0x00167A14
		internal static string InvalidCtorUseOnType(object p0)
		{
			return EntityRes.GetString("InvalidCtorUseOnType", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x00169838 File Offset: 0x00167A38
		internal static string InvalidDateTimeOffsetLiteral(object p0)
		{
			return EntityRes.GetString("InvalidDateTimeOffsetLiteral", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0016985C File Offset: 0x00167A5C
		internal static string InvalidDay(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDay", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x00169884 File Offset: 0x00167A84
		internal static string InvalidDayInMonth(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDayInMonth", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x001698B0 File Offset: 0x00167AB0
		internal static string InvalidDeRefProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDeRefProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004FDB RID: 20443 RVA: 0x001698DB File Offset: 0x00167ADB
		internal static string InvalidDistinctArgumentInCtor
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInCtor");
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004FDC RID: 20444 RVA: 0x001698E7 File Offset: 0x00167AE7
		internal static string InvalidDistinctArgumentInNonAggFunction
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInNonAggFunction");
			}
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x001698F4 File Offset: 0x00167AF4
		internal static string InvalidEntityRootTypeArgument(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityRootTypeArgument", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0016991C File Offset: 0x00167B1C
		internal static string InvalidEntityTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidEntityTypeArgument", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0016994C File Offset: 0x00167B4C
		internal static string InvalidExpressionResolutionClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidExpressionResolutionClass", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004FE0 RID: 20448 RVA: 0x00169973 File Offset: 0x00167B73
		internal static string InvalidFlattenArgument
		{
			get
			{
				return EntityRes.GetString("InvalidFlattenArgument");
			}
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x00169980 File Offset: 0x00167B80
		internal static string InvalidGroupIdentifierReference(object p0)
		{
			return EntityRes.GetString("InvalidGroupIdentifierReference", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x001699A4 File Offset: 0x00167BA4
		internal static string InvalidHour(object p0, object p1)
		{
			return EntityRes.GetString("InvalidHour", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x001699CC File Offset: 0x00167BCC
		internal static string InvalidImplicitRelationshipFromEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipFromEnd", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x001699F0 File Offset: 0x00167BF0
		internal static string InvalidImplicitRelationshipToEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipToEnd", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x00169A14 File Offset: 0x00167C14
		internal static string InvalidInExprArgs(object p0, object p1)
		{
			return EntityRes.GetString("InvalidInExprArgs", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x00169A3B File Offset: 0x00167C3B
		internal static string InvalidJoinLeftCorrelation
		{
			get
			{
				return EntityRes.GetString("InvalidJoinLeftCorrelation");
			}
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x00169A48 File Offset: 0x00167C48
		internal static string InvalidKeyArgument(object p0)
		{
			return EntityRes.GetString("InvalidKeyArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x00169A6C File Offset: 0x00167C6C
		internal static string InvalidKeyTypeForCollation(object p0)
		{
			return EntityRes.GetString("InvalidKeyTypeForCollation", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x00169A90 File Offset: 0x00167C90
		internal static string InvalidLiteralFormat(object p0, object p1)
		{
			return EntityRes.GetString("InvalidLiteralFormat", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x00169AB7 File Offset: 0x00167CB7
		internal static string InvalidMetadataMemberName
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataMemberName");
			}
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x00169AC4 File Offset: 0x00167CC4
		internal static string InvalidMinute(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMinute", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x00169AEB File Offset: 0x00167CEB
		internal static string InvalidModeForWithRelationshipClause
		{
			get
			{
				return EntityRes.GetString("InvalidModeForWithRelationshipClause");
			}
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x00169AF8 File Offset: 0x00167CF8
		internal static string InvalidMonth(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMonth", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x00169B1F File Offset: 0x00167D1F
		internal static string InvalidNamespaceAlias
		{
			get
			{
				return EntityRes.GetString("InvalidNamespaceAlias");
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x00169B2B File Offset: 0x00167D2B
		internal static string InvalidNullArithmetic
		{
			get
			{
				return EntityRes.GetString("InvalidNullArithmetic");
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06004FF0 RID: 20464 RVA: 0x00169B37 File Offset: 0x00167D37
		internal static string InvalidNullComparison
		{
			get
			{
				return EntityRes.GetString("InvalidNullComparison");
			}
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x00169B44 File Offset: 0x00167D44
		internal static string InvalidNullLiteralForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidNullLiteralForNonNullableMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x00169B6C File Offset: 0x00167D6C
		internal static string InvalidParameterFormat(object p0)
		{
			return EntityRes.GetString("InvalidParameterFormat", new object[]
			{
				p0
			});
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x00169B90 File Offset: 0x00167D90
		internal static string InvalidPlaceholderRootTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidPlaceholderRootTypeArgument", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x00169BC0 File Offset: 0x00167DC0
		internal static string InvalidPlaceholderTypeArgument(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("InvalidPlaceholderTypeArgument", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4,
				p5
			});
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x00169BF9 File Offset: 0x00167DF9
		internal static string InvalidPredicateForCrossJoin
		{
			get
			{
				return EntityRes.GetString("InvalidPredicateForCrossJoin");
			}
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x00169C08 File Offset: 0x00167E08
		internal static string InvalidRelationshipMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipMember", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x00169C30 File Offset: 0x00167E30
		internal static string InvalidMetadataMemberClassResolution(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidMetadataMemberClassResolution", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x00169C5C File Offset: 0x00167E5C
		internal static string InvalidRootComplexType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootComplexType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x00169C84 File Offset: 0x00167E84
		internal static string InvalidRootRowType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootRowType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x00169CAC File Offset: 0x00167EAC
		internal static string InvalidRowType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidRowType", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x00169CDC File Offset: 0x00167EDC
		internal static string InvalidSecond(object p0, object p1)
		{
			return EntityRes.GetString("InvalidSecond", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x00169D03 File Offset: 0x00167F03
		internal static string InvalidSelectValueAliasedExpression
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueAliasedExpression");
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004FFD RID: 20477 RVA: 0x00169D0F File Offset: 0x00167F0F
		internal static string InvalidSelectValueList
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueList");
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004FFE RID: 20478 RVA: 0x00169D1B File Offset: 0x00167F1B
		internal static string InvalidTypeForWithRelationshipClause
		{
			get
			{
				return EntityRes.GetString("InvalidTypeForWithRelationshipClause");
			}
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x00169D28 File Offset: 0x00167F28
		internal static string InvalidUnarySetOpArgument(object p0)
		{
			return EntityRes.GetString("InvalidUnarySetOpArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x00169D4C File Offset: 0x00167F4C
		internal static string InvalidUnsignedTypeForUnaryMinusOperation(object p0)
		{
			return EntityRes.GetString("InvalidUnsignedTypeForUnaryMinusOperation", new object[]
			{
				p0
			});
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x00169D70 File Offset: 0x00167F70
		internal static string InvalidYear(object p0, object p1)
		{
			return EntityRes.GetString("InvalidYear", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x00169D98 File Offset: 0x00167F98
		internal static string InvalidWithRelationshipTargetEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidWithRelationshipTargetEndMultiplicity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x00169DC0 File Offset: 0x00167FC0
		internal static string InvalidQueryResultType(object p0)
		{
			return EntityRes.GetString("InvalidQueryResultType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06005004 RID: 20484 RVA: 0x00169DE3 File Offset: 0x00167FE3
		internal static string IsNullInvalidType
		{
			get
			{
				return EntityRes.GetString("IsNullInvalidType");
			}
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x00169DF0 File Offset: 0x00167FF0
		internal static string KeyMustBeCorrelated(object p0)
		{
			return EntityRes.GetString("KeyMustBeCorrelated", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06005006 RID: 20486 RVA: 0x00169E13 File Offset: 0x00168013
		internal static string LeftSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("LeftSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06005007 RID: 20487 RVA: 0x00169E1F File Offset: 0x0016801F
		internal static string LikeArgMustBeStringType
		{
			get
			{
				return EntityRes.GetString("LikeArgMustBeStringType");
			}
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x00169E2C File Offset: 0x0016802C
		internal static string LiteralTypeNotFoundInMetadata(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotFoundInMetadata", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06005009 RID: 20489 RVA: 0x00169E4F File Offset: 0x0016804F
		internal static string MalformedSingleQuotePayload
		{
			get
			{
				return EntityRes.GetString("MalformedSingleQuotePayload");
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600500A RID: 20490 RVA: 0x00169E5B File Offset: 0x0016805B
		internal static string MalformedStringLiteralPayload
		{
			get
			{
				return EntityRes.GetString("MalformedStringLiteralPayload");
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x0600500B RID: 20491 RVA: 0x00169E67 File Offset: 0x00168067
		internal static string MethodInvocationNotSupported
		{
			get
			{
				return EntityRes.GetString("MethodInvocationNotSupported");
			}
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x00169E74 File Offset: 0x00168074
		internal static string MultipleDefinitionsOfParameter(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfParameter", new object[]
			{
				p0
			});
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x00169E98 File Offset: 0x00168098
		internal static string MultipleDefinitionsOfVariable(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfVariable", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x0600500E RID: 20494 RVA: 0x00169EBB File Offset: 0x001680BB
		internal static string MultisetElemsAreNotTypeCompatible
		{
			get
			{
				return EntityRes.GetString("MultisetElemsAreNotTypeCompatible");
			}
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00169EC8 File Offset: 0x001680C8
		internal static string NamespaceAliasAlreadyUsed(object p0)
		{
			return EntityRes.GetString("NamespaceAliasAlreadyUsed", new object[]
			{
				p0
			});
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x00169EEC File Offset: 0x001680EC
		internal static string NamespaceAlreadyImported(object p0)
		{
			return EntityRes.GetString("NamespaceAlreadyImported", new object[]
			{
				p0
			});
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x00169F10 File Offset: 0x00168110
		internal static string NestedAggregateCannotBeUsedInAggregate(object p0, object p1)
		{
			return EntityRes.GetString("NestedAggregateCannotBeUsedInAggregate", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x00169F38 File Offset: 0x00168138
		internal static string NoAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoAggrFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x00169F64 File Offset: 0x00168164
		internal static string NoCanonicalAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalAggrFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00169F90 File Offset: 0x00168190
		internal static string NoCanonicalFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x00169FBC File Offset: 0x001681BC
		internal static string NoFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoFunctionOverloadMatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x00169FE8 File Offset: 0x001681E8
		internal static string NotAMemberOfCollection(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfCollection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0016A010 File Offset: 0x00168210
		internal static string NotAMemberOfType(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0016A038 File Offset: 0x00168238
		internal static string NotASuperOrSubType(object p0, object p1)
		{
			return EntityRes.GetString("NotASuperOrSubType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x0016A05F File Offset: 0x0016825F
		internal static string NullLiteralCannotBePromotedToCollectionOfNulls
		{
			get
			{
				return EntityRes.GetString("NullLiteralCannotBePromotedToCollectionOfNulls");
			}
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0016A06C File Offset: 0x0016826C
		internal static string NumberOfTypeCtorIsLessThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsLessThenFormalSpec", new object[]
			{
				p0
			});
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0016A090 File Offset: 0x00168290
		internal static string NumberOfTypeCtorIsMoreThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsMoreThenFormalSpec", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x0016A0B3 File Offset: 0x001682B3
		internal static string OrderByKeyIsNotOrderComparable
		{
			get
			{
				return EntityRes.GetString("OrderByKeyIsNotOrderComparable");
			}
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0016A0C0 File Offset: 0x001682C0
		internal static string OfTypeOnlyTypeArgumentCannotBeAbstract(object p0)
		{
			return EntityRes.GetString("OfTypeOnlyTypeArgumentCannotBeAbstract", new object[]
			{
				p0
			});
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0016A0E4 File Offset: 0x001682E4
		internal static string ParameterTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ParameterTypeNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0016A10C File Offset: 0x0016830C
		internal static string ParameterWasNotDefined(object p0)
		{
			return EntityRes.GetString("ParameterWasNotDefined", new object[]
			{
				p0
			});
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0016A130 File Offset: 0x00168330
		internal static string PlaceholderExpressionMustBeCompatibleWithEdm64(object p0, object p1)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeCompatibleWithEdm64", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0016A158 File Offset: 0x00168358
		internal static string PlaceholderExpressionMustBeConstant(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeConstant", new object[]
			{
				p0
			});
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x0016A17C File Offset: 0x0016837C
		internal static string PlaceholderExpressionMustBeGreaterThanOrEqualToZero(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeGreaterThanOrEqualToZero", new object[]
			{
				p0
			});
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0016A1A0 File Offset: 0x001683A0
		internal static string PlaceholderSetArgTypeIsNotEqualComparable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("PlaceholderSetArgTypeIsNotEqualComparable", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x0016A1CB File Offset: 0x001683CB
		internal static string PlusLeftExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusLeftExpressionInvalidType");
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x0016A1D7 File Offset: 0x001683D7
		internal static string PlusRightExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusRightExpressionInvalidType");
			}
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0016A1E4 File Offset: 0x001683E4
		internal static string PrecisionMustBeGreaterThanScale(object p0, object p1)
		{
			return EntityRes.GetString("PrecisionMustBeGreaterThanScale", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0016A20C File Offset: 0x0016840C
		internal static string RefArgIsNotOfEntityType(object p0)
		{
			return EntityRes.GetString("RefArgIsNotOfEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x0016A230 File Offset: 0x00168430
		internal static string RefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("RefTypeIdentifierMustSpecifyAnEntityType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x0016A257 File Offset: 0x00168457
		internal static string RelatedEndExprTypeMustBeReference
		{
			get
			{
				return EntityRes.GetString("RelatedEndExprTypeMustBeReference");
			}
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x0016A264 File Offset: 0x00168464
		internal static string RelatedEndExprTypeMustBePromotoableToToEnd(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEndExprTypeMustBePromotoableToToEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x0016A28B File Offset: 0x0016848B
		internal static string RelationshipFromEndIsAmbiguos
		{
			get
			{
				return EntityRes.GetString("RelationshipFromEndIsAmbiguos");
			}
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x0016A298 File Offset: 0x00168498
		internal static string RelationshipTypeExpected(object p0)
		{
			return EntityRes.GetString("RelationshipTypeExpected", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x0016A2BB File Offset: 0x001684BB
		internal static string RelationshipToEndIsAmbiguos
		{
			get
			{
				return EntityRes.GetString("RelationshipToEndIsAmbiguos");
			}
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x0016A2C8 File Offset: 0x001684C8
		internal static string RelationshipTargetMustBeUnique(object p0)
		{
			return EntityRes.GetString("RelationshipTargetMustBeUnique", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x0016A2EB File Offset: 0x001684EB
		internal static string ResultingExpressionTypeCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ResultingExpressionTypeCannotBeNull");
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x0016A2F7 File Offset: 0x001684F7
		internal static string RightSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("RightSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x0016A303 File Offset: 0x00168503
		internal static string RowCtorElementCannotBeNull
		{
			get
			{
				return EntityRes.GetString("RowCtorElementCannotBeNull");
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x0016A30F File Offset: 0x0016850F
		internal static string SelectDistinctMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("SelectDistinctMustBeEqualComparable");
			}
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x0016A31C File Offset: 0x0016851C
		internal static string SourceTypeMustBePromotoableToFromEndRelationType(object p0, object p1)
		{
			return EntityRes.GetString("SourceTypeMustBePromotoableToFromEndRelationType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x0016A343 File Offset: 0x00168543
		internal static string TopAndLimitCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndLimitCannotCoexist");
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x0016A34F File Offset: 0x0016854F
		internal static string TopAndSkipCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndSkipCannotCoexist");
			}
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x0016A35C File Offset: 0x0016855C
		internal static string TypeDoesNotSupportSpec(object p0)
		{
			return EntityRes.GetString("TypeDoesNotSupportSpec", new object[]
			{
				p0
			});
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0016A380 File Offset: 0x00168580
		internal static string TypeDoesNotSupportFacet(object p0, object p1)
		{
			return EntityRes.GetString("TypeDoesNotSupportFacet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0016A3A8 File Offset: 0x001685A8
		internal static string TypeArgumentCountMismatch(object p0, object p1)
		{
			return EntityRes.GetString("TypeArgumentCountMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x0016A3CF File Offset: 0x001685CF
		internal static string TypeArgumentMustBeLiteral
		{
			get
			{
				return EntityRes.GetString("TypeArgumentMustBeLiteral");
			}
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0016A3DC File Offset: 0x001685DC
		internal static string TypeArgumentBelowMin(object p0)
		{
			return EntityRes.GetString("TypeArgumentBelowMin", new object[]
			{
				p0
			});
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0016A400 File Offset: 0x00168600
		internal static string TypeArgumentExceedsMax(object p0)
		{
			return EntityRes.GetString("TypeArgumentExceedsMax", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600503C RID: 20540 RVA: 0x0016A423 File Offset: 0x00168623
		internal static string TypeArgumentIsNotValid
		{
			get
			{
				return EntityRes.GetString("TypeArgumentIsNotValid");
			}
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0016A430 File Offset: 0x00168630
		internal static string TypeKindMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TypeKindMismatch", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x0016A45F File Offset: 0x0016865F
		internal static string TypeMustBeInheritableType
		{
			get
			{
				return EntityRes.GetString("TypeMustBeInheritableType");
			}
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x0016A46C File Offset: 0x0016866C
		internal static string TypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeEntityType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0016A498 File Offset: 0x00168698
		internal static string TypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeNominalType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x0016A4C4 File Offset: 0x001686C4
		internal static string TypeNameNotFound(object p0)
		{
			return EntityRes.GetString("TypeNameNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x0016A4E7 File Offset: 0x001686E7
		internal static string GroupVarNotFoundInScope
		{
			get
			{
				return EntityRes.GetString("GroupVarNotFoundInScope");
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x0016A4F3 File Offset: 0x001686F3
		internal static string InvalidArgumentTypeForAggregateFunction
		{
			get
			{
				return EntityRes.GetString("InvalidArgumentTypeForAggregateFunction");
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06005044 RID: 20548 RVA: 0x0016A4FF File Offset: 0x001686FF
		internal static string InvalidSavePoint
		{
			get
			{
				return EntityRes.GetString("InvalidSavePoint");
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06005045 RID: 20549 RVA: 0x0016A50B File Offset: 0x0016870B
		internal static string InvalidScopeIndex
		{
			get
			{
				return EntityRes.GetString("InvalidScopeIndex");
			}
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0016A518 File Offset: 0x00168718
		internal static string LiteralTypeNotSupported(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06005047 RID: 20551 RVA: 0x0016A53B File Offset: 0x0016873B
		internal static string ParserFatalError
		{
			get
			{
				return EntityRes.GetString("ParserFatalError");
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x0016A547 File Offset: 0x00168747
		internal static string ParserInputError
		{
			get
			{
				return EntityRes.GetString("ParserInputError");
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06005049 RID: 20553 RVA: 0x0016A553 File Offset: 0x00168753
		internal static string StackOverflowInParser
		{
			get
			{
				return EntityRes.GetString("StackOverflowInParser");
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x0016A55F File Offset: 0x0016875F
		internal static string UnknownAstCommandExpression
		{
			get
			{
				return EntityRes.GetString("UnknownAstCommandExpression");
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600504B RID: 20555 RVA: 0x0016A56B File Offset: 0x0016876B
		internal static string UnknownAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownAstExpressionType");
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600504C RID: 20556 RVA: 0x0016A577 File Offset: 0x00168777
		internal static string UnknownBuiltInAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownBuiltInAstExpressionType");
			}
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0016A584 File Offset: 0x00168784
		internal static string UnknownExpressionResolutionClass(object p0)
		{
			return EntityRes.GetString("UnknownExpressionResolutionClass", new object[]
			{
				p0
			});
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x0016A5A8 File Offset: 0x001687A8
		internal static string Cqt_General_UnsupportedExpression(object p0)
		{
			return EntityRes.GetString("Cqt_General_UnsupportedExpression", new object[]
			{
				p0
			});
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x0016A5CC File Offset: 0x001687CC
		internal static string Cqt_General_PolymorphicTypeRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicTypeRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x0016A5F0 File Offset: 0x001687F0
		internal static string Cqt_General_PolymorphicArgRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicArgRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06005051 RID: 20561 RVA: 0x0016A613 File Offset: 0x00168813
		internal static string Cqt_General_MetadataNotReadOnly
		{
			get
			{
				return EntityRes.GetString("Cqt_General_MetadataNotReadOnly");
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06005052 RID: 20562 RVA: 0x0016A61F File Offset: 0x0016881F
		internal static string Cqt_General_NoProviderBooleanType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderBooleanType");
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06005053 RID: 20563 RVA: 0x0016A62B File Offset: 0x0016882B
		internal static string Cqt_General_NoProviderIntegerType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderIntegerType");
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x0016A637 File Offset: 0x00168837
		internal static string Cqt_General_NoProviderStringType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderStringType");
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06005055 RID: 20565 RVA: 0x0016A643 File Offset: 0x00168843
		internal static string Cqt_Metadata_EdmMemberIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EdmMemberIncorrectSpace");
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x0016A64F File Offset: 0x0016884F
		internal static string Cqt_Metadata_EntitySetEntityContainerNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetEntityContainerNull");
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06005057 RID: 20567 RVA: 0x0016A65B File Offset: 0x0016885B
		internal static string Cqt_Metadata_EntitySetIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetIncorrectSpace");
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x0016A667 File Offset: 0x00168867
		internal static string Cqt_Metadata_EntityTypeNullKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeNullKeyMembersInvalid");
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06005059 RID: 20569 RVA: 0x0016A673 File Offset: 0x00168873
		internal static string Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid");
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600505A RID: 20570 RVA: 0x0016A67F File Offset: 0x0016887F
		internal static string Cqt_Metadata_FunctionReturnParameterNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionReturnParameterNull");
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x0016A68B File Offset: 0x0016888B
		internal static string Cqt_Metadata_FunctionIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionIncorrectSpace");
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600505C RID: 20572 RVA: 0x0016A697 File Offset: 0x00168897
		internal static string Cqt_Metadata_FunctionParameterIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionParameterIncorrectSpace");
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x0016A6A3 File Offset: 0x001688A3
		internal static string Cqt_Metadata_TypeUsageIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_TypeUsageIncorrectSpace");
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600505E RID: 20574 RVA: 0x0016A6AF File Offset: 0x001688AF
		internal static string Cqt_Exceptions_InvalidCommandTree
		{
			get
			{
				return EntityRes.GetString("Cqt_Exceptions_InvalidCommandTree");
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600505F RID: 20575 RVA: 0x0016A6BB File Offset: 0x001688BB
		internal static string Cqt_Util_CheckListEmptyInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Util_CheckListEmptyInvalid");
			}
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x0016A6C8 File Offset: 0x001688C8
		internal static string Cqt_Util_CheckListDuplicateName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_Util_CheckListDuplicateName", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x0016A6F4 File Offset: 0x001688F4
		internal static string Cqt_ExpressionLink_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_ExpressionLink_TypeMismatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06005062 RID: 20578 RVA: 0x0016A71B File Offset: 0x0016891B
		internal static string Cqt_ExpressionList_IncorrectElementCount
		{
			get
			{
				return EntityRes.GetString("Cqt_ExpressionList_IncorrectElementCount");
			}
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x0016A728 File Offset: 0x00168928
		internal static string Cqt_Copier_EntityContainerNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_EntityContainerNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x0016A74C File Offset: 0x0016894C
		internal static string Cqt_Copier_EntitySetNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EntitySetNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0016A774 File Offset: 0x00168974
		internal static string Cqt_Copier_FunctionNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_FunctionNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0016A798 File Offset: 0x00168998
		internal static string Cqt_Copier_PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_PropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0016A7C0 File Offset: 0x001689C0
		internal static string Cqt_Copier_NavPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_NavPropertyNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x0016A7E8 File Offset: 0x001689E8
		internal static string Cqt_Copier_EndNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EndNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x0016A810 File Offset: 0x00168A10
		internal static string Cqt_Copier_TypeNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_TypeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x0016A833 File Offset: 0x00168A33
		internal static string Cqt_CommandTree_InvalidDataSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_CommandTree_InvalidDataSpace");
			}
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x0016A840 File Offset: 0x00168A40
		internal static string Cqt_CommandTree_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("Cqt_CommandTree_InvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x0016A864 File Offset: 0x00168A64
		internal static string Cqt_Validator_InvalidIncompatibleParameterReferences(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncompatibleParameterReferences", new object[]
			{
				p0
			});
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x0016A888 File Offset: 0x00168A88
		internal static string Cqt_Validator_InvalidOtherWorkspaceMetadata(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidOtherWorkspaceMetadata", new object[]
			{
				p0
			});
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x0016A8AC File Offset: 0x00168AAC
		internal static string Cqt_Validator_InvalidIncorrectDataSpaceMetadata(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncorrectDataSpaceMetadata", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x0016A8D3 File Offset: 0x00168AD3
		internal static string Cqt_Factory_NewCollectionInvalidCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NewCollectionInvalidCommonType");
			}
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x0016A8E0 File Offset: 0x00168AE0
		internal static string NoSuchProperty(object p0, object p1)
		{
			return EntityRes.GetString("NoSuchProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06005071 RID: 20593 RVA: 0x0016A907 File Offset: 0x00168B07
		internal static string Cqt_Factory_NoSuchRelationEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NoSuchRelationEnd");
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x0016A913 File Offset: 0x00168B13
		internal static string Cqt_Factory_IncompatibleRelationEnds
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_IncompatibleRelationEnds");
			}
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x0016A920 File Offset: 0x00168B20
		internal static string Cqt_Factory_MethodResultTypeNotSupported(object p0)
		{
			return EntityRes.GetString("Cqt_Factory_MethodResultTypeNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x0016A943 File Offset: 0x00168B43
		internal static string Cqt_Aggregate_InvalidFunction
		{
			get
			{
				return EntityRes.GetString("Cqt_Aggregate_InvalidFunction");
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06005075 RID: 20597 RVA: 0x0016A94F File Offset: 0x00168B4F
		internal static string Cqt_Binding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Binding_CollectionRequired");
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x0016A95B File Offset: 0x00168B5B
		internal static string Cqt_GroupBinding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBinding_CollectionRequired");
			}
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0016A968 File Offset: 0x00168B68
		internal static string Cqt_Binary_CollectionsRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Binary_CollectionsRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0016A98C File Offset: 0x00168B8C
		internal static string Cqt_Unary_CollectionRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Unary_CollectionRequired", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06005079 RID: 20601 RVA: 0x0016A9AF File Offset: 0x00168BAF
		internal static string Cqt_And_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_And_BooleanArgumentsRequired");
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600507A RID: 20602 RVA: 0x0016A9BB File Offset: 0x00168BBB
		internal static string Cqt_Apply_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Apply_DuplicateVariableNames");
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600507B RID: 20603 RVA: 0x0016A9C7 File Offset: 0x00168BC7
		internal static string Cqt_Arithmetic_NumericCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Arithmetic_NumericCommonType");
			}
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x0016A9D4 File Offset: 0x00168BD4
		internal static string Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus(object p0)
		{
			return EntityRes.GetString("Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600507D RID: 20605 RVA: 0x0016A9F7 File Offset: 0x00168BF7
		internal static string Cqt_Case_WhensMustEqualThens
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_WhensMustEqualThens");
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x0016AA03 File Offset: 0x00168C03
		internal static string Cqt_Case_InvalidResultType
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_InvalidResultType");
			}
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x0016AA10 File Offset: 0x00168C10
		internal static string Cqt_Cast_InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Cast_InvalidCast", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06005080 RID: 20608 RVA: 0x0016AA37 File Offset: 0x00168C37
		internal static string Cqt_Comparison_ComparableRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Comparison_ComparableRequired");
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06005081 RID: 20609 RVA: 0x0016AA43 File Offset: 0x00168C43
		internal static string Cqt_Constant_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_Constant_InvalidType");
			}
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x0016AA50 File Offset: 0x00168C50
		internal static string Cqt_Constant_InvalidValueForType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidValueForType", new object[]
			{
				p0
			});
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x0016AA74 File Offset: 0x00168C74
		internal static string Cqt_Constant_InvalidConstantType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidConstantType", new object[]
			{
				p0
			});
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0016AA98 File Offset: 0x00168C98
		internal static string Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06005085 RID: 20613 RVA: 0x0016AAC3 File Offset: 0x00168CC3
		internal static string Cqt_Distinct_InvalidCollection
		{
			get
			{
				return EntityRes.GetString("Cqt_Distinct_InvalidCollection");
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06005086 RID: 20614 RVA: 0x0016AACF File Offset: 0x00168CCF
		internal static string Cqt_DeRef_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_DeRef_RefRequired");
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06005087 RID: 20615 RVA: 0x0016AADB File Offset: 0x00168CDB
		internal static string Cqt_Element_InvalidArgumentForUnwrapSingleProperty
		{
			get
			{
				return EntityRes.GetString("Cqt_Element_InvalidArgumentForUnwrapSingleProperty");
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06005088 RID: 20616 RVA: 0x0016AAE7 File Offset: 0x00168CE7
		internal static string Cqt_Function_VoidResultInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_VoidResultInvalid");
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06005089 RID: 20617 RVA: 0x0016AAF3 File Offset: 0x00168CF3
		internal static string Cqt_Function_NonComposableInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_NonComposableInExpression");
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x0600508A RID: 20618 RVA: 0x0016AAFF File Offset: 0x00168CFF
		internal static string Cqt_Function_CommandTextInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_CommandTextInExpression");
			}
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x0016AB0C File Offset: 0x00168D0C
		internal static string Cqt_Function_CanonicalFunction_NotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_NotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x0016AB30 File Offset: 0x00168D30
		internal static string Cqt_Function_CanonicalFunction_AmbiguousMatch(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_AmbiguousMatch", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x0600508D RID: 20621 RVA: 0x0016AB53 File Offset: 0x00168D53
		internal static string Cqt_GetEntityRef_EntityRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetEntityRef_EntityRequired");
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600508E RID: 20622 RVA: 0x0016AB5F File Offset: 0x00168D5F
		internal static string Cqt_GetRefKey_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetRefKey_RefRequired");
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600508F RID: 20623 RVA: 0x0016AB6B File Offset: 0x00168D6B
		internal static string Cqt_GroupBy_AtLeastOneKeyOrAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_AtLeastOneKeyOrAggregate");
			}
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0016AB78 File Offset: 0x00168D78
		internal static string Cqt_GroupBy_KeyNotEqualityComparable(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_KeyNotEqualityComparable", new object[]
			{
				p0
			});
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x0016AB9C File Offset: 0x00168D9C
		internal static string Cqt_GroupBy_AggregateColumnExistsAsGroupColumn(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_AggregateColumnExistsAsGroupColumn", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06005092 RID: 20626 RVA: 0x0016ABBF File Offset: 0x00168DBF
		internal static string Cqt_GroupBy_MoreThanOneGroupAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_MoreThanOneGroupAggregate");
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06005093 RID: 20627 RVA: 0x0016ABCB File Offset: 0x00168DCB
		internal static string Cqt_CrossJoin_AtLeastTwoInputs
		{
			get
			{
				return EntityRes.GetString("Cqt_CrossJoin_AtLeastTwoInputs");
			}
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x0016ABD8 File Offset: 0x00168DD8
		internal static string Cqt_CrossJoin_DuplicateVariableNames(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_CrossJoin_DuplicateVariableNames", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06005095 RID: 20629 RVA: 0x0016AC03 File Offset: 0x00168E03
		internal static string Cqt_IsNull_CollectionNotAllowed
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_CollectionNotAllowed");
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06005096 RID: 20630 RVA: 0x0016AC0F File Offset: 0x00168E0F
		internal static string Cqt_IsNull_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_InvalidType");
			}
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x0016AC1C File Offset: 0x00168E1C
		internal static string Cqt_InvalidTypeForSetOperation(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_InvalidTypeForSetOperation", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06005098 RID: 20632 RVA: 0x0016AC43 File Offset: 0x00168E43
		internal static string Cqt_Join_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Join_DuplicateVariableNames");
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x0016AC4F File Offset: 0x00168E4F
		internal static string Cqt_Limit_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x0600509A RID: 20634 RVA: 0x0016AC5B File Offset: 0x00168E5B
		internal static string Cqt_Limit_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_IntegerRequired");
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x0016AC67 File Offset: 0x00168E67
		internal static string Cqt_Limit_NonNegativeLimitRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_NonNegativeLimitRequired");
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600509C RID: 20636 RVA: 0x0016AC73 File Offset: 0x00168E73
		internal static string Cqt_NewInstance_CollectionTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_CollectionTypeRequired");
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600509D RID: 20637 RVA: 0x0016AC7F File Offset: 0x00168E7F
		internal static string Cqt_NewInstance_StructuralTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_StructuralTypeRequired");
			}
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x0016AC8C File Offset: 0x00168E8C
		internal static string Cqt_NewInstance_CannotInstantiateMemberlessType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateMemberlessType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x0016ACB0 File Offset: 0x00168EB0
		internal static string Cqt_NewInstance_CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateAbstractType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060050A0 RID: 20640 RVA: 0x0016ACD3 File Offset: 0x00168ED3
		internal static string Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid");
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060050A1 RID: 20641 RVA: 0x0016ACDF File Offset: 0x00168EDF
		internal static string Cqt_Not_BooleanArgumentRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Not_BooleanArgumentRequired");
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060050A2 RID: 20642 RVA: 0x0016ACEB File Offset: 0x00168EEB
		internal static string Cqt_Or_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Or_BooleanArgumentsRequired");
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060050A3 RID: 20643 RVA: 0x0016ACF7 File Offset: 0x00168EF7
		internal static string Cqt_In_SameResultTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_In_SameResultTypeRequired");
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060050A4 RID: 20644 RVA: 0x0016AD03 File Offset: 0x00168F03
		internal static string Cqt_Property_InstanceRequiredForInstance
		{
			get
			{
				return EntityRes.GetString("Cqt_Property_InstanceRequiredForInstance");
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060050A5 RID: 20645 RVA: 0x0016AD0F File Offset: 0x00168F0F
		internal static string Cqt_Ref_PolymorphicArgRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Ref_PolymorphicArgRequired");
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060050A6 RID: 20646 RVA: 0x0016AD1B File Offset: 0x00168F1B
		internal static string Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship");
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060050A7 RID: 20647 RVA: 0x0016AD27 File Offset: 0x00168F27
		internal static string Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne");
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060050A8 RID: 20648 RVA: 0x0016AD33 File Offset: 0x00168F33
		internal static string Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd");
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060050A9 RID: 20649 RVA: 0x0016AD3F File Offset: 0x00168F3F
		internal static string Cqt_RelatedEntityRef_TargetEntityNotRef
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotRef");
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060050AA RID: 20650 RVA: 0x0016AD4B File Offset: 0x00168F4B
		internal static string Cqt_RelatedEntityRef_TargetEntityNotCompatible
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotCompatible");
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060050AB RID: 20651 RVA: 0x0016AD57 File Offset: 0x00168F57
		internal static string Cqt_RelNav_NoCompositions
		{
			get
			{
				return EntityRes.GetString("Cqt_RelNav_NoCompositions");
			}
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x0016AD64 File Offset: 0x00168F64
		internal static string Cqt_RelNav_WrongSourceType(object p0)
		{
			return EntityRes.GetString("Cqt_RelNav_WrongSourceType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060050AD RID: 20653 RVA: 0x0016AD87 File Offset: 0x00168F87
		internal static string Cqt_Skip_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x0016AD93 File Offset: 0x00168F93
		internal static string Cqt_Skip_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_IntegerRequired");
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060050AF RID: 20655 RVA: 0x0016AD9F File Offset: 0x00168F9F
		internal static string Cqt_Skip_NonNegativeCountRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_NonNegativeCountRequired");
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060050B0 RID: 20656 RVA: 0x0016ADAB File Offset: 0x00168FAB
		internal static string Cqt_Sort_NonStringCollationInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_NonStringCollationInvalid");
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060050B1 RID: 20657 RVA: 0x0016ADB7 File Offset: 0x00168FB7
		internal static string Cqt_Sort_OrderComparable
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_OrderComparable");
			}
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0016ADC4 File Offset: 0x00168FC4
		internal static string Cqt_UDF_FunctionDefinitionGenerationFailed(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionGenerationFailed", new object[]
			{
				p0
			});
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0016ADE8 File Offset: 0x00168FE8
		internal static string Cqt_UDF_FunctionDefinitionWithCircularReference(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionWithCircularReference", new object[]
			{
				p0
			});
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0016AE0C File Offset: 0x0016900C
		internal static string Cqt_UDF_FunctionDefinitionResultTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionResultTypeMismatch", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0016AE38 File Offset: 0x00169038
		internal static string Cqt_UDF_FunctionHasNoDefinition(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionHasNoDefinition", new object[]
			{
				p0
			});
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0016AE5C File Offset: 0x0016905C
		internal static string Cqt_Validator_VarRefInvalid(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefInvalid", new object[]
			{
				p0
			});
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0016AE80 File Offset: 0x00169080
		internal static string Cqt_Validator_VarRefTypeMismatch(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefTypeMismatch", new object[]
			{
				p0
			});
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0016AEA4 File Offset: 0x001690A4
		internal static string Iqt_General_UnsupportedOp(object p0)
		{
			return EntityRes.GetString("Iqt_General_UnsupportedOp", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060050B9 RID: 20665 RVA: 0x0016AEC7 File Offset: 0x001690C7
		internal static string Iqt_CTGen_UnexpectedAggregate
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedAggregate");
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x0016AED3 File Offset: 0x001690D3
		internal static string Iqt_CTGen_UnexpectedVarDefList
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDefList");
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x0016AEDF File Offset: 0x001690DF
		internal static string Iqt_CTGen_UnexpectedVarDef
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDef");
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x0016AEEB File Offset: 0x001690EB
		internal static string ADP_MustUseSequentialAccess
		{
			get
			{
				return EntityRes.GetString("ADP_MustUseSequentialAccess");
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x0016AEF7 File Offset: 0x001690F7
		internal static string ADP_ProviderDoesNotSupportCommandTrees
		{
			get
			{
				return EntityRes.GetString("ADP_ProviderDoesNotSupportCommandTrees");
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x0016AF03 File Offset: 0x00169103
		internal static string ADP_ClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ClosedDataReaderError");
			}
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0016AF10 File Offset: 0x00169110
		internal static string ADP_DataReaderClosed(object p0)
		{
			return EntityRes.GetString("ADP_DataReaderClosed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060050C0 RID: 20672 RVA: 0x0016AF33 File Offset: 0x00169133
		internal static string ADP_ImplicitlyClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ImplicitlyClosedDataReaderError");
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x0016AF3F File Offset: 0x0016913F
		internal static string ADP_NoData
		{
			get
			{
				return EntityRes.GetString("ADP_NoData");
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060050C2 RID: 20674 RVA: 0x0016AF4B File Offset: 0x0016914B
		internal static string ADP_GetSchemaTableIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ADP_GetSchemaTableIsNotSupported");
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060050C3 RID: 20675 RVA: 0x0016AF57 File Offset: 0x00169157
		internal static string ADP_InvalidDataReaderFieldCountForScalarType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderFieldCountForScalarType");
			}
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0016AF64 File Offset: 0x00169164
		internal static string ADP_InvalidDataReaderMissingColumnForType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingColumnForType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0016AF8C File Offset: 0x0016918C
		internal static string ADP_InvalidDataReaderMissingDiscriminatorColumn(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingDiscriminatorColumn", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x0016AFB3 File Offset: 0x001691B3
		internal static string ADP_InvalidDataReaderUnableToDetermineType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderUnableToDetermineType");
			}
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x0016AFC0 File Offset: 0x001691C0
		internal static string ADP_InvalidDataReaderUnableToMaterializeNonScalarType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderUnableToMaterializeNonScalarType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x0016AFE8 File Offset: 0x001691E8
		internal static string ADP_KeysRequiredForJoinOverNest(object p0)
		{
			return EntityRes.GetString("ADP_KeysRequiredForJoinOverNest", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060050C9 RID: 20681 RVA: 0x0016B00B File Offset: 0x0016920B
		internal static string ADP_KeysRequiredForNesting
		{
			get
			{
				return EntityRes.GetString("ADP_KeysRequiredForNesting");
			}
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x0016B018 File Offset: 0x00169218
		internal static string ADP_NestingNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NestingNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x0016B040 File Offset: 0x00169240
		internal static string ADP_NoQueryMappingView(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NoQueryMappingView", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0016B068 File Offset: 0x00169268
		internal static string ADP_InternalProviderError(object p0)
		{
			return EntityRes.GetString("ADP_InternalProviderError", new object[]
			{
				p0
			});
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0016B08C File Offset: 0x0016928C
		internal static string ADP_InvalidEnumerationValue(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidEnumerationValue", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x0016B0B4 File Offset: 0x001692B4
		internal static string ADP_InvalidBufferSizeOrIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidBufferSizeOrIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x0016B0DC File Offset: 0x001692DC
		internal static string ADP_InvalidDataLength(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataLength", new object[]
			{
				p0
			});
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x0016B100 File Offset: 0x00169300
		internal static string ADP_InvalidDataType(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataType", new object[]
			{
				p0
			});
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x0016B124 File Offset: 0x00169324
		internal static string ADP_InvalidDestinationBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDestinationBufferIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x0016B14C File Offset: 0x0016934C
		internal static string ADP_InvalidSourceBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidSourceBufferIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x0016B174 File Offset: 0x00169374
		internal static string ADP_NonSequentialChunkAccess(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ADP_NonSequentialChunkAccess", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0016B1A0 File Offset: 0x001693A0
		internal static string ADP_NonSequentialColumnAccess(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NonSequentialColumnAccess", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x0016B1C8 File Offset: 0x001693C8
		internal static string ADP_UnknownDataTypeCode(object p0, object p1)
		{
			return EntityRes.GetString("ADP_UnknownDataTypeCode", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060050D6 RID: 20694 RVA: 0x0016B1EF File Offset: 0x001693EF
		internal static string DataCategory_Data
		{
			get
			{
				return EntityRes.GetString("DataCategory_Data");
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x0016B1FB File Offset: 0x001693FB
		internal static string DbParameter_Direction
		{
			get
			{
				return EntityRes.GetString("DbParameter_Direction");
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060050D8 RID: 20696 RVA: 0x0016B207 File Offset: 0x00169407
		internal static string DbParameter_Size
		{
			get
			{
				return EntityRes.GetString("DbParameter_Size");
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060050D9 RID: 20697 RVA: 0x0016B213 File Offset: 0x00169413
		internal static string DataCategory_Update
		{
			get
			{
				return EntityRes.GetString("DataCategory_Update");
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060050DA RID: 20698 RVA: 0x0016B21F File Offset: 0x0016941F
		internal static string DbParameter_SourceColumn
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceColumn");
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x0016B22B File Offset: 0x0016942B
		internal static string DbParameter_SourceVersion
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceVersion");
			}
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0016B238 File Offset: 0x00169438
		internal static string ADP_CollectionParameterElementIsNull(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNull", new object[]
			{
				p0
			});
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0016B25C File Offset: 0x0016945C
		internal static string ADP_CollectionParameterElementIsNullOrEmpty(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNullOrEmpty", new object[]
			{
				p0
			});
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060050DE RID: 20702 RVA: 0x0016B27F File Offset: 0x0016947F
		internal static string NonReturnParameterInReturnParameterCollection
		{
			get
			{
				return EntityRes.GetString("NonReturnParameterInReturnParameterCollection");
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x0016B28B File Offset: 0x0016948B
		internal static string ReturnParameterInInputParameterCollection
		{
			get
			{
				return EntityRes.GetString("ReturnParameterInInputParameterCollection");
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x0016B297 File Offset: 0x00169497
		internal static string NullEntitySetsForFunctionReturningMultipleResultSets
		{
			get
			{
				return EntityRes.GetString("NullEntitySetsForFunctionReturningMultipleResultSets");
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x0016B2A3 File Offset: 0x001694A3
		internal static string NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters
		{
			get
			{
				return EntityRes.GetString("NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters");
			}
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0016B2B0 File Offset: 0x001694B0
		internal static string EntityParameterCollectionInvalidParameterName(object p0)
		{
			return EntityRes.GetString("EntityParameterCollectionInvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x0016B2D4 File Offset: 0x001694D4
		internal static string EntityParameterCollectionInvalidIndex(object p0, object p1)
		{
			return EntityRes.GetString("EntityParameterCollectionInvalidIndex", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0016B2FC File Offset: 0x001694FC
		internal static string InvalidEntityParameterType(object p0)
		{
			return EntityRes.GetString("InvalidEntityParameterType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060050E5 RID: 20709 RVA: 0x0016B31F File Offset: 0x0016951F
		internal static string EntityParameterContainedByAnotherCollection
		{
			get
			{
				return EntityRes.GetString("EntityParameterContainedByAnotherCollection");
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060050E6 RID: 20710 RVA: 0x0016B32B File Offset: 0x0016952B
		internal static string EntityParameterCollectionRemoveInvalidObject
		{
			get
			{
				return EntityRes.GetString("EntityParameterCollectionRemoveInvalidObject");
			}
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x0016B338 File Offset: 0x00169538
		internal static string ADP_ConnectionStringSyntax(object p0)
		{
			return EntityRes.GetString("ADP_ConnectionStringSyntax", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060050E8 RID: 20712 RVA: 0x0016B35B File Offset: 0x0016955B
		internal static string ExpandingDataDirectoryFailed
		{
			get
			{
				return EntityRes.GetString("ExpandingDataDirectoryFailed");
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060050E9 RID: 20713 RVA: 0x0016B367 File Offset: 0x00169567
		internal static string ADP_InvalidDataDirectory
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataDirectory");
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x060050EA RID: 20714 RVA: 0x0016B373 File Offset: 0x00169573
		internal static string ADP_InvalidMultipartNameDelimiterUsage
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidMultipartNameDelimiterUsage");
			}
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x0016B380 File Offset: 0x00169580
		internal static string ADP_InvalidSizeValue(object p0)
		{
			return EntityRes.GetString("ADP_InvalidSizeValue", new object[]
			{
				p0
			});
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0016B3A4 File Offset: 0x001695A4
		internal static string ADP_KeywordNotSupported(object p0)
		{
			return EntityRes.GetString("ADP_KeywordNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x0016B3C8 File Offset: 0x001695C8
		internal static string ConstantFacetSpecifiedInSchema(object p0, object p1)
		{
			return EntityRes.GetString("ConstantFacetSpecifiedInSchema", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x0016B3F0 File Offset: 0x001695F0
		internal static string DuplicateAnnotation(object p0, object p1)
		{
			return EntityRes.GetString("DuplicateAnnotation", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x0016B418 File Offset: 0x00169618
		internal static string EmptyFile(object p0)
		{
			return EntityRes.GetString("EmptyFile", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x060050F0 RID: 20720 RVA: 0x0016B43B File Offset: 0x0016963B
		internal static string EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("EmptySchemaTextReader");
			}
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x0016B448 File Offset: 0x00169648
		internal static string EmptyName(object p0)
		{
			return EntityRes.GetString("EmptyName", new object[]
			{
				p0
			});
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0016B46C File Offset: 0x0016966C
		internal static string InvalidName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x060050F3 RID: 20723 RVA: 0x0016B493 File Offset: 0x00169693
		internal static string MissingName
		{
			get
			{
				return EntityRes.GetString("MissingName");
			}
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x0016B4A0 File Offset: 0x001696A0
		internal static string UnexpectedXmlAttribute(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlAttribute", new object[]
			{
				p0
			});
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x0016B4C4 File Offset: 0x001696C4
		internal static string UnexpectedXmlElement(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlElement", new object[]
			{
				p0
			});
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x0016B4E8 File Offset: 0x001696E8
		internal static string TextNotAllowed(object p0)
		{
			return EntityRes.GetString("TextNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x0016B50C File Offset: 0x0016970C
		internal static string UnexpectedXmlNodeType(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlNodeType", new object[]
			{
				p0
			});
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0016B530 File Offset: 0x00169730
		internal static string MalformedXml(object p0, object p1)
		{
			return EntityRes.GetString("MalformedXml", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x0016B558 File Offset: 0x00169758
		internal static string ValueNotUnderstood(object p0, object p1)
		{
			return EntityRes.GetString("ValueNotUnderstood", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x0016B580 File Offset: 0x00169780
		internal static string EntityContainerAlreadyExists(object p0)
		{
			return EntityRes.GetString("EntityContainerAlreadyExists", new object[]
			{
				p0
			});
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x0016B5A4 File Offset: 0x001697A4
		internal static string TypeNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("TypeNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x0016B5C8 File Offset: 0x001697C8
		internal static string PropertyNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("PropertyNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x0016B5EC File Offset: 0x001697EC
		internal static string DuplicateMemberNameInExtendedEntityContainer(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberNameInExtendedEntityContainer", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x0016B618 File Offset: 0x00169818
		internal static string DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("DuplicateEntityContainerMemberName", new object[]
			{
				p0
			});
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x0016B63C File Offset: 0x0016983C
		internal static string PropertyTypeAlreadyDefined(object p0)
		{
			return EntityRes.GetString("PropertyTypeAlreadyDefined", new object[]
			{
				p0
			});
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x0016B660 File Offset: 0x00169860
		internal static string InvalidSize(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidSize", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x0016B690 File Offset: 0x00169890
		internal static string InvalidSystemReferenceId(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidSystemReferenceId", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x0016B6C0 File Offset: 0x001698C0
		internal static string BadNamespaceOrAlias(object p0)
		{
			return EntityRes.GetString("BadNamespaceOrAlias", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x0016B6E3 File Offset: 0x001698E3
		internal static string MissingNamespaceAttribute
		{
			get
			{
				return EntityRes.GetString("MissingNamespaceAttribute");
			}
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x0016B6F0 File Offset: 0x001698F0
		internal static string InvalidBaseTypeForStructuredType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForStructuredType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x0016B718 File Offset: 0x00169918
		internal static string InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("InvalidPropertyType", new object[]
			{
				p0
			});
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x0016B73C File Offset: 0x0016993C
		internal static string InvalidBaseTypeForItemType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForItemType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x0016B764 File Offset: 0x00169964
		internal static string InvalidBaseTypeForNestedType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForNestedType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06005108 RID: 20744 RVA: 0x0016B78B File Offset: 0x0016998B
		internal static string DefaultNotAllowed
		{
			get
			{
				return EntityRes.GetString("DefaultNotAllowed");
			}
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x0016B798 File Offset: 0x00169998
		internal static string FacetNotAllowed(object p0, object p1)
		{
			return EntityRes.GetString("FacetNotAllowed", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x0016B7C0 File Offset: 0x001699C0
		internal static string RequiredFacetMissing(object p0, object p1)
		{
			return EntityRes.GetString("RequiredFacetMissing", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x0016B7E8 File Offset: 0x001699E8
		internal static string InvalidDefaultBinaryWithNoMaxLength(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBinaryWithNoMaxLength", new object[]
			{
				p0
			});
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0016B80C File Offset: 0x00169A0C
		internal static string InvalidDefaultIntegral(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultIntegral", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0016B838 File Offset: 0x00169A38
		internal static string InvalidDefaultDateTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTime", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x0016B860 File Offset: 0x00169A60
		internal static string InvalidDefaultTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultTime", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x0016B888 File Offset: 0x00169A88
		internal static string InvalidDefaultDateTimeOffset(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTimeOffset", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0016B8B0 File Offset: 0x00169AB0
		internal static string InvalidDefaultDecimal(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultDecimal", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x0016B8DC File Offset: 0x00169ADC
		internal static string InvalidDefaultFloatingPoint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultFloatingPoint", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0016B908 File Offset: 0x00169B08
		internal static string InvalidDefaultGuid(object p0)
		{
			return EntityRes.GetString("InvalidDefaultGuid", new object[]
			{
				p0
			});
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x0016B92C File Offset: 0x00169B2C
		internal static string InvalidDefaultBoolean(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBoolean", new object[]
			{
				p0
			});
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x0016B950 File Offset: 0x00169B50
		internal static string DuplicateMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberName", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06005115 RID: 20757 RVA: 0x0016B97B File Offset: 0x00169B7B
		internal static string GeneratorErrorSeverityError
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityError");
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06005116 RID: 20758 RVA: 0x0016B987 File Offset: 0x00169B87
		internal static string GeneratorErrorSeverityWarning
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityWarning");
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x0016B993 File Offset: 0x00169B93
		internal static string GeneratorErrorSeverityUnknown
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityUnknown");
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x0016B99F File Offset: 0x00169B9F
		internal static string SourceUriUnknown
		{
			get
			{
				return EntityRes.GetString("SourceUriUnknown");
			}
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x0016B9AC File Offset: 0x00169BAC
		internal static string BadPrecisionAndScale(object p0, object p1)
		{
			return EntityRes.GetString("BadPrecisionAndScale", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x0016B9D4 File Offset: 0x00169BD4
		internal static string InvalidNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceInUsing", new object[]
			{
				p0
			});
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x0016B9F8 File Offset: 0x00169BF8
		internal static string BadNavigationPropertyRelationshipNotRelationship(object p0)
		{
			return EntityRes.GetString("BadNavigationPropertyRelationshipNotRelationship", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x0016BA1B File Offset: 0x00169C1B
		internal static string BadNavigationPropertyRolesCannotBeTheSame
		{
			get
			{
				return EntityRes.GetString("BadNavigationPropertyRolesCannotBeTheSame");
			}
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x0016BA28 File Offset: 0x00169C28
		internal static string BadNavigationPropertyUndefinedRole(object p0, object p1)
		{
			return EntityRes.GetString("BadNavigationPropertyUndefinedRole", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x0016BA50 File Offset: 0x00169C50
		internal static string BadNavigationPropertyBadFromRoleType(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("BadNavigationPropertyBadFromRoleType", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x0016BA84 File Offset: 0x00169C84
		internal static string InvalidMemberNameMatchesTypeName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMemberNameMatchesTypeName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x0016BAAC File Offset: 0x00169CAC
		internal static string InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyKeyDefinedInBaseClass", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x0016BAD4 File Offset: 0x00169CD4
		internal static string InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNullablePart", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x0016BAFC File Offset: 0x00169CFC
		internal static string InvalidKeyNoProperty(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNoProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x0016BB24 File Offset: 0x00169D24
		internal static string KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("KeyMissingOnEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06005124 RID: 20772 RVA: 0x0016BB47 File Offset: 0x00169D47
		internal static string InvalidDocumentationBothTextAndStructure
		{
			get
			{
				return EntityRes.GetString("InvalidDocumentationBothTextAndStructure");
			}
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x0016BB54 File Offset: 0x00169D54
		internal static string ArgumentOutOfRangeExpectedPostiveNumber(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRangeExpectedPostiveNumber", new object[]
			{
				p0
			});
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x0016BB78 File Offset: 0x00169D78
		internal static string ArgumentOutOfRange(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRange", new object[]
			{
				p0
			});
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x0016BB9C File Offset: 0x00169D9C
		internal static string UnacceptableUri(object p0)
		{
			return EntityRes.GetString("UnacceptableUri", new object[]
			{
				p0
			});
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x0016BBC0 File Offset: 0x00169DC0
		internal static string UnexpectedTypeInCollection(object p0, object p1)
		{
			return EntityRes.GetString("UnexpectedTypeInCollection", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x0016BBE7 File Offset: 0x00169DE7
		internal static string AllElementsMustBeInSchema
		{
			get
			{
				return EntityRes.GetString("AllElementsMustBeInSchema");
			}
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x0016BBF4 File Offset: 0x00169DF4
		internal static string AliasNameIsAlreadyDefined(object p0)
		{
			return EntityRes.GetString("AliasNameIsAlreadyDefined", new object[]
			{
				p0
			});
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x0016BC18 File Offset: 0x00169E18
		internal static string NeedNotUseSystemNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("NeedNotUseSystemNamespaceInUsing", new object[]
			{
				p0
			});
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x0016BC3C File Offset: 0x00169E3C
		internal static string CannotUseSystemNamespaceAsAlias(object p0)
		{
			return EntityRes.GetString("CannotUseSystemNamespaceAsAlias", new object[]
			{
				p0
			});
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x0016BC60 File Offset: 0x00169E60
		internal static string EntitySetTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EntitySetTypeHasNoKeys", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x0016BC88 File Offset: 0x00169E88
		internal static string TableAndSchemaAreMutuallyExclusiveWithDefiningQuery(object p0)
		{
			return EntityRes.GetString("TableAndSchemaAreMutuallyExclusiveWithDefiningQuery", new object[]
			{
				p0
			});
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x0016BCAC File Offset: 0x00169EAC
		internal static string UnexpectedRootElement(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElement", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x0016BCD8 File Offset: 0x00169ED8
		internal static string UnexpectedRootElementNoNamespace(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElementNoNamespace", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x0016BD04 File Offset: 0x00169F04
		internal static string ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("ParameterNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x0016BD28 File Offset: 0x00169F28
		internal static string FunctionWithNonPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonPrimitiveTypeNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x0016BD50 File Offset: 0x00169F50
		internal static string FunctionWithNonEdmPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonEdmPrimitiveTypeNotSupported", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x0016BD78 File Offset: 0x00169F78
		internal static string FunctionImportWithUnsupportedReturnTypeV1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1", new object[]
			{
				p0
			});
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x0016BD9C File Offset: 0x00169F9C
		internal static string FunctionImportWithUnsupportedReturnTypeV1_1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1_1", new object[]
			{
				p0
			});
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x0016BDC0 File Offset: 0x00169FC0
		internal static string FunctionImportWithUnsupportedReturnTypeV2(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV2", new object[]
			{
				p0
			});
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x0016BDE4 File Offset: 0x00169FE4
		internal static string FunctionImportUnknownEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("FunctionImportUnknownEntitySet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x0016BE0C File Offset: 0x0016A00C
		internal static string FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(object p0)
		{
			return EntityRes.GetString("FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet", new object[]
			{
				p0
			});
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x0016BE30 File Offset: 0x0016A030
		internal static string FunctionImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("FunctionImportEntityTypeDoesNotMatchEntitySet", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x0016BE5C File Offset: 0x0016A05C
		internal static string FunctionImportSpecifiesEntitySetButNotEntityType(object p0)
		{
			return EntityRes.GetString("FunctionImportSpecifiesEntitySetButNotEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x0016BE80 File Offset: 0x0016A080
		internal static string FunctionImportEntitySetAndEntitySetPathDeclared(object p0)
		{
			return EntityRes.GetString("FunctionImportEntitySetAndEntitySetPathDeclared", new object[]
			{
				p0
			});
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x0016BEA4 File Offset: 0x0016A0A4
		internal static string FunctionImportComposableAndSideEffectingNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportComposableAndSideEffectingNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x0016BEC8 File Offset: 0x0016A0C8
		internal static string FunctionImportCollectionAndRefParametersNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportCollectionAndRefParametersNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x0016BEEC File Offset: 0x0016A0EC
		internal static string FunctionImportNonNullableParametersNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportNonNullableParametersNotAllowed", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600513F RID: 20799 RVA: 0x0016BF0F File Offset: 0x0016A10F
		internal static string TVFReturnTypeRowHasNonScalarProperty
		{
			get
			{
				return EntityRes.GetString("TVFReturnTypeRowHasNonScalarProperty");
			}
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x0016BF1C File Offset: 0x0016A11C
		internal static string DuplicateEntitySetTable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateEntitySetTable", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0016BF48 File Offset: 0x0016A148
		internal static string ConcurrencyRedefinedOnSubTypeOfEntitySetType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConcurrencyRedefinedOnSubTypeOfEntitySetType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x0016BF74 File Offset: 0x0016A174
		internal static string SimilarRelationshipEnd(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("SimilarRelationshipEnd", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x0016BFA8 File Offset: 0x0016A1A8
		internal static string InvalidRelationshipEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndMultiplicity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x0016BFD0 File Offset: 0x0016A1D0
		internal static string EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EndNameAlreadyDefinedDuplicate", new object[]
			{
				p0
			});
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x0016BFF4 File Offset: 0x0016A1F4
		internal static string InvalidRelationshipEndType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x0016C01C File Offset: 0x0016A21C
		internal static string BadParameterDirection(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("BadParameterDirection", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x0016C04C File Offset: 0x0016A24C
		internal static string BadParameterDirectionForComposableFunctions(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("BadParameterDirectionForComposableFunctions", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0016C07B File Offset: 0x0016A27B
		internal static string InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x0016C088 File Offset: 0x0016A288
		internal static string InvalidAction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidAction", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x0016C0B0 File Offset: 0x0016A2B0
		internal static string DuplicationOperation(object p0)
		{
			return EntityRes.GetString("DuplicationOperation", new object[]
			{
				p0
			});
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x0016C0D4 File Offset: 0x0016A2D4
		internal static string NotInNamespaceAlias(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NotInNamespaceAlias", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0016C100 File Offset: 0x0016A300
		internal static string NotNamespaceQualified(object p0)
		{
			return EntityRes.GetString("NotNamespaceQualified", new object[]
			{
				p0
			});
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0016C124 File Offset: 0x0016A324
		internal static string NotInNamespaceNoAlias(object p0, object p1)
		{
			return EntityRes.GetString("NotInNamespaceNoAlias", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x0016C14C File Offset: 0x0016A34C
		internal static string InvalidValueForParameterTypeSemanticsAttribute(object p0)
		{
			return EntityRes.GetString("InvalidValueForParameterTypeSemanticsAttribute", new object[]
			{
				p0
			});
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x0016C170 File Offset: 0x0016A370
		internal static string DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatePropertyNameSpecifiedInEntityKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x0016C198 File Offset: 0x0016A398
		internal static string InvalidEntitySetType(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetType", new object[]
			{
				p0
			});
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0016C1BC File Offset: 0x0016A3BC
		internal static string InvalidRelationshipSetType(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetType", new object[]
			{
				p0
			});
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x0016C1E0 File Offset: 0x0016A3E0
		internal static string InvalidEntityContainerNameInExtends(object p0)
		{
			return EntityRes.GetString("InvalidEntityContainerNameInExtends", new object[]
			{
				p0
			});
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x0016C204 File Offset: 0x0016A404
		internal static string InvalidNamespaceOrAliasSpecified(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceOrAliasSpecified", new object[]
			{
				p0
			});
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0016C228 File Offset: 0x0016A428
		internal static string PrecisionOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("PrecisionOutOfRange", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x0016C258 File Offset: 0x0016A458
		internal static string ScaleOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ScaleOutOfRange", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0016C288 File Offset: 0x0016A488
		internal static string InvalidEntitySetNameReference(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntitySetNameReference", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0016C2B0 File Offset: 0x0016A4B0
		internal static string InvalidEntityEndName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityEndName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x0016C2D8 File Offset: 0x0016A4D8
		internal static string DuplicateEndName(object p0)
		{
			return EntityRes.GetString("DuplicateEndName", new object[]
			{
				p0
			});
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x0016C2FC File Offset: 0x0016A4FC
		internal static string AmbiguousEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousEntityContainerEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x0016C324 File Offset: 0x0016A524
		internal static string MissingEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("MissingEntityContainerEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x0016C34C File Offset: 0x0016A54C
		internal static string InvalidEndEntitySetTypeMismatch(object p0)
		{
			return EntityRes.GetString("InvalidEndEntitySetTypeMismatch", new object[]
			{
				p0
			});
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x0016C370 File Offset: 0x0016A570
		internal static string InferRelationshipEndFailedNoEntitySetMatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndFailedNoEntitySetMatch", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x0016C3A4 File Offset: 0x0016A5A4
		internal static string InferRelationshipEndAmbiguous(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndAmbiguous", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x0016C3D8 File Offset: 0x0016A5D8
		internal static string InferRelationshipEndGivesAlreadyDefinedEnd(object p0, object p1)
		{
			return EntityRes.GetString("InferRelationshipEndGivesAlreadyDefinedEnd", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x0016C400 File Offset: 0x0016A600
		internal static string TooManyAssociationEnds(object p0)
		{
			return EntityRes.GetString("TooManyAssociationEnds", new object[]
			{
				p0
			});
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x0016C424 File Offset: 0x0016A624
		internal static string InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEndRoleInRelationshipConstraint", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x0016C44C File Offset: 0x0016A64C
		internal static string InvalidFromPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFromPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x0016C478 File Offset: 0x0016A678
		internal static string InvalidToPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidToPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x0016C4A4 File Offset: 0x0016A6A4
		internal static string InvalidPropertyInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidPropertyInRelationshipConstraint", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x0016C4CC File Offset: 0x0016A6CC
		internal static string TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("TypeMismatchRelationshipConstraint", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x0016C500 File Offset: 0x0016A700
		internal static string InvalidMultiplicityFromRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleUpperBoundMustBeOne", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x0016C528 File Offset: 0x0016A728
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV1", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0016C550 File Offset: 0x0016A750
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV2(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV2", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x0016C578 File Offset: 0x0016A778
		internal static string InvalidMultiplicityFromRoleToPropertyNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNullableV1", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x0016C5A0 File Offset: 0x0016A7A0
		internal static string InvalidMultiplicityToRoleLowerBoundMustBeZero(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleLowerBoundMustBeZero", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x0016C5C8 File Offset: 0x0016A7C8
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeOne", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x0016C5F0 File Offset: 0x0016A7F0
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeMany(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeMany", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x0016C617 File Offset: 0x0016A817
		internal static string MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0016C624 File Offset: 0x0016A824
		internal static string MissingConstraintOnRelationshipType(object p0)
		{
			return EntityRes.GetString("MissingConstraintOnRelationshipType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0016C648 File Offset: 0x0016A848
		internal static string SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("SameRoleReferredInReferentialConstraint", new object[]
			{
				p0
			});
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0016C66C File Offset: 0x0016A86C
		internal static string InvalidPrimitiveTypeKind(object p0)
		{
			return EntityRes.GetString("InvalidPrimitiveTypeKind", new object[]
			{
				p0
			});
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0016C690 File Offset: 0x0016A890
		internal static string EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EntityKeyMustBeScalar", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0016C6B8 File Offset: 0x0016A8B8
		internal static string EntityKeyTypeCurrentlyNotSupportedInSSDL(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupportedInSSDL", new object[]
			{
				p0,
				p1,
				p2,
				p3,
				p4
			});
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0016C6EC File Offset: 0x0016A8EC
		internal static string EntityKeyTypeCurrentlyNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupported", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0016C718 File Offset: 0x0016A918
		internal static string MissingFacetDescription(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MissingFacetDescription", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0016C744 File Offset: 0x0016A944
		internal static string EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0, object p1)
		{
			return EntityRes.GetString("EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x0016C76C File Offset: 0x0016A96C
		internal static string EndWithoutMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("EndWithoutMultiplicity", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0016C794 File Offset: 0x0016A994
		internal static string EntityContainerCannotExtendItself(object p0)
		{
			return EntityRes.GetString("EntityContainerCannotExtendItself", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x0016C7B7 File Offset: 0x0016A9B7
		internal static string ComposableFunctionOrFunctionImportMustDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("ComposableFunctionOrFunctionImportMustDeclareReturnType");
			}
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0016C7C4 File Offset: 0x0016A9C4
		internal static string NonComposableFunctionCannotBeMappedAsComposable(object p0)
		{
			return EntityRes.GetString("NonComposableFunctionCannotBeMappedAsComposable", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x0016C7E7 File Offset: 0x0016A9E7
		internal static string ComposableFunctionImportsReturningEntitiesNotSupported
		{
			get
			{
				return EntityRes.GetString("ComposableFunctionImportsReturningEntitiesNotSupported");
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x0016C7F3 File Offset: 0x0016A9F3
		internal static string StructuralTypeMappingsMustNotBeNullForFunctionImportsReturingNonScalarValues
		{
			get
			{
				return EntityRes.GetString("StructuralTypeMappingsMustNotBeNullForFunctionImportsReturingNonScalarValues");
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x0016C7FF File Offset: 0x0016A9FF
		internal static string InvalidReturnTypeForComposableFunction
		{
			get
			{
				return EntityRes.GetString("InvalidReturnTypeForComposableFunction");
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x0016C80B File Offset: 0x0016AA0B
		internal static string NonComposableFunctionMustNotDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionMustNotDeclareReturnType");
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x0016C817 File Offset: 0x0016AA17
		internal static string CommandTextFunctionsNotComposable
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsNotComposable");
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600517E RID: 20862 RVA: 0x0016C823 File Offset: 0x0016AA23
		internal static string CommandTextFunctionsCannotDeclareStoreFunctionName
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsCannotDeclareStoreFunctionName");
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600517F RID: 20863 RVA: 0x0016C82F File Offset: 0x0016AA2F
		internal static string NonComposableFunctionHasDisallowedAttribute
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionHasDisallowedAttribute");
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x0016C83B File Offset: 0x0016AA3B
		internal static string EmptyDefiningQuery
		{
			get
			{
				return EntityRes.GetString("EmptyDefiningQuery");
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x0016C847 File Offset: 0x0016AA47
		internal static string EmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EmptyCommandText");
			}
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0016C854 File Offset: 0x0016AA54
		internal static string AmbiguousFunctionOverload(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionOverload", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x0016C87C File Offset: 0x0016AA7C
		internal static string AmbiguousFunctionAndType(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionAndType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x0016C8A4 File Offset: 0x0016AAA4
		internal static string CycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("CycleInTypeHierarchy", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x0016C8C7 File Offset: 0x0016AAC7
		internal static string IncorrectProviderManifest
		{
			get
			{
				return EntityRes.GetString("IncorrectProviderManifest");
			}
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x0016C8D4 File Offset: 0x0016AAD4
		internal static string ComplexTypeAsReturnTypeAndDefinedEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndDefinedEntitySet", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x0016C8FC File Offset: 0x0016AAFC
		internal static string ComplexTypeAsReturnTypeAndNestedComplexProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndNestedComplexProperty", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x0016C928 File Offset: 0x0016AB28
		internal static string FacetsOnNonScalarType(object p0)
		{
			return EntityRes.GetString("FacetsOnNonScalarType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06005189 RID: 20873 RVA: 0x0016C94B File Offset: 0x0016AB4B
		internal static string FacetDeclarationRequiresTypeAttribute
		{
			get
			{
				return EntityRes.GetString("FacetDeclarationRequiresTypeAttribute");
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x0016C957 File Offset: 0x0016AB57
		internal static string TypeMustBeDeclared
		{
			get
			{
				return EntityRes.GetString("TypeMustBeDeclared");
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600518B RID: 20875 RVA: 0x0016C963 File Offset: 0x0016AB63
		internal static string RowTypeWithoutProperty
		{
			get
			{
				return EntityRes.GetString("RowTypeWithoutProperty");
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600518C RID: 20876 RVA: 0x0016C96F File Offset: 0x0016AB6F
		internal static string TypeDeclaredAsAttributeAndElement
		{
			get
			{
				return EntityRes.GetString("TypeDeclaredAsAttributeAndElement");
			}
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x0016C97C File Offset: 0x0016AB7C
		internal static string ReferenceToNonEntityType(object p0)
		{
			return EntityRes.GetString("ReferenceToNonEntityType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x0016C9A0 File Offset: 0x0016ABA0
		internal static string NoCodeGenNamespaceInStructuralAnnotation(object p0)
		{
			return EntityRes.GetString("NoCodeGenNamespaceInStructuralAnnotation", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x0016C9C3 File Offset: 0x0016ABC3
		internal static string CannotLoadDifferentVersionOfSchemaInTheSameItemCollection
		{
			get
			{
				return EntityRes.GetString("CannotLoadDifferentVersionOfSchemaInTheSameItemCollection");
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06005190 RID: 20880 RVA: 0x0016C9CF File Offset: 0x0016ABCF
		internal static string InvalidEnumUnderlyingType
		{
			get
			{
				return EntityRes.GetString("InvalidEnumUnderlyingType");
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06005191 RID: 20881 RVA: 0x0016C9DB File Offset: 0x0016ABDB
		internal static string DuplicateEnumMember
		{
			get
			{
				return EntityRes.GetString("DuplicateEnumMember");
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06005192 RID: 20882 RVA: 0x0016C9E7 File Offset: 0x0016ABE7
		internal static string CalculatedEnumValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("CalculatedEnumValueOutOfRange");
			}
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0016C9F4 File Offset: 0x0016ABF4
		internal static string EnumMemberValueOutOfItsUnderylingTypeRange(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EnumMemberValueOutOfItsUnderylingTypeRange", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06005194 RID: 20884 RVA: 0x0016CA1F File Offset: 0x0016AC1F
		internal static string SpatialWithUseStrongSpatialTypesFalse
		{
			get
			{
				return EntityRes.GetString("SpatialWithUseStrongSpatialTypesFalse");
			}
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x0016CA2C File Offset: 0x0016AC2C
		internal static string ObjectQuery_QueryBuilder_InvalidResultType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidResultType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x0016CA4F File Offset: 0x0016AC4F
		internal static string ObjectQuery_QueryBuilder_InvalidQueryArgument
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidQueryArgument");
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x0016CA5B File Offset: 0x0016AC5B
		internal static string ObjectQuery_QueryBuilder_NotSupportedLinqSource
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_NotSupportedLinqSource");
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06005198 RID: 20888 RVA: 0x0016CA67 File Offset: 0x0016AC67
		internal static string ObjectQuery_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_InvalidConnection");
			}
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0016CA74 File Offset: 0x0016AC74
		internal static string ObjectQuery_InvalidQueryName(object p0)
		{
			return EntityRes.GetString("ObjectQuery_InvalidQueryName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600519A RID: 20890 RVA: 0x0016CA97 File Offset: 0x0016AC97
		internal static string ObjectQuery_UnableToMapResultType
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_UnableToMapResultType");
			}
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x0016CAA4 File Offset: 0x0016ACA4
		internal static string ObjectQuery_UnableToMaterializeArray(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArray", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0016CACC File Offset: 0x0016ACCC
		internal static string ObjectQuery_UnableToMaterializeArbitaryProjectionType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArbitaryProjectionType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x0016CAF0 File Offset: 0x0016ACF0
		internal static string ObjectParameter_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0016CB14 File Offset: 0x0016AD14
		internal static string ObjectParameter_InvalidParameterType(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0016CB38 File Offset: 0x0016AD38
		internal static string ObjectParameterCollection_ParameterNameNotFound(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterNameNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x0016CB5C File Offset: 0x0016AD5C
		internal static string ObjectParameterCollection_ParameterAlreadyExists(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterAlreadyExists", new object[]
			{
				p0
			});
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x0016CB80 File Offset: 0x0016AD80
		internal static string ObjectParameterCollection_DuplicateParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_DuplicateParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060051A2 RID: 20898 RVA: 0x0016CBA3 File Offset: 0x0016ADA3
		internal static string ObjectParameterCollection_ParametersLocked
		{
			get
			{
				return EntityRes.GetString("ObjectParameterCollection_ParametersLocked");
			}
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x0016CBB0 File Offset: 0x0016ADB0
		internal static string ProviderReturnedNullForGetDbInformation(object p0)
		{
			return EntityRes.GetString("ProviderReturnedNullForGetDbInformation", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060051A4 RID: 20900 RVA: 0x0016CBD3 File Offset: 0x0016ADD3
		internal static string ProviderReturnedNullForCreateCommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderReturnedNullForCreateCommandDefinition");
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060051A5 RID: 20901 RVA: 0x0016CBDF File Offset: 0x0016ADDF
		internal static string ProviderDidNotReturnAProviderManifest
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifest");
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x0016CBEB File Offset: 0x0016ADEB
		internal static string ProviderDidNotReturnAProviderManifestToken
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifestToken");
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x0016CBF7 File Offset: 0x0016ADF7
		internal static string ProviderDidNotReturnSpatialServices
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnSpatialServices");
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x0016CC03 File Offset: 0x0016AE03
		internal static string SpatialProviderNotUsable
		{
			get
			{
				return EntityRes.GetString("SpatialProviderNotUsable");
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x0016CC0F File Offset: 0x0016AE0F
		internal static string ProviderRequiresStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("ProviderRequiresStoreCommandTree");
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x0016CC1B File Offset: 0x0016AE1B
		internal static string ProviderShouldOverrideEscapeLikeArgument
		{
			get
			{
				return EntityRes.GetString("ProviderShouldOverrideEscapeLikeArgument");
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x0016CC27 File Offset: 0x0016AE27
		internal static string ProviderEscapeLikeArgumentReturnedNull
		{
			get
			{
				return EntityRes.GetString("ProviderEscapeLikeArgumentReturnedNull");
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x0016CC33 File Offset: 0x0016AE33
		internal static string ProviderDidNotCreateACommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotCreateACommandDefinition");
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060051AD RID: 20909 RVA: 0x0016CC3F File Offset: 0x0016AE3F
		internal static string ProviderDoesNotSupportCreateDatabaseScript
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabaseScript");
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060051AE RID: 20910 RVA: 0x0016CC4B File Offset: 0x0016AE4B
		internal static string ProviderDoesNotSupportCreateDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabase");
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x0016CC57 File Offset: 0x0016AE57
		internal static string ProviderDoesNotSupportDatabaseExists
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDatabaseExists");
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060051B0 RID: 20912 RVA: 0x0016CC63 File Offset: 0x0016AE63
		internal static string ProviderDoesNotSupportDeleteDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDeleteDatabase");
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060051B1 RID: 20913 RVA: 0x0016CC6F File Offset: 0x0016AE6F
		internal static string Spatial_GeographyValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_GeographyValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x0016CC7B File Offset: 0x0016AE7B
		internal static string Spatial_GeometryValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_GeometryValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x0016CC87 File Offset: 0x0016AE87
		internal static string Spatial_ProviderValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_ProviderValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060051B4 RID: 20916 RVA: 0x0016CC93 File Offset: 0x0016AE93
		internal static string Spatial_WellKnownValueSerializationPropertyNotDirectlySettable
		{
			get
			{
				return EntityRes.GetString("Spatial_WellKnownValueSerializationPropertyNotDirectlySettable");
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060051B5 RID: 20917 RVA: 0x0016CC9F File Offset: 0x0016AE9F
		internal static string EntityConnectionString_Name
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Name");
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060051B6 RID: 20918 RVA: 0x0016CCAB File Offset: 0x0016AEAB
		internal static string EntityConnectionString_Provider
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Provider");
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060051B7 RID: 20919 RVA: 0x0016CCB7 File Offset: 0x0016AEB7
		internal static string EntityConnectionString_Metadata
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Metadata");
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060051B8 RID: 20920 RVA: 0x0016CCC3 File Offset: 0x0016AEC3
		internal static string EntityConnectionString_ProviderConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_ProviderConnectionString");
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060051B9 RID: 20921 RVA: 0x0016CCCF File Offset: 0x0016AECF
		internal static string EntityDataCategory_Context
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Context");
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060051BA RID: 20922 RVA: 0x0016CCDB File Offset: 0x0016AEDB
		internal static string EntityDataCategory_NamedConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_NamedConnectionString");
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060051BB RID: 20923 RVA: 0x0016CCE7 File Offset: 0x0016AEE7
		internal static string EntityDataCategory_Source
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Source");
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060051BC RID: 20924 RVA: 0x0016CCF3 File Offset: 0x0016AEF3
		internal static string ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection");
			}
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x0016CD00 File Offset: 0x0016AF00
		internal static string ObjectQuery_Span_NoNavProp(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_Span_NoNavProp", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x0016CD27 File Offset: 0x0016AF27
		internal static string ObjectQuery_Span_SpanPathSyntaxError
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_SpanPathSyntaxError");
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x0016CD33 File Offset: 0x0016AF33
		internal static string EntityProxyTypeInfo_ProxyHasWrongWrapper
		{
			get
			{
				return EntityRes.GetString("EntityProxyTypeInfo_ProxyHasWrongWrapper");
			}
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x0016CD40 File Offset: 0x0016AF40
		internal static string EntityProxyTypeInfo_CannotSetEntityCollectionProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_CannotSetEntityCollectionProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x0016CD68 File Offset: 0x0016AF68
		internal static string EntityProxyTypeInfo_ProxyMetadataIsUnavailable(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_ProxyMetadataIsUnavailable", new object[]
			{
				p0
			});
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x0016CD8C File Offset: 0x0016AF8C
		internal static string EntityProxyTypeInfo_DuplicateOSpaceType(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_DuplicateOSpaceType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x0016CDAF File Offset: 0x0016AFAF
		internal static string InvalidEdmMemberInstance
		{
			get
			{
				return EntityRes.GetString("InvalidEdmMemberInstance");
			}
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x0016CDBC File Offset: 0x0016AFBC
		internal static string EF6Providers_NoProviderFound(object p0)
		{
			return EntityRes.GetString("EF6Providers_NoProviderFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x0016CDE0 File Offset: 0x0016AFE0
		internal static string EF6Providers_ProviderTypeMissing(object p0, object p1)
		{
			return EntityRes.GetString("EF6Providers_ProviderTypeMissing", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0016CE08 File Offset: 0x0016B008
		internal static string EF6Providers_InstanceMissing(object p0)
		{
			return EntityRes.GetString("EF6Providers_InstanceMissing", new object[]
			{
				p0
			});
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0016CE2C File Offset: 0x0016B02C
		internal static string EF6Providers_NotDbProviderServices(object p0)
		{
			return EntityRes.GetString("EF6Providers_NotDbProviderServices", new object[]
			{
				p0
			});
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x0016CE50 File Offset: 0x0016B050
		internal static string ProviderInvariantRepeatedInConfig(object p0)
		{
			return EntityRes.GetString("ProviderInvariantRepeatedInConfig", new object[]
			{
				p0
			});
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0016CE74 File Offset: 0x0016B074
		internal static string DbDependencyResolver_NoProviderInvariantName(object p0)
		{
			return EntityRes.GetString("DbDependencyResolver_NoProviderInvariantName", new object[]
			{
				p0
			});
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x0016CE98 File Offset: 0x0016B098
		internal static string DbDependencyResolver_InvalidKey(object p0, object p1)
		{
			return EntityRes.GetString("DbDependencyResolver_InvalidKey", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x0016CEC0 File Offset: 0x0016B0C0
		internal static string DefaultConfigurationUsedBeforeSet(object p0)
		{
			return EntityRes.GetString("DefaultConfigurationUsedBeforeSet", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060051CC RID: 20940 RVA: 0x0016CEE3 File Offset: 0x0016B0E3
		internal static string AddHandlerToInUseConfiguration
		{
			get
			{
				return EntityRes.GetString("AddHandlerToInUseConfiguration");
			}
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0016CEF0 File Offset: 0x0016B0F0
		internal static string ConfigurationSetTwice(object p0, object p1)
		{
			return EntityRes.GetString("ConfigurationSetTwice", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x0016CF18 File Offset: 0x0016B118
		internal static string ConfigurationNotDiscovered(object p0)
		{
			return EntityRes.GetString("ConfigurationNotDiscovered", new object[]
			{
				p0
			});
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0016CF3C File Offset: 0x0016B13C
		internal static string SetConfigurationNotDiscovered(object p0, object p1)
		{
			return EntityRes.GetString("SetConfigurationNotDiscovered", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0016CF64 File Offset: 0x0016B164
		internal static string MultipleConfigsInAssembly(object p0, object p1)
		{
			return EntityRes.GetString("MultipleConfigsInAssembly", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x0016CF8C File Offset: 0x0016B18C
		internal static string CreateInstance_BadMigrationsConfigurationType(object p0, object p1)
		{
			return EntityRes.GetString("CreateInstance_BadMigrationsConfigurationType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0016CFB4 File Offset: 0x0016B1B4
		internal static string CreateInstance_BadSqlGeneratorType(object p0, object p1)
		{
			return EntityRes.GetString("CreateInstance_BadSqlGeneratorType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0016CFDC File Offset: 0x0016B1DC
		internal static string CreateInstance_BadDbConfigurationType(object p0, object p1)
		{
			return EntityRes.GetString("CreateInstance_BadDbConfigurationType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0016D004 File Offset: 0x0016B204
		internal static string DbConfigurationTypeNotFound(object p0)
		{
			return EntityRes.GetString("DbConfigurationTypeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x0016D028 File Offset: 0x0016B228
		internal static string DbConfigurationTypeInAttributeNotFound(object p0)
		{
			return EntityRes.GetString("DbConfigurationTypeInAttributeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x0016D04C File Offset: 0x0016B24C
		internal static string CreateInstance_NoParameterlessConstructor(object p0)
		{
			return EntityRes.GetString("CreateInstance_NoParameterlessConstructor", new object[]
			{
				p0
			});
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x0016D070 File Offset: 0x0016B270
		internal static string CreateInstance_AbstractType(object p0)
		{
			return EntityRes.GetString("CreateInstance_AbstractType", new object[]
			{
				p0
			});
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0016D094 File Offset: 0x0016B294
		internal static string CreateInstance_GenericType(object p0)
		{
			return EntityRes.GetString("CreateInstance_GenericType", new object[]
			{
				p0
			});
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x0016D0B8 File Offset: 0x0016B2B8
		internal static string ConfigurationLocked(object p0)
		{
			return EntityRes.GetString("ConfigurationLocked", new object[]
			{
				p0
			});
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x0016D0DC File Offset: 0x0016B2DC
		internal static string EnableMigrationsForContext(object p0)
		{
			return EntityRes.GetString("EnableMigrationsForContext", new object[]
			{
				p0
			});
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0016D100 File Offset: 0x0016B300
		internal static string EnableMigrations_MultipleContexts(object p0)
		{
			return EntityRes.GetString("EnableMigrations_MultipleContexts", new object[]
			{
				p0
			});
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0016D124 File Offset: 0x0016B324
		internal static string EnableMigrations_MultipleContextsWithName(object p0, object p1)
		{
			return EntityRes.GetString("EnableMigrations_MultipleContextsWithName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0016D14C File Offset: 0x0016B34C
		internal static string EnableMigrations_NoContext(object p0)
		{
			return EntityRes.GetString("EnableMigrations_NoContext", new object[]
			{
				p0
			});
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0016D170 File Offset: 0x0016B370
		internal static string EnableMigrations_NoContextWithName(object p0, object p1)
		{
			return EntityRes.GetString("EnableMigrations_NoContextWithName", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x0016D197 File Offset: 0x0016B397
		internal static string MoreThanOneElement
		{
			get
			{
				return EntityRes.GetString("MoreThanOneElement");
			}
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x0016D1A4 File Offset: 0x0016B3A4
		internal static string IQueryable_Not_Async(object p0)
		{
			return EntityRes.GetString("IQueryable_Not_Async", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060051E1 RID: 20961 RVA: 0x0016D1C7 File Offset: 0x0016B3C7
		internal static string IQueryable_Provider_Not_Async
		{
			get
			{
				return EntityRes.GetString("IQueryable_Provider_Not_Async");
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x0016D1D3 File Offset: 0x0016B3D3
		internal static string EmptySequence
		{
			get
			{
				return EntityRes.GetString("EmptySequence");
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060051E3 RID: 20963 RVA: 0x0016D1DF File Offset: 0x0016B3DF
		internal static string UnableToMoveHistoryTableWithAuto
		{
			get
			{
				return EntityRes.GetString("UnableToMoveHistoryTableWithAuto");
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x0016D1EB File Offset: 0x0016B3EB
		internal static string NoMatch
		{
			get
			{
				return EntityRes.GetString("NoMatch");
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x0016D1F7 File Offset: 0x0016B3F7
		internal static string MoreThanOneMatch
		{
			get
			{
				return EntityRes.GetString("MoreThanOneMatch");
			}
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0016D204 File Offset: 0x0016B404
		internal static string CreateConfigurationType_NoParameterlessConstructor(object p0)
		{
			return EntityRes.GetString("CreateConfigurationType_NoParameterlessConstructor", new object[]
			{
				p0
			});
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x0016D228 File Offset: 0x0016B428
		internal static string CollectionEmpty(object p0, object p1)
		{
			return EntityRes.GetString("CollectionEmpty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x0016D250 File Offset: 0x0016B450
		internal static string DbMigrationsConfiguration_ContextType(object p0)
		{
			return EntityRes.GetString("DbMigrationsConfiguration_ContextType", new object[]
			{
				p0
			});
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0016D274 File Offset: 0x0016B474
		internal static string ContextFactoryContextType(object p0)
		{
			return EntityRes.GetString("ContextFactoryContextType", new object[]
			{
				p0
			});
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0016D298 File Offset: 0x0016B498
		internal static string DbMigrationsConfiguration_RootedPath(object p0)
		{
			return EntityRes.GetString("DbMigrationsConfiguration_RootedPath", new object[]
			{
				p0
			});
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0016D2BC File Offset: 0x0016B4BC
		internal static string ModelBuilder_PropertyFilterTypeMustBePrimitive(object p0)
		{
			return EntityRes.GetString("ModelBuilder_PropertyFilterTypeMustBePrimitive", new object[]
			{
				p0
			});
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0016D2E0 File Offset: 0x0016B4E0
		internal static string LightweightEntityConfiguration_NonScalarProperty(object p0)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_NonScalarProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0016D304 File Offset: 0x0016B504
		internal static string MigrationsPendingException(object p0)
		{
			return EntityRes.GetString("MigrationsPendingException", new object[]
			{
				p0
			});
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x0016D328 File Offset: 0x0016B528
		internal static string ExecutionStrategy_ExistingTransaction(object p0)
		{
			return EntityRes.GetString("ExecutionStrategy_ExistingTransaction", new object[]
			{
				p0
			});
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0016D34C File Offset: 0x0016B54C
		internal static string ExecutionStrategy_MinimumMustBeLessThanMaximum(object p0, object p1)
		{
			return EntityRes.GetString("ExecutionStrategy_MinimumMustBeLessThanMaximum", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0016D374 File Offset: 0x0016B574
		internal static string ExecutionStrategy_NegativeDelay(object p0)
		{
			return EntityRes.GetString("ExecutionStrategy_NegativeDelay", new object[]
			{
				p0
			});
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0016D398 File Offset: 0x0016B598
		internal static string ExecutionStrategy_RetryLimitExceeded(object p0, object p1)
		{
			return EntityRes.GetString("ExecutionStrategy_RetryLimitExceeded", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0016D3C0 File Offset: 0x0016B5C0
		internal static string BaseTypeNotMappedToFunctions(object p0, object p1)
		{
			return EntityRes.GetString("BaseTypeNotMappedToFunctions", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0016D3E8 File Offset: 0x0016B5E8
		internal static string InvalidResourceName(object p0)
		{
			return EntityRes.GetString("InvalidResourceName", new object[]
			{
				p0
			});
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0016D40C File Offset: 0x0016B60C
		internal static string ModificationFunctionParameterNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ModificationFunctionParameterNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060051F5 RID: 20981 RVA: 0x0016D433 File Offset: 0x0016B633
		internal static string EntityClient_CannotOpenBrokenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotOpenBrokenConnection");
			}
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0016D440 File Offset: 0x0016B640
		internal static string ModificationFunctionParameterNotFoundOriginal(object p0, object p1)
		{
			return EntityRes.GetString("ModificationFunctionParameterNotFoundOriginal", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x0016D468 File Offset: 0x0016B668
		internal static string ResultBindingNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ResultBindingNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x0016D490 File Offset: 0x0016B690
		internal static string ConflictingFunctionsMapping(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingFunctionsMapping", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060051F9 RID: 20985 RVA: 0x0016D4B7 File Offset: 0x0016B6B7
		internal static string DbContext_InvalidTransactionForConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_InvalidTransactionForConnection");
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060051FA RID: 20986 RVA: 0x0016D4C3 File Offset: 0x0016B6C3
		internal static string DbContext_InvalidTransactionNoConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_InvalidTransactionNoConnection");
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060051FB RID: 20987 RVA: 0x0016D4CF File Offset: 0x0016B6CF
		internal static string DbContext_TransactionAlreadyStarted
		{
			get
			{
				return EntityRes.GetString("DbContext_TransactionAlreadyStarted");
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060051FC RID: 20988 RVA: 0x0016D4DB File Offset: 0x0016B6DB
		internal static string DbContext_TransactionAlreadyEnlistedInUserTransaction
		{
			get
			{
				return EntityRes.GetString("DbContext_TransactionAlreadyEnlistedInUserTransaction");
			}
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0016D4E8 File Offset: 0x0016B6E8
		internal static string ExecutionStrategy_StreamingNotSupported(object p0)
		{
			return EntityRes.GetString("ExecutionStrategy_StreamingNotSupported", new object[]
			{
				p0
			});
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0016D50C File Offset: 0x0016B70C
		internal static string EdmProperty_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmProperty_InvalidPropertyType", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x0016D52F File Offset: 0x0016B72F
		internal static string ConcurrentMethodInvocation
		{
			get
			{
				return EntityRes.GetString("ConcurrentMethodInvocation");
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06005200 RID: 20992 RVA: 0x0016D53B File Offset: 0x0016B73B
		internal static string AssociationSet_EndEntityTypeMismatch
		{
			get
			{
				return EntityRes.GetString("AssociationSet_EndEntityTypeMismatch");
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x0016D547 File Offset: 0x0016B747
		internal static string VisitDbInExpressionNotImplemented
		{
			get
			{
				return EntityRes.GetString("VisitDbInExpressionNotImplemented");
			}
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0016D554 File Offset: 0x0016B754
		internal static string InvalidColumnBuilderArgument(object p0)
		{
			return EntityRes.GetString("InvalidColumnBuilderArgument", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06005203 RID: 20995 RVA: 0x0016D577 File Offset: 0x0016B777
		internal static string StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed
		{
			get
			{
				return EntityRes.GetString("StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed");
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x0016D583 File Offset: 0x0016B783
		internal static string StorageComplexPropertyMapping_OnlyComplexPropertyAllowed
		{
			get
			{
				return EntityRes.GetString("StorageComplexPropertyMapping_OnlyComplexPropertyAllowed");
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x0016D58F File Offset: 0x0016B78F
		internal static string MetadataItemErrorsFoundDuringGeneration
		{
			get
			{
				return EntityRes.GetString("MetadataItemErrorsFoundDuringGeneration");
			}
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x0016D59C File Offset: 0x0016B79C
		internal static string AutomaticStaleFunctions(object p0)
		{
			return EntityRes.GetString("AutomaticStaleFunctions", new object[]
			{
				p0
			});
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x0016D5BF File Offset: 0x0016B7BF
		internal static string ScaffoldSprocInDownNotSupported
		{
			get
			{
				return EntityRes.GetString("ScaffoldSprocInDownNotSupported");
			}
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0016D5CC File Offset: 0x0016B7CC
		internal static string LightweightEntityConfiguration_ConfigurationConflict_ComplexType(object p0, object p1)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_ConfigurationConflict_ComplexType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x0016D5F4 File Offset: 0x0016B7F4
		internal static string LightweightEntityConfiguration_ConfigurationConflict_IgnoreType(object p0, object p1)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_ConfigurationConflict_IgnoreType", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x0016D61C File Offset: 0x0016B81C
		internal static string AttemptToAddEdmMemberFromWrongDataSpace(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("AttemptToAddEdmMemberFromWrongDataSpace", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x0016D64C File Offset: 0x0016B84C
		internal static string LightweightEntityConfiguration_InvalidNavigationProperty(object p0)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_InvalidNavigationProperty", new object[]
			{
				p0
			});
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x0016D670 File Offset: 0x0016B870
		internal static string LightweightEntityConfiguration_InvalidInverseNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_InvalidInverseNavigationProperty", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0016D6A0 File Offset: 0x0016B8A0
		internal static string LightweightEntityConfiguration_MismatchedInverseNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_MismatchedInverseNavigationProperty", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0016D6D0 File Offset: 0x0016B8D0
		internal static string DuplicateParameterName(object p0)
		{
			return EntityRes.GetString("DuplicateParameterName", new object[]
			{
				p0
			});
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0016D6F4 File Offset: 0x0016B8F4
		internal static string CommandLogFailed(object p0, object p1, object p2)
		{
			return EntityRes.GetString("CommandLogFailed", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0016D720 File Offset: 0x0016B920
		internal static string CommandLogCanceled(object p0, object p1)
		{
			return EntityRes.GetString("CommandLogCanceled", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x0016D748 File Offset: 0x0016B948
		internal static string CommandLogComplete(object p0, object p1, object p2)
		{
			return EntityRes.GetString("CommandLogComplete", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x0016D774 File Offset: 0x0016B974
		internal static string CommandLogAsync(object p0, object p1)
		{
			return EntityRes.GetString("CommandLogAsync", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x0016D79C File Offset: 0x0016B99C
		internal static string CommandLogNonAsync(object p0, object p1)
		{
			return EntityRes.GetString("CommandLogNonAsync", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06005214 RID: 21012 RVA: 0x0016D7C3 File Offset: 0x0016B9C3
		internal static string SuppressionAfterExecution
		{
			get
			{
				return EntityRes.GetString("SuppressionAfterExecution");
			}
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x0016D7D0 File Offset: 0x0016B9D0
		internal static string BadContextTypeForDiscovery(object p0)
		{
			return EntityRes.GetString("BadContextTypeForDiscovery", new object[]
			{
				p0
			});
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0016D7F4 File Offset: 0x0016B9F4
		internal static string ErrorGeneratingCommandTree(object p0, object p1)
		{
			return EntityRes.GetString("ErrorGeneratingCommandTree", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x0016D81C File Offset: 0x0016BA1C
		internal static string LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity(object p0, object p1, object p2)
		{
			return EntityRes.GetString("LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x0016D848 File Offset: 0x0016BA48
		internal static string LightweightNavigationPropertyConfiguration_InvalidMultiplicity(object p0)
		{
			return EntityRes.GetString("LightweightNavigationPropertyConfiguration_InvalidMultiplicity", new object[]
			{
				p0
			});
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0016D86C File Offset: 0x0016BA6C
		internal static string LightweightPrimitivePropertyConfiguration_NonNullableProperty(object p0, object p1)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_NonNullableProperty", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x0016D894 File Offset: 0x0016BA94
		internal static string TestDoubleNotImplemented(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TestDoubleNotImplemented", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x0600521B RID: 21019 RVA: 0x0016D8BF File Offset: 0x0016BABF
		internal static string TestDoublesCannotBeConverted
		{
			get
			{
				return EntityRes.GetString("TestDoublesCannotBeConverted");
			}
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0016D8CC File Offset: 0x0016BACC
		internal static string InvalidNavigationPropertyComplexType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidNavigationPropertyComplexType", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0016D8F8 File Offset: 0x0016BAF8
		internal static string ConventionsConfiguration_InvalidConventionType(object p0)
		{
			return EntityRes.GetString("ConventionsConfiguration_InvalidConventionType", new object[]
			{
				p0
			});
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0016D91C File Offset: 0x0016BB1C
		internal static string ConventionsConfiguration_ConventionTypeMissmatch(object p0, object p1)
		{
			return EntityRes.GetString("ConventionsConfiguration_ConventionTypeMissmatch", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x0016D944 File Offset: 0x0016BB44
		internal static string LightweightPrimitivePropertyConfiguration_DateTimeScale(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_DateTimeScale", new object[]
			{
				p0
			});
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0016D968 File Offset: 0x0016BB68
		internal static string LightweightPrimitivePropertyConfiguration_DecimalNoScale(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_DecimalNoScale", new object[]
			{
				p0
			});
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0016D98C File Offset: 0x0016BB8C
		internal static string LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime", new object[]
			{
				p0
			});
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0016D9B0 File Offset: 0x0016BBB0
		internal static string LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal", new object[]
			{
				p0
			});
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0016D9D4 File Offset: 0x0016BBD4
		internal static string LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary", new object[]
			{
				p0
			});
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0016D9F8 File Offset: 0x0016BBF8
		internal static string LightweightPrimitivePropertyConfiguration_IsUnicodeNonString(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_IsUnicodeNonString", new object[]
			{
				p0
			});
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0016DA1C File Offset: 0x0016BC1C
		internal static string LightweightPrimitivePropertyConfiguration_NonLength(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_NonLength", new object[]
			{
				p0
			});
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x0016DA3F File Offset: 0x0016BC3F
		internal static string UnableToUpgradeHistoryWhenCustomFactory
		{
			get
			{
				return EntityRes.GetString("UnableToUpgradeHistoryWhenCustomFactory");
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06005227 RID: 21031 RVA: 0x0016DA4B File Offset: 0x0016BC4B
		internal static string CommitFailed
		{
			get
			{
				return EntityRes.GetString("CommitFailed");
			}
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0016DA58 File Offset: 0x0016BC58
		internal static string InterceptorTypeNotFound(object p0)
		{
			return EntityRes.GetString("InterceptorTypeNotFound", new object[]
			{
				p0
			});
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x0016DA7C File Offset: 0x0016BC7C
		internal static string InterceptorTypeNotInterceptor(object p0)
		{
			return EntityRes.GetString("InterceptorTypeNotInterceptor", new object[]
			{
				p0
			});
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x0016DAA0 File Offset: 0x0016BCA0
		internal static string ViewGenContainersNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ViewGenContainersNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0016DAC8 File Offset: 0x0016BCC8
		internal static string HashCalcContainersNotFound(object p0, object p1)
		{
			return EntityRes.GetString("HashCalcContainersNotFound", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x0600522C RID: 21036 RVA: 0x0016DAEF File Offset: 0x0016BCEF
		internal static string ViewGenMultipleContainers
		{
			get
			{
				return EntityRes.GetString("ViewGenMultipleContainers");
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600522D RID: 21037 RVA: 0x0016DAFB File Offset: 0x0016BCFB
		internal static string HashCalcMultipleContainers
		{
			get
			{
				return EntityRes.GetString("HashCalcMultipleContainers");
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x0600522E RID: 21038 RVA: 0x0016DB07 File Offset: 0x0016BD07
		internal static string BadConnectionWrapping
		{
			get
			{
				return EntityRes.GetString("BadConnectionWrapping");
			}
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0016DB14 File Offset: 0x0016BD14
		internal static string ConnectionClosedLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionClosedLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0016DB3C File Offset: 0x0016BD3C
		internal static string ConnectionCloseErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConnectionCloseErrorLog", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0016DB68 File Offset: 0x0016BD68
		internal static string ConnectionOpenedLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionOpenedLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x0016DB90 File Offset: 0x0016BD90
		internal static string ConnectionOpenErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConnectionOpenErrorLog", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x0016DBBC File Offset: 0x0016BDBC
		internal static string ConnectionOpenedLogAsync(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionOpenedLogAsync", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0016DBE4 File Offset: 0x0016BDE4
		internal static string ConnectionOpenErrorLogAsync(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConnectionOpenErrorLogAsync", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0016DC10 File Offset: 0x0016BE10
		internal static string TransactionStartedLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionStartedLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x0016DC38 File Offset: 0x0016BE38
		internal static string TransactionStartErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TransactionStartErrorLog", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0016DC64 File Offset: 0x0016BE64
		internal static string TransactionCommittedLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionCommittedLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x0016DC8C File Offset: 0x0016BE8C
		internal static string TransactionCommitErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TransactionCommitErrorLog", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x0016DCB8 File Offset: 0x0016BEB8
		internal static string TransactionRolledBackLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionRolledBackLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x0016DCE0 File Offset: 0x0016BEE0
		internal static string TransactionRollbackErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TransactionRollbackErrorLog", new object[]
			{
				p0,
				p1,
				p2
			});
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x0016DD0C File Offset: 0x0016BF0C
		internal static string ConnectionOpenCanceledLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionOpenCanceledLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x0016DD33 File Offset: 0x0016BF33
		internal static string TransactionHandler_AlreadyInitialized
		{
			get
			{
				return EntityRes.GetString("TransactionHandler_AlreadyInitialized");
			}
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x0016DD40 File Offset: 0x0016BF40
		internal static string ConnectionDisposedLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionDisposedLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x0016DD68 File Offset: 0x0016BF68
		internal static string TransactionDisposedLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionDisposedLog", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x0016DD90 File Offset: 0x0016BF90
		internal static string UnableToLoadEmbeddedResource(object p0, object p1)
		{
			return EntityRes.GetString("UnableToLoadEmbeddedResource", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0016DDB8 File Offset: 0x0016BFB8
		internal static string CannotSetBaseTypeCyclicInheritance(object p0, object p1)
		{
			return EntityRes.GetString("CannotSetBaseTypeCyclicInheritance", new object[]
			{
				p0,
				p1
			});
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06005241 RID: 21057 RVA: 0x0016DDDF File Offset: 0x0016BFDF
		internal static string CannotDefineKeysOnBothBaseAndDerivedTypes
		{
			get
			{
				return EntityRes.GetString("CannotDefineKeysOnBothBaseAndDerivedTypes");
			}
		}
	}
}
