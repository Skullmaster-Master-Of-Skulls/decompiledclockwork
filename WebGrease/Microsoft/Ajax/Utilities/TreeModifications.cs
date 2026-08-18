using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200007A RID: 122
	[Flags]
	public enum TreeModifications : long
	{
		// Token: 0x040002AD RID: 685
		None = 0L,
		// Token: 0x040002AE RID: 686
		PreserveImportantComments = 1L,
		// Token: 0x040002AF RID: 687
		BracketMemberToDotMember = 2L,
		// Token: 0x040002B0 RID: 688
		NewObjectToObjectLiteral = 4L,
		// Token: 0x040002B1 RID: 689
		NewArrayToArrayLiteral = 8L,
		// Token: 0x040002B2 RID: 690
		RemoveEmptyDefaultCase = 16L,
		// Token: 0x040002B3 RID: 691
		RemoveEmptyCaseWhenNoDefault = 32L,
		// Token: 0x040002B4 RID: 692
		RemoveBreakFromLastCaseBlock = 64L,
		// Token: 0x040002B5 RID: 693
		RemoveEmptyFinally = 128L,
		// Token: 0x040002B6 RID: 694
		RemoveDuplicateVar = 256L,
		// Token: 0x040002B7 RID: 695
		CombineVarStatements = 512L,
		// Token: 0x040002B8 RID: 696
		MoveVarIntoFor = 1024L,
		// Token: 0x040002B9 RID: 697
		VarInitializeReturnToReturnInitializer = 2048L,
		// Token: 0x040002BA RID: 698
		IfEmptyToExpression = 4096L,
		// Token: 0x040002BB RID: 699
		IfConditionCallToConditionAndCall = 8192L,
		// Token: 0x040002BC RID: 700
		IfElseReturnToReturnConditional = 16384L,
		// Token: 0x040002BD RID: 701
		IfConditionReturnToCondition = 32768L,
		// Token: 0x040002BE RID: 702
		IfConditionFalseToIfNotConditionTrue = 65536L,
		// Token: 0x040002BF RID: 703
		CombineAdjacentStringLiterals = 131072L,
		// Token: 0x040002C0 RID: 704
		RemoveUnaryPlusOnNumericLiteral = 262144L,
		// Token: 0x040002C1 RID: 705
		ApplyUnaryMinusToNumericLiteral = 524288L,
		// Token: 0x040002C2 RID: 706
		MinifyStringLiterals = 1048576L,
		// Token: 0x040002C3 RID: 707
		MinifyNumericLiterals = 2097152L,
		// Token: 0x040002C4 RID: 708
		RemoveUnusedParameters = 4194304L,
		// Token: 0x040002C5 RID: 709
		StripDebugStatements = 8388608L,
		// Token: 0x040002C6 RID: 710
		LocalRenaming = 16777216L,
		// Token: 0x040002C7 RID: 711
		RemoveFunctionExpressionNames = 33554432L,
		// Token: 0x040002C8 RID: 712
		RemoveUnnecessaryLabels = 67108864L,
		// Token: 0x040002C9 RID: 713
		RemoveUnnecessaryCCOnStatements = 134217728L,
		// Token: 0x040002CA RID: 714
		DateGetTimeToUnaryPlus = 268435456L,
		// Token: 0x040002CB RID: 715
		EvaluateNumericExpressions = 536870912L,
		// Token: 0x040002CC RID: 716
		SimplifyStringToNumericConversion = 1073741824L,
		// Token: 0x040002CD RID: 717
		PropertyRenaming = 2147483648L,
		// Token: 0x040002CE RID: 718
		RemoveQuotesFromObjectLiteralNames = 8589934592L,
		// Token: 0x040002CF RID: 719
		BooleanLiteralsToNotOperators = 17179869184L,
		// Token: 0x040002D0 RID: 720
		IfExpressionsToExpression = 34359738368L,
		// Token: 0x040002D1 RID: 721
		CombineAdjacentExpressionStatements = 68719476736L,
		// Token: 0x040002D2 RID: 722
		ReduceStrictOperatorIfTypesAreSame = 137438953472L,
		// Token: 0x040002D3 RID: 723
		ReduceStrictOperatorIfTypesAreDifferent = 274877906944L,
		// Token: 0x040002D4 RID: 724
		MoveFunctionToTopOfScope = 549755813888L,
		// Token: 0x040002D5 RID: 725
		CombineVarStatementsToTopOfScope = 1099511627776L,
		// Token: 0x040002D6 RID: 726
		IfNotTrueFalseToIfFalseTrue = 2199023255552L,
		// Token: 0x040002D7 RID: 727
		MoveInExpressionsIntoForStatement = 4398046511104L,
		// Token: 0x040002D8 RID: 728
		InvertIfReturn = 8796093022208L,
		// Token: 0x040002D9 RID: 729
		CombineNestedIfs = 17592186044416L,
		// Token: 0x040002DA RID: 730
		CombineEquivalentIfReturns = 35184372088832L,
		// Token: 0x040002DB RID: 731
		ChangeWhileToFor = 70368744177664L,
		// Token: 0x040002DC RID: 732
		InvertIfContinue = 140737488355328L,
		// Token: 0x040002DD RID: 733
		EvaluateLiteralJoins = 281474976710656L,
		// Token: 0x040002DE RID: 734
		RemoveUnusedVariables = 562949953421312L,
		// Token: 0x040002DF RID: 735
		UnfoldCommaExpressionStatements = 1125899906842624L,
		// Token: 0x040002E0 RID: 736
		EvaluateLiteralLengths = 2251799813685248L,
		// Token: 0x040002E1 RID: 737
		RemoveWindowDotFromTypeOf = 4503599627370496L
	}
}
