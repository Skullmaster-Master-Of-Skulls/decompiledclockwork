using System;
using System.Configuration;
using System.Net.Mail;

namespace System.Net.Configuration
{
	// Token: 0x02000344 RID: 836
	public sealed class SmtpNetworkElement : ConfigurationElement
	{
		// Token: 0x06001E0A RID: 7690 RVA: 0x0008D4C8 File Offset: 0x0008B6C8
		public SmtpNetworkElement()
		{
			this.properties.Add(this.defaultCredentials);
			this.properties.Add(this.host);
			this.properties.Add(this.clientDomain);
			this.properties.Add(this.password);
			this.properties.Add(this.port);
			this.properties.Add(this.userName);
			this.properties.Add(this.targetName);
			this.properties.Add(this.enableSsl);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0008D66C File Offset: 0x0008B86C
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			PropertyInformation propertyInformation = base.ElementInformation.Properties["port"];
			if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue)
			{
				try
				{
					new SmtpPermission(SmtpAccess.ConnectToUnrestrictedPort).Demand();
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("net_config_property_permission", new object[]
					{
						propertyInformation.Name
					}), inner);
				}
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0008D6FC File Offset: 0x0008B8FC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0008D704 File Offset: 0x0008B904
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x0008D717 File Offset: 0x0008B917
		[ConfigurationProperty("defaultCredentials", DefaultValue = false)]
		public bool DefaultCredentials
		{
			get
			{
				return (bool)base[this.defaultCredentials];
			}
			set
			{
				base[this.defaultCredentials] = value;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0008D72B File Offset: 0x0008B92B
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x0008D73E File Offset: 0x0008B93E
		[ConfigurationProperty("host")]
		public string Host
		{
			get
			{
				return (string)base[this.host];
			}
			set
			{
				base[this.host] = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0008D74D File Offset: 0x0008B94D
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0008D760 File Offset: 0x0008B960
		[ConfigurationProperty("targetName")]
		public string TargetName
		{
			get
			{
				return (string)base[this.targetName];
			}
			set
			{
				base[this.targetName] = value;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x0008D76F File Offset: 0x0008B96F
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x0008D782 File Offset: 0x0008B982
		[ConfigurationProperty("clientDomain")]
		public string ClientDomain
		{
			get
			{
				return (string)base[this.clientDomain];
			}
			set
			{
				base[this.clientDomain] = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x0008D791 File Offset: 0x0008B991
		// (set) Token: 0x06001E16 RID: 7702 RVA: 0x0008D7A4 File Offset: 0x0008B9A4
		[ConfigurationProperty("password")]
		public string Password
		{
			get
			{
				return (string)base[this.password];
			}
			set
			{
				base[this.password] = value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0008D7B3 File Offset: 0x0008B9B3
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x0008D7C6 File Offset: 0x0008B9C6
		[ConfigurationProperty("port", DefaultValue = 25)]
		public int Port
		{
			get
			{
				return (int)base[this.port];
			}
			set
			{
				base[this.port] = value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0008D7DA File Offset: 0x0008B9DA
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x0008D7ED File Offset: 0x0008B9ED
		[ConfigurationProperty("userName")]
		public string UserName
		{
			get
			{
				return (string)base[this.userName];
			}
			set
			{
				base[this.userName] = value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0008D7FC File Offset: 0x0008B9FC
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x0008D80F File Offset: 0x0008BA0F
		[ConfigurationProperty("enableSsl", DefaultValue = false)]
		public bool EnableSsl
		{
			get
			{
				return (bool)base[this.enableSsl];
			}
			set
			{
				base[this.enableSsl] = value;
			}
		}

		// Token: 0x04001CA4 RID: 7332
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CA5 RID: 7333
		private readonly ConfigurationProperty defaultCredentials = new ConfigurationProperty("defaultCredentials", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001CA6 RID: 7334
		private readonly ConfigurationProperty host = new ConfigurationProperty("host", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CA7 RID: 7335
		private readonly ConfigurationProperty clientDomain = new ConfigurationProperty("clientDomain", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CA8 RID: 7336
		private readonly ConfigurationProperty password = new ConfigurationProperty("password", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CA9 RID: 7337
		private readonly ConfigurationProperty port = new ConfigurationProperty("port", typeof(int), 25, null, new IntegerValidator(1, 65535), ConfigurationPropertyOptions.None);

		// Token: 0x04001CAA RID: 7338
		private readonly ConfigurationProperty userName = new ConfigurationProperty("userName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CAB RID: 7339
		private readonly ConfigurationProperty targetName = new ConfigurationProperty("targetName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CAC RID: 7340
		private readonly ConfigurationProperty enableSsl = new ConfigurationProperty("enableSsl", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
