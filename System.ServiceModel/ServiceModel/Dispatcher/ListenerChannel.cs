using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000589 RID: 1417
	internal class ListenerChannel
	{
		// Token: 0x060036A3 RID: 13987 RVA: 0x000D2827 File Offset: 0x000D0A27
		public ListenerChannel(IChannelBinder binder)
		{
			this.binder = binder;
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x000D2836 File Offset: 0x000D0A36
		public IChannelBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x000D283E File Offset: 0x000D0A3E
		// (set) Token: 0x060036A6 RID: 13990 RVA: 0x000D2846 File Offset: 0x000D0A46
		public ServiceThrottle Throttle
		{
			get
			{
				return this.throttle;
			}
			set
			{
				this.throttle = value;
			}
		}

		// Token: 0x040028BD RID: 10429
		private IChannelBinder binder;

		// Token: 0x040028BE RID: 10430
		private ServiceThrottle throttle;
	}
}
