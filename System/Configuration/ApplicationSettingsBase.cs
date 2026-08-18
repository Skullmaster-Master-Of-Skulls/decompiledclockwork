using System;
using System.Collections;
using System.ComponentModel;
using System.Deployment.Internal;
using System.Reflection;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020006E2 RID: 1762
	public abstract class ApplicationSettingsBase : SettingsBase, INotifyPropertyChanged
	{
		// Token: 0x0600366B RID: 13931 RVA: 0x000E845A File Offset: 0x000E745A
		protected ApplicationSettingsBase()
		{
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000E8474 File Offset: 0x000E7474
		protected ApplicationSettingsBase(IComponent owner) : this(owner, string.Empty)
		{
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000E8482 File Offset: 0x000E7482
		protected ApplicationSettingsBase(string settingsKey)
		{
			this._settingsKey = settingsKey;
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000E84A4 File Offset: 0x000E74A4
		protected ApplicationSettingsBase(IComponent owner, string settingsKey) : this(settingsKey)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			if (owner.Site != null)
			{
				ISettingsProviderService settingsProviderService = owner.Site.GetService(typeof(ISettingsProviderService)) as ISettingsProviderService;
				if (settingsProviderService != null)
				{
					foreach (object obj in this.Properties)
					{
						SettingsProperty settingsProperty = (SettingsProperty)obj;
						SettingsProvider settingsProvider = settingsProviderService.GetSettingsProvider(settingsProperty);
						if (settingsProvider != null)
						{
							settingsProperty.Provider = settingsProvider;
						}
					}
					this.ResetProviders();
				}
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000E8554 File Offset: 0x000E7554
		[Browsable(false)]
		public override SettingsContext Context
		{
			get
			{
				if (this._context == null)
				{
					if (base.IsSynchronized)
					{
						lock (this)
						{
							if (this._context == null)
							{
								this._context = new SettingsContext();
								this.EnsureInitialized();
							}
							goto IL_4B;
						}
					}
					this._context = new SettingsContext();
					this.EnsureInitialized();
				}
				IL_4B:
				return this._context;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x000E85C4 File Offset: 0x000E75C4
		[Browsable(false)]
		public override SettingsPropertyCollection Properties
		{
			get
			{
				if (this._settings == null)
				{
					if (base.IsSynchronized)
					{
						lock (this)
						{
							if (this._settings == null)
							{
								this._settings = new SettingsPropertyCollection();
								this.EnsureInitialized();
							}
							goto IL_4B;
						}
					}
					this._settings = new SettingsPropertyCollection();
					this.EnsureInitialized();
				}
				IL_4B:
				return this._settings;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000E8634 File Offset: 0x000E7634
		[Browsable(false)]
		public override SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				return base.PropertyValues;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x000E863C File Offset: 0x000E763C
		[Browsable(false)]
		public override SettingsProviderCollection Providers
		{
			get
			{
				if (this._providers == null)
				{
					if (base.IsSynchronized)
					{
						lock (this)
						{
							if (this._providers == null)
							{
								this._providers = new SettingsProviderCollection();
								this.EnsureInitialized();
							}
							goto IL_4B;
						}
					}
					this._providers = new SettingsProviderCollection();
					this.EnsureInitialized();
				}
				IL_4B:
				return this._providers;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x000E86AC File Offset: 0x000E76AC
		// (set) Token: 0x06003674 RID: 13940 RVA: 0x000E86B4 File Offset: 0x000E76B4
		[Browsable(false)]
		public string SettingsKey
		{
			get
			{
				return this._settingsKey;
			}
			set
			{
				this._settingsKey = value;
				this.Context["SettingsKey"] = this._settingsKey;
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06003675 RID: 13941 RVA: 0x000E86D3 File Offset: 0x000E76D3
		// (remove) Token: 0x06003676 RID: 13942 RVA: 0x000E86EC File Offset: 0x000E76EC
		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				this._onPropertyChanged = (PropertyChangedEventHandler)Delegate.Combine(this._onPropertyChanged, value);
			}
			remove
			{
				this._onPropertyChanged = (PropertyChangedEventHandler)Delegate.Remove(this._onPropertyChanged, value);
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06003677 RID: 13943 RVA: 0x000E8705 File Offset: 0x000E7705
		// (remove) Token: 0x06003678 RID: 13944 RVA: 0x000E871E File Offset: 0x000E771E
		public event SettingChangingEventHandler SettingChanging
		{
			add
			{
				this._onSettingChanging = (SettingChangingEventHandler)Delegate.Combine(this._onSettingChanging, value);
			}
			remove
			{
				this._onSettingChanging = (SettingChangingEventHandler)Delegate.Remove(this._onSettingChanging, value);
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06003679 RID: 13945 RVA: 0x000E8737 File Offset: 0x000E7737
		// (remove) Token: 0x0600367A RID: 13946 RVA: 0x000E8750 File Offset: 0x000E7750
		public event SettingsLoadedEventHandler SettingsLoaded
		{
			add
			{
				this._onSettingsLoaded = (SettingsLoadedEventHandler)Delegate.Combine(this._onSettingsLoaded, value);
			}
			remove
			{
				this._onSettingsLoaded = (SettingsLoadedEventHandler)Delegate.Remove(this._onSettingsLoaded, value);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x0600367B RID: 13947 RVA: 0x000E8769 File Offset: 0x000E7769
		// (remove) Token: 0x0600367C RID: 13948 RVA: 0x000E8782 File Offset: 0x000E7782
		public event SettingsSavingEventHandler SettingsSaving
		{
			add
			{
				this._onSettingsSaving = (SettingsSavingEventHandler)Delegate.Combine(this._onSettingsSaving, value);
			}
			remove
			{
				this._onSettingsSaving = (SettingsSavingEventHandler)Delegate.Remove(this._onSettingsSaving, value);
			}
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000E879C File Offset: 0x000E779C
		public object GetPreviousVersion(string propertyName)
		{
			if (this.Properties.Count == 0)
			{
				throw new SettingsPropertyNotFoundException();
			}
			SettingsProperty settingsProperty = this.Properties[propertyName];
			SettingsPropertyValue settingsPropertyValue = null;
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException();
			}
			IApplicationSettingsProvider applicationSettingsProvider = settingsProperty.Provider as IApplicationSettingsProvider;
			if (applicationSettingsProvider != null)
			{
				settingsPropertyValue = applicationSettingsProvider.GetPreviousVersion(this.Context, settingsProperty);
			}
			if (settingsPropertyValue != null)
			{
				return settingsPropertyValue.PropertyValue;
			}
			return null;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000E87FC File Offset: 0x000E77FC
		protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this._onPropertyChanged != null)
			{
				this._onPropertyChanged(this, e);
			}
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000E8813 File Offset: 0x000E7813
		protected virtual void OnSettingChanging(object sender, SettingChangingEventArgs e)
		{
			if (this._onSettingChanging != null)
			{
				this._onSettingChanging(this, e);
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000E882A File Offset: 0x000E782A
		protected virtual void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e)
		{
			if (this._onSettingsLoaded != null)
			{
				this._onSettingsLoaded(this, e);
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000E8841 File Offset: 0x000E7841
		protected virtual void OnSettingsSaving(object sender, CancelEventArgs e)
		{
			if (this._onSettingsSaving != null)
			{
				this._onSettingsSaving(this, e);
			}
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000E8858 File Offset: 0x000E7858
		public void Reload()
		{
			if (this.PropertyValues != null)
			{
				this.PropertyValues.Clear();
			}
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				PropertyChangedEventArgs e = new PropertyChangedEventArgs(settingsProperty.Name);
				this.OnPropertyChanged(this, e);
			}
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000E88D4 File Offset: 0x000E78D4
		public void Reset()
		{
			if (this.Properties != null)
			{
				foreach (object obj in this.Providers)
				{
					SettingsProvider settingsProvider = (SettingsProvider)obj;
					IApplicationSettingsProvider applicationSettingsProvider = settingsProvider as IApplicationSettingsProvider;
					if (applicationSettingsProvider != null)
					{
						applicationSettingsProvider.Reset(this.Context);
					}
				}
			}
			this.Reload();
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000E894C File Offset: 0x000E794C
		public override void Save()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
			this.OnSettingsSaving(this, cancelEventArgs);
			if (!cancelEventArgs.Cancel)
			{
				base.Save();
			}
		}

		// Token: 0x17000CA2 RID: 3234
		public override object this[string propertyName]
		{
			get
			{
				if (base.IsSynchronized)
				{
					lock (this)
					{
						return this.GetPropertyValue(propertyName);
					}
				}
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				SettingChangingEventArgs settingChangingEventArgs = new SettingChangingEventArgs(propertyName, base.GetType().FullName, this.SettingsKey, value, false);
				this.OnSettingChanging(this, settingChangingEventArgs);
				if (!settingChangingEventArgs.Cancel)
				{
					base[propertyName] = value;
					PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
					this.OnPropertyChanged(this, e);
				}
			}
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000E8A10 File Offset: 0x000E7A10
		public virtual void Upgrade()
		{
			if (this.Properties != null)
			{
				foreach (object obj in this.Providers)
				{
					SettingsProvider settingsProvider = (SettingsProvider)obj;
					IApplicationSettingsProvider applicationSettingsProvider = settingsProvider as IApplicationSettingsProvider;
					if (applicationSettingsProvider != null)
					{
						applicationSettingsProvider.Upgrade(this.Context, this.GetPropertiesForProvider(settingsProvider));
					}
				}
			}
			this.Reload();
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000E8A90 File Offset: 0x000E7A90
		private SettingsProperty CreateSetting(PropertyInfo propInfo)
		{
			object[] customAttributes = propInfo.GetCustomAttributes(false);
			SettingsProperty settingsProperty = new SettingsProperty(this.Initializer);
			bool flag = this._explicitSerializeOnClass;
			settingsProperty.Name = propInfo.Name;
			settingsProperty.PropertyType = propInfo.PropertyType;
			for (int i = 0; i < customAttributes.Length; i++)
			{
				Attribute attribute = customAttributes[i] as Attribute;
				if (attribute != null)
				{
					if (attribute is DefaultSettingValueAttribute)
					{
						settingsProperty.DefaultValue = ((DefaultSettingValueAttribute)attribute).Value;
					}
					else if (attribute is ReadOnlyAttribute)
					{
						settingsProperty.IsReadOnly = true;
					}
					else if (attribute is SettingsProviderAttribute)
					{
						string providerTypeName = ((SettingsProviderAttribute)attribute).ProviderTypeName;
						Type type = Type.GetType(providerTypeName);
						if (type == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("ProviderTypeLoadFailed", new object[]
							{
								providerTypeName
							}));
						}
						SettingsProvider settingsProvider = SecurityUtils.SecureCreateInstance(type) as SettingsProvider;
						if (settingsProvider == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("ProviderInstantiationFailed", new object[]
							{
								providerTypeName
							}));
						}
						settingsProvider.Initialize(null, null);
						settingsProvider.ApplicationName = ConfigurationManagerInternalFactory.Instance.ExeProductName;
						SettingsProvider settingsProvider2 = this._providers[settingsProvider.Name];
						if (settingsProvider2 != null)
						{
							settingsProvider = settingsProvider2;
						}
						settingsProperty.Provider = settingsProvider;
					}
					else if (attribute is SettingsSerializeAsAttribute)
					{
						settingsProperty.SerializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
						flag = true;
					}
					else
					{
						settingsProperty.Attributes.Add(attribute.GetType(), attribute);
					}
				}
			}
			if (!flag)
			{
				settingsProperty.SerializeAs = this.GetSerializeAs(propInfo.PropertyType);
			}
			return settingsProperty;
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000E8C2C File Offset: 0x000E7C2C
		private void EnsureInitialized()
		{
			if (!this._initialized)
			{
				this._initialized = true;
				Type type = base.GetType();
				if (this._context == null)
				{
					this._context = new SettingsContext();
				}
				this._context["GroupName"] = type.FullName;
				this._context["SettingsKey"] = this.SettingsKey;
				this._context["SettingsClassType"] = type;
				PropertyInfo[] array = this.SettingsFilter(type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
				this._classAttributes = type.GetCustomAttributes(false);
				if (this._settings == null)
				{
					this._settings = new SettingsPropertyCollection();
				}
				if (this._providers == null)
				{
					this._providers = new SettingsProviderCollection();
				}
				for (int i = 0; i < array.Length; i++)
				{
					SettingsProperty settingsProperty = this.CreateSetting(array[i]);
					if (settingsProperty != null)
					{
						this._settings.Add(settingsProperty);
						if (settingsProperty.Provider != null && this._providers[settingsProperty.Provider.Name] == null)
						{
							this._providers.Add(settingsProperty.Provider);
						}
					}
				}
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x000E8D3C File Offset: 0x000E7D3C
		private SettingsProperty Initializer
		{
			get
			{
				if (this._init == null)
				{
					this._init = new SettingsProperty("");
					this._init.DefaultValue = null;
					this._init.IsReadOnly = false;
					this._init.PropertyType = null;
					SettingsProvider settingsProvider = new LocalFileSettingsProvider();
					if (this._classAttributes != null)
					{
						for (int i = 0; i < this._classAttributes.Length; i++)
						{
							Attribute attribute = this._classAttributes[i] as Attribute;
							if (attribute != null)
							{
								if (attribute is ReadOnlyAttribute)
								{
									this._init.IsReadOnly = true;
								}
								else if (attribute is SettingsGroupNameAttribute)
								{
									if (this._context == null)
									{
										this._context = new SettingsContext();
									}
									this._context["GroupName"] = ((SettingsGroupNameAttribute)attribute).GroupName;
								}
								else if (attribute is SettingsProviderAttribute)
								{
									string providerTypeName = ((SettingsProviderAttribute)attribute).ProviderTypeName;
									Type type = Type.GetType(providerTypeName);
									if (type == null)
									{
										throw new ConfigurationErrorsException(SR.GetString("ProviderTypeLoadFailed", new object[]
										{
											providerTypeName
										}));
									}
									SettingsProvider settingsProvider2 = SecurityUtils.SecureCreateInstance(type) as SettingsProvider;
									if (settingsProvider2 == null)
									{
										throw new ConfigurationErrorsException(SR.GetString("ProviderInstantiationFailed", new object[]
										{
											providerTypeName
										}));
									}
									settingsProvider = settingsProvider2;
								}
								else if (attribute is SettingsSerializeAsAttribute)
								{
									this._init.SerializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
									this._explicitSerializeOnClass = true;
								}
								else
								{
									this._init.Attributes.Add(attribute.GetType(), attribute);
								}
							}
						}
					}
					settingsProvider.Initialize(null, null);
					settingsProvider.ApplicationName = ConfigurationManagerInternalFactory.Instance.ExeProductName;
					this._init.Provider = settingsProvider;
				}
				return this._init;
			}
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000E8EF8 File Offset: 0x000E7EF8
		private SettingsPropertyCollection GetPropertiesForProvider(SettingsProvider provider)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.Provider == provider)
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			return settingsPropertyCollection;
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000E8F64 File Offset: 0x000E7F64
		private object GetPropertyValue(string propertyName)
		{
			if (this.PropertyValues[propertyName] == null)
			{
				object obj = base[propertyName];
				SettingsProperty settingsProperty = this.Properties[propertyName];
				SettingsProvider provider = (settingsProperty != null) ? settingsProperty.Provider : null;
				if (this._firstLoad)
				{
					this._firstLoad = false;
					if (this.IsFirstRunOfClickOnceApp())
					{
						this.Upgrade();
					}
				}
				SettingsLoadedEventArgs e = new SettingsLoadedEventArgs(provider);
				this.OnSettingsLoaded(this, e);
				return base[propertyName];
			}
			return base[propertyName];
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000E8FDC File Offset: 0x000E7FDC
		private SettingsSerializeAs GetSerializeAs(Type type)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			bool flag = converter.CanConvertTo(typeof(string));
			bool flag2 = converter.CanConvertFrom(typeof(string));
			if (flag && flag2)
			{
				return SettingsSerializeAs.String;
			}
			return SettingsSerializeAs.Xml;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000E901C File Offset: 0x000E801C
		private bool IsFirstRunOfClickOnceApp()
		{
			ActivationContext activationContext = AppDomain.CurrentDomain.ActivationContext;
			return ApplicationSettingsBase.IsClickOnceDeployed(AppDomain.CurrentDomain) && InternalActivationContextHelper.IsFirstRun(activationContext);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000E9048 File Offset: 0x000E8048
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal static bool IsClickOnceDeployed(AppDomain appDomain)
		{
			ActivationContext activationContext = appDomain.ActivationContext;
			if (activationContext != null && activationContext.Form == ActivationContext.ContextForm.StoreBounded)
			{
				string fullName = activationContext.Identity.FullName;
				if (!string.IsNullOrEmpty(fullName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000E9080 File Offset: 0x000E8080
		private PropertyInfo[] SettingsFilter(PropertyInfo[] allProps)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < allProps.Length; i++)
			{
				object[] customAttributes = allProps[i].GetCustomAttributes(false);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					Attribute attribute = customAttributes[j] as Attribute;
					if (attribute is SettingAttribute)
					{
						arrayList.Add(allProps[i]);
						break;
					}
				}
			}
			return (PropertyInfo[])arrayList.ToArray(typeof(PropertyInfo));
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000E90F0 File Offset: 0x000E80F0
		private void ResetProviders()
		{
			this.Providers.Clear();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (this.Providers[settingsProperty.Provider.Name] == null)
				{
					this.Providers.Add(settingsProperty.Provider);
				}
			}
		}

		// Token: 0x0400318D RID: 12685
		private bool _explicitSerializeOnClass;

		// Token: 0x0400318E RID: 12686
		private object[] _classAttributes;

		// Token: 0x0400318F RID: 12687
		private IComponent _owner;

		// Token: 0x04003190 RID: 12688
		private PropertyChangedEventHandler _onPropertyChanged;

		// Token: 0x04003191 RID: 12689
		private SettingsContext _context;

		// Token: 0x04003192 RID: 12690
		private SettingsProperty _init;

		// Token: 0x04003193 RID: 12691
		private SettingsPropertyCollection _settings;

		// Token: 0x04003194 RID: 12692
		private SettingsProviderCollection _providers;

		// Token: 0x04003195 RID: 12693
		private SettingChangingEventHandler _onSettingChanging;

		// Token: 0x04003196 RID: 12694
		private SettingsLoadedEventHandler _onSettingsLoaded;

		// Token: 0x04003197 RID: 12695
		private SettingsSavingEventHandler _onSettingsSaving;

		// Token: 0x04003198 RID: 12696
		private string _settingsKey = string.Empty;

		// Token: 0x04003199 RID: 12697
		private bool _firstLoad = true;

		// Token: 0x0400319A RID: 12698
		private bool _initialized;
	}
}
