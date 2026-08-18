using System;

namespace System.Web.Hosting
{
	// Token: 0x02000786 RID: 1926
	public sealed class RecycleLimitInfo
	{
		// Token: 0x06005C4D RID: 23629 RVA: 0x0013F7B8 File Offset: 0x0013D9B8
		public RecycleLimitInfo(long currentPrivateBytes, long recycleLimit, RecycleLimitNotificationFrequency recycleLimitNearFrequency)
		{
			this._currentPB = currentPrivateBytes;
			this._recycleLimit = recycleLimit;
			this._recycleLimitNearFrequency = recycleLimitNearFrequency;
			this._requestGC = false;
		}

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x06005C4E RID: 23630 RVA: 0x0013F7DC File Offset: 0x0013D9DC
		public long CurrentPrivateBytes
		{
			get
			{
				return this._currentPB;
			}
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x06005C4F RID: 23631 RVA: 0x0013F7E4 File Offset: 0x0013D9E4
		public long RecycleLimit
		{
			get
			{
				return this._recycleLimit;
			}
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x06005C50 RID: 23632 RVA: 0x0013F7EC File Offset: 0x0013D9EC
		public RecycleLimitNotificationFrequency TrimFrequency
		{
			get
			{
				return this._recycleLimitNearFrequency;
			}
		}

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x06005C51 RID: 23633 RVA: 0x0013F7F4 File Offset: 0x0013D9F4
		// (set) Token: 0x06005C52 RID: 23634 RVA: 0x0013F7FC File Offset: 0x0013D9FC
		public bool RequestGC
		{
			get
			{
				return this._requestGC;
			}
			set
			{
				this._requestGC = (this._requestGC || value);
			}
		}

		// Token: 0x04003096 RID: 12438
		private long _currentPB;

		// Token: 0x04003097 RID: 12439
		private long _recycleLimit;

		// Token: 0x04003098 RID: 12440
		private RecycleLimitNotificationFrequency _recycleLimitNearFrequency;

		// Token: 0x04003099 RID: 12441
		private bool _requestGC;
	}
}
