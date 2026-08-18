using System;
using System.Collections.Generic;
using System.Linq.Parallel;

namespace System.Linq
{
	// Token: 0x0200016E RID: 366
	internal static class AggregationMinMaxHelpers<T>
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x00030904 File Offset: 0x0002EB04
		private static T Reduce(IEnumerable<T> source, int sign)
		{
			Func<Pair<bool, T>, T, Pair<bool, T>> intermediateReduce = AggregationMinMaxHelpers<T>.MakeIntermediateReduceFunction(sign);
			Func<Pair<bool, T>, Pair<bool, T>, Pair<bool, T>> finalReduce = AggregationMinMaxHelpers<T>.MakeFinalReduceFunction(sign);
			Func<Pair<bool, T>, T> resultSelector = AggregationMinMaxHelpers<T>.MakeResultSelectorFunction();
			AssociativeAggregationOperator<T, Pair<bool, T>, T> associativeAggregationOperator = new AssociativeAggregationOperator<T, Pair<bool, T>, T>(source, new Pair<bool, T>(false, default(T)), null, true, intermediateReduce, finalReduce, resultSelector, default(T) != null, QueryAggregationOptions.AssociativeCommutative);
			return associativeAggregationOperator.Aggregate();
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003095A File Offset: 0x0002EB5A
		internal static T ReduceMin(IEnumerable<T> source)
		{
			return AggregationMinMaxHelpers<T>.Reduce(source, -1);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00030963 File Offset: 0x0002EB63
		internal static T ReduceMax(IEnumerable<T> source)
		{
			return AggregationMinMaxHelpers<T>.Reduce(source, 1);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003096C File Offset: 0x0002EB6C
		private static Func<Pair<bool, T>, T, Pair<bool, T>> MakeIntermediateReduceFunction(int sign)
		{
			Comparer<T> comparer = Util.GetDefaultComparer<T>();
			return delegate(Pair<bool, T> accumulator, T element)
			{
				if ((default(T) != null || element != null) && (!accumulator.First || Util.Sign(comparer.Compare(element, accumulator.Second)) == sign))
				{
					return new Pair<bool, T>(true, element);
				}
				return accumulator;
			};
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000309A0 File Offset: 0x0002EBA0
		private static Func<Pair<bool, T>, Pair<bool, T>, Pair<bool, T>> MakeFinalReduceFunction(int sign)
		{
			Comparer<T> comparer = Util.GetDefaultComparer<T>();
			return delegate(Pair<bool, T> accumulator, Pair<bool, T> element)
			{
				if (element.First && (!accumulator.First || Util.Sign(comparer.Compare(element.Second, accumulator.Second)) == sign))
				{
					return new Pair<bool, T>(true, element.Second);
				}
				return accumulator;
			};
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000309D1 File Offset: 0x0002EBD1
		private static Func<Pair<bool, T>, T> MakeResultSelectorFunction()
		{
			return (Pair<bool, T> accumulator) => accumulator.Second;
		}
	}
}
