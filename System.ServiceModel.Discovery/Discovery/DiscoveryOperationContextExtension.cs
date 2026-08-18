using System;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000024 RID: 36
	public class DiscoveryOperationContextExtension : IExtension<OperationContext>
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00006A08 File Offset: 0x00004C08
		internal DiscoveryOperationContextExtension() : this(TimeSpan.Zero, ServiceDiscoveryMode.Adhoc, DiscoveryVersion.DefaultDiscoveryVersion)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006A1B File Offset: 0x00004C1B
		internal DiscoveryOperationContextExtension(TimeSpan maxResponseDelay, ServiceDiscoveryMode discoveryMode, DiscoveryVersion discoveryVersion)
		{
			TimeoutHelper.ThrowIfNegativeArgument(maxResponseDelay, "maxResponseDelay");
			this.maxResponseDelay = maxResponseDelay;
			this.discoveryMode = discoveryMode;
			this.discoveryVersion = discoveryVersion;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006A43 File Offset: 0x00004C43
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00006A4B File Offset: 0x00004C4B
		public TimeSpan MaxResponseDelay
		{
			get
			{
				return this.maxResponseDelay;
			}
			internal set
			{
				TimeoutHelper.ThrowIfNegativeArgument(value, "values");
				this.maxResponseDelay = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006A5F File Offset: 0x00004C5F
		public ServiceDiscoveryMode DiscoveryMode
		{
			get
			{
				return this.discoveryMode;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006A67 File Offset: 0x00004C67
		public DiscoveryVersion DiscoveryVersion
		{
			get
			{
				return this.discoveryVersion;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000030E1 File Offset: 0x000012E1
		void IExtension<OperationContext>.Attach(OperationContext owner)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000030E1 File Offset: 0x000012E1
		void IExtension<OperationContext>.Detach(OperationContext owner)
		{
		}

		// Token: 0x04000072 RID: 114
		private TimeSpan maxResponseDelay;

		// Token: 0x04000073 RID: 115
		private ServiceDiscoveryMode discoveryMode;

		// Token: 0x04000074 RID: 116
		private DiscoveryVersion discoveryVersion;
	}
}
