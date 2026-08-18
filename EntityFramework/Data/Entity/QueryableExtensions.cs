using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x020001D5 RID: 469
	public static class QueryableExtensions
	{
		// Token: 0x06000F7F RID: 3967 RVA: 0x000418FC File Offset: 0x0003FAFC
		public static IQueryable<T> Include<T>(this IQueryable<T> source, string path)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotEmpty(path, "path");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery != null)
			{
				return dbQuery.Include(path);
			}
			ObjectQuery<T> objectQuery = source as ObjectQuery<T>;
			if (objectQuery != null)
			{
				return objectQuery.Include(path);
			}
			return QueryableExtensions.CommonInclude<IQueryable<T>>(source, path);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0004194C File Offset: 0x0003FB4C
		public static IQueryable Include(this IQueryable source, string path)
		{
			Check.NotNull<IQueryable>(source, "source");
			Check.NotEmpty(path, "path");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonInclude<IQueryable>(source, path);
			}
			return dbQuery.Include(path);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x000419A0 File Offset: 0x0003FBA0
		private static T CommonInclude<T>(T source, string path)
		{
			MethodInfo runtimeMethod = source.GetType().GetRuntimeMethod("Include", (MethodInfo p) => p.IsPublic && !p.IsStatic, new Type[][]
			{
				new Type[]
				{
					typeof(string)
				},
				new Type[]
				{
					typeof(IComparable)
				},
				new Type[]
				{
					typeof(ICloneable)
				},
				new Type[]
				{
					typeof(IComparable<string>)
				},
				new Type[]
				{
					typeof(IEnumerable<char>)
				},
				new Type[]
				{
					typeof(IEnumerable)
				},
				new Type[]
				{
					typeof(IEquatable<string>)
				},
				new Type[]
				{
					typeof(object)
				}
			});
			if (runtimeMethod != null && typeof(T).IsAssignableFrom(runtimeMethod.ReturnType))
			{
				return (T)((object)runtimeMethod.Invoke(source, new object[]
				{
					path
				}));
			}
			return source;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00041AF0 File Offset: 0x0003FCF0
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotNull<Expression<Func<T, TProperty>>>(path, "path");
			string text;
			if (!DbHelpers.TryParsePath(path.Body, out text) || text == null)
			{
				throw new ArgumentException(Strings.DbExtensions_InvalidIncludePathExpression, "path");
			}
			return source.Include(text);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00041B40 File Offset: 0x0003FD40
		public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> source) where T : class
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsNoTracking<IQueryable<T>>(source);
			}
			return dbQuery.AsNoTracking();
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00041B70 File Offset: 0x0003FD70
		public static IQueryable AsNoTracking(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsNoTracking<IQueryable>(source);
			}
			return dbQuery.AsNoTracking();
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00041BA0 File Offset: 0x0003FDA0
		private static T CommonAsNoTracking<T>(T source) where T : class
		{
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return (T)((object)DbHelpers.CreateNoTrackingQuery(objectQuery));
			}
			MethodInfo publicInstanceMethod = source.GetType().GetPublicInstanceMethod("AsNoTracking", new Type[0]);
			if (publicInstanceMethod != null && typeof(T).IsAssignableFrom(publicInstanceMethod.ReturnType))
			{
				return (T)((object)publicInstanceMethod.Invoke(source, null));
			}
			return source;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00041C1C File Offset: 0x0003FE1C
		[Obsolete("LINQ queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public static IQueryable<T> AsStreaming<T>(this IQueryable<T> source)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsStreaming<IQueryable<T>>(source);
			}
			return dbQuery.AsStreaming();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00041C4C File Offset: 0x0003FE4C
		[Obsolete("LINQ queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public static IQueryable AsStreaming(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsStreaming<IQueryable>(source);
			}
			return dbQuery.AsStreaming();
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00041C7C File Offset: 0x0003FE7C
		private static T CommonAsStreaming<T>(T source) where T : class
		{
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return (T)((object)DbHelpers.CreateStreamingQuery(objectQuery));
			}
			MethodInfo publicInstanceMethod = source.GetType().GetPublicInstanceMethod("AsStreaming", new Type[0]);
			if (publicInstanceMethod != null && typeof(T).IsAssignableFrom(publicInstanceMethod.ReturnType))
			{
				return (T)((object)publicInstanceMethod.Invoke(source, null));
			}
			return source;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00041CF8 File Offset: 0x0003FEF8
		internal static IQueryable<T> WithExecutionStrategy<T>(this IQueryable<T> source, IDbExecutionStrategy executionStrategy)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonWithExecutionStrategy<IQueryable<T>>(source, executionStrategy);
			}
			return dbQuery.WithExecutionStrategy(executionStrategy);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00041D2C File Offset: 0x0003FF2C
		internal static IQueryable WithExecutionStrategy(this IQueryable source, IDbExecutionStrategy executionStrategy)
		{
			Check.NotNull<IQueryable>(source, "source");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonWithExecutionStrategy<IQueryable>(source, executionStrategy);
			}
			return dbQuery.WithExecutionStrategy(executionStrategy);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00041D60 File Offset: 0x0003FF60
		private static T CommonWithExecutionStrategy<T>(T source, IDbExecutionStrategy executionStrategy) where T : class
		{
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return (T)((object)DbHelpers.CreateQueryWithExecutionStrategy(objectQuery, executionStrategy));
			}
			MethodInfo publicInstanceMethod = source.GetType().GetPublicInstanceMethod("WithExecutionStrategy", new Type[0]);
			if (publicInstanceMethod != null && typeof(T).IsAssignableFrom(publicInstanceMethod.ReturnType))
			{
				return (T)((object)publicInstanceMethod.Invoke(source, new object[]
				{
					executionStrategy
				}));
			}
			return source;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00041DE8 File Offset: 0x0003FFE8
		public static void Load(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			using (IEnumerator enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
				}
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00041E34 File Offset: 0x00040034
		public static Task LoadAsync(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.LoadAsync(CancellationToken.None);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00041E4F File Offset: 0x0004004F
		public static Task LoadAsync(this IQueryable source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.ForEachAsync(delegate(object e)
			{
			}, cancellationToken);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00041E81 File Offset: 0x00040081
		public static Task ForEachAsync(this IQueryable source, Action<object> action)
		{
			Check.NotNull<IQueryable>(source, "source");
			Check.NotNull<Action<object>>(action, "action");
			return source.AsDbAsyncEnumerable().ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00041EAC File Offset: 0x000400AC
		public static Task ForEachAsync(this IQueryable source, Action<object> action, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable>(source, "source");
			Check.NotNull<Action<object>>(action, "action");
			return source.AsDbAsyncEnumerable().ForEachAsync(action, cancellationToken);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00041ED3 File Offset: 0x000400D3
		public static Task ForEachAsync<T>(this IQueryable<T> source, Action<T> action)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotNull<Action<T>>(action, "action");
			return source.AsDbAsyncEnumerable<T>().ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00041EFE File Offset: 0x000400FE
		public static Task ForEachAsync<T>(this IQueryable<T> source, Action<T> action, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotNull<Action<T>>(action, "action");
			return source.AsDbAsyncEnumerable<T>().ForEachAsync(action, cancellationToken);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00041F25 File Offset: 0x00040125
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<List<object>> ToListAsync(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.AsDbAsyncEnumerable().ToListAsync<object>();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00041F3E File Offset: 0x0004013E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<List<object>> ToListAsync(this IQueryable source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.AsDbAsyncEnumerable().ToListAsync(cancellationToken);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00041F58 File Offset: 0x00040158
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToListAsync<TSource>();
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00041F71 File Offset: 0x00040171
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToListAsync(cancellationToken);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00041F8B File Offset: 0x0004018B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToArrayAsync<TSource>();
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00041FA4 File Offset: 0x000401A4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToArrayAsync(cancellationToken);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00041FBE File Offset: 0x000401BE
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00041FE4 File Offset: 0x000401E4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, cancellationToken);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0004200B File Offset: 0x0004020B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, comparer);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00042032 File Offset: 0x00040232
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, comparer, cancellationToken);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0004205A File Offset: 0x0004025A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0004208D File Offset: 0x0004028D
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector, cancellationToken);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000420C1 File Offset: 0x000402C1
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector, comparer);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000420F5 File Offset: 0x000402F5
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector, comparer, cancellationToken);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004212B File Offset: 0x0004032B
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.FirstAsync(CancellationToken.None);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00042144 File Offset: 0x00040344
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._first.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000421B6 File Offset: 0x000403B6
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.FirstAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000421DC File Offset: 0x000403DC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._first_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00042263 File Offset: 0x00040463
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.FirstOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004227C File Offset: 0x0004047C
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._firstOrDefault.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000422EE File Offset: 0x000404EE
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.FirstOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00042314 File Offset: 0x00040514
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._firstOrDefault_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004239B File Offset: 0x0004059B
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.SingleAsync(CancellationToken.None);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000423B4 File Offset: 0x000405B4
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._single.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00042426 File Offset: 0x00040626
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.SingleAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0004244C File Offset: 0x0004064C
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._single_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000424D3 File Offset: 0x000406D3
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.SingleOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000424EC File Offset: 0x000406EC
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._singleOrDefault.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0004255E File Offset: 0x0004075E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.SingleOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00042584 File Offset: 0x00040784
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._singleOrDefault_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004260B File Offset: 0x0004080B
		public static Task<bool> ContainsAsync<TSource>(this IQueryable<TSource> source, TSource item)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.ContainsAsync(item, CancellationToken.None);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00042628 File Offset: 0x00040828
		public static Task<bool> ContainsAsync<TSource>(this IQueryable<TSource> source, TSource item, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._contains.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Constant(item, typeof(TSource))
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000426B2 File Offset: 0x000408B2
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AnyAsync(CancellationToken.None);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000426CC File Offset: 0x000408CC
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._any.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004273E File Offset: 0x0004093E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.AnyAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00042764 File Offset: 0x00040964
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._any_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000427EB File Offset: 0x000409EB
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<bool> AllAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.AllAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00042814 File Offset: 0x00040A14
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<bool> AllAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._all_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0004289B File Offset: 0x00040A9B
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.CountAsync(CancellationToken.None);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000428B4 File Offset: 0x00040AB4
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._count.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00042926 File Offset: 0x00040B26
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.CountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0004294C File Offset: 0x00040B4C
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._count_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000429D3 File Offset: 0x00040BD3
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.LongCountAsync(CancellationToken.None);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000429EC File Offset: 0x00040BEC
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._longCount.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00042A5E File Offset: 0x00040C5E
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.LongCountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00042A84 File Offset: 0x00040C84
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._longCount_Predicate.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00042B0B File Offset: 0x00040D0B
		public static Task<TSource> MinAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.MinAsync(CancellationToken.None);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00042B24 File Offset: 0x00040D24
		public static Task<TSource> MinAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._min.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00042B96 File Offset: 0x00040D96
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TResult> MinAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			return source.MinAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00042BBC File Offset: 0x00040DBC
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<TResult> MinAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TResult>(Expression.Call(null, QueryableExtensions._min_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource),
					typeof(TResult)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00042C50 File Offset: 0x00040E50
		public static Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.MaxAsync(CancellationToken.None);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00042C6C File Offset: 0x00040E6C
		public static Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._max.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00042CDE File Offset: 0x00040EDE
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TResult> MaxAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			return source.MaxAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00042D04 File Offset: 0x00040F04
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<TResult> MaxAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TResult>(Expression.Call(null, QueryableExtensions._max_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource),
					typeof(TResult)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00042D98 File Offset: 0x00040F98
		public static Task<int> SumAsync(this IQueryable<int> source)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00042DB4 File Offset: 0x00040FB4
		public static Task<int> SumAsync(this IQueryable<int> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._sum_Int, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00042E0C File Offset: 0x0004100C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int?> SumAsync(this IQueryable<int?> source)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00042E28 File Offset: 0x00041028
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int?> SumAsync(this IQueryable<int?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int?>(Expression.Call(null, QueryableExtensions._sum_IntNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00042E80 File Offset: 0x00041080
		public static Task<long> SumAsync(this IQueryable<long> source)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00042E9C File Offset: 0x0004109C
		public static Task<long> SumAsync(this IQueryable<long> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._sum_Long, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00042EF4 File Offset: 0x000410F4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<long?> SumAsync(this IQueryable<long?> source)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00042F10 File Offset: 0x00041110
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<long?> SumAsync(this IQueryable<long?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long?>(Expression.Call(null, QueryableExtensions._sum_LongNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00042F68 File Offset: 0x00041168
		public static Task<float> SumAsync(this IQueryable<float> source)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00042F84 File Offset: 0x00041184
		public static Task<float> SumAsync(this IQueryable<float> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._sum_Float, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00042FDC File Offset: 0x000411DC
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> SumAsync(this IQueryable<float?> source)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00042FF8 File Offset: 0x000411F8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> SumAsync(this IQueryable<float?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._sum_FloatNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00043050 File Offset: 0x00041250
		public static Task<double> SumAsync(this IQueryable<double> source)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0004306C File Offset: 0x0004126C
		public static Task<double> SumAsync(this IQueryable<double> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._sum_Double, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000430C4 File Offset: 0x000412C4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> SumAsync(this IQueryable<double?> source)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000430E0 File Offset: 0x000412E0
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> SumAsync(this IQueryable<double?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._sum_DoubleNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00043138 File Offset: 0x00041338
		public static Task<decimal> SumAsync(this IQueryable<decimal> source)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00043154 File Offset: 0x00041354
		public static Task<decimal> SumAsync(this IQueryable<decimal> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._sum_Decimal, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000431AC File Offset: 0x000413AC
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> SumAsync(this IQueryable<decimal?> source)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000431C8 File Offset: 0x000413C8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> SumAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._sum_DecimalNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00043220 File Offset: 0x00041420
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00043248 File Offset: 0x00041448
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._sum_Int_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000432CF File Offset: 0x000414CF
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000432F8 File Offset: 0x000414F8
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<int?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int?>(Expression.Call(null, QueryableExtensions._sum_IntNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004337F File Offset: 0x0004157F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<long> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000433A8 File Offset: 0x000415A8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<long> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._sum_Long_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0004342F File Offset: 0x0004162F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<long?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00043458 File Offset: 0x00041658
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<long?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long?>(Expression.Call(null, QueryableExtensions._sum_LongNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000434DF File Offset: 0x000416DF
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00043508 File Offset: 0x00041708
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<float> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._sum_Float_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004358F File Offset: 0x0004178F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000435B8 File Offset: 0x000417B8
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._sum_FloatNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004363F File Offset: 0x0004183F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00043668 File Offset: 0x00041868
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._sum_Double_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000436EF File Offset: 0x000418EF
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00043718 File Offset: 0x00041918
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._sum_DoubleNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0004379F File Offset: 0x0004199F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000437C8 File Offset: 0x000419C8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<decimal> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._sum_Decimal_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0004384F File Offset: 0x00041A4F
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00043878 File Offset: 0x00041A78
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._sum_DecimalNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000438FF File Offset: 0x00041AFF
		public static Task<double> AverageAsync(this IQueryable<int> source)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00043918 File Offset: 0x00041B18
		public static Task<double> AverageAsync(this IQueryable<int> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Int, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00043970 File Offset: 0x00041B70
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync(this IQueryable<int?> source)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0004398C File Offset: 0x00041B8C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync(this IQueryable<int?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_IntNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000439E4 File Offset: 0x00041BE4
		public static Task<double> AverageAsync(this IQueryable<long> source)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00043A00 File Offset: 0x00041C00
		public static Task<double> AverageAsync(this IQueryable<long> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Long, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00043A58 File Offset: 0x00041C58
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync(this IQueryable<long?> source)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00043A74 File Offset: 0x00041C74
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync(this IQueryable<long?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_LongNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00043ACC File Offset: 0x00041CCC
		public static Task<float> AverageAsync(this IQueryable<float> source)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00043AE8 File Offset: 0x00041CE8
		public static Task<float> AverageAsync(this IQueryable<float> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._average_Float, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00043B40 File Offset: 0x00041D40
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> AverageAsync(this IQueryable<float?> source)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00043B5C File Offset: 0x00041D5C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> AverageAsync(this IQueryable<float?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._average_FloatNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00043BB4 File Offset: 0x00041DB4
		public static Task<double> AverageAsync(this IQueryable<double> source)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00043BD0 File Offset: 0x00041DD0
		public static Task<double> AverageAsync(this IQueryable<double> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Double, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00043C28 File Offset: 0x00041E28
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync(this IQueryable<double?> source)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00043C44 File Offset: 0x00041E44
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync(this IQueryable<double?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_DoubleNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00043C9C File Offset: 0x00041E9C
		public static Task<decimal> AverageAsync(this IQueryable<decimal> source)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00043CB8 File Offset: 0x00041EB8
		public static Task<decimal> AverageAsync(this IQueryable<decimal> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._average_Decimal, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00043D10 File Offset: 0x00041F10
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> AverageAsync(this IQueryable<decimal?> source)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00043D2C File Offset: 0x00041F2C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> AverageAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._average_DecimalNullable, new Expression[]
				{
					source.Expression
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00043D84 File Offset: 0x00041F84
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00043DAC File Offset: 0x00041FAC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Int_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00043E33 File Offset: 0x00042033
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00043E5C File Offset: 0x0004205C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_IntNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00043EE3 File Offset: 0x000420E3
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00043F0C File Offset: 0x0004210C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Long_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00043F93 File Offset: 0x00042193
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00043FBC File Offset: 0x000421BC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_LongNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00044043 File Offset: 0x00042243
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004406C File Offset: 0x0004226C
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._average_Float_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x000440F3 File Offset: 0x000422F3
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004411C File Offset: 0x0004231C
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<float?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._average_FloatNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000441A3 File Offset: 0x000423A3
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000441CC File Offset: 0x000423CC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Double_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00044253 File Offset: 0x00042453
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0004427C File Offset: 0x0004247C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_DoubleNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00044303 File Offset: 0x00042503
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0004432C File Offset: 0x0004252C
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._average_Decimal_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000443B3 File Offset: 0x000425B3
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000443DC File Offset: 0x000425DC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Task<decimal?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._average_DecimalNullable_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00044464 File Offset: 0x00042664
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static IQueryable<TSource> Skip<TSource>(this IQueryable<TSource> source, Expression<Func<int>> countAccessor)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<int>>>(countAccessor, "countAccessor");
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, QueryableExtensions._skip.MakeGenericMethod(new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				countAccessor.Body
			}));
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000444D4 File Offset: 0x000426D4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static IQueryable<TSource> Take<TSource>(this IQueryable<TSource> source, Expression<Func<int>> countAccessor)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<int>>>(countAccessor, "countAccessor");
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, QueryableExtensions._take.MakeGenericMethod(new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				countAccessor.Body
			}));
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00044544 File Offset: 0x00042744
		internal static ObjectQuery TryGetObjectQuery(this IQueryable source)
		{
			if (source == null)
			{
				return null;
			}
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return objectQuery;
			}
			IInternalQueryAdapter internalQueryAdapter = source as IInternalQueryAdapter;
			if (internalQueryAdapter != null)
			{
				return internalQueryAdapter.InternalQuery.ObjectQuery;
			}
			return null;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004457C File Offset: 0x0004277C
		private static IDbAsyncEnumerable AsDbAsyncEnumerable(this IQueryable source)
		{
			IDbAsyncEnumerable dbAsyncEnumerable = source as IDbAsyncEnumerable;
			if (dbAsyncEnumerable != null)
			{
				return dbAsyncEnumerable;
			}
			throw Error.IQueryable_Not_Async(string.Empty);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000445A0 File Offset: 0x000427A0
		private static IDbAsyncEnumerable<T> AsDbAsyncEnumerable<T>(this IQueryable<T> source)
		{
			IDbAsyncEnumerable<T> dbAsyncEnumerable = source as IDbAsyncEnumerable<T>;
			if (dbAsyncEnumerable != null)
			{
				return dbAsyncEnumerable;
			}
			throw Error.IQueryable_Not_Async("<" + typeof(T) + ">");
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000445D7 File Offset: 0x000427D7
		private static MethodInfo GetMethod(string methodName, Func<Type[]> getParameterTypes)
		{
			return QueryableExtensions.GetMethod(methodName, getParameterTypes, 0);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000445E1 File Offset: 0x000427E1
		private static MethodInfo GetMethod(string methodName, Func<Type, Type, Type[]> getParameterTypes)
		{
			return QueryableExtensions.GetMethod(methodName, getParameterTypes, 2);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000445EB File Offset: 0x000427EB
		private static MethodInfo GetMethod(string methodName, Func<Type, Type[]> getParameterTypes)
		{
			return QueryableExtensions.GetMethod(methodName, getParameterTypes, 1);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000445F8 File Offset: 0x000427F8
		private static MethodInfo GetMethod(string methodName, Delegate getParameterTypesDelegate, int genericArgumentsCount)
		{
			IEnumerable<MethodInfo> declaredMethods = typeof(Queryable).GetDeclaredMethods(methodName);
			foreach (MethodInfo methodInfo in declaredMethods)
			{
				Type[] genericArguments = methodInfo.GetGenericArguments();
				if (genericArguments.Length == genericArgumentsCount && QueryableExtensions.Matches(methodInfo, (Type[])getParameterTypesDelegate.DynamicInvoke(genericArguments)))
				{
					return methodInfo;
				}
			}
			return null;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00044680 File Offset: 0x00042880
		private static bool Matches(MethodInfo methodInfo, Type[] parameterTypes)
		{
			return (from p in methodInfo.GetParameters()
			select p.ParameterType).SequenceEqual(parameterTypes);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000446B0 File Offset: 0x000428B0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Called from an assert")]
		private static string PrettyPrint(MethodInfo getParameterTypesMethod, int genericArgumentsCount)
		{
			Type[] array = new Type[genericArgumentsCount];
			for (int i = 0; i < genericArgumentsCount; i++)
			{
				array[i] = typeof(object);
			}
			Type[] array2 = (Type[])getParameterTypesMethod.Invoke(null, array);
			string[] array3 = new string[array2.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array3[j] = array2[j].ToString();
			}
			return "(" + string.Join(", ", array3) + ")";
		}

		// Token: 0x04000431 RID: 1073
		private static readonly MethodInfo _first = QueryableExtensions.GetMethod("First", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x04000432 RID: 1074
		private static readonly MethodInfo _first_Predicate = QueryableExtensions.GetMethod("First", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x04000433 RID: 1075
		private static readonly MethodInfo _firstOrDefault = QueryableExtensions.GetMethod("FirstOrDefault", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x04000434 RID: 1076
		private static readonly MethodInfo _firstOrDefault_Predicate = QueryableExtensions.GetMethod("FirstOrDefault", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x04000435 RID: 1077
		private static readonly MethodInfo _single = QueryableExtensions.GetMethod("Single", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x04000436 RID: 1078
		private static readonly MethodInfo _single_Predicate = QueryableExtensions.GetMethod("Single", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x04000437 RID: 1079
		private static readonly MethodInfo _singleOrDefault = QueryableExtensions.GetMethod("SingleOrDefault", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x04000438 RID: 1080
		private static readonly MethodInfo _singleOrDefault_Predicate = QueryableExtensions.GetMethod("SingleOrDefault", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x04000439 RID: 1081
		private static readonly MethodInfo _contains = QueryableExtensions.GetMethod("Contains", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			T
		});

		// Token: 0x0400043A RID: 1082
		private static readonly MethodInfo _any = QueryableExtensions.GetMethod("Any", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x0400043B RID: 1083
		private static readonly MethodInfo _any_Predicate = QueryableExtensions.GetMethod("Any", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x0400043C RID: 1084
		private static readonly MethodInfo _all_Predicate = QueryableExtensions.GetMethod("All", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x0400043D RID: 1085
		private static readonly MethodInfo _count = QueryableExtensions.GetMethod("Count", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x0400043E RID: 1086
		private static readonly MethodInfo _count_Predicate = QueryableExtensions.GetMethod("Count", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x0400043F RID: 1087
		private static readonly MethodInfo _longCount = QueryableExtensions.GetMethod("LongCount", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x04000440 RID: 1088
		private static readonly MethodInfo _longCount_Predicate = QueryableExtensions.GetMethod("LongCount", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(bool)
				})
			})
		});

		// Token: 0x04000441 RID: 1089
		private static readonly MethodInfo _min = QueryableExtensions.GetMethod("Min", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x04000442 RID: 1090
		private static readonly MethodInfo _min_Selector = QueryableExtensions.GetMethod("Min", (Type T, Type U) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					U
				})
			})
		});

		// Token: 0x04000443 RID: 1091
		private static readonly MethodInfo _max = QueryableExtensions.GetMethod("Max", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			})
		});

		// Token: 0x04000444 RID: 1092
		private static readonly MethodInfo _max_Selector = QueryableExtensions.GetMethod("Max", (Type T, Type U) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					U
				})
			})
		});

		// Token: 0x04000445 RID: 1093
		private static readonly MethodInfo _sum_Int = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<int>)
		});

		// Token: 0x04000446 RID: 1094
		private static readonly MethodInfo _sum_IntNullable = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<int?>)
		});

		// Token: 0x04000447 RID: 1095
		private static readonly MethodInfo _sum_Long = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<long>)
		});

		// Token: 0x04000448 RID: 1096
		private static readonly MethodInfo _sum_LongNullable = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<long?>)
		});

		// Token: 0x04000449 RID: 1097
		private static readonly MethodInfo _sum_Float = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<float>)
		});

		// Token: 0x0400044A RID: 1098
		private static readonly MethodInfo _sum_FloatNullable = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<float?>)
		});

		// Token: 0x0400044B RID: 1099
		private static readonly MethodInfo _sum_Double = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<double>)
		});

		// Token: 0x0400044C RID: 1100
		private static readonly MethodInfo _sum_DoubleNullable = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<double?>)
		});

		// Token: 0x0400044D RID: 1101
		private static readonly MethodInfo _sum_Decimal = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<decimal>)
		});

		// Token: 0x0400044E RID: 1102
		private static readonly MethodInfo _sum_DecimalNullable = QueryableExtensions.GetMethod("Sum", () => new Type[]
		{
			typeof(IQueryable<decimal?>)
		});

		// Token: 0x0400044F RID: 1103
		private static readonly MethodInfo _sum_Int_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(int)
				})
			})
		});

		// Token: 0x04000450 RID: 1104
		private static readonly MethodInfo _sum_IntNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(int?)
				})
			})
		});

		// Token: 0x04000451 RID: 1105
		private static readonly MethodInfo _sum_Long_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(long)
				})
			})
		});

		// Token: 0x04000452 RID: 1106
		private static readonly MethodInfo _sum_LongNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(long?)
				})
			})
		});

		// Token: 0x04000453 RID: 1107
		private static readonly MethodInfo _sum_Float_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(float)
				})
			})
		});

		// Token: 0x04000454 RID: 1108
		private static readonly MethodInfo _sum_FloatNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(float?)
				})
			})
		});

		// Token: 0x04000455 RID: 1109
		private static readonly MethodInfo _sum_Double_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(double)
				})
			})
		});

		// Token: 0x04000456 RID: 1110
		private static readonly MethodInfo _sum_DoubleNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(double?)
				})
			})
		});

		// Token: 0x04000457 RID: 1111
		private static readonly MethodInfo _sum_Decimal_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(decimal)
				})
			})
		});

		// Token: 0x04000458 RID: 1112
		private static readonly MethodInfo _sum_DecimalNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(decimal?)
				})
			})
		});

		// Token: 0x04000459 RID: 1113
		private static readonly MethodInfo _average_Int = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<int>)
		});

		// Token: 0x0400045A RID: 1114
		private static readonly MethodInfo _average_IntNullable = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<int?>)
		});

		// Token: 0x0400045B RID: 1115
		private static readonly MethodInfo _average_Long = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<long>)
		});

		// Token: 0x0400045C RID: 1116
		private static readonly MethodInfo _average_LongNullable = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<long?>)
		});

		// Token: 0x0400045D RID: 1117
		private static readonly MethodInfo _average_Float = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<float>)
		});

		// Token: 0x0400045E RID: 1118
		private static readonly MethodInfo _average_FloatNullable = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<float?>)
		});

		// Token: 0x0400045F RID: 1119
		private static readonly MethodInfo _average_Double = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<double>)
		});

		// Token: 0x04000460 RID: 1120
		private static readonly MethodInfo _average_DoubleNullable = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<double?>)
		});

		// Token: 0x04000461 RID: 1121
		private static readonly MethodInfo _average_Decimal = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<decimal>)
		});

		// Token: 0x04000462 RID: 1122
		private static readonly MethodInfo _average_DecimalNullable = QueryableExtensions.GetMethod("Average", () => new Type[]
		{
			typeof(IQueryable<decimal?>)
		});

		// Token: 0x04000463 RID: 1123
		private static readonly MethodInfo _average_Int_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(int)
				})
			})
		});

		// Token: 0x04000464 RID: 1124
		private static readonly MethodInfo _average_IntNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(int?)
				})
			})
		});

		// Token: 0x04000465 RID: 1125
		private static readonly MethodInfo _average_Long_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(long)
				})
			})
		});

		// Token: 0x04000466 RID: 1126
		private static readonly MethodInfo _average_LongNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(long?)
				})
			})
		});

		// Token: 0x04000467 RID: 1127
		private static readonly MethodInfo _average_Float_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(float)
				})
			})
		});

		// Token: 0x04000468 RID: 1128
		private static readonly MethodInfo _average_FloatNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(float?)
				})
			})
		});

		// Token: 0x04000469 RID: 1129
		private static readonly MethodInfo _average_Double_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(double)
				})
			})
		});

		// Token: 0x0400046A RID: 1130
		private static readonly MethodInfo _average_DoubleNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(double?)
				})
			})
		});

		// Token: 0x0400046B RID: 1131
		private static readonly MethodInfo _average_Decimal_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(decimal)
				})
			})
		});

		// Token: 0x0400046C RID: 1132
		private static readonly MethodInfo _average_DecimalNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(Expression<>).MakeGenericType(new Type[]
			{
				typeof(Func<, >).MakeGenericType(new Type[]
				{
					T,
					typeof(decimal?)
				})
			})
		});

		// Token: 0x0400046D RID: 1133
		private static readonly MethodInfo _skip = QueryableExtensions.GetMethod("Skip", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(int)
		});

		// Token: 0x0400046E RID: 1134
		private static readonly MethodInfo _take = QueryableExtensions.GetMethod("Take", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				T
			}),
			typeof(int)
		});
	}
}
