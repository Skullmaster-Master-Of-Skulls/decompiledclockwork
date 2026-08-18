using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000011 RID: 17
	public sealed class AppSettingsSection : ConfigurationSection
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002590 File Offset: 0x00000790
		private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
		{
			if (AppSettingsSection.s_properties == null)
			{
				ConfigurationProperty property = new ConfigurationProperty(null, typeof(KeyValueConfigurationCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
				ConfigurationProperty property2 = new ConfigurationProperty("file", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
				ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
				configurationPropertyCollection.Add(property);
				configurationPropertyCollection.Add(property2);
				AppSettingsSection.s_propAppSettings = property;
				AppSettingsSection.s_propFile = property2;
				AppSettingsSection.s_properties = configurationPropertyCollection;
			}
			return AppSettingsSection.s_properties;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002607 File Offset: 0x00000807
		public AppSettingsSection()
		{
			AppSettingsSection.EnsureStaticPropertyBag();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002615 File Offset: 0x00000815
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AppSettingsSection.EnsureStaticPropertyBag();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000261C File Offset: 0x0000081C
		protected internal override object GetRuntimeObject()
		{
			this.SetReadOnly();
			return this.InternalSettings;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000262A File Offset: 0x0000082A
		internal NameValueCollection InternalSettings
		{
			get
			{
				if (this._KeyValueCollection == null)
				{
					this._KeyValueCollection = new KeyValueInternalCollection(this);
				}
				return this._KeyValueCollection;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002646 File Offset: 0x00000846
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public KeyValueConfigurationCollection Settings
		{
			get
			{
				return (KeyValueConfigurationCollection)base[AppSettingsSection.s_propAppSettings];
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000265C File Offset: 0x0000085C
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002686 File Offset: 0x00000886
		[ConfigurationProperty("file", DefaultValue = "")]
		public string File
		{
			get
			{
				string text = (string)base[AppSettingsSection.s_propFile];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base[AppSettingsSection.s_propFile] = value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002696 File Offset: 0x00000896
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			this._KeyValueCollection = null;
			base.Reset(parentSection);
			if (!string.IsNullOrEmpty((string)base[AppSettingsSection.s_propFile]))
			{
				base.SetPropertyValue(AppSettingsSection.s_propFile, null, true);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026CE File Offset: 0x000008CE
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026D6 File Offset: 0x000008D6
		protected internal override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			return base.SerializeSection(parentElement, name, saveMode);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026E4 File Offset: 0x000008E4
		protected internal override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			string name = reader.Name;
			base.DeserializeElement(reader, serializeCollectionKey);
			if (this.File != null && this.File.Length > 0)
			{
				string source = base.ElementInformation.Source;
				string text;
				if (string.IsNullOrEmpty(source))
				{
					text = this.File;
				}
				else
				{
					string directoryName = Path.GetDirectoryName(source);
					text = Path.Combine(directoryName, this.File);
				}
				if (System.IO.File.Exists(text))
				{
					int lineOffset = 0;
					string rawXml = null;
					using (Stream stream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						using (XmlUtil xmlUtil = new XmlUtil(stream, text, true))
						{
							if (xmlUtil.Reader.Name != name)
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_name_value_file_section_file_invalid_root", new object[]
								{
									name
								}), xmlUtil);
							}
							lineOffset = xmlUtil.Reader.LineNumber;
							rawXml = xmlUtil.CopySection();
							while (!xmlUtil.Reader.EOF)
							{
								XmlNodeType nodeType = xmlUtil.Reader.NodeType;
								if (nodeType != XmlNodeType.Comment)
								{
									throw new ConfigurationErrorsException(SR.GetString("Config_source_file_format"), xmlUtil);
								}
								xmlUtil.Reader.Read();
							}
						}
					}
					ConfigXmlReader configXmlReader = new ConfigXmlReader(rawXml, text, lineOffset);
					configXmlReader.Read();
					if (configXmlReader.MoveToNextAttribute())
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
						{
							configXmlReader.Name
						}), configXmlReader);
					}
					configXmlReader.MoveToElement();
					base.DeserializeElement(configXmlReader, serializeCollectionKey);
				}
			}
		}

		// Token: 0x040000BC RID: 188
		private static volatile ConfigurationPropertyCollection s_properties;

		// Token: 0x040000BD RID: 189
		private static volatile ConfigurationProperty s_propAppSettings;

		// Token: 0x040000BE RID: 190
		private static volatile ConfigurationProperty s_propFile;

		// Token: 0x040000BF RID: 191
		private KeyValueInternalCollection _KeyValueCollection;
	}
}
