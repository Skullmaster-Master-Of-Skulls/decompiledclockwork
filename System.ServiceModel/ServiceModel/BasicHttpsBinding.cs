using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x0200012A RID: 298
	public class BasicHttpsBinding : HttpBindingBase
	{
		// Token: 0x0600082C RID: 2092 RVA: 0x00021ADD File Offset: 0x0001FCDD
		public BasicHttpsBinding() : this(BasicHttpsSecurityMode.Transport)
		{
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00021AE6 File Offset: 0x0001FCE6
		public BasicHttpsBinding(string configurationName) : this()
		{
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00021AF5 File Offset: 0x0001FCF5
		public BasicHttpsBinding(BasicHttpsSecurityMode securityMode)
		{
			this.basicHttpsSecurity = new BasicHttpsSecurity();
			this.basicHttpsSecurity.Mode = securityMode;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00021B14 File Offset: 0x0001FD14
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00021B1C File Offset: 0x0001FD1C
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00021B25 File Offset: 0x0001FD25
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00021B2D File Offset: 0x0001FD2D
		public BasicHttpsSecurity Security
		{
			get
			{
				return this.basicHttpsSecurity;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.basicHttpsSecurity = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00021B49 File Offset: 0x0001FD49
		internal override BasicHttpSecurity BasicHttpSecurity
		{
			get
			{
				return this.basicHttpsSecurity.BasicHttpSecurity;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00021B56 File Offset: 0x0001FD56
		internal override EnvelopeVersion GetEnvelopeVersion()
		{
			return EnvelopeVersion.Soap11;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00021B60 File Offset: 0x0001FD60
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

		// Token: 0x06000836 RID: 2102 RVA: 0x00021BDC File Offset: 0x0001FDDC
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

		// Token: 0x06000837 RID: 2103 RVA: 0x00021C59 File Offset: 0x0001FE59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00021C68 File Offset: 0x0001FE68
		private void ApplyConfiguration(string configurationName)
		{
			BasicHttpsBindingCollectionElement bindingCollectionElement = BasicHttpsBindingCollectionElement.GetBindingCollectionElement();
			BasicHttpsBindingElement basicHttpsBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (basicHttpsBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"basicHttpsBinding"
				})));
			}
			basicHttpsBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x04000B04 RID: 2820
		private WSMessageEncoding messageEncoding;

		// Token: 0x04000B05 RID: 2821
		private BasicHttpsSecurity basicHttpsSecurity;
	}
}
