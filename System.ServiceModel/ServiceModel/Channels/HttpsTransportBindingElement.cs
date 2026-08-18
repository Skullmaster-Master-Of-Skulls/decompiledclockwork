using System;
using System.ComponentModel;
using System.Net;
using System.Net.Security;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200089E RID: 2206
	[__DynamicallyInvokable]
	public class HttpsTransportBindingElement : HttpTransportBindingElement, ITransportTokenAssertionProvider
	{
		// Token: 0x0600541A RID: 21530 RVA: 0x00135EFA File Offset: 0x001340FA
		[__DynamicallyInvokable]
		public HttpsTransportBindingElement()
		{
			this.requireClientCertificate = false;
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x00135F09 File Offset: 0x00134109
		[__DynamicallyInvokable]
		protected HttpsTransportBindingElement(HttpsTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.requireClientCertificate = elementToBeCloned.requireClientCertificate;
			this.messageSecurityVersion = elementToBeCloned.messageSecurityVersion;
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x00135F2A File Offset: 0x0013412A
		private HttpsTransportBindingElement(HttpTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x00135F33 File Offset: 0x00134133
		// (set) Token: 0x0600541E RID: 21534 RVA: 0x00135F3B File Offset: 0x0013413B
		[DefaultValue(false)]
		public bool RequireClientCertificate
		{
			get
			{
				return this.requireClientCertificate;
			}
			set
			{
				this.requireClientCertificate = value;
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x00135F44 File Offset: 0x00134144
		[__DynamicallyInvokable]
		public override string Scheme
		{
			[__DynamicallyInvokable]
			get
			{
				return "https";
			}
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x00135F4B File Offset: 0x0013414B
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new HttpsTransportBindingElement(this);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x00135F53 File Offset: 0x00134153
		internal override bool GetSupportsClientAuthenticationImpl(AuthenticationSchemes effectiveAuthenticationSchemes)
		{
			return this.requireClientCertificate || base.GetSupportsClientAuthenticationImpl(effectiveAuthenticationSchemes);
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x00135F66 File Offset: 0x00134166
		internal override bool GetSupportsClientWindowsIdentityImpl(AuthenticationSchemes effectiveAuthenticationSchemes)
		{
			return this.requireClientCertificate || base.GetSupportsClientWindowsIdentityImpl(effectiveAuthenticationSchemes);
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06005423 RID: 21539 RVA: 0x00135F79 File Offset: 0x00134179
		// (set) Token: 0x06005424 RID: 21540 RVA: 0x00135F81 File Offset: 0x00134181
		internal MessageSecurityVersion MessageSecurityVersion
		{
			get
			{
				return this.messageSecurityVersion;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.messageSecurityVersion = value;
			}
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x00135FA4 File Offset: 0x001341A4
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (base.MessageHandlerFactory != null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("HttpPipelineNotSupportedOnClientSide", new object[]
				{
					"MessageHandlerFactory"
				})));
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			return (IChannelFactory<TChannel>)new HttpsChannelFactory<TChannel>(this, context);
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x00136038 File Offset: 0x00134238
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			base.UpdateAuthenticationSchemes(context);
			HttpChannelListener httpChannelListener = new HttpsChannelListener<TChannel>(this, context);
			AspNetEnvironment.Current.ApplyHostedContext(httpChannelListener, context);
			return (IChannelListener<TChannel>)httpChannelListener;
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x001360AF File Offset: 0x001342AF
		internal static HttpsTransportBindingElement CreateFromHttpBindingElement(HttpTransportBindingElement elementToBeCloned)
		{
			return new HttpsTransportBindingElement(elementToBeCloned);
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x001360B8 File Offset: 0x001342B8
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				AuthenticationSchemes effectiveAuthenticationSchemes = HttpTransportBindingElement.GetEffectiveAuthenticationSchemes(base.AuthenticationScheme, context.BindingParameters);
				return (T)((object)new SecurityCapabilities(this.GetSupportsClientAuthenticationImpl(effectiveAuthenticationSchemes), true, this.GetSupportsClientWindowsIdentityImpl(effectiveAuthenticationSchemes), ProtectionLevel.EncryptAndSign, ProtectionLevel.EncryptAndSign));
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x00136128 File Offset: 0x00134328
		internal override void OnExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			base.OnExportPolicy(exporter, context);
			SecurityBindingElement.ExportPolicyForTransportTokenAssertionProviders(exporter, context);
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0013613C File Offset: 0x0013433C
		internal override void OnImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			base.OnImportPolicy(importer, policyContext);
			WSSecurityPolicy wssecurityPolicy = null;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(policyContext.GetBindingAssertions(), out wssecurityPolicy))
			{
				wssecurityPolicy.TryImportWsspHttpsTokenAssertion(importer, policyContext.GetBindingAssertions(), this);
			}
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x00136171 File Offset: 0x00134371
		public XmlElement GetTransportTokenAssertion()
		{
			return null;
		}

		// Token: 0x04003300 RID: 13056
		private bool requireClientCertificate;

		// Token: 0x04003301 RID: 13057
		private MessageSecurityVersion messageSecurityVersion;
	}
}
