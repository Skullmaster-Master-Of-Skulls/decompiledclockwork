using System;
using System.ComponentModel;

namespace System.Configuration
{
	// Token: 0x020006E1 RID: 1761
	public abstract class SettingsBase
	{
		// Token: 0x0600365C RID: 13916 RVA: 0x000E7F1C File Offset: 0x000E6F1C
		protected SettingsBase()
		{
			this._PropertyValues = new SettingsPropertyValueCollection();
		}

		// Token: 0x17000C97 RID: 3223
		public virtual object this[string propertyName]
		{
			get
			{
				if (this.IsSynchronized)
				{
					lock (this)
					{
						return this.GetPropertyValueByName(propertyName);
					}
				}
				return this.GetPropertyValueByName(propertyName);
			}
			set
			{
				if (this.IsSynchronized)
				{
					lock (this)
					{
						this.SetPropertyValueByName(propertyName, value);
						return;
					}
				}
				this.SetPropertyValueByName(propertyName, value);
			}
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000E7FC0 File Offset: 0x000E6FC0
		private object GetPropertyValueByName(string propertyName)
		{
			if (this.Properties == null || this._PropertyValues == null || this.Properties.Count == 0)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[]
				{
					propertyName
				}));
			}
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[]
				{
					propertyName
				}));
			}
			SettingsPropertyValue settingsPropertyValue = this._PropertyValues[propertyName];
			if (settingsPropertyValue == null)
			{
				this.GetPropertiesFromProvider(settingsProperty.Provider);
				settingsPropertyValue = this._PropertyValues[propertyName];
				if (settingsPropertyValue == null)
				{
					throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[]
					{
						propertyName
					}));
				}
			}
			return settingsPropertyValue.PropertyValue;
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000E8084 File Offset: 0x000E7084
		private void SetPropertyValueByName(string propertyName, object propertyValue)
		{
			if (this.Properties == null || this._PropertyValues == null || this.Properties.Count == 0)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[]
				{
					propertyName
				}));
			}
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[]
				{
					propertyName
				}));
			}
			if (settingsProperty.IsReadOnly)
			{
				throw new SettingsPropertyIsReadOnlyException(SR.GetString("SettingsPropertyReadOnly", new object[]
				{
					propertyName
				}));
			}
			if (propertyValue != null && !settingsProperty.PropertyType.IsInstanceOfType(propertyValue))
			{
				throw new SettingsPropertyWrongTypeException(SR.GetString("SettingsPropertyWrongType", new object[]
				{
					propertyName
				}));
			}
			SettingsPropertyValue settingsPropertyValue = this._PropertyValues[propertyName];
			if (settingsPropertyValue == null)
			{
				this.GetPropertiesFromProvider(settingsProperty.Provider);
				settingsPropertyValue = this._PropertyValues[propertyName];
				if (settingsPropertyValue == null)
				{
					throw new SettingsPropertyNotFoundException(SR.GetString("SettingsPropertyNotFound", new object[]
					{
						propertyName
					}));
				}
			}
			settingsPropertyValue.PropertyValue = propertyValue;
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000E819F File Offset: 0x000E719F
		public void Initialize(SettingsContext context, SettingsPropertyCollection properties, SettingsProviderCollection providers)
		{
			this._Context = context;
			this._Properties = properties;
			this._Providers = providers;
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000E81B8 File Offset: 0x000E71B8
		public virtual void Save()
		{
			if (this.IsSynchronized)
			{
				lock (this)
				{
					this.SaveCore();
					return;
				}
			}
			this.SaveCore();
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000E81FC File Offset: 0x000E71FC
		private void SaveCore()
		{
			if (this.Properties == null || this._PropertyValues == null || this.Properties.Count == 0)
			{
				return;
			}
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
				foreach (object obj2 in this.PropertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (settingsPropertyValue.Property.Provider == settingsProvider)
					{
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
				}
				if (settingsPropertyValueCollection.Count > 0)
				{
					settingsProvider.SetPropertyValues(this.Context, settingsPropertyValueCollection);
				}
			}
			foreach (object obj3 in this.PropertyValues)
			{
				SettingsPropertyValue settingsPropertyValue2 = (SettingsPropertyValue)obj3;
				settingsPropertyValue2.IsDirty = false;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x000E8340 File Offset: 0x000E7340
		public virtual SettingsPropertyCollection Properties
		{
			get
			{
				return this._Properties;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000E8348 File Offset: 0x000E7348
		public virtual SettingsProviderCollection Providers
		{
			get
			{
				return this._Providers;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003666 RID: 13926 RVA: 0x000E8350 File Offset: 0x000E7350
		public virtual SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				return this._PropertyValues;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x000E8358 File Offset: 0x000E7358
		public virtual SettingsContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000E8360 File Offset: 0x000E7360
		private void GetPropertiesFromProvider(SettingsProvider provider)
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
			if (settingsPropertyCollection.Count > 0)
			{
				SettingsPropertyValueCollection propertyValues = provider.GetPropertyValues(this.Context, settingsPropertyCollection);
				foreach (object obj2 in propertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (this._PropertyValues[settingsPropertyValue.Name] == null)
					{
						this._PropertyValues.Add(settingsPropertyValue);
					}
				}
			}
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000E8448 File Offset: 0x000E7448
		public static SettingsBase Synchronized(SettingsBase settingsBase)
		{
			settingsBase._IsSynchronized = true;
			return settingsBase;
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x000E8452 File Offset: 0x000E7452
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this._IsSynchronized;
			}
		}

		// Token: 0x04003188 RID: 12680
		private SettingsPropertyCollection _Properties;

		// Token: 0x04003189 RID: 12681
		private SettingsProviderCollection _Providers;

		// Token: 0x0400318A RID: 12682
		private SettingsPropertyValueCollection _PropertyValues;

		// Token: 0x0400318B RID: 12683
		private SettingsContext _Context;

		// Token: 0x0400318C RID: 12684
		private bool _IsSynchronized;
	}
}
