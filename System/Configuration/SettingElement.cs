using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000724 RID: 1828
	public sealed class SettingElement : ConfigurationElement
	{
		// Token: 0x060037D9 RID: 14297 RVA: 0x000EC488 File Offset: 0x000EB488
		static SettingElement()
		{
			SettingElement._properties = new ConfigurationPropertyCollection();
			SettingElement._properties.Add(SettingElement._propName);
			SettingElement._properties.Add(SettingElement._propSerializeAs);
			SettingElement._properties.Add(SettingElement._propValue);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000EC530 File Offset: 0x000EB530
		public SettingElement()
		{
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000EC538 File Offset: 0x000EB538
		public SettingElement(string name, SettingsSerializeAs serializeAs) : this()
		{
			this.Name = name;
			this.SerializeAs = serializeAs;
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060037DC RID: 14300 RVA: 0x000EC54E File Offset: 0x000EB54E
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000EC558 File Offset: 0x000EB558
		public override bool Equals(object settings)
		{
			SettingElement settingElement = settings as SettingElement;
			return settingElement != null && base.Equals(settings) && object.Equals(settingElement.Value, this.Value);
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000EC58B File Offset: 0x000EB58B
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000EC59F File Offset: 0x000EB59F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingElement._properties;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000EC5A6 File Offset: 0x000EB5A6
		// (set) Token: 0x060037E1 RID: 14305 RVA: 0x000EC5B8 File Offset: 0x000EB5B8
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[SettingElement._propName];
			}
			set
			{
				base[SettingElement._propName] = value;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000EC5C6 File Offset: 0x000EB5C6
		// (set) Token: 0x060037E3 RID: 14307 RVA: 0x000EC5D8 File Offset: 0x000EB5D8
		[ConfigurationProperty("serializeAs", IsRequired = true, DefaultValue = SettingsSerializeAs.String)]
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return (SettingsSerializeAs)base[SettingElement._propSerializeAs];
			}
			set
			{
				base[SettingElement._propSerializeAs] = value;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000EC5EB File Offset: 0x000EB5EB
		// (set) Token: 0x060037E5 RID: 14309 RVA: 0x000EC5FD File Offset: 0x000EB5FD
		[ConfigurationProperty("value", IsRequired = true, DefaultValue = null)]
		public SettingValueElement Value
		{
			get
			{
				return (SettingValueElement)base[SettingElement._propValue];
			}
			set
			{
				base[SettingElement._propValue] = value;
			}
		}

		// Token: 0x040031F3 RID: 12787
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x040031F4 RID: 12788
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040031F5 RID: 12789
		private static readonly ConfigurationProperty _propSerializeAs = new ConfigurationProperty("serializeAs", typeof(SettingsSerializeAs), SettingsSerializeAs.String, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040031F6 RID: 12790
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(SettingValueElement), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040031F7 RID: 12791
		private static XmlDocument doc = new XmlDocument();
	}
}
