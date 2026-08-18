using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutoMapper
{
	// Token: 0x0200003A RID: 58
	public class ThreadSafeList<T> : IEnumerable<T>, IEnumerable, IDisposable where T : class
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public void Add(T propertyMap)
		{
			this._lock.EnterWriteLock();
			try
			{
				this._propertyMaps.Add(propertyMap);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00005D1C File Offset: 0x00003F1C
		public T GetOrCreate(Predicate<T> predicate, Func<T> creatorFunc)
		{
			this._lock.EnterUpgradeableReadLock();
			T result;
			try
			{
				T t = this._propertyMaps.FirstOrDefault((T pm) => predicate(pm));
				if (t == null)
				{
					this._lock.EnterWriteLock();
					try
					{
						t = creatorFunc();
						this._propertyMaps.Add(t);
					}
					finally
					{
						this._lock.ExitWriteLock();
					}
				}
				result = t;
			}
			finally
			{
				this._lock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005DBC File Offset: 0x00003FBC
		public void Clear()
		{
			this._lock.EnterWriteLock();
			try
			{
				this._propertyMaps.Clear();
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005E00 File Offset: 0x00004000
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumeratorImpl();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005E00 File Offset: 0x00004000
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorImpl();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005E08 File Offset: 0x00004008
		private IEnumerator<T> GetEnumeratorImpl()
		{
			this._lock.EnterReadLock();
			IEnumerator<T> result;
			try
			{
				result = this._propertyMaps.ToList<T>().GetEnumerator();
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00005E58 File Offset: 0x00004058
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00005E67 File Offset: 0x00004067
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					ReaderWriterLockSlim @lock = this._lock;
					if (@lock != null)
					{
						@lock.Dispose();
					}
				}
				this._lock = null;
				this._disposed = true;
			}
		}

		// Token: 0x0400006C RID: 108
		private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

		// Token: 0x0400006D RID: 109
		private readonly IList<T> _propertyMaps = new List<T>();

		// Token: 0x0400006E RID: 110
		private bool _disposed;
	}
}
