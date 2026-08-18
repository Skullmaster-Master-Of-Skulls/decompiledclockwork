using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200060D RID: 1549
	internal enum OpType
	{
		// Token: 0x040016C3 RID: 5827
		Constant,
		// Token: 0x040016C4 RID: 5828
		InternalConstant,
		// Token: 0x040016C5 RID: 5829
		NullSentinel,
		// Token: 0x040016C6 RID: 5830
		Null,
		// Token: 0x040016C7 RID: 5831
		ConstantPredicate,
		// Token: 0x040016C8 RID: 5832
		VarRef,
		// Token: 0x040016C9 RID: 5833
		GT,
		// Token: 0x040016CA RID: 5834
		GE,
		// Token: 0x040016CB RID: 5835
		LE,
		// Token: 0x040016CC RID: 5836
		LT,
		// Token: 0x040016CD RID: 5837
		EQ,
		// Token: 0x040016CE RID: 5838
		NE,
		// Token: 0x040016CF RID: 5839
		Like,
		// Token: 0x040016D0 RID: 5840
		Plus,
		// Token: 0x040016D1 RID: 5841
		Minus,
		// Token: 0x040016D2 RID: 5842
		Multiply,
		// Token: 0x040016D3 RID: 5843
		Divide,
		// Token: 0x040016D4 RID: 5844
		Modulo,
		// Token: 0x040016D5 RID: 5845
		UnaryMinus,
		// Token: 0x040016D6 RID: 5846
		And,
		// Token: 0x040016D7 RID: 5847
		Or,
		// Token: 0x040016D8 RID: 5848
		In,
		// Token: 0x040016D9 RID: 5849
		Not,
		// Token: 0x040016DA RID: 5850
		IsNull,
		// Token: 0x040016DB RID: 5851
		Case,
		// Token: 0x040016DC RID: 5852
		Treat,
		// Token: 0x040016DD RID: 5853
		IsOf,
		// Token: 0x040016DE RID: 5854
		Cast,
		// Token: 0x040016DF RID: 5855
		SoftCast,
		// Token: 0x040016E0 RID: 5856
		Aggregate,
		// Token: 0x040016E1 RID: 5857
		Function,
		// Token: 0x040016E2 RID: 5858
		RelProperty,
		// Token: 0x040016E3 RID: 5859
		Property,
		// Token: 0x040016E4 RID: 5860
		NewEntity,
		// Token: 0x040016E5 RID: 5861
		NewInstance,
		// Token: 0x040016E6 RID: 5862
		DiscriminatedNewEntity,
		// Token: 0x040016E7 RID: 5863
		NewMultiset,
		// Token: 0x040016E8 RID: 5864
		NewRecord,
		// Token: 0x040016E9 RID: 5865
		GetRefKey,
		// Token: 0x040016EA RID: 5866
		GetEntityRef,
		// Token: 0x040016EB RID: 5867
		Ref,
		// Token: 0x040016EC RID: 5868
		Exists,
		// Token: 0x040016ED RID: 5869
		Element,
		// Token: 0x040016EE RID: 5870
		Collect,
		// Token: 0x040016EF RID: 5871
		Deref,
		// Token: 0x040016F0 RID: 5872
		Navigate,
		// Token: 0x040016F1 RID: 5873
		ScanTable,
		// Token: 0x040016F2 RID: 5874
		ScanView,
		// Token: 0x040016F3 RID: 5875
		Filter,
		// Token: 0x040016F4 RID: 5876
		Project,
		// Token: 0x040016F5 RID: 5877
		InnerJoin,
		// Token: 0x040016F6 RID: 5878
		LeftOuterJoin,
		// Token: 0x040016F7 RID: 5879
		FullOuterJoin,
		// Token: 0x040016F8 RID: 5880
		CrossJoin,
		// Token: 0x040016F9 RID: 5881
		CrossApply,
		// Token: 0x040016FA RID: 5882
		OuterApply,
		// Token: 0x040016FB RID: 5883
		Unnest,
		// Token: 0x040016FC RID: 5884
		Sort,
		// Token: 0x040016FD RID: 5885
		ConstrainedSort,
		// Token: 0x040016FE RID: 5886
		GroupBy,
		// Token: 0x040016FF RID: 5887
		GroupByInto,
		// Token: 0x04001700 RID: 5888
		UnionAll,
		// Token: 0x04001701 RID: 5889
		Intersect,
		// Token: 0x04001702 RID: 5890
		Except,
		// Token: 0x04001703 RID: 5891
		Distinct,
		// Token: 0x04001704 RID: 5892
		SingleRow,
		// Token: 0x04001705 RID: 5893
		SingleRowTable,
		// Token: 0x04001706 RID: 5894
		VarDef,
		// Token: 0x04001707 RID: 5895
		VarDefList,
		// Token: 0x04001708 RID: 5896
		Leaf,
		// Token: 0x04001709 RID: 5897
		PhysicalProject,
		// Token: 0x0400170A RID: 5898
		SingleStreamNest,
		// Token: 0x0400170B RID: 5899
		MultiStreamNest,
		// Token: 0x0400170C RID: 5900
		MaxMarker,
		// Token: 0x0400170D RID: 5901
		NotValid = 73
	}
}
