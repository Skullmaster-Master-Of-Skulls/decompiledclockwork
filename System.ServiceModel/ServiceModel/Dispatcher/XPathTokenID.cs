using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000521 RID: 1313
	internal enum XPathTokenID
	{
		// Token: 0x04002693 RID: 9875
		Unknown,
		// Token: 0x04002694 RID: 9876
		Terminal = 268435456,
		// Token: 0x04002695 RID: 9877
		NameTest = 536870912,
		// Token: 0x04002696 RID: 9878
		NodeType = 1073741824,
		// Token: 0x04002697 RID: 9879
		Operator = 16777216,
		// Token: 0x04002698 RID: 9880
		NamedOperator = 33554432,
		// Token: 0x04002699 RID: 9881
		Function = 67108864,
		// Token: 0x0400269A RID: 9882
		Axis = 134217728,
		// Token: 0x0400269B RID: 9883
		Literal = 1048576,
		// Token: 0x0400269C RID: 9884
		Number = 2097152,
		// Token: 0x0400269D RID: 9885
		Variable = 4194304,
		// Token: 0x0400269E RID: 9886
		TypeMask = 2134900736,
		// Token: 0x0400269F RID: 9887
		LParen = 268435457,
		// Token: 0x040026A0 RID: 9888
		RParen,
		// Token: 0x040026A1 RID: 9889
		LBracket,
		// Token: 0x040026A2 RID: 9890
		RBracket,
		// Token: 0x040026A3 RID: 9891
		Period,
		// Token: 0x040026A4 RID: 9892
		DblPeriod,
		// Token: 0x040026A5 RID: 9893
		AtSign,
		// Token: 0x040026A6 RID: 9894
		Comma,
		// Token: 0x040026A7 RID: 9895
		DblColon,
		// Token: 0x040026A8 RID: 9896
		Whitespace,
		// Token: 0x040026A9 RID: 9897
		Eq = 16777227,
		// Token: 0x040026AA RID: 9898
		Neq,
		// Token: 0x040026AB RID: 9899
		Gt,
		// Token: 0x040026AC RID: 9900
		Gte,
		// Token: 0x040026AD RID: 9901
		Lt,
		// Token: 0x040026AE RID: 9902
		Lte,
		// Token: 0x040026AF RID: 9903
		Plus = 16777234,
		// Token: 0x040026B0 RID: 9904
		Minus,
		// Token: 0x040026B1 RID: 9905
		Slash,
		// Token: 0x040026B2 RID: 9906
		Multiply,
		// Token: 0x040026B3 RID: 9907
		Pipe,
		// Token: 0x040026B4 RID: 9908
		DblSlash,
		// Token: 0x040026B5 RID: 9909
		Mod = 33554456,
		// Token: 0x040026B6 RID: 9910
		And,
		// Token: 0x040026B7 RID: 9911
		Or,
		// Token: 0x040026B8 RID: 9912
		Div,
		// Token: 0x040026B9 RID: 9913
		Integer = 2097180,
		// Token: 0x040026BA RID: 9914
		Decimal,
		// Token: 0x040026BB RID: 9915
		String = 1048606,
		// Token: 0x040026BC RID: 9916
		Comment = 1073741855,
		// Token: 0x040026BD RID: 9917
		Text,
		// Token: 0x040026BE RID: 9918
		Processing,
		// Token: 0x040026BF RID: 9919
		Node,
		// Token: 0x040026C0 RID: 9920
		Wildcard = 536870947,
		// Token: 0x040026C1 RID: 9921
		NameWildcard,
		// Token: 0x040026C2 RID: 9922
		Ancestor = 134217767,
		// Token: 0x040026C3 RID: 9923
		AncestorOrSelf,
		// Token: 0x040026C4 RID: 9924
		Attribute,
		// Token: 0x040026C5 RID: 9925
		Child,
		// Token: 0x040026C6 RID: 9926
		Descendant,
		// Token: 0x040026C7 RID: 9927
		DescendantOrSelf,
		// Token: 0x040026C8 RID: 9928
		Following,
		// Token: 0x040026C9 RID: 9929
		FollowingSibling,
		// Token: 0x040026CA RID: 9930
		Namespace,
		// Token: 0x040026CB RID: 9931
		Parent,
		// Token: 0x040026CC RID: 9932
		Preceding,
		// Token: 0x040026CD RID: 9933
		PrecedingSibling,
		// Token: 0x040026CE RID: 9934
		Self
	}
}
