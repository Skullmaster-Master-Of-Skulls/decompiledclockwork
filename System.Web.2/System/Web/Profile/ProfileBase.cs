using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;

namespace System.Web.Profile
{
	// Token: 0x0200015B RID: 347
	public class ProfileBase : SettingsBase
	{
		// Token: 0x170005F6 RID: 1526
		public override object this[string propertyName]
		{
			get
			{
				if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
				{
					HttpRuntime.NamedPermissionSet.PermitOnly();
				}
				return this.GetInternal(propertyName);
			}
			set
			{
				if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
				{
					HttpRuntime.NamedPermissionSet.PermitOnly();
				}
				this.SetInternal(propertyName, value);
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00038F7E File Offset: 0x0003717E
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private object GetInternal(string propertyName)
		{
			return base[propertyName];
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00038F88 File Offset: 0x00037188
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private void SetInternal(string propertyName, object value)
		{
			if (!this._IsAuthenticated)
			{
				SettingsProperty settingsProperty = ProfileBase.s_Properties[propertyName];
				if (settingsProperty != null && !(bool)settingsProperty.Attributes["AllowAnonymous"])
				{
					throw new ProviderException(SR.GetString("Profile_anonoymous_not_allowed_to_set_property"));
				}
			}
			base[propertyName] = value;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00038FDD File Offset: 0x000371DD
		public object GetPropertyValue(string propertyName)
		{
			return this[propertyName];
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00038FE6 File Offset: 0x000371E6
		public void SetPropertyValue(string propertyName, object propertyValue)
		{
			this[propertyName] = propertyValue;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00038FF0 File Offset: 0x000371F0
		public ProfileGroupBase GetProfileGroup(string groupName)
		{
			ProfileGroupBase profileGroupBase = (ProfileGroupBase)this._Groups[groupName];
			if (profileGroupBase == null)
			{
				Type type = BuildManager.GetProfileType();
				if (type == null)
				{
					throw new ProviderException(SR.GetString("Profile_group_not_found", new object[]
					{
						groupName
					}));
				}
				type = type.Assembly.GetType("ProfileGroup" + groupName, false);
				if (type == null)
				{
					throw new ProviderException(SR.GetString("Profile_group_not_found", new object[]
					{
						groupName
					}));
				}
				profileGroupBase = (ProfileGroupBase)Activator.CreateInstance(type);
				profileGroupBase.Init(this, groupName);
			}
			return profileGroupBase;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0003908B File Offset: 0x0003728B
		public ProfileBase()
		{
			if (!ProfileManager.Enabled)
			{
				throw new ProviderException(SR.GetString("Profile_not_enabled"));
			}
			if (!ProfileBase.s_Initialized)
			{
				ProfileBase.InitializeStatic();
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000390C4 File Offset: 0x000372C4
		public void Initialize(string username, bool isAuthenticated)
		{
			if (username != null)
			{
				this._UserName = username.Trim();
			}
			else
			{
				this._UserName = username;
			}
			SettingsContext settingsContext = new SettingsContext();
			settingsContext.Add("UserName", this._UserName);
			settingsContext.Add("IsAuthenticated", isAuthenticated);
			this._IsAuthenticated = isAuthenticated;
			base.Initialize(settingsContext, ProfileBase.s_Properties, ProfileManager.Providers);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00039129 File Offset: 0x00037329
		public override void Save()
		{
			if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
			{
				HttpRuntime.NamedPermissionSet.PermitOnly();
			}
			this.SaveWithAssert();
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00039150 File Offset: 0x00037350
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private void SaveWithAssert()
		{
			base.Save();
			this._IsDirty = false;
			this._DatesRetrieved = false;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00039166 File Offset: 0x00037366
		public string UserName
		{
			get
			{
				return this._UserName;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0003916E File Offset: 0x0003736E
		public bool IsAnonymous
		{
			get
			{
				return !this._IsAuthenticated;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0003917C File Offset: 0x0003737C
		public bool IsDirty
		{
			get
			{
				if (this._IsDirty)
				{
					return true;
				}
				foreach (object obj in this.PropertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
					if (settingsPropertyValue.IsDirty)
					{
						this._IsDirty = true;
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000391F0 File Offset: 0x000373F0
		public DateTime LastActivityDate
		{
			get
			{
				if (!this._DatesRetrieved)
				{
					this.RetrieveDates();
				}
				return this._LastActivityDate.ToLocalTime();
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0003920B File Offset: 0x0003740B
		public DateTime LastUpdatedDate
		{
			get
			{
				if (!this._DatesRetrieved)
				{
					this.RetrieveDates();
				}
				return this._LastUpdatedDate.ToLocalTime();
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00039226 File Offset: 0x00037426
		public static ProfileBase Create(string username)
		{
			return ProfileBase.Create(username, true);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00039230 File Offset: 0x00037430
		public static ProfileBase Create(string username, bool isAuthenticated)
		{
			if (!ProfileManager.Enabled)
			{
				throw new ProviderException(SR.GetString("Profile_not_enabled"));
			}
			ProfileBase.InitializeStatic();
			if (ProfileBase.s_SingletonInstance != null)
			{
				return ProfileBase.s_SingletonInstance;
			}
			if (ProfileBase.s_Properties.Count == 0)
			{
				object obj = ProfileBase.s_InitializeLock;
				lock (obj)
				{
					if (ProfileBase.s_SingletonInstance == null)
					{
						ProfileBase.s_SingletonInstance = new DefaultProfile();
					}
					return ProfileBase.s_SingletonInstance;
				}
			}
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			return ProfileBase.CreateMyInstance(username, isAuthenticated);
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x000392D0 File Offset: 0x000374D0
		public new static SettingsPropertyCollection Properties
		{
			get
			{
				ProfileBase.InitializeStatic();
				return ProfileBase.s_Properties;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x000392DC File Offset: 0x000374DC
		internal static Type InheritsFromType
		{
			get
			{
				if (!ProfileManager.Enabled)
				{
					return typeof(DefaultProfile);
				}
				Type type;
				if (HostingEnvironment.IsHosted)
				{
					type = BuildManager.GetType(ProfileBase.InheritsFromTypeString, true, true);
				}
				else
				{
					type = ProfileBase.GetPropType(ProfileBase.InheritsFromTypeString);
				}
				if (!typeof(ProfileBase).IsAssignableFrom(type))
				{
					ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
					throw new ConfigurationErrorsException(SR.GetString("Wrong_profile_base_type"), null, profileAppConfig.ElementInformation.Properties["inherits"].Source, profileAppConfig.ElementInformation.Properties["inherit"].LineNumber);
				}
				return type;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0003937C File Offset: 0x0003757C
		internal static string InheritsFromTypeString
		{
			get
			{
				string result = typeof(ProfileBase).ToString();
				if (!ProfileManager.Enabled)
				{
					return result;
				}
				ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
				if (profileAppConfig.Inherits == null)
				{
					return result;
				}
				string text = profileAppConfig.Inherits.Trim();
				if (text.Length < 1)
				{
					return result;
				}
				Type type = Type.GetType(text, false, true);
				if (type == null)
				{
					return text;
				}
				if (!typeof(ProfileBase).IsAssignableFrom(type))
				{
					throw new ConfigurationErrorsException(SR.GetString("Wrong_profile_base_type"), null, profileAppConfig.ElementInformation.Properties["inherits"].Source, profileAppConfig.ElementInformation.Properties["inherit"].LineNumber);
				}
				return type.AssemblyQualifiedName;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0003943C File Offset: 0x0003763C
		internal static bool InheritsFromCustomType
		{
			get
			{
				if (!ProfileManager.Enabled)
				{
					return false;
				}
				ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
				if (profileAppConfig.Inherits == null)
				{
					return false;
				}
				string text = profileAppConfig.Inherits.Trim();
				if (text == null || text.Length < 1)
				{
					return false;
				}
				Type type = Type.GetType(text, false, true);
				return type == null || type != typeof(ProfileBase);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x000394A3 File Offset: 0x000376A3
		internal static ProfileBase SingletonInstance
		{
			get
			{
				return ProfileBase.s_SingletonInstance;
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x000394AC File Offset: 0x000376AC
		internal static Hashtable GetPropertiesForCompilation()
		{
			if (!ProfileManager.Enabled)
			{
				return null;
			}
			if (ProfileBase.s_PropertiesForCompilation != null)
			{
				return ProfileBase.s_PropertiesForCompilation;
			}
			object obj = ProfileBase.s_InitializeLock;
			lock (obj)
			{
				if (ProfileBase.s_PropertiesForCompilation != null)
				{
					return ProfileBase.s_PropertiesForCompilation;
				}
				Hashtable ht = new Hashtable();
				ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
				if (profileAppConfig.PropertySettings == null)
				{
					ProfileBase.s_PropertiesForCompilation = ht;
					return ProfileBase.s_PropertiesForCompilation;
				}
				ProfileBase.AddProfilePropertySettingsForCompilation(profileAppConfig.PropertySettings, ht, null);
				foreach (object obj2 in profileAppConfig.PropertySettings.GroupSettings)
				{
					ProfileGroupSettings profileGroupSettings = (ProfileGroupSettings)obj2;
					ProfileBase.AddProfilePropertySettingsForCompilation(profileGroupSettings.PropertySettings, ht, profileGroupSettings.Name);
				}
				ProfileBase.AddProfilePropertySettingsForCompilation(ProfileManager.DynamicProfileProperties, ht, null);
				ProfileBase.s_PropertiesForCompilation = ht;
			}
			return ProfileBase.s_PropertiesForCompilation;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000395C0 File Offset: 0x000377C0
		internal static string GetProfileClassName()
		{
			Hashtable propertiesForCompilation = ProfileBase.GetPropertiesForCompilation();
			if (propertiesForCompilation == null)
			{
				return "System.Web.Profile.DefaultProfile";
			}
			if (propertiesForCompilation.Count > 0 || ProfileBase.InheritsFromCustomType)
			{
				return "ProfileCommon";
			}
			return "System.Web.Profile.DefaultProfile";
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000395F8 File Offset: 0x000377F8
		private static void AddProfilePropertySettingsForCompilation(ProfilePropertySettingsCollection propertyCollection, Hashtable ht, string groupName)
		{
			foreach (object obj in propertyCollection)
			{
				ProfilePropertySettings profilePropertySettings = (ProfilePropertySettings)obj;
				ProfileNameTypeStruct profileNameTypeStruct = new ProfileNameTypeStruct();
				if (groupName != null)
				{
					profileNameTypeStruct.Name = groupName + "." + profilePropertySettings.Name;
				}
				else
				{
					profileNameTypeStruct.Name = profilePropertySettings.Name;
				}
				Type type = profilePropertySettings.TypeInternal;
				if (type == null)
				{
					type = ProfileBase.ResolvePropertyTypeForCommonTypes(profilePropertySettings.Type.ToLower(CultureInfo.InvariantCulture));
				}
				if (type == null)
				{
					type = BuildManager.GetType(profilePropertySettings.Type, false);
				}
				if (type == null)
				{
					profileNameTypeStruct.PropertyCodeRefType = new CodeTypeReference(profilePropertySettings.Type);
				}
				else
				{
					profileNameTypeStruct.PropertyCodeRefType = new CodeTypeReference(type);
				}
				profileNameTypeStruct.PropertyType = type;
				profilePropertySettings.TypeInternal = type;
				profileNameTypeStruct.IsReadOnly = profilePropertySettings.ReadOnly;
				profileNameTypeStruct.LineNumber = profilePropertySettings.ElementInformation.Properties["name"].LineNumber;
				profileNameTypeStruct.FileName = profilePropertySettings.ElementInformation.Properties["name"].Source;
				ht.Add(profileNameTypeStruct.Name, profileNameTypeStruct);
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00039754 File Offset: 0x00037954
		private static ProfileBase CreateMyInstance(string username, bool isAuthenticated)
		{
			Type type;
			if (HostingEnvironment.IsHosted)
			{
				type = BuildManager.GetProfileType();
			}
			else
			{
				type = ProfileBase.InheritsFromType;
			}
			ProfileBase profileBase = (ProfileBase)Activator.CreateInstance(type);
			profileBase.Initialize(username, isAuthenticated);
			return profileBase;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003978C File Offset: 0x0003798C
		private static void InitializeStatic()
		{
			if (ProfileManager.Enabled && !ProfileBase.s_Initialized)
			{
				object obj = ProfileBase.s_InitializeLock;
				lock (obj)
				{
					if (ProfileBase.s_Initialized)
					{
						if (ProfileBase.s_InitializeException != null)
						{
							throw ProfileBase.s_InitializeException;
						}
						return;
					}
					else
					{
						try
						{
							ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
							bool flag2 = !HostingEnvironment.IsHosted || AnonymousIdentificationModule.Enabled;
							Type inheritsFromType = ProfileBase.InheritsFromType;
							bool flag3 = HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Low);
							ProfileBase.s_Properties = new SettingsPropertyCollection();
							ProfileBase.AddPropertySettingsFromConfig(inheritsFromType, flag2, flag3, ProfileManager.DynamicProfileProperties, null);
							if (inheritsFromType != typeof(ProfileBase))
							{
								PropertyInfo[] properties = typeof(ProfileBase).GetProperties();
								NameValueCollection nameValueCollection = new NameValueCollection(properties.Length);
								foreach (PropertyInfo propertyInfo in properties)
								{
									nameValueCollection.Add(propertyInfo.Name, string.Empty);
								}
								PropertyInfo[] properties2 = inheritsFromType.GetProperties();
								foreach (PropertyInfo propertyInfo2 in properties2)
								{
									if (nameValueCollection[propertyInfo2.Name] == null)
									{
										ProfileProvider profileProvider = flag3 ? ProfileManager.Provider : null;
										bool isReadOnly = false;
										SettingsSerializeAs serializeAs = SettingsSerializeAs.ProviderSpecific;
										string defaultValue = string.Empty;
										bool flag4 = false;
										string value = null;
										Attribute[] customAttributes = Attribute.GetCustomAttributes(propertyInfo2, true);
										foreach (Attribute attribute in customAttributes)
										{
											if (attribute is SettingsSerializeAsAttribute)
											{
												serializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
											}
											else if (attribute is SettingsAllowAnonymousAttribute)
											{
												flag4 = ((SettingsAllowAnonymousAttribute)attribute).Allow;
												if (!flag2 && flag4)
												{
													throw new ConfigurationErrorsException(SR.GetString("Annoymous_id_module_not_enabled", new object[]
													{
														propertyInfo2.Name
													}), profileAppConfig.ElementInformation.Properties["inherits"].Source, profileAppConfig.ElementInformation.Properties["inherits"].LineNumber);
												}
											}
											else if (attribute is ReadOnlyAttribute)
											{
												isReadOnly = ((ReadOnlyAttribute)attribute).IsReadOnly;
											}
											else if (attribute is DefaultSettingValueAttribute)
											{
												defaultValue = ((DefaultSettingValueAttribute)attribute).Value;
											}
											else if (attribute is CustomProviderDataAttribute)
											{
												value = ((CustomProviderDataAttribute)attribute).CustomProviderData;
											}
											else if (flag3 && attribute is ProfileProviderAttribute)
											{
												profileProvider = ProfileManager.Providers[((ProfileProviderAttribute)attribute).ProviderName];
												if (profileProvider == null)
												{
													throw new ConfigurationErrorsException(SR.GetString("Profile_provider_not_found", new object[]
													{
														((ProfileProviderAttribute)attribute).ProviderName
													}), profileAppConfig.ElementInformation.Properties["inherits"].Source, profileAppConfig.ElementInformation.Properties["inherits"].LineNumber);
												}
											}
										}
										SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
										settingsAttributeDictionary.Add("AllowAnonymous", flag4);
										if (!string.IsNullOrEmpty(value))
										{
											settingsAttributeDictionary.Add("CustomProviderData", value);
										}
										SettingsProperty property = new SettingsProperty(propertyInfo2.Name, propertyInfo2.PropertyType, profileProvider, isReadOnly, defaultValue, serializeAs, settingsAttributeDictionary, false, true);
										ProfileBase.s_Properties.Add(property);
									}
								}
							}
							if (profileAppConfig.PropertySettings != null)
							{
								ProfileBase.AddPropertySettingsFromConfig(inheritsFromType, flag2, flag3, profileAppConfig.PropertySettings, null);
								foreach (object obj2 in profileAppConfig.PropertySettings.GroupSettings)
								{
									ProfileGroupSettings profileGroupSettings = (ProfileGroupSettings)obj2;
									ProfileBase.AddPropertySettingsFromConfig(inheritsFromType, flag2, flag3, profileGroupSettings.PropertySettings, profileGroupSettings.Name);
								}
							}
						}
						catch (Exception ex)
						{
							if (ProfileBase.s_InitializeException == null)
							{
								ProfileBase.s_InitializeException = ex;
							}
						}
						if (ProfileBase.s_Properties == null)
						{
							ProfileBase.s_Properties = new SettingsPropertyCollection();
						}
						ProfileBase.s_Properties.SetReadOnly();
						ProfileBase.s_Initialized = true;
					}
				}
				if (ProfileBase.s_InitializeException != null)
				{
					throw ProfileBase.s_InitializeException;
				}
				return;
			}
			if (ProfileBase.s_InitializeException != null)
			{
				throw ProfileBase.s_InitializeException;
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00039BE8 File Offset: 0x00037DE8
		private static void AddPropertySettingsFromConfig(Type baseType, bool fAnonEnabled, bool hasLowTrust, ProfilePropertySettingsCollection settingsCollection, string groupName)
		{
			foreach (object obj in settingsCollection)
			{
				ProfilePropertySettings profilePropertySettings = (ProfilePropertySettings)obj;
				string name = (groupName != null) ? (groupName + "." + profilePropertySettings.Name) : profilePropertySettings.Name;
				if (baseType != typeof(ProfileBase) && ProfileBase.s_Properties[name] != null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Profile_property_already_added"), null, profilePropertySettings.ElementInformation.Properties["name"].Source, profilePropertySettings.ElementInformation.Properties["name"].LineNumber);
				}
				try
				{
					if (profilePropertySettings.TypeInternal == null)
					{
						profilePropertySettings.TypeInternal = ProfileBase.ResolvePropertyType(profilePropertySettings.Type);
					}
				}
				catch (Exception ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("Profile_could_not_create_type", new object[]
					{
						ex.Message
					}), ex, profilePropertySettings.ElementInformation.Properties["type"].Source, profilePropertySettings.ElementInformation.Properties["type"].LineNumber);
				}
				if (!fAnonEnabled)
				{
					bool allowAnonymous = profilePropertySettings.AllowAnonymous;
					if (allowAnonymous)
					{
						throw new ConfigurationErrorsException(SR.GetString("Annoymous_id_module_not_enabled", new object[]
						{
							profilePropertySettings.Name
						}), profilePropertySettings.ElementInformation.Properties["allowAnonymous"].Source, profilePropertySettings.ElementInformation.Properties["allowAnonymous"].LineNumber);
					}
				}
				if (hasLowTrust)
				{
					ProfileBase.SetProviderForProperty(profilePropertySettings);
				}
				else
				{
					profilePropertySettings.ProviderInternal = null;
				}
				bool flag = profilePropertySettings.ProviderInternal == null || profilePropertySettings.ProviderInternal.GetType() == typeof(SqlProfileProvider);
				if (flag && profilePropertySettings.SerializeAs == SerializationMode.Binary && !profilePropertySettings.TypeInternal.IsSerializable)
				{
					throw new ConfigurationErrorsException(SR.GetString("Property_not_serializable", new object[]
					{
						profilePropertySettings.Name
					}), profilePropertySettings.ElementInformation.Properties["serializeAs"].Source, profilePropertySettings.ElementInformation.Properties["serializeAs"].LineNumber);
				}
				SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
				settingsAttributeDictionary.Add("AllowAnonymous", profilePropertySettings.AllowAnonymous);
				if (!string.IsNullOrEmpty(profilePropertySettings.CustomProviderData))
				{
					settingsAttributeDictionary.Add("CustomProviderData", profilePropertySettings.CustomProviderData);
				}
				SettingsProperty property = new SettingsProperty(name, profilePropertySettings.TypeInternal, profilePropertySettings.ProviderInternal, profilePropertySettings.ReadOnly, profilePropertySettings.DefaultValue, (SettingsSerializeAs)profilePropertySettings.SerializeAs, settingsAttributeDictionary, false, true);
				ProfileBase.s_Properties.Add(property);
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00039ED8 File Offset: 0x000380D8
		private static void SetProviderForProperty(ProfilePropertySettings pps)
		{
			if (pps.Provider == null || pps.Provider.Length < 1)
			{
				pps.ProviderInternal = ProfileManager.Provider;
			}
			else
			{
				pps.ProviderInternal = ProfileManager.Providers[pps.Provider];
			}
			if (pps.ProviderInternal == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Profile_provider_not_found", new object[]
				{
					pps.Provider
				}), pps.ElementInformation.Properties["provider"].Source, pps.ElementInformation.Properties["provider"].LineNumber);
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00039F7C File Offset: 0x0003817C
		private static Type ResolvePropertyTypeForCommonTypes(string typeName)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(typeName);
			if (num <= 2699759368U)
			{
				if (num > 1683620383U)
				{
					if (num <= 2090339911U)
					{
						if (num != 1687288466U)
						{
							if (num != 1710517951U)
							{
								if (num != 2090339911U)
								{
									goto IL_4CE;
								}
								if (!(typeName == "float64"))
								{
									goto IL_4CE;
								}
							}
							else
							{
								if (!(typeName == "boolean"))
								{
									goto IL_4CE;
								}
								goto IL_434;
							}
						}
						else
						{
							if (!(typeName == "int8"))
							{
								goto IL_4CE;
							}
							goto IL_429;
						}
					}
					else if (num <= 2515107422U)
					{
						if (num != 2133018345U)
						{
							if (num != 2515107422U)
							{
								goto IL_4CE;
							}
							if (!(typeName == "int"))
							{
								goto IL_4CE;
							}
							goto IL_44A;
						}
						else
						{
							if (!(typeName == "single"))
							{
								goto IL_4CE;
							}
							return typeof(float);
						}
					}
					else if (num != 2667225454U)
					{
						if (num != 2699759368U)
						{
							goto IL_4CE;
						}
						if (!(typeName == "double"))
						{
							goto IL_4CE;
						}
					}
					else
					{
						if (!(typeName == "ulong"))
						{
							goto IL_4CE;
						}
						goto IL_4B8;
					}
					return typeof(double);
				}
				if (num <= 398550328U)
				{
					if (num != 64103268U)
					{
						if (num != 132346577U)
						{
							if (num != 398550328U)
							{
								goto IL_4CE;
							}
							if (!(typeName == "string"))
							{
								goto IL_4CE;
							}
							return typeof(string);
						}
						else
						{
							if (!(typeName == "int16"))
							{
								goto IL_4CE;
							}
							goto IL_48C;
						}
					}
					else
					{
						if (!(typeName == "int64"))
						{
							goto IL_4CE;
						}
						goto IL_481;
					}
				}
				else if (num <= 848563180U)
				{
					if (num != 520654156U)
					{
						if (num != 848563180U)
						{
							goto IL_4CE;
						}
						if (!(typeName == "uint32"))
						{
							goto IL_4CE;
						}
						goto IL_4AD;
					}
					else
					{
						if (!(typeName == "decimal"))
						{
							goto IL_4CE;
						}
						return typeof(decimal);
					}
				}
				else if (num != 1630192034U)
				{
					if (num != 1683620383U)
					{
						goto IL_4CE;
					}
					if (!(typeName == "byte"))
					{
						goto IL_4CE;
					}
				}
				else
				{
					if (!(typeName == "ushort"))
					{
						goto IL_4CE;
					}
					goto IL_4A2;
				}
				IL_429:
				return typeof(byte);
			}
			if (num <= 3218261061U)
			{
				if (num <= 2928590578U)
				{
					if (num != 2797886853U)
					{
						if (num != 2823553821U)
						{
							if (num != 2928590578U)
							{
								goto IL_4CE;
							}
							if (!(typeName == "uint16"))
							{
								goto IL_4CE;
							}
							goto IL_4A2;
						}
						else
						{
							if (!(typeName == "char"))
							{
								goto IL_4CE;
							}
							return typeof(char);
						}
					}
					else if (!(typeName == "float"))
					{
						goto IL_4CE;
					}
				}
				else if (num <= 3099987130U)
				{
					if (num != 2929723411U)
					{
						if (num != 3099987130U)
						{
							goto IL_4CE;
						}
						if (!(typeName == "object"))
						{
							goto IL_4CE;
						}
						return typeof(object);
					}
					else
					{
						if (!(typeName == "uint64"))
						{
							goto IL_4CE;
						}
						goto IL_4B8;
					}
				}
				else if (num != 3122818005U)
				{
					if (num != 3218261061U)
					{
						goto IL_4CE;
					}
					if (!(typeName == "integer"))
					{
						goto IL_4CE;
					}
					goto IL_44A;
				}
				else
				{
					if (!(typeName == "short"))
					{
						goto IL_4CE;
					}
					goto IL_48C;
				}
			}
			else if (num <= 3415750305U)
			{
				if (num != 3270303571U)
				{
					if (num != 3365180733U)
					{
						if (num != 3415750305U)
						{
							goto IL_4CE;
						}
						if (!(typeName == "uint"))
						{
							goto IL_4CE;
						}
						goto IL_4AD;
					}
					else
					{
						if (!(typeName == "bool"))
						{
							goto IL_4CE;
						}
						goto IL_434;
					}
				}
				else
				{
					if (!(typeName == "long"))
					{
						goto IL_4CE;
					}
					goto IL_481;
				}
			}
			else
			{
				if (num <= 3564297305U)
				{
					if (num != 3437915536U)
					{
						if (num != 3564297305U)
						{
							goto IL_4CE;
						}
						if (!(typeName == "date"))
						{
							goto IL_4CE;
						}
					}
					else if (!(typeName == "datetime"))
					{
						goto IL_4CE;
					}
					return typeof(DateTime);
				}
				if (num != 3902764048U)
				{
					if (num != 4225688255U)
					{
						goto IL_4CE;
					}
					if (!(typeName == "int32"))
					{
						goto IL_4CE;
					}
					goto IL_44A;
				}
				else if (!(typeName == "float32"))
				{
					goto IL_4CE;
				}
			}
			return typeof(float);
			IL_434:
			return typeof(bool);
			IL_44A:
			return typeof(int);
			IL_481:
			return typeof(long);
			IL_48C:
			return typeof(short);
			IL_4A2:
			return typeof(ushort);
			IL_4AD:
			return typeof(uint);
			IL_4B8:
			return typeof(ulong);
			IL_4CE:
			return null;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0003A458 File Offset: 0x00038658
		private static Type ResolvePropertyType(string typeName)
		{
			Type type = ProfileBase.ResolvePropertyTypeForCommonTypes(typeName.ToLower(CultureInfo.InvariantCulture));
			if (type != null)
			{
				return type;
			}
			if (HostingEnvironment.IsHosted)
			{
				return BuildManager.GetType(typeName, true, true);
			}
			return ProfileBase.GetPropType(typeName);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0003A497 File Offset: 0x00038697
		private static Type GetPropType(string typeName)
		{
			return Type.GetType(typeName, true, true);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0003A4A4 File Offset: 0x000386A4
		private void RetrieveDates()
		{
			if (this._DatesRetrieved || ProfileManager.Provider == null)
			{
				return;
			}
			int num;
			ProfileInfoCollection profileInfoCollection = ProfileManager.Provider.FindProfilesByUserName(ProfileAuthenticationOption.All, this._UserName, 0, 1, out num);
			using (IEnumerator enumerator = profileInfoCollection.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ProfileInfo profileInfo = (ProfileInfo)enumerator.Current;
					this._LastActivityDate = profileInfo.LastActivityDate.ToUniversalTime();
					this._LastUpdatedDate = profileInfo.LastUpdatedDate.ToUniversalTime();
					this._DatesRetrieved = true;
				}
			}
		}

		// Token: 0x040014EE RID: 5358
		private Hashtable _Groups = new Hashtable();

		// Token: 0x040014EF RID: 5359
		private bool _IsAuthenticated;

		// Token: 0x040014F0 RID: 5360
		private string _UserName;

		// Token: 0x040014F1 RID: 5361
		private bool _IsDirty;

		// Token: 0x040014F2 RID: 5362
		private DateTime _LastActivityDate;

		// Token: 0x040014F3 RID: 5363
		private DateTime _LastUpdatedDate;

		// Token: 0x040014F4 RID: 5364
		private bool _DatesRetrieved;

		// Token: 0x040014F5 RID: 5365
		private static SettingsPropertyCollection s_Properties = null;

		// Token: 0x040014F6 RID: 5366
		private static object s_InitializeLock = new object();

		// Token: 0x040014F7 RID: 5367
		private static Exception s_InitializeException = null;

		// Token: 0x040014F8 RID: 5368
		private static bool s_Initialized = false;

		// Token: 0x040014F9 RID: 5369
		private static ProfileBase s_SingletonInstance = null;

		// Token: 0x040014FA RID: 5370
		private static Hashtable s_PropertiesForCompilation = null;
	}
}
