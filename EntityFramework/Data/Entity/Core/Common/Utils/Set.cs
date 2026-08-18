using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000330 RID: 816
	internal class Set<TElement> : InternalBase, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06001C33 RID: 7219 RVA: 0x0008AE25 File Offset: 0x00089025
		internal Set(Set<TElement> other) : this(other._values, other.Comparer)
		{
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0008AE39 File Offset: 0x00089039
		internal Set() : this(null, null)
		{
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0008AE43 File Offset: 0x00089043
		internal Set(IEnumerable<TElement> elements) : this(elements, null)
		{
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0008AE4D File Offset: 0x0008904D
		internal Set(IEqualityComparer<TElement> comparer) : this(null, comparer)
		{
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0008AE57 File Offset: 0x00089057
		internal Set(IEnumerable<TElement> elements, IEqualityComparer<TElement> comparer)
		{
			this._values = new HashSet<TElement>(elements ?? Enumerable.Empty<TElement>(), comparer ?? EqualityComparer<TElement>.Default);
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0008AE7E File Offset: 0x0008907E
		internal int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0008AE8B File Offset: 0x0008908B
		internal IEqualityComparer<TElement> Comparer
		{
			get
			{
				return this._values.Comparer;
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0008AE98 File Offset: 0x00089098
		internal bool Contains(TElement element)
		{
			return this._values.Contains(element);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0008AEA6 File Offset: 0x000890A6
		internal void Add(TElement element)
		{
			this._values.Add(element);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0008AEB8 File Offset: 0x000890B8
		internal void AddRange(IEnumerable<TElement> elements)
		{
			foreach (TElement element in elements)
			{
				this.Add(element);
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0008AF00 File Offset: 0x00089100
		internal void Remove(TElement element)
		{
			this._values.Remove(element);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0008AF0F File Offset: 0x0008910F
		internal void Clear()
		{
			this._values.Clear();
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0008AF1C File Offset: 0x0008911C
		internal TElement[] ToArray()
		{
			return this._values.ToArray<TElement>();
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0008AF29 File Offset: 0x00089129
		internal bool SetEquals(Set<TElement> other)
		{
			return this._values.Count == other._values.Count && this._values.IsSubsetOf(other._values);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0008AF56 File Offset: 0x00089156
		internal bool IsSubsetOf(Set<TElement> other)
		{
			return this._values.IsSubsetOf(other._values);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0008AF69 File Offset: 0x00089169
		internal bool Overlaps(Set<TElement> other)
		{
			return this._values.Overlaps(other._values);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0008AF7C File Offset: 0x0008917C
		internal void Subtract(IEnumerable<TElement> other)
		{
			this._values.ExceptWith(other);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0008AF8C File Offset: 0x0008918C
		internal Set<TElement> Difference(IEnumerable<TElement> other)
		{
			Set<TElement> set = new Set<TElement>(this);
			set.Subtract(other);
			return set;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0008AFA8 File Offset: 0x000891A8
		internal void Unite(IEnumerable<TElement> other)
		{
			this._values.UnionWith(other);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0008AFB8 File Offset: 0x000891B8
		internal Set<TElement> Union(IEnumerable<TElement> other)
		{
			Set<TElement> set = new Set<TElement>(this);
			set.Unite(other);
			return set;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0008AFD4 File Offset: 0x000891D4
		internal void Intersect(Set<TElement> other)
		{
			this._values.IntersectWith(other._values);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0008AFE8 File Offset: 0x000891E8
		internal Set<TElement> AsReadOnly()
		{
			if (this._isReadOnly)
			{
				return this;
			}
			return new Set<TElement>(this)
			{
				_isReadOnly = true
			};
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0008B00E File Offset: 0x0008920E
		internal Set<TElement> MakeReadOnly()
		{
			this._isReadOnly = true;
			return this;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0008B018 File Offset: 0x00089218
		internal int GetElementsHashCode()
		{
			int num = 0;
			foreach (TElement obj in this)
			{
				num ^= this.Comparer.GetHashCode(obj);
			}
			return num;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0008B074 File Offset: 0x00089274
		public HashSet<TElement>.Enumerator GetEnumerator()
		{
			return this._values.GetEnumerator();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0008B081 File Offset: 0x00089281
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[Conditional("DEBUG")]
		private void AssertReadWrite()
		{
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0008B083 File Offset: 0x00089283
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[Conditional("DEBUG")]
		private void AssertSetCompatible(Set<TElement> other)
		{
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0008B085 File Offset: 0x00089285
		IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0008B092 File Offset: 0x00089292
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0008B09F File Offset: 0x0008929F
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedStringSorted(builder, this);
		}

		// Token: 0x040009C8 RID: 2504
		internal static readonly Set<TElement> Empty = new Set<TElement>().MakeReadOnly();

		// Token: 0x040009C9 RID: 2505
		private readonly HashSet<TElement> _values;

		// Token: 0x040009CA RID: 2506
		private bool _isReadOnly;

		// Token: 0x02000331 RID: 817
		public class Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06001C52 RID: 7250 RVA: 0x0008B0B9 File Offset: 0x000892B9
			internal Enumerator(Dictionary<TElement, bool>.KeyCollection.Enumerator keys)
			{
				this.keys = keys;
			}

			// Token: 0x17000315 RID: 789
			// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0008B0C8 File Offset: 0x000892C8
			public TElement Current
			{
				get
				{
					return this.keys.Current;
				}
			}

			// Token: 0x06001C54 RID: 7252 RVA: 0x0008B0D5 File Offset: 0x000892D5
			public void Dispose()
			{
				this.keys.Dispose();
			}

			// Token: 0x17000316 RID: 790
			// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0008B0E2 File Offset: 0x000892E2
			object IEnumerator.Current
			{
				get
				{
					return ((IEnumerator)this.keys).Current;
				}
			}

			// Token: 0x06001C56 RID: 7254 RVA: 0x0008B0F4 File Offset: 0x000892F4
			public bool MoveNext()
			{
				return this.keys.MoveNext();
			}

			// Token: 0x06001C57 RID: 7255 RVA: 0x0008B101 File Offset: 0x00089301
			void IEnumerator.Reset()
			{
				((IEnumerator)this.keys).Reset();
			}

			// Token: 0x040009CB RID: 2507
			private Dictionary<TElement, bool>.KeyCollection.Enumerator keys;
		}
	}
}
