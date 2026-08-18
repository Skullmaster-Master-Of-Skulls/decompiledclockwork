using System;
using System.Configuration;

namespace AjaxControlToolkit
{
	// Token: 0x02000012 RID: 18
	internal class CustomControlElement : ConfigurationElement
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000407B File Offset: 0x0000227B
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000408D File Offset: 0x0000228D
		[ConfigurationProperty("type", IsRequired = true, IsKey = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}
	}
}
