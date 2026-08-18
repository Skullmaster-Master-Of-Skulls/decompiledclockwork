using System;
using System.ComponentModel;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006F5 RID: 1781
	[__DynamicallyInvokable]
	public abstract class Binding : IDefaultCommunicationTimeouts
	{
		// Token: 0x0600442D RID: 17453 RVA: 0x00101700 File Offset: 0x000FF900
		[__DynamicallyInvokable]
		protected Binding()
		{
			this.name = null;
			this.namespaceIdentifier = "http://tempuri.org/";
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x00101754 File Offset: 0x000FF954
		[__DynamicallyInvokable]
		protected Binding(string name, string ns)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("name", SR.GetString("SFXBindingNameCannotBeNullOrEmpty"));
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			if (ns.Length > 0)
			{
				NamingHelper.CheckUriParameter(ns, "ns");
			}
			this.name = name;
			this.namespaceIdentifier = ns;
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x001017EA File Offset: 0x000FF9EA
		// (set) Token: 0x06004430 RID: 17456 RVA: 0x001017F4 File Offset: 0x000FF9F4
		[DefaultValue(typeof(TimeSpan), "00:01:00")]
		[__DynamicallyInvokable]
		public TimeSpan CloseTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.closeTimeout;
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
				this.closeTimeout = value;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06004431 RID: 17457 RVA: 0x00101867 File Offset: 0x000FFA67
		// (set) Token: 0x06004432 RID: 17458 RVA: 0x00101888 File Offset: 0x000FFA88
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.name == null)
				{
					this.name = base.GetType().Name;
				}
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("SFXBindingNameCannotBeNullOrEmpty"));
				}
				this.name = value;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06004433 RID: 17459 RVA: 0x001018B3 File Offset: 0x000FFAB3
		// (set) Token: 0x06004434 RID: 17460 RVA: 0x001018BB File Offset: 0x000FFABB
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.namespaceIdentifier;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.Length > 0)
				{
					NamingHelper.CheckUriProperty(value, "Namespace");
				}
				this.namespaceIdentifier = value;
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x001018EB File Offset: 0x000FFAEB
		// (set) Token: 0x06004436 RID: 17462 RVA: 0x001018F4 File Offset: 0x000FFAF4
		[DefaultValue(typeof(TimeSpan), "00:01:00")]
		[__DynamicallyInvokable]
		public TimeSpan OpenTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.openTimeout;
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
				this.openTimeout = value;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x00101967 File Offset: 0x000FFB67
		// (set) Token: 0x06004438 RID: 17464 RVA: 0x00101970 File Offset: 0x000FFB70
		[DefaultValue(typeof(TimeSpan), "00:10:00")]
		[__DynamicallyInvokable]
		public TimeSpan ReceiveTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.receiveTimeout;
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
				this.receiveTimeout = value;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06004439 RID: 17465
		[__DynamicallyInvokable]
		public abstract string Scheme { [__DynamicallyInvokable] get; }

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x001019E3 File Offset: 0x000FFBE3
		[__DynamicallyInvokable]
		public MessageVersion MessageVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetProperty<MessageVersion>(new BindingParameterCollection());
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x001019F0 File Offset: 0x000FFBF0
		// (set) Token: 0x0600443C RID: 17468 RVA: 0x001019F8 File Offset: 0x000FFBF8
		[DefaultValue(typeof(TimeSpan), "00:01:00")]
		[__DynamicallyInvokable]
		public TimeSpan SendTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.sendTimeout;
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
				this.sendTimeout = value;
			}
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00101A6B File Offset: 0x000FFC6B
		[__DynamicallyInvokable]
		public IChannelFactory<TChannel> BuildChannelFactory<TChannel>(params object[] parameters)
		{
			return this.BuildChannelFactory<TChannel>(new BindingParameterCollection(parameters));
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x00101A7C File Offset: 0x000FFC7C
		[__DynamicallyInvokable]
		public virtual IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingParameterCollection parameters)
		{
			this.EnsureInvariants();
			BindingContext bindingContext = new BindingContext(new CustomBinding(this), parameters);
			IChannelFactory<TChannel> channelFactory = bindingContext.BuildInnerChannelFactory<TChannel>();
			bindingContext.ValidateBindingElementsConsumed();
			this.ValidateSecurityCapabilities(channelFactory.GetProperty<ISecurityCapabilities>(), parameters);
			return channelFactory;
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x00101AB8 File Offset: 0x000FFCB8
		private void ValidateSecurityCapabilities(ISecurityCapabilities runtimeSecurityCapabilities, BindingParameterCollection parameters)
		{
			ISecurityCapabilities property = this.GetProperty<ISecurityCapabilities>(parameters);
			if (!SecurityCapabilities.IsEqual(property, runtimeSecurityCapabilities))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityCapabilitiesMismatched", new object[]
				{
					this
				})));
			}
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x00101AFA File Offset: 0x000FFCFA
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(params object[] parameters) where TChannel : class, IChannel
		{
			return this.BuildChannelListener<TChannel>(new BindingParameterCollection(parameters));
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x00101B08 File Offset: 0x000FFD08
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, params object[] parameters) where TChannel : class, IChannel
		{
			return this.BuildChannelListener<TChannel>(listenUriBaseAddress, new BindingParameterCollection(parameters));
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x00101B17 File Offset: 0x000FFD17
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, params object[] parameters) where TChannel : class, IChannel
		{
			return this.BuildChannelListener<TChannel>(listenUriBaseAddress, listenUriRelativeAddress, new BindingParameterCollection(parameters));
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x00101B27 File Offset: 0x000FFD27
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode, params object[] parameters) where TChannel : class, IChannel
		{
			return this.BuildChannelListener<TChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, new BindingParameterCollection(parameters));
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x00101B3C File Offset: 0x000FFD3C
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingParameterCollection parameters) where TChannel : class, IChannel
		{
			UriBuilder uriBuilder = new UriBuilder(this.Scheme, DnsCache.MachineName);
			return this.BuildChannelListener<TChannel>(uriBuilder.Uri, string.Empty, ListenUriMode.Unique, parameters);
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x00101B6D File Offset: 0x000FFD6D
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, BindingParameterCollection parameters) where TChannel : class, IChannel
		{
			return this.BuildChannelListener<TChannel>(listenUriBaseAddress, string.Empty, ListenUriMode.Explicit, parameters);
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x00101B7D File Offset: 0x000FFD7D
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, BindingParameterCollection parameters) where TChannel : class, IChannel
		{
			return this.BuildChannelListener<TChannel>(listenUriBaseAddress, listenUriRelativeAddress, ListenUriMode.Explicit, parameters);
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x00101B8C File Offset: 0x000FFD8C
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode, BindingParameterCollection parameters) where TChannel : class, IChannel
		{
			this.EnsureInvariants();
			BindingContext bindingContext = new BindingContext(new CustomBinding(this), parameters, listenUriBaseAddress, listenUriRelativeAddress, listenUriMode);
			IChannelListener<TChannel> channelListener = bindingContext.BuildInnerChannelListener<TChannel>();
			bindingContext.ValidateBindingElementsConsumed();
			this.ValidateSecurityCapabilities(channelListener.GetProperty<ISecurityCapabilities>(), parameters);
			return channelListener;
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x00101BCC File Offset: 0x000FFDCC
		[__DynamicallyInvokable]
		public bool CanBuildChannelFactory<TChannel>(params object[] parameters)
		{
			return this.CanBuildChannelFactory<TChannel>(new BindingParameterCollection(parameters));
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x00101BDC File Offset: 0x000FFDDC
		[__DynamicallyInvokable]
		public virtual bool CanBuildChannelFactory<TChannel>(BindingParameterCollection parameters)
		{
			BindingContext bindingContext = new BindingContext(new CustomBinding(this), parameters);
			return bindingContext.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x00101BFC File Offset: 0x000FFDFC
		public bool CanBuildChannelListener<TChannel>(params object[] parameters) where TChannel : class, IChannel
		{
			return this.CanBuildChannelListener<TChannel>(new BindingParameterCollection(parameters));
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x00101C0C File Offset: 0x000FFE0C
		public virtual bool CanBuildChannelListener<TChannel>(BindingParameterCollection parameters) where TChannel : class, IChannel
		{
			BindingContext bindingContext = new BindingContext(new CustomBinding(this), parameters);
			return bindingContext.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x0600444C RID: 17484
		[__DynamicallyInvokable]
		public abstract BindingElementCollection CreateBindingElements();

		// Token: 0x0600444D RID: 17485 RVA: 0x00101C2C File Offset: 0x000FFE2C
		[__DynamicallyInvokable]
		public T GetProperty<T>(BindingParameterCollection parameters) where T : class
		{
			BindingContext bindingContext = new BindingContext(new CustomBinding(this), parameters);
			return bindingContext.GetInnerProperty<T>();
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x00101C4C File Offset: 0x000FFE4C
		private void EnsureInvariants()
		{
			this.EnsureInvariants(null);
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x00101C58 File Offset: 0x000FFE58
		internal void EnsureInvariants(string contractName)
		{
			BindingElementCollection bindingElementCollection = this.CreateBindingElements();
			TransportBindingElement transportBindingElement = null;
			int i;
			for (i = 0; i < bindingElementCollection.Count; i++)
			{
				transportBindingElement = (bindingElementCollection[i] as TransportBindingElement);
				if (transportBindingElement != null)
				{
					break;
				}
			}
			if (transportBindingElement == null)
			{
				if (contractName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CustomBindingRequiresTransport", new object[]
					{
						this.Name
					})));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCustomBindingNeedsTransport1", new object[]
				{
					contractName
				})));
			}
			else
			{
				if (i != bindingElementCollection.Count - 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportBindingElementMustBeLast", new object[]
					{
						this.Name,
						transportBindingElement.GetType().Name
					})));
				}
				if (string.IsNullOrEmpty(transportBindingElement.Scheme))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidBindingScheme", new object[]
					{
						transportBindingElement.GetType().Name
					})));
				}
				if (this.MessageVersion == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageVersionMissingFromBinding", new object[]
					{
						this.Name
					})));
				}
				return;
			}
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x00101D90 File Offset: 0x000FFF90
		internal void CopyTimeouts(IDefaultCommunicationTimeouts source)
		{
			this.CloseTimeout = source.CloseTimeout;
			this.OpenTimeout = source.OpenTimeout;
			this.ReceiveTimeout = source.ReceiveTimeout;
			this.SendTimeout = source.SendTimeout;
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x00101DC2 File Offset: 0x000FFFC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeName()
		{
			return this.Name != base.GetType().Name;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x00101DDA File Offset: 0x000FFFDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeNamespace()
		{
			return this.Namespace != "http://tempuri.org/";
		}

		// Token: 0x04002D2D RID: 11565
		private TimeSpan closeTimeout = ServiceDefaults.CloseTimeout;

		// Token: 0x04002D2E RID: 11566
		private string name;

		// Token: 0x04002D2F RID: 11567
		private string namespaceIdentifier;

		// Token: 0x04002D30 RID: 11568
		private TimeSpan openTimeout = ServiceDefaults.OpenTimeout;

		// Token: 0x04002D31 RID: 11569
		private TimeSpan receiveTimeout = ServiceDefaults.ReceiveTimeout;

		// Token: 0x04002D32 RID: 11570
		private TimeSpan sendTimeout = ServiceDefaults.SendTimeout;

		// Token: 0x04002D33 RID: 11571
		internal const string DefaultNamespace = "http://tempuri.org/";
	}
}
