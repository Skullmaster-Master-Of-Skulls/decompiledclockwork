using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000041 RID: 65
	internal class ConfigurationValues : NameObjectCollectionBase
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0001214E File Offset: 0x0001034E
		internal ConfigurationValues() : base(StringComparer.Ordinal)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001215C File Offset: 0x0001035C
		internal void AssociateContext(BaseConfigurationRecord configRecord)
		{
			this._configRecord = configRecord;
			foreach (object obj in this.ConfigurationElements)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)obj;
				configurationElement.AssociateContext(this._configRecord);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000121C4 File Offset: 0x000103C4
		internal bool Contains(string key)
		{
			return base.BaseGet(key) != null;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011A85 File Offset: 0x0000FC85
		internal string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000121D0 File Offset: 0x000103D0
		internal ConfigurationValue GetConfigValue(string key)
		{
			return (ConfigurationValue)base.BaseGet(key);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000121DE File Offset: 0x000103DE
		internal ConfigurationValue GetConfigValue(int index)
		{
			return (ConfigurationValue)base.BaseGet(index);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000121EC File Offset: 0x000103EC
		internal PropertySourceInfo GetSourceInfo(string key)
		{
			ConfigurationValue configValue = this.GetConfigValue(key);
			if (configValue != null)
			{
				return configValue.SourceInfo;
			}
			return null;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001220C File Offset: 0x0001040C
		internal void ChangeSourceInfo(string key, PropertySourceInfo sourceInfo)
		{
			ConfigurationValue configValue = this.GetConfigValue(key);
			if (configValue != null)
			{
				configValue.SourceInfo = sourceInfo;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001222C File Offset: 0x0001042C
		private ConfigurationValue CreateConfigValue(object value, ConfigurationValueFlags valueFlags, PropertySourceInfo sourceInfo)
		{
			if (value != null)
			{
				if (value is ConfigurationElement)
				{
					this._containsElement = true;
					((ConfigurationElement)value).AssociateContext(this._configRecord);
				}
				else if (value is InvalidPropValue)
				{
					this._containsInvalidValue = true;
				}
			}
			return new ConfigurationValue(value, valueFlags, sourceInfo);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001227C File Offset: 0x0001047C
		internal void SetValue(string key, object value, ConfigurationValueFlags valueFlags, PropertySourceInfo sourceInfo)
		{
			ConfigurationValue value2 = this.CreateConfigValue(value, valueFlags, sourceInfo);
			base.BaseSet(key, value2);
		}

		// Token: 0x170000CA RID: 202
		internal object this[string key]
		{
			get
			{
				ConfigurationValue configValue = this.GetConfigValue(key);
				if (configValue != null)
				{
					return configValue.Value;
				}
				return null;
			}
			set
			{
				this.SetValue(key, value, ConfigurationValueFlags.Modified, null);
			}
		}

		// Token: 0x170000CB RID: 203
		internal object this[int index]
		{
			get
			{
				ConfigurationValue configValue = this.GetConfigValue(index);
				if (configValue != null)
				{
					return configValue.Value;
				}
				return null;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000122E8 File Offset: 0x000104E8
		internal void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000101B8 File Offset: 0x0000E3B8
		internal object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000122F0 File Offset: 0x000104F0
		internal ConfigurationValueFlags RetrieveFlags(string key)
		{
			ConfigurationValue configurationValue = (ConfigurationValue)base.BaseGet(key);
			if (configurationValue != null)
			{
				return configurationValue.ValueFlags;
			}
			return ConfigurationValueFlags.Default;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00012318 File Offset: 0x00010518
		internal bool IsModified(string key)
		{
			ConfigurationValue configurationValue = (ConfigurationValue)base.BaseGet(key);
			return configurationValue != null && (configurationValue.ValueFlags & ConfigurationValueFlags.Modified) > ConfigurationValueFlags.Default;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00012344 File Offset: 0x00010544
		internal bool IsInherited(string key)
		{
			ConfigurationValue configurationValue = (ConfigurationValue)base.BaseGet(key);
			return configurationValue != null && (configurationValue.ValueFlags & ConfigurationValueFlags.Inherited) > ConfigurationValueFlags.Default;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0001236E File Offset: 0x0001056E
		internal IEnumerable ConfigurationElements
		{
			get
			{
				if (this._containsElement)
				{
					return new ConfigurationValues.ConfigurationElementsCollection(this);
				}
				return ConfigurationValues.EmptyCollectionInstance;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00012386 File Offset: 0x00010586
		internal IEnumerable InvalidValues
		{
			get
			{
				if (this._containsInvalidValue)
				{
					return new ConfigurationValues.InvalidValuesCollection(this);
				}
				return ConfigurationValues.EmptyCollectionInstance;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0001239E File Offset: 0x0001059E
		private static IEnumerable EmptyCollectionInstance
		{
			get
			{
				if (ConfigurationValues.s_emptyCollection == null)
				{
					ConfigurationValues.s_emptyCollection = new ConfigurationValues.EmptyCollection();
				}
				return ConfigurationValues.s_emptyCollection;
			}
		}

		// Token: 0x04000226 RID: 550
		private BaseConfigurationRecord _configRecord;

		// Token: 0x04000227 RID: 551
		private volatile bool _containsElement;

		// Token: 0x04000228 RID: 552
		private volatile bool _containsInvalidValue;

		// Token: 0x04000229 RID: 553
		private static volatile IEnumerable s_emptyCollection;

		// Token: 0x020000D1 RID: 209
		private class EmptyCollection : IEnumerable
		{
			// Token: 0x060007F1 RID: 2033 RVA: 0x00020C5D File Offset: 0x0001EE5D
			internal EmptyCollection()
			{
				this._emptyEnumerator = new ConfigurationValues.EmptyCollection.EmptyCollectionEnumerator();
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x00020C70 File Offset: 0x0001EE70
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._emptyEnumerator;
			}

			// Token: 0x040004A0 RID: 1184
			private IEnumerator _emptyEnumerator;

			// Token: 0x020000DD RID: 221
			private class EmptyCollectionEnumerator : IEnumerator
			{
				// Token: 0x06000807 RID: 2055 RVA: 0x00008751 File Offset: 0x00006951
				bool IEnumerator.MoveNext()
				{
					return false;
				}

				// Token: 0x17000252 RID: 594
				// (get) Token: 0x06000808 RID: 2056 RVA: 0x000088C2 File Offset: 0x00006AC2
				object IEnumerator.Current
				{
					get
					{
						return null;
					}
				}

				// Token: 0x06000809 RID: 2057 RVA: 0x00005E74 File Offset: 0x00004074
				void IEnumerator.Reset()
				{
				}
			}
		}

		// Token: 0x020000D2 RID: 210
		private class ConfigurationElementsCollection : IEnumerable
		{
			// Token: 0x060007F3 RID: 2035 RVA: 0x00020C78 File Offset: 0x0001EE78
			internal ConfigurationElementsCollection(ConfigurationValues values)
			{
				this._values = values;
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x00020C87 File Offset: 0x0001EE87
			IEnumerator IEnumerable.GetEnumerator()
			{
				if (this._values._containsElement)
				{
					int num;
					for (int index = 0; index < this._values.Count; index = num + 1)
					{
						object obj = this._values[index];
						if (obj is ConfigurationElement)
						{
							yield return obj;
						}
						num = index;
					}
				}
				yield break;
			}

			// Token: 0x040004A1 RID: 1185
			private ConfigurationValues _values;
		}

		// Token: 0x020000D3 RID: 211
		private class InvalidValuesCollection : IEnumerable
		{
			// Token: 0x060007F5 RID: 2037 RVA: 0x00020C96 File Offset: 0x0001EE96
			internal InvalidValuesCollection(ConfigurationValues values)
			{
				this._values = values;
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x00020CA5 File Offset: 0x0001EEA5
			IEnumerator IEnumerable.GetEnumerator()
			{
				if (this._values._containsInvalidValue)
				{
					int num;
					for (int index = 0; index < this._values.Count; index = num + 1)
					{
						object obj = this._values[index];
						if (obj is InvalidPropValue)
						{
							yield return obj;
						}
						num = index;
					}
				}
				yield break;
			}

			// Token: 0x040004A2 RID: 1186
			private ConfigurationValues _values;
		}
	}
}
