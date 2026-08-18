using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D0 RID: 1232
	internal enum OpcodeID
	{
		// Token: 0x0400255A RID: 9562
		NoOp,
		// Token: 0x0400255B RID: 9563
		SubExpr,
		// Token: 0x0400255C RID: 9564
		Branch,
		// Token: 0x0400255D RID: 9565
		JumpIfNot,
		// Token: 0x0400255E RID: 9566
		Filter,
		// Token: 0x0400255F RID: 9567
		Function,
		// Token: 0x04002560 RID: 9568
		XsltFunction,
		// Token: 0x04002561 RID: 9569
		XsltInternalFunction,
		// Token: 0x04002562 RID: 9570
		Cast,
		// Token: 0x04002563 RID: 9571
		QueryTree,
		// Token: 0x04002564 RID: 9572
		BlockEnd,
		// Token: 0x04002565 RID: 9573
		SubRoutine,
		// Token: 0x04002566 RID: 9574
		Ordinal,
		// Token: 0x04002567 RID: 9575
		LiteralOrdinal,
		// Token: 0x04002568 RID: 9576
		Empty,
		// Token: 0x04002569 RID: 9577
		Union,
		// Token: 0x0400256A RID: 9578
		Merge,
		// Token: 0x0400256B RID: 9579
		ApplyBoolean,
		// Token: 0x0400256C RID: 9580
		StartBoolean,
		// Token: 0x0400256D RID: 9581
		EndBoolean,
		// Token: 0x0400256E RID: 9582
		Relation,
		// Token: 0x0400256F RID: 9583
		StringEquals,
		// Token: 0x04002570 RID: 9584
		NumberEquals,
		// Token: 0x04002571 RID: 9585
		StringEqualsBranch,
		// Token: 0x04002572 RID: 9586
		NumberEqualsBranch,
		// Token: 0x04002573 RID: 9587
		NumberRelation,
		// Token: 0x04002574 RID: 9588
		NumberInterval,
		// Token: 0x04002575 RID: 9589
		NumberIntervalBranch,
		// Token: 0x04002576 RID: 9590
		Select,
		// Token: 0x04002577 RID: 9591
		InitialSelect,
		// Token: 0x04002578 RID: 9592
		SelectRoot,
		// Token: 0x04002579 RID: 9593
		PushXsltVariable,
		// Token: 0x0400257A RID: 9594
		PushBool,
		// Token: 0x0400257B RID: 9595
		PushString,
		// Token: 0x0400257C RID: 9596
		PushDouble,
		// Token: 0x0400257D RID: 9597
		PushContextNode,
		// Token: 0x0400257E RID: 9598
		PushNodeSequence,
		// Token: 0x0400257F RID: 9599
		PushPosition,
		// Token: 0x04002580 RID: 9600
		PopSequenceToValueStack,
		// Token: 0x04002581 RID: 9601
		PopSequenceToSequenceStack,
		// Token: 0x04002582 RID: 9602
		PopContextNodes,
		// Token: 0x04002583 RID: 9603
		PushContextCopy,
		// Token: 0x04002584 RID: 9604
		PopValueFrame,
		// Token: 0x04002585 RID: 9605
		Plus,
		// Token: 0x04002586 RID: 9606
		Minus,
		// Token: 0x04002587 RID: 9607
		Multiply,
		// Token: 0x04002588 RID: 9608
		Divide,
		// Token: 0x04002589 RID: 9609
		Mod,
		// Token: 0x0400258A RID: 9610
		Negate,
		// Token: 0x0400258B RID: 9611
		StringPrefix,
		// Token: 0x0400258C RID: 9612
		StringPrefixBranch,
		// Token: 0x0400258D RID: 9613
		MatchAlways,
		// Token: 0x0400258E RID: 9614
		MatchResult,
		// Token: 0x0400258F RID: 9615
		MatchFilterResult,
		// Token: 0x04002590 RID: 9616
		MatchMultipleResult,
		// Token: 0x04002591 RID: 9617
		MatchSingleFx,
		// Token: 0x04002592 RID: 9618
		QuerySingleFx,
		// Token: 0x04002593 RID: 9619
		QueryResult,
		// Token: 0x04002594 RID: 9620
		QueryMultipleResult
	}
}
