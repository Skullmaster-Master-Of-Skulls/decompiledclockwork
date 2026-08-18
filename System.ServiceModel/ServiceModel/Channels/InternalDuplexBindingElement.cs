using System;
using System.Net.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200089F RID: 2207
	internal sealed class InternalDuplexBindingElement : BindingElement
	{
		// Token: 0x0600542C RID: 21548 RVA: 0x00136174 File Offset: 0x00134374
		public InternalDuplexBindingElement() : this(false)
		{
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0013617D File Offset: 0x0013437D
		internal InternalDuplexBindingElement(bool providesCorrelation)
		{
			this.providesCorrelation = providesCorrelation;
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0013618C File Offset: 0x0013438C
		private InternalDuplexBindingElement(InternalDuplexBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.clientChannelDemuxer = elementToBeCloned.ClientChannelDemuxer;
			this.providesCorrelation = elementToBeCloned.ProvidesCorrelation;
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x001361AD File Offset: 0x001343AD
		internal InputChannelDemuxer ClientChannelDemuxer
		{
			get
			{
				return this.clientChannelDemuxer;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06005430 RID: 21552 RVA: 0x001361B5 File Offset: 0x001343B5
		internal bool ProvidesCorrelation
		{
			get
			{
				return this.providesCorrelation;
			}
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x001361BD File Offset: 0x001343BD
		public override BindingElement Clone()
		{
			return new InternalDuplexBindingElement(this);
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x001361C8 File Offset: 0x001343C8
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			IChannelFactory<IOutputChannel> innerChannelFactory = context.Clone().BuildInnerChannelFactory<IOutputChannel>();
			if (this.clientChannelDemuxer == null)
			{
				this.clientChannelDemuxer = new InputChannelDemuxer(context);
			}
			else
			{
				context.RemainingBindingElements.Clear();
			}
			LocalAddressProvider localAddressProvider = context.BindingParameters.Remove<LocalAddressProvider>();
			return (IChannelFactory<TChannel>)new InternalDuplexChannelFactory(this, context, this.clientChannelDemuxer, innerChannelFactory, localAddressProvider);
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0013626C File Offset: 0x0013446C
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(TChannel) != typeof(IDuplexChannel))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			return (IChannelListener<TChannel>)new InternalDuplexChannelListener(this, context);
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x001362E0 File Offset: 0x001344E0
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return typeof(TChannel) == typeof(IDuplexChannel) && context.CanBuildInnerChannelFactory<IOutputChannel>() && context.CanBuildInnerChannelListener<IInputChannel>();
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00136320 File Offset: 0x00134520
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return typeof(TChannel) == typeof(IDuplexChannel) && context.CanBuildInnerChannelFactory<IOutputChannel>() && context.CanBuildInnerChannelListener<IInputChannel>();
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00136360 File Offset: 0x00134560
		internal static T GetSecurityCapabilities<T>(ISecurityCapabilities lowerCapabilities)
		{
			if (lowerCapabilities != null)
			{
				return (T)((object)new SecurityCapabilities(lowerCapabilities.SupportsClientAuthentication, false, lowerCapabilities.SupportsClientWindowsIdentity, lowerCapabilities.SupportedRequestProtectionLevel, ProtectionLevel.None));
			}
			return (T)((object)null);
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0013638A File Offset: 0x0013458A
		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(ISecurityCapabilities) && !this.ProvidesCorrelation)
			{
				return InternalDuplexBindingElement.GetSecurityCapabilities<T>(context.GetInnerProperty<ISecurityCapabilities>());
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x001363C1 File Offset: 0x001345C1
		internal override bool IsMatch(BindingElement b)
		{
			return b != null && b is InternalDuplexBindingElement;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x001363D4 File Offset: 0x001345D4
		public static void AddDuplexFactorySupport(BindingContext context, ref InternalDuplexBindingElement internalDuplexBindingElement)
		{
			if (context.CanBuildInnerChannelFactory<IDuplexChannel>())
			{
				return;
			}
			if (context.RemainingBindingElements.Find<CompositeDuplexBindingElement>() == null)
			{
				return;
			}
			if (context.CanBuildInnerChannelFactory<IOutputChannel>() && context.CanBuildInnerChannelListener<IInputChannel>())
			{
				if (context.CanBuildInnerChannelFactory<IRequestChannel>())
				{
					return;
				}
				if (context.CanBuildInnerChannelFactory<IRequestSessionChannel>())
				{
					return;
				}
				if (context.CanBuildInnerChannelFactory<IOutputSessionChannel>())
				{
					return;
				}
				if (context.CanBuildInnerChannelFactory<IDuplexSessionChannel>())
				{
					return;
				}
				if (internalDuplexBindingElement == null)
				{
					internalDuplexBindingElement = new InternalDuplexBindingElement();
				}
				context.RemainingBindingElements.Insert(0, internalDuplexBindingElement);
			}
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x00136448 File Offset: 0x00134648
		public static void AddDuplexListenerSupport(BindingContext context, ref InternalDuplexBindingElement internalDuplexBindingElement)
		{
			if (context.CanBuildInnerChannelListener<IDuplexChannel>())
			{
				return;
			}
			if (context.RemainingBindingElements.Find<CompositeDuplexBindingElement>() == null)
			{
				return;
			}
			if (context.CanBuildInnerChannelFactory<IOutputChannel>() && context.CanBuildInnerChannelListener<IInputChannel>())
			{
				if (context.CanBuildInnerChannelListener<IReplyChannel>())
				{
					return;
				}
				if (context.CanBuildInnerChannelListener<IReplySessionChannel>())
				{
					return;
				}
				if (context.CanBuildInnerChannelListener<IInputSessionChannel>())
				{
					return;
				}
				if (context.CanBuildInnerChannelListener<IDuplexSessionChannel>())
				{
					return;
				}
				if (internalDuplexBindingElement == null)
				{
					internalDuplexBindingElement = new InternalDuplexBindingElement();
				}
				context.RemainingBindingElements.Insert(0, internalDuplexBindingElement);
			}
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x001364BC File Offset: 0x001346BC
		public static void AddDuplexListenerSupport(CustomBinding binding, ref InternalDuplexBindingElement internalDuplexBindingElement)
		{
			if (binding.CanBuildChannelListener<IDuplexChannel>(new object[0]))
			{
				return;
			}
			if (binding.Elements.Find<CompositeDuplexBindingElement>() == null)
			{
				return;
			}
			if (binding.CanBuildChannelFactory<IOutputChannel>(new object[0]) && binding.CanBuildChannelListener<IInputChannel>(new object[0]))
			{
				if (binding.CanBuildChannelListener<IReplyChannel>(new object[0]))
				{
					return;
				}
				if (binding.CanBuildChannelListener<IReplySessionChannel>(new object[0]))
				{
					return;
				}
				if (binding.CanBuildChannelListener<IInputSessionChannel>(new object[0]))
				{
					return;
				}
				if (binding.CanBuildChannelListener<IDuplexSessionChannel>(new object[0]))
				{
					return;
				}
				if (internalDuplexBindingElement == null)
				{
					internalDuplexBindingElement = new InternalDuplexBindingElement();
				}
				binding.Elements.Insert(0, internalDuplexBindingElement);
			}
		}

		// Token: 0x04003302 RID: 13058
		private InputChannelDemuxer clientChannelDemuxer;

		// Token: 0x04003303 RID: 13059
		private bool providesCorrelation;
	}
}
