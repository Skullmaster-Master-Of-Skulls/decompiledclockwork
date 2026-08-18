using System;
using System.Configuration;
using System.Security.Authentication;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000692 RID: 1682
	public sealed class SslStreamSecurityElement : BindingElementExtensionElement
	{
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x000F7318 File Offset: 0x000F5518
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("requireClientCertificate", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sslProtocols", typeof(SslProtocols), SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, null, new ServiceModelEnumValidator(typeof(SslProtocolsHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x000F73A0 File Offset: 0x000F55A0
		// (set) Token: 0x06004113 RID: 16659 RVA: 0x000F73B2 File Offset: 0x000F55B2
		[ConfigurationProperty("requireClientCertificate", DefaultValue = false)]
		public bool RequireClientCertificate
		{
			get
			{
				return (bool)base["requireClientCertificate"];
			}
			set
			{
				base["requireClientCertificate"] = value;
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x000F73C5 File Offset: 0x000F55C5
		// (set) Token: 0x06004115 RID: 16661 RVA: 0x000F73D7 File Offset: 0x000F55D7
		[ConfigurationProperty("sslProtocols", DefaultValue = (SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12))]
		[ServiceModelEnumValidator(typeof(SslProtocolsHelper))]
		public SslProtocols SslProtocols
		{
			get
			{
				return (SslProtocols)base["sslProtocols"];
			}
			private set
			{
				base["sslProtocols"] = value;
			}
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000F73EC File Offset: 0x000F55EC
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			SslStreamSecurityBindingElement sslStreamSecurityBindingElement = (SslStreamSecurityBindingElement)bindingElement;
			sslStreamSecurityBindingElement.RequireClientCertificate = this.RequireClientCertificate;
			sslStreamSecurityBindingElement.SslProtocols = this.SslProtocols;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000F7420 File Offset: 0x000F5620
		protected internal override BindingElement CreateBindingElement()
		{
			SslStreamSecurityBindingElement sslStreamSecurityBindingElement = new SslStreamSecurityBindingElement();
			this.ApplyConfiguration(sslStreamSecurityBindingElement);
			return sslStreamSecurityBindingElement;
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x000F743B File Offset: 0x000F563B
		public override Type BindingElementType
		{
			get
			{
				return typeof(SslStreamSecurityBindingElement);
			}
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000F7448 File Offset: 0x000F5648
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			SslStreamSecurityElement sslStreamSecurityElement = (SslStreamSecurityElement)from;
			this.RequireClientCertificate = sslStreamSecurityElement.RequireClientCertificate;
			this.SslProtocols = sslStreamSecurityElement.SslProtocols;
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000F747C File Offset: 0x000F567C
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			SslStreamSecurityBindingElement sslStreamSecurityBindingElement = (SslStreamSecurityBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<bool>("requireClientCertificate", sslStreamSecurityBindingElement.RequireClientCertificate);
			base.SetPropertyValueIfNotDefaultValue<SslProtocols>("sslProtocols", sslStreamSecurityBindingElement.SslProtocols);
		}

		// Token: 0x04002CE0 RID: 11488
		private ConfigurationPropertyCollection properties;
	}
}
