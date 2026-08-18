using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200073C RID: 1852
	[__DynamicallyInvokable]
	public abstract class ChannelManagerBase : CommunicationObject, IDefaultCommunicationTimeouts
	{
		// Token: 0x0600466E RID: 18030 RVA: 0x00106B85 File Offset: 0x00104D85
		[__DynamicallyInvokable]
		protected ChannelManagerBase()
		{
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x0600466F RID: 18031
		[__DynamicallyInvokable]
		protected abstract TimeSpan DefaultReceiveTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06004670 RID: 18032
		[__DynamicallyInvokable]
		protected abstract TimeSpan DefaultSendTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004671 RID: 18033 RVA: 0x00106B8D File Offset: 0x00104D8D
		internal TimeSpan InternalReceiveTimeout
		{
			get
			{
				return this.DefaultReceiveTimeout;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06004672 RID: 18034 RVA: 0x00106B95 File Offset: 0x00104D95
		internal TimeSpan InternalSendTimeout
		{
			get
			{
				return this.DefaultSendTimeout;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x00106B9D File Offset: 0x00104D9D
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.CloseTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultCloseTimeout;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06004674 RID: 18036 RVA: 0x00106BA5 File Offset: 0x00104DA5
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.OpenTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultOpenTimeout;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x00106BAD File Offset: 0x00104DAD
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.ReceiveTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultReceiveTimeout;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x00106BB5 File Offset: 0x00104DB5
		[__DynamicallyInvokable]
		TimeSpan IDefaultCommunicationTimeouts.SendTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.DefaultSendTimeout;
			}
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x00106BBD File Offset: 0x00104DBD
		internal Exception CreateChannelTypeNotSupportedException(Type type)
		{
			return new ArgumentException(SR.GetString("ChannelTypeNotSupported", new object[]
			{
				type
			}), "TChannel");
		}
	}
}
