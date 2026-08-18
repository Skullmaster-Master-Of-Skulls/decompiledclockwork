using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations.Infrastructure;

namespace System.Data.Entity.Resources
{
	// Token: 0x0200072E RID: 1838
	[GeneratedCode("Resources.tt", "1.0.0.0")]
	internal static class Error
	{
		// Token: 0x06005242 RID: 21058 RVA: 0x0016DDEB File Offset: 0x0016BFEB
		internal static Exception AutomaticDataLoss()
		{
			return new AutomaticDataLossException(Strings.AutomaticDataLoss);
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0016DDF7 File Offset: 0x0016BFF7
		internal static Exception MetadataOutOfDate()
		{
			return new MigrationsException(Strings.MetadataOutOfDate);
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0016DE03 File Offset: 0x0016C003
		internal static Exception MigrationNotFound(object p0)
		{
			return new MigrationsException(Strings.MigrationNotFound(p0));
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0016DE10 File Offset: 0x0016C010
		internal static Exception PartialFkOperation(object p0, object p1)
		{
			return new MigrationsException(Strings.PartialFkOperation(p0, p1));
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0016DE1E File Offset: 0x0016C01E
		internal static Exception AutoNotValidTarget(object p0)
		{
			return new MigrationsException(Strings.AutoNotValidTarget(p0));
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x0016DE2B File Offset: 0x0016C02B
		internal static Exception AutoNotValidForScriptWindows(object p0)
		{
			return new MigrationsException(Strings.AutoNotValidForScriptWindows(p0));
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x0016DE38 File Offset: 0x0016C038
		internal static Exception ContextNotConstructible(object p0)
		{
			return new MigrationsException(Strings.ContextNotConstructible(p0));
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0016DE45 File Offset: 0x0016C045
		internal static Exception AmbiguousMigrationName(object p0)
		{
			return new MigrationsException(Strings.AmbiguousMigrationName(p0));
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0016DE52 File Offset: 0x0016C052
		internal static Exception AutomaticDisabledException()
		{
			return new AutomaticMigrationsDisabledException(Strings.AutomaticDisabledException);
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0016DE5E File Offset: 0x0016C05E
		internal static Exception DownScriptWindowsNotSupported()
		{
			return new MigrationsException(Strings.DownScriptWindowsNotSupported);
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x0016DE6A File Offset: 0x0016C06A
		internal static Exception AssemblyMigrator_NoConfigurationWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.AssemblyMigrator_NoConfigurationWithName(p0, p1));
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x0016DE78 File Offset: 0x0016C078
		internal static Exception AssemblyMigrator_MultipleConfigurationsWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.AssemblyMigrator_MultipleConfigurationsWithName(p0, p1));
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x0016DE86 File Offset: 0x0016C086
		internal static Exception AssemblyMigrator_NoConfiguration(object p0)
		{
			return new MigrationsException(Strings.AssemblyMigrator_NoConfiguration(p0));
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x0016DE93 File Offset: 0x0016C093
		internal static Exception AssemblyMigrator_MultipleConfigurations(object p0)
		{
			return new MigrationsException(Strings.AssemblyMigrator_MultipleConfigurations(p0));
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x0016DEA0 File Offset: 0x0016C0A0
		internal static Exception MigrationsNamespaceNotUnderRootNamespace(object p0, object p1)
		{
			return new MigrationsException(Strings.MigrationsNamespaceNotUnderRootNamespace(p0, p1));
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x0016DEAE File Offset: 0x0016C0AE
		internal static Exception UnableToDispatchAddOrUpdate(object p0)
		{
			return new InvalidOperationException(Strings.UnableToDispatchAddOrUpdate(p0));
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0016DEBB File Offset: 0x0016C0BB
		internal static Exception NoSqlGeneratorForProvider(object p0)
		{
			return new MigrationsException(Strings.NoSqlGeneratorForProvider(p0));
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0016DEC8 File Offset: 0x0016C0C8
		internal static Exception EntityTypeConfigurationMismatch(object p0)
		{
			return new InvalidOperationException(Strings.EntityTypeConfigurationMismatch(p0));
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x0016DED5 File Offset: 0x0016C0D5
		internal static Exception ComplexTypeConfigurationMismatch(object p0)
		{
			return new InvalidOperationException(Strings.ComplexTypeConfigurationMismatch(p0));
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0016DEE2 File Offset: 0x0016C0E2
		internal static Exception KeyPropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.KeyPropertyNotFound(p0, p1));
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x0016DEF0 File Offset: 0x0016C0F0
		internal static Exception ForeignKeyPropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ForeignKeyPropertyNotFound(p0, p1));
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0016DEFE File Offset: 0x0016C0FE
		internal static Exception PropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.PropertyNotFound(p0, p1));
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0016DF0C File Offset: 0x0016C10C
		internal static Exception NavigationPropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.NavigationPropertyNotFound(p0, p1));
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x0016DF1A File Offset: 0x0016C11A
		internal static Exception InvalidPropertyExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidPropertyExpression(p0));
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x0016DF27 File Offset: 0x0016C127
		internal static Exception InvalidComplexPropertyExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidComplexPropertyExpression(p0));
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0016DF34 File Offset: 0x0016C134
		internal static Exception InvalidPropertiesExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidPropertiesExpression(p0));
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x0016DF41 File Offset: 0x0016C141
		internal static Exception InvalidComplexPropertiesExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidComplexPropertiesExpression(p0));
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x0016DF4E File Offset: 0x0016C14E
		internal static Exception DuplicateStructuralTypeConfiguration(object p0)
		{
			return new InvalidOperationException(Strings.DuplicateStructuralTypeConfiguration(p0));
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0016DF5B File Offset: 0x0016C15B
		internal static Exception ConflictingPropertyConfiguration(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.ConflictingPropertyConfiguration(p0, p1, p2));
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x0016DF6A File Offset: 0x0016C16A
		internal static Exception ConflictingTypeAnnotation(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.ConflictingTypeAnnotation(p0, p1, p2, p3));
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x0016DF7A File Offset: 0x0016C17A
		internal static Exception ConflictingColumnConfiguration(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.ConflictingColumnConfiguration(p0, p1, p2));
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0016DF89 File Offset: 0x0016C189
		internal static Exception CodeFirstInvalidComplexType(object p0)
		{
			return new InvalidOperationException(Strings.CodeFirstInvalidComplexType(p0));
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x0016DF96 File Offset: 0x0016C196
		internal static Exception InvalidEntityType(object p0)
		{
			return new InvalidOperationException(Strings.InvalidEntityType(p0));
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x0016DFA3 File Offset: 0x0016C1A3
		internal static Exception NavigationInverseItself(object p0, object p1)
		{
			return new InvalidOperationException(Strings.NavigationInverseItself(p0, p1));
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x0016DFB1 File Offset: 0x0016C1B1
		internal static Exception ConflictingConstraint(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingConstraint(p0, p1));
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0016DFBF File Offset: 0x0016C1BF
		internal static Exception ConflictingInferredColumnType(object p0, object p1, object p2)
		{
			return new MappingException(Strings.ConflictingInferredColumnType(p0, p1, p2));
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x0016DFCE File Offset: 0x0016C1CE
		internal static Exception ConflictingMapping(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingMapping(p0, p1));
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0016DFDC File Offset: 0x0016C1DC
		internal static Exception ConflictingCascadeDeleteOperation(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingCascadeDeleteOperation(p0, p1));
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0016DFEA File Offset: 0x0016C1EA
		internal static Exception ConflictingMultiplicities(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingMultiplicities(p0, p1));
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x0016DFF8 File Offset: 0x0016C1F8
		internal static Exception MaxLengthAttributeConvention_InvalidMaxLength(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MaxLengthAttributeConvention_InvalidMaxLength(p0, p1));
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x0016E006 File Offset: 0x0016C206
		internal static Exception StringLengthAttributeConvention_InvalidMaximumLength(object p0, object p1)
		{
			return new InvalidOperationException(Strings.StringLengthAttributeConvention_InvalidMaximumLength(p0, p1));
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x0016E014 File Offset: 0x0016C214
		internal static Exception ModelGeneration_UnableToDetermineKeyOrder(object p0)
		{
			return new InvalidOperationException(Strings.ModelGeneration_UnableToDetermineKeyOrder(p0));
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0016E021 File Offset: 0x0016C221
		internal static Exception ForeignKeyAttributeConvention_EmptyKey(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_EmptyKey(p0, p1));
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x0016E02F File Offset: 0x0016C22F
		internal static Exception ForeignKeyAttributeConvention_InvalidKey(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_InvalidKey(p0, p1, p2, p3));
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x0016E03F File Offset: 0x0016C23F
		internal static Exception ForeignKeyAttributeConvention_InvalidNavigationProperty(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_InvalidNavigationProperty(p0, p1, p2));
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x0016E04E File Offset: 0x0016C24E
		internal static Exception ForeignKeyAttributeConvention_OrderRequired(object p0)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_OrderRequired(p0));
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x0016E05B File Offset: 0x0016C25B
		internal static Exception InversePropertyAttributeConvention_PropertyNotFound(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.InversePropertyAttributeConvention_PropertyNotFound(p0, p1, p2, p3));
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x0016E06B File Offset: 0x0016C26B
		internal static Exception InversePropertyAttributeConvention_SelfInverseDetected(object p0, object p1)
		{
			return new InvalidOperationException(Strings.InversePropertyAttributeConvention_SelfInverseDetected(p0, p1));
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x0016E079 File Offset: 0x0016C279
		internal static Exception KeyRegisteredOnDerivedType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.KeyRegisteredOnDerivedType(p0, p1));
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0016E087 File Offset: 0x0016C287
		internal static Exception InvalidTableMapping(object p0, object p1)
		{
			return new InvalidOperationException(Strings.InvalidTableMapping(p0, p1));
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0016E095 File Offset: 0x0016C295
		internal static Exception InvalidTableMapping_NoTableName(object p0)
		{
			return new InvalidOperationException(Strings.InvalidTableMapping_NoTableName(p0));
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0016E0A2 File Offset: 0x0016C2A2
		internal static Exception InvalidChainedMappingSyntax(object p0)
		{
			return new InvalidOperationException(Strings.InvalidChainedMappingSyntax(p0));
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0016E0AF File Offset: 0x0016C2AF
		internal static Exception InvalidNotNullCondition(object p0, object p1)
		{
			return new InvalidOperationException(Strings.InvalidNotNullCondition(p0, p1));
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x0016E0BD File Offset: 0x0016C2BD
		internal static Exception InvalidDiscriminatorType(object p0)
		{
			return new ArgumentException(Strings.InvalidDiscriminatorType(p0));
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x0016E0CA File Offset: 0x0016C2CA
		internal static Exception ConventionNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConventionNotFound(p0, p1));
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0016E0D8 File Offset: 0x0016C2D8
		internal static Exception InvalidEntitySplittingProperties(object p0)
		{
			return new InvalidOperationException(Strings.InvalidEntitySplittingProperties(p0));
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x0016E0E5 File Offset: 0x0016C2E5
		internal static Exception InvalidDatabaseName(object p0)
		{
			return new ArgumentException(Strings.InvalidDatabaseName(p0));
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x0016E0F2 File Offset: 0x0016C2F2
		internal static Exception EntityMappingConfiguration_DuplicateMapInheritedProperties(object p0)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_DuplicateMapInheritedProperties(p0));
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0016E0FF File Offset: 0x0016C2FF
		internal static Exception EntityMappingConfiguration_DuplicateMappedProperties(object p0)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_DuplicateMappedProperties(p0));
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x0016E10C File Offset: 0x0016C30C
		internal static Exception EntityMappingConfiguration_DuplicateMappedProperty(object p0, object p1)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_DuplicateMappedProperty(p0, p1));
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x0016E11A File Offset: 0x0016C31A
		internal static Exception EntityMappingConfiguration_CannotMapIgnoredProperty(object p0, object p1)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_CannotMapIgnoredProperty(p0, p1));
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0016E128 File Offset: 0x0016C328
		internal static Exception EntityMappingConfiguration_InvalidTableSharing(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_InvalidTableSharing(p0, p1, p2));
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0016E137 File Offset: 0x0016C337
		internal static Exception EntityMappingConfiguration_TPCWithIAsOnNonLeafType(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_TPCWithIAsOnNonLeafType(p0, p1, p2));
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x0016E146 File Offset: 0x0016C346
		internal static Exception CannotIgnoreMappedBaseProperty(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.CannotIgnoreMappedBaseProperty(p0, p1, p2));
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x0016E155 File Offset: 0x0016C355
		internal static Exception ModelBuilder_KeyPropertiesMustBePrimitive(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ModelBuilder_KeyPropertiesMustBePrimitive(p0, p1));
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0016E163 File Offset: 0x0016C363
		internal static Exception TableNotFound(object p0)
		{
			return new InvalidOperationException(Strings.TableNotFound(p0));
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0016E170 File Offset: 0x0016C370
		internal static Exception IncorrectColumnCount(object p0)
		{
			return new InvalidOperationException(Strings.IncorrectColumnCount(p0));
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0016E17D File Offset: 0x0016C37D
		internal static Exception CircularComplexTypeHierarchy()
		{
			return new InvalidOperationException(Strings.CircularComplexTypeHierarchy);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x0016E189 File Offset: 0x0016C389
		internal static Exception UnableToDeterminePrincipal(object p0, object p1)
		{
			return new InvalidOperationException(Strings.UnableToDeterminePrincipal(p0, p1));
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x0016E197 File Offset: 0x0016C397
		internal static Exception UnmappedAbstractType(object p0)
		{
			return new InvalidOperationException(Strings.UnmappedAbstractType(p0));
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x0016E1A4 File Offset: 0x0016C3A4
		internal static Exception UnsupportedHybridInheritanceMapping(object p0)
		{
			return new NotSupportedException(Strings.UnsupportedHybridInheritanceMapping(p0));
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x0016E1B1 File Offset: 0x0016C3B1
		internal static Exception OrphanedConfiguredTableDetected(object p0)
		{
			return new InvalidOperationException(Strings.OrphanedConfiguredTableDetected(p0));
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x0016E1BE File Offset: 0x0016C3BE
		internal static Exception DuplicateConfiguredColumnOrder(object p0)
		{
			return new InvalidOperationException(Strings.DuplicateConfiguredColumnOrder(p0));
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0016E1CB File Offset: 0x0016C3CB
		internal static Exception UnsupportedUseOfV3Type(object p0, object p1)
		{
			return new NotSupportedException(Strings.UnsupportedUseOfV3Type(p0, p1));
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x0016E1D9 File Offset: 0x0016C3D9
		internal static Exception MultiplePropertiesMatchedAsKeys(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MultiplePropertiesMatchedAsKeys(p0, p1));
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x0016E1E7 File Offset: 0x0016C3E7
		internal static Exception DbPropertyEntry_CannotGetCurrentValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_CannotGetCurrentValue(p0, p1));
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0016E1F5 File Offset: 0x0016C3F5
		internal static Exception DbPropertyEntry_CannotSetCurrentValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_CannotSetCurrentValue(p0, p1));
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x0016E203 File Offset: 0x0016C403
		internal static Exception DbPropertyEntry_NotSupportedForDetached(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_NotSupportedForDetached(p0, p1, p2));
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x0016E212 File Offset: 0x0016C412
		internal static Exception DbPropertyEntry_SettingEntityRefNotSupported(object p0, object p1, object p2)
		{
			return new NotSupportedException(Strings.DbPropertyEntry_SettingEntityRefNotSupported(p0, p1, p2));
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x0016E221 File Offset: 0x0016C421
		internal static Exception DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(p0, p1, p2));
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0016E230 File Offset: 0x0016C430
		internal static Exception DbEntityEntry_NotSupportedForDetached(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbEntityEntry_NotSupportedForDetached(p0, p1));
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x0016E23E File Offset: 0x0016C43E
		internal static Exception DbSet_BadTypeForAddAttachRemove(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.DbSet_BadTypeForAddAttachRemove(p0, p1, p2));
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x0016E24D File Offset: 0x0016C44D
		internal static Exception DbSet_BadTypeForCreate(object p0, object p1)
		{
			return new ArgumentException(Strings.DbSet_BadTypeForCreate(p0, p1));
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x0016E25B File Offset: 0x0016C45B
		internal static Exception DbEntity_BadTypeForCast(object p0, object p1, object p2)
		{
			return new InvalidCastException(Strings.DbEntity_BadTypeForCast(p0, p1, p2));
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x0016E26A File Offset: 0x0016C46A
		internal static Exception DbMember_BadTypeForCast(object p0, object p1, object p2, object p3, object p4)
		{
			return new InvalidCastException(Strings.DbMember_BadTypeForCast(p0, p1, p2, p3, p4));
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x0016E27C File Offset: 0x0016C47C
		internal static Exception DbEntityEntry_UsedReferenceForCollectionProp(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_UsedReferenceForCollectionProp(p0, p1));
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0016E28A File Offset: 0x0016C48A
		internal static Exception DbEntityEntry_UsedCollectionForReferenceProp(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_UsedCollectionForReferenceProp(p0, p1));
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x0016E298 File Offset: 0x0016C498
		internal static Exception DbEntityEntry_NotANavigationProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotANavigationProperty(p0, p1));
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x0016E2A6 File Offset: 0x0016C4A6
		internal static Exception DbEntityEntry_NotAScalarProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotAScalarProperty(p0, p1));
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0016E2B4 File Offset: 0x0016C4B4
		internal static Exception DbEntityEntry_NotAComplexProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotAComplexProperty(p0, p1));
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x0016E2C2 File Offset: 0x0016C4C2
		internal static Exception DbEntityEntry_NotAProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotAProperty(p0, p1));
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x0016E2D0 File Offset: 0x0016C4D0
		internal static Exception DbEntityEntry_DottedPartNotComplex(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.DbEntityEntry_DottedPartNotComplex(p0, p1, p2));
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x0016E2DF File Offset: 0x0016C4DF
		internal static Exception DbEntityEntry_DottedPathMustBeProperty(object p0)
		{
			return new ArgumentException(Strings.DbEntityEntry_DottedPathMustBeProperty(p0));
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x0016E2EC File Offset: 0x0016C4EC
		internal static Exception DbEntityEntry_WrongGenericForNavProp(object p0, object p1, object p2, object p3)
		{
			return new ArgumentException(Strings.DbEntityEntry_WrongGenericForNavProp(p0, p1, p2, p3));
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x0016E2FC File Offset: 0x0016C4FC
		internal static Exception DbEntityEntry_WrongGenericForCollectionNavProp(object p0, object p1, object p2, object p3)
		{
			return new ArgumentException(Strings.DbEntityEntry_WrongGenericForCollectionNavProp(p0, p1, p2, p3));
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x0016E30C File Offset: 0x0016C50C
		internal static Exception DbEntityEntry_WrongGenericForProp(object p0, object p1, object p2, object p3)
		{
			return new ArgumentException(Strings.DbEntityEntry_WrongGenericForProp(p0, p1, p2, p3));
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x0016E31C File Offset: 0x0016C51C
		internal static Exception DbPropertyValues_CannotGetValuesForState(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotGetValuesForState(p0, p1));
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x0016E32A File Offset: 0x0016C52A
		internal static Exception DbPropertyValues_CannotSetNullValue(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotSetNullValue(p0, p1, p2));
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x0016E339 File Offset: 0x0016C539
		internal static Exception DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(p0, p1));
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x0016E347 File Offset: 0x0016C547
		internal static Exception DbPropertyValues_WrongTypeForAssignment(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_WrongTypeForAssignment(p0, p1, p2, p3));
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x0016E357 File Offset: 0x0016C557
		internal static Exception DbPropertyValues_PropertyValueNamesAreReadonly()
		{
			return new NotSupportedException(Strings.DbPropertyValues_PropertyValueNamesAreReadonly);
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0016E363 File Offset: 0x0016C563
		internal static Exception DbPropertyValues_PropertyDoesNotExist(object p0, object p1)
		{
			return new ArgumentException(Strings.DbPropertyValues_PropertyDoesNotExist(p0, p1));
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0016E371 File Offset: 0x0016C571
		internal static Exception DbPropertyValues_AttemptToSetValuesFromWrongObject(object p0, object p1)
		{
			return new ArgumentException(Strings.DbPropertyValues_AttemptToSetValuesFromWrongObject(p0, p1));
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0016E37F File Offset: 0x0016C57F
		internal static Exception DbPropertyValues_AttemptToSetValuesFromWrongType(object p0, object p1)
		{
			return new ArgumentException(Strings.DbPropertyValues_AttemptToSetValuesFromWrongType(p0, p1));
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0016E38D File Offset: 0x0016C58D
		internal static Exception DbPropertyValues_AttemptToSetNonValuesOnComplexProperty()
		{
			return new ArgumentException(Strings.DbPropertyValues_AttemptToSetNonValuesOnComplexProperty);
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0016E399 File Offset: 0x0016C599
		internal static Exception DbPropertyValues_ComplexObjectCannotBeNull(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_ComplexObjectCannotBeNull(p0, p1));
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0016E3A7 File Offset: 0x0016C5A7
		internal static Exception DbPropertyValues_NestedPropertyValuesNull(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_NestedPropertyValuesNull(p0, p1));
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x0016E3B5 File Offset: 0x0016C5B5
		internal static Exception DbPropertyValues_CannotSetPropertyOnNullCurrentValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotSetPropertyOnNullCurrentValue(p0, p1));
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0016E3C3 File Offset: 0x0016C5C3
		internal static Exception DbPropertyValues_CannotSetPropertyOnNullOriginalValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotSetPropertyOnNullOriginalValue(p0, p1));
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0016E3D1 File Offset: 0x0016C5D1
		internal static Exception DatabaseInitializationStrategy_ModelMismatch(object p0)
		{
			return new InvalidOperationException(Strings.DatabaseInitializationStrategy_ModelMismatch(p0));
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0016E3DE File Offset: 0x0016C5DE
		internal static Exception Database_DatabaseAlreadyExists(object p0)
		{
			return new InvalidOperationException(Strings.Database_DatabaseAlreadyExists(p0));
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0016E3EB File Offset: 0x0016C5EB
		internal static Exception Database_NonCodeFirstCompatibilityCheck()
		{
			return new NotSupportedException(Strings.Database_NonCodeFirstCompatibilityCheck);
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0016E3F7 File Offset: 0x0016C5F7
		internal static Exception Database_NoDatabaseMetadata()
		{
			return new NotSupportedException(Strings.Database_NoDatabaseMetadata);
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x0016E403 File Offset: 0x0016C603
		internal static Exception ContextConfiguredMultipleTimes(object p0)
		{
			return new InvalidOperationException(Strings.ContextConfiguredMultipleTimes(p0));
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x0016E410 File Offset: 0x0016C610
		internal static Exception DbContext_ContextUsedInModelCreating()
		{
			return new InvalidOperationException(Strings.DbContext_ContextUsedInModelCreating);
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0016E41C File Offset: 0x0016C61C
		internal static Exception DbContext_MESTNotSupported()
		{
			return new InvalidOperationException(Strings.DbContext_MESTNotSupported);
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x0016E428 File Offset: 0x0016C628
		internal static Exception DbContext_Disposed()
		{
			return new InvalidOperationException(Strings.DbContext_Disposed);
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0016E434 File Offset: 0x0016C634
		internal static Exception DbContext_ProviderReturnedNullConnection()
		{
			return new InvalidOperationException(Strings.DbContext_ProviderReturnedNullConnection);
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0016E440 File Offset: 0x0016C640
		internal static Exception DbContext_ProviderNameMissing(object p0)
		{
			return new InvalidOperationException(Strings.DbContext_ProviderNameMissing(p0));
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0016E44D File Offset: 0x0016C64D
		internal static Exception DbContext_ConnectionFactoryReturnedNullConnection()
		{
			return new InvalidOperationException(Strings.DbContext_ConnectionFactoryReturnedNullConnection);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0016E459 File Offset: 0x0016C659
		internal static Exception DbSet_WrongEntityTypeFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbSet_WrongEntityTypeFound(p0, p1));
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x0016E467 File Offset: 0x0016C667
		internal static Exception DbSet_MultipleAddedEntitiesFound()
		{
			return new InvalidOperationException(Strings.DbSet_MultipleAddedEntitiesFound);
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0016E473 File Offset: 0x0016C673
		internal static Exception DbSet_DbSetUsedWithComplexType(object p0)
		{
			return new InvalidOperationException(Strings.DbSet_DbSetUsedWithComplexType(p0));
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x0016E480 File Offset: 0x0016C680
		internal static Exception DbSet_PocoAndNonPocoMixedInSameAssembly(object p0)
		{
			return new InvalidOperationException(Strings.DbSet_PocoAndNonPocoMixedInSameAssembly(p0));
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x0016E48D File Offset: 0x0016C68D
		internal static Exception DbSet_EntityTypeNotInModel(object p0)
		{
			return new InvalidOperationException(Strings.DbSet_EntityTypeNotInModel(p0));
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0016E49A File Offset: 0x0016C69A
		internal static Exception DbQuery_BindingToDbQueryNotSupported()
		{
			return new NotSupportedException(Strings.DbQuery_BindingToDbQueryNotSupported);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0016E4A6 File Offset: 0x0016C6A6
		internal static Exception DbContext_ConnectionStringNotFound(object p0)
		{
			return new InvalidOperationException(Strings.DbContext_ConnectionStringNotFound(p0));
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0016E4B3 File Offset: 0x0016C6B3
		internal static Exception DbContext_ConnectionHasModel()
		{
			return new InvalidOperationException(Strings.DbContext_ConnectionHasModel);
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0016E4BF File Offset: 0x0016C6BF
		internal static Exception DbCollectionEntry_CannotSetCollectionProp(object p0, object p1)
		{
			return new NotSupportedException(Strings.DbCollectionEntry_CannotSetCollectionProp(p0, p1));
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x0016E4CD File Offset: 0x0016C6CD
		internal static Exception CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported()
		{
			return new NotSupportedException(Strings.CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported);
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x0016E4D9 File Offset: 0x0016C6D9
		internal static Exception Mapping_MESTNotSupported(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.Mapping_MESTNotSupported(p0, p1, p2));
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0016E4E8 File Offset: 0x0016C6E8
		internal static Exception DbModelBuilder_MissingRequiredCtor(object p0)
		{
			return new InvalidOperationException(Strings.DbModelBuilder_MissingRequiredCtor(p0));
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x0016E4F5 File Offset: 0x0016C6F5
		internal static Exception SqlConnectionFactory_MdfNotSupported(object p0)
		{
			return new NotSupportedException(Strings.SqlConnectionFactory_MdfNotSupported(p0));
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x0016E502 File Offset: 0x0016C702
		internal static Exception EdmxWriter_EdmxFromObjectContextNotSupported()
		{
			return new NotSupportedException(Strings.EdmxWriter_EdmxFromObjectContextNotSupported);
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0016E50E File Offset: 0x0016C70E
		internal static Exception EdmxWriter_EdmxFromModelFirstNotSupported()
		{
			return new NotSupportedException(Strings.EdmxWriter_EdmxFromModelFirstNotSupported);
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x0016E51A File Offset: 0x0016C71A
		internal static Exception DbContextServices_MissingDefaultCtor(object p0)
		{
			return new InvalidOperationException(Strings.DbContextServices_MissingDefaultCtor(p0));
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x0016E527 File Offset: 0x0016C727
		internal static Exception CannotCallGenericSetWithProxyType()
		{
			return new InvalidOperationException(Strings.CannotCallGenericSetWithProxyType);
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x0016E533 File Offset: 0x0016C733
		internal static Exception MaxLengthAttribute_InvalidMaxLength()
		{
			return new InvalidOperationException(Strings.MaxLengthAttribute_InvalidMaxLength);
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x0016E53F File Offset: 0x0016C73F
		internal static Exception MinLengthAttribute_InvalidMinLength()
		{
			return new InvalidOperationException(Strings.MinLengthAttribute_InvalidMinLength);
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x0016E54B File Offset: 0x0016C74B
		internal static Exception DbConnectionInfo_ConnectionStringNotFound(object p0)
		{
			return new InvalidOperationException(Strings.DbConnectionInfo_ConnectionStringNotFound(p0));
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x0016E558 File Offset: 0x0016C758
		internal static Exception EagerInternalContext_CannotSetConnectionInfo()
		{
			return new InvalidOperationException(Strings.EagerInternalContext_CannotSetConnectionInfo);
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0016E564 File Offset: 0x0016C764
		internal static Exception LazyInternalContext_CannotReplaceEfConnectionWithDbConnection()
		{
			return new InvalidOperationException(Strings.LazyInternalContext_CannotReplaceEfConnectionWithDbConnection);
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x0016E570 File Offset: 0x0016C770
		internal static Exception LazyInternalContext_CannotReplaceDbConnectionWithEfConnection()
		{
			return new InvalidOperationException(Strings.LazyInternalContext_CannotReplaceDbConnectionWithEfConnection);
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x0016E57C File Offset: 0x0016C77C
		internal static Exception EntityKey_UnexpectedNull()
		{
			return new InvalidOperationException(Strings.EntityKey_UnexpectedNull);
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x0016E588 File Offset: 0x0016C788
		internal static Exception EntityClient_ConnectionStringNeededBeforeOperation()
		{
			return new InvalidOperationException(Strings.EntityClient_ConnectionStringNeededBeforeOperation);
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x0016E594 File Offset: 0x0016C794
		internal static Exception EntityClient_ConnectionNotOpen()
		{
			return new InvalidOperationException(Strings.EntityClient_ConnectionNotOpen);
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x0016E5A0 File Offset: 0x0016C7A0
		internal static Exception EntityClient_NoConnectionForAdapter()
		{
			return new InvalidOperationException(Strings.EntityClient_NoConnectionForAdapter);
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0016E5AC File Offset: 0x0016C7AC
		internal static Exception EntityClient_ClosedConnectionForUpdate()
		{
			return new InvalidOperationException(Strings.EntityClient_ClosedConnectionForUpdate);
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0016E5B8 File Offset: 0x0016C7B8
		internal static Exception EntityClient_NoStoreConnectionForUpdate()
		{
			return new InvalidOperationException(Strings.EntityClient_NoStoreConnectionForUpdate);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0016E5C4 File Offset: 0x0016C7C4
		internal static Exception Mapping_Default_OCMapping_Member_Type_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
		{
			return new MappingException(Strings.Mapping_Default_OCMapping_Member_Type_Mismatch(p0, p1, p2, p3, p4, p5, p6, p7));
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0016E5DC File Offset: 0x0016C7DC
		internal static Exception ObjectStateManager_ConflictingChangesOfRelationshipDetected(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(p0, p1));
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0016E5EA File Offset: 0x0016C7EA
		internal static Exception RelatedEnd_InvalidOwnerStateForAttach()
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidOwnerStateForAttach);
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x0016E5F6 File Offset: 0x0016C7F6
		internal static Exception RelatedEnd_InvalidNthElementNullForAttach(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidNthElementNullForAttach(p0));
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0016E603 File Offset: 0x0016C803
		internal static Exception RelatedEnd_InvalidNthElementContextForAttach(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidNthElementContextForAttach(p0));
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0016E610 File Offset: 0x0016C810
		internal static Exception RelatedEnd_InvalidNthElementStateForAttach(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidNthElementStateForAttach(p0));
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0016E61D File Offset: 0x0016C81D
		internal static Exception RelatedEnd_InvalidEntityContextForAttach()
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidEntityContextForAttach);
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0016E629 File Offset: 0x0016C829
		internal static Exception RelatedEnd_InvalidEntityStateForAttach()
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidEntityStateForAttach);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0016E635 File Offset: 0x0016C835
		internal static Exception RelatedEnd_UnableToAddRelationshipWithDeletedEntity()
		{
			return new InvalidOperationException(Strings.RelatedEnd_UnableToAddRelationshipWithDeletedEntity);
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0016E641 File Offset: 0x0016C841
		internal static Exception Collections_NoRelationshipSetMatched(object p0)
		{
			return new InvalidOperationException(Strings.Collections_NoRelationshipSetMatched(p0));
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0016E64E File Offset: 0x0016C84E
		internal static Exception Collections_InvalidEntityStateSource()
		{
			return new InvalidOperationException(Strings.Collections_InvalidEntityStateSource);
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0016E65A File Offset: 0x0016C85A
		internal static Exception Collections_InvalidEntityStateLoad(object p0)
		{
			return new InvalidOperationException(Strings.Collections_InvalidEntityStateLoad(p0));
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0016E667 File Offset: 0x0016C867
		internal static Exception EntityReference_LessThanExpectedRelatedEntitiesFound()
		{
			return new InvalidOperationException(Strings.EntityReference_LessThanExpectedRelatedEntitiesFound);
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0016E673 File Offset: 0x0016C873
		internal static Exception EntityReference_MoreThanExpectedRelatedEntitiesFound()
		{
			return new InvalidOperationException(Strings.EntityReference_MoreThanExpectedRelatedEntitiesFound);
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0016E67F File Offset: 0x0016C87F
		internal static Exception EntityReference_CannotSetSpecialKeys()
		{
			return new InvalidOperationException(Strings.EntityReference_CannotSetSpecialKeys);
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0016E68B File Offset: 0x0016C88B
		internal static Exception RelatedEnd_RelatedEndNotFound()
		{
			return new InvalidOperationException(Strings.RelatedEnd_RelatedEndNotFound);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0016E697 File Offset: 0x0016C897
		internal static Exception RelatedEnd_RelatedEndNotAttachedToContext(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_RelatedEndNotAttachedToContext(p0));
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0016E6A4 File Offset: 0x0016C8A4
		internal static Exception RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd()
		{
			return new InvalidOperationException(Strings.RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd);
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0016E6B0 File Offset: 0x0016C8B0
		internal static Exception RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd()
		{
			return new InvalidOperationException(Strings.RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd);
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0016E6BC File Offset: 0x0016C8BC
		internal static Exception RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(p0));
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0016E6C9 File Offset: 0x0016C8C9
		internal static Exception RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts()
		{
			return new InvalidOperationException(Strings.RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts);
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0016E6D5 File Offset: 0x0016C8D5
		internal static Exception RelatedEnd_MismatchedMergeOptionOnLoad(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_MismatchedMergeOptionOnLoad(p0));
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0016E6E2 File Offset: 0x0016C8E2
		internal static Exception RelatedEnd_EntitySetIsNotValidForRelationship(object p0, object p1, object p2, object p3, object p4)
		{
			return new InvalidOperationException(Strings.RelatedEnd_EntitySetIsNotValidForRelationship(p0, p1, p2, p3, p4));
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0016E6F4 File Offset: 0x0016C8F4
		internal static Exception RelatedEnd_OwnerIsNull()
		{
			return new InvalidOperationException(Strings.RelatedEnd_OwnerIsNull);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0016E700 File Offset: 0x0016C900
		internal static Exception RelationshipManager_NavigationPropertyNotFound(object p0)
		{
			return new InvalidOperationException(Strings.RelationshipManager_NavigationPropertyNotFound(p0));
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0016E70D File Offset: 0x0016C90D
		internal static Exception ADP_ClosedDataReaderError()
		{
			return new InvalidOperationException(Strings.ADP_ClosedDataReaderError);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0016E719 File Offset: 0x0016C919
		internal static Exception ADP_DataReaderClosed(object p0)
		{
			return new InvalidOperationException(Strings.ADP_DataReaderClosed(p0));
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x0016E726 File Offset: 0x0016C926
		internal static Exception ADP_ImplicitlyClosedDataReaderError()
		{
			return new InvalidOperationException(Strings.ADP_ImplicitlyClosedDataReaderError);
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0016E732 File Offset: 0x0016C932
		internal static Exception ADP_NoData()
		{
			return new InvalidOperationException(Strings.ADP_NoData);
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x0016E73E File Offset: 0x0016C93E
		internal static Exception InvalidEdmMemberInstance()
		{
			return new ArgumentException(Strings.InvalidEdmMemberInstance);
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x0016E74A File Offset: 0x0016C94A
		internal static Exception EnableMigrations_MultipleContextsWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.EnableMigrations_MultipleContextsWithName(p0, p1));
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x0016E758 File Offset: 0x0016C958
		internal static Exception EnableMigrations_NoContext(object p0)
		{
			return new MigrationsException(Strings.EnableMigrations_NoContext(p0));
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0016E765 File Offset: 0x0016C965
		internal static Exception EnableMigrations_NoContextWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.EnableMigrations_NoContextWithName(p0, p1));
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x0016E773 File Offset: 0x0016C973
		internal static Exception MoreThanOneElement()
		{
			return new InvalidOperationException(Strings.MoreThanOneElement);
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0016E77F File Offset: 0x0016C97F
		internal static Exception IQueryable_Not_Async(object p0)
		{
			return new InvalidOperationException(Strings.IQueryable_Not_Async(p0));
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0016E78C File Offset: 0x0016C98C
		internal static Exception IQueryable_Provider_Not_Async()
		{
			return new InvalidOperationException(Strings.IQueryable_Provider_Not_Async);
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0016E798 File Offset: 0x0016C998
		internal static Exception EmptySequence()
		{
			return new InvalidOperationException(Strings.EmptySequence);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x0016E7A4 File Offset: 0x0016C9A4
		internal static Exception UnableToMoveHistoryTableWithAuto()
		{
			return new MigrationsException(Strings.UnableToMoveHistoryTableWithAuto);
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x0016E7B0 File Offset: 0x0016C9B0
		internal static Exception NoMatch()
		{
			return new InvalidOperationException(Strings.NoMatch);
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x0016E7BC File Offset: 0x0016C9BC
		internal static Exception MoreThanOneMatch()
		{
			return new InvalidOperationException(Strings.MoreThanOneMatch);
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x0016E7C8 File Offset: 0x0016C9C8
		internal static Exception ModelBuilder_PropertyFilterTypeMustBePrimitive(object p0)
		{
			return new InvalidOperationException(Strings.ModelBuilder_PropertyFilterTypeMustBePrimitive(p0));
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x0016E7D5 File Offset: 0x0016C9D5
		internal static Exception MigrationsPendingException(object p0)
		{
			return new MigrationsPendingException(Strings.MigrationsPendingException(p0));
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x0016E7E2 File Offset: 0x0016C9E2
		internal static Exception BaseTypeNotMappedToFunctions(object p0, object p1)
		{
			return new InvalidOperationException(Strings.BaseTypeNotMappedToFunctions(p0, p1));
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x0016E7F0 File Offset: 0x0016C9F0
		internal static Exception InvalidResourceName(object p0)
		{
			return new ArgumentException(Strings.InvalidResourceName(p0));
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x0016E7FD File Offset: 0x0016C9FD
		internal static Exception ModificationFunctionParameterNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ModificationFunctionParameterNotFound(p0, p1));
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x0016E80B File Offset: 0x0016CA0B
		internal static Exception EntityClient_CannotOpenBrokenConnection()
		{
			return new InvalidOperationException(Strings.EntityClient_CannotOpenBrokenConnection);
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0016E817 File Offset: 0x0016CA17
		internal static Exception ModificationFunctionParameterNotFoundOriginal(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ModificationFunctionParameterNotFoundOriginal(p0, p1));
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x0016E825 File Offset: 0x0016CA25
		internal static Exception ResultBindingNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ResultBindingNotFound(p0, p1));
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x0016E833 File Offset: 0x0016CA33
		internal static Exception ConflictingFunctionsMapping(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingFunctionsMapping(p0, p1));
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0016E841 File Offset: 0x0016CA41
		internal static Exception AutomaticStaleFunctions(object p0)
		{
			return new MigrationsException(Strings.AutomaticStaleFunctions(p0));
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0016E84E File Offset: 0x0016CA4E
		internal static Exception UnableToUpgradeHistoryWhenCustomFactory()
		{
			return new MigrationsException(Strings.UnableToUpgradeHistoryWhenCustomFactory);
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x0016E85A File Offset: 0x0016CA5A
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0016E862 File Offset: 0x0016CA62
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0016E869 File Offset: 0x0016CA69
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
