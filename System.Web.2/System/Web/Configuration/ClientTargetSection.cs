using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006BB RID: 1723
	public sealed class ClientTargetSection : ConfigurationSection
	{
		// Token: 0x06005346 RID: 21318 RVA: 0x00124B47 File Offset: 0x00122D47
		static ClientTargetSection()
		{
			ClientTargetSection._properties = new ConfigurationPropertyCollection();
			ClientTargetSection._properties.Add(ClientTargetSection._propClientTargets);
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06005347 RID: 21319 RVA: 0x00124B79 File Offset: 0x00122D79
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTargetSection._properties;
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x00124B80 File Offset: 0x00122D80
		[ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
		public ClientTargetCollection ClientTargets
		{
			get
			{
				return (ClientTargetCollection)base[ClientTargetSection._propClientTargets];
			}
		}

		// Token: 0x04002BAB RID: 11179
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002BAC RID: 11180
		private static readonly ConfigurationProperty _propClientTargets = new ConfigurationProperty(null, typeof(ClientTargetCollection), null, ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired);
	}
}
