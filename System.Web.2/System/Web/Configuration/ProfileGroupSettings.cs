using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x02000730 RID: 1840
	public sealed class ProfileGroupSettings : ConfigurationElement
	{
		// Token: 0x060058B4 RID: 22708 RVA: 0x001366E8 File Offset: 0x001348E8
		static ProfileGroupSettings()
		{
			ProfileGroupSettings._properties = new ConfigurationPropertyCollection();
			ProfileGroupSettings._properties.Add(ProfileGroupSettings._propName);
			ProfileGroupSettings._properties.Add(ProfileGroupSettings._propProperties);
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x00136755 File Offset: 0x00134955
		internal void InternalDeserialize(XmlReader reader, bool serializeCollectionKey)
		{
			this.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x00117E9E File Offset: 0x0011609E
		internal ProfileGroupSettings()
		{
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x0013675F File Offset: 0x0013495F
		public ProfileGroupSettings(string name)
		{
			base[ProfileGroupSettings._propName] = name;
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x00136774 File Offset: 0x00134974
		public override bool Equals(object obj)
		{
			ProfileGroupSettings profileGroupSettings = obj as ProfileGroupSettings;
			return profileGroupSettings != null && this.Name == profileGroupSettings.Name && object.Equals(this.PropertySettings, profileGroupSettings.PropertySettings);
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x001367B1 File Offset: 0x001349B1
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.PropertySettings.GetHashCode();
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x001367CA File Offset: 0x001349CA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileGroupSettings._properties;
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x060058BB RID: 22715 RVA: 0x001367D1 File Offset: 0x001349D1
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ProfileGroupSettings._propName];
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x060058BC RID: 22716 RVA: 0x001367E3 File Offset: 0x001349E3
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ProfilePropertySettingsCollection PropertySettings
		{
			get
			{
				return (ProfilePropertySettingsCollection)base[ProfileGroupSettings._propProperties];
			}
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x001367F5 File Offset: 0x001349F5
		internal void InternalReset(ProfileGroupSettings parentSettings)
		{
			base.Reset(parentSettings);
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x001367FE File Offset: 0x001349FE
		internal void InternalUnmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		// Token: 0x04002F2C RID: 12076
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002F2D RID: 12077
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, ProfilePropertyNameValidator.SingletonInstance, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002F2E RID: 12078
		private static readonly ConfigurationProperty _propProperties = new ConfigurationProperty(null, typeof(ProfilePropertySettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
