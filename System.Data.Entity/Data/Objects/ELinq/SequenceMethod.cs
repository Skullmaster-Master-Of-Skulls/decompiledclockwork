using System;

namespace System.Data.Objects.ELinq
{
	// Token: 0x020001A4 RID: 420
	internal enum SequenceMethod
	{
		// Token: 0x04000C25 RID: 3109
		Where,
		// Token: 0x04000C26 RID: 3110
		WhereOrdinal,
		// Token: 0x04000C27 RID: 3111
		OfType,
		// Token: 0x04000C28 RID: 3112
		Cast,
		// Token: 0x04000C29 RID: 3113
		Select,
		// Token: 0x04000C2A RID: 3114
		SelectOrdinal,
		// Token: 0x04000C2B RID: 3115
		SelectMany,
		// Token: 0x04000C2C RID: 3116
		SelectManyOrdinal,
		// Token: 0x04000C2D RID: 3117
		SelectManyResultSelector,
		// Token: 0x04000C2E RID: 3118
		SelectManyOrdinalResultSelector,
		// Token: 0x04000C2F RID: 3119
		Join,
		// Token: 0x04000C30 RID: 3120
		JoinComparer,
		// Token: 0x04000C31 RID: 3121
		GroupJoin,
		// Token: 0x04000C32 RID: 3122
		GroupJoinComparer,
		// Token: 0x04000C33 RID: 3123
		OrderBy,
		// Token: 0x04000C34 RID: 3124
		OrderByComparer,
		// Token: 0x04000C35 RID: 3125
		OrderByDescending,
		// Token: 0x04000C36 RID: 3126
		OrderByDescendingComparer,
		// Token: 0x04000C37 RID: 3127
		ThenBy,
		// Token: 0x04000C38 RID: 3128
		ThenByComparer,
		// Token: 0x04000C39 RID: 3129
		ThenByDescending,
		// Token: 0x04000C3A RID: 3130
		ThenByDescendingComparer,
		// Token: 0x04000C3B RID: 3131
		Take,
		// Token: 0x04000C3C RID: 3132
		TakeWhile,
		// Token: 0x04000C3D RID: 3133
		TakeWhileOrdinal,
		// Token: 0x04000C3E RID: 3134
		Skip,
		// Token: 0x04000C3F RID: 3135
		SkipWhile,
		// Token: 0x04000C40 RID: 3136
		SkipWhileOrdinal,
		// Token: 0x04000C41 RID: 3137
		GroupBy,
		// Token: 0x04000C42 RID: 3138
		GroupByComparer,
		// Token: 0x04000C43 RID: 3139
		GroupByElementSelector,
		// Token: 0x04000C44 RID: 3140
		GroupByElementSelectorComparer,
		// Token: 0x04000C45 RID: 3141
		GroupByResultSelector,
		// Token: 0x04000C46 RID: 3142
		GroupByResultSelectorComparer,
		// Token: 0x04000C47 RID: 3143
		GroupByElementSelectorResultSelector,
		// Token: 0x04000C48 RID: 3144
		GroupByElementSelectorResultSelectorComparer,
		// Token: 0x04000C49 RID: 3145
		Distinct,
		// Token: 0x04000C4A RID: 3146
		DistinctComparer,
		// Token: 0x04000C4B RID: 3147
		Concat,
		// Token: 0x04000C4C RID: 3148
		Union,
		// Token: 0x04000C4D RID: 3149
		UnionComparer,
		// Token: 0x04000C4E RID: 3150
		Intersect,
		// Token: 0x04000C4F RID: 3151
		IntersectComparer,
		// Token: 0x04000C50 RID: 3152
		Except,
		// Token: 0x04000C51 RID: 3153
		ExceptComparer,
		// Token: 0x04000C52 RID: 3154
		First,
		// Token: 0x04000C53 RID: 3155
		FirstPredicate,
		// Token: 0x04000C54 RID: 3156
		FirstOrDefault,
		// Token: 0x04000C55 RID: 3157
		FirstOrDefaultPredicate,
		// Token: 0x04000C56 RID: 3158
		Last,
		// Token: 0x04000C57 RID: 3159
		LastPredicate,
		// Token: 0x04000C58 RID: 3160
		LastOrDefault,
		// Token: 0x04000C59 RID: 3161
		LastOrDefaultPredicate,
		// Token: 0x04000C5A RID: 3162
		Single,
		// Token: 0x04000C5B RID: 3163
		SinglePredicate,
		// Token: 0x04000C5C RID: 3164
		SingleOrDefault,
		// Token: 0x04000C5D RID: 3165
		SingleOrDefaultPredicate,
		// Token: 0x04000C5E RID: 3166
		ElementAt,
		// Token: 0x04000C5F RID: 3167
		ElementAtOrDefault,
		// Token: 0x04000C60 RID: 3168
		DefaultIfEmpty,
		// Token: 0x04000C61 RID: 3169
		DefaultIfEmptyValue,
		// Token: 0x04000C62 RID: 3170
		Contains,
		// Token: 0x04000C63 RID: 3171
		ContainsComparer,
		// Token: 0x04000C64 RID: 3172
		Reverse,
		// Token: 0x04000C65 RID: 3173
		Empty,
		// Token: 0x04000C66 RID: 3174
		SequenceEqual,
		// Token: 0x04000C67 RID: 3175
		SequenceEqualComparer,
		// Token: 0x04000C68 RID: 3176
		Any,
		// Token: 0x04000C69 RID: 3177
		AnyPredicate,
		// Token: 0x04000C6A RID: 3178
		All,
		// Token: 0x04000C6B RID: 3179
		Count,
		// Token: 0x04000C6C RID: 3180
		CountPredicate,
		// Token: 0x04000C6D RID: 3181
		LongCount,
		// Token: 0x04000C6E RID: 3182
		LongCountPredicate,
		// Token: 0x04000C6F RID: 3183
		Min,
		// Token: 0x04000C70 RID: 3184
		MinSelector,
		// Token: 0x04000C71 RID: 3185
		Max,
		// Token: 0x04000C72 RID: 3186
		MaxSelector,
		// Token: 0x04000C73 RID: 3187
		MinInt,
		// Token: 0x04000C74 RID: 3188
		MinNullableInt,
		// Token: 0x04000C75 RID: 3189
		MinLong,
		// Token: 0x04000C76 RID: 3190
		MinNullableLong,
		// Token: 0x04000C77 RID: 3191
		MinDouble,
		// Token: 0x04000C78 RID: 3192
		MinNullableDouble,
		// Token: 0x04000C79 RID: 3193
		MinDecimal,
		// Token: 0x04000C7A RID: 3194
		MinNullableDecimal,
		// Token: 0x04000C7B RID: 3195
		MinSingle,
		// Token: 0x04000C7C RID: 3196
		MinNullableSingle,
		// Token: 0x04000C7D RID: 3197
		MinIntSelector,
		// Token: 0x04000C7E RID: 3198
		MinNullableIntSelector,
		// Token: 0x04000C7F RID: 3199
		MinLongSelector,
		// Token: 0x04000C80 RID: 3200
		MinNullableLongSelector,
		// Token: 0x04000C81 RID: 3201
		MinDoubleSelector,
		// Token: 0x04000C82 RID: 3202
		MinNullableDoubleSelector,
		// Token: 0x04000C83 RID: 3203
		MinDecimalSelector,
		// Token: 0x04000C84 RID: 3204
		MinNullableDecimalSelector,
		// Token: 0x04000C85 RID: 3205
		MinSingleSelector,
		// Token: 0x04000C86 RID: 3206
		MinNullableSingleSelector,
		// Token: 0x04000C87 RID: 3207
		MaxInt,
		// Token: 0x04000C88 RID: 3208
		MaxNullableInt,
		// Token: 0x04000C89 RID: 3209
		MaxLong,
		// Token: 0x04000C8A RID: 3210
		MaxNullableLong,
		// Token: 0x04000C8B RID: 3211
		MaxDouble,
		// Token: 0x04000C8C RID: 3212
		MaxNullableDouble,
		// Token: 0x04000C8D RID: 3213
		MaxDecimal,
		// Token: 0x04000C8E RID: 3214
		MaxNullableDecimal,
		// Token: 0x04000C8F RID: 3215
		MaxSingle,
		// Token: 0x04000C90 RID: 3216
		MaxNullableSingle,
		// Token: 0x04000C91 RID: 3217
		MaxIntSelector,
		// Token: 0x04000C92 RID: 3218
		MaxNullableIntSelector,
		// Token: 0x04000C93 RID: 3219
		MaxLongSelector,
		// Token: 0x04000C94 RID: 3220
		MaxNullableLongSelector,
		// Token: 0x04000C95 RID: 3221
		MaxDoubleSelector,
		// Token: 0x04000C96 RID: 3222
		MaxNullableDoubleSelector,
		// Token: 0x04000C97 RID: 3223
		MaxDecimalSelector,
		// Token: 0x04000C98 RID: 3224
		MaxNullableDecimalSelector,
		// Token: 0x04000C99 RID: 3225
		MaxSingleSelector,
		// Token: 0x04000C9A RID: 3226
		MaxNullableSingleSelector,
		// Token: 0x04000C9B RID: 3227
		SumInt,
		// Token: 0x04000C9C RID: 3228
		SumNullableInt,
		// Token: 0x04000C9D RID: 3229
		SumLong,
		// Token: 0x04000C9E RID: 3230
		SumNullableLong,
		// Token: 0x04000C9F RID: 3231
		SumDouble,
		// Token: 0x04000CA0 RID: 3232
		SumNullableDouble,
		// Token: 0x04000CA1 RID: 3233
		SumDecimal,
		// Token: 0x04000CA2 RID: 3234
		SumNullableDecimal,
		// Token: 0x04000CA3 RID: 3235
		SumSingle,
		// Token: 0x04000CA4 RID: 3236
		SumNullableSingle,
		// Token: 0x04000CA5 RID: 3237
		SumIntSelector,
		// Token: 0x04000CA6 RID: 3238
		SumNullableIntSelector,
		// Token: 0x04000CA7 RID: 3239
		SumLongSelector,
		// Token: 0x04000CA8 RID: 3240
		SumNullableLongSelector,
		// Token: 0x04000CA9 RID: 3241
		SumDoubleSelector,
		// Token: 0x04000CAA RID: 3242
		SumNullableDoubleSelector,
		// Token: 0x04000CAB RID: 3243
		SumDecimalSelector,
		// Token: 0x04000CAC RID: 3244
		SumNullableDecimalSelector,
		// Token: 0x04000CAD RID: 3245
		SumSingleSelector,
		// Token: 0x04000CAE RID: 3246
		SumNullableSingleSelector,
		// Token: 0x04000CAF RID: 3247
		AverageInt,
		// Token: 0x04000CB0 RID: 3248
		AverageNullableInt,
		// Token: 0x04000CB1 RID: 3249
		AverageLong,
		// Token: 0x04000CB2 RID: 3250
		AverageNullableLong,
		// Token: 0x04000CB3 RID: 3251
		AverageDouble,
		// Token: 0x04000CB4 RID: 3252
		AverageNullableDouble,
		// Token: 0x04000CB5 RID: 3253
		AverageDecimal,
		// Token: 0x04000CB6 RID: 3254
		AverageNullableDecimal,
		// Token: 0x04000CB7 RID: 3255
		AverageSingle,
		// Token: 0x04000CB8 RID: 3256
		AverageNullableSingle,
		// Token: 0x04000CB9 RID: 3257
		AverageIntSelector,
		// Token: 0x04000CBA RID: 3258
		AverageNullableIntSelector,
		// Token: 0x04000CBB RID: 3259
		AverageLongSelector,
		// Token: 0x04000CBC RID: 3260
		AverageNullableLongSelector,
		// Token: 0x04000CBD RID: 3261
		AverageDoubleSelector,
		// Token: 0x04000CBE RID: 3262
		AverageNullableDoubleSelector,
		// Token: 0x04000CBF RID: 3263
		AverageDecimalSelector,
		// Token: 0x04000CC0 RID: 3264
		AverageNullableDecimalSelector,
		// Token: 0x04000CC1 RID: 3265
		AverageSingleSelector,
		// Token: 0x04000CC2 RID: 3266
		AverageNullableSingleSelector,
		// Token: 0x04000CC3 RID: 3267
		Aggregate,
		// Token: 0x04000CC4 RID: 3268
		AggregateSeed,
		// Token: 0x04000CC5 RID: 3269
		AggregateSeedSelector,
		// Token: 0x04000CC6 RID: 3270
		AsQueryable,
		// Token: 0x04000CC7 RID: 3271
		AsQueryableGeneric,
		// Token: 0x04000CC8 RID: 3272
		AsEnumerable,
		// Token: 0x04000CC9 RID: 3273
		Zip,
		// Token: 0x04000CCA RID: 3274
		NotSupported
	}
}
