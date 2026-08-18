using System;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000568 RID: 1384
	internal enum SequenceMethod
	{
		// Token: 0x04001408 RID: 5128
		Where,
		// Token: 0x04001409 RID: 5129
		WhereOrdinal,
		// Token: 0x0400140A RID: 5130
		OfType,
		// Token: 0x0400140B RID: 5131
		Cast,
		// Token: 0x0400140C RID: 5132
		Select,
		// Token: 0x0400140D RID: 5133
		SelectOrdinal,
		// Token: 0x0400140E RID: 5134
		SelectMany,
		// Token: 0x0400140F RID: 5135
		SelectManyOrdinal,
		// Token: 0x04001410 RID: 5136
		SelectManyResultSelector,
		// Token: 0x04001411 RID: 5137
		SelectManyOrdinalResultSelector,
		// Token: 0x04001412 RID: 5138
		Join,
		// Token: 0x04001413 RID: 5139
		JoinComparer,
		// Token: 0x04001414 RID: 5140
		GroupJoin,
		// Token: 0x04001415 RID: 5141
		GroupJoinComparer,
		// Token: 0x04001416 RID: 5142
		OrderBy,
		// Token: 0x04001417 RID: 5143
		OrderByComparer,
		// Token: 0x04001418 RID: 5144
		OrderByDescending,
		// Token: 0x04001419 RID: 5145
		OrderByDescendingComparer,
		// Token: 0x0400141A RID: 5146
		ThenBy,
		// Token: 0x0400141B RID: 5147
		ThenByComparer,
		// Token: 0x0400141C RID: 5148
		ThenByDescending,
		// Token: 0x0400141D RID: 5149
		ThenByDescendingComparer,
		// Token: 0x0400141E RID: 5150
		Take,
		// Token: 0x0400141F RID: 5151
		TakeWhile,
		// Token: 0x04001420 RID: 5152
		TakeWhileOrdinal,
		// Token: 0x04001421 RID: 5153
		Skip,
		// Token: 0x04001422 RID: 5154
		SkipWhile,
		// Token: 0x04001423 RID: 5155
		SkipWhileOrdinal,
		// Token: 0x04001424 RID: 5156
		GroupBy,
		// Token: 0x04001425 RID: 5157
		GroupByComparer,
		// Token: 0x04001426 RID: 5158
		GroupByElementSelector,
		// Token: 0x04001427 RID: 5159
		GroupByElementSelectorComparer,
		// Token: 0x04001428 RID: 5160
		GroupByResultSelector,
		// Token: 0x04001429 RID: 5161
		GroupByResultSelectorComparer,
		// Token: 0x0400142A RID: 5162
		GroupByElementSelectorResultSelector,
		// Token: 0x0400142B RID: 5163
		GroupByElementSelectorResultSelectorComparer,
		// Token: 0x0400142C RID: 5164
		Distinct,
		// Token: 0x0400142D RID: 5165
		DistinctComparer,
		// Token: 0x0400142E RID: 5166
		Concat,
		// Token: 0x0400142F RID: 5167
		Union,
		// Token: 0x04001430 RID: 5168
		UnionComparer,
		// Token: 0x04001431 RID: 5169
		Intersect,
		// Token: 0x04001432 RID: 5170
		IntersectComparer,
		// Token: 0x04001433 RID: 5171
		Except,
		// Token: 0x04001434 RID: 5172
		ExceptComparer,
		// Token: 0x04001435 RID: 5173
		First,
		// Token: 0x04001436 RID: 5174
		FirstPredicate,
		// Token: 0x04001437 RID: 5175
		FirstOrDefault,
		// Token: 0x04001438 RID: 5176
		FirstOrDefaultPredicate,
		// Token: 0x04001439 RID: 5177
		Last,
		// Token: 0x0400143A RID: 5178
		LastPredicate,
		// Token: 0x0400143B RID: 5179
		LastOrDefault,
		// Token: 0x0400143C RID: 5180
		LastOrDefaultPredicate,
		// Token: 0x0400143D RID: 5181
		Single,
		// Token: 0x0400143E RID: 5182
		SinglePredicate,
		// Token: 0x0400143F RID: 5183
		SingleOrDefault,
		// Token: 0x04001440 RID: 5184
		SingleOrDefaultPredicate,
		// Token: 0x04001441 RID: 5185
		ElementAt,
		// Token: 0x04001442 RID: 5186
		ElementAtOrDefault,
		// Token: 0x04001443 RID: 5187
		DefaultIfEmpty,
		// Token: 0x04001444 RID: 5188
		DefaultIfEmptyValue,
		// Token: 0x04001445 RID: 5189
		Contains,
		// Token: 0x04001446 RID: 5190
		ContainsComparer,
		// Token: 0x04001447 RID: 5191
		Reverse,
		// Token: 0x04001448 RID: 5192
		Empty,
		// Token: 0x04001449 RID: 5193
		SequenceEqual,
		// Token: 0x0400144A RID: 5194
		SequenceEqualComparer,
		// Token: 0x0400144B RID: 5195
		Any,
		// Token: 0x0400144C RID: 5196
		AnyPredicate,
		// Token: 0x0400144D RID: 5197
		All,
		// Token: 0x0400144E RID: 5198
		Count,
		// Token: 0x0400144F RID: 5199
		CountPredicate,
		// Token: 0x04001450 RID: 5200
		LongCount,
		// Token: 0x04001451 RID: 5201
		LongCountPredicate,
		// Token: 0x04001452 RID: 5202
		Min,
		// Token: 0x04001453 RID: 5203
		MinSelector,
		// Token: 0x04001454 RID: 5204
		Max,
		// Token: 0x04001455 RID: 5205
		MaxSelector,
		// Token: 0x04001456 RID: 5206
		MinInt,
		// Token: 0x04001457 RID: 5207
		MinNullableInt,
		// Token: 0x04001458 RID: 5208
		MinLong,
		// Token: 0x04001459 RID: 5209
		MinNullableLong,
		// Token: 0x0400145A RID: 5210
		MinDouble,
		// Token: 0x0400145B RID: 5211
		MinNullableDouble,
		// Token: 0x0400145C RID: 5212
		MinDecimal,
		// Token: 0x0400145D RID: 5213
		MinNullableDecimal,
		// Token: 0x0400145E RID: 5214
		MinSingle,
		// Token: 0x0400145F RID: 5215
		MinNullableSingle,
		// Token: 0x04001460 RID: 5216
		MinIntSelector,
		// Token: 0x04001461 RID: 5217
		MinNullableIntSelector,
		// Token: 0x04001462 RID: 5218
		MinLongSelector,
		// Token: 0x04001463 RID: 5219
		MinNullableLongSelector,
		// Token: 0x04001464 RID: 5220
		MinDoubleSelector,
		// Token: 0x04001465 RID: 5221
		MinNullableDoubleSelector,
		// Token: 0x04001466 RID: 5222
		MinDecimalSelector,
		// Token: 0x04001467 RID: 5223
		MinNullableDecimalSelector,
		// Token: 0x04001468 RID: 5224
		MinSingleSelector,
		// Token: 0x04001469 RID: 5225
		MinNullableSingleSelector,
		// Token: 0x0400146A RID: 5226
		MaxInt,
		// Token: 0x0400146B RID: 5227
		MaxNullableInt,
		// Token: 0x0400146C RID: 5228
		MaxLong,
		// Token: 0x0400146D RID: 5229
		MaxNullableLong,
		// Token: 0x0400146E RID: 5230
		MaxDouble,
		// Token: 0x0400146F RID: 5231
		MaxNullableDouble,
		// Token: 0x04001470 RID: 5232
		MaxDecimal,
		// Token: 0x04001471 RID: 5233
		MaxNullableDecimal,
		// Token: 0x04001472 RID: 5234
		MaxSingle,
		// Token: 0x04001473 RID: 5235
		MaxNullableSingle,
		// Token: 0x04001474 RID: 5236
		MaxIntSelector,
		// Token: 0x04001475 RID: 5237
		MaxNullableIntSelector,
		// Token: 0x04001476 RID: 5238
		MaxLongSelector,
		// Token: 0x04001477 RID: 5239
		MaxNullableLongSelector,
		// Token: 0x04001478 RID: 5240
		MaxDoubleSelector,
		// Token: 0x04001479 RID: 5241
		MaxNullableDoubleSelector,
		// Token: 0x0400147A RID: 5242
		MaxDecimalSelector,
		// Token: 0x0400147B RID: 5243
		MaxNullableDecimalSelector,
		// Token: 0x0400147C RID: 5244
		MaxSingleSelector,
		// Token: 0x0400147D RID: 5245
		MaxNullableSingleSelector,
		// Token: 0x0400147E RID: 5246
		SumInt,
		// Token: 0x0400147F RID: 5247
		SumNullableInt,
		// Token: 0x04001480 RID: 5248
		SumLong,
		// Token: 0x04001481 RID: 5249
		SumNullableLong,
		// Token: 0x04001482 RID: 5250
		SumDouble,
		// Token: 0x04001483 RID: 5251
		SumNullableDouble,
		// Token: 0x04001484 RID: 5252
		SumDecimal,
		// Token: 0x04001485 RID: 5253
		SumNullableDecimal,
		// Token: 0x04001486 RID: 5254
		SumSingle,
		// Token: 0x04001487 RID: 5255
		SumNullableSingle,
		// Token: 0x04001488 RID: 5256
		SumIntSelector,
		// Token: 0x04001489 RID: 5257
		SumNullableIntSelector,
		// Token: 0x0400148A RID: 5258
		SumLongSelector,
		// Token: 0x0400148B RID: 5259
		SumNullableLongSelector,
		// Token: 0x0400148C RID: 5260
		SumDoubleSelector,
		// Token: 0x0400148D RID: 5261
		SumNullableDoubleSelector,
		// Token: 0x0400148E RID: 5262
		SumDecimalSelector,
		// Token: 0x0400148F RID: 5263
		SumNullableDecimalSelector,
		// Token: 0x04001490 RID: 5264
		SumSingleSelector,
		// Token: 0x04001491 RID: 5265
		SumNullableSingleSelector,
		// Token: 0x04001492 RID: 5266
		AverageInt,
		// Token: 0x04001493 RID: 5267
		AverageNullableInt,
		// Token: 0x04001494 RID: 5268
		AverageLong,
		// Token: 0x04001495 RID: 5269
		AverageNullableLong,
		// Token: 0x04001496 RID: 5270
		AverageDouble,
		// Token: 0x04001497 RID: 5271
		AverageNullableDouble,
		// Token: 0x04001498 RID: 5272
		AverageDecimal,
		// Token: 0x04001499 RID: 5273
		AverageNullableDecimal,
		// Token: 0x0400149A RID: 5274
		AverageSingle,
		// Token: 0x0400149B RID: 5275
		AverageNullableSingle,
		// Token: 0x0400149C RID: 5276
		AverageIntSelector,
		// Token: 0x0400149D RID: 5277
		AverageNullableIntSelector,
		// Token: 0x0400149E RID: 5278
		AverageLongSelector,
		// Token: 0x0400149F RID: 5279
		AverageNullableLongSelector,
		// Token: 0x040014A0 RID: 5280
		AverageDoubleSelector,
		// Token: 0x040014A1 RID: 5281
		AverageNullableDoubleSelector,
		// Token: 0x040014A2 RID: 5282
		AverageDecimalSelector,
		// Token: 0x040014A3 RID: 5283
		AverageNullableDecimalSelector,
		// Token: 0x040014A4 RID: 5284
		AverageSingleSelector,
		// Token: 0x040014A5 RID: 5285
		AverageNullableSingleSelector,
		// Token: 0x040014A6 RID: 5286
		Aggregate,
		// Token: 0x040014A7 RID: 5287
		AggregateSeed,
		// Token: 0x040014A8 RID: 5288
		AggregateSeedSelector,
		// Token: 0x040014A9 RID: 5289
		AsQueryable,
		// Token: 0x040014AA RID: 5290
		AsQueryableGeneric,
		// Token: 0x040014AB RID: 5291
		AsEnumerable,
		// Token: 0x040014AC RID: 5292
		ToList,
		// Token: 0x040014AD RID: 5293
		Zip,
		// Token: 0x040014AE RID: 5294
		NotSupported
	}
}
