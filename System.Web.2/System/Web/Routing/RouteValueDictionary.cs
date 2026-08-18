using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x0200014F RID: 335
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteValueDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x0600136D RID: 4973 RVA: 0x00038853 File Offset: 0x00036A53
		public RouteValueDictionary()
		{
			this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0003886B File Offset: 0x00036A6B
		public RouteValueDictionary(object values)
		{
			this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this.AddValues(values);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0003888A File Offset: 0x00036A8A
		public RouteValueDictionary(IDictionary<string, object> dictionary)
		{
			this._dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x000388A3 File Offset: 0x00036AA3
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x000388B0 File Offset: 0x00036AB0
		public Dictionary<string, object>.KeyCollection Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x000388BD File Offset: 0x00036ABD
		public Dictionary<string, object>.ValueCollection Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x170005E4 RID: 1508
		public object this[string key]
		{
			get
			{
				object result;
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this._dictionary[key] = value;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000388F3 File Offset: 0x00036AF3
		public void Add(string key, object value)
		{
			this._dictionary.Add(key, value);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00038904 File Offset: 0x00036B04
		private void AddValues(object values)
		{
			if (values != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(values);
				foreach (object obj in properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					object value = propertyDescriptor.GetValue(values);
					this.Add(propertyDescriptor.Name, value);
				}
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00038974 File Offset: 0x00036B74
		public void Clear()
		{
			this._dictionary.Clear();
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00038981 File Offset: 0x00036B81
		public bool ContainsKey(string key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003898F File Offset: 0x00036B8F
		public bool ContainsValue(object value)
		{
			return this._dictionary.ContainsValue(value);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0003899D File Offset: 0x00036B9D
		public Dictionary<string, object>.Enumerator GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000389AA File Offset: 0x00036BAA
		public bool Remove(string key)
		{
			return this._dictionary.Remove(key);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000389B8 File Offset: 0x00036BB8
		public bool TryGetValue(string key, out object value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x000388B0 File Offset: 0x00036AB0
		ICollection<string> IDictionary<string, object>.Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x000388BD File Offset: 0x00036ABD
		ICollection<object> IDictionary<string, object>.Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000389C7 File Offset: 0x00036BC7
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			((ICollection<KeyValuePair<string, object>>)this._dictionary).Add(item);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000389D5 File Offset: 0x00036BD5
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this._dictionary).Contains(item);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000389E3 File Offset: 0x00036BE3
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, object>>)this._dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x000389F2 File Offset: 0x00036BF2
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, object>>)this._dictionary).IsReadOnly;
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000389FF File Offset: 0x00036BFF
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this._dictionary).Remove(item);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00038A0D File Offset: 0x00036C0D
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00038A0D File Offset: 0x00036C0D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040014DF RID: 5343
		private Dictionary<string, object> _dictionary;
	}
}
