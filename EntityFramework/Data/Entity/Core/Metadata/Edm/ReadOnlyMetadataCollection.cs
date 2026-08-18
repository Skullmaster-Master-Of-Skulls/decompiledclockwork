using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200020B RID: 523
	public class ReadOnlyMetadataCollection<T> : ReadOnlyCollection<T> where T : MetadataItem
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x0004FA50 File Offset: 0x0004DC50
		internal ReadOnlyMetadataCollection() : base(new MetadataCollection<T>())
		{
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004FA5D File Offset: 0x0004DC5D
		internal ReadOnlyMetadataCollection(MetadataCollection<T> collection) : base(collection)
		{
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004FA66 File Offset: 0x0004DC66
		internal ReadOnlyMetadataCollection(List<T> list) : base(MetadataCollection<T>.Wrap(list))
		{
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0004FA74 File Offset: 0x0004DC74
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001E2 RID: 482
		public virtual T this[string identity]
		{
			get
			{
				return ((MetadataCollection<T>)base.Items)[identity];
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0004FA8C File Offset: 0x0004DC8C
		internal MetadataCollection<T> Source
		{
			get
			{
				MetadataCollection<T> result;
				try
				{
					result = (MetadataCollection<T>)base.Items;
				}
				finally
				{
					EventHandler sourceAccessed = this.SourceAccessed;
					if (sourceAccessed != null)
					{
						sourceAccessed(this, null);
					}
				}
				return result;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001318 RID: 4888 RVA: 0x0004FACC File Offset: 0x0004DCCC
		// (remove) Token: 0x06001319 RID: 4889 RVA: 0x0004FB04 File Offset: 0x0004DD04
		internal event EventHandler SourceAccessed;

		// Token: 0x0600131A RID: 4890 RVA: 0x0004FB39 File Offset: 0x0004DD39
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			return ((MetadataCollection<T>)base.Items).GetValue(identity, ignoreCase);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0004FB4D File Offset: 0x0004DD4D
		public virtual bool Contains(string identity)
		{
			return ((MetadataCollection<T>)base.Items).ContainsIdentity(identity);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0004FB60 File Offset: 0x0004DD60
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			return ((MetadataCollection<T>)base.Items).TryGetValue(identity, ignoreCase, out item);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004FB75 File Offset: 0x0004DD75
		public new ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(base.Items);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004FB82 File Offset: 0x0004DD82
		public new virtual int IndexOf(T value)
		{
			return base.IndexOf(value);
		}

		// Token: 0x0200020C RID: 524
		[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x0600131F RID: 4895 RVA: 0x0004FB8B File Offset: 0x0004DD8B
			internal Enumerator(IList<T> collection)
			{
				this._parent = collection;
				this._nextIndex = 0;
				this._current = default(T);
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004FBA7 File Offset: 0x0004DDA7
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06001321 RID: 4897 RVA: 0x0004FBAF File Offset: 0x0004DDAF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x0004FBBC File Offset: 0x0004DDBC
			public void Dispose()
			{
			}

			// Token: 0x06001323 RID: 4899 RVA: 0x0004FBC0 File Offset: 0x0004DDC0
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

			// Token: 0x06001324 RID: 4900 RVA: 0x0004FC14 File Offset: 0x0004DE14
			public void Reset()
			{
				this._current = default(T);
				this._nextIndex = 0;
			}

			// Token: 0x04000595 RID: 1429
			private int _nextIndex;

			// Token: 0x04000596 RID: 1430
			private readonly IList<T> _parent;

			// Token: 0x04000597 RID: 1431
			private T _current;
		}
	}
}
