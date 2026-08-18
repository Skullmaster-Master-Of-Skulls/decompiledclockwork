using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x02000023 RID: 35
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpApplicationStateWrapper : HttpApplicationStateBase
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000430B File Offset: 0x0000250B
		public HttpApplicationStateWrapper(HttpApplicationState httpApplicationState)
		{
			if (httpApplicationState == null)
			{
				throw new ArgumentNullException("httpApplicationState");
			}
			this._application = httpApplicationState;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004328 File Offset: 0x00002528
		public override string[] AllKeys
		{
			get
			{
				return this._application.AllKeys;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004335 File Offset: 0x00002535
		public override HttpApplicationStateBase Contents
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004338 File Offset: 0x00002538
		public override int Count
		{
			get
			{
				return this._application.Count;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004345 File Offset: 0x00002545
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this._application).IsSynchronized;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004352 File Offset: 0x00002552
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._application.Keys;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000435F File Offset: 0x0000255F
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this._application).SyncRoot;
			}
		}

		// Token: 0x17000032 RID: 50
		public override object this[int index]
		{
			get
			{
				return this._application[index];
			}
		}

		// Token: 0x17000033 RID: 51
		public override object this[string name]
		{
			get
			{
				return this._application[name];
			}
			set
			{
				this._application[name] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004397 File Offset: 0x00002597
		public override HttpStaticObjectsCollectionBase StaticObjects
		{
			get
			{
				return new HttpStaticObjectsCollectionWrapper(this._application.StaticObjects);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000043A9 File Offset: 0x000025A9
		public override void Add(string name, object value)
		{
			this._application.Add(name, value);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000043B8 File Offset: 0x000025B8
		public override void Clear()
		{
			this._application.Clear();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000043C5 File Offset: 0x000025C5
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this._application).CopyTo(array, index);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000043D4 File Offset: 0x000025D4
		public override object Get(int index)
		{
			return this._application.Get(index);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000043E2 File Offset: 0x000025E2
		public override object Get(string name)
		{
			return this._application.Get(name);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000043F0 File Offset: 0x000025F0
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this._application).GetEnumerator();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000043FD File Offset: 0x000025FD
		public override string GetKey(int index)
		{
			return this._application.GetKey(index);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000440B File Offset: 0x0000260B
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this._application.GetObjectData(info, context);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000441A File Offset: 0x0000261A
		public override void Lock()
		{
			this._application.Lock();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004427 File Offset: 0x00002627
		public override void OnDeserialization(object sender)
		{
			this._application.OnDeserialization(sender);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004435 File Offset: 0x00002635
		public override void Remove(string name)
		{
			this._application.Remove(name);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004443 File Offset: 0x00002643
		public override void RemoveAll()
		{
			this._application.RemoveAll();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004450 File Offset: 0x00002650
		public override void RemoveAt(int index)
		{
			this._application.RemoveAt(index);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000445E File Offset: 0x0000265E
		public override void Set(string name, object value)
		{
			this._application.Set(name, value);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000446D File Offset: 0x0000266D
		public override void UnLock()
		{
			this._application.UnLock();
		}

		// Token: 0x04000107 RID: 263
		private HttpApplicationState _application;
	}
}
