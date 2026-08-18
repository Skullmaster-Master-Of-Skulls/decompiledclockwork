using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x020003B7 RID: 951
	[DesignerSerializer("System.Diagnostics.Design.StringDictionaryCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class StringDictionary : IEnumerable
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x000A8D9B File Offset: 0x000A6F9B
		public virtual int Count
		{
			get
			{
				return this.contents.Count;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x000A8DA8 File Offset: 0x000A6FA8
		public virtual bool IsSynchronized
		{
			get
			{
				return this.contents.IsSynchronized;
			}
		}

		// Token: 0x17000918 RID: 2328
		public virtual string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key.ToLower(CultureInfo.InvariantCulture)];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key.ToLower(CultureInfo.InvariantCulture)] = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x000A8E07 File Offset: 0x000A7007
		public virtual ICollection Keys
		{
			get
			{
				return this.contents.Keys;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000A8E14 File Offset: 0x000A7014
		public virtual object SyncRoot
		{
			get
			{
				return this.contents.SyncRoot;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x000A8E21 File Offset: 0x000A7021
		public virtual ICollection Values
		{
			get
			{
				return this.contents.Values;
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000A8E2E File Offset: 0x000A702E
		public virtual void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key.ToLower(CultureInfo.InvariantCulture), value);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000A8E55 File Offset: 0x000A7055
		public virtual void Clear()
		{
			this.contents.Clear();
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000A8E62 File Offset: 0x000A7062
		public virtual bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000A8E88 File Offset: 0x000A7088
		public virtual bool ContainsValue(string value)
		{
			return this.contents.ContainsValue(value);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000A8E96 File Offset: 0x000A7096
		public virtual void CopyTo(Array array, int index)
		{
			this.contents.CopyTo(array, index);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000A8EA5 File Offset: 0x000A70A5
		public virtual IEnumerator GetEnumerator()
		{
			return this.contents.GetEnumerator();
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000A8EB2 File Offset: 0x000A70B2
		public virtual void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000A8ED8 File Offset: 0x000A70D8
		internal void ReplaceHashtable(Hashtable useThisHashtableInstead)
		{
			this.contents = useThisHashtableInstead;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000A8EE1 File Offset: 0x000A70E1
		internal IDictionary<string, string> AsGenericDictionary()
		{
			return new StringDictionary.GenericAdapter(this);
		}

		// Token: 0x04001FF8 RID: 8184
		internal Hashtable contents = new Hashtable();

		// Token: 0x020007F0 RID: 2032
		private class GenericAdapter : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
		{
			// Token: 0x06004416 RID: 17430 RVA: 0x0011E446 File Offset: 0x0011C646
			internal GenericAdapter(StringDictionary stringDictionary)
			{
				this.m_stringDictionary = stringDictionary;
			}

			// Token: 0x06004417 RID: 17431 RVA: 0x0011E455 File Offset: 0x0011C655
			public void Add(string key, string value)
			{
				this[key] = value;
			}

			// Token: 0x06004418 RID: 17432 RVA: 0x0011E45F File Offset: 0x0011C65F
			public bool ContainsKey(string key)
			{
				return this.m_stringDictionary.ContainsKey(key);
			}

			// Token: 0x06004419 RID: 17433 RVA: 0x0011E46D File Offset: 0x0011C66D
			public void Clear()
			{
				this.m_stringDictionary.Clear();
			}

			// Token: 0x17000F70 RID: 3952
			// (get) Token: 0x0600441A RID: 17434 RVA: 0x0011E47A File Offset: 0x0011C67A
			public int Count
			{
				get
				{
					return this.m_stringDictionary.Count;
				}
			}

			// Token: 0x17000F71 RID: 3953
			public string this[string key]
			{
				get
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					if (!this.m_stringDictionary.ContainsKey(key))
					{
						throw new KeyNotFoundException();
					}
					return this.m_stringDictionary[key];
				}
				set
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					this.m_stringDictionary[key] = value;
				}
			}

			// Token: 0x17000F72 RID: 3954
			// (get) Token: 0x0600441D RID: 17437 RVA: 0x0011E4D4 File Offset: 0x0011C6D4
			public ICollection<string> Keys
			{
				get
				{
					if (this._keys == null)
					{
						this._keys = new StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, StringDictionary.GenericAdapter.KeyOrValue.Key);
					}
					return this._keys;
				}
			}

			// Token: 0x17000F73 RID: 3955
			// (get) Token: 0x0600441E RID: 17438 RVA: 0x0011E4F6 File Offset: 0x0011C6F6
			public ICollection<string> Values
			{
				get
				{
					if (this._values == null)
					{
						this._values = new StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, StringDictionary.GenericAdapter.KeyOrValue.Value);
					}
					return this._values;
				}
			}

			// Token: 0x0600441F RID: 17439 RVA: 0x0011E518 File Offset: 0x0011C718
			public bool Remove(string key)
			{
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					return false;
				}
				this.m_stringDictionary.Remove(key);
				return true;
			}

			// Token: 0x06004420 RID: 17440 RVA: 0x0011E537 File Offset: 0x0011C737
			public bool TryGetValue(string key, out string value)
			{
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					value = null;
					return false;
				}
				value = this.m_stringDictionary[key];
				return true;
			}

			// Token: 0x06004421 RID: 17441 RVA: 0x0011E55B File Offset: 0x0011C75B
			void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
			{
				this.m_stringDictionary.Add(item.Key, item.Value);
			}

			// Token: 0x06004422 RID: 17442 RVA: 0x0011E578 File Offset: 0x0011C778
			bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
			{
				string text;
				return this.TryGetValue(item.Key, out text) && text.Equals(item.Value);
			}

			// Token: 0x06004423 RID: 17443 RVA: 0x0011E5A8 File Offset: 0x0011C7A8
			void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array", SR.GetString("ArgumentNull_Array"));
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < this.Count)
				{
					throw new ArgumentException(SR.GetString("Arg_ArrayPlusOffTooSmall"));
				}
				int num = arrayIndex;
				foreach (object obj in this.m_stringDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					array[num++] = new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
			}

			// Token: 0x17000F74 RID: 3956
			// (get) Token: 0x06004424 RID: 17444 RVA: 0x0011E674 File Offset: 0x0011C874
			bool ICollection<KeyValuePair<string, string>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004425 RID: 17445 RVA: 0x0011E678 File Offset: 0x0011C878
			bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
			{
				if (!((ICollection<KeyValuePair<string, string>>)this).Contains(item))
				{
					return false;
				}
				this.m_stringDictionary.Remove(item.Key);
				return true;
			}

			// Token: 0x06004426 RID: 17446 RVA: 0x0011E6A5 File Offset: 0x0011C8A5
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06004427 RID: 17447 RVA: 0x0011E6AD File Offset: 0x0011C8AD
			public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
			{
				foreach (object obj in this.m_stringDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					yield return new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04003512 RID: 13586
			private StringDictionary m_stringDictionary;

			// Token: 0x04003513 RID: 13587
			private StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter _values;

			// Token: 0x04003514 RID: 13588
			private StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter _keys;

			// Token: 0x02000926 RID: 2342
			internal enum KeyOrValue
			{
				// Token: 0x04003DC3 RID: 15811
				Key,
				// Token: 0x04003DC4 RID: 15812
				Value
			}

			// Token: 0x02000927 RID: 2343
			private class ICollectionToGenericCollectionAdapter : ICollection<string>, IEnumerable<string>, IEnumerable
			{
				// Token: 0x06004683 RID: 18051 RVA: 0x00126AB7 File Offset: 0x00124CB7
				public ICollectionToGenericCollectionAdapter(StringDictionary source, StringDictionary.GenericAdapter.KeyOrValue keyOrValue)
				{
					if (source == null)
					{
						throw new ArgumentNullException("source");
					}
					this._internal = source;
					this._keyOrValue = keyOrValue;
				}

				// Token: 0x06004684 RID: 18052 RVA: 0x00126ADB File Offset: 0x00124CDB
				public void Add(string item)
				{
					this.ThrowNotSupportedException();
				}

				// Token: 0x06004685 RID: 18053 RVA: 0x00126AE3 File Offset: 0x00124CE3
				public void Clear()
				{
					this.ThrowNotSupportedException();
				}

				// Token: 0x06004686 RID: 18054 RVA: 0x00126AEB File Offset: 0x00124CEB
				public void ThrowNotSupportedException()
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						throw new NotSupportedException(SR.GetString("NotSupported_KeyCollectionSet"));
					}
					throw new NotSupportedException(SR.GetString("NotSupported_ValueCollectionSet"));
				}

				// Token: 0x06004687 RID: 18055 RVA: 0x00126B14 File Offset: 0x00124D14
				public bool Contains(string item)
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						return this._internal.ContainsKey(item);
					}
					return this._internal.ContainsValue(item);
				}

				// Token: 0x06004688 RID: 18056 RVA: 0x00126B38 File Offset: 0x00124D38
				public void CopyTo(string[] array, int arrayIndex)
				{
					ICollection underlyingCollection = this.GetUnderlyingCollection();
					underlyingCollection.CopyTo(array, arrayIndex);
				}

				// Token: 0x17000FE8 RID: 4072
				// (get) Token: 0x06004689 RID: 18057 RVA: 0x00126B54 File Offset: 0x00124D54
				public int Count
				{
					get
					{
						return this._internal.Count;
					}
				}

				// Token: 0x17000FE9 RID: 4073
				// (get) Token: 0x0600468A RID: 18058 RVA: 0x00126B61 File Offset: 0x00124D61
				public bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x0600468B RID: 18059 RVA: 0x00126B64 File Offset: 0x00124D64
				public bool Remove(string item)
				{
					this.ThrowNotSupportedException();
					return false;
				}

				// Token: 0x0600468C RID: 18060 RVA: 0x00126B6D File Offset: 0x00124D6D
				private ICollection GetUnderlyingCollection()
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						return this._internal.Keys;
					}
					return this._internal.Values;
				}

				// Token: 0x0600468D RID: 18061 RVA: 0x00126B8E File Offset: 0x00124D8E
				public IEnumerator<string> GetEnumerator()
				{
					ICollection underlyingCollection = this.GetUnderlyingCollection();
					foreach (object obj in underlyingCollection)
					{
						string text = (string)obj;
						yield return text;
					}
					IEnumerator enumerator = null;
					yield break;
					yield break;
				}

				// Token: 0x0600468E RID: 18062 RVA: 0x00126B9D File Offset: 0x00124D9D
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetUnderlyingCollection().GetEnumerator();
				}

				// Token: 0x04003DC5 RID: 15813
				private StringDictionary _internal;

				// Token: 0x04003DC6 RID: 15814
				private StringDictionary.GenericAdapter.KeyOrValue _keyOrValue;
			}
		}
	}
}
