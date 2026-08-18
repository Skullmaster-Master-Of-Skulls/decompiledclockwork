using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006D3 RID: 1747
	public sealed class DeploymentSection : ConfigurationSection
	{
		// Token: 0x06005411 RID: 21521 RVA: 0x00127013 File Offset: 0x00125213
		static DeploymentSection()
		{
			DeploymentSection._properties = new ConfigurationPropertyCollection();
			DeploymentSection._properties.Add(DeploymentSection._propRetail);
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x0012704E File Offset: 0x0012524E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DeploymentSection._properties;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06005414 RID: 21524 RVA: 0x00127055 File Offset: 0x00125255
		// (set) Token: 0x06005415 RID: 21525 RVA: 0x00127067 File Offset: 0x00125267
		[ConfigurationProperty("retail", DefaultValue = false)]
		public bool Retail
		{
			get
			{
				return (bool)base[DeploymentSection._propRetail];
			}
			set
			{
				base[DeploymentSection._propRetail] = value;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06005416 RID: 21526 RVA: 0x0012707A File Offset: 0x0012527A
		internal static bool RetailInternal
		{
			get
			{
				if (!DeploymentSection.s_hasCachedData)
				{
					DeploymentSection.s_retail = RuntimeConfig.GetAppConfig().Deployment.Retail;
					DeploymentSection.s_hasCachedData = true;
				}
				return DeploymentSection.s_retail;
			}
		}

		// Token: 0x04002C3A RID: 11322
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C3B RID: 11323
		private static readonly ConfigurationProperty _propRetail = new ConfigurationProperty("retail", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002C3C RID: 11324
		private static bool s_hasCachedData;

		// Token: 0x04002C3D RID: 11325
		private static bool s_retail;
	}
}
