using System;
using System.Collections;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x0200026C RID: 620
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebControlsSection : ConfigurationSection
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x0008E088 File Offset: 0x0008D088
		static WebControlsSection()
		{
			WebControlsSection._properties = new ConfigurationPropertyCollection();
			WebControlsSection._properties.Add(WebControlsSection._propClientScriptsLocation);
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x0008E0C8 File Offset: 0x0008D0C8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebControlsSection._properties;
			}
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0008E0D0 File Offset: 0x0008D0D0
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

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0008E13C File Offset: 0x0008D13C
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("clientScriptsLocation", IsRequired = true, DefaultValue = "/aspnet_client/{0}/{1}/")]
		public string ClientScriptsLocation
		{
			get
			{
				return (string)base[WebControlsSection._propClientScriptsLocation];
			}
		}

		// Token: 0x04001AAD RID: 6829
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04001AAE RID: 6830
		private static readonly ConfigurationProperty _propClientScriptsLocation = new ConfigurationProperty("clientScriptsLocation", typeof(string), "/aspnet_client/{0}/{1}/", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}
