using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200061E RID: 1566
	public sealed class EndToEndTracingElement : ConfigurationElement
	{
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x000E5D02 File Offset: 0x000E3F02
		// (set) Token: 0x06003C26 RID: 15398 RVA: 0x000E5D14 File Offset: 0x000E3F14
		[ConfigurationProperty("propagateActivity", DefaultValue = false)]
		public bool PropagateActivity
		{
			get
			{
				return (bool)base["propagateActivity"];
			}
			set
			{
				base["propagateActivity"] = value;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x000E5D27 File Offset: 0x000E3F27
		// (set) Token: 0x06003C28 RID: 15400 RVA: 0x000E5D39 File Offset: 0x000E3F39
		[ConfigurationProperty("activityTracing", DefaultValue = false)]
		public bool ActivityTracing
		{
			get
			{
				return (bool)base["activityTracing"];
			}
			set
			{
				base["activityTracing"] = value;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000E5D4C File Offset: 0x000E3F4C
		// (set) Token: 0x06003C2A RID: 15402 RVA: 0x000E5D5E File Offset: 0x000E3F5E
		[ConfigurationProperty("messageFlowTracing", DefaultValue = false)]
		public bool MessageFlowTracing
		{
			get
			{
				return (bool)base["messageFlowTracing"];
			}
			set
			{
				base["messageFlowTracing"] = value;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x000E5D74 File Offset: 0x000E3F74
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("propagateActivity", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("activityTracing", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("messageFlowTracing", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C7C RID: 11388
		private ConfigurationPropertyCollection properties;
	}
}
