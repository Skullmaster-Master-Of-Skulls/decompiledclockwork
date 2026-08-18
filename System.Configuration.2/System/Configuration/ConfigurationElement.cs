using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000024 RID: 36
	public abstract class ConfigurationElement
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000099A9 File Offset: 0x00007BA9
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000099B1 File Offset: 0x00007BB1
		internal bool DataToWriteInternal
		{
			get
			{
				return this._bDataToWrite;
			}
			set
			{
				this._bDataToWrite = value;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000099BC File Offset: 0x00007BBC
		internal ConfigurationElement CreateElement(Type type)
		{
			ConfigurationElement configurationElement = (ConfigurationElement)TypeUtil.CreateInstanceRestricted(base.GetType(), type);
			configurationElement.CallInit();
			return configurationElement;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000099E2 File Offset: 0x00007BE2
		protected ConfigurationElement()
		{
			this._values = new ConfigurationValues();
			ConfigurationElement.ApplyValidator(this);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009A06 File Offset: 0x00007C06
		protected internal virtual void Init()
		{
			this._bInited = true;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009A0F File Offset: 0x00007C0F
		internal void CallInit()
		{
			if (!this._bInited)
			{
				this.Init();
				this._bInited = true;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009A26 File Offset: 0x00007C26
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00009A2E File Offset: 0x00007C2E
		internal bool ElementPresent
		{
			get
			{
				return this._bElementPresent;
			}
			set
			{
				this._bElementPresent = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00009A37 File Offset: 0x00007C37
		internal string ElementTagName
		{
			get
			{
				return this._elementTagName;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00009A3F File Offset: 0x00007C3F
		internal ConfigurationLockCollection LockedAttributesList
		{
			get
			{
				return this._lockedAttributesList;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009A47 File Offset: 0x00007C47
		internal ConfigurationLockCollection LockedAllExceptAttributesList
		{
			get
			{
				return this._lockedAllExceptAttributesList;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00009A4F File Offset: 0x00007C4F
		internal ConfigurationValueFlags ItemLocked
		{
			get
			{
				return this._fItemLocked;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009A57 File Offset: 0x00007C57
		public ConfigurationLockCollection LockAttributes
		{
			get
			{
				if (this._lockedAttributesList == null)
				{
					this._lockedAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedAttributes);
				}
				return this._lockedAttributesList;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009A74 File Offset: 0x00007C74
		internal void MergeLocks(ConfigurationElement source)
		{
			if (source != null)
			{
				this._fItemLocked = (((source._fItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default) ? (ConfigurationValueFlags.Inherited | source._fItemLocked) : this._fItemLocked);
				if (source._lockedAttributesList != null)
				{
					if (this._lockedAttributesList == null)
					{
						this._lockedAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedAttributes);
					}
					foreach (object obj in source._lockedAttributesList)
					{
						string name = (string)obj;
						this._lockedAttributesList.Add(name, ConfigurationValueFlags.Inherited);
					}
				}
				if (source._lockedAllExceptAttributesList != null)
				{
					if (this._lockedAllExceptAttributesList == null)
					{
						this._lockedAllExceptAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedExceptionList, string.Empty, source._lockedAllExceptAttributesList);
					}
					StringCollection stringCollection = this.IntersectLockCollections(this._lockedAllExceptAttributesList, source._lockedAllExceptAttributesList);
					this._lockedAllExceptAttributesList.ClearInternal(false);
					foreach (string name2 in stringCollection)
					{
						this._lockedAllExceptAttributesList.Add(name2, ConfigurationValueFlags.Default);
					}
				}
				if (source._lockedElementsList != null)
				{
					if (this._lockedElementsList == null)
					{
						this._lockedElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElements);
					}
					ConfigurationElementCollection configurationElementCollection = null;
					if (this.Properties.DefaultCollectionProperty != null)
					{
						configurationElementCollection = (this[this.Properties.DefaultCollectionProperty] as ConfigurationElementCollection);
						if (configurationElementCollection != null)
						{
							configurationElementCollection.internalElementTagName = source.ElementTagName;
							if (configurationElementCollection._lockedElementsList == null)
							{
								configurationElementCollection._lockedElementsList = this._lockedElementsList;
							}
						}
					}
					foreach (object obj2 in source._lockedElementsList)
					{
						string name3 = (string)obj2;
						this._lockedElementsList.Add(name3, ConfigurationValueFlags.Inherited);
						if (configurationElementCollection != null)
						{
							configurationElementCollection._lockedElementsList.Add(name3, ConfigurationValueFlags.Inherited);
						}
					}
				}
				if (source._lockedAllExceptElementsList != null)
				{
					if (this._lockedAllExceptElementsList == null || this._lockedAllExceptElementsList.Count == 0)
					{
						this._lockedAllExceptElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElementsExceptionList, source._elementTagName, source._lockedAllExceptElementsList);
					}
					StringCollection stringCollection2 = this.IntersectLockCollections(this._lockedAllExceptElementsList, source._lockedAllExceptElementsList);
					if (this.Properties.DefaultCollectionProperty != null)
					{
						ConfigurationElementCollection configurationElementCollection2 = this[this.Properties.DefaultCollectionProperty] as ConfigurationElementCollection;
						if (configurationElementCollection2 != null && configurationElementCollection2._lockedAllExceptElementsList == null)
						{
							configurationElementCollection2._lockedAllExceptElementsList = this._lockedAllExceptElementsList;
						}
					}
					this._lockedAllExceptElementsList.ClearInternal(false);
					foreach (string text in stringCollection2)
					{
						if (!this._lockedAllExceptElementsList.Contains(text) || text == this.ElementTagName)
						{
							this._lockedAllExceptElementsList.Add(text, ConfigurationValueFlags.Default);
						}
					}
					if (this._lockedAllExceptElementsList.HasParentElements)
					{
						foreach (object obj3 in this.Properties)
						{
							ConfigurationProperty configurationProperty = (ConfigurationProperty)obj3;
							if (!this._lockedAllExceptElementsList.Contains(configurationProperty.Name) && configurationProperty.IsConfigurationElementType)
							{
								((ConfigurationElement)this[configurationProperty]).SetLocked();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009E04 File Offset: 0x00008004
		internal void HandleLockedAttributes(ConfigurationElement source)
		{
			if (source != null && (source._lockedAttributesList != null || source._lockedAllExceptAttributesList != null))
			{
				foreach (object obj in source.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (((source._lockedAttributesList != null && (source._lockedAttributesList.Contains(propertyInformation.Name) || source._lockedAttributesList.Contains("*"))) || (source._lockedAllExceptAttributesList != null && !source._lockedAllExceptAttributesList.Contains(propertyInformation.Name))) && propertyInformation.Name != "lockAttributes" && propertyInformation.Name != "lockAllAttributesExcept")
					{
						if (this.ElementInformation.Properties[propertyInformation.Name] == null)
						{
							ConfigurationPropertyCollection properties = this.Properties;
							ConfigurationProperty property = source.Properties[propertyInformation.Name];
							properties.Add(property);
							this._evaluationElement = null;
							ConfigurationValueFlags valueFlags = ConfigurationValueFlags.Inherited | ConfigurationValueFlags.Locked;
							this._values.SetValue(propertyInformation.Name, propertyInformation.Value, valueFlags, source.PropertyInfoInternal(propertyInformation.Name));
						}
						else
						{
							if (this.ElementInformation.Properties[propertyInformation.Name].ValueOrigin == PropertyValueOrigin.SetHere)
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
								{
									propertyInformation.Name
								}));
							}
							this.ElementInformation.Properties[propertyInformation.Name].Value = propertyInformation.Value;
						}
					}
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00009FCC File Offset: 0x000081CC
		internal virtual void AssociateContext(BaseConfigurationRecord configRecord)
		{
			this._configRecord = configRecord;
			this.Values.AssociateContext(configRecord);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00009FE1 File Offset: 0x000081E1
		public ConfigurationLockCollection LockAllAttributesExcept
		{
			get
			{
				if (this._lockedAllExceptAttributesList == null)
				{
					this._lockedAllExceptAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedExceptionList, this._elementTagName);
				}
				return this._lockedAllExceptAttributesList;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000A004 File Offset: 0x00008204
		public ConfigurationLockCollection LockElements
		{
			get
			{
				if (this._lockedElementsList == null)
				{
					this._lockedElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElements);
				}
				return this._lockedElementsList;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000A021 File Offset: 0x00008221
		public ConfigurationLockCollection LockAllElementsExcept
		{
			get
			{
				if (this._lockedAllExceptElementsList == null)
				{
					this._lockedAllExceptElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElementsExceptionList, this._elementTagName);
				}
				return this._lockedAllExceptElementsList;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000A044 File Offset: 0x00008244
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000A054 File Offset: 0x00008254
		public bool LockItem
		{
			get
			{
				return (this._fItemLocked & ConfigurationValueFlags.Locked) > ConfigurationValueFlags.Default;
			}
			set
			{
				if ((this._fItemLocked & ConfigurationValueFlags.Inherited) == ConfigurationValueFlags.Default)
				{
					this._fItemLocked = (value ? ConfigurationValueFlags.Locked : ConfigurationValueFlags.Default);
					this._fItemLocked |= ConfigurationValueFlags.Modified;
					return;
				}
				throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
				{
					"lockItem"
				}));
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A0A4 File Offset: 0x000082A4
		protected internal virtual bool IsModified()
		{
			if (this._bModified)
			{
				return true;
			}
			if (this._lockedAttributesList != null && this._lockedAttributesList.IsModified)
			{
				return true;
			}
			if (this._lockedAllExceptAttributesList != null && this._lockedAllExceptAttributesList.IsModified)
			{
				return true;
			}
			if (this._lockedElementsList != null && this._lockedElementsList.IsModified)
			{
				return true;
			}
			if (this._lockedAllExceptElementsList != null && this._lockedAllExceptElementsList.IsModified)
			{
				return true;
			}
			if ((this._fItemLocked & ConfigurationValueFlags.Modified) != ConfigurationValueFlags.Default)
			{
				return true;
			}
			foreach (object obj in this._values.ConfigurationElements)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)obj;
				if (configurationElement.IsModified())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A17C File Offset: 0x0000837C
		protected internal virtual void ResetModified()
		{
			this._bModified = false;
			if (this._lockedAttributesList != null)
			{
				this._lockedAttributesList.ResetModified();
			}
			if (this._lockedAllExceptAttributesList != null)
			{
				this._lockedAllExceptAttributesList.ResetModified();
			}
			if (this._lockedElementsList != null)
			{
				this._lockedElementsList.ResetModified();
			}
			if (this._lockedAllExceptElementsList != null)
			{
				this._lockedAllExceptElementsList.ResetModified();
			}
			foreach (object obj in this._values.ConfigurationElements)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)obj;
				configurationElement.ResetModified();
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A22C File Offset: 0x0000842C
		public virtual bool IsReadOnly()
		{
			return this._bReadOnly;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A234 File Offset: 0x00008434
		protected internal virtual void SetReadOnly()
		{
			this._bReadOnly = true;
			foreach (object obj in this._values.ConfigurationElements)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)obj;
				configurationElement.SetReadOnly();
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A298 File Offset: 0x00008498
		internal void SetLocked()
		{
			this._fItemLocked = (ConfigurationValueFlags.Locked | ConfigurationValueFlags.XMLParentInherited);
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty prop = (ConfigurationProperty)obj;
				ConfigurationElement configurationElement = this[prop] as ConfigurationElement;
				if (configurationElement != null)
				{
					if (configurationElement.GetType() != base.GetType())
					{
						configurationElement.SetLocked();
					}
					ConfigurationElementCollection configurationElementCollection = this[prop] as ConfigurationElementCollection;
					if (configurationElementCollection != null)
					{
						foreach (object obj2 in configurationElementCollection)
						{
							ConfigurationElement configurationElement2 = obj2 as ConfigurationElement;
							if (configurationElement2 != null)
							{
								configurationElement2.SetLocked();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000A388 File Offset: 0x00008588
		internal ArrayList GetErrorsList()
		{
			ArrayList arrayList = new ArrayList();
			this.ListErrors(arrayList);
			return arrayList;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A3A4 File Offset: 0x000085A4
		internal ConfigurationErrorsException GetErrors()
		{
			ArrayList errorsList = this.GetErrorsList();
			if (errorsList.Count == 0)
			{
				return null;
			}
			return new ConfigurationErrorsException(errorsList);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A3CC File Offset: 0x000085CC
		protected virtual void ListErrors(IList errorList)
		{
			foreach (object obj in this._values.InvalidValues)
			{
				InvalidPropValue invalidPropValue = (InvalidPropValue)obj;
				errorList.Add(invalidPropValue.Error);
			}
			foreach (object obj2 in this._values.ConfigurationElements)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)obj2;
				configurationElement.ListErrors(errorList);
				ConfigurationElementCollection configurationElementCollection = configurationElement as ConfigurationElementCollection;
				if (configurationElementCollection != null)
				{
					foreach (object obj3 in configurationElementCollection)
					{
						ConfigurationElement configurationElement2 = (ConfigurationElement)obj3;
						configurationElement2.ListErrors(errorList);
					}
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005E74 File Offset: 0x00004074
		protected internal virtual void InitializeDefault()
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000A4D8 File Offset: 0x000086D8
		internal void CheckLockedElement(string elementName, XmlReader reader)
		{
			if (elementName != null && ((this._lockedElementsList != null && (this._lockedElementsList.DefinedInParent("*") || this._lockedElementsList.DefinedInParent(elementName))) || (this._lockedAllExceptElementsList != null && this._lockedAllExceptElementsList.Count != 0 && this._lockedAllExceptElementsList.HasParentElements && !this._lockedAllExceptElementsList.DefinedInParent(elementName)) || (this._fItemLocked & ConfigurationValueFlags.Inherited) != ConfigurationValueFlags.Default))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_element_locked", new object[]
				{
					elementName
				}), reader);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A568 File Offset: 0x00008768
		internal void RemoveAllInheritedLocks()
		{
			if (this._lockedAttributesList != null)
			{
				this._lockedAttributesList.RemoveInheritedLocks();
			}
			if (this._lockedElementsList != null)
			{
				this._lockedElementsList.RemoveInheritedLocks();
			}
			if (this._lockedAllExceptAttributesList != null)
			{
				this._lockedAllExceptAttributesList.RemoveInheritedLocks();
			}
			if (this._lockedAllExceptElementsList != null)
			{
				this._lockedAllExceptElementsList.RemoveInheritedLocks();
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A5C4 File Offset: 0x000087C4
		internal void ResetLockLists(ConfigurationElement parentElement)
		{
			this._lockedAttributesList = null;
			this._lockedAllExceptAttributesList = null;
			this._lockedElementsList = null;
			this._lockedAllExceptElementsList = null;
			if (parentElement != null)
			{
				this._fItemLocked = (((parentElement._fItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default) ? (ConfigurationValueFlags.Inherited | parentElement._fItemLocked) : ConfigurationValueFlags.Default);
				if (parentElement._lockedAttributesList != null)
				{
					this._lockedAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedAttributes);
					foreach (object obj in parentElement._lockedAttributesList)
					{
						string name = (string)obj;
						this._lockedAttributesList.Add(name, ConfigurationValueFlags.Inherited);
					}
				}
				if (parentElement._lockedAllExceptAttributesList != null)
				{
					this._lockedAllExceptAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedExceptionList, string.Empty, parentElement._lockedAllExceptAttributesList);
				}
				if (parentElement._lockedElementsList != null)
				{
					this._lockedElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElements);
					if (this.Properties.DefaultCollectionProperty != null)
					{
						ConfigurationElementCollection configurationElementCollection = this[this.Properties.DefaultCollectionProperty] as ConfigurationElementCollection;
						if (configurationElementCollection != null)
						{
							configurationElementCollection.internalElementTagName = parentElement.ElementTagName;
							if (configurationElementCollection._lockedElementsList == null)
							{
								configurationElementCollection._lockedElementsList = this._lockedElementsList;
							}
						}
					}
					foreach (object obj2 in parentElement._lockedElementsList)
					{
						string name2 = (string)obj2;
						this._lockedElementsList.Add(name2, ConfigurationValueFlags.Inherited);
					}
				}
				if (parentElement._lockedAllExceptElementsList != null)
				{
					this._lockedAllExceptElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElementsExceptionList, parentElement._elementTagName, parentElement._lockedAllExceptElementsList);
					if (this.Properties.DefaultCollectionProperty != null)
					{
						ConfigurationElementCollection configurationElementCollection2 = this[this.Properties.DefaultCollectionProperty] as ConfigurationElementCollection;
						if (configurationElementCollection2 != null && configurationElementCollection2._lockedAllExceptElementsList == null)
						{
							configurationElementCollection2._lockedAllExceptElementsList = this._lockedAllExceptElementsList;
						}
					}
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A7B0 File Offset: 0x000089B0
		protected internal virtual void Reset(ConfigurationElement parentElement)
		{
			this.Values.Clear();
			this.ResetLockLists(parentElement);
			ConfigurationPropertyCollection properties = this.Properties;
			this._bElementPresent = false;
			if (parentElement == null)
			{
				this.InitializeDefault();
				return;
			}
			bool flag = false;
			ConfigurationPropertyCollection configurationPropertyCollection = null;
			for (int i = 0; i < parentElement.Values.Count; i++)
			{
				string key = parentElement.Values.GetKey(i);
				ConfigurationValue configValue = parentElement.Values.GetConfigValue(i);
				object obj = (configValue != null) ? configValue.Value : null;
				PropertySourceInfo sourceInfo = (configValue != null) ? configValue.SourceInfo : null;
				ConfigurationProperty configurationProperty = parentElement.Properties[key];
				if (configurationProperty != null && (configurationPropertyCollection == null || configurationPropertyCollection.Contains(configurationProperty.Name)))
				{
					if (configurationProperty.IsConfigurationElementType)
					{
						flag = true;
					}
					else
					{
						ConfigurationValueFlags valueFlags = ConfigurationValueFlags.Inherited | (((this._lockedAttributesList != null && (this._lockedAttributesList.Contains(key) || this._lockedAttributesList.Contains("*"))) || (this._lockedAllExceptAttributesList != null && !this._lockedAllExceptAttributesList.Contains(key))) ? ConfigurationValueFlags.Locked : ConfigurationValueFlags.Default);
						if (obj != ConfigurationElement.s_nullPropertyValue)
						{
							this._values.SetValue(key, obj, valueFlags, sourceInfo);
						}
						if (!properties.Contains(key))
						{
							properties.Add(configurationProperty);
							this._values.SetValue(key, obj, valueFlags, sourceInfo);
						}
					}
				}
			}
			if (flag)
			{
				for (int j = 0; j < parentElement.Values.Count; j++)
				{
					string key2 = parentElement.Values.GetKey(j);
					object obj2 = parentElement.Values[j];
					ConfigurationProperty configurationProperty2 = parentElement.Properties[key2];
					if (configurationProperty2 != null && configurationProperty2.IsConfigurationElementType)
					{
						ConfigurationElement configurationElement = (ConfigurationElement)this[configurationProperty2];
						configurationElement.Reset((ConfigurationElement)obj2);
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A97C File Offset: 0x00008B7C
		public override bool Equals(object compareTo)
		{
			ConfigurationElement configurationElement = compareTo as ConfigurationElement;
			if (configurationElement == null || compareTo.GetType() != base.GetType() || (configurationElement != null && configurationElement.Properties.Count != this.Properties.Count))
			{
				return false;
			}
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (!object.Equals(this.Values[configurationProperty.Name], configurationElement.Values[configurationProperty.Name]) && ((this.Values[configurationProperty.Name] != null && this.Values[configurationProperty.Name] != ConfigurationElement.s_nullPropertyValue) || !object.Equals(configurationElement.Values[configurationProperty.Name], configurationProperty.DefaultValue)) && ((configurationElement.Values[configurationProperty.Name] != null && configurationElement.Values[configurationProperty.Name] != ConfigurationElement.s_nullPropertyValue) || !object.Equals(this.Values[configurationProperty.Name], configurationProperty.DefaultValue)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty prop = (ConfigurationProperty)obj;
				object obj2 = this[prop];
				if (obj2 != null)
				{
					num ^= this[prop].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x1700006A RID: 106
		protected internal object this[ConfigurationProperty prop]
		{
			get
			{
				object obj = this._values[prop.Name];
				if (obj == null)
				{
					if (prop.IsConfigurationElementType)
					{
						object syncRoot = this._values.SyncRoot;
						lock (syncRoot)
						{
							obj = this._values[prop.Name];
							if (obj == null)
							{
								ConfigurationElement configurationElement = this.CreateElement(prop.Type);
								if (this._bReadOnly)
								{
									configurationElement.SetReadOnly();
								}
								if (typeof(ConfigurationElementCollection).IsAssignableFrom(prop.Type))
								{
									ConfigurationElementCollection configurationElementCollection = configurationElement as ConfigurationElementCollection;
									if (prop.AddElementName != null)
									{
										configurationElementCollection.AddElementName = prop.AddElementName;
									}
									if (prop.RemoveElementName != null)
									{
										configurationElementCollection.RemoveElementName = prop.RemoveElementName;
									}
									if (prop.ClearElementName != null)
									{
										configurationElementCollection.ClearElementName = prop.ClearElementName;
									}
								}
								this._values.SetValue(prop.Name, configurationElement, ConfigurationValueFlags.Inherited, null);
								obj = configurationElement;
							}
							goto IL_FF;
						}
					}
					obj = prop.DefaultValue;
				}
				else if (obj == ConfigurationElement.s_nullPropertyValue)
				{
					obj = null;
				}
				IL_FF:
				if (obj is InvalidPropValue)
				{
					throw ((InvalidPropValue)obj).Error;
				}
				return obj;
			}
			set
			{
				this.SetPropertyValue(prop, value, false);
			}
		}

		// Token: 0x1700006B RID: 107
		protected internal object this[string propertyName]
		{
			get
			{
				ConfigurationProperty configurationProperty = this.Properties[propertyName];
				if (configurationProperty == null)
				{
					configurationProperty = this.Properties[""];
					if (configurationProperty.ProvidedName != propertyName)
					{
						return null;
					}
				}
				return this[configurationProperty];
			}
			set
			{
				this.SetPropertyValue(this.Properties[propertyName], value, false);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		private static void ApplyInstanceAttributes(object instance)
		{
			Type type = instance.GetType();
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				ConfigurationPropertyAttribute configurationPropertyAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationPropertyAttribute)) as ConfigurationPropertyAttribute;
				if (configurationPropertyAttribute != null)
				{
					Type propertyType = propertyInfo.PropertyType;
					if (typeof(ConfigurationElementCollection).IsAssignableFrom(propertyType))
					{
						ConfigurationCollectionAttribute configurationCollectionAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
						if (configurationCollectionAttribute == null)
						{
							configurationCollectionAttribute = (Attribute.GetCustomAttribute(propertyType, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute);
						}
						ConfigurationElementCollection configurationElementCollection = propertyInfo.GetValue(instance, null) as ConfigurationElementCollection;
						if (configurationElementCollection == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_element_null_instance", new object[]
							{
								propertyInfo.Name,
								configurationPropertyAttribute.Name
							}));
						}
						if (configurationCollectionAttribute != null)
						{
							if (configurationCollectionAttribute.AddItemName.IndexOf(',') == -1)
							{
								configurationElementCollection.AddElementName = configurationCollectionAttribute.AddItemName;
							}
							configurationElementCollection.RemoveElementName = configurationCollectionAttribute.RemoveItemName;
							configurationElementCollection.ClearElementName = configurationCollectionAttribute.ClearItemsName;
						}
					}
					else if (typeof(ConfigurationElement).IsAssignableFrom(propertyType))
					{
						object value = propertyInfo.GetValue(instance, null);
						if (value == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_element_null_instance", new object[]
							{
								propertyInfo.Name,
								configurationPropertyAttribute.Name
							}));
						}
						ConfigurationElement.ApplyInstanceAttributes(value);
					}
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000AE60 File Offset: 0x00009060
		private static bool PropertiesFromType(Type type, out ConfigurationPropertyCollection result)
		{
			ConfigurationPropertyCollection configurationPropertyCollection = (ConfigurationPropertyCollection)ConfigurationElement.s_propertyBags[type];
			result = null;
			bool result2 = false;
			if (configurationPropertyCollection == null)
			{
				object syncRoot = ConfigurationElement.s_propertyBags.SyncRoot;
				lock (syncRoot)
				{
					configurationPropertyCollection = (ConfigurationPropertyCollection)ConfigurationElement.s_propertyBags[type];
					if (configurationPropertyCollection == null)
					{
						configurationPropertyCollection = ConfigurationElement.CreatePropertyBagFromType(type);
						ConfigurationElement.s_propertyBags[type] = configurationPropertyCollection;
						result2 = true;
					}
				}
			}
			result = configurationPropertyCollection;
			return result2;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000AEE4 File Offset: 0x000090E4
		private static ConfigurationPropertyCollection CreatePropertyBagFromType(Type type)
		{
			if (typeof(ConfigurationElement).IsAssignableFrom(type))
			{
				ConfigurationValidatorAttribute configurationValidatorAttribute = Attribute.GetCustomAttribute(type, typeof(ConfigurationValidatorAttribute)) as ConfigurationValidatorAttribute;
				if (configurationValidatorAttribute != null)
				{
					configurationValidatorAttribute.SetDeclaringType(type);
					ConfigurationValidatorBase validatorInstance = configurationValidatorAttribute.ValidatorInstance;
					if (validatorInstance != null)
					{
						ConfigurationElement.CachePerTypeValidator(type, validatorInstance);
					}
				}
			}
			ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
			foreach (PropertyInfo propertyInformation in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				ConfigurationProperty configurationProperty = ConfigurationElement.CreateConfigurationPropertyFromAttributes(propertyInformation);
				if (configurationProperty != null)
				{
					configurationPropertyCollection.Add(configurationProperty);
				}
			}
			return configurationPropertyCollection;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000AF74 File Offset: 0x00009174
		private static ConfigurationProperty CreateConfigurationPropertyFromAttributes(PropertyInfo propertyInformation)
		{
			ConfigurationProperty configurationProperty = null;
			ConfigurationPropertyAttribute configurationPropertyAttribute = Attribute.GetCustomAttribute(propertyInformation, typeof(ConfigurationPropertyAttribute)) as ConfigurationPropertyAttribute;
			if (configurationPropertyAttribute != null)
			{
				configurationProperty = new ConfigurationProperty(propertyInformation);
			}
			if (configurationProperty != null && typeof(ConfigurationElement).IsAssignableFrom(configurationProperty.Type))
			{
				ConfigurationPropertyCollection configurationPropertyCollection = null;
				ConfigurationElement.PropertiesFromType(configurationProperty.Type, out configurationPropertyCollection);
			}
			return configurationProperty;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000AFD0 File Offset: 0x000091D0
		private static void CachePerTypeValidator(Type type, ConfigurationValidatorBase validator)
		{
			if (ConfigurationElement.s_perTypeValidators == null)
			{
				ConfigurationElement.s_perTypeValidators = new Dictionary<Type, ConfigurationValidatorBase>();
			}
			if (!validator.CanValidate(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Validator_does_not_support_elem_type", new object[]
				{
					type.Name
				}));
			}
			ConfigurationElement.s_perTypeValidators.Add(type, validator);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B028 File Offset: 0x00009228
		private static void ApplyValidatorsRecursive(ConfigurationElement root)
		{
			ConfigurationElement.ApplyValidator(root);
			foreach (object obj in root._values.ConfigurationElements)
			{
				ConfigurationElement root2 = (ConfigurationElement)obj;
				ConfigurationElement.ApplyValidatorsRecursive(root2);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B08C File Offset: 0x0000928C
		private static void ApplyValidator(ConfigurationElement elem)
		{
			if (ConfigurationElement.s_perTypeValidators != null && ConfigurationElement.s_perTypeValidators.ContainsKey(elem.GetType()))
			{
				elem._elementProperty = new ConfigurationElementProperty(ConfigurationElement.s_perTypeValidators[elem.GetType()]);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B0C8 File Offset: 0x000092C8
		protected void SetPropertyValue(ConfigurationProperty prop, object value, bool ignoreLocks)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
			if (!ignoreLocks && ((this._lockedAllExceptAttributesList != null && this._lockedAllExceptAttributesList.HasParentElements && !this._lockedAllExceptAttributesList.DefinedInParent(prop.Name)) || (this._lockedAttributesList != null && (this._lockedAttributesList.DefinedInParent(prop.Name) || this._lockedAttributesList.DefinedInParent("*"))) || ((this._fItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default && (this._fItemLocked & ConfigurationValueFlags.Inherited) != ConfigurationValueFlags.Default)))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
				{
					prop.Name
				}));
			}
			this._bModified = true;
			if (value != null)
			{
				prop.Validate(value);
			}
			this._values[prop.Name] = ((value != null) ? value : ConfigurationElement.s_nullPropertyValue);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000B1A8 File Offset: 0x000093A8
		protected internal virtual ConfigurationPropertyCollection Properties
		{
			get
			{
				ConfigurationPropertyCollection result = null;
				if (ConfigurationElement.PropertiesFromType(base.GetType(), out result))
				{
					ConfigurationElement.ApplyInstanceAttributes(this);
					ConfigurationElement.ApplyValidatorsRecursive(this);
				}
				return result;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000B1D3 File Offset: 0x000093D3
		internal ConfigurationValues Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B1DB File Offset: 0x000093DB
		internal PropertySourceInfo PropertyInfoInternal(string propertyName)
		{
			return this._values.GetSourceInfo(propertyName);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B1EC File Offset: 0x000093EC
		internal string PropertyFileName(string propertyName)
		{
			PropertySourceInfo propertySourceInfo = this.PropertyInfoInternal(propertyName);
			if (propertySourceInfo == null)
			{
				propertySourceInfo = this.PropertyInfoInternal(string.Empty);
			}
			if (propertySourceInfo == null)
			{
				return string.Empty;
			}
			return propertySourceInfo.FileName;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000B220 File Offset: 0x00009420
		internal int PropertyLineNumber(string propertyName)
		{
			PropertySourceInfo propertySourceInfo = this.PropertyInfoInternal(propertyName);
			if (propertySourceInfo == null)
			{
				propertySourceInfo = this.PropertyInfoInternal(string.Empty);
			}
			if (propertySourceInfo == null)
			{
				return 0;
			}
			return propertySourceInfo.LineNumber;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B250 File Offset: 0x00009450
		internal virtual void Dump(TextWriter tw)
		{
			tw.WriteLine("Type: " + base.GetType().FullName);
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
			{
				tw.WriteLine("{0}: {1}", propertyInfo.Name, propertyInfo.GetValue(this, null));
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000B2B0 File Offset: 0x000094B0
		protected internal virtual void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (sourceElement != null)
			{
				bool flag = false;
				this._lockedAllExceptAttributesList = sourceElement._lockedAllExceptAttributesList;
				this._lockedAllExceptElementsList = sourceElement._lockedAllExceptElementsList;
				this._fItemLocked = sourceElement._fItemLocked;
				this._lockedAttributesList = sourceElement._lockedAttributesList;
				this._lockedElementsList = sourceElement._lockedElementsList;
				this.AssociateContext(sourceElement._configRecord);
				if (parentElement != null)
				{
					if (parentElement._lockedAttributesList != null)
					{
						this._lockedAttributesList = this.UnMergeLockList(sourceElement._lockedAttributesList, parentElement._lockedAttributesList, saveMode);
					}
					if (parentElement._lockedElementsList != null)
					{
						this._lockedElementsList = this.UnMergeLockList(sourceElement._lockedElementsList, parentElement._lockedElementsList, saveMode);
					}
					if (parentElement._lockedAllExceptAttributesList != null)
					{
						this._lockedAllExceptAttributesList = this.UnMergeLockList(sourceElement._lockedAllExceptAttributesList, parentElement._lockedAllExceptAttributesList, saveMode);
					}
					if (parentElement._lockedAllExceptElementsList != null)
					{
						this._lockedAllExceptElementsList = this.UnMergeLockList(sourceElement._lockedAllExceptElementsList, parentElement._lockedAllExceptElementsList, saveMode);
					}
				}
				ConfigurationPropertyCollection properties = this.Properties;
				ConfigurationPropertyCollection configurationPropertyCollection = null;
				for (int i = 0; i < sourceElement.Values.Count; i++)
				{
					string key = sourceElement.Values.GetKey(i);
					object obj = sourceElement.Values[i];
					ConfigurationProperty configurationProperty = sourceElement.Properties[key];
					if (configurationProperty != null && (configurationPropertyCollection == null || configurationPropertyCollection.Contains(configurationProperty.Name)))
					{
						if (configurationProperty.IsConfigurationElementType)
						{
							flag = true;
						}
						else if (obj != ConfigurationElement.s_nullPropertyValue && !properties.Contains(key))
						{
							ConfigurationValueFlags valueFlags = sourceElement.Values.RetrieveFlags(key);
							this._values.SetValue(key, obj, valueFlags, null);
							properties.Add(configurationProperty);
						}
					}
				}
				foreach (object obj2 in this.Properties)
				{
					ConfigurationProperty configurationProperty2 = (ConfigurationProperty)obj2;
					if (configurationProperty2 != null && (configurationPropertyCollection == null || configurationPropertyCollection.Contains(configurationProperty2.Name)))
					{
						if (configurationProperty2.IsConfigurationElementType)
						{
							flag = true;
						}
						else
						{
							object obj3 = sourceElement.Values[configurationProperty2.Name];
							if ((configurationProperty2.IsRequired || saveMode == ConfigurationSaveMode.Full) && (obj3 == null || obj3 == ConfigurationElement.s_nullPropertyValue) && configurationProperty2.DefaultValue != null)
							{
								obj3 = configurationProperty2.DefaultValue;
							}
							if (obj3 != null && obj3 != ConfigurationElement.s_nullPropertyValue)
							{
								object obj4 = null;
								if (parentElement != null)
								{
									obj4 = parentElement.Values[configurationProperty2.Name];
								}
								if (obj4 == null)
								{
									obj4 = configurationProperty2.DefaultValue;
								}
								switch (saveMode)
								{
								case ConfigurationSaveMode.Modified:
								{
									bool flag2 = sourceElement.Values.IsModified(configurationProperty2.Name);
									bool flag3 = sourceElement.Values.IsInherited(configurationProperty2.Name);
									if (configurationProperty2.IsRequired || flag2 || !flag3 || (parentElement == null && flag3 && !object.Equals(obj3, obj4)))
									{
										this._values[configurationProperty2.Name] = obj3;
									}
									break;
								}
								case ConfigurationSaveMode.Minimal:
									if (!object.Equals(obj3, obj4) || configurationProperty2.IsRequired)
									{
										this._values[configurationProperty2.Name] = obj3;
									}
									break;
								case ConfigurationSaveMode.Full:
									if (obj3 != null && obj3 != ConfigurationElement.s_nullPropertyValue)
									{
										this._values[configurationProperty2.Name] = obj3;
									}
									else
									{
										this._values[configurationProperty2.Name] = obj4;
									}
									break;
								}
							}
						}
					}
				}
				if (flag)
				{
					foreach (object obj5 in this.Properties)
					{
						ConfigurationProperty configurationProperty3 = (ConfigurationProperty)obj5;
						if (configurationProperty3.IsConfigurationElementType)
						{
							ConfigurationElement parentElement2 = (ConfigurationElement)((parentElement != null) ? parentElement[configurationProperty3] : null);
							ConfigurationElement configurationElement = (ConfigurationElement)this[configurationProperty3];
							if ((ConfigurationElement)sourceElement[configurationProperty3] != null)
							{
								configurationElement.Unmerge((ConfigurationElement)sourceElement[configurationProperty3], parentElement2, saveMode);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000B6DC File Offset: 0x000098DC
		protected internal virtual bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this._configRecord != null && this._configRecord.TargetFramework != null)
			{
				ConfigurationSection configurationSection = null;
				if (this._configRecord.SectionsStack.Count > 0)
				{
					configurationSection = (this._configRecord.SectionsStack.Peek() as ConfigurationSection);
				}
				if (configurationSection != null && !configurationSection.ShouldSerializeElementInTargetVersion(this, elementName, this._configRecord.TargetFramework))
				{
					return false;
				}
			}
			bool flag = this._bDataToWrite;
			if ((this._lockedElementsList != null && this._lockedElementsList.DefinedInParent(elementName)) || (this._lockedAllExceptElementsList != null && this._lockedAllExceptElementsList.HasParentElements && !this._lockedAllExceptElementsList.DefinedInParent(elementName)))
			{
				return flag;
			}
			if (this.SerializeElement(null, false))
			{
				if (writer != null)
				{
					writer.WriteStartElement(elementName);
				}
				flag |= this.SerializeElement(writer, false);
				if (writer != null)
				{
					writer.WriteEndElement();
				}
			}
			return flag;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000B7B4 File Offset: 0x000099B4
		protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			this.PreSerialize(writer);
			bool flag = this._bDataToWrite;
			bool flag2 = false;
			bool flag3 = false;
			ConfigurationPropertyCollection properties = this.Properties;
			ConfigurationPropertyCollection configurationPropertyCollection = null;
			for (int i = 0; i < this._values.Count; i++)
			{
				string key = this._values.GetKey(i);
				object obj = this._values[i];
				ConfigurationProperty configurationProperty = properties[key];
				if (configurationProperty != null && (configurationPropertyCollection == null || configurationPropertyCollection.Contains(configurationProperty.Name)))
				{
					if (configurationProperty.IsVersionCheckRequired && this._configRecord != null && this._configRecord.TargetFramework != null)
					{
						ConfigurationSection configurationSection = null;
						if (this._configRecord.SectionsStack.Count > 0)
						{
							configurationSection = (this._configRecord.SectionsStack.Peek() as ConfigurationSection);
						}
						if (configurationSection != null && !configurationSection.ShouldSerializePropertyInTargetVersion(configurationProperty, configurationProperty.Name, this._configRecord.TargetFramework, this))
						{
							goto IL_1F5;
						}
					}
					if (configurationProperty.IsConfigurationElementType)
					{
						flag2 = true;
					}
					else
					{
						if ((this._lockedAllExceptAttributesList != null && this._lockedAllExceptAttributesList.HasParentElements && !this._lockedAllExceptAttributesList.DefinedInParent(configurationProperty.Name)) || (this._lockedAttributesList != null && this._lockedAttributesList.DefinedInParent(configurationProperty.Name)))
						{
							if (configurationProperty.IsRequired)
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_locked", new object[]
								{
									configurationProperty.Name
								}));
							}
							obj = ConfigurationElement.s_nullPropertyValue;
						}
						if (obj != ConfigurationElement.s_nullPropertyValue && (!serializeCollectionKey || configurationProperty.IsKey))
						{
							string text;
							if (obj is InvalidPropValue)
							{
								text = ((InvalidPropValue)obj).Value;
							}
							else
							{
								configurationProperty.Validate(obj);
								text = configurationProperty.ConvertToString(obj);
							}
							if (text != null && writer != null)
							{
								if (configurationProperty.IsTypeStringTransformationRequired)
								{
									text = this.GetTransformedTypeString(text);
								}
								if (configurationProperty.IsAssemblyStringTransformationRequired)
								{
									text = this.GetTransformedAssemblyString(text);
								}
								writer.WriteAttributeString(configurationProperty.Name, text);
							}
							flag = (flag || text != null);
						}
					}
				}
				IL_1F5:;
			}
			if (!serializeCollectionKey)
			{
				flag |= this.SerializeLockList(this._lockedAttributesList, "lockAttributes", writer);
				flag |= this.SerializeLockList(this._lockedAllExceptAttributesList, "lockAllAttributesExcept", writer);
				flag |= this.SerializeLockList(this._lockedElementsList, "lockElements", writer);
				flag |= this.SerializeLockList(this._lockedAllExceptElementsList, "lockAllElementsExcept", writer);
				if ((this._fItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default && (this._fItemLocked & ConfigurationValueFlags.Inherited) == ConfigurationValueFlags.Default && (this._fItemLocked & ConfigurationValueFlags.XMLParentInherited) == ConfigurationValueFlags.Default)
				{
					flag = true;
					if (writer != null)
					{
						writer.WriteAttributeString("lockItem", true.ToString().ToLower(CultureInfo.InvariantCulture));
					}
				}
			}
			if (flag2)
			{
				for (int j = 0; j < this._values.Count; j++)
				{
					string key2 = this._values.GetKey(j);
					object obj2 = this._values[j];
					ConfigurationProperty configurationProperty2 = properties[key2];
					if ((!serializeCollectionKey || configurationProperty2.IsKey) && obj2 is ConfigurationElement && (this._lockedElementsList == null || !this._lockedElementsList.DefinedInParent(key2)) && (this._lockedAllExceptElementsList == null || !this._lockedAllExceptElementsList.HasParentElements || this._lockedAllExceptElementsList.DefinedInParent(key2)))
					{
						ConfigurationElement configurationElement = (ConfigurationElement)obj2;
						if (configurationProperty2.Name != ConfigurationProperty.DefaultCollectionPropertyName)
						{
							flag |= configurationElement.SerializeToXmlElement(writer, configurationProperty2.Name);
						}
						else
						{
							if (flag3)
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_base_element_cannot_have_multiple_child_elements", new object[]
								{
									configurationProperty2.Name
								}));
							}
							configurationElement._lockedAttributesList = null;
							configurationElement._lockedAllExceptAttributesList = null;
							configurationElement._lockedElementsList = null;
							configurationElement._lockedAllExceptElementsList = null;
							flag |= configurationElement.SerializeElement(writer, false);
							flag3 = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BB98 File Offset: 0x00009D98
		private bool SerializeLockList(ConfigurationLockCollection list, string elementKey, XmlWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (list != null)
			{
				foreach (object obj in list)
				{
					string text = (string)obj;
					if (!list.DefinedInParent(text))
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(text);
					}
				}
			}
			if (writer != null && stringBuilder.Length != 0)
			{
				writer.WriteAttributeString(elementKey, stringBuilder.ToString());
			}
			return stringBuilder.Length != 0;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BC34 File Offset: 0x00009E34
		internal void ReportInvalidLock(string attribToLockTrim, ConfigurationLockCollectionType lockedType, ConfigurationValue value, string collectionProperties)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(collectionProperties) && (lockedType == ConfigurationLockCollectionType.LockedElements || lockedType == ConfigurationLockCollectionType.LockedElementsExceptionList))
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(collectionProperties);
			}
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (configurationProperty.Name != "lockAttributes" && configurationProperty.Name != "lockAllAttributesExcept" && configurationProperty.Name != "lockElements" && configurationProperty.Name != "lockAllElementsExcept")
				{
					if (lockedType == ConfigurationLockCollectionType.LockedElements || lockedType == ConfigurationLockCollectionType.LockedElementsExceptionList)
					{
						if (typeof(ConfigurationElement).IsAssignableFrom(configurationProperty.Type))
						{
							if (stringBuilder.Length != 0)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append("'");
							stringBuilder.Append(configurationProperty.Name);
							stringBuilder.Append("'");
						}
					}
					else if (!typeof(ConfigurationElement).IsAssignableFrom(configurationProperty.Type))
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append("'");
						stringBuilder.Append(configurationProperty.Name);
						stringBuilder.Append("'");
					}
				}
			}
			string @string;
			if (lockedType == ConfigurationLockCollectionType.LockedElements || lockedType == ConfigurationLockCollectionType.LockedElementsExceptionList)
			{
				if (value != null)
				{
					@string = SR.GetString("Config_base_invalid_element_to_lock");
				}
				else
				{
					@string = SR.GetString("Config_base_invalid_element_to_lock_by_add");
				}
			}
			else if (value != null)
			{
				@string = SR.GetString("Config_base_invalid_attribute_to_lock");
			}
			else
			{
				@string = SR.GetString("Config_base_invalid_attribute_to_lock_by_add");
			}
			if (value != null)
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, @string, new object[]
				{
					attribToLockTrim,
					stringBuilder.ToString()
				}), value.SourceInfo.FileName, value.SourceInfo.LineNumber);
			}
			throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, @string, new object[]
			{
				attribToLockTrim,
				stringBuilder.ToString()
			}));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BE7C File Offset: 0x0000A07C
		private ConfigurationLockCollection ParseLockedAttributes(ConfigurationValue value, ConfigurationLockCollectionType lockType)
		{
			ConfigurationLockCollection configurationLockCollection = new ConfigurationLockCollection(this, lockType);
			string text = (string)value.Value;
			if (string.IsNullOrEmpty(text))
			{
				if (lockType == ConfigurationLockCollectionType.LockedAttributes)
				{
					throw new ConfigurationErrorsException(SR.GetString("Empty_attribute", new object[]
					{
						"lockAttributes"
					}), value.SourceInfo.FileName, value.SourceInfo.LineNumber);
				}
				if (lockType == ConfigurationLockCollectionType.LockedElements)
				{
					throw new ConfigurationErrorsException(SR.GetString("Empty_attribute", new object[]
					{
						"lockElements"
					}), value.SourceInfo.FileName, value.SourceInfo.LineNumber);
				}
				if (lockType == ConfigurationLockCollectionType.LockedExceptionList)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_empty_lock_attributes_except", new object[]
					{
						"lockAllAttributesExcept",
						"lockAttributes"
					}), value.SourceInfo.FileName, value.SourceInfo.LineNumber);
				}
				if (lockType == ConfigurationLockCollectionType.LockedElementsExceptionList)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_empty_lock_element_except", new object[]
					{
						"lockAllElementsExcept",
						"lockElements"
					}), value.SourceInfo.FileName, value.SourceInfo.LineNumber);
				}
			}
			string[] array = text.Split(new char[]
			{
				',',
				':',
				';'
			});
			foreach (string text2 in array)
			{
				string text3 = text2.Trim();
				if (!string.IsNullOrEmpty(text3))
				{
					if ((lockType != ConfigurationLockCollectionType.LockedElements && lockType != ConfigurationLockCollectionType.LockedAttributes) || !(text3 == "*"))
					{
						ConfigurationProperty configurationProperty = this.Properties[text3];
						if (configurationProperty == null || text3 == "lockAttributes" || text3 == "lockAllAttributesExcept" || text3 == "lockElements" || (lockType != ConfigurationLockCollectionType.LockedElements && lockType != ConfigurationLockCollectionType.LockedElementsExceptionList && typeof(ConfigurationElement).IsAssignableFrom(configurationProperty.Type)) || ((lockType == ConfigurationLockCollectionType.LockedElements || lockType == ConfigurationLockCollectionType.LockedElementsExceptionList) && !typeof(ConfigurationElement).IsAssignableFrom(configurationProperty.Type)))
						{
							ConfigurationElementCollection configurationElementCollection = this as ConfigurationElementCollection;
							if (configurationElementCollection == null && this.Properties.DefaultCollectionProperty != null)
							{
								configurationElementCollection = (this[this.Properties.DefaultCollectionProperty] as ConfigurationElementCollection);
							}
							if (configurationElementCollection == null || lockType == ConfigurationLockCollectionType.LockedAttributes || lockType == ConfigurationLockCollectionType.LockedExceptionList)
							{
								this.ReportInvalidLock(text3, lockType, value, null);
							}
							else if (!configurationElementCollection.IsLockableElement(text3))
							{
								this.ReportInvalidLock(text3, lockType, value, configurationElementCollection.LockableElements);
							}
						}
						if (configurationProperty != null && configurationProperty.IsRequired)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_lock_attempt", new object[]
							{
								configurationProperty.Name
							}));
						}
					}
					configurationLockCollection.Add(text3, ConfigurationValueFlags.Default);
				}
			}
			return configurationLockCollection;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C118 File Offset: 0x0000A318
		private StringCollection IntersectLockCollections(ConfigurationLockCollection Collection1, ConfigurationLockCollection Collection2)
		{
			ConfigurationLockCollection configurationLockCollection = (Collection1.Count < Collection2.Count) ? Collection1 : Collection2;
			ConfigurationLockCollection configurationLockCollection2 = (Collection1.Count >= Collection2.Count) ? Collection1 : Collection2;
			StringCollection stringCollection = new StringCollection();
			foreach (object obj in configurationLockCollection)
			{
				string text = (string)obj;
				if (configurationLockCollection2.Contains(text) || text == this.ElementTagName)
				{
					stringCollection.Add(text);
				}
			}
			return stringCollection;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			ConfigurationPropertyCollection properties = this.Properties;
			ConfigurationValue configurationValue = null;
			ConfigurationValue configurationValue2 = null;
			ConfigurationValue configurationValue3 = null;
			ConfigurationValue configurationValue4 = null;
			bool flag = false;
			this._bElementPresent = true;
			ConfigurationElement configurationElement = null;
			ConfigurationProperty configurationProperty = (properties != null) ? properties.DefaultCollectionProperty : null;
			if (configurationProperty != null)
			{
				configurationElement = (ConfigurationElement)this[configurationProperty];
			}
			this._elementTagName = reader.Name;
			PropertySourceInfo sourceInfo = new PropertySourceInfo(reader);
			this._values.SetValue(reader.Name, null, ConfigurationValueFlags.Modified, sourceInfo);
			this._values.SetValue("", configurationElement, ConfigurationValueFlags.Modified, sourceInfo);
			if ((this._lockedElementsList != null && (this._lockedElementsList.Contains(reader.Name) || (this._lockedElementsList.Contains("*") && reader.Name != this.ElementTagName))) || (this._lockedAllExceptElementsList != null && this._lockedAllExceptElementsList.Count != 0 && !this._lockedAllExceptElementsList.Contains(reader.Name)) || ((this._fItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default && (this._fItemLocked & ConfigurationValueFlags.Inherited) != ConfigurationValueFlags.Default))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_element_locked", new object[]
				{
					reader.Name
				}), reader);
			}
			if (reader.AttributeCount > 0)
			{
				while (reader.MoveToNextAttribute())
				{
					string name = reader.Name;
					if (((this._lockedAttributesList != null && (this._lockedAttributesList.Contains(name) || this._lockedAttributesList.Contains("*"))) || (this._lockedAllExceptAttributesList != null && !this._lockedAllExceptAttributesList.Contains(name))) && name != "lockAttributes" && name != "lockAllAttributesExcept")
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
						{
							name
						}), reader);
					}
					ConfigurationProperty configurationProperty2 = (properties != null) ? properties[name] : null;
					if (configurationProperty2 != null)
					{
						if (serializeCollectionKey && !configurationProperty2.IsKey)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
							{
								name
							}), reader);
						}
						this._values.SetValue(name, this.DeserializePropertyValue(configurationProperty2, reader), ConfigurationValueFlags.Modified, new PropertySourceInfo(reader));
					}
					else
					{
						if (name == "lockItem")
						{
							try
							{
								flag = bool.Parse(reader.Value);
								continue;
							}
							catch
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_invalid_boolean_attribute", new object[]
								{
									name
								}), reader);
							}
						}
						if (name == "lockAttributes")
						{
							configurationValue = new ConfigurationValue(reader.Value, ConfigurationValueFlags.Default, new PropertySourceInfo(reader));
						}
						else if (name == "lockAllAttributesExcept")
						{
							configurationValue2 = new ConfigurationValue(reader.Value, ConfigurationValueFlags.Default, new PropertySourceInfo(reader));
						}
						else if (name == "lockElements")
						{
							configurationValue3 = new ConfigurationValue(reader.Value, ConfigurationValueFlags.Default, new PropertySourceInfo(reader));
						}
						else if (name == "lockAllElementsExcept")
						{
							configurationValue4 = new ConfigurationValue(reader.Value, ConfigurationValueFlags.Default, new PropertySourceInfo(reader));
						}
						else if (serializeCollectionKey || !this.OnDeserializeUnrecognizedAttribute(name, reader.Value))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
							{
								name
							}), reader);
						}
					}
				}
			}
			reader.MoveToElement();
			try
			{
				HybridDictionary hybridDictionary = new HybridDictionary();
				if (!reader.IsEmptyElement)
				{
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							string name2 = reader.Name;
							this.CheckLockedElement(name2, null);
							ConfigurationProperty configurationProperty3 = (properties != null) ? properties[name2] : null;
							if (configurationProperty3 != null)
							{
								if (!configurationProperty3.IsConfigurationElementType)
								{
									throw new ConfigurationErrorsException(SR.GetString("Config_base_property_is_not_a_configuration_element", new object[]
									{
										name2
									}), reader);
								}
								if (hybridDictionary.Contains(name2))
								{
									throw new ConfigurationErrorsException(SR.GetString("Config_base_element_cannot_have_multiple_child_elements", new object[]
									{
										name2
									}), reader);
								}
								hybridDictionary.Add(name2, name2);
								ConfigurationElement configurationElement2 = (ConfigurationElement)this[configurationProperty3];
								configurationElement2.DeserializeElement(reader, serializeCollectionKey);
								ConfigurationElement.ValidateElement(configurationElement2, configurationProperty3.Validator, false);
							}
							else if (!this.OnDeserializeUnrecognizedElement(name2, reader) && (configurationElement == null || !configurationElement.OnDeserializeUnrecognizedElement(name2, reader)))
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element_name", new object[]
								{
									name2
								}), reader);
							}
						}
						else
						{
							if (reader.NodeType == XmlNodeType.EndElement)
							{
								break;
							}
							if (reader.NodeType == XmlNodeType.CDATA || reader.NodeType == XmlNodeType.Text)
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_base_section_invalid_content"), reader);
							}
						}
					}
				}
				this.EnsureRequiredProperties(serializeCollectionKey);
				ConfigurationElement.ValidateElement(this, null, false);
			}
			catch (ConfigurationException ex)
			{
				if (ex.Filename == null || ex.Filename.Length == 0)
				{
					throw new ConfigurationErrorsException(ex.Message, reader);
				}
				throw ex;
			}
			if (flag)
			{
				this.SetLocked();
				this._fItemLocked = ConfigurationValueFlags.Locked;
			}
			if (configurationValue != null)
			{
				if (this._lockedAttributesList == null)
				{
					this._lockedAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedAttributes);
				}
				foreach (object obj in this.ParseLockedAttributes(configurationValue, ConfigurationLockCollectionType.LockedAttributes))
				{
					string name3 = (string)obj;
					if (!this._lockedAttributesList.Contains(name3))
					{
						this._lockedAttributesList.Add(name3, ConfigurationValueFlags.Default);
					}
					else
					{
						this._lockedAttributesList.Add(name3, ConfigurationValueFlags.Inherited | ConfigurationValueFlags.Modified);
					}
				}
			}
			if (configurationValue2 != null)
			{
				ConfigurationLockCollection configurationLockCollection = this.ParseLockedAttributes(configurationValue2, ConfigurationLockCollectionType.LockedExceptionList);
				if (this._lockedAllExceptAttributesList == null)
				{
					this._lockedAllExceptAttributesList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedExceptionList, string.Empty, configurationLockCollection);
					this._lockedAllExceptAttributesList.ClearSeedList();
				}
				StringCollection stringCollection = this.IntersectLockCollections(this._lockedAllExceptAttributesList, configurationLockCollection);
				this._lockedAllExceptAttributesList.ClearInternal(false);
				foreach (string name4 in stringCollection)
				{
					this._lockedAllExceptAttributesList.Add(name4, ConfigurationValueFlags.Default);
				}
			}
			if (configurationValue3 != null)
			{
				if (this._lockedElementsList == null)
				{
					this._lockedElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElements);
				}
				ConfigurationLockCollection configurationLockCollection2 = this.ParseLockedAttributes(configurationValue3, ConfigurationLockCollectionType.LockedElements);
				if (properties.DefaultCollectionProperty != null)
				{
					ConfigurationElementCollection configurationElementCollection = this[properties.DefaultCollectionProperty] as ConfigurationElementCollection;
					if (configurationElementCollection != null && configurationElementCollection._lockedElementsList == null)
					{
						configurationElementCollection._lockedElementsList = this._lockedElementsList;
					}
				}
				foreach (object obj2 in configurationLockCollection2)
				{
					string text = (string)obj2;
					if (!this._lockedElementsList.Contains(text))
					{
						this._lockedElementsList.Add(text, ConfigurationValueFlags.Default);
						ConfigurationProperty configurationProperty4 = this.Properties[text];
						if (configurationProperty4 != null && typeof(ConfigurationElement).IsAssignableFrom(configurationProperty4.Type))
						{
							((ConfigurationElement)this[text]).SetLocked();
						}
						if (text == "*")
						{
							foreach (object obj3 in this.Properties)
							{
								ConfigurationProperty configurationProperty5 = (ConfigurationProperty)obj3;
								if (!string.IsNullOrEmpty(configurationProperty5.Name) && configurationProperty5.IsConfigurationElementType)
								{
									((ConfigurationElement)this[configurationProperty5]).SetLocked();
								}
							}
						}
					}
				}
			}
			if (configurationValue4 != null)
			{
				ConfigurationLockCollection configurationLockCollection3 = this.ParseLockedAttributes(configurationValue4, ConfigurationLockCollectionType.LockedElementsExceptionList);
				if (this._lockedAllExceptElementsList == null)
				{
					this._lockedAllExceptElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElementsExceptionList, this._elementTagName, configurationLockCollection3);
					this._lockedAllExceptElementsList.ClearSeedList();
				}
				StringCollection stringCollection2 = this.IntersectLockCollections(this._lockedAllExceptElementsList, configurationLockCollection3);
				if (properties.DefaultCollectionProperty != null)
				{
					ConfigurationElementCollection configurationElementCollection2 = this[properties.DefaultCollectionProperty] as ConfigurationElementCollection;
					if (configurationElementCollection2 != null && configurationElementCollection2._lockedAllExceptElementsList == null)
					{
						configurationElementCollection2._lockedAllExceptElementsList = this._lockedAllExceptElementsList;
					}
				}
				this._lockedAllExceptElementsList.ClearInternal(false);
				foreach (string text2 in stringCollection2)
				{
					if (!this._lockedAllExceptElementsList.Contains(text2) || text2 == this.ElementTagName)
					{
						this._lockedAllExceptElementsList.Add(text2, ConfigurationValueFlags.Default);
					}
				}
				foreach (object obj4 in this.Properties)
				{
					ConfigurationProperty configurationProperty6 = (ConfigurationProperty)obj4;
					if (!string.IsNullOrEmpty(configurationProperty6.Name) && !this._lockedAllExceptElementsList.Contains(configurationProperty6.Name) && configurationProperty6.IsConfigurationElementType)
					{
						((ConfigurationElement)this[configurationProperty6]).SetLocked();
					}
				}
			}
			if (configurationProperty != null)
			{
				configurationElement = (ConfigurationElement)this[configurationProperty];
				if (this._lockedElementsList == null)
				{
					this._lockedElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElements);
				}
				configurationElement._lockedElementsList = this._lockedElementsList;
				if (this._lockedAllExceptElementsList == null)
				{
					this._lockedAllExceptElementsList = new ConfigurationLockCollection(this, ConfigurationLockCollectionType.LockedElementsExceptionList, reader.Name);
					this._lockedAllExceptElementsList.ClearSeedList();
				}
				configurationElement._lockedAllExceptElementsList = this._lockedAllExceptElementsList;
			}
			this.PostDeserialize();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000CB88 File Offset: 0x0000AD88
		private object DeserializePropertyValue(ConfigurationProperty prop, XmlReader reader)
		{
			string value = reader.Value;
			object obj = null;
			try
			{
				obj = prop.ConvertFromString(value);
				prop.Validate(obj);
			}
			catch (ConfigurationException ex)
			{
				if (string.IsNullOrEmpty(ex.Filename))
				{
					ex = new ConfigurationErrorsException(ex.Message, reader);
				}
				obj = new InvalidPropValue(value, ex);
			}
			catch
			{
			}
			return obj;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		internal static void ValidateElement(ConfigurationElement elem, ConfigurationValidatorBase propValidator, bool recursive)
		{
			ConfigurationValidatorBase configurationValidatorBase = propValidator;
			if (configurationValidatorBase == null && elem.ElementProperty != null)
			{
				configurationValidatorBase = elem.ElementProperty.Validator;
				if (configurationValidatorBase != null && !configurationValidatorBase.CanValidate(elem.GetType()))
				{
					throw new ConfigurationErrorsException(SR.GetString("Validator_does_not_support_elem_type", new object[]
					{
						elem.GetType().Name
					}));
				}
			}
			try
			{
				if (configurationValidatorBase != null)
				{
					configurationValidatorBase.Validate(elem);
				}
			}
			catch (ConfigurationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Validator_element_not_valid", new object[]
				{
					elem._elementTagName,
					ex.Message
				}));
			}
			if (recursive)
			{
				if (elem is ConfigurationElementCollection && elem is ConfigurationElementCollection)
				{
					IEnumerator elementsEnumerator = ((ConfigurationElementCollection)elem).GetElementsEnumerator();
					while (elementsEnumerator.MoveNext())
					{
						object obj = elementsEnumerator.Current;
						ConfigurationElement.ValidateElement((ConfigurationElement)obj, null, true);
					}
				}
				for (int i = 0; i < elem.Values.Count; i++)
				{
					ConfigurationElement configurationElement = elem.Values[i] as ConfigurationElement;
					if (configurationElement != null)
					{
						ConfigurationElement.ValidateElement(configurationElement, null, true);
					}
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000CD18 File Offset: 0x0000AF18
		private void EnsureRequiredProperties(bool ensureKeysOnly)
		{
			ConfigurationPropertyCollection properties = this.Properties;
			if (properties != null)
			{
				foreach (object obj in properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					if (configurationProperty.IsRequired && !this._values.Contains(configurationProperty.Name) && (!ensureKeysOnly || configurationProperty.IsKey))
					{
						this._values[configurationProperty.Name] = this.OnRequiredPropertyNotFound(configurationProperty.Name);
					}
				}
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		protected virtual object OnRequiredPropertyNotFound(string name)
		{
			throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_missing", new object[]
			{
				name
			}), this.PropertyFileName(name), this.PropertyLineNumber(name));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005E74 File Offset: 0x00004074
		protected virtual void PostDeserialize()
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E74 File Offset: 0x00004074
		protected virtual void PreSerialize(XmlWriter writer)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008751 File Offset: 0x00006951
		protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008751 File Offset: 0x00006951
		protected virtual bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return false;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000CDDD File Offset: 0x0000AFDD
		protected virtual string GetTransformedTypeString(string typeName)
		{
			if (typeName == null || this._configRecord == null || !this._configRecord.TypeStringTransformerIsSet)
			{
				return typeName;
			}
			return this._configRecord.TypeStringTransformer(typeName);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000CE0A File Offset: 0x0000B00A
		protected virtual string GetTransformedAssemblyString(string assemblyName)
		{
			if (assemblyName == null || this._configRecord == null || !this._configRecord.AssemblyStringTransformerIsSet)
			{
				return assemblyName;
			}
			return this._configRecord.AssemblyStringTransformer(assemblyName);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000CE37 File Offset: 0x0000B037
		public ElementInformation ElementInformation
		{
			get
			{
				if (this._evaluationElement == null)
				{
					this._evaluationElement = new ElementInformation(this);
				}
				return this._evaluationElement;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000CE59 File Offset: 0x0000B059
		protected ContextInformation EvaluationContext
		{
			get
			{
				if (this._evalContext == null)
				{
					if (this._configRecord == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_element_no_context"));
					}
					this._evalContext = new ContextInformation(this._configRecord);
				}
				return this._evalContext;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000CE92 File Offset: 0x0000B092
		protected internal virtual ConfigurationElementProperty ElementProperty
		{
			get
			{
				return this._elementProperty;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000CE9C File Offset: 0x0000B09C
		internal ConfigurationLockCollection UnMergeLockList(ConfigurationLockCollection sourceLockList, ConfigurationLockCollection parentLockList, ConfigurationSaveMode saveMode)
		{
			if (!sourceLockList.ExceptionList)
			{
				if (saveMode == ConfigurationSaveMode.Modified)
				{
					ConfigurationLockCollection configurationLockCollection = new ConfigurationLockCollection(this, sourceLockList.LockType);
					foreach (object obj in sourceLockList)
					{
						string name = (string)obj;
						if (!parentLockList.Contains(name) || sourceLockList.IsValueModified(name))
						{
							configurationLockCollection.Add(name, ConfigurationValueFlags.Default);
						}
					}
					return configurationLockCollection;
				}
				if (saveMode == ConfigurationSaveMode.Minimal)
				{
					ConfigurationLockCollection configurationLockCollection2 = new ConfigurationLockCollection(this, sourceLockList.LockType);
					foreach (object obj2 in sourceLockList)
					{
						string name2 = (string)obj2;
						if (!parentLockList.Contains(name2))
						{
							configurationLockCollection2.Add(name2, ConfigurationValueFlags.Default);
						}
					}
					return configurationLockCollection2;
				}
			}
			else if (saveMode == ConfigurationSaveMode.Modified || saveMode == ConfigurationSaveMode.Minimal)
			{
				bool flag = false;
				if (sourceLockList.Count == parentLockList.Count)
				{
					flag = true;
					foreach (object obj3 in sourceLockList)
					{
						string name3 = (string)obj3;
						if (!parentLockList.Contains(name3) || (sourceLockList.IsValueModified(name3) && saveMode == ConfigurationSaveMode.Modified))
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					return null;
				}
			}
			return sourceLockList;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000D00C File Offset: 0x0000B20C
		internal static bool IsLockAttributeName(string name)
		{
			if (!StringUtil.StartsWith(name, "lock"))
			{
				return false;
			}
			foreach (string b in ConfigurationElement.s_lockAttributeNames)
			{
				if (name == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000D04C File Offset: 0x0000B24C
		protected bool HasContext
		{
			get
			{
				return this._configRecord != null;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000D057 File Offset: 0x0000B257
		public Configuration CurrentConfiguration
		{
			get
			{
				if (this._configRecord != null)
				{
					return this._configRecord.CurrentConfiguration;
				}
				return null;
			}
		}

		// Token: 0x04000191 RID: 401
		private const string LockAttributesKey = "lockAttributes";

		// Token: 0x04000192 RID: 402
		private const string LockAllAttributesExceptKey = "lockAllAttributesExcept";

		// Token: 0x04000193 RID: 403
		private const string LockElementsKey = "lockElements";

		// Token: 0x04000194 RID: 404
		private const string LockAll = "*";

		// Token: 0x04000195 RID: 405
		private const string LockAllElementsExceptKey = "lockAllElementsExcept";

		// Token: 0x04000196 RID: 406
		private const string LockItemKey = "lockItem";

		// Token: 0x04000197 RID: 407
		internal const string DefaultCollectionPropertyName = "";

		// Token: 0x04000198 RID: 408
		private static string[] s_lockAttributeNames = new string[]
		{
			"lockAttributes",
			"lockAllAttributesExcept",
			"lockElements",
			"lockAllElementsExcept",
			"lockItem"
		};

		// Token: 0x04000199 RID: 409
		private static Hashtable s_propertyBags = new Hashtable();

		// Token: 0x0400019A RID: 410
		private static volatile Dictionary<Type, ConfigurationValidatorBase> s_perTypeValidators;

		// Token: 0x0400019B RID: 411
		internal static readonly object s_nullPropertyValue = new object();

		// Token: 0x0400019C RID: 412
		private static ConfigurationElementProperty s_ElementProperty = new ConfigurationElementProperty(new DefaultValidator());

		// Token: 0x0400019D RID: 413
		private bool _bDataToWrite;

		// Token: 0x0400019E RID: 414
		private bool _bModified;

		// Token: 0x0400019F RID: 415
		private bool _bReadOnly;

		// Token: 0x040001A0 RID: 416
		private bool _bElementPresent;

		// Token: 0x040001A1 RID: 417
		private bool _bInited;

		// Token: 0x040001A2 RID: 418
		internal ConfigurationLockCollection _lockedAttributesList;

		// Token: 0x040001A3 RID: 419
		internal ConfigurationLockCollection _lockedAllExceptAttributesList;

		// Token: 0x040001A4 RID: 420
		internal ConfigurationLockCollection _lockedElementsList;

		// Token: 0x040001A5 RID: 421
		internal ConfigurationLockCollection _lockedAllExceptElementsList;

		// Token: 0x040001A6 RID: 422
		private readonly ConfigurationValues _values;

		// Token: 0x040001A7 RID: 423
		private string _elementTagName;

		// Token: 0x040001A8 RID: 424
		private volatile ElementInformation _evaluationElement;

		// Token: 0x040001A9 RID: 425
		private ConfigurationElementProperty _elementProperty = ConfigurationElement.s_ElementProperty;

		// Token: 0x040001AA RID: 426
		internal ConfigurationValueFlags _fItemLocked;

		// Token: 0x040001AB RID: 427
		internal ContextInformation _evalContext;

		// Token: 0x040001AC RID: 428
		internal BaseConfigurationRecord _configRecord;
	}
}
