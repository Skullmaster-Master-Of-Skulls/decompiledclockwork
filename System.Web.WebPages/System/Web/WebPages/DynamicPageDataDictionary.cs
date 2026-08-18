using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages
{
	// Token: 0x0200006F RID: 111
	internal class DynamicPageDataDictionary<TValue> : DynamicObject, IDictionary<object, TValue>, ICollection<KeyValuePair<object, TValue>>, IEnumerable<KeyValuePair<object, TValue>>, IEnumerable
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000A766 File Offset: 0x00008966
		public DynamicPageDataDictionary(PageDataDictionary<TValue> dictionary)
		{
			this._data = dictionary;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A775 File Offset: 0x00008975
		public ICollection<object> Keys
		{
			get
			{
				return this._data.Keys;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A782 File Offset: 0x00008982
		public ICollection<TValue> Values
		{
			get
			{
				return this._data.Values;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000A78F File Offset: 0x0000898F
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000A79C File Offset: 0x0000899C
		public bool IsReadOnly
		{
			get
			{
				return this._data.IsReadOnly;
			}
		}

		// Token: 0x1700009D RID: 157
		public TValue this[object key]
		{
			get
			{
				return this._data[key];
			}
			set
			{
				this._data[key] = value;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A7C6 File Offset: 0x000089C6
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = this._data[binder.Name];
			return true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A7E4 File Offset: 0x000089E4
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			TValue value2 = (TValue)((object)value);
			this._data[binder.Name] = value2;
			return true;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A80B File Offset: 0x00008A0B
		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			if (indexes == null || indexes.Length != 1)
			{
				throw new ArgumentException(WebPageResources.DynamicDictionary_InvalidNumberOfIndexes);
			}
			result = this._data[indexes[0]];
			return true;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A837 File Offset: 0x00008A37
		public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			if (indexes == null || indexes.Length != 1)
			{
				throw new ArgumentException(WebPageResources.DynamicDictionary_InvalidNumberOfIndexes);
			}
			this._data[indexes[0]] = (TValue)((object)value);
			return true;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A862 File Offset: 0x00008A62
		public void Add(object key, TValue value)
		{
			this._data.Add(key, value);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000A871 File Offset: 0x00008A71
		public bool ContainsKey(object key)
		{
			return this._data.ContainsKey(key);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A87F File Offset: 0x00008A7F
		public bool Remove(object key)
		{
			return this._data.Remove(key);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A88D File Offset: 0x00008A8D
		public bool TryGetValue(object key, out TValue value)
		{
			return this._data.TryGetValue(key, out value);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A89C File Offset: 0x00008A9C
		public void Add(KeyValuePair<object, TValue> item)
		{
			this._data.Add(item);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A8AA File Offset: 0x00008AAA
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public bool Contains(KeyValuePair<object, TValue> item)
		{
			return this._data.Contains(item);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000A8C5 File Offset: 0x00008AC5
		public void CopyTo(KeyValuePair<object, TValue>[] array, int arrayIndex)
		{
			this._data.CopyTo(array, arrayIndex);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		public bool Remove(KeyValuePair<object, TValue> item)
		{
			return this._data.Remove(item.Key);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public IEnumerator<KeyValuePair<object, TValue>> GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000A8F5 File Offset: 0x00008AF5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x040000E4 RID: 228
		private PageDataDictionary<TValue> _data;
	}
}
