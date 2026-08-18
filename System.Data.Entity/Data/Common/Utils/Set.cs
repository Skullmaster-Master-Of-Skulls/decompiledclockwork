using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x02000399 RID: 921
	internal class Set<TElement> : InternalBase, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060032F8 RID: 13048 RVA: 0x000C6F11 File Offset: 0x000C5111
		internal Set(Set<TElement> other) : this(other._values, other.Comparer)
		{
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000C6F25 File Offset: 0x000C5125
		internal Set() : this(null, null)
		{
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000C6F2F File Offset: 0x000C512F
		internal Set(IEnumerable<TElement> elements) : this(elements, null)
		{
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000C6F39 File Offset: 0x000C5139
		internal Set(IEqualityComparer<TElement> comparer) : this(null, comparer)
		{
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000C6F43 File Offset: 0x000C5143
		internal Set(IEnumerable<TElement> elements, IEqualityComparer<TElement> comparer)
		{
			this._values = new HashSet<TElement>(elements ?? Enumerable.Empty<TElement>(), comparer ?? EqualityComparer<TElement>.Default);
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000C6F6A File Offset: 0x000C516A
		internal int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000C6F77 File Offset: 0x000C5177
		internal IEqualityComparer<TElement> Comparer
		{
			get
			{
				return this._values.Comparer;
			}
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000C6F84 File Offset: 0x000C5184
		internal bool Contains(TElement element)
		{
			return this._values.Contains(element);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000C6F92 File Offset: 0x000C5192
		internal void Add(TElement element)
		{
			this._values.Add(element);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000C6FA4 File Offset: 0x000C51A4
		internal void AddRange(IEnumerable<TElement> elements)
		{
			foreach (TElement element in elements)
			{
				this.Add(element);
			}
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000C6FEC File Offset: 0x000C51EC
		internal void Remove(TElement element)
		{
			this._values.Remove(element);
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000C6FFB File Offset: 0x000C51FB
		internal void Clear()
		{
			this._values.Clear();
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000C7008 File Offset: 0x000C5208
		internal TElement[] ToArray()
		{
			return this._values.ToArray<TElement>();
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000C7015 File Offset: 0x000C5215
		internal bool SetEquals(Set<TElement> other)
		{
			return this._values.Count == other._values.Count && this._values.IsSubsetOf(other._values);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000C7042 File Offset: 0x000C5242
		internal bool IsSubsetOf(Set<TElement> other)
		{
			return this._values.IsSubsetOf(other._values);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000C7055 File Offset: 0x000C5255
		internal bool Overlaps(Set<TElement> other)
		{
			return this._values.Overlaps(other._values);
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000C7068 File Offset: 0x000C5268
		internal void Subtract(IEnumerable<TElement> other)
		{
			this._values.ExceptWith(other);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000C7078 File Offset: 0x000C5278
		internal Set<TElement> Difference(IEnumerable<TElement> other)
		{
			Set<TElement> set = new Set<TElement>(this);
			set.Subtract(other);
			return set;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000C7094 File Offset: 0x000C5294
		internal void Unite(IEnumerable<TElement> other)
		{
			this._values.UnionWith(other);
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000C70A4 File Offset: 0x000C52A4
		internal Set<TElement> Union(IEnumerable<TElement> other)
		{
			Set<TElement> set = new Set<TElement>(this);
			set.Unite(other);
			return set;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000C70C0 File Offset: 0x000C52C0
		internal void Intersect(Set<TElement> other)
		{
			this._values.IntersectWith(other._values);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000C70D4 File Offset: 0x000C52D4
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

		// Token: 0x0600330E RID: 13070 RVA: 0x000C70FA File Offset: 0x000C52FA
		internal Set<TElement> MakeReadOnly()
		{
			this._isReadOnly = true;
			return this;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000C7104 File Offset: 0x000C5304
		internal int GetElementsHashCode()
		{
			int num = 0;
			foreach (TElement obj in this)
			{
				num ^= this.Comparer.GetHashCode(obj);
			}
			return num;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000C7160 File Offset: 0x000C5360
		public HashSet<TElement>.Enumerator GetEnumerator()
		{
			return this._values.GetEnumerator();
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000089D0 File Offset: 0x00006BD0
		[Conditional("DEBUG")]
		private void AssertReadWrite()
		{
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000089D0 File Offset: 0x00006BD0
		[Conditional("DEBUG")]
		private void AssertSetCompatible(Set<TElement> other)
		{
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000C716D File Offset: 0x000C536D
		IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000C716D File Offset: 0x000C536D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000C717A File Offset: 0x000C537A
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedStringSorted(builder, this);
		}

		// Token: 0x04001667 RID: 5735
		internal static readonly IEqualityComparer<Set<TElement>> ValueComparer = new Set<TElement>.SetValueComparer();

		// Token: 0x04001668 RID: 5736
		internal static readonly Set<TElement> Empty = new Set<TElement>().MakeReadOnly();

		// Token: 0x04001669 RID: 5737
		private readonly HashSet<TElement> _values;

		// Token: 0x0400166A RID: 5738
		private bool _isReadOnly;

		// Token: 0x0200067B RID: 1659
		public class Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x060044CA RID: 17610 RVA: 0x000F87B1 File Offset: 0x000F69B1
			internal Enumerator(Dictionary<TElement, bool>.KeyCollection.Enumerator keys)
			{
				this.keys = keys;
			}

			// Token: 0x17000BB8 RID: 3000
			// (get) Token: 0x060044CB RID: 17611 RVA: 0x000F87C0 File Offset: 0x000F69C0
			public TElement Current
			{
				get
				{
					return this.keys.Current;
				}
			}

			// Token: 0x060044CC RID: 17612 RVA: 0x000F87CD File Offset: 0x000F69CD
			public void Dispose()
			{
				this.keys.Dispose();
			}

			// Token: 0x17000BB9 RID: 3001
			// (get) Token: 0x060044CD RID: 17613 RVA: 0x000F87DA File Offset: 0x000F69DA
			object IEnumerator.Current
			{
				get
				{
					return ((IEnumerator)this.keys).Current;
				}
			}

			// Token: 0x060044CE RID: 17614 RVA: 0x000F87EC File Offset: 0x000F69EC
			public bool MoveNext()
			{
				return this.keys.MoveNext();
			}

			// Token: 0x060044CF RID: 17615 RVA: 0x000F87F9 File Offset: 0x000F69F9
			void IEnumerator.Reset()
			{
				((IEnumerator)this.keys).Reset();
			}

			// Token: 0x04001FB9 RID: 8121
			private Dictionary<TElement, bool>.KeyCollection.Enumerator keys;
		}

		// Token: 0x0200067C RID: 1660
		private class SetValueComparer : IEqualityComparer<Set<TElement>>
		{
			// Token: 0x060044D0 RID: 17616 RVA: 0x000F880B File Offset: 0x000F6A0B
			bool IEqualityComparer<Set<!0>>.Equals(Set<TElement> x, Set<TElement> y)
			{
				return x.SetEquals(y);
			}

			// Token: 0x060044D1 RID: 17617 RVA: 0x000F8814 File Offset: 0x000F6A14
			int IEqualityComparer<Set<!0>>.GetHashCode(Set<TElement> obj)
			{
				return obj.GetElementsHashCode();
			}
		}
	}
}
