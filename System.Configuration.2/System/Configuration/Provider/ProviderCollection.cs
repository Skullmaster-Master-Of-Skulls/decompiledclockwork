using System;
using System.Collections;

namespace System.Configuration.Provider
{
	// Token: 0x020000C2 RID: 194
	public class ProviderCollection : IEnumerable, ICollection
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x00020790 File Offset: 0x0001E990
		public ProviderCollection()
		{
			this._Hashtable = new Hashtable(10, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000207AC File Offset: 0x0001E9AC
		public virtual void Add(ProviderBase provider)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (provider.Name == null || provider.Name.Length < 1)
			{
				throw new ArgumentException(SR.GetString("Config_provider_name_null_or_empty"));
			}
			this._Hashtable.Add(provider.Name, provider);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00020817 File Offset: 0x0001EA17
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this._Hashtable.Remove(name);
		}

		// Token: 0x1700023D RID: 573
		public ProviderBase this[string name]
		{
			get
			{
				return this._Hashtable[name] as ProviderBase;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00020850 File Offset: 0x0001EA50
		public IEnumerator GetEnumerator()
		{
			return this._Hashtable.Values.GetEnumerator();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00020862 File Offset: 0x0001EA62
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00020874 File Offset: 0x0001EA74
		public void Clear()
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this._Hashtable.Clear();
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00020899 File Offset: 0x0001EA99
		public int Count
		{
			get
			{
				return this._Hashtable.Count;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00008751 File Offset: 0x00006951
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x000101B8 File Offset: 0x0000E3B8
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public void CopyTo(ProviderBase[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000208A6 File Offset: 0x0001EAA6
		void ICollection.CopyTo(Array array, int index)
		{
			this._Hashtable.Values.CopyTo(array, index);
		}

		// Token: 0x04000469 RID: 1129
		private Hashtable _Hashtable;

		// Token: 0x0400046A RID: 1130
		private bool _ReadOnly;
	}
}
