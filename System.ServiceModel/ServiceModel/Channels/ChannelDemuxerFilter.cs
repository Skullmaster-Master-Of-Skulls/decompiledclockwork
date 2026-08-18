using System;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000733 RID: 1843
	internal class ChannelDemuxerFilter
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x001062B2 File Offset: 0x001044B2
		public ChannelDemuxerFilter(MessageFilter filter, int priority)
		{
			this.filter = filter;
			this.priority = priority;
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x001062C8 File Offset: 0x001044C8
		public MessageFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x001062D0 File Offset: 0x001044D0
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x04002D75 RID: 11637
		private MessageFilter filter;

		// Token: 0x04002D76 RID: 11638
		private int priority;
	}
}
