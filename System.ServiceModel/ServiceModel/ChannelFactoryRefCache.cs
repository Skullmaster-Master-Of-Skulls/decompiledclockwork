using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel
{
	// Token: 0x020000EA RID: 234
	internal class ChannelFactoryRefCache<TChannel> : MruCache<EndpointTrait<TChannel>, ChannelFactoryRef<TChannel>> where TChannel : class
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x000176D8 File Offset: 0x000158D8
		public ChannelFactoryRefCache(int watermark) : base(watermark * 4 / 5, watermark, ChannelFactoryRefCache<TChannel>.DefaultEndpointTraitComparer)
		{
			this.watermark = watermark;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000176F2 File Offset: 0x000158F2
		protected override void OnSingleItemRemoved(ChannelFactoryRef<TChannel> item)
		{
			if (item.Release())
			{
				item.Abort();
			}
			if (TD.ClientBaseCachedChannelFactoryCountIsEnabled())
			{
				TD.ClientBaseCachedChannelFactoryCount(base.Count, this.watermark, this);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001771B File Offset: 0x0001591B
		protected override void OnItemAgedOutOfCache(ChannelFactoryRef<TChannel> item)
		{
			if (TD.ClientBaseChannelFactoryAgedOutofCacheIsEnabled())
			{
				TD.ClientBaseChannelFactoryAgedOutofCache(this.watermark, this);
			}
		}

		// Token: 0x04000A20 RID: 2592
		private static ChannelFactoryRefCache<TChannel>.EndpointTraitComparer DefaultEndpointTraitComparer = new ChannelFactoryRefCache<TChannel>.EndpointTraitComparer();

		// Token: 0x04000A21 RID: 2593
		private readonly int watermark;

		// Token: 0x02000AD7 RID: 2775
		private class EndpointTraitComparer : IEqualityComparer<EndpointTrait<TChannel>>
		{
			// Token: 0x06006E81 RID: 28289 RVA: 0x0019BE55 File Offset: 0x0019A055
			public bool Equals(EndpointTrait<TChannel> x, EndpointTrait<TChannel> y)
			{
				if (x != null)
				{
					return y != null && x.Equals(y);
				}
				return y == null;
			}

			// Token: 0x06006E82 RID: 28290 RVA: 0x0019BE6D File Offset: 0x0019A06D
			public int GetHashCode(EndpointTrait<TChannel> obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}
		}
	}
}
