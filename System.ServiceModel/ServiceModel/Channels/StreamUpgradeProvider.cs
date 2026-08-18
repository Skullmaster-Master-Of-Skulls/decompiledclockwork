using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000836 RID: 2102
	public abstract class StreamUpgradeProvider : CommunicationObject
	{
		// Token: 0x06004E85 RID: 20101 RVA: 0x0011E4BD File Offset: 0x0011C6BD
		protected StreamUpgradeProvider() : this(null)
		{
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x0011E4C6 File Offset: 0x0011C6C6
		protected StreamUpgradeProvider(IDefaultCommunicationTimeouts timeouts)
		{
			if (timeouts != null)
			{
				this.closeTimeout = timeouts.CloseTimeout;
				this.openTimeout = timeouts.OpenTimeout;
				return;
			}
			this.closeTimeout = ServiceDefaults.CloseTimeout;
			this.openTimeout = ServiceDefaults.OpenTimeout;
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x0011E500 File Offset: 0x0011C700
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.closeTimeout;
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x0011E508 File Offset: 0x0011C708
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.closeTimeout;
			}
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x0011E510 File Offset: 0x0011C710
		public virtual T GetProperty<T>() where T : class
		{
			return default(T);
		}

		// Token: 0x06004E8A RID: 20106
		public abstract StreamUpgradeInitiator CreateUpgradeInitiator(EndpointAddress remoteAddress, Uri via);

		// Token: 0x06004E8B RID: 20107
		public abstract StreamUpgradeAcceptor CreateUpgradeAcceptor();

		// Token: 0x040030EC RID: 12524
		private TimeSpan closeTimeout;

		// Token: 0x040030ED RID: 12525
		private TimeSpan openTimeout;
	}
}
