using System;
using System.Configuration;
using System.Net.Mail;

namespace System.Net.Configuration
{
	// Token: 0x02000664 RID: 1636
	public sealed class SmtpNetworkElement : ConfigurationElement
	{
		// Token: 0x060032A4 RID: 12964 RVA: 0x000D6FF8 File Offset: 0x000D5FF8
		public SmtpNetworkElement()
		{
			this.properties.Add(this.defaultCredentials);
			this.properties.Add(this.host);
			this.properties.Add(this.clientDomain);
			this.properties.Add(this.password);
			this.properties.Add(this.port);
			this.properties.Add(this.userName);
			this.properties.Add(this.targetName);
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000D7168 File Offset: 0x000D6168
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

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000D71FC File Offset: 0x000D61FC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x000D7204 File Offset: 0x000D6204
		// (set) Token: 0x060032A8 RID: 12968 RVA: 0x000D7217 File Offset: 0x000D6217
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

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000D722B File Offset: 0x000D622B
		// (set) Token: 0x060032AA RID: 12970 RVA: 0x000D723E File Offset: 0x000D623E
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

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x000D724D File Offset: 0x000D624D
		// (set) Token: 0x060032AC RID: 12972 RVA: 0x000D7260 File Offset: 0x000D6260
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

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x000D726F File Offset: 0x000D626F
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x000D7282 File Offset: 0x000D6282
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

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000D7291 File Offset: 0x000D6291
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x000D72A4 File Offset: 0x000D62A4
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

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x000D72B3 File Offset: 0x000D62B3
		// (set) Token: 0x060032B2 RID: 12978 RVA: 0x000D72C6 File Offset: 0x000D62C6
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

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000D72DA File Offset: 0x000D62DA
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x000D72ED File Offset: 0x000D62ED
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

		// Token: 0x04002F61 RID: 12129
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F62 RID: 12130
		private readonly ConfigurationProperty defaultCredentials = new ConfigurationProperty("defaultCredentials", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F63 RID: 12131
		private readonly ConfigurationProperty host = new ConfigurationProperty("host", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F64 RID: 12132
		private readonly ConfigurationProperty clientDomain = new ConfigurationProperty("clientDomain", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F65 RID: 12133
		private readonly ConfigurationProperty password = new ConfigurationProperty("password", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F66 RID: 12134
		private readonly ConfigurationProperty port = new ConfigurationProperty("port", typeof(int), 25, null, new IntegerValidator(1, 65535), ConfigurationPropertyOptions.None);

		// Token: 0x04002F67 RID: 12135
		private readonly ConfigurationProperty userName = new ConfigurationProperty("userName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F68 RID: 12136
		private readonly ConfigurationProperty targetName = new ConfigurationProperty("targetName", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
