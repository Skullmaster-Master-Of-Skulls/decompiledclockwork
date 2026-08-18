using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006CD RID: 1741
	public abstract class ServiceModelConfigurationElement : ConfigurationElement
	{
		// Token: 0x06004357 RID: 17239 RVA: 0x000FE83C File Offset: 0x000FCA3C
		protected void SetPropertyValueIfNotDefaultValue<T>(string propertyName, T value)
		{
			ConfigurationProperty configurationProperty = this.Properties[propertyName];
			if (!object.Equals(value, configurationProperty.DefaultValue))
			{
				base.SetPropertyValue(configurationProperty, value, false);
			}
		}

		// Token: 0x04002D13 RID: 11539
		internal object lockObj = new object();
	}
}
