using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200069D RID: 1693
	public sealed class WsdlImporterElement : ConfigurationElement
	{
		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000F87DC File Offset: 0x000F69DC
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

		// Token: 0x06004187 RID: 16775 RVA: 0x000F882D File Offset: 0x000F6A2D
		public WsdlImporterElement()
		{
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000F8835 File Offset: 0x000F6A35
		public WsdlImporterElement(string type)
		{
			this.Type = type;
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000F8844 File Offset: 0x000F6A44
		public WsdlImporterElement(Type type)
		{
			SubclassTypeValidator subclassTypeValidator = new SubclassTypeValidator(typeof(IWsdlImportExtension));
			subclassTypeValidator.Validate(type);
			this.Type = type.AssemblyQualifiedName;
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x0600418A RID: 16778 RVA: 0x000F887A File Offset: 0x000F6A7A
		// (set) Token: 0x0600418B RID: 16779 RVA: 0x000F888C File Offset: 0x000F6A8C
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

		// Token: 0x04002CEB RID: 11499
		private ConfigurationPropertyCollection properties;
	}
}
