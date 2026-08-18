using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000667 RID: 1639
	public sealed class StandardBindingOptionalReliableSessionElement : StandardBindingReliableSessionElement
	{
		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x000EF72D File Offset: 0x000ED92D
		// (set) Token: 0x06003F04 RID: 16132 RVA: 0x000EF73F File Offset: 0x000ED93F
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x000EF752 File Offset: 0x000ED952
		public void InitializeFrom(OptionalReliableSession optionalReliableSession)
		{
			if (optionalReliableSession == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalReliableSession");
			}
			base.InitializeFrom(optionalReliableSession);
			base.SetPropertyValueIfNotDefaultValue<bool>("enabled", optionalReliableSession.Enabled);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x000EF77F File Offset: 0x000ED97F
		public void ApplyConfiguration(OptionalReliableSession optionalReliableSession)
		{
			if (optionalReliableSession == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalReliableSession");
			}
			base.ApplyConfiguration(optionalReliableSession);
			optionalReliableSession.Enabled = this.Enabled;
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x000EF7A8 File Offset: 0x000ED9A8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("enabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CAE RID: 11438
		private ConfigurationPropertyCollection properties;
	}
}
