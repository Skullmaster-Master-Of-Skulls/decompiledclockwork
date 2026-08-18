using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000609 RID: 1545
	public sealed class CompositeDuplexElement : BindingElementExtensionElement
	{
		// Token: 0x06003B74 RID: 15220 RVA: 0x000E3A3C File Offset: 0x000E1C3C
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			CompositeDuplexBindingElement compositeDuplexBindingElement = (CompositeDuplexBindingElement)bindingElement;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["clientBaseAddress"].ValueOrigin != PropertyValueOrigin.Default)
			{
				compositeDuplexBindingElement.ClientBaseAddress = this.ClientBaseAddress;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000E3A81 File Offset: 0x000E1C81
		public override Type BindingElementType
		{
			get
			{
				return typeof(CompositeDuplexBindingElement);
			}
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000E3A90 File Offset: 0x000E1C90
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			CompositeDuplexElement compositeDuplexElement = (CompositeDuplexElement)from;
			this.ClientBaseAddress = compositeDuplexElement.ClientBaseAddress;
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000E3AB8 File Offset: 0x000E1CB8
		protected internal override BindingElement CreateBindingElement()
		{
			CompositeDuplexBindingElement compositeDuplexBindingElement = new CompositeDuplexBindingElement();
			this.ApplyConfiguration(compositeDuplexBindingElement);
			return compositeDuplexBindingElement;
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000E3AD3 File Offset: 0x000E1CD3
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x000E3AE5 File Offset: 0x000E1CE5
		[ConfigurationProperty("clientBaseAddress", DefaultValue = null)]
		public Uri ClientBaseAddress
		{
			get
			{
				return (Uri)base["clientBaseAddress"];
			}
			set
			{
				base["clientBaseAddress"] = value;
			}
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000E3AF4 File Offset: 0x000E1CF4
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			CompositeDuplexBindingElement compositeDuplexBindingElement = (CompositeDuplexBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<Uri>("clientBaseAddress", compositeDuplexBindingElement.ClientBaseAddress);
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000E3B20 File Offset: 0x000E1D20
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientBaseAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A86 RID: 10886
		private ConfigurationPropertyCollection properties;
	}
}
