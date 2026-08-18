using System;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200007C RID: 124
	public sealed class HttpApplicationState : NameObjectCollectionBase
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x000106CA File Offset: 0x0000E8CA
		internal HttpApplicationState() : this(null, null)
		{
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000106D4 File Offset: 0x0000E8D4
		internal HttpApplicationState(HttpStaticObjectsCollection applicationStaticObjects, HttpStaticObjectsCollection sessionStaticObjects) : base(Misc.CaseInsensitiveInvariantKeyComparer)
		{
			this._applicationStaticObjects = applicationStaticObjects;
			if (this._applicationStaticObjects == null)
			{
				this._applicationStaticObjects = new HttpStaticObjectsCollection();
			}
			this._sessionStaticObjects = sessionStaticObjects;
			if (this._sessionStaticObjects == null)
			{
				this._sessionStaticObjects = new HttpStaticObjectsCollection();
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001072B File Offset: 0x0000E92B
		internal HttpStaticObjectsCollection SessionStaticObjects
		{
			get
			{
				return this._sessionStaticObjects;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00010734 File Offset: 0x0000E934
		public override int Count
		{
			get
			{
				int result = 0;
				this._lock.AcquireRead();
				try
				{
					result = base.Count;
				}
				finally
				{
					this._lock.ReleaseRead();
				}
				return result;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00010774 File Offset: 0x0000E974
		public void Add(string name, object value)
		{
			this._lock.AcquireWrite();
			try
			{
				base.BaseAdd(name, value);
			}
			finally
			{
				this._lock.ReleaseWrite();
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public void Set(string name, object value)
		{
			this._lock.AcquireWrite();
			try
			{
				base.BaseSet(name, value);
			}
			finally
			{
				this._lock.ReleaseWrite();
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public void Remove(string name)
		{
			this._lock.AcquireWrite();
			try
			{
				base.BaseRemove(name);
			}
			finally
			{
				this._lock.ReleaseWrite();
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00010834 File Offset: 0x0000EA34
		public void RemoveAt(int index)
		{
			this._lock.AcquireWrite();
			try
			{
				base.BaseRemoveAt(index);
			}
			finally
			{
				this._lock.ReleaseWrite();
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00010874 File Offset: 0x0000EA74
		public void Clear()
		{
			this._lock.AcquireWrite();
			try
			{
				base.BaseClear();
			}
			finally
			{
				this._lock.ReleaseWrite();
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public void RemoveAll()
		{
			this.Clear();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000108B8 File Offset: 0x0000EAB8
		public object Get(string name)
		{
			object result = null;
			this._lock.AcquireRead();
			try
			{
				result = base.BaseGet(name);
			}
			finally
			{
				this._lock.ReleaseRead();
			}
			return result;
		}

		// Token: 0x1700031B RID: 795
		public object this[string name]
		{
			get
			{
				return this.Get(name);
			}
			set
			{
				this.Set(name, value);
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00010910 File Offset: 0x0000EB10
		public object Get(int index)
		{
			object result = null;
			this._lock.AcquireRead();
			try
			{
				result = base.BaseGet(index);
			}
			finally
			{
				this._lock.ReleaseRead();
			}
			return result;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00010954 File Offset: 0x0000EB54
		public string GetKey(int index)
		{
			string result = null;
			this._lock.AcquireRead();
			try
			{
				result = base.BaseGetKey(index);
			}
			finally
			{
				this._lock.ReleaseRead();
			}
			return result;
		}

		// Token: 0x1700031C RID: 796
		public object this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000109A4 File Offset: 0x0000EBA4
		public string[] AllKeys
		{
			get
			{
				string[] result = null;
				this._lock.AcquireRead();
				try
				{
					result = base.BaseGetAllKeys();
				}
				finally
				{
					this._lock.ReleaseRead();
				}
				return result;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00004335 File Offset: 0x00002535
		public HttpApplicationState Contents
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000109E4 File Offset: 0x0000EBE4
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this._applicationStaticObjects;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000109EC File Offset: 0x0000EBEC
		public void Lock()
		{
			this._lock.AcquireWrite();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000109F9 File Offset: 0x0000EBF9
		public void UnLock()
		{
			this._lock.ReleaseWrite();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00010A06 File Offset: 0x0000EC06
		internal void EnsureUnLock()
		{
			this._lock.EnsureReleaseWrite();
		}

		// Token: 0x0400028B RID: 651
		private HttpApplicationStateLock _lock = new HttpApplicationStateLock();

		// Token: 0x0400028C RID: 652
		private HttpStaticObjectsCollection _applicationStaticObjects;

		// Token: 0x0400028D RID: 653
		private HttpStaticObjectsCollection _sessionStaticObjects;
	}
}
