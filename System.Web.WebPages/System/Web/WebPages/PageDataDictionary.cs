using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Web.WebPages
{
	// Token: 0x0200008D RID: 141
	internal class PageDataDictionary<TValue> : IDictionary<object, TValue>, ICollection<KeyValuePair<object, TValue>>, IEnumerable<KeyValuePair<object, TValue>>, IEnumerable
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		internal IDictionary<object, TValue> Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000DA74 File Offset: 0x0000BC74
		internal IDictionary<string, TValue> StringDictionary
		{
			get
			{
				return this._stringDictionary;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		internal IList<TValue> IndexedValues
		{
			get
			{
				return this._indexedValues;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000DA84 File Offset: 0x0000BC84
		public ICollection<object> Keys
		{
			get
			{
				List<object> list = new List<object>();
				list.AddRange(this._stringDictionary.Keys);
				for (int i = 0; i < this._indexedValues.Count; i++)
				{
					list.Add(i);
				}
				foreach (object obj in this._data.Keys)
				{
					if (!this.ContainsIndex(obj) && !this.ContainsStringKey(obj))
					{
						list.Add(obj);
					}
				}
				return list;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public ICollection<TValue> Values
		{
			get
			{
				List<TValue> list = new List<TValue>();
				foreach (object key in this.Keys)
				{
					list.Add(this[key]);
				}
				return list;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000DB80 File Offset: 0x0000BD80
		internal ICollection<KeyValuePair<object, TValue>> Items
		{
			get
			{
				List<KeyValuePair<object, TValue>> list = new List<KeyValuePair<object, TValue>>();
				foreach (object key in this.Keys)
				{
					TValue value = this[key];
					KeyValuePair<object, TValue> item = new KeyValuePair<object, TValue>(key, value);
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000DBEC File Offset: 0x0000BDEC
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000DBF9 File Offset: 0x0000BDF9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000FB RID: 251
		public TValue this[object key]
		{
			get
			{
				TValue result = default(TValue);
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				if (this.ContainsStringKey(key))
				{
					this._stringDictionary[(string)key] = value;
					return;
				}
				if (this.ContainsIndex(key))
				{
					this._indexedValues[(int)key] = value;
					return;
				}
				this._data[key] = value;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000DC6E File Offset: 0x0000BE6E
		public void Add(object key, TValue value)
		{
			this._data.Add(key, value);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000DC7D File Offset: 0x0000BE7D
		internal bool ContainsIndex(object o)
		{
			return o is int && this.ContainsIndex((int)o);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000DC95 File Offset: 0x0000BE95
		internal bool ContainsIndex(int index)
		{
			return this._indexedValues.Count > index && index >= 0;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		internal bool ContainsStringKey(object o)
		{
			string text = o as string;
			return text != null && this.ContainsStringKey(text);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		internal bool ContainsStringKey(string key)
		{
			return this._stringDictionary.ContainsKey(key);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		public bool ContainsKey(object key)
		{
			return this.ContainsIndex(key) || this.ContainsStringKey(key) || this._data.ContainsKey(key);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000DD08 File Offset: 0x0000BF08
		public bool Remove(object key)
		{
			if (this.ContainsStringKey(key))
			{
				return this._stringDictionary.Remove((string)key);
			}
			if (this.ContainsIndex(key))
			{
				return this._indexedValues.Remove(this._indexedValues[(int)key]);
			}
			return this._data.Remove(key);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public bool TryGetValue(object key, out TValue value)
		{
			if (this.ContainsStringKey(key))
			{
				return this._stringDictionary.TryGetValue((string)key, out value);
			}
			if (this.ContainsIndex(key))
			{
				value = this._indexedValues[(int)key];
				return true;
			}
			return this._data.TryGetValue(key, out value);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		public void Add(KeyValuePair<object, TValue> item)
		{
			this[item.Key] = item.Value;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000DDD2 File Offset: 0x0000BFD2
		public void Clear()
		{
			this._stringDictionary.Clear();
			this._indexedValues.Clear();
			this._data.Clear();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000DDF5 File Offset: 0x0000BFF5
		public bool Contains(KeyValuePair<object, TValue> item)
		{
			return this.ContainsKey(item.Key) && this.Values.Contains(item.Value);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000DE1A File Offset: 0x0000C01A
		public void CopyTo(KeyValuePair<object, TValue>[] array, int arrayIndex)
		{
			this.Items.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000DE29 File Offset: 0x0000C029
		public bool Remove(KeyValuePair<object, TValue> item)
		{
			return this.Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000DE43 File Offset: 0x0000C043
		public IEnumerator<KeyValuePair<object, TValue>> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000DE50 File Offset: 0x0000C050
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000DE60 File Offset: 0x0000C060
		[return: Dynamic(new bool[]
		{
			false,
			false,
			true
		})]
		internal static IDictionary<object, dynamic> CreatePageDataFromParameters(IDictionary<object, dynamic> previousPageData, params object[] data)
		{
			PageDataDictionary<object> pageDataDictionary = previousPageData as PageDataDictionary<object>;
			PageDataDictionary<object> pageDataDictionary2 = new PageDataDictionary<object>();
			foreach (KeyValuePair<object, object> item in pageDataDictionary.Data)
			{
				pageDataDictionary2.Data.Add(item);
			}
			if (data != null && data.Length > 0)
			{
				for (int i = 0; i < data.Length; i++)
				{
					pageDataDictionary2.IndexedValues.Add(data[i]);
				}
				object obj = data[0];
				Type type = obj.GetType();
				if (TypeHelper.IsAnonymousType(type))
				{
					TypeHelper.AddAnonymousObjectToDictionary(pageDataDictionary2.StringDictionary, obj);
				}
				if (typeof(IDictionary<string, object>).IsAssignableFrom(type))
				{
					IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
					foreach (KeyValuePair<string, object> item2 in dictionary)
					{
						pageDataDictionary2.StringDictionary.Add(item2);
					}
				}
			}
			return pageDataDictionary2;
		}

		// Token: 0x0400013A RID: 314
		private IDictionary<object, TValue> _data = new Dictionary<object, TValue>(new PageDataDictionary<TValue>.PageDataComparer());

		// Token: 0x0400013B RID: 315
		private IDictionary<string, TValue> _stringDictionary = new Dictionary<string, TValue>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400013C RID: 316
		private IList<TValue> _indexedValues = new List<TValue>();

		// Token: 0x0200008E RID: 142
		private sealed class PageDataComparer : IEqualityComparer<object>
		{
			// Token: 0x0600047A RID: 1146 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				string text = x as string;
				string text2 = y as string;
				if (text != null && text2 != null)
				{
					return string.Equals(text, text2, StringComparison.OrdinalIgnoreCase);
				}
				return object.Equals(x, y);
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x0000DFDC File Offset: 0x0000C1DC
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				string text = obj as string;
				if (text != null)
				{
					return text.ToUpperInvariant().GetHashCode();
				}
				return obj.GetHashCode();
			}
		}
	}
}
