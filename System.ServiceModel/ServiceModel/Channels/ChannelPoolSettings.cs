using System;
using System.ComponentModel;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200073F RID: 1855
	public class ChannelPoolSettings
	{
		// Token: 0x06004682 RID: 18050 RVA: 0x00106D63 File Offset: 0x00104F63
		public ChannelPoolSettings()
		{
			this.idleTimeout = OneWayDefaults.IdleTimeout;
			this.leaseTimeout = OneWayDefaults.LeaseTimeout;
			this.maxOutboundChannelsPerEndpoint = 10;
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00106D89 File Offset: 0x00104F89
		private ChannelPoolSettings(ChannelPoolSettings poolToBeCloned)
		{
			this.idleTimeout = poolToBeCloned.idleTimeout;
			this.leaseTimeout = poolToBeCloned.leaseTimeout;
			this.maxOutboundChannelsPerEndpoint = poolToBeCloned.maxOutboundChannelsPerEndpoint;
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x00106DB5 File Offset: 0x00104FB5
		// (set) Token: 0x06004685 RID: 18053 RVA: 0x00106DC0 File Offset: 0x00104FC0
		[DefaultValue(typeof(TimeSpan), "00:02:00")]
		public TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
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

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x00106E33 File Offset: 0x00105033
		// (set) Token: 0x06004687 RID: 18055 RVA: 0x00106E3C File Offset: 0x0010503C
		[DefaultValue(typeof(TimeSpan), "00:10:00")]
		public TimeSpan LeaseTimeout
		{
			get
			{
				return this.leaseTimeout;
			}
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

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x00106EAF File Offset: 0x001050AF
		// (set) Token: 0x06004689 RID: 18057 RVA: 0x00106EB7 File Offset: 0x001050B7
		[DefaultValue(10)]
		public int MaxOutboundChannelsPerEndpoint
		{
			get
			{
				return this.maxOutboundChannelsPerEndpoint;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxOutboundChannelsPerEndpoint = value;
			}
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x00106EE9 File Offset: 0x001050E9
		internal ChannelPoolSettings Clone()
		{
			return new ChannelPoolSettings(this);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x00106EF4 File Offset: 0x001050F4
		internal bool IsMatch(ChannelPoolSettings channelPoolSettings)
		{
			return channelPoolSettings != null && !(this.idleTimeout != channelPoolSettings.idleTimeout) && !(this.leaseTimeout != channelPoolSettings.leaseTimeout) && this.maxOutboundChannelsPerEndpoint == channelPoolSettings.maxOutboundChannelsPerEndpoint;
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x00106F41 File Offset: 0x00105141
		internal bool InternalShouldSerialize()
		{
			return this.maxOutboundChannelsPerEndpoint != 10 || this.idleTimeout != OneWayDefaults.IdleTimeout || this.leaseTimeout != OneWayDefaults.LeaseTimeout;
		}

		// Token: 0x04002D8D RID: 11661
		private TimeSpan idleTimeout;

		// Token: 0x04002D8E RID: 11662
		private TimeSpan leaseTimeout;

		// Token: 0x04002D8F RID: 11663
		private int maxOutboundChannelsPerEndpoint;
	}
}
