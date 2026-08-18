using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000763 RID: 1891
	internal class ReplyAdapterBindingElement : BindingElement
	{
		// Token: 0x06004834 RID: 18484 RVA: 0x0010B300 File Offset: 0x00109500
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			if (context.CanBuildInnerChannelListener<IReplySessionChannel>() || context.CanBuildInnerChannelListener<IReplyChannel>())
			{
				return context.BuildInnerChannelListener<TChannel>();
			}
			if (typeof(TChannel) == typeof(IReplySessionChannel) && context.CanBuildInnerChannelListener<IDuplexSessionChannel>())
			{
				return (IChannelListener<TChannel>)new ReplySessionOverDuplexSessionChannelListener(context);
			}
			if (typeof(TChannel) == typeof(IReplyChannel) && context.CanBuildInnerChannelListener<IDuplexChannel>())
			{
				return (IChannelListener<TChannel>)new ReplyOverDuplexChannelListener(context);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}));
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x0010B3E4 File Offset: 0x001095E4
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (typeof(TChannel) == typeof(IReplySessionChannel))
			{
				return context.CanBuildInnerChannelListener<IReplySessionChannel>() || context.CanBuildInnerChannelListener<IDuplexSessionChannel>();
			}
			return typeof(TChannel) == typeof(IReplyChannel) && (context.CanBuildInnerChannelListener<IReplyChannel>() || context.CanBuildInnerChannelListener<IDuplexChannel>());
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0010B44A File Offset: 0x0010964A
		public override BindingElement Clone()
		{
			return new ReplyAdapterBindingElement();
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0010B451 File Offset: 0x00109651
		public override T GetProperty<T>(BindingContext context)
		{
			return context.GetInnerProperty<T>();
		}
	}
}
