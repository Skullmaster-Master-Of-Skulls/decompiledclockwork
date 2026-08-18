using System;
using System.ComponentModel;
using System.ServiceModel.Description;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F5 RID: 2549
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	public sealed class PeerCustomResolverBindingElement : PeerResolverBindingElement
	{
		// Token: 0x06006519 RID: 25881 RVA: 0x001792AC File Offset: 0x001774AC
		public PeerCustomResolverBindingElement()
		{
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x001792B4 File Offset: 0x001774B4
		public PeerCustomResolverBindingElement(PeerCustomResolverBindingElement other) : base(other)
		{
			this.address = other.address;
			this.bindingConfiguration = other.bindingConfiguration;
			this.bindingSection = other.bindingSection;
			this.binding = other.binding;
			this.resolver = other.resolver;
			this.credentials = other.credentials;
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x00179310 File Offset: 0x00177510
		public PeerCustomResolverBindingElement(PeerCustomResolverSettings settings)
		{
			if (settings != null)
			{
				this.address = settings.Address;
				this.binding = settings.Binding;
				this.resolver = settings.Resolver;
				this.bindingConfiguration = settings.BindingConfiguration;
				this.bindingSection = settings.BindingSection;
			}
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x00179362 File Offset: 0x00177562
		public PeerCustomResolverBindingElement(BindingContext context, PeerCustomResolverSettings settings) : this(settings)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			this.credentials = context.BindingParameters.Find<ClientCredentials>();
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x00179394 File Offset: 0x00177594
		public override T GetProperty<T>(BindingContext context)
		{
			return context.GetInnerProperty<T>();
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x0600651E RID: 25886 RVA: 0x0017939C File Offset: 0x0017759C
		// (set) Token: 0x0600651F RID: 25887 RVA: 0x001793A4 File Offset: 0x001775A4
		public EndpointAddress Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x06006520 RID: 25888 RVA: 0x001793AD File Offset: 0x001775AD
		// (set) Token: 0x06006521 RID: 25889 RVA: 0x001793B5 File Offset: 0x001775B5
		public Binding Binding
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

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x06006522 RID: 25890 RVA: 0x001793BE File Offset: 0x001775BE
		// (set) Token: 0x06006523 RID: 25891 RVA: 0x001793C6 File Offset: 0x001775C6
		public override PeerReferralPolicy ReferralPolicy
		{
			get
			{
				return this.referralPolicy;
			}
			set
			{
				if (!PeerReferralPolicyHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(PeerReferralPolicy)));
				}
				this.referralPolicy = value;
			}
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x001793F7 File Offset: 0x001775F7
		public override BindingElement Clone()
		{
			return new PeerCustomResolverBindingElement(this);
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x001793FF File Offset: 0x001775FF
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			this.credentials = context.BindingParameters.Find<ClientCredentials>();
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x0017943C File Offset: 0x0017763C
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			this.credentials = context.BindingParameters.Find<ClientCredentials>();
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x00179479 File Offset: 0x00177679
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			this.credentials = context.BindingParameters.Find<ClientCredentials>();
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x001794B6 File Offset: 0x001776B6
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			this.credentials = context.BindingParameters.Find<ClientCredentials>();
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x001794F4 File Offset: 0x001776F4
		public override PeerResolver CreatePeerResolver()
		{
			if (this.resolver == null)
			{
				if (this.address == null || (this.binding == null && (string.IsNullOrEmpty(this.bindingSection) || string.IsNullOrEmpty(this.bindingConfiguration))))
				{
					PeerExceptionHelper.ThrowArgument_InsufficientResolverSettings();
				}
				if (this.binding == null)
				{
					this.binding = ConfigLoader.LookupBinding(this.bindingSection, this.bindingConfiguration);
					if (this.binding == null)
					{
						PeerExceptionHelper.ThrowArgument_InsufficientResolverSettings();
					}
				}
				this.resolver = new PeerDefaultCustomResolverClient();
			}
			if (this.resolver != null)
			{
				this.resolver.Initialize(this.address, this.binding, this.credentials, this.referralPolicy);
				if (this.resolver is PeerDefaultCustomResolverClient)
				{
					(this.resolver as PeerDefaultCustomResolverClient).BindingName = this.bindingSection;
					(this.resolver as PeerDefaultCustomResolverClient).BindingConfigurationName = this.bindingConfiguration;
				}
			}
			return this.resolver;
		}

		// Token: 0x04003A06 RID: 14854
		private EndpointAddress address;

		// Token: 0x04003A07 RID: 14855
		private Binding binding;

		// Token: 0x04003A08 RID: 14856
		private string bindingSection;

		// Token: 0x04003A09 RID: 14857
		private string bindingConfiguration;

		// Token: 0x04003A0A RID: 14858
		private PeerResolver resolver;

		// Token: 0x04003A0B RID: 14859
		private ClientCredentials credentials;

		// Token: 0x04003A0C RID: 14860
		private PeerReferralPolicy referralPolicy;
	}
}
