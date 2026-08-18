using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.Common.Utils
{
	// Token: 0x0200039D RID: 925
	internal sealed class ThreadSafeList<T> : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600333A RID: 13114 RVA: 0x000C77A1 File Offset: 0x000C59A1
		internal ThreadSafeList()
		{
			this._list = new List<T>();
			this._lock = new ReaderWriterLockSlim();
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000C77C0 File Offset: 0x000C59C0
		public int Count
		{
			get
			{
				this._lock.EnterReadLock();
				int count;
				try
				{
					count = this._list.Count;
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return count;
			}
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000C7804 File Offset: 0x000C5A04
		public void Add(T item)
		{
			this._lock.EnterWriteLock();
			try
			{
				this._list.Add(item);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x170009FF RID: 2559
		public T this[int index]
		{
			get
			{
				this._lock.EnterReadLock();
				T result;
				try
				{
					result = this._list[index];
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return result;
			}
			set
			{
				this._lock.EnterWriteLock();
				try
				{
					this._list[index] = value;
				}
				finally
				{
					this._lock.ExitWriteLock();
				}
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000C78D0 File Offset: 0x000C5AD0
		public int IndexOf(T item)
		{
			this._lock.EnterReadLock();
			int result;
			try
			{
				result = this._list.IndexOf(item);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000C7914 File Offset: 0x000C5B14
		public void Insert(int index, T item)
		{
			this._lock.EnterWriteLock();
			try
			{
				this._list.Insert(index, item);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000C7958 File Offset: 0x000C5B58
		public void RemoveAt(int index)
		{
			this._lock.EnterWriteLock();
			try
			{
				this._list.RemoveAt(index);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000C799C File Offset: 0x000C5B9C
		public void Clear()
		{
			this._lock.EnterWriteLock();
			try
			{
				this._list.Clear();
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000C79E0 File Offset: 0x000C5BE0
		public bool Contains(T item)
		{
			this._lock.EnterReadLock();
			bool result;
			try
			{
				result = this._list.Contains(item);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000C7A24 File Offset: 0x000C5C24
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._lock.EnterWriteLock();
			try
			{
				this._list.CopyTo(array, arrayIndex);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000C7A68 File Offset: 0x000C5C68
		public bool Remove(T item)
		{
			this._lock.EnterWriteLock();
			bool result;
			try
			{
				result = this._list.Remove(item);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000C7AAC File Offset: 0x000C5CAC
		public IEnumerator<T> GetEnumerator()
		{
			this._lock.EnterReadLock();
			try
			{
				foreach (T t in this._list)
				{
					yield return t;
				}
				List<T>.Enumerator enumerator = default(List<T>.Enumerator);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			yield break;
			yield break;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000C7ABB File Offset: 0x000C5CBB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001673 RID: 5747
		private readonly ReaderWriterLockSlim _lock;

		// Token: 0x04001674 RID: 5748
		private List<T> _list;
	}
}
