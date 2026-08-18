using System;

namespace System.Data.Mapping.ViewGeneration.Utils
{
	// Token: 0x02000271 RID: 625
	internal enum ViewGenErrorCode
	{
		// Token: 0x04001195 RID: 4501
		Value = 3000,
		// Token: 0x04001196 RID: 4502
		InvalidCondition,
		// Token: 0x04001197 RID: 4503
		KeyConstraintViolation,
		// Token: 0x04001198 RID: 4504
		KeyConstraintUpdateViolation,
		// Token: 0x04001199 RID: 4505
		AttributesUnrecoverable,
		// Token: 0x0400119A RID: 4506
		AmbiguousMultiConstants,
		// Token: 0x0400119B RID: 4507
		NonKeyProjectedWithOverlappingPartitions = 3007,
		// Token: 0x0400119C RID: 4508
		ConcurrencyDerivedClass,
		// Token: 0x0400119D RID: 4509
		ConcurrencyTokenHasCondition,
		// Token: 0x0400119E RID: 4510
		DomainConstraintViolation = 3012,
		// Token: 0x0400119F RID: 4511
		ForeignKeyMissingTableMapping,
		// Token: 0x040011A0 RID: 4512
		ForeignKeyNotGuaranteedInCSpace,
		// Token: 0x040011A1 RID: 4513
		ForeignKeyMissingRelationshipMapping,
		// Token: 0x040011A2 RID: 4514
		ForeignKeyUpperBoundMustBeOne,
		// Token: 0x040011A3 RID: 4515
		ForeignKeyLowerBoundMustBeOne,
		// Token: 0x040011A4 RID: 4516
		ForeignKeyParentTableNotMappedToEnd,
		// Token: 0x040011A5 RID: 4517
		ForeignKeyColumnOrderIncorrect,
		// Token: 0x040011A6 RID: 4518
		DisjointConstraintViolation,
		// Token: 0x040011A7 RID: 4519
		DuplicateCPropertiesMapped,
		// Token: 0x040011A8 RID: 4520
		NotNullNoProjectedSlot,
		// Token: 0x040011A9 RID: 4521
		NoDefaultValue,
		// Token: 0x040011AA RID: 4522
		KeyNotMappedForCSideExtent,
		// Token: 0x040011AB RID: 4523
		KeyNotMappedForTable,
		// Token: 0x040011AC RID: 4524
		PartitionConstraintViolation,
		// Token: 0x040011AD RID: 4525
		MissingExtentMapping,
		// Token: 0x040011AE RID: 4526
		ImpopssibleCondition = 3030,
		// Token: 0x040011AF RID: 4527
		NullableMappingForNonNullableColumn,
		// Token: 0x040011B0 RID: 4528
		ErrorPatternConditionError,
		// Token: 0x040011B1 RID: 4529
		ErrorPatternSplittingError,
		// Token: 0x040011B2 RID: 4530
		ErrorPatternInvalidPartitionError,
		// Token: 0x040011B3 RID: 4531
		ErrorPatternMissingMappingError,
		// Token: 0x040011B4 RID: 4532
		NoJoinKeyOrFKProvidedInMapping,
		// Token: 0x040011B5 RID: 4533
		MultipleFragmentsBetweenCandSExtentWithDistinct
	}
}
