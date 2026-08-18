using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000497 RID: 1175
	internal enum QueryCompileError
	{
		// Token: 0x04002473 RID: 9331
		None,
		// Token: 0x04002474 RID: 9332
		General,
		// Token: 0x04002475 RID: 9333
		CouldNotParseExpression,
		// Token: 0x04002476 RID: 9334
		UnexpectedToken,
		// Token: 0x04002477 RID: 9335
		UnsupportedOperator,
		// Token: 0x04002478 RID: 9336
		UnsupportedAxis,
		// Token: 0x04002479 RID: 9337
		UnsupportedFunction,
		// Token: 0x0400247A RID: 9338
		UnsupportedNodeTest,
		// Token: 0x0400247B RID: 9339
		UnsupportedExpression,
		// Token: 0x0400247C RID: 9340
		AbsolutePathRequired,
		// Token: 0x0400247D RID: 9341
		InvalidNCName,
		// Token: 0x0400247E RID: 9342
		InvalidVariable,
		// Token: 0x0400247F RID: 9343
		InvalidNumber,
		// Token: 0x04002480 RID: 9344
		InvalidLiteral,
		// Token: 0x04002481 RID: 9345
		InvalidOperatorName,
		// Token: 0x04002482 RID: 9346
		InvalidNodeType,
		// Token: 0x04002483 RID: 9347
		InvalidExpression,
		// Token: 0x04002484 RID: 9348
		InvalidFunction,
		// Token: 0x04002485 RID: 9349
		InvalidLocationPath,
		// Token: 0x04002486 RID: 9350
		InvalidLocationStep,
		// Token: 0x04002487 RID: 9351
		InvalidAxisSpecifier,
		// Token: 0x04002488 RID: 9352
		InvalidNodeTest,
		// Token: 0x04002489 RID: 9353
		InvalidPredicate,
		// Token: 0x0400248A RID: 9354
		InvalidComparison,
		// Token: 0x0400248B RID: 9355
		InvalidOrdinal,
		// Token: 0x0400248C RID: 9356
		InvalidType,
		// Token: 0x0400248D RID: 9357
		InvalidTypeConversion,
		// Token: 0x0400248E RID: 9358
		NoNamespaceForPrefix,
		// Token: 0x0400248F RID: 9359
		MismatchedParen,
		// Token: 0x04002490 RID: 9360
		DuplicateOpcode,
		// Token: 0x04002491 RID: 9361
		OpcodeExists,
		// Token: 0x04002492 RID: 9362
		OpcodeNotFound,
		// Token: 0x04002493 RID: 9363
		PredicateNestingTooDeep
	}
}
