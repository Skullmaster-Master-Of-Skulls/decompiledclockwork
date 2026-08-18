using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000332 RID: 818
	public sealed class HttpListenerElement : ConfigurationElement
	{
		// Token: 0x06001D51 RID: 7505 RVA: 0x0008B9EC File Offset: 0x00089BEC
		static HttpListenerElement()
		{
			HttpListenerElement.properties = new ConfigurationPropertyCollection();
			HttpListenerElement.properties.Add(HttpListenerElement.unescapeRequestUrl);
			HttpListenerElement.properties.Add(HttpListenerElement.timeouts);
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0008BA5C File Offset: 0x00089C5C
		[ConfigurationProperty("unescapeRequestUrl", DefaultValue = true, IsRequired = false)]
		public bool UnescapeRequestUrl
		{
			get
			{
				return (bool)base[HttpListenerElement.unescapeRequestUrl];
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0008BA6E File Offset: 0x00089C6E
		[ConfigurationProperty("timeouts")]
		public HttpListenerTimeoutsElement Timeouts
		{
			get
			{
				return (HttpListenerTimeoutsElement)base[HttpListenerElement.timeouts];
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0008BA80 File Offset: 0x00089C80
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpListenerElement.properties;
			}
		}

		// Token: 0x04001C3B RID: 7227
		internal const bool UnescapeRequestUrlDefaultValue = true;

		// Token: 0x04001C3C RID: 7228
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04001C3D RID: 7229
		private static readonly ConfigurationProperty unescapeRequestUrl = new ConfigurationProperty("unescapeRequestUrl", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C3E RID: 7230
		private static readonly ConfigurationProperty timeouts = new ConfigurationProperty("timeouts", typeof(HttpListenerTimeoutsElement), null, ConfigurationPropertyOptions.None);
	}
}
