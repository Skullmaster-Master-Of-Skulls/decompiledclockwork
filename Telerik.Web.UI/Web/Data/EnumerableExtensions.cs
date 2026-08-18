using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Telerik.Web.Data
{
	// Token: 0x02001B87 RID: 7047
	internal static class EnumerableExtensions
	{
		// Token: 0x06011131 RID: 69937 RVA: 0x003C3D3C File Offset: 0x003C1F3C
		internal static IEnumerable AsGenericEnumerable(this IEnumerable source)
		{
			Type typeFromHandle;
			if (source.TryGetGenericElementType(out typeFromHandle))
			{
				return source;
			}
			if (!source.TryGetFirstElementType(out typeFromHandle))
			{
				typeFromHandle = typeof(object);
			}
			Type type = typeof(GenericEnumerable<>).MakeGenericType(new Type[]
			{
				typeFromHandle
			});
			object[] args = new object[]
			{
				source
			};
			return (IEnumerable)Activator.CreateInstance(type, args);
		}

		// Token: 0x06011132 RID: 69938 RVA: 0x003C3DA4 File Offset: 0x003C1FA4
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		internal static bool TryGetFirstElement(this IEnumerable source, out object firstElement)
		{
			firstElement = null;
			IList list = source as IList;
			if (list != null && list.Count > 0)
			{
				firstElement = list[0];
				return true;
			}
			using (IEnumerator enumerator = source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					firstElement = obj;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06011133 RID: 69939 RVA: 0x003C3E1C File Offset: 0x003C201C
		internal static bool TryGetFirstElementType(this IEnumerable source, out Type firstElementType)
		{
			firstElementType = null;
			object obj;
			if (source.TryGetFirstElement(out obj) && obj != null)
			{
				firstElementType = obj.GetType();
				return true;
			}
			return false;
		}

		// Token: 0x06011134 RID: 69940 RVA: 0x003C3E44 File Offset: 0x003C2044
		internal static bool TryGetGenericElementType(this IEnumerable source, out Type elementType)
		{
			elementType = null;
			Type type = source.GetType().FindGenericType(typeof(IEnumerable<>));
			if (type != null)
			{
				elementType = type.GetGenericArguments().First<Type>();
				return true;
			}
			return false;
		}

		// Token: 0x06011135 RID: 69941 RVA: 0x003C3E84 File Offset: 0x003C2084
		internal static int Count(this IEnumerable source)
		{
			int num = 0;
			foreach (object obj in source)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06011136 RID: 69942 RVA: 0x003C3ED4 File Offset: 0x003C20D4
		internal static IEnumerable CastToFirstElementType(this IEnumerable source)
		{
			Type type;
			Type type2;
			if (source.TryGetGenericElementType(out type) && source.TryGetFirstElementType(out type2))
			{
				if (EnumerableExtensions.IsElementTypeSpecial(type) || EnumerableExtensions.IsElementTypeSpecial(type2))
				{
					return source;
				}
				if (type2 != type && source.AllAreFromType(type2))
				{
					MethodInfo methodInfo = EnumerableExtensions.EnumerableCastMethod.MakeGenericMethod(new Type[]
					{
						type2
					});
					return (IEnumerable)methodInfo.Invoke(null, new IEnumerable[]
					{
						source
					});
				}
			}
			return source;
		}

		// Token: 0x06011137 RID: 69943 RVA: 0x003C3F70 File Offset: 0x003C2170
		private static bool AllAreFromType(this IEnumerable items, Type targetType)
		{
			return items.All((object item) => item != null && item.GetType() == targetType);
		}

		// Token: 0x06011138 RID: 69944 RVA: 0x003C3F9C File Offset: 0x003C219C
		private static bool IsElementTypeSpecial(Type elementType)
		{
			return elementType.IsCompatibleWith(typeof(DataRow)) || elementType.IsCompatibleWith(typeof(ICustomTypeDescriptor)) || elementType.IsCompatibleWith(typeof(XmlNode));
		}

		// Token: 0x06011139 RID: 69945 RVA: 0x003C3FD4 File Offset: 0x003C21D4
		internal static int IndexOf(this IEnumerable source, object item)
		{
			int num = 0;
			foreach (object objA in source)
			{
				if (object.Equals(objA, item))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x0601113A RID: 69946 RVA: 0x003C4038 File Offset: 0x003C2238
		internal static bool All(this IEnumerable source, Func<object, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (object arg in source)
			{
				if (!predicate(arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0601113B RID: 69947 RVA: 0x003C40AC File Offset: 0x003C22AC
		internal static object ElementAt(this IEnumerable source, int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IList list = source as IList;
			if (list != null && list.Count > 0)
			{
				return list[index];
			}
			foreach (object result in source)
			{
				if (index == 0)
				{
					return result;
				}
				index--;
			}
			return null;
		}

		// Token: 0x0601113C RID: 69948 RVA: 0x003C4330 File Offset: 0x003C2530
		public static IEnumerable<TSource> SelectRecursive<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>> recursiveSelector)
		{
			Stack<IEnumerator<TSource>> stack = new Stack<IEnumerator<TSource>>();
			stack.Push(source.GetEnumerator());
			try
			{
				while (stack.Count > 0)
				{
					if (stack.Peek().MoveNext())
					{
						TSource current = stack.Peek().Current;
						yield return current;
						stack.Push(recursiveSelector(current).GetEnumerator());
					}
					else
					{
						stack.Pop().Dispose();
					}
				}
			}
			finally
			{
				while (stack.Count > 0)
				{
					stack.Pop().Dispose();
				}
			}
			yield break;
		}

		// Token: 0x0601113D RID: 69949 RVA: 0x003C4354 File Offset: 0x003C2554
		public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
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
			return EnumerableExtensions.ZipIterator<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		// Token: 0x0601113E RID: 69950 RVA: 0x003C45A4 File Offset: 0x003C27A4
		private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			using (IEnumerator<TFirst> e = first.GetEnumerator())
			{
				using (IEnumerator<TSecond> e2 = second.GetEnumerator())
				{
					while (e.MoveNext() && e2.MoveNext())
					{
						yield return resultSelector(e.Current, e2.Current);
					}
				}
			}
			yield break;
		}

		// Token: 0x0601113F RID: 69951 RVA: 0x003C45D0 File Offset: 0x003C27D0
		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> sequence)
		{
			if (sequence == null)
			{
				return EnumerableExtensions.DefaultReadOnlyCollection<T>.Empty;
			}
			ReadOnlyCollection<T> readOnlyCollection = sequence as ReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection;
			}
			return new ReadOnlyCollection<T>(sequence.ToArray<T>());
		}

		// Token: 0x04004C6C RID: 19564
		private static readonly MethodInfo EnumerableCastMethod = typeof(Enumerable).GetMethod("Cast");

		// Token: 0x02001B88 RID: 7048
		private static class DefaultReadOnlyCollection<T>
		{
			// Token: 0x1700536E RID: 21358
			// (get) Token: 0x06011141 RID: 69953 RVA: 0x003C4618 File Offset: 0x003C2818
			internal static ReadOnlyCollection<T> Empty
			{
				get
				{
					if (EnumerableExtensions.DefaultReadOnlyCollection<T>.defaultCollection == null)
					{
						EnumerableExtensions.DefaultReadOnlyCollection<T>.defaultCollection = new ReadOnlyCollection<T>(new T[0]);
					}
					return EnumerableExtensions.DefaultReadOnlyCollection<T>.defaultCollection;
				}
			}

			// Token: 0x04004C6D RID: 19565
			private static ReadOnlyCollection<T> defaultCollection;
		}
	}
}
