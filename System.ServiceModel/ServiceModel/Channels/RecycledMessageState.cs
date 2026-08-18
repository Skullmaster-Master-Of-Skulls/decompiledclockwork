using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C2 RID: 2498
	internal class RecycledMessageState
	{
		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06006230 RID: 25136 RVA: 0x0016D8E6 File Offset: 0x0016BAE6
		public HeaderInfoCache HeaderInfoCache
		{
			get
			{
				if (this.headerInfoCache == null)
				{
					this.headerInfoCache = new HeaderInfoCache();
				}
				return this.headerInfoCache;
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06006231 RID: 25137 RVA: 0x0016D901 File Offset: 0x0016BB01
		public UriCache UriCache
		{
			get
			{
				if (this.uriCache == null)
				{
					this.uriCache = new UriCache();
				}
				return this.uriCache;
			}
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x0016D91C File Offset: 0x0016BB1C
		public MessageProperties TakeProperties()
		{
			MessageProperties result = this.recycledProperties;
			this.recycledProperties = null;
			return result;
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x0016D938 File Offset: 0x0016BB38
		public void ReturnProperties(MessageProperties properties)
		{
			if (properties.CanRecycle)
			{
				properties.Recycle();
				this.recycledProperties = properties;
			}
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x0016D950 File Offset: 0x0016BB50
		public MessageHeaders TakeHeaders()
		{
			MessageHeaders result = this.recycledHeaders;
			this.recycledHeaders = null;
			return result;
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x0016D96C File Offset: 0x0016BB6C
		public void ReturnHeaders(MessageHeaders headers)
		{
			if (headers.CanRecycle)
			{
				headers.Recycle(this.HeaderInfoCache);
				this.recycledHeaders = headers;
			}
		}

		// Token: 0x040038FB RID: 14587
		private MessageHeaders recycledHeaders;

		// Token: 0x040038FC RID: 14588
		private MessageProperties recycledProperties;

		// Token: 0x040038FD RID: 14589
		private UriCache uriCache;

		// Token: 0x040038FE RID: 14590
		private HeaderInfoCache headerInfoCache;
	}
}
