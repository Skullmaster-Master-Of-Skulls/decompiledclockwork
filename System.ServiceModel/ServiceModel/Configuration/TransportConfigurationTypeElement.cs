using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000697 RID: 1687
	public sealed class TransportConfigurationTypeElement : ConfigurationElement
	{
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x000F7DD0 File Offset: 0x000F5FD0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("transportConfigurationType", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000F7E4A File Offset: 0x000F604A
		public TransportConfigurationTypeElement()
		{
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x000F7E52 File Offset: 0x000F6052
		public TransportConfigurationTypeElement(string name) : this()
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			this.Name = name;
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x000F7E79 File Offset: 0x000F6079
		public TransportConfigurationTypeElement(string name, string transportConfigurationTypeName) : this(name)
		{
			if (string.IsNullOrEmpty(transportConfigurationTypeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("transportConfigurationTypeName");
			}
			this.TransportConfigurationType = transportConfigurationTypeName;
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x000F7EA1 File Offset: 0x000F60A1
		// (set) Token: 0x06004154 RID: 16724 RVA: 0x000F7EB3 File Offset: 0x000F60B3
		[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["name"] = value;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x000F7ED0 File Offset: 0x000F60D0
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x000F7EE2 File Offset: 0x000F60E2
		[ConfigurationProperty("transportConfigurationType", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string TransportConfigurationType
		{
			get
			{
				return (string)base["transportConfigurationType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["transportConfigurationType"] = value;
			}
		}

		// Token: 0x04002CE5 RID: 11493
		private ConfigurationPropertyCollection properties;
	}
}
