using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Apis.Json
{
	// Token: 0x0200001E RID: 30
	public static class JsonExplicitNull
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000036D4 File Offset: 0x000018D4
		public static IList<T> ForIList<T>()
		{
			return JsonExplicitNull.ExplicitNullList<T>.Instance;
		}

		// Token: 0x02000042 RID: 66
		[JsonExplicitNull]
		private sealed class ExplicitNullList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x1700005B RID: 91
			public T this[int index]
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000141 RID: 321 RVA: 0x000046EA File Offset: 0x000028EA
			public int Count
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000142 RID: 322 RVA: 0x000046EA File Offset: 0x000028EA
			public bool IsReadOnly
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000143 RID: 323 RVA: 0x000046EA File Offset: 0x000028EA
			public void Add(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000144 RID: 324 RVA: 0x000046EA File Offset: 0x000028EA
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000145 RID: 325 RVA: 0x000046EA File Offset: 0x000028EA
			public bool Contains(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000146 RID: 326 RVA: 0x000046EA File Offset: 0x000028EA
			public void CopyTo(T[] array, int arrayIndex)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000147 RID: 327 RVA: 0x000046EA File Offset: 0x000028EA
			public IEnumerator<T> GetEnumerator()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000148 RID: 328 RVA: 0x000046EA File Offset: 0x000028EA
			public int IndexOf(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000149 RID: 329 RVA: 0x000046EA File Offset: 0x000028EA
			public void Insert(int index, T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600014A RID: 330 RVA: 0x000046EA File Offset: 0x000028EA
			public bool Remove(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600014B RID: 331 RVA: 0x000046EA File Offset: 0x000028EA
			public void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600014C RID: 332 RVA: 0x000046EA File Offset: 0x000028EA
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000089 RID: 137
			public static JsonExplicitNull.ExplicitNullList<T> Instance = new JsonExplicitNull.ExplicitNullList<T>();
		}
	}
}
