using System;
using System.Configuration;
using System.Net.Security;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200069C RID: 1692
	public sealed class WindowsStreamSecurityElement : BindingElementExtensionElement
	{
		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x000F86B0 File Offset: 0x000F68B0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("protectionLevel", typeof(ProtectionLevel), ProtectionLevel.EncryptAndSign, null, new StandardRuntimeEnumValidator(typeof(ProtectionLevel)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x000F8711 File Offset: 0x000F6911
		// (set) Token: 0x06004180 RID: 16768 RVA: 0x000F8723 File Offset: 0x000F6923
		[ConfigurationProperty("protectionLevel", DefaultValue = ProtectionLevel.EncryptAndSign)]
		[StandardRuntimeEnumValidator(typeof(ProtectionLevel))]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return (ProtectionLevel)base["protectionLevel"];
			}
			set
			{
				base["protectionLevel"] = value;
			}
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000F8738 File Offset: 0x000F6938
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = (WindowsStreamSecurityBindingElement)bindingElement;
			windowsStreamSecurityBindingElement.ProtectionLevel = this.ProtectionLevel;
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x000F8760 File Offset: 0x000F6960
		protected internal override BindingElement CreateBindingElement()
		{
			WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = new WindowsStreamSecurityBindingElement();
			this.ApplyConfiguration(windowsStreamSecurityBindingElement);
			return windowsStreamSecurityBindingElement;
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x000F877B File Offset: 0x000F697B
		public override Type BindingElementType
		{
			get
			{
				return typeof(WindowsStreamSecurityBindingElement);
			}
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000F8788 File Offset: 0x000F6988
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			WindowsStreamSecurityElement windowsStreamSecurityElement = (WindowsStreamSecurityElement)from;
			this.ProtectionLevel = windowsStreamSecurityElement.ProtectionLevel;
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000F87B0 File Offset: 0x000F69B0
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = (WindowsStreamSecurityBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<ProtectionLevel>("protectionLevel", windowsStreamSecurityBindingElement.ProtectionLevel);
		}

		// Token: 0x04002CEA RID: 11498
		private ConfigurationPropertyCollection properties;
	}
}
