using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000558 RID: 1368
	public class PersonalizationDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x0600458C RID: 17804 RVA: 0x000E572E File Offset: 0x000E392E
		public PersonalizationDictionary()
		{
			this._dictionary = new HybridDictionary(true);
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000E5742 File Offset: 0x000E3942
		public PersonalizationDictionary(int initialSize)
		{
			this._dictionary = new HybridDictionary(initialSize, true);
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x000E5757 File Offset: 0x000E3957
		public virtual int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x0600458F RID: 17807 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06004590 RID: 17808 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06004591 RID: 17809 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x000E5764 File Offset: 0x000E3964
		public virtual ICollection Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x00004335 File Offset: 0x00002535
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x000E5771 File Offset: 0x000E3971
		public virtual ICollection Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x17001488 RID: 5256
		public virtual PersonalizationEntry this[string key]
		{
			get
			{
				key = StringUtil.CheckAndTrimString(key, "key");
				return (PersonalizationEntry)this._dictionary[key];
			}
			set
			{
				key = StringUtil.CheckAndTrimString(key, "key");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._dictionary[key] = value;
			}
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x000E57C8 File Offset: 0x000E39C8
		public virtual void Add(string key, PersonalizationEntry value)
		{
			key = StringUtil.CheckAndTrimString(key, "key");
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._dictionary.Add(key, value);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x000E57F2 File Offset: 0x000E39F2
		public virtual void Clear()
		{
			this._dictionary.Clear();
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x000E57FF File Offset: 0x000E39FF
		public virtual bool Contains(string key)
		{
			key = StringUtil.CheckAndTrimString(key, "key");
			return this._dictionary.Contains(key);
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x000E581A File Offset: 0x000E3A1A
		public virtual void CopyTo(DictionaryEntry[] array, int index)
		{
			this._dictionary.CopyTo(array, index);
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x000E5829 File Offset: 0x000E3A29
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x000E5836 File Offset: 0x000E3A36
		public virtual void Remove(string key)
		{
			key = StringUtil.CheckAndTrimString(key, "key");
			this._dictionary.Remove(key);
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x000E5854 File Offset: 0x000E3A54
		internal void RemoveSharedProperties()
		{
			DictionaryEntry[] array = new DictionaryEntry[this.Count];
			this.CopyTo(array, 0);
			foreach (DictionaryEntry dictionaryEntry in array)
			{
				if (((PersonalizationEntry)dictionaryEntry.Value).Scope == PersonalizationScope.Shared)
				{
					this.Remove((string)dictionaryEntry.Key);
				}
			}
		}

		// Token: 0x17001489 RID: 5257
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is string))
				{
					throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypeString"), "key");
				}
				return this[(string)key];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!(key is string))
				{
					throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypeString"), "key");
				}
				if (!(value is PersonalizationEntry))
				{
					throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypePersonalizationEntry"), "value");
				}
				this[(string)key] = (PersonalizationEntry)value;
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x000E5948 File Offset: 0x000E3B48
		void IDictionary.Add(object key, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(key is string))
			{
				throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypeString"), "key");
			}
			if (!(value is PersonalizationEntry))
			{
				throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypePersonalizationEntry"), "value");
			}
			this.Add((string)key, (PersonalizationEntry)value);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x000E59AF File Offset: 0x000E3BAF
		bool IDictionary.Contains(object key)
		{
			if (!(key is string))
			{
				throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypeString"), "key");
			}
			return this.Contains((string)key);
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x000E59DA File Offset: 0x000E3BDA
		void IDictionary.Remove(object key)
		{
			if (!(key is string))
			{
				throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypeString"), "key");
			}
			this.Remove((string)key);
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x000E5A05 File Offset: 0x000E3C05
		void ICollection.CopyTo(Array array, int index)
		{
			if (!(array is DictionaryEntry[]))
			{
				throw new ArgumentException(SR.GetString("PersonalizationDictionary_MustBeTypeDictionaryEntryArray"), "array");
			}
			this.CopyTo((DictionaryEntry[])array, index);
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x000E5A31 File Offset: 0x000E3C31
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002673 RID: 9843
		private HybridDictionary _dictionary;
	}
}
