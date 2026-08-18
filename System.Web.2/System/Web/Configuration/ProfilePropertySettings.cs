using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000734 RID: 1844
	public sealed class ProfilePropertySettings : ConfigurationElement
	{
		// Token: 0x060058DD RID: 22749 RVA: 0x00136AC0 File Offset: 0x00134CC0
		static ProfilePropertySettings()
		{
			ProfilePropertySettings._properties = new ConfigurationPropertyCollection();
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propName);
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propReadOnly);
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propSerializeAs);
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propProviderName);
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propDefaultValue);
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propType);
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propAllowAnonymous);
			ProfilePropertySettings._properties.Add(ProfilePropertySettings._propCustomProviderData);
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x00117E9E File Offset: 0x0011609E
		internal ProfilePropertySettings()
		{
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x00136C4C File Offset: 0x00134E4C
		public ProfilePropertySettings(string name)
		{
			this.Name = name;
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x00136C5C File Offset: 0x00134E5C
		public ProfilePropertySettings(string name, bool readOnly, SerializationMode serializeAs, string providerName, string defaultValue, string profileType, bool allowAnonymous, string customProviderData)
		{
			this.Name = name;
			this.ReadOnly = readOnly;
			this.SerializeAs = serializeAs;
			this.Provider = providerName;
			this.DefaultValue = defaultValue;
			this.Type = profileType;
			this.AllowAnonymous = allowAnonymous;
			this.CustomProviderData = customProviderData;
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x060058E1 RID: 22753 RVA: 0x00136CAC File Offset: 0x00134EAC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfilePropertySettings._properties;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x060058E2 RID: 22754 RVA: 0x00136CB3 File Offset: 0x00134EB3
		// (set) Token: 0x060058E3 RID: 22755 RVA: 0x00136CC5 File Offset: 0x00134EC5
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ProfilePropertySettings._propName];
			}
			set
			{
				base[ProfilePropertySettings._propName] = value;
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x060058E4 RID: 22756 RVA: 0x00136CD3 File Offset: 0x00134ED3
		// (set) Token: 0x060058E5 RID: 22757 RVA: 0x00136CE5 File Offset: 0x00134EE5
		[ConfigurationProperty("readOnly", DefaultValue = false)]
		public bool ReadOnly
		{
			get
			{
				return (bool)base[ProfilePropertySettings._propReadOnly];
			}
			set
			{
				base[ProfilePropertySettings._propReadOnly] = value;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x060058E6 RID: 22758 RVA: 0x00136CF8 File Offset: 0x00134EF8
		// (set) Token: 0x060058E7 RID: 22759 RVA: 0x00136D0A File Offset: 0x00134F0A
		[ConfigurationProperty("serializeAs", DefaultValue = SerializationMode.ProviderSpecific)]
		public SerializationMode SerializeAs
		{
			get
			{
				return (SerializationMode)base[ProfilePropertySettings._propSerializeAs];
			}
			set
			{
				base[ProfilePropertySettings._propSerializeAs] = value;
			}
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x00136D1D File Offset: 0x00134F1D
		// (set) Token: 0x060058E9 RID: 22761 RVA: 0x00136D2F File Offset: 0x00134F2F
		[ConfigurationProperty("provider", DefaultValue = "")]
		public string Provider
		{
			get
			{
				return (string)base[ProfilePropertySettings._propProviderName];
			}
			set
			{
				base[ProfilePropertySettings._propProviderName] = value;
			}
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x00136D3D File Offset: 0x00134F3D
		// (set) Token: 0x060058EB RID: 22763 RVA: 0x00136D45 File Offset: 0x00134F45
		internal SettingsProvider ProviderInternal
		{
			get
			{
				return this._providerInternal;
			}
			set
			{
				this._providerInternal = value;
			}
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x00136D4E File Offset: 0x00134F4E
		// (set) Token: 0x060058ED RID: 22765 RVA: 0x00136D60 File Offset: 0x00134F60
		[ConfigurationProperty("defaultValue", DefaultValue = "")]
		public string DefaultValue
		{
			get
			{
				return (string)base[ProfilePropertySettings._propDefaultValue];
			}
			set
			{
				base[ProfilePropertySettings._propDefaultValue] = value;
			}
		}

		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x060058EE RID: 22766 RVA: 0x00136D6E File Offset: 0x00134F6E
		// (set) Token: 0x060058EF RID: 22767 RVA: 0x00136D80 File Offset: 0x00134F80
		[ConfigurationProperty("type", DefaultValue = "string")]
		public string Type
		{
			get
			{
				return (string)base[ProfilePropertySettings._propType];
			}
			set
			{
				base[ProfilePropertySettings._propType] = value;
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x060058F0 RID: 22768 RVA: 0x00136D8E File Offset: 0x00134F8E
		// (set) Token: 0x060058F1 RID: 22769 RVA: 0x00136D96 File Offset: 0x00134F96
		internal Type TypeInternal
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x00136D9F File Offset: 0x00134F9F
		// (set) Token: 0x060058F3 RID: 22771 RVA: 0x00136DB1 File Offset: 0x00134FB1
		[ConfigurationProperty("allowAnonymous", DefaultValue = false)]
		public bool AllowAnonymous
		{
			get
			{
				return (bool)base[ProfilePropertySettings._propAllowAnonymous];
			}
			set
			{
				base[ProfilePropertySettings._propAllowAnonymous] = value;
			}
		}

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x060058F4 RID: 22772 RVA: 0x00136DC4 File Offset: 0x00134FC4
		// (set) Token: 0x060058F5 RID: 22773 RVA: 0x00136DD6 File Offset: 0x00134FD6
		[ConfigurationProperty("customProviderData", DefaultValue = "")]
		public string CustomProviderData
		{
			get
			{
				return (string)base[ProfilePropertySettings._propCustomProviderData];
			}
			set
			{
				base[ProfilePropertySettings._propCustomProviderData] = value;
			}
		}

		// Token: 0x04002F35 RID: 12085
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002F36 RID: 12086
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, ProfilePropertyNameValidator.SingletonInstance, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002F37 RID: 12087
		private static readonly ConfigurationProperty _propReadOnly = new ConfigurationProperty("readOnly", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F38 RID: 12088
		private static readonly ConfigurationProperty _propSerializeAs = new ConfigurationProperty("serializeAs", typeof(SerializationMode), SerializationMode.ProviderSpecific, ConfigurationPropertyOptions.None);

		// Token: 0x04002F39 RID: 12089
		private static readonly ConfigurationProperty _propProviderName = new ConfigurationProperty("provider", typeof(string), "", ConfigurationPropertyOptions.None);

		// Token: 0x04002F3A RID: 12090
		private static readonly ConfigurationProperty _propDefaultValue = new ConfigurationProperty("defaultValue", typeof(string), "", ConfigurationPropertyOptions.None);

		// Token: 0x04002F3B RID: 12091
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), "string", ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002F3C RID: 12092
		private static readonly ConfigurationProperty _propAllowAnonymous = new ConfigurationProperty("allowAnonymous", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F3D RID: 12093
		private static readonly ConfigurationProperty _propCustomProviderData = new ConfigurationProperty("customProviderData", typeof(string), "", ConfigurationPropertyOptions.None);

		// Token: 0x04002F3E RID: 12094
		private Type _type;

		// Token: 0x04002F3F RID: 12095
		private SettingsProvider _providerInternal;
	}
}
