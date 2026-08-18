using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A7 RID: 2215
	[__DynamicallyInvokable]
	public sealed class TcpConnectionPoolSettings
	{
		// Token: 0x06005473 RID: 21619 RVA: 0x00136EAE File Offset: 0x001350AE
		internal TcpConnectionPoolSettings()
		{
			this.groupName = "default";
			this.idleTimeout = ConnectionOrientedTransportDefaults.IdleTimeout;
			this.leaseTimeout = TcpTransportDefaults.ConnectionLeaseTimeout;
			this.maxOutboundConnectionsPerEndpoint = 10;
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x00136EDF File Offset: 0x001350DF
		internal TcpConnectionPoolSettings(TcpConnectionPoolSettings tcp)
		{
			this.groupName = tcp.groupName;
			this.idleTimeout = tcp.idleTimeout;
			this.leaseTimeout = tcp.leaseTimeout;
			this.maxOutboundConnectionsPerEndpoint = tcp.maxOutboundConnectionsPerEndpoint;
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06005475 RID: 21621 RVA: 0x00136F17 File Offset: 0x00135117
		// (set) Token: 0x06005476 RID: 21622 RVA: 0x00136F1F File Offset: 0x0013511F
		[__DynamicallyInvokable]
		public string GroupName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.groupName;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.groupName = value;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06005477 RID: 21623 RVA: 0x00136F3B File Offset: 0x0013513B
		// (set) Token: 0x06005478 RID: 21624 RVA: 0x00136F44 File Offset: 0x00135144
		[__DynamicallyInvokable]
		public TimeSpan IdleTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.idleTimeout;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.idleTimeout = value;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06005479 RID: 21625 RVA: 0x00136FB7 File Offset: 0x001351B7
		// (set) Token: 0x0600547A RID: 21626 RVA: 0x00136FC0 File Offset: 0x001351C0
		[__DynamicallyInvokable]
		public TimeSpan LeaseTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.leaseTimeout;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.leaseTimeout = value;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x00137033 File Offset: 0x00135233
		// (set) Token: 0x0600547C RID: 21628 RVA: 0x0013703B File Offset: 0x0013523B
		[__DynamicallyInvokable]
		public int MaxOutboundConnectionsPerEndpoint
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxOutboundConnectionsPerEndpoint;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxOutboundConnectionsPerEndpoint = value;
			}
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0013706D File Offset: 0x0013526D
		internal TcpConnectionPoolSettings Clone()
		{
			return new TcpConnectionPoolSettings(this);
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x00137078 File Offset: 0x00135278
		internal bool IsMatch(TcpConnectionPoolSettings tcp)
		{
			return !(this.groupName != tcp.groupName) && !(this.idleTimeout != tcp.idleTimeout) && !(this.leaseTimeout != tcp.leaseTimeout) && this.maxOutboundConnectionsPerEndpoint == tcp.maxOutboundConnectionsPerEndpoint;
		}

		// Token: 0x04003315 RID: 13077
		private string groupName;

		// Token: 0x04003316 RID: 13078
		private TimeSpan idleTimeout;

		// Token: 0x04003317 RID: 13079
		private TimeSpan leaseTimeout;

		// Token: 0x04003318 RID: 13080
		private int maxOutboundConnectionsPerEndpoint;
	}
}
