using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x0001AE5C File Offset: 0x0001905C
		public ModelStateDictionary()
		{
			this._innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001AE74 File Offset: 0x00019074
		public ModelStateDictionary(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw Error.ArgumentNull("dictionary");
			}
			this._innerDictionary = new Dictionary<string, ModelState>(dictionary, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001AE9B File Offset: 0x0001909B
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001AEA8 File Offset: 0x000190A8
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).IsReadOnly;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001AEC5 File Offset: 0x000190C5
		public bool IsValid
		{
			get
			{
				return this.Values.All((ModelState modelState) => modelState.Errors.Count == 0);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001AEEF File Offset: 0x000190EF
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001AEFC File Offset: 0x000190FC
		public ICollection<ModelState> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x17000280 RID: 640
		public ModelState this[string key]
		{
			get
			{
				ModelState result;
				this._innerDictionary.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this._innerDictionary[key] = value;
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001AF38 File Offset: 0x00019138
		public void Add(KeyValuePair<string, ModelState> item)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Add(item);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001AF46 File Offset: 0x00019146
		public void Add(string key, ModelState value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001AF55 File Offset: 0x00019155
		public void AddModelError(string key, Exception exception)
		{
			this.GetModelStateForKey(key).Errors.Add(exception);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001AF69 File Offset: 0x00019169
		public void AddModelError(string key, string errorMessage)
		{
			this.GetModelStateForKey(key).Errors.Add(errorMessage);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001AF7D File Offset: 0x0001917D
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001AF8A File Offset: 0x0001918A
		public bool Contains(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Contains(item);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001AF98 File Offset: 0x00019198
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001AFA6 File Offset: 0x000191A6
		public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001AFB5 File Offset: 0x000191B5
		public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001AFC8 File Offset: 0x000191C8
		private ModelState GetModelStateForKey(string key)
		{
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			ModelState modelState;
			if (!this.TryGetValue(key, out modelState))
			{
				modelState = new ModelState();
				this[key] = modelState;
			}
			return modelState;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001B000 File Offset: 0x00019200
		public bool IsValidField(string key)
		{
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in this.FindKeysWithPrefix(key))
			{
				if (keyValuePair.Value.Errors.Count != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001B070 File Offset: 0x00019270
		public void Merge(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary)
			{
				this[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001B0CC File Offset: 0x000192CC
		public bool Remove(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Remove(item);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001B0DA File Offset: 0x000192DA
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001B0E8 File Offset: 0x000192E8
		public void SetModelValue(string key, ValueProviderResult value)
		{
			this.GetModelStateForKey(key).Value = value;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001B0F7 File Offset: 0x000192F7
		public bool TryGetValue(string key, out ModelState value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001B106 File Offset: 0x00019306
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x0400026C RID: 620
		private readonly Dictionary<string, ModelState> _innerDictionary;
	}
}
