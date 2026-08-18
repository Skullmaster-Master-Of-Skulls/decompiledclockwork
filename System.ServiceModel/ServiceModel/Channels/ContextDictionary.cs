using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007AC RID: 1964
	[Serializable]
	internal class ContextDictionary : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06004A4C RID: 19020 RVA: 0x00111513 File Offset: 0x0010F713
		public ContextDictionary()
		{
			this.dictionaryStore = new Dictionary<string, string>();
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00111528 File Offset: 0x0010F728
		public ContextDictionary(IDictionary<string, string> context)
		{
			this.dictionaryStore = new Dictionary<string, string>();
			if (context != null)
			{
				bool flag = context is ContextDictionary;
				foreach (KeyValuePair<string, string> item in context)
				{
					if (flag)
					{
						this.dictionaryStore.Add(item);
					}
					else
					{
						this.Add(item);
					}
				}
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x001115A0 File Offset: 0x0010F7A0
		internal static ContextDictionary Empty
		{
			get
			{
				if (ContextDictionary.empty == null)
				{
					ContextDictionary.empty = new ContextDictionary
					{
						dictionaryStore = new ReadOnlyDictionaryInternal<string, string>(new Dictionary<string, string>(0))
					};
				}
				return ContextDictionary.empty;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x001115D6 File Offset: 0x0010F7D6
		public int Count
		{
			get
			{
				return this.dictionaryStore.Count;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x001115E3 File Offset: 0x0010F7E3
		public bool IsReadOnly
		{
			get
			{
				return this.dictionaryStore.IsReadOnly;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x001115F0 File Offset: 0x0010F7F0
		public ICollection<string> Keys
		{
			get
			{
				return this.dictionaryStore.Keys;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x001115FD File Offset: 0x0010F7FD
		public ICollection<string> Values
		{
			get
			{
				return this.dictionaryStore.Values;
			}
		}

		// Token: 0x170012B8 RID: 4792
		public string this[string key]
		{
			get
			{
				ContextDictionary.ValidateKeyValueSpace(key);
				return this.dictionaryStore[key];
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				ContextDictionary.ValidateKeyValueSpace(key);
				this.dictionaryStore[key] = value;
			}
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x00111646 File Offset: 0x0010F846
		public void Add(string key, string value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			ContextDictionary.ValidateKeyValueSpace(key);
			this.dictionaryStore.Add(key, value);
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x00111670 File Offset: 0x0010F870
		public void Add(KeyValuePair<string, string> item)
		{
			if (item.Key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item.Key");
			}
			if (item.Value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item.Value");
			}
			ContextDictionary.ValidateKeyValueSpace(item.Key);
			this.dictionaryStore.Add(item);
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x001116C7 File Offset: 0x0010F8C7
		public void Clear()
		{
			this.dictionaryStore.Clear();
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x001116D4 File Offset: 0x0010F8D4
		public bool Contains(KeyValuePair<string, string> item)
		{
			if (item.Key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item.Key");
			}
			if (item.Value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item.Value");
			}
			ContextDictionary.ValidateKeyValueSpace(item.Key);
			return this.dictionaryStore.Contains(item);
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x0011172B File Offset: 0x0010F92B
		public bool ContainsKey(string key)
		{
			ContextDictionary.ValidateKeyValueSpace(key);
			return this.dictionaryStore.ContainsKey(key);
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0011173F File Offset: 0x0010F93F
		public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			this.dictionaryStore.CopyTo(array, arrayIndex);
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0011174E File Offset: 0x0010F94E
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.dictionaryStore.GetEnumerator();
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x0011175B File Offset: 0x0010F95B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.dictionaryStore.GetEnumerator();
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x00111768 File Offset: 0x0010F968
		public bool Remove(string key)
		{
			ContextDictionary.ValidateKeyValueSpace(key);
			return this.dictionaryStore.Remove(key);
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x0011177C File Offset: 0x0010F97C
		public bool Remove(KeyValuePair<string, string> item)
		{
			if (item.Key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item.Key");
			}
			if (item.Value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item.Value");
			}
			ContextDictionary.ValidateKeyValueSpace(item.Key);
			return this.dictionaryStore.Remove(item);
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x001117D3 File Offset: 0x0010F9D3
		public bool TryGetValue(string key, out string value)
		{
			ContextDictionary.ValidateKeyValueSpace(key);
			return this.dictionaryStore.TryGetValue(key, out value);
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x001117E8 File Offset: 0x0010F9E8
		internal static bool TryValidateKeyValueSpace(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			foreach (char c in key)
			{
				if (!ContextDictionary.IsLetterOrDigit(c) && c != '-' && c != '_' && c != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00111840 File Offset: 0x0010FA40
		private static bool IsLetterOrDigit(char c)
		{
			return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z') || ('0' <= c && c <= '9');
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x00111867 File Offset: 0x0010FA67
		private static void ValidateKeyValueSpace(string key)
		{
			if (!ContextDictionary.TryValidateKeyValueSpace(key))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("key", SR.GetString("InvalidCookieContent", new object[]
				{
					key
				})));
			}
		}

		// Token: 0x04002F0D RID: 12045
		private static ContextDictionary empty;

		// Token: 0x04002F0E RID: 12046
		private IDictionary<string, string> dictionaryStore;
	}
}
