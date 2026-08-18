using System;

namespace System.Web.SessionState
{
	// Token: 0x02000124 RID: 292
	public class SessionStateStoreData
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x00031024 File Offset: 0x0002F224
		public SessionStateStoreData(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
		{
			this._sessionItems = sessionItems;
			this._staticObjects = staticObjects;
			this._timeout = timeout;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x00031041 File Offset: 0x0002F241
		public virtual ISessionStateItemCollection Items
		{
			get
			{
				return this._sessionItems;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00031049 File Offset: 0x0002F249
		public virtual HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this._staticObjects;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00031051 File Offset: 0x0002F251
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x00031059 File Offset: 0x0002F259
		public virtual int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				this._timeout = value;
			}
		}

		// Token: 0x040013EC RID: 5100
		private ISessionStateItemCollection _sessionItems;

		// Token: 0x040013ED RID: 5101
		private HttpStaticObjectsCollection _staticObjects;

		// Token: 0x040013EE RID: 5102
		private int _timeout;
	}
}
