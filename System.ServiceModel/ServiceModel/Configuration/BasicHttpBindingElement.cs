using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005EC RID: 1516
	public class BasicHttpBindingElement : HttpBindingBaseElement
	{
		// Token: 0x06003A79 RID: 14969 RVA: 0x000E0E34 File Offset: 0x000DF034
		public BasicHttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000E0E3D File Offset: 0x000DF03D
		public BasicHttpBindingElement() : this(null)
		{
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000E0E46 File Offset: 0x000DF046
		protected override Type BindingElementType
		{
			get
			{
				return typeof(BasicHttpBinding);
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000E0E52 File Offset: 0x000DF052
		// (set) Token: 0x06003A7D RID: 14973 RVA: 0x000E0E64 File Offset: 0x000DF064
		[ConfigurationProperty("messageEncoding", DefaultValue = WSMessageEncoding.Text)]
		[ServiceModelEnumValidator(typeof(WSMessageEncodingHelper))]
		public WSMessageEncoding MessageEncoding
		{
			get
			{
				return (WSMessageEncoding)base["messageEncoding"];
			}
			set
			{
				base["messageEncoding"] = value;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000E0E77 File Offset: 0x000DF077
		[ConfigurationProperty("security")]
		public BasicHttpSecurityElement Security
		{
			get
			{
				return (BasicHttpSecurityElement)base["security"];
			}
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000E0E8C File Offset: 0x000DF08C
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			BasicHttpBinding basicHttpBinding = (BasicHttpBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<WSMessageEncoding>("messageEncoding", basicHttpBinding.MessageEncoding);
			this.Security.InitializeFrom(basicHttpBinding.Security);
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000E0ECC File Offset: 0x000DF0CC
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			BasicHttpBinding basicHttpBinding = (BasicHttpBinding)binding;
			basicHttpBinding.MessageEncoding = this.MessageEncoding;
			this.Security.ApplyConfiguration(basicHttpBinding.Security);
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000E0F04 File Offset: 0x000DF104
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
							configurationPropertyCollection.Add(new ConfigurationProperty("messageEncoding", typeof(WSMessageEncoding), WSMessageEncoding.Text, null, new ServiceModelEnumValidator(typeof(WSMessageEncodingHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(BasicHttpSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002A70 RID: 10864
		private ConfigurationPropertyCollection properties;
	}
}
