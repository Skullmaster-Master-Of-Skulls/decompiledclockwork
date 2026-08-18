using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000095 RID: 149
	public sealed class HttpCookieCollection : NameObjectCollectionBase
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00016330 File Offset: 0x00014530
		internal HttpCookieCollection(HttpResponse response, bool readOnly) : base(StringComparer.OrdinalIgnoreCase)
		{
			this._response = response;
			base.IsReadOnly = readOnly;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001634B File Offset: 0x0001454B
		public HttpCookieCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00016358 File Offset: 0x00014558
		internal HttpCookieCollection(HttpCookieCollection col) : base(StringComparer.OrdinalIgnoreCase)
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000163AB File Offset: 0x000145AB
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x000163B3 File Offset: 0x000145B3
		internal bool Changed
		{
			get
			{
				return this._changed;
			}
			set
			{
				this._changed = value;
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000163BC File Offset: 0x000145BC
		internal void AddCookie(HttpCookie cookie, bool append)
		{
			this.ThrowIfMaxHttpCollectionKeysExceeded();
			this._all = null;
			this._allKeys = null;
			if (append)
			{
				if (!cookie.IsInResponseHeader)
				{
					cookie.Added = true;
				}
				base.BaseAdd(cookie.Name, cookie);
				return;
			}
			if (base.BaseGet(cookie.Name) != null)
			{
				cookie.Changed = true;
			}
			base.BaseSet(cookie.Name, cookie);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00016420 File Offset: 0x00014620
		internal void Append(HttpCookieCollection cookies)
		{
			for (int i = 0; i < cookies.Count; i++)
			{
				HttpCookie httpCookie = (HttpCookie)cookies.BaseGet(i);
				base.BaseAdd(httpCookie.Name, httpCookie);
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00016458 File Offset: 0x00014658
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

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001648A File Offset: 0x0001468A
		internal void EnableGranularValidation(ValidateStringCallback validationCallback)
		{
			this._keysAwaitingValidation = new HashSet<string>(this.Keys.Cast<string>(), StringComparer.OrdinalIgnoreCase);
			this._validationCallback = validationCallback;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000164AE File Offset: 0x000146AE
		private void EnsureKeyValidated(string key, string value)
		{
			if (this._keysAwaitingValidation == null)
			{
				return;
			}
			if (!this._keysAwaitingValidation.Contains(key))
			{
				return;
			}
			if (!string.IsNullOrEmpty(value))
			{
				this._validationCallback(key, value);
			}
			this._keysAwaitingValidation.Remove(key);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000164EA File Offset: 0x000146EA
		internal void MakeReadOnly()
		{
			base.IsReadOnly = true;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000164F3 File Offset: 0x000146F3
		internal void RemoveCookie(string name)
		{
			this._all = null;
			this._allKeys = null;
			base.BaseRemove(name);
			this._changed = true;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00016511 File Offset: 0x00014711
		internal void Reset()
		{
			this._all = null;
			this._allKeys = null;
			base.BaseClear();
			this._changed = true;
			this._keysAwaitingValidation = null;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00016535 File Offset: 0x00014735
		public void Add(HttpCookie cookie)
		{
			if (this._response != null)
			{
				this._response.BeforeCookieCollectionChange();
			}
			this.AddCookie(cookie, true);
			if (this._response != null)
			{
				this._response.OnCookieAdd(cookie);
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00016568 File Offset: 0x00014768
		public void CopyTo(Array dest, int index)
		{
			if (this._all == null)
			{
				int count = this.Count;
				HttpCookie[] array = new HttpCookie[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this.Get(i);
				}
				this._all = array;
			}
			this._all.CopyTo(dest, index);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000165B5 File Offset: 0x000147B5
		public void Set(HttpCookie cookie)
		{
			if (this._response != null)
			{
				this._response.BeforeCookieCollectionChange();
			}
			this.AddCookie(cookie, false);
			if (this._response != null)
			{
				this._response.OnCookieCollectionChange();
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000165E5 File Offset: 0x000147E5
		public void Remove(string name)
		{
			if (this._response != null)
			{
				this._response.BeforeCookieCollectionChange();
			}
			this.RemoveCookie(name);
			if (this._response != null)
			{
				this._response.OnCookieCollectionChange();
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00016614 File Offset: 0x00014814
		public void Clear()
		{
			this.Reset();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001661C File Offset: 0x0001481C
		public HttpCookie Get(string name)
		{
			HttpCookie httpCookie = (HttpCookie)base.BaseGet(name);
			if (httpCookie == null && this._response != null)
			{
				httpCookie = new HttpCookie(name);
				this.AddCookie(httpCookie, true);
				this._response.OnCookieAdd(httpCookie);
			}
			if (httpCookie != null)
			{
				this.EnsureKeyValidated(name, httpCookie.Value);
			}
			return httpCookie;
		}

		// Token: 0x170003D7 RID: 983
		public HttpCookie this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00016678 File Offset: 0x00014878
		public HttpCookie Get(int index)
		{
			HttpCookie httpCookie = (HttpCookie)base.BaseGet(index);
			if (httpCookie != null)
			{
				this.EnsureKeyValidated(this.GetKey(index), httpCookie.Value);
			}
			return httpCookie;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000166A9 File Offset: 0x000148A9
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x170003D8 RID: 984
		public HttpCookie this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000166BB File Offset: 0x000148BB
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

		// Token: 0x0400039C RID: 924
		private HttpResponse _response;

		// Token: 0x0400039D RID: 925
		private HttpCookie[] _all;

		// Token: 0x0400039E RID: 926
		private string[] _allKeys;

		// Token: 0x0400039F RID: 927
		private bool _changed;

		// Token: 0x040003A0 RID: 928
		private ValidateStringCallback _validationCallback;

		// Token: 0x040003A1 RID: 929
		private HashSet<string> _keysAwaitingValidation;
	}
}
