using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Threading;

namespace System.Web.SessionState
{
	// Token: 0x0200012E RID: 302
	public class HttpSessionStateContainer : IHttpSessionState
	{
		// Token: 0x0600120B RID: 4619 RVA: 0x00032324 File Offset: 0x00030524
		public HttpSessionStateContainer(string id, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout, bool newSession, HttpCookieMode cookieMode, SessionStateMode mode, bool isReadonly) : this(null, id, sessionItems, staticObjects, timeout, newSession, cookieMode, mode, isReadonly)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00032354 File Offset: 0x00030554
		internal HttpSessionStateContainer(SessionStateModule stateModule, string id, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout, bool newSession, HttpCookieMode cookieMode, SessionStateMode mode, bool isReadonly)
		{
			this._stateModule = stateModule;
			this._id = id;
			this._sessionItems = sessionItems;
			this._staticObjects = staticObjects;
			this._timeout = timeout;
			this._newSession = newSession;
			this._cookieMode = cookieMode;
			this._mode = mode;
			this._isReadonly = isReadonly;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000030B5 File Offset: 0x000012B5
		internal HttpSessionStateContainer()
		{
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x000323AC File Offset: 0x000305AC
		public string SessionID
		{
			get
			{
				if (this._id == null)
				{
					this._id = this._stateModule.DelayedGetSessionId();
				}
				return this._id;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000323CD File Offset: 0x000305CD
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000323D8 File Offset: 0x000305D8
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException(SR.GetString("Timeout_must_be_positive"));
				}
				if (value > 525600 && (this.Mode == SessionStateMode.InProc || this.Mode == SessionStateMode.StateServer))
				{
					throw new ArgumentException(SR.GetString("Invalid_cache_based_session_timeout"));
				}
				this._timeout = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0003242A File Offset: 0x0003062A
		public bool IsNewSession
		{
			get
			{
				return this._newSession;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00032432 File Offset: 0x00030632
		public SessionStateMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0003243A File Offset: 0x0003063A
		public bool IsCookieless
		{
			get
			{
				if (this._stateModule != null)
				{
					return this._stateModule.SessionIDManagerUseCookieless;
				}
				return this.CookieMode == HttpCookieMode.UseUri;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00032459 File Offset: 0x00030659
		public HttpCookieMode CookieMode
		{
			get
			{
				return this._cookieMode;
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00032461 File Offset: 0x00030661
		public void Abandon()
		{
			this._abandon = true;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0003246A File Offset: 0x0003066A
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x0003247B File Offset: 0x0003067B
		public int LCID
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.LCID;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = HttpServerUtility.CreateReadOnlyCultureInfo(value);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0003248D File Offset: 0x0003068D
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x000324B5 File Offset: 0x000306B5
		public int CodePage
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.Response.ContentEncoding.CodePage;
				}
				return Encoding.Default.CodePage;
			}
			set
			{
				if (HttpContext.Current != null)
				{
					HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding(value);
				}
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x000324D3 File Offset: 0x000306D3
		public bool IsAbandoned
		{
			get
			{
				return this._abandon;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x000324DB File Offset: 0x000306DB
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this._staticObjects;
			}
		}

		// Token: 0x170005B1 RID: 1457
		public object this[string name]
		{
			get
			{
				return this._sessionItems[name];
			}
			set
			{
				this._sessionItems[name] = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		public object this[int index]
		{
			get
			{
				return this._sessionItems[index];
			}
			set
			{
				this._sessionItems[index] = value;
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000324F1 File Offset: 0x000306F1
		public void Add(string name, object value)
		{
			this._sessionItems[name] = value;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003251D File Offset: 0x0003071D
		public void Remove(string name)
		{
			this._sessionItems.Remove(name);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003252B File Offset: 0x0003072B
		public void RemoveAt(int index)
		{
			this._sessionItems.RemoveAt(index);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00032539 File Offset: 0x00030739
		public void Clear()
		{
			this._sessionItems.Clear();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00032546 File Offset: 0x00030746
		public void RemoveAll()
		{
			this.Clear();
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0003254E File Offset: 0x0003074E
		public int Count
		{
			get
			{
				return this._sessionItems.Count;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0003255B File Offset: 0x0003075B
		public NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._sessionItems.Keys;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00032568 File Offset: 0x00030768
		public IEnumerator GetEnumerator()
		{
			return this._sessionItems.GetEnumerator();
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00032578 File Offset: 0x00030778
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x000325A8 File Offset: 0x000307A8
		public bool IsReadOnly
		{
			get
			{
				return this._isReadonly;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001422 RID: 5154
		private string _id;

		// Token: 0x04001423 RID: 5155
		private ISessionStateItemCollection _sessionItems;

		// Token: 0x04001424 RID: 5156
		private HttpStaticObjectsCollection _staticObjects;

		// Token: 0x04001425 RID: 5157
		private int _timeout;

		// Token: 0x04001426 RID: 5158
		private bool _newSession;

		// Token: 0x04001427 RID: 5159
		private HttpCookieMode _cookieMode;

		// Token: 0x04001428 RID: 5160
		private SessionStateMode _mode;

		// Token: 0x04001429 RID: 5161
		private bool _abandon;

		// Token: 0x0400142A RID: 5162
		private bool _isReadonly;

		// Token: 0x0400142B RID: 5163
		private SessionStateModule _stateModule;
	}
}
