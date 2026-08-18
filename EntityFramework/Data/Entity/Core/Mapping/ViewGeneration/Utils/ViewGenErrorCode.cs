using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Utils
{
	// Token: 0x0200048D RID: 1165
	internal enum ViewGenErrorCode
	{
		// Token: 0x04000FCE RID: 4046
		Value = 3000,
		// Token: 0x04000FCF RID: 4047
		InvalidCondition,
		// Token: 0x04000FD0 RID: 4048
		KeyConstraintViolation,
		// Token: 0x04000FD1 RID: 4049
		KeyConstraintUpdateViolation,
		// Token: 0x04000FD2 RID: 4050
		AttributesUnrecoverable,
		// Token: 0x04000FD3 RID: 4051
		AmbiguousMultiConstants,
		// Token: 0x04000FD4 RID: 4052
		NonKeyProjectedWithOverlappingPartitions = 3007,
		// Token: 0x04000FD5 RID: 4053
		ConcurrencyDerivedClass,
		// Token: 0x04000FD6 RID: 4054
		ConcurrencyTokenHasCondition,
		// Token: 0x04000FD7 RID: 4055
		DomainConstraintViolation = 3012,
		// Token: 0x04000FD8 RID: 4056
		ForeignKeyMissingTableMapping,
		// Token: 0x04000FD9 RID: 4057
		ForeignKeyNotGuaranteedInCSpace,
		// Token: 0x04000FDA RID: 4058
		ForeignKeyMissingRelationshipMapping,
		// Token: 0x04000FDB RID: 4059
		ForeignKeyUpperBoundMustBeOne,
		// Token: 0x04000FDC RID: 4060
		ForeignKeyLowerBoundMustBeOne,
		// Token: 0x04000FDD RID: 4061
		ForeignKeyParentTableNotMappedToEnd,
		// Token: 0x04000FDE RID: 4062
		ForeignKeyColumnOrderIncorrect,
		// Token: 0x04000FDF RID: 4063
		DisjointConstraintViolation,
		// Token: 0x04000FE0 RID: 4064
		DuplicateCPropertiesMapped,
		// Token: 0x04000FE1 RID: 4065
		NotNullNoProjectedSlot,
		// Token: 0x04000FE2 RID: 4066
		NoDefaultValue,
		// Token: 0x04000FE3 RID: 4067
		KeyNotMappedForCSideExtent,
		// Token: 0x04000FE4 RID: 4068
		KeyNotMappedForTable,
		// Token: 0x04000FE5 RID: 4069
		PartitionConstraintViolation,
		// Token: 0x04000FE6 RID: 4070
		MissingExtentMapping,
		// Token: 0x04000FE7 RID: 4071
		ImpopssibleCondition = 3030,
		// Token: 0x04000FE8 RID: 4072
		NullableMappingForNonNullableColumn,
		// Token: 0x04000FE9 RID: 4073
		ErrorPatternConditionError,
		// Token: 0x04000FEA RID: 4074
		ErrorPatternSplittingError,
		// Token: 0x04000FEB RID: 4075
		ErrorPatternInvalidPartitionError,
		// Token: 0x04000FEC RID: 4076
		ErrorPatternMissingMappingError,
		// Token: 0x04000FED RID: 4077
		NoJoinKeyOrFKProvidedInMapping,
		// Token: 0x04000FEE RID: 4078
		MultipleFragmentsBetweenCandSExtentWithDistinct
	}
}
