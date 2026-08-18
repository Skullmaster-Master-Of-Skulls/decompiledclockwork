using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x02000129 RID: 297
	[__DynamicallyInvokable]
	public class BasicHttpBinding : HttpBindingBase
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x0002173C File Offset: 0x0001F93C
		[__DynamicallyInvokable]
		public BasicHttpBinding() : this(BasicHttpSecurityMode.None)
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00021745 File Offset: 0x0001F945
		public BasicHttpBinding(string configurationName)
		{
			this.Initialize();
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002175A File Offset: 0x0001F95A
		[__DynamicallyInvokable]
		public BasicHttpBinding(BasicHttpSecurityMode securityMode)
		{
			this.Initialize();
			this.basicHttpSecurity.Mode = securityMode;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00021774 File Offset: 0x0001F974
		private BasicHttpBinding(BasicHttpSecurity security)
		{
			this.Initialize();
			this.basicHttpSecurity = security;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00021789 File Offset: 0x0001F989
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00021791 File Offset: 0x0001F991
		[DefaultValue(WSMessageEncoding.Text)]
		public WSMessageEncoding MessageEncoding
		{
			get
			{
				return this.messageEncoding;
			}
			set
			{
				this.messageEncoding = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002179A File Offset: 0x0001F99A
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x000217A2 File Offset: 0x0001F9A2
		[__DynamicallyInvokable]
		public BasicHttpSecurity Security
		{
			[__DynamicallyInvokable]
			get
			{
				return this.basicHttpSecurity;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.basicHttpSecurity = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x000217BE File Offset: 0x0001F9BE
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x000217C6 File Offset: 0x0001F9C6
		[Obsolete("This property is obsolete. To enable Http CookieContainer, use the AllowCookies property instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool EnableHttpCookieContainer
		{
			get
			{
				return base.AllowCookies;
			}
			set
			{
				base.AllowCookies = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x000217CF File Offset: 0x0001F9CF
		internal override BasicHttpSecurity BasicHttpSecurity
		{
			get
			{
				return this.basicHttpSecurity;
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000217D8 File Offset: 0x0001F9D8
		private bool IsBindingElementsMatch(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding)
		{
			if (this.MessageEncoding == WSMessageEncoding.Text)
			{
				if (!base.TextMessageEncodingBindingElement.IsMatch(encoding))
				{
					return false;
				}
			}
			else if (this.MessageEncoding == WSMessageEncoding.Mtom && !base.MtomMessageEncodingBindingElement.IsMatch(encoding))
			{
				return false;
			}
			return base.GetTransport().IsMatch(transport);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00021827 File Offset: 0x0001FA27
		internal override EnvelopeVersion GetEnvelopeVersion()
		{
			return EnvelopeVersion.Soap11;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002182E File Offset: 0x0001FA2E
		internal override void InitializeFrom(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding)
		{
			base.InitializeFrom(transport, encoding);
			if (encoding is TextMessageEncodingBindingElement)
			{
				this.MessageEncoding = WSMessageEncoding.Text;
				return;
			}
			if (encoding is MtomMessageEncodingBindingElement)
			{
				this.messageEncoding = WSMessageEncoding.Mtom;
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00021858 File Offset: 0x0001FA58
		private void ApplyConfiguration(string configurationName)
		{
			BasicHttpBindingCollectionElement bindingCollectionElement = BasicHttpBindingCollectionElement.GetBindingCollectionElement();
			BasicHttpBindingElement basicHttpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (basicHttpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"basicHttpBinding"
				})));
			}
			basicHttpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000218B0 File Offset: 0x0001FAB0
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingParameterCollection parameters)
		{
			if ((this.BasicHttpSecurity.Mode == BasicHttpSecurityMode.Transport || this.BasicHttpSecurity.Mode == BasicHttpSecurityMode.TransportCredentialOnly) && this.BasicHttpSecurity.Transport.ClientCredentialType == HttpClientCredentialType.InheritedFromHost)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpClientCredentialTypeInvalid", new object[]
				{
					this.BasicHttpSecurity.Transport.ClientCredentialType
				})));
			}
			return base.BuildChannelFactory<TChannel>(parameters);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002192C File Offset: 0x0001FB2C
		[__DynamicallyInvokable]
		public override BindingElementCollection CreateBindingElements()
		{
			this.CheckSettings();
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			SecurityBindingElement securityBindingElement = this.BasicHttpSecurity.CreateMessageSecurity();
			if (securityBindingElement != null)
			{
				bindingElementCollection.Add(securityBindingElement);
			}
			WSMessageEncodingHelper.SyncUpEncodingBindingElementProperties(base.TextMessageEncodingBindingElement, base.MtomMessageEncodingBindingElement);
			if (this.MessageEncoding == WSMessageEncoding.Text)
			{
				bindingElementCollection.Add(base.TextMessageEncodingBindingElement);
			}
			else if (this.MessageEncoding == WSMessageEncoding.Mtom)
			{
				bindingElementCollection.Add(base.MtomMessageEncodingBindingElement);
			}
			bindingElementCollection.Add(base.GetTransport());
			return bindingElementCollection.Clone();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000219AC File Offset: 0x0001FBAC
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count > 3)
			{
				return false;
			}
			SecurityBindingElement securityBindingElement = null;
			MessageEncodingBindingElement messageEncodingBindingElement = null;
			HttpTransportBindingElement httpTransportBindingElement = null;
			foreach (BindingElement bindingElement in elements)
			{
				if (bindingElement is SecurityBindingElement)
				{
					securityBindingElement = (bindingElement as SecurityBindingElement);
				}
				else if (bindingElement is TransportBindingElement)
				{
					httpTransportBindingElement = (bindingElement as HttpTransportBindingElement);
				}
				else
				{
					if (!(bindingElement is MessageEncodingBindingElement))
					{
						return false;
					}
					messageEncodingBindingElement = (bindingElement as MessageEncodingBindingElement);
				}
			}
			HttpsTransportBindingElement httpsTransportBindingElement = httpTransportBindingElement as HttpsTransportBindingElement;
			if (securityBindingElement != null && httpsTransportBindingElement != null && httpsTransportBindingElement.RequireClientCertificate)
			{
				return false;
			}
			HttpTransportSecurity transportSecurity = new HttpTransportSecurity();
			UnifiedSecurityMode mode;
			if (!HttpBindingBase.GetSecurityModeFromTransport(httpTransportBindingElement, transportSecurity, out mode))
			{
				return false;
			}
			if (messageEncodingBindingElement == null)
			{
				return false;
			}
			if (!messageEncodingBindingElement.CheckEncodingVersion(EnvelopeVersion.Soap11))
			{
				return false;
			}
			BasicHttpSecurity security;
			if (!HttpBindingBase.TryCreateSecurity(securityBindingElement, mode, transportSecurity, out security))
			{
				return false;
			}
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding(security);
			basicHttpBinding.InitializeFrom(httpTransportBindingElement, messageEncodingBindingElement);
			if (!basicHttpBinding.IsBindingElementsMatch(httpTransportBindingElement, messageEncodingBindingElement))
			{
				return false;
			}
			binding = basicHttpBinding;
			return true;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00021ACD File Offset: 0x0001FCCD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeEnableHttpCookieContainer()
		{
			return false;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		private void Initialize()
		{
			this.basicHttpSecurity = new BasicHttpSecurity();
		}

		// Token: 0x04000B02 RID: 2818
		private WSMessageEncoding messageEncoding;

		// Token: 0x04000B03 RID: 2819
		private BasicHttpSecurity basicHttpSecurity;
	}
}
