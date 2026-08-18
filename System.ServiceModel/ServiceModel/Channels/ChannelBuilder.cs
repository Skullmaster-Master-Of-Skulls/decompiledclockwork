using System;
using System.Runtime;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200071D RID: 1821
	internal class ChannelBuilder
	{
		// Token: 0x0600451A RID: 17690 RVA: 0x00103014 File Offset: 0x00101214
		public ChannelBuilder(BindingContext context, bool addChannelDemuxerIfRequired)
		{
			this.context = context;
			if (addChannelDemuxerIfRequired)
			{
				this.AddDemuxerBindingElement(context.RemainingBindingElements);
			}
			this.binding = new CustomBinding(context.Binding, context.RemainingBindingElements);
			this.bindingParameters = context.BindingParameters;
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x00103060 File Offset: 0x00101260
		public ChannelBuilder(Binding binding, BindingParameterCollection bindingParameters, bool addChannelDemuxerIfRequired)
		{
			this.binding = new CustomBinding(binding);
			this.bindingParameters = bindingParameters;
			if (addChannelDemuxerIfRequired)
			{
				this.AddDemuxerBindingElement(this.binding.Elements);
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x00103090 File Offset: 0x00101290
		public ChannelBuilder(ChannelBuilder channelBuilder)
		{
			this.binding = new CustomBinding(channelBuilder.Binding);
			this.bindingParameters = channelBuilder.BindingParameters;
			if (this.binding.Elements.Find<ChannelDemuxerBindingElement>() == null)
			{
				throw Fx.AssertAndThrow("ChannelBuilder.ctor (this.binding.Elements.Find<ChannelDemuxerBindingElement>() != null)");
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x001030DD File Offset: 0x001012DD
		// (set) Token: 0x0600451E RID: 17694 RVA: 0x001030E5 File Offset: 0x001012E5
		public CustomBinding Binding
		{
			get
			{
				return this.binding;
			}
			set
			{
				this.binding = value;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x001030EE File Offset: 0x001012EE
		// (set) Token: 0x06004520 RID: 17696 RVA: 0x001030F6 File Offset: 0x001012F6
		public BindingParameterCollection BindingParameters
		{
			get
			{
				return this.bindingParameters;
			}
			set
			{
				this.bindingParameters = value;
			}
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x00103100 File Offset: 0x00101300
		private void AddDemuxerBindingElement(BindingElementCollection elements)
		{
			if (elements.Find<ChannelDemuxerBindingElement>() == null)
			{
				TransportBindingElement transportBindingElement = elements.Find<TransportBindingElement>();
				if (transportBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportBindingElementNotFound")));
				}
				elements.Insert(elements.IndexOf(transportBindingElement), new ChannelDemuxerBindingElement(true));
			}
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x0010314C File Offset: 0x0010134C
		public IChannelFactory<TChannel> BuildChannelFactory<TChannel>()
		{
			if (this.context != null)
			{
				IChannelFactory<TChannel> result = this.context.BuildInnerChannelFactory<TChannel>();
				this.context = null;
				return result;
			}
			return this.binding.BuildChannelFactory<TChannel>(this.bindingParameters);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x00103188 File Offset: 0x00101388
		public IChannelListener<TChannel> BuildChannelListener<TChannel>() where TChannel : class, IChannel
		{
			if (this.context != null)
			{
				IChannelListener<TChannel> channelListener = this.context.BuildInnerChannelListener<TChannel>();
				this.listenUri = channelListener.Uri;
				this.context = null;
				return channelListener;
			}
			return this.binding.BuildChannelListener<TChannel>(this.listenUri, this.bindingParameters);
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x001031D8 File Offset: 0x001013D8
		public IChannelListener<TChannel> BuildChannelListener<TChannel>(MessageFilter filter, int priority) where TChannel : class, IChannel
		{
			this.bindingParameters.Add(new ChannelDemuxerFilter(filter, priority));
			IChannelListener<TChannel> result = this.BuildChannelListener<TChannel>();
			this.bindingParameters.Remove<ChannelDemuxerFilter>();
			return result;
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0010320B File Offset: 0x0010140B
		public bool CanBuildChannelFactory<TChannel>()
		{
			return this.binding.CanBuildChannelFactory<TChannel>(this.bindingParameters);
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0010321E File Offset: 0x0010141E
		public bool CanBuildChannelListener<TChannel>() where TChannel : class, IChannel
		{
			return this.binding.CanBuildChannelListener<TChannel>(this.bindingParameters);
		}

		// Token: 0x04002D49 RID: 11593
		private CustomBinding binding;

		// Token: 0x04002D4A RID: 11594
		private BindingContext context;

		// Token: 0x04002D4B RID: 11595
		private BindingParameterCollection bindingParameters;

		// Token: 0x04002D4C RID: 11596
		private Uri listenUri;
	}
}
