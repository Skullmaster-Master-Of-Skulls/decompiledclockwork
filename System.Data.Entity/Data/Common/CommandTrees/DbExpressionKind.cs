using System;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003EE RID: 1006
	public enum DbExpressionKind
	{
		// Token: 0x040017B7 RID: 6071
		All,
		// Token: 0x040017B8 RID: 6072
		And,
		// Token: 0x040017B9 RID: 6073
		Any,
		// Token: 0x040017BA RID: 6074
		Case,
		// Token: 0x040017BB RID: 6075
		Cast,
		// Token: 0x040017BC RID: 6076
		Constant,
		// Token: 0x040017BD RID: 6077
		CrossApply,
		// Token: 0x040017BE RID: 6078
		CrossJoin,
		// Token: 0x040017BF RID: 6079
		Deref,
		// Token: 0x040017C0 RID: 6080
		Distinct,
		// Token: 0x040017C1 RID: 6081
		Divide,
		// Token: 0x040017C2 RID: 6082
		Element,
		// Token: 0x040017C3 RID: 6083
		EntityRef,
		// Token: 0x040017C4 RID: 6084
		Equals,
		// Token: 0x040017C5 RID: 6085
		Except,
		// Token: 0x040017C6 RID: 6086
		Filter,
		// Token: 0x040017C7 RID: 6087
		FullOuterJoin,
		// Token: 0x040017C8 RID: 6088
		Function,
		// Token: 0x040017C9 RID: 6089
		GreaterThan,
		// Token: 0x040017CA RID: 6090
		GreaterThanOrEquals,
		// Token: 0x040017CB RID: 6091
		GroupBy,
		// Token: 0x040017CC RID: 6092
		InnerJoin,
		// Token: 0x040017CD RID: 6093
		Intersect,
		// Token: 0x040017CE RID: 6094
		IsEmpty,
		// Token: 0x040017CF RID: 6095
		IsNull,
		// Token: 0x040017D0 RID: 6096
		IsOf,
		// Token: 0x040017D1 RID: 6097
		IsOfOnly,
		// Token: 0x040017D2 RID: 6098
		Lambda = 57,
		// Token: 0x040017D3 RID: 6099
		LeftOuterJoin = 27,
		// Token: 0x040017D4 RID: 6100
		LessThan,
		// Token: 0x040017D5 RID: 6101
		LessThanOrEquals,
		// Token: 0x040017D6 RID: 6102
		Like,
		// Token: 0x040017D7 RID: 6103
		Limit,
		// Token: 0x040017D8 RID: 6104
		Minus,
		// Token: 0x040017D9 RID: 6105
		Modulo,
		// Token: 0x040017DA RID: 6106
		Multiply,
		// Token: 0x040017DB RID: 6107
		NewInstance,
		// Token: 0x040017DC RID: 6108
		Not,
		// Token: 0x040017DD RID: 6109
		NotEquals,
		// Token: 0x040017DE RID: 6110
		Null,
		// Token: 0x040017DF RID: 6111
		OfType,
		// Token: 0x040017E0 RID: 6112
		OfTypeOnly,
		// Token: 0x040017E1 RID: 6113
		Or,
		// Token: 0x040017E2 RID: 6114
		OuterApply,
		// Token: 0x040017E3 RID: 6115
		ParameterReference,
		// Token: 0x040017E4 RID: 6116
		Plus,
		// Token: 0x040017E5 RID: 6117
		Project,
		// Token: 0x040017E6 RID: 6118
		Property,
		// Token: 0x040017E7 RID: 6119
		Ref,
		// Token: 0x040017E8 RID: 6120
		RefKey,
		// Token: 0x040017E9 RID: 6121
		RelationshipNavigation,
		// Token: 0x040017EA RID: 6122
		Scan,
		// Token: 0x040017EB RID: 6123
		Skip,
		// Token: 0x040017EC RID: 6124
		Sort,
		// Token: 0x040017ED RID: 6125
		Treat,
		// Token: 0x040017EE RID: 6126
		UnaryMinus,
		// Token: 0x040017EF RID: 6127
		UnionAll,
		// Token: 0x040017F0 RID: 6128
		VariableReference
	}
}
