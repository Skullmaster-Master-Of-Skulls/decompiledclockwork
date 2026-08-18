using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState
{
	// Token: 0x0200012C RID: 300
	public sealed class HttpSessionState : ICollection, IEnumerable
	{
		// Token: 0x060011EB RID: 4587 RVA: 0x00032198 File Offset: 0x00030398
		internal HttpSessionState(IHttpSessionState container)
		{
			this._container = container;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x000321A7 File Offset: 0x000303A7
		internal IHttpSessionState Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x000321AF File Offset: 0x000303AF
		public string SessionID
		{
			get
			{
				return this._container.SessionID;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x000321BC File Offset: 0x000303BC
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x000321C9 File Offset: 0x000303C9
		public int Timeout
		{
			get
			{
				return this._container.Timeout;
			}
			set
			{
				this._container.Timeout = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000321D7 File Offset: 0x000303D7
		public bool IsNewSession
		{
			get
			{
				return this._container.IsNewSession;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000321E4 File Offset: 0x000303E4
		public SessionStateMode Mode
		{
			get
			{
				return this._container.Mode;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x000321F1 File Offset: 0x000303F1
		public bool IsCookieless
		{
			get
			{
				return this._container.IsCookieless;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000321FE File Offset: 0x000303FE
		public HttpCookieMode CookieMode
		{
			get
			{
				return this._container.CookieMode;
			}
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0003220B File Offset: 0x0003040B
		public void Abandon()
		{
			this._container.Abandon();
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00032218 File Offset: 0x00030418
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x00032225 File Offset: 0x00030425
		public int LCID
		{
			get
			{
				return this._container.LCID;
			}
			set
			{
				this._container.LCID = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00032233 File Offset: 0x00030433
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00032240 File Offset: 0x00030440
		public int CodePage
		{
			get
			{
				return this._container.CodePage;
			}
			set
			{
				this._container.CodePage = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00004335 File Offset: 0x00002535
		public HttpSessionState Contents
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0003224E File Offset: 0x0003044E
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this._container.StaticObjects;
			}
		}

		// Token: 0x170005A0 RID: 1440
		public object this[string name]
		{
			get
			{
				return this._container[name];
			}
			set
			{
				this._container[name] = value;
			}
		}

		// Token: 0x170005A1 RID: 1441
		public object this[int index]
		{
			get
			{
				return this._container[index];
			}
			set
			{
				this._container[index] = value;
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00032269 File Offset: 0x00030469
		public void Add(string name, object value)
		{
			this._container[name] = value;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00032295 File Offset: 0x00030495
		public void Remove(string name)
		{
			this._container.Remove(name);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000322A3 File Offset: 0x000304A3
		public void RemoveAt(int index)
		{
			this._container.RemoveAt(index);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000322B1 File Offset: 0x000304B1
		public void Clear()
		{
			this._container.Clear();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000322BE File Offset: 0x000304BE
		public void RemoveAll()
		{
			this.Clear();
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x000322C6 File Offset: 0x000304C6
		public int Count
		{
			get
			{
				return this._container.Count;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x000322D3 File Offset: 0x000304D3
		public NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._container.Keys;
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000322E0 File Offset: 0x000304E0
		public IEnumerator GetEnumerator()
		{
			return this._container.GetEnumerator();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000322ED File Offset: 0x000304ED
		public void CopyTo(Array array, int index)
		{
			this._container.CopyTo(array, index);
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x000322FC File Offset: 0x000304FC
		public object SyncRoot
		{
			get
			{
				return this._container.SyncRoot;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00032309 File Offset: 0x00030509
		public bool IsReadOnly
		{
			get
			{
				return this._container.IsReadOnly;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00032316 File Offset: 0x00030516
		public bool IsSynchronized
		{
			get
			{
				return this._container.IsSynchronized;
			}
		}

		// Token: 0x0400141C RID: 5148
		private IHttpSessionState _container;
	}
}
