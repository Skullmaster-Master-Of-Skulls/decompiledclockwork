using System;
using System.Configuration;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x02000745 RID: 1861
	[ConfigurationCollection(typeof(ProfilePropertySettings))]
	public sealed class RootProfilePropertySettingsCollection : ProfilePropertySettingsCollection
	{
		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x060059C0 RID: 22976 RVA: 0x0013987C File Offset: 0x00137A7C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RootProfilePropertySettingsCollection._properties;
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x060059C2 RID: 22978 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool AllowClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x00139898 File Offset: 0x00137A98
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			bool result;
			if (elementName == "group")
			{
				ProfileGroupSettings parentSettings = null;
				string attribute = reader.GetAttribute("name");
				ProfileGroupSettingsCollection groupSettings = this.GroupSettings;
				if (attribute != null)
				{
					parentSettings = groupSettings[attribute];
				}
				ProfileGroupSettings profileGroupSettings = new ProfileGroupSettings();
				profileGroupSettings.InternalReset(parentSettings);
				profileGroupSettings.InternalDeserialize(reader, false);
				groupSettings.AddOrReplace(profileGroupSettings);
				result = true;
			}
			else
			{
				if (elementName == "clear")
				{
					this.GroupSettings.Clear();
				}
				result = base.OnDeserializeUnrecognizedElement(elementName, reader);
			}
			return result;
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x00139919 File Offset: 0x00137B19
		protected override bool IsModified()
		{
			return base.IsModified() || this.GroupSettings.InternalIsModified();
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x00139930 File Offset: 0x00137B30
		protected override void ResetModified()
		{
			base.ResetModified();
			this.GroupSettings.InternalResetModified();
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x00139944 File Offset: 0x00137B44
		public override bool Equals(object rootProfilePropertySettingsCollection)
		{
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection2 = rootProfilePropertySettingsCollection as RootProfilePropertySettingsCollection;
			return rootProfilePropertySettingsCollection2 != null && object.Equals(this, rootProfilePropertySettingsCollection2) && object.Equals(this.GroupSettings, rootProfilePropertySettingsCollection2.GroupSettings);
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x00139977 File Offset: 0x00137B77
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(base.GetHashCode(), this.GroupSettings.GetHashCode());
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x00139990 File Offset: 0x00137B90
		protected override void Reset(ConfigurationElement parentElement)
		{
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection = parentElement as RootProfilePropertySettingsCollection;
			base.Reset(parentElement);
			this.GroupSettings.InternalReset(rootProfilePropertySettingsCollection.GroupSettings);
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x001399BC File Offset: 0x00137BBC
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection = parentElement as RootProfilePropertySettingsCollection;
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection2 = sourceElement as RootProfilePropertySettingsCollection;
			base.Unmerge(sourceElement, parentElement, saveMode);
			this.GroupSettings.InternalUnMerge(rootProfilePropertySettingsCollection2.GroupSettings, (rootProfilePropertySettingsCollection != null) ? rootProfilePropertySettingsCollection.GroupSettings : null, saveMode);
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x00139A00 File Offset: 0x00137C00
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			bool flag = false;
			if (base.SerializeElement(null, false) || this.GroupSettings.InternalSerialize(null, false))
			{
				flag |= base.SerializeElement(writer, false);
				flag |= this.GroupSettings.InternalSerialize(writer, false);
			}
			return flag;
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x060059CC RID: 22988 RVA: 0x00139A44 File Offset: 0x00137C44
		[ConfigurationProperty("group")]
		public ProfileGroupSettingsCollection GroupSettings
		{
			get
			{
				return this._propGroups;
			}
		}

		// Token: 0x04002F8D RID: 12173
		private ProfileGroupSettingsCollection _propGroups = new ProfileGroupSettingsCollection();

		// Token: 0x04002F8E RID: 12174
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
