using System;
using System.Configuration.Internal;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000085 RID: 133
	internal sealed class RuntimeConfigurationRecord : BaseConfigurationRecord
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0001A504 File Offset: 0x00018704
		internal static IInternalConfigRecord Create(InternalConfigRoot configRoot, IInternalConfigRecord parent, string configPath)
		{
			RuntimeConfigurationRecord runtimeConfigurationRecord = new RuntimeConfigurationRecord();
			runtimeConfigurationRecord.Init(configRoot, (BaseConfigurationRecord)parent, configPath, null);
			return runtimeConfigurationRecord;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001472A File Offset: 0x0001292A
		private RuntimeConfigurationRecord()
		{
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001A527 File Offset: 0x00018727
		protected override SimpleBitVector32 ClassFlags
		{
			get
			{
				return RuntimeConfigurationRecord.RuntimeClassFlags;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001A52E File Offset: 0x0001872E
		protected override object CreateSectionFactory(FactoryRecord factoryRecord)
		{
			return new RuntimeConfigurationRecord.RuntimeConfigurationFactory(this, factoryRecord);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001A538 File Offset: 0x00018738
		protected override object CreateSection(bool inputIsTrusted, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader)
		{
			RuntimeConfigurationRecord.RuntimeConfigurationFactory runtimeConfigurationFactory = (RuntimeConfigurationRecord.RuntimeConfigurationFactory)factoryRecord.Factory;
			return runtimeConfigurationFactory.CreateSection(inputIsTrusted, this, factoryRecord, sectionRecord, sectionInput, parentConfig, reader);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001A563 File Offset: 0x00018763
		protected override object UseParentResult(string configKey, object parentResult, SectionRecord sectionRecord)
		{
			return parentResult;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001A566 File Offset: 0x00018766
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private object GetRuntimeObjectWithFullTrust(ConfigurationSection section)
		{
			return section.GetRuntimeObject();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001A570 File Offset: 0x00018770
		private object GetRuntimeObjectWithRestrictedPermissions(ConfigurationSection section)
		{
			bool flag = false;
			object runtimeObject;
			try
			{
				PermissionSet restrictedPermissions = base.GetRestrictedPermissions();
				if (restrictedPermissions != null)
				{
					restrictedPermissions.PermitOnly();
					flag = true;
				}
				runtimeObject = section.GetRuntimeObject();
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertPermitOnly();
				}
			}
			return runtimeObject;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001A5B4 File Offset: 0x000187B4
		protected override object GetRuntimeObject(object result)
		{
			ConfigurationSection configurationSection = result as ConfigurationSection;
			object result2;
			if (configurationSection == null)
			{
				result2 = result;
			}
			else
			{
				try
				{
					using (base.Impersonate())
					{
						if (this._flags[8192])
						{
							result2 = this.GetRuntimeObjectWithFullTrust(configurationSection);
						}
						else
						{
							result2 = this.GetRuntimeObjectWithRestrictedPermissions(configurationSection);
						}
					}
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_exception_in_config_section_handler", new object[]
					{
						configurationSection.SectionInformation.SectionName
					}), inner);
				}
			}
			return result2;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001A64C File Offset: 0x0001884C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		protected override string CallHostDecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfig)
		{
			return base.CallHostDecryptSection(encryptedXml, protectionProvider, protectedConfig);
		}

		// Token: 0x040002E8 RID: 744
		private static readonly SimpleBitVector32 RuntimeClassFlags = new SimpleBitVector32(47);

		// Token: 0x020000D7 RID: 215
		private class RuntimeConfigurationFactory
		{
			// Token: 0x060007FD RID: 2045 RVA: 0x00020D4D File Offset: 0x0001EF4D
			internal RuntimeConfigurationFactory(RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord)
			{
				if (factoryRecord.IsFromTrustedConfigRecord)
				{
					this.InitWithFullTrust(configRecord, factoryRecord);
					return;
				}
				this.InitWithRestrictedPermissions(configRecord, factoryRecord);
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x00020D70 File Offset: 0x0001EF70
			private void Init(RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord)
			{
				Type typeWithReflectionPermission = TypeUtil.GetTypeWithReflectionPermission(configRecord.Host, factoryRecord.FactoryTypeName, true);
				if (typeof(ConfigurationSection).IsAssignableFrom(typeWithReflectionPermission))
				{
					this._sectionCtor = TypeUtil.GetConstructorWithReflectionPermission(typeWithReflectionPermission, typeof(ConfigurationSection), true);
					return;
				}
				TypeUtil.VerifyAssignableType(typeof(IConfigurationSectionHandler), typeWithReflectionPermission, true);
				this._sectionHandler = (IConfigurationSectionHandler)TypeUtil.CreateInstanceWithReflectionPermission(typeWithReflectionPermission);
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00020DDD File Offset: 0x0001EFDD
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			private void InitWithFullTrust(RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord)
			{
				this.Init(configRecord, factoryRecord);
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00020DE8 File Offset: 0x0001EFE8
			private void InitWithRestrictedPermissions(RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord)
			{
				bool flag = false;
				try
				{
					PermissionSet restrictedPermissions = configRecord.GetRestrictedPermissions();
					if (restrictedPermissions != null)
					{
						restrictedPermissions.PermitOnly();
						flag = true;
					}
					this.Init(configRecord, factoryRecord);
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertPermitOnly();
					}
				}
			}

			// Token: 0x06000801 RID: 2049 RVA: 0x00020E2C File Offset: 0x0001F02C
			private static void CheckForLockAttributes(string sectionName, XmlNode xmlNode)
			{
				XmlAttributeCollection attributes = xmlNode.Attributes;
				if (attributes != null)
				{
					foreach (object obj in attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						if (ConfigurationElement.IsLockAttributeName(xmlAttribute.Name))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_element_locking_not_supported", new object[]
							{
								sectionName
							}), xmlAttribute);
						}
					}
				}
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						RuntimeConfigurationRecord.RuntimeConfigurationFactory.CheckForLockAttributes(sectionName, xmlNode2);
					}
				}
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x00020F04 File Offset: 0x0001F104
			private object CreateSectionImpl(RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader)
			{
				object result;
				if (this._sectionCtor != null)
				{
					ConfigurationSection configurationSection = (ConfigurationSection)TypeUtil.InvokeCtorWithReflectionPermission(this._sectionCtor);
					configurationSection.SectionInformation.SetRuntimeConfigurationInformation(configRecord, factoryRecord, sectionRecord);
					configurationSection.CallInit();
					ConfigurationSection parentElement = (ConfigurationSection)parentConfig;
					configurationSection.Reset(parentElement);
					if (reader != null)
					{
						configurationSection.DeserializeSection(reader);
					}
					if (configRecord != null && sectionInput != null && sectionInput.ConfigBuilder != null)
					{
						configurationSection = configRecord.CallHostProcessConfigurationSection(configurationSection, sectionInput.ConfigBuilder);
					}
					ConfigurationErrorsException errors = configurationSection.GetErrors();
					if (errors != null)
					{
						throw errors;
					}
					configurationSection.SetReadOnly();
					configurationSection.ResetModified();
					result = configurationSection;
				}
				else if (reader != null)
				{
					XmlNode xmlNode = ErrorInfoXmlDocument.CreateSectionXmlNode(reader);
					RuntimeConfigurationRecord.RuntimeConfigurationFactory.CheckForLockAttributes(factoryRecord.ConfigKey, xmlNode);
					object configContext = configRecord.Host.CreateDeprecatedConfigContext(configRecord.ConfigPath);
					result = this._sectionHandler.Create(parentConfig, configContext, xmlNode);
				}
				else
				{
					result = null;
				}
				return result;
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x00020FDE File Offset: 0x0001F1DE
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			private object CreateSectionWithFullTrust(RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader)
			{
				return this.CreateSectionImpl(configRecord, factoryRecord, sectionRecord, sectionInput, parentConfig, reader);
			}

			// Token: 0x06000804 RID: 2052 RVA: 0x00020FF0 File Offset: 0x0001F1F0
			private object CreateSectionWithRestrictedPermissions(RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader)
			{
				bool flag = false;
				object result;
				try
				{
					PermissionSet restrictedPermissions = configRecord.GetRestrictedPermissions();
					if (restrictedPermissions != null)
					{
						restrictedPermissions.PermitOnly();
						flag = true;
					}
					result = this.CreateSectionImpl(configRecord, factoryRecord, sectionRecord, sectionInput, parentConfig, reader);
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertPermitOnly();
					}
				}
				return result;
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x00021040 File Offset: 0x0001F240
			internal object CreateSection(bool inputIsTrusted, RuntimeConfigurationRecord configRecord, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader)
			{
				if (inputIsTrusted)
				{
					return this.CreateSectionWithFullTrust(configRecord, factoryRecord, sectionRecord, sectionInput, parentConfig, reader);
				}
				return this.CreateSectionWithRestrictedPermissions(configRecord, factoryRecord, sectionRecord, sectionInput, parentConfig, reader);
			}

			// Token: 0x040004AE RID: 1198
			private ConstructorInfo _sectionCtor;

			// Token: 0x040004AF RID: 1199
			private IConfigurationSectionHandler _sectionHandler;
		}
	}
}
