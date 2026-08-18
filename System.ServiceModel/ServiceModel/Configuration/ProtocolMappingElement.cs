using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200068B RID: 1675
	public sealed class ProtocolMappingElement : ConfigurationElement
	{
		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000F596C File Offset: 0x000F3B6C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("scheme", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("binding", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired),
						new ConfigurationProperty("bindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x000F5A16 File Offset: 0x000F3C16
		public ProtocolMappingElement()
		{
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x000F5A20 File Offset: 0x000F3C20
		public ProtocolMappingElement(string schemeType, string binding, string bindingConfiguration)
		{
			if (string.IsNullOrEmpty(schemeType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("schemeType");
			}
			this.Scheme = schemeType;
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			this.Binding = binding;
			this.BindingConfiguration = bindingConfiguration;
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x000F5A73 File Offset: 0x000F3C73
		// (set) Token: 0x060040BC RID: 16572 RVA: 0x000F5A85 File Offset: 0x000F3C85
		[ConfigurationProperty("scheme", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Scheme
		{
			get
			{
				return (string)base["scheme"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["scheme"] = value;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x000F5AA2 File Offset: 0x000F3CA2
		// (set) Token: 0x060040BE RID: 16574 RVA: 0x000F5AB4 File Offset: 0x000F3CB4
		[ConfigurationProperty("binding", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string Binding
		{
			get
			{
				return (string)base["binding"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["binding"] = value;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x000F5AD1 File Offset: 0x000F3CD1
		// (set) Token: 0x060040C0 RID: 16576 RVA: 0x000F5AE3 File Offset: 0x000F3CE3
		[ConfigurationProperty("bindingConfiguration", Options = ConfigurationPropertyOptions.None)]
		[StringValidator(MinLength = 0)]
		public string BindingConfiguration
		{
			get
			{
				return (string)base["bindingConfiguration"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["bindingConfiguration"] = value;
			}
		}

		// Token: 0x04002CD8 RID: 11480
		private ConfigurationPropertyCollection properties;
	}
}
