using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200065E RID: 1630
	[Serializable]
	public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
	{
		// Token: 0x0600500B RID: 20491 RVA: 0x00114F3F File Offset: 0x0011313F
		public ModelStateDictionary()
		{
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x00114F58 File Offset: 0x00113158
		public ModelStateDictionary(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary)
			{
				this._innerDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x0600500D RID: 20493 RVA: 0x00114FD8 File Offset: 0x001131D8
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x0600500E RID: 20494 RVA: 0x00114FE5 File Offset: 0x001131E5
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).IsReadOnly;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x0600500F RID: 20495 RVA: 0x00114FF2 File Offset: 0x001131F2
		public bool IsValid
		{
			get
			{
				return this.Values.All((ModelState modelState) => modelState.Errors.Count == 0);
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06005010 RID: 20496 RVA: 0x0011501E File Offset: 0x0011321E
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x17001721 RID: 5921
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

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x00115058 File Offset: 0x00113258
		public ICollection<ModelState> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00115065 File Offset: 0x00113265
		public void Add(KeyValuePair<string, ModelState> item)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Add(item);
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x00115073 File Offset: 0x00113273
		public void Add(string key, ModelState value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x00115082 File Offset: 0x00113282
		public void AddModelError(string key, Exception exception)
		{
			this.GetModelStateForKey(key).Errors.Add(exception);
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x00115096 File Offset: 0x00113296
		public void AddModelError(string key, string errorMessage)
		{
			this.GetModelStateForKey(key).Errors.Add(errorMessage);
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x001150AA File Offset: 0x001132AA
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x001150B7 File Offset: 0x001132B7
		public bool Contains(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Contains(item);
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x001150C5 File Offset: 0x001132C5
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x001150D3 File Offset: 0x001132D3
		public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x001150E2 File Offset: 0x001132E2
		public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x001150F4 File Offset: 0x001132F4
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

		// Token: 0x0600501E RID: 20510 RVA: 0x00115129 File Offset: 0x00113329
		public bool IsValidField(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return DictionaryHelpers.FindKeysWithPrefix<ModelState>(this, key).All((KeyValuePair<string, ModelState> entry) => entry.Value.Errors.Count == 0);
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x00115164 File Offset: 0x00113364
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

		// Token: 0x06005020 RID: 20512 RVA: 0x001151C0 File Offset: 0x001133C0
		public bool Remove(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Remove(item);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x001151CE File Offset: 0x001133CE
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x001151DC File Offset: 0x001133DC
		public void SetModelValue(string key, ValueProviderResult value)
		{
			this.GetModelStateForKey(key).Value = value;
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x001151EB File Offset: 0x001133EB
		public bool TryGetValue(string key, out ModelState value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x001151FA File Offset: 0x001133FA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x04002AB3 RID: 10931
		private readonly Dictionary<string, ModelState> _innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);
	}
}
