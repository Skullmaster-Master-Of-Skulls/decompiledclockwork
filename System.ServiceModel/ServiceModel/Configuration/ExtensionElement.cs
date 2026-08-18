using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000688 RID: 1672
	public class ExtensionElement : ConfigurationElement
	{
		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x000F5460 File Offset: 0x000F3660
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("type", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x000F54DA File Offset: 0x000F36DA
		public ExtensionElement()
		{
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x000F54E2 File Offset: 0x000F36E2
		public ExtensionElement(string name) : this()
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			this.Name = name;
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000F5509 File Offset: 0x000F3709
		public ExtensionElement(string name, string type) : this(name)
		{
			if (string.IsNullOrEmpty(type))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("type");
			}
			this.Type = type;
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x000F5531 File Offset: 0x000F3731
		// (set) Token: 0x0600409D RID: 16541 RVA: 0x000F5543 File Offset: 0x000F3743
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

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x000F5560 File Offset: 0x000F3760
		// (set) Token: 0x0600409F RID: 16543 RVA: 0x000F5572 File Offset: 0x000F3772
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
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

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x000F558F File Offset: 0x000F378F
		internal string TypeName
		{
			get
			{
				if (string.IsNullOrEmpty(this.typeName))
				{
					this.typeName = ExtensionElement.GetTypeName(this.Type);
				}
				return this.typeName;
			}
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000F55B8 File Offset: 0x000F37B8
		internal static string GetTypeName(string fullyQualifiedName)
		{
			string text = fullyQualifiedName.Split(new char[]
			{
				','
			})[0];
			return text.Trim();
		}

		// Token: 0x04002CD4 RID: 11476
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CD5 RID: 11477
		private string typeName;
	}
}
