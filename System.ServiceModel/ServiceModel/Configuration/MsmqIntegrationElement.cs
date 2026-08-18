using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.MsmqIntegration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000644 RID: 1604
	public sealed class MsmqIntegrationElement : MsmqElementBase
	{
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000EBD0C File Offset: 0x000E9F0C
		public override Type BindingElementType
		{
			get
			{
				return typeof(MsmqIntegrationBindingElement);
			}
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x000EBD18 File Offset: 0x000E9F18
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			MsmqIntegrationElement msmqIntegrationElement = from as MsmqIntegrationElement;
			if (msmqIntegrationElement != null)
			{
				this.SerializationFormat = msmqIntegrationElement.SerializationFormat;
			}
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000EBD42 File Offset: 0x000E9F42
		protected override TransportBindingElement CreateDefaultBindingElement()
		{
			return new MsmqIntegrationBindingElement();
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000EBD4C File Offset: 0x000E9F4C
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			MsmqIntegrationBindingElement msmqIntegrationBindingElement = bindingElement as MsmqIntegrationBindingElement;
			msmqIntegrationBindingElement.SerializationFormat = this.SerializationFormat;
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x000EBD74 File Offset: 0x000E9F74
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			MsmqIntegrationBindingElement msmqIntegrationBindingElement = bindingElement as MsmqIntegrationBindingElement;
			base.SetPropertyValueIfNotDefaultValue<MsmqMessageSerializationFormat>("serializationFormat", msmqIntegrationBindingElement.SerializationFormat);
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000EBDA0 File Offset: 0x000E9FA0
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x000EBDB2 File Offset: 0x000E9FB2
		[ConfigurationProperty("serializationFormat", DefaultValue = MsmqMessageSerializationFormat.Xml)]
		[ServiceModelEnumValidator(typeof(MsmqMessageSerializationFormatHelper))]
		public MsmqMessageSerializationFormat SerializationFormat
		{
			get
			{
				return (MsmqMessageSerializationFormat)base["serializationFormat"];
			}
			set
			{
				base["serializationFormat"] = value;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000EBDC8 File Offset: 0x000E9FC8
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
							configurationPropertyCollection.Add(new ConfigurationProperty("serializationFormat", typeof(MsmqMessageSerializationFormat), MsmqMessageSerializationFormat.Xml, null, new ServiceModelEnumValidator(typeof(MsmqMessageSerializationFormatHelper)), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C96 RID: 11414
		private ConfigurationPropertyCollection properties;
	}
}
