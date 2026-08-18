using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.MsmqIntegration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000643 RID: 1603
	public class MsmqIntegrationBindingElement : MsmqBindingElementBase
	{
		// Token: 0x06003DBA RID: 15802 RVA: 0x000EBB8D File Offset: 0x000E9D8D
		public MsmqIntegrationBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000EBB96 File Offset: 0x000E9D96
		public MsmqIntegrationBindingElement() : this(null)
		{
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x000EBB9F File Offset: 0x000E9D9F
		protected override Type BindingElementType
		{
			get
			{
				return typeof(MsmqIntegrationBinding);
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000EBBAB File Offset: 0x000E9DAB
		[ConfigurationProperty("security")]
		public MsmqIntegrationSecurityElement Security
		{
			get
			{
				return (MsmqIntegrationSecurityElement)base["security"];
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000EBBBD File Offset: 0x000E9DBD
		// (set) Token: 0x06003DBF RID: 15807 RVA: 0x000EBBCF File Offset: 0x000E9DCF
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

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000EBBE4 File Offset: 0x000E9DE4
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			MsmqIntegrationBinding msmqIntegrationBinding = (MsmqIntegrationBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<MsmqMessageSerializationFormat>("serializationFormat", msmqIntegrationBinding.SerializationFormat);
			this.Security.InitializeFrom(msmqIntegrationBinding.Security);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000EBC24 File Offset: 0x000E9E24
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			MsmqIntegrationBinding msmqIntegrationBinding = (MsmqIntegrationBinding)binding;
			msmqIntegrationBinding.SerializationFormat = this.SerializationFormat;
			this.Security.ApplyConfiguration(msmqIntegrationBinding.Security);
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x000EBC5C File Offset: 0x000E9E5C
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
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(MsmqIntegrationSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("serializationFormat", typeof(MsmqMessageSerializationFormat), MsmqMessageSerializationFormat.Xml, null, new ServiceModelEnumValidator(typeof(MsmqMessageSerializationFormatHelper)), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C95 RID: 11413
		private ConfigurationPropertyCollection properties;
	}
}
