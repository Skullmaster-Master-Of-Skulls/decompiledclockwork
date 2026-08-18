using System;

namespace System.Configuration
{
	// Token: 0x02000072 RID: 114
	public sealed class NameValueConfigurationElement : ConfigurationElement
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00018ADC File Offset: 0x00016CDC
		static NameValueConfigurationElement()
		{
			NameValueConfigurationElement._properties = new ConfigurationPropertyCollection();
			NameValueConfigurationElement._properties.Add(NameValueConfigurationElement._propName);
			NameValueConfigurationElement._properties.Add(NameValueConfigurationElement._propValue);
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00018B4F File Offset: 0x00016D4F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NameValueConfigurationElement._properties;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000127BF File Offset: 0x000109BF
		internal NameValueConfigurationElement()
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00018B56 File Offset: 0x00016D56
		public NameValueConfigurationElement(string name, string value)
		{
			base[NameValueConfigurationElement._propName] = name;
			base[NameValueConfigurationElement._propValue] = value;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00018B76 File Offset: 0x00016D76
		[ConfigurationProperty("name", IsKey = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[NameValueConfigurationElement._propName];
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00018B88 File Offset: 0x00016D88
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00018B9A File Offset: 0x00016D9A
		[ConfigurationProperty("value", DefaultValue = "")]
		public string Value
		{
			get
			{
				return (string)base[NameValueConfigurationElement._propValue];
			}
			set
			{
				base[NameValueConfigurationElement._propValue] = value;
			}
		}

		// Token: 0x040002B2 RID: 690
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x040002B3 RID: 691
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsKey);

		// Token: 0x040002B4 RID: 692
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
