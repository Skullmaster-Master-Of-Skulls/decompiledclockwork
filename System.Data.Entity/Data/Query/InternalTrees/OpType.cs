using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000BA RID: 186
	internal enum OpType
	{
		// Token: 0x04000903 RID: 2307
		Constant,
		// Token: 0x04000904 RID: 2308
		InternalConstant,
		// Token: 0x04000905 RID: 2309
		NullSentinel,
		// Token: 0x04000906 RID: 2310
		Null,
		// Token: 0x04000907 RID: 2311
		ConstantPredicate,
		// Token: 0x04000908 RID: 2312
		VarRef,
		// Token: 0x04000909 RID: 2313
		GT,
		// Token: 0x0400090A RID: 2314
		GE,
		// Token: 0x0400090B RID: 2315
		LE,
		// Token: 0x0400090C RID: 2316
		LT,
		// Token: 0x0400090D RID: 2317
		EQ,
		// Token: 0x0400090E RID: 2318
		NE,
		// Token: 0x0400090F RID: 2319
		Like,
		// Token: 0x04000910 RID: 2320
		Plus,
		// Token: 0x04000911 RID: 2321
		Minus,
		// Token: 0x04000912 RID: 2322
		Multiply,
		// Token: 0x04000913 RID: 2323
		Divide,
		// Token: 0x04000914 RID: 2324
		Modulo,
		// Token: 0x04000915 RID: 2325
		UnaryMinus,
		// Token: 0x04000916 RID: 2326
		And,
		// Token: 0x04000917 RID: 2327
		Or,
		// Token: 0x04000918 RID: 2328
		Not,
		// Token: 0x04000919 RID: 2329
		IsNull,
		// Token: 0x0400091A RID: 2330
		Case,
		// Token: 0x0400091B RID: 2331
		Treat,
		// Token: 0x0400091C RID: 2332
		IsOf,
		// Token: 0x0400091D RID: 2333
		Cast,
		// Token: 0x0400091E RID: 2334
		SoftCast,
		// Token: 0x0400091F RID: 2335
		Aggregate,
		// Token: 0x04000920 RID: 2336
		Function,
		// Token: 0x04000921 RID: 2337
		RelProperty,
		// Token: 0x04000922 RID: 2338
		Property,
		// Token: 0x04000923 RID: 2339
		NewEntity,
		// Token: 0x04000924 RID: 2340
		NewInstance,
		// Token: 0x04000925 RID: 2341
		DiscriminatedNewEntity,
		// Token: 0x04000926 RID: 2342
		NewMultiset,
		// Token: 0x04000927 RID: 2343
		NewRecord,
		// Token: 0x04000928 RID: 2344
		GetRefKey,
		// Token: 0x04000929 RID: 2345
		GetEntityRef,
		// Token: 0x0400092A RID: 2346
		Ref,
		// Token: 0x0400092B RID: 2347
		Exists,
		// Token: 0x0400092C RID: 2348
		Element,
		// Token: 0x0400092D RID: 2349
		Collect,
		// Token: 0x0400092E RID: 2350
		Deref,
		// Token: 0x0400092F RID: 2351
		Navigate,
		// Token: 0x04000930 RID: 2352
		ScanTable,
		// Token: 0x04000931 RID: 2353
		ScanView,
		// Token: 0x04000932 RID: 2354
		Filter,
		// Token: 0x04000933 RID: 2355
		Project,
		// Token: 0x04000934 RID: 2356
		InnerJoin,
		// Token: 0x04000935 RID: 2357
		LeftOuterJoin,
		// Token: 0x04000936 RID: 2358
		FullOuterJoin,
		// Token: 0x04000937 RID: 2359
		CrossJoin,
		// Token: 0x04000938 RID: 2360
		CrossApply,
		// Token: 0x04000939 RID: 2361
		OuterApply,
		// Token: 0x0400093A RID: 2362
		Unnest,
		// Token: 0x0400093B RID: 2363
		Sort,
		// Token: 0x0400093C RID: 2364
		ConstrainedSort,
		// Token: 0x0400093D RID: 2365
		GroupBy,
		// Token: 0x0400093E RID: 2366
		GroupByInto,
		// Token: 0x0400093F RID: 2367
		UnionAll,
		// Token: 0x04000940 RID: 2368
		Intersect,
		// Token: 0x04000941 RID: 2369
		Except,
		// Token: 0x04000942 RID: 2370
		Distinct,
		// Token: 0x04000943 RID: 2371
		SingleRow,
		// Token: 0x04000944 RID: 2372
		SingleRowTable,
		// Token: 0x04000945 RID: 2373
		VarDef,
		// Token: 0x04000946 RID: 2374
		VarDefList,
		// Token: 0x04000947 RID: 2375
		Leaf,
		// Token: 0x04000948 RID: 2376
		PhysicalProject,
		// Token: 0x04000949 RID: 2377
		SingleStreamNest,
		// Token: 0x0400094A RID: 2378
		MultiStreamNest,
		// Token: 0x0400094B RID: 2379
		MaxMarker,
		// Token: 0x0400094C RID: 2380
		NotValid = 72
	}
}
