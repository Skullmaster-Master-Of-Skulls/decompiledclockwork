using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebPages.Html
{
	// Token: 0x02000087 RID: 135
	public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000CF3E File Offset: 0x0000B13E
		public ModelStateDictionary()
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CF58 File Offset: 0x0000B158
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000CFE5 File Offset: 0x0000B1E5
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).IsReadOnly;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000CFFA File Offset: 0x0000B1FA
		public bool IsValid
		{
			get
			{
				return !this.Values.SelectMany((ModelState modelState) => modelState.Errors).Any<string>();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000D02C File Offset: 0x0000B22C
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000D039 File Offset: 0x0000B239
		public ICollection<ModelState> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x170000E5 RID: 229
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

		// Token: 0x06000418 RID: 1048 RVA: 0x0000D074 File Offset: 0x0000B274
		public void Add(KeyValuePair<string, ModelState> item)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Add(item);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D082 File Offset: 0x0000B282
		public void Add(string key, ModelState value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000D091 File Offset: 0x0000B291
		public void AddError(string key, string errorMessage)
		{
			this.GetModelStateForKey(key).Errors.Add(errorMessage);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000D0A5 File Offset: 0x0000B2A5
		public void AddFormError(string errorMessage)
		{
			this.GetModelStateForKey("_FORM").Errors.Add(errorMessage);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000D0BD File Offset: 0x0000B2BD
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		public bool Contains(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Contains(item);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000D0E6 File Offset: 0x0000B2E6
		public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000D0F5 File Offset: 0x0000B2F5
		public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000D108 File Offset: 0x0000B308
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
				this._innerDictionary[key] = modelState;
			}
			return modelState;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000D144 File Offset: 0x0000B344
		public bool IsValidField(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ModelState modelState = this[key];
			return modelState == null || !modelState.Errors.Any<string>();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000D17C File Offset: 0x0000B37C
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

		// Token: 0x06000424 RID: 1060 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		public bool Remove(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Remove(item);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000D1E6 File Offset: 0x0000B3E6
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		public bool TryGetValue(string key, out ModelState value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000D204 File Offset: 0x0000B404
		public void SetModelValue(string key, object value)
		{
			ModelState modelStateForKey = this.GetModelStateForKey(key);
			modelStateForKey.Value = value;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000D220 File Offset: 0x0000B420
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x04000129 RID: 297
		internal const string FormFieldKey = "_FORM";

		// Token: 0x0400012A RID: 298
		private readonly Dictionary<string, ModelState> _innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);
	}
}
