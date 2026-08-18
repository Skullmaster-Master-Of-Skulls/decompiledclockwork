using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000782 RID: 1922
	internal class ReadOnlySet<T> : ISet<T>, ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06005703 RID: 22275 RVA: 0x00178139 File Offset: 0x00176339
		public ReadOnlySet(ISet<T> set)
		{
			this._set = set;
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x00178148 File Offset: 0x00176348
		public bool Add(T item)
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x0017814F File Offset: 0x0017634F
		public void ExceptWith(IEnumerable<T> other)
		{
			this._set.ExceptWith(other);
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x0017815D File Offset: 0x0017635D
		public void IntersectWith(IEnumerable<T> other)
		{
			this._set.IntersectWith(other);
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0017816B File Offset: 0x0017636B
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSubsetOf(other);
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x00178179 File Offset: 0x00176379
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSupersetOf(other);
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x00178187 File Offset: 0x00176387
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsSubsetOf(other);
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x00178195 File Offset: 0x00176395
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsSupersetOf(other);
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x001781A3 File Offset: 0x001763A3
		public bool Overlaps(IEnumerable<T> other)
		{
			return this._set.Overlaps(other);
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x001781B1 File Offset: 0x001763B1
		public bool SetEquals(IEnumerable<T> other)
		{
			return this._set.SetEquals(other);
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x001781BF File Offset: 0x001763BF
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			this._set.SymmetricExceptWith(other);
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x001781CD File Offset: 0x001763CD
		public void UnionWith(IEnumerable<T> other)
		{
			this._set.UnionWith(other);
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x001781DB File Offset: 0x001763DB
		void ICollection<!0>.Add(T item)
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x001781E2 File Offset: 0x001763E2
		public void Clear()
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x001781E9 File Offset: 0x001763E9
		public bool Contains(T item)
		{
			return this._set.Contains(item);
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x001781F7 File Offset: 0x001763F7
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._set.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x00178206 File Offset: 0x00176406
		public int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x00178213 File Offset: 0x00176413
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x00178216 File Offset: 0x00176416
		public bool Remove(T item)
		{
			throw Error.DbPropertyValues_PropertyValueNamesAreReadonly();
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x0017821D File Offset: 0x0017641D
		public IEnumerator<T> GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x0017822A File Offset: 0x0017642A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x04002320 RID: 8992
		private readonly ISet<T> _set;
	}
}
