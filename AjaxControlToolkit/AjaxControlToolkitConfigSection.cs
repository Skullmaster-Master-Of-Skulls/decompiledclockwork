using System;
using System.Configuration;

namespace AjaxControlToolkit
{
	// Token: 0x02000011 RID: 17
	public class AjaxControlToolkitConfigSection : ConfigurationSection
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003FD7 File Offset: 0x000021D7
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003FE9 File Offset: 0x000021E9
		[ConfigurationProperty("useStaticResources", DefaultValue = false)]
		public bool UseStaticResources
		{
			get
			{
				return (bool)base["useStaticResources"];
			}
			set
			{
				base["useStaticResources"] = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003FFC File Offset: 0x000021FC
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000400E File Offset: 0x0000220E
		[ConfigurationProperty("renderStyleLinks", DefaultValue = true)]
		public bool RenderStyleLinks
		{
			get
			{
				return (bool)base["renderStyleLinks"];
			}
			set
			{
				base["renderStyleLinks"] = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004021 File Offset: 0x00002221
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004033 File Offset: 0x00002233
		[ConfigurationProperty("htmlSanitizer")]
		public string HtmlSanitizer
		{
			get
			{
				return (string)base["htmlSanitizer"];
			}
			set
			{
				base["htmlSanitizer"] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004041 File Offset: 0x00002241
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004053 File Offset: 0x00002253
		[ConfigurationProperty("tempFolder", IsRequired = false)]
		public string TempFolder
		{
			get
			{
				return (string)base["tempFolder"];
			}
			set
			{
				base["tempFolder"] = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004061 File Offset: 0x00002261
		[ConfigurationProperty("customControls", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(CustomControlsCollection))]
		public CustomControlsCollection CustomControls
		{
			get
			{
				return (CustomControlsCollection)base["customControls"];
			}
		}
	}
}
