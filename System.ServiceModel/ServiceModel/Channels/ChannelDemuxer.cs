using System;
using System.Collections.Generic;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200071E RID: 1822
	internal class ChannelDemuxer
	{
		// Token: 0x06004527 RID: 17703 RVA: 0x00103231 File Offset: 0x00101431
		public ChannelDemuxer()
		{
			this.peekTimeout = ChannelDemuxer.UseDefaultReceiveTimeout;
			this.maxPendingSessions = 10;
			this.typeDemuxers = new Dictionary<Type, TypedChannelDemuxer>();
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x00103257 File Offset: 0x00101457
		// (set) Token: 0x06004529 RID: 17705 RVA: 0x0010325F File Offset: 0x0010145F
		public TimeSpan PeekTimeout
		{
			get
			{
				return this.peekTimeout;
			}
			set
			{
				this.peekTimeout = value;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x00103268 File Offset: 0x00101468
		// (set) Token: 0x0600452B RID: 17707 RVA: 0x00103270 File Offset: 0x00101470
		public int MaxPendingSessions
		{
			get
			{
				return this.maxPendingSessions;
			}
			set
			{
				this.maxPendingSessions = value;
			}
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x00103279 File Offset: 0x00101479
		public IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			return this.BuildChannelListener<TChannel>(context, new ChannelDemuxerFilter(new MatchAllMessageFilter(), 0));
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x0010328D File Offset: 0x0010148D
		public IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context, ChannelDemuxerFilter filter) where TChannel : class, IChannel
		{
			return this.GetTypedDemuxer(typeof(TChannel), context).BuildChannelListener<TChannel>(filter);
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x001032A8 File Offset: 0x001014A8
		private TypedChannelDemuxer CreateTypedDemuxer(Type channelType, BindingContext context)
		{
			if (channelType == typeof(IDuplexChannel))
			{
				return (TypedChannelDemuxer)new DuplexChannelDemuxer(context);
			}
			if (channelType == typeof(IInputSessionChannel))
			{
				return (TypedChannelDemuxer)new InputSessionChannelDemuxer(context, this.peekTimeout, this.maxPendingSessions);
			}
			if (channelType == typeof(IReplySessionChannel))
			{
				return (TypedChannelDemuxer)new ReplySessionChannelDemuxer(context, this.peekTimeout, this.maxPendingSessions);
			}
			if (channelType == typeof(IDuplexSessionChannel))
			{
				return (TypedChannelDemuxer)new DuplexSessionChannelDemuxer(context, this.peekTimeout, this.maxPendingSessions);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x00103360 File Offset: 0x00101560
		private TypedChannelDemuxer GetTypedDemuxer(Type channelType, BindingContext context)
		{
			TypedChannelDemuxer typedChannelDemuxer = null;
			bool flag = false;
			if (channelType == typeof(IInputChannel))
			{
				if (this.inputDemuxer == null)
				{
					if (context.CanBuildInnerChannelListener<IReplyChannel>())
					{
						this.inputDemuxer = (this.replyDemuxer = new ReplyChannelDemuxer(context));
					}
					else
					{
						this.inputDemuxer = new InputChannelDemuxer(context);
					}
					flag = true;
				}
				typedChannelDemuxer = this.inputDemuxer;
			}
			else if (channelType == typeof(IReplyChannel))
			{
				if (this.replyDemuxer == null)
				{
					this.inputDemuxer = (this.replyDemuxer = new ReplyChannelDemuxer(context));
					flag = true;
				}
				typedChannelDemuxer = this.replyDemuxer;
			}
			else if (!this.typeDemuxers.TryGetValue(channelType, out typedChannelDemuxer))
			{
				typedChannelDemuxer = this.CreateTypedDemuxer(channelType, context);
				this.typeDemuxers.Add(channelType, typedChannelDemuxer);
				flag = true;
			}
			if (!flag)
			{
				context.RemainingBindingElements.Clear();
			}
			return typedChannelDemuxer;
		}

		// Token: 0x04002D4D RID: 11597
		public static readonly TimeSpan UseDefaultReceiveTimeout = TimeSpan.MinValue;

		// Token: 0x04002D4E RID: 11598
		private TypedChannelDemuxer inputDemuxer;

		// Token: 0x04002D4F RID: 11599
		private TypedChannelDemuxer replyDemuxer;

		// Token: 0x04002D50 RID: 11600
		private Dictionary<Type, TypedChannelDemuxer> typeDemuxers;

		// Token: 0x04002D51 RID: 11601
		private TimeSpan peekTimeout;

		// Token: 0x04002D52 RID: 11602
		private int maxPendingSessions;
	}
}
