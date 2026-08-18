using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200071C RID: 1820
	[__DynamicallyInvokable]
	public abstract class ChannelBase : CommunicationObject, IChannel, ICommunicationObject, IDefaultCommunicationTimeouts
	{
		// Token: 0x0600450E RID: 17678 RVA: 0x00102EE4 File Offset: 0x001010E4
		[__DynamicallyInvokable]
		protected ChannelBase(ChannelManagerBase channelManager)
		{
			if (channelManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelManager");
			}
			this.channelManager = channelManager;
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262175, SR.GetString("TraceCodeChannelCreated", new object[]
				{
					TraceUtility.CreateSourceString(this)
				}), this);
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x00102F3E File Offset: 0x0010113E
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.CloseTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultCloseTimeout;
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x00102F46 File Offset: 0x00101146
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.OpenTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultOpenTimeout;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x00102F4E File Offset: 0x0010114E
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.ReceiveTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultReceiveTimeout;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x00102F56 File Offset: 0x00101156
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.SendTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultSendTimeout;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x00102F5E File Offset: 0x0010115E
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultCloseTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return ((IDefaultCommunicationTimeouts)this.channelManager).CloseTimeout;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x00102F6B File Offset: 0x0010116B
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultOpenTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return ((IDefaultCommunicationTimeouts)this.channelManager).OpenTimeout;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x00102F78 File Offset: 0x00101178
		[__DynamicallyInvokable]
		protected TimeSpan DefaultReceiveTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return ((IDefaultCommunicationTimeouts)this.channelManager).ReceiveTimeout;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x00102F85 File Offset: 0x00101185
		[__DynamicallyInvokable]
		protected TimeSpan DefaultSendTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return ((IDefaultCommunicationTimeouts)this.channelManager).SendTimeout;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x00102F92 File Offset: 0x00101192
		[__DynamicallyInvokable]
		protected ChannelManagerBase Manager
		{
			[__DynamicallyInvokable]
			get
			{
				return this.channelManager;
			}
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x00102F9C File Offset: 0x0010119C
		[__DynamicallyInvokable]
		public virtual T GetProperty<T>() where T : class
		{
			IChannelFactory channelFactory = this.channelManager as IChannelFactory;
			if (channelFactory != null)
			{
				return channelFactory.GetProperty<T>();
			}
			IChannelListener channelListener = this.channelManager as IChannelListener;
			if (channelListener != null)
			{
				return channelListener.GetProperty<T>();
			}
			return default(T);
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00102FDE File Offset: 0x001011DE
		[__DynamicallyInvokable]
		protected override void OnClosed()
		{
			base.OnClosed();
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262176, SR.GetString("TraceCodeChannelDisposed", new object[]
				{
					TraceUtility.CreateSourceString(this)
				}), this);
			}
		}

		// Token: 0x04002D48 RID: 11592
		private ChannelManagerBase channelManager;
	}
}
