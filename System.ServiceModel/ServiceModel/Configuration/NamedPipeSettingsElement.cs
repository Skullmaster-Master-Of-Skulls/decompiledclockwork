using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200064B RID: 1611
	public sealed class NamedPipeSettingsElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x000ED1BE File Offset: 0x000EB3BE
		// (set) Token: 0x06003E2A RID: 15914 RVA: 0x000ED1D0 File Offset: 0x000EB3D0
		[ConfigurationProperty("applicationContainerSettings")]
		public ApplicationContainerSettingsElement ApplicationContainerSettings
		{
			get
			{
				return (ApplicationContainerSettingsElement)base["applicationContainerSettings"];
			}
			set
			{
				base["applicationContainerSettings"] = value;
			}
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000ED1DE File Offset: 0x000EB3DE
		internal void ApplyConfiguration(NamedPipeSettings settings)
		{
			if (settings == null)
			{
				throw FxTrace.Exception.ArgumentNull("settings");
			}
			this.ApplicationContainerSettings.ApplyConfiguration(settings.ApplicationContainerSettings);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000ED204 File Offset: 0x000EB404
		internal void InitializeFrom(NamedPipeSettings settings)
		{
			if (settings == null)
			{
				throw FxTrace.Exception.ArgumentNull("settings");
			}
			this.ApplicationContainerSettings.InitializeFrom(settings.ApplicationContainerSettings);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000ED22A File Offset: 0x000EB42A
		internal void CopyFrom(NamedPipeSettingsElement source)
		{
			if (source == null)
			{
				throw FxTrace.Exception.ArgumentNull("source");
			}
			this.ApplicationContainerSettings.CopyFrom(source.ApplicationContainerSettings);
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000ED250 File Offset: 0x000EB450
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("applicationContainerSettings", typeof(ApplicationContainerSettingsElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C9D RID: 11421
		private ConfigurationPropertyCollection properties;
	}
}
