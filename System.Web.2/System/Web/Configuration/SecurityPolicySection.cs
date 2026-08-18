using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200074F RID: 1871
	public sealed class SecurityPolicySection : ConfigurationSection
	{
		// Token: 0x06005A38 RID: 23096 RVA: 0x0013A7CC File Offset: 0x001389CC
		static SecurityPolicySection()
		{
			SecurityPolicySection._properties = new ConfigurationPropertyCollection();
			SecurityPolicySection._properties.Add(SecurityPolicySection._propTrustLevels);
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06005A3A RID: 23098 RVA: 0x0013A7FE File Offset: 0x001389FE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SecurityPolicySection._properties;
			}
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x06005A3B RID: 23099 RVA: 0x0013A805 File Offset: 0x00138A05
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public TrustLevelCollection TrustLevels
		{
			get
			{
				return (TrustLevelCollection)base[SecurityPolicySection._propTrustLevels];
			}
		}

		// Token: 0x04002FC7 RID: 12231
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002FC8 RID: 12232
		private static readonly ConfigurationProperty _propTrustLevels = new ConfigurationProperty(null, typeof(TrustLevelCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
