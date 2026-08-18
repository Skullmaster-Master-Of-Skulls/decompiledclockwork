using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000672 RID: 1650
	public sealed class PolicyImporterElement : ConfigurationElement
	{
		// Token: 0x06003F52 RID: 16210 RVA: 0x000F0564 File Offset: 0x000EE764
		public PolicyImporterElement()
		{
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000F056C File Offset: 0x000EE76C
		public PolicyImporterElement(string type)
		{
			this.Type = type;
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000F057C File Offset: 0x000EE77C
		public PolicyImporterElement(Type type)
		{
			SubclassTypeValidator subclassTypeValidator = new SubclassTypeValidator(typeof(IPolicyImportExtension));
			subclassTypeValidator.Validate(type);
			this.Type = type.AssemblyQualifiedName;
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x000F05B2 File Offset: 0x000EE7B2
		// (set) Token: 0x06003F56 RID: 16214 RVA: 0x000F05C4 File Offset: 0x000EE7C4
		[ConfigurationProperty("type", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["type"] = value;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x000F05E4 File Offset: 0x000EE7E4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("type", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CB5 RID: 11445
		private ConfigurationPropertyCollection properties;
	}
}
