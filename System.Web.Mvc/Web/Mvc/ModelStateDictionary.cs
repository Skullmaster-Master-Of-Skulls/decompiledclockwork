using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x000233BE File Offset: 0x000215BE
		public ModelStateDictionary()
		{
			this._innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000233D6 File Offset: 0x000215D6
		public ModelStateDictionary(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._innerDictionary = new CopyOnWriteDictionary<string, ModelState>(dictionary, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x000233FD File Offset: 0x000215FD
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002340A File Offset: 0x0002160A
		public bool IsReadOnly
		{
			get
			{
				return this._innerDictionary.IsReadOnly;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00023427 File Offset: 0x00021627
		public bool IsValid
		{
			get
			{
				return this.Values.All((ModelState modelState) => modelState.Errors.Count == 0);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00023451 File Offset: 0x00021651
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0002345E File Offset: 0x0002165E
		public ICollection<ModelState> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x170002E9 RID: 745
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

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00023498 File Offset: 0x00021698
		internal IDictionary<string, ModelState> InnerDictionary
		{
			get
			{
				return this._innerDictionary;
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000234A0 File Offset: 0x000216A0
		public void Add(KeyValuePair<string, ModelState> item)
		{
			this._innerDictionary.Add(item);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000234AE File Offset: 0x000216AE
		public void Add(string key, ModelState value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000234BD File Offset: 0x000216BD
		public void AddModelError(string key, Exception exception)
		{
			this.GetModelStateForKey(key).Errors.Add(exception);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000234D1 File Offset: 0x000216D1
		public void AddModelError(string key, string errorMessage)
		{
			this.GetModelStateForKey(key).Errors.Add(errorMessage);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000234E5 File Offset: 0x000216E5
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000234F2 File Offset: 0x000216F2
		public bool Contains(KeyValuePair<string, ModelState> item)
		{
			return this._innerDictionary.Contains(item);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00023500 File Offset: 0x00021700
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002350E File Offset: 0x0002170E
		public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
		{
			this._innerDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002351D File Offset: 0x0002171D
		public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002352C File Offset: 0x0002172C
		private ModelState GetModelStateForKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ModelState modelState;
			if (!this.TryGetValue(key, out modelState))
			{
				modelState = new ModelState();
				this[key] = modelState;
			}
			return modelState;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00023577 File Offset: 0x00021777
		public bool IsValidField(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return DictionaryHelpers.FindKeysWithPrefix<ModelState>(this, key).All((KeyValuePair<string, ModelState> entry) => entry.Value.Errors.Count == 0);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000235B0 File Offset: 0x000217B0
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

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002360C File Offset: 0x0002180C
		public bool Remove(KeyValuePair<string, ModelState> item)
		{
			return this._innerDictionary.Remove(item);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002361A File Offset: 0x0002181A
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00023628 File Offset: 0x00021828
		public void SetModelValue(string key, ValueProviderResult value)
		{
			this.GetModelStateForKey(key).Value = value;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00023637 File Offset: 0x00021837
		public bool TryGetValue(string key, out ModelState value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00023646 File Offset: 0x00021846
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000368 RID: 872
		private readonly IDictionary<string, ModelState> _innerDictionary;
	}
}
