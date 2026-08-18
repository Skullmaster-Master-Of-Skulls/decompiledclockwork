using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E4 RID: 1508
	public sealed class AuthorizationPolicyTypeElement : ConfigurationElement
	{
		// Token: 0x06003A60 RID: 14944 RVA: 0x000E0B3C File Offset: 0x000DED3C
		public AuthorizationPolicyTypeElement()
		{
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000E0B44 File Offset: 0x000DED44
		public AuthorizationPolicyTypeElement(string policyType)
		{
			if (string.IsNullOrEmpty(policyType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyType");
			}
			this.PolicyType = policyType;
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000E0B6B File Offset: 0x000DED6B
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000E0B7D File Offset: 0x000DED7D
		[ConfigurationProperty("policyType", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string PolicyType
		{
			get
			{
				return (string)base["policyType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["policyType"] = value;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000E0B9C File Offset: 0x000DED9C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("policyType", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A5A RID: 10842
		private ConfigurationPropertyCollection properties;
	}
}
