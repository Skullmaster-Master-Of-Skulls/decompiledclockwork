using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200062D RID: 1581
	public class HttpsTransportElement : HttpTransportElement
	{
		// Token: 0x06003C83 RID: 15491 RVA: 0x000E6E20 File Offset: 0x000E5020
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			HttpsTransportBindingElement httpsTransportBindingElement = (HttpsTransportBindingElement)bindingElement;
			httpsTransportBindingElement.RequireClientCertificate = this.RequireClientCertificate;
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000E6E47 File Offset: 0x000E5047
		public override Type BindingElementType
		{
			get
			{
				return typeof(HttpsTransportBindingElement);
			}
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000E6E54 File Offset: 0x000E5054
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			HttpsTransportElement httpsTransportElement = (HttpsTransportElement)from;
			this.RequireClientCertificate = httpsTransportElement.RequireClientCertificate;
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x000E6E7B File Offset: 0x000E507B
		protected override TransportBindingElement CreateDefaultBindingElement()
		{
			return new HttpsTransportBindingElement();
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x000E6E84 File Offset: 0x000E5084
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			HttpsTransportBindingElement httpsTransportBindingElement = (HttpsTransportBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<bool>("requireClientCertificate", httpsTransportBindingElement.RequireClientCertificate);
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000E6EB0 File Offset: 0x000E50B0
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x000E6EC2 File Offset: 0x000E50C2
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

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x000E6ED8 File Offset: 0x000E50D8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("requireClientCertificate", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C83 RID: 11395
		private ConfigurationPropertyCollection properties;
	}
}
