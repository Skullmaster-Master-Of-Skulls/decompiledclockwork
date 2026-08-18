using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200009E RID: 158
	public enum JSError
	{
		// Token: 0x04000352 RID: 850
		NoError,
		// Token: 0x04000353 RID: 851
		SyntaxError = 1002,
		// Token: 0x04000354 RID: 852
		NoColon,
		// Token: 0x04000355 RID: 853
		NoSemicolon,
		// Token: 0x04000356 RID: 854
		NoLeftParenthesis,
		// Token: 0x04000357 RID: 855
		NoRightParenthesis,
		// Token: 0x04000358 RID: 856
		NoRightBracket,
		// Token: 0x04000359 RID: 857
		NoLeftCurly,
		// Token: 0x0400035A RID: 858
		NoRightCurly,
		// Token: 0x0400035B RID: 859
		NoIdentifier,
		// Token: 0x0400035C RID: 860
		NoEqual,
		// Token: 0x0400035D RID: 861
		IllegalAssignment,
		// Token: 0x0400035E RID: 862
		RegExpSyntax,
		// Token: 0x0400035F RID: 863
		IllegalChar,
		// Token: 0x04000360 RID: 864
		UnterminatedString,
		// Token: 0x04000361 RID: 865
		NoCommentEnd,
		// Token: 0x04000362 RID: 866
		BadReturn = 1018,
		// Token: 0x04000363 RID: 867
		BadBreak,
		// Token: 0x04000364 RID: 868
		BadContinue,
		// Token: 0x04000365 RID: 869
		UnusedLabel,
		// Token: 0x04000366 RID: 870
		BadHexEscapeSequence = 1023,
		// Token: 0x04000367 RID: 871
		NoWhile,
		// Token: 0x04000368 RID: 872
		BadLabel,
		// Token: 0x04000369 RID: 873
		NoLabel,
		// Token: 0x0400036A RID: 874
		DupDefault,
		// Token: 0x0400036B RID: 875
		NoMemberIdentifier,
		// Token: 0x0400036C RID: 876
		NoCCEnd,
		// Token: 0x0400036D RID: 877
		CCOff,
		// Token: 0x0400036E RID: 878
		NoCatch = 1033,
		// Token: 0x0400036F RID: 879
		InvalidElse,
		// Token: 0x04000370 RID: 880
		NoComma = 1100,
		// Token: 0x04000371 RID: 881
		BadSwitch = 1103,
		// Token: 0x04000372 RID: 882
		CCInvalidEnd,
		// Token: 0x04000373 RID: 883
		CCInvalidElse,
		// Token: 0x04000374 RID: 884
		CCInvalidElseIf,
		// Token: 0x04000375 RID: 885
		ErrorEndOfFile,
		// Token: 0x04000376 RID: 886
		DuplicateName = 1111,
		// Token: 0x04000377 RID: 887
		UndeclaredVariable = 1135,
		// Token: 0x04000378 RID: 888
		KeywordUsedAsIdentifier = 1137,
		// Token: 0x04000379 RID: 889
		UndeclaredFunction,
		// Token: 0x0400037A RID: 890
		NoRightParenthesisOrComma = 1193,
		// Token: 0x0400037B RID: 891
		NoRightBracketOrComma,
		// Token: 0x0400037C RID: 892
		ExpressionExpected,
		// Token: 0x0400037D RID: 893
		UnexpectedSemicolon,
		// Token: 0x0400037E RID: 894
		TooManyTokensSkipped,
		// Token: 0x0400037F RID: 895
		SuspectAssignment = 1206,
		// Token: 0x04000380 RID: 896
		SuspectSemicolon,
		// Token: 0x04000381 RID: 897
		ParameterListNotLast = 1240,
		// Token: 0x04000382 RID: 898
		StatementBlockExpected = 1267,
		// Token: 0x04000383 RID: 899
		VariableDefinedNotReferenced,
		// Token: 0x04000384 RID: 900
		ArgumentNotReferenced = 1270,
		// Token: 0x04000385 RID: 901
		WithNotRecommended,
		// Token: 0x04000386 RID: 902
		FunctionNotReferenced,
		// Token: 0x04000387 RID: 903
		AmbiguousCatchVar,
		// Token: 0x04000388 RID: 904
		FunctionExpressionExpected,
		// Token: 0x04000389 RID: 905
		ObjectConstructorTakesNoArguments,
		// Token: 0x0400038A RID: 906
		JSParserException,
		// Token: 0x0400038B RID: 907
		NumericOverflow,
		// Token: 0x0400038C RID: 908
		NumericMaximum,
		// Token: 0x0400038D RID: 909
		NumericMinimum,
		// Token: 0x0400038E RID: 910
		ResourceReferenceMustBeConstant,
		// Token: 0x0400038F RID: 911
		AmbiguousNamedFunctionExpression,
		// Token: 0x04000390 RID: 912
		ConditionalCompilationTooComplex,
		// Token: 0x04000391 RID: 913
		UnterminatedAspNetBlock,
		// Token: 0x04000392 RID: 914
		MisplacedFunctionDeclaration,
		// Token: 0x04000393 RID: 915
		OctalLiteralsDeprecated,
		// Token: 0x04000394 RID: 916
		FunctionNameMustBeIdentifier,
		// Token: 0x04000395 RID: 917
		StrictComparisonIsAlwaysTrueOrFalse,
		// Token: 0x04000396 RID: 918
		StrictModeNoWith,
		// Token: 0x04000397 RID: 919
		StrictModeDuplicateArgument,
		// Token: 0x04000398 RID: 920
		StrictModeVariableName,
		// Token: 0x04000399 RID: 921
		StrictModeFunctionName,
		// Token: 0x0400039A RID: 922
		StrictModeDuplicateProperty,
		// Token: 0x0400039B RID: 923
		StrictModeInvalidAssign,
		// Token: 0x0400039C RID: 924
		StrictModeInvalidPreOrPost,
		// Token: 0x0400039D RID: 925
		StrictModeInvalidDelete,
		// Token: 0x0400039E RID: 926
		StrictModeArgumentName,
		// Token: 0x0400039F RID: 927
		DuplicateConstantDeclaration,
		// Token: 0x040003A0 RID: 928
		AssignmentToConstant,
		// Token: 0x040003A1 RID: 929
		StringNotInlineSafe,
		// Token: 0x040003A2 RID: 930
		StrictModeUndefinedVariable,
		// Token: 0x040003A3 RID: 931
		UnclosedFunction,
		// Token: 0x040003A4 RID: 932
		ObjectLiteralKeyword = 1303,
		// Token: 0x040003A5 RID: 933
		NoEndIfDirective,
		// Token: 0x040003A6 RID: 934
		NoEndDebugDirective,
		// Token: 0x040003A7 RID: 935
		BadNumericLiteral,
		// Token: 0x040003A8 RID: 936
		DuplicateLexicalDeclaration,
		// Token: 0x040003A9 RID: 937
		DuplicateCatch,
		// Token: 0x040003AA RID: 938
		SuspectEquality,
		// Token: 0x040003AB RID: 939
		SemicolonInsertion,
		// Token: 0x040003AC RID: 940
		ArrayLiteralTrailingComma,
		// Token: 0x040003AD RID: 941
		StrictModeCatchName,
		// Token: 0x040003AE RID: 942
		BindingPatternRequiresInitializer,
		// Token: 0x040003AF RID: 943
		ImplicitPropertyNameMustBeIdentifier,
		// Token: 0x040003B0 RID: 944
		SetterMustHaveOneParameter,
		// Token: 0x040003B1 RID: 945
		RestParameterNotLast,
		// Token: 0x040003B2 RID: 946
		UnableToConvertToBinding,
		// Token: 0x040003B3 RID: 947
		UnableToConvertFromBinding,
		// Token: 0x040003B4 RID: 948
		BadBindingSyntax,
		// Token: 0x040003B5 RID: 949
		MethodsNotAllowedInBindings,
		// Token: 0x040003B6 RID: 950
		NoForOrIf,
		// Token: 0x040003B7 RID: 951
		ClassElementExpected,
		// Token: 0x040003B8 RID: 952
		DuplicateClassElementName,
		// Token: 0x040003B9 RID: 953
		SpecialConstructor,
		// Token: 0x040003BA RID: 954
		StaticPrototype,
		// Token: 0x040003BB RID: 955
		NoBinding,
		// Token: 0x040003BC RID: 956
		MultipleDefaultExports,
		// Token: 0x040003BD RID: 957
		ImportNoModuleName,
		// Token: 0x040003BE RID: 958
		DuplicateModuleDeclaration,
		// Token: 0x040003BF RID: 959
		NoDefaultModuleExport,
		// Token: 0x040003C0 RID: 960
		NoModuleExport,
		// Token: 0x040003C1 RID: 961
		NoExpectedFrom,
		// Token: 0x040003C2 RID: 962
		NoStringLiteral,
		// Token: 0x040003C3 RID: 963
		NewLineNotAllowed,
		// Token: 0x040003C4 RID: 964
		NoSpecifierSet,
		// Token: 0x040003C5 RID: 965
		ExportNotAtModuleLevel,
		// Token: 0x040003C6 RID: 966
		ArrowCannotBeConstructor,
		// Token: 0x040003C7 RID: 967
		HighSurrogate,
		// Token: 0x040003C8 RID: 968
		LowSurrogate,
		// Token: 0x040003C9 RID: 969
		ApplicationError = 7000,
		// Token: 0x040003CA RID: 970
		NoSource
	}
}
