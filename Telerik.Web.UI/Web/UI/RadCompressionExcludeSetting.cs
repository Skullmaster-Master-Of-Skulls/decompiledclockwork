using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x02001832 RID: 6194
	public class RadCompressionExcludeSetting : ConfigurationElement
	{
		// Token: 0x170048C8 RID: 18632
		// (get) Token: 0x0600F0D0 RID: 61648 RVA: 0x0036BAF8 File Offset: 0x00369CF8
		[ConfigurationProperty("handlerPath", DefaultValue = "", IsRequired = true)]
		public string HandlerPath
		{
			get
			{
				return (string)(base["handlerPath"] ?? string.Empty);
			}
		}

		// Token: 0x170048C9 RID: 18633
		// (get) Token: 0x0600F0D1 RID: 61649 RVA: 0x0036BB13 File Offset: 0x00369D13
		[ConfigurationProperty("matchExact", DefaultValue = "true", IsRequired = false)]
		public bool MatchExact
		{
			get
			{
				return base["matchExact"] == null || (bool)base["matchExact"];
			}
		}
	}
}
