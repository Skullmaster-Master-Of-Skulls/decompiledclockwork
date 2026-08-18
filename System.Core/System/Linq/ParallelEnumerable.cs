using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Parallel;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
	// Token: 0x0200016B RID: 363
	[__DynamicallyInvokable]
	public static class ParallelEnumerable
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x0002E4CA File Offset: 0x0002C6CA
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> AsParallel<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ParallelEnumerableWrapper<TSource>(source);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002E4E0 File Offset: 0x0002C6E0
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> AsParallel<TSource>(this Partitioner<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new PartitionerQueryOperator<TSource>(source);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> AsOrdered<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(source is ParallelEnumerableWrapper<TSource>) && !(source is IParallelPartitionable<TSource>))
			{
				PartitionerQueryOperator<TSource> partitionerQueryOperator = source as PartitionerQueryOperator<TSource>;
				if (partitionerQueryOperator == null)
				{
					throw new InvalidOperationException(SR.GetString("ParallelQuery_InvalidAsOrderedCall"));
				}
				if (!partitionerQueryOperator.Orderable)
				{
					throw new InvalidOperationException(SR.GetString("ParallelQuery_PartitionerNotOrderable"));
				}
			}
			return new OrderingQueryOperator<TSource>(QueryOperator<TSource>.AsQueryOperator(source), true);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002E564 File Offset: 0x0002C764
		[__DynamicallyInvokable]
		public static ParallelQuery AsOrdered(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper parallelEnumerableWrapper = source as ParallelEnumerableWrapper;
			if (parallelEnumerableWrapper == null)
			{
				throw new InvalidOperationException(SR.GetString("ParallelQuery_InvalidNonGenericAsOrderedCall"));
			}
			return new OrderingQueryOperator<object>(QueryOperator<object>.AsQueryOperator(parallelEnumerableWrapper), true);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002E5A5 File Offset: 0x0002C7A5
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> AsUnordered<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new OrderingQueryOperator<TSource>(QueryOperator<TSource>.AsQueryOperator(source), false);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002E5C1 File Offset: 0x0002C7C1
		[__DynamicallyInvokable]
		public static ParallelQuery AsParallel(this IEnumerable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ParallelEnumerableWrapper(source);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> AsSequential<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				return parallelEnumerableWrapper.WrappedEnumerable;
			}
			return source;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002E608 File Offset: 0x0002C808
		internal static ParallelQuery<TSource> WithTaskScheduler<TSource>(this ParallelQuery<TSource> source, TaskScheduler taskScheduler)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (taskScheduler == null)
			{
				throw new ArgumentNullException("taskScheduler");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.TaskScheduler = taskScheduler;
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002E64C File Offset: 0x0002C84C
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> WithDegreeOfParallelism<TSource>(this ParallelQuery<TSource> source, int degreeOfParallelism)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (degreeOfParallelism < 1 || degreeOfParallelism > 512)
			{
				throw new ArgumentOutOfRangeException("degreeOfParallelism");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.DegreeOfParallelism = new int?(degreeOfParallelism);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002E6A0 File Offset: 0x0002C8A0
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> WithCancellation<TSource>(this ParallelQuery<TSource> source, CancellationToken cancellationToken)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			try
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate()
				{
				});
			}
			catch (ObjectDisposedException)
			{
				throw new ArgumentException(SR.GetString("ParallelEnumerable_WithCancellation_TokenSourceDisposed"), "cancellationToken");
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.CancellationState = new CancellationState(cancellationToken);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002E748 File Offset: 0x0002C948
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> WithExecutionMode<TSource>(this ParallelQuery<TSource> source, ParallelExecutionMode executionMode)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (executionMode != ParallelExecutionMode.Default && executionMode != ParallelExecutionMode.ForceParallelism)
			{
				throw new ArgumentException(SR.GetString("ParallelEnumerable_WithQueryExecutionMode_InvalidMode"));
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.ExecutionMode = new ParallelExecutionMode?(executionMode);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002E79C File Offset: 0x0002C99C
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> WithMergeOptions<TSource>(this ParallelQuery<TSource> source, ParallelMergeOptions mergeOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (mergeOptions != ParallelMergeOptions.Default && mergeOptions != ParallelMergeOptions.AutoBuffered && mergeOptions != ParallelMergeOptions.NotBuffered && mergeOptions != ParallelMergeOptions.FullyBuffered)
			{
				throw new ArgumentException(SR.GetString("ParallelEnumerable_WithMergeOptions_InvalidOptions"));
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.MergeOptions = new ParallelMergeOptions?(mergeOptions);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002E7F5 File Offset: 0x0002C9F5
		[__DynamicallyInvokable]
		public static ParallelQuery<int> Range(int start, int count)
		{
			if (count < 0 || (count > 0 && 2147483647 - (count - 1) < start))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new RangeEnumerable(start, count);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002E81D File Offset: 0x0002CA1D
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Repeat<TResult>(TResult element, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new RepeatEnumerable<TResult>(element, count);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002E835 File Offset: 0x0002CA35
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Empty<TResult>()
		{
			return EmptyEnumerable<TResult>.Instance;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002E83C File Offset: 0x0002CA3C
		[__DynamicallyInvokable]
		public static void ForAll<TSource>(this ParallelQuery<TSource> source, Action<TSource> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			new ForAllOperator<TSource>(source, action).RunSynchronously();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002E866 File Offset: 0x0002CA66
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new WhereQueryOperator<TSource>(source, predicate);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002E88B File Offset: 0x0002CA8B
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new IndexedWhereQueryOperator<TSource>(source, predicate);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002E8B0 File Offset: 0x0002CAB0
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Select<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectQueryOperator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002E8D5 File Offset: 0x0002CAD5
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Select<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new IndexedSelectQueryOperator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002E8FA File Offset: 0x0002CAFA
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(this ParallelQuery<TFirst> first, ParallelQuery<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new ZipQueryOperator<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002E92E File Offset: 0x0002CB2E
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(this ParallelQuery<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002E93F File Offset: 0x0002CB3F
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002E94D File Offset: 0x0002CB4D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002E960 File Offset: 0x0002CB60
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new JoinQueryOperator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002E9C1 File Offset: 0x0002CBC1
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002E9D2 File Offset: 0x0002CBD2
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002E9E0 File Offset: 0x0002CBE0
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002E9F4 File Offset: 0x0002CBF4
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new GroupJoinQueryOperator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002EA55 File Offset: 0x0002CC55
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002EA66 File Offset: 0x0002CC66
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> SelectMany<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectManyQueryOperator<TSource, TResult, TResult>(source, selector, null, null);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002EA8D File Offset: 0x0002CC8D
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> SelectMany<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectManyQueryOperator<TSource, TResult, TResult>(source, null, selector, null);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002EAB4 File Offset: 0x0002CCB4
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(this ParallelQuery<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new SelectManyQueryOperator<TSource, TCollection, TResult>(source, collectionSelector, null, resultSelector);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002EAE9 File Offset: 0x0002CCE9
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new SelectManyQueryOperator<TSource, TCollection, TResult>(source, null, collectionSelector, resultSelector);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002EB1E File Offset: 0x0002CD1E
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, null, false));
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002EB4A File Offset: 0x0002CD4A
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, comparer, false));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002EB76 File Offset: 0x0002CD76
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, null, true));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002EBA2 File Offset: 0x0002CDA2
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, comparer, true));
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002EBCE File Offset: 0x0002CDCE
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, null, false));
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002EC04 File Offset: 0x0002CE04
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, comparer, false));
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002EC3A File Offset: 0x0002CE3A
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, null, true));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002EC70 File Offset: 0x0002CE70
		[__DynamicallyInvokable]
		public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, comparer, true));
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002ECA6 File Offset: 0x0002CEA6
		[__DynamicallyInvokable]
		public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.GroupBy(keySelector, null);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002ECB0 File Offset: 0x0002CEB0
		[__DynamicallyInvokable]
		public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new GroupByQueryOperator<TSource, TKey, TSource>(source, keySelector, null, comparer);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002ECD7 File Offset: 0x0002CED7
		[__DynamicallyInvokable]
		public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.GroupBy(keySelector, elementSelector, null);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002ECE2 File Offset: 0x0002CEE2
		[__DynamicallyInvokable]
		public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			return new GroupByQueryOperator<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002ED18 File Offset: 0x0002CF18
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector)
			select resultSelector(grouping.Key, grouping);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002ED60 File Offset: 0x0002CF60
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, comparer)
			select resultSelector(grouping.Key, grouping);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, elementSelector)
			select resultSelector(grouping.Key, grouping);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002EDF0 File Offset: 0x0002CFF0
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, elementSelector, comparer)
			select resultSelector(grouping.Key, grouping);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002EE38 File Offset: 0x0002D038
		private static T PerformAggregation<T>(this ParallelQuery<T> source, Func<T, T, T> reduce, T seed, bool seedIsSpecified, bool throwIfEmpty, QueryAggregationOptions options)
		{
			AssociativeAggregationOperator<T, T, T> associativeAggregationOperator = new AssociativeAggregationOperator<T, T, T>(source, seed, null, seedIsSpecified, reduce, reduce, (T obj) => obj, throwIfEmpty, options);
			return associativeAggregationOperator.Aggregate();
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002EE7C File Offset: 0x0002D07C
		private static TAccumulate PerformSequentialAggregation<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, bool seedIsSpecified, Func<TAccumulate, TSource, TAccumulate> func)
		{
			TAccumulate result;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				TAccumulate taccumulate;
				if (seedIsSpecified)
				{
					taccumulate = seed;
				}
				else
				{
					if (!enumerator.MoveNext())
					{
						throw new InvalidOperationException(SR.GetString("NoElements"));
					}
					taccumulate = (TAccumulate)((object)enumerator.Current);
				}
				while (enumerator.MoveNext())
				{
					TSource arg = enumerator.Current;
					try
					{
						taccumulate = func(taccumulate, arg);
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[]
						{
							ex
						});
					}
				}
				result = taccumulate;
			}
			return result;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002EF2C File Offset: 0x0002D12C
		[__DynamicallyInvokable]
		public static TSource Aggregate<TSource>(this ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func)
		{
			return source.Aggregate(func, QueryAggregationOptions.AssociativeCommutative);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002EF38 File Offset: 0x0002D138
		internal static TSource Aggregate<TSource>(this ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func, QueryAggregationOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if ((~(QueryAggregationOptions.Associative | QueryAggregationOptions.Commutative) & options) != QueryAggregationOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & QueryAggregationOptions.Associative) != QueryAggregationOptions.Associative)
			{
				return source.PerformSequentialAggregation(default(TSource), false, func);
			}
			return source.PerformAggregation(func, default(TSource), false, true, options);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002EF9D File Offset: 0x0002D19D
		[__DynamicallyInvokable]
		public static TAccumulate Aggregate<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			return source.Aggregate(seed, func, QueryAggregationOptions.AssociativeCommutative);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002EFA8 File Offset: 0x0002D1A8
		internal static TAccumulate Aggregate<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, QueryAggregationOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if ((~(QueryAggregationOptions.Associative | QueryAggregationOptions.Commutative) & options) != QueryAggregationOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			return source.PerformSequentialAggregation(seed, true, func);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002EFE0 File Offset: 0x0002D1E0
		[__DynamicallyInvokable]
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			TAccumulate arg = source.PerformSequentialAggregation(seed, true, func);
			TResult result;
			try
			{
				result = resultSelector(arg);
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new AggregateException(new Exception[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002F05C File Offset: 0x0002D25C
		[__DynamicallyInvokable]
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (updateAccumulatorFunc == null)
			{
				throw new ArgumentNullException("updateAccumulatorFunc");
			}
			if (combineAccumulatorsFunc == null)
			{
				throw new ArgumentNullException("combineAccumulatorsFunc");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new AssociativeAggregationOperator<TSource, TAccumulate, TResult>(source, seed, null, true, updateAccumulatorFunc, combineAccumulatorsFunc, resultSelector, false, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
		[__DynamicallyInvokable]
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, Func<TAccumulate> seedFactory, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (seedFactory == null)
			{
				throw new ArgumentNullException("seedFactory");
			}
			if (updateAccumulatorFunc == null)
			{
				throw new ArgumentNullException("updateAccumulatorFunc");
			}
			if (combineAccumulatorsFunc == null)
			{
				throw new ArgumentNullException("combineAccumulatorsFunc");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new AssociativeAggregationOperator<TSource, TAccumulate, TResult>(source, default(TAccumulate), seedFactory, true, updateAccumulatorFunc, combineAccumulatorsFunc, resultSelector, false, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002F128 File Offset: 0x0002D328
		[__DynamicallyInvokable]
		public static int Count<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				ICollection<TSource> collection = parallelEnumerableWrapper.WrappedEnumerable as ICollection<TSource>;
				if (collection != null)
				{
					return collection.Count;
				}
			}
			return new CountAggregationOperator<TSource>(source).Aggregate();
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002F16E File Offset: 0x0002D36E
		[__DynamicallyInvokable]
		public static int Count<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new CountAggregationOperator<TSource>(source.Where(predicate)).Aggregate();
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002F1A0 File Offset: 0x0002D3A0
		[__DynamicallyInvokable]
		public static long LongCount<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				ICollection<TSource> collection = parallelEnumerableWrapper.WrappedEnumerable as ICollection<TSource>;
				if (collection != null)
				{
					return (long)collection.Count;
				}
			}
			return new LongCountAggregationOperator<TSource>(source).Aggregate();
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002F1E7 File Offset: 0x0002D3E7
		[__DynamicallyInvokable]
		public static long LongCount<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new LongCountAggregationOperator<TSource>(source.Where(predicate)).Aggregate();
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002F216 File Offset: 0x0002D416
		[__DynamicallyInvokable]
		public static int Sum(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002F231 File Offset: 0x0002D431
		[__DynamicallyInvokable]
		public static int? Sum(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002F24C File Offset: 0x0002D44C
		[__DynamicallyInvokable]
		public static long Sum(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002F267 File Offset: 0x0002D467
		[__DynamicallyInvokable]
		public static long? Sum(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002F282 File Offset: 0x0002D482
		[__DynamicallyInvokable]
		public static float Sum(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002F29D File Offset: 0x0002D49D
		[__DynamicallyInvokable]
		public static float? Sum(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002F2B8 File Offset: 0x0002D4B8
		[__DynamicallyInvokable]
		public static double Sum(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002F2D3 File Offset: 0x0002D4D3
		[__DynamicallyInvokable]
		public static double? Sum(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002F2EE File Offset: 0x0002D4EE
		[__DynamicallyInvokable]
		public static decimal Sum(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002F309 File Offset: 0x0002D509
		[__DynamicallyInvokable]
		public static decimal? Sum(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalSumAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002F324 File Offset: 0x0002D524
		[__DynamicallyInvokable]
		public static int Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002F332 File Offset: 0x0002D532
		[__DynamicallyInvokable]
		public static int? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002F340 File Offset: 0x0002D540
		[__DynamicallyInvokable]
		public static long Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002F34E File Offset: 0x0002D54E
		[__DynamicallyInvokable]
		public static long? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002F35C File Offset: 0x0002D55C
		[__DynamicallyInvokable]
		public static float Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002F36A File Offset: 0x0002D56A
		[__DynamicallyInvokable]
		public static float? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002F378 File Offset: 0x0002D578
		[__DynamicallyInvokable]
		public static double Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002F386 File Offset: 0x0002D586
		[__DynamicallyInvokable]
		public static double? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002F394 File Offset: 0x0002D594
		[__DynamicallyInvokable]
		public static decimal Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002F3A2 File Offset: 0x0002D5A2
		[__DynamicallyInvokable]
		public static decimal? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
		[__DynamicallyInvokable]
		public static int Min(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002F3CC File Offset: 0x0002D5CC
		[__DynamicallyInvokable]
		public static int? Min(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002F3E8 File Offset: 0x0002D5E8
		[__DynamicallyInvokable]
		public static long Min(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0002F404 File Offset: 0x0002D604
		[__DynamicallyInvokable]
		public static long? Min(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0002F420 File Offset: 0x0002D620
		[__DynamicallyInvokable]
		public static float Min(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002F43C File Offset: 0x0002D63C
		[__DynamicallyInvokable]
		public static float? Min(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002F458 File Offset: 0x0002D658
		[__DynamicallyInvokable]
		public static double Min(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002F474 File Offset: 0x0002D674
		[__DynamicallyInvokable]
		public static double? Min(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002F490 File Offset: 0x0002D690
		[__DynamicallyInvokable]
		public static decimal Min(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		[__DynamicallyInvokable]
		public static decimal? Min(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalMinMaxAggregationOperator(source, -1).Aggregate();
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
		[__DynamicallyInvokable]
		public static TSource Min<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return AggregationMinMaxHelpers<TSource>.ReduceMin(source);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002F4DE File Offset: 0x0002D6DE
		[__DynamicallyInvokable]
		public static int Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Min<int>();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002F4EC File Offset: 0x0002D6EC
		[__DynamicallyInvokable]
		public static int? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Min<int?>();
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0002F4FA File Offset: 0x0002D6FA
		[__DynamicallyInvokable]
		public static long Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Min<long>();
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002F508 File Offset: 0x0002D708
		[__DynamicallyInvokable]
		public static long? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Min<long?>();
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002F516 File Offset: 0x0002D716
		[__DynamicallyInvokable]
		public static float Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Min<float>();
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002F524 File Offset: 0x0002D724
		[__DynamicallyInvokable]
		public static float? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Min<float?>();
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002F532 File Offset: 0x0002D732
		[__DynamicallyInvokable]
		public static double Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Min<double>();
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0002F540 File Offset: 0x0002D740
		[__DynamicallyInvokable]
		public static double? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Min<double?>();
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0002F54E File Offset: 0x0002D74E
		[__DynamicallyInvokable]
		public static decimal Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Min<decimal>();
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0002F55C File Offset: 0x0002D75C
		[__DynamicallyInvokable]
		public static decimal? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Min<decimal?>();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0002F56A File Offset: 0x0002D76A
		[__DynamicallyInvokable]
		public static TResult Min<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Min<TResult>();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0002F578 File Offset: 0x0002D778
		[__DynamicallyInvokable]
		public static int Max(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0002F594 File Offset: 0x0002D794
		[__DynamicallyInvokable]
		public static int? Max(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		[__DynamicallyInvokable]
		public static long Max(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002F5CC File Offset: 0x0002D7CC
		[__DynamicallyInvokable]
		public static long? Max(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		[__DynamicallyInvokable]
		public static float Max(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002F604 File Offset: 0x0002D804
		[__DynamicallyInvokable]
		public static float? Max(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0002F620 File Offset: 0x0002D820
		[__DynamicallyInvokable]
		public static double Max(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0002F63C File Offset: 0x0002D83C
		[__DynamicallyInvokable]
		public static double? Max(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0002F658 File Offset: 0x0002D858
		[__DynamicallyInvokable]
		public static decimal Max(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002F674 File Offset: 0x0002D874
		[__DynamicallyInvokable]
		public static decimal? Max(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalMinMaxAggregationOperator(source, 1).Aggregate();
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002F690 File Offset: 0x0002D890
		[__DynamicallyInvokable]
		public static TSource Max<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return AggregationMinMaxHelpers<TSource>.ReduceMax(source);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002F6A6 File Offset: 0x0002D8A6
		[__DynamicallyInvokable]
		public static int Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Max<int>();
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
		[__DynamicallyInvokable]
		public static int? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Max<int?>();
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002F6C2 File Offset: 0x0002D8C2
		[__DynamicallyInvokable]
		public static long Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Max<long>();
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002F6D0 File Offset: 0x0002D8D0
		[__DynamicallyInvokable]
		public static long? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Max<long?>();
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002F6DE File Offset: 0x0002D8DE
		[__DynamicallyInvokable]
		public static float Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Max<float>();
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002F6EC File Offset: 0x0002D8EC
		[__DynamicallyInvokable]
		public static float? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Max<float?>();
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002F6FA File Offset: 0x0002D8FA
		[__DynamicallyInvokable]
		public static double Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Max<double>();
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002F708 File Offset: 0x0002D908
		[__DynamicallyInvokable]
		public static double? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Max<double?>();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002F716 File Offset: 0x0002D916
		[__DynamicallyInvokable]
		public static decimal Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Max<decimal>();
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002F724 File Offset: 0x0002D924
		[__DynamicallyInvokable]
		public static decimal? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Max<decimal?>();
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002F732 File Offset: 0x0002D932
		[__DynamicallyInvokable]
		public static TResult Max<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Max<TResult>();
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002F740 File Offset: 0x0002D940
		[__DynamicallyInvokable]
		public static double Average(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002F75B File Offset: 0x0002D95B
		[__DynamicallyInvokable]
		public static double? Average(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002F776 File Offset: 0x0002D976
		[__DynamicallyInvokable]
		public static double Average(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002F791 File Offset: 0x0002D991
		[__DynamicallyInvokable]
		public static double? Average(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002F7AC File Offset: 0x0002D9AC
		[__DynamicallyInvokable]
		public static float Average(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002F7C7 File Offset: 0x0002D9C7
		[__DynamicallyInvokable]
		public static float? Average(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002F7E2 File Offset: 0x0002D9E2
		[__DynamicallyInvokable]
		public static double Average(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002F7FD File Offset: 0x0002D9FD
		[__DynamicallyInvokable]
		public static double? Average(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002F818 File Offset: 0x0002DA18
		[__DynamicallyInvokable]
		public static decimal Average(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002F833 File Offset: 0x0002DA33
		[__DynamicallyInvokable]
		public static decimal? Average(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalAverageAggregationOperator(source).Aggregate();
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002F84E File Offset: 0x0002DA4E
		[__DynamicallyInvokable]
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002F85C File Offset: 0x0002DA5C
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002F86A File Offset: 0x0002DA6A
		[__DynamicallyInvokable]
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002F878 File Offset: 0x0002DA78
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002F886 File Offset: 0x0002DA86
		[__DynamicallyInvokable]
		public static float Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002F894 File Offset: 0x0002DA94
		[__DynamicallyInvokable]
		public static float? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002F8A2 File Offset: 0x0002DAA2
		[__DynamicallyInvokable]
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002F8B0 File Offset: 0x0002DAB0
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002F8BE File Offset: 0x0002DABE
		[__DynamicallyInvokable]
		public static decimal Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002F8CC File Offset: 0x0002DACC
		[__DynamicallyInvokable]
		public static decimal? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002F8DA File Offset: 0x0002DADA
		[__DynamicallyInvokable]
		public static bool Any<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new AnyAllSearchOperator<TSource>(source, true, predicate).Aggregate();
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002F905 File Offset: 0x0002DB05
		[__DynamicallyInvokable]
		public static bool Any<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Any((TSource x) => true);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002F93A File Offset: 0x0002DB3A
		[__DynamicallyInvokable]
		public static bool All<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new AnyAllSearchOperator<TSource>(source, false, predicate).Aggregate();
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002F965 File Offset: 0x0002DB65
		[__DynamicallyInvokable]
		public static bool Contains<TSource>(this ParallelQuery<TSource> source, TSource value)
		{
			return source.Contains(value, null);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002F96F File Offset: 0x0002DB6F
		[__DynamicallyInvokable]
		public static bool Contains<TSource>(this ParallelQuery<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ContainsSearchOperator<TSource>(source, value, comparer).Aggregate();
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002F98C File Offset: 0x0002DB8C
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Take<TSource>(this ParallelQuery<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count > 0)
			{
				return new TakeOrSkipQueryOperator<TSource>(source, count, true);
			}
			return ParallelEnumerable.Empty<TSource>();
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002F9AE File Offset: 0x0002DBAE
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> TakeWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, predicate, null, true);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002F9D5 File Offset: 0x0002DBD5
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> TakeWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, null, predicate, true);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002F9FC File Offset: 0x0002DBFC
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Skip<TSource>(this ParallelQuery<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count <= 0)
			{
				return source;
			}
			return new TakeOrSkipQueryOperator<TSource>(source, count, false);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002FA1A File Offset: 0x0002DC1A
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> SkipWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, predicate, null, false);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002FA41 File Offset: 0x0002DC41
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> SkipWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, null, predicate, false);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002FA68 File Offset: 0x0002DC68
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Concat<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new ConcatQueryOperator<TSource>(first, second);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002FA8D File Offset: 0x0002DC8D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Concat<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002FA9E File Offset: 0x0002DC9E
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return first.SequenceEqual(second, null);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002FAD8 File Offset: 0x0002DCD8
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			comparer = (comparer ?? EqualityComparer<TSource>.Default);
			QueryOperator<TSource> queryOperator = QueryOperator<TSource>.AsQueryOperator(first);
			QueryOperator<TSource> queryOperator2 = QueryOperator<TSource>.AsQueryOperator(second);
			QuerySettings querySettings = queryOperator.SpecifiedQuerySettings.Merge(queryOperator2.SpecifiedQuerySettings).WithDefaults().WithPerExecutionSettings(new CancellationTokenSource(), new Shared<bool>(false));
			IEnumerator<TSource> enumerator = first.GetEnumerator();
			try
			{
				IEnumerator<TSource> enumerator2 = second.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext() || !comparer.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					if (enumerator2.MoveNext())
					{
						return false;
					}
				}
				catch (ThreadAbortException)
				{
					throw;
				}
				catch (Exception ex)
				{
					ExceptionAggregator.ThrowOCEorAggregateException(ex, querySettings.CancellationState);
				}
				finally
				{
					ParallelEnumerable.DisposeEnumerator<TSource>(enumerator2, querySettings.CancellationState);
				}
			}
			finally
			{
				ParallelEnumerable.DisposeEnumerator<TSource>(enumerator, querySettings.CancellationState);
			}
			return true;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002FC04 File Offset: 0x0002DE04
		private static void DisposeEnumerator<TSource>(IEnumerator<TSource> e, CancellationState cancelState)
		{
			try
			{
				e.Dispose();
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex)
			{
				ExceptionAggregator.ThrowOCEorAggregateException(ex, cancelState);
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002FC44 File Offset: 0x0002DE44
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002FC55 File Offset: 0x0002DE55
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Distinct<TSource>(this ParallelQuery<TSource> source)
		{
			return source.Distinct(null);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002FC5E File Offset: 0x0002DE5E
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Distinct<TSource>(this ParallelQuery<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DistinctQueryOperator<TSource>(source, comparer);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002FC75 File Offset: 0x0002DE75
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Union(second, null);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002FC7F File Offset: 0x0002DE7F
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002FC90 File Offset: 0x0002DE90
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new UnionQueryOperator<TSource>(first, second, comparer);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0002FCB6 File Offset: 0x0002DEB6
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0002FCC7 File Offset: 0x0002DEC7
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Intersect(second, null);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0002FCD1 File Offset: 0x0002DED1
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002FCE2 File Offset: 0x0002DEE2
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new IntersectQueryOperator<TSource>(first, second, comparer);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002FD08 File Offset: 0x0002DF08
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002FD19 File Offset: 0x0002DF19
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Except(second, null);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002FD23 File Offset: 0x0002DF23
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002FD34 File Offset: 0x0002DF34
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new ExceptQueryOperator<TSource>(first, second, comparer);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002FD5A File Offset: 0x0002DF5A
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException(SR.GetString("ParallelEnumerable_BinaryOpMustUseAsParallel"));
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002FD6B File Offset: 0x0002DF6B
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> AsEnumerable<TSource>(this ParallelQuery<TSource> source)
		{
			return source.AsSequential<TSource>();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002FD74 File Offset: 0x0002DF74
		[__DynamicallyInvokable]
		public static TSource[] ToArray<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			if (queryOperator != null)
			{
				return queryOperator.ExecuteAndGetResultsAsArray();
			}
			return source.ToList<TSource>().ToArray<TSource>();
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002FDAC File Offset: 0x0002DFAC
		[__DynamicallyInvokable]
		public static List<TSource> ToList<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			List<TSource> list = new List<TSource>();
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator;
			if (queryOperator != null)
			{
				if (queryOperator.OrdinalIndexState == OrdinalIndexState.Indexible && queryOperator.OutputOrdered)
				{
					return new List<TSource>(source.ToArray<TSource>());
				}
				enumerator = queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered));
			}
			else
			{
				enumerator = source.GetEnumerator();
			}
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource item = enumerator.Current;
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002FE40 File Offset: 0x0002E040
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002FE50 File Offset: 0x0002E050
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(comparer);
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator = (queryOperator == null) ? source.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true);
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					try
					{
						TKey key = keySelector(tsource);
						dictionary.Add(key, tsource);
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[]
						{
							ex
						});
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002FF18 File Offset: 0x0002E118
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002FF28 File Offset: 0x0002E128
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator = (queryOperator == null) ? source.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true);
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource arg = enumerator.Current;
					try
					{
						dictionary.Add(keySelector(arg), elementSelector(arg));
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[]
						{
							ex
						});
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00030000 File Offset: 0x0002E200
		[__DynamicallyInvokable]
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToLookup(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00030010 File Offset: 0x0002E210
		[__DynamicallyInvokable]
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			comparer = (comparer ?? EqualityComparer<TKey>.Default);
			ParallelQuery<IGrouping<TKey, TSource>> parallelQuery = source.GroupBy(keySelector, comparer);
			Lookup<TKey, TSource> lookup = new Lookup<TKey, TSource>(comparer);
			QueryOperator<IGrouping<TKey, TSource>> queryOperator = parallelQuery as QueryOperator<IGrouping<TKey, TSource>>;
			IEnumerator<IGrouping<TKey, TSource>> enumerator = (queryOperator == null) ? parallelQuery.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					IGrouping<TKey, TSource> grouping = enumerator.Current;
					lookup.Add(grouping);
				}
			}
			return lookup;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000300AC File Offset: 0x0002E2AC
		[__DynamicallyInvokable]
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToLookup(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000300BC File Offset: 0x0002E2BC
		[__DynamicallyInvokable]
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			comparer = (comparer ?? EqualityComparer<TKey>.Default);
			ParallelQuery<IGrouping<TKey, TElement>> parallelQuery = source.GroupBy(keySelector, elementSelector, comparer);
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			QueryOperator<IGrouping<TKey, TElement>> queryOperator = parallelQuery as QueryOperator<IGrouping<TKey, TElement>>;
			IEnumerator<IGrouping<TKey, TElement>> enumerator = (queryOperator == null) ? parallelQuery.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					IGrouping<TKey, TElement> grouping = enumerator.Current;
					lookup.Add(grouping);
				}
			}
			return lookup;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00030168 File Offset: 0x0002E368
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> Reverse<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ReverseQueryOperator<TSource>(source);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0003017E File Offset: 0x0002E37E
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> OfType<TResult>(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.OfType<TResult>();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00030194 File Offset: 0x0002E394
		[__DynamicallyInvokable]
		public static ParallelQuery<TResult> Cast<TResult>(this ParallelQuery source)
		{
			return source.Cast<TResult>();
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003019C File Offset: 0x0002E39C
		private static TSource GetOneWithPossibleDefault<TSource>(QueryOperator<TSource> queryOp, bool throwIfTwo, bool defaultIfEmpty)
		{
			using (IEnumerator<TSource> enumerator = queryOp.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)))
			{
				if (enumerator.MoveNext())
				{
					TSource result = enumerator.Current;
					if (throwIfTwo && enumerator.MoveNext())
					{
						throw new InvalidOperationException(SR.GetString("MoreThanOneMatch"));
					}
					return result;
				}
			}
			if (defaultIfEmpty)
			{
				return default(TSource);
			}
			throw new InvalidOperationException(SR.GetString("NoElements"));
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00030220 File Offset: 0x0002E420
		[__DynamicallyInvokable]
		public static TSource First<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, null);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).First<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, false);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000302C4 File Offset: 0x0002E4C4
		[__DynamicallyInvokable]
		public static TSource First<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).First(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, false);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00030384 File Offset: 0x0002E584
		[__DynamicallyInvokable]
		public static TSource FirstOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, null);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).FirstOrDefault<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, true);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00030428 File Offset: 0x0002E628
		[__DynamicallyInvokable]
		public static TSource FirstOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).FirstOrDefault(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, true);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000304E8 File Offset: 0x0002E6E8
		[__DynamicallyInvokable]
		public static TSource Last<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, null);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).Last<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, false);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003058C File Offset: 0x0002E78C
		[__DynamicallyInvokable]
		public static TSource Last<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).Last(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, false);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003064C File Offset: 0x0002E84C
		[__DynamicallyInvokable]
		public static TSource LastOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, null);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).LastOrDefault<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, true);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000306F0 File Offset: 0x0002E8F0
		[__DynamicallyInvokable]
		public static TSource LastOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					IEnumerable<TSource> source2 = lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken);
					IEnumerable<TSource> source3 = CancellableEnumerable.Wrap<TSource>(source2, querySettings.CancellationState.ExternalCancellationToken);
					return ExceptionAggregator.WrapEnumerable<TSource>(source3, querySettings.CancellationState).LastOrDefault(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, true);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000307AF File Offset: 0x0002E9AF
		[__DynamicallyInvokable]
		public static TSource Single<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, null), true, false);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000307CD File Offset: 0x0002E9CD
		[__DynamicallyInvokable]
		public static TSource Single<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, predicate), true, false);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000307F9 File Offset: 0x0002E9F9
		[__DynamicallyInvokable]
		public static TSource SingleOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, null), true, true);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00030817 File Offset: 0x0002EA17
		[__DynamicallyInvokable]
		public static TSource SingleOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, predicate), true, true);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00030844 File Offset: 0x0002EA44
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(this ParallelQuery<TSource> source)
		{
			return source.DefaultIfEmpty(default(TSource));
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00030860 File Offset: 0x0002EA60
		[__DynamicallyInvokable]
		public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(this ParallelQuery<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DefaultIfEmptyQueryOperator<TSource>(source, defaultValue);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00030878 File Offset: 0x0002EA78
		[__DynamicallyInvokable]
		public static TSource ElementAt<TSource>(this ParallelQuery<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ElementAtQueryOperator<TSource> elementAtQueryOperator = new ElementAtQueryOperator<TSource>(source, index);
			TSource result;
			if (elementAtQueryOperator.Aggregate(out result, false))
			{
				return result;
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000308C4 File Offset: 0x0002EAC4
		[__DynamicallyInvokable]
		public static TSource ElementAtOrDefault<TSource>(this ParallelQuery<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (index >= 0)
			{
				ElementAtQueryOperator<TSource> elementAtQueryOperator = new ElementAtQueryOperator<TSource>(source, index);
				TSource result;
				if (elementAtQueryOperator.Aggregate(out result, true))
				{
					return result;
				}
			}
			return default(TSource);
		}

		// Token: 0x040007A7 RID: 1959
		private const string RIGHT_SOURCE_NOT_PARALLEL_STR = "The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.";
	}
}
