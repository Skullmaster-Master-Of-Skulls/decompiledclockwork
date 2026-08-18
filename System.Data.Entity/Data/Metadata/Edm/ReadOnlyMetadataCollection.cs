using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001EF RID: 495
	public class ReadOnlyMetadataCollection<T> : ReadOnlyCollection<T> where T : MetadataItem
	{
		// Token: 0x060020FD RID: 8445 RVA: 0x000745CE File Offset: 0x000727CE
		internal ReadOnlyMetadataCollection(IList<T> collection) : base(collection)
		{
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00017938 File Offset: 0x00015B38
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006B6 RID: 1718
		public virtual T this[string identity]
		{
			get
			{
				return ((MetadataCollection<T>)base.Items)[identity];
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x000745EA File Offset: 0x000727EA
		internal MetadataCollection<T> Source
		{
			get
			{
				return (MetadataCollection<T>)base.Items;
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000745F7 File Offset: 0x000727F7
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			return ((MetadataCollection<T>)base.Items).GetValue(identity, ignoreCase);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0007460B File Offset: 0x0007280B
		public virtual bool Contains(string identity)
		{
			return ((MetadataCollection<T>)base.Items).ContainsIdentity(identity);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0007461E File Offset: 0x0007281E
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			return ((MetadataCollection<T>)base.Items).TryGetValue(identity, ignoreCase, out item);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00074633 File Offset: 0x00072833
		public new ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(base.Items);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00074640 File Offset: 0x00072840
		public new virtual int IndexOf(T value)
		{
			return base.IndexOf(value);
		}

		// Token: 0x02000521 RID: 1313
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003E25 RID: 15909 RVA: 0x000E7BB3 File Offset: 0x000E5DB3
			internal Enumerator(IList<T> collection)
			{
				this._parent = collection;
				this._nextIndex = 0;
				this._current = default(T);
			}

			// Token: 0x17000B12 RID: 2834
			// (get) Token: 0x06003E26 RID: 15910 RVA: 0x000E7BCF File Offset: 0x000E5DCF
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000B13 RID: 2835
			// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000E7BD7 File Offset: 0x000E5DD7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003E28 RID: 15912 RVA: 0x000089D0 File Offset: 0x00006BD0
			public void Dispose()
			{
			}

			// Token: 0x06003E29 RID: 15913 RVA: 0x000E7BE4 File Offset: 0x000E5DE4
			public bool MoveNext()
			{
				if (this._nextIndex < this._parent.Count)
				{
					this._current = this._parent[this._nextIndex];
					this._nextIndex++;
					return true;
				}
				this._current = default(T);
				return false;
			}

			// Token: 0x06003E2A RID: 15914 RVA: 0x000E7C38 File Offset: 0x000E5E38
			public void Reset()
			{
				this._current = default(T);
				this._nextIndex = 0;
			}

			// Token: 0x04001B45 RID: 6981
			private int _nextIndex;

			// Token: 0x04001B46 RID: 6982
			private IList<T> _parent;

			// Token: 0x04001B47 RID: 6983
			private T _current;
		}
	}
}
