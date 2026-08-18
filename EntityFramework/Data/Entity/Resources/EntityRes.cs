using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace System.Data.Entity.Resources
{
	// Token: 0x0200072F RID: 1839
	[GeneratedCode("Resources.tt", "1.0.0.0")]
	internal sealed class EntityRes
	{
		// Token: 0x0600530D RID: 21261 RVA: 0x0016E870 File Offset: 0x0016CA70
		private EntityRes()
		{
			this.resources = new ResourceManager("System.Data.Entity.Properties.Resources", typeof(DbContext).GetTypeInfo().Assembly);
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0016E89C File Offset: 0x0016CA9C
		private static EntityRes GetLoader()
		{
			if (EntityRes.loader == null)
			{
				EntityRes value = new EntityRes();
				Interlocked.CompareExchange<EntityRes>(ref EntityRes.loader, value, null);
			}
			return EntityRes.loader;
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x0600530F RID: 21263 RVA: 0x0016E8C8 File Offset: 0x0016CAC8
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005310 RID: 21264 RVA: 0x0016E8CB File Offset: 0x0016CACB
		public static ResourceManager Resources
		{
			get
			{
				return EntityRes.GetLoader().resources;
			}
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x0016E8D8 File Offset: 0x0016CAD8
		public static string GetString(string name, params object[] args)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			string @string = entityRes.resources.GetString(name, EntityRes.Culture);
			if (args != null && args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0016E95C File Offset: 0x0016CB5C
		public static string GetString(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetString(name, EntityRes.Culture);
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x0016E985 File Offset: 0x0016CB85
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return EntityRes.GetString(name);
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0016E990 File Offset: 0x0016CB90
		public static object GetObject(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetObject(name, EntityRes.Culture);
		}

		// Token: 0x04001B74 RID: 7028
		internal const string AutomaticMigration = "AutomaticMigration";

		// Token: 0x04001B75 RID: 7029
		internal const string BootstrapMigration = "BootstrapMigration";

		// Token: 0x04001B76 RID: 7030
		internal const string InitialCreate = "InitialCreate";

		// Token: 0x04001B77 RID: 7031
		internal const string AutomaticDataLoss = "AutomaticDataLoss";

		// Token: 0x04001B78 RID: 7032
		internal const string LoggingAutoMigrate = "LoggingAutoMigrate";

		// Token: 0x04001B79 RID: 7033
		internal const string LoggingRevertAutoMigrate = "LoggingRevertAutoMigrate";

		// Token: 0x04001B7A RID: 7034
		internal const string LoggingApplyMigration = "LoggingApplyMigration";

		// Token: 0x04001B7B RID: 7035
		internal const string LoggingRevertMigration = "LoggingRevertMigration";

		// Token: 0x04001B7C RID: 7036
		internal const string LoggingSeedingDatabase = "LoggingSeedingDatabase";

		// Token: 0x04001B7D RID: 7037
		internal const string LoggingPendingMigrations = "LoggingPendingMigrations";

		// Token: 0x04001B7E RID: 7038
		internal const string LoggingPendingMigrationsDown = "LoggingPendingMigrationsDown";

		// Token: 0x04001B7F RID: 7039
		internal const string LoggingNoExplicitMigrations = "LoggingNoExplicitMigrations";

		// Token: 0x04001B80 RID: 7040
		internal const string LoggingAlreadyAtTarget = "LoggingAlreadyAtTarget";

		// Token: 0x04001B81 RID: 7041
		internal const string LoggingTargetDatabase = "LoggingTargetDatabase";

		// Token: 0x04001B82 RID: 7042
		internal const string LoggingTargetDatabaseFormat = "LoggingTargetDatabaseFormat";

		// Token: 0x04001B83 RID: 7043
		internal const string LoggingExplicit = "LoggingExplicit";

		// Token: 0x04001B84 RID: 7044
		internal const string UpgradingHistoryTable = "UpgradingHistoryTable";

		// Token: 0x04001B85 RID: 7045
		internal const string MetadataOutOfDate = "MetadataOutOfDate";

		// Token: 0x04001B86 RID: 7046
		internal const string MigrationNotFound = "MigrationNotFound";

		// Token: 0x04001B87 RID: 7047
		internal const string PartialFkOperation = "PartialFkOperation";

		// Token: 0x04001B88 RID: 7048
		internal const string AutoNotValidTarget = "AutoNotValidTarget";

		// Token: 0x04001B89 RID: 7049
		internal const string AutoNotValidForScriptWindows = "AutoNotValidForScriptWindows";

		// Token: 0x04001B8A RID: 7050
		internal const string ContextNotConstructible = "ContextNotConstructible";

		// Token: 0x04001B8B RID: 7051
		internal const string AmbiguousMigrationName = "AmbiguousMigrationName";

		// Token: 0x04001B8C RID: 7052
		internal const string AutomaticDisabledException = "AutomaticDisabledException";

		// Token: 0x04001B8D RID: 7053
		internal const string DownScriptWindowsNotSupported = "DownScriptWindowsNotSupported";

		// Token: 0x04001B8E RID: 7054
		internal const string AssemblyMigrator_NoConfigurationWithName = "AssemblyMigrator_NoConfigurationWithName";

		// Token: 0x04001B8F RID: 7055
		internal const string AssemblyMigrator_MultipleConfigurationsWithName = "AssemblyMigrator_MultipleConfigurationsWithName";

		// Token: 0x04001B90 RID: 7056
		internal const string AssemblyMigrator_NoConfiguration = "AssemblyMigrator_NoConfiguration";

		// Token: 0x04001B91 RID: 7057
		internal const string AssemblyMigrator_MultipleConfigurations = "AssemblyMigrator_MultipleConfigurations";

		// Token: 0x04001B92 RID: 7058
		internal const string MigrationsNamespaceNotUnderRootNamespace = "MigrationsNamespaceNotUnderRootNamespace";

		// Token: 0x04001B93 RID: 7059
		internal const string UnableToDispatchAddOrUpdate = "UnableToDispatchAddOrUpdate";

		// Token: 0x04001B94 RID: 7060
		internal const string NoSqlGeneratorForProvider = "NoSqlGeneratorForProvider";

		// Token: 0x04001B95 RID: 7061
		internal const string ToolingFacade_AssemblyNotFound = "ToolingFacade_AssemblyNotFound";

		// Token: 0x04001B96 RID: 7062
		internal const string ArgumentIsNullOrWhitespace = "ArgumentIsNullOrWhitespace";

		// Token: 0x04001B97 RID: 7063
		internal const string EntityTypeConfigurationMismatch = "EntityTypeConfigurationMismatch";

		// Token: 0x04001B98 RID: 7064
		internal const string ComplexTypeConfigurationMismatch = "ComplexTypeConfigurationMismatch";

		// Token: 0x04001B99 RID: 7065
		internal const string KeyPropertyNotFound = "KeyPropertyNotFound";

		// Token: 0x04001B9A RID: 7066
		internal const string ForeignKeyPropertyNotFound = "ForeignKeyPropertyNotFound";

		// Token: 0x04001B9B RID: 7067
		internal const string PropertyNotFound = "PropertyNotFound";

		// Token: 0x04001B9C RID: 7068
		internal const string NavigationPropertyNotFound = "NavigationPropertyNotFound";

		// Token: 0x04001B9D RID: 7069
		internal const string InvalidPropertyExpression = "InvalidPropertyExpression";

		// Token: 0x04001B9E RID: 7070
		internal const string InvalidComplexPropertyExpression = "InvalidComplexPropertyExpression";

		// Token: 0x04001B9F RID: 7071
		internal const string InvalidPropertiesExpression = "InvalidPropertiesExpression";

		// Token: 0x04001BA0 RID: 7072
		internal const string InvalidComplexPropertiesExpression = "InvalidComplexPropertiesExpression";

		// Token: 0x04001BA1 RID: 7073
		internal const string DuplicateStructuralTypeConfiguration = "DuplicateStructuralTypeConfiguration";

		// Token: 0x04001BA2 RID: 7074
		internal const string ConflictingPropertyConfiguration = "ConflictingPropertyConfiguration";

		// Token: 0x04001BA3 RID: 7075
		internal const string ConflictingTypeAnnotation = "ConflictingTypeAnnotation";

		// Token: 0x04001BA4 RID: 7076
		internal const string ConflictingColumnConfiguration = "ConflictingColumnConfiguration";

		// Token: 0x04001BA5 RID: 7077
		internal const string ConflictingConfigurationValue = "ConflictingConfigurationValue";

		// Token: 0x04001BA6 RID: 7078
		internal const string ConflictingAnnotationValue = "ConflictingAnnotationValue";

		// Token: 0x04001BA7 RID: 7079
		internal const string ConflictingIndexAttributeProperty = "ConflictingIndexAttributeProperty";

		// Token: 0x04001BA8 RID: 7080
		internal const string ConflictingIndexAttribute = "ConflictingIndexAttribute";

		// Token: 0x04001BA9 RID: 7081
		internal const string ConflictingIndexAttributesOnProperty = "ConflictingIndexAttributesOnProperty";

		// Token: 0x04001BAA RID: 7082
		internal const string IncompatibleTypes = "IncompatibleTypes";

		// Token: 0x04001BAB RID: 7083
		internal const string AnnotationSerializeWrongType = "AnnotationSerializeWrongType";

		// Token: 0x04001BAC RID: 7084
		internal const string AnnotationSerializeBadFormat = "AnnotationSerializeBadFormat";

		// Token: 0x04001BAD RID: 7085
		internal const string ConflictWhenConsolidating = "ConflictWhenConsolidating";

		// Token: 0x04001BAE RID: 7086
		internal const string OrderConflictWhenConsolidating = "OrderConflictWhenConsolidating";

		// Token: 0x04001BAF RID: 7087
		internal const string CodeFirstInvalidComplexType = "CodeFirstInvalidComplexType";

		// Token: 0x04001BB0 RID: 7088
		internal const string InvalidEntityType = "InvalidEntityType";

		// Token: 0x04001BB1 RID: 7089
		internal const string SimpleNameCollision = "SimpleNameCollision";

		// Token: 0x04001BB2 RID: 7090
		internal const string NavigationInverseItself = "NavigationInverseItself";

		// Token: 0x04001BB3 RID: 7091
		internal const string ConflictingConstraint = "ConflictingConstraint";

		// Token: 0x04001BB4 RID: 7092
		internal const string ConflictingInferredColumnType = "ConflictingInferredColumnType";

		// Token: 0x04001BB5 RID: 7093
		internal const string ConflictingMapping = "ConflictingMapping";

		// Token: 0x04001BB6 RID: 7094
		internal const string ConflictingCascadeDeleteOperation = "ConflictingCascadeDeleteOperation";

		// Token: 0x04001BB7 RID: 7095
		internal const string ConflictingMultiplicities = "ConflictingMultiplicities";

		// Token: 0x04001BB8 RID: 7096
		internal const string MaxLengthAttributeConvention_InvalidMaxLength = "MaxLengthAttributeConvention_InvalidMaxLength";

		// Token: 0x04001BB9 RID: 7097
		internal const string StringLengthAttributeConvention_InvalidMaximumLength = "StringLengthAttributeConvention_InvalidMaximumLength";

		// Token: 0x04001BBA RID: 7098
		internal const string ModelGeneration_UnableToDetermineKeyOrder = "ModelGeneration_UnableToDetermineKeyOrder";

		// Token: 0x04001BBB RID: 7099
		internal const string ForeignKeyAttributeConvention_EmptyKey = "ForeignKeyAttributeConvention_EmptyKey";

		// Token: 0x04001BBC RID: 7100
		internal const string ForeignKeyAttributeConvention_InvalidKey = "ForeignKeyAttributeConvention_InvalidKey";

		// Token: 0x04001BBD RID: 7101
		internal const string ForeignKeyAttributeConvention_InvalidNavigationProperty = "ForeignKeyAttributeConvention_InvalidNavigationProperty";

		// Token: 0x04001BBE RID: 7102
		internal const string ForeignKeyAttributeConvention_OrderRequired = "ForeignKeyAttributeConvention_OrderRequired";

		// Token: 0x04001BBF RID: 7103
		internal const string InversePropertyAttributeConvention_PropertyNotFound = "InversePropertyAttributeConvention_PropertyNotFound";

		// Token: 0x04001BC0 RID: 7104
		internal const string InversePropertyAttributeConvention_SelfInverseDetected = "InversePropertyAttributeConvention_SelfInverseDetected";

		// Token: 0x04001BC1 RID: 7105
		internal const string ValidationHeader = "ValidationHeader";

		// Token: 0x04001BC2 RID: 7106
		internal const string ValidationItemFormat = "ValidationItemFormat";

		// Token: 0x04001BC3 RID: 7107
		internal const string KeyRegisteredOnDerivedType = "KeyRegisteredOnDerivedType";

		// Token: 0x04001BC4 RID: 7108
		internal const string InvalidTableMapping = "InvalidTableMapping";

		// Token: 0x04001BC5 RID: 7109
		internal const string InvalidTableMapping_NoTableName = "InvalidTableMapping_NoTableName";

		// Token: 0x04001BC6 RID: 7110
		internal const string InvalidChainedMappingSyntax = "InvalidChainedMappingSyntax";

		// Token: 0x04001BC7 RID: 7111
		internal const string InvalidNotNullCondition = "InvalidNotNullCondition";

		// Token: 0x04001BC8 RID: 7112
		internal const string InvalidDiscriminatorType = "InvalidDiscriminatorType";

		// Token: 0x04001BC9 RID: 7113
		internal const string ConventionNotFound = "ConventionNotFound";

		// Token: 0x04001BCA RID: 7114
		internal const string InvalidEntitySplittingProperties = "InvalidEntitySplittingProperties";

		// Token: 0x04001BCB RID: 7115
		internal const string ProviderNameNotFound = "ProviderNameNotFound";

		// Token: 0x04001BCC RID: 7116
		internal const string ProviderNotFound = "ProviderNotFound";

		// Token: 0x04001BCD RID: 7117
		internal const string InvalidDatabaseName = "InvalidDatabaseName";

		// Token: 0x04001BCE RID: 7118
		internal const string EntityMappingConfiguration_DuplicateMapInheritedProperties = "EntityMappingConfiguration_DuplicateMapInheritedProperties";

		// Token: 0x04001BCF RID: 7119
		internal const string EntityMappingConfiguration_DuplicateMappedProperties = "EntityMappingConfiguration_DuplicateMappedProperties";

		// Token: 0x04001BD0 RID: 7120
		internal const string EntityMappingConfiguration_DuplicateMappedProperty = "EntityMappingConfiguration_DuplicateMappedProperty";

		// Token: 0x04001BD1 RID: 7121
		internal const string EntityMappingConfiguration_CannotMapIgnoredProperty = "EntityMappingConfiguration_CannotMapIgnoredProperty";

		// Token: 0x04001BD2 RID: 7122
		internal const string EntityMappingConfiguration_InvalidTableSharing = "EntityMappingConfiguration_InvalidTableSharing";

		// Token: 0x04001BD3 RID: 7123
		internal const string EntityMappingConfiguration_TPCWithIAsOnNonLeafType = "EntityMappingConfiguration_TPCWithIAsOnNonLeafType";

		// Token: 0x04001BD4 RID: 7124
		internal const string CannotIgnoreMappedBaseProperty = "CannotIgnoreMappedBaseProperty";

		// Token: 0x04001BD5 RID: 7125
		internal const string ModelBuilder_KeyPropertiesMustBePrimitive = "ModelBuilder_KeyPropertiesMustBePrimitive";

		// Token: 0x04001BD6 RID: 7126
		internal const string TableNotFound = "TableNotFound";

		// Token: 0x04001BD7 RID: 7127
		internal const string IncorrectColumnCount = "IncorrectColumnCount";

		// Token: 0x04001BD8 RID: 7128
		internal const string BadKeyNameForAnnotation = "BadKeyNameForAnnotation";

		// Token: 0x04001BD9 RID: 7129
		internal const string BadAnnotationName = "BadAnnotationName";

		// Token: 0x04001BDA RID: 7130
		internal const string CircularComplexTypeHierarchy = "CircularComplexTypeHierarchy";

		// Token: 0x04001BDB RID: 7131
		internal const string UnableToDeterminePrincipal = "UnableToDeterminePrincipal";

		// Token: 0x04001BDC RID: 7132
		internal const string UnmappedAbstractType = "UnmappedAbstractType";

		// Token: 0x04001BDD RID: 7133
		internal const string UnsupportedHybridInheritanceMapping = "UnsupportedHybridInheritanceMapping";

		// Token: 0x04001BDE RID: 7134
		internal const string OrphanedConfiguredTableDetected = "OrphanedConfiguredTableDetected";

		// Token: 0x04001BDF RID: 7135
		internal const string BadTphMappingToSharedColumn = "BadTphMappingToSharedColumn";

		// Token: 0x04001BE0 RID: 7136
		internal const string DuplicateConfiguredColumnOrder = "DuplicateConfiguredColumnOrder";

		// Token: 0x04001BE1 RID: 7137
		internal const string UnsupportedUseOfV3Type = "UnsupportedUseOfV3Type";

		// Token: 0x04001BE2 RID: 7138
		internal const string MultiplePropertiesMatchedAsKeys = "MultiplePropertiesMatchedAsKeys";

		// Token: 0x04001BE3 RID: 7139
		internal const string FailedToGetProviderInformation = "FailedToGetProviderInformation";

		// Token: 0x04001BE4 RID: 7140
		internal const string DbPropertyEntry_CannotGetCurrentValue = "DbPropertyEntry_CannotGetCurrentValue";

		// Token: 0x04001BE5 RID: 7141
		internal const string DbPropertyEntry_CannotSetCurrentValue = "DbPropertyEntry_CannotSetCurrentValue";

		// Token: 0x04001BE6 RID: 7142
		internal const string DbPropertyEntry_NotSupportedForDetached = "DbPropertyEntry_NotSupportedForDetached";

		// Token: 0x04001BE7 RID: 7143
		internal const string DbPropertyEntry_SettingEntityRefNotSupported = "DbPropertyEntry_SettingEntityRefNotSupported";

		// Token: 0x04001BE8 RID: 7144
		internal const string DbPropertyEntry_NotSupportedForPropertiesNotInTheModel = "DbPropertyEntry_NotSupportedForPropertiesNotInTheModel";

		// Token: 0x04001BE9 RID: 7145
		internal const string DbEntityEntry_NotSupportedForDetached = "DbEntityEntry_NotSupportedForDetached";

		// Token: 0x04001BEA RID: 7146
		internal const string DbSet_BadTypeForAddAttachRemove = "DbSet_BadTypeForAddAttachRemove";

		// Token: 0x04001BEB RID: 7147
		internal const string DbSet_BadTypeForCreate = "DbSet_BadTypeForCreate";

		// Token: 0x04001BEC RID: 7148
		internal const string DbEntity_BadTypeForCast = "DbEntity_BadTypeForCast";

		// Token: 0x04001BED RID: 7149
		internal const string DbMember_BadTypeForCast = "DbMember_BadTypeForCast";

		// Token: 0x04001BEE RID: 7150
		internal const string DbEntityEntry_UsedReferenceForCollectionProp = "DbEntityEntry_UsedReferenceForCollectionProp";

		// Token: 0x04001BEF RID: 7151
		internal const string DbEntityEntry_UsedCollectionForReferenceProp = "DbEntityEntry_UsedCollectionForReferenceProp";

		// Token: 0x04001BF0 RID: 7152
		internal const string DbEntityEntry_NotANavigationProperty = "DbEntityEntry_NotANavigationProperty";

		// Token: 0x04001BF1 RID: 7153
		internal const string DbEntityEntry_NotAScalarProperty = "DbEntityEntry_NotAScalarProperty";

		// Token: 0x04001BF2 RID: 7154
		internal const string DbEntityEntry_NotAComplexProperty = "DbEntityEntry_NotAComplexProperty";

		// Token: 0x04001BF3 RID: 7155
		internal const string DbEntityEntry_NotAProperty = "DbEntityEntry_NotAProperty";

		// Token: 0x04001BF4 RID: 7156
		internal const string DbEntityEntry_DottedPartNotComplex = "DbEntityEntry_DottedPartNotComplex";

		// Token: 0x04001BF5 RID: 7157
		internal const string DbEntityEntry_DottedPathMustBeProperty = "DbEntityEntry_DottedPathMustBeProperty";

		// Token: 0x04001BF6 RID: 7158
		internal const string DbEntityEntry_WrongGenericForNavProp = "DbEntityEntry_WrongGenericForNavProp";

		// Token: 0x04001BF7 RID: 7159
		internal const string DbEntityEntry_WrongGenericForCollectionNavProp = "DbEntityEntry_WrongGenericForCollectionNavProp";

		// Token: 0x04001BF8 RID: 7160
		internal const string DbEntityEntry_WrongGenericForProp = "DbEntityEntry_WrongGenericForProp";

		// Token: 0x04001BF9 RID: 7161
		internal const string DbEntityEntry_BadPropertyExpression = "DbEntityEntry_BadPropertyExpression";

		// Token: 0x04001BFA RID: 7162
		internal const string DbContext_IndependentAssociationUpdateException = "DbContext_IndependentAssociationUpdateException";

		// Token: 0x04001BFB RID: 7163
		internal const string DbPropertyValues_CannotGetValuesForState = "DbPropertyValues_CannotGetValuesForState";

		// Token: 0x04001BFC RID: 7164
		internal const string DbPropertyValues_CannotSetNullValue = "DbPropertyValues_CannotSetNullValue";

		// Token: 0x04001BFD RID: 7165
		internal const string DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull = "DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull";

		// Token: 0x04001BFE RID: 7166
		internal const string DbPropertyValues_WrongTypeForAssignment = "DbPropertyValues_WrongTypeForAssignment";

		// Token: 0x04001BFF RID: 7167
		internal const string DbPropertyValues_PropertyValueNamesAreReadonly = "DbPropertyValues_PropertyValueNamesAreReadonly";

		// Token: 0x04001C00 RID: 7168
		internal const string DbPropertyValues_PropertyDoesNotExist = "DbPropertyValues_PropertyDoesNotExist";

		// Token: 0x04001C01 RID: 7169
		internal const string DbPropertyValues_AttemptToSetValuesFromWrongObject = "DbPropertyValues_AttemptToSetValuesFromWrongObject";

		// Token: 0x04001C02 RID: 7170
		internal const string DbPropertyValues_AttemptToSetValuesFromWrongType = "DbPropertyValues_AttemptToSetValuesFromWrongType";

		// Token: 0x04001C03 RID: 7171
		internal const string DbPropertyValues_AttemptToSetNonValuesOnComplexProperty = "DbPropertyValues_AttemptToSetNonValuesOnComplexProperty";

		// Token: 0x04001C04 RID: 7172
		internal const string DbPropertyValues_ComplexObjectCannotBeNull = "DbPropertyValues_ComplexObjectCannotBeNull";

		// Token: 0x04001C05 RID: 7173
		internal const string DbPropertyValues_NestedPropertyValuesNull = "DbPropertyValues_NestedPropertyValuesNull";

		// Token: 0x04001C06 RID: 7174
		internal const string DbPropertyValues_CannotSetPropertyOnNullCurrentValue = "DbPropertyValues_CannotSetPropertyOnNullCurrentValue";

		// Token: 0x04001C07 RID: 7175
		internal const string DbPropertyValues_CannotSetPropertyOnNullOriginalValue = "DbPropertyValues_CannotSetPropertyOnNullOriginalValue";

		// Token: 0x04001C08 RID: 7176
		internal const string DatabaseInitializationStrategy_ModelMismatch = "DatabaseInitializationStrategy_ModelMismatch";

		// Token: 0x04001C09 RID: 7177
		internal const string Database_DatabaseAlreadyExists = "Database_DatabaseAlreadyExists";

		// Token: 0x04001C0A RID: 7178
		internal const string Database_NonCodeFirstCompatibilityCheck = "Database_NonCodeFirstCompatibilityCheck";

		// Token: 0x04001C0B RID: 7179
		internal const string Database_NoDatabaseMetadata = "Database_NoDatabaseMetadata";

		// Token: 0x04001C0C RID: 7180
		internal const string Database_BadLegacyInitializerEntry = "Database_BadLegacyInitializerEntry";

		// Token: 0x04001C0D RID: 7181
		internal const string Database_InitializeFromLegacyConfigFailed = "Database_InitializeFromLegacyConfigFailed";

		// Token: 0x04001C0E RID: 7182
		internal const string Database_InitializeFromConfigFailed = "Database_InitializeFromConfigFailed";

		// Token: 0x04001C0F RID: 7183
		internal const string ContextConfiguredMultipleTimes = "ContextConfiguredMultipleTimes";

		// Token: 0x04001C10 RID: 7184
		internal const string SetConnectionFactoryFromConfigFailed = "SetConnectionFactoryFromConfigFailed";

		// Token: 0x04001C11 RID: 7185
		internal const string DbContext_ContextUsedInModelCreating = "DbContext_ContextUsedInModelCreating";

		// Token: 0x04001C12 RID: 7186
		internal const string DbContext_MESTNotSupported = "DbContext_MESTNotSupported";

		// Token: 0x04001C13 RID: 7187
		internal const string DbContext_Disposed = "DbContext_Disposed";

		// Token: 0x04001C14 RID: 7188
		internal const string DbContext_ProviderReturnedNullConnection = "DbContext_ProviderReturnedNullConnection";

		// Token: 0x04001C15 RID: 7189
		internal const string DbContext_ProviderNameMissing = "DbContext_ProviderNameMissing";

		// Token: 0x04001C16 RID: 7190
		internal const string DbContext_ConnectionFactoryReturnedNullConnection = "DbContext_ConnectionFactoryReturnedNullConnection";

		// Token: 0x04001C17 RID: 7191
		internal const string DbSet_WrongNumberOfKeyValuesPassed = "DbSet_WrongNumberOfKeyValuesPassed";

		// Token: 0x04001C18 RID: 7192
		internal const string DbSet_WrongKeyValueType = "DbSet_WrongKeyValueType";

		// Token: 0x04001C19 RID: 7193
		internal const string DbSet_WrongEntityTypeFound = "DbSet_WrongEntityTypeFound";

		// Token: 0x04001C1A RID: 7194
		internal const string DbSet_MultipleAddedEntitiesFound = "DbSet_MultipleAddedEntitiesFound";

		// Token: 0x04001C1B RID: 7195
		internal const string DbSet_DbSetUsedWithComplexType = "DbSet_DbSetUsedWithComplexType";

		// Token: 0x04001C1C RID: 7196
		internal const string DbSet_PocoAndNonPocoMixedInSameAssembly = "DbSet_PocoAndNonPocoMixedInSameAssembly";

		// Token: 0x04001C1D RID: 7197
		internal const string DbSet_EntityTypeNotInModel = "DbSet_EntityTypeNotInModel";

		// Token: 0x04001C1E RID: 7198
		internal const string DbQuery_BindingToDbQueryNotSupported = "DbQuery_BindingToDbQueryNotSupported";

		// Token: 0x04001C1F RID: 7199
		internal const string DbExtensions_InvalidIncludePathExpression = "DbExtensions_InvalidIncludePathExpression";

		// Token: 0x04001C20 RID: 7200
		internal const string DbContext_ConnectionStringNotFound = "DbContext_ConnectionStringNotFound";

		// Token: 0x04001C21 RID: 7201
		internal const string DbContext_ConnectionHasModel = "DbContext_ConnectionHasModel";

		// Token: 0x04001C22 RID: 7202
		internal const string DbCollectionEntry_CannotSetCollectionProp = "DbCollectionEntry_CannotSetCollectionProp";

		// Token: 0x04001C23 RID: 7203
		internal const string CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported = "CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported";

		// Token: 0x04001C24 RID: 7204
		internal const string Mapping_MESTNotSupported = "Mapping_MESTNotSupported";

		// Token: 0x04001C25 RID: 7205
		internal const string DbModelBuilder_MissingRequiredCtor = "DbModelBuilder_MissingRequiredCtor";

		// Token: 0x04001C26 RID: 7206
		internal const string DbEntityValidationException_ValidationFailed = "DbEntityValidationException_ValidationFailed";

		// Token: 0x04001C27 RID: 7207
		internal const string DbUnexpectedValidationException_ValidationAttribute = "DbUnexpectedValidationException_ValidationAttribute";

		// Token: 0x04001C28 RID: 7208
		internal const string DbUnexpectedValidationException_IValidatableObject = "DbUnexpectedValidationException_IValidatableObject";

		// Token: 0x04001C29 RID: 7209
		internal const string SqlConnectionFactory_MdfNotSupported = "SqlConnectionFactory_MdfNotSupported";

		// Token: 0x04001C2A RID: 7210
		internal const string Database_InitializationException = "Database_InitializationException";

		// Token: 0x04001C2B RID: 7211
		internal const string EdmxWriter_EdmxFromObjectContextNotSupported = "EdmxWriter_EdmxFromObjectContextNotSupported";

		// Token: 0x04001C2C RID: 7212
		internal const string EdmxWriter_EdmxFromModelFirstNotSupported = "EdmxWriter_EdmxFromModelFirstNotSupported";

		// Token: 0x04001C2D RID: 7213
		internal const string UnintentionalCodeFirstException_Message = "UnintentionalCodeFirstException_Message";

		// Token: 0x04001C2E RID: 7214
		internal const string DbContextServices_MissingDefaultCtor = "DbContextServices_MissingDefaultCtor";

		// Token: 0x04001C2F RID: 7215
		internal const string CannotCallGenericSetWithProxyType = "CannotCallGenericSetWithProxyType";

		// Token: 0x04001C30 RID: 7216
		internal const string EdmModel_Validator_Semantic_SystemNamespaceEncountered = "EdmModel_Validator_Semantic_SystemNamespaceEncountered";

		// Token: 0x04001C31 RID: 7217
		internal const string EdmModel_Validator_Semantic_SimilarRelationshipEnd = "EdmModel_Validator_Semantic_SimilarRelationshipEnd";

		// Token: 0x04001C32 RID: 7218
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetNameReference = "EdmModel_Validator_Semantic_InvalidEntitySetNameReference";

		// Token: 0x04001C33 RID: 7219
		internal const string EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType = "EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType";

		// Token: 0x04001C34 RID: 7220
		internal const string EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys = "EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys";

		// Token: 0x04001C35 RID: 7221
		internal const string EdmModel_Validator_Semantic_DuplicateEndName = "EdmModel_Validator_Semantic_DuplicateEndName";

		// Token: 0x04001C36 RID: 7222
		internal const string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey = "EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey";

		// Token: 0x04001C37 RID: 7223
		internal const string EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection = "EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection";

		// Token: 0x04001C38 RID: 7224
		internal const string EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1 = "EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1";

		// Token: 0x04001C39 RID: 7225
		internal const string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract = "EdmModel_Validator_Semantic_InvalidComplexTypeAbstract";

		// Token: 0x04001C3A RID: 7226
		internal const string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic = "EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic";

		// Token: 0x04001C3B RID: 7227
		internal const string EdmModel_Validator_Semantic_InvalidKeyNullablePart = "EdmModel_Validator_Semantic_InvalidKeyNullablePart";

		// Token: 0x04001C3C RID: 7228
		internal const string EdmModel_Validator_Semantic_EntityKeyMustBeScalar = "EdmModel_Validator_Semantic_EntityKeyMustBeScalar";

		// Token: 0x04001C3D RID: 7229
		internal const string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass = "EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass";

		// Token: 0x04001C3E RID: 7230
		internal const string EdmModel_Validator_Semantic_KeyMissingOnEntityType = "EdmModel_Validator_Semantic_KeyMissingOnEntityType";

		// Token: 0x04001C3F RID: 7231
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole = "EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole";

		// Token: 0x04001C40 RID: 7232
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame = "EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame";

		// Token: 0x04001C41 RID: 7233
		internal const string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation = "EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation";

		// Token: 0x04001C42 RID: 7234
		internal const string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified = "EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified";

		// Token: 0x04001C43 RID: 7235
		internal const string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate = "EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate";

		// Token: 0x04001C44 RID: 7236
		internal const string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint = "EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint";

		// Token: 0x04001C45 RID: 7237
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne = "EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne";

		// Token: 0x04001C46 RID: 7238
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1 = "EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1";

		// Token: 0x04001C47 RID: 7239
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1 = "EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1";

		// Token: 0x04001C48 RID: 7240
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2 = "EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2";

		// Token: 0x04001C49 RID: 7241
		internal const string EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint = "EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint";

		// Token: 0x04001C4A RID: 7242
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne = "EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne";

		// Token: 0x04001C4B RID: 7243
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany = "EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany";

		// Token: 0x04001C4C RID: 7244
		internal const string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint = "EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint";

		// Token: 0x04001C4D RID: 7245
		internal const string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint = "EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint";

		// Token: 0x04001C4E RID: 7246
		internal const string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint = "EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint";

		// Token: 0x04001C4F RID: 7247
		internal const string EdmModel_Validator_Semantic_NullableComplexType = "EdmModel_Validator_Semantic_NullableComplexType";

		// Token: 0x04001C50 RID: 7248
		internal const string EdmModel_Validator_Semantic_InvalidPropertyType = "EdmModel_Validator_Semantic_InvalidPropertyType";

		// Token: 0x04001C51 RID: 7249
		internal const string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName = "EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName";

		// Token: 0x04001C52 RID: 7250
		internal const string EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate = "EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate";

		// Token: 0x04001C53 RID: 7251
		internal const string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName = "EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName";

		// Token: 0x04001C54 RID: 7252
		internal const string EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate = "EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate";

		// Token: 0x04001C55 RID: 7253
		internal const string EdmModel_Validator_Semantic_CycleInTypeHierarchy = "EdmModel_Validator_Semantic_CycleInTypeHierarchy";

		// Token: 0x04001C56 RID: 7254
		internal const string EdmModel_Validator_Semantic_InvalidPropertyType_V1_1 = "EdmModel_Validator_Semantic_InvalidPropertyType_V1_1";

		// Token: 0x04001C57 RID: 7255
		internal const string EdmModel_Validator_Semantic_InvalidPropertyType_V3 = "EdmModel_Validator_Semantic_InvalidPropertyType_V3";

		// Token: 0x04001C58 RID: 7256
		internal const string EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion = "EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion";

		// Token: 0x04001C59 RID: 7257
		internal const string EdmModel_Validator_Syntactic_MissingName = "EdmModel_Validator_Syntactic_MissingName";

		// Token: 0x04001C5A RID: 7258
		internal const string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong = "EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong";

		// Token: 0x04001C5B RID: 7259
		internal const string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed = "EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed";

		// Token: 0x04001C5C RID: 7260
		internal const string EdmModel_Validator_Syntactic_EdmAssociationType_AssocationEndMustNotBeNull = "EdmModel_Validator_Syntactic_EdmAssociationType_AssocationEndMustNotBeNull";

		// Token: 0x04001C5D RID: 7261
		internal const string EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull = "EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull";

		// Token: 0x04001C5E RID: 7262
		internal const string EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty = "EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty";

		// Token: 0x04001C5F RID: 7263
		internal const string EdmModel_Validator_Syntactic_EdmNavigationProperty_AssocationMustNotBeNull = "EdmModel_Validator_Syntactic_EdmNavigationProperty_AssocationMustNotBeNull";

		// Token: 0x04001C60 RID: 7264
		internal const string EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull = "EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull";

		// Token: 0x04001C61 RID: 7265
		internal const string EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull = "EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull";

		// Token: 0x04001C62 RID: 7266
		internal const string EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull = "EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull";

		// Token: 0x04001C63 RID: 7267
		internal const string EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull = "EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull";

		// Token: 0x04001C64 RID: 7268
		internal const string EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull = "EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull";

		// Token: 0x04001C65 RID: 7269
		internal const string EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull = "EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull";

		// Token: 0x04001C66 RID: 7270
		internal const string EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid = "EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid";

		// Token: 0x04001C67 RID: 7271
		internal const string MetadataItem_InvalidDataSpace = "MetadataItem_InvalidDataSpace";

		// Token: 0x04001C68 RID: 7272
		internal const string EdmModel_AddItem_NonMatchingNamespace = "EdmModel_AddItem_NonMatchingNamespace";

		// Token: 0x04001C69 RID: 7273
		internal const string Serializer_OneNamespaceAndOneContainer = "Serializer_OneNamespaceAndOneContainer";

		// Token: 0x04001C6A RID: 7274
		internal const string MaxLengthAttribute_ValidationError = "MaxLengthAttribute_ValidationError";

		// Token: 0x04001C6B RID: 7275
		internal const string MaxLengthAttribute_InvalidMaxLength = "MaxLengthAttribute_InvalidMaxLength";

		// Token: 0x04001C6C RID: 7276
		internal const string MinLengthAttribute_ValidationError = "MinLengthAttribute_ValidationError";

		// Token: 0x04001C6D RID: 7277
		internal const string MinLengthAttribute_InvalidMinLength = "MinLengthAttribute_InvalidMinLength";

		// Token: 0x04001C6E RID: 7278
		internal const string DbConnectionInfo_ConnectionStringNotFound = "DbConnectionInfo_ConnectionStringNotFound";

		// Token: 0x04001C6F RID: 7279
		internal const string EagerInternalContext_CannotSetConnectionInfo = "EagerInternalContext_CannotSetConnectionInfo";

		// Token: 0x04001C70 RID: 7280
		internal const string LazyInternalContext_CannotReplaceEfConnectionWithDbConnection = "LazyInternalContext_CannotReplaceEfConnectionWithDbConnection";

		// Token: 0x04001C71 RID: 7281
		internal const string LazyInternalContext_CannotReplaceDbConnectionWithEfConnection = "LazyInternalContext_CannotReplaceDbConnectionWithEfConnection";

		// Token: 0x04001C72 RID: 7282
		internal const string EntityKey_EntitySetDoesNotMatch = "EntityKey_EntitySetDoesNotMatch";

		// Token: 0x04001C73 RID: 7283
		internal const string EntityKey_IncorrectNumberOfKeyValuePairs = "EntityKey_IncorrectNumberOfKeyValuePairs";

		// Token: 0x04001C74 RID: 7284
		internal const string EntityKey_IncorrectValueType = "EntityKey_IncorrectValueType";

		// Token: 0x04001C75 RID: 7285
		internal const string EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember = "EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember";

		// Token: 0x04001C76 RID: 7286
		internal const string EntityKey_MissingKeyValue = "EntityKey_MissingKeyValue";

		// Token: 0x04001C77 RID: 7287
		internal const string EntityKey_NoNullsAllowedInKeyValuePairs = "EntityKey_NoNullsAllowedInKeyValuePairs";

		// Token: 0x04001C78 RID: 7288
		internal const string EntityKey_UnexpectedNull = "EntityKey_UnexpectedNull";

		// Token: 0x04001C79 RID: 7289
		internal const string EntityKey_DoesntMatchKeyOnEntity = "EntityKey_DoesntMatchKeyOnEntity";

		// Token: 0x04001C7A RID: 7290
		internal const string EntityKey_EntityKeyMustHaveValues = "EntityKey_EntityKeyMustHaveValues";

		// Token: 0x04001C7B RID: 7291
		internal const string EntityKey_InvalidQualifiedEntitySetName = "EntityKey_InvalidQualifiedEntitySetName";

		// Token: 0x04001C7C RID: 7292
		internal const string EntityKey_MissingEntitySetName = "EntityKey_MissingEntitySetName";

		// Token: 0x04001C7D RID: 7293
		internal const string EntityKey_InvalidName = "EntityKey_InvalidName";

		// Token: 0x04001C7E RID: 7294
		internal const string EntityKey_CannotChangeKey = "EntityKey_CannotChangeKey";

		// Token: 0x04001C7F RID: 7295
		internal const string EntityTypesDoNotAgree = "EntityTypesDoNotAgree";

		// Token: 0x04001C80 RID: 7296
		internal const string EntityKey_NullKeyValue = "EntityKey_NullKeyValue";

		// Token: 0x04001C81 RID: 7297
		internal const string EdmMembersDefiningTypeDoNotAgreeWithMetadataType = "EdmMembersDefiningTypeDoNotAgreeWithMetadataType";

		// Token: 0x04001C82 RID: 7298
		internal const string CannotCallNoncomposableFunction = "CannotCallNoncomposableFunction";

		// Token: 0x04001C83 RID: 7299
		internal const string EntityClient_ConnectionStringMissingInfo = "EntityClient_ConnectionStringMissingInfo";

		// Token: 0x04001C84 RID: 7300
		internal const string EntityClient_ValueNotString = "EntityClient_ValueNotString";

		// Token: 0x04001C85 RID: 7301
		internal const string EntityClient_KeywordNotSupported = "EntityClient_KeywordNotSupported";

		// Token: 0x04001C86 RID: 7302
		internal const string EntityClient_NoCommandText = "EntityClient_NoCommandText";

		// Token: 0x04001C87 RID: 7303
		internal const string EntityClient_ConnectionStringNeededBeforeOperation = "EntityClient_ConnectionStringNeededBeforeOperation";

		// Token: 0x04001C88 RID: 7304
		internal const string EntityClient_ConnectionNotOpen = "EntityClient_ConnectionNotOpen";

		// Token: 0x04001C89 RID: 7305
		internal const string EntityClient_DuplicateParameterNames = "EntityClient_DuplicateParameterNames";

		// Token: 0x04001C8A RID: 7306
		internal const string EntityClient_NoConnectionForCommand = "EntityClient_NoConnectionForCommand";

		// Token: 0x04001C8B RID: 7307
		internal const string EntityClient_NoConnectionForAdapter = "EntityClient_NoConnectionForAdapter";

		// Token: 0x04001C8C RID: 7308
		internal const string EntityClient_ClosedConnectionForUpdate = "EntityClient_ClosedConnectionForUpdate";

		// Token: 0x04001C8D RID: 7309
		internal const string EntityClient_InvalidNamedConnection = "EntityClient_InvalidNamedConnection";

		// Token: 0x04001C8E RID: 7310
		internal const string EntityClient_NestedNamedConnection = "EntityClient_NestedNamedConnection";

		// Token: 0x04001C8F RID: 7311
		internal const string EntityClient_InvalidStoreProvider = "EntityClient_InvalidStoreProvider";

		// Token: 0x04001C90 RID: 7312
		internal const string EntityClient_DataReaderIsStillOpen = "EntityClient_DataReaderIsStillOpen";

		// Token: 0x04001C91 RID: 7313
		internal const string EntityClient_SettingsCannotBeChangedOnOpenConnection = "EntityClient_SettingsCannotBeChangedOnOpenConnection";

		// Token: 0x04001C92 RID: 7314
		internal const string EntityClient_ExecutingOnClosedConnection = "EntityClient_ExecutingOnClosedConnection";

		// Token: 0x04001C93 RID: 7315
		internal const string EntityClient_ConnectionStateClosed = "EntityClient_ConnectionStateClosed";

		// Token: 0x04001C94 RID: 7316
		internal const string EntityClient_ConnectionStateBroken = "EntityClient_ConnectionStateBroken";

		// Token: 0x04001C95 RID: 7317
		internal const string EntityClient_CannotCloneStoreProvider = "EntityClient_CannotCloneStoreProvider";

		// Token: 0x04001C96 RID: 7318
		internal const string EntityClient_UnsupportedCommandType = "EntityClient_UnsupportedCommandType";

		// Token: 0x04001C97 RID: 7319
		internal const string EntityClient_ErrorInClosingConnection = "EntityClient_ErrorInClosingConnection";

		// Token: 0x04001C98 RID: 7320
		internal const string EntityClient_ErrorInBeginningTransaction = "EntityClient_ErrorInBeginningTransaction";

		// Token: 0x04001C99 RID: 7321
		internal const string EntityClient_ExtraParametersWithNamedConnection = "EntityClient_ExtraParametersWithNamedConnection";

		// Token: 0x04001C9A RID: 7322
		internal const string EntityClient_CommandDefinitionPreparationFailed = "EntityClient_CommandDefinitionPreparationFailed";

		// Token: 0x04001C9B RID: 7323
		internal const string EntityClient_CommandDefinitionExecutionFailed = "EntityClient_CommandDefinitionExecutionFailed";

		// Token: 0x04001C9C RID: 7324
		internal const string EntityClient_CommandExecutionFailed = "EntityClient_CommandExecutionFailed";

		// Token: 0x04001C9D RID: 7325
		internal const string EntityClient_StoreReaderFailed = "EntityClient_StoreReaderFailed";

		// Token: 0x04001C9E RID: 7326
		internal const string EntityClient_FailedToGetInformation = "EntityClient_FailedToGetInformation";

		// Token: 0x04001C9F RID: 7327
		internal const string EntityClient_TooFewColumns = "EntityClient_TooFewColumns";

		// Token: 0x04001CA0 RID: 7328
		internal const string EntityClient_InvalidParameterName = "EntityClient_InvalidParameterName";

		// Token: 0x04001CA1 RID: 7329
		internal const string EntityClient_EmptyParameterName = "EntityClient_EmptyParameterName";

		// Token: 0x04001CA2 RID: 7330
		internal const string EntityClient_ReturnedNullOnProviderMethod = "EntityClient_ReturnedNullOnProviderMethod";

		// Token: 0x04001CA3 RID: 7331
		internal const string EntityClient_CannotDeduceDbType = "EntityClient_CannotDeduceDbType";

		// Token: 0x04001CA4 RID: 7332
		internal const string EntityClient_InvalidParameterDirection = "EntityClient_InvalidParameterDirection";

		// Token: 0x04001CA5 RID: 7333
		internal const string EntityClient_UnknownParameterType = "EntityClient_UnknownParameterType";

		// Token: 0x04001CA6 RID: 7334
		internal const string EntityClient_UnsupportedDbType = "EntityClient_UnsupportedDbType";

		// Token: 0x04001CA7 RID: 7335
		internal const string EntityClient_IncompatibleNavigationPropertyResult = "EntityClient_IncompatibleNavigationPropertyResult";

		// Token: 0x04001CA8 RID: 7336
		internal const string EntityClient_TransactionAlreadyStarted = "EntityClient_TransactionAlreadyStarted";

		// Token: 0x04001CA9 RID: 7337
		internal const string EntityClient_InvalidTransactionForCommand = "EntityClient_InvalidTransactionForCommand";

		// Token: 0x04001CAA RID: 7338
		internal const string EntityClient_NoStoreConnectionForUpdate = "EntityClient_NoStoreConnectionForUpdate";

		// Token: 0x04001CAB RID: 7339
		internal const string EntityClient_CommandTreeMetadataIncompatible = "EntityClient_CommandTreeMetadataIncompatible";

		// Token: 0x04001CAC RID: 7340
		internal const string EntityClient_ProviderGeneralError = "EntityClient_ProviderGeneralError";

		// Token: 0x04001CAD RID: 7341
		internal const string EntityClient_ProviderSpecificError = "EntityClient_ProviderSpecificError";

		// Token: 0x04001CAE RID: 7342
		internal const string EntityClient_FunctionImportEmptyCommandText = "EntityClient_FunctionImportEmptyCommandText";

		// Token: 0x04001CAF RID: 7343
		internal const string EntityClient_UnableToFindFunctionImportContainer = "EntityClient_UnableToFindFunctionImportContainer";

		// Token: 0x04001CB0 RID: 7344
		internal const string EntityClient_UnableToFindFunctionImport = "EntityClient_UnableToFindFunctionImport";

		// Token: 0x04001CB1 RID: 7345
		internal const string EntityClient_FunctionImportMustBeNonComposable = "EntityClient_FunctionImportMustBeNonComposable";

		// Token: 0x04001CB2 RID: 7346
		internal const string EntityClient_UnmappedFunctionImport = "EntityClient_UnmappedFunctionImport";

		// Token: 0x04001CB3 RID: 7347
		internal const string EntityClient_InvalidStoredProcedureCommandText = "EntityClient_InvalidStoredProcedureCommandText";

		// Token: 0x04001CB4 RID: 7348
		internal const string EntityClient_ItemCollectionsNotRegisteredInWorkspace = "EntityClient_ItemCollectionsNotRegisteredInWorkspace";

		// Token: 0x04001CB5 RID: 7349
		internal const string EntityClient_DbConnectionHasNoProvider = "EntityClient_DbConnectionHasNoProvider";

		// Token: 0x04001CB6 RID: 7350
		internal const string EntityClient_RequiresNonStoreCommandTree = "EntityClient_RequiresNonStoreCommandTree";

		// Token: 0x04001CB7 RID: 7351
		internal const string EntityClient_CannotReprepareCommandDefinitionBasedCommand = "EntityClient_CannotReprepareCommandDefinitionBasedCommand";

		// Token: 0x04001CB8 RID: 7352
		internal const string EntityClient_EntityParameterEdmTypeNotScalar = "EntityClient_EntityParameterEdmTypeNotScalar";

		// Token: 0x04001CB9 RID: 7353
		internal const string EntityClient_EntityParameterInconsistentEdmType = "EntityClient_EntityParameterInconsistentEdmType";

		// Token: 0x04001CBA RID: 7354
		internal const string EntityClient_CannotGetCommandText = "EntityClient_CannotGetCommandText";

		// Token: 0x04001CBB RID: 7355
		internal const string EntityClient_CannotSetCommandText = "EntityClient_CannotSetCommandText";

		// Token: 0x04001CBC RID: 7356
		internal const string EntityClient_CannotGetCommandTree = "EntityClient_CannotGetCommandTree";

		// Token: 0x04001CBD RID: 7357
		internal const string EntityClient_CannotSetCommandTree = "EntityClient_CannotSetCommandTree";

		// Token: 0x04001CBE RID: 7358
		internal const string ELinq_ExpressionMustBeIQueryable = "ELinq_ExpressionMustBeIQueryable";

		// Token: 0x04001CBF RID: 7359
		internal const string ELinq_UnsupportedExpressionType = "ELinq_UnsupportedExpressionType";

		// Token: 0x04001CC0 RID: 7360
		internal const string ELinq_UnsupportedUseOfContextParameter = "ELinq_UnsupportedUseOfContextParameter";

		// Token: 0x04001CC1 RID: 7361
		internal const string ELinq_UnboundParameterExpression = "ELinq_UnboundParameterExpression";

		// Token: 0x04001CC2 RID: 7362
		internal const string ELinq_UnsupportedConstructor = "ELinq_UnsupportedConstructor";

		// Token: 0x04001CC3 RID: 7363
		internal const string ELinq_UnsupportedInitializers = "ELinq_UnsupportedInitializers";

		// Token: 0x04001CC4 RID: 7364
		internal const string ELinq_UnsupportedBinding = "ELinq_UnsupportedBinding";

		// Token: 0x04001CC5 RID: 7365
		internal const string ELinq_UnsupportedMethod = "ELinq_UnsupportedMethod";

		// Token: 0x04001CC6 RID: 7366
		internal const string ELinq_UnsupportedMethodSuggestedAlternative = "ELinq_UnsupportedMethodSuggestedAlternative";

		// Token: 0x04001CC7 RID: 7367
		internal const string ELinq_ThenByDoesNotFollowOrderBy = "ELinq_ThenByDoesNotFollowOrderBy";

		// Token: 0x04001CC8 RID: 7368
		internal const string ELinq_UnrecognizedMember = "ELinq_UnrecognizedMember";

		// Token: 0x04001CC9 RID: 7369
		internal const string ELinq_UnresolvableFunctionForMethod = "ELinq_UnresolvableFunctionForMethod";

		// Token: 0x04001CCA RID: 7370
		internal const string ELinq_UnresolvableFunctionForMethodAmbiguousMatch = "ELinq_UnresolvableFunctionForMethodAmbiguousMatch";

		// Token: 0x04001CCB RID: 7371
		internal const string ELinq_UnresolvableFunctionForMethodNotFound = "ELinq_UnresolvableFunctionForMethodNotFound";

		// Token: 0x04001CCC RID: 7372
		internal const string ELinq_UnresolvableFunctionForMember = "ELinq_UnresolvableFunctionForMember";

		// Token: 0x04001CCD RID: 7373
		internal const string ELinq_UnresolvableStoreFunctionForMember = "ELinq_UnresolvableStoreFunctionForMember";

		// Token: 0x04001CCE RID: 7374
		internal const string ELinq_UnresolvableFunctionForExpression = "ELinq_UnresolvableFunctionForExpression";

		// Token: 0x04001CCF RID: 7375
		internal const string ELinq_UnresolvableStoreFunctionForExpression = "ELinq_UnresolvableStoreFunctionForExpression";

		// Token: 0x04001CD0 RID: 7376
		internal const string ELinq_UnsupportedType = "ELinq_UnsupportedType";

		// Token: 0x04001CD1 RID: 7377
		internal const string ELinq_UnsupportedNullConstant = "ELinq_UnsupportedNullConstant";

		// Token: 0x04001CD2 RID: 7378
		internal const string ELinq_UnsupportedConstant = "ELinq_UnsupportedConstant";

		// Token: 0x04001CD3 RID: 7379
		internal const string ELinq_UnsupportedCast = "ELinq_UnsupportedCast";

		// Token: 0x04001CD4 RID: 7380
		internal const string ELinq_UnsupportedIsOrAs = "ELinq_UnsupportedIsOrAs";

		// Token: 0x04001CD5 RID: 7381
		internal const string ELinq_UnsupportedQueryableMethod = "ELinq_UnsupportedQueryableMethod";

		// Token: 0x04001CD6 RID: 7382
		internal const string ELinq_InvalidOfTypeResult = "ELinq_InvalidOfTypeResult";

		// Token: 0x04001CD7 RID: 7383
		internal const string ELinq_UnsupportedNominalType = "ELinq_UnsupportedNominalType";

		// Token: 0x04001CD8 RID: 7384
		internal const string ELinq_UnsupportedEnumerableType = "ELinq_UnsupportedEnumerableType";

		// Token: 0x04001CD9 RID: 7385
		internal const string ELinq_UnsupportedHeterogeneousInitializers = "ELinq_UnsupportedHeterogeneousInitializers";

		// Token: 0x04001CDA RID: 7386
		internal const string ELinq_UnsupportedDifferentContexts = "ELinq_UnsupportedDifferentContexts";

		// Token: 0x04001CDB RID: 7387
		internal const string ELinq_UnsupportedCastToDecimal = "ELinq_UnsupportedCastToDecimal";

		// Token: 0x04001CDC RID: 7388
		internal const string ELinq_UnsupportedKeySelector = "ELinq_UnsupportedKeySelector";

		// Token: 0x04001CDD RID: 7389
		internal const string ELinq_CreateOrderedEnumerableNotSupported = "ELinq_CreateOrderedEnumerableNotSupported";

		// Token: 0x04001CDE RID: 7390
		internal const string ELinq_UnsupportedPassthrough = "ELinq_UnsupportedPassthrough";

		// Token: 0x04001CDF RID: 7391
		internal const string ELinq_UnexpectedTypeForNavigationProperty = "ELinq_UnexpectedTypeForNavigationProperty";

		// Token: 0x04001CE0 RID: 7392
		internal const string ELinq_SkipWithoutOrder = "ELinq_SkipWithoutOrder";

		// Token: 0x04001CE1 RID: 7393
		internal const string ELinq_PropertyIndexNotSupported = "ELinq_PropertyIndexNotSupported";

		// Token: 0x04001CE2 RID: 7394
		internal const string ELinq_NotPropertyOrField = "ELinq_NotPropertyOrField";

		// Token: 0x04001CE3 RID: 7395
		internal const string ELinq_UnsupportedStringRemoveCase = "ELinq_UnsupportedStringRemoveCase";

		// Token: 0x04001CE4 RID: 7396
		internal const string ELinq_UnsupportedTrimStartTrimEndCase = "ELinq_UnsupportedTrimStartTrimEndCase";

		// Token: 0x04001CE5 RID: 7397
		internal const string ELinq_UnsupportedVBDatePartNonConstantInterval = "ELinq_UnsupportedVBDatePartNonConstantInterval";

		// Token: 0x04001CE6 RID: 7398
		internal const string ELinq_UnsupportedVBDatePartInvalidInterval = "ELinq_UnsupportedVBDatePartInvalidInterval";

		// Token: 0x04001CE7 RID: 7399
		internal const string ELinq_UnsupportedAsUnicodeAndAsNonUnicode = "ELinq_UnsupportedAsUnicodeAndAsNonUnicode";

		// Token: 0x04001CE8 RID: 7400
		internal const string ELinq_UnsupportedComparison = "ELinq_UnsupportedComparison";

		// Token: 0x04001CE9 RID: 7401
		internal const string ELinq_UnsupportedRefComparison = "ELinq_UnsupportedRefComparison";

		// Token: 0x04001CEA RID: 7402
		internal const string ELinq_UnsupportedRowComparison = "ELinq_UnsupportedRowComparison";

		// Token: 0x04001CEB RID: 7403
		internal const string ELinq_UnsupportedRowMemberComparison = "ELinq_UnsupportedRowMemberComparison";

		// Token: 0x04001CEC RID: 7404
		internal const string ELinq_UnsupportedRowTypeComparison = "ELinq_UnsupportedRowTypeComparison";

		// Token: 0x04001CED RID: 7405
		internal const string ELinq_AnonymousType = "ELinq_AnonymousType";

		// Token: 0x04001CEE RID: 7406
		internal const string ELinq_ClosureType = "ELinq_ClosureType";

		// Token: 0x04001CEF RID: 7407
		internal const string ELinq_UnhandledExpressionType = "ELinq_UnhandledExpressionType";

		// Token: 0x04001CF0 RID: 7408
		internal const string ELinq_UnhandledBindingType = "ELinq_UnhandledBindingType";

		// Token: 0x04001CF1 RID: 7409
		internal const string ELinq_UnsupportedNestedFirst = "ELinq_UnsupportedNestedFirst";

		// Token: 0x04001CF2 RID: 7410
		internal const string ELinq_UnsupportedNestedSingle = "ELinq_UnsupportedNestedSingle";

		// Token: 0x04001CF3 RID: 7411
		internal const string ELinq_UnsupportedInclude = "ELinq_UnsupportedInclude";

		// Token: 0x04001CF4 RID: 7412
		internal const string ELinq_UnsupportedMergeAs = "ELinq_UnsupportedMergeAs";

		// Token: 0x04001CF5 RID: 7413
		internal const string ELinq_MethodNotDirectlyCallable = "ELinq_MethodNotDirectlyCallable";

		// Token: 0x04001CF6 RID: 7414
		internal const string ELinq_CycleDetected = "ELinq_CycleDetected";

		// Token: 0x04001CF7 RID: 7415
		internal const string ELinq_DbFunctionAttributedFunctionWithWrongReturnType = "ELinq_DbFunctionAttributedFunctionWithWrongReturnType";

		// Token: 0x04001CF8 RID: 7416
		internal const string ELinq_DbFunctionDirectCall = "ELinq_DbFunctionDirectCall";

		// Token: 0x04001CF9 RID: 7417
		internal const string ELinq_HasFlagArgumentAndSourceTypeMismatch = "ELinq_HasFlagArgumentAndSourceTypeMismatch";

		// Token: 0x04001CFA RID: 7418
		internal const string Elinq_ToStringNotSupportedForType = "Elinq_ToStringNotSupportedForType";

		// Token: 0x04001CFB RID: 7419
		internal const string Elinq_ToStringNotSupportedForEnumsWithFlags = "Elinq_ToStringNotSupportedForEnumsWithFlags";

		// Token: 0x04001CFC RID: 7420
		internal const string CompiledELinq_UnsupportedParameterTypes = "CompiledELinq_UnsupportedParameterTypes";

		// Token: 0x04001CFD RID: 7421
		internal const string CompiledELinq_UnsupportedNamedParameterType = "CompiledELinq_UnsupportedNamedParameterType";

		// Token: 0x04001CFE RID: 7422
		internal const string CompiledELinq_UnsupportedNamedParameterUseAsType = "CompiledELinq_UnsupportedNamedParameterUseAsType";

		// Token: 0x04001CFF RID: 7423
		internal const string Update_UnsupportedExpressionKind = "Update_UnsupportedExpressionKind";

		// Token: 0x04001D00 RID: 7424
		internal const string Update_UnsupportedCastArgument = "Update_UnsupportedCastArgument";

		// Token: 0x04001D01 RID: 7425
		internal const string Update_UnsupportedExtentType = "Update_UnsupportedExtentType";

		// Token: 0x04001D02 RID: 7426
		internal const string Update_ConstraintCycle = "Update_ConstraintCycle";

		// Token: 0x04001D03 RID: 7427
		internal const string Update_UnsupportedJoinType = "Update_UnsupportedJoinType";

		// Token: 0x04001D04 RID: 7428
		internal const string Update_UnsupportedProjection = "Update_UnsupportedProjection";

		// Token: 0x04001D05 RID: 7429
		internal const string Update_ConcurrencyError = "Update_ConcurrencyError";

		// Token: 0x04001D06 RID: 7430
		internal const string Update_MissingEntity = "Update_MissingEntity";

		// Token: 0x04001D07 RID: 7431
		internal const string Update_RelationshipCardinalityConstraintViolation = "Update_RelationshipCardinalityConstraintViolation";

		// Token: 0x04001D08 RID: 7432
		internal const string Update_GeneralExecutionException = "Update_GeneralExecutionException";

		// Token: 0x04001D09 RID: 7433
		internal const string Update_MissingRequiredEntity = "Update_MissingRequiredEntity";

		// Token: 0x04001D0A RID: 7434
		internal const string Update_RelationshipCardinalityViolation = "Update_RelationshipCardinalityViolation";

		// Token: 0x04001D0B RID: 7435
		internal const string Update_NotSupportedComputedKeyColumn = "Update_NotSupportedComputedKeyColumn";

		// Token: 0x04001D0C RID: 7436
		internal const string Update_AmbiguousServerGenIdentifier = "Update_AmbiguousServerGenIdentifier";

		// Token: 0x04001D0D RID: 7437
		internal const string Update_WorkspaceMismatch = "Update_WorkspaceMismatch";

		// Token: 0x04001D0E RID: 7438
		internal const string Update_MissingRequiredRelationshipValue = "Update_MissingRequiredRelationshipValue";

		// Token: 0x04001D0F RID: 7439
		internal const string Update_MissingResultColumn = "Update_MissingResultColumn";

		// Token: 0x04001D10 RID: 7440
		internal const string Update_NullReturnValueForNonNullableMember = "Update_NullReturnValueForNonNullableMember";

		// Token: 0x04001D11 RID: 7441
		internal const string Update_ReturnValueHasUnexpectedType = "Update_ReturnValueHasUnexpectedType";

		// Token: 0x04001D12 RID: 7442
		internal const string Update_UnableToConvertRowsAffectedParameter = "Update_UnableToConvertRowsAffectedParameter";

		// Token: 0x04001D13 RID: 7443
		internal const string Update_MappingNotFound = "Update_MappingNotFound";

		// Token: 0x04001D14 RID: 7444
		internal const string Update_ModifyingIdentityColumn = "Update_ModifyingIdentityColumn";

		// Token: 0x04001D15 RID: 7445
		internal const string Update_GeneratedDependent = "Update_GeneratedDependent";

		// Token: 0x04001D16 RID: 7446
		internal const string Update_ReferentialConstraintIntegrityViolation = "Update_ReferentialConstraintIntegrityViolation";

		// Token: 0x04001D17 RID: 7447
		internal const string Update_ErrorLoadingRecord = "Update_ErrorLoadingRecord";

		// Token: 0x04001D18 RID: 7448
		internal const string Update_NullValue = "Update_NullValue";

		// Token: 0x04001D19 RID: 7449
		internal const string Update_CircularRelationships = "Update_CircularRelationships";

		// Token: 0x04001D1A RID: 7450
		internal const string Update_RelationshipCardinalityConstraintViolationSingleValue = "Update_RelationshipCardinalityConstraintViolationSingleValue";

		// Token: 0x04001D1B RID: 7451
		internal const string Update_MissingFunctionMapping = "Update_MissingFunctionMapping";

		// Token: 0x04001D1C RID: 7452
		internal const string Update_InvalidChanges = "Update_InvalidChanges";

		// Token: 0x04001D1D RID: 7453
		internal const string Update_DuplicateKeys = "Update_DuplicateKeys";

		// Token: 0x04001D1E RID: 7454
		internal const string Update_AmbiguousForeignKey = "Update_AmbiguousForeignKey";

		// Token: 0x04001D1F RID: 7455
		internal const string Update_InsertingOrUpdatingReferenceToDeletedEntity = "Update_InsertingOrUpdatingReferenceToDeletedEntity";

		// Token: 0x04001D20 RID: 7456
		internal const string ViewGen_Extent = "ViewGen_Extent";

		// Token: 0x04001D21 RID: 7457
		internal const string ViewGen_Null = "ViewGen_Null";

		// Token: 0x04001D22 RID: 7458
		internal const string ViewGen_CommaBlank = "ViewGen_CommaBlank";

		// Token: 0x04001D23 RID: 7459
		internal const string ViewGen_Entities = "ViewGen_Entities";

		// Token: 0x04001D24 RID: 7460
		internal const string ViewGen_Tuples = "ViewGen_Tuples";

		// Token: 0x04001D25 RID: 7461
		internal const string ViewGen_NotNull = "ViewGen_NotNull";

		// Token: 0x04001D26 RID: 7462
		internal const string ViewGen_NegatedCellConstant = "ViewGen_NegatedCellConstant";

		// Token: 0x04001D27 RID: 7463
		internal const string ViewGen_Error = "ViewGen_Error";

		// Token: 0x04001D28 RID: 7464
		internal const string Viewgen_CannotGenerateQueryViewUnderNoValidation = "Viewgen_CannotGenerateQueryViewUnderNoValidation";

		// Token: 0x04001D29 RID: 7465
		internal const string ViewGen_Missing_Sets_Mapping = "ViewGen_Missing_Sets_Mapping";

		// Token: 0x04001D2A RID: 7466
		internal const string ViewGen_Missing_Type_Mapping = "ViewGen_Missing_Type_Mapping";

		// Token: 0x04001D2B RID: 7467
		internal const string ViewGen_Missing_Set_Mapping = "ViewGen_Missing_Set_Mapping";

		// Token: 0x04001D2C RID: 7468
		internal const string ViewGen_Concurrency_Derived_Class = "ViewGen_Concurrency_Derived_Class";

		// Token: 0x04001D2D RID: 7469
		internal const string ViewGen_Concurrency_Invalid_Condition = "ViewGen_Concurrency_Invalid_Condition";

		// Token: 0x04001D2E RID: 7470
		internal const string ViewGen_TableKey_Missing = "ViewGen_TableKey_Missing";

		// Token: 0x04001D2F RID: 7471
		internal const string ViewGen_EntitySetKey_Missing = "ViewGen_EntitySetKey_Missing";

		// Token: 0x04001D30 RID: 7472
		internal const string ViewGen_AssociationSetKey_Missing = "ViewGen_AssociationSetKey_Missing";

		// Token: 0x04001D31 RID: 7473
		internal const string ViewGen_Cannot_Recover_Attributes = "ViewGen_Cannot_Recover_Attributes";

		// Token: 0x04001D32 RID: 7474
		internal const string ViewGen_Cannot_Recover_Types = "ViewGen_Cannot_Recover_Types";

		// Token: 0x04001D33 RID: 7475
		internal const string ViewGen_Cannot_Disambiguate_MultiConstant = "ViewGen_Cannot_Disambiguate_MultiConstant";

		// Token: 0x04001D34 RID: 7476
		internal const string ViewGen_No_Default_Value = "ViewGen_No_Default_Value";

		// Token: 0x04001D35 RID: 7477
		internal const string ViewGen_No_Default_Value_For_Configuration = "ViewGen_No_Default_Value_For_Configuration";

		// Token: 0x04001D36 RID: 7478
		internal const string ViewGen_KeyConstraint_Violation = "ViewGen_KeyConstraint_Violation";

		// Token: 0x04001D37 RID: 7479
		internal const string ViewGen_KeyConstraint_Update_Violation_EntitySet = "ViewGen_KeyConstraint_Update_Violation_EntitySet";

		// Token: 0x04001D38 RID: 7480
		internal const string ViewGen_KeyConstraint_Update_Violation_AssociationSet = "ViewGen_KeyConstraint_Update_Violation_AssociationSet";

		// Token: 0x04001D39 RID: 7481
		internal const string ViewGen_AssociationEndShouldBeMappedToKey = "ViewGen_AssociationEndShouldBeMappedToKey";

		// Token: 0x04001D3A RID: 7482
		internal const string ViewGen_Duplicate_CProperties = "ViewGen_Duplicate_CProperties";

		// Token: 0x04001D3B RID: 7483
		internal const string ViewGen_Duplicate_CProperties_IsMapped = "ViewGen_Duplicate_CProperties_IsMapped";

		// Token: 0x04001D3C RID: 7484
		internal const string ViewGen_NotNull_No_Projected_Slot = "ViewGen_NotNull_No_Projected_Slot";

		// Token: 0x04001D3D RID: 7485
		internal const string ViewGen_InvalidCondition = "ViewGen_InvalidCondition";

		// Token: 0x04001D3E RID: 7486
		internal const string ViewGen_NonKeyProjectedWithOverlappingPartitions = "ViewGen_NonKeyProjectedWithOverlappingPartitions";

		// Token: 0x04001D3F RID: 7487
		internal const string ViewGen_CQ_PartitionConstraint = "ViewGen_CQ_PartitionConstraint";

		// Token: 0x04001D40 RID: 7488
		internal const string ViewGen_CQ_DomainConstraint = "ViewGen_CQ_DomainConstraint";

		// Token: 0x04001D41 RID: 7489
		internal const string ViewGen_ErrorLog = "ViewGen_ErrorLog";

		// Token: 0x04001D42 RID: 7490
		internal const string ViewGen_ErrorLog2 = "ViewGen_ErrorLog2";

		// Token: 0x04001D43 RID: 7491
		internal const string ViewGen_Foreign_Key_Missing_Table_Mapping = "ViewGen_Foreign_Key_Missing_Table_Mapping";

		// Token: 0x04001D44 RID: 7492
		internal const string ViewGen_Foreign_Key_ParentTable_NotMappedToEnd = "ViewGen_Foreign_Key_ParentTable_NotMappedToEnd";

		// Token: 0x04001D45 RID: 7493
		internal const string ViewGen_Foreign_Key = "ViewGen_Foreign_Key";

		// Token: 0x04001D46 RID: 7494
		internal const string ViewGen_Foreign_Key_UpperBound_MustBeOne = "ViewGen_Foreign_Key_UpperBound_MustBeOne";

		// Token: 0x04001D47 RID: 7495
		internal const string ViewGen_Foreign_Key_LowerBound_MustBeOne = "ViewGen_Foreign_Key_LowerBound_MustBeOne";

		// Token: 0x04001D48 RID: 7496
		internal const string ViewGen_Foreign_Key_Missing_Relationship_Mapping = "ViewGen_Foreign_Key_Missing_Relationship_Mapping";

		// Token: 0x04001D49 RID: 7497
		internal const string ViewGen_Foreign_Key_Not_Guaranteed_InCSpace = "ViewGen_Foreign_Key_Not_Guaranteed_InCSpace";

		// Token: 0x04001D4A RID: 7498
		internal const string ViewGen_Foreign_Key_ColumnOrder_Incorrect = "ViewGen_Foreign_Key_ColumnOrder_Incorrect";

		// Token: 0x04001D4B RID: 7499
		internal const string ViewGen_AssociationSet_AsUserString = "ViewGen_AssociationSet_AsUserString";

		// Token: 0x04001D4C RID: 7500
		internal const string ViewGen_AssociationSet_AsUserString_Negated = "ViewGen_AssociationSet_AsUserString_Negated";

		// Token: 0x04001D4D RID: 7501
		internal const string ViewGen_EntitySet_AsUserString = "ViewGen_EntitySet_AsUserString";

		// Token: 0x04001D4E RID: 7502
		internal const string ViewGen_EntitySet_AsUserString_Negated = "ViewGen_EntitySet_AsUserString_Negated";

		// Token: 0x04001D4F RID: 7503
		internal const string ViewGen_EntityInstanceToken = "ViewGen_EntityInstanceToken";

		// Token: 0x04001D50 RID: 7504
		internal const string Viewgen_ConfigurationErrorMsg = "Viewgen_ConfigurationErrorMsg";

		// Token: 0x04001D51 RID: 7505
		internal const string ViewGen_HashOnMappingClosure_Not_Matching = "ViewGen_HashOnMappingClosure_Not_Matching";

		// Token: 0x04001D52 RID: 7506
		internal const string Viewgen_RightSideNotDisjoint = "Viewgen_RightSideNotDisjoint";

		// Token: 0x04001D53 RID: 7507
		internal const string Viewgen_QV_RewritingNotFound = "Viewgen_QV_RewritingNotFound";

		// Token: 0x04001D54 RID: 7508
		internal const string Viewgen_NullableMappingForNonNullableColumn = "Viewgen_NullableMappingForNonNullableColumn";

		// Token: 0x04001D55 RID: 7509
		internal const string Viewgen_ErrorPattern_ConditionMemberIsMapped = "Viewgen_ErrorPattern_ConditionMemberIsMapped";

		// Token: 0x04001D56 RID: 7510
		internal const string Viewgen_ErrorPattern_DuplicateConditionValue = "Viewgen_ErrorPattern_DuplicateConditionValue";

		// Token: 0x04001D57 RID: 7511
		internal const string Viewgen_ErrorPattern_TableMappedToMultipleES = "Viewgen_ErrorPattern_TableMappedToMultipleES";

		// Token: 0x04001D58 RID: 7512
		internal const string Viewgen_ErrorPattern_Partition_Disj_Eq = "Viewgen_ErrorPattern_Partition_Disj_Eq";

		// Token: 0x04001D59 RID: 7513
		internal const string Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember = "Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember";

		// Token: 0x04001D5A RID: 7514
		internal const string Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition = "Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition";

		// Token: 0x04001D5B RID: 7515
		internal const string Viewgen_ErrorPattern_Partition_Disj_Subs_Ref = "Viewgen_ErrorPattern_Partition_Disj_Subs_Ref";

		// Token: 0x04001D5C RID: 7516
		internal const string Viewgen_ErrorPattern_Partition_Disj_Subs = "Viewgen_ErrorPattern_Partition_Disj_Subs";

		// Token: 0x04001D5D RID: 7517
		internal const string Viewgen_ErrorPattern_Partition_Disj_Unk = "Viewgen_ErrorPattern_Partition_Disj_Unk";

		// Token: 0x04001D5E RID: 7518
		internal const string Viewgen_ErrorPattern_Partition_Eq_Disj = "Viewgen_ErrorPattern_Partition_Eq_Disj";

		// Token: 0x04001D5F RID: 7519
		internal const string Viewgen_ErrorPattern_Partition_Eq_Subs_Ref = "Viewgen_ErrorPattern_Partition_Eq_Subs_Ref";

		// Token: 0x04001D60 RID: 7520
		internal const string Viewgen_ErrorPattern_Partition_Eq_Subs = "Viewgen_ErrorPattern_Partition_Eq_Subs";

		// Token: 0x04001D61 RID: 7521
		internal const string Viewgen_ErrorPattern_Partition_Eq_Unk = "Viewgen_ErrorPattern_Partition_Eq_Unk";

		// Token: 0x04001D62 RID: 7522
		internal const string Viewgen_ErrorPattern_Partition_Eq_Unk_Association = "Viewgen_ErrorPattern_Partition_Eq_Unk_Association";

		// Token: 0x04001D63 RID: 7523
		internal const string Viewgen_ErrorPattern_Partition_Sub_Disj = "Viewgen_ErrorPattern_Partition_Sub_Disj";

		// Token: 0x04001D64 RID: 7524
		internal const string Viewgen_ErrorPattern_Partition_Sub_Eq = "Viewgen_ErrorPattern_Partition_Sub_Eq";

		// Token: 0x04001D65 RID: 7525
		internal const string Viewgen_ErrorPattern_Partition_Sub_Eq_Ref = "Viewgen_ErrorPattern_Partition_Sub_Eq_Ref";

		// Token: 0x04001D66 RID: 7526
		internal const string Viewgen_ErrorPattern_Partition_Sub_Unk = "Viewgen_ErrorPattern_Partition_Sub_Unk";

		// Token: 0x04001D67 RID: 7527
		internal const string Viewgen_NoJoinKeyOrFK = "Viewgen_NoJoinKeyOrFK";

		// Token: 0x04001D68 RID: 7528
		internal const string Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct = "Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct";

		// Token: 0x04001D69 RID: 7529
		internal const string Validator_EmptyIdentity = "Validator_EmptyIdentity";

		// Token: 0x04001D6A RID: 7530
		internal const string Validator_CollectionHasNoTypeUsage = "Validator_CollectionHasNoTypeUsage";

		// Token: 0x04001D6B RID: 7531
		internal const string Validator_NoKeyMembers = "Validator_NoKeyMembers";

		// Token: 0x04001D6C RID: 7532
		internal const string Validator_FacetTypeIsNull = "Validator_FacetTypeIsNull";

		// Token: 0x04001D6D RID: 7533
		internal const string Validator_MemberHasNullDeclaringType = "Validator_MemberHasNullDeclaringType";

		// Token: 0x04001D6E RID: 7534
		internal const string Validator_MemberHasNullTypeUsage = "Validator_MemberHasNullTypeUsage";

		// Token: 0x04001D6F RID: 7535
		internal const string Validator_ItemAttributeHasNullTypeUsage = "Validator_ItemAttributeHasNullTypeUsage";

		// Token: 0x04001D70 RID: 7536
		internal const string Validator_RefTypeHasNullEntityType = "Validator_RefTypeHasNullEntityType";

		// Token: 0x04001D71 RID: 7537
		internal const string Validator_TypeUsageHasNullEdmType = "Validator_TypeUsageHasNullEdmType";

		// Token: 0x04001D72 RID: 7538
		internal const string Validator_BaseTypeHasMemberOfSameName = "Validator_BaseTypeHasMemberOfSameName";

		// Token: 0x04001D73 RID: 7539
		internal const string Validator_CollectionTypesCannotHaveBaseType = "Validator_CollectionTypesCannotHaveBaseType";

		// Token: 0x04001D74 RID: 7540
		internal const string Validator_RefTypesCannotHaveBaseType = "Validator_RefTypesCannotHaveBaseType";

		// Token: 0x04001D75 RID: 7541
		internal const string Validator_TypeHasNoName = "Validator_TypeHasNoName";

		// Token: 0x04001D76 RID: 7542
		internal const string Validator_TypeHasNoNamespace = "Validator_TypeHasNoNamespace";

		// Token: 0x04001D77 RID: 7543
		internal const string Validator_FacetHasNoName = "Validator_FacetHasNoName";

		// Token: 0x04001D78 RID: 7544
		internal const string Validator_MemberHasNoName = "Validator_MemberHasNoName";

		// Token: 0x04001D79 RID: 7545
		internal const string Validator_MetadataPropertyHasNoName = "Validator_MetadataPropertyHasNoName";

		// Token: 0x04001D7A RID: 7546
		internal const string Validator_NullableEntityKeyProperty = "Validator_NullableEntityKeyProperty";

		// Token: 0x04001D7B RID: 7547
		internal const string Validator_OSpace_InvalidNavPropReturnType = "Validator_OSpace_InvalidNavPropReturnType";

		// Token: 0x04001D7C RID: 7548
		internal const string Validator_OSpace_ScalarPropertyNotPrimitive = "Validator_OSpace_ScalarPropertyNotPrimitive";

		// Token: 0x04001D7D RID: 7549
		internal const string Validator_OSpace_ComplexPropertyNotComplex = "Validator_OSpace_ComplexPropertyNotComplex";

		// Token: 0x04001D7E RID: 7550
		internal const string Validator_OSpace_Convention_MultipleTypesWithSameName = "Validator_OSpace_Convention_MultipleTypesWithSameName";

		// Token: 0x04001D7F RID: 7551
		internal const string Validator_OSpace_Convention_NonPrimitiveTypeProperty = "Validator_OSpace_Convention_NonPrimitiveTypeProperty";

		// Token: 0x04001D80 RID: 7552
		internal const string Validator_OSpace_Convention_MissingRequiredProperty = "Validator_OSpace_Convention_MissingRequiredProperty";

		// Token: 0x04001D81 RID: 7553
		internal const string Validator_OSpace_Convention_BaseTypeIncompatible = "Validator_OSpace_Convention_BaseTypeIncompatible";

		// Token: 0x04001D82 RID: 7554
		internal const string Validator_OSpace_Convention_MissingOSpaceType = "Validator_OSpace_Convention_MissingOSpaceType";

		// Token: 0x04001D83 RID: 7555
		internal const string Validator_OSpace_Convention_RelationshipNotLoaded = "Validator_OSpace_Convention_RelationshipNotLoaded";

		// Token: 0x04001D84 RID: 7556
		internal const string Validator_OSpace_Convention_AttributeAssemblyReferenced = "Validator_OSpace_Convention_AttributeAssemblyReferenced";

		// Token: 0x04001D85 RID: 7557
		internal const string Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter = "Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter";

		// Token: 0x04001D86 RID: 7558
		internal const string Validator_OSpace_Convention_AmbiguousClrType = "Validator_OSpace_Convention_AmbiguousClrType";

		// Token: 0x04001D87 RID: 7559
		internal const string Validator_OSpace_Convention_Struct = "Validator_OSpace_Convention_Struct";

		// Token: 0x04001D88 RID: 7560
		internal const string Validator_OSpace_Convention_BaseTypeNotLoaded = "Validator_OSpace_Convention_BaseTypeNotLoaded";

		// Token: 0x04001D89 RID: 7561
		internal const string Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch = "Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch";

		// Token: 0x04001D8A RID: 7562
		internal const string Validator_OSpace_Convention_NonMatchingUnderlyingTypes = "Validator_OSpace_Convention_NonMatchingUnderlyingTypes";

		// Token: 0x04001D8B RID: 7563
		internal const string Validator_UnsupportedEnumUnderlyingType = "Validator_UnsupportedEnumUnderlyingType";

		// Token: 0x04001D8C RID: 7564
		internal const string ExtraInfo = "ExtraInfo";

		// Token: 0x04001D8D RID: 7565
		internal const string Metadata_General_Error = "Metadata_General_Error";

		// Token: 0x04001D8E RID: 7566
		internal const string InvalidNumberOfParametersForAggregateFunction = "InvalidNumberOfParametersForAggregateFunction";

		// Token: 0x04001D8F RID: 7567
		internal const string InvalidParameterTypeForAggregateFunction = "InvalidParameterTypeForAggregateFunction";

		// Token: 0x04001D90 RID: 7568
		internal const string InvalidSchemaEncountered = "InvalidSchemaEncountered";

		// Token: 0x04001D91 RID: 7569
		internal const string SystemNamespaceEncountered = "SystemNamespaceEncountered";

		// Token: 0x04001D92 RID: 7570
		internal const string NoCollectionForSpace = "NoCollectionForSpace";

		// Token: 0x04001D93 RID: 7571
		internal const string OperationOnReadOnlyCollection = "OperationOnReadOnlyCollection";

		// Token: 0x04001D94 RID: 7572
		internal const string OperationOnReadOnlyItem = "OperationOnReadOnlyItem";

		// Token: 0x04001D95 RID: 7573
		internal const string EntitySetInAnotherContainer = "EntitySetInAnotherContainer";

		// Token: 0x04001D96 RID: 7574
		internal const string InvalidKeyMember = "InvalidKeyMember";

		// Token: 0x04001D97 RID: 7575
		internal const string InvalidFileExtension = "InvalidFileExtension";

		// Token: 0x04001D98 RID: 7576
		internal const string NewTypeConflictsWithExistingType = "NewTypeConflictsWithExistingType";

		// Token: 0x04001D99 RID: 7577
		internal const string NotValidInputPath = "NotValidInputPath";

		// Token: 0x04001D9A RID: 7578
		internal const string UnableToDetermineApplicationContext = "UnableToDetermineApplicationContext";

		// Token: 0x04001D9B RID: 7579
		internal const string WildcardEnumeratorReturnedNull = "WildcardEnumeratorReturnedNull";

		// Token: 0x04001D9C RID: 7580
		internal const string InvalidUseOfWebPath = "InvalidUseOfWebPath";

		// Token: 0x04001D9D RID: 7581
		internal const string UnableToFindReflectedType = "UnableToFindReflectedType";

		// Token: 0x04001D9E RID: 7582
		internal const string AssemblyMissingFromAssembliesToConsider = "AssemblyMissingFromAssembliesToConsider";

		// Token: 0x04001D9F RID: 7583
		internal const string UnableToLoadResource = "UnableToLoadResource";

		// Token: 0x04001DA0 RID: 7584
		internal const string EdmVersionNotSupportedByRuntime = "EdmVersionNotSupportedByRuntime";

		// Token: 0x04001DA1 RID: 7585
		internal const string AtleastOneSSDLNeeded = "AtleastOneSSDLNeeded";

		// Token: 0x04001DA2 RID: 7586
		internal const string InvalidMetadataPath = "InvalidMetadataPath";

		// Token: 0x04001DA3 RID: 7587
		internal const string UnableToResolveAssembly = "UnableToResolveAssembly";

		// Token: 0x04001DA4 RID: 7588
		internal const string DuplicatedFunctionoverloads = "DuplicatedFunctionoverloads";

		// Token: 0x04001DA5 RID: 7589
		internal const string EntitySetNotInCSPace = "EntitySetNotInCSPace";

		// Token: 0x04001DA6 RID: 7590
		internal const string TypeNotInEntitySet = "TypeNotInEntitySet";

		// Token: 0x04001DA7 RID: 7591
		internal const string TypeNotInAssociationSet = "TypeNotInAssociationSet";

		// Token: 0x04001DA8 RID: 7592
		internal const string DifferentSchemaVersionInCollection = "DifferentSchemaVersionInCollection";

		// Token: 0x04001DA9 RID: 7593
		internal const string InvalidCollectionForMapping = "InvalidCollectionForMapping";

		// Token: 0x04001DAA RID: 7594
		internal const string OnlyStoreConnectionsSupported = "OnlyStoreConnectionsSupported";

		// Token: 0x04001DAB RID: 7595
		internal const string StoreItemCollectionMustHaveOneArtifact = "StoreItemCollectionMustHaveOneArtifact";

		// Token: 0x04001DAC RID: 7596
		internal const string CheckArgumentContainsNullFailed = "CheckArgumentContainsNullFailed";

		// Token: 0x04001DAD RID: 7597
		internal const string InvalidRelationshipSetName = "InvalidRelationshipSetName";

		// Token: 0x04001DAE RID: 7598
		internal const string InvalidEntitySetName = "InvalidEntitySetName";

		// Token: 0x04001DAF RID: 7599
		internal const string OnlyFunctionImportsCanBeAddedToEntityContainer = "OnlyFunctionImportsCanBeAddedToEntityContainer";

		// Token: 0x04001DB0 RID: 7600
		internal const string ItemInvalidIdentity = "ItemInvalidIdentity";

		// Token: 0x04001DB1 RID: 7601
		internal const string ItemDuplicateIdentity = "ItemDuplicateIdentity";

		// Token: 0x04001DB2 RID: 7602
		internal const string NotStringTypeForTypeUsage = "NotStringTypeForTypeUsage";

		// Token: 0x04001DB3 RID: 7603
		internal const string NotBinaryTypeForTypeUsage = "NotBinaryTypeForTypeUsage";

		// Token: 0x04001DB4 RID: 7604
		internal const string NotDateTimeTypeForTypeUsage = "NotDateTimeTypeForTypeUsage";

		// Token: 0x04001DB5 RID: 7605
		internal const string NotDateTimeOffsetTypeForTypeUsage = "NotDateTimeOffsetTypeForTypeUsage";

		// Token: 0x04001DB6 RID: 7606
		internal const string NotTimeTypeForTypeUsage = "NotTimeTypeForTypeUsage";

		// Token: 0x04001DB7 RID: 7607
		internal const string NotDecimalTypeForTypeUsage = "NotDecimalTypeForTypeUsage";

		// Token: 0x04001DB8 RID: 7608
		internal const string ArrayTooSmall = "ArrayTooSmall";

		// Token: 0x04001DB9 RID: 7609
		internal const string MoreThanOneItemMatchesIdentity = "MoreThanOneItemMatchesIdentity";

		// Token: 0x04001DBA RID: 7610
		internal const string MissingDefaultValueForConstantFacet = "MissingDefaultValueForConstantFacet";

		// Token: 0x04001DBB RID: 7611
		internal const string MinAndMaxValueMustBeSameForConstantFacet = "MinAndMaxValueMustBeSameForConstantFacet";

		// Token: 0x04001DBC RID: 7612
		internal const string BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet = "BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet";

		// Token: 0x04001DBD RID: 7613
		internal const string MinAndMaxValueMustBeDifferentForNonConstantFacet = "MinAndMaxValueMustBeDifferentForNonConstantFacet";

		// Token: 0x04001DBE RID: 7614
		internal const string MinAndMaxMustBePositive = "MinAndMaxMustBePositive";

		// Token: 0x04001DBF RID: 7615
		internal const string MinMustBeLessThanMax = "MinMustBeLessThanMax";

		// Token: 0x04001DC0 RID: 7616
		internal const string SameRoleNameOnRelationshipAttribute = "SameRoleNameOnRelationshipAttribute";

		// Token: 0x04001DC1 RID: 7617
		internal const string RoleTypeInEdmRelationshipAttributeIsInvalidType = "RoleTypeInEdmRelationshipAttributeIsInvalidType";

		// Token: 0x04001DC2 RID: 7618
		internal const string TargetRoleNameInNavigationPropertyNotValid = "TargetRoleNameInNavigationPropertyNotValid";

		// Token: 0x04001DC3 RID: 7619
		internal const string RelationshipNameInNavigationPropertyNotValid = "RelationshipNameInNavigationPropertyNotValid";

		// Token: 0x04001DC4 RID: 7620
		internal const string NestedClassNotSupported = "NestedClassNotSupported";

		// Token: 0x04001DC5 RID: 7621
		internal const string NullParameterForEdmRelationshipAttribute = "NullParameterForEdmRelationshipAttribute";

		// Token: 0x04001DC6 RID: 7622
		internal const string NullRelationshipNameforEdmRelationshipAttribute = "NullRelationshipNameforEdmRelationshipAttribute";

		// Token: 0x04001DC7 RID: 7623
		internal const string NavigationPropertyRelationshipEndTypeMismatch = "NavigationPropertyRelationshipEndTypeMismatch";

		// Token: 0x04001DC8 RID: 7624
		internal const string AllArtifactsMustTargetSameProvider_InvariantName = "AllArtifactsMustTargetSameProvider_InvariantName";

		// Token: 0x04001DC9 RID: 7625
		internal const string AllArtifactsMustTargetSameProvider_ManifestToken = "AllArtifactsMustTargetSameProvider_ManifestToken";

		// Token: 0x04001DCA RID: 7626
		internal const string ProviderManifestTokenNotFound = "ProviderManifestTokenNotFound";

		// Token: 0x04001DCB RID: 7627
		internal const string FailedToRetrieveProviderManifest = "FailedToRetrieveProviderManifest";

		// Token: 0x04001DCC RID: 7628
		internal const string InvalidMaxLengthSize = "InvalidMaxLengthSize";

		// Token: 0x04001DCD RID: 7629
		internal const string ArgumentMustBeCSpaceType = "ArgumentMustBeCSpaceType";

		// Token: 0x04001DCE RID: 7630
		internal const string ArgumentMustBeOSpaceType = "ArgumentMustBeOSpaceType";

		// Token: 0x04001DCF RID: 7631
		internal const string FailedToFindOSpaceTypeMapping = "FailedToFindOSpaceTypeMapping";

		// Token: 0x04001DD0 RID: 7632
		internal const string FailedToFindCSpaceTypeMapping = "FailedToFindCSpaceTypeMapping";

		// Token: 0x04001DD1 RID: 7633
		internal const string FailedToFindClrTypeMapping = "FailedToFindClrTypeMapping";

		// Token: 0x04001DD2 RID: 7634
		internal const string GenericTypeNotSupported = "GenericTypeNotSupported";

		// Token: 0x04001DD3 RID: 7635
		internal const string InvalidEDMVersion = "InvalidEDMVersion";

		// Token: 0x04001DD4 RID: 7636
		internal const string Mapping_General_Error = "Mapping_General_Error";

		// Token: 0x04001DD5 RID: 7637
		internal const string Mapping_InvalidContent_General = "Mapping_InvalidContent_General";

		// Token: 0x04001DD6 RID: 7638
		internal const string Mapping_InvalidContent_EntityContainer = "Mapping_InvalidContent_EntityContainer";

		// Token: 0x04001DD7 RID: 7639
		internal const string Mapping_InvalidContent_StorageEntityContainer = "Mapping_InvalidContent_StorageEntityContainer";

		// Token: 0x04001DD8 RID: 7640
		internal const string Mapping_AlreadyMapped_StorageEntityContainer = "Mapping_AlreadyMapped_StorageEntityContainer";

		// Token: 0x04001DD9 RID: 7641
		internal const string Mapping_InvalidContent_Entity_Set = "Mapping_InvalidContent_Entity_Set";

		// Token: 0x04001DDA RID: 7642
		internal const string Mapping_InvalidContent_Entity_Type = "Mapping_InvalidContent_Entity_Type";

		// Token: 0x04001DDB RID: 7643
		internal const string Mapping_InvalidContent_AbstractEntity_FunctionMapping = "Mapping_InvalidContent_AbstractEntity_FunctionMapping";

		// Token: 0x04001DDC RID: 7644
		internal const string Mapping_InvalidContent_AbstractEntity_Type = "Mapping_InvalidContent_AbstractEntity_Type";

		// Token: 0x04001DDD RID: 7645
		internal const string Mapping_InvalidContent_AbstractEntity_IsOfType = "Mapping_InvalidContent_AbstractEntity_IsOfType";

		// Token: 0x04001DDE RID: 7646
		internal const string Mapping_InvalidContent_Entity_Type_For_Entity_Set = "Mapping_InvalidContent_Entity_Type_For_Entity_Set";

		// Token: 0x04001DDF RID: 7647
		internal const string Mapping_Invalid_Association_Type_For_Association_Set = "Mapping_Invalid_Association_Type_For_Association_Set";

		// Token: 0x04001DE0 RID: 7648
		internal const string Mapping_InvalidContent_Table = "Mapping_InvalidContent_Table";

		// Token: 0x04001DE1 RID: 7649
		internal const string Mapping_InvalidContent_Complex_Type = "Mapping_InvalidContent_Complex_Type";

		// Token: 0x04001DE2 RID: 7650
		internal const string Mapping_InvalidContent_Association_Set = "Mapping_InvalidContent_Association_Set";

		// Token: 0x04001DE3 RID: 7651
		internal const string Mapping_InvalidContent_AssociationSet_Condition = "Mapping_InvalidContent_AssociationSet_Condition";

		// Token: 0x04001DE4 RID: 7652
		internal const string Mapping_InvalidContent_ForeignKey_Association_Set = "Mapping_InvalidContent_ForeignKey_Association_Set";

		// Token: 0x04001DE5 RID: 7653
		internal const string Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK = "Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK";

		// Token: 0x04001DE6 RID: 7654
		internal const string Mapping_InvalidContent_Association_Type = "Mapping_InvalidContent_Association_Type";

		// Token: 0x04001DE7 RID: 7655
		internal const string Mapping_InvalidContent_EndProperty = "Mapping_InvalidContent_EndProperty";

		// Token: 0x04001DE8 RID: 7656
		internal const string Mapping_InvalidContent_Association_Type_Empty = "Mapping_InvalidContent_Association_Type_Empty";

		// Token: 0x04001DE9 RID: 7657
		internal const string Mapping_InvalidContent_Table_Expected = "Mapping_InvalidContent_Table_Expected";

		// Token: 0x04001DEA RID: 7658
		internal const string Mapping_InvalidContent_Cdm_Member = "Mapping_InvalidContent_Cdm_Member";

		// Token: 0x04001DEB RID: 7659
		internal const string Mapping_InvalidContent_Column = "Mapping_InvalidContent_Column";

		// Token: 0x04001DEC RID: 7660
		internal const string Mapping_InvalidContent_End = "Mapping_InvalidContent_End";

		// Token: 0x04001DED RID: 7661
		internal const string Mapping_InvalidContent_Container_SubElement = "Mapping_InvalidContent_Container_SubElement";

		// Token: 0x04001DEE RID: 7662
		internal const string Mapping_InvalidContent_Duplicate_Cdm_Member = "Mapping_InvalidContent_Duplicate_Cdm_Member";

		// Token: 0x04001DEF RID: 7663
		internal const string Mapping_InvalidContent_Duplicate_Condition_Member = "Mapping_InvalidContent_Duplicate_Condition_Member";

		// Token: 0x04001DF0 RID: 7664
		internal const string Mapping_InvalidContent_ConditionMapping_Both_Members = "Mapping_InvalidContent_ConditionMapping_Both_Members";

		// Token: 0x04001DF1 RID: 7665
		internal const string Mapping_InvalidContent_ConditionMapping_Either_Members = "Mapping_InvalidContent_ConditionMapping_Either_Members";

		// Token: 0x04001DF2 RID: 7666
		internal const string Mapping_InvalidContent_ConditionMapping_Both_Values = "Mapping_InvalidContent_ConditionMapping_Both_Values";

		// Token: 0x04001DF3 RID: 7667
		internal const string Mapping_InvalidContent_ConditionMapping_Either_Values = "Mapping_InvalidContent_ConditionMapping_Either_Values";

		// Token: 0x04001DF4 RID: 7668
		internal const string Mapping_InvalidContent_ConditionMapping_NonScalar = "Mapping_InvalidContent_ConditionMapping_NonScalar";

		// Token: 0x04001DF5 RID: 7669
		internal const string Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind = "Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind";

		// Token: 0x04001DF6 RID: 7670
		internal const string Mapping_InvalidContent_ConditionMapping_InvalidMember = "Mapping_InvalidContent_ConditionMapping_InvalidMember";

		// Token: 0x04001DF7 RID: 7671
		internal const string Mapping_InvalidContent_ConditionMapping_Computed = "Mapping_InvalidContent_ConditionMapping_Computed";

		// Token: 0x04001DF8 RID: 7672
		internal const string Mapping_InvalidContent_Emtpty_SetMap = "Mapping_InvalidContent_Emtpty_SetMap";

		// Token: 0x04001DF9 RID: 7673
		internal const string Mapping_InvalidContent_TypeMapping_QueryView = "Mapping_InvalidContent_TypeMapping_QueryView";

		// Token: 0x04001DFA RID: 7674
		internal const string Mapping_Default_OCMapping_Clr_Member = "Mapping_Default_OCMapping_Clr_Member";

		// Token: 0x04001DFB RID: 7675
		internal const string Mapping_Default_OCMapping_Clr_Member2 = "Mapping_Default_OCMapping_Clr_Member2";

		// Token: 0x04001DFC RID: 7676
		internal const string Mapping_Default_OCMapping_Invalid_MemberType = "Mapping_Default_OCMapping_Invalid_MemberType";

		// Token: 0x04001DFD RID: 7677
		internal const string Mapping_Default_OCMapping_MemberKind_Mismatch = "Mapping_Default_OCMapping_MemberKind_Mismatch";

		// Token: 0x04001DFE RID: 7678
		internal const string Mapping_Default_OCMapping_MultiplicityMismatch = "Mapping_Default_OCMapping_MultiplicityMismatch";

		// Token: 0x04001DFF RID: 7679
		internal const string Mapping_Default_OCMapping_Member_Count_Mismatch = "Mapping_Default_OCMapping_Member_Count_Mismatch";

		// Token: 0x04001E00 RID: 7680
		internal const string Mapping_Default_OCMapping_Member_Type_Mismatch = "Mapping_Default_OCMapping_Member_Type_Mismatch";

		// Token: 0x04001E01 RID: 7681
		internal const string Mapping_Enum_OCMapping_UnderlyingTypesMismatch = "Mapping_Enum_OCMapping_UnderlyingTypesMismatch";

		// Token: 0x04001E02 RID: 7682
		internal const string Mapping_Enum_OCMapping_MemberMismatch = "Mapping_Enum_OCMapping_MemberMismatch";

		// Token: 0x04001E03 RID: 7683
		internal const string Mapping_NotFound_EntityContainer = "Mapping_NotFound_EntityContainer";

		// Token: 0x04001E04 RID: 7684
		internal const string Mapping_Duplicate_CdmAssociationSet_StorageMap = "Mapping_Duplicate_CdmAssociationSet_StorageMap";

		// Token: 0x04001E05 RID: 7685
		internal const string Mapping_Invalid_CSRootElementMissing = "Mapping_Invalid_CSRootElementMissing";

		// Token: 0x04001E06 RID: 7686
		internal const string Mapping_ConditionValueTypeMismatch = "Mapping_ConditionValueTypeMismatch";

		// Token: 0x04001E07 RID: 7687
		internal const string Mapping_Storage_InvalidSpace = "Mapping_Storage_InvalidSpace";

		// Token: 0x04001E08 RID: 7688
		internal const string Mapping_Invalid_Member_Mapping = "Mapping_Invalid_Member_Mapping";

		// Token: 0x04001E09 RID: 7689
		internal const string Mapping_Invalid_CSide_ScalarProperty = "Mapping_Invalid_CSide_ScalarProperty";

		// Token: 0x04001E0A RID: 7690
		internal const string Mapping_Duplicate_Type = "Mapping_Duplicate_Type";

		// Token: 0x04001E0B RID: 7691
		internal const string Mapping_Duplicate_PropertyMap_CaseInsensitive = "Mapping_Duplicate_PropertyMap_CaseInsensitive";

		// Token: 0x04001E0C RID: 7692
		internal const string Mapping_Enum_EmptyValue = "Mapping_Enum_EmptyValue";

		// Token: 0x04001E0D RID: 7693
		internal const string Mapping_Enum_InvalidValue = "Mapping_Enum_InvalidValue";

		// Token: 0x04001E0E RID: 7694
		internal const string Mapping_InvalidMappingSchema_Parsing = "Mapping_InvalidMappingSchema_Parsing";

		// Token: 0x04001E0F RID: 7695
		internal const string Mapping_InvalidMappingSchema_validation = "Mapping_InvalidMappingSchema_validation";

		// Token: 0x04001E10 RID: 7696
		internal const string Mapping_Object_InvalidType = "Mapping_Object_InvalidType";

		// Token: 0x04001E11 RID: 7697
		internal const string Mapping_Provider_WrongConnectionType = "Mapping_Provider_WrongConnectionType";

		// Token: 0x04001E12 RID: 7698
		internal const string Mapping_Views_For_Extent_Not_Generated = "Mapping_Views_For_Extent_Not_Generated";

		// Token: 0x04001E13 RID: 7699
		internal const string Mapping_TableName_QueryView = "Mapping_TableName_QueryView";

		// Token: 0x04001E14 RID: 7700
		internal const string Mapping_Empty_QueryView = "Mapping_Empty_QueryView";

		// Token: 0x04001E15 RID: 7701
		internal const string Mapping_Empty_QueryView_OfType = "Mapping_Empty_QueryView_OfType";

		// Token: 0x04001E16 RID: 7702
		internal const string Mapping_Empty_QueryView_OfTypeOnly = "Mapping_Empty_QueryView_OfTypeOnly";

		// Token: 0x04001E17 RID: 7703
		internal const string Mapping_QueryView_PropertyMaps = "Mapping_QueryView_PropertyMaps";

		// Token: 0x04001E18 RID: 7704
		internal const string Mapping_Invalid_QueryView = "Mapping_Invalid_QueryView";

		// Token: 0x04001E19 RID: 7705
		internal const string Mapping_Invalid_QueryView2 = "Mapping_Invalid_QueryView2";

		// Token: 0x04001E1A RID: 7706
		internal const string Mapping_Invalid_QueryView_Type = "Mapping_Invalid_QueryView_Type";

		// Token: 0x04001E1B RID: 7707
		internal const string Mapping_TypeName_For_First_QueryView = "Mapping_TypeName_For_First_QueryView";

		// Token: 0x04001E1C RID: 7708
		internal const string Mapping_AllQueryViewAtCompileTime = "Mapping_AllQueryViewAtCompileTime";

		// Token: 0x04001E1D RID: 7709
		internal const string Mapping_QueryViewMultipleTypeInTypeName = "Mapping_QueryViewMultipleTypeInTypeName";

		// Token: 0x04001E1E RID: 7710
		internal const string Mapping_QueryView_Duplicate_OfType = "Mapping_QueryView_Duplicate_OfType";

		// Token: 0x04001E1F RID: 7711
		internal const string Mapping_QueryView_Duplicate_OfTypeOnly = "Mapping_QueryView_Duplicate_OfTypeOnly";

		// Token: 0x04001E20 RID: 7712
		internal const string Mapping_QueryView_TypeName_Not_Defined = "Mapping_QueryView_TypeName_Not_Defined";

		// Token: 0x04001E21 RID: 7713
		internal const string Mapping_QueryView_For_Base_Type = "Mapping_QueryView_For_Base_Type";

		// Token: 0x04001E22 RID: 7714
		internal const string Mapping_UnsupportedExpressionKind_QueryView = "Mapping_UnsupportedExpressionKind_QueryView";

		// Token: 0x04001E23 RID: 7715
		internal const string Mapping_UnsupportedFunctionCall_QueryView = "Mapping_UnsupportedFunctionCall_QueryView";

		// Token: 0x04001E24 RID: 7716
		internal const string Mapping_UnsupportedScanTarget_QueryView = "Mapping_UnsupportedScanTarget_QueryView";

		// Token: 0x04001E25 RID: 7717
		internal const string Mapping_UnsupportedPropertyKind_QueryView = "Mapping_UnsupportedPropertyKind_QueryView";

		// Token: 0x04001E26 RID: 7718
		internal const string Mapping_UnsupportedInitialization_QueryView = "Mapping_UnsupportedInitialization_QueryView";

		// Token: 0x04001E27 RID: 7719
		internal const string Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView = "Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView";

		// Token: 0x04001E28 RID: 7720
		internal const string Mapping_Invalid_Query_Views_MissingSetClosure = "Mapping_Invalid_Query_Views_MissingSetClosure";

		// Token: 0x04001E29 RID: 7721
		internal const string DbMappingViewCacheTypeAttribute_InvalidContextType = "DbMappingViewCacheTypeAttribute_InvalidContextType";

		// Token: 0x04001E2A RID: 7722
		internal const string DbMappingViewCacheTypeAttribute_CacheTypeNotFound = "DbMappingViewCacheTypeAttribute_CacheTypeNotFound";

		// Token: 0x04001E2B RID: 7723
		internal const string DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType = "DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType";

		// Token: 0x04001E2C RID: 7724
		internal const string DbMappingViewCacheFactory_CreateFailure = "DbMappingViewCacheFactory_CreateFailure";

		// Token: 0x04001E2D RID: 7725
		internal const string Generated_View_Type_Super_Class = "Generated_View_Type_Super_Class";

		// Token: 0x04001E2E RID: 7726
		internal const string Generated_Views_Invalid_Extent = "Generated_Views_Invalid_Extent";

		// Token: 0x04001E2F RID: 7727
		internal const string MappingViewCacheFactory_MustNotChange = "MappingViewCacheFactory_MustNotChange";

		// Token: 0x04001E30 RID: 7728
		internal const string Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace = "Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace";

		// Token: 0x04001E31 RID: 7729
		internal const string Mapping_AbstractTypeMappingToNonAbstractType = "Mapping_AbstractTypeMappingToNonAbstractType";

		// Token: 0x04001E32 RID: 7730
		internal const string Mapping_EnumTypeMappingToNonEnumType = "Mapping_EnumTypeMappingToNonEnumType";

		// Token: 0x04001E33 RID: 7731
		internal const string StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping = "StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping";

		// Token: 0x04001E34 RID: 7732
		internal const string Mapping_InvalidContent_IsTypeOfNotTerminated = "Mapping_InvalidContent_IsTypeOfNotTerminated";

		// Token: 0x04001E35 RID: 7733
		internal const string Mapping_CannotMapCLRTypeMultipleTimes = "Mapping_CannotMapCLRTypeMultipleTimes";

		// Token: 0x04001E36 RID: 7734
		internal const string Mapping_ModificationFunction_In_Table_Context = "Mapping_ModificationFunction_In_Table_Context";

		// Token: 0x04001E37 RID: 7735
		internal const string Mapping_ModificationFunction_Multiple_Types = "Mapping_ModificationFunction_Multiple_Types";

		// Token: 0x04001E38 RID: 7736
		internal const string Mapping_ModificationFunction_UnknownFunction = "Mapping_ModificationFunction_UnknownFunction";

		// Token: 0x04001E39 RID: 7737
		internal const string Mapping_ModificationFunction_AmbiguousFunction = "Mapping_ModificationFunction_AmbiguousFunction";

		// Token: 0x04001E3A RID: 7738
		internal const string Mapping_ModificationFunction_NotValidFunction = "Mapping_ModificationFunction_NotValidFunction";

		// Token: 0x04001E3B RID: 7739
		internal const string Mapping_ModificationFunction_NotValidFunctionParameter = "Mapping_ModificationFunction_NotValidFunctionParameter";

		// Token: 0x04001E3C RID: 7740
		internal const string Mapping_ModificationFunction_MissingParameter = "Mapping_ModificationFunction_MissingParameter";

		// Token: 0x04001E3D RID: 7741
		internal const string Mapping_ModificationFunction_AssociationSetDoesNotExist = "Mapping_ModificationFunction_AssociationSetDoesNotExist";

		// Token: 0x04001E3E RID: 7742
		internal const string Mapping_ModificationFunction_AssociationSetRoleDoesNotExist = "Mapping_ModificationFunction_AssociationSetRoleDoesNotExist";

		// Token: 0x04001E3F RID: 7743
		internal const string Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet = "Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet";

		// Token: 0x04001E40 RID: 7744
		internal const string Mapping_ModificationFunction_AssociationSetCardinality = "Mapping_ModificationFunction_AssociationSetCardinality";

		// Token: 0x04001E41 RID: 7745
		internal const string Mapping_ModificationFunction_ComplexTypeNotFound = "Mapping_ModificationFunction_ComplexTypeNotFound";

		// Token: 0x04001E42 RID: 7746
		internal const string Mapping_ModificationFunction_WrongComplexType = "Mapping_ModificationFunction_WrongComplexType";

		// Token: 0x04001E43 RID: 7747
		internal const string Mapping_ModificationFunction_MissingVersion = "Mapping_ModificationFunction_MissingVersion";

		// Token: 0x04001E44 RID: 7748
		internal const string Mapping_ModificationFunction_VersionMustBeOriginal = "Mapping_ModificationFunction_VersionMustBeOriginal";

		// Token: 0x04001E45 RID: 7749
		internal const string Mapping_ModificationFunction_VersionMustBeCurrent = "Mapping_ModificationFunction_VersionMustBeCurrent";

		// Token: 0x04001E46 RID: 7750
		internal const string Mapping_ModificationFunction_ParameterNotFound = "Mapping_ModificationFunction_ParameterNotFound";

		// Token: 0x04001E47 RID: 7751
		internal const string Mapping_ModificationFunction_PropertyNotFound = "Mapping_ModificationFunction_PropertyNotFound";

		// Token: 0x04001E48 RID: 7752
		internal const string Mapping_ModificationFunction_PropertyNotKey = "Mapping_ModificationFunction_PropertyNotKey";

		// Token: 0x04001E49 RID: 7753
		internal const string Mapping_ModificationFunction_ParameterBoundTwice = "Mapping_ModificationFunction_ParameterBoundTwice";

		// Token: 0x04001E4A RID: 7754
		internal const string Mapping_ModificationFunction_RedundantEntityTypeMapping = "Mapping_ModificationFunction_RedundantEntityTypeMapping";

		// Token: 0x04001E4B RID: 7755
		internal const string Mapping_ModificationFunction_MissingSetClosure = "Mapping_ModificationFunction_MissingSetClosure";

		// Token: 0x04001E4C RID: 7756
		internal const string Mapping_ModificationFunction_MissingEntityType = "Mapping_ModificationFunction_MissingEntityType";

		// Token: 0x04001E4D RID: 7757
		internal const string Mapping_ModificationFunction_PropertyParameterTypeMismatch = "Mapping_ModificationFunction_PropertyParameterTypeMismatch";

		// Token: 0x04001E4E RID: 7758
		internal const string Mapping_ModificationFunction_AssociationSetAmbiguous = "Mapping_ModificationFunction_AssociationSetAmbiguous";

		// Token: 0x04001E4F RID: 7759
		internal const string Mapping_ModificationFunction_MultipleEndsOfAssociationMapped = "Mapping_ModificationFunction_MultipleEndsOfAssociationMapped";

		// Token: 0x04001E50 RID: 7760
		internal const string Mapping_ModificationFunction_AmbiguousResultBinding = "Mapping_ModificationFunction_AmbiguousResultBinding";

		// Token: 0x04001E51 RID: 7761
		internal const string Mapping_ModificationFunction_AssociationSetNotMappedForOperation = "Mapping_ModificationFunction_AssociationSetNotMappedForOperation";

		// Token: 0x04001E52 RID: 7762
		internal const string Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType = "Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType";

		// Token: 0x04001E53 RID: 7763
		internal const string Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation = "Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation";

		// Token: 0x04001E54 RID: 7764
		internal const string Mapping_StoreTypeMismatch_ScalarPropertyMapping = "Mapping_StoreTypeMismatch_ScalarPropertyMapping";

		// Token: 0x04001E55 RID: 7765
		internal const string Mapping_DistinctFlagInReadWriteContainer = "Mapping_DistinctFlagInReadWriteContainer";

		// Token: 0x04001E56 RID: 7766
		internal const string Mapping_ProviderReturnsNullType = "Mapping_ProviderReturnsNullType";

		// Token: 0x04001E57 RID: 7767
		internal const string Mapping_DifferentEdmStoreVersion = "Mapping_DifferentEdmStoreVersion";

		// Token: 0x04001E58 RID: 7768
		internal const string Mapping_DifferentMappingEdmStoreVersion = "Mapping_DifferentMappingEdmStoreVersion";

		// Token: 0x04001E59 RID: 7769
		internal const string Mapping_FunctionImport_StoreFunctionDoesNotExist = "Mapping_FunctionImport_StoreFunctionDoesNotExist";

		// Token: 0x04001E5A RID: 7770
		internal const string Mapping_FunctionImport_FunctionImportDoesNotExist = "Mapping_FunctionImport_FunctionImportDoesNotExist";

		// Token: 0x04001E5B RID: 7771
		internal const string Mapping_FunctionImport_FunctionImportMappedMultipleTimes = "Mapping_FunctionImport_FunctionImportMappedMultipleTimes";

		// Token: 0x04001E5C RID: 7772
		internal const string Mapping_FunctionImport_TargetFunctionMustBeNonComposable = "Mapping_FunctionImport_TargetFunctionMustBeNonComposable";

		// Token: 0x04001E5D RID: 7773
		internal const string Mapping_FunctionImport_TargetFunctionMustBeComposable = "Mapping_FunctionImport_TargetFunctionMustBeComposable";

		// Token: 0x04001E5E RID: 7774
		internal const string Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter = "Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter";

		// Token: 0x04001E5F RID: 7775
		internal const string Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter = "Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter";

		// Token: 0x04001E60 RID: 7776
		internal const string Mapping_FunctionImport_IncompatibleParameterMode = "Mapping_FunctionImport_IncompatibleParameterMode";

		// Token: 0x04001E61 RID: 7777
		internal const string Mapping_FunctionImport_IncompatibleParameterType = "Mapping_FunctionImport_IncompatibleParameterType";

		// Token: 0x04001E62 RID: 7778
		internal const string Mapping_FunctionImport_IncompatibleEnumParameterType = "Mapping_FunctionImport_IncompatibleEnumParameterType";

		// Token: 0x04001E63 RID: 7779
		internal const string Mapping_FunctionImport_RowsAffectedParameterDoesNotExist = "Mapping_FunctionImport_RowsAffectedParameterDoesNotExist";

		// Token: 0x04001E64 RID: 7780
		internal const string Mapping_FunctionImport_RowsAffectedParameterHasWrongType = "Mapping_FunctionImport_RowsAffectedParameterHasWrongType";

		// Token: 0x04001E65 RID: 7781
		internal const string Mapping_FunctionImport_RowsAffectedParameterHasWrongMode = "Mapping_FunctionImport_RowsAffectedParameterHasWrongMode";

		// Token: 0x04001E66 RID: 7782
		internal const string Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet = "Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet";

		// Token: 0x04001E67 RID: 7783
		internal const string Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet = "Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet";

		// Token: 0x04001E68 RID: 7784
		internal const string Mapping_FunctionImport_ConditionValueTypeMismatch = "Mapping_FunctionImport_ConditionValueTypeMismatch";

		// Token: 0x04001E69 RID: 7785
		internal const string Mapping_FunctionImport_UnsupportedType = "Mapping_FunctionImport_UnsupportedType";

		// Token: 0x04001E6A RID: 7786
		internal const string Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount = "Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount";

		// Token: 0x04001E6B RID: 7787
		internal const string Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType = "Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType";

		// Token: 0x04001E6C RID: 7788
		internal const string Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected = "Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected";

		// Token: 0x04001E6D RID: 7789
		internal const string Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected = "Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected";

		// Token: 0x04001E6E RID: 7790
		internal const string Mapping_FunctionImport_ResultMapping_InvalidSType = "Mapping_FunctionImport_ResultMapping_InvalidSType";

		// Token: 0x04001E6F RID: 7791
		internal const string Mapping_FunctionImport_PropertyNotMapped = "Mapping_FunctionImport_PropertyNotMapped";

		// Token: 0x04001E70 RID: 7792
		internal const string Mapping_FunctionImport_ImplicitMappingForAbstractReturnType = "Mapping_FunctionImport_ImplicitMappingForAbstractReturnType";

		// Token: 0x04001E71 RID: 7793
		internal const string Mapping_FunctionImport_ScalarMappingToMulticolumnTVF = "Mapping_FunctionImport_ScalarMappingToMulticolumnTVF";

		// Token: 0x04001E72 RID: 7794
		internal const string Mapping_FunctionImport_ScalarMappingTypeMismatch = "Mapping_FunctionImport_ScalarMappingTypeMismatch";

		// Token: 0x04001E73 RID: 7795
		internal const string Mapping_FunctionImport_UnreachableType = "Mapping_FunctionImport_UnreachableType";

		// Token: 0x04001E74 RID: 7796
		internal const string Mapping_FunctionImport_UnreachableIsTypeOf = "Mapping_FunctionImport_UnreachableIsTypeOf";

		// Token: 0x04001E75 RID: 7797
		internal const string Mapping_FunctionImport_FunctionAmbiguous = "Mapping_FunctionImport_FunctionAmbiguous";

		// Token: 0x04001E76 RID: 7798
		internal const string Mapping_FunctionImport_CannotInferTargetFunctionKeys = "Mapping_FunctionImport_CannotInferTargetFunctionKeys";

		// Token: 0x04001E77 RID: 7799
		internal const string Entity_EntityCantHaveMultipleChangeTrackers = "Entity_EntityCantHaveMultipleChangeTrackers";

		// Token: 0x04001E78 RID: 7800
		internal const string ComplexObject_NullableComplexTypesNotSupported = "ComplexObject_NullableComplexTypesNotSupported";

		// Token: 0x04001E79 RID: 7801
		internal const string ComplexObject_ComplexObjectAlreadyAttachedToParent = "ComplexObject_ComplexObjectAlreadyAttachedToParent";

		// Token: 0x04001E7A RID: 7802
		internal const string ComplexObject_ComplexChangeRequestedOnScalarProperty = "ComplexObject_ComplexChangeRequestedOnScalarProperty";

		// Token: 0x04001E7B RID: 7803
		internal const string ObjectStateEntry_SetModifiedOnInvalidProperty = "ObjectStateEntry_SetModifiedOnInvalidProperty";

		// Token: 0x04001E7C RID: 7804
		internal const string ObjectStateEntry_OriginalValuesDoesNotExist = "ObjectStateEntry_OriginalValuesDoesNotExist";

		// Token: 0x04001E7D RID: 7805
		internal const string ObjectStateEntry_CurrentValuesDoesNotExist = "ObjectStateEntry_CurrentValuesDoesNotExist";

		// Token: 0x04001E7E RID: 7806
		internal const string ObjectStateEntry_InvalidState = "ObjectStateEntry_InvalidState";

		// Token: 0x04001E7F RID: 7807
		internal const string ObjectStateEntry_CannotModifyKeyProperty = "ObjectStateEntry_CannotModifyKeyProperty";

		// Token: 0x04001E80 RID: 7808
		internal const string ObjectStateEntry_CantModifyRelationValues = "ObjectStateEntry_CantModifyRelationValues";

		// Token: 0x04001E81 RID: 7809
		internal const string ObjectStateEntry_CantModifyRelationState = "ObjectStateEntry_CantModifyRelationState";

		// Token: 0x04001E82 RID: 7810
		internal const string ObjectStateEntry_CantModifyDetachedDeletedEntries = "ObjectStateEntry_CantModifyDetachedDeletedEntries";

		// Token: 0x04001E83 RID: 7811
		internal const string ObjectStateEntry_SetModifiedStates = "ObjectStateEntry_SetModifiedStates";

		// Token: 0x04001E84 RID: 7812
		internal const string ObjectStateEntry_CantSetEntityKey = "ObjectStateEntry_CantSetEntityKey";

		// Token: 0x04001E85 RID: 7813
		internal const string ObjectStateEntry_CannotAccessKeyEntryValues = "ObjectStateEntry_CannotAccessKeyEntryValues";

		// Token: 0x04001E86 RID: 7814
		internal const string ObjectStateEntry_CannotModifyKeyEntryState = "ObjectStateEntry_CannotModifyKeyEntryState";

		// Token: 0x04001E87 RID: 7815
		internal const string ObjectStateEntry_CannotDeleteOnKeyEntry = "ObjectStateEntry_CannotDeleteOnKeyEntry";

		// Token: 0x04001E88 RID: 7816
		internal const string ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging = "ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging";

		// Token: 0x04001E89 RID: 7817
		internal const string ObjectStateEntry_ChangeOnUnmappedProperty = "ObjectStateEntry_ChangeOnUnmappedProperty";

		// Token: 0x04001E8A RID: 7818
		internal const string ObjectStateEntry_ChangeOnUnmappedComplexProperty = "ObjectStateEntry_ChangeOnUnmappedComplexProperty";

		// Token: 0x04001E8B RID: 7819
		internal const string ObjectStateEntry_ChangedInDifferentStateFromChanging = "ObjectStateEntry_ChangedInDifferentStateFromChanging";

		// Token: 0x04001E8C RID: 7820
		internal const string ObjectStateEntry_UnableToEnumerateCollection = "ObjectStateEntry_UnableToEnumerateCollection";

		// Token: 0x04001E8D RID: 7821
		internal const string ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers = "ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers";

		// Token: 0x04001E8E RID: 7822
		internal const string ObjectStateEntry_InvalidTypeForComplexTypeProperty = "ObjectStateEntry_InvalidTypeForComplexTypeProperty";

		// Token: 0x04001E8F RID: 7823
		internal const string ObjectStateEntry_ComplexObjectUsedMultipleTimes = "ObjectStateEntry_ComplexObjectUsedMultipleTimes";

		// Token: 0x04001E90 RID: 7824
		internal const string ObjectStateEntry_SetOriginalComplexProperties = "ObjectStateEntry_SetOriginalComplexProperties";

		// Token: 0x04001E91 RID: 7825
		internal const string ObjectStateEntry_NullOriginalValueForNonNullableProperty = "ObjectStateEntry_NullOriginalValueForNonNullableProperty";

		// Token: 0x04001E92 RID: 7826
		internal const string ObjectStateEntry_SetOriginalPrimaryKey = "ObjectStateEntry_SetOriginalPrimaryKey";

		// Token: 0x04001E93 RID: 7827
		internal const string ObjectStateManager_NoEntryExistForEntityKey = "ObjectStateManager_NoEntryExistForEntityKey";

		// Token: 0x04001E94 RID: 7828
		internal const string ObjectStateManager_NoEntryExistsForObject = "ObjectStateManager_NoEntryExistsForObject";

		// Token: 0x04001E95 RID: 7829
		internal const string ObjectStateManager_EntityNotTracked = "ObjectStateManager_EntityNotTracked";

		// Token: 0x04001E96 RID: 7830
		internal const string ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager = "ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager";

		// Token: 0x04001E97 RID: 7831
		internal const string ObjectStateManager_ObjectStateManagerContainsThisEntityKey = "ObjectStateManager_ObjectStateManagerContainsThisEntityKey";

		// Token: 0x04001E98 RID: 7832
		internal const string ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity = "ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity";

		// Token: 0x04001E99 RID: 7833
		internal const string ObjectStateManager_CannotFixUpKeyToExistingValues = "ObjectStateManager_CannotFixUpKeyToExistingValues";

		// Token: 0x04001E9A RID: 7834
		internal const string ObjectStateManager_KeyPropertyDoesntMatchValueInKey = "ObjectStateManager_KeyPropertyDoesntMatchValueInKey";

		// Token: 0x04001E9B RID: 7835
		internal const string ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach = "ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach";

		// Token: 0x04001E9C RID: 7836
		internal const string ObjectStateManager_InvalidKey = "ObjectStateManager_InvalidKey";

		// Token: 0x04001E9D RID: 7837
		internal const string ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType = "ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType";

		// Token: 0x04001E9E RID: 7838
		internal const string ObjectStateManager_AcceptChangesEntityKeyIsNotValid = "ObjectStateManager_AcceptChangesEntityKeyIsNotValid";

		// Token: 0x04001E9F RID: 7839
		internal const string ObjectStateManager_EntityConflictsWithKeyEntry = "ObjectStateManager_EntityConflictsWithKeyEntry";

		// Token: 0x04001EA0 RID: 7840
		internal const string ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity = "ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity";

		// Token: 0x04001EA1 RID: 7841
		internal const string ObjectStateManager_CannotChangeRelationshipStateEntityDeleted = "ObjectStateManager_CannotChangeRelationshipStateEntityDeleted";

		// Token: 0x04001EA2 RID: 7842
		internal const string ObjectStateManager_CannotChangeRelationshipStateEntityAdded = "ObjectStateManager_CannotChangeRelationshipStateEntityAdded";

		// Token: 0x04001EA3 RID: 7843
		internal const string ObjectStateManager_CannotChangeRelationshipStateKeyEntry = "ObjectStateManager_CannotChangeRelationshipStateKeyEntry";

		// Token: 0x04001EA4 RID: 7844
		internal const string ObjectStateManager_ConflictingChangesOfRelationshipDetected = "ObjectStateManager_ConflictingChangesOfRelationshipDetected";

		// Token: 0x04001EA5 RID: 7845
		internal const string ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations = "ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations";

		// Token: 0x04001EA6 RID: 7846
		internal const string ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid = "ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid";

		// Token: 0x04001EA7 RID: 7847
		internal const string ObjectContext_ClientEntityRemovedFromStore = "ObjectContext_ClientEntityRemovedFromStore";

		// Token: 0x04001EA8 RID: 7848
		internal const string ObjectContext_StoreEntityNotPresentInClient = "ObjectContext_StoreEntityNotPresentInClient";

		// Token: 0x04001EA9 RID: 7849
		internal const string ObjectContext_InvalidConnectionString = "ObjectContext_InvalidConnectionString";

		// Token: 0x04001EAA RID: 7850
		internal const string ObjectContext_InvalidConnection = "ObjectContext_InvalidConnection";

		// Token: 0x04001EAB RID: 7851
		internal const string ObjectContext_InvalidDefaultContainerName = "ObjectContext_InvalidDefaultContainerName";

		// Token: 0x04001EAC RID: 7852
		internal const string ObjectContext_NthElementInAddedState = "ObjectContext_NthElementInAddedState";

		// Token: 0x04001EAD RID: 7853
		internal const string ObjectContext_NthElementIsDuplicate = "ObjectContext_NthElementIsDuplicate";

		// Token: 0x04001EAE RID: 7854
		internal const string ObjectContext_NthElementIsNull = "ObjectContext_NthElementIsNull";

		// Token: 0x04001EAF RID: 7855
		internal const string ObjectContext_NthElementNotInObjectStateManager = "ObjectContext_NthElementNotInObjectStateManager";

		// Token: 0x04001EB0 RID: 7856
		internal const string ObjectContext_ObjectNotFound = "ObjectContext_ObjectNotFound";

		// Token: 0x04001EB1 RID: 7857
		internal const string ObjectContext_CannotDeleteEntityNotInObjectStateManager = "ObjectContext_CannotDeleteEntityNotInObjectStateManager";

		// Token: 0x04001EB2 RID: 7858
		internal const string ObjectContext_CannotDetachEntityNotInObjectStateManager = "ObjectContext_CannotDetachEntityNotInObjectStateManager";

		// Token: 0x04001EB3 RID: 7859
		internal const string ObjectContext_EntitySetNotFoundForName = "ObjectContext_EntitySetNotFoundForName";

		// Token: 0x04001EB4 RID: 7860
		internal const string ObjectContext_EntityContainerNotFoundForName = "ObjectContext_EntityContainerNotFoundForName";

		// Token: 0x04001EB5 RID: 7861
		internal const string ObjectContext_InvalidCommandTimeout = "ObjectContext_InvalidCommandTimeout";

		// Token: 0x04001EB6 RID: 7862
		internal const string ObjectContext_NoMappingForEntityType = "ObjectContext_NoMappingForEntityType";

		// Token: 0x04001EB7 RID: 7863
		internal const string ObjectContext_EntityAlreadyExistsInObjectStateManager = "ObjectContext_EntityAlreadyExistsInObjectStateManager";

		// Token: 0x04001EB8 RID: 7864
		internal const string ObjectContext_InvalidEntitySetInKey = "ObjectContext_InvalidEntitySetInKey";

		// Token: 0x04001EB9 RID: 7865
		internal const string ObjectContext_CannotAttachEntityWithoutKey = "ObjectContext_CannotAttachEntityWithoutKey";

		// Token: 0x04001EBA RID: 7866
		internal const string ObjectContext_CannotAttachEntityWithTemporaryKey = "ObjectContext_CannotAttachEntityWithTemporaryKey";

		// Token: 0x04001EBB RID: 7867
		internal const string ObjectContext_EntitySetNameOrEntityKeyRequired = "ObjectContext_EntitySetNameOrEntityKeyRequired";

		// Token: 0x04001EBC RID: 7868
		internal const string ObjectContext_ExecuteFunctionTypeMismatch = "ObjectContext_ExecuteFunctionTypeMismatch";

		// Token: 0x04001EBD RID: 7869
		internal const string ObjectContext_ExecuteFunctionCalledWithScalarFunction = "ObjectContext_ExecuteFunctionCalledWithScalarFunction";

		// Token: 0x04001EBE RID: 7870
		internal const string ObjectContext_ExecuteFunctionCalledWithNonQueryFunction = "ObjectContext_ExecuteFunctionCalledWithNonQueryFunction";

		// Token: 0x04001EBF RID: 7871
		internal const string ObjectContext_ExecuteFunctionCalledWithNullParameter = "ObjectContext_ExecuteFunctionCalledWithNullParameter";

		// Token: 0x04001EC0 RID: 7872
		internal const string ObjectContext_ContainerQualifiedEntitySetNameRequired = "ObjectContext_ContainerQualifiedEntitySetNameRequired";

		// Token: 0x04001EC1 RID: 7873
		internal const string ObjectContext_CannotSetDefaultContainerName = "ObjectContext_CannotSetDefaultContainerName";

		// Token: 0x04001EC2 RID: 7874
		internal const string ObjectContext_QualfiedEntitySetName = "ObjectContext_QualfiedEntitySetName";

		// Token: 0x04001EC3 RID: 7875
		internal const string ObjectContext_EntitiesHaveDifferentType = "ObjectContext_EntitiesHaveDifferentType";

		// Token: 0x04001EC4 RID: 7876
		internal const string ObjectContext_EntityMustBeUnchangedOrModified = "ObjectContext_EntityMustBeUnchangedOrModified";

		// Token: 0x04001EC5 RID: 7877
		internal const string ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted = "ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted";

		// Token: 0x04001EC6 RID: 7878
		internal const string ObjectContext_AcceptAllChangesFailure = "ObjectContext_AcceptAllChangesFailure";

		// Token: 0x04001EC7 RID: 7879
		internal const string ObjectContext_CommitWithConceptualNull = "ObjectContext_CommitWithConceptualNull";

		// Token: 0x04001EC8 RID: 7880
		internal const string ObjectContext_InvalidEntitySetOnEntity = "ObjectContext_InvalidEntitySetOnEntity";

		// Token: 0x04001EC9 RID: 7881
		internal const string ObjectContext_InvalidObjectSetTypeForEntitySet = "ObjectContext_InvalidObjectSetTypeForEntitySet";

		// Token: 0x04001ECA RID: 7882
		internal const string ObjectContext_InvalidEntitySetInKeyFromName = "ObjectContext_InvalidEntitySetInKeyFromName";

		// Token: 0x04001ECB RID: 7883
		internal const string ObjectContext_ObjectDisposed = "ObjectContext_ObjectDisposed";

		// Token: 0x04001ECC RID: 7884
		internal const string ObjectContext_CannotExplicitlyLoadDetachedRelationships = "ObjectContext_CannotExplicitlyLoadDetachedRelationships";

		// Token: 0x04001ECD RID: 7885
		internal const string ObjectContext_CannotLoadReferencesUsingDifferentContext = "ObjectContext_CannotLoadReferencesUsingDifferentContext";

		// Token: 0x04001ECE RID: 7886
		internal const string ObjectContext_SelectorExpressionMustBeMemberAccess = "ObjectContext_SelectorExpressionMustBeMemberAccess";

		// Token: 0x04001ECF RID: 7887
		internal const string ObjectContext_MultipleEntitySetsFoundInSingleContainer = "ObjectContext_MultipleEntitySetsFoundInSingleContainer";

		// Token: 0x04001ED0 RID: 7888
		internal const string ObjectContext_MultipleEntitySetsFoundInAllContainers = "ObjectContext_MultipleEntitySetsFoundInAllContainers";

		// Token: 0x04001ED1 RID: 7889
		internal const string ObjectContext_NoEntitySetFoundForType = "ObjectContext_NoEntitySetFoundForType";

		// Token: 0x04001ED2 RID: 7890
		internal const string ObjectContext_EntityNotInObjectSet_Delete = "ObjectContext_EntityNotInObjectSet_Delete";

		// Token: 0x04001ED3 RID: 7891
		internal const string ObjectContext_EntityNotInObjectSet_Detach = "ObjectContext_EntityNotInObjectSet_Detach";

		// Token: 0x04001ED4 RID: 7892
		internal const string ObjectContext_InvalidEntityState = "ObjectContext_InvalidEntityState";

		// Token: 0x04001ED5 RID: 7893
		internal const string ObjectContext_InvalidRelationshipState = "ObjectContext_InvalidRelationshipState";

		// Token: 0x04001ED6 RID: 7894
		internal const string ObjectContext_EntityNotTrackedOrHasTempKey = "ObjectContext_EntityNotTrackedOrHasTempKey";

		// Token: 0x04001ED7 RID: 7895
		internal const string ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues = "ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues";

		// Token: 0x04001ED8 RID: 7896
		internal const string ObjectContext_InvalidEntitySetForStoreQuery = "ObjectContext_InvalidEntitySetForStoreQuery";

		// Token: 0x04001ED9 RID: 7897
		internal const string ObjectContext_InvalidTypeForStoreQuery = "ObjectContext_InvalidTypeForStoreQuery";

		// Token: 0x04001EDA RID: 7898
		internal const string ObjectContext_TwoPropertiesMappedToSameColumn = "ObjectContext_TwoPropertiesMappedToSameColumn";

		// Token: 0x04001EDB RID: 7899
		internal const string RelatedEnd_InvalidOwnerStateForAttach = "RelatedEnd_InvalidOwnerStateForAttach";

		// Token: 0x04001EDC RID: 7900
		internal const string RelatedEnd_InvalidNthElementNullForAttach = "RelatedEnd_InvalidNthElementNullForAttach";

		// Token: 0x04001EDD RID: 7901
		internal const string RelatedEnd_InvalidNthElementContextForAttach = "RelatedEnd_InvalidNthElementContextForAttach";

		// Token: 0x04001EDE RID: 7902
		internal const string RelatedEnd_InvalidNthElementStateForAttach = "RelatedEnd_InvalidNthElementStateForAttach";

		// Token: 0x04001EDF RID: 7903
		internal const string RelatedEnd_InvalidEntityContextForAttach = "RelatedEnd_InvalidEntityContextForAttach";

		// Token: 0x04001EE0 RID: 7904
		internal const string RelatedEnd_InvalidEntityStateForAttach = "RelatedEnd_InvalidEntityStateForAttach";

		// Token: 0x04001EE1 RID: 7905
		internal const string RelatedEnd_UnableToAddEntity = "RelatedEnd_UnableToAddEntity";

		// Token: 0x04001EE2 RID: 7906
		internal const string RelatedEnd_UnableToRemoveEntity = "RelatedEnd_UnableToRemoveEntity";

		// Token: 0x04001EE3 RID: 7907
		internal const string RelatedEnd_UnableToAddRelationshipWithDeletedEntity = "RelatedEnd_UnableToAddRelationshipWithDeletedEntity";

		// Token: 0x04001EE4 RID: 7908
		internal const string RelatedEnd_CannotSerialize = "RelatedEnd_CannotSerialize";

		// Token: 0x04001EE5 RID: 7909
		internal const string RelatedEnd_CannotAddToFixedSizeArray = "RelatedEnd_CannotAddToFixedSizeArray";

		// Token: 0x04001EE6 RID: 7910
		internal const string RelatedEnd_CannotRemoveFromFixedSizeArray = "RelatedEnd_CannotRemoveFromFixedSizeArray";

		// Token: 0x04001EE7 RID: 7911
		internal const string Materializer_PropertyIsNotNullable = "Materializer_PropertyIsNotNullable";

		// Token: 0x04001EE8 RID: 7912
		internal const string Materializer_PropertyIsNotNullableWithName = "Materializer_PropertyIsNotNullableWithName";

		// Token: 0x04001EE9 RID: 7913
		internal const string Materializer_SetInvalidValue = "Materializer_SetInvalidValue";

		// Token: 0x04001EEA RID: 7914
		internal const string Materializer_InvalidCastReference = "Materializer_InvalidCastReference";

		// Token: 0x04001EEB RID: 7915
		internal const string Materializer_InvalidCastNullable = "Materializer_InvalidCastNullable";

		// Token: 0x04001EEC RID: 7916
		internal const string Materializer_NullReferenceCast = "Materializer_NullReferenceCast";

		// Token: 0x04001EED RID: 7917
		internal const string Materializer_RecyclingEntity = "Materializer_RecyclingEntity";

		// Token: 0x04001EEE RID: 7918
		internal const string Materializer_AddedEntityAlreadyExists = "Materializer_AddedEntityAlreadyExists";

		// Token: 0x04001EEF RID: 7919
		internal const string Materializer_CannotReEnumerateQueryResults = "Materializer_CannotReEnumerateQueryResults";

		// Token: 0x04001EF0 RID: 7920
		internal const string Materializer_UnsupportedType = "Materializer_UnsupportedType";

		// Token: 0x04001EF1 RID: 7921
		internal const string Collections_NoRelationshipSetMatched = "Collections_NoRelationshipSetMatched";

		// Token: 0x04001EF2 RID: 7922
		internal const string Collections_ExpectedCollectionGotReference = "Collections_ExpectedCollectionGotReference";

		// Token: 0x04001EF3 RID: 7923
		internal const string Collections_InvalidEntityStateSource = "Collections_InvalidEntityStateSource";

		// Token: 0x04001EF4 RID: 7924
		internal const string Collections_InvalidEntityStateLoad = "Collections_InvalidEntityStateLoad";

		// Token: 0x04001EF5 RID: 7925
		internal const string Collections_CannotFillTryDifferentMergeOption = "Collections_CannotFillTryDifferentMergeOption";

		// Token: 0x04001EF6 RID: 7926
		internal const string Collections_UnableToMergeCollections = "Collections_UnableToMergeCollections";

		// Token: 0x04001EF7 RID: 7927
		internal const string EntityReference_ExpectedReferenceGotCollection = "EntityReference_ExpectedReferenceGotCollection";

		// Token: 0x04001EF8 RID: 7928
		internal const string EntityReference_CannotAddMoreThanOneEntityToEntityReference = "EntityReference_CannotAddMoreThanOneEntityToEntityReference";

		// Token: 0x04001EF9 RID: 7929
		internal const string EntityReference_LessThanExpectedRelatedEntitiesFound = "EntityReference_LessThanExpectedRelatedEntitiesFound";

		// Token: 0x04001EFA RID: 7930
		internal const string EntityReference_MoreThanExpectedRelatedEntitiesFound = "EntityReference_MoreThanExpectedRelatedEntitiesFound";

		// Token: 0x04001EFB RID: 7931
		internal const string EntityReference_CannotChangeReferentialConstraintProperty = "EntityReference_CannotChangeReferentialConstraintProperty";

		// Token: 0x04001EFC RID: 7932
		internal const string EntityReference_CannotSetSpecialKeys = "EntityReference_CannotSetSpecialKeys";

		// Token: 0x04001EFD RID: 7933
		internal const string EntityReference_EntityKeyValueMismatch = "EntityReference_EntityKeyValueMismatch";

		// Token: 0x04001EFE RID: 7934
		internal const string RelatedEnd_RelatedEndNotFound = "RelatedEnd_RelatedEndNotFound";

		// Token: 0x04001EFF RID: 7935
		internal const string RelatedEnd_RelatedEndNotAttachedToContext = "RelatedEnd_RelatedEndNotAttachedToContext";

		// Token: 0x04001F00 RID: 7936
		internal const string RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd = "RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd";

		// Token: 0x04001F01 RID: 7937
		internal const string RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd = "RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd";

		// Token: 0x04001F02 RID: 7938
		internal const string RelatedEnd_InvalidContainedType_Collection = "RelatedEnd_InvalidContainedType_Collection";

		// Token: 0x04001F03 RID: 7939
		internal const string RelatedEnd_InvalidContainedType_Reference = "RelatedEnd_InvalidContainedType_Reference";

		// Token: 0x04001F04 RID: 7940
		internal const string RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities = "RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities";

		// Token: 0x04001F05 RID: 7941
		internal const string RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts = "RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts";

		// Token: 0x04001F06 RID: 7942
		internal const string RelatedEnd_MismatchedMergeOptionOnLoad = "RelatedEnd_MismatchedMergeOptionOnLoad";

		// Token: 0x04001F07 RID: 7943
		internal const string RelatedEnd_EntitySetIsNotValidForRelationship = "RelatedEnd_EntitySetIsNotValidForRelationship";

		// Token: 0x04001F08 RID: 7944
		internal const string RelatedEnd_OwnerIsNull = "RelatedEnd_OwnerIsNull";

		// Token: 0x04001F09 RID: 7945
		internal const string RelationshipManager_UnableToRetrieveReferentialConstraintProperties = "RelationshipManager_UnableToRetrieveReferentialConstraintProperties";

		// Token: 0x04001F0A RID: 7946
		internal const string RelationshipManager_InconsistentReferentialConstraintProperties = "RelationshipManager_InconsistentReferentialConstraintProperties";

		// Token: 0x04001F0B RID: 7947
		internal const string RelationshipManager_CircularRelationshipsWithReferentialConstraints = "RelationshipManager_CircularRelationshipsWithReferentialConstraints";

		// Token: 0x04001F0C RID: 7948
		internal const string RelationshipManager_UnableToFindRelationshipTypeInMetadata = "RelationshipManager_UnableToFindRelationshipTypeInMetadata";

		// Token: 0x04001F0D RID: 7949
		internal const string RelationshipManager_InvalidTargetRole = "RelationshipManager_InvalidTargetRole";

		// Token: 0x04001F0E RID: 7950
		internal const string RelationshipManager_UnexpectedNull = "RelationshipManager_UnexpectedNull";

		// Token: 0x04001F0F RID: 7951
		internal const string RelationshipManager_InvalidRelationshipManagerOwner = "RelationshipManager_InvalidRelationshipManagerOwner";

		// Token: 0x04001F10 RID: 7952
		internal const string RelationshipManager_OwnerIsNotSourceType = "RelationshipManager_OwnerIsNotSourceType";

		// Token: 0x04001F11 RID: 7953
		internal const string RelationshipManager_UnexpectedNullContext = "RelationshipManager_UnexpectedNullContext";

		// Token: 0x04001F12 RID: 7954
		internal const string RelationshipManager_ReferenceAlreadyInitialized = "RelationshipManager_ReferenceAlreadyInitialized";

		// Token: 0x04001F13 RID: 7955
		internal const string RelationshipManager_RelationshipManagerAttached = "RelationshipManager_RelationshipManagerAttached";

		// Token: 0x04001F14 RID: 7956
		internal const string RelationshipManager_InitializeIsForDeserialization = "RelationshipManager_InitializeIsForDeserialization";

		// Token: 0x04001F15 RID: 7957
		internal const string RelationshipManager_CollectionAlreadyInitialized = "RelationshipManager_CollectionAlreadyInitialized";

		// Token: 0x04001F16 RID: 7958
		internal const string RelationshipManager_CollectionRelationshipManagerAttached = "RelationshipManager_CollectionRelationshipManagerAttached";

		// Token: 0x04001F17 RID: 7959
		internal const string RelationshipManager_CollectionInitializeIsForDeserialization = "RelationshipManager_CollectionInitializeIsForDeserialization";

		// Token: 0x04001F18 RID: 7960
		internal const string RelationshipManager_NavigationPropertyNotFound = "RelationshipManager_NavigationPropertyNotFound";

		// Token: 0x04001F19 RID: 7961
		internal const string RelationshipManager_CannotGetRelatEndForDetachedPocoEntity = "RelationshipManager_CannotGetRelatEndForDetachedPocoEntity";

		// Token: 0x04001F1A RID: 7962
		internal const string ObjectView_CannotReplacetheEntityorRow = "ObjectView_CannotReplacetheEntityorRow";

		// Token: 0x04001F1B RID: 7963
		internal const string ObjectView_IndexBasedInsertIsNotSupported = "ObjectView_IndexBasedInsertIsNotSupported";

		// Token: 0x04001F1C RID: 7964
		internal const string ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList = "ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList";

		// Token: 0x04001F1D RID: 7965
		internal const string ObjectView_AddNewOperationNotAllowedOnAbstractBindingList = "ObjectView_AddNewOperationNotAllowedOnAbstractBindingList";

		// Token: 0x04001F1E RID: 7966
		internal const string ObjectView_IncompatibleArgument = "ObjectView_IncompatibleArgument";

		// Token: 0x04001F1F RID: 7967
		internal const string ObjectView_CannotResolveTheEntitySet = "ObjectView_CannotResolveTheEntitySet";

		// Token: 0x04001F20 RID: 7968
		internal const string CodeGen_ConstructorNoParameterless = "CodeGen_ConstructorNoParameterless";

		// Token: 0x04001F21 RID: 7969
		internal const string CodeGen_PropertyDeclaringTypeIsValueType = "CodeGen_PropertyDeclaringTypeIsValueType";

		// Token: 0x04001F22 RID: 7970
		internal const string CodeGen_PropertyUnsupportedType = "CodeGen_PropertyUnsupportedType";

		// Token: 0x04001F23 RID: 7971
		internal const string CodeGen_PropertyIsIndexed = "CodeGen_PropertyIsIndexed";

		// Token: 0x04001F24 RID: 7972
		internal const string CodeGen_PropertyIsStatic = "CodeGen_PropertyIsStatic";

		// Token: 0x04001F25 RID: 7973
		internal const string CodeGen_PropertyNoGetter = "CodeGen_PropertyNoGetter";

		// Token: 0x04001F26 RID: 7974
		internal const string CodeGen_PropertyNoSetter = "CodeGen_PropertyNoSetter";

		// Token: 0x04001F27 RID: 7975
		internal const string PocoEntityWrapper_UnableToSetFieldOrProperty = "PocoEntityWrapper_UnableToSetFieldOrProperty";

		// Token: 0x04001F28 RID: 7976
		internal const string PocoEntityWrapper_UnexpectedTypeForNavigationProperty = "PocoEntityWrapper_UnexpectedTypeForNavigationProperty";

		// Token: 0x04001F29 RID: 7977
		internal const string PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType = "PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType";

		// Token: 0x04001F2A RID: 7978
		internal const string GeneralQueryError = "GeneralQueryError";

		// Token: 0x04001F2B RID: 7979
		internal const string CtxAlias = "CtxAlias";

		// Token: 0x04001F2C RID: 7980
		internal const string CtxAliasedNamespaceImport = "CtxAliasedNamespaceImport";

		// Token: 0x04001F2D RID: 7981
		internal const string CtxAnd = "CtxAnd";

		// Token: 0x04001F2E RID: 7982
		internal const string CtxAnyElement = "CtxAnyElement";

		// Token: 0x04001F2F RID: 7983
		internal const string CtxApplyClause = "CtxApplyClause";

		// Token: 0x04001F30 RID: 7984
		internal const string CtxBetween = "CtxBetween";

		// Token: 0x04001F31 RID: 7985
		internal const string CtxCase = "CtxCase";

		// Token: 0x04001F32 RID: 7986
		internal const string CtxCaseElse = "CtxCaseElse";

		// Token: 0x04001F33 RID: 7987
		internal const string CtxCaseWhenThen = "CtxCaseWhenThen";

		// Token: 0x04001F34 RID: 7988
		internal const string CtxCast = "CtxCast";

		// Token: 0x04001F35 RID: 7989
		internal const string CtxCollatedOrderByClauseItem = "CtxCollatedOrderByClauseItem";

		// Token: 0x04001F36 RID: 7990
		internal const string CtxCollectionTypeDefinition = "CtxCollectionTypeDefinition";

		// Token: 0x04001F37 RID: 7991
		internal const string CtxCommandExpression = "CtxCommandExpression";

		// Token: 0x04001F38 RID: 7992
		internal const string CtxCreateRef = "CtxCreateRef";

		// Token: 0x04001F39 RID: 7993
		internal const string CtxDeref = "CtxDeref";

		// Token: 0x04001F3A RID: 7994
		internal const string CtxDivide = "CtxDivide";

		// Token: 0x04001F3B RID: 7995
		internal const string CtxElement = "CtxElement";

		// Token: 0x04001F3C RID: 7996
		internal const string CtxEquals = "CtxEquals";

		// Token: 0x04001F3D RID: 7997
		internal const string CtxEscapedIdentifier = "CtxEscapedIdentifier";

		// Token: 0x04001F3E RID: 7998
		internal const string CtxExcept = "CtxExcept";

		// Token: 0x04001F3F RID: 7999
		internal const string CtxExists = "CtxExists";

		// Token: 0x04001F40 RID: 8000
		internal const string CtxExpressionList = "CtxExpressionList";

		// Token: 0x04001F41 RID: 8001
		internal const string CtxFlatten = "CtxFlatten";

		// Token: 0x04001F42 RID: 8002
		internal const string CtxFromApplyClause = "CtxFromApplyClause";

		// Token: 0x04001F43 RID: 8003
		internal const string CtxFromClause = "CtxFromClause";

		// Token: 0x04001F44 RID: 8004
		internal const string CtxFromClauseItem = "CtxFromClauseItem";

		// Token: 0x04001F45 RID: 8005
		internal const string CtxFromClauseList = "CtxFromClauseList";

		// Token: 0x04001F46 RID: 8006
		internal const string CtxFromJoinClause = "CtxFromJoinClause";

		// Token: 0x04001F47 RID: 8007
		internal const string CtxFunction = "CtxFunction";

		// Token: 0x04001F48 RID: 8008
		internal const string CtxFunctionDefinition = "CtxFunctionDefinition";

		// Token: 0x04001F49 RID: 8009
		internal const string CtxGreaterThan = "CtxGreaterThan";

		// Token: 0x04001F4A RID: 8010
		internal const string CtxGreaterThanEqual = "CtxGreaterThanEqual";

		// Token: 0x04001F4B RID: 8011
		internal const string CtxGroupByClause = "CtxGroupByClause";

		// Token: 0x04001F4C RID: 8012
		internal const string CtxGroupPartition = "CtxGroupPartition";

		// Token: 0x04001F4D RID: 8013
		internal const string CtxHavingClause = "CtxHavingClause";

		// Token: 0x04001F4E RID: 8014
		internal const string CtxIdentifier = "CtxIdentifier";

		// Token: 0x04001F4F RID: 8015
		internal const string CtxIn = "CtxIn";

		// Token: 0x04001F50 RID: 8016
		internal const string CtxIntersect = "CtxIntersect";

		// Token: 0x04001F51 RID: 8017
		internal const string CtxIsNotNull = "CtxIsNotNull";

		// Token: 0x04001F52 RID: 8018
		internal const string CtxIsNotOf = "CtxIsNotOf";

		// Token: 0x04001F53 RID: 8019
		internal const string CtxIsNull = "CtxIsNull";

		// Token: 0x04001F54 RID: 8020
		internal const string CtxIsOf = "CtxIsOf";

		// Token: 0x04001F55 RID: 8021
		internal const string CtxJoinClause = "CtxJoinClause";

		// Token: 0x04001F56 RID: 8022
		internal const string CtxJoinOnClause = "CtxJoinOnClause";

		// Token: 0x04001F57 RID: 8023
		internal const string CtxKey = "CtxKey";

		// Token: 0x04001F58 RID: 8024
		internal const string CtxLessThan = "CtxLessThan";

		// Token: 0x04001F59 RID: 8025
		internal const string CtxLessThanEqual = "CtxLessThanEqual";

		// Token: 0x04001F5A RID: 8026
		internal const string CtxLike = "CtxLike";

		// Token: 0x04001F5B RID: 8027
		internal const string CtxLimitSubClause = "CtxLimitSubClause";

		// Token: 0x04001F5C RID: 8028
		internal const string CtxLiteral = "CtxLiteral";

		// Token: 0x04001F5D RID: 8029
		internal const string CtxMemberAccess = "CtxMemberAccess";

		// Token: 0x04001F5E RID: 8030
		internal const string CtxMethod = "CtxMethod";

		// Token: 0x04001F5F RID: 8031
		internal const string CtxMinus = "CtxMinus";

		// Token: 0x04001F60 RID: 8032
		internal const string CtxModulus = "CtxModulus";

		// Token: 0x04001F61 RID: 8033
		internal const string CtxMultiply = "CtxMultiply";

		// Token: 0x04001F62 RID: 8034
		internal const string CtxMultisetCtor = "CtxMultisetCtor";

		// Token: 0x04001F63 RID: 8035
		internal const string CtxNamespaceImport = "CtxNamespaceImport";

		// Token: 0x04001F64 RID: 8036
		internal const string CtxNamespaceImportList = "CtxNamespaceImportList";

		// Token: 0x04001F65 RID: 8037
		internal const string CtxNavigate = "CtxNavigate";

		// Token: 0x04001F66 RID: 8038
		internal const string CtxNot = "CtxNot";

		// Token: 0x04001F67 RID: 8039
		internal const string CtxNotBetween = "CtxNotBetween";

		// Token: 0x04001F68 RID: 8040
		internal const string CtxNotEqual = "CtxNotEqual";

		// Token: 0x04001F69 RID: 8041
		internal const string CtxNotIn = "CtxNotIn";

		// Token: 0x04001F6A RID: 8042
		internal const string CtxNotLike = "CtxNotLike";

		// Token: 0x04001F6B RID: 8043
		internal const string CtxNullLiteral = "CtxNullLiteral";

		// Token: 0x04001F6C RID: 8044
		internal const string CtxOfType = "CtxOfType";

		// Token: 0x04001F6D RID: 8045
		internal const string CtxOfTypeOnly = "CtxOfTypeOnly";

		// Token: 0x04001F6E RID: 8046
		internal const string CtxOr = "CtxOr";

		// Token: 0x04001F6F RID: 8047
		internal const string CtxOrderByClause = "CtxOrderByClause";

		// Token: 0x04001F70 RID: 8048
		internal const string CtxOrderByClauseItem = "CtxOrderByClauseItem";

		// Token: 0x04001F71 RID: 8049
		internal const string CtxOverlaps = "CtxOverlaps";

		// Token: 0x04001F72 RID: 8050
		internal const string CtxParen = "CtxParen";

		// Token: 0x04001F73 RID: 8051
		internal const string CtxPlus = "CtxPlus";

		// Token: 0x04001F74 RID: 8052
		internal const string CtxTypeNameWithTypeSpec = "CtxTypeNameWithTypeSpec";

		// Token: 0x04001F75 RID: 8053
		internal const string CtxQueryExpression = "CtxQueryExpression";

		// Token: 0x04001F76 RID: 8054
		internal const string CtxQueryStatement = "CtxQueryStatement";

		// Token: 0x04001F77 RID: 8055
		internal const string CtxRef = "CtxRef";

		// Token: 0x04001F78 RID: 8056
		internal const string CtxRefTypeDefinition = "CtxRefTypeDefinition";

		// Token: 0x04001F79 RID: 8057
		internal const string CtxRelationship = "CtxRelationship";

		// Token: 0x04001F7A RID: 8058
		internal const string CtxRelationshipList = "CtxRelationshipList";

		// Token: 0x04001F7B RID: 8059
		internal const string CtxRowCtor = "CtxRowCtor";

		// Token: 0x04001F7C RID: 8060
		internal const string CtxRowTypeDefinition = "CtxRowTypeDefinition";

		// Token: 0x04001F7D RID: 8061
		internal const string CtxSelectRowClause = "CtxSelectRowClause";

		// Token: 0x04001F7E RID: 8062
		internal const string CtxSelectValueClause = "CtxSelectValueClause";

		// Token: 0x04001F7F RID: 8063
		internal const string CtxSet = "CtxSet";

		// Token: 0x04001F80 RID: 8064
		internal const string CtxSimpleIdentifier = "CtxSimpleIdentifier";

		// Token: 0x04001F81 RID: 8065
		internal const string CtxSkipSubClause = "CtxSkipSubClause";

		// Token: 0x04001F82 RID: 8066
		internal const string CtxTopSubClause = "CtxTopSubClause";

		// Token: 0x04001F83 RID: 8067
		internal const string CtxTreat = "CtxTreat";

		// Token: 0x04001F84 RID: 8068
		internal const string CtxTypeCtor = "CtxTypeCtor";

		// Token: 0x04001F85 RID: 8069
		internal const string CtxTypeName = "CtxTypeName";

		// Token: 0x04001F86 RID: 8070
		internal const string CtxUnaryMinus = "CtxUnaryMinus";

		// Token: 0x04001F87 RID: 8071
		internal const string CtxUnaryPlus = "CtxUnaryPlus";

		// Token: 0x04001F88 RID: 8072
		internal const string CtxUnion = "CtxUnion";

		// Token: 0x04001F89 RID: 8073
		internal const string CtxUnionAll = "CtxUnionAll";

		// Token: 0x04001F8A RID: 8074
		internal const string CtxWhereClause = "CtxWhereClause";

		// Token: 0x04001F8B RID: 8075
		internal const string CannotConvertNumericLiteral = "CannotConvertNumericLiteral";

		// Token: 0x04001F8C RID: 8076
		internal const string GenericSyntaxError = "GenericSyntaxError";

		// Token: 0x04001F8D RID: 8077
		internal const string InFromClause = "InFromClause";

		// Token: 0x04001F8E RID: 8078
		internal const string InGroupClause = "InGroupClause";

		// Token: 0x04001F8F RID: 8079
		internal const string InRowCtor = "InRowCtor";

		// Token: 0x04001F90 RID: 8080
		internal const string InSelectProjectionList = "InSelectProjectionList";

		// Token: 0x04001F91 RID: 8081
		internal const string InvalidAliasName = "InvalidAliasName";

		// Token: 0x04001F92 RID: 8082
		internal const string InvalidEmptyIdentifier = "InvalidEmptyIdentifier";

		// Token: 0x04001F93 RID: 8083
		internal const string InvalidEmptyQuery = "InvalidEmptyQuery";

		// Token: 0x04001F94 RID: 8084
		internal const string InvalidEscapedIdentifier = "InvalidEscapedIdentifier";

		// Token: 0x04001F95 RID: 8085
		internal const string InvalidEscapedIdentifierUnbalanced = "InvalidEscapedIdentifierUnbalanced";

		// Token: 0x04001F96 RID: 8086
		internal const string InvalidOperatorSymbol = "InvalidOperatorSymbol";

		// Token: 0x04001F97 RID: 8087
		internal const string InvalidPunctuatorSymbol = "InvalidPunctuatorSymbol";

		// Token: 0x04001F98 RID: 8088
		internal const string InvalidSimpleIdentifier = "InvalidSimpleIdentifier";

		// Token: 0x04001F99 RID: 8089
		internal const string InvalidSimpleIdentifierNonASCII = "InvalidSimpleIdentifierNonASCII";

		// Token: 0x04001F9A RID: 8090
		internal const string LocalizedCollection = "LocalizedCollection";

		// Token: 0x04001F9B RID: 8091
		internal const string LocalizedColumn = "LocalizedColumn";

		// Token: 0x04001F9C RID: 8092
		internal const string LocalizedComplex = "LocalizedComplex";

		// Token: 0x04001F9D RID: 8093
		internal const string LocalizedEntity = "LocalizedEntity";

		// Token: 0x04001F9E RID: 8094
		internal const string LocalizedEntityContainerExpression = "LocalizedEntityContainerExpression";

		// Token: 0x04001F9F RID: 8095
		internal const string LocalizedFunction = "LocalizedFunction";

		// Token: 0x04001FA0 RID: 8096
		internal const string LocalizedInlineFunction = "LocalizedInlineFunction";

		// Token: 0x04001FA1 RID: 8097
		internal const string LocalizedKeyword = "LocalizedKeyword";

		// Token: 0x04001FA2 RID: 8098
		internal const string LocalizedLeft = "LocalizedLeft";

		// Token: 0x04001FA3 RID: 8099
		internal const string LocalizedLine = "LocalizedLine";

		// Token: 0x04001FA4 RID: 8100
		internal const string LocalizedMetadataMemberExpression = "LocalizedMetadataMemberExpression";

		// Token: 0x04001FA5 RID: 8101
		internal const string LocalizedNamespace = "LocalizedNamespace";

		// Token: 0x04001FA6 RID: 8102
		internal const string LocalizedNear = "LocalizedNear";

		// Token: 0x04001FA7 RID: 8103
		internal const string LocalizedPrimitive = "LocalizedPrimitive";

		// Token: 0x04001FA8 RID: 8104
		internal const string LocalizedReference = "LocalizedReference";

		// Token: 0x04001FA9 RID: 8105
		internal const string LocalizedRight = "LocalizedRight";

		// Token: 0x04001FAA RID: 8106
		internal const string LocalizedRow = "LocalizedRow";

		// Token: 0x04001FAB RID: 8107
		internal const string LocalizedTerm = "LocalizedTerm";

		// Token: 0x04001FAC RID: 8108
		internal const string LocalizedType = "LocalizedType";

		// Token: 0x04001FAD RID: 8109
		internal const string LocalizedEnumMember = "LocalizedEnumMember";

		// Token: 0x04001FAE RID: 8110
		internal const string LocalizedValueExpression = "LocalizedValueExpression";

		// Token: 0x04001FAF RID: 8111
		internal const string AliasNameAlreadyUsed = "AliasNameAlreadyUsed";

		// Token: 0x04001FB0 RID: 8112
		internal const string AmbiguousFunctionArguments = "AmbiguousFunctionArguments";

		// Token: 0x04001FB1 RID: 8113
		internal const string AmbiguousMetadataMemberName = "AmbiguousMetadataMemberName";

		// Token: 0x04001FB2 RID: 8114
		internal const string ArgumentTypesAreIncompatible = "ArgumentTypesAreIncompatible";

		// Token: 0x04001FB3 RID: 8115
		internal const string BetweenLimitsCannotBeUntypedNulls = "BetweenLimitsCannotBeUntypedNulls";

		// Token: 0x04001FB4 RID: 8116
		internal const string BetweenLimitsTypesAreNotCompatible = "BetweenLimitsTypesAreNotCompatible";

		// Token: 0x04001FB5 RID: 8117
		internal const string BetweenLimitsTypesAreNotOrderComparable = "BetweenLimitsTypesAreNotOrderComparable";

		// Token: 0x04001FB6 RID: 8118
		internal const string BetweenValueIsNotOrderComparable = "BetweenValueIsNotOrderComparable";

		// Token: 0x04001FB7 RID: 8119
		internal const string CannotCreateEmptyMultiset = "CannotCreateEmptyMultiset";

		// Token: 0x04001FB8 RID: 8120
		internal const string CannotCreateMultisetofNulls = "CannotCreateMultisetofNulls";

		// Token: 0x04001FB9 RID: 8121
		internal const string CannotInstantiateAbstractType = "CannotInstantiateAbstractType";

		// Token: 0x04001FBA RID: 8122
		internal const string CannotResolveNameToTypeOrFunction = "CannotResolveNameToTypeOrFunction";

		// Token: 0x04001FBB RID: 8123
		internal const string ConcatBuiltinNotSupported = "ConcatBuiltinNotSupported";

		// Token: 0x04001FBC RID: 8124
		internal const string CouldNotResolveIdentifier = "CouldNotResolveIdentifier";

		// Token: 0x04001FBD RID: 8125
		internal const string CreateRefTypeIdentifierMustBeASubOrSuperType = "CreateRefTypeIdentifierMustBeASubOrSuperType";

		// Token: 0x04001FBE RID: 8126
		internal const string CreateRefTypeIdentifierMustSpecifyAnEntityType = "CreateRefTypeIdentifierMustSpecifyAnEntityType";

		// Token: 0x04001FBF RID: 8127
		internal const string DeRefArgIsNotOfRefType = "DeRefArgIsNotOfRefType";

		// Token: 0x04001FC0 RID: 8128
		internal const string DuplicatedInlineFunctionOverload = "DuplicatedInlineFunctionOverload";

		// Token: 0x04001FC1 RID: 8129
		internal const string ElementOperatorIsNotSupported = "ElementOperatorIsNotSupported";

		// Token: 0x04001FC2 RID: 8130
		internal const string MemberDoesNotBelongToEntityContainer = "MemberDoesNotBelongToEntityContainer";

		// Token: 0x04001FC3 RID: 8131
		internal const string ExpressionCannotBeNull = "ExpressionCannotBeNull";

		// Token: 0x04001FC4 RID: 8132
		internal const string OfTypeExpressionElementTypeMustBeEntityType = "OfTypeExpressionElementTypeMustBeEntityType";

		// Token: 0x04001FC5 RID: 8133
		internal const string OfTypeExpressionElementTypeMustBeNominalType = "OfTypeExpressionElementTypeMustBeNominalType";

		// Token: 0x04001FC6 RID: 8134
		internal const string ExpressionMustBeCollection = "ExpressionMustBeCollection";

		// Token: 0x04001FC7 RID: 8135
		internal const string ExpressionMustBeNumericType = "ExpressionMustBeNumericType";

		// Token: 0x04001FC8 RID: 8136
		internal const string ExpressionTypeMustBeBoolean = "ExpressionTypeMustBeBoolean";

		// Token: 0x04001FC9 RID: 8137
		internal const string ExpressionTypeMustBeEqualComparable = "ExpressionTypeMustBeEqualComparable";

		// Token: 0x04001FCA RID: 8138
		internal const string ExpressionTypeMustBeEntityType = "ExpressionTypeMustBeEntityType";

		// Token: 0x04001FCB RID: 8139
		internal const string ExpressionTypeMustBeNominalType = "ExpressionTypeMustBeNominalType";

		// Token: 0x04001FCC RID: 8140
		internal const string ExpressionTypeMustNotBeCollection = "ExpressionTypeMustNotBeCollection";

		// Token: 0x04001FCD RID: 8141
		internal const string ExprIsNotValidEntitySetForCreateRef = "ExprIsNotValidEntitySetForCreateRef";

		// Token: 0x04001FCE RID: 8142
		internal const string FailedToResolveAggregateFunction = "FailedToResolveAggregateFunction";

		// Token: 0x04001FCF RID: 8143
		internal const string GeneralExceptionAsQueryInnerException = "GeneralExceptionAsQueryInnerException";

		// Token: 0x04001FD0 RID: 8144
		internal const string GroupingKeysMustBeEqualComparable = "GroupingKeysMustBeEqualComparable";

		// Token: 0x04001FD1 RID: 8145
		internal const string GroupPartitionOutOfContext = "GroupPartitionOutOfContext";

		// Token: 0x04001FD2 RID: 8146
		internal const string HavingRequiresGroupClause = "HavingRequiresGroupClause";

		// Token: 0x04001FD3 RID: 8147
		internal const string ImcompatibleCreateRefKeyElementType = "ImcompatibleCreateRefKeyElementType";

		// Token: 0x04001FD4 RID: 8148
		internal const string ImcompatibleCreateRefKeyType = "ImcompatibleCreateRefKeyType";

		// Token: 0x04001FD5 RID: 8149
		internal const string InnerJoinMustHaveOnPredicate = "InnerJoinMustHaveOnPredicate";

		// Token: 0x04001FD6 RID: 8150
		internal const string InvalidAssociationTypeForUnion = "InvalidAssociationTypeForUnion";

		// Token: 0x04001FD7 RID: 8151
		internal const string InvalidCaseResultTypes = "InvalidCaseResultTypes";

		// Token: 0x04001FD8 RID: 8152
		internal const string InvalidCaseWhenThenNullType = "InvalidCaseWhenThenNullType";

		// Token: 0x04001FD9 RID: 8153
		internal const string InvalidCast = "InvalidCast";

		// Token: 0x04001FDA RID: 8154
		internal const string InvalidCastExpressionType = "InvalidCastExpressionType";

		// Token: 0x04001FDB RID: 8155
		internal const string InvalidCastType = "InvalidCastType";

		// Token: 0x04001FDC RID: 8156
		internal const string InvalidComplexType = "InvalidComplexType";

		// Token: 0x04001FDD RID: 8157
		internal const string InvalidCreateRefKeyType = "InvalidCreateRefKeyType";

		// Token: 0x04001FDE RID: 8158
		internal const string InvalidCtorArgumentType = "InvalidCtorArgumentType";

		// Token: 0x04001FDF RID: 8159
		internal const string InvalidCtorUseOnType = "InvalidCtorUseOnType";

		// Token: 0x04001FE0 RID: 8160
		internal const string InvalidDateTimeOffsetLiteral = "InvalidDateTimeOffsetLiteral";

		// Token: 0x04001FE1 RID: 8161
		internal const string InvalidDay = "InvalidDay";

		// Token: 0x04001FE2 RID: 8162
		internal const string InvalidDayInMonth = "InvalidDayInMonth";

		// Token: 0x04001FE3 RID: 8163
		internal const string InvalidDeRefProperty = "InvalidDeRefProperty";

		// Token: 0x04001FE4 RID: 8164
		internal const string InvalidDistinctArgumentInCtor = "InvalidDistinctArgumentInCtor";

		// Token: 0x04001FE5 RID: 8165
		internal const string InvalidDistinctArgumentInNonAggFunction = "InvalidDistinctArgumentInNonAggFunction";

		// Token: 0x04001FE6 RID: 8166
		internal const string InvalidEntityRootTypeArgument = "InvalidEntityRootTypeArgument";

		// Token: 0x04001FE7 RID: 8167
		internal const string InvalidEntityTypeArgument = "InvalidEntityTypeArgument";

		// Token: 0x04001FE8 RID: 8168
		internal const string InvalidExpressionResolutionClass = "InvalidExpressionResolutionClass";

		// Token: 0x04001FE9 RID: 8169
		internal const string InvalidFlattenArgument = "InvalidFlattenArgument";

		// Token: 0x04001FEA RID: 8170
		internal const string InvalidGroupIdentifierReference = "InvalidGroupIdentifierReference";

		// Token: 0x04001FEB RID: 8171
		internal const string InvalidHour = "InvalidHour";

		// Token: 0x04001FEC RID: 8172
		internal const string InvalidImplicitRelationshipFromEnd = "InvalidImplicitRelationshipFromEnd";

		// Token: 0x04001FED RID: 8173
		internal const string InvalidImplicitRelationshipToEnd = "InvalidImplicitRelationshipToEnd";

		// Token: 0x04001FEE RID: 8174
		internal const string InvalidInExprArgs = "InvalidInExprArgs";

		// Token: 0x04001FEF RID: 8175
		internal const string InvalidJoinLeftCorrelation = "InvalidJoinLeftCorrelation";

		// Token: 0x04001FF0 RID: 8176
		internal const string InvalidKeyArgument = "InvalidKeyArgument";

		// Token: 0x04001FF1 RID: 8177
		internal const string InvalidKeyTypeForCollation = "InvalidKeyTypeForCollation";

		// Token: 0x04001FF2 RID: 8178
		internal const string InvalidLiteralFormat = "InvalidLiteralFormat";

		// Token: 0x04001FF3 RID: 8179
		internal const string InvalidMetadataMemberName = "InvalidMetadataMemberName";

		// Token: 0x04001FF4 RID: 8180
		internal const string InvalidMinute = "InvalidMinute";

		// Token: 0x04001FF5 RID: 8181
		internal const string InvalidModeForWithRelationshipClause = "InvalidModeForWithRelationshipClause";

		// Token: 0x04001FF6 RID: 8182
		internal const string InvalidMonth = "InvalidMonth";

		// Token: 0x04001FF7 RID: 8183
		internal const string InvalidNamespaceAlias = "InvalidNamespaceAlias";

		// Token: 0x04001FF8 RID: 8184
		internal const string InvalidNullArithmetic = "InvalidNullArithmetic";

		// Token: 0x04001FF9 RID: 8185
		internal const string InvalidNullComparison = "InvalidNullComparison";

		// Token: 0x04001FFA RID: 8186
		internal const string InvalidNullLiteralForNonNullableMember = "InvalidNullLiteralForNonNullableMember";

		// Token: 0x04001FFB RID: 8187
		internal const string InvalidParameterFormat = "InvalidParameterFormat";

		// Token: 0x04001FFC RID: 8188
		internal const string InvalidPlaceholderRootTypeArgument = "InvalidPlaceholderRootTypeArgument";

		// Token: 0x04001FFD RID: 8189
		internal const string InvalidPlaceholderTypeArgument = "InvalidPlaceholderTypeArgument";

		// Token: 0x04001FFE RID: 8190
		internal const string InvalidPredicateForCrossJoin = "InvalidPredicateForCrossJoin";

		// Token: 0x04001FFF RID: 8191
		internal const string InvalidRelationshipMember = "InvalidRelationshipMember";

		// Token: 0x04002000 RID: 8192
		internal const string InvalidMetadataMemberClassResolution = "InvalidMetadataMemberClassResolution";

		// Token: 0x04002001 RID: 8193
		internal const string InvalidRootComplexType = "InvalidRootComplexType";

		// Token: 0x04002002 RID: 8194
		internal const string InvalidRootRowType = "InvalidRootRowType";

		// Token: 0x04002003 RID: 8195
		internal const string InvalidRowType = "InvalidRowType";

		// Token: 0x04002004 RID: 8196
		internal const string InvalidSecond = "InvalidSecond";

		// Token: 0x04002005 RID: 8197
		internal const string InvalidSelectValueAliasedExpression = "InvalidSelectValueAliasedExpression";

		// Token: 0x04002006 RID: 8198
		internal const string InvalidSelectValueList = "InvalidSelectValueList";

		// Token: 0x04002007 RID: 8199
		internal const string InvalidTypeForWithRelationshipClause = "InvalidTypeForWithRelationshipClause";

		// Token: 0x04002008 RID: 8200
		internal const string InvalidUnarySetOpArgument = "InvalidUnarySetOpArgument";

		// Token: 0x04002009 RID: 8201
		internal const string InvalidUnsignedTypeForUnaryMinusOperation = "InvalidUnsignedTypeForUnaryMinusOperation";

		// Token: 0x0400200A RID: 8202
		internal const string InvalidYear = "InvalidYear";

		// Token: 0x0400200B RID: 8203
		internal const string InvalidWithRelationshipTargetEndMultiplicity = "InvalidWithRelationshipTargetEndMultiplicity";

		// Token: 0x0400200C RID: 8204
		internal const string InvalidQueryResultType = "InvalidQueryResultType";

		// Token: 0x0400200D RID: 8205
		internal const string IsNullInvalidType = "IsNullInvalidType";

		// Token: 0x0400200E RID: 8206
		internal const string KeyMustBeCorrelated = "KeyMustBeCorrelated";

		// Token: 0x0400200F RID: 8207
		internal const string LeftSetExpressionArgsMustBeCollection = "LeftSetExpressionArgsMustBeCollection";

		// Token: 0x04002010 RID: 8208
		internal const string LikeArgMustBeStringType = "LikeArgMustBeStringType";

		// Token: 0x04002011 RID: 8209
		internal const string LiteralTypeNotFoundInMetadata = "LiteralTypeNotFoundInMetadata";

		// Token: 0x04002012 RID: 8210
		internal const string MalformedSingleQuotePayload = "MalformedSingleQuotePayload";

		// Token: 0x04002013 RID: 8211
		internal const string MalformedStringLiteralPayload = "MalformedStringLiteralPayload";

		// Token: 0x04002014 RID: 8212
		internal const string MethodInvocationNotSupported = "MethodInvocationNotSupported";

		// Token: 0x04002015 RID: 8213
		internal const string MultipleDefinitionsOfParameter = "MultipleDefinitionsOfParameter";

		// Token: 0x04002016 RID: 8214
		internal const string MultipleDefinitionsOfVariable = "MultipleDefinitionsOfVariable";

		// Token: 0x04002017 RID: 8215
		internal const string MultisetElemsAreNotTypeCompatible = "MultisetElemsAreNotTypeCompatible";

		// Token: 0x04002018 RID: 8216
		internal const string NamespaceAliasAlreadyUsed = "NamespaceAliasAlreadyUsed";

		// Token: 0x04002019 RID: 8217
		internal const string NamespaceAlreadyImported = "NamespaceAlreadyImported";

		// Token: 0x0400201A RID: 8218
		internal const string NestedAggregateCannotBeUsedInAggregate = "NestedAggregateCannotBeUsedInAggregate";

		// Token: 0x0400201B RID: 8219
		internal const string NoAggrFunctionOverloadMatch = "NoAggrFunctionOverloadMatch";

		// Token: 0x0400201C RID: 8220
		internal const string NoCanonicalAggrFunctionOverloadMatch = "NoCanonicalAggrFunctionOverloadMatch";

		// Token: 0x0400201D RID: 8221
		internal const string NoCanonicalFunctionOverloadMatch = "NoCanonicalFunctionOverloadMatch";

		// Token: 0x0400201E RID: 8222
		internal const string NoFunctionOverloadMatch = "NoFunctionOverloadMatch";

		// Token: 0x0400201F RID: 8223
		internal const string NotAMemberOfCollection = "NotAMemberOfCollection";

		// Token: 0x04002020 RID: 8224
		internal const string NotAMemberOfType = "NotAMemberOfType";

		// Token: 0x04002021 RID: 8225
		internal const string NotASuperOrSubType = "NotASuperOrSubType";

		// Token: 0x04002022 RID: 8226
		internal const string NullLiteralCannotBePromotedToCollectionOfNulls = "NullLiteralCannotBePromotedToCollectionOfNulls";

		// Token: 0x04002023 RID: 8227
		internal const string NumberOfTypeCtorIsLessThenFormalSpec = "NumberOfTypeCtorIsLessThenFormalSpec";

		// Token: 0x04002024 RID: 8228
		internal const string NumberOfTypeCtorIsMoreThenFormalSpec = "NumberOfTypeCtorIsMoreThenFormalSpec";

		// Token: 0x04002025 RID: 8229
		internal const string OrderByKeyIsNotOrderComparable = "OrderByKeyIsNotOrderComparable";

		// Token: 0x04002026 RID: 8230
		internal const string OfTypeOnlyTypeArgumentCannotBeAbstract = "OfTypeOnlyTypeArgumentCannotBeAbstract";

		// Token: 0x04002027 RID: 8231
		internal const string ParameterTypeNotSupported = "ParameterTypeNotSupported";

		// Token: 0x04002028 RID: 8232
		internal const string ParameterWasNotDefined = "ParameterWasNotDefined";

		// Token: 0x04002029 RID: 8233
		internal const string PlaceholderExpressionMustBeCompatibleWithEdm64 = "PlaceholderExpressionMustBeCompatibleWithEdm64";

		// Token: 0x0400202A RID: 8234
		internal const string PlaceholderExpressionMustBeConstant = "PlaceholderExpressionMustBeConstant";

		// Token: 0x0400202B RID: 8235
		internal const string PlaceholderExpressionMustBeGreaterThanOrEqualToZero = "PlaceholderExpressionMustBeGreaterThanOrEqualToZero";

		// Token: 0x0400202C RID: 8236
		internal const string PlaceholderSetArgTypeIsNotEqualComparable = "PlaceholderSetArgTypeIsNotEqualComparable";

		// Token: 0x0400202D RID: 8237
		internal const string PlusLeftExpressionInvalidType = "PlusLeftExpressionInvalidType";

		// Token: 0x0400202E RID: 8238
		internal const string PlusRightExpressionInvalidType = "PlusRightExpressionInvalidType";

		// Token: 0x0400202F RID: 8239
		internal const string PrecisionMustBeGreaterThanScale = "PrecisionMustBeGreaterThanScale";

		// Token: 0x04002030 RID: 8240
		internal const string RefArgIsNotOfEntityType = "RefArgIsNotOfEntityType";

		// Token: 0x04002031 RID: 8241
		internal const string RefTypeIdentifierMustSpecifyAnEntityType = "RefTypeIdentifierMustSpecifyAnEntityType";

		// Token: 0x04002032 RID: 8242
		internal const string RelatedEndExprTypeMustBeReference = "RelatedEndExprTypeMustBeReference";

		// Token: 0x04002033 RID: 8243
		internal const string RelatedEndExprTypeMustBePromotoableToToEnd = "RelatedEndExprTypeMustBePromotoableToToEnd";

		// Token: 0x04002034 RID: 8244
		internal const string RelationshipFromEndIsAmbiguos = "RelationshipFromEndIsAmbiguos";

		// Token: 0x04002035 RID: 8245
		internal const string RelationshipTypeExpected = "RelationshipTypeExpected";

		// Token: 0x04002036 RID: 8246
		internal const string RelationshipToEndIsAmbiguos = "RelationshipToEndIsAmbiguos";

		// Token: 0x04002037 RID: 8247
		internal const string RelationshipTargetMustBeUnique = "RelationshipTargetMustBeUnique";

		// Token: 0x04002038 RID: 8248
		internal const string ResultingExpressionTypeCannotBeNull = "ResultingExpressionTypeCannotBeNull";

		// Token: 0x04002039 RID: 8249
		internal const string RightSetExpressionArgsMustBeCollection = "RightSetExpressionArgsMustBeCollection";

		// Token: 0x0400203A RID: 8250
		internal const string RowCtorElementCannotBeNull = "RowCtorElementCannotBeNull";

		// Token: 0x0400203B RID: 8251
		internal const string SelectDistinctMustBeEqualComparable = "SelectDistinctMustBeEqualComparable";

		// Token: 0x0400203C RID: 8252
		internal const string SourceTypeMustBePromotoableToFromEndRelationType = "SourceTypeMustBePromotoableToFromEndRelationType";

		// Token: 0x0400203D RID: 8253
		internal const string TopAndLimitCannotCoexist = "TopAndLimitCannotCoexist";

		// Token: 0x0400203E RID: 8254
		internal const string TopAndSkipCannotCoexist = "TopAndSkipCannotCoexist";

		// Token: 0x0400203F RID: 8255
		internal const string TypeDoesNotSupportSpec = "TypeDoesNotSupportSpec";

		// Token: 0x04002040 RID: 8256
		internal const string TypeDoesNotSupportFacet = "TypeDoesNotSupportFacet";

		// Token: 0x04002041 RID: 8257
		internal const string TypeArgumentCountMismatch = "TypeArgumentCountMismatch";

		// Token: 0x04002042 RID: 8258
		internal const string TypeArgumentMustBeLiteral = "TypeArgumentMustBeLiteral";

		// Token: 0x04002043 RID: 8259
		internal const string TypeArgumentBelowMin = "TypeArgumentBelowMin";

		// Token: 0x04002044 RID: 8260
		internal const string TypeArgumentExceedsMax = "TypeArgumentExceedsMax";

		// Token: 0x04002045 RID: 8261
		internal const string TypeArgumentIsNotValid = "TypeArgumentIsNotValid";

		// Token: 0x04002046 RID: 8262
		internal const string TypeKindMismatch = "TypeKindMismatch";

		// Token: 0x04002047 RID: 8263
		internal const string TypeMustBeInheritableType = "TypeMustBeInheritableType";

		// Token: 0x04002048 RID: 8264
		internal const string TypeMustBeEntityType = "TypeMustBeEntityType";

		// Token: 0x04002049 RID: 8265
		internal const string TypeMustBeNominalType = "TypeMustBeNominalType";

		// Token: 0x0400204A RID: 8266
		internal const string TypeNameNotFound = "TypeNameNotFound";

		// Token: 0x0400204B RID: 8267
		internal const string GroupVarNotFoundInScope = "GroupVarNotFoundInScope";

		// Token: 0x0400204C RID: 8268
		internal const string InvalidArgumentTypeForAggregateFunction = "InvalidArgumentTypeForAggregateFunction";

		// Token: 0x0400204D RID: 8269
		internal const string InvalidSavePoint = "InvalidSavePoint";

		// Token: 0x0400204E RID: 8270
		internal const string InvalidScopeIndex = "InvalidScopeIndex";

		// Token: 0x0400204F RID: 8271
		internal const string LiteralTypeNotSupported = "LiteralTypeNotSupported";

		// Token: 0x04002050 RID: 8272
		internal const string ParserFatalError = "ParserFatalError";

		// Token: 0x04002051 RID: 8273
		internal const string ParserInputError = "ParserInputError";

		// Token: 0x04002052 RID: 8274
		internal const string StackOverflowInParser = "StackOverflowInParser";

		// Token: 0x04002053 RID: 8275
		internal const string UnknownAstCommandExpression = "UnknownAstCommandExpression";

		// Token: 0x04002054 RID: 8276
		internal const string UnknownAstExpressionType = "UnknownAstExpressionType";

		// Token: 0x04002055 RID: 8277
		internal const string UnknownBuiltInAstExpressionType = "UnknownBuiltInAstExpressionType";

		// Token: 0x04002056 RID: 8278
		internal const string UnknownExpressionResolutionClass = "UnknownExpressionResolutionClass";

		// Token: 0x04002057 RID: 8279
		internal const string Cqt_General_UnsupportedExpression = "Cqt_General_UnsupportedExpression";

		// Token: 0x04002058 RID: 8280
		internal const string Cqt_General_PolymorphicTypeRequired = "Cqt_General_PolymorphicTypeRequired";

		// Token: 0x04002059 RID: 8281
		internal const string Cqt_General_PolymorphicArgRequired = "Cqt_General_PolymorphicArgRequired";

		// Token: 0x0400205A RID: 8282
		internal const string Cqt_General_MetadataNotReadOnly = "Cqt_General_MetadataNotReadOnly";

		// Token: 0x0400205B RID: 8283
		internal const string Cqt_General_NoProviderBooleanType = "Cqt_General_NoProviderBooleanType";

		// Token: 0x0400205C RID: 8284
		internal const string Cqt_General_NoProviderIntegerType = "Cqt_General_NoProviderIntegerType";

		// Token: 0x0400205D RID: 8285
		internal const string Cqt_General_NoProviderStringType = "Cqt_General_NoProviderStringType";

		// Token: 0x0400205E RID: 8286
		internal const string Cqt_Metadata_EdmMemberIncorrectSpace = "Cqt_Metadata_EdmMemberIncorrectSpace";

		// Token: 0x0400205F RID: 8287
		internal const string Cqt_Metadata_EntitySetEntityContainerNull = "Cqt_Metadata_EntitySetEntityContainerNull";

		// Token: 0x04002060 RID: 8288
		internal const string Cqt_Metadata_EntitySetIncorrectSpace = "Cqt_Metadata_EntitySetIncorrectSpace";

		// Token: 0x04002061 RID: 8289
		internal const string Cqt_Metadata_EntityTypeNullKeyMembersInvalid = "Cqt_Metadata_EntityTypeNullKeyMembersInvalid";

		// Token: 0x04002062 RID: 8290
		internal const string Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid = "Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid";

		// Token: 0x04002063 RID: 8291
		internal const string Cqt_Metadata_FunctionReturnParameterNull = "Cqt_Metadata_FunctionReturnParameterNull";

		// Token: 0x04002064 RID: 8292
		internal const string Cqt_Metadata_FunctionIncorrectSpace = "Cqt_Metadata_FunctionIncorrectSpace";

		// Token: 0x04002065 RID: 8293
		internal const string Cqt_Metadata_FunctionParameterIncorrectSpace = "Cqt_Metadata_FunctionParameterIncorrectSpace";

		// Token: 0x04002066 RID: 8294
		internal const string Cqt_Metadata_TypeUsageIncorrectSpace = "Cqt_Metadata_TypeUsageIncorrectSpace";

		// Token: 0x04002067 RID: 8295
		internal const string Cqt_Exceptions_InvalidCommandTree = "Cqt_Exceptions_InvalidCommandTree";

		// Token: 0x04002068 RID: 8296
		internal const string Cqt_Util_CheckListEmptyInvalid = "Cqt_Util_CheckListEmptyInvalid";

		// Token: 0x04002069 RID: 8297
		internal const string Cqt_Util_CheckListDuplicateName = "Cqt_Util_CheckListDuplicateName";

		// Token: 0x0400206A RID: 8298
		internal const string Cqt_ExpressionLink_TypeMismatch = "Cqt_ExpressionLink_TypeMismatch";

		// Token: 0x0400206B RID: 8299
		internal const string Cqt_ExpressionList_IncorrectElementCount = "Cqt_ExpressionList_IncorrectElementCount";

		// Token: 0x0400206C RID: 8300
		internal const string Cqt_Copier_EntityContainerNotFound = "Cqt_Copier_EntityContainerNotFound";

		// Token: 0x0400206D RID: 8301
		internal const string Cqt_Copier_EntitySetNotFound = "Cqt_Copier_EntitySetNotFound";

		// Token: 0x0400206E RID: 8302
		internal const string Cqt_Copier_FunctionNotFound = "Cqt_Copier_FunctionNotFound";

		// Token: 0x0400206F RID: 8303
		internal const string Cqt_Copier_PropertyNotFound = "Cqt_Copier_PropertyNotFound";

		// Token: 0x04002070 RID: 8304
		internal const string Cqt_Copier_NavPropertyNotFound = "Cqt_Copier_NavPropertyNotFound";

		// Token: 0x04002071 RID: 8305
		internal const string Cqt_Copier_EndNotFound = "Cqt_Copier_EndNotFound";

		// Token: 0x04002072 RID: 8306
		internal const string Cqt_Copier_TypeNotFound = "Cqt_Copier_TypeNotFound";

		// Token: 0x04002073 RID: 8307
		internal const string Cqt_CommandTree_InvalidDataSpace = "Cqt_CommandTree_InvalidDataSpace";

		// Token: 0x04002074 RID: 8308
		internal const string Cqt_CommandTree_InvalidParameterName = "Cqt_CommandTree_InvalidParameterName";

		// Token: 0x04002075 RID: 8309
		internal const string Cqt_Validator_InvalidIncompatibleParameterReferences = "Cqt_Validator_InvalidIncompatibleParameterReferences";

		// Token: 0x04002076 RID: 8310
		internal const string Cqt_Validator_InvalidOtherWorkspaceMetadata = "Cqt_Validator_InvalidOtherWorkspaceMetadata";

		// Token: 0x04002077 RID: 8311
		internal const string Cqt_Validator_InvalidIncorrectDataSpaceMetadata = "Cqt_Validator_InvalidIncorrectDataSpaceMetadata";

		// Token: 0x04002078 RID: 8312
		internal const string Cqt_Factory_NewCollectionInvalidCommonType = "Cqt_Factory_NewCollectionInvalidCommonType";

		// Token: 0x04002079 RID: 8313
		internal const string NoSuchProperty = "NoSuchProperty";

		// Token: 0x0400207A RID: 8314
		internal const string Cqt_Factory_NoSuchRelationEnd = "Cqt_Factory_NoSuchRelationEnd";

		// Token: 0x0400207B RID: 8315
		internal const string Cqt_Factory_IncompatibleRelationEnds = "Cqt_Factory_IncompatibleRelationEnds";

		// Token: 0x0400207C RID: 8316
		internal const string Cqt_Factory_MethodResultTypeNotSupported = "Cqt_Factory_MethodResultTypeNotSupported";

		// Token: 0x0400207D RID: 8317
		internal const string Cqt_Aggregate_InvalidFunction = "Cqt_Aggregate_InvalidFunction";

		// Token: 0x0400207E RID: 8318
		internal const string Cqt_Binding_CollectionRequired = "Cqt_Binding_CollectionRequired";

		// Token: 0x0400207F RID: 8319
		internal const string Cqt_GroupBinding_CollectionRequired = "Cqt_GroupBinding_CollectionRequired";

		// Token: 0x04002080 RID: 8320
		internal const string Cqt_Binary_CollectionsRequired = "Cqt_Binary_CollectionsRequired";

		// Token: 0x04002081 RID: 8321
		internal const string Cqt_Unary_CollectionRequired = "Cqt_Unary_CollectionRequired";

		// Token: 0x04002082 RID: 8322
		internal const string Cqt_And_BooleanArgumentsRequired = "Cqt_And_BooleanArgumentsRequired";

		// Token: 0x04002083 RID: 8323
		internal const string Cqt_Apply_DuplicateVariableNames = "Cqt_Apply_DuplicateVariableNames";

		// Token: 0x04002084 RID: 8324
		internal const string Cqt_Arithmetic_NumericCommonType = "Cqt_Arithmetic_NumericCommonType";

		// Token: 0x04002085 RID: 8325
		internal const string Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus = "Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus";

		// Token: 0x04002086 RID: 8326
		internal const string Cqt_Case_WhensMustEqualThens = "Cqt_Case_WhensMustEqualThens";

		// Token: 0x04002087 RID: 8327
		internal const string Cqt_Case_InvalidResultType = "Cqt_Case_InvalidResultType";

		// Token: 0x04002088 RID: 8328
		internal const string Cqt_Cast_InvalidCast = "Cqt_Cast_InvalidCast";

		// Token: 0x04002089 RID: 8329
		internal const string Cqt_Comparison_ComparableRequired = "Cqt_Comparison_ComparableRequired";

		// Token: 0x0400208A RID: 8330
		internal const string Cqt_Constant_InvalidType = "Cqt_Constant_InvalidType";

		// Token: 0x0400208B RID: 8331
		internal const string Cqt_Constant_InvalidValueForType = "Cqt_Constant_InvalidValueForType";

		// Token: 0x0400208C RID: 8332
		internal const string Cqt_Constant_InvalidConstantType = "Cqt_Constant_InvalidConstantType";

		// Token: 0x0400208D RID: 8333
		internal const string Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType = "Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType";

		// Token: 0x0400208E RID: 8334
		internal const string Cqt_Distinct_InvalidCollection = "Cqt_Distinct_InvalidCollection";

		// Token: 0x0400208F RID: 8335
		internal const string Cqt_DeRef_RefRequired = "Cqt_DeRef_RefRequired";

		// Token: 0x04002090 RID: 8336
		internal const string Cqt_Element_InvalidArgumentForUnwrapSingleProperty = "Cqt_Element_InvalidArgumentForUnwrapSingleProperty";

		// Token: 0x04002091 RID: 8337
		internal const string Cqt_Function_VoidResultInvalid = "Cqt_Function_VoidResultInvalid";

		// Token: 0x04002092 RID: 8338
		internal const string Cqt_Function_NonComposableInExpression = "Cqt_Function_NonComposableInExpression";

		// Token: 0x04002093 RID: 8339
		internal const string Cqt_Function_CommandTextInExpression = "Cqt_Function_CommandTextInExpression";

		// Token: 0x04002094 RID: 8340
		internal const string Cqt_Function_CanonicalFunction_NotFound = "Cqt_Function_CanonicalFunction_NotFound";

		// Token: 0x04002095 RID: 8341
		internal const string Cqt_Function_CanonicalFunction_AmbiguousMatch = "Cqt_Function_CanonicalFunction_AmbiguousMatch";

		// Token: 0x04002096 RID: 8342
		internal const string Cqt_GetEntityRef_EntityRequired = "Cqt_GetEntityRef_EntityRequired";

		// Token: 0x04002097 RID: 8343
		internal const string Cqt_GetRefKey_RefRequired = "Cqt_GetRefKey_RefRequired";

		// Token: 0x04002098 RID: 8344
		internal const string Cqt_GroupBy_AtLeastOneKeyOrAggregate = "Cqt_GroupBy_AtLeastOneKeyOrAggregate";

		// Token: 0x04002099 RID: 8345
		internal const string Cqt_GroupBy_KeyNotEqualityComparable = "Cqt_GroupBy_KeyNotEqualityComparable";

		// Token: 0x0400209A RID: 8346
		internal const string Cqt_GroupBy_AggregateColumnExistsAsGroupColumn = "Cqt_GroupBy_AggregateColumnExistsAsGroupColumn";

		// Token: 0x0400209B RID: 8347
		internal const string Cqt_GroupBy_MoreThanOneGroupAggregate = "Cqt_GroupBy_MoreThanOneGroupAggregate";

		// Token: 0x0400209C RID: 8348
		internal const string Cqt_CrossJoin_AtLeastTwoInputs = "Cqt_CrossJoin_AtLeastTwoInputs";

		// Token: 0x0400209D RID: 8349
		internal const string Cqt_CrossJoin_DuplicateVariableNames = "Cqt_CrossJoin_DuplicateVariableNames";

		// Token: 0x0400209E RID: 8350
		internal const string Cqt_IsNull_CollectionNotAllowed = "Cqt_IsNull_CollectionNotAllowed";

		// Token: 0x0400209F RID: 8351
		internal const string Cqt_IsNull_InvalidType = "Cqt_IsNull_InvalidType";

		// Token: 0x040020A0 RID: 8352
		internal const string Cqt_InvalidTypeForSetOperation = "Cqt_InvalidTypeForSetOperation";

		// Token: 0x040020A1 RID: 8353
		internal const string Cqt_Join_DuplicateVariableNames = "Cqt_Join_DuplicateVariableNames";

		// Token: 0x040020A2 RID: 8354
		internal const string Cqt_Limit_ConstantOrParameterRefRequired = "Cqt_Limit_ConstantOrParameterRefRequired";

		// Token: 0x040020A3 RID: 8355
		internal const string Cqt_Limit_IntegerRequired = "Cqt_Limit_IntegerRequired";

		// Token: 0x040020A4 RID: 8356
		internal const string Cqt_Limit_NonNegativeLimitRequired = "Cqt_Limit_NonNegativeLimitRequired";

		// Token: 0x040020A5 RID: 8357
		internal const string Cqt_NewInstance_CollectionTypeRequired = "Cqt_NewInstance_CollectionTypeRequired";

		// Token: 0x040020A6 RID: 8358
		internal const string Cqt_NewInstance_StructuralTypeRequired = "Cqt_NewInstance_StructuralTypeRequired";

		// Token: 0x040020A7 RID: 8359
		internal const string Cqt_NewInstance_CannotInstantiateMemberlessType = "Cqt_NewInstance_CannotInstantiateMemberlessType";

		// Token: 0x040020A8 RID: 8360
		internal const string Cqt_NewInstance_CannotInstantiateAbstractType = "Cqt_NewInstance_CannotInstantiateAbstractType";

		// Token: 0x040020A9 RID: 8361
		internal const string Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid = "Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid";

		// Token: 0x040020AA RID: 8362
		internal const string Cqt_Not_BooleanArgumentRequired = "Cqt_Not_BooleanArgumentRequired";

		// Token: 0x040020AB RID: 8363
		internal const string Cqt_Or_BooleanArgumentsRequired = "Cqt_Or_BooleanArgumentsRequired";

		// Token: 0x040020AC RID: 8364
		internal const string Cqt_In_SameResultTypeRequired = "Cqt_In_SameResultTypeRequired";

		// Token: 0x040020AD RID: 8365
		internal const string Cqt_Property_InstanceRequiredForInstance = "Cqt_Property_InstanceRequiredForInstance";

		// Token: 0x040020AE RID: 8366
		internal const string Cqt_Ref_PolymorphicArgRequired = "Cqt_Ref_PolymorphicArgRequired";

		// Token: 0x040020AF RID: 8367
		internal const string Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship = "Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship";

		// Token: 0x040020B0 RID: 8368
		internal const string Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne = "Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne";

		// Token: 0x040020B1 RID: 8369
		internal const string Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd = "Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd";

		// Token: 0x040020B2 RID: 8370
		internal const string Cqt_RelatedEntityRef_TargetEntityNotRef = "Cqt_RelatedEntityRef_TargetEntityNotRef";

		// Token: 0x040020B3 RID: 8371
		internal const string Cqt_RelatedEntityRef_TargetEntityNotCompatible = "Cqt_RelatedEntityRef_TargetEntityNotCompatible";

		// Token: 0x040020B4 RID: 8372
		internal const string Cqt_RelNav_NoCompositions = "Cqt_RelNav_NoCompositions";

		// Token: 0x040020B5 RID: 8373
		internal const string Cqt_RelNav_WrongSourceType = "Cqt_RelNav_WrongSourceType";

		// Token: 0x040020B6 RID: 8374
		internal const string Cqt_Skip_ConstantOrParameterRefRequired = "Cqt_Skip_ConstantOrParameterRefRequired";

		// Token: 0x040020B7 RID: 8375
		internal const string Cqt_Skip_IntegerRequired = "Cqt_Skip_IntegerRequired";

		// Token: 0x040020B8 RID: 8376
		internal const string Cqt_Skip_NonNegativeCountRequired = "Cqt_Skip_NonNegativeCountRequired";

		// Token: 0x040020B9 RID: 8377
		internal const string Cqt_Sort_NonStringCollationInvalid = "Cqt_Sort_NonStringCollationInvalid";

		// Token: 0x040020BA RID: 8378
		internal const string Cqt_Sort_OrderComparable = "Cqt_Sort_OrderComparable";

		// Token: 0x040020BB RID: 8379
		internal const string Cqt_UDF_FunctionDefinitionGenerationFailed = "Cqt_UDF_FunctionDefinitionGenerationFailed";

		// Token: 0x040020BC RID: 8380
		internal const string Cqt_UDF_FunctionDefinitionWithCircularReference = "Cqt_UDF_FunctionDefinitionWithCircularReference";

		// Token: 0x040020BD RID: 8381
		internal const string Cqt_UDF_FunctionDefinitionResultTypeMismatch = "Cqt_UDF_FunctionDefinitionResultTypeMismatch";

		// Token: 0x040020BE RID: 8382
		internal const string Cqt_UDF_FunctionHasNoDefinition = "Cqt_UDF_FunctionHasNoDefinition";

		// Token: 0x040020BF RID: 8383
		internal const string Cqt_Validator_VarRefInvalid = "Cqt_Validator_VarRefInvalid";

		// Token: 0x040020C0 RID: 8384
		internal const string Cqt_Validator_VarRefTypeMismatch = "Cqt_Validator_VarRefTypeMismatch";

		// Token: 0x040020C1 RID: 8385
		internal const string Iqt_General_UnsupportedOp = "Iqt_General_UnsupportedOp";

		// Token: 0x040020C2 RID: 8386
		internal const string Iqt_CTGen_UnexpectedAggregate = "Iqt_CTGen_UnexpectedAggregate";

		// Token: 0x040020C3 RID: 8387
		internal const string Iqt_CTGen_UnexpectedVarDefList = "Iqt_CTGen_UnexpectedVarDefList";

		// Token: 0x040020C4 RID: 8388
		internal const string Iqt_CTGen_UnexpectedVarDef = "Iqt_CTGen_UnexpectedVarDef";

		// Token: 0x040020C5 RID: 8389
		internal const string ADP_MustUseSequentialAccess = "ADP_MustUseSequentialAccess";

		// Token: 0x040020C6 RID: 8390
		internal const string ADP_ProviderDoesNotSupportCommandTrees = "ADP_ProviderDoesNotSupportCommandTrees";

		// Token: 0x040020C7 RID: 8391
		internal const string ADP_ClosedDataReaderError = "ADP_ClosedDataReaderError";

		// Token: 0x040020C8 RID: 8392
		internal const string ADP_DataReaderClosed = "ADP_DataReaderClosed";

		// Token: 0x040020C9 RID: 8393
		internal const string ADP_ImplicitlyClosedDataReaderError = "ADP_ImplicitlyClosedDataReaderError";

		// Token: 0x040020CA RID: 8394
		internal const string ADP_NoData = "ADP_NoData";

		// Token: 0x040020CB RID: 8395
		internal const string ADP_GetSchemaTableIsNotSupported = "ADP_GetSchemaTableIsNotSupported";

		// Token: 0x040020CC RID: 8396
		internal const string ADP_InvalidDataReaderFieldCountForScalarType = "ADP_InvalidDataReaderFieldCountForScalarType";

		// Token: 0x040020CD RID: 8397
		internal const string ADP_InvalidDataReaderMissingColumnForType = "ADP_InvalidDataReaderMissingColumnForType";

		// Token: 0x040020CE RID: 8398
		internal const string ADP_InvalidDataReaderMissingDiscriminatorColumn = "ADP_InvalidDataReaderMissingDiscriminatorColumn";

		// Token: 0x040020CF RID: 8399
		internal const string ADP_InvalidDataReaderUnableToDetermineType = "ADP_InvalidDataReaderUnableToDetermineType";

		// Token: 0x040020D0 RID: 8400
		internal const string ADP_InvalidDataReaderUnableToMaterializeNonScalarType = "ADP_InvalidDataReaderUnableToMaterializeNonScalarType";

		// Token: 0x040020D1 RID: 8401
		internal const string ADP_KeysRequiredForJoinOverNest = "ADP_KeysRequiredForJoinOverNest";

		// Token: 0x040020D2 RID: 8402
		internal const string ADP_KeysRequiredForNesting = "ADP_KeysRequiredForNesting";

		// Token: 0x040020D3 RID: 8403
		internal const string ADP_NestingNotSupported = "ADP_NestingNotSupported";

		// Token: 0x040020D4 RID: 8404
		internal const string ADP_NoQueryMappingView = "ADP_NoQueryMappingView";

		// Token: 0x040020D5 RID: 8405
		internal const string ADP_InternalProviderError = "ADP_InternalProviderError";

		// Token: 0x040020D6 RID: 8406
		internal const string ADP_InvalidEnumerationValue = "ADP_InvalidEnumerationValue";

		// Token: 0x040020D7 RID: 8407
		internal const string ADP_InvalidBufferSizeOrIndex = "ADP_InvalidBufferSizeOrIndex";

		// Token: 0x040020D8 RID: 8408
		internal const string ADP_InvalidDataLength = "ADP_InvalidDataLength";

		// Token: 0x040020D9 RID: 8409
		internal const string ADP_InvalidDataType = "ADP_InvalidDataType";

		// Token: 0x040020DA RID: 8410
		internal const string ADP_InvalidDestinationBufferIndex = "ADP_InvalidDestinationBufferIndex";

		// Token: 0x040020DB RID: 8411
		internal const string ADP_InvalidSourceBufferIndex = "ADP_InvalidSourceBufferIndex";

		// Token: 0x040020DC RID: 8412
		internal const string ADP_NonSequentialChunkAccess = "ADP_NonSequentialChunkAccess";

		// Token: 0x040020DD RID: 8413
		internal const string ADP_NonSequentialColumnAccess = "ADP_NonSequentialColumnAccess";

		// Token: 0x040020DE RID: 8414
		internal const string ADP_UnknownDataTypeCode = "ADP_UnknownDataTypeCode";

		// Token: 0x040020DF RID: 8415
		internal const string DataCategory_Data = "DataCategory_Data";

		// Token: 0x040020E0 RID: 8416
		internal const string DbParameter_Direction = "DbParameter_Direction";

		// Token: 0x040020E1 RID: 8417
		internal const string DbParameter_Size = "DbParameter_Size";

		// Token: 0x040020E2 RID: 8418
		internal const string DataCategory_Update = "DataCategory_Update";

		// Token: 0x040020E3 RID: 8419
		internal const string DbParameter_SourceColumn = "DbParameter_SourceColumn";

		// Token: 0x040020E4 RID: 8420
		internal const string DbParameter_SourceVersion = "DbParameter_SourceVersion";

		// Token: 0x040020E5 RID: 8421
		internal const string ADP_CollectionParameterElementIsNull = "ADP_CollectionParameterElementIsNull";

		// Token: 0x040020E6 RID: 8422
		internal const string ADP_CollectionParameterElementIsNullOrEmpty = "ADP_CollectionParameterElementIsNullOrEmpty";

		// Token: 0x040020E7 RID: 8423
		internal const string NonReturnParameterInReturnParameterCollection = "NonReturnParameterInReturnParameterCollection";

		// Token: 0x040020E8 RID: 8424
		internal const string ReturnParameterInInputParameterCollection = "ReturnParameterInInputParameterCollection";

		// Token: 0x040020E9 RID: 8425
		internal const string NullEntitySetsForFunctionReturningMultipleResultSets = "NullEntitySetsForFunctionReturningMultipleResultSets";

		// Token: 0x040020EA RID: 8426
		internal const string NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters = "NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters";

		// Token: 0x040020EB RID: 8427
		internal const string EntityParameterCollectionInvalidParameterName = "EntityParameterCollectionInvalidParameterName";

		// Token: 0x040020EC RID: 8428
		internal const string EntityParameterCollectionInvalidIndex = "EntityParameterCollectionInvalidIndex";

		// Token: 0x040020ED RID: 8429
		internal const string InvalidEntityParameterType = "InvalidEntityParameterType";

		// Token: 0x040020EE RID: 8430
		internal const string EntityParameterContainedByAnotherCollection = "EntityParameterContainedByAnotherCollection";

		// Token: 0x040020EF RID: 8431
		internal const string EntityParameterCollectionRemoveInvalidObject = "EntityParameterCollectionRemoveInvalidObject";

		// Token: 0x040020F0 RID: 8432
		internal const string ADP_ConnectionStringSyntax = "ADP_ConnectionStringSyntax";

		// Token: 0x040020F1 RID: 8433
		internal const string ExpandingDataDirectoryFailed = "ExpandingDataDirectoryFailed";

		// Token: 0x040020F2 RID: 8434
		internal const string ADP_InvalidDataDirectory = "ADP_InvalidDataDirectory";

		// Token: 0x040020F3 RID: 8435
		internal const string ADP_InvalidMultipartNameDelimiterUsage = "ADP_InvalidMultipartNameDelimiterUsage";

		// Token: 0x040020F4 RID: 8436
		internal const string ADP_InvalidSizeValue = "ADP_InvalidSizeValue";

		// Token: 0x040020F5 RID: 8437
		internal const string ADP_KeywordNotSupported = "ADP_KeywordNotSupported";

		// Token: 0x040020F6 RID: 8438
		internal const string ConstantFacetSpecifiedInSchema = "ConstantFacetSpecifiedInSchema";

		// Token: 0x040020F7 RID: 8439
		internal const string DuplicateAnnotation = "DuplicateAnnotation";

		// Token: 0x040020F8 RID: 8440
		internal const string EmptyFile = "EmptyFile";

		// Token: 0x040020F9 RID: 8441
		internal const string EmptySchemaTextReader = "EmptySchemaTextReader";

		// Token: 0x040020FA RID: 8442
		internal const string EmptyName = "EmptyName";

		// Token: 0x040020FB RID: 8443
		internal const string InvalidName = "InvalidName";

		// Token: 0x040020FC RID: 8444
		internal const string MissingName = "MissingName";

		// Token: 0x040020FD RID: 8445
		internal const string UnexpectedXmlAttribute = "UnexpectedXmlAttribute";

		// Token: 0x040020FE RID: 8446
		internal const string UnexpectedXmlElement = "UnexpectedXmlElement";

		// Token: 0x040020FF RID: 8447
		internal const string TextNotAllowed = "TextNotAllowed";

		// Token: 0x04002100 RID: 8448
		internal const string UnexpectedXmlNodeType = "UnexpectedXmlNodeType";

		// Token: 0x04002101 RID: 8449
		internal const string MalformedXml = "MalformedXml";

		// Token: 0x04002102 RID: 8450
		internal const string ValueNotUnderstood = "ValueNotUnderstood";

		// Token: 0x04002103 RID: 8451
		internal const string EntityContainerAlreadyExists = "EntityContainerAlreadyExists";

		// Token: 0x04002104 RID: 8452
		internal const string TypeNameAlreadyDefinedDuplicate = "TypeNameAlreadyDefinedDuplicate";

		// Token: 0x04002105 RID: 8453
		internal const string PropertyNameAlreadyDefinedDuplicate = "PropertyNameAlreadyDefinedDuplicate";

		// Token: 0x04002106 RID: 8454
		internal const string DuplicateMemberNameInExtendedEntityContainer = "DuplicateMemberNameInExtendedEntityContainer";

		// Token: 0x04002107 RID: 8455
		internal const string DuplicateEntityContainerMemberName = "DuplicateEntityContainerMemberName";

		// Token: 0x04002108 RID: 8456
		internal const string PropertyTypeAlreadyDefined = "PropertyTypeAlreadyDefined";

		// Token: 0x04002109 RID: 8457
		internal const string InvalidSize = "InvalidSize";

		// Token: 0x0400210A RID: 8458
		internal const string InvalidSystemReferenceId = "InvalidSystemReferenceId";

		// Token: 0x0400210B RID: 8459
		internal const string BadNamespaceOrAlias = "BadNamespaceOrAlias";

		// Token: 0x0400210C RID: 8460
		internal const string MissingNamespaceAttribute = "MissingNamespaceAttribute";

		// Token: 0x0400210D RID: 8461
		internal const string InvalidBaseTypeForStructuredType = "InvalidBaseTypeForStructuredType";

		// Token: 0x0400210E RID: 8462
		internal const string InvalidPropertyType = "InvalidPropertyType";

		// Token: 0x0400210F RID: 8463
		internal const string InvalidBaseTypeForItemType = "InvalidBaseTypeForItemType";

		// Token: 0x04002110 RID: 8464
		internal const string InvalidBaseTypeForNestedType = "InvalidBaseTypeForNestedType";

		// Token: 0x04002111 RID: 8465
		internal const string DefaultNotAllowed = "DefaultNotAllowed";

		// Token: 0x04002112 RID: 8466
		internal const string FacetNotAllowed = "FacetNotAllowed";

		// Token: 0x04002113 RID: 8467
		internal const string RequiredFacetMissing = "RequiredFacetMissing";

		// Token: 0x04002114 RID: 8468
		internal const string InvalidDefaultBinaryWithNoMaxLength = "InvalidDefaultBinaryWithNoMaxLength";

		// Token: 0x04002115 RID: 8469
		internal const string InvalidDefaultIntegral = "InvalidDefaultIntegral";

		// Token: 0x04002116 RID: 8470
		internal const string InvalidDefaultDateTime = "InvalidDefaultDateTime";

		// Token: 0x04002117 RID: 8471
		internal const string InvalidDefaultTime = "InvalidDefaultTime";

		// Token: 0x04002118 RID: 8472
		internal const string InvalidDefaultDateTimeOffset = "InvalidDefaultDateTimeOffset";

		// Token: 0x04002119 RID: 8473
		internal const string InvalidDefaultDecimal = "InvalidDefaultDecimal";

		// Token: 0x0400211A RID: 8474
		internal const string InvalidDefaultFloatingPoint = "InvalidDefaultFloatingPoint";

		// Token: 0x0400211B RID: 8475
		internal const string InvalidDefaultGuid = "InvalidDefaultGuid";

		// Token: 0x0400211C RID: 8476
		internal const string InvalidDefaultBoolean = "InvalidDefaultBoolean";

		// Token: 0x0400211D RID: 8477
		internal const string DuplicateMemberName = "DuplicateMemberName";

		// Token: 0x0400211E RID: 8478
		internal const string GeneratorErrorSeverityError = "GeneratorErrorSeverityError";

		// Token: 0x0400211F RID: 8479
		internal const string GeneratorErrorSeverityWarning = "GeneratorErrorSeverityWarning";

		// Token: 0x04002120 RID: 8480
		internal const string GeneratorErrorSeverityUnknown = "GeneratorErrorSeverityUnknown";

		// Token: 0x04002121 RID: 8481
		internal const string SourceUriUnknown = "SourceUriUnknown";

		// Token: 0x04002122 RID: 8482
		internal const string BadPrecisionAndScale = "BadPrecisionAndScale";

		// Token: 0x04002123 RID: 8483
		internal const string InvalidNamespaceInUsing = "InvalidNamespaceInUsing";

		// Token: 0x04002124 RID: 8484
		internal const string BadNavigationPropertyRelationshipNotRelationship = "BadNavigationPropertyRelationshipNotRelationship";

		// Token: 0x04002125 RID: 8485
		internal const string BadNavigationPropertyRolesCannotBeTheSame = "BadNavigationPropertyRolesCannotBeTheSame";

		// Token: 0x04002126 RID: 8486
		internal const string BadNavigationPropertyUndefinedRole = "BadNavigationPropertyUndefinedRole";

		// Token: 0x04002127 RID: 8487
		internal const string BadNavigationPropertyBadFromRoleType = "BadNavigationPropertyBadFromRoleType";

		// Token: 0x04002128 RID: 8488
		internal const string InvalidMemberNameMatchesTypeName = "InvalidMemberNameMatchesTypeName";

		// Token: 0x04002129 RID: 8489
		internal const string InvalidKeyKeyDefinedInBaseClass = "InvalidKeyKeyDefinedInBaseClass";

		// Token: 0x0400212A RID: 8490
		internal const string InvalidKeyNullablePart = "InvalidKeyNullablePart";

		// Token: 0x0400212B RID: 8491
		internal const string InvalidKeyNoProperty = "InvalidKeyNoProperty";

		// Token: 0x0400212C RID: 8492
		internal const string KeyMissingOnEntityType = "KeyMissingOnEntityType";

		// Token: 0x0400212D RID: 8493
		internal const string InvalidDocumentationBothTextAndStructure = "InvalidDocumentationBothTextAndStructure";

		// Token: 0x0400212E RID: 8494
		internal const string ArgumentOutOfRangeExpectedPostiveNumber = "ArgumentOutOfRangeExpectedPostiveNumber";

		// Token: 0x0400212F RID: 8495
		internal const string ArgumentOutOfRange = "ArgumentOutOfRange";

		// Token: 0x04002130 RID: 8496
		internal const string UnacceptableUri = "UnacceptableUri";

		// Token: 0x04002131 RID: 8497
		internal const string UnexpectedTypeInCollection = "UnexpectedTypeInCollection";

		// Token: 0x04002132 RID: 8498
		internal const string AllElementsMustBeInSchema = "AllElementsMustBeInSchema";

		// Token: 0x04002133 RID: 8499
		internal const string AliasNameIsAlreadyDefined = "AliasNameIsAlreadyDefined";

		// Token: 0x04002134 RID: 8500
		internal const string NeedNotUseSystemNamespaceInUsing = "NeedNotUseSystemNamespaceInUsing";

		// Token: 0x04002135 RID: 8501
		internal const string CannotUseSystemNamespaceAsAlias = "CannotUseSystemNamespaceAsAlias";

		// Token: 0x04002136 RID: 8502
		internal const string EntitySetTypeHasNoKeys = "EntitySetTypeHasNoKeys";

		// Token: 0x04002137 RID: 8503
		internal const string TableAndSchemaAreMutuallyExclusiveWithDefiningQuery = "TableAndSchemaAreMutuallyExclusiveWithDefiningQuery";

		// Token: 0x04002138 RID: 8504
		internal const string UnexpectedRootElement = "UnexpectedRootElement";

		// Token: 0x04002139 RID: 8505
		internal const string UnexpectedRootElementNoNamespace = "UnexpectedRootElementNoNamespace";

		// Token: 0x0400213A RID: 8506
		internal const string ParameterNameAlreadyDefinedDuplicate = "ParameterNameAlreadyDefinedDuplicate";

		// Token: 0x0400213B RID: 8507
		internal const string FunctionWithNonPrimitiveTypeNotSupported = "FunctionWithNonPrimitiveTypeNotSupported";

		// Token: 0x0400213C RID: 8508
		internal const string FunctionWithNonEdmPrimitiveTypeNotSupported = "FunctionWithNonEdmPrimitiveTypeNotSupported";

		// Token: 0x0400213D RID: 8509
		internal const string FunctionImportWithUnsupportedReturnTypeV1 = "FunctionImportWithUnsupportedReturnTypeV1";

		// Token: 0x0400213E RID: 8510
		internal const string FunctionImportWithUnsupportedReturnTypeV1_1 = "FunctionImportWithUnsupportedReturnTypeV1_1";

		// Token: 0x0400213F RID: 8511
		internal const string FunctionImportWithUnsupportedReturnTypeV2 = "FunctionImportWithUnsupportedReturnTypeV2";

		// Token: 0x04002140 RID: 8512
		internal const string FunctionImportUnknownEntitySet = "FunctionImportUnknownEntitySet";

		// Token: 0x04002141 RID: 8513
		internal const string FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet = "FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet";

		// Token: 0x04002142 RID: 8514
		internal const string FunctionImportEntityTypeDoesNotMatchEntitySet = "FunctionImportEntityTypeDoesNotMatchEntitySet";

		// Token: 0x04002143 RID: 8515
		internal const string FunctionImportSpecifiesEntitySetButNotEntityType = "FunctionImportSpecifiesEntitySetButNotEntityType";

		// Token: 0x04002144 RID: 8516
		internal const string FunctionImportEntitySetAndEntitySetPathDeclared = "FunctionImportEntitySetAndEntitySetPathDeclared";

		// Token: 0x04002145 RID: 8517
		internal const string FunctionImportComposableAndSideEffectingNotAllowed = "FunctionImportComposableAndSideEffectingNotAllowed";

		// Token: 0x04002146 RID: 8518
		internal const string FunctionImportCollectionAndRefParametersNotAllowed = "FunctionImportCollectionAndRefParametersNotAllowed";

		// Token: 0x04002147 RID: 8519
		internal const string FunctionImportNonNullableParametersNotAllowed = "FunctionImportNonNullableParametersNotAllowed";

		// Token: 0x04002148 RID: 8520
		internal const string TVFReturnTypeRowHasNonScalarProperty = "TVFReturnTypeRowHasNonScalarProperty";

		// Token: 0x04002149 RID: 8521
		internal const string DuplicateEntitySetTable = "DuplicateEntitySetTable";

		// Token: 0x0400214A RID: 8522
		internal const string ConcurrencyRedefinedOnSubTypeOfEntitySetType = "ConcurrencyRedefinedOnSubTypeOfEntitySetType";

		// Token: 0x0400214B RID: 8523
		internal const string SimilarRelationshipEnd = "SimilarRelationshipEnd";

		// Token: 0x0400214C RID: 8524
		internal const string InvalidRelationshipEndMultiplicity = "InvalidRelationshipEndMultiplicity";

		// Token: 0x0400214D RID: 8525
		internal const string EndNameAlreadyDefinedDuplicate = "EndNameAlreadyDefinedDuplicate";

		// Token: 0x0400214E RID: 8526
		internal const string InvalidRelationshipEndType = "InvalidRelationshipEndType";

		// Token: 0x0400214F RID: 8527
		internal const string BadParameterDirection = "BadParameterDirection";

		// Token: 0x04002150 RID: 8528
		internal const string BadParameterDirectionForComposableFunctions = "BadParameterDirectionForComposableFunctions";

		// Token: 0x04002151 RID: 8529
		internal const string InvalidOperationMultipleEndsInAssociation = "InvalidOperationMultipleEndsInAssociation";

		// Token: 0x04002152 RID: 8530
		internal const string InvalidAction = "InvalidAction";

		// Token: 0x04002153 RID: 8531
		internal const string DuplicationOperation = "DuplicationOperation";

		// Token: 0x04002154 RID: 8532
		internal const string NotInNamespaceAlias = "NotInNamespaceAlias";

		// Token: 0x04002155 RID: 8533
		internal const string NotNamespaceQualified = "NotNamespaceQualified";

		// Token: 0x04002156 RID: 8534
		internal const string NotInNamespaceNoAlias = "NotInNamespaceNoAlias";

		// Token: 0x04002157 RID: 8535
		internal const string InvalidValueForParameterTypeSemanticsAttribute = "InvalidValueForParameterTypeSemanticsAttribute";

		// Token: 0x04002158 RID: 8536
		internal const string DuplicatePropertyNameSpecifiedInEntityKey = "DuplicatePropertyNameSpecifiedInEntityKey";

		// Token: 0x04002159 RID: 8537
		internal const string InvalidEntitySetType = "InvalidEntitySetType";

		// Token: 0x0400215A RID: 8538
		internal const string InvalidRelationshipSetType = "InvalidRelationshipSetType";

		// Token: 0x0400215B RID: 8539
		internal const string InvalidEntityContainerNameInExtends = "InvalidEntityContainerNameInExtends";

		// Token: 0x0400215C RID: 8540
		internal const string InvalidNamespaceOrAliasSpecified = "InvalidNamespaceOrAliasSpecified";

		// Token: 0x0400215D RID: 8541
		internal const string PrecisionOutOfRange = "PrecisionOutOfRange";

		// Token: 0x0400215E RID: 8542
		internal const string ScaleOutOfRange = "ScaleOutOfRange";

		// Token: 0x0400215F RID: 8543
		internal const string InvalidEntitySetNameReference = "InvalidEntitySetNameReference";

		// Token: 0x04002160 RID: 8544
		internal const string InvalidEntityEndName = "InvalidEntityEndName";

		// Token: 0x04002161 RID: 8545
		internal const string DuplicateEndName = "DuplicateEndName";

		// Token: 0x04002162 RID: 8546
		internal const string AmbiguousEntityContainerEnd = "AmbiguousEntityContainerEnd";

		// Token: 0x04002163 RID: 8547
		internal const string MissingEntityContainerEnd = "MissingEntityContainerEnd";

		// Token: 0x04002164 RID: 8548
		internal const string InvalidEndEntitySetTypeMismatch = "InvalidEndEntitySetTypeMismatch";

		// Token: 0x04002165 RID: 8549
		internal const string InferRelationshipEndFailedNoEntitySetMatch = "InferRelationshipEndFailedNoEntitySetMatch";

		// Token: 0x04002166 RID: 8550
		internal const string InferRelationshipEndAmbiguous = "InferRelationshipEndAmbiguous";

		// Token: 0x04002167 RID: 8551
		internal const string InferRelationshipEndGivesAlreadyDefinedEnd = "InferRelationshipEndGivesAlreadyDefinedEnd";

		// Token: 0x04002168 RID: 8552
		internal const string TooManyAssociationEnds = "TooManyAssociationEnds";

		// Token: 0x04002169 RID: 8553
		internal const string InvalidEndRoleInRelationshipConstraint = "InvalidEndRoleInRelationshipConstraint";

		// Token: 0x0400216A RID: 8554
		internal const string InvalidFromPropertyInRelationshipConstraint = "InvalidFromPropertyInRelationshipConstraint";

		// Token: 0x0400216B RID: 8555
		internal const string InvalidToPropertyInRelationshipConstraint = "InvalidToPropertyInRelationshipConstraint";

		// Token: 0x0400216C RID: 8556
		internal const string InvalidPropertyInRelationshipConstraint = "InvalidPropertyInRelationshipConstraint";

		// Token: 0x0400216D RID: 8557
		internal const string TypeMismatchRelationshipConstraint = "TypeMismatchRelationshipConstraint";

		// Token: 0x0400216E RID: 8558
		internal const string InvalidMultiplicityFromRoleUpperBoundMustBeOne = "InvalidMultiplicityFromRoleUpperBoundMustBeOne";

		// Token: 0x0400216F RID: 8559
		internal const string InvalidMultiplicityFromRoleToPropertyNonNullableV1 = "InvalidMultiplicityFromRoleToPropertyNonNullableV1";

		// Token: 0x04002170 RID: 8560
		internal const string InvalidMultiplicityFromRoleToPropertyNonNullableV2 = "InvalidMultiplicityFromRoleToPropertyNonNullableV2";

		// Token: 0x04002171 RID: 8561
		internal const string InvalidMultiplicityFromRoleToPropertyNullableV1 = "InvalidMultiplicityFromRoleToPropertyNullableV1";

		// Token: 0x04002172 RID: 8562
		internal const string InvalidMultiplicityToRoleLowerBoundMustBeZero = "InvalidMultiplicityToRoleLowerBoundMustBeZero";

		// Token: 0x04002173 RID: 8563
		internal const string InvalidMultiplicityToRoleUpperBoundMustBeOne = "InvalidMultiplicityToRoleUpperBoundMustBeOne";

		// Token: 0x04002174 RID: 8564
		internal const string InvalidMultiplicityToRoleUpperBoundMustBeMany = "InvalidMultiplicityToRoleUpperBoundMustBeMany";

		// Token: 0x04002175 RID: 8565
		internal const string MismatchNumberOfPropertiesinRelationshipConstraint = "MismatchNumberOfPropertiesinRelationshipConstraint";

		// Token: 0x04002176 RID: 8566
		internal const string MissingConstraintOnRelationshipType = "MissingConstraintOnRelationshipType";

		// Token: 0x04002177 RID: 8567
		internal const string SameRoleReferredInReferentialConstraint = "SameRoleReferredInReferentialConstraint";

		// Token: 0x04002178 RID: 8568
		internal const string InvalidPrimitiveTypeKind = "InvalidPrimitiveTypeKind";

		// Token: 0x04002179 RID: 8569
		internal const string EntityKeyMustBeScalar = "EntityKeyMustBeScalar";

		// Token: 0x0400217A RID: 8570
		internal const string EntityKeyTypeCurrentlyNotSupportedInSSDL = "EntityKeyTypeCurrentlyNotSupportedInSSDL";

		// Token: 0x0400217B RID: 8571
		internal const string EntityKeyTypeCurrentlyNotSupported = "EntityKeyTypeCurrentlyNotSupported";

		// Token: 0x0400217C RID: 8572
		internal const string MissingFacetDescription = "MissingFacetDescription";

		// Token: 0x0400217D RID: 8573
		internal const string EndWithManyMultiplicityCannotHaveOperationsSpecified = "EndWithManyMultiplicityCannotHaveOperationsSpecified";

		// Token: 0x0400217E RID: 8574
		internal const string EndWithoutMultiplicity = "EndWithoutMultiplicity";

		// Token: 0x0400217F RID: 8575
		internal const string EntityContainerCannotExtendItself = "EntityContainerCannotExtendItself";

		// Token: 0x04002180 RID: 8576
		internal const string ComposableFunctionOrFunctionImportMustDeclareReturnType = "ComposableFunctionOrFunctionImportMustDeclareReturnType";

		// Token: 0x04002181 RID: 8577
		internal const string NonComposableFunctionCannotBeMappedAsComposable = "NonComposableFunctionCannotBeMappedAsComposable";

		// Token: 0x04002182 RID: 8578
		internal const string ComposableFunctionImportsReturningEntitiesNotSupported = "ComposableFunctionImportsReturningEntitiesNotSupported";

		// Token: 0x04002183 RID: 8579
		internal const string StructuralTypeMappingsMustNotBeNullForFunctionImportsReturingNonScalarValues = "StructuralTypeMappingsMustNotBeNullForFunctionImportsReturingNonScalarValues";

		// Token: 0x04002184 RID: 8580
		internal const string InvalidReturnTypeForComposableFunction = "InvalidReturnTypeForComposableFunction";

		// Token: 0x04002185 RID: 8581
		internal const string NonComposableFunctionMustNotDeclareReturnType = "NonComposableFunctionMustNotDeclareReturnType";

		// Token: 0x04002186 RID: 8582
		internal const string CommandTextFunctionsNotComposable = "CommandTextFunctionsNotComposable";

		// Token: 0x04002187 RID: 8583
		internal const string CommandTextFunctionsCannotDeclareStoreFunctionName = "CommandTextFunctionsCannotDeclareStoreFunctionName";

		// Token: 0x04002188 RID: 8584
		internal const string NonComposableFunctionHasDisallowedAttribute = "NonComposableFunctionHasDisallowedAttribute";

		// Token: 0x04002189 RID: 8585
		internal const string EmptyDefiningQuery = "EmptyDefiningQuery";

		// Token: 0x0400218A RID: 8586
		internal const string EmptyCommandText = "EmptyCommandText";

		// Token: 0x0400218B RID: 8587
		internal const string AmbiguousFunctionOverload = "AmbiguousFunctionOverload";

		// Token: 0x0400218C RID: 8588
		internal const string AmbiguousFunctionAndType = "AmbiguousFunctionAndType";

		// Token: 0x0400218D RID: 8589
		internal const string CycleInTypeHierarchy = "CycleInTypeHierarchy";

		// Token: 0x0400218E RID: 8590
		internal const string IncorrectProviderManifest = "IncorrectProviderManifest";

		// Token: 0x0400218F RID: 8591
		internal const string ComplexTypeAsReturnTypeAndDefinedEntitySet = "ComplexTypeAsReturnTypeAndDefinedEntitySet";

		// Token: 0x04002190 RID: 8592
		internal const string ComplexTypeAsReturnTypeAndNestedComplexProperty = "ComplexTypeAsReturnTypeAndNestedComplexProperty";

		// Token: 0x04002191 RID: 8593
		internal const string FacetsOnNonScalarType = "FacetsOnNonScalarType";

		// Token: 0x04002192 RID: 8594
		internal const string FacetDeclarationRequiresTypeAttribute = "FacetDeclarationRequiresTypeAttribute";

		// Token: 0x04002193 RID: 8595
		internal const string TypeMustBeDeclared = "TypeMustBeDeclared";

		// Token: 0x04002194 RID: 8596
		internal const string RowTypeWithoutProperty = "RowTypeWithoutProperty";

		// Token: 0x04002195 RID: 8597
		internal const string TypeDeclaredAsAttributeAndElement = "TypeDeclaredAsAttributeAndElement";

		// Token: 0x04002196 RID: 8598
		internal const string ReferenceToNonEntityType = "ReferenceToNonEntityType";

		// Token: 0x04002197 RID: 8599
		internal const string NoCodeGenNamespaceInStructuralAnnotation = "NoCodeGenNamespaceInStructuralAnnotation";

		// Token: 0x04002198 RID: 8600
		internal const string CannotLoadDifferentVersionOfSchemaInTheSameItemCollection = "CannotLoadDifferentVersionOfSchemaInTheSameItemCollection";

		// Token: 0x04002199 RID: 8601
		internal const string InvalidEnumUnderlyingType = "InvalidEnumUnderlyingType";

		// Token: 0x0400219A RID: 8602
		internal const string DuplicateEnumMember = "DuplicateEnumMember";

		// Token: 0x0400219B RID: 8603
		internal const string CalculatedEnumValueOutOfRange = "CalculatedEnumValueOutOfRange";

		// Token: 0x0400219C RID: 8604
		internal const string EnumMemberValueOutOfItsUnderylingTypeRange = "EnumMemberValueOutOfItsUnderylingTypeRange";

		// Token: 0x0400219D RID: 8605
		internal const string SpatialWithUseStrongSpatialTypesFalse = "SpatialWithUseStrongSpatialTypesFalse";

		// Token: 0x0400219E RID: 8606
		internal const string ObjectQuery_QueryBuilder_InvalidResultType = "ObjectQuery_QueryBuilder_InvalidResultType";

		// Token: 0x0400219F RID: 8607
		internal const string ObjectQuery_QueryBuilder_InvalidQueryArgument = "ObjectQuery_QueryBuilder_InvalidQueryArgument";

		// Token: 0x040021A0 RID: 8608
		internal const string ObjectQuery_QueryBuilder_NotSupportedLinqSource = "ObjectQuery_QueryBuilder_NotSupportedLinqSource";

		// Token: 0x040021A1 RID: 8609
		internal const string ObjectQuery_InvalidConnection = "ObjectQuery_InvalidConnection";

		// Token: 0x040021A2 RID: 8610
		internal const string ObjectQuery_InvalidQueryName = "ObjectQuery_InvalidQueryName";

		// Token: 0x040021A3 RID: 8611
		internal const string ObjectQuery_UnableToMapResultType = "ObjectQuery_UnableToMapResultType";

		// Token: 0x040021A4 RID: 8612
		internal const string ObjectQuery_UnableToMaterializeArray = "ObjectQuery_UnableToMaterializeArray";

		// Token: 0x040021A5 RID: 8613
		internal const string ObjectQuery_UnableToMaterializeArbitaryProjectionType = "ObjectQuery_UnableToMaterializeArbitaryProjectionType";

		// Token: 0x040021A6 RID: 8614
		internal const string ObjectParameter_InvalidParameterName = "ObjectParameter_InvalidParameterName";

		// Token: 0x040021A7 RID: 8615
		internal const string ObjectParameter_InvalidParameterType = "ObjectParameter_InvalidParameterType";

		// Token: 0x040021A8 RID: 8616
		internal const string ObjectParameterCollection_ParameterNameNotFound = "ObjectParameterCollection_ParameterNameNotFound";

		// Token: 0x040021A9 RID: 8617
		internal const string ObjectParameterCollection_ParameterAlreadyExists = "ObjectParameterCollection_ParameterAlreadyExists";

		// Token: 0x040021AA RID: 8618
		internal const string ObjectParameterCollection_DuplicateParameterName = "ObjectParameterCollection_DuplicateParameterName";

		// Token: 0x040021AB RID: 8619
		internal const string ObjectParameterCollection_ParametersLocked = "ObjectParameterCollection_ParametersLocked";

		// Token: 0x040021AC RID: 8620
		internal const string ProviderReturnedNullForGetDbInformation = "ProviderReturnedNullForGetDbInformation";

		// Token: 0x040021AD RID: 8621
		internal const string ProviderReturnedNullForCreateCommandDefinition = "ProviderReturnedNullForCreateCommandDefinition";

		// Token: 0x040021AE RID: 8622
		internal const string ProviderDidNotReturnAProviderManifest = "ProviderDidNotReturnAProviderManifest";

		// Token: 0x040021AF RID: 8623
		internal const string ProviderDidNotReturnAProviderManifestToken = "ProviderDidNotReturnAProviderManifestToken";

		// Token: 0x040021B0 RID: 8624
		internal const string ProviderDidNotReturnSpatialServices = "ProviderDidNotReturnSpatialServices";

		// Token: 0x040021B1 RID: 8625
		internal const string SpatialProviderNotUsable = "SpatialProviderNotUsable";

		// Token: 0x040021B2 RID: 8626
		internal const string ProviderRequiresStoreCommandTree = "ProviderRequiresStoreCommandTree";

		// Token: 0x040021B3 RID: 8627
		internal const string ProviderShouldOverrideEscapeLikeArgument = "ProviderShouldOverrideEscapeLikeArgument";

		// Token: 0x040021B4 RID: 8628
		internal const string ProviderEscapeLikeArgumentReturnedNull = "ProviderEscapeLikeArgumentReturnedNull";

		// Token: 0x040021B5 RID: 8629
		internal const string ProviderDidNotCreateACommandDefinition = "ProviderDidNotCreateACommandDefinition";

		// Token: 0x040021B6 RID: 8630
		internal const string ProviderDoesNotSupportCreateDatabaseScript = "ProviderDoesNotSupportCreateDatabaseScript";

		// Token: 0x040021B7 RID: 8631
		internal const string ProviderDoesNotSupportCreateDatabase = "ProviderDoesNotSupportCreateDatabase";

		// Token: 0x040021B8 RID: 8632
		internal const string ProviderDoesNotSupportDatabaseExists = "ProviderDoesNotSupportDatabaseExists";

		// Token: 0x040021B9 RID: 8633
		internal const string ProviderDoesNotSupportDeleteDatabase = "ProviderDoesNotSupportDeleteDatabase";

		// Token: 0x040021BA RID: 8634
		internal const string Spatial_GeographyValueNotCompatibleWithSpatialServices = "Spatial_GeographyValueNotCompatibleWithSpatialServices";

		// Token: 0x040021BB RID: 8635
		internal const string Spatial_GeometryValueNotCompatibleWithSpatialServices = "Spatial_GeometryValueNotCompatibleWithSpatialServices";

		// Token: 0x040021BC RID: 8636
		internal const string Spatial_ProviderValueNotCompatibleWithSpatialServices = "Spatial_ProviderValueNotCompatibleWithSpatialServices";

		// Token: 0x040021BD RID: 8637
		internal const string Spatial_WellKnownValueSerializationPropertyNotDirectlySettable = "Spatial_WellKnownValueSerializationPropertyNotDirectlySettable";

		// Token: 0x040021BE RID: 8638
		internal const string EntityConnectionString_Name = "EntityConnectionString_Name";

		// Token: 0x040021BF RID: 8639
		internal const string EntityConnectionString_Provider = "EntityConnectionString_Provider";

		// Token: 0x040021C0 RID: 8640
		internal const string EntityConnectionString_Metadata = "EntityConnectionString_Metadata";

		// Token: 0x040021C1 RID: 8641
		internal const string EntityConnectionString_ProviderConnectionString = "EntityConnectionString_ProviderConnectionString";

		// Token: 0x040021C2 RID: 8642
		internal const string EntityDataCategory_Context = "EntityDataCategory_Context";

		// Token: 0x040021C3 RID: 8643
		internal const string EntityDataCategory_NamedConnectionString = "EntityDataCategory_NamedConnectionString";

		// Token: 0x040021C4 RID: 8644
		internal const string EntityDataCategory_Source = "EntityDataCategory_Source";

		// Token: 0x040021C5 RID: 8645
		internal const string ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection = "ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection";

		// Token: 0x040021C6 RID: 8646
		internal const string ObjectQuery_Span_NoNavProp = "ObjectQuery_Span_NoNavProp";

		// Token: 0x040021C7 RID: 8647
		internal const string ObjectQuery_Span_SpanPathSyntaxError = "ObjectQuery_Span_SpanPathSyntaxError";

		// Token: 0x040021C8 RID: 8648
		internal const string EntityProxyTypeInfo_ProxyHasWrongWrapper = "EntityProxyTypeInfo_ProxyHasWrongWrapper";

		// Token: 0x040021C9 RID: 8649
		internal const string EntityProxyTypeInfo_CannotSetEntityCollectionProperty = "EntityProxyTypeInfo_CannotSetEntityCollectionProperty";

		// Token: 0x040021CA RID: 8650
		internal const string EntityProxyTypeInfo_ProxyMetadataIsUnavailable = "EntityProxyTypeInfo_ProxyMetadataIsUnavailable";

		// Token: 0x040021CB RID: 8651
		internal const string EntityProxyTypeInfo_DuplicateOSpaceType = "EntityProxyTypeInfo_DuplicateOSpaceType";

		// Token: 0x040021CC RID: 8652
		internal const string InvalidEdmMemberInstance = "InvalidEdmMemberInstance";

		// Token: 0x040021CD RID: 8653
		internal const string EF6Providers_NoProviderFound = "EF6Providers_NoProviderFound";

		// Token: 0x040021CE RID: 8654
		internal const string EF6Providers_ProviderTypeMissing = "EF6Providers_ProviderTypeMissing";

		// Token: 0x040021CF RID: 8655
		internal const string EF6Providers_InstanceMissing = "EF6Providers_InstanceMissing";

		// Token: 0x040021D0 RID: 8656
		internal const string EF6Providers_NotDbProviderServices = "EF6Providers_NotDbProviderServices";

		// Token: 0x040021D1 RID: 8657
		internal const string ProviderInvariantRepeatedInConfig = "ProviderInvariantRepeatedInConfig";

		// Token: 0x040021D2 RID: 8658
		internal const string DbDependencyResolver_NoProviderInvariantName = "DbDependencyResolver_NoProviderInvariantName";

		// Token: 0x040021D3 RID: 8659
		internal const string DbDependencyResolver_InvalidKey = "DbDependencyResolver_InvalidKey";

		// Token: 0x040021D4 RID: 8660
		internal const string DefaultConfigurationUsedBeforeSet = "DefaultConfigurationUsedBeforeSet";

		// Token: 0x040021D5 RID: 8661
		internal const string AddHandlerToInUseConfiguration = "AddHandlerToInUseConfiguration";

		// Token: 0x040021D6 RID: 8662
		internal const string ConfigurationSetTwice = "ConfigurationSetTwice";

		// Token: 0x040021D7 RID: 8663
		internal const string ConfigurationNotDiscovered = "ConfigurationNotDiscovered";

		// Token: 0x040021D8 RID: 8664
		internal const string SetConfigurationNotDiscovered = "SetConfigurationNotDiscovered";

		// Token: 0x040021D9 RID: 8665
		internal const string MultipleConfigsInAssembly = "MultipleConfigsInAssembly";

		// Token: 0x040021DA RID: 8666
		internal const string CreateInstance_BadMigrationsConfigurationType = "CreateInstance_BadMigrationsConfigurationType";

		// Token: 0x040021DB RID: 8667
		internal const string CreateInstance_BadSqlGeneratorType = "CreateInstance_BadSqlGeneratorType";

		// Token: 0x040021DC RID: 8668
		internal const string CreateInstance_BadDbConfigurationType = "CreateInstance_BadDbConfigurationType";

		// Token: 0x040021DD RID: 8669
		internal const string DbConfigurationTypeNotFound = "DbConfigurationTypeNotFound";

		// Token: 0x040021DE RID: 8670
		internal const string DbConfigurationTypeInAttributeNotFound = "DbConfigurationTypeInAttributeNotFound";

		// Token: 0x040021DF RID: 8671
		internal const string CreateInstance_NoParameterlessConstructor = "CreateInstance_NoParameterlessConstructor";

		// Token: 0x040021E0 RID: 8672
		internal const string CreateInstance_AbstractType = "CreateInstance_AbstractType";

		// Token: 0x040021E1 RID: 8673
		internal const string CreateInstance_GenericType = "CreateInstance_GenericType";

		// Token: 0x040021E2 RID: 8674
		internal const string ConfigurationLocked = "ConfigurationLocked";

		// Token: 0x040021E3 RID: 8675
		internal const string EnableMigrationsForContext = "EnableMigrationsForContext";

		// Token: 0x040021E4 RID: 8676
		internal const string EnableMigrations_MultipleContexts = "EnableMigrations_MultipleContexts";

		// Token: 0x040021E5 RID: 8677
		internal const string EnableMigrations_MultipleContextsWithName = "EnableMigrations_MultipleContextsWithName";

		// Token: 0x040021E6 RID: 8678
		internal const string EnableMigrations_NoContext = "EnableMigrations_NoContext";

		// Token: 0x040021E7 RID: 8679
		internal const string EnableMigrations_NoContextWithName = "EnableMigrations_NoContextWithName";

		// Token: 0x040021E8 RID: 8680
		internal const string MoreThanOneElement = "MoreThanOneElement";

		// Token: 0x040021E9 RID: 8681
		internal const string IQueryable_Not_Async = "IQueryable_Not_Async";

		// Token: 0x040021EA RID: 8682
		internal const string IQueryable_Provider_Not_Async = "IQueryable_Provider_Not_Async";

		// Token: 0x040021EB RID: 8683
		internal const string EmptySequence = "EmptySequence";

		// Token: 0x040021EC RID: 8684
		internal const string UnableToMoveHistoryTableWithAuto = "UnableToMoveHistoryTableWithAuto";

		// Token: 0x040021ED RID: 8685
		internal const string NoMatch = "NoMatch";

		// Token: 0x040021EE RID: 8686
		internal const string MoreThanOneMatch = "MoreThanOneMatch";

		// Token: 0x040021EF RID: 8687
		internal const string CreateConfigurationType_NoParameterlessConstructor = "CreateConfigurationType_NoParameterlessConstructor";

		// Token: 0x040021F0 RID: 8688
		internal const string CollectionEmpty = "CollectionEmpty";

		// Token: 0x040021F1 RID: 8689
		internal const string DbMigrationsConfiguration_ContextType = "DbMigrationsConfiguration_ContextType";

		// Token: 0x040021F2 RID: 8690
		internal const string ContextFactoryContextType = "ContextFactoryContextType";

		// Token: 0x040021F3 RID: 8691
		internal const string DbMigrationsConfiguration_RootedPath = "DbMigrationsConfiguration_RootedPath";

		// Token: 0x040021F4 RID: 8692
		internal const string ModelBuilder_PropertyFilterTypeMustBePrimitive = "ModelBuilder_PropertyFilterTypeMustBePrimitive";

		// Token: 0x040021F5 RID: 8693
		internal const string LightweightEntityConfiguration_NonScalarProperty = "LightweightEntityConfiguration_NonScalarProperty";

		// Token: 0x040021F6 RID: 8694
		internal const string MigrationsPendingException = "MigrationsPendingException";

		// Token: 0x040021F7 RID: 8695
		internal const string ExecutionStrategy_ExistingTransaction = "ExecutionStrategy_ExistingTransaction";

		// Token: 0x040021F8 RID: 8696
		internal const string ExecutionStrategy_MinimumMustBeLessThanMaximum = "ExecutionStrategy_MinimumMustBeLessThanMaximum";

		// Token: 0x040021F9 RID: 8697
		internal const string ExecutionStrategy_NegativeDelay = "ExecutionStrategy_NegativeDelay";

		// Token: 0x040021FA RID: 8698
		internal const string ExecutionStrategy_RetryLimitExceeded = "ExecutionStrategy_RetryLimitExceeded";

		// Token: 0x040021FB RID: 8699
		internal const string BaseTypeNotMappedToFunctions = "BaseTypeNotMappedToFunctions";

		// Token: 0x040021FC RID: 8700
		internal const string InvalidResourceName = "InvalidResourceName";

		// Token: 0x040021FD RID: 8701
		internal const string ModificationFunctionParameterNotFound = "ModificationFunctionParameterNotFound";

		// Token: 0x040021FE RID: 8702
		internal const string EntityClient_CannotOpenBrokenConnection = "EntityClient_CannotOpenBrokenConnection";

		// Token: 0x040021FF RID: 8703
		internal const string ModificationFunctionParameterNotFoundOriginal = "ModificationFunctionParameterNotFoundOriginal";

		// Token: 0x04002200 RID: 8704
		internal const string ResultBindingNotFound = "ResultBindingNotFound";

		// Token: 0x04002201 RID: 8705
		internal const string ConflictingFunctionsMapping = "ConflictingFunctionsMapping";

		// Token: 0x04002202 RID: 8706
		internal const string DbContext_InvalidTransactionForConnection = "DbContext_InvalidTransactionForConnection";

		// Token: 0x04002203 RID: 8707
		internal const string DbContext_InvalidTransactionNoConnection = "DbContext_InvalidTransactionNoConnection";

		// Token: 0x04002204 RID: 8708
		internal const string DbContext_TransactionAlreadyStarted = "DbContext_TransactionAlreadyStarted";

		// Token: 0x04002205 RID: 8709
		internal const string DbContext_TransactionAlreadyEnlistedInUserTransaction = "DbContext_TransactionAlreadyEnlistedInUserTransaction";

		// Token: 0x04002206 RID: 8710
		internal const string ExecutionStrategy_StreamingNotSupported = "ExecutionStrategy_StreamingNotSupported";

		// Token: 0x04002207 RID: 8711
		internal const string EdmProperty_InvalidPropertyType = "EdmProperty_InvalidPropertyType";

		// Token: 0x04002208 RID: 8712
		internal const string ConcurrentMethodInvocation = "ConcurrentMethodInvocation";

		// Token: 0x04002209 RID: 8713
		internal const string AssociationSet_EndEntityTypeMismatch = "AssociationSet_EndEntityTypeMismatch";

		// Token: 0x0400220A RID: 8714
		internal const string VisitDbInExpressionNotImplemented = "VisitDbInExpressionNotImplemented";

		// Token: 0x0400220B RID: 8715
		internal const string InvalidColumnBuilderArgument = "InvalidColumnBuilderArgument";

		// Token: 0x0400220C RID: 8716
		internal const string StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed = "StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed";

		// Token: 0x0400220D RID: 8717
		internal const string StorageComplexPropertyMapping_OnlyComplexPropertyAllowed = "StorageComplexPropertyMapping_OnlyComplexPropertyAllowed";

		// Token: 0x0400220E RID: 8718
		internal const string MetadataItemErrorsFoundDuringGeneration = "MetadataItemErrorsFoundDuringGeneration";

		// Token: 0x0400220F RID: 8719
		internal const string AutomaticStaleFunctions = "AutomaticStaleFunctions";

		// Token: 0x04002210 RID: 8720
		internal const string ScaffoldSprocInDownNotSupported = "ScaffoldSprocInDownNotSupported";

		// Token: 0x04002211 RID: 8721
		internal const string LightweightEntityConfiguration_ConfigurationConflict_ComplexType = "LightweightEntityConfiguration_ConfigurationConflict_ComplexType";

		// Token: 0x04002212 RID: 8722
		internal const string LightweightEntityConfiguration_ConfigurationConflict_IgnoreType = "LightweightEntityConfiguration_ConfigurationConflict_IgnoreType";

		// Token: 0x04002213 RID: 8723
		internal const string AttemptToAddEdmMemberFromWrongDataSpace = "AttemptToAddEdmMemberFromWrongDataSpace";

		// Token: 0x04002214 RID: 8724
		internal const string LightweightEntityConfiguration_InvalidNavigationProperty = "LightweightEntityConfiguration_InvalidNavigationProperty";

		// Token: 0x04002215 RID: 8725
		internal const string LightweightEntityConfiguration_InvalidInverseNavigationProperty = "LightweightEntityConfiguration_InvalidInverseNavigationProperty";

		// Token: 0x04002216 RID: 8726
		internal const string LightweightEntityConfiguration_MismatchedInverseNavigationProperty = "LightweightEntityConfiguration_MismatchedInverseNavigationProperty";

		// Token: 0x04002217 RID: 8727
		internal const string DuplicateParameterName = "DuplicateParameterName";

		// Token: 0x04002218 RID: 8728
		internal const string CommandLogFailed = "CommandLogFailed";

		// Token: 0x04002219 RID: 8729
		internal const string CommandLogCanceled = "CommandLogCanceled";

		// Token: 0x0400221A RID: 8730
		internal const string CommandLogComplete = "CommandLogComplete";

		// Token: 0x0400221B RID: 8731
		internal const string CommandLogAsync = "CommandLogAsync";

		// Token: 0x0400221C RID: 8732
		internal const string CommandLogNonAsync = "CommandLogNonAsync";

		// Token: 0x0400221D RID: 8733
		internal const string SuppressionAfterExecution = "SuppressionAfterExecution";

		// Token: 0x0400221E RID: 8734
		internal const string BadContextTypeForDiscovery = "BadContextTypeForDiscovery";

		// Token: 0x0400221F RID: 8735
		internal const string ErrorGeneratingCommandTree = "ErrorGeneratingCommandTree";

		// Token: 0x04002220 RID: 8736
		internal const string LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity = "LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity";

		// Token: 0x04002221 RID: 8737
		internal const string LightweightNavigationPropertyConfiguration_InvalidMultiplicity = "LightweightNavigationPropertyConfiguration_InvalidMultiplicity";

		// Token: 0x04002222 RID: 8738
		internal const string LightweightPrimitivePropertyConfiguration_NonNullableProperty = "LightweightPrimitivePropertyConfiguration_NonNullableProperty";

		// Token: 0x04002223 RID: 8739
		internal const string TestDoubleNotImplemented = "TestDoubleNotImplemented";

		// Token: 0x04002224 RID: 8740
		internal const string TestDoublesCannotBeConverted = "TestDoublesCannotBeConverted";

		// Token: 0x04002225 RID: 8741
		internal const string InvalidNavigationPropertyComplexType = "InvalidNavigationPropertyComplexType";

		// Token: 0x04002226 RID: 8742
		internal const string ConventionsConfiguration_InvalidConventionType = "ConventionsConfiguration_InvalidConventionType";

		// Token: 0x04002227 RID: 8743
		internal const string ConventionsConfiguration_ConventionTypeMissmatch = "ConventionsConfiguration_ConventionTypeMissmatch";

		// Token: 0x04002228 RID: 8744
		internal const string LightweightPrimitivePropertyConfiguration_DateTimeScale = "LightweightPrimitivePropertyConfiguration_DateTimeScale";

		// Token: 0x04002229 RID: 8745
		internal const string LightweightPrimitivePropertyConfiguration_DecimalNoScale = "LightweightPrimitivePropertyConfiguration_DecimalNoScale";

		// Token: 0x0400222A RID: 8746
		internal const string LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime = "LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime";

		// Token: 0x0400222B RID: 8747
		internal const string LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal = "LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal";

		// Token: 0x0400222C RID: 8748
		internal const string LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary = "LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary";

		// Token: 0x0400222D RID: 8749
		internal const string LightweightPrimitivePropertyConfiguration_IsUnicodeNonString = "LightweightPrimitivePropertyConfiguration_IsUnicodeNonString";

		// Token: 0x0400222E RID: 8750
		internal const string LightweightPrimitivePropertyConfiguration_NonLength = "LightweightPrimitivePropertyConfiguration_NonLength";

		// Token: 0x0400222F RID: 8751
		internal const string UnableToUpgradeHistoryWhenCustomFactory = "UnableToUpgradeHistoryWhenCustomFactory";

		// Token: 0x04002230 RID: 8752
		internal const string CommitFailed = "CommitFailed";

		// Token: 0x04002231 RID: 8753
		internal const string InterceptorTypeNotFound = "InterceptorTypeNotFound";

		// Token: 0x04002232 RID: 8754
		internal const string InterceptorTypeNotInterceptor = "InterceptorTypeNotInterceptor";

		// Token: 0x04002233 RID: 8755
		internal const string ViewGenContainersNotFound = "ViewGenContainersNotFound";

		// Token: 0x04002234 RID: 8756
		internal const string HashCalcContainersNotFound = "HashCalcContainersNotFound";

		// Token: 0x04002235 RID: 8757
		internal const string ViewGenMultipleContainers = "ViewGenMultipleContainers";

		// Token: 0x04002236 RID: 8758
		internal const string HashCalcMultipleContainers = "HashCalcMultipleContainers";

		// Token: 0x04002237 RID: 8759
		internal const string BadConnectionWrapping = "BadConnectionWrapping";

		// Token: 0x04002238 RID: 8760
		internal const string ConnectionClosedLog = "ConnectionClosedLog";

		// Token: 0x04002239 RID: 8761
		internal const string ConnectionCloseErrorLog = "ConnectionCloseErrorLog";

		// Token: 0x0400223A RID: 8762
		internal const string ConnectionOpenedLog = "ConnectionOpenedLog";

		// Token: 0x0400223B RID: 8763
		internal const string ConnectionOpenErrorLog = "ConnectionOpenErrorLog";

		// Token: 0x0400223C RID: 8764
		internal const string ConnectionOpenedLogAsync = "ConnectionOpenedLogAsync";

		// Token: 0x0400223D RID: 8765
		internal const string ConnectionOpenErrorLogAsync = "ConnectionOpenErrorLogAsync";

		// Token: 0x0400223E RID: 8766
		internal const string TransactionStartedLog = "TransactionStartedLog";

		// Token: 0x0400223F RID: 8767
		internal const string TransactionStartErrorLog = "TransactionStartErrorLog";

		// Token: 0x04002240 RID: 8768
		internal const string TransactionCommittedLog = "TransactionCommittedLog";

		// Token: 0x04002241 RID: 8769
		internal const string TransactionCommitErrorLog = "TransactionCommitErrorLog";

		// Token: 0x04002242 RID: 8770
		internal const string TransactionRolledBackLog = "TransactionRolledBackLog";

		// Token: 0x04002243 RID: 8771
		internal const string TransactionRollbackErrorLog = "TransactionRollbackErrorLog";

		// Token: 0x04002244 RID: 8772
		internal const string ConnectionOpenCanceledLog = "ConnectionOpenCanceledLog";

		// Token: 0x04002245 RID: 8773
		internal const string TransactionHandler_AlreadyInitialized = "TransactionHandler_AlreadyInitialized";

		// Token: 0x04002246 RID: 8774
		internal const string ConnectionDisposedLog = "ConnectionDisposedLog";

		// Token: 0x04002247 RID: 8775
		internal const string TransactionDisposedLog = "TransactionDisposedLog";

		// Token: 0x04002248 RID: 8776
		internal const string UnableToLoadEmbeddedResource = "UnableToLoadEmbeddedResource";

		// Token: 0x04002249 RID: 8777
		internal const string CannotSetBaseTypeCyclicInheritance = "CannotSetBaseTypeCyclicInheritance";

		// Token: 0x0400224A RID: 8778
		internal const string CannotDefineKeysOnBothBaseAndDerivedTypes = "CannotDefineKeysOnBothBaseAndDerivedTypes";

		// Token: 0x0400224B RID: 8779
		private static EntityRes loader;

		// Token: 0x0400224C RID: 8780
		private readonly ResourceManager resources;
	}
}
