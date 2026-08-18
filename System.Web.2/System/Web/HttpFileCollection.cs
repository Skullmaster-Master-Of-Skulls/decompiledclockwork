using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200009F RID: 159
	public sealed class HttpFileCollection : NameObjectCollectionBase
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x000171CC File Offset: 0x000153CC
		internal HttpFileCollection() : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000171DC File Offset: 0x000153DC
		internal HttpFileCollection(HttpFileCollection col) : this()
		{
			for (int i = 0; i < col.Count; i++)
			{
				this.ThrowIfMaxHttpCollectionKeysExceeded();
				string name = col.BaseGetKey(i);
				object value = col.BaseGet(i);
				base.BaseAdd(name, value);
			}
			base.IsReadOnly = col.IsReadOnly;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001722C File Offset: 0x0001542C
		public void CopyTo(Array dest, int index)
		{
			if (this._all == null)
			{
				int count = this.Count;
				HttpPostedFile[] array = new HttpPostedFile[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this.Get(i);
				}
				this._all = array;
			}
			if (this._all != null)
			{
				this._all.CopyTo(dest, index);
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00017281 File Offset: 0x00015481
		internal void AddFile(string key, HttpPostedFile file)
		{
			this.ThrowIfMaxHttpCollectionKeysExceeded();
			this._all = null;
			this._allKeys = null;
			base.BaseAdd(key, file);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00016458 File Offset: 0x00014658
		private void ThrowIfMaxHttpCollectionKeysExceeded()
		{
			if (this.Count >= AppSettings.MaxHttpCollectionKeys)
			{
				throw new InvalidOperationException(SR.GetString("CollectionCountExceeded_HttpValueCollection", new object[]
				{
					AppSettings.MaxHttpCollectionKeys
				}));
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000172A0 File Offset: 0x000154A0
		internal void EnableGranularValidation(ValidateStringCallback validationCallback)
		{
			this._filesAwaitingValidation = new HashSet<HttpPostedFile>();
			for (int i = 0; i < this.Count; i++)
			{
				this._filesAwaitingValidation.Add((HttpPostedFile)base.BaseGet(i));
			}
			this._validationCallback = validationCallback;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000172E8 File Offset: 0x000154E8
		private void EnsureFileValidated(HttpPostedFile file)
		{
			if (this._filesAwaitingValidation == null)
			{
				return;
			}
			if (!this._filesAwaitingValidation.Contains(file))
			{
				return;
			}
			this._validationCallback(null, file.FileName);
			this._filesAwaitingValidation.Remove(file);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00017324 File Offset: 0x00015524
		public HttpPostedFile Get(string name)
		{
			HttpPostedFile httpPostedFile = (HttpPostedFile)base.BaseGet(name);
			if (httpPostedFile != null)
			{
				this.EnsureFileValidated(httpPostedFile);
			}
			return httpPostedFile;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001734C File Offset: 0x0001554C
		public IList<HttpPostedFile> GetMultiple(string name)
		{
			List<HttpPostedFile> list = new List<HttpPostedFile>();
			for (int i = 0; i < this.Count; i++)
			{
				string key = this.GetKey(i);
				if (string.Equals(key, name, StringComparison.InvariantCultureIgnoreCase))
				{
					list.Add(this.Get(i));
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x170003EF RID: 1007
		public HttpPostedFile this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000173A0 File Offset: 0x000155A0
		public HttpPostedFile Get(int index)
		{
			HttpPostedFile httpPostedFile = (HttpPostedFile)base.BaseGet(index);
			if (httpPostedFile != null)
			{
				this.EnsureFileValidated(httpPostedFile);
			}
			return httpPostedFile;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000166A9 File Offset: 0x000148A9
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x170003F0 RID: 1008
		public HttpPostedFile this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000173CE File Offset: 0x000155CE
		public string[] AllKeys
		{
			get
			{
				if (this._allKeys == null)
				{
					this._allKeys = base.BaseGetAllKeys();
				}
				return this._allKeys;
			}
		}

		// Token: 0x040003B3 RID: 947
		private HttpPostedFile[] _all;

		// Token: 0x040003B4 RID: 948
		private string[] _allKeys;

		// Token: 0x040003B5 RID: 949
		private ValidateStringCallback _validationCallback;

		// Token: 0x040003B6 RID: 950
		private HashSet<HttpPostedFile> _filesAwaitingValidation;
	}
}
