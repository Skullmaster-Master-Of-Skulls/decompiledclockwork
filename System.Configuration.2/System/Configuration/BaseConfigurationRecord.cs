using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Internal;
using System.Diagnostics;
using System.IO;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000012 RID: 18
	[DebuggerDisplay("ConfigPath = {ConfigPath}")]
	internal abstract class BaseConfigurationRecord : IInternalConfigRecord
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002884 File Offset: 0x00000A84
		internal BaseConfigurationRecord()
		{
			this._flags = default(SafeBitVector32);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51
		protected abstract SimpleBitVector32 ClassFlags { get; }

		// Token: 0x06000034 RID: 52
		protected abstract object CreateSectionFactory(FactoryRecord factoryRecord);

		// Token: 0x06000035 RID: 53
		protected abstract object CreateSection(bool inputIsTrusted, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader);

		// Token: 0x06000036 RID: 54
		protected abstract object UseParentResult(string configKey, object parentResult, SectionRecord sectionRecord);

		// Token: 0x06000037 RID: 55
		protected abstract object GetRuntimeObject(object result);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000028A8 File Offset: 0x00000AA8
		public string ConfigPath
		{
			get
			{
				return this._configPath;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000028B0 File Offset: 0x00000AB0
		public string StreamName
		{
			get
			{
				return this.ConfigStreamInfo.StreamName;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028C0 File Offset: 0x00000AC0
		public bool HasInitErrors
		{
			get
			{
				return this._initErrors.HasErrors(this.ClassFlags[64]);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void ThrowIfInitErrors()
		{
			this.ThrowIfParseErrors(this._initErrors);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028F6 File Offset: 0x00000AF6
		public object GetSection(string configKey)
		{
			return this.GetSection(configKey, false, true);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002901 File Offset: 0x00000B01
		public object GetLkgSection(string configKey)
		{
			return this.GetSection(configKey, true, true);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000290C File Offset: 0x00000B0C
		public void RefreshSection(string configKey)
		{
			this._configRoot.ClearResult(this, configKey, true);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000291C File Offset: 0x00000B1C
		public void Remove()
		{
			this._configRoot.RemoveConfigRecord(this);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000292A File Offset: 0x00000B2A
		internal bool HasStream
		{
			get
			{
				return this.ConfigStreamInfo.HasStream;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002938 File Offset: 0x00000B38
		private bool ShouldPrefetchRawXml(FactoryRecord factoryRecord)
		{
			if (this._flags[8])
			{
				return true;
			}
			string configKey = factoryRecord.ConfigKey;
			return configKey == "configProtectedData" || configKey == "system.diagnostics" || configKey == "appSettings" || configKey == "connectionStrings" || this.Host.PrefetchSection(factoryRecord.Group, factoryRecord.Name);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029AC File Offset: 0x00000BAC
		protected IDisposable Impersonate()
		{
			IDisposable disposable = null;
			if (this.ClassFlags[4])
			{
				disposable = this.Host.Impersonate();
			}
			if (disposable == null)
			{
				disposable = EmptyImpersonationContext.GetStaticInstance();
			}
			return disposable;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal PermissionSet GetRestrictedPermissions()
		{
			if (!this._flags[2048])
			{
				lock (this)
				{
					if (!this._flags[2048])
					{
						if (AppDomain.CurrentDomain.IsHomogenous)
						{
							this._restrictedPermissions = AppDomain.CurrentDomain.PermissionSet;
							this._flags[2048] = true;
						}
						else
						{
							PermissionSet restrictedPermissions;
							bool flag2;
							this.GetRestrictedPermissionsWithAssert(out restrictedPermissions, out flag2);
							if (flag2)
							{
								this._restrictedPermissions = restrictedPermissions;
								this._flags[2048] = true;
							}
						}
						if (this._restrictedPermissions != null && this._restrictedPermissions.IsUnrestricted())
						{
							this._restrictedPermissions = null;
						}
					}
				}
			}
			return this._restrictedPermissions;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AB8 File Offset: 0x00000CB8
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void GetRestrictedPermissionsWithAssert(out PermissionSet permissionSet, out bool isHostReady)
		{
			this.Host.GetRestrictedPermissions(this, out permissionSet, out isHostReady);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AC8 File Offset: 0x00000CC8
		internal void Init(IInternalConfigRoot configRoot, BaseConfigurationRecord parent, string configPath, string locationSubPath)
		{
			this._initErrors = new ConfigurationSchemaErrors();
			try
			{
				this._configRoot = (InternalConfigRoot)configRoot;
				this._parent = parent;
				this._configPath = configPath;
				this._locationSubPath = locationSubPath;
				this._configName = ConfigPathUtility.GetName(configPath);
				if (this.IsLocationConfig)
				{
					this._configStreamInfo = this._parent.ConfigStreamInfo;
				}
				else
				{
					this._configStreamInfo = new BaseConfigurationRecord.ConfigRecordStreamInfo();
				}
				if (!this.IsRootConfig)
				{
					this._flags[65536] = (this.ClassFlags[1] && this.Host.SupportsChangeNotifications);
					this._flags[131072] = (this.ClassFlags[2] && this.Host.SupportsRefresh);
					this._flags[524288] = (this.ClassFlags[16] || this._flags[131072]);
					this._flags[262144] = this.Host.SupportsPath;
					this._flags[1048576] = this.Host.SupportsLocation;
					if (this._flags[1048576])
					{
						this._flags[32] = this.Host.IsAboveApplication(this._configPath);
					}
					this._flags[8192] = this.Host.IsTrustedConfigPath(this._configPath);
					ArrayList arrayList = null;
					if (this._flags[1048576])
					{
						if (this.IsLocationConfig && this._parent._locationSections != null)
						{
							this._parent.ResolveLocationSections();
							int i = 0;
							while (i < this._parent._locationSections.Count)
							{
								LocationSectionRecord locationSectionRecord = (LocationSectionRecord)this._parent._locationSections[i];
								if (!StringUtil.EqualsIgnoreCase(locationSectionRecord.SectionXmlInfo.TargetConfigPath, this.ConfigPath))
								{
									i++;
								}
								else
								{
									this._parent._locationSections.RemoveAt(i);
									if (arrayList == null)
									{
										arrayList = new ArrayList();
									}
									arrayList.Add(locationSectionRecord);
								}
							}
						}
						if (this.IsLocationConfig && this.Host.IsLocationApplicable(this._configPath))
						{
							Dictionary<string, List<SectionInput>> dictionary = null;
							BaseConfigurationRecord parent2 = this._parent;
							while (!parent2.IsRootConfig)
							{
								if (parent2._locationSections != null)
								{
									parent2.ResolveLocationSections();
									foreach (object obj in parent2._locationSections)
									{
										LocationSectionRecord locationSectionRecord2 = (LocationSectionRecord)obj;
										if (this.IsLocationConfig && UrlPath.IsSubpath(locationSectionRecord2.SectionXmlInfo.TargetConfigPath, this.ConfigPath) && UrlPath.IsSubpath(parent.ConfigPath, locationSectionRecord2.SectionXmlInfo.TargetConfigPath) && !this.ShouldSkipDueToInheritInChildApplications(locationSectionRecord2.SectionXmlInfo.SkipInChildApps, locationSectionRecord2.SectionXmlInfo.TargetConfigPath))
										{
											if (dictionary == null)
											{
												dictionary = new Dictionary<string, List<SectionInput>>(1);
											}
											string configKey = locationSectionRecord2.SectionXmlInfo.ConfigKey;
											if (!((IDictionary)dictionary).Contains(configKey))
											{
												dictionary.Add(configKey, new List<SectionInput>(1));
											}
											dictionary[configKey].Add(new SectionInput(locationSectionRecord2.SectionXmlInfo, locationSectionRecord2.ErrorsList));
											if (locationSectionRecord2.HasErrors)
											{
												this._initErrors.AddSavedLocalErrors(locationSectionRecord2.Errors);
											}
										}
									}
								}
								parent2 = parent2._parent;
							}
							if (dictionary != null)
							{
								foreach (KeyValuePair<string, List<SectionInput>> keyValuePair in dictionary)
								{
									List<SectionInput> value = keyValuePair.Value;
									string key = keyValuePair.Key;
									value.Sort(BaseConfigurationRecord.s_indirectInputsComparer);
									SectionRecord sectionRecord = this.EnsureSectionRecord(key, true);
									foreach (SectionInput sectionInput in value)
									{
										sectionRecord.AddIndirectLocationInput(sectionInput);
									}
								}
							}
						}
						if (this.Host.IsLocationApplicable(this._configPath))
						{
							BaseConfigurationRecord parent3 = this._parent;
							while (!parent3.IsRootConfig)
							{
								if (parent3._locationSections != null)
								{
									parent3.ResolveLocationSections();
									foreach (object obj2 in parent3._locationSections)
									{
										LocationSectionRecord locationSectionRecord3 = (LocationSectionRecord)obj2;
										if (StringUtil.EqualsIgnoreCase(locationSectionRecord3.SectionXmlInfo.TargetConfigPath, this._configPath) && !this.ShouldSkipDueToInheritInChildApplications(locationSectionRecord3.SectionXmlInfo.SkipInChildApps))
										{
											SectionRecord sectionRecord2 = this.EnsureSectionRecord(locationSectionRecord3.ConfigKey, true);
											SectionInput sectionInput2 = new SectionInput(locationSectionRecord3.SectionXmlInfo, locationSectionRecord3.ErrorsList);
											sectionRecord2.AddLocationInput(sectionInput2);
											if (locationSectionRecord3.HasErrors)
											{
												this._initErrors.AddSavedLocalErrors(locationSectionRecord3.Errors);
											}
										}
									}
								}
								parent3 = parent3._parent;
							}
						}
					}
					if (!this.IsLocationConfig)
					{
						this.InitConfigFromFile();
					}
					else if (arrayList != null)
					{
						foreach (object obj3 in arrayList)
						{
							LocationSectionRecord locationSectionRecord4 = (LocationSectionRecord)obj3;
							SectionRecord sectionRecord3 = this.EnsureSectionRecord(locationSectionRecord4.ConfigKey, true);
							SectionInput sectionInput3 = new SectionInput(locationSectionRecord4.SectionXmlInfo, locationSectionRecord4.ErrorsList);
							sectionRecord3.AddFileInput(sectionInput3);
							if (locationSectionRecord4.HasErrors)
							{
								this._initErrors.AddSavedLocalErrors(locationSectionRecord4.Errors);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				string filename = (this.ConfigStreamInfo != null) ? this.ConfigStreamInfo.StreamName : null;
				this._initErrors.AddError(ExceptionUtil.WrapAsConfigException(SR.GetString("Config_error_loading_XML_file"), e, filename, 0), ExceptionAction.Global);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003178 File Offset: 0x00001378
		private void InitConfigFromFile()
		{
			bool flag = false;
			try
			{
				if (this.ClassFlags[32] && this.Host.IsInitDelayed(this))
				{
					if (this._parent._initDelayedRoot == null)
					{
						this._initDelayedRoot = this;
					}
					else
					{
						this._initDelayedRoot = this._parent._initDelayedRoot;
					}
				}
				else
				{
					using (this.Impersonate())
					{
						this.ConfigStreamInfo.StreamName = this.Host.GetStreamName(this._configPath);
						if (!string.IsNullOrEmpty(this.ConfigStreamInfo.StreamName))
						{
							this.ConfigStreamInfo.StreamVersion = this.MonitorStream(null, null, this.ConfigStreamInfo.StreamName);
							using (Stream stream = this.Host.OpenStreamForRead(this.ConfigStreamInfo.StreamName))
							{
								if (stream == null)
								{
									return;
								}
								this.ConfigStreamInfo.HasStream = true;
								this._flags[8] = this.Host.PrefetchAll(this._configPath, this.ConfigStreamInfo.StreamName);
								using (XmlUtil xmlUtil = new XmlUtil(stream, this.ConfigStreamInfo.StreamName, true, this._initErrors))
								{
									this.ConfigStreamInfo.StreamEncoding = xmlUtil.Reader.Encoding;
									Hashtable factoryRecords = this.ScanFactories(xmlUtil);
									this._factoryRecords = factoryRecords;
									this.AddImplicitSections(null);
									flag = true;
									if (xmlUtil.Reader.Depth == 1)
									{
										this.ScanSections(xmlUtil);
									}
								}
							}
						}
					}
				}
			}
			catch (XmlException e)
			{
				this._initErrors.SetSingleGlobalError(ExceptionUtil.WrapAsConfigException(SR.GetString("Config_error_loading_XML_file"), e, this.ConfigStreamInfo.StreamName, 0));
			}
			catch (Exception e2)
			{
				this._initErrors.AddError(ExceptionUtil.WrapAsConfigException(SR.GetString("Config_error_loading_XML_file"), e2, this.ConfigStreamInfo.StreamName, 0), ExceptionAction.Global);
			}
			if (this._initErrors.HasGlobalErrors)
			{
				this._initErrors.ResetLocalErrors();
				HybridDictionary hybridDictionary = null;
				lock (this)
				{
					if (this.ConfigStreamInfo.HasStreamInfos)
					{
						hybridDictionary = this.ConfigStreamInfo.StreamInfos;
						this.ConfigStreamInfo.ClearStreamInfos();
						if (!string.IsNullOrEmpty(this.ConfigStreamInfo.StreamName))
						{
							StreamInfo streamInfo = (StreamInfo)hybridDictionary[this.ConfigStreamInfo.StreamName];
							if (streamInfo != null)
							{
								hybridDictionary.Remove(this.ConfigStreamInfo.StreamName);
								this.ConfigStreamInfo.StreamInfos.Add(this.ConfigStreamInfo.StreamName, streamInfo);
							}
						}
					}
				}
				if (hybridDictionary != null)
				{
					foreach (object obj in hybridDictionary.Values)
					{
						StreamInfo streamInfo2 = (StreamInfo)obj;
						if (streamInfo2.IsMonitored)
						{
							this.Host.StopMonitoringStreamForChanges(streamInfo2.StreamName, this.ConfigStreamInfo.CallbackDelegate);
						}
					}
				}
				if (this._sectionRecords != null)
				{
					List<SectionRecord> list = null;
					foreach (object obj2 in this._sectionRecords.Values)
					{
						SectionRecord sectionRecord = (SectionRecord)obj2;
						if (sectionRecord.HasLocationInputs)
						{
							sectionRecord.RemoveFileInput();
						}
						else
						{
							if (list == null)
							{
								list = new List<SectionRecord>();
							}
							list.Add(sectionRecord);
						}
					}
					if (list != null)
					{
						foreach (SectionRecord sectionRecord2 in list)
						{
							this._sectionRecords.Remove(sectionRecord2.ConfigKey);
						}
					}
				}
				if (this._locationSections != null)
				{
					this._locationSections.Clear();
				}
				if (this._factoryRecords != null)
				{
					this._factoryRecords.Clear();
				}
			}
			if (!flag)
			{
				this.AddImplicitSections(null);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003650 File Offset: 0x00001850
		private bool IsInitDelayed
		{
			get
			{
				return this._initDelayedRoot != null;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000365C File Offset: 0x0000185C
		private void RefreshFactoryRecord(string configKey)
		{
			Hashtable hashtable = null;
			FactoryRecord factoryRecord = null;
			ConfigurationSchemaErrors configurationSchemaErrors = new ConfigurationSchemaErrors();
			int line = 0;
			try
			{
				using (this.Impersonate())
				{
					using (Stream stream = this.Host.OpenStreamForRead(this.ConfigStreamInfo.StreamName))
					{
						if (stream != null)
						{
							this.ConfigStreamInfo.HasStream = true;
							using (XmlUtil xmlUtil = new XmlUtil(stream, this.ConfigStreamInfo.StreamName, true, configurationSchemaErrors))
							{
								try
								{
									hashtable = this.ScanFactories(xmlUtil);
									this.ThrowIfParseErrors(xmlUtil.SchemaErrors);
								}
								catch
								{
									line = xmlUtil.LineNumber;
									throw;
								}
							}
						}
					}
				}
				if (hashtable == null)
				{
					hashtable = new Hashtable();
				}
				this.AddImplicitSections(hashtable);
				if (hashtable != null)
				{
					factoryRecord = (FactoryRecord)hashtable[configKey];
				}
			}
			catch (Exception e)
			{
				configurationSchemaErrors.AddError(ExceptionUtil.WrapAsConfigException(SR.GetString("Config_error_loading_XML_file"), e, this.ConfigStreamInfo.StreamName, line), ExceptionAction.Global);
			}
			if (factoryRecord != null || this.HasFactoryRecords)
			{
				this.EnsureFactories()[configKey] = factoryRecord;
			}
			this.ThrowIfParseErrors(configurationSchemaErrors);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000037B4 File Offset: 0x000019B4
		internal IInternalConfigHost Host
		{
			get
			{
				return this._configRoot.Host;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000037C1 File Offset: 0x000019C1
		internal IInternalConfigurationBuilderHost ConfigBuilderHost
		{
			get
			{
				return this._configRoot.ConfigBuilderHost;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000037CE File Offset: 0x000019CE
		internal BaseConfigurationRecord Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000037D6 File Offset: 0x000019D6
		internal bool IsRootConfig
		{
			get
			{
				return this._parent == null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000037E1 File Offset: 0x000019E1
		internal bool IsMachineConfig
		{
			get
			{
				return this._parent == this._configRoot.RootConfigRecord;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000037F6 File Offset: 0x000019F6
		internal string LocationSubPath
		{
			get
			{
				return this._locationSubPath;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000037FE File Offset: 0x000019FE
		internal bool IsLocationConfig
		{
			get
			{
				return this._locationSubPath != null;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003809 File Offset: 0x00001A09
		protected BaseConfigurationRecord.ConfigRecordStreamInfo ConfigStreamInfo
		{
			get
			{
				if (this.IsLocationConfig)
				{
					return this._parent._configStreamInfo;
				}
				return this._configStreamInfo;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003828 File Offset: 0x00001A28
		private object GetSection(string configKey, bool getLkg, bool checkPermission)
		{
			object obj;
			object result;
			this.GetSectionRecursive(configKey, getLkg, checkPermission, true, true, out obj, out result);
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003848 File Offset: 0x00001A48
		private void GetSectionRecursive(string configKey, bool getLkg, bool checkPermission, bool getRuntimeObject, bool requestIsHere, out object result, out object resultRuntimeObject)
		{
			result = null;
			resultRuntimeObject = null;
			object obj = null;
			object obj2 = null;
			bool requirePermission = true;
			bool flag = true;
			if (!getLkg)
			{
				this.ThrowIfInitErrors();
			}
			bool flag2 = false;
			SectionRecord sectionRecord = this.GetSectionRecord(configKey, getLkg);
			if (sectionRecord != null && sectionRecord.HasResult)
			{
				if (getRuntimeObject && !sectionRecord.HasResultRuntimeObject)
				{
					try
					{
						sectionRecord.ResultRuntimeObject = this.GetRuntimeObject(sectionRecord.Result);
					}
					catch
					{
						if (!getLkg)
						{
							throw;
						}
					}
				}
				if (!getRuntimeObject || sectionRecord.HasResultRuntimeObject)
				{
					requirePermission = sectionRecord.RequirePermission;
					flag = sectionRecord.IsResultTrustedWithoutAptca;
					obj = sectionRecord.Result;
					if (getRuntimeObject)
					{
						obj2 = sectionRecord.ResultRuntimeObject;
					}
					flag2 = true;
				}
			}
			if (!flag2)
			{
				bool flag3 = sectionRecord != null && sectionRecord.HasInput;
				bool flag4 = requestIsHere || flag3;
				try
				{
					bool flag5;
					FactoryRecord factoryRecord;
					if (requestIsHere)
					{
						factoryRecord = this.FindAndEnsureFactoryRecord(configKey, out flag5);
						if (this.IsInitDelayed && (factoryRecord == null || this._initDelayedRoot.IsDefinitionAllowed(factoryRecord.AllowDefinition, factoryRecord.AllowExeDefinition)))
						{
							if (factoryRecord == null && BaseConfigurationRecord.NeverLoadUserConfigFilesDuringFactorySearch(configKey))
							{
								return;
							}
							string configPath = this._configPath;
							InternalConfigRoot configRoot = this._configRoot;
							this.Host.RequireCompleteInit(this._initDelayedRoot);
							this._initDelayedRoot.Remove();
							BaseConfigurationRecord baseConfigurationRecord = (BaseConfigurationRecord)configRoot.GetConfigRecord(configPath);
							baseConfigurationRecord.GetSectionRecursive(configKey, getLkg, checkPermission, getRuntimeObject, requestIsHere, out result, out resultRuntimeObject);
							return;
						}
						else
						{
							if (factoryRecord == null || factoryRecord.IsGroup)
							{
								return;
							}
							configKey = factoryRecord.ConfigKey;
						}
					}
					else if (flag3)
					{
						factoryRecord = this.FindAndEnsureFactoryRecord(configKey, out flag5);
					}
					else
					{
						factoryRecord = this.GetFactoryRecord(configKey, false);
						if (factoryRecord == null)
						{
							flag5 = false;
						}
						else
						{
							factoryRecord = this.FindAndEnsureFactoryRecord(configKey, out flag5);
						}
					}
					if (flag5)
					{
						flag4 = true;
					}
					if (sectionRecord == null && flag4)
					{
						sectionRecord = this.EnsureSectionRecord(configKey, true);
					}
					bool getRuntimeObject2 = getRuntimeObject && !flag3;
					object obj3 = null;
					object obj4 = null;
					if (flag5)
					{
						SectionRecord sectionRecord2 = flag3 ? null : sectionRecord;
						this.CreateSectionDefault(configKey, getRuntimeObject2, factoryRecord, sectionRecord2, out obj3, out obj4);
					}
					else
					{
						this._parent.GetSectionRecursive(configKey, false, false, getRuntimeObject2, false, out obj3, out obj4);
					}
					if (flag3)
					{
						if (!this.Evaluate(factoryRecord, sectionRecord, obj3, getLkg, getRuntimeObject, out obj, out obj2))
						{
							flag4 = false;
						}
					}
					else if (sectionRecord != null)
					{
						obj = this.UseParentResult(configKey, obj3, sectionRecord);
						if (getRuntimeObject)
						{
							if (obj3 == obj4)
							{
								obj2 = obj;
							}
							else
							{
								obj2 = this.UseParentResult(configKey, obj4, sectionRecord);
							}
						}
					}
					else
					{
						obj = obj3;
						obj2 = obj4;
					}
					if (flag4 || checkPermission)
					{
						requirePermission = factoryRecord.RequirePermission;
						flag = factoryRecord.IsFactoryTrustedWithoutAptca;
						if (flag4)
						{
							if (sectionRecord == null)
							{
								sectionRecord = this.EnsureSectionRecord(configKey, true);
							}
							sectionRecord.Result = obj;
							if (getRuntimeObject)
							{
								sectionRecord.ResultRuntimeObject = obj2;
							}
							sectionRecord.RequirePermission = requirePermission;
							sectionRecord.IsResultTrustedWithoutAptca = flag;
						}
					}
					flag2 = true;
				}
				catch
				{
					if (!getLkg)
					{
						throw;
					}
				}
				if (!flag2)
				{
					this._parent.GetSectionRecursive(configKey, true, checkPermission, true, true, out result, out resultRuntimeObject);
					return;
				}
			}
			if (checkPermission)
			{
				this.CheckPermissionAllowed(configKey, requirePermission, flag);
			}
			result = obj;
			if (getRuntimeObject)
			{
				resultRuntimeObject = obj2;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003B6C File Offset: 0x00001D6C
		private static bool NeverLoadUserConfigFilesDuringFactorySearch(string configKey)
		{
			return (!LocalAppContextSwitches.AllowUserConfigFilesToLoadWhenSearchingForWellKnownSqlClientFactories && (configKey == "SqlColumnEncryptionEnclaveProviders" || configKey == "SqlAuthenticationProviders")) || (!LocalAppContextSwitches.AllowUserConfigFilesToLoadWhenSearchingForDatasetSerializationAllowedTypes && configKey == "system.data.dataset.serialization/allowedTypes") || (!LocalAppContextSwitches.AllowUserConfigFilesToLoadWhenSearchingForMarkupSerializationAllowedTypes && configKey == "system.windows.markup.serialization/allowedTypes");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003BCC File Offset: 0x00001DCC
		protected void CreateSectionDefault(string configKey, bool getRuntimeObject, FactoryRecord factoryRecord, SectionRecord sectionRecord, out object result, out object resultRuntimeObject)
		{
			result = null;
			resultRuntimeObject = null;
			SectionRecord sectionRecord2;
			if (sectionRecord != null)
			{
				sectionRecord2 = sectionRecord;
			}
			else
			{
				sectionRecord2 = new SectionRecord(configKey);
			}
			object obj = this.CallCreateSection(true, factoryRecord, sectionRecord2, null, null, null);
			object obj2;
			if (getRuntimeObject)
			{
				obj2 = this.GetRuntimeObject(obj);
			}
			else
			{
				obj2 = null;
			}
			result = obj;
			resultRuntimeObject = obj2;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003C15 File Offset: 0x00001E15
		private bool ShouldSkipDueToInheritInChildApplications(bool skipInChildApps)
		{
			return skipInChildApps && this._flags[32];
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003C29 File Offset: 0x00001E29
		private bool ShouldSkipDueToInheritInChildApplications(bool skipInChildApps, string configPath)
		{
			return skipInChildApps && this.Host.IsAboveApplication(configPath);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003C3C File Offset: 0x00001E3C
		private bool Evaluate(FactoryRecord factoryRecord, SectionRecord sectionRecord, object parentResult, bool getLkg, bool getRuntimeObject, out object result, out object resultRuntimeObject)
		{
			result = null;
			resultRuntimeObject = null;
			object obj = null;
			object obj2 = null;
			List<SectionInput> locationInputs = sectionRecord.LocationInputs;
			List<SectionInput> indirectLocationInputs = sectionRecord.IndirectLocationInputs;
			SectionInput fileInput = sectionRecord.FileInput;
			bool flag = false;
			if (sectionRecord.HasResult)
			{
				if (getRuntimeObject && !sectionRecord.HasResultRuntimeObject)
				{
					try
					{
						sectionRecord.ResultRuntimeObject = this.GetRuntimeObject(sectionRecord.Result);
					}
					catch
					{
						if (!getLkg)
						{
							throw;
						}
					}
				}
				if (!getRuntimeObject || sectionRecord.HasResultRuntimeObject)
				{
					obj = sectionRecord.Result;
					if (getRuntimeObject)
					{
						obj2 = sectionRecord.ResultRuntimeObject;
					}
					flag = true;
				}
			}
			if (!flag)
			{
				Exception ex = null;
				try
				{
					string configKey = factoryRecord.ConfigKey;
					string[] keys = configKey.Split(BaseConfigurationRecord.ConfigPathSeparatorParams);
					object obj3 = parentResult;
					if (indirectLocationInputs != null)
					{
						foreach (SectionInput sectionInput in indirectLocationInputs)
						{
							if (!sectionInput.HasResult)
							{
								sectionInput.ThrowOnErrors();
								bool isTrusted = this.Host.IsTrustedConfigPath(sectionInput.SectionXmlInfo.DefinitionConfigPath);
								sectionInput.Result = this.EvaluateOne(keys, sectionInput, isTrusted, factoryRecord, sectionRecord, obj3);
							}
							obj3 = sectionInput.Result;
						}
					}
					if (locationInputs != null)
					{
						foreach (SectionInput sectionInput2 in locationInputs)
						{
							if (!sectionInput2.HasResult)
							{
								sectionInput2.ThrowOnErrors();
								bool isTrusted2 = this.Host.IsTrustedConfigPath(sectionInput2.SectionXmlInfo.DefinitionConfigPath);
								sectionInput2.Result = this.EvaluateOne(keys, sectionInput2, isTrusted2, factoryRecord, sectionRecord, obj3);
							}
							obj3 = sectionInput2.Result;
						}
					}
					if (fileInput != null)
					{
						if (!fileInput.HasResult)
						{
							fileInput.ThrowOnErrors();
							bool isTrusted3 = this._flags[8192];
							fileInput.Result = this.EvaluateOne(keys, fileInput, isTrusted3, factoryRecord, sectionRecord, obj3);
						}
						obj3 = fileInput.Result;
					}
					else
					{
						obj3 = this.UseParentResult(configKey, obj3, sectionRecord);
					}
					if (getRuntimeObject)
					{
						obj2 = this.GetRuntimeObject(obj3);
					}
					obj = obj3;
					flag = true;
				}
				catch (Exception ex2)
				{
					if (!getLkg || locationInputs == null)
					{
						throw;
					}
					ex = ex2;
				}
				if (!flag)
				{
					int num = locationInputs.Count;
					while (--num >= 0)
					{
						SectionInput sectionInput3 = locationInputs[num];
						if (sectionInput3.HasResult)
						{
							if (getRuntimeObject && !sectionInput3.HasResultRuntimeObject)
							{
								try
								{
									sectionInput3.ResultRuntimeObject = this.GetRuntimeObject(sectionInput3.Result);
								}
								catch
								{
								}
							}
							if (!getRuntimeObject || sectionInput3.HasResultRuntimeObject)
							{
								obj = sectionInput3.Result;
								if (getRuntimeObject)
								{
									obj2 = sectionInput3.ResultRuntimeObject;
									break;
								}
								break;
							}
						}
					}
					if (num < 0)
					{
						throw ex;
					}
				}
			}
			if (flag && !this._flags[524288])
			{
				sectionRecord.ClearRawXml();
			}
			result = obj;
			if (getRuntimeObject)
			{
				resultRuntimeObject = obj2;
			}
			return flag;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003F74 File Offset: 0x00002174
		private object EvaluateOne(string[] keys, SectionInput input, bool isTrusted, FactoryRecord factoryRecord, SectionRecord sectionRecord, object parentResult)
		{
			object result;
			try
			{
				ConfigXmlReader sectionXmlReader = this.GetSectionXmlReader(keys, input);
				if (sectionXmlReader == null)
				{
					result = this.UseParentResult(factoryRecord.ConfigKey, parentResult, sectionRecord);
				}
				else
				{
					result = this.CallCreateSection(isTrusted, factoryRecord, sectionRecord, input, parentResult, sectionXmlReader);
				}
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_exception_creating_section", new object[]
				{
					factoryRecord.ConfigKey
				}), e, input.SectionXmlInfo);
			}
			return result;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003FEC File Offset: 0x000021EC
		private static ConfigurationPermission UnrestrictedConfigPermission
		{
			get
			{
				if (BaseConfigurationRecord.s_unrestrictedConfigPermission == null)
				{
					BaseConfigurationRecord.s_unrestrictedConfigPermission = new ConfigurationPermission(PermissionState.Unrestricted);
				}
				return BaseConfigurationRecord.s_unrestrictedConfigPermission;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000400C File Offset: 0x0000220C
		private void CheckPermissionAllowed(string configKey, bool requirePermission, bool isTrustedWithoutAptca)
		{
			if (requirePermission)
			{
				try
				{
					BaseConfigurationRecord.UnrestrictedConfigPermission.Demand();
				}
				catch (SecurityException inner)
				{
					throw new SecurityException(SR.GetString("ConfigurationPermission_Denied", new object[]
					{
						configKey
					}), inner);
				}
			}
			if (isTrustedWithoutAptca && !this.Host.IsFullTrustSectionWithoutAptcaAllowed(this))
			{
				throw new ConfigurationErrorsException(SR.GetString("Section_from_untrusted_assembly", new object[]
				{
					configKey
				}));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004080 File Offset: 0x00002280
		private ConfigXmlReader FindSection(string[] keys, SectionXmlInfo sectionXmlInfo, out int lineNumber)
		{
			lineNumber = 0;
			ConfigXmlReader configXmlReader = null;
			try
			{
				using (this.Impersonate())
				{
					using (Stream stream = this.Host.OpenStreamForRead(sectionXmlInfo.Filename))
					{
						if (!this._flags[131072] && (stream == null || this.HasStreamChanged(sectionXmlInfo.Filename, sectionXmlInfo.StreamVersion)))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_file_has_changed"), sectionXmlInfo.Filename, 0);
						}
						if (stream != null)
						{
							using (XmlUtil xmlUtil = new XmlUtil(stream, sectionXmlInfo.Filename, true))
							{
								if (sectionXmlInfo.SubPath == null)
								{
									configXmlReader = this.FindSectionRecursive(keys, 0, xmlUtil, ref lineNumber);
								}
								else
								{
									xmlUtil.ReadToNextElement();
									while (xmlUtil.Reader.Depth > 0)
									{
										if (xmlUtil.Reader.Name == "location")
										{
											bool flag = false;
											string text = xmlUtil.Reader.GetAttribute("path");
											try
											{
												text = BaseConfigurationRecord.NormalizeLocationSubPath(text, xmlUtil);
												flag = true;
											}
											catch (ConfigurationException ce)
											{
												xmlUtil.SchemaErrors.AddError(ce, ExceptionAction.NonSpecific);
											}
											if (flag && StringUtil.EqualsIgnoreCase(sectionXmlInfo.SubPath, text))
											{
												configXmlReader = this.FindSectionRecursive(keys, 0, xmlUtil, ref lineNumber);
												if (configXmlReader != null)
												{
													break;
												}
											}
										}
										xmlUtil.SkipToNextElement();
									}
								}
								this.ThrowIfParseErrors(xmlUtil.SchemaErrors);
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return configXmlReader;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004258 File Offset: 0x00002458
		private ConfigXmlReader FindSectionRecursive(string[] keys, int iKey, XmlUtil xmlUtil, ref int lineNumber)
		{
			string b = keys[iKey];
			ConfigXmlReader configXmlReader = null;
			int depth = xmlUtil.Reader.Depth;
			xmlUtil.ReadToNextElement();
			while (xmlUtil.Reader.Depth > depth)
			{
				if (xmlUtil.Reader.Name == b)
				{
					if (iKey >= keys.Length - 1)
					{
						string filename = ((IConfigErrorInfo)xmlUtil).Filename;
						int lineNumber2 = xmlUtil.Reader.LineNumber;
						string rawXml = xmlUtil.CopySection();
						configXmlReader = new ConfigXmlReader(rawXml, filename, lineNumber2);
						break;
					}
					configXmlReader = this.FindSectionRecursive(keys, iKey + 1, xmlUtil, ref lineNumber);
					if (configXmlReader != null)
					{
						break;
					}
				}
				else
				{
					if (iKey == 0 && xmlUtil.Reader.Name == "location")
					{
						string text = xmlUtil.Reader.GetAttribute("path");
						bool flag = false;
						try
						{
							text = BaseConfigurationRecord.NormalizeLocationSubPath(text, xmlUtil);
							flag = true;
						}
						catch (ConfigurationException ce)
						{
							xmlUtil.SchemaErrors.AddError(ce, ExceptionAction.NonSpecific);
						}
						if (flag && text == null)
						{
							configXmlReader = this.FindSectionRecursive(keys, iKey, xmlUtil, ref lineNumber);
							if (configXmlReader != null)
							{
								break;
							}
							continue;
						}
					}
					xmlUtil.SkipToNextElement();
				}
			}
			return configXmlReader;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004374 File Offset: 0x00002574
		private ConfigXmlReader LoadConfigSource(string name, SectionXmlInfo sectionXmlInfo)
		{
			string configSourceStreamName = sectionXmlInfo.ConfigSourceStreamName;
			ConfigXmlReader result;
			try
			{
				using (this.Impersonate())
				{
					using (Stream stream = this.Host.OpenStreamForRead(configSourceStreamName))
					{
						if (stream == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_cannot_open_config_source", new object[]
							{
								sectionXmlInfo.ConfigSource
							}), sectionXmlInfo);
						}
						using (XmlUtil xmlUtil = new XmlUtil(stream, configSourceStreamName, true))
						{
							if (xmlUtil.Reader.Name != name)
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_source_file_format"), xmlUtil);
							}
							string attribute = xmlUtil.Reader.GetAttribute("configProtectionProvider");
							if (attribute != null)
							{
								if (xmlUtil.Reader.AttributeCount != 1)
								{
									throw new ConfigurationErrorsException(SR.GetString("Protection_provider_syntax_error"), xmlUtil);
								}
								sectionXmlInfo.ProtectionProviderName = BaseConfigurationRecord.ValidateProtectionProviderAttribute(attribute, xmlUtil);
							}
							string attribute2 = xmlUtil.Reader.GetAttribute("configBuilders");
							if (attribute2 != null)
							{
								sectionXmlInfo.ConfigBuilderName = BaseConfigurationRecord.ValidateConfigBuilderAttribute(attribute2, xmlUtil);
							}
							int lineNumber = xmlUtil.Reader.LineNumber;
							string rawXml = xmlUtil.CopySection();
							while (!xmlUtil.Reader.EOF)
							{
								XmlNodeType nodeType = xmlUtil.Reader.NodeType;
								if (nodeType != XmlNodeType.Comment)
								{
									throw new ConfigurationErrorsException(SR.GetString("Config_source_file_format"), xmlUtil);
								}
								xmlUtil.Reader.Read();
							}
							ConfigXmlReader configXmlReader = new ConfigXmlReader(rawXml, configSourceStreamName, lineNumber);
							result = configXmlReader;
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004540 File Offset: 0x00002740
		protected ConfigXmlReader GetSectionXmlReader(string[] keys, SectionInput input)
		{
			ConfigXmlReader configXmlReader = null;
			string filename = input.SectionXmlInfo.Filename;
			int num = input.SectionXmlInfo.LineNumber;
			try
			{
				string name = keys[keys.Length - 1];
				string rawXml = input.SectionXmlInfo.RawXml;
				if (rawXml != null)
				{
					configXmlReader = new ConfigXmlReader(rawXml, input.SectionXmlInfo.Filename, input.SectionXmlInfo.LineNumber);
				}
				else if (!string.IsNullOrEmpty(input.SectionXmlInfo.ConfigSource))
				{
					filename = input.SectionXmlInfo.ConfigSourceStreamName;
					num = 0;
					configXmlReader = this.LoadConfigSource(name, input.SectionXmlInfo);
				}
				else
				{
					num = 0;
					configXmlReader = this.FindSection(keys, input.SectionXmlInfo, out num);
				}
				if (configXmlReader != null)
				{
					if (!input.IsProtectionProviderDetermined)
					{
						input.ProtectionProvider = this.GetProtectionProviderFromName(input.SectionXmlInfo.ProtectionProviderName, false);
					}
					if (input.ProtectionProvider != null)
					{
						configXmlReader = this.DecryptConfigSection(configXmlReader, input.ProtectionProvider);
					}
				}
				if (configXmlReader != null)
				{
					if (!input.IsConfigBuilderDetermined && !string.IsNullOrWhiteSpace(input.SectionXmlInfo.ConfigBuilderName))
					{
						input.ConfigBuilder = this.GetConfigBuilderFromName(input.SectionXmlInfo.ConfigBuilderName);
					}
					if (input.IsConfigBuilderDetermined)
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.PreserveWhitespace = true;
						xmlDocument.Load(configXmlReader);
						xmlDocument.DocumentElement.RemoveAttribute("configBuilders");
						configXmlReader = new ConfigXmlReader(xmlDocument.DocumentElement.OuterXml, filename, num);
					}
					if (input.ConfigBuilder != null)
					{
						configXmlReader = this.ProcessRawXml(configXmlReader, input.ConfigBuilder);
					}
				}
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_error_loading_XML_file"), e, filename, num);
			}
			return configXmlReader;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000046E4 File Offset: 0x000028E4
		internal string DefaultProviderName
		{
			get
			{
				return this.ProtectedConfig.DefaultProvider;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000046F4 File Offset: 0x000028F4
		internal ProtectedConfigurationProvider GetProtectionProviderFromName(string providerName, bool throwIfNotFound)
		{
			if (!string.IsNullOrEmpty(providerName))
			{
				return this.ProtectedConfig.GetProviderFromName(providerName);
			}
			if (throwIfNotFound)
			{
				throw new ConfigurationErrorsException(SR.GetString("ProtectedConfigurationProvider_not_found", new object[]
				{
					providerName
				}));
			}
			return null;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004738 File Offset: 0x00002938
		private ProtectedConfigurationSection ProtectedConfig
		{
			get
			{
				if (!this._flags[1])
				{
					this.InitProtectedConfigurationSection();
				}
				return this._protectedConfig;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004754 File Offset: 0x00002954
		internal void InitProtectedConfigurationSection()
		{
			if (!this._flags[1])
			{
				this._protectedConfig = (this.GetSection("configProtectedData", false, false) as ProtectedConfigurationSection);
				this._flags[1] = true;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004789 File Offset: 0x00002989
		internal ConfigurationBuilder GetConfigBuilderFromName(string builderName)
		{
			if (string.IsNullOrEmpty(builderName) || this.ConfigBuilders == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_builder_not_found", new object[]
				{
					builderName
				}));
			}
			return this.ConfigBuilders.GetBuilderFromName(builderName);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000047C1 File Offset: 0x000029C1
		private ConfigurationBuildersSection ConfigBuilders
		{
			get
			{
				if (!this._flags[134217728])
				{
					this.InitConfigBuildersSection();
				}
				return this._configBuilders;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000047E1 File Offset: 0x000029E1
		internal void InitConfigBuildersSection()
		{
			if (!this._flags[134217728])
			{
				this._configBuilders = (this.GetSection("configBuilders", false, false) as ConfigurationBuildersSection);
				this._flags[134217728] = true;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004820 File Offset: 0x00002A20
		protected object CallCreateSection(bool inputIsTrusted, FactoryRecord factoryRecord, SectionRecord sectionRecord, SectionInput sectionInput, object parentConfig, ConfigXmlReader reader)
		{
			string filename = null;
			int line = -1;
			if (sectionInput != null && sectionInput.SectionXmlInfo != null)
			{
				filename = sectionInput.SectionXmlInfo.Filename;
				line = sectionInput.SectionXmlInfo.LineNumber;
			}
			object obj;
			try
			{
				using (this.Impersonate())
				{
					obj = this.CreateSection(inputIsTrusted, factoryRecord, sectionRecord, sectionInput, parentConfig, reader);
					if (obj == null && parentConfig != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_object_is_null"), filename, line);
					}
				}
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_exception_creating_section_handler", new object[]
				{
					factoryRecord.ConfigKey
				}), e, filename, line);
			}
			return obj;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000048E8 File Offset: 0x00002AE8
		internal bool IsRootDeclaration(string configKey, bool implicitIsRooted)
		{
			return (implicitIsRooted || !BaseConfigurationRecord.IsImplicitSection(configKey)) && (this._parent.IsRootConfig || this._parent.FindFactoryRecord(configKey, true) == null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004918 File Offset: 0x00002B18
		internal FactoryRecord FindFactoryRecord(string configKey, bool permitErrors, out BaseConfigurationRecord configRecord)
		{
			configRecord = null;
			BaseConfigurationRecord baseConfigurationRecord = this;
			while (!baseConfigurationRecord.IsRootConfig)
			{
				FactoryRecord factoryRecord = baseConfigurationRecord.GetFactoryRecord(configKey, permitErrors);
				if (factoryRecord != null)
				{
					configRecord = baseConfigurationRecord;
					return factoryRecord;
				}
				baseConfigurationRecord = baseConfigurationRecord._parent;
			}
			return null;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004950 File Offset: 0x00002B50
		internal FactoryRecord FindFactoryRecord(string configKey, bool permitErrors)
		{
			BaseConfigurationRecord baseConfigurationRecord;
			return this.FindFactoryRecord(configKey, permitErrors, out baseConfigurationRecord);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004968 File Offset: 0x00002B68
		private FactoryRecord FindAndEnsureFactoryRecord(string configKey, out bool isRootDeclaredHere)
		{
			isRootDeclaredHere = false;
			BaseConfigurationRecord baseConfigurationRecord;
			FactoryRecord factoryRecord = this.FindFactoryRecord(configKey, false, out baseConfigurationRecord);
			if (factoryRecord != null && !factoryRecord.IsGroup)
			{
				FactoryRecord factoryRecord2 = factoryRecord;
				BaseConfigurationRecord baseConfigurationRecord2 = baseConfigurationRecord;
				BaseConfigurationRecord parent = baseConfigurationRecord._parent;
				while (!parent.IsRootConfig)
				{
					BaseConfigurationRecord baseConfigurationRecord3;
					FactoryRecord factoryRecord3 = parent.FindFactoryRecord(configKey, false, out baseConfigurationRecord3);
					if (factoryRecord3 == null)
					{
						break;
					}
					factoryRecord2 = factoryRecord3;
					baseConfigurationRecord2 = baseConfigurationRecord3;
					parent = baseConfigurationRecord3.Parent;
				}
				if (factoryRecord2.Factory == null)
				{
					try
					{
						object obj = baseConfigurationRecord2.CreateSectionFactory(factoryRecord2);
						bool isFactoryTrustedWithoutAptca = TypeUtil.IsTypeFromTrustedAssemblyWithoutAptca(obj.GetType());
						factoryRecord2.Factory = obj;
						factoryRecord2.IsFactoryTrustedWithoutAptca = isFactoryTrustedWithoutAptca;
					}
					catch (Exception e)
					{
						throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_exception_creating_section_handler", new object[]
						{
							factoryRecord.ConfigKey
						}), e, factoryRecord);
					}
				}
				if (factoryRecord.Factory == null)
				{
					factoryRecord.Factory = factoryRecord2.Factory;
					factoryRecord.IsFactoryTrustedWithoutAptca = factoryRecord2.IsFactoryTrustedWithoutAptca;
				}
				isRootDeclaredHere = (this == baseConfigurationRecord2);
			}
			return factoryRecord;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004A58 File Offset: 0x00002C58
		private Hashtable ScanFactories(XmlUtil xmlUtil)
		{
			Hashtable hashtable = new Hashtable();
			if (xmlUtil.Reader.NodeType != XmlNodeType.Element || xmlUtil.Reader.Name != "configuration")
			{
				string text = ConfigurationErrorsException.AlwaysSafeFilename(((IConfigErrorInfo)xmlUtil).Filename);
				throw new ConfigurationErrorsException(SR.GetString("Config_file_doesnt_have_root_configuration", new object[]
				{
					text
				}), xmlUtil);
			}
			while (xmlUtil.Reader.MoveToNextAttribute())
			{
				string name = xmlUtil.Reader.Name;
				if (name == "xmlns")
				{
					if (xmlUtil.Reader.Value == "http://schemas.microsoft.com/.NetConfiguration/v2.0")
					{
						this._flags[512] = true;
						this._flags[67108864] = true;
					}
					else
					{
						ConfigurationErrorsException ce = new ConfigurationErrorsException(SR.GetString("Config_namespace_invalid", new object[]
						{
							xmlUtil.Reader.Value,
							"http://schemas.microsoft.com/.NetConfiguration/v2.0"
						}), xmlUtil);
						xmlUtil.SchemaErrors.AddError(ce, ExceptionAction.Global);
					}
				}
				else
				{
					xmlUtil.AddErrorUnrecognizedAttribute(ExceptionAction.NonSpecific);
				}
			}
			xmlUtil.StrictReadToNextElement(ExceptionAction.NonSpecific);
			if (xmlUtil.Reader.Depth == 1 && xmlUtil.Reader.Name == "configSections")
			{
				xmlUtil.VerifyNoUnrecognizedAttributes(ExceptionAction.NonSpecific);
				this.ScanFactoriesRecursive(xmlUtil, string.Empty, hashtable);
			}
			return hashtable;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004BA4 File Offset: 0x00002DA4
		private void ScanFactoriesRecursive(XmlUtil xmlUtil, string parentConfigKey, Hashtable factoryList)
		{
			xmlUtil.SchemaErrors.ResetLocalErrors();
			int depth = xmlUtil.Reader.Depth;
			xmlUtil.StrictReadToNextElement(ExceptionAction.NonSpecific);
			while (xmlUtil.Reader.Depth == depth + 1)
			{
				bool flag = false;
				string name = xmlUtil.Reader.Name;
				if (!(name == "sectionGroup"))
				{
					if (!(name == "section"))
					{
						if (!(name == "remove"))
						{
							if (!(name == "clear"))
							{
								xmlUtil.AddErrorUnrecognizedElement(ExceptionAction.NonSpecific);
								xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
								flag = true;
							}
							else
							{
								xmlUtil.VerifyNoUnrecognizedAttributes(ExceptionAction.NonSpecific);
							}
						}
						else
						{
							string text = null;
							while (xmlUtil.Reader.MoveToNextAttribute())
							{
								if (xmlUtil.Reader.Name != "name")
								{
									xmlUtil.AddErrorUnrecognizedAttribute(ExceptionAction.NonSpecific);
								}
								text = xmlUtil.Reader.Value;
								int lineNumber = xmlUtil.Reader.LineNumber;
							}
							xmlUtil.Reader.MoveToElement();
							if (xmlUtil.VerifyRequiredAttribute(text, "name", ExceptionAction.NonSpecific))
							{
								BaseConfigurationRecord.VerifySectionName(text, xmlUtil, ExceptionAction.NonSpecific, false, true);
							}
						}
					}
					else
					{
						string text2 = null;
						string text3 = null;
						ConfigurationAllowDefinition allowDefinition = ConfigurationAllowDefinition.Everywhere;
						ConfigurationAllowExeDefinition allowExeDefinition = ConfigurationAllowExeDefinition.MachineToApplication;
						OverrideModeSetting overrideModeDefault = OverrideModeSetting.SectionDefault;
						bool allowLocation = true;
						bool restartOnExternalChanges = true;
						bool requirePermission = true;
						bool flag2 = false;
						int lineNumber2 = xmlUtil.Reader.LineNumber;
						while (xmlUtil.Reader.MoveToNextAttribute())
						{
							string name2 = xmlUtil.Reader.Name;
							uint num = <PrivateImplementationDetails>.ComputeStringHash(name2);
							if (num <= 1841158919U)
							{
								if (num <= 1361572173U)
								{
									if (num != 1066839313U)
									{
										if (num != 1361572173U)
										{
											goto IL_48A;
										}
										if (!(name2 == "type"))
										{
											goto IL_48A;
										}
										xmlUtil.VerifyAndGetNonEmptyStringAttribute(ExceptionAction.Local, out text3);
										flag2 = true;
										continue;
									}
									else
									{
										if (!(name2 == "allowLocation"))
										{
											goto IL_48A;
										}
										xmlUtil.VerifyAndGetBooleanAttribute(ExceptionAction.Local, true, out allowLocation);
										continue;
									}
								}
								else if (num != 1766272347U)
								{
									if (num != 1841158919U)
									{
										goto IL_48A;
									}
									if (!(name2 == "restartOnExternalChanges"))
									{
										goto IL_48A;
									}
								}
								else
								{
									if (!(name2 == "requirePermission"))
									{
										goto IL_48A;
									}
									xmlUtil.VerifyAndGetBooleanAttribute(ExceptionAction.Local, true, out requirePermission);
									continue;
								}
							}
							else
							{
								if (num <= 2369371622U)
								{
									if (num != 1931054735U)
									{
										if (num != 2369371622U)
										{
											goto IL_48A;
										}
										if (!(name2 == "name"))
										{
											goto IL_48A;
										}
										text2 = xmlUtil.Reader.Value;
										BaseConfigurationRecord.VerifySectionName(text2, xmlUtil, ExceptionAction.Local, false, true);
										continue;
									}
									else
									{
										if (!(name2 == "allowExeDefinition"))
										{
											goto IL_48A;
										}
										try
										{
											allowExeDefinition = BaseConfigurationRecord.AllowExeDefinitionToEnum(xmlUtil.Reader.Value, xmlUtil);
											continue;
										}
										catch (ConfigurationException ce)
										{
											xmlUtil.SchemaErrors.AddError(ce, ExceptionAction.Local);
											continue;
										}
									}
								}
								else if (num != 3132892695U)
								{
									if (num != 3263379011U)
									{
										goto IL_48A;
									}
									if (!(name2 == "allowDefinition"))
									{
										goto IL_48A;
									}
								}
								else
								{
									if (!(name2 == "overrideModeDefault"))
									{
										goto IL_48A;
									}
									try
									{
										overrideModeDefault = OverrideModeSetting.CreateFromXmlReadValue(OverrideModeSetting.ParseOverrideModeXmlValue(xmlUtil.Reader.Value, xmlUtil));
										if (overrideModeDefault.OverrideMode == OverrideMode.Inherit)
										{
											overrideModeDefault.ChangeModeInternal(OverrideMode.Allow);
										}
										continue;
									}
									catch (ConfigurationException ce2)
									{
										xmlUtil.SchemaErrors.AddError(ce2, ExceptionAction.Local);
										continue;
									}
									goto IL_48A;
								}
								try
								{
									allowDefinition = BaseConfigurationRecord.AllowDefinitionToEnum(xmlUtil.Reader.Value, xmlUtil);
									continue;
								}
								catch (ConfigurationException ce3)
								{
									xmlUtil.SchemaErrors.AddError(ce3, ExceptionAction.Local);
									continue;
								}
							}
							xmlUtil.VerifyAndGetBooleanAttribute(ExceptionAction.Local, true, out restartOnExternalChanges);
							continue;
							IL_48A:
							xmlUtil.AddErrorUnrecognizedAttribute(ExceptionAction.Local);
						}
						xmlUtil.Reader.MoveToElement();
						if (!xmlUtil.VerifyRequiredAttribute(text2, "name", ExceptionAction.NonSpecific))
						{
							xmlUtil.SchemaErrors.RetrieveAndResetLocalErrors(true);
						}
						else
						{
							if (!flag2)
							{
								xmlUtil.AddErrorRequiredAttribute("type", ExceptionAction.Local);
							}
							if (StringUtil.StartsWith(text2, "config"))
							{
								Type type = Type.GetType(text3);
								if (!object.Equals(text2, "configBuilders") || type != this.ConfigurationBuildersSectionType)
								{
									throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_cannot_begin_with_config"), xmlUtil);
								}
							}
							string text4 = BaseConfigurationRecord.CombineConfigKey(parentConfigKey, text2);
							FactoryRecord factoryRecord = (FactoryRecord)factoryList[text4];
							if (factoryRecord != null)
							{
								xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined_at_this_level", new object[]
								{
									text2
								}), xmlUtil), ExceptionAction.Local);
							}
							else
							{
								FactoryRecord factoryRecord2 = this._parent.FindFactoryRecord(text4, true);
								if (factoryRecord2 != null)
								{
									text4 = factoryRecord2.ConfigKey;
									if (factoryRecord2.IsGroup)
									{
										xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
										{
											text2
										}), xmlUtil), ExceptionAction.Local);
										factoryRecord2 = null;
									}
									else if (!factoryRecord2.IsEquivalentSectionFactory(this.Host, text3, allowLocation, allowDefinition, allowExeDefinition, restartOnExternalChanges, requirePermission))
									{
										xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
										{
											text2
										}), xmlUtil), ExceptionAction.Local);
										factoryRecord2 = null;
									}
								}
								if (factoryRecord2 != null)
								{
									factoryRecord = factoryRecord2.CloneSection(xmlUtil.Filename, lineNumber2);
								}
								else
								{
									factoryRecord = new FactoryRecord(text4, parentConfigKey, text2, text3, allowLocation, allowDefinition, allowExeDefinition, overrideModeDefault, restartOnExternalChanges, requirePermission, this._flags[8192], false, xmlUtil.Filename, lineNumber2);
								}
								factoryList[text4] = factoryRecord;
							}
							factoryRecord.AddErrors(xmlUtil.SchemaErrors.RetrieveAndResetLocalErrors(true));
						}
					}
					if (!flag)
					{
						xmlUtil.StrictReadToNextElement(ExceptionAction.NonSpecific);
						if (xmlUtil.Reader.Depth > depth + 1)
						{
							xmlUtil.AddErrorUnrecognizedElement(ExceptionAction.NonSpecific);
							while (xmlUtil.Reader.Depth > depth + 1)
							{
								xmlUtil.ReadToNextElement();
							}
						}
					}
				}
				else
				{
					string text5 = null;
					string text6 = null;
					int lineNumber3 = xmlUtil.Reader.LineNumber;
					while (xmlUtil.Reader.MoveToNextAttribute())
					{
						string name3 = xmlUtil.Reader.Name;
						if (!(name3 == "name"))
						{
							if (!(name3 == "type"))
							{
								xmlUtil.AddErrorUnrecognizedAttribute(ExceptionAction.Local);
							}
							else
							{
								xmlUtil.VerifyAndGetNonEmptyStringAttribute(ExceptionAction.Local, out text6);
							}
						}
						else
						{
							text5 = xmlUtil.Reader.Value;
							BaseConfigurationRecord.VerifySectionName(text5, xmlUtil, ExceptionAction.Local, false, false);
						}
					}
					xmlUtil.Reader.MoveToElement();
					if (!xmlUtil.VerifyRequiredAttribute(text5, "name", ExceptionAction.NonSpecific))
					{
						xmlUtil.SchemaErrors.RetrieveAndResetLocalErrors(true);
						xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
					}
					else
					{
						string text7 = BaseConfigurationRecord.CombineConfigKey(parentConfigKey, text5);
						FactoryRecord factoryRecord3 = (FactoryRecord)factoryList[text7];
						if (factoryRecord3 != null)
						{
							xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined_at_this_level", new object[]
							{
								text5
							}), xmlUtil), ExceptionAction.Local);
						}
						else
						{
							FactoryRecord factoryRecord4 = this._parent.FindFactoryRecord(text7, true);
							if (factoryRecord4 != null)
							{
								text7 = factoryRecord4.ConfigKey;
								if (factoryRecord4 != null && (!factoryRecord4.IsGroup || !factoryRecord4.IsEquivalentSectionGroupFactory(this.Host, text6)))
								{
									xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
									{
										text5
									}), xmlUtil), ExceptionAction.Local);
									factoryRecord4 = null;
								}
							}
							if (factoryRecord4 != null)
							{
								factoryRecord3 = factoryRecord4.CloneSectionGroup(text6, xmlUtil.Filename, lineNumber3);
							}
							else
							{
								factoryRecord3 = new FactoryRecord(text7, parentConfigKey, text5, text6, xmlUtil.Filename, lineNumber3);
							}
							factoryList[text7] = factoryRecord3;
						}
						factoryRecord3.AddErrors(xmlUtil.SchemaErrors.RetrieveAndResetLocalErrors(true));
						this.ScanFactoriesRecursive(xmlUtil, text7, factoryList);
					}
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005328 File Offset: 0x00003528
		internal static ConfigurationAllowExeDefinition AllowExeDefinitionToEnum(string allowExeDefinition, XmlUtil xmlUtil)
		{
			if (allowExeDefinition == "MachineOnly")
			{
				return ConfigurationAllowExeDefinition.MachineOnly;
			}
			if (allowExeDefinition == "MachineToApplication")
			{
				return ConfigurationAllowExeDefinition.MachineToApplication;
			}
			if (allowExeDefinition == "MachineToRoamingUser")
			{
				return ConfigurationAllowExeDefinition.MachineToRoamingUser;
			}
			if (!(allowExeDefinition == "MachineToLocalUser"))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_section_allow_exe_definition_attribute_invalid"), xmlUtil);
			}
			return ConfigurationAllowExeDefinition.MachineToLocalUser;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000538C File Offset: 0x0000358C
		internal static ConfigurationAllowDefinition AllowDefinitionToEnum(string allowDefinition, XmlUtil xmlUtil)
		{
			string value = xmlUtil.Reader.Value;
			if (value == "Everywhere")
			{
				return ConfigurationAllowDefinition.Everywhere;
			}
			if (value == "MachineOnly")
			{
				return ConfigurationAllowDefinition.MachineOnly;
			}
			if (value == "MachineToApplication")
			{
				return ConfigurationAllowDefinition.MachineToApplication;
			}
			if (!(value == "MachineToWebRoot"))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_section_allow_definition_attribute_invalid"), xmlUtil);
			}
			return ConfigurationAllowDefinition.MachineToWebRoot;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000250E File Offset: 0x0000070E
		internal static string CombineConfigKey(string parentConfigKey, string tagName)
		{
			if (string.IsNullOrEmpty(parentConfigKey))
			{
				return tagName;
			}
			if (string.IsNullOrEmpty(tagName))
			{
				return parentConfigKey;
			}
			return parentConfigKey + "/" + tagName;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000053FC File Offset: 0x000035FC
		internal static void SplitConfigKey(string configKey, out string group, out string name)
		{
			int num = configKey.LastIndexOf('/');
			if (num == -1)
			{
				group = string.Empty;
				name = configKey;
				return;
			}
			group = configKey.Substring(0, num);
			name = configKey.Substring(num + 1);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005438 File Offset: 0x00003638
		[Conditional("DBG")]
		private void DebugValidateIndirectInputs(SectionRecord sectionRecord)
		{
			if (this._parent.IsRootConfig)
			{
				return;
			}
			for (int i = sectionRecord.IndirectLocationInputs.Count - 1; i >= 0; i--)
			{
				SectionInput sectionInput = sectionRecord.IndirectLocationInputs[i];
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005478 File Offset: 0x00003678
		private OverrideMode ResolveOverrideModeFromParent(string configKey, out OverrideMode childLockMode)
		{
			OverrideMode overrideMode = OverrideMode.Inherit;
			BaseConfigurationRecord parent = this.Parent;
			BaseConfigurationRecord parent2 = this.Parent;
			childLockMode = OverrideMode.Inherit;
			while (!parent.IsRootConfig && overrideMode == OverrideMode.Inherit)
			{
				SectionRecord sectionRecord = parent.GetSectionRecord(configKey, true);
				if (sectionRecord != null)
				{
					if (this.IsLocationConfig && parent2 == parent)
					{
						overrideMode = (sectionRecord.Locked ? OverrideMode.Deny : OverrideMode.Allow);
						childLockMode = (sectionRecord.LockChildren ? OverrideMode.Deny : OverrideMode.Allow);
					}
					else
					{
						overrideMode = (sectionRecord.LockChildren ? OverrideMode.Deny : OverrideMode.Allow);
						childLockMode = overrideMode;
					}
				}
				parent = parent._parent;
			}
			if (overrideMode == OverrideMode.Inherit)
			{
				OverrideMode overrideMode2 = this.FindFactoryRecord(configKey, true).OverrideModeDefault.OverrideMode;
				bool flag;
				if (this.IsLocationConfig)
				{
					flag = (this.Parent.GetFactoryRecord(configKey, true) != null);
				}
				else
				{
					flag = (this.GetFactoryRecord(configKey, true) != null);
				}
				if (!flag)
				{
					overrideMode = (childLockMode = overrideMode2);
				}
				else
				{
					overrideMode = OverrideMode.Allow;
					childLockMode = overrideMode2;
				}
			}
			return overrideMode;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000554C File Offset: 0x0000374C
		protected OverrideMode GetSectionLockedMode(string configKey)
		{
			OverrideMode overrideMode = OverrideMode.Inherit;
			return this.GetSectionLockedMode(configKey, out overrideMode);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005564 File Offset: 0x00003764
		protected OverrideMode GetSectionLockedMode(string configKey, out OverrideMode childLockMode)
		{
			SectionRecord sectionRecord = this.GetSectionRecord(configKey, true);
			OverrideMode result;
			if (sectionRecord != null)
			{
				result = (sectionRecord.Locked ? OverrideMode.Deny : OverrideMode.Allow);
				childLockMode = (sectionRecord.LockChildren ? OverrideMode.Deny : OverrideMode.Allow);
			}
			else
			{
				result = this.ResolveOverrideModeFromParent(configKey, out childLockMode);
			}
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000055A6 File Offset: 0x000037A6
		private void ScanSections(XmlUtil xmlUtil)
		{
			this.ScanSectionsRecursive(xmlUtil, string.Empty, false, null, OverrideModeSetting.LocationDefault, false);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000055BC File Offset: 0x000037BC
		private void ScanSectionsRecursive(XmlUtil xmlUtil, string parentConfigKey, bool inLocation, string locationSubPath, OverrideModeSetting overrideMode, bool skipInChildApps)
		{
			xmlUtil.SchemaErrors.ResetLocalErrors();
			int num;
			if (parentConfigKey.Length == 0 && !inLocation)
			{
				num = 0;
			}
			else
			{
				num = xmlUtil.Reader.Depth;
				xmlUtil.StrictReadToNextElement(ExceptionAction.NonSpecific);
			}
			while (xmlUtil.Reader.Depth == num + 1)
			{
				string name = xmlUtil.Reader.Name;
				if (name == "configSections")
				{
					xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_client_config_too_many_configsections_elements", new object[]
					{
						name
					}), xmlUtil), ExceptionAction.NonSpecific);
					xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
				}
				else if (name == "location")
				{
					if (parentConfigKey.Length > 0 || inLocation)
					{
						xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_location_location_not_allowed"), xmlUtil), ExceptionAction.Global);
						xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
					}
					else
					{
						this.ScanLocationSection(xmlUtil);
					}
				}
				else
				{
					string text = BaseConfigurationRecord.CombineConfigKey(parentConfigKey, name);
					FactoryRecord factoryRecord = this.FindFactoryRecord(text, true);
					if (factoryRecord == null)
					{
						if (!this.ClassFlags[64])
						{
							xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_unrecognized_configuration_section", new object[]
							{
								text
							}), xmlUtil), ExceptionAction.Local);
						}
						BaseConfigurationRecord.VerifySectionName(name, xmlUtil, ExceptionAction.Local, false, false);
						factoryRecord = new FactoryRecord(text, parentConfigKey, name, typeof(DefaultSection).AssemblyQualifiedName, true, ConfigurationAllowDefinition.Everywhere, ConfigurationAllowExeDefinition.MachineToRoamingUser, OverrideModeSetting.SectionDefault, true, true, this._flags[8192], true, null, -1);
						factoryRecord.AddErrors(xmlUtil.SchemaErrors.RetrieveAndResetLocalErrors(true));
						this.EnsureFactories()[text] = factoryRecord;
					}
					if (factoryRecord.IsGroup)
					{
						if (factoryRecord.HasErrors)
						{
							xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
						}
						else
						{
							if (xmlUtil.Reader.AttributeCount > 0)
							{
								while (xmlUtil.Reader.MoveToNextAttribute())
								{
									if (BaseConfigurationRecord.IsReservedAttributeName(xmlUtil.Reader.Name))
									{
										xmlUtil.AddErrorReservedAttribute(ExceptionAction.NonSpecific);
									}
								}
								xmlUtil.Reader.MoveToElement();
							}
							this.ScanSectionsRecursive(xmlUtil, text, inLocation, locationSubPath, overrideMode, skipInChildApps);
						}
					}
					else
					{
						text = factoryRecord.ConfigKey;
						string filename = xmlUtil.Filename;
						int lineNumber = xmlUtil.LineNumber;
						string rawXml = null;
						string text2 = null;
						string text3 = null;
						object configSourceStreamVersion = null;
						string configBuilderName = null;
						string protectionProviderName = null;
						OverrideMode overrideMode2 = OverrideMode.Inherit;
						OverrideMode forChildren = OverrideMode.Inherit;
						bool flag = false;
						bool flag2 = locationSubPath == null;
						if (!factoryRecord.HasErrors)
						{
							if (inLocation && !factoryRecord.AllowLocation)
							{
								xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_section_cannot_be_used_in_location"), xmlUtil), ExceptionAction.Local);
							}
							if (flag2)
							{
								SectionRecord sectionRecord = this.GetSectionRecord(text, true);
								if (sectionRecord != null && sectionRecord.HasFileInput && !factoryRecord.IsIgnorable())
								{
									xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_sections_must_be_unique"), xmlUtil), ExceptionAction.Local);
								}
								try
								{
									this.VerifyDefinitionAllowed(factoryRecord, this._configPath, xmlUtil);
								}
								catch (ConfigurationException ce)
								{
									xmlUtil.SchemaErrors.AddError(ce, ExceptionAction.Local);
								}
							}
							overrideMode2 = this.GetSectionLockedMode(text, out forChildren);
							if (overrideMode2 == OverrideMode.Deny)
							{
								xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_section_locked"), xmlUtil), ExceptionAction.Local);
							}
							if (xmlUtil.Reader.AttributeCount >= 1)
							{
								string attribute = xmlUtil.Reader.GetAttribute("configSource");
								if (attribute != null)
								{
									try
									{
										text2 = BaseConfigurationRecord.NormalizeConfigSource(attribute, xmlUtil);
									}
									catch (ConfigurationException ce2)
									{
										xmlUtil.SchemaErrors.AddError(ce2, ExceptionAction.Local);
									}
									if (xmlUtil.Reader.AttributeCount != 1)
									{
										xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_source_syntax_error"), xmlUtil), ExceptionAction.Local);
									}
								}
								string attribute2 = xmlUtil.Reader.GetAttribute("configProtectionProvider");
								if (attribute2 != null)
								{
									try
									{
										protectionProviderName = BaseConfigurationRecord.ValidateProtectionProviderAttribute(attribute2, xmlUtil);
									}
									catch (ConfigurationException ce3)
									{
										xmlUtil.SchemaErrors.AddError(ce3, ExceptionAction.Local);
									}
									if (xmlUtil.Reader.AttributeCount != 1)
									{
										xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Protection_provider_syntax_error"), xmlUtil), ExceptionAction.Local);
									}
								}
								string attribute3 = xmlUtil.Reader.GetAttribute("configBuilders");
								if (attribute3 != null)
								{
									try
									{
										configBuilderName = BaseConfigurationRecord.ValidateConfigBuilderAttribute(attribute3, xmlUtil);
									}
									catch (ConfigurationException ce4)
									{
										xmlUtil.SchemaErrors.AddError(ce4, ExceptionAction.Local);
									}
								}
								if (attribute != null && !xmlUtil.Reader.IsEmptyElement)
								{
									while (xmlUtil.Reader.Read())
									{
										XmlNodeType nodeType = xmlUtil.Reader.NodeType;
										if (nodeType == XmlNodeType.EndElement)
										{
											break;
										}
										if (nodeType != XmlNodeType.Comment)
										{
											xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Config_source_syntax_error"), xmlUtil), ExceptionAction.Local);
											if (nodeType == XmlNodeType.Element)
											{
												xmlUtil.StrictSkipToOurParentsEndElement(ExceptionAction.NonSpecific);
											}
											else
											{
												xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
											}
											flag = true;
											break;
										}
									}
								}
							}
							if (text2 != null)
							{
								try
								{
									try
									{
										text3 = this.Host.GetStreamNameForConfigSource(this.ConfigStreamInfo.StreamName, text2);
									}
									catch (Exception e)
									{
										throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_source_invalid"), e, xmlUtil);
									}
									this.ValidateUniqueConfigSource(text, text3, text2, xmlUtil);
									configSourceStreamVersion = this.MonitorStream(text, text2, text3);
								}
								catch (ConfigurationException ce5)
								{
									xmlUtil.SchemaErrors.AddError(ce5, ExceptionAction.Local);
								}
							}
							if (!xmlUtil.SchemaErrors.HasLocalErrors && text2 == null && this.ShouldPrefetchRawXml(factoryRecord))
							{
								rawXml = xmlUtil.CopySection();
								if (xmlUtil.Reader.NodeType != XmlNodeType.Element)
								{
									xmlUtil.VerifyIgnorableNodeType(ExceptionAction.NonSpecific);
									xmlUtil.StrictReadToNextElement(ExceptionAction.NonSpecific);
								}
								flag = true;
							}
						}
						List<ConfigurationException> errors = xmlUtil.SchemaErrors.RetrieveAndResetLocalErrors(flag2);
						if (!flag)
						{
							xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
						}
						bool flag3 = true;
						if (flag2)
						{
							if (this.ShouldSkipDueToInheritInChildApplications(skipInChildApps))
							{
								flag3 = false;
							}
						}
						else if (!this._flags[1048576])
						{
							flag3 = false;
						}
						if (flag3)
						{
							string targetConfigPath = (locationSubPath == null) ? this._configPath : null;
							SectionXmlInfo sectionXmlInfo = new SectionXmlInfo(text, this._configPath, targetConfigPath, locationSubPath, filename, lineNumber, this.ConfigStreamInfo.StreamVersion, rawXml, text2, text3, configSourceStreamVersion, configBuilderName, protectionProviderName, overrideMode, skipInChildApps);
							if (locationSubPath == null)
							{
								SectionRecord sectionRecord2 = this.EnsureSectionRecordUnsafe(text, true);
								sectionRecord2.ChangeLockSettings(overrideMode2, forChildren);
								SectionInput sectionInput = new SectionInput(sectionXmlInfo, errors);
								sectionRecord2.AddFileInput(sectionInput);
							}
							else
							{
								LocationSectionRecord value = new LocationSectionRecord(sectionXmlInfo, errors);
								this.EnsureLocationSections().Add(value);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005C00 File Offset: 0x00003E00
		private void ScanLocationSection(XmlUtil xmlUtil)
		{
			string text = null;
			bool flag = true;
			int globalErrorCount = xmlUtil.SchemaErrors.GlobalErrorCount;
			OverrideModeSetting overrideMode = OverrideModeSetting.LocationDefault;
			bool flag2 = false;
			while (xmlUtil.Reader.MoveToNextAttribute())
			{
				string name = xmlUtil.Reader.Name;
				if (!(name == "path"))
				{
					if (!(name == "allowOverride"))
					{
						if (!(name == "overrideMode"))
						{
							if (!(name == "inheritInChildApplications"))
							{
								xmlUtil.AddErrorUnrecognizedAttribute(ExceptionAction.Global);
							}
							else
							{
								xmlUtil.VerifyAndGetBooleanAttribute(ExceptionAction.Global, true, out flag);
							}
						}
						else if (flag2)
						{
							xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Invalid_override_mode_declaration"), xmlUtil), ExceptionAction.Global);
						}
						else
						{
							overrideMode = OverrideModeSetting.CreateFromXmlReadValue(OverrideModeSetting.ParseOverrideModeXmlValue(xmlUtil.Reader.Value, xmlUtil));
							flag2 = true;
						}
					}
					else if (flag2)
					{
						xmlUtil.SchemaErrors.AddError(new ConfigurationErrorsException(SR.GetString("Invalid_override_mode_declaration"), xmlUtil), ExceptionAction.Global);
					}
					else
					{
						bool allowOverride = true;
						xmlUtil.VerifyAndGetBooleanAttribute(ExceptionAction.Global, true, out allowOverride);
						overrideMode = OverrideModeSetting.CreateFromXmlReadValue(allowOverride);
						flag2 = true;
					}
				}
				else
				{
					text = xmlUtil.Reader.Value;
				}
			}
			xmlUtil.Reader.MoveToElement();
			try
			{
				text = BaseConfigurationRecord.NormalizeLocationSubPath(text, xmlUtil);
				if (text == null && !flag && this.Host.IsDefinitionAllowed(this._configPath, ConfigurationAllowDefinition.MachineToWebRoot, ConfigurationAllowExeDefinition.MachineOnly))
				{
					throw new ConfigurationErrorsException(SR.GetString("Location_invalid_inheritInChildApplications_in_machine_or_root_web_config"), xmlUtil);
				}
			}
			catch (ConfigurationErrorsException ce)
			{
				xmlUtil.SchemaErrors.AddError(ce, ExceptionAction.Global);
			}
			if (xmlUtil.SchemaErrors.GlobalErrorCount > globalErrorCount)
			{
				xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
				return;
			}
			if (text == null)
			{
				this.ScanSectionsRecursive(xmlUtil, string.Empty, true, null, overrideMode, !flag);
				return;
			}
			if (!this._flags[1048576])
			{
				xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
				return;
			}
			IInternalConfigHost host = this.Host;
			if (this is RuntimeConfigurationRecord && host != null && text.Length != 0 && text[0] != '.')
			{
				if (BaseConfigurationRecord.s_appConfigPath == null)
				{
					object configContext = this.ConfigContext;
					if (configContext != null)
					{
						string value = configContext.ToString();
						Interlocked.CompareExchange<string>(ref BaseConfigurationRecord.s_appConfigPath, value, null);
					}
				}
				string configPathFromLocationSubPath = host.GetConfigPathFromLocationSubPath(this._configPath, text);
				if (!StringUtil.StartsWithIgnoreCase(BaseConfigurationRecord.s_appConfigPath, configPathFromLocationSubPath) && !StringUtil.StartsWithIgnoreCase(configPathFromLocationSubPath, BaseConfigurationRecord.s_appConfigPath))
				{
					xmlUtil.StrictSkipToNextElement(ExceptionAction.NonSpecific);
					return;
				}
			}
			this.AddLocation(text);
			this.ScanSectionsRecursive(xmlUtil, string.Empty, true, text, overrideMode, !flag);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005E74 File Offset: 0x00004074
		protected virtual void AddLocation(string LocationSubPath)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005E78 File Offset: 0x00004078
		private void ResolveLocationSections()
		{
			if (!this._flags[256])
			{
				if (!this._parent.IsRootConfig)
				{
					this._parent.ResolveLocationSections();
				}
				lock (this)
				{
					if (!this._flags[256] && this._locationSections != null)
					{
						HybridDictionary hybridDictionary = new HybridDictionary(true);
						foreach (object obj in this._locationSections)
						{
							LocationSectionRecord locationSectionRecord = (LocationSectionRecord)obj;
							string configPathFromLocationSubPath = this.Host.GetConfigPathFromLocationSubPath(this._configPath, locationSectionRecord.SectionXmlInfo.SubPath);
							locationSectionRecord.SectionXmlInfo.TargetConfigPath = configPathFromLocationSubPath;
							HybridDictionary hybridDictionary2 = (HybridDictionary)hybridDictionary[configPathFromLocationSubPath];
							if (hybridDictionary2 == null)
							{
								hybridDictionary2 = new HybridDictionary(false);
								hybridDictionary.Add(configPathFromLocationSubPath, hybridDictionary2);
							}
							LocationSectionRecord locationSectionRecord2 = (LocationSectionRecord)hybridDictionary2[locationSectionRecord.ConfigKey];
							FactoryRecord factoryRecord = null;
							if (locationSectionRecord2 == null)
							{
								hybridDictionary2.Add(locationSectionRecord.ConfigKey, locationSectionRecord);
							}
							else
							{
								factoryRecord = this.FindFactoryRecord(locationSectionRecord.ConfigKey, true);
								if (factoryRecord == null || !factoryRecord.IsIgnorable())
								{
									if (!locationSectionRecord2.HasErrors)
									{
										locationSectionRecord2.AddError(new ConfigurationErrorsException(SR.GetString("Config_sections_must_be_unique"), locationSectionRecord2.SectionXmlInfo));
									}
									locationSectionRecord.AddError(new ConfigurationErrorsException(SR.GetString("Config_sections_must_be_unique"), locationSectionRecord.SectionXmlInfo));
								}
							}
							if (factoryRecord == null)
							{
								factoryRecord = this.FindFactoryRecord(locationSectionRecord.ConfigKey, true);
							}
							if (!factoryRecord.HasErrors)
							{
								try
								{
									this.VerifyDefinitionAllowed(factoryRecord, configPathFromLocationSubPath, locationSectionRecord.SectionXmlInfo);
								}
								catch (ConfigurationException e)
								{
									locationSectionRecord.AddError(e);
								}
							}
						}
						BaseConfigurationRecord parent = this._parent;
						while (!parent.IsRootConfig)
						{
							foreach (object obj2 in this._locationSections)
							{
								LocationSectionRecord locationSectionRecord3 = (LocationSectionRecord)obj2;
								bool flag2 = false;
								SectionRecord sectionRecord = parent.GetSectionRecord(locationSectionRecord3.ConfigKey, true);
								if (sectionRecord != null && (sectionRecord.LockChildren || sectionRecord.Locked))
								{
									flag2 = true;
								}
								else if (parent._locationSections != null)
								{
									string targetConfigPath = locationSectionRecord3.SectionXmlInfo.TargetConfigPath;
									foreach (object obj3 in parent._locationSections)
									{
										LocationSectionRecord locationSectionRecord4 = (LocationSectionRecord)obj3;
										string targetConfigPath2 = locationSectionRecord4.SectionXmlInfo.TargetConfigPath;
										if (locationSectionRecord4.SectionXmlInfo.OverrideModeSetting.IsLocked && locationSectionRecord3.ConfigKey == locationSectionRecord4.ConfigKey && UrlPath.IsEqualOrSubpath(targetConfigPath, targetConfigPath2))
										{
											flag2 = true;
											break;
										}
									}
								}
								if (flag2)
								{
									locationSectionRecord3.AddError(new ConfigurationErrorsException(SR.GetString("Config_section_locked"), locationSectionRecord3.SectionXmlInfo));
								}
							}
							parent = parent._parent;
						}
					}
					this._flags[256] = true;
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006234 File Offset: 0x00004434
		private void VerifyDefinitionAllowed(FactoryRecord factoryRecord, string configPath, IConfigErrorInfo errorInfo)
		{
			this.Host.VerifyDefinitionAllowed(configPath, factoryRecord.AllowDefinition, factoryRecord.AllowExeDefinition, errorInfo);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000624F File Offset: 0x0000444F
		internal bool IsDefinitionAllowed(ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return this.Host.IsDefinitionAllowed(this._configPath, allowDefinition, allowExeDefinition);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00006264 File Offset: 0x00004464
		protected static void VerifySectionName(string name, XmlUtil xmlUtil, ExceptionAction action, bool allowImplicit, bool allowConfigNames = false)
		{
			try
			{
				BaseConfigurationRecord.VerifySectionName(name, xmlUtil, allowImplicit, allowConfigNames);
			}
			catch (ConfigurationErrorsException ce)
			{
				xmlUtil.SchemaErrors.AddError(ce, action);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000062A0 File Offset: 0x000044A0
		protected static void VerifySectionName(string name, IConfigErrorInfo errorInfo, bool allowImplicit, bool allowConfigNames = false)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_invalid"), errorInfo);
			}
			try
			{
				XmlConvert.VerifyName(name);
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("Config_tag_name_invalid"), e, errorInfo);
			}
			if (BaseConfigurationRecord.IsImplicitSection(name))
			{
				if (allowImplicit)
				{
					return;
				}
				throw new ConfigurationErrorsException(SR.GetString("Cannot_declare_or_remove_implicit_section", new object[]
				{
					name
				}), errorInfo);
			}
			else
			{
				if (!allowConfigNames && StringUtil.StartsWith(name, "config"))
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_cannot_begin_with_config"), errorInfo);
				}
				if (name == "location")
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_cannot_be_location"), errorInfo);
				}
				return;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000635C File Offset: 0x0000455C
		internal static string NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo)
		{
			if (string.IsNullOrEmpty(subPath))
			{
				return null;
			}
			if (subPath == ".")
			{
				return null;
			}
			string text = subPath.TrimStart(new char[0]);
			if (text.Length != subPath.Length)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_location_path_invalid_first_character"), errorInfo);
			}
			if ("\\./".IndexOf(subPath[0]) != -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_location_path_invalid_first_character"), errorInfo);
			}
			text = subPath.TrimEnd(new char[0]);
			if (text.Length != subPath.Length)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_location_path_invalid_last_character"), errorInfo);
			}
			if ("\\./".IndexOf(subPath[subPath.Length - 1]) != -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_location_path_invalid_last_character"), errorInfo);
			}
			if (subPath.IndexOfAny(BaseConfigurationRecord.s_invalidSubPathCharactersArray) != -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_location_path_invalid_character"), errorInfo);
			}
			return subPath;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000644C File Offset: 0x0000464C
		protected SectionRecord GetSectionRecord(string configKey, bool permitErrors)
		{
			SectionRecord sectionRecord;
			if (this._sectionRecords != null)
			{
				sectionRecord = (SectionRecord)this._sectionRecords[configKey];
			}
			else
			{
				sectionRecord = null;
			}
			if (sectionRecord != null && !permitErrors)
			{
				sectionRecord.ThrowOnErrors();
			}
			return sectionRecord;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006484 File Offset: 0x00004684
		protected SectionRecord EnsureSectionRecord(string configKey, bool permitErrors)
		{
			return this.EnsureSectionRecordImpl(configKey, permitErrors, true);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000648F File Offset: 0x0000468F
		protected SectionRecord EnsureSectionRecordUnsafe(string configKey, bool permitErrors)
		{
			return this.EnsureSectionRecordImpl(configKey, permitErrors, false);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000649C File Offset: 0x0000469C
		private SectionRecord EnsureSectionRecordImpl(string configKey, bool permitErrors, bool setLockSettings)
		{
			SectionRecord sectionRecord = this.GetSectionRecord(configKey, permitErrors);
			if (sectionRecord == null)
			{
				lock (this)
				{
					if (this._sectionRecords == null)
					{
						this._sectionRecords = new Hashtable();
					}
					else
					{
						sectionRecord = this.GetSectionRecord(configKey, permitErrors);
					}
					if (sectionRecord == null)
					{
						sectionRecord = new SectionRecord(configKey);
						this._sectionRecords.Add(configKey, sectionRecord);
					}
				}
				if (setLockSettings)
				{
					OverrideMode forChildren = OverrideMode.Inherit;
					OverrideMode forSelf = this.ResolveOverrideModeFromParent(configKey, out forChildren);
					sectionRecord.ChangeLockSettings(forSelf, forChildren);
				}
			}
			return sectionRecord;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00006530 File Offset: 0x00004730
		private bool HasFactoryRecords
		{
			get
			{
				return this._factoryRecords != null;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000653C File Offset: 0x0000473C
		internal FactoryRecord GetFactoryRecord(string configKey, bool permitErrors)
		{
			if (this._factoryRecords == null)
			{
				return null;
			}
			FactoryRecord factoryRecord = (FactoryRecord)this._factoryRecords[configKey];
			if (factoryRecord != null && !permitErrors)
			{
				factoryRecord.ThrowOnErrors();
			}
			return factoryRecord;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00006572 File Offset: 0x00004772
		protected Hashtable EnsureFactories()
		{
			if (this._factoryRecords == null)
			{
				this._factoryRecords = new Hashtable();
			}
			return this._factoryRecords;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000658D File Offset: 0x0000478D
		private ArrayList EnsureLocationSections()
		{
			if (this._locationSections == null)
			{
				this._locationSections = new ArrayList();
			}
			return this._locationSections;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000065A8 File Offset: 0x000047A8
		internal bool IsEmpty
		{
			get
			{
				return this._parent != null && !this._initErrors.HasErrors(false) && (this._sectionRecords == null || this._sectionRecords.Count == 0) && (this._factoryRecords == null || this._factoryRecords.Count == 0) && (this._locationSections == null || this._locationSections.Count == 0);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006610 File Offset: 0x00004810
		internal static string NormalizeConfigSource(string configSource, IConfigErrorInfo errorInfo)
		{
			if (string.IsNullOrEmpty(configSource))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_source_invalid_format"), errorInfo);
			}
			string text = configSource.Trim();
			if (text.Length != configSource.Length)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_source_invalid_format"), errorInfo);
			}
			if (configSource.IndexOf('/') != -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_source_invalid_chars"), errorInfo);
			}
			if (string.IsNullOrEmpty(configSource) || Path.IsPathRooted(configSource))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_source_invalid_format"), errorInfo);
			}
			return configSource;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000669C File Offset: 0x0000489C
		protected object MonitorStream(string configKey, string configSource, string streamname)
		{
			lock (this)
			{
				if (this._flags[2])
				{
					return null;
				}
				StreamInfo streamInfo = (StreamInfo)this.ConfigStreamInfo.StreamInfos[streamname];
				if (streamInfo != null)
				{
					if (streamInfo.SectionName != configKey)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_source_cannot_be_shared", new object[]
						{
							streamname
						}));
					}
					if (streamInfo.IsMonitored)
					{
						return streamInfo.Version;
					}
				}
				else
				{
					streamInfo = new StreamInfo(configKey, configSource, streamname);
					this.ConfigStreamInfo.StreamInfos.Add(streamname, streamInfo);
				}
			}
			object streamVersion = this.Host.GetStreamVersion(streamname);
			StreamChangeCallback callback = null;
			lock (this)
			{
				if (this._flags[2])
				{
					return null;
				}
				StreamInfo streamInfo2 = (StreamInfo)this.ConfigStreamInfo.StreamInfos[streamname];
				if (streamInfo2.IsMonitored)
				{
					return streamInfo2.Version;
				}
				streamInfo2.IsMonitored = true;
				streamInfo2.Version = streamVersion;
				if (this._flags[65536])
				{
					if (this.ConfigStreamInfo.CallbackDelegate == null)
					{
						this.ConfigStreamInfo.CallbackDelegate = new StreamChangeCallback(this.OnStreamChanged);
					}
					callback = this.ConfigStreamInfo.CallbackDelegate;
				}
			}
			if (this._flags[65536])
			{
				this.Host.StartMonitoringStreamForChanges(streamname, callback);
			}
			return streamVersion;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006854 File Offset: 0x00004A54
		private void OnStreamChanged(string streamname)
		{
			string sectionName;
			lock (this)
			{
				if (this._flags[2])
				{
					return;
				}
				StreamInfo streamInfo = (StreamInfo)this.ConfigStreamInfo.StreamInfos[streamname];
				if (streamInfo == null || !streamInfo.IsMonitored)
				{
					return;
				}
				sectionName = streamInfo.SectionName;
			}
			bool flag2;
			if (sectionName == null)
			{
				flag2 = true;
			}
			else
			{
				FactoryRecord factoryRecord = this.FindFactoryRecord(sectionName, false);
				flag2 = factoryRecord.RestartOnExternalChanges;
			}
			if (flag2)
			{
				this._configRoot.FireConfigChanged(this._configPath);
				return;
			}
			this._configRoot.ClearResult(this, sectionName, false);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006904 File Offset: 0x00004B04
		private void ValidateUniqueConfigSource(string configKey, string configSourceStreamName, string configSourceArg, IConfigErrorInfo errorInfo)
		{
			lock (this)
			{
				if (this.ConfigStreamInfo.HasStreamInfos)
				{
					StreamInfo streamInfo = (StreamInfo)this.ConfigStreamInfo.StreamInfos[configSourceStreamName];
					if (streamInfo != null && streamInfo.SectionName != configKey)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_source_cannot_be_shared", new object[]
						{
							configSourceArg
						}), errorInfo);
					}
				}
			}
			this.ValidateUniqueChildConfigSource(configKey, configSourceStreamName, configSourceArg, errorInfo);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006998 File Offset: 0x00004B98
		protected void ValidateUniqueChildConfigSource(string configKey, string configSourceStreamName, string configSourceArg, IConfigErrorInfo errorInfo)
		{
			BaseConfigurationRecord parent;
			if (this.IsLocationConfig)
			{
				parent = this._parent._parent;
			}
			else
			{
				parent = this._parent;
			}
			while (!parent.IsRootConfig)
			{
				BaseConfigurationRecord obj = parent;
				lock (obj)
				{
					if (parent.ConfigStreamInfo.HasStreamInfos)
					{
						StreamInfo streamInfo = (StreamInfo)parent.ConfigStreamInfo.StreamInfos[configSourceStreamName];
						if (streamInfo != null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_source_parent_conflict", new object[]
							{
								configSourceArg
							}), errorInfo);
						}
					}
				}
				parent = parent.Parent;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006A40 File Offset: 0x00004C40
		internal void hlClearResultRecursive(string configKey, bool forceEvaluatation)
		{
			this.RefreshFactoryRecord(configKey);
			SectionRecord sectionRecord = this.GetSectionRecord(configKey, false);
			if (sectionRecord != null)
			{
				sectionRecord.ClearResult();
				sectionRecord.ClearRawXml();
			}
			if (forceEvaluatation && !this.IsInitDelayed && !string.IsNullOrEmpty(this.ConfigStreamInfo.StreamName))
			{
				if (this._flags[262144])
				{
					throw ExceptionUtil.UnexpectedError("BaseConfigurationRecord::hlClearResultRecursive");
				}
				FactoryRecord factoryRecord = this.FindFactoryRecord(configKey, false);
				if (factoryRecord != null && !factoryRecord.IsGroup)
				{
					configKey = factoryRecord.ConfigKey;
					sectionRecord = this.EnsureSectionRecord(configKey, false);
					if (!sectionRecord.HasFileInput)
					{
						SectionXmlInfo sectionXmlInfo = new SectionXmlInfo(configKey, this._configPath, this._configPath, null, this.ConfigStreamInfo.StreamName, 0, null, null, null, null, null, null, null, OverrideModeSetting.LocationDefault, false);
						SectionInput sectionInput = new SectionInput(sectionXmlInfo, null);
						sectionRecord.AddFileInput(sectionInput);
					}
				}
			}
			if (this._children != null)
			{
				IEnumerable values = this._children.Values;
				foreach (object obj in values)
				{
					BaseConfigurationRecord baseConfigurationRecord = (BaseConfigurationRecord)obj;
					baseConfigurationRecord.hlClearResultRecursive(configKey, forceEvaluatation);
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006B80 File Offset: 0x00004D80
		internal BaseConfigurationRecord hlGetChild(string configName)
		{
			if (this._children == null)
			{
				return null;
			}
			return (BaseConfigurationRecord)this._children[configName];
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006B9D File Offset: 0x00004D9D
		internal void hlAddChild(string configName, BaseConfigurationRecord child)
		{
			if (this._children == null)
			{
				this._children = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			this._children.Add(configName, child);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006BC4 File Offset: 0x00004DC4
		internal void hlRemoveChild(string configName)
		{
			if (this._children != null)
			{
				this._children.Remove(configName);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006BDC File Offset: 0x00004DDC
		internal bool hlNeedsChildFor(string configName)
		{
			if (this.IsRootConfig)
			{
				return true;
			}
			if (this.HasInitErrors)
			{
				return false;
			}
			string text = ConfigPathUtility.Combine(this._configPath, configName);
			try
			{
				using (this.Impersonate())
				{
					if (this.Host.IsConfigRecordRequired(text))
					{
						return true;
					}
				}
			}
			catch
			{
				throw;
			}
			if (this._flags[1048576])
			{
				BaseConfigurationRecord baseConfigurationRecord = this;
				while (!baseConfigurationRecord.IsRootConfig)
				{
					if (baseConfigurationRecord._locationSections != null)
					{
						baseConfigurationRecord.ResolveLocationSections();
						foreach (object obj in baseConfigurationRecord._locationSections)
						{
							LocationSectionRecord locationSectionRecord = (LocationSectionRecord)obj;
							if (UrlPath.IsEqualOrSubpath(text, locationSectionRecord.SectionXmlInfo.TargetConfigPath))
							{
								return true;
							}
						}
					}
					baseConfigurationRecord = baseConfigurationRecord._parent;
				}
			}
			return false;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006CEC File Offset: 0x00004EEC
		internal void CloseRecursive()
		{
			if (!this._flags[2])
			{
				bool flag = false;
				HybridDictionary hybridDictionary = null;
				StreamChangeCallback callback = null;
				lock (this)
				{
					if (!this._flags[2])
					{
						this._flags[2] = true;
						flag = true;
						if (!this.IsLocationConfig && this.ConfigStreamInfo.HasStreamInfos)
						{
							callback = this.ConfigStreamInfo.CallbackDelegate;
							hybridDictionary = this.ConfigStreamInfo.StreamInfos;
							this.ConfigStreamInfo.CallbackDelegate = null;
							this.ConfigStreamInfo.ClearStreamInfos();
						}
					}
				}
				if (flag)
				{
					if (this._children != null)
					{
						foreach (object obj in this._children.Values)
						{
							BaseConfigurationRecord baseConfigurationRecord = (BaseConfigurationRecord)obj;
							baseConfigurationRecord.CloseRecursive();
						}
					}
					if (hybridDictionary != null)
					{
						foreach (object obj2 in hybridDictionary.Values)
						{
							StreamInfo streamInfo = (StreamInfo)obj2;
							if (streamInfo.IsMonitored)
							{
								this.Host.StopMonitoringStreamForChanges(streamInfo.StreamName, callback);
								streamInfo.IsMonitored = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006E70 File Offset: 0x00005070
		internal string FindChangedConfigurationStream()
		{
			BaseConfigurationRecord baseConfigurationRecord = this;
			while (!baseConfigurationRecord.IsRootConfig)
			{
				BaseConfigurationRecord obj = baseConfigurationRecord;
				lock (obj)
				{
					if (baseConfigurationRecord.ConfigStreamInfo.HasStreamInfos)
					{
						foreach (object obj2 in baseConfigurationRecord.ConfigStreamInfo.StreamInfos.Values)
						{
							StreamInfo streamInfo = (StreamInfo)obj2;
							if (streamInfo.IsMonitored && this.HasStreamChanged(streamInfo.StreamName, streamInfo.Version))
							{
								return streamInfo.StreamName;
							}
						}
					}
				}
				baseConfigurationRecord = baseConfigurationRecord._parent;
			}
			return null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006F48 File Offset: 0x00005148
		private bool HasStreamChanged(string streamname, object lastVersion)
		{
			object streamVersion = this.Host.GetStreamVersion(streamname);
			if (lastVersion != null)
			{
				return streamVersion == null || !lastVersion.Equals(streamVersion);
			}
			return streamVersion != null;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006F79 File Offset: 0x00005179
		protected virtual string CallHostDecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfig)
		{
			return this.Host.DecryptSection(encryptedXml, protectionProvider, protectedConfig);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006F89 File Offset: 0x00005189
		protected virtual XmlNode CallHostProcessRawXml(XmlNode rawXml, ConfigurationBuilder configBuilder)
		{
			if (this.ConfigBuilderHost != null)
			{
				return this.ConfigBuilderHost.ProcessRawXml(rawXml, configBuilder);
			}
			return rawXml;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006FA4 File Offset: 0x000051A4
		protected virtual ConfigurationSection CallHostProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder configBuilder)
		{
			if (this.ConfigBuilderHost != null)
			{
				try
				{
					return this.ConfigBuilderHost.ProcessConfigurationSection(configSection, configBuilder);
				}
				catch (Exception e)
				{
					throw ExceptionUtil.WrapAsConfigException(SR.GetString("ConfigBuilder_processSection_error", new object[]
					{
						configBuilder.Name,
						configSection.SectionInformation.Name
					}), e, null);
				}
				return configSection;
			}
			return configSection;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000700C File Offset: 0x0000520C
		internal static string ValidateConfigBuilderAttribute(string configBuilder, IConfigErrorInfo errorInfo)
		{
			if (string.IsNullOrEmpty(configBuilder))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_builder_invalid_format"), errorInfo);
			}
			return configBuilder;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007028 File Offset: 0x00005228
		internal static string ValidateProtectionProviderAttribute(string protectionProvider, IConfigErrorInfo errorInfo)
		{
			if (string.IsNullOrEmpty(protectionProvider))
			{
				throw new ConfigurationErrorsException(SR.GetString("Protection_provider_invalid_format"), errorInfo);
			}
			return protectionProvider;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007044 File Offset: 0x00005244
		private ConfigXmlReader DecryptConfigSection(ConfigXmlReader reader, ProtectedConfigurationProvider protectionProvider)
		{
			ConfigXmlReader configXmlReader = reader.Clone();
			IConfigErrorInfo configErrorInfo = configXmlReader;
			string rawXml = null;
			configXmlReader.Read();
			string filename = configErrorInfo.Filename;
			int lineNumber = configErrorInfo.LineNumber;
			int lineOffset = lineNumber;
			if (configXmlReader.IsEmptyElement)
			{
				throw new ConfigurationErrorsException(SR.GetString("EncryptedNode_not_found"), filename, lineNumber);
			}
			for (;;)
			{
				configXmlReader.Read();
				XmlNodeType nodeType = configXmlReader.NodeType;
				if (nodeType == XmlNodeType.Element && configXmlReader.Name == "EncryptedData")
				{
					goto IL_A3;
				}
				if (nodeType == XmlNodeType.EndElement)
				{
					break;
				}
				if (nodeType != XmlNodeType.Comment && nodeType != XmlNodeType.Whitespace)
				{
					goto Block_5;
				}
			}
			throw new ConfigurationErrorsException(SR.GetString("EncryptedNode_not_found"), filename, lineNumber);
			Block_5:
			throw new ConfigurationErrorsException(SR.GetString("EncryptedNode_is_in_invalid_format"), filename, lineNumber);
			IL_A3:
			lineNumber = configErrorInfo.LineNumber;
			string encryptedXml = configXmlReader.ReadOuterXml();
			try
			{
				rawXml = this.CallHostDecryptSection(encryptedXml, protectionProvider, this.ProtectedConfig);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Decryption_failed", new object[]
				{
					protectionProvider.Name,
					ex.Message
				}), ex, filename, lineNumber);
			}
			for (;;)
			{
				XmlNodeType nodeType = configXmlReader.NodeType;
				if (nodeType == XmlNodeType.EndElement)
				{
					goto IL_129;
				}
				if (nodeType != XmlNodeType.Comment && nodeType != XmlNodeType.Whitespace)
				{
					break;
				}
				if (!configXmlReader.Read())
				{
					goto IL_129;
				}
			}
			throw new ConfigurationErrorsException(SR.GetString("EncryptedNode_is_in_invalid_format"), filename, lineNumber);
			IL_129:
			return new ConfigXmlReader(rawXml, filename, lineOffset, true);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007198 File Offset: 0x00005398
		private ConfigXmlReader ProcessRawXml(ConfigXmlReader reader, ConfigurationBuilder configBuilder)
		{
			XmlNode xmlNode = null;
			string filename = ((IConfigErrorInfo)reader).Filename;
			int lineNumber = ((IConfigErrorInfo)reader).LineNumber;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.Load(reader);
				xmlNode = this.CallHostProcessRawXml(xmlDocument.DocumentElement, configBuilder);
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("ConfigBuilder_processXml_error_short", new object[]
				{
					configBuilder.Name
				}), e, null);
			}
			return new ConfigXmlReader(xmlNode.OuterXml, filename, lineNumber, true);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00007224 File Offset: 0x00005424
		internal object ConfigContext
		{
			get
			{
				if (!this._flags[128])
				{
					this._configContext = this.Host.CreateConfigurationContext(this.ConfigPath, this.LocationSubPath);
					this._flags[128] = true;
				}
				return this._configContext;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007278 File Offset: 0x00005478
		private void ThrowIfParseErrors(ConfigurationSchemaErrors schemaErrors)
		{
			schemaErrors.ThrowIfErrors(this.ClassFlags[64]);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000729B File Offset: 0x0000549B
		internal bool RecordSupportsLocation
		{
			get
			{
				return this._flags[1048576] || this.IsMachineConfig;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000072B7 File Offset: 0x000054B7
		internal static bool IsImplicitSection(string configKey)
		{
			return string.Equals(configKey, "configProtectedData", StringComparison.Ordinal) || string.Equals(configKey, "System.Windows.Forms.ApplicationConfigurationSection", StringComparison.Ordinal);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000072D8 File Offset: 0x000054D8
		private void AddImplicitSections(Hashtable factoryList)
		{
			if (this._parent.IsRootConfig)
			{
				if (factoryList == null)
				{
					factoryList = this.EnsureFactories();
				}
				if ((FactoryRecord)factoryList["configProtectedData"] == null)
				{
					factoryList["configProtectedData"] = new FactoryRecord("configProtectedData", string.Empty, "configProtectedData", "System.Configuration.ProtectedConfigurationSection, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true, ConfigurationAllowDefinition.Everywhere, ConfigurationAllowExeDefinition.MachineToApplication, OverrideModeSetting.SectionDefault, true, true, true, true, null, -1);
				}
				if ((FactoryRecord)factoryList["System.Windows.Forms.ApplicationConfigurationSection"] == null)
				{
					factoryList["System.Windows.Forms.ApplicationConfigurationSection"] = new FactoryRecord("System.Windows.Forms.ApplicationConfigurationSection", string.Empty, "System.Windows.Forms.ApplicationConfigurationSection", "System.Configuration.AppSettingsSection, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true, ConfigurationAllowDefinition.Everywhere, ConfigurationAllowExeDefinition.MachineToApplication, OverrideModeSetting.SectionDefault, true, true, true, true, null, -1);
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007396 File Offset: 0x00005596
		internal static bool IsReservedAttributeName(string name)
		{
			return StringUtil.StartsWith(name, "config") || StringUtil.StartsWith(name, "lock");
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000073B5 File Offset: 0x000055B5
		internal Configuration CurrentConfiguration
		{
			get
			{
				return this._configRoot.CurrentConfiguration;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000073C2 File Offset: 0x000055C2
		internal bool TypeStringTransformerIsSet
		{
			get
			{
				return this.CurrentConfiguration != null && this.CurrentConfiguration.TypeStringTransformerIsSet;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000073D9 File Offset: 0x000055D9
		internal bool AssemblyStringTransformerIsSet
		{
			get
			{
				return this.CurrentConfiguration != null && this.CurrentConfiguration.AssemblyStringTransformerIsSet;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000073F0 File Offset: 0x000055F0
		internal Func<string, string> TypeStringTransformer
		{
			get
			{
				if (this.CurrentConfiguration != null)
				{
					return this.CurrentConfiguration.TypeStringTransformer;
				}
				return null;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00007407 File Offset: 0x00005607
		internal Func<string, string> AssemblyStringTransformer
		{
			get
			{
				if (this.CurrentConfiguration != null)
				{
					return this.CurrentConfiguration.AssemblyStringTransformer;
				}
				return null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000741E File Offset: 0x0000561E
		internal FrameworkName TargetFramework
		{
			get
			{
				if (this.CurrentConfiguration != null)
				{
					return this.CurrentConfiguration.TargetFramework;
				}
				return null;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00007435 File Offset: 0x00005635
		internal Stack SectionsStack
		{
			get
			{
				if (this.CurrentConfiguration != null)
				{
					return this.CurrentConfiguration.SectionsStack;
				}
				return new Stack();
			}
		}

		// Token: 0x040000C0 RID: 192
		protected const string NL = "\r\n";

		// Token: 0x040000C1 RID: 193
		internal const string KEYWORD_TRUE = "true";

		// Token: 0x040000C2 RID: 194
		internal const string KEYWORD_FALSE = "false";

		// Token: 0x040000C3 RID: 195
		protected const string KEYWORD_CONFIGURATION = "configuration";

		// Token: 0x040000C4 RID: 196
		protected const string KEYWORD_CONFIGURATION_NAMESPACE = "http://schemas.microsoft.com/.NetConfiguration/v2.0";

		// Token: 0x040000C5 RID: 197
		protected const string KEYWORD_CONFIGSECTIONS = "configSections";

		// Token: 0x040000C6 RID: 198
		protected const string KEYWORD_SECTION = "section";

		// Token: 0x040000C7 RID: 199
		protected const string KEYWORD_SECTION_NAME = "name";

		// Token: 0x040000C8 RID: 200
		protected const string KEYWORD_SECTION_TYPE = "type";

		// Token: 0x040000C9 RID: 201
		protected const string KEYWORD_SECTION_ALLOWLOCATION = "allowLocation";

		// Token: 0x040000CA RID: 202
		protected const string KEYWORD_SECTION_ALLOWDEFINITION = "allowDefinition";

		// Token: 0x040000CB RID: 203
		protected const string KEYWORD_SECTION_ALLOWDEFINITION_EVERYWHERE = "Everywhere";

		// Token: 0x040000CC RID: 204
		protected const string KEYWORD_SECTION_ALLOWDEFINITION_MACHINEONLY = "MachineOnly";

		// Token: 0x040000CD RID: 205
		protected const string KEYWORD_SECTION_ALLOWDEFINITION_MACHINETOAPPLICATION = "MachineToApplication";

		// Token: 0x040000CE RID: 206
		protected const string KEYWORD_SECTION_ALLOWDEFINITION_MACHINETOWEBROOT = "MachineToWebRoot";

		// Token: 0x040000CF RID: 207
		protected const string KEYWORD_SECTION_ALLOWEXEDEFINITION = "allowExeDefinition";

		// Token: 0x040000D0 RID: 208
		protected const string KEYWORD_SECTION_ALLOWEXEDEFINITION_MACHTOROAMING = "MachineToRoamingUser";

		// Token: 0x040000D1 RID: 209
		protected const string KEYWORD_SECTION_ALLOWEXEDEFINITION_MACHTOLOCAL = "MachineToLocalUser";

		// Token: 0x040000D2 RID: 210
		protected const string KEYWORD_SECTION_RESTARTONEXTERNALCHANGES = "restartOnExternalChanges";

		// Token: 0x040000D3 RID: 211
		protected const string KEYWORD_SECTION_REQUIREPERMISSION = "requirePermission";

		// Token: 0x040000D4 RID: 212
		protected const string KEYWORD_SECTIONGROUP = "sectionGroup";

		// Token: 0x040000D5 RID: 213
		protected const string KEYWORD_SECTIONGROUP_NAME = "name";

		// Token: 0x040000D6 RID: 214
		protected const string KEYWORD_SECTIONGROUP_TYPE = "type";

		// Token: 0x040000D7 RID: 215
		protected const string KEYWORD_REMOVE = "remove";

		// Token: 0x040000D8 RID: 216
		protected const string KEYWORD_CLEAR = "clear";

		// Token: 0x040000D9 RID: 217
		protected const string KEYWORD_LOCATION = "location";

		// Token: 0x040000DA RID: 218
		protected const string KEYWORD_LOCATION_PATH = "path";

		// Token: 0x040000DB RID: 219
		internal const string KEYWORD_LOCATION_ALLOWOVERRIDE = "allowOverride";

		// Token: 0x040000DC RID: 220
		protected const string KEYWORD_LOCATION_INHERITINCHILDAPPLICATIONS = "inheritInChildApplications";

		// Token: 0x040000DD RID: 221
		protected const string KEYWORD_CONFIGSOURCE = "configSource";

		// Token: 0x040000DE RID: 222
		protected const string KEYWORD_XMLNS = "xmlns";

		// Token: 0x040000DF RID: 223
		protected const string KEYWORD_CONFIG_BUILDER = "configBuilders";

		// Token: 0x040000E0 RID: 224
		internal const string KEYWORD_PROTECTION_PROVIDER = "configProtectionProvider";

		// Token: 0x040000E1 RID: 225
		protected const string FORMAT_NEWCONFIGFILE = "<?xml version=\"1.0\" encoding=\"{0}\"?>\r\n";

		// Token: 0x040000E2 RID: 226
		protected const string FORMAT_CONFIGURATION = "<configuration>\r\n";

		// Token: 0x040000E3 RID: 227
		protected const string FORMAT_CONFIGURATION_NAMESPACE = "<configuration xmlns=\"{0}\">\r\n";

		// Token: 0x040000E4 RID: 228
		protected const string FORMAT_CONFIGURATION_ENDELEMENT = "</configuration>";

		// Token: 0x040000E5 RID: 229
		internal const string KEYWORD_SECTION_OVERRIDEMODEDEFAULT = "overrideModeDefault";

		// Token: 0x040000E6 RID: 230
		internal const string KEYWORD_LOCATION_OVERRIDEMODE = "overrideMode";

		// Token: 0x040000E7 RID: 231
		internal const string KEYWORD_OVERRIDEMODE_INHERIT = "Inherit";

		// Token: 0x040000E8 RID: 232
		internal const string KEYWORD_OVERRIDEMODE_ALLOW = "Allow";

		// Token: 0x040000E9 RID: 233
		internal const string KEYWORD_OVERRIDEMODE_DENY = "Deny";

		// Token: 0x040000EA RID: 234
		protected const string FORMAT_LOCATION_NOPATH = "<location {0} inheritInChildApplications=\"{1}\">\r\n";

		// Token: 0x040000EB RID: 235
		protected const string FORMAT_LOCATION_PATH = "<location path=\"{2}\" {0} inheritInChildApplications=\"{1}\">\r\n";

		// Token: 0x040000EC RID: 236
		protected const string FORMAT_LOCATION_ENDELEMENT = "</location>";

		// Token: 0x040000ED RID: 237
		internal const string KEYWORD_LOCATION_OVERRIDEMODE_STRING = "{0}=\"{1}\"";

		// Token: 0x040000EE RID: 238
		protected const string FORMAT_SECTION_CONFIGSOURCE = "<{0} configSource=\"{1}\" />";

		// Token: 0x040000EF RID: 239
		protected const string FORMAT_CONFIGSOURCE_FILE = "<?xml version=\"1.0\" encoding=\"{0}\"?>\r\n";

		// Token: 0x040000F0 RID: 240
		protected const string FORMAT_SECTIONGROUP_ENDELEMENT = "</sectionGroup>";

		// Token: 0x040000F1 RID: 241
		protected const int ClassSupportsChangeNotifications = 1;

		// Token: 0x040000F2 RID: 242
		protected const int ClassSupportsRefresh = 2;

		// Token: 0x040000F3 RID: 243
		protected const int ClassSupportsImpersonation = 4;

		// Token: 0x040000F4 RID: 244
		protected const int ClassSupportsRestrictedPermissions = 8;

		// Token: 0x040000F5 RID: 245
		protected const int ClassSupportsKeepInputs = 16;

		// Token: 0x040000F6 RID: 246
		protected const int ClassSupportsDelayedInit = 32;

		// Token: 0x040000F7 RID: 247
		protected const int ClassIgnoreLocalErrors = 64;

		// Token: 0x040000F8 RID: 248
		protected const int ProtectedDataInitialized = 1;

		// Token: 0x040000F9 RID: 249
		protected const int Closed = 2;

		// Token: 0x040000FA RID: 250
		protected const int PrefetchAll = 8;

		// Token: 0x040000FB RID: 251
		protected const int IsAboveApplication = 32;

		// Token: 0x040000FC RID: 252
		private const int ContextEvaluated = 128;

		// Token: 0x040000FD RID: 253
		private const int IsLocationListResolved = 256;

		// Token: 0x040000FE RID: 254
		protected const int NamespacePresentInFile = 512;

		// Token: 0x040000FF RID: 255
		private const int RestrictedPermissionsResolved = 2048;

		// Token: 0x04000100 RID: 256
		protected const int IsTrusted = 8192;

		// Token: 0x04000101 RID: 257
		protected const int SupportsChangeNotifications = 65536;

		// Token: 0x04000102 RID: 258
		protected const int SupportsRefresh = 131072;

		// Token: 0x04000103 RID: 259
		protected const int SupportsPath = 262144;

		// Token: 0x04000104 RID: 260
		protected const int SupportsKeepInputs = 524288;

		// Token: 0x04000105 RID: 261
		protected const int SupportsLocation = 1048576;

		// Token: 0x04000106 RID: 262
		protected const int ForceLocationWritten = 16777216;

		// Token: 0x04000107 RID: 263
		protected const int SuggestLocationRemoval = 33554432;

		// Token: 0x04000108 RID: 264
		protected const int NamespacePresentCurrent = 67108864;

		// Token: 0x04000109 RID: 265
		protected const int ConfigBuildersInitialized = 134217728;

		// Token: 0x0400010A RID: 266
		internal const char ConfigPathSeparatorChar = '/';

		// Token: 0x0400010B RID: 267
		internal const string ConfigPathSeparatorString = "/";

		// Token: 0x0400010C RID: 268
		internal static readonly char[] ConfigPathSeparatorParams = new char[]
		{
			'/'
		};

		// Token: 0x0400010D RID: 269
		private static volatile ConfigurationPermission s_unrestrictedConfigPermission;

		// Token: 0x0400010E RID: 270
		protected SafeBitVector32 _flags;

		// Token: 0x0400010F RID: 271
		protected BaseConfigurationRecord _parent;

		// Token: 0x04000110 RID: 272
		protected Hashtable _children;

		// Token: 0x04000111 RID: 273
		protected InternalConfigRoot _configRoot;

		// Token: 0x04000112 RID: 274
		protected string _configName;

		// Token: 0x04000113 RID: 275
		protected string _configPath;

		// Token: 0x04000114 RID: 276
		protected string _locationSubPath;

		// Token: 0x04000115 RID: 277
		private BaseConfigurationRecord.ConfigRecordStreamInfo _configStreamInfo;

		// Token: 0x04000116 RID: 278
		private object _configContext;

		// Token: 0x04000117 RID: 279
		private ConfigurationBuildersSection _configBuilders;

		// Token: 0x04000118 RID: 280
		private ProtectedConfigurationSection _protectedConfig;

		// Token: 0x04000119 RID: 281
		private PermissionSet _restrictedPermissions;

		// Token: 0x0400011A RID: 282
		private ConfigurationSchemaErrors _initErrors;

		// Token: 0x0400011B RID: 283
		private BaseConfigurationRecord _initDelayedRoot;

		// Token: 0x0400011C RID: 284
		protected Hashtable _factoryRecords;

		// Token: 0x0400011D RID: 285
		protected Hashtable _sectionRecords;

		// Token: 0x0400011E RID: 286
		protected ArrayList _locationSections;

		// Token: 0x0400011F RID: 287
		private static string s_appConfigPath;

		// Token: 0x04000120 RID: 288
		private static IComparer<SectionInput> s_indirectInputsComparer = new BaseConfigurationRecord.IndirectLocationInputComparer();

		// Token: 0x04000121 RID: 289
		private const string invalidFirstSubPathCharacters = "\\./";

		// Token: 0x04000122 RID: 290
		private const string invalidLastSubPathCharacters = "\\./";

		// Token: 0x04000123 RID: 291
		private const string invalidSubPathCharactersString = "\\?:*\"<>|";

		// Token: 0x04000124 RID: 292
		private static char[] s_invalidSubPathCharactersArray = "\\?:*\"<>|".ToCharArray();

		// Token: 0x04000125 RID: 293
		private const string ConfigurationBuildersSectionTypeName = "System.Configuration.ConfigurationBuildersSection, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000126 RID: 294
		internal const string RESERVED_SECTION_CONFIGURATION_BUILDERS = "configBuilders";

		// Token: 0x04000127 RID: 295
		private Type ConfigurationBuildersSectionType = Type.GetType("System.Configuration.ConfigurationBuildersSection, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

		// Token: 0x04000128 RID: 296
		private const string ProtectedConfigurationSectionTypeName = "System.Configuration.ProtectedConfigurationSection, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000129 RID: 297
		internal const string RESERVED_SECTION_PROTECTED_CONFIGURATION = "configProtectedData";

		// Token: 0x0400012A RID: 298
		internal const string WINFORMS_CONFIGURATION_SECTION = "System.Windows.Forms.ApplicationConfigurationSection";

		// Token: 0x0400012B RID: 299
		private const string SystemConfigurationSectionTypeName = "System.Configuration.AppSettingsSection, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x020000C7 RID: 199
		protected class ConfigRecordStreamInfo
		{
			// Token: 0x060007CA RID: 1994 RVA: 0x00020918 File Offset: 0x0001EB18
			internal ConfigRecordStreamInfo()
			{
				this._encoding = Encoding.UTF8;
			}

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x060007CB RID: 1995 RVA: 0x0002092B File Offset: 0x0001EB2B
			// (set) Token: 0x060007CC RID: 1996 RVA: 0x00020933 File Offset: 0x0001EB33
			internal bool HasStream
			{
				get
				{
					return this._hasStream;
				}
				set
				{
					this._hasStream = value;
				}
			}

			// Token: 0x17000242 RID: 578
			// (get) Token: 0x060007CD RID: 1997 RVA: 0x0002093C File Offset: 0x0001EB3C
			// (set) Token: 0x060007CE RID: 1998 RVA: 0x00020944 File Offset: 0x0001EB44
			internal string StreamName
			{
				get
				{
					return this._streamname;
				}
				set
				{
					this._streamname = value;
				}
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x060007CF RID: 1999 RVA: 0x0002094D File Offset: 0x0001EB4D
			// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00020955 File Offset: 0x0001EB55
			internal object StreamVersion
			{
				get
				{
					return this._streamVersion;
				}
				set
				{
					this._streamVersion = value;
				}
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0002095E File Offset: 0x0001EB5E
			// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00020966 File Offset: 0x0001EB66
			internal Encoding StreamEncoding
			{
				get
				{
					return this._encoding;
				}
				set
				{
					this._encoding = value;
				}
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0002096F File Offset: 0x0001EB6F
			// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00020977 File Offset: 0x0001EB77
			internal StreamChangeCallback CallbackDelegate
			{
				get
				{
					return this._callbackDelegate;
				}
				set
				{
					this._callbackDelegate = value;
				}
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00020980 File Offset: 0x0001EB80
			internal HybridDictionary StreamInfos
			{
				get
				{
					if (this._streamInfos == null)
					{
						this._streamInfos = new HybridDictionary(true);
					}
					return this._streamInfos;
				}
			}

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0002099C File Offset: 0x0001EB9C
			internal bool HasStreamInfos
			{
				get
				{
					return this._streamInfos != null;
				}
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x000209A7 File Offset: 0x0001EBA7
			internal void ClearStreamInfos()
			{
				this._streamInfos = null;
			}

			// Token: 0x04000478 RID: 1144
			private bool _hasStream;

			// Token: 0x04000479 RID: 1145
			private string _streamname;

			// Token: 0x0400047A RID: 1146
			private object _streamVersion;

			// Token: 0x0400047B RID: 1147
			private Encoding _encoding;

			// Token: 0x0400047C RID: 1148
			private StreamChangeCallback _callbackDelegate;

			// Token: 0x0400047D RID: 1149
			private HybridDictionary _streamInfos;
		}

		// Token: 0x020000C8 RID: 200
		private class IndirectLocationInputComparer : IComparer<SectionInput>
		{
			// Token: 0x060007D8 RID: 2008 RVA: 0x000209B0 File Offset: 0x0001EBB0
			public int Compare(SectionInput x, SectionInput y)
			{
				if (x == y)
				{
					return 0;
				}
				string targetConfigPath = x.SectionXmlInfo.TargetConfigPath;
				string targetConfigPath2 = y.SectionXmlInfo.TargetConfigPath;
				if (UrlPath.IsSubpath(targetConfigPath, targetConfigPath2))
				{
					return 1;
				}
				if (UrlPath.IsSubpath(targetConfigPath2, targetConfigPath))
				{
					return -1;
				}
				string definitionConfigPath = x.SectionXmlInfo.DefinitionConfigPath;
				string definitionConfigPath2 = y.SectionXmlInfo.DefinitionConfigPath;
				if (UrlPath.IsSubpath(definitionConfigPath, definitionConfigPath2))
				{
					return 1;
				}
				if (UrlPath.IsSubpath(definitionConfigPath2, definitionConfigPath))
				{
					return -1;
				}
				return 0;
			}
		}
	}
}
