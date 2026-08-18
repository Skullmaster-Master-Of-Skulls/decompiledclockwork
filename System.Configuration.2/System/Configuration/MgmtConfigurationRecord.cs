using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Internal;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200006F RID: 111
	internal sealed class MgmtConfigurationRecord : BaseConfigurationRecord
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0001470C File Offset: 0x0001290C
		internal static MgmtConfigurationRecord Create(IInternalConfigRoot configRoot, IInternalConfigRecord parent, string configPath, string locationSubPath)
		{
			MgmtConfigurationRecord mgmtConfigurationRecord = new MgmtConfigurationRecord();
			mgmtConfigurationRecord.Init(configRoot, parent, configPath, locationSubPath);
			return mgmtConfigurationRecord;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001472A File Offset: 0x0001292A
		private MgmtConfigurationRecord()
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00014734 File Offset: 0x00012934
		private void Init(IInternalConfigRoot configRoot, IInternalConfigRecord parent, string configPath, string locationSubPath)
		{
			base.Init(configRoot, (BaseConfigurationRecord)parent, configPath, locationSubPath);
			if (base.IsLocationConfig && (this.MgmtParent._locationTags == null || !this.MgmtParent._locationTags.Contains(this._locationSubPath)))
			{
				this._flags[16777216] = true;
			}
			this.InitStreamInfoUpdates();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00014798 File Offset: 0x00012998
		private void InitStreamInfoUpdates()
		{
			this._streamInfoUpdates = new HybridDictionary(true);
			if (base.ConfigStreamInfo.HasStreamInfos)
			{
				foreach (object obj in base.ConfigStreamInfo.StreamInfos.Values)
				{
					StreamInfo streamInfo = (StreamInfo)obj;
					this._streamInfoUpdates.Add(streamInfo.StreamName, streamInfo.Clone());
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014824 File Offset: 0x00012A24
		private MgmtConfigurationRecord MgmtParent
		{
			get
			{
				return (MgmtConfigurationRecord)this._parent;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00014831 File Offset: 0x00012A31
		private UpdateConfigHost UpdateConfigHost
		{
			get
			{
				return (UpdateConfigHost)base.Host;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001483E File Offset: 0x00012A3E
		protected override SimpleBitVector32 ClassFlags
		{
			get
			{
				return MgmtConfigurationRecord.MgmtClassFlags;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00014848 File Offset: 0x00012A48
		protected override object CreateSectionFactory(FactoryRecord factoryRecord)
		{
			Type type = TypeUtil.GetTypeWithReflectionPermission(base.Host, factoryRecord.FactoryTypeName, true);
			if (!typeof(ConfigurationSection).IsAssignableFrom(type))
			{
				TypeUtil.VerifyAssignableType(typeof(IConfigurationSectionHandler), type, true);
				type = typeof(DefaultSection);
			}
			return TypeUtil.GetConstructorWithReflectionPermission(type, typeof(ConfigurationSection), true);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000148AC File Offset: 0x00012AAC
		protected override object CreateSection(bool inputIsTrusted, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader)
		{
			ConstructorInfo ctor = (ConstructorInfo)factoryRecord.Factory;
			ConfigurationSection configurationSection = (ConfigurationSection)TypeUtil.InvokeCtorWithReflectionPermission(ctor);
			configurationSection.SectionInformation.AttachToConfigurationRecord(this, factoryRecord, sectionRecord);
			configurationSection.CallInit();
			ConfigurationSection parentElement = (ConfigurationSection)parentConfig;
			configurationSection.Reset(parentElement);
			if (reader != null)
			{
				configurationSection.DeserializeSection(reader);
			}
			if (sectionInput != null && sectionInput.ConfigBuilder != null)
			{
				configurationSection = this.CallHostProcessConfigurationSection(configurationSection, sectionInput.ConfigBuilder);
			}
			configurationSection.ResetModified();
			return configurationSection;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00014924 File Offset: 0x00012B24
		private ConstructorInfo CreateSectionGroupFactory(FactoryRecord factoryRecord)
		{
			Type type;
			if (string.IsNullOrEmpty(factoryRecord.FactoryTypeName))
			{
				type = typeof(ConfigurationSectionGroup);
			}
			else
			{
				type = TypeUtil.GetTypeWithReflectionPermission(base.Host, factoryRecord.FactoryTypeName, true);
			}
			return TypeUtil.GetConstructorWithReflectionPermission(type, typeof(ConfigurationSectionGroup), true);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00014974 File Offset: 0x00012B74
		private ConstructorInfo EnsureSectionGroupFactory(FactoryRecord factoryRecord)
		{
			ConstructorInfo constructorInfo = (ConstructorInfo)factoryRecord.Factory;
			if (constructorInfo == null)
			{
				constructorInfo = this.CreateSectionGroupFactory(factoryRecord);
				factoryRecord.Factory = constructorInfo;
			}
			return constructorInfo;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000149A8 File Offset: 0x00012BA8
		protected override object UseParentResult(string configKey, object parentResult, SectionRecord sectionRecord)
		{
			FactoryRecord factoryRecord = base.FindFactoryRecord(configKey, false);
			if (factoryRecord == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_unrecognized_configuration_section", new object[]
				{
					configKey
				}));
			}
			return base.CallCreateSection(false, factoryRecord, sectionRecord, null, parentResult, null);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000935E File Offset: 0x0000755E
		protected override object GetRuntimeObject(object result)
		{
			return result;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000149EC File Offset: 0x00012BEC
		private ConfigurationSection GetConfigSection(string configKey)
		{
			SectionRecord sectionRecord = base.GetSectionRecord(configKey, false);
			if (sectionRecord != null && sectionRecord.HasResult)
			{
				return (ConfigurationSection)sectionRecord.Result;
			}
			return null;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00014A1A File Offset: 0x00012C1A
		private Hashtable SectionGroups
		{
			get
			{
				if (this._sectionGroups == null)
				{
					this._sectionGroups = new Hashtable();
				}
				return this._sectionGroups;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00014A35 File Offset: 0x00012C35
		private Hashtable RemovedSections
		{
			get
			{
				if (this._removedSections == null)
				{
					this._removedSections = new Hashtable();
				}
				return this._removedSections;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00014A50 File Offset: 0x00012C50
		private Hashtable RemovedSectionGroups
		{
			get
			{
				if (this._removedSectionGroups == null)
				{
					this._removedSectionGroups = new Hashtable();
				}
				return this._removedSectionGroups;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014A6C File Offset: 0x00012C6C
		internal ConfigurationSectionGroup LookupSectionGroup(string configKey)
		{
			ConfigurationSectionGroup result = null;
			if (this._sectionGroups != null)
			{
				result = (ConfigurationSectionGroup)this._sectionGroups[configKey];
			}
			return result;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014A98 File Offset: 0x00012C98
		internal ConfigurationSectionGroup GetSectionGroup(string configKey)
		{
			ConfigurationSectionGroup configurationSectionGroup = this.LookupSectionGroup(configKey);
			if (configurationSectionGroup == null)
			{
				BaseConfigurationRecord baseConfigurationRecord;
				FactoryRecord factoryRecord = base.FindFactoryRecord(configKey, false, out baseConfigurationRecord);
				if (factoryRecord == null)
				{
					return null;
				}
				if (!factoryRecord.IsGroup)
				{
					throw ExceptionUtil.ParameterInvalid("sectionGroupName");
				}
				if (factoryRecord.FactoryTypeName == null)
				{
					configurationSectionGroup = new ConfigurationSectionGroup();
				}
				else
				{
					ConstructorInfo ctor = this.EnsureSectionGroupFactory(factoryRecord);
					try
					{
						configurationSectionGroup = (ConfigurationSectionGroup)TypeUtil.InvokeCtorWithReflectionPermission(ctor);
					}
					catch (Exception inner)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_exception_creating_section_handler", new object[]
						{
							factoryRecord.ConfigKey
						}), inner, factoryRecord);
					}
				}
				configurationSectionGroup.AttachToConfigurationRecord(this, factoryRecord);
				this.SectionGroups[configKey] = configurationSectionGroup;
			}
			return configurationSectionGroup;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014B48 File Offset: 0x00012D48
		internal ConfigurationLocationCollection GetLocationCollection(Configuration config)
		{
			ArrayList arrayList = new ArrayList();
			if (this._locationTags != null)
			{
				foreach (object obj in this._locationTags.Values)
				{
					string locationSubPath = (string)obj;
					arrayList.Add(new ConfigurationLocation(config, locationSubPath));
				}
			}
			return new ConfigurationLocationCollection(arrayList);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014BC4 File Offset: 0x00012DC4
		protected override void AddLocation(string locationSubPath)
		{
			if (this._locationTags == null)
			{
				this._locationTags = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			this._locationTags[locationSubPath] = locationSubPath;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00014BEB File Offset: 0x00012DEB
		internal Hashtable SectionFactories
		{
			get
			{
				if (this._sectionFactories == null)
				{
					this._sectionFactories = this.GetAllFactories(false);
				}
				return this._sectionFactories;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00014C08 File Offset: 0x00012E08
		internal Hashtable SectionGroupFactories
		{
			get
			{
				if (this._sectionGroupFactories == null)
				{
					this._sectionGroupFactories = this.GetAllFactories(true);
				}
				return this._sectionGroupFactories;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00014C28 File Offset: 0x00012E28
		private Hashtable GetAllFactories(bool isGroup)
		{
			Hashtable hashtable = new Hashtable();
			MgmtConfigurationRecord mgmtConfigurationRecord = this;
			do
			{
				if (mgmtConfigurationRecord._factoryRecords != null)
				{
					foreach (object obj in mgmtConfigurationRecord._factoryRecords.Values)
					{
						FactoryRecord factoryRecord = (FactoryRecord)obj;
						if (factoryRecord.IsGroup == isGroup)
						{
							string configKey = factoryRecord.ConfigKey;
							hashtable[configKey] = new FactoryId(factoryRecord.ConfigKey, factoryRecord.Group, factoryRecord.Name);
						}
					}
				}
				mgmtConfigurationRecord = mgmtConfigurationRecord.MgmtParent;
			}
			while (!mgmtConfigurationRecord.IsRootConfig);
			return hashtable;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014CD8 File Offset: 0x00012ED8
		internal ConfigurationSection FindImmediateParentSection(ConfigurationSection section)
		{
			ConfigurationSection configurationSection = null;
			string sectionName = section.SectionInformation.SectionName;
			SectionRecord sectionRecord = base.GetSectionRecord(sectionName, false);
			if (sectionRecord.HasLocationInputs)
			{
				SectionInput lastLocationInput = sectionRecord.LastLocationInput;
				configurationSection = (ConfigurationSection)lastLocationInput.Result;
			}
			else if (sectionRecord.HasIndirectLocationInputs)
			{
				SectionInput lastIndirectLocationInput = sectionRecord.LastIndirectLocationInput;
				configurationSection = (ConfigurationSection)lastIndirectLocationInput.Result;
			}
			else if (base.IsRootDeclaration(sectionName, true))
			{
				FactoryRecord factoryRecord = base.GetFactoryRecord(sectionName, false);
				object obj;
				object obj2;
				base.CreateSectionDefault(sectionName, false, factoryRecord, null, out obj, out obj2);
				configurationSection = (ConfigurationSection)obj;
			}
			else
			{
				MgmtConfigurationRecord mgmtParent = this.MgmtParent;
				while (!mgmtParent.IsRootConfig)
				{
					sectionRecord = mgmtParent.GetSectionRecord(sectionName, false);
					if (sectionRecord != null && sectionRecord.HasResult)
					{
						configurationSection = (ConfigurationSection)sectionRecord.Result;
						break;
					}
					mgmtParent = mgmtParent.MgmtParent;
				}
			}
			if (!configurationSection.IsReadOnly())
			{
				configurationSection.SetReadOnly();
			}
			return configurationSection;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014DB8 File Offset: 0x00012FB8
		internal ConfigurationSection FindAndCloneImmediateParentSection(ConfigurationSection configSection)
		{
			string configKey = configSection.SectionInformation.ConfigKey;
			ConfigurationSection parentResult = this.FindImmediateParentSection(configSection);
			SectionRecord sectionRecord = base.GetSectionRecord(configKey, false);
			return (ConfigurationSection)this.UseParentResult(configKey, parentResult, sectionRecord);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014DF4 File Offset: 0x00012FF4
		internal void RevertToParent(ConfigurationSection configSection)
		{
			configSection.SectionInformation.RawXml = null;
			try
			{
				ConfigurationSection parentElement = this.FindImmediateParentSection(configSection);
				configSection.Reset(parentElement);
				configSection.ResetModified();
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_exception_in_config_section_handler", new object[]
				{
					configSection.SectionInformation.SectionName
				}), inner, base.ConfigStreamInfo.StreamName, 0);
			}
			configSection.SectionInformation.Removed = true;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00014E74 File Offset: 0x00013074
		internal string GetRawXml(string configKey)
		{
			SectionRecord sectionRecord = base.GetSectionRecord(configKey, false);
			if (sectionRecord == null || !sectionRecord.HasFileInput)
			{
				return null;
			}
			string[] keys = configKey.Split(BaseConfigurationRecord.ConfigPathSeparatorParams);
			ConfigXmlReader sectionXmlReader = base.GetSectionXmlReader(keys, sectionRecord.FileInput);
			return sectionXmlReader.RawXml;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00014EB8 File Offset: 0x000130B8
		internal void SetRawXml(ConfigurationSection configSection, string xmlElement)
		{
			if (string.IsNullOrEmpty(xmlElement))
			{
				this.RevertToParent(configSection);
				return;
			}
			this.ValidateSectionXml(xmlElement, configSection.SectionInformation.Name);
			ConfigurationSection parentElement = this.FindImmediateParentSection(configSection);
			ConfigXmlReader reader = new ConfigXmlReader(xmlElement, null, 0);
			configSection.SectionInformation.RawXml = xmlElement;
			try
			{
				try
				{
					bool elementPresent = configSection.ElementPresent;
					PropertySourceInfo sourceInformation = configSection.ElementInformation.PropertyInfoInternal();
					configSection.Reset(parentElement);
					configSection.DeserializeSection(reader);
					configSection.ResetModified();
					configSection.ElementPresent = elementPresent;
					configSection.ElementInformation.ChangeSourceAndLineNumber(sourceInformation);
				}
				catch
				{
					configSection.SectionInformation.RawXml = null;
					throw;
				}
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_exception_in_config_section_handler", new object[]
				{
					configSection.SectionInformation.SectionName
				}), inner, null, 0);
			}
			configSection.SectionInformation.Removed = false;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00014FA4 File Offset: 0x000131A4
		private bool IsStreamUsed(string oldStreamName)
		{
			MgmtConfigurationRecord mgmtConfigurationRecord = this;
			if (base.IsLocationConfig)
			{
				mgmtConfigurationRecord = this.MgmtParent;
				if (mgmtConfigurationRecord._sectionRecords != null)
				{
					foreach (object obj in mgmtConfigurationRecord._sectionRecords.Values)
					{
						SectionRecord sectionRecord = (SectionRecord)obj;
						if (sectionRecord.HasFileInput && StringUtil.EqualsIgnoreCase(sectionRecord.FileInput.SectionXmlInfo.ConfigSourceStreamName, oldStreamName))
						{
							return true;
						}
					}
				}
			}
			if (mgmtConfigurationRecord._locationSections != null)
			{
				foreach (object obj2 in mgmtConfigurationRecord._locationSections)
				{
					LocationSectionRecord locationSectionRecord = (LocationSectionRecord)obj2;
					if (StringUtil.EqualsIgnoreCase(locationSectionRecord.SectionXmlInfo.ConfigSourceStreamName, oldStreamName))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000150A8 File Offset: 0x000132A8
		internal void ChangeConfigSource(SectionInformation sectionInformation, string oldConfigSource, string oldConfigSourceStreamName, string newConfigSource)
		{
			if (string.IsNullOrEmpty(oldConfigSource))
			{
				oldConfigSource = null;
			}
			if (string.IsNullOrEmpty(newConfigSource))
			{
				newConfigSource = null;
			}
			if (StringUtil.EqualsIgnoreCase(oldConfigSource, newConfigSource))
			{
				return;
			}
			if (string.IsNullOrEmpty(base.ConfigStreamInfo.StreamName))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_source_requires_file"));
			}
			string text = null;
			if (newConfigSource != null)
			{
				text = base.Host.GetStreamNameForConfigSource(base.ConfigStreamInfo.StreamName, newConfigSource);
			}
			if (text != null)
			{
				base.ValidateUniqueChildConfigSource(sectionInformation.ConfigKey, text, newConfigSource, null);
				StreamInfo streamInfo = (StreamInfo)this._streamInfoUpdates[text];
				if (streamInfo != null)
				{
					if (streamInfo.SectionName != sectionInformation.ConfigKey)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_source_cannot_be_shared", new object[]
						{
							newConfigSource
						}));
					}
				}
				else
				{
					streamInfo = new StreamInfo(sectionInformation.ConfigKey, newConfigSource, text);
					this._streamInfoUpdates.Add(text, streamInfo);
				}
			}
			if (oldConfigSourceStreamName != null && !this.IsStreamUsed(oldConfigSourceStreamName))
			{
				this._streamInfoUpdates.Remove(oldConfigSourceStreamName);
			}
			sectionInformation.ConfigSourceStreamName = text;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000151AC File Offset: 0x000133AC
		private void ValidateSectionXml(string xmlElement, string configKey)
		{
			if (string.IsNullOrEmpty(xmlElement))
			{
				return;
			}
			XmlTextReader xmlTextReader = null;
			try
			{
				XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.Default, Encoding.Unicode);
				xmlTextReader = new XmlTextReader(xmlElement, XmlNodeType.Element, context);
				xmlTextReader.Read();
				if (xmlTextReader.NodeType != XmlNodeType.Element)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_unexpected_node_type", new object[]
					{
						xmlTextReader.NodeType
					}));
				}
				string text;
				string b;
				BaseConfigurationRecord.SplitConfigKey(configKey, out text, out b);
				if (xmlTextReader.Name != b)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_unexpected_element_name", new object[]
					{
						xmlTextReader.Name
					}));
				}
				while (xmlTextReader.Read())
				{
					XmlNodeType nodeType = xmlTextReader.NodeType;
					if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_invalid_node_type"), xmlTextReader);
					}
					if (xmlTextReader.Depth <= 0 && xmlTextReader.NodeType != XmlNodeType.EndElement)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_more_data_than_expected"), xmlTextReader);
					}
				}
				if (xmlTextReader.Depth != 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_unexpected_element_end"), xmlTextReader);
				}
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000152D0 File Offset: 0x000134D0
		internal void AddConfigurationSection(string group, string name, ConfigurationSection configSection)
		{
			if (base.IsLocationConfig)
			{
				throw new InvalidOperationException(SR.GetString("Config_add_configurationsection_in_location_config"));
			}
			BaseConfigurationRecord.VerifySectionName(name, null, false, false);
			if (configSection == null)
			{
				throw new ArgumentNullException("configSection");
			}
			if (configSection.SectionInformation.Attached)
			{
				throw new InvalidOperationException(SR.GetString("Config_add_configurationsection_already_added"));
			}
			string text = BaseConfigurationRecord.CombineConfigKey(group, name);
			FactoryRecord factoryRecord = base.FindFactoryRecord(text, true);
			if (factoryRecord != null)
			{
				throw new ArgumentException(SR.GetString("Config_add_configurationsection_already_exists"));
			}
			if (!string.IsNullOrEmpty(configSection.SectionInformation.ConfigSource))
			{
				this.ChangeConfigSource(configSection.SectionInformation, null, null, configSection.SectionInformation.ConfigSource);
			}
			if (this._sectionFactories != null)
			{
				this._sectionFactories.Add(text, new FactoryId(text, group, name));
			}
			string text2 = configSection.SectionInformation.Type;
			if (text2 == null)
			{
				text2 = base.Host.GetConfigTypeName(configSection.GetType());
			}
			factoryRecord = new FactoryRecord(text, group, name, text2, configSection.SectionInformation.AllowLocation, configSection.SectionInformation.AllowDefinition, configSection.SectionInformation.AllowExeDefinition, configSection.SectionInformation.OverrideModeDefaultSetting, configSection.SectionInformation.RestartOnExternalChanges, configSection.SectionInformation.RequirePermission, this._flags[8192], false, base.ConfigStreamInfo.StreamName, -1);
			factoryRecord.Factory = TypeUtil.GetConstructorWithReflectionPermission(configSection.GetType(), typeof(ConfigurationSection), true);
			factoryRecord.IsFactoryTrustedWithoutAptca = TypeUtil.IsTypeFromTrustedAssemblyWithoutAptca(configSection.GetType());
			base.EnsureFactories()[text] = factoryRecord;
			SectionRecord sectionRecord = base.EnsureSectionRecordUnsafe(text, false);
			sectionRecord.Result = configSection;
			sectionRecord.ResultRuntimeObject = configSection;
			if (this._removedSections != null)
			{
				this._removedSections.Remove(text);
			}
			configSection.SectionInformation.AttachToConfigurationRecord(this, factoryRecord, sectionRecord);
			string rawXml = configSection.SectionInformation.RawXml;
			if (!string.IsNullOrEmpty(rawXml))
			{
				configSection.SectionInformation.RawXml = null;
				configSection.SectionInformation.SetRawXml(rawXml);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000154C0 File Offset: 0x000136C0
		internal void RemoveConfigurationSection(string group, string name)
		{
			bool flag = false;
			BaseConfigurationRecord.VerifySectionName(name, null, true, false);
			string text = BaseConfigurationRecord.CombineConfigKey(group, name);
			if (this.RemovedSections.Contains(text))
			{
				return;
			}
			if (base.FindFactoryRecord(text, true) == null)
			{
				return;
			}
			ConfigurationSection configSection = this.GetConfigSection(text);
			if (configSection != null)
			{
				configSection.SectionInformation.DetachFromConfigurationRecord();
			}
			bool flag2 = base.IsRootDeclaration(text, false);
			if (this._sectionFactories != null && flag2)
			{
				this._sectionFactories.Remove(text);
			}
			if (!base.IsLocationConfig && this._factoryRecords != null && this._factoryRecords.Contains(text))
			{
				flag = true;
				this._factoryRecords.Remove(text);
			}
			if (this._sectionRecords != null && this._sectionRecords.Contains(text))
			{
				flag = true;
				this._sectionRecords.Remove(text);
			}
			if (this._locationSections != null)
			{
				int i = 0;
				while (i < this._locationSections.Count)
				{
					LocationSectionRecord locationSectionRecord = (LocationSectionRecord)this._locationSections[i];
					if (locationSectionRecord.ConfigKey != text)
					{
						i++;
					}
					else
					{
						flag = true;
						this._locationSections.RemoveAt(i);
					}
				}
			}
			if (flag)
			{
				this.RemovedSections.Add(text, text);
			}
			List<string> list = new List<string>();
			foreach (object obj in this._streamInfoUpdates.Values)
			{
				StreamInfo streamInfo = (StreamInfo)obj;
				if (streamInfo.SectionName == text)
				{
					list.Add(streamInfo.StreamName);
				}
			}
			foreach (string key in list)
			{
				this._streamInfoUpdates.Remove(key);
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000156A4 File Offset: 0x000138A4
		internal void AddConfigurationSectionGroup(string group, string name, ConfigurationSectionGroup configSectionGroup)
		{
			if (base.IsLocationConfig)
			{
				throw new InvalidOperationException(SR.GetString("Config_add_configurationsectiongroup_in_location_config"));
			}
			BaseConfigurationRecord.VerifySectionName(name, null, false, false);
			if (configSectionGroup == null)
			{
				throw ExceptionUtil.ParameterInvalid("name");
			}
			if (configSectionGroup.Attached)
			{
				throw new InvalidOperationException(SR.GetString("Config_add_configurationsectiongroup_already_added"));
			}
			string text = BaseConfigurationRecord.CombineConfigKey(group, name);
			FactoryRecord factoryRecord = base.FindFactoryRecord(text, true);
			if (factoryRecord != null)
			{
				throw new ArgumentException(SR.GetString("Config_add_configurationsectiongroup_already_exists"));
			}
			if (this._sectionGroupFactories != null)
			{
				this._sectionGroupFactories.Add(text, new FactoryId(text, group, name));
			}
			string text2 = configSectionGroup.Type;
			if (text2 == null)
			{
				text2 = base.Host.GetConfigTypeName(configSectionGroup.GetType());
			}
			factoryRecord = new FactoryRecord(text, group, name, text2, base.ConfigStreamInfo.StreamName, -1);
			base.EnsureFactories()[text] = factoryRecord;
			this.SectionGroups[text] = configSectionGroup;
			if (this._removedSectionGroups != null)
			{
				this._removedSectionGroups.Remove(text);
			}
			configSectionGroup.AttachToConfigurationRecord(this, factoryRecord);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000157A0 File Offset: 0x000139A0
		private ArrayList GetDescendentSectionFactories(string configKey)
		{
			ArrayList arrayList = new ArrayList();
			string s;
			if (configKey.Length == 0)
			{
				s = string.Empty;
			}
			else
			{
				s = configKey + "/";
			}
			foreach (object obj in this.SectionFactories.Values)
			{
				FactoryId factoryId = (FactoryId)obj;
				if (factoryId.Group == configKey || StringUtil.StartsWith(factoryId.Group, s))
				{
					arrayList.Add(factoryId);
				}
			}
			return arrayList;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015844 File Offset: 0x00013A44
		private ArrayList GetDescendentSectionGroupFactories(string configKey)
		{
			ArrayList arrayList = new ArrayList();
			string s;
			if (configKey.Length == 0)
			{
				s = string.Empty;
			}
			else
			{
				s = configKey + "/";
			}
			foreach (object obj in this.SectionGroupFactories.Values)
			{
				FactoryId factoryId = (FactoryId)obj;
				if (factoryId.ConfigKey == configKey || StringUtil.StartsWith(factoryId.ConfigKey, s))
				{
					arrayList.Add(factoryId);
				}
			}
			return arrayList;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000158E8 File Offset: 0x00013AE8
		internal void RemoveConfigurationSectionGroup(string group, string name)
		{
			BaseConfigurationRecord.VerifySectionName(name, null, false, false);
			string configKey = BaseConfigurationRecord.CombineConfigKey(group, name);
			if (base.FindFactoryRecord(configKey, true) == null)
			{
				return;
			}
			ArrayList descendentSectionFactories = this.GetDescendentSectionFactories(configKey);
			foreach (object obj in descendentSectionFactories)
			{
				FactoryId factoryId = (FactoryId)obj;
				this.RemoveConfigurationSection(factoryId.Group, factoryId.Name);
			}
			ArrayList descendentSectionGroupFactories = this.GetDescendentSectionGroupFactories(configKey);
			foreach (object obj2 in descendentSectionGroupFactories)
			{
				FactoryId factoryId2 = (FactoryId)obj2;
				if (!this.RemovedSectionGroups.Contains(factoryId2.ConfigKey))
				{
					ConfigurationSectionGroup configurationSectionGroup = this.LookupSectionGroup(factoryId2.ConfigKey);
					if (configurationSectionGroup != null)
					{
						configurationSectionGroup.DetachFromConfigurationRecord();
					}
					bool flag = base.IsRootDeclaration(factoryId2.ConfigKey, false);
					if (this._sectionGroupFactories != null && flag)
					{
						this._sectionGroupFactories.Remove(factoryId2.ConfigKey);
					}
					if (!base.IsLocationConfig && this._factoryRecords != null)
					{
						this._factoryRecords.Remove(factoryId2.ConfigKey);
					}
					if (this._sectionGroups != null)
					{
						this._sectionGroups.Remove(factoryId2.ConfigKey);
					}
					this.RemovedSectionGroups.Add(factoryId2.ConfigKey, factoryId2.ConfigKey);
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00015A7C File Offset: 0x00013C7C
		internal string ConfigurationFilePath
		{
			get
			{
				string text = this.UpdateConfigHost.GetNewStreamname(base.ConfigStreamInfo.StreamName);
				if (text == null)
				{
					text = string.Empty;
				}
				if (!string.IsNullOrEmpty(text))
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00015AC0 File Offset: 0x00013CC0
		internal void SaveAs(string filename, ConfigurationSaveMode saveMode, bool forceUpdateAll)
		{
			SectionUpdates configDeclarationUpdates = this.GetConfigDeclarationUpdates(saveMode, forceUpdateAll);
			bool flag = false;
			bool flag2 = filename != null;
			ConfigDefinitionUpdates configDefinitionUpdates;
			ArrayList arrayList;
			this.GetConfigDefinitionUpdates(flag2, saveMode, forceUpdateAll, out configDefinitionUpdates, out arrayList);
			if (filename != null)
			{
				if (!base.Host.IsRemote && this._streamInfoUpdates.Contains(filename))
				{
					throw new ArgumentException(SR.GetString("Filename_in_SaveAs_is_used_already", new object[]
					{
						filename
					}));
				}
				if (string.IsNullOrEmpty(base.ConfigStreamInfo.StreamName))
				{
					StreamInfo value = new StreamInfo(null, null, filename);
					this._streamInfoUpdates.Add(filename, value);
					base.ConfigStreamInfo.StreamName = filename;
					base.ConfigStreamInfo.StreamVersion = base.MonitorStream(null, null, base.ConfigStreamInfo.StreamName);
				}
				this.UpdateConfigHost.AddStreamname(base.ConfigStreamInfo.StreamName, filename, base.Host.IsRemote);
				foreach (object obj in this._streamInfoUpdates.Values)
				{
					StreamInfo streamInfo = (StreamInfo)obj;
					if (!string.IsNullOrEmpty(streamInfo.SectionName))
					{
						string newStreamname = InternalConfigHost.StaticGetStreamNameForConfigSource(filename, streamInfo.ConfigSource);
						this.UpdateConfigHost.AddStreamname(streamInfo.StreamName, newStreamname, base.Host.IsRemote);
					}
				}
			}
			if (!flag2)
			{
				flag2 = this.RecordItselfRequiresUpdates;
			}
			if (configDeclarationUpdates != null || configDefinitionUpdates != null || flag2)
			{
				byte[] buffer = null;
				Encoding encoding = null;
				if (base.ConfigStreamInfo.HasStream)
				{
					using (Stream stream = base.Host.OpenStreamForRead(base.ConfigStreamInfo.StreamName))
					{
						if (stream == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_file_has_changed"), base.ConfigStreamInfo.StreamName, 0);
						}
						buffer = new byte[stream.Length];
						int num = stream.Read(buffer, 0, (int)stream.Length);
						if ((long)num != stream.Length)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_data_read_count_mismatch"));
						}
					}
					try
					{
						using (StreamReader streamReader = new StreamReader(base.ConfigStreamInfo.StreamName))
						{
							if (streamReader.Peek() >= 0)
							{
								streamReader.Read();
							}
							if (streamReader.CurrentEncoding is UnicodeEncoding)
							{
								encoding = streamReader.CurrentEncoding;
							}
						}
					}
					catch
					{
					}
				}
				string text = base.FindChangedConfigurationStream();
				if (text != null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_file_has_changed"), text, 0);
				}
				flag = true;
				object writeContext = null;
				bool flag3 = false;
				try
				{
					try
					{
						using (Stream stream2 = base.Host.OpenStreamForWrite(base.ConfigStreamInfo.StreamName, null, ref writeContext))
						{
							flag3 = true;
							using (StreamWriter streamWriter = (encoding == null) ? new StreamWriter(stream2) : new StreamWriter(stream2, encoding))
							{
								XmlUtilWriter utilWriter = new XmlUtilWriter(streamWriter, true);
								if (base.ConfigStreamInfo.HasStream)
								{
									this.CopyConfig(configDeclarationUpdates, configDefinitionUpdates, buffer, base.ConfigStreamInfo.StreamName, this.NamespaceChangeNeeded, utilWriter);
								}
								else
								{
									this.CreateNewConfig(configDeclarationUpdates, configDefinitionUpdates, this.NamespaceChangeNeeded, utilWriter);
								}
							}
						}
					}
					catch
					{
						if (flag3)
						{
							base.Host.WriteCompleted(base.ConfigStreamInfo.StreamName, false, writeContext);
						}
						throw;
					}
				}
				catch (Exception e)
				{
					throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_error_loading_XML_file"), e, base.ConfigStreamInfo.StreamName, 0);
				}
				base.Host.WriteCompleted(base.ConfigStreamInfo.StreamName, true, writeContext);
				base.ConfigStreamInfo.HasStream = true;
				base.ConfigStreamInfo.ClearStreamInfos();
				base.ConfigStreamInfo.StreamVersion = base.MonitorStream(null, null, base.ConfigStreamInfo.StreamName);
			}
			if (arrayList != null)
			{
				if (!flag)
				{
					string text2 = base.FindChangedConfigurationStream();
					if (text2 != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_file_has_changed"), text2, 0);
					}
				}
				foreach (object obj2 in arrayList)
				{
					DefinitionUpdate update = (DefinitionUpdate)obj2;
					this.SaveConfigSource(update);
				}
			}
			this.UpdateRecords();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00015F50 File Offset: 0x00014150
		private bool AreDeclarationAttributesModified(FactoryRecord factoryRecord, ConfigurationSection configSection)
		{
			return factoryRecord.FactoryTypeName != configSection.SectionInformation.Type || factoryRecord.AllowLocation != configSection.SectionInformation.AllowLocation || factoryRecord.RestartOnExternalChanges != configSection.SectionInformation.RestartOnExternalChanges || factoryRecord.RequirePermission != configSection.SectionInformation.RequirePermission || factoryRecord.AllowDefinition != configSection.SectionInformation.AllowDefinition || factoryRecord.AllowExeDefinition != configSection.SectionInformation.AllowExeDefinition || factoryRecord.OverrideModeDefault.OverrideMode != configSection.SectionInformation.OverrideModeDefaultSetting.OverrideMode || configSection.SectionInformation.IsModifiedFlags();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00016007 File Offset: 0x00014207
		private void AppendAttribute(StringBuilder sb, string key, string value)
		{
			sb.Append(key);
			sb.Append("=\"");
			sb.Append(value);
			sb.Append("\" ");
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00016034 File Offset: 0x00014234
		private string GetUpdatedSectionDeclarationXml(FactoryRecord factoryRecord, ConfigurationSection configSection, ConfigurationSaveMode saveMode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('<');
			stringBuilder.Append("section");
			stringBuilder.Append(' ');
			string text = (configSection.SectionInformation.Type != null) ? configSection.SectionInformation.Type : factoryRecord.FactoryTypeName;
			if (base.TypeStringTransformerIsSet)
			{
				text = base.TypeStringTransformer(text);
			}
			this.AppendAttribute(stringBuilder, "name", configSection.SectionInformation.Name);
			this.AppendAttribute(stringBuilder, "type", text);
			if (!configSection.SectionInformation.AllowLocation || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && configSection.SectionInformation.AllowLocationModified))
			{
				this.AppendAttribute(stringBuilder, "allowLocation", configSection.SectionInformation.AllowLocation ? "true" : "false");
			}
			if (configSection.SectionInformation.AllowDefinition != ConfigurationAllowDefinition.Everywhere || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && configSection.SectionInformation.AllowDefinitionModified))
			{
				string value = null;
				ConfigurationAllowDefinition allowDefinition = configSection.SectionInformation.AllowDefinition;
				if (allowDefinition <= ConfigurationAllowDefinition.MachineToWebRoot)
				{
					if (allowDefinition != ConfigurationAllowDefinition.MachineOnly)
					{
						if (allowDefinition == ConfigurationAllowDefinition.MachineToWebRoot)
						{
							value = "MachineToWebRoot";
						}
					}
					else
					{
						value = "MachineOnly";
					}
				}
				else if (allowDefinition != ConfigurationAllowDefinition.MachineToApplication)
				{
					if (allowDefinition == ConfigurationAllowDefinition.Everywhere)
					{
						value = "Everywhere";
					}
				}
				else
				{
					value = "MachineToApplication";
				}
				this.AppendAttribute(stringBuilder, "allowDefinition", value);
			}
			if (configSection.SectionInformation.AllowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && configSection.SectionInformation.AllowExeDefinitionModified))
			{
				this.AppendAttribute(stringBuilder, "allowExeDefinition", this.ExeDefinitionToString(configSection.SectionInformation.AllowExeDefinition));
			}
			if (!configSection.SectionInformation.OverrideModeDefaultSetting.IsDefaultForSection || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && configSection.SectionInformation.OverrideModeDefaultModified))
			{
				this.AppendAttribute(stringBuilder, "overrideModeDefault", configSection.SectionInformation.OverrideModeDefaultSetting.OverrideModeXmlValue);
			}
			if (!configSection.SectionInformation.RestartOnExternalChanges)
			{
				this.AppendAttribute(stringBuilder, "restartOnExternalChanges", "false");
			}
			else if (saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && configSection.SectionInformation.RestartOnExternalChangesModified))
			{
				this.AppendAttribute(stringBuilder, "restartOnExternalChanges", "true");
			}
			if (!configSection.SectionInformation.RequirePermission)
			{
				this.AppendAttribute(stringBuilder, "requirePermission", "false");
			}
			else if (saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && configSection.SectionInformation.RequirePermissionModified))
			{
				this.AppendAttribute(stringBuilder, "requirePermission", "true");
			}
			stringBuilder.Append("/>");
			return stringBuilder.ToString();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000162AC File Offset: 0x000144AC
		private string ExeDefinitionToString(ConfigurationAllowExeDefinition allowDefinition)
		{
			if (allowDefinition <= ConfigurationAllowExeDefinition.MachineToApplication)
			{
				if (allowDefinition == ConfigurationAllowExeDefinition.MachineOnly)
				{
					return "MachineOnly";
				}
				if (allowDefinition == ConfigurationAllowExeDefinition.MachineToApplication)
				{
					return "MachineToApplication";
				}
			}
			else
			{
				if (allowDefinition == ConfigurationAllowExeDefinition.MachineToRoamingUser)
				{
					return "MachineToRoamingUser";
				}
				if (allowDefinition == ConfigurationAllowExeDefinition.MachineToLocalUser)
				{
					return "MachineToLocalUser";
				}
			}
			throw ExceptionUtil.PropertyInvalid("AllowExeDefinition");
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000162FC File Offset: 0x000144FC
		private string GetUpdatedSectionGroupDeclarationXml(FactoryRecord factoryRecord, ConfigurationSectionGroup configSectionGroup)
		{
			if (base.TargetFramework != null && !configSectionGroup.ShouldSerializeSectionGroupInTargetVersion(base.TargetFramework))
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('<');
			stringBuilder.Append("sectionGroup");
			stringBuilder.Append(' ');
			this.AppendAttribute(stringBuilder, "name", configSectionGroup.Name);
			string text = (configSectionGroup.Type != null) ? configSectionGroup.Type : factoryRecord.FactoryTypeName;
			if (base.TypeStringTransformerIsSet)
			{
				text = base.TypeStringTransformer(text);
			}
			this.AppendAttribute(stringBuilder, "type", text);
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000163A5 File Offset: 0x000145A5
		private bool HasRemovedSectionsOrGroups
		{
			get
			{
				return (this._removedSections != null && this._removedSections.Count > 0) || (this._removedSectionGroups != null && this._removedSectionGroups.Count > 0);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000163D7 File Offset: 0x000145D7
		private bool HasRemovedSections
		{
			get
			{
				return this._removedSections != null && this._removedSections.Count > 0;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000163F4 File Offset: 0x000145F4
		private SectionUpdates GetConfigDeclarationUpdates(ConfigurationSaveMode saveMode, bool forceUpdateAll)
		{
			if (base.IsLocationConfig)
			{
				return null;
			}
			bool flag = this.HasRemovedSectionsOrGroups;
			SectionUpdates sectionUpdates = new SectionUpdates(string.Empty);
			if (this._factoryRecords != null)
			{
				foreach (object obj in this._factoryRecords.Values)
				{
					FactoryRecord factoryRecord = (FactoryRecord)obj;
					if (!factoryRecord.IsGroup)
					{
						string text = null;
						if (!factoryRecord.IsUndeclared)
						{
							ConfigurationSection configSection = this.GetConfigSection(factoryRecord.ConfigKey);
							if (configSection != null)
							{
								if (!configSection.SectionInformation.IsDeclared && !this.MgmtParent.IsRootConfig && this.MgmtParent.FindFactoryRecord(factoryRecord.ConfigKey, false) != null)
								{
									if (factoryRecord.HasFile)
									{
										flag = true;
										continue;
									}
									continue;
								}
								else
								{
									if (base.TargetFramework != null && !configSection.ShouldSerializeSectionInTargetVersion(base.TargetFramework))
									{
										continue;
									}
									if (this.AreDeclarationAttributesModified(factoryRecord, configSection) || !factoryRecord.HasFile)
									{
										text = this.GetUpdatedSectionDeclarationXml(factoryRecord, configSection, saveMode);
										if (!string.IsNullOrEmpty(text))
										{
											flag = true;
										}
									}
								}
							}
							DeclarationUpdate update = new DeclarationUpdate(factoryRecord.ConfigKey, !factoryRecord.HasFile, text);
							sectionUpdates.AddSection(update);
						}
					}
					else
					{
						bool flag2 = false;
						ConfigurationSectionGroup configurationSectionGroup = this.LookupSectionGroup(factoryRecord.ConfigKey);
						if (!factoryRecord.HasFile)
						{
							flag2 = true;
						}
						else if (configurationSectionGroup != null && configurationSectionGroup.IsDeclarationRequired)
						{
							flag2 = true;
						}
						else if (factoryRecord.FactoryTypeName != null || configurationSectionGroup != null)
						{
							FactoryRecord factoryRecord2 = null;
							if (!this.MgmtParent.IsRootConfig)
							{
								factoryRecord2 = this.MgmtParent.FindFactoryRecord(factoryRecord.ConfigKey, false);
							}
							flag2 = (factoryRecord2 == null || factoryRecord2.FactoryTypeName == null);
						}
						if (flag2)
						{
							string text2 = null;
							if (!factoryRecord.HasFile || (configurationSectionGroup != null && configurationSectionGroup.Type != factoryRecord.FactoryTypeName))
							{
								text2 = this.GetUpdatedSectionGroupDeclarationXml(factoryRecord, configurationSectionGroup);
								if (!string.IsNullOrEmpty(text2))
								{
									flag = true;
								}
							}
							DeclarationUpdate update2 = new DeclarationUpdate(factoryRecord.ConfigKey, !factoryRecord.HasFile, text2);
							sectionUpdates.AddSectionGroup(update2);
						}
					}
				}
			}
			if (this._sectionRecords != null)
			{
				foreach (object obj2 in this._sectionRecords.Values)
				{
					SectionRecord sectionRecord = (SectionRecord)obj2;
					if (base.GetFactoryRecord(sectionRecord.ConfigKey, false) == null && sectionRecord.HasResult)
					{
						ConfigurationSection configurationSection = (ConfigurationSection)sectionRecord.Result;
						FactoryRecord factoryRecord3 = this.MgmtParent.FindFactoryRecord(sectionRecord.ConfigKey, false);
						if (configurationSection.SectionInformation.IsDeclared)
						{
							string updatedSectionDeclarationXml = this.GetUpdatedSectionDeclarationXml(factoryRecord3, configurationSection, saveMode);
							if (!string.IsNullOrEmpty(updatedSectionDeclarationXml))
							{
								flag = true;
								DeclarationUpdate update3 = new DeclarationUpdate(factoryRecord3.ConfigKey, true, updatedSectionDeclarationXml);
								sectionUpdates.AddSection(update3);
							}
						}
					}
				}
			}
			if (this._sectionGroups != null)
			{
				foreach (object obj3 in this._sectionGroups.Values)
				{
					ConfigurationSectionGroup configurationSectionGroup2 = (ConfigurationSectionGroup)obj3;
					if (base.GetFactoryRecord(configurationSectionGroup2.SectionGroupName, false) == null)
					{
						FactoryRecord factoryRecord4 = this.MgmtParent.FindFactoryRecord(configurationSectionGroup2.SectionGroupName, false);
						if (configurationSectionGroup2.IsDeclared || (factoryRecord4 != null && configurationSectionGroup2.Type != factoryRecord4.FactoryTypeName))
						{
							string updatedSectionGroupDeclarationXml = this.GetUpdatedSectionGroupDeclarationXml(factoryRecord4, configurationSectionGroup2);
							if (!string.IsNullOrEmpty(updatedSectionGroupDeclarationXml))
							{
								flag = true;
								DeclarationUpdate update4 = new DeclarationUpdate(factoryRecord4.ConfigKey, true, updatedSectionGroupDeclarationXml);
								sectionUpdates.AddSectionGroup(update4);
							}
						}
					}
				}
			}
			if (flag)
			{
				return sectionUpdates;
			}
			return null;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00016804 File Offset: 0x00014A04
		private bool AreLocationAttributesModified(SectionRecord sectionRecord, ConfigurationSection configSection)
		{
			OverrideModeSetting x = OverrideModeSetting.LocationDefault;
			bool flag = true;
			if (sectionRecord.HasFileInput)
			{
				SectionXmlInfo sectionXmlInfo = sectionRecord.FileInput.SectionXmlInfo;
				x = sectionXmlInfo.OverrideModeSetting;
				flag = !sectionXmlInfo.SkipInChildApps;
			}
			return !OverrideModeSetting.CanUseSameLocationTag(x, configSection.SectionInformation.OverrideModeSetting) || flag != configSection.SectionInformation.InheritInChildApplications;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00016864 File Offset: 0x00014A64
		private bool AreSectionAttributesModified(SectionRecord sectionRecord, ConfigurationSection configSection)
		{
			string s;
			string s2;
			string s3;
			if (sectionRecord.HasFileInput)
			{
				SectionXmlInfo sectionXmlInfo = sectionRecord.FileInput.SectionXmlInfo;
				s = sectionXmlInfo.ConfigSource;
				s2 = sectionXmlInfo.ProtectionProviderName;
				s3 = sectionXmlInfo.ConfigBuilderName;
			}
			else
			{
				s = null;
				s2 = null;
				s3 = null;
			}
			return !StringUtil.EqualsNE(s, configSection.SectionInformation.ConfigSource) || !StringUtil.EqualsNE(s2, configSection.SectionInformation.ProtectionProviderName) || !StringUtil.EqualsNE(s3, configSection.SectionInformation.ConfigBuilderName) || this.AreLocationAttributesModified(sectionRecord, configSection);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000168E5 File Offset: 0x00014AE5
		private bool IsConfigSectionMoved(SectionRecord sectionRecord, ConfigurationSection configSection)
		{
			return !sectionRecord.HasFileInput || this.AreLocationAttributesModified(sectionRecord, configSection);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000168FC File Offset: 0x00014AFC
		private void GetConfigDefinitionUpdates(bool requireUpdates, ConfigurationSaveMode saveMode, bool forceSaveAll, out ConfigDefinitionUpdates definitionUpdates, out ArrayList configSourceUpdates)
		{
			definitionUpdates = new ConfigDefinitionUpdates();
			configSourceUpdates = null;
			bool flag = this.HasRemovedSections;
			if (this._sectionRecords != null)
			{
				base.InitProtectedConfigurationSection();
				foreach (object obj in this._sectionRecords)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					SectionRecord sectionRecord = (SectionRecord)dictionaryEntry.Value;
					sectionRecord.AddUpdate = false;
					bool flag2 = sectionRecord.HasFileInput;
					OverrideModeSetting overrideMode = OverrideModeSetting.LocationDefault;
					bool flag3 = true;
					bool flag4 = false;
					string text2 = null;
					bool flag5 = false;
					if (!sectionRecord.HasResult)
					{
						if (sectionRecord.HasFileInput)
						{
							SectionXmlInfo sectionXmlInfo = sectionRecord.FileInput.SectionXmlInfo;
							overrideMode = sectionXmlInfo.OverrideModeSetting;
							flag3 = !sectionXmlInfo.SkipInChildApps;
							flag5 = (requireUpdates && !string.IsNullOrEmpty(sectionXmlInfo.ConfigSource));
						}
					}
					else
					{
						ConfigurationSection configurationSection = (ConfigurationSection)sectionRecord.Result;
						if (base.TargetFramework != null && !configurationSection.ShouldSerializeSectionInTargetVersion(base.TargetFramework))
						{
							continue;
						}
						overrideMode = configurationSection.SectionInformation.OverrideModeSetting;
						flag3 = configurationSection.SectionInformation.InheritInChildApplications;
						if (!configurationSection.SectionInformation.AllowLocation && (!overrideMode.IsDefaultForLocationTag || !flag3))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_inconsistent_location_attributes", new object[]
							{
								text
							}));
						}
						flag5 = (requireUpdates && !string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource));
						try
						{
							bool flag6 = configurationSection.SectionInformation.ForceSave || configurationSection.IsModified() || (forceSaveAll && !configurationSection.SectionInformation.IsLocked);
							bool flag7 = this.AreSectionAttributesModified(sectionRecord, configurationSection);
							bool flag8 = flag6 || configurationSection.SectionInformation.RawXml != null;
							if (flag8 || flag7)
							{
								configurationSection.SectionInformation.VerifyIsEditable();
								configurationSection.SectionInformation.Removed = false;
								flag2 = true;
								flag4 = this.IsConfigSectionMoved(sectionRecord, configurationSection);
								if (!flag5)
								{
									flag5 = (!string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource) && (flag8 || configurationSection.SectionInformation.ConfigSourceModified));
								}
								if (flag6 || configurationSection.SectionInformation.RawXml == null || saveMode == ConfigurationSaveMode.Full)
								{
									ConfigurationSection parentElement = this.FindImmediateParentSection(configurationSection);
									text2 = configurationSection.SerializeSection(parentElement, configurationSection.SectionInformation.Name, saveMode);
									if (string.Equals(configurationSection.SectionInformation.Name, "System.Windows.Forms.ApplicationConfigurationSection", StringComparison.Ordinal) && (string.Equals(configurationSection._configRecord.ConfigPath, "MACHINE", StringComparison.Ordinal) || string.Equals(text2, this.WriteEmptyElement("System.Windows.Forms.ApplicationConfigurationSection"), StringComparison.Ordinal)))
									{
										text2 = null;
									}
									this.ValidateSectionXml(text2, text);
								}
								else
								{
									text2 = configurationSection.SectionInformation.RawXml;
								}
								if (string.IsNullOrEmpty(text2) && (!string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource) || !configurationSection.SectionInformation.LocationAttributesAreDefault || configurationSection.SectionInformation.ProtectionProvider != null))
								{
									text2 = this.WriteEmptyElement(configurationSection.SectionInformation.Name);
								}
								if (string.IsNullOrEmpty(text2))
								{
									configurationSection.SectionInformation.Removed = true;
									text2 = null;
									flag2 = false;
									if (sectionRecord.HasFileInput)
									{
										flag = true;
										sectionRecord.RemoveFileInput();
										goto IL_429;
									}
									goto IL_429;
								}
								else
								{
									if (flag7 || flag4 || string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource))
									{
										flag = true;
									}
									if (configurationSection.SectionInformation.ProtectionProvider == null)
									{
										goto IL_429;
									}
									ProtectedConfigurationSection protectedConfigSection = base.GetSection("configProtectedData") as ProtectedConfigurationSection;
									try
									{
										string encryptedXml = base.Host.EncryptSection(text2, configurationSection.SectionInformation.ProtectionProvider, protectedConfigSection);
										text2 = ProtectedConfigurationSection.FormatEncryptedSection(encryptedXml, configurationSection.SectionInformation.Name, configurationSection.SectionInformation.ProtectionProvider.Name);
										goto IL_429;
									}
									catch (Exception ex)
									{
										throw new ConfigurationErrorsException(SR.GetString("Encryption_failed", new object[]
										{
											configurationSection.SectionInformation.SectionName,
											configurationSection.SectionInformation.ProtectionProvider.Name,
											ex.Message
										}), ex);
									}
								}
							}
							if (configurationSection.SectionInformation.Removed)
							{
								flag2 = false;
								if (sectionRecord.HasFileInput)
								{
									flag = true;
								}
							}
							IL_429:;
						}
						catch (Exception inner)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_exception_in_config_section_handler", new object[]
							{
								configurationSection.SectionInformation.SectionName
							}), inner);
						}
					}
					if (flag2)
					{
						if (base.GetSectionLockedMode(sectionRecord.ConfigKey) == OverrideMode.Deny)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_section_locked"), null);
						}
						sectionRecord.AddUpdate = true;
						DefinitionUpdate value = definitionUpdates.AddUpdate(overrideMode, flag3, flag4, text2, sectionRecord);
						if (flag5)
						{
							if (configSourceUpdates == null)
							{
								configSourceUpdates = new ArrayList();
							}
							configSourceUpdates.Add(value);
						}
					}
				}
			}
			if (this._flags[16777216])
			{
				flag = true;
				definitionUpdates.RequireLocation = true;
			}
			if (this._flags[33554432])
			{
				flag = true;
			}
			if (flag)
			{
				definitionUpdates.CompleteUpdates();
				return;
			}
			definitionUpdates = null;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00016E68 File Offset: 0x00015068
		private string WriteEmptyElement(string ElementName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('<');
			stringBuilder.Append(ElementName);
			stringBuilder.Append(" />");
			return stringBuilder.ToString();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00016EA0 File Offset: 0x000150A0
		private void UpdateRecords()
		{
			if (this._factoryRecords != null)
			{
				foreach (object obj in this._factoryRecords.Values)
				{
					FactoryRecord factoryRecord = (FactoryRecord)obj;
					if (string.IsNullOrEmpty(factoryRecord.Filename))
					{
						factoryRecord.Filename = base.ConfigStreamInfo.StreamName;
					}
					factoryRecord.LineNumber = 0;
					ConfigurationSection configSection = this.GetConfigSection(factoryRecord.ConfigKey);
					if (configSection != null)
					{
						if (configSection.SectionInformation.Type != null)
						{
							factoryRecord.FactoryTypeName = configSection.SectionInformation.Type;
						}
						factoryRecord.AllowLocation = configSection.SectionInformation.AllowLocation;
						factoryRecord.RestartOnExternalChanges = configSection.SectionInformation.RestartOnExternalChanges;
						factoryRecord.RequirePermission = configSection.SectionInformation.RequirePermission;
						factoryRecord.AllowDefinition = configSection.SectionInformation.AllowDefinition;
						factoryRecord.AllowExeDefinition = configSection.SectionInformation.AllowExeDefinition;
					}
				}
			}
			if (this._sectionRecords != null)
			{
				string definitionConfigPath = base.IsLocationConfig ? this._parent.ConfigPath : base.ConfigPath;
				foreach (object obj2 in this._sectionRecords.Values)
				{
					SectionRecord sectionRecord = (SectionRecord)obj2;
					ConfigurationSection configurationSection;
					string text;
					string text2;
					if (sectionRecord.HasResult)
					{
						configurationSection = (ConfigurationSection)sectionRecord.Result;
						text = configurationSection.SectionInformation.ConfigSource;
						if (string.IsNullOrEmpty(text))
						{
							text = null;
						}
						text2 = configurationSection.SectionInformation.ConfigSourceStreamName;
						if (string.IsNullOrEmpty(text2))
						{
							text2 = null;
						}
					}
					else
					{
						configurationSection = null;
						text = null;
						text2 = null;
						if (sectionRecord.HasFileInput)
						{
							SectionXmlInfo sectionXmlInfo = sectionRecord.FileInput.SectionXmlInfo;
							text = sectionXmlInfo.ConfigSource;
							text2 = sectionXmlInfo.ConfigSourceStreamName;
						}
					}
					object configSourceStreamVersion;
					if (!string.IsNullOrEmpty(text))
					{
						configSourceStreamVersion = base.MonitorStream(sectionRecord.ConfigKey, text, text2);
					}
					else
					{
						configSourceStreamVersion = null;
					}
					if (!sectionRecord.HasResult)
					{
						if (sectionRecord.HasFileInput)
						{
							SectionXmlInfo sectionXmlInfo2 = sectionRecord.FileInput.SectionXmlInfo;
							sectionXmlInfo2.StreamVersion = base.ConfigStreamInfo.StreamVersion;
							sectionXmlInfo2.ConfigSourceStreamVersion = configSourceStreamVersion;
						}
					}
					else
					{
						configurationSection.SectionInformation.RawXml = null;
						bool addUpdate = sectionRecord.AddUpdate;
						sectionRecord.AddUpdate = false;
						if (addUpdate)
						{
							SectionInput sectionInput = sectionRecord.FileInput;
							if (sectionInput == null)
							{
								SectionXmlInfo sectionXmlInfo3 = new SectionXmlInfo(sectionRecord.ConfigKey, definitionConfigPath, this._configPath, this._locationSubPath, base.ConfigStreamInfo.StreamName, 0, base.ConfigStreamInfo.StreamVersion, null, text, text2, configSourceStreamVersion, configurationSection.SectionInformation.ConfigBuilderName, configurationSection.SectionInformation.ProtectionProviderName, configurationSection.SectionInformation.OverrideModeSetting, !configurationSection.SectionInformation.InheritInChildApplications);
								sectionInput = new SectionInput(sectionXmlInfo3, null);
								sectionInput.Result = configurationSection;
								sectionInput.ResultRuntimeObject = configurationSection;
								sectionRecord.AddFileInput(sectionInput);
							}
							else
							{
								SectionXmlInfo sectionXmlInfo4 = sectionInput.SectionXmlInfo;
								sectionXmlInfo4.LineNumber = 0;
								sectionXmlInfo4.StreamVersion = base.ConfigStreamInfo.StreamVersion;
								sectionXmlInfo4.RawXml = null;
								sectionXmlInfo4.ConfigSource = text;
								sectionXmlInfo4.ConfigSourceStreamName = text2;
								sectionXmlInfo4.ConfigSourceStreamVersion = configSourceStreamVersion;
								sectionXmlInfo4.ConfigBuilderName = configurationSection.SectionInformation.ConfigBuilderName;
								sectionXmlInfo4.ProtectionProviderName = configurationSection.SectionInformation.ProtectionProviderName;
								sectionXmlInfo4.OverrideModeSetting = configurationSection.SectionInformation.OverrideModeSetting;
								sectionXmlInfo4.SkipInChildApps = !configurationSection.SectionInformation.InheritInChildApplications;
							}
							sectionInput.ProtectionProvider = configurationSection.SectionInformation.ProtectionProvider;
						}
						try
						{
							configurationSection.ResetModified();
						}
						catch (Exception inner)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_exception_in_config_section_handler", new object[]
							{
								sectionRecord.ConfigKey
							}), inner, base.ConfigStreamInfo.StreamName, 0);
						}
					}
				}
			}
			foreach (object obj3 in this._streamInfoUpdates.Values)
			{
				StreamInfo streamInfo = (StreamInfo)obj3;
				if (!base.ConfigStreamInfo.StreamInfos.Contains(streamInfo.StreamName))
				{
					base.MonitorStream(streamInfo.SectionName, streamInfo.ConfigSource, streamInfo.StreamName);
				}
			}
			this.InitStreamInfoUpdates();
			this._flags[512] = this._flags[67108864];
			this._flags[16777216] = false;
			this._flags[33554432] = false;
			if (!base.IsLocationConfig && this._locationSections != null && this._removedSections != null && this._removedSections.Count > 0)
			{
				int i = 0;
				while (i < this._locationSections.Count)
				{
					LocationSectionRecord locationSectionRecord = (LocationSectionRecord)this._locationSections[i];
					if (this._removedSections.Contains(locationSectionRecord.ConfigKey))
					{
						this._locationSections.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
			this._removedSections = null;
			this._removedSectionGroups = null;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00017444 File Offset: 0x00015644
		private void CreateNewConfig(SectionUpdates declarationUpdates, ConfigDefinitionUpdates definitionUpdates, NamespaceChange namespaceChange, XmlUtilWriter utilWriter)
		{
			int linePosition = 5;
			int indent = 4;
			utilWriter.Write(string.Format(CultureInfo.InvariantCulture, "<?xml version=\"1.0\" encoding=\"{0}\"?>\r\n", new object[]
			{
				base.ConfigStreamInfo.StreamEncoding.WebName
			}));
			if (namespaceChange == NamespaceChange.Add)
			{
				utilWriter.Write(string.Format(CultureInfo.InvariantCulture, "<configuration xmlns=\"{0}\">\r\n", new object[]
				{
					"http://schemas.microsoft.com/.NetConfiguration/v2.0"
				}));
			}
			else
			{
				utilWriter.Write("<configuration>\r\n");
			}
			if (declarationUpdates != null)
			{
				this.WriteNewConfigDeclarations(declarationUpdates, utilWriter, linePosition, indent, false);
			}
			this.WriteNewConfigDefinitions(definitionUpdates, utilWriter, linePosition, indent);
			utilWriter.Write("</configuration>");
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000174E4 File Offset: 0x000156E4
		private void WriteNewConfigDeclarations(SectionUpdates declarationUpdates, XmlUtilWriter utilWriter, int linePosition, int indent, bool skipFirstIndent)
		{
			if (!skipFirstIndent)
			{
				utilWriter.AppendSpacesToLinePosition(linePosition);
			}
			utilWriter.Write("<configSections>\r\n");
			this.WriteUnwrittenConfigDeclarations(declarationUpdates, utilWriter, linePosition + indent, indent, false);
			utilWriter.AppendSpacesToLinePosition(linePosition);
			utilWriter.Write("</configSections>\r\n");
			if (skipFirstIndent)
			{
				utilWriter.AppendSpacesToLinePosition(linePosition);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00017538 File Offset: 0x00015738
		private void WriteUnwrittenConfigDeclarations(SectionUpdates declarationUpdates, XmlUtilWriter utilWriter, int linePosition, int indent, bool skipFirstIndent)
		{
			this.WriteUnwrittenConfigDeclarationsRecursive(declarationUpdates, utilWriter, linePosition, indent, skipFirstIndent);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00017548 File Offset: 0x00015748
		private void WriteUnwrittenConfigDeclarationsRecursive(SectionUpdates declarationUpdates, XmlUtilWriter utilWriter, int linePosition, int indent, bool skipFirstIndent)
		{
			string[] unretrievedSectionNames = declarationUpdates.GetUnretrievedSectionNames();
			if (unretrievedSectionNames != null)
			{
				foreach (string configKey in unretrievedSectionNames)
				{
					if (!skipFirstIndent)
					{
						utilWriter.AppendSpacesToLinePosition(linePosition);
					}
					skipFirstIndent = false;
					DeclarationUpdate declarationUpdate = declarationUpdates.GetDeclarationUpdate(configKey);
					if (declarationUpdate != null && !string.IsNullOrEmpty(declarationUpdate.UpdatedXml))
					{
						utilWriter.Write(declarationUpdate.UpdatedXml);
						utilWriter.AppendNewLine();
					}
				}
			}
			string[] unretrievedGroupNames = declarationUpdates.GetUnretrievedGroupNames();
			if (unretrievedGroupNames != null)
			{
				string[] array2 = unretrievedGroupNames;
				int j = 0;
				while (j < array2.Length)
				{
					string text = array2[j];
					if (!(base.TargetFramework != null))
					{
						goto IL_B3;
					}
					ConfigurationSectionGroup sectionGroup = this.GetSectionGroup(text);
					if (sectionGroup == null || sectionGroup.ShouldSerializeSectionGroupInTargetVersion(base.TargetFramework))
					{
						goto IL_B3;
					}
					declarationUpdates.MarkGroupAsRetrieved(text);
					IL_12C:
					j++;
					continue;
					IL_B3:
					if (!skipFirstIndent)
					{
						utilWriter.AppendSpacesToLinePosition(linePosition);
					}
					skipFirstIndent = false;
					SectionUpdates sectionUpdatesForGroup = declarationUpdates.GetSectionUpdatesForGroup(text);
					DeclarationUpdate sectionGroupUpdate = sectionUpdatesForGroup.GetSectionGroupUpdate();
					if (sectionGroupUpdate == null)
					{
						utilWriter.Write("<sectionGroup name=\"" + text + "\">");
					}
					else
					{
						utilWriter.Write(sectionGroupUpdate.UpdatedXml);
					}
					utilWriter.AppendNewLine();
					this.WriteUnwrittenConfigDeclarationsRecursive(sectionUpdatesForGroup, utilWriter, linePosition + indent, indent, false);
					utilWriter.AppendSpacesToLinePosition(linePosition);
					utilWriter.Write("</sectionGroup>\r\n");
					goto IL_12C;
				}
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00017694 File Offset: 0x00015894
		private void WriteNewConfigDefinitions(ConfigDefinitionUpdates configDefinitionUpdates, XmlUtilWriter utilWriter, int linePosition, int indent)
		{
			if (configDefinitionUpdates == null)
			{
				return;
			}
			foreach (object obj in configDefinitionUpdates.LocationUpdatesList)
			{
				LocationUpdates locationUpdates = (LocationUpdates)obj;
				SectionUpdates sectionUpdates = locationUpdates.SectionUpdates;
				if (!sectionUpdates.IsEmpty && sectionUpdates.IsNew)
				{
					configDefinitionUpdates.FlagLocationWritten();
					bool flag = this._locationSubPath != null || !locationUpdates.IsDefault;
					int num = linePosition;
					utilWriter.AppendSpacesToLinePosition(linePosition);
					if (flag)
					{
						if (this._locationSubPath == null)
						{
							utilWriter.Write(string.Format(CultureInfo.InvariantCulture, "<location {0} inheritInChildApplications=\"{1}\">\r\n", new object[]
							{
								locationUpdates.OverrideMode.LocationTagXmlString,
								MgmtConfigurationRecord.BoolToString(locationUpdates.InheritInChildApps)
							}));
						}
						else
						{
							utilWriter.Write(string.Format(CultureInfo.InvariantCulture, "<location path=\"{2}\" {0} inheritInChildApplications=\"{1}\">\r\n", new object[]
							{
								locationUpdates.OverrideMode.LocationTagXmlString,
								MgmtConfigurationRecord.BoolToString(locationUpdates.InheritInChildApps),
								this._locationSubPath
							}));
						}
						num += indent;
						utilWriter.AppendSpacesToLinePosition(num);
					}
					this.WriteNewConfigDefinitionsRecursive(utilWriter, locationUpdates.SectionUpdates, num, indent, true);
					if (flag)
					{
						utilWriter.AppendSpacesToLinePosition(linePosition);
						utilWriter.Write("</location>");
						utilWriter.AppendNewLine();
					}
				}
			}
			if (configDefinitionUpdates.RequireLocation)
			{
				configDefinitionUpdates.FlagLocationWritten();
				utilWriter.AppendSpacesToLinePosition(linePosition);
				utilWriter.Write(string.Format(CultureInfo.InvariantCulture, "<location path=\"{2}\" {0} inheritInChildApplications=\"{1}\">\r\n", new object[]
				{
					OverrideModeSetting.LocationDefault.LocationTagXmlString,
					"true",
					this._locationSubPath
				}));
				utilWriter.AppendSpacesToLinePosition(linePosition);
				utilWriter.Write("</location>");
				utilWriter.AppendNewLine();
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00017880 File Offset: 0x00015A80
		private bool WriteNewConfigDefinitionsRecursive(XmlUtilWriter utilWriter, SectionUpdates sectionUpdates, int linePosition, int indent, bool skipFirstIndent)
		{
			bool result = false;
			string[] movedSectionNames = sectionUpdates.GetMovedSectionNames();
			if (movedSectionNames != null)
			{
				result = true;
				foreach (string configKey in movedSectionNames)
				{
					DefinitionUpdate definitionUpdate = sectionUpdates.GetDefinitionUpdate(configKey);
					this.WriteSectionUpdate(utilWriter, definitionUpdate, linePosition, indent, skipFirstIndent);
					utilWriter.AppendNewLine();
					skipFirstIndent = false;
				}
			}
			string[] newGroupNames = sectionUpdates.GetNewGroupNames();
			if (newGroupNames != null)
			{
				string[] array2 = newGroupNames;
				int j = 0;
				while (j < array2.Length)
				{
					string text = array2[j];
					if (!(base.TargetFramework != null))
					{
						goto IL_9E;
					}
					ConfigurationSectionGroup sectionGroup = this.GetSectionGroup(text);
					if (sectionGroup == null || sectionGroup.ShouldSerializeSectionGroupInTargetVersion(base.TargetFramework))
					{
						goto IL_9E;
					}
					sectionUpdates.MarkGroupAsRetrieved(text);
					IL_103:
					j++;
					continue;
					IL_9E:
					if (!skipFirstIndent)
					{
						utilWriter.AppendSpacesToLinePosition(linePosition);
					}
					skipFirstIndent = false;
					utilWriter.Write("<" + text + ">\r\n");
					bool flag = this.WriteNewConfigDefinitionsRecursive(utilWriter, sectionUpdates.GetSectionUpdatesForGroup(text), linePosition + indent, indent, false);
					if (flag)
					{
						result = true;
					}
					utilWriter.AppendSpacesToLinePosition(linePosition);
					utilWriter.Write("</" + text + ">\r\n");
					goto IL_103;
				}
			}
			sectionUpdates.IsNew = false;
			return result;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000179AC File Offset: 0x00015BAC
		private void CheckPreamble(byte[] preamble, XmlUtilWriter utilWriter, byte[] buffer)
		{
			bool flag = false;
			using (Stream stream = new MemoryStream(buffer))
			{
				byte[] array = new byte[preamble.Length];
				if (stream.Read(array, 0, array.Length) == array.Length)
				{
					flag = true;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != preamble[i])
						{
							flag = false;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				object o = utilWriter.CreateStreamCheckpoint();
				utilWriter.Write('x');
				utilWriter.RestoreStreamCheckpoint(o);
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00017A30 File Offset: 0x00015C30
		private int UpdateIndent(int oldIndent, XmlUtil xmlUtil, XmlUtilWriter utilWriter, int parentLinePosition)
		{
			int result = oldIndent;
			if (xmlUtil.Reader.NodeType == XmlNodeType.Element && utilWriter.IsLastLineBlank)
			{
				int trueLinePosition = xmlUtil.TrueLinePosition;
				if (parentLinePosition < trueLinePosition && trueLinePosition <= parentLinePosition + 10)
				{
					result = trueLinePosition - parentLinePosition;
				}
			}
			return result;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00017A70 File Offset: 0x00015C70
		private void CopyConfig(SectionUpdates declarationUpdates, ConfigDefinitionUpdates definitionUpdates, byte[] buffer, string filename, NamespaceChange namespaceChange, XmlUtilWriter utilWriter)
		{
			this.CheckPreamble(base.ConfigStreamInfo.StreamEncoding.GetPreamble(), utilWriter, buffer);
			using (Stream stream = new MemoryStream(buffer))
			{
				using (XmlUtil xmlUtil = new XmlUtil(stream, filename, false))
				{
					XmlTextReader reader = xmlUtil.Reader;
					reader.WhitespaceHandling = WhitespaceHandling.All;
					reader.Read();
					xmlUtil.CopyReaderToNextElement(utilWriter, false);
					int num = 4;
					int trueLinePosition = xmlUtil.TrueLinePosition;
					bool isEmptyElement = reader.IsEmptyElement;
					string updatedStartElement;
					if (namespaceChange == NamespaceChange.Add)
					{
						updatedStartElement = string.Format(CultureInfo.InvariantCulture, "<configuration xmlns=\"{0}\">\r\n", new object[]
						{
							"http://schemas.microsoft.com/.NetConfiguration/v2.0"
						});
					}
					else if (namespaceChange == NamespaceChange.Remove)
					{
						updatedStartElement = "<configuration>\r\n";
					}
					else
					{
						updatedStartElement = null;
					}
					bool needsChildren = declarationUpdates != null || definitionUpdates != null;
					string text = xmlUtil.UpdateStartElement(utilWriter, updatedStartElement, needsChildren, trueLinePosition, num);
					bool flag = false;
					if (!isEmptyElement)
					{
						xmlUtil.CopyReaderToNextElement(utilWriter, true);
						num = this.UpdateIndent(num, xmlUtil, utilWriter, trueLinePosition);
						if (reader.NodeType == XmlNodeType.Element && reader.Name == "configSections")
						{
							flag = true;
							int trueLinePosition2 = xmlUtil.TrueLinePosition;
							bool isEmptyElement2 = reader.IsEmptyElement;
							if (declarationUpdates == null)
							{
								xmlUtil.CopyOuterXmlToNextElement(utilWriter, true);
							}
							else
							{
								string text2 = xmlUtil.UpdateStartElement(utilWriter, null, true, trueLinePosition2, num);
								if (!isEmptyElement2)
								{
									xmlUtil.CopyReaderToNextElement(utilWriter, true);
									this.CopyConfigDeclarationsRecursive(declarationUpdates, xmlUtil, utilWriter, string.Empty, trueLinePosition2, num);
								}
								if (declarationUpdates.HasUnretrievedSections())
								{
									int linePosition = 0;
									if (text2 == null)
									{
										linePosition = xmlUtil.TrueLinePosition;
									}
									if (!utilWriter.IsLastLineBlank)
									{
										utilWriter.AppendNewLine();
									}
									this.WriteUnwrittenConfigDeclarations(declarationUpdates, utilWriter, trueLinePosition2 + num, num, false);
									if (text2 == null)
									{
										utilWriter.AppendSpacesToLinePosition(linePosition);
									}
								}
								if (text2 == null)
								{
									xmlUtil.CopyXmlNode(utilWriter);
								}
								else
								{
									utilWriter.Write(text2);
								}
								xmlUtil.CopyReaderToNextElement(utilWriter, true);
							}
						}
					}
					if (!flag && declarationUpdates != null)
					{
						bool flag2 = reader.Depth > 0 && reader.NodeType == XmlNodeType.Element;
						int linePosition2;
						if (flag2)
						{
							linePosition2 = xmlUtil.TrueLinePosition;
						}
						else
						{
							linePosition2 = trueLinePosition + num;
						}
						this.WriteNewConfigDeclarations(declarationUpdates, utilWriter, linePosition2, num, flag2);
					}
					if (definitionUpdates != null)
					{
						bool locationPathApplies = false;
						LocationUpdates locationUpdates = null;
						SectionUpdates sectionUpdates = null;
						if (!base.IsLocationConfig)
						{
							locationPathApplies = true;
							locationUpdates = definitionUpdates.FindLocationUpdates(OverrideModeSetting.LocationDefault, true);
							if (locationUpdates != null)
							{
								sectionUpdates = locationUpdates.SectionUpdates;
							}
						}
						this.CopyConfigDefinitionsRecursive(definitionUpdates, xmlUtil, utilWriter, locationPathApplies, locationUpdates, sectionUpdates, true, string.Empty, trueLinePosition, num);
						this.WriteNewConfigDefinitions(definitionUpdates, utilWriter, trueLinePosition + num, num);
					}
					if (text != null)
					{
						if (!utilWriter.IsLastLineBlank)
						{
							utilWriter.AppendNewLine();
						}
						utilWriter.Write(text);
					}
					while (xmlUtil.CopyXmlNode(utilWriter))
					{
					}
				}
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00017D38 File Offset: 0x00015F38
		private bool CopyConfigDeclarationsRecursive(SectionUpdates declarationUpdates, XmlUtil xmlUtil, XmlUtilWriter utilWriter, string group, int parentLinePosition, int parentIndent)
		{
			bool result = false;
			XmlTextReader reader = xmlUtil.Reader;
			int num = this.UpdateIndent(parentIndent, xmlUtil, utilWriter, parentLinePosition);
			int num2;
			int linePosition;
			if (reader.NodeType == XmlNodeType.Element)
			{
				num2 = xmlUtil.TrueLinePosition;
				linePosition = num2;
			}
			else if (reader.NodeType == XmlNodeType.EndElement)
			{
				num2 = parentLinePosition + num;
				if (utilWriter.IsLastLineBlank)
				{
					linePosition = xmlUtil.TrueLinePosition;
				}
				else
				{
					linePosition = parentLinePosition;
				}
			}
			else
			{
				num2 = parentLinePosition + num;
				linePosition = 0;
			}
			if (declarationUpdates != null)
			{
				string[] movedSectionNames = declarationUpdates.GetMovedSectionNames();
				if (movedSectionNames != null)
				{
					if (!utilWriter.IsLastLineBlank)
					{
						utilWriter.AppendNewLine();
					}
					foreach (string configKey in movedSectionNames)
					{
						DeclarationUpdate declarationUpdate = declarationUpdates.GetDeclarationUpdate(configKey);
						utilWriter.AppendSpacesToLinePosition(num2);
						utilWriter.Write(declarationUpdate.UpdatedXml);
						utilWriter.AppendNewLine();
						result = true;
					}
					utilWriter.AppendSpacesToLinePosition(linePosition);
				}
			}
			if (reader.NodeType == XmlNodeType.Element)
			{
				int depth = reader.Depth;
				while (reader.Depth == depth)
				{
					bool flag = false;
					DeclarationUpdate declarationUpdate2 = null;
					DeclarationUpdate declarationUpdate3 = null;
					SectionUpdates declarationUpdates2 = declarationUpdates;
					string group2 = group;
					num = this.UpdateIndent(num, xmlUtil, utilWriter, parentLinePosition);
					num2 = xmlUtil.TrueLinePosition;
					string name = reader.Name;
					string attribute = reader.GetAttribute("name");
					string text = BaseConfigurationRecord.CombineConfigKey(group, attribute);
					if (name == "sectionGroup")
					{
						SectionUpdates sectionUpdatesForGroup = declarationUpdates.GetSectionUpdatesForGroup(attribute);
						if (sectionUpdatesForGroup != null)
						{
							declarationUpdate3 = sectionUpdatesForGroup.GetSectionGroupUpdate();
							if (sectionUpdatesForGroup.HasUnretrievedSections())
							{
								flag = true;
								group2 = text;
								declarationUpdates2 = sectionUpdatesForGroup;
							}
						}
					}
					else
					{
						declarationUpdate2 = declarationUpdates.GetDeclarationUpdate(text);
					}
					bool flag2 = declarationUpdate3 != null && declarationUpdate3.UpdatedXml != null;
					if (flag)
					{
						object o = utilWriter.CreateStreamCheckpoint();
						string text2 = null;
						if (flag2)
						{
							utilWriter.Write(declarationUpdate3.UpdatedXml);
							reader.Read();
						}
						else
						{
							text2 = xmlUtil.UpdateStartElement(utilWriter, null, true, num2, num);
						}
						if (text2 == null)
						{
							xmlUtil.CopyReaderToNextElement(utilWriter, true);
						}
						bool flag3 = this.CopyConfigDeclarationsRecursive(declarationUpdates2, xmlUtil, utilWriter, group2, num2, num);
						if (text2 != null)
						{
							utilWriter.AppendSpacesToLinePosition(num2);
							utilWriter.Write(text2);
							utilWriter.AppendSpacesToLinePosition(parentLinePosition);
						}
						else
						{
							xmlUtil.CopyXmlNode(utilWriter);
						}
						if (flag3 || flag2)
						{
							result = true;
						}
						else
						{
							utilWriter.RestoreStreamCheckpoint(o);
						}
						xmlUtil.CopyReaderToNextElement(utilWriter, true);
					}
					else
					{
						bool flag4 = false;
						bool flag5;
						if (declarationUpdate2 == null)
						{
							flag5 = true;
							if (flag2)
							{
								result = true;
								utilWriter.Write(declarationUpdate3.UpdatedXml);
								utilWriter.AppendNewLine();
								utilWriter.AppendSpacesToLinePosition(num2);
								utilWriter.Write("</sectionGroup>");
								utilWriter.AppendNewLine();
								utilWriter.AppendSpacesToLinePosition(num2);
							}
							else if (declarationUpdate3 != null)
							{
								result = true;
								flag5 = false;
								flag4 = true;
							}
						}
						else
						{
							result = true;
							if (declarationUpdate2.UpdatedXml == null)
							{
								flag5 = false;
							}
							else
							{
								flag5 = true;
								utilWriter.Write(declarationUpdate2.UpdatedXml);
							}
						}
						if (flag5)
						{
							xmlUtil.SkipAndCopyReaderToNextElement(utilWriter, true);
						}
						else if (flag4)
						{
							xmlUtil.SkipChildElementsAndCopyOuterXmlToNextElement(utilWriter);
						}
						else
						{
							xmlUtil.CopyOuterXmlToNextElement(utilWriter, true);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00018014 File Offset: 0x00016214
		private bool CopyConfigDefinitionsRecursive(ConfigDefinitionUpdates configDefinitionUpdates, XmlUtil xmlUtil, XmlUtilWriter utilWriter, bool locationPathApplies, LocationUpdates locationUpdates, SectionUpdates sectionUpdates, bool addNewSections, string group, int parentLinePosition, int parentIndent)
		{
			bool result = false;
			XmlTextReader reader = xmlUtil.Reader;
			int num = this.UpdateIndent(parentIndent, xmlUtil, utilWriter, parentLinePosition);
			int num2;
			int linePosition;
			if (reader.NodeType == XmlNodeType.Element)
			{
				num2 = xmlUtil.TrueLinePosition;
				linePosition = num2;
			}
			else if (reader.NodeType == XmlNodeType.EndElement)
			{
				num2 = parentLinePosition + num;
				if (utilWriter.IsLastLineBlank)
				{
					linePosition = xmlUtil.TrueLinePosition;
				}
				else
				{
					linePosition = parentLinePosition;
				}
			}
			else
			{
				num2 = parentLinePosition + num;
				linePosition = 0;
			}
			if (sectionUpdates != null && addNewSections)
			{
				sectionUpdates.IsNew = false;
				string[] movedSectionNames = sectionUpdates.GetMovedSectionNames();
				if (movedSectionNames != null)
				{
					if (!utilWriter.IsLastLineBlank)
					{
						utilWriter.AppendNewLine();
					}
					utilWriter.AppendSpacesToLinePosition(num2);
					bool skipFirstIndent = true;
					foreach (string configKey in movedSectionNames)
					{
						DefinitionUpdate definitionUpdate = sectionUpdates.GetDefinitionUpdate(configKey);
						this.WriteSectionUpdate(utilWriter, definitionUpdate, num2, num, skipFirstIndent);
						skipFirstIndent = false;
						utilWriter.AppendNewLine();
						result = true;
					}
					utilWriter.AppendSpacesToLinePosition(linePosition);
				}
			}
			if (reader.NodeType == XmlNodeType.Element)
			{
				int depth = reader.Depth;
				while (reader.Depth == depth)
				{
					bool flag = false;
					DefinitionUpdate definitionUpdate2 = null;
					bool flag2 = locationPathApplies;
					LocationUpdates locationUpdates2 = locationUpdates;
					SectionUpdates sectionUpdates2 = sectionUpdates;
					bool addNewSections2 = addNewSections;
					string group2 = group;
					bool flag3 = false;
					num = this.UpdateIndent(num, xmlUtil, utilWriter, parentLinePosition);
					num2 = xmlUtil.TrueLinePosition;
					string name = reader.Name;
					if (name == "location")
					{
						string text = reader.GetAttribute("path");
						text = BaseConfigurationRecord.NormalizeLocationSubPath(text, xmlUtil);
						OverrideModeSetting overrideMode = OverrideModeSetting.LocationDefault;
						bool inheritInChildApps = true;
						if (base.IsLocationConfig)
						{
							flag2 = (text != null && StringUtil.EqualsIgnoreCase(base.ConfigPath, base.Host.GetConfigPathFromLocationSubPath(base.Parent.ConfigPath, text)));
						}
						else
						{
							flag2 = (text == null);
						}
						if (flag2)
						{
							string attribute = reader.GetAttribute("allowOverride");
							if (attribute != null)
							{
								overrideMode = OverrideModeSetting.CreateFromXmlReadValue(bool.Parse(attribute));
							}
							string attribute2 = reader.GetAttribute("overrideMode");
							if (attribute2 != null)
							{
								overrideMode = OverrideModeSetting.CreateFromXmlReadValue(OverrideModeSetting.ParseOverrideModeXmlValue(attribute2, null));
							}
							string attribute3 = reader.GetAttribute("inheritInChildApplications");
							if (attribute3 != null)
							{
								inheritInChildApps = bool.Parse(attribute3);
							}
							configDefinitionUpdates.FlagLocationWritten();
						}
						if (reader.IsEmptyElement)
						{
							flag2 = (flag2 && configDefinitionUpdates.FindLocationUpdates(overrideMode, inheritInChildApps) != null);
						}
						else if (flag2)
						{
							if (configDefinitionUpdates != null)
							{
								locationUpdates2 = configDefinitionUpdates.FindLocationUpdates(overrideMode, inheritInChildApps);
								if (locationUpdates2 != null)
								{
									flag = true;
									sectionUpdates2 = locationUpdates2.SectionUpdates;
									if (this._locationSubPath == null && locationUpdates2.IsDefault)
									{
										addNewSections2 = false;
									}
								}
							}
						}
						else if (this.HasRemovedSectionsOrGroups && !base.IsLocationConfig && base.Host.SupportsLocation)
						{
							flag = true;
							locationUpdates2 = null;
							sectionUpdates2 = null;
							addNewSections2 = false;
						}
					}
					else
					{
						string text2 = BaseConfigurationRecord.CombineConfigKey(group, name);
						FactoryRecord factoryRecord = base.FindFactoryRecord(text2, false);
						if (factoryRecord == null)
						{
							if (!flag2 && !base.IsLocationConfig)
							{
								flag3 = true;
							}
						}
						else if (factoryRecord.IsGroup)
						{
							if (reader.IsEmptyElement)
							{
								if (!flag2 && !base.IsLocationConfig)
								{
									flag3 = true;
								}
							}
							else if (sectionUpdates != null)
							{
								SectionUpdates sectionUpdatesForGroup = sectionUpdates.GetSectionUpdatesForGroup(name);
								if (sectionUpdatesForGroup != null)
								{
									flag = true;
									group2 = text2;
									sectionUpdates2 = sectionUpdatesForGroup;
								}
							}
							else if (!flag2 && !base.IsLocationConfig)
							{
								if (this._removedSectionGroups != null && this._removedSectionGroups.Contains(text2))
								{
									flag3 = true;
								}
								else
								{
									flag = true;
									group2 = text2;
									locationUpdates2 = null;
									sectionUpdates2 = null;
									addNewSections2 = false;
								}
							}
						}
						else if (sectionUpdates != null)
						{
							definitionUpdate2 = sectionUpdates.GetDefinitionUpdate(text2);
						}
						else if (!flag2 && !base.IsLocationConfig && this._removedSections != null && this._removedSections.Contains(text2))
						{
							flag3 = true;
						}
					}
					if (flag)
					{
						object o = utilWriter.CreateStreamCheckpoint();
						xmlUtil.CopyXmlNode(utilWriter);
						xmlUtil.CopyReaderToNextElement(utilWriter, true);
						bool flag4 = this.CopyConfigDefinitionsRecursive(configDefinitionUpdates, xmlUtil, utilWriter, flag2, locationUpdates2, sectionUpdates2, addNewSections2, group2, num2, num);
						xmlUtil.CopyXmlNode(utilWriter);
						if (flag4)
						{
							result = true;
						}
						else
						{
							utilWriter.RestoreStreamCheckpoint(o);
						}
						xmlUtil.CopyReaderToNextElement(utilWriter, true);
					}
					else
					{
						bool flag5;
						if (definitionUpdate2 == null)
						{
							flag5 = (flag2 || flag3);
						}
						else
						{
							flag5 = false;
							if (definitionUpdate2.UpdatedXml != null)
							{
								ConfigurationSection configurationSection = (ConfigurationSection)definitionUpdate2.SectionRecord.Result;
								if (string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource) || configurationSection.SectionInformation.ConfigSourceModified)
								{
									flag5 = true;
									this.WriteSectionUpdate(utilWriter, definitionUpdate2, num2, num, true);
									result = true;
								}
							}
						}
						if (flag5)
						{
							xmlUtil.SkipAndCopyReaderToNextElement(utilWriter, true);
						}
						else
						{
							xmlUtil.CopyOuterXmlToNextElement(utilWriter, true);
							result = true;
						}
					}
				}
			}
			if (sectionUpdates != null && addNewSections && sectionUpdates.HasNewSectionGroups())
			{
				num2 = parentLinePosition + num;
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (utilWriter.IsLastLineBlank)
					{
						linePosition = xmlUtil.TrueLinePosition;
					}
					else
					{
						linePosition = parentLinePosition;
					}
				}
				else
				{
					linePosition = 0;
				}
				utilWriter.AppendSpacesToLinePosition(num2);
				bool flag6 = this.WriteNewConfigDefinitionsRecursive(utilWriter, sectionUpdates, num2, num, true);
				if (flag6)
				{
					result = true;
				}
				utilWriter.AppendSpacesToLinePosition(linePosition);
			}
			return result;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00018500 File Offset: 0x00016700
		private void WriteSectionUpdate(XmlUtilWriter utilWriter, DefinitionUpdate update, int linePosition, int indent, bool skipFirstIndent)
		{
			ConfigurationSection configurationSection = (ConfigurationSection)update.SectionRecord.Result;
			string xmlElement;
			if (!string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource))
			{
				xmlElement = string.Format(CultureInfo.InvariantCulture, "<{0} configSource=\"{1}\" />", new object[]
				{
					configurationSection.SectionInformation.Name,
					configurationSection.SectionInformation.ConfigSource
				});
			}
			else
			{
				xmlElement = update.UpdatedXml;
			}
			string s = XmlUtil.FormatXmlElement(xmlElement, linePosition, indent, skipFirstIndent);
			utilWriter.Write(s);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00018580 File Offset: 0x00016780
		private void SaveConfigSource(DefinitionUpdate update)
		{
			string configSourceStreamName;
			if (update.SectionRecord.HasResult)
			{
				ConfigurationSection configurationSection = (ConfigurationSection)update.SectionRecord.Result;
				configSourceStreamName = configurationSection.SectionInformation.ConfigSourceStreamName;
			}
			else
			{
				SectionInput fileInput = update.SectionRecord.FileInput;
				configSourceStreamName = fileInput.SectionXmlInfo.ConfigSourceStreamName;
			}
			byte[] array = null;
			using (Stream stream = base.Host.OpenStreamForRead(configSourceStreamName))
			{
				if (stream != null)
				{
					array = new byte[stream.Length];
					int num = stream.Read(array, 0, (int)stream.Length);
					if ((long)num != stream.Length)
					{
						throw new ConfigurationErrorsException();
					}
				}
			}
			bool flag = array != null;
			object writeContext = null;
			bool flag2 = false;
			try
			{
				try
				{
					string templateStreamName;
					if (base.Host.IsRemote)
					{
						templateStreamName = null;
					}
					else
					{
						templateStreamName = base.ConfigStreamInfo.StreamName;
					}
					using (Stream stream2 = base.Host.OpenStreamForWrite(configSourceStreamName, templateStreamName, ref writeContext))
					{
						flag2 = true;
						if (update.UpdatedXml == null)
						{
							if (flag)
							{
								stream2.Write(array, 0, array.Length);
							}
						}
						else
						{
							using (StreamWriter streamWriter = new StreamWriter(stream2))
							{
								XmlUtilWriter utilWriter = new XmlUtilWriter(streamWriter, true);
								if (flag)
								{
									this.CopyConfigSource(utilWriter, update.UpdatedXml, configSourceStreamName, array);
								}
								else
								{
									this.CreateNewConfigSource(utilWriter, update.UpdatedXml, 4);
								}
							}
						}
					}
				}
				catch
				{
					if (flag2)
					{
						base.Host.WriteCompleted(configSourceStreamName, false, writeContext);
					}
					throw;
				}
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_error_loading_XML_file"), e, configSourceStreamName, 0);
			}
			base.Host.WriteCompleted(configSourceStreamName, true, writeContext);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00018754 File Offset: 0x00016954
		private void CopyConfigSource(XmlUtilWriter utilWriter, string updatedXml, string configSourceStreamName, byte[] buffer)
		{
			byte[] preamble;
			using (Stream stream = new MemoryStream(buffer))
			{
				using (new XmlUtil(stream, configSourceStreamName, true))
				{
					preamble = base.ConfigStreamInfo.StreamEncoding.GetPreamble();
				}
			}
			this.CheckPreamble(preamble, utilWriter, buffer);
			using (Stream stream2 = new MemoryStream(buffer))
			{
				using (XmlUtil xmlUtil2 = new XmlUtil(stream2, configSourceStreamName, false))
				{
					XmlTextReader reader = xmlUtil2.Reader;
					reader.WhitespaceHandling = WhitespaceHandling.All;
					reader.Read();
					int indent = 4;
					int num = 1;
					bool flag = xmlUtil2.CopyReaderToNextElement(utilWriter, false);
					if (flag)
					{
						int lineNumber = reader.LineNumber;
						num = reader.LinePosition - 1;
						int num2 = 0;
						while (reader.MoveToNextAttribute())
						{
							if (reader.LineNumber > lineNumber)
							{
								num2 = reader.LinePosition - num;
								break;
							}
						}
						int num3 = 0;
						reader.Read();
						while (reader.Depth >= 1)
						{
							if (reader.NodeType == XmlNodeType.Element)
							{
								num3 = reader.LinePosition - 1 - num;
								break;
							}
							reader.Read();
						}
						if (num3 > 0)
						{
							indent = num3;
						}
						else if (num2 > 0)
						{
							indent = num2;
						}
					}
					string s = XmlUtil.FormatXmlElement(updatedXml, num, indent, true);
					utilWriter.Write(s);
					if (flag)
					{
						while (reader.Depth > 0)
						{
							reader.Read();
						}
						if (reader.IsEmptyElement || reader.NodeType == XmlNodeType.EndElement)
						{
							reader.Read();
						}
						while (xmlUtil2.CopyXmlNode(utilWriter))
						{
						}
					}
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00018940 File Offset: 0x00016B40
		private void CreateNewConfigSource(XmlUtilWriter utilWriter, string updatedXml, int indent)
		{
			string str = XmlUtil.FormatXmlElement(updatedXml, 0, indent, true);
			utilWriter.Write(string.Format(CultureInfo.InvariantCulture, "<?xml version=\"1.0\" encoding=\"{0}\"?>\r\n", new object[]
			{
				base.ConfigStreamInfo.StreamEncoding.WebName
			}));
			utilWriter.Write(str + "\r\n");
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00018998 File Offset: 0x00016B98
		private static string BoolToString(bool v)
		{
			if (!v)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000189A8 File Offset: 0x00016BA8
		internal void RemoveLocationWriteRequirement()
		{
			if (base.IsLocationConfig)
			{
				this._flags[16777216] = false;
				this._flags[33554432] = true;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000189D4 File Offset: 0x00016BD4
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x000189E6 File Offset: 0x00016BE6
		internal bool NamespacePresent
		{
			get
			{
				return this._flags[67108864];
			}
			set
			{
				this._flags[67108864] = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000189F9 File Offset: 0x00016BF9
		private NamespaceChange NamespaceChangeNeeded
		{
			get
			{
				if (this._flags[67108864] == this._flags[512])
				{
					return NamespaceChange.None;
				}
				if (this._flags[67108864])
				{
					return NamespaceChange.Add;
				}
				return NamespaceChange.Remove;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00018A34 File Offset: 0x00016C34
		private bool RecordItselfRequiresUpdates
		{
			get
			{
				return this.NamespaceChangeNeeded > NamespaceChange.None;
			}
		}

		// Token: 0x040002A3 RID: 675
		private const int DEFAULT_INDENT = 4;

		// Token: 0x040002A4 RID: 676
		private const int MAX_INDENT = 10;

		// Token: 0x040002A5 RID: 677
		private Hashtable _sectionGroups;

		// Token: 0x040002A6 RID: 678
		private Hashtable _sectionFactories;

		// Token: 0x040002A7 RID: 679
		private Hashtable _sectionGroupFactories;

		// Token: 0x040002A8 RID: 680
		private Hashtable _removedSections;

		// Token: 0x040002A9 RID: 681
		private Hashtable _removedSectionGroups;

		// Token: 0x040002AA RID: 682
		private Hashtable _locationTags;

		// Token: 0x040002AB RID: 683
		private HybridDictionary _streamInfoUpdates;

		// Token: 0x040002AC RID: 684
		private static readonly SimpleBitVector32 MgmtClassFlags = new SimpleBitVector32(80);
	}
}
