using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Management;

namespace System.Web.Configuration
{
	// Token: 0x020006EF RID: 1775
	internal class HealthMonitoringSectionHelper
	{
		// Token: 0x06005534 RID: 21812 RVA: 0x00129D0C File Offset: 0x00127F0C
		internal static HealthMonitoringSectionHelper GetHelper()
		{
			if (HealthMonitoringSectionHelper.s_helper == null)
			{
				HealthMonitoringSectionHelper.s_helper = new HealthMonitoringSectionHelper();
			}
			return HealthMonitoringSectionHelper.s_helper;
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00129D24 File Offset: 0x00127F24
		private HealthMonitoringSectionHelper()
		{
			try
			{
				this._section = RuntimeConfig.GetAppConfig().HealthMonitoring;
			}
			catch (Exception initializationException)
			{
				if (HttpRuntime.InitializationException == null)
				{
					HttpRuntime.InitializationException = initializationException;
				}
				this._section = RuntimeConfig.GetAppLKGConfig().HealthMonitoring;
				if (this._section == null)
				{
					throw;
				}
			}
			this._enabled = this._section.Enabled;
			if (!this._enabled)
			{
				return;
			}
			this.BasicSanityCheck();
			this._ruleInfos = new ArrayList();
			this._customEvaluatorInstances = new Hashtable();
			this._providerInstances = new HealthMonitoringSectionHelper.ProviderInstances(this._section);
			this._cachedMatchedRulesForCustomEvents = new Hashtable(new WebBaseEventKeyComparer());
			HealthMonitoringSectionHelper._cachedMatchedRules = new ArrayList[WebEventCodes.GetEventArrayDimensionSize(0), WebEventCodes.GetEventArrayDimensionSize(1)];
			this.BuildRuleInfos();
			this._providerInstances.CleanupUninitProviders();
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x00129E04 File Offset: 0x00128004
		internal bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06005537 RID: 21815 RVA: 0x00129E0C File Offset: 0x0012800C
		internal HealthMonitoringSection HealthMonitoringSection
		{
			get
			{
				return this._section;
			}
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00129E14 File Offset: 0x00128014
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private void BasicSanityCheck()
		{
			foreach (object obj in this._section.Providers)
			{
				ProviderSettings providerSettings = (ProviderSettings)obj;
				Type type = ConfigUtil.GetType(providerSettings.Type, "type", providerSettings);
				HandlerBase.CheckAssignableType(providerSettings.ElementInformation.Properties["type"].Source, providerSettings.ElementInformation.Properties["type"].LineNumber, typeof(WebEventProvider), type);
			}
			foreach (object obj2 in this._section.EventMappings)
			{
				EventMappingSettings eventMappingSettings = (EventMappingSettings)obj2;
				Type type = ConfigUtil.GetType(eventMappingSettings.Type, "type", eventMappingSettings);
				if (eventMappingSettings.StartEventCode > eventMappingSettings.EndEventCode)
				{
					string propertyName = "startEventCode";
					if (eventMappingSettings.ElementInformation.Properties[propertyName].LineNumber == 0)
					{
						propertyName = "endEventCode";
					}
					throw new ConfigurationErrorsException(SR.GetString("Event_name_invalid_code_range"), eventMappingSettings.ElementInformation.Properties[propertyName].Source, eventMappingSettings.ElementInformation.Properties[propertyName].LineNumber);
				}
				HandlerBase.CheckAssignableType(eventMappingSettings.ElementInformation.Properties["type"].Source, eventMappingSettings.ElementInformation.Properties["type"].LineNumber, typeof(WebBaseEvent), type);
				eventMappingSettings.RealType = type;
			}
			foreach (object obj3 in this._section.Rules)
			{
				RuleSettings ruleSettings = (RuleSettings)obj3;
				string provider = ruleSettings.Provider;
				if (!string.IsNullOrEmpty(provider) && this._section.Providers[provider] == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Health_mon_provider_not_found", new object[]
					{
						provider
					}), ruleSettings.ElementInformation.Properties["provider"].Source, ruleSettings.ElementInformation.Properties["provider"].LineNumber);
				}
				string profile = ruleSettings.Profile;
				if (!string.IsNullOrEmpty(profile) && this._section.Profiles[profile] == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Health_mon_profile_not_found", new object[]
					{
						profile
					}), ruleSettings.ElementInformation.Properties["profile"].Source, ruleSettings.ElementInformation.Properties["profile"].LineNumber);
				}
				if (this._section.EventMappings[ruleSettings.EventName] == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Event_name_not_found", new object[]
					{
						ruleSettings.EventName
					}), ruleSettings.ElementInformation.Properties["eventName"].Source, ruleSettings.ElementInformation.Properties["eventName"].LineNumber);
				}
			}
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x00006164 File Offset: 0x00004364
		private void DisplayRuleInfo(HealthMonitoringSectionHelper.RuleInfo ruleInfo)
		{
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0012A1C4 File Offset: 0x001283C4
		private void BuildRuleInfos()
		{
			foreach (object obj in this._section.Rules)
			{
				RuleSettings ruleSettings = (RuleSettings)obj;
				HealthMonitoringSectionHelper.RuleInfo ruleInfo = this.CreateRuleInfo(ruleSettings);
				this.DisplayRuleInfo(ruleInfo);
				this._ruleInfos.Add(ruleInfo);
			}
			this._ruleInfos.Sort(HealthMonitoringSectionHelper.s_ruleInfoComparer);
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0012A248 File Offset: 0x00128448
		private HealthMonitoringSectionHelper.RuleInfo CreateRuleInfo(RuleSettings ruleSettings)
		{
			HealthMonitoringSectionHelper.RuleInfo ruleInfo = new HealthMonitoringSectionHelper.RuleInfo(ruleSettings, this._section);
			this.MergeValuesWithProfile(ruleInfo);
			this.InitReferencedProvider(ruleInfo);
			this.InitCustomEvaluator(ruleInfo);
			return ruleInfo;
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0012A278 File Offset: 0x00128478
		private void InitReferencedProvider(HealthMonitoringSectionHelper.RuleInfo ruleInfo)
		{
			string provider = ruleInfo._ruleSettings.Provider;
			if (string.IsNullOrEmpty(provider))
			{
				return;
			}
			WebEventProvider referencedProvider = this._providerInstances[provider];
			ruleInfo._referencedProvider = referencedProvider;
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0012A2B0 File Offset: 0x001284B0
		private void MergeValuesWithProfile(HealthMonitoringSectionHelper.RuleInfo ruleInfo)
		{
			ProfileSettings profileSettings = null;
			if (ruleInfo._ruleSettings.ElementInformation.Properties["profile"].ValueOrigin != PropertyValueOrigin.Default)
			{
				profileSettings = this._section.Profiles[ruleInfo._ruleSettings.Profile];
			}
			if (profileSettings != null && ruleInfo._ruleSettings.ElementInformation.Properties["minInstances"].ValueOrigin == PropertyValueOrigin.Default)
			{
				ruleInfo._minInstances = profileSettings.MinInstances;
			}
			else
			{
				ruleInfo._minInstances = ruleInfo._ruleSettings.MinInstances;
			}
			if (profileSettings != null && ruleInfo._ruleSettings.ElementInformation.Properties["maxLimit"].ValueOrigin == PropertyValueOrigin.Default)
			{
				ruleInfo._maxLimit = profileSettings.MaxLimit;
			}
			else
			{
				ruleInfo._maxLimit = ruleInfo._ruleSettings.MaxLimit;
			}
			if (profileSettings != null && ruleInfo._ruleSettings.ElementInformation.Properties["minInterval"].ValueOrigin == PropertyValueOrigin.Default)
			{
				ruleInfo._minInterval = profileSettings.MinInterval;
			}
			else
			{
				ruleInfo._minInterval = ruleInfo._ruleSettings.MinInterval;
			}
			if (profileSettings != null && ruleInfo._ruleSettings.ElementInformation.Properties["custom"].ValueOrigin == PropertyValueOrigin.Default)
			{
				ruleInfo._customEvaluator = profileSettings.Custom;
				ruleInfo._customEvaluatorConfig = profileSettings;
				return;
			}
			ruleInfo._customEvaluator = ruleInfo._ruleSettings.Custom;
			ruleInfo._customEvaluatorConfig = ruleInfo._ruleSettings;
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x0012A41C File Offset: 0x0012861C
		private void InitCustomEvaluator(HealthMonitoringSectionHelper.RuleInfo ruleInfo)
		{
			string customEvaluator = ruleInfo._customEvaluator;
			if (customEvaluator == null || customEvaluator.Trim().Length == 0)
			{
				ruleInfo._customEvaluatorType = null;
				return;
			}
			ruleInfo._customEvaluatorType = ConfigUtil.GetType(ruleInfo._customEvaluator, "custom", ruleInfo._customEvaluatorConfig);
			HandlerBase.CheckAssignableType(ruleInfo._customEvaluatorConfig.ElementInformation.Properties["custom"].Source, ruleInfo._customEvaluatorConfig.ElementInformation.Properties["custom"].LineNumber, typeof(IWebEventCustomEvaluator), ruleInfo._customEvaluatorType);
			if (this._customEvaluatorInstances[ruleInfo._customEvaluatorType] == null)
			{
				this._customEvaluatorInstances[ruleInfo._customEvaluatorType] = HttpRuntime.CreatePublicInstanceByWebObjectActivator(ruleInfo._customEvaluatorType);
			}
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0012A4E8 File Offset: 0x001286E8
		internal ArrayList FindFiringRuleInfos(Type eventType, int eventCode)
		{
			bool flag = eventCode < 100000;
			CustomWebEventKey key = null;
			int num = 0;
			int num2 = 0;
			ArrayList arrayList;
			if (flag)
			{
				WebEventCodes.GetEventArrayIndexsFromEventCode(eventCode, out num, out num2);
				arrayList = HealthMonitoringSectionHelper._cachedMatchedRules[num, num2];
			}
			else
			{
				key = new CustomWebEventKey(eventType, eventCode);
				arrayList = (ArrayList)this._cachedMatchedRulesForCustomEvents[key];
			}
			if (arrayList != null)
			{
				return arrayList;
			}
			object obj;
			if (flag)
			{
				obj = HealthMonitoringSectionHelper._cachedMatchedRules;
			}
			else
			{
				obj = this._cachedMatchedRulesForCustomEvents;
			}
			object obj2 = obj;
			ArrayList result;
			lock (obj2)
			{
				if (flag)
				{
					arrayList = HealthMonitoringSectionHelper._cachedMatchedRules[num, num2];
				}
				else
				{
					arrayList = (ArrayList)this._cachedMatchedRulesForCustomEvents[key];
				}
				if (arrayList != null)
				{
					result = arrayList;
				}
				else
				{
					ArrayList arrayList2 = new ArrayList();
					for (int i = this._ruleInfos.Count - 1; i >= 0; i--)
					{
						HealthMonitoringSectionHelper.RuleInfo ruleInfo = (HealthMonitoringSectionHelper.RuleInfo)this._ruleInfos[i];
						if (ruleInfo.Match(eventType, eventCode))
						{
							arrayList2.Add(new HealthMonitoringSectionHelper.FiringRuleInfo(ruleInfo));
						}
					}
					int count = arrayList2.Count;
					for (int j = 0; j < count; j++)
					{
						HealthMonitoringSectionHelper.FiringRuleInfo firingRuleInfo = (HealthMonitoringSectionHelper.FiringRuleInfo)arrayList2[j];
						if (firingRuleInfo._ruleInfo._referencedProvider != null)
						{
							for (int k = j + 1; k < count; k++)
							{
								HealthMonitoringSectionHelper.FiringRuleInfo firingRuleInfo2 = (HealthMonitoringSectionHelper.FiringRuleInfo)arrayList2[k];
								if (firingRuleInfo2._ruleInfo._referencedProvider != null && firingRuleInfo2._indexOfFirstRuleInfoWithSameProvider == -1 && firingRuleInfo._ruleInfo._referencedProvider == firingRuleInfo2._ruleInfo._referencedProvider)
								{
									if (firingRuleInfo._indexOfFirstRuleInfoWithSameProvider == -1)
									{
										firingRuleInfo._indexOfFirstRuleInfoWithSameProvider = j;
									}
									firingRuleInfo2._indexOfFirstRuleInfoWithSameProvider = j;
								}
							}
						}
					}
					if (flag)
					{
						HealthMonitoringSectionHelper._cachedMatchedRules[num, num2] = arrayList2;
					}
					else
					{
						this._cachedMatchedRulesForCustomEvents[key] = arrayList2;
					}
					result = arrayList2;
				}
			}
			return result;
		}

		// Token: 0x04002CA8 RID: 11432
		private static HealthMonitoringSectionHelper s_helper;

		// Token: 0x04002CA9 RID: 11433
		private static RuleInfoComparer s_ruleInfoComparer = new RuleInfoComparer();

		// Token: 0x04002CAA RID: 11434
		private HealthMonitoringSection _section;

		// Token: 0x04002CAB RID: 11435
		internal HealthMonitoringSectionHelper.ProviderInstances _providerInstances;

		// Token: 0x04002CAC RID: 11436
		internal Hashtable _customEvaluatorInstances;

		// Token: 0x04002CAD RID: 11437
		internal ArrayList _ruleInfos;

		// Token: 0x04002CAE RID: 11438
		private bool _enabled;

		// Token: 0x04002CAF RID: 11439
		private static ArrayList[,] _cachedMatchedRules;

		// Token: 0x04002CB0 RID: 11440
		private Hashtable _cachedMatchedRulesForCustomEvents;

		// Token: 0x02000A41 RID: 2625
		internal class RuleInfo
		{
			// Token: 0x06006E90 RID: 28304 RVA: 0x00189E48 File Offset: 0x00188048
			internal RuleInfo(RuleSettings ruleSettings, HealthMonitoringSection section)
			{
				this._eventMappingSettings = section.EventMappings[ruleSettings.EventName];
				this._ruleSettings = ruleSettings;
				this._ruleFiringRecord = new RuleFiringRecord(this);
			}

			// Token: 0x06006E91 RID: 28305 RVA: 0x00189E7C File Offset: 0x0018807C
			internal bool Match(Type eventType, int eventCode)
			{
				return (eventType.Equals(this._eventMappingSettings.RealType) || eventType.IsSubclassOf(this._eventMappingSettings.RealType)) && this._eventMappingSettings.StartEventCode <= eventCode && eventCode <= this._eventMappingSettings.EndEventCode;
			}

			// Token: 0x04003B01 RID: 15105
			internal string _customEvaluator;

			// Token: 0x04003B02 RID: 15106
			internal ConfigurationElement _customEvaluatorConfig;

			// Token: 0x04003B03 RID: 15107
			internal int _minInstances;

			// Token: 0x04003B04 RID: 15108
			internal int _maxLimit;

			// Token: 0x04003B05 RID: 15109
			internal TimeSpan _minInterval;

			// Token: 0x04003B06 RID: 15110
			internal RuleSettings _ruleSettings;

			// Token: 0x04003B07 RID: 15111
			internal WebEventProvider _referencedProvider;

			// Token: 0x04003B08 RID: 15112
			internal Type _customEvaluatorType;

			// Token: 0x04003B09 RID: 15113
			internal EventMappingSettings _eventMappingSettings;

			// Token: 0x04003B0A RID: 15114
			internal RuleFiringRecord _ruleFiringRecord;
		}

		// Token: 0x02000A42 RID: 2626
		internal class FiringRuleInfo
		{
			// Token: 0x06006E92 RID: 28306 RVA: 0x00189ED0 File Offset: 0x001880D0
			internal FiringRuleInfo(HealthMonitoringSectionHelper.RuleInfo ruleInfo)
			{
				this._ruleInfo = ruleInfo;
				this._indexOfFirstRuleInfoWithSameProvider = -1;
			}

			// Token: 0x04003B0B RID: 15115
			internal HealthMonitoringSectionHelper.RuleInfo _ruleInfo;

			// Token: 0x04003B0C RID: 15116
			internal int _indexOfFirstRuleInfoWithSameProvider;
		}

		// Token: 0x02000A43 RID: 2627
		internal class ProviderInstances
		{
			// Token: 0x06006E93 RID: 28307 RVA: 0x00189EE8 File Offset: 0x001880E8
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			internal ProviderInstances(HealthMonitoringSection section)
			{
				this._instances = CollectionsUtil.CreateCaseInsensitiveHashtable(section.Providers.Count);
				foreach (object obj in section.Providers)
				{
					ProviderSettings providerSettings = (ProviderSettings)obj;
					this._instances.Add(providerSettings.Name, providerSettings);
				}
			}

			// Token: 0x06006E94 RID: 28308 RVA: 0x00189F6C File Offset: 0x0018816C
			private WebEventProvider GetProviderInstance(string providerName)
			{
				object obj = this._instances[providerName];
				if (obj == null)
				{
					return null;
				}
				ProviderSettings providerSettings = obj as ProviderSettings;
				WebEventProvider webEventProvider;
				if (providerSettings != null)
				{
					string type = providerSettings.Type;
					Type type2 = BuildManager.GetType(type, false);
					if (typeof(IInternalWebEventProvider).IsAssignableFrom(type2))
					{
						webEventProvider = (WebEventProvider)HttpRuntime.CreateNonPublicInstance(type2);
					}
					else
					{
						webEventProvider = (WebEventProvider)HttpRuntime.CreatePublicInstanceByWebObjectActivator(type2);
					}
					using (new ProcessImpersonationContext())
					{
						try
						{
							webEventProvider.Initialize(providerSettings.Name, providerSettings.Parameters);
						}
						catch (ConfigurationErrorsException)
						{
							throw;
						}
						catch (ConfigurationException ex)
						{
							throw new ConfigurationErrorsException(ex.Message, providerSettings.ElementInformation.Properties["type"].Source, providerSettings.ElementInformation.Properties["type"].LineNumber);
						}
						catch
						{
							throw;
						}
					}
					this._instances[providerName] = webEventProvider;
				}
				else
				{
					webEventProvider = (obj as WebEventProvider);
				}
				return webEventProvider;
			}

			// Token: 0x17001E3F RID: 7743
			internal WebEventProvider this[string name]
			{
				get
				{
					return this.GetProviderInstance(name);
				}
			}

			// Token: 0x06006E96 RID: 28310 RVA: 0x0018A09C File Offset: 0x0018829C
			internal void CleanupUninitProviders()
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._instances)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value is ProviderSettings)
					{
						arrayList.Add(dictionaryEntry.Key);
					}
				}
				foreach (object key in arrayList)
				{
					this._instances.Remove(key);
				}
			}

			// Token: 0x06006E97 RID: 28311 RVA: 0x0018A15C File Offset: 0x0018835C
			internal bool ContainsKey(string name)
			{
				return this._instances.ContainsKey(name);
			}

			// Token: 0x06006E98 RID: 28312 RVA: 0x0018A16A File Offset: 0x0018836A
			public IDictionaryEnumerator GetEnumerator()
			{
				return this._instances.GetEnumerator();
			}

			// Token: 0x04003B0D RID: 15117
			internal Hashtable _instances;
		}
	}
}
