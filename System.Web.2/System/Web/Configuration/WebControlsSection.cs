using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000778 RID: 1912
	public sealed class WebControlsSection : ConfigurationSection
	{
		// Token: 0x06005C1B RID: 23579 RVA: 0x0013EF80 File Offset: 0x0013D180
		static WebControlsSection()
		{
			WebControlsSection._properties = new ConfigurationPropertyCollection();
			WebControlsSection._properties.Add(WebControlsSection._propClientScriptsLocation);
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x0013EFC0 File Offset: 0x0013D1C0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebControlsSection._properties;
			}
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x0013EFC8 File Offset: 0x0013D1C8
		protected override object GetRuntimeObject()
		{
			Hashtable hashtable = new Hashtable();
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				hashtable[configurationProperty.Name] = base[configurationProperty];
			}
			return hashtable;
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x06005C1E RID: 23582 RVA: 0x0013F034 File Offset: 0x0013D234
		[ConfigurationProperty("clientScriptsLocation", IsRequired = true, DefaultValue = "/aspnet_client/{0}/{1}/")]
		[StringValidator(MinLength = 1)]
		public string ClientScriptsLocation
		{
			get
			{
				return (string)base[WebControlsSection._propClientScriptsLocation];
			}
		}

		// Token: 0x04003070 RID: 12400
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04003071 RID: 12401
		private static readonly ConfigurationProperty _propClientScriptsLocation = new ConfigurationProperty("clientScriptsLocation", typeof(string), "/aspnet_client/{0}/{1}/", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}
