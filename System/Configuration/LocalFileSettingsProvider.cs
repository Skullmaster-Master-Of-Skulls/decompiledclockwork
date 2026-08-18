using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006FF RID: 1791
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class LocalFileSettingsProvider : SettingsProvider, IApplicationSettingsProvider
	{
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000EA7E0 File Offset: 0x000E97E0
		// (set) Token: 0x06003739 RID: 14137 RVA: 0x000EA7E8 File Offset: 0x000E97E8
		public override string ApplicationName
		{
			get
			{
				return this._appName;
			}
			set
			{
				this._appName = value;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x000EA7F1 File Offset: 0x000E97F1
		private LocalFileSettingsProvider.XmlEscaper Escaper
		{
			get
			{
				if (this._escaper == null)
				{
					this._escaper = new LocalFileSettingsProvider.XmlEscaper();
				}
				return this._escaper;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000EA80C File Offset: 0x000E980C
		private ClientSettingsStore Store
		{
			get
			{
				if (this._store == null)
				{
					this._store = new ClientSettingsStore();
				}
				return this._store;
			}
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000EA827 File Offset: 0x000E9827
		public override void Initialize(string name, NameValueCollection values)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "LocalFileSettingsProvider";
			}
			base.Initialize(name, values);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000EA840 File Offset: 0x000E9840
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
		{
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			string sectionName = this.GetSectionName(context);
			IDictionary dictionary = this.Store.ReadSettings(sectionName, false);
			IDictionary dictionary2 = this.Store.ReadSettings(sectionName, true);
			ConnectionStringSettingsCollection connectionStringSettingsCollection = this.Store.ReadConnectionStrings();
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				string name = settingsProperty.Name;
				SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
				SpecialSettingAttribute specialSettingAttribute = settingsProperty.Attributes[typeof(SpecialSettingAttribute)] as SpecialSettingAttribute;
				bool flag = specialSettingAttribute != null && specialSettingAttribute.SpecialSetting == SpecialSetting.ConnectionString;
				if (flag)
				{
					string name2 = sectionName + "." + name;
					if (connectionStringSettingsCollection != null && connectionStringSettingsCollection[name2] != null)
					{
						settingsPropertyValue.PropertyValue = connectionStringSettingsCollection[name2].ConnectionString;
					}
					else if (settingsProperty.DefaultValue != null && settingsProperty.DefaultValue is string)
					{
						settingsPropertyValue.PropertyValue = settingsProperty.DefaultValue;
					}
					else
					{
						settingsPropertyValue.PropertyValue = string.Empty;
					}
					settingsPropertyValue.IsDirty = false;
					settingsPropertyValueCollection.Add(settingsPropertyValue);
				}
				else
				{
					bool flag2 = this.IsUserSetting(settingsProperty);
					if (flag2 && !ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
					{
						throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
					}
					IDictionary dictionary3 = flag2 ? dictionary2 : dictionary;
					if (dictionary3.Contains(name))
					{
						StoredSetting storedSetting = (StoredSetting)dictionary3[name];
						string text = storedSetting.Value.InnerXml;
						if (storedSetting.SerializeAs == SettingsSerializeAs.String)
						{
							text = this.Escaper.Unescape(text);
						}
						settingsPropertyValue.SerializedValue = text;
					}
					else if (settingsProperty.DefaultValue != null)
					{
						settingsPropertyValue.SerializedValue = settingsProperty.DefaultValue;
					}
					else
					{
						settingsPropertyValue.PropertyValue = null;
					}
					settingsPropertyValue.IsDirty = false;
					settingsPropertyValueCollection.Add(settingsPropertyValue);
				}
			}
			return settingsPropertyValueCollection;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000EAA54 File Offset: 0x000E9A54
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
		{
			string sectionName = this.GetSectionName(context);
			IDictionary dictionary = new Hashtable();
			IDictionary dictionary2 = new Hashtable();
			foreach (object obj in values)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				SettingsProperty property = settingsPropertyValue.Property;
				bool flag = this.IsUserSetting(property);
				if (settingsPropertyValue.IsDirty && flag)
				{
					bool flag2 = LocalFileSettingsProvider.IsRoamingSetting(property);
					StoredSetting storedSetting = new StoredSetting(property.SerializeAs, this.SerializeToXmlElement(property, settingsPropertyValue));
					if (flag2)
					{
						dictionary[property.Name] = storedSetting;
					}
					else
					{
						dictionary2[property.Name] = storedSetting;
					}
					settingsPropertyValue.IsDirty = false;
				}
			}
			if (dictionary.Count > 0)
			{
				this.Store.WriteSettings(sectionName, true, dictionary);
			}
			if (dictionary2.Count > 0)
			{
				this.Store.WriteSettings(sectionName, false, dictionary2);
			}
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000EAB64 File Offset: 0x000E9B64
		public void Reset(SettingsContext context)
		{
			string sectionName = this.GetSectionName(context);
			this.Store.RevertToParent(sectionName, true);
			this.Store.RevertToParent(sectionName, false);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000EAB94 File Offset: 0x000E9B94
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			SettingsPropertyCollection settingsPropertyCollection2 = new SettingsPropertyCollection();
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				bool flag = LocalFileSettingsProvider.IsRoamingSetting(settingsProperty);
				if (flag)
				{
					settingsPropertyCollection2.Add(settingsProperty);
				}
				else
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			if (settingsPropertyCollection2.Count > 0)
			{
				this.Upgrade(context, settingsPropertyCollection2, true);
			}
			if (settingsPropertyCollection.Count > 0)
			{
				this.Upgrade(context, settingsPropertyCollection, false);
			}
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000EAC34 File Offset: 0x000E9C34
		private Version CreateVersion(string name)
		{
			Version result = null;
			try
			{
				result = new Version(name);
			}
			catch (ArgumentException)
			{
				result = null;
			}
			catch (OverflowException)
			{
				result = null;
			}
			catch (FormatException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000EAC84 File Offset: 0x000E9C84
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[FileIOPermission(SecurityAction.Assert, AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery))]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			bool isRoaming = LocalFileSettingsProvider.IsRoamingSetting(property);
			string previousConfigFileName = this.GetPreviousConfigFileName(isRoaming);
			if (!string.IsNullOrEmpty(previousConfigFileName))
			{
				SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
				settingsPropertyCollection.Add(property);
				SettingsPropertyValueCollection settingValuesFromFile = this.GetSettingValuesFromFile(previousConfigFileName, this.GetSectionName(context), true, settingsPropertyCollection);
				return settingValuesFromFile[property.Name];
			}
			return new SettingsPropertyValue(property)
			{
				PropertyValue = null
			};
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000EACE8 File Offset: 0x000E9CE8
		private string GetPreviousConfigFileName(bool isRoaming)
		{
			if (!ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
			}
			string text = isRoaming ? this._prevRoamingConfigFileName : this._prevLocalConfigFileName;
			if (string.IsNullOrEmpty(text))
			{
				string path = isRoaming ? ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigDirectory : ConfigurationManagerInternalFactory.Instance.ExeLocalConfigDirectory;
				Version version = this.CreateVersion(ConfigurationManagerInternalFactory.Instance.ExeProductVersion);
				Version version2 = null;
				DirectoryInfo directoryInfo = null;
				string text2 = null;
				if (version == null)
				{
					return null;
				}
				DirectoryInfo parent = Directory.GetParent(path);
				if (parent.Exists)
				{
					foreach (DirectoryInfo directoryInfo2 in parent.GetDirectories())
					{
						Version version3 = this.CreateVersion(directoryInfo2.Name);
						if (version3 != null && version3 < version)
						{
							if (version2 == null)
							{
								version2 = version3;
								directoryInfo = directoryInfo2;
							}
							else if (version3 > version2)
							{
								version2 = version3;
								directoryInfo = directoryInfo2;
							}
						}
					}
					if (directoryInfo != null)
					{
						text2 = Path.Combine(directoryInfo.FullName, ConfigurationManagerInternalFactory.Instance.UserConfigFilename);
					}
					if (File.Exists(text2))
					{
						text = text2;
					}
				}
				if (isRoaming)
				{
					this._prevRoamingConfigFileName = text;
				}
				else
				{
					this._prevLocalConfigFileName = text;
				}
			}
			return text;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000EAE28 File Offset: 0x000E9E28
		private string GetSectionName(SettingsContext context)
		{
			string text = (string)context["GroupName"];
			string text2 = (string)context["SettingsKey"];
			string text3 = text;
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					text3,
					text2
				});
			}
			return XmlConvert.EncodeLocalName(text3);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000EAE88 File Offset: 0x000E9E88
		private SettingsPropertyValueCollection GetSettingValuesFromFile(string configFileName, string sectionName, bool userScoped, SettingsPropertyCollection properties)
		{
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			IDictionary dictionary = ClientSettingsStore.ReadSettingsFromFile(configFileName, sectionName, userScoped);
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				string name = settingsProperty.Name;
				SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
				if (dictionary.Contains(name))
				{
					StoredSetting storedSetting = (StoredSetting)dictionary[name];
					string text = storedSetting.Value.InnerXml;
					if (storedSetting.SerializeAs == SettingsSerializeAs.String)
					{
						text = this.Escaper.Unescape(text);
					}
					settingsPropertyValue.SerializedValue = text;
					settingsPropertyValue.IsDirty = true;
					settingsPropertyValueCollection.Add(settingsPropertyValue);
				}
			}
			return settingsPropertyValueCollection;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000EAF54 File Offset: 0x000E9F54
		private static bool IsRoamingSetting(SettingsProperty setting)
		{
			bool flag = !ApplicationSettingsBase.IsClickOnceDeployed(AppDomain.CurrentDomain);
			bool result = false;
			if (flag)
			{
				SettingsManageabilityAttribute settingsManageabilityAttribute = setting.Attributes[typeof(SettingsManageabilityAttribute)] as SettingsManageabilityAttribute;
				bool flag2;
				if (settingsManageabilityAttribute != null)
				{
					SettingsManageability manageability = settingsManageabilityAttribute.Manageability;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000EAFA0 File Offset: 0x000E9FA0
		private bool IsUserSetting(SettingsProperty setting)
		{
			bool flag = setting.Attributes[typeof(UserScopedSettingAttribute)] is UserScopedSettingAttribute;
			bool flag2 = setting.Attributes[typeof(ApplicationScopedSettingAttribute)] is ApplicationScopedSettingAttribute;
			if (flag && flag2)
			{
				throw new ConfigurationErrorsException(SR.GetString("BothScopeAttributes"));
			}
			if (!flag && !flag2)
			{
				throw new ConfigurationErrorsException(SR.GetString("NoScopeAttributes"));
			}
			return flag;
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000EB018 File Offset: 0x000EA018
		private XmlNode SerializeToXmlElement(SettingsProperty setting, SettingsPropertyValue value)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("value");
			string text = value.SerializedValue as string;
			if (text == null && setting.SerializeAs == SettingsSerializeAs.Binary)
			{
				byte[] array = value.SerializedValue as byte[];
				if (array != null)
				{
					text = Convert.ToBase64String(array);
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (setting.SerializeAs == SettingsSerializeAs.String)
			{
				text = this.Escaper.Escape(text);
			}
			xmlElement.InnerXml = text;
			XmlNode xmlNode = null;
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.NodeType == XmlNodeType.XmlDeclaration)
				{
					xmlNode = xmlNode2;
					break;
				}
			}
			if (xmlNode != null)
			{
				xmlElement.RemoveChild(xmlNode);
			}
			return xmlElement;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000EB0FC File Offset: 0x000EA0FC
		[FileIOPermission(SecurityAction.Assert, AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery))]
		private void Upgrade(SettingsContext context, SettingsPropertyCollection properties, bool isRoaming)
		{
			string previousConfigFileName = this.GetPreviousConfigFileName(isRoaming);
			if (!string.IsNullOrEmpty(previousConfigFileName))
			{
				SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
				foreach (object obj in properties)
				{
					SettingsProperty settingsProperty = (SettingsProperty)obj;
					if (!(settingsProperty.Attributes[typeof(NoSettingsVersionUpgradeAttribute)] is NoSettingsVersionUpgradeAttribute))
					{
						settingsPropertyCollection.Add(settingsProperty);
					}
				}
				SettingsPropertyValueCollection settingValuesFromFile = this.GetSettingValuesFromFile(previousConfigFileName, this.GetSectionName(context), true, settingsPropertyCollection);
				this.SetPropertyValues(context, settingValuesFromFile);
			}
		}

		// Token: 0x040031C1 RID: 12737
		private string _appName = string.Empty;

		// Token: 0x040031C2 RID: 12738
		private ClientSettingsStore _store;

		// Token: 0x040031C3 RID: 12739
		private string _prevLocalConfigFileName;

		// Token: 0x040031C4 RID: 12740
		private string _prevRoamingConfigFileName;

		// Token: 0x040031C5 RID: 12741
		private LocalFileSettingsProvider.XmlEscaper _escaper;

		// Token: 0x02000700 RID: 1792
		private class XmlEscaper
		{
			// Token: 0x0600374B RID: 14155 RVA: 0x000EB1B7 File Offset: 0x000EA1B7
			internal XmlEscaper()
			{
				this.doc = new XmlDocument();
				this.temp = this.doc.CreateElement("temp");
			}

			// Token: 0x0600374C RID: 14156 RVA: 0x000EB1E0 File Offset: 0x000EA1E0
			internal string Escape(string xmlString)
			{
				if (string.IsNullOrEmpty(xmlString))
				{
					return xmlString;
				}
				this.temp.InnerText = xmlString;
				return this.temp.InnerXml;
			}

			// Token: 0x0600374D RID: 14157 RVA: 0x000EB203 File Offset: 0x000EA203
			internal string Unescape(string escapedString)
			{
				if (string.IsNullOrEmpty(escapedString))
				{
					return escapedString;
				}
				this.temp.InnerXml = escapedString;
				return this.temp.InnerText;
			}

			// Token: 0x040031C6 RID: 12742
			private XmlDocument doc;

			// Token: 0x040031C7 RID: 12743
			private XmlElement temp;
		}
	}
}
