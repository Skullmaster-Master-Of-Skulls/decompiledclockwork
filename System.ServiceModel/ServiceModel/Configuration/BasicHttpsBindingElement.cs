using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F0 RID: 1520
	public class BasicHttpsBindingElement : HttpBindingBaseElement
	{
		// Token: 0x06003A94 RID: 14996 RVA: 0x000E12B1 File Offset: 0x000DF4B1
		public BasicHttpsBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000E12BA File Offset: 0x000DF4BA
		public BasicHttpsBindingElement() : this(null)
		{
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000E12C3 File Offset: 0x000DF4C3
		// (set) Token: 0x06003A97 RID: 14999 RVA: 0x000E12D5 File Offset: 0x000DF4D5
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

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000E12E8 File Offset: 0x000DF4E8
		[ConfigurationProperty("security")]
		public BasicHttpsSecurityElement Security
		{
			get
			{
				return (BasicHttpsSecurityElement)base["security"];
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x000E12FA File Offset: 0x000DF4FA
		protected override Type BindingElementType
		{
			get
			{
				return typeof(BasicHttpsBinding);
			}
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000E1308 File Offset: 0x000DF508
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			BasicHttpsBinding basicHttpsBinding = (BasicHttpsBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<WSMessageEncoding>("messageEncoding", basicHttpsBinding.MessageEncoding);
			this.Security.InitializeFrom(basicHttpsBinding.Security);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000E1348 File Offset: 0x000DF548
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			BasicHttpsBinding basicHttpsBinding = (BasicHttpsBinding)binding;
			basicHttpsBinding.MessageEncoding = this.MessageEncoding;
			this.Security.ApplyConfiguration(basicHttpsBinding.Security);
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000E1380 File Offset: 0x000DF580
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
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(BasicHttpsSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002A73 RID: 10867
		private ConfigurationPropertyCollection properties;
	}
}
