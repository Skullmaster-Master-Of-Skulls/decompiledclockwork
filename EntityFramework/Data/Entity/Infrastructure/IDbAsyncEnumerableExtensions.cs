using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000295 RID: 661
	internal static class IDbAsyncEnumerableExtensions
	{
		// Token: 0x06001717 RID: 5911 RVA: 0x00072FB8 File Offset: 0x000711B8
		internal static async Task ForEachAsync(this IDbAsyncEnumerable source, Action<object> action, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator enumerator = source.GetAsyncEnumerator())
			{
				if (await enumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					Task<bool> moveNextTask;
					do
					{
						cancellationToken.ThrowIfCancellationRequested();
						object obj = enumerator.Current;
						moveNextTask = enumerator.MoveNextAsync(cancellationToken);
						action(obj);
					}
					while (await moveNextTask.WithCurrentCulture<bool>());
				}
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0007300E File Offset: 0x0007120E
		internal static Task ForEachAsync<T>(this IDbAsyncEnumerable<T> source, Action<T> action, CancellationToken cancellationToken)
		{
			return IDbAsyncEnumerableExtensions.ForEachAsync<T>(source.GetAsyncEnumerator(), action, cancellationToken);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0007322C File Offset: 0x0007142C
		private static async Task ForEachAsync<T>(IDbAsyncEnumerator<T> enumerator, Action<T> action, CancellationToken cancellationToken)
		{
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (await enumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					Task<bool> moveNextTask;
					do
					{
						cancellationToken.ThrowIfCancellationRequested();
						T obj = enumerator.Current;
						moveNextTask = enumerator.MoveNextAsync(cancellationToken);
						action(obj);
					}
					while (await moveNextTask.WithCurrentCulture<bool>());
				}
			}
			finally
			{
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00073282 File Offset: 0x00071482
		internal static Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable source)
		{
			return source.ToListAsync(CancellationToken.None);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000733C4 File Offset: 0x000715C4
		internal static async Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable source, CancellationToken cancellationToken)
		{
			List<T> list = new List<T>();
			await source.ForEachAsync(delegate(object e)
			{
				list.Add((T)((object)e));
			}, cancellationToken).WithCurrentCulture();
			return list;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00073412 File Offset: 0x00071612
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable<T> source)
		{
			return source.ToListAsync(CancellationToken.None);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0007347C File Offset: 0x0007167C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();
			List<T> list = new List<T>();
			source.ForEachAsync(new Action<T>(list.Add), cancellationToken).ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				tcs.TrySetResult(list);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000734DF File Offset: 0x000716DF
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<T[]> ToArrayAsync<T>(this IDbAsyncEnumerable<T> source)
		{
			return source.ToArrayAsync(CancellationToken.None);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000735E4 File Offset: 0x000717E4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static async Task<T[]> ToArrayAsync<T>(this IDbAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			List<T> list = await source.ToListAsync(cancellationToken).WithCurrentCulture<List<T>>();
			return list.ToArray();
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00073632 File Offset: 0x00071832
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, null, CancellationToken.None);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00073646 File Offset: 0x00071846
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, null, cancellationToken);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00073656 File Offset: 0x00071856
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, comparer, CancellationToken.None);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0007366A File Offset: 0x0007186A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, comparer, cancellationToken);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0007367A File Offset: 0x0007187A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionaryAsync(keySelector, elementSelector, null, CancellationToken.None);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0007368A File Offset: 0x0007188A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken)
		{
			return source.ToDictionaryAsync(keySelector, elementSelector, null, cancellationToken);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00073696 File Offset: 0x00071896
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			return source.ToDictionaryAsync(keySelector, elementSelector, comparer, CancellationToken.None);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00073818 File Offset: 0x00071A18
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TElement> d = new Dictionary<TKey, TElement>(comparer);
			await source.ForEachAsync(delegate(TSource element)
			{
				d.Add(keySelector(element), elementSelector(element));
			}, cancellationToken).WithCurrentCulture();
			return d;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0007387F File Offset: 0x00071A7F
		internal static IDbAsyncEnumerable<TResult> Cast<TResult>(this IDbAsyncEnumerable source)
		{
			return new IDbAsyncEnumerableExtensions.CastDbAsyncEnumerable<TResult>(source);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00073887 File Offset: 0x00071A87
		internal static Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.FirstAsync(CancellationToken.None);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00073894 File Offset: 0x00071A94
		internal static Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.FirstAsync(predicate, CancellationToken.None);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000739EC File Offset: 0x00071BEC
		internal static async Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				if (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					return e.Current;
				}
			}
			throw Error.EmptySequence();
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00073B9C File Offset: 0x00071D9C
		internal static async Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				if (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>() && predicate(e.Current))
				{
					return e.Current;
				}
			}
			throw Error.NoMatch();
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00073BF2 File Offset: 0x00071DF2
		internal static Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.FirstOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00073BFF File Offset: 0x00071DFF
		internal static Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.FirstOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00073D5C File Offset: 0x00071F5C
		internal static async Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				if (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					return e.Current;
				}
			}
			return default(TSource);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00073F28 File Offset: 0x00072128
		internal static async Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				if (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>() && predicate(e.Current))
				{
					return e.Current;
				}
			}
			return default(TSource);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00073F7E File Offset: 0x0007217E
		internal static Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.SingleAsync(CancellationToken.None);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00074198 File Offset: 0x00072398
		internal static async Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				if (!(await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>()))
				{
					throw Error.EmptySequence();
				}
				cancellationToken.ThrowIfCancellationRequested();
				TSource result = e.Current;
				if (!(await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>()))
				{
					return result;
				}
			}
			throw Error.MoreThanOneElement();
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000741E6 File Offset: 0x000723E6
		internal static Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.SingleAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000743D8 File Offset: 0x000725D8
		internal static async Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			TSource result = default(TSource);
			long count = 0L;
			long num;
			checked
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (predicate(e.Current))
						{
							result = e.Current;
							count += 1L;
						}
					}
				}
				num = count;
			}
			if (num <= 1L && num >= 0L)
			{
				switch ((int)num)
				{
				case 0:
					throw Error.NoMatch();
				case 1:
					return result;
				}
			}
			throw Error.MoreThanOneMatch();
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0007442E File Offset: 0x0007262E
		internal static Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.SingleOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00074654 File Offset: 0x00072854
		internal static async Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				if (!(await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>()))
				{
					return default(TSource);
				}
				cancellationToken.ThrowIfCancellationRequested();
				TSource result = e.Current;
				if (!(await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>()))
				{
					return result;
				}
			}
			throw Error.MoreThanOneElement();
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000746A2 File Offset: 0x000728A2
		internal static Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.SingleOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00074874 File Offset: 0x00072A74
		internal static async Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			TSource result = default(TSource);
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (predicate(e.Current))
						{
							result = e.Current;
							count += 1L;
						}
					}
				}
				if (count < 2L)
				{
					return result;
				}
				throw Error.MoreThanOneMatch();
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000748CA File Offset: 0x00072ACA
		internal static Task<bool> ContainsAsync<TSource>(this IDbAsyncEnumerable<TSource> source, TSource value)
		{
			return source.ContainsAsync(value, CancellationToken.None);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00074A58 File Offset: 0x00072C58
		internal static async Task<bool> ContainsAsync<TSource>(this IDbAsyncEnumerable<TSource> source, TSource value, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					if (EqualityComparer<TSource>.Default.Equals(e.Current, value))
					{
						return true;
					}
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
			return false;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00074AAE File Offset: 0x00072CAE
		internal static Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.AnyAsync(CancellationToken.None);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00074BF4 File Offset: 0x00072DF4
		internal static async Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				if (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00074C42 File Offset: 0x00072E42
		internal static Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.AnyAsync(predicate, CancellationToken.None);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00074DB4 File Offset: 0x00072FB4
		internal static async Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					if (predicate(e.Current))
					{
						return true;
					}
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
			return false;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00074E0A File Offset: 0x0007300A
		internal static Task<bool> AllAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.AllAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00074F7C File Offset: 0x0007317C
		internal static async Task<bool> AllAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					if (!predicate(e.Current))
					{
						return false;
					}
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
			return true;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00074FD2 File Offset: 0x000731D2
		internal static Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.CountAsync(CancellationToken.None);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00075140 File Offset: 0x00073340
		internal static async Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int count = 0;
			checked
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						count++;
					}
				}
				return count;
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0007518E File Offset: 0x0007338E
		internal static Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.CountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00075330 File Offset: 0x00073530
		internal static async Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int count = 0;
			checked
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (predicate(e.Current))
						{
							count++;
						}
					}
				}
				return count;
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00075386 File Offset: 0x00073586
		internal static Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.LongCountAsync(CancellationToken.None);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000754F8 File Offset: 0x000736F8
		internal static async Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						count += 1L;
					}
				}
				return count;
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00075546 File Offset: 0x00073746
		internal static Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.LongCountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000756E8 File Offset: 0x000738E8
		internal static async Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (predicate(e.Current))
						{
							count += 1L;
						}
					}
				}
				return count;
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0007573E File Offset: 0x0007393E
		internal static Task<TSource> MinAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.MinAsync(CancellationToken.None);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00075A84 File Offset: 0x00073C84
		internal static async Task<TSource> MinAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Comparer<TSource> comparer = Comparer<TSource>.Default;
			TSource value = default(TSource);
			TSource result;
			if (value == null)
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (e.Current != null && (value == null || comparer.Compare(e.Current, value) < 0))
						{
							value = e.Current;
						}
					}
				}
				result = value;
			}
			else
			{
				bool hasValue = false;
				using (IDbAsyncEnumerator<TSource> e2 = source.GetAsyncEnumerator())
				{
					while (await e2.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (hasValue)
						{
							if (comparer.Compare(e2.Current, value) < 0)
							{
								value = e2.Current;
							}
						}
						else
						{
							value = e2.Current;
							hasValue = true;
						}
					}
				}
				if (!hasValue)
				{
					throw Error.EmptySequence();
				}
				result = value;
			}
			return result;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00075AD2 File Offset: 0x00073CD2
		internal static Task<TSource> MaxAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.MaxAsync(CancellationToken.None);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00075E18 File Offset: 0x00074018
		internal static async Task<TSource> MaxAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Comparer<TSource> comparer = Comparer<TSource>.Default;
			TSource value = default(TSource);
			TSource result;
			if (value == null)
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (e.Current != null && (value == null || comparer.Compare(e.Current, value) > 0))
						{
							value = e.Current;
						}
					}
				}
				result = value;
			}
			else
			{
				bool hasValue = false;
				using (IDbAsyncEnumerator<TSource> e2 = source.GetAsyncEnumerator())
				{
					while (await e2.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						if (hasValue)
						{
							if (comparer.Compare(e2.Current, value) > 0)
							{
								value = e2.Current;
							}
						}
						else
						{
							value = e2.Current;
							hasValue = true;
						}
					}
				}
				if (!hasValue)
				{
					throw Error.EmptySequence();
				}
				result = value;
			}
			return result;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00075E66 File Offset: 0x00074066
		internal static Task<int> SumAsync(this IDbAsyncEnumerable<int> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00075FF8 File Offset: 0x000741F8
		internal static async Task<int> SumAsync(this IDbAsyncEnumerable<int> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			checked
			{
				using (IDbAsyncEnumerator<int> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						sum += unchecked((long)e.Current);
					}
				}
			}
			return (int)sum;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00076046 File Offset: 0x00074246
		internal static Task<int?> SumAsync(this IDbAsyncEnumerable<int?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00076200 File Offset: 0x00074400
		internal static async Task<int?> SumAsync(this IDbAsyncEnumerable<int?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			using (IDbAsyncEnumerator<int?> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					int? num = e.Current;
					if (num != null)
					{
						long num2 = sum;
						int? num3 = e.Current;
						sum = checked(num2 + unchecked((long)num3.GetValueOrDefault()));
					}
				}
			}
			return new int?((int)sum);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0007624E File Offset: 0x0007444E
		internal static Task<long> SumAsync(this IDbAsyncEnumerable<long> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000763E0 File Offset: 0x000745E0
		internal static async Task<long> SumAsync(this IDbAsyncEnumerable<long> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			checked
			{
				using (IDbAsyncEnumerator<long> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						sum += e.Current;
					}
				}
				return sum;
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0007642E File Offset: 0x0007462E
		internal static Task<long?> SumAsync(this IDbAsyncEnumerable<long?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000765E8 File Offset: 0x000747E8
		internal static async Task<long?> SumAsync(this IDbAsyncEnumerable<long?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			using (IDbAsyncEnumerator<long?> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					long? num = e.Current;
					if (num != null)
					{
						long num2 = sum;
						long? num3 = e.Current;
						sum = checked(num2 + num3.GetValueOrDefault());
					}
				}
			}
			return new long?(sum);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00076636 File Offset: 0x00074836
		internal static Task<float> SumAsync(this IDbAsyncEnumerable<float> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000767D0 File Offset: 0x000749D0
		internal static async Task<float> SumAsync(this IDbAsyncEnumerable<float> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<float> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					sum += (double)e.Current;
				}
			}
			return (float)sum;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0007681E File Offset: 0x00074A1E
		internal static Task<float?> SumAsync(this IDbAsyncEnumerable<float?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000769E0 File Offset: 0x00074BE0
		internal static async Task<float?> SumAsync(this IDbAsyncEnumerable<float?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<float?> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					float? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						float? num3 = e.Current;
						sum = num2 + (double)num3.GetValueOrDefault();
					}
				}
			}
			return new float?((float)sum);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00076A2E File Offset: 0x00074C2E
		internal static Task<double> SumAsync(this IDbAsyncEnumerable<double> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00076BC8 File Offset: 0x00074DC8
		internal static async Task<double> SumAsync(this IDbAsyncEnumerable<double> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<double> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					sum += e.Current;
				}
			}
			return sum;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00076C16 File Offset: 0x00074E16
		internal static Task<double?> SumAsync(this IDbAsyncEnumerable<double?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00076DD4 File Offset: 0x00074FD4
		internal static async Task<double?> SumAsync(this IDbAsyncEnumerable<double?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<double?> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					double? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						double? num3 = e.Current;
						sum = num2 + num3.GetValueOrDefault();
					}
				}
			}
			return new double?(sum);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00076E22 File Offset: 0x00075022
		internal static Task<decimal> SumAsync(this IDbAsyncEnumerable<decimal> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00076FBC File Offset: 0x000751BC
		internal static async Task<decimal> SumAsync(this IDbAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			using (IDbAsyncEnumerator<decimal> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					sum += e.Current;
				}
			}
			return sum;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0007700A File Offset: 0x0007520A
		internal static Task<decimal?> SumAsync(this IDbAsyncEnumerable<decimal?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000771CC File Offset: 0x000753CC
		internal static async Task<decimal?> SumAsync(this IDbAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			using (IDbAsyncEnumerator<decimal?> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					decimal? num = e.Current;
					if (num != null)
					{
						decimal d = sum;
						decimal? num2 = e.Current;
						sum = d + num2.GetValueOrDefault();
					}
				}
			}
			return new decimal?(sum);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0007721A File Offset: 0x0007541A
		internal static Task<double> AverageAsync(this IDbAsyncEnumerable<int> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000773E0 File Offset: 0x000755E0
		internal static async Task<double> AverageAsync(this IDbAsyncEnumerable<int> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<int> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						sum += unchecked((long)e.Current);
						count += 1L;
					}
				}
				if (count > 0L)
				{
					return (double)sum / (double)count;
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0007742E File Offset: 0x0007562E
		internal static Task<double?> AverageAsync(this IDbAsyncEnumerable<int?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00077618 File Offset: 0x00075818
		internal static async Task<double?> AverageAsync(this IDbAsyncEnumerable<int?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<int?> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						int? num = e.Current;
						if (num != null)
						{
							long num2 = sum;
							int? num3 = e.Current;
							sum = num2 + unchecked((long)num3.GetValueOrDefault());
							count += 1L;
						}
					}
				}
				if (count > 0L)
				{
					return new double?((double)sum / (double)count);
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00077666 File Offset: 0x00075866
		internal static Task<double> AverageAsync(this IDbAsyncEnumerable<long> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0007782C File Offset: 0x00075A2C
		internal static async Task<double> AverageAsync(this IDbAsyncEnumerable<long> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<long> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						sum += e.Current;
						count += 1L;
					}
				}
				if (count > 0L)
				{
					return (double)sum / (double)count;
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0007787A File Offset: 0x00075A7A
		internal static Task<double?> AverageAsync(this IDbAsyncEnumerable<long?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00077A64 File Offset: 0x00075C64
		internal static async Task<double?> AverageAsync(this IDbAsyncEnumerable<long?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<long?> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						long? num = e.Current;
						if (num != null)
						{
							long num2 = sum;
							long? num3 = e.Current;
							sum = num2 + num3.GetValueOrDefault();
							count += 1L;
						}
					}
				}
				if (count > 0L)
				{
					return new double?((double)sum / (double)count);
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00077AB2 File Offset: 0x00075CB2
		internal static Task<float> AverageAsync(this IDbAsyncEnumerable<float> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00077C80 File Offset: 0x00075E80
		internal static async Task<float> AverageAsync(this IDbAsyncEnumerable<float> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<float> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					sum += (double)e.Current;
					checked
					{
						count += 1L;
					}
				}
			}
			if (count > 0L)
			{
				return (float)(sum / (double)count);
			}
			throw Error.EmptySequence();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00077CCE File Offset: 0x00075ECE
		internal static Task<float?> AverageAsync(this IDbAsyncEnumerable<float?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00077EC0 File Offset: 0x000760C0
		internal static async Task<float?> AverageAsync(this IDbAsyncEnumerable<float?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<float?> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					float? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						float? num3 = e.Current;
						sum = num2 + (double)num3.GetValueOrDefault();
						checked
						{
							count += 1L;
						}
					}
				}
			}
			if (count > 0L)
			{
				return new float?((float)(sum / (double)count));
			}
			throw Error.EmptySequence();
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00077F0E File Offset: 0x0007610E
		internal static Task<double> AverageAsync(this IDbAsyncEnumerable<double> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000780DC File Offset: 0x000762DC
		internal static async Task<double> AverageAsync(this IDbAsyncEnumerable<double> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<double> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					sum += e.Current;
					checked
					{
						count += 1L;
					}
				}
			}
			if (count > 0L)
			{
				return (double)((float)(sum / (double)count));
			}
			throw Error.EmptySequence();
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0007812A File Offset: 0x0007632A
		internal static Task<double?> AverageAsync(this IDbAsyncEnumerable<double?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0007831C File Offset: 0x0007651C
		internal static async Task<double?> AverageAsync(this IDbAsyncEnumerable<double?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<double?> e = source.GetAsyncEnumerator())
			{
				while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
				{
					cancellationToken.ThrowIfCancellationRequested();
					double? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						double? num3 = e.Current;
						sum = num2 + num3.GetValueOrDefault();
						checked
						{
							count += 1L;
						}
					}
				}
			}
			if (count > 0L)
			{
				return new double?((double)((float)(sum / (double)count)));
			}
			throw Error.EmptySequence();
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0007836A File Offset: 0x0007656A
		internal static Task<decimal> AverageAsync(this IDbAsyncEnumerable<decimal> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0007853C File Offset: 0x0007673C
		internal static async Task<decimal> AverageAsync(this IDbAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<decimal> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						sum += e.Current;
						count += 1L;
					}
				}
				if (count > 0L)
				{
					return sum / count;
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0007858A File Offset: 0x0007678A
		internal static Task<decimal?> AverageAsync(this IDbAsyncEnumerable<decimal?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00078780 File Offset: 0x00076980
		internal static async Task<decimal?> AverageAsync(this IDbAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<decimal?> e = source.GetAsyncEnumerator())
				{
					while (await e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						cancellationToken.ThrowIfCancellationRequested();
						decimal? num = e.Current;
						if (num != null)
						{
							decimal d = sum;
							decimal? num2 = e.Current;
							sum = d + num2.GetValueOrDefault();
							count += 1L;
						}
					}
				}
				if (count > 0L)
				{
					return new decimal?(sum / count);
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x02000297 RID: 663
		private class CastDbAsyncEnumerable<TResult> : IDbAsyncEnumerable<!0>, IDbAsyncEnumerable
		{
			// Token: 0x06001776 RID: 6006 RVA: 0x000787CE File Offset: 0x000769CE
			public CastDbAsyncEnumerable(IDbAsyncEnumerable sourceEnumerable)
			{
				this._underlyingEnumerable = sourceEnumerable;
			}

			// Token: 0x06001777 RID: 6007 RVA: 0x000787DD File Offset: 0x000769DD
			public IDbAsyncEnumerator<TResult> GetAsyncEnumerator()
			{
				return this._underlyingEnumerable.GetAsyncEnumerator().Cast<TResult>();
			}

			// Token: 0x06001778 RID: 6008 RVA: 0x000787EF File Offset: 0x000769EF
			IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
			{
				return this._underlyingEnumerable.GetAsyncEnumerator();
			}

			// Token: 0x04000855 RID: 2133
			private readonly IDbAsyncEnumerable _underlyingEnumerable;
		}

		// Token: 0x02000298 RID: 664
		private static class IdentityFunction<TElement>
		{
			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06001779 RID: 6009 RVA: 0x000787FF File Offset: 0x000769FF
			internal static Func<TElement, TElement> Instance
			{
				get
				{
					return (TElement x) => x;
				}
			}
		}
	}
}
