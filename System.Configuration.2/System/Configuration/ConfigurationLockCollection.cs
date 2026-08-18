using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace System.Configuration
{
	// Token: 0x0200002C RID: 44
	public sealed class ConfigurationLockCollection : IEnumerable, ICollection
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		internal ConfigurationLockCollection(ConfigurationElement thisElement) : this(thisElement, ConfigurationLockCollectionType.LockedAttributes)
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000FA56 File Offset: 0x0000DC56
		internal ConfigurationLockCollection(ConfigurationElement thisElement, ConfigurationLockCollectionType lockType) : this(thisElement, lockType, string.Empty)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000FA65 File Offset: 0x0000DC65
		internal ConfigurationLockCollection(ConfigurationElement thisElement, ConfigurationLockCollectionType lockType, string ignoreName) : this(thisElement, lockType, ignoreName, null)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000FA74 File Offset: 0x0000DC74
		internal ConfigurationLockCollection(ConfigurationElement thisElement, ConfigurationLockCollectionType lockType, string ignoreName, ConfigurationLockCollection parentCollection)
		{
			this._thisElement = thisElement;
			this._lockType = lockType;
			this.internalDictionary = new HybridDictionary();
			this.internalArraylist = new ArrayList();
			this._bModified = false;
			this._bExceptionList = (this._lockType == ConfigurationLockCollectionType.LockedExceptionList || this._lockType == ConfigurationLockCollectionType.LockedElementsExceptionList);
			this._ignoreName = ignoreName;
			if (parentCollection != null)
			{
				foreach (object obj in parentCollection)
				{
					string text = (string)obj;
					this.Add(text, ConfigurationValueFlags.Inherited);
					if (this._bExceptionList)
					{
						if (this.SeedList.Length != 0)
						{
							this.SeedList += ",";
						}
						this.SeedList += text;
					}
				}
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000FB74 File Offset: 0x0000DD74
		internal void ClearSeedList()
		{
			this.SeedList = string.Empty;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000FB81 File Offset: 0x0000DD81
		internal ConfigurationLockCollectionType LockType
		{
			get
			{
				return this._lockType;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000FB8C File Offset: 0x0000DD8C
		public void Add(string name)
		{
			if ((this._thisElement.ItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default && (this._thisElement.ItemLocked & ConfigurationValueFlags.Inherited) != ConfigurationValueFlags.Default)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
				{
					name
				}));
			}
			ConfigurationValueFlags configurationValueFlags = ConfigurationValueFlags.Modified;
			string text = name.Trim();
			ConfigurationProperty configurationProperty = this._thisElement.Properties[text];
			if (configurationProperty == null && text != "*")
			{
				ConfigurationElementCollection configurationElementCollection = this._thisElement as ConfigurationElementCollection;
				if (configurationElementCollection == null && this._thisElement.Properties.DefaultCollectionProperty != null)
				{
					configurationElementCollection = (this._thisElement[this._thisElement.Properties.DefaultCollectionProperty] as ConfigurationElementCollection);
				}
				if (configurationElementCollection == null || this._lockType == ConfigurationLockCollectionType.LockedAttributes || this._lockType == ConfigurationLockCollectionType.LockedExceptionList)
				{
					this._thisElement.ReportInvalidLock(text, this._lockType, null, null);
				}
				else if (!configurationElementCollection.IsLockableElement(text))
				{
					this._thisElement.ReportInvalidLock(text, this._lockType, null, configurationElementCollection.LockableElements);
				}
			}
			else
			{
				if (configurationProperty != null && configurationProperty.IsRequired)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_lock_attempt", new object[]
					{
						configurationProperty.Name
					}));
				}
				if (text != "*")
				{
					if (this._lockType == ConfigurationLockCollectionType.LockedElements || this._lockType == ConfigurationLockCollectionType.LockedElementsExceptionList)
					{
						if (!typeof(ConfigurationElement).IsAssignableFrom(configurationProperty.Type))
						{
							this._thisElement.ReportInvalidLock(text, this._lockType, null, null);
						}
					}
					else if (typeof(ConfigurationElement).IsAssignableFrom(configurationProperty.Type))
					{
						this._thisElement.ReportInvalidLock(text, this._lockType, null, null);
					}
				}
			}
			if (this.internalDictionary.Contains(name))
			{
				configurationValueFlags = (ConfigurationValueFlags.Modified | (ConfigurationValueFlags)this.internalDictionary[name]);
				this.internalDictionary.Remove(name);
				this.internalArraylist.Remove(name);
			}
			this.internalDictionary.Add(name, configurationValueFlags);
			this.internalArraylist.Add(name);
			this._bModified = true;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		internal void Add(string name, ConfigurationValueFlags flags)
		{
			if (flags != ConfigurationValueFlags.Inherited && this.internalDictionary.Contains(name))
			{
				flags = (ConfigurationValueFlags.Modified | (ConfigurationValueFlags)this.internalDictionary[name]);
				this.internalDictionary.Remove(name);
				this.internalArraylist.Remove(name);
			}
			this.internalDictionary.Add(name, flags);
			this.internalArraylist.Add(name);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000FE0C File Offset: 0x0000E00C
		internal bool DefinedInParent(string name)
		{
			if (name == null)
			{
				return false;
			}
			if (this._bExceptionList)
			{
				string text = "," + this.SeedList + ",";
				if (name.Equals(this._ignoreName) || text.IndexOf("," + name + ",", StringComparison.Ordinal) >= 0)
				{
					return true;
				}
			}
			return this.internalDictionary.Contains(name) && ((ConfigurationValueFlags)this.internalDictionary[name] & ConfigurationValueFlags.Inherited) > ConfigurationValueFlags.Default;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000FE8C File Offset: 0x0000E08C
		internal bool IsValueModified(string name)
		{
			return this.internalDictionary.Contains(name) && ((ConfigurationValueFlags)this.internalDictionary[name] & ConfigurationValueFlags.Modified) > ConfigurationValueFlags.Default;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		internal void RemoveInheritedLocks()
		{
			StringCollection stringCollection = new StringCollection();
			foreach (object obj in this)
			{
				string text = (string)obj;
				if (this.DefinedInParent(text))
				{
					stringCollection.Add(text);
				}
			}
			foreach (string text2 in stringCollection)
			{
				this.internalDictionary.Remove(text2);
				this.internalArraylist.Remove(text2);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000FF70 File Offset: 0x0000E170
		public void Remove(string name)
		{
			if (!this.internalDictionary.Contains(name))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_not_found", new object[]
				{
					name
				}));
			}
			if (this._bExceptionList || ((ConfigurationValueFlags)this.internalDictionary[name] & ConfigurationValueFlags.Inherited) == ConfigurationValueFlags.Default)
			{
				this.internalDictionary.Remove(name);
				this.internalArraylist.Remove(name);
				this._bModified = true;
				return;
			}
			if (((ConfigurationValueFlags)this.internalDictionary[name] & ConfigurationValueFlags.Modified) != ConfigurationValueFlags.Default)
			{
				ConfigurationValueFlags configurationValueFlags = (ConfigurationValueFlags)this.internalDictionary[name];
				configurationValueFlags &= ~ConfigurationValueFlags.Modified;
				this.internalDictionary[name] = configurationValueFlags;
				this._bModified = true;
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
			{
				name
			}));
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00010041 File Offset: 0x0000E241
		public IEnumerator GetEnumerator()
		{
			return this.internalArraylist.GetEnumerator();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00010050 File Offset: 0x0000E250
		internal void ClearInternal(bool useSeedIfAvailble)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.internalDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (((ConfigurationValueFlags)dictionaryEntry.Value & ConfigurationValueFlags.Inherited) == ConfigurationValueFlags.Default || this._bExceptionList)
				{
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			foreach (object obj2 in arrayList)
			{
				this.internalDictionary.Remove(obj2);
				this.internalArraylist.Remove(obj2);
			}
			if (useSeedIfAvailble && !string.IsNullOrEmpty(this.SeedList))
			{
				string[] array = this.SeedList.Split(new char[]
				{
					','
				});
				foreach (string name in array)
				{
					this.Add(name, ConfigurationValueFlags.Inherited);
				}
			}
			this._bModified = true;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0001017C File Offset: 0x0000E37C
		public void Clear()
		{
			this.ClearInternal(true);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00010185 File Offset: 0x0000E385
		public bool Contains(string name)
		{
			return (this._bExceptionList && name.Equals(this._ignoreName)) || this.internalDictionary.Contains(name);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000101AB File Offset: 0x0000E3AB
		public int Count
		{
			get
			{
				return this.internalDictionary.Count;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00008751 File Offset: 0x00006951
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000101B8 File Offset: 0x0000E3B8
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public void CopyTo(string[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000101BB File Offset: 0x0000E3BB
		void ICollection.CopyTo(Array array, int index)
		{
			this.internalArraylist.CopyTo(array, index);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000101CA File Offset: 0x0000E3CA
		public bool IsModified
		{
			get
			{
				return this._bModified;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000101D2 File Offset: 0x0000E3D2
		internal void ResetModified()
		{
			this._bModified = false;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000101DB File Offset: 0x0000E3DB
		public bool IsReadOnly(string name)
		{
			if (!this.internalDictionary.Contains(name))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_not_found", new object[]
				{
					name
				}));
			}
			return ((ConfigurationValueFlags)this.internalDictionary[name] & ConfigurationValueFlags.Inherited) > ConfigurationValueFlags.Default;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0001021B File Offset: 0x0000E41B
		internal bool ExceptionList
		{
			get
			{
				return this._bExceptionList;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00010224 File Offset: 0x0000E424
		public string AttributeList
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.internalDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(dictionaryEntry.Key);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000102A4 File Offset: 0x0000E4A4
		public void SetFromList(string attributeList)
		{
			string[] array = attributeList.Split(new char[]
			{
				',',
				';',
				':'
			});
			this.Clear();
			foreach (string text in array)
			{
				string name = text.Trim();
				if (!this.Contains(name))
				{
					this.Add(name);
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00010300 File Offset: 0x0000E500
		public bool HasParentElements
		{
			get
			{
				bool result = false;
				if (this.ExceptionList && this.internalDictionary.Count == 0 && !string.IsNullOrEmpty(this.SeedList))
				{
					return true;
				}
				foreach (object obj in this.internalDictionary)
				{
					if (((ConfigurationValueFlags)((DictionaryEntry)obj).Value & ConfigurationValueFlags.Inherited) != ConfigurationValueFlags.Default)
					{
						result = true;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x040001CF RID: 463
		private HybridDictionary internalDictionary;

		// Token: 0x040001D0 RID: 464
		private ArrayList internalArraylist;

		// Token: 0x040001D1 RID: 465
		private bool _bModified;

		// Token: 0x040001D2 RID: 466
		private bool _bExceptionList;

		// Token: 0x040001D3 RID: 467
		private string _ignoreName = string.Empty;

		// Token: 0x040001D4 RID: 468
		private ConfigurationElement _thisElement;

		// Token: 0x040001D5 RID: 469
		private ConfigurationLockCollectionType _lockType;

		// Token: 0x040001D6 RID: 470
		private string SeedList = string.Empty;

		// Token: 0x040001D7 RID: 471
		private const string LockAll = "*";
	}
}
